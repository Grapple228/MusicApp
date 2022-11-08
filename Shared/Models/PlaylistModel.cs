namespace Shared.Models;

public class PlaylistModel
{
    public string? Id;
    public string? Title;
    public byte[]? Image;
    public string? Creator;
    public string? CreatorId;
    public List<string>? TracksId;

    public PlaylistModel()
    {
        
    }
    
    public PlaylistModel(object[] playlistData)
    {
        Id = ""+playlistData[0];
        CreatorId = ""+playlistData[3];
        //Creator = ""+AppSettings.DataBase.DataReader($"Select UserLogin from Users where UserID='{CreatorId}'")[0][0];
        Title = ""+playlistData[1];
        TracksId = new();
        //TODO GEN IMAGE trackData[2]
        Image = null;
    }
}