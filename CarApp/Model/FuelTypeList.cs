namespace CarApp.Model
{
    class FuelTypeList
    {
        private static FuelTypeList? _instance;
        private static readonly object _lock = new object();

        public List<FuelType> FuelTypeCollection { get; }

        private FuelTypeList()
        {
            FuelTypeCollection = new List<FuelType>();
            Seed();
        }

        public static FuelTypeList Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new FuelTypeList();
                        }
                    }
                }
                return _instance;
            }
        }

        public void Seed()
        {
            lock (_lock)
            {
                FuelTypeCollection.Add(new FuelType(GenerateId(), "Benzin 95", 14.99m));
                FuelTypeCollection.Add(new FuelType(GenerateId(), "Diesel", 14.59m));
                FuelTypeCollection.Add(new FuelType(GenerateId(), "El", 2.85m));
                FuelTypeCollection.Add(new FuelType(GenerateId(), "Benzin 100", 16.96m));
            }
        }

        public int GenerateId()
        {
            int id = 0;
            if (FuelTypeCollection.Count > 0)
                id = FuelTypeCollection.Last().Id + 1;
            return id;
        }
    }
}
