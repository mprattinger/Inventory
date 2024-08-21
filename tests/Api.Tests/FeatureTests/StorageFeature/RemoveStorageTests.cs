using System;
using Api.Features.StorageFeature;
using Api.Infrastructure;
using FlintSoft.Result.Types;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;

namespace Api.Tests.FeatureTests.StorageFeature;

public class RemoveStorageTests
{
    private readonly IStorageRepository _storageRepository = Substitute.For<IStorageRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    private readonly RemoveStorage _handler;

    public RemoveStorageTests()
    {
        _handler = new RemoveStorage(NullLogger<RemoveStorage>.Instance, _storageRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_Should_ReturnNotFoundError_When_StockNotFound()
    {
        //Arrange
        var guid = Guid.NewGuid();
        var command = new RemoveStorage.Request(guid);
        _storageRepository.GetById(guid).ReturnsNull();

        //Act
        var result = await _handler.Handle(command);

        result.IsNotFound.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.StorageNotFound>();
    }

    [Fact]
    public async Task Handle_Should_ReturnSucess_When_StockExists()
    {
        //Arrange
        var guid = Guid.NewGuid();
        var command = new RemoveStorage.Request(guid);
        var stock = new Storage(guid) { Description = "Test" };
        _storageRepository.GetById(guid).Returns(stock);

        //Act
        var result = await _handler.Handle(command);

        result.IsSuccess.Should().BeTrue();
        result.Value!.Should().Be(new Success());
    }

    [Fact]
    public async Task Handle_HandlingExcpetion_IfServiceThrows()
    {
        var storageId = Guid.NewGuid();
        var storage = new Storage(storageId) { Description = "Test" };
        _storageRepository.GetById(storageId).Throws(new Exception("Test"));

        var result = await _handler.Handle(new RemoveStorage.Request(storageId));

        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error!.Description.Should().Be("Error deleting storage!");
    }
}
