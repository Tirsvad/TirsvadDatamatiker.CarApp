using System.Text;
using CarApp.Model;

namespace CarApp
{

    internal record struct MenuItem(string Name, int Index)
    {
        public static implicit operator (string, int)(MenuItem value)
        {
            return (value.Name, value.Index);
        }

        public static implicit operator MenuItem((string, int) value)
        {
            return new MenuItem(value.Item1, value.Item2);
        }
    }

    /// <summary>
    /// Handles CarApp's main program logic.
    /// </summary>
    public class Program
    {
        static int _paganize = 10;
        static Car? _selectedCar;

        static readonly FuelTypeList _fuelTypeList = FuelTypeList.Instance;
        static readonly CarList _carList = CarList.Instance;

        /// <summary>
        /// The year of the first car in the world.
        /// </summary>
        static int FirstAutomobileYear { get; } = 1886;

        #region Is methods

        // <summary>
        /// Checks if a car's mileage is a palindrome.
        /// </summary>
        /// <param name="car">The car object containing the mileage information.</param>
        /// <returns>True if the mileage is a palindrome, otherwise false.</returns>
        static bool IsPalindrome(Car car)
        {
            string odometer = car.Mileage.ToString() ?? string.Empty;

            for (int i = 0; i < odometer.Length / 2; i++)
            {
                if (odometer[i] != odometer[odometer.Length - i - 1])
                {
                    return false;
                }
            }
            return true;
        }

        static bool IsFuelTypeIdExists(int id)
        {
            return _fuelTypeList.Exists(id);
        }


        #endregion Is methods
        #region Car methods

        static Car? SelectCar()
        {
            List<MenuItem> menuItems = new List<MenuItem>();

            int i = 0;
            foreach (Car car in _carList.GetCars())
            {
                menuItems.Add(new MenuItem($"{car.Brand} {car.Model} {car.Year}", i));
                i++;
            }

            Header("Vælg bil");

            int selectedIndex = PaganizesMenu(menuItems, 10);
            if (selectedIndex == -1) { return null; }
            return _carList.GetCars()[selectedIndex];
        }

        static Car InputAddCar()
        {
            string? brand; // Brand of the car
            string? model; // Model of the car
            int year; // Year of the car
            char gearType; // Gear type as a character
            int mileage; // Mileage of the car
            int fuelTypeId; // Fuel type ID of the car
            double fuelEfficiency = 0f; // Fuel efficiency of the car
            string description = ""; // Description of the car

            string errMsg = "";
            Console.Clear(); // Clear the console window
            Console.CursorVisible = true;

            Header("Tilføj bil"); // Display the header

            do
            {
                Console.Write("Mærke: ");
                brand = Console.ReadLine(); // Read the brand from the console
                if (string.IsNullOrEmpty(brand)) // If the brand is empty
                {
                    PrintError("Mærke må ikke være tomt."); // Display an error message
                }
            } while (string.IsNullOrEmpty(brand)); // Repeat until a valid brand is entered

            do
            {
                Console.Write("Model: ");
                model = Console.ReadLine(); // Read the model from the console
                if (string.IsNullOrEmpty(model)) // If the model is empty
                {
                    PrintError("Model må ikke være tomt."); // Display an error message
                }
            } while (string.IsNullOrEmpty(model)); // Repeat until a valid model is entered

            do
            {
                Console.Write("Årgang: ");
                string? input = Console.ReadLine();
                if (!int.TryParse(input, out year)) // Read the year from the console
                {
                    PrintError("Årgang skal være et tal."); // Display an error message
                }
                else if (year < FirstAutomobileYear) // If the year is before the first car
                {
                    PrintError($"Årgang skal være efter {FirstAutomobileYear}."); // Display an error message
                }
            } while (year < FirstAutomobileYear); // Repeat until a valid year is entered

            do
            {
                Console.Write("Gear type (A/M): ");
                gearType = char.ToUpper(Console.ReadKey().KeyChar); // Read the gear type from the console
                Console.WriteLine(); // Move the cursor to the next line
                if (gearType != 'A' && gearType != 'M') // If the gear type is not A or M
                {
                    PrintError("Gear type skal være enten A eller M."); // Display an error message
                }
            } while (gearType != 'A' && gearType != 'M'); // Repeat until a valid gear type is entered

            do
            {
                Console.WriteLine("Brændstoftyper:"); // Display the fuel types
                foreach (FuelType fuelType in _fuelTypeList.GetFuelTypes())
                {
                    Console.WriteLine($"{fuelType.Id}: {fuelType.Name} ({fuelType.Price} kr/liter)");
                }
                Console.Write("Brændstoftype: ");
                string? input = Console.ReadLine(); // Read the fuel type from the console
                if (!int.TryParse(input, out fuelTypeId)) // If the fuel type is not a number
                {
                    fuelTypeId = -1; // Set the fuel type to -1 as it will get 0 from parse!
                    PrintError("Brændstoftype skal være et tal."); // Display an error message
                }
                else if (!IsFuelTypeIdExists(fuelTypeId)) // If the fuel type does not exist
                {
                    PrintError("Brændstoftype findes ikke."); // Display an error message
                }

            } while (!IsFuelTypeIdExists(fuelTypeId)); // Repeat until a valid fuel type is entered

            do
            {
                Console.Write("Kilometer: ");
                string? input = Console.ReadLine(); // Read the mileage from the console
                if (!int.TryParse(input, out mileage)) // If the mileage is not a number
                {
                    PrintError("Kilometer skal være et tal."); // Display an error message
                }
            } while (mileage == 0); // Repeat until a valid mileage is entered

            do
            {
                Console.WriteLine("Kilometer per liter:"); // Display the fuel efficiencies
                string? input = Console.ReadLine();
                if (!double.TryParse(input, out fuelEfficiency)) // If the fuel efficiency is not a number
                {
                    PrintError("Kilometer per liter skal være et tal."); // Display an error message
                }
            } while (fuelEfficiency == 0); // Repeat until a valid fuel efficiency is entered

            do
            {
                Console.Write("Beskrivelse: ");
                description = Console.ReadLine() ?? string.Empty; // Read the description from the console
            } while (description == null); // Repeat until a valid description is entered

            Car car = new Car(
                _carList.GenerateId(),
                brand: brand,
                model: model,
                year: year,
                gearType: gearType,
                fuelTypeId: fuelTypeId,
                fuelEfficiency: fuelEfficiency,
                mileage: mileage,
                description: errMsg
                ); // Create a new car object

            return car; // Return the car object
        }

