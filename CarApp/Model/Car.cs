namespace CarApp.Model;

/// <summary>
/// Represents a car with various properties.
/// </summary>
public class Car
{
    // Database properties

    /// <summary>
    /// Gets and sets the Id of the car.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Gets or sets the brand of the car.
    /// </summary>
    public string Brand { get; private set; }

    /// <summary>
    /// Gets or sets the model of the car.
    /// </summary>
    public string Model { get; private set; }

    /// <summary>
    /// Gets or sets the year of the car.
    /// </summary>
    public int Year { get; private set; }

    /// <summary>
    /// Gets or sets the gear type of the car.
    /// </summary>
    public char GearType { get; private set; }

    /// <summary>
    /// Gets or sets the fuel type ID of the car.
    /// </summary>
    public FuelType FuelType { get; private set; }

    /// <summary>
    /// Gets or sets the fuel efficiency of the car.
    /// </summary>
    public double FuelEfficiency { get; private set; }

    /// <summary>
    /// Gets or sets the mileage of the car.
    /// </summary>
    public int Mileage { get; private set; }

    /// <summary>
    /// Gets or sets the description of the car.
    /// </summary>
    public string Description { get; private set; }

    public Owner? Owner { get; private set; }

    public TripList? Trips { get; set; }

    // Runtime properties

    //private FuelTypeList FuelTypeList { get; set; }
    public bool IsEngineRunning { get; set; } = false;

    /// <summary>
    /// Initializes a new instance of the Car class.
    /// </summary>
    /// <param name="id">The ID of the car.</param>
    /// <param name="brand">The brand of the car.</param>
    /// <param name="model">The model of the car.</param>
    /// <param name="year">The year of the car.</param>
    /// <param name="gearType">The gear type of the car.</param>
    /// <param name="fuelType">The fuel type ID of the car.</param>
    /// <param name="fuelEfficiency">The fuel efficiency of the car.</param>
    /// <param name="mileage">The mileage of the car.</param>
    /// <param name="description">The description of the car.</param>
    public Car(int id, string brand, string model, int year, char gearType, FuelType fuelType, double fuelEfficiency, int mileage, string description = "")
    {
        Id = id;
        Brand = brand;
        Model = model;
        Year = year;
        GearType = gearType;
        FuelType = fuelType;
        FuelEfficiency = fuelEfficiency;
        Mileage = mileage;
        Description = description;
    }

    /// <summary>
    /// Initializes a new instance of the Car class.
    /// </summary>
    /// <param name="id">The ID of the car.</param>
    /// <param name="brand">The brand of the car.</param>
    /// <param name="model">The model of the car.</param>
    /// <param name="year">The year of the car.</param>
    /// <param name="gearType">The gear type of the car.</param>
    /// <param name="fuelType">The fuel type ID of the car.</param>
    /// <param name="fuelEfficiency">The fuel efficiency of the car.</param>
    /// <param name="mileage">The mileage of the car.</param>
    /// <param name="owner">The owner of the car.</param>
    /// <param name="description">The description of the car.</param>
    public Car(int id, string brand, string model, int year, char gearType, FuelType fuelType, double fuelEfficiency, int mileage, Owner? owner, string description = "")
    {
        Id = id;
        Brand = brand;
        Model = model;
        Year = year;
        GearType = gearType;
        FuelType = fuelType;
        FuelEfficiency = fuelEfficiency;
        Mileage = mileage;
        Owner = owner;
        Description = description;
    }

    /// <summary>
    /// Toggle the engine on or off.
    /// </summary>
    public void ToggleEngine()
    {
        if (IsEngineRunning)
        {
            StopEngine();
        }
        else
        {
            StartEngine();
        }
    }

    /// <summary>
    /// Starts the engine.
    /// </summary>
    public void StartEngine()
    {
        IsEngineRunning = true;
    }

    /// <summary>
    /// Stops the engine.
    /// </summary>
    public void StopEngine()
    {
        IsEngineRunning = false;
    }

    /// <summary>
    /// Drives a given trip if the engine is running.
    /// </summary>
    /// <param name="trip">The trip to be driven.</param>
    public void Drive(Trip trip)
    {
        if (IsEngineRunning)
        {
            Trips?.AddTrip(trip);
            Mileage += (int)trip.Distance;
        }
    }

    /// <summary>
    /// Adds a tour distance to the car's mileage if engine is running else it will simulate tour.
    /// </summary>
    /// <param name="addDistanceInKm">The distance of the tour in kilometers.</param>
    public void UpdateMileAge(int addDistanceInKm)
    {
        if (IsEngineRunning)
        {
            Mileage += (int)addDistanceInKm;
        }
    }

    /// <summary>
    /// Calculates the amount of fuel needed for a given distance.
    /// </summary>
    /// <param name="car">The car object containing fuel efficiency information.</param>
    /// <param name="distance">The distance to be traveled in kilometers.</param>
    /// <returns>The amount of fuel needed for the given distance.</returns>
    public static double CalculateFuelNeeded(Car car, int distance)
    {
        return (double)(distance / car.FuelEfficiency);
    }

    /// <summary>
    /// Calculates the trip cost based on the fuel needed and the fuel price.
    /// </summary>
    /// <param name="car">The car object containing fuel type information.</param>
    /// <param name="fuelNeeded">The amount of fuel needed for the trip.</param>
    /// <param name="fuelPrice">The price of the fuel per liter.</param>
    /// <!---->! This method should be in a separate class, not in the Car class. <!---->
    /// <returns>The cost of the trip.</returns>
    static public double CalculateTripCost(Car car, double fuelNeeded, double fuelPrice)
    {
        return (double)(fuelNeeded * fuelPrice);
    }
}
