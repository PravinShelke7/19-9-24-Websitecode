Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyProGetData
Imports SavvyProUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Net.Mail
Imports System.Net
Partial Class Pages_SavvyPackPro_CheckRFP_Status
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try

            pnlBMConfigVendor.Visible = True
            lblFooter.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            If Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            If Session("USERID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                hidSortIdBMVendor.Value = "0"
                'GetStatusList()
                ChkExistingRfp()
            End If
            If tabRfpManager.ActiveTabIndex = "1" Then
                DivRFPSelector.Style("display") = "inline"
            Else
                DivRFPSelector.Style("display") = "none"
            End If
        Catch ex As Exception
            lblError.Text = "Error: Page_Load() " + ex.Message()
        End Try
    End Sub

    Protected Sub ChkExistingRfp()
        Dim DsCheckRfp As New DataSet()
        Try
            If Session("hidRfpID") = Nothing Then
                DsCheckRfp = objGetdata.GetLatestRFPbyLicenseID(Session("LicenseNo"), Session("USERID"))
                If DsCheckRfp.Tables(0).Rows.Count > 0 Then
                    GetRfpDetails(DsCheckRfp.Tables(0).Rows(0).Item("ID").ToString())
                Else
                    RfpDetail.Visible = False
                End If
            Else
                GetRfpDetails(Session("hidRfpID"))
            End If

        Catch ex As Exception
            lblError.Text = "Error: ChkExistingRfp() " + ex.Message()
        End Try
    End Sub

    Protected Sub GetRfpDetails(ByVal RfpID As String)
        Dim DsRfpdet As New DataSet()
        Dim str As DateTime
        Try
            If RfpID <> "" Or RfpID <> "0" Then
                DsRfpdet = objGetdata.GetRFPLogList(RfpID, Session("LicenseNo"), txtlogs.Text)
                If DsRfpdet.Tables(0).Rows.Count > 0 Then
                    RfpDetail.Visible = True
                    lblDueD.Text = DsRfpdet.Tables(0).Rows(0).Item("FDUEDATE").ToString()
                    ' str = DsRfpdet.Tables(0).Rows(0).Item("DESCRIPTION").ToString()
                    lblSelRfpID.Text = DsRfpdet.Tables(0).Rows(0).Item("ID").ToString()
                    'lblDueD.Text = FormatDateTime(str, DateFormat.LongDate).Replace(DateTime.Now.DayOfWeek.ToString("MMM dd, yyyy") + ",", "")
                    lnkSelRFP.Text = DsRfpdet.Tables(0).Rows(0).Item("DES1").ToString()
                    Session("hidRfpID") = lblSelRfpID.Text
                    lblSelRfpDes.Text = DsRfpdet.Tables(0).Rows(0).Item("DESCRIPTION").ToString()
                    lblType.Text = DsRfpdet.Tables(0).Rows(0).Item("TYPEDESC").ToString()
                    If Session("USERID") = DsRfpdet.Tables(0).Rows(0).Item("USERID").ToString() Then
                        tabRfpManager.Enabled = True
                    Else
                        tabRfpManager.Enabled = False
                    End If
                    'tabIssueRFP.Enabled = True
                    loadTab()
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('System isn't able to find RFPID. Please try again.');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error: GetRfpDetails() " + ex.Message()
        End Try
    End Sub

    Protected Sub loadTab()
        Try
            GetStatusList()
            GetLogStatusList()
        Catch ex As Exception
            lblError.Text = "Error: loadTab() " + ex.Message()
        End Try
    End Sub

    Protected Sub tabRfpManager_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabRfpManager.ActiveTabChanged
        Try
            If tabRfpManager.ActiveTabIndex = "1" Then
                DivRFPSelector.Style("display") = "inline"
            Else
                DivRFPSelector.Style("display") = "none"
            End If
        Catch ex As Exception
            lblError.Text = "Error: tabSupplierManager_ActiveTabChanged() " + ex.Message()
        End Try
    End Sub

#Region "Summary"

    Protected Sub GetStatusList()
        Dim Ds As New DataSet
        Try
            Ds = objGetdata.GetRFPList(txtKey.Text.Trim.ToString(), Session("LicenseNo"))
            Session("BMVendorList") = Ds
            lblRecondCnt.Text = Ds.Tables(0).Rows.Count
            'If tabRfpManager.ActiveTabIndex = "1" Then
            '    DivRFPSelector.Style("display") = "inline"
            'End If

            If Ds.Tables(0).Rows.Count > 0 Then
                lblNOVendor.Visible = False
                grdUsers.Visible = True
                grdUsers.DataSource = Ds
                grdUsers.DataBind()
                BindLinkSum()
            Else
                lblNOVendor.Visible = True
                grdUsers.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetStatusList:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub BindLinkSum()
        Dim lblUserID As New Label
        Try
            For Each Gr As GridViewRow In grdUsers.Rows
                lblUserID = grdUsers.Rows(Gr.RowIndex).FindControl("lblUserID")

                If Session("UserId") = lblUserID.Text Then
                    Gr.Enabled = True

                Else
                    Gr.Enabled = False
                    Gr.BackColor = Drawing.Color.LightGray
                End If

            Next
        Catch ex As Exception
            lblError.Text = "Error: BindLink:" + ex.Message()
        End Try
    End Sub
    Protected Sub BindUsersListUsingSession()
        Try
            Dim Dts As New DataSet
            Dts = Session("BMVendorList")
            grdUsers.DataSource = Dts
            grdUsers.DataBind()
            BindLinkSum()

        Catch ex As Exception
            Response.Write("Error:BindUsersListUsingSession:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetStatusList()
            GetLogStatusList()
        Catch ex As Exception
            lblError.Text = "Error:btnSearch_Click" + ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "Log"

    Protected Sub GetLogStatusList()
        Dim Ds As New DataSet
        Try
            'Ds = objGetdata.GetRFPLogList(Session("hidRfpID"))
            Ds = objGetdata.GetRFPLogList(Session("hidRfpID"), Session("LicenseNo"), txtlogs.Text)
            Session("BMLogList") = Ds
            lbllogcnt.Text = Ds.Tables(0).Rows.Count

            'RFPSelector.Visible = False
            ' RFPSelector.Style("display") = "block"
            If Ds.Tables(0).Rows.Count > 0 Then
                lblNOVendor.Visible = False
                grdLog.Visible = True
                grdLog.DataSource = Ds
                grdLog.DataBind()
                BindLinkLog()
            Else
                lblNOVendor.Visible = True
                grdLog.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetLogStatusList:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub BindLinkLog()
        Dim lblUserID As New Label
        Dim lnkstatus As New LinkButton
        Try
            For Each Gr As GridViewRow In grdLog.Rows
                lblUserID = grdLog.Rows(Gr.RowIndex).FindControl("lblUserID1")
                lnkstatus = grdLog.Rows(Gr.RowIndex).FindControl("lnkstatus")
                If Session("UserId") = lblUserID.Text Then
                    Gr.Enabled = True
                Else
                    Gr.Enabled = False
                    Gr.BackColor = Drawing.Color.LightGray
                End If
                lnkstatus.Attributes.Add("onclick", "return ShowPopUpStatus('PopUp/Status.aspx'); return false;")
            Next
        Catch ex As Exception
            lblError.Text = "Error: BindLinkLog:" + ex.Message()
        End Try
    End Sub
    Protected Sub BindUsersListUsingSession1()
        Try
            Dim Dts As New DataSet
            Dts = Session("BMVendorList")
            grdLog.DataSource = Dts
            grdLog.DataBind()
            BindLinkLog()

        Catch ex As Exception
            Response.Write("Error:BindUsersListUsingSession:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub BindUsersListUsingSessionLog()
        Try
            Dim Dts As New DataSet
            Dts = Session("BMLogList")
            grdLog.DataSource = Dts
            grdLog.DataBind()
        Catch ex As Exception
            Response.Write("Error:BindUsersListUsingSession:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub btnLogSearch_Click(sender As Object, e As System.EventArgs) Handles btnLogSearch.Click
        Try
            'GetStatusList()
            GetLogStatusList()
        Catch ex As Exception
            lblError.Text = "Error: btnLogSearch_Click() " + ex.Message()
        End Try
    End Sub

#End Region

#Region "User Grid Event"

    Protected Sub grdUsers_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdUsers.Sorting
        Dim Dts As New DataSet
        Dim dv As DataView
        Dim numberDiv As Integer
        Dim dsSorted As New DataSet()
        Try
            numberDiv = Convert.ToInt16(hidSortIdBMVendor.Value.ToString())
            Dts = Session("BMVendorList")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            hidSortIdBMVendor.Value = numberDiv.ToString()
            grdUsers.DataSource = dv
            grdUsers.DataBind()

            BindLinkSum()
            dsSorted.Tables.Add(dv.ToTable())
            Session("BMVendorList") = dsSorted

        Catch ex As Exception
            Response.Write("Error:grdUsers_Sorting:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub grdUsers_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdUsers.PageIndexChanging
        Try
            grdUsers.PageIndex = e.NewPageIndex
            BindUsersListUsingSession()
        Catch ex As Exception
            Response.Write("Error:grdUsers_PageIndexChanging:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub ddlPageCountC_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageCountC.SelectedIndexChanged
        Try
            grdUsers.PageSize = ddlPageCountC.SelectedItem.ToString()
            BindUsersListUsingSession()
        Catch ex As Exception
            Response.Write("Error:ddlPageCountC_SelectedIndexChanged:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub grdUsers_databound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdUsers.DataBound
        Try
            Dim gvr As GridViewRow = grdUsers.TopPagerRow

            Dim lb1 As Label = DirectCast(gvr.Cells(0).FindControl("currentpage"), Label)
            lb1.Text = Convert.ToString(grdUsers.PageIndex + 1)
            Dim page As Integer() = New Integer(10) {}
            page(0) = grdUsers.PageIndex - 2
            page(1) = grdUsers.PageIndex - 1
            page(2) = grdUsers.PageIndex
            page(3) = grdUsers.PageIndex + 1
            page(4) = grdUsers.PageIndex + 2
            page(5) = grdUsers.PageIndex + 3
            page(6) = grdUsers.PageIndex + 4
            page(7) = grdUsers.PageIndex + 5
            page(8) = grdUsers.PageIndex + 6
            page(9) = grdUsers.PageIndex + 7
            page(10) = grdUsers.PageIndex + 8
            For i As Integer = 0 To 10
                If i <> 3 Then
                    If page(i) < 1 OrElse page(i) > grdUsers.PageCount Then
                        Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p" & Convert.ToString(i)), LinkButton)
                        lb.Visible = False
                    Else
                        Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p" & Convert.ToString(i)), LinkButton)
                        lb.Text = Convert.ToString(page(i))

                        lb.CommandName = "pageno"

                        lb.CommandArgument = lb.Text
                    End If
                End If
            Next
            If grdUsers.PageIndex = 0 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton1"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton2"), LinkButton)

                lb.Visible = False
            End If
            If grdUsers.PageIndex = grdUsers.PageCount - 1 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton3"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton4"), LinkButton)

                lb.Visible = False
            End If
            If grdUsers.PageIndex > grdUsers.PageCount - 5 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("nmore"), Label)
                lbmore.Visible = False
            End If
            If grdUsers.PageIndex < 4 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("pmore"), Label)
                lbmore.Visible = False
            End If
        Catch ex As Exception
            Response.Write("Error:grdUsers_databound:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub lb_command_VC(ByVal sender As Object, ByVal e As CommandEventArgs)
        grdUsers.PageIndex = Convert.ToInt32(e.CommandArgument) - 1
        BindUsersListUsingSession()
    End Sub

    Protected Sub grdUsers_rowcreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdUsers.RowCreated
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim gvr As GridViewRow = e.Row
            Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p0"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p1"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p2"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p4"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p5"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p6"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p7"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p8"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p9"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
            lb = DirectCast(gvr.Cells(0).FindControl("p10"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC
        End If
    End Sub

#End Region

#Region "User Grid Event Log"

    Protected Sub grdLog_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdLog.Sorting
        Dim Dts As New DataSet
        Dim dv As DataView
        Dim numberDiv As Integer
        Dim dsSorted As New DataSet()
        Try
            numberDiv = Convert.ToInt16(hidSortIdBMVendor.Value.ToString())
            Dts = Session("BMLogList")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            hidSortIdBMVendor.Value = numberDiv.ToString()
            grdLog.DataSource = dv
            grdLog.DataBind()
            BindLinkLog()
            dsSorted.Tables.Add(dv.ToTable())
            Session("BMLogList") = dsSorted

        Catch ex As Exception
            Response.Write("Error:grdLog_Sorting:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub grdLog_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdLog.PageIndexChanging
        Try
            grdLog.PageIndex = e.NewPageIndex
            BindUsersListUsingSession1()
        Catch ex As Exception
            Response.Write("Error:grdLog_PageIndexChanging:" + ex.Message.ToString())
        End Try
    End Sub



    Protected Sub grdLog_databound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdLog.DataBound
        Try
            Dim gvr As GridViewRow = grdLog.TopPagerRow

            Dim lb1 As Label = DirectCast(gvr.Cells(0).FindControl("currentpage"), Label)
            lb1.Text = Convert.ToString(grdLog.PageIndex + 1)
            Dim page As Integer() = New Integer(10) {}
            page(0) = grdLog.PageIndex - 2
            page(1) = grdLog.PageIndex - 1
            page(2) = grdLog.PageIndex
            page(3) = grdLog.PageIndex + 1
            page(4) = grdLog.PageIndex + 2
            page(5) = grdLog.PageIndex + 3
            page(6) = grdLog.PageIndex + 4
            page(7) = grdLog.PageIndex + 5
            page(8) = grdLog.PageIndex + 6
            page(9) = grdLog.PageIndex + 7
            page(10) = grdLog.PageIndex + 8
            For i As Integer = 0 To 10
                If i <> 3 Then
                    If page(i) < 1 OrElse page(i) > grdLog.PageCount Then
                        Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p" & Convert.ToString(i)), LinkButton)
                        lb.Visible = False
                    Else
                        Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p" & Convert.ToString(i)), LinkButton)
                        lb.Text = Convert.ToString(page(i))

                        lb.CommandName = "pageno"

                        lb.CommandArgument = lb.Text
                    End If
                End If
            Next
            If grdLog.PageIndex = 0 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton1"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton2"), LinkButton)

                lb.Visible = False
            End If
            If grdLog.PageIndex = grdLog.PageCount - 1 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton3"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton4"), LinkButton)

                lb.Visible = False
            End If
            If grdLog.PageIndex > grdLog.PageCount - 5 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("nmore"), Label)
                lbmore.Visible = False
            End If
            If grdLog.PageIndex < 4 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("pmore"), Label)
                lbmore.Visible = False
            End If
        Catch ex As Exception
            Response.Write("Error:grdLog_databound:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub lb_command_VC1(ByVal sender As Object, ByVal e As CommandEventArgs)
        grdLog.PageIndex = Convert.ToInt32(e.CommandArgument) - 1
        BindUsersListUsingSession1()
    End Sub

    Protected Sub grdLog_rowcreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdLog.RowCreated
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim gvr As GridViewRow = e.Row
            Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p0"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC1
            lb = DirectCast(gvr.Cells(0).FindControl("p1"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC1
            lb = DirectCast(gvr.Cells(0).FindControl("p2"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC1
            lb = DirectCast(gvr.Cells(0).FindControl("p4"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC1
            lb = DirectCast(gvr.Cells(0).FindControl("p5"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC1
            lb = DirectCast(gvr.Cells(0).FindControl("p6"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC1
            lb = DirectCast(gvr.Cells(0).FindControl("p7"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC1
            lb = DirectCast(gvr.Cells(0).FindControl("p8"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC1
            lb = DirectCast(gvr.Cells(0).FindControl("p9"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC1
            lb = DirectCast(gvr.Cells(0).FindControl("p10"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_VC1
        End If
    End Sub

#End Region

#Region "Hidden Buttons"
    Protected Sub btnHidRFPCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHidRFPCreate.Click
        Try
            If hidRfpID.Value <> "" Or hidRfpID.Value <> "0" Then
                ' tabIssueRFP.Enabled = True
                lnkSelRFP.Text = hidRfpNm.Value
                GetRfpDetails(hidRfpID.Value)
            End If
        Catch ex As Exception
            lblError.Text = "Error: btnHidRFPCreate_Click() " + ex.Message()
        End Try
    End Sub
    Protected Sub btnRefreshVList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefreshVList.Click
        Try
            GetStatusList()
            GetLogStatusList()
        Catch ex As Exception
            lblError.Text = "Error: btnRefreshVList_Click() " + ex.Message()
        End Try
    End Sub

#End Region

End Class
