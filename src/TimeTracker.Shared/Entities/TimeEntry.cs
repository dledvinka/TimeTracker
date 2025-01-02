namespace TimeTracker.Shared.Entities;

public class TimeEntry
{
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime? End { get; set; }
    public int Id { get; set; }
    public required string Project { get; set; }
    public DateTime Start { get; set; } = DateTime.Now;
    public DateTime? Updated { get; set; }
}