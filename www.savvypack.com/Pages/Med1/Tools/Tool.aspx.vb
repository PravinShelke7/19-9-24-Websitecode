Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Med1GetData
Imports Med1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports ToolCCS
Partial Class Pages_MedEcon1_Tools_Tool
    Inherits System.Web.UI.Page
    Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
    Dim MedEcon1Connection As String = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            If Session("Med1UserRole") = "AADMIN" Then
                IsAdmin.Visible = True
            Else
                IsAdmin.Visible = False
            End If

            If Not IsPostBack Then
                GetBCaseDetails()
                GetPCaseDetails()
                GetUserDetails()
                GetTotalCaseCount()
                'divsharecopy.Style.Add("Display", "none")
                If Session("Service") = "COMP" Then
                    Med1table.Attributes.Add("class", "E1CompModule")
                    divMainHeading.InnerHtml = "Med1 - Competitive Module - Tools"
                    divMainHeading.Attributes.Add("onmouseover", "Tip('Competitive Module-Tools')")
                    Page.Title = "Med1-Competitive Module-Tools"
                    IsAdmin.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetBCaseDetails()
        Dim objGetData As New Med1GetData.Selectdata()
        Dim ds As New DataSet()
        Try
            If Session("Service") = "COMP" Then
                ds = objGetData.GetBCaseCompDetails()
            Else
                ds = objGetData.GetBCaseDetails()
            End If

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
        Dim objGetData As New Med1GetData.Selectdata()
        Dim ds As New DataSet()
        Try
            If Session("Service") = "COMP" Then
                ds = objGetData.GetPropCaseDetails(Session("Med1UserName").ToString(), Session("ServiceId"))
            Else
                ds = objGetData.GetPCaseDetails(Session("UserId").ToString())
            End If

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
        Dim objGetData As New Med1GetData.Selectdata()
        Dim ds As New DataSet()
        Try
            If Session("Service") = "COMP" Then
                ds = objGetData.GetCompUserCompanyUsers(Session("Med1UserName").ToString(), Session("ServiceId").ToString())
            Else
                ds = objGetData.GetUserCompanyUsers(Session("Med1UserName").ToString())
            End If

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
        Dim objGetData As New Med1GetData.Selectdata()
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
        Dim objGetData As New Med1GetData.Selectdata()
        Dim ds As New DataSet()
        Dim dsSUserCount As New DataSet()
        Dim dsShareUserCount As New DataSet()
        Dim dsSUserMaxCountMed1 As New DataSet()
        Dim dsSUserMaxCountSMed1 As New DataSet()
        Dim dsShareUserMaxCount As New DataSet()
        Try


            'Getting Totalcase for current user
            If Session("Service") = "COMP" Then
                ds = objGetData.GetCompTotalCaseCount(Session("Med1UserName").ToString(), Session("ServiceId").ToString())
            Else
                ds = objGetData.GetTotalCaseCount(Session("UserId").ToString())
            End If

            If ds.Tables(0).Rows.Count > 0 Then
                hidTotalCaseCount.Value = ds.Tables(0).Rows(0).Item("TOTALCOUNT").ToString()
            End If


            'Getting Total Case created for selected User for share copy
            If Session("Service") = "COMP" Then
                dsSUserCount = objGetData.GetCompTotalCaseCount(ddlUser2.SelectedItem.Text.ToUpper(), Session("ServiceId").ToString())
            Else
                dsSUserCount = objGetData.GetTotalCaseCount(ddlUser2.SelectedValue)
            End If

            'Getting details for selected user for share Copy
            If Session("Service") = "COMP" Then
                dsSUserMaxCountMed1 = objGetData.GetSelectedUserDetails(ddlUser2.SelectedItem.Text, "CompEcon")
            Else
                dsSUserMaxCountMed1 = objGetData.GetSelectedUserDetails(ddlUser2.SelectedItem.Text, "MedE1")
            End If

            If dsSUserMaxCountMed1.Tables(0).Rows.Count > 0 Then
                hidUserMaxCaseCount.Value = CInt(dsSUserMaxCountMed1.Tables(0).Rows(0).Item("MAXCASECOUNT")).ToString()
                hidTotalSCount.Value = CInt(dsSUserCount.Tables(0).Rows(0).Item("TOTALCOUNT")).ToString()
            Else
                If Session("Service") = "COMP" Then
                    dsSUserMaxCountSMed1 = objGetData.GetSelectedUserDetails(ddlUser2.SelectedItem.Text, "CompSustain")
                Else
                    dsSUserMaxCountSMed1 = objGetData.GetSelectedUserDetails(ddlUser2.SelectedItem.Text, "MedS1")
                End If

                If dsSUserMaxCountSMed1.Tables(0).Rows.Count > 0 Then
                    hidUserMaxCaseCount.Value = dsSUserMaxCountSMed1.Tables(0).Rows(0).Item("MAXCASECOUNT").ToString().Trim()
                    hidTotalSCount.Value = dsSUserCount.Tables(0).Rows(0).Item("TOTALCOUNT").ToString().Trim()
                Else
                    hidUserMaxCaseCount.Value = ""
                    hidTotalSCount.Value = ""
                End If

            End If

            'Getting details for selected user for share Access
            If Session("Service") = "COMP" Then
                dsShareUserCount = objGetData.GetCompTotalCaseCount(ddlUsers.SelectedItem.Text.ToUpper(), Session("ServiceId").ToString())
            Else
                dsShareUserCount = objGetData.GetTotalCaseCount(ddlUsers.SelectedValue)
            End If

            If Session("Service") = "COMP" Then
                dsShareUserMaxCount = objGetData.GetSelectedUserDetails(ddlUsers.SelectedItem.Text, "CompEcon")
            Else
                dsShareUserMaxCount = objGetData.GetSelectedUserDetails(ddlUsers.SelectedItem.Text, "MedE1")
            End If

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
            SCaseId = ddlBCases.SelectedValue.ToString()
            TCaseId = CreateCase(Session("UserId"))
            Flag = CopyCase(SCaseId, TCaseId, "MedEcon1ConnectionString")
            Flag = CopyCase(SCaseId, TCaseId, "MedSustain1ConnectionString")
            If Flag Then
                GetPCaseDetails()
                GetTotalCaseCount()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + TCaseId.ToString() + " created successfully.\nCase #" + SCaseId.ToString() + " variables transferred to Case #" + TCaseId.ToString() + " successfully.');", True)
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
            SCaseId = ddlPCases.SelectedValue.ToString()
            TCaseId = CreateCase(Session("UserId"))
            Flag = CopyCase(SCaseId, TCaseId, "MedEcon1ConnectionString")
            Flag = CopyCase(SCaseId, TCaseId, "MedSustain1ConnectionString")
            If Flag Then
                GetPCaseDetails()
                GetTotalCaseCount()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + TCaseId.ToString() + " created successfully.\nCase #" + SCaseId.ToString() + " variables transferred to Case #" + TCaseId.ToString() + " successfully.');", True)
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
            SCaseId = ddlBCases.SelectedValue.ToString()
            TCaseId = ddlTarget.SelectedValue.ToString()
            Flag = CopyCase(SCaseId, TCaseId, "MedEcon1ConnectionString")
            If Flag Then
                GetPCaseDetails()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + SCaseId.ToString() + " variables transferred to Case #" + TCaseId.ToString() + " successfully.');", True)
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
            SCaseId = ddlPCases.SelectedValue.ToString()
            TCaseId = ddlTarget1.SelectedValue.ToString()
            Flag = CopyCase(SCaseId, TCaseId, "MedEcon1ConnectionString")
            If Flag Then
                GetPCaseDetails()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + SCaseId.ToString() + " variables transferred to Case #" + TCaseId.ToString() + " successfully.');", True)
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
        Dim objUpdatedata As New Med1UpInsData.UpdateInsert()
        Try
            CaseId = ddlPCases.SelectedValue.ToString()
            UserName = ddlUsers.SelectedValue.ToString()
            obj.CaseAccess(UserName, CaseId, "MedEcon1ConnectionString")
            obj.CaseAccess(UserName, CaseId, "MedSustain1ConnectionString")
            divshareAccess.Style.Add("Display", "none")
            If Session("Service") = "COMP" Then
                objUpdatedata.UpdateServiceId(CaseId, Session("ServiceMed1"), Session("ServiceSMed1"), UserName)
            End If
            GetTotalCaseCount()
            Flag = True
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
            SCaseId = ddlPCases.SelectedValue.ToString()
            TCaseId = CreateCase(UserName.ToString())
            Flag = CopyCase(SCaseId, TCaseId, "MedEcon1ConnectionString")
            Flag = CopyCase(SCaseId, TCaseId, "MedSustain1ConnectionString")
            If Flag Then
                GetPCaseDetails()
                GetTotalCaseCount()
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + SCaseId + " has been successfully copied to User - " + UserName.ToString() + " Proprietary case #" + TCaseId + "');", True)
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Case #" + TCaseId.ToString() + " created successfully.\nCase #" + SCaseId.ToString() + " variables transferred to Case #" + TCaseId.ToString() + " successfully.');", True)
                divsharecopy.Style.Add("Display", "none")
            End If

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

            SCaseId = ddlPCases.SelectedValue.ToString()
            TCaseId = ddlPCases.SelectedValue.ToString()
            obj.CaseSynch(SCaseId, TCaseId, "SMED1", "MED1", "MedEcon1ConnectionString", "SystemConnectionString")
            Flag = True
        Catch ex As Exception
            Flag = False
            lblError.Text = "Error:btnSynchronize_Click:" + ex.Message.ToString() + ""
        End Try
        If Flag Then
            GetPCaseDetails()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('You have suceessfully synchronized Case #" + SCaseId.ToString() + " from SMed1 to Med1');", True)
        End If
    End Sub
