using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Logic.Interfaces;
using HYPJCW_HSZFT.Models.Entity_Models;
using HYPJCW_HSZFT.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Logic
{
    public class MixedLogic : IMixedLogic
    {
        IRepository<Employees> employeeRepo;
        IRepository<Departments> departmentRepo;
        IRepository<Managers> managerRepo;

        public MixedLogic(IRepository<Employees> repo1, IRepository<Departments> repo2, IRepository<Managers> repo3)
        {
            employeeRepo = repo1;
            departmentRepo = repo2;
            managerRepo = repo3;
        }

        public List<Managers> IsThereManagerWhoIsDepartmentManager()
        {
            var everyDepartment = departmentRepo.ReadAll();
            var everyManager = managerRepo.ReadAll();

            var managersWhoAreHeads = everyManager
                .Where(m => everyDepartment.Any(d => d.HeadOfDepartment.Equals(m.Name, StringComparison.OrdinalIgnoreCase)))
                .ToList();
            return managersWhoAreHeads;

        }

        public void WhoAreManagersOrDepartmentManagers()
        {
            throw new NotImplementedException();
        }

        public void WhoWorksForTheLongest()
        {
            throw new NotImplementedException();
        }
    }
}
