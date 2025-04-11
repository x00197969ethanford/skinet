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
}

public class BaseSpecification<T, TResult>(Expression<Func<T, bool>>? criteria)
    : BaseSpecification<T>(criteria), ISpecification<T, TResult>
{
    protected BaseSpecification() : this(null) { }
    public Expression<Func<T, TResult>>? select {get; private set;}
    protected void AddSelect(Expression<Func<T,TResult>> selectExpression)
    {
        select = selectExpression;
    }
}