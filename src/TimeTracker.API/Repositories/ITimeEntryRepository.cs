namespace TimeTracker.API.Repositories;

using TimeTracker.Shared.Entities;

public interface ITimeEntryRepository
{
    List<TimeEntry> GetAll();
    List<TimeEntry> Create(TimeEntry timeEntry);
    List<TimeEntry>? Update(int id, TimeEntry timeEntry);
    List<TimeEntry>? Delete(int id);
    TimeEntry? Get(int id);
}