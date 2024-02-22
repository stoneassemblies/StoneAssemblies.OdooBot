namespace StoneAssemblies.OdooBot.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

using StoneAssemblies.EntityFrameworkCore.Services.Interfaces;
using StoneAssemblies.OdooBot.Entities;
using StoneAssemblies.OdooBot.Services;

public class ProductsController : ODataController
{
    [EnableQuery(PageSize = 10)]
    public ActionResult<IEnumerable<Product>> Get([FromServices] IRepository<Product, ApplicationDbContext> repository)
    {
        return this.Ok(repository.All());
    }

    [EnableQuery]
    public async Task<ActionResult<Product>> Get([FromRoute] Guid key, [FromServices] IRepository<Product, ApplicationDbContext> repository)
    {
        var product = await repository.SingleOrDefaultAsync(SpecificationBuilder.Build<Product>(categories => categories.Where(product => product.Id == key)));
        if (product is null)
        {
            return this.NotFound();
        }

        return this.Ok(product);
    }

    [HttpGet("odata/Products({key})/Category")]
    [EnableQuery]
    public async Task<ActionResult<Category>> GetProductCategory([FromRoute] Guid key, [FromServices] IRepository<Product, ApplicationDbContext> repository)
    {
        var category = await repository.SingleOrDefaultAsync(
            SpecificationBuilder.Build<Product, Category>(
                products => products.Where(product => product.Id == key).Select(product => product.Category)));
        return this.Ok(category);
    }

}