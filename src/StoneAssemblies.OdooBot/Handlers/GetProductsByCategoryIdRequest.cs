using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StoneAssemblies.EntityFrameworkCore.Services.Interfaces;
using StoneAssemblies.OdooBot.DataTransferObjects;
using StoneAssemblies.OdooBot.Entities;
using StoneAssemblies.OdooBot.Requests;
using StoneAssemblies.OdooBot.Services;
using StoneAssemblies.OdooBot.Specs;
using StoneAssemblies.OdooBot.Specs.Extensions;

namespace StoneAssemblies.OdooBot.Handlers;


public class GetProductsByCategoryIdRequestHandler(IRepository<Product, ApplicationDbContext> productsRepository)
    : IRequestHandler<GetProductsByCategoryIdRequest, PagedResult<ProductDetails>>
{
    public async Task<PagedResult<ProductDetails>> Handle(GetProductsByCategoryIdRequest request, CancellationToken cancellationToken)
    {
        var specification = SpecificationBuilder.Build<Product, ProductDetails>(products => products
            .Where(product => product.CategoryId == request.Id).
            Include(product => product.Images).
            Select(product => new ProductDetails
            {
                Id = product.Id,
                ExternalId = product.ExternalId,
                Name = product.Name,
                Description = product.Description,
                FeatureImages = product.Images.Where(image => image.IsFeatured && image.Size == ImageSize.Small)
                    .Select(image => new Image()
                {
                    Id = image.Id,
                    Content = image.Content,
                    Size = image.Size,
                }),
                InStockQuantity = product.InStockQuantity,
                IncomingQuantity = product.IncomingQuantity,
                AggregateQuantity = product.AggregateQuantity,
                QuantityUnit = product.QuantityUnit,
                StandardPrice = product.StandardPrice,
                IsPublished = product.IsPublished,
            })
        );

        var count = await productsRepository.CountAsync(specification);
        if (count == 0)
        {
            return PagedResult<ProductDetails>.Empty;
        }

        try
        {
            var products = await productsRepository.FindAsync(specification.Apply(new PaginationOptions(request.Skip, request.Take)));
            return new PagedResult<ProductDetails>()
            {
                Items = products.Adapt<List<ProductDetails>>(),
                Count = count,
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}