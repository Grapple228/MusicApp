namespace Shared.Models;

public class TrackModel : ObservableObject
{
    /// <summary>
    /// ID трека
    /// </summary>
    public string? Id;
    public string? ArtistId;
    public string? AlbumId;
    /// <summary>
    /// Название трека
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// Изображение
    /// </summary>
    public byte[]? Image { get; set; }
    /// <summary>
    /// Длительность
    /// </summary>
    public float? Duration { get; set; }
    /// <summary>
    /// Частота дискретизации
    /// </summary>
    public int? Frequency;
    /// <summary>
    /// Количество каналов
    /// </summary>
    public int? Channels;
    /// <summary>
    /// Количество битов на секунду
    /// </summary>
    public int? Bytes;

    private bool _isLiked;
    public bool IsLiked
    {
        get => _isLiked;
        set
        {
            _isLiked = value;
            OnPropertyChanged();
        }
    }

    private bool? _isBlocked;
    public bool? IsBlocked
    {
        get => _isBlocked;
        set
        {
            _isBlocked = value;
            OnPropertyChanged();
        }
    }

    public TrackModel(object[] trackData)
    {
        Id =""+trackData[0];
        Title =""+trackData[1];
        //TODO GEN IMAGE trackData[3]
        Image = null;
        Duration = Convert.ToInt32((decimal)trackData[4]);
    }
    public TrackModel()
    {
    }
}