        static void RemoveCar(Car selectedCar)
        {
            _carList.Remove(selectedCar);
            _selectedCar = null;
        }

        static void PalinDrome(Car selectedCar)
        {
            Console.Clear();
            if (IsPalindrome(selectedCar))
            {
                Console.WriteLine("Kilometer tallet er et palindrom");
            }
            else
            {
                Console.WriteLine("Kilometer tallet er ikke et palindrom");
            }
            Console.WriteLine("\nTryk på en tast for at fortsætte...");
            Console.Write(Console.ReadKey());

        }

        static double CalculateFuelNeeded(Car selectedCar, double distance)
        {
            double fuelNeeded = distance / selectedCar.FuelEfficiency;
            return fuelNeeded;
        }

        private static void CalculateTripCost(Car selectedCar)
        {
            Console.Clear();
            Console.WriteLine("Indtast kilometer for turen:");
            string input = Console.ReadLine();
            if (!double.TryParse(input, out double distance))
            {
                PrintError("Kilometer skal være et tal");
                Console.WriteLine("\nTryk på en tast for at fortsætte...");
                Console.ReadKey();
                return;
            }
            double fuelNeeded = CalculateFuelNeeded(selectedCar, distance);
            decimal fuelPrice = _fuelTypeList.GetFuelTypes()[selectedCar.FuelTypeId].Price;
            decimal tripCost = (decimal)fuelNeeded * fuelPrice;
            if (selectedCar.IsEngineRunning)
            {
                selectedCar.UpdateMileAge((int)distance);
            }
            Console.WriteLine($"Pris for turen: {tripCost:F2} kr");
            Console.WriteLine("\nTryk på en tast for at fortsætte...");
            Console.ReadKey();
        }

        #endregion Car methods
        #region Table methods

        /// <summary>
        /// Creates a horizontal table frame for the console output.
        /// </summary>
        /// <param name="columns">A list of integers representing the width of each column.</param>
        static void CreateTableFrameH(List<int> columns)
        {
            Console.Write("+");
            foreach (int column in columns)
            {
                Console.Write(new string('-', column + 2));
                Console.Write("+");
            }
            Console.WriteLine();
        }

        #endregion Table methods
        #region String methods

        /// <summary>
        /// Displays a title with the specified text.
        /// </summary>
        /// <param name="text">The text to display as a title.</param>
        public static void Header(string text)
        {
            Console.Clear();
            Console.WriteLine(text);
            Console.WriteLine(new string('=', text.Length));
            Console.WriteLine();
        }

        /// <summary>
        /// Centers the given text within a specified width.
        /// </summary>
        /// <param name="text">The text to center.</param>
        /// <param name="width">The width within which to center the text.</param>
        /// <returns>The centered text with padding.</returns>
        static string CenterString(string text, int width)
        {
            if (width <= text.Length)
            {
                return text; // Or throw an exception, or truncate the string
            }
            int padding = width - text.Length;
            int leftPadding = padding / 2;
            int rightPadding = padding - leftPadding;
            return new string(' ', leftPadding) + text + new string(' ', rightPadding);
        }

