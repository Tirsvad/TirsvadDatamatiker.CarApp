namespace CarApp.Model
{

    class CarList
    {
        private static CarList? _instance;
        private static readonly object _lock = new object();

        private List<Car> _carCollection;

        public List<Car> CarCollection
        {
            get => _carCollection;
            set
            {
                _carCollection = value;
            }
        }

        private CarList()
        {
            _carCollection = new List<Car>();
            Seed();
        }

        public static CarList Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new CarList();
                    }
                    return _instance;
                }
            }
        }

        public void AddCar(Car car)
        {
            CarCollection = CarCollection.Append(car).ToList();
        }

        public void RemoveCar(Car car)
        {
            CarCollection.Remove(car);
        }

        public override string ToString()
        {
            string result = "";
            foreach (Car car in CarCollection)
            {
                result += $"{car.Id:4} {car.Brand}\n";
            }
            return result;
        }

        private void Seed()
        {
            AddCar(new Car(GenerateId(), "Toyota", "Corolla", 2010, 'A', 1, 15.0f, 100000, "A nice car"));
            AddCar(new Car(GenerateId(), "Ford", "Focus", 2015, 'M', 1, 14.3f, 50000, "A nice car"));
            AddCar(new Car(GenerateId(), "Volkswagen", "Golf", 2018, 'M', 1, 18.2f, 20000, "A sporty car"));
            AddCar(new Car(GenerateId(), "Audi", "A4", 2019, 'A', 2, 19.0f, 10000, "A luxurious car"));
            AddCar(new Car(GenerateId(), "BMW", "M3", 2020, 'M', 2, 12.0f, 5000, "A sporty car"));
            AddCar(new Car(GenerateId(), "Mercedes", "C-Class", 2021, 'A', 2, 25.0f, 1000, "A luxurious car"));
            AddCar(new Car(GenerateId(), "Tesla", "Model S", 2022, 'A', 3, 45.0f, 100, "An electric car"));
        }

        public int GenerateId()
        {
            int id = 0;
            if (CarCollection.Count > 0)
                id = CarCollection.Last().Id + 1;
            return id;
        }
    }
}

