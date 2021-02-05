Imports System.Data
Imports System.Text.RegularExpressions

Public Class AddUser


    Private Sub AddUser_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        txtName.Focus()
        'show all record in data grid
        Dim userdata As DataTable = Executesql("select * from UserLogin order by UserID")
        dguserdetails.ItemsSource = userdata.DefaultView
        'show the nect user id
        Dim newuserid As DataTable = Executesql("select top 1 * from UserLogin order by UserID desc")
        txtUserID.Text = newuserid.Rows(0)(0) + 1
    End Sub

    'enter to focus on the next control
    Private Sub txtName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtName.KeyDown
        If e.Key = Key.Enter Then
            txtEmail.Focus()
        End If
    End Sub
    Private Sub txtEmail_KeyDown(sender As Object, e As KeyEventArgs) Handles txtEmail.KeyDown
        If e.Key = Key.Enter Then
            txtPhoneNumber.Focus()
        End If
    End Sub
    Private Sub txtPhoneNumber_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPhoneNumber.KeyDown
        If e.Key = Key.Enter Then
            txtUsername.Focus()
        End If
    End Sub
    Private Sub txtUsername_KeyDown(sender As Object, e As KeyEventArgs) Handles txtUsername.KeyDown
        If e.Key = Key.Enter Then
            txtPassword.Focus()
        End If
    End Sub
    Private Sub txtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPassword.KeyDown
        If e.Key = Key.Enter Then
            txtRetypePassword.Focus()
        End If
    End Sub
    Private Sub txtRetypePassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtRetypePassword.KeyDown
        If e.Key = Key.Enter Then
            btnadduser.Focus()
        End If
    End Sub

    'to remove the watermark
    Private Sub txtName_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtName.TextChanged
        If txtName.Text.Length > 0 Then
            tbname.Visibility = Visibility.Collapsed
        Else
            tbname.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txtPhoneNumber_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtPhoneNumber.TextChanged
        If txtPhoneNumber.Text.Length > 0 Then
            tbphonenumber.Visibility = Visibility.Collapsed
        Else
            tbphonenumber.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txtEmail_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtEmail.TextChanged
        If txtEmail.Text.Length > 0 Then
            tbemail.Visibility = Visibility.Collapsed
        Else
            tbemail.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txtUsername_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtUsername.TextChanged
        If txtUsername.Text.Length > 0 Then
            tbusername.Visibility = Visibility.Collapsed
        Else
            tbusername.Visibility = Visibility.Visible
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

    ' reload the form when cancel is clicked
    Private Sub btncancel_Click(sender As Object, e As RoutedEventArgs) Handles btncancel.Click
        Dim parent = TryCast(Me.Parent, Grid)
        If Not parent Is Nothing Then
            parent.Children.Remove(Me)
        End If
        parent.Children.Add(New AddUser)

    End Sub

    'data validation
    Dim mail As Regex = New Regex("^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$")
    Dim emailcheck As DataTable = Executesql("select Email from UserLogin")
    Private Sub txtEmail_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtEmail.LostFocus
        If mail.IsMatch(txtEmail.Text) Then
            lblemailerror.Content = ""
        Else
            lblemailerror.Content = "*Invalid Email Address."
            Return
        End If
        For i = 0 To emailcheck.Rows.Count - 1
            If emailcheck.Rows(i)(0) = txtEmail.Text Then
                MsgBox("Email Address Exist In Database.", MsgBoxStyle.OkOnly, "Oceana Clinic")
                Return
            End If
        Next
    End Sub
    Private Sub txtRetypePassword_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtRetypePassword.LostFocus
        If txtPassword.Password = txtRetypePassword.Password Then
            lblpassworderror.Content = ""
        Else
            lblpassworderror.Content = "*Password Does Not Match."
            Return
        End If
    End Sub
    Private Sub txtPassword_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtPassword.LostFocus
        If txtPassword.Password = txtRetypePassword.Password Then
            lblpassworderror.Content = ""
        Else
            lblpassworderror.Content = "*Password Does Not Match."
            Return
        End If
    End Sub


    Private Sub btnadduser_Click(sender As Object, e As RoutedEventArgs) Handles btnadduser.Click
        ' check for empty field and focus it
        Dim title As String = "Oceana Clinic"
        Dim btn As MessageBoxButton = MessageBoxButton.OK
        Dim icon As MessageBoxImage = MessageBoxImage.Exclamation
        If String.IsNullOrEmpty(txtName.Text) Then
            MessageBox.Show("Please Enter Name.", title, btn, icon)
            txtName.Focus()
            Return
        End If
        If String.IsNullOrEmpty(txtEmail.Text) Then
            MessageBox.Show("Please Enter E-mail.", title, btn, icon)
            txtEmail.Focus()
            Return
        End If
        If String.IsNullOrEmpty(txtPhoneNumber.Text) Then
            MessageBox.Show("Please Enter Phone Number.", title, btn, icon)
            txtPhoneNumber.Focus()
            Return
        End If
        If String.IsNullOrEmpty(txtUsername.Text) Then
            MessageBox.Show("Please Enter Username.", title, btn, icon)
            txtUsername.Focus()
            Return
        End If
        If String.IsNullOrEmpty(txtPassword.Password) Then
            MessageBox.Show("Please Enter Password.", title, btn, icon)
            txtPassword.Focus()
            Return
        End If
        If String.IsNullOrEmpty(txtRetypePassword.Password) Then
            MessageBox.Show("Please Enter Password.", title, btn, icon)
            txtPassword.Focus()
            Return
        End If

        'check for validation again
        If mail.IsMatch(txtEmail.Text) Then
        Else
            MessageBox.Show("Please Enter Valid E-mail Address.", title, btn, icon)
            txtEmail.Focus()
            Return
        End If
        For i = 0 To emailcheck.Rows.Count - 1
            If emailcheck.Rows(i)(0) = txtEmail.Text Then
                MsgBox("Email Address Exist In Database.", MsgBoxStyle.OkOnly, "Oceana Clinic")
                Return
            End If
        Next
        'check if password and retype password is same
        If txtPassword.Password <> txtRetypePassword.Password Then
            MessageBox.Show("Password does not match", title, btn, icon)
            txtRetypePassword.Focus()
            Return
        End If

        'save the new user information
        'check for duplication of username if exist neeed to user another one
        Dim CheckUsernameDuplication As DataTable = Executesql("select username from UserLogin where Username ='" & txtUsername.Text & "'")
        If CheckUsernameDuplication.Rows.Count > 0 Then
            MsgBox("Please use a different username.")
            txtUsername.Focus()
            Return
        End If


        'insert into database
        Executesql("insert into UserLogin (Username, UserPassword, Usertype, Name, PhoneNumber, Email) values ('" & txtUsername.Text & "','" & txtPassword.Password & "','" & cbousertype.Text & "','" & txtName.Text & "','" & txtPhoneNumber.Text & "','" & txtEmail.Text & "')")
        'tell user is added
        MsgBox("User Successfully Added.", MsgBoxStyle.OkOnly, "Oceana Clinic")
        Dim userdata As DataTable = Executesql("select * from UserLogin order by UserID ")
        dguserdetails.ItemsSource = userdata.DefaultView

    End Sub


End Class
