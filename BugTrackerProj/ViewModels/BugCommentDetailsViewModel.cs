using BugTrackerProj.Models;
using BugTrackerProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.ViewModels
{
    public class BugCommentDetailsViewModel
    {
        [Required]
        [DisplayName("Comment")]
        public string CommentText { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Bug> Bug { get; set; }
    }
}
