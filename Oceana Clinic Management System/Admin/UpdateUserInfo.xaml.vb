Imports System.Data
Imports System.Text.RegularExpressions

Public Class UpdateUserInfo
    'to remove the watermark
    Private Sub txtsearch_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtsearch.TextChanged
        If txtsearch.Text.Length > 0 Then
            tbsearch.Visibility = Visibility.Collapsed
        Else
            tbsearch.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txtPassword_PasswordChanged(sender As Object, e As RoutedEventArgs) Handles txtPassword.PasswordChanged
        If txtPassword.Password.Length > 0 Then
            tbpassword.Visibility = Visibility.Collapsed
        Else
            tbpassword.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txtRetypePassword_PasswordChanged(sender As Object, e As RoutedEventArgs) Handles txtRetypePassword.PasswordChanged
        If txtRetypePassword.Password.Length > 0 Then
            tbretypepassword.Visibility = Visibility.Collapsed
        Else
            tbretypepassword.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txtEmail_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtEmail.TextChanged
        If txtEmail.Text.Length > 0 Then
            tbemail.Visibility = Visibility.Collapsed
        Else
            tbemail.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txtPhoneNumber_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtPhoneNumber.TextChanged
        If txtPhoneNumber.Text.Length > 0 Then
            tbphonenumber.Visibility = Visibility.Collapsed
        Else
            tbphonenumber.Visibility = Visibility.Visible
        End If
    End Sub

    ' reload the form when cancel is clicked
    Private Sub btncancel_Click(sender As Object, e As RoutedEventArgs) Handles btncancel.Click
        Dim parent = TryCast(Me.Parent, Grid)
        If Not parent Is Nothing Then
            parent.Children.Remove(Me)
        End If
        parent.Children.Add(New UpdateUserInfo)
    End Sub

    'data validation (need bbe be same as below)
    Dim mail As Regex = New Regex("^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$")
    Dim emailcheck As DataTable = Executesql("select Email from UserLogin")
    Private Sub txtEmail_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtEmail.LostFocus
        If mail.IsMatch(txtEmail.Text) Then
            lblemailerror.Content = ""
        Else
            lblemailerror.Content = "*Invalid Email Address"
            Return
        End If

    End Sub
    Private Sub txtRetypePassword_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtRetypePassword.LostFocus
        If txtPassword.Password = txtRetypePassword.Password Then
            lblpassworderror.Content = ""
        Else
            lblpassworderror.Content = "*Password Does Not Match"
            Return
        End If
    End Sub
    Private Sub txtPassword_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtPassword.LostFocus
        If txtPassword.Password = txtRetypePassword.Password Then
            lblpassworderror.Content = ""
        Else
            lblpassworderror.Content = "*Password Does Not Match"
            Return
        End If
    End Sub
    Private Sub txtPhoneNumber_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtPhoneNumber.LostFocus
        If IsNumeric(txtPhoneNumber.Text) Then
            lblphonenumbererror.Content = ""
        Else
            lblphonenumbererror.Content = "*Invalid Phone Number"
            Return
        End If
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
            txtName.Text = searchuserid.Rows(0)(4)
            txtUsername.Text = searchuserid.Rows(0)(1)
            If searchuserid.Rows(0)(3) = "Doctor" Then
                cbousertype.SelectedIndex = 0
            ElseIf searchuserid.Rows(0)(3) = "Nurse" Then
                cbousertype.SelectedIndex = 1
            ElseIf searchuserid.Rows(0)(3) = "Admin" Then
                cbousertype.SelectedIndex = 2
            End If

            If Not IsDBNull(searchuserid.Rows(0)(6)) Then
                txtEmail.Text = searchuserid.Rows(0)(6)
            End If
            If Not IsDBNull(searchuserid.Rows(0)(5)) Then
                txtPhoneNumber.Text = searchuserid.Rows(0)(5)
            End If
            txtPassword.Password = searchuserid.Rows(0)(2)
            txtRetypePassword.Password = searchuserid.Rows(0)(2)

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
            txtName.Text = searchusername.Rows(0)(4)
            txtUsername.Text = searchusername.Rows(0)(1)
            If searchusername.Rows(0)(3) = "Doctor" Then
                cbousertype.SelectedIndex = 0
            ElseIf searchusername.Rows(0)(3) = "Nurse" Then
                cbousertype.SelectedIndex = 1
            ElseIf searchusername.Rows(0)(3) = "Admin" Then
                cbousertype.SelectedIndex = 2
            End If

            If Not IsDBNull(searchusername.Rows(0)(6)) Then
                txtEmail.Text = searchusername.Rows(0)(6)
            End If
            If Not IsDBNull(searchusername.Rows(0)(5)) Then
                txtPhoneNumber.Text = searchusername.Rows(0)(5)
            End If
            txtPassword.Password = searchusername.Rows(0)(2)
            txtRetypePassword.Password = searchusername.Rows(0)(2)

            txtsearch.Clear()
        Else ' userid and usernae dose not exist
            MsgBox("Please enter a valid UserID or Username to search.", MsgBoxStyle.OkOnly, "Oceana Clinic")
        End If
    End Sub
    Private Sub btnsearch_Click(sender As Object, e As RoutedEventArgs) Handles btnsearch.Click
        usersearch()
    End Sub
    'same but use enter key
    Private Sub txtsearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtsearch.KeyDown
        If e.Key = Key.Enter Then
            usersearch()
        End If
    End Sub

    Private Sub btnupdate_Click(sender As Object, e As RoutedEventArgs) Handles btnupdate.Click
        'check did admin search before updating 
        If Not String.IsNullOrEmpty(txtUserID.Text) Then
            ' check for empty field and focus it
            Dim title As String = "Oceana Clinic"
            Dim btn As MessageBoxButton = MessageBoxButton.OK
            Dim icon As MessageBoxImage = MessageBoxImage.Exclamation
            If String.IsNullOrEmpty(txtEmail.Text) Then
                MessageBox.Show("Please Enter Email Address.", title, btn, icon)
                txtEmail.Focus()
                Return
            End If
            If String.IsNullOrEmpty(txtPhoneNumber.Text) Then
                MessageBox.Show("Please Enter Phone Number.", title, btn, icon)
                txtPhoneNumber.Focus()
                Return
            End If
            If String.IsNullOrEmpty(cbousertype.Text) Then
                MessageBox.Show("Please Select User Type.", title, btn, icon)
                txtPhoneNumber.Focus()
                Return
            End If

            'data validation (need to be same as above)
            If mail.IsMatch(txtEmail.Text) Then
            Else
                MessageBox.Show("Please Enter Valid E-mail Address.", title, btn, icon)
                txtEmail.Focus()
                Return
            End If
            If txtPassword.Password <> txtRetypePassword.Password Then
                MsgBox("Password Dose Not Match.", MsgBoxStyle.Exclamation, "Oceana Clinic")
                txtRetypePassword.Focus()
                Return
            End If
            If Not IsNumeric(txtPhoneNumber.Text) Then
                MsgBox("Phone Number Contain Numbers Only.", MsgBoxStyle.Exclamation, "Oceana Clinic")
                txtPhoneNumber.Focus()
                Return
            End If
            'update the information into database
            Executesql("update UserLogin set Name = '" & txtName.Text & "', Email = '" & txtEmail.Text & "', PhoneNumber = '" & txtPhoneNumber.Text & "', Usertype = '" & cbousertype.Text & "', UserPassword = '" & txtPassword.Password & "' where UserID = " & txtUserID.Text & "")
            MsgBox("User Information Successfully Updated.", MsgBoxStyle.OkOnly, "Oceana Clinic")
            txtUserID.Clear()
            txtName.Clear()
            txtEmail.Clear()
            txtPhoneNumber.Clear()
            txtUsername.Clear()
            txtPassword.Clear()
            txtRetypePassword.Clear()
            cbousertype.SelectedIndex = -1

        Else 'ask user to search before updating information
            MsgBox("Please enter UserID or Username to search before updating user information.", MsgBoxStyle.OkOnly, "Oceana Clinic")
        End If
    End Sub


End Class
