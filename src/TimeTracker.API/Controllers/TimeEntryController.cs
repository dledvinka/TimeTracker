namespace TimeTracker.API.Controllers;

using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TimeEntryController : ControllerBase
{
    private readonly ITimeEntryService _timeEntryService;

    public TimeEntryController(ITimeEntryService timeEntryService) => _timeEntryService = timeEntryService;

    [HttpGet]
    public async Task<ActionResult<List<TimeEntryResponse>>> GetAllTimeEntriesAsync() => Ok(await _timeEntryService.GetAllAsync());

    [HttpGet("project/{projectId}")]
    public async Task<ActionResult<List<TimeEntryByProjectResponse>>> GetByProjectAsync(int projectId) => Ok(await _timeEntryService.GetByProjectAsync(projectId));

    [HttpGet("{id}")]
    public async Task<ActionResult<TimeEntryResponse>> GetTimeEntryAsync(int id)
    {
        var result = await _timeEntryService.GetAsync(id);

        if (result is null)
            return NotFound("TimeEntry with the given Id was not found");

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<List<TimeEntryResponse>>> CreateTimeEntryAsync(TimeEntryCreateRequest createRequest) =>
        Ok(await _timeEntryService.CreateAsync(createRequest));

    [HttpPut("{id}")]
    public async Task<ActionResult<List<TimeEntryResponse>>> UpdateTimeEntryAsync(int id, TimeEntryUpdateRequest updateRequest)
    {
        var result = await _timeEntryService.UpdateAsync(id, updateRequest);

        if (result is null)
            return NotFound("TimeEntry with the given Id was not found");

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<TimeEntryResponse>>> DeleteTimeEntryAsync(int id)
    {
        var result = await _timeEntryService.DeleteAsync(id);

        if (result is null)
            return NotFound("TimeEntry with the given Id was not found");

        return Ok(result);
    }
}