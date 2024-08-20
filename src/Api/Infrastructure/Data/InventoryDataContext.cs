using System;
using Api.Features.StorageFeature;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Data;

public class InventoryDataContext : DbContext
{
    public virtual DbSet<Storage> Storages => Set<Storage>();

    public InventoryDataContext(DbContextOptions<InventoryDataContext> options)
    : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Storage>()
        .HasData(new Storage(Guid.NewGuid()) { Description = "Kellerabteil"});
    }
}
