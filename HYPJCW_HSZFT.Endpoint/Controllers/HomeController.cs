using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HYPJCW_HSZFT.Endpoint.Controllers
{
    [Route("/api")]
    [ApiController]
    public class HomeController : Controller
    {
        IEmployeesLogic logic;

        public HomeController(IEmployeesLogic logic)
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
        public Employees Read([FromRoute] string employeeId)
        {
            return this.logic.Read(employeeId);
        }

        [HttpGet("/Employees")]
        public IQueryable<Employees> ReadAll()
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
        public void EmployeesUnderOrOver()
        {
            this.logic.GetNumberOfEmployeesUnderOrOverTheAverageSalary();
        }

        [HttpGet("/Employees/borninthe80s")]
        public IQueryable<Employees> EmployeesBornIn80()
        {
            return this.logic.GetEmployeesBornInThe80();
        }

        [HttpGet("/Employees/employeesworkingatleast2departments")]
        public IQueryable<Employees> EmployeesWorkingMultipleDepartments()
        {
            return this.logic.GetEmployeesAtleastWorkingInTwoDepartments();
        }

        [HttpGet("/Employees/employeesworkingbutpension")]
        public IQueryable<Employees> EmployeesWorkingButPension()
        {
            return this.logic.GetEmployeesWorkingButPension();
        }

        [HttpGet("/Employees/employeesonepension")]
        public IQueryable<Employees> EmployeesOnPension()
        {
            return this.logic.GetEmployeesOnPension();
        }

        [HttpGet("/Employees/averageonpension")]
        public double AverageOnpension()
        {
            return this.logic.GetAverageOfSalaryOfEmployeesOnPension();
        }

        [HttpGet("/Employees/averagesalarywithcommission")]
        public IEnumerable<Employees> AverageSalaryWithCommission()
        {
            return this.logic.GetWorkersDescSalaryWithCommission();
        }

        [HttpGet("/Employees/employeeswithdrmanagers")]
        public IQueryable<Employees> EmployeesWithDoctorManager()
        {
            return this.logic.GetEmployeesOfDepartmentWithDoctorateManager();
        }

        [HttpGet("/Employees/averageeachlevel")]
        public Dictionary<string, double> AvergaeEachLevel()
        {
            return this.logic.GetAverageOfSalaryEachLevel();
        }

        [HttpGet("/Employees/whoearnsmore")]
        public Employees WhoEarnsMore()
        {
            return this.logic.WhoEarnsMoreJuniorOrMedior();
        }

        [HttpGet("/Employees/highestcommissionbylevel")]
        public (string Level, decimal HighestCommission) HighestCommissionByLevel()
        {
            return this.logic.GetHighestCommissionFromLevel();
        }

        [HttpGet("/Employees/employeeleastprojects")]
        public Employees EmployeesLeastProjects()
        {
            return this.logic.GetEmployeeWithLeastProjectsBasedOnYearsWorked();
        }

        [HttpGet("/Employees/salaryofemployeesbirthyear")]
        public IQueryable<Employees> SalaryOfEmployeesBirthYear()
        {
            return this.logic.GetSalaryOfEmployeesBasedOnBirthYear();
        }

        [HttpGet("/Employees/activeemployeeleastprojects")]
        public Employees ActiveEmployeesLeastProjects()
        {
            return this.logic.GetActiveEmployeeLeastProjects();
        }

        [HttpGet("/Employees/highercommissionthanmaxsalary")]
        public (List<Employees>? EmployeesWithHigherCommission, List<Employees>? EmployeeWithLowerSalary) HigherCommissionThanMaxSalary()
        {
            return this.logic.GetEmployeeWithHigherCommissionThanOthersSalary();
        }
    }
}
