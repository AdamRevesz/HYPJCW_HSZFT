﻿using HYPJCW_HSZFT.Entities.Entity_Models;
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

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder mod)
        {
            mod.Entity<Employees>(entity =>
            {
                entity.HasKey(x => x.EmployeeId);

                entity.HasMany(e => e.Departments)
                .WithMany(d => d.Employees);
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



            base.OnModelCreating(mod);
        }
    }
}
