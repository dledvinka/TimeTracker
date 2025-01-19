namespace TimeTracker.API.Repositories;

using TimeTracker.API.Data;
using TimeTracker.API.Exceptions;
using TimeTracker.Shared.Entities;

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IUserContextService _userContextService;

    public ProjectRepository(AppDbContext dbContext, IUserContextService userContextService)
    {
        _dbContext = dbContext;
        _userContextService = userContextService;
    }

    public async Task<List<Project>> CreateAsync(Project project)
    {
        var user = await _userContextService.GetUserAsync();

        if (user == null)
            throw new EntityNotFoundException("User not found");

        project.Users.Add(user);

        _dbContext.Projects.Add(project);
        await _dbContext.SaveChangesAsync();

        return await GetAllAsync();
    }

    public async Task<List<Project>?> DeleteAsync(int id)
    {
        var userId = _userContextService.GetUserId();

        if (userId == null)
            throw new EntityNotFoundException("User not found");

        var entry = await _dbContext.Projects.FirstOrDefaultAsync(p => p.Users.Any(u => u.Id == userId) && p.Id == id);

        if (entry == null)
            throw new EntityNotFoundException($"Entity with ID = {id} was not found");


        entry.IsDeleted = true;
        entry.Deleted = DateTime.Now;

        await _dbContext.SaveChangesAsync();

        return await GetAllAsync();
    }

    public async Task<List<Project>> GetAllAsync()
    {
        var userId = _userContextService.GetUserId();

        if (userId == null)
            throw new EntityNotFoundException("User not found");

        return await _dbContext.Projects.Where(p => p.Users.Any(u => u.Id == userId) && !p.IsDeleted)
                               .Include(p => p.ProjectDetails)
                               .Include(p => p.TimeEntries)
                               .ToListAsync();
    }

    public async Task<Project?> GetAsync(int id)
    {
        var userId = _userContextService.GetUserId();

        if (userId == null)
            throw new EntityNotFoundException("User not found");

        return await _dbContext.Projects
                               .Include(p => p.ProjectDetails)
                               .FirstOrDefaultAsync(p => p.Users.Any(u => u.Id == userId) && !p.IsDeleted && p.Id == id);
    }

    public async Task<List<Project>?> UpdateAsync(int id, Project project)
    {
        var userId = _userContextService.GetUserId();

        if (userId == null)
            throw new EntityNotFoundException("User not found");

        var entry = await _dbContext.Projects.Include(p => p.ProjectDetails)
                                    .FirstOrDefaultAsync(p => p.Users.Any(u => u.Id == userId) && p.Id == id && !p.IsDeleted);

        if (entry == null)
            throw new EntityNotFoundException($"Entity with ID = {id} was not found");

        entry.Name = project.Name;

        entry.ProjectDetails ??= new ProjectDetails() { Project = entry };

        entry.ProjectDetails.Description = project.ProjectDetails.Description;
        entry.ProjectDetails.StartDate = project.ProjectDetails.StartDate;
        entry.ProjectDetails.EndDate = project.ProjectDetails.EndDate;
        entry.Updated = DateTime.Now;

        await _dbContext.SaveChangesAsync();

        return await GetAllAsync();
    }
}