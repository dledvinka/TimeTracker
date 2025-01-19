using Microsoft.AspNetCore.Identity;
using TimeTracker.Shared.Models.Account;

namespace TimeTracker.API.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;

    public AccountService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<AccountRegistrationResponse> RegisterAsync(AccountRegistrationRequest request)
    {
        var user = new User() { Email = request.Email, UserName = request.UserName, EmailConfirmed = true};
        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return new AccountRegistrationResponse(false, errors);
        }

        return new AccountRegistrationResponse(true);
    }
}
