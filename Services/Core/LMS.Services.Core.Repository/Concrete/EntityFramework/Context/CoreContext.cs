using Microsoft.EntityFrameworkCore;
using System;
using LMS.Services.Core.Domain.Identity;
using LMS.Common.Model;

namespace LMS.Services.Core.Repository.Concrete.EntityFramework.Context
{
    public class CoreContext :DbContext
    {
        public CoreContext()
        {
        }

        public CoreContext(DbContextOptions<CoreContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departmant>()
                .HasOne(a => a.Subscription)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Departmant>()
                .HasOne(a => a.Subscription)
                .WithMany(a=> a.Departmants)
                .HasForeignKey(a=> a.SubscriptionId);


          //  PrepareInitialValues(modelBuilder);
        }

        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Departmant> Departmants { get; set; }
        public DbSet<DepartmantEducation> DepartmantEducations { get; set; }
        public DbSet<DepartmantEducationsResult> DepartmantEducationsResult { get; set; }
        public DbSet<User> Users { get; set; }

        private void PrepareInitialValues(ModelBuilder modelBuilder)
        {
            
            
        }
    }
}
