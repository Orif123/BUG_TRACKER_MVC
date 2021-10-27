using BugTrackerProj.Data;
using BugTrackerProj.Models;
using BugTrackerProj.ViewModels;
using BugTrackerProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BugTrackerProj.Service
{
    public class BugService : IBugService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly ApplicationDbContext _context;
        public BugService(ApplicationDbContext context, IHttpContextAccessor contextAccessor, UserManager <ApplicationUser> userManager)
        {
            _httpContext = contextAccessor;
            _context = context;
        }
        public void AddComment(BugCommentDetailsViewModel model)
        {
            var comment = new Comment()
            {
                CommentId = Guid.NewGuid().ToString(),
                Text = model.CommentText,
                BugId = model.BugId,
                UserId = model.UserId
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
        public void BugSolved(string id)
        {
            var bug = _context.Bugs.SingleOrDefault(b => b.BugId == id);
            var comments = _context.Comments.Where(c => c.BugId == bug.BugId).ToList();
            _context.Comments.RemoveRange(comments);
            _context.SaveChanges();
            _context.Remove(bug);
            _context.SaveChanges();
        }
        public MainPageViewModel GetAllUserBugs(MainPageViewModel model, string userid = "")
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userid);
            var project = _context.Projects.SingleOrDefault(p => p.ProjectId == user.ProjectId);
            if (model.CategoryId == "" || model.CategoryId == null)
            {

                model.Bugs = _context.Bugs.Where(b => b.ProjectId == user.ProjectId).Include(u => u.Category).Include(u => u.Project).Include(u => u.User);
                return model;
            }
            model.Bugs = GetBugsByCategoryId(model.CategoryId);
            return model;
        }
        public MainPageViewModel GetAllBugsManager(MainPageViewModel model)
        {
            if (model.CategoryId == "" || model.CategoryId == null)
            {

                model.Bugs = GetAllBugs();
                return model;
            }
            model.Bugs = GetBugsByCategoryId(model.CategoryId);
            return model;
        }
        public List<Bug> GetBugsByCategoryId(string id)
        {
            var list = _context.Bugs.Where(b=>b.CategoryId==id).Include(p => p.Category).Include(P => P.Project).Include(P => P.User).ToList();
            return list;
        }
        public List<SelectListItem> GetCategories(string id)
        {
            var categolist = _context.Categories.ToList();
            var selectlist = new List<SelectListItem>();
            foreach (var item in categolist)
            {
                if (item.ProjectId == id)
                {
                    selectlist.Add(new SelectListItem { Text = item.CtaegoryName, Value = item.CategoryId });
                }
            }
            return selectlist;
        }
        public List<SelectListItem> ListItemCategories()
        {
            var categolist = _context.Categories.ToList();
            var selectlist = new List<SelectListItem>();
            foreach (var item in categolist)
            {
                selectlist.Add(new SelectListItem { Text = item.CtaegoryName, Value = item.CategoryId });
            }
            return selectlist;
        }
        public BugCommentDetailsViewModel GetDetails(string id)
        {
            var model = new BugCommentDetailsViewModel();
            model.Bug = _context.Bugs.Where(p => p.BugId == id).Include(c => c.Category).Include(c => c.User).Include(c => c.Project).ToList();
            model.Comments = _context.Comments.Where(c => c.BugId == id).Include(u => u.User);
            return model;
        }
        public List<SelectListItem> GetProjects()
        {
            var projlist = _context.Projects.ToList();
            var selectlist = new List<SelectListItem>();
            foreach (var item in projlist)
            {
                selectlist.Add(new SelectListItem { Text = item.ProjectName, Value = item.ProjectId });
            }
            return selectlist;
        }
        public List<Project> GetRealProjects()
        {
            var list = _context.Projects.ToList();
            return list;
        }
        public List<ApplicationUser> GetRealUsers()
        {
            var list = _context.Users.ToList();
            return list;
        }
        public List<string> GetUsers()
        {
            var list = _context.Users.Select(list => list.Id).ToList();
            return list;
        }
        public void NewBug(Bug bug)
        {
            bug.BugId = Guid.NewGuid().ToString();
            bug.BugDate = DateTime.Now;
            var user = GetRealUsers().SingleOrDefault(u => u.Id == bug.UserId);
            bug.ProjectId = user.ProjectId;
            _context.Bugs.Add(bug);
            _context.SaveChanges();
        }
        public IEnumerable<Bug> GetAllBugs()
        {
            var list = _context.Bugs.Include(p => p.Category).Include(P => P.User).ToList();
            return list;
        }
        public List<Bug> GetBugsByProject(string projectid)
        {
            var list = _context.Bugs.Where(p => p.ProjectId == projectid).Include(p => p.Category).Include(P => P.User).ToList();
            return list;
        }
        public MainPageViewModel CountProjectBugs(string projectid)
        {
            var model = new MainPageViewModel();
            model.BugCounter = GetBugsByProject(projectid).Count();
            return model;
        }
        public MainPageViewModel CountAllBugs()
        {
            var model = new MainPageViewModel();
            model.BugCounter = GetAllBugs().Count();
            return model;
        }
        public List<SelectListItem> GetUserRoles()
        {
            var roleList = _context.Roles.ToList();
            var selectedlist = new List<SelectListItem>();
            foreach (var item in roleList)
            {
                selectedlist.Add(new SelectListItem { Value = item.Name, Text = item.Name });
            }
            return selectedlist;
        }
        public List<SelectListItem> GetUserNames()
        {
            var roleList = _context.Users.ToList();
            var selectedlist = new List<SelectListItem>();
            foreach (var item in roleList)
            {
                selectedlist.Add(new SelectListItem { Value = item.UserName, Text = item.UserName });
            }
            return selectedlist;
        }
        public List<ApplicationUser> GetUserByProject(string projectid)
        {
            var list = _context.Users.Where(u => u.ProjectId == projectid).Include(u => u.Project).ToList();
            return list;
        }
        public UpdateUserViewModel FindUserById(string id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            var model = new UpdateUserViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProjectId = user.ProjectId,
            };
            return model;
        }
        public ApplicationUser GetUserById(string id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            return user;
        }
        public UserBugsViewModel GetBugsByUserId(string id)
        {
            var model = new UserBugsViewModel();
            model.User = _context.Users.Include(p=>p.Project).SingleOrDefault(u => u.Id == id);
            model.Bugs = _context.Bugs.Where(b => b.UserId == id).Include(b=>b.Category).ToList();
            model.BugCounter = model.Bugs.Count();
            return model;
        }

        public void NewCategory(NewCategoryViewModel model)
        {
            var category = new Category()
            {
                CategoryId = Guid.NewGuid().ToString(),
                CtaegoryName=model.CategoryName,
                ProjectId=model.ProjectId
        };
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(string id)
        {
           var category= _context.Categories.SingleOrDefault(p => p.CategoryId == id);
            var bugs=_context.Bugs.Where(c => c.CategoryId == category.CategoryId).ToList();
            foreach (var bug in bugs)
            {
                BugSolved(bug.BugId);
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
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
                BugSolved(bug.BugId);
            }
            var categories = _context.Categories.Where(c => c.ProjectId == project.ProjectId);
            var users = _context.Users.Where(c => c.ProjectId == project.ProjectId);
            _context.Categories.RemoveRange(categories);
            _context.Users.RemoveRange(users);
            _context.Remove(project);
            _context.SaveChanges();
        }
        public List<Category> GetAllCategories()
        {
            var list = _context.Categories.Include(p=>p.Project).ToList();
            return list;
        }
    }
}

