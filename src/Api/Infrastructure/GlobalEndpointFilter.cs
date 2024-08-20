using System;
using System.Reflection.Metadata.Ecma335;
using FlintSoft.Result;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Infrastructure;

public class GlobalEndpointFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var response = await next(context);
        
        try
        {
            var result = (response as FlintSoft.Result.IResult);
            if(result is null) {
                throw new Exception("IResult is null!");
            }

            if(result.IsFailure) {
                var err = result.GetError();
                if(err is null) {
                    throw new Exception("No error");
                }

                Error error = (Error)err;
                return Results.BadRequest(error.Code + ": " + error.Description);
            }

            if(result.IsNotFound) {
                var err = result.GetError();
                if(err is null) {
                    throw new Exception("No error");
                }

                FlintSoft.Result.NotFound nf = (FlintSoft.Result.NotFound)err;
                return Results.NotFound(nf.Code + ": " + nf.Description);
            }

            return Results.Ok(result.GetValue());
        }
        catch (Exception)
        {
            
        }

        return response;
    }
}
