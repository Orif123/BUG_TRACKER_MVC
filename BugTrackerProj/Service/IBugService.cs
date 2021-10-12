﻿using BugTrackerProj.Data;
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
        MainPageViewModel GetAllBugs( string searchtext, string projectid);
        void NewBug(Bug bug);
        List<SelectListItem> GetCategories(string id);
        List<string> GetUsers();
        List<SelectListItem> GetProjects();
        List<Project> GetRealProjects();
        List<ApplicationUser> GetRealUsers();
    }
}
