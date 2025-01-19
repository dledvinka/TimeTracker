namespace TimeTracker.Client.Services;

using TimeTracker.Shared.Models.Account;

public interface IAuthService
{
    Task Register(AccountRegistrationRequest request);
}
