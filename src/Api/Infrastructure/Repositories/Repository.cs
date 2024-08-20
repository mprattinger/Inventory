using System;
using Api.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Repositories;

public abstract class Repository<T> where T : Entity
{

    internal readonly InventoryDataContext DbContext;

    protected Repository(InventoryDataContext context)
    {
        DbContext = context;
    }

    public virtual void Add(T entity)
    {
        DbContext.Add(entity);
    }

    public virtual void Update(T entity)
    {
        DbContext.Update(entity);
    }

    public virtual Task<T?> GetById(Guid id)
    {
        return DbContext.Set<T>()
                .SingleOrDefaultAsync(e => e.Id == id);
    }

    public virtual Task<List<T>> GetAll()
    {
        return DbContext
                .Set<T>()
                .ToListAsync();
    }

    public virtual void Remove(T entity)
    {
        DbContext.Remove(entity);
    }
}
