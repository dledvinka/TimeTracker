namespace TimeTracker.Shared.Models.TimeEntry;

using TimeTracker.Shared.Models.Project;

public record struct TimeEntryResponse(int Id, ProjectResponse Project, DateTime Start, DateTime? End);