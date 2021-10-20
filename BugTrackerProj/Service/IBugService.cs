using BugTrackerProj.Data;
using BugTrackerProj.ViewModels;
using BugTrackerProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.Service
{
    public interface IBugService
    {
        MainPageViewModel GetAllBugs( MainPageViewModel model, string projectid);
        void NewBug(Bug bug);
        List<SelectListItem> GetCategories(string id);
        List<string> GetUsers();
        List<SelectListItem> GetProjects();
        List<Project> GetRealProjects();
        List<ApplicationUser> GetRealUsers();
        List<Bug> GetBugsByCategoryId(string id);
        void BugSolved(string id);
        BugCommentDetailsViewModel GetDetails(string id);
        void AddComment(BugCommentDetailsViewModel model);
        IEnumerable<Bug> GetAllBugs();
        List<Bug> GetBugsByProject(string projectid);
    }
}
