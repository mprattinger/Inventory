using System;
using FlintSoft.Result;

namespace Api.Features.StorageFeature;

public static class Errors
{
    public class StorageExistsError : Error
    {
        public StorageExistsError() : base($"Storage.{nameof(StorageExistsError)}", "Storage exists") { }
    }

    public class StorageNotFound : NotFound
    {
        public StorageNotFound() : base($"Storage.{nameof(StorageNotFound)}", "Storage not found") { }
    }
}
