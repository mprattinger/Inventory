using System;
using Api.Features.StockyardFeature;
using Api.Infrastructure;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;

namespace Api.Tests.FeatureTests.StockyardFeature;

public class CreateStockyardTests
{
    private readonly IStockyardRepository _stockyardRepository = Substitute.For<IStockyardRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private readonly CreateStockyard _handler;

    public CreateStockyardTests()
    {
        _handler = new CreateStockyard(NullLogger<CreateStockyard>.Instance, _stockyardRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldReturnStockyard_WhenCreated()
    {
        var storageGuid = Guid.NewGuid();

        var request = new CreateStockyard.Request("First", storageGuid, "Pos1", "Pos2", "Pos3");

        var result = await _handler.Handle(request);
        result.IsSuccess.Should().BeTrue();
        result.Value!.Description.Should().Be(request.Description);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenExists()
    {
        var storageGuid = Guid.NewGuid();

        var request = new CreateStockyard.Request("First", storageGuid, "Pos1", "Pos2", "Pos3");

        var result = await _handler.Handle(request);
        result.IsFailure.Should().BeTrue();
        result.Error!.Should().BeOfType<Errors.StockyardExistsError>();
        result.Error!.Description.Should().Be(new Errors.StockyardExistsError("code").Description);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenServiceThrowsException()
    {
        var storageGuid = Guid.NewGuid();

        var request = new CreateStockyard.Request("First", storageGuid, "Pos1", "Pos2", "Pos3");

        var result = await _handler.Handle(request);
        result.IsFailure.Should().BeTrue();
        result.Error!.Should().BeOfType<Errors.CreateStockyardExceptionError>();
        result.Error!.Description.Should().Be(new Errors.CreateStockyardExceptionError(new Exception(), "code").Description);
    }
}
