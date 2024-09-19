Imports System.Data
Imports System.Data.OleDb
Imports System
Imports LoginGetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports ToolCCS
Imports LogFiles
Partial Class Universal_loginN_Pages_UniversalMgr
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New CryptoHelper
        Try
            Try
                lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            Catch ex As Exception
            End Try
            If Session("SBack") = Nothing Then
                Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub
    

    Protected Sub imgLogoff_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgLogoff.Click
        Dim objUpdate As New LoginUpdateData.Selectdata
        If Session("TID") <> Nothing Then
            If Session("LogInCount") <> Nothing Then
                'objUpdate.UpdateLogOffDetails2(Session("TID"), Session.SessionID, Session("UserId"), Session("LogInCount").ToString())
            Else
                'objUpdate.UpdateLogOffDetails2(Session("TID"), Session.SessionID, Session("UserId"), "")

            End If
        End If
        'Session.Abandon()
        'Session.RemoveAll()
        Response.Redirect("~/Index.aspx", True)
    End Sub
End Class
