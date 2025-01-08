namespace TimeTracker.Client.Services;

using System.Net.Http.Json;
using TimeTracker.Shared.Models.Project;

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
}