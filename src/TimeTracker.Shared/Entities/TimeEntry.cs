namespace TimeTracker.Shared.Entities;

public class TimeEntry : BaseEntity
{
    public DateTime? End { get; set; }
    public Project? Project { get; set; }
    public int? ProjectId { get; set; }
    public DateTime Start { get; set; } = DateTime.Now;
    public User? User { get; set; }
}