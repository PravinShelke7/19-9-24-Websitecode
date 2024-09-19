Imports System.Data
Imports System.Data.OleDb
Imports System.HttpStyleUriParser
Imports System.Web.UI.WebControls
Imports StandGetData
Imports StandUpInsData
Partial Class Pages_StandAssist_Tools_ManageGroups
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
                hidType.Value = Request.QueryString("Type").ToString()
                'Started Activity Log Changes
                Try
                    If Request.QueryString("Type").ToString() = "PROP" Then
                        objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked on Manage Proprietary Groups Button", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        objUpIns.InsertLog1(Session("UserId").ToString(), "6", "Opened Proprietary Manage Groups Page", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    Else
                        objUpIns.InsertLog1(Session("UserId").ToString(), "1", "Clicked on Manage Company Groups Button", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                        objUpIns.InsertLog1(Session("UserId").ToString(), "6", "Opened Manage Company Groups Page", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                    End If
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
            If Request.QueryString("Type").ToString() = "PROP" Then
                ds = objGetData.GetPManageCaseGrpDetails(Session("USERID").ToString(), txtKey.Text.Trim(), "PROP")
            Else
                ds = objGetData.GetCompanyManageCaseGrpDetails(Session("USERID").ToString(), txtKey.Text.Trim(), "CPROP")
            End If
             
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

    Protected Sub btnGCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGCreate.Click
        Try
            Dim ID As String = 1
            Dim Name As String = ""
            Name = Trim(txtGDES1.Text)
            Dim objUpdateData As New StandUpInsData.UpdateInsert
            Dim objGetData As New StandGetData.Selectdata()
            Dim dt As New DataSet()
            Dim dsGrps As New DataSet()
            Dim GROUPID As String = ""
            If Name.Length <> 0 Then
                If Request.QueryString("Type").ToString() = "PROP" Then
                    dsGrps = objGetData.GetGroupIDCheck(txtGDES1.Text, txtGDES2.Text, Session("UserId").ToString(), "PROP")
                    If dsGrps.Tables(0).Rows.Count > 0 Then
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Group already exist');", True)
                    Else

                        GROUPID = objUpdateData.AddGroupName(txtGDES1.Text, txtGDES2.Text, txtGDES3.Text, txtGAPP.Text, Session("UserId"), "PROP", Session("SBALicenseId"))
                        'Started Activity Log Changes
                        Try
                            objUpdateData.InsertLog1(Session("UserId").ToString(), "6", " Proprietary Group #" + GROUPID + " Created succesfully. ", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", GROUPID)
                        Catch ex As Exception

                        End Try
                        'Ended Activity Log Changes
                        txtGDES1.Text = ""
                        txtGDES2.Text = ""
                        txtGDES3.Text = ""
                        txtGAPP.Text = ""
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Group #" + GROUPID + " created successfully');", True)
                        'divGModify.Style.Add("Display", "none")
                        divGModify.Visible = False
                        GetPCaseDetails()
                        GetGroupsForCase()
                    End If
                Else
                    dsGrps = objGetData.GetGroupIDCheck(txtGDES1.Text, txtGDES2.Text, Session("UserId").ToString(), "CPROP")
                    If dsGrps.Tables(0).Rows.Count > 0 Then

                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Group already exist');", True)
                    Else

                        GROUPID = objUpdateData.AddGroupName(txtGDES1.Text, txtGDES2.Text, txtGDES3.Text, txtGAPP.Text, Session("UserId"), "CPROP", Session("SBALicenseId"))
                        'Started Activity Log Changes
                        Try
                            objUpdateData.InsertLog1(Session("UserId").ToString(), "6", " Company Group #" + GROUPID + " Created succesfully. ", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", GROUPID)
                        Catch ex As Exception

                        End Try
                        'Ended Activity Log Changes
                        txtGDES1.Text = ""
                        txtGDES2.Text = ""
                        txtGDES3.Text = ""
                        txtGAPP.Text = ""
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Group #" + GROUPID.ToString() + " created successfully');", True)
                        divGModify.Visible = False
                        GetPCaseDetails()
                        GetGroupsForCase()
                    End If
                End If
                
            End If
        Catch ex As Exception
            lblError.Text = "btnCreateGrp_Click:" + ex.Message
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
			
			  'Started Activity Log Changes
            Try
                Dim objUpdateData As New StandUpInsData.UpdateInsert
                objUpdateData.InsertLog1(Session("UserId").ToString(), "6", "List Sorted, Sort by: " + e.SortExpression + " ", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
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

                If Request.QueryString("Type").ToString() = "PROP" Then
                    linkBut.Attributes.Add("onclick", "return OpenGroupPopup('../Popup/GroupDetailsFinal.aspx?groupId=" + lblGrpId.Text.Trim() + "&CaseID=" + lblCaseID.Text.Trim() + "&Type=PROP');")
                Else
                    linkBut.Attributes.Add("onclick", "return OpenGroupPopup('../Popup/GroupDetailsFinal.aspx?groupId=" + lblGrpId.Text.Trim() + "&CaseID=" + lblCaseID.Text.Trim() + "&Type=CPROP');")
                End If
                 Next
        Catch ex As Exception
            Response.Write("Error:bindCaseGridSession:" + ex.Message.ToString())
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
                objUpIns.InsertLog1(Session("UserId").ToString(), "6", "Clicked on Button Search, Searched Text: " + txtKey.Text + "", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
			Catch ex As Exception
            Throw New Exception("btnSearch_Click:" + ex.Message)
        End Try
    End Sub

    Protected Sub btnCGrp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCGrp.Click
        Try
            If Request.QueryString("Type").ToString() = "PROP" Then
                lblTitle.Text = "Create Proprietary Group"
            ElseIf Request.QueryString("Type").ToString() = "CPROP" Then
                lblTitle.Text = "Create Company Group"
            End If

            divGModify.Visible = True
            'Started Activity Log Changes
            Try
                Dim objUpIns As New StandUpInsData.UpdateInsert
                objUpIns.InsertLog1(Session("UserId").ToString(), "6", "Clicked on Create Group", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnGCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGCancel.Click
        Try
            divGModify.Visible = False
            txtGDES1.Text = ""
            txtGDES2.Text = ""
            txtGDES3.Text = ""
            txtGAPP.Text = ""
			 'Started Activity Log Changes
            Try
                Dim objUpIns As New StandUpInsData.UpdateInsert
                objUpIns.InsertLog1(Session("UserId").ToString(), "6", "Clicked on Cancel button for Creating Group.", "", Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
            Catch ex As Exception

            End Try
            'Ended Activity Log Changes
        Catch ex As Exception

        End Try
    End Sub

End Class
