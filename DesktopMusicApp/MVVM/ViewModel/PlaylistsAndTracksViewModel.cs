using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Desktop.Classes;
using Desktop.MVVM.Interfaces;
using Desktop.Tools;
using DesktopMusicApp;
using Shared.Models;
using TrackModel = Desktop.MVVM.Model.TrackModel;

namespace Desktop.MVVM.ViewModel;

public class PlaylistsAndTracksViewModel : IPlaylistWithPlaylistsAndTracks
{
    public BindingList<IPlaylist> Playlists { get; set; }
    public BindingList<TrackModel> Tracks { get; set; }

    public PlaylistsAndTracksViewModel(string title, string id)
    {
        Playlists = new BindingList<IPlaylist>();
        Tracks = new BindingList<TrackModel>();
        Id = id;
        Title = title;
    }

    public PlaylistsAndTracksViewModel(ArtistModel artist)
    {
        Playlists = new BindingList<IPlaylist>();
        Tracks = new BindingList<TrackModel>();
        Id = artist.Id;
        Title = artist.Name;
        SourceClass = artist;
    }

    public PlaylistsAndTracksViewModel()
    {
        Id = "PlaylistsAndTracksX";
        Title = "PlaylistsAndTracksX";
        Playlists = new()
        {
            new TracksViewModel("Playlist1", "Playlist1"),
            new TracksViewModel("Playlist2", "Playlist2"),
            new TracksViewModel("Playlist3", "Playlist3"),
            new TracksViewModel("Playlist4", "Playlist4"),
            new TracksViewModel("Playlist5", "Playlist5"),
            new TracksViewModel("Playlist6", "Playlist6"),
            new TracksViewModel("Playlist7", "Playlist7"),
        };
        Tracks = new()
        {
            new TrackModel
            {
                Title = "track 1", Artist = new PlaylistsAndTracksViewModel("Artist 1", "Artist 1"),
                Album = new TracksViewModel("Album 1", "Album 1")
            },
            new TrackModel
            {
                Title = "track 2", Artist = new PlaylistsAndTracksViewModel("Artist 2", "Artist 2"),
                Album = new TracksViewModel("Album 2", "Album 2")
            },
            new TrackModel
            {
                Title = "track 3", Artist = new PlaylistsAndTracksViewModel("Artist 3", "Artist 3"),
                Album = new TracksViewModel("Album 3", "Album 3")
            },
        };
    }

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

    public IPlaylistWithTracks? SelectedPlaylist
    {
        get => Playlists.SingleOrDefault(x => x.Id == AppSettings.ControlPanelViewModel.CurrentPlaylist?.Id) != null
            ? AppSettings.ControlPanelViewModel.CurrentPlaylist
            : null;
        set
        {
            AppSettings.ControlPanelViewModel.CurrentPlaylist = value;
            OnPropertyChanged();
        }
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