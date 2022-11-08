using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Desktop.Classes;
using NAudio.Wave;

namespace Desktop.Net;

public class UdpListener
{
    public int Port { get; set; }
    public static WaveOut output;
    public static BufferedWaveProvider bufferStream;
    public static Thread UdpThread { get; set; }
    
    public static UdpClient UdpClient { get; set; }
    
    public UdpListener(int port)
    {
        Port = port;
        UdpThread = new Thread(UdpReceiveAudioProcessor)
        {
            IsBackground = true,
            Name = "UDP listener thread"
        };
        UdpThread.Start();
    }
    
    private void UdpReceiveAudioProcessor()
    {
        try
        {
            IPEndPoint localIP = new IPEndPoint(IPAddress.Any, Port);
            Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sender.Bind(localIP);
            EndPoint remoteIP = new IPEndPoint(IPAddress.Parse(AppSettings.Manager.GetPrivateString("startSettings", "hostIP")),
                Port);
            output = new WaveOut();
            bufferStream = new BufferedWaveProvider(new WaveFormat(44100, 16, 2));
            bufferStream.BufferLength = 999999999;
            output.Init(bufferStream);
            output.Play();
            while (true)
            {
                try
                {
                    byte[] data = new byte[65535];
                    int received = sender.ReceiveFrom(data, ref remoteIP);
                    bufferStream.AddSamples(data, 0, received);
                }
                catch (SocketException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine(e);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}