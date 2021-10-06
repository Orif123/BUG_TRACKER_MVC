using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProject.Models
{
    public class Category
    {
        public string CategoryId { get; set; }
        public string CtaegoryName { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
        [ForeignKey("BugId")]
        public virtual ICollection<Bug> Bugs { get; set; }
    }
}
