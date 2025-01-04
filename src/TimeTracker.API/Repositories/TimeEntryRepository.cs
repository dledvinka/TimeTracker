namespace TimeTracker.API.Repositories;

using TimeTracker.API.Data;
using TimeTracker.API.Exceptions;

public class TimeEntryRepository : ITimeEntryRepository
{
    private readonly AppDbContext _dbContext;

    public TimeEntryRepository(AppDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<TimeEntry>> CreateAsync(TimeEntry timeEntry)
    {
        _dbContext.TimeEntries.Add(timeEntry);
        await _dbContext.SaveChangesAsync();

        return await GetAllAsync();
    }

    public async Task<List<TimeEntry>?> DeleteAsync(int id)
    {
        var entry = await _dbContext.TimeEntries.FirstOrDefaultAsync(e => e.Id == id);

        if (entry == null)
            throw new EntityNotFoundException($"Entity with ID = {id} was not found");

        _dbContext.TimeEntries.Remove(entry);
        await _dbContext.SaveChangesAsync();

        return await GetAllAsync();
    }

    public Task<List<TimeEntry>> GetAllAsync() => _dbContext.TimeEntries.Include(te => te.Project).ThenInclude(p => p.ProjectDetails).ToListAsync();

    public async Task<TimeEntry?> GetAsync(int id) =>
        await _dbContext.TimeEntries.Include(te => te.Project).ThenInclude(p => p.ProjectDetails).FirstOrDefaultAsync(e => e.Id == id);

    public async Task<List<TimeEntry>?> GetByProjectAsync(int projectId) =>
        await _dbContext.TimeEntries.Where(te => te.ProjectId == projectId).Include(te => te.Project).ThenInclude(p => p.ProjectDetails).ToListAsync();

    public async Task<List<TimeEntry>?> UpdateAsync(int id, TimeEntry timeEntry)
    {
        var entry = await _dbContext.TimeEntries.FirstOrDefaultAsync(e => e.Id == id);

        if (entry == null)
            throw new EntityNotFoundException($"Entity with ID = {id} was not found");

        entry.ProjectId = timeEntry.ProjectId;
        entry.Start = timeEntry.Start;
        entry.End = timeEntry.End;
        entry.Updated = DateTime.Now;

        await _dbContext.SaveChangesAsync();

        return await GetAllAsync();
    }
}