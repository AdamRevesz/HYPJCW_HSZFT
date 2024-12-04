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
        public EmployeeLogic(IRepository<Employees> repo, IRepository<Departments> repo2)
        {
            employeeRepo = repo;
            departmentRepo = repo2;
        }
        public List<EmployeeDto> ReadAll()
        {
            var everyEmployee = employeeRepo.ReadAll();
            var everyDepartment = departmentRepo.ReadAll();

            var employeeDtos = everyEmployee.Select(e => new EmployeeDto
            {
                EmployeeId = e.EmployeeId,
                Name = e.Name,
                BirthYear = e.BirthYear,
                StartYear = e.StartYear,
                CompletedProjects = e.CompletedProjects,
                Active = e.Active,
                Retired = e.Retired,
                Email = e.Email,
                Phone = e.Phone,
                Job = e.Job,
                Level = e.Level,
                Salary = e.Salary,
                Commission = e.Commission,
                Departments = e.Departments?.Select(d => new DepartmentDto
                {
                    Name = d.Name,
                    DepartmentCode = d.DepartmentCode,
                    HeadOfDepartment = d.HeadOfDepartment
                }).ToList() ?? new List<DepartmentDto>()
            }).ToList();
            return employeeDtos;
        }

        public LevelDto GetRatesOfEmployeeLevels()
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

        public string GetNumberOfEmployeesUnderOrOverTheAverageSalary()
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
            return $"Under Average Employees: {rate.UnderAverage}" +
                $"\nOver Average Employees {rate.OverAverage}";
        }

        public List<EmployeesEvenShorterViewDto> GetEmployeesBornInThe80()
        {
            var everyEmployee = employeeRepo.ReadAll();
            var employees = everyEmployee.Where(p => p.BirthYear >= 1980 && p.BirthYear <= 1989).ToList()
                .Select(e => new EmployeesEvenShorterViewDto
                {
                    EmployeeId = e.EmployeeId,
                    Name = e.Name,
                    BirthYear = e.BirthYear,
                    StartYear = e.StartYear
                })
                .ToList();
            return employees;
        }

        public List<EmployeesShortViewDto> GetEmployeesAtleastWorkingInTwoDepartments()
        {
            var everyEployee = employeeRepo.ReadAll();
            var everyDepartment = departmentRepo.ReadAll();
            return everyEployee
                .Where(e => e.Departments.Count >= 2)
                .Select(e => new EmployeesShortViewDto
                {
                    Name = e.Name,
                    Salary = e.Salary,
                    Commisison = e.Commission,
                    Departments = e.Departments.Select(d => new DepartmentDto
                    {
                        Name = d.Name,
                        DepartmentCode = d.DepartmentCode,
                        HeadOfDepartment = d.HeadOfDepartment
                    }).ToList()
                })
                .ToList();
        }

        public List<Employees> GetEmployeesWorkingButPension()
        {
            var everyEmployee = employeeRepo.ReadAll();
            var workingButPension = everyEmployee.Where(e => e.Retired && e.Active).ToList();
            if (workingButPension is null)
            {
                throw new ArgumentException("There are no employees working on pension");
            }
            return workingButPension;
        }

        public List<Employees> GetEmployeesOnPension()
        {
            var everyEmployee = employeeRepo.ReadAll();
            var employeesOnPension = everyEmployee.Where(e => e.Retired && !e.Active).ToList();
            if (employeesOnPension is null)
            {
                throw new ArgumentException("There are no employees on Pension");
            }
            return employeesOnPension;
        }

        public double GetAverageOfSalaryOfEmployeesOnPension()
        {
            var everyEmployee = employeeRepo.ReadAll();
            var employee = everyEmployee
                .Where(e => e.Retired);

            if (!employee.Any())
            {
                throw new NullReferenceException("There is no employee on penison");
            }
            return employee
                .Average(a => a.Salary);

        }

        public IEnumerable<EmployeeDto> GetWorkersDescSalaryWithCommission()
        {
            var everyEmployee = employeeRepo.ReadAll()
                .Where(e => !string.IsNullOrEmpty(e.Commission))
                .ToList();

            foreach (var e in everyEmployee)
            {
                if (e.Commission.Contains("eur", StringComparison.OrdinalIgnoreCase))
                {
                    var commissionValue = decimal.Parse(e.Commission.Replace("eur", "").Trim());
                    e.Commission = (commissionValue * 400).ToString("F0"); // Convert to HUF
                }
            }
            return everyEmployee
                .Select(d => new EmployeeDto
                {
                    Name = d.Name,
                    Salary = d.Salary,
                    Commission = d.Commission
                })
                .OrderByDescending(e => e.Salary);
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
                .ToDictionary(x => x.Level, x => Math.Round(x.AverageSalary));
            return grouped;

        }

        public string WhoEarnsMoreJuniorOrMedior()
        {
            var everyEmployee = employeeRepo.ReadAll();

            var mediors = everyEmployee.Where(e => e.Level.ToLower() == "junior");
            var juniors = everyEmployee.Where(e => e.Level.ToLower() == "medior");

            var averageMediorSalary = mediors.Any() ? mediors.Average(m => m.Salary) : 0;
            var maxJuniorSalary = juniors.Any() ? juniors.Max(j => j.Salary) : 0;
            if (averageMediorSalary > maxJuniorSalary)
            {
                return $"Average Medior: {(averageMediorSalary)}";
            }
            return $"Max Junior: {maxJuniorSalary}";
        }

        public (string Level, decimal HighestCommission) GetHighestCommissionFromLevel()
        {
            var everyEmployee = GetWorkersDescSalaryWithCommission();
            var highestCommssion = everyEmployee
                .OrderByDescending(e => e.Commission)
                .FirstOrDefault();

            return (highestCommssion.Level, Math.Round(decimal.Parse(highestCommssion.Commission)));
        }

        public EmployeesShortViewDto GetEmployeeWithLeastProjectsBasedOnYearsWorked()
        {
            var everyEmployee = employeeRepo.ReadAll();
            var employeLeast = everyEmployee
                .OrderBy(e => e.CompletedProjects / (DateTime.Now.Year - e.StartYear + 1)) //+1 to prevent calculating with zero
                .FirstOrDefault();

            if (employeLeast is null)
            {
                throw new NullReferenceException("output item is null");
            }
            return new EmployeesShortViewDto
            {
                Name = employeLeast.Name,
                CompletedProjects = employeLeast.CompletedProjects
            };


        }

        public List<EmployeeDto> GetSalaryOfEmployeesBasedOnBirthYear()
        {
            var everyEmployee = employeeRepo.ReadAll();
            return everyEmployee
                .Select(e => new EmployeeDto
                {
                    Name = e.Name,
                    BirthYear = e.BirthYear,
                    Salary = e.Salary

                })
                .OrderBy(e => e.BirthYear)
                .ToList();
        }

        public EmployeeDto GetActiveEmployeeLeastProjects()
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
            return new EmployeeDto
            {
                Name = employee.Name,
                CompletedProjects = employee.CompletedProjects,
                Active = employee.Active,
                Salary = employee.Salary,
                StartYear = employee.StartYear
            };
        }

        public (List<EmployeeDto>? EmployeesWithHigherCommission, List<EmployeeDto>? EmployeeWithLowerSalary) GetEmployeeWithHigherCommissionThanOthersSalary()
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

        public EmployeeDto Read(string id)
        {
            var employee = employeeRepo.Read(id);
            if (employee == null)
            {
                throw new ArgumentException("Employee not found");
            }
            var employeeDto =
                new EmployeeDto
                {
                    EmployeeId = employee.EmployeeId,
                    Name = employee.Name,
                    BirthYear = employee.BirthYear,
                    StartYear = employee.StartYear,
                    CompletedProjects = employee.CompletedProjects,
                    Active = employee.Active,
                    Retired = employee.Retired,
                    Email = employee.Email,
                    Phone = employee.Phone,
                    Job = employee.Job,
                    Level = employee.Level,
                    Salary = employee.Salary,
                    Commission = employee.Commission,
                    Departments = employee.Departments?.Select(d => new DepartmentDto
                    {
                        Name = d.Name,
                        DepartmentCode = d.DepartmentCode,
                        HeadOfDepartment = d.HeadOfDepartment
                    }).ToList() ?? new List<DepartmentDto>()
                };
            
            return employeeDto;
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
