using System;
using Api.Infrastructure;
using FlintSoft.Result;
using FlintSoft.Result.Types;
using MediatR;

namespace Api.Features.StockFeature;

public static class CreateStock
{
    public record Command(string Description) : IRequest<Result<Stock>>;

    internal sealed class Handler : IRequestHandler<Command, Result<Stock>>
    {
        private readonly ILogger<Handler> _logger;
        private readonly IStockRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ILogger<Handler> logger, IStockRepository repository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Stock>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                if (await _repository.Exists(request.Description))
                {
                    return new Errors.StockExistsError();
                }

                var stock = new Stock
                {
                    Id = Guid.NewGuid(),
                    Description = request.Description
                };

                _repository.Add(stock);
                await _unitOfWork.SaveChangesAsync();

                return stock;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating the stock with name {description}: {message}", request.Description, ex.Message);
                return new Error(nameof(CreateStock), $"Error creating stock!");
            }
        }
    }
}
