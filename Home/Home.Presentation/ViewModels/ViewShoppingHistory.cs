using System;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Home.Presentation.ViewModels
{
    public class ViewShoppingHistory : BaseViewModel,IBudgetDynamicViewModel
    {
        MainWindowViewModel _parent;
        public ObservableCollection<ShoppingViewModel> ListItems { get; private set; }

        
        private bool _isCalendar;

        public BaseViewModel CurrentView => _isCalendar ? CalendarViewModel : (BaseViewModel)TreeCalendar;

        public bool IsTreeEnabled
        {
            get { return !_isCalendar; }
            set
            {
                _isCalendar = !value;
                OnPropertyChanged(nameof(IsCalendarEnabled));
                OnPropertyChanged(nameof(IsTreeEnabled));
                OnPropertyChanged(nameof(CurrentView));
            }
        }
        public bool IsCalendarEnabled
        {
            get { return _isCalendar;}
            set
            {
                _isCalendar = value;
                OnPropertyChanged(nameof(IsCalendarEnabled));
                OnPropertyChanged(nameof(IsTreeEnabled));
                OnPropertyChanged(nameof(CurrentView));
            }
        }
        public TreeCalendar TreeCalendar { get; }
        public CalendarViewModel CalendarViewModel { get; }
        public ViewShoppingHistory(MainWindowViewModel parent)
        {
            _parent = parent;
            ListItems = new ObservableCollection<ShoppingViewModel>( );
            TreeCalendar = new TreeCalendar();
            TreeCalendar.SelectedChanged += SelectedDayChanged;
            CalendarViewModel = new CalendarViewModel();
            

        }

        private void SelectedDayChanged(object sender, CalendarEventHandlerArgs args)
        {
            if (!args.Item?.IsSelected ?? true)
                return;
            var leaf = args.Item as CalendarTreeLeaf;
            if (leaf != null)
            {
                ListItems = leaf.Shoppings;
                OnPropertyChanged(nameof(ListItems));
            }
        }
    }
}