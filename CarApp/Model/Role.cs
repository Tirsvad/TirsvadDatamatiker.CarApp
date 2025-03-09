namespace CarApp.Model
{
    class Role
    {
        public int Id { get; private set; }
        public string Type { get; private set; }

        public Role(int id, string type)
        {
            Id = id;
            Type = type;
        }
    }
}
