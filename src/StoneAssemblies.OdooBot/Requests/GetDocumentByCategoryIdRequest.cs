using MediatR;
using MMLib.MediatR.Generators.Controllers;
using StoneAssemblies.OdooBot.DataTransferObjects;

namespace StoneAssemblies.OdooBot.Requests;

public record DownloadDocumentByCategoryIdRequest(List<Guid> Ids) : IRequest<FileResult>
{
}