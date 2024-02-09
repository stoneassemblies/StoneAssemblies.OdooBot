using MediatR;
using MMLib.MediatR.Generators.Controllers;
using StoneAssemblies.OdooBot.DataTransferObjects;

namespace StoneAssemblies.OdooBot.Requests;

[HttpGet("products", Controller = "CatalogService", Name = "GetProductsByCategoryId", From = From.Query)]
public record GetProductsByCategoryIdRequest(Guid Id, int Skip, int Take) : IRequest<PagedResult<ProductDetails>>
{
}