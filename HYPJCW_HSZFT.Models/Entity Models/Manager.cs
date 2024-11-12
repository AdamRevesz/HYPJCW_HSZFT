using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Models.Entity_Models
{
    public class Manager
    {
        public string Name { get; set; }
        [Key]
        public string ManagerId { get; set; }
        public int BirthYear { get; set; }
        public string StartOfEmployment { get; set; }
        public bool HasMBA { get; set; }
        public Manager()
        {
            
        }

        public Manager(string name, string managerId, int birthYear, string startOfEmployment, bool hasMBA)
        {
            Name = name;
            ManagerId = managerId;
            BirthYear = birthYear;
            StartOfEmployment = startOfEmployment;
            HasMBA = hasMBA;
        }
    }
}
