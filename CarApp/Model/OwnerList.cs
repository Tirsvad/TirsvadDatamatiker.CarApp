namespace CarApp.Model;

public class OwnerList
{
    private List<Owner> _owners = new List<Owner>();

    public static OwnerList Instance { get; } = new OwnerList();

    private OwnerList()
    {
        Seed();
    }

    public List<Owner> Owners
    {
        get
        {
            return _owners;
        }
    }

    private void Seed()
    {
        _owners.Add(new Owner(0, "John Nielsen"));
        _owners.Add(new Owner(1, "Trine Nielsen"));
        _owners.Add(new Owner(2, "Alice Jensen")); // is another person but same name
        _owners.Add(new Owner(3, "Bob Pedersen"));
        _owners.Add(new Owner(4, "Charles Hansen"));
        _owners.Add(new Owner(5, "Diana Andersen"));
        _owners.Add(new Owner(6, "Trine Nielsen")); // is another person but same name
    }

    public void AddOwner(Owner owner)
    {
        _owners.Add(owner);
    }

    public void RemoveOwner(Owner owner)
    {
        _owners.Remove(owner);
    }

    public List<Owner> GetOwner(string name)
    {
        return _owners.FindAll(owner => owner.Name == name);
    }

    public Owner? GetOwnerById(int id)
    {
        return _owners.Find(owner => owner.Id == id);
    }
}
