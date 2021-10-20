using BugTrackerProj.Data;
using BugTrackerProj.Models;
using BugTrackerProj.ViewModels;
using BugTrackerProject.Models;
using Microsoft.AspNetCore.Http;
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
        public BugService(ApplicationDbContext context, IHttpContextAccessor contextAccessor)
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
            };
            _context.Comments.Add(comment);
        }

        public void BugSolved(string id)
        {
            var bug = _context.Bugs.SingleOrDefault(b => b.BugId == id);
            _context.Remove(bug);
            _context.SaveChanges();
        }

        public MainPageViewModel GetAllBugs(MainPageViewModel model, string userid = "")
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userid);
            var project = _context.Projects.SingleOrDefault(p => p.ProjectId == user.ProjectId);
            if (model.CategoryId == "" || model.CategoryId == null)
            {
                
                model.Bugs = _context.Bugs.Where(b => b.ProjectId == user.ProjectId).Include(u=>u.Category).Include(u=>u.Project).Include(u=>u.User);
                return model;
            }
            model.Bugs = GetBugsByCategoryId(model.CategoryId);
            return model;
        }
        
        public List<Bug> GetBugsByCategoryId(string id)
        {
            var list = _context.Bugs.FromSqlRaw($"select * from Bugs Where bugs.categoryid={id}").Include(p => p.Category).Include(P => P.Project).Include(P => P.User).ToList();
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

        public BugCommentDetailsViewModel GetDetails(string id)
        {
            var model = new BugCommentDetailsViewModel();
            model.Bug = _context.Bugs.Where(p=>p.BugId==id).Include(c=>c.Category).Include(c=>c.User).Include(c=>c.Project).ToList();
            model.Comments = _context.Comments.FromSqlRaw($"select * from Comments where comments.bugid={id}");
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
            bug.BugId = "ra";
            bug.BugDate = DateTime.Now;
            var user = GetRealUsers().SingleOrDefault(u => u.Id == bug.UserId);
            bug.ProjectId = user.ProjectId;
            _context.Bugs.Add(bug);
            _context.SaveChanges();
        }




    }
}

