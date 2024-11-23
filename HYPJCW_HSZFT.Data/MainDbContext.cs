using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Models.Entity_Models;
using Microsoft.EntityFrameworkCore;

namespace HYPJCW_HSZFT.Data
{
    public class MainDbContext : DbContext
    {
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Managers> Managers { get; set; }

        public MainDbContext(DbContextOptions option) : base(option)
        {
            this.Database.EnsureCreated();
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.HasMany(e => e.Departments)
                    .WithMany(d => d.Employees);

            });

            modelBuilder.Entity<Departments>(entity =>
            {
            entity.HasKey(d => d.DepartmentCode);

                entity.HasMany(d => d.Employees)
                .WithMany(d => d.Departments);
            });

            modelBuilder.Entity<Managers>(entity =>
            {
                entity.HasKey(m => m.ManagerId);
            });
        }
    }
}
