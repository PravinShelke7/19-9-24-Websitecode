Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Echem1GetData
Imports Echem1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Echem1_Assumptions_Wizard
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim obj As New CryptoHelper
            Dim Type As String = String.Empty
            Dim WSessionId As New Integer
            Dim ObjUpIns As New Echem1UpInsData.UpdateInsert()
            Type = Request.QueryString("Type")
            WSessionId = ObjUpIns.UpdateWizard(Session("Echem1CaseId").ToString())
            Response.Redirect("~/Wizard/Default.aspx?AspSessionId=" + WSessionId.ToString() + "&Module=Echem1&Type=" + Type + "", False)
        Catch ex As Exception

        End Try
    End Sub
End Class
