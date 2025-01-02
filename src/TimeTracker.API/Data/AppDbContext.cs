namespace TimeTracker.API.Data;

using Microsoft.EntityFrameworkCore;
using TimeTracker.Shared.Entities;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TimeEntry> TimeEntries { get; set; }
}