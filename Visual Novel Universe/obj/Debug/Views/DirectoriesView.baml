   M S B A M L   `   `   `  ����P  LVisual Novel Universe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:Visual_Novel_Universe.ThirdParty Visual_Novel_Universe.ThirdParty  5   	   1    +Visual_Novel_Universe.Views.DirectoriesView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ^ ZCaliburn.Micro.Platform, Version=3.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   ?
thirdParty.clr-namespace:Visual_Novel_Universe.ThirdParty  5   	     � Title$  Root Directories��5
   	   $	��300��6"   $	��300��6/   $	��600��6;   $	��300��6J    � 
ResizeMode$ CanResizeWithGrip=�6Z    � Icon$= 7/Visual Novel Universe;component/Resources/VNULogo2.ico��6y   .��5      � ���	#FF2E2E2E.+�5   
   �� -   5         ��Name$ ActiveDirectories��5      $ �� IsSynchronizedWithCurrentItem$
 TrueY�6   $��10,10,10,40q�6�   ���	#FF3F3F3F���#C9C9C9���	#00000000$��0q�6�   $��Disabled=�6  ��6.   �� *ActiveDirectories+ �� SelectedItem 6X   �� *SelectedItem+.  (System.Windows.Interactivity.Interaction   	Behaviors 5      =   7Visual_Novel_Universe.ThirdParty.ScrollIntoViewBehavior  5       � 	Resources 5       �� 
TargetType	 �� Property
 �� Value��%�   '��       �� 5      " ��6   .�5      �� 	 � ��
 �#2E2E2E63   �� 5      	 � ��
 �#60646663   �� 5      	 � ��
 �#2E2E2E64   5      5       �� View 5      �� 5         VnGridViewColumnHeader! �� ColumnHeaderContainerStyle# [  6   .P�5      �� $��	Directory��6%    �� Width 68   �� *ActualWidth+ �� ElementName$ ActiveDirectories�� �� Mode$ OneWay=�5      ��5      �� 5      " ��6   .�5      �� 	 � ��
 �#2E2E2E63   �� 5      	 � ��
 �#C9C9C963   �� 5       	 � ��$

 Bold"�63   �� 5!      	 � ��

 ���� 64   �� 5"      	 � ��$
 2q�68   �� 5#      	 � ��
 5$      �� 5%       �� 
TargetType" ��6.   .R�5&   "   ��  �� CornerRadius$ 8��6)   #��z��6:   #��z��6g   #��z��6�   ._�5'   &   �� $��Stretch=�6;   #��z��6Y   $
��Auto��6�   $	��0,1q�6�    �� Content# z��6�   5(   #   5)      5+       �� Triggers 5,      P� 5-       P� Property �@J�IsMouseOver P� Value . . �5.      �� 	 � ��
 �#2E2E2E6;   �� 5/      	 � ��

 ����6;   �� 50      	 � ��

 ����6<   �� 51      	 � ��$
 2q�6@   �� 52      	 � ��$

 Bold"�6;   53      P� 54       �@��
IsSelected . . �55      �� 	 � ��

 ����6;   �� 56      	 � ��

 �����6;   �� 57      	 � ��

 �����6<   �� 58      	 � ��$
 2q�6@   �� 59      	 � ��$

 Bold"�6;   5:      5<      5>      �� 5?   
   -   5         ��Name$	 Add��5?        
MenuButton#��[ 6   $	��Add��6B   $
��Left=�6P   $��	10,0,0,10q�6k   $��Bottom=�6~   $��75��6�   $��25��6�   �� 5@   
   -   5        $ Remove��5@      #��[ 6!   $��Remove��6E   $
��Left=�6V   $��	90,0,0,10q�6q   $��Bottom=�6�   $��75��6�   $��25��6�   �� 5A   
   -   5        $ Apply��5A   g     Caliburn.Micro.Message   Attach$F @[Event MouseEnter] = [Action RootDirectoriesApply($dataContext)]��6   #��[ 6v   $��Apply��6�   $��Right=�6�   $��	0,0,10,10q�6�   $��Bottom=�6�   $��75��6�   $��25��6�   5B      5C      