using System;
using System.Collections.Generic;

namespace Home.Budget
{
    class ShoppingHistory
    {
        public SortedList<DateTime, Shopping> History { get; private set; }

        public ShoppingHistory()
        {
            History = BudgetTopic.Get().Database.ShoppingHistory;
        }
        
    }
}
