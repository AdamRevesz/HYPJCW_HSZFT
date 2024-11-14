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
            double barLength = amount / 20000;
            string bar = new string('█', Convert.ToInt32(barLength));

            return bar;
        }
    }
}
