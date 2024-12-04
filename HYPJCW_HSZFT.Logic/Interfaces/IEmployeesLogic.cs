using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Logic.Interfaces
{
    public interface IEmployeesLogic
    {
        void Create(Employees item);
        EmployeeDto Read(string id);
        void Update(Employees item, string id);
        List<EmployeeDto> ReadAll();
        void Delete(string id);

        List<EmployeesEvenShorterViewDto> GetEmployeesBornInThe80();
        List<EmployeesShortViewDto> GetEmployeesAtleastWorkingInTwoDepartments();
        List<Employees> GetEmployeesWorkingButPension();
        List<Employees> GetEmployeesOnPension();
        double GetAverageOfSalaryOfEmployeesOnPension();
        IEnumerable<EmployeeDto> GetWorkersDescSalaryWithCommission();
        LevelDto GetRatesOfEmployeeLevels();
        List<Employees> GetEmployeesOfDepartmentWithDoctorateManager();
        string GetNumberOfEmployeesUnderOrOverTheAverageSalary();
        Dictionary<string, double> GetAverageOfSalaryEachLevel();
        string WhoEarnsMoreJuniorOrMedior();
        (string Level, decimal HighestCommission) GetHighestCommissionFromLevel(); //decimal for more precision
        EmployeesShortViewDto GetEmployeeWithLeastProjectsBasedOnYearsWorked();
        List<EmployeeDto> GetSalaryOfEmployeesBasedOnBirthYear();
        EmployeeDto GetActiveEmployeeLeastProjects();
        (List<EmployeeDto>? EmployeesWithHigherCommission, List<EmployeeDto>? EmployeeWithLowerSalary) GetEmployeeWithHigherCommissionThanOthersSalary();

    }
}
