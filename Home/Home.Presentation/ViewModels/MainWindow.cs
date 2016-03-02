using System.Windows.Input;
using Home.Presentation.ButtonGridControl;

namespace Home.Presentation.ViewModels
{
   
    public class MainWindow:BaseViewModel
    {
        

        public AddShopping AddShopping { get; }
        public ViewShoppingHistory CheckShoppingViewModel { get; }
        public ICommand CommandChangeView { get; }
        public BaseViewModel CurrentView { get; private set; }


        public MainWindow()
        {
            
            AddShopping = new AddShopping(this);
            CheckShoppingViewModel = new ViewShoppingHistory(this);
            CurrentView = CheckShoppingViewModel;
           
            
            CommandChangeView = new LambdaCommand(
                x =>
                {
                    CurrentView = x as BaseViewModel;
                    OnPropertyChanged(nameof(CurrentView));
                });
            
        }
    }
}
