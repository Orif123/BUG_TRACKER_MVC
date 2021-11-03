using BugTrackerProj.Data;
using BugTrackerProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.Models
{
    public class Comment
    {
        public string CommentId { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime When { get; set; }
        public virtual Bug Bug { get; set; }
        [ForeignKey("BugId")]
        public string BugId { get; set; }
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("UserId")]
        public string  UserId { get; set; }
    }
}
