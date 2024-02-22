using System.Globalization;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using PortaCapena.OdooJsonRpcClient;
using PortaCapena.OdooJsonRpcClient.Consts;
using PortaCapena.OdooJsonRpcClient.Converters;
using PortaCapena.OdooJsonRpcClient.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SkiaSharp;
using StoneAssemblies.OdooBot.Models;
using Image = QuestPDF.Infrastructure.Image;

namespace StoneAssemblies.OdooBot.Tests;

public class PoC
{
    private const string ImagePropertyFormat = "Image{0}";

    private static readonly string? ApiUrl = Environment.GetEnvironmentVariable("BOT_ODOO_APIURL");

    private static readonly string? Database = Environment.GetEnvironmentVariable("BOT_ODOO_DATABASE");

    private static readonly string? Password = Environment.GetEnvironmentVariable("BOT_ODOO_PASSWORD");

    private static readonly string? Username = Environment.GetEnvironmentVariable("BOT_ODOO_USERNAME");


    [Fact]
    public async Task Test1()
    {
        var config = new OdooConfig(ApiUrl, Database, Username, Password);
        var odooClient = new OdooClient(config);
        var versionResult = await odooClient.GetVersionAsync();

        var loginResult = await odooClient.LoginAsync();
        if (loginResult.Succeed)
        {
            var tableName = "product.pricelist.item";
            var modelResult = await odooClient.GetModelAsync(tableName);
            var model = OdooModelMapper.GetDotNetModel(tableName, modelResult.Value);
        }
    }

    private Image LoadImageWithTransparency(byte[] content, float transparency)
    {
        using var originalImage = SKImage.FromEncodedData(content);
        using var surface = SKSurface.Create(new SKImageInfo(originalImage.Width, originalImage.Height,
            SKColorType.Rgba8888, SKAlphaType.Premul));
        using var canvas = surface.Canvas;
        using var transparencyPaint = new SKPaint
        {
            ColorFilter = SKColorFilter.CreateBlendMode(SKColors.White.WithAlpha((byte) (transparency * 255)),
                SKBlendMode.DstIn)
        };
        canvas.DrawImage(originalImage, new SKPoint(0, 0), transparencyPaint);

        var encodedImage = surface.Snapshot().Encode(SKEncodedImageFormat.Png, 100).ToArray();
        return Image.FromBinaryData(encodedImage);
    }

    [Fact]
    public async Task DoSomething2()
    {
        var config = new OdooConfig(ApiUrl, Database, Username, Password);
        var listOdooRepository = new OdooRepository<ProductPricelistItemOdooModel>(config);
        var listAsync = await listOdooRepository.Query().ToListAsync();

        if (listAsync?.Value is null)
        {
            return;
        }

        var productPricelistOdooModels = listAsync.Value;
    }

