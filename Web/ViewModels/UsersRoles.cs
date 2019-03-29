using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class UsersRoles
    {
        public IdentityUser User { get; set; }
        public IdentityRole Role { get; set; }
    }
}