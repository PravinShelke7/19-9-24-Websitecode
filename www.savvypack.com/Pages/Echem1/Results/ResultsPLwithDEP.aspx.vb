﻿Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Echem1GetData
Imports Echem1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Echem1_Results_ResultsPLwithDEP
    Inherits System.Web.UI.Page
    Public CaseDes As String = String.Empty

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
        Comparebtn.OnClientClick = "return Compare('Comparision.aspx?Type=" + obj.Encrypt("PLDEP") + "') " 'Changed PL with PLDEP 

        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Profit and Loss Statement with Depreciation')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Echem1 - Profit and Loss Statement with Depreciation"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Echem1ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_ECHEM1_RESULTS_RESULTSPL")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
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
            CaseId = Session("Echem1CaseId")
            UserRole = Session("Echem1UserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New Echem1GetData.Selectdata
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
            ds = objGetData.GetProfitAndLossDetailsWithDep(CaseId, False)
            For i = 1 To 8
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "150px", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + ")"
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
                        If CDbl(ds.Tables(0).Rows(0).Item("finvolmunits").ToString()) > 0 Then
                            Title = "(" + ds.Tables(0).Rows(0).Item("title6").ToString() + " Per unit)"
                        End If
                        HeaderTdSetting(tdHeader, "90px", "By Volume", "1")
                        HeaderTdSetting(tdHeader1, "90px", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 6
                        If ds.Tables(0).Rows(0).Item("UNITTYPE").ToString() = 0 Then
                            Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        ElseIf ds.Tables(0).Rows(0).Item("UNITTYPE").ToString() = 1 Then
                            If CDbl(ds.Tables(0).Rows(0).Item("finvolmunits").ToString()) > 0 Then
                                Title = "(" + ds.Tables(0).Rows(0).Item("title6").ToString() + " Per unit)"
                            End If
                        ElseIf ds.Tables(0).Rows(0).Item("UNITTYPE").ToString() = 2 Then
                            Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/thousand)"
                        End If
                        HeaderTdSetting(tdHeader, "", "Change Price", "3")
                        Header2TdSetting(tdHeader2, "90px", "Suggested", "1")
                        Header2TdSetting(tdHeader1, "", Title, "3")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader1)
                        trHeader1.Controls.Add(tdHeader2)
                    Case 7
                        Header2TdSetting(tdHeader1, "90px", "Preferred", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 8
                        Header2TdSetting(tdHeader1, "70px", "Unit", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)

                End Select
            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader1)

            If ds.Tables(0).Rows(0).Item("VOLUNI1").ToString() <> "" Then
                lblSalesVol.Text = "<b>" + ds.Tables(0).Rows(0).Item("VOLUNI1").ToString() + ":</b> " + FormatNumber(ds.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString(), 0).ToString()
            End If
            If ds.Tables(0).Rows(0).Item("VOLUNI2").ToString() <> "" Then
                lblSalesVolUnit.Text = "<b>" + ds.Tables(0).Rows(0).Item("VOLUNI2").ToString() + ":</b> " + FormatNumber(ds.Tables(0).Rows(0).Item("SALESVOLUMEUNIT").ToString(), 0).ToString()
            End If


            'Inner
            hdnVolume.Value = CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString())
            hdnUnit.Value = CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMEUNIT").ToString())
            For i = 1 To 25

                trInner = New TableRow
                For j = 1 To 8
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Left")
                            tdInner.Text = "<b>" + ds.Tables(0).Rows(0).Item("DES" + i.ToString() + "").ToString() + "</b>"
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
                            If CDbl(ds.Tables(0).Rows(0).Item("PL1").ToString()) > 0 Then
                                percentage = CDbl(ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("PL1").ToString()) * 100
                                lbl.Text = FormatNumber(percentage, 2)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Right")
                            Dim perwt As New Decimal
                            lbl = New Label()
                            Dim salesVolume As Double
                            salesVolume = CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("CONVWT").ToString())

                            If salesVolume > 1 Then
                                perwt = CDbl(ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) / salesVolume * CDbl(ds.Tables(0).Rows(0).Item("CURR").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("CONVWT").ToString())
                                lbl.Text = FormatNumber(perwt, 3)
                                lbl.Text = FormatNumber(perwt, 3)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            Dim perunit As New Decimal
                            lbl = New Label()
                            If CDbl(ds.Tables(0).Rows(0).Item("finvolmunits").ToString()) > 0 Then
                                perunit = CDbl(ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("finvolmunits").ToString()) * 100 * CDbl(ds.Tables(0).Rows(0).Item("CURR").ToString())
                                lbl.Text = FormatNumber(perunit, 4)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            If i = 1 Then
                                If CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 0 Then
                                    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("SPRICE").ToString(), 3)
                                ElseIf CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 1 Then
                                    strVol = CDbl(ds.Tables(0).Rows(0).Item("SPRICE").ToString()) * ((CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) * 100) / CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMEUNIT").ToString()))
                                    lbl.Text = FormatNumber(strVol, 3)
                                ElseIf CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 2 Then
                                    strVol = ((CDbl(ds.Tables(0).Rows(0).Item("SPRICE").ToString()) * (CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()))) / CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMEUNIT").ToString())) * 1000
                                    lbl.Text = FormatNumber(strVol, 3)
                                End If
                            Else
                                lbl.Text = ""
                            End If

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Center")
                            Dim perunit As New Decimal
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "UNITPP"
                            txt.Width = 70

                            perunit = CDbl(ds.Tables(0).Rows(0).Item("unitprice").ToString()) * CDbl(ds.Tables(0).Rows(0).Item("CURR").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("CONVWT").ToString())
                            txt.MaxLength = 12
                            If i = 1 Then
                                If CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 0 Then
                                    txt.Text = FormatNumber(perunit, 3)
                                ElseIf CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 1 Then
                                    strVol = CDbl(perunit) * ((CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()) * 100) / CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMEUNIT").ToString()))
                                    txt.Text = FormatNumber(strVol, 3)
                                ElseIf CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 2 Then
                                    strVol = ((CDbl(perunit) * (CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()))) / CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMEUNIT").ToString())) * 1000
                                    txt.Text = FormatNumber(strVol, 3)
                                End If
                                tdInner.Controls.Add(txt)
                            End If
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "Center")
                            ddl = New DropDownList()
                            If i = 1 Then
                                BindUnitType(ddl, ds.Tables(0).Rows(0).Item("UNITTYPE").ToString())
                                tdInner.Controls.Add(ddl)
                            End If
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

    Protected Sub TextBoxSetting(ByVal txt As TextBox, ByVal Css As String)
        Try
            txt.CssClass = Css

        Catch ex As Exception
            _lErrorLble.Text = "Error:TextBoxSetting:" + ex.Message.ToString()
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
    Protected Sub BindUnitType(ByVal ddl As DropDownList, ByVal unitType As String)
        Dim objGetData As New Echem1GetData.Selectdata()
        Dim dts As New DataSet()
        Try
            dts = objGetData.getUnits(CaseId)
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


    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim UnitPrice As String = String.Empty
        Dim UnitType As String = String.Empty
        Dim ObjUpIns As New Echem1UpInsData.UpdateInsert()
        Dim obj As New CryptoHelper
        Try
            If Not objRefresh.IsRefresh Then
                UnitPrice = Request.Form("ctl00$Echem1ContentPlaceHolder$UNITPP").Replace(",", "")
                UnitType = Request.Form("ctl00$Echem1ContentPlaceHolder$UNITTYPE")
                'Check For IsNumric
                If Not IsNumeric(UnitPrice) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If

                If UnitType = 1 Then
                    UnitPrice = UnitPrice * (hdnUnit.Value / (hdnVolume.Value * 100))
                ElseIf UnitType = 2 Then
                    UnitPrice = UnitPrice * (hdnUnit.Value / (hdnVolume.Value * 1000))
                End If
                If hdnVolume.Value <> "0" Then
                    ObjUpIns.ResultsPl(CaseId.ToString(), UnitPrice, UnitType)
                End If
                Calculate()
            End If
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub Calculate()
        Try
            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim Echem1Conn As String = System.Configuration.ConfigurationManager.AppSettings("Echem1ConnectionString")
            Dim obj As New Echem1Calculation.Echem1Calculations(Echem1Conn, Econ1Conn)
            obj.Echem1Calculate(CaseId)
        Catch ex As Exception
            ErrorLable.Text = "Error:Calculate:" + ex.Message.ToString() + ""
        End Try
    End Sub


End Class
