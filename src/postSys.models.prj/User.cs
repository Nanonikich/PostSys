﻿using System;
using System.Collections.Generic;

namespace PostSys.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserSurname { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string? UserPatronymic { get; set; }

    public string UserEmail { get; set; } = null!;

    public string UserPhone { get; set; } = null!;

    public string UserCity { get; set; } = null!;

    public string UserUsername { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public int UserRole { get; set; }

    public virtual Role UserRoleNavigation { get; set; } = null!;
}
