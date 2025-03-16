namespace CarApp.Model;

/// <summary>
/// Represents an owner of a car.
/// </summary>
public class Owner
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    public Owner(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public void UpdateName(string name)
    {
        Name = name;
    }
}
