namespace Home.Core
{
    public class User
    {
        private string _name;
        private Address _address;

        public User(string name, Address address)
        {
            _name = name;
            _address = address;
        }

        public string Country { get; private set; }
        public string City { get; private set; }
    }
}
