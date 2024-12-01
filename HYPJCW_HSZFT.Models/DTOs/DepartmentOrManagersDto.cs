using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Models.DTOs
{
    public class DepartmentOrManagersDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool DepartmentManager { get; set; }
    }
}
