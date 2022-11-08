using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Desktop.Classes;
using Desktop.MVVM.Interfaces;
using Shared.Models;
using TrackModel = Desktop.MVVM.Model.TrackModel;

namespace Desktop.MVVM.ViewModel;

public class TracksViewModel : IPlaylistWithTracks
{
    public BindingList<TrackModel> Tracks { get; set; }
    public object? SourceClass { get; set; }
    public string Id { get; set; }
    public string Title { get; set; }
    private bool _isLiked;
    public bool IsLiked
    {
        get => _isLiked;
        set
        {
            _isLiked = value;
            OnPropertyChanged();
        } 
    }
    public bool IsPlaying
    {
        get => AppSettings.ControlPanelViewModel.IsPlaying
               && AppSettings.ControlPanelViewModel.CurrentPlaylist?.Id == Id;
        set
        {
            AppSettings.ControlPanelViewModel.IsPlaying = value;
            OnPropertyChanged();
        }
    }

    public TracksViewModel()
    {
        Id = "TracksX";
        Title = "TracksX";
        Tracks = new BindingList<TrackModel>
        {
            new()
            {
                Title = "track 1", Artist = new PlaylistsAndTracksViewModel("Artist 1", "Artist 1"),
                Album = new TracksViewModel("Album 1", "Album 1")
            },
            new()
            {
                Title = "track 2", Artist = new PlaylistsAndTracksViewModel("Artist 2", "Artist 2"),
                Album = new TracksViewModel("Album 2", "Album 2")
            },
            new()
            {
                Title = "track 3", Artist = new PlaylistsAndTracksViewModel("Artist 3", "Artist 3"),
                Album = new TracksViewModel("Album 3", "Album 3")
            },
        };
    }
    public TracksViewModel(string title, string id)
    {
        Tracks = new BindingList<TrackModel>();
        Id = id;
        Title = title;
    }
    public TracksViewModel(AlbumModel album)
    {
        Tracks = new BindingList<TrackModel>();
        Id = album.Id;
        Title = album.Title;
        SourceClass = album;
        IsLiked = album.IsLiked;
    }
    public TracksViewModel(PlaylistModel playlistServer)
    {
        Tracks = new BindingList<TrackModel>();
        Id = playlistServer.Id;
        Title = playlistServer.Title;
        SourceClass = playlistServer;
    }
    public TrackModel? SelectedTrack
    {
        get => AppSettings.ControlPanelViewModel.CurrentPlaylist?.Id == Id
               && Tracks.SingleOrDefault(x => x.Id == AppSettings.ControlPanelViewModel.CurrentTrack?.Id) != null
            ? AppSettings.ControlPanelViewModel.CurrentTrack
            : null;
        set
        {
            AppSettings.ControlPanelViewModel.CurrentTrack = value;
            OnPropertyChanged();
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string name = null!)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}