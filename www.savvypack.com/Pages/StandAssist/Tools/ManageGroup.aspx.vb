Imports System.Data
Imports System.Data.OleDb
Imports System.HttpStyleUriParser
Imports System.Web.UI.WebControls
Imports StandGetData
Imports StandUpInsData
Partial Class Pages_StandAssist_Tools_ManageGroup
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            txtKey.Attributes.Add("onKeyDown", "return clickButton(event,'" + btnSearch.ClientID + "')")
            If Not IsPostBack Then
                lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " Savvypack Corporation."

                hvUserGrd.Value = "0"
                GetPCaseDetails()
                GetGroupsForCase()
                'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "7", "Opened Public Structure Manage Groups Page", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try

                'Ended Activity Log Changes
            End If
        Catch ex As Exception
            lblError.Text = "Page_Load:" + ex.Message
        End Try
    End Sub
    Protected Sub GetPCaseDetails()
        Dim objGetData As New StandGetData.Selectdata
        Dim ds As New DataSet
        Dim dsGrps As New DataSet
        Dim dtGrps As New DataTable
        Dim j As Integer
        Dim grpCount As Integer = 0
        Try

            ds = objGetData.GetBCaseGrpDetails("BASE", txtKey.Text.Trim())
            dtGrps = ds.Tables(0).DefaultView.ToTable(True, "GROUPID")
            For j = 0 To dtGrps.Rows.Count - 1
                If dtGrps.Rows(j).Item("GROUPID").ToString() <> "0" Then
                    grpCount += 1
                End If
            Next
            lblGroupCnt.Text = grpCount

            If ds.Tables(0).Rows.Count > 0 Then
                lblCF.Text = ds.Tables(0).Rows.Count
                trmsg.Visible = False
            Else
                lblCF.Text = 0
                trmsg.Visible = True
                lblmsg.Text = "No groups created"
            End If
            'If dsGrps.Tables(0).Rows.Count > 0 Then
            '    lblGroupCnt.Text = dsGrps.Tables(0).Rows.Count
            'Else
            '    lblGroupCnt.Text = 0
            'End If
            Session("UsersDataGroup") = ds
            grdCase.DataSource = ds
            grdCase.DataBind()

        Catch ex As Exception
            lblError.Text = "GetPCaseDetails:" + ex.Message
        End Try
    End Sub

    Protected Sub btnCGrp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCGrp.Click
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert

            divGModify.Visible = True
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "7", "Clicked on Create Group Button", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes

        Catch ex As Exception

        End Try
    End Sub

    
    Protected Sub grdCase_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdCase.Sorting
        Try
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hvUserGrd.Value.ToString())
            Dts = Session("UsersDataGroup")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            hvUserGrd.Value = numberDiv.ToString()
            grdCase.DataSource = dv
            grdCase.DataBind()

            GetGroupsForCase()
			 Dim objUpIns As New StandUpInsData.UpdateInsert
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "7", "Sorted List, Sort by: " + e.SortExpression + "", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetGroupsForCase()
        Dim linkBut As New LinkButton
        Dim lblGrpId As New Label
        Dim lblCaseID As New Label
        Try
            For Each Gr As GridViewRow In grdCase.Rows
                lblGrpId = grdCase.Rows(Gr.RowIndex).FindControl("lblGroupID")
                lblCaseID = grdCase.Rows(Gr.RowIndex).FindControl("lblCaseID")
                linkBut = grdCase.Rows(Gr.RowIndex).FindControl("lnkGroId")

                linkBut.Attributes.Add("onclick", "return OpenGroupPopup('../Popup/GroupDetailsFinal.aspx?groupId=" + lblGrpId.Text.Trim() + "&CaseID=" + lblCaseID.Text.Trim() + "&Type=BASE');")
            Next
        Catch ex As Exception
            Response.Write("Error:GetGroupsForCase:" + ex.Message.ToString())
        End Try
    End Sub
    Protected Sub grdCase_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdCase.PageIndexChanging
        Try
            grdCase.PageIndex = e.NewPageIndex
            bindCaseGridSession()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub bindCaseGridSession()
        Dim ds As New DataSet()
        Try
            ds = Session("UsersDataGroup")
            grdCase.DataSource = ds
            grdCase.DataBind()
            GetGroupsForCase()
        Catch ex As Exception
            Response.Write("Error:bindCaseGridSession:" + ex.Message.ToString())
        End Try
    End Sub
    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            GetPCaseDetails()
            GetGroupsForCase()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            GetPCaseDetails()
            GetGroupsForCase()
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "7", "Clicked on Search Group Button, Searched Text: " + txtKey.Text + "", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
			Catch ex As Exception
            Throw New Exception("btnSearch_Click:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnGRename_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGRename.Click
        Dim obj As New ToolCCS
        Dim groupID As String = String.Empty
        Try
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
                    groupID = objUpdateData.AddStructureGroup(txtGDES1.Text, txtGDES2.Text, txtGDES3.Text, txtGAPP.Text, txtSPMessage.Text, hidGSponsorId.Value, Session("UserId"), "BASE")
                    txtGDES1.Text = ""
                    txtGDES2.Text = ""
                    txtGDES3.Text = ""
                    txtGAPP.Text = ""
                    txtSPMessage.Text = ""
                    hidSponsorId.Value = "0"
                    hidGrpId.Value = "0"
                    hidGroupReportD.Value = "Select Group"
                    'lnkBaseGrps.Text = "Select Group"

                    'GetBaseGroups()
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Group #" + groupID + " created successfully');", True)
                    
					 Dim objUpIns As New StandUpInsData.UpdateInsert
                    'Started Activity Log Changes
                    Try
                        objUpIns.InsertLog1(Session("UserId").ToString(), "7", "Created new Public Structure Group, Group #" + groupID + "", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", groupID)
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
					divGModify.Visible = False

                    GetPCaseDetails()
                    GetGroupsForCase()
                End If
            End If

        Catch ex As Exception
            lblError.Text = "Error:btnRename_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnGCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGCancel.Click
        Try
            divGModify.Visible = False
            txtGDES1.Text = ""
            txtGDES2.Text = ""
            txtGDES3.Text = ""
            txtGAPP.Text = ""
            txtSPMessage.Text = ""
			  Dim objUpIns As New StandUpInsData.UpdateInsert
            'Started Activity Log Changes
            Try
                objUpIns.InsertLog1(Session("UserId").ToString(), "7", "Clicked on Cancel Button for Create Group.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception
            lblError.Text = "Error:btnRenameC_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

End Class
