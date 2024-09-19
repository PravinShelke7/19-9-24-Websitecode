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

Partial Class Pages_SavvyPackPro_SupplierDefault
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData
    'Investment
    Dim DesgDesc(50) As String
    Dim RSize(50) As String
    Dim ColVal(50) As String
    Dim CylVal(50) As String
    Dim SizeTotal As New Integer
    Dim SizeMTotal As New Integer
    Dim ColTotal As New Integer
    Dim CylTotal As New Integer
    'End Investment
    Dim tableCol() As String = {"", "", "BUYER", "RFP NUMBER", "RFP DESCRIPTION", "RFP DUE DATE", "TERMS OF PAYMENT", "INVENTORY COMMITMENT"}

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Try
                lblFooter.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            Catch ex As Exception
            End Try

            If Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Session("USERID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                hidSortIdSMPM.Value = "0"
                hidSortIdSpec.Value = "0"
                GetProposalDetails(hidPMGrpId.Value)
                GetSpecDetails()
                GetPageDetails()
                GetPriceCost()
                GetJobSeqDetails()
                GetTermsDetails()
            End If

            If tabSupplierManager.ActiveTabIndex = "4" Then
                GetJobSeqDetails()
            End If

        Catch ex As Exception
            lblError.Text = "Error: Page_Load() " + ex.Message()
        End Try
    End Sub

    Protected Sub tabSupplierManager_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabSupplierManager.ActiveTabChanged
        Try
            If tabSupplierManager.ActiveTabIndex = "0" Then
                'GetProposalDetails(hidPMGrpId.Value)
            ElseIf tabSupplierManager.ActiveTabIndex = "1" Then
                'GetSpecDetails()
            ElseIf tabSupplierManager.ActiveTabIndex = "2" Then
                GetPageDetails()
            ElseIf tabSupplierManager.ActiveTabIndex = "3" Then
                GetPriceCost()
            ElseIf tabSupplierManager.ActiveTabIndex = "4" Then
                GetJobSeqDetails()
            ElseIf tabSupplierManager.ActiveTabIndex = "5" Then
                GetTermsDetails()
            End If
        Catch ex As Exception
            lblError.Text = "Error: tabSupplierManager_ActiveTabChanged() " + ex.Message()
        End Try
    End Sub

#Region "Proposal Manager"

#Region "User Define Delegates"

    Protected Sub GetProposalDetails(ByVal GrpID As String)
        Dim ds As New DataSet
        Try
            ds = objGetdata.GetProposalDetails(txtSearchSMProposal.Text.Trim.ToString(), GrpID.ToString())
            Session("SMProposal") = ds
            If ds.Tables(0).Rows.Count > 0 Then
                grdPpslMngr.DataSource = ds
                grdPpslMngr.DataBind()
                grdPpslMngr.Visible = True
                trpagecountSMProposal.Visible = True
                lblMsgSMProposal.Visible = False
            Else
                lblMsgSMProposal.Visible = True
                trpagecountSMProposal.Visible = False
                grdPpslMngr.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error: GetProposalDetails() " + ex.Message()
        End Try
    End Sub

    Protected Sub BindProposalUsingSession()
        Try
            Dim Dts As New DataSet
            Dts = Session("SMProposal")
            grdPpslMngr.DataSource = Dts
            grdPpslMngr.DataBind()
            lblMsgSMProposal.Visible = False
        Catch ex As Exception
            lblError.Text = "Error: BindProposalUsingSession:" + ex.Message()
        End Try
    End Sub

#End Region

#Region "User Grid Event"

    Protected Sub grdPpslMngr_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdPpslMngr.Sorting
        Try
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim numberDiv As Integer
            Dim dsSorted As New DataSet()

            numberDiv = Convert.ToInt16(hidSortIdSMPM.Value.ToString())
            Dts = Session("SMProposal")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If

            numberDiv += 1
            hidSortIdSMPM.Value = numberDiv.ToString()
            grdPpslMngr.DataSource = dv
            grdPpslMngr.DataBind()

            dsSorted.Tables.Add(dv.ToTable())
            Session("SMProposal") = dsSorted
            lblMsgSMProposal.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlPageCountSMProposal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageCountSMProposal.SelectedIndexChanged
        Try
            grdPpslMngr.PageSize = ddlPageCountSMProposal.SelectedValue.ToString()
            BindProposalUsingSession()
        Catch ex As Exception
            Throw New Exception("Error:ddlPageCountSMProposal_SelectedIndexChanged" + ex.Message)
        End Try
    End Sub

    Protected Sub grdPpslMngr_databound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdPpslMngr.DataBound
        Try
            Dim gvr As GridViewRow = grdPpslMngr.TopPagerRow

            Dim lb1 As Label = DirectCast(gvr.Cells(0).FindControl("currentpage"), Label)
            lb1.Text = Convert.ToString(grdPpslMngr.PageIndex + 1)
            Dim page As Integer() = New Integer(10) {}
            page(0) = grdPpslMngr.PageIndex - 2
            page(1) = grdPpslMngr.PageIndex - 1
            page(2) = grdPpslMngr.PageIndex
            page(3) = grdPpslMngr.PageIndex + 1
            page(4) = grdPpslMngr.PageIndex + 2
            page(5) = grdPpslMngr.PageIndex + 3
            page(6) = grdPpslMngr.PageIndex + 4
            page(7) = grdPpslMngr.PageIndex + 5
            page(8) = grdPpslMngr.PageIndex + 6
            page(9) = grdPpslMngr.PageIndex + 7
            page(10) = grdPpslMngr.PageIndex + 8
            For i As Integer = 0 To 10
                If i <> 3 Then
                    If page(i) < 1 OrElse page(i) > grdPpslMngr.PageCount Then
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
            If grdPpslMngr.PageIndex = 0 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton1"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton2"), LinkButton)

                lb.Visible = False
            End If
            If grdPpslMngr.PageIndex = grdPpslMngr.PageCount - 1 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton3"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton4"), LinkButton)

                lb.Visible = False
            End If
            If grdPpslMngr.PageIndex > grdPpslMngr.PageCount - 5 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("nmore"), Label)
                lbmore.Visible = False
            End If
            If grdPpslMngr.PageIndex < 4 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("pmore"), Label)
                lbmore.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error: grdPpslMngr_databound() " + ex.Message()
        End Try
    End Sub

    Protected Sub lb_command(ByVal sender As Object, ByVal e As CommandEventArgs)
        grdPpslMngr.PageIndex = Convert.ToInt32(e.CommandArgument) - 1
        BindProposalUsingSession()
    End Sub

    Protected Sub grdPpslMngr_rowcreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdPpslMngr.RowCreated
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

    Protected Sub grdPpslMngr_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdPpslMngr.PageIndexChanging
        Try
            grdPpslMngr.PageIndex = e.NewPageIndex
            BindProposalUsingSession()
        Catch ex As Exception
            lblError.Text = "Error: grdPpslMngr_PageIndexChanging() " + ex.Message()
        End Try
    End Sub

