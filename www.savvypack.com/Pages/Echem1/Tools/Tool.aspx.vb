Imports System.Data
Imports System.Data.OleDb
Imports System
'Imports E1GetData
'Imports E1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports ToolCCS
Partial Class Pages_Echem1_Tools_Tool
    Inherits System.Web.UI.Page
#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_ECHEM1_TOOLS_TOOL")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Session("Echem1UserRole") = "AADMIN" Then
                IsAdmin.Visible = True
            Else
                IsAdmin.Visible = False
            End If

            If Not IsPostBack Then
                GetBCaseDetails()
                GetPCaseDetails()
                GetUserDetails()
                GetTotalCaseCount()
            End If
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetBCaseDetails()
        Dim objGetData As New Echem1GetData.Selectdata() 'change made E1GetData by Echem1GetData
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetBCaseDetails()
            With ddlBCases
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
        Dim objGetData As New Echem1GetData.Selectdata() 'change made E1GetData by Echem1GetData
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetPCaseDetails(Session("USERID").ToString()) 'Replace E1UserName
            With ddlPCases
                .DataSource = ds
                .DataTextField = "CASEDES"
                .DataValueField = "CASEID"
                .DataBind()
                .Font.Size = 8
            End With
            With ddlTarget
                .DataSource = ds
                .DataTextField = "CASEDES"
                .DataValueField = "CASEID"
                .DataBind()
                .Font.Size = 8
            End With

            With ddlTarget1
                .DataSource = ds
                .DataTextField = "CASEDES"
                .DataValueField = "CASEID"
                .DataBind()
                .Font.Size = 8
            End With
            If ds.Tables(0).Rows.Count <= 0 Then
                btnCopyPcase.Visible = False
                btnPTransfer.Visible = False
                btnBTransfer.Visible = False
                btnPShareCase.Visible = False
                btnPShareCopy.Visible = False
                btnSynchronize.Visible = False
                btnPRename.Visible = False
                btnDelete.Visible = False
                ddlPCases.Visible = False
                lblPCase.Visible = True

            Else
                btnCopyPcase.Visible = True
                btnPTransfer.Visible = True
                btnBTransfer.Visible = True
                btnPShareCase.Visible = True
                btnPShareCopy.Visible = True
                btnSynchronize.Visible = True
                btnPRename.Visible = True
                btnDelete.Visible = True
                ddlPCases.Visible = True
                lblPCase.Visible = False

            End If
        Catch ex As Exception
            lblError.Text = "Error:GetPCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetUserDetails()
        Dim objGetData As New Echem1GetData.Selectdata() 'change made E1GetData by Echem1GetData
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetUserCompanyUsers(Session("Echem1UserName").ToString()) 'Replace E1UserName
            With ddlUsers
                .DataSource = ds
                .DataTextField = "USERNAME"
                .DataValueField = "USERID"
                .DataBind()
                .Font.Size = 8
            End With
            With ddlUser2
                .DataSource = ds
                .DataTextField = "USERNAME"
                .DataValueField = "USERID"
                .DataBind()
                .Font.Size = 8
            End With

        Catch ex As Exception
            lblError.Text = "Error:GetBCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetCaseDetails()
        Dim objGetData As New Echem1GetData.Selectdata() 'change made E1GetData by Echem1GetData
        Dim ds As New DataSet
        Dim CaseId As New Integer
        Try
            CaseId = CInt(ddlPCases.SelectedValue)
            ds = objGetData.GetCaseDetails(CaseId.ToString())
            txtCaseDe1.Text = ds.Tables(0).Rows(0).Item("CASEDE1").ToString()
            txtCaseDe2.Text = ds.Tables(0).Rows(0).Item("CASEDE2").ToString()
            txtCaseDe3.Text = ds.Tables(0).Rows(0).Item("CASEDE3").ToString()


        Catch ex As Exception
            lblError.Text = "Error:GetCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetTotalCaseCount()
        Dim objGetData As New Echem1GetData.Selectdata()
        Dim ds As New DataSet()
        Dim dsSUserCount As New DataSet()
        Dim dsShareUserCount As New DataSet()
        Dim dsSUserMaxCountEchem1 As New DataSet()
        Dim dsSUserMaxCountSchem1 As New DataSet()
        Dim dsShareUserMaxCount As New DataSet()
        Try


            'Getting Totalcase for current user
            ds = objGetData.GetTotalCaseCount(Session("USERID").ToString())
            If ds.Tables(0).Rows.Count > 0 Then
                hidTotalCaseCount.Value = ds.Tables(0).Rows(0).Item("TOTALCOUNT").ToString()
            End If


            'Getting Total Case created for selected User for share copy
            dsSUserCount = objGetData.GetTotalCaseCount(ddlUser2.SelectedItem.Value)
            'Getting details for selected user for share Copy
            dsSUserMaxCountEchem1 = objGetData.GetSelectedUserDetails(ddlUser2.SelectedItem.Text, "Echem1")
            If dsSUserMaxCountEchem1.Tables(0).Rows.Count > 0 Then
                hidUserMaxCaseCount.Value = CInt(dsSUserMaxCountEchem1.Tables(0).Rows(0).Item("MAXCASECOUNT")).ToString()
                hidTotalSCount.Value = CInt(dsSUserCount.Tables(0).Rows(0).Item("TOTALCOUNT")).ToString()
            Else
                dsSUserMaxCountSchem1 = objGetData.GetSelectedUserDetails(ddlUser2.SelectedItem.Text, "Schem1")
                If dsSUserMaxCountSchem1.Tables(0).Rows.Count > 0 Then
                    hidUserMaxCaseCount.Value = dsSUserMaxCountSchem1.Tables(0).Rows(0).Item("MAXCASECOUNT").ToString().Trim()
                    hidTotalSCount.Value = dsSUserCount.Tables(0).Rows(0).Item("TOTALCOUNT").ToString().Trim()
                Else
                    hidUserMaxCaseCount.Value = ""
                    hidTotalSCount.Value = ""
                End If

            End If

            'Getting details for selected user for share Access
            dsShareUserCount = objGetData.GetTotalCaseCount(ddlUsers.SelectedItem.Value)
            dsShareUserMaxCount = objGetData.GetSelectedUserDetails(ddlUsers.SelectedItem.Text, "Echem1")
            If dsShareUserMaxCount.Tables(0).Rows.Count > 0 Then
                hidShareUserMaxCaseCount.Value = CInt(dsShareUserMaxCount.Tables(0).Rows(0).Item("MAXCASECOUNT")).ToString()
                hidTotalShareCount.Value = CInt(dsShareUserCount.Tables(0).Rows(0).Item("TOTALCOUNT")).ToString()
            Else
                hidShareUserMaxCaseCount.Value = ""
                hidTotalShareCount.Value = ""

            End If

        Catch ex As Exception
            lblError.Text = "Error:GetBCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

