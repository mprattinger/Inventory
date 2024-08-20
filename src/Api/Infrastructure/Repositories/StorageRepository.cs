using System;
using Api.Features.StorageFeature;
using Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Repositories;

public class StorageRepository : Repository<Storage>, IStorageRepository
{

    public StorageRepository(InventoryDataContext context) : base(context)
    {
        
    }

    public async Task<bool> Exists(string description)
    {
        return await DbContext.Storages.AnyAsync(s => s.Description == description);
    }
}
