namespace Shared.Models;

public class SettingsModel
{
    public string Theme { get; set; }
    public string Language { get; set; }
    public SettingsModel(string userId)
    {
        Theme = "light";
        Language = "en-US";
    }
}