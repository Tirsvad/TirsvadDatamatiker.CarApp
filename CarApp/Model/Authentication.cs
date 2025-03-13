namespace CarApp.Model
{
    class Authentication
    {
        private UserList Users { get; set; }

        public string? User { get; private set; }

        public Authentication()
        {
            Users = UserList.Instance;
        }

        public bool Login(string username, string password)
        {
            foreach (User user in Users.Users)
            {
                if (user.Name == username && user.Password == password)
                {
                    User = username;
                    return true;
                }
            }
            return false;
        }

        public void Logout()
        {
            User = null;
        }

        public Role GetRole(string? user)
        {
            if (user == null)
                return Role.Guest;

            foreach (User item in Users.Users)
            {
                if (item.Name == User)
                {
                    return (Role)item.RolleId;
                }
            }

            return Role.Guest;
        }
    }
}
