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
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole("admin")
                {
                    Id = "78ca9c03-4b50-4935-874e-d93fae9f7c4e",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole("user")
                {
                    Id = "d42b4f89-4216-4f43-9fc0-7037efb191f1",
                    NormalizedName = "USER"
                }
                );

            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser("Admin")
                {
                    Id = "c1bbd3af-2601-42d1-a2c6-542b473b01e4",
                    PasswordHash = "AQAAAAEAACcQAAAAEPaBlw2dbcjmOyJCHCYPb4TfmHmwGlNq2IOq9rsmresca3M0eIhLIZYyCy8SNqx2+w==", //pasword: 123456
                    Email = "Admin@admin.com",
                    NormalizedUserName = "ADMIN"
                }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    UserId = "c1bbd3af-2601-42d1-a2c6-542b473b01e4",
                    RoleId = "78ca9c03-4b50-4935-874e-d93fae9f7c4e"
                }
                );
        }
        #endregion
    }
}
