﻿namespace TimeTracker.Shared.Entities;

public class Project : SoftDeletableEntity
{
    public required string Name { get; set; }
    public List<TimeEntry> TimeEntries { get; set; } = [];

    public ProjectDetails? ProjectDetails { get; set; }
}