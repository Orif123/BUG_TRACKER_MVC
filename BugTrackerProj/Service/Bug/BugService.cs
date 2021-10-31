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
        public void BugSolved(string id)
        {
            var bug = _context.Bugs.SingleOrDefault(b => b.BugId == id);
            var comments = _context.Comments.Where(c => c.BugId == bug.BugId).ToList();
            _context.Comments.RemoveRange(comments);
            _context.SaveChanges();
            _context.Remove(bug);
            _context.SaveChanges();
        }
        public void AddComment(BugCommentDetailsViewModel model)
        {
            var comment = new Comment()
            {
                CommentId = Guid.NewGuid().ToString(),
                When=DateTime.Now,
                Text = model.CommentText,
                BugId = model.BugId,
                UserId = model.UserId,
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
        public MainPageViewModel GetAllUserBugs(MainPageViewModel model, string userid = "")
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userid);
            var project = _context.Projects.SingleOrDefault(p => p.ProjectId == user.ProjectId);
            if (model.CategoryId == "" || model.CategoryId == null)
            {

                model.Bugs = _context.Bugs.Where(b => b.ProjectId == user.ProjectId).OrderByDescending(b=>b.BugDate).Include(u => u.Category).Include(u => u.Project).Include(u => u.User);
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
            var list = _context.Bugs.Where(b=>b.CategoryId==id).OrderByDescending(b=>b.BugDate).Include(p => p.Category).Include(P => P.Project).Include(P => P.User).ToList();
            return list;
        }
        public BugCommentDetailsViewModel GetDetails(string id)
        {
            var model = new BugCommentDetailsViewModel();
            model.Bug = _context.Bugs.Where(p => p.BugId == id).Include(c => c.Category).Include(c => c.User).Include(c => c.Project).ToList();
            model.Comments = _context.Comments.Where(c => c.BugId == id).OrderByDescending(d=>d.When).Include(u => u.User);
            return model;
        }
        public void NewBug(Bug bug)
        {
            bug.BugId = Guid.NewGuid().ToString();
            bug.BugDate = DateTime.Now;
            var user = _context.Users.SingleOrDefault(u => u.Id == bug.UserId);
            bug.ProjectId = user.ProjectId;
            _context.Bugs.Add(bug);
            _context.SaveChanges();
        }
        public IEnumerable<Bug> GetAllBugs()
        {
            var list = _context.Bugs.OrderByDescending(b=>b.BugDate).Include(p => p.Category).Include(P => P.User).ToList();
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
        public UserBugsViewModel GetBugsByUserId(string id)
        {
            var model = new UserBugsViewModel();
            model.User = _context.Users.Include(p=>p.Project).SingleOrDefault(u => u.Id == id);
            model.Bugs = _context.Bugs.Where(b => b.UserId == id).Include(b=>b.Category).ToList();
            model.BugCounter = model.Bugs.Count();
            return model;
        }
        
        
        
    }
}

