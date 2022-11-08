using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Desktop.Classes;
using Desktop.MVVM.Interfaces;
using Desktop.MVVM.Model;
using Desktop.MVVM.ViewModel;
using Microsoft.VisualBasic.ApplicationServices;

namespace Desktop.MVVM.View;

public partial class MainWindow
{
    private const float ScrollSizeDividing = 8.0f;

    public MainWindow()
    {
        InitializeComponent();
        VolumeSliderHtArea.MouseLeave += VolumeSliderHtAreaOnMouseLeave;
        VolumeButton.MouseEnter += VolumeButtonOnMouseEnter;
        ControlBar.Visibility = Visibility.Collapsed;
        ControlPanelViewModel control = new();
        ControlBar.DataContext = control;
        VolumeSliderHtArea.DataContext = control;
        AppSettings.ControlPanelViewModel = control;
        AppSettings.ClientThread.Start();
    }

    private void VolumeButtonOnMouseEnter(object sender, MouseEventArgs e)
    {
        VolumeSliderBorder.Visibility = Visibility.Visible;
    }

    private void VolumeSliderHtAreaOnMouseLeave(object sender, MouseEventArgs e)
    {
        VolumeSliderBorder.Visibility = Visibility.Collapsed;
    }

    private void ButtonClose_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
    private void ButtonMaximize_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
    }
    private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2)
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

        if (e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }

    private void VolumeButton_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        AppSettings.ControlPanelViewModel.IsMuted = !AppSettings.ControlPanelViewModel.IsMuted;
    }

    private void LeftPanelScrollViewer_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        ScrollViewer scv = (ScrollViewer)sender;
        scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta / ScrollSizeDividing);
        e.Handled = true;
    }

    private void ContentScrollViewer_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        ScrollViewer scv = (ScrollViewer)sender;
        scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta / ScrollSizeDividing);
        e.Handled = true;
    }

    private void PageBorder_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if(sender is not Border border) return;
        switch (border.Name)
        {
            case "HomeBorder": 
                AppSettings.MainViewModel!.CurrentContentView = new HomeViewModel();
                break;
            case "AddBorder":
                AppSettings.MainViewModel!.CurrentContentView = new AddViewModel();
                break;
            case "BrowseBorder":
                AppSettings.MainViewModel!.CurrentContentView = new BrowseViewModel();
                break;
        }
    }

    private void ItemBorder_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if(sender is not Border border) return;
        AppSettings.MainViewModel!.SelectedPlaylist = (IPlaylist)border.DataContext;
    }
}