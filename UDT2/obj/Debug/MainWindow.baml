   M S B A M L   `   `   `  ����J  FComptuerDetails, Version=2.1.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:ComptuerDetailsComptuerDetails  5   	   P LLoadingIndicators.WPF, Version=0.0.1.0, Culture=neutral, PublicKeyToken=null\Bclr-namespace:LoadingIndicators.WPF;assembly=LoadingIndicators.WPFLoadingIndicators.WPF 5   	        ComptuerDetails.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:ComptuerDetails     5   	   QliBclr-namespace:LoadingIndicators.WPF;assembly=LoadingIndicators.WPF     5   	   -   5          �Name$  MainPane��5   	    � WindowStartupLocation$ CenterScreen=�6@    � Title$ UCLan Drive Tool 2.1.0��5	   	   $	��600��6(   $
��1280��65    � 
ResizeMode$ NoResize=�6B    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $T Npack://application:,,,/LoadingIndicators.WPF;component/Styles/LoadingWave.xaml?�6%   �� 5      $Y Spack://application:,,,/LoadingIndicators.WPF;component/Styles/LoadingThreeDots.xaml?�6%   �� 5      $Y Spack://application:,,,/LoadingIndicators.WPF;component/Styles/LoadingFlipPlane.xaml?�6%   �� 5      $U Opack://application:,,,/LoadingIndicators.WPF;component/Styles/LoadingPulse.xaml?�6%   �� 5      $\ Vpack://application:,,,/LoadingIndicators.WPF;component/Styles/LoadingDoubleBounce.xaml?�6%   �� 5      $H Bpack://application:,,,/LoadingIndicators.WPF;component/Colors.xaml?�6%   �� 5      $H Bpack://application:,,,/LoadingIndicators.WPF;component/Styles.xaml?�6%   %    5      .��5      � -   5         �Name$ 
mainLayout��5       � RowDefinitions 5   
   �� 5      $
��166* �6   6,   �� 5      $
��355* �6   6,   �� 5      $	��50* �6   6+   	 � ColumnDefinitions	 5    
   �� 5!      6!   �� 5"      6!   �� 5#      6!   .+�5&   
   �� ���	#FFE4E4E4$��Stretch=�6%   $��1��6C   $��0��6P   $��2��6`   $��3��6q   $��Stretch=�6�   ,  &LoadingIndicators.WPF.LoadingIndicator  5(   
   -   5        
  Name$	
 LI1��5(      $��0��6+   $��1��6;      
SpeedRatio$ 1Q�6H      IsActive .    LoadingIndicatorArcsStyle#���   6g    J� RenderTransformOrigin$ 0.5,0.5&�6�   z�5)      �� 5*       �� ScaleX$
 5.55Q�6!    �� ScaleY$
 5.55Q�6/   5,      �� 5.   
   -   5         ��Name$ phaseOneImg��5.      $��Center=�6%   $��Center=�6B   $��0��6]   $��1��6m   $��Resources/tick.png��6z   �� 50   
   -   5         ��Name$ phaseOneLabel��50      $��1. Internet Connection��6'   $��Center=�6H   $��Bottom=�6e   $��0��6�   $��1��6�   $��20(�6�   6�     52   
   -   5        $	
 LI2��52      $��1��6+   $��1��6;   $ 1Q�6H    . #���   6g   $ 0.5,0.5&�6�   z�53      �� 54      $
 5.55Q�6!   $
 5.55Q�6/   56      �� 58   
   -   5        $ phaseTwoImg��58      $��Center=�6%   $��Center=�6B   $��1��6]   $��1��6m   $��Resources/tick.png��6z   �� 5:   
   -   5        $ phaseTwoLabel��5:      $��2. UCLan Services��6'   $��Center=�6C   $��Bottom=�6`   $��1��6{   $��1��6�   $��20(�6�   6�     5<   
   -	   5        $	
 LI3��5<      $��2��6+   $��1��6;   $ 1Q�6H    . #���   6g   $ 0.5,0.5&�6�   z�5=      �� 5>      $
 5.55Q�6!   $
 5.55Q�6/   5@      �� 5B   
   -
   5        $ phaseThreeImg��5B      $��Center=�6'   $��Center=�6D   $��2��6_   $��1��6o   $��Resources/tick.png��6|   �� 5D   
   -   5        $ phaseThreeLabel��5D      $��3. Network Drives��6)   $��Center=�6E   $��Bottom=�6b   $��2��6}   $��1��6�   $��20(�6�   6�   �� 5F   
   -   5        $ statusLabel��5F      $��Ready...��6%   $��Stretch=�68   $��2��6V   $��0��6c   $��3��6s   $��6q�6�   $��Stretch=�6�   
���   �
���   �$��1q�6�   $��16(�6�   
�������6�   �� 5G   
   -   5        $ logoImg��5G      $��0��6!   $��0��61   $��Center=�6>   $��Center=�6[   $��Resources/2019logo.png��6v    �� BitmapScalingMode$ HighQuality=�6�   $��10q�6�   � 5I   
   $��0��6   $��1��6   $��Stretch=�6,   $��Stretch=�6J    5K      �� 5L      6"   �� 5M      6"   �� 5N      6"   �� 5O      6"   �� 5P      6"   �� 5Q      6"   �� 5R      6"   	 5U      �� 5V      $
��118* �6#   62   �� 5W      $
��309* �6#   62   .+�5Z      �� -   5        $ usrLabel��5Z      $��0��6&   $��0��66   $��	Username:��6C   $��Right=�6W   $
��Bold"�6s   $��-2q�6�   6�   �� 5[      -   5        $ usrVal��5[      $��1��6$   $��0��64   $��DefaultUser��6A   $
��Left=�6W   $��-2q�6y   6�   �� 5\      -   5        $ cnLabel��5\      $��0��6%   $��1��65   $��Computer Name:��6B   $��Right=�6[   $
��Bold"�6w   $��-2q�6�   �� 5]      -   5        $ cnValue��5]      $��1��6%   $��1��65   $��TEST-PC12345678912345978��6B   $
��Left=�6e   $��-2q�6�   6�   �� 5^      -   5        $ 	uuidLabel��5^      $��0��6'   $��2��67   $��UUID:��6D   $��Right=�6T   $
��Bold"�6p   $��-2q�6�   6�   �� 5_      -   5        $ uuidVal��5_      $��1��6%   $��2��65   $!��0000:0000:000000000000:0000��6B   $
��Left=�6h   $��-2q�6�   6�   �� 5`      -   5        $ osLabel��5`      $��0��6%   $��3��65   $��Operating System:��6B   $��Right=�6^   $
��Bold"�6z   $��-2q�6�   6�   �� 5a      -   5        $ osVal��5a      $��1��6#   $��3��63   $ ��Windows 10 Enterprise 1909��6@   $��-2q�6e   6s   �� 5b      -   5        $ officeLabel��5b      $��0��6)   $��4��69   $��Office 365 Version:��6F   $��Right=�6d   $
��Bold"�6�   $��-2q�6�   6�   �� 5c      -   5        $ 	officeVal��5c      $��1��6'   $��4��67   $��16.123456789456��6D   $
��Left=�6^   $��-2q�6�   6�   �� 5d      -   5        $ macLabel��5d      $��0��6&   $��5��66   $��Mac Address:��6C   $��Right=�6Z   $
��Bold"�6v   $��-2q�6�   6�   �� 5e      -   5        $ macVal��5e      $��1��6$   $��5��64   $��00:00:00:00:00:00��6A   $
��Left=�6]   $��-2q�6x   6�   �� 5f      -   5        $ ipLabel��5f      $��0��6%   $��6��65   $��IP Address:��6B   $��Right=�6X   $
��Bold"�6t   $��-2q�6�   6�   �� 5g      -   5        $ ipVal��5g      $��1��6#   $��6��63   $��0.0.0.0��6@   $
��Left=�6R   $��-2q�6m   6{   5i      � 5k   
   -   5         �Name$ btnWP��5k      $��0��6#   $��2��60   $��Right=�6@   $��Bottom=�6\   $��RightToLeft=�6w    � Orientation$ 
Horizontal=�6�   $��5q�6�   .+�5l      �� -   5         ��Name$ helpBtn��5l      $
��Help��6&   $	��166��65   $��46��6A   $��3q�6M   ���	#FFE5E5E5
���   �$��2q�6�   $
��Bold"�6�   $��20(�6�   6�   �� 5m      -   5        $ padding��5m      $�� ��6%   $	��166��60   $��46��6<   $��3q�6H   6U   �� 5n      -   5        $ exiBtn��5n      $
��Exit��6%   $	��166��64   $��46��6@   $��3q�6L   ���	#FFE5E5E5
���   �$��2q�6�   $
��Bold"�6�   $��20(�6�   6�   �� 5o      -    5        $ chkBtn��5o      $��Re-Check��6%   $	��166��68   $��46��6D   $��3q�6P   ���	#FFE5E5E5
���   �$��2q�6�   $
��Bold"�6�   $��20(�6�   6�   5p      5r      5s      