using StoneAssemblies.EntityFrameworkCore.Specifications.Interfaces;

namespace StoneAssemblies.OdooBot.Specs;

/// <summary>
/// The specification wrapper.
/// </summary>
/// <typeparam name="TEntity">
/// The entity type.
/// </typeparam>
public class PagedSpecificationWrapper<TEntity> : ISpecification<TEntity>
{
    private readonly ISpecification<TEntity> specification;

    private readonly PaginationOptions options;

    public PagedSpecificationWrapper(ISpecification<TEntity> specification, PaginationOptions options)
    {
        ArgumentNullException.ThrowIfNull(specification);
        ArgumentNullException.ThrowIfNull(options);

        this.specification = specification;
        this.options = options;
    }

    /// <inheritdoc />
    public Func<IQueryable<TEntity>, IQueryable<TEntity>> Build()
    {
        return entities => specification.Build()(entities).Skip(options.Skip).Take(options.Take);
    }
}


public class PagedSpecificationWrapper<TEntity, TOutput> : ISpecification<TEntity, TOutput>
{
    private readonly ISpecification<TEntity, TOutput> specification;

    private readonly PaginationOptions options;

    public PagedSpecificationWrapper(ISpecification<TEntity, TOutput> specification, PaginationOptions options)
    {
        ArgumentNullException.ThrowIfNull(specification);
        ArgumentNullException.ThrowIfNull(options);

        this.specification = specification;
        this.options = options;
    }

    public Func<IQueryable<TEntity>, IQueryable<TOutput>> Build()
    {
        return entities => specification.Build()(entities).Skip(options.Skip).Take(options.Take);
    }
}