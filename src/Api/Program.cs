using System.Reflection;
using Api.Features;
using Api.Infrastructure;
using FlintSoft.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

builder.Services.AddInfrastructure();
builder.Services.AddFeatures();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var routes = app.MapGroup(string.Empty);
routes.AddEndpointFilter<GlobalEndpointFilter>();

routes.MapEndpoints(app);

app.Run();