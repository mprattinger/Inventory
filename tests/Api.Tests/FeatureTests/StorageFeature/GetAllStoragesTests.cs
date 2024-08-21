using System;
using Api.Features.StorageFeature;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace Api.Tests.FeatureTests.StorageFeature;

public class GetAllStoragesTests
{
    private readonly IStorageRepository _storageRepository = Substitute.For<IStorageRepository>();

    private readonly GetAllStorages _handler;

    public GetAllStoragesTests()
    {
        _handler = new GetAllStorages(NullLogger<GetAllStorages>.Instance, _storageRepository);
    }

    [Fact]
    public async Task Handle_Should_Return_EmptyList()
    {
        _storageRepository.GetAll().Returns(new List<Storage>());

        var result = await _handler.Handle();

        result.IsSuccess.Should().BeTrue();
        result.Value!.Count.Should().Be(0);
    }

    [Fact]
    public async Task Handle_Should_Return_List()
    {
        _storageRepository.GetAll().Returns(new List<Storage> {
            new Storage(Guid.NewGuid()) { Description = "Test"}
        });

        var result = await _handler.Handle();

        result.IsSuccess.Should().BeTrue();
        result.Value!.Count.Should().Be(1);
    }

    [Fact]
    public async Task Handle_HandlingExcpetion_IfServiceThrows()
    {
        _storageRepository.GetAll().Throws(new Exception("Test"));

        var result = await _handler.Handle();

        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error!.Description.Should().Be("Error reading all storage!");
    }
}
