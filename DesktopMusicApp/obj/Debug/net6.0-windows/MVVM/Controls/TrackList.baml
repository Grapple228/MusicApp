   M S B A M L   `   `   `  ????B  >Desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Desktop.MVVM.ViewModelDesktop.MVVM.ViewModel  5      %    Desktop.MVVM.Controls.TrackList   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :	viewModel$clr-namespace:Desktop.MVVM.ViewModel     5      .??5
      ?? -   5        $??RootElement??5
      $??0q?5      
?????? ??5
   "   ?? *Tracks+  ?? SelectedItem  5      ?? *SelectedTrack+??5      ?? 5       ?? 
TargetType" ??6   .?5      ??  ?? Property ? |? ?? Value$ False??66   ?? 5       ? ?? 5      ?? 5   "    ?? 
TargetType" ??62    ? 	Resources 5   &      BorderStyle??%?   &	        ?? 5   *   " ??60   .?5   .   ??  ? ??
 ???? 6K   ?? 5   .    ?@??CornerRadius$ 13??6M   5   +   5   '   .R?5   &   ?? $??TrackListItemBorder??6-    ?? Padding$	 0,6q?6H   $	??0,1q?5   -   $??0q?5   -   #??z??5   -   #??[  5   -   ._?5   *   ?  ? 	Resources 5    .     TextBlockStyle  AccentFontColor  AllowSelection ?? BasedOn	 ?? Triggers
 P? Property P? Value  SelectionFontColor??%?  &	       &	     7  ?? 5!   2   " ??68   .?5"   6   ??  ? ??# ?  6S   ?? 5#   6    ? ??$ 12(?6Q   ?? 5$   6    ? ??$
 Left=?6\   ?? 5%   6    ? ??$ Center=?6Z   ?? 5&   6    ? ??$ 0q?6O   ?? 5'   6    ?@??Padding$ 0q?6P   5(   3   ?? 5)   2   " ??68   8   	 5*   6   P? 5+   :   
 ?@J?IsMouseOver$
 True??6Y   . ?5,   >   ??  ? ??# ?  6[   5-   ;   5/   3   50   /    ? ColumnDefinitions 51   .   ?? 52   2   $??40 ?6C   ?? 53   2   $??* ?6C   ?? 54   2   $??* ?6C   ?? 55   2   $??* ?6C   ?? 56   2   $??* ?6C   ?? 57   2   $??40 ?6C   .+?5:   .   ? $??0??63   .+?5;   2   ?? $??
PlayButton??69   -   5        $??28??5;   K   $??28??6V    ?? CornerRadius$ 90??6b     SelectionItemColor#???  5<   A   $??0q?6s   $y?Hidden=?5=   x   ._?5>   6   ?? $??Center=?6?   $??Center=?6\    J? IsHitTestVisible$ False??5?   t    d? 	IsChecked 6G   ?? *	IsPlaying+ ?? Mode$ TwoWay=???5@   :   ?? 5A   >   " ??6D   .?5B   B   ??  ? ?? 5C   F   ?? 5D   J   " ??6Z   .R?5E   N   ??  ?? Orientation$ 
Horizontal=?6Y   .+?5F   R   ??  ??Name$ checkboxImage??6X   $??0,0,0,0q?5G   ]   $/??)../../Icons/DarkTheme/ControlBar/play.png??5H   ]   $??13??6?   ?? 5I   R   5J   O    ?? Triggers 5K   N   P? 5L   R   
 ?@d?	IsChecked$
 TrueY?6o   . ?5M   V   ??  ?? 
TargetName$ checkboxImage??6]    ? ??$ 0q?6?   6?   ?? 5N   V   $ checkboxImage??6]    ? ??$0 *../../Icons/DarkTheme/ControlBar/pause.png??6?   5O   S   5Q   K   5S   C   5T   ?   5V   7   5W   3   5X   /   ?? 5Y   .   $??1??68   #??[ 5Z   8   ??5Y   H   ?? *Title+5Z   8   ?? 5[   .   $??AlbumTextBlock??5]   8   -   5        $??2??5[   8   #??[ 5\   8   ??5[   H   ?? *Album.Title+5^   8   ?? 5_   .   $??ArtistTextBlock??5a   8   -   5        $??3??5_   8   #??[ 5`   8   ??5_   H   ?? *Artist.Title+5b   8   ?? 5c   .   $??4??68   #??[ 5d   8   ??5c   H   ?? *
Duration+5d   8   ? 5e   .   $??5??63   .+?5f   2   ?? $??
LikeButton??69   $??28??6K   $??28??6V   $ 90??6b   #???  5g   9   $??0q?6k   ??5h   6   ?? 5i   :   " ??6@   .?5j   >   ??  ? y?$ Hidden=?6[   	 5k   >   ?? 5l   B    ?? Binding 6N   ?? *	IsLiked+ ?? Value True6j   .a?5m   F   ??  ? y?$ Visible=?6c   5n   C   5p   ;   ._?5r   6   ?? $??Center=?6?   $??Center=?6\    5s   ?   ?? *	IsLiked+??5t   :   ?? 5u   >   " ??6D   .?5v   B   ??  ? ?? 5w   F   ?? 5x   J   " ??6Z   .R?5y   N   ?? $ 
Horizontal=?6Y   .+?5z   R   ?? $ checkboxImage??6X   $0??*../../Icons/DarkTheme/Other/heartEmpty.png??5{   ]   $??19??6?   ?? 5|   R   5}   O    5~   N   P? 5   R   
 ?@d?	IsChecked$
 TrueY?6o   . ?5?   V   ?? $ checkboxImage??6]    ? ??$1 +../../Icons/DarkTheme/Other/heartFilled.png??6?   5?   S   5?   K   5?   C   5?   ?   5?   7   5?   3   5?   /   5?   +   5?   '    5?   &   P? 5?   *   
 ?@J?IsMouseOver$
 True??6I   . ?5?   .   ??   HoverItemColor# ?  6K    ? ???? 5?   .   $ 
PlayButton??6[    ? y?$ Visible=?6K   6[   ?? 5?   .   $ 
LikeButton??6[    ? y?$ Visible=?6K   6[   5?   +   P? 5?   *   
 ?@??
IsSelected$
 True??6H   . ?5?   .   ?? # ?  6K    ? ???? 5?   .   $ 
PlayButton??6[    ? y?$ Visible=?6K   6[   5?   +   5?   #   5?      5?      5?      5?      