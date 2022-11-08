using System.ComponentModel;
using Desktop.Classes;
using Desktop.MVVM.Model;
using Shared;

namespace Desktop.MVVM.Interfaces;

public interface IPlaylist : IObservableObject
{
    public object? SourceClass { get; set; }
    public string Id { get; set; }
    public string Title { get; set; }
    public bool IsLiked { get; set; }
    public bool IsPlaying { get; set; }
}

public interface IPlaylistWithPlaylists : IPlaylist
{
    public BindingList<IPlaylist> Playlists { get; set; }
    public IPlaylistWithTracks? SelectedPlaylist { get; set; }
}

public interface IPlaylistWithTracks : IPlaylist
{
    public BindingList<TrackModel> Tracks { get; set; }
    public TrackModel? SelectedTrack { get; set; }
}

public interface IPlaylistWithPlaylistsAndTracks : IPlaylistWithPlaylists, IPlaylistWithTracks
{
    
}