Imports System
Imports System.Data
Imports M1SubGetData
Imports ToolCCS
Partial Class Pages_Market1Sub_PopUp_GroupDetails
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                GetReportDetails()
                hvReportGrd.Value = "0"
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    


    Protected Sub GetReportDetails()
        Dim ds As New DataSet
        Dim objGetData As New M1SubGetData.Selectdata()
        Try
            ds = objGetData.GetGroupDetails(Session("USERID").ToString(), Session("M1ServiceID"), "Y")
            If ds.Tables(0).Rows.Count = 0 Then
                lblmsg.Text = "No groups created"
                lblmsg.Visible = True
            End If
            Session("UsersData") = ds
            grdGrpReports.DataSource = ds
            grdGrpReports.DataBind()

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetReportDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub grdGrpReports_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdGrpReports.Sorting
        Try
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hvReportGrd.Value.ToString())
            Dts = Session("UsersData")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If

            numberDiv += 1
            hvReportGrd.Value = numberDiv.ToString()
            grdGrpReports.DataSource = dv
            grdGrpReports.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetGroupsForReport()
        Dim linkBut As New LinkButton
        Dim lblGrpId As New Label
        Dim lblReportID As New Label
        For Each Gr As GridViewRow In grdGrpReports.Rows
            lblGrpId = grdGrpReports.Rows(Gr.RowIndex).FindControl("lblGroupID")
            lblReportID = grdGrpReports.Rows(Gr.RowIndex).FindControl("lblReportID")
            linkBut = grdGrpReports.Rows(Gr.RowIndex).FindControl("lnkGroId")
            linkBut.Attributes.Add("onclick", "return OpenGroupPopup('../Popup/GroupDetails.aspx?groupId=" + lblGrpId.Text.Trim() + "&ReportID=" + lblReportID.Text.Trim() + "');")
        Next
    End Sub
    Protected Sub lnkGrpId_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim grpId As String = ""
        Dim ObjUpData As New M1SubUpInsData.UpdateInsert()
        Try
            grpId = grdGrpReports.DataKeys(DirectCast(DirectCast(sender, LinkButton).Parent.Parent, GridViewRow).RowIndex).Value
            If Request.QueryString("groupId") <> Nothing And Request.QueryString("ReportId") <> Nothing Then
                'Code tp Update Report
                ObjUpData.UpdateGroupByReportID(Request.QueryString("groupId"), grpId, Request.QueryString("ReportId"), Session("M1ServiceID"))
            End If
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "GroupSearch();", True)
        Catch ex As Exception

        End Try
    End Sub
End Class
