Imports System.Data
Imports System.Data.OleDb
Imports System.HttpStyleUriParser
Imports System.Web.UI.WebControls
Imports E1GetData
Imports E1UpInsData
Partial Class Pages_Econ1_Tools_ManageGroups
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " Savvypack Corporation."
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            txtKey.Attributes.Add("onKeyDown", "return clickButton(event,'" + btnSearch.ClientID + "')")
            If Not IsPostBack Then
                GetPCaseDetails()
                GetGroupsForCase()
                hvUserGrd.Value = "0"
                If Session("Service") = "COMP" Then
                    E1table.Attributes.Add("class", "E1CompModule")
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Page_Load:" + ex.Message
        End Try
    End Sub
    Protected Sub GetPCaseDetails()
        Dim objGetData As New E1GetData.Selectdata
        Dim ds As New DataSet
        'Dim dsGrps As New DataSet
        Dim dtGrps As New DataTable
        Dim j As Integer
        Dim grpCount As Integer = 0
        Try
            If Session("SavvyModType") <> "1" Then
                ds = objGetData.GetPCaseGrpDetailsBem(Session("USERID").ToString(), txtKey.Text.Trim().Replace("'", "''").ToString())
            Else
                If Session("Service") = "COMP" Then
                    ds = objGetData.GetCompCaseGrpDetails(Session("USERID").ToString(), txtKey.Text.Trim().Replace("'", "''").ToString(), Session("ServiceId"))
                Else
                    ds = objGetData.GetPCaseGrpDetails(Session("USERID").ToString(), txtKey.Text.Trim().Replace("'", "''").ToString())
                End If

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
                lblmsg.Text = "No Data Available"
            End If

            Session("UsersDataGroup") = ds
            grdCase.DataSource = ds
            grdCase.DataBind()

        Catch ex As Exception
            lblError.Text = "GetPCaseDetails:" + ex.Message
        End Try
    End Sub
 
    Protected Sub btnCreateGrp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateGrp.Click
        Try
            Dim ID As String = 1
            Dim Name As String = ""
            Name = Trim(txtGroupDe1.Text)
            Dim objUpdateData As New E1UpInsData.UpdateInsert
            Dim objGetData As New E1GetData.Selectdata()
            Dim dt As New DataSet()
            Dim dsGrps As New DataSet()
            If Name.Length <> 0 Then


                dt = objGetData.ValidateGroupName(txtGroupDe1.Text.Trim().Replace("'", "''").ToString(), txtGroupDe2.Text.Trim().Replace("'", "''").ToString(), Session("UserId"), Session("Service"))
                If dt.Tables(0).Rows.Count > 0 Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Group already exist');", True)
                    trCreate.Style.Add("Display", "Block")
                Else
                    If Session("Service") = "COMP" Then
                        objUpdateData.AddCompGroupName(txtGroupDe1.Text.Trim().Replace("'", "''").ToString(), txtGroupDe2.Text.Trim().Replace("'", "''").ToString(), Session("UserId"), Session("ServiceId"))
                    Else
                        objUpdateData.AddGroupName(txtGroupDe1.Text.Trim().Replace("'", "''").ToString(), txtGroupDe2.Text.Trim().Replace("'", "''").ToString(), Session("UserId"))
                    End If
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Group created successfully');", True)
                    txtGroupDe1.Text = ""
                    txtGroupDe2.Text = ""

                    trCreate.Style.Add("Display", "none")
                End If
            End If

        Catch ex As Exception
            lblError.Text = "btnCreateGrp_Click:" + ex.Message
        End Try
    End Sub

    Protected Sub btnGlobalManager_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnGlobalManager.Click
        Try
            Session("E1GroupID") = Nothing
            'Response.Redirect("~/Pages/Econ1/Default.aspx")
            If Session("SavvyModType") <> "1" Then
                Response.Redirect("~/Pages/Econ1/Default2.aspx")
            Else
                Response.Redirect("~/Pages/Econ1/Default.aspx")
            End If
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
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetGroupsForCase()
        Dim linkBut As New LinkButton
        Dim lblGrpId As New Label
        Dim lblCaseID As New Label
        For Each Gr As GridViewRow In grdCase.Rows
            lblGrpId = grdCase.Rows(Gr.RowIndex).FindControl("lblGroupID")
            lblCaseID = grdCase.Rows(Gr.RowIndex).FindControl("lblCaseID")
            linkBut = grdCase.Rows(Gr.RowIndex).FindControl("lnkGroId")
            linkBut.Attributes.Add("onclick", "return OpenGroupPopup('../Popup/GroupDetails.aspx?groupId=" + lblGrpId.Text.Trim() + "&CaseID=" + lblCaseID.Text.Trim() + "');")
        Next
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
            GetPCaseDetails()
            GetGroupsForCase()
        Catch ex As Exception
            Throw New Exception("btnSearch_Click" + ex.Message)
        End Try
    End Sub
End Class