    [Fact]
    public async Task DoSomethingAsync()
    {
        QuestPDF.Settings.License = LicenseType.Community;
        // QuestPDF.Settings.EnableDebugging = true;

        var config = new OdooConfig(ApiUrl, Database, Username, Password);

        var categoryRepository = new OdooRepository<ProductCategoryOdooModel>(config);
        var productCategoriesResult = await categoryRepository.Query()
            .Where(c => c.ParentId, OdooOperator.EqualsTo, null)
            .Where(c => c.Id, OdooOperator.GreaterThan, 1)
            .ToListAsync();

        if (productCategoriesResult?.Value is null)
        {
            return;
        }

        var productTemplateRepository = new OdooRepository<ProductTemplateOdooModel>(config);
        var productImageRepository = new OdooRepository<ProductImageOdooModel>(config);
        var irTranslationRepository = new OdooRepository<IrTranslationOdooModel>(config);
        List<Category>? categories = null;
        var cacheJson = "local-cache.json";
        if (File.Exists(cacheJson))
        {
            try
            {
                categories = JsonConvert.DeserializeObject<List<Category>>(await File.ReadAllTextAsync(cacheJson));
            }
            catch (Exception)
            {
                // Ignore.
            }
        }

        var imageResolutions = new List<string>
        {
            "128",
            "256",
            "512",
            "1024",
            "1920"
        };

        categories ??= new List<Category>();

        var odooProductCategories = productCategoriesResult.Value;
        for (var index = categories.Count - 1; index >= 0; index--)
        {
            var category = categories[index];
            var productCategoryOdooModel = odooProductCategories.FirstOrDefault(model => model.Id == category.Id);
            if (productCategoryOdooModel is null)
            {
                categories.RemoveAt(index);
            }
        }

        foreach (var odooProductCategory in odooProductCategories.Where(model => model.Name.Contains("Impresoras - Toners - Accesorios")))
        {
            var category = categories.FirstOrDefault(category => category.Id == odooProductCategory.Id);
            if (category is null)
            {
                category = new Category
                {
                    Id = odooProductCategory.Id,
                    Products = new List<Product>(),
                    Name = odooProductCategory.DisplayName
                };

                categories.Add(category);
            }

            var productIds = category.Products.Select(product => product.Id).ToArray();
            var countResult = await productTemplateRepository.Query()
                .Where(p => p.CategId, OdooOperator.EqualsTo, category.Id)
                .Where(p => p.Id, OdooOperator.NotIn, productIds).CountAsync();

            if (countResult?.Value > 0)
            {
                var count = countResult.Value;
                var offset = 0;
                const int limit = 5;

                category.Products.Clear();
                do
                {
                    var productTemplatesResult = await productTemplateRepository.Query()
                        .Where(p => p.CategId, OdooOperator.EqualsTo, category.Id)
                        .Skip(offset).Take(limit)
                        .ToListAsync();

                    if (productTemplatesResult?.Value is not null)
                    {
                        foreach (var productTemplateOdooModel in productTemplatesResult.Value)
                        {
                            var uomName = productTemplateOdooModel.UomName;

                            var product = new Product
                                              {
                                                  Id = productTemplateOdooModel.Id,
                                                  Images = new Dictionary<string, List<string>>()
                                              };

                            foreach (var imageKey in imageResolutions)
                            {
                                var value = productTemplateOdooModel.GetType().GetProperty(
                                    string.Format(ImagePropertyFormat, imageKey),
                                    BindingFlags.Public | BindingFlags.Instance)!.GetValue(productTemplateOdooModel);
                                var stringValue = Convert.ToString(value);
                                product.Images[imageKey] = new List<string>();
                                if (!string.IsNullOrWhiteSpace(stringValue))
                                {
                                    product.Images[imageKey].Add(stringValue);
                                }
                            }

                            var imagesResult = await productImageRepository.Query()
                                .Where(y => y.Id, OdooOperator.In, productTemplateOdooModel.ProductTemplateImageIds)
                                .ToListAsync();

                            if (imagesResult?.Value is not null)
                            {
                                foreach (var imageOdooModel in imagesResult.Value)
                                {
                                    foreach (var imageResolution in imageResolutions)
                                    {
                                        var value = imageOdooModel.GetType()
                                            .GetProperty(string.Format(ImagePropertyFormat, imageResolution),
                                                BindingFlags.Public | BindingFlags.Instance)?.GetValue(imageOdooModel);
                                        var stringValue = Convert.ToString(value);
                                        if (!string.IsNullOrWhiteSpace(stringValue))
                                        {
                                            product.Images[imageResolution].Add(stringValue);
                                        }
                                    }
                                }
                            }

                            var nameTranslationResult = await irTranslationRepository
                                .Query()
                                .Where(model => model.ResId, OdooOperator.EqualsTo, product.Id)
                                .Where(model => model.Lang, OdooOperator.EqualsTo,
                                    LanguageIrTranslationOdooEnum.SpanishEspaOl)
                                .Where(model => model.Name, OdooOperator.EqualsTo, "product.template,name")
                                .FirstOrDefaultAsync();

                            product.Name = nameTranslationResult?.Value?.Value ?? productTemplateOdooModel.DisplayName;

                            var descriptionTranslationResult = await irTranslationRepository
                                .Query()
                                .Where(model => model.ResId, OdooOperator.EqualsTo, product.Id)
                                .Where(model => model.Lang, OdooOperator.EqualsTo,
                                    LanguageIrTranslationOdooEnum.SpanishEspaOl)
                                .Where(model => model.Name, OdooOperator.EqualsTo, "product.template,description_sale")
                                .FirstOrDefaultAsync();

                            product.Description = descriptionTranslationResult?.Value?.Value ??
                                                  productTemplateOdooModel.DescriptionSale;
                            category.Products.Add(product);
                        }

                        offset += Math.Min(limit, productTemplatesResult.Value.Length);
                    }
                } while (offset < count);

                await File.WriteAllTextAsync(cacheJson,
                    JsonConvert.SerializeObject(categories, Formatting.Indented), Encoding.UTF8);
            }
        }

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(20));

