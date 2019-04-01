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
        public string userId { get; set; }

        public IdentityUser user { get; set; }

        public List<IdentityRole> roles { get; set; }
    }
}
