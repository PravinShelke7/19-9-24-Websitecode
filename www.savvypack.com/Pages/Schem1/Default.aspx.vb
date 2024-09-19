Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Schem1GetData
Imports Schem1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Schem1_Default
    Inherits System.Web.UI.Page
#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_SCHEM1_DEFAULT")
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
                Session("EquipMod") = "Schem1"

                SetSessions()
                GetPCaseGroupDetails()
                GetBCaseDetails()
                GetPCaseDetails()
                btSerach.Attributes.Add("onclick", "return ShowPopWindow('PopUp/CaseSearch.aspx?Id=ddlPCase&GrpID=" + ddlCaseGroup.SelectedValue + "')")
            End If
            SetToolButton()
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub GetPCaseGroupDetails()
        Dim Dts As New DataSet
        Dim objGetData As New Schem1GetData.Selectdata
        Dim lst As New ListItem

        Try
            lst.Text = "-------------All Groups and All Cases------------"
            lst.Value = "0"
            lst.Selected = True
            ddlCaseGroup.Items.Add(lst)
            ddlCaseGroup.AppendDataBoundItems = True

            Dts = objGetData.GetGroupCaseDetails(Session("USERID").ToString())
            If Dts.Tables(0).Rows.Count > 0 Then
                ddlCaseGroup.DataSource = Dts
                ddlCaseGroup.DataValueField = "GROUPID"
                ddlCaseGroup.DataTextField = "GROUPDES"
                ddlCaseGroup.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = "Error:GetPCaseGroupDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub SetSessions()
        Dim objGetData As New Schem1GetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetUserDetails(Session("ID"))
            Session("UserId") = ds.Tables(0).Rows(0).Item("USERID").ToString()
            Session("Schem1UserRole") = ds.Tables(0).Rows(0).Item("USERROLE").ToString()
            Session("Schem1ServiceRole") = ds.Tables(0).Rows(0).Item("SERVIECROLE").ToString()
            Session("Schem1UserName") = ds.Tables(0).Rows(0).Item("USERNAME").ToString()
            Session("Schem1ToolUserName") = ds.Tables(0).Rows(0).Item("TOOLUSERNAME").ToString()
            Session("Schem1MaxCaseCount") = ds.Tables(0).Rows(0).Item("MAXCASECOUNT").ToString()

            'Set for License Administrator
            Session("Schem1LicAdmin") = ds.Tables(0).Rows(0).Item("ISIADMINLICUSR").ToString()
            Session("Schem1LicenseId") = ds.Tables(0).Rows(0).Item("LICENSEID").ToString()


        Catch ex As Exception
            lblError.Text = "Error:SetSessions:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetBCaseDetails()
        Dim objGetData As New Schem1GetData.Selectdata()
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
        Dim objGetData As New Schem1GetData.Selectdata()
        Dim ds As New DataSet()
        Try
            If ddlCaseGroup.SelectedValue <> "0" Then
                ds = objGetData.GetPCaseDetailsByGroup(Session("USERID").ToString(), ddlCaseGroup.SelectedValue)
            Else
                If Session("Schem1LicAdmin") = "N" Then
                    ds = objGetData.GetPCaseDetails(Session("USERID").ToString())
                Else
                    ds = objGetData.GetPCaseDetailsByLicense(Session("USERID").ToString())
                End If
            End If

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
                If ddlCaseGroup.SelectedValue <> "0" Then
                    lblPCase.Visible = True
                    lblPCase.Text = "You currently have no Proprietary Cases assigned to this Group."
                Else
                    lblPCase.Visible = True
                End If
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
            If Session("Schem1ServiceRole") <> "ReadWrite" Then
                btnToolBox.Enabled = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:SetToolButton:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnBCase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBCase.Click
        Try
            Session("Schem1CaseId") = ddlBaseCases.SelectedValue.ToString()
            If Not objRefresh.IsRefresh Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinB", "OpenNewWindow('Assumptions/CaseManager.aspx');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnBCase_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnPCase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPCase.Click
        Try
            Session("Schem1CaseId") = ddlPCase.SelectedValue.ToString()
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
        Dim objUpIns As New Schem1UpInsData.UpdateInsert()
        Dim ds As New DataSet()
        Try
            objUpIns.CaseDesUpdate(txtCaseid.Value, txtCaseDe1.Text.Trim().Replace("'", "''").ToString(), txtCaseDe2.Text.Trim().Replace("'", "''").ToString())
            GetPCaseDetails()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub ddlCaseGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCaseGroup.SelectedIndexChanged
        Try
            btSerach.Attributes.Add("onclick", "return ShowPopWindow('PopUp/CaseSearch.aspx?Id=ddlPCase&GrpID=" + ddlCaseGroup.SelectedValue + "')")
            GetPCaseDetails()
        Catch ex As Exception

        End Try
    End Sub
End Class
