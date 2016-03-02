using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Home.Budget;

namespace Home.Presentation.ViewModels
{
    public class TreeCalendar:BaseViewModel
    {
        public ObservableCollection<CalendarTreeItem> Items { get; }
        public ICommand CommandSelectedItemChanged { get; }
        public CalendarTreeItem SelectedItem { get; private set; }
        public event CalendarEventHandler SelectedChanged;
        public TreeCalendar ()
        {
            Items = new ObservableCollection<CalendarTreeItem>(BuildTree());
            CommandSelectedItemChanged = new LambdaCommand(OnSelectedItemChanged);
        }

        private void OnSelectedItemChanged(object parameter)
        {
            SelectedItem = parameter as CalendarTreeItem;;
            SelectedChanged?.Invoke(this,new CalendarEventHandlerArgs() {Item=SelectedItem});
        }


        private List<CalendarTreeItem> BuildTree()
        {
            var history = BudgetTopic.Get().Database.ShoppingHistory;
            List<CalendarTreeItem> result = new List<CalendarTreeItem>();
            CalendarTreeItem lastyear = null;
            CalendarTreeItem lastmonth = null;
            CalendarTreeLeaf lastday = null;
            int year = 1000;
            int month = -1;
            int day = -1;
            foreach (var shopping in history)
            {
                var date = shopping.Key;
                var shoppingValue = shopping.Value;
                if (date.Year > year)
                {
                    lastyear = new CalendarTreeItem(date.Year.ToString());
                    year = date.Year;
                    month = -1;
                    result.Add(lastyear);
                }
                if (date.Month > month)
                {
                    lastmonth = new CalendarTreeItem(MonthsTranslation[date.Month]);
                    month = date.Month;
                    day = -1;
                    lastyear.Items.Add(lastmonth);
                }
                if (date.Day > day)
                {
                    lastday = new CalendarTreeLeaf(date.Day.ToString());
                    day = date.Day;
                    lastmonth.Items.Add(lastday);
                }
                lastday.Shoppings.Add(new ShoppingViewModel(date,shoppingValue));
            }
            return result;
            ;
        }


        static Dictionary<int, string> MonthsTranslation;

        static TreeCalendar()
        {
            MonthsTranslation = new Dictionary<int, string>(12)
            {
                {1, "Styczeń"},
                {2,"Luty" },
                {3,"Marzec" },
                {4,"Kwiecień" },
                {5,"Maj" },
                {6,"Czerwiec" },
                {7,"Lipiec" },
                {8,"Sierpień" },
                {9,"Wrzesień" },
                {10,"Październik" },
                {11,"Listopad" },
                {12,"Grudzień" }
            };
        }

    }

    public delegate void CalendarEventHandler(object sender, CalendarEventHandlerArgs args);

    public class CalendarEventHandlerArgs:EventArgs
    {
        public CalendarTreeItem Item { get; set; }
        
    }
}
