﻿using System.Collections.Generic;

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
}
