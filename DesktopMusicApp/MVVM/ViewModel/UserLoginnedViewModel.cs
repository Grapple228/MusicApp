using System.Windows.Media;
using Shared;

namespace Desktop.MVVM.ViewModel;

public class UserLoginnedViewModel : ObservableObject
{
    public string Username { get; set; }
    public ImageBrush? Image { get; set; }

    public UserLoginnedViewModel(string username, ImageBrush? image)
    {
        Username = username;
        Image = image;
    }
}