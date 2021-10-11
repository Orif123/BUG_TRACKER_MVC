using BugTrackerProj.Data;
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
        MainPageViewModel GetAllBugs( string searchtext, string projectid);
        void NewBug(Bug bug);
        List<string> GetCategories();
        List<string> GetUsers();
        List<string> GetProjects();
        List<Project> GetRealProjects();
        List<ApplicationUser> GetRealUsers();
    }
}
