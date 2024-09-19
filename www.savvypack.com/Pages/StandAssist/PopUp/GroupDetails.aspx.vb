Imports System
Imports System.Data
Imports StandGetData
Partial Class Pages_StandAssist_PopUp_GroupDetails
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            hidGroupId.Value = Request.QueryString("groupId").ToString()
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            txtKey.Attributes.Add("onKeyDown", "return clickButton(event,'" + btnSearch.ClientID + "')")
            If Not IsPostBack Then
                GetReportDetails()
                hvCaseGrd.Value = "0"
                objUpIns.InsertLog1(Session("UserId").ToString(), "GroupDetails.aspx", Page.Title, "Request Page ", "null", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "")
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
                ds = objGetData.GetPCaseDetailsByUserGrp(Session("USERID"), txtKey.Text)
                If ds.Tables(0).Rows.Count = 0 Then
                    lblmsg.Text = "No groups created"
                    lblmsg.Visible = True
                End If
                Session("UsersData") = ds
                grdGrpCases.DataSource = ds
                grdGrpCases.DataBind()

            ElseIf Request.QueryString("Type").ToString() = "BASE" Then
                ds = objGetData.GetBCaseDetailsByUserGrp(txtKey.Text)
                If ds.Tables(0).Rows.Count = 0 Then
                    lblmsg.Text = "No groups created"
                    lblmsg.Visible = True
                End If
                Session("UsersData") = ds
                grdGrpCases.DataSource = ds
                grdGrpCases.DataBind()

            End If

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
            Dts = Session("UsersData")
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
    'Protected Sub GetGroupsForCase()
    '    Dim linkBut As New LinkButton
    '    Dim lblGrpId As New Label
    '    Dim lblCaseID As New Label
    '    For Each Gr As GridViewRow In grdGrpCases.Rows
    '        lblGrpId = grdGrpCases.Rows(Gr.RowIndex).FindControl("lblGroupID")
    '        lblCaseID = grdGrpCases.Rows(Gr.RowIndex).FindControl("lblCaseID")
    '        linkBut = grdGrpCases.Rows(Gr.RowIndex).FindControl("lnkGroId")
    '        linkBut.Attributes.Add("onclick", "return OpenGroupPopup('../Popup/GroupDetails.aspx?groupId=" + lblGrpId.Text.Trim() + "&CaseID=" + lblCaseID.Text.Trim() + "');")
    '    Next
    'End Sub
    'Protected Sub lnkGrpId_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim grpId As String = ""
    '    Dim ObjUpData As New StandUpInsData.UpdateInsert()
    '    Try
    '        grpId = grdGrpCases.DataKeys(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridViewRow).RowIndex).Value
    '        If Request.QueryString("groupId") <> Nothing And Request.QueryString("CaseId") <> Nothing Then
    '            'Code tp Update Case
    '            ObjUpData.UpdateGroupByCaseID(Request.QueryString("groupId"), grpId, Request.QueryString("CaseId"))
    '            ObjUpData.InsertLog1(Session("UserId").ToString(), Session("UserName").ToString(), "GroupDetails.aspx", Page.Title, "Assigned Group to CaseId = " + Request.QueryString("CaseId") + " ", Request.QueryString("CaseId").ToString(), "", Session("LogInCount").ToString(), Session.SessionID, "", "", "")
    '        End If
    '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "GroupSearch();", True)
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Protected Sub btnSubmitReg_Click(sender As Object, e As System.EventArgs) Handles btnSubmitReg.Click
        Dim grpId As String = ""
        Dim check As New CheckBox
        Dim lblCaseID As New Label
        Dim ObjUpData As New StandUpInsData.UpdateInsert()
        Try
            For Each Gr As GridViewRow In grdGrpCases.Rows
                check = grdGrpCases.Rows(Gr.RowIndex).FindControl("SelCase")
                lblCaseID = grdGrpCases.Rows(Gr.RowIndex).FindControl("lblCase")
                If check.Checked = True Then
                    ObjUpData.EditGroups(hidGroupId.Value.ToString(), lblCaseID.Text.ToString())
                ElseIf check.Checked = False Then
                    ObjUpData.DeleteCasesFrmGrp(hidGroupId.Value.ToString(), lblCaseID.Text.ToString())
                End If
            Next
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "GroupSearch();", True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdGrpCases_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdGrpCases.RowDataBound
        Dim Dts3 As New DataSet()
        Dim lblCaseID As New Label
        Dim check As New CheckBox
        Dim objGetData As New StandGetData.Selectdata()
        Dts3 = objGetData.GetPCaseByGroups(Session("USERID"), "", hidGroupId.Value.ToString(), Request.QueryString("Type").ToString())
        For Each Gr As GridViewRow In grdGrpCases.Rows
            lblCaseID = grdGrpCases.Rows(Gr.RowIndex).FindControl("lblCase")
            check = grdGrpCases.Rows(Gr.RowIndex).FindControl("SelCase")
            For i = 0 To Dts3.Tables(0).Rows.Count - 1
                If lblCaseID.Text = Dts3.Tables(0).Rows(i).Item("CASEID").ToString() Then
                    check.Checked = True
                End If
            Next
        Next
        If Request.QueryString("Type").ToString() = "BASE" Then
            If (e.Row.RowType = DataControlRowType.DataRow) Then

                If (e.Row.RowIndex = 0) Then

                    e.Row.Style.Add("height", "100px")
                    e.Row.Style.Add("vertical-align", "bottom")
                End If
            End If
        Else
            If (e.Row.RowType = DataControlRowType.DataRow) Then

                If (e.Row.RowIndex = 0) Then
                    e.Row.Style.Add("height", "70px")
                    e.Row.Style.Add("vertical-align", "bottom")
                End If
            End If
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            GetReportDetails()

           Catch ex As Exception
            Throw New Exception("btnSearch_Click:" + ex.Message)
        End Try
    End Sub
End Class
