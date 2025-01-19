namespace TimeTracker.Client.Services;

using System.Net.Http.Json;
using Blazored.Toast.Services;
using TimeTracker.Shared.Models.Account;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IToastService _toastService;

    public AuthService(HttpClient httpClient, IToastService toastService)
    {
        _httpClient = httpClient;
        _toastService = toastService;
    }

    public async Task Register(AccountRegistrationRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("api/account", request);

        if (result is not null)
        {
            var response = await result.Content.ReadFromJsonAsync<AccountRegistrationResponse>();

            if (!response.IsSuccess && response.Errors is not null)
            {
                foreach (var responseError in response.Errors)
                {
                    _toastService.ShowError(responseError);
                }
            }
            else if (!response.IsSuccess)
            {
                _toastService.ShowError("An error occurred while processing your request");
            }
            else
            {
                _toastService.ShowSuccess("Account created successfully");
            }
        }
    }
}
