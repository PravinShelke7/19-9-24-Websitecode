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
Imports AjaxControlToolkit
Partial Class Charts_SavvyPackProCharts_PopUp_VendorSelectionLine
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try

            If Not IsPostBack Then
                hidSortIdBMVendor.Value = "0"
                GetUsersList()

            End If
        Catch ex As Exception
            lblError.Text = "Error: Page_Load() " + ex.Message()
        End Try
    End Sub


    Protected Sub GetUsersList()
        Dim Ds As New DataSet
        Try
            Ds = objGetdata.GetVendorListByUserID(txtKey.Text.Trim.ToString(), Session("USERID"))
            Session("BMVendorList") = Ds
            lblRecondCnt.Text = Ds.Tables(0).Rows.Count

            If Ds.Tables(0).Rows.Count > 0 Then
                lblNOVendor.Visible = False
                grdUsers.Visible = True
                grdUsers.DataSource = Ds
                grdUsers.DataBind()
            Else
                lblNOVendor.Visible = True
                grdUsers.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:BindUser:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindUsersListUsingSession()
        Try
            Dim Dts As New DataSet
            Dts = Session("BMVendorList")
            grdUsers.DataSource = Dts
            grdUsers.DataBind()
        Catch ex As Exception
            Response.Write("Error:BindUsersListUsingSession:" + ex.Message.ToString())
        End Try
    End Sub
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
    Protected Sub grdUsers_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdUsers.RowDataBound
        Dim DsExistusr As New DataSet
        Dim lblUserID As New Label
        Dim check As New CheckBox

        Try
            DsExistusr = objGetdata.GetLinkedVendor(Session("hidRfpID"))
            For Each Gr As GridViewRow In grdUsers.Rows
                lblUserID = grdUsers.Rows(Gr.RowIndex).FindControl("lblMUsrID")
                check = grdUsers.Rows(Gr.RowIndex).FindControl("select")
                For i = 0 To DsExistusr.Tables(0).Rows.Count - 1
                    If lblUserID.Text = DsExistusr.Tables(0).Rows(i).Item("VENDORID") Then
                        check.Checked = True
                    End If
                Next
            Next
        Catch ex As Exception
            lblError.Text = "Error:grdUsers_RowDataBound" + ex.Message.ToString()
        End Try
    End Sub

#End Region

    Protected Sub btnSubmit_Click(sender As Object, e As System.EventArgs) Handles btnSubmit.Click
        Dim chk As New CheckBox
        Dim lbl As New Label
        Dim dsUser As New DataSet
        Dim dvUser As New DataView
        Dim dtUser As New DataTable
        Dim tablist As New ArrayList
        Dim vendorlst As New ArrayList
        Dim i As Integer = 0
        Try


            dsUser = DirectCast(Session("BMVendorList"), DataSet)
            dvUser = dsUser.Tables(0).DefaultView
            For Each Gr As GridViewRow In grdUsers.Rows
                chk = grdUsers.Rows(Gr.RowIndex).FindControl("select")
                lbl = grdUsers.Rows(Gr.RowIndex).FindControl("lblMUsrID")
                dvUser.RowFilter = "VENDORID='" + lbl.Text + "'"
                dtUser = dvUser.ToTable()
                If chk.Checked() Then
                    '    con(i) = dttab.Rows(0).Item("NAME").ToString()
                    tablist.Add(dtUser.Rows(0).Item("VENDORID").ToString())
                    vendorlst.Add(dtUser.Rows(0).Item("EMAILADDRESS").ToString())

                    i = i + 1
                End If
            Next
            Dim tabarr As String = String.Join(",", (tablist.ToArray()))
            Dim vendorstr As String = String.Join(",", (vendorlst.ToArray()))
            Session("GraphVendors") = tabarr
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "JSScript", "ClosePage('" + vendorstr.ToString() + "','" + tabarr.ToString() + "');", True)
        Catch ex As Exception
            Response.Write("Error:BtnSubmit:" + ex.Message.ToString())

        End Try
    End Sub
End Class
