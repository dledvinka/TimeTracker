namespace TimeTracker.API.Services;

public interface ITimeEntryService
{
    List<TimeEntryResponse> GetAll();
    Task<List<TimeEntryResponse>> CreateAsync(TimeEntryCreateRequest createRequest);
    List<TimeEntryResponse>? Update(int id, TimeEntryUpdateRequest updateRequest);
    List<TimeEntryResponse>? Delete(int id);
    TimeEntryResponse? Get(int id);
}