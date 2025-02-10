using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            public int FuelType { get; set; }

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
        /// Interface for car collection.
        /// </summary>
        public class ICarCollection
        {
            /// <summary>
            /// Gets or sets the list of carCollection.
            /// </summary>
            public List<Car> Cars { get; set; } = new List<Car>();
        }

        /// <summary>
        /// Singleton collection of cars.
        /// </summary>
        public class CarCollection : ICarCollection
        {
            private static CarCollection? instance = null;
            private static readonly object padlock = new();
            private CarCollection() { }

            /// <summary>
            /// Gets the singleton instance of the CarCollection.
            /// </summary>
            public static CarCollection Instance
            {
                get
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new CarCollection();
                        }
                        return instance;
                    }
                }
            }

            /// <summary>
            /// Adds a car to the collection.
            /// </summary>
            /// <param name="car">The car to add.</param>
            public void Add(Car car)
            {
                Cars.Add(car);
            }
        }

        /// <summary>
        /// Represents file data with a version.
        /// </summary>
        public class FileData
        {
            /// <summary>
            /// Gets or sets the file version.
            /// </summary>
            public int FileVersion { get; set; } = 1;

            /// <summary>
            /// Initializes a new instance of the <see cref="FileData"/> class.
            /// </summary>
            public FileData() { }

            /// <summary>
            /// Initializes a new instance of the <see cref="FileData"/> class with a specified version.
            /// </summary>
            /// <param name="fileVersion">The file version.</param>
            [JsonConstructor]
            public FileData(int fileVersion)
            {
                FileVersion = fileVersion;
            }
        }

        /// <summary>
        /// Handles file operations for saving and loading data.
        /// </summary>
        static class FileHandler
        {
            /// <summary>
            /// Saves the car collection data to a file.
            /// </summary>
            /// <param name="fileName">The name of the file to save the data to.</param>
            public static void SaveData(string fileName)
            {
                CarCollection carCollection = CarCollection.Instance;

                if (carCollection.Cars == null || carCollection.Cars.Count == 0)
                {
                    Console.WriteLine("Cars list is empty. Adding default cars before saving.");
                    return;
                }

                DataContainer dataContainer = new DataContainer
                {
                    FileData = new FileData(),
                    Cars = carCollection.Cars
                };

                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(dataContainer, options);
                File.WriteAllText(fileName, jsonString);
            }

            /// <summary>
            /// Loads the car collection data from a file.
            /// </summary>
            /// <param name="fileName">The name of the file to load the data from.</param>
            public static void LoadData(string fileName)
            {
                try
                {
                    if (File.Exists(fileName))
                    {
                        string jsonString = File.ReadAllText(fileName);
                        var data = JsonSerializer.Deserialize<DataContainer>(jsonString);

                        if (data != null)
                        {
                            // Load CarCollection
                            CarCollection.Instance.Cars = data.Cars ?? new List<Car>();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data file found. Starting with default data.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading data: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Helper class to encapsulate both FileData and CarCollection for JSON serialization/deserialization.
        /// </summary>
        public class DataContainer
        {
            /// <summary>
            /// Gets or sets the file data.
            /// </summary>
            [JsonPropertyName("FileData")]
            public FileData? FileData { get; set; }

            /// <summary>
            /// Gets or sets the list of cars.
            /// </summary>
            [JsonPropertyName("Cars")]
            public List<Car>? Cars { get; set; }
        }

        /// <summary>
        /// Prompts the user to input car information and returns the car object.
        /// </summary>
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
            car.FuelType = fuelTypeIndex;

            Console.Write("Indtast forbrug: ");
            car.FuelEfficiency = Convert.ToSingle(Console.ReadLine());
            Console.Write("Indtast kilometerstand: ");
            car.Mileage = Convert.ToInt32(Console.ReadLine());

            return car; // Return the car object
        }

        /// <summary>
        /// Displays a list of carCollection and allows the user to choose one.
        /// </summary>
        /// <param name="carCollection">The list of carCollection to choose from.</param>
        /// <returns>The chosen car object.</returns>
        static Car? SelectCar(CarCollection carCollection)
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
            for (int i = 1; i < carCollection.Cars.Count + 1; i++)
            {
                int ii = i - 1;
                Console.WriteLine(
                    "|" + $"{i} ".PadLeft(4) +
                    "|" + $" {carCollection.Cars[ii].Brand}".PadRight(20) +
                    "|" + $" {carCollection.Cars[ii].Model}".PadRight(20) +
                    "|" + $"{carCollection.Cars[ii].Mileage} ".PadLeft(20) +
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
                if (int.TryParse(input, out choice) && choice > 0 && choice <= carCollection.Cars.Count)
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
            return carCollection.Cars[choice - 1];
        }

        /// <summary>
        /// Adds a tour to the car and calculates the fuel needed and trip cost.
        /// </summary>
        /// <param name="car">The car object to add the tour to.</param>
        /// <returns>The updated car object.</returns>
        static Car AddTour(Car car)
        {
            Console.Clear(); // Clear the console window

            FuelTypeCollection fuelTypeCollection = FuelTypeCollection.Instance; // Get the singleton instance of the FuelTypeCollection

            Console.WriteLine("Tilføj tur");
            Console.WriteLine("==========");
            Console.Write("Indtast antal kilometer: ");
            int distance = Convert.ToInt32(Console.ReadLine());

            double FuelNeeded = distance / car.FuelEfficiency;
            double TripCost = FuelNeeded * (double)fuelTypeCollection.FuelTypes[car.FuelType].Price;
            car.AddTour(distance);

            Console.WriteLine();
            Console.WriteLine("Tur rapport");
            Console.WriteLine("===========");

            Console.WriteLine(
                $"Bilmærke: {car.Brand}\n" +
                $"Bilmodel: {car.Model}\n" +
                $"Årgang: {car.Year}\n" +
                $"Gear: {car.GearType}\n" +
                $"Brændstof: {fuelTypeCollection.FuelTypes[car.FuelType].Name}\n" +
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
            FuelTypeCollection fuelTypeCollection = FuelTypeCollection.Instance;
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
                $"Brændstof: {fuelTypeCollection.FuelTypes[car.FuelType].Name}\n" +
                $"Forbrug: {car.FuelEfficiency}" + " km/l\n" +
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
            CarCollection carCollection = CarCollection.Instance; // Get the singleton instance of the CarCollection

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
                        carCollection.Cars.Add(car); // Add the car to the list of carCollection
                        break;
                    case 2:
                        car = SelectCar(carCollection);
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

            FileHandler.LoadData(Constants.FileName);
            Menu();
            FileHandler.SaveData(Constants.FileName);
        }
    }
}