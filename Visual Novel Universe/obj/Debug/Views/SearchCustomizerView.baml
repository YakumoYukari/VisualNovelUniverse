   M S B A M L   `   `   `  ����P  LVisual Novel Universe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:Visual_Novel_Universe.ThirdParty Visual_Novel_Universe.ThirdParty  5   	   6    0Visual_Novel_Universe.Views.SearchCustomizerView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   ?
thirdParty.clr-namespace:Visual_Novel_Universe.ThirdParty  5   	     � Title$  Customize Searches��5	   	   $	��300��6$   $	��300��61   $	��600��6=   $	��300��6L    � 
ResizeMode$ CanResizeWithGrip=�6\    � Icon$= 7/Visual Novel Universe;component/Resources/VNULogo2.ico��6{   .��5
      � ���	#FF2E2E2E � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��30 �6   �� 5      $��30 �6   .+�5   
   �� -   5         ��Name$ ActiveDirectories��5      $ �� IsSynchronizedWithCurrentItem$
 TrueY�6{   $��10,10,10,10q�6�   ���	#FF3F3F3F���#C9C9C9���	#00000000$��0q�6�   $��Disabled=�6   J� RenderTransformOrigin$ 0.5,0.5&�6C  ��6.   �� *SearchEntries+ �� SelectedItem 6T   �� *SelectedItem+.  (System.Windows.Interactivity.Interaction   	Behaviors 5      =   7Visual_Novel_Universe.ThirdParty.ScrollIntoViewBehavior  5      	 � 	Resources	 5      
 �� 
TargetType �� Property �� Value��%�   '��       �� 5      "
 ��6   .�5      ��  � �� �#2E2E2E63   �� 5       � �� �#60646663   �� 5       � �� �#2E2E2E64   5      5       �� View 5      �� 5         VnGridViewColumnHeader! �� ColumnHeaderContainerStyle# [  6   .P�5      �� $
��Name��6%    �� Width$	 200��6Y    �� DisplayMemberBinding 63   �� *Name+6Y   �� 5      $	��URL��6%   $	 375��6W    62   �� *Url+6W   5      ��5!      �� 5"      "
 ��6   .�5#      ��  � �� �#2E2E2E63   �� 5$       � �� �#C9C9C963   �� 5%       � ��$
 Bold"�63   �� 5&       � ��
 ���� 64   �� 5'       � ��$ 2q�68   �� 5(       � �� 5)      �� 5*       �� 
TargetType" ��6.   .R�5+   "   ��  �� CornerRadius$ 8��6)   #��z��6:   #��z��6g   #��z��6�   ._�5,   &   �� $��Stretch=�6;   #��z��6Y   $
��Auto��6�   $	��0,1q�6�    �� Content# z��6�   5-   #   5.      50       �� Triggers 51      P� 52       P� Property �@J�IsMouseOver P� Value . . �53      ��  � �� �#2E2E2E6;   �� 54       � ��
 ����6;   �� 55       � ��
 ����6<   �� 56       � ��$ 2q�6@   �� 57       � ��$
 Bold"�6;   58      P� 59       �@��
IsSelected . . �5:      ��  � ��
 ����6;   �� 5;       � ��
 �����6;   �� 5<       � ��
 �����6<   �� 5=       � ��$ 2q�6@   �� 5>       � ��$
 Bold"�6;   5?      5A      5C      �� 5D   
   -   5         ��Name$	 Add��5D        
MenuButton#��[ 6   $	��Add��6B   $
��Left=�6P   $��10,0,0,0q�6k   $��Center=�6}   $��75��6�   $��25��6�   $��2��6�   �� 5E   
   -   5        $ Update��5E      #��[ 6!   $��Update��6E   $
��Left=�6V   $��90,0,0,0q�6q   $��Center=�6�   $��75��6�   $��25��6�   $��2��6�   �� 5F   
   -   5        $ Remove��5F      #��[ 6!   $��Remove��6E   $
��Left=�6V   $��	170,0,0,0q�6q   $��Center=�6�   $��75��6�   $��25��6�   $��2��6�   �� 5G   
   -   5        $ Apply��5G      #��[ 6    $��Apply��6D   $��Right=�6T   $��0,0,10,0q�6p   $��Center=�6�   $��75��6�   $��25��6�   $��2��6�   � 5H   
   $��1��6    � ColumnDefinitions 5I      �� 5J      $��10 �6#   �� 5K      $��50 �6#   �� 5L      $��2* �6#   �� 5M      $��50 �6#   �� 5N      $��4* �6#   �� 5O      $��10 �6#   .+�5Q      �� $
��Name��6   ���#C9C9C9$��Center=�68   $��Center=�6U   $��1��6p   �� 5R      $	��URL��6   ���#C9C9C9$��Center=�67   $��Center=�6T   $��3��6o   �� 5S      -   5         ��Name$ NameText��5S      $��2��6(     TextBoxStyle#��[ 68   �� 5T      -   5        $ UrlText��5T      $��4��6'   #��[ 67   5U      �� 5V   
   $8��2Replace your search term with {0} when adding URLs��6   $��2��6M   $
��Left=�6Z   $��Center=�6u   $��10(�6�   ���	#FFCDCDCD$��	260,0,0,0q�6�   5W      5X      