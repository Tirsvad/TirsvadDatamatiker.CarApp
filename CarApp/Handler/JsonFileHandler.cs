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

    private const string _filePath = Constants.jsonFilePath; ///> The file path for the JSON file.
    private readonly Version _version = new Version(1, 0); ///> The version of the JSON file.
    private readonly DataContainer _dataContainer = new DataContainer(); ///> The data container for the JSON file.

    /// <summary>
    /// Represents the data container for the JSON file.
    /// </summary>
    public class DataContainer
    {
        public Version? Version { get; set; }
        public List<Car>? Cars { get; set; }
        public List<Owner>? Owners { get; set; }

        public DataContainer()
        {
            Version = null;
            Cars = CarList.Instance.GetCars();
            Owners = OwnerList.Instance.GetOwners();
        }
    }

    private JsonFileHandler()
    {
        _dataContainer = new DataContainer
        {
            Version = _version,
        };
    } ///> Private constructor for singleton pattern

    /// <summary>
    /// Exports data to a JSON file.
    /// </summary>
    /// <param name="filePath">Optional. Default value from Constants.jsonFilePath</param>
    public void ExportData(string filePath = _filePath)
    {
        lock (_lock)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_dataContainer, options);
            File.WriteAllText(filePath, jsonString);
        }
    }

    /// <summary>
    /// Imports data from a JSON file.
    /// Reassign owner object for Car objects. So owner object is the same in OwnerList and CarList.
    /// </summary>
    /// <param name="filename">Optional. Default value from Constants.jsonFilePath</param>
    public void ImportData(string filename = _filePath)
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
                    if (data.Version?.Major == 1)
                    {
                        for (int i = 0; i < data.Owners?.Count; i++)
                        {
                            Owner owner = data.Owners[i];
                            OwnerList.Instance.AddOwner(owner);
                        }
                        for (int i = 0; i < data.Cars?.Count; i++)
                        {
                            Car car = data.Cars[i];
                            car.Owner = OwnerList.Instance.GetOwners().Find(owner => owner.Id == car.Owner?.Id); // Reassign owner object
                            CarList.Instance.Add(car);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incompatible dataContainer file version.");
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
