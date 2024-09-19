Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E1GetData
Imports E1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Econ1_Assumptions_Extrusion
    Inherits System.Web.UI.Page
    Public CaseDes As String = String.Empty
    Shared objCalb As New BarrierGetData.Calculations
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
    Dim _btnInk As ImageButton

    Public Property Inkbtn() As ImageButton
        Get
            Return _btnInk
        End Get
        Set(ByVal value As ImageButton)
            _btnInk = value
        End Set
    End Property
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
        GetContentPlaceHolder()
        GetErrorLable()
        GetUpdatebtn()
        GetLogOffbtn()
        GetInstructionsbtn()
        GetChartbtn()
        GetFeedbackbtn()
        GetNotesbtn()
        GetMainHeadingdiv()
        GetInkbtn()
        GetBarPropbtn()
    End Sub
    Protected Sub GetBarPropbtn()
        Inkbtn = Page.Master.FindControl("imgBarProp")
        Inkbtn.Visible = True
        Inkbtn.ToolTip = "Show Barrier Properties"
        Inkbtn.OnClientClick = "return CallBackBarrier();"
    End Sub
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Visible = True
        Updatebtn.Attributes.Add("onclick", "return CheckForMaterialPageBarrierNew('" + ctlContentPlaceHolder.ClientID + "','hidMatid','T','hidDepid','hypDep','OTRTEMP','RH','txtOTRTemp','txtWVTRTemp','txtOTRHumidity','txtWVTRHumidity');")
        'Updatebtn.Attributes.Add("onclick", "return CheckForMaterialPage('" + ctlContentPlaceHolder.ClientID + "','hidMatid','T','hidDepid','hypDep');")
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
        Chartbtn.Visible = True
        Chartbtn.OnClientClick = "return Chart('../../../Charts/MaterialPrice.aspx','MatPriceChart');"
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
        MainHeading.Attributes.Add("onmouseover", "Tip('Material Assumption')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        MainHeading.InnerHtml = "Econ1 - Material Assumption"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Econ1ContentPlaceHolder")
    End Sub

    Protected Sub GetInkbtn()
        Inkbtn = Page.Master.FindControl("imgInkCost")
        Inkbtn.Visible = True
        Inkbtn.ToolTip = "INK Cost Assistant"
        Inkbtn.OnClientClick = "return Chart('../Popup/InkCost.aspx','MatPriceChart');"
    End Sub
