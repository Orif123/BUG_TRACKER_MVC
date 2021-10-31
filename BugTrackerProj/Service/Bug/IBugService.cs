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
        List<Bug> GetBugsByCategoryId(string id);
        void BugSolved(string id);
        BugCommentDetailsViewModel GetDetails(string id);
        void AddComment(BugCommentDetailsViewModel model);
        UserBugsViewModel GetBugsByUserId(string id);
        IEnumerable<Bug> GetAllBugs();
        List<Bug> GetBugsByProject(string projectid);
        MainPageViewModel CountProjectBugs(string projectid);
        MainPageViewModel CountAllBugs();
       
        
        
    }
}
