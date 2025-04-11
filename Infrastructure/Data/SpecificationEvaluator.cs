using System;
using Core.Entities;
using Core.interfaces;

namespace Infrastructure.Data;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
    {
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }
        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        if (spec.OrderByDesending != null)
        {
            query = query.OrderByDescending(spec.OrderByDesending);
        }

        if (spec.IsDistinct)
        {
            query = query.Distinct();
        }

        return query;
    }
        public static IQueryable<TResult> GetQuery<TSpec, TResult>(IQueryable<T> query, ISpecification<T, TResult> spec)
    {
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }
        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        if (spec.OrderByDesending != null)
        {
            query = query.OrderByDescending(spec.OrderByDesending);
        }

        var selectQuery = query as IQueryable<TResult>;
        if (spec.select != null)
        {
            selectQuery = query.Select(spec.select);
        }
        if (spec.IsDistinct)
        {
            selectQuery = selectQuery?.Distinct();
        }
        return selectQuery ?? query.Cast<TResult>();
    }
}

