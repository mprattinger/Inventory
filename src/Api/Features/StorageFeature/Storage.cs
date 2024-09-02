using System;
using System.ComponentModel.DataAnnotations;
using Api.Infrastructure.Data;

namespace Api.Features.StorageFeature;

public class Storage : Entity
{
    public Storage(Guid id) : base(id) { }

    private Storage() { }

    [Required]
    public string Description { get; set; } = "";
}
