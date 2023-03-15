using System.Linq.Expressions;
namespace We.EntitySpecification;


/// <summary>
/// Base Class of specification
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Specification<T> : ISpecification<T>
{
    protected Specification(Expression<Func<T, bool>> criteria)=>Criteria = criteria;
    
    protected Specification()
    {

    }

    /// <summary>
    /// Get Criteria
    /// </summary>
    public Expression<Func<T, bool>> Criteria { get; }

    /// <summary>
    /// Get includes
    /// </summary>
    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

    /// <summary>
    /// Get Includes as string
    /// </summary>
    public List<string> IncludeStrings { get; } = new List<string>();

    /// <summary>
    /// Get Orderby ascendind Criteria
    /// </summary>
    public Expression<Func<T, object>> OrderBy { get; private set; }
    /// <summary>
    /// Get OrderBy Descending Criteria
    /// </summary>
    public Expression<Func<T, object>> OrderByDescending { get; private set; }
    /// <summary>
    /// Get GroupBy Criteria
    /// </summary>
    public Expression<Func<T, object>> GroupBy { get; private set; }

    /// <summary>
    /// Get Take count of query
    /// </summary>
    public int Take { get; private set; }

    /// <summary>
    /// Get Skip count of query
    /// </summary>
    public int Skip { get; private set; }
    /// <summary>
    /// Get Paging enabled
    /// </summary>
    public bool IsPagingEnabled { get; private set; } = false;
    /// <summary>
    /// Get OrderByIf Criteria
    /// </summary>
    public OrderByIf OrderByIf { get; private set; }
    /// <summary>
    /// Get PagedBy Criteria
    /// </summary>
    public PagedBy PagedBy { get; private set; }

    /// <summary>
    /// Add Include Criteria
    /// </summary>
    /// <param name="includeExpression"></param>
    protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
    =>  Includes.Add(includeExpression);
    
    /// <summary>
    /// Add Include Criteria
    /// </summary>
    /// <param name="includeString"></param>
    protected virtual void AddInclude(string includeString)
    =>IncludeStrings.Add(includeString);
    
    /// <summary>
    /// Applying Paging
    /// </summary>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    protected virtual void ApplyPaging(int skip, int take)
    =>PagedBy = new PagedBy(skip, take);
    
    /// <summary>
    /// Applying OrderBy Asc
    /// </summary>
    /// <param name="orderByExpression"></param>
    protected virtual void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
    => OrderBy = orderByExpression;
    
    /// <summary>
    /// Applying OrderBy Desc
    /// </summary>
    /// <param name="orderByDescendingExpression"></param>
    protected virtual void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
    => OrderByDescending = orderByDescendingExpression;
    
    /// <summary>
    /// Applying GroupBy
    /// </summary>
    /// <param name="groupByExpression"></param>
    protected virtual void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
    => GroupBy = groupByExpression;

}
