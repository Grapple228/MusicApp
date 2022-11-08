using System.Windows;
using Desktop.Classes;
using Desktop.MVVM.Interfaces;
using Desktop.MVVM.Model;
using Shared;

namespace Desktop.MVVM.ViewModel;

public class ControlPanelViewModel : ObservableObject
{
    public ControlPanelViewModel()
    {
        RepeatType = RepeatType.None;
    }
    private TrackModel? _currentTrack;
    public TrackModel? CurrentTrack
    {
        get => _currentTrack;
        set
        {
            _currentTrack = value;
            if (AppSettings.MainViewModel != null) 
                AppSettings.MainViewModel.CurrentTrack = _currentTrack;
            if (AppSettings.MainWindow != null)
            {
                if (AppSettings.ControlPanelViewModel.CurrentTrack == null)
                {
                    AppSettings.MainWindow!.ControlBar.Visibility = Visibility.Collapsed;
                    AppSettings.MainWindow!.VolumeButton.Visibility = Visibility.Collapsed;
                }
                else
                {
                    AppSettings.MainWindow!.ControlBar.Visibility = Visibility.Visible;
                    AppSettings.MainWindow!.VolumeButton.Visibility = Visibility.Visible;
                }
            }
            OnPropertyChanged();
        }
    }
    private IPlaylistWithTracks? _currentPlaylist;

    public IPlaylistWithTracks? CurrentPlaylist
    {
        get => _currentPlaylist;
        set
        {
            _currentPlaylist = value;
            if (AppSettings.MainViewModel != null)
                AppSettings.MainViewModel.CurrentPlaylist = _currentPlaylist;
            OnPropertyChanged();
        }
    }

    private bool _isPlaying;
    public bool IsPlaying
    {
        get => _isPlaying;
        set
        {
            _isPlaying = value;
            if(AppSettings.MainViewModel != null)
                AppSettings.MainViewModel.IsPlaying = _isPlaying;
            OnPropertyChanged();
        }
    }
    
    private bool _isShuffle;
    public bool IsShuffle
    {
        get => _isShuffle;
        set
        {
            _isShuffle = value;
            OnPropertyChanged();
        }
    }
    
    private RepeatType _repeatType;
    public RepeatType RepeatType
    {
        get => _repeatType;
        set
        {
            _repeatType = value;
            OnPropertyChanged();
        }
    }
    private bool _isMuted;
    public bool IsMuted
    {
        get => _isMuted;
        set
        {
            _isMuted = value;
            OnPropertyChanged();
        }
    }
    
    public float Volume { get; set; }

    public void SetTrack(object dataContext,TrackModel trackModel, bool isPlaying = true)
    {
        if (dataContext is IPlaylistWithTracks playlistWithTracks)
        {
            playlistWithTracks.SelectedTrack = trackModel;
        }
        CurrentTrack = trackModel;
        trackModel.IsPlaying = isPlaying;
    }

    public void SetPlaylistAndTrack(object dataContext,IPlaylistWithTracks playlistModel,TrackModel trackModel, bool isPlaying = true)
    {
        if (dataContext is IPlaylistWithPlaylists playlistWithPlaylists)
        {
            playlistWithPlaylists.SelectedPlaylist = playlistModel;
        }
        CurrentPlaylist = playlistModel;
        SetTrack(dataContext, trackModel, isPlaying);
        playlistModel.IsPlaying = isPlaying;
    }
}