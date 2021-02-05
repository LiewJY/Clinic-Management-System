Imports System.Data
Imports System.Windows.Threading

Public Class Billing
    Dim selectedbillid As Integer

    Public Sub refreshbilling()
        Dim parent = TryCast(Me.Parent, Grid)
        If Not parent Is Nothing Then
            parent.Children.Remove(Me)
            parent.Children.Add(New Billing)
        End If
    End Sub
    'load pending payment into data grid
    Public Sub refreshdgpendingpayment()
        Dim paymentpending As DataTable = Executesql("Select Bill.BillID, Bill.PatientID,PatientInformation.PatientName,Bill.DoctorNote from (Bill inner join PatientInformation on Bill.PatientID = PatientInformation.PatientID) where Bill.PaymentStatus = 'Pending'")
        dgpendingpayment.ItemsSource = paymentpending.DefaultView
    End Sub
    Private Sub Billing_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        refreshdgpendingpayment()
    End Sub


    'fill in dgpayment (datagrid) and grand total
    Public Sub filldgpayment()
        Dim payment As DataTable = Executesql("select BillDetails.MSID, BillDetails.Quantity, BillDetails.Total,MedicineAndServices.MSName from ((Bill right join BillDetails on Bill.BillID = BillDetails.BillID) inner join MedicineAndServices on BillDetails.MSID = MedicineAndServices.MSID) where Bill.Billid = " & selectedbillid & " order by BillDetails.MSID")
        Dim grandtotal = Executesql("select sum(Total) from BillDetails where BillID = " & selectedbillid & "")
        dgpayment.ItemsSource = payment.DefaultView
        txtgrandtotal.Text = grandtotal(0)(0)
    End Sub

    'fill in payment when patient is selected in pending payment
    Private Sub btnselect_Click(sender As Object, e As RoutedEventArgs)
        'get the data from dgpending payment and fill selected patient details
        Dim row As DataRowView = CType(dgpendingpayment.SelectedItem, DataRowView)
        'use if to prevent error when return nothing
        If Not String.IsNullOrEmpty(row(0)) Then
            selectedbillid = row(0).ToString
        End If
        If Not String.IsNullOrEmpty(row(1)) Then
            txtpatientid.Text = row(1).ToString
        End If
        If Not String.IsNullOrEmpty(row(2)) Then
            txtname.Text = row(2).ToString
        End If
        If Not String.IsNullOrEmpty(row(3)) Then
            Dim doctornote = row(3).ToString
            Dim newdoctornotes = Replace(doctornote, "''", "'")
            txtdoctornote.Text = newdoctornotes
        End If
        filldgpayment()
    End Sub

    Private Sub btnedit_Click(sender As Object, e As RoutedEventArgs) Handles btnedit.Click
        If String.IsNullOrEmpty(txtpatientid.Text) Then
            MsgBox("Select Patient to add Medicine or Services.", MsgBoxStyle.OkOnly, "Oceana Clinic")
            Return
        End If
        ' open add medicine and serrvices and blur the current form(billing)
        Dim additems As New AddMedicineAndServices
        Me.Opacity = 0.3
        additems.BillID = selectedbillid
        additems.ShowDialog()

        'unblur billig when add medicine and services is closed
        Me.Opacity = 1
        filldgpayment()
    End Sub

    'show change ofter amount paid is inserted
    Private Sub txtamountpaid_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtamountpaid.TextChanged
        txtchange.Text = Val(txtamountpaid.Text) - Val(txtgrandtotal.Text)
    End Sub

    Private Sub btnpay_Click(sender As Object, e As RoutedEventArgs) Handles btnpay.Click
        If Val(txtamountpaid.Text) < Val(txtgrandtotal.Text) Then
            MsgBox("Amount Paid is less than grand total", MsgBoxStyle.OkOnly, "Oceana Clinic")
            Return
        End If
        Dim currentdate = Format(Now, "dd/MM/yyyy")
        Dim currenttime = Format(Now, "hh:mm tt")
        Executesql("update Bill set Dates ='" & currentdate.ToString & "', BillTime = '" & currenttime.ToString & "',GrandTotal = '" & txtgrandtotal.Text & "',AmountPaid = '" & txtamountpaid.Text & "',Change = '" & txtchange.Text & "', PaymentStatus ='Paid'  where BillID = " & selectedbillid & "")
        refreshbilling()
        refreshdgpendingpayment()
    End Sub

    'clear the form (reload the form)
    Private Sub btncancel_Click(sender As Object, e As RoutedEventArgs) Handles btncancel.Click
        refreshbilling()
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As RoutedEventArgs) Handles btnrefresh.Click
        refreshdgpendingpayment()
    End Sub
End Class
