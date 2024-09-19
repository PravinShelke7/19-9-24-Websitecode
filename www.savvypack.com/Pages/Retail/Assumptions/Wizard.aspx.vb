Imports System.Data
Imports System.Data.OleDb
Imports System
Imports RetlGetData
Imports RetlUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Retail_Assumptions_Wizard
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim obj As New CryptoHelper
            Dim Type As String = String.Empty
            Dim WSessionId As New Integer
            Dim ObjUpIns As New RetlUpInsData.UpdateInsert()
            Type = Request.QueryString("Type")
            WSessionId = ObjUpIns.UpdateWizard(Session("RetlCaseId").ToString())
            Response.Redirect("~/Wizard/Default.aspx?AspSessionId=" + WSessionId.ToString() + "&Module=Retl&Type=" + Type + "", False)
        Catch ex As Exception

        End Try
    End Sub
End Class