#End Region

    Protected Sub btnSearchSMProposal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchSMProposal.Click
        Try
            If hidPMGrpId.Value <> "" Then
                GetProposalDetails(hidPMGrpId.Value)
            End If
        Catch ex As Exception
            lblError.Text = "Error: btnSearchSMProposal_Click() " + ex.Message()
        End Try
    End Sub

    'Protected Sub btnLinkChnage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLinkChnage.Click
    '    Try
    '        If hidPMGrpId.Value <> "" Then
    '            lnkSelGrpSMProposal.Text = hidPMGrpNm.Value
    '            GetProposalDetails(hidPMGrpId.Value)
    '        Else
    '            lblMsgSMProposal.Visible = True
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = "Error: btnLinkChnage_Click() " + ex.Message()
    '    End Try
    'End Sub

#End Region

#Region "Spec Manager"

#Region "User Define Delegates"

    Protected Sub GetSpecDetails()
        Dim ds As New DataSet
        Try
            ds = objGetdata.GetSpecDetails(txtSpecSrch.Text.Trim.ToString())
            Session("SMSpec") = ds
            If ds.Tables(0).Rows.Count > 0 Then
                grdSpec.DataSource = ds
                grdSpec.DataBind()
                grdSpec.Visible = True
                trSpecPageCount.Visible = True
                lblSpecNoFound.Visible = False
            Else
                lblSpecNoFound.Visible = True
                trSpecPageCount.Visible = False
                grdSpec.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error: GetSpecDetails() " + ex.Message()
        End Try
    End Sub

    Protected Sub BindSpecUsingSession()
        Try
            Dim Dts As New DataSet
            Dts = Session("SMSpec")
            grdSpec.DataSource = Dts
            grdSpec.DataBind()
            lblSpecNoFound.Visible = False
        Catch ex As Exception
            lblError.Text = "Error: BindSpecUsingSession:" + ex.Message()
        End Try
    End Sub

#End Region

