namespace TimeTracker.API.Services;

using Mapster;
using TimeTracker.API.Repositories;
using TimeTracker.Shared.Entities;
using TimeTracker.Shared.Models.TimeEntry;

public class TimeEntryService : ITimeEntryService
{
    private readonly ITimeEntryRepository _timeEntryRepository;

    public TimeEntryService(ITimeEntryRepository timeEntryRepository) => _timeEntryRepository = timeEntryRepository;

    public List<TimeEntryResponse> Create(TimeEntryCreateRequest createRequest)
    {
        var newEntry = createRequest.Adapt<TimeEntry>();
        var result = _timeEntryRepository.Create(newEntry);

        return result.Adapt<List<TimeEntryResponse>>();
    }

    public List<TimeEntryResponse>? Update(int id, TimeEntryUpdateRequest updateRequest)
    {
        var updatedEntry = updateRequest.Adapt<TimeEntry>();
        var result = _timeEntryRepository.Update(id, updatedEntry);

        return result?.Adapt<List<TimeEntryResponse>>();
    }

    public List<TimeEntryResponse>? Delete(int id)
    {
        var result = _timeEntryRepository.Delete(id);

        return result?.Adapt<List<TimeEntryResponse>>();
    }

    public List<TimeEntryResponse> GetAll()
    {
        var result = _timeEntryRepository.GetAll();

        return result.Adapt<List<TimeEntryResponse>>();
    }

    public TimeEntryResponse? Get(int id)
    {
        var result = _timeEntryRepository.Get(id);

        return result?.Adapt<TimeEntryResponse>();
    }
}