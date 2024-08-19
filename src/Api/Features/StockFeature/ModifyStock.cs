using System;
using Api.Infrastructure;
using FlintSoft.Result;
using MediatR;

namespace Api.Features.StockFeature;

public static class ModifyStock
{
    public record Command(Guid Id, string Description) : IRequest<Result<Stock>>;

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
                var stock = await _repository.GetById(request.Id);
                if(stock is null) {
                    return new Errors.StockNotFoundError();
                }

                stock.Description = request.Description;

                _repository.Update(stock);
                await _unitOfWork.SaveChangesAsync();

                return stock;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error modifing the stock with id {id} to name {description}: {message}", request.Id, request.Description, ex.Message);
                return new Error(nameof(CreateStock), $"Error modifing stock!");
            }
        }
    }
}
