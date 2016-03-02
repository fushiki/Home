
using System.Windows;
using Home.Budget;
using Home.Presentation.ViewModels;
using Home.Core;
namespace Home.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Source.Get().AddTopic(new BudgetTopic());


            var mainWindow = new Views.MainWindow();
            mainWindow.DataContext = new MainWindowViewModel();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
