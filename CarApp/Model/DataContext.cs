namespace CarApp.Model
{
    internal class DataContext
    {
        public CarList CarList { get; private set; }
        public FuelTypeList FuelTypeList { get; private set; }

        public DataContext()
        {
            CarList = CarList.Instance;
            FuelTypeList = FuelTypeList.Instance;
        }
    }
}
