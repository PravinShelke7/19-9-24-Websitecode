Imports System
Imports System.Data
Imports StandGetData
Imports StandUpInsData
Partial Class Pages_StandAssist_PopUp_EditGroup
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            If Not IsPostBack Then
                GetReportDetails()
                hvCaseGrd.Value = "0"
                 'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "16", "Opened Edit Group PopUp", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
				End If
            If Request.QueryString("Type").ToString() = "PROP" Or Request.QueryString("Type").ToString() = "CPROP" Then
                PageSection1.Attributes.Add("width", "850px")
            Else
                PageSection1.Attributes.Add("width", "1250px")
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetReportDetails()
        Dim ds As New DataSet
        Dim objGetData As New StandGetData.Selectdata()
        Try
            If Request.QueryString("Type").ToString() = "PROP" Then

                ds = objGetData.GetGroupDetails(Session("USERID").ToString(), "N", Request.QueryString("Type").ToString())
                If ds.Tables(0).Rows.Count = 0 Then
                    lblmsg.Text = "No Groups created"
                    lblmsg.Visible = True
                    btnUpdate.Visible = False
                    btnDelete.Visible = False
                Else
                    btnUpdate.Visible = True
                    btnDelete.Visible = True
                End If
                Session("UsersEditData") = ds
                grdGrpCases.DataSource = ds
                grdGrpCases.DataBind()
            ElseIf Request.QueryString("Type").ToString() = "CPROP" Then

                ds = objGetData.GetCompGroupDetails(Session("USERID").ToString(), "N", Request.QueryString("Type").ToString())
                If ds.Tables(0).Rows.Count = 0 Then
                    lblmsg.Text = "No groups created"
                    lblmsg.Visible = True
                    btnUpdate.Visible = False
                    btnDelete.Visible = False
                Else
                    btnUpdate.Visible = True
                    btnDelete.Visible = True
                End If
                Session("UsersEditData") = ds
                grdGrpCases.DataSource = ds
                grdGrpCases.DataBind()
            Else
                ds = objGetData.GetBGroupDetails(Session("USERID").ToString(), "N", Request.QueryString("Type").ToString())
                If ds.Tables(0).Rows.Count = 0 Then
                    lblmsg.Text = "No Groups created"
                    lblmsg.Visible = True
                    btnUpdate.Visible = False
                    btnDelete.Visible = False
                Else
                    btnUpdate.Visible = True
                    btnDelete.Visible = True
                End If
                Session("UsersEditData") = ds
                grdGrpCases.DataSource = ds
                grdGrpCases.Columns(6).Visible = True
                grdGrpCases.Columns(7).Visible = True
                grdGrpCases.DataBind()
            End If
            GetGroupsForSupplier()
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetReportDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub grdGrpCases_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdGrpCases.Sorting
        Try
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hvCaseGrd.Value.ToString())
            Dts = Session("UsersEditData")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If

            numberDiv += 1
            hvCaseGrd.Value = numberDiv.ToString()
            grdGrpCases.DataSource = dv
            grdGrpCases.DataBind()
            GetGroupsForSupplier()
			
			  'Started Activity Log Changes
            Try
                Dim objUpIns As New StandUpInsData.UpdateInsert
                objUpIns.InsertLog1(Session("UserId").ToString(), "16", "List Sorted, Sorted by: " + e.SortExpression + "", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
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
        For Each Gr As GridViewRow In grdGrpCases.Rows
            lblGrpId = grdGrpCases.Rows(Gr.RowIndex).FindControl("lblGroupID")
            lblCaseID = grdGrpCases.Rows(Gr.RowIndex).FindControl("lblCaseID")
            linkBut = grdGrpCases.Rows(Gr.RowIndex).FindControl("lnkGroId")
            linkBut.Attributes.Add("onclick", "return OpenGroupPopup('../Popup/GroupDetails.aspx?groupId=" + lblGrpId.Text.Trim() + "&CaseID=" + lblCaseID.Text.Trim() + "');")
        Next
    End Sub
    Protected Sub lnkGrpId_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim grpId As String = ""
        Dim ObjUpData As New StandUpInsData.UpdateInsert()
        Try
            grpId = grdGrpCases.DataKeys(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridViewRow).RowIndex).Value
            If Request.QueryString("groupId") <> Nothing And Request.QueryString("CaseId") <> Nothing Then
                'Code tp Update Case
                ObjUpData.UpdateGroupByCaseID(Request.QueryString("groupId"), grpId, Request.QueryString("CaseId"))
            End If
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "GroupSearch();", True)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Dim txtGroupName As New TextBox
        Dim txtGroupDes As New TextBox
        Dim txtTechDes As New TextBox
        Dim txtApplication As New TextBox
        Dim txtFilename As New TextBox
        Dim lnkSponsor As New LinkButton
        Dim flag As Boolean = True
        Dim objUpdateData As New StandUpInsData.UpdateInsert()
        Dim groupName(1000) As String
        Dim groupDes(1000) As String
        Dim groupTechDes(1000) As String
        Dim groupApp(1000) As String
        Dim groupID(1000) As String
        Dim Filename(1000) As String
        Dim sponsorID(1000) As String
        Dim count As Integer = 0
        Dim grpId As String = ""
        Dim lblGrpId As New Label
        Dim cnt As Integer = 0
        Dim dsGrps As New DataSet()
        Dim j As Integer = 0
        Dim msg As String = ""
        Try


            'dsGrps = objGetData.GetGroupIDCheckEdit(txtGroupName.Text, Session("UserId").ToString(), Request.QueryString("Type").ToString())
            
                For Each Gr As GridViewRow In grdGrpCases.Rows
                txtGroupName = grdGrpCases.Rows(Gr.RowIndex).FindControl("txtGroupName")
                txtGroupDes = grdGrpCases.Rows(Gr.RowIndex).FindControl("txtGroupDes")
                txtTechDes = grdGrpCases.Rows(Gr.RowIndex).FindControl("txtTechDes")
                txtApplication = grdGrpCases.Rows(Gr.RowIndex).FindControl("txtApplication")
                txtFilename = grdGrpCases.Rows(Gr.RowIndex).FindControl("txtFilename")
                groupName(count) = txtGroupName.Text.Trim()
                groupDes(count) = txtGroupDes.Text.Trim()
                groupTechDes(count) = txtTechDes.Text.Trim()
                groupApp(count) = txtApplication.Text.Trim()
                Filename(count) = txtFilename.Text.Trim()
                lblGrpId = grdGrpCases.Rows(Gr.RowIndex).FindControl("lblGroupID")
                groupID(count) = lblGrpId.Text
                cnt = cnt + 1
                'sponsorID(count) = Request.Form("hidMatid" + cnt.ToString() + "")
                If Request.QueryString("Type").ToString() = "PROP" Then
                    sponsorID(count) = "0"
                Else
                    sponsorID(count) = Request.Form("hidMatid" + cnt.ToString() + "")
                End If
                    count += 1

                Next
                Dim Jval As String = ""
                For i = 0 To count - 1
                    Dim countVal As Integer = 0
                    If Jval.Contains(i) Then
                        Exit For
                    Else
                        For j = 0 To count - 1
                            If groupName(i).ToString() = groupName(j).ToString() And groupDes(i).ToString() = groupDes(j).ToString() Then
                                countVal += 1
                                If countVal = 2 Then
                                    flag = False
                                    Jval = Jval + "," + j.ToString()
                                    msg = msg + "Group Name:" + groupName(i).ToString() + "  Group Description:" + groupDes(i).ToString() + "\n"
                                    Exit For

                                End If

                            End If
                        Next
                    End If
                Next
                If flag = False Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "_New", " alert('These Group Name already exist:\n" + msg + "');", True)
					'Started Activity Log Changes
                Try
                    objUpdateData.InsertLog1(Session("UserId").ToString(), "16", "Group name already exists. ", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
                Else
                objUpdateData.UpdateGroupName(groupName, groupDes, groupTechDes, groupApp, Filename, sponsorID, groupID, count)
                
				'Started Activity Log Changes
                Try
                    objUpdateData.InsertLog1(Session("UserId").ToString(), "16", "Updated Group(s). ", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
				
				End If
        Catch ex As Exception

            Throw New Exception("btnUpdate_Click:" + ex.Message)
        End Try
        If (flag) Then
            hdnUpdate.Value = "1"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "_New", " alert('Update was Successful.');", True)
            GetReportDetails()
        End If
    End Sub

    Protected Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Try
		 'Started Activity Log Changes
            Try
                Dim objUpdateData As New StandUpInsData.UpdateInsert()
                objUpdateData.InsertLog1(Session("UserId").ToString(), "16", "Clicked on Edit Group close button.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "GroupSearch();", True)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        Dim chk As New CheckBox
        Dim lblGrpId As New Label
        Dim objUpdateData As New StandUpInsData.UpdateInsert()
        Dim flag As Boolean = True
        Dim strDelG As String = ""
        Try
            For Each Gr As GridViewRow In grdGrpCases.Rows
                chk = grdGrpCases.Rows(Gr.RowIndex).FindControl("delete")
                If chk.Checked = True Then
                    lblGrpId = grdGrpCases.Rows(Gr.RowIndex).FindControl("lblGroupID")
                    If strDelG = "" Then
                        strDelG = lblGrpId.Text
                    Else
                        strDelG = strDelG + "," + lblGrpId.Text
                    End If

                    objUpdateData.DeleteGroups(lblGrpId.Text)
                End If
            Next
             'Started Activity Log Changes
                    Try
                        objUpdateData.InsertLog1(Session("UserId").ToString(), "16", "Group(s) #" + strDelG + " Deleted successfully. ", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", lblGrpId.Text)
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
			GetReportDetails()
            GetGroupsForSupplier()
        Catch ex As Exception
            flag = False
        End Try
        If (flag) Then
            hdnUpdate.Value = "1"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "_New", " alert('Group(s) " + strDelG + " deleted successfully.');", True)
        End If
    End Sub

    Protected Sub GetGroupsForSupplier()
        Dim lnkName As New LinkButton
        Dim lblGroupID As New Label
        Dim trInner As New TableRow
        Dim tdHeader As New TableCell
        Dim hid As HiddenField
        Dim cnt As Integer = 0
        For Each Gr As GridViewRow In grdGrpCases.Rows
            cnt = cnt + 1
            lblGroupID = grdGrpCases.Rows(Gr.RowIndex).FindControl("lblGroupID")
            lnkName = grdGrpCases.Rows(Gr.RowIndex).FindControl("lnkName")

            hid = New HiddenField
            hid.ID = "hidMatid" + cnt.ToString()

            If lblGroupID.Text = 0 Then
                lnkName.Enabled = False
            Else
                lnkName.Attributes.Add("onclick", "return showPopUp('../PopUp/SponsorDetails.aspx?Des=" + lnkName.ClientID + "&Id=" + hid.ID + "');")
                lnkName.Enabled = True
            End If
            tdHeader.Controls.Add(hid)

        Next
        trInner.Controls.Add(tdHeader)
        tbl111.Controls.Add(trInner)
    End Sub
End Class
