Imports System.Data
Imports System.Data.OleDb
Imports System.HttpStyleUriParser
Imports System.Web.UI.WebControls
Imports M1SubGetData
Imports M1SubUpInsData
Partial Class Pages_Market1Sub_Tools_ManageGroups
    Inherits System.Web.UI.Page
    Dim Market1Connection As String = System.Configuration.ConfigurationManager.AppSettings("Market1ConnectionString")
    Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
    Dim Market1ConnectionForPkg As String = System.Configuration.ConfigurationManager.AppSettings("Market1ConnectionStringForPkg")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim objUpIns As New M1SubUpInsData.UpdateInsert
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            txtKey.Attributes.Add("onKeyDown", "return clickButton(event,'" + btnSearch.ClientID + "')")
            If Not IsPostBack Then
                lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " Savvypack Corporation."
                hvUserGrd.Value = "0"
                GetPReportDetails()
                GetGroupsForReport()
                hidType.Value = Request.QueryString("Type").ToString()
            End If
        Catch ex As Exception
            lblError.Text = "Page_Load:" + ex.Message
        End Try
    End Sub

    Protected Sub GetPReportDetails()
        Dim objGetData As New M1SubGetData.Selectdata
        Dim ds As New DataSet
        Dim dsGrps As New DataSet
        Dim dtGrps As New DataTable
        Dim j As Integer
        Dim grpCount As Integer = 0
        Try
            If Request.QueryString("Type").ToString() = "PROP" Then
                ds = objGetData.GetPManageReportGrpDetails(Session("M1UserName").ToString(), Session("UserId").ToString(), txtKey.Text.Trim(), "PROP", Session("M1ServiceID"))
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
          
            Session("UsersDataGroup") = ds
            grdReport.DataSource = ds
            grdReport.DataBind()

        Catch ex As Exception
            lblError.Text = "GetPReportDetails:" + ex.Message
        End Try
    End Sub

    'Protected Sub btnGCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGCreate.Click
    '    Try
    '        Dim ID As String = 1
    '        Dim Name As String = ""
    '        Name = Trim(txtGDES1.Text)
    '        Dim objUpdateData As New M1SubUpInsData.UpdateInsert
    '        Dim objGetData As New M1SubGetData.Selectdata()
    '        Dim dt As New DataSet()
    '        Dim dsGrps As New DataSet()
    '        Dim GROUPID As String = ""
    '        If Name.Length <> 0 Then
    '            If Request.QueryString("Type").ToString() = "PROP" Then
    '                dsGrps = objGetData.GetGroupIDCheck(txtGDES1.Text, txtGDES2.Text, Session("UserId").ToString(), "PROP", Session("M1ServiceID"))
    '                If dsGrps.Tables(0).Rows.Count > 0 Then
    '                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Group already exist');", True)
    '                Else

    '                    GROUPID = objUpdateData.AddGroupName(txtGDES1.Text, txtGDES2.Text, Session("UserId"), "PROP", Session("M1ServiceID"))
    '                    txtGDES1.Text = ""
    '                    txtGDES2.Text = ""
    '                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Group #" + GROUPID + " created successfully');", True)
    '                    'divGModify.Style.Add("Display", "none")
    '                    divGModify.Visible = False
    '                    GetPReportDetails()
    '                    GetGroupsForReport()
    '                End If
    '            End If

    '        End If
    '    Catch ex As Exception
    '        lblError.Text = "btnCreateGrp_Click:" + ex.Message
    '    End Try
    'End Sub

    Protected Sub grdReport_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdReport.Sorting
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
            grdReport.DataSource = dv
            grdReport.DataBind()

            GetGroupsForReport()

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetGroupsForReport()
        Dim linkBut As New LinkButton
        Dim lblGrpId As New Label
        Dim lblReportID As New Label
        Try
            For Each Gr As GridViewRow In grdReport.Rows
                lblGrpId = grdReport.Rows(Gr.RowIndex).FindControl("lblGroupID")
                lblReportID = grdReport.Rows(Gr.RowIndex).FindControl("lblReportID")
                linkBut = grdReport.Rows(Gr.RowIndex).FindControl("lnkGroId")

                If Request.QueryString("Type").ToString() = "PROP" Then
                    linkBut.Attributes.Add("onclick", "return OpenGroupPopup('../Popup/GroupDetailsFinal.aspx?groupId=" + lblGrpId.Text.Trim() + "&ReportID=" + lblReportID.Text.Trim() + "&Type=PROP');")
               End If
            Next
        Catch ex As Exception
            Response.Write("Error:bindReportGridSession:" + ex.Message.ToString())
        End Try
    End Sub
    Protected Sub grdReport_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdReport.PageIndexChanging
        Try
            grdReport.PageIndex = e.NewPageIndex
            bindReportGridSession()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub bindReportGridSession()
        Dim ds As New DataSet()
        Try
            ds = Session("UsersDataGroup")
            grdReport.DataSource = ds
            grdReport.DataBind()
            GetGroupsForReport()
        Catch ex As Exception
            Response.Write("Error:bindReportGridSession:" + ex.Message.ToString())
        End Try
    End Sub
    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            GetPReportDetails()
            GetGroupsForReport()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Dim objUpIns As New M1SubUpInsData.UpdateInsert
            GetPReportDetails()
            GetGroupsForReport()
        Catch ex As Exception
            Throw New Exception("btnSearch_Click:" + ex.Message)
        End Try
    End Sub

    'Protected Sub btnCGrp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCGrp.Click
    '    Try
    '        If Request.QueryString("Type").ToString() = "PROP" Then
    '            lblTitle.Text = "Create Proprietary Group"
    '        End If
    '        divGModify.Visible = True

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Protected Sub btnGCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGCancel.Click
    '    Try
    '        divGModify.Visible = False
    '        txtGDES1.Text = ""
    '        txtGDES2.Text = ""

    '    Catch ex As Exception

    '    End Try
    'End Sub

End Class
