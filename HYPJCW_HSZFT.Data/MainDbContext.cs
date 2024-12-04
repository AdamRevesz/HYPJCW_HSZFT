using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Models.Entity_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Reflection.Emit;

namespace HYPJCW_HSZFT.Data
{
    public class MainDbContext : DbContext
    {
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Managers> Managers { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder mod)
        {
            mod.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);
            });
            mod.Entity<Employees>()
                .HasMany(e => e.Departments)
                .WithMany(d => d.Employees);

            mod.Entity<Departments>(entity =>
            {
                entity.HasKey(x => x.DepartmentCode);
            });

            mod.Entity<Employees>()
    .HasMany(e => e.Departments)
    .WithMany(d => d.Employees)
    .UsingEntity<Dictionary<string, object>>(
        "EmployeeDepartment",
        l => l.HasOne<Departments>().WithMany().HasForeignKey("DepartmentCode").HasPrincipalKey(d => d.DepartmentCode),
        r => r.HasOne<Employees>().WithMany().HasForeignKey("EmployeeId").HasPrincipalKey(e => e.EmployeeId),
        j => j.HasKey("EmployeeId", "DepartmentCode"));
            base.OnModelCreating(mod);
        }
    }
}
