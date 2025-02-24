namespace CarApp.Model
{
    /// <summary>
    /// Represents a car with various properties.
    /// </summary>
    public class Car
    {
        /// <summary>
        /// Gets and sets the Id of the car.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the brand of the car.
        /// </summary>
        public string Brand { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the model of the car.
        /// </summary>
        public string Model { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the year of the car.
        /// </summary>
        public uint Year { get; set; } = 0;

        /// <summary>
        /// Gets or sets the gear type of the car.
        /// </summary>
        public char GearType { get; set; } = 'M';

        /// <summary>
        /// Gets or sets the fuel type ID of the car.
        /// </summary>
        public int? FuelTypeId { get; set; }

        /// <summary>
        /// Gets or sets the fuel type of the car.
        /// </summary>
        public FuelType? FuelType { get; set; } // Navigation property

        /// <summary>
        /// Gets or sets the fuel efficiency of the car.
        /// </summary>
        public float FuelEfficiency { get; set; } = 0;

        /// <summary>
        /// Gets or sets the mileage of the car.
        /// </summary>
        public uint Mileage { get; set; } = 0;

        /// <summary>
        /// Gets or sets the description of the car.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        // Non database properties

        /// <summary>
        /// Gets or sets the value indicating whether the engine is running.
        /// </summary>
        public bool IsEngineRunning { get; set; } = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Car"/> class.
        /// </summary>
        public Car() { }

        private DbSqliteHnd _dbSqliteHndInstance = DbSqliteHnd.Instance;

        /// <summary>
        /// Toggle the engine on or off.
        /// </summary>
        public void ToggleEngine()
        {
            if (IsEngineRunning)
            {
                StopEngine();
            }
            else
            {
                StartEngine();
            }
        }

        /// <summary>
        /// Starts the engine.
        /// </summary>
        public void StartEngine()
        {
            IsEngineRunning = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void StopEngine()
        {
            IsEngineRunning = false;
        }

        /// <summary>
        /// Adds a tour distance to the car's mileage if engine is running else it will simulate tour.
        /// </summary>
        /// <param name="addDistanceInKm">The distance of the tour in kilometers.</param>
        public void UpdateMileAge(int addDistanceInKm)
        {
            if (IsEngineRunning)
            {
                Mileage += (uint)addDistanceInKm;
                _dbSqliteHndInstance.UpdateCar(this);
            }
        }

        /// <summary>
        /// Calculates the amount of fuel needed for a given distance.
        /// </summary>
        /// <param name="car">The car object containing fuel efficiency information.</param>
        /// <param name="distance">The distance to be traveled in kilometers.</param>
        /// <returns>The amount of fuel needed for the given distance.</returns>
        public static double CalculateFuelNeeded(Car car, int distance)
        {
            return (double)(distance / car.FuelEfficiency);
        }

        /// <summary>
        /// Calculates the trip cost based on the fuel needed and the fuel price.
        /// </summary>
        /// <param name="car">The car object containing the fuel type information.</param>
        /// <param name="fuelNeeded">The amount of fuel needed for the trip.</param>
        /// <returns>The cost of the trip.</returns>
        public decimal CalculateTripCost(Car car, double fuelNeeded)
        {
            IEnumerable<FuelType> fuelTypes = _dbSqliteHndInstance.GetFuelTypes(); // Get the fuel types from the database
            return (decimal)(fuelNeeded * (float)fuelTypes.First(ft => ft.Id == car.FuelTypeId).Price);
        }

        /// <summary>
        /// Checks if a car's mileage is a palindrome.
        /// </summary>
        /// <param name="car">The car object containing the mileage information.</param>
        /// <returns>True if the mileage is a palindrome, otherwise false.</returns>
        public static bool IsPalindrome(Car car)
        {
            string odometer = car.Mileage.ToString();

            for (int i = 0; i < odometer.Length / 2; i++)
            {
                if (odometer[i] != odometer[odometer.Length - i - 1])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
