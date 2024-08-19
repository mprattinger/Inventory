using System;

namespace Api.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly InventoryDataContext _context;

    public UnitOfWork(InventoryDataContext context)
    {
        _context = context;
    }

    public Task SaveChangesAsync(CancellationToken token = default)
    {
        return _context.SaveChangesAsync(token);
    }
}
