using BugTrackerProj.Models;
using BugTrackerProject.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackerProj.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public ICollection<Bug> Bugs { get; set; }
        public ICollection<Comment> comments { get; set; }
        [MaxLength(450)]
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
        public string ProjectId { get; set; }
        public string PhotoPath { get; set; }
        public string Role { get; set; }
    }
}
