Imports System.Data
Imports System.Data.OleDb
Imports System
Imports MoldE1GetData
Imports MoldE1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports ToolCCS
Imports System.Net.Mail
Imports System.Net
Partial Class Pages_MoldEcon1_Tools_Tools2
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            If Session("MoldE1UserRole") = "AADMIN" Then
                IsAdmin.Visible = True
            Else
                IsAdmin.Visible = False
            End If

            If Not IsPostBack Then
                GetBCaseDetails()
                GetPCaseDetails()
                GetUserDetails()
                GetTotalCaseCount()
                hidGroupId.Value = "0"
                hidApprovedCase.Value = "0"
                hidPropCase.Value = "0"
                hidTargetApp.Value = "0"
                hidTargetProp.Value = "0"
                'divsharecopy.Style.Add("Display", "none")
            End If
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetBCaseDetails()
        Dim objGetData As New MoldE1GetData.Selectdata()
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
        Dim objGetData As New MoldE1GetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetPCaseDetails(Session("UserId").ToString())
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
            'If ds.Tables(0).Rows.Count <= 0 Then
            '    btnCopyPcase.Visible = False
            '    btnPTransfer.Visible = False
            '    btnBTransfer.Visible = False
            '    btnPShareCase.Visible = False
            '    btnPShareCopy.Visible = False
            '    btnSynchronize.Visible = False
            '    btnPRename.Visible = False
            '    btnDelete.Visible = False
            '    ' ddlPCases.Visible = False
            '    lblPCase.Visible = True

            'Else
            '    btnCopyPcase.Visible = True
            '    btnPTransfer.Visible = True
            '    btnBTransfer.Visible = True
            '    btnPShareCase.Visible = True
            '    btnPShareCopy.Visible = True
            '    btnSynchronize.Visible = True
            '    btnPRename.Visible = True
            '    btnDelete.Visible = True
            '    'ddlPCases.Visible = True
            '    lblPCase.Visible = False

            'End If
        Catch ex As Exception
            lblError.Text = "Error:GetPCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetUserDetails()
        Dim objGetData As New MoldE1GetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetUserCompanyUsers(Session("MoldE1UserName").ToString())
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
        Dim objGetData As New MoldE1GetData.Selectdata()
        Dim ds As New DataSet
        Dim CaseId As New Integer
        Try
            CaseId = CInt(hidPropCase.Value)
            ds = objGetData.GetCaseDetails(CaseId.ToString())
            txtCaseDe1.Text = ds.Tables(0).Rows(0).Item("CASEDE1").ToString()
            txtCaseDe2.Text = ds.Tables(0).Rows(0).Item("CASEDE2").ToString()
            txtCaseDe3.Text = ds.Tables(0).Rows(0).Item("CASEDE3").ToString()


        Catch ex As Exception
            lblError.Text = "Error:GetCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetTotalCaseCount()
        Dim objGetData As New MoldE1GetData.Selectdata()
        Dim ds As New DataSet()
        Dim dsSUserCount As New DataSet()
        Dim dsShareUserCount As New DataSet()
        Dim dsSUserMaxCountE1 As New DataSet()
        Dim dsSUserMaxCountS1 As New DataSet()
        Dim dsShareUserMaxCount As New DataSet()
        Try


            'Getting Totalcase for current user
            ds = objGetData.GetTotalCaseCount(Session("UserId").ToString())
            If ds.Tables(0).Rows.Count > 0 Then
                hidTotalCaseCount.Value = ds.Tables(0).Rows(0).Item("TOTALCOUNT").ToString()
            End If


            'Getting Total Case created for selected User for share copy
            dsSUserCount = objGetData.GetTotalCaseCount(ddlUser2.SelectedValue)
            'Getting details for selected user for share Copy
            dsSUserMaxCountE1 = objGetData.GetSelectedUserDetails(ddlUser2.SelectedItem.Text, "MOLDE1")
            If dsSUserMaxCountE1.Tables(0).Rows.Count > 0 Then
                hidUserMaxCaseCount.Value = CInt(dsSUserMaxCountE1.Tables(0).Rows(0).Item("MAXCASECOUNT")).ToString()
                hidTotalSCount.Value = CInt(dsSUserCount.Tables(0).Rows(0).Item("TOTALCOUNT")).ToString()
            Else
                dsSUserMaxCountS1 = objGetData.GetSelectedUserDetails(ddlUser2.SelectedItem.Text, "MOLDS1")
                If dsSUserMaxCountS1.Tables(0).Rows.Count > 0 Then
                    hidUserMaxCaseCount.Value = dsSUserMaxCountS1.Tables(0).Rows(0).Item("MAXCASECOUNT").ToString().Trim()
                    hidTotalSCount.Value = dsSUserCount.Tables(0).Rows(0).Item("TOTALCOUNT").ToString().Trim()
                Else
                    hidUserMaxCaseCount.Value = ""
                    hidTotalSCount.Value = ""
                End If

            End If

            'Getting details for selected user for share Access
            dsShareUserCount = objGetData.GetTotalCaseCount(ddlUsers.SelectedValue)
            dsShareUserMaxCount = objGetData.GetSelectedUserDetails(ddlUsers.SelectedItem.Text, "MOLDE1")
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
            SCaseId = hidApprovedCase.Value.ToString()
            TCaseId = CreateCase(Session("UserId"))
            Flag = CopyCase(SCaseId, TCaseId, "MoldE1ConnectionString")
            Flag = CopyCase(SCaseId, TCaseId, "MoldS1ConnectionString")
            If Flag Then
                GetPCaseDetails()
                GetTotalCaseCount()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + TCaseId.ToString() + " created successfully.\nCase #" + SCaseId.ToString() + " variables transferred to Case #" + TCaseId.ToString() + " successfully.');", True)
                hidApprovedCase.Value = "0"

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
            SCaseId = hidPropCase.Value
            TCaseId = CreateCase(Session("UserId"))
            Flag = CopyCase(SCaseId, TCaseId, "MoldE1ConnectionString")
            Flag = CopyCase(SCaseId, TCaseId, "MoldS1ConnectionString")
            If Flag Then
                GetPCaseDetails()
                GetTotalCaseCount()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + TCaseId.ToString() + " created successfully.\nCase #" + SCaseId.ToString() + " variables transferred to Case #" + TCaseId.ToString() + " successfully.');", True)
                hidPropCase.Value = "0"
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
            SCaseId = hidApprovedCase.Value.ToString()
            TCaseId = hidTargetApp.Value.ToString()
            Flag = CopyCase(SCaseId, TCaseId, "MoldE1ConnectionString")
            If Flag Then
                GetPCaseDetails()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + SCaseId.ToString() + " variables transferred to Case #" + TCaseId.ToString() + " successfully.');", True)
                hidApprovedCase.Value = "0"
                hidTargetApp.Value = "0"
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
            SCaseId = hidPropCase.Value
            TCaseId = hidTargetProp.Value.ToString()
            Flag = CopyCase(SCaseId, TCaseId, "MoldE1ConnectionString")
            If Flag Then
                GetPCaseDetails()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + SCaseId.ToString() + " variables transferred to Case #" + TCaseId.ToString() + " successfully.');", True)
                hidPropCase.Value = "0"
                hidTargetProp.Value = "0"
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

        Dim objGetData As New MoldE1GetData.Selectdata()
        Dim ds As New DataSet()
        Dim dsSUserCount As New DataSet()
        Dim dsShareUserCount As New DataSet()
        Dim dsSUserMaxCountE1 As New DataSet()
        Dim dsSUserMaxCountS1 As New DataSet()
        Dim dsShareUserMaxCount As New DataSet()
        Try

            'Getting details for selected user for share Access
            dsShareUserCount = objGetData.GetTotalCaseCount(hidUserSA.Value)
            dsShareUserMaxCount = objGetData.GetSelectedUserDetails(hidUserSA.Value, "MOLDE1")
            If dsShareUserMaxCount.Tables(0).Rows.Count > 0 Then
                hidShareUserMaxCaseCount.Value = CInt(dsShareUserMaxCount.Tables(0).Rows(0).Item("MAXCASECOUNT")).ToString()
                hidTotalShareCount.Value = CInt(dsShareUserCount.Tables(0).Rows(0).Item("TOTALCOUNT")).ToString()
            Else
                hidShareUserMaxCaseCount.Value = ""
                hidTotalShareCount.Value = ""

            End If
            If hidTotalShareCount.Value <> "" Or hidShareUserMaxCaseCount.Value <> "" Then
                If CInt(hidTotalShareCount.Value) < CInt(hidShareUserMaxCaseCount.Value) Then
                    CaseId = hidPropCase.Value
                    UserName = hidUserSA.Value ' ddlUsers.SelectedValue.ToString()
                    obj.CaseAccess(UserName, CaseId, "MoldE1ConnectionString")
                    obj.CaseAccess(UserName, CaseId, "MoldS1ConnectionString")
                    divshareAccess.Style.Add("Display", "none")
                    GetTotalCaseCount()
                    Flag = True
                Else
                    Dim msg As String = ""
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('You are trying to share a access with user " + hidUserSA.Value + ".  " + hidUserSA.Value + " has reached their limit of " + hidShareUserMaxCaseCount.Value + " cases.  Please contact SavvyPack Corporation (952-405-7500) to purchase more cases.');", True)
                    Flag = False

                    lnkUsers1.Text = "Select User"
                    hidUserSA.Value = ""
                    lnkPropCase.Text = hidPropCaseD.Value
                    divshareAccess.Style.Add("display", "block")
                End If
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('You are trying to share a copy with user " + hidUserSA.Value + ".  " + hidUserSA.Value + " does not have permissions to the appropriate module.  Please contact SavvyPack Corporation (952-405-7500) to purchase these permissions.');", True)
                lnkUsers1.Text = "Select User"
                hidUserSA.Value = ""
                lnkPropCase.Text = hidPropCaseD.Value
                divshareAccess.Style.Add("display", "block")
            End If


        Catch ex As Exception
            Flag = False
            lblError.Text = "Error:btnShareAccess_Click:" + ex.Message.ToString() + ""
        End Try
        If Flag Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + CaseId + " has been successfully shared with User " + UserName.ToString() + "');", True)
            hidPropCase.Value = "0"
            lnkUsers1.Text = "Select User"
            hidUserSA.Value = ""
            lnkPropCase.Text = "Select Case"
        End If
    End Sub
    Protected Sub btnShareAccessC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShareAccessC.Click
        Try
            hidPropCase.Value = "0"
            lnkUsers1.Text = "Select User"
            hidUserSA.Value = ""
            lnkPropCase.Text = "Select Case"
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
        Dim UserName As String = hidUserSC.Value
        Dim Flag As Boolean

        Dim objGetData As New MoldE1GetData.Selectdata()
        Dim ds As New DataSet()
        Dim dsSUserCount As New DataSet()
        Dim dsShareUserCount As New DataSet()
        Dim dsSUserMaxCountE1 As New DataSet()
        Dim dsSUserMaxCountS1 As New DataSet()
        Dim dsShareUserMaxCount As New DataSet()
        Try

            'Getting Total Case created for selected User for share copy
            dsSUserCount = objGetData.GetTotalCaseCount(hidUserSC.Value)
            'Getting details for selected user for share Copy
            dsSUserMaxCountE1 = objGetData.GetSelectedUserDetails(hidUserSC.Value, "MOLDE1")
            If dsSUserMaxCountE1.Tables(0).Rows.Count > 0 Then
                hidUserMaxCaseCount.Value = CInt(dsSUserMaxCountE1.Tables(0).Rows(0).Item("MAXCASECOUNT")).ToString()
                hidTotalSCount.Value = CInt(dsSUserCount.Tables(0).Rows(0).Item("TOTALCOUNT")).ToString()
            Else
                dsSUserMaxCountS1 = objGetData.GetSelectedUserDetails(hidUserSC.Value, "SUSTAIN1")
                If dsSUserMaxCountS1.Tables(0).Rows.Count > 0 Then
                    hidUserMaxCaseCount.Value = dsSUserMaxCountS1.Tables(0).Rows(0).Item("MAXCASECOUNT").ToString().Trim()
                    hidTotalSCount.Value = dsSUserCount.Tables(0).Rows(0).Item("TOTALCOUNT").ToString().Trim()
                Else
                    hidUserMaxCaseCount.Value = ""
                    hidTotalSCount.Value = ""
                End If

            End If

            If hidTotalSCount.Value <> "" Or hidUserMaxCaseCount.Value <> "" Then
                If CInt(hidTotalSCount.Value) < CInt(hidUserMaxCaseCount.Value) Then
                    SCaseId = hidPropCase.Value
                    TCaseId = CreateCase(UserName.ToString())
                    Flag = CopyCase(SCaseId, TCaseId, "MoldE1ConnectionString")
                    Flag = CopyCase(SCaseId, TCaseId, "MoldS1ConnectionString")
                    If Flag Then
                        GetPCaseDetails()
                        GetTotalCaseCount()
                        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + SCaseId + " has been successfully copied to User - " + UserName.ToString() + " Proprietary case #" + TCaseId + "');", True)
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + TCaseId.ToString() + " created successfully.\nCase #" + SCaseId.ToString() + " variables transferred to Case #" + TCaseId.ToString() + " successfully.');", True)
                        hidPropCase.Value = "0"
                        divsharecopy.Style.Add("Display", "none")
                        lnkUser2.Text = "Select User"
                        hidUserSC.Value = ""
                        lnkPropCase.Text = "Select Case"
                    End If
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('You are trying to share a copy with user " + hidUserSC.Value + ".  " + hidUserSC.Value + " has reached their limit of " + hidUserMaxCaseCount.Value + " cases.  Please contact SavvyPack Corporation (952-405-7500) to purchase more cases.');", True)
                    Flag = False

                    lnkUser2.Text = "Select User"
                    hidUserSC.Value = ""
                    lnkPropCase.Text = hidPropCaseD.Value
                    divsharecopy.Style.Add("display", "block")
                End If
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('You are trying to share a copy with user " + hidUserSC.Value + ".  " + hidUserSC.Value + " does not have permissions to the appropriate module.  Please contact SavvyPack Corporation (952-405-7500) to purchase these permissions.');", True)
                Flag = False

                lnkUser2.Text = "Select User"
                hidUserSC.Value = ""
                lnkPropCase.Text = hidPropCaseD.Value
                divsharecopy.Style.Add("display", "block")
            End If


        Catch ex As Exception
            lblError.Text = "Error:btnShareCopy_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnShareCopyC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShareCopyC.Click
        Try
            divsharecopy.Style.Add("Display", "none")
            hidPropCase.Value = "0"
            lnkUser2.Text = "Select User"
            hidUserSC.Value = ""
            lnkPropCase.Text = "Select Case"
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

            SCaseId = hidPropCase.Value ' ddlPCases.SelectedValue.ToString()
            TCaseId = hidPropCase.Value ' ddlPCases.SelectedValue.ToString()
            obj.CaseSynch(SCaseId, TCaseId, "SUSTAIN", "ECON", "MoldE1ConnectionString", "SystemConnectionString")
            Flag = True
        Catch ex As Exception
            Flag = False
            lblError.Text = "Error:btnSynchronize_Click:" + ex.Message.ToString() + ""
        End Try
        If Flag Then
            hidPropCase.Value = "0"
            hidPropCaseD.Value = "Select case"
            lnkPropCase.Text = hidPropCaseD.Value
            GetPCaseDetails()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('You have suceessfully synchronized Case #" + SCaseId.ToString() + " from Sustain1 Mold to Econ1 Mold');", True)
        End If
    End Sub
