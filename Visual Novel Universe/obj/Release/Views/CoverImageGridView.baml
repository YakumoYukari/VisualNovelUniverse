   M S B A M L   `   `   `  ����   Visual Novel Universe4    .Visual_Novel_Universe.Views.CoverImageGridView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  ^ ZCaliburn.Micro.Platform, Version=3.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org    � Title$  Visual Novel Covers��$	��300��$	��300��$	��800��$	��600�� � Icon$= 7/Visual Novel Universe;component/Resources/VNULogo2.ico�� � 
ResizeMode$ CanResizeWithGrip=�.��� .+��� $��Stretch=�$��Stretch=�$
��Auto=���.  � 	Resources  �� 
TargetType �� Property �� Value��%k   '��       �� " ��.���  � �� �#2E2E2E��  � �� �#606466��  � �� �#2E2E2E.���� ���#2E2E2E���� *CoverImages+���� .R�A�  A� Columns �� *ColumnsDisplayed+���� .R���  �� CornerRadius$ 8��$��5q����#4E4E4E$��2q����� *" ParentVisualNovel.FavoritedColor+.  (System.Windows.Interactivity.Interaction	   Triggers	 /  )System.Windows.Interactivity.EventTrigger  
   	EventName$
 	MouseDown��.  (System.Windows.Interactivity.TriggerBase   Actions. "  Caliburn.Micro.ActionMessage     
MethodName$ CoverImageClicked��   
Parameters.   Caliburn.Micro.Parameter     Value �� ._��  � RowDefinitions �� $��1* ��� $��50 �.+��� ���� *
Filepath+�� $��1��
�������$��0,3,0,0q�._��  �� �� .+���  ��Name$ EnglishNameText��
�������$��Calibri)�$
��Bold"�$��Stretch=�$��Stretch=�$��CharacterEllipsis=� �� TextAlignment$ Center=�$��2,2,2,0q�$��14(����� *ParentVisualNovel.EnglishName+�� $ JapaneseNameText��$��1��
�������$��Calibri)�$
��Bold"�$��Stretch=�$��Stretch=�$��CharacterEllipsis=�$ Center=�$��13(�$	��2,0q����� * ParentVisualNovel.JapaneseName+�� -    ��Name$ ZoomOut��$��-��$
��Left=�$��Bottom=�$��25��$��25�����#2E2E2E���	#FF7163FF$
��Bold"�
�������� -   $ ZoomIn��$��+��$
��Left=�$��Bottom=�$��25��$��25�����#2E2E2E���	#FF7163FF$
��Bold"�
������$��25,0,0,0q