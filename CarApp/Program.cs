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
            brand = Console.ReadLine() ?? String.Empty;

            Console.Write("Indtast bilmodel: ");
            model = Console.ReadLine() ?? String.Empty;

            Console.Write("Indtast årgang: ");
            year = Convert.ToInt32(Console.ReadLine());

            do
            {
                Console.Write("Indtast geartype: ");
                // Console.Write("Indtast geartype (A/M): ");
                gearType = char.ToUpper(Convert.ToChar(Console.Read()));
                Console.ReadLine(); // To consume the newline character
            } while (gearType != 'A' && gearType != 'M');

            Console.WriteLine($"Bilmærke: {brand}");
            Console.WriteLine($"Bilmodel: {model}");
            Console.WriteLine($"Årgang: {year}");
            Console.WriteLine($"Gear: {gearType}");

            Console.WriteLine($"Bil: {brand} {model} fra {year} med {gearType} gear");
        }
    }
}
