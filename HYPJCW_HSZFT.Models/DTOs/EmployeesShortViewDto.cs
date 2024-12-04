using HYPJCW_HSZFT.Entities.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Models.DTOs
{
    public class EmployeesShortViewDto
    {
        public string Name { get; set; }
        public int Salary { get; set; }
        public string Commisison { get; set; }
        public int CompletedProjects { get; set; }
        public List<DepartmentDto> Departments { get; set; } = new List<DepartmentDto>();

        
        public EmployeesShortViewDto()
        {
            Departments = new List<DepartmentDto>();
        }
        public override string ToString()
        {
            var departments = Departments != null ? string.Join(", ", Departments.Select(d => d.Name)) : "None";
            return $"Name: {Name}" +
                $"\nSalary: {Salary}" +
                $"\nCommission: {Commisison}" +
                $"\nCompleted Projects: {CompletedProjects}" +
                $"\nDepartments: {departments}";
        }
    }
}
