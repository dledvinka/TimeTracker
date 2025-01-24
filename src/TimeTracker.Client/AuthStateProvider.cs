namespace TimeTracker.Client;

using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var authToken = await _localStorage.GetItemAsync<string>("authToken");
        AuthenticationState authState;

        if (string.IsNullOrWhiteSpace(authToken))
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
            authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        else
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(authToken), "jwt")));
        }

        NotifyAuthenticationStateChanged(Task.FromResult(authState));

        return authState;
    }

    // From Steve Sanderson, Microsoft
    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }

        return Convert.FromBase64String(base64);
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwtAuthToken)
    {
        var payload = jwtAuthToken.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        var claims = new List<Claim>();

        foreach (var kvp in keyValuePairs)
        {
            if (kvp.Value is JsonElement element)
            {
                if (element.ValueKind == JsonValueKind.Array)
                {
                    claims.AddRange(element.EnumerateArray().Select(x => new Claim(kvp.Key, x.ToString())));
                }
                else
                {
                    claims.Add(new Claim(kvp.Key, element.ToString()));
                }
            }
            else
            {
                claims.Add(new Claim(kvp.Key, kvp.Value.ToString()));
            }
        }

        return claims;
    }
}