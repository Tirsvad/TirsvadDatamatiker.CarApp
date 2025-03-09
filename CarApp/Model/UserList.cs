namespace CarApp.Model
{
    class UserList
    {
        private static UserList? _instance;
        private static readonly object _lock = new object();
        public static UserList? Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new UserList();
                    }
                    return _instance;
                }
            }
        }

        public List<User> Users { get; private set; }

        private UserList()
        {
            Users = new List<User>();
            Seed();
        }

        public void Seed()
        {
            Users.Add(new User(
                1,                      // Id
                "user1",                // Name
                "user1",                // Password
                "user1@hotmail.com",    // Email
                "61616161",             // Phone
                "Acme 22",              // Address
                1));                    // See Constants.RoleType
            Users.Add(new User(
                2,
                "admin1",
                "admin1",
                "admin1@hotmail.com",
                "61616162",
                "Acme 23",
                0));
        }
    }
}
