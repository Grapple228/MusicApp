using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using Desktop.Classes;
using Desktop.MVVM.Interfaces;
using Desktop.MVVM.Model;
using Desktop.MVVM.ViewModel;

namespace Desktop.MVVM.View;

public partial class TracksView : UserControl
{
    private TracksViewModel _vm;
    public TracksView()
    {
        InitializeComponent();
        _vm = (TracksViewModel)RootElement.DataContext;
    }
}