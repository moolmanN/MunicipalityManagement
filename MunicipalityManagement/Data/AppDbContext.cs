using Microsoft.EntityFrameworkCore;
using MunicipalityManagement.Models;

namespace MunicipalityManagement.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Report> Reports { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Explicitly set the primary key for ServiceRequest
            modelBuilder.Entity<ServiceRequest>().HasKey(sr => sr.RequestID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
