namespace CarApp
{
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
}
