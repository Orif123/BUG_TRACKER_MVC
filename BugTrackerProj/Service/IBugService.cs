using BugTrackerProj.Data;
using BugTrackerProj.Models;
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
        MainPageViewModel GetAllUserBugs( MainPageViewModel model, string projectid);
        MainPageViewModel GetAllBugsManager(MainPageViewModel model);
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
        MainPageViewModel CountProjectBugs(string projectid);
        MainPageViewModel CountAllBugs();
        List<SelectListItem> GetUserRoles();
        List<SelectListItem> GetUserNames();
        List<ApplicationUser> GetUserByProject(string projectid);
        UpdateUserViewModel FindUserById(string id);
        ApplicationUser GetUserById(string id);
        UserBugsViewModel GetBugsByUserId(string id);
        void NewCategory(NewCategoryViewModel model);
        void DeleteCategory(string id);
        void NewProject(Project project);
        void DeleteProject(string id);
        List<Category> GetAllCategories();
        List<SelectListItem> ListItemCategories();
    }
}