#End Region

#Region "Rename Case"
    Protected Sub btnPRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPRename.Click
        Try
            divModify.Visible = True
            lnkPropCase.Text = hidPropCaseD.Value
            GetCaseDetails()

        Catch ex As Exception
            lblError.Text = "Error:btnPRename_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnRenameC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRenameC.Click
        Try
            divModify.Visible = False
            hidPropCase.Value = "0"
            hidPropCaseD.Value = "Select case"
            lnkPropCase.Text = "Select case"

        Catch ex As Exception
            lblError.Text = "Error:btnRenameC_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRename.Click
        Dim obj As New ToolCCS
        Dim CaseId As String = String.Empty
        Try
            CaseId = hidPropCase.Value
            obj.CaseRename(CaseId, txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), txtCaseDe2.Text.Trim.ToString().Replace("'", "''"), txtCaseDe3.Text.Trim.ToString().Replace("'", "''"), "MoldE1ConnectionString")
            GetPCaseDetails()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Case#" + CaseId.ToString() + " updated successfully');", True)
            hidPropCase.Value = "0"
            hidPropCaseD.Value = "Select case"
            lnkPropCase.Text = "Select case"
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
            CaseId = hidPropCase.Value
            obj.CaseDelete(Session("UserId").ToString(), CaseId.ToString(), "MoldE1ConnectionString")
            obj.CaseDelete(Session("UserId").ToString(), CaseId.ToString(), "MoldS1ConnectionString")

            'Delete Case from Group
            obj.GroupCaseDelete(Session("UserId").ToString(), CaseId.ToString(), "MoldE1ConnectionString")

            GetPCaseDetails()
            GetTotalCaseCount()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Case#" + CaseId.ToString() + " deleted successfully');", True)
            hidPropCase.Value = "0"
        Catch ex As Exception
            lblError.Text = "Error:btnDelete_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
