Imports System
Imports System.Data
Imports System.Net.Mail
Imports System.Data.OleDb
Imports LoginGetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports ToolCCS
Imports LogFiles

Partial Class _Index
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            ' Dim UrlName As String = Request.Url.ToString()
            Dim UrlName = Request.Url.AbsoluteUri
            'Dim UrlName As String = "http://localhost:64530/www.savvypack.com/Index.aspx?ID=ALLIED"
            'Session("URL") = UrlName
            If Not IsPostBack Then
                If Request.QueryString("ID") = "ALLIED" Then
                    If Session("ALLIED") <> True Then
                        Session("ALLIED") = True
                        ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "ShowPopWindow('RedirectMessage_Popup.aspx');", True)
                    End If

                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class