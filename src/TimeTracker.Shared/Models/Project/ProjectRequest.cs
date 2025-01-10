namespace TimeTracker.Shared.Models.Project;

using System.ComponentModel.DataAnnotations;

public class ProjectRequest
{
    public string Description { get; set; }
    public DateTime? EndDate { get; set; }
    [Required]
    public string Name { get; set; }
    public DateTime? StartDate { get; set; }
}