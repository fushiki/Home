using System;

namespace Home.Budget
{
    [Serializable]
    public class Shop
    {
        public enum ShopType
        {
            GroceryStore
        }

        public ShopType Type { get; private set; }
        public string Name { get; private set; }
        public string Brand { get; private set; }
        public Shop(string name,string brand, ShopType type)
        {
            Type = type;
            Name = name;
            Brand = brand;
        }



    }
}
