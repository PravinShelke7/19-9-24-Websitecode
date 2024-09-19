Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System

Public Class Configration
    Public Class Selectdata
        Dim ConfigurationConnection As String = System.Configuration.ConfigurationManager.AppSettings("ConfigurationConnectionString")
  
#Region "UserDetails"
    Public Function GetChartSettings() As DataSet
        Dim Dts As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Try
                StrSql = "SELECT EXTSERVERADD, INTCOMPORTADD FROM CHARTSETTING "
                Dts = odbUtil.FillDataSet(StrSql, ConfigurationConnection)
                Return Dts
        Catch ex As Exception
                Throw New Exception("Configration:GetChartSettings:" + ex.Message.ToString())
            Return Dts
        End Try
    End Function
#End Region

    End Class

End Class
