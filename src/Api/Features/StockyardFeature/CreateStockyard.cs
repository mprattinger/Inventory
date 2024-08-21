using System;
using Api.Infrastructure;
using FlintSoft.Result;

namespace Api.Features.StockyardFeature;

public sealed class CreateStockyard(ILogger<CreateStockyard> logger, IStockyardRepository repository, IUnitOfWork unitOfWork)
{
    public record Request(string Description, Guid StorageId, string Position1 = "", string Position2 = "", string Position3 = "");

    public async Task<Result<Stockyard>> Handle(Request request)
    {
        try
        {
            //Prüfen ob es den Lagerplatz schon gibt

            var stockyard = new Stockyard(Guid.NewGuid())
            {
                Description = request.Description,
                StorageId = request.StorageId,
                Position1 = request.Position1,
                Position2 = request.Position2,
                Position3 = request.Position3
            };

            return stockyard;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating the stockyard with name {description}: {message}", request.Description, ex.Message);
            return new Errors.CreateStockyardExceptionError(ex, nameof(CreateStockyard));
        }
    }
}
