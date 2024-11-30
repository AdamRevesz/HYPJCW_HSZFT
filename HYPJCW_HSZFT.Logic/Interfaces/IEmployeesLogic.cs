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
        Employees Read(string id);
        void Update(Employees item, string id);
        List<Employees> ReadAll();
        void Delete(string id);

        List<Employees> GetEmployeesBornInThe80();
        List<Employees> GetEmployeesAtleastWorkingInTwoDepartments();
        List<Employees> GetEmployeesWorkingButPension();
        List<Employees> GetEmployeesOnPension();
        double GetAverageOfSalaryOfEmployeesOnPension();
        IEnumerable<Employees> GetWorkersDescSalaryWithCommission();
        void GetRatesOfEmployeeLevels();
        List<Employees> GetEmployeesOfDepartmentWithDoctorateManager();
        AveragesalaryDto GetNumberOfEmployeesUnderOrOverTheAverageSalary();
        Dictionary<string, double> GetAverageOfSalaryEachLevel();
        Employees WhoEarnsMoreJuniorOrMedior();
        (string Level, decimal HighestCommission) GetHighestCommissionFromLevel(); //decimal for more precision
        Employees GetEmployeeWithLeastProjectsBasedOnYearsWorked();
        List<Employees> GetSalaryOfEmployeesBasedOnBirthYear();
        Employees GetActiveEmployeeLeastProjects();
        (List<Employees>? EmployeesWithHigherCommission, List<Employees>? EmployeeWithLowerSalary) GetEmployeeWithHigherCommissionThanOthersSalary();

    }
}
