using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.ViewModels
{
    public class NewBugViewModel
    {
        [Required]
        [DisplayName("Category")]
        public string CategoryId { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
