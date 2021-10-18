using BugTrackerProject.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackerProj.Data
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public ICollection<Bug> Bugs { get; set; }
        public virtual Project Project { get; set; }
        [MaxLength(450)]
        [ForeignKey("ProjectId")]
        public string ProjectId { get; set; }
    }
}
