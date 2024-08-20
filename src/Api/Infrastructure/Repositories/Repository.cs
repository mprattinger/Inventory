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

    public void Add(T entity)
    {
        DbContext.Add(entity);
    }

    public void Update(T entity)
    {
        DbContext.Update(entity);
    }

    public Task<T?> GetById(Guid id)
    {
        return DbContext.Set<T>()
                .SingleOrDefaultAsync(e => e.Id == id);
    }

    public Task<List<T>> GetAll()
    {
        return DbContext
                .Set<T>()
                .ToListAsync();
    }
}
