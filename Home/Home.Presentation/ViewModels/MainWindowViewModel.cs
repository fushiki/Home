using System.Windows.Input;
using Home.Presentation.ButtonGridControl;

namespace Home.Presentation.ViewModels
{
   
    public class MainWindowViewModel:BaseViewModel
    {
        

        public AddShoppingViewModel AddShoppingViewModel { get; }
        public ViewShoppingHistory CheckShoppingViewModel { get; }
        public ICommand CommandChangeView { get; }
        public BaseViewModel CurrentView { get; private set; }

        public ButtonGrid ButtonsGrid { get; }

        public MainWindowViewModel()
        {
            ButtonsGrid = new ShowProductsGrid();
            //AddShoppingViewModel = new AddShoppingViewModel(this);
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
