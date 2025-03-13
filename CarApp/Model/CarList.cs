namespace CarApp.Model;

/// <summary>
/// Represents a list of cars.
/// </summary>
public class CarList
{
    private static CarList? _instance;
    private static readonly object _lock = new object();
    /// <summary>
    /// Singleton instance of the CarList
    /// </summary>
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
    }

    /// <summary>
    /// List of cars
    /// </summary>
    private List<Car> Cars { get; }

    // Runtime properties

    OwnerList OwnerList { get; }

    /// <summary>
    /// Constructor for the CarList
    /// </summary>
    private CarList()
    {
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
    /// </summary>
    private void Seed()
    {
        // TODO: Load from Database
        Add(new Car(GenerateId(), "Toyota", "Corolla", 2010, 'A', FuelType.Benzin, 15.0f, 100000, OwnerList.GetOwnerById(0), "A nice car"));
        Add(new Car(GenerateId(), "Ford", "Focus", 2015, 'M', FuelType.Benzin, 14.3f, 50000, OwnerList.GetOwnerById(1), "A nice car"));
        Add(new Car(GenerateId(), "Volkswagen", "Golf", 2018, 'M', FuelType.Diesel, 18.2f, 20000, "A sporty car"));
        Add(new Car(GenerateId(), "Mercedes", "C-Class", 2021, 'A', FuelType.Diesel, 25.0f, 533335, OwnerList.GetOwnerById(3), "A luxurious car"));
        Add(new Car(GenerateId(), "Tesla", "Model S", 2022, 'A', FuelType.Electric, 45.0f, 100, OwnerList.GetOwnerById(4), "An electric car"));
        Add(new Car(GenerateId(), "Nissan", "Leaf", 2023, 'A', FuelType.Electric, 50.0f, 0, OwnerList.GetOwnerById(5), "An electric car"));
        Add(new Car(GenerateId(), "Audi", "A4", 2019, 'A', FuelType.Diesel, 19.0f, 10000, OwnerList.GetOwnerById(6), "A luxurious car"));
        Add(new Car(GenerateId(), "BMW", "M3", 2020, 'M', FuelType.Benzin, 12.0f, 5000, OwnerList.GetOwnerById(3), "A sporty car"));
        Add(new Car(GenerateId(), "Chevrolet", "Camaro", 2017, 'M', FuelType.Benzin, 13.0f, 30000, OwnerList.GetOwnerById(2), "A sporty car"));
        Add(new Car(GenerateId(), "Hyundai", "i30", 2016, 'A', FuelType.Benzin, 16.0f, 40000, OwnerList.GetOwnerById(2), "A nice car"));
        Add(new Car(GenerateId(), "Kia", "Ceed", 2014, 'M', FuelType.Benzin, 14.0f, 60000, OwnerList.GetOwnerById(4), "A nice car"));
        Add(new Car(GenerateId(), "Mazda", "3", 2013, 'A', FuelType.Benzin, 17.0f, 70000, OwnerList.GetOwnerById(2), "A nice car"));
        Add(new Car(GenerateId(), "Subaru", "Impreza", 2012, 'M', FuelType.Benzin, 15.0f, 80000, OwnerList.GetOwnerById(5), "A nice car"));
        Add(new Car(GenerateId(), "Volvo", "V60", 2011, 'A', FuelType.Diesel, 20.0f, 90000, "A strong car"));
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
}
