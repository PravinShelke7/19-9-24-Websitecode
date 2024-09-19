Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SMed2GetData
Imports SMed2UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MedSustain2_Assumptions_PalletIN
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
    Dim _btnWizard As ImageButton
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

    Public Property Wizardbtn() As ImageButton
        Get
            Return _btnWizard
        End Get
        Set(ByVal value As ImageButton)
            _btnWizard = value
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
        GetWizard()
        GetMainHeadingdiv()

    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Attributes.Add("onclick", "return CheckForPalletIn('" + ctlContentPlaceHolder.ClientID + "','hidPalid','NUM','NOUSE')")
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
        Notesbtn.Visible = True
        Notesbtn.OnClientClick = "return Notes('PALLETPKG');"
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetWizard()
        Wizardbtn = Page.Master.FindControl("imgWizard")
        Wizardbtn.Visible = True
        Wizardbtn.OnClientClick = "return Wizard('Wizard.aspx?Type=BLDBX');"
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Pallet Configuration Assumptions')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "SMed2 - Pallet Configuration Assumptions "
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("SMed2ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_MEDSUSTAIN2_ASSUMPTIONS_PALLETIN")
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
                ViewState("TabId") = 1
                GetPageDetails()
            End If
            CreateTab()
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub CreateTab()
        Dim tr As New TableRow
        Dim td As New TableCell
        Dim i As New Integer
        Dim lnk As New LinkButton
        Try
            tblTab.Rows.Clear()
            For i = 1 To 2
                td = New TableCell
                lnk = New LinkButton
                lnk.Text = "Pallet Packaging Tab " + i.ToString()
                lnk.CssClass = "TabLink"
                lnk.ID = "lnkPal" + i.ToString()
                If ViewState("TabId").ToString() = "1" Then
                    lnk.Attributes.Add("onclick", "return CheckForPalletIn('" + ctlContentPlaceHolder.ClientID + "','hidPalid','NUM','NOUSE')")
                ElseIf ViewState("TabId").ToString() = "2" Then
                    lnk.Attributes.Add("onclick", "return checkNumericAll();")
                End If

                AddHandler lnk.Click, AddressOf lnkPallet_Click
                If i = Convert.ToInt32(ViewState("TabId").ToString()) Then
                    td.CssClass = "AlterNateTab3"
                Else
                    td.CssClass = "AlterNateTab4"
                End If
                td.Controls.Add(lnk)
                tr.Controls.Add(td)
            Next
            tblTab.Controls.Add(tr)
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub lnkPallet_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lnk As LinkButton = DirectCast(sender, System.Web.UI.WebControls.LinkButton)
            UpdatePage()
            ViewState("TabId") = Convert.ToInt32(lnk.ID.Replace("lnkPal", "").ToString())
            If Not IsNothing(ViewState("TabId")) Then
                If ViewState("TabId").ToString() = "1" Then
                    Updatebtn.Attributes.Add("onclick", "return CheckForPalletIn('" + ctlContentPlaceHolder.ClientID + "','hidPalid','NUM','NOUSE')")
                ElseIf ViewState("TabId").ToString() = "2" Then
                    Updatebtn.Attributes.Add("onclick", "return checkNumericAll();")
                End If
            End If
            CreateTab()
            GetPageDetails()
        Catch ex As Exception
            Response.Write("lnkPallet_Click" + ex.Message)
        End Try
    End Sub

    Protected Sub GetSessionDetails()
        Try
            UserId = Session("UserId")
            CaseId = Session("SMed2CaseId")
            UserRole = Session("SMed2UserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New SMed2GetData.Selectdata
        Try
            ds = objGetData.GetPalletInDetails(CaseId)
            If ViewState("TabId") = "1" Then
                GetPageDetailsForTab1(ds)
            ElseIf ViewState("TabId") = "2" Then
                GetPageDetailsForTab2(ds)
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetPageDetailsForTab1(ByVal ds As DataSet)

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

            For i = 1 To 15
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
                        HeaderTdSetting(tdHeader, "150px", "Item", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "70px", "Number", "1")
                        Header2TdSetting(tdHeader2, "", "(per pallet)", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        Title = "(Units)"
                        HeaderTdSetting(tdHeader, "90", "Number Of Uses", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "", "Weight Each", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "70px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 6
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 7
                        Title = "(MJ/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + " Mat.)"
                        HeaderTdSetting(tdHeader, "", "Energy", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "70px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 8
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 9
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + " CO2/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + " " + ds.Tables(0).Rows(0).Item("TITLE2").ToString() + " Mat.)"
                        HeaderTdSetting(tdHeader, "", "CO2 Equivalent", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "70px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 10
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)

                    Case 11
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE10").ToString() + " Water/" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + " Mat.)"
                        HeaderTdSetting(tdHeader, "", "Water", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "60px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 12
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 13
                        Title = "(" + ds.Tables(0).Rows(0).Item("TITLE4").ToString() + ")"
                        HeaderTdSetting(tdHeader, "", "Ship", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "70px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 14
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 15
                        HeaderTdSetting(tdHeader, "120px", "Mfg. Dept.", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                End Select


            Next

            trHeader.Height = 30

            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader1)

            'Inner
            For i = 1 To 10
                trInner = New TableRow
                For j = 1 To 15
                    tdInner = New TableCell
                    Select Case j
                        Case 1
                            'Layer
                            InnerTdSetting(tdInner, "", "Center")
                            tdInner.Text = "<b>" + i.ToString() + "</b>"
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = "hypPalDes" + i.ToString()
                            hid.ID = "hidPalid" + i.ToString()
                            Link.Width = 150
                            Link.CssClass = "Link"
                            GetPalletDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()), Nothing)
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "NUM" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("NUM" + i.ToString() + "").ToString(), 2)
                            txt.MaxLength = 5
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "NOUSE" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("NU" + i.ToString() + "").ToString(), 2)
                            txt.MaxLength = 4
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 50
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WS" + i.ToString() + "").ToString(), 2)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 50
                            txt.ID = "WP" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WP" + i.ToString() + "").ToString(), 2)
                            txt.MaxLength = 6
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 50
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("ES" + i.ToString() + "").ToString(), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 50
                            txt.ID = "ERGYP" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("EP" + i.ToString() + "").ToString(), 3)
                            txt.MaxLength = 6
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 9
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 50
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("CO2S" + i.ToString() + "").ToString(), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 10
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 50
                            txt.ID = "GHGP" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("CO2P" + i.ToString() + "").ToString(), 3)
                            txt.MaxLength = 6
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 11
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 50
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WATERS" + i.ToString() + "").ToString(), 2)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 12
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 50
                            txt.ID = "WATERP" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("WATERP" + i.ToString() + "").ToString(), 2)
                            txt.MaxLength = 6
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 13
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 50
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("SS" + i.ToString() + "").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 14
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 50
                            txt.ID = "SDP" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("SP" + i.ToString() + "").ToString(), 2)
                            txt.MaxLength = 6
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 15
                            InnerTdSetting(tdInner, "", "Left")
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = "hypPalletDep" + i.ToString()
                            hid.ID = "hidPalletDepid" + i.ToString()
                            Link.Width = 120
                            Link.CssClass = "Link"
                            GetDeptDetails(Link, hid, CInt(ds.Tables(0).Rows(0).Item("D" + i.ToString() + "").ToString()))
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30

                tblComparision.Controls.Add(trInner)
            Next

            'Total
            trInner = New TableRow
            For i = 1 To 15
                tdInner = New TableCell
                Select Case i
                    Case 1
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.Text = "<span class='CalculatedFeilds'><b> Total </b></span>"
                        trInner.Controls.Add(tdInner)
                    Case 5
                        InnerTdSetting(tdInner, "", "center")
                        tdInner.ColumnSpan = 2
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("WEIGHTTOT").ToString(), 2) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 7
                        InnerTdSetting(tdInner, "", "center")
                        tdInner.ColumnSpan = 2
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("ENERGYTOT").ToString(), 3) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 9
                        InnerTdSetting(tdInner, "", "center")
                        tdInner.ColumnSpan = 2
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("CO2TOT").ToString(), 3) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 11
                        InnerTdSetting(tdInner, "", "center")
                        tdInner.ColumnSpan = 2
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("WATERTOT").ToString(), 3) + " </b> </span>"
                        trInner.Controls.Add(tdInner)

                    Case 13
                        InnerTdSetting(tdInner, "", "center")
                        tdInner.ColumnSpan = 2
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("SHIPTOT").ToString(), 2) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 2, 3, 4, 15
                        InnerTdSetting(tdInner, "", "Left")
                        trInner.Controls.Add(tdInner)

                End Select
            Next
            trInner.Height = 30
            trInner.CssClass = "AlterNateColor2"
            tblComparision.Controls.Add(trInner)

            'Total Old Pallet Shiping
            trInner = New TableRow
            For i = 1 To 15
                tdInner = New TableCell
                Select Case i
                    Case 13
                        InnerTdSetting(tdInner, "", "center")
                        tdInner.ColumnSpan = 2
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("SHIPOLDTOT").ToString(), 2) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 1
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.ColumnSpan = 2
                        trInner.Controls.Add(tdInner)

                    Case 3
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.ColumnSpan = 10
                        trInner.Controls.Add(tdInner)

                    Case 15
                        InnerTdSetting(tdInner, "", "Left")
                        trInner.Controls.Add(tdInner)

                End Select
            Next
            'trInner.Height = 30
            'trInner.CssClass = "AlterNateColor1"
            'tblComparision.Controls.Add(trInner)

            trInner.Height = 30
            trInner.CssClass = "AlterNateColor1"
            tblComparision.Controls.Add(trInner)

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetailsForTab1:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetailsForTab2(ByVal ds As DataSet)

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
                        HeaderTdSetting(tdHeader, "150px", "Item", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        Title = "(Unitless)"
                        HeaderTdSetting(tdHeader, "", "Recovery", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "70px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        Title = "(Unitless)"
                        HeaderTdSetting(tdHeader, "", "Sustainable Materials", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "70px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 6
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 7
                        Title = "(Unitless)"
                        HeaderTdSetting(tdHeader, "", "PC Recycle", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "70px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 8
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                End Select


            Next

            trHeader.Height = 30

            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader1)

            'Inner
            For i = 1 To 10
                trInner = New TableRow
                For j = 1 To 8
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            'Layer
                            InnerTdSetting(tdInner, "", "Center")
                            tdInner.Text = "<b>" + i.ToString() + "</b>"
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Width = 180
                            GetPalletDetails(Nothing, Nothing, CInt(ds.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()), lbl)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 50
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("RECS" + i.ToString() + "").ToString(), 2)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 50
                            txt.ID = "RECP" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("RECP" + i.ToString() + "").ToString(), 2)
                            txt.MaxLength = 6
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Width = 70
                            lbl.Text = ds.Tables(0).Rows(0).Item("SMS" + i.ToString() + "").ToString()
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Left")
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = "hypSusMat" + i.ToString()
                            hid.ID = "hidSusMatId" + i.ToString()
                            Link.Width = 70
                            Link.CssClass = "Link"
                            GetSustainMaterialDetails(Link, hid, ds.Tables(0).Rows(0).Item("SMP" + i.ToString() + "").ToString())
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 50
                            lbl.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PCRECS" + i.ToString() + "").ToString(), 2)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 50
                            txt.ID = "PCRECP" + i.ToString()
                            txt.Text = FormatNumber(ds.Tables(0).Rows(0).Item("PCRECP" + i.ToString() + "").ToString(), 2)
                            txt.MaxLength = 6
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30
                tblComparision.Controls.Add(trInner)
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
                    Case 3
                        InnerTdSetting(tdInner, "", "center")
                        tdInner.ColumnSpan = 2
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("RECOVTOT").ToString(), 2) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 5
                        InnerTdSetting(tdInner, "", "center")
                        tdInner.ColumnSpan = 2
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("TOTALOLDPALLETOSHA").ToString(), 2) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 7
                        InnerTdSetting(tdInner, "", "center")
                        tdInner.ColumnSpan = 2
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("PCRECTOT").ToString(), 2) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 2
                        InnerTdSetting(tdInner, "", "Left")
                        trInner.Controls.Add(tdInner)
                End Select
            Next
            trInner.Height = 30
            trInner.CssClass = "AlterNateColor2"
            tblComparision.Controls.Add(trInner)



        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetailsForTab2:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPalletDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal palletId As Integer, ByVal lbl As Label)
        Dim Ds As New DataSet
        Dim ObjGetdata As New SMed2GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Try

            Ds = ObjGetdata.GetPallets(palletId, "", "")
            If Not IsNothing(LinkMat) Then
                LinkMat.Text = Ds.Tables(0).Rows(0).Item("PalletDes").ToString()
                hid.Value = palletId.ToString()
                Path = "../PopUp/GetPalletPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
                LinkMat.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"
            ElseIf Not IsNothing(lbl) Then
                lbl.Text = Ds.Tables(0).Rows(0).Item("PalletDes").ToString()
                lbl.ID = palletId.ToString()
            End If


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

    Protected Sub GetDeptDetails(ByRef LinkDep As HyperLink, ByVal hid As HiddenField, ByVal ProcId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New SMed2GetData.Selectdata()
        Dim Path As String = String.Empty
        Try
            Ds = ObjGetdata.GetDept(ProcId, "")
            Path = "../PopUp/GetDepPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkDep.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
            If Ds.Tables(0).Rows.Count = 0 Then
                LinkDep.Text = "Dept. Conflict"
                LinkDep.ForeColor = Drawing.Color.DarkRed
            Else
                LinkDep.Text = Ds.Tables(0).Rows(0).Item("PROCDE").ToString()
            End If

            hid.Value = ProcId.ToString()
            LinkDep.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"
        Catch ex As Exception
            ErrorLable.Text = "Error:GetDeptDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetSustainMaterialDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal MatDes As String)
        Dim Ds As New DataSet
        Dim ObjGetdata As New SMed2GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty

        Try
            If MatDes.Trim().Length > 0 Then
                LinkMat.Text = MatDes.ToString()
                LinkMat.ToolTip = MatDes.ToString()
                hid.Value = MatDes.ToString()
            Else
                LinkMat.Text = "Nothing"
                LinkMat.ToolTip = "Nothing"
                hid.Value = "Nothing"
            End If
            LinkMat.Attributes.Add("text-decoration", "none")
            Path = "../PopUp/GetSusMatPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
            LinkMat.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"

        Catch ex As Exception
            ErrorLable.Text = "Error:GetSustainMaterialDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            If Not objRefresh.IsRefresh Then
                UpdatePage()
            End If
            GetPageDetails()

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub UpdatePage()
        Dim Pallet(9) As String
        Dim Number(9) As String
        Dim NoOfUses(9) As String
        Dim PrefWeight(9) As String
        Dim PrefErgy(9) As String
        Dim PrefCO2(9) As String
        Dim PrefWater(9) As String
        Dim PrefRECOV(9) As String
        Dim PrefSUSM(9) As String
        Dim PrefPCREC(9) As String
        Dim PrefSHIPD(9) As String
        Dim Dept(9) As String
        Dim i As New Integer
        Dim ObjUpIns As New SMed2UpInsData.UpdateInsert()
        Dim obj As New CryptoHelper
        Try
            If Not objRefresh.IsRefresh Then
                If ViewState("TabId").ToString() = "1" Then
                    For i = 1 To 10
                        Pallet(i - 1) = Request.Form("ctl00$SMed2ContentPlaceHolder$hidPalid" + i.ToString() + "")
                        Number(i - 1) = Request.Form("ctl00$SMed2ContentPlaceHolder$NUM" + i.ToString() + "").Replace(",", "")
                        NoOfUses(i - 1) = Request.Form("ctl00$SMed2ContentPlaceHolder$NOUSE" + i.ToString() + "").Replace(",", "")
                        PrefWeight(i - 1) = Request.Form("ctl00$SMed2ContentPlaceHolder$WP" + i.ToString() + "").Replace(",", "")
                        PrefErgy(i - 1) = Request.Form("ctl00$SMed2ContentPlaceHolder$ERGYP" + i.ToString() + "").Replace(",", "")
                        PrefCO2(i - 1) = Request.Form("ctl00$SMed2ContentPlaceHolder$GHGP" + i.ToString() + "").Replace(",", "")
                        PrefWater(i - 1) = Request.Form("ctl00$SMed2ContentPlaceHolder$WATERP" + i.ToString() + "").Replace(",", "")
                        PrefSHIPD(i - 1) = Request.Form("ctl00$SMed2ContentPlaceHolder$SDP" + i.ToString() + "").Replace(",", "")
                        Dept(i - 1) = Request.Form("ctl00$SMed2ContentPlaceHolder$hidPalletDepid" + i.ToString() + "")

                        'Check For IsNumric
                        If Not IsNumeric(Number(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If

                        If Not IsNumeric(NoOfUses(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If

                        If Not IsNumeric(PrefWeight(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If

                        If Not IsNumeric(PrefErgy(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If

                        If Not IsNumeric(PrefCO2(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If

                        If Not IsNumeric(PrefWater(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                        If Not IsNumeric(PrefSHIPD(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If

                        'Check For Dependant-Indepdant Error
                        If CInt(Pallet(i - 1)) <> 0 Then
                            'Checking Number
                            If CDbl(Number(i - 1)) <= CDbl(0.0) Then
                                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE105").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                            End If

                            ''Checking Dept.
                            'If CDbl(Dept(i - 1)) <= 0 Then
                            '    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE108").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                            'End If
                        End If
                    Next
                    ObjUpIns.PalletUpdate(CaseId, Pallet, Number, NoOfUses, PrefWeight, PrefErgy, PrefCO2, PrefRECOV, PrefSUSM, PrefPCREC, PrefSHIPD, Dept, PrefWater, ViewState("TabId").ToString())
                ElseIf ViewState("TabId").ToString() = "2" Then
                    For i = 1 To 10
                        PrefRECOV(i - 1) = Request.Form("ctl00$SMed2ContentPlaceHolder$RECP" + i.ToString() + "").Replace(",", "")
                        PrefSUSM(i - 1) = Request.Form("ctl00$SMed2ContentPlaceHolder$hidSusMatId" + i.ToString() + "")
                        PrefPCREC(i - 1) = Request.Form("ctl00$SMed2ContentPlaceHolder$PCRECP" + i.ToString() + "").Replace(",", "")
                        'Check Numeric
                        If Not IsNumeric(PrefRECOV(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If

                        If Not IsNumeric(PrefPCREC(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                    Next
                    ObjUpIns.PalletUpdate(CaseId, Pallet, Number, NoOfUses, PrefWeight, PrefErgy, PrefCO2, PrefRECOV, PrefSUSM, PrefPCREC, PrefSHIPD, Dept, PrefWater, ViewState("TabId").ToString())
                End If
                Calculate()
                'Update Server Date
                ObjUpIns.ServerDateUpdate(CaseId, Session("SMed2UserName"))
            End If

        Catch ex As Exception
            ErrorLable.Text = "Error:UpdatePage:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub Calculate()
        Try
            Dim Med1Conn As String = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
            Dim SMed1Conn As String = System.Configuration.ConfigurationManager.AppSettings("MedSustain1ConnectionString")
            Dim Med2Conn As String = System.Configuration.ConfigurationManager.AppSettings("MedEcon2ConnectionString")
            Dim SMed2Conn As String = System.Configuration.ConfigurationManager.AppSettings("MedSustain2ConnectionString")
            Dim obj As New SMed2Calculation.SMed2Calculations(SMed2Conn, Med2Conn, SMed1Conn, Med1Conn)
            obj.SMed2Calculate(CaseId)
        Catch ex As Exception
            ErrorLable.Text = "Error:Calculate:" + ex.Message.ToString() + ""
        End Try
    End Sub



End Class
