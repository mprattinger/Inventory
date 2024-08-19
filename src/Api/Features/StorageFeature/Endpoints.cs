using System;
using FlintSoft.Endpoints;
using FlintSoft.Result;

namespace Api.Features.StorageFeature;

public class Endpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var grp = app.MapGroup("api/storage");

        grp.MapGet("/", async (GetAllStorages useCase) => await useCase.Handle());
    }
}
