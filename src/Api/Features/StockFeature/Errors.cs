using System;
using FlintSoft.Result;

namespace Api.Features.StockFeature;

public static class Errors
{
    public class StockExistsError : Error
    {
        public StockExistsError() : base($"Stock.{nameof(StockExistsError)}", "Stock exists") { }
    }

    public class StockNotFoundError : Error
    {
        public StockNotFoundError() : base($"Stock.{nameof(StockNotFoundError)}", "Stock not found") { }
    }
}
