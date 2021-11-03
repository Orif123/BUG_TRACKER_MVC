using BugTrackerProj.Data;
using BugTrackerProj.Models;
using BugTrackerProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProject.Models
{
    public class Project
    {
        public string ProjectId { get; set; }
        [DisplayName("Project")]
        public string ProjectName { get; set; }
        [ForeignKey("Categories")]
        public ICollection<Category> Categories { get; set; }
        [ForeignKey("Users")]
        public ICollection<ApplicationUser> Users { get; set; }
        [ForeignKey("Bugs")]
        public ICollection<Bug> Bugs { get; set; }
    }
}
