using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Home.Budget;

namespace Home.Presentation.ViewModels
{
    public class ShoppingViewModel
    {
        public DateTime Date { get; }
        public Shopping Shopping { get; }
        public string Header => Date.ToString();
        public ShoppingViewModel(DateTime date, Shopping shoppingValue)
        {
            Date = date;
            Shopping = shoppingValue;
        }
    }
}
