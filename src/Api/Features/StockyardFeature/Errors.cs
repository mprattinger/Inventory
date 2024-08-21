using System;
using FlintSoft.Result;

namespace Api.Features.StockyardFeature;

public static class Errors
{
    public class CreateStockyardExceptionError : Error
    {
        public CreateStockyardExceptionError(Exception ex, string code) : base(code, "Error creating stockyard!")
        {
            Exception = ex;
        }
    }

    public class StockyardExistsError : Error
    {
        public StockyardExistsError(string code) : base(code, "Stockyard already exists")
        {

        }
    }
}
