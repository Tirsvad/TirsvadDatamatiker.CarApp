using System.Text.Json.Serialization;
using CarApp.Type;

namespace CarApp.Model
{
    /// <summary>
    /// Represents the engine of a car.
    /// </summary>
    public class Engine
    {
        public string Name { get; }
        public int? Ccm { get; }
        public int HorsePower { get; }
        public int Torque { get; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FuelType Fuel { get; }
        public int Mileage { get; private set; }
        public DateTime LastService { get; private set; } ///> Last service date
        public int ServiceIntervalMileage { get; } ///> Service interval in kilometers
        public int ServiceIntervalMonths { get; } ///> Service interval in months

        /// <summary>
        /// Constructor for ICE Engine (Internal Combustion Engine)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ccm"></param>
        /// <param name="horsePower"></param>
        /// <param name="torque"></param>
        /// <param name="fuel"></param>
        /// <param name="mileage"></param>
        /// <param name="lastService"></param>
        /// <param name="serviceIntervalMileage"></param>
        /// <param name="serviceIntervalMonths"></param>
        //[JsonConstructor]
        public Engine(string name, int? ccm, int horsePower, int torque, FuelType fuel, int mileage, DateTime lastService, int serviceIntervalMileage, int serviceIntervalMonths)
        {
            Name = name;
            Ccm = ccm ?? 0;
            HorsePower = horsePower;
            Torque = torque;
            Fuel = fuel;
            Mileage = mileage;
            LastService = lastService;
            ServiceIntervalMileage = serviceIntervalMileage;
            ServiceIntervalMonths = serviceIntervalMonths;
        }

        /// <summary>
        /// Constructor for El Engine (Electric / Hydrogen)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="horsePower"></param>
        /// <param name="torque"></param>
        /// <param name="fuel"></param>
        /// <param name="mileage"></param>
        /// <param name="lastService"></param>
        /// <param name="serviceIntervalMileage"></param>
        /// <param name="serviceIntervalMonths"></param>
        //public Engine(string name, int horsePower, int torque, FuelType fuel, int mileage, DateTime lastService, int serviceIntervalMileage, int serviceIntervalMonths)
        //{
        //    Name = name;
        //    HorsePower = horsePower;
        //    Torque = torque;
        //    Fuel = fuel;
        //    Mileage = mileage;
        //    LastService = lastService;
        //    ServiceIntervalMileage = serviceIntervalMileage;
        //    ServiceIntervalMonths = serviceIntervalMonths;
        //}


        /// <summary>
        /// Adds mileage to the engine.
        /// </summary>
        public void AddMileage(int mileage)
        {
            Mileage += mileage;
        }

        /// <summary>
        /// Checks if the engine needs service.
        /// </summary>
        public bool IsServiceTime()
        {
            return (Mileage >= ServiceIntervalMileage);
        }

        /// <summary>
        /// Parses the input string to an integer and returns the value if it is greater than 0.
        /// </summary>
        /// <Returns>Nullable int. Null if the input is not a valid integer or is less than 1.</Returns>
        public static bool ParseHorsePower(string input, out Int32 horsePower)
        {
            if (int.TryParse(input, out horsePower) && horsePower > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Formats the engine as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name} ({Ccm} ccm, {HorsePower} hp, {Torque} Nm, {Fuel})";
        }
    }
}
