using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Logic;
using HYPJCW_HSZFT.Models.Entity_Models;

namespace HYPJCW_HSZFT.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string url ="https://nik.siposm.hu/db/managers.json";
            var test = await ImportLogic.ImportJsFromUrl(url);

            List<Managers> managers = ImportLogic.GetManagersJson(test);

            foreach (var item in managers)
            {
                Console.WriteLine($"Name: {item.Name} \t ManagerId: {item.ManagerId}");
            }

            string url2 ="https://raw.githubusercontent.com/siposm/oktatas-hft/refs/heads/master/BPROF-HSZF/semester-project/employees-departments.xml";
            var test2 = await ImportLogic.ImportXmlFromUrl(url2);

            List<Employees> employees = ImportLogic.GetEmployeesXml(test2);

            foreach (var item in employees)
            {
                Console.WriteLine($"Name: {item.Name} \t Salary: {item.Salary}");
            }
            var typesToExport = new[] {typeof(Employees), typeof(Departments)};
            var xmlDocument = ExportLogic.ExportToXml(typesToExport);
            xmlDocument.Save("exported_entities.xml");
            Console.WriteLine(xmlDocument);

            Graphlogic.GraphOfEmployeeSalary(employees);
            
        }
    }
}
