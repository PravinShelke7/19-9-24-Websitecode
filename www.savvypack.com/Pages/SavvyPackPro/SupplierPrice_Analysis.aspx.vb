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
Partial Class Pages_SavvyPackPro_SupplierPrice_Analysis
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData
    Dim _iRFPId As String

    Public Property RFPIDD() As String
        Get
            Return _iRFPId
        End Get
        Set(ByVal Value As String)
            _iRFPId = Value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim DsM As New DataSet()
        Try

            'lblFooter.Text = "Copyright 1997 - " + Now.Year().ToString() + " SavvyPack Corporation"
            If Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If
            If Session("USERID") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                ChkExistingRfp()
                hidPriceID.Value = Request.QueryString("PriceID")
                BindPRICETable()
                DsM = objGetdata.GetMasterDataN(hidPriceID.Value)
                If DsM.Tables(0).Rows(0).Item("TBLNAME").ToString() = "SKUDETAIL" Then
                    'btnExtrapSkuLvl.Visible = False
                End If
            End If
            ' GetPageDetails()
        Catch ex As Exception
            lblError.Text = "Error: Page_Load() " + ex.Message()
        End Try
    End Sub

    Protected Sub ChkExistingRfp()
        Dim DsCheckRfp As New DataSet()
        Try
            GetRfpDetails(Session("ShidRfpID"))
        Catch ex As Exception
            lblError.Text = "Error: ChkExistingRfp() " + ex.Message()
        End Try
    End Sub

    Protected Sub GetRfpDetails(ByVal RfpID As String)
        Dim DsRfpdet As New DataSet()
        Try
            'RFPIDD = 5
            RFPIDD = RfpID
            If RfpID <> "" Or RfpID <> "0" Then

                DsRfpdet = objGetdata.GetRFPbyID(RFPIDD)
                If DsRfpdet.Tables(0).Rows.Count > 0 Then
                    RfpDetail.Visible = True
                    lbSelRFP.Text = DsRfpdet.Tables(0).Rows(0).Item("DES1").ToString()
                    lblSelRfpID.Text = DsRfpdet.Tables(0).Rows(0).Item("ID").ToString()
                    lblSelRfpDes.Text = DsRfpdet.Tables(0).Rows(0).Item("DES1").ToString()
                    lblType.Text = DsRfpdet.Tables(0).Rows(0).Item("TYPEDESC").ToString()
                Else
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('System isn't able to find ID. Please try again.');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = "Error: GetRfpDetails() " + ex.Message()
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
            ' _lErrorLble.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub InnerTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal Align As String)
        Try

            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.Style.Add("text-align", Align)
            If Align = "Left" Then
                Td.Style.Add("padding-left", "15px")
            End If
            If Align = "Right" Then
                Td.Style.Add("padding-right", "15px")
            End If
        Catch ex As Exception
            '_lErrorLble.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
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

            'ds = objGetData.GetPRICEDATARFPN(hidPriceID.Value, Session("BUserID"))
            ds = objGetData.GetPRICEDATARFPN(hidPriceID.Value, Session("BUserID"), Session("ShidRfpID"))
            dsM = objGetData.GetMasterDataN(hidPriceID.Value)
            dsP = objGetData.GetRFPPRICE(hidPriceID.Value)
            DsGetColorByCode = objGetData.GetCodeByPriceID(hidPriceID.Value)
            DvGetColorByCode = DsGetColorByCode.Tables(0).DefaultView


            If dsM.Tables(0).Rows(0).Item("MTYPEID").ToString() <> "5" Then
                dsPRC = objGetData.GetPriceOP(hidPriceID.Value, Session("SVendorID"))
            Else
                dsPRC = objGetData.GetPriceOPSku(hidPriceID.Value, Session("SVendorID"))
                IsSku = True
            End If

            dscnt = objGetData.GetPriceOpCnttype(hidPriceID.Value)

            If dsP.Tables(0).Rows.Count > 0 Then
                lblPriceName.Text = dsP.Tables(0).Rows(0).Item("PRICEDETAIL").ToString()
            End If
            For i = 1 To dsM.Tables(0).Rows.Count + 1
                tdHeader = New TableCell
                Dim Title As String = String.Empty
                'Header
                If i = 1 Then
                    HeaderTdSetting(tdHeader, "50px", "", "1")
                    trHeader.Controls.Add(tdHeader)
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
                    tdHeader.Controls.Add(lbl)
                    trHeader.Controls.Add(tdHeader)
                End If
            Next
            tblPRICE.Controls.Add(trHeader)

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
                        tdInner = New TableCell
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.Text = "<b>" + i.ToString() + "</b>"
                        trInner.Controls.Add(tdInner)
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
                            lblprice.Width = 70
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
                            strcode.Text = "<b>" + ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 2).Item("COLMNS").ToString().Replace(" ", "")).ToString() + "</b>"

                            If strVal = "" Then
                                strVal = ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 2).Item("COLMNS").ToString().Replace(" ", "")).ToString()
                            Else
                                strVal = strVal + "#" + ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 2).Item("COLMNS").ToString().Replace(" ", "")).ToString()
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
                tblPRICE.Controls.Add(trInner)
            Next
        Catch ex As Exception
            '_lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

End Class
