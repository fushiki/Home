using System;
using System.Collections.Generic;

namespace Home.Budget.Database
{
    [Serializable]
    public abstract class ShoppingDatabase
    {
        public static string Name = "ShoppingDatabase";



        public abstract List<Shop> Shops { get; }

        public abstract ProductCategory AllCategory { get; }

        public abstract SortedList<DateTime, Shopping> ShoppingHistory { get; }

        public virtual void Save() { }
    }
}
