using System.Text;

using CarApp.Model;

namespace CarApp
{
#if DEBUG // Unit tests only included in debug build 
    public class Program
#else
    internal class Program
#endif
    {
        public static DbSqliteHandler DbSqlHandler = new DbSqliteHandler(Constants.dbSqliteFileName);
        public static uint worldFirstCarYear = 1886; //!< The year the first car was made

        // Car methods

        /// <summary>
        /// Prompts the user to input car information and returns the car object.
        /// </summary>
        /// <returns>The populated car object.</returns>
        static Car InputCar()
        {
            Car car = new(); // Create a new car object

            IEnumerable<FuelType> fuelTypes = Program.DbSqlHandler.GetFuelTypes(); // Get the fuel types from the database

            char gearType; // Gear type as a character

            Console.Clear(); // Clear the console window
            Header("Tilføj bil"); // Display the header

            do
            { // Loop until a valid input is entered}

                Console.Write("Indtast bilmærke: ");
                car.Brand = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrEmpty(car.Brand))
                {
                    PrintError("Mærke må ikke være tomt.");
                }
            } while (string.IsNullOrEmpty(car.Brand)); // Repeat until a valid brand is entered

            do
            { // Loop until a valid input is entered            
                Console.Write("Indtast bilmodel: ");
                car.Model = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrEmpty(car.Model))
                {
                    PrintError("Model må ikke være tomt.");
                }
            } while (string.IsNullOrEmpty(car.Model)); // Repeat until a valid model is entered

            // Validate Year input
            uint year;
            do
            {
                Console.Write("Indtast årgang: ");
                string? input = Console.ReadLine();
                if (!uint.TryParse(input, out year) || year < worldFirstCarYear)
                {
                    PrintError($"Ugyldigt input. Årgang skal være større end {worldFirstCarYear - 1}.");
                }
            } while (year < worldFirstCarYear);
            car.Year = year;

            do
            {
                Console.Write("Indtast geartype ([A]utomatisk/[M]anuel): ");
                gearType = char.ToUpper(Convert.ToChar(Console.Read())); // Read a character and convert it to uppercase
                Console.ReadLine();
            } while (gearType != 'A' && gearType != 'M'); // Repeat until a valid gear type is entered
            car.GearType = gearType;

            Console.WriteLine();
            Header("Brændstoftyper"); // Display the header

            for (int i = 0; i < fuelTypes.Count(); i++) // Loop through the fuel types and display them
            {
                Console.WriteLine($"{i + 1}. {fuelTypes.ElementAt(i).Name}");
            }

            int fuelTypeIndex;
            do // Repeat until a valid fuel type is entered
            {
                Console.Write("Vælg brændstoftype: ");
                fuelTypeIndex = Convert.ToInt32(Console.ReadLine()) - 1;
            } while (fuelTypeIndex < 0 || fuelTypeIndex >= fuelTypes.Count());
            car.FuelTypeId = fuelTypes.ElementAt(fuelTypeIndex).Id;

            do
            { // Loop until a valid input is entered
                Console.Write("Indtast forbrug: ");
                car.FuelEfficiency = Convert.ToSingle(Console.ReadLine());
                if (car.FuelEfficiency <= 0)
                {
                    PrintError("Forbrug skal være større end 0.");
                }
            } while (car.FuelEfficiency <= 0); // Repeat until a valid fuel efficiency is entered

            do
            { // Loop until a valid input is entered
                Console.Write("Indtast kilometerstand: ");
                car.Mileage = (uint)Convert.ToInt32(Console.ReadLine());
                if (car.Mileage < 0)
                {
                    PrintError("Kilometerstand skal være større end eller lig med 0.");
                }
            } while (car.Mileage < 0); // Repeat until a valid mileage is entered

            Console.Write("Indtast beskrivelse: ");
            car.Description = Console.ReadLine() ?? string.Empty;

