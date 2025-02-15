using System.Text.Json;

namespace CarApp
{
    public class JsonFileHandler
    {
        public void ExportData(string filename, DataContainer dataContainer)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(dataContainer, options);
            File.WriteAllText(filename, jsonString);
        }

        public DataContainer? ImportData(string filename)
        {
            try
            {
                if (File.Exists(filename))
                {
                    string jsonString = File.ReadAllText(filename);
                    var data = JsonSerializer.Deserialize<DataContainer>(jsonString);
                    if (data != null)
                    {
                        return data;
                    }
                }
                else
                {
                    Console.WriteLine("No data file found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error importing data: {ex.Message}");
            }
            return null;
        }

        public class DataContainer
        {
            public int? FileVersion { get; set; } = null;
            public List<Car> Cars { get; set; } = new List<Car>();
            public List<FuelType> FuelTypes { get; set; } = new List<FuelType>();
        }
    }

}
