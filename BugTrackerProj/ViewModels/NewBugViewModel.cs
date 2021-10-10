using BugTrackerProj.Data;
using BugTrackerProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.ViewModels
{
    public class NewBugViewModel
    {
       
        [Required]
        public string BugId { get; set; }
        [Required]
        public DateTime BugDate { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public virtual Category Category { get; set; }
        [Required]
        public virtual ApplicationUser User { get; set; }
        [Required]
        public virtual Project Project { get; set; }
    }
}
