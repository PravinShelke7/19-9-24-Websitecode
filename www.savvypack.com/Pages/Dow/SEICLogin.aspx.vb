Imports System.Data
Imports System.Data.OleDb
Imports System
Imports LoginGetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Dow_SEICLogin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
        Catch ex As Exception
        End Try
        txtUserName.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnSubmit.ClientID + "')")
        txtPass.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnSubmit.ClientID + "')")
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet
        Dim obj As New CryptoHelper
        Try
            ds = objGetData.ValidateDowUser(txtUserName.Text.Trim(), txtPass.Text.Trim().Replace("'", "''"))
            If ds.Tables(0).Rows.Count > 0 Then
                'Response.Redirect("../../DowChemical/SecurityCheck.aspx?Id=DOW")
                Response.Redirect("http://seic.allied-dev.com/SecurityCheck.aspx?Id=DOW")
            Else
                Response.Redirect("ErrorDow.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnSubmit_Click:" + ex.Message.ToString()
        End Try
    End Sub
End Class
