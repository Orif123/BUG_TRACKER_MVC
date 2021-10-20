using BugTrackerProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.Models
{
    public class Comment
    {
        public string CommentId { get; set; }
        public string Text { get; set; }
        public virtual Bug Bug { get; set; }
        [ForeignKey("BugId")]
        public string BugId { get; set; }
    }
}
