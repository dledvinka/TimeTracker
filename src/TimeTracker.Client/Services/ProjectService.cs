namespace TimeTracker.Client.Services;

using System.Net.Http.Json;
using Mapster;
using TimeTracker.Shared.Models.Project;
using TimeTracker.Shared.Models.TimeEntry;

public class ProjectService : IProjectService
{
    private readonly HttpClient _httpClient;

    public List<ProjectResponse> Projects { get; private set; } = [];

    public event Action? StateChanged;

    public ProjectService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task LoadAllProjectsAsync()
    {
        var result = await _httpClient.GetFromJsonAsync<List<ProjectResponse>>("api/project");

        if (result != null)
        {
            Projects = result;
            StateChanged?.Invoke();
        }
    }

    public async Task<ProjectResponse> GetByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<ProjectResponse>($"api/Project/{id}");
    }

    public async Task Create(ProjectRequest timeEntry)
    {
        var request = timeEntry.Adapt<ProjectCreateRequest>();
        await _httpClient.PostAsJsonAsync("/api/Project", request);
    }

    public async Task Update(int id, ProjectRequest timeEntry)
    {
        var request = timeEntry.Adapt<ProjectUpdateRequest>();
        await _httpClient.PutAsJsonAsync($"/api/Project/{id}", request);
    }

    public Task DeleteAsync(int id) => _httpClient.DeleteAsync($"/api/Project/{id}");
}