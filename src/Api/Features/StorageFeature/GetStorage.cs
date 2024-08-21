using System;
using FlintSoft.Result;

namespace Api.Features.StorageFeature;

public class GetStorage(ILogger<GetStorage> logger, IStorageRepository storageRepository)
{
    public record Request(Guid Id);

    public async Task<Result<Storage>> Handle(Request request)
    {
        try
        {
            var storage = await storageRepository.GetById(request.Id);
            if (storage is null)
            {
                return new Errors.StorageNotFound();
            }

            return storage;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error loading the storage with id {id}: {message}", request.Id, ex.Message);
            return new Errors.GetStorageExceptionError(ex, nameof(ModifyStorage));
        }
    }
}
