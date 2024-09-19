Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.data

Namespace Allied.DataBaseUtility.DbUtil

    Public Class DbUtil

#Region "Function For DataTable"
        '<summery>

        '</summery
        Public Function FillDataTable(ByVal StrSql As String, ByVal strConnectionString As String) As DataTable
            Dim ds As New DataSet
            Dim ada As New OleDbDataAdapter(StrSql, strConnectionString)
            Try
                ada.Fill(ds, 0)
                Return ds.Tables(0)
            Catch ex As Exception
                Throw ex
            Finally
                ds.Dispose()
                ada.Dispose()
            End Try

        End Function
#End Region

#Region "Function For DataSet"
        'Function For DataSet
        Public Function FillDataSet(ByVal StrSql As String, ByVal strConnectionString As String) As DataSet
            Dim ds As New DataSet
            Dim ada As New OleDbDataAdapter(StrSql, strConnectionString)
            Try
                ada.Fill(ds, 0)
                Return ds
            Catch ex As Exception
                Throw ex
            Finally
                ds.Dispose()
                ada.Dispose()
            End Try

        End Function
#End Region

#Region "Function For Integer"
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
#End Region

#Region "Function For UpdateInsert"
        'Function For UpdateInsert
        Public Function UpIns(ByVal StrSql As String, ByVal StrMyConnection As String) As Integer
            Dim cmd As New OleDbCommand()
            Dim con As New OleDbConnection(StrMyConnection)
            Dim rowaff As New Integer
            Try
                cmd.Connection = con
                con.Open()
                cmd.CommandType = CommandType.Text
                cmd.CommandText = StrSql
                rowaff = cmd.ExecuteNonQuery()
                Return rowaff
            Catch ex As Exception
                Throw ex
            Finally
                con.Close()
                con.Dispose()
                cmd.Dispose()
            End Try



        End Function
#End Region

    End Class

End Namespace

