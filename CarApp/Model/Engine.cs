namespace CarApp.Model
{
    public class Engine
    {
        public enum FuelType
        {
            Benzin,
            Diesel,
            Electric,
            Hybrid,
            Brint
        }

        public string Name { get; }
        public double? Ccm { get; }
        public int HorsePower { get; }
        public int Torque { get; }
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
        public Engine(string name, double ccm, int horsePower, int torque, FuelType fuel, int mileage, DateTime lastService, int serviceIntervalMileage, int serviceIntervalMonths)
        {
            Name = name;
            Ccm = ccm;
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
        public Engine(string name, int horsePower, int torque, FuelType fuel, int mileage, DateTime lastService, int serviceIntervalMileage, int serviceIntervalMonths)
        {
            Name = name;
            HorsePower = horsePower;
            Torque = torque;
            Fuel = fuel;
            Mileage = mileage;
            LastService = lastService;
            ServiceIntervalMileage = serviceIntervalMileage;
            ServiceIntervalMonths = serviceIntervalMonths;
        }

        public void AddMileage(int mileage)
        {
            Mileage += mileage;
        }

        public bool IsServiceTime()
        {
            return (Mileage >= ServiceIntervalMileage);
        }

        public static bool TryParseHorsePower(string input, out int horsePower)
        {
            if (int.TryParse(input, out horsePower) && horsePower > 0)
            {
                return true;
            }
            horsePower = 0;
            return false;
        }

        public static bool TryParseTorque(string input, out int torque)
        {
            if (int.TryParse(input, out torque) && torque > 0)
            {
                return true;
            }
            torque = 0;
            return false;
        }

        public static bool TryParseCcm(string input, out double ccm)
        {
            if (double.TryParse(input, out ccm) && ccm > 0)
            {
                return true;
            }
            ccm = 0;
            return false;
        }

        public static bool TryParseServiceIntervalMileage(string input, out int serviceIntervalMileage)
        {
            if (int.TryParse(input, out serviceIntervalMileage) && serviceIntervalMileage > 0)
            {
                return true;
            }
            serviceIntervalMileage = 0;
            return false;
        }

        public static bool TryParseServiceIntervalMonths(string input, out int serviceIntervalMonths)
        {
            if (int.TryParse(input, out serviceIntervalMonths) && serviceIntervalMonths > 0)
            {
                return true;
            }
            serviceIntervalMonths = 0;
            return false;
        }

        public override string ToString()
        {
            return $"{Name} ({Ccm} ccm, {HorsePower} hp, {Torque} Nm, {Fuel})";
        }

    }
}
