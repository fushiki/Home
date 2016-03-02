using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Home.Budget;
using Home.Presentation.Views;
using Microsoft.Expression.Interactivity.Core;

namespace Home.Presentation.ViewModels
{
    public class AddCategoryViewModel:IWindowViewModel
    {
        public Window Window { get; }

        public string Name
        {
            get { return Category.Name; }
            set { Category.Name = value; }
        }
        public ProductCategory Category { get;}

        public ICommand CommandOK { get; }
        public AddCategoryViewModel()
        {
            Window = new AddCategoryWindow() { DataContext = this };
            Category = new ProductCategory();
            CommandOK = new ActionCommand((x) => Window.DialogResult = true);
        }

    }
}
