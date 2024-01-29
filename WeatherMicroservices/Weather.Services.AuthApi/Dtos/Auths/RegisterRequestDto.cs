﻿using System.ComponentModel.DataAnnotations;

namespace Weather.Services.AuthApi.Dtos.Auths;

public class RegisterRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    public string? Role { get; set; }
}
