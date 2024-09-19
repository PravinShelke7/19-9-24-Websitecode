Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyGetData
Imports SavvyUpInsData

Partial Class OnlineForm_ProjActivityLog
    Inherits System.Web.UI.Page
    Dim ObjGetData As New SavvyGetData.Selectdata()
    Dim ObjUpIns As New SavvyUpInsData.UpdateInsert()

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            If Request.QueryString("Site").ToString() = "A" Then
                hidLicenseID.Value = Request.QueryString("LID").ToString()
            Else
                hidLicenseID.Value = Session("SavvyLicenseId").ToString()
            End If
            If Not IsPostBack Then
                hidSortId.Value = "1"
                GetSubLatestSub()
            End If
        Catch ex As Exception
            lblError.Text = "Error:Page_Load_" + ex.Message.ToString()
        End Try
    End Sub

    'Protected Sub GetLastSubscription(ByVal SubscrID As String)
    '    Dim ds As New DataSet()
    '    Try
    '        ds = ObjGetData.GetSubScrInfo(hidSubscrpID.Value.ToString())
    '        If ds.Tables(0).Rows.Count > 0 Then
    '            lblSubStartDate.Text = ds.Tables(0).Rows(0).Item("STARTDATE").ToString()
    '            lblSubEndDate.Text = ds.Tables(0).Rows(0).Item("ENDDDATE").ToString()
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = "Error:GetSubScrpInfo" + ex.Message.ToString()
    '    End Try
    'End Sub

    Protected Sub GetSubLatestSub()
        Dim ds As New DataSet()
        Try
            ds = ObjGetData.GetSubLatestSub(hidLicenseID.Value.ToString())
            If ds.Tables(0).Rows.Count > 0 Then
                lblSubStartDate.Text = ds.Tables(0).Rows(0).Item("STARTDATE").ToString()
                lblSubEndDate.Text = ds.Tables(0).Rows(0).Item("ENDDDATE").ToString()
                hidSubSdate.Value = ds.Tables(0).Rows(0).Item("SDATE").ToString()
                hidSubEdate.Value = ds.Tables(0).Rows(0).Item("EDATE").ToString()
                GetProjBySubScription()
            Else
                lblMsg.Visible = True
                trpagecountSB.Visible = False
                grdProjDetail.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetSubLatestSub" + ex.Message.ToString()
        End Try
    End Sub

#Region "User Define Delegates"

    Protected Sub GetProjBySubScription()
        Dim ds As New DataSet()
        Dim Models As Integer = 0
        Try
            If hidSubSdate.Value <> "" And hidSubEdate.Value <> "" Then
                'ds = ObjGetData.GetProjDetailBySubScription(txtSearchSB.Text.Trim.ToString(), hidLicenseID.Value.ToString(), hidSubSdate.Value, hidSubEdate.Value)
                ds = ObjGetData.GetProjDetailBySubScription_Optmz(txtSearchSB.Text.Trim.ToString(), hidLicenseID.Value.ToString(), hidSubSdate.Value, hidSubEdate.Value)
                lblNOP.Text = ds.Tables(0).Rows.Count
                If ds.Tables(0).Rows.Count > 0 Then
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        Models = Models + ds.Tables(0).Rows(i).Item("ModelCompleted").ToString() + ds.Tables(0).Rows(i).Item("GRPMODELCOMPLETED").ToString()
                    Next
                End If
                lblNOM.Text = Models
                Session("ProjBySubscr") = ds
                If ds.Tables(0).Rows.Count > 0 Then
                    grdProjDetail.DataSource = ds
                    grdProjDetail.DataBind()
                    grdProjDetail.Visible = True
                    trpagecountSB.Visible = True
                    lblMsg.Visible = False
                Else
                    lblMsg.Visible = True
                    trpagecountSB.Visible = False
                    grdProjDetail.Visible = False
                End If
            Else
                lblMsg.Visible = True
                trpagecountSB.Visible = False
                grdProjDetail.Visible = False
            End If
            

        Catch ex As Exception
            lblError.Text = "Error: GetProjBySubScription() " + ex.Message()
        End Try
    End Sub

    Protected Sub BindSubScriptionListSession()
        Dim Dts As New DataSet()
        Try
            Dts = Session("ProjBySubscr")
            grdProjDetail.DataSource = Dts
            grdProjDetail.DataBind()
            lblMsg.Visible = False
        Catch ex As Exception
            lblError.Text = "Error: BindSubScriptionListSession() " + ex.Message()
        End Try
    End Sub

#End Region

