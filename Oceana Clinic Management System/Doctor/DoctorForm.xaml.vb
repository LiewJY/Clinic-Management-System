Imports System.Data

Public Class DoctorForm
    'data transfered from login
    Public Property doctorname As String
    Public Property userid As String

    ' use to open new form
    Dim history As New PatientHistory

    'to transfer data to patient history
    Dim patientid As Integer
    Dim patientname As String


    ' public variable for this form
    Dim medicinetable As New DataTable("addedmedicinetable")
    Dim selectedmedicine As DataTable = Executesql(" select * from MedicineAndServices where Type ='Medicine' order by MSName")

    'load name at the menu panel
    'load data into cmbmedicine
    Private Sub DoctorForm_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        lbluser.Content = doctorname

        cbomedicines.ItemsSource = selectedmedicine.DefaultView
        cbomedicines.DisplayMemberPath = "MSName"
        cbomedicines.SelectedValuePath = "MSID"

        medicinetable.Columns.Add("MSID")
        medicinetable.Columns.Add("MSName")
        medicinetable.Columns.Add("Quantity")

        dgaddmedicine.ItemsSource = medicinetable.DefaultView

        btnHistory.IsEnabled = False
        btnHistory.Opacity = 0.7
    End Sub

    'to remove the watermark
    Private Sub txtsearch_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtsearch.TextChanged
        If txtsearch.Text.Length > 0 Then
            tbsearch.Visibility = Visibility.Collapsed
        Else
            tbsearch.Visibility = Visibility.Visible
        End If
    End Sub

    'load corresponding form when button is clicked
    Private Sub btnHistory_Click(sender As Object, e As RoutedEventArgs) Handles btnHistory.Click
        maingrid.Children.Add(history)

        btnHistory.IsEnabled = False


        'this is to transfer the patientID and patient name to history form
        history.patientid = patientid
        history.patientname = patientname
    End Sub
    Private Sub btnPrescription_Click(sender As Object, e As RoutedEventArgs) Handles btnPrescription.Click
        maingrid.Children.Remove(history)

        btnHistory.IsEnabled = True


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
            txtbloodtype.Text = searchpatientid.Rows(0)(10)
            txtweight.Text = searchpatientid.Rows(0)(11)
            txtheight.Text = searchpatientid.Rows(0)(12)
            Dim dobpatientid As Date = searchpatientid.Rows(0)(4)
            txtdateofbirth.Text = dobpatientid.ToString("dd/MM/yyyy")
            If Not IsDBNull(searchpatientid.Rows(0)(13)) Then
                txtallergies.Text = searchpatientid.Rows(0)(13)
            End If
            If Not IsDBNull(searchpatientid.Rows(0)(14)) Then
                txtpastcondition.Text = searchpatientid.Rows(0)(14)
            End If
            If Not IsDBNull(searchpatientid.Rows(0)(15)) Then
                txtcurrentmedication.Text = searchpatientid.Rows(0)(15)
            End If

            'this is to transfer the patientID and patient name to history form
            patientid = searchpatientid.Rows(0)(0)
            patientname = searchpatientid.Rows(0)(1)

            btnHistory.IsEnabled = True
            btnHistory.Opacity = 1
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
            txtbloodtype.Text = searchpatientname.Rows(0)(10)
            txtweight.Text = searchpatientname.Rows(0)(11)
            txtheight.Text = searchpatientname.Rows(0)(12)
            Dim dobpatientname As Date = searchpatientname.Rows(0)(4)
            txtdateofbirth.Text = dobpatientname.ToString("dd/MM/yyyy")
            If Not IsDBNull(searchpatientname.Rows(0)(13)) Then
                txtallergies.Text = searchpatientname.Rows(0)(13)
            End If
            If Not IsDBNull(searchpatientname.Rows(0)(14)) Then
                txtpastcondition.Text = searchpatientname.Rows(0)(14)
            End If
            If Not IsDBNull(searchpatientname.Rows(0)(15)) Then
                txtcurrentmedication.Text = searchpatientname.Rows(0)(15)
            End If

            'this is to transfer the patientID and patient name to history form
            patientid = searchpatientname.Rows(0)(0)
            patientname = searchpatientname.Rows(0)(1)

            btnHistory.IsEnabled = True
            btnHistory.Opacity = 1
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

    'add medicine into datagrid
    Private Sub btnadd_Click(sender As Object, e As RoutedEventArgs) Handles btnadd.Click
        'data validation
        If String.IsNullOrEmpty(cbomedicines.Text) Then
            MsgBox("Please select a medicine to add.", MsgBoxStyle.OkOnly, "Oceana Clinic")
            Return
        End If
        If IsNumeric(txtquantity.Text) Then
        Else
            MsgBox("Quantity contains numbers only.", MsgBoxStyle.OkOnly, "Oceana Clinic")
            Return
        End If

        If medicinetable.Rows.Count = 0 Then
            medicinetable.Rows.Add(cbomedicines.SelectedValue.ToString, cbomedicines.Text, txtquantity.Text)
            dgaddmedicine.ItemsSource = medicinetable.DefaultView
        Else
            For i = 0 To medicinetable.Rows.Count - 1 Step 1
                If medicinetable.Rows(i)(1) = cbomedicines.Text Then
                    MsgBox("This medicine is already added.")
                    Return
                End If
            Next
            medicinetable.Rows.Add(cbomedicines.SelectedValue.ToString, cbomedicines.Text, txtquantity.Text)
            dgaddmedicine.ItemsSource = medicinetable.DefaultView
        End If
    End Sub

    'remove the row which is clicked on remove
    Private Sub btnremove_Click(sender As Object, e As RoutedEventArgs)
        Dim row As DataRowView = CType(dgaddmedicine.SelectedItem, DataRowView)
        Dim result As MsgBoxResult = MsgBox("Are you sure you want to remove this medicine?", MsgBoxStyle.YesNo, "Oceana Clinic")
        If result = MsgBoxResult.Yes Then
            medicinetable.Rows.Remove(row.Row)
        End If
    End Sub

    'add everything into database
    Private Sub btnsave_Click(sender As Object, e As RoutedEventArgs) Handles btnsave.Click
        'check did user search 
        If String.IsNullOrEmpty(txtpatientid.Text) Then
            'ask user to search first 
            MsgBox("Please Search before giving prescription.", MsgBoxStyle.OkOnly, "Oceana Clinic")
            Return

        Else 'got search can proceed

            ' data validation 
            If String.IsNullOrEmpty(txtdoctornotes.Text) Then
                MsgBox("Please Enter Doctor's Note.", MsgBoxStyle.OkOnly, "Oceana Clinic")
                Return
            End If


            'insert into bill table
            Dim doctornote = txtdoctornotes.Text
            Dim newdoctornotes = Replace(doctornote, "'", "''")
            Executesql("insert into Bill (PatientID, UserID, DoctorNote,PaymentStatus) values ('" & txtpatientid.Text & "', '" & userid & "','" & newdoctornotes & "','Pending')")

            'get billid for billdetails table
            Dim billid As DataTable = Executesql("select top 1 * from bill where PatientID =" & txtpatientid.Text & " order by BillID desc")
            Dim newbillid = billid.Rows(0)(0)

            'insert medicine and services into billdetails table
            'insert medicine from data grid
            For i As Integer = 0 To medicinetable.Rows.Count - 1 Step 1
                Dim msid = medicinetable.Rows(i)(0) - 6
                Dim total = selectedmedicine.Rows(msid)(3) * medicinetable.Rows(i)(2)
                Executesql("insert into billdetails (billid,msid,quantity,total) values (" & newbillid.ToString & "," & medicinetable.Rows(i)(0) & "," & medicinetable.Rows(i)(2) & "," & total & ")")
            Next

            'inset services into billdetails
            Dim servicetable As DataTable = Executesql("select * from medicineandservices")
            If chkconsultation.IsChecked = True Then
                Executesql("insert into billdetails (billid,msid,total,quantity) values (" & newbillid.ToString & "," & servicetable.Rows(0)(0) & "," & servicetable.Rows(0)(3) & ",1)")
            End If
            If chklabservices.IsChecked = True Then
                Executesql("insert into billdetails (billid,msid,total,quantity) values (" & newbillid.ToString & "," & servicetable.Rows(1)(0) & "," & servicetable.Rows(1)(3) & ",1)")
            End If
            If chkxray.IsChecked = True Then
                Executesql("insert into billdetails (billid,msid,total,quantity) values (" & newbillid.ToString & "," & servicetable.Rows(2)(0) & "," & servicetable.Rows(2)(3) & ",1)")
            End If
            If chkcomprehensivehealthcheck.IsChecked = True Then
                Executesql("insert into billdetails (billid,msid,total,quantity) values (" & newbillid.ToString & "," & servicetable.Rows(3)(0) & "," & servicetable.Rows(3)(3) & ",1)")
            End If
            If chkpartialhealthcheck.IsChecked = True Then
                Executesql("insert into billdetails (billid,msid,total,quantity) values (" & newbillid.ToString & "," & servicetable.Rows(4)(0) & "," & servicetable.Rows(4)(3) & ",1)")
            End If
            'tell user prescription is added
            MsgBox("Prescription added.", MsgBoxStyle.OkOnly, "Oceana Clinic")

            'clear everrything
            medicinetable.Clear()
            txtpatientid.Clear()
            txtname.Clear()
            txtdateofbirth.Clear()
            txtbloodtype.Clear()
            txtweight.Clear()
            txtheight.Clear()
            txtallergies.Clear()
            txtpastcondition.Clear()
            txtcurrentmedication.Clear()
            txtdoctornotes.Clear()
            chkconsultation.IsChecked = True
            chklabservices.IsChecked = False
            chkcomprehensivehealthcheck.IsChecked = False
            chkcomprehensivehealthcheck.IsChecked = False
            chkpartialhealthcheck.IsChecked = False
            btnHistory.IsEnabled = False
            btnHistory.Opacity = 0.7
        End If
    End Sub

    Private Sub btnlogout_Click(sender As Object, e As RoutedEventArgs) Handles btnlogout.Click
        Me.Close()
    End Sub



End Class
