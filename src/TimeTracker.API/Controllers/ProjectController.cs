namespace TimeTracker.API.Controllers;

using Microsoft.AspNetCore.Mvc;
using TimeTracker.Shared.Models.Project;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService) => _projectService = projectService;

    [HttpGet]
    public async Task<ActionResult<List<ProjectResponse>>> GetAllAsync() => Ok(await _projectService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectResponse>> GetAsync(int id)
    {
        var result = await _projectService.GetAsync(id);

        if (result is null)
            return NotFound("Project with the given Id was not found");

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<List<ProjectResponse>>> CreateAsync(ProjectCreateRequest createRequest) =>
        Ok(await _projectService.CreateAsync(createRequest));

    [HttpPut("{id}")]
    public async Task<ActionResult<List<ProjectResponse>>> UpdateAsync(int id, ProjectUpdateRequest updateRequest)
    {
        var result = await _projectService.UpdateAsync(id, updateRequest);

        if (result is null)
            return NotFound("Project with the given Id was not found");

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<ProjectResponse>>> DeleteAsync(int id)
    {
        var result = await _projectService.DeleteAsync(id);

        if (result is null)
            return NotFound("Project with the given Id was not found");

        return Ok(result);
    }
}