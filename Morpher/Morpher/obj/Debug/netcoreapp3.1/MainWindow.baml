   M S B A M L   `   `   `  ����B  >Morpher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:MorpherMorpher  5   	       Morpher.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Morpher     5   	     � Title$  
MainWindow��5   	   $	��900��6   $
��1600��6)   $��20(�66   .��5	      ]� .+�5
   
   `� $	��Top=�6   .�5      c� .@�5      �� -   5         ��Name$ btnMorph��5      $��Start Morphing��6+   6D   �� 5      -   5        $ btnEdit��5      $��	Edit Mode��6*   6>   �� 5      -   5        $ btnJpeg��5      $��Compress JPEG��6*   6B   5      5      � 5   
    � ShowGridLines$
 True��6    � ColumnDefinitions 5      �� 5      �� 5      .+�5      �� 
�������$��3q�6(   $��5q�6<   ._�5      �� $��0��6   $��5q�6-   .+�5      �� -   5         ��Name$ canvasL��5      .+�5      �� -   5         ��Name$
 imgL��5       $y�Hidden=�6.   $	��800��6B   $	��700��6R   6c   �� 5      -   5        $��	imgLLabel��5       5      �� 5      -   5        $��btnLoadL��5   U   $��Center=�6   $��Center=�68   $��
Load Image��6e   6z   5      5       �� 5"      
�������$��3q�6(   $��5q�6<   $��1��6G   ._�5#      �� $��5q�6   .+�5$      �� -   5        $ canvasR��5$      .+�5%      �� -	   5        $
 imgR��5%       $y�Hidden=�6.   $	��800��6B   $	��700��6R   6c   �� 5&      -
   5        $��	imgRLabel��5&       5'      �� 5(      -   5        $��btnLoadR��5(   U   $��Center=�6   $��Center=�68   $��
Load Image��6e   6z   5)      5*      5+      5-      5/      