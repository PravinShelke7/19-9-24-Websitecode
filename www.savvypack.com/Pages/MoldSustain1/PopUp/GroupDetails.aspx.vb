Imports System
Imports System.Data
Partial Class Pages_MoldSustain1_PopUp_GroupDetails
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
        Dim objGetData As New MoldS1GetData.Selectdata()
        Try
            If Session("Service") = "COMPS1" Then
                ds = objGetData.GetCompGroupDetails(Session("USERID").ToString(), "Y", Session("CompS1ServiceId").ToString())
            Else
                ds = objGetData.GetGroupDetails(Session("USERID").ToString(), "Y")
            End If

            If ds.Tables(0).Rows.Count = 0 Then
                lblmsg.Text = "No groups created"
                lblmsg.Visible = True
            End If
            Session("UsersData") = ds
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
        Dim ObjUpData As New MoldS1UpInsData.UpdateInsert()
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
End Class
