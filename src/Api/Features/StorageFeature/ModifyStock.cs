using System;
using Api.Infrastructure;
using FlintSoft.Result;

namespace Api.Features.StorageFeature;

public sealed class ModifyStorage(ILogger<ModifyStorage> logger, IStorageRepository storageRepository, IUnitOfWork unitOfWork)
{
    public record Request(Guid Id, string Description);

    public async Task<Result<Storage>> Handle(Request request) {
        try
            {
                var storage = await storageRepository.GetById(request.Id);
                if(storage is null) {
                    return new Errors.StorageNotFoundError();
                }

                storage.Description = request.Description;

                storageRepository.Update(storage);
                await unitOfWork.SaveChangesAsync();

                return storage;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error modifing the storage with id {id} to name {description}: {message}", request.Id, request.Description, ex.Message);
                return new Error(nameof(ModifyStorage), $"Error modifing storage!");
            }
    }
}
