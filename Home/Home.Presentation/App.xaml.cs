
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


            var mainWindow = new Views.MainWindowView();
            mainWindow.DataContext = new MainWindow();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
