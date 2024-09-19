Imports System.Data
Imports System.Data.OleDb
Imports System
Imports EmoGetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Net.Mail
Imports System.Net
Partial Class Pages_DataManager_Default
    Inherits System.Web.UI.Page
#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_DATAMANAGER_DEFAULT")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Try
                lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            Catch ex As Exception
            End Try
            If Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                GetProjectDetails()
                hidSortId.Value = "0"
            End If
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub
    
    Public Sub GetProjectDetails()
        Dim objGetData As New EmoGetData.Selectdata
        Dim ds As New DataSet
        Dim dsMemo As New DataSet
        Try
            ds = objGetData.GetDataManager()
            Session("ProjData") = ds
            lblRecondCnt.Text = ds.Tables(0).Rows.Count

            If ds.Tables(0).Rows.Count > 0 Then
                ' grdSpecs.PageSize = ddlSize.SelectedItem.ToString()
                grdSpecs.PageSize = ddlPageCountC.SelectedItem.ToString()
                grdSpecs.Visible = True
                grdSpecs.DataSource = ds
                grdSpecs.DataBind()
                lblMsg.Visible = False
                ddlPageCountC.Enabled = True
                If ds.Tables(0).Rows.Count <= 1 Then
                    grdSpecs.Height = 150
                End If
                BindLink()
            Else
                lblMsg.Height = 100
                ContentPage.Width = 1360
                ContentPage.Height = 150
                'ContentPage.Width = 200
                lblMsg.Visible = True
                ddlPageCountC.Enabled = False
                grdSpecs.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetProjectDetails:" + ex.Message.ToString() + ""
        End Try

    End Sub

    Protected Sub grdProject_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdSpecs.Sorting
        Dim dsSortedOppor As New DataSet()
        Try
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hidSortId.Value.ToString())
            Dts = Session("ProjData")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If

            numberDiv += 1
            hidSortId.Value = numberDiv.ToString()
            grdSpecs.DataSource = dv
            grdSpecs.DataBind()

            dsSortedOppor.Tables.Add(dv.ToTable())
            Session("ProjData") = dsSortedOppor
            lblMsg.Visible = False
            BindLink()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdUsers_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSpecs.PageIndexChanging
        Try
            grdSpecs.PageIndex = e.NewPageIndex
            BindGridSession()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlPageCountC_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageCountC.SelectedIndexChanged
        Try
            grdSpecs.PageSize = ddlPageCountC.SelectedItem.ToString()
            BindGridSession()
        Catch ex As Exception
            lblError.Text = "ddlPageCountC_SelectedIndexChanged:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindGridSession()
        Try
            Dim Dts As New DataSet
            Dts = Session("ProjData")
            grdSpecs.DataSource = Dts
            grdSpecs.DataBind()
            lblMsg.Visible = False
            BindLink()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grd_popup_details_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles grdSpecs.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            For i As Integer = 0 To e.Row.Cells.Count - 1
                If i = 0 Then
                    e.Row.Cells(i).ToolTip = "Click to sort by SKU"
                ElseIf i = 1 Then
                    e.Row.Cells(i).ToolTip = "Click to sort by SPEC#"
                ElseIf i = 2 Then
                    e.Row.Cells(i).ToolTip = "Click to sort by Package Type"
                ElseIf i = 3 Then
                    e.Row.Cells(i).ToolTip = "Click to sort by Package Discription"
                ElseIf i = 4 Then
                    e.Row.Cells(i).ToolTip = "Click to sort by Business Unit"
                ElseIf i = 5 Then
                    e.Row.Cells(i).ToolTip = "Click to sort by Brand"
                ElseIf i = 6 Then
                    e.Row.Cells(i).ToolTip = "Click to sort by Volume"
                ElseIf i = 7 Then
                    e.Row.Cells(i).ToolTip = "Click to sort by Length"
                ElseIf i = 8 Then
                    e.Row.Cells(i).ToolTip = "Click to sort by Width"
                ElseIf i = 9 Then
                    e.Row.Cells(i).ToolTip = "Click to sort by Area"
                ElseIf i = 10 Then
                    e.Row.Cells(i).ToolTip = "Click to sort by Weight"
                ElseIf i = 11 Then
                    e.Row.Cells(i).ToolTip = "Click to sort by Greenhouse Gases(lb GHG/lb Package)"
                ElseIf i = 12 Then
                    e.Row.Cells(i).ToolTip = "Click to sort by Greenhouse Gases(lb)"
                End If
            Next
        End If
        
    End Sub

    Protected Sub BindLink()
        Dim lblVol As Label
        Dim lblLNGTH As Label
        Dim lblWIDTH As Label
        Dim lblAREA As Label
        Dim lblWEIGHT As Label
        Dim lblGHGRELEASE As Label
        Dim lblGHGTOT As Label
        Try
            For Each Gr As GridViewRow In grdSpecs.Rows
                lblVol = grdSpecs.Rows(Gr.RowIndex).FindControl("lblVOLUME")
                Gr.Cells(6).Text = FormatNumber(lblVol.Text, 0, , TriState.False)

                lblLNGTH = grdSpecs.Rows(Gr.RowIndex).FindControl("lblLNGTH")
                Gr.Cells(7).Text = FormatNumber(lblLNGTH.Text, 2, , TriState.False)

                lblWIDTH = grdSpecs.Rows(Gr.RowIndex).FindControl("lblWIDTH")
                Gr.Cells(8).Text = FormatNumber(lblWIDTH.Text, 2, , TriState.False)

                lblAREA = grdSpecs.Rows(Gr.RowIndex).FindControl("lblAREA")
                Gr.Cells(9).Text = FormatNumber(lblAREA.Text, 2, , TriState.False)

                lblWEIGHT = grdSpecs.Rows(Gr.RowIndex).FindControl("lblWEIGHT")
                Gr.Cells(10).Text = FormatNumber(lblWEIGHT.Text, 0, , TriState.False)

                lblGHGRELEASE = grdSpecs.Rows(Gr.RowIndex).FindControl("lblGHGRELEASE")
                Gr.Cells(11).Text = FormatNumber(lblGHGRELEASE.Text, 2, , TriState.False)

                lblGHGTOT = grdSpecs.Rows(Gr.RowIndex).FindControl("lblGHGTOT")
                Gr.Cells(12).Text = FormatNumber(lblGHGTOT.Text, 0, , TriState.False)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grdProject_databound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSpecs.DataBound
        Try
            Dim gvr As GridViewRow = grdSpecs.TopPagerRow

            Dim lb1 As Label = DirectCast(gvr.Cells(0).FindControl("currentpage"), Label)
            lb1.Text = Convert.ToString(grdSpecs.PageIndex + 1)
            Dim page As Integer() = New Integer(10) {}
            page(0) = grdSpecs.PageIndex - 2
            page(1) = grdSpecs.PageIndex - 1
            page(2) = grdSpecs.PageIndex
            page(3) = grdSpecs.PageIndex + 1
            page(4) = grdSpecs.PageIndex + 2
            page(5) = grdSpecs.PageIndex + 3
            page(6) = grdSpecs.PageIndex + 4
            page(7) = grdSpecs.PageIndex + 5
            page(8) = grdSpecs.PageIndex + 6
            page(9) = grdSpecs.PageIndex + 7
            page(10) = grdSpecs.PageIndex + 8
            For i As Integer = 0 To 10
                If i <> 3 Then
                    If page(i) < 1 OrElse page(i) > grdSpecs.PageCount Then
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
            If grdSpecs.PageIndex = 0 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton1"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton2"), LinkButton)

                lb.Visible = False
            End If
            If grdSpecs.PageIndex = grdSpecs.PageCount - 1 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton3"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton4"), LinkButton)

                lb.Visible = False
            End If
            If grdSpecs.PageIndex > grdSpecs.PageCount - 5 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("nmore"), Label)
                lbmore.Visible = False
            End If
            If grdSpecs.PageIndex < 4 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("pmore"), Label)
                lbmore.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub lb_command(ByVal sender As Object, ByVal e As CommandEventArgs)
        grdSpecs.PageIndex = Convert.ToInt32(e.CommandArgument) - 1
        BindGridSession()
    End Sub

    Protected Sub grdProject_rowcreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSpecs.RowCreated
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim gvr As GridViewRow = e.Row
            Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p0"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command
            lb = DirectCast(gvr.Cells(0).FindControl("p1"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command
            lb = DirectCast(gvr.Cells(0).FindControl("p2"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command
            lb = DirectCast(gvr.Cells(0).FindControl("p4"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command
            lb = DirectCast(gvr.Cells(0).FindControl("p5"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command
            lb = DirectCast(gvr.Cells(0).FindControl("p6"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command
            lb = DirectCast(gvr.Cells(0).FindControl("p7"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command
            lb = DirectCast(gvr.Cells(0).FindControl("p8"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command
            lb = DirectCast(gvr.Cells(0).FindControl("p9"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command
            lb = DirectCast(gvr.Cells(0).FindControl("p10"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command
        End If
    End Sub

End Class
