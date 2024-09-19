Imports System.Data
Imports System.Data.OleDb
Imports System
Imports LoginGetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Universal_loginN_Pages_ULogRenewal
    Inherits System.Web.UI.Page
    Dim _strSchema As String
    Dim _strSerID As String

    Public Property Schema() As String
        Get
            Return _strSchema
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _strSchema = value
        End Set
    End Property

    Public Property ServID() As String
        Get
            Return _strSerID
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _strSerID = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            Schema = Request.QueryString("Schema").ToString()
            ServID = Request.QueryString("ServID").ToString()
            If Not IsPostBack Then
                txtUserName.Focus()
            End If
            txtUserName.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnSubmit.ClientID + "')")
            txtPassword.Attributes.Add("OnkeyPress", "return clickButton(event,'" + btnSubmit.ClientID + "')")
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim objGetData As New LoginGetData.Selectdata()
        Dim objUpdateData As New LoginUpdateData.Selectdata()
        Dim ds As New DataSet()
        Dim obj As New CryptoHelper
        Try
            If txtUserName.Text.Trim() <> "" And txtPassword.Text.Trim() <> "" Then
                ds = objGetData.ValidateUser(txtUserName.Text.Trim(), txtPassword.Text.Trim())
                If ds.Tables(0).Rows.Count > 0 Then
                    If txtUserName.Text.Trim().ToUpper() = Session("UserName").ToString().ToUpper() And txtPassword.Text.Trim().ToUpper() = Session("Password").ToString().ToUpper() Then
                        objUpdateData.UpdateInuseLogoutLog(txtUserName.Text.Trim(), txtPassword.Text.Trim())
                        'Response.Redirect("~/Index.aspx")
                        Response.Redirect("ModuleRedirect.aspx?ServID=" + ServID + "&Schema=" + Schema + "", False)
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('The LoggedIn User is " + Session("UserName") + " and you are trying to renew for different User " + txtUserName.Text + "');", True)
                    End If

                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('The EMAIL or PASSWORD that you entered could not be authenticated.');", True)
                    'Response.Redirect("ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnSubmit_Click:" + ex.Message.ToString()
        End Try
    End Sub

  
End Class
