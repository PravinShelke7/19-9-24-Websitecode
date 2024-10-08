﻿Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Echem1GetData
Imports Echem1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Echem1_Results_PrintCaseSummary
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
        GetMainHeadingdiv()
        GetContentPlaceHolder()
    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Visible = False
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetLogOffbtn()
        LogOffbtn = Page.Master.FindControl("imgLogoff")
        LogOffbtn.Visible = False
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetInstructionsbtn()
        Instrutionsbtn = Page.Master.FindControl("imgInstructions")
        Instrutionsbtn.Visible = False
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetChartbtn()
        Chartbtn = Page.Master.FindControl("imgChart")
        Chartbtn.Visible = False
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetFeedbackbtn()
        FeedBackbtn = Page.Master.FindControl("imgFeedback")
        FeedBackbtn.Visible = False
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetNotesbtn()
        Notesbtn = Page.Master.FindControl("imgNotes")
        Notesbtn.Visible = False
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Print Case Summary')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Echem1 - Print Case Summary"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Echem1ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_ECHEM1_RESULTS_PRINTCASESUMMARY")
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
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "Show", "visibleAll();", True)

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
        Try
            ProfitAndLoss()
            MaterialAssumption()
            MaterialResults()
            EquipmentAssumption()
            OperationAssumption()
            PersonnelAssumption()
            FixCostAssumption()
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ProfitAndLoss()
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

        Try
            ds = objGetData.GetProfitAndLossDetails(CaseId, False)


            trHeader = New TableRow
            trHeader1 = New TableRow
            For i = 1 To 2
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "150px", "Unit Price", "1")
                        Header2TdSetting(tdHeader1, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "150px", "Product Volume", "1")
                        Header2TdSetting(tdHeader1, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                End Select
            Next
            tblSalesVol.Controls.Add(trHeader)
            tblSalesVol.Controls.Add(trHeader1)




            'Inner
            For i = 1 To 1
                trInner = New TableRow
                For j = 1 To 2
                    tdInner = New TableCell
                    Select Case j

                        Case 1
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            lbl.Text = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("unitprice").ToString()) * CDbl(ds.Tables(0).Rows(0).Item("CURR").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("CONVWT").ToString()), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            lbl.Text = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("SALESVOLUMELB").ToString()), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case Else
                    End Select
                Next
                tblSalesVol.Controls.Add(trInner)

                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If

            Next




            trHeader = New TableRow
            trHeader1 = New TableRow


            For i = 1 To 2
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "150px", "Item", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + ")" ' "(" ")" Added by SagarJ
                        HeaderTdSetting(tdHeader, "150px", "Total Plant", "1")
                        Header2TdSetting(tdHeader1, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                End Select





            Next
            tblPandL.Controls.Add(trHeader)
            tblPandL.Controls.Add(trHeader1)



            For i = 1 To 24

                trInner = New TableRow
                For j = 1 To 2
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
                        Case Else
                    End Select
                Next


                tblPandL.Controls.Add(trInner)

                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
            Next
        Catch ex As Exception
            _lErrorLble.Text = "Error:ProfitAndLoss:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub MaterialAssumption()
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

        Dim chk As New CheckBox

        Dim radio As New RadioButton

        Try
            ds = objGetData.GetExtrusionDetails(CaseId)

            For i = 1 To 8
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "50px", "Layers", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "150px", "Primary Materials", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "", "Weight", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "70px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "", "Price", "2")
                        'tdHeader.Attributes.Add("onmouseover", "Tip('Date:" + ds.Tables(0).Rows(0).Item("Effdate").ToString() + "')")
                        'tdHeader.Attributes.Add("onmouseout", "UnTip('')")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "50px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 6
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 7
                        HeaderTdSetting(tdHeader, "50px", "Recycle", "1")
                        Header2TdSetting(tdHeader2, "", "(%)", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 8
                        HeaderTdSetting(tdHeader, "100px", "Extra-process", "1")
                        Header2TdSetting(tdHeader2, "", "(%)", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)

                End Select





            Next
            'trHeader.Height = 30
            'trHeader.Height = 30

            tblMatInput.Controls.Add(trHeader)
            tblMatInput.Controls.Add(trHeader2)
            tblMatInput.Controls.Add(trHeader1)



            'Inner
            For i = 1 To 10
                trInner = New TableRow
                If CInt(ds.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()) <> 0 Then
                    For j = 1 To 8
                        tdInner = New TableCell

                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "<b>" + i.ToString() + "</b>"
                                trInner.Controls.Add(tdInner)
                            Case 2
                                InnerTdSetting(tdInner, "150px", "Left")
                                Link = New HyperLink
                                hid = New HiddenField
                                Link.ID = "hypMatDes" + i.ToString()
                                hid.ID = "hidMatid" + i.ToString()
                                'Link.Width = 120
                                'Link.CssClass = "Link"
                                GetMaterialDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()))
                                tdInner.Controls.Add(hid)
                                tdInner.Controls.Add(Link)
                                trInner.Controls.Add(tdInner)
                            Case 3
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WEIGHTS" + i.ToString() + "").ToString(), 4)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 4
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WEIGHTP" + i.ToString() + "").ToString(), 4)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 5
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                If ds.Tables(0).Rows(0).Item("PRICES" + i.ToString() + "").ToString() <> "" Then
                                    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PRICES" + i.ToString() + "").ToString(), 3)
                                Else
                                    lbl.Text = ds.Tables(0).Rows(0).Item("PRICES" + i.ToString() + "").ToString()
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 6
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PRICEP" + i.ToString() + "").ToString(), 3)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 7
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("REC" + i.ToString() + "").ToString(), 2)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 8
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("EP" + i.ToString() + "").ToString(), 2)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                        End Select
                    Next
                    If (i Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    'trInner.Height = 30

                    tblMatInput.Controls.Add(trInner)
                End If

            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:MaterialAssumption:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub MaterialResults()
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

        Try
            ds = objGetData.GetExtrusionOutDetails(CaseId)
            For i = 1 To 4
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "50px", "Layers", "1")
                        HeaderTdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "170px", "Materials", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "60px", "Weight ", "1")
                        Title = "(%)"
                        Header2TdSetting(tdHeader1, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        HeaderTdSetting(tdHeader, "100px", "Purchases ", "1")

                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + ")" 'TITLE8 Replaced by TITLECHEM1
                        Header2TdSetting(tdHeader1, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)

                End Select





            Next
            tblMatResults.Controls.Add(trHeader)
            tblMatResults.Controls.Add(trHeader1)



            'Inner
            For i = 1 To 10
                If CInt(ds.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()) <> 0 Then
                    trInner = New TableRow
                    For j = 1 To 4
                        tdInner = New TableCell

                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "<b>" + i.ToString() + "</b>"
                                trInner.Controls.Add(tdInner)
                            Case 2
                                InnerTdSetting(tdInner, "", "Left")
                                lbl = New Label()
                                lbl.Text = ds.Tables(0).Rows(0).Item("MATDE" + i.ToString() + "").ToString()
                                lbl.Width = 170
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 3
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label()
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("W" + i.ToString() + "").ToString(), 2)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 4
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label()
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PURZ" + i.ToString() + "").ToString(), 0)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                        End Select
                    Next
                    If (i Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    If i = 11 Then
                        trInner.ForeColor = Drawing.Color.Red
                        trInner.Font.Bold = True
                    End If

                    tblMatResults.Controls.Add(trInner)
                End If
            Next
        Catch ex As Exception
            _lErrorLble.Text = "Error:MaterialResults:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub EquipmentAssumption()
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
        Dim dsEquip As New DataSet
        Dim dsEEquip As New DataSet
        Dim dvEEquip As New DataView
        Dim dtEEquip As New DataTable
        Try
            ds = objGetData.GetEquipmentInDetails(CaseId)
            'pt changes
            dsEquip = objGetData.GetEquipment("-1", "", "")

            dsEEquip = objGetData.GetEditEquip(CaseId)
            dvEEquip = dsEEquip.Tables(0).DefaultView
            Session("dsEEquip") = dsEEquip
            'end
            For i = 1 To 12
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "20px", "", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        HeaderTdSetting(tdHeader, "100px", "Asset Description", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE7").ToString() + ")"
                        HeaderTdSetting(tdHeader, "", "Plant Area", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "76px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        Header2TdSetting(tdHeader1, "76px", "Preferred", "1")

                End Select


                trHeader1.Controls.Add(tdHeader1)

            Next
            tblEquipment.Controls.Add(trHeader)
            tblEquipment.Controls.Add(trHeader2)
            tblEquipment.Controls.Add(trHeader1)


            'Inner
            For i = 1 To 30
                If CInt(ds.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()) <> 0 Then 'ASSETID replaced by M


                    trInner = New TableRow
                    For j = 1 To 12
                        tdInner = New TableCell

                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "<b>" + i.ToString() + "</b>"
                                trInner.Controls.Add(tdInner)
                            Case 2
                                InnerTdSetting(tdInner, "100px", "Left")
                                Link = New HyperLink
                                hid = New HiddenField
                                Link.ID = "hypAssetDes" + i.ToString()
                                hid.ID = "hidAssetId" + i.ToString()
                                'Link.CssClass = "Link"
                                GetEquipmentInDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()), dsEquip) 'ASSETID by M

                                'GetEquipmentInDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString())) 'ASSETID by M
                                tdInner.Controls.Add(hid)
                                tdInner.Controls.Add(Link)
                                trInner.Controls.Add(tdInner)
                            Case 6
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PAS" + i.ToString() + "").ToString(), 0)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 7
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PAP" + i.ToString() + "").ToString(), 0)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                        End Select
                    Next
                    If (i Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    tblEquipment.Controls.Add(trInner)
                End If
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:EquipmentAssumption:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub OperationAssumption()

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

        Try
            ds = objGetData.GetOperationInDetails(CaseId)
            For i = 1 To 6
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "20px", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        HeaderTdSetting(tdHeader, "140px", "Equipment Description", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)

                    Case 3
                        HeaderTdSetting(tdHeader, "100px", "Maximum Annual Run Hours", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + "/hr)"
                        HeaderTdSetting(tdHeader, "", "Instantaneous Rate", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 5
                        HeaderTdSetting(tdHeader, "100px", "Downtime", "1")
                        Header2TdSetting(tdHeader2, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 6
                        HeaderTdSetting(tdHeader, "100px", "Waste", "1")
                        Header2TdSetting(tdHeader2, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)


                End Select


            Next
            tblOperations.Controls.Add(trHeader)
            tblOperations.Controls.Add(trHeader2)

            'Inner()
            For i = 1 To 30

                If CInt(ds.Tables(0).Rows(0).Item("ASSETID" + i.ToString() + "").ToString()) <> 0 Then
                    trInner = New TableRow
                    For j = 1 To 6
                        tdInner = New TableCell

                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "<b>" + i.ToString() + "</b>"
                                trInner.Controls.Add(tdInner)
                            Case 2
                                InnerTdSetting(tdInner, "140px", "left")
                                lbl = New Label
                                If ds.Tables(0).Rows(0).Item("LEQUIPDES" + i.ToString() + "").ToString() <> "" Then
                                    lbl.Text = ds.Tables(0).Rows(0).Item("LEQUIPDES" + i.ToString() + "").ToString()
                                Else
                                    lbl.Text = ds.Tables(0).Rows(0).Item("EQDES" + i.ToString() + "").ToString()
                                End If
                                'lbl.Text = ds.Tables(0).Rows(0).Item("EQDES" + i.ToString() + "").ToString() 'replaced EQUIPDES by EQDES 
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 3
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MAXHOUR" + i.ToString() + "").ToString(), 0) 'replaced OMAXRH by MAXHOUR 
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 4
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                Dim cal As Double
                                cal = CDbl(ds.Tables(0).Rows(0).Item("OPINSTGRSRATE" + i.ToString() + "") * ds.Tables(0).Rows(0).Item("convwt")).ToString()
                                'lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("OPINSTGRSRATE" + i.ToString() + "").ToString(), 2)
                                lbl.Text = FormatNumber(cal, 1)

                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 5
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("DT" + i.ToString() + "").ToString(), 1) 'nO REPLACMENT
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 6
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("OPWASTE" + i.ToString() + "").ToString(), 1) 'nO REPLACMENT FOR OPWASTE
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)


                        End Select
                    Next
                    If (i Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    tblOperations.Controls.Add(trInner)
                End If
            Next
        Catch ex As Exception
            _lErrorLble.Text = "Error:OperationAssumption:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub PersonnelAssumption()

        Dim ds As New DataSet
        Dim dsEffCountry As New DataSet
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

        Try
            ' dsEffCountry = objGetData.GetEFFCOUNTRY(CaseId)
            dsEffCountry = objGetData.GetCountryTable(CaseId) 'Inserted by sagarj 
            Dim effCountry As String
            effCountry = dsEffCountry.Tables(0).Rows(0).Item("EFFCOUNTRY").ToString()
            ds = objGetData.GetPersonnelInDetails(CaseId, effCountry)
            For i = 1 To 5
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "20px", "", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        HeaderTdSetting(tdHeader, "160px", "Position Description", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        HeaderTdSetting(tdHeader, "100px", "Number of Workers", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/year)" 'PREFTITLE2 Replaced by TITLE2
                        HeaderTdSetting(tdHeader, "", "Salary and Benefits ", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "90px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 5
                        Header2TdSetting(tdHeader1, "90px", "Preferred", "1")
                    Case 6
                        HeaderTdSetting(tdHeader, "100px", "Effective Date", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                End Select


                trHeader1.Controls.Add(tdHeader1)

            Next
            tblPersonnel.Controls.Add(trHeader)
            tblPersonnel.Controls.Add(trHeader2)
            tblPersonnel.Controls.Add(trHeader1)


            'Inner
            For i = 1 To 30
                If CInt(ds.Tables(0).Rows(0).Item("PERSPOS" + i.ToString() + "").ToString()) <> 0 Then
                    trInner = New TableRow
                    For j = 1 To 5
                        tdInner = New TableCell

                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "<b>" + i.ToString() + "</b>"
                                trInner.Controls.Add(tdInner)
                            Case 2
                                InnerTdSetting(tdInner, "160px", "Left")
                                Link = New HyperLink
                                hid = New HiddenField
                                Link.ID = "hypPosDes" + i.ToString()
                                hid.ID = "hidPosId" + i.ToString()
                                'Link.CssClass = "Link"
                                GetPersonnelDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("PERSPOS" + i.ToString() + "").ToString()), dsEffCountry.Tables(0).Rows(0).Item("COUNTRY").ToString())
                                tdInner.Controls.Add(hid)
                                tdInner.Controls.Add(Link)
                                trInner.Controls.Add(tdInner)

                            Case 3
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PERNUM" + i.ToString() + "").ToString(), 2)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 4
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                If (ds.Tables(0).Rows(0).Item("SALS" + i.ToString() + "")).ToString() <> "" Then
                                    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("SALS" + i.ToString() + "").ToString(), 0)
                                Else
                                    lbl.Text = ds.Tables(0).Rows(0).Item("SALS" + i.ToString() + "").ToString()
                                End If
                                lbl.Width = 70
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                            Case 5
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("SALPRE" + i.ToString() + "").ToString(), 0)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 6
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = ds.Tables(0).Rows(0).Item("EFFDATE").ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                        End Select
                    Next
                    If (i Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    tblPersonnel.Controls.Add(trInner)
                End If
            Next



        Catch ex As Exception
            _lErrorLble.Text = "Error:PersonnelAssumption:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub FixCostAssumption()

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

        Try
            ds = objGetData.GetFixedCostDetails(CaseId)
            'Fixed Cost Details
            For i = 1 To 3
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "120px", "Category", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1) 'Added by sagarj
                        trHeader2.Controls.Add(tdHeader2) 'Added by sagarj
                    Case 2
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + ")"
                        HeaderTdSetting(tdHeader, "", "Fixed Cost", "2")
                        Header2TdSetting(tdHeader1, "", Title, "2")
                        Header2TdSetting(tdHeader2, "90px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1) 'Added by sagarj
                        trHeader2.Controls.Add(tdHeader2) 'Added by sagarj

                    Case 3

                        Header2TdSetting(tdHeader2, "90px", "Preferred", "1") 'Added by sagarj

                        trHeader2.Controls.Add(tdHeader2) 'Added by sagarj


                End Select


            Next
            tblFixCost.Controls.Add(trHeader)
            tblFixCost.Controls.Add(trHeader1)
            tblFixCost.Controls.Add(trHeader2)

            'Inner()
            For i = 1 To 14


                trInner = New TableRow
                For j = 1 To 3
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "left")
                            lbl = New Label
                            lbl.Text = ds.Tables(0).Rows(0).Item("CATEGORY" + i.ToString() + "").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("FCSG" + i.ToString() + "").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("FCPREF" + i.ToString() + "").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                tblFixCost.Controls.Add(trInner)
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

    Protected Sub GetMaterialDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal MatId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New Echem1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty

        Try

            Ds = ObjGetdata.GetMaterials(MatId, "", "")
            LinkMat.Text = Ds.Tables(0).Rows(0).Item("MATDES").ToString()


        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetDeptDetails(ByRef LinkDep As HyperLink, ByVal hid As HiddenField, ByVal ProcId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New Echem1GetData.Selectdata()
        Dim Path As String = String.Empty
        Try
            Ds = ObjGetdata.GetDept(ProcId, "", "")
            LinkDep.Text = Ds.Tables(0).Rows(0).Item("PROCDE").ToString()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub GetEquipmentInDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal EqId As Integer, ByVal dsEquip As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvEquip As New DataView
        Dim dtEquip As New DataTable

        Dim dsEEquip As New DataSet
        Dim str As String = ""
        Dim dv As New DataView
        Dim dt As New DataTable
        Dim dvMat As New DataView
        Dim dtMat As New DataTable

        Try
            'Ds = ObjGetdata.GetEquipment(EqId, "", "")
            dsEEquip = DirectCast(Session("dsEEquip"), DataSet)
            dv = dsEEquip.Tables(0).DefaultView

            If EqId <> 0 Then

                dv.RowFilter = "EQUIPID=" + EqId.ToString()
                dt = dv.ToTable()
                If dt.Rows.Count > 0 Then
                    LinkMat.Text = dt.Rows(0).Item("equipDES").ToString().Replace("&#", "'")
                    LinkMat.ToolTip = dt.Rows(0).Item("equipDES").ToString().Replace("&#", "'")
                    'hid.Value = EqId.ToString()
                    'Path = "../PopUp/GetEquipmentPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "&EId=" + EqId.ToString() + "&Case=" + CaseId.ToString() + ""
                    'LinkMat.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"
                Else

                    dvEquip = dsEquip.Tables(0).DefaultView
                    dvEquip.RowFilter = "EQUIPID = " + EqId.ToString()
                    dtEquip = dvEquip.ToTable()

                    LinkMat.Text = dtEquip.Rows(0).Item("equipDES").ToString()
                    'hid.Value = EqId.ToString()
                    '  Path = "../PopUp/GetEquipmentPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "&EId=" + EqId.ToString() + "&Case=" + CaseId.ToString() + ""
                    ' LinkMat.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"
                End If
            Else

                dvEquip = dsEquip.Tables(0).DefaultView
                dvEquip.RowFilter = "EQUIPID = " + EqId.ToString()
                dtEquip = dvEquip.ToTable()

                LinkMat.Text = dtEquip.Rows(0).Item("equipDES").ToString()
                'hid.Value = EqId.ToString()
                'Path = "../PopUp/GetEquipmentPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "&EId=" + EqId.ToString() + "&Case=" + CaseId.ToString() + ""
                ' LinkMat.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"
            End If

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    'Protected Sub GetEquipmentInDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal EqId As Integer)
    '    Dim Ds As New DataSet
    '    Dim ObjGetdata As New Echem1GetData.Selectdata()
    '    Dim hidval As New HiddenField
    '    Dim Path As String = String.Empty
    '    Try

    '        Ds = ObjGetdata.GetEquipment(EqId, "", "")
    '        LinkMat.Text = Ds.Tables(0).Rows(0).Item("equipDES").ToString()

    '    Catch ex As Exception
    '        ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
    '    End Try
    'End Sub

    Protected Sub GetPersonnelDetails(ByRef LinkPer As HyperLink, ByVal hid As HiddenField, ByVal PersId As Integer, ByVal COUNTRY As String)
        Dim Ds As New DataSet
        Dim ObjGetdata As New Echem1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try

            Ds = ObjGetdata.GetPersonnelInfo(PersId, "", "", COUNTRY)
            LinkPer.Text = Ds.Tables(0).Rows(0).Item("persDES").ToString()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub




End Class
