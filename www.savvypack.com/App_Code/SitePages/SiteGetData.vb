Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System

Public Class SiteGetData
    Public Class Selectdata
        Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

        Public Function GetClients() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT NAME  "
                StrSql = StrSql + "FROM CLIENTLIST "
                StrSql = StrSql + "ORDER BY NAME "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SiteGetData:GetClients:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        

    End Class
End Class
