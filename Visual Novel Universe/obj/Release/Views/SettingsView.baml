   M S B A M L   `   `   `  ���� P  LVisual Novel Universe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:Visual_Novel_Universe.ThirdParty Visual_Novel_Universe.ThirdParty  .    (Visual_Novel_Universe.Views.SettingsView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  ?
thirdParty.clr-namespace:Visual_Novel_Universe.ThirdParty    � Title$$  Visual Novel Universe Settings��$	��400��$	��600�� � 
ResizeMode$ NoResize=� � Icon$= 7/Visual Novel Universe;component/Resources/VNULogo2.ico��.��� ���	#FF2E2E2E � ColumnDefinitions �� $��1* ��� $	��180 � � RowDefinitions �� �� $��30 �.+��� -    ��Name$ 
OkayButton��$��OK��$��0��$
��Left=�$��10,0,0,0q�$��1��$��Center=�$��75��$��30��   
MenuButton#��[  �� -   $ CancelButton��$��Cancel��$��0��$
��Left=�$��85,0,0,0q�$��1��$��Center=�$��75��$��30��#��[  �� -   $ ResetButton��$��Reset��$��0��$
��Left=�$��	160,0,0,0q�$��1��$��Center=�$��75��$��30��#��[  � $��0��$��1�� �� $��25 ��� �� $��25 ��� $��25 �.+��� $'��!Strings to remove while importing��$��Center=�$��Center=�$��10(����	#FFCDCDCD�� -   $��FolderCleanupsList��$��1��$ �� IsSynchronizedWithCurrentItem$
 TrueY����	#FF3F3F3F���#C9C9C9���	#00000000$��0q�$��Disabled=����� *FolderCleanups+ �� SelectedItem �� *SelectedFolderCleanup+.  (System.Windows.Interactivity.Interaction   	Behaviors =   7Visual_Novel_Universe.ThirdParty.ScrollIntoViewBehavior  	 � 	Resources	 
 �� 
TargetType �� Property �� Value��%k   '��       �� "
 ��.���  � �� �#2E2E2E��  � �� �#606466��  � �� �#2E2E2E �� View ��   VnGridViewColumnHeader! �� ColumnHeaderContainerStyle# [ .P��� $��Strings to Remove�� �� Width �� *ActualWidth+ �� ElementName$ FolderCleanupsList�� �� Mode$ OneWay=����� "
 ��.���  � �� �#2E2E2E��  � �� �#C9C9C9��  � ��$
 Bold"���  � ��
 ���� ��  � ��$ 2q���  � �� ��  �� 
TargetType" ��.R���  �� CornerRadius$ 8��#��z��#��z��#��z��._��� $��Stretch=�#��z��$
��Auto��$	��0,1q� �� Content# z�� �� Triggers P�  P� Property �@J�IsMouseOver P� Value . . ���  � �� �#2E2E2E��  � ��
 ������  � ��
 ������  � ��$ 2q���  � ��$
 Bold"�P�  �@��
IsSelected . . ���  � ��
 ������  � ��
 �������  � ��
 �������  � ��$ 2q���  � ��$
 Bold"�� $��3�� �� �� .+��� -   $ AddFolderCleanupButton��$	��Add��#��[  �� -   $ RemoveFolderCleanupButton��$��Remove��$��1��#��[  �� -    ��Name$ FolderCleanupTextBox��$��2��  TextBoxStyle#��[ � $��0��$��0��.+��� $��DISPLAY COLUMNS WIP��$��Center=�$��Center=�$
��Left=�$	��Top=�$��14(����	#FFCDCDCD$��	10,10,0,0q�$��35��$	��180���� $��English Name��$
��Left=�$��	30,50,0,0q�$	��Top=����	#FFCDCDCD{�.   d� 	IsChecked �� *ShowEnglishNameChecked+�� $��Japanese Name��$
��Left=�$��	30,70,0,0q�$	��Top=����	#FFCDCDCD{�.   �� *ShowJapaneseNameChecked+�� $��	Developer��$
��Left=�$��	30,90,0,0q�$	��Top=����	#FFCDCDCD{�.   �� *ShowDeveloperChecked+�� $��Length��$
��Left=�$��
30,110,0,0q�$	��Top=����	#FFCDCDCD{�.   �� *ShowLengthChecked+�� $��
Age Rating��$
��Left=�$��
30,130,0,0q�$	��Top=����	#FFCDCDCD{�.   �� *ShowAgeRatingChecked+�� $��SEARCH CATEGORIES��$��Center=�$��Center=�$
��Left=�$	��Top=�$��14(����	#FFCDCDCD$��
10,200,0,0q�$��35��$	��150���� $��Search Titles��$
��Left=�$��
30,244,0,0q�$	��Top=����	#FFCDCDCD �� *SearchTitlesChecked+�� $��Search Developers��$
��Left=�$��
30,264,0,0q�$	��Top=����	#FFCDCDCD �� *SearchDevelopersChecked+�� $��Search Descriptions��$
��Left=�$��
30,284,0,0q�$	��Top=����	#FFCDCDCD �� *SearchDescriptionChecked+�� $��Search Tags��$
��Left=�$��
30,304,0,0q�$	��Top=����	#FFCDCDCD �� *SearchTagsChecked+�� $��HIGHLIGHT COLORS��$��Center=�$��Center=�$
��Left=�$	��Top=�$��14(����	#FFCDCDCD$��
200,10,0,0q�$��35��$	��188���� $
��Left=�$	��Top=�$��50��$��20��$��
260,50,0,0q�
���   ����� *HighlightDefault+�� $
��Left=�$	��Top=�$��50��$��20��$��
260,80,0,0q�
���   ����� *HighlightTitle+�� $
��Left=�$	��Top=�$��50��$��20��$��260,110,0,0q�
���   ����� *HighlightDeveloper+�� $
��Left=�$	��Top=�$��50��$��20��$��260,140,0,0q�
���   ����� *HighlightDescription+�� $
��Left=�$	��Top=�$��50��$��20��$��260,170,0,0q�
���   ����� *HighlightTags+�� -   $ HighlightDefaultText��$
��Left=�$	��Top=�$��75��$��20��$��
320,50,0,0q� �� TextWrapping$
 Wrap=�#��[ �� -	   $ HighlightTitleText��$
��Left=�$	��Top=�$��75��$��20��$��
320,80,0,0q�$
 Wrap=�#��[ �� -
   $ HighlightDeveloperText��$
��Left=�$	��Top=�$��75��$��20��$��320,110,0,0q�$
 Wrap=�#��[ �� -   $ HighlightDescriptionText��$
��Left=�$	��Top=�$��75��$��20��$��320,140,0,0q�$
 Wrap=�#��[ �� -   $ HighlightTagsText��$
��Left=�$	��Top=�$��75��$��20��$��320,170,0,0q�$
 Wrap=�#��[ �� $��DEFAULT��$��Right=�$��Center=�$��Right=�$	��Top=�$��9(����	#FFCDCDCD$��
0,50,160,0q�$��20��$��40��$��0q��� $��TITLE��$��Right=�$��Center=�$��Right=�$	��Top=�$��9(����	#FFCDCDCD$��
0,80,160,0q�$��20��$��30��$��0q��� $��	DEVELOPER��$��Right=�$��Center=�$��Right=�$	��Top=�$��9(����	#FFCDCDCD$��0,110,160,0q�$��20��$��60��$��0q��� $��DESCRIPTION��$��Right=�$��Center=�$��Right=�$	��Top=�$��9(����	#FFCDCDCD$��0,140,160,0q�$��20��$��70��$��0q��� $	��TAG��$��Right=�$��Center=�$��Right=�$	��Top=�$��9(����	#FFCDCDCD$��0,170,160,0q�$��20��$��25��$��0q��� $��MISC OPTIONS��$��Center=�$��Center=�$
��Left=�$	��Top=�$��14(����	#FFCDCDCD$��200,200,0,0q�$��35��$	��188���� $$��Auto Scroll Overwrites VN Info��$
��Left=�$��200,244,0,0q�$	��Top=����	#FFCDCDCD �� *GoToNextOverwritesChecked+