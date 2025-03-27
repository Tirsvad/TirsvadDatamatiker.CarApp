using System.Text.Json.Serialization;
using CarApp.Type;

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

    public List<Car> Cars { get; } ///> List of cars

    // Runtime properties

    OwnerList OwnerList { get; }

    [JsonConstructor]
    private CarList()
    {
        // Initialize the Cars list
        Cars = new List<Car>();
        OwnerList = OwnerList.Instance;
        Seed();
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

    public void UpdateCar(Car car)
    {
        this.Cars.First(c => c.Id == car.Id).Mileage = car.Mileage;
        this.Cars.First(c => c.Id == car.Id).Owner = car.Owner;
        this.Cars.First(c => c.Id == car.Id).Trips = car.Trips;
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
        Add(new Car(id: GenerateId(), brand: "Honda", model: "Civic", year: 2012, gearType: GearType.Automatic, fuelEfficiency: 14.0f, mileage: 80000, engine: new Engine(name: "1.8", ccm: 1800, horsePower: 140, torque: 174, fuel: FuelType.Benzin, mileage: 80000, lastService: DateTime.Now.Date, serviceIntervalMileage: 15000, serviceIntervalMonths: 12), Wheel.GetSetOf4Wheels(new Tire(brand: "Michelin", model: "Primacy", width: 205, height: 55, inch: 16, construction: Tire.ConstructionType.Radial, season: Tire.SeasonType.Summer)), description: "A reliable car", owner: OwnerList.GetOwnerById(0)));
        Add(new Car(id: GenerateId(), brand: "Toyota", model: "Camry", year: 2015, gearType: GearType.Automatic, fuelEfficiency: 12.0f, mileage: 60000, engine: new Engine(name: "2.5", ccm: 2500, horsePower: 178, torque: 230, fuel: FuelType.Benzin, mileage: 60000, lastService: DateTime.Now.Date, serviceIntervalMileage: 15000, serviceIntervalMonths: 12), Wheel.GetSetOf4Wheels(new Tire(brand: "Michelin", model: "Primacy", width: 215, height: 55, inch: 17, construction: Tire.ConstructionType.Radial, season: Tire.SeasonType.Summer)), description: "A comfortable sedan", owner: OwnerList.GetOwnerById(new Random().Next(0, 7))));
        Add(new Car(id: GenerateId(), brand: "Ford", model: "F-150", year: 2018, gearType: GearType.Automatic, fuelEfficiency: 10.0f, mileage: 40000, engine: new Engine(name: "3.5", ccm: 3500, horsePower: 375, torque: 470, fuel: FuelType.Benzin, mileage: 40000, lastService: DateTime.Now.Date, serviceIntervalMileage: 15000, serviceIntervalMonths: 12), Wheel.GetSetOf4Wheels(new Tire(brand: "Goodyear", model: "Wrangler", width: 275, height: 65, inch: 18, construction: Tire.ConstructionType.Radial, season: Tire.SeasonType.AllSeason)), description: "A powerful truck", owner: OwnerList.GetOwnerById(new Random().Next(0, 7))));
        Add(new Car(id: GenerateId(), brand: "Chevrolet", model: "Equinox", year: 2017, gearType: GearType.Automatic, fuelEfficiency: 11.0f, mileage: 50000, engine: new Engine(name: "2.0", ccm: 2000, horsePower: 252, torque: 353, fuel: FuelType.Benzin, mileage: 50000, lastService: DateTime.Now.Date, serviceIntervalMileage: 15000, serviceIntervalMonths: 12), Wheel.GetSetOf4Wheels(new Tire(brand: "Bridgestone", model: "Dueler", width: 225, height: 60, inch: 17, construction: Tire.ConstructionType.Radial, season: Tire.SeasonType.AllSeason)), description: "A versatile SUV", owner: OwnerList.GetOwnerById(new Random().Next(0, 7))));
        Add(new Car(id: GenerateId(), brand: "Honda", model: "Accord", year: 2016, gearType: GearType.Automatic, fuelEfficiency: 13.0f, mileage: 70000, engine: new Engine(name: "2.4", ccm: 2400, horsePower: 185, torque: 245, fuel: FuelType.Benzin, mileage: 70000, lastService: DateTime.Now.Date, serviceIntervalMileage: 15000, serviceIntervalMonths: 12), Wheel.GetSetOf4Wheels(new Tire(brand: "Continental", model: "ProContact", width: 225, height: 50, inch: 17, construction: Tire.ConstructionType.Radial, season: Tire.SeasonType.Summer)), description: "A reliable sedan", owner: OwnerList.GetOwnerById(new Random().Next(0, 7))));
        Add(new Car(id: GenerateId(), brand: "Nissan", model: "Altima", year: 2019, gearType: GearType.Automatic, fuelEfficiency: 12.5f, mileage: 30000, engine: new Engine(name: "2.5", ccm: 2500, horsePower: 188, torque: 244, fuel: FuelType.Benzin, mileage: 30000, lastService: DateTime.Now.Date, serviceIntervalMileage: 15000, serviceIntervalMonths: 12), Wheel.GetSetOf4Wheels(new Tire(brand: "Dunlop", model: "SP Sport", width: 215, height: 55, inch: 17, construction: Tire.ConstructionType.Radial, season: Tire.SeasonType.Summer)), description: "A stylish sedan", owner: OwnerList.GetOwnerById(new Random().Next(0, 7))));
        Add(new Car(id: GenerateId(), brand: "Hyundai", model: "Santa Fe", year: 2020, gearType: GearType.Automatic, fuelEfficiency: 11.5f, mileage: 20000, engine: new Engine(name: "2.4", ccm: 2400, horsePower: 185, torque: 241, fuel: FuelType.Benzin, mileage: 20000, lastService: DateTime.Now.Date, serviceIntervalMileage: 15000, serviceIntervalMonths: 12), Wheel.GetSetOf4Wheels(new Tire(brand: "Hankook", model: "Dynapro", width: 235, height: 60, inch: 18, construction: Tire.ConstructionType.Radial, season: Tire.SeasonType.AllSeason)), description: "A family SUV", owner: OwnerList.GetOwnerById(new Random().Next(0, 7))));
        Add(new Car(id: GenerateId(), brand: "Kia", model: "Optima", year: 2018, gearType: GearType.Automatic, fuelEfficiency: 13.5f, mileage: 45000, engine: new Engine(name: "2.0", ccm: 2000, horsePower: 245, torque: 353, fuel: FuelType.Benzin, mileage: 45000, lastService: DateTime.Now.Date, serviceIntervalMileage: 15000, serviceIntervalMonths: 12), Wheel.GetSetOf4Wheels(new Tire(brand: "Nexen", model: "N5000", width: 215, height: 55, inch: 17, construction: Tire.ConstructionType.Radial, season: Tire.SeasonType.Summer)), description: "A sporty sedan", owner: OwnerList.GetOwnerById(new Random().Next(0, 7))));
        Add(new Car(id: GenerateId(), brand: "Mazda", model: "CX-9", year: 2021, gearType: GearType.Automatic, fuelEfficiency: 10.5f, mileage: 15000, engine: new Engine(name: "2.5", ccm: 2500, horsePower: 250, torque: 420, fuel: FuelType.Benzin, mileage: 15000, lastService: DateTime.Now.Date, serviceIntervalMileage: 15000, serviceIntervalMonths: 12), Wheel.GetSetOf4Wheels(new Tire(brand: "Yokohama", model: "Geolandar", width: 255, height: 50, inch: 20, construction: Tire.ConstructionType.Radial, season: Tire.SeasonType.AllSeason)), description: "A premium SUV", owner: OwnerList.GetOwnerById(new Random().Next(0, 7))));
        Add(new Car(id: GenerateId(), brand: "Subaru", model: "Forester", year: 2017, gearType: GearType.Automatic, fuelEfficiency: 12.0f, mileage: 50000, engine: new Engine(name: "2.5", ccm: 2500, horsePower: 182, torque: 239, fuel: FuelType.Benzin, mileage: 50000, lastService: DateTime.Now.Date, serviceIntervalMileage: 15000, serviceIntervalMonths: 12), Wheel.GetSetOf4Wheels(new Tire(brand: "Falken", model: "Wildpeak", width: 225, height: 60, inch: 17, construction: Tire.ConstructionType.Radial, season: Tire.SeasonType.AllSeason)), description: "A rugged SUV", owner: OwnerList.GetOwnerById(new Random().Next(0, 7))));
        Add(new Car(id: GenerateId(), brand: "Volkswagen", model: "Passat", year: 2019, gearType: GearType.Automatic, fuelEfficiency: 13.0f, mileage: 35000, engine: new Engine(name: "2.0", ccm: 2000, horsePower: 174, torque: 207, fuel: FuelType.Benzin, mileage: 35000, lastService: DateTime.Now.Date, serviceIntervalMileage: 15000, serviceIntervalMonths: 12), Wheel.GetSetOf4Wheels(new Tire(brand: "Pirelli", model: "Cinturato", width: 215, height: 55, inch: 17, construction: Tire.ConstructionType.Radial, season: Tire.SeasonType.Summer)), description: "A reliable sedan", owner: OwnerList.GetOwnerById(new Random().Next(0, 7))));
        Add(new Car(id: GenerateId(), brand: "Ford", model: "Mustang", year: 2018, gearType: GearType.Manual, fuelEfficiency: 12.0f, mileage: 30000, engine: new Engine(name: "5.0", ccm: 5000, horsePower: 450, torque: 529, fuel: FuelType.Benzin, mileage: 30000, lastService: DateTime.Now.Date, serviceIntervalMileage: 10000, serviceIntervalMonths: 6), Wheel.GetSetOf4Wheels(new Tire(brand: "Pirelli", model: "P Zero", width: 255, height: 40, inch: 19, construction: Tire.ConstructionType.Radial, season: Tire.SeasonType.Summer)), description: "A powerful sports car", owner: OwnerList.GetOwnerById(2)));
        Add(new Car(id: GenerateId(), brand: "Chevrolet", model: "Malibu", year: 2016, gearType: GearType.Automatic, fuelEfficiency: 13.0f, mileage: 60000, engine: new Engine(name: "2.0", ccm: 2000, horsePower: 250, torque: 353, fuel: FuelType.Benzin, mileage: 60000, lastService: DateTime.Now.Date, serviceIntervalMileage: 15000, serviceIntervalMonths: 12), Wheel.GetSetOf4Wheels(new Tire(brand: "Goodyear", model: "Eagle", width: 225, height: 50, inch: 17, construction: Tire.ConstructionType.Radial, season: Tire.SeasonType.Summer)), description: "A comfortable sedan", owner: OwnerList.GetOwnerById(3)));
        Add(new Car(id: GenerateId(), brand: "BMW", model: "X5", year: 2020, gearType: GearType.Automatic, fuelEfficiency: 10.0f, mileage: 20000, engine: new Engine(name: "3.0", ccm: 3000, horsePower: 335, torque: 450, fuel: FuelType.Diesel, mileage: 20000, lastService: DateTime.Now.Date, serviceIntervalMileage: 15000, serviceIntervalMonths: 12), Wheel.GetSetOf4Wheels(new Tire(brand: "Continental", model: "CrossContact", width: 255, height: 50, inch: 19, construction: Tire.ConstructionType.Radial, season: Tire.SeasonType.AllSeason)), description: "A luxury SUV", owner: OwnerList.GetOwnerById(4)));
        Add(new Car(id: GenerateId(), brand: "Audi", model: "Q7", year: 2019, gearType: GearType.Automatic, fuelEfficiency: 11.0f, mileage: 25000, engine: new Engine(name: "3.0", ccm: 3000, horsePower: 329, torque: 440, fuel: FuelType.Diesel, mileage: 25000, lastService: DateTime.Now.Date, serviceIntervalMileage: 15000, serviceIntervalMonths: 12), Wheel.GetSetOf4Wheels(new Tire(brand: "Dunlop", model: "SP Sport", width: 275, height: 45, inch: 20, construction: Tire.ConstructionType.Radial, season: Tire.SeasonType.AllSeason)), description: "A premium SUV", owner: OwnerList.GetOwnerById(5)));
        Add(new Car(id: GenerateId(), brand: "Mercedes-Benz", model: "E-Class", year: 2017, gearType: GearType.Automatic, fuelEfficiency: 12.5f, mileage: 45000, engine: new Engine(name: "2.0", ccm: 2000, horsePower: 241, torque: 370, fuel: FuelType.Benzin, mileage: 45000, lastService: DateTime.Now.Date, serviceIntervalMileage: 15000, serviceIntervalMonths: 12), Wheel.GetSetOf4Wheels(new Tire(brand: "Bridgestone", model: "Potenza", width: 245, height: 45, inch: 18, construction: Tire.ConstructionType.Radial, season: Tire.SeasonType.Summer)), description: "A luxury sedan", owner: OwnerList.GetOwnerById(6)));
        Add(new Car(id: GenerateId(), brand: "Hyundai", model: "Elantra", year: 2015, gearType: GearType.Automatic, fuelEfficiency: 15.0f, mileage: 70000, engine: new Engine(name: "1.8", ccm: 1800, horsePower: 145, torque: 178, fuel: FuelType.Benzin, mileage: 70000, lastService: DateTime.Now.Date, serviceIntervalMileage: 15000, serviceIntervalMonths: 12), Wheel.GetSetOf4Wheels(new Tire(brand: "Hankook", model: "Ventus", width: 205, height: 55, inch: 16, construction: Tire.ConstructionType.Radial, season: Tire.SeasonType.Summer)), description: "A compact sedan", owner: OwnerList.GetOwnerById(2)));
        Add(new Car(id: GenerateId(), brand: "Kia", model: "Sorento", year: 2021, gearType: GearType.Automatic, fuelEfficiency: 13.5f, mileage: 15000, engine: new Engine(name: "2.5", ccm: 2500, horsePower: 191, torque: 247, fuel: FuelType.Benzin, mileage: 15000, lastService: DateTime.Now.Date, serviceIntervalMileage: 15000, serviceIntervalMonths: 12), Wheel.GetSetOf4Wheels(new Tire(brand: "Nexen", model: "Roadian", width: 235, height: 60, inch: 18, construction: Tire.ConstructionType.Radial, season: Tire.SeasonType.AllSeason)), description: "A midsize SUV", owner: OwnerList.GetOwnerById(1)));
        Add(new Car(id: GenerateId(), brand: "Mazda", model: "CX-5", year: 2018, gearType: GearType.Automatic, fuelEfficiency: 14.0f, mileage: 40000, engine: new Engine(name: "2.5", ccm: 2500, horsePower: 187, torque: 252, fuel: FuelType.Benzin, mileage: 40000, lastService: DateTime.Now.Date, serviceIntervalMileage: 15000, serviceIntervalMonths: 12), Wheel.GetSetOf4Wheels(new Tire(brand: "Yokohama", model: "Geolandar", width: 225, height: 65, inch: 17, construction: Tire.ConstructionType.Radial, season: Tire.SeasonType.AllSeason)), description: "A compact SUV", owner: OwnerList.GetOwnerById(1)));
        Add(new Car(id: GenerateId(), brand: "Subaru", model: "Outback", year: 2019, gearType: GearType.Automatic, fuelEfficiency: 13.0f, mileage: 35000, engine: new Engine(name: "2.5", ccm: 2500, horsePower: 182, torque: 239, fuel: FuelType.Benzin, mileage: 35000, lastService: DateTime.Now.Date, serviceIntervalMileage: 15000, serviceIntervalMonths: 12), Wheel.GetSetOf4Wheels(new Tire(brand: "Falken", model: "Wildpeak", width: 225, height: 60, inch: 18, construction: Tire.ConstructionType.Radial, season: Tire.SeasonType.AllSeason)), description: "A rugged crossover", owner: OwnerList.GetOwnerById(4)));
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
