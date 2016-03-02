namespace Home.Core
{
    public class Address
    {
        public enum AddressType
        {
            Street,
            Settlement
        }

        public string Country { get; private set; }
        public string City { get; private set; }
        public string Place { get; private set; }
        public AddressType Type { get; private set; }
        public int Building { get; private set; }
        public int? Apartment { get; private set; }

        public Address(string country, string city, string place, AddressType type, int building, int? apartment)
        {
            Country = country;
            City = city;
            Place = place;
            Type = type;
            Building = building;
            Apartment = apartment;
        }
        public Address(string country, string city, string place, AddressType type, int building)
            :this(country,city,place,type,building,null)
        {
            
        }
        public Address(string city, string place, AddressType type, int building, int? apartment)
            : this(Source.Get().ActualUser.Country,
                  city, place, type, building, apartment)
        { }
        public Address(string place, AddressType type, int building, int? apartment)
       : this(Source.Get().ActualUser.Country, Source.Get().ActualUser.City,
             place, type, building, apartment)
        { }
        public Address(string city, string place, AddressType type, int building)
            : this(Source.Get().ActualUser.Country,
                  city, place, type, building, null)
        { }
        public Address(string place, AddressType type, int building)
       : this(Source.Get().ActualUser.Country, Source.Get().ActualUser.City,
             place, type, building, null)
        { }

    }
}
