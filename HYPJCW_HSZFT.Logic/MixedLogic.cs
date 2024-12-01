using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Logic.Interfaces;
using HYPJCW_HSZFT.Models.DTOs;
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

        public List<DepartmentOrManagersDto> WhoAreManagersOrDepartmentManagers()
        {
            var everyManager = managerRepo.ReadAll();
            var departments = departmentRepo.ReadAll();

            var departmentHeads = departments.Select(d => d.HeadOfDepartment).ToHashSet(StringComparer.OrdinalIgnoreCase);

            var managerDtos = everyManager
                .Where(m => !departmentHeads.Contains(m.Name))
                .Select(m => new DepartmentOrManagersDto
                {
                    Id = m.ManagerId,
                    Name = m.Name,
                    DepartmentManager = false
                })
                .ToList();

            var departmentHeadDtos = departments
                .Select(d => new DepartmentOrManagersDto
                {
                    Id = d.DepartmentCode,
                    Name = d.HeadOfDepartment,
                    DepartmentManager = true
                })
                .ToList();

            return managerDtos.Concat(departmentHeadDtos).ToList();
        }


        public ManagerOrEmployeeDto WhoWorksForTheLongest()
        {
            var everyEmployee = employeeRepo.ReadAll();
            var everyManager = managerRepo.ReadAll();

            var longestWorkingEmployee = everyEmployee
                .Select(e => new ManagerOrEmployeeDto
                {
                    Id = e.EmployeeId,
                    Name = e.Name,
                    Manager = false,
                    YearsWorked = (DateTime.Now.Year - e.StartYear)
                })
                .OrderByDescending(e => e.YearsWorked)
                .FirstOrDefault();

            var longestWorkingManager = everyManager
                .Select(m => new ManagerOrEmployeeDto
                {
                    Id = m.ManagerId,
                    Name = m.Name,
                    Manager = true,
                    YearsWorked = (int)((DateTime.Now - DateTime.Parse(m.StartOfEmployment)).TotalDays / 365.25)
                })
                .OrderByDescending(m => m.YearsWorked)
                .FirstOrDefault();

            if (longestWorkingManager is null || longestWorkingEmployee is null)
            {
            throw new ArgumentException("Invalid data");
            }

            if (longestWorkingManager.YearsWorked > longestWorkingEmployee.YearsWorked)
            {
                return longestWorkingManager;
            }
            else if (longestWorkingEmployee.YearsWorked > longestWorkingManager.YearsWorked)
            {
                return longestWorkingEmployee;
            }
            throw new ArgumentException("They worked the same years");
        }
    }
}