                page.Header()
                    .AlignCenter()
                    .PaddingTop(100)
                    .Text("Catálogo ConsuPC")
                    .Bold()
                    .FontSize(36)
                    .FontColor(Colors.Blue.Darken4);

                var banner = typeof(PoC).Assembly.GetManifestResourceStream(
                    "StoneAssemblies.OdooBot.Tests.Resources.banner.png");

                page.Foreground()
                    .AlignRight()
                    .AlignMiddle()
                    .Image(banner!);

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Element().Text(DateTime.Now.ToString("D", CultureInfo.CreateSpecificCulture("es-ES")))
                            .FontSize(10);
                    });
            });

            foreach (var category in categories)
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                        .AlignCenter()
                        .Text(category.Name)
                        .Bold()
                        .FontSize(36)
                        .FontColor(Colors.Blue.Darken4);

                    //page.Footer()
                    //    .AlignCenter()
                    //    .Text(x => { x.CurrentPageNumber().FontSize(10); });
                });


                foreach (var product in category.Products)
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(20));

                        page.Header()
                            .AlignRight()
                            .Text(category.Name)
                            .Bold()
                            .FontSize(18)
                            .FontColor(Colors.Blue.Darken4);

                        page.Content()
                            .PaddingVertical(1, Unit.Centimetre)
                            .Column(x =>
                            {
                                x.Spacing(20);
                                x.Item()
                                    .Text(product.Name.Trim())
                                    .SemiBold()
                                    .FontSize(25);

                                if (product.Images.Count > 0)
                                {
                                    x.Item().Row(r =>
                                    {
                                        foreach (var image in product.Images["512"].Take(4))
                                        {
                                            r.Spacing(10);
                                            r.ConstantItem(100).Image(Convert.FromBase64String(image)).FitArea();
                                        }
                                    });
                                }

                                var productDescription = product.Description?.Trim();
                                if (!string.IsNullOrWhiteSpace(productDescription))
                                {
                                    x.Item().Text(productDescription).FontSize(12);
                                }
                                else
                                {
                                    x.Item().Text("[Sin descripción]").FontSize(12);
                                }
                            });

                        var productImages = product.Images["1024"];
                        if (productImages.Count > 0)
                        {
                            page.Foreground()
                                .AlignRight()
                                .PaddingTop(300)
                                .Row(r =>
                                {
                                    r.ConstantItem(200);
                                    var image = LoadImageWithTransparency(
                                        Convert.FromBase64String(productImages.First()), 0.20f);
                                    r.RelativeItem().Image(image).FitWidth();
                                });
                        }

                        page
                            .Footer()
                            .AlignCenter()
                            .Text(x => { x.CurrentPageNumber().FontSize(10); });
                    });
                }
            }
        });

        var settings = document.GetSettings();
        settings.ImageCompressionQuality = ImageCompressionQuality.VeryLow;

        document.GeneratePdf("odoo.pdf");
    }
}

public class Category
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<Product> Products { get; set; }
}

public class Product
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Dictionary<string, List<string>> Images { get; set; }
}