using System;
using Api.Features.StockyardFeature;
using Api.Infrastructure.Data;

namespace Api.Infrastructure.Repositories;

public class StockyardRepository: Repository<Stockyard>, IStockyardRepository
{
    public StockyardRepository(InventoryDataContext context) : base(context)
    {
        
    }
}
