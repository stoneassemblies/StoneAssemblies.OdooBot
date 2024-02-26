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
using StoneAssemblies.OdooBot;
using StoneAssemblies.OdooBot.Models;

using Image = StoneAssemblies.OdooBot.Entities.Image;

public class OddooSyncInvocable(ILogger<OddooSyncInvocable> logger, IServiceProvider provider,
        IOdooRepository<ProductCategoryOdooModel> productCategoryRepository,
        IOdooRepository<ProductTemplateOdooModel> productTemplateRepository,
        IOdooRepository<ProductImageOdooModel> productImageRepository,
        IOdooRepository<IrTranslationOdooModel> irTranslationRepository,
        IOdooRepository<ProductPricelistItemOdooModel> productPricelistItemRepository,
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
            logger.LogInformation("Synchronizing existing products of category '{CategoryName}'...", category.Name);

            var externalIds = await productRepository
                .Find(product => product.CategoryId == category.Id)
                .Select(product => product.ExternalId)
                .ToArrayAsync();

            int count = 0;
            foreach (var externalId in externalIds)
            {
                logger.LogInformation("Updating product '{ExternalId}' of category '{CategoryName}'", externalId, category.Name);

                var transaction = unitOfWork.BeginTransaction();

                try
                {
                    await UpdateProductAsync(externalId);
                    await UpdateFeatureImagesAsync(externalId);
                    await DeleteUnusedImagesAsync(externalId);
                    await UpdateImagesAsync(externalId);
                    await transaction.CommitAsync();

                    logger.LogInformation("Updated product '{ExternalId}' of category '{CategoryName}'", externalId, category.Name);

                    count++;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    logger.LogError(ex, "Error occurred updating product '{ExternalId}' - '{CategoryName}'", externalId, category.Name);
                }
            }

            logger.LogInformation("Synchronized '{ProductCount}' products of category '{CategoryName}'", count, category.Name);
        }

        logger.LogInformation("Synchronized existing products");
    }

    [Time]
    private async Task UpdateProductAsync(long externalId)
    {
        logger.LogInformation("Updating product '{ExternalId}' ", externalId);

        var changeDetected = false;

        var productRepository = unitOfWork.GetRepository<Product>();
        var product = await productRepository
            .SingleOrDefaultAsync(product => product.ExternalId == externalId);

        if (product is null)
        {
            logger.LogInformation("Update product skipped. Product '{ExternalId}' not found", externalId);
            return;
        }

        var queryResult = await productTemplateRepository.Query().ById(externalId).Select(model => new
        {
            model.Id,
            model.DescriptionSale,
            model.QtyAvailable,
            model.VirtualAvailable,
            model.StandardPrice,
            model.UomName
        }).FirstOrDefaultAsync();

        var productTemplateOdooModel = queryResult?.Value;
        if (productTemplateOdooModel is null)
        {
            logger.LogInformation("Unable to retrieve information of product '{ExternalId}'.", externalId);
            return;
        }

        logger.LogInformation("Detecting changes in product '{ExternalId}' - '{ProductName}'", productTemplateOdooModel.Id, product.Name);

        var standardPrice = productTemplateOdooModel.StandardPrice.GetValueOrDefault(0.0d);
        if (Math.Abs(product.StandardPrice - standardPrice) > 0.0001)
        {
            changeDetected = true;
            product.StandardPrice = standardPrice;
        }

        var inStockQuantity = productTemplateOdooModel.QtyAvailable.GetValueOrDefault(0.0d);
        if (Math.Abs(product.InStockQuantity - inStockQuantity) > 0.0001 || product.QuantityUnit != productTemplateOdooModel.UomName)
        {
            changeDetected = true;
            product.InStockQuantity = inStockQuantity;
            product.QuantityUnit = productTemplateOdooModel.UomName;
        }


        var incomingQuantity = Math.Max(productTemplateOdooModel.VirtualAvailable.GetValueOrDefault(0.0d) - product.InStockQuantity, 0.0d);
        if (Math.Abs(product.IncomingQuantity - incomingQuantity) > 0.0001)
        {
            changeDetected = true;
            product.IncomingQuantity = incomingQuantity;
        }

        var productPricelistItemResult = await productPricelistItemRepository.Query()
                                         .Where(model => model.Active, OdooOperator.EqualsTo, true).Where(
                                             model => model.ProductId,
                                             OdooOperator.EqualsTo,
                                             productTemplateOdooModel.Id).FirstOrDefaultAsync();

        // TODO: Improve this later
        double price;
        if (productPricelistItemResult?.Value is not null)
        {
            var productPricelistItemOdooModel = productPricelistItemResult.Value;
            price = productPricelistItemOdooModel.FixedPrice.GetValueOrDefault(0.0d);
        }
        else
        {
            price = product.StandardPrice;
        }

        if (Math.Abs(product.Price - price) > 0.0001)
        {
            changeDetected = true;
            product.Price = price;
        }

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
            logger.LogInformation("Detected change in name of product '{ExternalId}' - '{ProductName}'", productTemplateOdooModel.Id, product.Name);
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
            logger.LogInformation("Detected change in description of product '{ExternalId}' - '{ProductName}'", productTemplateOdooModel.Id, product.Name);
            product.Description = (descriptionTranslationResult?.Value?.Value ??
                                   productTemplateOdooModel.DescriptionSale).Trim();
        }

        if (changeDetected)
        {
            logger.LogInformation("Updating existing product '{ExternalId}' - '{ProductName}'", productTemplateOdooModel.Id, product.Name);

            productRepository.Update(product);
            await productRepository.SaveChangesAsync();

            logger.LogInformation("Updated existing product '{ExternalId}' - '{ProductName}'", productTemplateOdooModel.Id, product.Name);
        }
    }

    [Time]
    private async Task DeleteUnusedImagesAsync(long externalId)
    {
        logger.LogInformation("Deleting unused images of product '{ExternalId}'", externalId);

        var productRepository = unitOfWork.GetRepository<Product>();
        var product = await productRepository
            .SingleOrDefaultAsync(product => product.ExternalId == externalId);

        if (product is null)
        {
            logger.LogInformation("Deleted unused images of product skipped. Product '{ExternalId}' not found", externalId);
            return;
        }

        var queryResult = await productTemplateRepository.Query().ById(externalId).Select(model => new
        {
            model.Id,
            model.ProductTemplateImageIds,
        }).FirstOrDefaultAsync();

        var productTemplateOdooModel = queryResult.Value;
        if (productTemplateOdooModel is null)
        {
            logger.LogInformation("Unable to retrieve images of product '{ExternalId}'.", externalId);
            return;
        }

        logger.LogInformation("Deleting unused images of product '{ExternalId}' - '{ProductName}'", externalId, product.Name);

        var productTemplateImageIds = productTemplateOdooModel.ProductTemplateImageIds;

        var imageRepository = unitOfWork.GetRepository<Image>();

        imageRepository.Delete(image => image.ProductId == product.Id && !image.IsFeatured && image.ExternalId == null);
        imageRepository.Delete(image => image.ProductId == product.Id && image.ExternalId != null && !productTemplateImageIds.Contains(image.ExternalId.Value));

        await imageRepository.SaveChangesAsync();

        logger.LogInformation("Deleted unused images of product '{ExternalId}' - '{ProductName}'", productTemplateOdooModel.Id, product.Name);
    }

    [Time]
    private async Task UpdateFeatureImagesAsync(long externalId)
    {
        logger.LogInformation("Updating feature images of product '{ExternalId}'", externalId);

        var productRepository = unitOfWork.GetRepository<Product>();
        var product = await productRepository
            .SingleOrDefaultAsync(product => product.ExternalId == externalId);

        if (product is null)
        {
            logger.LogInformation("Update feature images of product '{ExternalId}' skipped. Product not found.", externalId);
            return;
        }

        var queryResult = await productTemplateRepository.Query().ById(externalId).Select(model => new
        {
            model.Id,
            model.Image128,
            model.Image256,
            model.Image512,
            model.Image1024,
            model.Image1920,
        }).FirstOrDefaultAsync();

        var productTemplateOdooModel = queryResult.Value;
        if (productTemplateOdooModel is null)
        {
            logger.LogInformation("Unable to retrieve feature images of product '{ExternalId}' skipped.", externalId);
            return;
        }

        var imageRepository = unitOfWork.GetRepository<Image>();
        logger.LogInformation("Detecting changes in feature images of product '{ExternalId}' - '{ProductName}'", externalId, product.Name);
        var count = 0;
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
                            "Adding new feature image of size '{ImageSize}' to product '{ExternalId}' - '{ProductName}'", imageSize, externalId, product.Name);

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
                        logger.LogInformation("Detected change in feature image of size '{ImageSize}' to product '{ExternalId}' - '{ProductName}'", imageSize, externalId, product.Name);

                        featuredImage.Content = content;
                        imageRepository.Update(featuredImage);
                        changeDetected = true;
                    }

                    if (changeDetected)
                    {
                        count++;

                        await imageRepository.SaveChangesAsync();

                        logger.LogInformation("Updated feature image of size '{ImageSize}' to product '{ExternalId}' - '{ProductName}'", imageSize, externalId, product.Name);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex,
                        "Error adding featured image of size '{ImageSize}' to product '{ExternalId}' - '{ProductName}'", imageSize, externalId, product.Name);
                }
            }
        }

        logger.LogInformation("Added or updated '{ImagesCount}' featured images to product '{ExternalId}' - '{ProductName}'", count, externalId, product.Name);
    }

    [Time]
    private async Task UpdateImagesAsync(long externalId)
    {
        logger.LogInformation("Updating images of product '{ExternalId}'", externalId);

        var productRepository = unitOfWork.GetRepository<Product>();
        var product = await productRepository
            .SingleOrDefaultAsync(product => product.ExternalId == externalId);

        if (product is null)
        {
            logger.LogInformation("Update images of product '{ExternalId}' skipped. Product not found.", externalId);
            return;
        }

        var queryResult = await productTemplateRepository.Query().ById(externalId).Select(model => new
        {
            model.Id,
            model.ProductTemplateImageIds,
        }).FirstOrDefaultAsync();

        var productTemplateOdooModel = queryResult?.Value;
        if (productTemplateOdooModel is null)
        {
            logger.LogInformation("Unable to retrieve images of product '{ExternalId}'.", externalId);
            return;
        }

        var productTemplateImageIds = productTemplateOdooModel.ProductTemplateImageIds;

        var imageRepository = unitOfWork.GetRepository<Image>();

        List<Image> images = (await imageRepository
            .FindAsync(SpecificationBuilder.Build<Image>(images =>
                images.Where(image => image.ProductId == product.Id && image.ExternalId != null)))).ToList();

        logger.LogInformation("Detecting changes in images of product '{ExternalId}' - '{ProductName}'", productTemplateOdooModel.Id, product.Name);

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
                logger.LogInformation("Detected change in images of product '{ExternalId}' - '{ProductName}'", productTemplateOdooModel.Id, product.Name);

                foreach (var imageSize in Enum.GetValues<StoneAssemblies.OdooBot.Entities.ImageSize>())
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
                                "Updated image of size '{ImageSize}' to product '{ExternalId}' - '{ProductName}'", imageSize, productTemplateOdooModel.Id, product.Name);
                        }
                        catch (Exception ex)
                        {
                            logger.LogWarning(ex,
                                "Error updating image of size '{ImageSize}' to product '{ExternalId}' - '{ProductName}'", imageSize, productTemplateOdooModel.Id, product.Name);
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

            var count = 0;
            await foreach (var productImageOdooModel in imageQuery.GetAsync())
            {
                foreach (var imageSize in Enum.GetValues<StoneAssemblies.OdooBot.Entities.ImageSize>())
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
                                "Adding new image of size '{ImageSize}' to product '{ExternalId}' - '{ProductName}'", imageSize, productTemplateOdooModel.Id, product.Name);

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

                            count++;

                            logger.LogInformation(
                                "Added new image of size '{ImageSize}' to product '{ExternalId}' - '{ProductName}'", imageSize, productTemplateOdooModel.Id, product.Name);
                        }
                        catch (Exception ex)
                        {
                            logger.LogWarning(ex,
                                "Error adding image of size '{ImageSize}' to product '{ExternalId}' - '{ProductName}'", imageSize, productTemplateOdooModel.Id, product.Name);
                        }
                    }
                }
            }

            logger.LogInformation("Added new '{ImagesCount}' images to product '{ExternalId}' - '{ProductName}'", count, productTemplateOdooModel.Id, product.Name);
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
            logger.LogInformation("Synchronizing new products of category '{CategoryName}'...", category.Name);

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
                    productTemplateOdooModel.DescriptionSale,
                    productTemplateOdooModel.QtyAvailable,
                    productTemplateOdooModel.VirtualAvailable,
                    productTemplateOdooModel.StandardPrice,
                    productTemplateOdooModel.UomName
                });

                await foreach (var productTemplateOdooModel in productTemplateQuery.GetAsync())
                {
                    var product = new Product
                    {
                        CategoryId = category.Id,
                        ExternalId = productTemplateOdooModel.Id,
                        InStockQuantity = productTemplateOdooModel.QtyAvailable.GetValueOrDefault(0.0d),
                        StandardPrice = productTemplateOdooModel.StandardPrice.GetValueOrDefault(0.0d),
                        QuantityUnit = productTemplateOdooModel.UomName,
                    };

                    product.IncomingQuantity = Math.Max(productTemplateOdooModel.VirtualAvailable.GetValueOrDefault(0.0d) - product.InStockQuantity, 0.0d);

                    var productPricelistItemResult = await productPricelistItemRepository.Query()
                                                   .Where(model => model.Active, OdooOperator.EqualsTo, true).Where(
                                                       model => model.ProductId,
                                                       OdooOperator.EqualsTo,
                                                       productTemplateOdooModel.Id).FirstOrDefaultAsync();

                    // TODO: Improve this later
                    if (productPricelistItemResult?.Value is not null)
                    {
                        var productPricelistItemOdooModel = productPricelistItemResult.Value;
                        product.Price = productPricelistItemOdooModel.FixedPrice.GetValueOrDefault(0.0d);
                    }
                    else
                    {
                        product.Price = product.StandardPrice;
                    }

                    var nameTranslationResult = await irTranslationRepository
                                                    .Query()
                                                    .Where(model => model.ResId, OdooOperator.EqualsTo, product.ExternalId)
                                                    .Where(model => model.Lang, OdooOperator.EqualsTo,
                                                        LanguageIrTranslationOdooEnum.SpanishEspaOl)
                                                    .Where(model => model.Name, OdooOperator.EqualsTo, "product.template,name")
                                                    .FirstOrDefaultAsync();

                    product.Name = (nameTranslationResult?.Value?.Value ?? productTemplateOdooModel.DisplayName)?.Trim() ?? string.Empty;

                    logger.LogInformation("Adding new product '{ProductName}' to category '{CategoryName}'", product.Name, category.Name);
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

                logger.LogInformation("Synchronized '{ProductCount}' new products of category '{CategoryName}'...", countResult.Value, category.Name);
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
            var category = await categoryRepository.SingleOrDefaultAsync(c => c.ExternalId == productCategoryOdooModel.Id);
            if (category is null)
            {
                logger.LogInformation("Adding new category '{CategoryName}'...", productCategoryOdooModel.Name);

                category = new Category
                {
                    Name = productCategoryOdooModel.DisplayName,
                    ExternalId = productCategoryOdooModel.Id
                };

                categoryRepository.Add(category);
            }
            else
            {
                logger.LogInformation("Updating category '{CategoryName}'...", productCategoryOdooModel.Name);

                category.Name = productCategoryOdooModel.DisplayName;
                categoryRepository.Update(category);
            }

            await categoryRepository.SaveChangesAsync();

            logger.LogInformation("Synchronized category '{CategoryName}'", productCategoryOdooModel.Name);
        }

        logger.LogInformation("Synchronized categories");
    }
}