namespace CarApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string brand;
            string model;
            int year;
            char gearType;

            Console.Write("Indtast bilmærke: ");
            brand = Console.ReadLine();

            Console.Write("Indtast bilmodel: ");
            model = Console.ReadLine();

            Console.Write("Indtast årgang: ");
            year = Convert.ToInt32(Console.ReadLine());

            Console.Write("Indtast geartype: ");
            gearType = Convert.ToChar(Console.Read());

            Console.WriteLine($"Bilmærke: {brand}");
            Console.WriteLine($"Bilmodel: {model}");
            Console.WriteLine($"Årgang: {year}");
            Console.WriteLine($"Gear: {gearType}");
        }
    }
}
