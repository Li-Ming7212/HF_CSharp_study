   M S B A M L   `   `   `  ����F  BTwoDecksWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:TwoDecksWPFTwoDecksWPF  5   	       TwoDecksWPF.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:TwoDecksWPF     5   	     � Title$  	Two Decks��5   	   $	��450��6   $	��400��6(    � 	Resources 5	        TwoDecksWPF.Deck   leftDeck  	rightDeck��%:   &	        &	        5
   
   6     5   
   6   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� -   5         ��Name$ 
deck1Label��5      $��Deck _1��6$   $��10,0,0,0q�66    �� Target 6H   ��  �� ElementName$ leftDeckListBox�� �� Mode$ OneWay=��� 5   
   -   5        $ 
deck2Label��5      $��1��6$   $��Deck _2��64   $��10,0,0,0q�6F    6X   �� $ rightDeckListBox��$ OneWay=��� 5   
   -   5         ��Name$ leftDeckListBox��5      $��1��6+   $��
10,0,10,10q�69   #���   6M   6�   �� 5   
   -   5        $ rightDeckListBox��5      $��1��6,   $��1��6<   $��
10,0,10,10q�6I   #���  6]   6�   �� 5   
   -   5        	 ��Name$	 shuffleLeftDeck��5      $��_Shuffle��6*   $��2��6>   $��
10,0,10,10q�6L   6`   �� 5   
   -   5        $	 clearRightDeck��5      $��_Clear��6)   $��2��6;   $��1��6H   $��
10,0,10,10q�6Y   6m   �� 5   
   -   5        $	 resetLeftDeck��5      $��_Reset��6(   $��3��6:   $��
10,0,10,10q�6H   6\   �� 5    
   -   5        $	 sortRightDeck��5       $��Sor_t��6(   $��3��69   $��1��6F   $��
10,0,10,10q�6W   6k   5"      5%      