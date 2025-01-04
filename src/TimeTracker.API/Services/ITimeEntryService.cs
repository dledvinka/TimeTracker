namespace TimeTracker.API.Services;

public interface ITimeEntryService
{
    Task<List<TimeEntryResponse>> GetAllAsync();
    Task<List<TimeEntryResponse>> CreateAsync(TimeEntryCreateRequest createRequest);
    Task<List<TimeEntryResponse>?> UpdateAsync(int id, TimeEntryUpdateRequest updateRequest);
    Task<List<TimeEntryResponse>?> DeleteAsync(int id);
    Task<TimeEntryResponse?> GetAsync(int id);
    Task<List<TimeEntryByProjectResponse>?> GetByProjectAsync(int projectId);
}