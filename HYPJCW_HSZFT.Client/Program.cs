using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Logic;
using HYPJCW_HSZFT.Models.Entity_Models;

namespace HYPJCW_HSZFT.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string url = "https://nik.siposm.hu/db/managers.json";
            var test = await ImportLogic.ImportJsFromUrl(url);

            List<Manager> employees = ImportLogic.GetManagersJson(test);

            foreach (var item in employees)
            {
                Console.WriteLine($"Name: {item.Name} \t Salary: {item.ManagerId}");
            }
        }
    }
}
