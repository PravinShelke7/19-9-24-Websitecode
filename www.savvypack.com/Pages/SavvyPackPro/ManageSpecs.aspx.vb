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
Partial Class Pages_SavvyPackPro_ManageSpecs
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            Try
                lblFooter.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            Catch ex As Exception
            End Try

            'If Session("SBack") = Nothing Then
            '    Dim obj As New CryptoHelper
            '    Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            'End If

            If Session("USERID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                hidSortIdSMPM.Value = "0"
                hidSortIdSpec.Value = "0"
                hidSortIdBMVendor.Value = "0"
                hidSortIdGrp.Value = "0"
                hidSortIdGrpMngr.Value = "0"
                hidSortIdMGroup.Value = "0"
                'GetSpecDetails()
                BindMasterGrp()
                BindMTypeDetails()
                ChkExistingRfp()
                'GetAllGrpDetails()
                'GetGrpManagerData()
                GetSkuDetails()
              
            End If

        Catch ex As Exception
            lblError.Text = "Error: Page_Load() " + ex.Message()
        End Try
    End Sub

    Protected Sub ChkExistingRfp()
        Dim DsCheck As New DataSet()
        Try
            DsCheck = objGetdata.GetMasterGrp(Session("USERID"))
            If DsCheck.Tables(0).Rows.Count > 0 Then
                lblNoMasterGrp.Visible = False
                GetLatestDetails(DsCheck.Tables(0).Rows(0).Item("MGROUPID").ToString())
            Else
                lblNoMasterGrp.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = "Error: ChkExistingRfp() " + ex.Message()
        End Try
    End Sub

    Protected Sub GetLatestDetails(ByVal MasterGrpID As String)
        Dim DsMasterDet As New DataSet()
        Try
            If MasterGrpID <> "" Or MasterGrpID <> "0" Then
                DsMasterDet = objGetdata.GetMasterGrpDet(MasterGrpID)
                Session("DsMasterDet") = DsMasterDet
                If DsMasterDet.Tables(0).Rows.Count > 0 Then
                    lnkSelMasterGrp.Text = DsMasterDet.Tables(0).Rows(0).Item("DESCRIPTION").ToString()
                    hidMasterGrpDes.Value = DsMasterDet.Tables(0).Rows(0).Item("DESCRIPTION").ToString()
                    hidMasterGrpID.Value = MasterGrpID
                    hidMTypeID.Value = DsMasterDet.Tables(0).Rows(0).Item("MTYPEID").ToString()
                    loadFunctions()
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('System isn't able to find ID. Please try again.');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error: GetLatestDetails() " + ex.Message()
        End Try
    End Sub

    Protected Sub loadFunctions()
        Try
            BindGrpByMasterID()
            GetGrpManagerData()
        Catch ex As Exception
            lblError.Text = "Error: loadFunctions() " + ex.Message()
        End Try
    End Sub

    Protected Sub BindMasterGrp()
        Dim ds As New DataSet
        Try
            ds = objGetdata.GetMasterGrp(Session("USERID"))
            With ddlMGrp
                .DataValueField = "MGROUPID"
                .DataTextField = "DESCRIPTION"
                .DataSource = ds
                .DataBind()
            End With
        Catch ex As Exception
            lblError.Text = "Error: BindMasterGrp() " + ex.Message()
        End Try
    End Sub

    Protected Sub BindMTypeDetails()
        Dim Dts As New DataSet
        Dim lst As New ListItem
        Try
            Dts = objGetdata.GetMTypeD()

            If Dts.Tables(0).Rows.Count > 0 Then
                ddlMType.DataSource = Dts
                ddlMType.DataValueField = "MTYPEID"
                ddlMType.DataTextField = "DESCRIPTION"
                ddlMType.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = "Error:BindMTypeDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub BindGrpByMasterID()
        Dim Dts As New DataSet
        Dim lst As New ListItem
        Try
            Dts = objGetdata.GetSubGrpByMaster(hidMasterGrpID.Value, Session("USERID").ToString())

            If Dts.Tables(0).Rows.Count > 0 Then
                ddlGrpByMGId.DataSource = Dts
                ddlGrpByMGId.DataValueField = "GRPID"
                ddlGrpByMGId.DataTextField = "GRPNM"
                ddlGrpByMGId.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = "Error:BindMTypeDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    '#Region "Spec Manager"

    '#Region "User Define Delegates"

    '    Protected Sub GetSpecDetails()
    '        Dim ds As New DataSet
    '        Try
    '            ds = objGetdata.GetSpecByUserID(txtSpecSrch.Text.Trim.ToString(), Session("USERID"), Session("LicenseNo"))

    '            Session("SMSpec") = ds
    '            If ds.Tables(0).Rows.Count > 0 Then
    '                grdSpec.DataSource = ds
    '                grdSpec.DataBind()
    '                grdSpec.Visible = True
    '                trSpecPageCount.Visible = True
    '                lblSpecNoFound.Visible = False
    '                BindLink()
    '            Else
    '                lblSpecNoFound.Visible = True
    '                trSpecPageCount.Visible = False
    '                grdSpec.Visible = False
    '            End If
    '        Catch ex As Exception
    '            lblError.Text = "Error: GetSpecDetails() " + ex.Message()
    '        End Try
    '    End Sub
    '    Protected Sub BindLink()
    '        Dim lblUserID As New Label
    '        Try
    '            For Each Gr As GridViewRow In grdSpec.Rows
    '                lblUserID = grdSpec.Rows(Gr.RowIndex).FindControl("lblUserID")

    '                If Session("UserId") = lblUserID.Text Then
    '                    Gr.Enabled = True
    '                Else
    '                    Gr.Cells(2).Enabled = False
    '                    Gr.BackColor = Drawing.Color.LightGray
    '                End If

    '            Next
    '        Catch ex As Exception
    '            lblError.Text = "Error: BindLink:" + ex.Message()
    '        End Try
    '    End Sub
    '    Protected Sub BindSpecUsingSession()
    '        Try
    '            Dim Dts As New DataSet
    '            Dts = Session("SMSpec")
    '            grdSpec.DataSource = Dts
    '            grdSpec.DataBind()
    '            lblSpecNoFound.Visible = False
    '        Catch ex As Exception
    '            lblError.Text = "Error: BindSpecUsingSession:" + ex.Message()
    '        End Try
    '    End Sub

    '#End Region

    '#Region "User Grid Event"

    '    Protected Sub grdSpec_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdSpec.Sorting
    '        Try
    '            Dim Dts As New DataSet
    '            Dim dv As DataView
    '            Dim numberDiv As Integer
    '            Dim dsSorted As New DataSet()

    '            numberDiv = Convert.ToInt16(hidSortIdSpec.Value.ToString())
    '            Dts = Session("SMSpec")
    '            dv = Dts.Tables(0).DefaultView

    '            If ((numberDiv Mod 2) = 0) Then
    '                dv.Sort = e.SortExpression + " " + "DESC"
    '            Else
    '                dv.Sort = e.SortExpression + " " + "ASC"
    '            End If

    '            numberDiv += 1
    '            hidSortIdSpec.Value = numberDiv.ToString()
    '            grdSpec.DataSource = dv
    '            grdSpec.DataBind()

    '            dsSorted.Tables.Add(dv.ToTable())
    '            Session("SMSpec") = dsSorted
    '            lblSpecNoFound.Visible = False
    '        Catch ex As Exception

    '        End Try
    '    End Sub

    '    Protected Sub drpSpecPageCount_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpSpecPageCount.SelectedIndexChanged
    '        Try
    '            grdSpec.PageSize = drpSpecPageCount.SelectedValue.ToString()
    '            BindSpecUsingSession()
    '        Catch ex As Exception
    '            Throw New Exception("Error:drpSpecPageCount_SelectedIndexChanged" + ex.Message)
    '        End Try
    '    End Sub

    '    Protected Sub grdSpec_databound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSpec.DataBound
    '        Try
    '            Dim gvr As GridViewRow = grdSpec.TopPagerRow

    '            Dim lb1 As Label = DirectCast(gvr.Cells(0).FindControl("currentpage"), Label)
    '            lb1.Text = Convert.ToString(grdSpec.PageIndex + 1)
    '            Dim page As Integer() = New Integer(10) {}
    '            page(0) = grdSpec.PageIndex - 2
    '            page(1) = grdSpec.PageIndex - 1
    '            page(2) = grdSpec.PageIndex
    '            page(3) = grdSpec.PageIndex + 1
    '            page(4) = grdSpec.PageIndex + 2
    '            page(5) = grdSpec.PageIndex + 3
    '            page(6) = grdSpec.PageIndex + 4
    '            page(7) = grdSpec.PageIndex + 5
    '            page(8) = grdSpec.PageIndex + 6
    '            page(9) = grdSpec.PageIndex + 7
    '            page(10) = grdSpec.PageIndex + 8
    '            For i As Integer = 0 To 10
    '                If i <> 3 Then
    '                    If page(i) < 1 OrElse page(i) > grdSpec.PageCount Then
    '                        Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p" & Convert.ToString(i)), LinkButton)
    '                        lb.Visible = False
    '                    Else
    '                        Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p" & Convert.ToString(i)), LinkButton)
    '                        lb.Text = Convert.ToString(page(i))

    '                        lb.CommandName = "pageno"

    '                        lb.CommandArgument = lb.Text
    '                    End If
    '                End If
    '            Next
    '            If grdSpec.PageIndex = 0 Then
    '                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton1"), LinkButton)
    '                lb.Visible = False
    '                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton2"), LinkButton)

    '                lb.Visible = False
    '            End If
    '            If grdSpec.PageIndex = grdSpec.PageCount - 1 Then
    '                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton3"), LinkButton)
    '                lb.Visible = False
    '                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton4"), LinkButton)

    '                lb.Visible = False
    '            End If
    '            If grdSpec.PageIndex > grdSpec.PageCount - 5 Then
    '                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("nmore"), Label)
    '                lbmore.Visible = False
    '            End If
    '            If grdSpec.PageIndex < 4 Then
    '                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("pmore"), Label)
    '                lbmore.Visible = False
    '            End If
    '        Catch ex As Exception
    '            lblError.Text = "Error: grdSpec_databound() " + ex.Message()
    '        End Try
    '    End Sub

    '    Protected Sub lb_command_spec(ByVal sender As Object, ByVal e As CommandEventArgs)
    '        grdSpec.PageIndex = Convert.ToInt32(e.CommandArgument) - 1
    '        BindSpecUsingSession()
    '    End Sub

    '    Protected Sub grdSpec_rowcreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSpec.RowCreated
    '        If e.Row.RowType = DataControlRowType.Pager Then
    '            Dim gvr As GridViewRow = e.Row
    '            Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p0"), LinkButton)
    '            AddHandler (lb.Command), AddressOf lb_command_spec
    '            lb = DirectCast(gvr.Cells(0).FindControl("p1"), LinkButton)
    '            AddHandler (lb.Command), AddressOf lb_command_spec
    '            lb = DirectCast(gvr.Cells(0).FindControl("p2"), LinkButton)
    '            AddHandler (lb.Command), AddressOf lb_command_spec
    '            lb = DirectCast(gvr.Cells(0).FindControl("p4"), LinkButton)
    '            AddHandler (lb.Command), AddressOf lb_command_spec
    '            lb = DirectCast(gvr.Cells(0).FindControl("p5"), LinkButton)
    '            AddHandler (lb.Command), AddressOf lb_command_spec
    '            lb = DirectCast(gvr.Cells(0).FindControl("p6"), LinkButton)
    '            AddHandler (lb.Command), AddressOf lb_command_spec
    '            lb = DirectCast(gvr.Cells(0).FindControl("p7"), LinkButton)
    '            AddHandler (lb.Command), AddressOf lb_command_spec
    '            lb = DirectCast(gvr.Cells(0).FindControl("p8"), LinkButton)
    '            AddHandler (lb.Command), AddressOf lb_command_spec
    '            lb = DirectCast(gvr.Cells(0).FindControl("p9"), LinkButton)
    '            AddHandler (lb.Command), AddressOf lb_command_spec
    '            lb = DirectCast(gvr.Cells(0).FindControl("p10"), LinkButton)
    '            AddHandler (lb.Command), AddressOf lb_command_spec
    '        End If
    '    End Sub

    '    Protected Sub grdSpec_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSpec.PageIndexChanging
    '        Try
    '            grdSpec.PageIndex = e.NewPageIndex
    '            BindSpecUsingSession()
    '        Catch ex As Exception
    '            lblError.Text = "Error: grdSpec_PageIndexChanging() " + ex.Message()
    '        End Try
    '    End Sub

    '#End Region

    '    Protected Sub btnSearchSMSpec_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchSMSpec.Click
    '        Try
    '            GetSpecDetails()
    '        Catch ex As Exception
    '            lblError.Text = "Error: btnSearchSMSpec_Click() " + ex.Message()
    '        End Try
    '    End Sub

    '    'Protected Sub btnLinkChnage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLinkChnage.Click
    '    '    Try
    '    '        If hidPMGrpId.Value <> "" Then
    '    '            lnkSelGrpSMProposal.Text = hidPMGrpNm.Value
    '    '            GetProposalDetails(hidPMGrpId.Value)
    '    '        Else
    '    '            lblMsgSMProposal.Visible = True
    '    '        End If
    '    '    Catch ex As Exception
    '    '        lblError.Text = "Error: btnLinkChnage_Click() " + ex.Message()
    '    '    End Try
    '    'End Sub

    '    Protected Sub btnCreateGrp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateGrp.Click
    '        Try
    '            objUpIns.AddGroupName(txtGroupDe1.Text.Trim().Replace("'", "''").ToString(), txtGroupDe2.Text.Trim().Replace("'", "''").ToString(), Session("UserId"))
    '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Group created successfully');", True)
    '            txtGroupDe1.Text = ""
    '            txtGroupDe2.Text = ""

    '            trCreate.Style.Add("Display", "none")
    '            GetSpecDetails()
    '        Catch ex As Exception
    '            lblError.Text = "Error: btnCreateGrp_Click() " + ex.Message()
    '        End Try
    '    End Sub

    '#End Region

#Region "Group"

    '    Protected Sub GetAllGrpDetails()
    '        Dim ds As New DataSet()
    '        Try
    '            ds = objGetdata.GetAllGrpDetails(txtKey.Text.Trim.ToString(), Session("LicenseNo"))
    '            Session("AllChildGrp") = ds
    '            lblRecondCnt.Text = ds.Tables(0).Rows.Count
    '            If ds.Tables(0).Rows.Count > 0 Then
    '                GrdCGrp.DataSource = ds
    '                GrdCGrp.DataBind()
    '                GrdCGrp.Visible = True
    '                lblNoGrp.Visible = False
    '                BindLink_Grp()
    '            Else
    '                lblNoGrp.Visible = True
    '                GrdCGrp.Visible = False
    '            End If
    '        Catch ex As Exception
    '            lblError.Text = "Error: GetAllGrpDetails() " + ex.Message()
    '        End Try
    '    End Sub

    '    Protected Sub BindLink_Grp()
    '        Dim lblUserID As New Label
    '        Try
    '            'For Each Gr As GridViewRow In GrdCGrp.Rows
    '            '    lblUserID = GrdCGrp.Rows(Gr.RowIndex).FindControl("lblUserID")

    '            '    If Session("UserId") = lblUserID.Text Then
    '            '        Gr.Enabled = True
    '            '    Else
    '            '        Gr.Cells(0).Enabled = False
    '            '        Gr.BackColor = Drawing.Color.LightGray
    '            '    End If

    '            'Next
    '        Catch ex As Exception
    '            lblError.Text = "Error: BindLink_Grp:" + ex.Message()
    '        End Try
    '    End Sub

    '    Protected Sub BindAllGrpUsingSession()
    '        Try
    '            Dim Dts As New DataSet
    '            Dts = Session("AllChildGrp")
    '            GrdCGrp.DataSource = Dts
    '            GrdCGrp.DataBind()
    '            BindLink_Grp()
    '            lblNoGrp.Visible = False
    '        Catch ex As Exception
    '            lblError.Text = "Error: BindAllGrpUsingSession:" + ex.Message()
    '        End Try
    '    End Sub

    '#Region "User Grid Event"

    '    Protected Sub GrdCGrp_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GrdCGrp.Sorting
    '        Dim Dts As New DataSet
    '        Dim dv As DataView
    '        Dim numberDiv As Integer
    '        Dim dsSorted As New DataSet()
    '        Try
    '            numberDiv = Convert.ToInt16(hidSortIdGrp.Value.ToString())
    '            Dts = Session("AllChildGrp")
    '            dv = Dts.Tables(0).DefaultView

    '            If ((numberDiv Mod 2) = 0) Then
    '                dv.Sort = e.SortExpression + " " + "DESC"
    '            Else
    '                dv.Sort = e.SortExpression + " " + "ASC"
    '            End If
    '            numberDiv += 1
    '            hidSortIdGrp.Value = numberDiv.ToString()
    '            GrdCGrp.DataSource = dv
    '            GrdCGrp.DataBind()

    '            dsSorted.Tables.Add(dv.ToTable())
    '            Session("AllChildGrp") = dsSorted
    '            BindLink_Grp()
    '        Catch ex As Exception
    '            Response.Write("Error:GrdCGrp_Sorting:" + ex.Message.ToString())
    '        End Try
    '    End Sub

    '    Protected Sub GrdCGrp_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrdCGrp.PageIndexChanging
    '        Try
    '            GrdCGrp.PageIndex = e.NewPageIndex
    '            BindAllGrpUsingSession()
    '        Catch ex As Exception
    '            Response.Write("Error:GrdCGrp_PageIndexChanging:" + ex.Message.ToString())
    '        End Try
    '    End Sub

    '    Protected Sub ddlPageCountC_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageCountC.SelectedIndexChanged
    '        Try
    '            GrdCGrp.PageSize = ddlPageCountC.SelectedItem.ToString()
    '            BindAllGrpUsingSession()
    '        Catch ex As Exception
    '            Response.Write("Error:ddlPageCountC_SelectedIndexChanged:" + ex.Message.ToString())
    '        End Try
    '    End Sub

    '#End Region

    '    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
    '        Try
    '            GetAllGrpDetails()
    '        Catch ex As Exception
    '            lblError.Text = "Error:btnSearch_Click" + ex.Message.ToString()
    '        End Try
    '    End Sub

    '    Protected Sub btnCreateCGrp_Click(sender As Object, e As EventArgs) Handles btnCreateCGrp.Click
    '        Try
    '            objUpIns.AddGrpByFormat(txtGrpName.Text.Trim().Replace("'", "''").ToString(), txtGrpDescription.Text.Trim().Replace("'", "''").ToString(), Session("UserId"), _
    '                                    ddlMGrp.SelectedValue.ToString())
    '            txtGrpName.Text = ""
    '            txtGrpDescription.Text = ""
    '            GetAllGrpDetails()
    '        Catch ex As Exception
    '            lblError.Text = "Error:btnSearch_Click" + ex.Message.ToString()
    '        End Try
    '    End Sub

#End Region

#Region "Group Manager"

    Protected Sub GetGrpManagerData()
        Dim Ds As New DataSet
        Try
            ' Ds = objGetdata.GetGrpDetailsByMasterGrp(btnSrchGM.Text.Trim.ToString(), Session("USERID"), hidMasterGrpID.Value.ToString())
            Ds = objGetdata.GetGrpDetailsByMasterGrp(txtKeyWordGM.Text.Trim.ToString(), Session("USERID"), hidMasterGrpID.Value.ToString(), rdbtnLogic.SelectedItem.Text, txtSKUDes.Text.ToString(), txtWidth.Text.ToString(), txtHeight.Text.ToString(), txtGusst.Text.ToString())
            Session("GrpManagerB") = Ds
            lblRcrdCountGM.Text = Ds.Tables(0).Rows.Count

            If Ds.Tables(0).Rows.Count > 0 Then
                lblNORcrdGM.Visible = False
                grdGrpMngr.Visible = True
                grdGrpMngr.DataSource = Ds
                grdGrpMngr.DataBind()
                BindLink_Grpmngr()
            Else
                lblNORcrdGM.Visible = True
                grdGrpMngr.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetGrpManagerData:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindGrpManagerUsingSession()
        Try
            Dim Dts As New DataSet
            Dts = Session("GrpManagerB")
            grdGrpMngr.DataSource = Dts
            grdGrpMngr.DataBind()
            BindLink_Grpmngr()
        Catch ex As Exception
            Response.Write("Error:BindGrpManagerUsingSession:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub BindLink_Grpmngr()
        Dim lblUserID As New Label
        Dim lnkGrpNm As New LinkButton
        Dim lblSkuID As New Label
        Dim lblGrpID As New Label
        Try
            For Each Gr As GridViewRow In grdGrpMngr.Rows
                lblUserID = grdGrpMngr.Rows(Gr.RowIndex).FindControl("lblUserID")
                lnkGrpNm = grdGrpMngr.Rows(Gr.RowIndex).FindControl("lnkGrpNm")
                lblSkuID = grdGrpMngr.Rows(Gr.RowIndex).FindControl("lblSkuID")
                lblGrpID = grdGrpMngr.Rows(Gr.RowIndex).FindControl("lblGrpID")

                'lnkGrpNm.Attributes.Add("onclick", "return ShowGroupPopUp('Popup/SelectGroupPopup.aspx?');")
                lnkGrpNm.Attributes.Add("onclick", "return ShowGroupPopUp('Popup/SelectGroupPopup.aspx?MGrpID=" + hidMasterGrpID.Value + "&MtypeID=" + hidMTypeID.Value + "&SkuID=" + lblSkuID.Text + "&lnkInnerText=" + lnkGrpNm.ClientID + "&GrpID=" + lblGrpID.Text + "');")
            Next
        Catch ex As Exception
            lblError.Text = "Error: BindLink_Grp:" + ex.Message()
        End Try
    End Sub

    Protected Sub btnSrchGM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSrchGM.Click
        Try
            GetGrpManagerData()
        Catch ex As Exception
            lblError.Text = "Error:btnSrchGM_Click" + ex.Message.ToString()
        End Try
    End Sub

#Region "User Grid Event"

    Protected Sub grdGrpMngr_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdGrpMngr.Sorting
        Dim Dts As New DataSet
        Dim dv As DataView
        Dim numberDiv As Integer
        Dim dsSorted As New DataSet()
        Try
            numberDiv = Convert.ToInt16(hidSortIdGrpMngr.Value.ToString())
            Dts = Session("GrpManagerB")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            hidSortIdGrpMngr.Value = numberDiv.ToString()
            grdGrpMngr.DataSource = dv
            grdGrpMngr.DataBind()

            dsSorted.Tables.Add(dv.ToTable())
            Session("GrpManagerB") = dsSorted
            BindLink_Grpmngr()
        Catch ex As Exception
            Response.Write("Error:grdGrpMngr_Sorting:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub grdGrpMngr_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdGrpMngr.PageIndexChanging
        Try
            grdGrpMngr.PageIndex = e.NewPageIndex
            BindGrpManagerUsingSession()
        Catch ex As Exception
            Response.Write("Error:grdGrpMngr_PageIndexChanging:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub ddlPageCountGM_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPageCountGM.SelectedIndexChanged
        Try
            grdGrpMngr.PageSize = ddlPageCountGM.SelectedItem.ToString()
            BindGrpManagerUsingSession()
        Catch ex As Exception
            Response.Write("Error:ddlPageCountGM_SelectedIndexChanged:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub grdGrpMngr_databound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdGrpMngr.DataBound
        Try
            Dim gvr As GridViewRow = grdGrpMngr.TopPagerRow

            Dim lb1 As Label = DirectCast(gvr.Cells(0).FindControl("currentpage"), Label)
            lb1.Text = Convert.ToString(grdGrpMngr.PageIndex + 1)
            Dim page As Integer() = New Integer(10) {}
            page(0) = grdGrpMngr.PageIndex - 2
            page(1) = grdGrpMngr.PageIndex - 1
            page(2) = grdGrpMngr.PageIndex
            page(3) = grdGrpMngr.PageIndex + 1
            page(4) = grdGrpMngr.PageIndex + 2
            page(5) = grdGrpMngr.PageIndex + 3
            page(6) = grdGrpMngr.PageIndex + 4
            page(7) = grdGrpMngr.PageIndex + 5
            page(8) = grdGrpMngr.PageIndex + 6
            page(9) = grdGrpMngr.PageIndex + 7
            page(10) = grdGrpMngr.PageIndex + 8
            For i As Integer = 0 To 10
                If i <> 3 Then
                    If page(i) < 1 OrElse page(i) > grdGrpMngr.PageCount Then
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
            If grdGrpMngr.PageIndex = 0 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton1"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton2"), LinkButton)

                lb.Visible = False
            End If
            If grdGrpMngr.PageIndex = grdGrpMngr.PageCount - 1 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton3"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton4"), LinkButton)

                lb.Visible = False
            End If
            If grdGrpMngr.PageIndex > grdGrpMngr.PageCount - 5 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("nmore"), Label)
                lbmore.Visible = False
            End If
            If grdGrpMngr.PageIndex < 4 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("pmore"), Label)
                lbmore.Visible = False
            End If
        Catch ex As Exception
            Response.Write("Error:grdGrpMngr_databound:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub lb_command_VC(ByVal sender As Object, ByVal e As CommandEventArgs)
        grdGrpMngr.PageIndex = Convert.ToInt32(e.CommandArgument) - 1
        BindGrpManagerUsingSession()
    End Sub

    Protected Sub grdGrpMngr_rowcreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdGrpMngr.RowCreated
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

#End Region

#Region "SKU Manager"

#Region "User Define Delegates Sku"

    Protected Sub GetSkuDetails()
        Dim ds As New DataSet
        Try
            ds = objGetdata.GetSkuByUserID(txtSkuSrch.Text.Trim.ToString(), Session("USERID"), Session("LicenseNo"))

            Session("SMSku") = ds
            If ds.Tables(0).Rows.Count > 0 Then
                grdsku.DataSource = ds
                grdsku.DataBind()
                grdsku.Visible = True
                trSkuPageCount.Visible = True
                lblSkuNoFound.Visible = False
                BindLinkSku()
            Else
                lblSkuNoFound.Visible = True
                trSkuPageCount.Visible = False
                grdsku.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error: GetSpecDetails() " + ex.Message()
        End Try
    End Sub
    Protected Sub BindLinkSku()
        Dim lblUserID As New Label
        Try
            For Each Gr As GridViewRow In grdsku.Rows
                lblUserID = grdsku.Rows(Gr.RowIndex).FindControl("lblUserID")

                If Session("UserId") = lblUserID.Text Then
                    Gr.Enabled = True
                Else
                    Gr.Cells(2).Enabled = False
                    Gr.BackColor = Drawing.Color.LightGray
                End If

            Next
        Catch ex As Exception
            lblError.Text = "Error: BindLink:" + ex.Message()
        End Try
    End Sub
    Protected Sub BindSkuUsingSession()
        Try
            Dim Dts As New DataSet
            Dts = Session("SMSku")
            grdsku.DataSource = Dts
            grdsku.DataBind()
            lblSkuNoFound.Visible = False
        Catch ex As Exception
            lblError.Text = "Error: BindSpecUsingSession:" + ex.Message()
        End Try
    End Sub

#End Region

#Region "User Grid Event SKU"

    Protected Sub grdSku_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdsku.Sorting
        Try
            Dim Dts As New DataSet
            Dim dv As DataView
            Dim numberDiv As Integer
            Dim dsSorted As New DataSet()

            numberDiv = Convert.ToInt16(hidSortIdSpec.Value.ToString())
            Dts = Session("SMSku")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If

            numberDiv += 1
            hidSortIdSpec.Value = numberDiv.ToString()
            grdsku.DataSource = dv
            grdsku.DataBind()

            dsSorted.Tables.Add(dv.ToTable())
            Session("SMSku") = dsSorted
            lblSkuNoFound.Visible = False
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub drpSkuPageCount_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpSkuPageCount.SelectedIndexChanged
        Try
            grdsku.PageSize = drpSkuPageCount.SelectedValue.ToString()
            BindSkuUsingSession()
        Catch ex As Exception
            Throw New Exception("Error:drpSpecPageCount_SelectedIndexChanged" + ex.Message)
        End Try
    End Sub

    Protected Sub grdSku_databound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdsku.DataBound
        Try
            Dim gvr As GridViewRow = grdsku.TopPagerRow

            Dim lb1 As Label = DirectCast(gvr.Cells(0).FindControl("currentpage"), Label)
            lb1.Text = Convert.ToString(grdsku.PageIndex + 1)
            Dim page As Integer() = New Integer(10) {}
            page(0) = grdsku.PageIndex - 2
            page(1) = grdsku.PageIndex - 1
            page(2) = grdsku.PageIndex
            page(3) = grdsku.PageIndex + 1
            page(4) = grdsku.PageIndex + 2
            page(5) = grdsku.PageIndex + 3
            page(6) = grdsku.PageIndex + 4
            page(7) = grdsku.PageIndex + 5
            page(8) = grdsku.PageIndex + 6
            page(9) = grdsku.PageIndex + 7
            page(10) = grdsku.PageIndex + 8
            For i As Integer = 0 To 10
                If i <> 3 Then
                    If page(i) < 1 OrElse page(i) > grdsku.PageCount Then
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
            If grdsku.PageIndex = 0 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton1"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton2"), LinkButton)

                lb.Visible = False
            End If
            If grdsku.PageIndex = grdsku.PageCount - 1 Then
                Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton3"), LinkButton)
                lb.Visible = False
                lb = DirectCast(gvr.Cells(0).FindControl("linkbutton4"), LinkButton)

                lb.Visible = False
            End If
            If grdsku.PageIndex > grdsku.PageCount - 5 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("nmore"), Label)
                lbmore.Visible = False
            End If
            If grdsku.PageIndex < 4 Then
                Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("pmore"), Label)
                lbmore.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error: grdSpec_databound() " + ex.Message()
        End Try
    End Sub

    Protected Sub lb_command_sku(ByVal sender As Object, ByVal e As CommandEventArgs)
        grdsku.PageIndex = Convert.ToInt32(e.CommandArgument) - 1
        BindSkuUsingSession()
    End Sub

    Protected Sub grdSku_rowcreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdsku.RowCreated
        If e.Row.RowType = DataControlRowType.Pager Then
            Dim gvr As GridViewRow = e.Row
            Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p0"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_sku
            lb = DirectCast(gvr.Cells(0).FindControl("p1"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_sku
            lb = DirectCast(gvr.Cells(0).FindControl("p2"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_sku
            lb = DirectCast(gvr.Cells(0).FindControl("p4"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_sku
            lb = DirectCast(gvr.Cells(0).FindControl("p5"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_sku
            lb = DirectCast(gvr.Cells(0).FindControl("p6"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_sku
            lb = DirectCast(gvr.Cells(0).FindControl("p7"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_sku
            lb = DirectCast(gvr.Cells(0).FindControl("p8"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_sku
            lb = DirectCast(gvr.Cells(0).FindControl("p9"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_sku
            lb = DirectCast(gvr.Cells(0).FindControl("p10"), LinkButton)
            AddHandler (lb.Command), AddressOf lb_command_sku
        End If
    End Sub

    Protected Sub grdSku_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdsku.PageIndexChanging
        Try
            grdsku.PageIndex = e.NewPageIndex
            BindSkuUsingSession()
        Catch ex As Exception
            lblError.Text = "Error: grdSpec_PageIndexChanging() " + ex.Message()
        End Try
    End Sub

#End Region

    Protected Sub btnSearchSMSku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchSMSku.Click
        Try
            GetSkuDetails()
        Catch ex As Exception
            lblError.Text = "Error: btnSearchSMSpec_Click() " + ex.Message()
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

#Region "Hidden Buttons"

    Protected Sub btnMasterSel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMasterSel.Click
        Try
            If hidMasterGrpID.Value <> "" Or hidMTypeID.Value <> "0" Then
                lnkSelMasterGrp.Text = hidMasterGrpDes.Value
                loadFunctions()
            End If
        Catch ex As Exception
            lblError.Text = "Error: btnMasterSel_Click() " + ex.Message()
        End Try
    End Sub

#End Region

    Protected Sub btnCreateGrp_Click(sender As Object, e As EventArgs) Handles btnCreateGrp.Click
        Try
            objUpIns.AddGrpByFormat(txtGrpName.Text.Trim().Replace("'", "''").ToString(), txtGrpDescription.Text.Trim().Replace("'", "''").ToString(), Session("UserId"), _
                                    ddlMGrp.SelectedValue.ToString())
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Create", "alert('Group Created successfully.');", True)
            txtGrpName.Text = ""
            txtGrpDescription.Text = ""
            trCreateGrp.Style.Add("Display", "none")
            BindGrpByMasterID()
        Catch ex As Exception
            lblError.Text = "Error:btnCreateGrp_Click" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnCreateMasterGrp_Click(sender As Object, e As EventArgs) Handles btnCreateMasterGrp.Click
        Try
            objUpIns.AddMasterGroupDetails(txtMGNm.Text.Trim().Replace("'", "''").ToString(), txtMGrpDes.Text.Trim().Replace("'", "''").ToString(), ddlMType.SelectedValue.ToString(), _
                                           Session("UserId"), "1", ddlMType.SelectedItem.Text.ToString())
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Create", "alert('Master Group Created successfully.');", True)
            txtMGNm.Text = ""
            txtMGrpDes.Text = ""
            trMasterGrp.Style.Add("Display", "none")
        Catch ex As Exception
            lblError.Text = "Error:btnCreateMasterGrp_Click" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnConnectSku_Click(sender As Object, e As EventArgs) Handles btnConnectSku.Click
        Dim Chk As New CheckBox
        Dim SKUID As String = String.Empty
        Try
            For Each gr As GridViewRow In grdGrpMngr.Rows
                Chk = gr.FindControl("ChkSku")
                SKUID = grdGrpMngr.DataKeys(gr.RowIndex).Value.ToString()
                If Chk.Checked = True Then
                    objUpIns.InsertIntoSKUGrpConn(SKUID, ddlGrpByMGId.SelectedValue.ToString(), hidMTypeID.Value)
                End If
            Next
            GetGrpManagerData()
        Catch ex As Exception
            lblError.Text = "Error:btnConnectSku_Click" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnAdvSrch_Click(sender As Object, e As System.EventArgs) Handles btnAdvSrch.Click
        GetGrpManagerData()
    End Sub
End Class