#Region "User Grid Event"

    Protected Sub grdProjDetail_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdProjDetail.Sorting
        Dim Dts As New DataSet
        Dim dvSBS As DataView
        Dim numberDivSBS As Integer
        Dim dsSorted As New DataSet()
        Try
            numberDivSBS = Convert.ToInt16(hidSortId.Value.ToString())
            Dts = Session("ProjBySubscr")
            dvSBS = Dts.Tables(0).DefaultView

            If ((numberDivSBS Mod 2) = 0) Then
                dvSBS.Sort = e.SortExpression + " " + "DESC"
            Else
                dvSBS.Sort = e.SortExpression + " " + "ASC"
            End If

            numberDivSBS += 1
            hidSortId.Value = numberDivSBS.ToString()
            grdProjDetail.DataSource = dvSBS
            grdProjDetail.DataBind()

            dsSorted.Tables.Add(dvSBS.ToTable())
            Session("ProjBySubscr") = dsSorted
            lblMsg.Visible = False
        Catch ex As Exception
            lblError.Text = "Error: grdProjDetail_Sorting() " + ex.Message()
        End Try
    End Sub

    Protected Sub ddlPageCountSB_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageCountSB.SelectedIndexChanged
        Try
            grdProjDetail.PageSize = ddlPageCountSB.SelectedValue.ToString()
            BindSubScriptionListSession()
        Catch ex As Exception
            Throw New Exception("Error:ddlPageCountSB_SelectedIndexChanged" + ex.Message)
        End Try
    End Sub

    Protected Sub grdProjDetail_databound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdProjDetail.DataBound
        Try
            Dim gvr As GridViewRow = grdProjDetail.TopPagerRow

            Dim lb1 As Label = DirectCast(gvr.Cells(0).FindControl("currentpage"), Label)
            lb1.Text = Convert.ToString(grdProjDetail.PageIndex + 1)
            Dim page As Integer() = New Integer(10) {}
            page(0) = grdProjDetail.PageIndex - 2
            page(1) = grdProjDetail.PageIndex - 1
            page(2) = grdProjDetail.PageIndex
            page(3) = grdProjDetail.PageIndex + 1
            page(4) = grdProjDetail.PageIndex + 2
            page(5) = grdProjDetail.PageIndex + 3
            page(6) = grdProjDetail.PageIndex + 4
            page(7) = grdProjDetail.PageIndex + 5
            page(8) = grdProjDetail.PageIndex + 6
            page(9) = grdProjDetail.PageIndex + 7
            page(10) = grdProjDetail.PageIndex + 8
            For i As Integer = 0 To 10
                If i <> 3 Then
                    If page(i) < 1 OrElse page(i) > grdProjDetail.PageCount Then
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
            If grdProjDetail.PageIndex = 0 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton1"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton2"), LinkButton)

                lb.Visible = False
            End If
            If grdProjDetail.PageIndex = grdProjDetail.PageCount - 1 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton3"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton4"), LinkButton)

                lb.Visible = False
            End If
            If grdProjDetail.PageIndex > grdProjDetail.PageCount - 5 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("nmore"), Label)
                lbmore.Visible = False
            End If
            If grdProjDetail.PageIndex < 4 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("pmore"), Label)
                lbmore.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error: grdProjDetail_databound() " + ex.Message()
        End Try
    End Sub

    Protected Sub lb_command_SB(ByVal sender As Object, ByVal e As CommandEventArgs)
        grdProjDetail.PageIndex = Convert.ToInt32(e.CommandArgument) - 1
        BindSubScriptionListSession()
    End Sub

    Protected Sub grdProjDetail_rowcreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdProjDetail.RowCreated
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim gvr As GridViewRow = e.Row
            Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p0"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_SB
            lb = DirectCast(gvr.Cells(0).FindControl("p1"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_SB
            lb = DirectCast(gvr.Cells(0).FindControl("p2"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_SB
            lb = DirectCast(gvr.Cells(0).FindControl("p4"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_SB
            lb = DirectCast(gvr.Cells(0).FindControl("p5"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_SB
            lb = DirectCast(gvr.Cells(0).FindControl("p6"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_SB
            lb = DirectCast(gvr.Cells(0).FindControl("p7"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_SB
            lb = DirectCast(gvr.Cells(0).FindControl("p8"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_SB
            lb = DirectCast(gvr.Cells(0).FindControl("p9"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_SB
            lb = DirectCast(gvr.Cells(0).FindControl("p10"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_SB
        End If
    End Sub

    Protected Sub grdProjDetail_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdProjDetail.PageIndexChanging
        Try
            grdProjDetail.PageIndex = e.NewPageIndex
            BindSubScriptionListSession()
        Catch ex As Exception
            lblError.Text = "Error: grdProjDetail_PageIndexChanging() " + ex.Message()
        End Try
    End Sub

#End Region

    Protected Sub btnSearchSB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchSB.Click
        Try
            GetProjBySubScription()
        Catch ex As Exception
            lblError.Text = "Error: btnSearchSB_Click() " + ex.Message()
        End Try
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Dim DsSubScrDates As New DataSet()
        Try
            DsSubScrDates = ObjGetData.GetSubDatesBySubID(hidSubscrpID.Value.ToString())
            If DsSubScrDates.Tables(0).Rows.Count > 0 Then
                lblSubEndDate.Text = DsSubScrDates.Tables(0).Rows(0).Item("INNEREDATE").ToString()
                lblSubStartDate.Text = DsSubScrDates.Tables(0).Rows(0).Item("INNERSDATE").ToString()
                hidSubSdate.Value = DsSubScrDates.Tables(0).Rows(0).Item("SDATE").ToString()
                hidSubEdate.Value = DsSubScrDates.Tables(0).Rows(0).Item("EDATE").ToString()
            End If
            'lnkSelSubscrp.Text = hidSubScrpName.Value
            GetProjBySubScription()
        Catch ex As Exception
            lblError.Text = "Error: btnRefresh_Click() " + ex.Message()
        End Try
    End Sub

End Class
