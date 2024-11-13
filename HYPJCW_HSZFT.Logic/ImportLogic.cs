using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Models.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace HYPJCW_HSZFT.Logic
{
    public class ImportLogic
    {
        public static async Task<JsonDocument> ImportJsFromUrl(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                //Fetch the Json content
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                //Read and parse ht Json
                string content = await response.Content.ReadAsStringAsync();
                JsonDocument jsDoc = JsonDocument.Parse(content);

                return jsDoc;
            }
        }

        public static List<Manager> GetManagersJson(JsonDocument jsDoc)
        {
            List<Manager> managers = new List<Manager>();

            // Navigate to the root JSON element and select the "Employee" elements
            var root = jsDoc.RootElement;
            var managerElements = jsDoc.RootElement.EnumerateArray();

            foreach (var element in managerElements)
            {
                Manager manager = new Manager(
                    name: element.GetProperty("Name").GetString() ?? "Unknown",
                    managerId: element.GetProperty("ManagerId").GetString() ?? "N/A",
                    birthYear: element.GetProperty("BirthYear").GetInt32(),
                    startOfEmployment: element.GetProperty("StartOfEmployment").GetString() ?? "Unknown",
                    hasMBA: element.GetProperty("HasMBA").GetBoolean()
                );

                managers.Add(manager);
            }

            return managers;
        }



        public static async Task<XDocument> ImportXmlFromUrl(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                // Fetch the XML content
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                // Read and parse the XML content
                string content = await response.Content.ReadAsStringAsync();
                XDocument xDoc = XDocument.Parse(content);

                return xDoc;
            }
        }

        public static List<Employee> GetEmployeesXml(XDocument xDoc)
        {
            List<Employee> employees = new List<Employee>();

            foreach (var element in xDoc.Descendants("Employee"))
            {
                Employee employee = new Employee
                {
                    EmployeeId = element.Attribute("EmployeeId")?.Value ?? "0",
                    Name = element.Element("Name")?.Value ?? "Null",
                    BirthYear = int.Parse(element.Element("BirthYear")?.Value ?? "0000"),
                    StartYear = int.Parse(element.Element("StartYear")?.Value ?? "0000"),
                    CompletedProjects = int.Parse(element.Element("CompletedProjects")?.Value ?? "0"),
                    Active = bool.Parse(element.Element("Active")?.Value ?? "false"),
                    Retired = bool.Parse(element.Element("Retired")?.Value ?? "false"),
                    Email = element.Element("Email")?.Value ?? "No email",
                    Phone = element.Element("Phone")?.Value ?? "No phone",
                    Job = element.Element("Job")?.Value ?? "No job",
                    Level = element.Element("Level")?.Value ?? "null",
                    Salary = int.Parse(element.Element("Salary")?.Value ?? "0"),
                    Departments = element.Element("Departments")?
                        .Elements("Department")?
                        .Select(dept => new Departments(
                            dept.Element("Name")?.Value ?? "Unknown",
                            dept.Element("DepartmentCode")?.Value ?? "000",
                            dept.Element("HeadOfDepartment")?.Value ?? "Unknown"
                        ))
                        .ToList() ?? new List<Departments>() // Use empty list if no departments found
                };
                var commissionElement = element.Element("Commission");
                if (commissionElement != null)
                {
                    employee.Commission = new Commission
                    {
                        Amount = int.Parse(commissionElement.Value),
                        Currency = commissionElement.Attribute("currency")?.Value ?? "0000"
                    };
                }

                employees.Add(employee);
            }

            return employees;
        }

    }
}




