using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Logic.Interfaces;
using HYPJCW_HSZFT.Models.DTOs;
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

        public LevelDto RateOfLevels(IQueryable<Employees> employees)
        {
            LevelDto rate = new LevelDto();

            var grouped = employees
                .GroupBy(e => e.Level.ToLower())
                .Select(g => new
                {
                    Level = g.Key,
                    Count = g.Count()
                })
                .ToList();
            foreach (var group in grouped)
            {
                switch (group.Level)
                {
                    case "junior":
                        rate.Junior = group.Count;
                        break;
                    case "medior":
                        rate.Medior = group.Count;
                        break;
                    case "senior":
                        rate.Senior = group.Count;
                        break;
                    default:
                        rate.None = group.Count;
                        break;
                }
            }
            return rate;
        }

        public AveragesalaryDto AverageSalary (IQueryable<Employees> employees)
        {
            var average = employees.Average(x => x.Salary);

            var result = employees
                .GroupBy(x => x.Salary < average)
                .Select(g => new
                {
                    IsUnderAverage = g.Key,
                    Count = g.Count()
                })
                .ToList();
            var rate = new AveragesalaryDto
            {
                UnderAverage = result.Where(r => r.IsUnderAverage).Sum(r => r.Count),
                OverAverage = result.Where(r => !r.IsUnderAverage).Sum(r => r.Count)
            };
            return rate;
        }

        public IQueryable<Employees> GetEmployeesBornInThe80()
        {
            var everyEmployee = employeeRepo.ReadAll();
            return everyEmployee.Where(p => p.BirthYear >= 1980 && p.BirthYear <= 1989);
        }

        public IQueryable<Employees> GetEmployeesAtleastWorkingInTwoDepartments()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Employees> GetEmployeesWorkingButPension()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Employees> GetEmployeesOnPension()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Employees> GetAverageOfSalaryOfEmployeesOnPension()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Employees> GetWorkersDescSalaryWithPension()
        {
            throw new NotImplementedException();
        }

        public LevelDto GetRatesOfEmployeeLevels()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Employees> GetEmployeesOfDepartmentWithDoctorateManager()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Employees> GetAverageOfSalaryEachLevel()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Employees> WhoEarnsMoreJuniorOrMedior()
        {
            throw new NotImplementedException();
        }

        public Employees GetHighestCommissionFromLevel()
        {
            throw new NotImplementedException();
        }

        public Employees GetEmployeeWithLeastProjectsBasedOnZearsWorked()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Employees> GetSalaryOFEmployeesBasedOnBirthYear()
        {
            throw new NotImplementedException();
        }

        public Employees GetActiveEmployeeLeastProjects()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Employees> GetEmployeeWithHigherCommissionThanOthersSalary()
        {
            throw new NotImplementedException();
        }
    }
}
