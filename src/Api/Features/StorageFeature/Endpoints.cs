using System;
using FlintSoft.Endpoints;
using FlintSoft.Result;
using Microsoft.AspNetCore.Mvc;

namespace Api.Features.StorageFeature;

public class Endpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var grp = app.MapGroup("api/storage");

        grp.MapGet("/", async (GetAllStorages useCase) => await useCase.Handle());
        grp.MapGet("/{id}", async (string id, GetStorage useCase) =>
        {
            var guid = Guid.Parse(id);
            return await useCase.Handle(new GetStorage.Request(guid));
        });
        grp.MapPost("/", async (CreateStorage.Request request, CreateStorage useCase) => await useCase.Handle(request));
        grp.MapPatch("/", async (ModifyStorage.Request request, ModifyStorage useCase) => await useCase.Handle(request));
        grp.MapDelete("/{id}", async (string id, RemoveStorage useCase) =>  {
            var guid = Guid.Parse(id);
            return await useCase.Handle(new RemoveStorage.Request(guid));
        });
    }
}
