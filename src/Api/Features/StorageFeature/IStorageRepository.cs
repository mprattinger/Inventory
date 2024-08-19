using System;

namespace Api.Features.StorageFeature;

public interface IStorageRepository
{
    void Add(Storage stock);

    void Update(Storage stock);

    Task<bool> Exists(string description);

    Task<Storage?> GetById(Guid id);
}
