namespace CarApp.Model;

/// <summary>
/// Represents a list of fuel prices.
/// </summary>
public class FuelPriceList
{
    private static FuelPriceList? _instance;
    private static readonly object _lock = new object();
    public static FuelPriceList Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new FuelPriceList();
                }
                return _instance;
            }
        }
    }

    public List<FuelPrice> FuelPrices { get; private set; }

    private FuelPriceList()
    {
        FuelPrices = new List<FuelPrice>();
        Seed();
    }

    private void Seed()
    {
        FuelPrices.Add(new FuelPrice(Engine.FuelType.Diesel, 13.48, "liter"));
        FuelPrices.Add(new FuelPrice(Engine.FuelType.Electric, 4.0, "kw"));
        FuelPrices.Add(new FuelPrice(Engine.FuelType.Benzin, 14.66, "liter"));
        double hybridPrice = this.FuelPrices.Find(f => f.FuelType == Engine.FuelType.Benzin)?.Price ?? 0.0;
        FuelPrices.Add(new FuelPrice(Engine.FuelType.Hybrid, hybridPrice, "liter hvis der køres på el 50 % af turen"));
        FuelPrices.Add(new FuelPrice(Engine.FuelType.Brint, 14.0, "per 100 gr"));
    }

    public double? GetPrice(Engine.FuelType fuelType)
    {
        return FuelPrices.Find(f => f.FuelType == fuelType)?.Price;
    }
}
