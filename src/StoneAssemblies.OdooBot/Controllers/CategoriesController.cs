namespace StoneAssemblies.OdooBot.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

using StoneAssemblies.EntityFrameworkCore.Services.Interfaces;
using StoneAssemblies.OdooBot.Entities;
using StoneAssemblies.OdooBot.Services;

public class CategoriesController : ODataController
{
    [EnableQuery(PageSize = 10)]
    public async Task<ActionResult<IEnumerable<Category>>> Get(
        [FromServices] IRepository<Category, ApplicationDbContext> repository)
    {
        return this.Ok(repository.All().Include(category => category.Products));
    }

    [EnableQuery]
    public async Task<ActionResult<Category>> Get(
        [FromRoute] Guid key, [FromServices] IRepository<Category, ApplicationDbContext> repository)
    {
        var category = await repository.FindAsync(
                           SpecificationBuilder.Build<Category>(
                               categories =>
                                   categories.Where(category => category.Id == key)
                                       .Include(category => category.Products)));
        if (category is null)
        {
            return this.NotFound();
        }

        return this.Ok(category);
    }

    [HttpGet("odata/Categories({key})/Products")]
    [HttpGet("odata/Categories/{key}/Products")]
    [EnableQuery(PageSize = 10)]
    public ActionResult<IEnumerable<Product>> GetProductsOfCategory([FromRoute] Guid key, [FromServices] IRepository<Product, ApplicationDbContext> repository)
    {
        return this.Ok(repository.All().Where(product => product.CategoryId == key));
    }
}