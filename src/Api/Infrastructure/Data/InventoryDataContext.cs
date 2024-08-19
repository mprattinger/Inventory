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
}
