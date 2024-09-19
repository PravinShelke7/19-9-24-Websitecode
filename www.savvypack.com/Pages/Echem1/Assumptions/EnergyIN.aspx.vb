Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Echem1GetData
Imports Echem1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Echem1_Assumptions_EnergyIN
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
        Updatebtn.Attributes.Add("onclick", "return checkNumericAll();")
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
        Chartbtn.OnClientClick = "return Chart('../../../Charts/EnergyPrice.aspx','EnergyPriceChart');"
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetFeedbackbtn()
        FeedBackbtn = Page.Master.FindControl("imgFeedback")
        FeedBackbtn.Visible = True
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetNotesbtn()
        Notesbtn = Page.Master.FindControl("imgNotes")
        Notesbtn.Visible = True
        Notesbtn.OnClientClick = "return Notes('ERGYASSUM');"
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Plant Energy Assumptions')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Echem1 - Plant Energy Assumptions"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Echem1ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_ECHEM1_ASSUMPTIONS_ENERGYIN")
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
        Dim tdInner As New TableCell

        Try
            ds = objGetData.GetEnergyDetails(CaseId)
            For i = 1 To 3
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell

                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "220px", "Energy Type", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "", "Energy Prices", "2")
                        Header2TdSetting(tdHeader2, "90px", "Suggested", "1")

                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        Header2TdSetting(tdHeader2, "90px", "Preferred", "1")

                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)


                End Select
            Next
            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader1)



            'Inner()
            For i = 1 To 2
                trInner = New TableRow
                For j = 1 To 3
                    tdInner = New TableCell
                    Dim Title As String = String.Empty
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "left")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Then
                                Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/kwh)"
                                lbl.Text = "Electricity" + Title
                            Else
                                Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/Mcf)"
                                lbl.Text = "Natural Gas" + Title
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 2
                            InnerTdSetting(tdInner, "", "Center")
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("ERGPSUG" + i.ToString() + "").ToString(), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 3
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "ERGPPREF" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("ERGPPREF" + i.ToString() + "").ToString(), 4)
                            txt.MaxLength = 12
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)


                    End Select
                Next

                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                tblComparision.Controls.Add(trInner)
            Next

            'Energy  calculations
            trHeader = New TableRow
            trHeader2 = New TableRow
            trHeader1 = New TableRow

            For i = 1 To 8
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "90px", "Space Type", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        HeaderTdSetting(tdHeader, "", "Energy Requirements", "2")
                        Header2TdSetting(tdHeader2, "75px", "Electricity", "1")
                        Header2TdSetting(tdHeader1, "75px", "(kwh)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE4") + "/" + ds.Tables(0).Rows(0).Item("TITLE7") + ")"
                        Header2TdSetting(tdHeader2, "75px", "Natural Gas", "1")
                        Header2TdSetting(tdHeader1, "75px", "(cubic ft)", "1")
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        Title = "(" + ds.Tables(0).Rows(0).Item("title2") + Title.ToString() + ")"
                        HeaderTdSetting(tdHeader, "", "Energy Cost", "2")
                        Header2TdSetting(tdHeader2, "75px", "Electricity", "1")
                        Header2TdSetting(tdHeader1, "75px", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 5
                        Title = "(" + ds.Tables(0).Rows(0).Item("title2") + Title.ToString() + ")"
                        Header2TdSetting(tdHeader2, "75px", "Natural Gas", "1")
                        Header2TdSetting(tdHeader1, "75px", Title, "1")
                        trHeader2.Controls.Add(tdHeader2)
                    Case 6
                        Title = "(" + ds.Tables(0).Rows(0).Item("title2") + Title.ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "Total", "1")
                        Header2TdSetting(tdHeader2, "100px", "", "1")
                        Header2TdSetting(tdHeader1, "100px", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 7
                        Title = "(" + ds.Tables(0).Rows(0).Item("title2") + Title.ToString() + ")"
                        HeaderTdSetting(tdHeader, "", "Energy Cost", "2")
                        Header2TdSetting(tdHeader2, "75px", "Variable", "1")
                        Header2TdSetting(tdHeader1, "75px", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 8
                        Title = "(" + ds.Tables(0).Rows(0).Item("title2") + Title.ToString() + ")"
                        Header2TdSetting(tdHeader2, "75px", "Fixed", "1")
                        Header2TdSetting(tdHeader1, "75px", Title, "1")
                        trHeader2.Controls.Add(tdHeader2)

                End Select
                trHeader1.Controls.Add(tdHeader1)
            Next
            tblComparision1.Controls.Add(trHeader)
            tblComparision1.Controls.Add(trHeader2)
            tblComparision1.Controls.Add(trHeader1)

            'Inner()
            For i = 1 To 4
                trInner = New TableRow
                For j = 1 To 8
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "left")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If i = 1 Then
                                lbl.Text = "Production"
                            ElseIf i = 2 Then
                                lbl.Text = "Warehouse"
                            ElseIf i = 3 Then
                                lbl.Text = "Office"
                            ElseIf i = 4 Then
                                lbl.Text = "Support"
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 2
                            InnerTdSetting(tdInner, "", "Center")
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("ENRGE" + i.ToString() + "").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 3
                            InnerTdSetting(tdInner, "", "Center")
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("ENRGN" + i.ToString() + "").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Center")
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("ENRGCE" + i.ToString() + "").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Center")
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("ENRGCG" + i.ToString() + "").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Center")
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("TOTAL" + i.ToString() + "").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Center")
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("VTOTAL" + i.ToString() + "").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "Center")
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("FTOTAL" + i.ToString() + "").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)


                    End Select
                Next

                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                tblComparision1.Controls.Add(trInner)
            Next
            'Total
            trInner = New TableRow
            For i = 1 To 8
                tdInner = New TableCell
                Select Case i
                    Case 1
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.Text = "<span class='CalculatedFeilds'><b> Total </b></span>"
                        trInner.Controls.Add(tdInner)
                    Case 2
                        InnerTdSetting(tdInner, "", "Right")
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("ENRGE5").ToString(), 0) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 3
                        InnerTdSetting(tdInner, "", "Right")
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("ENRGN5").ToString(), 0) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 4
                        InnerTdSetting(tdInner, "", "Right")
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("ENRGCE5").ToString(), 0) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 5
                        InnerTdSetting(tdInner, "", "Right")
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("ENRGCG5").ToString(), 0) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 6
                        InnerTdSetting(tdInner, "", "Right")
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("TOTAL5").ToString(), 0) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 7
                        InnerTdSetting(tdInner, "", "Right")
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("VTOTAL5").ToString(), 0) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 8
                        InnerTdSetting(tdInner, "", "Right")
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("FTOTAL5").ToString(), 0) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                End Select
            Next
            trInner.Height = 30
            trInner.CssClass = "AlterNateColor2"
            tblComparision1.Controls.Add(trInner)

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

        Dim ERGPPREF(1) As String
        Dim i As New Integer
        Dim ObjUpIns As New Echem1UpInsData.UpdateInsert()
        Dim obj As New CryptoHelper
        Try
            If Not objRefresh.IsRefresh Then
                For i = 0 To 1
                    ERGPPREF(i) = Request.Form("ctl00$Echem1ContentPlaceHolder$ERGPPREF" + (i + 1).ToString() + "").Replace(",", "")
                    'Check For IsNumric
                    If Not IsNumeric(ERGPPREF(i)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If
                Next
                ObjUpIns.EnergyUpdate(CaseId, ERGPPREF)
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
