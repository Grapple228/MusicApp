using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Desktop.Classes;
using Desktop.Enums;
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
        var scv = (ScrollViewer)sender;
        scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta / ScrollSizeDividing);
        e.Handled = true;
    }

    private void ContentScrollViewer_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        var scv = (ScrollViewer)sender;
        scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta / ScrollSizeDividing);
        e.Handled = true;
    }

    private void PageBorder_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if(sender is not Border border) return;
        AppSettings.MainViewModel!.CurrentContentView = border.Name switch
        {
            "HomeBorder" => new HomeViewModel(),
            "AddBorder" => new AddViewModel(),
            "BrowseBorder" => new BrowseViewModel(),
            _ => AppSettings.MainViewModel!.CurrentContentView
        };
    }

    private void ItemBorder_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if(sender is not Border border) return;
        AppSettings.MainViewModel!.SelectedPlaylist = (IPlaylist)border.DataContext;
    }

    private void ChangeThemeButton_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        AppSettings.Theme.CurrentTheme = AppSettings.Theme.CurrentTheme switch
        {
            Themes.Dark => Themes.Light,
            Themes.Light => Themes.Dark,
            _ => Themes.Dark
        };
    }
}