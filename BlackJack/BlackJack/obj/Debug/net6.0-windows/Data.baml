   M S B A M L   `   `   `  ����D  @BlackJack, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:BlackJack	BlackJack  5   	       BlackJack.Data   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:BlackJack     5   	     � Title$
  Data��5   	   $��450         ��6   $��820         ��5	      .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dGridPlayers��5      $��10,10,495,47q�6T      AutoGenerateColumns$
 True��6j   6  �� 5   
   -   5         ��Name$ btnAddPlayer��5      $��
Add Player��6'   $
��Left=�6<   $��
10,366,0,0q�6W   $	��Top=�6k   $��72��6�   6�   �� 5   
   -   5         ��Name$ txtBoxAddPlayer��5      $
��Left=�6+   $��
87,368,0,0q�6F    �� TextWrapping$
 Wrap=�6Z   $	��Top=�6n   $	��133��6�     5   
   -   5        $ dGridResults��5      $��310,10,265,47q�5         
IsReadOnly$
 True��5      5        5   
   -   5        $ 
dGridGames��5      $��540,10,10,47q�5      $
 True��5      5      �� 5   
   $��	Players
��6   $
��Left=�6,   $��225,364,0,0q�6G   $	��Top=�6\   $��69��6t   $��26��6   �� 5    
   $��Results
��6   $
��Left=�6'   $��388,363,0,0q�6B   $	��Top=�6W   $��69��6o   $��26��6z    J� RenderTransformOrigin$ 0.498,0.527&�6�   �� 5!   
   $��Games

��6   $
��Left=�6/   $��630,363,0,0q�6J   $	��Top=�6_   $��69��6w   $��26��6�   �� 5"   
   -   5        $ btnShowAllGames��5"      $��Show All Games��6*   $
��Left=�6C   $��697,366,0,0q�6^   $	��Top=�6s   $��93��6�   6�   5$      5%      