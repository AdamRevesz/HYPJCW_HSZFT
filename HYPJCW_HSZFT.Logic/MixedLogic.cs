using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Logic.Interfaces;
using HYPJCW_HSZFT.Models.Entity_Models;
using HYPJCW_HSZFT.Repository;
using Microsoft.EntityFrameworkCore;
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

        public (List<Managers> Managers, List<Managers> DepartmentManagers) WhoAreManagersOrDepartmentManagers()
        {
            var everymanager = managerRepo.ReadAll();
            var departmens = departmentRepo.ReadAll();

            var headsOfDepartments = departmens
                .Select(d => d.HeadOfDepartment)
                .ToList();

            var departmentManagers = everymanager
                .Where(m => headsOfDepartments.Contains(m.Name))
                .ToList();

            var onlyManagers = everymanager
                .Where(m => !headsOfDepartments.Contains(m.Name))
                .ToList();

            return
                (
                onlyManagers,
                departmentManagers
                );
        }

        public (string Name, int YearsWorked) WhoWorksForTheLongest()
        {
            var everyEmployee = employeeRepo.ReadAll();
            var everyManager = managerRepo.ReadAll();

            var longestWorkingEmployee = everyEmployee
                .Select(e => new
                {
                    Name = e.Name,
                    YearsWorked = DateTime.Now.Year - e.StartYear
                })
                .OrderByDescending(e => e.YearsWorked)
                .FirstOrDefault();

            var longestWorkingManager = everyManager
                .Select(m => new
                {
                    Name = m.Name,
                    YearsWorked = DateTime.Now.Year - DateTime.Parse(m.StartOfEmployment).Year
                })
                .OrderByDescending(m => m.YearsWorked)
                .FirstOrDefault();

            if (longestWorkingManager is null || longestWorkingEmployee is null)
            {
            throw new ArgumentException("Invalid data");
            }

            if (longestWorkingManager.YearsWorked > longestWorkingEmployee.YearsWorked)
            {
                return
                (
                  longestWorkingManager.Name,
                  longestWorkingManager.YearsWorked
                );
            }
            else if (longestWorkingEmployee.YearsWorked > longestWorkingManager.YearsWorked)
            {
                return
                (
                 longestWorkingEmployee.Name,
                 longestWorkingEmployee.YearsWorked
                );
            }
            throw new ArgumentException("They worked the same years");
        }
    }
}
