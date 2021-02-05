Public Class NurseForm

    'data transfered from login
    Public Property nursename As String
    Public Property userid As String


    'load name at the menu panel
    'change opacity of button and open billing
    Private Sub NurseForm_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        lbluser.Content = nursename
        maingrid.Children.Add(New Billing())
        btnaddpatient.Opacity = 1
        btnupdatepatientinfo.Opacity = 1
        btnbilling.Opacity = 0.7
        btnmedicine.Opacity = 1
    End Sub

    'load corresponding form when button is clicked
    Private Sub btnaddpatient_Click(sender As Object, e As RoutedEventArgs) Handles btnaddpatient.Click
        maingrid.Children.Clear()
        maingrid.Children.Add(New AddNewPatient())
        btnaddpatient.Opacity = 0.7
        btnupdatepatientinfo.Opacity = 1
        btnbilling.Opacity = 1
        btnmedicine.Opacity = 1
    End Sub
    Private Sub btnupdatepatientinfo_Click(sender As Object, e As RoutedEventArgs) Handles btnupdatepatientinfo.Click
        maingrid.Children.Clear()
        maingrid.Children.Add(New UpdatePatientInformation())
        btnaddpatient.Opacity = 1
        btnupdatepatientinfo.Opacity = 0.7
        btnbilling.Opacity = 1
        btnmedicine.Opacity = 1
    End Sub
    Private Sub btnbilling_Click(sender As Object, e As RoutedEventArgs) Handles btnbilling.Click
        maingrid.Children.Clear()
        maingrid.Children.Add(New Billing())
        btnaddpatient.Opacity = 1
        btnupdatepatientinfo.Opacity = 1
        btnbilling.Opacity = 0.7
        btnmedicine.Opacity = 1
    End Sub
    Private Sub btnmedicine_Click(sender As Object, e As RoutedEventArgs) Handles btnmedicine.Click
        maingrid.Children.Clear()
        maingrid.Children.Add(New AddNewMedicine())
        btnaddpatient.Opacity = 1
        btnupdatepatientinfo.Opacity = 1
        btnbilling.Opacity = 1
        btnmedicine.Opacity = 0.7
    End Sub
    Private Sub btnlogout_Click(sender As Object, e As RoutedEventArgs) Handles btnlogout.Click
        Me.Close()
    End Sub
End Class
