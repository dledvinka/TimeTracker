namespace TimeTracker.API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TimeEntry> TimeEntries { get; set; }
}