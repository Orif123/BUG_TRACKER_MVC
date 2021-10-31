using BugTrackerProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.Service
{
    public interface IProjectService
    {
        List<SelectListItem> GetProjects();
        List<Project> GetRealProjects();
        void NewProject(Project project);
        void DeleteProject(string id);
    }
}