#Region "Copy A Case"
    Protected Sub btnCopyBcase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopyBcase.Click
        Dim SCaseId As String = String.Empty
        Dim TCaseId As String = String.Empty
        Dim Flag As Boolean
        Try
            If Not objRefresh.IsRefresh Then
                SCaseId = ddlBCases.SelectedValue.ToString()
                TCaseId = CreateCase(Session("USERID")) 'Replace E1ToolUserName
                Flag = CopyCase(SCaseId, TCaseId, "Echem1ConnectionString") 'Replace EconConnectionString
                Flag = CopyCase(SCaseId, TCaseId, "Schem1ConnectionString") 'Replace Sustain1ConnectionString
                If Flag Then
                    GetPCaseDetails()
                    GetTotalCaseCount()
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + TCaseId.ToString() + " created successfully.\nCase #" + SCaseId.ToString() + " variables transferred to Case #" + TCaseId.ToString() + " successfully.');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnCopyBcase_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnCopyPcase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopyPcase.Click
        Dim SCaseId As String = String.Empty
        Dim TCaseId As String = String.Empty
        Dim Flag As Boolean
        Try
            If Not objRefresh.IsRefresh Then
                SCaseId = ddlPCases.SelectedValue.ToString()
                TCaseId = CreateCase(Session("USERID")) 'Replace E1ToolUserName
                Flag = CopyCase(SCaseId, TCaseId, "Echem1ConnectionString") 'Replace EconConnectionString
                Flag = CopyCase(SCaseId, TCaseId, "Schem1ConnectionString") 'Replace Sustain1ConnectionString
                If Flag Then
                    GetPCaseDetails()
                    GetTotalCaseCount()
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + TCaseId.ToString() + " created successfully.\nCase #" + SCaseId.ToString() + " variables transferred to Case #" + TCaseId.ToString() + " successfully.');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnCopyPcase_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
#End Region

