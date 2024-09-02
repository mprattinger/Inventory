using Api.Infrastructure;
using FlintSoft.Result;
using FlintSoft.Result.FluentValidation;
using FluentValidation;

namespace Api.Features.StorageFeature;

public sealed class CreateStorage(ILogger<CreateStorage> logger,
IStorageRepository stockRepository,
IUnitOfWork unitOfWork,
IValidator<CreateStorage.Request> validator)
{
    public record Request(string Description);

    public class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description must not be empty!");
        }
    }

    public async Task<Result<Storage>> Handle(Request request)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return validationResult.FromValidationResult()!;
            }

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
            return new Errors.CreateStorageExceptionError(ex, nameof(CreateStorage));
        }
    }
}
