Imports LoginGetData.Selectdata
Imports System.Data.SqlClient
Imports System.Data

Partial Class Universal_loginN_Pages_AddEditUser
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            Catch ex As Exception
            End Try
            Dim type As String
            Dim userId As String
            type = Request.QueryString("Type")
            If type = "Edit" Then
                dvAddUser.Style.Add("display", "none")
                dvEditUser.Style.Add("display", "inline")
                userId = Request.QueryString("ID")
                getUserDetails(userId)
            Else
                dvAddUser.Style.Add("display", "inline")
                dvEditUser.Style.Add("display", "none")
            End If
        End If
    End Sub
    Public Sub getUserDetails(ByVal uid As String)
        Dim objGetData As New LoginGetData.Selectdata
        Dim ds As New dataset
        ds = objGetData.GetUserDetailsByID(uid)
        If ds.Tables(0).Rows.Count > 0 Then
            txtEUserName.Text = ds.Tables(0).Rows(0).Item("USERNAME").ToString()
            txtEPassword.Text = ds.Tables(0).Rows(0).Item("Password").ToString()
            txtCompany.Text = ds.Tables(0).Rows(0).Item("Company").ToString()
        End If
    End Sub

    Protected Sub btnECancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnECancel.Click
        Response.Redirect("CorpUser.aspx", False)
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("CorpUser.aspx", False)
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            Dim objUpdate As New LoginUpdateData.Selectdata
            objUpdate.UpdatePassword(Request.QueryString("Id"), txtEPassword.Text)
            Response.Redirect("CorpUser.aspx", False)
        Catch ex As Exception
            lblError.Text = "Error:btnUpdate_Click:" + ex.Message.ToString() + ""
        End Try

    End Sub

    Protected Sub btnAddUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddUser.Click
        Try
            Dim objUpdate As New LoginUpdateData.Selectdata
            Dim objGetData As New LoginGetData.Selectdata
            Dim ds As New DataSet()
            ds = objGetData.GetUserDetailsByName(txtUserName.Text.ToUpper())
            If ds.Tables(0).Rows.Count > 0 Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + TCaseId.ToString() + " created successfully.\nCase #" + SCaseId.ToString() + " variables transferred to Case #" + TCaseId.ToString() + " successfully.');", True)
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('A User account already exists for this email address.Please use a different email address to create a new account.  Or, if you have forgotten your password to your original account, let us know and we will email it to you.')", True)
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Win3", "MessageWindow();", True)
            Else
                objUpdate.InsertUserDetails(txtUserName.Text, txtPassword.Text, Session("CUserName").ToString(), Session("CPassword").ToString())
                Response.Redirect("CorpUser.aspx", False)
            End If

            
        Catch ex As Exception
            lblError.Text = "Error:btnAddUser_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class
