namespace TimeTracker.API.Repositories;

using System.Collections.Generic;
using TimeTracker.API.Data;
using TimeTracker.API.Exceptions;

public class TimeEntryRepository : ITimeEntryRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IUserContextService _userContextService;

    public TimeEntryRepository(AppDbContext dbContext, IUserContextService userContextService)
    {
        _dbContext = dbContext;
        _userContextService = userContextService;
    }

    public async Task<List<TimeEntry>> CreateAsync(TimeEntry timeEntry)
    {
        var user = await _userContextService.GetUserAsync();

        if (user == null)
            throw new EntityNotFoundException("User not found");

        timeEntry.User = user;

        _dbContext.TimeEntries.Add(timeEntry);
        await _dbContext.SaveChangesAsync();

        return await GetAllAsync();
    }

    public async Task<List<TimeEntry>?> DeleteAsync(int id)
    {
        var userId = _userContextService.GetUserId();

        if (userId == null)
            return null;

        var entry = await _dbContext.TimeEntries.FirstOrDefaultAsync(te => te.User.Id == userId && te.Id == id);

        if (entry == null)
            throw new EntityNotFoundException($"Entity with ID = {id} was not found");

        _dbContext.TimeEntries.Remove(entry);
        await _dbContext.SaveChangesAsync();

        return await GetAllAsync();
    }

    public async Task<List<TimeEntry>> GetAllAsync()
    {
        var userId = _userContextService.GetUserId();

        if (userId == null)
            return new List<TimeEntry>();

        return await _dbContext.TimeEntries
                               .Include(te => te.Project)
                               .ThenInclude(p => p.ProjectDetails)
                               .Where(te => te.User.Id == userId)
                               .ToListAsync();
    }

    public async Task<TimeEntry?> GetAsync(int id)
    {
        var userId = _userContextService.GetUserId();

        if (userId == null)
            return null;

        return await _dbContext.TimeEntries
                               .Include(te => te.Project)
                               .ThenInclude(p => p.ProjectDetails)
                               .FirstOrDefaultAsync(te => te.User.Id == userId && te.Id == id);
    }

    public async Task<List<TimeEntry>?> GetByProjectAsync(int projectId)
    {
        var userId = CheckUserId();

        return await _dbContext.TimeEntries
                               .Where(te => te.User.Id == userId && te.ProjectId == projectId)
                               .Include(te => te.Project)
                               .ThenInclude(p => p.ProjectDetails)
                               .ToListAsync();
    }

    private string? CheckUserId()
    {
        var userId = _userContextService.GetUserId();

        if (userId == null)
            throw new EntityNotFoundException("User not found");
        return userId;
    }

    public Task<List<TimeEntry>?> GetByYearAsync(int year) => GetByYearMonthDayAsync(year, null, null);

    public Task<List<TimeEntry>?> GetByMonthAsync(int year, int month) => GetByYearMonthDayAsync(year, month, null);

    public Task<List<TimeEntry>?> GetByDayAsync(int year, int month, int day) => GetByYearMonthDayAsync(year, month, day);

    public async Task<List<TimeEntry>?> GetByYearMonthDayAsync(int year, int? month, int? day)
    {
        var userId = CheckUserId();

        return await _dbContext.TimeEntries
                               .Where(te => te.User.Id == userId && te.Start.Year == year && (!month.HasValue || te.Start.Month == month.Value) && (!day.HasValue || te.Start.Day == day.Value))
                               .Include(te => te.Project)
                               .ThenInclude(p => p.ProjectDetails)
                               .ToListAsync();
    }

    public async Task<List<TimeEntry>?> UpdateAsync(int id, TimeEntry timeEntry)
    {
        var userId = _userContextService.GetUserId();

        if (userId == null)
            throw new EntityNotFoundException("User not found");

        var entry = await _dbContext.TimeEntries.FirstOrDefaultAsync(te => te.User.Id == userId && te.Id == id);

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