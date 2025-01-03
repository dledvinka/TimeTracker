namespace TimeTracker.API.Repositories;

using TimeTracker.API.Data;

public class TimeEntryRepository : ITimeEntryRepository
{
    private readonly AppDbContext _dbContext;

    public TimeEntryRepository(AppDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<TimeEntry>> CreateAsync(TimeEntry timeEntry)
    {
        _dbContext.TimeEntries.Add(timeEntry);
        await _dbContext.SaveChangesAsync();

        return await _dbContext.TimeEntries.ToListAsync();
    }

    public async Task<List<TimeEntry>?> DeleteAsync(int id)
    {
        var entry = await _dbContext.TimeEntries.FirstOrDefaultAsync(e => e.Id == id);

        if (entry == null)
            return null;

        _dbContext.TimeEntries.Remove(entry);
        await _dbContext.SaveChangesAsync();

        return await _dbContext.TimeEntries.ToListAsync();
    }

    public Task<List<TimeEntry>> GetAllAsync() => _dbContext.TimeEntries.ToListAsync();

    public async Task<TimeEntry?> GetAsync(int id) => await _dbContext.TimeEntries.FirstOrDefaultAsync(e => e.Id == id);

    public async Task<List<TimeEntry>?> UpdateAsync(int id, TimeEntry timeEntry)
    {
        var entry = await _dbContext.TimeEntries.FirstOrDefaultAsync(e => e.Id == id);

        if (entry == null)
            return null;

        entry.Project = timeEntry.Project;
        entry.Start = timeEntry.Start;
        entry.End = timeEntry.End;
        entry.Updated = DateTime.Now;

        await _dbContext.SaveChangesAsync();

        return await _dbContext.TimeEntries.ToListAsync();
    }
}