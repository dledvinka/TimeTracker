using System.Net.Http.Json;
using TimeTracker.Shared.Models.TimeEntry;

namespace TimeTracker.Client.Services;

using Mapster;

public class TimeEntryService : ITimeEntryService
{
    private readonly HttpClient _httpClient;
    public event Action? StateChanged;

    public TimeEntryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public List<TimeEntryResponse> TimeEntries { get; private set; } = new();

    public async Task GetTimeEntriesByProject(int projectId)
    {
        List<TimeEntryResponse>? result = null;

        if (projectId <= 0)
        {
            result = await _httpClient.GetFromJsonAsync<List<TimeEntryResponse>>("api/TimeEntry");
        }
        else
        {
            result = await _httpClient.GetFromJsonAsync<List<TimeEntryResponse>>($"api/TimeEntry/project/{projectId}");
        }

        if (result != null)
        {
            TimeEntries = result;
            StateChanged?.Invoke();
        }
    }

    public async Task<TimeEntryResponse> GetTimeEntryByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<TimeEntryResponse>($"api/TimeEntry/{id}");
    }

    public async Task CreateTimeEntry(TimeEntryRequest timeEntry)
    {
        var request = timeEntry.Adapt<TimeEntryCreateRequest>();
        await _httpClient.PostAsJsonAsync("/api/TimeEntry", request);
    }

    public async Task UpdateTimeEntry(int id, TimeEntryRequest timeEntry)
    {
        var request = timeEntry.Adapt<TimeEntryUpdateRequest>();
        await _httpClient.PostAsJsonAsync("/api/TimeEntry", request);
    }
}
