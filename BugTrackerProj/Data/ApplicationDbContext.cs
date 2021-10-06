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
                new Category { CategoryId="1", CtaegoryName="Development"},
                new Category { CategoryId="2", CtaegoryName="QA"}
                );
        }
    }
}
