using System;
using Api.Features.StorageFeature;
using Api.Infrastructure.Data;

namespace Api.Features.StockyardFeature;

public class Stockyard : Entity
{
    public Stockyard(Guid id) : base(id) { }
    
    private Stockyard() {}

    public string Description { get; set; } = "";

    public string Position1 { get; set; }= "";

    public string Position2 { get; set; }= "";
    
    public string Position3 { get; set; }= "";

    public Guid StorageId { get; set; }

    public virtual Storage? Storage {get; set;}
}
