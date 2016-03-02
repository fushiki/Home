

using System.Collections.ObjectModel;

namespace Home.Presentation.ViewModels
{
    class CalendarTreeLeaf:CalendarTreeItem
    {
        public ObservableCollection<ShoppingViewModel> Shoppings { get; } 
        public CalendarTreeLeaf(string datename) : base(datename)
        {
            Shoppings = new ObservableCollection<ShoppingViewModel>();
        }
    }
}
