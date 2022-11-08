using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Desktop.Classes;
using Desktop.MVVM.Interfaces;
using Desktop.Tools;

namespace Desktop.MVVM.ViewModel;

public class PlaylistsViewModel : IPlaylistWithPlaylists
{
    public BindingList<IPlaylist> Playlists { get; set; }

    public PlaylistsViewModel()
    {
        Id = "PlaylistX";
        Title = "PlaylistX";
        Playlists = new BindingList<IPlaylist>
        {
            new TracksViewModel("Playlist1","Playlist1"),
            new TracksViewModel("Playlist2","Playlist2"),
            new TracksViewModel("Playlist3","Playlist3"),
            new TracksViewModel("Playlist4","Playlist4"),
            new TracksViewModel("Playlist5","Playlist5"),
            new TracksViewModel("Playlist6","Playlist6"),
            new TracksViewModel("Playlist7","Playlist7"),
        };
    }    
    
    public PlaylistsViewModel(string title, string id)
    {
        Playlists = new BindingList<IPlaylist>();
        Id = id;
        Title = title;
        IsLiked = false;
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
        get => AppSettings.ControlPanelViewModel.CurrentPlaylist;
        set
        {
            AppSettings.ControlPanelViewModel.CurrentPlaylist = value;
            OnPropertyChanged();
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string name = null!)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}