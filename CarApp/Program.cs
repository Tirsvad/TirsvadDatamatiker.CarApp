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

            // Create Table for console
            Console.WriteLine(
                "+" + "".PadRight(4, '-') +
                "+" + "".PadRight(20, '-') +
                "+" + "".PadRight(20, '-') +
                "+" + "".PadRight(20, '-') + "+");
            Console.WriteLine(
                "|" + "#".PadLeft(4) +
                "|" + " Mærke".PadRight(20) +
                "|" + " Model".PadRight(20) +
                "|" + " Kilemetertal".PadRight(20) +
                "|");
            Console.WriteLine(
                "+" + "".PadRight(4, '-') +
                "+" + "".PadRight(20, '-') +
                "+" + "".PadRight(20, '-') +
                "+" + "".PadRight(20, '-') +
                "+");

            for (int i = 1; i < cars.Count + 1; i++)
            {
                int ii = i - 1;
                Console.WriteLine(
                    "|" + $"{i} ".PadLeft(4) +
                    "|" + $" {cars[ii].Brand}".PadRight(20) +
                    "|" + $" {cars[ii].Model}".PadRight(20) +
                    "|" + $"{cars[ii].Mileage} ".PadLeft(20) +
                    "|");
            }
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

            Console.WriteLine("Tilføj tur");
            Console.WriteLine("==========");
            Console.Write("Indtast antal kilometer: ");
            int distance = Convert.ToInt32(Console.ReadLine());

            double FuelNeeded = distance / car.FuelEfficiency;
            double TripCost = FuelNeeded * (double)fuelTypes.First(ft => ft.Id == car.FuelTypeId).Price;
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
                    "Antal kilometer: {0}\n" +
                    "Brændstofforbrug: {1:F2} liter\n" +
                    "Pris: {2:F2} kr.",
                    distance,
                    FuelNeeded,
                    TripCost
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
        public static void Rapport(Car car)
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
                Console.WriteLine("4. Rapport");
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
                            Globals.DbSqlHandler.UpdateCar(car); // Update the car in the database
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
                            Rapport(car);
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