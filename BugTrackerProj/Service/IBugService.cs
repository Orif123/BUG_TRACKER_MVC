using BugTrackerProj.ViewModels;
using BugTrackerProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.Service
{
    public interface IBugService
    {
        MainPageViewModel GetAllBugs(string id);
        MainPageViewModel GetBugsByCategory(string id);
        MainPageViewModel GetBugsByUser(string id);
    }
}
