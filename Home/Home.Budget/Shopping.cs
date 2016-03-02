using System;
using System.Collections.Generic;

namespace Home.Budget
{
    [Serializable]
    public class Shopping
    {
        public DateTime Date { get; private set; }
        public Shop Shop { get; private set; }
        public List<Purchase> ShoppingList { get; private set; }

        public Shopping(DateTime date, Shop shop)
        {
            Date = date;
            Shop = shop;
            ShoppingList = new List<Purchase>();
        }



    }
}
