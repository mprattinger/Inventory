using Api.Features.StorageFeature;
using Api.Infrastructure;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Api.Tests.FeatureTests.StorageFeature;

public class ModifyStorageTests
{
    private readonly IStorageRepository _storageRepository = Substitute.For<IStorageRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly ModifyStorage _handler;

    public ModifyStorageTests()
    {
        _handler = new ModifyStorage(NullLogger<ModifyStorage>.Instance, _storageRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_Should_ReturnNotFoundError_When_StockNotFound()
    {
        //Arrange
        var guid = Guid.NewGuid();
        var command = new ModifyStorage.Request(guid, "Modified Storage");
        _storageRepository.GetById(guid).ReturnsNull();

        //Act
        var result = await _handler.Handle(command);

        result.IsNotFound.Should().BeTrue();
        result.Error.Should().BeOfType<Errors.StorageNotFound>();
    }

    [Fact]
    public async Task Handle_Should_ReturnModifiedStock_When_StockExists()
    {
        //Arrange
        var guid = Guid.NewGuid();
        var command = new ModifyStorage.Request(guid, "Modified Storage");
        var stock = new Storage(guid) { Description = command.Description };
        _storageRepository.GetById(guid).Returns(stock);

        //Act
        var result = await _handler.Handle(command);

        result.IsSuccess.Should().BeTrue();
        result.Value!.Id.Should().Be(guid);
        result.Value!.Description.Should().Be(command.Description);
    }
}
