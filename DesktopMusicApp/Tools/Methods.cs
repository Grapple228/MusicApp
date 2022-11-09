using System.Linq;
using System.Windows.Forms;
using Desktop.Classes;
using Desktop.MVVM.Interfaces;
using Desktop.MVVM.ViewModel;

namespace Desktop.Tools;

public static class Methods
{
    public static IPlaylistWithTracks? GetPlaylistWithTracks(IPlaylist playlistToProcess)
    {
        IPlaylistWithTracks? playlistWithTracks = null;
        switch (playlistToProcess)
        {
            case IPlaylistWithPlaylists playlist:
                if (playlist.Playlists.Count == 0)
                    return playlistWithTracks;
                playlistWithTracks = GetPlaylistWithTracks(playlist.Playlists[0]);
                break;
            case IPlaylistWithTracks playlist:
                playlistWithTracks = playlist;
                break;
        }
        return playlistWithTracks;
    }

    public static void ProcessPlaylistIsLiked(IPlaylist playlist)
    {
        if (AppSettings.MainViewModel == null
            || AppSettings.MainViewModel.Playlists.SingleOrDefault(x => x.Id == "Liked") is not IPlaylistWithPlaylists
                likedPlaylist) return;
        
        if (playlist.IsLiked)
        {
            if(likedPlaylist.Playlists.SingleOrDefault(x => x.Id == playlist.Id) == null)
                likedPlaylist.Playlists.Add(playlist);
        }
        else
        {
            if (likedPlaylist.Playlists.SingleOrDefault(x => x.Id == playlist.Id) == null)
                return;
            if (AppSettings.MainViewModel.CurrentContentView != likedPlaylist
                || AppSettings.ControlPanelViewModel.CurrentPlaylist?.Id != playlist.Id)
            {
                likedPlaylist.Playlists.Remove(playlist);
                return;
            }
            likedPlaylist.Playlists.Remove(playlist);
            if(playlist is IPlaylistWithTracks tracks)
                AppSettings.ControlPanelViewModel.CurrentPlaylist = tracks;
        }
    }
}