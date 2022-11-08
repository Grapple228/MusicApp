using System;
using System.Windows;
using System.Windows.Controls;
using Desktop.MVVM.View;
using Shared;

namespace Desktop.MVVM.ViewModel;

public class LoginViewModel : ObservableObject
{
    public LoginView View;
    
    private dynamic _selectedTextBox;
    public dynamic SelectedTextBox
    {
        get => _selectedTextBox;
        set
        {
            _selectedTextBox = value;
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                _selectedTextBox.Focus();
                _selectedTextBox.SelectAll();
            });
        }
    }
    
    public string Login { get; set; }
    
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
    private string _errorLabelText;

    public string ErrorLabelText
    {
        get => _errorLabelText;
        set
        {
            _errorLabelText = value;
            OnPropertyChanged();
        }
    }

    public LoginViewModel(string login, string password)
    {
        Login = login;
        Password = password;
        _errorLabelText = "";
    }
}