using System;
using System.Collections.Concurrent;
using Core.Entities;
using Core.interfaces;

namespace Infrastructure.Data;

public class UnitOfWork(StoreContext context) : IUnitOfWork
{

    private readonly ConcurrentDictionary<string, object> _repositories = new();
    public async Task<bool> Complete()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        context.Dispose();

    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        var type = typeof(TEntity).Name;
        return (IGenericRepository<TEntity>)_repositories.GetOrAdd(type, t =>
        {
            var RepositoryType = typeof(GenericRepository<>).MakeGenericType(typeof(TEntity));
            return Activator.CreateInstance(RepositoryType, context)
                ?? throw new InvalidOperationException(
                    $"Could not create repository instance for {t}");
        });
    }
}
