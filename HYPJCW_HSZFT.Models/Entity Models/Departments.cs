using HYPJCW_HSZFT.Entities.Dependencies;
using HYPJCW_HSZFT.Models.Entity_Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        //[JsonIgnore]
        //[NotMapped]
        public virtual List<Employees> Employees { get; set; } = new List<Employees>();
        //[JsonIgnore]
        //[NotMapped]


        public Departments(string name, string departmentCode,string headOfDepartment )
        {
            Name = name;
            DepartmentCode = departmentCode;
            HeadOfDepartment = headOfDepartment;
            this.Employees = new List<Employees>();
        }

        public Departments() { }

        public override string ToString()
        {
            return $"Department Name: {Name}" +
                $"Department Code: {DepartmentCode}" +
                $"Head of department: {HeadOfDepartment}";
        }

    }
}
