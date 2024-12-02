using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Models.DTOs
{
    public class EmployeeDto
    {
        public string EmployeeId { get; set; }
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
        public string Commission { get; set; }
        public List<DepartmentDto> Departments { get; set; }

        public override string ToString()
        {
            return $"Employee Id: {EmployeeId}" +
                $"\nName: {Name}" +
                $"\nBirthYear: {BirthYear}" +
                $"\nStartYear: {Convert.ToString(StartYear)}" +
                $"\nCompleted Projects: {Convert.ToString(CompletedProjects)}" +
                $"\n Active? : {(Active ? "Active" : "On pension")}" +
                $"\n Email: {Email}" +
                $"\nPhone number: {Phone}" +
                $"\nJob: {Job}" +
                $"\nLevel: {Level}" +
                $"\nSalary: {Convert.ToString(Salary)}" +
                $"\nCommission: {Commission}" +
                $"\nDepartments: {Departments}";
        }

    }

    public class DepartmentDto
    {
        public string Name { get; set; }
        public string DepartmentCode { get; set; }
        public string HeadOfDepartment { get; set; }
    }

}
