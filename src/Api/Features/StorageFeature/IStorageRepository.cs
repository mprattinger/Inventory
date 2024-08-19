using Api.Infrastructure.Repositories;

namespace Api.Features.StorageFeature;

public interface IStorageRepository : IRepository<Storage>
{
    Task<bool> Exists(string description);
}
