using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Models.DTOs;
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
            using HttpClient client = new HttpClient();

            //Fetch the Json content
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            //Read and parse ht Json
            string content = await response.Content.ReadAsStringAsync();
            JsonDocument jsDoc = JsonDocument.Parse(content);

            return jsDoc;
        }

        public static List<Managers> GetManagersJson(JsonDocument jsDoc)
        {
            List<Managers> managers = new List<Managers>();

            // Navigate to the root JSON element and select the "Employee" elements
            var root = jsDoc.RootElement;
            var managerElements = jsDoc.RootElement.EnumerateArray();
            MbaRateDto rate = new MbaRateDto();

            foreach (var element in managerElements)
            {
                Managers manager = new Managers(
                    name: element.GetProperty("Name").GetString() ?? "Unknown",
                    managerId: element.GetProperty("ManagerId").GetString() ?? "N/A",
                    birthYear: DateTime.Parse(element.GetProperty("BirthYear").GetString() ?? "0000-00-00"),
                    startOfEmployment: DateTime.Parse(element.GetProperty("StartOfEmployment").GetString() ?? "0000-00-00"),
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

                managers.Add(manager);
            }

            return managers;
        }



        public static async Task<XDocument> ImportXmlFromUrl(string url)
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

        public static List<Employees> GetEmployeesXml(XDocument xDoc)
        {
            List<Employees> employees = new List<Employees>();

            foreach (var element in xDoc.Descendants("Employee"))
            {
                Employees employee = new Employees
                {
                    EmployeeId = element.Attribute("employeeid")?.Value ?? "0",
                    Name = element.Element("Name")?.Value ?? "Null",
                    BirthYear = DateTime.Parse(element.Element("BirthYear")?.Value ?? "0000-00-00"),
                    StartYear = DateTime.Parse(element.Element("StartYear")?.Value ?? "0000-00-00"),
                    CompletedProjects = int.Parse(element.Element("CompletedProjects")?.Value ?? "0"),
                    Active = bool.Parse(element.Element("Active")?.Value ?? "false"),
                    Retired = bool.Parse(element.Element("Retired")?.Value ?? "false"),
                    Email = element.Element("Email")?.Value ?? "No email",
                    Phone = element.Element("Phone")?.Value ?? "No phone",
                    Job = element.Element("Job")?.Value ?? "No job",
                    Level = element.Element("Level")?.Value ?? "null",
                    Salary = int.Parse(element.Element("Salary")?.Value ?? "0"),
                    Commission = element.Element("Commission")?.Attribute("currency") != null
                     ? $"{element.Element("Commission")?.Attribute("currency")?.Value?? null} {element.Element("Commission")?.Value}"
                     : element.Element("Commission")?.Value ?? "0",
                    Departments = element.Element("Departments")?
                     .Elements("Department")?
                     .Select(dept => new Departments(
                      dept.Element("Name")?.Value ?? "Unknown",
                      dept.Element("DepartmentCode")?.Value ?? "000",
                      dept.Element("HeadOfDepartment")?.Value ?? "Unknown"))
                     .ToList() ?? new List<Departments>()
                };


                employees.Add(employee);
            }

            return employees;
        }

    }
}




