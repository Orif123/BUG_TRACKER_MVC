using BugTrackerProj.ViewModels;
using BugTrackerProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.Service
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
        void NewCategory(NewCategoryViewModel model);
        List<SelectListItem> ListItemCategories();
        List<SelectListItem> GetCategories(string id);
        void DeleteCategory(string id);
    }
}
