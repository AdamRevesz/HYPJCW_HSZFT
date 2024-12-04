using HYPJCW_HSZFT.Entities.Dependencies;
using HYPJCW_HSZFT.Models.DTOs;
using HYPJCW_HSZFT.Models.Entity_Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace HYPJCW_HSZFT.Entities.Entity_Models
{
    [ToExport]
    public class Employees
    {
        [XmlAttribute("employeeid")]
        [Key]
        public string EmployeeId { get; set; } = "";

        [StringLength(200)]
        public string Name { get; set; }
        public int BirthYear { get; set; }
        public int StartYear { get; set; }
        public int CompletedProjects { get; set; }
        public bool Active { get; set; }
        public bool Retired { get; set; }
        public string Email { get; set; } = "";
        public string Phone { get; set; }
        public string Job { get; set; }
        public string Level { get; set; }
        public int Salary { get; set; }
        public string Commission { get; set; } = "";
        //[JsonIgnore]
        //[NotMapped]
        public virtual ICollection<Departments> Departments { get; set; }
        //[JsonIgnore]
        //[NotMapped]
        public virtual ICollection<EmployeesOfDepartments> EmployeesOfDepartments { get; set; }

        public Employees(
            string employeeId, string name, int birthYear, int startYear,
            int completedProjects, bool active, bool retired, string email, string phone,
            string job, string level, int salary, string commission, ICollection<Departments> departments)
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
            this.Departments = new HashSet<Departments>(); // Initialize with empty list if null
        }
        public Employees() { }

        public Employees(EmployeeDto employeeDto)
        {
            EmployeeId = employeeDto.EmployeeId ?? string.Empty;
            Name = employeeDto.Name ?? string.Empty;
            BirthYear = employeeDto.BirthYear ?? default;
            StartYear = employeeDto.StartYear ?? default;
            CompletedProjects = employeeDto.CompletedProjects ?? default;
            Active = employeeDto.Active;
            Retired = employeeDto.Retired;
            Email = employeeDto.Email ?? string.Empty;
            Phone = employeeDto.Phone ?? string.Empty;
            Job = employeeDto.Job ?? string.Empty;
            Level = employeeDto.Level ?? string.Empty;
            Salary = employeeDto.Salary ?? default;
            Commission = employeeDto.Commission ?? string.Empty;
            Departments = employeeDto.Departments?.Select(d => new Departments
            {
                Name = d.Name,
                DepartmentCode = d.DepartmentCode,
                HeadOfDepartment = d.HeadOfDepartment
            }).ToList() ?? new List<Departments>();
        }
    }
}
