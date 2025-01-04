namespace TimeTracker.API.Repositories;

public interface IProjectRepository
{
    Task<List<Project>> GetAllAsync();
    Task<List<Project>> CreateAsync(Project project);
    Task<List<Project>?> UpdateAsync(int id, Project project);
    Task<List<Project>?> DeleteAsync(int id);
    Task<Project?> GetAsync(int id);
}