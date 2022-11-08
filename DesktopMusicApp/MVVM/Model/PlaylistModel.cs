using System.ComponentModel;
using System.Runtime.CompilerServices;
using Desktop.Classes;
using Desktop.MVVM.Interfaces;
using Desktop.MVVM.ViewModel;
using Desktop.Tools;
using Shared;
using Shared.Models;

namespace Desktop.MVVM.Model;

public abstract class PlaylistModel : IPlaylist, IObservableObject
{
    public object? SourceClass { get; set; }
    public string Id { get; set; }
    public bool IsPlaying { get; set; }
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
    private string _title;
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }
   
    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string name = null!)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}