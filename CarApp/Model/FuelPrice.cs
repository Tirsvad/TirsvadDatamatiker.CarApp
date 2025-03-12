namespace CarApp.Model
{
    internal class FuelPrice
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
}
