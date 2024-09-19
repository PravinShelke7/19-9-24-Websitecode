Imports System.Data
Imports System.Data.OleDb
Imports System
Imports StandGetData
Imports StandUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_StructAssist_Default
    Inherits System.Web.UI.Page

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_STRUCTASSIST_DEFAULT")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objUpIns As New StandUpInsData.UpdateInsert
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

                If Session("SBAUserRole") = "AADMIN" Then
                    IsBSManage.Visible = True
                    IsBManage.Visible = True
                Else
                    IsBSManage.Visible = False
                    IsBManage.Visible = False
                End If


                If Session("SBACompLib") = "Y" Then
                    IsCompanyAdmin.Visible = True
                    If Session("SBALicAdmin") = "Y" Then
                        btnCompGManage.Visible = True
                        btnCompSRename.Visible = True
                        btnCompSManage.Visible = True
                    Else
                        trCGroup.Visible = False
                        btnCompGManage.Visible = False
                        btnCompSRename.Visible = False
                        btnCompSManage.Visible = False
                    End If
                Else
                    IsCompanyAdmin.Visible = False
                End If
                
                GetBCaseDetails()
                GetPCaseDetails()

                hidGrpId.Value = "0"
                hidGroupReportD.Value = "All Structures"

                hidGGrpId.Value = "0"
                hidGGroupReportD.Value = "All Structures"

                hidApprovedCase.Value = "0"
                hidApprovedCaseD.Value = "Select Structure"

                hidPropCase.Value = "0"
                hidPropCaseD.Value = "Select Structure"

                hidCompnyCase.Value = "0"
                hidCompnyCaseD.Value = "Select Structure"
                hidCGrpId.Value = "0"
                hidCGrpDes.Value = "All Structures"

                hidBFileName.Value = ""
                hidFileName.Value = ""

                hidGNotes.Value = ""
                hidNotes.Value = ""
                hidGPNotes.Value = ""
                hidPNotes.Value = ""


                hidCaseId1.Value = "0"
                hidCaseId2.Value = "0"
                hidCaseId3.Value = "0"
                hidGrpId1.Value = "0"
                hidGrpId2.Value = "0"
                hidGrpId3.Value = "0"

                If Session("LogInCount") = Nothing Then
                   'Started Activity Log Changes
                    Try
                        Session("LogInCount") = objUpIns.InsertLog(Session("UserId").ToString(), "1", "Logged in into Structure Assistant Module ", Session.SessionID)
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                Else
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Logged in into Structure Assistant Module", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")

                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                End If
            End If
			
			   Dim dsDetail As New DataSet
        Dim objGetdata As New StandGetData.Selectdata
		 dsDetail = objGetdata.GetUserInforMation(Session("UserId"))
		  If dsDetail.Tables(0).Rows.Count > 0 Then
                If dsDetail.Tables(0).Rows(0).Item("ISIADMINLICUSR").ToString() = "Y" Then
                    ManageUser.Visible = True
                Else
                    ManageUser.Visible = False
                End If

                End If
               
            SetToolButton()
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub SetSessions()
        Dim objGetData As New StandGetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetUserDetails(Session("ID"))
            Session("UserId") = ds.Tables(0).Rows(0).Item("USERID").ToString()
            Session("SBAUserRole") = ds.Tables(0).Rows(0).Item("USERROLE").ToString()
            Session("SBAServiceRole") = ds.Tables(0).Rows(0).Item("SERVIECROLE").ToString()
            Session("SBAUserName") = ds.Tables(0).Rows(0).Item("USERNAME").ToString()
            Session("SBAToolUserName") = ds.Tables(0).Rows(0).Item("TOOLUSERNAME").ToString()
            Session("SBAMaxCaseCount") = ds.Tables(0).Rows(0).Item("MAXCASECOUNT").ToString()

            'Set for License Administrator
            Session("SBALicAdmin") = ds.Tables(0).Rows(0).Item("ISIADMINLICUSR").ToString()
            Session("SBALicenseId") = ds.Tables(0).Rows(0).Item("LICENSEID").ToString()
            Session("SBACompLib") = ds.Tables(0).Rows(0).Item("ISCOMPLIBRARY").ToString()
        Catch ex As Exception
            lblError.Text = "Error:SetSessions:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetBCaseDetails()
        Dim objGetData As New StandGetData.Selectdata()
        Dim ds As New DataSet()
        Try

            ds = objGetData.GetBCaseDetails()
            If ds.Tables(0).Rows.Count <= 0 Then
                btnBCase.Enabled = False

                lblBcase.Visible = True
                btnBCaseSearch.Enabled = False
                lnkApprovedCase.Visible = False
            Else
                btnBCase.Enabled = True
                lblBcase.Visible = False
                btnBCaseSearch.Enabled = True
                lnkApprovedCase.Visible = True
            End If
            
        Catch ex As Exception
            lblError.Text = "Error:GetBCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetPCaseDetails()
        Dim objGetData As New StandGetData.Selectdata()
        Dim ds As New DataSet()
        Try

            If Session("SBALicAdmin") = "N" Then

                ds = objGetData.GetPCaseDetails(Session("USERID").ToString())
            Else
                ds = objGetData.GetPCaseDetailsByLicense(Session("USERID").ToString())
               

            End If

            '        If ds.Tables(0).Rows.Count <= 0 Then
            '            btnPCase.Enabled = False
            '            btRename.Enabled = False
            '            lblPcase.Visible = True
            '            lnkPropCase.Visible = False
            'btnPNotes.Visible = False

            '        Else
            '            btnPCase.Enabled = True
            '            btRename.Enabled = True
            '            lnkPropCase.Visible = True
            '            btnPNotes.Visible = True
            '            lblPcase.Visible = False
            '        End If

            btnPCase.Enabled = True
            btRename.Enabled = True
            lnkPropCase.Visible = True
            btnPNotes.Visible = True
            lblPcase.Visible = False
        Catch ex As Exception
            lblError.Text = "Error:GetPCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    

    Protected Sub SetToolButton()
        Try
            If Session("SBAServiceRole") <> "ReadWrite" Then
                btnToolBox.Enabled = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:SetToolButton:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnBCase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBCase.Click
        Try
            
            Dim objUpIns As New StandUpInsData.UpdateInsert
            Dim flagVal1, flagVal2 As Boolean
            If hidApprovedCase.Value = "0" Or hidApprovedCase.Value = " " Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Structure');", True)
                lnkApprovedCase.Text = "Select Structure"
                lnkAllBGrps.Text = hidGGroupReportD.Value
				 'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked on Start a Public Structure But No Structure was Selected.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                     Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            Else
                If CheckAnyDelGroup(hidGGrpId.Value) Then
                    If CheckAnyDelCase(hidApprovedCase.Value.ToString()) = True Then
                        Session("SBACaseId") = hidApprovedCase.Value.ToString()
                        Session("CompLib") = "N"
                        Dim CaseDes = hidApprovedCaseD.Value.ToString()
                        If Not objRefresh.IsRefresh Then
                            'Started Activity Log Changes
                            If hidCaseId1.Value.ToString() = hidApprovedCase.Value.ToString() Then
                                flagVal1 = False
                            Else
                                flagVal1 = True
                            End If
                            If hidGrpId1.Value.ToString() = hidGGrpId.Value.ToString() Then
                                flagVal2 = False
                            Else
                                flagVal2 = True
                            End If
                            hidCaseId1.Value = hidApprovedCase.Value.ToString()
                            hidGrpId1.Value = hidGGrpId.Value
                            Try
                                Dim grpVal As String = ""
                                If hidGGrpId.Value = "0" Then
                                    grpVal = ""
                                Else
                                    grpVal = hidGGrpId.Value
                                    
                                End If
                                If flagVal2 = True Then
                                    If grpVal = "" Then
                                        objUpIns.InsertLog1(Session("UserId").ToString(), "9", "Selected Public Group for All Structures", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", grpVal)

                                    Else
                                        objUpIns.InsertLog1(Session("UserId").ToString(), "9", "Selected Public Group #" + grpVal, "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", grpVal)

                                    End If

                                End If
                                If flagVal1 = True Then
                                    objUpIns.InsertLog1(Session("UserId").ToString(), "8", "Selected Public Structure #" + Session("SBACaseId").ToString(), Session("SBACaseId").ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", grpVal)

                                End If
                                
                                objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked on Start a Public Structure", Session("SBACaseId").ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", grpVal)
                                objUpIns.InsertLog1(Session("UserId").ToString(), "3", "Opened Strcuture Designer for Structure #" + Session("SBACaseId") + "", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", grpVal)
                            Catch ex As Exception

                            End Try
                            'Ended Activity Log Changes
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinB", "OpenNewWindow('Assumptions/Extrusion.aspx?Type=Base&SponsBy=" + hidSpons.Value + "');", True)
                        End If
                        lnkAllBGrps.Text = hidGGroupReportD.Value.ToString()
                        lnkApprovedCase.Text = hidApprovedCaseD.Value.ToString()

                        lnkPropCase.Text = hidPropCaseD.Value.ToString()
                        If hidGrpId.Value = "0" Then
                            lnkAllGrps.Text = "All Structures"
                        Else
                            lnkAllGrps.Text = hidGroupReportD.Value.ToString()
                        End If

                        If hidGNotes.Value = "" Then
                            btnNotes.ToolTip = "No Notes Available"
                        Else
                            btnNotes.ToolTip = hidGNotes.Value
                        End If

                        If hidFileName.Value = "" Then
                            btnSponsorM.ToolTip = "No Sponsor message Available"
                        Else
                            btnSponsorM.ToolTip = "Sponsor Message"
                        End If

                        If hidNotes.Value = "" Then
                            btnSNotes.ToolTip = "No Notes Available"
                        Else
                            btnSNotes.ToolTip = hidNotes.Value
                        End If

                        If hidBFileName.Value = "" Then
                            btnSSMessage.ToolTip = "No Sponsor message Available"
                        Else
                            btnSSMessage.ToolTip = "Sponsor Message"
                        End If

                        If hidGPNotes.Value = "" Then
                            btnPGNotes.ToolTip = "No Notes Available"
                        Else
                            btnPGNotes.ToolTip = hidGPNotes.Value
                        End If

                        If hidPNotes.Value = "" Then
                            btnPNotes.ToolTip = "No Notes Available"
                        Else
                            btnPNotes.ToolTip = hidPNotes.Value
                        End If

                        If hidCompNotes.Value = "" Then
                            btnCompNotes.ToolTip = "No Notes Available"
                        Else
                            btnCompNotes.ToolTip = hidCompNotes.Value
                        End If

                        If hidCompGrpNotes.Value = "" Then
                            btnCompGNotes.ToolTip = "No Notes Available"
                        Else
                            btnCompGNotes.ToolTip = hidCompGrpNotes.Value
                        End If


                    Else
                        lnkAllBGrps.Text = "All Structures"
                        lnkApprovedCase.Text = "Select Structure"
                        hidApprovedCase.Value = "0"
                        hidGGrpId.Value = "0"


                        lnkPropCase.Text = hidPropCaseD.Value.ToString()
                        If hidGrpId.Value = "0" Then
                            lnkAllGrps.Text = "All Structures"
                        Else
                            lnkAllGrps.Text = hidGroupReportD.Value.ToString()
                        End If


                        hidGNotes.Value = "No Notes Available"
                        btnNotes.ToolTip = hidGNotes.Value

                        hidFileName.Value = ""
                        btnSponsorM.ToolTip = "No Sponsor message Available"

                        hidNotes.Value = "No Notes Available"
                        btnSNotes.ToolTip = hidNotes.Value
                        btnSSMessage.ToolTip = "No Sponsor message Available"

                        If hidGPNotes.Value = "" Then
                            btnPGNotes.ToolTip = "No Notes Available"
                        Else
                            btnPGNotes.ToolTip = hidGPNotes.Value
                        End If

                        If hidPNotes.Value = "" Then
                            btnPNotes.ToolTip = "No Notes Available"
                        Else
                            btnPNotes.ToolTip = hidPNotes.Value
                        End If

                        If hidCompNotes.Value = "" Then
                            btnCompNotes.ToolTip = "No Notes Available"
                        Else
                            btnCompNotes.ToolTip = hidCompNotes.Value
                        End If

                        If hidCompGrpNotes.Value = "" Then
                            btnCompGNotes.ToolTip = "No Notes Available"
                        Else
                            btnCompGNotes.ToolTip = hidCompGrpNotes.Value
                        End If
                    End If
                Else
                    lnkAllBGrps.Text = "All Structures"
                    lnkApprovedCase.Text = "Select Structure"
                    hidApprovedCase.Value = "0"
                    hidGGrpId.Value = "0"


                    lnkPropCase.Text = hidPropCaseD.Value.ToString()
                    If hidGrpId.Value = "0" Then
                        lnkAllGrps.Text = "All Structures"
                    Else
                        lnkAllGrps.Text = hidGroupReportD.Value.ToString()
                    End If


                    hidGNotes.Value = "No Notes Available"
                    btnNotes.ToolTip = hidGNotes.Value

                    hidFileName.Value = ""
                    btnSponsorM.ToolTip = "No Sponsor message Available"

                    hidNotes.Value = "No Notes Available"
                    btnSNotes.ToolTip = hidNotes.Value
                    btnSSMessage.ToolTip = "No Sponsor message Available"

                    If hidGPNotes.Value = "" Then
                        btnPGNotes.ToolTip = "No Notes Available"
                    Else
                        btnPGNotes.ToolTip = hidGPNotes.Value
                    End If

                    If hidPNotes.Value = "" Then
                        btnPNotes.ToolTip = "No Notes Available"
                    Else
                        btnPNotes.ToolTip = hidPNotes.Value
                    End If

                    If hidCompNotes.Value = "" Then
                        btnCompNotes.ToolTip = "No Notes Available"
                    Else
                        btnCompNotes.ToolTip = hidCompNotes.Value
                    End If

                    If hidCompGrpNotes.Value = "" Then
                        btnCompGNotes.ToolTip = "No Notes Available"
                    Else
                        btnCompGNotes.ToolTip = hidCompGrpNotes.Value
                    End If

                End If
            End If


           

        Catch ex As Exception
            lblError.Text = "Error:btnBCase_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnPCase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPCase.Click
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            Dim CaseDes As String
            Dim flagVal1, flagVal2 As Boolean

            If hidPropCase.Value = "0" Or hidPropCase.Value = " " Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Structure');", True)
                lnkPropCase.Text = "Select Structure"
                lnkAllGrps.Text = hidGroupReportD.Value

				  'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked on Start a Proprietary Structure But No Structure was Selected.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
				
            Else
                If CheckAnyDelGroup(hidGrpId.Value) Then
                    If CheckAnyDelCase(hidPropCase.Value.ToString()) = True Then
                        Session("SBACaseId") = hidPropCase.Value.ToString()
                        CaseDes = hidPropCaseD.Value.ToString()
                        Session("CompLib") = "N"
                        If Not objRefresh.IsRefresh Then
                            'Started Activity Log Changes
                            If hidCaseId2.Value.ToString() = hidPropCase.Value.ToString() Then
                                flagVal1 = False
                            Else
                                flagVal1 = True
                            End If
                            If hidGrpId2.Value.ToString() = hidGrpId.Value.ToString() Then
                                flagVal2 = False
                            Else
                                flagVal2 = True
                            End If
                            hidCaseId2.Value = hidPropCase.Value.ToString()
                            hidGrpId2.Value = hidGrpId.Value

                            Try
                                Dim grpVal As String = ""
                                If hidGrpId.Value = "0" Then
                                    grpVal = ""
                                Else
                                    grpVal = hidGrpId.Value
                                End If
                                If flagVal2 = True Then
                                    If grpVal = "" Then
                                        objUpIns.InsertLog1(Session("UserId").ToString(), "9", "Selected Proprietary Group for All Structures", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", grpVal)
                                    Else
                                        objUpIns.InsertLog1(Session("UserId").ToString(), "9", "Selected Proprietary Group #" + grpVal, "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", grpVal)
                                    End If
                                   
                                End If
                                If flagVal1 = True Then
                                    objUpIns.InsertLog1(Session("UserId").ToString(), "8", "Selected Proprietary Structure #" + Session("SBACaseId").ToString(), Session("SBACaseId").ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", grpVal)

                                End If
                                objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked on Start a Proprietary Structure", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", grpVal)
                                objUpIns.InsertLog1(Session("UserId").ToString(), "3", "Opened Strcuture Designer Page for Structure # " + Session("SBACaseId") + "", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", grpVal)
                            Catch ex As Exception

                            End Try
                           'Ended Activity Log Changes
						   Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "OpenNewWindow('Assumptions/Extrusion.aspx?Type=Prop');", True)
                        End If
                        lnkPropCase.Text = hidPropCaseD.Value.ToString()
                        If hidGrpId.Value = "0" Then
                            lnkAllGrps.Text = "All Structures"
                        Else
                            lnkAllGrps.Text = hidGroupReportD.Value.ToString()
                        End If


                        If hidGNotes.Value = "" Then
                            btnNotes.ToolTip = "No Notes Available"
                        Else
                            btnNotes.ToolTip = hidGNotes.Value
                        End If

                        If hidFileName.Value = "" Then
                            btnSponsorM.ToolTip = "No Sponsor message Available"
                        Else
                            btnSponsorM.ToolTip = "Sponsor Message"
                        End If

                        If hidNotes.Value = "" Then
                            btnSNotes.ToolTip = "No Notes Available"
                        Else
                            btnSNotes.ToolTip = hidNotes.Value
                        End If

                        If hidBFileName.Value = "" Then
                            btnSSMessage.ToolTip = "No Sponsor message Available"
                        Else
                            btnSSMessage.ToolTip = "Sponsor Message"
                        End If

                        If hidGPNotes.Value = "" Then
                            btnPGNotes.ToolTip = "No Notes Available"
                        Else
                            btnPGNotes.ToolTip = hidGPNotes.Value
                        End If

                        If hidPNotes.Value = "" Then
                            btnPNotes.ToolTip = "No Notes Available"
                        Else
                            btnPNotes.ToolTip = hidPNotes.Value
                        End If
                    Else
                        hidPropCase.Value = "0"
                        lnkPropCase.Text = "Select Structure"
                        hidPropCaseD.Value = "Select Structure"
                        hidGrpId.Value = "0"
                        lnkAllGrps.Text = "All Structures"

                        If hidGNotes.Value = "" Then
                            btnNotes.ToolTip = "No Notes Available"
                        Else
                            btnNotes.ToolTip = hidGNotes.Value
                        End If
                        If hidFileName.Value = "" Then
                            btnSponsorM.ToolTip = "No Sponsor message Available"
                        Else
                            btnSponsorM.ToolTip = "Sponsor Message"
                        End If

                        If hidNotes.Value = "" Then
                            btnSNotes.ToolTip = "No Notes Available"
                        Else
                            btnSNotes.ToolTip = hidNotes.Value
                        End If
                        If hidBFileName.Value = "" Then
                            btnSSMessage.ToolTip = "No Sponsor message Available"
                        Else
                            btnSSMessage.ToolTip = "Sponsor Message"
                        End If

                        hidGPNotes.Value = "No Notes Available"
                        btnPGNotes.ToolTip = hidGPNotes.Value
                        hidPNotes.Value = "No Notes Available"
                        btnPNotes.ToolTip = hidPNotes.Value
                        
                    End If
                Else
                    hidPropCase.Value = "0"
                    lnkPropCase.Text = "Select Structure"
                    hidPropCaseD.Value = "Select Structure"
                    hidGrpId.Value = "0"
                    lnkAllGrps.Text = "All Structures"

                    If hidGNotes.Value = "" Then
                        btnNotes.ToolTip = "No Notes Available"
                    Else
                        btnNotes.ToolTip = hidGNotes.Value
                    End If
                    If hidFileName.Value = "" Then
                        btnSponsorM.ToolTip = "No Sponsor message Available"
                    Else
                        btnSponsorM.ToolTip = "Sponsor Message"
                    End If

                    If hidNotes.Value = "" Then
                        btnSNotes.ToolTip = "No Notes Available"
                    Else
                        btnSNotes.ToolTip = hidNotes.Value
                    End If
                    If hidBFileName.Value = "" Then
                        btnSSMessage.ToolTip = "No Sponsor message Available"
                    Else
                        btnSSMessage.ToolTip = "Sponsor Message"
                    End If

                    hidGPNotes.Value = "No Notes Available"
                    btnPGNotes.ToolTip = hidGPNotes.Value
                    hidPNotes.Value = "No Notes Available"
                    btnPNotes.ToolTip = hidPNotes.Value
                End If
                
            End If
            If hidCompNotes.Value = "" Then
                btnCompNotes.ToolTip = "No Notes Available"
            Else
                btnCompNotes.ToolTip = hidCompNotes.Value
            End If

            If hidCompGrpNotes.Value = "" Then
                btnCompGNotes.ToolTip = "No Notes Available"
            Else
                btnCompGNotes.ToolTip = hidCompGrpNotes.Value
            End If
            lnkAllBGrps.Text = hidGGroupReportD.Value.ToString()
            lnkApprovedCase.Text = hidApprovedCaseD.Value.ToString()
           

           
        Catch ex As Exception
            lblError.Text = "Error:btnPCase_Click:" + ex.Message.ToString() + ""
        End Try

    End Sub

    Private Function CheckAnyDelCase(ByVal CaseId As String) As Boolean
        Dim ds As New DataSet()
        Dim dsGroup As New DataSet()
        Dim message As String = ""
        Dim i As Integer
        Dim GetData As New StandGetData.Selectdata()
        Try

            ds = GetData.GetDelStructure(CaseId, Session("USERID").ToString())
            If ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                'message = "------------------------------------------------------"
                message = message + "Structure " + CaseId + " is no longer available.\n"
                ' message = message + "------------------------------------------------------\n"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + message + "');", True)

                Return False
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function CheckAnyDelCaseComp(ByVal CaseId As String) As Boolean
        Dim ds As New DataSet()
        Dim dsGroup As New DataSet()
        Dim message As String = ""
        Dim i As Integer
        Dim GetData As New StandGetData.Selectdata()
        Try

            ds = GetData.GetCompCaseData(CaseId)
            If ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                'message = "------------------------------------------------------"
                message = message + "Structure " + CaseId + " is no longer available.\n"
                ' message = message + "------------------------------------------------------\n"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + message + "');", True)

                Return False
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function CheckAnyDelGroup(ByVal groupId As String) As Boolean
        Dim ds As New DataSet()
        Dim dsGroup As New DataSet()
        Dim message As String = ""
        Dim i As Integer
        Dim GetData As New StandGetData.Selectdata()
        Try
            If groupId <> "0" Then
                dsGroup = GetData.GetDelGroup(groupId)
                If dsGroup.Tables(0).Rows.Count > 0 Then
                    Return True
                Else
                    ' message = "------------------------------------------------------"
                    message = message + "Group " + groupId + " is no longer available.\n"
                    'message = message + "------------------------------------------------------\n"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('" + message + "');", True)

                    Return False

                End If
            Else
                Return True
            End If
           

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim obj As New ToolCCS
        Dim ds As New DataSet()
        Dim objGetData As New StandGetData.Selectdata()
        Dim objUpIns As New StandUpInsData.UpdateInsert
        Dim grpVal As String = ""
        Try
            obj.CasePropRename(txtCaseid.Value, txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), txtCaseDe2.Text.Trim.ToString().Replace("'", "''"), txtCaseDe3.Text.Trim.ToString().Replace("'", "''"), txtApp.Text.Trim.ToString().Replace("'", "''"), "SBAConnectionString")

            If hidGrpId.Value = "0" Then
                grpVal = ""
            Else
                grpVal = hidGrpId.Value
            End If
			  'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Renamed Structure #" + txtCaseid.Value + "", txtCaseid.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", grpVal)
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
			
            ds = GetCaseDetails(txtCaseid.Value)

            hidPropCaseD.Value = txtCaseid.Value.ToString() + ":" + txtCaseDe1.Text.Trim.ToString().Replace("'", "'") + " " + txtCaseDe2.Text.Trim.ToString().Replace("'", "'") + " " + ds.Tables(0).Rows(0).Item("APPLICATION").ToString().Replace("'", "'")
            lnkPropCase.Text = hidPropCaseD.Value.ToString()
            divRename.Style.Add("Display", "none")

            'Set Tooltip
            If hidGNotes.Value = "" Then
                btnNotes.ToolTip = "No Notes Available"
            Else
                btnNotes.ToolTip = hidGNotes.Value
            End If

            If hidFileName.Value = "" Then
                btnSponsorM.ToolTip = "No Sponsor message Available"
            Else
                btnSponsorM.ToolTip = "Sponsor Message"
            End If

            If hidNotes.Value = "" Then
                btnSNotes.ToolTip = "No Notes Available"
            Else
                btnSNotes.ToolTip = hidNotes.Value
            End If

            If hidBFileName.Value = "" Then
                btnSSMessage.ToolTip = "No Sponsor message Available"
            Else
                btnSSMessage.ToolTip = "Sponsor Message"
            End If


            If hidGPNotes.Value = "" Then
                btnPGNotes.ToolTip = "No Notes Available"
            Else
                btnPGNotes.ToolTip = hidGPNotes.Value
            End If

            hidPNotes.Value = ds.Tables(0).Rows(0).Item("CASEDE3").ToString()

            If hidPNotes.Value = "" Then
                btnPNotes.ToolTip = "No Notes Available"
            Else
                btnPNotes.ToolTip = hidPNotes.Value
            End If

            If hidCompNotes.Value = "" Then
                btnCompNotes.ToolTip = "No Notes Available"
            Else
                btnCompNotes.ToolTip = hidCompNotes.Value
            End If

            If hidCompGrpNotes.Value = "" Then
                btnCompGNotes.ToolTip = "No Notes Available"
            Else
                btnCompGNotes.ToolTip = hidCompGrpNotes.Value
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function GetCaseDetails(ByVal CaseId As String) As DataSet
        Dim Dts As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim SBAConnection As String = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
        Try
            StrSql = "SELECT CASEID,CASEDE1,(CASEDE1||' ' ||CASEDE2)CASEDES,CASEDE3,CASEDE2,CASETYPE,SERVERDATE,APPLICATION  "
            StrSql = StrSql + "FROM "
            StrSql = StrSql + "( "
            StrSql = StrSql + "SELECT  CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,'Public' CASETYPE,SERVERDATE,APPLICATION FROM BASECASES "
            StrSql = StrSql + "UNION "
            StrSql = StrSql + "SELECT  CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,'Company' CASETYPE,SERVERDATE,APPLICATION FROM COMPANYCASES "
            StrSql = StrSql + "UNION "
            StrSql = StrSql + "SELECT DISTINCT CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,'Proprietary' CASETYPE,SERVERDATE,APPLICATION FROM PERMISSIONSCASES "
            StrSql = StrSql + ") "
            StrSql = StrSql + "WHERE CASEID =" + CaseId.ToString() + " "
            StrSql = StrSql + "ORDER BY CASEID "

            Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
            Return Dts
        Catch ex As Exception
            Throw New Exception("StandGetData:GetCaseDetails:" + ex.Message.ToString())
            Return Dts
        End Try
    End Function
    
    Protected Sub btRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btRename.Click
        Dim flagVal1, flagVal2 As Boolean
        Dim objGetData As New StandGetData.Selectdata()
        Dim ds As New DataSet
        Dim CaseId As New Integer
		  Dim objUpIns As New StandUpInsData.UpdateInsert
        Try
            If hidPropCase.Value = "0" Or hidPropCase.Value = " " Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Structure');", True)
                lnkPropCase.Text = "Select Structure"
                If hidGrpId.Value = "0" Then
                    lnkAllGrps.Text = "All Structures"
                Else
                    lnkAllGrps.Text = hidGroupReportD.Value.ToString()
                End If
				
				 'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked on Rename Button for Structure But No Structure was Selected.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes

            Else
                If CheckAnyDelGroup(hidGrpId.Value) Then
                    If CheckAnyDelCase(hidPropCase.Value.ToString()) = True Then
                        GetCaseDetails()
                        divRename.Style.Add("Display", "block")

                        'Started Activity Log Changes
                        If hidCaseId2.Value.ToString() = hidPropCase.Value.ToString() Then
                            flagVal1 = False
                        Else
                            flagVal1 = True
                        End If
                        If hidGrpId2.Value.ToString() = hidGrpId.Value.ToString() Then
                            flagVal2 = False
                        Else
                            flagVal2 = True
                        End If
                        hidCaseId2.Value = hidPropCase.Value.ToString()
                        hidGrpId2.Value = hidGrpId.Value
                        Try
                            Dim grpVal As String = ""
                            If hidGrpId.Value = "0" Then
                                grpVal = ""
                            Else
                                grpVal = hidGrpId.Value
                            End If
                            If flagVal2 = True Then
                                objUpIns.InsertLog1(Session("UserId").ToString(), "9", "Selected Proprietary Group #" + grpVal, "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", grpVal)
                            End If
                            If flagVal1 = True Then
                                objUpIns.InsertLog1(Session("UserId").ToString(), "8", "Selected Proprietary Structure #" + Session("SBACaseId").ToString(), Session("SBACaseId").ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", grpVal)

                            End If
                            objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked on Rename Button for Structure: #" + hidPropCase.Value + "", hidPropCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", grpVal)
                        Catch ex As Exception

                        End Try
                        'Ended Activity Log Changes
						
                        lnkPropCase.Text = hidPropCaseD.Value.ToString()
                        If hidGrpId.Value = "0" Then
                            lnkAllGrps.Text = "All Structures"
                        Else
                            lnkAllGrps.Text = hidGroupReportD.Value.ToString()
                        End If

                        If hidGNotes.Value = "" Then
                            btnNotes.ToolTip = "No Notes Available"
                        Else
                            btnNotes.ToolTip = hidGNotes.Value
                        End If

                        If hidFileName.Value = "" Then
                            btnSponsorM.ToolTip = "No Sponsor message Available"
                        Else
                            btnSponsorM.ToolTip = "Sponsor Message"
                        End If

                        If hidNotes.Value = "" Then
                            btnSNotes.ToolTip = "No Notes Available"
                        Else
                            btnSNotes.ToolTip = hidNotes.Value
                        End If

                        If hidBFileName.Value = "" Then
                            btnSSMessage.ToolTip = "No Sponsor message Available"
                        Else
                            btnSSMessage.ToolTip = "Sponsor Message"
                        End If

                        If hidGPNotes.Value = "" Then
                            btnPGNotes.ToolTip = "No Notes Available"
                        Else
                            btnPGNotes.ToolTip = hidGPNotes.Value
                        End If

                        If hidPNotes.Value = "" Then
                            btnPNotes.ToolTip = "No Notes Available"
                        Else
                            btnPNotes.ToolTip = hidPNotes.Value
                        End If
                    Else
                        hidPropCase.Value = "0"
                        lnkPropCase.Text = "Select Structure"
                        hidPropCaseD.Value = "Select Structure"
                        hidGrpId.Value = "0"
                        lnkAllGrps.Text = "All Structures"

                        If hidGNotes.Value = "" Then
                            btnNotes.ToolTip = "No Notes Available"
                        Else
                            btnNotes.ToolTip = hidGNotes.Value
                        End If
                        If hidFileName.Value = "" Then
                            btnSponsorM.ToolTip = "No Sponsor message Available"
                        Else
                            btnSponsorM.ToolTip = "Sponsor Message"
                        End If

                        If hidNotes.Value = "" Then
                            btnSNotes.ToolTip = "No Notes Available"
                        Else
                            btnSNotes.ToolTip = hidNotes.Value
                        End If
                        If hidBFileName.Value = "" Then
                            btnSSMessage.ToolTip = "No Sponsor message Available"
                        Else
                            btnSSMessage.ToolTip = "Sponsor Message"
                        End If

                        hidGPNotes.Value = "No Notes Available"
                        btnPGNotes.ToolTip = hidGPNotes.Value
                        hidPNotes.Value = "No Notes Available"
                        btnPNotes.ToolTip = hidPNotes.Value

                    End If
                Else
                    hidPropCase.Value = "0"
                    lnkPropCase.Text = "Select Structure"
                    hidPropCaseD.Value = "Select Structure"
                    hidGrpId.Value = "0"
                    lnkAllGrps.Text = "All Structures"

                    If hidGNotes.Value = "" Then
                        btnNotes.ToolTip = "No Notes Available"
                    Else
                        btnNotes.ToolTip = hidGNotes.Value
                    End If
                    If hidFileName.Value = "" Then
                        btnSponsorM.ToolTip = "No Sponsor message Available"
                    Else
                        btnSponsorM.ToolTip = "Sponsor Message"
                    End If

                    If hidNotes.Value = "" Then
                        btnSNotes.ToolTip = "No Notes Available"
                    Else
                        btnSNotes.ToolTip = hidNotes.Value
                    End If
                    If hidBFileName.Value = "" Then
                        btnSSMessage.ToolTip = "No Sponsor message Available"
                    Else
                        btnSSMessage.ToolTip = "Sponsor Message"
                    End If

                    hidGPNotes.Value = "No Notes Available"
                    btnPGNotes.ToolTip = hidGPNotes.Value
                    hidPNotes.Value = "No Notes Available"
                    btnPNotes.ToolTip = hidPNotes.Value
                End If

                End If
            If hidCompNotes.Value = "" Then
                btnCompNotes.ToolTip = "No Notes Available"
            Else
                btnCompNotes.ToolTip = hidCompNotes.Value
            End If

            If hidCompGrpNotes.Value = "" Then
                btnCompGNotes.ToolTip = "No Notes Available"
            Else
                btnCompGNotes.ToolTip = hidCompGrpNotes.Value
            End If
            lnkAllBGrps.Text = hidGGroupReportD.Value.ToString()
            lnkApprovedCase.Text = hidApprovedCaseD.Value.ToString()
        Catch ex As Exception

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

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select Structure');", True)
            Else

                txtCaseDe1.Text = ds.Tables(0).Rows(0).Item("CASEDE1").ToString()
                txtCaseDe2.Text = ds.Tables(0).Rows(0).Item("CASEDE2").ToString()
                txtCaseDe3.Text = ds.Tables(0).Rows(0).Item("CASEDE3").ToString()
                txtApp.Text = ds.Tables(0).Rows(0).Item("APPLICATION").ToString()
                'divRename.Style.Add("Display", "block")
            End If

        Catch ex As Exception
            lblError.Text = "Error:GetCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            divRename.Style.Add("Display", "none")
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked on Rename Cancel Button", hidPropCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
            txtCaseDe1.Text = ""
            txtCaseDe2.Text = ""
            txtCaseDe3.Text = ""
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnPNotes_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim objUpIns As New StandUpInsData.UpdateInsert
        Try
            If hidPNotes.Value = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('No Notes Available');", True)
                If hidPropCase.Value.ToString() <> "" Then
                    objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked on Proprietary Notes Button But No Structure was selected.", "" + hidPropCase.Value.ToString() + "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "" + hidGrpId.Value.ToString() + "")
                End If
            Else
                Dim str As String = String.Empty
                str = hidPNotes.Value.ToString()
                If str.Contains("'") Then
                    str = str.Replace("'", "\'")
                End If
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('" + str + "');", True)
                If hidPropCase.Value.ToString() <> "" Then
                    objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked on Proprietary Notes Button for Structure# " + hidPropCase.Value.ToString() + "", "" + hidPropCase.Value.ToString() + "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "" + hidGrpId.Value.ToString() + "")
                End If
            End If
            lnkPropCase.Text = hidPropCaseD.Value.ToString()
            If hidGrpId.Value = "0" Then
                lnkAllGrps.Text = "All Structures"
            Else
                lnkAllGrps.Text = hidGroupReportD.Value.ToString()
            End If


            If hidGNotes.Value = "" Then
                btnNotes.ToolTip = "No Notes Available"
            Else
                btnNotes.ToolTip = hidGNotes.Value
            End If

            If hidFileName.Value = "" Then
                btnSponsorM.ToolTip = "No Sponsor message Available"
            Else
                btnSponsorM.ToolTip = "Sponsor Message"
            End If

            If hidNotes.Value = "" Then
                btnSNotes.ToolTip = "No Notes Available"
            Else
                btnSNotes.ToolTip = hidNotes.Value
            End If

            If hidBFileName.Value = "" Then
                btnSSMessage.ToolTip = "No Sponsor message Available"
            Else
                btnSSMessage.ToolTip = "Sponsor Message"
            End If

            If hidGPNotes.Value = "" Then
                btnPGNotes.ToolTip = "No Notes Available"
            Else
                btnPGNotes.ToolTip = hidGPNotes.Value
            End If

            If hidPNotes.Value = "" Then
                btnPNotes.ToolTip = "No Notes Available"
            Else
                btnPNotes.ToolTip = hidPNotes.Value
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCompCase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCompCase.Click
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            Dim CaseDes As String
            Dim flagVal1, flagVal2 As Boolean

            If hidCompnyCase.Value = "0" Or hidCompnyCase.Value = " " Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select Structure');", True)
                lnkCompCase.Text = "Select Structure"
                lnkCompGrp.Text = hidCGrpDes.Value
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked on Start a Company Structure But No Structure was Selected.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes

                If hidCGrpId.Value = "0" Then
                    lnkCompGrp.Text = "All Structures"
                Else
                    lnkCompGrp.Text = hidCGrpDes.Value.ToString()
                End If

                If hidGNotes.Value = "" Then
                    btnNotes.ToolTip = "No Notes Available"
                Else
                    btnNotes.ToolTip = hidGNotes.Value
                End If

                If hidFileName.Value = "" Then
                    btnSponsorM.ToolTip = "No Sponsor message Available"
                Else
                    btnSponsorM.ToolTip = "Sponsor Message"
                End If

                If hidNotes.Value = "" Then
                    btnSNotes.ToolTip = "No Notes Available"
                Else
                    btnSNotes.ToolTip = hidNotes.Value
                End If

                If hidBFileName.Value = "" Then
                    btnSSMessage.ToolTip = "No Sponsor message Available"
                Else
                    btnSSMessage.ToolTip = "Sponsor Message"
                End If

                If hidGPNotes.Value = "" Then
                    btnPGNotes.ToolTip = "No Notes Available"
                Else
                    btnPGNotes.ToolTip = hidGPNotes.Value
                End If

                If hidPNotes.Value = "" Then
                    btnPNotes.ToolTip = "No Notes Available"
                Else
                    btnPNotes.ToolTip = hidPNotes.Value
                End If


                If hidCompNotes.Value = "" Then
                    btnCompNotes.ToolTip = "No Notes Available"
                Else
                    btnCompNotes.ToolTip = hidCompNotes.Value
                End If

                If hidCompGrpNotes.Value = "" Then
                    btnCompGNotes.ToolTip = "No Notes Available"
                Else
                    btnCompGNotes.ToolTip = hidCompGrpNotes.Value
                End If
            Else
                If CheckAnyDelGroup(hidCGrpId.Value) Then
                    If CheckAnyDelCaseComp(hidCompnyCase.Value.ToString()) = True Then
                        Session("SBACaseId") = hidCompnyCase.Value.ToString()
                        Session("CompLib") = "Y"
                        CaseDes = hidCompnyCaseD.Value.ToString()

                        If Not objRefresh.IsRefresh Then
                            'Started Activity Log Changes
                            If hidCaseId3.Value.ToString() = hidCompnyCase.Value.ToString() Then
                                flagVal1 = False
                            Else
                                flagVal1 = True
                            End If
                            If hidGrpId3.Value.ToString() = hidCGrpId.Value.ToString() Then
                                flagVal2 = False
                            Else
                                flagVal2 = True
                            End If
                            hidCaseId3.Value = hidCompnyCase.Value.ToString()
                            hidGrpId3.Value = hidCGrpId.Value

                            Try
                                Dim grpVal As String = ""
                                If hidCGrpId.Value = "0" Then
                                    grpVal = ""
                                Else
                                    grpVal = hidCGrpId.Value
                                End If
                                If flagVal2 = True Then
                                    If grpVal = "" Then
                                        objUpIns.InsertLog1(Session("UserId").ToString(), "9", "Selected Company Group for All Structures", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", grpVal)
                                    Else
                                        objUpIns.InsertLog1(Session("UserId").ToString(), "9", "Selected Company Group #" + grpVal, "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", grpVal)
                                    End If

                                End If
                                If flagVal1 = True Then
                                    objUpIns.InsertLog1(Session("UserId").ToString(), "26", "Selected Company Structure #" + Session("SBACaseId").ToString(), Session("SBACaseId").ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", grpVal)

                                End If
                                objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked on Start a Company Structure", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", grpVal)
                                objUpIns.InsertLog1(Session("UserId").ToString(), "3", "Opened Strcuture Designer Page for Company Structure # " + Session("SBACaseId") + "", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", grpVal)
                            Catch ex As Exception

                            End Try
                            'Ended Activity Log Changes
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "OpenNewWindow('Assumptions/Extrusion.aspx?Type=Prop');", True)
                        End If
                        lnkCompCase.Text = hidCompnyCaseD.Value.ToString()

                        If hidCGrpId.Value = "0" Then
                            lnkCompGrp.Text = "All Structures"
                        Else
                            lnkCompGrp.Text = hidCGrpDes.Value.ToString()
                        End If

                        If hidGNotes.Value = "" Then
                            btnNotes.ToolTip = "No Notes Available"
                        Else
                            btnNotes.ToolTip = hidGNotes.Value
                        End If

                        If hidFileName.Value = "" Then
                            btnSponsorM.ToolTip = "No Sponsor message Available"
                        Else
                            btnSponsorM.ToolTip = "Sponsor Message"
                        End If

                        If hidNotes.Value = "" Then
                            btnSNotes.ToolTip = "No Notes Available"
                        Else
                            btnSNotes.ToolTip = hidNotes.Value
                        End If

                        If hidBFileName.Value = "" Then
                            btnSSMessage.ToolTip = "No Sponsor message Available"
                        Else
                            btnSSMessage.ToolTip = "Sponsor Message"
                        End If

                        If hidGPNotes.Value = "" Then
                            btnPGNotes.ToolTip = "No Notes Available"
                        Else
                            btnPGNotes.ToolTip = hidGPNotes.Value
                        End If

                        If hidPNotes.Value = "" Then
                            btnPNotes.ToolTip = "No Notes Available"
                        Else
                            btnPNotes.ToolTip = hidPNotes.Value
                        End If


                        If hidCompNotes.Value = "" Then
                            btnCompNotes.ToolTip = "No Notes Available"
                        Else
                            btnCompNotes.ToolTip = hidCompNotes.Value
                        End If

                        If hidCompGrpNotes.Value = "" Then
                            btnCompGNotes.ToolTip = "No Notes Available"
                        Else
                            btnCompGNotes.ToolTip = hidCompGrpNotes.Value
                        End If

                    Else
                        hidCompnyCase.Value = "0"
                        lnkCompCase.Text = "Select Structure"
                        hidCompnyCaseD.Value = "Select Structure"
                        hidCGrpId.Value = "0"
                        hidCGrpDes.Value = "All Structures"
                        lnkCompGrp.Text = "All Structures"

                        If hidCGrpId.Value = "0" Then
                            lnkCompGrp.Text = "All Structures"
                        Else
                            lnkCompGrp.Text = hidCGrpDes.Value.ToString()
                        End If

                        If hidGNotes.Value = "" Then
                            btnNotes.ToolTip = "No Notes Available"
                        Else
                            btnNotes.ToolTip = hidGNotes.Value
                        End If

                        If hidFileName.Value = "" Then
                            btnSponsorM.ToolTip = "No Sponsor message Available"
                        Else
                            btnSponsorM.ToolTip = "Sponsor Message"
                        End If

                        If hidNotes.Value = "" Then
                            btnSNotes.ToolTip = "No Notes Available"
                        Else
                            btnSNotes.ToolTip = hidNotes.Value
                        End If

                        If hidBFileName.Value = "" Then
                            btnSSMessage.ToolTip = "No Sponsor message Available"
                        Else
                            btnSSMessage.ToolTip = "Sponsor Message"
                        End If

                        If hidGPNotes.Value = "" Then
                            btnPGNotes.ToolTip = "No Notes Available"
                        Else
                            btnPGNotes.ToolTip = hidGPNotes.Value
                        End If

                        If hidPNotes.Value = "" Then
                            btnPNotes.ToolTip = "No Notes Available"
                        Else
                            btnPNotes.ToolTip = hidPNotes.Value
                        End If


                        hidCompNotes.Value = "No Notes Available"
                        btnCompNotes.ToolTip = hidCompNotes.Value
                        hidCompGrpNotes.Value = "No Notes Available"
                        btnCompGNotes.ToolTip = hidCompGrpNotes.Value

                    End If
                Else
                    hidCompnyCase.Value = "0"
                    lnkCompCase.Text = "Select Structure"
                    hidCompnyCaseD.Value = "Select Structure"
                    hidCGrpId.Value = "0"
                    hidCGrpDes.Value = "All Structures"
                    lnkCompGrp.Text = "All Structures"

                    If hidCGrpId.Value = "0" Then
                        lnkCompGrp.Text = "All Structures"
                    Else
                        lnkCompGrp.Text = hidCGrpDes.Value.ToString()
                    End If

                    If hidGNotes.Value = "" Then
                        btnNotes.ToolTip = "No Notes Available"
                    Else
                        btnNotes.ToolTip = hidGNotes.Value
                    End If

                    If hidFileName.Value = "" Then
                        btnSponsorM.ToolTip = "No Sponsor message Available"
                    Else
                        btnSponsorM.ToolTip = "Sponsor Message"
                    End If

                    If hidNotes.Value = "" Then
                        btnSNotes.ToolTip = "No Notes Available"
                    Else
                        btnSNotes.ToolTip = hidNotes.Value
                    End If

                    If hidBFileName.Value = "" Then
                        btnSSMessage.ToolTip = "No Sponsor message Available"
                    Else
                        btnSSMessage.ToolTip = "Sponsor Message"
                    End If

                    If hidGPNotes.Value = "" Then
                        btnPGNotes.ToolTip = "No Notes Available"
                    Else
                        btnPGNotes.ToolTip = hidGPNotes.Value
                    End If

                    If hidPNotes.Value = "" Then
                        btnPNotes.ToolTip = "No Notes Available"
                    Else
                        btnPNotes.ToolTip = hidPNotes.Value
                    End If


                    hidCompNotes.Value = "No Notes Available"
                    btnCompNotes.ToolTip = hidCompNotes.Value
                    hidCompGrpNotes.Value = "No Notes Available"
                    btnCompGNotes.ToolTip = hidCompGrpNotes.Value
                End If
            End If

            lnkAllBGrps.Text = hidGGroupReportD.Value.ToString()
            lnkApprovedCase.Text = hidApprovedCaseD.Value.ToString()

            lnkPropCase.Text = hidPropCaseD.Value.ToString()
            lnkAllGrps.Text = hidGroupReportD.Value.ToString()



        Catch ex As Exception
            lblError.Text = "Error:btnPCase_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnCompSRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCompSRename.Click
        Dim flagVal1, flagVal2 As Boolean
        Dim objGetData As New StandGetData.Selectdata()
        Dim ds As New DataSet
        Dim CaseId As New Integer
        Dim objUpIns As New StandUpInsData.UpdateInsert
        Try
            If hidCompnyCase.Value = "0" Or hidCompnyCase.Value = " " Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Structure');", True)
                lnkCompCase.Text = "Select Structure"
                If hidCGrpId.Value = "0" Then
                    lnkCompGrp.Text = "All Structures"
                Else
                    lnkCompGrp.Text = hidCGrpDes.Value.ToString()
                End If
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked on Rename Button for Company Structure But No Structure was Selected.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes

            Else
                If CheckAnyDelGroup(hidGrpId.Value) Then
                    If CheckAnyDelCaseComp(hidCompnyCase.Value.ToString()) = True Then
                        GetCompCaseDetails()
                        divComRename.Style.Add("Display", "block")

                        'Started Activity Log Changes
                        If hidCaseId3.Value.ToString() = hidCompnyCase.Value.ToString() Then
                            flagVal1 = False
                        Else
                            flagVal1 = True
                        End If
                        If hidGrpId3.Value.ToString() = hidCGrpId.Value.ToString() Then
                            flagVal2 = False
                        Else
                            flagVal2 = True
                        End If
                        hidCaseId3.Value = hidCompnyCase.Value.ToString()
                        hidGrpId3.Value = hidCGrpId.Value
                        Try
                            Dim grpVal As String = ""
                            If hidCGrpId.Value = "0" Then
                                grpVal = ""
                            Else
                                grpVal = hidCGrpId.Value
                            End If
                            If flagVal2 = True Then
                                objUpIns.InsertLog1(Session("UserId").ToString(), "9", "Selected Company Group #" + grpVal, "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", grpVal)
                            End If
                            If flagVal1 = True Then
                                objUpIns.InsertLog1(Session("UserId").ToString(), "26", "Selected Company Structure #" + hidCompnyCase.Value, hidCompnyCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", grpVal)

                            End If
                            objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked on Rename Button for Structure: #" + hidCompnyCase.Value + "", hidCompnyCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", grpVal)
                        Catch ex As Exception

                        End Try
                        'Ended Activity Log Changes

                        lnkCompCase.Text = hidCompnyCaseD.Value.ToString()

                        If hidCGrpId.Value = "0" Then
                            lnkCompGrp.Text = "All Structures"
                        Else
                            lnkCompGrp.Text = hidCGrpDes.Value.ToString()
                        End If

                        If hidGNotes.Value = "" Then
                            btnNotes.ToolTip = "No Notes Available"
                        Else
                            btnNotes.ToolTip = hidGNotes.Value
                        End If

                        If hidFileName.Value = "" Then
                            btnSponsorM.ToolTip = "No Sponsor message Available"
                        Else
                            btnSponsorM.ToolTip = "Sponsor Message"
                        End If

                        If hidNotes.Value = "" Then
                            btnSNotes.ToolTip = "No Notes Available"
                        Else
                            btnSNotes.ToolTip = hidNotes.Value
                        End If

                        If hidBFileName.Value = "" Then
                            btnSSMessage.ToolTip = "No Sponsor message Available"
                        Else
                            btnSSMessage.ToolTip = "Sponsor Message"
                        End If

                        If hidGPNotes.Value = "" Then
                            btnPGNotes.ToolTip = "No Notes Available"
                        Else
                            btnPGNotes.ToolTip = hidGPNotes.Value
                        End If

                        If hidPNotes.Value = "" Then
                            btnPNotes.ToolTip = "No Notes Available"
                        Else
                            btnPNotes.ToolTip = hidPNotes.Value
                        End If


                        If hidCompNotes.Value = "" Then
                            btnCompNotes.ToolTip = "No Notes Available"
                        Else
                            btnCompNotes.ToolTip = hidCompNotes.Value
                        End If

                        If hidCompGrpNotes.Value = "" Then
                            btnCompGNotes.ToolTip = "No Notes Available"
                        Else
                            btnCompGNotes.ToolTip = hidCompGrpNotes.Value
                        End If
                    Else
                        hidCompnyCase.Value = "0"
                        lnkCompCase.Text = "Select Structure"
                        hidCompnyCaseD.Value = "Select Structure"
                        hidCGrpId.Value = "0"
                        hidCGrpDes.Value = "All Structures"
                        lnkCompGrp.Text = "All Structures"

                        If hidCGrpId.Value = "0" Then
                            lnkCompGrp.Text = "All Structures"
                        Else
                            lnkCompGrp.Text = hidCGrpDes.Value.ToString()
                        End If

                        If hidGNotes.Value = "" Then
                            btnNotes.ToolTip = "No Notes Available"
                        Else
                            btnNotes.ToolTip = hidGNotes.Value
                        End If

                        If hidFileName.Value = "" Then
                            btnSponsorM.ToolTip = "No Sponsor message Available"
                        Else
                            btnSponsorM.ToolTip = "Sponsor Message"
                        End If

                        If hidNotes.Value = "" Then
                            btnSNotes.ToolTip = "No Notes Available"
                        Else
                            btnSNotes.ToolTip = hidNotes.Value
                        End If

                        If hidBFileName.Value = "" Then
                            btnSSMessage.ToolTip = "No Sponsor message Available"
                        Else
                            btnSSMessage.ToolTip = "Sponsor Message"
                        End If

                        If hidGPNotes.Value = "" Then
                            btnPGNotes.ToolTip = "No Notes Available"
                        Else
                            btnPGNotes.ToolTip = hidGPNotes.Value
                        End If

                        If hidPNotes.Value = "" Then
                            btnPNotes.ToolTip = "No Notes Available"
                        Else
                            btnPNotes.ToolTip = hidPNotes.Value
                        End If


                        hidCompNotes.Value = "No Notes Available"
                        btnCompNotes.ToolTip = hidCompNotes.Value
                        hidCompGrpNotes.Value = "No Notes Available"
                        btnCompGNotes.ToolTip = hidCompGrpNotes.Value

                    End If
                Else
                    hidCompnyCase.Value = "0"
                    lnkCompCase.Text = "Select Structure"
                    hidCompnyCaseD.Value = "Select Structure"
                    hidCGrpId.Value = "0"
                    hidCGrpDes.Value = "All Structures"
                    lnkCompGrp.Text = "All Structures"

                    If hidCGrpId.Value = "0" Then
                        lnkCompGrp.Text = "All Structures"
                    Else
                        lnkCompGrp.Text = hidCGrpDes.Value.ToString()
                    End If

                    If hidGNotes.Value = "" Then
                        btnNotes.ToolTip = "No Notes Available"
                    Else
                        btnNotes.ToolTip = hidGNotes.Value
                    End If

                    If hidFileName.Value = "" Then
                        btnSponsorM.ToolTip = "No Sponsor message Available"
                    Else
                        btnSponsorM.ToolTip = "Sponsor Message"
                    End If

                    If hidNotes.Value = "" Then
                        btnSNotes.ToolTip = "No Notes Available"
                    Else
                        btnSNotes.ToolTip = hidNotes.Value
                    End If

                    If hidBFileName.Value = "" Then
                        btnSSMessage.ToolTip = "No Sponsor message Available"
                    Else
                        btnSSMessage.ToolTip = "Sponsor Message"
                    End If

                    If hidGPNotes.Value = "" Then
                        btnPGNotes.ToolTip = "No Notes Available"
                    Else
                        btnPGNotes.ToolTip = hidGPNotes.Value
                    End If

                    If hidPNotes.Value = "" Then
                        btnPNotes.ToolTip = "No Notes Available"
                    Else
                        btnPNotes.ToolTip = hidPNotes.Value
                    End If


                    hidCompNotes.Value = "No Notes Available"
                    btnCompNotes.ToolTip = hidCompNotes.Value
                    hidCompGrpNotes.Value = "No Notes Available"
                    btnCompGNotes.ToolTip = hidCompGrpNotes.Value
                End If

            End If

            lnkAllBGrps.Text = hidGGroupReportD.Value.ToString()
            lnkApprovedCase.Text = hidApprovedCaseD.Value.ToString()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetCompCaseDetails()
        Dim objGetData As New StandGetData.Selectdata()
        Dim ds As New DataSet
        Dim CaseId As New Integer
        Try
            CaseId = CInt(hidCompnyCase.Value.ToString())
            ds = GetCaseDetails(CaseId.ToString())
            If ds.Tables(0).Rows.Count <= 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select Structure');", True)
            Else

                txtCompDes1.Text = ds.Tables(0).Rows(0).Item("CASEDE1").ToString()
                txtCompDes2.Text = ds.Tables(0).Rows(0).Item("CASEDE2").ToString()
                txtCompDes3.Text = ds.Tables(0).Rows(0).Item("CASEDE3").ToString()
                 txtCompApp.Text = ds.Tables(0).Rows(0).Item("APPLICATION").ToString()
                'divRename.Style.Add("Display", "block")
            End If

        Catch ex As Exception
            lblError.Text = "Error:GetCompCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnCompUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCompUpdate.Click
        Dim obj As New ToolCCS
        Dim ds As New DataSet()
        Dim objGetData As New StandGetData.Selectdata()
        Dim objUpIns As New StandUpInsData.UpdateInsert
        Dim grpVal As String
        Try
            obj.CaseCompRename(txtCaseid.Value, txtCompDes1.Text.Trim.ToString().Replace("'", "''"), txtCompDes2.Text.Trim.ToString().Replace("'", "''"), txtCompDes3.Text.Trim.ToString().Replace("'", "''"), txtCompApp.Text.Trim.ToString(), "SBAConnectionString")

            'Started Activity Log Changes
            If hidCGrpId.Value = "0" Then
                grpVal = ""
            Else
                grpVal = hidCGrpId.Value
            End If

            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Renamed Company Structure #" + txtCaseid.Value + "", txtCaseid.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", grpVal)
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes

            ds = GetCaseDetails(txtCaseid.Value)

            hidCompnyCaseD.Value = txtCaseid.Value.ToString() + ":" + txtCompDes1.Text.Trim.ToString().Replace("'", "'") + " " + txtCompDes2.Text.Trim.ToString().Replace("'", "'") + " " + ds.Tables(0).Rows(0).Item("APPLICATION").ToString().Replace("'", "'")
            lnkCompCase.Text = hidCompnyCaseD.Value.ToString()
            divComRename.Style.Add("Display", "none")

            'Set Tooltip
            If hidGNotes.Value = "" Then
                btnNotes.ToolTip = "No Notes Available"
            Else
                btnNotes.ToolTip = hidGNotes.Value
            End If

            If hidFileName.Value = "" Then
                btnSponsorM.ToolTip = "No Sponsor message Available"
            Else
                btnSponsorM.ToolTip = "Sponsor Message"
            End If

            If hidNotes.Value = "" Then
                btnSNotes.ToolTip = "No Notes Available"
            Else
                btnSNotes.ToolTip = hidNotes.Value
            End If

            If hidBFileName.Value = "" Then
                btnSSMessage.ToolTip = "No Sponsor message Available"
            Else
                btnSSMessage.ToolTip = "Sponsor Message"
            End If

            If hidGPNotes.Value = "" Then
                btnPGNotes.ToolTip = "No Notes Available"
            Else
                btnPGNotes.ToolTip = hidGPNotes.Value
            End If

            If hidPNotes.Value = "" Then
                btnPNotes.ToolTip = "No Notes Available"
            Else
                btnPNotes.ToolTip = hidPNotes.Value
            End If

            hidCompNotes.Value = ds.Tables(0).Rows(0).Item("CASEDE3").ToString()

            If hidCompNotes.Value = "" Then
                btnCompNotes.ToolTip = "No Notes Available"
            Else
                btnCompNotes.ToolTip = hidCompNotes.Value
            End If



            If hidCompGrpNotes.Value = "" Then
                btnCompGNotes.ToolTip = "No Notes Available"
            Else
                btnCompGNotes.ToolTip = hidCompGrpNotes.Value
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCompCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCompCancel.Click
        Try
            divComRename.Style.Add("Display", "none")
            'lnkAllGrps.Text = "All Structures"
            'lnkPropCase.Text = hidPropCaseD.Value
            txtCompDes1.Text = ""
            txtCompDes2.Text = ""
            txtCompDes3.Text = ""
        Catch ex As Exception

        End Try
    End Sub
End Class
