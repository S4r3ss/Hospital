using Hospital.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Doctors> Doctors { get; set; }

        public DbSet<Pills> Pills { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        { }
    }
}
