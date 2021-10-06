using BugTrackerProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

namespace BugTrackerProj.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Bug> Bugs { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Project>().HasData(
                new Project { ProjectId="1", ProjectName="Tesla",  },
                new Project { ProjectId="2", ProjectName="Microsoft"}
                );
            builder.Entity<Category>().HasData(
                new Category { CategoryId="1", CtaegoryName="Development", ProjectId="1"},
                new Category { CategoryId="2", CtaegoryName="QA", ProjectId="1"},
                new Category { CategoryId="3", CtaegoryName="Development", ProjectId="2"},
                new Category { CategoryId="4", CtaegoryName="QA", ProjectId="2"}
                );
            builder.Entity<Bug>().HasData(
                new Bug { BugId="o1", BugDate=DateTime.Now, Description="ori", ProjectId="1", CategoryId="1", UserId= "7f3c9ad6-090f-444a-8dd3-9e4179dcbac7" }
                );
        }
    }
}
