namespace TimeTracker.API.Repositories;

public class TimeEntryRepository : ITimeEntryRepository
{
    private static readonly List<TimeEntry> _entries =
    [
        new TimeEntry()
        {
            Id = 1,
            Project = "Time Tracker app",
            End = DateTime.Now.AddHours(1)
        }
    ];

    public List<TimeEntry> Create(TimeEntry timeEntry)
    {
        _entries.Add(timeEntry);
        return _entries;
    }

    public List<TimeEntry>? Delete(int id)
    {
        var entry = _entries.FirstOrDefault(e => e.Id == id);

        if (entry == null)
            return null;

        _entries.Remove(entry);

        return _entries;
    }

    public TimeEntry? Get(int id)
    {
        var entry = _entries.FirstOrDefault(e => e.Id == id);

        return entry;
    }

    public List<TimeEntry> GetAll() => _entries;

    public List<TimeEntry>? Update(int id, TimeEntry timeEntry)
    {
        var entry = _entries.FirstOrDefault(e => e.Id == id);

        if (entry == null)
            return null;

        entry.Project = timeEntry.Project;
        entry.Start = timeEntry.Start;
        entry.End = timeEntry.End;
        entry.Updated = DateTime.Now;

        return _entries;
    }
}