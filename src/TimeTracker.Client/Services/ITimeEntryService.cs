using TimeTracker.Shared.Models.TimeEntry;

namespace TimeTracker.Client.Services;

public interface ITimeEntryService
{
    event Action? StateChanged;

    List<TimeEntryResponse> TimeEntries { get; }

    Task GetTimeEntriesByProject(int projectId);
}
