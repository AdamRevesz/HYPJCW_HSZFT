using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Models.Entity_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace HYPJCW_HSZFT.Data
{
    public class MainDbContext : DbContext
    {
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Managers> Managers { get; set; }

        public MainDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder mod)
        {
            mod.Entity<Employees>(entity =>
            {
                entity.HasKey(x => x.EmployeeId);

                entity.HasMany(e => e.Departments)
                .WithMany(d => d.Employees)
              .UsingEntity<Dictionary<string, object>>
              (
                "EmployeeDepartment",
                j => j.HasOne<Departments>().WithMany().HasForeignKey("DepartmentId"),
                j => j.HasOne<Employees>().WithMany().HasForeignKey("EmployeeId"));
            }
            );
            mod.Entity<Departments>(entity =>
            {
                entity.HasKey(x => x.DepartmentCode);
            });

            mod.Entity<Managers>(entity =>
            {
                entity.HasKey(x => x.ManagerId);
            });

        }
    }
}
