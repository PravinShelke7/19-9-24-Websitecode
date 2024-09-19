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
Imports System.Web.HttpBrowserCapabilities
Imports Corda
Imports System.Web.UI.HtmlControls
Partial Class Pages_SavvyPackPro_AnalyzeProposal_Supplier
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData
    'Bag making
    Dim BMDes(50) As String
    Dim PCval(50) As String
    Dim CapVal(50) As String
    Dim FreeVal(50) As String
    Dim MacVal(50) As String
    'end bag making
    'Struct
    Dim CalWeight(9, 11) As String
    Dim StructMatId(9, 11) As String
    Dim StructThick(9, 11) As String
    Dim StructPrice(9, 11) As String
    Dim weightPercn(9, 11) As String
    Dim TtlThickness(9) As String
    Dim TtlDensity(9) As String
    Dim TtlWeight(9) As String
    Dim TtlDensityInner(9) As String
    Dim TtlPrice(9) As String
    Dim DataCnt As Integer
    Dim DsGroup As New DataSet()
    Dim DsSkuFStrct As New DataSet()
    Dim isSkuOnly As Boolean = True
    Private isPageLoaded As Boolean = True

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim DsChkMGrp As New DataSet()

        Try
            pnlBMConfigVendor.Visible = False
            GrdPriceOption.Visible = False
            lblFooter.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            If Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            If Session("USERID") = Nothing Or Session("UserName") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                hidSortIdBMVendor.Value = "0"
                ChkExistingRfp()
                ChkExistingMGrpRfp()
                GetPageDetails()
                'GetPrice()
                'GetPriceCost()
                'GetPriceOptn()
                'GetRFQ()
                'GetBagMakingDetails()
                'GetStructure()
                'For SKu Lvl
                DsChkMGrp = objGetdata.CheckMGrpForStruct(Session("ShidRfpID"))
                If DsChkMGrp.Tables(0).Rows.Count > 0 Then
                    isSkuOnly = False
                    trSelM.Visible = True
                    btnSkulvlData.Visible = True
                    'chkskulvldata()

                Else
                    isSkuOnly = True
                    trSelM.Visible = False
                    btnSkulvlData.Visible = False
                    'chkskulvldata()
                End If
                If isSkuOnly Then
                    GetSkuForStructInfo()
                End If
                'End
            Else
                '  GetPageDetails()
                'For SKu Lvl
                DsChkMGrp = objGetdata.CheckMGrpForStruct(Session("ShidRfpID"))
                If DsChkMGrp.Tables(0).Rows.Count > 0 Then
                    isSkuOnly = False
                    trSelM.Visible = True
                    btnSkulvlData.Visible = True
                    'chkskulvldata()

                Else
                    isSkuOnly = True
                    trSelM.Visible = False
                    btnSkulvlData.Visible = False
                    'chkskulvldata()

                End If
                'End
                If isSkuOnly Then
                    lblNoStructData.Visible = False
                    GetSkuForStructInfo()

                Else
                    If hidMasterGrpID.Value <> "" Then
                        GetGrpInfo()
                    End If
                End If
                chkskulvldata()
                GetStructure()
            End If
        Catch ex As Exception
            lblError.Text = "Error: Page_Load() " + ex.Message()
        End Try
    End Sub

    Protected Sub tabRfpSManager_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabRfpSManager.ActiveTabChanged
        Try
            If isPageLoaded Then
                isPageLoaded = False
                If tabRfpSManager.ActiveTabIndex = "0" Then
                    GetUsersList()
                ElseIf tabRfpSManager.ActiveTabIndex = "1" Then
                    GetPageDetails()
                ElseIf tabRfpSManager.ActiveTabIndex = "2" Then
                    GetPrice()
                ElseIf tabRfpSManager.ActiveTabIndex = "3" Then
                    GetPriceCost()
                ElseIf tabRfpSManager.ActiveTabIndex = "4" Then
                    ' GetPriceOptn()
                    BindPRICETable()
                ElseIf tabRfpSManager.ActiveTabIndex = "5" Then
                    GetRFQ()
                ElseIf tabRfpSManager.ActiveTabIndex = "6" Then
                    GetStructure()
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error: tabSupplierManager_ActiveTabChanged() " + ex.Message()
        End Try
    End Sub

