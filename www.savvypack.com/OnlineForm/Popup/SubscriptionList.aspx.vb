Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SavvyGetData
Imports SavvyUpInsData

Partial Class OnlineForm_Popup_SubscriptionList
    Inherits System.Web.UI.Page
    Dim ObjGetData As New SavvyGetData.Selectdata()
    Dim ObjUpIns As New SavvyUpInsData.UpdateInsert()
    Dim LicenseID As String = String.Empty

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            hidSubScrpID.Value = Request.QueryString("Id").ToString()
            hidInnerTxt.Value = Request.QueryString("Innertxt").ToString()
            hidSubScrpNm.Value = Request.QueryString("Nm").ToString()
            LicenseID = Request.QueryString("Lid").ToString()
            If Not IsPostBack Then
                hidSortId.Value = "1"
                GetSubScriptionList()
            End If
        Catch ex As Exception
            lblError.Text = "Error:Page_Load_" + ex.Message.ToString()
        End Try
    End Sub

#Region "User Define Delegates"

    Protected Sub GetSubScriptionList()
        Dim ds As New DataSet()
        Try
            If LicenseID <> Nothing Then
                ds = ObjGetData.GetSubscrpDetails("", LicenseID.ToString())
                Session("SubScriptionList") = ds
                If ds.Tables(0).Rows.Count > 0 Then
                    grdSubScrp.DataSource = ds
                    grdSubScrp.DataBind()
                    grdSubScrp.Visible = True
                    lblMsg.Visible = False
                Else
                    lblMsg.Visible = True
                    grdSubScrp.Visible = False
                End If
            Else
                lblMsg.Visible = True
                grdSubScrp.Visible = False
            End If
            
        Catch ex As Exception
            lblError.Text = "Error: GetSubScriptionList() " + ex.Message()
        End Try
    End Sub

    Protected Sub BindSubScriptionListSession()
        Dim Dts As New DataSet()
        Try
            Dts = Session("SubScriptionList")
            grdSubScrp.DataSource = Dts
            grdSubScrp.DataBind()
            lblMsg.Visible = False
        Catch ex As Exception
            lblError.Text = "Error: BindSubScriptionListSession() " + ex.Message()
        End Try
    End Sub

#End Region

#Region "User Grid Event"

    Protected Sub grdSubScrp_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdSubScrp.Sorting
        Dim Dts As New DataSet
        Dim dvSBS As DataView
        Dim numberDivSBS As Integer
        Dim dsSorted As New DataSet()
        Try
            numberDivSBS = Convert.ToInt16(hidSortId.Value.ToString())
            Dts = Session("SubScriptionList")
            dvSBS = Dts.Tables(0).DefaultView

            If ((numberDivSBS Mod 2) = 0) Then
                dvSBS.Sort = e.SortExpression + " " + "DESC"
            Else
                dvSBS.Sort = e.SortExpression + " " + "ASC"
            End If

            numberDivSBS += 1
            hidSortId.Value = numberDivSBS.ToString()
            grdSubScrp.DataSource = dvSBS
            grdSubScrp.DataBind()

            dsSorted.Tables.Add(dvSBS.ToTable())
            Session("SubScriptionList") = dsSorted
            lblMsg.Visible = False
        Catch ex As Exception
            lblError.Text = "Error: grdSubScrp_Sorting() " + ex.Message()
        End Try
    End Sub

    Protected Sub grdSubScrp_databound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSubScrp.DataBound
        Try
            Dim gvr As GridViewRow = grdSubScrp.TopPagerRow

            Dim lb1 As Label = DirectCast(gvr.Cells(0).FindControl("currentpage"), Label)
            lb1.Text = Convert.ToString(grdSubScrp.PageIndex + 1)
            Dim page As Integer() = New Integer(10) {}
            page(0) = grdSubScrp.PageIndex - 2
            page(1) = grdSubScrp.PageIndex - 1
            page(2) = grdSubScrp.PageIndex
            page(3) = grdSubScrp.PageIndex + 1
            page(4) = grdSubScrp.PageIndex + 2
            page(5) = grdSubScrp.PageIndex + 3
            page(6) = grdSubScrp.PageIndex + 4
            page(7) = grdSubScrp.PageIndex + 5
            page(8) = grdSubScrp.PageIndex + 6
            page(9) = grdSubScrp.PageIndex + 7
            page(10) = grdSubScrp.PageIndex + 8
            For i As Integer = 0 To 10
                If i <> 3 Then
                    If page(i) < 1 OrElse page(i) > grdSubScrp.PageCount Then
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
            If grdSubScrp.PageIndex = 0 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton1"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton2"), LinkButton)

                lb.Visible = False
            End If
            If grdSubScrp.PageIndex = grdSubScrp.PageCount - 1 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton3"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton4"), LinkButton)

                lb.Visible = False
            End If
            If grdSubScrp.PageIndex > grdSubScrp.PageCount - 5 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("nmore"), Label)
                lbmore.Visible = False
            End If
            If grdSubScrp.PageIndex < 4 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("pmore"), Label)
                lbmore.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error: grdSubScrp_databound() " + ex.Message()
        End Try
    End Sub

    Protected Sub lb_command_SB(ByVal sender As Object, ByVal e As CommandEventArgs)
        grdSubScrp.PageIndex = Convert.ToInt32(e.CommandArgument) - 1
        BindSubScriptionListSession()
    End Sub

    Protected Sub grdSubScrp_rowcreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSubScrp.RowCreated
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

    Protected Sub grdSubScrp_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSubScrp.PageIndexChanging
        Try
            grdSubScrp.PageIndex = e.NewPageIndex
            BindSubScriptionListSession()
        Catch ex As Exception
            lblError.Text = "Error: grdSubScrp_PageIndexChanging() " + ex.Message()
        End Try
    End Sub

#End Region

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim lblSUBSCDETAILID As New Label
        Dim lblSUBSCNAME As New Label
        Dim check As New CheckBox
        Dim checkflag As Boolean = False
        Dim SubDetailIDs As String = String.Empty
        Try
            For Each Gr As GridViewRow In grdSubScrp.Rows
                lblSUBSCDETAILID = grdSubScrp.Rows(Gr.RowIndex).FindControl("lblSUBSCDETAILID")
                lblSUBSCNAME = grdSubScrp.Rows(Gr.RowIndex).FindControl("lblSUBSCNAME")
                check = grdSubScrp.Rows(Gr.RowIndex).FindControl("SelCase")
                If check.Checked Then
                    checkflag = True
                    SubDetailIDs = SubDetailIDs + lblSUBSCDETAILID.Text + ","
                End If
            Next
            If checkflag Then
                SubDetailIDs = SubDetailIDs.Remove(SubDetailIDs.Length - 1)
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "CloseWindow", "SendCaseInfo('" + SubDetailIDs.ToString() + "');", True)
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Alert", "alert('Please select at least one subscription.');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error: btnSubmit_Click() " + ex.Message()
        End Try
    End Sub

End Class
