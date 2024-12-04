using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Models.DTOs;
using HYPJCW_HSZFT.Models.Entity_Models;
using Newtonsoft.Json;
using System.Text;
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
            string[] crudMainMenu =
            {
                "Create",
                "Read",
                "Update",
                "Delete",
                "Exit"
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
                       await HandleMainCrud(crudMainMenu);
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
                    await ShowRatesOfEmployees();
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
        static async Task HandleMainCrud(string[] crudMainMenu)
        {
            bool queryExit = false;
            while (!queryExit)
            {
                int selectedIndex = DisplayMenu(crudMainMenu);
                Console.Clear();
                ClearInputBuffer();
                if (selectedIndex == 0)
                {
                    await CreateMenu();
                }
                else if (selectedIndex == 1)
                {
                    await ReadMenu();
                }
                else if (selectedIndex == 2)
                {
                    await UpdateMenu();
                }
                else if (selectedIndex == 3)
                {
                    await DeleteMenu();
                }
                else if (selectedIndex == crudMainMenu.Length - 1)
                {
                    queryExit = true;
                }
                else
                {
                    Console.WriteLine($"You selected: {crudMainMenu[selectedIndex]}");
                    // Add your query handling logic here
                    Console.WriteLine("Press any key to return to the query menu");
                    Console.ReadKey();
                }
            }
        }

        public static async Task CreateMenu()
        {
            bool exitMenu = false;
            while (!exitMenu)
            {
                Console.Clear(); // Clear the console at the beginning of each loop
                Console.WriteLine("Please select an entity to create:");
                Console.WriteLine("E - Employee");
                Console.WriteLine("M - Manager");
                Console.WriteLine("D - Department");
                Console.WriteLine("ESC - Leave the menu");
                Console.WriteLine("Press the corresponding key to select.");
                ClearInputBuffer();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.E:
                        await CreateEmployee();
                        break;
                    case ConsoleKey.M:
                        await CreateManager();
                        break;
                    case ConsoleKey.D:
                        await CreateDepartment();
                        break;
                    case ConsoleKey.Escape:
                        exitMenu = true;
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }

                if (!exitMenu)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey(true);
                }
            }
        }

        static async Task ReadMenu()
        {
            bool exitMenu = false;
            while (!exitMenu)
            {
                Console.WriteLine("Please select an entity to read:");
                Console.WriteLine("E - Employee");
                Console.WriteLine("M - Manager");
                Console.WriteLine("D - Department");
                Console.WriteLine("Press the corresponding key to select.");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.E:
                        await ReadEmployee();
                        break;
                    case ConsoleKey.M:
                        await ReadManager();
                        break;
                    case ConsoleKey.D:
                        await ReadDepartment();
                        break;
                    case ConsoleKey.Escape:
                        exitMenu = true;
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            }
        }
        static async Task UpdateMenu()
        {
            bool exitMenu = false;
            while (!exitMenu)
            {

                Console.WriteLine("Please select an entity to update:");
                Console.WriteLine("E - Employee");
                Console.WriteLine("M - Manager");
                Console.WriteLine("D - Department");
                Console.WriteLine("Press the corresponding key to select.");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.E:
                        await UpdateEmployee();
                        break;
                    case ConsoleKey.M:
                        await UpdateManager();
                        break;
                    case ConsoleKey.D:
                        await UpdateDepartment();
                        break;
                    case ConsoleKey.Escape:
                        exitMenu = true;
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            }

        }
        static async Task DeleteMenu()
        {
            bool exitMenu = false;
            while (!exitMenu)
            {
                Console.WriteLine("Please select an entity to delete:");
                Console.WriteLine("E - Employee");
                Console.WriteLine("M - Manager");
                Console.WriteLine("D - Department");
                Console.WriteLine("Press the corresponding key to select.");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.E:
                        await DeleteEmployee();
                        break;
                    case ConsoleKey.M:
                        await DeleteManager();
                        break;
                    case ConsoleKey.D:
                        await DeleteDepartment();
                        break;
                    case ConsoleKey.Escape:
                        exitMenu = true;
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            }

        }
        //Create
        static async Task CreateEmployee()
        {
            Console.Clear();
            Console.WriteLine("Creating a new Employee.");

            Console.WriteLine("Enter Employee ID:");
            string employeeId = Console.ReadLine() ?? "";

            Console.WriteLine("Enter Name:");
            string name = Console.ReadLine() ?? "";

            Console.WriteLine("Enter Birth Year:");
            int birthYear = int.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine("Enter Start Year:");
            int startYear = int.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine("Enter Completed Projects:");
            int completedProjects = int.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine("Is Active? (true/false):");
            bool active = bool.Parse(Console.ReadLine().ToLower() ?? "false");

            Console.WriteLine("Is Retired? (true/false):");
            bool retired = bool.Parse(Console.ReadLine().ToLower() ?? "false");

            Console.WriteLine("Enter Email:");
            string email = Console.ReadLine() ?? "";

            Console.WriteLine("Enter Phone:");
            string phone = Console.ReadLine() ?? "";

            Console.WriteLine("Enter Job:");
            string job = Console.ReadLine() ?? "";

            Console.WriteLine("Enter Level:");
            string level = Console.ReadLine() ?? "";

            Console.WriteLine("Enter Salary:");
            int salary = int.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine("Enter Commission:");
            string commission = Console.ReadLine() ?? "";

            var departments = await GetAllDepartments();
            List<DepartmentDto> chosenDepartments = new List<DepartmentDto>();

            // Increase the array size by 1 to include the "Exit" option
            string[] departmentNames = new string[departments.Count + 1];
            for (int i = 0; i < departments.Count; i++)
            {
                departmentNames[i] = departments[i].Name;
            }
            departmentNames[departments.Count] = "Exit"; // Add "Exit" as the last option
            ClearInputBuffer();

            bool exit = false;
            while (!exit)
            {
                int selectedIndex2 = DisplayMenu(departmentNames);

                if (selectedIndex2 != departmentNames.Length - 1)
                {
                    var selectedDepartment = departments[selectedIndex2];
                    chosenDepartments.Add(new DepartmentDto
                    {
                        Name = departments[selectedIndex2].Name,
                        DepartmentCode = departments[selectedIndex2].DepartmentCode,
                        HeadOfDepartment = departments[selectedIndex2].HeadOfDepartment
                    });
                    Console.WriteLine($"Added {selectedDepartment.Name} to chosen departments.");
                }
                else
                {
                    exit = true; // Set exit to true to break the loop
                    Console.WriteLine("Exiting department selection.");
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey(true);
                }
            }
            // Create a new EmployeeDto object
            var newEmployee = new EmployeeDto
            {
                EmployeeId = employeeId,
                Name = name,
                BirthYear = birthYear,
                StartYear = startYear,
                CompletedProjects = completedProjects,
                Active = active,
                Retired = retired,
                Email = email,
                Phone = phone,
                Job = job,
                Level = level,
                Salary = salary,
                Commission = commission,
                Departments = chosenDepartments
            };

            // Call CreateEntity method
            bool success = await CreateEntity(newEmployee, "Employee");

            if (success)
            {
                Console.WriteLine("Employee created successfully!");
            }
            else
            {
                Console.WriteLine("Failed to create employee.");
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }
        public static void ClearInputBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }
        static async Task CreateManager()
        {
            Console.Clear();
            Console.WriteLine("Creating a new Manager.");

            Console.WriteLine("Enter Manager ID:");
            string managerId = Console.ReadLine() ?? "";

            Console.WriteLine("Enter Name:");
            string name = Console.ReadLine() ?? "";

            Console.WriteLine("Enter Birth Year:");
            int birthYear = int.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine("Enter Start Of Employment:");
            string startOfEmployment = Console.ReadLine() ?? "";

            Console.WriteLine("Has MBA? (true/false):");
            bool hasMba = bool.Parse(Console.ReadLine() ?? "false");

            // Create a new Managers object
            var newManager = new Managers
            {
                ManagerId = managerId,
                Name = name,
                BirthYear = birthYear,
                StartOfEmployment = startOfEmployment,
                HasMba = hasMba
            };

            // Call CreateEntity method
            bool success = await CreateEntity(newManager, "/Managers");

            if (success)
            {
                Console.WriteLine("Manager created successfully!");
            }
            else
            {
                Console.WriteLine("Failed to create manager.");
            }
        }
        static async Task CreateDepartment()
        {
            Console.Clear();
            Console.WriteLine("Creating a new Department.");

            Console.WriteLine("Enter Department Code:");
            string departmentCode = Console.ReadLine() ?? "";

            Console.WriteLine("Enter Name:");
            string name = Console.ReadLine() ?? "";

            Console.WriteLine("Enter Head Of Department:");
            string headOfDepartment = Console.ReadLine() ?? "";

            // Optionally, you can add code to select Employees

            // Create a new Departments object
            var newDepartment = new Departments
            {
                DepartmentCode = departmentCode,
                Name = name,
                HeadOfDepartment = headOfDepartment,
            };

            // Call CreateEntity method
            bool success = await CreateEntity(newDepartment, "/Department");

            if (success)
            {
                Console.WriteLine("Department created successfully!");
            }
            else
            {
                Console.WriteLine("Failed to create department.");
            }
        }
        //Read
        static async Task ReadEmployee()
        {
            Console.Clear();
            Console.WriteLine("Reading an Employee.");

            Console.WriteLine("Enter Employee ID:");
            string employeeId = Console.ReadLine() ?? "";

            var employee = await ReadEntity<EmployeeDto>(employeeId, "Employee");
            if (employee != null)
            {
                PrintProperties(employee);
            }
            else
            {
                Console.WriteLine("No employee found with the given ID.");
            }
        }
        static async Task ReadManager()
        {
            Console.Clear();
            Console.WriteLine("Reading a Manager.");

            Console.WriteLine("Enter Manager ID:");
            string managerId = Console.ReadLine() ?? "";

            var managers = await ReadEntity<Managers>(managerId, "Managers");

            if (managers != null)
            {
                managers.ToString();
            }
            else
            {
                Console.WriteLine("No manager found with the given ID.");
            }
        }
        static async Task ReadDepartment()
        {
            Console.Clear();
            Console.WriteLine("Reading a Department.");

            Console.WriteLine("Enter Department Code:");
            string departmentCode = Console.ReadLine() ?? "";

            var department = await ReadEntity<Departments>(departmentCode, "Departments");

            if (department != null)
            {
                department.ToString();
            }
            else
            {
                Console.WriteLine("No department found with the given code.");
            }
        }
        //Update
        static async Task UpdateEmployee()
        {
            Console.Clear();
            Console.WriteLine("Updating an Employee.");

            Console.WriteLine("Enter Employee ID:");
            string employeeId = Console.ReadLine() ?? "";

            // Fetch the existing employee data
            var employee = await ReadEntity<EmployeeDto>(employeeId, "Employees");
            if (employee is null)
            {
                Console.WriteLine("No employee found with the given ID.");
                return;
            }


            // Update the employee data
            Console.WriteLine("Enter Name (leave blank to keep current value):");
            string name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name)) employee.Name = name;

            Console.WriteLine("Enter Birth Year (leave blank to keep current value):");
            string birthYearInput = Console.ReadLine();
            if (int.TryParse(birthYearInput, out int birthYear)) employee.BirthYear = birthYear;

            Console.WriteLine("Enter Start Year (leave blank to keep current value):");
            string startYearInput = Console.ReadLine();
            if (int.TryParse(startYearInput, out int startYear)) employee.StartYear = startYear;

            Console.WriteLine("Enter Completed Projects (leave blank to keep current value):");
            string completedProjectsInput = Console.ReadLine();
            if (int.TryParse(completedProjectsInput, out int completedProjects)) employee.CompletedProjects = completedProjects;

            Console.WriteLine("Is Active? (true/false, leave blank to keep current value):");
            string activeInput = Console.ReadLine();
            if (bool.TryParse(activeInput, out bool active)) employee.Active = active;

            Console.WriteLine("Is Retired? (true/false, leave blank to keep current value):");
            string retiredInput = Console.ReadLine();
            if (bool.TryParse(retiredInput, out bool retired)) employee.Retired = retired;

            Console.WriteLine("Enter Email (leave blank to keep current value):");
            string email = Console.ReadLine();
            if (!string.IsNullOrEmpty(email)) employee.Email = email;

            Console.WriteLine("Enter Phone (leave blank to keep current value):");
            string phone = Console.ReadLine();
            if (!string.IsNullOrEmpty(phone)) employee.Phone = phone;

            Console.WriteLine("Enter Job (leave blank to keep current value):");
            string job = Console.ReadLine();
            if (!string.IsNullOrEmpty(job)) employee.Job = job;

            Console.WriteLine("Enter Level (leave blank to keep current value):");
            string level = Console.ReadLine();
            if (!string.IsNullOrEmpty(level)) employee.Level = level;

            Console.WriteLine("Enter Salary (leave blank to keep current value):");
            string salaryInput = Console.ReadLine();
            if (int.TryParse(salaryInput, out int salary)) employee.Salary = salary;

            Console.WriteLine("Enter Commission (leave blank to keep current value):");
            string commission = Console.ReadLine();
            if (!string.IsNullOrEmpty(commission)) employee.Commission = commission;

            // Optionally, you can add code to update Departments

            // Call UpdateEntity method
            bool success = await UpdateEntity(employeeId, employee, "Employees");

            if (success)
            {
                Console.WriteLine("Employee updated successfully!");
            }
            else
            {
                Console.WriteLine("Failed to update employee.");
            }
        }
        static async Task UpdateManager()
        {
            Console.Clear();
            Console.WriteLine("Updating a Manager.");

            Console.WriteLine("Enter Manager ID:");
            string managerId = Console.ReadLine() ?? "";

            // Fetch the existing manager data
            var manager = await ReadEntity<Managers>(managerId, "Managers");
            if (manager is null)
            {
                Console.WriteLine("No manager found with the given ID.");
                return;
            }


            // Update the manager data
            Console.WriteLine("Enter Name (leave blank to keep current value):");
            string name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name)) manager.Name = name;

            Console.WriteLine("Enter Birth Year (leave blank to keep current value):");
            string birthYearInput = Console.ReadLine();
            if (int.TryParse(birthYearInput, out int birthYear)) manager.BirthYear = birthYear;

            Console.WriteLine("Enter Start Of Employment (leave blank to keep current value):");
            string startOfEmployment = Console.ReadLine();
            if (!string.IsNullOrEmpty(startOfEmployment)) manager.StartOfEmployment = startOfEmployment;

            Console.WriteLine("Has MBA? (true/false, leave blank to keep current value):");
            string hasMbaInput = Console.ReadLine();
            if (bool.TryParse(hasMbaInput, out bool hasMba)) manager.HasMba = hasMba;

            // Call UpdateEntity method
            bool success = await UpdateEntity(managerId, manager, "Managers");

            if (success)
            {
                Console.WriteLine("Manager updated successfully!");
            }
            else
            {
                Console.WriteLine("Failed to update manager.");
            }
        }
        static async Task UpdateDepartment()
        {
            Console.Clear();
            Console.WriteLine("Updating a Department.");

            Console.WriteLine("Enter Department Code:");
            string departmentCode = Console.ReadLine() ?? "";

            // Fetch the existing department data
            var department = await ReadEntity<Departments>(departmentCode, "Departments");
            if (department is null)
            {
                Console.WriteLine("No department found with the given code.");
                return;
            }

            // Update the department data
            Console.WriteLine("Enter Name (leave blank to keep current value):");
            string name = Console.ReadLine();
            if (!string.IsNullOrEmpty(name)) department.Name = name;

            Console.WriteLine("Enter Head Of Department (leave blank to keep current value):");
            string headOfDepartment = Console.ReadLine();
            if (!string.IsNullOrEmpty(headOfDepartment)) department.HeadOfDepartment = headOfDepartment;

            // Optionally, you can add code to update Employees

            // Call UpdateEntity method
            bool success = await UpdateEntity(departmentCode, department, "Departments");

            if (success)
            {
                Console.WriteLine("Department updated successfully!");
            }
            else
            {
                Console.WriteLine("Failed to update department.");
            }
        }
        //Delete
        static async Task DeleteEmployee()
        {
            Console.Clear();
            Console.WriteLine("Deleting an Employee.");

            Console.WriteLine("Enter Employee ID:");
            string employeeId = Console.ReadLine();

            bool success = await DeleteEntity<Employees>(employeeId, "Employee");

            if (success)
            {
                Console.WriteLine("Employee deleted successfully!");
            }
            else
            {
                Console.WriteLine("Failed to delete employee.");
            }
        }
        static async Task DeleteManager()
        {
            Console.Clear();
            Console.WriteLine("Deleting a Manager.");

            Console.WriteLine("Enter Manager ID:");
            string managerId = Console.ReadLine() ?? "";

            bool success = await DeleteEntity<Managers>(managerId, "Managers");

            if (success)
            {
                Console.WriteLine("Manager deleted successfully!");
            }
            else
            {
                Console.WriteLine("Failed to delete manager.");
            }
        }
        static async Task DeleteDepartment()
        {
            Console.Clear();
            Console.WriteLine("Deleting a Department.");

            Console.WriteLine("Enter Department Code:");
            string departmentCode = Console.ReadLine() ?? "";

            bool success = await DeleteEntity<Departments>(departmentCode, "Departments");

            if (success)
            {
                Console.WriteLine("Department deleted successfully!");
            }
            else
            {
                Console.WriteLine("Failed to delete department.");
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
        //Create() Methods
        public static async Task<bool> CreateEntity<T>(T item, string endpoint) where T : class
        {
            var url = $"{baseUrl}/{endpoint}";
            using var client = new HttpClient();

            

            var jsonString = JsonConvert.SerializeObject(item);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"There was an error: {ex.Message}");
                return false;
            }
        }
        //Read Method
        static async Task<T> ReadEntity<T>(string id, string entityType)
        {
            try
            {
                string url = $"{baseUrl}/{entityType}/{id}";

                using var client = new HttpClient();
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                T entity = JsonConvert.DeserializeObject<T>(content);
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the {entityType}: {ex.Message}");
                return default;
            }
        }
        public static async Task<bool> UpdateEntity<T>(string id, T item, string endpoint) where T : class
        {
            var url = $"{baseUrl}/{endpoint}/{id}";
            using var client = new HttpClient();

            var jsonString = JsonConvert.SerializeObject(item);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PutAsync(url, content);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"There was an error: {ex.Message}");
                return false;
            }
        }
        //Delete method
        public static async Task<bool> DeleteEntity<T>(string id, string endpoint) where T : class
        {
            var url = $"{baseUrl}/{endpoint}/{id}";
            using var client = new HttpClient();
            try
            {
                var response = await client.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"There was an error: {ex.Message}");
                return false;
            }
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
        public static async Task<List<Departments>> GetAllDepartments()
        {
            var url = baseUrl + "/Departments";
            using var client = new HttpClient();
            try
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();

                var managers = JsonConvert.DeserializeObject<List<Departments>>(responseBody);
                return managers ?? new List<Departments>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"There was an error: {ex.Message}");
                return new List<Departments>();
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


