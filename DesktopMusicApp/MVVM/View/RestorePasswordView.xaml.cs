using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Desktop.Classes;
using Desktop.MVVM.ViewModel;

namespace Desktop.MVVM.View;

public partial class RestorePasswordView : UserControl
{
    public State CurrentState;

    public enum State
    {
        EnterLogin,
        EnterCode,
        EnterPassword
    }
    public RestorePasswordView()
    {
        InitializeComponent();
        ChangeState(State.EnterLogin);
    }

    private void GoToSignInPage_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        AppSettings.MainViewModel!.CurrentContentView = new LoginViewModel(LoginBox.Text, NewPassword.Text);
    }

    private void ChangeState(State state)
    {
        CurrentState = state;
        switch (CurrentState)
        {
            case State.EnterLogin:
                SetEnterLogin();
                break;
            case State.EnterCode:
                SetEnterCode();
                break;
            case State.EnterPassword:
                SetEnterPassword();
                break;
        }

        void SetEnterLogin()
        {
            PageLabel.Text = "Enter login";
            LoginBorder.IsEnabled = true;
            ConfirmationBorder.Visibility = Visibility.Collapsed;
            NewPasswordBorder.Visibility = Visibility.Collapsed;
            RepeatPasswordBorder.Visibility = Visibility.Collapsed;
            ButtonContent.Content = "Send";
        }
        void SetEnterCode()
        {
            PageLabel.Text = "Confirm operation";
            LoginBorder.IsEnabled = false;
            ConfirmationBorder.Visibility = Visibility.Visible;
            NewPasswordBorder.Visibility = Visibility.Collapsed;
            RepeatPasswordBorder.Visibility = Visibility.Collapsed;
            ButtonContent.Content = "Confirm";
        }
        void SetEnterPassword()
        {
            PageLabel.Text = "Enter password";
            LoginBorder.IsEnabled = false;
            ConfirmationBorder.Visibility = Visibility.Collapsed;
            NewPasswordBorder.Visibility = Visibility.Visible;
            RepeatPasswordBorder.Visibility = Visibility.Visible;
            ButtonContent.Content = "Change";
        }
    }

    public void ProcessState()
    {
        switch (CurrentState)
        {
            case State.EnterLogin:
                EnterLogin();
                break;
            case State.EnterCode:
                EnterCode();
                break;
            case State.EnterPassword:
                EnterPassword();
                break;
        }

        void EnterLogin()
        {
            ChangeState(State.EnterCode);
        }
        void EnterCode()
        {
            ChangeState(State.EnterPassword);
        }
        void EnterPassword()
        {
            ChangeState(State.EnterLogin);
        }
    }
    
    private void ChangeButton_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        ProcessState();
    }
}