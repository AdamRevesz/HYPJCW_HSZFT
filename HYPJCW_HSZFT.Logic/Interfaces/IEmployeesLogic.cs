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
        void Create(Employees item) { }
        void Read(string id) { }
        void Update(Employees item, string id) { }
        IQueryable<Employees> ReadAll();
        void Delete(string id) { }

        IQueryable<Employees> GetEmployeesBornInThe80();
        IQueryable<Employees> GetEmployeesAtleastWorkingInTwoDepartments();
        IQueryable<Employees> GetEmployeesWorkingButPension();
        IQueryable<Employees> GetEmployeesOnPension();
        double GetAverageOfSalaryOfEmployeesOnPension();
        IEnumerable<Employees> GetWorkersDescSalaryWithCommission();
        void GetRatesOfEmployeeLevels();
        IQueryable<Employees> GetEmployeesOfDepartmentWithDoctorateManager();
        AveragesalaryDto GetNumberOfEmployeesUnderOrOverTheAverageSalary();
        Dictionary<string, double> GetAverageOfSalaryEachLevel();
        Employees WhoEarnsMoreJuniorOrMedior();
        (string Level, decimal HighestCommission) GetHighestCommissionFromLevel(); //decimal for more precision
        Employees GetEmployeeWithLeastProjectsBasedOnYearsWorked();
        IQueryable<Employees> GetSalaryOfEmployeesBasedOnBirthYear();
        Employees GetActiveEmployeeLeastProjects();
        IQueryable<Employees> GetEmployeeWithHigherCommissionThanOthersSalary();
        
    }
}
