using Mapster;
using MediatR;
using StoneAssemblies.EntityFrameworkCore.Services.Interfaces;
using StoneAssemblies.OdooBot.DataTransferObjects;
using StoneAssemblies.OdooBot.Entities;
using StoneAssemblies.OdooBot.Requests;
using StoneAssemblies.OdooBot.Services;

namespace StoneAssemblies.OdooBot.Handlers;

public class GetCategoryByIdRequestHandler(IRepository<Category, ApplicationDbContext> categoriesRepository)
    : IRequestHandler<GetCategoryByIdRequest, CategoryDto>
{
    public async Task<CategoryDto> Handle(GetCategoryByIdRequest request, CancellationToken cancellationToken)
    {
        var category = await categoriesRepository.SingleOrDefaultAsync(category => category.Id == request.Id);
        return category.Adapt<CategoryDto>();
    }
}