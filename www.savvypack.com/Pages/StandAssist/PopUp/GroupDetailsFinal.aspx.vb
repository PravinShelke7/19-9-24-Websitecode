Imports System
Imports System.Data
Imports StandGetData
Partial Class Pages_StandAssist_PopUp_GroupDetailsFinal
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
				 'Started Activity Log Changes
                Try
                    objUpIns.InsertLog1(Session("UserId").ToString(), "15", "Opened Structure Details PopUp for Adding/Deleting Structure(s) in to Group.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", hidGroupId.Value.ToString())
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
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
                    lblmsg.Text = "No structure found"
                    lblmsg.Visible = True
                Else
                    lblmsg.Visible = False
                End If
                Session("UsersData") = ds
                grdGrpCases.DataSource = ds
                grdGrpCases.DataBind()

            ElseIf Request.QueryString("Type").ToString() = "BASE" Then
                ds = objGetData.GetBCaseDetailsByUserGrp(txtKey.Text)
                If ds.Tables(0).Rows.Count = 0 Then
                    lblmsg.Text = "No structure found"
                    lblmsg.Visible = True
                Else
                    lblmsg.Visible = False
                End If
                Session("UsersData") = ds
                grdGrpCases.DataSource = ds
                grdGrpCases.DataBind()
            ElseIf Request.QueryString("Type").ToString() = "CPROP" Then
                ds = objGetData.GetCompCaseDetailsByUserGrp(txtKey.Text, Session("UserId"))
                If ds.Tables(0).Rows.Count = 0 Then
                    lblmsg.Text = "No structure found"
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
			
			 'Started Activity Log Changes
            Try
                Dim objUpIns As New StandUpInsData.UpdateInsert
                objUpIns.InsertLog1(Session("UserId").ToString(), "15", "Sorted List, Sorted by:" + e.SortExpression, "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", hidGroupId.Value.ToString())
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSubmitReg_Click_ORG(ByVal sender As Object, ByVal e As System.EventArgs)
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
  Protected Sub btnSubmitReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmitReg.Click
        Dim Dts3 As DataSet
        Dim dv As DataView
        Dim grpId As String = ""
        Dim check As New CheckBox
        Dim CaseIds() As String
        Dim lblCaseID As New Label
        Dim objGetData As New StandGetData.Selectdata()
        Dim ObjUpData As New StandUpInsData.UpdateInsert()
        Dts3 = objGetData.GetPCaseByGroups(Session("USERID"), "", Request.QueryString("groupId").ToString(), Request.QueryString("Type").ToString())
        Try
            For Each Gr As GridViewRow In grdGrpCases.Rows
                check = grdGrpCases.Rows(Gr.RowIndex).FindControl("SelCase")
                lblCaseID = grdGrpCases.Rows(Gr.RowIndex).FindControl("lblCase")
                If check.Checked = True Then
                    ObjUpData.EditGroups(hidGroupId.Value.ToString(), lblCaseID.Text.ToString())
                    'Started Activity Log Changes
                    Try
                        Dim flag As Boolean
                        If Dts3.Tables(0).Rows.Count <> 0 Then
                            For i = 0 To Dts3.Tables(0).Rows.Count - 1
                                If Dts3.Tables(0).Rows(i).Item("CASEID").ToString() <> lblCaseID.Text.ToString() Then
                                    flag = False
                                Else
                                    flag = True
                                    Exit For
                                End If
                            Next
                            If flag = False Then
                                ObjUpData.InsertLog1(Session("UserId").ToString(), "15", "Added Structure #" + lblCaseID.Text.ToString() + " to Group #" + hidGroupId.Value + "", lblCaseID.Text.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", hidGroupId.Value.ToString())
                            End If
                        Else
                            ObjUpData.InsertLog1(Session("UserId").ToString(), "15", "Added Structure #" + lblCaseID.Text.ToString() + " to Group #" + hidGroupId.Value + "", lblCaseID.Text.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", hidGroupId.Value.ToString())
                        End If
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                ElseIf check.Checked = False Then
                    ObjUpData.DeleteCasesFrmGrp(hidGroupId.Value.ToString(), lblCaseID.Text.ToString())
                    'Started Activity Log Changes
                    Try
                        If Dts3.Tables(0).Rows.Count <> 0 Then
                            For i = 0 To Dts3.Tables(0).Rows.Count - 1
                                If Dts3.Tables(0).Rows(i).Item("CASEID").ToString() = lblCaseID.Text.ToString() Then
                                    ObjUpData.InsertLog1(Session("UserId").ToString(), "15", "Removed Structure #" + lblCaseID.Text.ToString() + " to Group #" + hidGroupId.Value + "", lblCaseID.Text.ToString(), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", hidGroupId.Value.ToString())
                                End If
                            Next
                        End If
                    Catch ex As Exception

                    End Try
                    'Ended Activity Log Changes
                End If
            Next
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "GroupSearch();", True)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Dim objUpIns As New StandUpInsData.UpdateInsert
            GetReportDetails()
			  'Started Activity Log Changes
            Try

                objUpIns.InsertLog1(Session("UserId").ToString(), "15", "Clicked on Search Button, Searched Text: " + txtKey.Text + "", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", hidGroupId.Value.ToString())
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes

        Catch ex As Exception
            Throw New Exception("btnSearch_Click:" + ex.Message)
        End Try
    End Sub

    Protected Sub grdGrpCases_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdGrpCases.RowDataBound
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
                    e.Row.Cells(1).Style.Add("padding-top", "35px")
                    e.Row.Cells(2).Style.Add("padding-top", "35px")
                    e.Row.Cells(3).Style.Add("padding-top", "35px")
                    e.Row.Cells(4).Style.Add("padding-top", "35px")
                    e.Row.Cells(5).Style.Add("padding-top", "35px")
                    e.Row.Cells(6).Style.Add("padding-top", "35px")
                    e.Row.Cells(7).Style.Add("padding-top", "35px")
                End If
            End If
        End If

        Dim Dts3 As New DataSet()
        Dim lblCaseID As New Label
        Dim check As New CheckBox
        Dim objGetData As New StandGetData.Selectdata()
        Dts3 = objGetData.GetPCaseByGroups(Session("USERID"), "", Request.QueryString("groupId").ToString(), Request.QueryString("Type").ToString())
        For Each Gr As GridViewRow In grdGrpCases.Rows
            lblCaseID = grdGrpCases.Rows(Gr.RowIndex).FindControl("lblCase")
            check = grdGrpCases.Rows(Gr.RowIndex).FindControl("SelCase")
            For i = 0 To Dts3.Tables(0).Rows.Count - 1
                If lblCaseID.Text = Dts3.Tables(0).Rows(i).Item("CASEID").ToString() Then
                    check.Checked = True
                End If
            Next
        Next
    End Sub
End Class
