using Hospital.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Hospital.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Doctors> Doctors { get; set; }

        public DbSet<Pills> Pills { get; set; }

        public DbSet<DoctorsRating> DoctorsRatings { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserToken> UserTokens { get; set; }

        public DbSet<AuditLog> AuditLogs { get; set; }

        public DbSet<Appointments> Appointments { get; set; }

        public DbSet<Roles> Roles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DoctorsRating>()
                .HasKey(o => new { o.UserID, o.DoctorID });
            modelBuilder.Entity<UserToken>()
                .HasKey(o => new { o.UserID, o.Token });
            modelBuilder.Entity<Roles>()
                .HasKey(o => new { o.RoleID });
            modelBuilder.Entity<Appointments>()
                .HasKey(o => new { o.Hour, o.Minute, o.Day, o.Year, o.Month });
            
            
            modelBuilder.Entity<Roles>()
                .HasData(new Roles { RoleID = Guid.Parse("1ab783cd-9eaf-4397-ad57-4d2a62abf48e"), RoleName = "Client", RoleOrder = 0 });
            modelBuilder.Entity<Roles>()
                .HasData(new Roles { RoleID = Guid.Parse("4c0f9bb5-fcc4-447d-baac-5d30363f757e"), RoleName = "Doctor", RoleOrder = 1 });
            modelBuilder.Entity<Roles>()
                .HasData(new Roles { RoleID = Guid.Parse("50c43a7e-76ab-4f66-9ff7-63cb8a3eb578"), RoleName = "Head Doctor", RoleOrder = 2 });

            modelBuilder.Entity<User>()
                .HasData(new User { Id = Guid.NewGuid(), Password = "P@ssw0rd", UserRole = Guid.Parse("50c43a7e-76ab-4f66-9ff7-63cb8a3eb578"), Email = "admin@gmail.com", Address = "Chernivtsi", Gender = "Male", Name = "Oleksandr", Surname = "Syrotiuk", Phone = "380951483461"});
            modelBuilder.Entity<User>()
                .HasData(new User { Id = Guid.NewGuid(), Password = "P@ssw0rd", UserRole = Guid.Parse("4c0f9bb5-fcc4-447d-baac-5d30363f757e"), Email = "client@gmail.com", Address = "Chernivtsi", Gender = "Male", Name = "Pitsul", Surname = "Andrii", Phone = "380668122551" });
        }
    }
}