        /// <summary>
        /// Prints an error message in red text.
        /// </summary>
        /// <param name="message"></param>
        static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        #endregion String methods
        #region Rapport methods

        /// <summary>
        /// Displays a report of the car's information.
        /// </summary>
        /// <param name="car">The car object to display the report for.</param>
        static void PrintCarDetail(Car car)
        {
            Console.Clear();
            Header("Bilrapport"); // Display the header
            Console.WriteLine();
            Console.WriteLine(
                $"Bilmærke: {car.Brand}" + "\n" +
                $"Bilmodel: {car.Model}" + "\n" +
                $"Årgang: {car.Year}" + "\n" +
                $"Gear: {car.GearType}" + "\n" +
                $"Brændstof: {_fuelTypeList.GetFuelTypes()[car.FuelTypeId].Name}" + "\n" +
                $"Forbrug: {car.FuelEfficiency} km/l" + "\n" +
                $"Kilometerstand: {car.Mileage}" +
                (IsPalindrome(car) ? " ** Palindrome nummer **" : "") + "\n" + // Check if the mileage is a palindrome
                $"Beskrivelse: {car.Description}" + "\n" +
                (car.IsEngineRunning ? "Bilen er tændt" : "Bilen er slukket") + "\n"
                );
            Console.WriteLine("\nTryk på en tast for at fortsætte...");
            Console.Write(Console.ReadKey());
        }

        /// <summary>
        /// Displays a list of cars in a table format.
        /// </summary>
        static void PrintCarList()
        {
            List<int> columns = new() { 3, 20, 20, 8, 20 }; // number of columns and their width
            int pageIndex = 0;
            int pageSize = 10;
            int totalPages = (int)Math.Ceiling(_carList.GetCars().Count / (double)pageSize);
            string errorMessage = "";

            while (true)
            {
                Console.Clear();
                Header("Biloversigt");

                // Create Table for console
                CreateTableFrameH(columns); // Create a horizontal table frame
                Console.WriteLine(
                    "| " + "#".PadLeft(columns[0]) +
                    " | " + CenterString("Mærke", columns[1]) +
                    " | " + CenterString("Model", columns[2]) +
                    " | " + CenterString("Årgang", columns[3]) +
                    " | " + CenterString("Kilemetertal", columns[4]) +
                    " |"
                    );
                CreateTableFrameH(columns); // Create a horizontal table frame

                var pagedCars = _carList.GetCars()
                    .Skip(pageIndex * pageSize)
                    .Take(pageSize)
                    .ToList();

                for (int i = 0; i < pagedCars.Count; i++)
                {
                    Console.WriteLine(
                        "| " + $"{pagedCars[i].Id}".PadLeft(columns[0]) +
                        " | " + $"{pagedCars[i].Brand}".PadRight(columns[1]) +
                        " | " + $"{pagedCars[i].Model}".PadRight(columns[2]) +
                        " | " + $"{pagedCars[i].Year}".PadRight(columns[3]) +
                        " | " + $"{pagedCars[i].Mileage}".PadLeft(columns[4]) +
                        " |");

                    CreateTableFrameH(columns); // Create a horizontal table frame
                }

                Console.WriteLine("ESC: Afslut");
                Console.WriteLine($"\nSide {pageIndex + 1} af {totalPages}");
                if (pageIndex > 0)
                {
                    Console.WriteLine("F11 previous page");
                }
                if (totalPages > pageIndex + 1)
                {
                    Console.WriteLine("F12 next page");
                }

                if (errorMessage != "")
                {
                    var Position = Console.GetCursorPosition();
                    Console.SetCursorPosition(0, Position.Top + 2);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(errorMessage);
                    Console.ResetColor();
                    Console.SetCursorPosition(Position.Left, Position.Top);
                    errorMessage = "";
                }

                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                {
                    return;
                }
                else if (key.Key == ConsoleKey.F12 && pageIndex < totalPages - 1)
                {
                    pageIndex++;
                }
                else if (key.Key == ConsoleKey.F11 && pageIndex > 0)
                {
                    pageIndex--;
                }
                else
                {
                    errorMessage = $"Fejl: Gyldige taster er F1-F{pagedCars.Count}.";
                }
            }
        }

        #endregion Rapport methods
        #region Menu methods

