using System.Windows.Controls;
using System.Windows.Input;
using Desktop.Classes;
using Desktop.MVVM.Interfaces;
using Desktop.MVVM.Model;
using Desktop.MVVM.ViewModel;

namespace Desktop.MVVM.Controls;

public partial class TrackList : UserControl
{
    public TrackList()
    {
        InitializeComponent();
    }

    private void AlbumTextBlock_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        TextBlock textBlock = (sender as TextBlock)!;
        TrackModel track = (TrackModel)textBlock.DataContext;
        AppSettings.MainViewModel!.SelectedPlaylist = track.Album!;
    }

    
    private void ArtistTextBlock_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        TextBlock textBlock = (sender as TextBlock)!;
        TrackModel track = (TrackModel)textBlock.DataContext;
        AppSettings.MainViewModel!.SelectedPlaylist = track.Artist!;
    }

    private void PlayButton_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        var border = sender as Border;
        if(border!.DataContext is not TrackModel currentTrack) return;
        SetTrackInfo(currentTrack);
    }

    private void SetTrackInfo(TrackModel currentTrack)
    {
        var oldTrack = AppSettings.ControlPanelViewModel.CurrentTrack;
        var oldPlaylist = AppSettings.ControlPanelViewModel.CurrentPlaylist;
        var currentPlaylist = (IPlaylistWithTracks)DataContext;
        
        if (oldTrack?.Id == currentTrack.Id && oldPlaylist?.Id == currentPlaylist.Id)
        {
            currentTrack.IsPlaying = !currentTrack.IsPlaying;
        }
        else
        {
            if(oldTrack != null)
                oldTrack.IsPlaying = false;
            if (oldPlaylist != null)
                oldPlaylist.IsPlaying = false;

            AppSettings.ControlPanelViewModel.SetPlaylistAndTrack(
                currentPlaylist, 
                currentTrack!);
        }
    }
}