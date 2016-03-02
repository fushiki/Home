using System;

namespace Home.Budget
{
    [Serializable]
    public class Purchase
    {
        public Product Product { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double FullPrice => Price * Quantity; 


    }
}
