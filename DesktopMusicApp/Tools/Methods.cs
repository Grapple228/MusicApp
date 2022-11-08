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
}