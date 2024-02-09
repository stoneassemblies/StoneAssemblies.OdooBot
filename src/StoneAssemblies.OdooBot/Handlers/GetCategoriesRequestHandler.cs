using Mapster;
using MediatR;
using StoneAssemblies.EntityFrameworkCore.Services.Interfaces;
using StoneAssemblies.OdooBot.DataTransferObjects;
using StoneAssemblies.OdooBot.Entities;
using StoneAssemblies.OdooBot.Requests;
using StoneAssemblies.OdooBot.Services;

namespace StoneAssemblies.OdooBot.Handlers;

public class GetCategoriesRequestHandler(IRepository<Category, ApplicationDbContext> categoriesRepository)
    : IRequestHandler<GetCategoriesRequest, List<CategoryDto>>
{
    public async Task<List<CategoryDto>> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
    {
        var findAsync = await categoriesRepository.FindAsync(SpecificationBuilder.Build<Category>(categories => categories.Where(category => category.Products.Count > 0)));
        var categoryDtos = findAsync.Adapt<List<CategoryDto>>();
        return categoryDtos;
    }
}