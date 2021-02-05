Imports System.Data

Public Class AddMedicineAndServices

    'billid information from billing form
    Public Property BillID As Integer

    ' public variable for this form
    Dim selectedmedicine As DataTable = Executesql("select * from  MedicineAndServices where type = 'Medicine' order by MSName")
    Dim selectedservices As DataTable = Executesql("select * from  MedicineAndServices where type = 'Services'")
    Dim medicinetable As DataTable
    Dim servicetable As DataTable

    'load data into cmbmedicine
    'load all the current medicine and services into here
    Private Sub AddMedicineAndServices_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        'combobox medicine
        cbomedicines.ItemsSource = selectedmedicine.DefaultView
        cbomedicines.DisplayMemberPath = "MSName"
        cbomedicines.SelectedValuePath = "MSID"

        'combobox services
        cboservices.ItemsSource = selectedservices.DefaultView
        cboservices.DisplayMemberPath = "MSName"
        cboservices.SelectedValuePath = "MSID"

        'medicine
        medicinetable = Executesql("select BillDetails.MSID,MedicineAndServices.MSName, BillDetails.Quantity from ((Bill right join BillDetails on Bill.BillID = BillDetails.BillID) inner join MedicineAndServices on BillDetails.MSID = MedicineAndServices.MSID) where Bill.Billid = " & BillID & " and MedicineAndServices.MSID > 5 order by BillDetails.MSID")
        dgaddmedicine.ItemsSource = medicinetable.DefaultView

        'service
        servicetable = Executesql("select BillDetails.MSID,MedicineAndServices.MSName, BillDetails.Quantity from ((Bill right join BillDetails on Bill.BillID = BillDetails.BillID) inner join MedicineAndServices on BillDetails.MSID = MedicineAndServices.MSID) where Bill.Billid = " & BillID & " and MedicineAndServices.MSID < 6 order by BillDetails.MSID")
        dgservices.ItemsSource = servicetable.DefaultView
    End Sub

    'remove medicine
    Private Sub btnremove_Click(sender As Object, e As RoutedEventArgs)
        Dim result As MsgBoxResult = MsgBox("Are you sure you want to remove this medicine?", MsgBoxStyle.YesNo, "Oceana Clinic")
        Dim row As DataRowView = CType(dgaddmedicine.SelectedItem, DataRowView)
        Dim remove = row(0).ToString()
        If result = MsgBoxResult.Yes Then
            Executesql("delete from BillDetails where BillID = " & BillID & " and  MSID = " & remove & "")
            medicinetable.Rows.Remove(row.Row)
        End If
    End Sub

    'add medicine when clicked
    Public Sub addmedicine()
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

        'insert medicine from database to cbo
        Dim msid = cbomedicines.SelectedValue - 6
        Dim total = selectedmedicine.Rows(msid)(3) * txtquantity.Text
        Executesql("insert into BillDetails (BillID, MSID, Quantity, Total) values (" & BillID & ", " & cbomedicines.SelectedValue & ", " & txtquantity.Text & "," & total & ")")

        'refresh grid
        medicinetable = Executesql("select BillDetails.MSID,MedicineAndServices.MSName, BillDetails.Quantity from ((Bill right join BillDetails on Bill.BillID = BillDetails.BillID) inner join MedicineAndServices on BillDetails.MSID = MedicineAndServices.MSID) where Bill.Billid = " & BillID & " and MedicineAndServices.MSID > 5 order by BillDetails.MSID")
        dgaddmedicine.ItemsSource = medicinetable.DefaultView
    End Sub
    Private Sub btnadd_Click(sender As Object, e As RoutedEventArgs) Handles btnadd.Click

        If medicinetable.Rows.Count = 0 Then
            addmedicine()
        Else
            For i = 0 To medicinetable.Rows.Count - 1 Step 1
                If medicinetable.Rows(i)(1) = cbomedicines.Text Then
                    MsgBox("This medicine is already added.")
                    Return
                End If
            Next
            addmedicine()
        End If

    End Sub

    'remove service
    Private Sub btnremoveservice_Click(sender As Object, e As RoutedEventArgs)
        Dim result As MsgBoxResult = MsgBox("Are you sure you want to remove this service?", MsgBoxStyle.YesNo, "Oceana Clinic")
        Dim row As DataRowView = CType(dgservices.SelectedItem, DataRowView)
        Dim remove = row(0).ToString()
        If result = MsgBoxResult.Yes Then
            Executesql("delete from BillDetails where BillID = " & BillID & " and  MSID = " & remove & "")
            servicetable.Rows.Remove(row.Row)
        End If
    End Sub
    'add service when clicked
    Public Sub addservices()
        If String.IsNullOrEmpty(cboservices.Text) Then
            MsgBox("Please select a service to add.", MsgBoxStyle.OkOnly, "Oceana Clinic")
            Return
        End If
        Dim msid = cboservices.SelectedValue - 1
        Dim unitprice = selectedservices.Rows(msid)(3)
        Executesql("insert into BillDetails (BillID, MSID, Quantity, Total) values (" & BillID & ", " & cboservices.SelectedValue & ", 1 ," & unitprice & ")")

        'refresh grid
        servicetable = Executesql("select BillDetails.MSID,MedicineAndServices.MSName, BillDetails.Quantity from ((Bill right join BillDetails on Bill.BillID = BillDetails.BillID) inner join MedicineAndServices on BillDetails.MSID = MedicineAndServices.MSID) where Bill.Billid = " & BillID & " and MedicineAndServices.MSID < 6 order by BillDetails.MSID")
        dgservices.ItemsSource = servicetable.DefaultView
    End Sub
    Private Sub btnaddservices_Click(sender As Object, e As RoutedEventArgs) Handles btnaddservices.Click

        If selectedservices.Rows.Count = 0 Then
            addservices()
        Else
            For i = 0 To servicetable.Rows.Count - 1 Step 1
                If servicetable.Rows(i)(1) = cboservices.Text Then
                    MsgBox("This service is already added.")
                    Return
                End If
            Next
            addservices()
        End If
    End Sub
    ' close the form when back is clicked
    Private Sub btnback_Click(sender As Object, e As RoutedEventArgs) Handles btnback.Click
        Me.Close()
    End Sub


End Class
