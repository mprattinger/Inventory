using System;
using FlintSoft.Endpoints;
using FlintSoft.Result;

namespace Api.Features.StorageFeature;

public class Endpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/", () => {
            return new Error("TEST", "Test");
        });
    }
}
