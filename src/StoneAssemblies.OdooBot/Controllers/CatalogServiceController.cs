

using Microsoft.AspNetCore.Mvc;
using FileResult = StoneAssemblies.OdooBot.DataTransferObjects.FileResult;

namespace StoneAssemblies.OdooBot.Controllers
{
    public partial class CatalogServiceController
    {
        [HttpPost("")]
        public async Task<FileResult> DownloadDocumentByCategoryIds([FromBody] Requests.DownloadDocumentByCategoryIdRequest command, CancellationToken cancellationToken)
        {
            return await this._mediator.Send(command);
        }
    }
}
