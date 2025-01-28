using Microsoft.AspNetCore.Components;

namespace TimeTracker.Client.Services;

using System.Net.Http.Json;
using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components.Authorization;
using TimeTracker.Shared.Models.Account;
using TimeTracker.Shared.Models.Login;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly IToastService _toastService;
    private readonly NavigationManager _navigationManager;
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationStateProvider _authStateProvider;

    public AuthService(HttpClient httpClient, 
                       IToastService toastService, 
                       NavigationManager navigationManager, 
                       ILocalStorageService localStorage, 
                       AuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _toastService = toastService;
        _navigationManager = navigationManager;
        _localStorage = localStorage;
        _authStateProvider = authStateProvider;
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("api/login", request);

        if (result is not null)
        {
            var response = await result.Content.ReadFromJsonAsync<LoginResponse>();

            if (response.IsSuccess)
            {
                if (response.Token is not null)
                {
                    await _localStorage.SetItemAsStringAsync("authToken", response.Token);
                    await _authStateProvider.GetAuthenticationStateAsync();
                }

                _navigationManager.NavigateTo("/time-entries");
            }

            return response;
        }

        return new LoginResponse(false, "An error occurred while processing your request");
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        await _authStateProvider.GetAuthenticationStateAsync();
        _navigationManager.NavigateTo("/login");
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
