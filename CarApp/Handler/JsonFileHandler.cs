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
        public CarList? Cars { get; set; }
        public OwnerList? Owners { get; set; }
    }

    private JsonFileHandler()
    {
        _dataContainer = new DataContainer
        {
            Version = _version,
            Cars = CarList.Instance,
            Owners = OwnerList.Instance
        };
    }

    public void ExportData(string filePath = _filePath)
    {
        lock (_lock)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(_dataContainer, options);
            File.WriteAllText(filePath, jsonString);
        }
    }

    public DataContainer? ImportData(string filename)
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
                        return null;
                    }
                    switch (data)
                    {
                        case { Version: { Major: 1, Minor: 0 } }:
                            DataContainer dataContainer = new DataContainer();
                            dataContainer.Version = data.Version;
                            dataContainer.Cars = data.Cars;
                            dataContainer.Owners = data.Owners;
                            return dataContainer;
                        default:
                            Console.WriteLine("Incompatible dataContainer file version.");
                            return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error importing dataContainer: {ex.Message}");
            }
            return null;
        }
    }
}
