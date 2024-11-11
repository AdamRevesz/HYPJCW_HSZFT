using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Logic;

namespace HYPJCW_HSZFT.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string url = "https://raw.githubusercontent.com/siposm/oktatas-hft/refs/heads/master/BPROF-HSZF/semester-project/employees-departments.xml";
            var test = await ImportLogic.ImportXmlFromUrl(url);

            List<Employee> employees = ImportLogic.GetEmployeesXml(test);

            foreach (var item in employees)
            {
                Console.WriteLine($"Name: {item.Name} \t Salary: {item.Salary}");
            }
        }
    }
}
