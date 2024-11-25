using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Models.Entity_Models
{
    public class Managers
    {
        public string Name { get; set; }
        [Key]
        public string ManagerId { get; set; }
        public DateTime BirthYear { get; set; }
        public DateTime StartOfEmployment { get; set; }
        public bool HasMba { get; set; }
        public Managers()
        {
            
        }

        public Managers(string name, string managerId, DateTime birthYear, DateTime startOfEmployment, bool hasMBA)
        {
            Name = name;
            ManagerId = managerId;
            BirthYear = birthYear;
            StartOfEmployment = startOfEmployment;
            HasMba = hasMBA;
        }
    }
}
