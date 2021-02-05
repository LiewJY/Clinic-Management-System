Public Class AdminForm
    'data transfered from login
    Public Property adminname As String
    Public Property userid As String

    'load name at the menu panel
    Private Sub AdminForm_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        lbluser.Content = adminname
    End Sub


    'load corresponding form when button is clicked
    Private Sub btnAddUser_Click(sender As Object, e As RoutedEventArgs) Handles btnAddUser.Click
        maingrid.Children.Clear()
        maingrid.Children.Add(New AddUser)
        btnAddUser.Opacity = 0.7
        btnRemoveUser.Opacity = 1
        btnUpdateUserInfo.Opacity = 1

    End Sub
    Private Sub btnRemoveUser_Click(sender As Object, e As RoutedEventArgs) Handles btnRemoveUser.Click
        maingrid.Children.Clear()
        maingrid.Children.Add(New RemoveUser)
        btnAddUser.Opacity = 1
        btnRemoveUser.Opacity = 0.7
        btnUpdateUserInfo.Opacity = 1

    End Sub
    Private Sub btnUpdateUserInfo_Click(sender As Object, e As RoutedEventArgs) Handles btnUpdateUserInfo.Click
        maingrid.Children.Clear()
        maingrid.Children.Add(New UpdateUserInfo)
        btnAddUser.Opacity = 1
        btnRemoveUser.Opacity = 1
        btnUpdateUserInfo.Opacity = 0.7

    End Sub

    Private Sub btnlogout_Click(sender As Object, e As RoutedEventArgs) Handles btnlogout.Click
        Me.Close()
    End Sub
End Class
