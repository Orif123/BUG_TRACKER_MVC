using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.ViewModels
{
    public class UserToRoleRegistraionViewModel
    {
        [Required]
        public string RoleName { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
