namespace TimeTracker.Shared.Models.TimeEntry;

public class TimeEntryRequest
{
    public int ProjectId { get; set; }
    public DateTime Start { get; set; }
    public DateTime? End { get; set; }
}