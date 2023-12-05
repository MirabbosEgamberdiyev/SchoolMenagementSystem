﻿using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Models;

public class User : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}