#Region "Transfer A Case"
    Protected Sub btnBS2T_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBS2T.Click
        Dim SCaseId As String = String.Empty
        Dim TCaseId As String = String.Empty
        Dim Flag As Boolean
        Try
            If Not objRefresh.IsRefresh Then
                SCaseId = ddlBCases.SelectedValue.ToString()
                TCaseId = ddlTarget.SelectedValue.ToString()
                Flag = CopyCase(SCaseId, TCaseId, "Echem1ConnectionString") 'Replace EconConnectionString
                If Flag Then
                    GetPCaseDetails()
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + SCaseId.ToString() + " variables transferred to Case #" + TCaseId.ToString() + " successfully.');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnBS2T_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnPS2T_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPS2T.Click
        Dim SCaseId As String = String.Empty
        Dim TCaseId As String = String.Empty
        Dim Flag As Boolean

        Try
            If Not objRefresh.IsRefresh Then
                SCaseId = ddlPCases.SelectedValue.ToString()
                TCaseId = ddlTarget1.SelectedValue.ToString()
                Flag = CopyCase(SCaseId, TCaseId, "Echem1ConnectionString") 'Replace EconConnectionString
                If Flag Then
                    GetPCaseDetails()
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + SCaseId.ToString() + " variables transferred to Case #" + TCaseId.ToString() + " successfully.');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnPS2T_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
#End Region

#Region "Share Access"
    Protected Sub btnShareAccess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShareAccess.Click
        Dim CaseId As String = String.Empty
        Dim UserName As String = String.Empty
        Dim obj As New ToolCCS
        Dim Flag As Boolean
        Try
            If Not objRefresh.IsRefresh Then
                CaseId = ddlPCases.SelectedValue.ToString()
                UserName = ddlUsers.SelectedValue.ToString()
                obj.CaseAccess(UserName, CaseId, "Echem1ConnectionString")
                obj.CaseAccess(UserName, CaseId, "Schem1ConnectionString")
                GetTotalCaseCount()
                Flag = True
            End If
            divshareAccess.Style.Add("Display", "none")
        Catch ex As Exception
            Flag = False
            lblError.Text = "Error:btnShareAccess_Click:" + ex.Message.ToString() + ""
        End Try
        If Flag Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + CaseId + " has been successfully shared with User " + ddlUsers.SelectedItem.Text.ToString() + "');", True)
        End If
    End Sub
    Protected Sub btnShareAccessC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShareAccessC.Click
        Try
            divshareAccess.Style.Add("Display", "none")
        Catch ex As Exception
            lblError.Text = "Error:btnShareAccessC_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
#End Region

#Region "Share A Copy"
    Protected Sub btnShareCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShareCopy.Click
        Dim SCaseId As String = String.Empty
        Dim TCaseId As String = String.Empty
        Dim UserName As String = ddlUser2.SelectedValue.ToString()
        Dim Flag As Boolean
        Try
            If Not objRefresh.IsRefresh Then
                SCaseId = ddlPCases.SelectedValue.ToString()
                TCaseId = CreateCase(UserName.ToString())
                Flag = CopyCase(SCaseId, TCaseId, "Echem1ConnectionString")
                Flag = CopyCase(SCaseId, TCaseId, "Schem1ConnectionString")
                If Flag Then
                    GetPCaseDetails()
                    GetTotalCaseCount()
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + SCaseId + " has been successfully copied to User - " + UserName.ToString() + " Proprietary case #" + TCaseId + "');", True)
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + TCaseId.ToString() + " created successfully.\nCase #" + SCaseId.ToString() + " variables transferred to Case #" + TCaseId.ToString() + " successfully.');", True)
                End If
            End If
            divsharecopy.Style.Add("Display", "none")
        Catch ex As Exception
            lblError.Text = "Error:btnShareCopy_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub btnShareCopyC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShareCopyC.Click
        Try
            divsharecopy.Style.Add("Display", "none")
        Catch ex As Exception
            lblError.Text = "Error:btnShareCopyC_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
#End Region

