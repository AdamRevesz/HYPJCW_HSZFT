using HYPJCW_HSZFT.Entities.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Logic
{
    public class Graphlogic
    {
        public static void GraphOfEmployeeSalary(List<Employee> employees)
        {
            foreach (var employee in employees)
            {
                Console.WriteLine($"\n{employee.Name,-13} {GraphGraphic(employee.Salary), 20} {employee.Salary} HUF");
            }
        }

        public static string GraphGraphic (int amount)
        {
            double scale = 20.0;  // Adjust this to control bar length relative to max amount
            double barLength = amount / 1000.0 / scale;  // Divide amount by 1000 to scale down

            // Ensure bar length is at least 1 for small values
            int barCharacters = Math.Max(1, Convert.ToInt32(barLength));
            string bar = new string('█', barCharacters);

            return bar;
        }
    }
}
