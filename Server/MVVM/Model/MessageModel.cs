using Shared;
using Shared.Models;

namespace Server.MVVM.Model;

public abstract class MessageModel : ObservableObject
{
    public OperationId OperationId { get; set; }

    public MessageModel(OperationId operationId)
    {
        OperationId = operationId;
    }

    protected MessageModel()
    {
        
    }
}
public class RegisterMessage : MessageModel
{
    public readonly string Login;
    public bool Result;
    
    public RegisterMessage(string login)
    {
        OperationId = OperationId.RegisterServer;
        Login = login;
        Result = false;
    }
}
public class LoginMessage : MessageModel
{
    public string Login { get; set; }
    public byte[] Image { get; set; }
    public bool Result { get; set; }
    public int UdpPort { get; set; }
    
    public LoginMessage(string login)
    {
        OperationId = OperationId.LoginServer; 
        Login = login;
        Result = false;
    }
}
public class SettingsMessage : MessageModel
{
    public string Theme;
    public string Language;
    public SettingsMessage(SettingsModel settings)
    {
        OperationId = OperationId.SettingsServer;
        Theme = settings.Theme;
        Language = settings.Language;
    }
}
public class MusicMessage : MessageModel
{
    public string[]? Tracks;
    public string[]? Playlists;
    public string[]? Albums;
    public string[]? Artists;
    
    public MusicMessage()
    {
        OperationId = OperationId.TracksInfoServer;
        Tracks = null;
        Playlists = null;
        Albums = null;
        Artists = null;
    }
}

