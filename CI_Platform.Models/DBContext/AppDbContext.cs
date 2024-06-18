using CI_Platform.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models.DBContext
{
    public  class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins{ get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<LoginCarousel> LoginCarousels { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Theme> Themes{ get; set; }
        public DbSet<Mission> Missions{ get; set; }
        public DbSet<MissionApplication> MissionApplications{ get; set; }
        public DbSet<MissionMedia> MissionMedias { get; set; }
        public DbSet<VolunteeringTimesheet> VolunteeringTimesheets { get; set; }
        public DbSet<RecentVolunteer> RecentVolunteers { get; set; }
        public DbSet<CMSPrivacyPolicy> CMSPrivacyPolicys { get; set; }
        public DbSet<StoryMedia> StoryMedias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_DATE");

            modelBuilder.Entity<Admin>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_DATE");

            modelBuilder.Entity<LoginCarousel>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_DATE");

            modelBuilder.Entity<Mission>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_DATE");

            modelBuilder.Entity<Comment>().Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_DATE");

            modelBuilder.Entity<MissionApplication>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_DATE");

        }
    }
}
