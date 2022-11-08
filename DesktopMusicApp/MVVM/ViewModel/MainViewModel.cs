using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Desktop.Classes;
using Desktop.MVVM.Interfaces;
using Desktop.MVVM.Model;
using Shared;

namespace Desktop.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    private TrackModel? _currentTrack;
    public TrackModel? CurrentTrack
    {
        get => _currentTrack;
        set
        {
            _currentTrack = value;
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
            OnPropertyChanged();
        }
    }
    
    public BindingList<IPlaylist> Playlists
    {
        get => _playlists;
        set
        {
            _playlists = value;
            OnPropertyChanged();
        }
    }
    private BindingList<IPlaylist> _playlists;

    private object _currentContentView;
    private object _currentUserView;

    public MainViewModel()
    {
        Playlists = new BindingList<IPlaylist>()
        {
            new PlaylistsAndTracksViewModel("Liked", "Liked"),
            new TracksViewModel("Tracks","Tracks"),
            new PlaylistsViewModel("Albums","Albums"),
            new PlaylistsViewModel("Artists","Artists"),
        };
        CurrentUserView = new UserNeedToLoginViewModel();
    }

    public void ClearPlaylists()
    {
        Playlists = new BindingList<IPlaylist>()
        {
            new PlaylistsAndTracksViewModel("Liked", "Liked"),
            new TracksViewModel("Tracks","Tracks"),
            new PlaylistsViewModel("Albums","Albums"),
            new PlaylistsViewModel("Artists","Artists"),
        };
    }
    
    
    public object CurrentUserView
    {
        get => _currentUserView;
        set
        {
            _currentUserView = value;
            OnPropertyChanged();
        }
    }

    private IPlaylist? _selectedPlaylist;
    public IPlaylist? SelectedPlaylist
    {
        get => _selectedPlaylist;
        set
        {
            _selectedPlaylist = value;
            if (_selectedPlaylist != null)
            {
                CurrentContentView = _selectedPlaylist;
                RemoveButtonSelection();
            }
   
            OnPropertyChanged();

            void RemoveButtonSelection()
            {
                AppSettings.MainWindow!.HomeButton.IsChecked = false;
                AppSettings.MainWindow.AddButton.IsChecked = false;
                AppSettings.MainWindow.BrowseButton.IsChecked = false;
            }
        }
    }
    
    public object CurrentContentView
    {
        get => _currentContentView;
        set
        {
            AppSettings.MainWindow!.Dispatcher.Invoke((Action)(() =>
            {
                _currentContentView = value;
                switch (_currentContentView)
                {
                    case HomeViewModel:
                        AppSettings.MainWindow.HomeButton.IsChecked = true;
                        RemovePlaylistSelection();
                        break;
                    case AddViewModel:
                        AppSettings.MainWindow.AddButton.IsChecked = true;
                        RemovePlaylistSelection();
                        break;
                    case BrowseViewModel:
                        AppSettings.MainWindow.BrowseButton.IsChecked = true;
                        RemovePlaylistSelection();
                        break;
                    case IPlaylist playlist:
                    {
                        if (AppSettings.MainWindow.Playlists.SelectedItem is not IPlaylist selected)
                            break;
                        if (Playlists.SingleOrDefault(x => x.Id == playlist.Id) == null)
                        {
                            AppSettings.MainWindow.Playlists.SelectedItem = null;
                        }
                        break;
                    }
                    default:
                        SelectedPlaylist = null;
                        AppSettings.MainWindow.BrowseButton.IsChecked = true;
                        AppSettings.MainWindow.BrowseButton.IsChecked = false;
                        break;
                }


                OnPropertyChanged();

                void RemovePlaylistSelection()
                {
                    SelectedPlaylist = null;
                }
            }));
        }
    }
}