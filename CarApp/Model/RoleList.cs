namespace CarApp.Model
{
    class RoleList
    {
        private static RoleList? _instance;
        private static readonly object _lock = new object();
        public static RoleList? Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new RoleList();
                    }
                    return _instance;
                }
            }
        }

        public List<Role> Roles { get; private set; }

        private RoleList()
        {
            Roles = new List<Role>();
            Seed();
        }

        private void Seed()
        {
            Roles.Add(new Role(1, "Admin"));
            Roles.Add(new Role(2, "User"));
        }
    }
}
