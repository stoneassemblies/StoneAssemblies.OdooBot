namespace StoneAssemblies.OdooBot.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

using StoneAssemblies.EntityFrameworkCore.Services.Interfaces;
using StoneAssemblies.OdooBot.Entities;
using StoneAssemblies.OdooBot.Services;

public class ProductsController : ODataController
{
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<Product>>> Get(
        [FromServices] IRepository<Product, ApplicationDbContext> repository)
    {
        var products = await repository.FindAsync(SpecificationBuilder.Build<Product>((IQueryable<Product> products) => products));
        return this.Ok(products);
    }

    [EnableQuery]
    public async Task<ActionResult<Product>> Get([FromRoute] Guid key, [FromServices] IRepository<Product, ApplicationDbContext> repository)
    {
        var product = await repository.SingleOrDefaultAsync(
                          SpecificationBuilder.Build<Product>((IQueryable<Product> categories) => categories.Where((Product product) => product.Id == key)));
        if (product is null)
        {
            return this.NotFound();
        }

        return this.Ok(product);
    }
}