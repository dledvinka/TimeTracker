namespace TimeTracker.API.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TimeEntryController : ControllerBase
{
    private readonly ITimeEntryService _timeEntryService;

    public TimeEntryController(ITimeEntryService timeEntryService) => _timeEntryService = timeEntryService;

    [HttpGet]
    public async Task<ActionResult<List<TimeEntryResponse>>> GetAllTimeEntriesAsync()
    {
        var userName = HttpContext.User.Identity!.Name;
        return Ok(await _timeEntryService.GetAllAsync());
    }

    [HttpGet("project/{projectId}")]
    public async Task<ActionResult<List<TimeEntryResponse>>> GetByProjectAsync(int projectId) => Ok(await _timeEntryService.GetByProjectAsync(projectId));

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

    [HttpGet("year/{year}")]
    public async Task<ActionResult<List<TimeEntryResponse>>> GetByYearAsync(int year) => Ok(await _timeEntryService.GetByYearAsync(year));

    [HttpGet("month/{year}/{month}")]
    public async Task<ActionResult<List<TimeEntryResponse>>> GetByMonthAsync(int year, int month) => Ok(await _timeEntryService.GetByMonthAsync(year, month));

    [HttpGet("day/{year}/{month}/{day}")]
    public async Task<ActionResult<List<TimeEntryResponse>>> GetByDayAsync(int year, int month, int day) => Ok(await _timeEntryService.GetByDayAsync(year, month, day));
}