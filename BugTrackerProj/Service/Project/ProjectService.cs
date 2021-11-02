using BugTrackerProj.Data;
using BugTrackerProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.Service
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _context;
        private readonly IBugService _bugService;
        public ProjectService(ApplicationDbContext context, IBugService bugService)
        {
            _context = context;
            _bugService = bugService;
        }
        public void NewProject(Project project)
        {
            project.ProjectId = Guid.NewGuid().ToString();
            _context.Projects.Add(project);
            _context.SaveChanges();
        }
        public void DeleteProject(string id)
        {
            var project = _context.Projects.SingleOrDefault(p => p.ProjectId == id);
            var bugs = _context.Bugs.Where(c => c.ProjectId == project.ProjectId).ToList();
            foreach (var bug in bugs)
            {
                _bugService.BugSolved(bug.BugId);
            }
            var categories = _context.Categories.Where(c => c.ProjectId == project.ProjectId);
            var users = _context.Users.Where(c => c.ProjectId == project.ProjectId);
            _context.Categories.RemoveRange(categories);
            foreach (var user in users)
            {
                user.ProjectId = "NoProject";
            }
            _context.Remove(project);
            _context.SaveChanges();
        }
        public List<SelectListItem> GetProjects()
        {
            var projlist = _context.Projects.ToList();
            var selectlist = new List<SelectListItem>();
            foreach (var item in projlist)
            {
                if(item.ProjectId !="NoProject")
                {
                    selectlist.Add(new SelectListItem { Text = item.ProjectName, Value = item.ProjectId });
                }
            }
            return selectlist;
        }
        public List<Project> GetRealProjects()
        {
            var project = _context.Projects.Where(p => p.ProjectId == "NoProject");
            var list = _context.Projects.Except(project).ToList();
            return list;
        }
    }
}
