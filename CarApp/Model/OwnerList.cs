namespace CarApp.Model;

/// <summary>
/// Represents a list of owners.
/// </summary>
public class OwnerList
{
    private static OwnerList? _instance;
    private static readonly object _lock = new object();
    public static OwnerList Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new OwnerList();
                }
                return _instance;
            }
        }
    }

    public List<Owner> Owners { get; private set; } ///> List of owners

    /// <summary>
    /// Initializes a new instance of the <see cref="OwnerList"/> class.
    /// JsonConstructor is used to create an instance of the class from JSON.
    /// </summary>
    private OwnerList()
    {
        Owners = [];
        //Seed();
    }

    public List<Owner> GetOwners()
    {
        return Owners;
    }

    /// <summary>
    /// Seeds the list of owners with some initial data.
    /// Only used for testing purposes as we get the data from the json file
    /// </summary>
    private void Seed()
    {
        Owners.Add(new Owner(0, "John Nielsen"));
        Owners.Add(new Owner(1, "Trine Nielsen"));
        Owners.Add(new Owner(2, "Alice Jensen")); // is another person but with same name
        Owners.Add(new Owner(3, "Bob Pedersen"));
        Owners.Add(new Owner(4, "Charles Hansen"));
        Owners.Add(new Owner(5, "Diana Andersen"));
        Owners.Add(new Owner(6, "Trine Nielsen")); // is another person but with same name
    }

    /// <summary>
    /// Adds an owner to the list.
    /// </summary>
    /// <param name="owner">The owner to add.</param>
    public void AddOwner(Owner owner)
    {
        Owners.Add(owner);
    }

    /// <summary>
    /// Removes an owner from the list.
    /// </summary>
    /// <param name="owner">The owner to remove.</param>
    public void RemoveOwner(Owner owner)
    {
        Owners.Remove(owner);
    }

    /// <summary>
    /// Gets all owners with a specific name.
    /// </summary>
    /// <returns>A list of owners with the specified name.</returns>
    public List<Owner> GetOwner(string name)
    {
        return Owners.FindAll(owner => owner.Name == name);
    }

    /// <summary>
    /// Gets an owner by their ID.
    /// </summary>
    /// <param name="id">The ID of the owner to get.</param>
    public Owner? GetOwnerById(int id)
    {
        return Owners.Find(owner => owner.Id == id);
    }
}
