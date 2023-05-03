using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace We.EntitySpecification;

public record OrderByIf(bool Condition, string Sorting);

public record PagedBy(int Skip, int Take);

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
    OrderByIf OrderByIf { get; }
    Expression<Func<T, object>> OrderBy { get; }
    Expression<Func<T, object>> OrderByDescending { get; }
    Expression<Func<T, object>> GroupBy { get; }
    PagedBy PagedBy { get; }
}
