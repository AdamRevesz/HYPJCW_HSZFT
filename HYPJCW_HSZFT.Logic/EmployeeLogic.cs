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
        IRepository<Departments> departmentRepo;
        public EmployeeLogic(IRepository<Employees> repo)
        {
            employeeRepo = repo;
        }
        public IQueryable<Employees> ReadAll()
        {
            return employeeRepo.ReadAll();
        }

        public void GetRatesOfEmployeeLevels()
        {
            LevelDto rate = new LevelDto();
            var employees = employeeRepo.ReadAll();

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
                        Console.WriteLine($"\nJunior: {Graphlogic.GraphGraphicSmallNumber(rate.Junior)} {rate.Junior}");
                        break;
                    case "medior":
                        rate.Medior = group.Count;
                        Console.WriteLine($"\nMedior: {Graphlogic.GraphGraphicSmallNumber(rate.Medior)} {rate.Medior}");

                        break;
                    case "senior":
                        rate.Senior = group.Count;
                        Console.WriteLine($"\nSenior: {Graphlogic.GraphGraphicSmallNumber(rate.Senior)} {rate.Senior}");

                        break;
                    default:
                        rate.None = group.Count;
                        Console.WriteLine($"\nNone: {Graphlogic.GraphGraphicSmallNumber(rate.None)} {rate.None}");
                        break;
                }
            }
            
        }

        public AveragesalaryDto GetNumberOfEmployeesUnderOrOverTheAverageSalary()
        {
            var everEmployee = employeeRepo.ReadAll();
            var average = everEmployee.Average(x => x.Salary);

            var result = everEmployee
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
            var everyEployee = employeeRepo.ReadAll();
            return everyEployee.Where(e => e.Departments.Count >= 2);
        }

        public IQueryable<Employees> GetEmployeesWorkingButPension()
        {
            var everyEmployee = employeeRepo.ReadAll();
            return everyEmployee.Where(e => e.Retired && e.Active);
        }

        public IQueryable<Employees> GetEmployeesOnPension()
        {
            var everyEmployee = employeeRepo.ReadAll();
            return everyEmployee.Where(e => e.Retired && !e.Active);
        }

        public double GetAverageOfSalaryOfEmployeesOnPension()
        {
            var everyEmployee = employeeRepo.ReadAll();
            return everyEmployee
                .Where(e => e.Retired)
                .Average(e => e.Salary);
        }

        public IEnumerable<Employees> GetWorkersDescSalaryWithCommission()
        {
            var everyEmployee = employeeRepo.ReadAll()
                .Where(e => !string.IsNullOrEmpty(e.Commission))
                .OrderByDescending(e => e.Salary)
                .ToList();

            foreach (var e in everyEmployee)
            {
                if (e.Commission.Contains("eur", StringComparison.OrdinalIgnoreCase))
                {
                    var commissionValue = decimal.Parse(e.Commission.Replace("eur", "").Trim());
                    e.Commission = (commissionValue * 400).ToString("F0"); // Convert to HUF
                }
            }
            return everyEmployee;
        }


        public IQueryable<Employees> GetEmployeesOfDepartmentWithDoctorateManager()
        {
            var everyDepartment = departmentRepo.ReadAll();
            var everyEmployee = employeeRepo.ReadAll();

            return everyEmployee
                .Where(e => e.Departments.Any(d =>
                everyDepartment
                .Where(dept => dept.HeadOfDepartment.Contains("Dr. "))
                .Select(dept => dept.DepartmentCode)
                .Contains(d.DepartmentCode)
                ));
        }

        

        public double GetAverageOfSalaryEachLevel()
        {

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
