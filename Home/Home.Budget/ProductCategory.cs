using System;
using System.Collections.Generic;

namespace Home.Budget
{
    [Serializable]
    public class ProductCategory:BudgetItemModel
    {
        
        public List<ProductCategory> SubCategories { get; }
        public List<Product> Products { get; }

        public ProductCategory()
        {
            SubCategories = new List<ProductCategory>();
            Products = new List<Product>();
        }

    }
}
