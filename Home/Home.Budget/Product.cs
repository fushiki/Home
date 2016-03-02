using System;

namespace Home.Budget
{
    [Serializable]
    public class Product:BudgetItemModel
    {
        public enum ProductVolumeType
        {
            Piece,
            Mass
        }

        
        public double Volume { get; set; }
        public string Brand { get; set; }
        public ProductVolumeType Type { get; set; }
        

        
    }
}
