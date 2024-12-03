using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Models.DTOs;
using HYPJCW_HSZFT.Models.Entity_Models;
using Newtonsoft.Json;
using System.Text.Json;

namespace HYPJCW_HSZFT.Client
{
    internal class Program
    {
        const string baseUrl = "http://localhost:5174";
        const string baseUrl2 = "https://localhost:7185";
        const string path = "InputData/InputData.json";

        static async Task Main(string[] args)
        {
            string[] menuItems = { "Read Xml File", "Read Json File", "Export Class Data", "CRUD", "Graphs", "Queries", "Exit" };
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
                int selectedIndex = DisplayMenu(menuItems);
                Console.Clear();
                switch (selectedIndex)
                {
                    case 0:
                        Console.WriteLine($"You selected: {menuItems[selectedIndex]}");
                        await ImportFromXml();
                        break;
                    case 1:
                        Console.WriteLine($"You selected: {menuItems[selectedIndex]}");
                        await ImportFromJson();
                        break;
                    case 2:
                        await ExportClasses();
                        break;
                    case 3:
                        
                        break;
                    case 4:
                        GraphOfSalaries(await GetAllEmployeesList());
                        break;
                    case 5:
                        await HandleEmployeeQueries(queryMenuItems);
                        break;
                    case 6:
                        exit = true;
                        break;
                }
                if (!exit)
                {
                    Console.WriteLine("Press any key to return");
                    Console.ReadKey();
                }
            }
        }

        //Menu stops here

