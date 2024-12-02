using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Logic.Interfaces;
using HYPJCW_HSZFT.Models.DTOs;
using HYPJCW_HSZFT.Models.Entity_Models;
using HYPJCW_HSZFT.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace HYPJCW_HSZFT.Logic
{
    public class ImportLogic : IImportLogic
    {
        private readonly IRepository<Managers> _managerRepo;
        private readonly IRepository<Employees> _employeeRepo;
        private readonly IRepository<Departments> _departmentRepo;
        private readonly IRepository<EmployeesOfDepartments> employeesOfDeptRepo;

        public ImportLogic(IRepository<Managers> repo1, IRepository<Employees> repo2, IRepository<Departments> repo3)
        {
            _employeeRepo = repo2;
            _managerRepo = repo1;
            _departmentRepo = repo3;
        }

        public async Task<JsonDocument> ImportJsFromUrl(string url)
        {
            using HttpClient client = new HttpClient();

            //Fetch the Json content
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            //Read and parse ht Json
            string content = await response.Content.ReadAsStringAsync();
            JsonDocument jsDoc = JsonDocument.Parse(content);

            return jsDoc;
        }

        public async Task GetManagersJson(string url)
        {

            // Fetch the JSON content
            JsonDocument raw = await ImportJsFromUrl(url);
            var root = raw.RootElement;
            var managerElements = root.EnumerateArray();
            MbaRateDto rate = new MbaRateDto();

            foreach (var element in managerElements)
            {
                Managers manager = new Managers(
                    name: element.GetProperty("Name").GetString() ?? "Unknown",
                    managerId: element.GetProperty("ManagerId").GetString() ?? "N/A",
                    birthYear: element.GetProperty("BirthYear").GetInt32(),
                    startOfEmployment: element.GetProperty("StartOfEmployment").GetString() ?? "0000-00-00",
                    hasMBA: element.GetProperty("HasMBA").GetBoolean()
                );
                if (manager.HasMba)
                {
                    rate.WithMba++;
                }
                else
                {
                    rate.WithoutMba++;
                }

                _managerRepo.Create(manager);
            }

        }



        public async Task<XDocument> ImportXmlFromUrl(string url)
        {
            using HttpClient client = new HttpClient();

            // Fetch the XML content
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // Read and parse the XML content
            string content = await response.Content.ReadAsStringAsync();
            XDocument xDoc = XDocument.Parse(content);

            return xDoc;

        }

        public async Task GetEmployeesXml(string url)
        {

            // Fetch the XML content
            XDocument xDoc = await ImportXmlFromUrl(url);

            foreach (var element in xDoc.Descendants("Employee"))
            {
                var departmentElements = element.Element("Departments")?
                    .Elements("Department")?
                    .ToList() ?? new List<XElement>();

                var departments = new List<Departments>();

                foreach (var deptElement in departmentElements)
                {
                    var departmentCode = deptElement.Element("DepartmentCode")?.Value ?? "000";
                    var existingDepartment = _departmentRepo.ReadAll()
                        .FirstOrDefault(d => d.DepartmentCode == departmentCode);

                    if (existingDepartment != null)
                    {
                        departments.Add(existingDepartment);
                    }
                    else
                    {
                        var newDepartment = new Departments(
                            name: deptElement.Element("Name")?.Value ?? "Unknown",
                            departmentCode: departmentCode,
                            headOfDepartment: deptElement.Element("HeadOfDepartment")?.Value ?? "Unknown");

                        _departmentRepo.Create(newDepartment);
                        departments.Add(newDepartment);
                    }
                }

                // Ensure no null departments are added
                departments = departments.Where(d => d != null).ToList();
                var employeeId = element.Attribute("employeeid")?.Value ?? "0";
                var existingEmployee = _employeeRepo.ReadAll()
                    .FirstOrDefault(e => e.EmployeeId == employeeId);

                if (existingEmployee == null)
                {
                    // Create the employee and assign the departments
                    Employees employee = new Employees
                    {
                        EmployeeId = employeeId,
                        Name = element.Element("Name")?.Value ?? "Null",
                        BirthYear = int.Parse(element.Element("BirthYear")?.Value),
                        StartYear = int.Parse(element.Element("StartYear")?.Value),
                        CompletedProjects = int.Parse(element.Element("CompletedProjects")?.Value ?? "0"),
                        Active = bool.Parse(element.Element("Active")?.Value ?? "false"),
                        Retired = bool.Parse(element.Element("Retired")?.Value ?? "false"),
                        Email = element.Element("Email")?.Value ?? "No email",
                        Phone = element.Element("Phone")?.Value ?? "No phone",
                        Job = element.Element("Job")?.Value ?? "No job",
                        Level = element.Element("Level")?.Value ?? "null",
                        Salary = int.Parse(element.Element("Salary")?.Value ?? "0"),
                        Commission = element.Element("Commission")?.Attribute("currency") != null
                            ? $"{element.Element("Commission")?.Attribute("currency")?.Value} {element.Element("Commission")?.Value}"
                            : element.Element("Commission")?.Value ?? "0",
                        Departments = departments
                    };

                    _employeeRepo.Create(employee);

                    foreach (var department in employee?.Departments)
                    {
                            employeesOfDeptRepo.Create(new EmployeesOfDepartments
                            {
                                EmployeesOfDepartmentsId = Guid.NewGuid().ToString(),
                                EmployeeId = employee.EmployeeId,
                                DepartmentId = department.DepartmentCode
                            });
                        
                    }
                }
            }

        }
    }
}




