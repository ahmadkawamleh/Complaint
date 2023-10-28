using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions options)
           : base(options)
        {
        }
        public DbSet<Complaint> Complaint { get; set; }
        public DbSet<ComplaintAttachment> ComplaintAttachment { get; set; }
        public DbSet<Demands> Demands { get; set; }

        #region ModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole("admin"), new IdentityRole("user"));
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser("AdminUser") { PasswordHash= "fd4fb3735df3f485a73ea6900668e30598fc31d1a6a5ef2b19803fee407b7f7f",Email= "AdminUser@admin.com", NormalizedUserName= "ADMINUSER" });
        }
        #endregion
    }
}
