using HYPJCW_HSZFT.Entities.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Models.Entity_Models
{
    public class EmployeesOfDepartments
    {
        public string EmployeesOfDepartmentsId { get; set; }
        public string DepartmentId { get; set; }
        public string EmployeeId { get; set; }

        public virtual Departments Department { get; set; }
        public virtual Employees Employee { get; set; }
        public EmployeesOfDepartments()
        {

        }
    }
}
