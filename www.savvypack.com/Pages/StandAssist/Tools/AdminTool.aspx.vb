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
Partial Class Pages_StandAssist_Tools_AdminTool
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                hidGSponsorId.Value = "0"
				'Started Activity Log Changes
                Try
                    Dim objUpIns As New StandUpInsData.UpdateInsert
                    objUpIns.InsertLog1(Session("UserId").ToString(), "5", "Opened Public Structure Tools Page", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            End If
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetCaseDetails()
        Dim objGetData As New StandGetData.Selectdata()
        Dim ds As New DataSet
        Dim CaseId As New Integer
        Try
            CaseId = CInt(hidBaseCase.Value)
            ds = objGetData.GetCaseDetails(CaseId.ToString())
            txtCaseDe1.Text = ds.Tables(0).Rows(0).Item("CASEDE1").ToString()
            txtCaseDe2.Text = ds.Tables(0).Rows(0).Item("CASEDE2").ToString()
            txtCaseDe3.Text = ds.Tables(0).Rows(0).Item("CASEDE3").ToString()
            txtApp.Text = ds.Tables(0).Rows(0).Item("APPLICATION").ToString()
        Catch ex As Exception
            lblError.Text = "Error:GetCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetGroupDetails()
        Dim objGetData As New StandGetData.Selectdata()
        Dim ds As New DataSet
        Dim groupId As New Integer
        Try
            If hidCreate.value = "0" Then
                groupId = CInt(hidGrpId.Value)
                ds = objGetData.GetBGroupCaseDet2(Session("USERID"), "BASE", groupId)
                txtGDES1.Text = ds.Tables(0).Rows(0).Item("DES1").ToString()
                txtGDES2.Text = ds.Tables(0).Rows(0).Item("DES2").ToString()
                txtGDES3.Text = ds.Tables(0).Rows(0).Item("DES3").ToString()
                txtGAPP.Text = ds.Tables(0).Rows(0).Item("APPLICATION").ToString()
                txtSPMessage.Text = ds.Tables(0).Rows(0).Item("FILENAME").ToString()

                hidGSponsorId.Value = ds.Tables(0).Rows(0).Item("SUPPLIERID").ToString()
                If hidGSponsorId.Value = "0" Then
                    lnkGSP.Text = "Select Group"
                Else
                    lnkGSP.Text = ds.Tables(0).Rows(0).Item("NAME").ToString()
                End If

                lblGHeader.Text = "Rename Public Group"
            Else
                txtGDES1.Text = ""
                txtGDES2.Text = ""
                txtGDES3.Text = ""
                txtGAPP.Text = ""
                txtSPMessage.Text = ""
                lnkGSP.Text = "Select Sponsor"
                hidGSponsorId.Value = "0"
                lblGHeader.Text = "Create Public Group"
            End If
          
        Catch ex As Exception
            lblError.Text = "Error:GetGroupDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub GetBaseSDetails()
        Dim objGetData As New StandGetData.Selectdata()
        Dim ds As New DataSet
        Dim caseId As New Integer
        Try
            caseId = CInt(hidBaseCase.Value)
            ds = objGetData.GetBCasesByID(caseId)
            txtCaseDe1.Text = ds.Tables(0).Rows(0).Item("CASEDE1").ToString()
            txtCaseDe2.Text = ds.Tables(0).Rows(0).Item("CASEDE2").ToString()
            txtCaseDe3.Text = ds.Tables(0).Rows(0).Item("CASEDE3").ToString()
            txtApp.Text = ds.Tables(0).Rows(0).Item("APPLICATION").ToString()
            txtStructSM.Text = ds.Tables(0).Rows(0).Item("FILENAME").ToString()
            hidSponsorId.Value = ds.Tables(0).Rows(0).Item("SUPPLIERID").ToString()
            If hidSponsorId.Value = "0" Then
                lnkSponsored.Text = "Select Sponsor"
            Else
                lnkSponsored.Text = ds.Tables(0).Rows(0).Item("NAME").ToString()
            End If

            lnkBaseCase.Text = hidBaseCaseD.Value


        Catch ex As Exception
            lblError.Text = "Error:GetBaseSDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

#Region "Copy A Case"

    Protected Sub btnCopyPcase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopyPcase.Click
        Dim SCaseId As String = String.Empty
        Dim TCaseId As String = String.Empty
        Dim Flag As Boolean
		 Dim objUpIns As New StandUpInsData.UpdateInsert
        Try
            SCaseId = hidBaseCase.Value
            TCaseId = CreateCase()
            Flag = CopyCase(SCaseId, TCaseId, "SBAConnectionString")

            If Flag Then
                'GetBCaseDetails()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Structure #" + TCaseId.ToString() + " created successfully.\nStructure #" + SCaseId.ToString() + " variables transferred to Structure #" + TCaseId.ToString() + " successfully.');", True)
            'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "5", "Clicked on Copy Public Structure #" + SCaseId.ToString() + " ", SCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    objUpIns.InsertLog1(Session("UserId").ToString(), "5", "Created new Public Structure #" + TCaseId.ToString() + " Copy of Public Structure #" + SCaseId.ToString() + "", TCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    objUpIns.InsertLog3(Session("UserId").ToString(), "5", "Copied Data from Public Structure #" + SCaseId.ToString() + " to new Public Structure #" + TCaseId.ToString() + "", SCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, TCaseId.ToString())
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
		   End If
            hidBaseCase.Value = "0"
            hidBaseCaseD.Value = "Select Structure"
            lnkBaseCase.Text = "Select Structure"
        Catch ex As Exception
            lblError.Text = "Error:btnCopyPcase_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
