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
Partial Class Pages_StandAssist_Tools_CompanyAdmin
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                GetCompanyCaseDetails()
                hidCaseId1.Value = "0"
                hidCaseId2.Value = "0"

                'Started Activity Log changes
                Dim objUpIns As New StandUpInsData.UpdateInsert
                objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked on Manage Company Structures Button", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                objUpIns.InsertLog1(Session("UserId").ToString(), "27", "Opened Company Structure Tools Page", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                'Ended Activity Log changes
            End If
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetCompanyCaseDetails()
        Dim objGetData As New StandGetData.Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetData.GetCompCaseDetails(Session("USERID").ToString())

            If ds.Tables(0).Rows.Count <= 0 Then

                btnPTransfer.Visible = False
                btnPRename.Visible = False
                btnDelete.Visible = False

            Else

                btnPTransfer.Visible = True
                btnPRename.Visible = True
                btnDelete.Visible = True

            End If
        Catch ex As Exception
            lblError.Text = "Error:GetPCaseDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetBaseSDetails()
        Dim objGetData As New StandGetData.Selectdata()
        Dim ds As New DataSet
        Dim caseId As New Integer
        Try
            caseId = CInt(hidBaseCase.Value)
            ds = objGetData.GetCompCaseData(caseId)
            If ds.Tables(0).Rows.Count > 0 Then
                txtCompDes1.Text = ds.Tables(0).Rows(0).Item("CASEDE1").ToString()
                txtCompDes2.Text = ds.Tables(0).Rows(0).Item("CASEDE2").ToString()
                txtCompDes3.Text = ds.Tables(0).Rows(0).Item("CASEDE3").ToString()
                txtApp.Text = ds.Tables(0).Rows(0).Item("APPLICATION").ToString()
                lnkBaseCase.Text = hidBaseCaseD.Value
            Else
                divModify.Visible = False
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('You can not rename Proprietary Structure. Please select a Company Structure Only.');", True)
                Dim objUpIns As New StandUpInsData.UpdateInsert
                objUpIns.InsertLog1(Session("UserId").ToString(), "27", "Got Error:You can not rename Proprietary Structure. Please select a Company Structure Only.", hidBaseCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")

                hidBaseCase.Value = ""
                hidBaseCaseD.Value = "Select Structure"
                lnkBaseCase.Text = "Select Structure"
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetBaseSDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

#Region "Copy A Case"

    Protected Sub btnCopyPcase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopyPcase.Click
        Dim SCaseId As String = String.Empty
        Dim TCaseId As String = String.Empty
        Dim Flag, flagVal1 As Boolean
        Dim objUpIns As New StandUpInsData.UpdateInsert
        Try
            If hidBaseCase.Value = "" Or hidBaseCaseD.Value = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Structure');", True)
                lnkBaseCase.Text = "Select Structure"
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "27", "Clicked on Copy Structure But No Structure was Selected.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            Else
                SCaseId = hidBaseCase.Value
                TCaseId = CreateCompCase(Session("SBAToolUserName"), Session("SBALicenseId"))
                Flag = CopyCompCase(SCaseId, TCaseId, "SBAConnectionString")

                'Started Activity Log Changes
                If hidCaseId1.Value.ToString() = hidBaseCase.Value.ToString() Then
                    flagVal1 = False
                Else
                    flagVal1 = True
                End If
                hidCaseId1.Value = "0"
                Try
                    If flagVal1 = True Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "26", "Selected Structure #" + hidBaseCase.Value, hidBaseCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    End If
                    objUpIns.InsertLog1(Session("UserId").ToString(), "27", "Clicked on Copy Structure #" + SCaseId.ToString() + " ", SCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    objUpIns.InsertLog1(Session("UserId").ToString(), "27", "Created new Company Structure #" + TCaseId.ToString() + ". Copied Data from Structure: #" + SCaseId.ToString() + " to Company Structure #" + TCaseId.ToString() + "", TCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    objUpIns.InsertLog3(Session("UserId").ToString(), "27", "Copied Data from Structure #" + SCaseId.ToString() + " to Company Structure #" + TCaseId.ToString() + "", SCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, TCaseId.ToString())

                Catch ex As Exception
                End Try
                'Ended Activity Log Changes

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Structure #" + TCaseId.ToString() + " created successfully.\n Structure #" + SCaseId.ToString() + " variables transferred to Structure #" + TCaseId.ToString() + " successfully.');", True)

                GetCompanyCaseDetails()
                hidBaseCase.Value = ""
                hidBaseCaseD.Value = "Select Structure"
                lnkBaseCase.Text = "Select Structure"
            End If
            
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
            If hidTransferCase.Value = "" Or hidTransferCase.Value = "0" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Structure');", True)
                lnkTransferCase.Text = "Select Structure"
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "27", "Clicked on Transfer Company Structure But No Structure was Selected.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            Else
                SCaseId = hidBaseCase.Value
                TCaseId = hidTransferCase.Value
                Flag = CopyCompCase(SCaseId, TCaseId, "SBAConnectionString")
                If Flag Then
                    'GetBCaseDetails()
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Structure #" + SCaseId.ToString() + " variables transferred to Structure #" + TCaseId.ToString() + " successfully.');", True)
                End If
                GetCompanyCaseDetails()
                If Flag Then
                    'Started Activity Log Changes
                    Try
                        hidCaseId2.Value = "0"
                        objUpIns.InsertLog1(Session("UserId").ToString(), "26", "Selected Company Target Structure #" + hidTransferCase.Value, hidTransferCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")

                        objUpIns.InsertLog1(Session("UserId").ToString(), "27", "Clicked on Transfer Structure from #" + SCaseId.ToString() + " to Company Structure #" + TCaseId.ToString() + " ", SCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        objUpIns.InsertLog1(Session("UserId").ToString(), "27", "Transferred Data from Structure #" + SCaseId.ToString() + " to Company Structure #" + TCaseId.ToString() + "", TCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        objUpIns.InsertLog3(Session("UserId").ToString(), "27", "Transferred Material Data from Structure: #" + SCaseId.ToString() + " to Company Structure: #" + TCaseId.ToString() + "", SCaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, TCaseId.ToString())
                    Catch ex As Exception
                    End Try
                    'Ended Activity Log Changes
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Structure #" + SCaseId.ToString() + " variables transferred to Structure #" + TCaseId.ToString() + " successfully.');", True)

                    lnkBaseCase.Text = "Select Structure"
                    lnkTransferCase.Text = "Select Structure"
                    hidBaseCase.Value = "0"
                    hidBaseCaseD.Value = "Select Structure"
                    hidTransferCase.Value = "0"
                    divCopy.Style.Add("Display", "none")
                End If
            End If

           
        Catch ex As Exception
            lblError.Text = "Error:btnPS2T_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            divCopy.Visible = False

            hidBaseCase.Value = ""
            hidBaseCaseD.Value = "Select Structure"
            lnkBaseCase.Text = "Select Structure"
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "27", "Clicked on Company Structure Transfer Cancel Button", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception
            End Try
            'Ended Activity Log Changes
        Catch ex As Exception
            lblError.Text = "Error:btnRenameC_Click:" + ex.Message.ToString() + ""
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
            Dim objUpIns As New StandUpInsData.UpdateInsert
            divModify.Visible = False

            hidBaseCase.Value = ""
            hidBaseCaseD.Value = "Select Structure"
            lnkBaseCase.Text = "Select Structure"

            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "27", "Clicked on Company Structure Rename Cancel Button", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
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
        Dim objGetData As New StandGetData.Selectdata()
        Dim objUpIns As New StandUpInsData.UpdateInsert
        Dim ds As New DataSet
        Try
            If hidBaseCase.Value = "0" Or hidBaseCase.Value = " " Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Structure');", True)
                lnkBaseCase.Text = "Select Structure"

                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "27", "Clicked on Rename Company Structure But No Structure was selected.", CaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception
                End Try
                'Ended Activity Log Changes
            Else
                CaseId = hidBaseCase.Value
                ds = objGetData.GetCompCaseData(CaseId)
                If ds.Tables(0).Rows.Count > 0 Then
                    obj.CaseCompRename(hidBaseCase.Value, txtCompDes1.Text.Trim.ToString().Replace("'", "''"), txtCompDes2.Text.Trim.ToString().Replace("'", "''"), txtCompDes3.Text.Trim.ToString().Replace("'", "''"), txtApp.Text.Trim.ToString().Replace("'", "''"), "SBAConnectionString")
                    'GetBCaseDetails()
                    GetCompanyCaseDetails()
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "27", "Renamed Company Structure #" + CaseId.ToString() + "", CaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Catch ex As Exception
                    End Try
                    'Ended Activity Log Changes
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Company Structure#" + CaseId.ToString() + " updated successfully');", True)
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('You can not Rename Proprietary Structure. Please Select a Company Structure');", True)
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "27", "Cannot Rename Proprietary Structure", CaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Catch ex As Exception
                    End Try
                    'Ended Activity Log Changes
                End If

                divModify.Visible = False
                hidBaseCase.Value = ""
                hidBaseCaseD.Value = "Select Structure"
                lnkBaseCase.Text = "Select Structure"
            End If
            
        Catch ex As Exception
            lblError.Text = "Error:btnRename_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
#End Region

#Region "Delete Case"
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim obj As New ToolCCS
        Dim CaseId As New Integer
        Dim objGetData As New StandGetData.Selectdata()
        Dim objUpIns As New StandUpInsData.UpdateInsert
        Dim ds As New DataSet
        Try
            If hidBaseCase.Value = "" Or hidBaseCase.Value = "0" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Structure');", True)
                lnkBaseCase.Text = "Select Structure"

                'Started Activity Log Changes
                Try
                     objUpIns.InsertLog1(Session("UserId").ToString(), "27", "Clicked on Company Structure Delete Button But No Structure was Selected.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            Else
                CaseId = CInt(hidBaseCase.Value)
                ds = objGetData.GetCompCaseData(CaseId)
                If ds.Tables(0).Rows.Count > 0 Then
                    obj.CaseCompDelete(Session("SBAToolUserName").ToString(), CaseId.ToString(), "SBAConnectionString")
                    obj.CompGroupCaseDelete(CaseId.ToString(), "SBAConnectionString")
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "27", "Clicked on Company Structure Delete Button", CaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        objUpIns.InsertLog1(Session("UserId").ToString(), "27", "Deleted Structure #" + CaseId.ToString() + " ", CaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Catch ex As Exception
                    End Try
                    'Ended Activity Log Changes
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Company Structure#" + CaseId.ToString() + " deleted successfully');", True)
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('You can not delete Proprietary Structure. Please select a Company Structure Only.');", True)
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "27", "Can not delete Proprietary Structure", CaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Catch ex As Exception
                    End Try
                    'Ended Activity Log Changes
                End If
                hidBaseCase.Value = ""
                hidBaseCaseD.Value = "Select Structure"
                lnkBaseCase.Text = "Select Structure"
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

            CaseId = CreateCompCase(Session("SBAToolUserName"), Session("SBALicenseId"))
            'GetBCaseDetails()
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "27", "Clicked on Company Structure Create Button", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                objUpIns.InsertLog1(Session("UserId").ToString(), "27", "Created new Company Structure #" + CaseId.ToString() + " ", CaseId.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception
            End Try
            'Ended Activity Log Changes
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Structure #" + CaseId.ToString() + " created successfully');", True)
            GetCompanyCaseDetails()
        Catch ex As Exception
            lblError.Text = "Error:btnCreate_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub


#End Region

#Region "Tools Function"
    Protected Function CreateCompCase(ByVal UserName As String, ByVal LicId As String) As Integer
        Dim CaseId As Integer
        Dim obj As New ToolCCS
        Try
            CaseId = obj.CreateCompCase(UserName, LicId, "SBAConnectionString", "SBA", 0)
            Return CaseId
        Catch ex As Exception
            lblError.Text = "Error:CreatedCase:" + ex.Message.ToString() + ""
        End Try
    End Function

    Protected Function CopyCompCase(ByVal SCaseID As String, ByVal TCaseId As String, ByVal Schema As String) As Boolean
        Dim obj As New ToolCCS
        Try
            obj.CopyCompAdminCase(SCaseID, TCaseId, Schema)
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

    Protected Sub btnPRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPRename.Click
        Try
            Dim flagVal1 As Boolean
            Dim objUpIns As New StandUpInsData.UpdateInsert
            If hidBaseCase.Value = "" Or hidBaseCase.Value = "0" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Please select a Source Structure');", True)
                hidBaseCase.Value = ""
                hidBaseCaseD.Value = "Select Structure"
                lnkBaseCase.Text = "Select Structure"
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "27", "Clicked on Company Structure Rename Button But No Structure was Selected.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            Else
                divModify.Visible = True

                'Started Activity Log Changes
                If hidCaseId2.Value.ToString() = hidBaseCase.Value.ToString() Then
                    flagVal1 = False
                Else
                    flagVal1 = True
                End If
                hidCaseId2.Value = hidBaseCase.Value.ToString()

                Try
                    If flagVal1 = True Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "26", "Selected Company/Proprietary Structure #" + hidBaseCase.Value, hidBaseCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    End If
                    objUpIns.InsertLog1(Session("UserId").ToString(), "27", "Clicked on Company Structure Rename Button", hidBaseCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception
                End Try
                'Ended Activity Log Changes
                GetBaseSDetails()
            End If

        Catch ex As Exception
            lblError.Text = "Error:btnPRename_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnPTransfer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPTransfer.Click
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            Dim flagVal1 As Boolean
            If hidBaseCase.Value = "" Or hidBaseCase.Value = "0" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select a Source Structure');", True)
                lnkBaseCase.Text = "Select Structure"

                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "27", "Clicked on Company Structure Transfer Button But No Structure was Selected.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes

            Else
                divCopy.Style.Add("Display", "inline")
                lnkBaseCase.Text = hidBaseCaseD.Value.ToString()


                'Started Activity Log Changes
                Try
                    If hidCaseId2.Value.ToString() = hidBaseCase.Value.ToString() Then
                        flagVal1 = False
                    Else
                        flagVal1 = True
                    End If
                    hidCaseId2.Value = hidBaseCase.Value.ToString()
                    If flagVal1 = True Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "26", "Selected Structure #" + hidBaseCase.Value, hidBaseCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")

                    End If
                    objUpIns.InsertLog1(Session("UserId").ToString(), "27", "Clicked on Company Structure Transfer Button", hidBaseCase.Value, Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception
                End Try
                'Ended Activity Log Changes

            End If
        Catch ex As Exception

        End Try
    End Sub

End Class
