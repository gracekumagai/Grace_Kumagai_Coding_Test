   M S B A M L   `   `   `  ����d  `Microsoft.Wpf.Interop.DirectX, Version=0.9.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35tRclr-namespace:Microsoft.Wpf.Interop.DirectX;assembly=Microsoft.Wpf.Interop.DirectXMicrosoft.Wpf.Interop.DirectX  5   	    WpfD3D11Interop!   Wpf.D3D11Interop.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   adxRclr-namespace:Microsoft.Wpf.Interop.DirectX;assembly=Microsoft.Wpf.Interop.DirectX     5   	   -   5          � Title$  Grace Kumagai��5   	   $	��540��6   $	��900��6,    � 	Resources 5         MediumGreyBrush �� Color  KinectPurpleBrush % !HorizontalSliderTrackNormalBorder �� EndPoint �� 
StartPoint � Color � Offset  KinectSlider �� 
TargetType �� Property	 �� Value
 �� 
TargetType �� CornerRadius ]�Name ]� Thumb��%�  &	        &	 #     &	 F     &	 �     7  �� 5	   
   $ 	#ff4e4e4e��62   �� 5
   
   $ 	#ff52318f��64   �� 5   
   $	 0,1&�6H   $	 0,0&�6W   .��5      � $ 	#FFAEB1AF��6   $	 0.1Q�6-   � 5      $ White��6   $ .9Q�6)   5      �� 5   
   " ��5      .�5      ��  �@��Orientation$	 
Horizontal=�6,   �� 5       � ��	 5      �� 5      "
 ��6&   .R�5      � .+�5      �� ���	#FFE7EAEA8��  $��1q�6}   $ 1��6�   $	��5,0q�6�   $	��4.0��6�   $��1��6�   ]� 5      $ 
PART_Track��69   $y�Visible=�6$    5   "   n� 5   &   ��5   *   �� 5   .   .R�5   2   �� $��Images/Slider.png��68   5   /   5!   '   5#      5$      5%      5'      5(      5)      .��5,      � $��
10,0,10,10q�6    � RowDefinitions 5-   
   �� 5.      $��62 �6   �� 5/      $��* �6   .+�51   
   � $��1��6    � ColumnDefinitions 52      �� 53      $��* �6#   �� 54      $	��250 �6#   .+�57      � $��1��6   }�. .+�58      �� -   5        $��ControlHostElement��58      #��[ 59      $��2q�5:      $��Stretch=�6&   $��Stretch=�6D   ._�5=      � -   5         �Name$
 host��5=       � Cursor$
 None��6)   .+�5>      �� $��0��6$   
���   �64   �� 5?      -   5        $��Backdrop��5?       $��Images\Background1.jpg��61   �� 5@      -   5        $��	ImageHost��5@   /   $
��Fill=�6    ��5A      .   (Microsoft.Wpf.Interop.DirectX.D3D11Image  5B   "   -   5        InteropImage��5B   0   5D      5E      5F      �� 5I      -   5         ��Name$ CurserToggle��5I      $
 None��6/   5J      5K      �� 5N      $��1��6   #��[  6(   $��10,0,0,0q�6P   �� 5O      $��1��6   .+�5P      � $��20,10,10,10q�6    5Q      �� 5R      $��35 �6(   �� 5S      $��* �6(   �� 5T      $��15 �6(   �� 5U      $��25 �6(   �� 5V      $��15 �6(   �� 5W      $��25 �6(   �� 5X      $��50 �6(   �� 5Y      $��35 �6(   �� 5Z      $��25 �6(   �� 5[      $��15 �6(   �� 5\      $��25 �6(   �� 5]      $��15 �6(   �� 5^      $��25 �6(   .+�5b      �� $��7��6    $��Camera Position��6-   $
��Left=�6D   $��20(�6_   $��Segeo UI)�6m   
�������6�   �� 5c      $��8��6   $��_Radius��6)   $��Center=�6;   $
��Left=�6V   $��12(�6q   $��Segeo UI)�6   
������� �� Target 6�   ��  �� ElementName$ RadiusSlider���� 5d      -   5         ��Name$ RadiusSlider��5d   *   $��8��6   #��[ 6@    � Minimum$ .1Q�6�    � Value$ 2Q�6�    � Maximum$ 5Q�6�   $��	50,0,30,0q�6�    � DataContext 6�   �� �� 5e      $��10��6   $��_Theta��6*   $��Center=�6<   $
��Left=�6W   $��12(�6r   $��Segeo UI)�6�   
������� 6�   �� $ ThetaSlider���� 5f      -	   5        $ ThetaSlider��5f   +   $��10��6   #��[ 6A   $
 -180Q�6�   $	 180Q�6�   $ 0Q�6�   $��	50,0,30,0q�6�   �� 5g      $��12��6   $
��_Phi��6*   $��Center=�69   $
��Left=�6T   $��12(�6o   $��Segeo UI)�6}   
������� 6�   �� $ 	PhiSlider���� 5h      -
   5        $ 	PhiSlider��5h   +   $��12��6   #��[ 6>   $ -89.9Q�6�   $
 89.9Q�6�   $ 0Q�6�   $��	50,0,30,0q�6�   5i      5j      �� 5m      -   5         ��Name$ comboBox��5m      $��1��6)   $��Center=�69   $��65.667,27,0,0q�6V   $	��Top=�6m   $	��120��6�   .@�5n      �� -   5        $
��Save��5n      .��6+   Save61   �� 5o      -   5        $
��Load��5o      .��6+   Load61   �� 5p      -   5        $
��Exit��5p      .��6+   Exit61   5q      �� 5t      -   5         ��Name$ button��5t      $��Render��6%   $��1��66   $��Center=�6F   $��142.667,71,32.333,0q�6c   $	��Top=�6�   $��75��6�   6�   5v      �� 5w   
   $"��Coding Test: WPF and DirectX��6   $
��Left=�68   $��Bottom=�6S   $��0,0,0,17q�6n   $��14(�6�   #��[  6�   $��Segoe UI)�6�   5x      5y      