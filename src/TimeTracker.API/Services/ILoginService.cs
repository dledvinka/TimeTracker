namespace TimeTracker.API.Services;

using TimeTracker.Shared.Models.Login;

public interface ILoginService
{
    Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
}