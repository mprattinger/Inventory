using System;

namespace Api.Features.StockFeature;

public interface IStockRepository
{
    void Add(Stock stock);

    void Update(Stock stock);

    Task<bool> Exists(string description);

    Task<Stock?> GetById(Guid id);
}
