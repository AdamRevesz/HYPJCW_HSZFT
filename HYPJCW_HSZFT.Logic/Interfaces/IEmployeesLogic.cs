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
        Task Create(EmployeeDto employeeDto);
        EmployeeDto Read(string id);
        void Update(Employees item, string id);
        List<EmployeeDto> ReadAll();
        void Delete(string id);

        List<EmployeesShorterViewDto> GetEmployeesBornInThe80();
        List<EmployeesShortViewDto> GetEmployeesAtleastWorkingInTwoDepartments();
        List<Employees> GetEmployeesWorkingButPension();
        List<Employees> GetEmployeesOnPension();
        double GetAverageOfSalaryOfEmployeesOnPension();
        List<EmployeeSalaryDto> GetWorkersDescSalaryWithCommission();
        LevelDto GetRatesOfEmployeeLevels();
        List<Employees> GetEmployeesOfDepartmentWithDoctorateManager();
        AveragesalaryDto GetNumberOfEmployeesUnderOrOverTheAverageSalary();
        List<AverageEachLevelDto> GetAverageOfSalaryEachLevel();
        string WhoEarnsMoreJuniorOrMedior();
        HighestCommissionDto GetHighestCommissionFromLevel(); //decimal for more precision
        EmployeesShortViewDto GetEmployeeWithLeastProjectsBasedOnYearsWorked();
        List<EmployeeDto> GetSalaryOfEmployeesBasedOnBirthYear();
        EmployeeDto GetActiveEmployeeLeastProjects();
        (List<EmployeeSalaryDto>? EmployeesWithHigherCommission, List<EmployeeSalaryDto>? EmployeeWithLowerSalary) GetEmployeeWithHigherCommissionThanOthersSalary();

    }
}
