namespace TimeTracker.API.Services;

public interface ITimeEntryService
{
    List<TimeEntryResponse> GetAll();
    List<TimeEntryResponse> Create(TimeEntryCreateRequest createRequest);
    List<TimeEntryResponse>? Update(int id, TimeEntryUpdateRequest updateRequest);
    List<TimeEntryResponse>? Delete(int id);
    TimeEntryResponse? Get(int id);
}