#End Region

#Region "Transfer A Case"
    Protected Sub btnPS2T_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPS2T.Click
        Dim SCaseId As String = String.Empty
        Dim TCaseId As String = String.Empty
        Dim Flag As Boolean
 Dim objUpIns As New StandUpInsData.UpdateInsert
        Try
            SCaseId = hidBaseCase.Value
            TCaseId = hidTransferCase.Value
            Flag = CopyCase(SCaseId, TCaseId, "SBAConnectionString")
            If Flag Then
                'GetBCaseDetails()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Structure #" + SCaseId.ToString() + " variables transferred to Structure #" + TCaseId.ToString() + " successfully.');", True)
            'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "5", "Clicked on Transfer Public Structure ", SCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    objUpIns.InsertLog1(Session("UserId").ToString(), "5", "Transfered Data from Public Structure #" + SCaseId.ToString() + " to Public Structure #" + TCaseId.ToString() + "", TCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    objUpIns.InsertLog3(Session("UserId").ToString(), "5", "Transfered Data from Public Structure #" + SCaseId.ToString() + " to Public Structure #" + TCaseId.ToString() + "", TCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, TCaseId.ToString())
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
		   End If
            hidBaseCase.Value = "0"
            hidBaseCaseD.Value = "Select Structure"
            lnkBaseCase.Text = "Select Structure"
            divCopy.Style.Add("Display", "none")
        Catch ex As Exception
            lblError.Text = "Error:btnPS2T_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
#End Region

