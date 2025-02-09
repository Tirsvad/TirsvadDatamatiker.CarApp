using System.Text;

namespace CarApp
{
    internal class Program
    {
        /// <summary>
        /// Represents a type of fuel with a name and price.
        /// </summary>
        public class FuelType
        {
            /// <summary>
            /// Gets or sets the name of the fuel type.
            /// </summary>
            public string Name { get; set; } = String.Empty;

            /// <summary>
            /// Gets or sets the price of the fuel type.
            /// </summary>
            public decimal Price { get; set; } = 0;
        }

        /// <summary>
        /// Singleton collection of fuel types.
        /// </summary>
        public class FuelTypeCollection
        {
            private static FuelTypeCollection? instance = null;
            private static readonly object padlock = new();

            private FuelTypeCollection()
            {
                Add(new FuelType { Name = "Benzin", Price = 13.49m });
                Add(new FuelType { Name = "Diesel", Price = 12.29m });
            }

            /// <summary>
            /// Gets the singleton instance of the FuelTypeCollection.
            /// </summary>
            public static FuelTypeCollection Instance
            {
                get
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new FuelTypeCollection();
                        }
                        return instance;
                    }
                }
            }

            /// <summary>
            /// Gets the list of fuel types.
            /// </summary>
            public List<FuelType> FuelTypes { get; set; } = [];

            /// <summary>
            /// Adds a fuel type to the collection.
            /// </summary>
            /// <param name="fuelType">The fuel type to add.</param>
            private void Add(FuelType fuelType)
            {
                FuelTypes.Add(fuelType);
            }
        }

        /// <summary>
        /// Represents a car with various properties.
        /// </summary>
        public class Car
        {
            /// <summary>
            /// Gets or sets the brand of the car.
            /// </summary>
            public string Brand { get; set; } = String.Empty;

            /// <summary>
            /// Gets or sets the model of the car.
            /// </summary>
            public string Model { get; set; } = String.Empty;

            /// <summary>
            /// Gets or sets the year of the car.
            /// </summary>
            public int Year { get; set; } = 0;

            /// <summary>
            /// Gets or sets the gear type of the car.
            /// </summary>
            public char GearType { get; set; } = 'M';

            /// <summary>
            /// Gets or sets the fuel type of the car.
            /// </summary>
            public FuelType FuelType { get; set; } = FuelTypeCollection.Instance.FuelTypes[0];

            /// <summary>
            /// Gets or sets the fuel efficiency of the car.
            /// </summary>
            public float FuelEfficiency { get; set; } = 0;

            /// <summary>
            /// Gets or sets the mileage of the car.
            /// </summary>
            public int Mileage { get; set; } = 0;

            /// <summary>
            /// Gets or sets the description of the car.
            /// </summary>
            public string Description { get; set; } = String.Empty;

            /// <summary>
            /// Adds a tour distance to the car's mileage.
            /// </summary>
            /// <param name="km">The distance of the tour in kilometers.</param>
            public void AddTour(int km)
            {
                Mileage += km;
            }
        }

        /// <summary>
        /// Prompts the user to input car information and returns the car object.
        /// </summary>
        /// <param name="car">The car object to populate with input data.</param>
        /// <returns>The populated car object.</returns>
        static Car InputCar()
        {
            Car car = new();
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
            FuelTypeCollection fuelTypeCollection = FuelTypeCollection.Instance; // Get the singleton instance of the FuelTypeCollection
            for (int i = 0; i < fuelTypeCollection.FuelTypes.Count; i++) // Loop through the fuel types and display them
            {
                Console.WriteLine($"{i + 1}. {fuelTypeCollection.FuelTypes[i].Name}");
            }

            int fuelTypeIndex;
            do // Repeat until a valid fuel type is entered
            {
                Console.Write("Vælg brændstoftype: ");
                fuelTypeIndex = Convert.ToInt32(Console.ReadLine()) - 1;
            } while (fuelTypeIndex < 0 || fuelTypeIndex >= fuelTypeCollection.FuelTypes.Count);
            car.FuelType = fuelTypeCollection.FuelTypes[fuelTypeIndex];

            Console.Write("Indtast forbrug: ");
            car.FuelEfficiency = Convert.ToSingle(Console.ReadLine());
            Console.Write("Indtast kilometerstand: ");
            car.Mileage = Convert.ToInt32(Console.ReadLine());

            return car; // Return the car object
        }

        /// <summary>
        /// Displays a list of cars and allows the user to choose one.
        /// </summary>
        /// <param name="cars">The list of cars to choose from.</param>
        /// <returns>The chosen car object.</returns>
        static Car? SelectCar(List<Car> cars)
        {
            Console.Clear();
            Console.WriteLine("Vælg bil");
            Console.WriteLine("========");
            Console.WriteLine();

            // Create Table for console
            //Console.WriteLine(string.Format("{0,-20} {1,-20}", "Mærke", "Model"));
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

            // 
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
            Console.Write("Vælg: ");
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

            Console.WriteLine("Tilføj tur");
            Console.WriteLine("==========");
            Console.Write("Indtast antal kilometer: ");
            int distance = Convert.ToInt32(Console.ReadLine());

            double FuelNeeded = distance / car.FuelEfficiency;
            double TripCost = FuelNeeded * (double)car.FuelType.Price;

            car.AddTour(distance);

            Console.WriteLine();
            Console.WriteLine("Tur rapport");
            Console.WriteLine("===========");

            Console.WriteLine(
                $"Bilmærke: {car.Brand}\n" +
                $"Bilmodel: {car.Model}\n" +
                $"Årgang: {car.Year}\n" +
                $"Gear: {car.GearType}\n" +
                $"Brændstof: {car.FuelType.Name}\n" +
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
            Console.ReadLine();

            return car;
        }

        /// <summary>
        /// Displays a report of the car's information.
        /// </summary>
        /// <param name="car">The car object to display the report for.</param>
        public static void Rapport(Car car)
        {
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
                $"Brændstof: {car.FuelType.Name}\n" +
                $"Forbrug: {car.FuelEfficiency}\n" +
                $"Kilometerstand: {car.Mileage}\n" +
                $"Beskrivelse: {car.Description}"
            );

            Console.WriteLine();
            Console.WriteLine("\nTryk på en tast for at fortsætte...");
            Console.ReadLine();
        }

        /// <summary>
        /// Displays the menu and handles user input.
        /// </summary>
        static public void Menu()
        {
            Car? car = null; // Create a car object
            List<Car> cars = []; // Create a list of cars

            Car car1;

            car1 = new()
            {
                Brand = "Toyota",
                Model = "Corolla",
                Year = 2010,
                GearType = 'M',
                FuelType = FuelTypeCollection.Instance.FuelTypes[0],
                FuelEfficiency = 15.0f,
                Mileage = 100000,
                Description = "Fin bil"
            };
            cars.Add(car1);

            car1 = new()
            {
                Brand = "Volkswagen",
                Model = "Golf",
                Year = 2015,
                GearType = 'A',
                FuelType = FuelTypeCollection.Instance.FuelTypes[1],
                FuelEfficiency = 18.0f,
                Mileage = 80000,
                Description = "Pæn bil"
            };
            cars.Add(car1);

            int choice;
            do
            {
                Console.Clear();
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
                        cars.Add(car);
                        break;
                    case 2:
                        car = SelectCar(cars);
                        break;
                    case 3:
                        if (car != null)
                        {
                            car = AddTour(car);
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