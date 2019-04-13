﻿using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyForum_Backend.Models
{
    // Models returned by AccountController actions.

    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class ManageInfoViewModel
    {
        public string LocalLoginProvider { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class UserInfoViewModel
    {
        public string Login { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<IdentityUserRole> Roles { get; set; }

        public bool LockoutEnabled { get; set; }
    }

    public class UserLoginInfoViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}
