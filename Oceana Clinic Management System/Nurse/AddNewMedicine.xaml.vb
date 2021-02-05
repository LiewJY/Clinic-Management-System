Imports System.Data

Public Class AddNewMedicine
    'public variable use in this form
    Dim showmedicine As DataTable = Executesql("select * from MedicineAndServices where MSID > 5 order by MSID")
    Dim title As String = "Oceana Clinic"
    Dim btn As MessageBoxButton = MessageBoxButton.OK
    Dim icon As MessageBoxImage = MessageBoxImage.Exclamation

    'to remove the watermark
    Private Sub txtsearch_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtsearch.TextChanged
        If txtsearch.Text.Length > 0 Then
            tbsearch.Visibility = Visibility.Collapsed
        Else
            tbsearch.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txtname_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtname.TextChanged
        If txtname.Text.Length > 0 Then
            tbname.Visibility = Visibility.Collapsed
        Else
            tbname.Visibility = Visibility.Visible
        End If
    End Sub
    Private Sub txtprice_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtprice.TextChanged
        If txtprice.Text.Length > 0 Then
            tbprice.Visibility = Visibility.Collapsed
        Else
            tbprice.Visibility = Visibility.Visible
        End If
    End Sub


    'enter to focus on the next control
    Private Sub txtName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtname.KeyDown
        If e.Key = Key.Enter Then
            txtprice.Focus()
        End If
    End Sub
    Private Sub txtprice_KeyDown(sender As Object, e As KeyEventArgs) Handles txtprice.KeyDown
        If e.Key = Key.Enter Then
            btnaddmedicine.Focus()
        End If
    End Sub

    'load data into data grid
    Private Sub AddNewMedicine_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        dgmedicine.ItemsSource = showmedicine.DefaultView
        Dim shownextmsid As DataTable = Executesql("select top 1 * from MedicineAndServices order by MSID desc")
        Dim newmsid = shownextmsid.Rows(0)(0)
        txtmsid.Text = newmsid + 1
    End Sub

    ' reload the form when cancel is clicked
    Private Sub btncancel_Click(sender As Object, e As RoutedEventArgs) Handles btncancel.Click
        Dim parent = TryCast(Me.Parent, Grid)
        If Not parent Is Nothing Then
            parent.Children.Remove(Me)
        End If
        parent.Children.Add(New AddNewMedicine)

    End Sub

    'search
    Private Sub medicinesearch()
        If txtsearch.Text.Length = 0 Then
            MsgBox("Please enter a valid Medicine ID or medicine name to search.", MsgBoxStyle.OkOnly, "Oceana Clinic")
            Return
        End If
        'search using medicine id 
        If IsNumeric(txtsearch.Text) Then
            Dim searchmedicineid As DataTable = Executesql("select * from MedicineAndServices where MSID = " & txtsearch.Text & "")
            'return error medicine id not found
            If searchmedicineid.Rows.Count = 0 Then
                MsgBox("Please enter a valid Medicine ID.", MsgBoxStyle.OkOnly, "Oceana Clinic")
                Return
            End If
            'fill in when medicine id is found
            txtsearchmsid.Text = searchmedicineid.Rows(0)(0)
            txtsearchname.Text = searchmedicineid.Rows(0)(1)
            txtsearchprice.Text = searchmedicineid.Rows(0)(3)


            txtsearch.Clear()
            'search using medicine name
        ElseIf Not IsNumeric(txtsearch) Then
            Dim searchmedicinename As DataTable = Executesql("select * from MedicineAndServices where MSID = " & txtsearch.Text & "")
            'return error medicine name not found
            If searchmedicinename.Rows.Count = 0 Then
                MsgBox("Please enter a valid Medicine Name.", MsgBoxStyle.OkOnly, "Oceana Clinic")
                Return
            End If
            'fill in when medicine name is found
            txtsearchmsid.Text = searchmedicinename.Rows(0)(0)
            txtsearchname.Text = searchmedicinename.Rows(0)(1)
            txtsearchprice.Text = searchmedicinename.Rows(0)(3)


            txtsearch.Clear()
        Else ' medicine id and medicine name dose not exist
            MsgBox("Please enter a valid Medicine ID or Medicine Name to search.", MsgBoxStyle.OkOnly, "Oceana Clinic")
        End If


    End Sub
    Private Sub btnsearch_Click(sender As Object, e As RoutedEventArgs) Handles btnsearch.Click
        medicinesearch()
    End Sub
    'same but use enter key
    Private Sub txtsearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtsearch.KeyDown
        If e.Key = Key.Enter Then
            medicinesearch()
        End If
    End Sub

    Private Sub btnaddmedicine_Click(sender As Object, e As RoutedEventArgs) Handles btnaddmedicine.Click

        ' check for empty field and focus it
        If String.IsNullOrEmpty(txtname.Text) Then
            MessageBox.Show("Please Enter Medicine Name.", title, btn, icon)
            txtname.Focus()
            Return
        End If
        If String.IsNullOrEmpty(txtprice.Text) Then
            MessageBox.Show("Please Enter Unit Price.", title, btn, icon)
            txtprice.Focus()
            Return
        End If

        'data validation
        If IsNumeric(txtprice.Text) Then
        Else
            MsgBox("Quantity contains numbers only.", MsgBoxStyle.OkOnly, "Oceana Clinic")
            Return
        End If

        Executesql("insert into MedicineAndServices (MSName, UnitPrice, Type) values ('" & txtname.Text & "', '" & txtprice.Text & "', 'Medicine')")
        MsgBox("Medicine successfully added.", MsgBoxStyle.OkOnly, "Oceana Clinic")

        'refresh the data grid
        Dim showmedicine As DataTable = Executesql("select * from MedicineAndServices where MSID > 5 order by MSID")
        dgmedicine.ItemsSource = showmedicine.DefaultView

    End Sub
    'remove medicine
    Private Sub btnremove_Click(sender As Object, e As RoutedEventArgs)
        Dim result As MsgBoxResult = MsgBox("Are you sure you want to remove this medicine from the database?", MsgBoxStyle.YesNo, "Oceana Clinic")
        Dim row As DataRowView = CType(dgmedicine.SelectedItem, DataRowView)
        Dim remove = row(0).ToString()
        If result = MsgBoxResult.Yes Then
            Executesql("delete from MedicineAndServices where MSID = " & txtmsid.Text & "")
            showmedicine.Rows.Remove(row.Row)
        End If
    End Sub

    Private Sub btncancel2_Click(sender As Object, e As RoutedEventArgs) Handles btncancel2.Click
        Dim parent = TryCast(Me.Parent, Grid)
        If Not parent Is Nothing Then
            parent.Children.Remove(Me)
        End If
        parent.Children.Add(New AddNewMedicine)
    End Sub

    Private Sub btnsave_Click(sender As Object, e As RoutedEventArgs) Handles btnsave.Click
        ' check for empty field and focus it

        If String.IsNullOrEmpty(txtsearchname.Text) Then
            MessageBox.Show("Please Enter Medicine Name Before Updating.", title, btn, icon)
            txtname.Focus()
            Return
        End If
        If String.IsNullOrEmpty(txtsearchprice.Text) Then
            MessageBox.Show("Please Enter Unit Price Before Updating.", title, btn, icon)
            txtprice.Focus()
            Return
        End If

        'data validation
        If IsNumeric(txtsearchprice.Text) Then
        Else
            MsgBox("Quantity contains numbers only.", MsgBoxStyle.OkOnly, "Oceana Clinic")
            Return
        End If

        Executesql("update MedicineAndServices set MSName='" & txtsearchname.Text & "', UnitPrice = '" & txtsearchprice.Text & "' where MSID = " & txtsearchmsid.Text & "")
        MsgBox("Medicine successfully edited.", MsgBoxStyle.OkOnly, "Oceana Clinic")

        'refresh the data grid
        Dim showmedicine As DataTable = Executesql("select * from MedicineAndServices where MSID > 5 order by MSID")
        dgmedicine.ItemsSource = showmedicine.DefaultView
    End Sub
End Class
