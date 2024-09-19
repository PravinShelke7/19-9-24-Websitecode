Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E1GetData
Imports E1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ1_IntResults_ExtrusrionOut
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
        Updatebtn.Attributes.Add("onclick", "return checkNumericAllForResult();")
        Updatebtn.Visible = True
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

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Material and Structure Results')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Econ1 - Material and Structure Results"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Econ1ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_Pages_Econ1_IntResults_ExtrusrionOut")
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
                GetDiscreteMat()
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetSessionDetails()
        Try
            UserId = Session("UserId")
            CaseId = Session("E1CaseId")
            UserRole = Session("E1UserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New E1GetData.Selectdata
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
        Dim dspl As New DataSet
        Dim dsSug As New DataSet
        Dim dspreferences As New DataSet
        Try
            ds = objGetData.GetExtrusionOutDetails(CaseId)
            'dspl = objGetData.GetProfitAndLossDetails(CaseId, True)
            'dspreferences = objGetData.GetPref(CaseId)
            'dsSug = objGetData.GetExtrusionDetailsBarrS(CaseId)
            For i = 1 To 18
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
                        HeaderTdSetting(tdHeader, "90px", "Specific Gravity", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        HeaderTdSetting(tdHeader, "80px", "Weight/Area", "1")
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + "/" + ds.Tables(0).Rows(0).Item("TITLE3").ToString() + ")"
                        Header2TdSetting(tdHeader1, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        HeaderTdSetting(tdHeader, "60px", "Weight ", "1")
                        Title = "(%)"
                        Header2TdSetting(tdHeader1, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 6, 7, 8
                        HeaderTdSetting(tdHeader, "90px", "Purchases ", "1")
                        If i = 6 Then
                            Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        ElseIf i = 7 Then
                            Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + ")"
                        ElseIf i = 8 Then
                            Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        End If

                        Header2TdSetting(tdHeader1, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 9
                        HeaderTdSetting(tdHeader, "90px", "Purchases", "1")
                        Header2TdSetting(tdHeader1, "", "(" + ds.Tables(0).Rows(0).Item("PUN").ToString() + ")", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 10
                        HeaderTdSetting(tdHeader, "90px", "Production", "1")
                        Header2TdSetting(tdHeader1, "", "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 11
                        HeaderTdSetting(tdHeader, "90px", "Production", "1")
                        Header2TdSetting(tdHeader1, "", "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + ")", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 12
                        HeaderTdSetting(tdHeader, "90px", "Waste", "1")
                        Header2TdSetting(tdHeader1, "", "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 13
                        HeaderTdSetting(tdHeader, "90px", "Waste", "1")
                        Header2TdSetting(tdHeader1, "", "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + ")", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 14
                        HeaderTdSetting(tdHeader, "90px", "Waste", "1")
                        Header2TdSetting(tdHeader1, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)

                    Case 15
                        HeaderTdSetting(tdHeader, "140px", "Credit", "1")
                        Header2TdSetting(tdHeader1, "", "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 16
                        HeaderTdSetting(tdHeader, "140px", "Price", "1")
                        Header2TdSetting(tdHeader1, "", "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 17
                        HeaderTdSetting(tdHeader, "90px", "credit", "1")
                        Header2TdSetting(tdHeader1, "", "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + ")", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 18
                        HeaderTdSetting(tdHeader, "90px", "Net Purchases", "1")
                        Header2TdSetting(tdHeader1, "", "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + ")", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                End Select
            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader1)

            'Inner
            For i = 1 To 13
                trInner = New TableRow
                For j = 1 To 18
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            'Layer
                            tdInner.Style.Add("padding-right", "15px")
                            lbl = New Label()
                            If i = 11 Then
                                InnerTdSetting(tdInner, "", "Left")
                                lbl.Text = "Sub Total"
                            ElseIf i = 12 Then
                                InnerTdSetting(tdInner, "", "Left")
                                lbl.Text = "Sub Total"
                            ElseIf i = 13 Then
                                InnerTdSetting(tdInner, "", "Left")
                                lbl.Text = "Total"
                            Else
                                InnerTdSetting(tdInner, "", "Center")
                                lbl.Text = "<b>" + i.ToString() + "</b>"
                            End If
                            lbl.Font.Bold = True
                            lbl.Width = 60
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label()
                            If i = 11 Then
                                InnerTdSetting(tdInner, "", "Left")
                                lbl.Text = "Direct Materials"
                                lbl.CssClass = "CalculatedFeilds"
                            ElseIf i = 12 Then
                                InnerTdSetting(tdInner, "", "Left")
                                lbl.Text = "Discrete Materials"
                                lbl.CssClass = "CalculatedFeilds"
                            ElseIf i = 13 Then
                                InnerTdSetting(tdInner, "", "Left")
                                lbl.Text = "Materials"
                                lbl.CssClass = "CalculatedFeilds"
                            Else
                                If ds.Tables(0).Rows(0).Item("MATDES" + i.ToString() + "").ToString() <> "" Then
                                    lbl.Text = ds.Tables(0).Rows(0).Item("MATDES" + i.ToString() + "").ToString()
                                Else
                                    lbl.Text = ds.Tables(0).Rows(0).Item("MATS" + i.ToString() + "").ToString()
                                End If
                                'lbl.Text = ds.Tables(0).Rows(0).Item("MATS" + i.ToString() + "").ToString()
                            End If
                            lbl.Width = 170

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            'lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("SG" + i.ToString() + "").ToString(), 2)
                            If i = 12 Then
                                lbl.Text = ""
                            ElseIf i = 13 Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("SG11").ToString(), 2)
                                lbl.CssClass = "CalculatedFeilds"

                            Else
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("SG" + i.ToString() + "").ToString(), 2)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            'lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("W" + i.ToString() + "").ToString(), 3)
                            If i = 12 Then
                                lbl.Text = ""
                            ElseIf i = 13 Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("W11").ToString(), 3)
                            Else
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("W" + i.ToString() + "").ToString(), 3)
                            End If
                            lbl.CssClass = "CalculatedFeilds"
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            'lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("P" + i.ToString() + "").ToString(), 2)
                            If i = 12 Then
                                lbl.Text = ""
                            ElseIf i = 13 Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("P11").ToString(), 2)
                                lbl.CssClass = "CalculatedFeilds"

                            Else
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("P" + i.ToString() + "").ToString(), 2)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            
                            'lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PUR" + i.ToString() + "").ToString(), 0)
                            If i = 12 Then

                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("DISCRETETOTWT").ToString(), 0)
                                lbl.CssClass = "CalculatedFeilds"
                            ElseIf i = 13 Then
                                'lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PUR11").ToString(), 0)
                                lbl.Text = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("DISCRETETOTWT").ToString()) + CDbl(ds.Tables(0).Rows(0).Item("PUR11").ToString()), 0)
                                lbl.CssClass = "CalculatedFeilds"

                            Else
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PUR" + i.ToString() + "").ToString(), 0)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            'lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PURZ" + i.ToString() + "").ToString(), 0)
                            If i = 12 Then
                                'lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("FINVOLMUNITS").ToString() * ds.Tables(0).Rows(0).Item("DISCRETECOST").ToString() * ds.Tables(0).Rows(0).Item("CURR").ToString(), 0)
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("DISCRETETOTCST").ToString(), 0)
                                lbl.CssClass = "CalculatedFeilds"

                            ElseIf i = 13 Then
                                'Dim Total As Int64
                                'Dim Total2 As Int64
                                'Total = FormatNumber(ds.Tables(0).Rows(0).Item("FINVOLMUNITS").ToString() * ds.Tables(0).Rows(0).Item("DISCRETECOST").ToString() * ds.Tables(0).Rows(0).Item("CURR").ToString(), 0)
                                'Total2 = ds.Tables(0).Rows(0).Item("PURZ11").ToString()
                                'lbl.Text = FormatNumber(Total + Total2, 0)
                                lbl.Text = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("DISCRETETOTCST").ToString()) + (ds.Tables(0).Rows(0).Item("PURZ11").ToString()), 0)
                                lbl.CssClass = "CalculatedFeilds"

                            Else

                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PURZ" + i.ToString() + "").ToString(), 0)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            'If FormatNumber(ds.Tables(0).Rows(0).Item("PURZ" + i.ToString() + "").ToString(), 0) > 0 Then
                            '    lbl.Text = FormatNumber((ds.Tables(0).Rows(0).Item("PURZ" + i.ToString() + "").ToString() / ds.Tables(0).Rows(0).Item("PUR" + i.ToString() + "").ToString()), 3)
                            'Else
                            '    lbl.Text = "na"
                            'End If
                            If i = 12 Then
                                lbl.Text = ""
                            ElseIf i = 13 Then
                                If FormatNumber(ds.Tables(0).Rows(0).Item("PURZ11").ToString(), 0) > 0 Then
                                    lbl.Text = FormatNumber((ds.Tables(0).Rows(0).Item("PURZ11").ToString() / ds.Tables(0).Rows(0).Item("PUR11").ToString()), 3)
                                Else
                                    lbl.Text = "na"
                                End If
                                lbl.CssClass = "CalculatedFeilds"

                            Else
                                If FormatNumber(ds.Tables(0).Rows(0).Item("PURZ" + i.ToString() + "").ToString(), 0) > 0 Then
                                    lbl.Text = FormatNumber((ds.Tables(0).Rows(0).Item("PURZ" + i.ToString() + "").ToString() / ds.Tables(0).Rows(0).Item("PUR" + i.ToString() + "").ToString()), 3)
                                Else
                                    lbl.Text = "na"
                                End If
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 9
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            'lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PUN" + i.ToString() + "").ToString(), 3)
                            If i = 12 Then
                                lbl.Text = ""
                            ElseIf i = 13 Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PUN11").ToString(), 3)
                                lbl.CssClass = "CalculatedFeilds"

                            Else
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PUN" + i.ToString() + "").ToString(), 3)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                            'Case 9
                            '    InnerTdSetting(tdInner, "", "Right")
                            '    lbl = New Label()
                            '    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PUN" + i.ToString() + "").ToString(), 3)
                            '    tdInner.Controls.Add(lbl)
                            '    trInner.Controls.Add(tdInner)
                        Case 10
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            'If ds.Tables(0).Rows(0).Item("PROD" + i.ToString() + "").ToString() <> "" Then
                            '    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PROD" + i.ToString() + "").ToString(), 0)
                            'End If
                            If i = 12 Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("DISCRETETOTWT").ToString(), 0)
                                lbl.CssClass = "CalculatedFeilds"
                            ElseIf i = 13 Then
                                If ds.Tables(0).Rows(0).Item("PROD11").ToString() <> "" Then
                                    '  lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PROD11").ToString(), 0)
                                    lbl.Text = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("DISCRETETOTWT").ToString()) + CDbl(ds.Tables(0).Rows(0).Item("PROD11").ToString()), 0)

                                End If
                                lbl.CssClass = "CalculatedFeilds"

                            Else
                                If ds.Tables(0).Rows(0).Item("PROD" + i.ToString() + "").ToString() <> "" Then
                                    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PROD" + i.ToString() + "").ToString(), 0)
                                End If
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 11
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            'If ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString() <> "" Then
                            '    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString(), 0)
                            'End If
                            If i = 12 Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("DISCRETETOTCST").ToString(), 0)
                                lbl.CssClass = "CalculatedFeilds"
                            ElseIf i = 13 Then
                                If ds.Tables(0).Rows(0).Item("PRODZ11").ToString() <> "" Then
                                    'lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PRODZ11").ToString(), 0)
                                    lbl.Text = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("DISCRETETOTCST").ToString()) + CDbl(ds.Tables(0).Rows(0).Item("PRODZ11").ToString()), 0)

                                End If
                                lbl.CssClass = "CalculatedFeilds"

                            Else

                                If ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString() <> "" Then
                                    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PRODZ" + i.ToString() + "").ToString(), 0)
                                End If
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 12
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            hid = New HiddenField
                            hid.ID = "hidWaste" + i.ToString()
                            'If ds.Tables(0).Rows(0).Item("WASTE" + i.ToString()).ToString() <> "" Then
                            '    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WASTE" + i.ToString()).ToString(), 0)
                            'End If
                            If i = 12 Then
                                lbl.Text = ""
                                hid.Value = ""
                            ElseIf i = 13 Then
                                If ds.Tables(0).Rows(0).Item("WASTE11").ToString() <> "" Then
                                    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WASTE11").ToString(), 0)
                                End If
                                hid.Value = ds.Tables(0).Rows(0).Item("WASTE11").ToString()
                                lbl.CssClass = "CalculatedFeilds"

                            Else

                                If ds.Tables(0).Rows(0).Item("WASTE" + i.ToString()).ToString() <> "" Then
                                    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WASTE" + i.ToString()).ToString(), 0)
                                End If
                                hid.Value = ds.Tables(0).Rows(0).Item("WASTE" + i.ToString()).ToString()
                            End If
                            tdInner.Controls.Add(lbl)
                            tdInner.Controls.Add(hid)
                            trInner.Controls.Add(tdInner)
                        Case 13
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            hid = New HiddenField
                            hid.ID = "hidWaste" + i.ToString()
                            'If ds.Tables(0).Rows(0).Item("WASTEZ" + i.ToString()).ToString() <> "" Then
                            '    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WASTEZ" + i.ToString()).ToString(), 0)
                            'End If
                            If i = 12 Then
                                lbl.Text = ""
                                hid.Value = ""
                            ElseIf i = 13 Then
                                If ds.Tables(0).Rows(0).Item("WASTEZ11").ToString() <> "" Then
                                    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WASTEZ11").ToString(), 0)
                                End If
                                hid.Value = ds.Tables(0).Rows(0).Item("WASTEZ11").ToString()
                                lbl.CssClass = "CalculatedFeilds"

                            Else

                                If ds.Tables(0).Rows(0).Item("WASTEZ" + i.ToString()).ToString() <> "" Then
                                    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WASTEZ" + i.ToString()).ToString(), 0)
                                End If
                                hid.Value = ds.Tables(0).Rows(0).Item("WASTEZ" + i.ToString()).ToString()
                            End If
                            tdInner.Controls.Add(lbl)
                            tdInner.Controls.Add(hid)
                            trInner.Controls.Add(tdInner)

                        Case 14
                            Dim wasteP As String = ""
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            hid = New HiddenField
                            hid.ID = "hidPWaste" + i.ToString()

                            'If i <> 11 Then
                            '    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PWASTE" + i.ToString()).ToString(), 2)
                            'Else

                            '    If ds.Tables(0).Rows(0).Item("PUR" + i.ToString()).ToString() <> "" And ds.Tables(0).Rows(0).Item("PUR" + i.ToString()).ToString() <> 0 Then
                            '        wasteP = (ds.Tables(0).Rows(0).Item("WASTE" + i.ToString()).ToString() / ds.Tables(0).Rows(0).Item("PUR" + i.ToString()).ToString() * 100)
                            '        lbl.Text = FormatNumber(wasteP, 2)
                            '    Else
                            '        lbl.Text = FormatNumber(0, 2)
                            '    End If

                            'End If
                            If i = 12 Then
                                lbl.Text = ""
                                hid.Value = ""
                            ElseIf i = 13 Then
                                If ds.Tables(0).Rows(0).Item("PUR11").ToString() <> "" And ds.Tables(0).Rows(0).Item("PUR11").ToString() <> 0 Then
                                    wasteP = (ds.Tables(0).Rows(0).Item("WASTE11").ToString() / ds.Tables(0).Rows(0).Item("PUR11").ToString() * 100)
                                    lbl.Text = FormatNumber(wasteP, 2)
                                Else
                                    lbl.Text = FormatNumber(0, 2)
                                End If
                                'hid.Value = ds.Tables(0).Rows(0).Item("PWASTE11").ToString()
                                lbl.CssClass = "CalculatedFeilds"

                            Else
                                If i <> 11 Then
                                    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PWASTE" + i.ToString()).ToString(), 2)
                                Else

                                    If ds.Tables(0).Rows(0).Item("PUR" + i.ToString()).ToString() <> "" And ds.Tables(0).Rows(0).Item("PUR" + i.ToString()).ToString() <> 0 Then
                                        wasteP = (ds.Tables(0).Rows(0).Item("WASTE" + i.ToString()).ToString() / ds.Tables(0).Rows(0).Item("PUR" + i.ToString()).ToString() * 100)
                                        lbl.Text = FormatNumber(wasteP, 2)
                                    Else
                                        lbl.Text = FormatNumber(0, 2)
                                    End If

                                End If
                            End If

                            hid.Value = wasteP
                            tdInner.Controls.Add(lbl)
                            tdInner.Controls.Add(hid)
                            trInner.Controls.Add(tdInner)

                        Case 15
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
 txt.Width = 80
                            txt.ID = "C" + i.ToString()
                            'If i <> 11 Then
                            '    If ds.Tables(0).Rows(0).Item("CREDIT" + i.ToString() + "").ToString() <> "" Then
                            '        txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("CREDIT" + i.ToString() + "").ToString(), 3)
                            '    Else
                            '        txt.Text = ""
                            '    End If
                            '    txt.MaxLength = 6
                            '    tdInner.Controls.Add(txt)
                            'Else
                            '    tdInner.Text = FormatNumber(ds.Tables(0).Rows(0).Item("CREDIT" + i.ToString() + "").ToString(), 3)
                            'End If
                            If i = 12 Then
                                txt.Text = ""
                            ElseIf i = 13 Then
                                tdInner.Text = FormatNumber(ds.Tables(0).Rows(0).Item("CREDIT11").ToString(), 3)
                                tdInner.Font.Bold = True
                            ElseIf i = 11 Then
                                tdInner.Text = FormatNumber(ds.Tables(0).Rows(0).Item("CREDIT" + i.ToString() + "").ToString(), 3)
                            Else
                                If ds.Tables(0).Rows(0).Item("CREDIT" + i.ToString() + "").ToString() <> "" Then
                                    txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("CREDIT" + i.ToString() + "").ToString(), 3)
                                Else
                                    txt.Text = ""
                                End If
                                txt.MaxLength = 9
                                tdInner.Controls.Add(txt)
                            End If
                            trInner.Controls.Add(tdInner)
                        Case 16
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
 txt.Width = 80
                            txt.ID = "Prc" + i.ToString()
                            'If i <> 11 Then
                            '    If ds.Tables(0).Rows(0).Item("PRICE" + i.ToString() + "").ToString() <> "" Then
                            '        txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PRICE" + i.ToString() + "").ToString(), 3)
                            '    Else
                            '        txt.Text = ""
                            '    End If
                            '    txt.MaxLength = 6
                            '    tdInner.Controls.Add(txt)
                            'Else
                            '    tdInner.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PRICE" + i.ToString() + "").ToString(), 3)
                            'End If
                            If i = 12 Then
                                txt.Text = ""
                            ElseIf i = 13 Then
                                tdInner.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PRICE11").ToString(), 3)
                                tdInner.Font.Bold = True
                            ElseIf i = 11 Then
                                tdInner.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PRICE" + i.ToString() + "").ToString(), 3)
                            Else
                                If ds.Tables(0).Rows(0).Item("PRICE" + i.ToString() + "").ToString() <> "" Then
                                    txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PRICE" + i.ToString() + "").ToString(), 3)
                                Else
                                    txt.Text = ""
                                End If
                                txt.MaxLength = 9
                                tdInner.Controls.Add(txt)

                            End If
                            trInner.Controls.Add(tdInner)
                        Case 17
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            'If ds.Tables(0).Rows(0).Item("Tcredit" + i.ToString() + "").ToString() <> "" Then
                            '    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("Tcredit" + i.ToString() + "").ToString(), 0)
                            'Else
                            '    lbl.Text = ""
                            'End If
                            If i = 12 Then
                                lbl.Text = ""
                                hid.Value = ""
                            ElseIf i = 13 Then
                                If ds.Tables(0).Rows(0).Item("Tcredit11").ToString() <> "" Then
                                    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("Tcredit11").ToString(), 0)
                                Else
                                    lbl.Text = ""
                                End If
                                lbl.CssClass = "CalculatedFeilds"

                            Else

                                If ds.Tables(0).Rows(0).Item("Tcredit" + i.ToString() + "").ToString() <> "" Then
                                    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("Tcredit" + i.ToString() + "").ToString(), 0)
                                Else
                                    lbl.Text = ""
                                End If
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 18
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label()
                            'If ds.Tables(0).Rows(0).Item("NPURZ" + i.ToString() + "").ToString() <> "" Then
                            '    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("NPURZ" + i.ToString() + "").ToString(), 0)
                            'Else
                            '    lbl.Text = ""
                            'End If
                            If i = 12 Then
                                ' lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("FINVOLMUNITS").ToString() * ds.Tables(0).Rows(0).Item("DISCRETECOST").ToString() * ds.Tables(0).Rows(0).Item("CURR").ToString(), 0)
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("DISCRETETOTCST").ToString(), 0)
                                lbl.CssClass = "CalculatedFeilds"
                            ElseIf i = 13 Then
                                'Dim Total As Int64
                                'Dim Total2 As Int64
                                'Total = FormatNumber(ds.Tables(0).Rows(0).Item("FINVOLMUNITS").ToString() * ds.Tables(0).Rows(0).Item("DISCRETECOST").ToString() * ds.Tables(0).Rows(0).Item("CURR").ToString(), 0)
                                'Total2 = ds.Tables(0).Rows(0).Item("NPURZ11").ToString()
                                'lbl.Text = FormatNumber(Total + Total2, 0)
                                lbl.Text = FormatNumber(CDbl(ds.Tables(0).Rows(0).Item("DISCRETETOTCST").ToString()) + CDbl(ds.Tables(0).Rows(0).Item("NPURZ11").ToString()), 0)


                                lbl.CssClass = "CalculatedFeilds"

                            Else

                                If ds.Tables(0).Rows(0).Item("NPURZ" + i.ToString() + "").ToString() <> "" Then
                                    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("NPURZ" + i.ToString() + "").ToString(), 0)
                                Else
                                    lbl.Text = ""
                                End If
                            End If
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
                    trInner.ForeColor = Drawing.Color.Black
                    trInner.Font.Bold = True
                End If

                tblComparision.Controls.Add(trInner)
            Next
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetDiscreteMat()
        Dim objGetData As New E1GetData.Selectdata
        Dim objUpdateData As New E1UpInsData.UpdateInsert
        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        Dim tdHeader2 As TableCell

        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell

        Dim dsPref As New DataSet
        Dim dsSug As New DataSet
        Dim DsDiscMat As New DataSet()
        Dim dspl As New DataSet
        Dim dspreferences As New DataSet
        Dim ds As New DataSet
        ' dspreferences = objGetData.GetPref(CaseId)
        ds = objGetData.GetExtrusionOutDetails(CaseId)
        DsDiscMat = objGetData.GetDiscMaterials("-1", "")
        'dsPref = objGetData.GetExtrusionDetailsBarrP(CaseId)
        ' dsSug = objGetData.GetExtrusionDetailsBarrS(CaseId)
        ' dspl = objGetData.GetProfitAndLossDetails(CaseId, True)
        Try

            'Discrete Materails
            trHeader = New TableRow
            trHeader2 = New TableRow
            For i = 1 To 4
                tdHeader = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "90px", "Discrete Materials", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        HeaderTdSetting(tdHeader, "120px", "Material", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + ")"
                        HeaderTdSetting(tdHeader, "85px", "Purchase", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)

                End Select

            Next
            trHeader.Height = 30
            trHeader.Height = 30

            tblComparision2.Controls.Add(trHeader)
            tblComparision2.Controls.Add(trHeader2)

            'Inner
            For i = 1 To 3
                trInner = New TableRow
                For j = 1 To 5
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            'Layer
                            InnerTdSetting(tdInner, "", "left")
                            If i = 1 Then
                                tdInner.Text = "<b>" + "Discrete1" + "</b>"
                            ElseIf i = 2 Then
                                tdInner.Text = "<b>" + "Discrete2" + "</b>"
                            ElseIf i = 3 Then
                                tdInner.Text = "<b>" + "Discrete3" + "</b>"
                            End If

                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            GetDiscreteMaterialDetails(lbl, hid, CInt(ds.Tables(0).Rows(0).Item("DISID" + i.ToString() + "").ToString()), DsDiscMat)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("DISP" + i.ToString() + "").ToString() * ds.Tables(0).Rows(0).Item("FINVOLMUNITS").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)


                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30
                tblComparision2.Controls.Add(trInner)
            Next
            'Total
            trInner = New TableRow
            For i = 1 To 3
                tdInner = New TableCell
                Select Case i
                    Case 1
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.Text = "<span class='CalculatedFeilds'><b> Total </b></span>"
                        trInner.Controls.Add(tdInner)
                    Case 3
                        InnerTdSetting(tdInner, "", "Right")
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("FINVOLMUNITS").ToString() * ds.Tables(0).Rows(0).Item("DISCRETECOST").ToString() * ds.Tables(0).Rows(0).Item("CURR").ToString(), 0) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 2
                        InnerTdSetting(tdInner, "", "Left")
                        trInner.Controls.Add(tdInner)


                End Select
            Next
            trInner.Height = 30
            trInner.CssClass = "AlterNateColor1"
            tblComparision2.Controls.Add(trInner)

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetDiscreteMaterialDetails(ByRef lbl As Label, ByVal hid As HiddenField, ByVal MatDisId As Integer, ByVal dsDmat As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvDmat As New DataView
        Dim dtDmat As New DataTable
        Try

            dvDmat = dsDmat.Tables(0).DefaultView
            dvDmat.RowFilter = "MATDISID = " + MatDisId.ToString()
            dtDmat = dvDmat.ToTable()

            lbl.Text = dtDmat.Rows(0).Item("matDISde1").ToString()
            lbl.ToolTip = dtDmat.Rows(0).Item("matDISde1").ToString()
            lbl.Attributes.Add("text-decoration", "none")



        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
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

 
    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim ObjUpIns As New E1UpInsData.UpdateInsert()
        Dim Credit(9) As String
        Dim Price(9) As String
        Try
            If Not objRefresh.IsRefresh Then
                For i = 1 To 10
                    Credit(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$C" + i.ToString() + "")
                    Price(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$Prc" + i.ToString() + "")                    
                Next
            End If
            ObjUpIns.UpdateCreditInput(CaseId, Credit, Price)
            Calculate()
            'Update Server Date
            ObjUpIns.ServerDateUpdate(CaseId, Session("E1UserName"))
            GetPageDetails()
            GetDiscreteMat()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub Calculate()
        Try
            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim obj As New EconCalculation.EconCalculation(Econ1Conn)
            obj.EconCalculate(CaseId)
        Catch ex As Exception
            ErrorLable.Text = "Error:Calculate:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class