#Region "User Grid Event"

    Protected Sub grdSpec_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdSpec.Sorting
        Try
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim numberDiv As Integer
            Dim dsSorted As New DataSet()

            numberDiv = Convert.ToInt16(hidSortIdSpec.Value.ToString())
            Dts = Session("SMSpec")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If

            numberDiv += 1
            hidSortIdSpec.Value = numberDiv.ToString()
            grdSpec.DataSource = dv
            grdSpec.DataBind()

            dsSorted.Tables.Add(dv.ToTable())
            Session("SMSpec") = dsSorted
            lblSpecNoFound.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub drpSpecPageCount_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpSpecPageCount.SelectedIndexChanged
        Try
            grdSpec.PageSize = drpSpecPageCount.SelectedValue.ToString()
            BindSpecUsingSession()
        Catch ex As Exception
            Throw New Exception("Error:drpSpecPageCount_SelectedIndexChanged" + ex.Message)
        End Try
    End Sub

    Protected Sub grdSpec_databound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSpec.DataBound
        Try
            Dim gvr As GridViewRow = grdSpec.TopPagerRow

            Dim lb1 As Label = DirectCast(gvr.Cells(0).FindControl("currentpage"), Label)
            lb1.Text = Convert.ToString(grdSpec.PageIndex + 1)
            Dim page As Integer() = New Integer(10) {}
            page(0) = grdSpec.PageIndex - 2
            page(1) = grdSpec.PageIndex - 1
            page(2) = grdSpec.PageIndex
            page(3) = grdSpec.PageIndex + 1
            page(4) = grdSpec.PageIndex + 2
            page(5) = grdSpec.PageIndex + 3
            page(6) = grdSpec.PageIndex + 4
            page(7) = grdSpec.PageIndex + 5
            page(8) = grdSpec.PageIndex + 6
            page(9) = grdSpec.PageIndex + 7
            page(10) = grdSpec.PageIndex + 8
            For i As Integer = 0 To 10
                If i <> 3 Then
                    If page(i) < 1 OrElse page(i) > grdSpec.PageCount Then
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
            If grdSpec.PageIndex = 0 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton1"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton2"), LinkButton)

                lb.Visible = False
            End If
            If grdSpec.PageIndex = grdSpec.PageCount - 1 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton3"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton4"), LinkButton)

                lb.Visible = False
            End If
            If grdSpec.PageIndex > grdSpec.PageCount - 5 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("nmore"), Label)
                lbmore.Visible = False
            End If
            If grdSpec.PageIndex < 4 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("pmore"), Label)
                lbmore.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error: grdSpec_databound() " + ex.Message()
        End Try
    End Sub

    Protected Sub lb_command_spec(ByVal sender As Object, ByVal e As CommandEventArgs)
        grdSpec.PageIndex = Convert.ToInt32(e.CommandArgument) - 1
        BindSpecUsingSession()
    End Sub

    Protected Sub grdSpec_rowcreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSpec.RowCreated
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim gvr As GridViewRow = e.Row
            Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p0"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_spec
            lb = DirectCast(gvr.Cells(0).FindControl("p1"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_spec
            lb = DirectCast(gvr.Cells(0).FindControl("p2"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_spec
            lb = DirectCast(gvr.Cells(0).FindControl("p4"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_spec
            lb = DirectCast(gvr.Cells(0).FindControl("p5"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_spec
            lb = DirectCast(gvr.Cells(0).FindControl("p6"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_spec
            lb = DirectCast(gvr.Cells(0).FindControl("p7"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_spec
            lb = DirectCast(gvr.Cells(0).FindControl("p8"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_spec
            lb = DirectCast(gvr.Cells(0).FindControl("p9"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_spec
            lb = DirectCast(gvr.Cells(0).FindControl("p10"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_spec
        End If
    End Sub

    Protected Sub grdSpec_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSpec.PageIndexChanging
        Try
            grdSpec.PageIndex = e.NewPageIndex
            BindSpecUsingSession()
        Catch ex As Exception
            lblError.Text = "Error: grdSpec_PageIndexChanging() " + ex.Message()
        End Try
    End Sub

#End Region

    Protected Sub btnSearchSMSpec_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchSMSpec.Click
        Try
            GetSpecDetails()
        Catch ex As Exception
            lblError.Text = "Error: btnSearchSMSpec_Click() " + ex.Message()
        End Try
    End Sub

    'Protected Sub btnLinkChnage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLinkChnage.Click
    '    Try
    '        If hidPMGrpId.Value <> "" Then
    '            lnkSelGrpSMProposal.Text = hidPMGrpNm.Value
    '            GetProposalDetails(hidPMGrpId.Value)
    '        Else
    '            lblMsgSMProposal.Visible = True
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = "Error: btnLinkChnage_Click() " + ex.Message()
    '    End Try
    'End Sub

    Protected Sub btnCreateGrp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateGrp.Click
        Try
            objUpIns.AddGroupName(txtGroupDe1.Text.Trim().Replace("'", "''").ToString(), txtGroupDe2.Text.Trim().Replace("'", "''").ToString(), Session("UserId"))
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Group created successfully');", True)
            txtGroupDe1.Text = ""
            txtGroupDe2.Text = ""

            trCreate.Style.Add("Display", "none")
            GetSpecDetails()
        Catch ex As Exception
            lblError.Text = "Error: btnCreateGrp_Click() " + ex.Message()
        End Try
    End Sub

#End Region

#Region "Structure Manager"

    Public Sub GetPageDetails()
        Dim ds As New DataSet
        Dim tdHeader As New TableCell
        Dim tdHeaderM As New TableCell

        Dim trHeader As New TableRow
        Dim trHeaderM As New TableRow

        Dim tdHeader1 As New TableCell
        Dim tdHeader1M As New TableCell

        Dim trHeader1 As New TableRow
        Dim trHeader1M As New TableRow

        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim dsPref As New DataSet
        Dim dsMat As New DataSet
        Dim rdb As New RadioButton
        Dim val As New Double
        Dim dvData As New DataView
        Dim dtData As New DataTable
        Dim dsCyl As New DataSet
        Dim dsDataT As New DataSet
        Dim txt As New TextBox
        Dim txtarea As New HtmlTextArea
        Dim lnkbtn As New HyperLink
        Dim count As New Integer
        Dim TotalCol As New Double
        Dim TotalSize As New Double
        Dim TotalCyl As New Double
        Dim dsEquip As DataSet
        Dim trmain As TableRow
        Dim tdmain As TableCell
        Dim hid As HiddenField
        Try
            tblJSeq.Rows.Clear()
            tblmain.Rows.Clear()

            For a As Integer = 0 To 1

                If a = 1 Then
                    trInner = New TableRow
                    tdInner = New TableCell

                    InnerTdSetting(tdInner, "20px", "")
                    lbl = New Label
                    lbl.ID = "skudes" + a.ToString()
                    lbl.Text = "SKUDES : 2kg (G2)"

                    lbl.Width = 180
                    lbl.Height = 20
                    tdInner.Controls.Add(lbl)
                    trInner.Controls.Add(tdInner)
                ElseIf a = 0 Then
                    trInner = New TableRow
                    tdInner = New TableCell

                    InnerTdSetting(tdInner, "20px", "")
                    lbl = New Label
                    lbl.ID = "sku" + a.ToString()
                    lbl.Text = "SKUID : 1111"
                    lbl.Width = 180
                    lbl.Height = 20
                    tdInner.Controls.Add(lbl)
                    trInner.Controls.Add(tdInner)
                End If
                trInner.Controls.Add(tdInner)

                tblJSeq.Controls.Add(trInner)

            Next

            trHeader = New TableRow
            For i = 1 To 3
                tdHeader = New TableCell
                tdHeader1 = New TableCell

                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "150px", "Material", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "90px", "Thickness", "1")
                        HeaderTdSetting(tdHeader1, "0", "(micron)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "90px", "Weight", "1")
                        HeaderTdSetting(tdHeader1, "0", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)

                End Select
            Next
            tblJSeq.Controls.Add(trHeader)
            tblJSeq.Controls.Add(trHeader1)

            For i = 1 To 10   'dsData.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 0 To 3
                    tdInner = New TableCell
                    If j = 0 Then
                        InnerTdSetting(tdInner, "150px", "left")
                        lbl = New Label
                        lnkbtn = New HyperLink
                        hid = New HiddenField
                        lnkbtn.CssClass = "Link"
                        lnkbtn.ID = "lnkmat_1" + i.ToString()
                        hid.Value = "lnkmat_1" + i.ToString()

                        If i = 1 Then
                            lnkbtn.Text = "PET"
                        ElseIf i = 2 Then
                            lnkbtn.Text = "PETmet"
                        ElseIf i = 3 Then
                            lnkbtn.Text = "PE"
                        Else
                            lnkbtn.Text = "Select_Material" + i.ToString()
                        End If

                        lnkbtn.Width = 80
                        lnkbtn.Height = 20
                        GetMat(lnkbtn, hid)
                        ' AddHandler lnkbtn.Click, AddressOf LinkButton_Click
                        tdInner.Controls.Add(lnkbtn)
                        trInner.Controls.Add(tdInner)

                    ElseIf j = 1 Then
                        InnerTdSetting(tdInner, "60px", "Left")
                        txt = New TextBox
                        txt.CssClass = "SmallTextBox"

                        If i = 1 Then
                            txt.Text = "12"
                        ElseIf i = 2 Then
                            txt.Text = "12"
                        ElseIf i = 3 Then
                            txt.Text = "90"
                        Else
                            txt.Text = ""
                        End If

                        txt.ID = "txtthick" + i.ToString()
                        tdInner.Controls.Add(txt)
                        trInner.Controls.Add(tdInner)
                    ElseIf j = 2 Then
                        InnerTdSetting(tdInner, "90px", "Left")
                        txt = New TextBox
                        txt.CssClass = "SmallTextBox"
                        txt.ID = "txtweight" + i.ToString()
                        txt.Text = ""
                        tdInner.Controls.Add(txt)
                        trInner.Controls.Add(tdInner)
                    End If
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If


                tblJSeq.Controls.Add(trInner)

            Next

            '''''''''''''''''''second  table''''''


            For a As Integer = 0 To 1

                If a = 1 Then
                    trInner = New TableRow
                    tdInner = New TableCell

                    InnerTdSetting(tdInner, "20px", "")
                    lbl = New Label
                    lbl.ID = "skudes" + a.ToString()
                    lbl.Text = "SKUDES : 4kg (D4)"

                    lbl.Width = 180
                    lbl.Height = 20
                    tdInner.Controls.Add(lbl)
                    trInner.Controls.Add(tdInner)
                ElseIf a = 0 Then
                    trInner = New TableRow
                    tdInner = New TableCell

                    InnerTdSetting(tdInner, "20px", "")
                    lbl = New Label
                    lbl.ID = "sku" + a.ToString()
                    lbl.Text = "SKUID : 1114"
                    lbl.Width = 180
                    lbl.Height = 20
                    tdInner.Controls.Add(lbl)
                    trInner.Controls.Add(tdInner)
                End If
                trInner.Controls.Add(tdInner)

                tblmain.Controls.Add(trInner)

            Next

            trHeaderM = New TableRow
            For i = 1 To 3
                tdHeaderM = New TableCell
                tdHeader1M = New TableCell

                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeaderM, "150px", "Material", "1")
                        HeaderTdSetting(tdHeader1M, "0", "", "1")
                        trHeaderM.Controls.Add(tdHeaderM)
                        trHeader1M.Controls.Add(tdHeader1M)
                    Case 2
                        HeaderTdSetting(tdHeaderM, "90px", "Thickness", "1")
                        HeaderTdSetting(tdHeader1M, "0", "(micron)", "1")
                        trHeaderM.Controls.Add(tdHeaderM)
                        trHeader1M.Controls.Add(tdHeader1M)
                    Case 3
                        HeaderTdSetting(tdHeaderM, "90px", "Weight", "1")
                        HeaderTdSetting(tdHeader1M, "0", "(%)", "1")
                        trHeaderM.Controls.Add(tdHeaderM)
                        trHeader1M.Controls.Add(tdHeader1M)

                End Select
            Next
            tblmain.Controls.Add(trHeaderM)
            tblmain.Controls.Add(trHeader1M)

            For i = 1 To 10   'dsData.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 0 To 3
                    tdInner = New TableCell
                    If j = 0 Then
                        InnerTdSetting(tdInner, "150px", "left")
                        lbl = New Label
                        lnkbtn = New HyperLink
                        hid = New HiddenField
                        lnkbtn.CssClass = "Link"
                        lnkbtn.ID = "lnkmat_2" + i.ToString()
                        hid.Value = "lnkmat_2" + i.ToString()

                        If i = 1 Then
                            lnkbtn.Text = "PETnoslip"
                        ElseIf i = 2 Then
                            lnkbtn.Text = "PETmet"
                        ElseIf i = 3 Then
                            lnkbtn.Text = "PE"
                        Else
                            lnkbtn.Text = "Select_Material" + i.ToString()
                        End If


                        lnkbtn.Width = 80
                        lnkbtn.Height = 20
                        GetMat(lnkbtn, hid)
                        'AddHandler lnkbtn.Click, AddressOf LinkButton_Click
                        tdInner.Controls.Add(lnkbtn)
                        trInner.Controls.Add(tdInner)

                    ElseIf j = 1 Then
                        InnerTdSetting(tdInner, "60px", "Left")
                        txt = New TextBox
                        txt.CssClass = "SmallTextBox"
                        txt.ID = "txtthick" + i.ToString()

                        If i = 1 Then
                            txt.Text = "12"
                        ElseIf i = 2 Then
                            txt.Text = "12"
                        ElseIf i = 3 Then
                            txt.Text = "100"
                        Else
                            txt.Text = ""
                        End If

                        tdInner.Controls.Add(txt)
                        trInner.Controls.Add(tdInner)
                    ElseIf j = 2 Then
                        InnerTdSetting(tdInner, "90px", "Left")
                        txt = New TextBox
                        txt.CssClass = "SmallTextBox"
                        txt.ID = "txtweight" + i.ToString()
                        txt.Text = ""
                        tdInner.Controls.Add(txt)
                        trInner.Controls.Add(tdInner)
                    End If
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                tblmain.Controls.Add(trInner)

            Next
        Catch ex As Exception

        End Try
    End Sub
    Private Sub GetMat(ByVal lnk As HyperLink, ByVal hid As HiddenField)
        Try

            Dim Path As String = String.Empty
            'Path = "../Pages/Econ1/PopUp/GetMatPopUp.aspx "
            Path = "../Econ1/PopUp/GetMatPopUp.aspx?Des=" + lnk.ClientID + "&ID=" + hid.ClientID + ""
            lnk.Font.Underline = True
            lnk.Attributes.Add("onClick", "javascript:return ShowMatPopWindow(this,'" + Path + "')")
            'lnk.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"
            'lnk.NavigateUrl = Path

            'lnk.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Investment"

    Public Sub GetJobSeqDetails()
        Dim ds As New DataSet
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim dsPref As New DataSet
        Dim dsMat As New DataSet
        Dim rdb As New RadioButton
        Dim val As New Double
        Dim dvData As New DataView
        Dim dtData As New DataTable
        Dim dsCyl As New DataSet
        Dim dsDataT As New DataSet
        Dim txt As New TextBox
        Dim txtarea As New HtmlTextArea
        Dim lnkbtn As New LinkButton
        Dim count As New Integer
        Dim TotalCol As New Double
        Dim TotalSize As New Double
        Dim TotalCyl As New Double
        Dim dsEquip As DataSet
        Try
            tblInvest.Rows.Clear()
            'dsEquip = Session("dsEquip")
            ' dsEquip = objGetData.GetEquipmentDetails()
            dsData = objGetData.GetInvestmentManagerDet(Session("USERID"))
            If hidRowNum.Value <> "2" Then
                count = hidRowNum.Value
            Else
                If dsData.Tables(0).Rows.Count > 0 Then
                    count = dsData.Tables(0).Rows.Count + 1
                    hidRowNum.Value = dsData.Tables(0).Rows.Count + 1
                Else
                    count = hidRowNum.Value
                End If
            End If



            trHeader = New TableRow
            For i = 1 To 3
                tdHeader = New TableCell
                tdHeader1 = New TableCell

                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "200px", "Description", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "180px", "Amount", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "180px", "Comments", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)

                End Select
            Next
            tblInvest.Controls.Add(trHeader)
            tblInvest.Controls.Add(trHeader1)

            For i = 1 To count   'dsData.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 0 To 3
                    tdInner = New TableCell
                    If j = 0 Then
                        InnerTdSetting(tdInner, "20px", "Center")
                        txt = New TextBox
                        txt.CssClass = "MatSmallTextBox"
                        txt.ID = "txtDes" + i.ToString()
                        If i = count Then
                            tdInner.Text = "Total"
                        Else
                            If DesgDesc(i - 1) <> "" Then
                                txt.Text = DesgDesc(i - 1)
                            Else

                                If i > count Then
                                    txt.Text = ""
                                Else
                                    If dsData.Tables(0).Rows.Count > i Then
                                        If dsData.Tables(0).Rows(i - 1).Item("imdescription").ToString() <> "" Then
                                            txt.Text = dsData.Tables(0).Rows(i - 1).Item("imdescription").ToString()
                                        Else
                                            txt.Text = "description " + i.ToString()
                                        End If
                                    Else
                                        txt.Text = "description " + i.ToString()
                                    End If
                                End If
                            End If
                            tdInner.Controls.Add(txt)
                        End If

                    ElseIf j = 1 Then
                        InnerTdSetting(tdInner, "60px", "Right")
                        txt = New TextBox
                        txt.CssClass = "Amount"
                        txt.ID = "txtRSize" + i.ToString()
                        If i = count Then
                            tdInner.Text = TotalSize.ToString()
                            SizeTotal = TotalSize
                        Else
                            If RSize(i - 1) <> "" Then
                                txt.Text = RSize(i - 1)
                            Else
                                If i = count Then
                                    If i <> 1 Then

                                        txt.Text = ""


                                    Else
                                        txt.Text = ""
                                    End If

                                Else
                                    If dsData.Tables(0).Rows.Count >= i Then
                                        If dsData.Tables(0).Rows(i - 1).Item("amounts").ToString() <> "" Then
                                            txt.Text = dsData.Tables(0).Rows(i - 1).Item("amounts").ToString()
                                            TotalSize = TotalSize + dsData.Tables(0).Rows(i - 1).Item("amounts")
                                        Else
                                            txt.Text = ""
                                        End If
                                    Else
                                        txt.Text = ""
                                    End If
                                End If
                            End If
                            'txt.Text = "Design " + (i + 1).ToString()
                            tdInner.Controls.Add(txt)
                        End If


                    ElseIf j = 2 Then
                        InnerTdSetting(tdInner, "90px", "Right")
                        txt = New TextBox
                        txt.TextMode = TextBoxMode.MultiLine
                        txt.CssClass = "Comments"
                        txt.ID = "txtCol" + i.ToString()
                        If i = count Then

                        Else
                            If ColVal(i - 1) <> "" Then
                                txt.Text = ColVal(i - 1)
                            Else
                                If i = count Then
                                    If i <> 1 Then

                                        txt.Text = ""


                                    Else
                                        txt.Text = ""
                                    End If
                                    'If i <> 1 Then
                                    '    If ColVal(i - 2) <> "" Then
                                    '        txt.Text = ColVal(i - 2)
                                    '    Else
                                    '        txt.Text = ""
                                    '    End If
                                    'Else
                                    '    txt.Text = ""
                                    'End If
                                Else
                                    If dsData.Tables(0).Rows.Count >= i Then
                                        If dsData.Tables(0).Rows(i - 1).Item("comments").ToString() <> "" Then
                                            txt.Text = dsData.Tables(0).Rows(i - 1).Item("comments").ToString()
                                            'TotalCol = TotalCol + dsData.Tables(0).Rows(i - 1).Item("COLVAL")
                                        Else
                                            txt.Text = ""
                                        End If
                                    Else
                                        txt.Text = ""
                                    End If
                                End If
                            End If
                            'txt.Text = "Design " + (i + 1).ToString()
                            tdInner.Controls.Add(txt)
                        End If


                    Else
                        InnerTdSetting(tdInner, "", "Left")
                        InnerTdSetting(tdInner, "", "Center")
                        lbl = New Label
                        lnkbtn = New LinkButton
                        lnkbtn.ID = "lnkAdd" + i.ToString()
                        lnkbtn.Text = "Add More+"
                        lnkbtn.Width = 80
                        lnkbtn.Height = 20
                        'lbl.ID = "lbl" + i.ToString()

                        If i = count Then

                            tdInner.Style.Add("display", "")

                        Else
                            tdInner.Style.Add("display", "none")

                        End If
                        AddHandler lnkbtn.Click, AddressOf LinkButton_Click
                        tdInner.Controls.Add(lnkbtn)
                        trInner.Controls.Add(tdInner)
                    End If
                    trInner.Controls.Add(tdInner)
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If

                If i > count Then
                    trInner.Style.Add("display", "none")
                End If

                tblInvest.Controls.Add(trInner)
            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LinkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim count As Integer = Convert.ToInt32(hidRowNum.Value.ToString())
            For i = 0 To count
                DesgDesc(i) = Request.Form("tabSupplierManager_tabSMInvest_txtDes" + (i + 1).ToString() + "")
                RSize(i) = Request.Form("tabSupplierManager_tabSMInvest_txtRSize" + (i + 1).ToString() + "")
                ColVal(i) = Request.Form("tabSupplierManager_tabSMInvest_txtCol" + (i + 1).ToString() + "")
            Next
            'Session("Linkbuttonclicked") = "Addmore"
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hidRowNum.Value.ToString())
            numberDiv += 1
            hidRowNum.Value = numberDiv.ToString()
            GetJobSeqDetails()
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub btnupdate_Click(sender As Object, e As System.EventArgs) Handles btnupdate.Click
    '    Dim count As Integer = Convert.ToInt32(hidRowNum.Value.ToString())
    '    Dim des(count) As String
    '    Dim amt(count) As String
    '    Dim cmt(count) As String
    '    For i = 0 To count
    '        des(i) = Request.Form("txtDes" + (i + 1).ToString() + "")
    '        amt(i) = Request.Form("txtRSize" + (i + 1).ToString() + "")
    '        cmt(i) = Request.Form("txtCol" + (i + 1).ToString() + "")

    '    Next
    '    objUpIns.UpdateInvestment(des, amt, cmt, Session("USERID"))
    'End Sub

#End Region

#Region "Price&Cost Manage"

    Protected Sub GetPriceCost()
        Dim ds As New DataSet
        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow

        Dim trInner As New TableRow

        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell


        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim ddl As New DropDownList
        Dim strVol As Double = 0.0
        Try
            ds = objGetData.GetPriceCost()
            lblSKU.Text = "1111" 'ds.Tables(0).Rows(0).Item("SKUID".ToString() + "").ToString()
            lblSKUDesc.Text = ds.Tables(0).Rows(0).Item("SKUDESC".ToString() + "").ToString()


            For i = 1 To 5
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "200px", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/M" + ")"
                        HeaderTdSetting(tdHeader, "120px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        HeaderTdSetting(tdHeader, "90px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        Header2TdSetting(tdHeader2, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "90px", "By Weight", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 5
                        Title = "(" + ds.Tables(0).Rows(0).Item("PUN").ToString() + ")"
                        HeaderTdSetting(tdHeader, "90px", "By Volume", "1")
                        HeaderTdSetting(tdHeader1, "90px", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                End Select
            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader1)


            For i = 1 To 16
                If i <> 17 Then
                    trInner = New TableRow
                    For j = 1 To 5
                        tdInner = New TableCell
                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "", "Left")
                                If i = 1 Or i = 2 Then
                                    tdInner.Text = "<b>" + ds.Tables(0).Rows(0).Item("PS" + i.ToString() + "").ToString() + "</b>"
                                ElseIf i = 8 Or i = 12 Or i = 16 Then
                                    tdInner.Text = "<b>" + ds.Tables(0).Rows(0).Item("PDES" + i.ToString() + "").ToString() + "</b>"
                                Else
                                    tdInner.Text = "<span style='margin-left:20px;'><b>  " + ds.Tables(0).Rows(0).Item("PDES" + i.ToString() + "").ToString() + "</b></span>"
                                End If

                                trInner.Controls.Add(tdInner)
                            Case 2
                                InnerTdSetting(tdInner, "", "Left")
                                lbl = New Label()
                                If i = 1 Then
                                    txt = New TextBox
                                    txt.ID = "txtP" + i.ToString()
                                    txt.Width = 100
                                    txt.CssClass = "SmallTextBox"
                                    tdInner.Controls.Add(txt)
                                    trInner.Controls.Add(tdInner)
                                ElseIf i = 2 Then
                                    txt = New TextBox
                                    txt.ID = "txtS" + i.ToString()
                                    txt.Width = 100
                                    txt.CssClass = "SmallTextBox"
                                    tdInner.Controls.Add(txt)
                                    trInner.Controls.Add(tdInner)
                                Else
                                    'lbl = New Label()
                                    '' tdInner.Text = "<b>" + ds.Tables(0).Rows(0).Item("PS" + i.ToString() + "").ToString() + "</b>"
                                    'lbl.Text = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()))
                                    'tdInner.Controls.Add(lbl)
                                    'trInner.Controls.Add(tdInner)
                                    txt = New TextBox
                                    'txt.ID = "txtS" + i.ToString()
                                    txt.Width = 100
                                    txt.CssClass = "SmallTextBox"
                                    tdInner.Controls.Add(txt)
                                    trInner.Controls.Add(tdInner)
                                End If


                            Case 3
                                Dim percentage As New Decimal
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label()
                                If i > 2 Then
                                    lbl.Text = ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()
                                Else
                                    lbl.Text = ""
                                End If

                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 4
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perwt As New Decimal
                                lbl = New Label()
                                If i > 2 Then
                                    lbl.Text = ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()
                                Else
                                    lbl.Text = ""
                                End If

                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 5
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perunit As New Decimal
                                lbl = New Label()
                                If i > 2 Then
                                    lbl.Text = ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()
                                Else
                                    lbl.Text = ""
                                End If

                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                        End Select
                    Next


                    tblComparision.Controls.Add(trInner)
                End If
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
            Next
        Catch ex As Exception
            lblError.Text = "Error:GetPriceCost:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindUnitType(ByVal ddl As DropDownList, ByVal unitType As String)
        Dim objGetData As New E1GetData.Selectdata()
        Dim dts As New DataSet()
        Try
            dts = objGetData.getUnits("97047")
            ddl.CssClass = "DropDownConT"
            ddl.ID = "UNITTYPE"
            'Binding Dropdown
            With ddl
                .DataSource = dts
                .DataTextField = "UNIT"
                .DataValueField = "VAL"
                .DataBind()
            End With
            ddl.SelectedValue = unitType
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Terms Manager"

    Public Sub GetTermsDetails()
        Dim ds As New DataSet
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim dsPref As New DataSet
        Dim dsMat As New DataSet
        Dim rdb As New RadioButton
        Dim val As New Double
        Dim dvData As New DataView
        Dim dtData As New DataTable
        Dim dsCyl As New DataSet
        Dim dsDataT As New DataSet
        Dim txt As New TextBox
        Dim txtarea As New HtmlTextArea
        Dim lnkbtn As New LinkButton
        Dim count As New Integer
        Dim TotalCol As New Double
        Dim TotalSize As New Double
        Dim TotalCyl As New Double
        Try
            tblterm.Rows.Clear()
            dsData = objGetdata.GetTermsManagerDet(1)

            For a As Integer = 0 To dsData.Tables(0).Rows.Count - 1
                ' trInner = New TableRow
                For k As Integer = 2 To dsData.Tables(0).Columns().Count - 1
                    trInner = New TableRow
                    For i = 0 To 1   'dsData.Tables(0).Rows.Count - 1
                        If dsData.Tables(0).Rows(a).Item(k).ToString() <> "" Then
                            If i = 0 Then
                                tdInner = New TableCell
                                lbl = New Label
                                InnerTdSetting(tdInner, "350px", "left")

                                lbl.Font.Bold = True
                                lbl.CssClass = "NormalLable_Term"
                                lbl.ID = "lbl_" + k.ToString()
                                lbl.Text = tableCol(k) + "  : " 'dsData.Tables(0).Columns(k).ColumnName.ToString() + "  : "
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                            ElseIf i = 1 Then
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "450px", "left")

                                lbl = New Label
                                lbl.CssClass = "NormalLable_Term"
                                lbl.ID = "lbl_" + k.ToString()
                                lbl.Text = dsData.Tables(0).Rows(a).Item(k).ToString()
                                tdInner.Controls.Add(lbl)
                                'HeaderTdSetting(tdInner, "200px", dsData.Tables(0).Rows(a).Item(k).ToString(), "1")
                                trInner.Controls.Add(tdInner)
                            End If
                            'If (i Mod 2 = 0) Then
                            '    trInner.CssClass = "AlterNateColor1"
                            'Else
                            '    trInner.CssClass = "AlterNateColor2"
                            'End If
                            tblterm.Controls.Add(trInner)
                        End If
                    Next
                Next
            Next
        Catch ex As Exception
            lblError.Text = "Error: GetTermsDetails() " + ex.Message()
        End Try
    End Sub

#End Region

#Region "Formatting"

    Protected Sub HeaderTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal ColSpan As String)
        Try
            Td.Text = HeaderText
            Td.ColumnSpan = ColSpan
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.CssClass = "TdHeading"
            Td.Height = 20
            Td.Font.Size = 10
            Td.Font.Bold = True
            Td.HorizontalAlign = HorizontalAlign.Center
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Header2TdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal ColSpan As String)
        Try
            Td.Text = HeaderText
            Td.ColumnSpan = ColSpan
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.CssClass = "TdHeading"
            Td.Font.Size = 8
            Td.Height = 20
            Td.HorizontalAlign = HorizontalAlign.Center
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub InnerTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal Align As String)
        Try
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.Style.Add("text-align", Align)
            If Align = "Left" Then
                Td.Style.Add("padding-left", "5px")
            End If
            If Align = "Right" Then
                Td.Style.Add("padding-right", "5px")
            End If
            Td.Style.Add("font-size", "13px")
        Catch ex As Exception

        End Try
    End Sub

#End Region
End Class
