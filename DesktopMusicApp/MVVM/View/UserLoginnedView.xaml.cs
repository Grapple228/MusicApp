using System.Windows.Controls;
using System.Windows.Input;
using Desktop.Classes;
using Desktop.MVVM.Model;
using Desktop.MVVM.ViewModel;
using Shared;

namespace Desktop.MVVM.View;

public partial class UserLoginnedView : UserControl
{
    public UserLoginnedView()
    {
        InitializeComponent();
    }

    private void LogoutLabel_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        AppSettings.Connection?.SendMessageAsync(new MessageModel(OperationId.LogoutClient));
        AppSettings.MainViewModel!.CurrentUserView = new UserNeedToLoginViewModel();
        AppSettings.MainViewModel!.CurrentContentView = new HomeViewModel();
        AppSettings.MainViewModel!.ClearPlaylists();
        AppSettings.ControlPanelViewModel.CurrentPlaylist = null;
        AppSettings.ControlPanelViewModel.CurrentTrack = null;
        AppSettings.ControlPanelViewModel.IsPlaying = false;
    }
}