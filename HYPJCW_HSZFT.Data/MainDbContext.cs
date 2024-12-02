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
        public DbSet<EmployeesOfDepartments> EmployeesOfDepartments { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder mod)
        {
            mod.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);
            });

            mod.Entity<Departments>(entity =>
            {
                entity.HasKey(x => x.DepartmentCode);
            });

            mod.Entity<Managers>(entity =>
            {
                entity.HasKey(x => x.ManagerId);
            });

            mod.Entity<EmployeesOfDepartments>(entity =>
            {
                entity.HasKey(e => e.EmployeesOfDepartmentsId);

                entity.HasOne(eod => eod.Employee)
                    .WithMany(e => e.EmployeesOfDepartments)
                    .HasForeignKey(eod => eod.EmployeeId);

                entity.HasOne(e => e.Department)
                    .WithMany(d => d.EmployeesOfDepartments)
                    .HasForeignKey(eod => eod.DepartmentId);
            });
            base.OnModelCreating(mod);
        }
    }
}
