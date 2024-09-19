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
Partial Class Pages_SavvyPackPro_SkuManager
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
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
            hidSortIdBSpec.Value = "0"
            hidSortIdGrpMngr.Value = "0"
            ChkExistingRfp()
            ChkExistingGrp()
        End If
    End Sub

    Protected Sub tabSkuManager_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabSkuManager.ActiveTabChanged
        Try
            If tabSkuManager.ActiveTabIndex = "0" Then
                GetSkuDetails()
            ElseIf tabSkuManager.ActiveTabIndex = "1" Then
                GetGrpManagerData()
            End If
        Catch ex As Exception
            lblError.Text = "Error: tabSkuManager_ActiveTabChanged() " + ex.Message()
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
                    tabSkuManager.Enabled = False
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
        Try
            If RfpID <> "" Or RfpID <> "0" Then
                DsRfpdet = objGetdata.GetRFPbyID(RfpID)
                Session("DsRfpdet") = DsRfpdet
                If DsRfpdet.Tables(0).Rows.Count > 0 Then
                    RfpDetail.Visible = True
                    lnkSelRFP.Text = DsRfpdet.Tables(0).Rows(0).Item("DES1").ToString()
                    lblSelRfpID.Text = DsRfpdet.Tables(0).Rows(0).Item("ID").ToString()
                    Session("hidRfpID") = lblSelRfpID.Text
                    lblSelRfpDes.Text = DsRfpdet.Tables(0).Rows(0).Item("DES1").ToString()
                    lblType.Text = DsRfpdet.Tables(0).Rows(0).Item("TYPEDESC").ToString()
                    'Check for LoggedIn User
                    If Session("USERID") = DsRfpdet.Tables(0).Rows(0).Item("USERID").ToString() Then
                        tabSkuManager.Enabled = True

                    Else
                        tabSkuManager.Enabled = False
                    End If
                    'End
                    loadTab()
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('System isn't able to find ID. Please try again.');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error: GetRfpDetails() " + ex.Message()
        End Try
    End Sub

    Protected Sub loadTab()
        Try
            GetSkuDetails()
        Catch ex As Exception
            lblError.Text = "Error: loadTab() " + ex.Message()
        End Try
    End Sub

    Protected Sub ChkExistingGrp()
        Dim DsCheck As New DataSet()
        Try
            DsCheck = objGetdata.GetMasterGrp(Session("USERID"), Session("hidRfpID"))
            If DsCheck.Tables(0).Rows.Count > 0 Then
                lblNoMasterGrp.Visible = False
                GetLatestDetails(DsCheck.Tables(0).Rows(0).Item("MGROUPID").ToString())
            Else
                hidMasterGrpID.Value = ""
                lnkSelMasterGrp.Text = "Nothing Selected"
                hidMasterGrpDes.Value = ""
                lblRcrdCountGM.Text = "0"
                lblNoMasterGrp.Visible = True
                grdGrpMngr.Visible = False
                Session("SMGrpMngrGrid") = Nothing
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

#Region "Select SKU"

#Region "User Define Delegates"

    Protected Sub GetSkuDetails()
        Dim ds As New DataSet()
        Try
            'ds = objGetdata.GetSkuDetails(txtSpecKeyword.Text.Trim.ToString(), Session("USERID"))
            ds = objGetdata.GetSkuDetail_N(txtSpecKeyword.Text.Trim.ToString(), Session("USERID"))
            Session("SMSelSku") = ds
            lblNoOfSpec.Text = ds.Tables(0).Rows.Count

            If ds.Tables(0).Rows.Count > 0 Then
                grdSpec.DataSource = ds
                grdSpec.DataBind()
                grdSpec.Visible = True
                lblSpecNoFound.Visible = False
            Else
                lblSpecNoFound.Visible = True
                grdSpec.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error: GetSkuDetails() " + ex.Message()
        End Try
    End Sub

    Protected Sub BindSpecUsingSession()
        Try
            Dim Dts As New DataSet
            Dts = Session("SMSelSku")
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
        Dim Dts As New DataSet
        Dim dv As DataView
        Dim numberDiv As Integer
        Dim dsSorted As New DataSet()
        Try
            numberDiv = Convert.ToInt16(hidSortIdBSpec.Value.ToString())
            Dts = Session("SMSelSku")
            dv = Dts.Tables(0).DefaultView

            If ((numberDiv Mod 2) = 0) Then
                dv.Sort = e.SortExpression + " " + "DESC"
            Else
                dv.Sort = e.SortExpression + " " + "ASC"
            End If
            numberDiv += 1
            hidSortIdBSpec.Value = numberDiv.ToString()
            grdSpec.DataSource = dv
            grdSpec.DataBind()

            dsSorted.Tables.Add(dv.ToTable())
            Session("SMSelSku") = dsSorted

        Catch ex As Exception
            Response.Write("Error:grdSpec_Sorting:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub grdSpec_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdSpec.PageIndexChanging
        Try
            grdSpec.PageIndex = e.NewPageIndex
            BindSpecUsingSession()
        Catch ex As Exception
            Response.Write("Error:grdSpec_PageIndexChanging:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub ddlSpec_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSpec.SelectedIndexChanged
        Try
            grdSpec.PageSize = ddlSpec.SelectedItem.ToString()
            BindSpecUsingSession()
        Catch ex As Exception
            Response.Write("Error:ddlPageCountC_SelectedIndexChanged:" + ex.Message.ToString())
        End Try
    End Sub

    'Protected Sub grdSpec_databound(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSpec.DataBound
    '    Try
    '        Dim gvr As GridViewRow = grdSpec.TopPagerRow

    '        Dim lb1 As Label = DirectCast(gvr.Cells(0).FindControl("currentpage"), Label)
    '        lb1.Text = Convert.ToString(grdSpec.PageIndex + 1)
    '        Dim page As Integer() = New Integer(10) {}
    '        page(0) = grdSpec.PageIndex - 2
    '        page(1) = grdSpec.PageIndex - 1
    '        page(2) = grdSpec.PageIndex
    '        page(3) = grdSpec.PageIndex + 1
    '        page(4) = grdSpec.PageIndex + 2
    '        page(5) = grdSpec.PageIndex + 3
    '        page(6) = grdSpec.PageIndex + 4
    '        page(7) = grdSpec.PageIndex + 5
    '        page(8) = grdSpec.PageIndex + 6
    '        page(9) = grdSpec.PageIndex + 7
    '        page(10) = grdSpec.PageIndex + 8
    '        For i As Integer = 0 To 10
    '            If i <> 3 Then
    '                If page(i) < 1 OrElse page(i) > grdSpec.PageCount Then
    '                    Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p" & Convert.ToString(i)), LinkButton)
    '                    lb.Visible = False
    '                Else
    '                    Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p" & Convert.ToString(i)), LinkButton)
    '                    lb.Text = Convert.ToString(page(i))

    '                    lb.CommandName = "pageno"

    '                    lb.CommandArgument = lb.Text
    '                End If
    '            End If
    '        Next
    '        If grdSpec.PageIndex = 0 Then
    '            Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton1"), LinkButton)
    '            lb.Visible = False
    '            lb = DirectCast(gvr.Cells(0).FindControl("linkbutton2"), LinkButton)

    '            lb.Visible = False
    '        End If
    '        If grdSpec.PageIndex = grdSpec.PageCount - 1 Then
    '            Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("linkbutton3"), LinkButton)
    '            lb.Visible = False
    '            lb = DirectCast(gvr.Cells(0).FindControl("linkbutton4"), LinkButton)

    '            lb.Visible = False
    '        End If
    '        If grdSpec.PageIndex > grdSpec.PageCount - 5 Then
    '            Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("nmore"), Label)
    '            lbmore.Visible = False
    '        End If
    '        If grdSpec.PageIndex < 4 Then
    '            Dim lbmore As Label = DirectCast(gvr.Cells(0).FindControl("pmore"), Label)
    '            lbmore.Visible = False
    '        End If
    '    Catch ex As Exception
    '        Response.Write("Error:grdSpec_databound:" + ex.Message.ToString())
    '    End Try
    'End Sub

    'Protected Sub lb_command_SS(ByVal sender As Object, ByVal e As CommandEventArgs)
    '    grdSpec.PageIndex = Convert.ToInt32(e.CommandArgument) - 1
    '    BindSpecUsingSession()
    'End Sub

    'Protected Sub grdSpec_rowcreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSpec.RowCreated
    '    If e.Row.RowType = DataControlRowType.Pager Then
    '        Dim gvr As GridViewRow = e.Row
    '        Dim lb As LinkButton = DirectCast(gvr.Cells(0).FindControl("p0"), LinkButton)
    '        AddHandler (lb.Command), AddressOf lb_command_SS
    '        lb = DirectCast(gvr.Cells(0).FindControl("p1"), LinkButton)
    '        AddHandler (lb.Command), AddressOf lb_command_SS
    '        lb = DirectCast(gvr.Cells(0).FindControl("p2"), LinkButton)
    '        AddHandler (lb.Command), AddressOf lb_command_SS
    '        lb = DirectCast(gvr.Cells(0).FindControl("p4"), LinkButton)
    '        AddHandler (lb.Command), AddressOf lb_command_SS
    '        lb = DirectCast(gvr.Cells(0).FindControl("p5"), LinkButton)
    '        AddHandler (lb.Command), AddressOf lb_command_SS
    '        lb = DirectCast(gvr.Cells(0).FindControl("p6"), LinkButton)
    '        AddHandler (lb.Command), AddressOf lb_command_SS
    '        lb = DirectCast(gvr.Cells(0).FindControl("p7"), LinkButton)
    '        AddHandler (lb.Command), AddressOf lb_command_SS
    '        lb = DirectCast(gvr.Cells(0).FindControl("p8"), LinkButton)
    '        AddHandler (lb.Command), AddressOf lb_command_SS
    '        lb = DirectCast(gvr.Cells(0).FindControl("p9"), LinkButton)
    '        AddHandler (lb.Command), AddressOf lb_command_SS
    '        lb = DirectCast(gvr.Cells(0).FindControl("p10"), LinkButton)
    '        AddHandler (lb.Command), AddressOf lb_command_SS
    '    End If
    'End Sub

#End Region

    Protected Sub btnSpecSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSpecSearch.Click
        Try
            GetSkuDetails()
        Catch ex As Exception
            lblError.Text = "Error:btnSpecSearch_Click" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub grdSpec_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSpec.RowDataBound
        Dim DsExistSpec As New DataSet
        Dim lblSpecID As New Label
        Dim check As New CheckBox
        Try
            DsExistSpec = objGetdata.GetSKUByRfpID(Session("hidRfpID"), Session("USERID"))
            For Each Gr As GridViewRow In grdSpec.Rows
                lblSpecID = grdSpec.Rows(Gr.RowIndex).FindControl("lblSKUID")
                check = grdSpec.Rows(Gr.RowIndex).FindControl("chkSku")
                For i = 0 To DsExistSpec.Tables(0).Rows.Count - 1
                    If lblSpecID.Text = DsExistSpec.Tables(0).Rows(i).Item("SKUID").ToString() Then
                        check.Checked = True
                    End If
                Next
            Next
        Catch ex As Exception
            lblError.Text = "Error:grdSpec_RowDataBound" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub chkchangedSku(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim chkSku As CheckBox = TryCast(sender, CheckBox)
            Dim gvrow As GridViewRow = TryCast(chkSku.NamingContainer, GridViewRow)
            Dim SkuID As String = CType(gvrow.FindControl("lblSKUID"), Label).Text

            If chkSku.Checked = True Then
                objUpIns.ConnectSkuRfp(SkuID.ToString(), Session("hidRfpID"))
            Else
                objUpIns.DisconnectSkuRfp(SkuID.ToString(), Session("hidRfpID"))
            End If
            GetSkuDetails()
        Catch ex As Exception

        End Try
    End Sub

    'Protected Sub gvSelSKu_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
    '    Dim DsColtbl As New DataSet()
    '    Dim IsUnitAvl As Boolean = False
    '    Dim UnitVal As String = String.Empty
    '    Try
    '        DsColtbl = objGetdata.GetUnitCols("")
    '        Dim gvRow__1 As GridViewRow = e.Row
    '        If gvRow__1.RowType = DataControlRowType.Header Then
    '            Dim gvrow__2 As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
    '            gvrow__2.BorderWidth = 1
    '            Dim HeaderCell As TableCell = New TableCell()
    '            For Each Cols As DataControlField In grdSpec.Columns
    '                If Cols.HeaderText.ToUpper() = "USERID" Then
    '                Else
    '                    If DsColtbl.Tables(0).Rows.Count > 0 Then
    '                        For i = 0 To DsColtbl.Tables(0).Rows.Count - 1
    '                            If Cols.HeaderText.ToUpper() = DsColtbl.Tables(0).Rows(i).Item("DETAIL").ToString() Then
    '                                IsUnitAvl = True
    '                                UnitVal = DsColtbl.Tables(0).Rows(i).Item("UNITNAME").ToString()
    '                                Exit For
    '                            Else
    '                                IsUnitAvl = False
    '                                UnitVal = String.Empty
    '                            End If
    '                        Next
    '                        If IsUnitAvl Then
    '                            HeaderCell = New TableCell()
    '                            HeaderCell.Text = UnitVal
    '                            HeaderCell.HorizontalAlign = HorizontalAlign.Center
    '                            HeaderCell.ColumnSpan = 0
    '                            gvrow__2.Cells.Add(HeaderCell)
    '                        Else
    '                            HeaderCell = New TableCell()
    '                            HeaderCell.Text = ""
    '                            HeaderCell.HorizontalAlign = HorizontalAlign.Center
    '                            HeaderCell.ColumnSpan = 0
    '                            gvrow__2.Cells.Add(HeaderCell)
    '                        End If
    '                    End If
    '                End If
    '            Next
    '            grdSpec.Controls(0).Controls.AddAt(2, gvrow__2)
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

#End Region

#Region "Group Manager"

    Protected Sub BindGrpByMasterID()
        Dim Dts As New DataSet
        Dim lst As New ListItem
        Try
            Dts = objGetdata.BindGrpByMasterGrp(hidMasterGrpID.Value, Session("USERID").ToString())

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

    Protected Sub GetGrpManagerData()
        Dim Ds As New DataSet
        Try
            If hidMasterGrpID.Value <> "" Then
                Ds = objGetdata.GetGrpDetailsByMasterGrp_N(txtKeyWordGM.Text.Trim.ToString(), Session("USERID"), hidMasterGrpID.Value.ToString(), rdbtnLogic.SelectedItem.Text, _
                                                        txtSKUDes.Text.ToString(), txtWidth.Text.ToString(), txtHeight.Text.ToString(), txtGusst.Text.ToString(), Session("hidRfpID"))
                Session("SMGrpMngrGrid") = Ds
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
            Else
                lblNORcrdGM.Visible = True
                grdGrpMngr.Visible = False
                lblRcrdCountGM.Text = "0"
                Session("SMGrpMngrGrid") = Nothing
            End If
           
        Catch ex As Exception
            lblError.Text = "Error:GetGrpManagerData:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindGrpManagerUsingSession()
        Try
            Dim Dts As New DataSet
            Dts = Session("SMGrpMngrGrid")
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

                lnkGrpNm.Attributes.Add("onclick", "return ShowGroupPopUp('Popup/SelectGroupPopup.aspx?MGrpID=" + hidMasterGrpID.Value + "&SkuID=" + lblSkuID.Text + "&lnkInnerText=" + lnkGrpNm.ClientID + "&GrpID=" + lblGrpID.Text + "');")
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
            Dts = Session("SMGrpMngrGrid")
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
            Session("SMGrpMngrGrid") = dsSorted
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

    'Protected Sub gvDetails_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
    '    Dim DsColtbl As New DataSet()
    '    Dim IsUnitAvl As Boolean = False
    '    Dim UnitVal As String = String.Empty
    '    Try
    '        DsColtbl = objGetdata.GetUnitCols("")
    '        Dim gvRow__1 As GridViewRow = e.Row
    '        If gvRow__1.RowType = DataControlRowType.Header Then
    '            Dim gvrow__2 As New GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert)
    '            gvrow__2.BorderWidth = 1
    '            Dim HeaderCell As TableCell = New TableCell()
    '            For Each Cols As DataControlField In grdGrpMngr.Columns
    '                If Cols.HeaderText.ToUpper() = "USERID" Then
    '                Else
    '                    If DsColtbl.Tables(0).Rows.Count > 0 Then
    '                        For i = 0 To DsColtbl.Tables(0).Rows.Count - 1
    '                            If Cols.HeaderText.ToUpper() = DsColtbl.Tables(0).Rows(i).Item("DETAIL").ToString() Then
    '                                IsUnitAvl = True
    '                                UnitVal = DsColtbl.Tables(0).Rows(i).Item("UNITNAME").ToString()
    '                                Exit For
    '                            Else
    '                                IsUnitAvl = False
    '                                UnitVal = String.Empty
    '                            End If
    '                        Next

    '                        If IsUnitAvl Then
    '                            HeaderCell = New TableCell()
    '                            HeaderCell.Text = UnitVal
    '                            HeaderCell.HorizontalAlign = HorizontalAlign.Center
    '                            HeaderCell.ColumnSpan = 0
    '                            gvrow__2.Cells.Add(HeaderCell)
    '                        Else
    '                            HeaderCell = New TableCell()
    '                            HeaderCell.Text = ""
    '                            HeaderCell.HorizontalAlign = HorizontalAlign.Center
    '                            HeaderCell.ColumnSpan = 0
    '                            gvrow__2.Cells.Add(HeaderCell)
    '                        End If

    '                    End If
    '                End If
    '            Next
    '            grdGrpMngr.Controls(0).Controls.AddAt(2, gvrow__2)
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

#End Region

    Protected Sub btnConnectSku_Click(sender As Object, e As EventArgs) Handles btnConnectSku.Click
        Dim Chk As New CheckBox
        Dim SKUID As String = String.Empty
        Try
            For Each gr As GridViewRow In grdGrpMngr.Rows
                Chk = gr.FindControl("ChkSku")
                SKUID = grdGrpMngr.DataKeys(gr.RowIndex).Value.ToString()
                If Chk.Checked = True Then
                    objUpIns.InsertIntoSKUGrpConn(SKUID, ddlGrpByMGId.SelectedValue.ToString())
                End If
            Next
            ScriptManager.RegisterStartupScript(Page, Me.GetType(), "CreateRFP", "alert('SKU Connected successfully');", True)
            GetGrpManagerData()
        Catch ex As Exception
            lblError.Text = "Error:btnConnectSku_Click" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnAdvSrch_Click(sender As Object, e As System.EventArgs) Handles btnAdvSrch.Click
        Try
            GetGrpManagerData()
        Catch ex As Exception
            lblError.Text = "Error:btnAdvSrch_Click" + ex.Message.ToString()
        End Try
    End Sub

#End Region

#Region "Hidden Buttons"

    Protected Sub btnHidRFPCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHidRFPCreate.Click
        Try
            If hidRfpID.Value <> "" Or hidRfpID.Value <> "0" Then
                tabSkuManager.Enabled = True
                lnkSelRFP.Text = hidRfpNm.Value
                GetRfpDetails(hidRfpID.Value)
                ChkExistingGrp()
            End If
        Catch ex As Exception
            lblError.Text = "Error: btnHidRFPCreate_Click() " + ex.Message()
        End Try
    End Sub

    Protected Sub btnMasterSel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMasterSel.Click
        Try
            If hidMasterGrpID.Value <> "" Then
                lnkSelMasterGrp.Text = hidMasterGrpDes.Value
                BindGrpByMasterID()
                GetGrpManagerData()
            End If
        Catch ex As Exception
            lblError.Text = "Error: btnMasterSel_Click() " + ex.Message()
        End Try
    End Sub

#End Region

End Class
