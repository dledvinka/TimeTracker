namespace TimeTracker.Client.Services;

using System.Net.Http.Json;
using Mapster;
using TimeTracker.Shared.Models.TimeEntry;

public class TimeEntryService : ITimeEntryService
{
    private readonly HttpClient _httpClient;

    public List<TimeEntryResponse> TimeEntries { get; private set; } = new();
    public event Action? StateChanged;
    public TimeSpan TotalDuration { get; set; }

    public TimeEntryService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task CreateTimeEntry(TimeEntryRequest timeEntry)
    {
        var request = timeEntry.Adapt<TimeEntryCreateRequest>();
        await _httpClient.PostAsJsonAsync("/api/TimeEntry", request);
    }

    public Task DeleteAsync(int id) => _httpClient.DeleteAsync($"/api/TimeEntry/{id}");

    public async Task GetTimeEntriesByProject(int projectId)
    {
        List<TimeEntryResponse>? result = null;

        if (projectId <= 0)
            result = await _httpClient.GetFromJsonAsync<List<TimeEntryResponse>>("api/TimeEntry");
        else
            result = await _httpClient.GetFromJsonAsync<List<TimeEntryResponse>>($"api/TimeEntry/project/{projectId}");

        SetTimeEntries(result);
    }

    public async Task<TimeEntryResponse> GetTimeEntryByIdAsync(int id) => await _httpClient.GetFromJsonAsync<TimeEntryResponse>($"api/TimeEntry/{id}");

    public async Task UpdateTimeEntry(int id, TimeEntryRequest timeEntry)
    {
        var request = timeEntry.Adapt<TimeEntryUpdateRequest>();
        await _httpClient.PutAsJsonAsync($"/api/TimeEntry/{id}", request);
    }

    private void SetTimeEntries(List<TimeEntryResponse>? result)
    {
        if (result != null)
        {
            TimeEntries = result;
            TotalDuration = GetTotalDuration();
            StateChanged?.Invoke();
        }
    }

    public async Task GetByYear(int year)
    {
        var result = await _httpClient.GetFromJsonAsync<List<TimeEntryResponse>>($"api/TimeEntry/year/{year}");
        SetTimeEntries(result);
    }

    public async Task GetByMonth(int year, int month)
    {
        var result = await _httpClient.GetFromJsonAsync<List<TimeEntryResponse>>($"api/TimeEntry/month/{year}/{month}");
        SetTimeEntries(result);
    }

    public async Task GetByDay(int year, int month, int day)
    {
        var result = await _httpClient.GetFromJsonAsync<List<TimeEntryResponse>>($"api/TimeEntry/day/{year}/{month}/{day}");
        SetTimeEntries(result);
    }

    private TimeSpan GetDuration(TimeEntryResponse entry)
    {
        if (!entry.End.HasValue || entry.End.Value < entry.Start)
            return TimeSpan.Zero;

        return entry.End.Value - entry.Start;
    }

    private TimeSpan GetTotalDuration()
    {
        var total = new TimeSpan();

        foreach (var entry in TimeEntries)
            total += GetDuration(entry);

        return total;
    }
}