            return car; // Return the car object
        }

        /// <summary>
        /// Displays a list of cars and allows the user to choose one.
        /// </summary>
        /// <returns>The chosen car object.</returns>
        static Car? SelectCar()
        {
            List<Car> cars = Program.DbSqlHandler.GetCars().ToList(); // Get the cars from the database
            List<int> columns = new() { 3, 20, 20, 20 }; // number of columns and their width
            System.ConsoleKeyInfo choice; // The user's choice
            int elementCounter = 0;
            int i;

            do
            {
                Console.Clear();
                Header("Vælg bil");

                // Create Table for console
                CreateTableFrameH(columns); // Create a horizontal table frame
                Console.WriteLine(
                    "| " + "#".PadLeft(columns[0]) +
                    " | " + CenterString("Mærke", columns[1]) +
                    " | " + CenterString("Model", columns[2]) +
                    " | " + CenterString("Kilemetertal", columns[3]) +
                    " |");
                CreateTableFrameH(columns); // Create a horizontal table frame

                for (i = 1 + elementCounter; i < cars.Count + 1; i++)
                {
                    if (i > elementCounter + 10)
                    {
                        break;
                    }
                    int ii = i - 1;

                    string option = $"F{(i - elementCounter)}";
                    Console.WriteLine(
                        "| " + $"{option.PadLeft(columns[0])}" +
                        " | " + $"{cars[ii].Brand}".PadRight(columns[1]) +
                        " | " + $"{cars[ii].Model}".PadRight(columns[2]) +
                        " | " + $"{cars[ii].Mileage}".PadLeft(columns[3]) +
                        " |");
                }


                CreateTableFrameH(columns); // Create a horizontal table frame
                Console.WriteLine();
                if (elementCounter > 0)
                {
                    Console.WriteLine("F11: Forrige side ");
                }
                if (elementCounter + 10 < cars.Count - 1)
                {
                    Console.WriteLine("F12: Næste side ");
                }
                Console.WriteLine("ESC: Afslut");
                choice = Console.ReadKey();

                switch (choice.Key)
                {
                    case ConsoleKey.F1:
                        return cars[elementCounter];
                    case ConsoleKey.F2:
                        if (elementCounter + 1 < cars.Count)
                        {
                            return cars[elementCounter + 1];
                        }
                        break;
                    case ConsoleKey.F3:
                        if (elementCounter + 2 < cars.Count)
                        {
                            return cars[elementCounter + 2];
                        }
                        break;
                    case ConsoleKey.F4:
                        if (elementCounter + 3 < cars.Count)
                        {
                            return cars[elementCounter + 3];
                        }
                        break;
                    case ConsoleKey.F5:
                        if (elementCounter + 4 < cars.Count)
                        {
                            return cars[elementCounter + 4];
                        }
                        break;
                    case ConsoleKey.F6:
                        if (elementCounter + 5 < cars.Count)
                        {
                            return cars[elementCounter + 5];
                        }
                        break;
                    case ConsoleKey.F7:
                        if (elementCounter + 6 < cars.Count)
                        {
                            return cars[elementCounter + 6];
                        }
                        break;
                    case ConsoleKey.F8:
                        if (elementCounter + 7 < cars.Count)
                        {
                            return cars[elementCounter + 7];
                        }
                        break;
                    case ConsoleKey.F9:
                        if (elementCounter + 8 < cars.Count)
                        {
                            return cars[elementCounter + 8];
                        }
                        break;
                    case ConsoleKey.F10:
                        if (elementCounter + 9 < cars.Count)
                        {
                            return cars[elementCounter + 9];
                        }
                        break;
                    case ConsoleKey.F11:
                        if (elementCounter > 0)
                        {
                            elementCounter -= 10;
                        }
                        break;
                    case ConsoleKey.F12:
                        if (elementCounter + 10 < cars.Count)
                        {
                            elementCounter += 10;
                        }
                        break;
                    case ConsoleKey.Escape:
                        Console.Write(choice.KeyChar);
                        return null;
                    default:
                        Console.WriteLine("Ugyldigt valg.");
                        Console.WriteLine("Tast for at forsætte.");
                        Console.ReadKey();
                        break;
                }
            } while (true);
        }

        /// <summary>
        /// Displays a report of the car's information.
        /// </summary>
        /// <param name="car">The car object to display the report for.</param>
        static void PrintCarDetails(IEnumerable<Car> cars)
        {
            IEnumerable<FuelType> fuelTypes = DbSqlHandler.GetFuelTypes(); // Get the fuel types from the database

            Console.Clear();
            Header("Bilrapport"); // Display the header
            Console.WriteLine();
            foreach (Car car in cars)
            {
                Console.WriteLine(
                    $"Bilmærke: {car.Brand}" + "\n" +
                    $"Bilmodel: {car.Model}" + "\n" +
                    $"Årgang: {car.Year}" + "\n" +
                    $"Gear: {car.GearType}" + "\n" +
                    $"Brændstof: {fuelTypes.First(ft => ft.Id == car.FuelTypeId).Name}" + "\n" +
                    $"Forbrug: {car.FuelEfficiency} km/l" + "\n" +
                    $"Kilometerstand: {car.Mileage}" +
                    (Car.IsPalindrome(car) ? " ** Palindrome nummer **" : "") + "\n" + // Check if the mileage is a palindrome
                    $"Beskrivelse: {car.Description}" + "\n" +
                    (car.IsEngineRunning ? "Bilen er tændt" : "Bilen er slukket") + "\n"
                );
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("\nTryk på en tast for at fortsætte...");
            Console.ReadKey(); // Wait for a key press
        }

        static void TripCost(Car car)
        {
            Console.Clear();
            Header("Beregn tur omkostning"); // Display the header
            Console.Write($"Indtast {(car.IsEngineRunning ? "en simuleret tur" : "turens")} længde? ");
            if (int.TryParse(Console.ReadLine(), out int km))
            {
                if (car.IsEngineRunning)
                {
                    car.UpdateMileAge(km); // Update the car's mileage
                }
                double fuelNeeded = Car.CalculateFuelNeeded(car, km);
                Console.WriteLine("\n" + $"Turens omkostning bliver {Car.CalculateTripCost(car, fuelNeeded):F2} kr og der skal bruges {fuelNeeded:F2} liter brændstof" + "\n");
                Console.WriteLine("\nTryk på en tast for at fortsætte...");
                Console.ReadKey(); // Wait for a key press

            }
            else
            {
                Console.WriteLine("Ugyldigt input. Indtast et gyldigt tal.");
                Console.WriteLine("\nTryk på en tast for at fortsætte...");
                Console.ReadKey();
            }
        }

        // Table methods

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

        // String methods

        /// <summary>
        /// Displays a header with the specified text.
        /// </summary>
        /// <param name="text">The text to display in the header.</param>
        public static void Header(string text)
        {
#if DEBUG
            if (Console.IsOutputRedirected == false)
            {
                Console.Clear();
            }
#else
            Console.Clear();
#endif
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

        // Menu methods

        /// <summary>
        /// Displays the menu and handles user input.
        /// </summary>
        private static void Menu()
        {
            // Declare variables
            Car? car = null; // Create a car object
            System.ConsoleKeyInfo choice; // The user's choice
            IEnumerable<FuelType> fuelTypes = DbSqlHandler.GetFuelTypes(); // Get the fuel types from the database

            // Loop until the user exits the program
            do
            {
                Console.Clear(); // Clear the console window
                Header("Menu"); // Display the header
                Console.WriteLine("F1: Tilføj en bil...");
                Console.WriteLine("F2: Slet en bil...");
                Console.WriteLine("F3: Vælg bil...");
                if (car != null) // If a car is selected
                {
                    Console.WriteLine("F4: Beregn tur omkostning...");
                    Console.WriteLine($"F5: Vis {car.Brand} {car.Model} detajler");
                    Console.WriteLine($"F6: Tjek om {car.Brand} {car.Model} kilometerstanden er et palindrom");
                    Console.WriteLine($"F7: {(car.IsEngineRunning ? "Sluk" : "Tænd")} motoren");
                } // End if a car is selected
                Console.WriteLine("F8: Database Menu...");
                Console.WriteLine("F9: Print alle bilers rapporter");
                Console.WriteLine("ESC: Afslut");
                Console.WriteLine();

                choice = Console.ReadKey(); // Wait for a key press

                switch (choice.Key) // Switch on the user's choice
                {
                    case ConsoleKey.F1: // If the user pressed F1
                        car = InputCar(); // Pass a new Car object to InputCar
                        DbSqlHandler.AddCar(car); // Add the car to the database
                        break;
                    case ConsoleKey.F2:
                        car = SelectCar();
                        if (car != null)
                        {
                            DbSqlHandler.DeleteCar(car); // Delete the car from the database
                        }
                        break;
                    case ConsoleKey.F3:
                        car = SelectCar();
                        break;
                    case ConsoleKey.F4:
                        if (car != null)
                        {
                            TripCost(car);
                        }
                        else
                        {
                            Console.WriteLine("Ingen bil valgt. Tilføj eller vælg en bil først.");
                            Console.ReadKey();
                        }
                        break;
                    case ConsoleKey.F5:
                        if (car != null)
                        {
                            IEnumerable<Car> SelectedCar = new List<Car> { car };
                            PrintCarDetails(SelectedCar);
                        }
                        else
                        {
                            Console.WriteLine("Ingen bil valgt. Tilføj eller vælg en bil først.");
                            Console.WriteLine("\nTryk på en tast for at fortsætte...");
                            Console.ReadKey(); // Wait for a key press
                        }
                        break;
                    case ConsoleKey.F6:
                        if (car != null)
                        {
                            Console.WriteLine(Car.IsPalindrome(car) ? "Kilometertallet er et palindrom." : "Kilometertallet er ikke et palindrom.");
                            Console.WriteLine("\nTryk på en tast for at fortsætte...");
                            Console.ReadKey(); // Wait for a key press
                        }
                        else
                        {
                            Console.WriteLine("Ingen bil valgt. Tilføj eller vælg en bil først.");
                            Console.WriteLine("\nTryk på en tast for at fortsætte...");
                            Console.ReadKey(); // Wait for a key press
                        }
                        break;
                    case ConsoleKey.F7:
                        if (car != null)
                        {
                            if (car.IsEngineRunning)
                            {
                                car.StopEngine();
                            }
                            else
                            {
                                car.StartEngine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ingen bil valgt. Tilføj eller vælg en bil først.");
                            Console.WriteLine("\nTryk på en tast for at fortsætte...");
                            Console.ReadKey(); // Wait for a key press
                        }
                        break;
                    case ConsoleKey.F8:
                        MenuDatabase();
                        break;
                    case ConsoleKey.F9:
                        IEnumerable<Car> cars = DbSqlHandler.GetCars();
                        PrintCarDetails(cars);
                        break;
                    case ConsoleKey.Escape: // If the user pressed ESC
                        Console.Write(choice.KeyChar);
                        return; // Exit the method (and the program)
                    default:
                        Console.WriteLine("Ugyldigt valg.");
                        Console.WriteLine("\nTryk på en tast for at fortsætte...");
                        Console.ReadKey(); // Wait for a key press
                        break;
                }
            } while (true); // End loop
        }

        /// <summary>
        /// Displays the database menu and handles user input.
        /// </summary>
        static void MenuDatabase()
        {
            do // Loop until the user exits the database menu
            {
                Console.Clear(); // Clear the console window
                Header("Database Menu"); // Display the header
                Console.WriteLine("F1: Import json to database (It will clear all existing data in db");
                Console.WriteLine("F2: Export database to json");
                // TODO Console.WriteLine("F3: Clear database");
                Console.WriteLine("ESC: Afslut");
                ConsoleKeyInfo choice = Console.ReadKey(); // Wait for a key press
                switch (choice.Key)
                {
                    case ConsoleKey.F1:
                        DbSqlHandler.ImportFromJson();
                        break;
                    case ConsoleKey.F2:
                        DbSqlHandler.ExportToJson();
                        break;
                    /* TODO
                case ConsoleKey.F3:
                    DbSqlHandler.ClearDatabase();
                break;
                    */
                    case ConsoleKey.Escape:
                        Console.Write(choice.KeyChar);
                        return; // Exit the method
                    default:
                        Console.WriteLine("Ugyldigt valg.");
                        Console.WriteLine("Tast for at forsætte.");
                        Console.ReadKey();
                        break;
                }
            } while (true);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(/* string[] args */)
        {
            // Set the console output encoding to UTF-8 so æøå are displayed correctly
            Console.OutputEncoding = Encoding.UTF8;

            Menu();
        }
    }
}
