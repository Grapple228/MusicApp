   M S B A M L   `   `   `  ????B  >Desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Desktop.MVVM.ViewModelDesktop.MVVM.ViewModel  5      (    "Desktop.MVVM.View.UserLoginnedView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :	viewModel$clr-namespace:Desktop.MVVM.ViewModel     5      
?????? .??5
      ? -   5          ?Name$  	UserPanel??5      $??0,10,0,0q?5      $??Stretch=?5      $	??Top=?5      $??50??5      
?????? .+?5      ??  ?? Orientation$ Vertical=?6   .+?5      ?  ? ColumnDefinitions 5      ?? 5      $
??Auto ?6/   ?? 5      ?? 5      $
??Auto ?6/   .+?5      ?? $??0??6!    ?? CornerRadius$ 90??61   $??32??5   !   $??32??5   !   
???????$??10,0,5,0q?5   !   ?? 5      $??1??6    $??0q?5       $??Center=?5!       $
??Left=?5"       $
??Left=?5#          AccentFontColor#???   5$       $??12(?5%       ??5        ?? *
Username+ ?? FallbackValue$ Username??5%       ?? 5&      -   5         ??Name$ ShowUserInfoCheckBox??5&   3   $??2??6#   $??3,0,10,0q?5'   #   ??5(      ?? 5)   "    ?? 
TargetType" ??6(    ?? Triggers 5*   &   ?? 5+   *    ?? Binding 66   ?? *IsMouseOver+	 ?? ElementName$	 	UserPanel??
 ?? Value	
 False6m   .a?5,   .   ??  ?? Property ?@d?	IsChecked ?? Value$ FalseY?6J   5-   +   .?5/   &   ??  ? ?? 50   *   ?? 51   .    ?? 
TargetType" ??6>   .R?52   2   ?? $ 
Horizontal=?6=   .+?53   6   ??  ??Name$ checkboxImage??6<   $/??)../../Icons/DarkTheme/Other/arrowDown.png??54   <   $??13??6o   ?? 55   6   56   3    ?? Triggers 57   2   P? 58   6    P? Property ?@d?	IsChecked P? Value$
 TrueY?6S   . ?59   :   ??  ?? 
TargetName$ checkboxImage??6A    ? ??$- '../../Icons/DarkTheme/Other/arrowUp.png??6n   5:   7   5<   /   5>   '   5?   #   5A      5B      ?? 5C        
LightColor#???  6   $ 	0,0,15, 0??5D       ?? Padding$ 
10,0,10,10q?5E      $??0,15,7,0q?5F      ??5G      ?? 5H      " ??6$   .?5I   "   ??  ? y?$ 	Collapsed=?6?    5J   "   ?? 5K   &    62   ?? *	IsChecked+$	 ShowUserInfoCheckBox??
 True6r   .a?5L   *   ??  ? y?$ Visible=?6G   5M   '   5O      ._?5Q      ?? $??Center=?6%    ? 	Resources 5R        SelectionFontColor??%	  '??       ?? 5S   "   " ??6(   .?5T   &   ??  ? ??# ?   6C   ?? 5U   &    ? ??$ 16(?6A   ?? 5V   &    ? ??$ Center=?6J    5W   &   P? 5X   *    ?@J?IsMouseOver$
 True??6I   . ?5Y   .   ??  ? ??# ?  6K   5Z   +   5\   #   5]      .+?5^      ?? -   5        $??ProfileLabel??5^   $   $??Profile??68   ?? 5_      -   5        $??LogoutLabel??5_   $   $??Logout??67   6H   5`      5a      5b      5c      5d      