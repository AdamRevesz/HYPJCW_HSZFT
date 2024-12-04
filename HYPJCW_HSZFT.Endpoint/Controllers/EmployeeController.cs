using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Logic.Interfaces;
using HYPJCW_HSZFT.Models.DTOs;
using HYPJCW_HSZFT.Models.Entity_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace HYPJCW_HSZFT.Endpoint.Controllers
{
    [Route("/api")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeesLogic logic;

        public EmployeeController(IEmployeesLogic logic)
        {
            this.logic = logic;
        }
        [HttpPost("/Employee")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            await this.logic.Create(employeeDto);
            return Ok();
        }

        [HttpDelete("/Employee/{employeeId}")]
        public void Delete([FromRoute] string employeeId)
        {
            this.logic.Delete(employeeId);
        }

        [HttpGet("/Employee/{employeeId}")]
        public EmployeeDto Read([FromRoute] string employeeId)
        {
           return this.logic.Read(employeeId);
        }

        [HttpGet("/Employees")]
        public List<EmployeeDto> ReadAll()
        {
            return this.logic.ReadAll();
        }

        [HttpPut("/Employee/{employeeId}")]
        public void Update([FromBody] Employees item, [FromRoute] string employeeId)
        {
            this.logic.Update(item, employeeId);
        }

        [HttpGet("/Employees/level-rates")]
        public LevelDto RateOfLevels()
        {
            return this.logic.GetRatesOfEmployeeLevels();
        }

        [HttpGet("/Employees/under-or-over-avr-salary")]
        public AveragesalaryDto EmployeesUnderOrOver()
        {
            return this.logic.GetNumberOfEmployeesUnderOrOverTheAverageSalary();
        }

        [HttpGet("/Employees/born-in-the-80s")]
        public List<EmployeesShorterViewDto> EmployeesBornIn80()
        {
            return this.logic.GetEmployeesBornInThe80();
        }

        [HttpGet("/Employees/employees-working-atleast-2departments")]
        public List<EmployeesShortViewDto> EmployeesWorkingMultipleDepartments()
        {
            return this.logic.GetEmployeesAtleastWorkingInTwoDepartments();
        }

        [HttpGet("/Employees/employees-working-but-pension")]
        public List<Employees> EmployeesWorkingButPension()
        {
            return this.logic.GetEmployeesWorkingButPension();
        }

        [HttpGet("/Employees/employees-on-pension")]
        public List<Employees> EmployeesOnPension()
        {
            return this.logic.GetEmployeesOnPension();
        }

        [HttpGet("/Employees/average-on-pension")]
        public double AverageOnpension()
        {
            return this.logic.GetAverageOfSalaryOfEmployeesOnPension();
        }

        [HttpGet("/Employees/average-salary-with-commission")]
        public IEnumerable<EmployeeSalaryDto> AverageSalaryWithCommission()
        {
            return this.logic.GetWorkersDescSalaryWithCommission();
        }

        [HttpGet("/Employees/employees-with-drmanagers")]
        public List<Employees> EmployeesWithDoctorManager()
        {
            return this.logic.GetEmployeesOfDepartmentWithDoctorateManager();
        }

        [HttpGet("/Employees/average-each-level")]
        public List<AverageEachLevelDto> AverageEachLevel()
        {
            return this.logic.GetAverageOfSalaryEachLevel();
        }

        [HttpGet("/Employees/who-earns-more")]
        public IActionResult WhoEarnsMore()
        {
            var result = this.logic.WhoEarnsMoreJuniorOrMedior();
            return Ok(result);
        }

        [HttpGet("/Employees/highest-commission-by-level")]
        public HighestCommissionDto HighestCommissionByLevel()
        {
            return this.logic.GetHighestCommissionFromLevel();
        }

        [HttpGet("/Employees/employee-least-projects")]
        public EmployeesShortViewDto EmployeesLeastProjects()
        {
            return this.logic.GetEmployeeWithLeastProjectsBasedOnYearsWorked();
        }

        [HttpGet("/Employees/salary-of-employees-birthyear")]
        public List<EmployeeDto> SalaryOfEmployeesBirthYear()
        {
            return this.logic.GetSalaryOfEmployeesBasedOnBirthYear();
        }

        [HttpGet("/Employees/active-employee-leastprojects")]
        public EmployeeDto ActiveEmployeesLeastProjects()
        {
            return this.logic.GetActiveEmployeeLeastProjects();
        }

        [HttpGet("/Employees/higher-commission-than-max-salary")]
        public (List<EmployeeSalaryDto>? EmployeesWithHigherCommission, List<EmployeeSalaryDto>? EmployeeWithLowerSalary) HigherCommissionThanMaxSalary()
        {
            return this.logic.GetEmployeeWithHigherCommissionThanOthersSalary();
        }
    }
}
