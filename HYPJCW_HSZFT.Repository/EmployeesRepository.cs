using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using HYPJCW_HSZFT.Data;
using HYPJCW_HSZFT.Entities.Entity_Models;

namespace HYPJCW_HSZFT.Repository
{
    public class EmployeesRepository
    {
        private readonly MainDbContext _context;

        public EmployeesRepository(MainDbContext ctx)
        {
            _context = ctx;
        }

        public override Employees Read(string id)
        {
            return _context.Employees.FirstOrDefault(x => x.EmployeeId == id);
        }

    }
}
