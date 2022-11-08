using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Desktop.Classes;
using Desktop.MVVM.Interfaces;
using Desktop.MVVM.Model;
using Desktop.MVVM.ViewModel;

namespace Desktop.MVVM.Controls;

public partial class ControlPanel : UserControl
{
    public ControlPanel()
    {
        InitializeComponent();
    }

    private void PlayTrackButton_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        ControlPanelViewModel dataContext = (ControlPanelViewModel)this.DataContext;
        dataContext.IsPlaying = !dataContext.IsPlaying;
        IPlaylist? currentPlaylist = AppSettings.ControlPanelViewModel.CurrentPlaylist;
        TrackModel? currentTrack = AppSettings.ControlPanelViewModel.CurrentTrack;
        if(currentPlaylist != null)
            currentPlaylist.IsPlaying = dataContext.IsPlaying;
        if(currentTrack != null)
            currentTrack.IsPlaying = dataContext.IsPlaying;
    }

    private void TrackLiked_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        ControlPanelViewModel dataContext = (ControlPanelViewModel)this.DataContext;
        dataContext.CurrentTrack!.IsLiked = !dataContext.CurrentTrack.IsLiked;
    }

    private void ShuffleTrackButton_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        ControlPanelViewModel dataContext = (ControlPanelViewModel)this.DataContext;
        dataContext.IsShuffle = !dataContext.IsShuffle;
    }

    private void PreviousTrack_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        ControlPanelViewModel dataContext = (ControlPanelViewModel)this.DataContext;
        var currentTrack = dataContext.CurrentTrack;
        var currentPlaylist = dataContext.CurrentPlaylist;
        int index = currentPlaylist!.Tracks.IndexOf(currentTrack!);
        if(index - 1 < 0)
            return;
        AppSettings.ControlPanelViewModel.SetTrack(AppSettings.MainViewModel!.CurrentContentView,
            currentPlaylist.Tracks[index - 1]);
    }

    private void NextTrack_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        ControlPanelViewModel dataContext = (ControlPanelViewModel)this.DataContext;
        var currentTrack = dataContext.CurrentTrack;
        var currentPlaylist = dataContext.CurrentPlaylist;
        int index = currentPlaylist!.Tracks.IndexOf(currentTrack!);
        if(index + 1 == currentPlaylist.Tracks.Count)
            return;
        AppSettings.ControlPanelViewModel.SetTrack(AppSettings.MainViewModel!.CurrentContentView,
            currentPlaylist.Tracks[index + 1]);
    }

    private void TrackTitleLabel_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if(sender is not Label label) return;
        if(label.DataContext is not ControlPanelViewModel controlPanelViewModel) return;
        var trackModel = controlPanelViewModel.CurrentTrack;
        if(trackModel == null) return;
        AppSettings.MainViewModel!.CurrentContentView = trackModel.Album!;
    }
    private void TrackArtistLabel_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if(sender is not Label label) return;
        if(label.DataContext is not ControlPanelViewModel controlPanelViewModel) return;
        var trackModel = controlPanelViewModel.CurrentTrack;
        if(trackModel == null) return;
        AppSettings.MainViewModel!.CurrentContentView = trackModel.Artist!;
    }
    private void RepeatTrackButton_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        ControlPanelViewModel dataContext = (ControlPanelViewModel)this.DataContext;
        dataContext.RepeatType = dataContext.RepeatType switch
        {
            RepeatType.None => RepeatType.RepeatPlaylist,
            RepeatType.RepeatPlaylist => RepeatType.RepeatTrack,
            RepeatType.RepeatTrack => RepeatType.None,
            _ => RepeatType.None
        };
    }
    private void RepeatTrackButton_OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
        ControlPanelViewModel dataContext = (ControlPanelViewModel)this.DataContext;
        dataContext.RepeatType = dataContext.RepeatType switch
        {
            RepeatType.None => RepeatType.RepeatTrack,
            RepeatType.RepeatTrack => RepeatType.RepeatPlaylist,
            RepeatType.RepeatPlaylist => RepeatType.None,
            _ => RepeatType.None
        };
    }
}