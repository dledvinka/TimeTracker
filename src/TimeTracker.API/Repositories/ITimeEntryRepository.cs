namespace TimeTracker.API.Repositories;

public interface ITimeEntryRepository
{
    List<TimeEntry> GetAll();
    Task<List<TimeEntry>> CreateAsync(TimeEntry timeEntry);
    List<TimeEntry>? Update(int id, TimeEntry timeEntry);
    List<TimeEntry>? Delete(int id);
    TimeEntry? Get(int id);
}