#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_ECON1_ASSUMPTIONS_EXTRUSION")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("INTUSR") = "Y" Then
                lnkSelBulkModel.Visible = True
            End If
            GetMasterPageControls()
            GetSessionDetails()
            GetChartbtn()
            If Not IsPostBack Then
                hidBarrier.Value = "0"
                Dim objUpdateData As New E1UpInsData.UpdateInsert
                Try
                    'objUpdateData.GradeUpdateNew(CaseId)
                Catch ex As Exception
                End Try
                GetTempRH()
                GetPageDetails()
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub
    Protected Sub GetTempRH()
        Dim ds1 As New DataSet()
        Dim ds2 As New DataSet()
        Dim ds3 As New DataSet()
        Dim objGetData As New E1GetData.Selectdata()
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
        ' Dim ds As New DataSet
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
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell


        Dim lbl As New Label
        Dim hid As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell

        Dim chk As New CheckBox

        Dim radio As New RadioButton

        Dim OTRFlag As String
        Dim WVTRFlag As String
        Dim OTR As String
        Dim WVTR As String

        Dim dsPref As New DataSet
        Dim dsSug As New DataSet

        Dim ImgBut As New ImageButton
        Dim ImgDis As New ImageButton
        Dim dsEmat As New DataSet
        Dim dvEmat As New DataView
        Dim dtEmat As New DataTable
        Dim lnkCount As New Integer

        'Changes for AWS
        Dim DsGrade As New DataSet()
        Dim DsMat As New DataSet()
        Dim DsDiscMat As New DataSet()
        Dim DsDept As New DataSet()
        'ENd

        Try
            'Changes for AWS
            DsMat = objGetData.GetMaterials("-1", "", "")
            DsDept = objGetData.GetDept("-1", "", "")
            DsDiscMat = objGetData.GetDiscMaterials("-1", "")
            DsGrade = objGetData.GetAllGradesVal("-1")
            'ENd

            dsPref = objGetData.GetExtrusionDetailsBarrP(CaseId)
            dsSug = objGetData.GetExtrusionDetailsBarrS(CaseId)

            dsEmat = objGetData.GetEditMaterial(CaseId)
            dvEmat = dsEmat.Tables(0).DefaultView
            Session("dsEmat") = dsEmat

            'Calculate $/msi and $/lb total
            Dim dsMatInput As New DataSet
            Dim l, m As Integer
            Dim arrCostPerMsi(10) As String
            Dim CostPerMsiTotal As Double = 0.0
            Dim CostPerLBTotal As Double = 0.0

            For l = 1 To 10
                'Calculation for Cost Per Area
                If dsPref.Tables(0).Rows(0).Item("PRP" + l.ToString() + "").ToString() <> "0" Then
                    arrCostPerMsi(l) = CDbl(dsPref.Tables(0).Rows(0).Item("PRP" + l.ToString()).ToString()) * CDbl(dsSug.Tables(0).Rows(0).Item("WTPARA" + l.ToString()).ToString())
                Else
                    arrCostPerMsi(l) = CDbl(dsSug.Tables(0).Rows(0).Item("PRS" + l.ToString()).ToString()) * CDbl(dsSug.Tables(0).Rows(0).Item("WTPARA" + l.ToString()).ToString())
                End If
            Next

            For m = 1 To 10
                If CDbl(dsPref.Tables(0).Rows(0).Item("M" + m.ToString() + "").ToString()) <> 0 Then
                    If dsPref.Tables(0).Rows(0).Item("PRP" + m.ToString() + "").ToString() <> "0" Then
                        CostPerLBTotal = CostPerLBTotal + (CDbl(dsPref.Tables(0).Rows(0).Item("PRP" + m.ToString()).ToString()) * CDbl(dsSug.Tables(0).Rows(0).Item("WTPARA" + m.ToString()).ToString() / dsSug.Tables(0).Rows(0).Item("WTPERAREA").ToString()))
                    Else
                        CostPerLBTotal = CostPerLBTotal + (CDbl(dsSug.Tables(0).Rows(0).Item("PRS" + m.ToString()).ToString()) * CDbl(dsSug.Tables(0).Rows(0).Item("WTPARA" + m.ToString()).ToString() / dsSug.Tables(0).Rows(0).Item("WTPERAREA").ToString()))
                    End If
                    CostPerMsiTotal = CostPerMsiTotal + (CDbl(arrCostPerMsi(m)))
                    'CostPerMsiTotal = CostPerMsiTotal + (CDbl(arrCostPerMsi(m)) * CDbl(dsSug.Tables(0).Rows(0).Item("WTPARA" + m.ToString()).ToString() / dsSug.Tables(0).Rows(0).Item("WTPERAREA").ToString()))
                End If
            Next



            'Barrier
            If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() <> 1 Then
                OTR = dsPref.Tables(0).Rows(0).Item("OTRTEMP").ToString()
                WVTR = dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString()
            Else
                OTR = (CDbl(dsPref.Tables(0).Rows(0).Item("OTRTEMP").ToString()) - 32) * 5 / 9
                WVTR = (CDbl(dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString()) - 32) * 5 / 9
            End If


            Dim Grades(9) As String
            Dim Materials(9) As String
            Dim OtrVal(9) As String
            Dim WVTRVal(9) As String
            Dim Thick(9) As String
            Dim ISADJTHICK(9) As String
            For i = 0 To 9
                Grades(i) = dsPref.Tables(0).Rows(0).Item("GRADE" + (i + 1).ToString() + "").ToString()
                Materials(i) = dsSug.Tables(0).Rows(0).Item("MATS" + (i + 1).ToString() + "").ToString()
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

            txtOTRTemp.Text = OTR 'ds.Tables(0).Rows(0).Item("OTRTEMP").ToString()
            txtWVTRTemp.Text = WVTR 'ds.Tables(0).Rows(0).Item("WVTRTEMP").ToString()
            txtOTRHumidity.Text = dsPref.Tables(0).Rows(0).Item("OTRRH").ToString()
            txtWVTRHumidity.Text = dsPref.Tables(0).Rows(0).Item("WVTRRH").ToString()


            If hidBarrier.Value = "0" Then
                tblBarrier.Style.Add("Display", "none")
            Else
                tblBarrier.Style.Add("Display", "")
            End If


            For i = 1 To 17
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "60px", "Layers", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "120px", "Primary Materials", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        lbl = New Label
                        lbl.Width = 135
                        lbl.Text = ""
                        'lbl.Style.Add("Display", "none")
                        ' path = "MatSources.aspx"

                        tdHeader2.Controls.Add(lbl)
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
                        HeaderTdSetting(tdHeader, "70px", "Thickness", "1")
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
                        Title = "(" + dsPref.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "", "Price", "2")
                        tdHeader.Attributes.Add("onmouseover", "Tip('Date:" + dsPref.Tables(0).Rows(0).Item("EDATE").ToString() + "')")
                        tdHeader.Attributes.Add("onmouseout", "UnTip('')")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "50px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 10
                        Header2TdSetting(tdHeader1, "85px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 11
                        HeaderTdSetting(tdHeader, "50px", "Recycle", "1")
                        Header2TdSetting(tdHeader2, "", "(%)", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 12
                        HeaderTdSetting(tdHeader, "50px", "Extra-process", "1")
                        Header2TdSetting(tdHeader2, "", "(%)", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 13
                        HeaderTdSetting(tdHeader, "", "Specific Gravity", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "50px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 14
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 15
                        Title = "(" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + "/" + dsPref.Tables(0).Rows(0).Item("TITLE3").ToString() + ")"
                        HeaderTdSetting(tdHeader, "50px", "Weight/Area", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 16
                        Title = "(" + dsPref.Tables(0).Rows(0).Item("TITLE2").ToString() + "/" + dsPref.Tables(0).Rows(0).Item("TITLE3").ToString() + ")"
                        HeaderTdSetting(tdHeader, "50px", "Cost/Area", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 17
                        tdHeader.ID = "S111"
                        tdHeader2.ID = "S222"
                        tdHeader1.ID = "S333"
                        HeaderTdSetting(tdHeader, "100px", "Mfg. Dept.", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                End Select

            Next
            trHeader.Height = 30
            trHeader.Height = 30

            tblComparision.Controls.Add(trHeader)
            tblComparision.Controls.Add(trHeader2)
            tblComparision.Controls.Add(trHeader1)



            'Inner
            For i = 1 To 10
                trInner = New TableRow
                For j = 1 To 17
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
                            Link.Width = 120
                            'Link.CssClass = "LinkMat"
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


                                GetMaterialDetailsNew(Link, hid, CInt(dsPref.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()), "hypGradeDes" + i.ToString(), "hidGradeId" + i.ToString(), "SG" + i.ToString(), DsMat)

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
                                GetMaterialDetailsNew(Link, hid, CInt(dsPref.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()), "hypGradeDes" + i.ToString(), "hidGradeId" + i.ToString(), "SG" + i.ToString(), DsMat)
                                lnkCount = Link.Text.Length
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
                            GetGrades(Link, hid, "hidMatid" + i.ToString(), CInt(dsPref.Tables(0).Rows(0).Item("GRADE" + i.ToString() + "").ToString()), "SG" + i.ToString(), DsGrade)

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
                            txt.ID = "T" + i.ToString()
                            txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("THICK" + i.ToString() + "").ToString(), 3)
                            txt.MaxLength = 6
                            tdInner.Controls.Add(txt)
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
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            If dsSug.Tables(0).Rows(0).Item("PRS" + i.ToString() + "").ToString() <> "" Then
                                lbl.Text = FormatNumber(dsSug.Tables(0).Rows(0).Item("PRS" + i.ToString() + "").ToString(), 4)
                            Else
                                lbl.Text = dsSug.Tables(0).Rows(0).Item("PRS" + i.ToString() + "").ToString()
                            End If
                            If i = 1 Then
                                hid = New HiddenField
                                hid.ID = "UnitId"
                                hid.Value = dsPref.Tables(0).Rows(0).Item("UNITS").ToString()
                            End If

                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 10
                            InnerTdSetting(tdInner, "", "Center")
                            ImgBut = New ImageButton
                            ImgBut.ID = "imgPriceBut" + i.ToString()
                            ImgBut.Width = 6
                            ImgBut.Height = 6
                            ImgDis = New ImageButton
                            ImgDis.ID = "imgPriceDis" + i.ToString()
                            ImgDis.Width = 6
                            ImgDis.Height = 6

                            If dsPref.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString() = "0" Then
                                ImgBut.ImageUrl = "~/Images/595958.png"
                                'ImgBut.ToolTip = "User Preferred Material Price"
                                ImgDis.ImageUrl = "~/Images/595958-b.png"
                                If hidBarrier.Value = "0" Then
                                    ImgBut.Attributes.Add("style", "float:right; margin-bottom:20px; Display:none;")
                                    ImgDis.Attributes.Add("style", "position: relative; bottom: 0px; right: 10px; display:none;")
                                Else
                                    ImgBut.Attributes.Add("style", "float:right; margin-bottom:5px; Display:none;")
                                    ImgDis.Attributes.Add("style", "position: absolute; bottom: 0px; right: 10px; display:none;")
                                End If
                                ImgBut.Attributes.Add("onclick", "ShowPriceWindow('../PopUp/MaterialPrice.aspx','" + ctlContentPlaceHolder.ClientID.ToString() + "_hidMatid" + i.ToString() + "'); return false;")
                                ImgDis.Attributes.Add("onclick", "ShowPriceWindow('../PopUp/" + dsSug.Tables(0).Rows(0).Item("PAGEURL" + i.ToString() + "").ToString() + "','" + ctlContentPlaceHolder.ClientID.ToString() + "_hidMatid" + i.ToString() + "'); return false;")
                            Else
                                ImgBut.ImageUrl = "~/Images/595958.png"
                                'ImgBut.ToolTip = "User Preferred Material Price"
                                ImgDis.ImageUrl = "~/Images/595958-b.png"

                                If hidBarrier.Value = "0" Then
                                    If lnkCount > 23 Then
                                        ImgBut.Attributes.Add("style", "float:right; margin-bottom:20px; display:inline;")
                                        ImgDis.Attributes.Add("style", "float:right; position: relative; top: 28px; right: -6px; display:inline;")
                                    Else
                                        ImgBut.Attributes.Add("style", "float:right; margin-bottom:20px; display:inline;")
                                        ImgDis.Attributes.Add("style", "float:right; position: relative; top: 25px; right: -6px; display:inline;")
                                    End If
                                Else
                                    If lnkCount > 23 Then
                                        ImgBut.Attributes.Add("style", "float:right; margin-bottom:5px; display:inline;")
                                        ImgDis.Attributes.Add("style", "position: absolute; bottom: 0px; right: 0px; display:inline;")
                                    Else
                                        ImgBut.Attributes.Add("style", "float:right; margin-bottom:5px; display:inline;")
                                        ImgDis.Attributes.Add("style", "position: absolute; bottom: 0px; right: 0px; display:inline;")
                                    End If
                                End If
                                ImgBut.Attributes.Add("onclick", "ShowPriceWindow('../PopUp/MaterialPrice.aspx','" + ctlContentPlaceHolder.ClientID.ToString() + "_hidMatid" + i.ToString() + "'); return false;")
                                ImgDis.Attributes.Add("onclick", "ShowPriceWindow('../PopUp/" + dsSug.Tables(0).Rows(0).Item("PAGEURL" + i.ToString() + "").ToString() + "','" + ctlContentPlaceHolder.ClientID.ToString() + "_hidMatid" + i.ToString() + "'); return false;")
                            End If

                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "P" + i.ToString()
                            If (dsPref.Tables(0).Rows(0).Item("PRP" + i.ToString() + "").ToString() <> "") Then
                                txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("PRP" + i.ToString() + "").ToString(), 4)
                            End If
                            txt.MaxLength = 9
                            If Session("E1UserRole") = "AADMIN" Then
                                ImgBut.Visible = True
                                ImgDis.Visible = True
                            Else
                                ImgBut.Visible = False
                                ImgDis.Visible = False
                            End If
                            tdInner.Controls.Add(ImgBut)
                            If dsSug.Tables(0).Rows(0).Item("ASSIST" + i.ToString() + "").ToString() <> "N" Then
                                tdInner.Controls.Add(ImgDis)
                            End If
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 11
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 50
                            txt.ID = "R" + i.ToString()
                            If (dsPref.Tables(0).Rows(0).Item("R" + i.ToString() + "").ToString() <> "") Then 'ADDED CONDITION FOR BUG#210
                                txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("R" + i.ToString() + "").ToString(), 2)
                            End If
                            txt.MaxLength = 6
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 12
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 50
                            txt.ID = "E" + i.ToString()
                            txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("E" + i.ToString() + "").ToString(), 2)
                            txt.MaxLength = 6

                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 13
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = FormatNumber(dsSug.Tables(0).Rows(0).Item("SGS" + i.ToString() + "").ToString(), 3)
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 14
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "SG" + i.ToString()
                            txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("SGP" + i.ToString() + "").ToString(), 3)

                            txt.MaxLength = 9
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 15
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = "<span class='CalculatedFeilds'>" + FormatNumber(dsSug.Tables(0).Rows(0).Item("WTPARA" + i.ToString() + "").ToString(), 3) + "</span>"
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 16
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.Text = "<span class='CalculatedFeilds'>" + FormatNumber(CDbl(CDbl(arrCostPerMsi(i).ToString())), 3) + "</span>"
                            ' lbl.Text = "<span class='CalculatedFeilds'>" + FormatNumber(ds.Tables(0).Rows(0).Item("COSTPARA" + i.ToString() + "").ToString(), 3) + "</span>"
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 17
                            InnerTdSetting(tdInner, "", "Left")
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = "hypDep" + i.ToString()
                            hid.ID = "hidDepid" + i.ToString()
                            Link.Width = 100
                            Link.CssClass = "Link"
                            GetDeptDetails(Link, hid, CInt(dsPref.Tables(0).Rows(0).Item("D" + i.ToString() + "").ToString()), DsDept)
                            tdInner.ID = "DepSud" + i.ToString()
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
            For i = 1 To 17
                tdInner = New TableCell
                Select Case i
                    Case 1
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.Text = "  <span class='CalculatedFeilds'><b> Structure Total </b></span><span style='color:Red;font-size:14px;'>*</span>"
                        tdInner.Attributes.Add("onmouseover", "Tip('Structure total values do not include waste, recycling, or extra-processing')")
                        tdInner.Attributes.Add("onmouseout", "UnTip('')")
                        trInner.Controls.Add(tdInner)
                    Case 4
                        InnerTdSetting(tdInner, "", "Right")
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(dsSug.Tables(0).Rows(0).Item("THICK").ToString(), 3) + " </b> </span>"
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
                    Case 9
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.ColumnSpan = 2
                        tdInner.Style.Add("text-align", "center")
                        'tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("PRICE").ToString(), 3) + " </b> </span>"
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(CDbl(CDbl(CostPerLBTotal.ToString())), 3) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 13
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.ColumnSpan = 2
                        trInner.Controls.Add(tdInner)
                    Case 2, 3, 11, 12, 17
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

                    Case 15
                        InnerTdSetting(tdInner, "", "Right")
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(dsSug.Tables(0).Rows(0).Item("WTPERAREA").ToString(), 3) + " </b></span>"
                        trInner.Controls.Add(tdInner)
                    Case 16
                        InnerTdSetting(tdInner, "", "Right")
                        'tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(ds.Tables(0).Rows(0).Item("COSTPARAREA").ToString(), 3) + " </b></span>"
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(CDbl(CDbl(CostPerMsiTotal.ToString())), 3) + " </b> </span>"
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
                            InnerTdSetting(tdInner, "50px", "left")
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
                            InnerTdSetting(tdInner, "50px", "left")
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
                        Title = "(" + dsPref.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                        HeaderTdSetting(tdHeader, "85px", "Weight", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        Title = "(" + dsPref.Tables(0).Rows(0).Item("TITLE2").ToString() + "/unit)"
                        HeaderTdSetting(tdHeader, "85px", "Weight", "1")
                        HeaderTdSetting(tdHeader, "", "Price", "1")
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
                            Link = New HyperLink
                            hid = New HiddenField
                            Link.ID = "hypDisMatDes" + i.ToString()
                            hid.ID = "hidDisMatid" + i.ToString()
                            Link.Width = 120
                            Link.CssClass = "Link"
                            GetDiscreteMaterialDetails(Link, hid, CInt(dsPref.Tables(0).Rows(0).Item("DISID" + i.ToString() + "").ToString()), DsDiscMat)
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(Link)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "DISW" + i.ToString()
                            txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("DISW" + i.ToString() + "").ToString(), 5)
                            txt.MaxLength = 9
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "DISP" + i.ToString()
                            txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("DISP" + i.ToString() + "").ToString(), 5)
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
            For i = 1 To 4
                tdInner = New TableCell
                Select Case i
                    Case 1
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.Text = "<span class='CalculatedFeilds'><b> Total </b></span>"
                        trInner.Controls.Add(tdInner)
                    Case 3
                        InnerTdSetting(tdInner, "", "Right")
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(dsSug.Tables(0).Rows(0).Item("DISCTOTAL").ToString(), 5) + " </b> </span>"
                        trInner.Controls.Add(tdInner)
                    Case 2, 4
                        InnerTdSetting(tdInner, "", "Left")
                        trInner.Controls.Add(tdInner)


                End Select
            Next
            trInner.Height = 30
            trInner.CssClass = "AlterNateColor1"
            tblComparision2.Controls.Add(trInner)

            'Discrete Materails
            trHeader = New TableRow
            tdHeader = New TableCell

            HeaderTdSetting(tdHeader, "", "Printing Plates", "1")
            tdHeader.Style.Add("text-align", "left")
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 30
            trHeader.Height = 30
            tblComparision3.Controls.Add(trHeader)



            'Inner
            For i = 1 To 3
                trInner = New TableRow
                tdInner = New TableCell

                InnerTdSetting(tdInner, "230px", "left")
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
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetMaterialDetailsNew(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal MatId As Integer, ByVal linkGrade As String, ByVal hidGrade As String, _
                                        ByVal SG As String, ByVal dsMat As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dsEmat As New DataSet
        Dim str As String = ""
        Dim dv As New DataView
        Dim dt As New DataTable
        'Changes for AWS
        Dim dvMat As New DataView
        Dim dtMat As New DataTable
        'End
        Try

            dsEmat = DirectCast(Session("dsEmat"), DataSet)
            dv = dsEmat.Tables(0).DefaultView

            If MatId <> 0 Then
                dv.RowFilter = "MATID=" + MatId.ToString()
                dt = dv.ToTable()

                If dt.Rows.Count > 0 Then
                    LinkMat.Text = dt.Rows(0).Item("MATDES").ToString().Replace("&#", "'")
                    LinkMat.ToolTip = dt.Rows(0).Item("MATDES").ToString().Replace("&#", "'")
                    LinkMat.Attributes.Add("text-decoration", "none")
                Else
                    'Ds = ObjGetdata.GetMaterials(MatId, "", "")
                    'If Ds.Tables(0).Rows(0).Item("MATDES").ToString().Length > 25 Then
                    'LinkMat.Font.Size = 8
                    'End If

                    'Changes for AWS
                    dvMat = dsMat.Tables(0).DefaultView '
                    dvMat.RowFilter = "MATID = " + MatId.ToString() '
                    dtMat = dvMat.ToTable()

                    If dtMat.Rows(0).Item("MATDES").ToString().Length > 25 Then
                        LinkMat.Font.Size = 8
                    End If
                    'End

                    LinkMat.Text = dtMat.Rows(0).Item("MATDES").ToString()
                    LinkMat.ToolTip = dtMat.Rows(0).Item("MATDES").ToString()
                    LinkMat.Attributes.Add("text-decoration", "none")
                End If
            Else
                'Ds = ObjGetdata.GetMaterials(MatId, "", "")
                'If Ds.Tables(0).Rows(0).Item("MATDES").ToString().Length > 25 Then
                '    'LinkMat.Font.Size = 8
                'End If

                'Changes for AWS
                dvMat = dsMat.Tables(0).DefaultView
                dvMat.RowFilter = "MATID = " + MatId.ToString() '
                dtMat = dvMat.ToTable()

                If dtMat.Rows(0).Item("MATDES").ToString().Length > 25 Then
                    'LinkMat.Font.Size = 8
                End If
                'End

                LinkMat.Text = dtMat.Rows(0).Item("MATDES").ToString()
                LinkMat.ToolTip = dtMat.Rows(0).Item("MATDES").ToString()
                LinkMat.Attributes.Add("text-decoration", "none")
            End If

            hid.Value = MatId.ToString()
            Path = "../PopUp/GetMatPopUpGrade.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "&GradeDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + linkGrade + "&GradeId=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hidGrade + "&SG=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + SG + "&CaseId=" + Session("E1CaseId") + ""
            LinkMat.NavigateUrl = "#"

            LinkMat.Attributes.Add("onClick", "javascript:return ShowMatPopWindow(this,'" + Path + "','" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "')")

        Catch ex As Exception
            ErrorLable.Text = "Error:GetMaterialDetailsNew:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetGrades(ByRef LinkGrade As HyperLink, ByVal hid As HiddenField, ByVal MatId As String, ByVal GradeId As Integer, ByVal SG As String, ByVal dsGrad As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim Path As String = String.Empty
        Dim MaterialId As String = String.Empty
        'Changes for AWS
        Dim dvGrad As New DataView
        Dim dtGrad As New DataTable
        'End
        Try
            'Ds = ObjGetdata.GetGradesVal(GradeId.ToString())

            dvGrad = dsGrad.Tables(0).DefaultView
            dvGrad.RowFilter = "GRADEID = " + GradeId.ToString()
            dtGrad = dvGrad.ToTable()

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

    Protected Sub GetMaterialDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal MatId As Integer, ByVal dsmat As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvmat As New DataView
        Dim dtmat As New DataTable
        Try

            'Ds = ObjGetdata.GetMaterials(MatId, "", "")
            'If Ds.Tables(0).Rows(0).Item("MATDES").ToString().Length > 25 Then
            '    'LinkMat.Font.Size = 8
            'End If
            'LinkMat.Text = Ds.Tables(0).Rows(0).Item("MATDES").ToString()
            'LinkMat.ToolTip = Ds.Tables(0).Rows(0).Item("MATDES").ToString()
            'LinkMat.Attributes.Add("text-decoration", "none")
            'LinkMat.Attributes.Add("onmouseover", "Tip('" + Ds.Tables(0).Rows(0).Item("MATDES").ToString() + "')")
            'LinkMat.Attributes.Add("onmouseout", "UnTip()")

            dvmat = dsmat.Tables(0).DefaultView
            dvmat.RowFilter = "MATID = " + MatId.ToString()
            dtmat = dvmat.ToTable()

            If dtmat.Rows(0).Item("MATDES").ToString().Length > 25 Then
                'LinkMat.Font.Size = 8
            End If
            LinkMat.Text = dtmat.Rows(0).Item("MATDES").ToString()
            LinkMat.ToolTip = dtmat.Rows(0).Item("MATDES").ToString()
            LinkMat.Attributes.Add("text-decoration", "none")

            hid.Value = MatId.ToString()
            Path = "../PopUp/GetMatPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
            LinkMat.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetDiscreteMaterialDetails(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal MatDisId As Integer, ByVal dsDmat As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvDmat As New DataView
        Dim dtDmat As New DataTable
        Try

            'Ds = ObjGetdata.GetDiscMaterials(MatDisId, "")
            'LinkMat.Text = Ds.Tables(0).Rows(0).Item("matDISde1").ToString()
            'LinkMat.ToolTip = Ds.Tables(0).Rows(0).Item("matDISde1").ToString()
            'LinkMat.Attributes.Add("text-decoration", "none")

            dvDmat = dsDmat.Tables(0).DefaultView
            dvDmat.RowFilter = "MATDISID = " + MatDisId.ToString()
            dtDmat = dvDmat.ToTable()

            LinkMat.Text = dtDmat.Rows(0).Item("matDISde1").ToString()
            LinkMat.ToolTip = dtDmat.Rows(0).Item("matDISde1").ToString()
            LinkMat.Attributes.Add("text-decoration", "none")

            hid.Value = MatDisId.ToString()
            Path = "../PopUp/GetMatDisPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + ""
            LinkMat.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"

        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetDeptDetails(ByRef LinkDep As HyperLink, ByVal hid As HiddenField, ByVal ProcId As Integer, ByVal dsDept As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New E1GetData.Selectdata()
        Dim Path As String = String.Empty
        Dim dvDept As New DataView
        Dim dtDept As New DataTable
        Try
            ' Ds = ObjGetdata.GetDept(ProcId, "", "")

            dvDept = dsDept.Tables(0).DefaultView
            dvDept.RowFilter = "PROCID = " + ProcId.ToString()
            dtDept = dvDept.ToTable()

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
        Dim Material(9) As String
        Dim Thickness(9) As String
        Dim Price(9) As String
        Dim Recycle(9) As String
        Dim Extra(9) As String
        Dim Sg(9) As String
        Dim Dept(9) As String
        Dim raddiscmatyn As String
        Dim plate As String
        Dim i As New Integer
        Dim MaterialDis(2) As String
        Dim WeightDis(2) As String
        Dim WeightPrice(2) As String
        Dim ObjUpIns As New E1UpInsData.UpdateInsert()
        Dim DisCount As New Integer
        Dim ProductFormt As New DataSet
        Dim ObjGetData As New E1GetData.Selectdata()
        ProductFormt = ObjGetData.GetProductFromatIn(CaseId)
        Dim obj As New CryptoHelper

        Dim OTR(9) As String
        Dim WVTR(9) As String
        Dim TempDs As New DataSet
        Dim RHDs As New DataSet
        Dim GRADE(9) As String
        Try
            If Not objRefresh.IsRefresh Then
                For i = 1 To 10
                    Material(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidMatid" + i.ToString() + "")
                    Thickness(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$T" + i.ToString() + "")
                    Price(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$P" + i.ToString() + "")
                    Recycle(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$R" + i.ToString() + "")
                    Extra(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$E" + i.ToString() + "")
                    Sg(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$SG" + i.ToString() + "")
                    Dept(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidDepid" + i.ToString() + "")

                    OTR(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$OTR" + i.ToString() + "")
                    WVTR(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$WVTR" + i.ToString() + "")
                    GRADE(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidGradeId" + i.ToString() + "")

                    'Check For IsNumric
                    If Not IsNumeric(Thickness(i - 1)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If
                    If Not IsNumeric(Price(i - 1)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If

                    If Not IsNumeric(Recycle(i - 1)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If

                    If Not IsNumeric(Extra(i - 1)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If

                    If Not IsNumeric(Sg(i - 1)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If


                    'Check For Dependant-Indepdant Error
                    If CInt(Material(i - 1)) <> 0 Then
                        'Checking Thickness
                        If CDbl(Thickness(i - 1)) <= CDbl(0.0) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE101").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If

                        'Checking Dept.
                        If CDbl(Dept(i - 1)) <= 0 Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE102").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                    End If

                Next

                For i = 1 To 3

                    MaterialDis(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidDisMatid" + i.ToString() + "")
                    WeightDis(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$DISW" + i.ToString() + "")
                    WeightPrice(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$DISP" + i.ToString() + "")

                    'Check For IsNumric
                    If Not IsNumeric(WeightDis(i - 1)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If

                    If Not IsNumeric(WeightPrice(i - 1)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If

                    If Request.Form("ctl00$Econ1ContentPlaceHolder$DISW" + i.ToString() + "") <> 0 Then
                        If CInt(ProductFormt.Tables(0).Rows(0).Item("M1")) = 1 Or CInt(ProductFormt.Tables(0).Rows(0).Item("M1")) = 17 Then
                            DisCount = DisCount + 1
                        End If
                    End If

                Next


                plate = Request.Form("ctl00$Econ1ContentPlaceHolder$radPlate")
                raddiscmatyn = Request.Form("ctl00$Econ1ContentPlaceHolder$raddiscmatyn")
                If DisCount = 0 Then
                    ObjUpIns.ExtrusionUpdate(CaseId, Material, Thickness, Price, Recycle, Extra, Sg, Dept, raddiscmatyn, plate, MaterialDis, WeightDis, WeightPrice, True)
                Else
                    ObjUpIns.ExtrusionUpdate(CaseId, Material, Thickness, Price, Recycle, Extra, Sg, Dept, raddiscmatyn, plate, MaterialDis, WeightDis, WeightPrice, False)
                End If

                ObjUpIns.BarrierUpdateNew(CaseId, txtOTRTemp.Text, txtWVTRTemp.Text, txtOTRHumidity.Text, txtWVTRHumidity.Text, OTR, WVTR, GRADE, hidBarrier.Value)

                Calculate()
                'Update Server Date
                ObjUpIns.ServerDateUpdate(CaseId, Session("E1UserName"))
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

#Region "Bulk Model Management"

    Protected Sub btnUpdateBulk_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdateBulk.Click
        Dim Material(9) As String
        Dim Thickness(9) As String
        Dim Price(9) As String
        Dim Recycle(9) As String
        Dim Extra(9) As String
        Dim Sg(9) As String
        Dim Dept(9) As String
        Dim raddiscmatyn As String
        Dim plate As String
        Dim i As New Integer
        Dim MaterialDis(2) As String
        Dim WeightDis(2) As String
        Dim WeightPrice(2) As String
        Dim ObjUpIns As New E1UpInsData.UpdateInsert()
        Dim DisCount As New Integer
        Dim ProductFormt As New DataSet
        Dim ObjGetData As New E1GetData.Selectdata()
        ProductFormt = ObjGetData.GetProductFromatIn(CaseId)
        Dim obj As New CryptoHelper

        Dim OTR(9) As String
        Dim WVTR(9) As String
        Dim TempDs As New DataSet
        Dim RHDs As New DataSet
        Dim GRADE(9) As String
        Try
            For i = 1 To 10
                Material(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidMatid" + i.ToString() + "")
                Thickness(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$T" + i.ToString() + "")
                Price(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$P" + i.ToString() + "")
                Recycle(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$R" + i.ToString() + "")
                Extra(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$E" + i.ToString() + "")
                Sg(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$SG" + i.ToString() + "")
                Dept(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidDepid" + i.ToString() + "")

                OTR(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$OTR" + i.ToString() + "")
                WVTR(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$WVTR" + i.ToString() + "")
                GRADE(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidGradeId" + i.ToString() + "")

                'Check For IsNumric
                If Not IsNumeric(Thickness(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If
                If Not IsNumeric(Price(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If

                If Not IsNumeric(Recycle(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If

                If Not IsNumeric(Extra(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If

                If Not IsNumeric(Sg(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If


                'Check For Dependant-Indepdant Error
                If CInt(Material(i - 1)) <> 0 Then
                    'Checking Thickness
                    If CDbl(Thickness(i - 1)) <= CDbl(0.0) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE101").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If

                    'Checking Dept.
                    If CDbl(Dept(i - 1)) <= 0 Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE102").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If
                End If

            Next

            For i = 1 To 3

                MaterialDis(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidDisMatid" + i.ToString() + "")
                WeightDis(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$DISW" + i.ToString() + "")
                WeightPrice(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$DISP" + i.ToString() + "")

                'Check For IsNumric
                If Not IsNumeric(WeightDis(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If

                If Not IsNumeric(WeightPrice(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If

                If Request.Form("ctl00$Econ1ContentPlaceHolder$DISW" + i.ToString() + "") <> 0 Then
                    If CInt(ProductFormt.Tables(0).Rows(0).Item("M1")) = 1 Or CInt(ProductFormt.Tables(0).Rows(0).Item("M1")) = 17 Then
                        DisCount = DisCount + 1
                    End If
                End If

            Next


            plate = Request.Form("ctl00$Econ1ContentPlaceHolder$radPlate")
            raddiscmatyn = Request.Form("ctl00$Econ1ContentPlaceHolder$raddiscmatyn")

            'Start Updating Bulk Model
            Dim str As String
            Dim strArr() As String
            Dim count As Integer
            Dim BCaseId As Integer
            str = Session("CaseIdString")
            If str <> "" Then
                strArr = str.Split(",")
                For count = 0 To strArr.Length - 1
                    BCaseId = strArr(count)
                    Try
                        If DisCount = 0 Then
                            ObjUpIns.ExtrusionUpdate(BCaseId, Material, Thickness, Price, Recycle, Extra, Sg, Dept, raddiscmatyn, plate, MaterialDis, WeightDis, WeightPrice, True)
                        Else
                            ObjUpIns.ExtrusionUpdate(BCaseId, Material, Thickness, Price, Recycle, Extra, Sg, Dept, raddiscmatyn, plate, MaterialDis, WeightDis, WeightPrice, False)
                        End If

                        ObjUpIns.BarrierUpdateNew(BCaseId, txtOTRTemp.Text, txtWVTRTemp.Text, txtOTRHumidity.Text, txtWVTRHumidity.Text, OTR, WVTR, GRADE, hidBarrier.Value)

                        'Update Server Date
                        ObjUpIns.ServerDateUpdate(BCaseId, Session("E1UserName"))
                    Catch ex As Exception
                    End Try
                Next
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alertMsg", "alert('Variables transferred successfully.');", True)
                'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "alertMsg", "alert('Values successfully transfered.');", True)
            Else
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alertMsg", "alert('Something went wrong! No model found for transfer.');", True)
            End If
            'End Updating Bulk Model
            loading.Style.Add("display", "none")
            lnkSelBulkModel.Visible = True
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:btnUpdateBulk_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub btnUpdateBulk1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdateBulk1.Click
        Dim Material(9) As String
        Dim Thickness(9) As String
        Dim Price(9) As String
        Dim Recycle(9) As String
        Dim Extra(9) As String
        Dim Sg(9) As String
        Dim Dept(9) As String
        Dim raddiscmatyn As String
        Dim plate As String
        Dim i As New Integer
        Dim MaterialDis(2) As String
        Dim WeightDis(2) As String
        Dim WeightPrice(2) As String
        Dim ObjUpIns As New E1UpInsData.UpdateInsert()
        Dim DisCount As New Integer
        Dim ProductFormt As New DataSet
        Dim ObjGetData As New E1GetData.Selectdata()
        ProductFormt = ObjGetData.GetProductFromatIn(CaseId)
        Dim obj As New CryptoHelper

        Dim OTR(9) As String
        Dim WVTR(9) As String
        Dim TempDs As New DataSet
        Dim RHDs As New DataSet
        Dim GRADE(9) As String

        Try
            For i = 1 To 10
                Material(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidMatid" + i.ToString() + "")
                Thickness(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$T" + i.ToString() + "")
                Price(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$P" + i.ToString() + "")
                Recycle(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$R" + i.ToString() + "")
                Extra(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$E" + i.ToString() + "")
                Sg(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$SG" + i.ToString() + "")
                Dept(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidDepid" + i.ToString() + "")

                OTR(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$OTR" + i.ToString() + "")
                WVTR(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$WVTR" + i.ToString() + "")
                GRADE(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidGradeId" + i.ToString() + "")

                'Check For IsNumric
                If Not IsNumeric(Thickness(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If
                If Not IsNumeric(Price(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If

                If Not IsNumeric(Recycle(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If

                If Not IsNumeric(Extra(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If

                If Not IsNumeric(Sg(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If


                'Check For Dependant-Indepdant Error
                If CInt(Material(i - 1)) <> 0 Then
                    'Checking Thickness
                    If CDbl(Thickness(i - 1)) <= CDbl(0.0) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE101").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If

                    'Checking Dept.
                    If CDbl(Dept(i - 1)) <= 0 Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE102").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If
                End If

            Next

            For i = 1 To 3

                MaterialDis(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$hidDisMatid" + i.ToString() + "")
                WeightDis(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$DISW" + i.ToString() + "")
                WeightPrice(i - 1) = Request.Form("ctl00$Econ1ContentPlaceHolder$DISP" + i.ToString() + "")

                'Check For IsNumric
                If Not IsNumeric(WeightDis(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If

                If Not IsNumeric(WeightPrice(i - 1)) Then
                    Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE111").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                End If

                If Request.Form("ctl00$Econ1ContentPlaceHolder$DISW" + i.ToString() + "") <> 0 Then
                    If CInt(ProductFormt.Tables(0).Rows(0).Item("M1")) = 1 Or CInt(ProductFormt.Tables(0).Rows(0).Item("M1")) = 17 Then
                        DisCount = DisCount + 1
                    End If
                End If

            Next

            plate = Request.Form("ctl00$Econ1ContentPlaceHolder$radPlate")
            raddiscmatyn = Request.Form("ctl00$Econ1ContentPlaceHolder$raddiscmatyn")

            'Start Updating Bulk Model
            Dim str As String
            Dim strArr() As String
            Dim count As Integer
            Dim BCaseId As Integer
            str = Session("CaseIdString")
            If str <> "" Then
                strArr = str.Split(",")
                For count = 0 To strArr.Length - 1
                    BCaseId = strArr(count)
                    Try
                        If DisCount = 0 Then
                            ObjUpIns.ExtrusionUpdate(BCaseId, Material, Thickness, Price, Recycle, Extra, Sg, Dept, raddiscmatyn, plate, MaterialDis, WeightDis, WeightPrice, True)
                        Else
                            ObjUpIns.ExtrusionUpdate(BCaseId, Material, Thickness, Price, Recycle, Extra, Sg, Dept, raddiscmatyn, plate, MaterialDis, WeightDis, WeightPrice, False)
                        End If

                        ObjUpIns.BarrierUpdateNew(BCaseId, txtOTRTemp.Text, txtWVTRTemp.Text, txtOTRHumidity.Text, txtWVTRHumidity.Text, OTR, WVTR, GRADE, hidBarrier.Value)
                        Calculate_Bulk(BCaseId)
                        'Update Server Date
                        ObjUpIns.ServerDateUpdate(BCaseId, Session("E1UserName"))
                    Catch ex As Exception
                    End Try
                Next
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alertMsg", "alert('Variables transferred and calculated successfully.');", True)
                'ScriptManager.RegisterStartupScript(Page, Me.GetType(), "alertMsg", "alert('Values successfully transfered and Calculated.');", True)
            Else
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "alertMsg", "alert('Something went wrong! No model found for transfer.');", True)
            End If
            'End Updating Bulk Model
            loading.Style.Add("display", "none")
            lnkSelBulkModel.Visible = True
            GetPageDetails()
        Catch ex As Exception
            ErrorLable.Text = "Error:btnUpdateBulk1_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub Calculate_Bulk(ByVal BCaseID As Integer)
        Try
            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim obj As New EconCalculation.EconCalculation(Econ1Conn)
            obj.EconCalculate(BCaseID)
        Catch ex As Exception
            ErrorLable.Text = "Error:Calculate_Bulk:" + ex.Message.ToString() + ""
        End Try
    End Sub

#End Region

End Class
