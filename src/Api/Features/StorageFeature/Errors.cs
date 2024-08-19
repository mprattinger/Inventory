using System;
using FlintSoft.Result;

namespace Api.Features.StorageFeature;

public static class Errors
{
    public class StorageExistsError : Error
    {
        public StorageExistsError() : base($"Storage.{nameof(StorageExistsError)}", "Storage exists") { }
    }

    public class StorageNotFoundError : Error
    {
        public StorageNotFoundError() : base($"Storage.{nameof(StorageNotFoundError)}", "Storage not found") { }
    }
}
