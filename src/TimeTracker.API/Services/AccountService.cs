using Microsoft.AspNetCore.Identity;
using TimeTracker.Shared.Models.Account;

namespace TimeTracker.API.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
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

    public async Task AssignRole(string userName, string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
            await _roleManager.CreateAsync(new IdentityRole(roleName));

        var user = await _userManager.FindByNameAsync(userName);
        await _userManager.AddToRoleAsync(user!, roleName);
    }
}
