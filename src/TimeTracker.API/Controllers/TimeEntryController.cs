namespace TimeTracker.API.Controllers;

using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TimeEntryController : ControllerBase
{
    private readonly ITimeEntryService _timeEntryService;

    public TimeEntryController(ITimeEntryService timeEntryService) => _timeEntryService = timeEntryService;

    [HttpGet]
    public ActionResult<List<TimeEntryResponse>> GetAllTimeEntries() => Ok(_timeEntryService.GetAll());

    [HttpGet("{id}")]
    public ActionResult<TimeEntryResponse> GetTimeEntry(int id)
    {
        var result = _timeEntryService.Get(id);

        if (result is null)
            return NotFound("TimeEntry with the given Id was not found");

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<List<TimeEntryResponse>>> CreateTimeEntryAsync(TimeEntryCreateRequest createRequest) => Ok(await _timeEntryService.CreateAsync(createRequest));

    [HttpPut("{id}")]
    public ActionResult<List<TimeEntryResponse>> UpdateTimeEntry(int id, TimeEntryUpdateRequest updateRequest)
    {
        var result = _timeEntryService.Update(id, updateRequest);

        if (result is null)
            return NotFound("TimeEntry with the given Id was not found");

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public ActionResult<List<TimeEntryResponse>> DeleteTimeEntry(int id)
    {
        var result = _timeEntryService.Delete(id);

        if (result is null)
            return NotFound("TimeEntry with the given Id was not found");

        return Ok(result);
    }
}