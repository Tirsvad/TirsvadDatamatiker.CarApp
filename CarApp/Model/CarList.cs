﻿namespace CarApp.Model;

/// <summary>
/// Represents a list of cars.
/// </summary>
class CarList
{
    private static CarList? _instance;
    private static readonly object _lock = new object();
    private List<Car> Cars { get; }

    /// <summary>
    /// Constructor for the CarList
    /// </summary>
    private CarList()
    {
        Cars = new List<Car>();
        Seed();
    }

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
        Add(new Car(GenerateId(), "Toyota", "Corolla", 2010, 'A', 1, 15.0f, 100000, "A nice car"));
        Add(new Car(GenerateId(), "Ford", "Focus", 2015, 'M', 1, 14.3f, 50000, "A nice car"));
        Add(new Car(GenerateId(), "Volkswagen", "Golf", 2018, 'M', 1, 18.2f, 20000, "A sporty car"));
        Add(new Car(GenerateId(), "Mercedes", "C-Class", 2021, 'A', 2, 25.0f, 533335, "A luxurious car"));
        Add(new Car(GenerateId(), "Tesla", "Model S", 2022, 'A', 3, 45.0f, 100, "An electric car"));
        Add(new Car(GenerateId(), "Nissan", "Leaf", 2023, 'A', 3, 50.0f, 0, "An electric car"));
        Add(new Car(GenerateId(), "Audi", "A4", 2019, 'A', 2, 19.0f, 10000, "A luxurious car"));
        Add(new Car(GenerateId(), "BMW", "M3", 2020, 'M', 2, 12.0f, 5000, "A sporty car"));
        Add(new Car(GenerateId(), "Chevrolet", "Camaro", 2017, 'M', 1, 13.0f, 30000, "A sporty car"));
        Add(new Car(GenerateId(), "Hyundai", "i30", 2016, 'A', 1, 16.0f, 40000, "A nice car"));
        Add(new Car(GenerateId(), "Kia", "Ceed", 2014, 'M', 1, 14.0f, 60000, "A nice car"));
        Add(new Car(GenerateId(), "Mazda", "3", 2013, 'A', 1, 17.0f, 70000, "A nice car"));
        Add(new Car(GenerateId(), "Subaru", "Impreza", 2012, 'M', 1, 15.0f, 80000, "A nice car"));
        Add(new Car(GenerateId(), "Volvo", "V60", 2011, 'A', 2, 20.0f, 90000, "A strong car"));
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
