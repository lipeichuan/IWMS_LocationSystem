   M S B A M L   `   `   `  ����N  JIWMS_LocationServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:IWMS_LocationServer.PropertiesIWMS_LocationServer.Properties  5   	   G(clr-namespace:IWMS_LocationServer.src.uiIWMS_LocationServer.src.ui  5   	   .    (IWMS_LocationServer.src.ui.SettingWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <res,clr-namespace:IWMS_LocationServer.Properties     5   	   :local(clr-namespace:IWMS_LocationServer.src.ui     5   	     � Title$  SettingWindow��5	   	   $	��640��6   $	��640��6+    � 
ResizeMode$ NoResize=�69    � WindowStartupLocation$ CenterOwner=�6O    � 	Resources 5
         datagridStyle �� 
TargetType&   System.Windows.Controls.DataGrid �� Property �� Value  buttonStyle��%Q  &	        &	 �     �� 5   
   "  6&   .�5      ��  �@ AutoGenerateColumns .  64   �� 5       �@ HeadersVisibility$
 None=�62   �� 5       �@ GridLinesVisibility$
 None=�64   �� 5       � ��$ 1q�60   5      �� 5   
   " ��6$   .�5      ��  � ��$ 0 0 0 10q�6'   5      5      .��5      ]� .+�5   
   � $	��Top=�6   .+�5      �� $��地图��6   $��
20 20 20 0q�6#   .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5         ��Name$ mapEditButton��5      $	��254��64   $��20, 10q�6@   $��10q�6Q   $��地图设置��6]   6l   5      5      5      � 5   
   	 � ColumnDefinitions	 5      �� 5      �� 5       .+�5"      � $��0��6   .+�5#      �� $��基站��6   $��20,20,10,20q�6'   $��10q�6<   .��5$      ]� .+�5%      �� $	��Top=�6%   $ 
Horizontal=�6:   .+�5&      �� -   5        $ anchorsAddButton��5&   %   $��批量添加��6?   #��[ 6N   6s   �� 5'      -   5        $ anchorClearButton��5'   %   $��清空��6@   #��[ 6M   6r   5(        5)      -   5        
  Name$
 anchorDataGrid��5)   #   #��[  6;      Columns 5*      0  *System.Windows.Controls.DataGridTextColumn  5+   "   .   (IWMS_LocationServer.Properties.Resources   IDS_ANCHOR_SN,  &System.Windows.Controls.DataGridColumn   Header# Z 65      Width5  /System.Windows.Controls.DataGridLengthConverter$
 100* 6e   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6r   �� *Sn+ �� UpdateSourceTrigger$ PropertyChanged=�5-      5.      5/      50      � 51      $��1��6   .+�52      �� $��标签��6   $��10,20,20,20q�6'   $��10q�6<   .��53      ]� .+�54      �� $	��Top=�6%   $ 
Horizontal=�6:   .+�55      �� -   5        $ tagsAddButton��55   %   $��批量添加��6<   #��[ 6K   6p   �� 56      -   5        $ tagClearButton��56   %   $��清空��6=   #��[ 6J   6o   57        58      -   5        $
 tagDataGrid��58   #   #��[  68    59        5:   "      
IDS_TAG_SN# Z 65   $
 100* 6b    6o   �� *Sn+$ PropertyChanged=�5<      5=      5>      5?      5@      5A      5B      