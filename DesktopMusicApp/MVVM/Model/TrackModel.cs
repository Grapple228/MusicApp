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

    public TrackModel(object[] trackData) : base(trackData)
    {
    }

    public TrackModel()
    {
    }
}