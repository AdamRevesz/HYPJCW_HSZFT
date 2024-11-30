using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Logic.Interfaces;
using HYPJCW_HSZFT.Models.DTOs;
using HYPJCW_HSZFT.Models.Entity_Models;
using HYPJCW_HSZFT.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public List<Employees> ReadAll()
        {
            return employeeRepo.ReadAll().ToList();
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

        public List<Employees> GetEmployeesBornInThe80()
        {
            var everyEmployee = employeeRepo.ReadAll();
            return everyEmployee.Where(p => p.BirthYear >= 1980 && p.BirthYear <= 1989).ToList();
        }

        public List<Employees> GetEmployeesAtleastWorkingInTwoDepartments()
        {
            var everyEployee = employeeRepo.ReadAll();
            return everyEployee.Where(e => e.Departments != null && e.Departments.Count >= 2).ToList();
        }

        public List<Employees> GetEmployeesWorkingButPension()
        {
            var everyEmployee = employeeRepo.ReadAll();
            return everyEmployee.Where(e => e.Retired && e.Active).ToList(); ;
        }

        public List<Employees> GetEmployeesOnPension()
        {
            var everyEmployee = employeeRepo.ReadAll();
            return everyEmployee.Where(e => e.Retired && !e.Active).ToList();
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


        public List<Employees> GetEmployeesOfDepartmentWithDoctorateManager()
        {
            var everyDepartment = departmentRepo.ReadAll();
            var everyEmployee = employeeRepo.ReadAll();

            return everyEmployee
                .Where(e => e.Departments.Any(d =>
                everyDepartment
                .Where(dept => dept.HeadOfDepartment.Contains("Dr. "))
                .Select(dept => dept.DepartmentCode)
                .Contains(d.DepartmentCode)
                )).ToList();
        }



        public Dictionary<string, double> GetAverageOfSalaryEachLevel()
        {
            var everyEmployee = employeeRepo.ReadAll();
            var grouped = everyEmployee
                .GroupBy(e => e.Level.ToLower() ?? "unkown")
                .Select(g => new
                {
                    Level = g.Key,
                    AverageSalary = g.Average(e => e.Salary)
                })
                .ToDictionary(x => x.Level, x => x.AverageSalary);
            return grouped;

        }

        public Employees WhoEarnsMoreJuniorOrMedior()
        {
            var everyEmployee = employeeRepo.ReadAll();

            var mediors = everyEmployee.Where(e => e.Level.ToLower() == "junior");
            var juniors = everyEmployee.Where(e => e.Level.ToLower() == "medior");

            var averageMediorSalary = mediors.Any() ? mediors.Average(m => m.Salary) : 0;
            var maxJuniorSalary = juniors.Any() ? juniors.Max(j => j.Salary) : 0;
            if (averageMediorSalary > maxJuniorSalary)
            {
                return mediors.FirstOrDefault(m => m.Salary == averageMediorSalary);
            }
            return juniors.FirstOrDefault(j => j.Salary == maxJuniorSalary);
        }

        public (string Level, decimal HighestCommission) GetHighestCommissionFromLevel()
        {
            var everyEmployee = GetWorkersDescSalaryWithCommission();
            var highestCommssion = everyEmployee
                .OrderByDescending(e => e.Commission)
                .FirstOrDefault();

            return (highestCommssion.Level, decimal.Parse(highestCommssion.Commission));
        }

        public Employees GetEmployeeWithLeastProjectsBasedOnYearsWorked()
        {
            var everyEmployee = employeeRepo.ReadAll();
            var employeLeast = everyEmployee
                .OrderBy(e => e.CompletedProjects / (DateTime.Now.Year - e.StartYear + 1)) //+1 to prevent calculating with zero
                .FirstOrDefault();

            if (employeLeast is null)
            {
                throw new ArgumentException();
            }
            return employeLeast;

        }

        public List<Employees> GetSalaryOfEmployeesBasedOnBirthYear()
        {
            var everyEmployee = employeeRepo.ReadAll();
            return everyEmployee
                .OrderBy(e => e.BirthYear).ToList();
        }

        public Employees GetActiveEmployeeLeastProjects()
        {
            var everyEmployee = employeeRepo.ReadAll();
            var employee = everyEmployee
                .Where(e => e.Active)
                .OrderBy(e => e.CompletedProjects)
                .FirstOrDefault();
            if (employee is null)
            {
                throw new ArgumentException();
            }
            return employee;
        }

        public (List<Employees>? EmployeesWithHigherCommission, List<Employees>? EmployeeWithLowerSalary) GetEmployeeWithHigherCommissionThanOthersSalary()
        {
            var everyEmployee = GetWorkersDescSalaryWithCommission();

            if (!everyEmployee.Any())
            {
                throw new ArgumentException("No employees found.");
            }

            var maxSalary = everyEmployee.Max(e => e.Salary);
            var maxCommisison = everyEmployee.Max(e => int.Parse(e.Commission));

            // Find the employee with the higher commission
            var employeesWithHigherCommission = everyEmployee
                .Where(e => int.Parse(e.Commission) > maxSalary)
                .ToList();

            // Find the employees with lower salary
            var employeesWithLowestSalary = everyEmployee
                .Where(e => e.Salary < maxCommisison)
                .ToList();

            return (
                EmployeesWithLowerSalary: employeesWithLowestSalary,
                EmployeesWithHigherCommission: employeesWithHigherCommission
                );
        }

        public void Create(Employees item)
        {
            var everyEmployee = employeeRepo.ReadAll();
            if (item is null)
            {
                throw new ArgumentException("Manager is null");
            }

            var lastId = everyEmployee
                .OrderByDescending(m => m.EmployeeId)
                .Select(m => m.EmployeeId)
                .FirstOrDefault();

            if (lastId != null && lastId.StartsWith("EMP"))
            {
                var numericPart = int.Parse(lastId.Substring(3));
                var newId = $"EMP{numericPart + 1:D3}";
                item.EmployeeId = newId;
            }
            else
            {
                item.EmployeeId = "EMP001";
            }

            employeeRepo.Create(item);
        }

        public Employees Read(string id)
        {
            var employee = employeeRepo.Read(id);
            if(employee is null)
            {
                throw new ArgumentException("Employee not found");
            }
            return employee;
        }

        public void Update(Employees item, string id)
        {
            item.EmployeeId = id;
            employeeRepo.Update(item);
        }

        public void Delete(string id)
        {
            employeeRepo.Delete(id);
        }
    }
}
