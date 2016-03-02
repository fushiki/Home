using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Home.Budget;
using Home.Presentation.ButtonGridControl;
using Home.Presentation.Tree;
using Microsoft.Expression.Interactivity.Core;

namespace Home.Presentation.ViewModels
{
    public class AddShopping : BaseViewModel
    {
        private MainWindow _mainWindow;
        private double _price;
        private double _quantity;
        public  ButtonGrid ButtonGrid { get; }
        public string Title => "Add Shopping"; //TODO Localizable

        public double Quntity
        {
            get { return _quantity; }
            set
            {
                if (value - _quantity < 0.000001) return;
                _quantity = value;
                OnPropertyChanged(nameof(Quntity));
            }
        }

        public double Price
        {
            get { return _price; }
            set
            {
                if (value - _price < 0.000001) return;
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public AddShopping(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            ButtonGrid = new ShowProductsGrid(2.0,BudgetTopic.Get().Database.AllCategory, new ActionCommand(x=>MessageBox.Show("Selected")));
        }
        

        
    }
}