        static void Menu()
        {
            string errorMessage = "";
            do
            {
                List<MenuItem> menuItems = new List<MenuItem> { };

                menuItems.Add(new MenuItem("Vis liste af biler", 5));
                menuItems.Add(new MenuItem("Vælg bil", 0));
                menuItems.Add(new MenuItem("Tilføj bil", 1));
                if (_selectedCar != null)
                {
                    menuItems.Add(new MenuItem($"Fjern bilen", 2));
                    menuItems.Add(new MenuItem("Bil detaljer", 3));
                    if (_selectedCar.IsEngineRunning)
                    {
                        menuItems.Add(new MenuItem("Stop motor", 4));
                    }
                    else
                    {
                        menuItems.Add(new MenuItem("Start motor", 4));
                    }
                    menuItems.Add(new MenuItem("Beregn tur pris", 7));
                    menuItems.Add(new MenuItem("Er kilometer tal et palindrom?", 22));
                }

                Console.Clear();
                Header("Main Menu");

                if (_selectedCar != null)
                {
                    Console.WriteLine($"Bil der er valgt er en: {_selectedCar.Brand} {_selectedCar.Model} med id {_selectedCar.Id}");
                }
                Console.WriteLine();

                int selectedIndex = PaganizesMenu(menuItems, 10);

                Console.SetCursorPosition(0, Console.CursorTop + 1);
                PrintError(errorMessage);

                int CTop = Console.CursorTop;

                if (_selectedCar == null)
                {
                    switch (selectedIndex)
                    {
                        case 0:
                            _selectedCar = SelectCar();
                            break;
                        case 1:
                            _carList.Add(InputAddCar());
                            break;
                        case 5:
                            PrintCarList();
                            break;
                        case -1:
                            Environment.Exit(0);
                            break;
                        default:
                            errorMessage = "Ugyldig valg";
                            break;
                    }
                }
                else
                {
                    switch (selectedIndex)
                    {
                        case 0:
                            _selectedCar = SelectCar();
                            break;
                        case 1:
                            _carList.Add(InputAddCar());
                            break;
                        case 2:
                            RemoveCar(_selectedCar);
                            break;
                        case 3:
                            PrintCarDetail(_selectedCar);
                            break;
                        case 4:
                            _selectedCar?.ToggleEngine();
                            break;
                        case 5:
                            PrintCarList();
                            break;
                        case 7:
                            CalculateTripCost(_selectedCar);
                            break;
                        case 22:
                            PalinDrome(_selectedCar);
                            break;
                        case -1:
                            Environment.Exit(0);
                            break;
                        default:
                            errorMessage = "Ugyldig valg";
                            break;
                    }
                }

            } while (true);
        }

        static int PaganizesMenu(List<MenuItem> menuItems, int pageSize, bool sorting = false)
        {
            string errorMessage = "";
            int pageIndex = 0;
            (int Left, int Top) Position;
            Console.CursorVisible = false;

            int totalPages = (int)Math.Ceiling(menuItems.Count / (double)pageSize);

            if (sorting)
            {
                menuItems = menuItems.OrderBy(m => m.Name).ToList();
            }

            var pagedMenuItems = menuItems
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .Select((item, index) => $"F{index + 1}: {item.Name}")
                .ToList();

            for (int i = 0; i < pagedMenuItems.Count; i++)
            {
                Console.WriteLine(pagedMenuItems[i], Console.WindowWidth);
            }

            Console.WriteLine("ESC: Afslut");

            Console.WriteLine($"\nSide {pageIndex + 1} af {totalPages}");
            if (pageIndex > 0)
            {
                Console.WriteLine("F11 previous page");
            }

            if (totalPages > pageIndex + 1)
            {
                Console.WriteLine("F12 next page");
            }

            if (errorMessage != "")
            {
                Position = Console.GetCursorPosition();
                Console.SetCursorPosition(0, Position.Top + 2);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(errorMessage);
                Console.ResetColor();
                Console.SetCursorPosition(Position.Left, Position.Top);
                errorMessage = "";
            }

            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape)
            {
                return -1;
            }
            else if (key.Key == ConsoleKey.F12 && pageIndex < totalPages - 1)
            {
                pageIndex++;
            }
            else if (key.Key == ConsoleKey.F11 && pageIndex > 0)
            {
                pageIndex--;
            }
            else if (key.Key >= ConsoleKey.F1 && key.Key <= ConsoleKey.F10)
            {
                int selectedIndex = key.Key - ConsoleKey.F1;
                if (selectedIndex < pagedMenuItems.Count)
                {

                    return menuItems[selectedIndex + (pageIndex * pageSize)].Index;
                }
                else
                {
                    errorMessage = "Fejl. Ikke gyldig valg";
                }
            }
            else
            {
                errorMessage = $"Fejl: Gyldige taster er F1-F{pagedMenuItems.Count}.";
            }
            return -2;
        }

        #endregion Menu methods

        static void Main(string[] args)
        {
            // Set the console output encoding to UTF-8 so æøå are displayed correctly
            Console.OutputEncoding = Encoding.UTF8;

            Menu();
        }
    }
}
