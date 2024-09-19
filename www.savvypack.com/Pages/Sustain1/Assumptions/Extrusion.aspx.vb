Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S1GetData
Imports S1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain1_Assumptions_Extrusion
    Inherits System.Web.UI.Page
    Public CaseDes As String = String.Empty
    Shared objCalb As New S1BarrierGetData.Calculations
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
    Dim _btnCompare As ImageButton
    Dim _btnBarr As ImageButton

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
    Public Property Comparebtn() As ImageButton
        Get
            Return _btnCompare
        End Get
        Set(ByVal value As ImageButton)
            _btnCompare = value
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

    Public Property Barrbtn() As ImageButton
        Get
            Return _btnBarr
        End Get
        Set(ByVal value As ImageButton)
            _btnBarr = value
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
        GetMainHeadingdiv()
        GetComparebtn()
        GetBarPropbtn()
    End Sub
    Protected Sub GetBarPropbtn()
        Barrbtn = Page.Master.FindControl("imgBarProp")
        Barrbtn.Visible = True
        Barrbtn.ToolTip = "Show Barrier Properties"
        Barrbtn.OnClientClick = "return CallBackBarrier();"
    End Sub
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Visible = True
        If IsNothing(ViewState("TabId")) Then
            Updatebtn.Attributes.Add("onclick", "return CheckForMaterialPageBarr('" + ctlContentPlaceHolder.ClientID + "','hidMatid','THICK','SHIPU','hidShipSId','hypShipS','hidDepid','hypDep','divVariable','rdScrollOff','OTRTEMP','RH','txtOTRTemp','txtWVTRTemp','txtOTRHumidity','txtWVTRHumidity');")
            'Updatebtn.Attributes.Add("onclick", "return CheckForMaterialPage('" + ctlContentPlaceHolder.ClientID + "','hidMatid','THICK','SHIPU','hidShipSId','hypShipS','hidDepid','hypDep','divVariable','rdScrollOff');")
        End If
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
        Notesbtn.OnClientClick = "return Notes('MATANDSTR');"
        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Material and Structure Assumptions')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Sustain1 - Material and Structure Assumptions"
    End Sub
    Protected Sub GetComparebtn()
        Dim obj As New CryptoHelper()
        Comparebtn = Page.Master.FindControl("imgCompare")
        Comparebtn.Visible = True
        Comparebtn.OnClientClick = "return Compare('LCIComparison.aspx') "

        'AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Sustain1ContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_SUSTAIN1_ASSUMPTIONS_EXTRUSION")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GetMasterPageControls()
            GetSessionDetails()
            GetChartbtn()
            If Not IsPostBack Then
                hidBarrier.Value = "0"
                ViewState("TabId") = 1
                GetPageDetails()
                GetTempRH()
            End If
            CreateTab()
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

    Protected Sub GetTempRH()
        Dim ds1 As New DataSet()
        Dim ds2 As New DataSet()
        Dim ds3 As New DataSet()
        Dim objGetData As New S1GetData.Selectdata()
        Try
            ds3 = objGetData.GetPrefDetails(CaseId)
            ds1 = objGetData.GetMinMaxBarrierTemp()
            ds2 = objGetData.GetMinMaxBarrierHumidity()

            If ds1.Tables(0).Rows.Count > 0 Then
                If ds3.Tables(0).Rows(0).Item("UNITS").ToString() <> "1" Then
                    OTRTEMP1.Value = ds1.Tables(0).Rows(0).Item("MINVAL").ToString()
                    OTRTEMP2.Value = ds1.Tables(0).Rows(0).Item("MAXVAL").ToString()
                Else
                    OTRTEMP1.Value = (CDbl(ds1.Tables(0).Rows(0).Item("MINVAL").ToString()) - 32) * (5 / 9)
                    OTRTEMP2.Value = (CDbl(ds1.Tables(0).Rows(0).Item("MAXVAL").ToString()) - 32) * (5 / 9)
                End If

            End If

            If ds2.Tables(0).Rows.Count > 0 Then
                RH1.Value = ds2.Tables(0).Rows(0).Item("MINVAL").ToString()
                RH2.Value = ds2.Tables(0).Rows(0).Item("MAXVAL").ToString()
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetPageDetails()
        'Dim ds As New DataSet
        Dim objGetData As New S1GetData.Selectdata
        Dim dsPref As New DataSet
        Dim dsSug As New DataSet
        Try
            'ds = objGetData.GetExtrusionDetails(CaseId)
            dsPref = objGetData.GetExtrusionDetailsPref(CaseId)
            dsSug = objGetData.GetExtrusionDetailsSugg(CaseId)

            If ViewState("TabId") = "1" Then
                Barrbtn.Enabled = True
                GetPageDetailsForTab1(dsPref, dsSug)
            ElseIf ViewState("TabId") = "2" Then
                Barrbtn.Enabled = False
                GetPageDetailsForTab2(dsPref, dsSug)
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetPageDetailsForTab1(ByVal dsPref As DataSet, ByVal dsSug As DataSet)

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

        Dim chk As New CheckBox

        Dim radio As New RadioButton
        Dim OTR As String
        Dim WVTR As String
        Dim path As String = ""

        Dim ImgBut As New ImageButton
        Dim ImgDis As New ImageButton
        Dim dsEmat As New DataSet
        Dim dvEmat As New DataView
        Dim dtEmat As New DataTable

        Dim ds1 As New DataSet
        Dim dsGrad As New DataSet
        Dim dsDept As New DataSet
        Dim dsSP As New DataSet
        Dim dsDM As New DataSet
        Try
            ds1 = objGetData.GetMaterials("-1", "", "")
            dsGrad = objGetData.GetAllGradesVal("-1")
            dsDept = objGetData.GetDept("-1", "", "")
            dsSP = objGetData.GetShipingSelector("-1", "")
            dsDM = objGetData.GetDiscMaterials("-1", "")

            'Barrier
            If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() <> 1 Then
                OTR = dsPref.Tables(0).Rows(0).Item("OTRTEMP").ToString()
                WVTR = dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString()
            Else
                OTR = (CDbl(dsPref.Tables(0).Rows(0).Item("OTRTEMP").ToString()) - 32) * 5 / 9
                WVTR = (CDbl(dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString()) - 32) * 5 / 9
            End If

            dsEmat = objGetData.GetEditMaterial(CaseId)
            dvEmat = dsEmat.Tables(0).DefaultView

            Session("dsEmat") = dsEmat

            Dim Grades(9) As String
            Dim Materials(9) As String
            Dim OtrVal(9) As String
            Dim WVTRVal(9) As String
            Dim Thick(9) As String
            Dim ISADJTHICK(9) As String
            Dim MatIDD(9) As String
            For i = 0 To 9
                Grades(i) = dsPref.Tables(0).Rows(0).Item("GRADE" + (i + 1).ToString() + "").ToString()
                Materials(i) = dsSug.Tables(0).Rows(0).Item("MATS" + (i + 1).ToString() + "").ToString()
                MatIDD(i) = dsPref.Tables(0).Rows(0).Item("M" + (i + 1).ToString() + "").ToString()
                OtrVal(i) = dsPref.Tables(0).Rows(0).Item("OTR" + (i + 1).ToString() + "").ToString()
                WVTRVal(i) = dsPref.Tables(0).Rows(0).Item("WVTR" + (i + 1).ToString() + "").ToString()
                Thick(i) = CDbl(dsPref.Tables(0).Rows(0).Item("THICK" + (i + 1).ToString() + "").ToString() / dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString())
                ISADJTHICK(i) = dsSug.Tables(0).Rows(0).Item("ISADJTHICK" + (i + 1).ToString() + "").ToString()
            Next

            objCalb.BarrierPropCalculateNew(CaseId, dsPref.Tables(0).Rows(0).Item("OTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("OTRRH").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRRH").ToString(), _
            Grades, CDate(dsPref.Tables(0).Rows(0).Item("EFFDATEB").ToString()), Materials, OtrVal, WVTRVal, Thick, ISADJTHICK)

            If objCalb.MsgB <> "" Then
                hidMessageBarr.Value = objCalb.MsgB
                hidMessageBarr.Value = hidMessageBarr.Value.Replace("\n", "#")
                If hidBarrier.Value = "1" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinRename", "alert('" + objCalb.MsgB + "');", True)
                End If
            Else
                hidMessageBarr.Value = ""
            End If

            lblOTRTemp.Text = "OTR Temperature(" + dsPref.Tables(0).Rows(0).Item("TITLE20").ToString() + "):"
            lblWVTRTemp.Text = "WVTR Temperature(" + dsPref.Tables(0).Rows(0).Item("TITLE20").ToString() + "):"

            txtOTRTemp.Text = OTR
            txtWVTRTemp.Text = WVTR
            txtOTRHumidity.Text = dsPref.Tables(0).Rows(0).Item("OTRRH").ToString()
            txtWVTRHumidity.Text = dsPref.Tables(0).Rows(0).Item("WVTRRH").ToString()


            If hidBarrier.Value = "0" Then
                tblBarrier.Style.Add("Display", "none")
                Barrbtn.ToolTip = "Show Barrier Properties"
            Else
                tblBarrier.Style.Add("Display", "")
                Barrbtn.ToolTip = "Hide Barrier Properties"
            End If

            For i = 1 To 24
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
                        HeaderTdSetting(tdHeader, "150px", "Materials", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        'Material Source
                        Link = New HyperLink
                        Link.Width = 135
                        Link.CssClass = "LinkMatNew"
                        path = "MatSources.aspx"
                        Link.Text = "Material Data Sources"
                        Link.NavigateUrl = "javascript:ShowPopMatWindow('" + path + "')"
                        tdHeader2.Controls.Add(Link)
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "50px", "Grade", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        tdHeader.ID = "GradeHeader"
                        tdHeader.Width = 50
                        tdHeader2.ID = "GradeHeader2"
                        tdHeader1.ID = "GradeHeader1"
                        If hidBarrier.Value = "0" Then
                            tdHeader.Style.Add("Display", "None")
                            tdHeader2.Style.Add("Display", "None")
                            tdHeader1.Style.Add("Display", "None")
                        Else
                            tdHeader.Style.Add("Display", "")
                            tdHeader2.Style.Add("Display", "")
                            tdHeader1.Style.Add("Display", "")
                        End If
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        Title = "(" + dsPref.Tables(0).Rows(0).Item("TITLE1").ToString() + ")"
                        HeaderTdSetting(tdHeader, "100px", "Thickness", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        Title = "" + dsPref.Tables(0).Rows(0).Item("TITLE19").ToString() + ""
                        HeaderTdSetting(tdHeader, "140px", "OTR", "2")
                        tdHeader.Attributes.Add("Title", "Oxygen Transformation Rate")
                        Header2TdSetting(tdHeader1, "76px", "Suggested", "1")
                        If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                            Header2TdSetting(tdHeader2, "", "(cc/ " + Title + "/day)", "2")
                        Else
                            Header2TdSetting(tdHeader2, "", "(cc/100 " + Title + "/day)", "2")
                        End If
                        tdHeader.ID = "OTRHeader"
                        tdHeader2.ID = "OTRHeader2"
                        tdHeader1.ID = "OTRSHeader1"

                        If hidBarrier.Value = "0" Then
                            tdHeader.Style.Add("Display", "None")
                            tdHeader2.Style.Add("Display", "None")
                            tdHeader1.Style.Add("Display", "None")
                        Else
                            tdHeader.Style.Add("Display", "")
                            tdHeader2.Style.Add("Display", "")
                            tdHeader1.Style.Add("Display", "")
                        End If
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 6
                        Header2TdSetting(tdHeader1, "76px", "Preferred", "1")
                        tdHeader1.ID = "OTRPHeader1"
                        If hidBarrier.Value = "0" Then
                            tdHeader1.Style.Add("Display", "None")
                        Else
                            tdHeader1.Style.Add("Display", "")
                        End If
                        trHeader1.Controls.Add(tdHeader1)
                    Case 7
                        Title = "" + dsPref.Tables(0).Rows(0).Item("TITLE19").ToString() + ""
                        HeaderTdSetting(tdHeader, "140px", "WVTR", "2")
                        tdHeader.Attributes.Add("Title", "Water Vapour Transformation Rate")
                        Header2TdSetting(tdHeader1, "76px", "Suggested", "1")
                        If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                            Header2TdSetting(tdHeader2, "", "(gm/" + Title + "/day)", "2")
                        Else
                            Header2TdSetting(tdHeader2, "", "(gm/100 " + Title + "/day)", "2")
                        End If
                        tdHeader.ID = "WVTRHeader"
                        tdHeader2.ID = "WVTRHeader2"
                        tdHeader1.ID = "WVTRSHeader1"
                        If hidBarrier.Value = "0" Then
                            tdHeader.Style.Add("Display", "None")
                            tdHeader2.Style.Add("Display", "None")
                            tdHeader1.Style.Add("Display", "None")
                        Else
                            tdHeader.Style.Add("Display", "")
                            tdHeader2.Style.Add("Display", "")
                            tdHeader1.Style.Add("Display", "")

                        End If
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 8
                        Header2TdSetting(tdHeader1, "76px", "Preferred", "1")
                        tdHeader1.ID = "WVTRPHeader1"
                        If hidBarrier.Value = "0" Then
                            tdHeader1.Style.Add("Display", "None")
                        Else
                            tdHeader1.Style.Add("Display", "")
                        End If
                        trHeader1.Controls.Add(tdHeader1)
                    Case 9
                        Title = "(%)"
                        HeaderTdSetting(tdHeader, "100px", "Recycle", "1")
                        Header2TdSetting(tdHeader2, "0", Title, "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)

                    Case 10
                        Title = "(MJ/" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " mat)"
                        HeaderTdSetting(tdHeader, "", "Energy", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "50px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 11
                        Header2TdSetting(tdHeader1, "80px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 12
                        Title = "(" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " CO2/" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " mat)"
                        HeaderTdSetting(tdHeader, "", "CO2 Equivalent", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "80px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 13
                        Header2TdSetting(tdHeader1, "80px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                        'Changes for Water Start
                    Case 14
                        Title = "(" + dsPref.Tables(0).Rows(0).Item("TITLE10").ToString() + " Water/" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " mat)"
                        HeaderTdSetting(tdHeader, "", "Water", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "80px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 15
                        Header2TdSetting(tdHeader1, "80px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 16
                        Title = "(" + dsPref.Tables(0).Rows(0).Item("TITLE4").ToString() + ")"
                        HeaderTdSetting(tdHeader, "", "Ship Distance", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "70px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 17
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 18
                        Title = "(%)"
                        HeaderTdSetting(tdHeader, "70px", "Extra-process", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 19
                        HeaderTdSetting(tdHeader, "", "Specific Gravity", "2")
                        Header2TdSetting(tdHeader2, "", "", "2")
                        Header2TdSetting(tdHeader1, "70px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 20
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 21
                        Title = "(" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "70px", "Weight/area", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 22
                        Title = "(" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + ") per Shipping unit"
                        HeaderTdSetting(tdHeader, "100px", Title, "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        HeaderTdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 23
                        HeaderTdSetting(tdHeader, "50px", "Shipping Selector", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 24
                        HeaderTdSetting(tdHeader, "110px", "Mfg. Dept.", "1")
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
                For j = 1 To 24
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
                            Link.ID = "hypMatDes" + i.ToString()
                            hid.ID = "hidMatid" + i.ToString()
			    Link.width=120
                            'Link.CssClass = "LinkNewMat"
                            Link.CssClass = "Link"
                            ImgBut = New ImageButton
                            ImgBut.ID = "imgBut" + i.ToString()
                            ImgBut.Width = 6
                            ImgBut.Height = 6
                            ImgDis = New ImageButton
                            ImgDis.ID = "imgDis" + i.ToString()
                            ImgDis.Width = 6
                            ImgDis.Height = 6
                            If dsPref.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString() = "0" Then

                                ImgBut.ImageUrl = "~/Images/595958.png"
                                ImgBut.ToolTip = "User Preferred Material Name"
                                If hidBarrier.Value = "0" Then
                                    ImgBut.Attributes.Add("style", "float:right; margin-bottom:20px; Display:none;")
                                Else
                                    ImgBut.Attributes.Add("style", "float:right; margin-bottom:5px; Display:none;")
                                End If


                                ImgDis.ImageUrl = "~/Images/F1F1F2.png"
                                ImgDis.ToolTip = "System Suggested Material Name"
                                If hidBarrier.Value = "0" Then
                                    ImgDis.Attributes.Add("style", "float:right; margin-bottom:20px; Display:none;")
                                Else
                                    ImgDis.Attributes.Add("style", "float:right; margin-bottom:5px; Display:none;")
                                End If


                                GetMaterialDetailsNew(Link, hid, CInt(dsPref.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()), "hypGradeDes" + i.ToString(), "hidGradeId" + i.ToString(), "SGP" + i.ToString(), ds1)
                                ImgBut.Attributes.Add("onclick", "ShowEditPopWindow('../PopUp/UpdateMaterial.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_hypMatDes" + i.ToString() + "','" + ctlContentPlaceHolder.ClientID.ToString() + "_hidMatid" + i.ToString() + "'); return false;")
                                ImgDis.Attributes.Add("onclick", "ShowEditPopWindow('../PopUp/UpdateMaterial.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_hypMatDes" + i.ToString() + "','" + ctlContentPlaceHolder.ClientID.ToString() + "_hidMatid" + i.ToString() + "'); return false;")
                            Else
                                dvEmat.RowFilter = "MATID=" + dsPref.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()
                                dtEmat = dvEmat.ToTable()
                                If dtEmat.Rows.Count > 0 Then
                                    ImgBut.ImageUrl = "~/Images/595958.png"
                                    ImgBut.ToolTip = "User Preferred Material Name"

                                    ImgDis.ImageUrl = "~/Images/F1F1F2.png"
                                    ImgDis.ToolTip = "System Suggested Material Name"

                                    If hidBarrier.Value = "0" Then
                                        ImgBut.Attributes.Add("style", "float:right; margin-bottom:20px; display:inline;")
                                        ImgDis.Attributes.Add("style", "float:right; margin-bottom:20px; display:none;")
                                    Else
                                        ImgBut.Attributes.Add("style", "float:right; margin-bottom:5px; display:inline;")
                                        ImgDis.Attributes.Add("style", "float:right; margin-bottom:5px; display:none;")
                                    End If
                                    
                                Else
                                    ImgBut.ImageUrl = "~/Images/595958.png"
                                    ImgBut.ToolTip = "User Preferred Material Name"

                                    ImgDis.ImageUrl = "~/Images/F1F1F2.png"
                                    ImgDis.ToolTip = "System Suggested Material Name"

                                    If hidBarrier.Value = "0" Then
                                        ImgBut.Attributes.Add("style", "float:right; margin-bottom:20px; display:none;")
                                        ImgDis.Attributes.Add("style", "float:right; margin-bottom:20px; display:inline;")
                                    Else
                                        ImgBut.Attributes.Add("style", "float:right; margin-bottom:5px; display:none;")
                                        ImgDis.Attributes.Add("style", "float:right; margin-bottom:5px; display:inline;")
                                    End If
                                    
                                End If
                                ImgBut.Attributes.Add("onclick", "ShowEditPopWindow('../PopUp/UpdateMaterial.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_hypMatDes" + i.ToString() + "','" + ctlContentPlaceHolder.ClientID.ToString() + "_hidMatid" + i.ToString() + "'); return false;")
                                ImgDis.Attributes.Add("onclick", "ShowEditPopWindow('../PopUp/UpdateMaterial.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_hypMatDes" + i.ToString() + "','" + ctlContentPlaceHolder.ClientID.ToString() + "_hidMatid" + i.ToString() + "'); return false;")
                                GetMaterialDetailsNew(Link, hid, CInt(dsPref.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()), "hypGradeDes" + i.ToString(), "hidGradeId" + i.ToString(), "SGP" + i.ToString(), ds1)

                                If Link.Text.Length > 23 Then
                                    Link.Attributes.Add("style", "margin-bottom:5px; margin-top:3px;")
                                    If Link.Text.Length > 38 Then
                                        Link.Text = Link.Text.Substring(0, 20) + " " + Link.Text.Substring(20, 15) + "..."
                                    Else
                                        Link.Text = Link.Text.Substring(0, 20) + " " + Link.Text.Substring(20, Link.Text.Length - 20)
                                    End If
                                Else
                                    Link.Attributes.Add("style", "margin-bottom:5px; margin-top:5px;")
                                End If
                            End If
                            'GetMaterialDetailsNew(Link, hid, CInt(dsPref.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()), "hypGradeDes" + i.ToString(), "hidGradeId" + i.ToString(), "SGP" + i.ToString())
                            
                            tdInner.Controls.Add(ImgBut)
                            tdInner.Controls.Add(ImgDis)
                            tdInner.Width = 120
                            tdInner.Height = 20
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Left")
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = "hypGradeDes" + i.ToString()
                            hid.ID = "hidGradeId" + i.ToString()
                            Link.Width = 100
                            Link.CssClass = "Link"
                            GetGrades(Link, hid, "hidMatid" + i.ToString(), CInt(dsPref.Tables(0).Rows(0).Item("GRADE" + i.ToString() + "").ToString()), "SGP" + i.ToString(), dsGrad)
                            tdInner.ID = "GradeVal" + i.ToString()
                            If hidBarrier.Value = "0" Then
                                tdInner.Style.Add("Display", "None")
                            Else
                                tdInner.Style.Add("Display", "")
                            End If
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "THICK" + i.ToString()
                            txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("THICK" + i.ToString() + "").ToString(), 3)
                            txt.MaxLength = 6
                            tdInner.Controls.Add(txt)
                            If i = 1 Then
                                hid = New HiddenField
                                hid.ID = "UnitId"
                                hid.Value = dsPref.Tables(0).Rows(0).Item("UNITS").ToString()
                            End If

                            tdInner.Controls.Add(hid)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            If dsPref.Tables(0).Rows(0).Item("GRADE" + i.ToString() + "").ToString() <> 0 Then
                                If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                    lbl.Text = FormatNumber(CDbl(objCalb.OTRB(i - 1)) / (CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString()) / 1000) / 100, 3)
                                Else
                                    lbl.Text = FormatNumber(objCalb.OTRB(i - 1), 3)
                                End If
                            Else
                                lbl.Text = ""
                            End If
                            tdInner.ID = "OTRSVal" + i.ToString()
                            If hidBarrier.Value = "0" Then
                                tdInner.Style.Add("Display", "None")
                            Else
                                tdInner.Style.Add("Display", "")
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "OTR" + i.ToString()
                            If Not IsNumeric(dsPref.Tables(0).Rows(0).Item("OTR" + i.ToString())) Then
                                txt.Text = ""
                            Else
                                If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                    txt.Text = FormatNumber(CDbl(dsPref.Tables(0).Rows(0).Item("OTR" + i.ToString())) / (CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString()) / 1000) / 100, 3)
                                Else
                                    txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("OTR" + i.ToString()).ToString(), 3)
                                End If
                            End If
                            txt.MaxLength = 6
                            tdInner.ID = "OTRPVal" + i.ToString()
                            If hidBarrier.Value = "0" Then
                                tdInner.Style.Add("Display", "None")
                            Else
                                tdInner.Style.Add("Display", "")
                            End If
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            If dsPref.Tables(0).Rows(0).Item("GRADE" + i.ToString() + "").ToString() <> 0 Then
                                If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                    lbl.Text = FormatNumber(CDbl(objCalb.WVTRB(i - 1)) / (CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString()) / 1000) / 100, 3)
                                Else
                                    lbl.Text = FormatNumber(objCalb.WVTRB(i - 1), 3)
                                End If
                            Else
                                lbl.Text = ""
                            End If
                            tdInner.ID = "WVTRSVal" + i.ToString()
                            If hidBarrier.Value = "0" Then
                                tdInner.Style.Add("Display", "None")
                            Else
                                tdInner.Style.Add("Display", "")
                            End If
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "WVTR" + i.ToString()
                            If Not IsNumeric(dsPref.Tables(0).Rows(0).Item("WVTR" + i.ToString())) Then
                                txt.Text = ""
                            Else
                                If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                    txt.Text = FormatNumber(CDbl(dsPref.Tables(0).Rows(0).Item("WVTR" + i.ToString())) / (CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString()) / 1000) / 100, 3)
                                Else
                                    txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("WVTR" + i.ToString()).ToString(), 3)
                                End If
                            End If
                            txt.MaxLength = 6
                            tdInner.ID = "WVTRPVal" + i.ToString()
                            If hidBarrier.Value = "0" Then
                                tdInner.Style.Add("Display", "None")
                            Else
                                tdInner.Style.Add("Display", "")
                            End If
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 9
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "REC" + i.ToString()
                            txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("R" + i.ToString() + "").ToString(), 2)
                            txt.MaxLength = 6

                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 10
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 50
                            lbl.Text = FormatNumber(dsSug.Tables(0).Rows(0).Item("ERGYS" + i.ToString() + "").ToString(), 1)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 11
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "ERGYP" + i.ToString()
                            txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("ERGYP" + i.ToString() + "").ToString(), 3)
                            txt.MaxLength = 9
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 12
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 50
                            lbl.Text = FormatNumber(dsSug.Tables(0).Rows(0).Item("CO2S" + i.ToString() + "").ToString(), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 13
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "CO2P" + i.ToString()
                            txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("CO2P" + i.ToString() + "").ToString(), 3)
                            txt.MaxLength = 9
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                            'Changes for Water Start
                        Case 14
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 50
                            lbl.Text = FormatNumber(dsSug.Tables(0).Rows(0).Item("WATERS" + i.ToString() + "").ToString(), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 15
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "WATERP" + i.ToString()
                            txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("WATERP" + i.ToString() + "").ToString(), 3)
                            txt.MaxLength = 9
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                            'Changes for Water end
                        Case 16
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 50
                            lbl.Text = FormatNumber(dsSug.Tables(0).Rows(0).Item("SHIPS" + i.ToString() + "").ToString(), 0)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 17
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "SHIPP" + i.ToString()
                            txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("SHIPP" + i.ToString() + "").ToString(), 3)
                            txt.MaxLength = 9
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 18
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "EP" + i.ToString()
                            txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("E" + i.ToString() + "").ToString(), 2)
                            txt.MaxLength = 9
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 19
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 50
                            lbl.Text = FormatNumber(dsSug.Tables(0).Rows(0).Item("SGS" + i.ToString() + "").ToString(), 2)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 20
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "SGP" + i.ToString()
                            txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("SGP" + i.ToString() + "").ToString(), 3)
                            txt.MaxLength = 9
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 21
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(dsSug.Tables(0).Rows(0).Item("WTPARA" + i.ToString() + "").ToString(), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 22
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "MediumTextBox"
                            txt.ID = "SHIPU" + i.ToString()
                            txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("SHIPUNIT" + i.ToString() + "").ToString(), 0)
                            txt.MaxLength = 11
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 23
                            InnerTdSetting(tdInner, "", "Left")
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = "hypShipS" + i.ToString()
                            hid.ID = "hidShipSId" + i.ToString()
                            Link.Width = 50
                            Link.CssClass = "Link"
                            GetShipSelectorDetails(Link, hid, dsPref.Tables(0).Rows(0).Item("SS" + i.ToString() + "").ToString(), dsSP)
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInner.Controls.Add(tdInner)

                        Case 24
                            InnerTdSetting(tdInner, "", "Left")
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = "hypDep" + i.ToString()
                            hid.ID = "hidDepid" + i.ToString()
                            Link.Width = 100
                            Link.CssClass = "Link"
                            GetDeptDetails(Link, hid, CInt(dsPref.Tables(0).Rows(0).Item("D" + i.ToString() + "").ToString()), dsDept)
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
            For i = 1 To 24
                tdInner = New TableCell
                Select Case i
                    Case 1
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.Text = "<span class='CalculatedFeilds'><b> Structure Total  </b></span><span style='color:Red;font-size:14px;'>*</span>"
                        tdInner.Attributes.Add("onmouseover", "Tip('Structure total values do not include waste, recycling, or extra-processing')")
                        tdInner.Attributes.Add("onmouseout", "UnTip('')")
                        trInner.Controls.Add(tdInner)
                    Case 4
                        InnerTdSetting(tdInner, "", "Right")
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(dsSug.Tables(0).Rows(0).Item("TOTTHICK").ToString(), 3) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 5
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.ColumnSpan = 2
                        tdInner.ID = "Total" + i.ToString()
                        If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                            tdInner.Text = FormatNumber(CDbl(objCalb.TotalOTRB) / (CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString()) / 1000) / 100, 3)
                        Else
                            tdInner.Text = FormatNumber(objCalb.TotalOTRB, 3)
                        End If
                        tdInner.Text = "<b>" + tdInner.Text + "</b>"
                        tdInner.Style.Add("text-align", "center")
                        If hidBarrier.Value = "0" Then
                            tdInner.Style.Add("Display", "None")
                        Else
                            tdInner.Style.Add("Display", "")
                        End If
                        trInner.Controls.Add(tdInner)
                    Case 7
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.ColumnSpan = 2
                        tdInner.ID = "Total" + i.ToString()
                        If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                            tdInner.Text = FormatNumber(CDbl(objCalb.TotalWVTRB) / (CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString()) / 1000) / 100, 3)
                        Else
                            tdInner.Text = FormatNumber(objCalb.TotalWVTRB, 3)
                        End If
                        tdInner.Style.Add("text-align", "center")
                        tdInner.Text = "<b>" + tdInner.Text + "</b>"
                        If hidBarrier.Value = "0" Then
                            tdInner.Style.Add("Display", "None")
                        Else
                            tdInner.Style.Add("Display", "")
                        End If
                        trInner.Controls.Add(tdInner)
                    Case 10
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.ColumnSpan = 2
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(dsSug.Tables(0).Rows(0).Item("TOTERGY").ToString(), 3) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 12
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.ColumnSpan = 2
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(dsSug.Tables(0).Rows(0).Item("TOTCO2").ToString(), 3) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 14
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.ColumnSpan = 2
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(dsSug.Tables(0).Rows(0).Item("TOTWATER").ToString(), 3) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 16
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.ColumnSpan = 2
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(dsSug.Tables(0).Rows(0).Item("TOTSHIP").ToString(), 3) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 19
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.ColumnSpan = 2
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(dsSug.Tables(0).Rows(0).Item("TOTSG").ToString(), 2) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 21
                        InnerTdSetting(tdInner, "", "Right")
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(dsSug.Tables(0).Rows(0).Item("TOTWEIGHT").ToString(), 3) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 1, 2, 3, 9, 18, 22, 23, 24
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.ID = "Total" + i.ToString()
                        If hidBarrier.Value = "0" Then
                            If i = 3 Then
                                tdInner.Style.Add("Display", "None")
                            End If
                        Else
                            tdInner.Style.Add("Display", "")
                        End If
                        trInner.Controls.Add(tdInner)
                End Select
            Next
            trInner.Height = 30
            trInner.CssClass = "AlterNateColor2"
            tblComparision.Controls.Add(trInner)

            'P & L Statements
            For i = 1 To 1
                trInner = New TableRow
                For j = 1 To 3
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            'Layer
                            InnerTdSetting(tdInner, "", "Center")
                            tdInner.Text = "<b> Include weight of discrete materials in P&L statements </b>"
                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "50px", "Left")
                            radio = New RadioButton
                            If dsPref.Tables(0).Rows(0).Item("discmatyn") = 1 Then
                                radio.Checked = True
                            End If
                            radio.Text = "Yes"
                            radio.ID = "1"
                            radio.GroupName = "raddiscmatyn"
                            tdInner.Controls.Add(radio)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "50px", "Left")
                            radio = New RadioButton
                            If dsPref.Tables(0).Rows(0).Item("discmatyn") = 0 Then
                                radio.Checked = True
                            End If
                            radio.Text = "No"
                            radio.ID = "0"
                            radio.GroupName = "raddiscmatyn"
                            tdInner.Controls.Add(radio)
                            trInner.Controls.Add(tdInner)

                    End Select
                Next
                trInner.CssClass = "AlterNateColor2"
                trInner.Height = 30
                tblComparision1.Controls.Add(trInner)
            Next

            'Discrete Materails
            trHeader = New TableRow
            trHeader2 = New TableRow
            For i = 1 To 6
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
                        Title = "(" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "80px", "Weight", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        Title = "(MJ/" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " mat)"
                        HeaderTdSetting(tdHeader, "80px", "Energy ", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 5
                        Title = "(" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " CO2/" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " mat)"
                        HeaderTdSetting(tdHeader, "80px", "CO2 Equivalent", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 6
                        Title = "(" + dsPref.Tables(0).Rows(0).Item("TITLE10").ToString() + " Water/" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + " mat)"
                        HeaderTdSetting(tdHeader, "80px", "Water Equivalent", "1")
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
                For j = 1 To 6
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            'Layer
                            InnerTdSetting(tdInner, "", "Left")
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
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = "hypDisMatDes" + i.ToString()
                            hid.ID = "hidDisMatid" + i.ToString()
                            Link.Width = 120
                            Link.CssClass = "Link"
                            GetDiscreteMaterialDetails(Link, hid, CInt(dsPref.Tables(0).Rows(0).Item("DISID" + i.ToString() + "").ToString()), dsDM)
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 50
                            txt.ID = "DISW" + i.ToString()
                            txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("DISWEIGHT" + i.ToString() + "").ToString(), 4)
                            txt.MaxLength = 9
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 50
                            txt.ID = "DISERGY" + i.ToString()
                            txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("DISERGY" + i.ToString() + "").ToString(), 3)
                            txt.MaxLength = 9
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 50
                            txt.ID = "DISCO" + i.ToString()
                            txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("DISCO" + i.ToString() + "").ToString(), 3)
                            txt.MaxLength = 9
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 50
                            txt.ID = "DISWATER" + i.ToString()
                            txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("DISWATER" + i.ToString() + "").ToString(), 3)
                            txt.MaxLength = 9
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
                tblComparision2.Controls.Add(trInner)
            Next

            'Total
            trInner = New TableRow
            For i = 1 To 6
                tdInner = New TableCell
                Select Case i
                    Case 1
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.Text = "<span class='CalculatedFeilds'><b> Total </b></span>"
                        trInner.Controls.Add(tdInner)
                    Case 2
                        InnerTdSetting(tdInner, "", "Left")
                        trInner.Controls.Add(tdInner)

                    Case 3
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(dsSug.Tables(0).Rows(0).Item("TOTDISWEIGHT").ToString(), 4) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 4
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(dsSug.Tables(0).Rows(0).Item("TOTDISERGY").ToString(), 3) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 5
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(dsSug.Tables(0).Rows(0).Item("TOTDISCO2").ToString(), 3) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 6
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(dsSug.Tables(0).Rows(0).Item("TOTDISWATER").ToString(), 3) + " </b> </span>"
                        trInner.Controls.Add(tdInner)

                End Select
            Next
            trInner.Height = 30
            trInner.CssClass = "AlterNateColor1"
            tblComparision2.Controls.Add(trInner)

            'Printing Plates
            trHeader = New TableRow
            tdHeader = New TableCell

            HeaderTdSetting(tdHeader, "", "Printing Plates", "1")
            tdHeader.Style.Add("text-align", "left")
            tdHeader.Style.Add("padding-left", "5px")
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 30
            trHeader.Height = 30
            tblComparision3.Controls.Add(trHeader)



            'Inner
            For i = 1 To 3
                trInner = New TableRow
                tdInner = New TableCell

                InnerTdSetting(tdInner, "230px", "Left")
                radio = New RadioButton
                If dsPref.Tables(0).Rows(0).Item("plate") = (i - 1).ToString() Then
                    radio.Checked = True
                End If
                Select Case i
                    Case 1
                        radio.Text = "not required"
                    Case 2
                        radio.Text = "produced by packaging supplier"
                    Case 3
                        radio.Text = "produced by outside supplier"
                End Select

                radio.ID = i - 1
                radio.GroupName = "radPlate"
                tdInner.Controls.Add(radio)
                trInner.Controls.Add(tdInner)

                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 30
                tblComparision3.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetailsForTab1:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetailsForTab2(ByVal dsPref As DataSet, ByVal dsSug As DataSet)

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

        Dim chk As New CheckBox
        Dim path As String = ""
        Dim radio As New RadioButton

        Dim dsEmat As New DataSet
        Dim dvEmat As New DataView
        Dim dtEmat As New DataTable
        Dim ds As New DataSet
        Dim ds1 As New DataSet
        Try
            ds1 = objGetData.GetMaterials("-1", "", "")

            dsEmat = objGetData.GetEditMaterial(CaseId)
            Session("dsEmat") = dsEmat

            tblBarrier.Style.Add("Display", "none")
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
                        HeaderTdSetting(tdHeader, "180px", "Materials", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        'Material Source
                        Link = New HyperLink
                        Link.Width = 120
                        Link.CssClass = "LinkMatNew"
                        path = "MatSources.aspx"
                        Link.Text = "Material Data Sources"
                        Link.NavigateUrl = "javascript:ShowPopMatWindow('" + path + "')"
                        tdHeader2.Controls.Add(Link)
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "", "Recovery", "2")
                        Header2TdSetting(tdHeader2, "", "(unitless)", "2")
                        Header2TdSetting(tdHeader1, "70px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)

                    Case 5
                        HeaderTdSetting(tdHeader, "", "Sustainable Materials", "2")
                        Header2TdSetting(tdHeader2, "", "(unitless)", "2")
                        Header2TdSetting(tdHeader1, "100px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 6
                        Header2TdSetting(tdHeader1, "100px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 7
                        HeaderTdSetting(tdHeader, "", "PC Recycle", "2")
                        Header2TdSetting(tdHeader2, "", "(unitless)", "2")
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
                            GetMaterialDetails(Nothing, Nothing, CInt(dsPref.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()), lbl, i.ToString(), ds1)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 3
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 50
                            lbl.Text = FormatNumber(dsSug.Tables(0).Rows(0).Item("RECOS" + i.ToString() + "").ToString(), 2)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "RECOP" + i.ToString()
                            txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("RECOP" + i.ToString() + "").ToString(), 2)
                            txt.MaxLength = 9
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 5
                            InnerTdSetting(tdInner, "", "Left")
                            lbl = New Label
                            lbl.Text = dsSug.Tables(0).Rows(0).Item("SUSS" + i.ToString() + "").ToString()
                            lbl.Width = 70
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
                            GetSustainMaterialDetails(Link, hid, dsPref.Tables(0).Rows(0).Item("SUSP" + i.ToString() + "").ToString())
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Width = 50
                            lbl.Text = FormatNumber(dsSug.Tables(0).Rows(0).Item("PCRECS" + i.ToString() + "").ToString(), 2)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "PCRECP" + i.ToString()
                            txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("PCRECP" + i.ToString() + "").ToString(), 2)
                            txt.MaxLength = 9
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
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.ColumnSpan = 2
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(dsSug.Tables(0).Rows(0).Item("TOTRECOV").ToString(), 3) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 5
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.ColumnSpan = 2
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(dsSug.Tables(0).Rows(0).Item("TOTSUSMAT").ToString(), 3) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 7
                        InnerTdSetting(tdInner, "", "Center")
                        tdInner.ColumnSpan = 2
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(dsSug.Tables(0).Rows(0).Item("TOTREC").ToString(), 3) + " </b> </span>"
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

    Protected Sub GetMaterialDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal MatId As Integer, ByVal lbl As Label, ByVal str As String, ByVal ds1 As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New S1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dsEmat As New DataSet
        Dim dv As New DataView
        Dim dt As New DataTable
        Dim dv1 As New DataView
        Dim dt1 As New DataTable
        Try
            dsEmat = DirectCast(Session("dsEmat"), DataSet)
            'Ds = ObjGetdata.GetMaterials(MatId, "", "")
            dv = dsEmat.Tables(0).DefaultView

            dv1 = ds1.Tables(0).DefaultView
            dv1.RowFilter = "MATID= " + MatId.ToString()
            dt1 = dv1.ToTable()

            If Not IsNothing(LinkMat) Then
                dv.RowFilter = "MATID=" + MatId.ToString()
                dt = dv.ToTable()
                If dt.Rows.Count > 0 Then
                    LinkMat.Text = dt.Rows(0).Item("MATDES").ToString().Replace("&#", "'")
                    LinkMat.ToolTip = dt.Rows(0).Item("MATDES").ToString().Replace("&#", "'")
                Else
                    LinkMat.Text = dt1.Rows(0).Item("MATDES").ToString()
                    LinkMat.ToolTip = dt1.Rows(0).Item("MATDES").ToString()
                End If

                LinkMat.Attributes.Add("text-decoration", "none")
                hid.Value = MatId.ToString()
                Path = "../PopUp/GetMatPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
                LinkMat.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"
            ElseIf Not IsNothing(lbl) Then
                dv.RowFilter = "MATID=" + MatId.ToString()
                dt = dv.ToTable()
                If dt.Rows.Count > 0 Then
                    lbl.Text = dt.Rows(0).Item("MATDES").ToString().Replace("&#", "'")
                    lbl.ID = MatId.ToString()
                Else
                    lbl.Text = dt1.Rows(0).Item("MATDES").ToString()
                    lbl.ID = MatId.ToString()
                End If
            End If


        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetMaterialDetailsNew(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal MatId As Integer, ByVal linkGrade As String, ByVal hidGrade As String, _
                                        ByVal SG As String, ByVal ds1 As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New S1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dsEmat As New DataSet
        Dim str As String = ""
        Dim dv As New DataView
        Dim dt As New DataTable
        Dim dv1 As New DataView
        Dim dt1 As New DataTable

        Try
            dsEmat = DirectCast(Session("dsEmat"), DataSet)
            dv = dsEmat.Tables(0).DefaultView
            str = hidGrade(hidGrade.Length - 1)
            If str = "0" Then
                str = "10"
            End If

            'Ds = ObjGetdata.GetMaterials(MatId, "", "")
            dv1 = ds1.Tables(0).DefaultView
            dv1.RowFilter = "MATID= " + MatId.ToString()
            dt1 = dv1.ToTable()

            If dt1.Rows(0).Item("MATDES").ToString().Length > 25 Then
                'LinkMat.Font.Size = 8
            End If

            If MatId <> 0 Then
                dv.RowFilter = "MATID=" + MatId.ToString()
                dt = dv.ToTable()
                If dt.Rows.Count > 0 Then
                    LinkMat.Text = dt.Rows(0).Item("MATDES").ToString().Replace("&#", "'")
                    LinkMat.ToolTip = dt.Rows(0).Item("MATDES").ToString().Replace("&#", "'")
                    LinkMat.Attributes.Add("text-decoration", "none")
                Else
                    'Ds = ObjGetdata.GetMaterials(MatId, "", "")
                    If dt1.Rows(0).Item("MATDES").ToString().Length > 25 Then
                        'LinkMat.Font.Size = 8
                    End If
                    LinkMat.Text = dt1.Rows(0).Item("MATDES").ToString()
                    LinkMat.ToolTip = dt1.Rows(0).Item("MATDES").ToString()
                    LinkMat.Attributes.Add("text-decoration", "none")
                End If

            Else
                'Ds = ObjGetdata.GetMaterials(MatId, "", "")
                If dt1.Rows(0).Item("MATDES").ToString().Length > 25 Then
                    'LinkMat.Font.Size = 8
                End If
                LinkMat.Text = dt1.Rows(0).Item("MATDES").ToString()
                LinkMat.ToolTip = dt1.Rows(0).Item("MATDES").ToString()
                LinkMat.Attributes.Add("text-decoration", "none")
            End If


            hid.Value = MatId.ToString()
            Path = "../PopUp/GetMatPopUpGrade.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "&GradeDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + linkGrade + "&GradeId=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hidGrade + "&SG=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + SG + "&CaseId=" + Session("S1CaseId") + ""
            LinkMat.NavigateUrl = "#"
            LinkMat.Attributes.Add("onClick", "javascript:return ShowMatPopWindow(this,'" + Path + "','" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "')")

        Catch ex As Exception
            ErrorLable.Text = "Error:GetMaterialDetailsNew:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetGrades(ByRef LinkGrade As HyperLink, ByVal hid As HiddenField, ByVal MatId As String, ByVal GradeId As Integer, ByVal SG As String, ByVal dsGrad As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New S1GetData.Selectdata()
        Dim Path As String = String.Empty
        Dim MaterialId As String = String.Empty
        Dim dvGrad As New DataView
        Dim dtGrad As New DataTable
        Try
            'Ds = ObjGetdata.GetGradesVal(GradeId.ToString())

            dvGrad = dsGrad.Tables(0).DefaultView '
            dvGrad.RowFilter = "GRADEID= " + GradeId.ToString() '
            dtGrad = dvGrad.ToTable() '


            LinkGrade.Text = dtGrad.Rows(0).Item("GRADENAME").ToString()
            LinkGrade.ToolTip = dtGrad.Rows(0).Item("GRADENAME").ToString()
            hid.Value = dtGrad.Rows(0).Item("GRADEID").ToString()
            LinkGrade.Attributes.Add("text-decoration", "none")
            MaterialId = ctlContentPlaceHolder.ClientID.ToString() + "_" + MatId
            Path = "../PopUp/GetGradeDetails.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkGrade.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "&SG=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + SG + ""
            LinkGrade.NavigateUrl = "javascript:ShowGradePopWindow('" + Path + "','" + MaterialId + "')"
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetSustainMaterialDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal MatDes As String)
        Dim Ds As New DataSet
        Dim ObjGetdata As New S1GetData.Selectdata()
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
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetDeptDetails(ByRef LinkDep As HyperLink, ByVal hid As HiddenField, ByVal ProcId As Integer, ByVal dsDept As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New S1GetData.Selectdata()
        Dim Path As String = String.Empty
        Dim dvDept As New DataView
        Dim dtDept As New DataTable
        Try

            'Ds = ObjGetdata.GetDept(ProcId, "", "")
            dvDept = dsDept.Tables(0).DefaultView '
            dvDept.RowFilter = "PROCID= " + ProcId.ToString() '
            dtDept = dvDept.ToTable() '


            Path = "../PopUp/GetDepPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkDep.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
            If dtDept.Rows.Count = 0 Then
                LinkDep.Text = "Dept. Conflict"
                LinkDep.ForeColor = Drawing.Color.DarkRed
            Else
                LinkDep.Text = dtDept.Rows(0).Item("PROCDE").ToString()
            End If


            hid.Value = ProcId.ToString()
            LinkDep.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetShipSelectorDetails(ByRef LinkShip As HyperLink, ByVal hid As HiddenField, ByVal ShipId As Integer, ByVal dsSP As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New S1GetData.Selectdata()
        Dim Path As String = String.Empty
        Dim dvSP As New DataView
        Dim dtSP As New DataTable
        Try
            'Ds = ObjGetdata.GetShipingSelector(ShipId, "")

            dvSP = dsSP.Tables(0).DefaultView '
            dvSP.RowFilter = "SHIPID= " + ShipId.ToString() '
            dtSP = dvSP.ToTable() '


            Path = "../PopUp/GetShipPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkShip.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""

            LinkShip.Text = dtSP.Rows(0).Item("shipdes").ToString()



            hid.Value = ShipId.ToString()
            LinkShip.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetDiscreteMaterialDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal MatDisId As Integer, ByVal dsDM As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New S1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvDM As New DataView
        Dim dtDM As New DataTable
        Try
            'Ds = ObjGetdata.GetDiscMaterials(MatDisId, "")
            dvDM = dsDM.Tables(0).DefaultView '
            dvDM.RowFilter = "MATDISID= " + MatDisId.ToString() '
            dtDM = dvDM.ToTable() '

            LinkMat.Text = dtDM.Rows(0).Item("matDISde1").ToString()
            LinkMat.ToolTip = dtDM.Rows(0).Item("matDISde1").ToString()
            LinkMat.Attributes.Add("text-decoration", "none")

            hid.Value = MatDisId.ToString()
            Path = "../PopUp/GetMatDisPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
            LinkMat.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
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
                lnk.Text = "Material & Structure Tab " + i.ToString()
                lnk.CssClass = "TabLink"
                lnk.ID = "lnkMat" + i.ToString()
                If ViewState("TabId").ToString() = "1" Then
                    lnk.Attributes.Add("onclick", "return CheckForMaterialPageBarr('" + ctlContentPlaceHolder.ClientID + "','hidMatid','THICK','SHIPU','hidShipSId','hypShipS','hidDepid','hypDep','divVariable','rdScrollOff','OTRTEMP','RH','txtOTRTemp','txtWVTRTemp','txtOTRHumidity','txtWVTRHumidity');")
                    'lnk.Attributes.Add("onclick", "return CheckForMaterialPage('" + ctlContentPlaceHolder.ClientID + "','hidMatid','THICK','SHIPU','hidShipSId','hypShipS','hidDepid','hypDep','divVariable','rdScrollOff');")
                ElseIf ViewState("TabId").ToString() = "2" Then
                    lnk.Attributes.Add("onclick", "return checkNumericAll();")
                End If

                AddHandler lnk.Click, AddressOf lnkmat_Click
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
    Protected Sub lnkmat_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim lnk As LinkButton = DirectCast(sender, System.Web.UI.WebControls.LinkButton)
            UpdatePage()
            ViewState("TabId") = Convert.ToInt32(lnk.ID.Replace("lnkMat", "").ToString())
            If Not IsNothing(ViewState("TabId")) Then
                If ViewState("TabId").ToString() = "1" Then
                    Updatebtn.Attributes.Add("onclick", "return CheckForMaterialPageBarr('" + ctlContentPlaceHolder.ClientID + "','hidMatid','THICK','SHIPU','hidShipSId','hypShipS','hidDepid','hypDep','divVariable','rdScrollOff','OTRTEMP','RH','txtOTRTemp','txtWVTRTemp','txtOTRHumidity','txtWVTRHumidity');")
                    'Updatebtn.Attributes.Add("onclick", "return CheckForMaterialPage('" + ctlContentPlaceHolder.ClientID + "','hidMatid','THICK','SHIPU','hidShipSId','hypShipS','hidDepid','hypDep','divVariable','rdScrollOff');")
                ElseIf ViewState("TabId").ToString() = "2" Then
                    Updatebtn.Attributes.Add("onclick", "return checkNumericAll();")
                End If
            End If
            CreateTab()
            GetPageDetails()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub UpdatePage()

        Dim Material(9) As String
        Dim Thickness(9) As String
        Dim Recycle(9) As String
        Dim ERGYP(9) As String
        Dim CO2P(9) As String
        Dim WATER(9) As String
        Dim SHIPP(9) As String
        Dim RECOP(9) As String
        Dim SUSMat(9) As String
        Dim PCRECP(9) As String
        Dim EP(9) As String
        Dim SGP(9) As String
        Dim SHIPU(9) As String
        Dim SHIPS(9) As String

        Dim Dept(9) As String
        Dim raddiscmatyn As String
        Dim plate As String
        Dim i As New Integer
        Dim MaterialDis(2) As String
        Dim WeightDis(2) As String
        Dim ErgyDis(2) As String
        Dim CO2Dis(2) As String
        Dim WaterDis(2) As String

        Dim ObjUpIns As New S1UpInsData.UpdateInsert
        Dim DisCount As New Integer

        Dim ProductFormt As New DataSet
        Dim ObjGetData As New S1GetData.Selectdata()
        ProductFormt = ObjGetData.GetProductFromatIn(CaseId)
        Dim obj As New CryptoHelper

        Dim OTR(9) As String
        Dim WVTR(9) As String
        Dim GRADE(9) As String
        Try
            If Not objRefresh.IsRefresh Then
                'Update for First Tab
                If ViewState("TabId").ToString() = "1" Then
                    For i = 1 To 10
                        Material(i - 1) = Request.Form("ctl00$Sustain1ContentPlaceHolder$hidMatid" + i.ToString() + "")
                        Thickness(i - 1) = Request.Form("ctl00$Sustain1ContentPlaceHolder$THICK" + i.ToString() + "")
                        Recycle(i - 1) = Request.Form("ctl00$Sustain1ContentPlaceHolder$REC" + i.ToString() + "")
                        ERGYP(i - 1) = Request.Form("ctl00$Sustain1ContentPlaceHolder$ERGYP" + i.ToString() + "")
                        CO2P(i - 1) = Request.Form("ctl00$Sustain1ContentPlaceHolder$CO2P" + i.ToString() + "")
                        SHIPP(i - 1) = Request.Form("ctl00$Sustain1ContentPlaceHolder$SHIPP" + i.ToString() + "")
                        EP(i - 1) = Request.Form("ctl00$Sustain1ContentPlaceHolder$EP" + i.ToString() + "")
                        SGP(i - 1) = Request.Form("ctl00$Sustain1ContentPlaceHolder$SGP" + i.ToString() + "")
                        SHIPU(i - 1) = Request.Form("ctl00$Sustain1ContentPlaceHolder$SHIPU" + i.ToString() + "")
                        SHIPS(i - 1) = Request.Form("ctl00$Sustain1ContentPlaceHolder$hidShipSId" + i.ToString() + "")
                        Dept(i - 1) = Request.Form("ctl00$Sustain1ContentPlaceHolder$hidDepid" + i.ToString() + "")
                        WATER(i - 1) = Request.Form("ctl00$Sustain1ContentPlaceHolder$WATERP" + i.ToString() + "")

                        OTR(i - 1) = Request.Form("ctl00$Sustain1ContentPlaceHolder$OTR" + i.ToString() + "")
                        WVTR(i - 1) = Request.Form("ctl00$Sustain1ContentPlaceHolder$WVTR" + i.ToString() + "")
                        GRADE(i - 1) = Request.Form("ctl00$Sustain1ContentPlaceHolder$hidGradeId" + i.ToString() + "")

                        'Check For IsNumric
                        If Not IsNumeric(Thickness(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                        If Not IsNumeric(Recycle(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                        If Not IsNumeric(ERGYP(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                        If Not IsNumeric(CO2P(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                        If Not IsNumeric(WATER(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                        If Not IsNumeric(SHIPP(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                        
                        If Not IsNumeric(EP(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                        If Not IsNumeric(SGP(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                        If Not IsNumeric(SHIPU(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If



                        'Check For Dependant-Indepdant Error
                        If CInt(Material(i - 1)) <> 0 Then
                            'Checking Thickness
                            If CDbl(Thickness(i - 1)) <= CDbl(0.0) Then
                                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE101").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                            End If

                            'Checking Dept.
                            If CDbl(Dept(i - 1)) <= 0 Then
                                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE104").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                            End If

                            'Checking Shipping Unit.
                            If CDbl(SHIPU(i - 1)) <= 0 Then
                                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE102").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                            End If

                            'Checking Shipping Type.
                            If CDbl(SHIPS(i - 1)) <= 0 Then
                                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE114").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                            End If


                        End If
                    Next

                    For i = 1 To 3

                        MaterialDis(i - 1) = Request.Form("ctl00$Sustain1ContentPlaceHolder$hidDisMatid" + i.ToString() + "")
                        WeightDis(i - 1) = Request.Form("ctl00$Sustain1ContentPlaceHolder$DISW" + i.ToString() + "")
                        ErgyDis(i - 1) = Request.Form("ctl00$Sustain1ContentPlaceHolder$DISERGY" + i.ToString() + "")
                        CO2Dis(i - 1) = Request.Form("ctl00$Sustain1ContentPlaceHolder$DISCO" + i.ToString() + "")
                        WaterDis(i - 1) = Request.Form("ctl00$Sustain1ContentPlaceHolder$DISWATER" + i.ToString() + "")

                        'Check For IsNumric
                        If Not IsNumeric(WeightDis(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If

                        If Not IsNumeric(ErgyDis(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                        If Not IsNumeric(CO2Dis(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If

                        If Not IsNumeric(WaterDis(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If

                        If Request.Form("ctl00$Sustain1ContentPlaceHolder$DISW" + i.ToString() + "") <> 0 Then
                            If CInt(ProductFormt.Tables(0).Rows(0).Item("M1")) = 1 Or CInt(ProductFormt.Tables(0).Rows(0).Item("M1")) = 17 Then
                                DisCount = DisCount + 1
                            End If
                        End If

                    Next


                    plate = Request.Form("ctl00$Sustain1ContentPlaceHolder$radPlate")

                    raddiscmatyn = Request.Form("ctl00$Sustain1ContentPlaceHolder$raddiscmatyn")

                    If DisCount = 0 Then
                        ObjUpIns.ExtrusionUpdate(CaseId, Material, Thickness, Recycle, ERGYP, CO2P, SHIPP, RECOP, SUSMat, PCRECP, EP, SGP, SHIPU, SHIPS, Dept, raddiscmatyn, plate, MaterialDis, WeightDis, ErgyDis, CO2Dis, "1", WATER, WaterDis, True)
                    Else
                        ObjUpIns.ExtrusionUpdate(CaseId, Material, Thickness, Recycle, ERGYP, CO2P, SHIPP, RECOP, SUSMat, PCRECP, EP, SGP, SHIPU, SHIPS, Dept, raddiscmatyn, plate, MaterialDis, WeightDis, ErgyDis, CO2Dis, "1", WATER, WaterDis, False)
                    End If
                    ObjUpIns.BarrierUpdateNew(CaseId, txtOTRTemp.Text, txtWVTRTemp.Text, txtOTRHumidity.Text, txtWVTRHumidity.Text, OTR, WVTR, GRADE, hidBarrier.Value)
                ElseIf ViewState("TabId").ToString() = "2" Then
                    For i = 1 To 10
                        RECOP(i - 1) = Request.Form("ctl00$Sustain1ContentPlaceHolder$RECOP" + i.ToString() + "")
                        SUSMat(i - 1) = Request.Form("ctl00$Sustain1ContentPlaceHolder$hidSusMatId" + i.ToString() + "")
                        PCRECP(i - 1) = Request.Form("ctl00$Sustain1ContentPlaceHolder$PCRECP" + i.ToString() + "")

                        If Not IsNumeric(RECOP(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                        If Not IsNumeric(PCRECP(i - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                    Next
                    ObjUpIns.ExtrusionUpdate(CaseId, Material, Thickness, Recycle, ERGYP, CO2P, SHIPP, RECOP, SUSMat, PCRECP, EP, SGP, SHIPU, SHIPS, Dept, raddiscmatyn, plate, MaterialDis, WeightDis, ErgyDis, CO2Dis, "2", WATER, WaterDis, True)
                    
                End If
                Calculate()
                'Update Server Date
                ObjUpIns.ServerDateUpdate(CaseId, Session("S1UserName"))
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:Update:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            If Not objRefresh.IsRefresh Then
                UpdatePage()
            End If
            GetPageDetails()
            'GetScrollDetails()
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
