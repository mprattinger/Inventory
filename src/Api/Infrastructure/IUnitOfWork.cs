using System;

namespace Api.Infrastructure;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
