Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System

Public Class InteractiveGetData
    Public Class Selectdata
        Dim ShoppingConnection As String = System.Configuration.ConfigurationManager.AppSettings("ShoppingConnectionString")
        Public Function GetInventoryDetails(ByVal ReportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select * from Inventory WHERE "
                StrSql = StrSql + "PARTID ='" + ReportId + "' "
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("InteractiveGetData:GetInventoryDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
    End Class
End Class
