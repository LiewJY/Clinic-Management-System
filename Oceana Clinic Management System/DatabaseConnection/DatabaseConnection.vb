Imports System.Data

Module DatabaseConnection
    Function Executesql(sql As String) As DataTable
        Try
            Dim connection As New OleDb.OleDbConnection
            Dim adapter As New OleDb.OleDbDataAdapter
            Dim data As New DataTable
            Dim dbproviderb As String
            Dim dbsource As String

            dbproviderb = "PROVIDER= Microsoft.JET.OLEDB.4.0;"
            dbsource = "Data Source=|DataDirectory|\Oceana Clinic Management System.mdb"
            connection.ConnectionString = dbproviderb & dbsource

            connection.Open()
            adapter = New OleDb.OleDbDataAdapter(sql, connection)
            adapter.Fill(data)

            connection.Close()
            connection = Nothing

            Return data
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Function
End Module
