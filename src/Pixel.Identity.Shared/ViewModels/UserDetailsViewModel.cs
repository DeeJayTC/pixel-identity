﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pixel.Identity.Shared.ViewModels
{
    public class UserDetailsViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public bool LockoutEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }
      
        [Required]
        public List<UserRoleViewModel> UserRoles { get; set; } = new ();
    }
}
