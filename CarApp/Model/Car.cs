using System.Text.Json.Serialization;

namespace CarApp.Model;

/// <summary>
/// Represents a car with various properties.
/// </summary>
public class Car
{
    // Properties
    public int Id { get; private set; } ///> A unique ID for the car.
    public string Brand { get; private set; } ///> The brand of the car.
    public string Model { get; private set; } ///> The model of the car.
    public int Year { get; private set; } ///> The year of the car.
    public char GearType { get; private set; } ///> The gear type of the car.
    public double FuelEfficiency { get; private set; } ///> The fuel efficiency of the car in kilometers per liter.
    public int Mileage { get; private set; } ///> The mileage of the car in kilometers.
    public Engine? Engine { get; private set; } ///> The engine object of the car.
    // TODO: What if car has more than 4 wheels? (Ex. Land Rover Defender Flying Huntsman) 
    public Wheel[] Wheels { get; private set; } = new Wheel[4]; ///> The wheels of the car.
    public string Description { get; private set; } ///> The description of the car.
    public Owner? Owner { get; set; } ///> The owner of the car.
    public List<Trip>? Trips { get; private set; } ///> The list of trips that this car has driven.

    // Runtime properties
    /// <summary>
    /// Gets or sets a value indicating whether the engine is running.
    /// </summary>
    public bool IsEngineRunning { get; set; } = false;

    [JsonConstructor]
    public Car(int id, string brand, string model, int year, char gearType, double fuelEfficiency, int mileage, Engine engine, Wheel[] wheels, string description, Owner? owner)
    {
        Id = id;
        Brand = brand;
        Model = model;
        Year = year;
        GearType = gearType;
        FuelEfficiency = fuelEfficiency;
        Mileage = mileage;
        Engine = engine;
        Wheels = wheels;
        Description = description;
        Owner = owner;
    }

    /// <summary>
    /// Toggles the engine on or off.
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
            Trips?.Add(trip);
            Mileage += (int)trip.Distance;
        }
    }

    /// <summary>
    /// Adds a tour distance to the car's mileage if the engine is running, else it will simulate the tour.
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
    /// <returns>The cost of the trip.</returns>
    public static double CalculateTripCost(Car car, double fuelNeeded, double fuelPrice)
    {
        return (double)(fuelNeeded * fuelPrice);
    }

    public double RemoveTrip(Trip trip)
    {
        Trips?.Remove(trip);
        return trip.Distance;
    }

    public string ToStringAllTrip(Car car)
    {
        string result = "";
        for (int i = 0; i < Trips?.Count; i++)
        {
            result += Trips[i].GetTripInfo(car) + "\n";
        }
        return result;
    }
}
