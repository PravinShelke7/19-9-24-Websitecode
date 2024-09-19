Imports System
Imports System.Data
Partial Class Pages_MedSustain2_PopUp_EditGroup
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            If Not IsPostBack Then
                GetReportDetails()
                hvCaseGrd.Value = "0"
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetReportDetails()
        Dim ds As New DataSet
        Dim objGetData As New SMed2GetData.Selectdata()
        Try
            ds = objGetData.GetGroupDetails(Session("USERID").ToString(), "N")
            If ds.Tables(0).Rows.Count = 0 Then
                lblmsg.Text = "No groups created"
                lblmsg.Visible = True
                btnUpdate.Visible = False
                btnDelete.Visible = False
            Else
                btnUpdate.Visible = True
                btnDelete.Visible = True
            End If
            Session("S2EditUsersData") = ds
            grdGrpCases.DataSource = ds
            grdGrpCases.DataBind()

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
            Dts = Session("S2EditUsersData")
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
        Dim ObjUpData As New SMed2UpInsData.UpdateInsert()
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
        Dim flag As Boolean = True
        Dim objUpdateData As New SMed2UpInsData.UpdateInsert()
        Dim groupName(1000) As String
        Dim groupDes(1000) As String
        Dim groupID(1000) As String
        Dim count As Integer = 0
        Dim grpId As String = ""
        Dim lblGrpId As New Label
        Dim msg As String = ""
        Dim i As Integer = 0
        Dim j As Integer = 0
        Try
            For Each Gr As GridViewRow In grdGrpCases.Rows
                txtGroupName = grdGrpCases.Rows(Gr.RowIndex).FindControl("txtGroupName")
                txtGroupDes = grdGrpCases.Rows(Gr.RowIndex).FindControl("txtGroupDes")
                groupName(count) = txtGroupName.Text.Trim()
                groupDes(count) = txtGroupDes.Text.Trim()
                lblGrpId = grdGrpCases.Rows(Gr.RowIndex).FindControl("lblGroupID")
                groupID(count) = lblGrpId.Text
                count += 1
            Next
            'Check for Duplicate Entry for Group
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
            Else
                objUpdateData.UpdateGroupName(groupName, groupDes, groupID, count)
            End If

        Catch ex As Exception
            flag = False
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
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "GroupSearch();", True)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        Dim chk As New CheckBox
        Dim lblGrpId As New Label
        Dim objUpdateData As New SMed2UpInsData.UpdateInsert()
        Dim flag As Boolean = True
        Try
            For Each Gr As GridViewRow In grdGrpCases.Rows
                chk = grdGrpCases.Rows(Gr.RowIndex).FindControl("delete")
                If chk.Checked = True Then
                    lblGrpId = grdGrpCases.Rows(Gr.RowIndex).FindControl("lblGroupID")
                    objUpdateData.DeleteGroups(lblGrpId.Text)
                End If
            Next
            GetReportDetails()
        Catch ex As Exception
            flag = False
        End Try
        If (flag) Then
            hdnUpdate.Value = "1"
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "_New", " alert('Group(s) deleted Successfully.');", True)
        End If
    End Sub
End Class
