namespace TimeTracker.API.Repositories;

public interface ITimeEntryRepository
{
    Task<List<TimeEntry>> GetAllAsync();
    Task<List<TimeEntry>> CreateAsync(TimeEntry timeEntry);
    Task<List<TimeEntry>?> UpdateAsync(int id, TimeEntry timeEntry);
    Task<List<TimeEntry>?> DeleteAsync(int id);
    Task<TimeEntry?> GetAsync(int id);
    Task<List<TimeEntry>?> GetByProjectAsync(int projectId);
    Task<List<TimeEntry>?> GetByYearAsync(int year);
    Task<List<TimeEntry>?> GetByMonthAsync(int year, int month);
    Task<List<TimeEntry>?> GetByDayAsync(int year, int month, int day);
}