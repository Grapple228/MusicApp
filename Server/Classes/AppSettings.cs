using System;
using Shared;
using Server.MVVM.View;
using Server.MVVM.ViewModel;
using Server.Tools;

namespace Server.Classes;

public static class AppSettings
{
    public static IniManager Manager { get; set; }
    public static DataBase? DataBase;
    public static MainWindow? Window;
    public static MainViewModel? MainViewModel;

    static AppSettings()
    {
        Manager = new IniManager(Environment.CurrentDirectory + @"\config.ini");
    }
}