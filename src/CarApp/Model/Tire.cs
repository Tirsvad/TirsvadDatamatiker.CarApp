namespace CarApp.Model
{
    public class Tire
    {
        public enum SeasonType
        {
            Winter,
            Summer,
            AllSeason
        }

        public enum ConstructionType
        {
            Radial,
            Bias,
            Diagonal
        }

        public string Brand { get; } ///> Brand of the tire
        public string Model { get; } ///> Model of the tire
        public int Width { get; } ///> Width of the tire in mm
        public int Height { get; } ///> Height of the tire in mm
        public int Inch { get; } ///> Diameter of the tire in inches
        public ConstructionType Construction { get; } ///> ConstructionType of the tire
        public SeasonType Season { get; } ///> SeasonType of the tire

        public Tire(string brand, string model, int width, int height, int inch, ConstructionType construction, SeasonType season)
        {
            Brand = brand;
            Model = model;
            Width = width;
            Height = height;
            Inch = inch;
            Construction = construction;
            Season = season;
        }
    }
}
