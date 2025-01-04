namespace TimeTracker.API.Services;

using Mapster;
using TimeTracker.API.Exceptions;

public class TimeEntryService : ITimeEntryService
{
    private readonly ITimeEntryRepository _timeEntryRepository;

    public TimeEntryService(ITimeEntryRepository timeEntryRepository) => _timeEntryRepository = timeEntryRepository;

    public async Task<List<TimeEntryResponse>> CreateAsync(TimeEntryCreateRequest createRequest)
    {
        var newEntry = createRequest.Adapt<TimeEntry>();
        var result = await _timeEntryRepository.CreateAsync(newEntry);

        return result.Adapt<List<TimeEntryResponse>>();
    }

    public async Task<List<TimeEntryResponse>?> DeleteAsync(int id)
    {
        var result = await _timeEntryRepository.DeleteAsync(id);

        return result?.Adapt<List<TimeEntryResponse>>();
    }

    public async Task<List<TimeEntryResponse>> GetAllAsync()
    {
        var result = await _timeEntryRepository.GetAllAsync();

        return result.Adapt<List<TimeEntryResponse>>();
    }

    public async Task<TimeEntryResponse?> GetAsync(int id)
    {
        var result = await _timeEntryRepository.GetAsync(id);

        return result?.Adapt<TimeEntryResponse>();
    }

    public async Task<List<TimeEntryByProjectResponse>?> GetByProjectAsync(int projectId)
    {
        var result = await _timeEntryRepository.GetByProjectAsync(projectId);

        return result.Adapt<List<TimeEntryByProjectResponse>>();
    }

    public async Task<List<TimeEntryResponse>?> UpdateAsync(int id, TimeEntryUpdateRequest updateRequest)
    {
        try
        {
            var updatedEntry = updateRequest.Adapt<TimeEntry>();
            var result = await _timeEntryRepository.UpdateAsync(id, updatedEntry);

            return result.Adapt<List<TimeEntryResponse>>();
        }
        catch (EntityNotFoundException)
        {
            return null;
        }
    }
}