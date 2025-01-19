namespace TimeTracker.Shared.Models.Account;

public record struct AccountRegistrationResponse(bool IsSuccess, IEnumerable<string>? Errors = null);