using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using Shared;
using Server.Classes;
using Server.MVVM.Model;
using Server.Tools;
using LoginMessage = Server.MVVM.Model.LoginMessage;
using RegisterMessage = Server.MVVM.Model.RegisterMessage;

namespace Server.Net;

public class Connection : IDisposable
    {
        private readonly TcpClient _client;
        private readonly NetworkStream _stream;
        public readonly EndPoint RemoteEndPoint;
        private readonly Task _readingTask;
        private readonly Task _writingTask;
        private readonly Action<Connection> _disposeCallback;
        private readonly Channel<string> _channel;
        bool _disposed;

        public Connection(TcpClient client, Action<Connection> disposeCallback)
        {
            _client = client;
            _stream = client.GetStream();
            RemoteEndPoint = client.Client.RemoteEndPoint!;
            _disposeCallback = disposeCallback;
            _channel = Channel.CreateUnbounded<string>();
            _readingTask = RunReadingLoop();
            _writingTask = RunWritingLoop();
        }
        private async Task RunReadingLoop()
        {
            await Task.Yield();
            try
            {
                byte[] headerBuffer = new byte[4];
                while (true)
                {
                    int bytesReceived = await _stream.ReadAsync(headerBuffer, 0, 4);
                    if (bytesReceived != 4)
                        break;
                    int length = BinaryPrimitives.ReadInt32LittleEndian(headerBuffer);
                    byte[] buffer = new byte[length];
                    int count = 0;
                    while (count < length)
                    {
                        bytesReceived = await _stream.ReadAsync(buffer, count, buffer.Length - count);
                        count += bytesReceived;
                    }
                    
                    ProcessMessage(buffer);
                }
                Methods.AddTextToOutBox($"Connection {RemoteEndPoint} had closed");
                _stream.Close();
            }
            catch (IOException)
            {
                Methods.AddTextToOutBox($"Connection {RemoteEndPoint} closed by server");
            }
            catch (Exception ex)
            {
                Methods.AddTextToOutBox($"{ex.GetType().Name}: {ex.Message}");
            }
            RemoveConnection();
            if (!_disposed)
                _disposeCallback(this);
        }

        private void ProcessMessage(byte[] buffer)
        {
            Dictionary<string, object> message = DecodeMessage(buffer);
            if(!Enum.TryParse(message["OperationId"].ToString(), out OperationId result))
                return;
            
            switch (result)
            {
                case OperationId.LoginClient:
                    ProcessLoginMessage();
                    break;
                case OperationId.RegisterClient:
                    ProcessRegisterMessage();
                    break;
                case OperationId.LogoutClient:
                    ProcessLogoutMessage();
                    break;
                case OperationId.TracksInfoClient:
                    ProcessTracksInfoClient();
                    break;
            }

            async void ProcessLoginMessage()
            {
                LoginMessage answer = new LoginMessage(
                    UserDataProcessor.NormalizeLogin("" + message["Login"]));
                DeviceModel? device = null;
                try
                {
                    // CHECK LOGIN AND PASSWORD
                    answer.Result = UserDataProcessor.CheckUserLogin(
                        answer.Login,
                        "" + message["Password"]);

                    if (!answer.Result) return;
                    Application.Current.Dispatcher.Invoke((Action)delegate
                    {
                        var userdata =
                            AppSettings.DataBase!.DataReader(
                                $"Select Users.UserId from Users where Users.UserLogin='{answer.Login}'")[0];
                        var users = AppSettings.MainViewModel!.Users;
                        var user = users.SingleOrDefault(x => x?.Id == "" + userdata[0]);
                        if (user == null)
                        {
                            user = new UserModel("" + userdata[0], answer.Login);
                            users.Add(user);
                        }

                        device = user.Devices.SingleOrDefault(x => x?.Connection == this);

                        if (device != null) return;
                        device = new DeviceModel(this);
                        user.Devices.Add(device);
                    });
                }
                catch (Exception e)
                {
                    Methods.AddTextToOutBox(e.Message);
                }
                finally
                {
                    if (device != null)
                    {
                        answer.UdpPort = device!.Port;
                        // ТРАНСЛИРОВАНИЕ МУЗЫКИ
                        // ТРАНСЛИРОВАНИЕ МУЗЫКИ
                        // ТРАНСЛИРОВАНИЕ МУЗЫКИ
                        // device.UdpThread.Start(device.StopUdpThreadToken);
                    }
                    
                    await SendMessageAsync(answer);
                }
            }
            async void ProcessRegisterMessage()
            {
                RegisterMessage answer = new RegisterMessage(
                    UserDataProcessor.NormalizeLogin("" + message["Login"]));
                try
                {
                    answer.Result = UserDataProcessor.RegisterUser(
                        answer.Login, 
                        ""+message["Password"], 
                        ""+message["Email"]);
                }
                catch (Exception e)
                {
                    Methods.AddTextToOutBox(e.Message);
                }
                finally
                {
                    await SendMessageAsync(answer);
                }
            }
            void ProcessLogoutMessage()
            {
                RemoveConnection();
            }
            async void ProcessTracksInfoClient()
            {
                Shared.Models.MusicMessage message = new Shared.Models.MusicMessage();
                UserModel? user = null;
                Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    try
                    {
                        foreach (var u in AppSettings.MainViewModel!.Users)
                        {
                            if (u!.Devices.Any(device => device.Connection == this))
                            {
                                user = u;
                                break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Methods.AddTextToOutBox(e.Message);
                    }
                });
                if(user != null)
                    message = Methods.GenerateMusicMessage(user!.Username!);
                await SendMessageAsync(message);
            }
        }

        private Dictionary<string, object> DecodeMessage(byte[] buffer)
        {
            return Json.StringToDictionary(Encoding.UTF8.GetString(buffer));
        }
        private async Task SendMessageAsync<T>(T message)
        {
            Methods.AddTextToOutBox($">> {RemoteEndPoint}: {message}");
            await _channel.Writer.WriteAsync(Json.InstanceToString(message));
        }
        private async Task RunWritingLoop()
        {
            byte[] header = new byte[4];
            await foreach(string message in _channel.Reader.ReadAllAsync())
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                BinaryPrimitives.WriteInt32LittleEndian(header, buffer.Length);
                await _stream.WriteAsync(header, 0, header.Length);
                await _stream.WriteAsync(buffer, 0, buffer.Length);
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (_disposed)
                throw new ObjectDisposedException(typeof(TcpServer).FullName);
            _disposed = true;
            if (_client.Connected)
            {
                _channel.Writer.Complete();
                _stream.Close();
                Task.WaitAll(_readingTask, _writingTask);
            }
            if (disposing)
            {
                _client.Dispose();
            }
        }

        private void RemoveConnection()
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                try
                {
                    var users = AppSettings.MainViewModel!.Users;
                    foreach (var user in users)
                    {
                        int count = user!.Devices.Count;
                        foreach (var device in user!.Devices)
                        {
                            if (device.Connection == this)
                            {
                                if (count == 1)
                                {
                                    users.Remove(user);
                                    return;
                                }
                                device.StopUdpThreadToken.Cancel();
                                user.Devices.Remove(device);
                                return;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Methods.AddTextToOutBox(e.Message);
                }
            });
        }
        ~Connection() => Dispose(false);
    }