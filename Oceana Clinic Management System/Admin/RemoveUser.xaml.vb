Imports System.Data

Public Class RemoveUser

    'to remove the watermark
    Private Sub txtsearch_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtsearch.TextChanged
        If txtsearch.Text.Length > 0 Then
            tbsearch.Visibility = Visibility.Collapsed
        Else
            tbsearch.Visibility = Visibility.Visible
        End If
    End Sub

    ' reload the form when cancel is clicked
    Private Sub btncancel_Click(sender As Object, e As RoutedEventArgs) Handles btncancel.Click
        Dim parent = TryCast(Me.Parent, Grid)
        If Not parent Is Nothing Then
            parent.Children.Remove(Me)
        End If
        parent.Children.Add(New RemoveUser)
    End Sub

    'search using userid or username
    Public Sub usersearch()
        'search using userid
        If IsNumeric(txtsearch.Text) Then
            Dim searchuserid As DataTable = Executesql("select * from UserLogin where UserID = " & txtsearch.Text & "")
            'return error userid not found
            If searchuserid.Rows.Count = 0 Then
                MsgBox("Please enter a valid userID.", MsgBoxStyle.OkOnly, "Oceana Clinic")
                Return
            End If
            'fill in when userid is found
            txtUserID.Text = searchuserid.Rows(0)(0)
            txtname.Text = searchuserid.Rows(0)(4)
            txtUsername.Text = searchuserid.Rows(0)(1)
            txtusertype.Text = searchuserid.Rows(0)(3)


            If Not IsDBNull(searchuserid.Rows(0)(6)) Then
                txtEmail.Text = searchuserid.Rows(0)(6)
            End If
            If Not IsDBNull(searchuserid.Rows(0)(5)) Then
                txtPhoneNumber.Text = searchuserid.Rows(0)(5)
            End If

            txtsearch.Clear()
            'search using username
        ElseIf Not IsNumeric(txtsearch) Then
            Dim searchusername As DataTable = Executesql("select * from UserLogin where Username = '" & txtsearch.Text & "'")
            'return error username not found
            If searchusername.Rows.Count = 0 Then
                MsgBox("Please enter a valid Username.", MsgBoxStyle.OkOnly, "Oceana Clinic")
                Return
            End If
            'fill in when username is found
            txtUserID.Text = searchusername.Rows(0)(0)
            txtname.Text = searchusername.Rows(0)(4)
            txtUsername.Text = searchusername.Rows(0)(1)
            txtusertype.Text = searchusername.Rows(0)(3)


            If Not IsDBNull(searchusername.Rows(0)(6)) Then
                txtEmail.Text = searchusername.Rows(0)(6)
            End If
            If Not IsDBNull(searchusername.Rows(0)(5)) Then
                txtPhoneNumber.Text = searchusername.Rows(0)(5)
            End If

            txtsearch.Clear()
        Else ' userid and usernae dose not exist
            MsgBox("Please enter a valid UserID or Username to search.", MsgBoxStyle.OkOnly, "Oceana Clinic")
        End If
    End Sub
    Private Sub btnsearch_Click(sender As Object, e As RoutedEventArgs) Handles btnsearch.Click
        usersearch()
    End Sub
    'same but use enter button
    Private Sub txtsearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtsearch.KeyDown
        If e.Key = Key.Enter Then
            usersearch()
        End If
    End Sub

    'remov user with confirmation
    Private Sub btnremoveuser_Click(sender As Object, e As RoutedEventArgs) Handles btnremoveuser.Click
        Dim result As MsgBoxResult = MsgBox("Are you sure you want to permenantly delete this user?", MsgBoxStyle.YesNo, "Oceana Clinic")
        If result = MsgBoxResult.Yes Then
            Executesql("delete from userlogin where username ='" & txtUsername.Text & "'")
            MsgBox("User Sucessfully removed.", MsgBoxStyle.OkOnly, "Oceana Clinic")
            txtUserID.Clear()
            txtUsername.Clear()
            txtname.Clear()
            txtPhoneNumber.Clear()
            txtEmail.Clear()
            txtusertype.Clear()
        End If

    End Sub
End Class
