using System;
using Home.Core;

namespace Home.Budget
{
    [Serializable]
    public abstract class BudgetItemModel:IButtonGridItemModel
    {
        public string Name { get; set; }
        public string ImageName { get; set; }
    }
}
