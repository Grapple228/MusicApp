using System;
using System.Windows;
using System.Windows.Controls;
using Desktop.Classes;
using Desktop.MVVM.View;
using Shared;

namespace Desktop.MVVM.ViewModel;

public class RegistrationViewModel : ObservableObject
{
    public RegistrationView View;
    
    private dynamic _selectedTextBox;
    public dynamic SelectedTextBox
    {
        get => _selectedTextBox;
        set
        {
            _selectedTextBox = value;
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                PasswordBox p = new PasswordBox();
                _selectedTextBox.Focus();
                _selectedTextBox.SelectAll();
            });
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

    public RegistrationViewModel(string login, string password)
    {
        _login = login;
        _password = password;
        _errorLabelText = "";
    }
}