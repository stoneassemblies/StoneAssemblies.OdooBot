using Coravel.Invocable;
using Microsoft.EntityFrameworkCore;
using PortaCapena.OdooJsonRpcClient;
using PortaCapena.OdooJsonRpcClient.Consts;
using StoneAssemblies.EntityFrameworkCore.Services.Interfaces;
using StoneAssemblies.OdooBot.Entities;
using StoneAssemblies.OdooBot.Services;
using StoneAssemblies.OdooBot.Tests;
using System.Reflection;
using MethodTimer;
using PortaCapena.OdooJsonRpcClient.Extensions;
using StoneAssemblies.OdooBot;
using StoneAssemblies.OdooBot.Models;
using static OddooSyncInvocable;

public class OddooSyncInvocable(ILogger<OddooSyncInvocable> logger, IServiceProvider provider,
        IOdooRepository<ProductCategoryOdooModel> productCategoryRepository,
        IOdooRepository<ProductTemplateOdooModel> productTemplateRepository,
        IOdooRepository<ProductImageOdooModel> productImageRepository,
        IOdooRepository<IrTranslationOdooModel> irTranslationRepository,
        IUnitOfWork<ApplicationDbContext> unitOfWork)
    : IInvocable
{
    private const string ImagePropertyFormat = "Image{0}";

    [Time]
    public async Task Invoke()
    {
        logger.LogInformation("Synchronizing ...");

        await SynchronizeCategoriesAsync();
        await SynchronizeProductsAsync();

        logger.LogInformation("Synchronized ...");
    }

    [Time]
    private async Task SynchronizeProductsAsync()
    {
        logger.LogInformation("Synchronizing products...");

        await SynchronizingNewProductsAsync();
        await SynchronizingExistingProductsAsync();

        logger.LogInformation("Synchronized products...");
    }

    [Time]
    private async Task SynchronizingExistingProductsAsync()
    {
        logger.LogInformation("Synchronizing existing products...");

        var categoryRepository = unitOfWork.GetRepository<Category>();
        var productRepository = unitOfWork.GetRepository<Product>();

        var categories = await categoryRepository.All().ToListAsync();
        foreach (var category in categories)
        {
            logger.LogInformation("Synchronizing existing products for category '{Category}'...", category.Name);

            var productIds = await productRepository
                .Find(product => product.CategoryId == category.Id)
                .Select(product => product.ExternalId)
                .ToArrayAsync();

            var query = productTemplateRepository.Query().ByIds(productIds).Select(model => new
            {
                model.Id,
                model.DescriptionSale,
                model.Image128,
                model.Image256,
                model.Image512,
                model.Image1024,
                model.Image1920,
                model.ProductTemplateImageIds,
            });

            int count = 0;
            await foreach (var productTemplateOdooModel in query.GetAsync())
            {
                logger.LogInformation("Updating product '{OdooProductId}' - '{CategoryName}'", productTemplateOdooModel.Id, category.Name);

                var transaction = unitOfWork.BeginTransaction();

                try
                {
                    await UpdateProductAsync(productTemplateOdooModel);
                    await UpdateFeatureImagesAsync(productTemplateOdooModel);
                    await DeleteUnusedImagesAsync(productTemplateOdooModel);
                    await UpdateImagesAsync(productTemplateOdooModel);

                    await transaction.CommitAsync();

                    logger.LogInformation("Updated product '{OdooProductId}' - '{CategoryName}'", productTemplateOdooModel.Id, category.Name);
                    count++;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    logger.LogError(ex, "Error occurred updating product '{OdooProductId}' - '{CategoryName}'", productTemplateOdooModel.Id, category.Name);
                }
            }

            logger.LogInformation("Synchronized '{ProductCount}' products for category '{Category}'...", count, category.Name);
        }

        logger.LogInformation("Synchronized existing products...");
    }

    [Time]
    private async Task UpdateProductAsync(ProductTemplateOdooModel productTemplateOdooModel)
    {
        var changeDetected = false;

        var productRepository = unitOfWork.GetRepository<Product>();
        var product = await productRepository
            .SingleAsync(product => product.ExternalId == productTemplateOdooModel.Id);

        logger.LogInformation("Detecting changes in product '{OdooProductId}' - '{ProductName}'", productTemplateOdooModel.Id, product.Name);

        var nameTranslationResult = await irTranslationRepository
            .Query()
            .Where(model => model.ResId, OdooOperator.EqualsTo, product.ExternalId)
            .Where(model => model.Lang, OdooOperator.EqualsTo,
                LanguageIrTranslationOdooEnum.SpanishEspaOl)
            .Where(model => model.Name, OdooOperator.EqualsTo, "product.template,name")
            .Where(model => model.Value, OdooOperator.NotEqualsTo, product.Name)
            .FirstOrDefaultAsync();

        if (nameTranslationResult?.Value?.Value is not null)
        {
            changeDetected = true;
            logger.LogInformation("Detected change in name of product '{OdooProductId}' - '{ProductName}'", productTemplateOdooModel.Id, product.Name);
            product.Name = nameTranslationResult.Value.Value.Trim();
        }

        var descriptionTranslationResult = await irTranslationRepository
            .Query()
            .Where(model => model.ResId, OdooOperator.EqualsTo, product.ExternalId)
            .Where(model => model.Lang, OdooOperator.EqualsTo,
                LanguageIrTranslationOdooEnum.SpanishEspaOl)
            .Where(model => model.Name, OdooOperator.EqualsTo, "product.template,description_sale")
            .Where(model => model.Value, OdooOperator.NotEqualsTo, product.Description)
            .FirstOrDefaultAsync();

        if (nameTranslationResult?.Value?.Value is not null)
        {
            changeDetected = true;
            logger.LogInformation("Detected change in description of product '{OdooProductId}' - '{ProductName}'", productTemplateOdooModel.Id, product.Name);
            product.Description = (descriptionTranslationResult?.Value?.Value ??
                                   productTemplateOdooModel.DescriptionSale).Trim();
        }

        if (changeDetected)
        {
            logger.LogInformation("Updating existing product '{OdooProductId}' - '{ProductName}'", productTemplateOdooModel.Id, product.Name);

            productRepository.Update(product);
            await productRepository.SaveChangesAsync();

            logger.LogInformation("Updated existing product '{OdooProductId}' - '{ProductName}'", productTemplateOdooModel.Id, product.Name);
        }
    }

    [Time]
    private async Task DeleteUnusedImagesAsync(ProductTemplateOdooModel productTemplateOdooModel)
    {
        var productRepository = unitOfWork.GetRepository<Product>();
        var product = await productRepository
            .SingleAsync(product => product.ExternalId == productTemplateOdooModel.Id);

        logger.LogInformation("Deleting unused images of product '{OdooProductId}' - '{ProductName}'", productTemplateOdooModel.Id, product.Name);

        var productTemplateImageIds = productTemplateOdooModel.ProductTemplateImageIds;

        var imageRepository = unitOfWork.GetRepository<Image>();

        imageRepository.Delete(image => image.ProductId == product.Id && !image.IsFeatured && image.ExternalId == null);
        imageRepository.Delete(image => image.ProductId == product.Id && image.ExternalId != null && !productTemplateImageIds.Contains(image.ExternalId.Value));

        await imageRepository.SaveChangesAsync();

        logger.LogInformation("Deleted unused images of product '{OdooProductId}' - '{ProductName}'", productTemplateOdooModel.Id, product.Name);
    }

    [Time]
    private async Task UpdateFeatureImagesAsync(ProductTemplateOdooModel productTemplateOdooModel)
    {
        var productRepository = unitOfWork.GetRepository<Product>();
        var product = await productRepository
            .SingleAsync(product => product.ExternalId == productTemplateOdooModel.Id);

        var imageRepository = unitOfWork.GetRepository<Image>();

        logger.LogInformation("Detecting changes in feature images for product '{OdooProductId}' - '{ProductName}'", productTemplateOdooModel.Id, product.Name);
        foreach (var imageSize in Enum.GetValues<ImageSize>())
        {
            var changeDetected = false;
            var encodeContent = typeof(ProductTemplateOdooModel).GetProperty(
                    string.Format(ImagePropertyFormat, Convert.ToInt32(imageSize)),
                    BindingFlags.Public | BindingFlags.Instance)?.GetValue(productTemplateOdooModel)
                ?.ToString();

            if (!string.IsNullOrWhiteSpace(encodeContent))
            {
                try
                {
                    var content = Convert.FromBase64String(encodeContent);
                    var featuredImage = await imageRepository.SingleOrDefaultAsync(image =>
                        image.ProductId == product.Id && image.Size == imageSize && image.IsFeatured);

                    if (featuredImage is null)
                    {
                        logger.LogInformation(
                            "Adding new feature image of size '{ImageSize}' to product '{OdooProductId}' - '{ProductName}'", imageSize, productTemplateOdooModel.Id, product.Name);

                        featuredImage = new Image
                        {
                            ProductId = product.Id,
                            Size = imageSize,
                            Content = content,
                            IsFeatured = true
                        };

                        imageRepository.Add(featuredImage);
                        changeDetected = true;
                    }
                    else if (!content.SequenceEqual(featuredImage.Content))
                    {
                        logger.LogInformation(
                            "Detected change in feature image of size '{ImageSize}' to product '{OdooProductId}' - '{ProductName}'", imageSize, productTemplateOdooModel.Id, product.Name);

                        featuredImage.Content = content;
                        imageRepository.Update(featuredImage);
                        changeDetected = true;
                    }

                    if (changeDetected)
                    {
                        await imageRepository.SaveChangesAsync();

                        logger.LogInformation("Updated feature image of size '{ImageSize}' to product '{OdooProductId}' - '{ProductName}'", imageSize, productTemplateOdooModel.Id, product.Name);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex,
                        "Error adding featured image of size '{ImageSize}' to product '{OdooProductId}' - '{ProductName}'", imageSize, productTemplateOdooModel.Id, product.Name);
                }
            }
        }
    }

    public class ImageWithoutContent
    {
        public long ExternalId { get; set; }
        public DateTime? LastUpdate { get; set; }
        public Guid Id { get; set; }
    }

    [Time]
    private async Task UpdateImagesAsync(ProductTemplateOdooModel productTemplateOdooModel)
    {
        var productRepository = unitOfWork.GetRepository<Product>();
        var product = await productRepository
            .SingleAsync(product => product.ExternalId == productTemplateOdooModel.Id);

        var productTemplateImageIds = productTemplateOdooModel.ProductTemplateImageIds;

        var imageRepository = unitOfWork.GetRepository<Image>();

        List<Image> images = (await imageRepository
            .FindAsync(SpecificationBuilder.Build<Image>(images =>
                images.Where(image => image.ProductId == product.Id && image.ExternalId != null)))).ToList();

        logger.LogInformation("Detecting changes in images for '{OdooProductId}' - '{ProductName}'", productTemplateOdooModel.Id, product.Name);

        var cache = new Dictionary<long, ProductImageOdooModel>();

        foreach (var image in images)
        {
            ProductImageOdooModel? productImageOdooModel;
            if (!cache.TryGetValue(image.ExternalId!.Value, out productImageOdooModel))
            {
                var query = productImageRepository.Query().ById(image.ExternalId.Value).Select(model => new { model.WriteDate });
                var result = await query.FirstOrDefaultAsync();

                // PATCH: this is not working => query = query.Where(model => model.WriteDate, OdooOperator.GreaterThan, imageLastUpdate);
                if (result?.Value?.WriteDate > image.LastUpdate)
                {
                    cache[image.ExternalId.Value] = result.Value;
                    query = query.Select(model => new
                    {
                        model.Image128,
                        model.Image256,
                        model.Image512,
                        model.Image1024,
                        model.Image1920,
                    });

                    result = await query.FirstOrDefaultAsync();
                    if (result?.Value is not null)
                    {
                        productImageOdooModel = cache[image.ExternalId.Value] = result.Value;
                    }
                }
            }

            if (productImageOdooModel is not null && productImageOdooModel.WriteDate > image.LastUpdate)
            {
                logger.LogInformation("Detected change in images of product '{OdooProductId}' - '{ProductName}'", productTemplateOdooModel.Id, product.Name);

                foreach (var imageSize in Enum.GetValues<ImageSize>())
                {
                    var encodeContent = typeof(ProductImageOdooModel).GetProperty(
                            string.Format(ImagePropertyFormat, Convert.ToInt32(imageSize)),
                            BindingFlags.Public | BindingFlags.Instance)?.GetValue(productImageOdooModel)
                        ?.ToString();

                    if (!string.IsNullOrWhiteSpace(encodeContent))
                    {
                        try
                        {
                            logger.LogInformation(
                                "Updating image of size {ImageSize} of product {ProductName}",
                                imageSize, product.Name);

                            var content = Convert.FromBase64String(encodeContent);
                            image.Content = content;
                            image.LastUpdate = productImageOdooModel.WriteDate!.Value.Date;
                            imageRepository.Update(image);

                            await imageRepository.SaveChangesAsync();

                            logger.LogInformation(
                                "Updated image of size '{ImageSize}' to product '{OdooProductId}' - '{ProductName}'", imageSize, productTemplateOdooModel.Id, product.Name);
                        }
                        catch (Exception ex)
                        {
                            logger.LogWarning(ex,
                                "Error updating image of size '{ImageSize}' to product '{OdooProductId}' - '{ProductName}'", imageSize, productTemplateOdooModel.Id, product.Name);
                        }
                    }
                }
            }
        }

        var productIds = productTemplateImageIds.ToHashSet();
        foreach (var id in images.Select(image => image.ExternalId!.Value).Distinct())
        {
            productIds.Remove(id);
        }

        if (productIds.Count > 0)
        {
            var imageQuery = productImageRepository.Query()
                .Where(model => model.Id, OdooOperator.In, productIds);

            await foreach (var productImageOdooModel in imageQuery.GetAsync())
            {
                foreach (var imageSize in Enum.GetValues<ImageSize>())
                {
                    var encodeContent = typeof(ProductImageOdooModel).GetProperty(
                            string.Format(ImagePropertyFormat, Convert.ToInt32(imageSize)),
                            BindingFlags.Public | BindingFlags.Instance)?.GetValue(productImageOdooModel)
                        ?.ToString();

                    if (!string.IsNullOrWhiteSpace(encodeContent))
                    {
                        try
                        {
                            var content = Convert.FromBase64String(encodeContent);

                            logger.LogInformation(
                                "Adding new image of size '{ImageSize}' to product '{OdooProductId}' - '{ProductName}'", imageSize, productTemplateOdooModel.Id, product.Name);

                            var image = new Image
                            {
                                ProductId = product.Id,
                                ExternalId = productImageOdooModel.Id,
                                Size = imageSize,
                                LastUpdate = productImageOdooModel.WriteDate,
                                Content = content,
                                IsFeatured = false
                            };

                            imageRepository.Add(image);
                            await imageRepository.SaveChangesAsync();

                            logger.LogInformation(
                                "Added new image of size '{ImageSize}' to product '{OdooProductId}' - '{ProductName}'", imageSize, productTemplateOdooModel.Id, product.Name);
                        }
                        catch (Exception ex)
                        {
                            logger.LogWarning(ex,
                                "Error adding image of size '{ImageSize}' to product '{OdooProductId}' - '{ProductName}'", imageSize, productTemplateOdooModel.Id, product.Name);
                        }
                    }
                }
            }
        }
    }

    [Time]
    private async Task SynchronizingNewProductsAsync()
    {
        logger.LogInformation("Synchronizing new products...");

        var categoryRepository = unitOfWork.GetRepository<Category>();
        var productRepository = unitOfWork.GetRepository<Product>();

        // TODO: Improve this later // async from database?
        var categories = await categoryRepository.All().ToListAsync();
        foreach (var category in categories)
        {
            logger.LogInformation("Synchronizing new products for category '{Category}'...", category.Name);

            var productIds = await productRepository
                .Find(product => product.CategoryId == category.Id)
                .Select(product => product.ExternalId)
                .ToArrayAsync();

            var productTemplateQuery = productTemplateRepository.Query()
                .Where(p => p.CategId, OdooOperator.EqualsTo, category.ExternalId)
                .Where(p => p.Id, OdooOperator.NotIn, productIds);

            var countResult = await productTemplateQuery
                .CountAsync();

            if (countResult?.Value is not null)
            {
                productTemplateQuery = productTemplateQuery.Select(productTemplateOdooModel => new
                {
                    productTemplateOdooModel.Id,
                    productTemplateOdooModel.DisplayName,
                    productTemplateOdooModel.DescriptionSale
                });

                await foreach (var productTemplateOdooModel in productTemplateQuery.GetAsync())
                {
                    var product = new Product
                    {
                        CategoryId = category.Id,
                        ExternalId = productTemplateOdooModel.Id,
                    };

                    var nameTranslationResult = await irTranslationRepository
                        .Query()
                        .Where(model => model.ResId, OdooOperator.EqualsTo, product.ExternalId)
                        .Where(model => model.Lang, OdooOperator.EqualsTo,
                            LanguageIrTranslationOdooEnum.SpanishEspaOl)
                        .Where(model => model.Name, OdooOperator.EqualsTo, "product.template,name")
                        .FirstOrDefaultAsync();

                    product.Name = (nameTranslationResult?.Value?.Value ?? productTemplateOdooModel.DisplayName)?.Trim() ?? string.Empty;

                    logger.LogInformation("Adding new product '{ProductName}'", product.Name);
                    var descriptionTranslationResult = await irTranslationRepository
                        .Query()
                        .Where(model => model.ResId, OdooOperator.EqualsTo, product.ExternalId)
                        .Where(model => model.Lang, OdooOperator.EqualsTo,
                            LanguageIrTranslationOdooEnum.SpanishEspaOl)
                        .Where(model => model.Name, OdooOperator.EqualsTo, "product.template,description_sale")
                        .FirstOrDefaultAsync();

                    product.Description = (descriptionTranslationResult?.Value?.Value ??
                                           productTemplateOdooModel.DescriptionSale)?.Trim() ?? string.Empty;

                    productRepository.Add(product);
                    await productRepository.SaveChangesAsync();
                }

                logger.LogInformation("Synchronized '{ProductCount}' new products for category '{Category}'...", countResult.Value, category.Name);
            }
        }

        logger.LogInformation("Synchronized new products...");
    }

    private async Task SynchronizeCategoriesAsync()
    {
        logger.LogInformation("Synchronizing categories...");

        var categoryQuery = productCategoryRepository.Query()
            .Where(c => c.ParentId, OdooOperator.EqualsTo, null)
            .Where(c => c.Id, OdooOperator.GreaterThan, 1);

        var categoryRepository = unitOfWork.GetRepository<Category>();
        await foreach (var productCategoryOdooModel in categoryQuery.GetAsync())
        {
            logger.LogInformation("Synchronizing category '{Category}'...", productCategoryOdooModel.Name);
            var category =
                await categoryRepository.SingleOrDefaultAsync(c => c.ExternalId == productCategoryOdooModel.Id);
            if (category is null)
            {
                category = new Category
                {
                    Name = productCategoryOdooModel.DisplayName,
                    ExternalId = productCategoryOdooModel.Id
                };

                categoryRepository.Add(category);
            }
            else
            {
                category.Name = productCategoryOdooModel.DisplayName;
                categoryRepository.Update(category);
            }

            await categoryRepository.SaveChangesAsync();
            logger.LogInformation("Synchronized category '{Category}'...", productCategoryOdooModel.Name);
        }

        logger.LogInformation("Synchronized categories...");
    }
}