﻿using HYPJCW_HSZFT.Entities.Dependencies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HYPJCW_HSZFT.Entities.Entity_Models
{
    public class Employee
    {
        [XmlAttribute("employeeid")]
        [Key]
        public string EmployeeId { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public int BirthYear { get; set; }

        public int StartYear { get; set; }
        public int CompletedProjects { get; set; }
        public bool Active { get; set; }
        public bool Retired { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Job { get; set; }
        public string Level { get; set; }
        public int Salary { get; set; }
        public int Commission { get; set; }
        public List<Departments> Departments { get; set; }

        public Employee(
            string employeeId, string name, int birthYear, int startYear,
            int completedProjects, bool active, bool retired, string email, string phone,
            string job, string level, int salary, int commission, List<Departments> departments)
        {
            //EmployeeId = employeeId;
            Name = name;
            BirthYear = birthYear;
            StartYear = startYear;
            CompletedProjects = completedProjects;
            Active = active;
            Retired = retired;
            Email = email;
            Phone = phone;
            Job = job;
            Level = level;
            Salary = salary;
            Commission = commission;
            Departments = departments ?? new List<Departments>(); // Initialize with empty list if null
        }
        public Employee() { }

    }
}