        static int DisplayMenu(string[] menuItems)
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
            return selectedIndex;
        }

        static async Task HandleEmployeeQueries(string[] queryMenuItems)
        {
            bool queryExit = false;
            while (!queryExit)
            {
                int selectedIndex2 = DisplayMenu(queryMenuItems);
                Console.Clear();
                if (selectedIndex2 == 0)
                {
                    StringFromList<EmployeeDto>(await GetAllEmployeesList());
                    Console.WriteLine("Press any key to continue");
                    Console.ReadLine();
                    Console.Clear();
                }
                else if (selectedIndex2 == 1)
                {
                    ShowRatesOfEmployees();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadLine();
                    Console.Clear();
                }
                else if (selectedIndex2 == queryMenuItems.Length - 1)
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
        //Menu stops here

        //Export class data
        public static async Task<bool> ExportClasses()
        {
            var url = baseUrl + "/export/exportclassdata";

            using var client = new HttpClient();
            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"There was an error {ex.Message}");
                return false;
            }
            return true;
        }
        //Import from XML
        public static async Task<bool> ImportFromXml()
        {
            Console.WriteLine("Please input a link");

            string xmlUrl = Console.ReadLine();
            var url = baseUrl + "/import/importxml";

            using var client = new HttpClient();
            if (xmlUrl is null)
            {
                throw new NullReferenceException("The input is empty");
            }
            url += $"?url={xmlUrl}";
            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"There was an error {ex.Message}");
                return false;
            }
            return true;
        }
        //Import from Json
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
            url += $"?url={jsonUrl}";
            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"There was an error {ex.Message}");
                return false;
            }
            return true;
        }
        //ReadAll() methods
          //Employees
        public static async Task<List<EmployeeDto>> GetAllEmployeesList()
        {
            var url = baseUrl + "/Employees";
            using var client = new HttpClient();
            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                var employees = JsonConvert.DeserializeObject<List<EmployeeDto>>(responseBody);
                return employees ?? new List<EmployeeDto>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"There was an error: {ex.Message}");
                return new List<EmployeeDto>();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"JSON Deserialization error: {ex.Message}");
                throw;
            }
        }
          //Managers
        public static async Task<List<Managers>> GetAllManagers()
        {
            var url = baseUrl + "/Managers";
            using var client = new HttpClient();
            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                var managers = JsonConvert.DeserializeObject<List<Managers>>(responseBody);
                return managers ?? new List<Managers>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"There was an error: {ex.Message}");
                return new List<Managers>();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"JSON Deserialization error: {ex.Message}");
                throw;
            }
        }
        //Read Methods
          //Employee
        public static async Task<List<EmployeeDto>> GetEmployee(string Id)
        {
            var url = baseUrl + "/Employees/{id}";
            using var client = new HttpClient();
            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                var employees = JsonConvert.DeserializeObject<List<EmployeeDto>>(responseBody);
                return employees ?? new List<EmployeeDto>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"There was an error: {ex.Message}");
                return new List<EmployeeDto>();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"JSON Deserialization error: {ex.Message}");
                throw;
            }
        }

        //Show the rates of employees per level
        public static async Task<bool> ShowRatesOfEmployees()
        {
            var url = baseUrl + "/Employees/levelrates";

            using var client = new HttpClient();
            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON content into a LevelDto object
                LevelDto rate = JsonConvert.DeserializeObject<LevelDto>(content);

                // Display the results
                Console.WriteLine($"\nJunior: {GraphGraphicSmallNumber(rate.Junior)} {rate.Junior}");
                Console.WriteLine($"\nMedior: {GraphGraphicSmallNumber(rate.Medior)} {rate.Medior}");
                Console.WriteLine($"\nSenior: {GraphGraphicSmallNumber(rate.Senior)} {rate.Senior}");
                Console.WriteLine($"\nNone: {GraphGraphicSmallNumber(rate.None)} {rate.None}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"There was an error: {ex.Message}");
                return false;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error parsing JSON response: {ex.Message}");
                return false;
            }
            return true;
        }

        //Generic Methods to print out the API calls
        public static void StringFromList<T>(List<T> list) where T : class
        {
            foreach (var item in list)
            {
                PrintProperties(item);
                Console.WriteLine(new string('-', 20));
            }
        }

        //Calling the method recursively when encountering a nested list
        private static void PrintProperties(object item)
        {
            var properties = item.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(item, null) ?? "null";
                if (value is System.Collections.IEnumerable enumerable && !(value is string))
                {
                    Console.WriteLine($"{prop.Name}:");
                    foreach (var subItem in enumerable)
                    {
                        if (subItem is not null && subItem.GetType().IsClass && subItem.GetType() != typeof(string))
                        {
                            PrintProperties(subItem);
                        }
                        else
                        {
                            Console.WriteLine($"  - {subItem}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"{prop.Name}: {value}");
                }
            }
        }

        //GraphOfEmployeeSalary
        
        //Graph Design
        public static string GraphGraphic(int amount)
        {
            double scale = 20.0;  // Adjust this to control bar length relative to max amount
            double barLength = amount / 1000.0 / scale;  // Divide amount by 1000 to scale down

            // Ensure bar length is at least 1 for small values
            int barCharacters = Math.Max(1, Convert.ToInt32(barLength));
            string bar = new string('█', barCharacters);

            return bar;
        }
        //For smaller numbers
        public static string GraphGraphicSmallNumber(int amount)
        {
            double scale = 1.0;
            double barLength = amount / scale;

            int barCharacters = Math.Max(1, Convert.ToInt32(barLength));
            string bar = new string('█', barCharacters);

            return bar;
        }
        //Graph of employee salary
        public static void GraphOfSalaries<T>(List<T> list) where T : class
        {
            var salaryProperty = typeof(T).GetProperty("Salary");
            var nameProperty = typeof(T).GetProperty("Name");

            if (salaryProperty == null || nameProperty == null)
            {
                Console.WriteLine("The provided type does not have 'Salary' or 'Name' properties.");
                return;
            }

            foreach (var item in list)
            {
                var salaryValue = salaryProperty.GetValue(item, null);
                var nameValue = nameProperty.GetValue(item, null);

                if (salaryValue is int salary && nameValue is string name)
                {
                    Console.WriteLine($"\n{name}: {GraphGraphic(salary)} {salary}");
                }
            }
        }
    }
}


