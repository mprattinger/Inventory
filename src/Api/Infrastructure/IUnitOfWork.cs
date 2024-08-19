using System;

namespace Api.Infrastructure;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken token = default);
}
