using System.Windows;
using System.Windows.Media;
using Desktop.Enums;
using Shared;

namespace Desktop.Classes;

public class Theme : ObservableObject
{
    private Themes _currentTheme;
    public Themes CurrentTheme
    {
        get => _currentTheme;
        set
        {
            _currentTheme = value;
            ChangeTheme(_currentTheme);
            AppSettings.MainViewModel!.Theme = this;
            OnPropertyChanged();
        }
    }
    public string HomeImage{ get; private set; }
    public string AddImage{ get; private set; }
    public string BrowseImage { get; private set; }

    public Theme(Themes theme)
    {
        ChangeTheme(theme);
    }

    private void ChangeTheme(Themes theme)
    {
        _currentTheme = theme;
        ChangeIcons(theme);
        ChangeColors(theme);
    }

    private void ChangeIcons(Themes theme)
    {
        switch (theme)
        {
            case Themes.Dark:
                HomeImage = "pack://application:,,,/Desktop;component/Icons/DarkTheme/Playlist/home.png";
                BrowseImage = "pack://application:,,,/Desktop;component/Icons/DarkTheme/Playlist/browse.png";
                AddImage = "pack://application:,,,/Desktop;component/Icons/DarkTheme/Playlist/add.png";
                break;
            case Themes.Light:
                HomeImage = "pack://application:,,,/Desktop;component/Icons/LightTheme/Playlist/home.png";
                BrowseImage = "pack://application:,,,/Desktop;component/Icons/LightTheme/Playlist/browse.png";
                AddImage = "pack://application:,,,/Desktop;component/Icons/LightTheme/Playlist/add.png";
                break;
        }
        AppSettings.Manager.WritePrivateString("appSettings","theme",theme.ToString());
    }

    private void ChangeColors(Themes theme)
    {
        var app = Application.Current;
        
        switch (theme)
        {
            case Themes.Dark:
                Application.Current.Resources["FontColor"] = 
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#737373"));
                Application.Current.Resources["AccentFontColor"] = 
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c4c4c4"));
                Application.Current.Resources["SelectionFontColor"] = 
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("Aquamarine"));
                Application.Current.Resources["BackgroundColor"] = 
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#282828"));
                Application.Current.Resources["LightColor"] = 
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#252525"));
                Application.Current.Resources["HoverItemColor"] = 
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#242424"));
                Application.Current.Resources["SelectionItemColor"] = 
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#202020"));
                break;
            case Themes.Light:
                Application.Current.Resources["FontColor"] = 
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2b2b2b"));
                Application.Current.Resources["AccentFontColor"] = 
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#212121"));
                Application.Current.Resources["SelectionFontColor"] = 
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("MediumAquamarine"));
                Application.Current.Resources["BackgroundColor"] = 
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c2c2c2"));
                Application.Current.Resources["LightColor"] = 
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#d6d6d6"));
                Application.Current.Resources["HoverItemColor"] = 
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#b8b8b8"));
                Application.Current.Resources["SelectionItemColor"] = 
                    new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ababab"));
                break;
        }
    }
}