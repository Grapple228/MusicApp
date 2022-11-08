using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Desktop.MVVM.View;
using Desktop.MVVM.ViewModel;
using Desktop.Net;
using DesktopMusicApp;
using Shared;
using NAudio.Wave;

namespace Desktop.Classes;

public static class AppSettings
{
    static AppSettings()
    {
        Manager = new IniManager($"{Environment.CurrentDirectory}\\config.ini");
        //Language.ChangeLanguage(Manager.GetPrivateString("appSettings", "language"));
        ClientThread = new Thread(StartClient);
    }

    private static async void StartClient()
    {
        try
        {
            ServerPort = int.Parse(Manager.GetPrivateString("startSettings", "port"));
            ServerIp = Manager.GetPrivateString("startSettings", "hostIP");
            TcpClient = new TcpClient(ServerIp, ServerPort);
            Connection = new Connection(TcpClient);
            IsConnectedToServer = true;
        }
        catch (Exception ex)
        {
            IsConnectedToServer = false;
        }
    }
    public static string ServerIp { get; set; }
    public static int ServerPort { get; set; }
    public static Thread ClientThread { get; set; }
    public static TcpClient TcpClient { get; set; }

    public static UdpListener? UdpListener { get; set; }
    
    public static Connection Connection { get; set; }
    public static IniManager Manager { get; }
    public static MainViewModel? MainViewModel { get; set; }
    public static MainWindow? MainWindow { get; set; }
    public static App? App { get; set; }
    public static ControlPanelViewModel ControlPanelViewModel { get; set; }
    public static bool IsConnectedToServer { get; set; }
}