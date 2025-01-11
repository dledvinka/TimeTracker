namespace TimeTracker.Shared.Entities;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    public List<Project> Projects { get; set; } = [];
    public List<TimeEntry> TimeEntries { get; set; } = [];
}