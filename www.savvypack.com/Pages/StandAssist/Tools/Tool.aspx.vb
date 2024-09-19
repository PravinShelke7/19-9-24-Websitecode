Imports System.Data
Imports System.Data.OleDb
Imports System
Imports StandGetData
Imports StandUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports ToolCCS
Partial Class Pages_StandAssist_Tools_Tool
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " Savvypack Corporation."
                GetBCaseDetails()
                GetPCaseDetails()
                GetUserDetails()
                GetTotalCaseCount()
                hidPropCase.Value = "0"
                hidPropCase1.Value = "0"
                hidPropCaseD.Value = "Select Structure"

                hidApprovedCase.Value = "0"
                hidApprovedCaseD.Value = "Select Structure"

                hidUserId.Value = "0"
                hidUserIdShare.Value = "0"
                hidUsername.Value = "Select User"

                hidCaseId1.Value = "0"
                hidCaseId2.Value = "0"
                hidCaseId3.Value = "0"
                hidUserId1.Value = "0"
                hidUserId2.Value = "0"


                Dim objUpIns As New StandUpInsData.UpdateInsert
                objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked on Manage Structures Button", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Opened Proprietary Structure Tools Page", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")

                'objUpIns.InsertLog1(Session("UserId").ToString(), Session("UserName").ToString(), "Tool.aspx", Page.Title, "Requested Page", "null", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "")
            End If
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub GetBCaseDetails()
        Dim objGetData As New StandGetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetBCaseDetails()

        Catch ex As Exception
            lblError.Text = "Error:GetBCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

	 Protected Sub btnRenameGroup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRenameGroup.Click
        Try
            hidCreate.Value = "0"
            If hidGrpId.Value = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Select Group');", True)
            Else
                divGModify.Visible = True
                btnGRename.Text = "Rename"
                lnkPropGrps.Text = hidGroupReportD.Value
                GetGroupDetails()
            End If

        Catch ex As Exception
            lblError.Text = "Error:btnPRename_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
	Protected Sub GetGroupDetails()
        Dim objGetData As New StandGetData.Selectdata()
        Dim ds As New DataSet
        Dim groupId As New Integer
        Try
            If hidCreate.value = "0" Then
                groupId = CInt(hidGrpId.Value)
                ds = objGetData.GetBGroupCaseDet2(Session("USERID"), "PROP", groupId)
                txtGDES1.Text = ds.Tables(0).Rows(0).Item("DES1").ToString()
                txtGDES2.Text = ds.Tables(0).Rows(0).Item("DES2").ToString()
                txtGDES3.Text = ds.Tables(0).Rows(0).Item("DES3").ToString()
                txtGAPP.Text = ds.Tables(0).Rows(0).Item("APPLICATION").ToString()
                lblGHeader.Text = "Rename Group"
            Else
                txtGDES1.Text = ""
                txtGDES2.Text = ""
                txtGDES3.Text = ""
                txtGAPP.Text = ""
                lblGHeader.Text = "Create Group"
            End If

        Catch ex As Exception
            lblError.Text = "Error:GetGroupDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub
	   Protected Sub btnCreateGrp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateGrp.Click
        Try
            hidCreate.Value = "1"
            divGModify.Visible = True
            btnGRename.Text = "Create"
            GetGroupDetails()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetPCaseDetails()
        Dim objGetData As New StandGetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetPCaseDetails(Session("USERID").ToString())
            
            
            If ds.Tables(0).Rows.Count <= 0 Then
                btnCopyPcase.Visible = False
                btnPTransfer.Visible = False
                btnBTransfer.Visible = False
                btnPShareCase.Visible = False
                btnPShareCopy.Visible = False
                'btnSynchronize.Visible = False
                btnPRename.Visible = False
                btnDelete.Visible = False
                

            Else
                btnCopyPcase.Visible = True
                btnPTransfer.Visible = True
                btnBTransfer.Visible = True
                btnPShareCase.Visible = True
                btnPShareCopy.Visible = True
                'btnSynchronize.Visible = True
                btnPRename.Visible = True
                btnDelete.Visible = True
                

            End If
        Catch ex As Exception
            lblError.Text = "Error:GetPCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetUserDetails()
        Dim objGetData As New StandGetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetUserCompanyUsers(Session("SBAUserName").ToString())
            With ddlUsers
                .DataSource = ds
                .DataTextField = "USERNAME"
                .DataValueField = "USERID"
                .DataBind()
                .Font.Size = 8
            End With

        Catch ex As Exception
            lblError.Text = "Error:GetUserDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetCaseDetails()
        Dim objGetData As New StandGetData.Selectdata()
        Dim ds As New DataSet
        Dim CaseId As New Integer
        Try
            CaseId = CInt(hidPropCase.Value.ToString())

            ds = objGetData.GetCaseDetails(CaseId.ToString())
            If ds.Tables(0).Rows.Count <= 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Structure');", True)
            Else

                txtCaseDe1.Text = ds.Tables(0).Rows(0).Item("CASEDE1").ToString()
                txtCaseDe2.Text = ds.Tables(0).Rows(0).Item("CASEDE2").ToString()
                txtCaseDe3.Text = ds.Tables(0).Rows(0).Item("CASEDE3").ToString()
