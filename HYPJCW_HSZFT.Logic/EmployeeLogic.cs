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

        public List<EmployeesShorterViewDto> GetEmployeesBornInThe80()
        {
            var everyEmployee = employeeRepo.ReadAll();
            var employees = everyEmployee.Where(p => p.BirthYear >= 1980 && p.BirthYear <= 1989).ToList()
                .Select(e => new EmployeesShorterViewDto
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
                    Commission = e.Commission,
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
                return 0;
            }
            return employee
                .Average(a => a.Salary);

        }

        public List<EmployeeSalaryDto> GetWorkersDescSalaryWithCommission()
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
                .Select(d => new EmployeeSalaryDto
                {
                    Name = d.Name,
                    Salary = d.Salary,
                    Commission = double.Parse(d.Commission),
                    Level = d.Level
                })
                .OrderByDescending(e => e.Salary)
                .ToList();
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



        public List<AverageEachLevelDto> GetAverageOfSalaryEachLevel()
        {
            var everyEmployee = employeeRepo.ReadAll();
            var grouped = everyEmployee
                .GroupBy(e => (e.Level ?? "unknown").ToLower())
                .Select(g => new AverageEachLevelDto
                {
                    Level = g.Key,
                    Average = g.Average(e => e.Salary)
                })
                .ToList();
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

        public HighestCommissionDto GetHighestCommissionFromLevel()
        {
            var everyEmployee = GetWorkersDescSalaryWithCommission();
            var highestCommission = everyEmployee
            .OrderByDescending(e => Convert.ToInt32(e.Commission))
            .FirstOrDefault();

            var result = new HighestCommissionDto
            {
                Level = highestCommission.Level,
                Average = Convert.ToInt32(highestCommission.Commission)
            };
            return result;
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
                CompletedProjects = employeLeast.CompletedProjects,
                Salary = employeLeast.Salary,
                Commission = employeLeast.Commission,
                Departments = employeLeast.Departments.Select(d => new DepartmentDto
                {
                    Name = d.Name,
                    DepartmentCode = d.DepartmentCode,
                    HeadOfDepartment = d.HeadOfDepartment
                }).ToList()
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

        public (List<EmployeeSalaryDto>? EmployeesWithHigherCommission, List<EmployeeSalaryDto>? EmployeeWithLowerSalary) GetEmployeeWithHigherCommissionThanOthersSalary()
        {
            var everyEmployee = GetWorkersDescSalaryWithCommission();

            if (!everyEmployee.Any())
            {
                throw new ArgumentException("No employees found.");
            }

            var maxSalary = everyEmployee.Max(e => e.Salary);
            var maxCommisison = everyEmployee.Max(e => Convert.ToInt32(e.Commission));

            // Find the employee with the higher commission
            var employeesWithHigherCommission = everyEmployee
                .Where(e => Convert.ToInt32(e.Commission) > maxSalary)
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

        public async Task Create(EmployeeDto employeeDto)
        {
            var employee = new Employees(employeeDto);
            await employeeRepo.Create(employeeDto);
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
