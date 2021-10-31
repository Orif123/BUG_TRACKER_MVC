using BugTrackerProj.Data;
using BugTrackerProj.ViewModels;
using BugTrackerProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.Service
{
    public class CategoryService: ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IBugService _bugService;
        public CategoryService(ApplicationDbContext context, IBugService bugService)
        {
            _context = context;
            _bugService = bugService;
        }
        public List<Category> GetAllCategories()
        {
            var list = _context.Categories.Include(p => p.Project).ToList();
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
        public void DeleteCategory(string id)
        {
            var category = _context.Categories.SingleOrDefault(p => p.CategoryId == id);
            var bugs = _context.Bugs.Where(c => c.CategoryId == category.CategoryId).ToList();
            foreach (var bug in bugs)
            {
                _bugService.BugSolved(bug.BugId);
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
        public void NewCategory(NewCategoryViewModel model)
        {
            var category = new Category()
            {
                CategoryId = Guid.NewGuid().ToString(),
                CtaegoryName = model.CategoryName,
                ProjectId = model.ProjectId
            };
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
    }
}
