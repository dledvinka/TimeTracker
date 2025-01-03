namespace TimeTracker.Shared.Entities;

public abstract class SoftDeletableEntity : BaseEntity
{
    public DateTime? Deleted { get; set; }
    public bool IsDeleted { get; set; } = false;
}