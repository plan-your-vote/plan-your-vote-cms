using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class UserRoleViewModel
    {
        [Key]
        public string UserId { get; set; }

        public IdentityUser User { get; set; }

        public List<IdentityRole> Roles { get; set; }
    }
}
