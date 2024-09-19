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

Partial Class Pages_SavvyPackPro_AnalyzeProposals
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData
    'For Vendor
    Public VendorCnt As Integer
    Public VendorDes As New ArrayList
    'For PriceType
    Public PriceTypeCnt As Integer
    Public PricetypeDesp As New ArrayList
    Public StructTypeCnt As Integer
    Public StructtypeDesp As New ArrayList
    'For Groups
    Public GrpCnt As Integer
    Public GrpDesp As New ArrayList
    Private isPageLoaded As Boolean = True

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            If Session("USERID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                hidSortIdBMVendor.Value = "0"
                ChkExistingRfp()
                GetPageDetails()
                GetPricePageDetails()
                GetPriceCostPageDetails()
                GetPriceOptnPageDetails()
                GetStructPageDetails()
                GetBatchPriceDetails()
                BindPRICETable()
            End If
        Catch ex As Exception
            lblError.Text = "Error: Page_Load() " + ex.Message()
        End Try
    End Sub

    Protected Sub tabAnalyzePPSl_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabAnalyzePPSl.ActiveTabChanged
        Try
            If isPageLoaded Then
                loading.Style.Add("display", "none")
                isPageLoaded = False
                If tabAnalyzePPSl.ActiveTabIndex = "0" Then
                    GetPageDetails()
                ElseIf tabAnalyzePPSl.ActiveTabIndex = "1" Then
                    GetPricePageDetails()
                ElseIf tabAnalyzePPSl.ActiveTabIndex = "2" Then
                    GetPriceCostPageDetails()
                ElseIf tabAnalyzePPSl.ActiveTabIndex = "3" Then
                    GetPriceOptnPageDetails()
                ElseIf tabAnalyzePPSl.ActiveTabIndex = "4" Then
                    GetStructPageDetails()
                ElseIf tabAnalyzePPSl.ActiveTabIndex = "5" Then
                    GetBatchPriceDetails()
                ElseIf tabAnalyzePPSl.ActiveTabIndex = "6" Then
                    BindPRICETable()
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error: tabAnalyzePPSl_ActiveTabChanged() " + ex.Message()
        End Try
    End Sub

#Region "Rfp Region"

    Protected Sub ChkExistingRfp()
        Dim DsCheckRfp As New DataSet()
        Try
            imgUpdate.Attributes.Add("onClick", "return ShowPopUpGraph('../../Charts/SavvyPackProCharts/Default.aspx');")
            ' DsCheckRfp = objGetdata.GetRFPbyUserID(Session("USERID"))
            If Session("hidRfpID") = Nothing Then
                DsCheckRfp = objGetdata.GetLatestRFPbyLicenseID(Session("LicenseNo"), Session("USERID"))
                If DsCheckRfp.Tables(0).Rows.Count > 0 Then
                    GetRfpDetails(DsCheckRfp.Tables(0).Rows(0).Item("ID").ToString())
                Else
                    RfpDetail.Visible = False
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
                If DsRfpdet.Tables(0).Rows.Count > 0 Then
                    RfpDetail.Visible = True
                    lnkSelRFP.Text = DsRfpdet.Tables(0).Rows(0).Item("DES1").ToString()
                    lblSelRfpID.Text = DsRfpdet.Tables(0).Rows(0).Item("ID").ToString()
                    lblType.Text = DsRfpdet.Tables(0).Rows(0).Item("TYPEDESC").ToString()
                    hidRfpID.Value = lblSelRfpID.Text
                    Session("hidRfpID") = lblSelRfpID.Text
                    lblSelRfpDes.Text = DsRfpdet.Tables(0).Rows(0).Item("DES1").ToString()
                    If Session("USERID") = DsRfpdet.Tables(0).Rows(0).Item("USERID").ToString() Then
                        tabAnalyzePPSl.Enabled = True
                    Else
                        tabAnalyzePPSl.Enabled = False
                    End If
                    Session("RFPID") = lblSelRfpID.Text
                    'loadTab()
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('System isn't able to find RFPID. Please try again.');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error: GetRfpDetails() " + ex.Message()
        End Try
    End Sub

    Protected Sub loadTab()
        Try
            GetPageDetails()
            GetPricePageDetails()
            GetPriceCostPageDetails()
            GetStructPageDetails()
            GetBatchPriceDetails()
            BindPRICETable()
        Catch ex As Exception
            lblError.Text = "Error: GetRfpDetails() " + ex.Message()
        End Try
    End Sub

#End Region

#Region "Terms"

    Protected Sub GetPageDetails()
        Dim trHeader As New TableRow
        Dim tdHeader As TableCell
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim DsConnVendor As New DataSet()
        Dim DsTermData As New DataSet()
        Dim DtTermData As New DataTable()
        Dim DvTermData As New DataView()
        Dim DsTemsByRfpID As New DataSet()
        Try
            tblEditQ.Rows.Clear()
            tblcustomize.Rows.Clear()
            DsConnVendor = objGetdata.GetNoOfVendorByRFPID(hidRfpID.Value)
            DsTermData = objGetdata.GetTemsCommentByRFPID(hidRfpID.Value)
            DvTermData = DsTermData.Tables(0).DefaultView
            DsTemsByRfpID = objGetdata.GetTemsRFPID(hidRfpID.Value, Session("USERID"))
            If DsTermData.Tables(0).Rows.Count > 0 Then
                lblNoData.Visible = False
                For k = 1 To 3
                    trHeader = New TableRow
                    For i = 1 To 4
                        If k = 1 Then
                            Select Case i
                                Case 1
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "50px", "Term #", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 2
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "250px", "Term Item", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 3
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "350px", "Term Actual Des", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 4
                                    For j = 0 To DsConnVendor.Tables(0).Rows.Count - 1
                                        tdHeader = New TableCell
                                        HeaderTdSetting(tdHeader, "200px", "Terms", "2")
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
                                    HeaderTdSetting(tdHeader, "250px", "", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 3
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "350px", "", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 4
                                    For j = 0 To DsConnVendor.Tables(0).Rows.Count - 1
                                        tdHeader = New TableCell
                                        Header2TdSetting(tdHeader, "200px", "" + DsConnVendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "", "2")
                                        trHeader.Controls.Add(tdHeader)
                                    Next
                            End Select
                        ElseIf k = 3 Then
                            Select Case i
                                Case 1
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "50px", "", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 2
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "250px", "", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 3
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "350px", "", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 4
                                    For j = 0 To DsConnVendor.Tables(0).Rows.Count - 1
                                        For l = 0 To 1
                                            If l = 0 Then
                                                tdHeader = New TableCell
                                                Header2TdSetting(tdHeader, "100px", "Accepted", "1")
                                                trHeader.Controls.Add(tdHeader)
                                            Else
                                                tdHeader = New TableCell
                                                Header2TdSetting(tdHeader, "100px", "Comments", "1")
                                                trHeader.Controls.Add(tdHeader)
                                            End If
                                        Next
                                    Next
                            End Select
                        End If
                    Next
                    trHeader.Height = 30
                    tblEditQ.Controls.Add(trHeader)
                Next

                'pt changes               
                    For k = 1 To 3
                        trHeader = New TableRow
                        For i = 1 To 4
                            If k = 1 Then
                                Select Case i
                                    Case 1
                                        tdHeader = New TableCell
                                        HeaderTdSetting(tdHeader, "50px", "Term #", "1")
                                        trHeader.Controls.Add(tdHeader)
                                    Case 2
                                        tdHeader = New TableCell
                                        HeaderTdSetting(tdHeader, "250px", "Term Item", "1")
                                        trHeader.Controls.Add(tdHeader)
                                    Case 3
                                        tdHeader = New TableCell
                                        HeaderTdSetting(tdHeader, "350px", "Term Actual Des", "1")
                                        trHeader.Controls.Add(tdHeader)
                                    Case 4
                                        For j = 0 To DsConnVendor.Tables(0).Rows.Count - 1
                                            tdHeader = New TableCell
                                            HeaderTdSetting(tdHeader, "200px", "Terms", "2")
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
                                        HeaderTdSetting(tdHeader, "250px", "", "1")
                                        trHeader.Controls.Add(tdHeader)
                                    Case 3
                                        tdHeader = New TableCell
                                        HeaderTdSetting(tdHeader, "350px", "", "1")
                                        trHeader.Controls.Add(tdHeader)
                                    Case 4
                                        For j = 0 To DsConnVendor.Tables(0).Rows.Count - 1
                                            tdHeader = New TableCell
                                            Header2TdSetting(tdHeader, "200px", "" + DsConnVendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "", "2")
                                            trHeader.Controls.Add(tdHeader)
                                        Next
                                End Select
                            ElseIf k = 3 Then
                                Select Case i
                                    Case 1
                                        tdHeader = New TableCell
                                        HeaderTdSetting(tdHeader, "50px", "", "1")
                                        trHeader.Controls.Add(tdHeader)
                                    Case 2
                                        tdHeader = New TableCell
                                        HeaderTdSetting(tdHeader, "250px", "", "1")
                                        trHeader.Controls.Add(tdHeader)
                                    Case 3
                                        tdHeader = New TableCell
                                        HeaderTdSetting(tdHeader, "350px", "", "1")
                                        trHeader.Controls.Add(tdHeader)
                                    Case 4
                                        For j = 0 To DsConnVendor.Tables(0).Rows.Count - 1
                                            For l = 0 To 1
                                                If l = 0 Then
                                                    tdHeader = New TableCell
                                                    Header2TdSetting(tdHeader, "100px", "Accepted", "1")
                                                    trHeader.Controls.Add(tdHeader)
                                                Else
                                                    tdHeader = New TableCell
                                                    Header2TdSetting(tdHeader, "100px", "Comments", "1")
                                                    trHeader.Controls.Add(tdHeader)
                                                End If
                                            Next
                                        Next
                                End Select
                            End If
                        Next
                        trHeader.Height = 30
                        tblcustomize.Controls.Add(trHeader)
                    Next
                    'end pt changes


                    'For Data
                    For i = 0 To DsTemsByRfpID.Tables(0).Rows.Count - 1 'For Number of terms
                        If DsTemsByRfpID.Tables(0).Rows(i).Item("ISDEFAULT").ToString() = "Y" Then

                            trInner = New TableRow
                            For j = 0 To 3
                                Select Case j
                                    Case 0
                                        tdInner = New TableCell
                                    tdInner.Text = DsTemsByRfpID.Tables(0).Rows(i).Item("TERMSEQ").ToString() '(i + 1).ToString()
                                        InnerTdSetting(tdInner, "50px", "")
                                        trInner.Controls.Add(tdInner)
                                        trInner.Controls.Add(tdInner)
                                    Case 1
                                        tdInner = New TableCell
                                        tdInner.Text = DsTemsByRfpID.Tables(0).Rows(i).Item("TITLE").ToString()
                                        InnerTdSetting(tdInner, "250px", "")
                                        trInner.Controls.Add(tdInner)
                                    Case 2
                                        tdInner = New TableCell
                                        tdInner.Text = DsTemsByRfpID.Tables(0).Rows(i).Item("DESCRIPTION").ToString()
                                        InnerTdSetting(tdInner, "350px", "")
                                        trInner.Controls.Add(tdInner)
                                    Case 3
                                        For l = 0 To DsConnVendor.Tables(0).Rows.Count - 1 'for number of vendors
                                            DvTermData.RowFilter = "TERMID=" + DsTemsByRfpID.Tables(0).Rows(i).Item("TERMID").ToString() + "AND VENDORID=" + DsConnVendor.Tables(0).Rows(l).Item("VENDORID").ToString()
                                            DtTermData = DvTermData.ToTable()
                                            For m = 0 To 1
                                                If m = 0 Then
                                                    tdInner = New TableCell
                                                    Try
                                                        tdInner.Text = DtTermData.Rows(0).Item("ACCEPTED").ToString()
                                                    Catch ex As Exception
                                                        tdInner.Text = ""
                                                    End Try
                                                    InnerTdSetting(tdInner, "100px", "")
                                                    trInner.Controls.Add(tdInner)
                                                Else
                                                    tdInner = New TableCell
                                                    Try
                                                        tdInner.Text = DtTermData.Rows(0).Item("COMMENTS").ToString()
                                                    Catch ex As Exception
                                                        tdInner.Text = ""
                                                    End Try
                                                    InnerTdSetting(tdInner, "100px", "")
                                                    trInner.Controls.Add(tdInner)
                                                End If
                                            Next
                                        Next
                                End Select
                            Next
                            If (i Mod 2 = 0) Then
                                trInner.CssClass = "AlterNateColor1"
                            Else
                                trInner.CssClass = "AlterNateColor2"
                            End If
                            tblEditQ.Controls.Add(trInner)

                            'pt changes
                        Else
                            tblcustomize.Visible = True
                        lblcustomize.Visible = True
                        lblcustomize.Text = "<br>Customize Terms:"
                            trInner = New TableRow
                            For j = 0 To 3
                                Select Case j
                                    Case 0
                                        tdInner = New TableCell
                                    tdInner.Text = DsTemsByRfpID.Tables(0).Rows(i).Item("TERMSEQ").ToString() '(i + 1).ToString()

                                        InnerTdSetting(tdInner, "50px", "")
                                        trInner.Controls.Add(tdInner)
                                        trInner.Controls.Add(tdInner)
                                    Case 1
                                        tdInner = New TableCell
                                        tdInner.Text = DsTemsByRfpID.Tables(0).Rows(i).Item("TITLE").ToString()
                                        InnerTdSetting(tdInner, "250px", "")
                                        trInner.Controls.Add(tdInner)
                                    Case 2
                                        tdInner = New TableCell
                                        tdInner.Text = DsTemsByRfpID.Tables(0).Rows(i).Item("DESCRIPTION").ToString()
                                        InnerTdSetting(tdInner, "350px", "")
                                        trInner.Controls.Add(tdInner)
                                    Case 3
                                        For l = 0 To DsConnVendor.Tables(0).Rows.Count - 1 'for number of vendors
                                            DvTermData.RowFilter = "TERMID=" + DsTemsByRfpID.Tables(0).Rows(i).Item("TERMID").ToString() + "AND VENDORID=" + DsConnVendor.Tables(0).Rows(l).Item("VENDORID").ToString()
                                            DtTermData = DvTermData.ToTable()
                                            For m = 0 To 1
                                                If m = 0 Then
                                                    tdInner = New TableCell
                                                    Try
                                                        tdInner.Text = DtTermData.Rows(0).Item("ACCEPTED").ToString()
                                                    Catch ex As Exception
                                                        tdInner.Text = ""
                                                    End Try
                                                    InnerTdSetting(tdInner, "100px", "")
                                                    trInner.Controls.Add(tdInner)
                                                Else
                                                    tdInner = New TableCell
                                                    Try
                                                        tdInner.Text = DtTermData.Rows(0).Item("COMMENTS").ToString()
                                                    Catch ex As Exception
                                                        tdInner.Text = ""
                                                    End Try
                                                    InnerTdSetting(tdInner, "100px", "")
                                                    trInner.Controls.Add(tdInner)
                                                End If
                                            Next
                                        Next
                                End Select
                            Next
                            If (i Mod 2 = 0) Then
                                trInner.CssClass = "AlterNateColor1"
                            Else
                                trInner.CssClass = "AlterNateColor2"
                            End If
                            tblcustomize.Controls.Add(trInner)

                            'end pt changes
                        End If

                    Next
                'End

                'additional term
                Dim DsTermsByVendor As New DataSet()
                Dim dvTermByvendor As New DataView
                Dim dtTermByvendor As New DataTable
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
                Dim dataprinttech As DataSet
                Dim datapriceopt As DataSet
                DsTermsByVendor = objGetdata.CheckAddtnTermByRFPID(Session("hidRfpID").ToString())
                dvTermByvendor = DsTermsByVendor.Tables(0).DefaultView

                For k = 1 To 3
                    trHeader = New TableRow
                    For i = 1 To 4
                        If k = 1 Then
                            Select Case i
                                Case 1
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "50px", "Term #", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 2
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "250px", "Additional Terms", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 3
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "200px", "Description", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 4
                                    For j = 0 To DsConnVendor.Tables(0).Rows.Count - 1
                                        tdHeader = New TableCell
                                        HeaderTdSetting(tdHeader, "200px", "Terms", "3")
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
                                    HeaderTdSetting(tdHeader, "250px", "", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 3
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "350px", "", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 4
                                    For j = 0 To DsConnVendor.Tables(0).Rows.Count - 1
                                        tdHeader = New TableCell
                                        Header2TdSetting(tdHeader, "200px", "" + DsConnVendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "", "3")
                                        trHeader.Controls.Add(tdHeader)
                                    Next
                            End Select
                        ElseIf k = 3 Then
                            Select Case i
                                Case 1
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "50px", "", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 2
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "250px", "", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 3
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "350px", "", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 4
                                    For j = 0 To DsConnVendor.Tables(0).Rows.Count - 1
                                        For h As Integer = 0 To 2
                                            If h = 0 Then
                                                tdHeader = New TableCell
                                                HeaderTdSetting(tdHeader, "200px", "Comments", "1")
                                                trHeader.Controls.Add(tdHeader)
                                            ElseIf h = 1 Then
                                                tdHeader = New TableCell
                                                HeaderTdSetting(tdHeader, "200px", " Price Option", "1")
                                                trHeader.Controls.Add(tdHeader)
                                            ElseIf h = 2 Then
                                                tdHeader = New TableCell
                                                HeaderTdSetting(tdHeader, "200px", "Print Tech", "1")
                                                trHeader.Controls.Add(tdHeader)
                                            End If
                                        Next
                                        'tdHeader = New TableCell
                                        'HeaderTdSetting(tdHeader, "200px", "Comments", "3")
                                        'trHeader.Controls.Add(tdHeader)
                                    Next
                            End Select
                        End If
                    Next
                    trHeader.Height = 30
                    tblAddTerm.Controls.Add(trHeader)
                Next

                ''''''''''''''''''''''''''''''''''''''''''''''''

                For j = 0 To 0 'DsTermsByVendor.Tables(0).Rows.Count - 1
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
                            tdInnerAT.Controls.Add(lbldes)
                            trInnerAT.Controls.Add(tdInnerAT)
                        ElseIf k = 3 Then
                            For l = 0 To DsConnVendor.Tables(0).Rows.Count - 1 'for number of vendors
                                For m = 0 To 2
                                    If m = 0 Then
                                        tdInnerAT = New TableCell
                                        dvTermByvendor.RowFilter = " VENDORID=" + DsConnVendor.Tables(0).Rows(l).Item("VENDORID").ToString()
                                        dtTermByvendor = dvTermByvendor.ToTable()
                                        lblcomment = New Label
                                        InnerTdSetting(tdInnerAT, "100px", "Center")
                                        lblcomment.Text = dtTermByvendor.Rows(0).Item("COMMENTS").ToString()
                                        lblcomment.ID = l.ToString() + "_ATC_" + j.ToString()
                                        lblcomment.Font.Size = 10
                                        'tdInnerAT.ColumnSpan = "2"
                                        tdInnerAT.Controls.Add(lblcomment)
                                        trInnerAT.Controls.Add(tdInnerAT)
                                    ElseIf m = 1 Then
                                        tdInnerAT = New TableCell
                                        dvTermByvendor.RowFilter = " VENDORID=" + DsConnVendor.Tables(0).Rows(l).Item("VENDORID").ToString()
                                        dtTermByvendor = dvTermByvendor.ToTable()
                                        lblcomment = New Label
                                        InnerTdSetting(tdInnerAT, "100px", "Center")
                                        If dtTermByvendor.Rows(0).Item("PRICEREQID").ToString() <> "" Then
                                            datapriceopt = objGetdata.getpriceoptionbyid(dtTermByvendor.Rows(0).Item("PRICEREQID").ToString())
                                            lblcomment.Text = datapriceopt.Tables(0).Rows(0).Item("DESCRIPTION").ToString() 'dtTermByvendor.Rows(0).Item("PRICEREQID").ToString()
                                        Else
                                            lblcomment.Text = ""

                                        End If
                                       lblcomment.ID = l.ToString() + "_ATC_" + j.ToString()
                                        lblcomment.Font.Size = 10
                                        'tdInnerAT.ColumnSpan = "2"
                                        tdInnerAT.Controls.Add(lblcomment)
                                        trInnerAT.Controls.Add(tdInnerAT)
                                    ElseIf m = 2 Then
                                        tdInnerAT = New TableCell
                                        lblcomment = New Label
                                        InnerTdSetting(tdInnerAT, "100px", "Center")
                                        dvTermByvendor.RowFilter = " VENDORID=" + DsConnVendor.Tables(0).Rows(l).Item("VENDORID").ToString()
                                        dtTermByvendor = dvTermByvendor.ToTable()
                                        If dtTermByvendor.Rows(0).Item("PRINTINGTECHID").ToString() <> "" Then
                                            dataprinttech = objGetdata.getprinttechdes(Session("hidRfpID"), dtTermByvendor.Rows(0).Item("PRINTINGTECHID").ToString())
                                            lblcomment.Text = dataprinttech.Tables(0).Rows(0).Item("PRINTTECHDES").ToString() 'dtTermByvendor.Rows(0).Item("PRINTINGTECHID").ToString()
                                        Else
                                            lblcomment.Text = ""
                                        End If

                                        lblcomment.ID = l.ToString() + "_ATC_" + j.ToString()
                                        lblcomment.Font.Size = 10
                                        'tdInnerAT.ColumnSpan = "2"
                                        tdInnerAT.Controls.Add(lblcomment)
                                        trInnerAT.Controls.Add(tdInnerAT)
                                    End If
                                Next
                              
                            Next
                        End If
                    Next
                    trInnerAT.CssClass = "AlterNateColor1"
                    tblAddTerm.Controls.Add(trInnerAT)
                Next


                'end additional term



                Else
                    lblNoData.Visible = True
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

            ds = objGetdata.GetPriceOptnForTerms(Session("hidRfpID"), "")
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

            ds = objGetdata.GetPrintTechForTerms(Session("hidRfpID"), "")
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

#End Region

#Region "Price"

    Protected Sub GetPricePageDetails()
        Dim trHeader As New TableRow
        Dim tdHeader As TableCell
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim DsPriceData As New DataSet()
        Dim DtPriceData As New DataTable()
        Dim DvPriceData As New DataView()
        Dim DsSKUByRfpID As New DataSet()
        Dim DsUniqueVendor As New DataSet()
        Try
            tblPA.Rows.Clear()
            DsUniqueVendor = objGetdata.GetUniqueVnedorsByRfpID(hidRfpID.Value)
            DsPriceData = objGetdata.GetPriceByRfpID(hidRfpID.Value)
            DvPriceData = DsPriceData.Tables(0).DefaultView
            DsSKUByRfpID = objGetdata.GetSkuByRfpID_S(hidRfpID.Value, Session("UserId"))
            If DsPriceData.Tables(0).Rows.Count > 0 Then
                lblNoRecordPA.Visible = False
                For k = 1 To 3
                    trHeader = New TableRow
                    For i = 1 To 4
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
                                    HeaderTdSetting(tdHeader, "120px", "SHOULD COST", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 4
                                    For j = 0 To DsUniqueVendor.Tables(0).Rows.Count - 1
                                        tdHeader = New TableCell
                                        HeaderTdSetting(tdHeader, "120px", "PRICE", "1")
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
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "120px", "", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 4
                                    For j = 0 To DsUniqueVendor.Tables(0).Rows.Count - 1
                                        tdHeader = New TableCell
                                        DvPriceData.RowFilter = "VENDORID=" + DsUniqueVendor.Tables(0).Rows(j).Item("VENDORID").ToString()
                                        DtPriceData = DvPriceData.ToTable()
                                        Try
                                            Header2TdSetting(tdHeader, "120px", "" + DtPriceData.Rows(0).Item("VENDORNM").ToString() + "", "1")
                                        Catch ex As Exception
                                            Header2TdSetting(tdHeader, "120px", "Not Found", "1")
                                        End Try
                                        trHeader.Controls.Add(tdHeader)
                                    Next
                            End Select
                        ElseIf k = 3 Then
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
                                    HeaderTdSetting(tdHeader, "120px", "(US$/M)", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 4
                                    For j = 0 To DsUniqueVendor.Tables(0).Rows.Count - 1
                                        tdHeader = New TableCell
                                        Header2TdSetting(tdHeader, "120px", "(US$/M)", "1")
                                        trHeader.Controls.Add(tdHeader)
                                    Next
                            End Select
                        End If
                    Next
                    trHeader.Height = 30
                    tblPA.Controls.Add(trHeader)
                Next

                'For Data
                For i = 0 To DsSKUByRfpID.Tables(0).Rows.Count - 1 'For Number of terms
                    trInner = New TableRow
                    For j = 0 To 3
                        Select Case j
                            Case 0
                                tdInner = New TableCell
                                tdInner.Text = DsSKUByRfpID.Tables(0).Rows(i).Item("SKUID").ToString()
                                InnerTdSetting(tdInner, "50px", "")
                                trInner.Controls.Add(tdInner)
                                trInner.Controls.Add(tdInner)
                            Case 1
                                tdInner = New TableCell
                                tdInner.Text = DsSKUByRfpID.Tables(0).Rows(i).Item("SKUDES").ToString()
                                InnerTdSetting(tdInner, "350px", "")
                                trInner.Controls.Add(tdInner)
                            Case 2
                                tdInner = New TableCell
                                tdInner.Text = "12000"
                                InnerTdSetting(tdInner, "120px", "")
                                trInner.Controls.Add(tdInner)
                            Case 3
                                For l = 0 To DsUniqueVendor.Tables(0).Rows.Count - 1 'for number of vendors
                                    tdInner = New TableCell
                                    DvPriceData.RowFilter = "VENDORID=" + DsPriceData.Tables(0).Rows(l).Item("VENDORID").ToString() + " AND SKUID=" + DsSKUByRfpID.Tables(0).Rows(i).Item("SKUID").ToString()
                                    DtPriceData = DvPriceData.ToTable()
                                    Try
                                        tdInner.Text = DtPriceData.Rows(0).Item("PRICE").ToString()
                                    Catch ex As Exception
                                        tdInner.Text = ""
                                    End Try
                                    InnerTdSetting(tdInner, "120px", "")
                                    trInner.Controls.Add(tdInner)
                                Next
                        End Select
                    Next
                    If (i Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    tblPA.Controls.Add(trInner)
                Next
                'End
            Else
                lblNoRecordPA.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetPricePageDetails:" + ex.Message.ToString()
        End Try
    End Sub

#End Region

#Region "Price & Cost"

    Protected Sub GetPriceCostPageDetails()
        Dim trHeader As New TableRow
        Dim tdHeader As TableCell
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim DsPriceData As New DataSet()
        Dim DtPriceData As New DataTable()
        Dim DvPriceData As New DataView()
        Dim DsSKUByRfpID As New DataSet()
        Try
            tblPC.Rows.Clear()
            DsPriceData = objGetdata.GetPriceCostByRfpID(hidRfpID.Value, Session("UserId"))
            DvPriceData = DsPriceData.Tables(0).DefaultView
            DsSKUByRfpID = objGetdata.GetSkuByRfpID_S(hidRfpID.Value, Session("UserId"))
            If DsPriceData.Tables(0).Rows.Count > 0 Then
                lblNoRecordPC.Visible = False
                For k = 1 To 2
                    trHeader = New TableRow
                    For i = 1 To 4
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
                                    HeaderTdSetting(tdHeader, "120px", "VENDOR", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 4
                                    For j = 0 To 15
                                        tdHeader = New TableCell
                                        HeaderTdSetting(tdHeader, "120px", "" + DsPriceData.Tables(0).Rows(0).Item("DES" + (j + 1).ToString()) + "", "1")
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
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "120px", "", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 4
                                    For j = 0 To 15
                                        tdHeader = New TableCell
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
                For i = 0 To DsPriceData.Tables(0).Rows.Count - 1 'For Number of terms                    
                    trInner = New TableRow
                    For j = 0 To 3
                        Select Case j
                            Case 0
                                tdInner = New TableCell
                                tdInner.Text = DsPriceData.Tables(0).Rows(i).Item("SKUID").ToString()
                                InnerTdSetting(tdInner, "50px", "")
                                trInner.Controls.Add(tdInner)
                                trInner.Controls.Add(tdInner)
                            Case 1
                                tdInner = New TableCell
                                tdInner.Text = DsPriceData.Tables(0).Rows(i).Item("SKUDES").ToString()
                                InnerTdSetting(tdInner, "220px", "")
                                trInner.Controls.Add(tdInner)
                            Case 2
                                tdInner = New TableCell
                                tdInner.Text = DsPriceData.Tables(0).Rows(i).Item("VENDORNM").ToString()
                                InnerTdSetting(tdInner, "120px", "")
                                trInner.Controls.Add(tdInner)
                            Case 3
                                For l = 0 To 15 'for number of vendors
                                    tdInner = New TableCell
                                    If l = 0 Then
                                        Try
                                            tdInner.Text = DsPriceData.Tables(0).Rows(i).Item("PRICE").ToString()
                                        Catch ex As Exception
                                            tdInner.Text = ""
                                        End Try
                                    ElseIf l = 1 Then
                                        Try
                                            tdInner.Text = DsPriceData.Tables(0).Rows(i).Item("SETUP").ToString()
                                        Catch ex As Exception
                                            tdInner.Text = ""
                                        End Try
                                    Else
                                        Try
                                            tdInner.Text = DsPriceData.Tables(0).Rows(i).Item("M" + (l - 1).ToString()).ToString()
                                        Catch ex As Exception
                                            tdInner.Text = ""
                                        End Try
                                    End If
                                    InnerTdSetting(tdInner, "120px", "")
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
                lblNoRecordPC.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetPriceCostPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

#End Region

#Region "Price Options"

    Protected Sub GetPriceOptnPageDetails()
        Dim trHeader As New TableRow
        Dim tdHeader As TableCell
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim DsPriceCols As New DataSet()
        Dim DsVendors As New DataSet()
        Dim DsPriceType As New DataSet()
        Dim DsUniqGrp As New DataSet()
        Dim DsPriceDataPO As New DataSet()
        Dim DvPriceDataPO As New DataView()
        Dim DtPriceDataPO As New DataTable()
        Dim DsM As New DataSet()
        Dim SkuOnly As Boolean = False
        Dim ColNum As Integer = 2
        Try
            tblPO.Rows.Clear()
            If hidPriceOpID.Value = "" Or hidPriceOpID.Value = "0" Then
                lblNoPriceOp.Visible = True
            Else
                DsM = objGetdata.GetMasterDataN(hidPriceOpID.Value)
                If DsM.Tables(0).Rows(0).Item("TBLNAME").ToString() = "SKUDETAIL" Then
                    btnPriceoptnSLV.Visible = False
                    SkuOnly = True
                Else
                    btnPriceoptnSLV.Visible = True
                    SkuOnly = False
                End If

                If SkuOnly Then
                    SetSkuLvlPriceOptn()
                Else
                    lblNoPriceOp.Visible = False
                    DsVendors = objGetdata.GetNoOfVendorByRFPIDPO(hidRfpID.Value, hidPriceOpID.Value)
                    DsPriceType = objGetdata.GetPriceTypeByRFPIDPO(hidRfpID.Value, hidPriceOpID.Value)
                    DsUniqGrp = objGetdata.GetUniqGrpPO(hidRfpID.Value, hidPriceOpID.Value)
                    DsPriceDataPO = objGetdata.GetPricesPO(hidRfpID.Value, hidPriceOpID.Value)

                    If DsVendors.Tables(0).Rows.Count > 0 And DsPriceType.Tables(0).Rows.Count > 0 And DsUniqGrp.Tables(0).Rows.Count > 0 And DsPriceDataPO.Tables(0).Rows.Count > 0 Then
                        lblNoRecordPOA.Visible = False

                        DvPriceDataPO = DsPriceDataPO.Tables(0).DefaultView()

                        VendorDes.Clear()
                        PricetypeDesp.Clear()

                        ColNum = ColNum + DsVendors.Tables(0).Rows.Count
                        VendorCnt = DsVendors.Tables(0).Rows.Count
                        For p = 0 To DsVendors.Tables(0).Rows.Count - 1
                            VendorDes.Add(DsVendors.Tables(0).Rows(p).Item("VENDORNM").ToString())
                        Next

                        hidGrpCnt.Value = DsUniqGrp.Tables(0).Rows.Count

                        PriceTypeCnt = DsPriceType.Tables(0).Rows.Count
                        hidPricetypeCnt.Value = DsPriceType.Tables(0).Rows.Count - 1

                        For q = 0 To DsPriceType.Tables(0).Rows.Count - 1
                            PricetypeDesp.Add(DsPriceType.Tables(0).Rows(q).Item("DESCRIPTION").ToString())
                        Next

                        trHeader = New TableRow
                        For i = 1 To 4
                            Select Case i
                                Case 1
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "50px", "GROUP", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 2
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "220px", "PRICE TYPE", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 3
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "80px", "UNIT", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 4
                                    For j = 0 To DsVendors.Tables(0).Rows.Count - 1
                                        tdHeader = New TableCell
                                        HeaderTdSetting(tdHeader, "120px", "" + DsVendors.Tables(0).Rows(j).Item("VENDORNM").ToString() + "", "1")
                                        trHeader.Controls.Add(tdHeader)
                                    Next
                            End Select
                        Next
                        trHeader.Height = 30
                        tblPO.Controls.Add(trHeader)

                        For i = 0 To DsUniqGrp.Tables(0).Rows.Count - 1
                            trInner = New TableRow
                            For m = 0 To ColNum
                                tdInner = New TableCell
                                If m = 0 Then
                                    tdInner.Text = DsUniqGrp.Tables(0).Rows(i).Item("CODE").ToString()
                                Else
                                    tdInner.Text = ""
                                End If
                                'tdInner.ColumnSpan = "7"
                                InnerTdSetting(tdInner, "50px", "")
                                trInner.Controls.Add(tdInner)
                            Next
                            trInner.CssClass = "AlterNateColor1"
                            tblPO.Controls.Add(trInner)

                            For j = 0 To DsPriceType.Tables(0).Rows.Count - 1
                                trInner = New TableRow
                                trInner.ID = "PriceOptn_" + i.ToString() + "_" + j.ToString() + ""

                                tdInner = New TableCell
                                tdInner.Text = ""
                                InnerTdSetting(tdInner, "50px", "")
                                trInner.Controls.Add(tdInner)

                                tdInner = New TableCell
                                tdInner.Text = DsPriceType.Tables(0).Rows(j).Item("DESCRIPTION").ToString()
                                InnerTdSetting(tdInner, "220px", "")
                                trInner.Controls.Add(tdInner)

                                tdInner = New TableCell
                                ' tdInner.Text = "(US$/M)"
                                tdInner.Text = "(Euro/kg)"
                                InnerTdSetting(tdInner, "80px", "")
                                trInner.Controls.Add(tdInner)

                                For k = 0 To DsVendors.Tables(0).Rows.Count - 1
                                    DvPriceDataPO.RowFilter = "CODE='" + DsUniqGrp.Tables(0).Rows(i).Item("CODE") + "' AND PRICEREQID=" + DsPriceType.Tables(0).Rows(j).Item("PRICEREQID").ToString() + " AND VENDORID=" + DsVendors.Tables(0).Rows(k).Item("VENDORID").ToString() + ""
                                    DtPriceDataPO = DvPriceDataPO.ToTable()

                                    tdInner = New TableCell
                                    Try
                                        tdInner.Text = DtPriceDataPO.Rows(0).Item("PRICE").ToString()
                                    Catch ex As Exception
                                        tdInner.Text = "0"
                                    End Try
                                    InnerTdSetting(tdInner, "120px", "")
                                    trInner.Controls.Add(tdInner)
                                Next

                                trInner.CssClass = "AlterNateColor2"
                                tblPO.Controls.Add(trInner)
                            Next
                        Next
                    Else
                        lblNoRecordPOA.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetPriceOptnPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnPriceoptnSLV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPriceoptnSLV.Click
        Try
            If hidPriceOpID.Value = "" Or hidPriceOpID.Value = "0" Then
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "AlertMasterGrp", "alert('Please Select Price Option.');", True)
            Else
                SetSkuLvlPriceOptn()
            End If
        Catch ex As Exception
            lblError.Text = "Error: btnPriceoptnSLV_Click() " + ex.Message()
        End Try
    End Sub

    Protected Sub SetSkuLvlPriceOptn()
        Dim trHeader As New TableRow
        Dim tdHeader As TableCell
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim DsPriceCols As New DataSet()
        Dim DsVendors As New DataSet()
        Dim DsPriceType As New DataSet()
        Dim DsUniqGrp As New DataSet()
        Dim DsPriceDataPO As New DataSet()
        Dim DvPriceDataPO As New DataView()
        Dim DtPriceDataPO As New DataTable()
        Dim ColNum As Integer = 2
        Try
            tblPO.Rows.Clear()
            If hidPriceOpID.Value = "" Or hidPriceOpID.Value = "0" Then
                lblNoPriceOp.Visible = True
            Else
                lblNoPriceOp.Visible = False
                DsVendors = objGetdata.GetNoOfVendorByRFPIDPO(hidRfpID.Value, hidPriceOpID.Value)
                DsPriceType = objGetdata.GetPriceTypeByRFPIDPO(hidRfpID.Value, hidPriceOpID.Value)
                DsUniqGrp = objGetdata.GetUniqGrpPOSL(hidPriceOpID.Value, Session("USERID"))
                DsPriceDataPO = objGetdata.GetPricesPOSL(hidPriceOpID.Value)

                If DsVendors.Tables(0).Rows.Count > 0 And DsPriceType.Tables(0).Rows.Count > 0 And DsUniqGrp.Tables(0).Rows.Count > 0 And DsPriceDataPO.Tables(0).Rows.Count > 0 Then
                    lblNoRecordPOA.Visible = False

                    DvPriceDataPO = DsPriceDataPO.Tables(0).DefaultView()

                    VendorDes.Clear()
                    PricetypeDesp.Clear()

                    ColNum = ColNum + DsVendors.Tables(0).Rows.Count
                    'VendorCnt = DsVendors.Tables(0).Rows.Count
                    'For p = 0 To DsVendors.Tables(0).Rows.Count - 1
                    '    VendorDes.Add(DsVendors.Tables(0).Rows(p).Item("VENDORNM").ToString())
                    'Next

                    'hidGrpCnt.Value = DsUniqGrp.Tables(0).Rows.Count

                    'PriceTypeCnt = DsPriceType.Tables(0).Rows.Count
                    'hidPricetypeCnt.Value = DsPriceType.Tables(0).Rows.Count - 1

                    'For q = 0 To DsPriceType.Tables(0).Rows.Count - 1
                    '    PricetypeDesp.Add(DsPriceType.Tables(0).Rows(q).Item("DESCRIPTION").ToString())
                    'Next

                    trHeader = New TableRow
                    For i = 1 To 4
                        Select Case i
                            Case 1
                                tdHeader = New TableCell
                                HeaderTdSetting(tdHeader, "50px", "SKU", "1")
                                trHeader.Controls.Add(tdHeader)
                            Case 2
                                tdHeader = New TableCell
                                HeaderTdSetting(tdHeader, "220px", "PRICE TYPE", "1")
                                trHeader.Controls.Add(tdHeader)
                            Case 3
                                tdHeader = New TableCell
                                HeaderTdSetting(tdHeader, "80px", "UNIT", "1")
                                trHeader.Controls.Add(tdHeader)
                            Case 4
                                For j = 0 To DsVendors.Tables(0).Rows.Count - 1
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "120px", "" + DsVendors.Tables(0).Rows(j).Item("VENDORNM").ToString() + "", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Next
                        End Select
                    Next
                    trHeader.Height = 30
                    tblPO.Controls.Add(trHeader)

                    For i = 0 To DsUniqGrp.Tables(0).Rows.Count - 1
                        trInner = New TableRow
                        For m = 0 To ColNum
                            tdInner = New TableCell
                            If m = 0 Then
                                tdInner.Text = DsUniqGrp.Tables(0).Rows(i).Item("DESCRIPTION").ToString()
                            Else
                                tdInner.Text = ""
                            End If
                            'tdInner.ColumnSpan = "7"
                            InnerTdSetting(tdInner, "50px", "")
                            trInner.Controls.Add(tdInner)
                        Next
                        trInner.CssClass = "AlterNateColor1"
                        tblPO.Controls.Add(trInner)

                        For j = 0 To DsPriceType.Tables(0).Rows.Count - 1
                            trInner = New TableRow
                            trInner.ID = "PriceOptn_" + i.ToString() + "_" + j.ToString() + ""

                            tdInner = New TableCell
                            tdInner.Text = ""
                            InnerTdSetting(tdInner, "50px", "")
                            trInner.Controls.Add(tdInner)

                            tdInner = New TableCell
                            tdInner.Text = DsPriceType.Tables(0).Rows(j).Item("DESCRIPTION").ToString()
                            InnerTdSetting(tdInner, "220px", "")
                            trInner.Controls.Add(tdInner)

                            tdInner = New TableCell
                            ' tdInner.Text = "(US$/M)"
                            tdInner.Text = "(Euro/kg)"
                            InnerTdSetting(tdInner, "80px", "")
                            trInner.Controls.Add(tdInner)

                            For k = 0 To DsVendors.Tables(0).Rows.Count - 1
                                DvPriceDataPO.RowFilter = "SKUID=" + DsUniqGrp.Tables(0).Rows(i).Item("SKUID").ToString() + " AND PRICEREQID=" + DsPriceType.Tables(0).Rows(j).Item("PRICEREQID").ToString() + " AND VENDORID=" + DsVendors.Tables(0).Rows(k).Item("VENDORID").ToString() + ""
                                DtPriceDataPO = DvPriceDataPO.ToTable()

                                tdInner = New TableCell
                                Try
                                    tdInner.Text = DtPriceDataPO.Rows(0).Item("PRICE").ToString()
                                Catch ex As Exception
                                    tdInner.Text = "0"
                                End Try
                                InnerTdSetting(tdInner, "120px", "")
                                trInner.Controls.Add(tdInner)
                            Next

                            trInner.CssClass = "AlterNateColor2"
                            tblPO.Controls.Add(trInner)
                        Next
                    Next
                Else
                    lblNoRecordPOA.Visible = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error:SetSkuLvlPriceOptn:" + ex.Message.ToString()
        End Try
    End Sub

#End Region

#Region "Structure"

    Protected Sub GetStructPageDetails()
        Dim trHeader As New TableRow
        Dim tdHeader As TableCell
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim DsPriceCols As New DataSet()
        Dim DsVendors As New DataSet()
        Dim DsStrType As New DataSet()
        Dim DsUniqGrp As New DataSet()
        ' Dim DsPriceDataPO As New DataSet()
        Dim DvStructData As New DataView()
        Dim DtStructData As New DataTable()
        Dim ColNum As Integer = 3
        Dim p As Integer
        Dim dsGrpD As New DataSet()
        Dim dsMat As New DataSet()
        Dim DvMatData As New DataView()
        Dim DtMatData As New DataTable()
        Dim DsChkMGrp As New DataSet()
        Dim IsSkuOnly As Boolean = True
        Try
            tblStructA.Rows.Clear()

            DsChkMGrp = objGetdata.CheckMGrpForStruct(hidRfpID.Value)
            If DsChkMGrp.Tables(0).Rows.Count > 0 Then
                Tr6.Visible = True
                IsSkuOnly = False
            Else
                Tr6.Visible = False
                IsSkuOnly = True
            End If

            If Not IsSkuOnly Then
                If hidMasterGrpID.Value = "" Or hidMasterGrpID.Value = "0" Then
                    lblNoStruct.Visible = True
                Else
                    lblNoStruct.Visible = False
                    DsVendors = objGetdata.GetNoOfVendorByRFP(hidRfpID.Value)
                    DsStrType = objGetdata.GetStructTypeByRFP()
                    dsGrpD = objGetdata.GetGrpStructData(hidRfpID.Value, hidMasterGrpID.Value)
                    DsUniqGrp = objGetdata.GetUniqGrpData(hidMasterGrpID.Value)

                    dsMat = objGetdata.GetStructMatDet()
                    'DsPriceDataPO = objGetdata.GetPricesPO(hidRfpID.Value, hidPriceOpID.Value)

                    If DsVendors.Tables(0).Rows.Count > 0 And DsStrType.Tables(0).Rows.Count > 0 And DsUniqGrp.Tables(0).Rows.Count > 0 Then
                        lblNoStruct.Visible = False
                        ColNum = ColNum + DsVendors.Tables(0).Rows.Count
                        trHeader = New TableRow
                        For i = 1 To 5
                            Select Case i
                                Case 1
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "120px", "GROUP", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 2
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "100px", "LAYER", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 3
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "120px", "TYPE", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 4
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "90px", "UNIT", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 5
                                    For j = 0 To DsVendors.Tables(0).Rows.Count - 1
                                        tdHeader = New TableCell
                                        HeaderTdSetting(tdHeader, "150px", "" + DsVendors.Tables(0).Rows(j).Item("VENDORNM").ToString() + "", "1")
                                        trHeader.Controls.Add(tdHeader)
                                    Next
                            End Select
                        Next
                        trHeader.Height = 30
                        tblStructA.Controls.Add(trHeader)

                        For i = 0 To DsUniqGrp.Tables(0).Rows.Count - 1
                            For p = 0 To 11
                                trInner = New TableRow
                                For m = 0 To ColNum
                                    tdInner = New TableCell
                                    If m = 0 Then
                                        If p = 0 Then
                                            tdInner.Text = DsUniqGrp.Tables(0).Rows(i).Item("DESCRIPTION").ToString()
                                        End If

                                    ElseIf m = 1 Then
                                        tdInner.Text = "Layer " + (p + 1).ToString()
                                    Else
                                        tdInner.Text = ""
                                    End If
                                    'tdInner.ColumnSpan = "7"
                                    InnerTdSetting(tdInner, "50px", "")
                                    trInner.Controls.Add(tdInner)
                                Next
                                trInner.CssClass = "AlterNateColor1"
                                tblStructA.Controls.Add(trInner)

                                For j = 0 To DsStrType.Tables(0).Rows.Count - 1
                                    trInner = New TableRow
                                    trInner.ID = "StructO_" + p.ToString() + i.ToString() + "_" + j.ToString() + ""

                                    tdInner = New TableCell
                                    tdInner.Text = ""
                                    InnerTdSetting(tdInner, "50px", "")
                                    trInner.Controls.Add(tdInner)

                                    tdInner = New TableCell
                                    tdInner.Text = ""
                                    InnerTdSetting(tdInner, "50px", "")
                                    trInner.Controls.Add(tdInner)

                                    tdInner = New TableCell
                                    tdInner.Text = DsStrType.Tables(0).Rows(j).Item("DESCRIPTION").ToString()
                                    InnerTdSetting(tdInner, "220px", "")
                                    trInner.Controls.Add(tdInner)



                                    tdInner = New TableCell
                                    If j = 1 Then
                                        tdInner.Text = "(micron)"
                                    ElseIf j = 2 Then
                                        tdInner.Text = "(%)"
                                    ElseIf j = 3 Then
                                        tdInner.Text = "(Euro/kg)"

                                    End If

                                    InnerTdSetting(tdInner, "80px", "")
                                    trInner.Controls.Add(tdInner)

                                    For k = 0 To DsVendors.Tables(0).Rows.Count - 1
                                        DvStructData = dsGrpD.Tables(0).DefaultView()
                                        DvStructData.RowFilter = "GROUPID='" + DsUniqGrp.Tables(0).Rows(i).Item("GROUPID").ToString() + "' AND VENDORID=" + DsVendors.Tables(0).Rows(k).Item("VENDORID").ToString() + ""
                                        DtStructData = DvStructData.ToTable()
                                        ' DvPriceDataPO.RowFilter = "CODE='" + DsUniqGrp.Tables(0).Rows(i).Item("CODE") + "' AND PRICEREQID=" + DsPriceType.Tables(0).Rows(j).Item("PRICEREQID").ToString() + " AND VENDORID=" + DsVendors.Tables(0).Rows(k).Item("VENDORID").ToString() + ""
                                        ' DtPriceDataPO = DvPriceDataPO.ToTable()

                                        tdInner = New TableCell
                                        Try
                                            If DtStructData.Rows.Count > 0 Then
                                                If j = 0 Then
                                                    If DtStructData.Rows(0).Item("S" + (p + 1).ToString()).ToString() <> "0" Then
                                                        DvMatData = dsMat.Tables(0).DefaultView
                                                        DvMatData.RowFilter = "MATID=" + DtStructData.Rows(0).Item("S" + (p + 1).ToString()).ToString() + " "
                                                        DtMatData = DvMatData.ToTable()
                                                        If DtMatData.Rows.Count > 0 Then
                                                            tdInner.Text = DtMatData.Rows(0).Item("MATDES").ToString()
                                                        Else
                                                            tdInner.Text = ""
                                                        End If
                                                    Else
                                                        tdInner.Text = ""
                                                    End If

                                                ElseIf j = 1 Then
                                                    tdInner.Text = DtStructData.Rows(0).Item("TH" + (p + 1).ToString())
                                                ElseIf j = 2 Then
                                                    tdInner.Text = DtStructData.Rows(0).Item("W" + (p + 1).ToString())
                                                ElseIf j = 3 Then
                                                    tdInner.Text = DtStructData.Rows(0).Item("P" + (p + 1).ToString())
                                                End If
                                            Else
                                                tdInner.Text = "0"
                                            End If

                                            ' DtPriceDataPO.Rows(0).Item("PRICE").ToString()
                                        Catch ex As Exception
                                            tdInner.Text = "0"
                                        End Try
                                        InnerTdSetting(tdInner, "120px", "")
                                        trInner.Controls.Add(tdInner)
                                    Next

                                    trInner.CssClass = "AlterNateColor2"
                                    tblStructA.Controls.Add(trInner)
                                Next

                            Next


                        Next
                    Else
                        lblNoStruct.Visible = True
                    End If
                End If
            Else
                GetStructSkuDet("ONLYSKU")
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetStructPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnCheckSkuDataStruct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCheckSkuDataStruct.Click
        Try
            If hidMasterGrpID.Value = "" Or hidMasterGrpID.Value = "0" Then
                ScriptManager.RegisterStartupScript(Page, Me.GetType(), "AlertMasterGrp", "alert('Please select master group');", True)
            Else
                GetStructSkuDet("GRPSKU")
            End If
        Catch ex As Exception
            lblError.Text = "Error: btnCheckSkuDataStruct_Click() " + ex.Message()
        End Try
    End Sub

    Protected Sub GetStructSkuDet(ByVal SType As String)
        Dim trHeader As New TableRow
        Dim tdHeader As TableCell
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim DsVendors As New DataSet()
        Dim DsStrType As New DataSet()
        Dim DsUniqGrp As New DataSet()
        Dim DvStructData As New DataView()
        Dim DtStructData As New DataTable()
        Dim ColNum As Integer = 3
        Dim p As Integer
        Dim dsGrpD As New DataSet()
        Dim dsMat As New DataSet()
        Dim DvMatData As New DataView()
        Dim DtMatData As New DataTable()
        Try
            tblStructA.Rows.Clear()
            lblNoStruct.Visible = False
            DsVendors = objGetdata.GetNoOfVendorByRFP(hidRfpID.Value)
            DsStrType = objGetdata.GetStructTypeByRFP()
            dsGrpD = objGetdata.GetSKUStructDataB(hidRfpID.Value, SType.ToString(), hidMasterGrpID.Value)
            DsUniqGrp = objGetdata.GetSKUByRfpID(hidRfpID.Value, Session("USERID"))
            dsMat = objGetdata.GetStructMatDet()

            If DsVendors.Tables(0).Rows.Count > 0 And DsStrType.Tables(0).Rows.Count > 0 And DsUniqGrp.Tables(0).Rows.Count > 0 Then
                lblNoStruct.Visible = False
                ColNum = ColNum + DsVendors.Tables(0).Rows.Count
                trHeader = New TableRow
                For i = 1 To 5
                    Select Case i
                        Case 1
                            tdHeader = New TableCell
                            HeaderTdSetting(tdHeader, "120px", "SKU", "1")
                            trHeader.Controls.Add(tdHeader)
                        Case 2
                            tdHeader = New TableCell
                            HeaderTdSetting(tdHeader, "100px", "LAYER", "1")
                            trHeader.Controls.Add(tdHeader)
                        Case 3
                            tdHeader = New TableCell
                            HeaderTdSetting(tdHeader, "120px", "TYPE", "1")
                            trHeader.Controls.Add(tdHeader)
                        Case 4
                            tdHeader = New TableCell
                            HeaderTdSetting(tdHeader, "90px", "UNIT", "1")
                            trHeader.Controls.Add(tdHeader)
                        Case 5
                            For j = 0 To DsVendors.Tables(0).Rows.Count - 1
                                tdHeader = New TableCell
                                HeaderTdSetting(tdHeader, "150px", "" + DsVendors.Tables(0).Rows(j).Item("VENDORNM").ToString() + "", "1")
                                trHeader.Controls.Add(tdHeader)
                            Next
                    End Select
                Next
                trHeader.Height = 30
                tblStructA.Controls.Add(trHeader)

                For i = 0 To DsUniqGrp.Tables(0).Rows.Count - 1
                    For p = 0 To 11
                        trInner = New TableRow
                        For m = 0 To ColNum
                            tdInner = New TableCell
                            If m = 0 Then
                                If p = 0 Then
                                    tdInner.Text = DsUniqGrp.Tables(0).Rows(i).Item("DESCRIPTION").ToString()
                                End If
                            ElseIf m = 1 Then
                                tdInner.Text = "Layer " + (p + 1).ToString()
                            Else
                                tdInner.Text = ""
                            End If
                            'tdInner.ColumnSpan = "7"
                            InnerTdSetting(tdInner, "50px", "")
                            trInner.Controls.Add(tdInner)
                        Next
                        trInner.CssClass = "AlterNateColor1"
                        tblStructA.Controls.Add(trInner)

                        For j = 0 To DsStrType.Tables(0).Rows.Count - 1
                            trInner = New TableRow
                            trInner.ID = "StructO_" + p.ToString() + i.ToString() + "_" + j.ToString() + ""

                            tdInner = New TableCell
                            tdInner.Text = ""
                            InnerTdSetting(tdInner, "50px", "")
                            trInner.Controls.Add(tdInner)

                            tdInner = New TableCell
                            tdInner.Text = ""
                            InnerTdSetting(tdInner, "50px", "")
                            trInner.Controls.Add(tdInner)

                            tdInner = New TableCell
                            tdInner.Text = DsStrType.Tables(0).Rows(j).Item("DESCRIPTION").ToString()
                            InnerTdSetting(tdInner, "220px", "")
                            trInner.Controls.Add(tdInner)



                            tdInner = New TableCell
                            If j = 1 Then
                                tdInner.Text = "(micron)"
                            ElseIf j = 2 Then
                                tdInner.Text = "(%)"
                            ElseIf j = 3 Then
                                tdInner.Text = "(Euro/kg)"
                            End If
                            InnerTdSetting(tdInner, "80px", "")
                            trInner.Controls.Add(tdInner)

                            For k = 0 To DsVendors.Tables(0).Rows.Count - 1
                                DvStructData = dsGrpD.Tables(0).DefaultView()
                                DvStructData.RowFilter = "SKUID='" + DsUniqGrp.Tables(0).Rows(i).Item("SKUID").ToString() + "' AND VENDORID=" + DsVendors.Tables(0).Rows(k).Item("VENDORID").ToString() + ""
                                DtStructData = DvStructData.ToTable()
                                tdInner = New TableCell
                                Try
                                    If DtStructData.Rows.Count > 0 Then
                                        If j = 0 Then
                                            If DtStructData.Rows(0).Item("S" + (p + 1).ToString()).ToString() <> "0" Then
                                                DvMatData = dsMat.Tables(0).DefaultView
                                                DvMatData.RowFilter = "MATID=" + DtStructData.Rows(0).Item("S" + (p + 1).ToString()).ToString() + " "
                                                DtMatData = DvMatData.ToTable()
                                                If DtMatData.Rows.Count > 0 Then
                                                    tdInner.Text = DtMatData.Rows(0).Item("MATDES").ToString()
                                                Else
                                                    tdInner.Text = "NA"
                                                End If
                                            Else
                                                tdInner.Text = "NA"
                                            End If
                                        ElseIf j = 1 Then
                                            tdInner.Text = DtStructData.Rows(0).Item("TH" + (p + 1).ToString())
                                        ElseIf j = 2 Then
                                            tdInner.Text = DtStructData.Rows(0).Item("W" + (p + 1).ToString())
                                        ElseIf j = 3 Then
                                            tdInner.Text = DtStructData.Rows(0).Item("P" + (p + 1).ToString())
                                        End If
                                    Else
                                        If j = 0 Then
                                            tdInner.Text = "NA"
                                        Else
                                            tdInner.Text = "0"
                                        End If
                                    End If
                                Catch ex As Exception
                                    If j = 0 Then
                                        tdInner.Text = "NA"
                                    Else
                                        tdInner.Text = "0"
                                    End If
                                End Try
                                InnerTdSetting(tdInner, "120px", "")
                                trInner.Controls.Add(tdInner)
                            Next
                            trInner.CssClass = "AlterNateColor2"
                            tblStructA.Controls.Add(trInner)
                        Next
                    Next
                Next
            Else
                lblNoStruct.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetStructSkuDet:" + ex.Message.ToString()
        End Try
    End Sub

#End Region

#Region "Batch Price"

    Protected Sub GetBatchPriceDetails()
        Dim trHeader As New TableRow
        Dim tdHeader As TableCell
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim DsPriceCols As New DataSet()
        Dim DsVendors As New DataSet()
        Dim DsPriceType As New DataSet()
        Dim DsUniqBatch As New DataSet()
        Dim DsPriceDataPO As New DataSet()
        Dim DvPriceDataPO As New DataView()
        Dim DtPriceDataPO As New DataTable()
        Dim DsRfpdet As New DataSet()
        Dim DvRfpdet As New DataView()
        Dim DtRfpdet As New DataTable()
        Dim DsAllGrpByPriceOpID As New DataSet()
        Dim div1 As HtmlGenericControl
        Try
            tblBatchPrice.Rows.Clear()
            If hidPriceOpIDBP.Value = "" Or hidPriceOpIDBP.Value = "0" Then
                lblNoPOBatch.Visible = True
            Else
                lblNoPOBatch.Visible = False
                DsVendors = objGetdata.GetNoOfVendorBatchByRFPIDPO(hidRfpID.Value, hidPriceOpIDBP.Value)
                'DsPriceType = objGetdata.GetBatchPriceTypeByRFPIDPO(hidRfpID.Value, hidPriceOpIDBP.Value)
                'DsUniqBatch = objGetdata.GetUniqBatchGrpPO(hidRfpID.Value, hidPriceOpIDBP.Value)
                'DsPriceDataPO = objGetdata.GetBatchPricesPO(hidRfpID.Value, hidPriceOpIDBP.Value, hidGroupId.Value)
                DsUniqBatch = objGetdata.GetBatchDetails()
                DsPriceDataPO = objGetdata.GetBatchPricesAllGrp(hidRfpID.Value, hidPriceOpIDBP.Value, hidGroupId.Value)
                DsAllGrpByPriceOpID = objGetdata.GetGrpDETByBatchLine(hidPriceOpIDBP.Value, "", Session("UserId"), hidRfpID.Value)

                If DsAllGrpByPriceOpID.Tables(0).Rows.Count > 0 Then
                    If DsVendors.Tables(0).Rows.Count > 0 And DsUniqBatch.Tables(0).Rows.Count > 0 Then
                        lblNoRcrdBatch.Visible = False
                        DvPriceDataPO = DsPriceDataPO.Tables(0).DefaultView()

                        trHeader = New TableRow
                        For i = 1 To 3
                            Select Case i
                                Case 1
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "150px", "Group", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 2
                                    tdHeader = New TableCell
                                    HeaderTdSetting(tdHeader, "50px", "Batches", "1")
                                    trHeader.Controls.Add(tdHeader)
                                Case 3
                                    For j = 0 To DsVendors.Tables(0).Rows.Count - 1
                                        tdHeader = New TableCell
                                        HeaderTdSetting(tdHeader, "120px", "" + DsVendors.Tables(0).Rows(j).Item("VENDORNM").ToString() + "", "1")
                                        trHeader.Controls.Add(tdHeader)
                                    Next
                            End Select
                        Next
                        trHeader.Height = 30
                        tblBatchPrice.Controls.Add(trHeader)

                        For P = 0 To DsAllGrpByPriceOpID.Tables(0).Rows.Count - 1

                            trInner = New TableRow
                            For m = 0 To (DsVendors.Tables(0).Rows.Count - 1) + 2
                                tdInner = New TableCell
                                If m = 0 Then
                                    tdInner.Text = DsAllGrpByPriceOpID.Tables(0).Rows(P).Item("CODE").ToString()
                                Else
                                    tdInner.Text = ""
                                End If
                                InnerTdSetting(tdInner, "150px", "")
                                trInner.Controls.Add(tdInner)
                            Next

                            'Test
                            'div1 = New HtmlGenericControl
                            'tdInner = New TableCell
                            'tdInner.ColumnSpan = 8
                            'tdInner.RowSpan = 8
                            'div1.ID = "div1" + P.ToString()
                            'InnerTdSetting(tdInner, "", "")
                            'tdInner.Controls.Add(div1)
                            'trInner.Controls.Add(tdInner)
                            'GenerateGraphLineBatch(DsAllGrpByPriceOpID.Tables(0).Rows(P).Item("OTHERPREFRFPID").ToString(), DsAllGrpByPriceOpID.Tables(0).Rows(P).Item("CODE").ToString(), div1)
                            'End

                            trInner.CssClass = "AlterNateColor3"
                            tblBatchPrice.Controls.Add(trInner)

                            For i = 0 To DsUniqBatch.Tables(0).Rows.Count - 1
                                trInner = New TableRow

                                tdInner = New TableCell
                                tdInner.Text = ""
                                InnerTdSetting(tdInner, "150px", "")
                                trInner.Controls.Add(tdInner)

                                tdInner = New TableCell
                                tdInner.Text = DsUniqBatch.Tables(0).Rows(i).Item("BATCHVALUE").ToString()
                                InnerTdSetting(tdInner, "50px", "")
                                trInner.Controls.Add(tdInner)
                                tblBatchPrice.Controls.Add(trInner)

                                'Price option Prices By Vendor
                                For k = 0 To DsVendors.Tables(0).Rows.Count - 1
                                    DvPriceDataPO.RowFilter = "BATCHID=" + DsUniqBatch.Tables(0).Rows(i).Item("BATCHID").ToString() + " AND VENDORID=" + DsVendors.Tables(0).Rows(k).Item("VENDORID").ToString() + " AND OTHERPREFRFPID=" + DsAllGrpByPriceOpID.Tables(0).Rows(P).Item("OTHERPREFRFPID").ToString() + ""
                                    DtPriceDataPO = DvPriceDataPO.ToTable()

                                    tdInner = New TableCell
                                    If DtPriceDataPO.Rows.Count > 0 Then
                                        Try
                                            tdInner.Text = DtPriceDataPO.Rows(0).Item("PRICE").ToString()
                                        Catch ex As Exception
                                            tdInner.Text = "0"
                                        End Try
                                    Else
                                        tdInner.Text = "NA"
                                    End If
                                    InnerTdSetting(tdInner, "120px", "")
                                    trInner.Controls.Add(tdInner)
                                Next
                                If (i Mod 2 = 0) Then
                                    trInner.CssClass = "AlterNateColor1"
                                Else
                                    trInner.CssClass = "AlterNateColor2"
                                End If
                                tblBatchPrice.Controls.Add(trInner)
                            Next
                        Next
                    Else
                        lblNoRcrdBatch.Visible = True
                    End If
                Else
                    lblNoRcrdBatch.Visible = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetBatchPriceDetails:" + ex.Message.ToString()
        End Try

    End Sub

    Protected Sub GetBatchPriceChart()
        Try
            loading.Style.Add("display", "inline")
            '   ifrDownloadPage.Attributes.Add("src", "http://192.168.3.5/Pages/SavvyPackPro/PopUp/AllBatchChart.aspx?RfpPriceID=" + hidPriceOpIDBP.Value)
            'ifrDownloadPage.Attributes.Add("src", "http://localhost:5062/www.savvypack.com_RaxaStudio/Pages/SavvyPackPro/PopUp/AllBatchChart.aspx?RfpPriceID=" + hidPriceOpIDBP.Value)
        Catch ex As Exception
            lblError.Text = "Error:GetBatchPriceChart:" + ex.Message.ToString()
        End Try

    End Sub

#End Region

#Region "Sumup"

    Protected Sub BindPRICETable()
        'Dim ds As New DataSet
        'Dim DsNoOfMasterGrp As New DataSet
        'Dim i As New Integer
        'Dim j As New Integer
        'Dim DWidth As String = String.Empty
        'Dim trHeader As New TableRow
        'Dim trHeader1 As New TableRow
        'Dim trHeader2 As New TableRow
        'Dim trInner As New TableRow
        'Dim tdHeader As TableCell
        'Dim tdHeader1 As TableCell
        'Dim tdHeader2 As TableCell
        'Dim lbl As New Label
        'Dim hid As New HiddenField
        'Dim Link As New HyperLink
        'Dim txt As New TextBox
        'Dim tdInner As TableCell
        'Dim objGetData As New SavvyProGetData()
        ''Dim RFPCol(txtRw.Text) As String
        'Dim DsVendors As New DataSet()
        'Try
        '    tblSumup.Rows.Clear()
        '    ds = objGetData.GetSumUpData("515", Session("USERID"))
        '    DsNoOfMasterGrp = objGetData.GetMasterGrpByPriceID("515")
        '    DsVendors = objGetData.GetNoOfVendorByRFP("16")

        '    trInner = New TableRow
        '    For i = 0 To 3
        '        If i = 0 Then
        '            For j = 0 To DsNoOfMasterGrp.Tables(0).Rows.Count - 1 'Master Groups
        '                tdHeader = New TableCell
        '                HeaderTdSetting(tdHeader, "50px", "" + DsNoOfMasterGrp.Tables(0).Rows(j).Item("DESCRIPTION").ToString() + "", "1")
        '                trHeader.Controls.Add(tdHeader)
        '            Next
        '        ElseIf i = 1 Then
        '            tdHeader = New TableCell
        '            HeaderTdSetting(tdHeader, "50px", "Qty Ordered", "1")
        '            trHeader.Controls.Add(tdHeader)
        '        ElseIf i = 2 Then
        '            tdHeader = New TableCell
        '            HeaderTdSetting(tdHeader, "50px", "Volume", "1")
        '            trHeader.Controls.Add(tdHeader)
        '        ElseIf i = 3 Then
        '            For p = 0 To DsVendors.Tables(0).Rows.Count - 1
        '                tdHeader = New TableCell
        '                HeaderTdSetting(tdHeader, "50px", "" + DsVendors.Tables(0).Rows(p).Item("VENDORNM").ToString() + "", "1")
        '                trHeader.Controls.Add(tdHeader)
        '            Next
        '        End If
        '    Next
        '    tblSumup.Controls.Add(trHeader)


        '    For k = 0 To ds.Tables(0).Rows.Count - 1
        '        trInner = New TableRow
        '        Select Case k
        '            Case 0
        '                For l = 0 To DsNoOfMasterGrp.Tables(0).Rows.Count - 1 'Master Groups
        '                    tdInner = New TableCell
        '                    tdInner.Text = ds.Tables(0).Rows(k).Item("" + DsNoOfMasterGrp.Tables(0).Rows(l).Item("DESCRIPTION").ToString() + "").ToString()
        '                    InnerTdSetting(tdInner, "50px", "")
        '                    trInner.Controls.Add(tdInner)
        '                    trInner.Controls.Add(tdInner)
        '                Next

        '                tdInner = New TableCell
        '                tdInner.Text = ds.Tables(0).Rows(k).Item("SUMOFQTY").ToString()
        '                InnerTdSetting(tdInner, "50px", "")
        '                trInner.Controls.Add(tdInner)
        '                trInner.Controls.Add(tdInner)

        '                tdInner = New TableCell
        '                tdInner.Text = ds.Tables(0).Rows(k).Item("VOLUME").ToString()
        '                InnerTdSetting(tdInner, "50px", "")
        '                trInner.Controls.Add(tdInner)
        '                trInner.Controls.Add(tdInner)

        '                For q = 0 To DsVendors.Tables(0).Rows.Count - 1
        '                    tdInner = New TableCell
        '                    tdInner.Text = "Calculation" + (q + 1).ToString()
        '                    InnerTdSetting(tdInner, "50px", "")
        '                    trInner.Controls.Add(tdInner)
        '                    trInner.Controls.Add(tdInner)
        '                Next
        '        End Select
        '        If (k Mod 2 = 0) Then
        '            trInner.CssClass = "AlterNateColor1"
        '        Else
        '            trInner.CssClass = "AlterNateColor2"
        '        End If
        '        tblSumup.Controls.Add(trInner)
        '    Next

        'Catch ex As Exception
        '    '_lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        'End Try
    End Sub

#End Region

#Region "Text Formatting"

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
            Td.Height = 30
        Catch ex As Exception
            lblError.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

#End Region

    Protected Sub btnHidRFPCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHidRFPCreate.Click
        Try
            If hidRfpID.Value <> "" Or hidRfpID.Value <> "0" Then
                lnkSelRFP.Text = hidRfpNm.Value
                'Changes included by Pk
                hidPriceOpID.Value = ""
                hidMasterGrpID.Value = ""
                hidPriceOpIDBP.Value = ""
                hidGroupId.Value = ""
                'End
                GetRfpDetails(hidRfpID.Value)
                loadTab()

            End If
        Catch ex As Exception
            lblError.Text = "Error: btnHidRFPCreate_Click() " + ex.Message()
        End Try
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        Try
            If hidPriceOpID.Value <> "" Or hidPriceOpID.Value <> "0" Then
                lnkSelRFPPO.Text = hidPriceOpNm.Value
                GetPriceOptnPageDetails()
            End If
        Catch ex As Exception
            lblError.Text = "Error: btnRefresh_Click() " + ex.Message()
        End Try
    End Sub

    Protected Sub btnMasterSel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMasterSel.Click
        Try
            If hidMasterGrpID.Value <> "" Then
                lnkSelMasterGrp.Text = hidMasterGrpDes.Value
                GetStructPageDetails()
            End If
        Catch ex As Exception
            lblError.Text = "Error: btnMasterSel_Click() " + ex.Message()
        End Try
    End Sub

    Protected Sub btnBatchPageRef_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatchPageRef.Click
        Try
            If hidPriceOpIDBP.Value <> "" And hidGroupId.Value <> "" Then
                lnkPriceOpBatch.Text = hidPriceOpNmBP.Value
                lnkGrpSelectnB.Text = hidGroupDes.Value
                GetCalculation()
                GetBatchPriceDetails()
            Else
                lnkPriceOpBatch.Text = hidPriceOpNmBP.Value
                GetCalculation()
                GetBatchPriceDetails()
                'GetBatchPriceChart()
            End If

        Catch ex As Exception
            lblError.Text = "Error: btnBatchPageRef_Click() " + ex.Message()
        End Try
    End Sub

    Protected Sub GetCalculation()
        Dim objGetData As New SavvyProGetData
        Dim objUpdata As New SavvyProUpInsData
        Dim dsColor As New DataSet
        Dim dvColor As New DataView()
        Dim dtColor As New DataTable()
        Dim dsFSPrice As New DataSet
        Dim dsSSPrice As New DataSet
        Dim dsROPrice As New DataSet
        Dim dvPrice1 As New DataView
        Dim dtPrice1 As New DataTable
        Dim dvPrice2 As New DataView
        Dim dtPrice2 As New DataTable
        Dim dvPrice3 As New DataView
        Dim dtPrice3 As New DataTable
        Dim setup1Price As Double = 0.0
        Dim setup2Price As Double = 0.0
        Dim RunOnPrice As Double = 0.0
        Dim Color As String = ""
        Dim i As Integer
        Dim DsUniqueVendor As New DataSet
        Dim j As Integer = 0
        Dim dsBatches As New DataSet
        Dim k As Integer = 0
        Dim setUpCost As Double = 0.0
        Dim runOnCost As Double = 0.0
        Dim SumOfCost As String = ""
        Dim DsQtyData As New DataSet()
        Dim DvQtyData As New DataView()
        Dim DtQtyData As New DataTable()
        Dim DsAllGrpByPriceOpID As New DataSet()
        Dim DsCalculatedPrice As New DataSet()
        Dim DvCalculatedPrice As New DataView()
        Dim DtCalculatedPrice As New DataTable()
        Dim DsAllPricesByPO As New DataSet()

        Dim dvPrice As New DataView
        Dim dtPrice As New DataTable()
        Dim Arr(20) As String
        Dim DsALL As New DataSet()
        Dim DsALL1 As New DataSet()
        Dim DsPrice As New DataSet()
        Dim DsPrice2 As New DataSet()
        Try
            DsAllGrpByPriceOpID = objGetData.GetGrpDETByBatchLine(hidPriceOpIDBP.Value, "", Session("UserId"), hidRfpID.Value)
            dsBatches = objGetData.GetBatchDetails()
            DsUniqueVendor = objGetData.GetNoOfVendorByRFP(hidRfpID.Value)

            dsColor = objGetData.GetColorDetailsAll(hidPriceOpIDBP.Value)
            DsAllPricesByPO = objGetData.GetAllPricesByPOID(hidPriceOpIDBP.Value)
            DsQtyData = objGetData.GetAllQtyDataByGrp(Session("UserId"), hidPriceOpIDBP.Value, hidRfpID.Value)
            DsCalculatedPrice = objGetData.GetBatchPricesAllGrpC(hidPriceOpIDBP.Value)

            DsALL = objGetData.GetALLData(hidPriceOpIDBP.Value) ' for Price requirement values 
            DsALL1 = objGetData.GetALL1Data(hidPriceOpIDBP.Value) ' all price req. values 


            If DsAllGrpByPriceOpID.Tables(0).Rows.Count > 0 Then
                For l = 0 To DsAllGrpByPriceOpID.Tables(0).Rows.Count - 1
                    If dsBatches.Tables(0).Rows.Count > 0 And DsUniqueVendor.Tables(0).Rows.Count > 0 Then
                        For i = 0 To DsUniqueVendor.Tables(0).Rows.Count - 1

                            If DsALL.Tables(0).Rows.Count > 0 Then
                                For k = 0 To DsALL.Tables(0).Rows.Count - 1 '

                                    dvPrice = DsALL1.Tables(0).DefaultView
                                    ' dvPrice.RowFilter = "VENDORID=" + DsUniqueVendor.Tables(0).Rows(i).Item("VENDORID").ToString() + "AND CODE='" + DsAllGrpByPriceOpID.Tables(0).Rows(k).Item("CODE").ToString() + "' AND PRICEREQID IN (1) "

                                    dvPrice.RowFilter = "PRICEREQID=" + DsALL.Tables(0).Rows(k).Item("COLVALUEID").ToString() + "AND VENDORID=" + DsUniqueVendor.Tables(0).Rows(i).Item("VENDORID").ToString()

                                    dtPrice = dvPrice.ToTable()


                                    ' DsPrice.Tables(0).TableName = (k + 1).ToString()

                                    ' DsPrice2.Tables.Add(DsPrice.Tables((k + 1).ToString()).Copy())
                                    'dvPrice1 = DsAllPricesByPO.Tables(0).DefaultView

                                    '' dvPrice1 = DsALL1.Tables(0).DefaultView

                                    'dvPrice1.RowFilter = "VENDORID=" + DsUniqueVendor.Tables(0).Rows(i).Item("VENDORID").ToString() + "AND CODE='" + DsAllGrpByPriceOpID.Tables(0).Rows(l).Item("CODE").ToString() + "' AND PRICEREQID IN (1) "
                                    'dtPrice1 = dvPrice1.ToTable()
                                    '''''
                                    If dtPrice.Rows.Count > 0 Then

                                        dvColor = dsColor.Tables(0).DefaultView
                                        dvColor.RowFilter = "CODE='" + DsAllGrpByPriceOpID.Tables(0).Rows(l).Item("CODE").ToString() + "'"
                                        dtColor = dvColor.ToTable()

                                        DvQtyData = DsQtyData.Tables(0).DefaultView
                                        ' DvQtyData.RowFilter = "CODE='" + DsAllGrpByPriceOpID.Tables(0).Rows(l).Item("CODE").ToString() + "'"
                                        DtQtyData = DvQtyData.ToTable()



                                        If dtPrice.Rows(0).Item("PRICEREQID") > 0 Then
                                            For j = 1 To dtPrice.Rows.Count
                                                setup1Price = setup1Price + FormatNumber(CDbl(dtPrice.Rows(j - 1).Item("PRICE").ToString()), 2)

                                            Next

                                        End If
                                    End If
                                Next '
                            End If
                        Next
                    End If
                Next
            End If


            'dvPrice1 = DsAllPricesByPO.Tables(0).DefaultView
            'dvPrice1.RowFilter = "VENDORID=" + DsUniqueVendor.Tables(0).Rows(i).Item("VENDORID").ToString() + "AND CODE='" + DsAllGrpByPriceOpID.Tables(0).Rows(l).Item("CODE").ToString() + "' AND PRICEREQID IN (1) "
            'dtPrice1 = dvPrice1.ToTable()

            'Next
            'dvPrice2 = DsAllPricesByPO.Tables(0).DefaultView
            'dvPrice2.RowFilter = "VENDORID=" + DsUniqueVendor.Tables(0).Rows(i).Item("VENDORID").ToString() + "AND CODE='" + DsAllGrpByPriceOpID.Tables(0).Rows(l).Item("CODE").ToString() + "' AND PRICEREQID=2"
            'dtPrice2 = dvPrice2.ToTable()


            'dvPrice3 = DsAllPricesByPO.Tables(0).DefaultView
            'dvPrice3.RowFilter = "VENDORID=" + DsUniqueVendor.Tables(0).Rows(i).Item("VENDORID").ToString() + "AND CODE='" + DsAllGrpByPriceOpID.Tables(0).Rows(l).Item("CODE").ToString() + "' AND PRICEREQID=3"
            'dtPrice3 = dvPrice3.ToTable()

            'If dtPrice1.Rows.Count > 0 And dtPrice2.Rows.Count > 0 And dtPrice3.Rows.Count > 0 Then

            'End If

            'If dtPrice1.Rows(0).Item("PRICE").ToString() <> "0" And dtPrice2.Rows(0).Item("PRICE").ToString() <> "0" And dtPrice3.Rows(0).Item("PRICE").ToString() <> "0" Then

            '    If dtPrice1.Rows.Count > 0 Then

            '        dvColor = dsColor.Tables(0).DefaultView
            '        dvColor.RowFilter = "CODE='" + DsAllGrpByPriceOpID.Tables(0).Rows(l).Item("CODE").ToString() + "'"
            '        dtColor = dvColor.ToTable()

            '        DvQtyData = DsQtyData.Tables(0).DefaultView
            '        ' DvQtyData.RowFilter = "CODE='" + DsAllGrpByPriceOpID.Tables(0).Rows(l).Item("CODE").ToString() + "'"
            '        DtQtyData = DvQtyData.ToTable()

            '        For j = 0 To dsBatches.Tables(0).Rows.Count - 1

            '            DvCalculatedPrice = DsCalculatedPrice.Tables(0).DefaultView
            '            DvCalculatedPrice.RowFilter = "VENDORID=" + DsUniqueVendor.Tables(0).Rows(i).Item("VENDORID").ToString() + "AND CODE='" + DsAllGrpByPriceOpID.Tables(0).Rows(l).Item("CODE").ToString() + "' AND BATCHID=" + dsBatches.Tables(0).Rows(j).Item("BATCHID").ToString() + ""
            '            DtCalculatedPrice = DvCalculatedPrice.ToTable()

            '            If DtCalculatedPrice.Rows.Count > 0 Then
            '            Else
            '                Try
            '                    If dtColor.Rows.Count > 0 Then
            '                        Color = dtColor.Rows(0).Item("VAL").ToString()
            '                    End If

            '                    If dtPrice1.Rows.Count > 0 Then
            '                        For k = 1 To dtPrice1.Rows.Count
            '                            setup1Price = setup1Price + FormatNumber(CDbl(dtPrice1.Rows(k - 1).Item("PRICE").ToString()), 2)
            '                        Next
            '                    End If

            '                    If dtPrice2.Rows.Count > 0 Then
            '                        For k = 1 To dtPrice2.Rows.Count
            '                            setup2Price = setup2Price + FormatNumber(CDbl(dtPrice2.Rows(k - 1).Item("PRICE").ToString()), 2)
            '                        Next
            '                    End If
            '                    setup2Price = setup2Price * Color * (DtQtyData.Rows(0).Item("AVGSKU").ToString() - 1)

            '                    setUpCost = (setup1Price + setup2Price) / (DtQtyData.Rows(0).Item("AVGSKU").ToString() * dsBatches.Tables(0).Rows(j).Item("BATCHVALUE").ToString())

            '                    If dtPrice3.Rows.Count > 0 Then
            '                        For k = 1 To dtPrice3.Rows.Count
            '                            runOnCost = runOnCost + FormatNumber(CDbl(dtPrice3.Rows(k - 1).Item("PRICE").ToString()), 2)
            '                        Next
            '                        runOnCost = runOnCost / 1000
            '                    End If

            '                    SumOfCost = FormatNumber(CDbl(setUpCost + runOnCost), 2)
            '                Catch ex As Exception
            '                    SumOfCost = "0"
            '                End Try

            '                objUpdata.InsUpdateBatchDetails(hidPriceOpIDBP.Value, dsBatches.Tables(0).Rows(j).Item("BATCHID").ToString(), _
            '                                                DsUniqueVendor.Tables(0).Rows(i).Item("VENDORID").ToString(), DsAllGrpByPriceOpID.Tables(0).Rows(l).Item("CODE").ToString(), SumOfCost, _
            '                                                hidRfpID.Value, "2")
            '                setup1Price = 0.0
            '                setup2Price = 0.0
            '                RunOnPrice = 0.0
            '                setUpCost = 0.0
            '                runOnCost = 0.0
            '            End If
            '        Next
            '    End If

            'End If
            ''    Next
            ''End If
            ''    Next
            ''End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetCalculation_old()
        Dim objGetData As New SavvyProGetData
        Dim objUpdata As New SavvyProUpInsData
        Dim dsColor As New DataSet
        Dim dsFSPrice As New DataSet
        Dim dsSSPrice As New DataSet
        Dim dsROPrice As New DataSet
        Dim dvPrice1 As New DataView
        Dim dtPrice1 As New DataTable
        Dim dvPrice2 As New DataView
        Dim dtPrice2 As New DataTable
        Dim dvPrice3 As New DataView
        Dim dtPrice3 As New DataTable
        Dim setup1Price As Double = 0.0
        Dim setup2Price As Double = 0.0
        Dim RunOnPrice As Double = 0.0
        Dim Color As String = ""
        Dim i As Integer
        Dim DsUniqueVendor As New DataSet
        Dim j As Integer = 0
        Dim dsBatches As New DataSet
        Dim k As Integer = 0
        Dim setUpCost As Double = 0.0
        Dim runOnCost As Double = 0.0
        Dim SumOfCost As String = ""
        Try

            DsUniqueVendor = objGetData.GetNoOfVendorByRFP(hidRfpID.Value)
            dsColor = objGetData.GetColorDetails(hidPriceOpIDBP.Value, hidGroupDes.Value)
            dsFSPrice = objGetData.GetFirstSetupPrice(hidPriceOpIDBP.Value, hidGroupDes.Value)
            dsSSPrice = objGetData.GetSecondSetupPrice(hidPriceOpIDBP.Value, hidGroupDes.Value)
            dsROPrice = objGetData.GetRunOnCost(hidPriceOpIDBP.Value, hidGroupDes.Value)
            dsBatches = objGetData.GetBatchDetails()

            If dsBatches.Tables(0).Rows.Count > 0 Then
                For k = 0 To dsBatches.Tables(0).Rows.Count - 1
                    If DsUniqueVendor.Tables(0).Rows.Count > 0 Then
                        For j = 0 To DsUniqueVendor.Tables(0).Rows.Count - 1

                            If dsColor.Tables(0).Rows.Count > 0 Then
                                Color = dsColor.Tables(0).Rows(0).Item("VAL").ToString()
                            End If


                            dvPrice1 = dsFSPrice.Tables(0).DefaultView
                            dvPrice1.RowFilter = "VENDORID=" + DsUniqueVendor.Tables(0).Rows(j).Item("VENDORID").ToString()
                            dtPrice1 = dvPrice1.ToTable()


                            dvPrice2 = dsSSPrice.Tables(0).DefaultView
                            dvPrice2.RowFilter = "VENDORID=" + DsUniqueVendor.Tables(0).Rows(j).Item("VENDORID").ToString()
                            dtPrice2 = dvPrice2.ToTable()


                            dvPrice3 = dsROPrice.Tables(0).DefaultView
                            dvPrice3.RowFilter = "VENDORID=" + DsUniqueVendor.Tables(0).Rows(j).Item("VENDORID").ToString()
                            dtPrice3 = dvPrice3.ToTable()

                            If dtPrice1.Rows.Count > 0 Then
                                For i = 1 To dtPrice1.Rows.Count
                                    setup1Price = setup1Price + dtPrice1.Rows(i - 1).Item("PRICE").ToString()
                                Next
                            End If

                            If dtPrice2.Rows.Count > 0 Then
                                For i = 1 To dtPrice2.Rows.Count
                                    setup2Price = setup2Price + dtPrice2.Rows(i - 1).Item("PRICE").ToString()
                                Next
                            End If
                            setup2Price = setup2Price * Color * (3 - 1)

                            setUpCost = (setup1Price + setup2Price) / (3 * dsBatches.Tables(0).Rows(k).Item("BATCHVALUE").ToString())

                            If dtPrice3.Rows.Count > 0 Then
                                For i = 1 To dtPrice3.Rows.Count
                                    runOnCost = runOnCost + dtPrice3.Rows(i - 1).Item("PRICE").ToString()
                                Next
                                runOnCost = runOnCost / 1000
                            End If

                            SumOfCost = FormatNumber(CDbl(setUpCost + runOnCost), 2)

                            objUpdata.InsUpdateBatchDetails(hidPriceOpIDBP.Value, dsBatches.Tables(0).Rows(k).Item("BATCHID").ToString(), _
                                                            DsUniqueVendor.Tables(0).Rows(j).Item("VENDORID").ToString(), hidGroupDes.Value, SumOfCost, _
                                                            hidRfpID.Value, "2")
                            setup1Price = 0.0
                            setup2Price = 0.0
                            RunOnPrice = 0.0
                            setUpCost = 0.0
                            runOnCost = 0.0
                        Next
                    End If
                Next
            End If
        Catch ex As Exception

        End Try

    End Sub

End Class