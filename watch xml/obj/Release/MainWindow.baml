   M S B A M L   `   `   `  ���� D  @watch xml, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:watch_xml	watch_xml      watch_xml.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:watch_xml     -     � Title$  
MainWindow��$��374.667��$��1113.821�� � WindowStyle$
 None=�.��� ���	#FF3C3C3C.+��� -    ��Name$ list_name_main��   ListViewStyle1#���   ���	#FF2C2C2C
�������
�������$��314,122,0,0q� � 	Resources  �� 
TargetType �� Property �� Value �� 
TargetType J� SnapsToDevicePixels	 ��Name
 �� CornerRadius ��Name �� Triggers P� Property P� Value��%A  '��       �� " ��.���  � �� �� " ��.R��  . $��0q�.+��� $	 Border��#��z��#��z��#��z��$
 0���� $
 Rows�� P�  �@J�IsMouseOver . . ���  � �� �#3c3c3c��  � �� �#fffP�  �@��
IsSelected . . ���  � �� �#4c4c4c��  � �� �#FFFFFF �� View �� .P���  �� Width$	 100��$��	Files xml�� �� DisplayMemberBinding �� *xml+�� $	 100��$��
Files xml2�� �� *xml2+�� $	 100��$��	Files txt�� �� *txt+�� $ 60��$��Status�� �� *status+�� $	 200��$	��Err�� �� *err+�� $	 100��$��Artist�� �� *artist+�� $	 100��$
��Name�� �� *name+�� $��	File xml:��$
��Left=�$��	38,90,0,0q�$	��Top=�
�������$��26���� -   $
��Left=�$��	15,94,0,0q�$��22��  ControlStyle22 � FocusVisualStyle# �  
�������
�������$��22��$	��Top=�.���� $��img/category.png���� -    ��Name$ label_xmlPath��$�� ��$
��Left=�$��	91,90,0,0q�$	��Top=�
�������$	��218���� $
��Err:��$
��Left=�$��
16,191,0,0q�$	��Top=�
�������$��27���� -   $ 	label_err��$�� ��$
��Left=�$��
43,191,0,0q�$	��Top=�
������� J� RenderTransformOrigin$ 
0.58,1.167&�$	��266���� $��Status:��$
��Left=�$��
16,217,0,0q�$	��Top=�
�������$��44���� -   $ label_status��$�� ��$
��Left=�$��
60,217,0,0q�$	��Top=�
�������$ 
0.58,1.167&�$	��249���� -    ��Name$ 
start_name��$��Start��$
��Left=�$��
15,243,0,0q�$	��Top=�$	��131���� -   $��Delete��$
��Left=�$��154,293,0,0q�$	��Top=�$	��131���� -	   $	��New��$
��Left=�$��
15,268,0,0q�$	��Top=�$	��131���� -
   $ label_header��$��
File xml2:��$
��Left=�$��
38,158,0,0q�$	��Top=�
�������$��24���� -   $ button_header��$
��Left=�$��
15,158,0,0q�$��22��# �  
�������
�������$��24��$	��Top=�.���� $��img/category.png���� -   $ label_xml2Path��$�� ��$
��Left=�$��
97,158,0,0q�$	��Top=�
�������$	��212��$��24���� $��	File txt:��$
��Left=�$��
37,122,0,0q�$	��Top=�
�������$��26���� -   $
��Left=�$��
15,126,0,0q�$��22��# �  
�������
�������$��22��$	��Top=�.���� $��img/category.png���� -   $ label_txtPath��$�� ��$
��Left=�$��
84,122,0,0q�$	��Top=�
�������$	��225���� -   $
��Edit��$
��Left=�$��154,268,0,0q�$	��Top=�$	��131���� -    ��Name$ comboBox_name_category��
�������$��
314,90,0,0q�$��5,0,5,5q� �� SelectedIndex$ 12��  ComboBoxStyle#���  $ 0.511,0.116&�$
��Left=�$	��143�����	#FF494949$��23��$	��Top=�  ComboBoxItemStyle1#���    ItemsPanelTemplate1#���  ���� *bindComboSearch+ �� Mode$ OneWay=�  �� Color��%   (��       *SystemColors.WindowBrushKey+)(��      * SystemColors.HighlightBrushKey+)�� $ #3c3c3c���� $ #3c3c3c���� -   $��Refresh info��$
��Left=�$��154,243,0,0q�$	��Top=�$	��131���� -   
���   �$��85��$	��Top=�����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&� �� RelativeTransform V� .~���  �� CenterY$	 0.5Q� �� CenterX$	 0.5Q���  �� CenterY$	 0.5Q�  �� CenterX$	  0.5Q��� ! �� Angle$	! -90Q�" �� CenterY$	" 0.5Q�# �� CenterX$	# 0.5Q�T� .��� $ � Color$$ 	#FF1EA2D3��� $$ 	#FF41BA8D��% � Offset$% 1Q��� $��	Metadata ��$
��Left=�$��	10,-3,0,0q�$	��Top=�
�������$��34(�$��Tahoma)��� $��Parser��$
��Left=�$��	10,34,0,0q�$	��Top=�
�������$��34(�$��Tahoma)��� $
��MDP ��$��0,21,0,0q�$	��Top=�
�������$��48(�$��Tahoma)�$��64��$��Right=�$	��169��$��
ExtraBlack"��� -   $��Closed��
�������$��14(�$��Segoe UI Semibold)�$��Right=�$��60��$��21��$	��Top=�$��0,0,1,0q���Z� ��Z� �� -   $ 	full_name��$
��Full��
�������$��14(�$��Segoe UI Semibold)�$��Right=�$��62��$��0,0,61,0q�$��21��$	��Top=���Z� ��Z� 