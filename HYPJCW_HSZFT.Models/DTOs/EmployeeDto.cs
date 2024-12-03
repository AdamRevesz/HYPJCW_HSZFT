using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Models.DTOs
{
    public class EmployeeDto
    {
        [JsonPropertyName("employeeid")]
        public string? EmployeeId { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("birthyear")]
        public int? BirthYear { get; set; }
        [JsonPropertyName("startyear")]
        public int? StartYear { get; set; }
        [JsonPropertyName("completedprojects")]
        public int? CompletedProjects { get; set; }
        [JsonPropertyName("active")]
        public bool Active { get; set; }
        [JsonPropertyName("retired")]
        public bool Retired { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }
        [JsonPropertyName("job")]
        public string? Job { get; set; }
        [JsonPropertyName("level")]
        public string? Level { get; set; }
        [JsonPropertyName("salary")]
        public int? Salary { get; set; }
        [JsonPropertyName("commission")]
        public string? Commission { get; set; }
        [JsonPropertyName("departments")]
        public List<DepartmentDto> Departments { get; set; }

        public override string ToString()
        {
            var departmentsStr = Departments != null
           ? string.Join(", ", Departments.Select(d => d.Name))
           : "None";

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
                $"\nDepartments: {departmentsStr}";
        }

    }

    public class DepartmentDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("departmentcode")]
        public string DepartmentCode { get; set; }
        [JsonPropertyName("headofdepartment")]
        public string HeadOfDepartment { get; set; }
    }

}
