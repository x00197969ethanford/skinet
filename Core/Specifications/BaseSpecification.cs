using System;
using System.Linq.Expressions;
using Core.interfaces;

namespace Core.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
{
    protected BaseSpecification() : this(null) { }
    public Expression<Func<T, bool>>? Criteria => criteria;

    public Expression<Func<T, object>>? OrderBy {get; private set; }

    public Expression<Func<T, object>>? OrderByDesending { get; private set; }

    public bool IsDistinct {get; private set;}

    public int Take { get; private set; }

    public int Skip { get; private set; }

    public bool IsPagingEnabled { get; private set; }

    public List<Expression<Func<T, object>>> Includes { get; } = [];

    public List<string> IncludeStrings { get; } = [];

    public IQueryable<T> ApplyCriteria(IQueryable<T> query)
    {
        if (Criteria != null)
        {
            query = query.Where(Criteria);
        }
        return query;
    }

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
    protected void AddInclude(string IncludeString)
    {
        IncludeStrings.Add(IncludeString);
    }
    protected void AddOrderby(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }
    protected void AddOrderbyDesending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDesending = orderByDescExpression;
    }

    protected void ApplyDistinct()
    {
        IsDistinct = true;
    }

    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}

public class BaseSpecification<T, TResult>(Expression<Func<T, bool>>? criteria)
    : BaseSpecification<T>(criteria), ISpecification<T, TResult>
{
    protected BaseSpecification() : this(null) { }
    public Expression<Func<T, TResult>>? Select {get; private set;}
    protected void AddSelect(Expression<Func<T,TResult>> selectExpression)
    {
        Select = selectExpression;
    }
}