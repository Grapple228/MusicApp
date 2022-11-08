using System.Windows;
using Desktop.Classes;
using Desktop.MVVM.View;
using Desktop.MVVM.ViewModel;

namespace DesktopMusicApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AppSettings.App = this;
            MainViewModel mainViewModel = new MainViewModel();
            AppSettings.MainViewModel = mainViewModel;


            AppSettings.MainWindow = new MainWindow()
            {
                DataContext = mainViewModel,
            };

            AppSettings.MainViewModel!.CurrentContentView = new HomeViewModel();
            AppSettings.MainViewModel!.CurrentUserView = new UserNeedToLoginViewModel();

            AppSettings.MainWindow.Show();
        }
    }
}
