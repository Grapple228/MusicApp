using Shared;
using Shared.Models;

namespace Desktop.MVVM.Model;

public class MessageModel : ObservableObject
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
    public string Login { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public bool Result { get; set; }
    
    public RegisterMessage(string login, string password, string email)
    {
        OperationId = OperationId.RegisterClient;
        Login = login;
        Password = password;
        Email = email;
        Result = false;
    }
}
public class LoginMessage : MessageModel
{
    public string Login { get; set; }
    public string Password { get; set; }
    
    public byte[] Image { get; set; }
    public bool Result { get; set; }
    public int UdpPort { get; set; }
    
    public LoginMessage(string login, string password)
    {
        OperationId = OperationId.LoginClient; 
        Login = login;
        Password = password;
        Result = false;
    }
}
public class SettingsMessage : MessageModel
{
    public string Theme;
    public string Language;
    public SettingsMessage(SettingsModel settingsServer)
    {
        OperationId = OperationId.SettingsClient;
        Theme = settingsServer.Theme;
        Language = settingsServer.Language;
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
        OperationId = OperationId.TracksInfoClient;
    }
}