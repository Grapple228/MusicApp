using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Server.Classes;
using Server.Net;
using Server.Tools;
using static Server.Tools.Methods;
using static Server.Classes.AppSettings;

namespace Server.MVVM.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool IsServerOn;
        private TcpServer _tcpServer;
        private CancellationTokenSource _tokenSource;
        private Thread MainThread { get; set; }
        
        
        public MainWindow()
        {
            InitializeComponent();
            AppSettings.Window = this;
            MainThread = new Thread(ServerTask);
            IsServerOn = Convert.ToBoolean(Manager.GetPrivateString("startSettings", "startServerOnOpen"));
            if (IsServerOn)
            {
                StartServer();
                StartServerButton.IsEnabled = false;
            }
        }
        
        
        private void StartServer()
        {
            _tokenSource = new CancellationTokenSource();
            int.TryParse(Manager.GetPrivateString("startSettings", "port"), out int port);
            _tcpServer = new TcpServer(port);
            MainThread.Start();
        }

        private async void ServerTask()
        {
            AddTextToOutBox("Trying to start server");
            AddTextToOutBox("Checking database");
            AppSettings.DataBase = new DataBase(
                Manager.GetPrivateString("startSettings", "connectionString"));
            if (!AppSettings.DataBase.CheckConnection())
            {
                AddTextToOutBox("Database error!");
                StartServerButton.IsEnabled = true;
                return;
            }
            AddTextToOutBox("Database checking success!");

            GenerateMusicMessage("vector");
            
            AddTextToOutBox("Server starting");
            var serverTask = _tcpServer.ListenAsync();
            
            while(!_tokenSource.IsCancellationRequested)
            {
                await serverTask;
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void ButtonMaximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }
        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
                WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void ClearLabel_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OutputBox.Clear();
        }
        private void StartServerButton_OnClick(object sender, RoutedEventArgs e)
        {
            IsServerOn = !IsServerOn;
            if (IsServerOn)
            {
                StartServerButton.IsEnabled = false;
                StartServer();
            }
        }
    }
}
