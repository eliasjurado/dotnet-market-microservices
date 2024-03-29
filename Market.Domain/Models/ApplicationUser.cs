﻿using Microsoft.AspNetCore.Identity;

namespace Market.Domain.Models
{
    public sealed class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
