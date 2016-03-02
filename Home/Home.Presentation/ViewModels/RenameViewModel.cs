using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Home.Presentation.Views;
using Microsoft.Expression.Interactivity.Core;

namespace Home.Presentation.ViewModels
{
    public class RenameViewModel:BaseViewModel
    {
        private string _name;

        public Window Window { get; }
        public ICommand CommandOK { get; }
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public RenameViewModel()
        {
            Window = new RenameView() {DataContext = this};
            CommandOK = new ActionCommand((x) => Window.DialogResult = true);

        }
    }
}
