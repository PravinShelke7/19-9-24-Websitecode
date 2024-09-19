Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SDistGetData
Imports SDistUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_SDistribution_Default
    Inherits System.Web.UI.Page
#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_SDistribution_DEFAULT")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Try
                lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            Catch ex As Exception
            End Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                SetSessions()
                GetBCaseDetails()
                GetPCaseDetails()
            End If
            SetToolButton()
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub SetSessions()
        Dim objGetData As New SDistGetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetUserDetails(Session("ID"))
            Session("UserId") = ds.Tables(0).Rows(0).Item("USERID").ToString()
            Session("SDistUserRole") = ds.Tables(0).Rows(0).Item("USERROLE").ToString()
            Session("SDistServiceRole") = ds.Tables(0).Rows(0).Item("SERVIECROLE").ToString()
            Session("SDistUserName") = ds.Tables(0).Rows(0).Item("USERNAME").ToString()
            Session("SDistToolUserName") = ds.Tables(0).Rows(0).Item("TOOLUSERNAME").ToString()
            Session("SDistMaxCaseCount") = ds.Tables(0).Rows(0).Item("MAXCASECOUNT").ToString()
        Catch ex As Exception
            lblError.Text = "Error:SetSessions:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetBCaseDetails()
        Dim objGetData As New SDistGetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetBCaseDetails()
            With ddlBaseCases
                .DataSource = ds
                .DataTextField = "CASEDES"
                .DataValueField = "CASEID"
                .DataBind()
                .Font.Size = 8
            End With
        Catch ex As Exception
            lblError.Text = "Error:GetBCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetPCaseDetails()
        Dim objGetData As New SDistGetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetPCaseDetails(Session("USERID").ToString())
            With ddlPCase
                .DataSource = ds
                .DataTextField = "CASEDES"
                .DataValueField = "CASEID"
                .DataBind()
                .Font.Size = 8
            End With
            If ds.Tables(0).Rows.Count <= 0 Then
                btnPCase.Visible = False
                btRename.Visible = False
                btSerach.Visible = False
                ddlPCase.Visible = False
                lblPCase.Visible = True
            Else
                btnPCase.Visible = True
                btRename.Visible = True
                btSerach.Visible = True
                ddlPCase.Visible = True
                lblPCase.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetPCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub SetToolButton()
        Try
            If Session("SDistServiceRole") <> "ReadWrite" Then
                btnToolBox.Enabled = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:SetToolButton:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnBCase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBCase.Click
        Try
            Session("SDistCaseId") = ddlBaseCases.SelectedValue.ToString()
            If Not objRefresh.IsRefresh Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinB", "OpenNewWindow('Assumptions/CaseManager.aspx');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnBCase_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnPCase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPCase.Click
        Try
            Session("SDistCaseId") = ddlPCase.SelectedValue.ToString()
            If Not objRefresh.IsRefresh Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "OpenNewWindow('Assumptions/CaseManager.aspx');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnPCase_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnToolBox_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnToolBox.Click
        Try
            Response.Redirect("Tools/Tool.aspx", True)
        Catch ex As Exception
            lblError.Text = "Error:btnToolBox_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim objUpIns As New SDistUpInsData.UpdateInsert()
        Dim ds As New DataSet()
        Try
            objUpIns.CaseDesUpdate(txtCaseid.Value, txtCaseDe1.Text.Trim().ToString().Replace("'", "''"), txtCaseDe2.Text.Trim().ToString().Replace("'", "''"))
            GetPCaseDetails()
        Catch ex As Exception

        End Try
    End Sub
End Class