#End Region

#Region "Create Case"
    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Try
            Dim CaseId As New Integer
            CaseId = CreateCase(Session("UserId"))
            GetPCaseDetails()
            GetTotalCaseCount()
            hidPropCase.Value = "0"
            hidPropCaseD.Value = "Selec case"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + CaseId.ToString() + " created successfully');", True)

        Catch ex As Exception
            lblError.Text = "Error:btnCreate_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub


#End Region

#Region "Tools Function"
    Protected Function CreateCase(ByVal UserId As String) As Integer
        Dim CaseId As Integer
        Dim obj As New ToolCCS
        Try
            CaseId = obj.CreateCase(UserId, "MoldE1ConnectionString", "MoldE1", 0)
            obj.CreateCase(UserId, "MoldS1ConnectionString", "MoldE1", CaseId)
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

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim objUpdateData As New MoldE1UpInsData.UpdateInsert()
            Dim objGetData As New MoldE1GetData.Selectdata()
            Dim ds As New DataSet()
            'Update Permissionscases Status and Log
            If hidPropCase.Value = "0" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinB", "alert('Please select Proprietary Case');", True)
                lnkPropCase.Text = "Select case"
            Else
                objUpdateData.PermissionStatusUpdate(hidPropCase.Value, Session("MoldE1UserName").ToString())
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinB", "alert('Case " + hidPropCase.Value + " submitted successfully.');", True)
                'Sending Email
                SendEmailStatus(hidPropCase.Value)

                'Getting Updated Case Status
                ds = objGetData.GetPropCasesById(Session("UserId"), hidPropCase.Value)
                If ds.Tables(0).Rows.Count > 0 Then
                    hidPropCaseD.Value = ds.Tables(0).Rows(0).Item("CaseId").ToString() + ":" + ds.Tables(0).Rows(0).Item("CaseDe1").ToString() + "&nbsp;&nbsp;" + ds.Tables(0).Rows(0).Item("CaseDe2").ToString() + "&nbsp;&nbsp;&nbsp;" + ds.Tables(0).Rows(0).Item("STATUS").ToString()
                End If

                lnkPropCase.Text = hidPropCaseD.Value
                hidPropCaseSt.Value = "1"
            End If


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub SendEmailStatus(ByVal CaseId As String)
        Try
            'Get Email Config Details
            Dim objE1GetData As New MoldE1GetData.Selectdata()
            Dim ds As New DataSet()
            Dim dsUserData As New DataSet
            Dim objGetData As New UsersGetData.Selectdata
            Dim link, EmailLink As String
            Dim strBodyData As String

            ds = objGetData.GetEmailConfigDetails("Y")
            dsUserData = objE1GetData.GetPermissionStatus(CaseId, Session("MoldE1UserName").ToString())
            If ds.Tables(0).Rows.Count > 0 Then
                If dsUserData.Tables(0).Rows.Count > 0 Then
                    'mail for verification
                    EmailLink = ds.Tables(0).Rows(0).Item("URL").ToString()
                    'Sending mail
                    strBodyData = GetEmailBodyData(EmailLink, dsUserData)
                    SendEmail(strBodyData)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub SendEmail(ByVal strBody As String)
        Try
            Dim objGetData As New UsersGetData.Selectdata
            Dim ds As New DataSet
            ds = objGetData.GetAlliedMemberMail("SUBFORAPP")
            If ds.Tables(0).Rows.Count > 0 Then
                Dim i As Integer
                Dim _To As New MailAddressCollection()
                Dim _From As New MailAddress(ds.Tables(0).Rows(0).Item("FROMADD").ToString(), ds.Tables(0).Rows(0).Item("FROMNAME").ToString())
                Dim _CC As New MailAddressCollection()
                Dim _BCC As New MailAddressCollection()
                Dim Item As MailAddress
                Dim Email As New EmailConfig()
                Dim _Subject As String = ds.Tables(0).Rows(0).Item("SUBJECT").ToString()

                'To's
                Item = New MailAddress(ds.Tables(0).Rows(0).Item("TOADD").ToString(), ds.Tables(0).Rows(0).Item("TONAME").ToString())
                _To.Add(Item)

                For i = 1 To 10
                    ' BCC() 's
                    If ds.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString() <> Nothing Then
                        Item = New MailAddress(ds.Tables(0).Rows(0).Item("BCC" + i.ToString()).ToString(), ds.Tables(0).Rows(0).Item("BCC" + i.ToString() + "NAME").ToString())
                        _BCC.Add(Item)
                    End If
                    If ds.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString() <> Nothing Then
                        Item = New MailAddress(ds.Tables(0).Rows(0).Item("CC" + i.ToString()).ToString(), ds.Tables(0).Rows(0).Item("CC" + i.ToString() + "NAME").ToString())
                        _CC.Add(Item)
                    End If
                Next



                Email.SendMail(_From, _To, _CC, _BCC, strBody, _Subject)
            End If



        Catch ex As Exception
            lblError.Text = "Error:SendEmail:" + ex.Message.ToString()
        End Try
    End Sub
    Public Function GetEmailBodyData(ByVal link As String, ByVal ds As DataSet) As String
        Dim StrSqlBody As String = ""
        Try
            StrSqlBody = "<div style='font-family:Verdana;'>  "
            StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:15px;margin-top:10px;margin-bottom:10px;font-weight:bold'>Request for Approval</div>"

            StrSqlBody = StrSqlBody + "<div style='font-family:Verdana;font-size:15px;margin-top:10px;margin-bottom:10px;font-weight:bold'></div>"
            StrSqlBody = StrSqlBody + "<table style='font-family:Verdana;width:700px;font-size:12px;border-collapse:collapse' border='1' bordercolor='black' cellpadding='0' cellspacing='0'>  "
            StrSqlBody = StrSqlBody + "<tr style='background-color:#336699;height:20px;text-align:center;color:white'> "
            StrSqlBody = StrSqlBody + "<td><b>Module</b></td> "
            StrSqlBody = StrSqlBody + "<td><b>Case Id</b></td> "
            StrSqlBody = StrSqlBody + "<td><b>Status</b></td> "
            StrSqlBody = StrSqlBody + "<td><b>Date</b></td> "
            StrSqlBody = StrSqlBody + "<td><b>Action By</b></td> "
            StrSqlBody = StrSqlBody + "<td><b>Comments</b></td> "
            StrSqlBody = StrSqlBody + "</tr> "
            StrSqlBody = StrSqlBody + "<tr style='height:20px;text-align:center'> "
            StrSqlBody = StrSqlBody + "<td>Econ1 Mold</b></td> "
            StrSqlBody = StrSqlBody + "<td>" + ds.Tables(0).Rows(0).Item("CASEID").ToString() + "</b></td> "
            StrSqlBody = StrSqlBody + "<td>" + ds.Tables(0).Rows(0).Item("STATUS").ToString() + "</b></td> "
            StrSqlBody = StrSqlBody + "<td>" + ds.Tables(0).Rows(0).Item("DATED").ToString() + "</b></td> "
            StrSqlBody = StrSqlBody + "<td>" + ds.Tables(0).Rows(0).Item("ACTIONBY").ToString() + "</b></td> "
            StrSqlBody = StrSqlBody + "<td>" + ds.Tables(0).Rows(0).Item("COMMENTS").ToString() + "</b></td> "
            StrSqlBody = StrSqlBody + "</tr> "
            StrSqlBody = StrSqlBody + "</table> "
            StrSqlBody = StrSqlBody + "</div> "
            Return StrSqlBody
        Catch ex As Exception
            lblError.Text = "Error:GetEmailBodyData:" + ex.Message.ToString()
            Return StrSqlBody
        End Try
    End Function

    Protected Sub btnBS2TCancle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBS2TCancle.Click
        Try
            hidApprovedCase.Value = "0"
            hidTargetApp.Value = "0"
            divBCopy.Style.Add("Display", "None")
        Catch ex As Exception

        End Try
    End Sub


    Protected Sub btnPS2TCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPS2TCancel.Click
        Try
            hidPropCase.Value = "0"
            hidTargetProp.Value = "0"
            divCopy.Style.Add("Display", "None")
        Catch ex As Exception

        End Try
    End Sub
End Class
