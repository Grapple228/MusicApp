using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Desktop.Classes;
using Desktop.MVVM.Model;
using Desktop.MVVM.ViewModel;

namespace Desktop.MVVM.View;

public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent();
    }

    private void GoToSignUpPage_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        AppSettings.MainViewModel!.CurrentContentView = new RegistrationViewModel(LoginBox.Text, PasswordBox.Password);
    }

    private void LoginButton_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        CheckData();
    }

    private void CheckData()
    {
        ErrorLabel.Content = "";
        LoginViewModel model = (LoginViewModel)this.DataContext;

        if (LoginBox.Text.Length == 0)
        {
            ErrorLabel.Content = "Логин пустой!";
            model.SelectedTextBox = LoginBox;
            return;
        }
        if (model.Password.Length == 0)
        {
            ErrorLabel.Content = "Пароль пустой!";
            model.SelectedTextBox = PasswordBox;
            return;
        }
        
        if (LoginBox.Text.Contains(" "))
        {
            ErrorLabel.Content = "Логин не должен содержать пробелов!";
            model.SelectedTextBox = LoginBox;
            return;
        }
        
        // Выполнение запроса на сервер
        AppSettings.Connection?.SendMessageAsync(new LoginMessage(
            LoginBox.Text, PasswordBox.Password));
    }
    
    private void LoginBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        ErrorLabel.Content = "";
    }   

    private void PasswordBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        ErrorLabel.Content = "";
    }

    private void LoginBox_OnKeyDown(object sender, KeyEventArgs e)
    {
        if(e.Key == Key.Enter)
            CheckData();
    }

    private void PasswordBox_OnKeyDown(object sender, KeyEventArgs e)
    {
        if(e.Key == Key.Enter)
            CheckData();
    }

    private void ForgotPasswordLabel_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        AppSettings.MainViewModel!.CurrentContentView = 
            new RestorePasswordViewModel(LoginBox.Text, PasswordBox.Password);
    }

    private void LoginView_OnLoaded(object sender, RoutedEventArgs e)
    {
        LoginViewModel model = (LoginViewModel)this.DataContext;
        model.View = this;
        PasswordBox.Password = model.Password;
        if (model.Login.Length == 0) model.SelectedTextBox = LoginBox;
        else model.SelectedTextBox = PasswordBox;
    }

    private void PassBox_OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        LoginViewModel model = (LoginViewModel)this.DataContext;
        if(sender is not PasswordBox passwordBox) return;
        model.Password = passwordBox.Password;
    }

    private void PassBox_OnKeyDown(object sender, KeyEventArgs e)
    {
        if(e.Key == Key.Enter)
            CheckData();
    }
}