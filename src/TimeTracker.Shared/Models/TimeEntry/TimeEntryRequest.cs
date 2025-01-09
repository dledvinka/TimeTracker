using System.ComponentModel.DataAnnotations;

namespace TimeTracker.Shared.Models.TimeEntry;

public class TimeEntryRequest
{
    [Range(1, int.MaxValue, ErrorMessage = "Project has to be set.")]
    public int ProjectId { get; set; }
    public DateTime Start { get; set; }
    public DateTime? End { get; set; }
}