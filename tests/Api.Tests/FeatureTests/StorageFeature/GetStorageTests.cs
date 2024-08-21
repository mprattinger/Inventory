using System;
using Api.Features.StorageFeature;
using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;

namespace Api.Tests.FeatureTests.StorageFeature;

public class GetStorageTests
{
    private readonly IStorageRepository _storageRepository = Substitute.For<IStorageRepository>();

    private readonly GetStorage _handler;

    public GetStorageTests()
    {
        _handler = new GetStorage(NullLogger<GetStorage>.Instance, _storageRepository);
    }

    [Fact]
    public async Task Handle_WhenStorageExists_ReturnsStorage()
    {
        var storageId = Guid.NewGuid();
        var storage = new Storage(storageId) { Description = "Test" };
        _storageRepository.GetById(storageId).Returns(storage);

        var result = await _handler.Handle(new GetStorage.Request(storageId));

        result.IsSuccess.Should().BeTrue();
        result.Value!.Description.Should().Be("Test");
    }

    [Fact]
    public async Task Handle_WhenStorageNotExists_ReturnsNotFound()
    {
        var storageId = Guid.NewGuid();
        var storage = new Storage(storageId) { Description = "Test" };
        _storageRepository.GetById(storageId).ReturnsNull();

        var result = await _handler.Handle(new GetStorage.Request(storageId));

        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_HandlingExcpetion_IfServiceThrows()
    {
        var storageId = Guid.NewGuid();
        var storage = new Storage(storageId) { Description = "Test" };
        _storageRepository.GetById(storageId).Throws(new Exception("Test"));

        var result = await _handler.Handle(new GetStorage.Request(storageId));

        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeTrue();
        result.Error!.Description.Should().Be("Error loading storage!");
    }
}
