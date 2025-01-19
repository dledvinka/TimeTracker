namespace TimeTracker.Shared.Models.Login;

public record struct LoginResponse(bool IsSuccess, string? Error = null, string? Token = null);