namespace TimeTracker.API.Services;

using TimeTracker.Shared.Models.Project;

public interface IProjectService
{
    Task<List<ProjectResponse>> GetAllAsync();
    Task<List<ProjectResponse>> CreateAsync(ProjectCreateRequest createRequest);
    Task<List<ProjectResponse>?> UpdateAsync(int id, ProjectUpdateRequest updateRequest);
    Task<List<ProjectResponse>?> DeleteAsync(int id);
    Task<ProjectResponse?> GetAsync(int id);
}