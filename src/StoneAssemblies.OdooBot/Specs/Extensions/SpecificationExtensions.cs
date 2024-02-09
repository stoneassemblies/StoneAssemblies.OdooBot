using StoneAssemblies.EntityFrameworkCore.Specifications.Interfaces;
using StoneAssemblies.OdooBot.Handlers;

namespace StoneAssemblies.OdooBot.Specs.Extensions;

public static class SpecificationExtensions
{
    public static ISpecification<TEntity> Apply<TEntity>(this ISpecification<TEntity> spec, PaginationOptions paginationOptions)
    {
        return new PagedSpecificationWrapper<TEntity>(spec, paginationOptions);
    }

    public static ISpecification<TEntity, TOutput> Apply<TEntity, TOutput>(this ISpecification<TEntity, TOutput> spec, PaginationOptions paginationOptions)
    {
        return new PagedSpecificationWrapper<TEntity, TOutput>(spec, paginationOptions);
    }
}