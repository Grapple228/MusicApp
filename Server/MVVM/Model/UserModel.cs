using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Sockets;
using Shared;
using Server.Classes;

namespace Server.MVVM.Model;

public class UserModel : ObservableObject
{
    public string? Username { get; set; }
    public string? Id { get; set; }
    public BindingList<DeviceModel> Devices { get; set; }

    public UserModel(string id, string username)
    {
        Id = id;
        Username = username;
        Devices = new BindingList<DeviceModel>();
    }
}