using Api.Infrastructure;
using FlintSoft.Result;

namespace Api.Features.StorageFeature;

public sealed class CreateStorage(ILogger<CreateStorage> logger, IStorageRepository stockRepository, IUnitOfWork unitOfWork)
{
    public record Request(string Description);

    public async Task<Result<Storage>> Handle(Request request) {
        try
            {
                if (await stockRepository.Exists(request.Description))
                {
                    return new Errors.StorageExistsError();
                }

                var storage = new Storage(Guid.NewGuid());
                storage.Description = request.Description;

                stockRepository.Add(storage);
                await unitOfWork.SaveChangesAsync();

                return storage;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error creating the Storage with name {description}: {message}", request.Description, ex.Message);
                return new Error(nameof(CreateStorage), $"Error creating stock!");
            }
    }
}
