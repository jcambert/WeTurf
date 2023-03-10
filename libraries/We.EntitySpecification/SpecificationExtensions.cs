using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
namespace We.EntitySpecification;
public static class SpecificationExtensions
{
    public static IQueryable<TEntity> GetQuery<TEntity>(this IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        where TEntity :class,IEntity
    {
        var query = inputQuery;

        // modify the IQueryable using the specification's criteria expression
        if (specification.Criteria is not null)
        {
            query = query.Where(specification.Criteria);
        }

        // Includes all expression-based includes
        query = specification.Includes.Aggregate(query,(current, include) => current.Include(include));

        // Include any string-based include statements
        query = specification.IncludeStrings.Aggregate(query,(current, include) => current.Include(include));

        // Apply ordering if expressions are set
        if (specification.OrderByIf is not null)
        {
            query = query.OrderByIf<TEntity,IQueryable<TEntity>>(specification.OrderByIf.Condition, specification.OrderByIf.Sorting);
        }
        else if (specification.OrderBy is not null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending is not null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        if (specification.GroupBy is not null)
        {
            query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
        }

        // Apply paging if enabled
        if (specification.PagedBy is not null)
        {
            query = query.PageBy(specification.PagedBy.Skip, specification.PagedBy.Take);
        }
        

        
        return query;
    }
}
