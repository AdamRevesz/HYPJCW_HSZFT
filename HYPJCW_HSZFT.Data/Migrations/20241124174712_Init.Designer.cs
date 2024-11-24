﻿// <auto-generated />
using HYPJCW_HSZFT.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HYPJCW_HSZFT.Data.Migrations
{
    [DbContext(typeof(MainDbContext))]
    [Migration("20241124174712_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DepartmentsEmployees", b =>
                {
                    b.Property<string>("DepartmentsDepartmentCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmployeesEmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DepartmentsDepartmentCode", "EmployeesEmployeeId");

                    b.HasIndex("EmployeesEmployeeId");

                    b.ToTable("DepartmentsEmployees");
                });

            modelBuilder.Entity("HYPJCW_HSZFT.Entities.Entity_Models.Departments", b =>
                {
                    b.Property<string>("DepartmentCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HeadOfDepartment")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("DepartmentCode");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("HYPJCW_HSZFT.Entities.Entity_Models.Employees", b =>
                {
                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("BirthYear")
                        .HasColumnType("int");

                    b.Property<string>("Commission")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CompletedProjects")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Job")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Retired")
                        .HasColumnType("bit");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.Property<int>("StartYear")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("HYPJCW_HSZFT.Models.Entity_Models.Managers", b =>
                {
                    b.Property<string>("ManagerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("BirthYear")
                        .HasColumnType("int");

                    b.Property<bool>("HasMba")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartOfEmployment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ManagerId");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("DepartmentsEmployees", b =>
                {
                    b.HasOne("HYPJCW_HSZFT.Entities.Entity_Models.Departments", null)
                        .WithMany()
                        .HasForeignKey("DepartmentsDepartmentCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HYPJCW_HSZFT.Entities.Entity_Models.Employees", null)
                        .WithMany()
                        .HasForeignKey("EmployeesEmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
