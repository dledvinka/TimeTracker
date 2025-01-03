namespace TimeTracker.Shared.Entities;

public abstract class BaseEntity
{
    public DateTime Created { get; set; } = DateTime.Now;
    public int Id { get; set; }
    public DateTime? Updated { get; set; }
}