#Region "Rename Case"
    'Protected Sub btnPRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPRename.Click
    '    Try
    '        divModify.Visible = True
    '        GetCaseDetails()
    '    Catch ex As Exception
    '        lblError.Text = "Error:btnPRename_Click:" + ex.Message.ToString() + ""
    '    End Try
    'End Sub

    Protected Sub btnRenameC_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRenameC.Click
        Try
            divModify.Visible = False
            lnkBaseCase.Text = hidBaseCaseD.Value.ToString()
			 'Started Activity Log Changes
            Dim objUpIns As New StandUpInsData.UpdateInsert
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "5", "Clicked on Rename Structure Cancel Button", hidBaseCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception
            lblError.Text = "Error:btnRenameC_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRename.Click
        Dim obj As New ToolCCS
        Dim CaseId As String = String.Empty
        Try
            CaseId = hidBaseCase.Value
            obj.CaseRenameBaseStruct(hidBaseCase.Value, txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), txtCaseDe2.Text.Trim.ToString().Replace("'", "''"), txtCaseDe3.Text.Trim.ToString().Replace("'", "''"), txtApp.Text.Trim.ToString().Replace("'", "''"), txtStructSM.text, hidSponsorId.Value, "SBAConnectionString")
           
		     'Started Activity Log Changes
            Dim objUpIns As New StandUpInsData.UpdateInsert
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "5", "Renamed Public Structure #" + hidBaseCase.Value + "", hidBaseCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
			
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Structure#" + CaseId.ToString() + " updated successfully');", True)
            divModify.Visible = False
            hidBaseCase.Value = ""
            hidBaseCaseD.Value = "Select Structure"
            lnkBaseCase.Text = "Select Structure"
        Catch ex As Exception
            lblError.Text = "Error:btnRename_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
#End Region

#Region "Delete Case"
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim obj As New ToolCCS
        Dim CaseId As New Integer
        Dim objUpIns As New StandUpInsData.UpdateInsert
        Try
            CaseId = CInt(hidBaseCase.Value)
            obj.CaseBaseDelete(Session("USERID").ToString(), CaseId.ToString(), "SBAConnectionString")
            objUpIns.DeleteStructureGrps(CaseId.ToString())
            'GetBCaseDetails()
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Structure#" + CaseId.ToString() + " deleted successfully');", True)
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "5", "Deleted Public Structure #" + hidBaseCase.Value + "", hidBaseCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes

		   hidBaseCase.Value = "0"
            hidBaseCaseD.Value = "Select Structure"
            lnkBaseCase.Text = "Select Structure"
        Catch ex As Exception
            lblError.Text = "Error:btnDelete_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
#End Region

