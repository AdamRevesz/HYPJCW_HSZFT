using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Logic.Interfaces;
using HYPJCW_HSZFT.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Logic
{
    public class EmployeeLogic : IEmployeesLogic
    {
        IRepository<Employees> employeeRepo;
        public EmployeeLogic(IRepository<Employees> repo)
        {
            employeeRepo = repo;
        }
        public IQueryable<Employees> ReadAll()
        {
            return employeeRepo.ReadAll();
        }
    }
}
