using System.Linq;
using System.Windows.Forms;
using Desktop.Classes;
using Desktop.MVVM.Interfaces;
using Desktop.MVVM.ViewModel;
using Desktop.Tools;

namespace Desktop.MVVM.Model;

public class TrackModel : Shared.Models.TrackModel
{
    public TracksViewModel? Album { get; set; }
    public PlaylistsAndTracksViewModel? Artist { get; set; }

    public bool IsPlaying
    {
        get => AppSettings.ControlPanelViewModel.CurrentTrack?.Id == Id
               && AppSettings.ControlPanelViewModel.IsPlaying
               && AppSettings.MainViewModel?.CurrentContentView is IPlaylistWithTracks playlistWithTracks
               && AppSettings.ControlPanelViewModel.CurrentPlaylist?.Id == playlistWithTracks.Id;
        set
        {
            AppSettings.ControlPanelViewModel.IsPlaying = value;
            OnPropertyChanged();
        }
    }
    public new bool IsLiked
    {
        get => _isLiked;
        set
        {
            _isLiked = value;
            if (AppSettings.MainViewModel != null 
                && AppSettings.MainViewModel.Playlists.SingleOrDefault(x => x.Id == "Liked") is IPlaylistWithTracks
                playlist)
            {
                if (_isLiked)
                {
                    if(playlist.Tracks.SingleOrDefault(x => x.Id == Id) == null)
                        playlist.Tracks.Add(this);
                }
                else
                {
                    if (playlist.Tracks.SingleOrDefault(x => x.Id == Id) != null)
                    {
                        if (AppSettings.ControlPanelViewModel.CurrentPlaylist == playlist
                            && AppSettings.ControlPanelViewModel.CurrentTrack == this)
                        {
                            var index = playlist.Tracks.IndexOf(this);
                            if (index + 1 < playlist.Tracks.Count)
                                AppSettings.ControlPanelViewModel.SetTrack(playlist.Tracks[index + 1]);
                            else if(index - 1 > -1)
                                AppSettings.ControlPanelViewModel.SetTrack(playlist.Tracks[index - 1]);
                            else
                                AppSettings.ControlPanelViewModel.SetPlaylistAndTrack(null, null);
                        }
                        playlist.Tracks.Remove(this);
                    }
                }
            }
            OnPropertyChanged();
        }
    }
    public TrackModel(object[] trackData) : base(trackData)
    {
    }

    public TrackModel()
    {
    }
}