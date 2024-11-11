using HYPJCW_HSZFT.Entities.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace HYPJCW_HSZFT.Logic
{
    public class ImportLogic
    {
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
                    Commission = int.Parse(element.Element("Commission")?.Value ?? "0"),
                    Departments = element.Element("Departments")?
                        .Elements("Department")?
                        .Select(dept => new Departments(
                            dept.Element("Name")?.Value ?? "Unknown",
                            dept.Element("DepartmentCode")?.Value ?? "000",
                            dept.Element("HeadOfDepartment")?.Value ?? "Unknown"
                        ))
                        .ToList() ?? new List<Departments>() // Use empty list if no departments found
                };

                employees.Add(employee);
            }

            return employees;
        }

    }
}




