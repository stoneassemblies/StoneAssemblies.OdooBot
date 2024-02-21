using System.Globalization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SkiaSharp;
using StoneAssemblies.EntityFrameworkCore.Services.Interfaces;
using StoneAssemblies.OdooBot.DataTransferObjects;
using StoneAssemblies.OdooBot.Entities;
using StoneAssemblies.OdooBot.Requests;
using StoneAssemblies.OdooBot.Services;
using Image = QuestPDF.Infrastructure.Image;
using ImageSize = StoneAssemblies.OdooBot.Entities.ImageSize;

namespace StoneAssemblies.OdooBot.Handlers;

using Unit = QuestPDF.Infrastructure.Unit;

public class DownloadDocumentByCategoryIdRequestHandler
(IRepository<Category, ApplicationDbContext> categoryRepository,
    IRepository<Category, ApplicationDbContext> productRepository,
    IRepository<Category, ApplicationDbContext> imageRepository) : IRequestHandler<DownloadDocumentByCategoryIdRequest,
    FileResult>
{
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

    public async Task<FileResult> Handle(DownloadDocumentByCategoryIdRequest request,
        CancellationToken cancellationToken)
    {
        var specification = SpecificationBuilder.Build<Category>(categories =>
        {
            categories = categories
                .Include(category => category.Products)
                .ThenInclude(product => product.Images.Where(image => image.Size == ImageSize.Medium))
                .Where(category => category.Products.Count > 0);

            if (request.Ids.Count > 0)
            {
                categories = categories.Where(category => request.Ids.Contains(category.Id));
            }

            return categories;
        });
        var categories = await categoryRepository.FindAsync(specification);

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
                    .Text("Catálogo - ConsuPC")
                    .Bold()
                    .FontSize(36)
                    .FontColor(Colors.Blue.Darken4);

                var banner = typeof(DownloadDocumentByCategoryIdRequestHandler).Assembly.GetManifestResourceStream(
                    "StoneAssemblies.OdooBot.Resources.banner.png");

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
                                    .SemiBold().FontSize(25);

                                if (product.Images.Count > 0)
                                {
                                    x.Item().Row(r =>
                                    {
                                        foreach (var image in product.Images
                                                     .Where(image => image.Size == ImageSize.Medium).Take(4))
                                        {
                                            r.Spacing(10);
                                            r.ConstantItem(100).Height(100).Image(image.Content).FitArea();
                                        }
                                    });
                                }

                                x.Item().Row(r =>
                                    {
                                        r.AutoItem().Text("Disponibilidad: ").SemiBold().FontSize(12);
                                        r.AutoItem().Text($"{product.InStockQuantity:0.##} {product.QuantityUnit}").NormalWeight().FontSize(12); ;
                                    });

                                x.Item().Text("Descripción:").SemiBold().SemiBold().FontSize(12);
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

                        var productImages = product.Images.Where(image => image.Size == ImageSize.Medium)
                            .Select(image => image.Content).ToList();
                        if (productImages.Count > 0)
                        {
                            page.Foreground()
                                .AlignRight()
                                .PaddingTop(350)
                                .Row(r =>
                                {
                                    r.ConstantItem(200);
                                    var image = LoadImageWithTransparency(productImages.First(), 0.20f);
                                    r.RelativeItem()
                                        .MaxHeight(300)
                                        .MaxWidth(300)
                                        .Image(image)
                                        .FitArea();
                                });
                        }

                        page
                            .Footer()
                            .AlignCenter()
                            .Text(x =>
                            {
                                x.CurrentPageNumber().FontSize(10)
                                    ;
                            });
                    });
                }
            }
        });

        var settings = document.GetSettings();
        settings.ImageCompressionQuality = ImageCompressionQuality.VeryLow;

        return new FileResult
        {
            FileName = "catalog.pdf",
            Content = document.GeneratePdf()
        };
    }
}