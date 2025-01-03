namespace TimeTracker.Shared.Entities;

public class ProjectDetails
{
    public string? Description { get; set; }
    public DateTime? EndDate { get; set; }
    public int Id { get; set; }
    public required Project Project { get; set; }
    public int ProjectId { get; set; }
    public DateTime? StartDate { get; set; }
}