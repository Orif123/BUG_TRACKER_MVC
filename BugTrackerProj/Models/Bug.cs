﻿using BugTrackerProj.Data;
using BugTrackerProj.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProject.Models
{
    public class Bug
    {
        [Required]
        public string BugId { get; set; }
        [Required]
        public DateTime BugDate { get; set; }
        [Required]
        public string  Description { get; set; }
        [Required]
        public string RepoLink { get; set; }
        [ForeignKey("CategoryId")]
        public string CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("ProjectId")]
        public string ProjectId { get; set; }
        public virtual Project Project { get; set; }
        [ForeignKey("CommentId")]
        public ICollection<Comment> Comments { get; set; }
    }
}
