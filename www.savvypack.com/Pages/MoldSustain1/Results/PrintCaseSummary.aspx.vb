Imports System.Data
Imports System.Data.OleDb
Imports System
Imports MoldS1GetData
Imports MoldS1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MoldSustain1_Results_PrintCaseSummary
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
        ' AddHandler Updatebtn.Click, AddressOf Update_Click
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
        MainHeading.InnerHtml = "Sustain1 Mold - Print Case Summary"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("MoldSustain1ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_SUSTAIN1_RESULTS_PRINTCASESUMMARY")
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
            CaseId = Session("MoldS1CaseId")
            UserRole = Session("MoldS1UserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Try
            Energy()
            GreenhouseGas()
            Water()
            MaterialAssumption()
            MaterialResults()
            EquipmentAssumption()
            OperationAssumption()
            PersonnelAssumption()
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub Energy()
        Dim ds As New DataSet
        Dim objGetData As New MoldS1GetData.Selectdata
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
            ds = objGetData.GetEnergyResults(CaseId)

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
                        HeaderTdSetting(tdHeader, "150px", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        Title = "(MJ)"
                        HeaderTdSetting(tdHeader, "150px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        HeaderTdSetting(tdHeader, "100px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        Header2TdSetting(tdHeader2, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        Title = "(MJ/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "By Weight", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 5
                        Title = "(" + ds.Tables(0).Rows(0).Item("SUNIT").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "By Volume", "1")
                        HeaderTdSetting(tdHeader1, "100px", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                End Select

            Next
            tblEnergy.Controls.Add(trHeader)
            tblEnergy.Controls.Add(trHeader2)


            'Inner
            For i = 1 To 12
                trInner = New TableRow
                For j = 1 To 5
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            lbl.Text = "<b>" + ds.Tables(0).Rows(0).Item("A" + i.ToString() + "").ToString() + "</b>"
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("T" + i.ToString() + "").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If CDbl(ds.Tables(0).Rows(0).Item("T8").ToString()) > 0 Then
                                lbl.Text = FormatNumber((CDbl(ds.Tables(0).Rows(0).Item("T" + i.ToString() + "").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("T8").ToString())) * 100, 3)
                            Else
                                lbl.Text = "na"
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString()) > 0 Then
                                lbl.Text = FormatNumber((CDbl(ds.Tables(0).Rows(0).Item("T" + i.ToString() + "").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString())), 3)
                            Else
                                lbl.Text = "na"
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If CDbl(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString()) > 0 Then
                                lbl.Text = FormatNumber((CDbl(ds.Tables(0).Rows(0).Item("T" + i.ToString() + "").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString())), 3)
                            Else
                                lbl.Text = "na"
                            End If
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
                If i = 8 Or i = 12 Then
                    trInner.CssClass = "TdHeading"
                End If
                tblEnergy.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GreenhouseGas()
        Dim ds As New DataSet
        Dim objGetData As New MoldS1GetData.Selectdata
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
            ds = objGetData.GetGhgResults(CaseId)



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
                        HeaderTdSetting(tdHeader, "150px", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "150px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        HeaderTdSetting(tdHeader, "100px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        Header2TdSetting(tdHeader2, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + " gr gas/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "By Weight", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 5
                        Title = "(" + ds.Tables(0).Rows(0).Item("SUNIT").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "By Volume", "1")
                        HeaderTdSetting(tdHeader1, "100px", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                End Select

            Next
            tblGHG.Controls.Add(trHeader)
            tblGHG.Controls.Add(trHeader2)


            'Inner
            For i = 1 To 12
                trInner = New TableRow
                For j = 1 To 5
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            lbl.Text = "<b>" + ds.Tables(0).Rows(0).Item("A" + i.ToString() + "").ToString() + "</b>"
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("T" + i.ToString() + "").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If CDbl(ds.Tables(0).Rows(0).Item("T8").ToString()) > 0 Then
                                lbl.Text = FormatNumber((CDbl(ds.Tables(0).Rows(0).Item("T" + i.ToString() + "").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("T8").ToString())) * 100, 2)
                            Else
                                lbl.Text = "na"
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString()) > 0 Then
                                lbl.Text = FormatNumber((CDbl(ds.Tables(0).Rows(0).Item("T" + i.ToString() + "").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString())), 3)
                            Else
                                lbl.Text = "na"
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If CDbl(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString()) > 0 Then
                                lbl.Text = FormatNumber((CDbl(ds.Tables(0).Rows(0).Item("T" + i.ToString() + "").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString())), 3)
                            Else
                                lbl.Text = "na"
                            End If
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
                If i = 8 Or i = 12 Then
                    trInner.CssClass = "TdHeading"
                End If
                tblGHG.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub Water()
        Dim ds As New DataSet
        Dim objGetData As New MoldS1GetData.Selectdata
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
            ds = objGetData.GetWaterResults(CaseId)



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
                        HeaderTdSetting(tdHeader, "150px", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE10").ToString() + ")"
                        HeaderTdSetting(tdHeader, "150px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        HeaderTdSetting(tdHeader, "100px", "Total", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        Header2TdSetting(tdHeader2, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE10").ToString() + " water/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "By Weight", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 5
                        Title = "(" + ds.Tables(0).Rows(0).Item("SUNIT").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "By Volume", "1")
                        HeaderTdSetting(tdHeader1, "100px", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                End Select

            Next
            tblWater.Controls.Add(trHeader)
            tblWater.Controls.Add(trHeader2)


            'Inner
            For i = 1 To 12
                trInner = New TableRow
                For j = 1 To 5
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            lbl.Text = "<b>" + ds.Tables(0).Rows(0).Item("A" + i.ToString() + "").ToString() + "</b>"
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("T" + i.ToString() + "").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If CDbl(ds.Tables(0).Rows(0).Item("T8").ToString()) > 0 Then
                                lbl.Text = FormatNumber((CDbl(ds.Tables(0).Rows(0).Item("T" + i.ToString() + "").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("T8").ToString())) * 100, 2)
                            Else
                                lbl.Text = "na"
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString()) > 0 Then
                                lbl.Text = FormatNumber((CDbl(ds.Tables(0).Rows(0).Item("T" + i.ToString() + "").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString())), 3)
                            Else
                                lbl.Text = "na"
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.CssClass = "NormalLable"
                            If CDbl(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString()) > 0 Then
                                lbl.Text = FormatNumber((CDbl(ds.Tables(0).Rows(0).Item("T" + i.ToString() + "").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString())), 3)
                            Else
                                lbl.Text = "na"
                            End If
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
                If i = 8 Or i = 12 Then
                    trInner.CssClass = "TdHeading"
                End If
                tblWater.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub MaterialAssumption()
        Dim ds As New DataSet
        Dim objGetData As New MoldS1GetData.Selectdata
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow

        Dim trHeaderFix As New TableRow
        Dim trHeaderFix1 As New TableRow
        Dim trHeaderFix2 As New TableRow

        Dim trInner As New TableRow
        Dim trInnerFix As New TableRow


        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell


        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell


        Try
            ds = objGetData.GetExtrusionDetails(CaseId)

            For i = 1 To 12
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "100", "Layers", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "150px", "Materials", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE1").ToString() + ")"
                        HeaderTdSetting(tdHeader, "60px", "Thickness", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        Title = "(%)"
                        HeaderTdSetting(tdHeader, "50px", "Recycle", "1")
                        Header2TdSetting(tdHeader2, "0", Title, "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)

                    Case 5
                        Title = "(MJ/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + " mat)"
                        HeaderTdSetting(tdHeader, "", "Energy", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "50px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 6
                        Header2TdSetting(tdHeader1, "50px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 7
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + " CO2/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + " mat)"
                        HeaderTdSetting(tdHeader, "", "CO2 Equivalent", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "50px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 8
                        Header2TdSetting(tdHeader1, "50px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)

                    Case 9
                        Title = "(%)"
                        HeaderTdSetting(tdHeader, "50px", "Extra-process", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 10
                        HeaderTdSetting(tdHeader, "", "Specific Gravity", "2")
                        Header2TdSetting(tdHeader2, "", "", "2")
                        Header2TdSetting(tdHeader1, "50px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 11
                        Header2TdSetting(tdHeader1, "50px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)

                    Case 12
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ") per Shipping unit"
                        HeaderTdSetting(tdHeader, "60px", Title, "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        HeaderTdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)



                End Select

            Next
            trHeader.Height = 30
            tblMatInput.Controls.Add(trHeader)
            tblMatInput.Controls.Add(trHeader2)
            tblMatInput.Controls.Add(trHeader1)


            'Inner
            For i = 1 To 10
                If CInt(ds.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()) <> 0 Then
                    trInner = New TableRow
                    k = k + 1
                    For j = 1 To 12

                        tdInner = New TableCell

                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "50px", "Center")
                                tdInner.Text = "<b>" + i.ToString() + "</b>"
                                trInner.Controls.Add(tdInner)
                            Case 2
                                InnerTdSetting(tdInner, "", "Left")
                                lbl = New Label
                                lbl.Width = 150
                                GetMaterialDetails(lbl, CInt(ds.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()))
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 3
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Width = 50
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("THICK" + i.ToString() + "").ToString(), 3)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 4
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Width = 50
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("R" + i.ToString() + "").ToString(), 2)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 5
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Width = 50
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("ERGYS" + i.ToString() + "").ToString(), 3)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 6
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Width = 50
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("ERGYP" + i.ToString() + "").ToString(), 3)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 7
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Width = 50
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("CO2S" + i.ToString() + "").ToString(), 3)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 8
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Width = 50
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("CO2P" + i.ToString() + "").ToString(), 3)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 9
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("E" + i.ToString() + "").ToString(), 2)
                                lbl.Width = 50
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 10
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Width = 50
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("SGS" + i.ToString() + "").ToString(), 3)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 11
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Width = 50
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("SGP" + i.ToString() + "").ToString(), 3)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 12
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Width = 60
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("SHIPUNIT" + i.ToString() + "").ToString(), 3)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                        End Select

                    Next
                End If

                If (k Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30
                tblMatInput.Controls.Add(trInner)
            Next
        Catch ex As Exception
            _lErrorLble.Text = "Error:MaterialAssumption:" + ex.Message.ToString()
        End Try

    End Sub

    Protected Sub MaterialResults()
        Dim ds As New DataSet
        Dim objGetData As New MoldS1GetData.Selectdata
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
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
                        HeaderTdSetting(tdHeader, "", "Layers", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "250px", "Material", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        Title = "(%)"
                        HeaderTdSetting(tdHeader, "90px", "Weight", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "120px", "Purchases", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                End Select

            Next
            tblMatResults.Controls.Add(trHeader)
            tblMatResults.Controls.Add(trHeader2)


            'Inner
            For i = 1 To 10
                If CInt(ds.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()) <> 0 Then
                    trInner = New TableRow
                    k = k + 1
                    For j = 1 To 4

                        tdInner = New TableCell

                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "50px", "Center")
                                tdInner.Text = "<b>" + i.ToString() + "</b>"
                                trInner.Controls.Add(tdInner)
                            Case 2
                                InnerTdSetting(tdInner, "", "left")
                                lbl = New Label
                                lbl.Text = ds.Tables(0).Rows(0).Item("MATDE" + i.ToString() + "").ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 3
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("P" + i.ToString() + "").ToString(), 1)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 4
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PUR" + i.ToString() + "").ToString(), 0)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                        End Select

                    Next

                    If (k Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"

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
        Dim objGetData As New MoldS1GetData.Selectdata
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
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
            ds = objGetData.GetEquipmentInDetails(CaseId)
            For i = 1 To 4
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "", "Asset", "1")
                        Header2TdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        HeaderTdSetting(tdHeader, "", "Description", "1")
                        Header2TdSetting(tdHeader1, "250px", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)

                    Case 3
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE7").ToString() + ")"
                        HeaderTdSetting(tdHeader, "", "Plant Area", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "60px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        Header2TdSetting(tdHeader1, "60px", "Preferred", "1")


                End Select


                trHeader1.Controls.Add(tdHeader1)

            Next
            tblEquipment.Controls.Add(trHeader)
            tblEquipment.Controls.Add(trHeader2)
            tblEquipment.Controls.Add(trHeader1)


            'Inner
            For i = 1 To 30
                If CInt(ds.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()) <> 0 Then
                    trInner = New TableRow
                    k = k + 1
                    For j = 1 To 4
                        tdInner = New TableCell

                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "50px", "Center")
                                tdInner.Text = "<b>" + i.ToString() + "</b>"
                                trInner.Controls.Add(tdInner)
                            Case 2
                                InnerTdSetting(tdInner, "", "Left")
                                lbl = New Label()
                                lbl.Width = 250
                                GetEquipment(lbl, CInt(ds.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()))
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 3
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PLANTAREAS" + i.ToString() + "").ToString(), 0)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                            Case 4
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PLANTAREAP" + i.ToString() + "").ToString(), 0)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)


                        End Select
                    Next

                    If (k Mod 2 = 0) Then
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
        Dim objGetData As New MoldS1GetData.Selectdata
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
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
            For i = 1 To 7
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "", "Asset", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        HeaderTdSetting(tdHeader, "250px", "Description", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)

                    Case 3
                        HeaderTdSetting(tdHeader, "100px", "Maximum Annual Run Hours", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        Title = "(" + ds.Tables(0).Rows(0).Item("Title8").ToString() + "/hr)"
                        HeaderTdSetting(tdHeader, "70px", "Instantaneous Rate", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 5
                        HeaderTdSetting(tdHeader, "", "Downtime", "1")
                        Header2TdSetting(tdHeader2, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 6
                        HeaderTdSetting(tdHeader, "70px", "Production Waste", "0")
                        Header2TdSetting(tdHeader2, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 7
                        HeaderTdSetting(tdHeader, "70px", "Design Waste", "0")
                        Header2TdSetting(tdHeader2, "", "(%)", "0")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)


                End Select


            Next
            tblOperations.Controls.Add(trHeader)
            tblOperations.Controls.Add(trHeader2)

            'Inner()
            For i = 1 To 30
                If CInt(ds.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()) <> 0 Then
                    trInner = New TableRow
                    k = k + 1
                    For j = 1 To 7
                        tdInner = New TableCell

                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "50px", "Center")
                                tdInner.Text = "<b>" + i.ToString() + "</b>"
                                trInner.Controls.Add(tdInner)
                            Case 2
                                InnerTdSetting(tdInner, "", "Left")
                                lbl = New Label
                                lbl.Text = ds.Tables(0).Rows(0).Item("EQUIPDES" + i.ToString() + "").ToString()
                                lbl.Width = 250
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                            Case 3
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("ANNUALRUNHOURS" + i.ToString() + "").ToString(), 0)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                            Case 4
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("INSTANTANEOUS2RATE" + i.ToString() + "").ToString(), 2)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 5
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("DOWNTIME" + i.ToString() + "").ToString(), 2)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 6
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PRODUCTIONWAST" + i.ToString() + "").ToString(), 1)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 7
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("DESIGNWAST" + i.ToString() + "").ToString(), 1)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)


                        End Select
                    Next

                    If (k Mod 2 = 0) Then
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
        Dim objGetData As New MoldS1GetData.Selectdata
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
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
            dsEffCountry = objGetData.GetEFFCOUNTRY(CaseId)
            Dim effCountry As String
            effCountry = dsEffCountry.Tables(0).Rows(0).Item("COUNTRY").ToString()
            ds = objGetData.GetPersonnelInDetails(CaseId)
            For i = 1 To 4
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "", "Positions", "1")
                        trHeader.Controls.Add(tdHeader)

                    Case 2
                        HeaderTdSetting(tdHeader, "160px", "Description", "1")
                        trHeader.Controls.Add(tdHeader)
                    Case 3
                        HeaderTdSetting(tdHeader, "100px", "Number of Workers", "1")
                        trHeader.Controls.Add(tdHeader)

                End Select


            Next
            tblPersonal.Controls.Add(trHeader)



            'Inner()
            For i = 1 To 30
                If CInt(ds.Tables(0).Rows(0).Item("ID" + i.ToString() + "").ToString()) <> 0 Then
                    trInner = New TableRow
                    k = k + 1
                    For j = 1 To 4
                        tdInner = New TableCell

                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "60px", "Center")
                                tdInner.Text = "<b>" + i.ToString() + "</b>"
                                trInner.Controls.Add(tdInner)
                            Case 2
                                InnerTdSetting(tdInner, "", "Left")
                                lbl = New Label()
                                GetPersonnelDetails(lbl, CInt(ds.Tables(0).Rows(0).Item("ID" + i.ToString() + "").ToString()), effCountry)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                            Case 3
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label
                                lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("NUMBER" + i.ToString() + "").ToString(), 2)
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                        End Select
                    Next

                    If (k Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    tblPersonal.Controls.Add(trInner)
                End If
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:PersonnelAssumption:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetEquipment(ByRef lbl As Label, ByVal EqId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New MoldS1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try

            Ds = ObjGetdata.GetEquipment(EqId, "", "")
            lbl.Text = Ds.Tables(0).Rows(0).Item("equipDES").ToString()

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetMaterialDetails(ByRef lbl As Label, ByVal MatId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New MoldS1GetData.Selectdata()
        Dim Path As String = String.Empty

        Try

            Ds = ObjGetdata.GetMaterials(MatId, "", "")
            lbl.Text = Ds.Tables(0).Rows(0).Item("MATDES").ToString()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetSustainMaterialDetails(ByRef lbl As Label, ByVal MatDes As String)
        Dim Ds As New DataSet
        Dim ObjGetdata As New MoldS1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty

        Try
            If MatDes.Trim().Length > 0 Then
                lbl.Text = MatDes.ToString()

            Else
                lbl.Text = "Nothing"
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetPersonnelDetails(ByRef lbl As Label, ByVal PersId As Integer, ByVal COUNTRY As String)
        Dim Ds As New DataSet
        Dim ObjGetdata As New MoldS1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try

            Ds = ObjGetdata.GetPersonnelInfo(PersId, "", "", COUNTRY)
            lbl.Text = Ds.Tables(0).Rows(0).Item("persDES").ToString()
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
End Class
