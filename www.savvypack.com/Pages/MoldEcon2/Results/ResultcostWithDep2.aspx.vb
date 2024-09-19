﻿Imports System.Data
Imports System.Data.OleDb
Imports System
Imports MoldE2GetData
Imports MoldE2UpInsData
Partial Class Pages_MoldEcon2_Results_ResultcostWithDep2
    Inherits System.Web.UI.Page

#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _iCaseId As Integer
    Dim _iUserId As Integer
    Dim _strUserRole As String
    Dim _btnUpdate As ImageButton
    Dim _btnLogOff As ImageButton
    Dim _btnChart As ImageButton
    Dim _btnNotes As ImageButton
    Dim _btnFeedBack As ImageButton
    Dim _btnInstrutions As ImageButton
    Dim _btnCompare As ImageButton
    Dim _divMainHeading As HtmlGenericControl
    Dim _ctlContentPlaceHolder As ContentPlaceHolder


    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property CaseId() As Integer
        Get
            Return _iCaseId
        End Get
        Set(ByVal Value As Integer)
            _iCaseId = Value
        End Set
    End Property

    Public Property UserId() As Integer
        Get
            Return _iUserId
        End Get
        Set(ByVal Value As Integer)
            _iUserId = Value
        End Set
    End Property

    Public Property UserRole() As String
        Get
            Return _strUserRole
        End Get
        Set(ByVal Value As String)
            _strUserRole = Value
        End Set
    End Property

    Public Property Updatebtn() As ImageButton
        Get
            Return _btnUpdate
        End Get
        Set(ByVal value As ImageButton)
            _btnUpdate = value
        End Set
    End Property

    Public Property LogOffbtn() As ImageButton
        Get
            Return _btnLogOff
        End Get
        Set(ByVal value As ImageButton)
            _btnLogOff = value
        End Set
    End Property

    Public Property Chartbtn() As ImageButton
        Get
            Return _btnChart
        End Get
        Set(ByVal value As ImageButton)
            _btnChart = value
        End Set
    End Property

    Public Property Notesbtn() As ImageButton
        Get
            Return _btnNotes
        End Get
        Set(ByVal value As ImageButton)
            _btnNotes = value
        End Set
    End Property

    Public Property FeedBackbtn() As ImageButton
        Get
            Return _btnFeedBack
        End Get
        Set(ByVal value As ImageButton)
            _btnFeedBack = value
        End Set
    End Property

    Public Property Instrutionsbtn() As ImageButton
        Get
            Return _btnInstrutions
        End Get
        Set(ByVal value As ImageButton)
            _btnInstrutions = value
        End Set
    End Property

    Public Property Comparebtn() As ImageButton
        Get
            Return _btnCompare
        End Get
        Set(ByVal value As ImageButton)
            _btnCompare = value
        End Set
    End Property

    Public Property MainHeading() As HtmlGenericControl
        Get
            Return _divMainHeading
        End Get
        Set(ByVal value As HtmlGenericControl)
            _divMainHeading = value
        End Set
    End Property

    Public Property ctlContentPlaceHolder() As ContentPlaceHolder
        Get
            Return _ctlContentPlaceHolder
        End Get
        Set(ByVal value As ContentPlaceHolder)
            _ctlContentPlaceHolder = value
        End Set
    End Property



    Public DataCnt As Integer
    Public CaseDesp As New ArrayList
#End Region

#Region "MastePage Content Variables"

    Protected Sub GetMasterPageControls()
        GetErrorLable()
        GetUpdatebtn()
        GetLogOffbtn()
        GetInstructionsbtn()
        GetChartbtn()
        GetFeedbackbtn()
        GetNotesbtn()
        GetComparebtn()
        GetMainHeadingdiv()
        GetContentPlaceHolder()
    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Visible = True
        Updatebtn.Attributes.Add("onclick", "return checkNumericAll()")
        AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetLogOffbtn()
        LogOffbtn = Page.Master.FindControl("imgLogoff")
        LogOffbtn.Visible = False
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetInstructionsbtn()
        Instrutionsbtn = Page.Master.FindControl("imgInstructions")
        Instrutionsbtn.Visible = True
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetChartbtn()
        Chartbtn = Page.Master.FindControl("imgChart")
        Chartbtn.Visible = False
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetFeedbackbtn()
        FeedBackbtn = Page.Master.FindControl("imgFeedback")
        FeedBackbtn.Visible = True
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetNotesbtn()
        Notesbtn = Page.Master.FindControl("imgNotes")
        Notesbtn.Visible = False
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetComparebtn()
        Dim obj As New CryptoHelper()
        Comparebtn = Page.Master.FindControl("imgCompare")
        Comparebtn.Visible = True
        Comparebtn.OnClientClick = "return Compare('Comparision.aspx?Type=" + obj.Encrypt("CustomerCSTDEP") + "') "  'obj.Encrypt("CST") CSTDEP

        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Packaging Cost with depreciation')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Econ2 Mold - Packaging Cost with depreciation"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("MoldEcon2ContentPlaceHolder")
    End Sub

