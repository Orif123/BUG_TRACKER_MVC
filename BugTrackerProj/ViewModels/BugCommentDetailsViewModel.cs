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
        [DisplayName("Bug Id")]
        public string BugId { get; set; }
        public string UserId { get; set; }
    }
    public class UserBugsViewModel
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<Bug> Bugs { get; set; }
        public int BugCounter { get; set; }
    }
}
