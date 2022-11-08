using System.Windows.Controls;
using System.Windows.Input;
using Desktop.Classes;
using Desktop.MVVM.Model;
using Desktop.MVVM.ViewModel;

namespace Desktop.MVVM.View;

public partial class UserNeedTioLoginView : UserControl
{
    public UserNeedTioLoginView()
    {
        InitializeComponent();
    }

    private void LoginLabel_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        switch (AppSettings.MainViewModel!.CurrentContentView)
        {
            case LoginViewModel s:
                AppSettings.MainViewModel!.CurrentContentView = new LoginViewModel(s.Login, s.Password);
                break;
            case RegistrationViewModel s: 
                AppSettings.MainViewModel!.CurrentContentView = new LoginViewModel(s.Login, s.Password);
                break;
            case RestorePasswordViewModel s: 
                AppSettings.MainViewModel!.CurrentContentView = new RegistrationViewModel(s.Login, s.Password);
                break;
            default:
                AppSettings.MainViewModel!.CurrentContentView = new LoginViewModel("","");
                break;
        }
    }

    private void RegisterLabel_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        switch (AppSettings.MainViewModel!.CurrentContentView)
        {
            case LoginViewModel s:
                AppSettings.MainViewModel!.CurrentContentView = new RegistrationViewModel(s.Login, s.Password);
                break;
            case RegistrationViewModel s: 
                AppSettings.MainViewModel!.CurrentContentView = new RegistrationViewModel(s.Login, s.Password);
                break;
            case RestorePasswordViewModel s: 
                AppSettings.MainViewModel!.CurrentContentView = new RegistrationViewModel(s.Login, s.Password);
                break;
            default:
                AppSettings.MainViewModel!.CurrentContentView = new RegistrationViewModel("","");
                break;
        }
    }
}