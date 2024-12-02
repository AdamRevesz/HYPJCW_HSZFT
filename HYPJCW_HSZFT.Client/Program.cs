using HYPJCW_HSZFT.Data;
using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Logic;
using HYPJCW_HSZFT.Models.Entity_Models;
using HYPJCW_HSZFT.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text;
using System.Xml.Linq;
using HYPJCW_HSZFT.Logic.Interfaces;
using HYPJCW_HSZFT.Models.DTOs;

namespace HYPJCW_HSZFT.Client
{
    internal class Program
    {
        private static IImportLogic _importLogic;
        // Instantiate ImportLogic

        const string baseUrl = "http://localhost:5174";
        const string baseUrl2 = "https://localhost:7108";
        const string path = "InputData/InputData.json";

        static async Task Main(string[] args)
        {
            string[] menuItems = { "Read Xml File", "Export Class Data", "Queries", "Exit" };
            string[] queryMenuItems =
            {
                "List all employees",
                "Show the rates of employee levels",
                "List employees that are over or under the average salary",
                "List employees born in the 80's",
                "List every employee working in atleast two departments",
                "List eployees on pension, but still working",
                "List employees on pension",
                "Show the average salary of employees on pension",
                "List employees descending based on salary with pension",
                "List employees that have a doctor head of department",
                "List average of salary each level",
                "Who earns more an average salary junior or a max salary medior?",
                "List highest commission each level",
                "Show the employee with the least completed project by years worked",
                "List employees salary based on birthyear",
                "Show active employee with the least completed projects",
                "Show employees with lower salary than employees with higher commission",
                "Exit back into the main menu"
            };
            bool exit = false;

            while (!exit)
            {
                int selectedIndex = 0;

                ConsoleKey key;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Use Arrow Keys to navigate and Enter to select:\n");

                    for (int i = 0; i < menuItems.Length; i++)
                    {
                        if (i == selectedIndex)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        else
                        {
                            Console.ResetColor();
                        }

                        Console.WriteLine(menuItems[i]);
                    }

                    Console.ResetColor();

                    key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                            selectedIndex = (selectedIndex == 0) ? menuItems.Length - 1 : selectedIndex - 1;
                            break;

                        case ConsoleKey.DownArrow:
                            selectedIndex = (selectedIndex == menuItems.Length - 1) ? 0 : selectedIndex + 1;
                            break;
                    }
                } while (key != ConsoleKey.Enter);


                Console.Clear();
                if (selectedIndex == 0)
                {
                    Console.WriteLine($"You selected: {menuItems[selectedIndex]}");
                    await ImportFromXml();
                }
                else if (selectedIndex == 1)
                {
                    Console.WriteLine($"You selected: {menuItems[selectedIndex]}");

                }
                else if (selectedIndex == 2)
                {
                    bool queryExit = false;
                    while (!queryExit)
                    {
                        int selectedIndex2 = 0;
                        ConsoleKey key1;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("Use Arrow Keys to navigate and Enter to select:\n");

                            for (int i = 0; i < queryMenuItems.Length; i++)
                            {
                                if (i == selectedIndex2)
                                {
                                    Console.BackgroundColor = ConsoleColor.Gray;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                }
                                else
                                {
                                    Console.ResetColor();
                                }

                                Console.WriteLine(queryMenuItems[i]);
                            }
                            Console.ResetColor();

                            key1 = Console.ReadKey(true).Key;

                            switch (key1)
                            {
                                case ConsoleKey.UpArrow:
                                    selectedIndex2 = (selectedIndex2 == 0) ? queryMenuItems.Length - 1 : selectedIndex2 - 1;
                                    break;

                                case ConsoleKey.DownArrow:
                                    selectedIndex2 = (selectedIndex2 == queryMenuItems.Length - 1) ? 0 : selectedIndex2 + 1;
                                    break;
                            }
                        } while (key1 != ConsoleKey.Enter);

                        Console.Clear();
                        if (selectedIndex2 == queryMenuItems.Length -1)
                        {
                            queryExit = true;
                        }
                        else
                        {
                            Console.WriteLine($"You selected: {queryMenuItems[selectedIndex2]}");
                            // Add your query handling logic here

                            Console.WriteLine("Press any key to return to the query menu");
                            Console.ReadKey();
                        }
                    }
                }
                else if (selectedIndex == 3)
                {
                    break;
                }
                Console.WriteLine("Press any key to return");
                Console.ReadKey();
            }
        }


        //public void List<EmployeeDto> GetAllEmployees()
        //{

                //}


        public static async Task ImportFromXml()
        {
            Console.WriteLine("Input the XML URL:");

            string xmlUrl = Console.ReadLine();
            if (string.IsNullOrEmpty(xmlUrl))
            {
                Console.WriteLine("The input is empty");
                return;
            }

            try
            {
                await _importLogic.GetEmployeesXml(xmlUrl);
                Console.WriteLine("Employees imported successfully from XML.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an error: {ex.Message}");
            }
        }

        public static async Task<bool> ImportFromJson()
        {
            Console.WriteLine("Please input a link");

            string jsonUrl = Console.ReadLine();
            var url = baseUrl + "/import/importjs";

            using var client = new HttpClient();
            if (jsonUrl is null)
            {
                throw new NullReferenceException("The input is empty");
            }
            try
            {
                var content = new StringContent($"\"{jsonUrl}\"", Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"There was an error {ex.Message}");
                return false;
            }
            return true;
        }


    }
}


