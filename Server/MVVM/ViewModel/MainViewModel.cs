using System.ComponentModel;
using Shared;
using Server.Classes;
using Server.MVVM.Model;

namespace Server.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    public BindingList<UserModel?> Users { get; set; }
    public MainViewModel()
    {
        AppSettings.MainViewModel = this;
        Users = new BindingList<UserModel?>();
    }
}