namespace CarApp.Model;

public class Wheel
{
    public Tire Tire { get; set; }
    // TODO Add Rims

    public Wheel(Tire tire)
    {
        Tire = tire;
    }

    public static Wheel[] GetSetOf4Wheels(Tire tire)
    {
        return new Wheel[]
        {
            new Wheel(tire),
            new Wheel(tire),
            new Wheel(tire),
            new Wheel(tire)
        };
    }
}
