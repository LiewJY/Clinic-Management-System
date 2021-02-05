Imports System.Data
Imports System.Text.RegularExpressions

Public Class AddNewPatient

    Dim emailcheck As DataTable = Executesql("select Email from PatientInformation")

    Private Sub AddNewPatient_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        'show the next patient id
        Dim newpatientid As DataTable = Executesql("select top 1 * from PatientInformation order by PatientID desc")
        txtpatientid.Text = newpatientid.Rows(0)(0) + 1
    End Sub

    'enter to focus at the next textbox
    Private Sub txtname_KeyDown(sender As Object, e As KeyEventArgs) Handles txtname.KeyDown
        If e.Key = Key.Enter Then
            txtphonenumber.Focus()
        End If
    End Sub
    Private Sub txticpasportnumber_KeyDown(sender As Object, e As KeyEventArgs) Handles txticpasportnumber.KeyDown
        If e.Key = Key.Enter Then
            cbosexgender.Focus()
        End If
    End Sub
    Private Sub cmbsexgender_KeyDown(sender As Object, e As KeyEventArgs) Handles cbosexgender.KeyDown
        If e.Key = Key.Enter Then
            txtdateofbirth.Focus()
        End If
    End Sub
    Private Sub txtdateofbirth_KeyDown(sender As Object, e As KeyEventArgs) Handles txtdateofbirth.KeyDown
        If e.Key = Key.Enter Then
            txtphonenumber.Focus()
        End If
    End Sub
    Private Sub txtphonenumber_KeyDown(sender As Object, e As KeyEventArgs) Handles txtphonenumber.KeyDown
        If e.Key = Key.Enter Then
            txtemail.Focus()
        End If
    End Sub
    Private Sub txtemail_KeyDown(sender As Object, e As KeyEventArgs) Handles txtemail.KeyDown
        If e.Key = Key.Enter Then
            txtaddress1.Focus()
        End If
    End Sub
    Private Sub txtaddress1_KeyDown(sender As Object, e As KeyEventArgs) Handles txtaddress1.KeyDown
        If e.Key = Key.Enter Then
            txtaddress2.Focus()
        End If
    End Sub
    Private Sub txtaddress2_KeyDown(sender As Object, e As KeyEventArgs) Handles txtaddress2.KeyDown
        If e.Key = Key.Enter Then
            txtcity.Focus()
        End If
    End Sub
    Private Sub txtcity_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcity.KeyDown
        If e.Key = Key.Enter Then
            txtstate.Focus()
        End If
    End Sub
    Private Sub txtstate_KeyDown(sender As Object, e As KeyEventArgs) Handles txtstate.KeyDown
        If e.Key = Key.Enter Then
            txtpostcode.Focus()
        End If
    End Sub
    Private Sub txtpostcode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtpostcode.KeyDown
        If e.Key = Key.Enter Then
            cmbbloodtype.Focus()
        End If
    End Sub
    Private Sub cmbbloodtype_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbbloodtype.KeyDown
        If e.Key = Key.Enter Then
            txtweight.Focus()
        End If
    End Sub
    Private Sub txtweight_KeyDown(sender As Object, e As KeyEventArgs) Handles txtweight.KeyDown
        If e.Key = Key.Enter Then
            txtheight.Focus()
        End If
    End Sub
    Private Sub txtheight_KeyDown(sender As Object, e As KeyEventArgs) Handles txtheight.KeyDown
        If e.Key = Key.Enter Then
            txtallergies.Focus()
        End If
    End Sub
    Private Sub txtallergies_KeyDown(sender As Object, e As KeyEventArgs) Handles txtallergies.KeyDown
        If e.Key = Key.Enter Then
            txtpastcondition.Focus()
        End If
    End Sub
    Private Sub txtpastcondition_KeyDown(sender As Object, e As KeyEventArgs) Handles txtpastcondition.KeyDown
        If e.Key = Key.Enter Then
            txtcurrentmedication.Focus()
        End If
    End Sub
    Private Sub txtcurrentmedication_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcurrentmedication.KeyDown
        If e.Key = Key.Enter Then
            btnaddpatient.Focus()
        End If
    End Sub

    'to remove the watermark
    Private Sub txtname_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtname.TextChanged
        If txtname.Text.Length > 0 Then
            tbname.Visibility = Visibility.Collapsed
        Else
            tbname.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txticpasportnumber_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txticpasportnumber.TextChanged
        If txticpasportnumber.Text.Length > 0 Then
            tbicpassportnumber.Visibility = Visibility.Collapsed
        Else
            tbicpassportnumber.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txtdateofbirth_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtdateofbirth.TextChanged
        If txtdateofbirth.Text.Length > 0 Then
            tbdateofbirth.Visibility = Visibility.Collapsed
        Else
            tbdateofbirth.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txtphonenumber_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtphonenumber.TextChanged
        If txtphonenumber.Text.Length > 0 Then
            tbphonenumber.Visibility = Visibility.Collapsed
        Else
            tbphonenumber.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txtemail_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtemail.TextChanged
        If txtemail.Text.Length > 0 Then
            tbemail.Visibility = Visibility.Collapsed
        Else
            tbemail.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txtaddress1_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtaddress1.TextChanged
        If txtaddress1.Text.Length > 0 Then
            tbaddress1.Visibility = Visibility.Collapsed
        Else
            tbaddress1.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txtaddress2_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtaddress2.TextChanged
        If txtaddress2.Text.Length > 0 Then
            tbaddress2.Visibility = Visibility.Collapsed
        Else
            tbaddress2.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txtcity_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtcity.TextChanged
        If txtcity.Text.Length > 0 Then
            tbcity.Visibility = Visibility.Collapsed
        Else
            tbcity.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txtstate_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtstate.TextChanged
        If txtstate.Text.Length > 0 Then
            tbstate.Visibility = Visibility.Collapsed
        Else
            tbstate.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txtpostcode_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtpostcode.TextChanged
        If txtpostcode.Text.Length > 0 Then
            tbpostcode.Visibility = Visibility.Collapsed
        Else
            tbpostcode.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txtweight_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtweight.TextChanged
        If txtweight.Text.Length > 0 Then
            tbweight.Visibility = Visibility.Collapsed
        Else
            tbweight.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txtheight_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtheight.TextChanged
        If txtheight.Text.Length > 0 Then
            tbheight.Visibility = Visibility.Collapsed
        Else
            tbheight.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txtallergies_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtallergies.TextChanged
        If txtallergies.Text.Length > 0 Then
            tballergies.Visibility = Visibility.Collapsed
        Else
            tballergies.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txtpastcondition_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtpastcondition.TextChanged
        If txtpastcondition.Text.Length > 0 Then
            tbpastcondition.Visibility = Visibility.Collapsed
        Else
            tbpastcondition.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txtcurrentmedication_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtcurrentmedication.TextChanged
        If txtcurrentmedication.Text.Length > 0 Then
            tbcurrentmedication.Visibility = Visibility.Collapsed
        Else
            tbcurrentmedication.Visibility = Visibility.Visible
        End If
    End Sub

    ' reload the form when cancel is clicked
    Private Sub Btncancel_Click(sender As Object, e As RoutedEventArgs) Handles btncancel.Click
        Dim parent = TryCast(Me.Parent, Grid)
        If Not parent Is Nothing Then
            parent.Children.Remove(Me)
        End If
        parent.Children.Add(New AddNewPatient)
    End Sub

    'data validation (must be the same amount as below)
    Dim mail As Regex = New Regex("^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$")
    Dim dateofbirth As Regex = New Regex("^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$")
    Private Sub txtEmail_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtemail.LostFocus
        Dim mail As Regex = New Regex("^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$")
        If mail.IsMatch(txtemail.Text) Then
            lblemailerror.Content = ""
        Else
            lblemailerror.Content = "*Invalid Email Address"
            Return
        End If
        For i = 0 To emailcheck.Rows.Count - 1
            If emailcheck.Rows(i)(0) = txtemail.Text Then
                MsgBox("Email Address Exist In Database.", MsgBoxStyle.OkOnly, "Oceana Clinic")
                Return
            End If
        Next
    End Sub
    Private Sub txtdateofbirth_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtdateofbirth.LostFocus
        If dateofbirth.IsMatch(txtdateofbirth.Text) Then
            lbldateofbirtherror.Content = ""
        Else
            lbldateofbirtherror.Content = "*Invalid Date Format"
            Return
        End If
    End Sub
    Private Sub txtweight_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtweight.LostFocus
        If IsNumeric(txtweight.Text) Then
            lblweighterror.Content = ""
        Else
            lblweighterror.Content = "*Weight Contains Numbers Only"
            Return
        End If
    End Sub
    Private Sub txtheight_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtheight.LostFocus
        If IsNumeric(txtheight.Text) Then
            lblheighterror.Content = ""
        Else
            lblheighterror.Content = "*Height Contains Numbers Only"
            Return
        End If
    End Sub
    Private Sub txtPhoneNumber_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtphonenumber.LostFocus
        If IsNumeric(txtphonenumber.Text) Then
            lblphonenumbererror.Content = ""
        Else
            lblphonenumbererror.Content = "*Invalid Phone Number"
            Return
        End If

    End Sub

    Private Sub btnaddpatient_Click(sender As Object, e As RoutedEventArgs) Handles btnaddpatient.Click
        ' check for empty field and focus it
        Dim title As String = "Oceana Clinic"
        Dim btn As MessageBoxButton = MessageBoxButton.OK
        Dim icon As MessageBoxImage = MessageBoxImage.Exclamation
        If String.IsNullOrEmpty(txtname.Text) Then
            MessageBox.Show("Please Enter Name.", title, btn, icon)
            txtname.Focus()
            Return
        End If
        If String.IsNullOrEmpty(txticpasportnumber.Text) Then
            MessageBox.Show("Please Enter IC or Passport Number.", title, btn, icon)
            txticpasportnumber.Focus()
            Return
        End If
        If String.IsNullOrEmpty(cbosexgender.Text) Then
            MessageBox.Show("Please Enter Sex / Gender.", title, btn, icon)
            cbosexgender.Focus()
            Return
        End If
        If String.IsNullOrEmpty(txtdateofbirth.Text) Then
            MessageBox.Show("Please Enter Date Of Birth.", title, btn, icon)
            txtdateofbirth.Focus()
            Return
        End If
        If String.IsNullOrEmpty(txtphonenumber.Text) Then
            MessageBox.Show("Please Enter Phone Number.", title, btn, icon)
            txtphonenumber.Focus()
            Return
        End If
        If String.IsNullOrEmpty(txtemail.Text) Then
            MessageBox.Show("Please Enter Email Address.", title, btn, icon)
            txtemail.Focus()
            Return
        End If
        If String.IsNullOrEmpty(txtaddress1.Text) Then
            MessageBox.Show("Please Enter Address.", title, btn, icon)
            txtaddress1.Focus()
            Return
        End If
        If String.IsNullOrEmpty(txtaddress2.Text) Then
            MessageBox.Show("Please Enter Address.", title, btn, icon)
            txtaddress2.Focus()
            Return
        End If
        If String.IsNullOrEmpty(txtcity.Text) Then
            MessageBox.Show("Please Enter City.", title, btn, icon)
            txtcity.Focus()
            Return
        End If
        If String.IsNullOrEmpty(txtstate.Text) Then
            MessageBox.Show("Please Enter State.", title, btn, icon)
            txtstate.Focus()
            Return
        End If
        If String.IsNullOrEmpty(txtpostcode.Text) Then
            MessageBox.Show("Please Enter Postcode.", title, btn, icon)
            txtpostcode.Focus()
            Return
        End If
        If String.IsNullOrEmpty(cmbbloodtype.Text) Then
            MessageBox.Show("Please Enter Blood Type.", title, btn, icon)
            cmbbloodtype.Focus()
            Return
        End If
        If String.IsNullOrEmpty(txtweight.Text) Then
            MessageBox.Show("Please Enter Weight.", title, btn, icon)
            txtweight.Focus()
            Return
        End If
        If String.IsNullOrEmpty(txtheight.Text) Then
            MessageBox.Show("Please Enter Height.", title, btn, icon)
            txtheight.Focus()
            Return
        End If

        'save the new patient information
        'check for data validation once again (must be the same amount as above)
        If mail.IsMatch(txtemail.Text) Then
        Else
            MessageBox.Show("Please Enter Valid E-mail Address.", title, btn, icon)
            txtemail.Focus()
            Return
        End If
        For i = 0 To emailcheck.Rows.Count - 1
            If emailcheck.Rows(i)(0) = txtemail.Text Then
                MsgBox("Email Address Exist In Database.", MsgBoxStyle.OkOnly, "Oceana Clinic")
                Return
            End If
        Next
        If dateofbirth.IsMatch(txtdateofbirth.Text) Then
        Else
            MessageBox.Show("Please Enter Date Of Birth With dd/mm/yyyy Format.", title, btn, icon)
            txtdateofbirth.Focus()
            Return
        End If
        If IsNumeric(txtweight.Text) Then
        Else
            MessageBox.Show("Weight Contains Numbers Only Example: 60.", title, btn, icon)
            txtheight.Focus()
            Return
        End If
        If IsNumeric(txtheight.Text) Then
        Else
            MessageBox.Show("Height Contains Numbers Only Example: 1.60.", title, btn, icon)
            txtheight.Focus()
            Return
        End If
        If Not IsNumeric(txtphonenumber.Text) Then
            MsgBox("Phone Number Contain Numbers Only.", MsgBoxStyle.Exclamation, "Oceana Clinic")
            txtphonenumber.Focus()
            Return
        End If

        'insert into database
        Executesql("insert into PatientInformation (PatientName, ICPassportNumber, SexGender, DateOfBirth, Address1, Address2, City, State, Postcode, Bloodtype, Weight, Height,Email, PhoneNumber)
values ('" & txtname.Text & "', '" & txticpasportnumber.Text & "', '" & cbosexgender.Text & "', '" & txtdateofbirth.Text & "', '" & txtaddress1.Text & "', '" & txtaddress2.Text & "', '" & txtcity.Text & "', '" & txtstate.Text & "', '" & txtpostcode.Text & "', '" & cmbbloodtype.Text & "', '" & txtweight.Text & "','" & txtheight.Text & "', '" & txtemail.Text & "','" & txtphonenumber.Text & "')")

        'insert allergies, past condition and current medication seperately becuase it might be empty
        'if it is filled then add into database
        If Not String.IsNullOrEmpty(txtallergies.Text) Then
            Executesql("update PatientInformation set Allergies = '" & txtallergies.Text & "' where PatientID = " & txtpatientid.Text & "")
        End If
        If Not String.IsNullOrEmpty(txtpastcondition.Text) Then
            Executesql("update PatientInformation  set PastCondition = '" & txtpastcondition.Text & "' where PatientID = " & txtpatientid.Text & "")
        End If
        If Not String.IsNullOrEmpty(txtcurrentmedication.Text) Then
            Executesql("update PatientInformation  set CurrentMedication = '" & txtcurrentmedication.Text & "' where PatientID = " & txtpatientid.Text & "")
        End If

        'tell patient is added
        MsgBox("User Successfully Added.", MsgBoxStyle.OkOnly, "Oceana Clinic")





    End Sub
End Class
