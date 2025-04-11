using System;
using System.Linq.Expressions;

namespace Core.interfaces;

public interface ISpecification<T>
{

    Expression<Func<T, bool>>? Criteria { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDesending { get; }
    bool IsDistinct {get;}

}

public interface ISpecification<T, TResult> : ISpecification<T>
{
    Expression<Func<T, TResult>>? select { get; }
}