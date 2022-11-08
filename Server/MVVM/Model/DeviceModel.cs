using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Shared;
using NAudio.Wave;
using Server.Net;

namespace Server.MVVM.Model;

public class DeviceModel : ObservableObject
{
    ~DeviceModel(){
        
    }
    public UdpClient AudioSender { get; set; }
    public string Id
    {
        get => _id;
        set
        {
            _id = value;
            OnPropertyChanged();
        }
    }
    private string _id;
    public Connection Connection { get; set; }
    public string Ip { get; set; }
    public int Port { get; set; }
    public Thread UdpThread { get; set; }

    public CancellationTokenSource StopUdpThreadToken { get; set; }

    public DeviceModel(Connection connection)
    {
        Connection = connection;
        var ipInfo = connection.RemoteEndPoint.ToString()!.Split(':');
        _id = Guid.NewGuid().ToString("N").Remove(16, 16);
        Ip = ipInfo[0];
        Port = int.Parse(ipInfo[1]);
        StopUdpThreadToken = new CancellationTokenSource();
        UdpThread = new Thread(UdpSenderProcess)
        {
            IsBackground = true,
            Name = _id + "_thread"
        };
    }

    private void UdpSenderProcess(object threadData)
    {
        try
        {
            var token = (CancellationTokenSource)threadData;
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(Ip), Port );

            string path = @"C:\Users\Green Apple\Desktop\Idealism-ilia_s-dream.wav";
            WaveFileReader reader = new WaveFileReader(path);
            int sendingCount = 3000;
            byte[] data = new byte[sendingCount];
            while (reader.Position < reader.Length)
            {
                if(token.IsCancellationRequested) 
                    break;
                reader.Read(data, 0, sendingCount);
                Thread.Sleep(5);
                client.SendTo(data, remoteEndPoint);
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine(e);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}