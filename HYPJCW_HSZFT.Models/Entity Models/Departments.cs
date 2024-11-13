using HYPJCW_HSZFT.Entities.Dependencies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Entities.Entity_Models
{
    [ToExport]
    public class Departments
    {
        [MaxLength(200)]
        public string Name { get; set; }
        [Key]
        public string DepartmentCode { get; set; }
        [MaxLength(200)]
        public string HeadOfDepartment { get; set; }

        public Departments(string name, string departmentCode,string headOfDepartment )
        {
            Name = name;
            DepartmentCode = departmentCode;
            HeadOfDepartment = headOfDepartment;
        }

        public Departments() { }


    }
}
