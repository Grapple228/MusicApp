using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Desktop.MVVM.Model;
using Shared;

namespace Desktop.Classes;

public class Language : ObservableObject
{
    public enum Languages
    {
        ru_ru,
        en_us
    }

    static Language()
    {
        Translations = new Dictionary<Languages, Dictionary<string, string>>()
        {
            { Languages.en_us, new Dictionary<string, string>()
            {
                {"librar",""},        
                {"",""},        
                {"",""},        
                {"",""},        
                {"",""},        
            } },
            { Languages.ru_ru, new Dictionary<string, string>()
            {
                {"",""},        
                {"",""},        
                {"",""},        
                {"",""},        
                {"",""},  
            } }
        };
    }

    private static Dictionary<Languages, Dictionary<string, string>> Translations;

    private Dictionary<string, string> _currentLangugeDictionary;

    public Dictionary<string, string> CurrentLangugeDictionary
    {
        get => _currentLangugeDictionary;
        set
        {
            
            OnPropertyChanged();
        }
    }

    public static void ChangeLanguage(string language)
    {
        Enum.TryParse(language.ToLower(), out Languages lang);
        switch (lang)
        {
            case Languages.ru_ru:
                break;
            case Languages.en_us:
                break;
            default:
                break;
        }
        
        void setToEnglish()
                        {
                            if(AppSettings.MainWindow!.FindName("TracksList") is not ListView listView) return;
        
                            foreach (PlaylistModel i in listView.Items)
                            {
                                switch (i.Id)
                                {
                                    case "Liked":
                                        i.Title = "Любимое";
                                        continue;
                                    case "Albums":
                                        i.Title = "Альбомы";
                                        continue;
                                    case "Artists":
                                        i.Title = "Исполнители";
                                        continue;
                                    case "Tracks":
                                        i.Title = "Треки";
                                        continue;
                                }
                            }
                        }
    }
}