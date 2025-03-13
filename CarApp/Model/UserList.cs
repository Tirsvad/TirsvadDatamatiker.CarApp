namespace CarApp.Model
{
    public class UserList
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

            // Here we could put OS user data into the env.tmp file and read it here
            // It will not be implemented in the sources

            var envFilePath = "env.tmp"; // Sensitive data should not be stored in the source code
            if (File.Exists(envFilePath))
            {
                var envVariables = File.ReadAllLines(envFilePath);
                foreach (var line in envVariables)
                {
                    var parts = line.Split('=', 2);
                    if (parts.Length == 2)
                    {
                        Environment.SetEnvironmentVariable(parts[0], parts[1]);
                    }
                }
            }

            Users.Add(new User(
                int.Parse(Environment.GetEnvironmentVariable("USER_ID") ?? "3"), // Id
                Environment.GetEnvironmentVariable("USER_NAME") ?? "defaultUser", // User name
                Environment.GetEnvironmentVariable("USER_PASSWORD") ?? "defaultPassword", // User password
                Environment.GetEnvironmentVariable("USER_EMAIL") ?? "defaultUser@example.com", // User email
                Environment.GetEnvironmentVariable("USER_PHONE") ?? "00000000", // User phone
                Environment.GetEnvironmentVariable("USER_ADDRESS") ?? "Default Address", // User address,
                int.Parse(Environment.GetEnvironmentVariable("USER_ROLE_ID") ?? ((int)Role.Guest).ToString()))); // User role id

            foreach (var item in Users)
            {
                Console.WriteLine($"{item.Name} {item.Password}");
            }

            //Environment.Exit(0);
        }
    }
}