#End Region

#Region "Rename Case"
    Protected Sub btnPRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPRename.Click
        Try
            divModify.Visible = True
            GetCaseDetails()
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
            CaseId = ddlPCases.SelectedValue.ToString()
            obj.CaseRename(ddlPCases.SelectedValue.ToString(), txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), txtCaseDe2.Text.Trim.ToString().Replace("'", "''"), txtCaseDe3.Text.Trim.ToString().Replace("'", "''"), "MedEcon1ConnectionString")
            GetPCaseDetails()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Case#" + CaseId.ToString() + " updated successfully');", True)
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
            CaseId = CInt(ddlPCases.SelectedValue.ToString())
            obj.CaseDelete(Session("UserId").ToString(), CaseId.ToString(), "MedEcon1ConnectionString")
            obj.CaseDelete(Session("UserId").ToString(), CaseId.ToString(), "MedSustain1ConnectionString")

            'Delete Case from Group
            obj.GroupCaseDelete(Session("UserId").ToString(), CaseId.ToString(), "MedEcon1ConnectionString")

            GetPCaseDetails()
            GetTotalCaseCount()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Case#" + CaseId.ToString() + " deleted successfully');", True)
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
        Dim objUpdatedata As New Med1UpInsData.UpdateInsert()
        Try
            CaseId = obj.CreateCase(UserId, "MedEcon1ConnectionString", "MEDE1", 0)
            obj.CreateCase(UserId, "MedSustain1ConnectionString", "MEDE1", CaseId)
            If Session("Service") = "COMP" Then
                objUpdatedata.UpdateServiceId(CaseId, Session("ServiceMed1"), Session("ServiceSMed1"), UserId)
            End If
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