txtApp.Text = ds.Tables(0).Rows(0).Item("APPLICATION").ToString()
            End If


        Catch ex As Exception
            lblError.Text = "Error:GetCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetTotalCaseCount()
        Dim objGetData As New StandGetData.Selectdata()
        Dim ds As New DataSet()
        Dim dsSUserCount As New DataSet()
        Dim dsShareUserCount As New DataSet()
        Dim dsSUserMaxCountE1 As New DataSet()
        Dim dsSUserMaxCountS1 As New DataSet()
        Dim dsShareUserMaxCount As New DataSet()
        Try

            'Getting Total case for current user

            ds = objGetData.GetTotalCaseCount(Session("USERID").ToString())


            If ds.Tables(0).Rows.Count > 0 Then
                hidTotalCaseCount.Value = ds.Tables(0).Rows(0).Item("TOTALCOUNT").ToString()
            End If

            'Getting Total Case created for selected User for share copy

            dsSUserCount = objGetData.GetTotalCaseCount(ddlUsers.SelectedValue)


            'Getting details for selected user for share Copy

            dsSUserMaxCountE1 = objGetData.GetSelectedUserDetails(ddlUsers.SelectedItem.Text.ToUpper(), "StandAssist")


            If dsSUserMaxCountE1.Tables(0).Rows.Count > 0 Then
                hidUserMaxCaseCount.Value = CInt(dsSUserMaxCountE1.Tables(0).Rows(0).Item("MAXCASECOUNT")).ToString()
                hidTotalSCount.Value = CInt(dsSUserCount.Tables(0).Rows(0).Item("TOTALCOUNT")).ToString()
            End If

            'Getting Total Case details for selected user for share Access
            dsShareUserCount = objGetData.GetTotalCaseCount(ddlUsers.SelectedValue)


            'Getting details for selected user for share Copy
            dsShareUserMaxCount = objGetData.GetSelectedUserDetails(ddlUsers.SelectedItem.Text.ToUpper(), "StandAssist")



            If dsShareUserMaxCount.Tables(0).Rows.Count > 0 Then
                hidShareUserMaxCaseCount.Value = CInt(dsShareUserMaxCount.Tables(0).Rows(0).Item("MAXCASECOUNT")).ToString()
                hidTotalShareCount.Value = CInt(dsShareUserCount.Tables(0).Rows(0).Item("TOTALCOUNT")).ToString()
            Else
                hidShareUserMaxCaseCount.Value = ""
                hidTotalShareCount.Value = ""

            End If

        Catch ex As Exception
            lblError.Text = "Error:GetTotalCaseCount:" + ex.Message.ToString() + ""
        End Try
    End Sub

