using System.Windows.Controls;
using System.Windows.Input;
using Desktop.Classes;
using Desktop.MVVM.Interfaces;
using Desktop.MVVM.ViewModel;
using Desktop.Tools;

namespace Desktop.MVVM.Controls;

public partial class PlaylistList : UserControl
{
    public PlaylistList()
    {
        InitializeComponent();
    }
    private void PlayButton_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var border = sender as Border;
        
        var oldTrack = AppSettings.ControlPanelViewModel.CurrentTrack;
        var oldPlaylist = AppSettings.ControlPanelViewModel.CurrentPlaylist;
        AppSettings.ControlPanelViewModel.CurrentPlaylist = border?.DataContext switch
        {
            IPlaylistWithPlaylistsAndTracks playlist => playlist,
            IPlaylistWithTracks playlist => playlist,
            IPlaylistWithPlaylists playlist => Methods.GetPlaylistWithTracks(playlist),
            _ => AppSettings.ControlPanelViewModel.CurrentPlaylist
        };
        var currentPlaylist = AppSettings.ControlPanelViewModel.CurrentPlaylist!;

        if (oldPlaylist?.Id == currentPlaylist.Id)
        {
            AppSettings.ControlPanelViewModel.SetPlaylistAndTrack(
                currentPlaylist, 
                oldTrack!,
                !currentPlaylist.IsPlaying);
        }
        else
        {
            if(oldTrack != null)
                oldTrack.IsPlaying = false;
            if(oldPlaylist != null)
                oldPlaylist.IsPlaying = false;
            var currentTrack = 
                currentPlaylist.Tracks.Count != 0 
                ? currentPlaylist.Tracks[0] 
                : null;
            AppSettings.ControlPanelViewModel.SetPlaylistAndTrack(
                currentPlaylist, 
                currentTrack!);
        }
    }

    private void TitleLabel_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Label label = (sender as Label)!;
        AppSettings.MainViewModel!.SelectedPlaylist = (IPlaylist)label.DataContext;
    }

    private void LikeButton_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var border = sender as Border;
        if(border!.DataContext is not IPlaylist playlistModel) return;
        playlistModel.IsLiked = !playlistModel.IsLiked;
    }

    private void ItemBorder_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Border label = (sender as Border)!;
        AppSettings.MainViewModel!.SelectedPlaylist = (IPlaylist)label.DataContext;
    }
}