using System;
using Api.Infrastructure.Data;

namespace Api.Infrastructure.Repositories;

public interface IRepository<T> where T : Entity
{
    void Add(T entity);

    void Update(T entity);

    Task<T?> GetById(Guid id);

    Task<List<T>> GetAll();

    void Remove(T entity);
}
