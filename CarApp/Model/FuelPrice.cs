namespace CarApp.Model;

public class FuelPrice
{
    public Engine.FuelType FuelType { get; }
    public double Price { get; }
    public string Measurement { get; }

    public FuelPrice(Engine.FuelType fuelType, double price, string measurement)
    {
        FuelType = fuelType;
        Price = price;
        Measurement = measurement;
    }
}
