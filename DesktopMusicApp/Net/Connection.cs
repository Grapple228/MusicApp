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
using Desktop.Classes;
using Desktop.MVVM.Interfaces;
using Desktop.MVVM.ViewModel;
using Shared;
using Shared.Models;
using Newtonsoft.Json;
using MusicMessage = Desktop.MVVM.Model.MusicMessage;
using TrackModel = Desktop.MVVM.Model.TrackModel;

namespace Desktop.Net;

public class Connection : IDisposable
    {
        private readonly TcpClient _client;
        private readonly NetworkStream _stream;
        private readonly EndPoint _remoteEndPoint;
        private readonly Task _readingTask;
        private readonly Task _writingTask;
        private readonly Channel<string> _channel;
        bool disposed;

        public Connection(TcpClient client)
        {
            _client = client;
            _stream = client.GetStream();
            _remoteEndPoint = client.Client.RemoteEndPoint;
            _channel = Channel.CreateUnbounded<string>();
            _readingTask = RunReadingLoop();
            _writingTask = RunWritingLoop();
        }
        private async Task RunReadingLoop()
        {
            try
            {
                byte[] headerBuffer = new byte[4];
                while (true)
                {
                    int bytesReceived = await _stream.ReadAsync(headerBuffer, 0, headerBuffer.Length);
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
                Console.WriteLine($"Сервер закрыл соединение.");
                _stream.Close();
            }
            catch (IOException)
            {
                Console.WriteLine($"Подключение закрыто.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
            }
        }
        private async void ProcessMessage(byte[] buffer)
        {
            OperationId result;
            Dictionary<string, object>? message = DecodeMessage(buffer);
            if(!Enum.TryParse(message["OperationId"].ToString(), out result))
                return;
            
            switch (result)
            {
                case OperationId.LoginServer:
                    ProcessLoginMessage(message);
                    break;
                case OperationId.RegisterServer:
                    ProcessRegisterMessage(message);
                    break;
                case OperationId.TracksInfoServer:
                    ProcessTracksInfo();
                    break;
            }
            async void ProcessLoginMessage(Dictionary<string, object>? loginMessage)
            {
                if(AppSettings.MainViewModel!.CurrentContentView is not LoginViewModel logModel)
                    return;
                
                // CHCECK SERVER ANSWER
                if (Convert.ToBoolean(loginMessage["Result"]))
                {
                    AppSettings.MainViewModel.CurrentUserView = new UserLoginnedViewModel(""+loginMessage["Login"], null);
                    AppSettings.MainViewModel.CurrentContentView = new HomeViewModel();
                    AppSettings.UdpListener = new(Int32.Parse(""+loginMessage["UdpPort"]));
                    await SendMessageAsync(new MusicMessage());
                }
                else
                {
                    logModel.SelectedTextBox = logModel.View.PasswordBox;
                    logModel.ErrorLabelText = "Неверный логин или пароль!";
                }
            }
            async void ProcessRegisterMessage(Dictionary<string, object>? registerMessage)
            {
                if(AppSettings.MainViewModel!.CurrentContentView is not RegistrationViewModel regModel)
                    return;
                
                // CHCECK SERVER ANSWER
                if (Convert.ToBoolean(registerMessage["Result"]))
                {
                    AppSettings.MainViewModel.CurrentContentView = new LoginViewModel(""+registerMessage["Login"],"");
                }
                else
                {
                    regModel.SelectedTextBox = regModel.View.LoginBox; 
                    regModel.ErrorLabelText = "Пользователь уже существует!";
                }
            }
            async void ProcessTracksInfo()
            {
                // Показать уведомление о начале синхронизации треков
                await Task.Run(() =>
                {
                    MusicMessage? musicModel = JsonConvert.DeserializeObject<MusicMessage>(Encoding.UTF8.GetString(buffer)!);
                    if(musicModel == null) return;
                    var MainPlaylists = AppSettings.MainViewModel?.Playlists;

                    TracksViewModel tracks = (TracksViewModel)MainPlaylists!.SingleOrDefault(x => x.Id == "Tracks")!;
                    PlaylistsViewModel albums = (PlaylistsViewModel)MainPlaylists!.SingleOrDefault(x => x.Id == "Albums")!;
                    PlaylistsViewModel artists = (PlaylistsViewModel)MainPlaylists!.SingleOrDefault(x => x.Id == "Artists")!;
                    PlaylistsAndTracksViewModel liked = (PlaylistsAndTracksViewModel)MainPlaylists!.SingleOrDefault(x => x.Id == "Liked")!;

                    foreach (var track in musicModel!.Tracks!)
                    {
                        var convertedTrack = Json.StringToInstance<TrackModel>(track);
                        AppSettings.MainWindow!.Dispatcher.Invoke((Action)(() =>
                        {
                            tracks.Tracks.Add(convertedTrack);
                            if(convertedTrack.IsLiked)
                                liked.Tracks.Add(convertedTrack);
                        }));
                    }
                    foreach (var album in musicModel!.Albums!)
                    {
                        var albumInstance = Json.StringToInstance<AlbumModel>(album);
                        var convertedAlbum = new TracksViewModel(albumInstance!);
                        foreach (var trackId in albumInstance!.TracksId)
                        {
                            TrackModel track = tracks.Tracks.SingleOrDefault(x => x.Id == trackId)!;
                            track.Album = convertedAlbum;
                            convertedAlbum.Tracks.Add(track);
                        }
                        AppSettings.MainWindow!.Dispatcher.Invoke((Action)(() =>
                        {
                            albums.Playlists.Add(convertedAlbum);
                            if(convertedAlbum.IsLiked)
                                liked.Playlists.Add(convertedAlbum);
                        }));
                    }
                    foreach (var artist in musicModel!.Artists!)
                    {
                        var artistInstance = Json.StringToInstance<ArtistModel>(artist);
                        var convertedArtist = new PlaylistsAndTracksViewModel(artistInstance!);
                        foreach (var trackId in artistInstance!.TracksId)
                        {
                            TrackModel track = tracks.Tracks.SingleOrDefault(x => x.Id == trackId)!;
                            track.Artist = convertedArtist;
                            convertedArtist.Tracks.Add(track);
                        }
                        foreach (var albumId in artistInstance!.AlbumsId)
                        {
                            IPlaylist playlist = albums.Playlists.SingleOrDefault(x => x.Id == albumId)!;
                            convertedArtist.Playlists.Add(playlist);
                        }
                        AppSettings.MainWindow!.Dispatcher.Invoke((Action)(() =>
                        {
                            artists.Playlists.Add(convertedArtist);
                            if(convertedArtist.IsLiked)
                                liked.Playlists.Add(convertedArtist);
                        }));
                    }
                    foreach (var playlist in musicModel!.Playlists!)
                    {
                        
                        var playlistInstance = Json.StringToInstance<Shared.Models.PlaylistModel>(playlist);
                        var convertedPlaylist = new TracksViewModel(playlistInstance!);
                        foreach (var trackId in playlistInstance!.TracksId)
                        {
                            convertedPlaylist.Tracks.Add(
                                tracks.Tracks.SingleOrDefault(x => x.Id == trackId)!);
                        }
                        AppSettings.MainWindow!.Dispatcher.Invoke((Action)(() =>
                        {
                            MainPlaylists.Add(convertedPlaylist);
                        }));
                    }
                }
                );
                // оказать уведомление о завершении синхронизации треков
            }
        }
        
        private Dictionary<string, object>? DecodeMessage(byte[] buffer)
        {
            return Json.StringToDictionary(Encoding.UTF8.GetString(buffer));
        }
        public async Task SendMessageAsync<T>(T message)
        {
            await _channel.Writer.WriteAsync(Json.InstanceToString(message));
        }
        private async Task RunWritingLoop()
        {
            byte[] header = new byte[4];
            await foreach (string message in _channel.Reader.ReadAllAsync())
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
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                throw new ObjectDisposedException(GetType().FullName);
            disposed = true;
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
        ~Connection() => Dispose(false);
    }