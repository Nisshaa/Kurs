   M S B A M L   `   `   `  ����D  @kursMinin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:kursMinin.windowkursMinin.window  5   	   $    kursMinin.window.ServiceWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:kursMinin.window     5   	     � Title$  ServiceWindow��5      $	��740��6   $	��800��6)   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��auto �6   �� 5      $��* �6    .+�5   
   �� $��1��6    �� Orientation$ 
Horizontal=�6%   y�6>   �� *
NewProduct+.+�5      �� $/��)Идентификатор услуги: ��6   �� 5      ��6   �� *CurrentOrder.id+5      �� 5   
   $
��5 60q�6   $��1��6#   .+�5      �� $
��left=�5      ��5      �� *UsermanList+ �� SelectedItem 5      �� *CurrentOrder.Userman+��5      �� 5      .R�5      �� ��6    �� *
FullName+y�6=   �� *
FilterRole+5      5      �� 5       $��Номер заказа��6   �� 5!      ��6   �� *CurrentOrder.Number+�� 5"      $��Дата заказа��6   �� 5#      ��6   �� *CurrentOrder.Data+�� 5$      $��Цена заказа��6   �� 5%      ��6   �� *CurrentOrder.Total+�� 5&      $��Время заказа��6   �� 5'      ��6   �� *CurrentOrder.OrderTime+�� 5(      -   5        $	��100��5(      $��5q�6!   $
��Left=�6,   .��6`   Сохранить6k   5)      5*      5+      