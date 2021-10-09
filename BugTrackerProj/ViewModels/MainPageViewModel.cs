using BugTrackerProj.Data;
using BugTrackerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.ViewModels
{
    public class MainPageViewModel
    {
        public IEnumerable<Bug> Bugs { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
