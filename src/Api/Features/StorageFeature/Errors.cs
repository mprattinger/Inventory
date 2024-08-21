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

    public class CreateStorageExceptionError : Error
    {
        public CreateStorageExceptionError(Exception ex, string code) : base(code, "Error creating storage!")
        {
            Exception = ex;
        }
    }

    public class GetAllStoragesStorageExceptionError : Error
    {
        public GetAllStoragesStorageExceptionError(Exception ex, string code) : base(code, "Error reading all storage!")
        {
            Exception = ex;
        }
    }

    public class GetStorageExceptionError : Error
    {
        public GetStorageExceptionError(Exception ex, string code) : base(code, "Error loading storage!")
        {
            Exception = ex;
        }
    }

    public class ModifyStorageExceptionError : Error
    {
        public ModifyStorageExceptionError(Exception ex, string code) : base(code, "Error modifing storage!")
        {
            Exception = ex;
        }
    }

    public class RemoveStorageExceptionError : Error
    {
        public RemoveStorageExceptionError(Exception ex, string code) : base(code, "Error deleting storage!")
        {
            Exception = ex;
        }
    }
}
