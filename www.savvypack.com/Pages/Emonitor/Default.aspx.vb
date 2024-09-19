Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Emonitor_Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
        Catch ex As Exception
        End Try
        If Session("Back") = Nothing Then
            Dim obj As New CryptoHelper
            Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
        End If
    End Sub
End Class
