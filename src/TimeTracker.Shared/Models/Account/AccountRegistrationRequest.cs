﻿namespace TimeTracker.Shared.Models.Account;

using System.ComponentModel.DataAnnotations;

public class AccountRegistrationRequest
{
    [Required]
    public string UserName { get; set; } = string.Empty;
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required, MinLength(6)]
    public string Password { get; set; } = string.Empty;
    [Required, Compare(nameof(Password), ErrorMessage = "The passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}