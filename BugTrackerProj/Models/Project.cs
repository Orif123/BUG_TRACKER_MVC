using BugTrackerProj.Data;
using BugTrackerProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProject.Models
{
    public class Project
    {
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        [ForeignKey("CategoryId")]
        public ICollection<Category> Categories { get; set; }
        [ForeignKey("UserId")]
        public ICollection<ApplicationUser> Users { get; set; }
    }
}
