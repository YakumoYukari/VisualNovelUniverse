   M S B A M L   `   `   `  ����  Visual Novel Universe4    .Visual_Novel_Universe.Views.CoverImageGridView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   ^ ZCaliburn.Micro.Platform, Version=3.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  5   	     � Title$  Visual Novel Covers��5	   	   $	��300��6%   $	��300��62   $	��800��6>   $	��600��6M    � Icon$= 7/Visual Novel Universe;component/Resources/VNULogo2.ico��6]    � 
ResizeMode$ CanResizeWithGrip=�6�   .��5
      � .+�5   
   �� ���#2E2E2E$	��Top=�6f   $��Disabled=�6�   ��6   �� *CoverImages+ �� IsAsync .  �� Mode$ OneWay=���5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property �@��
IsSelected �� Value 63   �� *Content.IsSelected+$ TwoWay=� �� RelativeSource �� *Self+�� 5       � �� 5      �� 5      	 �� 
TargetType"	 ��6.   .R�5   "   �� 5      5      5      
 � 	Resources
 5      ��%�   '��       �� 5      " ��6   .�5      ��  � �� �#2E2E2E63   �� 5       � �� �#60646663   �� 5       � �� �#2E2E2E64   5      5      ��5!      �� 5"      .R�5#      A�  K� IsItemsHost . $	��Top=�6Z    A� Columns 65   �� *ColumnsDisplayed+6Z   5$      ��5'      �� 5(      .R�5)      ��  �� CornerRadius$ 8��6   $��5q�6.   ���#4E4E4E$��2q�6�   ��6B   �� *" ParentVisualNovel.FavoritedColor+.  (System.Windows.Interactivity.Interaction   Triggers 5*      /  )System.Windows.Interactivity.EventTrigger  5+         	EventName$ 	MouseDown��6-   .  (System.Windows.Interactivity.TriggerBase   Actions. 5,   "   "  Caliburn.Micro.ActionMessage     
MethodName$ CoverImageClicked��64      
Parameters. 5-   &     Caliburn.Micro.Parameter     Value 64   �� 5.   #   5/      ._�51      �  � RowDefinitions 52      �� 53   "   $��1* �60   �� 54   "   $��25 �60   .+�56      �� ��6$   �� *
Filepath+ . �� 57       ��Name$ EnglishNameText��65   $��1��6(   
�������$��Calibri)�6�   $
��Bold"�6�   $��Stretch=�6�   $��Stretch=�6�   $��CharacterEllipsis=�6�    �� TextAlignment$ Center=�6  $��2,4,2,0q�6.  $��14(�6?  ��6N   �� *ParentVisualNovel.EnglishName+6?  58      59      5:      5<      �� 5=   
   -   5         ��Name$ ZoomOut��5=      $��-��6"   $
��Left=�6/   $��Bottom=�6J   $��25��6e   $��25��6p   ���#2E2E2E���	#FF7163FF$
��Bold"�6�   
������6�   �� 5>   
   -   5        $ ZoomIn��5>      $��+��6!   $
��Left=�6.   $��Bottom=�6I   $��25��6d   $��25��6o   ���#2E2E2E���	#FF7163FF$
��Bold"�6�   
������$��25,0,0,0q�6�   5?      5@      