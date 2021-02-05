Imports System.Data
Imports System.Text.RegularExpressions

Public Class UpdatePatientInformation
    Dim emailcheck As DataTable = Executesql("select Email from PatientInformation")

    'to remove the watermark
    Private Sub txtsearch_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtsearch.TextChanged
        If txtsearch.Text.Length > 0 Then
            tbsearch.Visibility = Visibility.Collapsed
        Else
            tbsearch.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txticpasportnumber_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txticpasportnumber.TextChanged
        If txticpasportnumber.Text.Length > 0 Then
            tbicpassportnumber.Visibility = Visibility.Collapsed
        Else
            tbicpassportnumber.Visibility = Visibility.Visible
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
    Private Sub btncancel_Click(sender As Object, e As RoutedEventArgs) Handles btncancel.Click
        Dim parent = TryCast(Me.Parent, Grid)
        If Not parent Is Nothing Then
            parent.Children.Remove(Me)
        End If
        parent.Children.Add(New UpdatePatientInformation)
    End Sub

    'data validation (must be the same amount as below)
    Dim mail As Regex = New Regex("^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$")
    Private Sub txtEmail_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtemail.LostFocus
        If mail.IsMatch(txtemail.Text) Then
            lblemailerror.Content = ""
        Else
            lblemailerror.Content = "*Invalid Email Address"
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
    'search
    Private Sub patientsearch()
        'search using patient id 
        If IsNumeric(txtsearch.Text) Then
            Dim searchpatientid As DataTable = Executesql("select * from PatientInformation where PatientID = " & txtsearch.Text & "")
            'return error patient id not found
            If searchpatientid.Rows.Count = 0 Then
                MsgBox("Please enter a valid PatientID.", MsgBoxStyle.OkOnly, "Oceana Clinic")
                Return
            End If
            'fill in when patient id is found
            txtpatientid.Text = searchpatientid.Rows(0)(0)
            txtname.Text = searchpatientid.Rows(0)(1)
            txticpasportnumber.Text = searchpatientid.Rows(0)(2)
            txtsexgender.Text = searchpatientid.Rows(0)(3)
            txtdateofbirth.Text = searchpatientid.Rows(0)(4)
            txtphonenumber.Text = searchpatientid.Rows(0)(17)
            txtemail.Text = searchpatientid.Rows(0)(16)
            txtaddress1.Text = searchpatientid.Rows(0)(5)
            txtaddress2.Text = searchpatientid.Rows(0)(6)
            txtcity.Text = searchpatientid.Rows(0)(7)
            txtstate.Text = searchpatientid.Rows(0)(8)
            txtpostcode.Text = searchpatientid.Rows(0)(9)
            txtbloodtype.Text = searchpatientid.Rows(0)(10)
            txtweight.Text = searchpatientid.Rows(0)(11)
            txtheight.Text = searchpatientid.Rows(0)(12)

            If Not IsDBNull(searchpatientid.Rows(0)(13)) Then
                txtallergies.Text = searchpatientid.Rows(0)(13)
            End If
            If Not IsDBNull(searchpatientid.Rows(0)(14)) Then
                txtpastcondition.Text = searchpatientid.Rows(0)(14)
            End If
            If Not IsDBNull(searchpatientid.Rows(0)(15)) Then
                txtcurrentmedication.Text = searchpatientid.Rows(0)(15)
            End If

            txtsearch.Clear()
            'search using patient name
        ElseIf Not IsNumeric(txtsearch) Then
            Dim searchpatientname As DataTable = Executesql("select * from PatientInformation where PatientName = '" & txtsearch.Text & "'")
            'return error patient name not found
            If searchpatientname.Rows.Count = 0 Then
                MsgBox("Please enter a valid Patient Name.", MsgBoxStyle.OkOnly, "Oceana Clinic")
                Return
            End If
            'fill in when patient name is found
            txtpatientid.Text = searchpatientname.Rows(0)(0)
            txtname.Text = searchpatientname.Rows(0)(1)
            txticpasportnumber.Text = searchpatientname.Rows(0)(2)
            txtsexgender.Text = searchpatientname.Rows(0)(3)
            txtdateofbirth.Text = searchpatientname.Rows(0)(4)
            txtphonenumber.Text = searchpatientname.Rows(0)(17)
            txtemail.Text = searchpatientname.Rows(0)(16)
            txtaddress1.Text = searchpatientname.Rows(0)(5)
            txtaddress2.Text = searchpatientname.Rows(0)(6)
            txtcity.Text = searchpatientname.Rows(0)(7)
            txtstate.Text = searchpatientname.Rows(0)(8)
            txtpostcode.Text = searchpatientname.Rows(0)(9)
            txtbloodtype.Text = searchpatientname.Rows(0)(10)
            txtweight.Text = searchpatientname.Rows(0)(11)
            txtheight.Text = searchpatientname.Rows(0)(12)

            If Not IsDBNull(searchpatientname.Rows(0)(13)) Then
                txtallergies.Text = searchpatientname.Rows(0)(13)
            End If
            If Not IsDBNull(searchpatientname.Rows(0)(14)) Then
                txtpastcondition.Text = searchpatientname.Rows(0)(14)
            End If
            If Not IsDBNull(searchpatientname.Rows(0)(15)) Then
                txtcurrentmedication.Text = searchpatientname.Rows(0)(15)
            End If

            txtsearch.Clear()
        Else ' patietnid and patient name dose not exist
            MsgBox("Please enter a valid PatientID or Patient Name to search.", MsgBoxStyle.OkOnly, "Oceana Clinic")
        End If
    End Sub
    Private Sub btnsearch_Click(sender As Object, e As RoutedEventArgs) Handles btnsearch.Click
        patientsearch()
    End Sub
    'same but use enter key
    Private Sub txtsearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtsearch.KeyDown
        If e.Key = Key.Enter Then
            patientsearch()
        End If
    End Sub


    Private Sub btnsave_Click(sender As Object, e As RoutedEventArgs) Handles btnsave.Click
        'check did user search before updating
        If Not String.IsNullOrEmpty(txtpatientid.Text) Then
            ' check for empty field and focus it
            Dim title As String = "Oceana Clinic"
            Dim btn As MessageBoxButton = MessageBoxButton.OK
            Dim icon As MessageBoxImage = MessageBoxImage.Exclamation
            If String.IsNullOrEmpty(txticpasportnumber.Text) Then
                MessageBox.Show("Please Enter IC or Passport Number.", title, btn, icon)
                txticpasportnumber.Focus()
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

            'data validation (need to be same as above)
            If mail.IsMatch(txtemail.Text) Then
            Else
                MessageBox.Show("Please Enter Valid E-mail Address.", title, btn, icon)
                txtemail.Focus()
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

            'update the information into database
            Executesql("update PatientInformation set ICPassportNumber = '" & txticpasportnumber.Text & "', PhoneNumber = '" & txtphonenumber.Text & "', Email = '" & txtemail.Text & "', Address1 = '" & txtaddress1.Text & "', Address2 = '" & txtaddress2.Text & "', City = '" & txtcity.Text & "', State = '" & txtstate.Text & "', Postcode = '" & txtpostcode.Text & "', Weight = '" & txtweight.Text & "', Height = '" & txtheight.Text & "' 
where PatientID = " & txtpatientid.Text & "")

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
            MsgBox("Patient Information Successfully Changed.", MsgBoxStyle.OkOnly, "Oceana Clinic")

        Else 'ask user to search before updating information
            MsgBox("Please enter Patient ID or Patient Name to search before updating user information.", MsgBoxStyle.OkOnly, "Oceana Clinic")

        End If



    End Sub


End Class
