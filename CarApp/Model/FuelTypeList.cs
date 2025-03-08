namespace CarApp.Model;

class FuelTypeList
{
    private static FuelTypeList? _instance;
    private static readonly object _lock = new object();

    private List<FuelType> FuelTypes { get; set; }

    private FuelTypeList()
    {
        FuelTypes = new List<FuelType>();
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

    public List<FuelType> GetFuelTypes()
    {
        return FuelTypes;
    }

    public bool Exists(int id)
    {
        return FuelTypes.Any(f => f.Id == id);
    }

    public void Seed()
    {
        lock (_lock)
        {
            FuelTypes.Add(new FuelType(GenerateId(), "Benzin 95", 14.99m));
            FuelTypes.Add(new FuelType(GenerateId(), "Diesel", 14.59m));
            FuelTypes.Add(new FuelType(GenerateId(), "El", 2.85m));
            FuelTypes.Add(new FuelType(GenerateId(), "Benzin 100", 16.96m));
        }
    }

    public int GenerateId()
    {
        int id = 0;
        if (FuelTypes.Count > 0)
            id = FuelTypes.Last().Id + 1;
        return id;
    }
}
