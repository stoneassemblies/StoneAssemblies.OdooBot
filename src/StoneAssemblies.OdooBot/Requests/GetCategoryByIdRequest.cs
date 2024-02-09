using MediatR;
using Microsoft.FluentUI.AspNetCore.Components;
using MMLib.MediatR.Generators.Controllers;
using StoneAssemblies.OdooBot.DataTransferObjects;

namespace StoneAssemblies.OdooBot.Requests;

[HttpGet(Controller = "CatalogService", Name="GetCategoryById", From = From.Query)]
public record GetCategoryByIdRequest(Guid Id) : IRequest<CategoryDto>
{
}