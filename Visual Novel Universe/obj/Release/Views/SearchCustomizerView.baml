   M S B A M L   `   `   `  ���� P  LVisual Novel Universe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:Visual_Novel_Universe.ThirdParty Visual_Novel_Universe.ThirdParty  6    0Visual_Novel_Universe.Views.SearchCustomizerView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  ?
thirdParty.clr-namespace:Visual_Novel_Universe.ThirdParty    � Title$  Customize Searches��$	��300��$	��300��$	��600��$	��300�� � 
ResizeMode$ CanResizeWithGrip=� � Icon$= 7/Visual Novel Universe;component/Resources/VNULogo2.ico��.��� ���	#FF2E2E2E � RowDefinitions �� $��1* ��� $��30 ��� $��30 �.+��� -    ��Name$ ActiveDirectories��$ �� IsSynchronizedWithCurrentItem$
 TrueY�$��10,10,10,10q����	#FF3F3F3F���#C9C9C9���	#00000000$��0q�$��Disabled=� J� RenderTransformOrigin$ 0.5,0.5&����� *SearchEntries+ �� SelectedItem �� *SelectedItem+.  (System.Windows.Interactivity.Interaction   	Behaviors =   7Visual_Novel_Universe.ThirdParty.ScrollIntoViewBehavior  	 � 	Resources	 
 �� 
TargetType �� Property �� Value��%k   '��       �� "
 ��.���  � �� �#2E2E2E��  � �� �#606466��  � �� �#2E2E2E �� View ��    VnGridViewColumnHeader! �� ColumnHeaderContainerStyle# [  .P��� $
��Name�� �� Width$	 200�� �� DisplayMemberBinding �� *Name+�� $	��URL��$	 375�� �� *Url+���� "
 ��.���  � �� �#2E2E2E��  � �� �#C9C9C9��  � ��$
 Bold"���  � ��
 ���� ��  � ��$ 2q���  � �� ��  �� 
TargetType" ��.R���  �� CornerRadius$ 8��#��z��#��z��#��z��._��� $��Stretch=�#��z��$
��Auto��$	��0,1q� �� Content# z�� �� Triggers P�  P� Property �@J�IsMouseOver P� Value . . ���  � �� �#2E2E2E��  � ��
 ������  � ��
 ������  � ��$ 2q���  � ��$
 Bold"�P�  �@��
IsSelected . . ���  � ��
 ������  � ��
 �������  � ��
 �������  � ��$ 2q���  � ��$
 Bold"��� -    ��Name$	 Add��  
MenuButton#��[ $	��Add��$
��Left=�$��10,0,0,0q�$��Center=�$��75��$��25��$��2���� -   $ Update��#��[ $��Update��$
��Left=�$��90,0,0,0q�$��Center=�$��75��$��25��$��2���� -   $ Remove��#��[ $��Remove��$
��Left=�$��	170,0,0,0q�$��Center=�$��75��$��25��$��2���� -   $ Apply��#��[ $��Apply��$��Right=�$��0,0,10,0q�$��Center=�$��75��$��25��$��2��� $��1�� � ColumnDefinitions �� $��10 ��� $��50 ��� $��2* ��� $��50 ��� $��4* ��� $��10 �.+��� $
��Name�����#C9C9C9$��Center=�$��Center=�$��1���� $	��URL�����#C9C9C9$��Center=�$��Center=�$��3���� -    ��Name$ NameText��$��2��  TextBoxStyle#��[ �� -   $ UrlText��$��4��#��[ �� $8��2Replace your search term with {0} when adding URLs��$��2��$
��Left=�$��Center=�$��10(����	#FFCDCDCD$��	260,0,0,0q