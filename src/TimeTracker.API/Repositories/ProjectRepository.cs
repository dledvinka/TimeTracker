namespace TimeTracker.API.Repositories;

using TimeTracker.API.Data;
using TimeTracker.API.Exceptions;

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _dbContext;

    public ProjectRepository(AppDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<Project>> CreateAsync(Project project)
    {
        _dbContext.Projects.Add(project);
        await _dbContext.SaveChangesAsync();

        return await GetAllAsync();
    }

    public async Task<List<Project>?> DeleteAsync(int id)
    {
        var entry = await _dbContext.Projects.FirstOrDefaultAsync(e => e.Id == id);

        if (entry == null)
            throw new EntityNotFoundException($"Entity with ID = {id} was not found");

        _dbContext.Projects.Remove(entry);
        await _dbContext.SaveChangesAsync();

        return await GetAllAsync();
    }

    public Task<List<Project>> GetAllAsync() => _dbContext.Projects.Include(p => p.ProjectDetails).ToListAsync();

    public async Task<Project?> GetAsync(int id) => await _dbContext.Projects.Include(p => p.ProjectDetails).FirstOrDefaultAsync(e => e.Id == id);

    public async Task<List<Project>?> UpdateAsync(int id, Project project)
    {
        var entry = await _dbContext.Projects.Include(p => p.ProjectDetails).FirstOrDefaultAsync(e => e.Id == id);

        if (entry == null)
            throw new EntityNotFoundException($"Entity with ID = {id} was not found");

        entry.Name = project.Name;

        if (entry.ProjectDetails == null)
            entry.ProjectDetails = new ProjectDetails()
            {
                Project = entry
            };

        entry.ProjectDetails.Description = project.ProjectDetails.Description;
        entry.ProjectDetails.StartDate = project.ProjectDetails.StartDate;
        entry.ProjectDetails.EndDate = project.ProjectDetails.EndDate;
        entry.Updated = DateTime.Now;

        await _dbContext.SaveChangesAsync();

        return await GetAllAsync();
    }
}