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
Partial Class Pages_SavvyPackPro_ConfigureRFP_Supplier
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
    Private isPageLoaded As Boolean = True
    Dim isSkuOnly As Boolean = True

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim DsChkMGrp As New DataSet()
        Try
            pnlBMConfigVendor.Visible = False

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
                InsertDefaultTerms()
                GetPageDetails()
                GetPrice()
                GetPriceCost()
                GetPriceOptn()
                GetRFQ()
                GetBagMakingDetails()

                'For SKu Lvl
                DsChkMGrp = objGetdata.CheckMGrpForStruct(Session("ShidRfpID"))
                If DsChkMGrp.Tables(0).Rows.Count > 0 Then
                    isSkuOnly = False
                    trSelM.Visible = True
                    btnExtrapSkuLvl.Visible = True
                Else
                    isSkuOnly = True
                    trSelM.Visible = False
                    btnExtrapSkuLvl.Visible = False
                End If
                If isSkuOnly Then
                    GetSkuForStructInfo()
                End If
                'End

                GetStructure()
            Else
                GetPageDetails()
                GetBagMakingDetails()
                If hidRfpID.Value <> "" Then
                    Session("ShidRfpID") = hidRfpID.Value
                End If
                'For SKu Lvl
                DsChkMGrp = objGetdata.CheckMGrpForStruct(Session("ShidRfpID"))
                If DsChkMGrp.Tables(0).Rows.Count > 0 Then
                    isSkuOnly = False
                    trSelM.Visible = True
                    btnExtrapSkuLvl.Visible = True
                Else
                    isSkuOnly = True
                    trSelM.Visible = False
                    btnExtrapSkuLvl.Visible = False
                End If
                'End
                If isSkuOnly Then
                    GetSkuForStructInfo()
                Else
                    If hidMasterGrpID.Value <> "" Then
                        GetGrpInfo()
                    End If
                End If
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
                ElseIf tabRfpSManager.ActiveTabIndex = "1" Then
                    GetPageDetails()
                ElseIf tabRfpSManager.ActiveTabIndex = "2" Then
                    GetPrice()
                ElseIf tabRfpSManager.ActiveTabIndex = "3" Then
                    GetPriceCost()
                ElseIf tabRfpSManager.ActiveTabIndex = "4" Then
                    GetPriceOptn()
                ElseIf tabRfpSManager.ActiveTabIndex = "5" Then
                    GetRFQ()
                ElseIf tabRfpSManager.ActiveTabIndex = "6" Then
                    GetBagMakingDetails()
                ElseIf tabRfpSManager.ActiveTabIndex = "7" Then
                    GetStructure()
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error: tabRfpSManager_ActiveTabChanged() " + ex.Message()
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
                        tabBagmaking.Visible = True
                    End If
                    'End

                    tabBagmaking.Visible = True
                    'loadTab()
                    If Session("UserName") = SelROwnerEmailID Then
                        tabRfpSManager.Enabled = True
                    Else
                        tabRfpSManager.Enabled = False
                    End If
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
            InsertDefaultTerms()
            GetPageDetails()
            GetPrice()
            GetPriceCost()
            GetPriceOptn()
            GetRFQ()
            GetBagMakingDetails()
        Catch ex As Exception
            lblError.Text = "Error: GetRfpDetails() " + ex.Message()
        End Try
    End Sub

    Protected Sub btnHidRFPCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHidRFPCreate.Click
        Try
            If hidRfpID.Value <> "" Or hidRfpID.Value <> "0" Then
                tabRfpSManager.Enabled = True
                lnkSelRFP.Text = hidRfpNm.Value
                GetRfpDetails_new(hidRfpID.Value, hidRSOwnerEmailID.Value)
                loadTab()
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('System isn't able to find RFPID. Please try again.');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error: btnHidRFPCreate_Click() " + ex.Message()
        End Try
    End Sub

#End Region

#Region "Terms"

    Protected Sub InsertDefaultTerms()
        Dim Dts As New DataSet()
        Dim i As New Integer
        Dim ds As New DataSet
        Dim DsT As New DataSet
        Dim DsAddTerm As New DataSet()
        Dim DsCheckTerm As New DataSet()
        Dim TermSeq As String = String.Empty
        Try
            If Session("ShidRfpID") = Nothing Or Session("SVendorID") = Nothing Or Session("BUserID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else
                'Dts = objGetdata.GetDefaultTerms()
                'ds = objGetdata.GetTerms(Session("ShidRfpID"), Session("UserId"))
                'ds = objGetdata.GetSTerms(Session("ShidRfpID"), Session("BUserID"))
                ds = objGetdata.GetSTerms_new(Session("ShidRfpID"), Session("BUserID"))
                DsT = objGetdata.GetTermsDetails_Supplier(Session("ShidRfpID"), Session("SVendorID"))

                For i = 1 To ds.Tables(0).Rows.Count
                    If ds.Tables(0).Rows(i - 1).Item("ISCHECKED").ToString() = "Y" Then
                        If DsT.Tables(0).Rows.Count = 0 Then
                            objUpIns.InsertAccepted_Terms(Session("ShidRfpID").ToString(), ds.Tables(0).Rows(i - 1).Item("TERMID").ToString(), Session("SVendorID"))
                        End If
                    End If
                Next

                'For Additional Term
                DsAddTerm = objGetdata.GetAddtnTerms(Session("ShidRfpID").ToString())
                DsCheckTerm = objGetdata.CheckAddtnTermByVendorID(Session("ShidRfpID").ToString(), Session("SVendorID"))

                For j = 0 To DsAddTerm.Tables(0).Rows.Count - 1
                    If DsCheckTerm.Tables(0).Rows.Count = 0 Then
                        objUpIns.InsertAddtnTerm(DsAddTerm.Tables(0).Rows(j).Item("ADDTERMID").ToString(), Session("SVendorID"))
                    End If
                Next
            End If
        Catch ex As Exception
            Response.Write("InsertAnalysisInformation" + ex.Message)
        End Try
    End Sub

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
        Dim HidPriceoptn As New HiddenField
        Dim ddlPrintTech As New DropDownList

        Try
            If Session("ShidRfpID") = Nothing Or Session("SVendorID") = Nothing Or Session("BUserID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else
                tblEditQ.Rows.Clear()
                tblcustomize.Rows.Clear()
                dsQues = objGetdata.GetSTerms(Session("ShidRfpID"), Session("BUserID"), Session("SVendorID"))

                Session("STermsData") = dsQues

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
                                        chck.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString()
                                        If dsQues.Tables(0).Rows(i).Item("ACCEPTED_TERMS") = "Y" Then
                                            chck.Checked = True
                                        Else
                                            chck.Checked = False
                                        End If
                                        chck.AutoPostBack = True
                                        AddHandler chck.CheckedChanged, AddressOf CheckBox_Check
                                        tdId.Width = 55
                                        tdId.Controls.Add(hid)
                                        tdId.Controls.Add(chck)

                                    Case 2
                                        lblItem = New Label
                                        'InnerTdSetting(lbl, "", "Center")
                                        lblItem.Text = dsQues.Tables(0).Rows(i).Item("TITLE").ToString()
                                        lblItem.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "." + (i + 1).ToString()
                                        lblItem.Style.Add("font-family", "Verdana")
                                        lblItem.Style.Add("width", "300px")
                                        lblItem.Style.Add("height", "14px")
                                        tdId.Controls.Add(lblItem)

                                    Case 3
                                        lblTerm = New Label
                                        'InnerTdSetting(lbl, "", "Center")
                                        lblTerm.Text = dsQues.Tables(0).Rows(i).Item("DESCRIPTION").ToString()
                                        lblTerm.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "_T" + (i + 1).ToString()
                                        lblTerm.Style.Add("font-family", "Verdana")
                                        lblTerm.Style.Add("width", "350px")
                                        lblTerm.Style.Add("height", "14px")
                                        tdId.Controls.Add(lblTerm)

                                    Case 4
                                        txtCom = New TextBox
                                        HidSeqQue = New HiddenField
                                        InnerTdSetting(tdId, "", "Center")
                                        txtCom.Text = dsQues.Tables(0).Rows(i).Item("COMMENTS").ToString()
                                        txtCom.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "_" + (i + 1).ToString()
                                        HidSeqQue.Value = txtCom.Text
                                        txtCom.Font.Size = 10
                                        txtCom.MaxLength = 600
                                        txtCom.TextMode = TextBoxMode.MultiLine
                                        txtCom.Style.Add("background-color", "#FEFCA1")
                                        txtCom.Style.Add("font-family", "Verdana")
                                        txtCom.Style.Add("width", "300px")
                                        txtCom.Style.Add("height", "50px")
                                        txtCom.AutoPostBack = True
                                        If dsQues.Tables(0).Rows(i).Item("TITLE").ToString() = "Add/Remove Color" Then
                                            txtCom.Attributes.Add("onchange", "javascript:return CheckNum(this,'" + HidSeqQue.Value + "');")
                                        Else
                                            txtCom.Attributes.Add("onchange", "javascript:return CheckSP(this,'" + HidSeqQue.Value + "');")
                                        End If
                                        AddHandler txtCom.TextChanged, AddressOf TextBox_TextChangedI
                                        tdId.Controls.Add(txtCom)
                                        tdId.Controls.Add(HidSeqQue)
                                    Case 5
                                        ddlPriceOptn = New DropDownList
                                        InnerTdSetting(tdId, "", "Center")
                                        GetPriceOptn(ddlPriceOptn, dsQues.Tables(0).Rows(i).Item("PRICEREQID").ToString())
                                        ddlPriceOptn.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "_D" + (i + 1).ToString()
                                        ddlPriceOptn.AutoPostBack = True
                                        If dsQues.Tables(0).Rows(i).Item("TITLE").ToString() = "Add/Remove Color" Then
                                            ddlPriceOptn.Enabled = True
                                        Else
                                            ddlPriceOptn.Enabled = False
                                        End If
                                        AddHandler ddlPriceOptn.SelectedIndexChanged, AddressOf DdlPriceoptn_ChangeE
                                        tdId.Controls.Add(ddlPriceOptn)

                                    Case 6
                                        ddlPrintTech = New DropDownList
                                        InnerTdSetting(tdId, "", "Center")
                                        GetPrintTech(ddlPrintTech, dsQues.Tables(0).Rows(i).Item("PRINTINGTECHID").ToString())
                                        ddlPrintTech.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "_PT" + (i + 1).ToString()
                                        ddlPrintTech.AutoPostBack = True
                                        If dsQues.Tables(0).Rows(i).Item("TITLE").ToString() = "Add/Remove Color" Then
                                            ddlPrintTech.Enabled = True
                                        Else
                                            ddlPrintTech.Enabled = False
                                        End If
                                        AddHandler ddlPrintTech.SelectedIndexChanged, AddressOf DdlPrintTech_ChangeE
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
                                        chck.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString()
                                        If dsQues.Tables(0).Rows(i).Item("ACCEPTED_TERMS") = "Y" Then
                                            chck.Checked = True
                                        Else
                                            chck.Checked = False
                                        End If
                                        chck.AutoPostBack = True
                                        AddHandler chck.CheckedChanged, AddressOf CheckBox_Check
                                        tdId.Width = 55
                                        tdId.Controls.Add(hid)
                                        tdId.Controls.Add(chck)

                                    Case 2
                                        lblItem = New Label
                                        'InnerTdSetting(lbl, "", "Center")
                                        lblItem.Text = dsQues.Tables(0).Rows(i).Item("TITLE").ToString()
                                        lblItem.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "." + (i + 1).ToString()
                                        lblItem.Style.Add("font-family", "Verdana")
                                        lblItem.Style.Add("width", "300px")
                                        lblItem.Style.Add("height", "14px")
                                        tdId.Controls.Add(lblItem)

                                    Case 3
                                        lblTerm = New Label
                                        'InnerTdSetting(lbl, "", "Center")
                                        lblTerm.Text = dsQues.Tables(0).Rows(i).Item("DESCRIPTION").ToString()
                                        lblTerm.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "_T" + (i + 1).ToString()
                                        lblTerm.Style.Add("font-family", "Verdana")
                                        lblTerm.Style.Add("width", "350px")
                                        lblTerm.Style.Add("height", "14px")
                                        tdId.Controls.Add(lblTerm)

                                    Case 4
                                        txtCom = New TextBox
                                        HidSeqQue = New HiddenField
                                        InnerTdSetting(tdId, "", "Center")
                                        txtCom.Text = dsQues.Tables(0).Rows(i).Item("COMMENTS").ToString()
                                        txtCom.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "_" + (i + 1).ToString()
                                        HidSeqQue.Value = txtCom.Text
                                        txtCom.Font.Size = 10
                                        txtCom.MaxLength = 600
                                        txtCom.TextMode = TextBoxMode.MultiLine
                                        txtCom.Style.Add("background-color", "#FEFCA1")
                                        txtCom.Style.Add("font-family", "Verdana")
                                        txtCom.Style.Add("width", "300px")
                                        txtCom.Style.Add("height", "50px")
                                        txtCom.AutoPostBack = True
                                        If dsQues.Tables(0).Rows(i).Item("TITLE").ToString() = "Add/Remove Color" Then
                                            txtCom.Attributes.Add("onchange", "javascript:return CheckNum(this,'" + HidSeqQue.Value + "');")
                                        Else
                                            txtCom.Attributes.Add("onchange", "javascript:return CheckSP(this,'" + HidSeqQue.Value + "');")
                                        End If
                                        AddHandler txtCom.TextChanged, AddressOf TextBox_TextChangedI
                                        tdId.Controls.Add(txtCom)
                                        tdId.Controls.Add(HidSeqQue)
                                    Case 5
                                        ddlPriceOptn = New DropDownList
                                        InnerTdSetting(tdId, "", "Center")
                                        GetPriceOptn(ddlPriceOptn, dsQues.Tables(0).Rows(i).Item("PRICEREQID").ToString())
                                        ddlPriceOptn.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "_D" + (i + 1).ToString()
                                        ddlPriceOptn.AutoPostBack = True
                                        If dsQues.Tables(0).Rows(i).Item("TITLE").ToString() = "Add/Remove Color" Then
                                            ddlPriceOptn.Enabled = True
                                        Else
                                            ddlPriceOptn.Enabled = False
                                        End If
                                        AddHandler ddlPriceOptn.SelectedIndexChanged, AddressOf DdlPriceoptn_ChangeE
                                        tdId.Controls.Add(ddlPriceOptn)

                                    Case 6
                                        ddlPrintTech = New DropDownList
                                        InnerTdSetting(tdId, "", "Center")
                                        GetPrintTech(ddlPrintTech, dsQues.Tables(0).Rows(i).Item("PRINTINGTECHID").ToString())
                                        ddlPrintTech.ID = dsQues.Tables(0).Rows(i).Item("TERMID").ToString() + "_PT" + (i + 1).ToString()
                                        ddlPrintTech.AutoPostBack = True
                                        If dsQues.Tables(0).Rows(i).Item("TITLE").ToString() = "Add/Remove Color" Then
                                            ddlPrintTech.Enabled = True
                                        Else
                                            ddlPrintTech.Enabled = False
                                        End If
                                        AddHandler ddlPrintTech.SelectedIndexChanged, AddressOf DdlPrintTech_ChangeE
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
                            txtDes = New TextBox
                            InnerTdSetting(tdInnerAT, "", "Center")
                            txtDes.Text = DsTermsByVendor.Tables(0).Rows(j).Item("ADDTERMDES").ToString()
                            txtDes.ID = DsTermsByVendor.Tables(0).Rows(j).Item("ADDTERMID").ToString() + "_ATD_" + j.ToString()
                            txtDes.Font.Size = 10
                            txtDes.MaxLength = 99
                            txtDes.Style.Add("background-color", "#FEFCA1")
                            txtDes.Style.Add("font-family", "Verdana")
                            txtDes.AutoPostBack = True
                            If DsTermsByVendor.Tables(0).Rows(j).Item("TITLE").ToString() = "Add/Remove Color" Then
                                txtDes.Attributes.Add("onchange", "javascript:return CheckNum(this,'" + txtDes.Text + "');")
                            Else
                                txtDes.Attributes.Add("onchange", "javascript:return CheckSP(this,'" + txtDes.Text + "');")
                            End If
                            AddHandler txtDes.TextChanged, AddressOf txtAT_TextChangedAT
                            tdInnerAT.Controls.Add(txtDes)
                            trInnerAT.Controls.Add(tdInnerAT)
                        ElseIf k = 3 Then
                            txtComment = New TextBox
                            InnerTdSetting(tdInnerAT, "", "Center")
                            txtComment.Text = DsTermsByVendor.Tables(0).Rows(j).Item("COMMENTS").ToString()
                            txtComment.ID = DsTermsByVendor.Tables(0).Rows(j).Item("ADDTERMID").ToString() + "_ATC_" + j.ToString()
                            txtComment.Font.Size = 10
                            txtComment.MaxLength = 499
                            txtComment.TextMode = TextBoxMode.MultiLine
                            txtComment.Style.Add("width", "300px")
                            txtComment.Style.Add("height", "50px")
                            txtComment.Style.Add("background-color", "#FEFCA1")
                            txtComment.Style.Add("font-family", "Verdana")
                            txtComment.AutoPostBack = True
                            txtComment.Attributes.Add("onchange", "javascript:return CheckSP(this,'" + txtComment.Text + "');")
                            AddHandler txtComment.TextChanged, AddressOf txtAT_TextChangedAT
                            tdInnerAT.Controls.Add(txtComment)
                            trInnerAT.Controls.Add(tdInnerAT)
                        ElseIf k = 4 Then
                            ddlPriceOptnAT = New DropDownList
                            InnerTdSetting(tdInnerAT, "", "Center")
                            GetPriceOptn(ddlPriceOptnAT, DsTermsByVendor.Tables(0).Rows(j).Item("PRICEREQID").ToString())
                            ddlPriceOptnAT.ID = DsTermsByVendor.Tables(0).Rows(j).Item("ADDTERMID").ToString() + "_ATPO_" + j.ToString()
                            ddlPriceOptnAT.AutoPostBack = True
                            If DsTermsByVendor.Tables(0).Rows(j).Item("TITLE").ToString() = "Add/Remove Color" Then
                                ddlPriceOptnAT.Enabled = True
                            Else
                                ddlPriceOptnAT.Enabled = False
                            End If
                            AddHandler ddlPriceOptnAT.SelectedIndexChanged, AddressOf DdlAddTerm_ChangeE
                            tdInnerAT.Controls.Add(ddlPriceOptnAT)
                            trInnerAT.Controls.Add(tdInnerAT)
                        ElseIf k = 5 Then
                            ddlPrintTechAT = New DropDownList
                            InnerTdSetting(tdInnerAT, "", "Center")
                            GetPrintTech(ddlPrintTechAT, DsTermsByVendor.Tables(0).Rows(j).Item("PRINTINGTECHID").ToString())
                            ddlPrintTechAT.ID = DsTermsByVendor.Tables(0).Rows(j).Item("ADDTERMID").ToString() + "_ATPT_" + j.ToString()
                            ddlPrintTechAT.AutoPostBack = True
                            If DsTermsByVendor.Tables(0).Rows(j).Item("TITLE").ToString() = "Add/Remove Color" Then
                                ddlPrintTechAT.Enabled = True
                            Else
                                ddlPrintTechAT.Enabled = False
                            End If
                            AddHandler ddlPrintTechAT.SelectedIndexChanged, AddressOf DdlAddTerm_ChangeE
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

    Protected Sub CheckBox_Check(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objUpIns As New SavvyProUpInsData()
        Dim objGetData As New SavvyProGetData()
        Dim QId As String
        Dim dsS As DataSet
        Dim IsAccept As Boolean
        Try
            If Session("ShidRfpID") = Nothing Or Session("SVendorID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else
                Dim chk = DirectCast(sender, CheckBox)
                If chk.Checked = True Then
                    QId = chk.ID
                    IsAccept = True
                    objUpIns.UpdateAccepted_Terms_New(Session("ShidRfpID").ToString(), QId.ToString(), Session("SVendorID"), IsAccept)
                Else
                    QId = chk.ID
                    IsAccept = False
                    objUpIns.UpdateAccepted_Terms_New(Session("ShidRfpID").ToString(), QId.ToString(), Session("SVendorID"), IsAccept)
                End If
                GetPageDetails()
            End If
        Catch ex As Exception
            lblError.Text = "Error:CheckBox_Check:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub txtAT_TextChangedAT(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim TextAT As String
        Dim TextAT_ID As String
        Try
            If Session("ShidRfpID") = Nothing Or Session("SVendorID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else
                Dim txtItem = DirectCast(sender, TextBox)
                TextAT = txtItem.Text
                TextAT_ID = txtItem.ID
                Dim Temp As String() = TextAT_ID.Split("_")
                If Temp(1) = "ATD" Then
                    objUpIns.UpdateAddTermDes(TextAT.ToString(), Temp(0).ToString(), Session("SVendorID"), "Des")
                Else
                    objUpIns.UpdateAddTermDes(TextAT.ToString(), Temp(0).ToString(), Session("SVendorID"), "Comm")
                End If
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert1", "alert('Comment Updated Succesfully.')", True)
                GetPageDetails()
            End If
        Catch ex As Exception
            lblError.Text = "Error:txtAT_TextChangedAT:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub TextBox_TextChangedI(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim TextSeq As String
        Dim QId As String
        Dim DsQuestionSeq As New DataSet()
        Dim Avail As New Boolean
        Dim dsC As DataSet
        Try
            If Session("ShidRfpID") = Nothing Or Session("SVendorID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else
                Dim txtItem = DirectCast(sender, TextBox)
                TextSeq = txtItem.Text
                QId = txtItem.ID
                Dim Temp As String() = QId.Split("_")
                Dim temp1 As String = Temp(0)
                objUpIns.UpdateComments(TextSeq.ToString(), Session("ShidRfpID").ToString(), temp1.ToString(), Session("SVendorID"))
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert1", "alert('Comment Updated Succesfully.')", True)
                GetPageDetails()
            End If
        Catch ex As Exception
            lblError.Text = "Error:TextBox_TextChanged:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub DdlPriceoptn_ChangeE(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim TextSeq As String
        Dim QId As String
        Try
            If Session("ShidRfpID") = Nothing Or Session("SVendorID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else
                Dim ddlPriceoptn = DirectCast(sender, DropDownList)
                TextSeq = ddlPriceoptn.SelectedValue
                QId = ddlPriceoptn.ID
                Dim Temp As String() = QId.Split("_")
                objUpIns.UpdatePrintTechPriceOptn(TextSeq.ToString(), Session("ShidRfpID").ToString(), Temp(0).ToString(), Session("SVendorID"), "Price")
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert1", "alert('Comment Updated Succesfully.')", True)
                GetPageDetails()
            End If
        Catch ex As Exception
            lblError.Text = "Error:DdlPriceoptn_ChangeE:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub DdlPrintTech_ChangeE(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim TextSeq As String
        Dim QId As String
        Try
            If Session("ShidRfpID") = Nothing Or Session("SVendorID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else
                Dim ddlPrintTech = DirectCast(sender, DropDownList)
                TextSeq = ddlPrintTech.SelectedValue
                QId = ddlPrintTech.ID
                Dim Temp As String() = QId.Split("_")
                objUpIns.UpdatePrintTechPriceOptn(TextSeq.ToString(), Session("ShidRfpID").ToString(), Temp(0).ToString(), Session("SVendorID"), "Print")
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert1", "alert('Comment Updated Succesfully.')", True)
                GetPageDetails()
            End If
        Catch ex As Exception
            lblError.Text = "Error:DdlPrintTech_ChangeE:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub DdlAddTerm_ChangeE(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim TextSel As String
        Dim TextSelID As String
        Try
            If Session("ShidRfpID") = Nothing Or Session("SVendorID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else
                Dim ddlPrintTech = DirectCast(sender, DropDownList)
                TextSel = ddlPrintTech.SelectedValue
                TextSelID = ddlPrintTech.ID
                Dim Temp As String() = TextSelID.Split("_")
                If Temp(1) = "ATPO" Then
                    objUpIns.UpdateAddTermPP(TextSel.ToString(), Temp(0).ToString(), Session("SVendorID"), "Price")
                Else
                    objUpIns.UpdateAddTermPP(TextSel.ToString(), Temp(0).ToString(), Session("SVendorID"), "Print")
                End If
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert1", "alert('Comment Updated Succesfully.')", True)
                GetPageDetails()
            End If
        Catch ex As Exception
            lblError.Text = "Error:DdlAddTerm_ChangeE:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnrefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Try

        Catch ex As Exception
            lblError.Text = "Error:btnLoad_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnrefreshT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefreshT.Click
        Try
            GetPageDetails()
            GetPriceCost()
        Catch ex As Exception
            lblError.Text = "Error:btnLoad_Click:" + ex.Message.ToString()
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
                    btnUpdatePrice.Visible = True
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
                                    txtprice = New TextBox
                                    txtprice.CssClass = "SmallTextBox"
                                    txtprice.Width = 70
                                    txtprice.ID = "txtPrice" + i.ToString()
                                    DvPriceData.RowFilter = "SKUID=" + DsSKUByRfpID.Tables(0).Rows(i).Item("SKUID").ToString()
                                    DtPriceData = DvPriceData.ToTable()
                                    Try
                                        txtprice.Text = FormatNumber(DtPriceData.Rows(0).Item("PRICE").ToString(), 0)
                                    Catch ex As Exception
                                        txtprice.Text = "0"
                                    End Try
                                    txtprice.MaxLength = 12
                                    tdInner.Controls.Add(txtprice)
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
                    btnUpdatePrice.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetPrice:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnUpdatePrice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdatePrice.Click
        Try
            If Session("ShidRfpID") = Nothing Or Session("SVendorID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else
                Dim count As Integer = Convert.ToInt32(Session("SkuCOuntPrice"))
                Dim SPECID(count - 1) As String
                Dim PRICE(count - 1) As String
                For i = 0 To count - 1
                    SPECID(i) = Request.Form("tabRfpSManager$tabSMPrice$hidSpecIDPrice" + i.ToString() + "")
                    PRICE(i) = Request.Form("tabRfpSManager$tabSMPrice$txtPrice" + i.ToString() + "").Replace(",", "")
                    objUpIns.InsUpdateOnlyPrice(Session("ShidRfpID"), Session("SVendorID"), SPECID(i), PRICE(i))
                Next
                Session("SkuCOuntPrice") = Nothing
                GetPrice()
                GetPriceCost()
            End If
        Catch ex As Exception
            lblError.Text = "Error: btnUpdatePrice_Click() " + ex.Message()
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
                    BtnUpdate.Visible = True
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
                                        txtpricePC = New TextBox
                                        txtpricePC.CssClass = "SmallTextBox"
                                        txtpricePC.Width = 70
                                        txtpricePC.ID = "txtPricePC" + i.ToString() + "_" + l.ToString() + ""
                                        If l = 0 Then
                                            Try
                                                txtpricePC.Text = FormatNumber(DtPriceData.Rows(0).Item("PRICE").ToString(), 0)
                                            Catch ex As Exception
                                                txtpricePC.Text = "0"
                                            End Try
                                        ElseIf l = 1 Then
                                            Try
                                                txtpricePC.Text = FormatNumber(DtPriceData.Rows(0).Item("SETUP").ToString(), 0)
                                            Catch ex As Exception
                                                txtpricePC.Text = "0"
                                            End Try
                                        Else
                                            Try
                                                txtpricePC.Text = FormatNumber(DtPriceData.Rows(0).Item("M" + (l - 1).ToString()).ToString(), 0)
                                            Catch ex As Exception
                                                txtpricePC.Text = "0"
                                            End Try
                                        End If
                                        txtpricePC.MaxLength = 12
                                        tdInner.Controls.Add(txtpricePC)
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
                    BtnUpdate.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetPriceCost:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BtnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        Try
            If Session("ShidRfpID") = Nothing Or Session("SVendorID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else
                Dim count As Integer = Convert.ToInt32(Session("SkuCOuntPC"))
                Dim SPECID(count - 1) As String
                Dim PRICE(15) As String
                For i = 0 To count - 1
                    SPECID(i) = Request.Form("tabRfpSManager$tabSMPC$hidSpecIDPC" + i.ToString() + "")
                    For k = 0 To 15
                        PRICE(k) = Request.Form("tabRfpSManager$tabSMPC$txtPricePC" + i.ToString() + "_" + k.ToString() + "").Replace(",", "")
                    Next
                    objUpIns.InsUpdatePriceCost(Session("ShidRfpID"), Session("SVendorID"), SPECID(i), PRICE)
                Next
                Session("SkuCOuntPC") = Nothing
                GetPriceCost()
                GetPrice()
            End If
        Catch ex As Exception
            lblError.Text = "Error: btnUpdatePrice_Click() " + ex.Message()
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
                    lblNoDataPriceCost.Visible = False
                    BtnUpdate.Visible = True

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
                                    hidSpecID.Value = DsSKUByRfpID.Tables(0).Rows(i).Item("SKUID").ToString()
                                    hidSpecID.ID = "hidSpecIDO" + i.ToString()
                                    lblSpecIDPC.Text = DsSKUByRfpID.Tables(0).Rows(i).Item("SKUID").ToString()
                                    InnerTdSetting(tdInner, "50px", "")
                                    tdInner.Controls.Add(lblSpecIDPC)
                                    tdInner.Controls.Add(hidSpecID)
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
                                        txtPrintT = New TextBox
                                        txtPrintT.CssClass = "SmallTextBox"
                                        txtPrintT.Width = 180
                                        txtPrintT.ID = "txtPrintT" + i.ToString()
                                        Try
                                            txtPrintT.Text = DsSKUByRfpID.Tables(0).Rows(i).Item("PRINTTECH").ToString()
                                        Catch ex As Exception
                                            txtPrintT.Text = ""
                                        End Try
                                        txtPrintT.MaxLength = 12
                                        tdInner.Controls.Add(txtPrintT)
                                        trInner.Controls.Add(tdInner)

                                    End If


                                Case 4
                                    If DsSKUByRfpID.Tables(0).Rows(0).Item("OPOPTIONID").ToString() = "2" Or DsSKUByRfpID.Tables(0).Rows(0).Item("OPOPTIONID").ToString() = "1,2" Then
                                        tdInner = New TableCell
                                        InnerTdSetting(tdInner, "120px", "")
                                        txtBagM = New TextBox
                                        txtBagM.CssClass = "SmallTextBox"
                                        txtBagM.Width = 180
                                        txtBagM.ID = "txtBagM" + i.ToString()

                                        Try
                                            txtBagM.Text = DsSKUByRfpID.Tables(0).Rows(i).Item("BAGMAKING").ToString()
                                        Catch ex As Exception
                                            txtBagM.Text = ""
                                        End Try
                                        txtBagM.MaxLength = 12
                                        tdInner.Controls.Add(txtBagM)
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
                    BtnUpdate1.Visible = False
                End If
            End If

        Catch ex As Exception
            lblError.Text = "Error:GetRFQ:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub BtnUpdate1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnUpdate1.Click
        Try
            If Session("ShidRfpID") = Nothing Or Session("SVendorID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            Else
                Dim DsSKUByRfpID As DataSet
                DsSKUByRfpID = objGetdata.GetSpecRFQDetails(Session("ShidRfpID"), Session("BUserID"))

                Dim count As Integer = Convert.ToInt32(Session("SpecCount"))
                Dim SPECID(count - 1) As String
                Dim Print(count - 1) As String
                Dim Bag(count - 1) As String
                For i = 0 To count - 1
                    SPECID(i) = Request.Form("tabRfpSManager$tabRFQ$hidSpecIDO" + i.ToString() + "")
                    ' Print(i) = Request.Form("tabRfpSManager$tabRFQ$txtPrintT" + i.ToString() + "").Replace(",", "")
                    'Bag(i) = Request.Form("tabRfpSManager$tabRFQ$txtBagM" + i.ToString() + "").Replace(",", "")

                    'pt changes
                    If DsSKUByRfpID.Tables(0).Rows(0).Item("OPOPTIONID").ToString() = "1" Or DsSKUByRfpID.Tables(0).Rows(0).Item("OPOPTIONID").ToString() = "1,2" Then
                        Print(i) = Request.Form("tabRfpSManager$tabRFQ$txtPrintT" + i.ToString() + "").Replace(",", "")
                    Else
                        Print(i) = String.Empty
                    End If
                    If DsSKUByRfpID.Tables(0).Rows(0).Item("OPOPTIONID").ToString() = "2" Or DsSKUByRfpID.Tables(0).Rows(0).Item("OPOPTIONID").ToString() = "1,2" Then
                        Bag(i) = Request.Form("tabRfpSManager$tabRFQ$txtBagM" + i.ToString() + "").Replace(",", "")
                    Else
                        Bag(i) = String.Empty

                    End If

                    'end pt changes
                    objUpIns.InsUpdatePrintBag(Session("ShidRfpID"), Session("SVendorID"), SPECID(i), Print(i), Bag(i))
                Next
                Session("SkuCOuntPC") = Nothing
                'GetPriceCost()
                'GetPrice()
                GetRFQ()
            End If
        Catch ex As Exception
            lblError.Text = "Error: btnUpdatePrice_Click() " + ex.Message()
        End Try
    End Sub

#End Region

#Region "Bag Making"

    Public Sub GetBagMakingDetails()
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
        Dim DsColumn As DataSet

        Try
            tblBagM.Rows.Clear()

            ' dsEquip = objGetData.GetEquipmentDetails()
            DsColumn = objGetdata.GetBagMakingCol(Session("SVendorID"))
            dsData = objGetdata.GetBagMakingDet(Session("ShidRfpID"), Session("SVendorID"))
            If hidRowNumBM.Value <> "" Then
                count = hidRowNumBM.Value
            Else
                If dsData.Tables(0).Rows.Count > 0 Then
                    count = dsData.Tables(0).Rows.Count
                    hidRowNumBM.Value = dsData.Tables(0).Rows.Count
                Else
                    count = 1
                    hidRowNumBM.Value = 1
                End If
            End If



            trHeader = New TableRow
            For i = 1 To 5
                tdHeader = New TableCell
                tdHeader1 = New TableCell

                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "200px", DsColumn.Tables(0).Rows(0).Item(i - 1).ToString(), "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "180px", DsColumn.Tables(0).Rows(0).Item(i - 1).ToString(), "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "180px", DsColumn.Tables(0).Rows(0).Item(i - 1).ToString(), "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        HeaderTdSetting(tdHeader, "180px", DsColumn.Tables(0).Rows(0).Item(i - 1).ToString(), "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        HeaderTdSetting(tdHeader, "180px", DsColumn.Tables(0).Rows(0).Item(i - 1).ToString(), "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)

                End Select
            Next
            tblBagM.Controls.Add(trHeader)
            tblBagM.Controls.Add(trHeader1)

            For i = 1 To count   'dsData.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 0 To 5
                    tdInner = New TableCell
                    If j = 0 Then
                        InnerTdSetting(tdInner, "20px", "Center")
                        txt = New TextBox
                        txt.CssClass = "MatSmallTextBox"
                        txt.ID = "txtBagMakingDes" + i.ToString()

                        If BMDes(i - 1) <> "" Then
                            txt.Text = BMDes(i - 1)
                        Else

                            If dsData.Tables(0).Rows.Count >= i Then
                                If dsData.Tables(0).Rows(i - 1).Item("BAGMAKINGDES").ToString() <> "" Then
                                    txt.Text = dsData.Tables(0).Rows(i - 1).Item("BAGMAKINGDES").ToString()
                                Else
                                    txt.Text = ""
                                End If
                            Else

                            End If
                        End If
                        tdInner.Controls.Add(txt)

                    ElseIf j = 1 Then
                        InnerTdSetting(tdInner, "60px", "Right")
                        txt = New TextBox
                        txt.CssClass = "MatSmallTextBox"
                        txt.ID = "txtPCFORMATSPRODUCED" + i.ToString()
                        If PCval(i - 1) <> "" Then
                            txt.Text = PCval(i - 1)
                        Else

                            If dsData.Tables(0).Rows.Count >= i Then
                                If dsData.Tables(0).Rows(i - 1).Item("PCFORMATSPRODUCED").ToString() <> "" Then
                                    txt.Text = dsData.Tables(0).Rows(i - 1).Item("PCFORMATSPRODUCED").ToString()
                                Else
                                    txt.Text = ""
                                End If
                            Else
                                txt.Text = ""
                            End If
                        End If
                        tdInner.Controls.Add(txt)


                    ElseIf j = 2 Then
                        InnerTdSetting(tdInner, "90px", "Right")
                        txt = New TextBox
                        txt.CssClass = "MatSmallTextBox"
                        txt.ID = "txtCAPACITYUNITS" + i.ToString()

                        If CapVal(i - 1) <> "" Then
                            txt.Text = CapVal(i - 1)
                        Else

                            If dsData.Tables(0).Rows.Count >= i Then
                                If dsData.Tables(0).Rows(i - 1).Item("CAPACITYUNITS").ToString() <> "" Then
                                    txt.Text = dsData.Tables(0).Rows(i - 1).Item("CAPACITYUNITS").ToString()
                                Else
                                    txt.Text = ""
                                End If
                            Else
                                txt.Text = ""
                            End If
                        End If
                        tdInner.Controls.Add(txt)


                    ElseIf j = 3 Then
                        InnerTdSetting(tdInner, "90px", "Right")
                        txt = New TextBox
                        txt.CssClass = "MatSmallTextBox"
                        txt.ID = "txtFREECAPACITY" + i.ToString()

                        If FreeVal(i - 1) <> "" Then
                            txt.Text = FreeVal(i - 1)
                        Else

                            If dsData.Tables(0).Rows.Count >= i Then
                                If dsData.Tables(0).Rows(i - 1).Item("FREECAPACITY").ToString() <> "" Then
                                    txt.Text = dsData.Tables(0).Rows(i - 1).Item("FREECAPACITY").ToString()
                                Else
                                    txt.Text = ""
                                End If
                            Else
                                txt.Text = ""
                            End If
                        End If
                        tdInner.Controls.Add(txt)
                    ElseIf j = 4 Then
                        InnerTdSetting(tdInner, "90px", "Right")
                        txt = New TextBox
                        txt.CssClass = "MatSmallTextBox"
                        txt.ID = "txtMACHINEDATED" + i.ToString()

                        If MacVal(i - 1) <> "" Then
                            txt.Text = MacVal(i - 1)
                        Else

                            If dsData.Tables(0).Rows.Count >= i Then
                                If dsData.Tables(0).Rows(i - 1).Item("MACHINEDATED").ToString() <> "" Then
                                    txt.Text = dsData.Tables(0).Rows(i - 1).Item("MACHINEDATED").ToString()
                                Else
                                    txt.Text = ""
                                End If
                            Else
                                txt.Text = ""
                            End If
                        End If
                        tdInner.Controls.Add(txt)

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

                tblBagM.Controls.Add(trInner)
            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub LinkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim count As Integer = Convert.ToInt32(hidRowNumBM.Value.ToString())
            For i = 0 To count
                BMDes(i) = Request.Form("tabRfpSManager$tabBagmaking$txtBagMakingDes" + (i + 1).ToString() + "")
                PCval(i) = Request.Form("tabRfpSManager$tabBagmaking$txtPCFORMATSPRODUCED" + (i + 1).ToString() + "")
                CapVal(i) = Request.Form("tabRfpSManager$tabBagmaking$txtCAPACITYUNITS" + (i + 1).ToString() + "")
                FreeVal(i) = Request.Form("tabRfpSManager$tabBagmaking$txtFREECAPACITY" + (i + 1).ToString() + "")
                MacVal(i) = Request.Form("tabRfpSManager$tabBagmaking$txtMACHINEDATED" + (i + 1).ToString() + "")

            Next
            'Session("Linkbuttonclicked") = "Addmore"
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hidRowNumBM.Value.ToString())
            numberDiv += 1
            hidRowNumBM.Value = numberDiv.ToString()
            GetBagMakingDetails()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnUpdateBM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateBM.Click
        Dim count As Integer = Convert.ToInt32(hidRowNumBM.Value.ToString())
        Dim BGdes(count) As String
        Dim PCFormat(count) As String
        Dim Capacity(count) As String
        Dim FCapacity(count) As String
        Dim MachineDated(count) As String
        For i = 0 To count - 1
            BGdes(i) = Request.Form("tabRfpSManager$tabBagmaking$txtBagMakingDes" + (i + 1).ToString() + "")
            PCFormat(i) = Request.Form("tabRfpSManager$tabBagmaking$txtPCFORMATSPRODUCED" + (i + 1).ToString() + "")
            Capacity(i) = Request.Form("tabRfpSManager$tabBagmaking$txtCAPACITYUNITS" + (i + 1).ToString() + "")
            FCapacity(i) = Request.Form("tabRfpSManager$tabBagmaking$txtFREECAPACITY" + (i + 1).ToString() + "")
            MachineDated(i) = Request.Form("tabRfpSManager$tabBagmaking$txtMACHINEDATED" + (i + 1).ToString() + "")
            'tabRfpSManager_tabBagmaking_txtBagMakingDes4()
        Next
        objUpIns.UpdateBagMaking(BGdes, PCFormat, Capacity, FCapacity, MachineDated, Session("SVendorID"), Session("ShidRfpID"))
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('Bag Making Machine Successfully');", True)

        hidRowNumBM.Value = ""
        GetBagMakingDetails()
    End Sub

#End Region

#Region "Price Option"

    Protected Sub GetPriceOptn()
        Dim Ds As New DataSet
        Try
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
                    lnkPriceOption.Attributes.Add("onclick", "return window.open('SupplierPrice.aspx?PriceID=" + lblPriceID.Text + "'); return false;")
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

    Protected Sub GrdPriceOption_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GrdPriceOption.PageIndexChanging
        Try
            GrdPriceOption.PageIndex = e.NewPageIndex
            BindGrdPriceOption()
        Catch ex As Exception
            Response.Write("Error:GrdPriceOption_PageIndexChanging:" + ex.Message.ToString())
        End Try
    End Sub

    Protected Sub BindGrdPriceOption()
        Try
            Dim Dts As New DataSet
            Dts = Session("GrdPriceOptionSupplier")
            GrdPriceOption.DataSource = Dts
            GrdPriceOption.DataBind()
            BindPriceOptn()
        Catch ex As Exception
            Response.Write("Error:BindGrdPriceOption:" + ex.Message.ToString())
        End Try
    End Sub

#End Region

#Region "Structure"

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
        Dim hid As New HiddenField
        Dim hidMat As New HiddenField
        Dim hidDes As New HiddenField
        Dim Link As New HyperLink
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
                    If hidGrpCount.Value <> "" And hidGrpCount.Value <> "-1" Then
                        Dim GrpCount As Integer = hidGrpCount.Value
                        lblNoStructData.Visible = False
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
                                        Link = New HyperLink
                                        hid = New HiddenField
                                        hidDes = New HiddenField
                                        hidMat = New HiddenField
                                        Link.ID = "hypMatDes" + l.ToString() + "_" + i.ToString()
                                        hid.ID = "hidMatid" + l.ToString() + "_" + i.ToString()
                                        hidDes.ID = "hidMatDes" + l.ToString() + "_" + i.ToString()
                                        hidMat.ID = "hidMatCat" + l.ToString() + "_" + i.ToString()

                                        Link.Width = 120
                                        Link.CssClass = "SavvyLink"

                                        Try
                                            If StructMatId(l, i) <> "" Then
                                                GetMaterialDetailsNew(Link, hid, StructMatId(l, i), hidDes, hidMat, "lblDensity_" + l.ToString() + "_" + i.ToString(), _
                                                                      Density, "hidDensity_" + l.ToString() + "_" + i.ToString())
                                            ElseIf DtGrpData.Rows(0).Item("S1").ToString() <> "" Or DtGrpData.Rows(0).Item("S1").ToString() <> "0" Then
                                                GetMaterialDetailsNew(Link, hid, DtGrpData.Rows(0).Item("S" + (i + 1).ToString()).ToString(), hidDes, hidMat, _
                                                                      "lblDensity_" + l.ToString() + "_" + i.ToString(), Density, "hidDensity_" + l.ToString() + "_" + i.ToString())
                                            Else
                                                GetMaterialDetailsNew(Link, hid, "0", hidDes, hidMat, "lblDensity_" + l.ToString() + "_" + i.ToString(), Density, _
                                                                      "hidDensity_" + l.ToString() + "_" + i.ToString())
                                            End If
                                        Catch ex As Exception
                                            GetMaterialDetailsNew(Link, hid, "0", hidDes, hidMat, "lblDensity_" + l.ToString() + "_" + i.ToString(), Density, _
                                                                  "hidDensity_" + l.ToString() + "_" + i.ToString())
                                        End Try

                                        tdInner.Controls.Add(hid)
                                        tdInner.Controls.Add(hidDes)
                                        tdInner.Controls.Add(hidMat)
                                        tdInner.Controls.Add(Link)
                                        trInner.Controls.Add(tdInner)
                                        'End
                                    ElseIf j = 2 Then
                                        InnerTdSetting(tdInner, "90px", "Left")
                                        txtthick = New TextBox
                                        txtthick.CssClass = "SmallTextBox"
                                        txtthick.AutoPostBack = True
                                        Try
                                            If StructThick(l, i) <> "" Then
                                                txtthick.Text = StructThick(l, i)
                                            Else
                                                txtthick.Text = DtGrpData.Rows(0).Item("TH" + (i + 1).ToString()).ToString()
                                            End If
                                        Catch ex As Exception
                                            txtthick.Text = "0"
                                        End Try
                                        TotalThik = TotalThik + txtthick.Text.ToString()
                                        txtthick.ID = "txtthick_" + l.ToString() + "_" + i.ToString()
                                        AddHandler txtthick.TextChanged, AddressOf TextBox_Thickness
                                        tdInner.Controls.Add(txtthick)
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
                                        txtPrice = New TextBox
                                        txtPrice.CssClass = "SmallTextBox"

                                        Try
                                            If StructPrice(l, i) <> "" Then
                                                txtPrice.Text = StructPrice(l, i)
                                            Else
                                                txtPrice.Text = DtGrpData.Rows(0).Item("P" + (i + 1).ToString()).ToString()
                                            End If
                                        Catch ex As Exception
                                            txtPrice.Text = "0"
                                        End Try
                                        TotalPrice = TotalPrice + txtPrice.Text.ToString()
                                        txtPrice.ID = "txtPriceMat_" + l.ToString() + "_" + i.ToString()
                                        tdInner.Controls.Add(txtPrice)
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
                If hidGrpCount.Value <> "" And hidGrpCount.Value <> "-1" Then
                    Dim GrpCount As Integer = hidGrpCount.Value
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
                                    Link = New HyperLink
                                    hid = New HiddenField
                                    hidDes = New HiddenField
                                    hidMat = New HiddenField
                                    Link.ID = "hypMatDes" + l.ToString() + "_" + i.ToString()
                                    hid.ID = "hidMatid" + l.ToString() + "_" + i.ToString()
                                    hidDes.ID = "hidMatDes" + l.ToString() + "_" + i.ToString()
                                    hidMat.ID = "hidMatCat" + l.ToString() + "_" + i.ToString()

                                    Link.Width = 120
                                    Link.CssClass = "SavvyLink"

                                    Try
                                        If StructMatId(l, i) <> "" Then
                                            GetMaterialDetailsNew(Link, hid, StructMatId(l, i), hidDes, hidMat, "lblDensity_" + l.ToString() + "_" + i.ToString(), _
                                                                  Density, "hidDensity_" + l.ToString() + "_" + i.ToString())
                                        ElseIf DtGrpData.Rows(0).Item("S1").ToString() <> "" Or DtGrpData.Rows(0).Item("S1").ToString() <> "0" Then
                                            GetMaterialDetailsNew(Link, hid, DtGrpData.Rows(0).Item("S" + (i + 1).ToString()).ToString(), hidDes, hidMat, _
                                                                  "lblDensity_" + l.ToString() + "_" + i.ToString(), Density, "hidDensity_" + l.ToString() + "_" + i.ToString())
                                        Else
                                            GetMaterialDetailsNew(Link, hid, "0", hidDes, hidMat, "lblDensity_" + l.ToString() + "_" + i.ToString(), Density, _
                                                                  "hidDensity_" + l.ToString() + "_" + i.ToString())
                                        End If
                                    Catch ex As Exception
                                        GetMaterialDetailsNew(Link, hid, "0", hidDes, hidMat, "lblDensity_" + l.ToString() + "_" + i.ToString(), Density, _
                                                              "hidDensity_" + l.ToString() + "_" + i.ToString())
                                    End Try

                                    tdInner.Controls.Add(hid)
                                    tdInner.Controls.Add(hidDes)
                                    tdInner.Controls.Add(hidMat)
                                    tdInner.Controls.Add(Link)
                                    trInner.Controls.Add(tdInner)
                                    'End
                                ElseIf j = 2 Then
                                    InnerTdSetting(tdInner, "90px", "Left")
                                    txtthick = New TextBox
                                    txtthick.CssClass = "SmallTextBox"
                                    txtthick.AutoPostBack = True
                                    Try
                                        If StructThick(l, i) <> "" Then
                                            txtthick.Text = StructThick(l, i)
                                        Else
                                            txtthick.Text = DtGrpData.Rows(0).Item("TH" + (i + 1).ToString()).ToString()
                                        End If
                                    Catch ex As Exception
                                        txtthick.Text = "0"
                                    End Try
                                    TotalThik = TotalThik + txtthick.Text.ToString()
                                    txtthick.ID = "txtthick_" + l.ToString() + "_" + i.ToString()
                                    AddHandler txtthick.TextChanged, AddressOf TextBox_Thickness
                                    tdInner.Controls.Add(txtthick)
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
                                    txtPrice = New TextBox
                                    txtPrice.CssClass = "SmallTextBox"

                                    Try
                                        If StructPrice(l, i) <> "" Then
                                            txtPrice.Text = StructPrice(l, i)
                                        Else
                                            txtPrice.Text = DtGrpData.Rows(0).Item("P" + (i + 1).ToString()).ToString()
                                        End If
                                    Catch ex As Exception
                                        txtPrice.Text = "0"
                                    End Try
                                    TotalPrice = TotalPrice + txtPrice.Text.ToString()
                                    txtPrice.ID = "txtPriceMat_" + l.ToString() + "_" + i.ToString()
                                    tdInner.Controls.Add(txtPrice)
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

    Protected Sub GetMaterialDetailsNew(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal MatId As Integer, ByVal hidDes As HiddenField, ByVal hidMat As HiddenField, _
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
            Path = "PopUp/GetMatPopUp.aspx?Des=tabRfpSManager_tabStruct_" + LinkMat.ClientID + "&Id=tabRfpSManager_tabStruct_" + hid.ClientID + "&GradeDes=&GradeId=&SG=&MatDes=tabRfpSManager_tabStruct_" + hidDes.ClientID + "&MatGrp=" + LinkMat.Text + "&Grp=" + LinkMat.Text + "&LinkId=N&Density=tabRfpSManager_tabStruct_" + labelGrade + "&HidDen=tabRfpSManager_tabStruct_" + hidDensity + ""
            LinkMat.NavigateUrl = "javascript:ShowPopWindow_Struct('" + Path + "')"
            LinkMat.Text = linkText
        Catch ex As Exception
            lblError.Text = "Error:GetMaterialDetailsNew:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub TextBox_Thickness(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim SGS As String
        Dim TotalLayerWeight As Double
        Dim TotalThickness As Double
        Dim TotalDensity As Double
        Dim TotalWeightPercn As Double
        Dim TotalPrice As Double
        Dim GrpCount As Integer = hidGrpCount.Value
        Try
            For l = 0 To GrpCount
                For i = 0 To 11
                    StructMatId(l, i) = Request.Form("tabRfpSManager$tabStruct$hidMatid" + l.ToString() + "_" + i.ToString())
                    If StructMatId(l, i) <> "0" Then
                        StructThick(l, i) = Request.Form("tabRfpSManager$tabStruct$txtthick_" + l.ToString() + "_" + i.ToString())
                        StructPrice(l, i) = Request.Form("tabRfpSManager$tabStruct$txtPriceMat_" + l.ToString() + "_" + i.ToString())
                        SGS = Request.Form("tabRfpSManager$tabStruct$hidDensity_" + l.ToString() + "_" + i.ToString())
                        If SGS <> "" Then
                            CalWeight(l, i) = FormatNumber((1000 * (CDbl(StructThick(l, i).ToString()) / 1000) / 1728) * 62.4 * CDbl(SGS.ToString()), 3)
                        End If
                        TotalLayerWeight = TotalLayerWeight + CalWeight(l, i)
                        TotalThickness = TotalThickness + StructThick(l, i)
                        TotalDensity = TotalDensity + SGS
                        TotalPrice = TotalPrice + StructPrice(l, i)
                    End If
                Next
                For j = 0 To 11
                    StructMatId(l, j) = Request.Form("tabRfpSManager$tabStruct$hidMatid" + l.ToString() + "_" + j.ToString())
                    If StructMatId(l, j) <> "0" Then
                        weightPercn(l, j) = FormatNumber(CDbl(CalWeight(l, j)) / CDbl(TotalLayerWeight) * 100, 3)
                        TotalWeightPercn = TotalWeightPercn + weightPercn(l, j)
                    End If
                Next

                TtlThickness(l) = TotalThickness
                TtlDensity(l) = TotalDensity
                TtlWeight(l) = TotalWeightPercn
                TtlPrice(l) = TotalPrice

                TotalThickness = 0.0
                TotalDensity = 0.0
                TotalWeightPercn = 0.0
                TotalPrice = 0.0
                TotalLayerWeight = 0.0
            Next
            GetStructure()
        Catch ex As Exception
            lblError.Text = "Error:TextBox_Thickness:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnUpdateStruct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateStruct.Click
        Try
            Dim MATID(11) As String
            Dim Thickness(11) As String
            Dim Weight(11) As String
            Dim PriceMat(11) As String
            Dim GrpID As String = String.Empty
            Dim GrpCount As Integer = hidGrpCount.Value

            'Delete Existing Data
            If isSkuOnly Then
                objUpIns.DelSkuStruct(Session("ShidRfpID"), Session("SVendorID"), "")
            Else
                objUpIns.DelGrpStruct(Session("ShidRfpID"), Session("SVendorID"), hidMasterGrpID.Value)
            End If

            'Insert New/updated data
            For l = 0 To GrpCount
                GrpID = Request.Form("tabRfpSManager$tabStruct$hidGrp_" + l.ToString())
                For i = 0 To 11
                    MATID(i) = Request.Form("tabRfpSManager$tabStruct$hidMatid" + l.ToString() + "_" + i.ToString() + "")
                    Thickness(i) = Request.Form("tabRfpSManager$tabStruct$txtthick_" + l.ToString() + "_" + i.ToString() + "")
                    Weight(i) = Request.Form("tabRfpSManager$tabStruct$hidwd_" + l.ToString() + "_" + i.ToString() + "") 'weightPercn(l, i)
                    PriceMat(i) = Request.Form("tabRfpSManager$tabStruct$txtPriceMat_" + l.ToString() + "_" + i.ToString() + "")
                Next
                If isSkuOnly Then
                    objUpIns.InsUpdateSkuStruct(Session("ShidRfpID"), Session("SVendorID"), GrpID, MATID, Thickness, Weight, PriceMat)
                Else
                    objUpIns.InsUpdateStruct(Session("ShidRfpID"), Session("SVendorID"), GrpID, MATID, Thickness, Weight, PriceMat, hidMasterGrpID.Value)
                End If
                ReDim MATID(11)
                ReDim Thickness(11)
                ReDim Weight(11)
                ReDim PriceMat(11)
            Next
            GetStructure()
        Catch ex As Exception
            lblError.Text = "Error: btnUpdateStruct_Click() " + ex.Message()
        End Try
    End Sub

    Protected Sub btnExtrapSkuLvl_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExtrapSkuLvl.Click
        Dim StrQry As Boolean
        Try
            If hidMasterGrpID.Value = "" Or hidMasterGrpID.Value = "0" Then
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "AlertMasterGrp", "alert('Please select master group');", True)
            Else
                StrQry = objGetdata.ChkGrpStructDetailQRY(Session("ShidRfpID"), hidMasterGrpID.Value, Session("SVendorID"))
                If StrQry Then
                    'Delete Existing Data
                    objUpIns.DelSkuStruct(Session("ShidRfpID"), Session("SVendorID"), hidMasterGrpID.Value())

                    'Insert New/updated data
                    objUpIns.InsSkuStruct(Session("ShidRfpID"), Session("SVendorID"), hidMasterGrpID.Value())
                End If
                GetStructure()
            End If

        Catch ex As Exception
            lblError.Text = "Error: btnExtrapSkuLvl_Click() " + ex.Message()
        End Try
    End Sub

    Protected Sub GetGrpInfo()
        Try
            DsGroup = objGetdata.GetGroupByMasterGrpS(hidMasterGrpID.Value())
            If DsGroup.Tables(0).Rows.Count > 0 Then
                hidGrpCount.Value = DsGroup.Tables(0).Rows.Count - 1
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
                hidGrpCount.Value = DsSkuFStrct.Tables(0).Rows.Count - 1
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

#End Region

    Protected Sub btnRefreshVList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefreshVList.Click
        Try
            'GetUsersList()
        Catch ex As Exception
            lblError.Text = "Error: btnRefreshVList_Click() " + ex.Message()
        End Try
    End Sub

    Protected Sub btnMasterSel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMasterSel.Click
        Try
            If hidMasterGrpID.Value <> "" Then
                lnkSelMasterGrp.Text = hidMasterGrpDes.Value
                If isSkuOnly Then
                    GetSkuForStructInfo()
                Else
                    If hidMasterGrpID.Value <> "" Then
                        GetGrpInfo()
                    End If
                End If
                GetStructure()
            End If
        Catch ex As Exception
            lblError.Text = "Error: btnMasterSel_Click() " + ex.Message()
        End Try
    End Sub

End Class
