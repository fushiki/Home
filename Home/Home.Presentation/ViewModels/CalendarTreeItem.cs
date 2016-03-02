using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home.Presentation.ViewModels
{
    public class CalendarTreeItem:BaseViewModel
    {
        public ObservableCollection<CalendarTreeItem> Items { get; } 
            = new ObservableCollection<CalendarTreeItem>();

        private bool _isSelected;

        public string Date { get; }

        public bool IsSelected
        {
            get { return _isSelected;}
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public CalendarTreeItem(string datename)
        {
            Date = datename;
        }

    }
}
