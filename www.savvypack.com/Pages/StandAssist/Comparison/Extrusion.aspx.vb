Imports System.Data
Imports System.Data.OleDb
Imports System
Imports StandGetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_StandAssist_Assumptions_Extrusion
    Inherits System.Web.UI.Page

#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iAssumptionId As Integer
    Dim _btnUpdate As ImageButton


    Public Property ErrorLable() As Label
        Get
            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property UserName() As String
        Get
            Return _strUserName
        End Get
        Set(ByVal Value As String)
            _strUserName = Value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return _strPassword
        End Get
        Set(ByVal Value As String)
            _strPassword = Value
        End Set
    End Property

    Public Property AssumptionId() As Integer
        Get
            Return _iAssumptionId
        End Get
        Set(ByVal Value As Integer)
            _iAssumptionId = Value
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


    Public DataCnt As Integer
    Public CaseDesp As New ArrayList

#End Region

#Region "MastePage Content Variables"
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Visible = False

    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Try
                GetErrorLable()
                GetUpdatebtn()
                GetSessionDetails()
                GetPageDetails()
            Catch ex As Exception

            End Try

        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetSessionDetails()
        Try
            UserName = Session("UserName")
            Password = Session("Password")
            AssumptionId = Session("AssumptionID")

            lblAID.Text = AssumptionId
            lblAdes.Text = Session("Description")

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Function GetCaseIds() As String()
        Dim CaseIds(0) As String
        Dim objGetData As New StandGetData.Selectdata
        Try
            CaseIds = objGetData.Cases(AssumptionId)
            Return CaseIds
        Catch ex As Exception
            Return CaseIds
            _lErrorLble.Text = "Error:GetCaseIds:" + ex.Message.ToString()
        End Try

    End Function

    Protected Sub GetPageDetails()
        Dim dstbl As New DataSet
        Dim dsCaseDetails As New DataSet
        Dim objGetData As New StandGetData.Selectdata
        Dim objCalbRH As New StandBarrierGetData.Calculations
        Dim objCalb As New StandBarrierGetData.Calculations
        Dim objCalb1 As New StandBarrierGetData.Calculations
        Dim objCalb2 As New StandBarrierGetData.Calculations
        Dim objCalb3 As New StandBarrierGetData.Calculations
        Dim objCalb4 As New StandBarrierGetData.Calculations
        Dim objCalb5 As New StandBarrierGetData.Calculations
        Dim objCalb6 As New StandBarrierGetData.Calculations
        Dim objCalb7 As New StandBarrierGetData.Calculations
        Dim objCalb8 As New StandBarrierGetData.Calculations
        Dim objCalb9 As New StandBarrierGetData.Calculations
        Dim CaseIds As String = String.Empty
        Dim i As New Integer
        Dim j As New Integer
        Dim k As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String
        Dim dsPref As New DataSet
        Dim dsSug As New DataSet
        Dim WeightPer1(10) As String
        Dim WeightPer2(10) As String
        Dim WeightPer3(10) As String
        Dim WeightPer4(10) As String
        Dim WeightPer5(10) As String
        Dim WeightPer6(10) As String
        Dim WeightPer7(10) As String
        Dim WeightPer8(10) As String
        Dim WeightPer9(10) As String
        Dim WeightPer10(10) As String
        Dim TotalWeightPer1 As Double
        Dim TotalWeightPer2 As Double
        Dim TotalWeightPer3 As Double
        Dim TotalWeightPer4 As Double
        Dim TotalWeightPer5 As Double
        Dim TotalWeightPer6 As Double
        Dim TotalWeightPer7 As Double
        Dim TotalWeightPer8 As Double
        Dim TotalWeightPer9 As Double
        Dim TotalWeightPer10 As Double
        Dim TotalThick1 As Double
        Dim TotalThick2 As Double
        Dim TotalThick3 As Double
        Dim TotalThick4 As Double
        Dim TotalThick5 As Double
        Dim TotalThick6 As Double
        Dim TotalThick7 As Double
        Dim TotalThick8 As Double
        Dim TotalThick9 As Double
        Dim TotalThick10 As Double
        Dim Thick(9, 9) As String
        Dim TotalSg(9) As Double
        Dim TotalThickSub(9, 9) As Double
        Dim WTPERAREAarr(9, 9) As Double

        'Blend
        Dim dsBPref As New DataSet
        Dim dsBSug As New DataSet
        Dim dvBPref As DataView
        Dim dtBPref As DataTable
        Dim dvBSug As DataView
        Dim dtBSug As DataTable

        Try
            arrCaseID = GetCaseIds()
            DataCnt = arrCaseID.Length - 1

            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim trInner As New TableRow
            Dim ddl As DropDownList
            Dim lbl As New Label
            Dim lblTSg As New Label
            Dim lblMat As New Label
            Dim lblGrade As New Label
            Dim txt As TextBox
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim Title As String = String.Empty
            Dim CurrTitle As String = String.Empty
            Dim CCurrTitle As String = String.Empty
            DWidth = txtDWidth.Text + "px"
            Dim UnitError As String = String.Empty
            Dim CurrError As String = String.Empty
            Dim lblTThick As New Label
            Dim lblTWeight As New Label
            Dim lblTOTR As New Label
            Dim lblTWVTR As New Label
            Dim lblTTS1 As New Label
            Dim lblTTS2 As New Label

            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, "200px", "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;'  />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"

            For i = 0 To DataCnt
                Dim ds As New DataSet
                ds = objGetData.GetExtrusionDetails(arrCaseID(i))
                ds.Tables(0).TableName = arrCaseID(i).ToString()
                dstbl.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())
            Next

            For i = 0 To DataCnt
                dsCaseDetails = objGetData.GetCaseDetails(arrCaseID(i).ToString())
                Cunits = Convert.ToInt32(dstbl.Tables(0).Rows(0).Item("Units").ToString())
                Units = Convert.ToInt32(dstbl.Tables(i).Rows(0).Item("Units").ToString())
                CCurrTitle = dstbl.Tables(0).Rows(0).Item("Title2").ToString().Trim()
                CurrTitle = dstbl.Tables(i).Rows(0).Item("Title2").ToString().Trim()

                tdHeader = New TableCell

                Dim Headertext As String = String.Empty

                If Cunits <> Units Then
                    UnitError = "<br/> <span  style='color:red'>Unit Mismatch</span>"
                Else
                    UnitError = ""
                End If

                If CCurrTitle <> CurrTitle Then
                    CurrError = "<br/> <span  style='color:red'>Currency Mismatch</span>"
                Else
                    CurrError = ""
                End If

                Headertext = "Structure#:" + arrCaseID(i).ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + UnitError + CurrError + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Structure" + i.ToString() + "'/>"

                CaseDesp.Add(arrCaseID(i).ToString())
                HeaderTdSetting(tdHeader, DWidth, Headertext, 1)
                trHeader.Controls.Add(tdHeader)


            Next
            tblComparision.Controls.Add(trHeader)

            'calculate OTR and WVTR
            For i = 0 To DataCnt
                Dim ARRlayerWT(10) As String
                Dim ARRlayerWTSugg(10) As String

                If i <= DataCnt Then
                    dsPref = objGetData.GetExtrusionDetailsPref(arrCaseID(i).ToString())
                    dsSug = objGetData.GetExtrusionDetailsSugg(arrCaseID(i).ToString())
                    'Getting Data for Sub Layers of Blend
                    dsBPref = objGetData.GetBlendDetailsPref(arrCaseID(i).ToString())
                    dsBSug = objGetData.GetBlendDetailsSugg(arrCaseID(i).ToString())
                    dvBPref = dsBPref.Tables(0).DefaultView()
                    dvBSug = dsBSug.Tables(0).DefaultView()

                    Dim Grades(9) As String
                    Dim Materials(9) As String
                    Dim OtrVal(9) As String
                    Dim WVTRVal(9) As String
                    Dim TS1Val(9) As String
                    Dim TS2Val(9) As String
                    Dim ISADJTHICK(9) As String
                    Dim MAT(9) As String
                    Dim DGVal(9) As String
                    Dim WeightPer(9) As String
                    Dim BMatPer(9, 1) As String
                    'Collecting Data for Sublayers of Blend            
                    Dim BWVTR(9, 1) As String
                    Dim BOTR(9, 1) As String
                    Dim BTS1(9, 1) As String
                    Dim BTS2(9, 1) As String
                    Dim BMAT(9, 1) As String
                    Dim BTHICK(9, 1) As String
                    Dim BTHICKPER(9, 1) As String
                    Dim BADJTHICK(9, 1) As String
                    Dim BMATERIALS(9, 1) As String
                    Dim BDG(9, 1) As String
                    Dim TotalThick As Double = 0.0

                    For j = 0 To 9
                        Grades(j) = dsPref.Tables(0).Rows(0).Item("GRADE" + (j + 1).ToString() + "").ToString()
                        If dsPref.Tables(0).Rows(0).Item("TYPEM" + (j + 1).ToString()).ToString() <> "0" Then
                            'Data for Sublayers of Blend
                            MAT(j) = dsPref.Tables(0).Rows(0).Item("TYPEM" + (j + 1).ToString() + "").ToString()
                            If MAT(j) > 500 And MAT(j) < 506 Then
                                dvBPref.RowFilter = "TYPEM=" + MAT(j) + ""
                                dvBSug.RowFilter = "TYPEM=" + MAT(j) + ""
                                dtBPref = dvBPref.ToTable()
                                dtBSug = dvBSug.ToTable()
                                Dim TotalThickB As Double = 0
                                If dtBPref.Rows.Count > 0 Then
                                    For k = 0 To 1
                                        If TotalThickB = 0 Then
                                            TotalThickB = CDbl(dtBPref.Rows(0).Item("BCT" + (k + 1).ToString() + "").ToString() * dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString())
                                        Else
                                            TotalThickB = TotalThickB + CDbl(dtBPref.Rows(0).Item("BCT" + (k + 1).ToString() + "").ToString() * dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString())
                                        End If
                                        BMatPer(j, k) = CDbl(dtBPref.Rows(0).Item("BCT" + (k + 1).ToString() + "").ToString() * dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString())
                                    Next
                                    For k = 0 To 1
                                        BMatPer(j, k) = BMatPer(j, k) / TotalThickB
                                    Next
                                Else
                                    TotalThickB = 0
                                End If
                                Thick(i, j) = CDbl(TotalThickB)
                                TotalThick = TotalThick + (CDbl(Thick(i, j)))
                                For k = 0 To 1
                                    If dtBPref.Rows.Count > 0 Then
                                        BMAT(j, k) = dtBPref.Rows(0).Item("BCM" + (k + 1).ToString() + "").ToString()
                                        BTHICK(j, k) = dtBPref.Rows(0).Item("BCT" + (k + 1).ToString() + "").ToString() * dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString()
                                        If TotalThickB <> 0 Then
                                            BTHICKPER(j, k) = ((CDbl(BTHICK(j, k)) / TotalThickB) * 100).ToString()
                                        End If
                                        BOTR(j, k) = dtBPref.Rows(0).Item("BCOTR" + (k + 1).ToString() + "").ToString()
                                        BWVTR(j, k) = dtBPref.Rows(0).Item("BCWVTR" + (k + 1).ToString() + "").ToString()
                                        BTS1(j, k) = dtBPref.Rows(0).Item("BCTS1VAL" + (k + 1).ToString() + "").ToString()
                                        BTS2(j, k) = dtBPref.Rows(0).Item("BCTS2VAL" + (k + 1).ToString() + "").ToString()
                                        BADJTHICK(j, k) = dtBSug.Rows(0).Item("ISADJTHICK" + (k + 1).ToString() + "").ToString()
                                        BMATERIALS(j, k) = dtBSug.Rows(0).Item("MATS" + (k + 1).ToString() + "").ToString()
                                        BDG(j, k) = dtBSug.Rows(0).Item("DG" + (k + 1).ToString() + "").ToString()
                                    Else
                                        BMAT(j, k) = "0"
                                        BTHICK(j, k) = "0"
                                        BOTR(j, k) = ""
                                        BWVTR(j, k) = ""
                                        BTS1(j, k) = ""
                                        BTS2(j, k) = ""
                                        BADJTHICK(j, k) = ""
                                        BTHICKPER(j, k) = "0"
                                        BMATERIALS(j, k) = ""
                                        BDG(j, k) = ""
                                    End If
                                Next
                            End If
                        ElseIf dsPref.Tables(0).Rows(0).Item("M" + (j + 1).ToString() + "").ToString() <> "" Then
                            MAT(j) = dsPref.Tables(0).Rows(0).Item("M" + (j + 1).ToString() + "").ToString()
                            Thick(i, j) = CDbl(dsPref.Tables(0).Rows(0).Item("THICK" + (j + 1).ToString() + "").ToString() * dsPref.Tables(0).Rows(0).Item("CONVTHICK").ToString())
                            TotalThick = TotalThick + (CDbl(Thick(i, j)))
                            For k = 0 To 1
                                BMAT(j, k) = "0"
                                BTHICK(j, k) = "0"
                                BOTR(j, k) = ""
                                BWVTR(j, k) = ""
                                BTS1(j, k) = ""
                                BTS2(j, k) = ""
                                BADJTHICK(j, k) = ""
                                BTHICKPER(j, k) = "0"
                                BMATERIALS(j, k) = ""
                                BDG(j, k) = ""
                            Next
                        End If
                        Materials(j) = dsSug.Tables(0).Rows(0).Item("MATS" + (j + 1).ToString() + "").ToString()
                        OtrVal(j) = dsPref.Tables(0).Rows(0).Item("OTR" + (j + 1).ToString() + "").ToString()
                        WVTRVal(j) = dsPref.Tables(0).Rows(0).Item("WVTR" + (j + 1).ToString() + "").ToString()
                        TS1Val(j) = dsPref.Tables(0).Rows(0).Item("TS1VAL" + (j + 1).ToString() + "").ToString()
                        TS2Val(j) = dsPref.Tables(0).Rows(0).Item("TS2VAL" + (j + 1).ToString() + "").ToString()
                        ISADJTHICK(j) = dsSug.Tables(0).Rows(0).Item("ISADJTHICK" + (j + 1).ToString() + "").ToString()
                        DGVal(j) = dsSug.Tables(0).Rows(0).Item("DG" + (j + 1).ToString() + "").ToString()
                    Next

                    For j = 0 To 9
                        WeightPer(j) = CDbl(Thick(i, j)) / TotalThick
                    Next

                    Dim ds As DataSet
                    ds = objCalbRH.BarrierDataComp(MAT, CDate(dsPref.Tables(0).Rows(0).Item("EFFDATEB").ToString()), BMAT)

                    If i = 0 Then
                        objCalb.BarrierPropCalculateNew(arrCaseID(i).ToString(), dsPref.Tables(0).Rows(0).Item("OTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("OTRRH").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRRH").ToString(), _
                    MAT, CDate(dsPref.Tables(0).Rows(0).Item("EFFDATEB").ToString()), Materials, OtrVal, WVTRVal, TS1Val, TS2Val, DGVal, Thick, ISADJTHICK, BMAT, BADJTHICK, BTHICKPER, BOTR, BWVTR, BTS1, BTS2, BDG, BTHICK, i, ds, BMATERIALS, WeightPer, BMatPer)
                    ElseIf i = 1 Then
                        objCalb1.BarrierPropCalculateNew(arrCaseID(i).ToString(), dsPref.Tables(0).Rows(0).Item("OTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("OTRRH").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRRH").ToString(), _
                    MAT, CDate(dsPref.Tables(0).Rows(0).Item("EFFDATEB").ToString()), Materials, OtrVal, WVTRVal, TS1Val, TS2Val, DGVal, Thick, ISADJTHICK, BMAT, BADJTHICK, BTHICKPER, BOTR, BWVTR, BTS1, BTS2, BDG, BTHICK, i, ds, BMATERIALS, WeightPer, BMatPer)
                    ElseIf i = 2 Then
                        objCalb2.BarrierPropCalculateNew(arrCaseID(i).ToString(), dsPref.Tables(0).Rows(0).Item("OTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("OTRRH").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRRH").ToString(), _
                        MAT, CDate(dsPref.Tables(0).Rows(0).Item("EFFDATEB").ToString()), Materials, OtrVal, WVTRVal, TS1Val, TS2Val, DGVal, Thick, ISADJTHICK, BMAT, BADJTHICK, BTHICKPER, BOTR, BWVTR, BTS1, BTS2, BDG, BTHICK, i, ds, BMATERIALS, WeightPer, BMatPer)
                    ElseIf i = 3 Then
                        objCalb3.BarrierPropCalculateNew(arrCaseID(i).ToString(), dsPref.Tables(0).Rows(0).Item("OTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("OTRRH").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRRH").ToString(), _
                        MAT, CDate(dsPref.Tables(0).Rows(0).Item("EFFDATEB").ToString()), Materials, OtrVal, WVTRVal, TS1Val, TS2Val, DGVal, Thick, ISADJTHICK, BMAT, BADJTHICK, BTHICKPER, BOTR, BWVTR, BTS1, BTS2, BDG, BTHICK, i, ds, BMATERIALS, WeightPer, BMatPer)
                    ElseIf i = 4 Then
                        objCalb4.BarrierPropCalculateNew(arrCaseID(i).ToString(), dsPref.Tables(0).Rows(0).Item("OTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("OTRRH").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRRH").ToString(), _
                        MAT, CDate(dsPref.Tables(0).Rows(0).Item("EFFDATEB").ToString()), Materials, OtrVal, WVTRVal, TS1Val, TS2Val, DGVal, Thick, ISADJTHICK, BMAT, BADJTHICK, BTHICKPER, BOTR, BWVTR, BTS1, BTS2, BDG, BTHICK, i, ds, BMATERIALS, WeightPer, BMatPer)
                    ElseIf i = 5 Then
                        objCalb5.BarrierPropCalculateNew(arrCaseID(i).ToString(), dsPref.Tables(0).Rows(0).Item("OTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("OTRRH").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRRH").ToString(), _
                        MAT, CDate(dsPref.Tables(0).Rows(0).Item("EFFDATEB").ToString()), Materials, OtrVal, WVTRVal, TS1Val, TS2Val, DGVal, Thick, ISADJTHICK, BMAT, BADJTHICK, BTHICKPER, BOTR, BWVTR, BTS1, BTS2, BDG, BTHICK, i, ds, BMATERIALS, WeightPer, BMatPer)
                    ElseIf i = 6 Then
                        objCalb6.BarrierPropCalculateNew(arrCaseID(i).ToString(), dsPref.Tables(0).Rows(0).Item("OTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("OTRRH").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRRH").ToString(), _
                        MAT, CDate(dsPref.Tables(0).Rows(0).Item("EFFDATEB").ToString()), Materials, OtrVal, WVTRVal, TS1Val, TS2Val, DGVal, Thick, ISADJTHICK, BMAT, BADJTHICK, BTHICKPER, BOTR, BWVTR, BTS1, BTS2, BDG, BTHICK, i, ds, BMATERIALS, WeightPer, BMatPer)
                    ElseIf i = 7 Then
                        objCalb7.BarrierPropCalculateNew(arrCaseID(i).ToString(), dsPref.Tables(0).Rows(0).Item("OTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("OTRRH").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRRH").ToString(), _
                        MAT, CDate(dsPref.Tables(0).Rows(0).Item("EFFDATEB").ToString()), Materials, OtrVal, WVTRVal, TS1Val, TS2Val, DGVal, Thick, ISADJTHICK, BMAT, BADJTHICK, BTHICKPER, BOTR, BWVTR, BTS1, BTS2, BDG, BTHICK, i, ds, BMATERIALS, WeightPer, BMatPer)
                    ElseIf i = 8 Then
                        objCalb8.BarrierPropCalculateNew(arrCaseID(i).ToString(), dsPref.Tables(0).Rows(0).Item("OTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("OTRRH").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRRH").ToString(), _
                        MAT, CDate(dsPref.Tables(0).Rows(0).Item("EFFDATEB").ToString()), Materials, OtrVal, WVTRVal, TS1Val, TS2Val, DGVal, Thick, ISADJTHICK, BMAT, BADJTHICK, BTHICKPER, BOTR, BWVTR, BTS1, BTS2, BDG, BTHICK, i, ds, BMATERIALS, WeightPer, BMatPer)
                    ElseIf i = 9 Then
                        objCalb9.BarrierPropCalculateNew(arrCaseID(i).ToString(), dsPref.Tables(0).Rows(0).Item("OTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRTEMP").ToString(), dsPref.Tables(0).Rows(0).Item("OTRRH").ToString(), dsPref.Tables(0).Rows(0).Item("WVTRRH").ToString(), _
                    MAT, CDate(dsPref.Tables(0).Rows(0).Item("EFFDATEB").ToString()), Materials, OtrVal, WVTRVal, TS1Val, TS2Val, DGVal, Thick, ISADJTHICK, BMAT, BADJTHICK, BTHICKPER, BOTR, BWVTR, BTS1, BTS2, BDG, BTHICK, i, ds, BMATERIALS, WeightPer, BMatPer)
                    End If
                    'Total Thickness Calculation
                    If i = 0 Then
                        For m = 0 To 9
                            If dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString() > 500 And dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString() < 506 Then
                                dvBPref.RowFilter = "TYPEM=" + dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString()
                                dtBPref = dvBPref.ToTable()
                                Dim TotalThickBlend As Double = 0
                                If dtBPref.Rows.Count > 0 Then
                                    For l = 1 To 2
                                        TotalThickBlend = TotalThickBlend + dtBPref.Rows(0).Item("BCT" + l.ToString() + "")
                                        TotalThick1 = TotalThick1 + dtBPref.Rows(0).Item("BCT" + l.ToString() + "")
                                    Next
                                    TotalThickSub(i, m) = TotalThickBlend
                                End If
                            Else
                                If dsPref.Tables(0).Rows(0).Item("THICK" + (m + 1).ToString() + "").ToString() <> "" Then
                                    TotalThick1 = TotalThick1 + dsPref.Tables(0).Rows(0).Item("THICK" + (m + 1).ToString() + "")
                                End If
                            End If
                        Next
                    ElseIf i = 1 Then
                        For m = 0 To 9
                            If dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString() > 500 And dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString() < 506 Then
                                dvBPref.RowFilter = "TYPEM=" + dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString()
                                dtBPref = dvBPref.ToTable()
                                Dim TotalThickBlend As Double = 0
                                If dtBPref.Rows.Count > 0 Then
                                    For l = 1 To 2
                                        TotalThickBlend = TotalThickBlend + dtBPref.Rows(0).Item("BCT" + l.ToString() + "")
                                        TotalThick2 = TotalThick2 + dtBPref.Rows(0).Item("BCT" + l.ToString() + "")
                                    Next
                                    TotalThickSub(i, m) = TotalThickBlend
                                End If
                            Else
                                If dsPref.Tables(0).Rows(0).Item("THICK" + (m + 1).ToString() + "").ToString() <> "" Then
                                    TotalThick2 = TotalThick2 + dsPref.Tables(0).Rows(0).Item("THICK" + (m + 1).ToString() + "")
                                End If
                            End If
                        Next
                    ElseIf i = 2 Then
                        For m = 0 To 9
                            If dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString() > 500 And dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString() < 506 Then
                                dvBPref.RowFilter = "TYPEM=" + dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString()
                                dtBPref = dvBPref.ToTable()
                                Dim TotalThickBlend As Double = 0
                                If dtBPref.Rows.Count > 0 Then
                                    For l = 1 To 2
                                        TotalThickBlend = TotalThickBlend + dtBPref.Rows(0).Item("BCT" + l.ToString() + "")
                                        TotalThick3 = TotalThick3 + dtBPref.Rows(0).Item("BCT" + l.ToString() + "")
                                    Next
                                    TotalThickSub(i, m) = TotalThickBlend
                                End If
                            Else
                                If dsPref.Tables(0).Rows(0).Item("THICK" + (m + 1).ToString() + "").ToString() <> "" Then
                                    TotalThick3 = TotalThick3 + dsPref.Tables(0).Rows(0).Item("THICK" + (m + 1).ToString() + "")
                                End If
                            End If
                        Next
                    ElseIf i = 3 Then
                        For m = 0 To 9
                            If dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString() > 500 And dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString() < 506 Then
                                dvBPref.RowFilter = "TYPEM=" + dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString()
                                dtBPref = dvBPref.ToTable()
                                Dim TotalThickBlend As Double = 0
                                If dtBPref.Rows.Count > 0 Then
                                    For l = 1 To 2
                                        TotalThickBlend = TotalThickBlend + dtBPref.Rows(0).Item("BCT" + l.ToString() + "")
                                        TotalThick4 = TotalThick4 + dtBPref.Rows(0).Item("BCT" + l.ToString() + "")
                                    Next
                                    TotalThickSub(i, m) = TotalThickBlend
                                End If
                            Else
                                If dsPref.Tables(0).Rows(0).Item("THICK" + (m + 1).ToString() + "").ToString() <> "" Then
                                    TotalThick4 = TotalThick4 + dsPref.Tables(0).Rows(0).Item("THICK" + (m + 1).ToString() + "")
                                End If
                            End If
                        Next
                    ElseIf i = 4 Then
                        For m = 0 To 9
                            If dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString() > 500 And dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString() < 506 Then
                                dvBPref.RowFilter = "TYPEM=" + dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString()
                                dtBPref = dvBPref.ToTable()
                                Dim TotalThickBlend As Double = 0
                                If dtBPref.Rows.Count > 0 Then
                                    For l = 1 To 2
                                        TotalThickBlend = TotalThickBlend + dtBPref.Rows(0).Item("BCT" + l.ToString() + "")
                                        TotalThick5 = TotalThick5 + dtBPref.Rows(0).Item("BCT" + l.ToString() + "")
                                    Next
                                    TotalThickSub(i, m) = TotalThickBlend
                                End If
                            Else
                                If dsPref.Tables(0).Rows(0).Item("THICK" + (m + 1).ToString() + "").ToString() <> "" Then
                                    TotalThick5 = TotalThick5 + dsPref.Tables(0).Rows(0).Item("THICK" + (m + 1).ToString() + "")
                                End If
                            End If
                        Next
                    ElseIf i = 5 Then
                        For m = 0 To 9
                            If dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString() > 500 And dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString() < 506 Then
                                dvBPref.RowFilter = "TYPEM=" + dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString()
                                dtBPref = dvBPref.ToTable()
                                Dim TotalThickBlend As Double = 0
                                If dtBPref.Rows.Count > 0 Then
                                    For l = 1 To 2
                                        TotalThickBlend = TotalThickBlend + dtBPref.Rows(0).Item("BCT" + l.ToString() + "")
                                        TotalThick6 = TotalThick6 + dtBPref.Rows(0).Item("BCT" + l.ToString() + "")
                                    Next
                                    TotalThickSub(i, m) = TotalThickBlend
                                End If
                            Else
                                If dsPref.Tables(0).Rows(0).Item("THICK" + (m + 1).ToString() + "").ToString() <> "" Then
                                    TotalThick6 = TotalThick6 + dsPref.Tables(0).Rows(0).Item("THICK" + (m + 1).ToString() + "")
                                End If
                            End If
                        Next
                    ElseIf i = 6 Then
                        For m = 0 To 9
                            If dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString() > 500 And dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString() < 506 Then
                                dvBPref.RowFilter = "TYPEM=" + dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString()
                                dtBPref = dvBPref.ToTable()
                                Dim TotalThickBlend As Double = 0
                                If dtBPref.Rows.Count > 0 Then
                                    For l = 1 To 2
                                        TotalThickBlend = TotalThickBlend + dtBPref.Rows(0).Item("BCT" + l.ToString() + "")
                                        TotalThick7 = TotalThick7 + dtBPref.Rows(0).Item("BCT" + l.ToString() + "")
                                    Next
                                    TotalThickSub(i, m) = TotalThickBlend
                                End If
                            Else
                                If dsPref.Tables(0).Rows(0).Item("THICK" + (m + 1).ToString() + "").ToString() <> "" Then
                                    TotalThick7 = TotalThick7 + dsPref.Tables(0).Rows(0).Item("THICK" + (m + 1).ToString() + "")
                                End If
                            End If
                        Next
                    ElseIf i = 7 Then
                        For m = 0 To 9
                            If dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString() > 500 And dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString() < 506 Then
                                dvBPref.RowFilter = "TYPEM=" + dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString()
                                dtBPref = dvBPref.ToTable()
                                Dim TotalThickBlend As Double = 0
                                If dtBPref.Rows.Count > 0 Then
                                    For l = 1 To 2
                                        TotalThickBlend = TotalThickBlend + dtBPref.Rows(0).Item("BCT" + l.ToString() + "")
                                        TotalThick8 = TotalThick8 + dtBPref.Rows(0).Item("BCT" + l.ToString() + "")
                                    Next
                                    TotalThickSub(i, m) = TotalThickBlend
                                End If
                            Else
                                If dsPref.Tables(0).Rows(0).Item("THICK" + (m + 1).ToString() + "").ToString() <> "" Then
                                    TotalThick8 = TotalThick8 + dsPref.Tables(0).Rows(0).Item("THICK" + (m + 1).ToString() + "")
                                End If
                            End If
                        Next
                    ElseIf i = 8 Then
                        For m = 0 To 9
                            If dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString() > 500 And dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString() < 506 Then
                                dvBPref.RowFilter = "TYPEM=" + dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString()
                                dtBPref = dvBPref.ToTable()
                                Dim TotalThickBlend As Double = 0
                                If dtBPref.Rows.Count > 0 Then
                                    For l = 1 To 2
                                        TotalThickBlend = TotalThickBlend + dtBPref.Rows(0).Item("BCT" + l.ToString() + "")
                                        TotalThick9 = TotalThick9 + dtBPref.Rows(0).Item("BCT" + l.ToString() + "")
                                    Next
                                    TotalThickSub(i, m) = TotalThickBlend
                                End If
                            Else
                                If dsPref.Tables(0).Rows(0).Item("THICK" + (m + 1).ToString() + "").ToString() <> "" Then
                                    TotalThick9 = TotalThick9 + dsPref.Tables(0).Rows(0).Item("THICK" + (m + 1).ToString() + "")
                                End If
                            End If
                        Next
                    ElseIf i = 9 Then
                        For m = 0 To 9
                            If dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString() > 500 And dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString() < 506 Then
                                dvBPref.RowFilter = "TYPEM=" + dsPref.Tables(0).Rows(0).Item("TYPEM" + (m + 1).ToString() + "").ToString()
                                dtBPref = dvBPref.ToTable()
                                Dim TotalThickBlend As Double = 0
                                If dtBPref.Rows.Count > 0 Then
                                    For l = 1 To 2
                                        TotalThickBlend = TotalThickBlend + dtBPref.Rows(0).Item("BCT" + l.ToString() + "")
                                        TotalThick10 = TotalThick10 + dtBPref.Rows(0).Item("BCT" + l.ToString() + "")
                                    Next
                                    TotalThickSub(i, m) = TotalThickBlend
                                End If
                            Else
                                If dsPref.Tables(0).Rows(0).Item("THICK" + (m + 1).ToString() + "").ToString() <> "" Then
                                    TotalThick10 = TotalThick10 + dsPref.Tables(0).Rows(0).Item("THICK" + (m + 1).ToString() + "")
                                End If
                            End If
                        Next
                    End If

                    Dim WTPERAREA As Double = 0
                    For l = 1 To 10
                        Dim BLLayerWTSUB As Double = 0
                        Dim BLLayerWTSUBLAYER As Double
                        If dsPref.Tables(0).Rows(0).Item("TYPEM" + l.ToString() + "").ToString() > 500 And dsPref.Tables(0).Rows(0).Item("TYPEM" + l.ToString() + "").ToString() < 506 Then
                            'Weight of Blend Layers                            
                            dvBPref.RowFilter = "TYPEM=" + dsPref.Tables(0).Rows(0).Item("TYPEM" + l.ToString() + "").ToString()
                            dtBPref = dvBPref.ToTable()
                            dvBSug.RowFilter = "TYPEM=" + dsPref.Tables(0).Rows(0).Item("TYPEM" + l.ToString()).ToString() + ""
                            dtBSug = dvBSug.ToTable()
                            If dtBPref.Rows.Count > 0 Then
                                For m = 1 To 2
                                    BLLayerWTSUBLAYER = 0.0
                                    'Weight of Sub Layers
                                    If CDbl(dtBPref.Rows(0).Item("BCSG" + m.ToString() + "").ToString()) <> 0.0 Then
                                        BLLayerWTSUBLAYER = (1000 * (CDbl(dtBPref.Rows(0).Item("BCT" + m.ToString() + "").ToString()) / 1000) / 1728) * 62.4 * CDbl(dtBPref.Rows(0).Item("BCSG" + m.ToString() + "").ToString())
                                    Else
                                        BLLayerWTSUBLAYER = (1000 * (CDbl(dtBPref.Rows(0).Item("BCT" + m.ToString() + "").ToString()) / 1000) / 1728) * 62.4 * CDbl(dtBSug.Rows(0).Item("SGS" + m.ToString() + "").ToString())
                                    End If
                                    BLLayerWTSUB = BLLayerWTSUB + BLLayerWTSUBLAYER
                                Next
                                ARRlayerWTSugg(l) = BLLayerWTSUB
                                WTPERAREAarr(i, l - 1) = ARRlayerWTSugg(l)
                            End If
                            If CDbl(dsPref.Tables(0).Rows(0).Item("TYPEMSG" + l.ToString() + "").ToString()) <> 0.0 Then
                                ARRlayerWT(l) = (1000 * (CDbl(TotalThickSub(i, l - 1)) / 1000) / 1728) * 62.4 * CDbl(dsPref.Tables(0).Rows(0).Item("TYPEMSG" + l.ToString() + "").ToString())
                            Else
                                ARRlayerWT(l) = ARRlayerWTSugg(l)
                            End If
                        Else
                            If CDbl(dsPref.Tables(0).Rows(0).Item("SGP" + l.ToString() + "").ToString()) <> 0.0 Then
                                ARRlayerWT(l) = (1000 * (CDbl(dsPref.Tables(0).Rows(0).Item("THICK" + l.ToString() + "").ToString()) / 1000) / 1728) * 62.4 * CDbl(dsPref.Tables(0).Rows(0).Item("SGP" + l.ToString() + "").ToString())
                            Else
                                ARRlayerWT(l) = (1000 * (CDbl(dsPref.Tables(0).Rows(0).Item("THICK" + l.ToString() + "").ToString()) / 1000) / 1728) * 62.4 * CDbl(dsSug.Tables(0).Rows(0).Item("SGS" + l.ToString() + "").ToString())
                            End If
                        End If
                        WTPERAREA = WTPERAREA + CDbl(ARRlayerWT(l))
                    Next
                    For l = 1 To 10
                        If i = 0 Then
                            If WTPERAREA > 0 Then
                                WeightPer1(l) = CDbl(ARRlayerWT(l)) / CDbl(WTPERAREA) * 100
                                If l = 10 Then
                                    If CDbl(TotalThick1) <> CDbl(0) Then
                                        TotalSg(i) = (WTPERAREA / ((1000 * TotalThick1 / 1000) / 1728)) / 62.4
                                    End If
                                End If
                            Else
                                WeightPer1(l) = 0.0
                                TotalSg(i) = 0
                            End If
                            TotalWeightPer1 = TotalWeightPer1 + WeightPer1(l)
                        ElseIf i = 1 Then
                            If WTPERAREA > 0 Then
                                WeightPer2(l) = CDbl(ARRlayerWT(l)) / CDbl(WTPERAREA) * 100
                                If l = 10 Then
                                    If CDbl(TotalThick2) <> CDbl(0) Then
                                        TotalSg(i) = (WTPERAREA / ((1000 * TotalThick2 / 1000) / 1728)) / 62.4
                                    End If
                                End If
                            Else
                                WeightPer2(l) = 0.0
                                TotalSg(i) = 0
                            End If
                            TotalWeightPer2 = WeightPer2(l) + TotalWeightPer2
                        ElseIf i = 2 Then
                            If WTPERAREA > 0 Then
                                WeightPer3(l) = CDbl(ARRlayerWT(l)) / CDbl(WTPERAREA) * 100
                                If l = 10 Then
                                    If CDbl(TotalThick3) <> CDbl(0) Then
                                        TotalSg(i) = (WTPERAREA / ((1000 * TotalThick3 / 1000) / 1728)) / 62.4
                                    End If
                                End If
                            Else
                                WeightPer3(l) = 0.0
                                TotalSg(i) = 0
                            End If
                            TotalWeightPer3 = WeightPer3(l) + TotalWeightPer3
                        ElseIf i = 3 Then
                            If WTPERAREA > 0 Then
                                WeightPer4(l) = CDbl(ARRlayerWT(l)) / CDbl(WTPERAREA) * 100
                                If l = 10 Then
                                    If CDbl(TotalThick4) <> CDbl(0) Then
                                        TotalSg(i) = (WTPERAREA / ((1000 * TotalThick4 / 1000) / 1728)) / 62.4
                                    End If
                                End If
                            Else
                                WeightPer4(l) = 0.0
                                TotalSg(i) = 0
                            End If
                            TotalWeightPer4 = WeightPer4(l) + TotalWeightPer4
                        ElseIf i = 4 Then
                            If WTPERAREA > 0 Then
                                WeightPer5(l) = CDbl(ARRlayerWT(l)) / CDbl(WTPERAREA) * 100
                                If l = 10 Then
                                    If CDbl(TotalThick5) <> CDbl(0) Then
                                        TotalSg(i) = (WTPERAREA / ((1000 * TotalThick5 / 1000) / 1728)) / 62.4
                                    End If
                                End If
                            Else
                                WeightPer5(l) = 0.0
                                TotalSg(i) = 0
                            End If
                            TotalWeightPer5 = WeightPer5(l) + TotalWeightPer5
                        ElseIf i = 5 Then
                            If WTPERAREA > 0 Then
                                WeightPer6(l) = CDbl(ARRlayerWT(l)) / CDbl(WTPERAREA) * 100
                                If l = 10 Then
                                    If CDbl(TotalThick6) <> CDbl(0) Then
                                        TotalSg(i) = (WTPERAREA / ((1000 * TotalThick6 / 1000) / 1728)) / 62.4
                                    End If
                                End If
                            Else
                                WeightPer6(l) = 0.0
                                TotalSg(i) = 0
                            End If
                            TotalWeightPer6 = WeightPer6(l) + TotalWeightPer6
                        ElseIf i = 6 Then
                            If WTPERAREA > 0 Then
                                WeightPer7(l) = CDbl(ARRlayerWT(l)) / CDbl(WTPERAREA) * 100
                                If l = 10 Then
                                    If CDbl(TotalThick7) <> CDbl(0) Then
                                        TotalSg(i) = (WTPERAREA / ((1000 * TotalThick7 / 1000) / 1728)) / 62.4
                                    End If
                                End If
                            Else
                                WeightPer7(l) = 0.0
                                TotalSg(i) = 0
                            End If
                            TotalWeightPer7 = WeightPer7(l) + TotalWeightPer7
                        ElseIf i = 7 Then
                            If WTPERAREA > 0 Then
                                WeightPer8(l) = CDbl(ARRlayerWT(l)) / CDbl(WTPERAREA) * 100
                                If l = 10 Then
                                    If CDbl(TotalThick8) <> CDbl(0) Then
                                        TotalSg(i) = (WTPERAREA / ((1000 * TotalThick8 / 1000) / 1728)) / 62.4
                                    End If
                                End If
                            Else
                                WeightPer8(l) = 0.0
                                TotalSg(i) = 0
                            End If
                            TotalWeightPer8 = WeightPer8(l) + TotalWeightPer8
                        ElseIf i = 8 Then
                            If WTPERAREA > 0 Then
                                WeightPer9(l) = CDbl(ARRlayerWT(l)) / CDbl(WTPERAREA) * 100
                                If l = 10 Then
                                    If CDbl(TotalThick9) <> CDbl(0) Then
                                        TotalSg(i) = (WTPERAREA / ((1000 * TotalThick9 / 1000) / 1728)) / 62.4
                                    End If
                                End If
                            Else
                                WeightPer9(l) = 0.0
                                TotalSg(i) = 0
                            End If
                            TotalWeightPer9 = WeightPer9(l) + TotalWeightPer9
                        ElseIf i = 9 Then
                            If WTPERAREA > 0 Then
                                WeightPer10(l) = CDbl(ARRlayerWT(l)) / CDbl(WTPERAREA) * 100
                                If l = 10 Then
                                    If CDbl(TotalThick10) <> CDbl(0) Then
                                        TotalSg(i) = (WTPERAREA / ((1000 * TotalThick10 / 1000) / 1728)) / 62.4
                                    End If
                                End If
                            Else
                                WeightPer10(l) = 0.0
                                TotalSg(i) = 0
                            End If
                            TotalWeightPer10 = WeightPer10(l) + TotalWeightPer10
                        End If
                    Next
                End If
            Next




            For i = 1 To 10
                For j = 1 To 16
                    trInner = New TableRow()

                    Select Case j
                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Layer" + i.ToString() + "</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 2
                            'If i = 1 Then
                            tdInner = New TableCell
                            Title = ""
                            LeftTdSetting(tdInner, "Effective Date" + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "EFFD_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = dstbl.Tables(k).Rows(0).Item("EDATE").ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                            'End If
                        Case 3
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Material", trInner, "AlterNateColor1")
                            trInner.ID = "M_" + i.ToString()

                            For k = 0 To DataCnt
                                lblMat = New Label
                                lblMat.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If dstbl.Tables(k).Rows(0).Item("TYPEM" + i.ToString() + "").ToString() <> 0 Then
                                    GetMaterialDetails(lblMat, dstbl.Tables(k).Rows(0).Item("TYPEM" + i.ToString() + "").ToString())
                                ElseIf dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString() <> 0 Then
                                    GetMaterialDetails(lblMat, dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString())
                                End If
                                tdInner.Controls.Add(lblMat)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 4
                            tdInner = New TableCell
                            'Title = "(" + dstbl.Tables(0).Rows(0).Item("Title1").ToString() + ")"
                            LeftTdSetting(tdInner, "Grade", trInner, "AlterNateColor2")
                            trInner.ID = "GRADE_" + i.ToString()

                            For k = 0 To DataCnt
                                lblGrade = New Label
                                lblGrade.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If dstbl.Tables(k).Rows(0).Item("TYPEM" + i.ToString() + "").ToString() <> 0 Then
                                    GetGradeDetails(lblGrade, dstbl.Tables(k).Rows(0).Item("TYPEM" + i.ToString() + "").ToString())
                                ElseIf dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString() <> 0 Then
                                    GetGradeDetails(lblGrade, dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString())
                                End If
                                tdInner.Controls.Add(lblGrade)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 5
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title1").ToString() + ")"
                            LeftTdSetting(tdInner, "Thickness " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "T_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lbl.Text = FormatNumber(Thick(k, i - 1) / dstbl.Tables(k).Rows(0).Item("CONVTHICK").ToString(), 3).ToString()
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 6
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Weight(%)", trInner, "AlterNateColor2")
                            trInner.ID = "Weight_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If k = 0 Then
                                    lbl.Text = FormatNumber(WeightPer1(i), 2)
                                ElseIf k = 1 Then
                                    lbl.Text = FormatNumber(WeightPer2(i), 2)
                                ElseIf k = 2 Then
                                    lbl.Text = FormatNumber(WeightPer3(i), 2)
                                ElseIf k = 3 Then
                                    lbl.Text = FormatNumber(WeightPer4(i), 2)
                                ElseIf k = 4 Then
                                    lbl.Text = FormatNumber(WeightPer5(i), 2)
                                ElseIf k = 5 Then
                                    lbl.Text = FormatNumber(WeightPer6(i), 2)
                                ElseIf k = 6 Then
                                    lbl.Text = FormatNumber(WeightPer7(i), 2)
                                ElseIf k = 7 Then
                                    lbl.Text = FormatNumber(WeightPer8(i), 2)
                                ElseIf k = 8 Then
                                    lbl.Text = FormatNumber(WeightPer9(i), 2)
                                ElseIf k = 9 Then
                                    lbl.Text = FormatNumber(WeightPer10(i), 2)
                                End If

                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 7
                            tdInner = New TableCell
                            If dstbl.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                Title = "(cc/ " + dstbl.Tables(0).Rows(0).Item("Title15").ToString() + "/day)"
                            Else
                                Title = "(cc/100 " + dstbl.Tables(0).Rows(0).Item("Title15").ToString() + "/day)"
                            End If
                            'Title = "(" + dstbl.Tables(0).Rows(0).Item("Title2").ToString() + "/" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                            LeftTdSetting(tdInner, "OTR Sugg. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "OTRS_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If dstbl.Tables(k).Rows(0).Item("TYPEM" + i.ToString() + "").ToString() <> 0 Then
                                    If dstbl.Tables(k).Rows(0).Item("UNITS").ToString() = "0" Then
                                        If k = 0 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb.OTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 1 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb1.OTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 2 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb2.OTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 3 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb3.OTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 4 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb4.OTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 5 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb5.OTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 6 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb6.OTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 7 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb7.OTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 8 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb8.OTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 9 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb9.OTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        End If

                                    Else
                                        If k = 0 Then
                                            lbl.Text = FormatNumber(objCalb.OTRB(i - 1), 3)
                                        ElseIf k = 1 Then
                                            lbl.Text = FormatNumber(objCalb1.OTRB(i - 1), 3)
                                        ElseIf k = 2 Then
                                            lbl.Text = FormatNumber(objCalb2.OTRB(i - 1), 3)
                                        ElseIf k = 3 Then
                                            lbl.Text = FormatNumber(objCalb3.OTRB(i - 1), 3)
                                        ElseIf k = 4 Then
                                            lbl.Text = FormatNumber(objCalb4.OTRB(i - 1), 3)
                                        ElseIf k = 5 Then
                                            lbl.Text = FormatNumber(objCalb5.OTRB(i - 1), 3)
                                        ElseIf k = 6 Then
                                            lbl.Text = FormatNumber(objCalb6.OTRB(i - 1), 3)
                                        ElseIf k = 7 Then
                                            lbl.Text = FormatNumber(objCalb7.OTRB(i - 1), 3)
                                        ElseIf k = 8 Then
                                            lbl.Text = FormatNumber(objCalb8.OTRB(i - 1), 3)
                                        ElseIf k = 9 Then
                                            lbl.Text = FormatNumber(objCalb9.OTRB(i - 1), 3)
                                        End If

                                    End If
                                ElseIf dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString() <> 0 Then
                                    If dstbl.Tables(k).Rows(0).Item("UNITS").ToString() = "0" Then
                                        If k = 0 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb.OTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 1 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb1.OTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 2 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb2.OTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 3 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb3.OTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 4 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb4.OTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 5 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb5.OTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 6 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb6.OTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 7 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb7.OTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 8 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb8.OTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 9 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb9.OTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        End If

                                    Else
                                        If k = 0 Then
                                            lbl.Text = FormatNumber(objCalb.OTRB(i - 1), 3)
                                        ElseIf k = 1 Then
                                            lbl.Text = FormatNumber(objCalb1.OTRB(i - 1), 3)
                                        ElseIf k = 2 Then
                                            lbl.Text = FormatNumber(objCalb2.OTRB(i - 1), 3)
                                        ElseIf k = 3 Then
                                            lbl.Text = FormatNumber(objCalb3.OTRB(i - 1), 3)
                                        ElseIf k = 4 Then
                                            lbl.Text = FormatNumber(objCalb4.OTRB(i - 1), 3)
                                        ElseIf k = 5 Then
                                            lbl.Text = FormatNumber(objCalb5.OTRB(i - 1), 3)
                                        ElseIf k = 6 Then
                                            lbl.Text = FormatNumber(objCalb6.OTRB(i - 1), 3)
                                        ElseIf k = 7 Then
                                            lbl.Text = FormatNumber(objCalb7.OTRB(i - 1), 3)
                                        ElseIf k = 8 Then
                                            lbl.Text = FormatNumber(objCalb8.OTRB(i - 1), 3)
                                        ElseIf k = 9 Then
                                            lbl.Text = FormatNumber(objCalb9.OTRB(i - 1), 3)
                                        End If

                                    End If
                                Else
                                    lbl.Text = ""
                                End If

                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 8
                            tdInner = New TableCell
                            If dstbl.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                Title = "(cc/ " + dstbl.Tables(0).Rows(0).Item("Title15").ToString() + "/day)"
                            Else
                                Title = "(cc/100 " + dstbl.Tables(0).Rows(0).Item("Title15").ToString() + "/day)"
                            End If
                            LeftTdSetting(tdInner, "OTR Pref. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "OTRP_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If (dstbl.Tables(k).Rows(0).Item("OTRP" + i.ToString() + "").ToString() <> "") Then 'ADDED CONDITION FOR BUG #210
                                    If dstbl.Tables(k).Rows(0).Item("UNITS").ToString() = "1" Then
                                        lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("OTRP" + i.ToString() + "").ToString(), 3).ToString()
                                    Else
                                        lbl.Text = FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("OTRP" + i.ToString() + "").ToString()) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3).ToString()
                                    End If
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 9
                            tdInner = New TableCell
                            If dstbl.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                Title = "(gm/ " + dstbl.Tables(0).Rows(0).Item("Title15").ToString() + "/day)"
                            Else
                                Title = "(gm/100 " + dstbl.Tables(0).Rows(0).Item("Title15").ToString() + "/day)"
                            End If
                            'Title = "(" + dstbl.Tables(0).Rows(0).Item("Title2").ToString() + "/" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                            LeftTdSetting(tdInner, "WVTR Sugg. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "WVTRS_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If dstbl.Tables(k).Rows(0).Item("TYPEM" + i.ToString() + "").ToString() <> 0 Then
                                    If dstbl.Tables(k).Rows(0).Item("UNITS").ToString() = "0" Then
                                        If k = 0 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb.WVTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 1 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb1.WVTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 2 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb2.WVTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 3 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb3.WVTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 4 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb4.WVTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 5 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb5.WVTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 6 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb6.WVTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 7 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb7.WVTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 8 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb8.WVTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 9 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb9.WVTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        End If
                                    Else
                                        If k = 0 Then
                                            lbl.Text = FormatNumber(objCalb.WVTRB(i - 1), 3)
                                        ElseIf k = 1 Then
                                            lbl.Text = FormatNumber(objCalb1.WVTRB(i - 1), 3)
                                        ElseIf k = 2 Then
                                            lbl.Text = FormatNumber(objCalb2.WVTRB(i - 1), 3)
                                        ElseIf k = 3 Then
                                            lbl.Text = FormatNumber(objCalb3.WVTRB(i - 1), 3)
                                        ElseIf k = 4 Then
                                            lbl.Text = FormatNumber(objCalb4.WVTRB(i - 1), 3)
                                        ElseIf k = 5 Then
                                            lbl.Text = FormatNumber(objCalb5.WVTRB(i - 1), 3)
                                        ElseIf k = 6 Then
                                            lbl.Text = FormatNumber(objCalb6.WVTRB(i - 1), 3)
                                        ElseIf k = 7 Then
                                            lbl.Text = FormatNumber(objCalb7.WVTRB(i - 1), 3)
                                        ElseIf k = 8 Then
                                            lbl.Text = FormatNumber(objCalb8.WVTRB(i - 1), 3)
                                        ElseIf k = 9 Then
                                            lbl.Text = FormatNumber(objCalb9.WVTRB(i - 1), 3)
                                        End If
                                    End If
                                ElseIf dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString() <> 0 Then
                                    If dstbl.Tables(k).Rows(0).Item("UNITS").ToString() = "0" Then
                                        If k = 0 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb.WVTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 1 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb1.WVTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 2 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb2.WVTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 3 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb3.WVTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 4 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb4.WVTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 5 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb5.WVTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 6 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb6.WVTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 7 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb7.WVTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 8 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb8.WVTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        ElseIf k = 9 Then
                                            lbl.Text = FormatNumber(CDbl(objCalb9.WVTRB(i - 1)) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                        End If
                                    Else
                                        If k = 0 Then
                                            lbl.Text = FormatNumber(objCalb.WVTRB(i - 1), 3)
                                        ElseIf k = 1 Then
                                            lbl.Text = FormatNumber(objCalb1.WVTRB(i - 1), 3)
                                        ElseIf k = 2 Then
                                            lbl.Text = FormatNumber(objCalb2.WVTRB(i - 1), 3)
                                        ElseIf k = 3 Then
                                            lbl.Text = FormatNumber(objCalb3.WVTRB(i - 1), 3)
                                        ElseIf k = 4 Then
                                            lbl.Text = FormatNumber(objCalb4.WVTRB(i - 1), 3)
                                        ElseIf k = 5 Then
                                            lbl.Text = FormatNumber(objCalb5.WVTRB(i - 1), 3)
                                        ElseIf k = 6 Then
                                            lbl.Text = FormatNumber(objCalb6.WVTRB(i - 1), 3)
                                        ElseIf k = 7 Then
                                            lbl.Text = FormatNumber(objCalb7.WVTRB(i - 1), 3)
                                        ElseIf k = 8 Then
                                            lbl.Text = FormatNumber(objCalb8.WVTRB(i - 1), 3)
                                        ElseIf k = 9 Then
                                            lbl.Text = FormatNumber(objCalb9.WVTRB(i - 1), 3)
                                        End If
                                    End If
                                Else
                                    lbl.Text = ""
                                End If
                                'lbl.Text = "" ' FormatNumber(dstbl.Tables(k).Rows(0).Item("PRP" + i.ToString() + "").ToString(), 3).ToString()

                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 10
                            tdInner = New TableCell
                            If dstbl.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                Title = "(gm/ " + dstbl.Tables(0).Rows(0).Item("Title15").ToString() + "/day)"
                            Else
                                Title = "(gm/100 " + dstbl.Tables(0).Rows(0).Item("Title15").ToString() + "/day)"
                            End If
                            'Title = "(" + dstbl.Tables(0).Rows(0).Item("Title2").ToString() + "/" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                            LeftTdSetting(tdInner, "WVTR Pref. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "WVTRP_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If (dstbl.Tables(k).Rows(0).Item("WVTRP" + i.ToString() + "").ToString() <> "") Then 'ADDED CONDITION FOR BUG #210
                                    If dstbl.Tables(k).Rows(0).Item("UNITS").ToString() = "1" Then
                                        lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("WVTRP" + i.ToString() + "").ToString(), 3).ToString()
                                    Else
                                        lbl.Text = FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("WVTRP" + i.ToString() + "").ToString()) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3).ToString()
                                    End If
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 11
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE21").ToString() + ")"

                            LeftTdSetting(tdInner, "Tensile at Break MD Sugg. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "TS1S_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If dstbl.Tables(k).Rows(0).Item("TYPEM" + i.ToString() + "").ToString() <> 0 Then
                                    If dstbl.Tables(k).Rows(0).Item("UNITS").ToString() = "0" Then
                                        If k = 0 Then
                                            If objCalb.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb.TS1B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 1 Then
                                            If objCalb1.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb1.TS1B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 2 Then
                                            If objCalb2.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb2.TS1B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 3 Then
                                            If objCalb3.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb3.TS1B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 4 Then
                                            If objCalb4.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb4.TS1B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 5 Then
                                            If objCalb5.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb5.TS1B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 6 Then
                                            If objCalb6.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb6.TS1B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 7 Then
                                            If objCalb7.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb7.TS1B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 8 Then
                                            If objCalb8.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb8.TS1B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 9 Then
                                            If objCalb9.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb9.TS1B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        End If
                                    Else
                                        If k = 0 Then
                                            If objCalb.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb.TS1B(i - 1), 1)
                                            End If
                                        ElseIf k = 1 Then
                                            If objCalb1.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb1.TS1B(i - 1), 1)
                                            End If
                                        ElseIf k = 2 Then
                                            If objCalb2.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb2.TS1B(i - 1), 1)
                                            End If
                                        ElseIf k = 3 Then
                                            If objCalb3.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb3.TS1B(i - 1), 1)
                                            End If
                                        ElseIf k = 4 Then
                                            If objCalb4.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb4.TS1B(i - 1), 1)
                                            End If
                                        ElseIf k = 5 Then
                                            If objCalb5.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb5.TS1B(i - 1), 1)
                                            End If
                                        ElseIf k = 6 Then
                                            If objCalb6.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb6.TS1B(i - 1), 1)
                                            End If
                                        ElseIf k = 7 Then
                                            If objCalb7.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb7.TS1B(i - 1), 1)
                                            End If
                                        ElseIf k = 8 Then
                                            If objCalb8.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb8.TS1B(i - 1), 1)
                                            End If
                                        ElseIf k = 9 Then
                                            If objCalb9.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb9.TS1B(i - 1), 1)
                                            End If
                                        End If
                                    End If
                                ElseIf dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString() <> 0 Then
                                    If dstbl.Tables(k).Rows(0).Item("UNITS").ToString() = "0" Then
                                        If k = 0 Then
                                            If objCalb.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb.TS1B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 1 Then
                                            If objCalb1.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb1.TS1B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 2 Then
                                            If objCalb2.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb2.TS1B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 3 Then
                                            If objCalb3.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb3.TS1B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 4 Then
                                            If objCalb4.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb4.TS1B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 5 Then
                                            If objCalb5.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb5.TS1B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 6 Then
                                            If objCalb6.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb6.TS1B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 7 Then
                                            If objCalb7.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb7.TS1B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 8 Then
                                            If objCalb8.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb8.TS1B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 9 Then
                                            If objCalb9.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb9.TS1B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        End If
                                    Else
                                        If k = 0 Then
                                            If objCalb.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb.TS1B(i - 1), 1)
                                            End If
                                        ElseIf k = 1 Then
                                            If objCalb1.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb1.TS1B(i - 1), 1)
                                            End If
                                        ElseIf k = 2 Then
                                            If objCalb2.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb2.TS1B(i - 1), 1)
                                            End If
                                        ElseIf k = 3 Then
                                            If objCalb3.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb3.TS1B(i - 1), 1)
                                            End If
                                        ElseIf k = 4 Then
                                            If objCalb4.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb4.TS1B(i - 1), 1)
                                            End If
                                        ElseIf k = 5 Then
                                            If objCalb5.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb5.TS1B(i - 1), 1)
                                            End If
                                        ElseIf k = 6 Then
                                            If objCalb6.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb6.TS1B(i - 1), 1)
                                            End If
                                        ElseIf k = 7 Then
                                            If objCalb7.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb7.TS1B(i - 1), 1)
                                            End If
                                        ElseIf k = 8 Then
                                            If objCalb8.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb8.TS1B(i - 1), 1)
                                            End If
                                        ElseIf k = 9 Then
                                            If objCalb9.TS1B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb9.TS1B(i - 1), 1)
                                            End If
                                        End If
                                    End If
                                Else
                                    lbl.Text = ""
                                End If
                                'lbl.Text = "" ' FormatNumber(dstbl.Tables(k).Rows(0).Item("PRP" + i.ToString() + "").ToString(), 3).ToString()

                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 12
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE21").ToString() + ")"

                            LeftTdSetting(tdInner, "Tensile at Break MD Pref. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "TS1P_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If (dstbl.Tables(k).Rows(0).Item("TS1VAL" + i.ToString() + "").ToString() <> "") Then 'ADDED CONDITION FOR BUG #210
                                    If dstbl.Tables(k).Rows(0).Item("UNITS").ToString() = "1" Then
                                        lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("TS1VAL" + i.ToString() + "").ToString(), 1).ToString()
                                    Else
                                        lbl.Text = FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("TS1VAL" + i.ToString() + "").ToString()) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0).ToString()
                                    End If
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 13
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE21").ToString() + ")"

                            LeftTdSetting(tdInner, "Tensile at Break TD Sugg. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "TS2S_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If dstbl.Tables(k).Rows(0).Item("TYPEM" + i.ToString() + "").ToString() <> 0 Then
                                    If dstbl.Tables(k).Rows(0).Item("UNITS").ToString() = "0" Then
                                        If k = 0 Then
                                            If objCalb.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb.TS2B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 1 Then
                                            If objCalb1.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb1.TS2B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 2 Then
                                            If objCalb2.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb2.TS2B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 3 Then
                                            If objCalb3.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb3.TS2B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 4 Then
                                            If objCalb4.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb4.TS2B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 5 Then
                                            If objCalb5.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb5.TS2B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 6 Then
                                            If objCalb6.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb6.TS2B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 7 Then
                                            If objCalb7.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb7.TS2B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 8 Then
                                            If objCalb8.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb8.TS2B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 9 Then
                                            If objCalb9.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb9.TS2B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        End If
                                    Else
                                        If k = 0 Then
                                            If objCalb.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb.TS2B(i - 1), 1)
                                            End If
                                        ElseIf k = 1 Then
                                            If objCalb1.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb1.TS2B(i - 1), 1)
                                            End If
                                        ElseIf k = 2 Then
                                            If objCalb2.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb2.TS2B(i - 1), 1)
                                            End If
                                        ElseIf k = 3 Then
                                            If objCalb3.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb3.TS2B(i - 1), 1)
                                            End If
                                        ElseIf k = 4 Then
                                            If objCalb4.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb4.TS2B(i - 1), 1)
                                            End If
                                        ElseIf k = 5 Then
                                            If objCalb5.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb5.TS2B(i - 1), 1)
                                            End If
                                        ElseIf k = 6 Then
                                            If objCalb6.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb6.TS2B(i - 1), 1)
                                            End If
                                        ElseIf k = 7 Then
                                            If objCalb7.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb7.TS2B(i - 1), 1)
                                            End If
                                        ElseIf k = 8 Then
                                            If objCalb8.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb8.TS2B(i - 1), 1)
                                            End If
                                        ElseIf k = 9 Then
                                            If objCalb9.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb9.TS2B(i - 1), 1)
                                            End If
                                        End If
                                    End If
                                ElseIf dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString() <> 0 Then
                                    If dstbl.Tables(k).Rows(0).Item("UNITS").ToString() = "0" Then
                                        If k = 0 Then
                                            If objCalb.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb.TS2B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 1 Then
                                            If objCalb1.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb1.TS2B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 2 Then
                                            If objCalb2.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb2.TS2B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 3 Then
                                            If objCalb3.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb3.TS2B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 4 Then
                                            If objCalb4.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb4.TS2B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 5 Then
                                            If objCalb5.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb5.TS2B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 6 Then
                                            If objCalb6.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb6.TS2B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 7 Then
                                            If objCalb7.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb7.TS2B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 8 Then
                                            If objCalb8.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb8.TS2B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        ElseIf k = 9 Then
                                            If objCalb9.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(CDbl(objCalb9.TS2B(i - 1)) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                            End If
                                        End If
                                    Else
                                        If k = 0 Then
                                            If objCalb.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb.TS2B(i - 1), 1)
                                            End If
                                        ElseIf k = 1 Then
                                            If objCalb1.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb1.TS2B(i - 1), 1)
                                            End If
                                        ElseIf k = 2 Then
                                            If objCalb2.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb2.TS2B(i - 1), 1)
                                            End If
                                        ElseIf k = 3 Then
                                            If objCalb3.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb3.TS2B(i - 1), 1)
                                            End If
                                        ElseIf k = 4 Then
                                            If objCalb4.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb4.TS2B(i - 1), 1)
                                            End If
                                        ElseIf k = 5 Then
                                            If objCalb5.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb5.TS2B(i - 1), 1)
                                            End If
                                        ElseIf k = 6 Then
                                            If objCalb6.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb6.TS2B(i - 1), 1)
                                            End If
                                        ElseIf k = 7 Then
                                            If objCalb7.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb7.TS2B(i - 1), 1)
                                            End If
                                        ElseIf k = 8 Then
                                            If objCalb8.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb8.TS2B(i - 1), 1)
                                            End If
                                        ElseIf k = 9 Then
                                            If objCalb9.TS2B(i - 1) <> "0" Then
                                                lbl.Text = FormatNumber(objCalb9.TS2B(i - 1), 1)
                                            End If
                                        End If
                                    End If
                                Else
                                    lbl.Text = ""
                                End If
                                'lbl.Text = "" ' FormatNumber(dstbl.Tables(k).Rows(0).Item("PRP" + i.ToString() + "").ToString(), 3).ToString()

                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 14
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE21").ToString() + ")"

                            LeftTdSetting(tdInner, "Tensile at Break TD Pref. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "TS2P_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If (dstbl.Tables(k).Rows(0).Item("TS2VAL" + i.ToString() + "").ToString() <> "") Then 'ADDED CONDITION FOR BUG #210
                                    If dstbl.Tables(k).Rows(0).Item("UNITS").ToString() = "1" Then
                                        lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("TS2VAL" + i.ToString() + "").ToString(), 1).ToString()
                                    Else
                                        lbl.Text = FormatNumber(CDbl(dstbl.Tables(k).Rows(0).Item("TS2VAL" + i.ToString() + "").ToString()) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0).ToString()
                                    End If
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 15
                            tdInner = New TableCell
                            Title = ""
                            LeftTdSetting(tdInner, "Specific Gravity Sugg." + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "SGS_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If dstbl.Tables(k).Rows(0).Item("TYPEM" + i.ToString() + "").ToString() <> 0 Then
                                    lbl.Text = FormatNumber((WTPERAREAarr(k, i - 1) / ((1000 * TotalThickSub(k, i - 1) / 1000) / 1728)) / 62.4, 3)
                                Else
                                    lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("SGS" + i.ToString() + "").ToString(), 3).ToString()
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 16
                            tdInner = New TableCell
                            Title = ""
                            LeftTdSetting(tdInner, "Specific Gravity Pref." + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "SGP_" + i.ToString()

                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If dstbl.Tables(k).Rows(0).Item("TYPEM" + i.ToString() + "").ToString() <> 0 Then
                                    lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("TYPEMSG" + i.ToString() + "").ToString(), 3).ToString()
                                Else
                                    lbl.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("SGP" + i.ToString() + "").ToString(), 3).ToString()
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            Next


                    End Select
                    tblComparision.Controls.Add(trInner)
                Next
            Next

            For i = 1 To 1
                For j = 1 To 8
                    trInner = New TableRow()

                    Select Case j
                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Total</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 2
                            'If i = 1 Then
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title1").ToString() + ")"
                            LeftTdSetting(tdInner, "Total Thickness " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "TThick_" + i.ToString()

                            For k = 0 To DataCnt
                                lblTThick = New Label
                                lblTThick.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If k = 0 Then
                                    lblTThick.Text = FormatNumber(TotalThick1, 3)
                                ElseIf k = 1 Then
                                    lblTThick.Text = FormatNumber(TotalThick2, 3)
                                ElseIf k = 2 Then
                                    lblTThick.Text = FormatNumber(TotalThick3, 3)
                                ElseIf k = 3 Then
                                    lblTThick.Text = FormatNumber(TotalThick4, 3)
                                ElseIf k = 4 Then
                                    lblTThick.Text = FormatNumber(TotalThick5, 3)
                                ElseIf k = 5 Then
                                    lblTThick.Text = FormatNumber(TotalThick6, 3)
                                ElseIf k = 6 Then
                                    lblTThick.Text = FormatNumber(TotalThick7, 3)
                                ElseIf k = 7 Then
                                    lblTThick.Text = FormatNumber(TotalThick8, 3)
                                ElseIf k = 8 Then
                                    lblTThick.Text = FormatNumber(TotalThick9, 3)
                                ElseIf k = 9 Then
                                    lblTThick.Text = FormatNumber(TotalThick10, 3)
                                End If
                                tdInner.Controls.Add(lblTThick)
                                trInner.Controls.Add(tdInner)
                            Next
                            'End If
                        Case 3
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Total Weight (%)", trInner, "AlterNateColor1")
                            trInner.ID = "TWeight_" + i.ToString()

                            For k = 0 To DataCnt
                                lblTWeight = New Label
                                lblTWeight.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                If k = 0 Then
                                    lblTWeight.Text = FormatNumber(TotalWeightPer1, 3)
                                ElseIf k = 1 Then
                                    lblTWeight.Text = FormatNumber(TotalWeightPer2, 3)
                                ElseIf k = 2 Then
                                    lblTWeight.Text = FormatNumber(TotalWeightPer3, 3)
                                ElseIf k = 3 Then
                                    lblTWeight.Text = FormatNumber(TotalWeightPer4, 3)
                                ElseIf k = 4 Then
                                    lblTWeight.Text = FormatNumber(TotalWeightPer5, 3)
                                ElseIf k = 5 Then
                                    lblTWeight.Text = FormatNumber(TotalWeightPer6, 3)
                                ElseIf k = 6 Then
                                    lblTWeight.Text = FormatNumber(TotalWeightPer7, 3)
                                ElseIf k = 7 Then
                                    lblTWeight.Text = FormatNumber(TotalWeightPer8, 3)
                                ElseIf k = 8 Then
                                    lblTWeight.Text = FormatNumber(TotalWeightPer9, 3)
                                ElseIf k = 9 Then
                                    lblTWeight.Text = FormatNumber(TotalWeightPer10, 3)
                                End If
                                tdInner.Controls.Add(lblTWeight)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 4
                            tdInner = New TableCell
                            If dstbl.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                Title = "(cc/ " + dstbl.Tables(0).Rows(0).Item("Title15").ToString() + "/day)"
                            Else
                                Title = "(cc/100 " + dstbl.Tables(0).Rows(0).Item("Title15").ToString() + "/day)"
                            End If
                            'Title = "(" + dstbl.Tables(0).Rows(0).Item("Title1").ToString() + ")"
                            LeftTdSetting(tdInner, "Total OTR " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "TOTR_" + i.ToString()

                            For k = 0 To DataCnt
                                lblTOTR = New Label
                                lblTOTR.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")

                                If dstbl.Tables(k).Rows(0).Item("UNITS").ToString() = "0" Then
                                    If k = 0 Then
                                        lblTOTR.Text = FormatNumber(CDbl(objCalb.TotalOTRB) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                    ElseIf k = 1 Then
                                        lblTOTR.Text = FormatNumber(CDbl(objCalb1.TotalOTRB) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                    ElseIf k = 2 Then
                                        lblTOTR.Text = FormatNumber(CDbl(objCalb2.TotalOTRB) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                    ElseIf k = 3 Then
                                        lblTOTR.Text = FormatNumber(CDbl(objCalb3.TotalOTRB) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                    ElseIf k = 4 Then
                                        lblTOTR.Text = FormatNumber(CDbl(objCalb4.TotalOTRB) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                    ElseIf k = 5 Then
                                        lblTOTR.Text = FormatNumber(CDbl(objCalb5.TotalOTRB) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                    ElseIf k = 6 Then
                                        lblTOTR.Text = FormatNumber(CDbl(objCalb6.TotalOTRB) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                    ElseIf k = 7 Then
                                        lblTOTR.Text = FormatNumber(CDbl(objCalb7.TotalOTRB) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                    ElseIf k = 8 Then
                                        lblTOTR.Text = FormatNumber(CDbl(objCalb8.TotalOTRB) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                    ElseIf k = 9 Then
                                        lblTOTR.Text = FormatNumber(CDbl(objCalb9.TotalOTRB) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                    End If

                                Else
                                    If k = 0 Then
                                        lblTOTR.Text = FormatNumber(objCalb.TotalOTRB, 3)
                                    ElseIf k = 1 Then
                                        lblTOTR.Text = FormatNumber(objCalb1.TotalOTRB, 3)
                                    ElseIf k = 2 Then
                                        lblTOTR.Text = FormatNumber(objCalb2.TotalOTRB, 3)
                                    ElseIf k = 3 Then
                                        lblTOTR.Text = FormatNumber(objCalb3.TotalOTRB, 3)
                                    ElseIf k = 4 Then
                                        lblTOTR.Text = FormatNumber(objCalb4.TotalOTRB, 3)
                                    ElseIf k = 5 Then
                                        lblTOTR.Text = FormatNumber(objCalb5.TotalOTRB, 3)
                                    ElseIf k = 6 Then
                                        lblTOTR.Text = FormatNumber(objCalb6.TotalOTRB, 3)
                                    ElseIf k = 7 Then
                                        lblTOTR.Text = FormatNumber(objCalb7.TotalOTRB, 3)
                                    ElseIf k = 8 Then
                                        lblTOTR.Text = FormatNumber(objCalb8.TotalOTRB, 3)
                                    ElseIf k = 9 Then
                                        lblTOTR.Text = FormatNumber(objCalb9.TotalOTRB, 3)
                                    End If

                                End If

                                tdInner.Controls.Add(lblTOTR)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 5
                            tdInner = New TableCell
                            If dstbl.Tables(0).Rows(0).Item("UNITS").ToString() = "1" Then
                                Title = "(gm/ " + dstbl.Tables(0).Rows(0).Item("Title15").ToString() + "/day)"
                            Else
                                Title = "(gm/100 " + dstbl.Tables(0).Rows(0).Item("Title15").ToString() + "/day)"
                            End If
                            'Title = "(" + dstbl.Tables(0).Rows(0).Item("Title1").ToString() + ")"
                            LeftTdSetting(tdInner, "Total WVTR " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "TWVTR_" + i.ToString()

                            For k = 0 To DataCnt
                                lblTWVTR = New Label
                                lblTWVTR.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")


                                If dstbl.Tables(k).Rows(0).Item("UNITS").ToString() = "0" Then
                                    If k = 0 Then
                                        lblTWVTR.Text = FormatNumber(CDbl(objCalb.TotalWVTRB) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                    ElseIf k = 1 Then
                                        lblTWVTR.Text = FormatNumber(CDbl(objCalb1.TotalWVTRB) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                    ElseIf k = 2 Then
                                        lblTWVTR.Text = FormatNumber(CDbl(objCalb2.TotalWVTRB) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                    ElseIf k = 3 Then
                                        lblTWVTR.Text = FormatNumber(CDbl(objCalb3.TotalWVTRB) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                    ElseIf k = 4 Then
                                        lblTWVTR.Text = FormatNumber(CDbl(objCalb4.TotalWVTRB) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                    ElseIf k = 5 Then
                                        lblTWVTR.Text = FormatNumber(CDbl(objCalb5.TotalWVTRB) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                    ElseIf k = 6 Then
                                        lblTWVTR.Text = FormatNumber(CDbl(objCalb6.TotalWVTRB) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                    ElseIf k = 7 Then
                                        lblTWVTR.Text = FormatNumber(CDbl(objCalb7.TotalWVTRB) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                    ElseIf k = 8 Then
                                        lblTWVTR.Text = FormatNumber(CDbl(objCalb8.TotalWVTRB) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                    ElseIf k = 9 Then
                                        lblTWVTR.Text = FormatNumber(CDbl(objCalb9.TotalWVTRB) * (CDbl(dstbl.Tables(k).Rows(0).Item("CONVAREA").ToString()) / 1000) * 100, 3)
                                    End If

                                Else
                                    If k = 0 Then
                                        lblTWVTR.Text = FormatNumber(objCalb.TotalWVTRB, 3)
                                    ElseIf k = 1 Then
                                        lblTWVTR.Text = FormatNumber(objCalb1.TotalWVTRB, 3)
                                    ElseIf k = 2 Then
                                        lblTWVTR.Text = FormatNumber(objCalb2.TotalWVTRB, 3)
                                    ElseIf k = 3 Then
                                        lblTWVTR.Text = FormatNumber(objCalb3.TotalWVTRB, 3)
                                    ElseIf k = 4 Then
                                        lblTWVTR.Text = FormatNumber(objCalb4.TotalWVTRB, 3)
                                    ElseIf k = 5 Then
                                        lblTWVTR.Text = FormatNumber(objCalb5.TotalWVTRB, 3)
                                    ElseIf k = 6 Then
                                        lblTWVTR.Text = FormatNumber(objCalb6.TotalWVTRB, 3)
                                    ElseIf k = 7 Then
                                        lblTWVTR.Text = FormatNumber(objCalb7.TotalWVTRB, 3)
                                    ElseIf k = 8 Then
                                        lblTWVTR.Text = FormatNumber(objCalb8.TotalWVTRB, 3)
                                    ElseIf k = 9 Then
                                        lblTWVTR.Text = FormatNumber(objCalb9.TotalWVTRB, 3)
                                    End If

                                End If
                                tdInner.Controls.Add(lblTWVTR)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 6
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE21").ToString() + ")"

                            'Title = "(" + dstbl.Tables(0).Rows(0).Item("Title1").ToString() + ")"
                            LeftTdSetting(tdInner, "Total Tensile at Break MD " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "TTS1_" + i.ToString()

                            For k = 0 To DataCnt
                                lblTTS1 = New Label
                                lblTTS1.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")


                                If dstbl.Tables(k).Rows(0).Item("UNITS").ToString() = "0" Then
                                    If k = 0 Then
                                        lblTTS1.Text = FormatNumber(CDbl(objCalb.TotalTS1B) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                    ElseIf k = 1 Then
                                        lblTTS1.Text = FormatNumber(CDbl(objCalb1.TotalTS1B) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                    ElseIf k = 2 Then
                                        lblTTS1.Text = FormatNumber(CDbl(objCalb2.TotalTS1B) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                    ElseIf k = 3 Then
                                        lblTTS1.Text = FormatNumber(CDbl(objCalb3.TotalTS1B) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                    ElseIf k = 4 Then
                                        lblTTS1.Text = FormatNumber(CDbl(objCalb4.TotalTS1B) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                    ElseIf k = 5 Then
                                        lblTTS1.Text = FormatNumber(CDbl(objCalb5.TotalTS1B) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                    ElseIf k = 6 Then
                                        lblTTS1.Text = FormatNumber(CDbl(objCalb6.TotalTS1B) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                    ElseIf k = 7 Then
                                        lblTTS1.Text = FormatNumber(CDbl(objCalb7.TotalTS1B) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                    ElseIf k = 8 Then
                                        lblTTS1.Text = FormatNumber(CDbl(objCalb8.TotalTS1B) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                    ElseIf k = 9 Then
                                        lblTTS1.Text = FormatNumber(CDbl(objCalb9.TotalTS1B) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                    End If

                                Else
                                    If k = 0 Then
                                        lblTTS1.Text = FormatNumber(objCalb.TotalTS1B, 1)
                                    ElseIf k = 1 Then
                                        lblTTS1.Text = FormatNumber(objCalb1.TotalTS1B, 1)
                                    ElseIf k = 2 Then
                                        lblTTS1.Text = FormatNumber(objCalb2.TotalTS1B, 1)
                                    ElseIf k = 3 Then
                                        lblTTS1.Text = FormatNumber(objCalb3.TotalTS1B, 1)
                                    ElseIf k = 4 Then
                                        lblTTS1.Text = FormatNumber(objCalb4.TotalTS1B, 1)
                                    ElseIf k = 5 Then
                                        lblTTS1.Text = FormatNumber(objCalb5.TotalTS1B, 1)
                                    ElseIf k = 6 Then
                                        lblTTS1.Text = FormatNumber(objCalb6.TotalTS1B, 1)
                                    ElseIf k = 7 Then
                                        lblTTS1.Text = FormatNumber(objCalb7.TotalTS1B, 1)
                                    ElseIf k = 8 Then
                                        lblTTS1.Text = FormatNumber(objCalb8.TotalTS1B, 1)
                                    ElseIf k = 9 Then
                                        lblTTS1.Text = FormatNumber(objCalb9.TotalTS1B, 1)
                                    End If

                                End If
                                tdInner.Controls.Add(lblTTS1)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 7
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE21").ToString() + ")"

                            LeftTdSetting(tdInner, "Total Tensile at Break TD " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "TTS2_" + i.ToString()

                            For k = 0 To DataCnt
                                lblTTS2 = New Label
                                lblTTS2.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")


                                If dstbl.Tables(k).Rows(0).Item("UNITS").ToString() = "0" Then
                                    If k = 0 Then
                                        lblTTS2.Text = FormatNumber(CDbl(objCalb.TotalTS2B) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                    ElseIf k = 1 Then
                                        lblTTS2.Text = FormatNumber(CDbl(objCalb1.TotalTS2B) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                    ElseIf k = 2 Then
                                        lblTTS2.Text = FormatNumber(CDbl(objCalb2.TotalTS2B) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                    ElseIf k = 3 Then
                                        lblTTS2.Text = FormatNumber(CDbl(objCalb3.TotalTS2B) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                    ElseIf k = 4 Then
                                        lblTTS2.Text = FormatNumber(CDbl(objCalb4.TotalTS2B) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                    ElseIf k = 5 Then
                                        lblTTS2.Text = FormatNumber(CDbl(objCalb5.TotalTS2B) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                    ElseIf k = 6 Then
                                        lblTTS2.Text = FormatNumber(CDbl(objCalb6.TotalTS2B) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                    ElseIf k = 7 Then
                                        lblTTS2.Text = FormatNumber(CDbl(objCalb7.TotalTS2B) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                    ElseIf k = 8 Then
                                        lblTTS2.Text = FormatNumber(CDbl(objCalb8.TotalTS2B) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                    ElseIf k = 9 Then
                                        lblTTS2.Text = FormatNumber(CDbl(objCalb9.TotalTS2B) / CDbl(dstbl.Tables(k).Rows(0).Item("CONVPA").ToString()), 0)
                                    End If

                                Else
                                    If k = 0 Then
                                        lblTTS2.Text = FormatNumber(objCalb.TotalTS2B, 1)
                                    ElseIf k = 1 Then
                                        lblTTS2.Text = FormatNumber(objCalb1.TotalTS2B, 1)
                                    ElseIf k = 2 Then
                                        lblTTS2.Text = FormatNumber(objCalb2.TotalTS2B, 1)
                                    ElseIf k = 3 Then
                                        lblTTS2.Text = FormatNumber(objCalb3.TotalTS2B, 1)
                                    ElseIf k = 4 Then
                                        lblTTS2.Text = FormatNumber(objCalb4.TotalTS2B, 1)
                                    ElseIf k = 5 Then
                                        lblTTS2.Text = FormatNumber(objCalb5.TotalTS2B, 1)
                                    ElseIf k = 6 Then
                                        lblTTS2.Text = FormatNumber(objCalb6.TotalTS2B, 1)
                                    ElseIf k = 7 Then
                                        lblTTS2.Text = FormatNumber(objCalb7.TotalTS2B, 1)
                                    ElseIf k = 8 Then
                                        lblTTS2.Text = FormatNumber(objCalb8.TotalTS2B, 1)
                                    ElseIf k = 9 Then
                                        lblTTS2.Text = FormatNumber(objCalb9.TotalTS2B, 1)
                                    End If

                                End If
                                tdInner.Controls.Add(lblTTS2)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 8
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Total Specific Gravity " + "", trInner, "AlterNateColor1")
                            trInner.ID = "TSG_" + i.ToString()
                            For k = 0 To DataCnt
                                lblTSg = New Label
                                lblTSg.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                lblTSg.Text = FormatNumber(TotalSg(k), 3)
                                tdInner.Controls.Add(lblTSg)
                                trInner.Controls.Add(tdInner)
                            Next
                    End Select
                    tblComparision.Controls.Add(trInner)
                Next
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
            Td.Height = 30
            Td.HorizontalAlign = HorizontalAlign.Center
        Catch ex As Exception

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

        End Try
    End Sub

    Protected Sub TextBoxSetting(ByVal txt As TextBox, ByVal Css As String, ByVal CaseId As Integer)
        Try
            txt.CssClass = Css
            If CaseId <= 1000 And Session("Password") <> "9krh65sve3" Then
                txt.Enabled = False
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub LableSetting(ByVal lbl As Label, ByVal Css As String)
        Try
            lbl.CssClass = Css

        Catch ex As Exception

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

    Protected Sub GetMaterialDetails(ByRef lbl As Label, ByVal MatId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New StandGetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty

        Try
            If MatId <> 0 Then
                Ds = ObjGetdata.GetMaterials(MatId)
                If Ds.Tables(0).Rows(0).Item("CATEGORY").ToString().Length > 25 Then
                    'LinkMat.Font.Size = 8
                End If
                If MatId > 500 And MatId < 506 Then
                    lbl.Text = Ds.Tables(0).Rows(0).Item("GRADEDES").ToString()
                    lbl.ToolTip = Ds.Tables(0).Rows(0).Item("GRADEDES").ToString()
                Else
                    lbl.Text = Ds.Tables(0).Rows(0).Item("CATEGORY").ToString()
                    lbl.ToolTip = Ds.Tables(0).Rows(0).Item("CATEGORY").ToString()
                End If
            Else
                lbl.Text = "Nothing Selected"
                lbl.ToolTip = "Nothing Selected"
            End If

        Catch ex As Exception
            ErrorLable.Text = "Error:GetMaterialDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub GetGradeDetails(ByRef lbl As Label, ByVal MatId As Integer)
        Dim Ds As New DataSet
        Dim ObjGetdata As New StandGetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty

        Try
            If MatId <> 0 Then
                Ds = ObjGetdata.GetMaterials(MatId)
                If Ds.Tables(0).Rows(0).Item("GRADEDES").ToString().Length > 25 Then
                    'LinkMat.Font.Size = 8
                End If
                If MatId > 500 And MatId < 506 Then
                    lbl.Text = ""
                    lbl.ToolTip = ""
                Else
                    lbl.Text = Ds.Tables(0).Rows(0).Item("GRADEDES").ToString()
                    lbl.ToolTip = Ds.Tables(0).Rows(0).Item("GRADEDES").ToString()
                End If
            Else
                lbl.Text = "Nothing Selected"
                lbl.ToolTip = "Nothing Selected"
            End If

        Catch ex As Exception
            ErrorLable.Text = "Error:GetGradeDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

End Class
