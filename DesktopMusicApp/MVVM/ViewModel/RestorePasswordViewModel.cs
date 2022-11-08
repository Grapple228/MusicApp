using Desktop.Classes;
using Shared;

namespace Desktop.MVVM.ViewModel;

public class RestorePasswordViewModel : ObservableObject
{
    public RestorePasswordViewModel(string login, string password)
    {
        _login = login;
        _password = password;
        _repeatPassword = "";
    }

    private string _login;
    public string Login
    {
        get => _login;
        set
        {
            _login = value;
            OnPropertyChanged();
        }
    }
    
    private string _password;
    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged();
        }
    }
    
    private string _repeatPassword;
    public string RepeatPassword
    {
        get => _repeatPassword;
        set
        {
            _repeatPassword = value;
            OnPropertyChanged();
        }
    }
}