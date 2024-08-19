using Api.Infrastructure;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using FluentAssertions;
using Api.Features.StorageFeature;

namespace Api.Tests.FeatureTests.StorageFeature;

public class CreateStorageTests
{
    private readonly IStorageRepository _storageRepository = Substitute.For<IStorageRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly CreateStorage _handler;

    public CreateStorageTests()
    {
        _handler = new CreateStorage(NullLogger<CreateStorage>.Instance, _storageRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_Should_ReturnExistsError_WhenStockExists()
    {

        //Arrange
        var command = new CreateStorage.Request("First Storage");
        _storageRepository.Exists(command.Description).Returns(true);

        //Act
        var result = await _handler.Handle(command);

        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnStock_WhenStockIsCreated()
    {
        var command = new CreateStorage.Request("First Storage");
        _storageRepository.Exists(command.Description).Returns(false);

        //Act
        var result = await _handler.Handle(command);

        result.IsSuccess.Should().BeTrue();
        result.Value!.Description.Should().Be(command.Description);
    }
}
