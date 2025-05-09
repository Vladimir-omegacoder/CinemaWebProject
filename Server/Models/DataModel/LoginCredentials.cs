﻿using System.ComponentModel.DataAnnotations;

namespace Server.Models.DataModel;

public class LoginCredentials
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
