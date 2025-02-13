namespace CarApp
{
    // 1. Define your Entities
    /// <summary>
    /// Represents a type of fuel with a name and price.
    /// </summary>
    public class FuelType
    {
        /// <summary>
        /// Gets or sets the ID of the fuel type.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the fuel type.
        /// </summary>
        public string Name { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the price of the fuel type.
        /// </summary>
        public decimal Price { get; set; } = 0;
    }

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
        public string Brand { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the model of the car.
        /// </summary>
        public string Model { get; set; } = String.Empty;

        /// <summary>
        /// Gets or sets the year of the car.
        /// </summary>
        public int Year { get; set; } = 0;

        /// <summary>
        /// Gets or sets the gear type of the car.
        /// </summary>
        public char GearType { get; set; } = 'M';

        public int? FuelTypeId { get; set; } // Foreign Key
        public FuelType? FuelType { get; set; } // Navigation property

        /// <summary>
        /// Gets or sets the fuel efficiency of the car.
        /// </summary>
        public float FuelEfficiency { get; set; } = 0;

        /// <summary>
        /// Gets or sets the mileage of the car.
        /// </summary>
        public int Mileage { get; set; } = 0;

        /// <summary>
        /// Gets or sets the description of the car.
        /// </summary>
        public string Description { get; set; } = String.Empty;

        // Non database properties

        public bool isEngineRunning = false;

        /// <summary>
        /// Adds a tour distance to the car's mileage.
        /// </summary>
        /// <param name="km">The distance of the tour in kilometers.</param>
        public void AddTour(int km)
        {
            if (isEngineRunning)
            {
                Mileage += km;
            }
        }
    }

    //public class CarAppContext
    //{
    //    public List<Car>? Cars { get; set; }
    //    public List<FuelType>? FuelTypes { get; set; }
    //}
}
