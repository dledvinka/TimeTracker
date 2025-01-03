namespace TimeTracker.API.Repositories;

public interface ITimeEntryRepository
{
    Task<List<TimeEntry>> GetAllAsync();
    Task<List<TimeEntry>> CreateAsync(TimeEntry timeEntry);
    Task<List<TimeEntry>?> UpdateAsync(int id, TimeEntry timeEntry);
    Task<List<TimeEntry>?> DeleteAsync(int id);
    Task<TimeEntry?> GetAsync(int id);
}