using System;
using Api.Infrastructure.Data;

namespace Api.Features.StorageFeature;

public class Storage : Entity
{
    public Storage(Guid id) : base(id) { }
    
    private Storage() {}

    public string Description { get; set; } = "";
}
