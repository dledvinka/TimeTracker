﻿namespace TimeTracker.Shared.Models.TimeEntry;

public record struct TimeEntryByProjectResponse(int Id, DateTime Start, DateTime? End);