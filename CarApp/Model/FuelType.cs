namespace CarApp
{
    /// <summary> 
    /// Represents a type of fuel with a name and price.
    /// </summary>
    public class FuelType
    {
        private int _id;
        private string _name;
        private decimal _price;

        public FuelType(int id, string name, decimal price)
        {
            _id = id;
            _name = name;
            _price = price;
        }

        /// <summary>
        /// Gets or sets the ID of the fuel type.
        /// </summary>
        public int Id
        {
            get { return _id; }
        }

        /// <summary>
        /// Gets or sets the name of the fuel type.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets or sets the price of the fuel type.
        /// </summary>
        public decimal Price
        {
            get { return _price; }
        }
    }
}
