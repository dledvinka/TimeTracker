namespace TimeTracker.Client.Services;

using TimeTracker.Shared.Models.TimeEntry;

public interface ITimeEntryService
{
    List<TimeEntryResponse> TimeEntries { get; }
    event Action? StateChanged;

    Task GetTimeEntriesByProject(int projectId);
    Task<TimeEntryResponse> GetTimeEntryByIdAsync(int id);
    Task CreateTimeEntry(TimeEntryRequest timeEntry);
    Task UpdateTimeEntry(int id, TimeEntryRequest timeEntry);
}