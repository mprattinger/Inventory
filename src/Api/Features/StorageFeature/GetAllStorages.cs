using FlintSoft.Result;

namespace Api.Features.StorageFeature;

public sealed class GetAllStorages(ILogger<GetAllStorages> logger, IStorageRepository storageRepository)
{
    public async Task<Result<List<Storage>>> Handle()
    {
        try
        {
            var ret = await storageRepository.GetAll();

            return ret;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error reading all storages: {message}", ex.Message);
            return new Error(nameof(GetAllStorages), $"Error reading all storage!");
        }
    }
}
