using System.Collections.Generic;
using System.Linq.Expressions;

namespace TravelAssistantBot.Core;
public interface IRepository<TEntity>
    where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity);
    TEntity Update(TEntity entity);
    TEntity Delete(TEntity entity);
    Task<IReadOnlyList<TEntity>> AllAsync();
    IReadOnlyList<TEntity> Filter(Func<TEntity, bool> predicate);
    TEntity? GetById(int id);
    Task<int> CommitAsync();
    int Commit();
    IQueryable<TEntity> GetQueryable(params Expression<Func<TEntity, object>>[] includes);
}

