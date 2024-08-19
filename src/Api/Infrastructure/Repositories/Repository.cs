using System;
using Api.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Api.Infrastructure.Repositories;

public abstract class Repository<T> where T:Entity {
    public void Add(T entity) {

    }

    public void Update(T entity) {

    }

    public Task<T?> GetById(Guid id) {
        return Task.FromResult(default(T));
    }

    public Task<List<T>> GetAll() {
        return Task.FromResult(new List<T>());
    }
}
