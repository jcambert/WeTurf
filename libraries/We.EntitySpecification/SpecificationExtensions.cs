using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace We.EntitySpecification;

public static class SpecificationExtensions
{
    /// <summary>
    /// Get Query with specification applied
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="inputQuery"></param>
    /// <param name="specification"></param>
    /// <returns></returns>
    public static IQueryable<TEntity> GetQuery<TEntity>(
        this IQueryable<TEntity> inputQuery,
        ISpecification<TEntity> specification
    ) where TEntity : class, IEntity
    {
        var query = inputQuery;

        if (specification.Distinct)
            query = query.Distinct();

        // modify the IQueryable using the specification's criteria expression
        if (specification.Criteria is not null)
        {
            query = query.Where(specification.Criteria);
        }

        // Includes all expression-based includes
        query = specification.Includes.Aggregate(
            query,
            (current, include) => current.Include(include)
        );

        // Include any string-based include statements
        query = specification.IncludeStrings.Aggregate(
            query,
            (current, include) => current.Include(include)
        );

        // Apply ordering if expressions are set
        if (specification.OrderByIf is not null)
        {
            query = query.OrderByIf<TEntity, IQueryable<TEntity>>(
                specification.OrderByIf.Condition,
                specification.OrderByIf.Sorting
            );
        }
        /*else if (specification.OrderBy is not null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending is not null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }*/
        else
        {
            bool ordered = false;
            foreach (var order in specification.Orders)
            {
                if (order.Order == Order.Asc)
                {
                    if (!ordered)
                    {
                        query = query.OrderBy(order.Expression);
                        ordered = true;
                    }
                    else
                        query = ((IOrderedQueryable<TEntity>)query).ThenBy(order.Expression);
                }
                else if (order.Order == Order.Desc)
                {
                    if (!ordered)
                    {
                        query = query.OrderByDescending(order.Expression);
                        ordered = true;
                    }
                    else
                        query = ((IOrderedQueryable<TEntity>)query).ThenByDescending(
                            order.Expression
                        );
                }
            }
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
