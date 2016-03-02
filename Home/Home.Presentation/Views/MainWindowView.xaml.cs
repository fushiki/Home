using System.Windows;
using Home.Budget;

namespace Home.Presentation.Views
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();
            
        }


        //TODO REMOVE
        private void clickTestrqweq(object sender, RoutedEventArgs e)
        {
            
            BudgetTopic.Get().Database.Save();
        }
    }
}
