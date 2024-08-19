using Api.Features.StockFeature;
using Api.Infrastructure;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using FluentAssertions;
using FlintSoft.Result;

namespace Api.Tests.FeatureTests.StockFeature;

public class CreateStockTests
{
    private readonly IStockRepository _stockRepository = Substitute.For<IStockRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly CreateStock.Handler _handler;

    public CreateStockTests()
    {
        _handler = new CreateStock.Handler(NullLogger<CreateStock.Handler>.Instance, _stockRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_Should_ReturnExistsError_WhenStockExists()
    {

        //Arrange
        var command = new CreateStock.Command("First Stock");
        _stockRepository.Exists(command.Description).Returns(true);

        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnStock_WhenStockIsCreated()
    {
        var command = new CreateStock.Command("First Stock");
        _stockRepository.Exists(command.Description).Returns(false);

        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value!.Description.Should().Be(command.Description);
    }
}
