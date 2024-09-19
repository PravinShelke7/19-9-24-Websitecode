Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.data



Public Class DBUtil

    'Function For DataTable
    Public Function FillDataTable(ByVal StrSql As String, ByVal strConnectionString As String) As DataTable
        Dim ds As New DataSet
        Dim ada As New OleDbDataAdapter(StrSql, strConnectionString)
        Try
            ada.Fill(ds, 0)
            Return ds.Tables(0)
        Catch ex As Exception
            Throw
            Dim temp As String = ex.Message
            Return ds.Tables(0)
        Finally
            ds.Dispose()
            ada.Dispose()
        End Try

    End Function

    'Function For DataSet
    Public Function FillDataSet(ByVal StrSql As String, ByVal strConnectionString As String) As DataSet
        Dim ds As New DataSet
        Dim ada As New OleDbDataAdapter(StrSql, strConnectionString)
        Try


            ada.Fill(ds, 0)
            Return ds
        Catch ex As Exception
            Dim temp As String = ex.Message
            Return ds
        Finally
            ds.Dispose()
            ada.Dispose()
        End Try

    End Function

    

    'Function For Integer
    Public Function FillData(ByVal StrSql As String, ByVal StrMyConnection As String) As Integer


        Dim ID As Integer
        Dim con As New OleDbConnection(StrMyConnection)
        Dim cmd As New OleDbCommand()
        Dim objId As Object
        Try
            cmd.Connection = con
            con.Open()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = StrSql
            objId = cmd.ExecuteScalar()
            ID = Convert.ToInt32(objId.ToString())
            cmd.Dispose()
            Return ID
        Catch ex As Exception
            Return 0
        Finally
            con.Close()
            con.Dispose()
            cmd.Dispose()

        End Try
    End Function


    'Function For UpdateInsert Which Doesnot return anything
    Public Sub UpIns(ByVal StrSql As String, ByVal StrMyConnection As String)
        Dim cmd As New OleDbCommand()
        Dim con As New OleDbConnection(StrMyConnection)
        Try
            cmd.Connection = con
            con.Open()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = StrSql
            cmd.ExecuteNonQuery()

        Catch ex As Exception
        Finally
            con.Close()
            con.Dispose()
            cmd.Dispose()
        End Try



    End Sub



    Public Sub FillEffdate(ByVal Year As Integer, ByVal Username As String)
        Try
            Dim cmd As New OleDbCommand()
            Dim Con As New OleDbConnection()
            Con.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Con.Open()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "FILLEFFDATE"
            cmd.Connection = Con
            cmd.Parameters.AddWithValue("YEARNO", Year)
            cmd.Parameters.AddWithValue("USERNAME", Username)
            cmd.ExecuteNonQuery()
            Con.Close()

        Catch ex As Exception

        End Try
    End Sub

    Public Sub UserRebuildIndex(ByVal schema As String)
        Try
            Dim cmd As New OleDbCommand()
            Dim Con As New OleDbConnection()
            If schema = "CONTR" Then
                Con.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("ContractConnectionString")
            Else
                Con.ConnectionString = System.Configuration.ConfigurationManager.AppSettings("PackProdConnectionString")
            End If

            Con.Open()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = "USERREBUILDCONTEXTINDEX"
            cmd.Connection = Con
            cmd.ExecuteNonQuery()
            Con.Close()
        Catch ex As Exception

        End Try
    End Sub

    'Public Overloads Function GetSQLData(ByVal StrSql As String) As OleDbDataReader
    '    'Dim MyDataReader As OleDbDataReader
    '    'DBOpen()
    '    'cmd.CommandType = Data.CommandType.Text
    '    'cmd.CommandText = StrSql
    '    'MyDataReader = cmd.ExecuteReader(Data.CommandBehavior.CloseConnection)
    '    'cmd.Dispose()
    '    ''DBClose()
    '    'Return MyDataReader
    'End Function


End Class
