   M S B A M L   `   `   `  ���� P  LVisual Novel Universe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:Visual_Novel_Universe.ThirdParty Visual_Novel_Universe.ThirdParty  1    +Visual_Novel_Universe.Views.DirectoriesView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ^ ZCaliburn.Micro.Platform, Version=3.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  ?
thirdParty.clr-namespace:Visual_Novel_Universe.ThirdParty    � Title$  Root Directories��$	��300��$	��300��$	��600��$	��300�� � 
ResizeMode$ CanResizeWithGrip=� � Icon$= 7/Visual Novel Universe;component/Resources/VNULogo2.ico��.��� ���	#FF2E2E2E.+��� -    ��Name$ ActiveDirectories��$ �� IsSynchronizedWithCurrentItem$
 TrueY�$��10,10,10,40q����	#FF3F3F3F���#C9C9C9���	#00000000$��0q�$��Disabled=����� *ActiveDirectories+ �� SelectedItem �� *SelectedItem+.  (System.Windows.Interactivity.Interaction   	Behaviors =   7Visual_Novel_Universe.ThirdParty.ScrollIntoViewBehavior   � 	Resources  �� 
TargetType	 �� Property
 �� Value��%k   '��       �� " ��.��� 	 � ��
 �#2E2E2E�� 	 � ��
 �#606466�� 	 � ��
 �#2E2E2E �� View ��    VnGridViewColumnHeader! �� ColumnHeaderContainerStyle# [  .P��� $��	Directory�� �� Width �� *ActualWidth+ �� ElementName$ ActiveDirectories�� �� Mode$ OneWay=����� " ��.��� 	 � ��
 �#2E2E2E�� 	 � ��
 �#C9C9C9�� 	 � ��$

 Bold"��� 	 � ��

 ���� �� 	 � ��$
 2q��� 	 � ��
 ��  �� 
TargetType" ��.R���  �� CornerRadius$ 8��#��z��#��z��#��z��._��� $��Stretch=�#��z��$
��Auto��$	��0,1q� �� Content# z�� �� Triggers P�  P� Property �@J�IsMouseOver P� Value . . ��� 	 � ��
 �#2E2E2E�� 	 � ��

 ������ 	 � ��

 ������ 	 � ��$
 2q��� 	 � ��$

 Bold"�P�  �@��
IsSelected . . ��� 	 � ��

 ������ 	 � ��

 ������� 	 � ��

 ������� 	 � ��$
 2q��� 	 � ��$

 Bold"��� -    ��Name$	 Add��  
MenuButton#��[ $	��Add��$
��Left=�$��	10,0,0,10q�$��Bottom=�$��75��$��25���� -   $ Remove��#��[ $��Remove��$
��Left=�$��	90,0,0,10q�$��Bottom=�$��75��$��25���� -   $ Apply��  Caliburn.Micro.Message   Attach$F @[Event MouseEnter] = [Action RootDirectoriesApply($dataContext)]��#��[ $��Apply��$��Right=�$��	0,0,10,10q�$��Bottom=�$��75��$��25��