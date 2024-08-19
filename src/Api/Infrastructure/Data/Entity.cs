using System.ComponentModel.DataAnnotations;

namespace Api.Infrastructure.Data;

public abstract class Entity
{
    [Key]
    public Guid Id { get; set; }


    protected Entity(Guid id)
    {
        Id = id;
    }

    protected Entity()
    {
        Id = default!;
    }
}