#Region "Copy A Case"
    Protected Sub btnCopyBcase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopyBcase.Click
        Dim SCaseId As String = String.Empty
        Dim TCaseId As String = String.Empty
        Dim objUpIns As New StandUpInsData.UpdateInsert
        Dim Flag As Boolean
        Dim flagVal1 As Boolean
        Try
            If hidApprovedCase.Value = "0" Or hidApprovedCase.Value = " " Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Structure');", True)
                lnkApprovedCase.Text = "Select Structure"
				 'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Copy Base Structure But No Structure was Selected.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            Else
                SCaseId = hidApprovedCase.Value.ToString()
                TCaseId = CreateCase(Session("USERID"))
                Flag = CopyCase(SCaseId, TCaseId, "SBAConnectionString")
                'Flag = CopyCase(SCaseId, TCaseId, "Sustain1ConnectionString")
                If Flag Then
                    GetBCaseDetails()
                    GetPCaseDetails()
                    GetTotalCaseCount()
                    'Started Activity Log Changes
                    If hidCaseId1.Value.ToString() = hidApprovedCase.Value.ToString() Then
                        flagVal1 = False
                    Else
                        flagVal1 = True
                    End If
                    hidCaseId1.Value = "0"
                    Try
                        If flagVal1 = True Then
                            objUpIns.InsertLog1(Session("UserId").ToString(), "8", "Selected Public Structure #" + hidApprovedCase.Value, hidApprovedCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")

                        End If
                        objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Copy Base Structure #" + SCaseId.ToString() + " ", SCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Created new Proprietary Structure #" + TCaseId.ToString() + ". Copied Data from Base Structure: #" + SCaseId.ToString() + " to Proprietary Structure #" + TCaseId.ToString() + "", TCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        objUpIns.InsertLog3(Session("UserId").ToString(), "2", "Copied Data from Base Structure #" + SCaseId.ToString() + " to Proprietary Structure #" + TCaseId.ToString() + "", SCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, TCaseId.ToString())

                    Catch ex As Exception
                    End Try
                   'Ended Activity Log Changes
				   Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Structure #" + TCaseId.ToString() + " created successfully.\n Structure #" + SCaseId.ToString() + " variables transferred to Structure #" + TCaseId.ToString() + " successfully.');", True)
                    lnkApprovedCase.Text = "Select Structure"
                    hidApprovedCase.Value = "0"
                    hidApprovedCaseD.Value = "Select Structure"
			   End If
            End If
           
        Catch ex As Exception
            lblError.Text = "Error:btnCopyBcase_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub btnCopyPcase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopyPcase.Click
        Dim SCaseId As String = String.Empty
        Dim TCaseId As String = String.Empty
        Dim objUpIns As New StandUpInsData.UpdateInsert
        Dim Flag As Boolean
        Try
            If hidPropCase.Value = "0" Or hidPropCase.Value = " " Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Structure');", True)
                lnkPropCase.Text = "Select Structure"
				 'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Copy Proprietary Structure But No Structure was Selected.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes

            Else
                SCaseId = hidPropCase.Value.ToString()
                TCaseId = CreateCase(Session("USERID"))
                Flag = CopyCase(SCaseId, TCaseId, "SBAConnectionString")
                'Flag = CopyCase(SCaseId, TCaseId, "Sustain1ConnectionString")
                If Flag Then
                    GetPCaseDetails()
                    GetTotalCaseCount()
                     'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "8", "Selected Proprietary Structure #" + hidPropCase.Value, hidPropCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Copy Proprietary Structure #" + SCaseId + " ", SCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Created new Proprietary Structure #" + TCaseId.ToString() + " .Copied Data from Proprietary Structure #" + SCaseId.ToString() + " to Proprietary Structure #" + TCaseId.ToString() + "", TCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        objUpIns.InsertLog3(Session("UserId").ToString(), "2", "Copied Material Data from Proprietary Structure #" + SCaseId.ToString() + " to Proprietary Structure #" + TCaseId.ToString() + "", SCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, TCaseId.ToString())

                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
				   Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Structure #" + TCaseId.ToString() + " created successfully.\n Structure #" + SCaseId.ToString() + " variables transferred to Structure #" + TCaseId.ToString() + " successfully.');", True)
                    lnkPropCase.Text = "Select Structure"
                    hidPropCase.Value = "0"
                    hidPropCaseD.Value = "Select Structure"
			  End If
            End If
            
        Catch ex As Exception
            lblError.Text = "Error:btnCopyPcase_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
#End Region
#Region "Tools Function"
    Protected Function CreateCase(ByVal UserId As String) As Integer
        Dim CaseId As Integer
        Dim obj As New ToolCCS
        Try
            CaseId = obj.CreateCase(UserId, "SBAConnectionString", "SBA", 0)

            Return CaseId
        Catch ex As Exception
            lblError.Text = "Error:CreatedCase:" + ex.Message.ToString() + ""
            Return CaseId
        End Try
    End Function
    Protected Function CopyCase(ByVal SCaseID As String, ByVal TCaseId As String, ByVal Schema As String) As Boolean
        Dim obj As New ToolCCS
        Try
            obj.CopyCaseSBA(SCaseID, TCaseId, Schema)
            obj.CopyBlendMat(SCaseID, TCaseId, Schema)
            lnkApprovedCase.Text = hidApprovedCaseD.Value.ToString()
            Return True
        Catch ex As Exception
            lblError.Text = "Error:CopyCase:" + ex.Message.ToString() + ""
            Return False
        End Try
    End Function
#End Region
#Region "Transfer A Case"
    Protected Sub btnBS2T_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBS2T.Click
        Dim SCaseId As String = String.Empty
        Dim TCaseId As String = String.Empty
        Dim objUpIns As New StandUpInsData.UpdateInsert
        Dim Flag As Boolean
        Dim flagVal1 As Boolean
        Try
            If hidApprovedCase.Value = "0" Or hidApprovedCase.Value = " " Or hidPropCase1.Value = "" Or hidPropCase1.Value = "0" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Structure');", True)
                lnkApprovedCase.Text = hidApprovedCaseD.Value
                lnkTargetPropCase.Text = "Select Structure"
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Transfer Button from Base to Proprietary Structure But No Structure was Selected.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            Else
                SCaseId = hidApprovedCase.Value.ToString()
                TCaseId = hidPropCase1.Value.ToString()
                Flag = CopyCase(SCaseId, TCaseId, "SBAConnectionString")
                If Flag Then
                    GetPCaseDetails()
                    'Started Activity Log Changes
                    objUpIns.InsertLog1(Session("UserId").ToString(), "8", "Selected Target Proprietary Structure #" + hidPropCase1.Value, hidPropCase1.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Try
                        hidCaseId1.Value = "0"
                        objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Transfer Base Structure from #" + SCaseId.ToString() + " to Proprietary Structure #" + TCaseId.ToString() + " ", SCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Transferred Data from Base Structure #" + SCaseId.ToString() + " to Proprietary Structure #" + TCaseId.ToString() + "", TCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        objUpIns.InsertLog3(Session("UserId").ToString(), "2", "Transferred Material Data from Base Structure #" + SCaseId.ToString() + " to Proprietary Structure #" + TCaseId.ToString() + "", SCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, TCaseId.ToString())

                    Catch ex As Exception
                    End Try
                    'Ended Activity Log Changes
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Structure #" + SCaseId.ToString() + " variables transferred to Structure #" + TCaseId.ToString() + " successfully.');", True)

                    hidApprovedCase.Value = "0"
                    hidApprovedCaseD.Value = "Select Structure"
                    hidPropCase1.Value = "0"
                    lnkApprovedCase.Text = "Select Structure"
                    lnkPropCase.Text = "Select Structure"

                    divBCopy.Style.Add("Display", "none")
                End If
            End If

        Catch ex As Exception
            lblError.Text = "Error:btnBS2T_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnBS2TCancle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBS2TCancle.Click
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            divBCopy.Style.Add("Display", "none")
            lnkPropCase.Text = "Select Structure"
            'lnkPropCase.Text = hidPropCaseD.Value.ToString()
            hidApprovedCase.Value = "0"
            hidApprovedCaseD.Value = "Select Structure"
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Base Structure Transfer Cancel Button", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception
            End Try
            'Ended Activity Log Changes

        Catch ex As Exception
            lblError.Text = "Error:btnBS2TCancle_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnPS2T_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPS2T.Click
        Dim SCaseId As String = String.Empty
        Dim TCaseId As String = String.Empty
        Dim Flag As Boolean
        Dim objUpIns As New StandUpInsData.UpdateInsert
        Try
            If hidPropCase.Value = "0" Or hidPropCase.Value = " " Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Structure');", True)
                lnkPropCase.Text = "Select Structure"
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Transfer Proprietary Structure But No Structure was Selected.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes

            Else
                SCaseId = hidPropCase.Value.ToString()
                TCaseId = hidPropCase1.Value.ToString()
                Flag = CopyCase(SCaseId, TCaseId, "SBAConnectionString")
                If Flag Then
                    GetPCaseDetails()
                    'Started Activity Log Changes
                    Try
                        hidCaseId2.Value = "0"
                        objUpIns.InsertLog1(Session("UserId").ToString(), "8", "Selected Proprietary Target Structure #" + hidPropCase1.Value, hidPropCase1.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")

                        objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Transfer Proprietary Structure from #" + SCaseId.ToString() + " to Proprietary Structure #" + TCaseId.ToString() + " ", SCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Transferred Data from Proprietary Structure #" + SCaseId.ToString() + " to Proprietary Structure #" + TCaseId.ToString() + "", TCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        objUpIns.InsertLog3(Session("UserId").ToString(), "2", "Transferred Material Data from Proprietary Structure: #" + SCaseId.ToString() + " to Proprietary Structure: #" + TCaseId.ToString() + "", SCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, TCaseId.ToString())
                    Catch ex As Exception
                    End Try
                    'Ended Activity Log Changes
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Structure #" + SCaseId.ToString() + " variables transferred to Structure #" + TCaseId.ToString() + " successfully.');", True)

                    lnkPropCase.Text = "Select Structure"
                    lnkTarget1PropCase.Text = "Select Structure"
                    hidPropCase.Value = "0"
                    hidPropCaseD.Value = "Select Structure"
                    hidPropCase1.Value = "0"
                    divCopy.Style.Add("Display", "none")
                End If
            End If

        Catch ex As Exception
            lblError.Text = "Error:btnPS2T_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnTransferC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTransferC.Click
        Try
            divCopy.Style.Add("Display", "none")

            lnkPropCase.Text = hidPropCaseD.Value.ToString()
  Dim objUpIns As New StandUpInsData.UpdateInsert
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Proprietary Transfer Cancel Button", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception
            End Try
            'Ended Activity Log Changes
        Catch ex As Exception
            lblError.Text = "Error:btnTransferC_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

#End Region

#Region "Share Access"
    Protected Sub btnShareAccess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShareAccess.Click
        Dim CaseId As String = String.Empty
        Dim UserName As String = String.Empty
        Dim UserID As String = String.Empty
        Dim obj As New ToolCCS
        Dim objUpIns As New StandUpInsData.UpdateInsert
        Dim Flag As Boolean


        Try
            If hidPropCase.Value = "0" Or hidPropCase.Value = " " Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Structure');", True)
                lnkPropCase.Text = "Select Structure"
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Share Access But No Structure was Selected.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            Else
                CaseId = hidPropCase.Value.ToString()
                UserName = hidUsernameD.Value.ToString()
                UserID = hidUserId.Value.ToString()
                obj.CaseAccessSBA(UserID, CaseId, "SBAConnectionString")
                'Started Activity Log Changes
              
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "8", "Selected User #" + hidUsernameD.Value, "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Share Copy Button ", CaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")

                    objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Proprietary Structure #" + CaseId.ToString() + " shared with User " + UserName + "", CaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    objUpIns.InsertLog3(Session("UserId").ToString(), "2", "Proprietary Structure #" + CaseId.ToString() + " material data shared with User " + UserName + " with Data", CaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, CaseId.ToString())
                Catch ex As Exception
                End Try
                'Ended Activity Log Changes
				divshareAccess.Style.Add("Display", "none")
                Flag = True
                GetTotalCaseCount()
                lnkPropCase.Text = "Select Structure"
                hidPropCase.Value = "0"
                hidPropCaseD.Value = "Select Structure"
                hidUsernameD.Value = ""
				lnkUser1.Text ="Select User"
            End If
           
            
        Catch ex As Exception
            Flag = False
            lblError.Text = "Error:btnShareAccess_Click:" + ex.Message.ToString() + ""
        End Try
        If Flag Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Structure #" + CaseId + " has been successfully shared with User " + UserName.ToString() + "');", True)
        End If
    End Sub
    Protected Sub btnShareAccessC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShareAccessC.Click
        Try
            divshareAccess.Style.Add("Display", "none")
            hidUsernameD.Value = ""
            lnkPropCase.Text = hidPropCaseD.Value.ToString()

            lnkUser1.Text = "Select User"
        Catch ex As Exception
            lblError.Text = "Error:btnShareAccessC_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
#End Region

#Region "Share A Copy"
    Protected Sub btnShareCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShareCopy.Click
        Dim SCaseId As String = String.Empty
        Dim TCaseId As String = String.Empty
        Dim UserName As String = hidUsernameD.Value.ToString()
        Dim UserID As String = hidUserIdShare.Value.ToString()
        Dim objUpIns As New StandUpInsData.UpdateInsert
        Dim Flag As Boolean
        Try
            If hidPropCase.Value = "0" Or hidPropCase.Value = " " Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Structure');", True)
                lnkPropCase.Text = "Select Structure"
				
				   'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Share Copy But No Structure was selected.", TCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception
                End Try
                'Ended Activity Log Changes

            Else
                SCaseId = hidPropCase.Value.ToString()
                TCaseId = CreateCase(UserID.ToString())
                Flag = CopyCase(SCaseId, TCaseId, "SBAConnectionString")
                If Flag Then
				   'Started Activity Log Changes
                    Try
                        hidCaseId2.Value = "0"
                        objUpIns.InsertLog1(Session("UserId").ToString(), "8", "Selected User #" + hidUsernameD.Value, "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Share Copy Button ", SCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")

                        objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Proprietary Structure #" + SCaseId.ToString() + " copied to User " + UserName + "", TCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        objUpIns.InsertLog3(Session("UserId").ToString(), "2", "Proprietary Structure " + SCaseId.ToString() + " material data copied to User " + UserName + " with Data", SCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, TCaseId.ToString())
                    Catch ex As Exception
                    End Try
                'Ended Activity Log Changes
                    GetPCaseDetails()
                    GetTotalCaseCount()
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Structure #" + TCaseId.ToString() + " created successfully.\n Structure #" + SCaseId.ToString() + " variables transferred to Structure #" + TCaseId.ToString() + " successfully.');", True)
                    divsharecopy.Style.Add("Display", "none")
                    lnkPropCase.Text = "Select Structure"
                    hidUsernameD.Value = ""
                    hidPropCase.Value = "0"
                    hidPropCaseD.Value = "Select Structure"
            lnkUser2.Text = "Select user"
                End If
            End If
             
        Catch ex As Exception
            lblError.Text = "Error:btnShareCopy_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub btnShareCopyC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShareCopyC.Click
        Try
            divsharecopy.Style.Add("Display", "none")
            hidUsernameD.Value = ""
            lnkUser2.Text = "Select user"
            lnkPropCase.Text = hidPropCaseD.Value.ToString()
        Catch ex As Exception
            lblError.Text = "Error:btnShareCopyC_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
#End Region

    
#Region "Rename Case"
    Protected Sub btnRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRename.Click
        Dim obj As New ToolCCS
        Dim CaseId As String = String.Empty
        Dim objUpIns As New StandUpInsData.UpdateInsert
        Try
            If hidPropCase.Value = "0" Or hidPropCase.Value = " " Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Structure');", True)
                lnkPropCase.Text = "Select Structure"

				  'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Rename Proprietary Structure But No Structure was selected.", CaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception
                End Try
                'Ended Activity Log Changes
            Else
                CaseId = hidPropCase.Value.ToString()
				   obj.CasePropRename(hidPropCase.Value.ToString(), txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), txtCaseDe2.Text.Trim.ToString().Replace("'", "''"), txtCaseDe3.Text.Trim.ToString().Replace("'", "''"), txtApp.Text.Trim().ToString().Replace("'", "''"), "SBAConnectionString")
               
			     'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Renamed Proprietary Structure #" + CaseId.ToString() + "", CaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception
                End Try
                'Ended Activity Log Changes
			   GetPCaseDetails()
               Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Structure#" + CaseId.ToString() + " updated successfully');", True)
                divModify.Visible = False
                lnkPropCase.Text = "Select Structure"
                hidPropCase.Value = "0"
                hidPropCaseD.Value = "Select Structure"
            End If
           

        Catch ex As Exception
            lblError.Text = "Error:btnRename_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub btnPRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPRename.Click
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            Dim flagVal1 As Boolean
            If hidPropCase.Value = "0" Or hidPropCase.Value = " " Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Structure');", True)
                lnkPropCase.Text = "Select Structure"
				   'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Proprietary Structure Rename Button But No Structure was Selected.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes

            Else
                divModify.Visible = True
                GetCaseDetails()
				  lnkPropCase.Text = hidPropCaseD.Value.ToString()
                'Started Activity Log Changes
                If hidCaseId2.Value.ToString() = hidPropCase.Value.ToString() Then
                    flagVal1 = False
                Else
                    flagVal1 = True
                End If
                hidCaseId2.Value = hidPropCase.Value.ToString()

                Try
                    If flagVal1 = True Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "8", "Selected Proprietary Structure #" + hidPropCase.Value, hidPropCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")

                    End If
                    objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Proprietary Structure Rename Button", hidPropCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception
                End Try
                'Ended Activity Log Changes
            End If
          
            
        Catch ex As Exception
            lblError.Text = "Error:btnPRename_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub


    Protected Sub btnRenameC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRenameC.Click
        Try
		  Dim objUpIns As New StandUpInsData.UpdateInsert
            divModify.Visible = False
            GetCaseDetails()
            lnkPropCase.Text = hidPropCaseD.Value.ToString()
  'Started Activity Log Changes
            Try
                hidCaseId2.Value = "0"
                objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Proprietary Structure Rename Cancel Button", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception
            End Try
            'Ended Activity Log Changes
        Catch ex As Exception
            lblError.Text = "Error:btnRenameC_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
#End Region


#Region "Delete Case"
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim obj As New ToolCCS
        Dim CaseId As New Integer
        Dim objUpIns As New StandUpInsData.UpdateInsert
        Try

            If hidPropCase.Value = "0" Or hidPropCase.Value = " " Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Structure');", True)
                lnkPropCase.Text = "Select Structure"
				
				  'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "8", "Selected Proprietary Structure #" + hidPropCase.Value, hidPropCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Proprietary Structure Delete Button But No Structure was Selected.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes

            Else
                CaseId = CInt(hidPropCase.Value.ToString())
                obj.CaseDelete(Session("USERID").ToString(), CaseId.ToString(), "SBAConnectionString")
                objUpIns.DeleteStructureGrps(CaseId.ToString())
                'obj.CaseDelete(Session("S1ToolUserName").ToString(), CaseId.ToString(), "Sustain1ConnectionString")
                GetPCaseDetails()
                GetTotalCaseCount()
                
				'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Proprietary Structure Delete Button", CaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Deleted Structure #" + CaseId.ToString() + " ", CaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception
                End Try
                'Ended Activity Log Changes
				Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Structure#" + CaseId.ToString() + " deleted successfully');", True)
                hidPropCase.Value = "0"
                lnkPropCase.Text = "Select Structure"
                hidPropCaseD.Value = "Select Structure"
		   End If
          
        Catch ex As Exception
            lblError.Text = "Error:btnDelete_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
#End Region

#Region "Create Case"
    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Try

            Dim CaseId As New Integer
            Dim objUpIns As New StandUpInsData.UpdateInsert

            CaseId = CreateCase(Session("USERID"))
            GetPCaseDetails()
            GetTotalCaseCount()
			
			 'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Proprietary Structure Create Button", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Created new Proprietary Structure #" + CaseId.ToString() + " ", CaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception
            End Try
            'Ended Activity Log Changes
			
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Structure #" + CaseId.ToString() + " created successfully');", True)
            lnkApprovedCase.Text = hidApprovedCaseD.Value ' "Select Structure"
            lnkPropCase.Text = hidPropCaseD.Value
        Catch ex As Exception
            lblError.Text = "Error:btnCreate_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
#End Region
Protected Sub btnGRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGRename.Click
        Dim obj As New ToolCCS
        Dim groupID As String = String.Empty
        Try
            If hidCreate.Value = "0" Then
                groupID = hidGrpId.Value
                obj.GroupRenamePropStruct(hidGrpId.Value, txtGDES1.Text.Trim.ToString().Replace("'", "''"), txtGDES2.Text.Trim.ToString().Replace("'", "''"), txtGDES3.Text.Trim.ToString().Replace("'", "''"), txtGAPP.Text.Trim.ToString().Replace("'", "''"), "SBAConnectionString")
                'GetBaseGroups()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Group#" + groupID.ToString() + " updated successfully');", True)
                divGModify.Visible = False
				txtGDES1.Text = ""
                txtGDES2.Text = ""
                txtGDES3.Text = ""
                txtGAPP.Text = ""
                hidGrpId.Value = "0"
                hidGroupReportD.Value = "Select Group"
                lnkPropGrps.Text = "Select Group"
            Else
                Dim ID As String = 1
                Dim Name As String = ""
                Name = Trim(txtGDES1.Text)
                Dim objUpdateData As New StandUpInsData.UpdateInsert
                Dim objGetData As New StandGetData.Selectdata()
                Dim dt As New DataSet()
                Dim dsGrps As New DataSet()

                If Name.Length <> 0 Then
                    dsGrps = objGetData.GetGroupIDCheck(txtGDES1.Text, txtGDES2.Text, Session("UserId").ToString(), "PROP")
                    If dsGrps.Tables(0).Rows.Count > 0 Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Group already exist');", True)
                    Else
                        objUpdateData.AddStructureGroup(txtGDES1.Text, txtGDES2.Text, txtGDES3.Text, txtGAPP.Text, "", Session("UserId"), "PROP")
                        txtGDES1.Text = ""
                        txtGDES2.Text = ""
                        txtGDES3.Text = ""
                        txtGAPP.Text = ""
                        'GetBaseGroups()
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Group created successfully');", True)
                        divGModify.Visible = False
                    End If
                End If

            End If


        Catch ex As Exception
            lblError.Text = "Error:btnRename_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnGCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGCancel.Click
        Try
            divGModify.Visible = False
            lnkPropGrps.Text = "Select Groups"
        Catch ex As Exception

        End Try
    End Sub
  Protected Sub btnBTransfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBTransfer.Click
        Try
            Dim flagVal1 As Boolean
            Dim objUpIns As New StandUpInsData.UpdateInsert
            If hidApprovedCase.Value = "" Or hidApprovedCase.Value = "0" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Source Structure');", True)
                lnkApprovedCase.Text = "Select Structure"
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Base Structure Transfer Button But No Structure was Selected.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            Else
                divBCopy.Style.Add("Display", "inline")
                lnkApprovedCase.Text = hidApprovedCaseD.Value.ToString()


                'Started Activity Log Changes
                If hidCaseId1.Value.ToString() = hidApprovedCase.Value.ToString() Then
                    flagVal1 = False
                Else
                    flagVal1 = True
                End If
                hidCaseId1.Value = hidApprovedCase.Value.ToString()
                Try
                    If flagVal1 = True Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "8", "Selected Public Structure #" + hidCaseId1.Value, hidCaseId1.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")

                    End If
                    objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Base Structure Transfer Button and Target structure displayed", hidApprovedCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception
                End Try
                'Ended Activity Log Changes

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnPTransfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPTransfer.Click
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            Dim flagVal1 As Boolean
            If hidPropCase.Value = "" Or hidPropCase.Value = "0" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Source Structure');", True)
                lnkPropCase.Text = "Select Structure"

                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Proprietary Structure Transfer Button But No Structure was Selected.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes

            Else
                divCopy.Style.Add("Display", "inline")
                lnkPropCase.Text = hidPropCaseD.Value.ToString()


                'Started Activity Log Changes
                Try
                    If hidCaseId2.Value.ToString() = hidPropCase.Value.ToString() Then
                        flagVal1 = False
                    Else
                        flagVal1 = True
                    End If
                    hidCaseId2.Value = hidPropCase.Value.ToString()
                    If flagVal1 = True Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "8", "Selected Proprietary Structure #" + hidPropCase.Value, hidPropCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")

                    End If
                    objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Proprietary Structure Transfer Button", hidPropCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception
                End Try
                'Ended Activity Log Changes

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnPShareCase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPShareCase.Click
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            Dim flagVal1 As Boolean
            If hidPropCase.Value = "" Or hidPropCase.Value = "0" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Source Structure');", True)
                lnkPropCase.Text = "Select Structure"
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Proprietary Structure Share Access Button But No Structure was Selected.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            Else
                divshareAccess.Style.Add("Display", "inline")
                lnkPropCase.Text = hidPropCaseD.Value.ToString()
                'Started Activity Log Changes
                If hidCaseId2.Value.ToString() = hidPropCase.Value.ToString() Then
                    flagVal1 = False
                Else
                    flagVal1 = True
                End If
                hidCaseId2.Value = hidPropCase.Value.ToString()
                Try
                    If flagVal1 = True Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "8", "Selected Proprietary Structure #" + hidPropCase.Value, hidPropCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")

                    End If
                    objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Proprietary Structure Share Access Button", hidPropCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception
                End Try
                'Ended Activity Log Changes
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnPShareCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPShareCopy.Click
        Try
            Dim flagVal1 As Boolean
            Dim objUpIns As New StandUpInsData.UpdateInsert
            If hidPropCase.Value = "" Or hidPropCase.Value = "0" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Source Structure');", True)
                lnkPropCase.Text = "Select Structure"

                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Proprietary Structure Share a Copy Button But No Structure was Selected.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes

            Else
                divsharecopy.Style.Add("Display", "inline")
                lnkPropCase.Text = hidPropCaseD.Value.ToString()

                'Started Activity Log Changes
                If hidCaseId2.Value.ToString() = hidPropCase.Value.ToString() Then
                    flagVal1 = False
                Else
                    flagVal1 = True
                End If
                hidCaseId2.Value = hidPropCase.Value.ToString()
                Try
                    If flagVal1 = True Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "8", "Selected Proprietary Structure #" + hidPropCase.Value, hidPropCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    End If

                    objUpIns.InsertLog1(Session("UserId").ToString(), "2", "Clicked on Proprietary Structure Share a Copy Button", hidPropCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception
                End Try
                'Ended Activity Log Changes

            End If
        Catch ex As Exception

        End Try
    End Sub


End Class
