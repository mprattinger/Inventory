using System;
using Api.Infrastructure;
using FlintSoft.Result;
using FlintSoft.Result.Types;

namespace Api.Features.StorageFeature;

public class RemoveStorage(ILogger<RemoveStorage> logger, IStorageRepository storageRepository, IUnitOfWork unitOfWork)
{
    public record Request(Guid Id);

    public async Task<Result<Success>> Handle(Request request)
    {
        try
        {
            var storage = await storageRepository.GetById(request.Id);
            if (storage is null)
            {
                return new Errors.StorageNotFound();
            }

            storageRepository.Remove(storage);
            await unitOfWork.SaveChangesAsync();

            return new Success();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting the storage with id {id}: {message}", request.Id, ex.Message);
            return new Error(nameof(ModifyStorage), $"Error deleting storage!");
        }
    }
}
