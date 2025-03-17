using System.Text.Json;
using CarApp.Model;

namespace CarApp.Handler;

class JsonFileHandler
{
    private static JsonFileHandler? _instance;
    private static readonly object _lock = new object();
    public static JsonFileHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new JsonFileHandler();
                    }
                }
            }
            return _instance;
        }
    }

    private const string _filePath = Constants.jsonFilePath;
    private readonly Version _version = new Version(1, 0);
    private readonly DataContainer _dataContainer = new DataContainer();

    public class DataContainer
    {
        public Version? Version { get; set; }
        public List<Car> Cars { get; set; } = CarList.Instance.GetCars();
        public List<Owner> Owners { get; set; } = OwnerList.Instance.GetOwners();

        public DataContainer()
        {
            Version = null;
        }
    }

    private JsonFileHandler()
    {
        _dataContainer = new DataContainer
        {
            Version = _version,
        };
    }

    public void ExportData(string filePath = _filePath)
    {
        lock (_lock)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            // Debugging: Check if CarList.Instance is populated
            if (_dataContainer.Cars == null || !_dataContainer.Cars.Any())
            {
                Console.WriteLine("No cars to export.");
                Console.WriteLine("Press any key to continue...");
                Console.WriteLine(Console.ReadLine());
                return;
            }
            string jsonString = JsonSerializer.Serialize(_dataContainer, options);
            File.WriteAllText(filePath, jsonString);
        }
    }

    public void ImportData(string filename)
    {
        lock (_lock)
        {
            try
            {
                if (File.Exists(filename))
                {
                    string jsonString = File.ReadAllText(filename);
                    var data = JsonSerializer.Deserialize<DataContainer>(jsonString);
                    if (data == null)
                    {
                        Console.WriteLine("No dataContainer file found.");
                        return;
                    }
                    switch (data)
                    {
                        case { Version: { Major: 1, Minor: 0 } }:
                            _dataContainer.Version = data.Version;
                            if (data.Cars != null)
                            {
                                _dataContainer.Cars?.Clear();
                                foreach (var car in data.Cars)
                                {
                                    _dataContainer.Cars?.Add(car);
                                }
                            }
                            if (data.Owners != null)
                            {
                                _dataContainer.Owners?.Clear();
                                foreach (var owner in data.Owners)
                                {
                                    _dataContainer.Owners?.Add(owner);
                                }
                            }
                            break;
                        default:
                            Console.WriteLine("Incompatible dataContainer file version.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error importing dataContainer: {ex.Message}");
            }
        }
    }
}
