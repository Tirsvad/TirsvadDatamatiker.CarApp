namespace CarApp.Model
{
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
            FuelPrices.Add(new FuelPrice(FuelType.Diesel, 10.0, "liter"));
            FuelPrices.Add(new FuelPrice(FuelType.Electric, 4.0, "kw"));
            FuelPrices.Add(new FuelPrice(FuelType.Benzin, 12.0, "liter"));
            double hybridPrice = this.FuelPrices.Find(f => f.FuelType == FuelType.Benzin).Price;
            FuelPrices.Add(new FuelPrice(FuelType.Hybrid, hybridPrice, "liter hvis der køre på el 50 % af turen"));
            FuelPrices.Add(new FuelPrice(FuelType.Brint, 14.0, "per 100 gr"));
        }
    }
}