#Region "Create Case"
    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Try
		Dim objUpIns As New StandUpInsData.UpdateInsert
            Dim CaseId As New Integer
            CaseId = CreateCase()
            'GetBCaseDetails()
            If hidBaseCaseD.Value = "" Then
                lnkBaseCase.Text = "Select Structure"
            Else
                lnkBaseCase.Text = hidBaseCaseD.Value
            End If
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "5", "Clicked on Public Structure Create Button", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")

                objUpIns.InsertLog1(Session("UserId").ToString(), "5", "Created new Public Structure #" + CaseId.ToString() + "", CaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Structure #" + CaseId.ToString() + " created successfully');", True)

        Catch ex As Exception
            lblError.Text = "Error:btnCreate_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub


#End Region

#Region "Tools Function"
    Protected Function CreateCase() As Integer
        Dim CaseId As Integer
        Dim obj As New ToolCCS
        Try
            CaseId = obj.CreateBaseCase("SBAConnectionString", "SBA", 0)
            Return CaseId
        Catch ex As Exception
            lblError.Text = "Error:CreatedCase:" + ex.Message.ToString() + ""
        End Try
    End Function

    Protected Function CopyCase(ByVal SCaseID As String, ByVal TCaseId As String, ByVal Schema As String) As Boolean
        Dim obj As New ToolCCS
        Try
            obj.CopyBaseCase(SCaseID, TCaseId, Schema)
            obj.CopyBlendMat(SCaseID, TCaseId, Schema)
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

    Protected Sub btnGRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGRename.Click
        Dim obj As New ToolCCS
        Dim groupID As String = String.Empty
        Try
            If hidCreate.Value = "0" Then
                groupID = hidGrpId.Value
                obj.GroupRenameBaseStruct(hidGrpId.Value, txtGDES1.Text.Trim.ToString().Replace("'", "''"), txtGDES2.Text.Trim.ToString().Replace("'", "''"), txtGDES3.Text.Trim.ToString().Replace("'", "''"), txtGAPP.Text.Trim.ToString().Replace("'", "''"), txtSPMessage.Text, hidGSponsorId.Value, "SBAConnectionString")
                'GetBaseGroups()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Group#" + groupID.ToString() + " updated successfully');", True)
                divGModify.Visible = False
                hidGrpId.Value = "0"
                hidGroupReportD.Value = "Select Group"
                lnkBaseGrps.Text = "Select Group"
            Else
                Dim ID As String = 1
                Dim Name As String = ""
                Name = Trim(txtGDES1.Text)
                Dim objUpdateData As New StandUpInsData.UpdateInsert
                Dim objGetData As New StandGetData.Selectdata()
                Dim dt As New DataSet()
                Dim dsGrps As New DataSet()

                If Name.Length <> 0 Then
                    dsGrps = objGetData.GetGroupIDCheck(txtGDES1.Text, txtGDES2.Text, Session("UserId").ToString(), "BASE")
                    If dsGrps.Tables(0).Rows.Count > 0 Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Group already exist');", True)
                    Else
                        objUpdateData.AddStructureGroup(txtGDES1.Text, txtGDES2.Text, txtGDES3.Text, txtGAPP.Text, txtSPMessage.Text, hidGSponsorId.Value, Session("UserId"), "BASE")
                        txtGDES1.Text = ""
                        txtGDES2.Text = ""
                        txtGDES3.Text = ""
                        txtApp.Text = ""
                        txtSPMessage.Text = ""
                        hidSponsorId.Value = "0"
                        hidGrpId.Value = "0"
                        hidGroupReportD.Value = "Select Group"
                        lnkBaseGrps.Text = "Select Group"

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

    Protected Sub btnRenameGroup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRenameGroup.Click
        Try
            hidCreate.Value = "0"
            If hidGrpId.Value = "" Or hidGrpId.Value = "0" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Select Group');", True)
            Else
                divGModify.Visible = True
                lnkBaseGrps.Text = hidGroupReportD.Value
                GetGroupDetails()
            End If
            
        Catch ex As Exception
            lblError.Text = "Error:btnPRename_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnCreateGrp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateGrp.Click
        Try
            hidCreate.value = "1"
            divGModify.Visible = True

            GetGroupDetails()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnGCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGCancel.Click
        Try
            divGModify.Visible = False
        Catch ex As Exception
            lblError.Text = "Error:btnRenameC_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnPRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPRename.Click
        Try
		 Dim objUpIns As New StandUpInsData.UpdateInsert
            If hidBaseCase.Value = "" Or hidBaseCase.Value = "0" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Please select Source Structure');", True)
                If hidBaseCaseD.Value = "" Then
                    lnkBaseCase.Text = "Select Structure"
                Else
                    lnkBaseCase.Text = hidBaseCaseD.Value
                End If
   'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "5", "Clicked on Public Structure Rename Button But No Structure was Selected.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
                ' hidBaseCaseD.Value = "Select Structure"
            Else
                divModify.Visible = True
                GetBaseSDetails()
				'Started Activity Log Changes

                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "5", "Clicked on Rename Public Structure Button", hidBaseCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            End If

        Catch ex As Exception
            lblError.Text = "Error:btnPRename_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
	
	 Protected Sub btnPTransfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPTransfer.Click
        Try
            If hidBaseCase.Value = "" Or hidBaseCase.Value = "0" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Source Structure');", True)
                lnkBaseCase.Text = "Select Structure"
				 'Started Activity Log Changes
                Dim objUpIns As New StandUpInsData.UpdateInsert
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "5", "Clicked on Public Structure Transfer Button But No Structure was selected", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            Else
                divCopy.Style.Add("Display", "inline")
                lnkBaseCase.Text = hidBaseCaseD.Value.ToString()

                'Started Activity Log Changes
                Dim objUpIns As New StandUpInsData.UpdateInsert
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "5", "Clicked on Public Structure Transfer Button", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes

            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            
            divCopy.Style.Add("Display", "none")
            'Started Activity Log Changes
            Dim objUpIns As New StandUpInsData.UpdateInsert
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "5", "Clicked on Cancel Transfer Button", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes


        Catch ex As Exception

        End Try
    End Sub
End Class
