namespace TimeTracker.API.Services;

public interface IUserContextService
{
    public string? GetUserId();

    public Task<User?> GetUserAsync();
}
