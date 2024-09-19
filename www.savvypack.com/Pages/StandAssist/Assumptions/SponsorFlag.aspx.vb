Imports System.Data
Imports System.Data.OleDb
Imports System
Imports StandUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Session
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ID As String = Request.QueryString("Id")
        Dim SpoURL As String = Request.QueryString("Type")
        Dim objUpIns As New StandUpInsData.UpdateInsert()
        objUpIns.InsertLog1(Session("UserId").ToString(), "3", "Clicked on Sponsor Flag for Sponsor Id #" + ID + "", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, "", "", ID.ToString(), "", "")

        Response.Redirect(SpoURL, True)
        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "window.open('" + SpoURL + "','_blank')", True)
        


    End Sub
End Class
