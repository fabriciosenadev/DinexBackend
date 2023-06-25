namespace Dinex.Core;

public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public Entity()
    {
        Id = Guid.NewGuid();
    }
}
