﻿using System;
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
        [DisplayName("Category: ")]
        public string CategoryId { get; set; }
        [Required]
        [DisplayName("Description: ")]
        public string Description { get; set; }
        [Required]
        [Bindable(true)]
        [DisplayName("Repository Link: ")]
        public string RepoLink { get; set; }
    }
    public class NewCategoryViewModel
    {
        [Required]
        [DisplayName("Category Name: ")]
        public string CategoryName { get; set; }
        [Required]
        [DisplayName("Project: ")]
        public string ProjectId { get; set; }
    }
}
