namespace TimeTracker.Client.Services;

using TimeTracker.Shared.Models.TimeEntry;

public interface ITimeEntryService
{
    List<TimeEntryResponse> TimeEntries { get; }
    event Action? StateChanged;
    public TimeSpan TotalDuration { get; set; }

    Task GetTimeEntriesByProject(int projectId);
    Task<TimeEntryResponse> GetTimeEntryByIdAsync(int id);
    Task CreateTimeEntry(TimeEntryRequest timeEntry);
    Task UpdateTimeEntry(int id, TimeEntryRequest timeEntry);
    Task DeleteAsync(int id);
    Task GetByYear(int year);
    Task GetByMonth(int year, int month);
    Task GetByDay(int year, int month, int day);
}