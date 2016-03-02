using System.Windows;
using Home.Budget;

namespace Home.Presentation.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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
