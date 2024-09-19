Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E1GetData
Imports E1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ1_Results_Resultspl
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
        GetContentPlaceHolder()
        GetErrorLable()
        GetUpdatebtn()
        GetLogOffbtn()
        GetInstructionsbtn()
        GetChartbtn()
        GetFeedbackbtn()
        GetNotesbtn()
        GetComparebtn()
        GetMainHeadingdiv()
    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Visible = True
        'Updatebtn.Attributes.Add("onclick", "return checkNumericAll()")
        Updatebtn.Attributes.Add("onclick", "return CheckResultpl('" + ctlContentPlaceHolder.ClientID + "','UNITPP','UNITPP2')")

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
        Comparebtn.OnClientClick = "return Compare('Comparision.aspx?Type=" + obj.Encrypt("PL") + "') "

        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Profit and Loss Statement')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Econ1 - Profit and Loss Statement"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Econ1ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_ECON1_RESULTS_RESULTSPL")
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
        Dim ddl As New DropDownList
        Dim strVol As Double = 0.0
 Dim ds2 As New DataSet

        Try
            ds = objGetData.GetProfitAndLossDetails(CaseId, False)
ds2 = objGetData.GetProductFromatIn(CaseId)
            If ds2.Tables(0).Rows(0).Item("M1").ToString() = 1 Then
                GetPageDetails2()
            Else
            For i = 1 To 9
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "160px", "", "1")
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
                        Title = "(" + ds.Tables(0).Rows(0).Item("PUN").ToString() + ")"
                        HeaderTdSetting(tdHeader, "90px", "By Volume", "1")
                        HeaderTdSetting(tdHeader1, "90px", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 6
                        Title = "(" + ds.Tables(0).Rows(0).Item("PUN1").ToString() + ")"
                        HeaderTdSetting(tdHeader, "90px", "By Volume", "1")
                        HeaderTdSetting(tdHeader1, "90px", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 7

                        If ds.Tables(0).Rows(0).Item("UNITTYPE").ToString() = 0 Then
                            Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        ElseIf ds.Tables(0).Rows(0).Item("UNITTYPE").ToString() = 1 Then
                            Title = "(" + ds.Tables(0).Rows(0).Item("PUN").ToString() + ")"
                        ElseIf ds.Tables(0).Rows(0).Item("UNITTYPE").ToString() = 2 Then
                            Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/thousand)"
                        End If
                        HeaderTdSetting(tdHeader, "", "Set Price", "3")
                        Header2TdSetting(tdHeader2, "70px", "Suggested", "1")
                        Header2TdSetting(tdHeader1, "", Title, "3")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader1)
                        trHeader1.Controls.Add(tdHeader2)
                    Case 8
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 9
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

            lblSalesVol.Text = "<b>Sales Volume (" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + "):</b> " + FormatNumber(ds.Tables(0).Rows(0).Item("SVOLUME").ToString(), 0).ToString()
            lblSalesVolUnit.Text = "<b>Sales Volume (" + ds.Tables(0).Rows(0).Item("SUNITLBL").ToString() + "):</b> " + FormatNumber(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString(), 0).ToString()



            'Inner
            hdnVolume.Value = CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString())
            hdnUnit.Value = CDbl(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString())
            For i = 1 To 25
                If i <> 24 Then
                    trInner = New TableRow
                    For j = 1 To 9
                        tdInner = New TableCell
                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "", "Left")
                                If i = 1 Or i = 7 Or i = 25 Then
                                    tdInner.Text = "<b>" + ds.Tables(0).Rows(0).Item("PDES" + i.ToString() + "").ToString() + "</b>"
                                Else
                                    tdInner.Text = "<span style='margin-left:20px;'><b>" + ds.Tables(0).Rows(0).Item("PDES" + i.ToString() + "").ToString() + "</b></span>"
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

                                If CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString()) > 0 Then
                                    perwt = CDbl(CDbl(ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds.Tables(0).Rows(0).Item("CURR").ToString())) / CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString())
                                    lbl.Text = FormatNumber(perwt, 3)
                                End If
                                '
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 5
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perunit As New Decimal
                                lbl = New Label()
                                If CDbl(ds.Tables(0).Rows(0).Item("SUNIT").ToString()) > 0 Then
                                    perunit = CDbl(ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds.Tables(0).Rows(0).Item("SUNIT").ToString())
                                    lbl.Text = FormatNumber(perunit, 4)
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 6
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perunit As New Decimal
                                lbl = New Label()
                                If CDbl(ds.Tables(0).Rows(0).Item("SUNIT1").ToString()) > 0 Then
                                    perunit = CDbl(ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds.Tables(0).Rows(0).Item("SUNIT1").ToString())
                                    lbl.Text = FormatNumber(perunit, 4)
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 7
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label()
                                'If i = 1 Then
                                '    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("UNITPS").ToString(), 3)
                                'Else
                                '    lbl.Text = ""
                                'End If
                                If i = 1 Then
                                    If CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 0 Then
                                        lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("UNITPS").ToString(), 3)
                                    ElseIf CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 1 Then
                                        strVol = CDbl(ds.Tables(0).Rows(0).Item("UNITPS").ToString()) * ((CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString()) * 100) / CDbl(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString()))
                                        lbl.Text = FormatNumber(strVol, 3)
                                    ElseIf CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 2 Then
                                        strVol = ((CDbl(ds.Tables(0).Rows(0).Item("UNITPS").ToString()) * (CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString()))) / CDbl(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString())) * 1000
                                        lbl.Text = FormatNumber(strVol, 3)
                                    End If
                                    tdInner.Controls.Add(lbl)
                                End If
                                If i = 4 Then
                                    InnerTdSetting(tdInner, "", "Center")
                                    HeaderTdSetting(tdInner, "", "Set Plant Margin Percent ", "3")
                                End If
                                'If i = 5 Then
                                '    InnerTdSetting(tdInner, "", "Center")
                                '    HeaderTdSetting(tdInner, "", "(%)", "3")
                                'End If

                                trInner.Controls.Add(tdInner)
                            Case 8
                                If i = 1 Then
                                    InnerTdSetting(tdInner, "", "Center")
                                    txt = New TextBox
                                    txt.CssClass = "SmallTextBox"
                                    txt.ID = "UNITPP"
                                    ' txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("UNITPP").ToString(), 3)
                                    txt.MaxLength = 6
                                    If ds.Tables(0).Rows(0).Item("UNITPP").ToString() <> "" Then
                                        If CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 0 Then
                                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("UNITPP").ToString(), 3)
                                        ElseIf CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 1 Then
                                            strVol = CDbl(ds.Tables(0).Rows(0).Item("UNITPP").ToString()) * ((CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString()) * 100) / CDbl(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString()))
                                            txt.Text = FormatNumber(strVol, 3)
                                        ElseIf CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 2 Then
                                            strVol = ((CDbl(ds.Tables(0).Rows(0).Item("UNITPP").ToString()) * (CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString()))) / CDbl(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString())) * 1000
                                            txt.Text = FormatNumber(strVol, 3)
                                        End If
                                    Else
                                        txt.Text = ""
                                    End If
                                    'AddHandler txt.TextChanged, AddressOf txtUnitpp_textchanged
                                    'txt.Attributes.Add("onclick", "return checkTxtEnable('" + ctlContentPlaceHolder.ClientID + "','UNITPP','UNITPP2')")
                                    tdInner.Controls.Add(txt)
                                End If

                                If i = 5 Then
                                    InnerTdSetting(tdInner, "", "Center")
                                    txt = New TextBox
                                    txt.CssClass = "SmallTextBox"
                                    txt.ID = "UNITPP2"
                                    txt.MaxLength = 6
                                    If ds.Tables(0).Rows(0).Item("UNITPP2").ToString() <> "" Then
                                        txt.Text = FormatNumber((CDbl(ds.Tables(0).Rows(0).Item("UNITPP2").ToString())), 2)
                                    Else
                                        txt.Text = ""
                                    End If
                                    'AddHandler txt.TextChanged, AddressOf txtUnitpp2_textchanged
                                    'txt.Attributes.Add("onclick", "return checkTxtEnable('" + ctlContentPlaceHolder.ClientID + "','UNITPP','UNITPP2')")
                                    tdInner.Controls.Add(txt)
                                End If
                                trInner.Controls.Add(tdInner)
                            Case 9
                                InnerTdSetting(tdInner, "", "Center")
                                ddl = New DropDownList()
                                lbl = New Label
                                If i = 1 Then
                                    BindUnitType(ddl, ds.Tables(0).Rows(0).Item("UNITTYPE").ToString())
                                    tdInner.Controls.Add(ddl)
                                End If
                                If i = 5 Then
                                    lbl.Text = "(%)"
                                    tdInner.Controls.Add(lbl)
                                End If
                                trInner.Controls.Add(tdInner)

                            Case Else
                        End Select
                    Next


                    tblComparision.Controls.Add(trInner)
                End If
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
            Next
 End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub


  Protected Sub GetPageDetails2()
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
        Dim ddl As New DropDownList
        Dim strVol As Double = 0.0
        Dim ds2 As New DataSet

        Try
            ds = objGetData.GetProfitAndLossDetails(CaseId, False)
            ds2 = objGetData.GetProductFromatIn(CaseId)

            For i = 1 To 10
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "160px", "", "1")
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
                        Title = "(" + ds.Tables(0).Rows(0).Item("PUN").ToString() + ")"
                        HeaderTdSetting(tdHeader, "90px", "By Volume", "1")
                        HeaderTdSetting(tdHeader1, "90px", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 6
                        Title = "(" + ds.Tables(0).Rows(0).Item("PUN1").ToString() + ")"
                        HeaderTdSetting(tdHeader, "90px", "By Volume", "1")
                        HeaderTdSetting(tdHeader1, "90px", "", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)


                    Case 7
                        Title = ds.Tables(0).Rows(0).Item("Title2").ToString()
                        HeaderTdSetting(tdHeader, "90px", "By Volume", "1")
                        HeaderTdSetting(tdHeader1, "90px", "", "1")
                        Header2TdSetting(tdHeader2, "", " (" + Title + "/thousand Impressions)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)


                    Case 8

                        If ds.Tables(0).Rows(0).Item("UNITTYPE").ToString() = 0 Then
                            Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        ElseIf ds.Tables(0).Rows(0).Item("UNITTYPE").ToString() = 1 Then
                            Title = "(" + ds.Tables(0).Rows(0).Item("PUN").ToString() + ")"
                        ElseIf ds.Tables(0).Rows(0).Item("UNITTYPE").ToString() = 2 Then
                            Title = "(" + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + "/thousand)"
                        End If
                        HeaderTdSetting(tdHeader, "", "Set Price", "3")
                        Header2TdSetting(tdHeader2, "70px", "Suggested", "1")
                        Header2TdSetting(tdHeader1, "", Title, "3")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader1)
                        trHeader1.Controls.Add(tdHeader2)
                    Case 9
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 10
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

            lblSalesVol.Text = "<b>Sales Volume (" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + "):</b> " + FormatNumber(ds.Tables(0).Rows(0).Item("SVOLUME").ToString(), 0).ToString()
            lblSalesVolUnit.Text = "<b>Sales Volume (" + ds.Tables(0).Rows(0).Item("SUNITLBL").ToString() + "):</b> " + FormatNumber(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString(), 0).ToString()
            lblSalesVolume.Text = "<b>Sales Volume (thousand Impressions):</b> " + FormatNumber(ds.Tables(0).Rows(0).Item("IMPRESSION").ToString(), 0).ToString()



            'Inner
            hdnVolume.Value = CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString())
            hdnUnit.Value = CDbl(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString())
            For i = 1 To 25
                If i <> 24 Then
                    trInner = New TableRow
                    For j = 1 To 10
                        tdInner = New TableCell
                        Select Case j
                            Case 1
                                'Layer
                                InnerTdSetting(tdInner, "", "Left")
                                If i = 1 Or i = 7 Or i = 25 Then
                                    tdInner.Text = "<b>" + ds.Tables(0).Rows(0).Item("PDES" + i.ToString() + "").ToString() + "</b>"
                                Else
                                    tdInner.Text = "<span style='margin-left:20px;'><b>" + ds.Tables(0).Rows(0).Item("PDES" + i.ToString() + "").ToString() + "</b></span>"
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

                                If CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString()) > 0 Then
                                    perwt = CDbl(CDbl(ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds.Tables(0).Rows(0).Item("CURR").ToString())) / CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString())
                                    lbl.Text = FormatNumber(perwt, 3)
                                End If
                                '
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 5
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perunit As New Decimal
                                lbl = New Label()
                                If CDbl(ds.Tables(0).Rows(0).Item("SUNIT").ToString()) > 0 Then
                                    perunit = CDbl(ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds.Tables(0).Rows(0).Item("SUNIT").ToString())
                                    lbl.Text = FormatNumber(perunit, 4)
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Case 6
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perunit As New Decimal
                                lbl = New Label()
                                If CDbl(ds.Tables(0).Rows(0).Item("SUNIT1").ToString()) > 0 Then
                                    perunit = CDbl(ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString()) * CDbl(ds.Tables(0).Rows(0).Item("SUNIT1").ToString())
                                    lbl.Text = FormatNumber(perunit, 4)
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                            Case 7
                                InnerTdSetting(tdInner, "", "Right")
                                Dim perunit As New Decimal
                                lbl = New Label()
                                If CDbl(ds.Tables(0).Rows(0).Item("IMPRESSION").ToString()) > 0 Then
                                     perunit = CDbl((CDbl(ds.Tables(0).Rows(0).Item("PL" + i.ToString() + "").ToString() * CDbl(ds.Tables(0).Rows(0).Item("CURR").ToString())) / CDbl(ds.Tables(0).Rows(0).Item("IMPRESSION").ToString())) )
                                   lbl.Text = FormatNumber(perunit, 4)
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                            Case 8
                                InnerTdSetting(tdInner, "", "Right")
                                lbl = New Label()
                                'If i = 1 Then
                                '    lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("UNITPS").ToString(), 3)
                                'Else
                                '    lbl.Text = ""
                                'End If
                                If i = 1 Then
                                    If CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 0 Then
                                        lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("UNITPS").ToString(), 3)
                                    ElseIf CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 1 Then
                                        strVol = CDbl(ds.Tables(0).Rows(0).Item("UNITPS").ToString()) * ((CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString()) * 100) / CDbl(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString()))
                                        lbl.Text = FormatNumber(strVol, 3)
                                    ElseIf CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 2 Then
                                        strVol = ((CDbl(ds.Tables(0).Rows(0).Item("UNITPS").ToString()) * (CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString()))) / CDbl(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString())) * 1000
                                        lbl.Text = FormatNumber(strVol, 3)
                                    End If
                                    tdInner.Controls.Add(lbl)
                                End If
                                If i = 4 Then
                                    InnerTdSetting(tdInner, "", "Center")
                                    HeaderTdSetting(tdInner, "", "Set Plant Margin Percent ", "3")
                                End If
                                'If i = 5 Then
                                '    InnerTdSetting(tdInner, "", "Center")
                                '    HeaderTdSetting(tdInner, "", "(%)", "3")
                                'End If

                                trInner.Controls.Add(tdInner)
                            Case 9
                                If i = 1 Then
                                    InnerTdSetting(tdInner, "", "Center")
                                    txt = New TextBox
                                    txt.CssClass = "SmallTextBox"
                                    txt.ID = "UNITPP"
                                    ' txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("UNITPP").ToString(), 3)
                                    txt.MaxLength = 6
                                    If ds.Tables(0).Rows(0).Item("UNITPP").ToString() <> "" Then
                                        If CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 0 Then
                                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("UNITPP").ToString(), 3)
                                        ElseIf CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 1 Then
                                            strVol = CDbl(ds.Tables(0).Rows(0).Item("UNITPP").ToString()) * ((CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString()) * 100) / CDbl(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString()))
                                            txt.Text = FormatNumber(strVol, 3)
                                        ElseIf CInt(ds.Tables(0).Rows(0).Item("UNITTYPE").ToString()) = 2 Then
                                            strVol = ((CDbl(ds.Tables(0).Rows(0).Item("UNITPP").ToString()) * (CDbl(ds.Tables(0).Rows(0).Item("SVOLUME").ToString()))) / CDbl(ds.Tables(0).Rows(0).Item("SUNITVAL").ToString())) * 1000
                                            txt.Text = FormatNumber(strVol, 3)
                                        End If
                                    Else
                                        txt.Text = ""
                                    End If
                                    'AddHandler txt.TextChanged, AddressOf txtUnitpp_textchanged
                                    'txt.Attributes.Add("onclick", "return checkTxtEnable('" + ctlContentPlaceHolder.ClientID + "','UNITPP','UNITPP2')")
                                    tdInner.Controls.Add(txt)
                                End If

                                If i = 5 Then
                                    InnerTdSetting(tdInner, "", "Center")
                                    txt = New TextBox
                                    txt.CssClass = "SmallTextBox"
                                    txt.ID = "UNITPP2"
                                    txt.MaxLength = 6
                                    If ds.Tables(0).Rows(0).Item("UNITPP2").ToString() <> "" Then
                                        txt.Text = FormatNumber((CDbl(ds.Tables(0).Rows(0).Item("UNITPP2").ToString())), 2)
                                    Else
                                        txt.Text = ""
                                    End If
                                    'AddHandler txt.TextChanged, AddressOf txtUnitpp2_textchanged
                                    'txt.Attributes.Add("onclick", "return checkTxtEnable('" + ctlContentPlaceHolder.ClientID + "','UNITPP','UNITPP2')")
                                    tdInner.Controls.Add(txt)
                                End If
                                trInner.Controls.Add(tdInner)
                            Case 10
                                InnerTdSetting(tdInner, "", "Center")
                                ddl = New DropDownList()
                                lbl = New Label
                                If i = 1 Then
                                    BindUnitType(ddl, ds.Tables(0).Rows(0).Item("UNITTYPE").ToString())
                                    tdInner.Controls.Add(ddl)
                                End If
                                If i = 5 Then
                                    lbl.Text = "(%)"
                                    tdInner.Controls.Add(lbl)
                                End If
                                trInner.Controls.Add(tdInner)

                            Case Else
                        End Select
                    Next


                    tblComparision.Controls.Add(trInner)
                End If
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


    Protected Sub BindUnitType(ByVal ddl As DropDownList, ByVal unitType As String)
        Dim objGetData As New E1GetData.Selectdata()
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
        Dim UnitPrice As String = String.Empty
        Dim UnitPrice2 As String = String.Empty
        Dim UnitType As String = String.Empty
        Dim ObjUpIns As New E1UpInsData.UpdateInsert()
        Dim obj As New CryptoHelper
        Try
            If Not objRefresh.IsRefresh Then
                Try
                    UnitPrice = Request.Form("ctl00$Econ1ContentPlaceHolder$UNITPP").Replace(",", "")
                Catch ex As Exception
                    UnitPrice = ""
                End Try
                Try
                    UnitPrice2 = Request.Form("ctl00$Econ1ContentPlaceHolder$UNITPP2").Replace(",", "")
                Catch ex As Exception
                    UnitPrice2 = ""
                End Try


                UnitType = Request.Form("ctl00$Econ1ContentPlaceHolder$UNITTYPE")

                If UnitPrice <> "" Then
                    If UnitType = 1 Then
                        UnitPrice = UnitPrice * (hdnUnit.Value / (hdnVolume.Value * 100))
                    ElseIf UnitType = 2 Then
                        UnitPrice = UnitPrice * (hdnUnit.Value / (hdnVolume.Value * 1000))
                    End If

                End If
                If hdnVolume.Value <> "0" Then
                    ObjUpIns.ResultsPl(CaseId.ToString(), UnitPrice, UnitType, UnitPrice2)
                End If
                Calculate()
                'Update Server Date
                ObjUpIns.ServerDateUpdate(CaseId, Session("E1UserName"))
                'End If
            End If

            GetPageDetails()
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
