using System;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Desktop.Enums;
using Shared;

namespace Desktop.Classes;

public class ImageSources : ObservableObject
{
    private string _homeImage;
    public string HomeImage
    {
        get => _homeImage;
        set
        {
            _homeImage = value;
            OnPropertyChanged();
        }
    }
    private string _addImage;
    public string AddImage
    {
        get => _addImage;
        set
        {
            _addImage = value;
            OnPropertyChanged();
        }
    }
    private string _browseImage;
    public string BrowseImage
    {
        get => _browseImage;
        set
        {
            _browseImage = value;
            OnPropertyChanged();
        }
    }
    public ImageSources()
    {
        ChangeTheme(Themes.Light);
    }

    public void ChangeTheme(Themes theme)
    {
        switch (theme)
        {
            case Themes.Dark:
                _homeImage = "pack://application:,,,/Desktop;component/Icons/DarkTheme/Playlist/home.png";
                _browseImage = "pack://application:,,,/Desktop;component/Icons/DarkTheme/Playlist/browse.png";
                _addImage = "pack://application:,,,/Desktop;component/Icons/DarkTheme/Playlist/add.png";
                break;
            case Themes.Light:
                _homeImage = "pack://application:,,,/Desktop;component/Icons/LightTheme/Playlist/home.png";
                _browseImage = "pack://application:,,,/Desktop;component/Icons/LightTheme/Playlist/browse.png";
                _addImage = "pack://application:,,,/Desktop;component/Icons/LightTheme/Playlist/add.png";
                break;
        }        
    }
}