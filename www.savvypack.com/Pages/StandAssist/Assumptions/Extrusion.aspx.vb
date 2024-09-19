Imports System.Data
Imports System.Data.OleDb
Imports System
Imports StandGetData
Imports StandUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_StandAssist_Assumptions_Extrusion1
    Inherits System.Web.UI.Page
    'For Blend Calculation in RH 
    Dim MTR(9) As String
    Dim Thick(9) As String
    Dim TotalThick As Double
    Dim BOTR(9, 1) As String
    Dim BMAT(9, 1) As String
    Dim BTHICK(9, 1) As String
    Dim BTHICKPER(9, 1) As String
    Dim BADJTHICK(9, 1) As String
    Dim BMATERIALS(9, 1) As String
    Dim TotalThickSub(9) As Double
    Dim BTS1(9, 1) As String
    Dim BTS2(9, 1) As String
    Dim BDG(9, 1) As String
    Dim path As String = "C:\LOG\TimeLog.txt"
    Dim objLog As New LogFiles.CreateLogFiles

#Region "Get Set Variables"
    Dim _lErrorLble As Label
	 Dim _lCaseUpdateLble As Label
    Dim _iCaseId As Integer
    Dim _iUserId As Integer
    Dim _strUserRole As String
    Dim _btnUpdate As ImageButton
    Dim _btnLogOff As ImageButton
    Dim _btnChart As ImageButton
    Dim _btnInk As ImageButton
    Dim _btnCompare As ImageButton
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
	
	  Public Property CaseUpdateLable() As Label
        Get
            Return _lCaseUpdateLble
        End Get
        Set(ByVal Value As Label)
            _lCaseUpdateLble = Value
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
    Public Property Inkbtn() As ImageButton
        Get
            Return _btnInk
        End Get
        Set(ByVal value As ImageButton)
            _btnInk = value
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
        GetComparebtn()
        GetFeedbackbtn()
        GetNotesbtn()
        GetMainHeadingdiv()
        GetCaseUpdateLable()
    End Sub
   
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub
  Protected Sub GetCaseUpdateLable()
        CaseUpdateLable = Page.Master.FindControl("lblCaseUpdate")
    End Sub
    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Visible = True
        Updatebtn.Attributes.Add("onclick", "return CheckForMaterialPageData('" + ctlContentPlaceHolder.ClientID + "','" + ctlContentPlaceHolder.ClientID + "_tabSDesigner_tabpnl1" + "','hidMatid','T','OTRTEMP','RH','txtOTRTemp','txtWVTRTemp','txtOTRHumidity','txtWVTRHumidity','txtRHTemp','BT','hidBlendid','hypBlendDes');")

        'Updatebtn.Attributes.Add("onclick", "return CheckForMaterialPageData123();")

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

    Protected Sub GetComparebtn()
        Dim obj As New CryptoHelper()
        Comparebtn = Page.Master.FindControl("imgCompare")
        Comparebtn.Visible = True
        Comparebtn.OnClientClick = "return Compare('../Comparison/Comparison.aspx') "
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Structure Assumption')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        'MainHeading.InnerHtml = "Stand Alone Barrier Assisant - Material Assumption"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("StandAssistContentPlaceHolder")
    End Sub

#End Region

