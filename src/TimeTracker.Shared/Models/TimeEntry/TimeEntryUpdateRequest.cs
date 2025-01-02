namespace TimeTracker.Shared.Models.TimeEntry;

public class TimeEntryUpdateRequest
{
    public DateTime? End { get; set; }
    public required string Project { get; set; }
    public DateTime Start { get; set; } = DateTime.Now;
}