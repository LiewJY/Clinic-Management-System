   M S B A M L   `   `   `  ����Z  VOceana Clinic Management System, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:Oceana_Clinic_Management_SystemOceana_Clinic_Management_System  5   	   /    )Oceana_Clinic_Management_System.NurseForm   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ?local-clr-namespace:Oceana_Clinic_Management_System     5   	     � Title$  Oceana Clinic��5      
������� � WindowStartupLocation$ CenterScreen=�5
   	    � WindowState$ Normal=�5   	    � WindowStyle$ ThreeDBorderWindow=�5   	    � Icon$ /Graphics/oceana logo.png��5   	   $	��700��5   	   $
��1280��6    � 	Resources 5         
menubutton �� 
TargetType �� Property �� Value	 �� 
TargetType
 �� Triggers P� Property P� Value��%}  &	        �� 5   
   " ��6#   .�5      ��  �@�OverridesDefaultStyle . 66   �� 5       � �� 5      �� 5      "	 ��6&   .R�5      �� #��z��6"   ._�5      �� $��Center=�6/   $��Center=�6L   5      
 5      P� 5       �@J�IsMouseOver . . �5   "   ��  �@J�Opacity$	 0.8Q�6<   5      5      5       5!      5"      .��5$      �  � ColumnDefinitions 5&   
   �� 5'      $	��180 �6   $	��180��6+   6=   �� 5(      $��* �6   6,    � RowDefinitions 5,   
   �� 5-      $��90 �6   6*   �� 5.      $��* �6   6)   .+�53   
   ]� $��0��6   $��0��6$   $��2��61   $	��Top=�54      $��90��60   ���#D1CAFF.+�57      �� $
��Left=�6   $��Center=�6/   $��10q�58      $	��275��6$   $$��/Graphics/oceana full logo.png��59      5:      �� 5;      -   5        $��	btnlogout��5;      $��Logout��5<      $��18(�6&   $��40��5=      $	��100��6!   $��Right=�6-   $��	50,0,40,0q�5>      $��5q�5?      
����������#707070 5@      ��%_   '��       �� 5A      " ��6   .�5B      ��  �@��CornerRadius$ 20��69   5C      5D      5E      5F      ]� 5I   
   $��1��6   $��0��6!   ���#746BAA.+�5K      �� $��0��6   $��0��6)   ���#746BAA$	��Top=�5M      .+�5O      �� $��20(�6   $��Welcome��6&   
�������$��Center=�5Q      5R      �� 5S      -   5        $��lbluser��5S      $��20(�5T      
�������$��Center=�5V      5W      �� 5Z      -   5        $��btnaddpatient��5Z      #��[  6/   $
��Left=�5[      $	��Top=�69   $��55��5\      $	��150��6*   $��0q�5]      $��	10,10,0,0q�63    J� RenderTransformOrigin$ 
14.367,2.6&�6F   ��5^      �� 5_       �� ImageSource$ /Graphics/AddPatient.png��6%   5a      �� 5c      -   5        $��btnupdatepatientinfo��5c      #��[  66   $
��Left=�5d      $	��Top=�69   $��55��5e      $	��150��6*   $��0q�5f      $��	10,10,0,0q�63   ��5g      �� 5h      $, &/Graphics/UpdatePatientInformation.png��6%   5j      �� 5l      -   5        $��
btnbilling��5l      #��[  6,   $
��Left=�5m      $	��Top=�69   $��55��5n      $	��150��6*   $��0q�5o      $��	10,10,0,0q�63   ��5p      �� 5q      $ /Graphics/Billing.png��6%   5s      �� 5u      -   5        $��btnmedicine��5u      #��[  6-   $
��Left=�5v      $	��Top=�69   $��55��5w      $	��150��6*   $��0q�5x      $��	10,10,0,0q�63   ��5y      �� 5z      $# /Graphics/MedicineManager.png��6%   5|      5~      5      � 5�   
   -   5        $��maingrid��5�      $��1��6   $��1��6/   5�      5�      