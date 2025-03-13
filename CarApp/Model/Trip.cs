namespace CarApp.Model;

public class Trip
{
    public double Distance { get; private set; }
    public DateTime TripDate { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public Car Car { get; private set; }
    public double FuelPrice { get; private set; }

    public Trip(double distance, DateTime tripDate, DateTime startTime, DateTime endTime, double fuelPrice)
    {
        Distance = distance;
        TripDate = tripDate;
        StartTime = startTime;
        EndTime = endTime;
        FuelPrice = fuelPrice;
    }

    /// <summary>
    /// Calculates the duration of the trip.
    /// </summary>
    /// <returns></returns>
    public TimeSpan CalculateDuration()
    {
        return EndTime - StartTime;
    }

    /// <summary>
    /// Calculates the fuel consumption of the trip.
    /// </summary>
    /// <returns></returns>
    public double CalculateFuelConsumption(Car car)
    {
        return Distance / car.FuelEfficiency;
    }

    /// <summary>
    /// Calculates the price of the trip.
    /// </summary>
    public double CalculateTripPrice(double fuelNeeded)
    {
        return fuelNeeded * FuelPrice;
    }

    /// <summary>
    /// Returns a string representation of the trip.
    /// </summary>
    /// <returns></returns>
    public string GetTripInfo(Car car)
    {
        double fuelNeeded = CalculateFuelConsumption(car);
        return $"Dato     : {TripDate:D}\n" +
               $"Start tid: {StartTime}\n" +
               $"slut tid : {EndTime}\n" +
               $"Afstand  : {Distance} km\n" +
               $"Kostprisen: {CalculateTripPrice(fuelNeeded):F2} kr";
    }
}
