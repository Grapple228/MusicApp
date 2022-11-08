using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Shared;

public interface IObservableObject : INotifyPropertyChanged
{
    public new event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string name = null!);
}