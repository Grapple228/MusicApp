namespace Shared.Models;

public class ArtistModel
{
    public string? Id;
    public string? Name { get; set; }
    public byte[]? Image;
    public List<string>? TracksId;
    public List<string>? AlbumsId;

    public ArtistModel()
    {
        
    }
    public ArtistModel(object[] artistData)
    {
        Id = ""+artistData[0];
        Name = "" + artistData[1];
        
        TracksId = new();
        AlbumsId = new();
    }
}