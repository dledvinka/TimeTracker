namespace TimeTracker.API.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TimeTracker.Shared.Models.Login;

public class LoginService : ILoginService
{
    private readonly IConfiguration _configuration;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public LoginService(SignInManager<User> signInManager, IConfiguration configuration, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
    {
        var result = await _signInManager.PasswordSignInAsync(loginRequest.UserName, loginRequest.Password, false, false);

        if (!result.Succeeded)
            return new LoginResponse(false, "Invalid username or password");

        var user = await _userManager.FindByNameAsync(loginRequest.UserName);

        if (user == null)
            return new LoginResponse(false, "User doesn't exist");

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, loginRequest.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSigningKey"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));
        var token = new JwtSecurityToken(issuer: _configuration["JwtIssuer"],
                                         audience: _configuration["JwtAudience"],
                                         claims: claims,
                                         expires: expiry,
                                         signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return new LoginResponse(true, null, jwt);
    }
}