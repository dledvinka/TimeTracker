namespace TimeTracker.Client.Services;

using TimeTracker.Shared.Models.Project;

public interface IProjectService
{
    public List<ProjectResponse> Projects { get; }

    event Action? StateChanged;
    Task LoadAllProjectsAsync();
    Task<ProjectResponse> GetByIdAsync(int id);
    Task Create(ProjectRequest timeEntry);
    Task Update(int id, ProjectRequest timeEntry);
    Task DeleteAsync(int id);
}