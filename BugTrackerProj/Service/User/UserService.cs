﻿using BugTrackerProj.Data;
using BugTrackerProj.Models;
using BugTrackerProj.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerProj.Service
{
    public class UserService :IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
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
        public List<ApplicationUser> GetUserByProject(string projectid)
        {
            var list = _context.Users.Where(u => u.ProjectId == projectid).Include(u => u.Project).ToList();
            return list;
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
    }
}