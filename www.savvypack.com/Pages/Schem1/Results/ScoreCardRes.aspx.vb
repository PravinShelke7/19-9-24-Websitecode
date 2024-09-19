Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Schem1GetData
Imports Schem1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Schem1_Results_ScoreCardRes
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

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Scorecard Results')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Schem1 - Scorecard Results "
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Schem1ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_SCHEM1_RESULTS_SCORECARDRES")
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
            CaseId = Session("Schem1CaseId")
            UserRole = Session("Schem1UserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New Schem1GetData.Selectdata
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
            ds = objGetData.GetScoreCardResults(CaseId)



            trHeader = New TableRow
            trHeader2 = New TableRow
            trHeader1 = New TableRow

            For i = 1 To 10
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "160px", "Type", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "70px", "Raw Score", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "", "Low Range", "2")
                        Header2TdSetting(tdHeader1, "70px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        HeaderTdSetting(tdHeader, "", "High Range", "2")
                        Header2TdSetting(tdHeader1, "70px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 6
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 7
                        HeaderTdSetting(tdHeader, "", "Weighting(%)", "2")
                        Header2TdSetting(tdHeader1, "70px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 8
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 9
                        HeaderTdSetting(tdHeader, "60px", "Max Score", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 10
                        HeaderTdSetting(tdHeader, "70px", "Total Score", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                End Select

            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader1)

            'Inner
            For i = 1 To 9
                trInner = New TableRow
                For j = 1 To 10
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            lbl.Text = "<b>" + ds.Tables(0).Rows(0).Item("TYPE" + i.ToString() + "").ToString() + "</b>"
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            If i < 8 Then
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.CssClass = "NormalLable"
                                If i <> 1 Then
                                    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("RW" + i.ToString() + "").ToString(), 3)
                                Else
                                    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("RW" + i.ToString() + "").ToString(), 7)
                                End If
                                tdInner.Controls.Add(lbl)
                            Else
                                InnerTdSetting(tdInner, "", "Center")
                                txt = New TextBox
                                txt.CssClass = "SmallTextBox"
                                txt.Width = 70
                                txt.ID = "RW" + i.ToString()
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("RW" + i.ToString() + "").ToString(), 3)
                                txt.MaxLength = 8
                                tdInner.Controls.Add(txt)
                            End If



                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i <> 1 Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("LOWRANGES" + i.ToString() + "").ToString(), 4)
                            Else
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("LOWRANGES" + i.ToString() + "").ToString(), 7)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Right")
                            txt = New TextBox
                            txt.CssClass = "MediumTextBox"
                            txt.ID = "LOWP_" + i.ToString()
                            If i <> 1 Then
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("LOWRANGEP" + i.ToString() + "").ToString(), 4)
                                txt.MaxLength = 8
                            Else
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("LOWRANGEP" + i.ToString() + "").ToString(), 7)
                                txt.MaxLength = 14
                            End If
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i <> 1 Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("HIGHRANGES" + i.ToString() + "").ToString(), 4)
                            Else
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("HIGHRANGES" + i.ToString() + "").ToString(), 7)
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Right")
                            txt = New TextBox
                            txt.ID = "HIGHP_" + i.ToString()
                            txt.CssClass = "MediumTextBox"

                            If i <> 1 Then
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("HIGHRANGEP" + i.ToString() + "").ToString(), 4)
                                txt.MaxLength = 8
                            Else
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("HIGHRANGEP" + i.ToString() + "").ToString(), 7)
                                txt.MaxLength = 14
                            End If
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            'If i <> 1 Then
                            '    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WEIGHTS" + i.ToString() + "").ToString(), 4)
                            'Else
                            '    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WEIGHTS" + i.ToString() + "").ToString(), 7)
                            'End If
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WEIGHTS" + i.ToString() + "").ToString(), 2)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "Right")
                            txt = New TextBox
                            txt.CssClass = "MediumTextBox"
                            txt.ID = "WTP_" + i.ToString()
                            'If i <> 1 Then
                            '    txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WEIGHTP" + i.ToString() + "").ToString(), 4)
                            '    txt.MaxLength = 8
                            'Else
                            '    txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WEIGHTP" + i.ToString() + "").ToString(), 7)
                            '    txt.MaxLength = 14
                            'End If
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WEIGHTP" + i.ToString() + "").ToString(), 2)
                            txt.MaxLength = 8
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 9
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"

                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MX" + i.ToString() + "").ToString(), 3)

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 10
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("TOT" + i.ToString() + "").ToString(), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                trInner.Height = 20
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                tblComparision.Controls.Add(trInner)
            Next


            'Total
            For i = 1 To 1
                trInner = New TableRow
                For j = 1 To 10
                    tdInner = New TableCell
                    Select Case j
                        Case 1, 2
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            lbl.Text = "<b></b>"
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            lbl.Text = "<b></b>"
                            tdInner.ColumnSpan = 2
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            lbl.Text = "<b>Total Scorecard:</b>"
                            tdInner.ColumnSpan = 2
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Center")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            lbl.Text = "<b>" + FormatNumber(ds.Tables(0).Rows(0).Item("WEIGHTINGSPREFTOT").ToString(), 3) + "</b>"
                            tdInner.ColumnSpan = 2
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 9
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            lbl.Text = "<b>" + FormatNumber(ds.Tables(0).Rows(0).Item("MXT").ToString(), 3) + "</b>"
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 10
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            lbl.Text = "<b>" + FormatNumber(ds.Tables(0).Rows(0).Item("TOTT").ToString(), 3) + "</b>"
                            tdInner.ColumnSpan = 2
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                tblComparision.Controls.Add(trInner)
                trInner.CssClass = "AlterNateColor1"
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
        Dim LowRange(9) As String
        Dim HighRange(9) As String
        Dim Weightings(9) As String
        Dim RawStore(9) As String
        Dim objUpIns As New Schem1UpInsData.UpdateInsert()
        Dim obj As New CryptoHelper
        Dim i As New Integer
        Try
            If Not objRefresh.IsRefresh Then
                For i = 1 To 9
                    LowRange(i) = Request.Form("ctl00$Schem1ContentPlaceHolder$LOWP_" + i.ToString() + "").Replace(",", "")
                    HighRange(i) = Request.Form("ctl00$Schem1ContentPlaceHolder$HIGHP_" + i.ToString() + "").Replace(",", "")
                    Weightings(i) = Request.Form("ctl00$Schem1ContentPlaceHolder$WTP_" + i.ToString() + "").Replace(",", "")
                    'Check For IsNumric
                    If Not IsNumeric(LowRange(i)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If
                    If Not IsNumeric(HighRange(i)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If
                    If Not IsNumeric(Weightings(i)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If

                    If i > 7 Then
                        RawStore(i) = Request.Form("ctl00$Schem1ContentPlaceHolder$RW" + i.ToString() + "").Replace(",", "")
                        'Check For IsNumric
                        If Not IsNumeric(RawStore(i)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                    End If


                Next
                objUpIns.ScoreCardResUpdate(CaseId, LowRange, HighRange, Weightings, RawStore)
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
            Dim Sustain1Conn As String = System.Configuration.ConfigurationManager.AppSettings("Sustain1ConnectionString")
            Dim Echem1Conn As String = System.Configuration.ConfigurationManager.AppSettings("Echem1ConnectionString")
            Dim Schem1Conn As String = System.Configuration.ConfigurationManager.AppSettings("Schem1ConnectionString")
            Dim obj As New Schem1Calculation.Schem1Calculations(Schem1Conn, Echem1Conn, Sustain1Conn, Econ1Conn)
            obj.Schem1Calculate(CaseId)
        Catch ex As Exception
            ErrorLable.Text = "Error:Calculate:" + ex.Message.ToString() + ""
        End Try
    End Sub



End Class