using CarApp.Type;

namespace CarApp.Model;

/// <summary>
/// Represents the price of a fuel type.
/// </summary>
public class FuelPrice
{
    public FuelType FuelType { get; }
    public double Price { get; }
    public string Measurement { get; }

    public FuelPrice(FuelType fuelType, double price, string measurement)
    {
        FuelType = fuelType;
        Price = price;
        Measurement = measurement;
    }
}
