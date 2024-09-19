Imports System
Imports System.Data
Imports M1SubGetData
Imports M1SubUpInsData
Partial Class Pages_Market1Sub_PopUp_GroupDetailsFinal
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objUpIns As New M1SubUpInsData.UpdateInsert
            hidGroupId.Value = Request.QueryString("groupId").ToString()
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            txtKey.Attributes.Add("onKeyDown", "return clickButton(event,'" + btnSearch.ClientID + "')")
            If Not IsPostBack Then
                GetReportDetails()
                hvREPORTGrd.Value = "0"
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetReportDetails()
        Dim ds As New DataSet
        Dim objGetData As New M1SubGetData.Selectdata()
        Try


            If Request.QueryString("Type").ToString() = "PROP" Then
                ds = objGetData.GetPReportDetailsByUserGrp(Session("UserId"), txtKey.Text, Session("M1ServiceID"))
                If ds.Tables(0).Rows.Count = 0 Then
                    lblmsg.Text = "No Report found"
                    lblmsg.Visible = True
                    btnSubmitReg.Visible = False
                Else
                    lblmsg.Visible = False
                    btnSubmitReg.Visible = True
                End If
                Session("UsersData") = ds
                grdGrpReports.DataSource = ds
                grdGrpReports.DataBind()
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetReportDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub grdGrpReports_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdGrpReports.Sorting
        Try
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hvREPORTGrd.Value.ToString())
            Dts = Session("UsersData")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If

            numberDiv += 1
            hvREPORTGrd.Value = numberDiv.ToString()
            grdGrpReports.DataSource = dv
            grdGrpReports.DataBind()
            
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSubmitReg_Click_ORG(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim grpId As String = ""
        Dim check As New CheckBox
        Dim lblReportID As New Label
        Dim ObjUpData As New M1SubUpInsData.UpdateInsert()
        Try
            For Each Gr As GridViewRow In grdGrpReports.Rows
                check = grdGrpReports.Rows(Gr.RowIndex).FindControl("SelReport")
                lblReportID = grdGrpReports.Rows(Gr.RowIndex).FindControl("lblReport")
                If check.Checked = True Then
                    ObjUpData.EditGroups(hidGroupId.Value.ToString(), lblReportID.Text.ToString(), Session("M1ServiceID"))
                ElseIf check.Checked = False Then
                    ObjUpData.DeleteReportsFrmGrp(hidGroupId.Value.ToString(), lblReportID.Text.ToString(), Session("M1ServiceID"))
                End If
            Next
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "GroupSearch();", True)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnSubmitReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmitReg.Click
        Dim Dts3 As DataSet
        Dim dv As DataView
        Dim grpId As String = ""
        Dim check As New CheckBox
        Dim ReportIds() As String
        Dim lblReportID As New Label
        Dim objGetData As New M1SubGetData.Selectdata()
        Dim ObjUpData As New M1SubUpInsData.UpdateInsert()
        Dts3 = objGetData.GetPReportByGroups(Session("UserId"), "", Request.QueryString("groupId").ToString(), Request.QueryString("Type").ToString())
        Try
            For Each Gr As GridViewRow In grdGrpReports.Rows
                check = grdGrpReports.Rows(Gr.RowIndex).FindControl("SelReport")
                lblReportID = grdGrpReports.Rows(Gr.RowIndex).FindControl("lblReport")
                If check.Checked = True Then
                    ObjUpData.EditGroups(hidGroupId.Value.ToString(), lblReportID.Text.ToString(), Session("M1ServiceID"))
                ElseIf check.Checked = False Then
                    ObjUpData.DeleteReportsFrmGrp(hidGroupId.Value.ToString(), lblReportID.Text.ToString(), Session("M1ServiceID"))
                End If
            Next
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "GroupSearch();", True)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Dim objUpIns As New M1SubUpInsData.UpdateInsert
            GetReportDetails()
        Catch ex As Exception
            Throw New Exception("btnSearch_Click:" + ex.Message)
        End Try
    End Sub

    Protected Sub grdGrpReports_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdGrpReports.RowDataBound
        If Request.QueryString("Type").ToString() = "BASE" Then
            If (e.Row.RowType = DataControlRowType.DataRow) Then

                If (e.Row.RowIndex = 0) Then

                    'e.Row.Style.Add("height", "65px")
                    'e.Row.Style.Add("vertical-align", "bottom")
                    e.Row.Cells(1).Style.Add("padding-top", "35px")
                    e.Row.Cells(2).Style.Add("padding-top", "35px")
                    e.Row.Cells(3).Style.Add("padding-top", "35px")
                    e.Row.Cells(4).Style.Add("padding-top", "35px")
                    e.Row.Cells(5).Style.Add("padding-top", "35px")
                    e.Row.Cells(6).Style.Add("padding-top", "35px")
                    e.Row.Cells(7).Style.Add("padding-top", "35px")
                End If
            End If
        Else
            If (e.Row.RowType = DataControlRowType.DataRow) Then

                If (e.Row.RowIndex = 0) Then
                    'e.Row.Style.Add("height", "60px")
                    'e.Row.Style.Add("vertical-align", "bottom")
                    e.Row.Cells(1).Style.Add("padding-top", "14px")
                    e.Row.Cells(2).Style.Add("padding-top", "14px")
                    e.Row.Cells(3).Style.Add("padding-top", "14px")
                    e.Row.Cells(4).Style.Add("padding-top", "14px")
                End If
            End If
        End If

        Dim Dts3 As New DataSet()
        Dim lblReportID As New Label
        Dim check As New CheckBox
        Dim objGetData As New M1SubGetData.Selectdata()
        Dts3 = objGetData.GetPReportByGroups(Session("UserId"), "", Request.QueryString("groupId").ToString(), Request.QueryString("Type").ToString())
        For Each Gr As GridViewRow In grdGrpReports.Rows
            lblReportID = grdGrpReports.Rows(Gr.RowIndex).FindControl("lblReport")
            check = grdGrpReports.Rows(Gr.RowIndex).FindControl("SelReport")
            For i = 0 To Dts3.Tables(0).Rows.Count - 1
                If lblReportID.Text = Dts3.Tables(0).Rows(i).Item("USERREPORTID").ToString() Then
                    check.Checked = True
                End If
            Next
        Next
    End Sub
End Class
