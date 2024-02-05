using PortaCapena.OdooJsonRpcClient.Models;
using PortaCapena.OdooJsonRpcClient.Request;

public static class OdooQueryBuilderExtensions
{
    public static async IAsyncEnumerable<T> GetAsync<T>(this OdooQueryBuilder<T> @this) where T : IOdooModel, new()
    {
        var limit = 5;
        var offset = 0;

        var odooResult = await @this.Skip(offset).Take(limit).ToListAsync();
        while(odooResult?.Value?.Length > 0)
        {
            foreach (var odooModel in odooResult.Value)
            {
                yield return odooModel;
            }

            offset += limit;
            odooResult = await @this.Skip(offset).Take(limit).ToListAsync();
        }
    }
}