#Region "RFP Config"

    Protected Sub ChkExistingRfp()
        Dim DsCheckRfp As New DataSet()
        Try
            DsCheckRfp = objGetdata.GetLatestRSbyLicenseID(Session("LicenseNo"), Session("UserName"))
            If DsCheckRfp.Tables(0).Rows.Count > 0 Then
                GetRfpDetails_new(DsCheckRfp.Tables(0).Rows(0).Item("ID").ToString(), DsCheckRfp.Tables(0).Rows(0).Item("VENDOREMAILID").ToString())
            Else
                RfpDetail.Visible = False
                tabRfpSManager.Enabled = False
            End If
        Catch ex As Exception
            lblError.Text = "Error: ChkExistingRfp() " + ex.Message()
        End Try
    End Sub

    'Protected Sub GetRfpDetails(ByVal RfpID As String)
    '    Dim DsRfpdet As New DataSet()
    '    Try
    '        If RfpID <> "" Or RfpID <> "0" Then
    '            DsRfpdet = objGetdata.GetRfpDetailOnVendor(RfpID, Session("UserName"))
    '            If DsRfpdet.Tables(0).Rows.Count > 0 Then
    '                RfpDetail.Visible = True
    '                lnkSelRFP.Text = DsRfpdet.Tables(0).Rows(0).Item("DES1").ToString()
    '                lblSelRfpID.Text = DsRfpdet.Tables(0).Rows(0).Item("ID").ToString()
    '                Session("ShidRfpID") = lblSelRfpID.Text
    '                Session("SVendorID") = DsRfpdet.Tables(0).Rows(0).Item("VENDORID").ToString()
    '                Session("BUserID") = DsRfpdet.Tables(0).Rows(0).Item("BUYERID").ToString()
    '                lblType.Text = DsRfpdet.Tables(0).Rows(0).Item("TYPEDESC").ToString()
    '                lblSelRfpDes.Text = DsRfpdet.Tables(0).Rows(0).Item("DES1").ToString()
    '                ' tabRfpSManager.Enabled = True
    '                If DsRfpdet.Tables(0).Rows(0).Item("PRICEOPTIONID").ToString() = "1" Then
    '                    tabSMPC.Visible = True
    '                    tabSMPrice.Visible = False
    '                    tabSMPrice.HeaderText = ""
    '                ElseIf DsRfpdet.Tables(0).Rows(0).Item("PRICEOPTIONID").ToString() = "2" Then
    '                    tabSMPC.Visible = False
    '                    tabSMPrice.Visible = True
    '                    tabSMPC.HeaderText = ""
    '                ElseIf DsRfpdet.Tables(0).Rows(0).Item("PRICEOPTIONID").ToString() = "" Then
    '                    tabSMPC.Visible = False
    '                    tabSMPrice.Visible = False
    '                    tabSMPC.HeaderText = ""
    '                    tabSMPrice.HeaderText = ""
    '                Else
    '                    tabSMPC.Visible = True
    '                    tabSMPrice.Visible = True
    '                End If
    '                If Session("USERID") = DsRfpdet.Tables(0).Rows(0).Item("BUYERID").ToString() Then
    '                    tabRfpSManager.Enabled = True
    '                Else
    '                    tabRfpSManager.Enabled = False
    '                End If
    '                loadTab()
    '            Else
    '                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('System isn't able to find ID. Please try again.');", True)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = "Error: GetRfpDetails() " + ex.Message()
    '    End Try
    'End Sub

    Protected Sub GetRfpDetails_new(ByVal TypeID As String, ByVal SelROwnerEmailID As String)
        Dim DsRfpdet As New DataSet()
        Try
            If TypeID <> "" Or TypeID <> "0" Then
                DsRfpdet = objGetdata.GetRfpDetailOnVendor(TypeID, SelROwnerEmailID)
                If DsRfpdet.Tables(0).Rows.Count > 0 Then
                    RfpDetail.Visible = True
                    lnkSelRFP.Text = DsRfpdet.Tables(0).Rows(0).Item("DES1").ToString()
                    lblSelRfpID.Text = DsRfpdet.Tables(0).Rows(0).Item("ID").ToString()
                    Session("ShidRfpID") = lblSelRfpID.Text
                    Session("SVendorID") = DsRfpdet.Tables(0).Rows(0).Item("VENDORID").ToString()
                    Session("BUserID") = DsRfpdet.Tables(0).Rows(0).Item("BUYERID").ToString()
                    lblType.Text = DsRfpdet.Tables(0).Rows(0).Item("TYPEDESC").ToString()
                    lblBuyer.Text = DsRfpdet.Tables(0).Rows(0).Item("BUYEREMAILADD").ToString()
                    lblSelRfpDes.Text = DsRfpdet.Tables(0).Rows(0).Item("DES1").ToString()

                    'Ak Changes
                    If DsRfpdet.Tables(0).Rows(0).Item("PRICEOPTIONID").ToString() = "1" Then
                        tabSMPC.Visible = True
                        tabSMPrice.Enabled = False
                        tabSMPrice.HeaderText = ""
                    ElseIf DsRfpdet.Tables(0).Rows(0).Item("PRICEOPTIONID").ToString() = "2" Then
                        tabSMPC.Enabled = False
                        tabSMPrice.Visible = True
                        'tabBagmaking.Visible = True
                        tabSMPC.HeaderText = ""
                    ElseIf DsRfpdet.Tables(0).Rows(0).Item("PRICEOPTIONID").ToString() = "" Then
                        tabSMPC.Enabled = False
                        tabSMPrice.Enabled = False
                        tabSMPC.HeaderText = ""
                        tabSMPrice.HeaderText = ""
                    Else
                        tabSMPC.Visible = True
                        tabSMPrice.Visible = True
                        'tabBagmaking.Visible = True
                    End If
                    'End

                    ' tabBagmaking.Visible = True
                    'loadTab()
                    If Session("UserName") = SelROwnerEmailID Then
                        tabRfpSManager.Enabled = True
                    Else
                        tabRfpSManager.Enabled = False
                    End If
                    'ptchanges
                    Dim DsChkMGrp As DataSet
                    DsChkMGrp = objGetdata.CheckMGrpForStruct(Session("ShidRfpID"))
                    If DsChkMGrp.Tables(0).Rows.Count > 0 Then
                        isSkuOnly = False
                        trSelM.Visible = True
                        btnSkulvlData.Visible = True
                    Else
                        isSkuOnly = True
                        trSelM.Visible = False
                        btnSkulvlData.Visible = False
                    End If

                    If isSkuOnly Then
                        lblNoStructData.Visible = False
                        GetSkuForStructInfo()

                    Else
                        If hidMasterGrpID.Value <> "" Then
                            GetGrpInfo()
                        End If
                    End If
                    'end pt changes
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
            'GetUsersList()
            GetPageDetails()
            GetPrice()
            GetPriceCost()
            'GetPriceOptn()
            BindPRICETable()
            GetRFQ()
            GetStructure()
            'GetBagMakingDetails()
        Catch ex As Exception
            lblError.Text = "Error: GetRfpDetails() " + ex.Message()
        End Try
    End Sub

    Protected Sub btnHidRFPCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHidRFPCreate.Click
        Try
            If hidRfpID.Value <> "" Or hidRfpID.Value <> "0" Then
                tabRfpSManager.Enabled = True
                lnkSelRFP.Text = hidRfpNm.Value
                lbkpriceopt.Text = "Nothing Selected"
                hidPriceId.Value = ""
                'GetRfpDetails(hidRfpID.Value)
                GetRfpDetails_new(hidRfpID.Value, hidRSOwnerEmailID.Value)
                chkskulvldata()
                ChkExistingMGrpRfp()
                loadTab()
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('System isn't able to find RFPID. Please try again.');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error: btnHidRFPCreate_Click() " + ex.Message()
        End Try
    End Sub

#End Region

#Region "Vendor Config"

    Protected Sub GetUsersList()
        Dim Ds As New DataSet
        Try
            Ds = objGetdata.GetUserList(txtKey.Text.Trim.ToString(), Session("ShidRfpID"))
            Session("SMBuyerList") = Ds
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
            Dts = Session("SMBuyerList")
            grdUsers.DataSource = Dts
            grdUsers.DataBind()
        Catch ex As Exception
            Response.Write("Error:BindUsersListUsingSession:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetUsersList()
        Catch ex As Exception
            lblError.Text = "Error:btnSearch_Click" + ex.Message.ToString()
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
            Dts = Session("SMBuyerList")
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
            Session("SMBuyerList") = dsSorted

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

    Protected Sub grdUsers_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdUsers.RowDataBound
        Dim DsExistusr As New DataSet
        Dim lblUserID As New Label
        Dim check As New CheckBox
        Try
            DsExistusr = objGetdata.GetLinkedVendor(Session("ShidRfpID"))
            For Each Gr As GridViewRow In grdUsers.Rows
                lblUserID = grdUsers.Rows(Gr.RowIndex).FindControl("lblMUsrID")
                check = grdUsers.Rows(Gr.RowIndex).FindControl("select")
                For i = 0 To DsExistusr.Tables(0).Rows.Count - 1
                    If lblUserID.Text = DsExistusr.Tables(0).Rows(i).Item("VENDORID").ToString() Then
                        check.Checked = True
                    End If
                Next
            Next
        Catch ex As Exception
            lblError.Text = "Error:grdUsers_RowDataBound" + ex.Message.ToString()
        End Try
    End Sub

#End Region

#Region "Terms"

    Protected Sub GetPageDetails()
        Dim dsQues As New DataSet()
        Dim lnkQues As LinkButton
        Dim trQues As TableRow
        Dim tdId As TableCell
        Dim delBtn As Button
        Dim trSpace As New TableRow
        Dim lblTerm As New Label
        Dim lblItem As New Label
        Dim txtCom As New TextBox
        Dim lblcom As New Label
        Dim HidSeqQue As New HiddenField
        Dim trHeader As New TableRow
        Dim tdHeader As TableCell
        Dim trInner As New TableRow
        Dim chck As New CheckBox
        Dim hid As HiddenField
        Dim DvData As DataView
        Dim dsData As DataTable
        Dim dsS As New DataSet
        Dim ddlPriceOptn As New DropDownList
        Dim lblpriceopt As New Label
        Dim HidPriceoptn As New HiddenField
        Dim ddlPrintTech As New DropDownList
        Dim lblprintTech As New Label

        Try
            If Session("ShidRfpID") = Nothing Or Session("SVendorID") = Nothing Or Session("BUserID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else
                tblEditQ.Rows.Clear()
                tblcustomize.Rows.Clear()
                dsQues = objGetdata.GetSTerms(Session("ShidRfpID"), Session("BUserID"), Session("SVendorID"))

                Session("STermsData") = dsQues
                DvData = dsQues.Tables(0).DefaultView
                Session("count") = dsQues.Tables(0).Rows.Count - 1

                For i = 1 To 6
                    tdHeader = New TableCell
                    Dim Title As String = String.Empty

                    Select Case i
                        Case 1
                            HeaderTdSetting(tdHeader, "50px", "Accept Terms", "1")
                            trHeader.Controls.Add(tdHeader)

                        Case 2

                            HeaderTdSetting(tdHeader, "250px", "Terms", "1")
                            trHeader.Controls.Add(tdHeader)

                        Case 3

                            HeaderTdSetting(tdHeader, "350px", "Description", "1")
                            trHeader.Controls.Add(tdHeader)

                        Case 4
                            HeaderTdSetting(tdHeader, "50px", "Comments", "1")
                            trHeader.Controls.Add(tdHeader)

                        Case 5
                            HeaderTdSetting(tdHeader, "50px", "Price Option", "1")
                            trHeader.Controls.Add(tdHeader)

                        Case 6
                            HeaderTdSetting(tdHeader, "50px", "Print Tech", "1")
                            trHeader.Controls.Add(tdHeader)

                    End Select

                Next

                trHeader.Height = 30
                tblEditQ.Controls.Add(trHeader)


                'pt changes
                Dim trHeaderS As New TableRow
                For i = 1 To 6
                    tdHeader = New TableCell
                    Dim Title As String = String.Empty

                    Select Case i
                        Case 1
                            HeaderTdSetting(tdHeader, "50px", "Accept Terms", "1")
                            trHeaderS.Controls.Add(tdHeader)

                        Case 2

                            HeaderTdSetting(tdHeader, "250px", "Terms", "1")
                            trHeaderS.Controls.Add(tdHeader)

                        Case 3

                            HeaderTdSetting(tdHeader, "350px", "Description", "1")
                            trHeaderS.Controls.Add(tdHeader)

                        Case 4
                            HeaderTdSetting(tdHeader, "50px", "Comments", "1")
                            trHeaderS.Controls.Add(tdHeader)

                        Case 5
                            HeaderTdSetting(tdHeader, "50px", "Price Option", "1")
                            trHeaderS.Controls.Add(tdHeader)

                        Case 6
                            HeaderTdSetting(tdHeader, "50px", "Print Tech", "1")
                            trHeaderS.Controls.Add(tdHeader)

                    End Select

                Next
                trHeaderS.Height = 30
                tblcustomize.Controls.Add(trHeaderS)
                tblcustomize.Visible = False
                lblcustomize.Visible = False
                'end pt changes

                If dsQues.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsQues.Tables(0).Rows.Count - 1
                        If dsQues.Tables(0).Rows(i).Item("ISDEFAULT").ToString() = "Y" Then
                            trQues = New TableRow
                            lnkQues = New LinkButton

                            For b = 1 To 6
                                tdId = New TableCell
                                Select Case b
                                    Case 1
                                        chck = New CheckBox
                                        hid = New HiddenField
                                        ' chck.ID = "chckBut" + (i + 1).ToString()
                                        'hid.ID = "hidchck" + (i + 1).ToString()
                                        chck.ID = "chk" + dsQues.Tables(0).Rows(i).Item("TERMID").ToString()
                                        If dsQues.Tables(0).Rows(i).Item("ACCEPTED_TERMS") = "Y" Then
                                            chck.Checked = True
                                        Else
                                            chck.Checked = False
                                        End If
                                        'chck.AutoPostBack = True
                                        'AddHandler chck.CheckedChanged, AddressOf CheckBox_Check
                                        chck.Enabled = False
                                        tdId.Width = 55
                                        tdId.Controls.Add(hid)
                                        tdId.Controls.Add(chck)

                                    Case 2
                                        lblItem = New Label
                                        'InnerTdSetting(lbl, "", "Center")
                                        lblItem.Text = dsQues.Tables(0).Rows(i).Item("TITLE").ToString()
                                        lblItem.ID = "lbl" + dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "." + (i + 1).ToString()
                                        lblItem.Style.Add("font-family", "Verdana")
                                        lblItem.Style.Add("width", "300px")
                                        lblItem.Style.Add("height", "14px")
                                        tdId.Controls.Add(lblItem)

                                    Case 3
                                        lblTerm = New Label
                                        'InnerTdSetting(lbl, "", "Center")
                                        lblTerm.Text = dsQues.Tables(0).Rows(i).Item("DESCRIPTION").ToString()
                                        lblTerm.ID = "lblt" + dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "_T" + (i + 1).ToString()
                                        lblTerm.Style.Add("font-family", "Verdana")
                                        lblTerm.Style.Add("width", "350px")
                                        lblTerm.Style.Add("height", "14px")
                                        tdId.Controls.Add(lblTerm)

                                    Case 4
                                        lblcom = New Label
                                        HidSeqQue = New HiddenField
                                        InnerTdSetting(tdId, "", "Center")
                                        lblcom.Text = dsQues.Tables(0).Rows(i).Item("COMMENTS").ToString()
                                        lblcom.ID = "lblcom" + dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "_" + (i + 1).ToString()
                                        ' HidSeqQue.Value = txtCom.Text
                                        lblcom.Font.Size = 10
                                        'txtCom.MaxLength = 600
                                        'txtCom.TextMode = TextBoxMode.MultiLine
                                        'lblcom.Style.Add("background-color", "#FEFCA1")
                                        'lblcom.Style.Add("font-family", "Verdana")
                                        'lblcom.Style.Add("width", "300px")
                                        'lblcom.Style.Add("height", "50px")
                                        ' txtCom.AutoPostBack = True

                                        'AddHandler txtCom.TextChanged, AddressOf TextBox_TextChangedI
                                        tdId.Controls.Add(lblcom)
                                        ' tdId.Controls.Add(HidSeqQue)
                                    Case 5
                                        'lblpriceopt = New Label
                                        ddlPriceOptn = New DropDownList
                                        InnerTdSetting(tdId, "", "Center")
                                        GetPriceOptn(ddlPriceOptn, dsQues.Tables(0).Rows(i).Item("PRICEREQID").ToString())

                                        ddlPriceOptn.ID = "ddl" + dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "_D" + (i + 1).ToString()
                                        ' lblpriceopt.Text = dsQues.Tables(0).Rows(i).Item("DESCRIPTION").ToString()  'DESCRIPTION
                                        ' ddlPriceOptn.AutoPostBack = True
                                        If dsQues.Tables(0).Rows(i).Item("TITLE").ToString() = "Add/Remove Color" Then
                                            ddlPriceOptn.Enabled = True
                                        Else
                                            ddlPriceOptn.Enabled = False
                                        End If
                                        ddlPriceOptn.Enabled = False
                                        'AddHandler ddlPriceOptn.SelectedIndexChanged, AddressOf DdlPriceoptn_ChangeE
                                        tdId.Controls.Add(ddlPriceOptn)

                                    Case 6
                                        ddlPrintTech = New DropDownList
                                        InnerTdSetting(tdId, "", "Center")
                                        GetPrintTech(ddlPrintTech, dsQues.Tables(0).Rows(i).Item("PRINTINGTECHID").ToString())
                                        ddlPrintTech.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "_PT" + (i + 1).ToString()
                                        'ddlPrintTech.AutoPostBack = True
                                        If dsQues.Tables(0).Rows(i).Item("TITLE").ToString() = "Add/Remove Color" Then
                                            ddlPrintTech.Enabled = True
                                        Else
                                            ddlPrintTech.Enabled = False
                                        End If
                                        ddlPrintTech.Enabled = False
                                        'AddHandler ddlPrintTech.SelectedIndexChanged, AddressOf DdlPrintTech_ChangeE
                                        tdId.Controls.Add(ddlPrintTech)
                                End Select
                                trQues.Controls.Add(tdId)
                            Next
                            If i Mod 2 = 0 Then
                                trQues.CssClass = "AlterNateColor1"
                            Else
                                trQues.CssClass = "AlterNateColor2"
                            End If
                            tblEditQ.Controls.Add(trQues)

                            'PT CHANGES
                        Else
                            tblcustomize.Visible = True
                            lblcustomize.Visible = True
                            lblcustomize.Text = "<br>Customize Terms:"

                            trQues = New TableRow
                            lnkQues = New LinkButton

                            For b = 1 To 6
                                tdId = New TableCell
                                Select Case b
                                    Case 1
                                        chck = New CheckBox
                                        hid = New HiddenField
                                        ' chck.ID = "chckBut" + (i + 1).ToString()
                                        'hid.ID = "hidchck" + (i + 1).ToString()
                                        chck.ID = "chk" + dsQues.Tables(0).Rows(i).Item("TERMID").ToString()
                                        If dsQues.Tables(0).Rows(i).Item("ACCEPTED_TERMS") = "Y" Then
                                            chck.Checked = True
                                        Else
                                            chck.Checked = False
                                        End If
                                        'chck.AutoPostBack = True
                                        'AddHandler chck.CheckedChanged, AddressOf CheckBox_Check
                                        chck.Enabled = False
                                        tdId.Width = 55
                                        tdId.Controls.Add(hid)
                                        tdId.Controls.Add(chck)

                                    Case 2
                                        lblItem = New Label
                                        'InnerTdSetting(lbl, "", "Center")
                                        lblItem.Text = dsQues.Tables(0).Rows(i).Item("TITLE").ToString()
                                        lblItem.ID = "lbl" + dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "." + (i + 1).ToString()
                                        lblItem.Style.Add("font-family", "Verdana")
                                        lblItem.Style.Add("width", "300px")
                                        lblItem.Style.Add("height", "14px")
                                        tdId.Controls.Add(lblItem)

                                    Case 3
                                        lblTerm = New Label
                                        'InnerTdSetting(lbl, "", "Center")
                                        lblTerm.Text = dsQues.Tables(0).Rows(i).Item("DESCRIPTION").ToString()
                                        lblTerm.ID = "lblt" + dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "_T" + (i + 1).ToString()
                                        lblTerm.Style.Add("font-family", "Verdana")
                                        lblTerm.Style.Add("width", "350px")
                                        lblTerm.Style.Add("height", "14px")
                                        tdId.Controls.Add(lblTerm)

                                    Case 4
                                        lblcom = New Label
                                        HidSeqQue = New HiddenField
                                        InnerTdSetting(tdId, "", "Center")
                                        lblcom.Text = dsQues.Tables(0).Rows(i).Item("COMMENTS").ToString()
                                        lblcom.ID = "lblcom" + dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "_" + (i + 1).ToString()
                                        ' HidSeqQue.Value = txtCom.Text
                                        lblcom.Font.Size = 10
                                        'txtCom.MaxLength = 600
                                        'txtCom.TextMode = TextBoxMode.MultiLine
                                        'lblcom.Style.Add("background-color", "#FEFCA1")
                                        'lblcom.Style.Add("font-family", "Verdana")
                                        'lblcom.Style.Add("width", "300px")
                                        'lblcom.Style.Add("height", "50px")
                                        ' txtCom.AutoPostBack = True

                                        'AddHandler txtCom.TextChanged, AddressOf TextBox_TextChangedI
                                        tdId.Controls.Add(lblcom)
                                        ' tdId.Controls.Add(HidSeqQue)
                                    Case 5
                                        'lblpriceopt = New Label
                                        ddlPriceOptn = New DropDownList
                                        InnerTdSetting(tdId, "", "Center")
                                        GetPriceOptn(ddlPriceOptn, dsQues.Tables(0).Rows(i).Item("PRICEREQID").ToString())

                                        ddlPriceOptn.ID = "ddl" + dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "_D" + (i + 1).ToString()
                                        ' lblpriceopt.Text = dsQues.Tables(0).Rows(i).Item("DESCRIPTION").ToString()  'DESCRIPTION
                                        ' ddlPriceOptn.AutoPostBack = True
                                        If dsQues.Tables(0).Rows(i).Item("TITLE").ToString() = "Add/Remove Color" Then
                                            ddlPriceOptn.Enabled = True
                                        Else
                                            ddlPriceOptn.Enabled = False
                                        End If
                                        ddlPriceOptn.Enabled = False
                                        'AddHandler ddlPriceOptn.SelectedIndexChanged, AddressOf DdlPriceoptn_ChangeE
                                        tdId.Controls.Add(ddlPriceOptn)

                                    Case 6
                                        ddlPrintTech = New DropDownList
                                        InnerTdSetting(tdId, "", "Center")
                                        GetPrintTech(ddlPrintTech, dsQues.Tables(0).Rows(i).Item("PRINTINGTECHID").ToString())
                                        ddlPrintTech.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "_PT" + (i + 1).ToString()
                                        'ddlPrintTech.AutoPostBack = True
                                        If dsQues.Tables(0).Rows(i).Item("TITLE").ToString() = "Add/Remove Color" Then
                                            ddlPrintTech.Enabled = True
                                        Else
                                            ddlPrintTech.Enabled = False
                                        End If
                                        ddlPrintTech.Enabled = False
                                        'AddHandler ddlPrintTech.SelectedIndexChanged, AddressOf DdlPrintTech_ChangeE
                                        tdId.Controls.Add(ddlPrintTech)
                                End Select
                                trQues.Controls.Add(tdId)
                            Next
                            If i Mod 2 = 0 Then
                                trQues.CssClass = "AlterNateColor1"
                            Else
                                trQues.CssClass = "AlterNateColor2"
                            End If
                            tblcustomize.Controls.Add(trQues)
                            'END PT CHANGES
                        End If
                    Next
                End If

                Dim DsTermsByVendor As New DataSet()
                Dim trHeaderAT As New TableRow
                Dim tdHeaderAT As New TableCell
                Dim trInnerAT As New TableRow
                Dim tdInnerAT As New TableCell
                Dim txtDes As New TextBox
                Dim txtComment As New TextBox
                Dim lbldes As New Label
                Dim lblcomment As Label
                Dim ddlPriceOptnAT As New DropDownList
                Dim ddlPrintTechAT As New DropDownList
                tblAddTerm.Rows.Clear()

                DsTermsByVendor = objGetdata.CheckAddtnTermByVendorID(Session("ShidRfpID").ToString(), Session("SVendorID"))

                For i = 1 To 6
                    tdHeaderAT = New TableCell
                    Select Case i
                        Case 1
                            HeaderTdSetting(tdHeaderAT, "50px", "" + i.ToString() + "", "1")
                            trHeaderAT.Controls.Add(tdHeaderAT)
                        Case 2
                            HeaderTdSetting(tdHeaderAT, "250px", "Additional Terms", "1")
                            trHeaderAT.Controls.Add(tdHeaderAT)
                        Case 3
                            HeaderTdSetting(tdHeaderAT, "350px", "Description", "1")
                            trHeaderAT.Controls.Add(tdHeaderAT)
                        Case 4
                            HeaderTdSetting(tdHeaderAT, "50px", "Comments", "1")
                            trHeaderAT.Controls.Add(tdHeaderAT)
                        Case 5
                            HeaderTdSetting(tdHeaderAT, "50px", "Price Option", "1")
                            trHeaderAT.Controls.Add(tdHeaderAT)
                        Case 6
                            HeaderTdSetting(tdHeaderAT, "50px", "Print Tech", "1")
                            trHeaderAT.Controls.Add(tdHeaderAT)
                    End Select
                Next
                trHeaderAT.Height = 30
                tblAddTerm.Controls.Add(trHeaderAT)

                For j = 0 To DsTermsByVendor.Tables(0).Rows.Count - 1
                    trInnerAT = New TableRow
                    For k = 0 To 5
                        tdInnerAT = New TableCell
                        If k = 0 Then
                            InnerTdSetting(tdInnerAT, "", "Center")
                            tdInnerAT.Text = ""
                            trInnerAT.Controls.Add(tdInnerAT)
                        ElseIf k = 1 Then
                            InnerTdSetting(tdInnerAT, "", "Center")
                            tdInnerAT.Text = DsTermsByVendor.Tables(0).Rows(j).Item("TITLE").ToString()
                            trInnerAT.Controls.Add(tdInnerAT)
                        ElseIf k = 2 Then
                            lbldes = New Label
                            InnerTdSetting(tdInnerAT, "", "Center")
                            lbldes.Text = DsTermsByVendor.Tables(0).Rows(j).Item("ADDTERMDES").ToString()
                            lbldes.ID = DsTermsByVendor.Tables(0).Rows(j).Item("ADDTERMID").ToString() + "_ATD_" + j.ToString()
                            lbldes.Font.Size = 10
                            ' txtDes.MaxLength = 99
                            'txtDes.Style.Add("background-color", "#FEFCA1")
                            'txtDes.Style.Add("font-family", "Verdana")
                            'txtDes.AutoPostBack = True

                            tdInnerAT.Controls.Add(lbldes)
                            trInnerAT.Controls.Add(tdInnerAT)
                        ElseIf k = 3 Then
                            lblcomment = New Label
                            InnerTdSetting(tdInnerAT, "", "Center")
                            lblcomment.Text = DsTermsByVendor.Tables(0).Rows(j).Item("COMMENTS").ToString()
                            lblcomment.ID = DsTermsByVendor.Tables(0).Rows(j).Item("ADDTERMID").ToString() + "_ATC_" + j.ToString()
                            lblcomment.Font.Size = 10
                            'txtComment.MaxLength = 499
                            'txtComment.TextMode = TextBoxMode.MultiLine
                            'txtComment.Style.Add("width", "300px")
                            'txtComment.Style.Add("height", "50px")
                            'txtComment.Style.Add("background-color", "#FEFCA1")
                            'txtComment.Style.Add("font-family", "Verdana")
                            'txtComment.AutoPostBack = True
                            'txtComment.Attributes.Add("onchange", "javascript:return CheckSP(this,'" + txtComment.Text + "');")
                            'AddHandler txtComment.TextChanged, AddressOf txtAT_TextChangedAT
                            tdInnerAT.Controls.Add(lblcomment)
                            trInnerAT.Controls.Add(tdInnerAT)
                        ElseIf k = 4 Then
                            ddlPriceOptnAT = New DropDownList
                            InnerTdSetting(tdInnerAT, "", "Center")
                            GetPriceOptn(ddlPriceOptnAT, DsTermsByVendor.Tables(0).Rows(j).Item("PRICEREQID").ToString())
                            ddlPriceOptnAT.ID = DsTermsByVendor.Tables(0).Rows(j).Item("ADDTERMID").ToString() + "_ATPO_" + j.ToString()
                            'ddlPriceOptnAT.AutoPostBack = True
                            'If DsTermsByVendor.Tables(0).Rows(j).Item("TITLE").ToString() = "Add/Remove Color" Then
                            '    ddlPriceOptnAT.Enabled = True
                            'Else
                            '    ddlPriceOptnAT.Enabled = False
                            'End If
                            ddlPriceOptnAT.Enabled = False

                            'AddHandler ddlPriceOptnAT.SelectedIndexChanged, AddressOf DdlAddTerm_ChangeE
                            tdInnerAT.Controls.Add(ddlPriceOptnAT)
                            trInnerAT.Controls.Add(tdInnerAT)
                        ElseIf k = 5 Then
                            ddlPrintTechAT = New DropDownList
                            InnerTdSetting(tdInnerAT, "", "Center")
                            GetPrintTech(ddlPrintTechAT, DsTermsByVendor.Tables(0).Rows(j).Item("PRINTINGTECHID").ToString())
                            ddlPrintTechAT.ID = DsTermsByVendor.Tables(0).Rows(j).Item("ADDTERMID").ToString() + "_ATPT_" + j.ToString()
                            ' ddlPrintTechAT.AutoPostBack = True
                            If DsTermsByVendor.Tables(0).Rows(j).Item("TITLE").ToString() = "Add/Remove Color" Then
                                ddlPrintTechAT.Enabled = True
                            Else
                                ddlPrintTechAT.Enabled = False
                            End If
                            ddlPrintTechAT.Enabled = False

                            '  AddHandler ddlPrintTechAT.SelectedIndexChanged, AddressOf DdlAddTerm_ChangeE
                            tdInnerAT.Controls.Add(ddlPrintTechAT)
                            trInnerAT.Controls.Add(tdInnerAT)
                        End If
                    Next
                    trInnerAT.CssClass = "AlterNateColor1"
                    tblAddTerm.Controls.Add(trInnerAT)
                Next

            End If

        Catch ex As Exception
            lblError.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPriceOptn(ByVal ddl As DropDownList, ByVal Id As String)
        Dim ds As New DataSet
        Try
            Dim list As New ListItem
            'Add User
            ddl.Items.Clear()
            list.Value = "0"
            list.Text = "Nothing Selected"
            ddl.Items.Add(list)
            ddl.AppendDataBoundItems = True

            ds = objGetdata.GetPriceOptnForTerms(Session("ShidRfpID"), "")
            With ddl
                .DataSource = ds
                .DataTextField = "DESCRIPTION"
                .DataValueField = "COLVALUEID"
                .DataBind()
            End With

            If Id = "" Or Id = "0" Then
                ddl.SelectedValue = "0"
            Else
                ddl.SelectedValue = Id
            End If

        Catch ex As Exception
            lblError.Text = "Error:GetPriceOptn:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetPrintTech(ByVal ddl As DropDownList, ByVal Id As String)
        Dim ds As New DataSet
        Try
            Dim list As New ListItem
            'Add User
            ddl.Items.Clear()
            list.Value = "0"
            list.Text = "Nothing Selected"
            ddl.Items.Add(list)
            ddl.AppendDataBoundItems = True

            ds = objGetdata.GetPrintTechForTerms(Session("ShidRfpID"), "")
            With ddl
                .DataSource = ds
                .DataTextField = "DESCRIPTION"
                .DataValueField = "COLVALUEID"
                .DataBind()
            End With

            If Id <> "" Then
                ddl.SelectedValue = Id
            Else
                ddl.SelectedValue = "0"
            End If

        Catch ex As Exception
            lblError.Text = "Error:GetPrintTech:" + ex.Message.ToString() + ""
        End Try
    End Sub

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
            lblError.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
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
            lblError.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
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
            lblError.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

#End Region

#Region "Price"

    Protected Sub GetPrice()
        Dim trHeader As New TableRow
        Dim tdHeader As TableCell
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim DsPriceData As New DataSet()
        Dim DtPriceData As New DataTable()
        Dim DvPriceData As New DataView()
        Dim DsSKUByRfpID As New DataSet()
        Dim DsPrefByID As New DataSet()
        Dim txtprice As TextBox
        Dim lblprice As Label
        Dim hidSpecID As HiddenField
        Dim lblSpecID As Label
        Dim TitleH As String = String.Empty
        Try

            If Session("ShidRfpID") = Nothing Or Session("SVendorID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else
                tblPrice.Rows.Clear()
                DsSKUByRfpID = objGetdata.GetSKUByRfpID(Session("ShidRfpID"), Session("BUserID"))
                DsPrefByID = objGetdata.GetPrefbyID(Session("ShidRfpID"))
                DsPriceData = objGetdata.GetPriceByRfpVenID(Session("ShidRfpID"), Session("SVendorID"))
                DvPriceData = DsPriceData.Tables(0).DefaultView
                If DsSKUByRfpID.Tables(0).Rows.Count > 0 Then
                    Session("SkuCOuntPrice") = DsSKUByRfpID.Tables(0).Rows.Count
                    lblNoRecordPrice.Visible = False
                    ' btnUpdatePrice.Visible = True
                    For k = 1 To 2
                        trHeader = New TableRow
                        For i = 1 To 3
                            If k = 1 Then
                                Select Case i
                                    Case 1
                                        tdHeader = New TableCell
                                        HeaderTdSetting(tdHeader, "50px", "SKU #", "1")
                                        trHeader.Controls.Add(tdHeader)
                                    Case 2
                                        tdHeader = New TableCell
                                        HeaderTdSetting(tdHeader, "220px", "SKU Des", "1")
                                        trHeader.Controls.Add(tdHeader)
                                    Case 3
                                        tdHeader = New TableCell
                                        HeaderTdSetting(tdHeader, "120px", "PRICE", "1")
                                        trHeader.Controls.Add(tdHeader)
                                End Select
                            ElseIf k = 2 Then
                                Select Case i
                                    Case 1
                                        tdHeader = New TableCell
                                        HeaderTdSetting(tdHeader, "50px", "", "1")
                                        trHeader.Controls.Add(tdHeader)
                                    Case 2
                                        tdHeader = New TableCell
                                        HeaderTdSetting(tdHeader, "220px", "", "1")
                                        trHeader.Controls.Add(tdHeader)
                                    Case 3
                                        tdHeader = New TableCell
                                        'Try
                                        '    TitleH = "(" + DsPrefByID.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + DsPrefByID.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                                        'Catch ex As Exception
                                        '    TitleH = "US$"
                                        'End Try
                                        'Header2TdSetting(tdHeader, "120px", TitleH, "1")
                                        Header2TdSetting(tdHeader, "120px", "(US$/M)", "1")
                                        trHeader.Controls.Add(tdHeader)
                                End Select
                            End If
                        Next
                        trHeader.Height = 30
                        tblPrice.Controls.Add(trHeader)
                    Next

                    'For Data
                    For i = 0 To DsSKUByRfpID.Tables(0).Rows.Count - 1 'For Number of terms
                        trInner = New TableRow
                        For j = 0 To 2
                            Select Case j
                                Case 0
                                    tdInner = New TableCell
                                    hidSpecID = New HiddenField
                                    lblSpecID = New Label
                                    hidSpecID.Value = DsSKUByRfpID.Tables(0).Rows(i).Item("SKUID").ToString()
                                    hidSpecID.ID = "hidSpecIDPrice" + i.ToString()
                                    lblSpecID.Text = DsSKUByRfpID.Tables(0).Rows(i).Item("SKUID").ToString()
                                    InnerTdSetting(tdInner, "50px", "")
                                    tdInner.Controls.Add(lblSpecID)
                                    tdInner.Controls.Add(hidSpecID)
                                    trInner.Controls.Add(tdInner)
                                Case 1
                                    tdInner = New TableCell
                                    tdInner.Text = DsSKUByRfpID.Tables(0).Rows(i).Item("SKUDES").ToString()
                                    InnerTdSetting(tdInner, "350px", "")
                                    trInner.Controls.Add(tdInner)
                                Case 2
                                    tdInner = New TableCell
                                    InnerTdSetting(tdInner, "120px", "")
                                    lblprice = New Label
                                    ' txtprice.CssClass = "SmallTextBox"
                                    lblprice.Width = 70
                                    lblprice.ID = "lblPrice" + i.ToString()
                                    DvPriceData.RowFilter = "SKUID=" + DsSKUByRfpID.Tables(0).Rows(i).Item("SKUID").ToString()
                                    DtPriceData = DvPriceData.ToTable()
                                    Try
                                        lblprice.Text = FormatNumber(DtPriceData.Rows(0).Item("PRICE").ToString(), 0)
                                    Catch ex As Exception
                                        lblprice.Text = "0"
                                    End Try
                                    'txtprice.MaxLength = 12
                                    tdInner.Controls.Add(lblprice)
                                    trInner.Controls.Add(tdInner)
                            End Select
                        Next
                        If (i Mod 2 = 0) Then
                            trInner.CssClass = "AlterNateColor1"
                        Else
                            trInner.CssClass = "AlterNateColor2"
                        End If
                        tblPrice.Controls.Add(trInner)
                    Next
                    'End
                Else
                    lblNoRecordPrice.Visible = True
                    ' btnUpdatePrice.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetPrice:" + ex.Message.ToString()
        End Try
    End Sub


#End Region

#Region "Price&Cost Manager"

    Protected Sub GetPriceCost()
        Dim trHeader As New TableRow
        Dim tdHeader As TableCell
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim DsPriceData As New DataSet()
        Dim DtPriceData As New DataTable()
        Dim DvPriceData As New DataView()
        Dim DsSKUByRfpID As New DataSet()
        Dim hidSpecIDPC As New HiddenField
        Dim lblSpecIDPC As Label
        Dim txtpricePC As TextBox
        Dim lblpricepc As Label
        Dim DsPriceCols As New DataSet()
        Dim DsPrefByID As New DataSet()
        Dim TitleH As String = String.Empty
        Try
            If Session("ShidRfpID") = Nothing Or Session("SVendorID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else
                tblPC.Rows.Clear()
                DsPriceData = objGetdata.GetPriceByRfpVenID(Session("ShidRfpID"), Session("SVendorID"))
                DsPrefByID = objGetdata.GetPrefbyID(Session("ShidRfpID"))
                DvPriceData = DsPriceData.Tables(0).DefaultView
                DsSKUByRfpID = objGetdata.GetSKUByRfpID(Session("ShidRfpID"), Session("BUserID"))
                DsPriceCols = objGetdata.GetPriceCosCol()
                If DsSKUByRfpID.Tables(0).Rows.Count > 0 Then
                    Session("SkuCOuntPC") = DsSKUByRfpID.Tables(0).Rows.Count
                    lblNoDataPriceCost.Visible = False
                    ' BtnUpdate.Visible = True
                    For k = 1 To 2
                        trHeader = New TableRow
                        For i = 1 To 3
                            If k = 1 Then
                                Select Case i
                                    Case 1
                                        tdHeader = New TableCell
                                        HeaderTdSetting(tdHeader, "50px", "SKU #", "1")
                                        trHeader.Controls.Add(tdHeader)
                                    Case 2
                                        tdHeader = New TableCell
                                        HeaderTdSetting(tdHeader, "220px", "SKU Des", "1")
                                        trHeader.Controls.Add(tdHeader)
                                    Case 3
                                        For j = 0 To 15
                                            tdHeader = New TableCell
                                            HeaderTdSetting(tdHeader, "120px", "" + DsPriceCols.Tables(0).Rows(0).Item("DES" + (j + 1).ToString()) + "", "1")
                                            trHeader.Controls.Add(tdHeader)
                                        Next
                                End Select
                            ElseIf k = 2 Then
                                Select Case i
                                    Case 1
                                        tdHeader = New TableCell
                                        HeaderTdSetting(tdHeader, "50px", "", "1")
                                        trHeader.Controls.Add(tdHeader)
                                    Case 2
                                        tdHeader = New TableCell
                                        HeaderTdSetting(tdHeader, "220px", "", "1")
                                        trHeader.Controls.Add(tdHeader)
                                    Case 3
                                        For j = 0 To 15
                                            tdHeader = New TableCell
                                            'Try
                                            '    TitleH = "(" + DsPrefByID.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + DsPrefByID.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                                            'Catch ex As Exception
                                            '    TitleH = "US$"
                                            'End Try

                                            'Header2TdSetting(tdHeader, "120px", TitleH, "1")
                                            Header2TdSetting(tdHeader, "120px", "(US$/M)", "1")
                                            trHeader.Controls.Add(tdHeader)
                                        Next
                                End Select
                            End If
                        Next
                        trHeader.Height = 30
                        tblPC.Controls.Add(trHeader)
                    Next

                    'For Data
                    For i = 0 To DsSKUByRfpID.Tables(0).Rows.Count - 1 'For Number of terms                    
                        trInner = New TableRow
                        DvPriceData.RowFilter = "SKUID=" + DsSKUByRfpID.Tables(0).Rows(i).Item("SKUID").ToString()
                        DtPriceData = DvPriceData.ToTable()
                        For j = 0 To 2
                            Select Case j
                                Case 0
                                    tdInner = New TableCell
                                    hidSpecIDPC = New HiddenField
                                    lblSpecIDPC = New Label
                                    hidSpecIDPC.Value = DsSKUByRfpID.Tables(0).Rows(i).Item("SKUID").ToString()
                                    hidSpecIDPC.ID = "hidSpecIDPC" + i.ToString()
                                    lblSpecIDPC.Text = DsSKUByRfpID.Tables(0).Rows(i).Item("SKUID").ToString()
                                    InnerTdSetting(tdInner, "50px", "")
                                    tdInner.Controls.Add(lblSpecIDPC)
                                    tdInner.Controls.Add(hidSpecIDPC)
                                    trInner.Controls.Add(tdInner)
                                Case 1
                                    tdInner = New TableCell
                                    tdInner.Text = DsSKUByRfpID.Tables(0).Rows(i).Item("SKUDES").ToString()
                                    InnerTdSetting(tdInner, "220px", "")
                                    trInner.Controls.Add(tdInner)
                                Case 2
                                    For l = 0 To 15 'for number of vendors
                                        tdInner = New TableCell
                                        InnerTdSetting(tdInner, "120px", "")
                                        lblpricepc = New Label
                                        ' txtpricePC.CssClass = "SmallTextBox"
                                        lblpricepc.Width = 70
                                        lblpricepc.ID = "lblPricePC" + i.ToString() + "_" + l.ToString() + ""
                                        If l = 0 Then
                                            Try
                                                lblpricepc.Text = FormatNumber(DtPriceData.Rows(0).Item("PRICE").ToString(), 0)
                                            Catch ex As Exception
                                                lblpricepc.Text = "0"
                                            End Try
                                        ElseIf l = 1 Then
                                            Try
                                                lblpricepc.Text = FormatNumber(DtPriceData.Rows(0).Item("SETUP").ToString(), 0)
                                            Catch ex As Exception
                                                lblpricepc.Text = "0"
                                            End Try
                                        Else
                                            Try
                                                lblpricepc.Text = FormatNumber(DtPriceData.Rows(0).Item("M" + (l - 1).ToString()).ToString(), 0)
                                            Catch ex As Exception
                                                lblpricepc.Text = "0"
                                            End Try
                                        End If
                                        'txtpricePC.MaxLength = 12
                                        tdInner.Controls.Add(lblpricepc)
                                        trInner.Controls.Add(tdInner)
                                    Next
                            End Select
                        Next
                        If (i Mod 2 = 0) Then
                            trInner.CssClass = "AlterNateColor1"
                        Else
                            trInner.CssClass = "AlterNateColor2"
                        End If
                        tblPC.Controls.Add(trInner)
                    Next
                Else
                    lblNoDataPriceCost.Visible = True
                    ' BtnUpdate.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetPriceCost:" + ex.Message.ToString()
        End Try
    End Sub

#End Region

#Region "RFQ"

    Protected Sub GetRFQ()
        Dim trHeader As New TableRow
        Dim tdHeader As TableCell
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim DsPriceData As New DataSet()
        Dim DtPriceData As New DataTable()
        Dim DvPriceData As New DataView()
        Dim DsSKUByRfpID As New DataSet()
        Dim hidSpecID As New HiddenField
        Dim lblSpecIDPC As Label
        Dim txtPrintT As TextBox
        Dim txtBagM As TextBox
        Dim lblprint As Label
        Dim lblbagm As Label
        Dim DsPriceCols As New DataSet()
        Try
            If Session("ShidRfpID") = Nothing Or Session("SVendorID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else
                tbleRFQ.Rows.Clear()
                DsSKUByRfpID = objGetdata.GetSpecRFQDetails(Session("ShidRfpID"), Session("BUserID"))
                If DsSKUByRfpID.Tables(0).Rows.Count > 0 Then
                    Session("SpecCount") = DsSKUByRfpID.Tables(0).Rows.Count
                    '  lblNoDataPriceCost.Visible = False

                    trHeader = New TableRow
                    For i = 1 To 5

                        Select Case i
                            Case 1
                                tdHeader = New TableCell
                                HeaderTdSetting(tdHeader, "50px", "SKUID #", "1")
                                trHeader.Controls.Add(tdHeader)
                            Case 2
                                tdHeader = New TableCell
                                HeaderTdSetting(tdHeader, "220px", "Spec Des", "1")
                                trHeader.Controls.Add(tdHeader)
                            Case 3
                                tdHeader = New TableCell
                                HeaderTdSetting(tdHeader, "200px", "Structure", "1")
                                trHeader.Controls.Add(tdHeader)

                            Case 4
                                If DsSKUByRfpID.Tables(0).Rows(0).Item("OPOPTIONID").ToString() = "1" Or DsSKUByRfpID.Tables(0).Rows(0).Item("OPOPTIONID").ToString() = "1,2" Then
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "250px", "Print technology 3", "1")
                                    trHeader.Controls.Add(tdHeader)
                                End If


                            Case 5
                                If DsSKUByRfpID.Tables(0).Rows(0).Item("OPOPTIONID").ToString() = "2" Or DsSKUByRfpID.Tables(0).Rows(0).Item("OPOPTIONID").ToString() = "1,2" Then
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "250px", "Bag making machine 5", "1")
                                    trHeader.Controls.Add(tdHeader)
                                End If

                        End Select

                    Next
                    trHeader.Height = 30
                    tbleRFQ.Controls.Add(trHeader)


                    'For Data
                    For i = 0 To DsSKUByRfpID.Tables(0).Rows.Count - 1 'For Number of terms                    
                        trInner = New TableRow
                        'DvPriceData.RowFilter = "SPECID=" + DsSKUByRfpID.Tables(0).Rows(i).Item("SPECID").ToString()
                        'DtPriceData = DvPriceData.ToTable()
                        For j = 0 To 4
                            Select Case j
                                Case 0
                                    tdInner = New TableCell
                                    hidSpecID = New HiddenField
                                    lblSpecIDPC = New Label
                                    ' hidSpecID.Value = DsSKUByRfpID.Tables(0).Rows(i).Item("SPECID").ToString()
                                    hidSpecID.ID = "hidSpecID" + i.ToString()
                                    lblSpecIDPC.Text = DsSKUByRfpID.Tables(0).Rows(i).Item("SKUID").ToString()
                                    InnerTdSetting(tdInner, "50px", "")
                                    tdInner.Controls.Add(lblSpecIDPC)
                                    ' tdInner.Controls.Add(hidSpecID)
                                    trInner.Controls.Add(tdInner)
                                Case 1
                                    tdInner = New TableCell
                                    tdInner.Text = DsSKUByRfpID.Tables(0).Rows(i).Item("SKUDES").ToString()
                                    InnerTdSetting(tdInner, "220px", "")
                                    trInner.Controls.Add(tdInner)
                                Case 2
                                    tdInner = New TableCell
                                    tdInner.Text = DsSKUByRfpID.Tables(0).Rows(i).Item("STRUCTURE").ToString()
                                    InnerTdSetting(tdInner, "220px", "")
                                    trInner.Controls.Add(tdInner)
                                Case 3
                                    If DsSKUByRfpID.Tables(0).Rows(0).Item("OPOPTIONID").ToString() = "1" Or DsSKUByRfpID.Tables(0).Rows(0).Item("OPOPTIONID").ToString() = "1,2" Then
                                        tdInner = New TableCell
                                        InnerTdSetting(tdInner, "120px", "")
                                        lblprint = New Label
                                        'txtPrintT.CssClass = "SmallTextBox"
                                        lblprint.Width = 180
                                        lblprint.ID = "lblPrintT" + i.ToString()
                                        Try
                                            lblprint.Text = DsSKUByRfpID.Tables(0).Rows(i).Item("PRINTTECH").ToString()
                                        Catch ex As Exception
                                            lblprint.Text = ""
                                        End Try
                                        'txtPrintT.MaxLength = 12
                                        tdInner.Controls.Add(lblprint)
                                        trInner.Controls.Add(tdInner)

                                    End If


                                Case 4
                                    If DsSKUByRfpID.Tables(0).Rows(0).Item("OPOPTIONID").ToString() = "2" Or DsSKUByRfpID.Tables(0).Rows(0).Item("OPOPTIONID").ToString() = "1,2" Then
                                        tdInner = New TableCell
                                        InnerTdSetting(tdInner, "120px", "")
                                        lblbagm = New Label
                                        ' txtBagM.CssClass = "SmallTextBox"
                                        lblbagm.Width = 180
                                        lblbagm.ID = "lblBagM" + i.ToString()

                                        Try
                                            lblbagm.Text = DsSKUByRfpID.Tables(0).Rows(i).Item("BAGMAKING").ToString()
                                        Catch ex As Exception
                                            lblbagm.Text = ""
                                        End Try
                                        ' txtBagM.MaxLength = 12
                                        tdInner.Controls.Add(lblbagm)
                                        trInner.Controls.Add(tdInner)

                                    End If

                            End Select
                        Next
                        If (i Mod 2 = 0) Then
                            trInner.CssClass = "AlterNateColor1"
                        Else
                            trInner.CssClass = "AlterNateColor2"
                        End If
                        tbleRFQ.Controls.Add(trInner)
                    Next
                Else
                    lblNoDataRFQ.Visible = True
                End If
            End If

        Catch ex As Exception
            lblError.Text = "Error:GetRFQ:" + ex.Message.ToString()
        End Try
    End Sub


#End Region

#Region "Price Option"

    Protected Sub chkskulvldata()
        Dim Ds As New DataSet
        Dim DsSku As New DataSet
        Try
            '    lblnosku.Visible = False
            If isSkuOnly Then
                Ds = objGetdata.GetPriceDes(Session("ShidRfpID"))
                btnpoptsku.Visible = False
                If Ds.Tables(0).Rows.Count > 0 Then
                    If hidPriceId.Value = "" Then
                        lbkpriceopt.Text = Ds.Tables(0).Rows(0).Item("PRICEDETAIL").ToString()
                        hidPriceId.Value = Ds.Tables(0).Rows(0).Item("RFPPRICEID").ToString()
                    End If
                End If
            End If
            If hidPriceId.Value <> "" Then
                DsSku = objGetdata.GetMasterDataN(hidPriceId.Value)
                If DsSku.Tables(0).Rows(0).Item("MTYPEID").ToString = "5" Then
                    btnpoptsku.Visible = False
                Else
                    btnpoptsku.Visible = True
                End If
            End If

        Catch ex As Exception
            lblError.Text = "Error:chkskulvldata:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindPRICETable()
        Dim ds As New DataSet
        Dim dsM As New DataSet
        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        Dim lbl As New Label
        Dim hid As New HiddenField

        Dim hidPP As New HiddenField
        Dim strcode As New Label
        Dim hidPRC As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim lblprice As Label
        Dim tdInner As TableCell
        Dim objGetData As New SavvyProGetData()
        Dim dsP As New DataSet

        Dim dscnt As New DataSet
        Dim dsPRC As New DataSet
        Dim IsSku As Boolean = False
        Dim HidColorGrp As New HiddenField
        Dim lblColor As New Label
        Dim DsGetColorByCode As New DataSet()
        Dim DvGetColorByCode As New DataView()
        Dim DtGetColorByCode As New DataTable()
        Try
            If hidPriceId.Value <> "" Then

                ds = objGetData.GetPRICEDATARFPN(hidPriceId.Value, Session("BUserID"), Session("ShidRfpID"))
                dsM = objGetData.GetMasterDataN(hidPriceID.Value)
                dsP = objGetData.GetRFPPRICE(hidPriceID.Value)
                DsGetColorByCode = objGetData.GetCodeByPriceID(hidPriceID.Value)
                DvGetColorByCode = DsGetColorByCode.Tables(0).DefaultView
                lblnosku.Visible = False


                If ds.Tables(0).Rows.Count > 0 Then

                    If dsM.Tables(0).Rows(0).Item("MTYPEID").ToString() <> "5" Then
                        dsPRC = objGetData.GetPriceOP(hidPriceId.Value, Session("SVendorID"))
                    Else
                        dsPRC = objGetData.GetPriceOPSku(hidPriceId.Value, Session("SVendorID"))
                        IsSku = True
                    End If

                    dscnt = objGetData.GetPriceOpCnttype(hidPriceId.Value)

                    If dsP.Tables(0).Rows.Count > 0 Then
                        'lblPriceName.Text = dsP.Tables(0).Rows(0).Item("PRICEDETAIL").ToString()
                    End If
                    For i = 1 To dsM.Tables(0).Rows.Count + 1
                        tdHeader = New TableCell
                        Dim Title As String = String.Empty
                        'Header
                        If i = 1 Then
                            'HeaderTdSetting(tdHeader, "50px", "", "1")
                            'trHeader.Controls.Add(tdHeader)
                        Else
                            If dsM.Tables(0).Rows(i - 2).Item("TBLNAME") = "PRICEREQUIREMENt" Then
                                HeaderTdSetting(tdHeader, "130px", "", "1")
                            Else
                                HeaderTdSetting(tdHeader, "110px", "", "1")
                            End If

                            lbl = New Label
                            hid = New HiddenField
                            ' Link.CssClass = "LinkM"
                            lbl.Text = dsM.Tables(0).Rows(i - 2).Item("DESCRIPTION")
                            If dsM.Tables(0).Rows(i - 2).Item("DESCRIPTION") = "SKU" Then
                                HeaderTdSetting(tdHeader, "200px", "", "1")
                            End If
                            tdHeader.Controls.Add(lbl)
                            trHeader.Controls.Add(tdHeader)
                        End If
                    Next
                    tblPriceOpt.Controls.Add(trHeader)

                    'Inner
                    For i = 1 To ds.Tables(0).Rows.Count
                        trInner = New TableRow
                        Dim strVal As String = ""
                        Dim strc As String = ""
                        Dim CodeCnt As Integer = 1

                        For j = 1 To dsM.Tables(0).Rows.Count + 1
                            Dim dv As New DataView
                            Dim dt As New DataTable
                            If j = 1 Then
                                'tdInner = New TableCell
                                'InnerTdSetting(tdInner, "", "Center")
                                'tdInner.Text = "<b>" + i.ToString() + "</b>"
                                'trInner.Controls.Add(tdInner)
                            Else
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Left")
                                If dsM.Tables(0).Rows(j - 2).Item("TBLNAME").ToString() = "PRICEREQUIREMENT" Or dsM.Tables(0).Rows(j - 2).Item("TBLNAME").ToString() = "OTHERVAL" Or dsM.Tables(0).Rows(j - 2).Item("TBLNAME").ToString() = "PACKAGINGMMNT" Then
                                Else
                                    If strc = "" Then
                                        strc = ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 2).Item("COLMNS").ToString().Replace(" ", "")).ToString()
                                    Else
                                        strc = strc + "#" + ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 2).Item("COLMNS").ToString().Replace(" ", "")).ToString()
                                    End If
                                End If

                                dv = dsPRC.Tables(0).DefaultView()
                                If IsSku Then
                                    dv.RowFilter = "PRICEREQID=" + dsM.Tables(0).Rows(j - 2).Item("GROUPID").ToString().Replace(" ", "") + " AND SKUID='" + strc.ToString() + "'"
                                Else
                                    dv.RowFilter = "PRICEREQID=" + dsM.Tables(0).Rows(j - 2).Item("GROUPID").ToString().Replace(" ", "") + " AND CODE='" + strc.ToString() + "'" 'SEQ=" + i.ToString()
                                End If
                                dt = dv.ToTable()

                                If dsM.Tables(0).Rows(j - 2).Item("TBLNAME").ToString() = "PRICEREQUIREMENT" Then
                                    lblprice = New Label
                                    'txt.CssClass = "SmallTextBox"
                                    lblprice.ID = "lblPRC" + i.ToString() + "_" + (j - 1).ToString()
                                    'Changes done by Pk
                                    Try
                                        If dt.Rows.Count > 0 Then
                                            lblprice.Text = FormatNumber(dt.Rows(0).Item("PRICE").ToString(), 2)
                                        Else
                                            lblprice.Text = FormatNumber(0, 2)
                                        End If
                                    Catch ex As Exception
                                        lblprice.Text = FormatNumber(0, 2)
                                    End Try
                                    'Changes end
                                    'lblprice.MaxLength = 10
                                    ' lblprice.Width = 70
                                    hidPRC = New HiddenField()
                                    hidPRC.ID = "hidPRC" + i.ToString() + "_" + (j - 1).ToString()
                                    hidPRC.Value = dsM.Tables(0).Rows(j - 2).Item("GROUPID").ToString().Replace(" ", "")
                                    tdInner.Controls.Add(hidPRC)
                                    tdInner.Controls.Add(lblprice)
                                    trInner.Controls.Add(tdInner)
                                ElseIf dsM.Tables(0).Rows(j - 2).Item("TBLNAME").ToString() = "OTHERVAL" Then
                                    lblColor = New Label
                                    HidColorGrp = New HiddenField()
                                    HidColorGrp.ID = "hidColorGrp_" + i.ToString()
                                    lblColor.ID = "lblcl" + i.ToString()
                                    Try
                                        If dsM.Tables(0).Rows(0).Item("MTYPEID").ToString() <> "5" Then
                                            DvGetColorByCode.RowFilter = "CODE='" + strc.ToString() + "'"
                                            DtGetColorByCode = DvGetColorByCode.ToTable()
                                            lblColor.Text = DtGetColorByCode.Rows(0).Item("VAL").ToString()
                                            HidColorGrp.Value = DtGetColorByCode.Rows(0).Item("VAL").ToString()
                                        Else
                                            lblColor.Text = ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 2).Item("COLMNS").ToString().Replace(" ", "")).ToString()
                                            HidColorGrp.Value = ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 2).Item("COLMNS").ToString().Replace(" ", "")).ToString()
                                        End If
                                    Catch ex As Exception
                                        lblColor.Text = "0"
                                        HidColorGrp.Value = "0"
                                    End Try
                                    tdInner.Controls.Add(lblColor)
                                    tdInner.Controls.Add(HidColorGrp)
                                    trInner.Controls.Add(tdInner)
                                ElseIf dsM.Tables(0).Rows(j - 2).Item("TBLNAME").ToString() = "PACKAGINGMMNT" Then
                                    Try
                                        tdInner.Text = ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 2).Item("COLMNS").ToString().Replace(" ", "")).ToString()
                                    Catch ex As Exception
                                        tdInner.Text = "0"
                                    End Try
                                    trInner.Controls.Add(tdInner)
                                Else
                                    hidPP = New HiddenField()
                                    strcode = New Label
                                    strcode.ID = "strcode_" + i.ToString()
                                    strcode.Text = "" + ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 2).Item("COLMNS").ToString().Replace(" ", "")).ToString() + ""

                                    If strVal = "" Then
                                        strVal = ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 2).Item("COLMNS").ToString().Replace(" ", "")).ToString()
                                    Else
                                        strVal = strVal + "#" + ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 2).Item("COLMNS").ToString().Replace(" ", "")).ToString()
                                    End If
                                    If dsM.Tables(0).Rows(j - 2).Item("COLMNS").ToString() = "SKUID" Then
                                        strcode.Text = strcode.Text + ":" + ds.Tables(0).Rows(i - 1).Item("SKUDES").ToString() + ""
                                    End If
                                    If CodeCnt = dscnt.Tables(0).Rows(0).Item("cnt").ToString() Then
                                        hidPP.ID = "hidPP" + i.ToString()
                                        hidPP.Value = strVal
                                    End If

                                    CodeCnt += 1
                                    tdInner.Controls.Add(hidPP)
                                    tdInner.Controls.Add(strcode)
                                    trInner.Controls.Add(tdInner)
                                End If
                            End If
                        Next
                        If (i Mod 2 = 0) Then
                            trInner.CssClass = "AlterNateColor1"
                        Else
                            trInner.CssClass = "AlterNateColor2"
                        End If
                        tblPriceOpt.Controls.Add(trInner)
                    Next
                Else
                    lblnosku.Visible = True
                    lblnosku.Text = "No Data found"
                End If
            Else
                lblnosku.Visible = True
                lblnosku.Text = "Please select price option"
            End If

        Catch ex As Exception
            '_lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPriceOptn()
        Dim Ds As New DataSet
        Try
            GrdPriceOption.Visible = False
            Ds = objGetdata.GetPriceOptionSup(Session("ShidRfpID"))
            If Ds.Tables(0).Rows.Count > 0 Then
                lblNoPO.Visible = False
                GrdPriceOption.Visible = True
                GrdPriceOption.DataSource = Ds
                GrdPriceOption.DataBind()
                BindPriceOptn()
            Else
                GrdPriceOption.Visible = False
                lblNoPO.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetPriceOptn:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BindPriceOptn()
        Dim lblPriceID As New Label
        Dim lnkPriceOption As LinkButton
        Dim DsPriceID As New DataSet()
        Dim ValAvl As Boolean
        Try
            DsPriceID = objGetdata.GetPriceIDFromTypeDet(Session("ShidRfpID"))
            Dim PriceOp() As String = DsPriceID.Tables(0).Rows(0).Item("PRICEOPTIONID").ToString().Split(",")
            For Each Gr As GridViewRow In GrdPriceOption.Rows
                lblPriceID = GrdPriceOption.Rows(Gr.RowIndex).FindControl("lblPriceID")
                lnkPriceOption = GrdPriceOption.Rows(Gr.RowIndex).FindControl("lnkPriceOption")
                ValAvl = False
                For i = 0 To PriceOp.Length - 1
                    If PriceOp(i) = lblPriceID.Text Then
                        ValAvl = True
                    End If
                Next
                If ValAvl Then
                    Gr.Visible = True
                    lnkPriceOption.Attributes.Add("onclick", "return window.open('SupplierPrice_Analysis.aspx?PriceID=" + lblPriceID.Text + "'); return false;")
                Else
                    Gr.Visible = False
                End If
                'lnkPriceOption.Attributes.Add("onclick", "return ShowPriceOPWindow('CreatePrice.aspx?PriceID=" + lblPriceID.Text + "');")
            Next
        Catch ex As Exception
            lblError.Text = "Error:BindPriceOptn:" + ex.Message.ToString()
        End Try
    End Sub

    'Protected Sub BindPriceOptn()
    '    Dim lblPriceID As New Label
    '    Dim lnkPriceOption As LinkButton
    '    Try

    '        For Each Gr As GridViewRow In GrdPriceOption.Rows
    '            lblPriceID = GrdPriceOption.Rows(Gr.RowIndex).FindControl("lblPriceID")
    '            lnkPriceOption = GrdPriceOption.Rows(Gr.RowIndex).FindControl("lnkPriceOption")
    '            lnkPriceOption.Attributes.Add("onclick", "return window.open('SupplierPrice.aspx?PriceID=" + lblPriceID.Text + "'); return false;")
    '            'lnkPriceOption.Attributes.Add("onclick", "return ShowPriceOPWindow('CreatePrice.aspx?PriceID=" + lblPriceID.Text + "');")
    '        Next
    '    Catch ex As Exception
    '        lblError.Text = "Error:BindPriceOptn:" + ex.Message.ToString()
    '    End Try
    'End Sub

    Protected Sub BindPRICETableSKUlevel()
        Dim ds As New DataSet
        Dim dsM As New DataSet
        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        Dim lbl As New Label
        Dim hid As New HiddenField

        Dim hidPP As New HiddenField
        Dim strcode As New Label
        Dim hidPRC As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim lblprice As Label
        Dim tdInner As TableCell
        Dim objGetData As New SavvyProGetData()
        Dim dsP As New DataSet

        Dim dscnt As New DataSet
        Dim dsPRC As New DataSet
        Dim IsSku As Boolean = False
        Dim HidColorGrp As New HiddenField
        Dim lblColor As New Label
        Dim DsGetColorByCode As New DataSet()
        Dim DvGetColorByCode As New DataView()
        Dim DtGetColorByCode As New DataTable()
        Dim dssku As New DataSet()
        Try

            ds = objGetData.GetPriceQry(hidPriceId.Value)
            dssku = objGetData.GetRFPSKU(hidPriceId.Value)
            dsM = objGetData.GetRFPSKUCols(hidPriceId.Value)
            If ds.Tables(0).Rows.Count > 0 Then
                lblnosku.Visible = False
                For i = 0 To dsM.Tables(0).Rows.Count - 1
                    tdHeader = New TableCell
                    Dim Title As String = String.Empty
                    'Header
                    If i = 0 Then
                        HeaderTdSetting(tdHeader, "250px", "SKU", "1")
                        trHeader.Controls.Add(tdHeader)
                    Else
                        HeaderTdSetting(tdHeader, "110px", "", "1")
                        lbl = New Label
                        hid = New HiddenField
                        If dsM.Tables(0).Rows(i).Item("DESCRIPTION").ToString() <> "Colors" Then
                            lbl.Text = dsM.Tables(0).Rows(i).Item("DESCRIPTION")
                            tdHeader.Controls.Add(lbl)
                            trHeader.Controls.Add(tdHeader)

                        End If

                    End If
                Next
                tblPriceOpt.Controls.Add(trHeader)

                'Inner

                For i = 1 To dssku.Tables(0).Rows.Count + 1
                    trInner = New TableRow
                    Dim strVal As String = ""
                    Dim strc As String = ""
                    Dim CodeCnt As Integer = 1
                    Dim dv As New DataView
                    Dim dt As New DataTable
                    dv = ds.Tables(0).DefaultView()
                    dv.RowFilter = "SKUID='" + dssku.Tables(0).Rows(i - 1).Item("SKUID").ToString().Replace(" ", "") + "' "  ' and pricereqid='" + ds.Tables(0).Rows(k).Item("pricereqid").ToString() + "'"
                    dt = dv.ToTable()
                    For j = 1 To dt.Rows.Count
                        Dim dv1 As New DataView
                        Dim dt1 As New DataTable
                        dv1 = ds.Tables(0).DefaultView()
                        dv1.RowFilter = "SKUID='" + dssku.Tables(0).Rows(i - 1).Item("SKUID").ToString().Replace(" ", "") + "'and pricereqid='" + dt.Rows(j - 1).Item("pricereqid").ToString() + "'"
                        dt1 = dv1.ToTable()
                        If dt1.Rows.Count > 0 Then


                        End If
                        If j = 1 Then
                            tdInner = New TableCell
                            InnerTdSetting(tdInner, "250px", "left")
                            tdInner.Text = dt1.Rows(0).Item("SKUID").ToString() + " : " + dt1.Rows(0).Item("DETAILS").ToString() + ""
                            trInner.Controls.Add(tdInner)
                        End If
                        tdInner = New TableCell
                        InnerTdSetting(tdInner, "", "left")
                        tdInner.Text = FormatNumber(dt1.Rows(0).Item("PRICE").ToString(), 2) 'dt1.Rows(0).Item("price").ToString()
                        trInner.Controls.Add(tdInner)

                        'If j = dt.Rows.Count Then

                        '    tdInner = New TableCell
                        '    InnerTdSetting(tdInner, "", "Center")
                        '    tdInner.Text = dt.Rows(j - 1).Item("val").ToString()
                        '    trInner.Controls.Add(tdInner)
                        'End If
                    Next
                    If (i Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    tblPriceOpt.Controls.Add(trInner)
                Next
            Else
                lblnosku.Visible = True
                lblnosku.Text = "No Sku level Data Found"
            End If


        Catch ex As Exception
            '_lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnpoptsku_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpoptsku.Click
        Try
            If lbkpriceopt.Text <> "Nothing Selected" Then
                BindPRICETableSKUlevel()
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Please select Price Option first.');", True)

            End If

        Catch ex As Exception
            lblError.Text = "Error: btnRefreshVList_Click() " + ex.Message()
        End Try
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            If hidPriceId.Value <> "" Then
                lbkpriceopt.Text = hidPriceDes.Value
                BindPRICETable()
            End If
        Catch ex As Exception
            lblError.Text = "Error: btnRefresh_Click() " + ex.Message()
        End Try
    End Sub

#End Region

#Region "Structure"

    Protected Sub ChkExistingMGrpRfp()
        Dim DsCheckRfp As New DataSet()
        Dim DsCheckGrp As New DataSet()

        Try
            DsCheckRfp = objGetdata.GetLatestRSbyLicenseID(Session("LicenseNo"), Session("UserName"))
            If DsCheckRfp.Tables(0).Rows.Count > 0 Then
                ' GetRfpDetails_new(DsCheckRfp.Tables(0).Rows(0).Item("ID").ToString(), DsCheckRfp.Tables(0).Rows(0).Item("VENDOREMAILID").ToString())
                DsCheckGrp = objGetdata.GetMGroupDetails_Supplier(Session("ShidRfpID"), "")
                If DsCheckGrp.Tables(0).Rows.Count > 0 Then
                    lnkSelMasterGrp.Visible = True
                    lblmaster.Visible = True
                    lblNoStructData.Visible = True
                Else
                    lnkSelMasterGrp.Visible = False
                    lblmaster.Visible = False
                    lblNoStructData.Visible = False
                End If
            Else
                RfpDetail.Visible = False
                tabRfpSManager.Enabled = False
            End If
            If isSkuOnly Then
                lblNoStructData.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error: ChkExistingMGrpRfp() " + ex.Message()
        End Try
    End Sub

    Protected Sub GetStructure()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim Txt As New TextBox
        Dim txtthick As New TextBox
        Dim txtPrice As New TextBox
        Dim lblthick As New Label
        Dim lblprice As New Label
        Dim hid As New HiddenField
        Dim hidMat As New HiddenField
        Dim hidDes As New HiddenField
        Dim Link As New HyperLink
        Dim dsPref As New DataSet()
        Dim lbl As New Label
        Dim lblmat As New Label
        Dim hidwd As New HiddenField
        Dim hidDens As New HiddenField
        Dim HidGrp As New HiddenField
        Dim Density As New Double
        Dim TotalDen As Double = 0.0
        Dim TotalThik As Double = 0.0
        Dim TotalWgt As Double = 0.0
        Dim TotalPrice As Double = 0.0
        Dim DsGrpData As New DataSet()
        Dim DvGrpData As New DataView()
        Dim DtGrpData As New DataTable()
        Dim DsChkMGrp As New DataSet()
        Try
            tblSpecDes.Rows.Clear()
            tblStruct.Rows.Clear()

            DsChkMGrp = objGetdata.CheckMGrpForStruct(Session("ShidRfpID"))
            If DsChkMGrp.Tables(0).Rows.Count > 0 Then
                If hidMasterGrpID.Value = "" Then
                    lblNoStructData.Visible = True
                    lblNoStructData.Text = "Please Select Master Group"
                Else
                    If hidGrpCount.Value <> "" And hidGrpCount.Value <> "0" Then
                        Dim GrpCount As Integer = hidGrpCount.Value - 1
                        lblNoStructData.Visible = False
                        ' DsGrpData = objGetdata.GetExistingStructure(Session("ShidRfpID"), Session("SVendorID"))
                        DsGrpData = objGetdata.GetExistingStructure(Session("ShidRfpID"), Session("SVendorID"), hidMasterGrpID.Value)

                        DvGrpData = DsGrpData.Tables(0).DefaultView()
                        'For Structure
                        trHeader = New TableRow
                        For i = 1 To 6
                            tdHeader = New TableCell
                            tdHeader1 = New TableCell
                            Dim Title As String = String.Empty
                            'Header                        
                            Select Case i
                                Case 1
                                    HeaderTdSetting(tdHeader, "150px", "Group", "1")
                                    HeaderTdSetting(tdHeader1, "0", "", "1")
                                    trHeader.Controls.Add(tdHeader)
                                    trHeader1.Controls.Add(tdHeader1)
                                Case 2
                                    HeaderTdSetting(tdHeader, "150px", "Material", "1")
                                    HeaderTdSetting(tdHeader1, "0", "", "1")
                                    trHeader.Controls.Add(tdHeader)
                                    trHeader1.Controls.Add(tdHeader1)
                                Case 3
                                    HeaderTdSetting(tdHeader, "90px", "Thickness", "1")
                                    HeaderTdSetting(tdHeader1, "0", "(micron)", "1")
                                    trHeader.Controls.Add(tdHeader)
                                    trHeader1.Controls.Add(tdHeader1)
                                Case 4
                                    HeaderTdSetting(tdHeader, "90px", "Weight", "1")
                                    HeaderTdSetting(tdHeader1, "0", "(%)", "1")
                                    trHeader.Controls.Add(tdHeader)
                                    trHeader1.Controls.Add(tdHeader1)
                                Case 5
                                    HeaderTdSetting(tdHeader, "90px", "Density", "1")
                                    HeaderTdSetting(tdHeader1, "0", "", "1")
                                    trHeader.Controls.Add(tdHeader)
                                    trHeader1.Controls.Add(tdHeader1)
                                Case 6
                                    HeaderTdSetting(tdHeader, "90px", "Price", "1")
                                    HeaderTdSetting(tdHeader1, "0", "(Euro/kg)", "1")
                                    trHeader.Controls.Add(tdHeader)
                                    trHeader1.Controls.Add(tdHeader1)
                            End Select
                        Next
                        tblStruct.Controls.Add(trHeader)
                        tblStruct.Controls.Add(trHeader1)

                        For l = 0 To GrpCount
                            'Row Filter
                            DvGrpData.RowFilter = "GROUPID=" + DsGroup.Tables(0).Rows(l).Item("MGROUPDETID").ToString() + ""
                            DtGrpData = DvGrpData.ToTable()
                            For i = 0 To 11
                                trInner = New TableRow
                                For j = 0 To 5
                                    tdInner = New TableCell
                                    If j = 0 Then
                                        If i = 0 Then
                                            InnerTdSetting(tdInner, "90px", "Left")
                                            tdInner.RowSpan = 12
                                            HidGrp = New HiddenField
                                            lbl = New Label
                                            lbl.CssClass = "NormalLabel"
                                            lbl.Text = DsGroup.Tables(0).Rows(l).Item("DESCRIPTION").ToString()
                                            HidGrp.ID = "hidGrp_" + l.ToString()
                                            HidGrp.Value = DsGroup.Tables(0).Rows(l).Item("MGROUPDETID").ToString()
                                            tdInner.Controls.Add(lbl)
                                            tdInner.Controls.Add(HidGrp)
                                            trInner.Controls.Add(tdInner)
                                        End If
                                    ElseIf j = 1 Then
                                        InnerTdSetting(tdInner, "", "Left")
                                        lblmat = New Label
                                        hid = New HiddenField
                                        hidDes = New HiddenField
                                        hidMat = New HiddenField
                                        lblmat.ID = "hypMatDes" + l.ToString() + "_" + i.ToString()
                                        hid.ID = "hidMatid" + l.ToString() + "_" + i.ToString()
                                        hidDes.ID = "hidMatDes" + l.ToString() + "_" + i.ToString()
                                        hidMat.ID = "hidMatCat" + l.ToString() + "_" + i.ToString()

                                        'Link.Width = 120
                                        'Link.CssClass = "SavvyLink"

                                        Try
                                            If StructMatId(l, i) <> "" Then
                                                GetMaterialDetailsNew(lblmat, hid, StructMatId(l, i), hidDes, hidMat, "lblDensity_" + l.ToString() + "_" + i.ToString(), _
                                                                      Density, "hidDensity_" + l.ToString() + "_" + i.ToString())
                                            ElseIf DtGrpData.Rows(0).Item("S1").ToString() <> "" Or DtGrpData.Rows(0).Item("S1").ToString() <> "0" Then
                                                GetMaterialDetailsNew(lblmat, hid, DtGrpData.Rows(0).Item("S" + (i + 1).ToString()).ToString(), hidDes, hidMat, _
                                                                      "lblDensity_" + l.ToString() + "_" + i.ToString(), Density, "hidDensity_" + l.ToString() + "_" + i.ToString())
                                            Else
                                                GetMaterialDetailsNew(lblmat, hid, "0", hidDes, hidMat, "lblDensity_" + l.ToString() + "_" + i.ToString(), Density, _
                                                                      "hidDensity_" + l.ToString() + "_" + i.ToString())
                                            End If
                                        Catch ex As Exception
                                            GetMaterialDetailsNew(lblmat, hid, "0", hidDes, hidMat, "lblDensity_" + l.ToString() + "_" + i.ToString(), Density, _
                                                                  "hidDensity_" + l.ToString() + "_" + i.ToString())
                                        End Try

                                        tdInner.Controls.Add(hid)
                                        tdInner.Controls.Add(hidDes)
                                        tdInner.Controls.Add(hidMat)
                                        tdInner.Controls.Add(lblmat)
                                        trInner.Controls.Add(tdInner)
                                        'End
                                    ElseIf j = 2 Then
                                        InnerTdSetting(tdInner, "90px", "Left")
                                        lblthick = New Label
                                        ' txtthick.CssClass = "SmallTextBox"
                                        'txtthick.AutoPostBack = True
                                        Try
                                            If StructThick(l, i) <> "" Then
                                                lblthick.Text = StructThick(l, i)
                                            Else
                                                lblthick.Text = DtGrpData.Rows(0).Item("TH" + (i + 1).ToString()).ToString()
                                            End If
                                        Catch ex As Exception
                                            lblthick.Text = "0"
                                        End Try
                                        TotalThik = TotalThik + lblthick.Text.ToString()
                                        lblthick.ID = "txtthick_" + l.ToString() + "_" + i.ToString()
                                        'AddHandler txtthick.TextChanged, AddressOf TextBox_Thickness
                                        tdInner.Controls.Add(lblthick)
                                        trInner.Controls.Add(tdInner)
                                    ElseIf j = 3 Then
                                        InnerTdSetting(tdInner, "90px", "Left")
                                        lbl = New Label
                                        hidwd = New HiddenField
                                        lbl.CssClass = "NormalLabel"
                                        lbl.ID = "lblWidth_" + l.ToString() + "_" + i.ToString()
                                        hidwd.ID = "hidwd_" + l.ToString() + "_" + i.ToString()
                                        Try
                                            If weightPercn(l, i) <> "" Then
                                                lbl.Text = weightPercn(l, i)
                                                hidwd.Value = weightPercn(l, i)
                                            Else
                                                lbl.Text = DtGrpData.Rows(0).Item("W" + (i + 1).ToString()).ToString()
                                                hidwd.Value = DtGrpData.Rows(0).Item("W" + (i + 1).ToString()).ToString()
                                            End If
                                        Catch ex As Exception
                                            lbl.Text = "0"
                                            hidwd.Value = "0"
                                        End Try
                                        TotalWgt = TotalWgt + lbl.Text.ToString()
                                        tdInner.Controls.Add(lbl)
                                        tdInner.Controls.Add(hidwd)
                                        trInner.Controls.Add(tdInner)
                                    ElseIf j = 4 Then
                                        InnerTdSetting(tdInner, "90px", "Left")
                                        lbl = New Label
                                        hidDens = New HiddenField
                                        lbl.CssClass = "NormalLabel"
                                        lbl.ID = "lblDensity_" + l.ToString() + "_" + i.ToString()
                                        hidDens.ID = "hidDensity_" + l.ToString() + "_" + i.ToString()
                                        If Density <> 0.0 Then
                                            lbl.Text = Density.ToString()
                                            TotalDen = TotalDen + Density
                                        Else
                                            'lbl.Text = 0
                                        End If
                                        hidDens.Value = Density
                                        tdInner.Controls.Add(lbl)
                                        tdInner.Controls.Add(hidDens)
                                        trInner.Controls.Add(tdInner)
                                    ElseIf j = 5 Then
                                        InnerTdSetting(tdInner, "90px", "Left")
                                        lblprice = New Label
                                        'txtPrice.CssClass = "SmallTextBox"

                                        Try
                                            If StructPrice(l, i) <> "" Then
                                                lblprice.Text = StructPrice(l, i)
                                            Else
                                                lblprice.Text = DtGrpData.Rows(0).Item("P" + (i + 1).ToString()).ToString()
                                            End If
                                        Catch ex As Exception
                                            lblprice.Text = "0"
                                        End Try
                                        TotalPrice = TotalPrice + lblprice.Text.ToString()
                                        lblprice.ID = "txtPriceMat_" + l.ToString() + "_" + i.ToString()
                                        tdInner.Controls.Add(lblprice)
                                        trInner.Controls.Add(tdInner)
                                    End If

                                Next

                                If (i Mod 2 = 0) Then
                                    trInner.CssClass = "AlterNateColor1"
                                Else
                                    trInner.CssClass = "AlterNateColor2"
                                End If
                                tblStruct.Controls.Add(trInner)
                            Next

                            'For Total
                            TtlDensityInner(l) = TotalDen
                            TtlThickness(l) = TotalThik
                            TtlWeight(l) = TotalWgt
                            TtlPrice(l) = TotalPrice
                            trInner = New TableRow
                            For q = 0 To 5
                                tdInner = New TableCell
                                tdInner.CssClass = "AlterNateColor4"
                                Select Case q
                                    Case 0
                                        InnerTdSetting(tdInner, "150px", "Left")
                                        tdInner.Text = "Total"
                                        trInner.Controls.Add(tdInner)
                                    Case 1
                                        InnerTdSetting(tdInner, "150px", "Left")
                                        tdInner.Text = ""
                                        trInner.Controls.Add(tdInner)
                                    Case 2
                                        InnerTdSetting(tdInner, "90px", "Left")
                                        Try
                                            If TtlThickness(l) <> "" Then
                                                tdInner.Text = TtlThickness(l)
                                            Else
                                                tdInner.Text = FormatNumber(DtGrpData.Rows(0).Item("TOTALTHICKNESS").ToString(), 2)
                                            End If
                                        Catch ex As Exception
                                            tdInner.Text = "0.0"
                                        End Try
                                        trInner.Controls.Add(tdInner)
                                    Case 3
                                        InnerTdSetting(tdInner, "90px", "Left")
                                        Try
                                            If TtlWeight(l) <> "" Then
                                                tdInner.Text = TtlWeight(l)
                                            Else
                                                tdInner.Text = FormatNumber(DtGrpData.Rows(0).Item("TOTALWEIGHT").ToString(), 2)
                                            End If
                                        Catch ex As Exception
                                            tdInner.Text = "0.0"
                                        End Try
                                        trInner.Controls.Add(tdInner)
                                    Case 4
                                        InnerTdSetting(tdInner, "90px", "Left")
                                        Try
                                            If TtlDensity(l) <> "" Then
                                                tdInner.Text = TtlDensity(l)
                                            Else
                                                tdInner.Text = FormatNumber(TtlDensityInner(l), 2)
                                            End If
                                        Catch ex As Exception
                                            tdInner.Text = "0.0"
                                        End Try
                                        trInner.Controls.Add(tdInner)
                                    Case 5
                                        InnerTdSetting(tdInner, "90px", "Left")
                                        Try
                                            If TtlPrice(l) <> "" Then
                                                tdInner.Text = TtlPrice(l)
                                            Else
                                                tdInner.Text = FormatNumber(DtGrpData.Rows(0).Item("TOTALPRICE").ToString(), 2)
                                            End If
                                        Catch ex As Exception
                                            tdInner.Text = "0.0"
                                        End Try
                                        trInner.Controls.Add(tdInner)
                                End Select
                            Next
                            tblStruct.Controls.Add(trInner)
                            TotalDen = 0.0
                            TotalThik = 0.0
                            TotalWgt = 0.0
                            TotalPrice = 0.0
                            'End Total


                            trInner = New TableRow
                            tdInner = New TableCell
                            tdInner.ColumnSpan = 6
                            ' tdInner.Height = 30
                            trInner.Controls.Add(tdInner)
                            trInner.CssClass = "AlterNateColor3"
                            tblStruct.Controls.Add(trInner)
                        Next
                    End If
                End If
            Else
                lblNoStructData.Visible = False

                If hidGrpCount.Value <> "" And hidGrpCount.Value <> "0" Then
                    Dim GrpCount As Integer = hidGrpCount.Value - 1
                    lblNoStructData.Visible = False
                    DsGrpData = objGetdata.GetExistingSKuStructure(Session("ShidRfpID"), Session("SVendorID"))
                    DvGrpData = DsGrpData.Tables(0).DefaultView()
                    'For Structure
                    trHeader = New TableRow
                    For i = 1 To 6
                        tdHeader = New TableCell
                        tdHeader1 = New TableCell
                        Dim Title As String = String.Empty
                        'Header                        
                        Select Case i
                            Case 1
                                HeaderTdSetting(tdHeader, "150px", "SKU", "1")
                                HeaderTdSetting(tdHeader1, "0", "", "1")
                                trHeader.Controls.Add(tdHeader)
                                trHeader1.Controls.Add(tdHeader1)
                            Case 2
                                HeaderTdSetting(tdHeader, "150px", "Material", "1")
                                HeaderTdSetting(tdHeader1, "0", "", "1")
                                trHeader.Controls.Add(tdHeader)
                                trHeader1.Controls.Add(tdHeader1)
                            Case 3
                                HeaderTdSetting(tdHeader, "90px", "Thickness", "1")
                                HeaderTdSetting(tdHeader1, "0", "(micron)", "1")
                                trHeader.Controls.Add(tdHeader)
                                trHeader1.Controls.Add(tdHeader1)
                            Case 4
                                HeaderTdSetting(tdHeader, "90px", "Weight", "1")
                                HeaderTdSetting(tdHeader1, "0", "(%)", "1")
                                trHeader.Controls.Add(tdHeader)
                                trHeader1.Controls.Add(tdHeader1)
                            Case 5
                                HeaderTdSetting(tdHeader, "90px", "Density", "1")
                                HeaderTdSetting(tdHeader1, "0", "", "1")
                                trHeader.Controls.Add(tdHeader)
                                trHeader1.Controls.Add(tdHeader1)
                            Case 6
                                HeaderTdSetting(tdHeader, "90px", "Price", "1")
                                HeaderTdSetting(tdHeader1, "0", "(Euro/kg)", "1")
                                trHeader.Controls.Add(tdHeader)
                                trHeader1.Controls.Add(tdHeader1)
                        End Select
                    Next
                    tblStruct.Controls.Add(trHeader)
                    tblStruct.Controls.Add(trHeader1)

                    For l = 0 To GrpCount
                        'Row Filter
                        DvGrpData.RowFilter = "SKUID=" + DsSkuFStrct.Tables(0).Rows(l).Item("SKUID").ToString() + ""
                        DtGrpData = DvGrpData.ToTable()
                        For i = 0 To 11
                            trInner = New TableRow
                            For j = 0 To 5
                                tdInner = New TableCell
                                If j = 0 Then
                                    If i = 0 Then
                                        InnerTdSetting(tdInner, "90px", "Left")
                                        tdInner.RowSpan = 12
                                        HidGrp = New HiddenField
                                        lbl = New Label
                                        lbl.CssClass = "NormalLabel"
                                        lbl.Text = DsSkuFStrct.Tables(0).Rows(l).Item("DESCRIPTION").ToString()
                                        HidGrp.ID = "hidGrp_" + l.ToString()
                                        HidGrp.Value = DsSkuFStrct.Tables(0).Rows(l).Item("SKUID").ToString()
                                        tdInner.Controls.Add(lbl)
                                        tdInner.Controls.Add(HidGrp)
                                        trInner.Controls.Add(tdInner)
                                    End If
                                ElseIf j = 1 Then
                                    InnerTdSetting(tdInner, "", "Left")
                                    lblmat = New Label
                                    hid = New HiddenField
                                    hidDes = New HiddenField
                                    hidMat = New HiddenField
                                    lblmat.ID = "hypMatDes" + l.ToString() + "_" + i.ToString()
                                    hid.ID = "hidMatid" + l.ToString() + "_" + i.ToString()
                                    hidDes.ID = "hidMatDes" + l.ToString() + "_" + i.ToString()
                                    hidMat.ID = "hidMatCat" + l.ToString() + "_" + i.ToString()

                                    'Link.Width = 120
                                    'Link.CssClass = "SavvyLink"

                                    Try
                                        If StructMatId(l, i) <> "" Then
                                            GetMaterialDetailsNew(lblmat, hid, StructMatId(l, i), hidDes, hidMat, "lblDensity_" + l.ToString() + "_" + i.ToString(), _
                                                                  Density, "hidDensity_" + l.ToString() + "_" + i.ToString())
                                        ElseIf DtGrpData.Rows(0).Item("S1").ToString() <> "" Or DtGrpData.Rows(0).Item("S1").ToString() <> "0" Then
                                            GetMaterialDetailsNew(lblmat, hid, DtGrpData.Rows(0).Item("S" + (i + 1).ToString()).ToString(), hidDes, hidMat, _
                                                                  "lblDensity_" + l.ToString() + "_" + i.ToString(), Density, "hidDensity_" + l.ToString() + "_" + i.ToString())
                                        Else
                                            GetMaterialDetailsNew(lblmat, hid, "0", hidDes, hidMat, "lblDensity_" + l.ToString() + "_" + i.ToString(), Density, _
                                                                  "hidDensity_" + l.ToString() + "_" + i.ToString())
                                        End If
                                    Catch ex As Exception
                                        GetMaterialDetailsNew(lblmat, hid, "0", hidDes, hidMat, "lblDensity_" + l.ToString() + "_" + i.ToString(), Density, _
                                                              "hidDensity_" + l.ToString() + "_" + i.ToString())
                                    End Try

                                    tdInner.Controls.Add(hid)
                                    tdInner.Controls.Add(hidDes)
                                    tdInner.Controls.Add(hidMat)
                                    tdInner.Controls.Add(lblmat)
                                    trInner.Controls.Add(tdInner)
                                    'End
                                ElseIf j = 2 Then
                                    InnerTdSetting(tdInner, "90px", "Left")
                                    lblthick = New Label
                                    'txtthick.CssClass = "SmallTextBox"
                                    'txtthick.AutoPostBack = True
                                    Try
                                        If StructThick(l, i) <> "" Then
                                            lblthick.Text = StructThick(l, i)
                                        Else
                                            lblthick.Text = DtGrpData.Rows(0).Item("TH" + (i + 1).ToString()).ToString()
                                        End If
                                    Catch ex As Exception
                                        lblthick.Text = "0"
                                    End Try
                                    TotalThik = TotalThik + lblthick.Text.ToString()
                                    lblthick.ID = "txtthick_" + l.ToString() + "_" + i.ToString()
                                    'AddHandler txtthick.TextChanged, AddressOf TextBox_Thickness
                                    tdInner.Controls.Add(lblthick)
                                    trInner.Controls.Add(tdInner)
                                ElseIf j = 3 Then
                                    InnerTdSetting(tdInner, "90px", "Left")
                                    lbl = New Label
                                    hidwd = New HiddenField
                                    lbl.CssClass = "NormalLabel"
                                    lbl.ID = "lblWidth_" + l.ToString() + "_" + i.ToString()
                                    hidwd.ID = "hidwd_" + l.ToString() + "_" + i.ToString()
                                    Try
                                        If weightPercn(l, i) <> "" Then
                                            lbl.Text = weightPercn(l, i)
                                            hidwd.Value = weightPercn(l, i)
                                        Else
                                            lbl.Text = DtGrpData.Rows(0).Item("W" + (i + 1).ToString()).ToString()
                                            hidwd.Value = DtGrpData.Rows(0).Item("W" + (i + 1).ToString()).ToString()
                                        End If
                                    Catch ex As Exception
                                        lbl.Text = "0"
                                        hidwd.Value = "0"
                                    End Try
                                    TotalWgt = TotalWgt + lbl.Text.ToString()
                                    tdInner.Controls.Add(lbl)
                                    tdInner.Controls.Add(hidwd)
                                    trInner.Controls.Add(tdInner)
                                ElseIf j = 4 Then
                                    InnerTdSetting(tdInner, "90px", "Left")
                                    lbl = New Label
                                    hidDens = New HiddenField
                                    lbl.CssClass = "NormalLabel"
                                    lbl.ID = "lblDensity_" + l.ToString() + "_" + i.ToString()
                                    hidDens.ID = "hidDensity_" + l.ToString() + "_" + i.ToString()
                                    If Density <> 0.0 Then
                                        lbl.Text = Density.ToString()
                                        TotalDen = TotalDen + Density
                                    Else
                                        'lbl.Text = 0
                                    End If
                                    hidDens.Value = Density
                                    tdInner.Controls.Add(lbl)
                                    tdInner.Controls.Add(hidDens)
                                    trInner.Controls.Add(tdInner)
                                ElseIf j = 5 Then
                                    InnerTdSetting(tdInner, "90px", "Left")
                                    lblprice = New Label
                                    'txtPrice.CssClass = "SmallTextBox"

                                    Try
                                        If StructPrice(l, i) <> "" Then
                                            lblprice.Text = StructPrice(l, i)
                                        Else
                                            lblprice.Text = DtGrpData.Rows(0).Item("P" + (i + 1).ToString()).ToString()
                                        End If
                                    Catch ex As Exception
                                        lblprice.Text = "0"
                                    End Try
                                    TotalPrice = TotalPrice + lblprice.Text.ToString()
                                    lblprice.ID = "txtPriceMat_" + l.ToString() + "_" + i.ToString()
                                    tdInner.Controls.Add(lblprice)
                                    trInner.Controls.Add(tdInner)
                                End If

                            Next

                            If (i Mod 2 = 0) Then
                                trInner.CssClass = "AlterNateColor1"
                            Else
                                trInner.CssClass = "AlterNateColor2"
                            End If
                            tblStruct.Controls.Add(trInner)
                        Next

                        'For Total
                        TtlDensityInner(l) = TotalDen
                        TtlThickness(l) = TotalThik
                        TtlWeight(l) = TotalWgt
                        TtlPrice(l) = TotalPrice
                        trInner = New TableRow
                        For q = 0 To 5
                            tdInner = New TableCell
                            tdInner.CssClass = "AlterNateColor4"
                            Select Case q
                                Case 0
                                    InnerTdSetting(tdInner, "150px", "Left")
                                    tdInner.Text = "Total"
                                    trInner.Controls.Add(tdInner)
                                Case 1
                                    InnerTdSetting(tdInner, "150px", "Left")
                                    tdInner.Text = ""
                                    trInner.Controls.Add(tdInner)
                                Case 2
                                    InnerTdSetting(tdInner, "90px", "Left")
                                    Try
                                        If TtlThickness(l) <> "" Then
                                            tdInner.Text = TtlThickness(l)
                                        Else
                                            tdInner.Text = FormatNumber(DtGrpData.Rows(0).Item("TOTALTHICKNESS").ToString(), 2)
                                        End If
                                    Catch ex As Exception
                                        tdInner.Text = "0.0"
                                    End Try
                                    trInner.Controls.Add(tdInner)
                                Case 3
                                    InnerTdSetting(tdInner, "90px", "Left")
                                    Try
                                        If TtlWeight(l) <> "" Then
                                            tdInner.Text = TtlWeight(l)
                                        Else
                                            tdInner.Text = FormatNumber(DtGrpData.Rows(0).Item("TOTALWEIGHT").ToString(), 2)
                                        End If
                                    Catch ex As Exception
                                        tdInner.Text = "0.0"
                                    End Try
                                    trInner.Controls.Add(tdInner)
                                Case 4
                                    InnerTdSetting(tdInner, "90px", "Left")
                                    Try
                                        If TtlDensity(l) <> "" Then
                                            tdInner.Text = TtlDensity(l)
                                        Else
                                            tdInner.Text = FormatNumber(TtlDensityInner(l), 2)
                                        End If
                                    Catch ex As Exception
                                        tdInner.Text = "0.0"
                                    End Try
                                    trInner.Controls.Add(tdInner)
                                Case 5
                                    InnerTdSetting(tdInner, "90px", "Left")
                                    Try
                                        If TtlPrice(l) <> "" Then
                                            tdInner.Text = TtlPrice(l)
                                        Else
                                            tdInner.Text = FormatNumber(DtGrpData.Rows(0).Item("TOTALPRICE").ToString(), 2)
                                        End If
                                    Catch ex As Exception
                                        tdInner.Text = "0.0"
                                    End Try
                                    trInner.Controls.Add(tdInner)
                            End Select
                        Next
                        tblStruct.Controls.Add(trInner)
                        TotalDen = 0.0
                        TotalThik = 0.0
                        TotalWgt = 0.0
                        TotalPrice = 0.0
                        'End Total


                        trInner = New TableRow
                        tdInner = New TableCell
                        tdInner.ColumnSpan = 6
                        ' tdInner.Height = 30
                        trInner.Controls.Add(tdInner)
                        trInner.CssClass = "AlterNateColor3"
                        tblStruct.Controls.Add(trInner)
                    Next
                End If
            End If


        Catch ex As Exception
            lblError.Text = "Error: GetStructure() " + ex.Message()
        End Try
    End Sub

    Protected Sub GetMaterialDetailsNew(ByRef LinkMat As Label, ByVal hid As HiddenField, ByVal MatId As Integer, ByVal hidDes As HiddenField, ByVal hidMat As HiddenField, _
                                        ByVal labelGrade As String, ByRef Dens As Double, ByVal hidDensity As String)
        Dim Ds As New DataSet
        Dim DsMat As New DataSet
        Dim DvMat As New DataView
        Dim DtMat As New DataTable
        Dim ObjGetdata As New StandGetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim linkText As String = ""
        Dim DsMatDensity As New DataSet()
        Dim objGetDataSPP As New SavvyProGetData
        Try
            If MatId <> "0" Then
                Ds = ObjGetdata.GetCategory(MatId, "", "")

                LinkMat.Text = Ds.Tables(0).Rows(0).Item("CATEGORY").ToString()

                'Showing Blend name on link
                If MatId > 500 And MatId < 506 Then
                    linkText = Ds.Tables(0).Rows(0).Item("MATID").ToString() + ":" + Ds.Tables(0).Rows(0).Item("MATERIAL").ToString()
                Else
                    linkText = Ds.Tables(0).Rows(0).Item("MATID").ToString() + ":" + Ds.Tables(0).Rows(0).Item("CATEGORY").ToString()
                End If
                LinkMat.ToolTip = Ds.Tables(0).Rows(0).Item("CATEGORY").ToString()
                LinkMat.Attributes.Add("text-decoration", "none")

                hidDes.Value = Ds.Tables(0).Rows(0).Item("CATEGORY").ToString()
                hid.Value = MatId.ToString()
                hidMat.Value = Ds.Tables(0).Rows(0).Item("CATEGORY").ToString()

                If (LinkMat.Text).ToUpper() = "RESIN" Then
                    DsMat = ObjGetdata.GetResinMaterialbyGroups(MatId, "", "Resin")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "FILM" Then
                    DsMat = ObjGetdata.GetFilmMaterialbyGroups(MatId, "", "Film")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "ADHESIVE" Then
                    DsMat = ObjGetdata.GetAdhesiveMaterialNew(MatId, "", "Adhesive")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "ALUMINUM" Then
                    DsMat = ObjGetdata.GetAluminMaterialNew(MatId, "", "Aluminum")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "COATING" Then
                    DsMat = ObjGetdata.GetCoatingMaterialbyGroups(MatId, "", "Coating")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "BASE FIBER" Then
                    DsMat = ObjGetdata.GetBaseFMaterialbyGroups(MatId, "", "Base Fiber")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "CONCENTRATE" Then
                    DsMat = ObjGetdata.GetConcentrateMaterialbyGroups(MatId, "", "Concentrate")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "GLASS" Then
                    DsMat = ObjGetdata.GetGlassMaterialbyGroups(MatId, "", "Glass")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "INK" Then
                    DsMat = ObjGetdata.GetInkMaterialbyGroups(MatId, "", "Ink")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "NON-WOVEN" Then
                    DsMat = ObjGetdata.GetNonWMaterialbyGroups(MatId, "", "Non-Woven")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "PAPER" Then
                    DsMat = ObjGetdata.GetPaperMaterialbyGroups(MatId, "", "Paper")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "PAPERBOARD" Then
                    DsMat = ObjGetdata.GetPaperBMaterialbyGroups(MatId, "", "Paperboard")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "SHEET" Then
                    DsMat = ObjGetdata.GetSheetMaterialbyGroups(MatId, "", "Sheet")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "STEEL" Then
                    DsMat = ObjGetdata.GetSteelMaterialbyGroups(MatId, "", "Steel")
                End If

                DsMatDensity = objGetDataSPP.GetMatSGByMatID(MatId.ToString())
                If DsMatDensity.Tables(0).Rows.Count > 0 Then
                    Dens = DsMatDensity.Tables(0).Rows(0).Item("SG").ToString()
                Else
                    Dens = "0"
                End If
            Else
                LinkMat.Text = "Nothing"
                LinkMat.ToolTip = "Nothing"
                LinkMat.Attributes.Add("text-decoration", "none")
                hidDes.Value = "Nothing"
                hid.Value = 0
                hidMat.Value = "Nothing"
                'labelGrade = "0"
                linkText = MatId.ToString() + ":" + "Nothing"
                Dens = "0"
            End If
            'Path = "PopUp/GetMatPopUp.aspx?Des=tabRfpSManager_tabStruct_" + LinkMat.ClientID + "&Id=tabRfpSManager_tabStruct_" + hid.ClientID + "&GradeDes=&GradeId=&SG=&MatDes=tabRfpSManager_tabStruct_" + hidDes.ClientID + "&MatGrp=" + LinkMat.Text + "&Grp=" + LinkMat.Text + "&LinkId=N&Density=tabRfpSManager_tabStruct_" + labelGrade + "&HidDen=tabRfpSManager_tabStruct_" + hidDensity + ""
            ' LinkMat.NavigateUrl = "javascript:ShowPopWindow_Struct('" + Path + "')"
            LinkMat.Text = linkText
        Catch ex As Exception
            lblError.Text = "Error:GetMaterialDetailsNew:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetGrpInfo()
        Try
            DsGroup = objGetdata.GetGroupByMasterGrpS(hidMasterGrpID.Value())
            If DsGroup.Tables(0).Rows.Count > 0 Then
                hidGrpCount.Value = DsGroup.Tables(0).Rows.Count
                ReDim CalWeight(hidGrpCount.Value, 11)
                ReDim StructMatId(hidGrpCount.Value, 11)
                ReDim StructThick(hidGrpCount.Value, 11)
                ReDim StructPrice(hidGrpCount.Value, 11)
                ReDim weightPercn(hidGrpCount.Value, 11)
                ReDim TtlThickness(hidGrpCount.Value)
                ReDim TtlDensity(hidGrpCount.Value)
                ReDim TtlWeight(hidGrpCount.Value)
                ReDim TtlDensityInner(hidGrpCount.Value)
                ReDim TtlPrice(hidGrpCount.Value)
                'GetStructure()
            End If
        Catch ex As Exception
            lblError.Text = "Error: GetGrpInfo() " + ex.Message()
        End Try
    End Sub

    Protected Sub GetSkuForStructInfo()
        Try
            DsSkuFStrct = objGetdata.GetSKUByRfpID(Session("ShidRfpID"), Session("BUserID"))
            If DsSkuFStrct.Tables(0).Rows.Count > 0 Then
                hidGrpCount.Value = DsSkuFStrct.Tables(0).Rows.Count
                ReDim CalWeight(hidGrpCount.Value, 11)
                ReDim StructMatId(hidGrpCount.Value, 11)
                ReDim StructThick(hidGrpCount.Value, 11)
                ReDim StructPrice(hidGrpCount.Value, 11)
                ReDim weightPercn(hidGrpCount.Value, 11)
                ReDim TtlThickness(hidGrpCount.Value)
                ReDim TtlDensity(hidGrpCount.Value)
                ReDim TtlWeight(hidGrpCount.Value)
                ReDim TtlDensityInner(hidGrpCount.Value)
                ReDim TtlPrice(hidGrpCount.Value)
            End If
        Catch ex As Exception
            lblError.Text = "Error: GetSkuForStructInfo() " + ex.Message()
        End Try
    End Sub

    Protected Sub GetStructureSKU()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim Txt As New TextBox
        Dim txtthick As New TextBox
        Dim txtPrice As New TextBox
        Dim lblthick As New Label
        Dim lblprice As New Label
        Dim hid As New HiddenField
        Dim hidMat As New HiddenField
        Dim hidDes As New HiddenField
        Dim Link As New HyperLink
        Dim lblmat As Label
        Dim dsPref As New DataSet()
        Dim lbl As New Label
        Dim hidwd As New HiddenField
        Dim hidDens As New HiddenField
        Dim HidGrp As New HiddenField
        Dim Density As New Double
        Dim TotalDen As Double = 0.0
        Dim TotalThik As Double = 0.0
        Dim TotalWgt As Double = 0.0
        Dim TotalPrice As Double = 0.0
        Dim DsGrpData As New DataSet()
        Dim DvGrpData As New DataView()
        Dim DtGrpData As New DataTable()
        Try
            'lnkSelMasterGrp.Text = "Nothing Selected"
            'hidMasterGrpID.Value = ""
            tblSpecDes.Rows.Clear()
            tblStruct.Rows.Clear()
            If hidMasterGrpID.Value = "" Then
                lblNoStructData.Visible = True
                lblNoStructData.Text = "Please Select Master Group"
            Else

                'Dim GrpCount As Integer = hidGrpCount.Value
                lblNoStructData.Visible = False
                DsGrpData = objGetdata.GetExistingSKUStructurePT(Session("ShidRfpID"), Session("SVendorID"), hidMasterGrpID.Value, Session("BUserID"))
                DvGrpData = DsGrpData.Tables(0).DefaultView()
                If DsGrpData.Tables(0).Rows.Count <= 0 Then
                    lblNoStructData.Text = "SKU level data not found "
                    lblNoStructData.Visible = True
                Else

                    'For Structure
                    trHeader = New TableRow
                    For i = 0 To 6
                        tdHeader = New TableCell
                        tdHeader1 = New TableCell
                        Dim Title As String = String.Empty
                        'Header                        
                        Select Case i
                            Case 0
                                HeaderTdSetting(tdHeader, "150px", "SKU", "1")
                                HeaderTdSetting(tdHeader1, "0", "", "1")
                                trHeader.Controls.Add(tdHeader)
                                trHeader1.Controls.Add(tdHeader1)
                            Case 1

                            Case 2
                                HeaderTdSetting(tdHeader, "150px", "Material", "1")
                                HeaderTdSetting(tdHeader1, "0", "", "1")
                                trHeader.Controls.Add(tdHeader)
                                trHeader1.Controls.Add(tdHeader1)
                            Case 3
                                HeaderTdSetting(tdHeader, "90px", "Thickness", "1")
                                HeaderTdSetting(tdHeader1, "0", "(micron)", "1")
                                trHeader.Controls.Add(tdHeader)
                                trHeader1.Controls.Add(tdHeader1)
                            Case 4
                                HeaderTdSetting(tdHeader, "90px", "Weight", "1")
                                HeaderTdSetting(tdHeader1, "0", "(%)", "1")
                                trHeader.Controls.Add(tdHeader)
                                trHeader1.Controls.Add(tdHeader1)
                            Case 5
                                HeaderTdSetting(tdHeader, "90px", "Density", "1")
                                HeaderTdSetting(tdHeader1, "0", "", "1")
                                trHeader.Controls.Add(tdHeader)
                                trHeader1.Controls.Add(tdHeader1)
                            Case 6
                                HeaderTdSetting(tdHeader, "90px", "Price", "1")
                                HeaderTdSetting(tdHeader1, "0", "(Euro/kg)", "1")
                                trHeader.Controls.Add(tdHeader)
                                trHeader1.Controls.Add(tdHeader1)
                        End Select
                    Next
                    tblStruct.Controls.Add(trHeader)
                    tblStruct.Controls.Add(trHeader1)

                    For l = 0 To DsGrpData.Tables(0).Rows.Count - 1 'GrpCount
                        'Row Filter
                        DvGrpData.RowFilter = "SKUID=" + DsGrpData.Tables(0).Rows(l).Item("SKUID").ToString() + ""
                        DtGrpData = DvGrpData.ToTable()
                        For i = 0 To 11
                            trInner = New TableRow
                            For j = 0 To 6
                                tdInner = New TableCell
                                If j = 0 Then
                                    If i = 0 Then
                                        InnerTdSetting(tdInner, "90px", "Left")
                                        tdInner.RowSpan = 12
                                        HidGrp = New HiddenField
                                        lbl = New Label
                                        lbl.CssClass = "NormalLabel"
                                        lbl.Text = DsGrpData.Tables(0).Rows(l).Item("SKUID").ToString() + ":" + DsGrpData.Tables(0).Rows(l).Item("DETAILS").ToString() 'DsGroup.Tables(0).Rows(l).Item("DESCRIPTION").ToString()
                                        'HidGrp.ID = "hidGrpsku_" + l.ToString()
                                        ' HidGrp.Value = DsGroup.Tables(0).Rows(l).Item("MGROUPDETID").ToString()
                                        tdInner.Controls.Add(lbl)
                                        'tdInner.Controls.Add(HidGrp)
                                        trInner.Controls.Add(tdInner)
                                    End If
                                ElseIf j = 1 Then


                                ElseIf j = 2 Then
                                    InnerTdSetting(tdInner, "", "Left")
                                    lblmat = New Label
                                    hid = New HiddenField
                                    hidDes = New HiddenField
                                    hidMat = New HiddenField
                                    lblmat.ID = "hypMatDes" + l.ToString() + "_" + i.ToString()
                                    hid.ID = "hidMatid" + l.ToString() + "_" + i.ToString()
                                    hidDes.ID = "hidMatDes" + l.ToString() + "_" + i.ToString()
                                    hidMat.ID = "hidMatCat" + l.ToString() + "_" + i.ToString()

                                    lblmat.Width = 120
                                    lblmat.CssClass = "SavvyLink"

                                    Try
                                        If StructMatId(l, i) <> "" Then
                                            GetMaterialDetailsNew(lblmat, hid, StructMatId(l, i), hidDes, hidMat, "lblDensity_" + l.ToString() + "_" + i.ToString(), _
                                                                  Density, "hidDensity_" + l.ToString() + "_" + i.ToString())
                                        ElseIf DtGrpData.Rows(0).Item("S1").ToString() <> "" Or DtGrpData.Rows(0).Item("S1").ToString() <> "0" Then
                                            GetMaterialDetailsNew(lblmat, hid, DtGrpData.Rows(0).Item("S" + (i + 1).ToString()).ToString(), hidDes, hidMat, _
                                                                  "lblDensity_" + l.ToString() + "_" + i.ToString(), Density, "hidDensity_" + l.ToString() + "_" + i.ToString())
                                        Else
                                            GetMaterialDetailsNew(lblmat, hid, "0", hidDes, hidMat, "lblDensity_" + l.ToString() + "_" + i.ToString(), Density, _
                                                                  "hidDensity_" + l.ToString() + "_" + i.ToString())
                                        End If
                                    Catch ex As Exception
                                        GetMaterialDetailsNew(lblmat, hid, "0", hidDes, hidMat, "lblDensity_" + l.ToString() + "_" + i.ToString(), Density, _
                                                              "hidDensity_" + l.ToString() + "_" + i.ToString())
                                    End Try

                                    tdInner.Controls.Add(hid)
                                    tdInner.Controls.Add(hidDes)
                                    tdInner.Controls.Add(hidMat)
                                    tdInner.Controls.Add(lblmat)
                                    trInner.Controls.Add(tdInner)
                                    'End
                                ElseIf j = 3 Then
                                    InnerTdSetting(tdInner, "90px", "Left")
                                    lblthick = New Label
                                    'txtthick.CssClass = "SmallTextBox"
                                    'txtthick.AutoPostBack = True
                                    Try
                                        If StructThick(l, i) <> "" Then
                                            lblthick.Text = StructThick(l, i)
                                        Else
                                            lblthick.Text = DtGrpData.Rows(0).Item("TH" + (i + 1).ToString()).ToString()
                                        End If
                                    Catch ex As Exception
                                        lblthick.Text = "0"
                                    End Try
                                    TotalThik = TotalThik + lblthick.Text.ToString()
                                    lblthick.ID = "lblthick_" + l.ToString() + "_" + i.ToString()
                                    ' AddHandler txtthick.TextChanged, AddressOf TextBox_Thickness
                                    tdInner.Controls.Add(lblthick)
                                    trInner.Controls.Add(tdInner)
                                ElseIf j = 4 Then
                                    InnerTdSetting(tdInner, "90px", "Left")
                                    lbl = New Label
                                    hidwd = New HiddenField
                                    lbl.CssClass = "NormalLabel"
                                    lbl.ID = "lblWidth_" + l.ToString() + "_" + i.ToString()
                                    hidwd.ID = "hidwd_" + l.ToString() + "_" + i.ToString()
                                    Try
                                        If weightPercn(l, i) <> "" Then
                                            lbl.Text = weightPercn(l, i)
                                            hidwd.Value = weightPercn(l, i)
                                        Else
                                            lbl.Text = DtGrpData.Rows(0).Item("W" + (i + 1).ToString()).ToString()
                                            hidwd.Value = DtGrpData.Rows(0).Item("W" + (i + 1).ToString()).ToString()
                                        End If
                                    Catch ex As Exception
                                        lbl.Text = "0"
                                        hidwd.Value = "0"
                                    End Try
                                    TotalWgt = TotalWgt + lbl.Text.ToString()

                                    tdInner.Controls.Add(lbl)
                                    tdInner.Controls.Add(hidwd)
                                    trInner.Controls.Add(tdInner)
                                ElseIf j = 5 Then
                                    InnerTdSetting(tdInner, "90px", "Left")
                                    lbl = New Label
                                    hidDens = New HiddenField
                                    lbl.CssClass = "NormalLabel"
                                    lbl.ID = "lblDensity_" + l.ToString() + "_" + i.ToString()
                                    hidDens.ID = "hidDensity_" + l.ToString() + "_" + i.ToString()
                                    If Density <> 0.0 Then
                                        lbl.Text = Density.ToString()
                                        TotalDen = TotalDen + Density
                                    Else
                                        'lbl.Text = 0
                                    End If
                                    hidDens.Value = Density
                                    tdInner.Controls.Add(lbl)
                                    tdInner.Controls.Add(hidDens)
                                    trInner.Controls.Add(tdInner)
                                ElseIf j = 6 Then
                                    InnerTdSetting(tdInner, "90px", "Left")
                                    lblprice = New Label
                                    'lblprice .CssClass = "SmallTextBox"

                                    Try
                                        If StructPrice(l, i) <> "" Then
                                            lblprice.Text = StructPrice(l, i)
                                        Else
                                            lblprice.Text = DtGrpData.Rows(0).Item("P" + (i + 1).ToString()).ToString()
                                        End If
                                    Catch ex As Exception
                                        lblprice.Text = "0"
                                    End Try
                                    TotalPrice = TotalPrice + lblprice.Text.ToString()
                                    lblprice.ID = "lblPriceMat_" + l.ToString() + "_" + i.ToString()
                                    tdInner.Controls.Add(lblprice)
                                    trInner.Controls.Add(tdInner)
                                End If

                            Next

                            If (i Mod 2 = 0) Then
                                trInner.CssClass = "AlterNateColor1"
                            Else
                                trInner.CssClass = "AlterNateColor2"
                            End If
                            tblStruct.Controls.Add(trInner)
                        Next



                        'For Total
                        TtlDensityInner(l) = TotalDen
                        TtlThickness(l) = TotalThik
                        TtlWeight(l) = TotalWgt
                        TtlPrice(l) = TotalPrice
                        trInner = New TableRow
                        For q = 0 To 5
                            tdInner = New TableCell
                            tdInner.CssClass = "AlterNateColor4"
                            Select Case q
                                Case 0
                                    InnerTdSetting(tdInner, "150px", "Left")
                                    tdInner.Text = "Total"
                                    trInner.Controls.Add(tdInner)
                                Case 1
                                    InnerTdSetting(tdInner, "150px", "Left")
                                    tdInner.Text = ""
                                    trInner.Controls.Add(tdInner)
                                Case 2
                                    InnerTdSetting(tdInner, "90px", "Left")
                                    Try
                                        If TtlThickness(l) <> "" Then
                                            tdInner.Text = TtlThickness(l)
                                        Else
                                            tdInner.Text = FormatNumber(DtGrpData.Rows(0).Item("TOTALTHICKNESS").ToString(), 2)
                                        End If
                                    Catch ex As Exception
                                        tdInner.Text = "0.0"
                                    End Try
                                    trInner.Controls.Add(tdInner)
                                Case 3
                                    InnerTdSetting(tdInner, "90px", "Left")
                                    Try
                                        If TtlWeight(l) <> "" Then
                                            tdInner.Text = TtlWeight(l)
                                        Else
                                            tdInner.Text = FormatNumber(DtGrpData.Rows(0).Item("TOTALWEIGHT").ToString(), 2)
                                        End If
                                    Catch ex As Exception
                                        tdInner.Text = "0.0"
                                    End Try
                                    trInner.Controls.Add(tdInner)
                                Case 4
                                    InnerTdSetting(tdInner, "90px", "Left")
                                    Try
                                        If TtlDensity(l) <> "" Then
                                            tdInner.Text = TtlDensity(l)
                                        Else
                                            tdInner.Text = FormatNumber(TtlDensityInner(l), 2)
                                        End If
                                    Catch ex As Exception
                                        tdInner.Text = "0.0"
                                    End Try
                                    trInner.Controls.Add(tdInner)
                                Case 5
                                    InnerTdSetting(tdInner, "90px", "Left")
                                    Try
                                        If TtlPrice(l) <> "" Then
                                            tdInner.Text = TtlPrice(l)
                                        Else
                                            tdInner.Text = FormatNumber(DtGrpData.Rows(0).Item("TOTALPRICE").ToString(), 2)
                                        End If
                                    Catch ex As Exception
                                        tdInner.Text = "0.0"
                                    End Try
                                    trInner.Controls.Add(tdInner)
                            End Select
                        Next
                        tblStruct.Controls.Add(trInner)
                        TotalDen = 0.0
                        TotalThik = 0.0
                        TotalWgt = 0.0
                        TotalPrice = 0.0
                        'End Total

                        trInner = New TableRow
                        tdInner = New TableCell
                        tdInner.ColumnSpan = 6
                        ' tdInner.Height = 30
                        trInner.Controls.Add(tdInner)
                        trInner.CssClass = "AlterNateColor3"
                        tblStruct.Controls.Add(trInner)
                    Next
                End If
            End If

        Catch ex As Exception
            lblError.Text = "Error: GetStructureSKU() " + ex.Message()
        End Try
    End Sub

    Protected Sub btnSkulvlData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSkulvlData.Click
        Try
            If lnkSelMasterGrp.Text <> "Nothing Selected" Then
                GetSkuForStructInfo()
                GetStructureSKU()
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Please select Master Group first.');", True)

            End If

        Catch ex As Exception
            lblError.Text = "Error: btnSkulvlData_Click() " + ex.Message()
        End Try
    End Sub

#End Region

    Protected Sub btnRefreshVList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefreshVList.Click
        Try
            GetUsersList()
        Catch ex As Exception
            lblError.Text = "Error: btnRefreshVList_Click() " + ex.Message()
        End Try
    End Sub

    Protected Sub btnMasterSel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMasterSel.Click
        Try
            If hidMasterGrpID.Value <> "" Then
                lnkSelMasterGrp.Text = hidMasterGrpDes.Value
            End If
        Catch ex As Exception
            lblError.Text = "Error: btnMasterSel_Click() " + ex.Message()
        End Try
    End Sub

End Class
