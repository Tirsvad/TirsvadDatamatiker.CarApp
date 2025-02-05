namespace CarApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string brand = "Toyota";
            string model = "Corolla";
            int year = 2020;
            char gearType = 'A';

            Console.WriteLine($"Bilmærke: {brand}");
            Console.WriteLine($"Bilmodel: {model}");
            Console.WriteLine($"Årgang: {year}");
            Console.WriteLine($"Gear: {gearType}");
        }
    }
}
