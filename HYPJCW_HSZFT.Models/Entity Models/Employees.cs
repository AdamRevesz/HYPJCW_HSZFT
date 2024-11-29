using HYPJCW_HSZFT.Entities.Dependencies;
using HYPJCW_HSZFT.Models.Entity_Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HYPJCW_HSZFT.Entities.Entity_Models
{
    [ToExport]
    public class Employees
    {
        [XmlAttribute("employeeid")]
        [JsonIgnore]
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
        public virtual ICollection<Departments> Departments { get; set; }

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
            Departments = departments ?? new List<Departments>(); // Initialize with empty list if null
        }
        public Employees() { }

    }
}
