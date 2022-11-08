using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Desktop.Classes;
using Desktop.MVVM.Model;
using Desktop.MVVM.ViewModel;

namespace Desktop.MVVM.View;

public partial class RegistrationView : UserControl
{
    public RegistrationView()
    {
        InitializeComponent();
    }

    private void GoToLogInUpPage_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        AppSettings.MainViewModel!.CurrentContentView = new LoginViewModel(LoginBox.Text, PasswordBox.Password);
    }

    private void LoginBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        ErrorLabel.Content = "";
    }

    private void PasswordBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        ErrorLabel.Content = "";
    }

    private void EmailBox_OnTextChanged_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        ErrorLabel.Content = "";
    }

    private void RegisterButton_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        CheckData();
    }

    private void LoginBox_OnKeyDown(object sender, KeyEventArgs e)
    {
        if(e.Key == Key.Enter)
            CheckData();
    }

    private void EmailBox_OnKeyDownBox_OnKeyDown(object sender, KeyEventArgs e)
    {
        if(e.Key == Key.Enter)
            CheckData();
    }

    private void PasswordBox_OnKeyDownBox_OnKeyDown(object sender, KeyEventArgs e)
    {
        if(e.Key == Key.Enter)
            CheckData();
    }
    
    private void CheckData()
    {
        ErrorLabel.Content = "";
        RegistrationViewModel model = (RegistrationViewModel)this.DataContext;

        if (LoginBox.Text.Length == 0)
        {
            ErrorLabel.Content = "Логин пустой!";
            model.SelectedTextBox = LoginBox;
            return;
        }
        if (PasswordBox.Password.Length == 0)
        {
            ErrorLabel.Content = "Пароль пустой!";
            model.SelectedTextBox = PasswordBox;
            return;
        }
        if (EmailBox.Text.Length == 0)
        {
            ErrorLabel.Content = "E-mail пустой!";
            model.SelectedTextBox = EmailBox;
            return;
        }
        
        if (LoginBox.Text.Contains(" "))
        {
            ErrorLabel.Content = "Логин не должен содержать пробелов!";
            model.SelectedTextBox = LoginBox;
            return;
        }

        if (PasswordBox.Password.Length < 6)
        {
            ErrorLabel.Content = "Минимальная длина пароля - 6 символов!";
            model.SelectedTextBox = PasswordBox;
            return;
        }
        
        // Выполнение запроса на сервер
        AppSettings.Connection?.SendMessageAsync(new RegisterMessage(
            LoginBox.Text, 
            PasswordBox.Password,
            EmailBox.Text));
    }

    private void RegistrationView_OnLoaded(object sender, RoutedEventArgs e)
    {
        RegistrationViewModel model = (RegistrationViewModel)this.DataContext;
        model.View = this;
        PasswordBox.Password = model.Password;
        if (model.Login.Length == 0) model.SelectedTextBox = LoginBox;
        else model.SelectedTextBox = EmailBox;
    }

    private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        RegistrationViewModel model = (RegistrationViewModel)this.DataContext;
        if(sender is not PasswordBox passwordBox) return;
        model.Password = passwordBox.Password;
    }
}