using Api.Infrastructure;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using FluentAssertions;
using Api.Features.StorageFeature;
using NSubstitute.ExceptionExtensions;
using FlintSoft.Result.FluentValidation;

namespace Api.Tests.FeatureTests.StorageFeature;

public class CreateStorageTests
{
    private readonly IStorageRepository _storageRepository = Substitute.For<IStorageRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly CreateStorage _handler;
    private readonly CreateStorage.Validator _validator;

    public CreateStorageTests()
    {
        _validator = new CreateStorage.Validator();

        _handler = new CreateStorage(NullLogger<CreateStorage>.Instance, _storageRepository, _unitOfWork, _validator);
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

    [Fact]
    public async Task Handle_HandlingExcpetion_IfServiceThrows()
    {
        var command = new CreateStorage.Request("First Storage");
        _storageRepository.Exists(command.Description).Throws(new Exception("TEst"));

        var result = await _handler.Handle(command);

        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error!.Description.Should().Be("Error creating storage!");
    }

    [Fact]
    public async Task Handle_Should_ReturnValidationError_WhenDescriptionIsEmpty()
    {
        var command = new CreateStorage.Request("");

        _storageRepository.Exists(command.Description).Returns(false);

        var result = await _handler.Handle(command);
        result.IsFailure!.Should().BeTrue();
        result.Error!.Description.Should().Be("Description must not be empty!");
    }
}
