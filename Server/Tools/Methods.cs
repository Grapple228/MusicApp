using System;
using System.Collections.Generic;
using System.Linq;
using Shared.Models;
using Newtonsoft.Json;
using Server.Classes;
using static  Server.Classes.AppSettings;

namespace Server.Tools;

public static class Methods
{
    public static void AddTextToOutBox(string text)
    {
        if(Window == null) return;
        
        var message = $"[{DateTime.Now:yyyy.MM.dd hh:mm:ss.fff}]:  {text}";
        Window.Dispatcher.Invoke((Action)(() =>
        {
            Window.OutputBox.Text += $"{message}\n";
        }));
    }
    
    public static MusicMessage GenerateMusicMessage(string userLogin)
    {
        MusicMessage answerMessage = new MusicMessage();
        DataBase db = AppSettings.DataBase!;
        try
        {
            List<TrackModel> tracks = new();
            List<AlbumModel> albums = new();
            List<PlaylistModel> playlists = new();
            List<ArtistModel> artists = new();

            string userId = ""+db.DataReader($"select UserID from Users where UserLogin='{userLogin}'")[0][0];
            
            // Создание альбомов
            foreach (var albumId in db.DataReader(
                         $"select AlbumID from UsersAndAlbums where UserID='{userId}'"))
            {
                var albumResult = db.DataReader(
                    $"Select * from Albums where AlbumID='{albumId[0]}'")[0];

                var artistModel = artists.SingleOrDefault(x => x.Id == ""+albumResult[1]);
                if (artistModel == null)
                {
                    artistModel = new ArtistModel(db.DataReader(
                        $"Select * from Artists where ArtistID='{albumResult[1]}'")[0]);
                    artists.Add(artistModel);
                }
                var albumModel = new AlbumModel(albumResult);
                albums.Add(albumModel);
                artistModel.AlbumsId.Add(albumModel.Id);
                
                var albumTracks = db.DataReader($"select * from Tracks where AlbumID='{""+albumId[0]}'");
                
                foreach (var track in albumTracks)
                {
                    var trackModel = tracks.SingleOrDefault(x => x.Id == ""+track[0]);
                    if (trackModel == null)
                    {
                        trackModel = new TrackModel(track);
                        trackModel.AlbumId = albumModel.Id;
                        trackModel.ArtistId = artistModel.Id;
                        tracks.Add(trackModel);
                    }
                    albumModel.TracksId.Add(trackModel.Id);
                    artistModel.TracksId.Add(trackModel.Id);
                }
            }
            // Создание плейлистов
            foreach (var playlistId in db.DataReader(
                         $"select PlaylistID from UsersAndPlaylists where UserID='{userId}'"))
            {
                var playlistModel = playlists.SingleOrDefault(x => x.Id == ""+playlistId[0]);

                if (playlistModel == null)
                {
                    playlistModel = new PlaylistModel(db.DataReader(
                        $"select * from Playlists where PlaylistID='{playlistId[0]}'")[0]);
                    playlists.Add(playlistModel);
                }

                var playlistTracks = db.DataReader(
                    $"select TrackID from PlaylistsAndTracks where PlaylistID='{playlistId[0]}'");
                
                foreach (var track in playlistTracks)
                {
                    var trackModel = tracks.SingleOrDefault(x => x.Id == ""+track[0]);
                    if (trackModel == null)
                    {
                        var trackResult = db.DataReader($"select * from Tracks where TrackID='{track[0]}'")[0];
                        trackModel = new TrackModel(trackResult);
                        trackModel.AlbumId = "" + trackResult[2];
                        tracks.Add(trackModel);
                    }
                    var albumModel = albums.SingleOrDefault(x => x.Id == trackModel.AlbumId);
                    if (albumModel == null)
                    {
                        var albumResult =
                            db.DataReader($"Select * from Albums where AlbumID='{trackModel.AlbumId}'")[0];
                        albumModel = new AlbumModel(albumResult);
                        albums.Add(albumModel);
                    }
                    var artistModel = artists.SingleOrDefault(x => x.Id == ""+albumModel.ArtistId);
                    if (artistModel == null)
                    {
                        artistModel = new ArtistModel(db.DataReader(
                            $"Select * from Artists where ArtistID='{albumModel.ArtistId}'")[0]);
                        artists.Add(artistModel);
                    }
                    trackModel.ArtistId = artistModel.Id;

                    if(!albumModel.TracksId!.Contains(trackModel.Id!))
                        albumModel.TracksId.Add(trackModel.Id!);
                    if(!artistModel.TracksId!.Contains(trackModel.Id!))
                        artistModel.TracksId.Add(trackModel.Id!);
                    if(!artistModel.AlbumsId!.Contains(albumModel.Id!))
                        artistModel.AlbumsId.Add(albumModel.Id!);

                    if(!playlistModel.TracksId!.Contains(trackModel.Id!))
                        playlistModel.TracksId.Add(trackModel.Id!);
                }
            }

            List<string> tracksDict = new();
            foreach (var track in tracks)
            {
                var result = JsonConvert.SerializeObject(track);
                tracksDict.Add(result);
            }
            answerMessage.Tracks = tracksDict.ToArray();
            
            List<string> albumsDict = new();
            foreach (var album in albums)
            {
                var result = JsonConvert.SerializeObject(album);
                albumsDict.Add(result);
            }
            answerMessage.Albums = albumsDict.ToArray();
            
            List<string> playlistsDict = new();
            foreach (var playlist in playlists)
            {
                var result = JsonConvert.SerializeObject(playlist);
                playlistsDict.Add(result);
            }
            answerMessage.Playlists = playlistsDict.ToArray();
            
            List<string> artistDict = new();
            foreach (var artist in artists)
            {
                var result = JsonConvert.SerializeObject(artist);
                artistDict.Add(result);
            }
            answerMessage.Artists = artistDict.ToArray();
        }
        catch (Exception e)
        {
            AddTextToOutBox(e.Message);
        }
        
        return answerMessage;
    }
}