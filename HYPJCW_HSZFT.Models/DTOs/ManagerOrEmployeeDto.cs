using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Models.DTOs
{
    public class ManagerOrEmployeeDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Manager { get; set; }
        public int YearsWorked { get; set; }
    }
}
