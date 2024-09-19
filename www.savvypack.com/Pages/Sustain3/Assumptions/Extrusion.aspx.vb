Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S3GetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain3_Assumptions_Extrusion
    Inherits System.Web.UI.Page

#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _strUserName As String
    Dim _strPassword As String
    Dim _iAssumptionId As Integer


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


    Public DataCnt As Integer
    Public CaseDesp As New ArrayList

#End Region

#Region "MastePage Content Variables"
    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Try
                GetErrorLable()
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
            AssumptionId = Session("AssumptionId")

            lblAID.Text = AssumptionId
            lblAdes.Text = Session("Description")

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetSessionDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Function GetCaseIds() As String()
        Dim CaseIds(0) As String
        Dim objGetData As New S3GetData.Selectdata

        Try
            CaseIds = objGetData.Cases1(AssumptionId)
            Return CaseIds
        Catch ex As Exception
            Return CaseIds
            _lErrorLble.Text = "Error:GetCaseIds:" + ex.Message.ToString()
        End Try
        Return CaseIds
    End Function

    Protected Sub GetPageDetails()
        Dim ds As New DataSet
        Dim dstbl As New DataSet
        Dim dsCaseDetails As New DataSet
        Dim objGetData As New S1GetData.Selectdata
        Dim CaseIds As String = String.Empty
        'Dim DataCnt As New Integer
        Dim i As New Integer
        Dim j As New Integer
        Dim l As New Integer
        Dim DWidth As String = String.Empty
        Dim arrCaseID() As String
        Dim dv As New DataView
        Dim dt As New DataTable
        Dim dsEmattbl As New DataSet
        Dim dsMat1 As New DataSet
        Try
            dsMat1 = objGetData.GetMaterials("-1", "", "")

            arrCaseID = GetCaseIds()
            DataCnt = arrCaseID.Length - 1

            Dim trHeader As New TableRow
            Dim tdHeader As TableCell
            Dim tdInner As TableCell
            Dim trInner As New TableRow
            Dim ddl As DropDownList


            trInner = New TableRow()
            Dim k As Integer
            Dim lbl As New Label
            Dim Cunits As New Integer
            Dim Units As New Integer
            Dim Title As String = String.Empty
            DWidth = txtDWidth.Text + "px"


            tdHeader = New TableCell
            HeaderTdSetting(tdHeader, DWidth, "<img alt='' src='../../Images/spacer.gif' Style='width:200px;height:0px;'  />", 1)
            trHeader.Controls.Add(tdHeader)
            trHeader.Height = 20
            trHeader.CssClass = "PageSSHeading"

            For i = 0 To DataCnt
                'Dim ds As New DataSet
                Dim dsEmat As New DataSet
                ds = objGetData.GetExtrusionDetails(arrCaseID(i))
                ds.Tables(0).TableName = arrCaseID(i).ToString()
                dstbl.Tables.Add(ds.Tables(arrCaseID(i).ToString()).Copy())

                dsEmat = objGetData.GetEditMaterial(arrCaseID(i))
                dsEmat.Tables(0).TableName = arrCaseID(i).ToString()
                dsEmattbl.Tables.Add(dsEmat.Tables(arrCaseID(i).ToString()).Copy())
            Next


            For i = 0 To DataCnt
                dsCaseDetails = objGetData.GetCaseDetails(arrCaseID(i).ToString())
                Cunits = Convert.ToInt32(dstbl.Tables(0).Rows(0).Item("Units").ToString())
                Units = Convert.ToInt32(dstbl.Tables(i).Rows(0).Item("Units").ToString())


                tdHeader = New TableCell
                Dim Headertext As String = String.Empty
                If Cunits <> Units Then
                    Headertext = "Case#:" + arrCaseID(i).ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + "<br/> <span  style='color:red'>Unit Mismatch</span>" + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"
                Else
                    Headertext = "Case#:" + arrCaseID(i).ToString() + "<br/>" + dsCaseDetails.Tables(0).Rows(0).Item("CaseDes").ToString() + "<input type='hidden' value='" + arrCaseID(i).ToString() + "' name='Case" + i.ToString() + "'/>"
                End If
                CaseDesp.Add(arrCaseID(i).ToString())
                HeaderTdSetting(tdHeader, DWidth, Headertext, 1)
                trHeader.Controls.Add(tdHeader)


            Next
            tblComparision.Controls.Add(trHeader)

            For i = 1 To 10
                For j = 1 To 25
                    trInner = New TableRow()

                    Select Case j

                        Case 1
                            tdInner = New TableCell
                            'LeftTdSetting(tdInner, "<b>Layer" + i.ToString() + "</b>", trBreak, "AlterNateColor3")
                            LeftTdSetting(tdInner, "<b>Layer" + i.ToString() + "</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                'trBreak.Controls.Add(tdInner)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 2
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Material", trInner, "AlterNateColor1")
                            trInner.ID = "M_" + i.ToString()
                            Dim dsMat As New DataSet
                            For k = 0 To DataCnt
                                lbl = New Label
                                lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Right")
                                dv = dsEmattbl.Tables(k).DefaultView
                                If dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString() <> "0" Then
                                    dv.RowFilter = "MATID=" + dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString()
                                    dt = dv.ToTable()
                                    If dt.Rows.Count > 0 Then
                                        lbl.Text = dt.Rows(0).Item("MATDES").ToString().Replace("&#", "'")
                                    Else
                                        'lbl.Text = dstbl.Tables(k).Rows(0).Item("MATS" + i.ToString() + "").ToString()
                                        GetMaterialDetails(lbl, dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString(), dsMat1)
                                    End If

                                Else
                                    'lbl.Text = dsEmattbl.Tables(k).Rows(0).Item("MATS" + i.ToString() + "").ToString()
                                    GetMaterialDetails(lbl, dstbl.Tables(k).Rows(0).Item("M" + i.ToString() + "").ToString(), dsMat1)
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)

                            Next

                        Case 3

                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title1").ToString() + ")"
                            'LeftTdSetting(tdInner, "Thickness" + Title + "", trThcikNess, "AlterNateColor2")
                            LeftTdSetting(tdInner, "Thickness " + Title + "", trInner, "AlterNateColor2")
                            'trThcikNess.ID = "T_" + i.ToString()
                            trInner.ID = "T_" + i.ToString()
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("THICK" + i.ToString() + "").ToString(), 3)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                ' trThcikNess.Controls.Add(tdInner)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 4

                            tdInner = New TableCell
                            Title = "(%)"
                            'LeftTdSetting(tdInner, "Recycle" + Title + "", trRecycle, "AlterNateColor1")
                            LeftTdSetting(tdInner, "Recycle " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "RE_" + i.ToString()

                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("R" + i.ToString() + "").ToString(), 2)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                ' trRecycle.Controls.Add(tdInner)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 5

                            tdInner = New TableCell
                            Title = "(MJ/" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + " mat)"
                            'LeftTdSetting(tdInner, "Energy Sugg." + Title + "", trEnergyS, "AlterNateColor2")
                            LeftTdSetting(tdInner, "Energy Sugg. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "ES_" + i.ToString()

                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, DWidth, "Right")
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("ERGYS" + i.ToString() + "").ToString(), 1)
                                'trEnergyS.Controls.Add(tdInner)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 6
                            tdInner = New TableCell
                            Title = "(MJ/" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + " mat)"
                            ' LeftTdSetting(tdInner, "Energy Pref." + Title + "", trEnergyP, "AlterNateColor1")
                            LeftTdSetting(tdInner, "Energy Pref. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "EP_" + i.ToString()

                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("ERGYP" + i.ToString() + "").ToString(), 3)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                'trEnergyP.Controls.Add(tdInner)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 7

                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + " CO2/" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + " mat)"
                            'LeftTdSetting(tdInner, "CO2 Sugg." + Title + "", trGHGS, "AlterNateColor2")
                            LeftTdSetting(tdInner, "CO2 Equivalent Sugg. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "GHGS_" + i.ToString()

                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, DWidth, "Right")
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("CO2S" + i.ToString() + "").ToString(), 3)
                                'trGHGS.Controls.Add(tdInner)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 8

                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + " CO2/" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + " mat)"
                            ' LeftTdSetting(tdInner, "CO2 Pref." + Title + "", trGHGP, "AlterNateColor1")
                            LeftTdSetting(tdInner, "CO2 Equivalent Pref. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "GHGP_" + i.ToString()

                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("CO2P" + i.ToString() + "").ToString(), 3)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                ' trGHGP.Controls.Add(tdInner)
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 9

                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title10").ToString() + " Water/" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + " mat)"
                            'LeftTdSetting(tdInner, "CO2 Sugg." + Title + "", trGHGS, "AlterNateColor2")
                            LeftTdSetting(tdInner, "Water Sugg. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "WaterS_" + i.ToString()

                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, DWidth, "Right")
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("WATERS" + i.ToString() + "").ToString(), 3)
                                'trGHGS.Controls.Add(tdInner)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 10

                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title10").ToString() + " Water/" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + " mat)"
                            ' LeftTdSetting(tdInner, "CO2 Pref." + Title + "", trGHGP, "AlterNateColor1")
                            LeftTdSetting(tdInner, "Water Pref. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "WaterP_" + i.ToString()

                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("WATERP" + i.ToString() + "").ToString(), 3)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                ' trGHGP.Controls.Add(tdInner)
                                trInner.Controls.Add(tdInner)
                            Next


                        Case 11
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title4").ToString() + ")"
                            ' LeftTdSetting(tdInner, "Ship Distance Sugg." + Title + "", trShipS, "AlterNateColor2")
                            LeftTdSetting(tdInner, "Ship Distance Sugg. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "SS_" + i.ToString()

                            For k = 0 To DataCnt
                                'Ship Distance Sugg
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, DWidth, "Right")
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("SHIPS" + i.ToString() + "").ToString(), 0)
                                trInner.Controls.Add(tdInner)

                            Next

                        Case 12
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title4").ToString() + ")"
                            ' LeftTdSetting(tdInner, "Ship Distance Pref." + Title + "", trShipP, "AlterNateColor1")
                            LeftTdSetting(tdInner, "Ship Distance Pref. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "SP_" + i.ToString()

                            For k = 0 To DataCnt
                                'Ship Distance Preff
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("SHIPP" + i.ToString() + "").ToString(), 3)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next


                        Case 13
                            tdInner = New TableCell
                            Title = "(%)"
                            '  LeftTdSetting(tdInner, "Extra-process" + Title + "", trEP, "AlterNateColor2")
                            LeftTdSetting(tdInner, "Extra-process " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "E_" + i.ToString()
                            For k = 0 To DataCnt
                                'Extra-process 
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("E" + i.ToString() + "").ToString(), 2)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 14
                            tdInner = New TableCell
                            Title = ""
                            'LeftTdSetting(tdInner, "Specific Gravity Sugg." + Title + "", trSGS, "AlterNateColor1")
                            LeftTdSetting(tdInner, "Specific Gravity Sugg. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "SGS_" + i.ToString()
                            For k = 0 To DataCnt
                                'SG Sugg
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, DWidth, "Right")
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("SGS" + i.ToString() + "").ToString(), 2)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 15
                            tdInner = New TableCell
                            Title = ""
                            ' LeftTdSetting(tdInner, "Specific Gravity Preff." + Title + "", trSGP, "AlterNateColor2")
                            LeftTdSetting(tdInner, "Specific Gravity Pref. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "SGP_" + i.ToString()
                            For k = 0 To DataCnt
                                'SG Preff
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("SGP" + i.ToString() + "").ToString(), 3)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 16
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                            'LeftTdSetting(tdInner, "Weight/area " + Title + "", trWPA, "AlterNateColor1")
                            LeftTdSetting(tdInner, "Weight/Area " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "W_" + i.ToString()
                            For k = 0 To DataCnt
                                'Weight Per Area
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("WTPARA" + i.ToString() + "").ToString(), 3)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 17
                            tdInner = New TableCell
                            'Title = "(" + ds.Tables(0).Rows(0).Item("TITLE8").ToString() + ") per Shipping unit"
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("Title8").ToString() + ")"
                            ' LeftTdSetting(tdInner, "" + Title + " Per Shipping unit", trPerShip, "AlterNateColor2")
                            LeftTdSetting(tdInner, "" + Title + " Per Shipping unit", trInner, "AlterNateColor2")
                            trInner.ID = "PSP_" + i.ToString()

                            For k = 0 To DataCnt
                                'Per Shipping Distance
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("SHIPUNIT" + i.ToString() + "").ToString(), 0)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 18
                            tdInner = New TableCell
                            Dim dsMat As New DataSet
                            LeftTdSetting(tdInner, "Shipping Selector", trInner, "AlterNateColor1")
                            'trMaterial.ID = "SS_" + i.ToString()
                            trInner.ID = "SSC_" + i.ToString()
                            For k = 0 To DataCnt
                                'lbl = New Label
                                'lbl.Style.Add("Width", DWidth)
                                tdInner = New TableCell
                                dsMat = objGetData.GetShipingSelector(dstbl.Tables(k).Rows(0).Item("SS" + i.ToString() + "").ToString(), "")
                                tdInner.Text = dsMat.Tables(0).Rows(0).Item("shipdes").ToString()
                                'tdInner.Controls.Add(lbl)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next


                        Case 19
                            tdInner = New TableCell
                            Dim dsMat As New DataSet
                            Title = ""
                            'LeftTdSetting(tdInner, "Mfg. Department" + Title + "", trDept, "AlterNateColor1")
                            LeftTdSetting(tdInner, "Mfg. Department" + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "D_" + i.ToString()

                            For k = 0 To DataCnt
                                'Mfg. Department
                                tdInner = New TableCell
                                dsMat = objGetData.GetDept(dstbl.Tables(k).Rows(0).Item("D" + i.ToString() + "").ToString(), "", "", arrCaseID(k))
                                If dsMat.Tables(0).Rows.Count > 0 Then
                                    tdInner.Text = dsMat.Tables(0).Rows(0).Item("PROCDE").ToString()
                                Else
                                    tdInner.Text = "Dept. Conflict"
                                End If
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 20

                            tdInner = New TableCell
                            Title = "(Unitless)"
                            ' LeftTdSetting(tdInner, "Recovery Sugg." + Title + "", trRecS, "AlterNateColor2")
                            LeftTdSetting(tdInner, "Recovery Sugg. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "RECS_" + i.ToString()
                            For k = 0 To DataCnt
                                'Recovery Sugg
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, DWidth, "Right")
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("RECOS" + i.ToString() + "").ToString(), 2)
                                trInner.Controls.Add(tdInner)
                            Next


                        Case 21
                            tdInner = New TableCell
                            Title = "(Unitless)"
                            'LeftTdSetting(tdInner, "Recovery Pref." + Title + "", trRecP, "AlterNateColor1")
                            LeftTdSetting(tdInner, "Recovery Pref. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "RECP_" + i.ToString()

                            For k = 0 To DataCnt
                                'Recovery Preff
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, DWidth, "Right")
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("RECOP" + i.ToString() + "").ToString(), 2)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 22

                            tdInner = New TableCell
                            Title = "(Unitless)"
                            'LeftTdSetting(tdInner, "Sustainable Materials Sugg." + Title + "", trSustainS, "AlterNateColor2")
                            LeftTdSetting(tdInner, "Sustainable Materials Sugg. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "SUS_" + i.ToString()

                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, DWidth, "Right")
                                tdInner.Text = dstbl.Tables(k).Rows(0).Item("SUSS" + i.ToString() + "").ToString()
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 23
                            tdInner = New TableCell
                            Title = "(Unitless)"
                            'LeftTdSetting(tdInner, "Sustainable Materials Pref." + Title + "", trSustainP, "AlterNateColor1")
                            LeftTdSetting(tdInner, "Sustainable Materials Pref. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "SUP_" + i.ToString()
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                tdInner.Text = dstbl.Tables(k).Rows(0).Item("SUSP" + i.ToString() + "").ToString()
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 24
                            tdInner = New TableCell
                            Title = "(Unitless)"
                            'LeftTdSetting(tdInner, "PC Recycle Sugg." + Title + "", trPCS, "AlterNateColor2")
                            LeftTdSetting(tdInner, "PC Recycle Sugg. " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "PCS_" + i.ToString()

                            For k = 0 To DataCnt
                                'PC Recycle Sugg
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, DWidth, "Right")
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("PCRECS" + i.ToString() + "").ToString(), 2)
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 25
                            tdInner = New TableCell
                            Title = "(Unitless)"
                            'LeftTdSetting(tdInner, "PC Recycle Pref." + Title + "", trPCP, "AlterNateColor1")
                            LeftTdSetting(tdInner, "PC Recycle Pref. " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "PCP_" + i.ToString()

                            For k = 0 To DataCnt
                                'PC Recycle Preff
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("PCRECP" + i.ToString() + "").ToString(), 2)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                    End Select
                    tblComparision.Controls.Add(trInner)
                Next
            Next


            For i = 1 To 3
                For j = 1 To 5
                    trInner = New TableRow
                    ' tdInner = New TableCell
                    Select Case j

                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b>Discrete Materials" + i.ToString() + "</b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 2
                            tdInner = New TableCell
                            Dim dsMat As New DataSet
                            LeftTdSetting(tdInner, "Material", trInner, "AlterNateColor2")
                            trInner.ID = "PCMM_" + i.ToString()
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                dsMat = objGetData.GetDiscMaterials(CInt(dstbl.Tables(k).Rows(0).Item("DISID" + i.ToString() + "").ToString()), "")
                                tdInner.Text = dsMat.Tables(0).Rows(0).Item("matDISde1").ToString()
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 3
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE8").ToString() + ")"
                            'tdInner.Text = "<b> Include weight of discrete materials in P&L statements </b>"
                            LeftTdSetting(tdInner, "Weight " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "PCML_" + i.ToString()
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("DISWEIGHT" + i.ToString() + "").ToString(), 4)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 4
                            tdInner = New TableCell
                            Title = "(MJ/" + dstbl.Tables(0).Rows(0).Item("TITLE8").ToString() + " mat)"
                            'tdInner.Text = "<b> Include weight of discrete materials in P&L statements </b>"
                            LeftTdSetting(tdInner, "Energy " + Title + "", trInner, "AlterNateColor2")
                            trInner.ID = "PCMK_" + i.ToString()
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("DISERGY" + i.ToString() + "").ToString(), 4)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)

                            Next

                        Case 5
                            tdInner = New TableCell
                            Title = "(" + dstbl.Tables(0).Rows(0).Item("TITLE8").ToString() + " CO2/" + dstbl.Tables(0).Rows(0).Item("TITLE8").ToString() + " mat)"
                            'tdInner.Text = "<b> Include weight of discrete materials in P&L statements </b>"
                            LeftTdSetting(tdInner, "CO2 Equivalent " + Title + "", trInner, "AlterNateColor1")
                            trInner.ID = "PCMN_" + i.ToString()
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                tdInner.Text = FormatNumber(dstbl.Tables(k).Rows(0).Item("DISCO" + i.ToString() + "").ToString(), 4)
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next
                    End Select
                    tblComparision.Controls.Add(trInner)
                Next
            Next

            For i = 1 To 1
                For j = 1 To 3
                    trInner = New TableRow()
                    Select Case j
                        Case 1
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "<b></b>", trInner, "AlterNateColor4")
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                InnerTdSetting(tdInner, "", "Center")
                                tdInner.Text = "&nbsp;"
                                trInner.Controls.Add(tdInner)
                            Next
                        Case 2

                            tdInner = New TableCell
                            tdInner.Width = 170
                            LeftTdSetting(tdInner, "Include Weight of Discrete Materials in P&L statements", trInner, "AlterNateColor1")
                            trInner.ID = "PCIN_" + i.ToString()
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                If dstbl.Tables(k).Rows(0).Item("discmatyn") = 1 Then
                                    tdInner.Text = "Yes"
                                Else
                                    tdInner.Text = "No"
                                End If
                                InnerTdSetting(tdInner, DWidth, "Right")
                                trInner.Controls.Add(tdInner)
                            Next

                        Case 3
                            Dim cnt As Integer
                            tdInner = New TableCell
                            LeftTdSetting(tdInner, "Printing plates", trInner, "AlterNateColor2")
                            trInner.ID = "PCMQ_" + i.ToString()
                            For k = 0 To DataCnt
                                tdInner = New TableCell
                                cnt = dstbl.Tables(k).Rows(0).Item("plate")
                                Select Case cnt
                                    Case 0
                                        tdInner.Text = "not required"
                                    Case 1
                                        tdInner.Text = "produced by packaging supplier"
                                    Case 2
                                        tdInner.Text = "produced by outside supplier"
                                End Select

                                InnerTdSetting(tdInner, DWidth, "Right")
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

    Protected Sub GetMaterialDetails(ByRef lbl As Label, ByVal MatId As Integer, ByVal dsMat1 As DataSet)
        Dim Ds As New DataSet
        Dim ObjGetdata As New S1GetData.Selectdata()
        Dim hidval As New HiddenField
        Dim Path As String = String.Empty
        Dim dvMat As New DataView
        Dim dtMat As New DataTable
        Try
            'Ds = ObjGetdata.GetMaterials(MatId, "", "")
            dvMat = dsMat1.Tables(0).DefaultView
            dvMat.RowFilter = "MATID= " + MatId.ToString()
            dtMat = dvMat.ToTable()

            If dtMat.Rows(0).Item("MATDES").ToString().Length > 25 Then
                'LinkMat.Font.Size = 8
            End If
            lbl.Text = dtMat.Rows(0).Item("MATDES").ToString()
            lbl.ToolTip = dtMat.Rows(0).Item("MATDES").ToString()


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

    Protected Sub TextBoxSetting(ByVal txt As TextBox, ByVal Css As String)
        Try


            txt.CssClass = Css
            txt.Enabled = False
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
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    
    Protected Function GetShipSelectorDetails(ByRef lbl As Label, ByVal ShipId As Integer)
        Dim ddl As New DropDownList
        Dim ds As New DataSet()
        Dim objGetData As New S1GetData.Selectdata
        Try
            ds = objGetData.GetShipingSelector(ShipId, "")
            'With ddl
            '    .DataSource = ds
            '    .DataTextField = "shipdes"
            '    .DataValueField = "shipid"
            '    .DataBind()
            '    .CssClass = "DropDown"
            '    .Width = 300
            '    .Enabled = False
            'End With

            If ds.Tables(0).Rows(0).Item("shipdes").ToString().Length > 25 Then
                'LinkMat.Font.Size = 8
            End If
            lbl.Text = ds.Tables(0).Rows(0).Item("shipdes").ToString()


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetMateralCombo:" + ex.Message.ToString()
        End Try
        Return ddl
    End Function

    'Protected Function GetSustainMaterials(ByRef lbl As Label, ByVal typeid As String)
    '    Dim ddl As New DropDownList
    '    Dim ds As New DataSet()
    '    Dim objGetData As New S1GetData.Selectdata
    '    Try
    '        ds = objGetData.GetSustainMaterials(typeid, "")
    '        'With ddl
    '        '    .DataSource = ds
    '        '    .DataTextField = "TYPE"
    '        '    .DataValueField = "TYPEID"
    '        '    .DataBind()
    '        '    .CssClass = "DropDown"
    '        '    .Width = 110
    '        '    .Enabled = False
    '        'End With
    '        If ds.Tables(0).Rows(0).Item("type").ToString().Length > 25 Then
    '            'LinkMat.Font.Size = 8
    '        End If
    '        lbl.Text = ds.Tables(0).Rows(0).Item("type").ToString()


    '    Catch ex As Exception
    '        _lErrorLble.Text = "Error:GetMateralCombo:" + ex.Message.ToString()
    '    End Try
    '    Return ddl
    'End Function




End Class
