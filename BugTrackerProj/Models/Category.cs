using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProject.Models
{
    public class Category
    {
        public string CategoryId { get; set; }
        [DisplayName("Category")]
        [Required]
        public string CtaegoryName { get; set; }
        [MaxLength(450)]
        [DisplayName("Project")]
        [Required]
        public string ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
        public virtual ICollection<Bug> Bugs { get; set; }
    }
}
