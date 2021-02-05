Imports System.Data

Public Class PatientHistory

    'data recieved from doctor form
    Public Property patientid As Integer
    Public Property patientname As String

    'to be used in this form
    Dim displaypatientmedicinehistory As DataTable = Executesql("select * from bill")

    'clear all textbox and others
    Public Sub clearall()
        txtdoctornotes.Clear()
        displaypatientmedicinehistory.Clear()
        chkconsultation.IsChecked = False
        chklabservices.IsChecked = False
        chkxray.IsChecked = False
        chkcomprehensivehealthcheck.IsChecked = False
        chkpartialhealthcheck.IsChecked = False
    End Sub

    'load name and date when patient history is opened
    Private Sub PatientHistory_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        'clear first 
        clearall()

        'show the patient ID and patient name in the textbox (value retrived from doctor form)
        txtname.Text = patientname
        txtpatientid.Text = patientid

        'fill in the combobox with date
        Dim patienthistory As DataTable = Executesql("select * from Bill where PatientID = " & txtpatientid.Text & " and PaymentStatus = 'Paid' order by Dates desc")
        cbodate.ItemsSource = patienthistory.DefaultView
        cbodate.DisplayMemberPath = "Dates"
        cbodate.ItemStringFormat = "dd/MM/yyyy"
        cbodate.SelectedValuePath = "BillID"
    End Sub

    Private Sub cmbdate_DropDownClosed(sender As Object, e As EventArgs) Handles cbodate.DropDownClosed
        If String.IsNullOrEmpty(cbodate.SelectedValue) Then
            Return
        End If
        'clear first 
        clearall()

        'dispaly medicine
        displaypatientmedicinehistory = Executesql("select * from ((Bill right join BillDetails on Bill.BillID = BillDetails.BillID)  inner join MedicineAndServices on BillDetails.MSID = MedicineAndServices.MSID) where Bill.BillID = " & cbodate.SelectedValue & " and MedicineAndServices.MSID > 5 and Bill.PaymentStatus ='Paid'")
        dgmedicine.ItemsSource = displaypatientmedicinehistory.DefaultView



        'dispaly services
        Dim displaypatientservicehistory As DataTable = Executesql("select BillDetails.MSID,Bill.DoctorNote from (Bill right join BillDetails on Bill.BillID = BillDetails.BillID) where Bill.BillID = " & cbodate.SelectedValue & " and Bill.PaymentStatus = 'Paid'")
        For i As Integer = 0 To displaypatientservicehistory.Rows.Count - 1 Step 1
            If displaypatientservicehistory.Rows(i)(0) = 1 Then
                chkconsultation.IsChecked = True
            End If
            If displaypatientservicehistory.Rows(i)(0) = 2 Then
                chklabservices.IsChecked = True
            End If
            If displaypatientservicehistory.Rows(i)(0) = 3 Then
                chkxray.IsChecked = True
            End If
            If displaypatientservicehistory.Rows(i)(0) = 4 Then
                chkcomprehensivehealthcheck.IsChecked = True
            End If
            If displaypatientservicehistory.Rows(i)(0) = 5 Then
                chkpartialhealthcheck.IsChecked = True
            End If

        Next
        Dim doctornote = displaypatientservicehistory(0)(1)
        Dim newdoctornotes = Replace(doctornote, "''", "'")
        txtdoctornotes.Text = newdoctornotes

    End Sub

    Private Sub btnback_Click(sender As Object, e As RoutedEventArgs) Handles btnback.Click
        Dim parent = TryCast(Me.Parent, Grid)
        If Not parent Is Nothing Then
            parent.Children.Remove(Me)
        End If
    End Sub
End Class
