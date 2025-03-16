namespace CarApp.Model;

public class User
{
    public enum RoleType
    {
        Guest,
        User,
        Admin
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string Phone { get; private set; }
    public string Address { get; private set; }
    public RoleType Role { get; private set; }

    public User(int id, string name, string password, string email, string phone, string address, RoleType role)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        Phone = phone;
        Address = address;
        Role = role;
    }
}
