using System;
using Api.Features.StorageFeature;

namespace Api.Infrastructure.Repositories;

public class StorageRepository : Repository<Storage>, IStorageRepository
{
    public Task<bool> Exists(string description)
    {
        throw new NotImplementedException();
    }
}
