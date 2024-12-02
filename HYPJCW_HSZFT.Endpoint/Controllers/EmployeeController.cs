using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Logic.Interfaces;
using HYPJCW_HSZFT.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
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
        public void Create([FromBody] Employees item)
        {
            this.logic.Create(item);
        }

        [HttpDelete("/Employee/{employeeId}")]
        public void Delete([FromRoute] string employeeId)
        {
            this.logic.Delete(employeeId);
        }

        [HttpGet("/Employee{employeeId}")]
        public EmployeesShortViewDto Read([FromRoute] string employeeId)
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

        [HttpGet("/Employees/levelrates")]
        public void RateOfLevels()
        {
            this.logic.GetRatesOfEmployeeLevels();
        }

        [HttpGet("/Employees/underoroveravrsalary")]
        public string EmployeesUnderOrOver()
        {
            return this.logic.GetNumberOfEmployeesUnderOrOverTheAverageSalary();
        }

        [HttpGet("/Employees/borninthe80s")]
        public List<EmployeesEvenShorterViewDto> EmployeesBornIn80()
        {
            return this.logic.GetEmployeesBornInThe80();
        }

        [HttpGet("/Employees/employeesworkingatleast2departments")]
        public List<EmployeesShortViewDto> EmployeesWorkingMultipleDepartments()
        {
            return this.logic.GetEmployeesAtleastWorkingInTwoDepartments();
        }

        [HttpGet("/Employees/employeesworkingbutpension")]
        public List<Employees> EmployeesWorkingButPension()
        {
            return this.logic.GetEmployeesWorkingButPension();
        }

        [HttpGet("/Employees/employeesonepension")]
        public List<Employees> EmployeesOnPension()
        {
            return this.logic.GetEmployeesOnPension();
        }

        [HttpGet("/Employees/averageonpension")]
        public double AverageOnpension()
        {
            return this.logic.GetAverageOfSalaryOfEmployeesOnPension();
        }

        [HttpGet("/Employees/averagesalarywithcommission")]
        public IEnumerable<EmployeeDto> AverageSalaryWithCommission()
        {
            return this.logic.GetWorkersDescSalaryWithCommission();
        }

        [HttpGet("/Employees/employeeswithdrmanagers")]
        public List<Employees> EmployeesWithDoctorManager()
        {
            return this.logic.GetEmployeesOfDepartmentWithDoctorateManager();
        }

        [HttpGet("/Employees/averageeachlevel")]
        public Dictionary<string, double> AvergaeEachLevel()
        {
            return this.logic.GetAverageOfSalaryEachLevel();
        }

        [HttpGet("/Employees/whoearnsmore")]
        public string WhoEarnsMore()
        {
            return this.logic.WhoEarnsMoreJuniorOrMedior();
        }

        [HttpGet("/Employees/highestcommissionbylevel")]
        public (string Level, decimal HighestCommission) HighestCommissionByLevel()
        {
            return this.logic.GetHighestCommissionFromLevel();
        }

        [HttpGet("/Employees/employeeleastprojects")]
        public EmployeesShortViewDto EmployeesLeastProjects()
        {
            return this.logic.GetEmployeeWithLeastProjectsBasedOnYearsWorked();
        }

        [HttpGet("/Employees/salaryofemployeesbirthyear")]
        public List<EmployeeDto> SalaryOfEmployeesBirthYear()
        {
            return this.logic.GetSalaryOfEmployeesBasedOnBirthYear();
        }

        [HttpGet("/Employees/activeemployeeleastprojects")]
        public EmployeeDto ActiveEmployeesLeastProjects()
        {
            return this.logic.GetActiveEmployeeLeastProjects();
        }

        [HttpGet("/Employees/highercommissionthanmaxsalary")]
        public (List<EmployeeDto>? EmployeesWithHigherCommission, List<EmployeeDto>? EmployeeWithLowerSalary) HigherCommissionThanMaxSalary()
        {
            return this.logic.GetEmployeeWithHigherCommissionThanOthersSalary();
        }
    }
}
