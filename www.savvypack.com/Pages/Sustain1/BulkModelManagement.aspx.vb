Imports System.Web.UI.HtmlTextWriter
Imports System.Data
Imports S1GetData
Imports S1UpInsData
Imports AjaxControlToolkit
Partial Class Pages_Sustain1_BulkModelManagement
    Inherits System.Web.UI.Page
    Dim objGetData As New S1GetData.Selectdata
    Dim PageNm As String = String.Empty
    Dim SCaseID As String = String.Empty

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            lblTag.Text = "Copyright 1997 - " + Now.Year().ToString() + " Savvypack Corporation."
            PageNm = Request.QueryString("PageNm").ToString()
            SCaseID = Request.QueryString("SCaseID").ToString()
            If Not IsPostBack Then
                GetGroups()
                bindGroupGrid()
                hidSortId.Value = "0"
                hidSortIdC.Value = "0"
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load " + ex.Message.ToString()
        End Try
    End Sub

#Region "Groups"

    Protected Sub GetGroups()
        Dim ds As New DataSet
        Dim lst As New ListItem()
        Dim lst1 As New ListItem()
        Try
            ds = objGetData.GetGroupsName(Session("USERID"), "", "")
            ddlGroup.AppendDataBoundItems = True
            lst1.Text = "--All Groups--"
            lst1.Value = 0
            lst.Text = "--All Cases--"
            lst.Value = -1
            ddlGroup.Items.Add(lst)
            ddlGroup.Items.Add(lst1)
            With ddlGroup
                .DataSource = ds
                .DataTextField = "DES1"
                .DataValueField = "GROUPID"
                .DataBind()
            End With

        Catch ex As Exception
            ErrorLable.Text = "Error:GetGroups" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub bindGroupGrid()
        Dim dsG As New DataSet()
        Dim ds As New DataSet()
        Dim j As Integer
        Dim grpCount As Integer = 0
        Try
            If ddlGroup.SelectedValue <> "0" And ddlGroup.SelectedValue <> "-1" Then
                dsG = objGetData.GetGroupsName(Session("USERID"), ddlGroup.SelectedValue, "GroupDesc")
                txtGrpDesc.Text = dsG.Tables(0).Rows(0).Item("DES2").ToString()
            Else
                txtGrpDesc.Text = ""
            End If

            ds = objGetData.GetGroupCasesBMM(ddlGroup.SelectedValue, Session("USERID"))
            lblRecondCnt.Text = ds.Tables(0).Rows.Count
            Session("S1BTGroupData") = ds
            If ds.Tables(0).Rows.Count > 0 Then
                GrdCase.Visible = True
                GrdCase.DataSource = ds
                GrdCase.DataBind()
                '  trRow.Visible = False
                lblMsg.Visible = False
            Else
                ' trRow.Visible = True
                lblMsg.Visible = True
                GrdCase.Visible = False
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:bindGroupGrid" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindGridSessionG()
        Try
            Dim Dts As New DataSet
            Dts = Session("S1BTGroupData")
            GrdCase.DataSource = Dts
            GrdCase.DataBind()
        Catch ex As Exception
            ErrorLable.Text = "Error:BindGridSessionG" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlGroup.SelectedIndexChanged
        Try
            bindGroupGrid()
        Catch ex As Exception
            ErrorLable.Text = "Error:ddlGroup_SelectedIndexChanged " + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GrdCase_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrdCase.PageIndexChanging
        Try
            GrdCase.PageIndex = e.NewPageIndex
            BindGridSessionG()
        Catch ex As Exception
            ErrorLable.Text = "Error:GrdCase_PageIndexChanging" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlSizeG_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSizeG.SelectedIndexChanged
        Try
            GrdCase.PageSize = ddlSizeG.SelectedItem.ToString()
            BindGridSessionG()
        Catch ex As Exception
            Throw New Exception("ddlSizeG_SelectedIndexChanged" + ex.Message)
        End Try
    End Sub

    Protected Sub GrdCase_databound(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrdCase.DataBound
        Try
            Dim gvr As GridViewRow = GrdCase.TopPagerRow

            Dim lb1 As Label = DirectCast(gvr.Cells(0).FindControl("currentpage"), Label)
            lb1.Text = Convert.ToString(GrdCase.PageIndex + 1)
            Dim page As Integer() = New Integer(10) {}
            page(0) = GrdCase.PageIndex - 2
            page(1) = GrdCase.PageIndex - 1
            page(2) = GrdCase.PageIndex
            page(3) = GrdCase.PageIndex + 1
            page(4) = GrdCase.PageIndex + 2
            page(5) = GrdCase.PageIndex + 3
            page(6) = GrdCase.PageIndex + 4
            page(7) = GrdCase.PageIndex + 5
            page(8) = GrdCase.PageIndex + 6
            page(9) = GrdCase.PageIndex + 7
            page(10) = GrdCase.PageIndex + 8
            For i As Integer = 0 To 10
                If i <> 3 Then
                    If page(i) < 1 OrElse page(i) > GrdCase.PageCount Then
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
            If GrdCase.PageIndex = 0 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton1"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton2"), LinkButton)

                lb.Visible = False
            End If
            If GrdCase.PageIndex = GrdCase.PageCount - 1 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton3"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton4"), LinkButton)

                lb.Visible = False
            End If
            If GrdCase.PageIndex > GrdCase.PageCount - 5 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("nmore"), Label)
                lbmore.Visible = False
            End If
            If GrdCase.PageIndex < 4 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("pmore"), Label)
                lbmore.Visible = False
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:GrdCase_databound" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub lb_command1(ByVal sender As Object, ByVal e As CommandEventArgs)
        GrdCase.PageIndex = Convert.ToInt32(e.CommandArgument) - 1
        BindGridSessionG()
    End Sub

    Protected Sub GrdCase_rowcreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GrdCase.RowCreated
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim gvr As GridViewRow = e.Row
            Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p0"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command1
            lb = DirectCast(gvr.Cells(0).FindControl("p1"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command1
            lb = DirectCast(gvr.Cells(0).FindControl("p2"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command1
            lb = DirectCast(gvr.Cells(0).FindControl("p4"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command1
            lb = DirectCast(gvr.Cells(0).FindControl("p5"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command1
            lb = DirectCast(gvr.Cells(0).FindControl("p6"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command1
            lb = DirectCast(gvr.Cells(0).FindControl("p7"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command1
            lb = DirectCast(gvr.Cells(0).FindControl("p8"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command1
            lb = DirectCast(gvr.Cells(0).FindControl("p9"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command1
            lb = DirectCast(gvr.Cells(0).FindControl("p10"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command1
        End If
    End Sub

    Protected Sub GrdCase_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GrdCase.Sorting
        Dim Dts As New DataSet
        Dim dv As DataView
        Dim numberDiv As Integer
        Dim dsSortedGD As New DataSet()
        Try
            numberDiv = Convert.ToInt16(hidSortId.Value.ToString())
            Dts = Session("S1BTGroupData")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            hidSortId.Value = numberDiv.ToString()
            GrdCase.DataSource = dv
            GrdCase.DataBind()

            dsSortedGD.Tables.Add(dv.ToTable())
            Session("S1BTGroupData") = dsSortedGD

        Catch ex As Exception
            ErrorLable.Text = "Error:GrdCase_Sorting" + ex.Message.ToString()
        End Try
    End Sub

#End Region

#Region "PopUp Extender Control"

    Protected Sub btnTransfer_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTransfer.Click
        Try
            hdnUpdate.Value = 1
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "CloseBulkManage", "ClosePopUp();", True)
        Catch ex As Exception
            ErrorLable.Text = "Error:btnTransfer_Click" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Try
            mp1.Hide()
        Catch ex As Exception
            ErrorLable.Text = "Error:btnCancel_Click" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnTransfCalc_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTransfCalc.Click
        Try
            hdnUpdate.Value = 1
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "CloseBulkManage", "ClosePopUp1();", True)
        Catch ex As Exception
            ErrorLable.Text = "Error:btnTransfCalc_Click" + ex.Message.ToString()
        End Try
    End Sub

#End Region

    Protected Sub btnSubmit2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit2.Click
        Dim strchk As String = String.Empty
        Dim check As New CheckBox
        Try
            Session("S1CaseIdString") = Nothing
            For Each Gr As GridViewRow In GrdCase.Rows
                check = GrdCase.Rows(Gr.RowIndex).FindControl("SelectGroup")
                If check.Checked = True Then
                    Dim CaseId As Integer = Convert.ToInt32(GrdCase.DataKeys(Gr.RowIndex).Values(0))
                    strchk += CaseId.ToString() + ","
                End If
            Next

            If strchk <> "" Then
                strchk = strchk.Trim().Remove(strchk.Length - 1)
                Session("S1CaseIdString") = strchk
                lblInfo.Text = "All of the changeable values of the <b>" + PageNm.ToString() + "</b> screen from model #<b>" + SCaseID.ToString() + "</b> are going to be transferred to the following models: "
                lblList.Text = Session("S1CaseIdString")
                lblOption.Text = "You have two options:</br>1.Transfer the variables only(take less than a minute).</br>2.Transfer and re-calculate each model(may takes several minutes).</br>You will be asked to confirm your following choice after you click one of the buttons."
                lblWarning.Text = "Note: Once you confirm your choice, this screen will close and you will be returned to the original page, where the transfer(and calculate if selected) process will run until complete."
                mp1.Show()
            Else
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "CloseBulkManage", "alert('Please select at least one case.');", True)
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:btnSubmit2_Click" + ex.Message.ToString()
        End Try
    End Sub

End Class
