using MediatR;
using MMLib.MediatR.Generators.Controllers;
using StoneAssemblies.OdooBot.DataTransferObjects;

namespace StoneAssemblies.OdooBot.Requests;

/// <summary>
/// Get categories request.
/// </summary>
[HttpGet("all", Controller = "CatalogService", Name= "GetCategories", From = From.Ignore)]
public record GetCategoriesRequest : IRequest<List<CategoryDto>>
{
}