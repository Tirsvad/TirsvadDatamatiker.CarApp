using System.Text;

namespace CarApp
{
    internal class Program
    {
        /// <summary>
        /// Prompts the user to input car information and returns the car object.
        /// </summary>
        /// <returns>The populated car object.</returns>
        static Car InputCar()
        {
            Car car = new(); // Create a new car object

            IEnumerable<FuelType> fuelTypes = Globals.DbSqlHandler.GetFuelTypes(); // Get the fuel types from the database

            char gearType; // Gear type as a character

            Console.Clear(); // Clear the console window

            Console.WriteLine("Tilføj bil");
            Console.WriteLine("==========");
            Console.Write("Indtast bilmærke: ");
            car.Brand = Console.ReadLine() ?? string.Empty;
            Console.Write("Indtast bilmodel: ");
            car.Model = Console.ReadLine() ?? string.Empty;
            Console.Write("Indtast årgang: ");
            car.Year = Convert.ToInt32(Console.ReadLine());
            do
            {
                Console.Write("Indtast geartype ([A]utomatisk/[M]anuel): ");
                gearType = char.ToUpper(Convert.ToChar(Console.Read())); // Read a character and convert it to uppercase
                Console.ReadLine();
            } while (gearType != 'A' && gearType != 'M'); // Repeat until a valid gear type is entered
            car.GearType = gearType;

            Console.WriteLine();
            Console.WriteLine("Brændstoftyper");
            Console.WriteLine("==============");

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

            Console.Write("Indtast forbrug: ");
            car.FuelEfficiency = Convert.ToSingle(Console.ReadLine());
            Console.Write("Indtast kilometerstand: ");
            car.Mileage = Convert.ToInt32(Console.ReadLine());

            return car; // Return the car object
        }

