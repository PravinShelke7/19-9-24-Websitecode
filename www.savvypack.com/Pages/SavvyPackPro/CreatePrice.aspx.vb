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
Partial Class Pages_SavvyPackPro_CreatePrice
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
            btnSAVE.Enabled = False
            If Not IsPostBack Then
                hidSortIdBSpec.Value = "0"
                hidSortIdBMVendor.Value = "0"
                ChkExistingRfp()
                Dim objUpInsdata As New SavvyProUpInsData
                objUpInsdata.DeleteRFPColumnsData(Session("hidRfpID"))
            End If
        Catch ex As Exception
            lblError.Text = "Error: Page_Load() " + ex.Message()
        End Try
    End Sub

    Protected Sub ChkExistingRfp()
        Try
            GetRfpDetails(Session("hidRfpID"))
        Catch ex As Exception
            lblError.Text = "Error: ChkExistingRfp() " + ex.Message()
        End Try
    End Sub

    Protected Sub GetRfpDetails(ByVal RfpID As String)
        Dim DsRfpdet As New DataSet()
        Try
            RFPIDD = RfpID
            If RfpID <> "" Or RfpID <> "0" Then

                DsRfpdet = objGetdata.GetRFPbyID(RfpID)
                Session("DsRfpdet") = DsRfpdet
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

    Protected Sub BindData()
        Dim ds As New DataSet
        Dim objGetData As New SavvyProGetData
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
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim RFPCol(txtRw.Text - 1) As String
        Try

            For i = 1 To txtRw.Text + 1
                tdHeader = New TableCell
                Dim Title As String = String.Empty
                'Header
                If i = 1 Then
                    HeaderTdSetting(tdHeader, "50px", "", "1")
                    trHeader.Controls.Add(tdHeader)
                Else
                    HeaderTdSetting(tdHeader, "110px", "", "1")
                    Link = New HyperLink
                    hid = New HiddenField
                    Link.ID = "hypColDes" + (i - 1).ToString()
                    hid.ID = "hidColid" + (i - 1).ToString()
                    Link.Width = 80
                    Link.CssClass = "LinkM"
                    GetColType(Link, hid, 0)                   
                    Link.Text = "column_" + (i - 1).ToString()
                    tdHeader.Controls.Add(hid)
                    tdHeader.Controls.Add(Link)
                    trHeader.Controls.Add(tdHeader)
                End If
            Next
            tblPRICE.Controls.Add(trHeader)
        Catch ex As Exception
            lblError.Text = "Error: BindData() " + ex.Message()
        End Try
    End Sub

    Protected Sub GetColType(ByRef LinkCOL As HyperLink, ByVal hid As HiddenField, ByVal ColId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try
            LinkCOL.Text = "Select Column"
            hid.Value = 0
            Path = "PopUp/GetCOLPopUp.aspx?Des=" + LinkCOL.ClientID + "&Id=" + hid.ClientID + ""
            LinkCOL.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"

        Catch ex As Exception

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

    Protected Sub GetRowLink(ByVal Seq As String, ByVal Link As HyperLink, ByVal RptId As String, ByVal hidId As String, ByVal hidValue As String)
        Dim Path As String
        Dim RowVal2D As String = String.Empty
        Dim RegionSetId As String = String.Empty
        Dim Curr As String = String.Empty
        Try
            Path = "../PopUp/RowSelector.aspx?RptId=" + RptId + "&Seq=" + Seq.ToString() + "&Id=" + Link.ClientID + "&hidId=" + hidId + "&isTemp=Y"
            Link.NavigateUrl = "javascript:ShowPopWindow('" + Path + "','" + hidId + "')"
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSumitt_Click(sender As Object, e As System.EventArgs) Handles btnSumitt.Click
        Try
            Dim ds As New DataSet
            Dim objGetData As New SavvyProGetData()
            If Session("ColN") = "" Then
                Session("ColN") = txtRw.Text
            End If
            If Session("hidRfpID") <> "" And Session("hidRfpID") <> "0" Then
                If Session("ColN") <> txtRw.Text Then
                    Dim objUpInsdata As New SavvyProUpInsData
                    objUpInsdata.DeleteRFPColumnsData(Session("PriceID"))
                    Session("ColN") = txtRw.Text
                    BindData()
                Else
                    Dim objUpInsdata As New SavvyProUpInsData

                    objUpInsdata.UpdatePriceTempDet(Session("PriceID").ToString(), txtPriceName.Text)

                    ds = objGetData.GetRFPColumns(Session("PriceID"))

                    If ds.Tables(0).Rows.Count > 0 Then
                        ' BindPRICETable()
                        If ds.Tables(0).Rows.Count = Session("ColN") Then
                            BindPRICETable()
                            btnSAVE.Enabled = True
                        Else
                            BindDataBlank()
                            btnSAVE.Enabled = False
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert1", "alert('Please select all columns.')", True)

                        End If

                    Else
                    BindData()
                End If
                    End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub BindDataBlank()
        Dim ds As New DataSet
        Dim objGetData As New SavvyProGetData
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
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim dsM As New DataSet
        Dim count As String
        Dim RFPCol(txtRw.Text - 1) As String
        Try
            dsM = objGetData.GetMasterData(Session("PRICEID").ToString())
            Count = dsM.Tables(0).Rows.Count
            If dsM.Tables(0).Rows.Count <> Session("ColN") Then
                Count = Count + (Int(txtRw.Text) - dsM.Tables(0).Rows.Count)
            End If
            For i = 1 To Count + 1 'dsM.Tables(0).Rows.Count + 1
                tdHeader = New TableCell
                Dim Title As String = String.Empty
                'Header
                If i = 1 Then
                    HeaderTdSetting(tdHeader, "50px", "", "1")
                    trHeader.Controls.Add(tdHeader)
                Else
                    HeaderTdSetting(tdHeader, "110px", "", "1")
                    Link = New HyperLink
                    hid = New HiddenField
                    Link.ID = "hypColDes" + (i - 1).ToString()
                    hid.ID = "hidColid" + (i - 1).ToString()
                    'Link.Width = 100
                    Link.CssClass = "LinkM"
                    GetColType(Link, hid, 0)
                    If (i - 1) > dsM.Tables(0).Rows.Count Then
                        Link.Text = "Column_" + (i - 1).ToString()
                    Else
                        Link.Text = dsM.Tables(0).Rows(i - 2).Item("DESCRIPTION")
                        If dsM.Tables(0).Rows(i - 2).Item("DESCRIPTION") = "SKU" Then
                            HeaderTdSetting(tdHeader, "110px", "", "1")
                        End If
                    End If

                    tdHeader.Controls.Add(hid)
                    tdHeader.Controls.Add(Link)
                    trHeader.Controls.Add(tdHeader)
                End If
            Next
            tblPRICE.Controls.Add(trHeader)

        Catch ex As Exception
            lblError.Text = "Error: BindData() " + ex.Message()
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
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim objGetData As New SavvyProGetData()
        Dim Count As String
        Dim RFPCol(txtRw.Text) As String
        Try

            ds = objGetData.GetPRICEDATARFP(Session("PRICEID").ToString(), Session("USERID"), Session("hidRfpID").ToString())
            dsM = objGetData.GetMasterData(Session("PRICEID").ToString())
            Count = dsM.Tables(0).Rows.Count
            If dsM.Tables(0).Rows.Count <> Session("ColN") Then
                Count = Count + (Int(txtRw.Text) - dsM.Tables(0).Rows.Count)
            End If
            For i = 1 To Count + 1 'dsM.Tables(0).Rows.Count + 1
                tdHeader = New TableCell
                Dim Title As String = String.Empty
                'Header
                If i = 1 Then
                    HeaderTdSetting(tdHeader, "50px", "", "1")
                    trHeader.Controls.Add(tdHeader)
                Else
                    HeaderTdSetting(tdHeader, "110px", "", "1")
                    Link = New HyperLink
                    hid = New HiddenField
                    Link.ID = "hypColDes" + (i - 1).ToString()
                    hid.ID = "hidColid" + (i - 1).ToString()
                    'Link.Width = 100
                    Link.CssClass = "LinkM"
                    GetColType(Link, hid, 0)
                    If (i - 1) > dsM.Tables(0).Rows.Count Then
                        Link.Text = "Column_" + (i - 1).ToString()
                    Else
                        Link.Text = dsM.Tables(0).Rows(i - 2).Item("DESCRIPTION")
                        If dsM.Tables(0).Rows(i - 2).Item("DESCRIPTION") = "SKU" Then
                            HeaderTdSetting(tdHeader, "200px", "", "1")
                        End If
                    End If

                    tdHeader.Controls.Add(hid)
                    tdHeader.Controls.Add(Link)
                    trHeader.Controls.Add(tdHeader)
                End If
            Next
            tblPRICE.Controls.Add(trHeader)

            'Inner
            For i = 1 To ds.Tables(0).Rows.Count
                Dim strVal As String = ""
                trInner = New TableRow

                For j = 1 To dsM.Tables(0).Rows.Count + 1
                    tdInner = New TableCell
                    If j = 1 Then
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.Text = "<b>" + i.ToString() + "</b>"
                        trInner.Controls.Add(tdInner)
                    ElseIf j = dsM.Tables(0).Rows.Count + 1 Then
                        If dsM.Tables(0).Rows(j - 2).Item("TBLNAME").ToString() = "OTHERVAL" Then
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "COL" + i.ToString() + "_" + (j - 1).ToString()
                            txt.Text = FormatNumber(7, 3)
                            txt.MaxLength = 10
                            txt.Width = 70
                            tdInner.Controls.Add(txt)
                        Else
                            tdInner.Text = "<b>" + ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 2).Item("COLMNS").ToString().Replace(" ", "")).ToString() + "</b>"
                        End If
                        If dsM.Tables(0).Rows(j - 2).Item("TBLNAME").ToString() <> "OTHERVAL" And dsM.Tables(0).Rows(j - 2).Item("TBLNAME").ToString() <> "PRICEREQUIREMENT" And dsM.Tables(0).Rows(j - 2).Item("TBLNAME").ToString() <> "PACKAGINGMMNT" Then
                            If strVal = "" Then
                                strVal = ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 2).Item("COLMNS").ToString().Replace(" ", "")).ToString()
                            Else
                                strVal = strVal + "#" + ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 2).Item("COLMNS").ToString().Replace(" ", "")).ToString()
                            End If
                        End If
                        hid.Value = strVal
                        tdInner.Controls.Add(hid)
                        trInner.Controls.Add(tdInner)
                    Else
                        hid = New HiddenField()
                        hid.ID = "hid" + i.ToString()
                        tdInner = New TableCell
                        InnerTdSetting(tdInner, "", "Left")
                        If dsM.Tables(0).Rows(j - 2).Item("TBLNAME").ToString() = "OTHERVAL" Then
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "COL" + i.ToString() + "_" + (j - 1).ToString()
                            txt.Text = FormatNumber(7, 3)
                            txt.MaxLength = 10
                            txt.Width = 70
                            tdInner.Controls.Add(txt)
                        Else
                            tdInner.Text = "<b>" + ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 2).Item("COLMNS").ToString().Replace(" ", "")).ToString() + "</b>"
                        End If
                        If dsM.Tables(0).Rows(j - 2).Item("COLMNS").ToString() = "SKUID" Then
                            tdInner.Text = tdInner.Text + "<b>:" + ds.Tables(0).Rows(i - 1).Item("SKUDES").ToString() + "</b>"
                        End If
                        If dsM.Tables(0).Rows(j - 2).Item("TBLNAME").ToString() <> "OTHERVAL" And dsM.Tables(0).Rows(j - 2).Item("TBLNAME").ToString() <> "PRICEREQUIREMENT" And dsM.Tables(0).Rows(j - 2).Item("TBLNAME").ToString() <> "PACKAGINGMMNT" Then
                            If strVal = "" Then
                                strVal = ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 2).Item("COLMNS").ToString().Replace(" ", "")).ToString()
                            Else
                                strVal = strVal + "#" + ds.Tables(0).Rows(i - 1).Item(dsM.Tables(0).Rows(j - 2).Item("COLMNS").ToString().Replace(" ", "")).ToString()
                            End If
                        End If
                        trInner.Controls.Add(tdInner)
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
   
    Protected Sub btnSAVE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSAVE.Click
        Dim i As New Integer
        Dim ObjUpIns As New SavvyProUpInsData()
        Dim obj As New CryptoHelper
        Dim ds As New DataSet
        Dim dsM As New DataSet
        Dim dsP As New DataSet
        Dim Price As String = ""
        Dim PRCCode As String = ""
        Dim PRCID As String = ""
        Dim IsSKU As Boolean = False
        Try
            Dim objUpInsData As New SavvyProUpInsData
            objUpInsData.InsUpdateRFPPriceData(Session("PriceID"))
            ds = objGetdata.GetPRICEDATARFPN_B(Session("PriceID"), Session("USERID"), Session("hidRfpID").ToString())
            dsM = objGetdata.GetMasterDataN(Session("PriceID"))
            dsP = objGetdata.GetRFPPRICE(Session("PriceID"))

            If dsM.Tables(0).Rows(0).Item("TBLNAME").ToString() = "SKUDETAIL" Then
                For i = 1 To ds.Tables(0).Rows.Count
                    For j = 1 To dsM.Tables(0).Rows.Count
                        If dsM.Tables(0).Rows(j - 1).Item("TBLNAME").ToString() = "OTHERVAL" Then
                            ' txt.ID = "PRC" + i.ToString() + "_" + (j - 1).ToString()
                            Price = Request.Form("COL" + i.ToString() + "_" + (j).ToString() + "")
                            PRCCode = Request.Form("hid" + i.ToString() + "")
                            PRCID = "1" 'Request.Form("hidPRC" + i.ToString() + "_" + (j).ToString() + "")
                            ObjUpIns.InsUpdateSkURfp(Session("PriceID"), PRCID, PRCCode, Price, i)
                            ' ObjUpIns.InsUpdateColors(Session("PriceID"), Session("SVendorID"), PRCID, PRCCode, Price, i)

                        End If
                    Next
                Next
            Else
                For i = 1 To ds.Tables(0).Rows.Count
                    For j = 1 To dsM.Tables(0).Rows.Count
                        If dsM.Tables(0).Rows(j - 1).Item("TBLNAME").ToString() = "OTHERVAL" Then
                            ' txt.ID = "PRC" + i.ToString() + "_" + (j - 1).ToString()
                            Price = Request.Form("COL" + i.ToString() + "_" + (j).ToString() + "")
                            PRCCode = Request.Form("hid" + i.ToString() + "")
                            PRCID = "1" 'Request.Form("hidPRC" + i.ToString() + "_" + (j).ToString() + "")
                            ObjUpIns.InsUpdateColors(Session("PriceID"), Session("SVendorID"), PRCID, PRCCode, Price, i)
                        End If
                    Next
                Next
            End If

            Session("PriceID") = Nothing
            divCreate.Visible = False
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "close", "ClosePopupSM();", True)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Try
            Session("PriceID") = Nothing
            txtRw.Text = "0"
            Dim ds As New DataSet
            Dim id As String = ""
            Dim objGetData As New SavvyProGetData
            Dim objUpInsData As New SavvyProUpInsData
            divCreate.Visible = True
            id = objGetData.GetRFPPriceID()
            If id <> "" Then
                Session("PriceID") = id
                objUpInsData.InsUpdateRFPPriceDataTEMP(Session("hidRfpID").ToString(), id, "Test Price")
                txtPriceName.Text = "Test Price"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            divCreate.Visible = False
            Session("PriceID") = Nothing
        Catch ex As Exception

        End Try
    End Sub
End Class
