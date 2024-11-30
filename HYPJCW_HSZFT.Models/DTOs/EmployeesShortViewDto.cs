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
        public List<DepartmentDto> Departments { get; set; }
    }
}
