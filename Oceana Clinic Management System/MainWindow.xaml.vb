Imports System.Data

Class MainWindow
    'focus at username when application start
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        txtusername.Focus()
    End Sub

    'enter to focus on the next control
    Private Sub txtusername_KeyDown(sender As Object, e As KeyEventArgs) Handles txtusername.KeyDown
        If e.Key = Key.Enter Then
            txtpassword.Focus()
        End If
    End Sub
    Sub txtpassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtpassword.KeyDown
        If e.Key = Key.Enter Then
            btnlogin_Click(Me, New RoutedEventArgs)
        End If
    End Sub

    'to remove the watermark
    Private Sub txtusername_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtusername.TextChanged
        If txtusername.Text.Length > 0 Then
            tbusername.Visibility = Visibility.Collapsed
        Else
            tbusername.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txtpassword_PasswordChanged(sender As Object, e As RoutedEventArgs) Handles txtpassword.PasswordChanged
        If txtpassword.Password.Length > 0 Then
            tbpassword.Visibility = Visibility.Collapsed
        Else
            tbpassword.Visibility = Visibility.Visible
        End If
    End Sub

    ' to exit the program
    Private Sub btnexit_Click(sender As Object, e As RoutedEventArgs) Handles btnexit.Click
        Application.Current.Shutdown()
    End Sub

    ' connect to database when login button Is clicked
    Private Sub btnlogin_Click(sender As Object, e As RoutedEventArgs) Handles btnlogin.Click
        'check if both username and password is filled
        If Not String.IsNullOrEmpty(txtusername.Text) And Not String.IsNullOrEmpty(txtpassword.Password) Then
            Dim userdata As DataTable = Executesql("Select * FROM UserLogin WHERE Username = '" & txtusername.Text & "' AND UserPassword = '" & txtpassword.Password & "'")
            If userdata.Rows.Count > 0 Then
                txtusername.Clear()
                txtpassword.Clear()
                Me.Hide()
                ' open form based on the usertype
                If userdata.Rows(0)(3) = "Doctor" Then
                    Dim formain As New DoctorForm()
                    'transfer name and userid to doctor form
                    formain.doctorname = userdata.Rows(0)(4)
                    formain.userid = userdata.Rows(0)(0)
                    formain.ShowDialog()
                    formain = Nothing
                    Me.Show()
                    txtusername.Focus()
                ElseIf userdata.Rows(0)(3) = "Nurse" Then
                    Dim formain As New NurseForm()
                    'transfer name and userid to nurse form
                    formain.nursename = userdata.Rows(0)(4)
                    formain.userid = userdata.Rows(0)(0)
                    formain.ShowDialog()
                    formain = Nothing
                    Me.Show()
                    txtusername.Focus()
                ElseIf userdata.Rows(0)(3) = "Admin" Then
                    Dim formain As New AdminForm()
                    'transfer name and userid to doctor form
                    formain.adminname = userdata.Rows(0)(4)
                    formain.userid = userdata.Rows(0)(0)
                    formain.ShowDialog()
                    formain = Nothing
                    Me.Show()
                    txtusername.Focus()
                End If
            Else
                MsgBox("Usermane or Password is Incorrect. Please Try Again", MsgBoxStyle.OkOnly, "Login Error")
                txtpassword.Clear()
                txtpassword.Focus()
            End If
        Else
            MsgBox("Pleaase enter Username and Password", MsgBoxStyle.OkOnly, "Login Error")
        End If
    End Sub


End Class
