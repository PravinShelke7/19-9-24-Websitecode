Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S1GetData
Imports S1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain1_IntResults_ResultsEOLIN
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
        MainHeading.Attributes.Add("onmouseover", "Tip('Material Balance Statement (Intermediate)')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Material Balance Statement (Intermediate) "
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Sustain1ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_SUSTAIN1_INTRESULTS_RESULTSEOLIN")
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
            CaseId = Session("S1CaseId")
            UserRole = Session("S1UserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New S1GetData.Selectdata
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
            ds = objGetData.GetResultsEOLIN(CaseId)
            If ds.Tables(0).Rows(0).Item("SALESUNIT1").ToString() = "" Then
                lblSalesVol.Text = "<b>Sales Volume (" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + "):</b>  " + FormatNumber(ds.Tables(0).Rows(0).Item("SALESUNITVAL1").ToString(), 0).ToString()
            Else
                lblSalesVol.Text = "<b>Sales Volume (" + ds.Tables(0).Rows(0).Item("SALESUNIT1").ToString() + "):</b> " + FormatNumber(ds.Tables(0).Rows(0).Item("SALESUNITVAL1").ToString(), 0).ToString()
            End If

            lblSalesVolUnit.Text = "<b>Sales Volume (" + ds.Tables(0).Rows(0).Item("SALESUNIT2").ToString() + "):</b> " + FormatNumber(ds.Tables(0).Rows(0).Item("SALESUNITVAL2").ToString(), 0).ToString()

            'calculations
            trHeader = New TableRow
            trHeader2 = New TableRow
            trHeader1 = New TableRow

            For i = 1 To 5
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "140px", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8") + Title.ToString() + ")"
                        HeaderTdSetting(tdHeader, "", "Material Balance", "1")
                        Header2TdSetting(tdHeader2, "90px", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        HeaderTdSetting(tdHeader, "", "Material to Recycling", "1")
                        Header2TdSetting(tdHeader2, "90px", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        HeaderTdSetting(tdHeader, "100px", "Material to Incineration", "1")
                        Header2TdSetting(tdHeader2, "100px", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 5
                        HeaderTdSetting(tdHeader, "100px", "Material to Composting", "1")
                        Header2TdSetting(tdHeader2, "100px", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)

                End Select

            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader2)


            For i = 1 To 13
                trInner = New TableRow()
                For j = 1 To 5
                    tdInner = New TableCell()
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Left")
                            If i = 1 Or i = 5 Or i = 8 Or i = 11 Then
                                tdInner.Text = "<span style='font-weight:normal;'>" + ds.Tables(0).Rows(0).Item("PLANTSPACE" + i.ToString() + "").ToString() + "</span>"
                            Else
                                tdInner.Text = "<span style='margin-left:15px;'>" + ds.Tables(0).Rows(0).Item("PLANTSPACE" + i.ToString() + "").ToString() + "</span>"
                            End If
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i > 1 And i < 5 Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MATB" + (i - 1).ToString() + "").ToString(), 0)
                            ElseIf i > 5 And i < 8 Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MATB" + (i - 2).ToString() + "").ToString(), 0)
                            ElseIf i > 8 And i < 11 Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MATB" + (i - 3).ToString() + "").ToString(), 0)
                            ElseIf i > 11 Then
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MATB" + (i - 4).ToString() + "").ToString(), 0)
                            Else
                                lbl.Text = ""
                            End If

                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Center")
                            If i > 2 And i < 5 Then
                                txt = New TextBox
                                txt.CssClass = "SmallTextBox"
                                txt.Width = 50
                                txt.ID = "MALRE" + (i - 1).ToString()
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MALRE" + (i - 1).ToString() + "").ToString(), 3)
                                txt.MaxLength = 6
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            ElseIf i > 6 And i < 8 Then
                                txt = New TextBox
                                txt.CssClass = "SmallTextBox"
                                txt.Width = 50
                                txt.ID = "MALRE" + (i - 2).ToString()
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MALRE" + (i - 2).ToString() + "").ToString(), 3)
                                txt.MaxLength = 6
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            ElseIf i > 9 And i < 11 Then
                                txt = New TextBox
                                txt.CssClass = "SmallTextBox"
                                txt.Width = 50
                                txt.ID = "MALRE" + (i - 3).ToString()
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MALRE" + (i - 3).ToString() + "").ToString(), 3)
                                txt.MaxLength = 6
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            ElseIf i > 11 And i <= 13 Then
                                txt = New TextBox
                                txt.CssClass = "SmallTextBox"
                                txt.Width = 50
                                txt.ID = "MALRE" + (i - 4).ToString()
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MALRE" + (i - 4).ToString() + "").ToString(), 3)
                                txt.MaxLength = 6
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Else
                                InnerTdSetting(tdInner, "", "right")
                                lbl = New Label
                                lbl.CssClass = "NormalLable"
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            End If
                        Case 4
                            InnerTdSetting(tdInner, "", "Center")
                            If i > 2 And i < 5 Then
                                txt = New TextBox
                                txt.CssClass = "SmallTextBox"
                                txt.Width = 50
                                txt.ID = "MALIN" + (i - 1).ToString()
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MALIN" + (i - 1).ToString() + "").ToString(), 3)
                                txt.MaxLength = 6
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            ElseIf i > 6 And i < 8 Then
                                txt = New TextBox
                                txt.CssClass = "SmallTextBox"
                                txt.Width = 50
                                txt.ID = "MALIN" + (i - 2).ToString()
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MALIN" + (i - 2).ToString() + "").ToString(), 3)
                                txt.MaxLength = 6
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            ElseIf i > 9 And i < 11 Then
                                txt = New TextBox
                                txt.CssClass = "SmallTextBox"
                                txt.Width = 50
                                txt.ID = "MALIN" + (i - 3).ToString()
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MALIN" + (i - 3).ToString() + "").ToString(), 3)
                                txt.MaxLength = 6
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            ElseIf i > 11 And i <= 13 Then
                                txt = New TextBox
                                txt.CssClass = "SmallTextBox"
                                txt.Width = 50
                                txt.ID = "MALIN" + (i - 4).ToString()
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MALIN" + (i - 4).ToString() + "").ToString(), 3)
                                txt.MaxLength = 6
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Else
                                InnerTdSetting(tdInner, "", "right")
                                lbl = New Label
                                lbl.CssClass = "NormalLable"
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            End If
                        Case 5
                            InnerTdSetting(tdInner, "", "Center")
                            If i > 2 And i < 5 Then
                                txt = New TextBox
                                txt.CssClass = "SmallTextBox"
                                txt.Width = 50
                                txt.ID = "MALCM" + (i - 1).ToString()
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MALCM" + (i - 1).ToString() + "").ToString(), 3)
                                txt.MaxLength = 6
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            ElseIf i > 6 And i < 8 Then
                                txt = New TextBox
                                txt.CssClass = "SmallTextBox"
                                txt.Width = 50
                                txt.ID = "MALCM" + (i - 2).ToString()
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MALCM" + (i - 2).ToString() + "").ToString(), 3)
                                txt.MaxLength = 6
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            ElseIf i > 9 And i < 11 Then
                                txt = New TextBox
                                txt.CssClass = "SmallTextBox"
                                txt.Width = 50
                                txt.ID = "MALCM" + (i - 3).ToString()
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MALCM" + (i - 3).ToString() + "").ToString(), 3)
                                txt.MaxLength = 6
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            ElseIf i > 11 And i <= 13 Then
                                txt = New TextBox
                                txt.CssClass = "SmallTextBox"
                                txt.Width = 50
                                txt.ID = "MALCM" + (i - 4).ToString()
                                txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("MALCM" + (i - 4).ToString() + "").ToString(), 3)
                                txt.MaxLength = 6
                                tdInner.Controls.Add(txt)
                                trInner.Controls.Add(tdInner)
                            Else
                                InnerTdSetting(tdInner, "", "right")
                                lbl = New Label
                                lbl.CssClass = "NormalLable"
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            End If

                    End Select

                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                tblComparision.Controls.Add(trInner)
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

        Dim MALRE(8) As String
        Dim MALIN(8) As String
        Dim MALCM(8) As String
        Dim obj As New CryptoHelper
        Dim i As New Integer
        Dim ObjUpIns As New S1UpInsData.UpdateInsert()
        Try
            If Not objRefresh.IsRefresh Then
                For i = 0 To 8
                    MALRE(i) = Request.Form("ctl00$Sustain1ContentPlaceHolder$MALRE" + (i + 1).ToString() + "")
                    MALIN(i) = Request.Form("ctl00$Sustain1ContentPlaceHolder$MALIN" + (i + 1).ToString() + "")
                    MALCM(i) = Request.Form("ctl00$Sustain1ContentPlaceHolder$MALCM" + (i + 1).ToString() + "")
                    'Check For IsNumric
                    If MALRE(i) Is Nothing Then
                    Else
                        If Not IsNumeric(MALRE(i)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                    End If

                    If MALIN(i) Is Nothing Then
                    Else
                        If Not IsNumeric(MALIN(i)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                    End If

                    If MALCM(i) Is Nothing Then
                    Else
                        If Not IsNumeric(MALCM(i)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                    End If
                   
                Next
                ObjUpIns.EndOfLifeUpdate(CaseId, MALRE, MALIN, MALCM)
                Calculate()
                'Update Server Date
                ObjUpIns.ServerDateUpdate(CaseId, Session("S1UserName"))
            End If
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub Calculate()
        Try
            Dim Sustain1Conn As String = System.Configuration.ConfigurationManager.AppSettings("Sustain1ConnectionString")
            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim obj As New SustainCalculation.SustainCalculations(Sustain1Conn, Econ1Conn)
            obj.SustainCalculate(CaseId)
        Catch ex As Exception
            ErrorLable.Text = "Error:Calculate:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class
