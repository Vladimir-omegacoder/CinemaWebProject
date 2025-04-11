using System;
using System.Collections.Generic;

namespace Server.Models.DbEntity;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Username { get; set; } = null!;

    public virtual Customer? Customer { get; set; }
}
