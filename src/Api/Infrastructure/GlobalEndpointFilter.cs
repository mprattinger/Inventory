using System;
using System.Reflection.Metadata.Ecma335;
using FlintSoft.Result;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Infrastructure;

public class GlobalEndpointFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var res = await next(context);

        if (res is Error)
        {
            Console.WriteLine("error");
            return Results.BadRequest((res as Error)!.Description);
        }

        var rType = (res as FlintSoft.Result.IResult);
        if (rType is null)
        {
            throw new Exception("Cannot detect Resulttype!");
        }
        return Results.Ok(rType.GetValue());
    }
}