#Region "Synchronize"
    Protected Sub btnSynchronize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSynchronize.Click
        Dim obj As New ToolCCS
        Dim Flag As Boolean
        Dim SCaseId As String = String.Empty
        Dim TCaseId As String = String.Empty
        Try
            If Not objRefresh.IsRefresh Then
                SCaseId = ddlPCases.SelectedValue.ToString()
                TCaseId = ddlPCases.SelectedValue.ToString()
                obj.CaseSynch(SCaseId, TCaseId, "SCHEM1", "ECHEM1", "Echem1ConnectionString", "SystemConnectionString")
                Flag = True
            End If
        Catch ex As Exception
            Flag = False
            lblError.Text = "Error:btnSynchronize_Click:" + ex.Message.ToString() + ""
        End Try
        If Flag Then
            GetPCaseDetails()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('You have suceessfully synchronized Case #" + SCaseId.ToString() + " from Schem1 to Echem1');", True)
        End If
    End Sub
#End Region

#Region "Rename Case"
    Protected Sub btnPRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPRename.Click
        Try
            If Not objRefresh.IsRefresh Then
                divModify.Visible = True
                GetCaseDetails()
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnPRename_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnRenameC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRenameC.Click
        Try
            divModify.Visible = False
        Catch ex As Exception
            lblError.Text = "Error:btnRenameC_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRename.Click
        Dim obj As New ToolCCS
        Dim CaseId As String = String.Empty
        Try
            If Not objRefresh.IsRefresh Then
                CaseId = ddlPCases.SelectedValue.ToString()
                obj.CaseRename(ddlPCases.SelectedValue.ToString(), txtCaseDe1.Text.Trim().Replace("'", "''").ToString(), txtCaseDe2.Text.Trim().Replace("'", "''").ToString(), txtCaseDe3.Text.Trim().Replace("'", "''").ToString(), "Echem1ConnectionString")
                GetPCaseDetails()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Case#" + CaseId.ToString() + " updated successfully');", True)
            End If
            divModify.Visible = False
        Catch ex As Exception
            lblError.Text = "Error:btnRename_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
#End Region

#Region "Delete Case"
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim obj As New ToolCCS
        Dim CaseId As New Integer
        Try
            If Not objRefresh.IsRefresh Then
                CaseId = CInt(ddlPCases.SelectedValue.ToString())
                obj.CaseDelete(Session("USERID").ToString(), CaseId.ToString(), "Echem1ConnectionString")
                obj.CaseDelete(Session("USERID").ToString(), CaseId.ToString(), "Schem1ConnectionString")
                GetPCaseDetails()
                GetTotalCaseCount()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Case#" + CaseId.ToString() + " deleted successfully');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnDelete_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
#End Region

#Region "Create Case"
    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Try
            If Not objRefresh.IsRefresh Then
                Dim CaseId As New Integer
                CaseId = CreateCase(Session("USERID"))
                GetPCaseDetails()
                GetTotalCaseCount()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + CaseId.ToString() + " created successfully');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnCreate_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub


#End Region

#Region "Tools Function"
    Protected Function CreateCase(ByVal USERID As String) As Integer
        Dim CaseId As Integer
        Dim obj As New ToolCCS
        Try
            CaseId = obj.CreateCase(USERID, "Echem1ConnectionString", "Echem1", 0)
            obj.CreateCase(USERID, "Schem1ConnectionString", "Echem1", CaseId)
            Return CaseId

        Catch ex As Exception
            lblError.Text = "Error:CreatedCase:" + ex.Message.ToString() + ""
        End Try
    End Function

    Protected Function CopyCase(ByVal SCaseID As String, ByVal TCaseId As String, ByVal Schema As String) As Boolean
        Dim obj As New ToolCCS
        Try
            obj.CopyCase(SCaseID, TCaseId, Schema)
            Return True
        Catch ex As Exception
            lblError.Text = "Error:CopyCase:" + ex.Message.ToString() + ""
            Return False
        End Try
    End Function
#End Region

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Try
            hidBackcheck.Value = "0"
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlUser2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUser2.SelectedIndexChanged
        GetTotalCaseCount()
        divsharecopy.Style.Add("Display", "inline")
    End Sub
    Protected Sub ddlUsers_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUsers.SelectedIndexChanged
        GetTotalCaseCount()
        divshareAccess.Style.Add("Display", "inline")
    End Sub


End Class