#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_STANDASSIST_ASSUMPTIONS_EXTRUSION1")
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
                hdnTabNum.Value = "0"
                CheckCaseView()
                GetTempRH()
                GetPageDetails()
                GetPageDetailsRH()

            End If
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "Layers", "DisplayLayers();", True)
            If CheckAnyDelCase(CaseId.ToString()) Then
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:Page_Load:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Private Function CheckCaseView() As Boolean
        Dim ds As New DataSet()
        Dim GetData As New StandGetData.Selectdata()
        Try

            ds = GetData.GetCaseViewDet(CaseId)
            If ds.Tables(0).Rows.Count > 0 Then
                hidOTR.Value = ds.Tables(0).Rows(0).Item("ISOTR").ToString()
                hidWVTR.Value = ds.Tables(0).Rows(0).Item("ISWVTR").ToString()
                hidTS1.Value = ds.Tables(0).Rows(0).Item("ISTS1").ToString()
                hidTS2.Value = ds.Tables(0).Rows(0).Item("ISTS2").ToString()
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function CheckAnyDelCase(ByVal CaseId As String) As Boolean
        Dim ds As New DataSet()
        Dim dsGroup As New DataSet()
        Dim message As String = ""
        Dim i As Integer
        Dim GetData As New StandGetData.Selectdata()
        Try

            ds = GetData.GetDelStructure(CaseId, Session("USERID").ToString())
            If ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "errorMessage(" + CaseId + ");", True)

                Return False
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Protected Sub GetSessionDetails()
        Try
            UserId = Session("UserId")
            CaseId = Session("SBACaseId")
            UserRole = Session("SBAUserRole")
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetTempRH()
        Dim ds1 As New DataSet()
        Dim ds2 As New DataSet()
        Dim ds3 As New DataSet()
        Dim objGetData As New StandGetData.Selectdata()
        Try
            ds3 = objGetData.GetPrefDetails(CaseId)
            ds1 = objGetData.GetMinMaxBarrierTemp()
            ds2 = objGetData.GetMinMaxBarrierHumidity()

            If ds1.Tables(0).Rows.Count > 0 Then
                If ds3.Tables(0).Rows(0).Item("UNITS").ToString() <> "0" Then
                    OTRTEMP1.Value = ds1.Tables(0).Rows(0).Item("MINVAL").ToString()
                    OTRTEMP2.Value = ds1.Tables(0).Rows(0).Item("MAXVAL").ToString()
                Else
                    OTRTEMP1.Value = (CDbl(ds1.Tables(0).Rows(0).Item("MINVAL").ToString()) * (9 / 5)) + 32
                    OTRTEMP2.Value = (CDbl(ds1.Tables(0).Rows(0).Item("MAXVAL").ToString()) * (9 / 5)) + 32
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
        ' Dim ds As New DataSet
        Dim objCalb As New StandBarrierGetData.Calculations
        Dim objCalb1 As New StandBarrierGetData.Calculations
        Dim objGetData As New StandGetData.Selectdata
        Dim objUpdateData As New StandUpInsData.UpdateInsert
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
        Dim label As New Label
        Dim hid As New HiddenField
        Dim hidGrade As New HiddenField
        Dim hidMat As New HiddenField



        Dim hidDes As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim chk As New CheckBox
        Dim radio As New RadioButton
        Dim OTR As String
        Dim WVTR As String
        Dim dsPref As New DataSet
        Dim dsSug As New DataSet
        Dim dsLabel As New DataSet
        Dim ImgBut As New ImageButton
        Dim hid1 As New HiddenField
        Dim ImgDis As New ImageButton

        'Blend
        Dim dsBPref As New DataSet
        Dim dsBSug As New DataSet
        Dim blendThick As Double
        Dim dvBPref As DataView
        Dim dtBPref As DataTable
        Dim dvBSug As DataView
        Dim dtBSug As DataTable
        Dim hidB As New HiddenField
        Dim hidBMat As New HiddenField
        Dim hidBDes As New HiddenField

        Dim objCalRH As New StandBarrierGetData.Calculations
        Dim dsOTRWVTR As New DataSet

        Dim Grades(9) As String
        Dim Materials(9) As String
        Dim OtrVal(9) As String
        Dim WVTRVal(9) As String
        Dim ISADJTHICK(9) As String
        Dim MAT(9) As String
        'Collecting Data for Sublayers of Blend            
        Dim BWVTR(9, 1) As String
        Dim TS1Val(9) As String
        Dim TS2Val(9) As String
        Dim DGVal(9) As String

        'Collecting data for Sub Layers and Blend
        Dim TotalSG As Double
        Dim BLLayerWTSUB As Double
        Dim WTPERAREASUB(10) As Double
        Dim WeightPerSub(10, 2) As String
        Dim ARRlayerWTSUB(10, 2) As String
        Dim MATWeight(9) As String
        Dim BMatWeight(9, 1) As String
		Dim imgOTR As New ImageButton
        Dim imgWVTR As New ImageButton
        Dim dsGBM As New DataSet
        Dim dsGMat1 As New DataSet
        Dim dsGrade As New DataSet
        Dim DsPre1 As New DataSet
        Dim DsSug1 As New DataSet
        Dim Mat1 As String
        Dim MATID1 As String = ""
        Try

            DsPre1 = objGetData.GetExtrusionDetailsPref(CaseId)
            DsSug1 = objGetData.GetExtrusionDetailsSugg(CaseId)

            For i = 0 To 9
                MAT1 = DsPre1.Tables(0).Rows(0).Item("M" + (i + 1).ToString() + "").ToString()
                MATID1 = MATID1 + "" + MAT1 + ","


                'dsMat1 = objGetData.GetCategory("-1", DsSug1.Tables(0).Rows(0).Item("MATS" + (i + 1).ToString() + "").ToString(), "")
            Next
            MATID1 = MATID1.Remove(MATID1.Length - 1)




            ' hid.ID = "hidMatid" + i.ToString()
            'dsGBM = objGetData.GetCategory("-1", "", "")
            'dsGMat1 = objGetData.GetCategory("-1", "", "")
            '  dsGrade = objGetData.GetMatSupplierGrade("-1", "", "-1")

            dsGBM = objGetData.GetCategoryNew(MATID1)
            dsGMat1 = objGetData.GetCategoryNew(MATID1)




            tblComparision.Rows.Clear()
            dsLabel = objGetData.GetCaseDetails(CaseId.ToString())
            If dsLabel.Tables(0).Rows.Count > 0 Then
                CaseUpdateLable.Text = dsLabel.Tables(0).Rows(0).Item("SERVERDATE").ToString()
            End If

            dsPref = objGetData.GetExtrusionDetailsPref(CaseId)
            dsSug = objGetData.GetExtrusionDetailsSugg(CaseId)

            'Getting Data for Sub Layers of Blend
            dsBPref = objGetData.GetBlendDetailsPref(CaseId)
            dsBSug = objGetData.GetBlendDetailsSugg(CaseId)
            dvBPref = dsBPref.Tables(0).DefaultView()
            dvBSug = dsBSug.Tables(0).DefaultView()

            Dim dsMatInput As New DataSet
            Dim l, m As Integer


            'Calculate Total Thick
            For i = 0 To 9
                If dsPref.Tables(0).Rows(0).Item("TYPEM" + (i + 1).ToString() + "").ToString() > 500 And dsPref.Tables(0).Rows(0).Item("TYPEM" + (i + 1).ToString() + "").ToString() < 506 Then
                    dvBPref.RowFilter = "TYPEM=" + dsPref.Tables(0).Rows(0).Item("TYPEM" + (i + 1).ToString() + "").ToString()
                    dtBPref = dvBPref.ToTable()
                    Dim TotalThickBlend As Double = 0
                    If dtBPref.Rows.Count > 0 Then
                        For l = 1 To 2
                            TotalThickBlend = TotalThickBlend + dtBPref.Rows(0).Item("BCT" + l.ToString() + "")
                            TotalThick = TotalThick + dtBPref.Rows(0).Item("BCT" + l.ToString() + "")
                        Next
                        TotalThickSub(i) = TotalThickBlend
                        MATWeight(i) = TotalThickBlend
                    End If
                Else
                    If dsPref.Tables(0).Rows(0).Item("THICK" + (i + 1).ToString() + "").ToString() <> "" Then
                        TotalThick = TotalThick + dsPref.Tables(0).Rows(0).Item("THICK" + (i + 1).ToString() + "")
                        MATWeight(i) = dsPref.Tables(0).Rows(0).Item("THICK" + (i + 1).ToString() + "")
                        TotalThickSub(i) = 0
                    End If
                End If
            Next

            For i = 0 To 9
                MATWeight(i) = MATWeight(i) / TotalThick
            Next

            'Barrier
            If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() <> 1 Then
                OTR = (CDbl(dsPref.Tables(0).Rows(0).Item("OTRTEMP").ToString()) * (9 / 5)) + 32
                WVTR = (CDbl(dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString()) * (9 / 5)) + 32
            Else
                OTR = dsPref.Tables(0).Rows(0).Item("OTRTEMP").ToString()
                WVTR = dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString()
            End If


            For i = 0 To 9
                Grades(i) = dsPref.Tables(0).Rows(0).Item("GRADE" + (i + 1).ToString() + "").ToString()
                If dsPref.Tables(0).Rows(0).Item("TYPEM" + (i + 1).ToString()).ToString() <> "0" Then
                    'Data for Sublayers of Blend
                    MAT(i) = dsPref.Tables(0).Rows(0).Item("TYPEM" + (i + 1).ToString() + "").ToString()
                    If MAT(i) > 500 And MAT(i) < 506 Then
                        dvBPref.RowFilter = "TYPEM=" + MAT(i) + ""
                        dvBSug.RowFilter = "TYPEM=" + MAT(i) + ""
                        dtBPref = dvBPref.ToTable()
                        dtBSug = dvBSug.ToTable()
                        Dim TotalThickB As Double = 0
                        If dtBPref.Rows.Count > 0 Then
                            For j = 0 To 1
                                If TotalThickB = 0 Then
                                    TotalThickB = CDbl(dtBPref.Rows(0).Item("BCT" + (j + 1).ToString() + "").ToString() * dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString())
                                Else
                                    TotalThickB = TotalThickB + CDbl(dtBPref.Rows(0).Item("BCT" + (j + 1).ToString() + "").ToString() * dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString())
                                End If
                                BMatWeight(i, j) = CDbl(dtBPref.Rows(0).Item("BCT" + (j + 1).ToString() + "").ToString() * dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString())
                            Next

                            For j = 0 To 1
                                BMatWeight(i, j) = CDbl(BMatWeight(i, j)) / TotalThickB
                            Next
                        Else
                            TotalThickB = 0
                        End If
                        Thick(i) = CDbl(TotalThickB)
                        For j = 0 To 1
                            If dtBPref.Rows.Count > 0 Then
                                BMAT(i, j) = dtBPref.Rows(0).Item("BCM" + (j + 1).ToString() + "").ToString()
                                BTHICK(i, j) = dtBPref.Rows(0).Item("BCT" + (j + 1).ToString() + "").ToString() * dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString()
                                If TotalThickB <> 0 Then
                                    BTHICKPER(i, j) = ((CDbl(BTHICK(i, j)) / TotalThickB) * 100).ToString()
                                End If
                                BOTR(i, j) = dtBPref.Rows(0).Item("BCOTR" + (j + 1).ToString() + "").ToString()
                                BWVTR(i, j) = dtBPref.Rows(0).Item("BCWVTR" + (j + 1).ToString() + "").ToString()
                                BADJTHICK(i, j) = dtBSug.Rows(0).Item("ISADJTHICK" + (j + 1).ToString() + "").ToString()
                                BMATERIALS(i, j) = dtBSug.Rows(0).Item("MATS" + (j + 1).ToString() + "").ToString()
                                BTS1(i, j) = dtBPref.Rows(0).Item("BCTS1VAL" + (j + 1).ToString() + "").ToString()
                                BTS2(i, j) = dtBPref.Rows(0).Item("BCTS2VAL" + (j + 1).ToString() + "").ToString()
                                BDG(i, j) = dtBSug.Rows(0).Item("DG" + (j + 1).ToString() + "").ToString()
                            Else
                                BMAT(i, j) = "0"
                                BTHICK(i, j) = "0"
                                BOTR(i, j) = ""
                                BWVTR(i, j) = ""
                                BADJTHICK(i, j) = ""
                                BTHICKPER(i, j) = "0"
                                BMATERIALS(i, j) = ""
                                BTS1(i, j) = ""
                                BTS2(i, j) = ""
                                BDG(i, j) = ""
                            End If
                        Next
                    Else
                        For j = 0 To 1
                            BMAT(i, j) = "0"
                            BTHICK(i, j) = "0"
                            BOTR(i, j) = ""
                            BWVTR(i, j) = ""
                            BADJTHICK(i, j) = ""
                            BTHICKPER(i, j) = "0"
                            BMATERIALS(i, j) = ""
                            BTS1(i, j) = ""
                            BTS2(i, j) = ""
                            BDG(i, j) = ""
                        Next
                    End If
                ElseIf dsPref.Tables(0).Rows(0).Item("M" + (i + 1).ToString()).ToString() <> "" Then
                    'Material Layers
                    MAT(i) = dsPref.Tables(0).Rows(0).Item("M" + (i + 1).ToString() + "").ToString()
                    Thick(i) = CDbl(dsPref.Tables(0).Rows(0).Item("THICK" + (i + 1).ToString() + "").ToString() * dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString())
                    For j = 0 To 1
                        BMAT(i, j) = "0"
                        BTHICK(i, j) = "0"
                        BOTR(i, j) = ""
                        BWVTR(i, j) = ""
                        BADJTHICK(i, j) = ""
                        BTHICKPER(i, j) = "0"
                        BMATERIALS(i, j) = ""
                        BTS1(i, j) = ""
                        BTS2(i, j) = ""
                        BDG(i, j) = ""
                    Next
                End If
                Materials(i) = dsSug.Tables(0).Rows(0).Item("MATS" + (i + 1).ToString() + "").ToString()
                OtrVal(i) = dsPref.Tables(0).Rows(0).Item("OTR" + (i + 1).ToString() + "").ToString()
                WVTRVal(i) = dsPref.Tables(0).Rows(0).Item("WVTR" + (i + 1).ToString() + "").ToString()
                ISADJTHICK(i) = dsSug.Tables(0).Rows(0).Item("ISADJTHICK" + (i + 1).ToString() + "").ToString()
                TS1Val(i) = dsPref.Tables(0).Rows(0).Item("TS1VAL" + (i + 1).ToString() + "").ToString()
                TS2Val(i) = dsPref.Tables(0).Rows(0).Item("TS2VAL" + (i + 1).ToString() + "").ToString()
                DGVal(i) = dsSug.Tables(0).Rows(0).Item("DG" + (i + 1).ToString() + "").ToString()
            Next


            dsOTRWVTR = objCalRH.BarrierData(CaseId, MAT, CDate(dsPref.Tables(0).Rows(0).Item("EFFDATEB").ToString()), BMAT)
            Session("OTRWVTR") = dsOTRWVTR

            objCalb.BarrierPropCalculate(CaseId, dsPref.Tables(0).Rows(0).Item("OTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("OTRRH").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRRH").ToString(), _
            MAT, CDate(dsPref.Tables(0).Rows(0).Item("EFFDATEB").ToString()), Materials, OtrVal, WVTRVal, Thick, ISADJTHICK, dsOTRWVTR)


            'Changes started for Fixed 100% RH for MVTR
            objCalb1.BarrierPropCalculateWVTR100(CaseId, dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString(), "100", MAT, CDate(dsPref.Tables(0).Rows(0).Item("EFFDATEB").ToString()), Materials, dsOTRWVTR)

            objCalb1.BarrierPropCalBlendSubLayersWVTR100(CaseId, dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString(), "100", BADJTHICK, BMAT, CDate(dsPref.Tables(0).Rows(0).Item("EFFDATEB").ToString()), BMATERIALS, BWVTR, BTHICK, _
                                                         BTHICKPER, dsOTRWVTR)
            'Changes ended for Fixed 100% RH for MVTR


            'Calculating Data for OTR,WVTR for Blend and Sub Layers 
            objCalb.BarrierPropCalBlendSubLayers(CaseId, dsPref.Tables(0).Rows(0).Item("OTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("OTRRH").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRRH").ToString(), _
            BMAT, CDate(dsPref.Tables(0).Rows(0).Item("EFFDATEB").ToString()), BMATERIALS, BOTR, BWVTR, BTS1, BTS2, BDG, BTHICK, BADJTHICK, BTHICKPER, OtrVal, WVTRVal, TS1Val, TS2Val, DGVal, MAT, dsOTRWVTR, MATWeight, BMatWeight)


            lblOTRTemp.Text = "OTR Temperature (" + dsPref.Tables(0).Rows(0).Item("TITLE20").ToString() + "):"
            lblWVTRTemp.Text = "WVTR Temperature (" + dsPref.Tables(0).Rows(0).Item("TITLE20").ToString() + "):"

            txtOTRTemp.Text = FormatNumber(OTR, 1) 'ds.Tables(0).Rows(0).Item("OTRTEMP").ToString()
            txtWVTRTemp.Text = FormatNumber(WVTR, 1) 'ds.Tables(0).Rows(0).Item("WVTRTEMP").ToString()
            txtOTRHumidity.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("OTRRH").ToString(), 1)
            txtWVTRHumidity.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("WVTRRH").ToString(), 1)

            'Weight Percentage Calculation
            Dim ARRlayerWT(10) As String
            Dim WTPERAREA As Double
            Dim WeightPer(10) As String
            Dim WeightPerT As Double
            For i = 1 To 10
                If dsPref.Tables(0).Rows(0).Item("TYPEM" + i.ToString() + "").ToString() > 500 And dsPref.Tables(0).Rows(0).Item("TYPEM" + i.ToString() + "").ToString() < 506 Then
                    'Blend
                    dvBPref.RowFilter = "TYPEM=" + dsPref.Tables(0).Rows(0).Item("TYPEM" + i.ToString() + "").ToString()
                    dtBPref = dvBPref.ToTable()
                    BLLayerWTSUB = 0.0
                    dvBSug.RowFilter = "TYPEM=" + dsPref.Tables(0).Rows(0).Item("TYPEM" + i.ToString()).ToString() + ""
                    dtBSug = dvBSug.ToTable()
                    If dtBPref.Rows.Count > 0 Then
                        For l = 1 To 2
                            'Weight of Sub Layers
                            If CDbl(dtBPref.Rows(0).Item("BCSG" + l.ToString() + "").ToString()) <> 0.0 Then
                                ARRlayerWTSUB(i, l) = (1000 * (CDbl(dtBPref.Rows(0).Item("BCT" + l.ToString() + "").ToString()) / 1000) / 1728) * 62.4 * CDbl(dtBPref.Rows(0).Item("BCSG" + l.ToString() + "").ToString())
                            Else
                                ARRlayerWTSUB(i, l) = (1000 * (CDbl(dtBPref.Rows(0).Item("BCT" + l.ToString() + "").ToString()) / 1000) / 1728) * 62.4 * CDbl(dtBSug.Rows(0).Item("SGS" + l.ToString() + "").ToString())
                            End If
                            BLLayerWTSUB = ARRlayerWTSUB(i, l) + BLLayerWTSUB
                        Next
                        'Weight Per Area for each blend
                        WTPERAREASUB(i) = BLLayerWTSUB
                        'Weight of Blend Layers
                        If CDbl(dsPref.Tables(0).Rows(0).Item("TYPEMSG" + i.ToString() + "").ToString()) <> 0.0 Then
                            ARRlayerWT(i) = (1000 * (CDbl(TotalThickSub(i - 1)) / 1000) / 1728) * 62.4 * CDbl(dsPref.Tables(0).Rows(0).Item("TYPEMSG" + i.ToString() + "").ToString())
                        Else
                            ARRlayerWT(i) = BLLayerWTSUB
                        End If
                    End If
                Else
                    If CDbl(dsPref.Tables(0).Rows(0).Item("SGP" + i.ToString() + "").ToString()) <> 0.0 Then
                        ARRlayerWT(i) = (1000 * (CDbl(dsPref.Tables(0).Rows(0).Item("THICK" + i.ToString() + "").ToString()) / 1000) / 1728) * 62.4 * CDbl(dsPref.Tables(0).Rows(0).Item("SGP" + i.ToString() + "").ToString())
                    Else
                        ARRlayerWT(i) = (1000 * (CDbl(dsPref.Tables(0).Rows(0).Item("THICK" + i.ToString() + "").ToString()) / 1000) / 1728) * 62.4 * CDbl(dsSug.Tables(0).Rows(0).Item("SGS" + i.ToString() + "").ToString())
                    End If
                End If
                WTPERAREA = WTPERAREA + CDbl(ARRlayerWT(i))
            Next

            For i = 1 To 10
                If WTPERAREA > 0 Then
                    WeightPer(i) = CDbl(ARRlayerWT(i)) / CDbl(WTPERAREA) * 100
                Else
                    WeightPer(i) = 0.0
                End If
                'Weight Percentage for Sub Layers
                For l = 1 To 2
                    If WTPERAREASUB(i) > 0 Then
                        WeightPerSub(i, l) = CDbl(ARRlayerWTSUB(i, l)) / WTPERAREASUB(i) * 100
                    Else
                        WeightPerSub(i, l) = 0.0
                    End If
                Next
                WeightPerT = WeightPerT + WeightPer(i)
            Next

            'Total Specific Gravity 
            If CDbl(TotalThick) <> CDbl(0) Then
                TotalSG = (WTPERAREA / ((1000 * TotalThick / 1000) / 1728)) / 62.4
            End If

            For i = 1 To 17
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "40px", "Layers", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "120px", "Primary Materials", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "120px", "Grade", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        tdHeader.ID = "GradeHeader"
                        tdHeader.Width = 50
                        tdHeader2.ID = "GradeHeader2"
                        tdHeader1.ID = "GradeHeader1"

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
                        HeaderTdSetting(tdHeader, "80px", "Weight ", "1")
                        Header2TdSetting(tdHeader2, "", "(%)", "1")
                        Header2TdSetting(tdHeader1, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)

                    Case 6
                        Title = "" + dsPref.Tables(0).Rows(0).Item("TITLE15").ToString() + ""
                        HeaderTdSetting(tdHeader, "140px", "OTR", "2")
                        tdHeader.Attributes.Add("Title", "Oxygen Transmission Rate")
                        Header2TdSetting(tdHeader1, "76px", "Suggested", "1")
                        If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                            Header2TdSetting(tdHeader2, "", "(cc/ " + Title + "/day)", "2")
                        Else
                            Header2TdSetting(tdHeader2, "", "(cc/100 " + Title + "/day)", "2")
                        End If

                        tdHeader.ID = "OTRHeader"
                        tdHeader2.ID = "OTRHeader2"
                        tdHeader1.ID = "OTRSHeader1"

                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 7
                        Header2TdSetting(tdHeader1, "76px", "Preferred", "1")
                        tdHeader1.ID = "OTRPHeader1"
                        trHeader1.Controls.Add(tdHeader1)

                    Case 8
                        Title = "" + dsPref.Tables(0).Rows(0).Item("TITLE15").ToString() + ""
                        HeaderTdSetting(tdHeader, "140px", "WVTR", "2")
                        tdHeader.Attributes.Add("Title", "Water Vapor Transmission Rate")
                        Header2TdSetting(tdHeader1, "76px", "Suggested", "1")
                        If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                            Header2TdSetting(tdHeader2, "", "(gm/" + Title + "/day)", "2")
                        Else
                            Header2TdSetting(tdHeader2, "", "(gm/100 " + Title + "/day)", "2")
                        End If

                        tdHeader.ID = "WVTRHeader"
                        tdHeader2.ID = "WVTRHeader2"
                        tdHeader1.ID = "WVTRSHeader1"

                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)

                    Case 9
                        Header2TdSetting(tdHeader1, "76px", "Preferred", "1")
                        tdHeader1.ID = "WVTRPHeader1"
                        trHeader1.Controls.Add(tdHeader1)

                    Case 10
                        Title = "(" + dsPref.Tables(0).Rows(0).Item("TITLE21").ToString() + ")"
                        HeaderTdSetting(tdHeader, "140px", "Tensile at Break MD", "2")
                        Header2TdSetting(tdHeader1, "76px", "Suggested", "1")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        tdHeader.ID = "TS1Header"
                        tdHeader2.ID = "TS1Header2"
                        tdHeader1.ID = "TS1SHeader1"

                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)

                    Case 11
                        Header2TdSetting(tdHeader1, "76px", "Preferred", "1")
                        tdHeader1.ID = "TS1PHeader1"
                        trHeader1.Controls.Add(tdHeader1)

                    Case 12
                        Title = "(" + dsPref.Tables(0).Rows(0).Item("TITLE21").ToString() + ")"
                        HeaderTdSetting(tdHeader, "140px", "Tensile at Break TD", "2")
                        Header2TdSetting(tdHeader1, "76px", "Suggested", "1")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        tdHeader.ID = "TS2Header"
                        tdHeader2.ID = "TS2Header2"
                        tdHeader1.ID = "TS2SHeader1"

                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)

                    Case 13
                        Header2TdSetting(tdHeader1, "76px", "Preferred", "1")
                        tdHeader1.ID = "TS2PHeader1"
                        trHeader1.Controls.Add(tdHeader1)

                    Case 14
                        HeaderTdSetting(tdHeader, "", "Specific Gravity", "2")
                        Header2TdSetting(tdHeader2, "", Title, "2")
                        Header2TdSetting(tdHeader1, "50px", "Suggested", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 15
                        Header2TdSetting(tdHeader1, "70px", "Preferred", "1")
                        trHeader1.Controls.Add(tdHeader1)

                    Case 16
                        HeaderTdSetting(tdHeader, "", "Supplier Grades", "2")
                        Header2TdSetting(tdHeader2, "", "", "2")
                        Header2TdSetting(tdHeader1, "120px", "Grades", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                        trHeader1.Controls.Add(tdHeader1)

                    Case 17
                        Header2TdSetting(tdHeader1, "50px", "TDS", "1")
                        tdHeader1.Attributes.Add("onmouseover", "Tip('Technical Data Sheet')")
                        tdHeader1.Attributes.Add("onmouseout", "UnTip('')")
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
                            hidDes = New HiddenField
                            hidMat = New HiddenField
                            Link.ID = "hypMatDes" + i.ToString()
                            hid.ID = "hidMatid" + i.ToString()
                            hidDes.ID = "hidMatDes" + i.ToString()
                            hidMat.ID = "hidMatCat" + i.ToString()
                            Link.Width = 120
                            Link.CssClass = "Link"
                            If dsPref.Tables(0).Rows(0).Item("TYPEM" + i.ToString()).ToString() <> "0" Then
                                GetMaterialDetailsNew(Link, hid, CInt(dsPref.Tables(0).Rows(0).Item("TYPEM" + i.ToString() + "").ToString()), "hypGradeDes" + i.ToString(), "hidGradeId" + i.ToString(), "SG" + i.ToString(), hidDes, hidMat, "lblGradeDes" + i.ToString(), dsGMat1)
                            ElseIf dsPref.Tables(0).Rows(0).Item("M" + i.ToString()).ToString() <> "" Then
                                GetMaterialDetailsNew(Link, hid, CInt(dsPref.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()), "hypGradeDes" + i.ToString(), "hidGradeId" + i.ToString(), "SG" + i.ToString(), hidDes, hidMat, "lblGradeDes" + i.ToString(), dsGMat1)
                            End If


                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(hidDes)
                            tdInner.Controls.Add(hidMat)
                            tdInner.Controls.Add(Link)
                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Left")
                            label = New Label
                            hid = New HiddenField
                            label.ID = "lblGradeDes" + i.ToString()
                            hid.ID = "hidGradeId" + i.ToString()
                            label.Width = 120
                            label.CssClass = "NormalLabel"
                            GetGrades(hid, "hidMatid" + i.ToString(), CInt(dsPref.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()), "SG" + i.ToString(), label)
                            tdInner.ID = "GradeVal" + i.ToString()

                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(label)
                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Center")
                            If dsPref.Tables(0).Rows(0).Item("TYPEM" + i.ToString()).ToString() <> "0" Then
                                lbl = New Label
                                lbl.ID = "lblT" + i.ToString()
                                dvBPref.RowFilter = "CASEID=" + CaseId.ToString() + " AND TYPEM=" + dsPref.Tables(0).Rows(0).Item("TYPEM" + i.ToString()).ToString()
                                dtBPref = dvBPref.ToTable()
                                If dtBPref.Rows.Count > 0 Then
                                    For k = 0 To 1
                                        blendThick = blendThick + dtBPref.Rows(0).Item("BCT" + (k + 1).ToString() + "")
                                    Next
                                    lbl.Text = FormatNumber(blendThick, 3)
                                    blendThick = 0
                                Else
                                    lbl.Text = ""
                                End If
                                txt = New TextBox
                                txt.CssClass = "SmallTextBox"
                                txt.ID = "T" + i.ToString()
                                txt.Text = "0.00"
                                txt.Style.Add("Display", "none")
                                tdInner.Controls.Add(txt)
                                tdInner.Controls.Add(lbl)
                            ElseIf dsPref.Tables(0).Rows(0).Item("M" + i.ToString()).ToString() <> "" Then
                                txt = New TextBox
                                txt.CssClass = "SmallTextBox"
                                txt.ID = "T" + i.ToString()
                                txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("THICK" + i.ToString() + "").ToString(), 3)
                                txt.MaxLength = 6
                                lbl = New Label
                                lbl.ID = "lblT" + i.ToString()
                                lbl.Style.Add("Display", "none")
                                tdInner.Controls.Add(lbl)
                                tdInner.Controls.Add(txt)
                            End If
                            trInner.Controls.Add(tdInner)

                        Case 5
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.ID = "lblWtPer" + i.ToString()
                            lbl.Text = FormatNumber(WeightPer(i), 2)
                            lbl.CssClass = "NormalLabel"
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "")
                            lbl = New Label
                            imgOTR = New ImageButton
                            imgOTR.ID = "imgOTR" + i.ToString()
                            imgOTR.ImageUrl = "~/Images/595958.png"
                            imgOTR.ToolTip = "View OTR Chart"
                            imgOTR.Attributes.Add("style", "float:right; margin-top:-8px; display:inline")
                            lbl.Attributes.Add("style", "padding-left:5px;")
                            imgOTR.Height = 6
                            imgOTR.Width = 6
                            imgOTR.Attributes.Add("onclick", "javascript:ShowChartWindow('../../../Charts/StandAssist/Chart.aspx?MatId=" + ctlContentPlaceHolder.ClientID.ToString() + "_tabSDesigner_tabpnl1_" + imgOTR.ClientID + "&Type=OTR'); return false;")

                            If dsPref.Tables(0).Rows(0).Item("TYPEM" + i.ToString()).ToString() <> "0" Then
                                If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                    lbl.Text = FormatNumber(objCalb.OTR(i - 1), 3)
                                Else
                                    lbl.Text = FormatNumber(CDbl(objCalb.OTR(i - 1)) * CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString() / 1000) * 100, 3)
                                End If

                                tdInner.Controls.Add(imgOTR)
                            ElseIf dsPref.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString() <> 0 Then
                                If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                    lbl.Text = FormatNumber(objCalb.OTRB(i - 1), 3)
                                Else
                                    lbl.Text = FormatNumber(CDbl(objCalb.OTRB(i - 1)) * CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString() / 1000) * 100, 3)
                                End If
                                tdInner.Controls.Add(imgOTR)
                            Else
                                lbl.Text = ""
                            End If

                            tdInner.ID = "OTRSVal" + i.ToString()
                            lbl.Style.Add("display", "block")
                            lbl.Style.Add("text-align", "right")
                            lbl.CssClass = "NormalLabel"
                            lbl.Style.Add("margin-right", "5px")
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "OTR" + i.ToString()
                            If Not IsNumeric(dsPref.Tables(0).Rows(0).Item("OTR" + i.ToString())) Then
                                txt.Text = ""
                            Else
                                If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                    txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("OTR" + i.ToString()).ToString(), 3)
                                Else
                                    txt.Text = FormatNumber(CDbl(dsPref.Tables(0).Rows(0).Item("OTR" + i.ToString())) * CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString() / 1000) * 100, 3)
                                End If
                            End If
                            txt.MaxLength = 6

                            tdInner.ID = "OTRPVal" + i.ToString()

                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "")
                            lbl = New Label
                            imgWVTR = New ImageButton
                            imgWVTR.ID = "imgWVTR" + i.ToString()
                            imgWVTR.ImageUrl = "~/Images/595958.png"
                            imgWVTR.ToolTip = "View WVTR Chart"
                            imgWVTR.Attributes.Add("style", "float:right; margin-top:-8px;")
                            imgWVTR.Style.Add("Display", "inline")
                            lbl.Attributes.Add("style", "padding-left:5px;")
                            imgWVTR.Height = 6
                            imgWVTR.Width = 6
                            imgWVTR.Attributes.Add("onclick", "javascript:ShowChartWindow('../../../Charts/StandAssist/Chart.aspx?MatId=" + ctlContentPlaceHolder.ClientID.ToString() + "_tabSDesigner_tabpnl1_" + imgWVTR.ClientID + "&Type=WVTR'); return false;")

                            If dsPref.Tables(0).Rows(0).Item("TYPEM" + i.ToString()).ToString() <> "0" Then
                                If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                    lbl.Text = FormatNumber(objCalb.WVTR(i - 1), 3)
                                Else
                                    lbl.Text = FormatNumber(CDbl(objCalb.WVTR(i - 1)) * CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString() / 1000) * 100, 3)
                                End If

                                'If Not IsNumeric(dsPref.Tables(0).Rows(0).Item("WVTR" + i.ToString())) Then
                                '    MTR(i - 1) = (objCalb.WVTR(i - 1) * Thick(i - 1) / 25.4)
                                'Else
                                '    MTR(i - 1) = (dsPref.Tables(0).Rows(0).Item("WVTR" + i.ToString()) * Thick(i - 1) / 25.4)
                                'End If
                                If Not IsNumeric(dsPref.Tables(0).Rows(0).Item("WVTR" + i.ToString())) Then
                                    MTR(i - 1) = (objCalb1.WVTR(i - 1) * Thick(i - 1) / 25.4)
                                Else
                                    MTR(i - 1) = (dsPref.Tables(0).Rows(0).Item("WVTR" + i.ToString()) * Thick(i - 1) / 25.4)
                                End If
                                tdInner.Controls.Add(imgWVTR)
                            ElseIf dsPref.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString() <> 0 Then
                                If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                    lbl.Text = FormatNumber(objCalb.WVTRB(i - 1), 3)
                                Else
                                    lbl.Text = FormatNumber(CDbl(objCalb.WVTRB(i - 1)) * CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString() / 1000) * 100, 3)
                                End If
                                'If Not IsNumeric(dsPref.Tables(0).Rows(0).Item("WVTR" + i.ToString())) Then
                                '    If dsSug.Tables(0).Rows(0).Item("ISADJTHICK" + i.ToString() + "").ToString() <> "Y" Then
                                '        MTR(i - 1) = objCalb.WVTRB(i - 1)
                                '    Else
                                '        MTR(i - 1) = (objCalb.WVTRB(i - 1) * Thick(i - 1) / 25.4)
                                '    End If
                                'Else
                                '    MTR(i - 1) = (dsPref.Tables(0).Rows(0).Item("WVTR" + i.ToString()) * Thick(i - 1) / 25.4)
                                'End If
                                If Not IsNumeric(dsPref.Tables(0).Rows(0).Item("WVTR" + i.ToString())) Then
                                    MTR(i - 1) = objCalb1.WVTRB(i - 1)
                                Else
                                    MTR(i - 1) = (dsPref.Tables(0).Rows(0).Item("WVTR" + i.ToString()) * Thick(i - 1) / 25.4)
                                End If
                                tdInner.Controls.Add(imgWVTR)
                            Else
                                lbl.Text = ""
                            End If

                            tdInner.ID = "WVTRSVal" + i.ToString()

                            lbl.Style.Add("display", "block")
                            lbl.Style.Add("margin-right", "5px")
                            lbl.Style.Add("text-align", "right")
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 9
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "WVTR" + i.ToString()
                            If Not IsNumeric(dsPref.Tables(0).Rows(0).Item("WVTR" + i.ToString())) Then
                                txt.Text = ""
                            Else
                                If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                    txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("WVTR" + i.ToString()).ToString(), 3)
                                Else
                                    txt.Text = FormatNumber(CDbl(dsPref.Tables(0).Rows(0).Item("WVTR" + i.ToString())) * CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString() / 1000) * 100, 3)
                                End If
                            End If
                            txt.MaxLength = 6

                            tdInner.ID = "WVTRPVal" + i.ToString()
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)

                        Case 10
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            If dsPref.Tables(0).Rows(0).Item("TYPEM" + i.ToString()).ToString() <> "0" Then
                                If objCalb.TS1(i - 1) <> "" Then
                                    If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                        lbl.Text = FormatNumber(objCalb.TS1(i - 1), 1)
                                    Else
                                        lbl.Text = FormatNumber(CDbl(objCalb.TS1(i - 1)) / dsPref.Tables(0).Rows(0).Item("CONVPA"), 0)
                                    End If
                                End If
                            ElseIf dsPref.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString() <> 0 Then
                                If objCalb.TS1B(i - 1) <> "" Then
                                    If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                        lbl.Text = FormatNumber(objCalb.TS1B(i - 1), 1)
                                    Else
                                        lbl.Text = FormatNumber(CDbl(objCalb.TS1B(i - 1)) / dsPref.Tables(0).Rows(0).Item("CONVPA"), 0)
                                    End If
                                End If
                            Else
                                lbl.Text = ""
                            End If

                            tdInner.ID = "TS1SVal" + i.ToString()
                            lbl.CssClass = "NormalLabel"
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 11
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "TS1Val" + i.ToString()
                            If Not IsNumeric(dsPref.Tables(0).Rows(0).Item("TS1VAL" + i.ToString())) Then
                                txt.Text = ""
                            Else
                                If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                    txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("TS1VAL" + i.ToString()).ToString() / dsPref.Tables(0).Rows(0).Item("CONVPA"), 1)
                                Else
                                    txt.Text = FormatNumber(CDbl(dsPref.Tables(0).Rows(0).Item("TS1VAL" + i.ToString())) / dsPref.Tables(0).Rows(0).Item("CONVPA"), 0)
                                End If
                            End If
                            txt.MaxLength = 6
                            txt.Width = 70
                            tdInner.ID = "TS1PVal" + i.ToString()
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)

                        Case 12
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            If dsPref.Tables(0).Rows(0).Item("TYPEM" + i.ToString()).ToString() <> "0" Then
                                If objCalb.TS2(i - 1) <> "" Then
                                    If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                        lbl.Text = FormatNumber(objCalb.TS2(i - 1) / dsPref.Tables(0).Rows(0).Item("CONVPA"), 1)
                                    Else
                                        lbl.Text = FormatNumber(CDbl(objCalb.TS2(i - 1)) / dsPref.Tables(0).Rows(0).Item("CONVPA"), 0)
                                    End If
                                End If
                            ElseIf dsPref.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString() <> 0 Then
                                If objCalb.TS2B(i - 1) <> "" Then
                                    If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                        lbl.Text = FormatNumber(objCalb.TS2B(i - 1) / dsPref.Tables(0).Rows(0).Item("CONVPA"), 1)
                                    Else
                                        lbl.Text = FormatNumber(CDbl(objCalb.TS2B(i - 1)) / dsPref.Tables(0).Rows(0).Item("CONVPA"), 0)
                                    End If
                                End If
                            Else
                                lbl.Text = ""
                            End If

                            tdInner.ID = "TS2SVal" + i.ToString()
                            lbl.CssClass = "NormalLabel"
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)
                        Case 13
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "TS2Val" + i.ToString()
                            If Not IsNumeric(dsPref.Tables(0).Rows(0).Item("TS2VAL" + i.ToString())) Then
                                txt.Text = ""
                            Else
                                If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                    txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("TS2VAL" + i.ToString()).ToString(), 1)
                                Else
                                    txt.Text = FormatNumber(CDbl(dsPref.Tables(0).Rows(0).Item("TS2VAL" + i.ToString())) / dsPref.Tables(0).Rows(0).Item("CONVPA"), 0)
                                End If
                            End If
                            txt.MaxLength = 6
                            txt.Width = 70
                            tdInner.ID = "TS2PVal" + i.ToString()
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)

                        Case 14
                            InnerTdSetting(tdInner, "", "Right")
                            lbl = New Label
                            lbl.ID = "lblSg" + i.ToString()
                            If dsPref.Tables(0).Rows(0).Item("TYPEM" + i.ToString()).ToString() <> "0" Then
                                If CDbl(TotalThickSub(i - 1)) <> CDbl(0) Then
                                    Dim Sg As Double
                                    Sg = (WTPERAREASUB(i) / ((1000 * TotalThickSub(i - 1) / 1000) / 1728)) / 62.4
                                    lbl.Text = FormatNumber(Sg, 3).ToString()
                                End If
                            ElseIf dsPref.Tables(0).Rows(0).Item("M" + i.ToString()).ToString() <> "" Then
                                lbl.Text = FormatNumber(dsSug.Tables(0).Rows(0).Item("SGS" + i.ToString() + "").ToString(), 3)
                            End If
                            If i = 1 Then
                                hid = New HiddenField
                                hid.ID = "UnitId"
                                hid.Value = dsPref.Tables(0).Rows(0).Item("UNITS").ToString()
                            End If
                            tdInner.Controls.Add(hid)
                            lbl.CssClass = "NormalLabel"
                            tdInner.Controls.Add(lbl)
                            trInner.Controls.Add(tdInner)

                        Case 15
                            InnerTdSetting(tdInner, "", "Center")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.Width = 70
                            txt.ID = "SG" + i.ToString()
                            If dsPref.Tables(0).Rows(0).Item("TYPEM" + i.ToString()).ToString() <> "0" Then
                                txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("TYPEMSG" + i.ToString() + "").ToString(), 3)
                            Else
                                txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("SGP" + i.ToString() + "").ToString(), 3)
                            End If

                            txt.MaxLength = 9
                            tdInner.Controls.Add(txt)
                            trInner.Controls.Add(tdInner)

                        Case 16
                            InnerTdSetting(tdInner, "", "Left")
                            If dsPref.Tables(0).Rows(0).Item("TYPEM" + i.ToString()).ToString() <> "0" Then
                                Link = New HyperLink
                                hid = New HiddenField
                                hid1 = New HiddenField
                                Link.ID = "hypSGradeDes" + i.ToString()
                                hid.ID = "hidSGradeId" + i.ToString()
                                hid1.ID = "hidSupId" + i.ToString()
                                ImgBut = New ImageButton
                                ImgBut.ID = "imgBut" + i.ToString()

                                ImgDis = New ImageButton
                                ImgDis.ID = "imgDis" + i.ToString()
                                tdInner.ID = "SGradeVal" + i.ToString()
                                Link.Style.Add("Display", "none")
                                ImgBut.ImageUrl = "~/Images/Notes Icon2.png"
                                ImgDis.ImageUrl = "~/Images/Notes Icon3.png"
                                ImgBut.Style.Add("display", "none")
                                ImgDis.Style.Add("display", "none")
                                Dim Path As String = ""
                                Path = "../PopUp/SupplierMaterials.aspx"

                                Link.CssClass = "Link"
                                Link.Width = 130
                                GetSGrades(Link, hid, "hidMatid" + i.ToString() + "", dsPref.Tables(0).Rows(0).Item("GRADE" + i.ToString()), hid1, CInt(dsPref.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()), ImgBut, ImgDis)
                                ImgDis.Width = 20
                                ImgDis.Height = 18
                                ImgBut.Width = 20
                                ImgBut.Height = 18
                                ImgBut.Style.Add("margin-left", "2px")
                                ImgDis.Style.Add("margin-left", "2px")
                                ImgBut.Style.Add("margin-right", "2px")
                                ImgDis.Style.Add("margin-right", "2px")
                                ImgBut.Attributes.Add("onclick", "OpenTDataSheet('hidSupId" + i.ToString() + "','hidMatid" + i.ToString() + "','" + Path + "','hidSGradeId" + i.ToString() + "'); return false;")

                            Else
                                Link = New HyperLink
                                hid = New HiddenField
                                hid1 = New HiddenField
                                Link.ID = "hypSGradeDes" + i.ToString()
                                hid.ID = "hidSGradeId" + i.ToString()
                                hid1.ID = "hidSupId" + i.ToString()
                                ImgBut = New ImageButton
                                ImgBut.ID = "imgBut" + i.ToString()

                                ImgDis = New ImageButton
                                ImgDis.ID = "imgDis" + i.ToString()
                                ImgDis.Width = 20
                                ImgDis.Height = 18
                                ImgDis.Enabled = False
                                ImgDis.ImageUrl = "~/Images/Notes Icon3.png"

                                Link.CssClass = "Link"
                                Link.Width = 130
                                GetSGrades(Link, hid, "hidMatid" + i.ToString() + "", dsPref.Tables(0).Rows(0).Item("GRADE" + i.ToString()), hid1, CInt(dsPref.Tables(0).Rows(0).Item("M" + i.ToString() + "").ToString()), ImgBut, ImgDis)
                                tdInner.ID = "SGradeVal" + i.ToString()
                                ImgBut.Width = 20
                                ImgBut.Height = 18
                                ImgBut.ImageUrl = "~/Images/Notes Icon2.png"
                                Dim Path As String = ""
                                Path = "../PopUp/SupplierMaterials.aspx"
                                ImgBut.Attributes.Add("onclick", "OpenTDataSheet('hidSupId" + i.ToString() + "','hidMatid" + i.ToString() + "','" + Path + "','hidSGradeId" + i.ToString() + "'); return false;")
                                ImgBut.Style.Add("margin-left", "2px")
                                ImgDis.Style.Add("margin-left", "2px")
                                ImgBut.Style.Add("margin-right", "2px")
                                ImgDis.Style.Add("margin-right", "2px")
                            End If
                            tdInner.Controls.Add(hid)
                            tdInner.Controls.Add(hid1)
                            tdInner.Controls.Add(Link)
                            trInner.Controls.Add(tdInner)
                        Case 17
                            InnerTdSetting(tdInner, "", "Left")
                            tdInner.Controls.Add(ImgBut)
                            tdInner.Controls.Add(ImgDis)
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

                'Blend Sub Layers Design
                If j = 18 Then
                    dvBPref.RowFilter = "CASEID=" + CaseId.ToString() + " AND TYPEM=" + dsPref.Tables(0).Rows(0).Item("TYPEM" + i.ToString()).ToString() + ""
                    dtBPref = dvBPref.ToTable()

                    dvBSug.RowFilter = "TYPEM=" + dsPref.Tables(0).Rows(0).Item("TYPEM" + i.ToString()).ToString() + ""
                    dtBSug = dvBSug.ToTable()

                    Dim cntblendmat As Integer = 0
                    If cntblendmat <= 2 Then
                        For k = 1 To 2
                            cntblendmat = cntblendmat + 1
                            trInner = New TableRow
                            trInner.ID = "Blendrow" + i.ToString() + "_" + cntblendmat.ToString()
                            For m = 1 To 17
                                tdInner = New TableCell
                                Select Case m
                                    Case 1
                                        'Layer
                                        InnerTdSetting(tdInner, "", "Center")
                                        tdInner.Text = " "
                                        trInner.Controls.Add(tdInner)
                                    Case 2
                                        InnerTdSetting(tdInner, "", "Left")
                                        Link = New HyperLink
                                        hidB = New HiddenField
                                        hidBDes = New HiddenField
                                        hidBMat = New HiddenField
                                        Link.ID = "hypBlendDes" + i.ToString() + "_" + cntblendmat.ToString()
                                        hidB.ID = "hidBlendid" + i.ToString() + "_" + cntblendmat.ToString()
                                        hidBDes.ID = "hidBlendDes" + i.ToString() + "_" + cntblendmat.ToString()
                                        hidBMat.ID = "hidBlendCat" + i.ToString() + "_" + cntblendmat.ToString()
                                        Link.Width = 120
                                        Link.CssClass = "Link"
                                        If dtBPref.Rows.Count > 0 Then
                                            If dtBPref.Rows(0).Item("BCM" + k.ToString() + "").ToString() = "0" Then
                                                GetBlendMaterialDetailsNew(Link, hidB, 0, "hypBlendDes" + i.ToString() + "_" + cntblendmat.ToString(), "hidBGradeId" + i.ToString() + "_" + cntblendmat.ToString(), "BSG" + i.ToString() + "_" + cntblendmat.ToString(), hidBDes, hidBMat, "lblBGradeDes" + i.ToString() + "_" + cntblendmat.ToString(), i.ToString(), dsGBM)
                                            Else
                                                GetBlendMaterialDetailsNew(Link, hidB, CInt(dtBPref.Rows(0).Item("BCM" + k.ToString() + "").ToString()), "hypBlendDes" + i.ToString() + "_" + cntblendmat.ToString(), "hidBGradeId" + i.ToString() + "_" + cntblendmat.ToString(), "BSG" + i.ToString() + "_" + cntblendmat.ToString(), hidBDes, hidBMat, "lblBGradeDes" + i.ToString() + "_" + cntblendmat.ToString(), i.ToString(), dsGBM)
                                            End If
                                        Else
                                            GetBlendMaterialDetailsNew(Link, hidB, 0, "hypBlendDes" + i.ToString() + "_" + cntblendmat.ToString(), "hidBGradeId" + i.ToString() + "_" + cntblendmat.ToString(), "BSG" + i.ToString() + "_" + cntblendmat.ToString(), hidBDes, hidBMat, "lblBGradeDes" + i.ToString() + "_" + cntblendmat.ToString(), i.ToString(), dsGBM)
                                        End If
                                        tdInner.Controls.Add(hidB)
                                        tdInner.Controls.Add(hidBDes)
                                        tdInner.Controls.Add(hidBMat)
                                        tdInner.Controls.Add(Link)
                                        trInner.Controls.Add(tdInner)
                                    Case 3
                                        InnerTdSetting(tdInner, "", "Left")
                                        label = New Label
                                        Link = New HyperLink
                                        hid = New HiddenField
                                        label.ID = "lblBGradeDes" + i.ToString() + "_" + cntblendmat.ToString()
                                        Link.ID = "hypBGradeDes" + i.ToString() + "_" + cntblendmat.ToString()
                                        hid.ID = "hidBGradeId" + i.ToString() + "_" + cntblendmat.ToString()
                                        label.Width = 120
                                        Link.CssClass = "Link"
                                        If dtBPref.Rows.Count > 0 Then
                                            If dtBPref.Rows(0).Item("BCM" + k.ToString() + "").ToString() = "0" Then
                                                GetGrades(hid, "hidBlendid" + i.ToString() + "_" + cntblendmat.ToString(), 0, "BSG" + i.ToString() + "_" + cntblendmat.ToString(), label)
                                            Else
                                                GetGrades(hid, "hidBlendid" + i.ToString() + "_" + cntblendmat.ToString(), CInt(dtBPref.Rows(0).Item("BCM" + k.ToString() + "").ToString()), "BSG" + i.ToString() + "_" + cntblendmat.ToString(), label)
                                            End If
                                        Else
                                            GetGrades(hid, "hidBlendid" + i.ToString() + "_" + cntblendmat.ToString(), 0, "BSG" + i.ToString() + "_" + cntblendmat.ToString(), label)
                                        End If
                                        tdInner.ID = "BGradeVal" + i.ToString() + "_" + cntblendmat.ToString()
                                        tdInner.Controls.Add(hid)
                                        tdInner.Controls.Add(label)
                                        trInner.Controls.Add(tdInner)
                                    Case 4
                                        InnerTdSetting(tdInner, "", "Center")
                                        txt = New TextBox
                                        txt.CssClass = "SmallTextBox"
                                        txt.ID = "BT" + i.ToString() + "_" + cntblendmat.ToString()
                                        If dtBPref.Rows.Count > 0 Then
                                            If dtBPref.Rows(0).Item("BCT" + k.ToString() + "").ToString() = "" Then
                                                txt.Text = "0.000"
                                            Else
                                                txt.Text = FormatNumber(dtBPref.Rows(0).Item("BCT" + k.ToString() + "").ToString(), 3)
                                            End If
                                        Else
                                            txt.Text = "0.000"
                                        End If
                                        txt.MaxLength = 6
                                        tdInner.Controls.Add(txt)
                                        trInner.Controls.Add(tdInner)
                                    Case 5
                                        InnerTdSetting(tdInner, "", "Right")
                                        lbl = New Label
                                        lbl.Text = FormatNumber(WeightPerSub(i, k), 2)
                                        tdInner.Controls.Add(lbl)
                                        trInner.Controls.Add(tdInner)
                                    Case 6
                                        InnerTdSetting(tdInner, "", "")
                                        lbl = New Label
                                        imgOTR = New ImageButton
                                        imgOTR.ID = "imgBOTR" + i.ToString() + "_" + cntblendmat.ToString()
                                        imgOTR.ImageUrl = "~/Images/595958.png"
                                        imgOTR.ToolTip = "View OTR Chart"
                                        imgOTR.Attributes.Add("style", "float:right; margin-top:-8px;")
                                        imgOTR.Style.Add("Display", "inline")
                                        imgOTR.Height = 6
                                        imgOTR.Width = 6
                                        lbl.Attributes.Add("style", "padding-left:5px;")
                                        imgOTR.Attributes.Add("onclick", "javascript:ShowChartWindow('../../../Charts/StandAssist/Chart.aspx?MatId=" + ctlContentPlaceHolder.ClientID.ToString() + "_tabSDesigner_tabpnl1_" + imgOTR.ClientID + "&Type=OTR'); return false;")

                                        If dtBPref.Rows.Count > 0 Then
                                            If dtBPref.Rows(0).Item("BCM" + k.ToString() + "").ToString() <> "" Then
                                                If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                                    lbl.Text = FormatNumber(objCalb.OTRBLENDSUB(i - 1, k - 1), 3)
                                                Else
                                                    lbl.Text = FormatNumber(CDbl(objCalb.OTRBLENDSUB(i - 1, k - 1)) * CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString() / 1000) * 100, 3)
                                                End If
                                                If dtBPref.Rows(0).Item("BCM" + k.ToString() + "").ToString() <> "0" Then
                                                    tdInner.Controls.Add(imgOTR)
                                                End If
                                            Else
                                                lbl.Text = ""
                                            End If
                                        Else
                                            lbl.Text = ""
                                        End If
                                        lbl.Style.Add("display", "block")
                                        lbl.Style.Add("margin-right", "5px")
                                        lbl.Style.Add("text-align", "right")
                                        tdInner.ID = "BOTRSVal" + i.ToString() + "_" + cntblendmat.ToString()
                                        tdInner.Controls.Add(lbl)
                                        trInner.Controls.Add(tdInner)
                                    Case 7
                                        InnerTdSetting(tdInner, "", "Center")
                                        txt = New TextBox
                                        txt.CssClass = "SmallTextBox"
                                        txt.ID = "BOTR" + i.ToString() + "_" + cntblendmat.ToString()
                                        If dtBPref.Rows.Count > 0 Then
                                            If Not IsNumeric(dtBPref.Rows(0).Item("BCOTR" + k.ToString())) Then
                                                txt.Text = ""
                                            Else
                                                If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                                    txt.Text = FormatNumber(dtBPref.Rows(0).Item("BCOTR" + k.ToString()).ToString(), 3)
                                                Else
                                                    txt.Text = FormatNumber(CDbl(dtBPref.Rows(0).Item("BCOTR" + k.ToString())) * CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString() / 1000) * 100, 3)
                                                End If
                                            End If
                                        Else
                                            txt.Text = ""
                                        End If
                                        txt.MaxLength = 6
                                        tdInner.ID = "BOTRPVal" + i.ToString() + "_" + cntblendmat.ToString()
                                        tdInner.Controls.Add(txt)
                                        trInner.Controls.Add(tdInner)
                                    Case 8
                                        InnerTdSetting(tdInner, "", "")
                                        lbl = New Label
                                        imgWVTR = New ImageButton
                                        imgWVTR.ID = "imgBWVTR" + i.ToString() + "_" + cntblendmat.ToString()
                                        imgWVTR.ImageUrl = "~/Images/595958.png"
                                        imgWVTR.ToolTip = "View WVTR Chart"
                                        imgWVTR.Attributes.Add("style", "float:right; margin-top:-8px;")
                                        imgWVTR.Style.Add("Display", "inline")
                                        imgWVTR.Height = 6
                                        imgWVTR.Width = 6
                                        lbl.Attributes.Add("style", "padding-left:5px;")
                                        imgWVTR.Attributes.Add("onclick", "javascript:ShowChartWindow('../../../Charts/StandAssist/Chart.aspx?MatId=" + ctlContentPlaceHolder.ClientID.ToString() + "_tabSDesigner_tabpnl1_" + imgWVTR.ClientID + "&Type=WVTR'); return false;")

                                        If dtBPref.Rows.Count > 0 Then
                                            If dtBPref.Rows(0).Item("BCM" + k.ToString() + "").ToString() <> "" Then
                                                If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                                    lbl.Text = FormatNumber(objCalb.WVTRBLENDSUB(i - 1, k - 1), 3)
                                                Else
                                                    lbl.Text = FormatNumber(CDbl(objCalb.WVTRBLENDSUB(i - 1, k - 1)) * CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString() / 1000) * 100, 3)
                                                End If
                                                If dtBPref.Rows(0).Item("BCM" + k.ToString() + "").ToString() <> "0" Then
                                                    tdInner.Controls.Add(imgWVTR)
                                                End If
                                            Else
                                                lbl.Text = ""
                                            End If
                                        Else
                                            lbl.Text = ""
                                        End If
                                        lbl.Style.Add("display", "block")
                                        lbl.Style.Add("margin-right", "5px")
                                        lbl.Style.Add("text-align", "right")
                                        tdInner.ID = "BWVTRSVal" + i.ToString() + "_" + cntblendmat.ToString()
                                        tdInner.Controls.Add(lbl)
                                        trInner.Controls.Add(tdInner)
                                    Case 9
                                        InnerTdSetting(tdInner, "", "Center")
                                        txt = New TextBox
                                        txt.CssClass = "SmallTextBox"
                                        txt.ID = "BWVTR" + i.ToString() + "_" + cntblendmat.ToString()
                                        If dtBPref.Rows.Count > 0 Then
                                            If Not IsNumeric(dtBPref.Rows(0).Item("BCWVTR" + k.ToString())) Then
                                                txt.Text = ""
                                            Else
                                                If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                                    txt.Text = FormatNumber(dtBPref.Rows(0).Item("BCWVTR" + k.ToString()).ToString(), 3)
                                                Else
                                                    txt.Text = FormatNumber(CDbl(dtBPref.Rows(0).Item("BCWVTR" + k.ToString())) * CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString() / 1000) * 100, 3)
                                                End If
                                            End If
                                        Else
                                            txt.Text = ""
                                        End If
                                        txt.MaxLength = 6
                                        tdInner.ID = "BWVTRPVal" + i.ToString() + "_" + cntblendmat.ToString()
                                        tdInner.Controls.Add(txt)
                                        trInner.Controls.Add(tdInner)

                                    Case 10
                                        InnerTdSetting(tdInner, "", "Right")
                                        lbl = New Label
                                        If dtBPref.Rows.Count > 0 Then
                                            If dtBPref.Rows(0).Item("BCM" + k.ToString() + "").ToString() <> "" Then
                                                If objCalb.TS1BLENDSUB(i - 1, k - 1) <> 0.0 Then
                                                    If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                                        lbl.Text = FormatNumber(objCalb.TS1BLENDSUB(i - 1, k - 1), 1)
                                                    Else
                                                        lbl.Text = FormatNumber(CDbl(objCalb.TS1BLENDSUB(i - 1, k - 1)) / dsPref.Tables(0).Rows(0).Item("CONVPA"), 0)
                                                    End If
                                                End If
                                            Else
                                                lbl.Text = ""
                                            End If
                                        Else
                                            lbl.Text = ""
                                        End If
                                        tdInner.ID = "BTS1SVal" + i.ToString() + "_" + cntblendmat.ToString()
                                        tdInner.Controls.Add(lbl)
                                        trInner.Controls.Add(tdInner)
                                    Case 11
                                        InnerTdSetting(tdInner, "", "Center")
                                        txt = New TextBox
                                        txt.CssClass = "SmallTextBox"
                                        txt.ID = "BTS1Val" + i.ToString() + "_" + cntblendmat.ToString()
                                        If dtBPref.Rows.Count > 0 Then
                                            If Not IsNumeric(dtBPref.Rows(0).Item("BCTS1VAL" + k.ToString())) Then
                                                txt.Text = ""
                                            Else
                                                If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                                    txt.Text = FormatNumber(dtBPref.Rows(0).Item("BCTS1VAL" + k.ToString()).ToString(), 1)
                                                Else
                                                    txt.Text = FormatNumber(CDbl(dtBPref.Rows(0).Item("BCTS1VAL" + k.ToString())) / dsPref.Tables(0).Rows(0).Item("CONVPA"), 0)
                                                End If
                                            End If
                                        Else
                                            txt.Text = ""
                                        End If
                                        txt.MaxLength = 6
                                        txt.Width = 70
                                        tdInner.ID = "BTS1PVal" + i.ToString() + "_" + cntblendmat.ToString()
                                        tdInner.Controls.Add(txt)
                                        trInner.Controls.Add(tdInner)

                                    Case 12
                                        InnerTdSetting(tdInner, "", "Right")
                                        lbl = New Label
                                        If dtBPref.Rows.Count > 0 Then
                                            If dtBPref.Rows(0).Item("BCM" + k.ToString() + "").ToString() <> "" Then
                                                If objCalb.TS2BLENDSUB(i - 1, k - 1) <> 0.0 Then
                                                    If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                                        lbl.Text = FormatNumber(objCalb.TS2BLENDSUB(i - 1, k - 1), 1)
                                                    Else
                                                        lbl.Text = FormatNumber(CDbl(objCalb.TS2BLENDSUB(i - 1, k - 1)) / dsPref.Tables(0).Rows(0).Item("CONVPA"), 0)
                                                    End If
                                                End If
                                            Else
                                                lbl.Text = ""
                                            End If
                                        Else
                                            lbl.Text = ""
                                        End If
                                        tdInner.ID = "BTS2SVal" + i.ToString() + "_" + cntblendmat.ToString()
                                        tdInner.Controls.Add(lbl)
                                        trInner.Controls.Add(tdInner)
                                    Case 13
                                        InnerTdSetting(tdInner, "", "Center")
                                        txt = New TextBox
                                        txt.CssClass = "SmallTextBox"
                                        txt.ID = "BTS2Val" + i.ToString() + "_" + cntblendmat.ToString()
                                        If dtBPref.Rows.Count > 0 Then
                                            If Not IsNumeric(dtBPref.Rows(0).Item("BCTS2VAL" + k.ToString())) Then
                                                txt.Text = ""
                                            Else
                                                If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                                    txt.Text = FormatNumber(dtBPref.Rows(0).Item("BCTS2VAL" + k.ToString()).ToString(), 1)
                                                Else
                                                    txt.Text = FormatNumber(CDbl(dtBPref.Rows(0).Item("BCTS2VAL" + k.ToString())) / dsPref.Tables(0).Rows(0).Item("CONVPA"), 0)
                                                End If
                                            End If
                                        Else
                                            txt.Text = ""
                                        End If
                                        txt.MaxLength = 6
                                        txt.Width = 70
                                        tdInner.ID = "BTS2PVal" + i.ToString() + "_" + cntblendmat.ToString()
                                        tdInner.Controls.Add(txt)
                                        trInner.Controls.Add(tdInner)

                                    Case 14
                                        InnerTdSetting(tdInner, "", "Right")
                                        lbl = New Label
                                        If dtBSug.Rows.Count > 0 Then
                                            If dtBSug.Rows(0).Item("SGS" + k.ToString() + "").ToString() = "" Then
                                                lbl.Text = "0.000"
                                            Else
                                                lbl.Text = FormatNumber(dtBSug.Rows(0).Item("SGS" + k.ToString() + "").ToString(), 3)
                                            End If
                                        Else
                                            lbl.Text = "0.000"
                                        End If
                                        If cntblendmat = 1 Then
                                            hid = New HiddenField
                                            hid.ID = "UnitId"
                                            hid.Value = dsPref.Tables(0).Rows(0).Item("UNITS").ToString()
                                        End If
                                        tdInner.Controls.Add(hid)
                                        tdInner.Controls.Add(lbl)
                                        trInner.Controls.Add(tdInner)
                                    Case 15
                                        InnerTdSetting(tdInner, "", "Center")
                                        txt = New TextBox
                                        txt.CssClass = "SmallTextBox"
                                        txt.Width = 70
                                        txt.ID = "BSG" + i.ToString() + "_" + cntblendmat.ToString()
                                        If dtBPref.Rows.Count > 0 Then
                                            If dtBPref.Rows(0).Item("BCSG" + k.ToString() + "").ToString() = "" Then
                                                txt.Text = "0.000"
                                            Else
                                                txt.Text = FormatNumber(dtBPref.Rows(0).Item("BCSG" + k.ToString() + "").ToString(), 3)
                                            End If
                                        Else
                                            txt.Text = "0.000"
                                        End If
                                        txt.MaxLength = 9
                                        tdInner.Controls.Add(txt)
                                        trInner.Controls.Add(tdInner)
                                    Case 16
                                        InnerTdSetting(tdInner, "", "Left")
                                        Link = New HyperLink
                                        hid = New HiddenField
                                        hid1 = New HiddenField
                                        ImgBut = New ImageButton
                                        ImgBut.ID = "imgBut" + i.ToString() + "_" + k.ToString()

                                        ImgDis = New ImageButton
                                        ImgDis.ID = "imgDis" + i.ToString() + "_" + k.ToString()
                                        ImgDis.Width = 20
                                        ImgDis.Height = 18
                                        ImgDis.Enabled = False
                                        ImgDis.ImageUrl = "~/Images/Notes Icon3.png"

                                        Link.ID = "hypSGradeDes" + i.ToString() + "_" + k.ToString()
                                        hid.ID = "hidSGradeId" + i.ToString() + "_" + k.ToString()
                                        hid1.ID = "hidSupId" + i.ToString() + "_" + k.ToString()
                                        Link.Width = 130
                                        Link.CssClass = "Link"
                                        If dtBPref.Rows.Count > 0 Then
                                            GetSGrades(Link, hid, "hidBlendid" + i.ToString() + "_" + k.ToString() + "", dtBPref.Rows(0).Item("BCGRADE" + k.ToString() + "").ToString(), hid1, CInt(dtBPref.Rows(0).Item("BCM" + k.ToString()).ToString()), ImgBut, ImgDis)
                                        Else
                                            GetSGrades(Link, hid, "hidBlendid" + i.ToString() + "_" + k.ToString() + "", "0", hid1, 0, ImgBut, ImgDis)
                                        End If
                                        ImgBut.Width = 20
                                        ImgBut.Height = 18
                                        ImgBut.ImageUrl = "~/Images/Notes Icon2.png"
                                        Dim Path As String = ""
                                        Path = "../PopUp/SupplierMaterials.aspx"
                                        ImgBut.Attributes.Add("onclick", "OpenTDataSheet('hidSupId" + i.ToString() + "_" + k.ToString() + "','hidBlendid" + i.ToString() + "_" + k.ToString() + "','" + Path + "','hidSGradeId" + i.ToString() + "_" + k.ToString() + "'); return false;")
                                        ImgBut.Style.Add("margin-bottom", "2px")
                                        ImgDis.Style.Add("margin-right", "2px")
                                        tdInner.ID = "SGradeVal" + i.ToString()
                                        tdInner.Controls.Add(hid)
                                        tdInner.Controls.Add(hid1)
                                        tdInner.Controls.Add(Link)
                                        trInner.Controls.Add(tdInner)
                                    Case 17
                                        InnerTdSetting(tdInner, "", "Left")
                                        tdInner.Controls.Add(ImgBut)
                                        tdInner.Controls.Add(ImgDis)
                                        trInner.Controls.Add(tdInner)
                                End Select
                            Next
                            If (k Mod 2 = 0) Then
                                If (i Mod 2 = 0) Then
                                    trInner.CssClass = "AlterNateColor1"
                                Else
                                    trInner.CssClass = "AlterNateColor2"
                                End If
                            Else
                                If (i Mod 2 = 0) Then
                                    trInner.CssClass = "AlterNateColor1"
                                Else
                                    trInner.CssClass = "AlterNateColor2"
                                End If
                            End If
                            trInner.Height = 30
                            If dsPref.Tables(0).Rows(0).Item("TYPEM" + i.ToString()).ToString() <> "0" Then

                            Else
                                trInner.Style.Add("display", "none")
                            End If
                            tblComparision.Controls.Add(trInner)
                        Next
                    End If
                End If
            Next


            'Total
            trInner = New TableRow
            For i = 1 To 17
                tdInner = New TableCell
                Select Case i
                    Case 1
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.Text = "  <span class='CalculatedFeilds'><b> Total </b></span><span style='color:Red;font-size:14px;'></span>"
                        tdInner.Attributes.Add("onmouseover", "Tip('Structure total values do not include waste, recycling, or extra-processing')")
                        tdInner.Attributes.Add("onmouseout", "UnTip('')")
                        trInner.Controls.Add(tdInner)
                    Case 4
                        InnerTdSetting(tdInner, "", "Right")
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(TotalThick, 3) + " </b> </span>"
                        tdInner.Style.Add("text-align", "center")
                        trInner.Controls.Add(tdInner)

                    Case 5
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.ColumnSpan = 1
                        tdInner.Text = FormatNumber(WeightPerT, 2)
                        tdInner.Text = "<b>" + tdInner.Text + "</b>"
                        tdInner.Style.Add("text-align", "center")
                        trInner.Controls.Add(tdInner)
                    Case 6
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.ColumnSpan = 2
                        tdInner.ID = "OTRTotal1"
                        If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                            tdInner.Text = FormatNumber(objCalb.TotalOTRBLEND, 3)
                        Else
                            tdInner.Text = FormatNumber(CDbl(objCalb.TotalOTRBLEND) * (CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                        End If
                        tdInner.Text = "<b>" + tdInner.Text + "</b>"
                        tdInner.Style.Add("text-align", "center")

                        trInner.Controls.Add(tdInner)
                    Case 8
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.ColumnSpan = 2
                        tdInner.ID = "WVTRTotal1"
                        If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                            tdInner.Text = FormatNumber(objCalb.TotalWVTRBLEND, 3)
                        Else
                            tdInner.Text = FormatNumber(CDbl(objCalb.TotalWVTRBLEND) * (CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                        End If
                        tdInner.Style.Add("text-align", "center")
                        tdInner.Text = "<b>" + tdInner.Text + "</b>"

                        trInner.Controls.Add(tdInner)

                    Case 9
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.ColumnSpan = 2
                        tdInner.ID = "TS1Total1"

                        If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                            tdInner.Text = FormatNumber(objCalb.TotalTS1BLEND, 1)
                        Else
                            tdInner.Text = FormatNumber(CDbl(objCalb.TotalTS1BLEND) / dsPref.Tables(0).Rows(0).Item("CONVPA"), 0)
                        End If
                        tdInner.Style.Add("text-align", "center")
                        tdInner.Text = "<b>" + tdInner.Text + "</b>"
                        trInner.Controls.Add(tdInner)

                    Case 10
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.ColumnSpan = 2
                        tdInner.ID = "TS2Total1"
                        If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                            tdInner.Text = FormatNumber(objCalb.TotalTS2BLEND, 1)
                        Else
                            tdInner.Text = FormatNumber(CDbl(objCalb.TotalTS2BLEND) / dsPref.Tables(0).Rows(0).Item("CONVPA"), 0)
                        End If
                        tdInner.Style.Add("text-align", "center")
                        tdInner.Text = "<b>" + tdInner.Text + "</b>"
                        trInner.Controls.Add(tdInner)

                    Case 11
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.ColumnSpan = 2
                        tdInner.ID = "Total" + i.ToString()
                        tdInner.Text = FormatNumber(TotalSG, 2)
                        tdInner.Style.Add("text-align", "center")
                        tdInner.Text = "<b>" + tdInner.Text + "</b>"
                        trInner.Controls.Add(tdInner)

                    Case 2, 3
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.ID = "Total" + i.ToString()
                        trInner.Controls.Add(tdInner)
                    Case 13
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.ColumnSpan = 2
                        trInner.Controls.Add(tdInner)

                End Select
            Next
            trInner.Height = 30
            trInner.CssClass = "AlterNateColor2"
            tblComparision.Controls.Add(trInner)

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetailsRH()
        ' Dim ds As New DataSet
        Dim objCalb As New StandBarrierGetData.Calculations
        Dim objGetData As New StandGetData.Selectdata
        Dim objUpdateData As New StandUpInsData.UpdateInsert
        Dim i As New Integer
        Dim j As New Integer
        Dim DWidth As String = String.Empty
        Dim trHeader As New TableRow
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim trHeader3 As New TableRow
        Dim trInner As New TableRow
        Dim tdHeader As TableCell
        Dim tdHeader1 As TableCell
        Dim tdHeader2 As TableCell
        Dim tdHeader3 As TableCell
        Dim lbl As New Label
        Dim label As New Label
        Dim hid As New HiddenField
        Dim hidGrade As New HiddenField
        Dim hidMat As New HiddenField
        Dim hidDes As New HiddenField
        Dim Link As New HyperLink
        Dim txt As New TextBox
        Dim tdInner As TableCell
        Dim chk As New CheckBox
        Dim radio As New RadioButton
        Dim OTR As String
        Dim WVTR As String
        Dim dsPref As New DataSet
        Dim dsSug As New DataSet
        Dim dsLabel As New DataSet
        Dim dsOTRWVTR As New DataSet
        Dim dsMatInput As New DataSet

        Dim dsMat1 As New DataSet

        Dim MAT1 As String
        Dim Grades1(9) As String
        Dim MATID1 As String = ""
        Dim DsPre1 As New DataSet
        Dim DsSug1 As New DataSet
        Dim m As String
        Try


            'For i = 0 To 9
            '    Grades1(i) = dsPref.Tables(0).Rows(0).Item("GRADE" + (i + 1).ToString() + "").ToString()
            '    If dsPref.Tables(0).Rows(0).Item("TYPEM" + (i + 1).ToString()).ToString() <> "0" Then
            '        MAT1(i) = dsPref.Tables(0).Rows(0).Item("TYPEM" + (i + 1).ToString() + "").ToString()
            '    Else
            ' 

            DsPre1 = objGetData.GetExtrusionDetailsPref(CaseId)
            DsSug1 = objGetData.GetExtrusionDetailsSugg(CaseId)

            For i = 0 To 9
                MAT1 = DsPre1.Tables(0).Rows(0).Item("M" + (i + 1).ToString() + "").ToString()
                MATID1 = MATID1 + "" + MAT1 + ","


                'dsMat1 = objGetData.GetCategory("-1", DsSug1.Tables(0).Rows(0).Item("MATS" + (i + 1).ToString() + "").ToString(), "")
            Next
            MATID1 = MATID1.Remove(MATID1.Length - 1)

            ' dsMat1 = objGetData.GetCategory("-1", " ", "")

            dsMat1 = objGetData.GetCategoryNew(MATID1)

            '

            dsOTRWVTR = CType(Session("OTRWVTR"), DataSet)
            'tblBarrier.Visible = False
            'tblHumidity.Visible = True
            dsLabel = objGetData.GetCaseDetails(CaseId.ToString())
            If dsLabel.Tables(0).Rows.Count > 0 Then
                CaseUpdateLable.Text = dsLabel.Tables(0).Rows(0).Item("SERVERDATE").ToString()
            End If
            dsPref = objGetData.GetExtrusionDetailsPref(CaseId)
            dsSug = objGetData.GetExtrusionDetailsSugg(CaseId)

            'Barrier
            If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() <> 1 Then
                OTR = (CDbl(dsPref.Tables(0).Rows(0).Item("OTRTEMP").ToString()) * (9 / 5)) + 32
                WVTR = (CDbl(dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString()) * (9 / 5)) + 32
            Else
                OTR = dsPref.Tables(0).Rows(0).Item("OTRTEMP").ToString()
                WVTR = dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString()
            End If


            Dim Grades(9) As String
            Dim Materials(9) As String
            Dim OtrVal(9) As String
            Dim WVTRVal(9) As String
            Dim ISADJTHICK(9) As String
            Dim MAT(9) As String
            For i = 0 To 9
                Grades(i) = dsPref.Tables(0).Rows(0).Item("GRADE" + (i + 1).ToString() + "").ToString()
                If dsPref.Tables(0).Rows(0).Item("TYPEM" + (i + 1).ToString()).ToString() <> "0" Then
                    MAT(i) = dsPref.Tables(0).Rows(0).Item("TYPEM" + (i + 1).ToString() + "").ToString()
                Else
                    MAT(i) = dsPref.Tables(0).Rows(0).Item("M" + (i + 1).ToString() + "").ToString()
                End If
                Materials(i) = dsSug.Tables(0).Rows(0).Item("MATS" + (i + 1).ToString() + "").ToString()
                OtrVal(i) = dsPref.Tables(0).Rows(0).Item("OTR" + (i + 1).ToString() + "").ToString()
                WVTRVal(i) = dsPref.Tables(0).Rows(0).Item("WVTR" + (i + 1).ToString() + "").ToString()
                'Thick(i) = CDbl(dsPref.Tables(0).Rows(0).Item("THICK" + (i + 1).ToString() + "").ToString() * dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString())
                ISADJTHICK(i) = dsSug.Tables(0).Rows(0).Item("ISADJTHICK" + (i + 1).ToString() + "").ToString()
            Next



            objCalb.BarrierPropCalculateRH(CaseId, dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString(), "100", _
            MAT, CDate(dsPref.Tables(0).Rows(0).Item("EFFDATEB").ToString()), Materials, Thick, ISADJTHICK, dsOTRWVTR, MTR)

            objCalb.RHCalculation(dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("ORH").ToString(), dsPref.Tables(0).Rows(0).Item("IRH").ToString(), MAT, Thick, ISADJTHICK, MTR, _
                                  CaseId, BMAT, dsPref.Tables(0).Rows(0).Item("EFFDATEB").ToString(), BMATERIALS, BOTR, BTHICK, BADJTHICK, BTHICKPER, dsOTRWVTR)

            objCalb.BarrierPropCalculateOTR(dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString(), objCalb.LayerRH, MAT, CDate(dsPref.Tables(0).Rows(0).Item("EFFDATEB").ToString()), Materials, Thick, ISADJTHICK, dsOTRWVTR)

            txtRHTemp.Text = FormatNumber(WVTR, 1)
            lblRHTemp.Text = "Temperature (" + dsPref.Tables(0).Rows(0).Item("TITLE20").ToString() + "):"

            For i = 1 To 10
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                tdHeader3 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i

                    Case 1
                        HeaderTdSetting(tdHeader, "60px", "Layers", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        HeaderTdSetting(tdHeader, "140px", "Primary Materials", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        HeaderTdSetting(tdHeader, "150px", "Grade", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        Title = "(" + dsPref.Tables(0).Rows(0).Item("TITLE1").ToString() + ")"
                        HeaderTdSetting(tdHeader, "90px", "Thickness", "1")
                        Header2TdSetting(tdHeader2, "", Title, "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case -1 '5
                        Title = "" + dsPref.Tables(0).Rows(0).Item("TITLE15").ToString() + ""
                        HeaderTdSetting(tdHeader, "100px", "MVTR ", "1")
                        '  Header2TdSetting(tdHeader2, "", "", "1")
                        If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                            tdHeader.Attributes.Add("Title", "MVTR at Standard Thickness (25.4 " + dsPref.Tables(0).Rows(0).Item("TITLE1").ToString() + ")")
                            Header2TdSetting(tdHeader2, "", "(gm/" + Title + "/day)", "1")
                        Else
                            tdHeader.Attributes.Add("Title", "MVTR at standard Thickness (1 " + dsPref.Tables(0).Rows(0).Item("TITLE1").ToString() + ")")
                            Header2TdSetting(tdHeader2, "", "(gm/100 " + Title + "/day)", "1")
                        End If

                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 6
                        HeaderTdSetting(tdHeader, "100px", "RH Position", "1")
                        Header2TdSetting(tdHeader2, "", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 7
                        HeaderTdSetting(tdHeader, "70px", "Position RH", "1")
                        Header2TdSetting(tdHeader2, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 8
                        HeaderTdSetting(tdHeader, "100px", "Layer RH", "1")
                        Header2TdSetting(tdHeader2, "", "(%)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 9
                        Title = "" + dsPref.Tables(0).Rows(0).Item("TITLE15").ToString() + ""
                        HeaderTdSetting(tdHeader, "90px", "OTR 100% O2", "1")
                        'Header2TdSetting(tdHeader2, "", "", "1")
                        If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                            Header2TdSetting(tdHeader2, "", "(cc/ " + Title + "/day)", "1")
                        Else
                            Header2TdSetting(tdHeader2, "", "(cc/100 " + Title + "/day)", "1")
                        End If
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 10
                        Title = "" + dsPref.Tables(0).Rows(0).Item("TITLE15").ToString() + ""
                        HeaderTdSetting(tdHeader, "90px", "OTR in Air", "1")
                        If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                            Header2TdSetting(tdHeader2, "", "(cc/ " + Title + "/day)", "1")
                        Else
                            Header2TdSetting(tdHeader2, "", "(cc/100 " + Title + "/day)", "1")
                        End If
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                End Select

            Next
            trHeader.Height = 30
            trHeader.Height = 30
            tblComparision1.Controls.Add(trHeader)
            tblComparision1.Controls.Add(trHeader2)


            Dim cntVal As Integer = 0
            'Inner
            For i = 1 To 21
                trInner = New TableRow
                For j = 1 To 10
                    tdInner = New TableCell

                    Select Case j
                        Case 1
                            'Layer
                            If (i Mod 2 = 0) Then
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "<b>" + (cntVal + 1).ToString() + "</b>"
                                cntVal = cntVal + 1
                            Else
                                If i = 1 Then
                                    tdInner.Text = "<b>OUTSIDE</b>"
                                ElseIf i = 21 Then
                                    tdInner.Text = "<b>INSIDE</b>"
                                End If
                                tdInner.Style.Add("font-size", "12px")
                                tdInner.Style.Add("font-family", "Optima")
                            End If


                            trInner.Controls.Add(tdInner)
                        Case 2
                            InnerTdSetting(tdInner, "", "Left")
                            If (i Mod 2 = 0) Then
                                lbl = New Label()
                                lbl.CssClass = "NormalLabel"
                                If dsPref.Tables(0).Rows(0).Item("TYPEM" + cntVal.ToString()).ToString() <> "0" Then
                                    GetMaterialDetails(CInt(dsPref.Tables(0).Rows(0).Item("TYPEM" + cntVal.ToString() + "").ToString()), lbl, dsMat1)
                                Else
                                    GetMaterialDetails(CInt(dsPref.Tables(0).Rows(0).Item("M" + cntVal.ToString() + "").ToString()), lbl, dsMat1)
                                End If
                                tdInner.Controls.Add(lbl)
                            Else

                            End If


                            trInner.Controls.Add(tdInner)
                        Case 3
                            InnerTdSetting(tdInner, "", "Left")

                            If (i Mod 2 = 0) Then
                                label = New Label
                                Link = New HyperLink
                                hid = New HiddenField
                                label.ID = "lblGradeDes" + cntVal.ToString()
                                Link.ID = "hypGradeDes" + cntVal.ToString()
                                hid.ID = "hidGradeId" + cntVal.ToString()
                                label.Width = 120
                                Link.CssClass = "Link"
                                GetGrades(hid, "hidMatid" + cntVal.ToString(), CInt(dsPref.Tables(0).Rows(0).Item("M" + cntVal.ToString() + "").ToString()), "SG" + cntVal.ToString(), label)
                                tdInner.ID = "GradeVal" + cntVal.ToString()
                                tdInner.Controls.Add(hid)
                                tdInner.Controls.Add(label)
                            End If

                            trInner.Controls.Add(tdInner)
                        Case 4
                            InnerTdSetting(tdInner, "", "Right")
                            If (i Mod 2 = 0) Then
                                lbl = New Label
                                lbl.Text = FormatNumber((Thick(cntVal - 1) / dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString()), 3)
                                lbl.CssClass = "NormalLabel"
                                tdInner.Controls.Add(lbl)
                            End If

                            trInner.Controls.Add(tdInner)

                        Case -1 '5
                            InnerTdSetting(tdInner, "", "Right")
                            If (i Mod 2 = 0) Then
                                lbl = New Label
                                If dsPref.Tables(0).Rows(0).Item("TYPEM" + cntVal.ToString()).ToString() <> "0" Then
                                    If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                        lbl.Text = FormatNumber(objCalb.WVTRHUMID(cntVal - 1), 3)
                                    Else
                                        lbl.Text = FormatNumber(CDbl(objCalb.WVTRHUMID(cntVal - 1)) * CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString() / 1000) * 100, 3)
                                    End If
                                ElseIf dsPref.Tables(0).Rows(0).Item("M" + cntVal.ToString() + "").ToString() <> 0 Then
                                    If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                        lbl.Text = FormatNumber(objCalb.WVTRHUMID(cntVal - 1), 3)
                                    Else
                                        lbl.Text = FormatNumber(CDbl(objCalb.WVTRHUMID(cntVal - 1)) * CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString() / 1000) * 100, 3)
                                    End If
                                Else
                                    lbl.Text = ""
                                End If
                                lbl.CssClass = "NormalLabel"
                                tdInner.Controls.Add(lbl)

                            End If

                            trInner.Controls.Add(tdInner)
                        Case 6
                            InnerTdSetting(tdInner, "", "Right")
                            If (i Mod 2 = 0) Then
                            Else
                                lbl = New Label
                                lbl.Text = cntVal
                                lbl.CssClass = "NormalLabel"
                                tdInner.Controls.Add(lbl)
                            End If
                            If i = 1 Then
                                hid = New HiddenField
                                hid.ID = "UnitId"
                                hid.Value = dsPref.Tables(0).Rows(0).Item("UNITS").ToString()
                            End If
                            tdInner.Controls.Add(hid)

                            trInner.Controls.Add(tdInner)
                        Case 7
                            InnerTdSetting(tdInner, "", "Right")
                            If (i Mod 2 = 0) Then

                            Else
                                If i = 1 Or i = 21 Then
                                    txt = New TextBox
                                    txt.CssClass = "SmallTextBox"
                                    txt.ID = "OIRH" + i.ToString()
                                    If i = 1 Then
                                        txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("ORH").ToString(), 1)
                                    ElseIf i = 21 Then
                                        txt.Text = FormatNumber(dsPref.Tables(0).Rows(0).Item("IRH").ToString(), 1)
                                    End If
                                    txt.MaxLength = 6
                                    tdInner.Controls.Add(txt)
                                Else
                                    lbl = New Label
                                    lbl.Text = FormatNumber(objCalb.RH(cntVal - 1), 1)
                                    lbl.CssClass = "NormalLabel"
                                    tdInner.Controls.Add(lbl)
                                End If

                            End If
                            trInner.Controls.Add(tdInner)
                        Case 8
                            InnerTdSetting(tdInner, "", "Right")
                            If (i Mod 2 = 0) Then
                                lbl = New Label
                                lbl.Text = FormatNumber(objCalb.LayerRH(cntVal - 1), 1)
                                lbl.CssClass = "NormalLabel"
                                tdInner.Controls.Add(lbl)
                            End If
                            trInner.Controls.Add(tdInner)
                        Case 9
                            InnerTdSetting(tdInner, "", "Right")
                            If (i Mod 2 = 0) Then
                                lbl = New Label
                                If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                    lbl.Text = FormatNumber(objCalb.OTRH(cntVal - 1), 3)
                                Else
                                    lbl.Text = FormatNumber(CDbl(objCalb.OTRH(cntVal - 1)) * CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString() / 1000) * 100, 3)
                                End If
                                lbl.CssClass = "NormalLabel"
                                tdInner.Controls.Add(lbl)
                            End If
                            trInner.Controls.Add(tdInner)
                        Case 10
                            InnerTdSetting(tdInner, "", "Right")
                            If (i Mod 2 = 0) Then
                                lbl = New Label
                                If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                    lbl.Text = FormatNumber(objCalb.OTRAIR(cntVal - 1), 3)
                                Else
                                    lbl.Text = FormatNumber(CDbl(objCalb.OTRAIR(cntVal - 1)) * CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString() / 1000) * 100, 3)
                                End If
                                lbl.CssClass = "NormalLabel"
                                tdInner.Controls.Add(lbl)
                            End If
                            trInner.Controls.Add(tdInner)

                    End Select
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                trInner.Height = 20
                tblComparision1.Controls.Add(trInner)
            Next


            'Total
            trInner = New TableRow
            For i = 1 To 10
                tdInner = New TableCell
                Select Case i
                    Case 1
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.Text = "  <span class='CalculatedFeilds'><b> Total </b></span><span style='color:Red;font-size:14px;'></span>"
                        tdInner.Attributes.Add("onmouseover", "Tip('Structure total values do not include waste, recycling, or extra-processing')")
                        tdInner.Attributes.Add("onmouseout", "UnTip('')")
                        trInner.Controls.Add(tdInner)
                    Case 4
                        InnerTdSetting(tdInner, "", "Right")
                        tdInner.Text = "<span class='CalculatedFeilds'><b>" + FormatNumber(TotalThick, 3) + " </b> </span>"
                        tdInner.Style.Add("text-align", "center")
                        trInner.Controls.Add(tdInner)
                    Case -1 '5
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.ColumnSpan = 1
                        tdInner.ID = "Total" + i.ToString()
                        If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                            tdInner.Text = FormatNumber(objCalb.TotalWVTRB, 3)
                        Else
                            tdInner.Text = FormatNumber(CDbl(objCalb.TotalWVTRB) * (CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                        End If
                        tdInner.Style.Add("text-align", "center")
                        tdInner.Text = "<b>" + tdInner.Text + "</b>"

                        trInner.Controls.Add(tdInner)

                    Case 9
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.ColumnSpan = 1
                        tdInner.ID = "Total" + i.ToString()
                        If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                            tdInner.Text = FormatNumber(objCalb.TotalOTRH, 3)
                        Else
                            tdInner.Text = FormatNumber(CDbl(objCalb.TotalOTRH) * (CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                        End If
                        tdInner.Text = "<b>" + tdInner.Text + "</b>"
                        tdInner.Style.Add("text-align", "center")

                        trInner.Controls.Add(tdInner)
                    Case 10
                        Dim OTRAir As Double
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.ColumnSpan = 1
                        tdInner.ID = "Total" + i.ToString()
                        If dsPref.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                            OTRAir = objCalb.TotalOTRH
                        Else
                            OTRAir = CDbl(objCalb.TotalOTRH) * (CDbl(dsPref.Tables(0).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100
                        End If
                        tdInner.Text = FormatNumber(((OTRAir * 20.95) / 100), 3)
                        tdInner.Text = "<b>" + tdInner.Text + "</b>"
                        tdInner.Style.Add("text-align", "center")

                        trInner.Controls.Add(tdInner)
                    Case 2, 3, 6, 7, 8
                        InnerTdSetting(tdInner, "", "Left")
                        tdInner.ID = "Total" + i.ToString()
                        trInner.Controls.Add(tdInner)

                End Select
            Next
            trInner.Height = 20
            trInner.CssClass = "AlterNateColor1"
            tblComparision1.Controls.Add(trInner)

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetBlendMaterialDetailsNew(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal MatId As Integer, ByVal linkGrade As String, ByVal hidGrade As String, ByVal SG As String, ByVal hidDes As HiddenField, ByVal hidMat As HiddenField, ByVal labelGrade As String, ByVal i As Integer, ByVal dsGBM As DataSet)
        Dim Ds As New DataSet
        Dim DsMat As New DataSet
        Dim ObjGetdata As New StandGetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty

        Dim dvGBM As New DataView
        Dim dtGBM As New DataTable
        Try
            If MatId <> "0" Then


                'Ds = ObjGetdata.GetCategory(MatId, "", "")

                dvGBM = dsGBM.Tables(0).DefaultView
                dvGBM.RowFilter = "MATID= " + MatId.ToString()
                dtGBM = dvGBM.ToTable()





                LinkMat.Text = dtGBM.Rows(0).Item("MATID").ToString() + ":" + dtGBM.Rows(0).Item("CATEGORY").ToString() '
                LinkMat.ToolTip = dtGBM.Rows(0).Item("CATEGORY").ToString() '
                LinkMat.Attributes.Add("text-decoration", "none")
                hidDes.Value = dtGBM.Rows(0).Item("CATEGORY").ToString() '
                hid.Value = MatId.ToString()
                hidMat.Value = dtGBM.Rows(0).Item("CATEGORY").ToString() '
                If (hidMat.Value).ToUpper() = "RESIN" Then
                    DsMat = ObjGetdata.GetResinMaterialSubLayer(MatId, "", "Resin", "BLEND")
                End If
            Else
                LinkMat.Text = "Nothing"
                LinkMat.ToolTip = "Nothing"
                LinkMat.Attributes.Add("text-decoration", "none")
                hidDes.Value = "Nothing"
                hid.Value = 0
                hidMat.Value = "Nothing"
            End If
            Session("MaterialGrade") = DsMat
            'Path = "../PopUp/GetMatPopUpGrade.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "&GradeDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + linkGrade + "&GradeId=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hidGrade + "&SG=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + SG + "&MatDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hidDes.ClientID + "&MatGrp=" + LinkMat.Text + "&Grp=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hidMat.ClientID + " &LinkId=N"
            Path = "../PopUp/GetResinBlendMatPopUp.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_tabSDesigner_tabpnl1_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_tabSDesigner_tabpnl1_" + hid.ClientID + "&GradeDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_tabSDesigner_tabpnl1_" + linkGrade + "&GradeId=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hidGrade + "&SG=" + ctlContentPlaceHolder.ClientID.ToString() + "_tabSDesigner_tabpnl1_" + SG + "&MatDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_tabSDesigner_tabpnl1_" + hidDes.ClientID + "&MatGrp=" + LinkMat.Text + "&Grp=RESIN&LinkId=N&Mcnt=" + i.ToString() + "&MBlend=BLEND"
            LinkMat.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"
        Catch ex As Exception
            ErrorLable.Text = "Error:GetBlendMaterialDetailsNew:" + ex.Message.ToString() + ""
        End Try
    End Sub


    Protected Sub GetMaterialDetails(ByVal MatId As Integer, ByVal lbl As Label, ByVal dsMat1 As DataSet)
        Dim Ds As New DataSet
        Dim DsMat As New DataSet
        Dim ObjGetdata As New StandGetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim linkText As String = ""

        Dim dvMat1 As New DataView
        Dim dtMat1 As New DataTable

        Try
            If MatId <> "0" Then

                ' Ds = ObjGetdata.GetCategory(MatId, "", "")


                dvMat1 = dsMat1.Tables(0).DefaultView
                dvMat1.RowFilter = "MATID= " + MatId.ToString()
                dtMat1 = dvMat1.ToTable()


                lbl.Text = dtMat1.Rows(0).Item("CATEGORY").ToString() '
                '  lbl.Text = Ds.Tables(0).Rows(0).Item("CATEGORY").ToString() '
                'Showing Blend name on link
                If MatId > 500 And MatId < 506 Then
                    linkText = dtMat1.Rows(0).Item("MATID").ToString() + ":" + dtMat1.Rows(0).Item("MATERIAL").ToString() '
                    'linkText = Ds.Tables(0).Rows(0).Item("MATID").ToString() + ":" + Ds.Tables(0).Rows(0).Item("MATERIAL").ToString() '

                Else
                    linkText = dtMat1.Rows(0).Item("MATID").ToString() + ":" + dtMat1.Rows(0).Item("CATEGORY").ToString() '
                    'linkText = Ds.Tables(0).Rows(0).Item("MATID").ToString() + ":" + Ds.Tables(0).Rows(0).Item("CATEGORY").ToString() '

                End If
                lbl.ToolTip = dtMat1.Rows(0).Item("CATEGORY").ToString()
                ' lbl.ToolTip = Ds.Tables(0).Rows(0).Item("CATEGORY").ToString()

                lbl.Attributes.Add("text-decoration", "none")

                If (lbl.Text).ToUpper() = "RESIN" Then
                    DsMat = ObjGetdata.GetResinMaterialbyGroups(MatId, "", "Resin")
                ElseIf (lbl.Text).ToUpper().Trim() = "FILM" Then
                    DsMat = ObjGetdata.GetFilmMaterialbyGroups(MatId, "", "Film")
                ElseIf (lbl.Text).ToUpper().Trim() = "ADHESIVE" Then
                    DsMat = ObjGetdata.GetAdhesiveMaterialNew(MatId, "", "Adhesive")
                ElseIf (lbl.Text).ToUpper().Trim() = "ALUMINUM" Then
                    DsMat = ObjGetdata.GetAluminMaterialNew(MatId, "", "Aluminum")
                ElseIf (lbl.Text).ToUpper().Trim() = "COATING" Then
                    DsMat = ObjGetdata.GetCoatingMaterialbyGroups(MatId, "", "Coating")
                ElseIf (lbl.Text).ToUpper().Trim() = "BASE FIBER" Then
                    DsMat = ObjGetdata.GetBaseFMaterialbyGroups(MatId, "", "Base Fiber")
                ElseIf (lbl.Text).ToUpper().Trim() = "CONCENTRATE" Then
                    DsMat = ObjGetdata.GetConcentrateMaterialbyGroups(MatId, "", "Concentrate")
                ElseIf (lbl.Text).ToUpper().Trim() = "GLASS" Then
                    DsMat = ObjGetdata.GetGlassMaterialbyGroups(MatId, "", "Glass")
                ElseIf (lbl.Text).ToUpper().Trim() = "INK" Then
                    DsMat = ObjGetdata.GetInkMaterialbyGroups(MatId, "", "Ink")
                ElseIf (lbl.Text).ToUpper().Trim() = "NON-WOVEN" Then
                    DsMat = ObjGetdata.GetNonWMaterialbyGroups(MatId, "", "Non-Woven")
                ElseIf (lbl.Text).ToUpper().Trim() = "PAPER" Then
                    DsMat = ObjGetdata.GetPaperMaterialbyGroups(MatId, "", "Paper")
                ElseIf (lbl.Text).ToUpper().Trim() = "PAPERBOARD" Then
                    DsMat = ObjGetdata.GetPaperBMaterialbyGroups(MatId, "", "Paperboard")
                ElseIf (lbl.Text).ToUpper().Trim() = "SHEET" Then
                    DsMat = ObjGetdata.GetSheetMaterialbyGroups(MatId, "", "Sheet")
                ElseIf (lbl.Text).ToUpper().Trim() = "STEEL" Then
                    DsMat = ObjGetdata.GetSteelMaterialbyGroups(MatId, "", "Steel")
                End If

            Else
                lbl.Text = "Nothing"
                lbl.ToolTip = "Nothing"
                lbl.Attributes.Add("text-decoration", "none")
                linkText = MatId.ToString() + ":" + "Nothing"
            End If
            Session("MaterialGrade") = DsMat
            lbl.Text = linkText
        Catch ex As Exception
            ErrorLable.Text = "Error:GetMaterialDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetMaterialDetailsNew(ByRef LinkMat As HyperLink, ByVal hid As HiddenField, ByVal MatId As Integer, ByVal linkGrade As String, ByVal hidGrade As String, ByVal SG As String, ByVal hidDes As HiddenField, ByVal hidMat As HiddenField, ByVal labelGrade As String, ByVal dsGMat1 As DataSet)
        Dim Ds As New DataSet
        Dim DsMat As New DataSet
        Dim ObjGetdata As New StandGetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim linkText As String = ""

        Dim dvGMat1 As New DataView
        Dim dtGMat1 As New DataTable

        Try
            If MatId <> "0" Then
                'Ds = ObjGetdata.GetCategory(MatId, "", "")


                dvGMat1 = dsGMat1.Tables(0).DefaultView
                dvGMat1.RowFilter = "MATID= " + MatId.ToString()
                dtGMat1 = dvGMat1.ToTable()

                LinkMat.Text = dtGMat1.Rows(0).Item("CATEGORY").ToString()
                'Showing Blend name on link
                If MatId > 500 And MatId < 506 Then
                    linkText = dtGMat1.Rows(0).Item("MATID").ToString() + ":" + dtGMat1.Rows(0).Item("MATERIAL").ToString()
                Else
                    linkText = dtGMat1.Rows(0).Item("MATID").ToString() + ":" + dtGMat1.Rows(0).Item("CATEGORY").ToString()
                End If
                LinkMat.ToolTip = dtGMat1.Rows(0).Item("CATEGORY").ToString()
                LinkMat.Attributes.Add("text-decoration", "none")

                hidDes.Value = dtGMat1.Rows(0).Item("CATEGORY").ToString()
                hid.Value = MatId.ToString()
                hidMat.Value = dtGMat1.Rows(0).Item("CATEGORY").ToString()

                If (LinkMat.Text).ToUpper() = "RESIN" Then
                    DsMat = ObjGetdata.GetResinMaterialbyGroups(MatId, "", "Resin")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "FILM" Then
                    DsMat = ObjGetdata.GetFilmMaterialbyGroups(MatId, "", "Film")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "ADHESIVE" Then
                    DsMat = ObjGetdata.GetAdhesiveMaterialNew(MatId, "", "Adhesive")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "ALUMINUM" Then
                    DsMat = ObjGetdata.GetAluminMaterialNew(MatId, "", "Aluminum")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "COATING" Then
                    DsMat = ObjGetdata.GetCoatingMaterialbyGroups(MatId, "", "Coating")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "BASE FIBER" Then
                    DsMat = ObjGetdata.GetBaseFMaterialbyGroups(MatId, "", "Base Fiber")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "CONCENTRATE" Then
                    DsMat = ObjGetdata.GetConcentrateMaterialbyGroups(MatId, "", "Concentrate")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "GLASS" Then
                    DsMat = ObjGetdata.GetGlassMaterialbyGroups(MatId, "", "Glass")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "INK" Then
                    DsMat = ObjGetdata.GetInkMaterialbyGroups(MatId, "", "Ink")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "NON-WOVEN" Then
                    DsMat = ObjGetdata.GetNonWMaterialbyGroups(MatId, "", "Non-Woven")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "PAPER" Then
                    DsMat = ObjGetdata.GetPaperMaterialbyGroups(MatId, "", "Paper")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "PAPERBOARD" Then
                    DsMat = ObjGetdata.GetPaperBMaterialbyGroups(MatId, "", "Paperboard")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "SHEET" Then
                    DsMat = ObjGetdata.GetSheetMaterialbyGroups(MatId, "", "Sheet")
                ElseIf (LinkMat.Text).ToUpper().Trim() = "STEEL" Then
                    DsMat = ObjGetdata.GetSteelMaterialbyGroups(MatId, "", "Steel")
                End If

            Else
                LinkMat.Text = "Nothing"
                LinkMat.ToolTip = "Nothing"
                LinkMat.Attributes.Add("text-decoration", "none")
                hidDes.Value = "Nothing"
                hid.Value = 0
                hidMat.Value = "Nothing"

                linkText = MatId.ToString() + ":" + "Nothing"
            End If
            Session("MaterialGrade") = DsMat
            Path = "../PopUp/GetMatPopUpGrade.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_tabSDesigner_tabpnl1_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_tabSDesigner_tabpnl1_" + hid.ClientID + "&GradeDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_tabSDesigner_tabpnl1_" + linkGrade + "&GradeId=" + ctlContentPlaceHolder.ClientID.ToString() + "_tabSDesigner_tabpnl1_" + hidGrade + "&SG=" + ctlContentPlaceHolder.ClientID.ToString() + "_tabSDesigner_tabpnl1_" + SG + "&MatDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_tabSDesigner_tabpnl1_" + hidDes.ClientID + "&MatGrp=" + LinkMat.Text + "&Grp=" + LinkMat.Text + " &LinkId=N"

            'Path = "../PopUp/GetMatPopUpGrade.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + LinkMat.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hid.ClientID + "&GradeDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + linkGrade + "&GradeId=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hidGrade + "&SG=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + SG + "&MatDes=" + ctlContentPlaceHolder.ClientID.ToString() + "_" + hidDes.ClientID + "&MatGrp=" + LinkMat.Text + "&Grp=" + LinkMat.Text + " &LinkId=N"
            LinkMat.NavigateUrl = "javascript:ShowPopWindow('" + Path + "')"
            LinkMat.Text = linkText
        Catch ex As Exception
            ErrorLable.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetGrades(ByVal hid As HiddenField, ByVal MatId As String, ByVal GradeId As Integer, ByVal SG As String, ByVal GradeLabel As Label)
        Dim Ds As New DataSet
        Dim DsMat As New DataSet
        Dim ObjGetdata As New StandGetData.Selectdata()
        Dim Path As String = String.Empty
        Dim MaterialId As String = String.Empty
        Dim dsGrade As New DataSet()
        Dim strGradeCol As String = ""
        Dim dvMat As New DataView
        Dim dtMat As New DataTable

       


        Try

            dsGrade = CType(Session("MaterialGrade"), DataSet)

            dvMat = dsGrade.Tables(0).DefaultView

            dvMat.RowFilter = "MATID=" + GradeId.ToString()
            dtMat = dvMat.ToTable()
            Dim GradeV() As String
            Dim GradeValue As String = String.Empty
            Dim MouseoverV() As String
            Dim MouseoverV1() As String
            Dim MouseoverValue As String = String.Empty
            Dim i As Integer = 0
            GradeV = Regex.Split(dtMat.Rows(0).Item("GRADECOL").Trim().ToString(), ",")

            If GradeV.Length > 0 Then
                'Dim strGradeCol As String = ""
                For i = 0 To GradeV.Length - 1
                    If i = 0 Then
                        strGradeCol = dtMat.Rows(0).Item("" + GradeV(i).Trim() + "").ToString()
                    Else
                        strGradeCol = strGradeCol + ", " + dtMat.Rows(0).Item("" + GradeV(i).Trim() + "").ToString()
                    End If
                Next
                GradeLabel.Text = strGradeCol
            End If
            MouseoverV = Regex.Split(dtMat.Rows(0).Item("MOUSEOVERCOL").Trim().ToString(), ",")
            MouseoverV1 = Regex.Split(dtMat.Rows(0).Item("MOUSEOVERCOL").Trim().ToString(), ",")
            If MouseoverV.Length > 0 Then
                Dim strGradeMO As String = ""
                For i = 0 To MouseoverV.Length - 1
                    If MouseoverV(i).ToString() = "DESCRIPTION" Then
                        MouseoverV1(i) = "Material Description"
                    ElseIf MouseoverV(i).ToString() = "TECHDESCRIPTION" Then
                        MouseoverV1(i) = "Technical Description"
                    ElseIf MouseoverV(i).ToString() = "SOLVENT" Then
                        MouseoverV1(i) = "Solvent"
                    ElseIf MouseoverV(i).ToString() = "DILUENT" Then
                        MouseoverV1(i) = "Diluent"
                    ElseIf MouseoverV(i).ToString() = "FEATURE" Then
                        MouseoverV1(i) = "Feature"
                    ElseIf MouseoverV(i).ToString() = "COATING" Then
                        MouseoverV1(i) = "Coating"
                    ElseIf MouseoverV(i).ToString() = "THICKNESS" Then
                        MouseoverV1(i) = "Thickness"
                    ElseIf MouseoverV(i).ToString() = "PARTICLESIZE" Then
                        MouseoverV1(i) = "Particle Size"
                    ElseIf MouseoverV(i).ToString() = "CURING" Then
                        MouseoverV1(i) = "Curing"
                    ElseIf MouseoverV(i).ToString() = "SUBSTRATE" Then
                        MouseoverV1(i) = "Substrate"
                    ElseIf MouseoverV(i).ToString() = "SUBSTRATES" Then
                        MouseoverV1(i) = "Substrates"
                    ElseIf MouseoverV(i).ToString() = "TYPE" Then
                        MouseoverV1(i) = "Type"
                    ElseIf MouseoverV(i).ToString() = "TYPEDESC" Then
                        MouseoverV1(i) = "Type Description"
                    ElseIf MouseoverV(i).ToString() = "MATERIAL" Then
                        MouseoverV1(i) = "Material Description"
                    ElseIf MouseoverV(i).ToString() = "PROCESS" Then
                        MouseoverV1(i) = "Process"
                    ElseIf MouseoverV(i).ToString() = "RESINFAMILY" Then
                        MouseoverV1(i) = "Resin Family"
                    ElseIf MouseoverV(i).ToString() = "ALLOY" Then
                        MouseoverV1(i) = "Alloy"
                    ElseIf MouseoverV(i).ToString() = "TEMPER" Then
                        MouseoverV1(i) = "Temper"
                    ElseIf MouseoverV(i).ToString() = "ALLOY" Then
                        MouseoverV1(i) = "Alloy"
                    ElseIf MouseoverV(i).ToString() = "TECHDESC" Then
                        MouseoverV1(i) = "Technical Description"
                    ElseIf MouseoverV(i).ToString() = "FUNCTION" Then
                        MouseoverV1(i) = "Function"
                    ElseIf MouseoverV(i).ToString() = "RECYCLE" Then
                        MouseoverV1(i) = "Recycle"
                    ElseIf MouseoverV(i).ToString() = "MATDES" Then
                        MouseoverV1(i) = "Material Category"
                    ElseIf MouseoverV(i).ToString() = "BARRIER" Then
                        MouseoverV1(i) = "Barrier"
                    ElseIf MouseoverV(i).ToString() = "DENSITY" Then
                        MouseoverV1(i) = "Density"
                    ElseIf MouseoverV(i).ToString() = "POLYMERSTRUCT" Then
                        MouseoverV1(i) = "Polymer Structure"
                    ElseIf MouseoverV(i).ToString() = "POLYMERDESC" Then
                        MouseoverV1(i) = "Polymer Description"
                    ElseIf MouseoverV(i).ToString() = "VISC" Then
                        MouseoverV1(i) = "Viscosity"
                    ElseIf MouseoverV(i).ToString() = "MELTFRATE" Then
                        MouseoverV1(i) = "Melt Flow Rate"
                    ElseIf MouseoverV(i).ToString() = "MELTINDEX" Then
                        MouseoverV1(i) = "Melt Index"
                    ElseIf MouseoverV(i).ToString() = "ETHYLENE" Then
                        MouseoverV1(i) = "% Ethylene"
                    ElseIf MouseoverV(i).ToString() = "TYPE" Then
                        MouseoverV1(i) = "Type"
                    ElseIf MouseoverV(i).ToString() = "ANHYDRIDEMOD" Then
                        MouseoverV1(i) = "Anhydride Modification"
                    ElseIf MouseoverV(i).ToString() = "WATERACTIVITY" Then
                        MouseoverV1(i) = "Water Activity "
                    ElseIf MouseoverV(i).ToString() = "ADHESIVE" Then
                        MouseoverV1(i) = "Adhesive"
                    ElseIf MouseoverV(i).ToString() = "FORMULATION" Then
                        MouseoverV1(i) = "Formulation"
                    ElseIf MouseoverV(i).ToString() = "REACTIVE" Then
                        MouseoverV1(i) = "Reactive"
                    ElseIf MouseoverV(i).ToString() = "SUBLAYERS" Then
                        MouseoverV1(i) = "Substrate Layers"
                    ElseIf MouseoverV(i).ToString() = "PRESIDE" Then
                        MouseoverV1(i) = "Pretreat Sides"
                    ElseIf MouseoverV(i).ToString() = "PRETYPE" Then
                        MouseoverV1(i) = "Pretreat Type"
                    ElseIf MouseoverV(i).ToString() = "MODIFIERS" Then
                        MouseoverV1(i) = "Modifiers"
                    ElseIf MouseoverV(i).ToString() = "OUTCOATING" Then
                        MouseoverV1(i) = "Outside Coating"
                    ElseIf MouseoverV(i).ToString() = "PRODOUTCOATING" Then
                        MouseoverV1(i) = "Product Side Coating"
                    ElseIf MouseoverV(i).ToString() = "OXYGENBARR" Then
                        MouseoverV1(i) = "Oxygen Barrier"
                    ElseIf MouseoverV(i).ToString() = "MOISTUREBARR" Then
                        MouseoverV1(i) = "Moisture Barrier"
                    ElseIf MouseoverV(i).ToString() = "APPLICATION" Then
                        MouseoverV1(i) = "Application"
                    ElseIf MouseoverV(i).ToString() = "DENSITY" Then
                        MouseoverV1(i) = "Density"
                    End If
                    If i = 0 Then
                        strGradeMO = "<b>" + MouseoverV1(i) + "</b>=" + dtMat.Rows(0).Item("" + MouseoverV(i).Trim() + "").ToString() + "</br>"
                    Else
                        strGradeMO = strGradeMO + "<b>" + MouseoverV1(i) + "</b>=" + dtMat.Rows(0).Item("" + MouseoverV(i).Trim() + "").ToString() + "</br>"
                    End If
                    GradeLabel.Attributes.Add("onmouseover", "Tip('" + strGradeMO + "')")
                    GradeLabel.Attributes.Add("onmouseout", "UnTip('')")
                Next
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetSGrades(ByRef LinkGrade As HyperLink, ByVal hid As HiddenField, ByVal MatId As String, ByVal GradeId As String, ByVal hid1 As HiddenField, ByVal materialIDD As Integer, ByVal img As ImageButton, ByVal imgd As ImageButton)
        Dim Ds As New DataSet
        Dim DsSheet As New DataSet
        Dim ObjGetdata As New StandGetData.Selectdata()
        Dim Path As String = String.Empty
        Dim SGradeId As String = String.Empty
        Dim MaterialId As String = String.Empty

        Dim dvGrade As New DataView
        Dim dtGrade As New DataTable

        Try
            If GradeId = "0" Then
                LinkGrade.Text = "Nothing Selected"
                LinkGrade.ToolTip = "View Supplier Grade"
                hid.Value = "0"
                hid1.Value = "0"
                img.Style.Add("display", "none")
                imgd.Style.Add("display", "none")
            ElseIf GradeId <> "0" Then
                Ds = ObjGetdata.GetMatSupplierGrade(materialIDD, "", GradeId)


                'dvGrade = dsGrade.Tables(0).DefaultView
                'dvGrade.RowFilter = "MATID= " + MatId.ToString()
                'dtGrade = dvGrade.ToTable()


                'Getting Tec Data
                DsSheet = ObjGetdata.GetSupplierMatGradeCheck(materialIDD, Ds.Tables(0).Rows(0).Item("SUPPLIERID").ToString(), GradeId)
                If DsSheet.Tables(0).Rows.Count > 0 Then
                    img.Style.Add("display", "inline")
                    imgd.Style.Add("display", "none")
                Else
                    If Ds.Tables(0).Rows(0).Item("DESCRIPTION").ToString() = "" Then
                        img.Style.Add("display", "none")
                        imgd.Style.Add("display", "inline")
                        imgd.ToolTip = "Technical data sheet not available now."
                        imgd.Enabled = False
                    Else
                        img.Style.Add("display", "inline")
                        imgd.Style.Add("display", "none")
                    End If
                End If
                LinkGrade.Text = Ds.Tables(0).Rows(0).Item("NAME").ToString() + " " + Ds.Tables(0).Rows(0).Item("GRADENAME").ToString()
                LinkGrade.ToolTip = Ds.Tables(0).Rows(0).Item("NAME").ToString() + " " + Ds.Tables(0).Rows(0).Item("GRADENAME").ToString()
                hid.Value = GradeId
                hid1.Value = Ds.Tables(0).Rows(0).Item("SUPPLIERID").ToString()
            End If
            'LinkGrade.Width = 130
            LinkGrade.Attributes.Add("text-decoration", "none")
            MaterialId = ctlContentPlaceHolder.ClientID.ToString() + "_tabSDesigner_tabpnl1_" + MatId
            Path = "../PopUp/GetSupplier.aspx?Des=" + ctlContentPlaceHolder.ClientID.ToString() + "_tabSDesigner_tabpnl1_" + LinkGrade.ClientID + "&Id=" + ctlContentPlaceHolder.ClientID.ToString() + "_tabSDesigner_tabpnl1_" + hid.ClientID + "&SupID=" + ctlContentPlaceHolder.ClientID.ToString() + "_tabSDesigner_tabpnl1_" + hid1.ClientID + "&ImgId=" + ctlContentPlaceHolder.ClientID.ToString() + "_tabSDesigner_tabpnl1_" + img.ClientID + "&ImgDis=" + ctlContentPlaceHolder.ClientID.ToString() + "_tabSDesigner_tabpnl1_" + imgd.ClientID + ""
            LinkGrade.NavigateUrl = "javascript:ShowGradePopWindow('" + Path + "','" + MaterialId + "')"
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
            Td.CssClass = "TdHeadingNew"
            Td.Height = 20
            Td.Font.Size = 9
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
            Td.CssClass = "TdHeadingNew"
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
            Td.Style.Add("font-family", "Optima")
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

    Protected Sub UpdatePage()

        Dim Material(9) As String
        Dim Thickness(9) As String
        Dim OTR(9) As String
        Dim WVTR(9) As String
        Dim TS1(9) As String
        Dim TS2(9) As String
        Dim Grade(9) As String
        Dim SGP(9) As String
        Dim OTRV(9) As String
        Dim WVTRV(9) As String
        Dim i As New Integer
        Dim MaterialDis(2) As String
        Dim WeightDis(2) As String
        Dim ErgyDis(2) As String
        Dim CO2Dis(2) As String
        Dim WaterDis(2) As String
        Dim ObjUpIns As New StandUpInsData.UpdateInsert
        Dim DisCount As New Integer
        Dim IsSg As Boolean
        Dim IsThick As Boolean
        Dim IsOTR As Boolean
        Dim IsWVTR As Boolean
        Dim IsGrade As Boolean
        Dim ProductFormt As New DataSet
        Dim ObjGetData As New StandGetData.Selectdata()
        'ProductFormt = ObjGetData.GetProductFromatIn(CaseId)
        Dim obj As New CryptoHelper
        Dim DsMat As New DataSet

        'Collecting Data for Blend Sub Layers
        Dim BMaterial(9) As String
        Dim BThickness(9) As String
        Dim BOTR(9) As String
        Dim BWVTR(9) As String
        Dim BTS1(9) As String
        Dim BTS2(9) As String
        Dim BSGP(9) As String
        Dim BSGrade(9) As String
        Dim j As New Integer
        Dim BlendType As String = String.Empty
        Dim dsBlend As New DataSet
        Dim IsBlSg As Boolean
        Dim IsBlThick As Boolean
        Dim IsBlOTR As Boolean
        Dim IsBlWVTR As Boolean
        Dim IsBlGrade As Boolean
        Dim BOTRV(9) As String
        Dim BWVTRV(9) As String

        DsMat = objGetData.GetExtrusionDetailsPref(CaseId)
        Try
            For i = 1 To 10
                Material(i - 1) = Request.Form("ctl00$StandAssistContentPlaceHolder$tabSDesigner$tabpnl1$hidMatid" + i.ToString() + "")
                Thickness(i - 1) = Request.Form("ctl00$StandAssistContentPlaceHolder$tabSDesigner$tabpnl1$T" + i.ToString() + "")
                OTR(i - 1) = Request.Form("ctl00$StandAssistContentPlaceHolder$tabSDesigner$tabpnl1$OTR" + i.ToString() + "")
                WVTR(i - 1) = Request.Form("ctl00$StandAssistContentPlaceHolder$tabSDesigner$tabpnl1$WVTR" + i.ToString() + "")
                TS1(i - 1) = Request.Form("ctl00$StandAssistContentPlaceHolder$tabSDesigner$tabpnl1$TS1Val" + i.ToString() + "")
                TS2(i - 1) = Request.Form("ctl00$StandAssistContentPlaceHolder$tabSDesigner$tabpnl1$TS2Val" + i.ToString() + "")
                SGP(i - 1) = Request.Form("ctl00$StandAssistContentPlaceHolder$tabSDesigner$tabpnl1$SG" + i.ToString() + "")
                Grade(i - 1) = Request.Form("ctl00$StandAssistContentPlaceHolder$tabSDesigner$tabpnl1$hidSGradeId" + i.ToString() + "")

                'BlendSub Materials
                If Material(i - 1) > 500 And Material(i - 1) < 506 Then
                    dsBlend = ObjGetData.GetBlendMatDetails(CaseId, Material(i - 1), "1")
                    For j = 1 To 2
                        BlendType = "1"
                        BMaterial(j - 1) = Request.Form("ctl00$StandAssistContentPlaceHolder$tabSDesigner$tabpnl1$hidBlendid" + i.ToString() + "_" + j.ToString() + "")
                        BThickness(j - 1) = Request.Form("ctl00$StandAssistContentPlaceHolder$tabSDesigner$tabpnl1$BT" + i.ToString() + "_" + j.ToString() + "")
                        BSGP(j - 1) = Request.Form("ctl00$StandAssistContentPlaceHolder$tabSDesigner$tabpnl1$BSG" + i.ToString() + "_" + j.ToString() + "")
                        BOTR(j - 1) = Request.Form("ctl00$StandAssistContentPlaceHolder$tabSDesigner$tabpnl1$BOTR" + i.ToString() + "_" + j.ToString() + "")
                        BWVTR(j - 1) = Request.Form("ctl00$StandAssistContentPlaceHolder$tabSDesigner$tabpnl1$BWVTR" + i.ToString() + "_" + j.ToString() + "")
                        BTS1(j - 1) = Request.Form("ctl00$StandAssistContentPlaceHolder$tabSDesigner$tabpnl1$BTS1Val" + i.ToString() + "_" + j.ToString() + "")
                        BTS2(j - 1) = Request.Form("ctl00$StandAssistContentPlaceHolder$tabSDesigner$tabpnl1$BTS2Val" + i.ToString() + "_" + j.ToString() + "")
                        BSGrade(j - 1) = Request.Form("ctl00$StandAssistContentPlaceHolder$tabSDesigner$tabpnl1$hidSGradeId" + i.ToString() + "_" + j.ToString() + "")

                        'Check For IsNumric
                        If Not IsNumeric(BThickness(j - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                        If Not IsNumeric(BSGP(j - 1)) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If

                        'Check For Dependant-Indepdant Error
                        If CInt(BMaterial(j - 1)) <> 0 Then
                            'Checking Thickness
                            If CDbl(BThickness(j - 1)) <= CDbl(0.0) Then
                                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE101").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                            End If
                        Else
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE119").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                    Next
                    ObjUpIns.BlendSubMatExtrusionUpdate(CaseId, Material(i - 1), BThickness, BSGP, BMaterial, BOTR, BWVTR, BTS1, BTS2, BSGrade, BlendType)

                    If DsMat.Tables(0).Rows(0).Item("TYPEM" + i.ToString()).ToString() <> Material(i - 1) Then
                        'Started Activity Log Changes
                        Try
                            ObjUpIns.InsertLog1(Session("UserId").ToString(), "3", "Updated Blend Material id #" + Material(i - 1) + " for Structure #" + Session("SBACaseId") + "", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, Material(i - 1), "", "", "", "")
                        Catch ex As Exception

                        End Try
                        'Ended Activity Log Changes

                        For j = 1 To 2
                            'Started Activity Log Changes
                            Try
                                ObjUpIns.InsertLog1(Session("UserId").ToString(), "3", "Updated Material #" + BMaterial(j - 1) + " in Blend Material id #" + Material(i - 1) + " for Structure #" + Session("SBACaseId") + "", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, BMaterial(j - 1), "", "", "", "")
                            Catch ex As Exception

                            End Try
                            'Ended Activity Log Changes
                        Next

                    Else
                        For j = 1 To 2
                            If BMaterial(j - 1) <> "0" Then
                                If BMaterial(j - 1) <> dsBlend.Tables(0).Rows(0).Item("BCM" + j.ToString() + "").ToString() Then
                                    'Started Activity Log Changes
                                    Try
                                        ObjUpIns.InsertLog1(Session("UserId").ToString(), "3", "Updated Material #" + BMaterial(j - 1) + " in Blend Material id #" + Material(i - 1) + " for Structure #" + Session("SBACaseId") + "", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, BMaterial(j - 1), "", "", "", "")
                                    Catch ex As Exception

                                    End Try
                                    'Ended Activity Log Changes
                                Else
                                    If BSGP(j - 1) <> FormatNumber(dsBlend.Tables(0).Rows(0).Item("BCSG" + j.ToString() + "").ToString(), 3) Then
                                        IsBlSg = True
                                    End If

                                    If BThickness(j - 1) <> FormatNumber(dsBlend.Tables(0).Rows(0).Item("BCT" + j.ToString() + "").ToString()/ DsMat.Tables(0).Rows(0).Item("CONVTHICK").ToString(), 3) Then
                                        IsBlThick = True
                                    End If

                                    If dsBlend.Tables(0).Rows(0).Item("BCOTR" + j.ToString() + "").ToString() = "" Then
                                        BOTRV(j - 1) = ""
                                    Else
                                        If DsMat.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                            BOTRV(j - 1) = FormatNumber(dsBlend.Tables(0).Rows(0).Item("BCOTR" + j.ToString()).ToString(), 3)
                                        Else
                                            BOTRV(j - 1) = FormatNumber(CDbl(dsBlend.Tables(0).Rows(0).Item("BCOTR" + j.ToString())) * (CDbl(DsMat.Tables(0).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        End If
                                    End If


                                    ' If CDbl(BOTR(j - 1)) <> CDbl(BOTRV(j - 1)) Then
                                    '    IsBlOTR = True
                                    'End If
                                    If BOTR(j - 1) <> BOTRV(j - 1) Then
                                        IsBlOTR = True
                                    End If


                                    If dsBlend.Tables(0).Rows(0).Item("BCWVTR" + j.ToString() + "").ToString() = "" Then
                                        BWVTRV(j - 1) = ""
                                    Else
                                        If DsMat.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                            BWVTRV(j - 1) = FormatNumber(dsBlend.Tables(0).Rows(0).Item("BCWVTR" + j.ToString()).ToString(), 3)
                                        Else
                                            BWVTRV(j - 1) = FormatNumber(CDbl(dsBlend.Tables(0).Rows(0).Item("BCWVTR" + j.ToString())) * (CDbl(DsMat.Tables(0).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        End If
                                    End If

                                    'If CDbl(BWVTR(j - 1)) <> CDbl(BWVTRV(j - 1)) Then
                                    '      IsBlWVTR = True
                                    '  End If
                                    If BWVTR(j - 1) <> BWVTRV(j - 1) Then
                                        IsBlWVTR = True
                                    End If

                                    If BSGrade(j - 1) <> dsBlend.Tables(0).Rows(0).Item("BCGRADE" + j.ToString() + "").ToString() Then
                                        IsBlGrade = True
                                    End If

                                    If IsBlSg Or IsBlThick Or IsBlOTR Or IsBlWVTR Or IsBlGrade Then
                                        'Started Activity Log Changes
                                        Try
                                            ObjUpIns.InsertLog1(Session("UserId").ToString(), "3", "Update Specific Gravity/OTR/WVTR/Thickness/Grade in Blend Material #" + Material(i - 1) + " for Structure #" + Session("SBACaseId") + "", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, BMaterial(j - 1), "", "", BSGrade(j - 1).ToString(), "")
                                        Catch ex As Exception

                                        End Try
                                        'Ended Activity Log Changes
                                    End If
                                End If

                            End If
                            IsBlSg = False
                            IsBlThick = False
                            IsBlOTR = False
                            IsBlWVTR = False
                            IsBlGrade = False
                        Next
                    End If
                    'Thickness(i - 1) = "0.0"
                End If

                If CInt(Material(i - 1)) = 0 Then
                    'If DsMat.Tables(0).Rows(0).Item("TYPEM" + i.ToString()).ToString() > 500 And DsMat.Tables(0).Rows(0).Item("TYPEM" + i.ToString()).ToString() < 506 Then
                    '    ObjUpIns.BlendMaterialExtrusionUpdate(CaseId, DsMat.Tables(0).Rows(0).Item("TYPEM" + i.ToString()).ToString(), i.ToString())
                    'Else
                    'Check For IsNumric
                    If Not IsNumeric(Thickness(i - 1)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If
                    If Not IsNumeric(SGP(i - 1)) Then
                        Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE113").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                    End If
                    'End If
                End If


                'Check For Dependant-Indepdant Error
                If CInt(Material(i - 1)) <> 0 Then
                    'If DsMat.Tables(0).Rows(0).Item("TYPEM" + i.ToString()).ToString() > 500 And DsMat.Tables(0).Rows(0).Item("TYPEM" + i.ToString()).ToString() < 506 Then
                    '    If CInt(Material(i - 1)) <> DsMat.Tables(0).Rows(0).Item("TYPEM" + i.ToString()).ToString() Then
                    '        ObjUpIns.BlendMaterialExtrusionUpdate(CaseId, DsMat.Tables(0).Rows(0).Item("TYPEM" + i.ToString()).ToString(), i.ToString())
                    '    End If
                    'Else
                    If CInt(Material(i - 1)) > 500 And CInt(Material(i - 1)) < 506 Then
                    Else
                        'Checking Thickness
                        If CDbl(Thickness(i - 1)) <= CDbl(0.0) Then
                            Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE101").Replace("+", "!Plus!").Replace("#", "!Hash!").Replace("&", "!And!") + "")
                        End If
                    End If
                    'End If
                End If
            Next

            ObjUpIns.ExtrusionUpdate(CaseId, Material, Thickness, SGP)
            For i = 1 To 10
                If Material(i - 1) <> DsMat.Tables(0).Rows(0).Item("M" + i.ToString()).ToString() And Material(i - 1) <> DsMat.Tables(0).Rows(0).Item("TYPEM" + i.ToString()).ToString() Then
                    If Material(i - 1) > 500 And Material(i - 1) < 506 Then

                    Else
                        'Started Activity Log Changes
                        Try
                            ObjUpIns.InsertLog1(Session("UserId").ToString(), "3", "Updated Material id #" + Material(i - 1) + " for Structure #" + Session("SBACaseId") + "", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, Material(i - 1), "", "", "", "")
                        Catch ex As Exception

                        End Try
                        'Ended Activity Log Changes
                    End If
                Else
                    If Material(i - 1) <> "0" Then
                        If Material(i - 1) > 500 And Material(i - 1) < 506 Then

                        Else
                            If SGP(i - 1) <> FormatNumber(DsMat.Tables(0).Rows(0).Item("SGP" + i.ToString() + "").ToString(), 3) Then
                                IsSg = True
                            End If

                            If Thickness(i - 1) <> FormatNumber(DsMat.Tables(0).Rows(0).Item("THICK" + i.ToString() + "").ToString(), 3) Then
                                IsThick = True
                            End If

                            If DsMat.Tables(0).Rows(0).Item("OTR" + i.ToString() + "").ToString() = "" Then
                                OTRV(i - 1) = ""
                            Else
                                If DsMat.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                    OTRV(i - 1) = FormatNumber(DsMat.Tables(0).Rows(0).Item("OTR" + i.ToString()).ToString(), 3)
                                Else
                                    OTRV(i - 1) = FormatNumber(CDbl(DsMat.Tables(0).Rows(0).Item("OTR" + i.ToString())) * (CDbl(DsMat.Tables(0).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                End If
                            End If


                            If OTR(i - 1) <> OTRV(i - 1) Then
                                IsOTR = True
                            End If


                            If DsMat.Tables(0).Rows(0).Item("WVTR" + i.ToString() + "").ToString() = "" Then
                                WVTRV(i - 1) = ""
                            Else
                                If DsMat.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                    WVTRV(i - 1) = FormatNumber(DsMat.Tables(0).Rows(0).Item("WVTR" + i.ToString()).ToString(), 3)
                                Else
                                    WVTRV(i - 1) = FormatNumber(CDbl(DsMat.Tables(0).Rows(0).Item("WVTR" + i.ToString())) * (CDbl(DsMat.Tables(0).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                End If
                            End If

                            If WVTR(i - 1) <> WVTRV(i - 1) Then
                                IsWVTR = True
                            End If

                            If Grade(i - 1) <> DsMat.Tables(0).Rows(0).Item("GRADE" + i.ToString() + "").ToString() Then
                                IsGrade = True
                            End If

                            If IsSg Or IsThick Or IsOTR Or IsWVTR Or IsGrade Then
                                'Started Activity Log Changes
                                Try
                                    ObjUpIns.InsertLog1(Session("UserId").ToString(), "3", "Updated Specific Gravity/OTR/WVTR/Thickness/Grade for Structure #" + Session("SBACaseId") + "", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, Material(i - 1), "", "", Grade(i - 1).ToString(), "")
                                Catch ex As Exception

                                End Try
                                'Ended Activity Log Changes

                            End If
                        End If

                    End If
                End If
                IsSg = False
                IsThick = False
                IsOTR = False
                IsWVTR = False
                IsGrade = False
            Next

			For i = 1 To 4
                Select Case i
                    Case 1
                        Dim OTRTemp As String
                        If DsMat.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                            OTRTemp = FormatNumber(DsMat.Tables(0).Rows(0).Item("OTRTEMP").ToString(), 1)
                        Else
                            OTRTemp = FormatNumber((CDbl(DsMat.Tables(0).Rows(0).Item("OTRTEMP") * (9 / 5)) + 32).ToString(), 1)
                        End If
                        If OTRTemp <> txtOTRTemp.Text.ToString() Then
                            'Started Activity Log Changes
                            Try
                                ObjUpIns.InsertLog1(Session("UserId").ToString(), "3", "Updated OTR Temperature for Structure #" + Session("SBACaseId") + "", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                            Catch ex As Exception

                            End Try
                            'Ended Activity Log Changes
                        End If
                    Case 2
                        Dim WVTRTemp As String
                        If DsMat.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                            WVTRTemp = FormatNumber(DsMat.Tables(0).Rows(0).Item("WvTRTEMP").ToString(), 1)
                        Else
                            WVTRTemp = FormatNumber((CDbl(DsMat.Tables(0).Rows(0).Item("WVTRTEMP") * (9 / 5)) + 32).ToString(), 1)
                        End If
                        If WVTRTemp <> txtWVTRTemp.Text.ToString() Then
                            'Started Activity Log Changes
                            Try
                                ObjUpIns.InsertLog1(Session("UserId").ToString(), "3", "Updated WVTR Temperature for Structure #" + Session("SBACaseId") + "", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                            Catch ex As Exception

                            End Try
                            'Ended Activity Log Changes
                        End If
                    Case 3
                        If FormatNumber(DsMat.Tables(0).Rows(0).Item("OTRRH").ToString(), 1) <> txtOTRHumidity.Text.ToString() Then
                            'Started Activity Log Changes
                            Try
                                ObjUpIns.InsertLog1(Session("UserId").ToString(), "3", "Updated OTR Humidity for Structure #" + Session("SBACaseId") + "", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                            Catch ex As Exception

                            End Try
                            'Ended Activity Log Changes
                        End If
                    Case 4
                        If FormatNumber(DsMat.Tables(0).Rows(0).Item("WVTRRH").ToString(), 1) <> txtWVTRHumidity.Text.ToString() Then
                            'Started Activity Log Changes
                            Try
                                ObjUpIns.InsertLog1(Session("UserId").ToString(), "3", "Updated WVTR Humidity for Structure #" + Session("SBACaseId") + "", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                            Catch ex As Exception

                            End Try
                            'Ended Activity Log Changes
                        End If
                End Select
            Next
            ObjUpIns.BarrierUpdateNew(CaseId, txtOTRTemp.Text, txtWVTRTemp.Text, txtOTRHumidity.Text, txtWVTRHumidity.Text, OTR, WVTR, TS1, TS2, Grade)
            ObjUpIns.UpdateCaseViewDet(CaseId, hidOTR.Value, hidWVTR.Value, hidTS1.Value, hidTS2.Value)
            'Calculate()
            'Update Server Date


        Catch ex As Exception
            ErrorLable.Text = "Error:Update:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            Dim ObjUpIns As New StandUpInsData.UpdateInsert
            If hdnTabNum.Value = "0" Then
                UpdatePage()

            ElseIf hdnTabNum.Value = "1" Then
                UpdatePage_RH()

            End If
            ObjUpIns.ServerDateUpdate(CaseId, Session("SBAUserName"))

            GetPageDetails()
            GetPageDetailsRH()
        Catch ex As Exception
            ErrorLable.Text = "Error:Update:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub UpdatePage_RH()
        Dim ObjUpIns As New StandUpInsData.UpdateInsert
        Dim ObjGetData As New StandGetData.Selectdata()
        Dim IsORH As Boolean
        Dim IsIRH As Boolean
        Dim IsWVTR As Boolean
        Dim DsMat As New DataSet
        Try
            DsMat = ObjGetData.GetExtrusionDetailsPref(CaseId)
            If FormatNumber(DsMat.Tables(0).Rows(0).Item("ORH").ToString(), 1) <> Request.Form("ctl00$StandAssistContentPlaceHolder$tabSDesigner$tabpnl2$OIRH1").ToString() Then
                IsORH = True
            End If

            If FormatNumber(DsMat.Tables(0).Rows(0).Item("IRH").ToString(), 1) <> Request.Form("ctl00$StandAssistContentPlaceHolder$tabSDesigner$tabpnl2$OIRH21").ToString() Then
                IsIRH = True
            End If

            If DsMat.Tables(0).Rows(0).Item("UNITS").ToString() <> 1 Then
                If FormatNumber((DsMat.Tables(0).Rows(0).Item("WVTRTEMP").ToString() * (9 / 5)) + 32, 1) <> txtRHTemp.Text Then
                    IsWVTR = True
                End If
            Else
                If FormatNumber(DsMat.Tables(0).Rows(0).Item("WVTRTEMP").ToString(), 1) <> txtRHTemp.Text Then
                    IsWVTR = True
                End If
            End If
            If IsORH = True Then
                'Started Activity Log Changes
                Try
                    ObjUpIns.InsertLog1(Session("UserId").ToString(), "3", "Updated Outside Relative Humidity for Structure #" + Session("SBACaseId") + "", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            End If

            If IsIRH = True Then
                'Started Activity Log Changes
                Try
                    ObjUpIns.InsertLog1(Session("UserId").ToString(), "3", "Updated Inside Relative Humidity for Structure #" + Session("SBACaseId") + "", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            End If

            If IsWVTR = True Then
                'Started Activity Log Changes
                Try
                    ObjUpIns.InsertLog1(Session("UserId").ToString(), "3", "Updated Distribution Temperature for MVTR and OTR for Structure #" + Session("SBACaseId") + "", Session("SBACaseId"), Session("LogInCount").ToString(), Session.SessionID, "", "", "", "", "")
                Catch ex As Exception

                End Try
                'Ended Activity Log Changes
            End If
            ObjUpIns.ExtrusionUpdate_RH(CaseId, Request.Form("ctl00$StandAssistContentPlaceHolder$tabSDesigner$tabpnl2$OIRH1").ToString(), Request.Form("ctl00$StandAssistContentPlaceHolder$tabSDesigner$tabpnl2$OIRH21").ToString(), txtRHTemp.Text)
            IsIRH = False
            IsORH = False
            IsWVTR = False
        Catch ex As Exception
            ErrorLable.Text = "Error:Update:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class

