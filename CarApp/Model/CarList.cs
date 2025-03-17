using System.Text.Json.Serialization;

namespace CarApp.Model;

/// <summary>
/// Represents a list of cars.
/// </summary>
public class CarList
{
    private static CarList? _instance;
    private static readonly object _lock = new object();
    public static CarList Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new CarList();
                }
                return _instance;
            }
        }
    } ///> Singleton instance of the CarList

    private List<Car> Cars { get; } ///> List of cars

    // Runtime properties

    OwnerList OwnerList { get; }

    [JsonConstructor]
    private CarList()
    {
        // Initialize the Cars list
        Cars = new List<Car>();
        OwnerList = OwnerList.Instance;
        //Seed();
    }

    /// <summary>
    /// Add a car to the list
    /// </summary>
    /// <param name="car"></param>
    public void Add(Car car)
    {
        Cars.Add(car);
    }

    /// <summary>
    /// Remove a car from the list
    /// </summary>
    /// <param name="car"></param>
    public void Remove(Car car)
    {
        Cars.Remove(car);
    }

    public List<Car> GetCars()
    {
        return Cars;
    }

    public override string ToString()
    {
        string result = "";
        foreach (Car car in Cars)
        {
            result += $"{car.Id:4} {car.Brand}\n";
        }
        return result;
    }

    /// <summary>
    /// Seed the car list with some cars
    /// Only used for testing purposes as we get the data from the json file
    /// </summary>
    private void Seed()
    {
        // public Car(int id, string brand, string model, int year, char gearType, double fuelEfficiency, int mileage, Engine engine, Wheel[] wheels, string description, Owner owner)

        // TODO: Load from Database
        Add(
            new Car(
                id: GenerateId(), // Auto generate ID
                brand: "Toyota",
                model: "Corolla",
                year: 2010,
                gearType: 'A',
                fuelEfficiency: 15.0f,
                mileage: 100000,
                engine: new Engine(
                    name: "1.6",
                    ccm: 1600,
                    horsePower: 120,
                    torque: 200,
                    fuel: Engine.FuelType.Benzin,
                    mileage: 100000, // Mileage
                    lastService: DateTime.Now, // LastService
                    serviceIntervalMileage: 15000, // ServiceIntervalMileage
                    serviceIntervalMonths: 12 // ServiceIntervalMonths
                    ),
                Wheel.GetSetOf4Wheels(
                    new Tire(
                        brand: "Bridgestone",
                        model: "Turanza",
                        width: 205,
                        height: 55,
                        inch: 16,
                        construction: Tire.ConstructionType.Radial,
                        season: Tire.SeasonType.Summer
                        )
                    ),
                description: "A nice car",
                owner: OwnerList.GetOwnerById(0)
            )
        );
        Add(new Car(GenerateId(), "Ford", "Focus", 2015, 'M', 14.3f, 50000, new Engine("1.6", 1600, 120, 200, Engine.FuelType.Benzin, 100000, DateTime.Now, 15000, 12), Wheel.GetSetOf4Wheels(new Tire("Bridgestone", "Turanza", 205, 55, 16, Tire.ConstructionType.Radial, Tire.SeasonType.Summer)), "A nice car", OwnerList.GetOwnerById(1)));
        Add(new Car(GenerateId(), "Volkswagen", "Golf", 2018, 'M', 18.2f, 20000, new Engine("1.6", 1600, 120, 200, Engine.FuelType.Diesel, 100000, DateTime.Now, 15000, 12), Wheel.GetSetOf4Wheels(new Tire("Bridgestone", "Turanza", 205, 55, 16, Tire.ConstructionType.Radial, Tire.SeasonType.Summer)), "A sporty car", OwnerList.GetOwnerById(2)));
        Add(new Car(GenerateId(), "Mercedes", "C-Class", 2021, 'A', 25.0f, 533335, new Engine("1.6", 1600, 120, 200, Engine.FuelType.Diesel, 100000, DateTime.Now, 15000, 12), Wheel.GetSetOf4Wheels(new Tire("Bridgestone", "Turanza", 205, 55, 16, Tire.ConstructionType.Radial, Tire.SeasonType.Summer)), "A luxurious car", OwnerList.GetOwnerById(3)));
        Add(new Car(GenerateId(), "Tesla", "Model S", 2022, 'A', 45.0f, 100, new Engine("1.6", 1600, 120, 200, Engine.FuelType.Electric, 100000, DateTime.Now, 15000, 12), Wheel.GetSetOf4Wheels(new Tire("Bridgestone", "Turanza", 205, 55, 16, Tire.ConstructionType.Radial, Tire.SeasonType.Summer)), "An electric car", OwnerList.GetOwnerById(4)));
        Add(new Car(GenerateId(), "Nissan", "Leaf", 2023, 'A', 50.0f, 0, new Engine("1.6", 1600, 120, 200, Engine.FuelType.Electric, 100000, DateTime.Now, 15000, 12), Wheel.GetSetOf4Wheels(new Tire("Bridgestone", "Turanza", 205, 55, 16, Tire.ConstructionType.Radial, Tire.SeasonType.Summer)), "An electric car", OwnerList.GetOwnerById(5)));
        Add(new Car(GenerateId(), "Audi", "A4", 2019, 'A', 19.0f, 10000, new Engine("1.6", 1600, 120, 200, Engine.FuelType.Diesel, 100000, DateTime.Now, 15000, 12), Wheel.GetSetOf4Wheels(new Tire("Bridgestone", "Turanza", 205, 55, 16, Tire.ConstructionType.Radial, Tire.SeasonType.Summer)), "A luxurious car", OwnerList.GetOwnerById(6)));
        Add(new Car(GenerateId(), "BMW", "M3", 2020, 'M', 12.0f, 5000, new Engine("1.6", 1600, 120, 200, Engine.FuelType.Benzin, 100000, DateTime.Now, 15000, 12), Wheel.GetSetOf4Wheels(new Tire("Bridgestone", "Turanza", 205, 55, 16, Tire.ConstructionType.Radial, Tire.SeasonType.Summer)), "A sporty car", OwnerList.GetOwnerById(3)));
        Add(new Car(GenerateId(), "Chevrolet", "Camaro", 2017, 'M', 13.0f, 30000, new Engine("1.6", 1600, 120, 200, Engine.FuelType.Benzin, 100000, DateTime.Now, 15000, 12), Wheel.GetSetOf4Wheels(new Tire("Bridgestone", "Turanza", 205, 55, 16, Tire.ConstructionType.Radial, Tire.SeasonType.Summer)), "A sporty car", OwnerList.GetOwnerById(2)));
        Add(new Car(GenerateId(), "Hyundai", "i30", 2016, 'A', 16.0f, 40000, new Engine("1.6", 1600, 120, 200, Engine.FuelType.Benzin, 100000, DateTime.Now, 15000, 12), Wheel.GetSetOf4Wheels(new Tire("Bridgestone", "Turanza", 205, 55, 16, Tire.ConstructionType.Radial, Tire.SeasonType.Summer)), "A nice car", OwnerList.GetOwnerById(2)));
        Add(new Car(GenerateId(), "Kia", "Ceed", 2014, 'M', 14.0f, 60000, new Engine("1.6", 1600, 120, 200, Engine.FuelType.Benzin, 100000, DateTime.Now, 15000, 12), Wheel.GetSetOf4Wheels(new Tire("Bridgestone", "Turanza", 205, 55, 16, Tire.ConstructionType.Radial, Tire.SeasonType.Summer)), "A nice car", OwnerList.GetOwnerById(4)));
        Add(new Car(GenerateId(), "Mazda", "3", 2013, 'A', 17.0f, 70000, new Engine("1.6", 1600, 120, 200, Engine.FuelType.Benzin, 100000, DateTime.Now, 15000, 12), Wheel.GetSetOf4Wheels(new Tire("Bridgestone", "Turanza", 205, 55, 16, Tire.ConstructionType.Radial, Tire.SeasonType.Summer)), "A nice car", OwnerList.GetOwnerById(2)));
        Add(new Car(GenerateId(), "Subaru", "Impreza", 2012, 'M', 15.0f, 80000, new Engine("1.6", 1600, 120, 200, Engine.FuelType.Benzin, 100000, DateTime.Now, 15000, 12), Wheel.GetSetOf4Wheels(new Tire("Bridgestone", "Turanza", 205, 55, 16, Tire.ConstructionType.Radial, Tire.SeasonType.Summer)), "A nice car", OwnerList.GetOwnerById(5)));
        Add(new Car(GenerateId(), "Volvo", "V60", 2011, 'A', 20.0f, 90000, new Engine("1.6", 1600, 120, 200, Engine.FuelType.Diesel, 100000, DateTime.Now, 15000, 12), Wheel.GetSetOf4Wheels(new Tire("Bridgestone", "Turanza", 205, 55, 16, Tire.ConstructionType.Radial, Tire.SeasonType.Summer)), "A strong car", OwnerList.GetOwnerById(6)));
    }

    /// <summary>
    /// Auto create new ID for new car
    /// </summary>
    /// <returns>new ID</returns>
    public int GenerateId()
    {
        // TODO: Let database handle this
        int id = 0;
        if (Cars.Count > 0)
            id = Cars.Last().Id + 1;
        return id;
    }

    internal void Clear()
    {
        Cars.Clear();
    }
}
