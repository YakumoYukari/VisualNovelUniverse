   M S B A M L   `   `   `  ����P  LVisual Novel Universe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:Visual_Novel_Universe.ThirdParty Visual_Novel_Universe.ThirdParty  5   	   .    (Visual_Novel_Universe.Views.SettingsView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   ?
thirdParty.clr-namespace:Visual_Novel_Universe.ThirdParty  5   	     � Title$$  Visual Novel Universe Settings��5	   	   $	��400��60   $	��600��6=    � 
ResizeMode$ NoResize=�6I    � Icon$= 7/Visual Novel Universe;component/Resources/VNULogo2.ico��6_   .��5
      � ���	#FF2E2E2E � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $	��180 �6    � RowDefinitions 5   
   �� 5      �� 5      $��30 �6   .+�5   
   �� -   5         ��Name$ 
OkayButton��5      $��OK��6%   $��0��62   $
��Left=�6B   $��10,0,0,0q�6]   $��1��6o   $��Center=�6|   $��75��6�   $��30��6�      
MenuButton#��[  6�   �� 5   
   -   5        $ CancelButton��5      $��Cancel��6'   $��0��68   $
��Left=�6H   $��85,0,0,0q�6c   $��1��6u   $��Center=�6�   $��75��6�   $��30��6�   #��[  6�   �� 5   
   -   5        $ ResetButton��5      $��Reset��6&   $��0��66   $
��Left=�6F   $��	160,0,0,0q�6a   $��1��6t   $��Center=�6�   $��75��6�   $��30��6�   #��[  6�   � 5   
   $��0��6   $��1��6    5      �� 5      $��25 �6    �� 5      �� 5      $��25 �6    �� 5      $��25 �6    .+�5      �� $'��!Strings to remove while importing��6   $��Center=�6@   $��Center=�6]   $��10(�6x   ���	#FFCDCDCD6�   �� 5      -   5        $��FolderCleanupsList��5      $��1��61   $ �� IsSynchronizedWithCurrentItem$
 TrueY�6�   ���	#FF3F3F3F���#C9C9C9���	#00000000$��0q�6�   $��Disabled=�6  ��6>   �� *FolderCleanups+ �� SelectedItem 6e   �� *SelectedFolderCleanup+.  (System.Windows.Interactivity.Interaction   	Behaviors 5      =   7Visual_Novel_Universe.ThirdParty.ScrollIntoViewBehavior  5       	 � 	Resources	 5"      
 �� 
TargetType �� Property �� Value��%�   '��       �� 5#      "
 ��6   .�5$      ��  � �� �#2E2E2E67   �� 5%       � �� �#60646667   �� 5&       � �� �#2E2E2E68   5'      5(       �� View 5)      �� 5*        VnGridViewColumnHeader! �� ColumnHeaderContainerStyle# [ 6   .P�5+      �� $��Strings to Remove��6)    �� Width 6D   �� *ActualWidth+ �� ElementName$ FolderCleanupsList�� �� Mode$ OneWay=�5,      ��5.      �� 5/      "
 ��6   .�50      ��  � �� �#2E2E2E67   �� 51       � �� �#C9C9C967   �� 52       � ��$
 Bold"�67   �� 53       � ��
 ���� 68   �� 54       � ��$ 2q�6<   �� 55       � �� 56      �� 57   "    �� 
TargetType" ��62   .R�58   &   ��  �� CornerRadius$ 8��6-   #��z��6>   #��z��6k   #��z��6�   ._�59   *   �� $��Stretch=�6?   #��z��6]   $
��Auto��6�   $	��0,1q�6�    �� Content# z��6�   5:   '   5;   #   5=       �� Triggers 5>      P� 5?       P� Property �@J�IsMouseOver P� Value . . �5@   "   ��  � �� �#2E2E2E6?   �� 5A   "    � ��
 ����6?   �� 5B   "    � ��
 ����6@   �� 5C   "    � ��$ 2q�6D   �� 5D   "    � ��$
 Bold"�6?   5E      P� 5F       �@��
IsSelected . . �5G   "   ��  � ��
 ����6?   �� 5H   "    � ��
 �����6?   �� 5I   "    � ��
 �����6@   �� 5J   "    � ��$ 2q�6D   �� 5K   "    � ��$
 Bold"�6?   5L      5N      5P      � 5Q      $��3��6    5R      �� 5S      �� 5T      .+�5V      �� -   5        $ AddFolderCleanupButton��5V      $	��Add��69   #��[  6G   �� 5W      -   5        $ RemoveFolderCleanupButton��5W      $��Remove��6<   $��1��6M   #��[  6]   5X      �� 5Y      -   5         ��Name$ FolderCleanupTextBox��5Y      $��2��64     TextBoxStyle#��[ 6A   5Z      � 5[   
   $��0��6   $��0��6   .+�5]      �� $��DISPLAY COLUMNS WIP��6   $��Center=�62   $��Center=�6V   $
��Left=�6x   $	��Top=�6�   $��14(�6�   ���	#FFCDCDCD$��	10,10,0,0q�6�   $��35��6�   $	��180��6�   �� 5_      $��English Name��6D   $
��Left=�6[   $��	30,50,0,0q�6v   $	��Top=�6�   ���	#FFCDCDCD{�.   d� 	IsChecked 6   �� *ShowEnglishNameChecked+6�   �� 5`      $��Japanese Name��6E   $
��Left=�6]   $��	30,70,0,0q�6x   $	��Top=�6�   ���	#FFCDCDCD{�.   6   �� *ShowJapaneseNameChecked+6�   �� 5a      $��	Developer��6B   $
��Left=�6V   $��	30,90,0,0q�6q   $	��Top=�6�   ���	#FFCDCDCD{�.   6   �� *ShowDeveloperChecked+6�   �� 5b      $��Length��6?   $
��Left=�6P   $��
30,110,0,0q�6k   $	��Top=�6   ���	#FFCDCDCD{�.   6   �� *ShowLengthChecked+6�   �� 5c      $��
Age Rating��6B   $
��Left=�6W   $��
30,130,0,0q�6r   $	��Top=�6�   ���	#FFCDCDCD{�.   6   �� *ShowAgeRatingChecked+6�   �� 5e      $��SEARCH CATEGORIES��6   $��Center=�60   $��Center=�6T   $
��Left=�6v   $	��Top=�6�   $��14(�6�   ���	#FFCDCDCD$��
10,200,0,0q�6�   $��35��6�   $	��150��6�   �� 5g      $��Search Titles��6A   $
��Left=�6Y   $��
30,244,0,0q�6t   $	��Top=�6�   ���	#FFCDCDCD 6   �� *SearchTitlesChecked+6�   �� 5h      $��Search Developers��6E   $
��Left=�6a   $��
30,264,0,0q�6|   $	��Top=�6�   ���	#FFCDCDCD 6   �� *SearchDevelopersChecked+6�   �� 5i      $��Search Descriptions��6F   $
��Left=�6d   $��
30,284,0,0q�6   $	��Top=�6�   ���	#FFCDCDCD 6   �� *SearchDescriptionChecked+6�   �� 5j      $��Search Tags��6?   $
��Left=�6U   $��
30,304,0,0q�6p   $	��Top=�6�   ���	#FFCDCDCD 6   �� *SearchTagsChecked+6�   �� 5m      $��HIGHLIGHT COLORS��6   $��Center=�6/   $��Center=�6S   $
��Left=�6u   $	��Top=�6�   $��14(�6�   ���	#FFCDCDCD$��
200,10,0,0q�6�   $��35��6�   $	��188��6�   �� 5o      $
��Left=�6:   $	��Top=�6U   $��50��6m   $��20��6x   $��
260,50,0,0q�6�   
���   ���6   �� *HighlightDefault+6�   �� 5p      $
��Left=�68   $	��Top=�6S   $��50��6k   $��20��6v   $��
260,80,0,0q�6�   
���   ���6   �� *HighlightTitle+6�   �� 5q      $
��Left=�6<   $	��Top=�6W   $��50��6o   $��20��6z   $��260,110,0,0q�6�   
���   ���6   �� *HighlightDeveloper+6�   �� 5r      $
��Left=�6>   $	��Top=�6Y   $��50��6q   $��20��6|   $��260,140,0,0q�6�   
���   ���6   �� *HighlightDescription+6�   �� 5s      $
��Left=�67   $	��Top=�6R   $��50��6j   $��20��6u   $��260,170,0,0q�6�   
���   ���6   �� *HighlightTags+6�   �� 5u      -   5        $ HighlightDefaultText��5u      $
��Left=�64   $	��Top=�6O   $��75��6g   $��20��6r   $��
320,50,0,0q�6~    �� TextWrapping$
 Wrap=�6�   #��[ 6�   �� 5v      -	   5        $ HighlightTitleText��5v      $
��Left=�62   $	��Top=�6M   $��75��6e   $��20��6p   $��
320,80,0,0q�6|   $
 Wrap=�6�   #��[ 6�   �� 5w      -
   5        $ HighlightDeveloperText��5w      $
��Left=�66   $	��Top=�6Q   $��75��6i   $��20��6t   $��320,110,0,0q�6�   $
 Wrap=�6�   #��[ 6�   �� 5x      -   5        $ HighlightDescriptionText��5x      $
��Left=�68   $	��Top=�6S   $��75��6k   $��20��6v   $��320,140,0,0q�6�   $
 Wrap=�6�   #��[ 6�   �� 5y      -   5        $ HighlightTagsText��5y      $
��Left=�61   $	��Top=�6L   $��75��6d   $��20��6o   $��320,170,0,0q�6{   $
 Wrap=�6�   #��[ 6�   �� 5{      $��DEFAULT��6   $��Right=�6&   $��Center=�6I   $��Right=�6k   $	��Top=�6�   $��9(�6�   ���	#FFCDCDCD$��
0,50,160,0q�6�   $��20��6�   $��40��6�   $��0q�6�   �� 5|      $��TITLE��6   $��Right=�6$   $��Center=�6G   $��Right=�6i   $	��Top=�6�   $��9(�6�   ���	#FFCDCDCD$��
0,80,160,0q�6�   $��20��6�   $��30��6�   $��0q�6�   �� 5}      $��	DEVELOPER��6   $��Right=�6(   $��Center=�6K   $��Right=�6m   $	��Top=�6�   $��9(�6�   ���	#FFCDCDCD$��0,110,160,0q�6�   $��20��6�   $��60��6�   $��0q�6�   �� 5~      $��DESCRIPTION��6   $��Right=�6*   $��Center=�6M   $��Right=�6o   $	��Top=�6�   $��9(�6�   ���	#FFCDCDCD$��0,140,160,0q�6�   $��20��6�   $��70��6�   $��0q�6�   �� 5      $	��TAG��6   $��Right=�6"   $��Center=�6E   $��Right=�6g   $	��Top=�6�   $��9(�6�   ���	#FFCDCDCD$��0,170,160,0q�6�   $��20��6�   $��25��6�   $��0q�6�   �� 5�      $��MISC OPTIONS��6   $��Center=�6+   $��Center=�6O   $
��Left=�6q   $	��Top=�6�   $��14(�6�   ���	#FFCDCDCD$��200,200,0,0q�6�   $��35��6�   $	��188��6�   �� 5�      $$��Auto Scroll Overwrites VN Info��6G   $
��Left=�6p   $��200,244,0,0q�6�   $	��Top=�6�   ���	#FFCDCDCD 6   �� *GoToNextOverwritesChecked+6�   5�      5�      5�      