#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GetMasterPageControls()
            GetSessionDetails()
            If Not IsPostBack Then
                GetPageDetails()
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetSessionDetails()
        Try
            UserId = Session("UserId")
            CaseId = Session("MoldE2CaseId")
            UserRole = Session("MoldE2UserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New MoldE2GetData.Selectdata
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
        Dim customerSaleValue As Double
        Dim CustValue As Decimal = 0
        Dim newVal As Decimal
        Dim lbToUnit As Double
        Dim UnitTolb As Double
        Try
            ds = objGetData.GetCostDetailsWithDep(CaseId, False)
            customerSaleValue = CDbl(ds.Tables(0).Rows(0).Item("CUSSALESVOLUME").ToString())
            lblNewSalesValue.Text = "Customer Sales Volume :"
            txtNewSaleValue.Text = FormatNumber(customerSaleValue, 0)
            BindDDL(ds.Tables(0).Rows(0).Item("TITLE8").ToString(), "Units", ds.Tables(0).Rows(0).Item("CUSSALESUNIT").ToString())

            If CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMEUNIT")) > 0 Then
                UnitTolb = CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMEUNIT"))
            End If
            If CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0 Then
                lbToUnit = CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMEUNIT").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMELB"))
            End If

            For i = 1 To 7
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "190px", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + ")"
                        HeaderTdSetting(tdHeader, "120px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + ")"
                        HeaderTdSetting(tdHeader, "", "Customer Total", "2")
                        Header2TdSetting(tdHeader1, "120px", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        Header2TdSetting(tdHeader1, "90px", "(%)", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "Customer By Weight", "1")
                        Header2TdSetting(tdHeader1, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 6
                        If CDbl(ds.Tables(0).Rows(0).Item("finvolmunits").ToString()) > 0 Then
                            Title = "(" + ds.Tables(0).Rows(0).Item("title6").ToString() + " per unit)"
                        End If
                        HeaderTdSetting(tdHeader, "100px", "Customer By Volume", "1")
                        Header2TdSetting(tdHeader1, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                End Select
            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader1)

            If ds.Tables(0).Rows(0).Item("VOLUNI1").ToString() <> "" Then
                lblSalesVol.Text = "<b>" + ds.Tables(0).Rows(0).Item("VOLUNI1").ToString() + ":</b> " + FormatNumber(ds.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString(), 0).ToString()
            End If
            If ds.Tables(0).Rows(0).Item("VOLUNI2").ToString() <> "" Then
                lblSalesVolUnit.Text = "<b>" + ds.Tables(0).Rows(0).Item("VOLUNI2").ToString() + ":</b> " + FormatNumber(ds.Tables(0).Rows(0).Item("SALESVOLUMEUNIT").ToString(), 0).ToString()

            End If

            'Inner
            For i = 1 To 25

                trInner = New TableRow
                For j = 1 To 7
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Left")
                            If i = 6 Or i = 24 Or i = 25 Then
                                tdInner.Text = "<b>" + ds.Tables(0).Rows(0).Item("DES" + i.ToString() + "").ToString() + "</b>"
                            Else
                                tdInner.Text = "<span style='margin-left:20px;'><b>" + ds.Tables(0).Rows(0).Item("DES" + i.ToString() + "").ToString() + "</span></b>"
                            End If


                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            lbl.Text = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds.Tables(0).Rows(0).Item("CURR").ToString()), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            Dim percentage As New Decimal
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            If CDbl(ds.Tables(0).Rows(0).Item("CUSSALESUNIT").ToString()) = 0 Then
                                If CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) > 0 Then
                                    percentage = CDbl(ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString() * CDbl(ds.Tables(0).Rows(0).Item("CURR").ToString())) * customerSaleValue / CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString())
                                    CustValue = CDbl(ds.Tables(0).Rows(0).Item("PL25").ToString() * CDbl(ds.Tables(0).Rows(0).Item("CURR").ToString())) * customerSaleValue / CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString())
                                End If
                            Else
                                If CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMEUNIT").ToString()) > 0 Then
                                    percentage = CDbl(ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds.Tables(0).Rows(0).Item("CURR").ToString()) * customerSaleValue / CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMEUNIT").ToString())
                                    CustValue = CDbl(ds.Tables(0).Rows(0).Item("PL25").ToString()) * CDbl(ds.Tables(0).Rows(0).Item("CURR").ToString()) * customerSaleValue / CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMEUNIT").ToString())
                                End If
                            End If
                            If i = 1 Then
                                newVal = percentage
                            Else
                                newVal = percentage
                            End If
                            lbl.Text = FormatNumber(percentage, 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            Dim percentage As New Decimal
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            If CDbl(CustValue) > 0 Then
                                percentage = CDbl(newVal) / CustValue * 100
                                lbl.Text = FormatNumber(percentage, 2)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            Dim perwt As New Decimal
                            lbl = New Label()
                            Dim salesVolume As Double
                            'salesVolume = CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("CONVWT").ToString())

                            'If salesVolume > 1 Then
                            '    perwt = CDbl(newVal) / salesVolume / CDbl(ds.Tables(0).Rows(0).Item("CONVWT").ToString())
                            '    lbl.Text = FormatNumber(perwt, 3)
                            '    lbl.Text = FormatNumber(perwt, 3)
                            'End If
                            If CDbl(ds.Tables(0).Rows(0).Item("CUSSALESUNIT").ToString()) <> 0 Then
                                If CDbl(customerSaleValue * UnitTolb) <> 0 Then
                                    perwt = CDbl(newVal) / CDbl(customerSaleValue * UnitTolb)
                                    lbl.Text = FormatNumber(perwt, 3)
                                Else
                                    lbl.Text = "na"
                                End If
                            Else
                                If CDbl(customerSaleValue) <> 0 Then
                                    perwt = CDbl(newVal) / CDbl(customerSaleValue)
                                    lbl.Text = FormatNumber(perwt, 3)
                                Else
                                    lbl.Text = "na"
                                End If
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Right")
                            Dim perunit As New Decimal
                            lbl = New Label()
                            'If CDbl(ds.Tables(0).Rows(0).Item("finvolmunits").ToString()) > 0 Then
                            '    perunit = CDbl(newVal) / CDbl(ds.Tables(0).Rows(0).Item("finvolmunits").ToString()) * 100
                            '    lbl.Text = FormatNumber(perunit, 4)
                            'End If
                            If CDbl(ds.Tables(0).Rows(0).Item("CUSSALESUNIT").ToString()) = 0 Then
                                If CDbl(customerSaleValue * lbToUnit) <> 0 Then
                                    perunit = (CDbl(newVal) / CDbl(customerSaleValue * lbToUnit)) * 100
                                    lbl.Text = FormatNumber(perunit, 4)
                                Else
                                    lbl.Text = "na"
                                End If
                            Else
                                If CDbl(customerSaleValue) <> 0 Then
                                    perunit = (CDbl(newVal) / CDbl(customerSaleValue)) * 100
                                    lbl.Text = FormatNumber(perunit, 4)
                                Else
                                    lbl.Text = "na"
                                End If

                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case Else
                    End Select
                Next
                tblComparision.Controls.Add(trInner)

                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If

            Next
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub BindDDL(ByVal FirstVal As String, ByVal SecVal As String, ByVal Unit As String)
        ddlCustUnit.Items.Clear()
        Dim list(2) As ListItem
        list(0) = New ListItem
        list(0).Text = FirstVal ' ds.Tables(0).Rows(0).Item("TITLE8").ToString()
        list(0).Value = 0
        ddlCustUnit.Items.Add(list(0))
        list(1) = New ListItem
        list(1).Text = SecVal ' ds.Tables(0).Rows(0).Item("SUNITLBL").ToString()
        list(1).Value = 1
        ddlCustUnit.Items.Add(list(1))
        ddlCustUnit.SelectedValue = CInt(Unit)
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
            _lErrorLble.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
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
            _lErrorLble.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
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
            _lErrorLble.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub


    Protected Sub LableSetting(ByVal lbl As Label, ByVal Css As String)
        Try
            lbl.CssClass = Css

        Catch ex As Exception
            _lErrorLble.Text = "Error:LableSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub LeftTdSetting(ByVal Td As TableCell, ByVal Text As String, ByVal tr As TableRow, ByVal Css As String)
        Try
            Td.Text = Text
            InnerTdSetting(Td, "", "Left")
            tr.Controls.Add(Td)
            tr.CssClass = Css
        Catch ex As Exception
            _lErrorLble.Text = "Error:LeftTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim ObjUpIns As New MoldE2UpInsData.UpdateInsert()
            Dim obj As New CryptoHelper
            Dim cusSalesVolume As String
            Dim cusSalesUnit As String

            cusSalesVolume = Request.Form("ctl00$MoldEcon2ContentPlaceHolder$txtNewSaleValue")
            cusSalesUnit = ddlCustUnit.SelectedValue
            If Not IsNumeric(cusSalesVolume) Then
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
            End If
            ObjUpIns.CSaleVolumeUpdate(CaseId, cusSalesVolume, cusSalesUnit)
            Calculate()
            'Update Server Date
            ObjUpIns.ServerDateUpdate(CaseId, Session("MoldE2UserName"))
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub Calculate()
        Try
            Dim MoldE1Conn As String = System.Configuration.ConfigurationManager.AppSettings("MoldE1ConnectionString")
            Dim MoldE2Conn As String = System.Configuration.ConfigurationManager.AppSettings("MoldE2ConnectionString")
            Dim obj As New MoldEcon2Calculation.MoldEcon2Calculations(MoldE2Conn, MoldE1Conn)
            obj.MoldEcon2Calculate(CaseId)
        Catch ex As Exception
            ErrorLable.Text = "Error:Calculate:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class
