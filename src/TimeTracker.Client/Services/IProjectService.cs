namespace TimeTracker.Client.Services;

using TimeTracker.Shared.Models.Project;

public interface IProjectService
{
    public List<ProjectResponse> Projects { get; }

    event Action? StateChanged;
    Task LoadAllProjectsAsync();
}