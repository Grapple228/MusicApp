namespace Shared.Models;

public class AlbumModel
{
    public string? Id;
    public string? Title { get; set; }
    public byte[]? Image;
    public string? ArtistId;
    public List<string>? TracksId;
    public bool IsLiked;

    public AlbumModel()
    {
        
    }
    
    public AlbumModel(object[] albumData)
    {
        Id = ""+albumData[0];
        ArtistId = "" + albumData[1];
        Title = ""+albumData[2];
        //TODO GEN IMAGE trackData[3]
        Image = null;
        TracksId = new();
        IsLiked = false;
    }
}