        /// <summary>
        /// Displays a list of cars and allows the user to choose one.
        /// </summary>
        /// <returns>The chosen car object.</returns>
        static Car? SelectCar()
        {
            Console.Clear();
            Console.WriteLine("Vælg bil");
            Console.WriteLine("========");
            Console.WriteLine();

            List<Car> cars = Globals.DbSqlHandler.GetCars().ToList();

            // number of columns
            List<int> columns = new() { 2, 20, 20, 20 };

            // Create Table for console
            CreateTableFrameH(columns);
            Console.WriteLine(
                "| " + "#".PadLeft(columns[0]) +
                " | " + CenterString("Mærke", columns[1]) +
                " | " + CenterString("Model", columns[2]) +
                " | " + CenterString("Kilemetertal", columns[3]) +
                " |");
            CreateTableFrameH(columns);

            for (int i = 1; i < cars.Count + 1; i++)
            {
                int ii = i - 1;
                Console.WriteLine(
                    "| " + $"{i.ToString().PadLeft(columns[0])}" +
                    " | " + $"{cars[ii].Brand}".PadRight(columns[1]) +
                    " | " + $"{cars[ii].Model}".PadRight(columns[2]) +
                    " | " + $"{cars[ii].Mileage}".PadLeft(columns[3]) +
                    " |");
            }
            CreateTableFrameH(columns);
            Console.WriteLine();
            Console.WriteLine("0. Afslut");
            Console.WriteLine();
            int choice;
            do
            {
                Console.Write("Vælg: ");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out choice) && choice > 0 && choice <= cars.Count)
                {
                    break;
                }
                else if (int.TryParse(input, out choice) && choice == 0)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Ugyldigt valg, prøv igen.");
                }
            } while (true);
            return cars[choice - 1];
        }

        /// <summary>
        /// Adds a tour to the car and calculates the fuel needed and trip cost.
        /// </summary>
        /// <param name="car">The car object to add the tour to.</param>
        /// <returns>The updated car object.</returns>
        static Car AddTour(Car car)
        {
            Console.Clear(); // Clear the console window
            IEnumerable<FuelType> fuelTypes = Globals.DbSqlHandler.GetFuelTypes(); // Get the fuel types from the database

            Console.WriteLine("Beregn turens pris");
            Console.WriteLine("==========");
            Console.Write("Simuleret tur? (j/n): ");
            char response = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();
            if (response == 'N')
            {
                car.isEngineRunning = true;
            }
            else
            {
                car.isEngineRunning = false;
            }

            Console.Write("Indtast antal kilometer: ");
            int distance = Convert.ToInt32(Console.ReadLine());

            double fuelNeeded = CalculateFuelNeeded(car, distance);
            decimal tripCost = CalculateTripCost(car, fuelNeeded);
            car.AddTour(distance);

            Console.WriteLine();
            Console.WriteLine("Tur rapport");
            Console.WriteLine("===========");

            Console.WriteLine(
                $"Bilmærke: {car.Brand}\n" +
                $"Bilmodel: {car.Model}\n" +
                $"Årgang: {car.Year}\n" +
                $"Gear: {car.GearType}\n" +
                $"Brændstof: {fuelTypes.First(ft => ft.Id == car.FuelTypeId).Name}\n" +
                $"Forbrug: {car.FuelEfficiency}\n" +
                $"Kilometerstand: {car.Mileage}\n" +
                $"Beskrivelse: {car.Description}"
            );
            Console.WriteLine();
            Console.WriteLine(
                string.Format(
                    "Tur\n" +
                    "===\n" +
                    "Antal kilometer {0} vil bruge " +
                    "{1:F2} liter brændstofforbrug" +
                    " hvilket vil kost {2:F2} kr.",
                    distance,
                    fuelNeeded,
                    tripCost
                )
            );
            Console.WriteLine();
            Console.WriteLine("\nTryk på en tast for at fortsætte...");
            Console.ReadKey(); // Wait for a key press

            return car;
        }

        /// <summary>
        /// Displays a report of the car's information.
        /// </summary>
        /// <param name="car">The car object to display the report for.</param>
        static void PrintCarDetails(Car car)
        {
            IEnumerable<FuelType> fuelTypes = Globals.DbSqlHandler.GetFuelTypes(); // Get the fuel types from the database

            Console.Clear();
            Console.WriteLine("Bilrapport");
            Console.WriteLine("==========");
            Console.WriteLine();
            Console.WriteLine("Biler");
            Console.WriteLine("=====");
            Console.WriteLine(
                $"Bilmærke: {car.Brand}\n" +
                $"Bilmodel: {car.Model}\n" +
                $"Årgang: {car.Year}\n" +
                $"Gear: {car.GearType}\n" +
                $"Brændstof: {fuelTypes.First(ft => ft.Id == car.FuelTypeId).Name}\n" +
                $"Forbrug: {car.FuelEfficiency}" + " km/l\n" +
                $"Kilometerstand: {car.Mileage}\n" +
                $"Beskrivelse: {car.Description}"
            );

            Console.WriteLine();
            Console.WriteLine("\nTryk på en tast for at fortsætte...");
            Console.ReadKey(); // Wait for a key press
        }

        /// <summary>
        /// Checks if a car's mileage is a palindrome.
        /// </summary>
        /// <param name="car">The car object containing the mileage information.</param>
        /// <returns>True if the mileage is a palindrome, otherwise false.</returns>
        static bool IsPalindrome(Car car)
        {
            string odometer = car.Mileage.ToString();

            for (int i = 0; i < odometer.Length / 2; i++)
            {
                if (odometer[i] != odometer[odometer.Length - i - 1])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Calculates the amount of fuel needed for a given distance.
        /// </summary>
        /// <param name="car">The car object containing fuel efficiency information.</param>
        /// <param name="distance">The distance to be traveled in kilometers.</param>
        /// <returns>The amount of fuel needed for the given distance.</returns>
        static double CalculateFuelNeeded(Car car, int distance)
        {
            return (double)(distance / car.FuelEfficiency);
        }

        /// <summary>
        /// Calculates the trip cost based on the fuel needed and the fuel price.
        /// </summary>
        /// <param name="car">The car object containing the fuel type information.</param>
        /// <param name="fuelNeeded">The amount of fuel needed for the trip.</param>
        /// <returns>The cost of the trip.</returns>
        static decimal CalculateTripCost(Car car, double fuelNeeded)
        {
            IEnumerable<FuelType> fuelTypes = Globals.DbSqlHandler.GetFuelTypes(); // Get the fuel types from the database
            return (decimal)(fuelNeeded * (float)fuelTypes.First(ft => ft.Id == car.FuelTypeId).Price);
        }

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
        /// Displays the menu and handles user input.
        /// </summary>
        static public void Menu()
        {
            Car? car = null; // Create a car object

            IEnumerable<FuelType> fuelTypes = Globals.DbSqlHandler.GetFuelTypes(); // Get the fuel types from the database

            int choice; // The user's choice
            do
            {
                Console.Clear(); // Clear the console window
                Console.WriteLine("Menu");
                Console.WriteLine("====");
                Console.WriteLine("1. Tilføj bil");
                Console.WriteLine("2. Vælg bil");
                Console.WriteLine("3. Tilføj tur");
                Console.WriteLine("4. Vis bilen detajler");
                Console.WriteLine("5. Tjek om kilometerstanden er et palindrom");
                Console.WriteLine("0. Afslut");
                Console.WriteLine();
                Console.Write("Vælg: ");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        car = InputCar(); // Pass a new Car object to InputCar
                        Globals.DbSqlHandler.AddCar(car); // Add the car to the database
                        break;
                    case 2:
                        car = SelectCar();
                        break;
                    case 3:
                        if (car != null)
                        {
                            car = AddTour(car);
                            if (car.isEngineRunning)
                            {
                                Globals.DbSqlHandler.UpdateCar(car); // Update the car in the database
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ingen bil valgt. Tilføj eller vælg en bil først.");
                            Console.ReadLine();
                        }
                        break;
                    case 4:
                        if (car != null)
                        {
                            PrintCarDetails(car);
                        }
                        else
                        {
                            Console.WriteLine("Ingen bil valgt. Tilføj eller vælg en bil først.");
                            Console.ReadLine();
                        }
                        break;
                    case 5:
                        if (car != null)
                        {
                            Console.WriteLine(IsPalindrome(car) ? "Kilometertallet er et palindrom." : "Kilometertallet er ikke et palindrom.");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Ingen bil valgt. Tilføj eller vælg en bil først.");
                            Console.ReadLine();
                        }
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Ugyldigt valg");
                        break;
                }
            } while (choice != 0);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            // Set the console output encoding to UTF-8 so æøå are displayed correctly
            Console.OutputEncoding = Encoding.UTF8;

            Menu();
        }
    }
}