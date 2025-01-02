namespace TimeTracker.Shared.Models.TimeEntry;

public class TimeEntryResponse
{
    public DateTime? End { get; set; }
    public int Id { get; set; }
    public required string Project { get; set; }
    public DateTime Start { get; set; } = DateTime.Now;
}