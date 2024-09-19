Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SelectQuery


Public Class RepeatedControls
    Public Function DeptDropdown() As DropDownList
        Dim DeptCombo As New DropDownList
        Try
            Dim GetDept As New Selectdata()
            Dim Dts As New DataTable()
            Dts = GetDept.GetDeptCombo()
            DeptCombo.DataSource = Dts
            DeptCombo.DataValueField = "PROCID"
            DeptCombo.DataTextField = "DEPDES"
            DeptCombo.DataBind()
            Return DeptCombo
        Catch ex As Exception
            Throw
            Return DeptCombo
        End Try
        
    End Function

End Class
