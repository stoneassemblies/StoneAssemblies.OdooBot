using MediatR;
using Microsoft.AspNetCore.Mvc;
using MMLib.MediatR.Generators.Controllers;

namespace StoneAssemblies.OdooBot.Requests;

[MMLib.MediatR.Generators.Controllers.HttpGet("images", Controller = "CatalogService", Name = "GetImageById", From = From.Query)]
public record GetImageByIdRequest(Guid Id) : IRequest<FileContentResult>
{
}