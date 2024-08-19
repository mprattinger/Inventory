using System;
using Api.Features.StockFeature;
using Api.Infrastructure;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Api.Tests.FeatureTests.StockFeature;

public class ModifyStockTests
{
    private readonly IStockRepository _stockRepository = Substitute.For<IStockRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly ModifyStock.Handler _handler;

    public ModifyStockTests()
    {
        _handler = new ModifyStock.Handler(NullLogger<ModifyStock.Handler>.Instance, _stockRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_Should_ReturnNotFoundError_When_StockNotFound()
    {
        //Arrange
        var guid = Guid.NewGuid();
        var command = new ModifyStock.Command(guid, "Modified Stock");
        _stockRepository.GetById(guid).ReturnsNull();

        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.StockNotFoundError>();
    }

    [Fact]
    public async Task Handle_Should_ReturnModifiedStock_When_StockExists()
    {
        //Arrange
        var guid = Guid.NewGuid();
        var command = new ModifyStock.Command(guid, "Modified Stock");
        var stock = new Stock { Id = guid, Description = command.Description };
        _stockRepository.GetById(guid).Returns(stock);

        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value!.Id.Should().Be(guid);
        result.Value!.Description.Should().Be(command.Description);
    }
}
