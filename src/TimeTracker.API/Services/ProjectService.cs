namespace TimeTracker.API.Services;

using Mapster;
using TimeTracker.API.Exceptions;
using TimeTracker.Shared.Models.Project;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository ProjectRepository) => _projectRepository = ProjectRepository;

    public async Task<List<ProjectResponse>> CreateAsync(ProjectCreateRequest createRequest)
    {
        var newEntry = createRequest.Adapt<Project>();
        var result = await _projectRepository.CreateAsync(newEntry);

        return result.Adapt<List<ProjectResponse>>();
    }

    public async Task<List<ProjectResponse>?> DeleteAsync(int id)
    {
        var result = await _projectRepository.DeleteAsync(id);

        return result?.Adapt<List<ProjectResponse>>();
    }

    public async Task<List<ProjectResponse>> GetAllAsync()
    {
        var result = await _projectRepository.GetAllAsync();

        return result.Adapt<List<ProjectResponse>>();
    }

    public async Task<ProjectResponse?> GetAsync(int id)
    {
        var result = await _projectRepository.GetAsync(id);

        return result?.Adapt<ProjectResponse>();
    }

    public async Task<List<ProjectResponse>?> UpdateAsync(int id, ProjectUpdateRequest updateRequest)
    {
        try
        {
            var updatedEntry = updateRequest.Adapt<Project>();
            var result = await _projectRepository.UpdateAsync(id, updatedEntry);

            return result.Adapt<List<ProjectResponse>>();
        }
        catch (EntityNotFoundException)
        {
            return null;
        }
    }
}