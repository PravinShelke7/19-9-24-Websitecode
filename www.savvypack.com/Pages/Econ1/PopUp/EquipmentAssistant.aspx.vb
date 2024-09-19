Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E1GetData
Imports E1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter

Partial Class Pages_Econ1_EqAssistantInfo
    Inherits System.Web.UI.Page

    'Press width
    Dim arrPWid(3) As String
    Dim arrCyl(2, 3) As String
    Dim arrCol(2, 3) As String
    Dim arrPWidId(3) As String
    Dim arrCylId(2, 3) As String
    Dim arrColId(2, 3) As String
    Dim UHeadVal As New Double
    Dim UColVal As New Double
    Dim UCylVal As New Double

    'Run Parameters
    Dim URPProc As New Double
    Dim URPReg As New Double
    Dim arrRPProc(5) As String
    Dim arrRPReg(5) As String
    Dim arrRPProcId(5) As String
    Dim arrRPRegId(5) As String
    Dim TotalRunP As New Double
    Dim cyclePerMin As New Double
    Dim piecePerMin As New Double

    'Registration Rate
    Dim URegVal As New Double
    Dim URegValue As New Double

    'Run Waste
    Dim URWProc As New Double
    Dim URWReg As New Double
    Dim arrRWProc(5) As String
    Dim arrRWReg(5) As String
    Dim arrRWProcId(5) As String
    Dim arrRWRegId(5) As String
    Dim TotalRunW As New Double

    'Registration Time
    Dim URTProc As New Double
    Dim URTReg As New Double
    Dim arrRTProc(5) As String
    Dim arrRTReg(5) As String
    Dim arrRTProcId(5) As String
    Dim arrRTRegId(5) As String
    Dim TotalRegT As New Double


    'Maintenance Time
    Dim UMainVal As New Double

    'Job Sequence
    Dim DesgDesc(70) As String
    Dim RSize(70) As String
    Dim columnVal(70, 5) As String
    Dim colName(5) As String
    'Dim CylVal(70, 5) As String
    Dim SizeTotal As New Integer
    Dim SizeMTotal As New Integer
    Dim ColTotal As New Integer
    Dim CylTotal As New Integer
    ' Dim columnValTotal(5) As Integer
    Dim columnCount As Integer

    'Sequence Results
    Dim TotNetVal As New Double
    Dim TotGrossVal As New Double
    Dim TotColVal As New Double
    Dim TotCylVal As New Double
    Dim TotSerVal As New Double
    Dim TotParVal As New Double
    Dim TotRegVal As New Double
    Dim TotMainVal As New Double
    Dim TotPressTotal As New Double
    Dim TotalPref As New Double
    Dim TotalPressPref As New Double

    'Material Results
    Dim TotMNetVal As New Double
    Dim TotMGrossVal As New Double
    Dim TotMColVal As New Double
    Dim TotMCylVal As New Double
    Dim TotMSerVal As New Double
    Dim TotMParVal As New Double
    Dim TotMRegVal As New Double
    Dim TotMMainVal As New Double
    Dim TotMPressTotal As New Double

    'Time Summary
    Dim TotSumVal As New Double
    Dim TotPerc As New Double
    Dim TotHrVal As New Double
    Dim TotHr2Val As New Double
    Dim PWaste1 As New Double
    Dim PWaste2 As New Double
    Dim RegT As New Double
    Dim GoodP As New Double

    'Preferred Value
    Dim URpPref As New Double
    Dim URrPref As New Double
    Dim URwPref As New Double
    Dim URtPref As New Double
    Dim UMtPref As New Double
    Dim jobResPref As String

    'Waste and Downtime Percent
    Dim wastePerc As New Double
    Dim downtimePerc As New Double
    Dim TotSum2Val As New Double
    Dim regValue As New Double
    Dim downTimeValue As New Double
    Dim regTimeValue As New Double
    Dim maintenanceValue As New Double

    Dim equipmentDesc As String
    Dim unit As String
    Dim unit2 As String
    Dim dcRadioSel As New Dictionary(Of Integer, Integer)

#Region "Get Set Variables"
    Dim _btnUpdate As ImageButton

    Public Property Updatebtn() As ImageButton
        Get
            Return _btnUpdate
        End Get
        Set(ByVal value As ImageButton)
            _btnUpdate = value
        End Set
    End Property

#End Region

#Region "MastePage Content Variables"

    Protected Sub GetMasterPageControls()
        GetUpdatebtn()
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Visible = True
        AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'GetMasterPageControls()
            If Not IsPostBack Then
                hidHdCol.Value = ""
                hidHdCyl.Value = ""
                hidRowNum.Value = "1"
                hidLayerId.Value = Request.QueryString("RowNum").ToString()
                hidJRes.Value = "Parallel"
                hidEqId.Value = Request.QueryString("EqId").ToString()
                GetDateDetails()
            End If
            GetDefaultValue()
            If Not IsPostBack Then
                'GetHeadDetails()
            End If
            lblTitle.Text = "Operating Parameter Assistant"
            lblTitle.ToolTip = "This assistant calculates the Waste and Downtime percentages for the Operating Paameter screen"
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetDateDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Try
            ds = objGetData.GetEqAssistEffDateDetails()

            With ddlDate
                .DataSource = ds
                .DataTextField = "EFFDATE"
                .DataValueField = "EFFDATE"
                .DataBind()
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GetPercentageValue()
        Try
            lblWastePerc.Text = FormatNumber((wastePerc / TotSum2Val) * 100, 1) + " %"
            lblWasteForm.Text = "Formula = (Process Waste + Registration Waste) / Total Footage"
            lblDowntimePerc.Text = FormatNumber(((downTimeValue + (regTimeValue) + maintenanceValue) / TotHrVal) * 100, 1) + " %"
            lblDownTimeForm.Text = "Formula = (Setup Downtime + Registration Downtime + Maintenance Downtime) / Total Press Time"
        Catch ex As Exception

        End Try
    End Sub

    Public Sub GetDefaultValue()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim dsPref As New DataSet
        Dim dsEquip As New DataSet
        Try
            ds = objGetData.GetEqCaseDetails(Session("E1CaseId"), hidEqId.Value, ddlDate.SelectedValue, hidLayerId.Value)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If ds.Tables(0).Rows(i).Item("PGCOMPONENTID").ToString() = "1" Then
                        hidSelHead.Value = ds.Tables(0).Rows(i).Item("SELVALUE").ToString()
                    ElseIf ds.Tables(0).Rows(i).Item("PGCOMPONENTID").ToString() = "2" Then
                        hidSelHdCyl.Value = ds.Tables(0).Rows(i).Item("SELVALUE").ToString()
                    ElseIf ds.Tables(0).Rows(i).Item("PGCOMPONENTID").ToString() = "3" Then
                        hidSelHdCol.Value = ds.Tables(0).Rows(i).Item("SELVALUE").ToString()
                    ElseIf ds.Tables(0).Rows(i).Item("PGCOMPONENTID").ToString() = "4" Then
                        hidSelRPProc.Value = ds.Tables(0).Rows(i).Item("SELVALUE").ToString()
                    ElseIf ds.Tables(0).Rows(i).Item("PGCOMPONENTID").ToString() = "5" Then
                        hidSelRPReg.Value = ds.Tables(0).Rows(i).Item("SELVALUE").ToString()
                    ElseIf ds.Tables(0).Rows(i).Item("PGCOMPONENTID").ToString() = "6" Then
                        hidSelRWProc.Value = ds.Tables(0).Rows(i).Item("SELVALUE").ToString()
                    ElseIf ds.Tables(0).Rows(i).Item("PGCOMPONENTID").ToString() = "7" Then
                        hidSelRWReg.Value = ds.Tables(0).Rows(i).Item("SELVALUE").ToString()
                    ElseIf ds.Tables(0).Rows(i).Item("PGCOMPONENTID").ToString() = "8" Then
                        hidSelRTProc.Value = ds.Tables(0).Rows(i).Item("SELVALUE").ToString()
                    ElseIf ds.Tables(0).Rows(i).Item("PGCOMPONENTID").ToString() = "9" Then
                        hidSelRTReg.Value = ds.Tables(0).Rows(i).Item("SELVALUE").ToString()
                    ElseIf ds.Tables(0).Rows(i).Item("PGCOMPONENTID").ToString() = "10" Then
                        hidSelJRes.Value = ds.Tables(0).Rows(i).Item("SELVALUE").ToString()
                    End If
                Next
            End If

            dsPref = objGetData.GetAssistPrefDetails(Session("E1CaseId"), hidEqId.Value, hidLayerId.Value)
            If dsPref.Tables(0).Rows.Count > 0 Then
                If dsPref.Tables(0).Rows(0).Item("RPARAMPREF").ToString() <> "" Then
                    URpPref = dsPref.Tables(0).Rows(0).Item("RPARAMPREF")
                End If
                If dsPref.Tables(0).Rows(0).Item("RRATEPREF").ToString() <> "" Then
                    URrPref = dsPref.Tables(0).Rows(0).Item("RRATEPREF")
                End If
                If dsPref.Tables(0).Rows(0).Item("RWASTEPREF").ToString() <> "" Then
                    URwPref = dsPref.Tables(0).Rows(0).Item("RWASTEPREF")
                End If
                If dsPref.Tables(0).Rows(0).Item("RTIMEPREF").ToString() <> "" Then
                    URtPref = dsPref.Tables(0).Rows(0).Item("RTIMEPREF")
                End If
                If dsPref.Tables(0).Rows(0).Item("MTIMEPREF").ToString() <> "" Then
                    UMtPref = dsPref.Tables(0).Rows(0).Item("MTIMEPREF")
                End If
                If dsPref.Tables(0).Rows(0).Item("JOBRESPREF").ToString() <> "" Then
                    jobResPref = dsPref.Tables(0).Rows(0).Item("JOBRESPREF")
                End If
                If dsPref.Tables(0).Rows(0).Item("CYCLEPMIN").ToString() <> "" Then
                    cyclePerMin = dsPref.Tables(0).Rows(0).Item("CYCLEPMIN")
                End If
                If dsPref.Tables(0).Rows(0).Item("PIECEPMIN").ToString() <> "" Then
                    piecePerMin = dsPref.Tables(0).Rows(0).Item("PIECEPMIN")
                End If
            End If

            dsEquip = objGetData.GetEquipmentUnit(hidEqId.Value)
            If dsEquip.Tables(0).Rows.Count > 0 Then
                If dsEquip.Tables(0).Rows(0).Item("UNITS").ToString() = "cpm" Then
                    unit = "pieces"
                    unit2 = "pieces"
                Else
                    unit = "feet"
                    unit2 = "meter"
                End If
            End If

            GetPageDetails()
            If dsEquip.Tables(0).Rows(0).Item("UNITS").ToString() = "cpm" Then
                GetRunPDetailsCPM()
            Else
                GetRunPDetails()
            End If
            'GetRunPDetails()
            GetRunRDetails()
            GetRunWDetails()
            GetRegTDetails()
            GetMaintTDetails()
            GetJobSeqDetails()
            GetJobSeqResDetails()
            GetMatJobSeqResDetails()
            GetTimeSumDetails()
            GetMatSumDetails()
            GetSetupSumDetails()
            GetPercentageValue()

        Catch ex As Exception

        End Try
    End Sub

    Public Sub GetPageDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim dsPref As New DataSet
        Dim dsMat As New DataSet
        Dim rdb As New RadioButton
        Dim val As New Double
        Dim dvData As New DataView
        Dim dtData As New DataTable
        Dim dsCyl As New DataSet
        Dim dsCylT As New DataSet
        Dim dsCol As New DataSet
        Dim dsColT As New DataSet
        Dim dvCyl As New DataView
        Dim dtCyl As New DataTable
        Dim dvCol As New DataView
        Dim dtCol As New DataTable
        Dim dsEquip As New DataSet
        Try
            tblData.Rows.Clear()
            'ds = objGetData.GetMaterials(hidEqId.Value, "", "")
            'dsMat = objGetData.GetMatArch(hidEqId.Value, Session("E1CaseId"))
            dsData = objGetData.GetPressWidth(hidEqId.Value, ddlDate.SelectedValue)
            dsCyl = objGetData.GetPWCylinder(hidEqId.Value, ddlDate.SelectedValue)
            dsCylT = objGetData.GetPWCylinderType(hidEqId.Value, ddlDate.SelectedValue)
            dsCol = objGetData.GetPWColor(hidEqId.Value, ddlDate.SelectedValue)
            dsColT = objGetData.GetPWColorType(hidEqId.Value, ddlDate.SelectedValue)
            dsCol = objGetData.GetPWColor(hidEqId.Value, ddlDate.SelectedValue)
            dvData = dsData.Tables(0).DefaultView
            dsPref = objGetData.GetPref(Session("E1CaseId"))
            dsEquip = objGetData.GetEquipmentDetails(hidEqId.Value)
            Session("dsEquip") = dsEquip

            lblEquipDesc.Text = dsEquip.Tables(0).Rows(0).Item("EQUIPDE2").ToString()
            'lblTitle.Text = "Material " + ds.Tables(0).Rows(0).Item("MATID").ToString() + ":" + ds.Tables(0).Rows(0).Item("MATDES").ToString()
            dvData.RowFilter = "ISDEFAULT='Y'"
            dtData = dvData.ToTable
            trHeader = New TableRow

            For i = 1 To 6
                tdHeader = New TableCell
                tdHeader1 = New TableCell

                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "200px", "", "2")
                        HeaderTdSetting(tdHeader1, "0", "Press Width", "2")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        rdb = New RadioButton
                        rdb.ID = "rdbHead_" + (i - 1).ToString()
                        rdb.GroupName = "PressW"
                        'If hidHead.Value <> "" Then
                        '    If hidHead.Value = rdb.ID.ToString() Then
                        '        rdb.Checked = True
                        '    Else
                        '        rdb.Checked = False
                        '    End If
                        'Else
                        If hidSelHead.Value <> "" Then
                            If dsData.Tables(0).Rows(i - 2).Item("PRESSWIDTHID").ToString() = hidSelHead.Value Then
                                rdb.Checked = True
                                UHeadVal = dsData.Tables(0).Rows(i - 2).Item("VALUE").ToString()
                                hidHead.Value = rdb.ID.ToString()
                                dcRadioSel(1) = dsData.Tables(0).Rows(i - 2).Item("PRESSWIDTHID")
                            Else
                                rdb.Checked = False
                            End If
                        Else
                            If dsData.Tables(0).Rows(i - 2).Item("ISDEFAULT").ToString() = "Y" Then
                                rdb.Checked = True
                                hidHead.Value = rdb.ID.ToString()
                                dcRadioSel(1) = dsData.Tables(0).Rows(i - 2).Item("PRESSWIDTHID")
                            Else
                                rdb.Checked = False
                            End If
                        End If
                        'End If

                        AddHandler rdb.CheckedChanged, AddressOf rdb_CheckedChanged
                        rdb.AutoPostBack = True
                        HeaderTdSetting(tdHeader, "90px", "", "1")
                        HeaderTdSetting(tdHeader1, "0", dsData.Tables(0).Rows(i - 2).Item("VALUE").ToString(), "1")
                        arrPWid(0) = dsData.Tables(0).Rows(i - 2).Item("VALUE").ToString()
                        arrPWidId(0) = dsData.Tables(0).Rows(i - 2).Item("PRESSWIDTHID").ToString()
                        If dsData.Tables(0).Rows(i - 2).Item("ISSELECT").ToString() = "Y" Then
                            tdHeader.Controls.Add(rdb)
                        End If
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        rdb = New RadioButton
                        rdb.ID = "rdbHead_" + (i - 1).ToString()
                        rdb.GroupName = "PressW"
                        'If hidHead.Value <> "" Then
                        '    If hidHead.Value = rdb.ID.ToString() Then
                        '        rdb.Checked = True
                        '    Else
                        '        rdb.Checked = False
                        '    End If
                        'Else
                        If hidSelHead.Value <> "" Then
                            If dsData.Tables(0).Rows(i - 2).Item("PRESSWIDTHID").ToString() = hidSelHead.Value Then
                                rdb.Checked = True
                                UHeadVal = dsData.Tables(0).Rows(i - 2).Item("VALUE").ToString()
                                hidHead.Value = rdb.ID.ToString()
                                dcRadioSel(1) = dsData.Tables(0).Rows(i - 2).Item("PRESSWIDTHID")
                            Else
                                rdb.Checked = False
                            End If
                        Else
                            If dsData.Tables(0).Rows(i - 2).Item("ISDEFAULT").ToString() = "Y" Then
                                rdb.Checked = True
                                hidHead.Value = rdb.ID.ToString()
                                dcRadioSel(1) = dsData.Tables(0).Rows(i - 2).Item("PRESSWIDTHID")
                            Else
                                rdb.Checked = False
                            End If
                        End If
                        'End If

                        AddHandler rdb.CheckedChanged, AddressOf rdb_CheckedChanged
                        rdb.AutoPostBack = True
                        HeaderTdSetting(tdHeader, "90px", "", "1")
                        HeaderTdSetting(tdHeader1, "0", dsData.Tables(0).Rows(i - 2).Item("VALUE").ToString(), "1")
                        arrPWid(1) = dsData.Tables(0).Rows(i - 2).Item("VALUE").ToString()
                        arrPWidId(1) = dsData.Tables(0).Rows(i - 2).Item("PRESSWIDTHID").ToString()
                        If dsData.Tables(0).Rows(i - 2).Item("ISSELECT").ToString() = "Y" Then
                            tdHeader.Controls.Add(rdb)
                        End If
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        rdb = New RadioButton
                        rdb.ID = "rdbHead_" + (i - 1).ToString()
                        rdb.GroupName = "PressW"
                        'If hidHead.Value <> "" Then
                        '    If hidHead.Value = rdb.ID.ToString() Then
                        '        rdb.Checked = True
                        '    Else
                        '        rdb.Checked = False
                        '    End If
                        'Else
                        If hidSelHead.Value <> "" Then
                            If dsData.Tables(0).Rows(i - 2).Item("PRESSWIDTHID").ToString() = hidSelHead.Value Then
                                rdb.Checked = True
                                UHeadVal = dsData.Tables(0).Rows(i - 2).Item("VALUE").ToString()
                                hidHead.Value = rdb.ID.ToString()
                                dcRadioSel(1) = dsData.Tables(0).Rows(i - 2).Item("PRESSWIDTHID")
                            Else
                                rdb.Checked = False
                            End If
                        Else
                            If dsData.Tables(0).Rows(i - 2).Item("ISDEFAULT").ToString() = "Y" Then
                                rdb.Checked = True
                                hidHead.Value = rdb.ID.ToString()
                                dcRadioSel(1) = dsData.Tables(0).Rows(i - 2).Item("PRESSWIDTHID")
                            Else
                                rdb.Checked = False
                            End If
                        End If
                        'End If
                        AddHandler rdb.CheckedChanged, AddressOf rdb_CheckedChanged
                        rdb.AutoPostBack = True
                        HeaderTdSetting(tdHeader, "90px", "", "1")
                        HeaderTdSetting(tdHeader1, "0", dsData.Tables(0).Rows(i - 2).Item("VALUE").ToString(), "1")
                        arrPWid(2) = dsData.Tables(0).Rows(i - 2).Item("VALUE").ToString()
                        arrPWidId(2) = dsData.Tables(0).Rows(i - 2).Item("PRESSWIDTHID").ToString()
                        If dsData.Tables(0).Rows(i - 2).Item("ISSELECT").ToString() = "Y" Then
                            tdHeader.Controls.Add(rdb)
                        End If
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        rdb = New RadioButton
                        rdb.ID = "rdbHead_" + (i - 1).ToString()
                        rdb.GroupName = "PressW"
                        'If hidHead.Value <> "" Then
                        '    If hidHead.Value = rdb.ID.ToString() Then
                        '        rdb.Checked = True
                        '    Else
                        '        rdb.Checked = False
                        '    End If
                        'Else
                        If hidSelHead.Value <> "" Then
                            If dsData.Tables(0).Rows(i - 2).Item("PRESSWIDTHID").ToString() = hidSelHead.Value Then
                                rdb.Checked = True
                                UHeadVal = dsData.Tables(0).Rows(i - 2).Item("VALUE").ToString()
                                hidHead.Value = rdb.ID.ToString()
                                dcRadioSel(1) = dsData.Tables(0).Rows(i - 2).Item("PRESSWIDTHID")
                            Else
                                rdb.Checked = False
                            End If
                        Else
                            If dsData.Tables(0).Rows(i - 2).Item("ISDEFAULT").ToString() = "Y" Then
                                rdb.Checked = True
                                hidHead.Value = rdb.ID.ToString()
                                dcRadioSel(1) = dsData.Tables(0).Rows(i - 2).Item("PRESSWIDTHID")
                            Else
                                rdb.Checked = False
                            End If
                        End If
                        'End If
                        AddHandler rdb.CheckedChanged, AddressOf rdb_CheckedChanged
                        rdb.AutoPostBack = True
                        HeaderTdSetting(tdHeader, "90px", "", "1")
                        HeaderTdSetting(tdHeader1, "0", dsData.Tables(0).Rows(i - 2).Item("VALUE").ToString(), "1")
                        arrPWid(3) = dsData.Tables(0).Rows(i - 2).Item("VALUE").ToString()
                        arrPWidId(3) = dsData.Tables(0).Rows(i - 2).Item("PRESSWIDTHID").ToString()
                        If dsData.Tables(0).Rows(i - 2).Item("ISSELECT").ToString() = "Y" Then
                            tdHeader.Controls.Add(rdb)
                        End If
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 6
                        HeaderTdSetting(tdHeader, "90px", "", "1")
                        HeaderTdSetting(tdHeader1, "0", "in", "1")
                        'tdHeader.Controls.Add(rdb)
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                End Select
            Next
            tblData.Controls.Add(trHeader)
            tblData.Controls.Add(trHeader1)

            If dsCylT.Tables(0).Rows.Count > 0 Then
                dvCyl = dsCyl.Tables(0).DefaultView

                For i = 0 To dsCylT.Tables(0).Rows.Count
                    trInner = New TableRow
                    dvCyl.RowFilter = "TYPE='" + dsCylT.Tables(0).Rows(0).Item("TYPE").ToString() + "'"
                    dtCyl = dvCyl.ToTable()
                    For j = 0 To dtCyl.Rows.Count + 2
                        tdInner = New TableCell
                        If i = 0 Then
                            If j = 0 Then
                                InnerTdSetting(tdInner, "200px", "Center")
                                lbl = New Label()
                                lbl.Width = 200
                                If hidEqId.Value = "3" Or hidEqId.Value = "26" Or hidEqId.Value = "83" Then
                                    lbl.Text = "Time to change a Graphic"
                                Else
                                    lbl.Text = "Time to change a Graphic"
                                End If
                                tdInner.ColumnSpan = "2"
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            ElseIf j <> 1 Then
                                InnerTdSetting(tdInner, "90px", "Right")
                                lbl = New Label
                                If j = 6 Then
                                    lbl.Text = ""
                                Else
                                    lbl.Text = dsData.Tables(0).Rows(j - 2).Item("VALUE").ToString() * 25.4
                                End If
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            End If
                        Else
                            dvCyl.RowFilter = "TYPE='" + dsCylT.Tables(0).Rows(i - 1).Item("TYPE").ToString() + "'"
                            dtCyl = dvCyl.ToTable()
                            If j = 0 Then
                                InnerTdSetting(tdInner, "20px", "Center")
                                rdb = New RadioButton
                                rdb.GroupName = "rdbCyl"
                                rdb.ID = "rdbCyl" + i.ToString()
                                'If hidHdCyl.Value <> "" Then
                                '    If hidHdCyl.Value = rdb.ID Then
                                '        rdb.Checked = True
                                '        If hidHead.Value <> "" Then
                                '            Dim head As Integer = Integer.Parse(Regex.Replace(hidHead.Value, "[^\d]", ""))
                                '            UCylVal = dtCyl.Rows(head - 1).Item("VALUE").ToString()
                                '        Else
                                '            UCylVal = dtCyl.Rows(Convert.ToInt32(hidSelHead.Value) - 1).Item("VALUE").ToString()
                                '        End If
                                '    Else
                                '        rdb.Checked = False
                                '    End If
                                'Else
                                If hidSelHdCyl.Value <> "" Then
                                    For b = 0 To dtCyl.Rows.Count - 1
                                        If dtCyl.Rows(b).Item("PWCYLINDERID").ToString() = hidSelHdCyl.Value Then
                                            rdb.Checked = True
                                            hidHdCyl.Value = rdb.ID
                                            UCylVal = dtCyl.Rows(b).Item("VALUE").ToString()
                                            dcRadioSel(2) = dtCyl.Rows(b).Item("PWCYLINDERID")
                                            Exit For
                                        Else
                                            rdb.Checked = False
                                        End If
                                    Next
                                Else
                                    If hidSelHdCyl.Value <> "" Then
                                        For b = 0 To dtCyl.Rows.Count - 1
                                            If dtCyl.Rows(b).Item("PWCYLINDERID").ToString() = hidSelHdCyl.Value Then
                                                rdb.Checked = True
                                                hidHdCyl.Value = rdb.ID
                                                UCylVal = dtCyl.Rows(b).Item("VALUE").ToString()
                                                dcRadioSel(2) = dtCyl.Rows(b).Item("PWCYLINDERID")
                                                Exit For
                                            Else
                                                rdb.Checked = False
                                            End If
                                        Next
                                    Else
                                        If hidSelHead.Value <> "" Then
                                            Dim head As Integer = Integer.Parse(Regex.Replace(hidSelHead.Value, "[^\d]", ""))
                                            For b = 0 To dtCyl.Rows.Count - 1
                                                If dtCyl.Rows(b).Item("ISDEFAULT").ToString() = "Y" Then
                                                    rdb.Checked = True
                                                    hidHdCyl.Value = rdb.ID
                                                    UCylVal = dtCyl.Rows(head - 1).Item("VALUE").ToString()
                                                    dcRadioSel(2) = dtCyl.Rows(b).Item("PWCYLINDERID")
                                                    Exit For
                                                Else
                                                    rdb.Checked = False
                                                End If
                                            Next
                                        Else
                                            For b = 0 To dtCyl.Rows.Count - 1
                                                If dtCyl.Rows(b).Item("ISDEFAULT").ToString() = "Y" Then
                                                    rdb.Checked = True
                                                    hidHdCyl.Value = rdb.ID
                                                    UCylVal = dtCyl.Rows(b).Item("VALUE").ToString()
                                                    dcRadioSel(2) = dtCyl.Rows(b).Item("PWCYLINDERID")
                                                    Exit For
                                                Else
                                                    rdb.Checked = False
                                                End If
                                            Next
                                        End If

                                    End If
                                End If
                                'End If

                                AddHandler rdb.CheckedChanged, AddressOf rdb_CheckedChanged
                                rdb.AutoPostBack = True
                                If dtCyl.Rows(0).Item("ISSELECT").ToString() = "Y" Then
                                    tdInner.Controls.Add(rdb)
                                End If

                            ElseIf j = 1 Then
                                InnerTdSetting(tdInner, "90px", "Left")
                                lbl = New Label
                                If dsCylT.Tables(0).Rows(i - 1).Item("TYPE").ToString() = "MANUAL" Then
                                    lbl.Text = "Manual Process"
                                ElseIf dsCylT.Tables(0).Rows(i - 1).Item("TYPE").ToString() = "SEMIAUTO" Then
                                    lbl.Text = "Semi Automated"
                                Else
                                    lbl.Text = "Automated"
                                End If
                                tdInner.Controls.Add(lbl)
                            ElseIf j = dtCyl.Rows.Count + 2 Then
                                InnerTdSetting(tdInner, "90px", "Center")
                                lbl = New Label
                                lbl.Text = "min"
                                tdInner.Controls.Add(lbl)
                            Else
                                InnerTdSetting(tdInner, "90px", "Right")
                                lbl = New Label
                                lbl.Text = dtCyl.Rows(j - 2).Item("VALUE").ToString()
                                arrCyl(i - 1, j - 2) = dtCyl.Rows(j - 2).Item("VALUE").ToString()
                                arrCylId(i - 1, j - 2) = dtCyl.Rows(j - 2).Item("PWCYLINDERID").ToString()
                                tdInner.Controls.Add(lbl)
                            End If
                            trInner.Controls.Add(tdInner)
                        End If
                    Next
                    If (i Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    tblData.Controls.Add(trInner)
                Next
            End If

            If dsColT.Tables(0).Rows.Count > 0 Then
                dvCol = dsCol.Tables(0).DefaultView

                For i = 0 To dsColT.Tables(0).Rows.Count
                    trInner = New TableRow
                    dvCol.RowFilter = "TYPE='" + dsColT.Tables(0).Rows(0).Item("TYPE").ToString() + "'"
                    dtCol = dvCol.ToTable()
                    For j = 0 To dtCol.Rows.Count + 2
                        tdInner = New TableCell
                        If i = 0 Then
                            If j = 0 Then
                                InnerTdSetting(tdInner, "200px", "Center")
                                lbl = New Label()
                                lbl.Text = "Time to change a color"
                                tdInner.ColumnSpan = "2"
                                tdInner.Controls.Add(lbl)
                                trInner.Controls.Add(tdInner)
                            ElseIf j <> 1 Then
                                InnerTdSetting(tdInner, "", "Left")
                                trInner.Controls.Add(tdInner)
                            End If
                        Else
                            dvCol.RowFilter = "TYPE='" + dsColT.Tables(0).Rows(i - 1).Item("TYPE").ToString() + "'"
                            dtCol = dvCol.ToTable()
                            If j = 0 Then
                                InnerTdSetting(tdInner, "20px", "Center")
                                rdb = New RadioButton
                                rdb.GroupName = "rdbCol"
                                rdb.ID = "rdbCol" + i.ToString()
                                'If hidHdCol.Value <> "" Then
                                '    If hidHdCol.Value = rdb.ID Then
                                '        rdb.Checked = True
                                '        If hidHead.Value <> "" Then
                                '            Dim head As Integer = Integer.Parse(Regex.Replace(hidHead.Value, "[^\d]", ""))
                                '            UColVal = dtCol.Rows(head - 1).Item("VALUE").ToString()
                                '        Else
                                '            UColVal = dtCol.Rows(Convert.ToInt32(hidSelHead.Value) - 1).Item("VALUE").ToString()
                                '        End If
                                '    Else
                                '        rdb.Checked = False
                                '    End If
                                'Else
                                If hidSelHdCol.Value <> "" Then
                                    For b = 0 To dtCol.Rows.Count - 1
                                        If dtCol.Rows(b).Item("PWCOLORID").ToString() = hidSelHdCol.Value Then
                                            rdb.Checked = True
                                            hidHdCol.Value = rdb.ID
                                            UColVal = dtCol.Rows(b).Item("VALUE").ToString()
                                            dcRadioSel(3) = dtCol.Rows(b).Item("PWCOLORID")
                                            Exit For
                                        Else
                                            rdb.Checked = False
                                        End If
                                    Next
                                Else
                                    If hidSelHead.Value <> "" Then
                                        Dim head As Integer = Integer.Parse(Regex.Replace(hidSelHead.Value, "[^\d]", ""))
                                        For b = 0 To dtCol.Rows.Count - 1
                                            If dtCol.Rows(b).Item("ISDEFAULT").ToString() = "Y" Then
                                                rdb.Checked = True
                                                hidHdCol.Value = rdb.ID
                                                UColVal = dtCol.Rows(head - 1).Item("VALUE").ToString()
                                                dcRadioSel(3) = dtCol.Rows(b).Item("PWCOLORID")
                                                Exit For
                                            Else
                                                rdb.Checked = False
                                            End If
                                        Next
                                    Else
                                        For b = 0 To dtCol.Rows.Count - 1
                                            If dtCol.Rows(b).Item("ISDEFAULT").ToString() = "Y" Then
                                                rdb.Checked = True
                                                hidHdCol.Value = rdb.ID
                                                UColVal = dtCol.Rows(b).Item("VALUE").ToString()
                                                dcRadioSel(3) = dtCol.Rows(b).Item("PWCOLORID")
                                                Exit For
                                            Else
                                                rdb.Checked = False
                                            End If
                                        Next
                                    End If

                                End If
                                'End If

                                AddHandler rdb.CheckedChanged, AddressOf rdb_CheckedChanged
                                rdb.AutoPostBack = True
                                If dtCol.Rows(0).Item("ISSELECT").ToString() = "Y" Then
                                    tdInner.Controls.Add(rdb)
                                End If

                            ElseIf j = 1 Then
                                InnerTdSetting(tdInner, "90px", "Left")
                                lbl = New Label
                                lbl.Text = dsColT.Tables(0).Rows(i - 1).Item("TYPE").ToString()
                                tdInner.Controls.Add(lbl)
                            ElseIf j = dtCol.Rows.Count + 2 Then
                                InnerTdSetting(tdInner, "90px", "Center")
                                lbl = New Label
                                lbl.Text = "min"
                                tdInner.Controls.Add(lbl)
                            Else
                                InnerTdSetting(tdInner, "90px", "Right")
                                lbl = New Label
                                lbl.Text = dtCol.Rows(j - 2).Item("VALUE").ToString()
                                arrCol(i - 1, j - 2) = dtCol.Rows(j - 2).Item("VALUE").ToString()
                                arrColId(i - 1, j - 2) = dtCol.Rows(j - 2).Item("PWCOLORID").ToString()
                                tdInner.Controls.Add(lbl)
                            End If
                            trInner.Controls.Add(tdInner)
                        End If
                    Next
                    If (i Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    tblData.Controls.Add(trInner)
                Next
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetRunPDetailsCPM()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim txt As New TextBox
        Dim dsPref As New DataSet
        Dim dsMat As New DataSet
        Dim rdb As New RadioButton
        Dim val As New Double
        Dim dvData As New DataView
        Dim dtData As New DataTable
        Dim dsCyl As New DataSet
        Dim dsDataT As New DataSet
        Try
            tblRunP.Rows.Clear()
            dsData = objGetData.GetRunParameters(hidEqId.Value, ddlDate.SelectedValue)
            dvData = dsData.Tables(0).DefaultView
            dsDataT = objGetData.GetRunParametersT(hidEqId.Value, ddlDate.SelectedValue)
            dsPref = objGetData.GetPref(Session("E1CaseId"))
            trHeader = New TableRow
            For i = 1 To 5
                tdHeader = New TableCell
                tdHeader1 = New TableCell

                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "200px", "Run Parameters", "2")
                        HeaderTdSetting(tdHeader1, "0", "Instaneous Rates", "2")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "180px", "Suggested", "3")
                        HeaderTdSetting(tdHeader1, "90px", "(cycle/min)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        If lblEquipDesc.Text.ToLower().Contains("sheet fed").ToString() Then
                            HeaderTdSetting(tdHeader1, "0", "(impressions/cycle)", "1")
                        ElseIf lblEquipDesc.Text.ToLower().Contains("injection molding").ToString() Then
                            HeaderTdSetting(tdHeader1, "0", "# of cavities", "1")
                        Else
                            HeaderTdSetting(tdHeader1, "0", "(" + unit2 + "/cycle)", "1")
                        End If

                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        If lblEquipDesc.Text.ToLower().Contains("sheet fed").ToString() Then
                            HeaderTdSetting(tdHeader1, "0", "(impressions/min)", "1")
                        Else
                            HeaderTdSetting(tdHeader1, "0", "(" + unit2 + "/min)", "1")
                        End If

                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        HeaderTdSetting(tdHeader, "180px", "Preferred", "1")
                        HeaderTdSetting(tdHeader1, "90px", "(" + unit + "/min)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)

                End Select
            Next
            tblRunP.Controls.Add(trHeader)
            tblRunP.Controls.Add(trHeader1)

            Dim RpProc As New Double
            Dim RpReg As New Double

            For i = 0 To dsDataT.Tables(0).Rows.Count - 1
                dvData.RowFilter = "TYPE='" + dsDataT.Tables(0).Rows(i).Item("TYPE").ToString() + "'"
                dtData = dvData.ToTable()
                For k = 0 To dtData.Rows.Count - 1
                    If dsDataT.Tables(0).Rows(i).Item("TYPE").ToString() = "PROC" Then
                        If hidSelRPProc.Value <> "" Then
                            If dtData.Rows(k).Item("PWRUNPARAMID").ToString() = hidSelRPProc.Value Then
                                RpProc = dtData.Rows(k).Item("VALUE").ToString()
                                'Else
                                '    arrRPProc(k) = dtData.Rows(k).Item("VALUE").ToString()
                            End If
                        Else
                            If dtData.Rows(k).Item("ISDEFAULT").ToString() = "Y" Then
                                RpProc = dtData.Rows(k).Item("VALUE").ToString()
                                '    arrRPProc(k) = dtData.Rows(k).Item("VALUE").ToString()
                                'Else
                                '    arrRPProc(k) = dtData.Rows(k).Item("VALUE").ToString()
                            End If
                        End If
                        arrRPProc(k) = dtData.Rows(k).Item("VALUE").ToString()
                    Else
                        If hidSelRPReg.Value <> "" Then
                            If dtData.Rows(k).Item("PWRUNPARAMID").ToString() = hidSelRPReg.Value Then
                                RpReg = dtData.Rows(k).Item("VALUE").ToString()
                                'Else
                                '    arrRPReg(k) = dtData.Rows(k).Item("VALUE").ToString()
                            End If
                        Else
                            If dtData.Rows(k).Item("ISDEFAULT").ToString() = "Y" Then
                                RpReg = dtData.Rows(k).Item("VALUE").ToString()
                                '    arrRPReg(k) = dtData.Rows(k).Item("VALUE").ToString()
                                'Else
                                '    arrRPReg(k) = dtData.Rows(k).Item("VALUE").ToString()
                            End If
                        End If
                        arrRPReg(k) = dtData.Rows(k).Item("VALUE").ToString()
                    End If
                Next
            Next
            'If hidRPProc.Value <> "" And hidRPReg.Value <> "" Then
            '    If dsDataT.Tables(0).Rows.Count <> 1 Then
            '        TotalRunP = (URPProc + URPReg) / 2
            '    Else
            '        TotalRunP = URPProc
            '    End If
            'Else
            If dsDataT.Tables(0).Rows.Count <> 1 Then
                If hidRPProc.Value = "" And hidRPReg.Value = "" Then
                    cyclePerMin = (RpProc + RpReg) / 2
                ElseIf hidRPProc.Value = "" Then
                    cyclePerMin = (RpProc + URPReg) / 2
                ElseIf hidRPReg.Value = "" Then
                    cyclePerMin = (URPProc + RpReg) / 2
                End If
            Else
                If URPProc <> 0 Then
                    cyclePerMin = URPProc
                Else
                    cyclePerMin = RpProc
                End If
               
            End If
            If cyclePerMin <> 0.0 And piecePerMin <> 0.0 Then
                TotalRunP = cyclePerMin * piecePerMin
            End If
            trInner = New TableRow
            For j = 0 To 4
                tdInner = New TableCell

                If j = 0 Then
                    InnerTdSetting(tdInner, "200px", "Center")
                    lbl = New Label()
                    lbl.Width = 200
                    lbl.Text = "Production Rate"
                    tdInner.ColumnSpan = "2"
                    tdInner.Controls.Add(lbl)
                    trInner.Controls.Add(tdInner)
                ElseIf j = 1 Then
                    InnerTdSetting(tdInner, "", "Right")
                    lbl = New Label()
                    lbl.Text = cyclePerMin
                    tdInner.Controls.Add(lbl)
                    trInner.Controls.Add(tdInner)
                ElseIf j = 2 Then
                    InnerTdSetting(tdInner, "", "Right")
                    txt = New TextBox
                    txt.ID = "txtPiece"
                    txt.CssClass = "SmallTextBox"
                    If piecePerMin <> 0.0 Then
                        txt.Text = piecePerMin.ToString()
                        'TotalRunP = URpPref
                    End If
                    tdInner.Controls.Add(txt)
                    trInner.Controls.Add(tdInner)
                ElseIf j = 3 Then
                    InnerTdSetting(tdInner, "", "Center")
                    lbl = New Label()
                    lbl.Text = TotalRunP
                    tdInner.Controls.Add(lbl)
                    trInner.Controls.Add(tdInner)
                Else
                    InnerTdSetting(tdInner, "", "Right")
                    txt = New TextBox
                    txt.ID = "txtRpPref"
                    txt.CssClass = "SmallTextBox"
                    If URpPref <> 0.0 Then
                        txt.Text = URpPref.ToString()
                        'TotalRunP = URpPref
                    End If
                    tdInner.Controls.Add(txt)
                    trInner.Controls.Add(tdInner)
                End If
                trInner.CssClass = "AlterNateColor1"
            Next
            tblRunP.Controls.Add(trInner)

            For i = 0 To dsDataT.Tables(0).Rows.Count - 1
                dvData.RowFilter = "TYPE='" + dsDataT.Tables(0).Rows(i).Item("TYPE").ToString() + "'"
                dtData = dvData.ToTable()
                For k = 0 To dtData.Rows.Count - 1
                    trInner = New TableRow
                    For j = 0 To 5
                        tdInner = New TableCell
                        If j = 0 Then
                            InnerTdSetting(tdInner, "20px", "Center")
                            rdb = New RadioButton
                            rdb.GroupName = "rdbRunP" + dsDataT.Tables(0).Rows(i).Item("TYPE").ToString()
                            rdb.ID = "rdbRunP" + dsDataT.Tables(0).Rows(i).Item("TYPE").ToString() + "_" + (k + 1).ToString()
                            If dsDataT.Tables(0).Rows(i).Item("TYPE").ToString() = "PROC" Then
                                'If hidRPProc.Value <> "" Then
                                '    If hidRPProc.Value = rdb.ID Then
                                '        rdb.Checked = True
                                '        URPProc = dtData.Rows(k).Item("VALUE")
                                '    Else
                                '        rdb.Checked = False
                                '    End If
                                'Else
                                If hidSelRPProc.Value <> "" Then
                                    If dtData.Rows(k).Item("PWRUNPARAMID").ToString() = hidSelRPProc.Value Then
                                        URPProc = dtData.Rows(k).Item("VALUE")
                                        dcRadioSel(4) = dtData.Rows(k).Item("PWRUNPARAMID")
                                        rdb.Checked = True
                                    Else
                                        rdb.Checked = False
                                    End If
                                Else
                                    If dtData.Rows(k).Item("ISDEFAULT").ToString() = "Y" Then
                                        URPProc = dtData.Rows(k).Item("VALUE")
                                        dcRadioSel(4) = dtData.Rows(k).Item("PWRUNPARAMID")
                                        rdb.Checked = True
                                    Else
                                        rdb.Checked = False
                                    End If
                                End If
                                'End If
                                arrRPProc(k) = dtData.Rows(k).Item("VALUE").ToString()
                                arrRPProcId(k) = dtData.Rows(k).Item("PWRUNPARAMID").ToString()
                            Else
                                'If hidRPReg.Value <> "" Then
                                '    If hidRPReg.Value = rdb.ID Then
                                '        rdb.Checked = True
                                '        URPReg = dtData.Rows(k).Item("VALUE")
                                '    Else
                                '        rdb.Checked = False
                                '    End If
                                'Else
                                If hidSelRPReg.Value <> "" Then
                                    If dtData.Rows(k).Item("PWRUNPARAMID").ToString() = hidSelRPReg.Value Then
                                        rdb.Checked = True
                                        URPReg = dtData.Rows(k).Item("VALUE")
                                        dcRadioSel(5) = dtData.Rows(k).Item("PWRUNPARAMID")
                                    Else
                                        rdb.Checked = False
                                    End If
                                Else
                                    If dtData.Rows(k).Item("ISDEFAULT").ToString() = "Y" Then
                                        URPReg = dtData.Rows(k).Item("VALUE")
                                        dcRadioSel(5) = dtData.Rows(k).Item("PWRUNPARAMID")
                                        rdb.Checked = True
                                    Else
                                        rdb.Checked = False
                                    End If
                                End If
                                'End If
                                arrRPReg(k) = dtData.Rows(k).Item("VALUE").ToString()
                                arrRPRegId(k) = dtData.Rows(k).Item("PWRUNPARAMID").ToString()
                            End If

                            AddHandler rdb.CheckedChanged, AddressOf rdb_CheckedChanged
                            rdb.AutoPostBack = True
                            tdInner.Controls.Add(rdb)
                        ElseIf j = 1 Then
                            InnerTdSetting(tdInner, "90px", "Left")
                            lbl = New Label
                            lbl.Text = dtData.Rows(k).Item("DESCRIPTION").ToString()
                            tdInner.Controls.Add(lbl)
                        ElseIf j = 2 Then
                            InnerTdSetting(tdInner, "90px", "Right")
                            lbl = New Label
                            lbl.Text = dtData.Rows(k).Item("VALUE").ToString()
                            tdInner.Controls.Add(lbl)
                        ElseIf j = 3 Then
                            InnerTdSetting(tdInner, "90px", "Right")
                            lbl = New Label
                            If unit <> "pieces" Then
                                lbl.Text = FormatNumber(dtData.Rows(k).Item("VALUE") / 3.27, 0)
                            End If
                            tdInner.Controls.Add(lbl)
                        Else
                            InnerTdSetting(tdInner, "90px", "Right")
                            lbl = New Label
                            lbl.Text = "" ' dtCyl.Rows(j - 2).Item("VALUE").ToString()
                            tdInner.Controls.Add(lbl)
                        End If
                        trInner.Controls.Add(tdInner)
                    Next
                    If (i Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    tblRunP.Controls.Add(trInner)
                Next
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetRunPDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetRunPDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim txt As New TextBox
        Dim dsPref As New DataSet
        Dim dsMat As New DataSet
        Dim rdb As New RadioButton
        Dim val As New Double
        Dim dvData As New DataView
        Dim dtData As New DataTable
        Dim dsCyl As New DataSet
        Dim dsDataT As New DataSet
        Try
            tblRunP.Rows.Clear()
            dsData = objGetData.GetRunParameters(hidEqId.Value, ddlDate.SelectedValue)
            dvData = dsData.Tables(0).DefaultView
            dsDataT = objGetData.GetRunParametersT(hidEqId.Value, ddlDate.SelectedValue)
            dsPref = objGetData.GetPref(Session("E1CaseId"))

            trHeader = New TableRow
            For i = 1 To 5
                tdHeader = New TableCell
                tdHeader1 = New TableCell

                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "200px", "Run Parameters", "2")
                        HeaderTdSetting(tdHeader1, "0", "Instaneous Rates", "2")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "180px", "Suggested", "2")
                        HeaderTdSetting(tdHeader1, "90px", "(" + unit + "/min)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader1, "0", "(" + unit2 + "/min)", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        HeaderTdSetting(tdHeader, "180px", "Preferred", "2")
                        HeaderTdSetting(tdHeader1, "90px", "(" + unit + "/min)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        HeaderTdSetting(tdHeader1, "0", "(" + unit2 + "/min)", "1")
                        trHeader1.Controls.Add(tdHeader1)
                End Select
            Next
            tblRunP.Controls.Add(trHeader)
            tblRunP.Controls.Add(trHeader1)

            Dim RpProc As New Double
            Dim RpReg As New Double

            For i = 0 To dsDataT.Tables(0).Rows.Count - 1
                dvData.RowFilter = "TYPE='" + dsDataT.Tables(0).Rows(i).Item("TYPE").ToString() + "'"
                dtData = dvData.ToTable()
                For k = 0 To dtData.Rows.Count - 1
                    If dsDataT.Tables(0).Rows(i).Item("TYPE").ToString() = "PROC" Then
                        If hidSelRPProc.Value <> "" Then
                            If dtData.Rows(k).Item("PWRUNPARAMID").ToString() = hidSelRPProc.Value Then
                                RpProc = dtData.Rows(k).Item("VALUE").ToString()
                                'Else
                                '    arrRPProc(k) = dtData.Rows(k).Item("VALUE").ToString()
                            End If
                        Else
                            If dtData.Rows(k).Item("ISDEFAULT").ToString() = "Y" Then
                                RpProc = dtData.Rows(k).Item("VALUE").ToString()
                                '    arrRPProc(k) = dtData.Rows(k).Item("VALUE").ToString()
                                'Else
                                '    arrRPProc(k) = dtData.Rows(k).Item("VALUE").ToString()
                            End If
                        End If
                        arrRPProc(k) = dtData.Rows(k).Item("VALUE").ToString()
                    Else
                        If hidSelRPReg.Value <> "" Then
                            If dtData.Rows(k).Item("PWRUNPARAMID").ToString() = hidSelRPReg.Value Then
                                RpReg = dtData.Rows(k).Item("VALUE").ToString()
                                'Else
                                '    arrRPReg(k) = dtData.Rows(k).Item("VALUE").ToString()
                            End If
                        Else
                            If dtData.Rows(k).Item("ISDEFAULT").ToString() = "Y" Then
                                RpReg = dtData.Rows(k).Item("VALUE").ToString()
                                '    arrRPReg(k) = dtData.Rows(k).Item("VALUE").ToString()
                                'Else
                                '    arrRPReg(k) = dtData.Rows(k).Item("VALUE").ToString()
                            End If
                        End If
                        arrRPReg(k) = dtData.Rows(k).Item("VALUE").ToString()
                    End If
                Next
            Next
            If hidRPProc.Value <> "" And hidRPReg.Value <> "" Then
                If dsDataT.Tables(0).Rows.Count <> 1 Then
                    TotalRunP = (URPProc + URPReg) / 2
                Else
                    TotalRunP = URPProc
                End If
            Else
                If dsDataT.Tables(0).Rows.Count <> 1 Then
                    If hidRPProc.Value = "" And hidRPReg.Value = "" Then
                        TotalRunP = (RpProc + RpReg) / 2
                    ElseIf hidRPProc.Value = "" Then
                        TotalRunP = (RpProc + URPReg) / 2
                    ElseIf hidRPReg.Value = "" Then
                        TotalRunP = (URPProc + RpReg) / 2
                    End If
                Else
                    If URPProc <> 0 Then
                        TotalRunP = URPProc
                    Else
                        TotalRunP = RpProc
                    End If
                End If
            End If

            trInner = New TableRow
            For j = 0 To 4
                tdInner = New TableCell

                If j = 0 Then
                    InnerTdSetting(tdInner, "200px", "Center")
                    lbl = New Label()
                    lbl.Width = 200
                    lbl.Text = "Production Rate"
                    tdInner.ColumnSpan = "2"
                    tdInner.Controls.Add(lbl)
                    trInner.Controls.Add(tdInner)
                ElseIf j = 1 Then
                    InnerTdSetting(tdInner, "", "Right")
                    lbl = New Label()
                    lbl.Text = TotalRunP
                    tdInner.Controls.Add(lbl)
                    trInner.Controls.Add(tdInner)
                ElseIf j = 2 Then
                    InnerTdSetting(tdInner, "", "Right")
                    lbl = New Label()
                    If unit <> "pieces" Then
                        lbl.Text = FormatNumber(TotalRunP / 3.27, 0)
                    End If
                    tdInner.Controls.Add(lbl)
                    trInner.Controls.Add(tdInner)
                ElseIf j = 3 Then
                    InnerTdSetting(tdInner, "", "Center")
                    txt = New TextBox
                    txt.ID = "txtRpPref"
                    txt.CssClass = "SmallTextBox"
                    If URpPref <> 0.0 Then
                        txt.Text = URpPref.ToString()
                        'TotalRunP = URpPref
                    End If
                    'lbl.Text = FormatNumber(TotalRunP / 3.27, 0)
                    tdInner.Controls.Add(txt)
                    trInner.Controls.Add(tdInner)
                Else
                    InnerTdSetting(tdInner, "", "Right")
                    lbl = New Label
                    If unit <> "pieces" Then
                        lbl.Text = FormatNumber(URpPref / 3.27, 0)
                    End If
                    tdInner.Controls.Add(lbl)
                    trInner.Controls.Add(tdInner)
                End If
                trInner.CssClass = "AlterNateColor1"
            Next
            tblRunP.Controls.Add(trInner)

            For i = 0 To dsDataT.Tables(0).Rows.Count - 1
                dvData.RowFilter = "TYPE='" + dsDataT.Tables(0).Rows(i).Item("TYPE").ToString() + "'"
                dtData = dvData.ToTable()
                For k = 0 To dtData.Rows.Count - 1
                    trInner = New TableRow
                    For j = 0 To 5
                        tdInner = New TableCell
                        If j = 0 Then
                            InnerTdSetting(tdInner, "20px", "Center")
                            rdb = New RadioButton
                            rdb.GroupName = "rdbRunP" + dsDataT.Tables(0).Rows(i).Item("TYPE").ToString()
                            rdb.ID = "rdbRunP" + dsDataT.Tables(0).Rows(i).Item("TYPE").ToString() + "_" + (k + 1).ToString()
                            If dsDataT.Tables(0).Rows(i).Item("TYPE").ToString() = "PROC" Then
                                'If hidRPProc.Value <> "" Then
                                '    If hidRPProc.Value = rdb.ID Then
                                '        rdb.Checked = True
                                '        URPProc = dtData.Rows(k).Item("VALUE")
                                '    Else
                                '        rdb.Checked = False
                                '    End If
                                'Else
                                If hidSelRPProc.Value <> "" Then
                                    If dtData.Rows(k).Item("PWRUNPARAMID").ToString() = hidSelRPProc.Value Then
                                        URPProc = dtData.Rows(k).Item("VALUE")
                                        dcRadioSel(4) = dtData.Rows(k).Item("PWRUNPARAMID")
                                        rdb.Checked = True
                                    Else
                                        rdb.Checked = False
                                    End If
                                Else
                                    If dtData.Rows(k).Item("ISDEFAULT").ToString() = "Y" Then
                                        URPProc = dtData.Rows(k).Item("VALUE")
                                        dcRadioSel(4) = dtData.Rows(k).Item("PWRUNPARAMID")
                                        rdb.Checked = True
                                    Else
                                        rdb.Checked = False
                                    End If
                                End If
                                'End If
                                arrRPProc(k) = dtData.Rows(k).Item("VALUE").ToString()
                                arrRPProcId(k) = dtData.Rows(k).Item("PWRUNPARAMID").ToString()
                            Else
                                'If hidRPReg.Value <> "" Then
                                '    If hidRPReg.Value = rdb.ID Then
                                '        rdb.Checked = True
                                '        URPReg = dtData.Rows(k).Item("VALUE")
                                '    Else
                                '        rdb.Checked = False
                                '    End If
                                'Else
                                If hidSelRPReg.Value <> "" Then
                                    If dtData.Rows(k).Item("PWRUNPARAMID").ToString() = hidSelRPReg.Value Then
                                        rdb.Checked = True
                                        URPReg = dtData.Rows(k).Item("VALUE")
                                        dcRadioSel(5) = dtData.Rows(k).Item("PWRUNPARAMID")
                                    Else
                                        rdb.Checked = False
                                    End If
                                Else
                                    If dtData.Rows(k).Item("ISDEFAULT").ToString() = "Y" Then
                                        URPReg = dtData.Rows(k).Item("VALUE")
                                        dcRadioSel(5) = dtData.Rows(k).Item("PWRUNPARAMID")
                                        rdb.Checked = True
                                    Else
                                        rdb.Checked = False
                                    End If
                                End If
                                'End If
                                arrRPReg(k) = dtData.Rows(k).Item("VALUE").ToString()
                                arrRPRegId(k) = dtData.Rows(k).Item("PWRUNPARAMID").ToString()
                            End If

                            AddHandler rdb.CheckedChanged, AddressOf rdb_CheckedChanged
                            rdb.AutoPostBack = True
                            tdInner.Controls.Add(rdb)
                        ElseIf j = 1 Then
                            InnerTdSetting(tdInner, "90px", "Left")
                            lbl = New Label
                            lbl.Text = dtData.Rows(k).Item("DESCRIPTION").ToString()
                            tdInner.Controls.Add(lbl)
                        ElseIf j = 2 Then
                            InnerTdSetting(tdInner, "90px", "Right")
                            lbl = New Label
                            lbl.Text = dtData.Rows(k).Item("VALUE").ToString()
                            tdInner.Controls.Add(lbl)
                        ElseIf j = 3 Then
                            InnerTdSetting(tdInner, "90px", "Right")
                            lbl = New Label
                            If unit <> "pieces" Then
                                lbl.Text = FormatNumber(dtData.Rows(k).Item("VALUE") / 3.27, 0)
                            End If
                            tdInner.Controls.Add(lbl)
                        Else
                            InnerTdSetting(tdInner, "90px", "Right")
                            lbl = New Label
                            lbl.Text = "" ' dtCyl.Rows(j - 2).Item("VALUE").ToString()
                            tdInner.Controls.Add(lbl)
                        End If
                        trInner.Controls.Add(tdInner)
                    Next
                    If (i Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    tblRunP.Controls.Add(trInner)
                Next
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetRunPDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetRunRDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim txt As New TextBox
        Dim dsPref As New DataSet
        Dim dsMat As New DataSet
        Dim rdb As New RadioButton
        Dim val As New Double
        Dim dvData As New DataView
        Dim dtData As New DataTable
        Dim dsCyl As New DataSet
        Dim dsDataT As New DataSet
        Try
            tblRunR.Rows.Clear()
            dsData = objGetData.GetRunRate(hidEqId.Value, ddlDate.SelectedValue)
            dvData = dsData.Tables(0).DefaultView
            dsDataT = objGetData.GetJobSeqDetails(hidEqId.Value, ddlDate.SelectedValue, hidLayerId.Value)
            dsPref = objGetData.GetPref(Session("E1CaseId"))

            trHeader = New TableRow
            For i = 1 To 7
                tdHeader = New TableCell
                tdHeader1 = New TableCell

                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "200px", "", "1")
                        HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "180px", "", "2")
                        HeaderTdSetting(tdHeader1, "90px", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader1, "90px", "", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        HeaderTdSetting(tdHeader, "180px", "Suggested", "2")
                        HeaderTdSetting(tdHeader1, "90px", "(" + unit + "/min)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        HeaderTdSetting(tdHeader1, "90px", "(" + unit2 + "/min)", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 6
                        HeaderTdSetting(tdHeader, "180px", "Preferred", "2")
                        HeaderTdSetting(tdHeader1, "90px", "(" + unit + "/min)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 7
                        HeaderTdSetting(tdHeader1, "90px", "(" + unit2 + "/min)", "1")
                        trHeader1.Controls.Add(tdHeader1)
                End Select
            Next
            tblRunR.Controls.Add(trHeader)
            tblRunR.Controls.Add(trHeader1)

            For i = 0 To dsData.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 0 To 6
                    tdInner = New TableCell
                    If j = 0 Then
                        InnerTdSetting(tdInner, "20px", "Center")
                        lbl = New Label
                        lbl.Width = 120
                        If dsData.Tables(0).Rows(0).Item("ISVISIBLE").ToString() = "N" Then
                            lbl.Text = "Registration Run Rate Not Used for this Equipment"
                        Else
                            lbl.Text = "Registration Run Rate Used for this Equipment"
                        End If
                        'lbl.Text = "Registration Run Rate"
                        tdInner.Controls.Add(lbl)
                    ElseIf j = 1 Then
                        InnerTdSetting(tdInner, "90px", "Right")
                        lbl = New Label
                        lbl.Text = dsData.Tables(0).Rows(i).Item("VALUE").ToString()
                        regValue = dsData.Tables(0).Rows(i).Item("VALUE")
                        tdInner.Controls.Add(lbl)
                    ElseIf j = 2 Then
                        InnerTdSetting(tdInner, "90px", "Left")
                        lbl = New Label
                        lbl.Width = 90
                        lbl.Text = "% " + dsData.Tables(0).Rows(i).Item("DESCRIPTION").ToString()
                        tdInner.Controls.Add(lbl)
                    ElseIf j = 3 Then
                        InnerTdSetting(tdInner, "90px", "Right")
                        lbl = New Label
                        If URpPref <> 0.0 Then
                            lbl.Text = (URpPref * dsData.Tables(0).Rows(i).Item("VALUE")) / 100
                            URegVal = (URpPref * dsData.Tables(0).Rows(i).Item("VALUE")) / 100
                        Else
                            lbl.Text = (TotalRunP * dsData.Tables(0).Rows(i).Item("VALUE")) / 100
                            URegVal = (TotalRunP * dsData.Tables(0).Rows(i).Item("VALUE")) / 100
                        End If

                        tdInner.Controls.Add(lbl)
                    ElseIf j = 4 Then
                        InnerTdSetting(tdInner, "90px", "Right")
                        lbl = New Label
                        If unit <> "pieces" Then
                            lbl.Text = FormatNumber(((TotalRunP * dsData.Tables(0).Rows(i).Item("VALUE")) / 100) / 3.27, 0)
                        End If
                        tdInner.Controls.Add(lbl)
                    ElseIf j = 5 Then
                        InnerTdSetting(tdInner, "90px", "Left")
                        txt = New TextBox
                        txt.ID = "txtRrPref"
                        txt.CssClass = "SmallTextBox"
                        If URrPref <> 0.0 Then
                            txt.Text = URrPref.ToString()
                            'URegVal = URrPref / 100
                            URegVal = URrPref '/ 100
                        End If
                        If dsData.Tables(0).Rows(0).Item("ISVISIBLE").ToString() = "N" Then
                            txt.Enabled = False
                            txt.BackColor = Drawing.Color.DarkGray
                        End If
                        tdInner.Controls.Add(txt)
                    Else
                        InnerTdSetting(tdInner, "90px", "Right")
                        lbl = New Label
                        If unit <> "pieces" Then
                            lbl.Text = FormatNumber(URrPref / 3.27, 0)
                        End If
                        tdInner.Controls.Add(lbl)
                    End If
                    trInner.Controls.Add(tdInner)
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                tblRunR.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetRunRDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetRunWDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim txt As New TextBox
        Dim dsPref As New DataSet
        Dim dsMat As New DataSet
        Dim rdb As New RadioButton
        Dim val As New Double
        Dim dvData As New DataView
        Dim dtData As New DataTable
        Dim dsCyl As New DataSet
        Dim dsDataT As New DataSet
        Try
            tblRunW.Rows.Clear()
            dsData = objGetData.GetRunWaste(hidEqId.Value, ddlDate.SelectedValue)
            dvData = dsData.Tables(0).DefaultView
            dsDataT = objGetData.GetRunWasteT(hidEqId.Value, ddlDate.SelectedValue)
            dsPref = objGetData.GetPref(Session("E1CaseId"))

            trHeader = New TableRow
            For i = 1 To 5
                tdHeader = New TableCell
                tdHeader1 = New TableCell

                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "300px", "", "2")
                        HeaderTdSetting(tdHeader1, "0", "", "2")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "180px", "Suggested", "2")
                        HeaderTdSetting(tdHeader1, "80px", "(" + unit + "/min)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader1, "80px", "(" + unit2 + "/min)", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        HeaderTdSetting(tdHeader, "180px", "Preferred", "2")
                        HeaderTdSetting(tdHeader1, "80px", "(" + unit + "/min)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        HeaderTdSetting(tdHeader1, "0", "(" + unit2 + "/min)", "1")
                        trHeader1.Controls.Add(tdHeader1)
                End Select
            Next
            tblRunW.Controls.Add(trHeader)
            'tblRunW.Controls.Add(trHeader1)

            Dim RwProc As New Double
            Dim RwReg As New Double
            For i = 0 To dsDataT.Tables(0).Rows.Count - 1
                dvData.RowFilter = "TYPE='" + dsDataT.Tables(0).Rows(i).Item("TYPE").ToString() + "'"
                dtData = dvData.ToTable()
                For k = 0 To dtData.Rows.Count - 1
                    If dsDataT.Tables(0).Rows(i).Item("TYPE").ToString() = "PROC" Then
                        If hidSelRWProc.Value <> "" Then
                            If dtData.Rows(k).Item("PWRUNWASTEID").ToString() = hidSelRWProc.Value Then
                                RwProc = dtData.Rows(k).Item("VALUE").ToString()
                                '    arrRWProc(k) = dtData.Rows(k).Item("VALUE").ToString()
                                'Else
                            End If
                        Else
                            If dtData.Rows(k).Item("ISDEFAULT").ToString() = "Y" Then
                                RwProc = dtData.Rows(k).Item("VALUE").ToString()
                                '    arrRWProc(k) = dtData.Rows(k).Item("VALUE").ToString()
                                'Else
                            End If
                        End If
                        arrRWProc(k) = dtData.Rows(k).Item("VALUE").ToString()
                    Else
                        If hidSelRWReg.Value <> "" Then
                            If dtData.Rows(k).Item("PWRUNWASTEID").ToString() = hidSelRWReg.Value Then
                                RwReg = dtData.Rows(k).Item("VALUE").ToString()
                                '    arrRWReg(k) = dtData.Rows(k).Item("VALUE").ToString()
                                'Else
                            End If
                        Else
                            If dtData.Rows(k).Item("ISDEFAULT").ToString() = "Y" Then
                                RwReg = dtData.Rows(k).Item("VALUE").ToString()
                                '    arrRWReg(k) = dtData.Rows(k).Item("VALUE").ToString()
                                'Else
                            End If
                        End If
                        arrRWReg(k) = dtData.Rows(k).Item("VALUE").ToString()
                    End If
                Next
            Next
            If hidRWProc.Value <> "" And hidRWReg.Value <> "" Then
                TotalRunW = (URWProc + URWReg)
            Else
                If hidRWProc.Value = "" And hidRWReg.Value = "" Then
                    TotalRunW = (RwProc + RwReg)
                ElseIf hidRWProc.Value = "" Then
                    TotalRunW = (RwProc + URWReg)
                ElseIf hidRWReg.Value = "" Then
                    TotalRunW = (URWProc + RwReg)
                End If
            End If

            trInner = New TableRow
            For j = 0 To 4
                tdInner = New TableCell

                If j = 0 Then
                    InnerTdSetting(tdInner, "300px", "Center")
                    lbl.Width = 300
                    lbl = New Label()
                    lbl.Text = "Run Time Waste"
                    tdInner.ColumnSpan = "2"
                    tdInner.Controls.Add(lbl)
                    trInner.Controls.Add(tdInner)
                ElseIf j = 1 Then
                    InnerTdSetting(tdInner, "", "Right")
                    lbl = New Label()
                    lbl.Text = TotalRunW
                    tdInner.Controls.Add(lbl)
                    trInner.Controls.Add(tdInner)
                ElseIf j = 2 Then
                    InnerTdSetting(tdInner, "", "Right")
                    lbl = New Label()
                    lbl.Text = "%"
                    tdInner.Controls.Add(lbl)
                    trInner.Controls.Add(tdInner)
                ElseIf j = 3 Then
                    InnerTdSetting(tdInner, "", "Center")
                    txt = New TextBox
                    txt.ID = "txtRwPref"
                    txt.CssClass = "SmallTextBox"
                    If URwPref <> 0.0 Then
                        txt.Text = URwPref.ToString()
                        'TotalRunW = URwPref
                    End If
                    tdInner.Controls.Add(txt)
                    trInner.Controls.Add(tdInner)
                ElseIf j = 4 Then
                    InnerTdSetting(tdInner, "", "Right")
                    lbl = New Label()
                    lbl.Text = "%"
                    tdInner.Controls.Add(lbl)
                    trInner.Controls.Add(tdInner)
                End If
                trInner.CssClass = "AlterNateColor1"
            Next
            tblRunW.Controls.Add(trInner)

            For i = 0 To dsDataT.Tables(0).Rows.Count - 1
                dvData.RowFilter = "TYPE='" + dsDataT.Tables(0).Rows(i).Item("TYPE").ToString() + "'"
                dtData = dvData.ToTable()
                For k = 0 To dtData.Rows.Count - 1
                    trInner = New TableRow
                    For j = 0 To 5
                        tdInner = New TableCell
                        If j = 0 Then
                            InnerTdSetting(tdInner, "20px", "Center")
                            rdb = New RadioButton
                            rdb.GroupName = "rdbRunW" + dsDataT.Tables(0).Rows(i).Item("TYPE").ToString()
                            rdb.ID = "rdbRunW" + dsDataT.Tables(0).Rows(i).Item("TYPE").ToString() + "_" + (k + 1).ToString()
                            If dsDataT.Tables(0).Rows(i).Item("TYPE").ToString() = "PROC" Then
                                'If hidRWProc.Value <> "" Then
                                '    If hidRWProc.Value = rdb.ID Then
                                '        rdb.Checked = True
                                '        URWProc = dtData.Rows(k).Item("VALUE")
                                '    Else
                                '        rdb.Checked = False
                                '    End If
                                'Else
                                If hidSelRWProc.Value <> "" Then
                                    If dtData.Rows(k).Item("PWRUNWASTEID").ToString() = hidSelRWProc.Value Then
                                        dcRadioSel(6) = dtData.Rows(k).Item("PWRUNWASTEID")
                                        URWProc = dtData.Rows(k).Item("VALUE").ToString()
                                        rdb.Checked = True
                                    Else
                                        rdb.Checked = False
                                    End If
                                Else
                                    If dtData.Rows(k).Item("ISDEFAULT").ToString() = "Y" Then
                                        dcRadioSel(6) = dtData.Rows(k).Item("PWRUNWASTEID")
                                        URWProc = dtData.Rows(k).Item("VALUE").ToString()
                                        rdb.Checked = True
                                    Else
                                        rdb.Checked = False
                                    End If
                                End If
                                'End If
                                arrRWProc(k) = dtData.Rows(k).Item("VALUE").ToString()
                                arrRWProcId(k) = dtData.Rows(k).Item("PWRUNWASTEID").ToString()
                            Else
                                'If hidRWReg.Value <> "" Then
                                '    If hidRWReg.Value = rdb.ID Then
                                '        rdb.Checked = True
                                '        URWReg = dtData.Rows(k).Item("VALUE")
                                '    Else
                                '        rdb.Checked = False
                                '    End If
                                'Else
                                If hidSelRWReg.Value <> "" Then
                                    If dtData.Rows(k).Item("PWRUNWASTEID").ToString() = hidSelRWReg.Value Then
                                        dcRadioSel(7) = dtData.Rows(k).Item("PWRUNWASTEID")
                                        URWReg = dtData.Rows(k).Item("VALUE").ToString()
                                        rdb.Checked = True
                                    Else
                                        rdb.Checked = False
                                    End If
                                Else
                                    If dtData.Rows(k).Item("ISDEFAULT").ToString() = "Y" Then
                                        dcRadioSel(7) = dtData.Rows(k).Item("PWRUNWASTEID")
                                        URWReg = dtData.Rows(k).Item("VALUE").ToString()
                                        rdb.Checked = True
                                    Else
                                        rdb.Checked = False
                                    End If
                                End If
                                'End If
                                arrRWReg(k) = dtData.Rows(k).Item("VALUE").ToString()
                                arrRWRegId(k) = dtData.Rows(k).Item("PWRUNWASTEID").ToString()
                            End If

                            AddHandler rdb.CheckedChanged, AddressOf rdb_CheckedChanged
                            rdb.AutoPostBack = True
                            tdInner.Controls.Add(rdb)
                        ElseIf j = 1 Then
                            InnerTdSetting(tdInner, "90px", "Left")

                            lbl = New Label
                            'lbl.Width = 80
                            lbl.Text = dtData.Rows(k).Item("DESCRIPTION").ToString()
                            tdInner.Controls.Add(lbl)
                        ElseIf j = 2 Then
                            InnerTdSetting(tdInner, "90px", "Right")
                            lbl = New Label
                            'lbl.Width = 90
                            lbl.Text = dtData.Rows(k).Item("VALUE").ToString()
                            tdInner.Controls.Add(lbl)
                        ElseIf j = 3 Then
                            InnerTdSetting(tdInner, "90px", "Right")
                            lbl = New Label
                            'lbl.Width = 80
                            lbl.Text = "%" ' dtData.Rows(k).Item("VALUE").ToString()
                            tdInner.Controls.Add(lbl)
                        Else
                            InnerTdSetting(tdInner, "90px", "Right")
                            lbl = New Label
                            'lbl.Width = 90
                            lbl.Text = "" ' dtCyl.Rows(j - 2).Item("VALUE").ToString()
                            tdInner.Controls.Add(lbl)
                        End If
                        trInner.Controls.Add(tdInner)
                    Next
                    If (i Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    tblRunW.Controls.Add(trInner)
                Next
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetRunWDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetRegTDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim dsPref As New DataSet
        Dim dsMat As New DataSet
        Dim rdb As New RadioButton
        Dim val As New Double
        Dim dvData As New DataView
        Dim dtData As New DataTable
        Dim dsCyl As New DataSet
        Dim dsDataT As New DataSet
        Dim txt As New TextBox
        Try
            tblRegT.Rows.Clear()

            dsData = objGetData.GetRegistrationTime(hidEqId.Value, ddlDate.SelectedValue)
            dvData = dsData.Tables(0).DefaultView
            dsDataT = objGetData.GetRegistrationTimeT(hidEqId.Value, ddlDate.SelectedValue)
            dsPref = objGetData.GetPref(Session("E1CaseId"))
            'lblTitle.Text = "Material " + ds.Tables(0).Rows(0).Item("MATID").ToString() + ":" + ds.Tables(0).Rows(0).Item("MATDES").ToString()

            trHeader = New TableRow

            For i = 1 To 5
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "200px", "", "2")
                        HeaderTdSetting(tdHeader1, "0", "", "2")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "180px", "Suggested", "2")
                        HeaderTdSetting(tdHeader1, "90px", "(" + unit + "/min)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader1, "90px", "(" + unit2 + "/min)", "1")
                        trHeader1.Controls.Add(tdHeader1)
                    Case 4
                        HeaderTdSetting(tdHeader, "180px", "Preferred", "2")
                        HeaderTdSetting(tdHeader1, "90px", "(" + unit + "/min)", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        HeaderTdSetting(tdHeader1, "90px", "(" + unit2 + "/min)", "1")
                        trHeader1.Controls.Add(tdHeader1)
                End Select
            Next
            tblRegT.Controls.Add(trHeader)
            'tblRegT.Controls.Add(trHeader1)

            Dim RtProc As New Double
            Dim RtReg As New Double
            For i = 0 To dsDataT.Tables(0).Rows.Count - 1
                dvData.RowFilter = "TYPE='" + dsDataT.Tables(0).Rows(i).Item("TYPE").ToString() + "'"
                dtData = dvData.ToTable()
                For k = 0 To dtData.Rows.Count - 1
                    If dsDataT.Tables(0).Rows(i).Item("TYPE").ToString() = "PROC" Then
                        If hidSelRTProc.Value <> "" Then
                            If dtData.Rows(k).Item("PWREGTIMEID").ToString() = hidSelRTProc.Value Then
                                RtProc = dtData.Rows(k).Item("VALUE").ToString()
                                '    arrRTProc(k) = dtData.Rows(k).Item("VALUE").ToString()
                                'Else
                            End If
                        Else
                            If dtData.Rows(k).Item("ISDEFAULT").ToString() = "Y" Then
                                RtProc = dtData.Rows(k).Item("VALUE").ToString()
                                '    arrRTProc(k) = dtData.Rows(k).Item("VALUE").ToString()
                                'Else
                            End If
                        End If
                        arrRTProc(k) = dtData.Rows(k).Item("VALUE").ToString()
                    Else
                        If hidSelRTReg.Value <> "" Then
                            If dtData.Rows(k).Item("PWREGTIMEID").ToString() = hidSelRTReg.Value Then
                                RtReg = dtData.Rows(k).Item("VALUE").ToString()
                                '    arrRTReg(k) = dtData.Rows(k).Item("VALUE").ToString()
                                'Else
                                '    arrRTReg(k) = dtData.Rows(k).Item("VALUE").ToString()
                            End If
                        Else
                            If dtData.Rows(k).Item("ISDEFAULT").ToString() = "Y" Then
                                RtReg = dtData.Rows(k).Item("VALUE").ToString()
                                '    arrRTReg(k) = dtData.Rows(k).Item("VALUE").ToString()
                                'Else
                                '    arrRTReg(k) = dtData.Rows(k).Item("VALUE").ToString()
                            End If
                        End If
                        arrRTReg(k) = dtData.Rows(k).Item("VALUE").ToString()
                    End If
                Next
            Next
            If hidRTProc.Value <> "" And hidRTReg.Value <> "" Then
                TotalRegT = (URTProc + URTReg)
            Else
                If hidRTProc.Value = "" And hidRTReg.Value = "" Then
                    TotalRegT = (RtProc + RtReg)
                ElseIf hidRTProc.Value = "" Then
                    TotalRegT = (RtProc + URTReg)
                ElseIf hidRTReg.Value = "" Then
                    TotalRegT = (URTProc + RtReg)
                End If
            End If

            trInner = New TableRow
            For j = 0 To 4
                tdInner = New TableCell

                If j = 0 Then
                    InnerTdSetting(tdInner, "200px", "Center")
                    lbl = New Label()
                    lbl.Width = 200
                    If dsData.Tables(0).Rows.Count() > 0 Then
                        If dsData.Tables(0).Rows(0).Item("ISVISIBLE").ToString() = "N" Then
                            lbl.Text = "Registration Time Not Used for this Equipment"
                        Else
                            lbl.Text = "Registration Time Used for this Equipment"
                        End If
                    Else
                        lbl.Text = "Registration Time Not Used for this Equipment"
                    End If
                    'lbl.Text = "Registration Time"
                    tdInner.ColumnSpan = "2"
                    tdInner.Controls.Add(lbl)
                    trInner.Controls.Add(tdInner)
                ElseIf j = 1 Then
                    InnerTdSetting(tdInner, "", "Right")
                    lbl = New Label()
                    lbl.Text = TotalRegT
                    tdInner.Controls.Add(lbl)
                    trInner.Controls.Add(tdInner)
                ElseIf j = 2 Then
                    InnerTdSetting(tdInner, "", "Center")
                    lbl = New Label()
                    lbl.Text = "(min/graphic)"
                    tdInner.Controls.Add(lbl)
                    trInner.Controls.Add(tdInner)
                ElseIf j = 3 Then
                    InnerTdSetting(tdInner, "", "Center")
                    txt = New TextBox
                    txt.ID = "txtRtPref"
                    txt.CssClass = "SmallTextBox"
                    If URtPref <> 0.0 Then
                        txt.Text = URtPref.ToString()
                        'TotalRegT = URtPref 
                    End If
                    If dsData.Tables(0).Rows.Count() > 0 Then
                        If dsData.Tables(0).Rows(0).Item("ISVISIBLE").ToString() = "N" Then
                            txt.Enabled = False
                            txt.BackColor = Drawing.Color.DarkGray
                        End If
                    Else
                        txt.Enabled = False
                        txt.BackColor = Drawing.Color.DarkGray
                    End If
                    tdInner.Controls.Add(txt)
                    trInner.Controls.Add(tdInner)
                ElseIf j = 4 Then
                InnerTdSetting(tdInner, "", "Center")
                lbl = New Label()
                lbl.Text = "(min/graphic)"
                tdInner.Controls.Add(lbl)
                trInner.Controls.Add(tdInner)
                End If
                trInner.CssClass = "AlterNateColor1"
            Next
            tblRegT.Controls.Add(trInner)

            

            For i = 0 To dsDataT.Tables(0).Rows.Count - 1

                dvData.RowFilter = "TYPE='" + dsDataT.Tables(0).Rows(i).Item("TYPE").ToString() + "'"
                dtData = dvData.ToTable()
                For k = 0 To dtData.Rows.Count - 1
                    trInner = New TableRow
                    For j = 0 To 5
                        tdInner = New TableCell
                        If j = 0 Then
                            InnerTdSetting(tdInner, "20px", "Center")
                            rdb = New RadioButton
                            rdb.GroupName = "rdb" + dsDataT.Tables(0).Rows(i).Item("TYPE").ToString()
                            rdb.ID = "rdbRegT" + dsDataT.Tables(0).Rows(i).Item("TYPE").ToString() + "_" + (k + 1).ToString()
                            If dsDataT.Tables(0).Rows(i).Item("TYPE").ToString() = "PROC" Then
                                'If hidRTProc.Value <> "" Then
                                '    If hidRTProc.Value = rdb.ID Then
                                '        rdb.Checked = True
                                '        URTProc = dtData.Rows(k).Item("VALUE")
                                '    Else
                                '        rdb.Checked = False
                                '    End If
                                'Else
                                If hidSelRTProc.Value <> "" Then
                                    If dtData.Rows(k).Item("PWREGTIMEID").ToString() = hidSelRTProc.Value Then
                                        dcRadioSel(8) = dtData.Rows(k).Item("PWREGTIMEID")
                                        URTProc = dtData.Rows(k).Item("VALUE").ToString()
                                        rdb.Checked = True
                                    Else
                                        rdb.Checked = False
                                    End If
                                Else
                                    If dtData.Rows(k).Item("ISDEFAULT").ToString() = "Y" Then
                                        dcRadioSel(8) = dtData.Rows(k).Item("PWREGTIMEID")
                                        URTProc = dtData.Rows(k).Item("VALUE").ToString()
                                        rdb.Checked = True
                                    Else
                                        rdb.Checked = False
                                    End If
                                End If
                                'End If
                                arrRTProc(k) = dtData.Rows(k).Item("VALUE").ToString()
                                arrRTProcId(k) = dtData.Rows(k).Item("PWREGTIMEID").ToString()
                            Else
                                'If hidRTReg.Value <> "" Then
                                '    If hidRTReg.Value = rdb.ID Then
                                '        rdb.Checked = True
                                '        URTReg = dtData.Rows(k).Item("VALUE")
                                '    Else
                                '        rdb.Checked = False
                                '    End If
                                'Else
                                If hidSelRTReg.Value <> "" Then
                                    If dtData.Rows(k).Item("PWREGTIMEID").ToString() = hidSelRTReg.Value Then
                                        dcRadioSel(9) = dtData.Rows(k).Item("PWREGTIMEID")
                                        URTReg = dtData.Rows(k).Item("VALUE").ToString()
                                        rdb.Checked = True
                                    Else
                                        rdb.Checked = False
                                    End If
                                Else
                                    If dtData.Rows(k).Item("ISDEFAULT").ToString() = "Y" Then
                                        dcRadioSel(9) = dtData.Rows(k).Item("PWREGTIMEID")
                                        URTReg = dtData.Rows(k).Item("VALUE").ToString()
                                        rdb.Checked = True
                                    Else
                                        rdb.Checked = False
                                    End If
                                End If
                                'End If
                                arrRTReg(k) = dtData.Rows(k).Item("VALUE").ToString()
                                arrRTRegId(k) = dtData.Rows(k).Item("PWREGTIMEID").ToString()
                            End If
                            'If dtData.Rows(k).Item("ISDEFAULT").ToString() = "Y" Then
                            '    rdb.Checked = True
                            'Else
                            '    rdb.Checked = False
                            'End If
                            AddHandler rdb.CheckedChanged, AddressOf rdb_CheckedChanged
                            rdb.AutoPostBack = True
                            tdInner.Controls.Add(rdb)
                        ElseIf j = 1 Then
                            InnerTdSetting(tdInner, "90px", "Left")
                            lbl = New Label
                            lbl.Text = dtData.Rows(k).Item("DESCRIPTION").ToString()
                            tdInner.Controls.Add(lbl)
                        ElseIf j = 2 Then
                            InnerTdSetting(tdInner, "90px", "Right")
                            lbl = New Label
                            lbl.Text = dtData.Rows(k).Item("VALUE").ToString()
                            tdInner.Controls.Add(lbl)
                        ElseIf j = 3 Then
                            InnerTdSetting(tdInner, "90px", "Center")
                            lbl = New Label
                            lbl.Text = "(min/graphic)" 'dtData.Rows(k).Item("VALUE").ToString()
                            tdInner.Controls.Add(lbl)
                        Else
                            InnerTdSetting(tdInner, "90px", "Right")
                            lbl = New Label
                            lbl.Text = "" ' dtCyl.Rows(j - 2).Item("VALUE").ToString()
                            tdInner.Controls.Add(lbl)
                        End If
                        trInner.Controls.Add(tdInner)
                    Next
                    If (i Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    tblRegT.Controls.Add(trInner)
                Next
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetRegTDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetMaintTDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim dsPref As New DataSet
        Dim dsMat As New DataSet
        Dim rdb As New RadioButton
        Dim val As New Double
        Dim txt As New TextBox
        Dim dvData As New DataView
        Dim dtData As New DataTable
        Dim dsCyl As New DataSet
        Dim dsDataT As New DataSet
        Try
            tblMainT.Rows.Clear()
            'ds = objGetData.GetMaterials(hidEqId.Value, "", "")
            'dsMat = objGetData.GetMatArch(hidEqId.Value, Session("E1CaseId"))
            dsData = objGetData.GetMaintTime(hidEqId.Value, ddlDate.SelectedValue)
            dvData = dsData.Tables(0).DefaultView
            dsDataT = objGetData.GetJobSeqDetails(hidEqId.Value, ddlDate.SelectedValue, hidLayerId.Value)
            dsPref = objGetData.GetPref(Session("E1CaseId"))
            'lblTitle.Text = "Material " + ds.Tables(0).Rows(0).Item("MATID").ToString() + ":" + ds.Tables(0).Rows(0).Item("MATDES").ToString()

            trHeader = New TableRow
            For i = 1 To 3
                tdHeader = New TableCell
                tdHeader1 = New TableCell

                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "200px", "", "1")
                        'HeaderTdSetting(tdHeader1, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        'trHeader1.Controls.Add(tdHeader1)
                    Case 2
                        HeaderTdSetting(tdHeader, "180px", "Suggested", "2")
                        'HeaderTdSetting(tdHeader1, "0", "Feet/min", "1")
                        trHeader.Controls.Add(tdHeader)
                        'trHeader1.Controls.Add(tdHeader1)
                    Case 3
                        HeaderTdSetting(tdHeader, "200px", "Preferred", "2")
                        'HeaderTdSetting(tdHeader1, "0", "(feet/min)", "1")
                        trHeader.Controls.Add(tdHeader)
                    Case 4
                        HeaderTdSetting(tdHeader, "180px", "Preferred", "2")
                        'HeaderTdSetting(tdHeader1, "0", "(feet/min)", "1")
                        trHeader.Controls.Add(tdHeader)
                        'trHeader1.Controls.Add(tdHeader1)
                    Case 5
                        'HeaderTdSetting(tdHeader, "200px", "Preferred", "")
                        'HeaderTdSetting(tdHeader1, "0", "(meter/min)", "1")
                        'trHeader.Controls.Add(tdHeader)
                        'trHeader1.Controls.Add(tdHeader1)
                End Select
            Next
            tblMainT.Controls.Add(trHeader)
            'tblMainT.Controls.Add(trHeader1)

            For i = 0 To dsData.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 0 To 4
                    tdInner = New TableCell
                    If j = 0 Then
                        InnerTdSetting(tdInner, "20px", "Center")
                        lbl = New Label
                        lbl.Text = "Maintenance Time"
                        tdInner.Controls.Add(lbl)
                    ElseIf j = 1 Then
                        InnerTdSetting(tdInner, "60px", "Right")
                        lbl = New Label
                        lbl.Text = dsData.Tables(0).Rows(i).Item("VALUE").ToString()
                        UMainVal = dsData.Tables(0).Rows(i).Item("VALUE")
                        tdInner.Controls.Add(lbl)
                    ElseIf j = 2 Then
                        InnerTdSetting(tdInner, "120px", "Left")
                        lbl = New Label
                        lbl.Text = "% " + dsData.Tables(0).Rows(i).Item("DESCRIPTION").ToString()
                        tdInner.Controls.Add(lbl)
                    ElseIf j = 4 Then
                        InnerTdSetting(tdInner, "90px", "Right")
                        lbl = New Label
                        lbl.Text = "% "
                        tdInner.Controls.Add(lbl)
                    ElseIf j = 3 Then
                        InnerTdSetting(tdInner, "90px", "Center")
                        txt = New TextBox
                        txt.ID = "txtMtPref"
                        txt.CssClass = "SmallTextBox"
                        If UMtPref <> 0.0 Then
                            txt.Text = UMtPref.ToString()
                            'UMainVal = UMtPref
                        End If
                        tdInner.Controls.Add(txt)
                    End If
                    trInner.Controls.Add(tdInner)
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If

                tblMainT.Controls.Add(trInner)
            Next


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetMaintTDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetJobSeqDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim trHeader1 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim dsPref As New DataSet
        Dim dsMat As New DataSet
        Dim rdb As New RadioButton
        Dim val As New Double
        Dim dvData As New DataView
        Dim dtData As New DataTable
        Dim dsCyl As New DataSet
        Dim dsDataT As New DataSet
        Dim txt As New TextBox
        Dim lnkbtn As New LinkButton
        Dim count As New Integer
        Dim TotalCol As New Double
        Dim TotalSize As New Double
        Dim TotalCyl As New Double
        Dim dsEquip As DataSet
        Dim dsColumn As New DataSet
        Dim columnValTotal(5) As Integer
        Try
            tblJSeq.Rows.Clear()
            dsEquip = Session("dsEquip")
            dsData = objGetData.GetJobSeqDetails(Session("E1CaseId"), hidEqId.Value, hidLayerId.Value)
Session("dsJobSeqData") = dsData
            dsColumn = objGetData.GetPrefferedColumn(hidEqId.Value, "Input")
            columnCount = dsColumn.Tables(0).Rows.Count
            If hidRowNum.Value <> "1" Then
                count = Convert.ToInt32(hidRowNum.Value)
            Else
                If dsData.Tables(0).Rows.Count > 0 Then
                    count = dsData.Tables(0).Rows.Count + 1
                    hidRowNum.Value = dsData.Tables(0).Rows.Count + 1
                Else
                    count = Convert.ToInt32(hidRowNum.Value)
                End If
            End If

            trHeader = New TableRow
            For i = 1 To dsColumn.Tables(0).Rows.Count + 3
                tdHeader = New TableCell
                tdHeader1 = New TableCell

                Dim Title As String = String.Empty
                'Header
                If i = 1 Then
                    HeaderTdSetting(tdHeader, "200px", "Design", "1")
                    HeaderTdSetting(tdHeader1, "0", "", "1")
                    trHeader.Controls.Add(tdHeader)
                    trHeader1.Controls.Add(tdHeader1)
                ElseIf i = 2 Then
                    HeaderTdSetting(tdHeader, "180px", "Run Size", "1")
                    HeaderTdSetting(tdHeader1, "0", "(" + unit + ")", "1")
                    trHeader.Controls.Add(tdHeader)
                    trHeader1.Controls.Add(tdHeader1)
                ElseIf i = 3 Then
                    HeaderTdSetting(tdHeader, "180px", "", "1")
                    HeaderTdSetting(tdHeader1, "0", "(" + unit2 + ")", "1")
                    trHeader.Controls.Add(tdHeader)
                    trHeader1.Controls.Add(tdHeader1)
                Else
                    HeaderTdSetting(tdHeader, "180px", dsColumn.Tables(0).Rows(i - 4).Item("COLUMNNAME").ToString(), "1")
                    HeaderTdSetting(tdHeader1, "0", dsColumn.Tables(0).Rows(i - 4).Item("UNIT").ToString(), "1")
                    trHeader.Controls.Add(tdHeader)
                    trHeader1.Controls.Add(tdHeader1)
                End If
            Next
            tblJSeq.Controls.Add(trHeader)
            tblJSeq.Controls.Add(trHeader1)

            For i = 1 To 71 'dsData.Tables(0).Rows.Count - 1
                trInner = New TableRow
                For j = 0 To dsColumn.Tables(0).Rows.Count + 3
                    tdInner = New TableCell
                    If j = 0 Then
                        InnerTdSetting(tdInner, "20px", "Center")
                        txt = New TextBox
                        txt.CssClass = "MatSmallTextBox"
                        txt.ID = "txtDes" + i.ToString()
                        If i = 71 Then
                            tdInner.Text = "Total"
                        Else
                            If DesgDesc(i - 1) <> "" Then
                                txt.Text = DesgDesc(i - 1)
                            Else
                                If i > count Then
                                    txt.Text = ""
                                Else
                                    If dsData.Tables(0).Rows.Count >= i Then
                                        If dsData.Tables(0).Rows(i - 1).Item("DESGDESC").ToString() <> "" Then
                                            txt.Text = dsData.Tables(0).Rows(i - 1).Item("DESGDESC").ToString()
                                        Else
                                            txt.Text = "Design " + i.ToString()
                                        End If
                                    Else
                                        txt.Text = "Design " + i.ToString()
                                    End If
                                End If
                            End If
                            tdInner.Controls.Add(txt)
                        End If

                    ElseIf j = 1 Then
                        InnerTdSetting(tdInner, "60px", "Right")
                        txt = New TextBox
                        txt.CssClass = "SmallTextBox"
                        txt.ID = "txtRSize" + i.ToString()
                        If i = 71 Then
                            tdInner.Text = TotalSize.ToString()
                            SizeTotal = TotalSize
                        Else
                            If RSize(i - 1) <> "" Then
                                txt.Text = RSize(i - 1)
                            Else
                                If i = count Then
                                    If i <> 1 Then
                                        If Session("Linkbuttonclicked") = "Duplicate" Then
                                            If RSize(i - 2) <> "" Then
                                                txt.Text = RSize(i - 2)
                                            Else
                                                txt.Text = ""
                                            End If
                                        Else
                                            txt.Text = ""
                                        End If

                                    Else
                                        txt.Text = ""
                                    End If
                                Else
                                    If dsData.Tables(0).Rows.Count >= i Then
                                        If dsData.Tables(0).Rows(i - 1).Item("RSIZEVAL").ToString() <> "" Then
                                            txt.Text = dsData.Tables(0).Rows(i - 1).Item("RSIZEVAL").ToString()
                                            TotalSize = TotalSize + dsData.Tables(0).Rows(i - 1).Item("RSIZEVAL")
                                        Else
                                            txt.Text = ""
                                        End If
                                    Else
                                        txt.Text = ""
                                    End If
                                End If
                            End If
                            tdInner.Controls.Add(txt)
                        End If

                    ElseIf j = 2 Then
                        InnerTdSetting(tdInner, "120px", "Right")
                        lbl = New Label
                        If i = 71 Then
                            If unit <> "pieces" Then
                                tdInner.Text = FormatNumber(TotalSize / 3.27, 0)
                            End If
                        Else
                            If RSize(i - 1) <> "" Then
                                If unit <> "pieces" Then
                                    lbl.Text = FormatNumber(RSize(i - 1) / 3.27, 0)
                                End If
                            Else
                                If i > count Then
                                    lbl.Text = ""
                                Else
                                    If dsData.Tables(0).Rows.Count >= i Then
                                        If dsData.Tables(0).Rows(i - 1).Item("RSIZEVAL").ToString() <> "" Then
                                            If unit <> "pieces" Then
                                                lbl.Text = FormatNumber(dsData.Tables(0).Rows(i - 1).Item("RSIZEVAL") / 3.27, 0)
                                            End If
                                        Else
                                            lbl.Text = ""
                                        End If
                                    Else
                                        lbl.Text = ""
                                    End If
                                End If
                            End If
                            tdInner.Controls.Add(lbl)
                        End If
                    ElseIf j = dsColumn.Tables(0).Rows.Count + 3 Then
                        InnerTdSetting(tdInner, "", "Left")
                        InnerTdSetting(tdInner, "", "Center")
                        lbl = New Label
                        lnkbtn = New LinkButton
                        lnkbtn.ID = "lnkAdd" + i.ToString()
                        lnkbtn.Text = "Add More+"
                        lnkbtn.Width = 80
                        lnkbtn.Height = 20
                        lbl.Text = "OR"
                        If i < count Then
                            tdInner.Style.Add("display", "none")
                        ElseIf i = 71 Then
                            tdInner.Style.Add("display", "none")
                        End If
                        AddHandler lnkbtn.Click, AddressOf LinkButton_Click
                        tdInner.Controls.Add(lnkbtn)
                        tdInner.Controls.Add(lbl)
                        trInner.Controls.Add(tdInner)


                        InnerTdSetting(tdInner, "", "Left")
                        lnkbtn = New LinkButton
                        lnkbtn.ID = "lnkDup" + i.ToString()
                        lnkbtn.Text = "Duplicate data"
                        lnkbtn.Width = 80
                        lnkbtn.Height = 20
                        If i < count Then
                            tdInner.Style.Add("display", "none")
                        ElseIf i = 71 Then
                            tdInner.Style.Add("display", "none")
                        End If
                        AddHandler lnkbtn.Click, AddressOf LinkButton_Click1
                        tdInner.Controls.Add(lnkbtn)
                        trInner.Controls.Add(tdInner)
                    Else
                        InnerTdSetting(tdInner, "90px", "Right")
                        txt = New TextBox
                        txt.CssClass = "SmallTextBox"
                        txt.ID = "txt_" + i.ToString() + "_" + (j - 2).ToString()
                        colName(j - 3) = dsColumn.Tables(0).Rows(j - 3).Item("COLUMNVALUE").ToString()
                        If i = 71 Then
                            tdInner.Text = columnValTotal(j - 2).ToString()
                            CylTotal = TotalCyl
                        Else
                            If columnVal(i - 1, j - 3) <> "" Then
                                txt.Text = columnVal(i - 1, j - 3)
                            Else
                                If i = count Then
                                    If i <> 1 Then
                                        If Session("Linkbuttonclicked") = "Duplicate" Then
                                            If columnVal(i - 2, j - 3) <> "" Then
                                                txt.Text = columnVal(i - 2, j - 3)
                                            Else
                                                txt.Text = ""
                                            End If
                                        Else
                                            txt.Text = ""
                                        End If
                                    Else
                                        txt.Text = ""
                                    End If
                                Else
                                    If dsData.Tables(0).Rows.Count >= i Then
                                        Dim colValue As Double = dsData.Tables(0).Rows(i - 1).Item(dsColumn.Tables(0).Rows(j - 3).Item("COLUMNVALUE").ToString())
                                        If colValue.ToString() <> "" Then
                                            txt.Text = colValue.ToString()
                                            columnValTotal(j - 2) = columnValTotal(j - 2) + colValue
                                        Else
                                            txt.Text = ""
                                        End If
                                    Else
                                        txt.Text = ""
                                    End If
                                End If
                            End If
                            tdInner.Controls.Add(txt)
                        End If
                    End If
                    trInner.Controls.Add(tdInner)
                Next
                If (i Mod 2 = 0) Then
                    trInner.CssClass = "AlterNateColor1"
                Else
                    trInner.CssClass = "AlterNateColor2"
                End If
                If i <> 71 Then
                    If i > count Then
                        trInner.Style.Add("display", "none")
                    End If
                End If
                tblJSeq.Controls.Add(trInner)
            Next

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetJobSeqDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetJobSeqResDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim tdHeader2 As New TableCell
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim dsPref As New DataSet
        Dim dsMat As New DataSet
        Dim rdb As New RadioButton
        Dim val As New Double
        Dim dvData As New DataView
        Dim dtData As New DataTable
        Dim dsCyl As New DataSet
        Dim dsDataT As New DataSet
        Dim txt As New TextBox
        Dim lnkbtn As New LinkButton
        Dim count As New Integer
        Dim dsEquip As DataSet
        Dim PressPrefTotal As New Double
        Dim dsResult As New DataSet
        Dim dsTotal As New DataSet
        Dim dsColumn As New DataSet
        Dim columnCount As Integer
        Try
            tblJSeqR.Rows.Clear()
            dsEquip = Session("dsEquip")
            dsData = objGetData.GetJobSeqDetails(Session("E1CaseId"), hidEqId.Value, hidLayerId.Value)
            dsPref = objGetData.GetJobSeqPrefDetails(Session("E1CaseId"), hidEqId.Value)
            dsResult = objGetData.GetJobResultTime(Session("E1CaseId"), hidEqId.Value, hidLayerId.Value)
            dsTotal = objGetData.GetResultTotal(Session("E1CaseId"), hidEqId.Value, hidLayerId.Value)
            dsColumn = objGetData.GetPrefferedColumn(hidEqId.Value, "Result")
            Session("SeqCount") = dsData.Tables(0).Rows.Count
            columnCount = dsColumn.Tables(0).Rows.Count

            trHeader = New TableRow
            For i = 1 To columnCount + 7 ' 11
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                If i = 1 Then
                    HeaderTdSetting(tdHeader, "200px", "", "1")
                    HeaderTdSetting(tdHeader1, "0", "Job Results Time", "1")
                    HeaderTdSetting(tdHeader2, "0", "", "1")
                    trHeader.Controls.Add(tdHeader)
                    trHeader1.Controls.Add(tdHeader1)
                    trHeader2.Controls.Add(tdHeader2)
                ElseIf i = 2 Then
                    HeaderTdSetting(tdHeader, "200px", "", "1")
                    HeaderTdSetting(tdHeader1, "180px", "Run Time Net", "1")
                    HeaderTdSetting(tdHeader2, "0", "(min)", "1")
                    trHeader.Controls.Add(tdHeader)
                    trHeader1.Controls.Add(tdHeader1)
                    trHeader2.Controls.Add(tdHeader2)
                ElseIf i = 3 Then
                    HeaderTdSetting(tdHeader, "200px", "", "1")
                    HeaderTdSetting(tdHeader1, "200px", "Run Time Gross", "1")
                    HeaderTdSetting(tdHeader2, "0", "(min)", "1")
                    trHeader.Controls.Add(tdHeader)
                    trHeader1.Controls.Add(tdHeader1)
                    trHeader2.Controls.Add(tdHeader2)
                ElseIf i = columnCount + 4 Then
                    HeaderTdSetting(tdHeader, "200px", "", "1")
                    HeaderTdSetting(tdHeader1, "200px", "Preferred", "1")
                    HeaderTdSetting(tdHeader2, "0", "(min)", "1")
                    trHeader.Controls.Add(tdHeader)
                    trHeader1.Controls.Add(tdHeader1)
                    trHeader2.Controls.Add(tdHeader2)
                ElseIf i = columnCount + 5 Then
                    HeaderTdSetting(tdHeader, "200px", "", "1")
                    HeaderTdSetting(tdHeader1, "200px", "Registration Time", "1")
                    HeaderTdSetting(tdHeader2, "0", "(min)", "1")
                    trHeader.Controls.Add(tdHeader)
                    trHeader1.Controls.Add(tdHeader1)
                    trHeader2.Controls.Add(tdHeader2)
                ElseIf i = columnCount + 6 Then
                    HeaderTdSetting(tdHeader, "200px", "", "1")
                    HeaderTdSetting(tdHeader1, "200px", "Maintenance Time", "1")
                    HeaderTdSetting(tdHeader2, "0", "(min)", "1")
                    trHeader.Controls.Add(tdHeader)
                    trHeader1.Controls.Add(tdHeader1)
                    trHeader2.Controls.Add(tdHeader2)
                ElseIf i = columnCount + 7 Then
                    HeaderTdSetting(tdHeader, "200px", "", "1")
                    HeaderTdSetting(tdHeader1, "200px", "Press Total", "1")
                    HeaderTdSetting(tdHeader2, "0", "(min)", "1")
                    trHeader.Controls.Add(tdHeader)
                    trHeader1.Controls.Add(tdHeader1)
                    trHeader2.Controls.Add(tdHeader2)
                Else
                    rdb = New RadioButton
                    rdb.Visible = False

                    If dsColumn.Tables(0).Rows(i - 4).Item("ISSELECT").ToString() = "Y" Then
                        rdb.Visible = True
                        rdb.ID = "rdbRes_" + (i).ToString()
                        rdb.GroupName = "JobResult"
                        If jobResPref <> "" Then
                            If jobResPref = dsColumn.Tables(0).Rows(i - 4).Item("TITLE").ToString() Then
                                rdb.Checked = True
                            Else
                                rdb.Checked = False
                            End If
                        Else
                            If dsColumn.Tables(0).Rows(i - 4).Item("TITLE").ToString() = "Parallel" Then
                                rdb.Checked = True
                            Else
                                rdb.Checked = False
                            End If
                        End If
                        AddHandler rdb.CheckedChanged, AddressOf rdb_CheckedChanged
                        rdb.AutoPostBack = True
                    End If

                    HeaderTdSetting(tdHeader, "200px", "", "1")
                    HeaderTdSetting(tdHeader1, "200px", dsColumn.Tables(0).Rows(i - 4).Item("COLUMNNAME").ToString(), "1")
                    HeaderTdSetting(tdHeader2, "0", "(min)", "1")
                    tdHeader.Controls.Add(rdb)
                    trHeader.Controls.Add(tdHeader)
                    trHeader1.Controls.Add(tdHeader1)
                    trHeader2.Controls.Add(tdHeader2)
                End If
                'Select Case i
                '    Case 5
                '        HeaderTdSetting(tdHeader, "200px", "", "1")
                '        'For k = 0 To dsEquip.Tables(0).Rows.Count - 1
                '        '    If dsEquip.Tables(0).Rows(k).Item("EQUIPDE1").ToString() = "Printing press flexo" Then
                '        '        HeaderTdSetting(tdHeader1, "200px", "Plate Change", "1")
                '        '    Else
                '        '        HeaderTdSetting(tdHeader1, "200px", "Cylinder Change", "1")
                '        '    End If
                '        'Next
                '        If hidEqId.Value = "3" Or hidEqId.Value = "26" Or hidEqId.Value = "83" Then
                '            HeaderTdSetting(tdHeader1, "200px", "Graphic Change", "1")
                '        Else
                '            HeaderTdSetting(tdHeader1, "200px", "Graphic Change", "1")
                '        End If
                '        HeaderTdSetting(tdHeader2, "0", "(min)", "1")
                '        trHeader.Controls.Add(tdHeader)
                '        trHeader1.Controls.Add(tdHeader1)
                '        trHeader2.Controls.Add(tdHeader2)
                '    Case 6
                '        rdb = New RadioButton
                '        rdb.ID = "rdbRes_" + (i - 5).ToString()
                '        rdb.GroupName = "JobResult"
                '        If hidJRes.Value = "Serial" Then
                '            rdb.Checked = True
                '        Else
                '            rdb.Checked = False
                '        End If
                '        AddHandler rdb.CheckedChanged, AddressOf rdb_CheckedChanged
                '        rdb.AutoPostBack = True
                '        HeaderTdSetting(tdHeader, "200px", "", "1")
                '        HeaderTdSetting(tdHeader1, "200px", "Press Serial", "1")
                '        HeaderTdSetting(tdHeader2, "0", "(min)", "1")
                '        tdHeader.Controls.Add(rdb)
                '        trHeader.Controls.Add(tdHeader)
                '        trHeader1.Controls.Add(tdHeader1)
                '        trHeader2.Controls.Add(tdHeader2)
                '    Case 7
                '        rdb = New RadioButton
                '        rdb.ID = "rdbRes_" + (i - 5).ToString()
                '        rdb.GroupName = "JobResult"
                '        If hidJRes.Value = "Parallel" Then
                '            rdb.Checked = True
                '        Else
                '            rdb.Checked = False
                '        End If
                '        AddHandler rdb.CheckedChanged, AddressOf rdb_CheckedChanged
                '        rdb.AutoPostBack = True
                '        HeaderTdSetting(tdHeader, "200px", "", "1")
                '        HeaderTdSetting(tdHeader1, "200px", "Press Parallel", "1")
                '        HeaderTdSetting(tdHeader2, "0", "(min)", "1")
                '        tdHeader.Controls.Add(rdb)
                '        trHeader.Controls.Add(tdHeader)
                '        trHeader1.Controls.Add(tdHeader1)
                '        trHeader2.Controls.Add(tdHeader2)
                'End Select
            Next
            tblJSeqR.Controls.Add(trHeader)
            tblJSeqR.Controls.Add(trHeader1)
            tblJSeqR.Controls.Add(trHeader2)

            Dim MnetVal As New Double
            Dim MgrossVal As New Double
            Dim McolVal As New Double
            Dim McylVal As New Double
            Dim MserVal As New Double
            Dim MregVal As New Double
            Dim MparVal As New Double
            Dim MmainVal As New Double
            Dim MPrefVal As New Double
            Dim MPressTotal As New Double
            If dsResult.Tables(0).Rows.Count > 0 Then
                For i = 0 To dsResult.Tables(0).Rows.Count
                    trInner = New TableRow
                    Dim netVal As New Double
                    Dim grossVal As New Double
                    Dim colVal As New Double
                    Dim cylVal As New Double
                    Dim serVal As New Double
                    Dim regVal As New Double
                    Dim parVal As New Double
                    Dim mainVal As New Double
                    Dim PressTotal As New Double
                    For j = 1 To columnCount + 7 ' 11
                        tdInner = New TableCell
                        If j = 1 Then
                            InnerTdSetting(tdInner, "20px", "Center")
                            lbl = New Label
                            If i = dsData.Tables(0).Rows.Count Then
                                tdInner.Text = "Total"
                            Else
                                lbl.Text = dsResult.Tables(0).Rows(i).Item("DESGDESC").ToString()
                                tdInner.Controls.Add(lbl)
                            End If
                        ElseIf j = 2 Then
                            InnerTdSetting(tdInner, "60px", "Right")
                            lbl = New Label

                            If i = dsData.Tables(0).Rows.Count Then
                                tdInner.Text = FormatNumber(dsTotal.Tables(0).Rows(0).Item("RUNTOTAL").ToString(), 1)
                                TotNetVal = MnetVal
                            Else
                                netVal = dsData.Tables(0).Rows(i).Item("RSIZEVAL") / TotalRunP
                                MnetVal = MnetVal + netVal
                                lbl.Text = FormatNumber(dsResult.Tables(0).Rows(i).Item("RUNNET").ToString(), 1)
                                tdInner.Controls.Add(lbl)
                            End If
                        ElseIf j = 3 Then
                            InnerTdSetting(tdInner, "60px", "Right")
                            lbl = New Label

                            If i = dsData.Tables(0).Rows.Count Then
                                tdInner.Text = FormatNumber(dsTotal.Tables(0).Rows(0).Item("GROSSTOTAL").ToString(), 1)
                                TotGrossVal = MgrossVal
                            Else
                                If URwPref <> 0.0 Then
                                    grossVal = netVal / (1 - (URwPref / 100))
                                    MgrossVal = MgrossVal + grossVal
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(i).Item("RUNGROSS").ToString(), 1)
                                    tdInner.Controls.Add(lbl)
                                Else
                                    grossVal = netVal / (1 - (TotalRunW / 100))
                                    MgrossVal = MgrossVal + grossVal
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(i).Item("RUNGROSS").ToString(), 1)
                                    tdInner.Controls.Add(lbl)
                                End If

                            End If
                        ElseIf j = columnCount + 4 Then
                            InnerTdSetting(tdInner, "60px", "Right")
                            txt = New TextBox
                            txt.CssClass = "SmallTextBox"
                            txt.ID = "txtPref" + (i + 1).ToString()
                            If i = dsData.Tables(0).Rows.Count Then
                                tdInner.Text = PressPrefTotal
                                TotalPref = PressPrefTotal
                                TotalPressPref = MPrefVal
                            Else
                                If dsData.Tables(0).Rows.Count >= i Then
                                    If dsPref.Tables(0).Rows.Count > 0 Then
                                        If dsPref.Tables(0).Rows(0).Item("M" + (i + 1).ToString()).ToString <> "" Then
                                            txt.Text = dsPref.Tables(0).Rows(0).Item("M" + (i + 1).ToString()).ToString
                                            MPrefVal = MPrefVal + dsPref.Tables(0).Rows(0).Item("M" + (i + 1).ToString()).ToString
                                            PressPrefTotal = PressPrefTotal + dsPref.Tables(0).Rows(0).Item("M" + (i + 1).ToString()).ToString
                                        Else
                                            txt.Text = ""
                                            If hidJRes.Value = "Parallel" Then
                                                MPrefVal = MPrefVal + parVal
                                            Else
                                                MPrefVal = MPrefVal + serVal
                                            End If
                                        End If
                                    Else
                                        If hidJRes.Value = "Parallel" Then
                                            MPrefVal = MPrefVal + parVal
                                        Else
                                            MPrefVal = MPrefVal + serVal
                                        End If
                                    End If
                                    tdInner.Controls.Add(txt)
                                End If

                            End If
                        ElseIf j = columnCount + 5 Then
                            InnerTdSetting(tdInner, "60px", "Right")
                            lbl = New Label
                            If i = dsData.Tables(0).Rows.Count Then
                                tdInner.Text = FormatNumber(dsTotal.Tables(0).Rows(0).Item("REGTOTAL").ToString(), 1)
                                TotRegVal = MregVal
                            Else
                                'regVal = dsData.Tables(0).Rows(i).Item("SETUPVAL") * TotalRegT
                                MregVal = MregVal + regVal
                                lbl.Text = FormatNumber(dsResult.Tables(0).Rows(i).Item("REGTIME").ToString(), 1)
                                tdInner.Controls.Add(lbl)
                            End If
                        ElseIf j = columnCount + 6 Then
                            InnerTdSetting(tdInner, "60px", "Right")
                            lbl = New Label
                            If i = dsData.Tables(0).Rows.Count Then
                                'tdInner.Text = MmainVal.ToString()
                                tdInner.Text = FormatNumber(dsTotal.Tables(0).Rows(0).Item("MAINTOTAL").ToString(), 1)
                                TotMainVal = MmainVal
                            Else
                                If UMtPref <> 0.0 Then
                                    mainVal = netVal * UMtPref / 100
                                    MmainVal = MmainVal + mainVal
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(i).Item("MAINTIME").ToString(), 1)
                                    'lbl.Text = mainVal.ToString()
                                    tdInner.Controls.Add(lbl)
                                Else
                                    mainVal = netVal * UMainVal / 100
                                    MmainVal = MmainVal + mainVal
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(i).Item("MAINTIME").ToString(), 1)
                                    'lbl.Text = mainVal.ToString()
                                    tdInner.Controls.Add(lbl)
                                End If
                            End If
                        ElseIf j = columnCount + 7 Then
                            InnerTdSetting(tdInner, "60px", "Right")
                            lbl = New Label
                            If i = dsData.Tables(0).Rows.Count Then
                                tdInner.Text = FormatNumber(dsTotal.Tables(0).Rows(0).Item("TOTALTIME").ToString(), 1)
                                TotPressTotal = MPressTotal
                            Else
                                If dsPref.Tables(0).Rows.Count > 0 Then
                                    If dsPref.Tables(0).Rows(0).Item("M" + (i + 1).ToString()).ToString <> "" Then
                                        PressTotal = grossVal + dsPref.Tables(0).Rows(0).Item("M" + (i + 1).ToString()) + regVal + mainVal
                                    Else
                                        If hidJRes.Value = "Parallel" Then
                                            PressTotal = grossVal + parVal + regVal + mainVal
                                        Else
                                            PressTotal = grossVal + serVal + regVal + mainVal
                                        End If
                                    End If
                                Else
                                    If hidJRes.Value = "Parallel" Then
                                        PressTotal = grossVal + parVal + regVal + mainVal
                                    Else
                                        PressTotal = grossVal + serVal + regVal + mainVal
                                    End If
                                End If

                                MPressTotal = MPressTotal + PressTotal
                                lbl.Text = FormatNumber(dsResult.Tables(0).Rows(i).Item("TOTALTIME").ToString(), 1)
                                tdInner.Controls.Add(lbl)
                            End If
                        Else
                            InnerTdSetting(tdInner, "60px", "Right")
                            lbl = New Label

                            If i = dsData.Tables(0).Rows.Count Then
                                tdInner.Text = FormatNumber(dsTotal.Tables(0).Rows(0).Item(dsColumn.Tables(0).Rows(j - 4).Item("COLUMNVALUE").ToString() + "TOTAL").ToString(), 1)
                                TotColVal = McolVal
                            Else
                                'colVal = dsData.Tables(0).Rows(i).Item("COLVAL") * UColVal
                                McolVal = McolVal + colVal
                                lbl.Text = FormatNumber(dsResult.Tables(0).Rows(i).Item(dsColumn.Tables(0).Rows(j - 4).Item("COLUMNVALUE").ToString()).ToString(), 1)
                                tdInner.Controls.Add(lbl)
                            End If
                        End If
                        'Select Case j
                        '    Case 1

                        '    Case 2

                        '    Case 3

                        '    Case 4

                        '    Case 5
                        '        InnerTdSetting(tdInner, "60px", "Right")
                        '        lbl = New Label

                        '        If i = dsData.Tables(0).Rows.Count Then
                        '            tdInner.Text = FormatNumber(dsTotal.Tables(0).Rows(0).Item("CYLVALTOTAL").ToString(), 1)
                        '            TotCylVal = McylVal
                        '        Else
                        '            cylVal = dsData.Tables(0).Rows(i).Item("CYLVAL") * UCylVal
                        '            McylVal = McylVal + cylVal
                        '            lbl.Text = FormatNumber(dsResult.Tables(0).Rows(i).Item("CYLVAL").ToString(), 1)  'If(netVal > grossVal, netVal, grossVal)
                        '            tdInner.Controls.Add(lbl)
                        '        End If
                        '    Case 6
                        '        InnerTdSetting(tdInner, "60px", "Right")
                        '        lbl = New Label
                        '        If i = dsData.Tables(0).Rows.Count Then
                        '            tdInner.Text = FormatNumber(dsTotal.Tables(0).Rows(0).Item("SERIALVALTOTAL").ToString(), 1)
                        '            TotSerVal = MserVal
                        '        Else
                        '            serVal = colVal + cylVal
                        '            MserVal = MserVal + serVal
                        '            lbl.Text = FormatNumber(dsResult.Tables(0).Rows(i).Item("SERIALVAL").ToString(), 1)
                        '            tdInner.Controls.Add(lbl)
                        '        End If
                        '    Case 7
                        '        InnerTdSetting(tdInner, "60px", "Right")
                        '        lbl = New Label
                        '        If i = dsData.Tables(0).Rows.Count Then
                        '            tdInner.Text = FormatNumber(dsTotal.Tables(0).Rows(0).Item("PARALLELVALTOTAL").ToString(), 1)
                        '            TotParVal = MparVal
                        '        Else
                        '            parVal = If(colVal > cylVal, colVal, cylVal)
                        '            MparVal = MparVal + parVal
                        '            lbl.Text = FormatNumber(dsResult.Tables(0).Rows(i).Item("PARALLELVAL").ToString(), 1)
                        '            tdInner.Controls.Add(lbl)
                        '        End If

                        '    Case 8

                        '    'InnerTdSetting(tdInner, "60px", "Left")
                        '    ' txt = New TextBox
                        '    ' txt.CssClass = "SmallTextBox"
                        '    ' txt.ID = "txtPref" + (i + 1).ToString()
                        '    'If i = dsData.Tables(0).Rows.Count Then
                        '    '    tdInner.Text = MPrefVal.ToString()
                        '    '    TotalPref = MPrefVal
                        '    'Else

                        '    '    If dsData.Tables(0).Rows.Count >= i Then
                        '    '       If dsPref.Tables(0).Rows(0).Item("M" + (i + 1).ToString()).ToString <> "" Then
                        '    '          txt.Text = dsPref.Tables(0).Rows(0).Item("M" + (i + 1).ToString()).ToString
                        '    '         MPrefVal = MPrefVal + dsPref.Tables(0).Rows(0).Item("M" + (i + 1).ToString()).ToString
                        '    '    Else
                        '    '       txt.Text = ""
                        '    '  End If
                        '    ' End If
                        '    'tdInner.Controls.Add(txt)
                        '    'End If
                        '    'Case 8
                        '    'InnerTdSetting(tdInner, "60px", "Left")
                        '    'txt = New TextBox
                        '    'If i = dsData.Tables(0).Rows.Count Then
                        '    '    'tdInner.Text = SizeTotal.ToString()
                        '    'Else
                        '    '    txt.ID = "txtPref" + (i + 1).ToString()
                        '    '    txt.CssClass = "SmallTextBox"
                        '    '    If dsPref.Tables(0).Rows.Count > 0 Then
                        '    '        txt.Text = dsPref.Tables(0).Rows(0).Item("M" + (i + 1).ToString()).ToString
                        '    '    Else
                        '    '        txt.Text = ""
                        '    '    End If
                        '    '    tdInner.Controls.Add(txt)
                        '    'End If
                        '    Case 9

                        '    Case 10

                        '    Case 11

                        'End Select
                        trInner.Controls.Add(tdInner)
                    Next
                    If (i Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    tblJSeqR.Controls.Add(trInner)
                Next
            End If


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetJobSeqResDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetMatJobSeqResDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim tdHeader2 As New TableCell
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim dsPref As New DataSet
        Dim dsMat As New DataSet
        Dim rdb As New RadioButton
        Dim val As New Double
        Dim dvData As New DataView
        Dim dtData As New DataTable
        Dim dsCyl As New DataSet
        Dim dsDataT As New DataSet
        Dim txt As New TextBox
        Dim lnkbtn As New LinkButton
        Dim count As New Integer
        Dim dsResult As New DataSet
        Dim dsTotal As New DataSet
        Try
            tblMatRes.Rows.Clear()


            dsData = objGetData.GetJobSeqDetails(Session("E1CaseId"), hidEqId.Value, hidLayerId.Value)
            dsResult = objGetData.GetJobResultMat(Session("E1CaseId"), hidEqId.Value, hidLayerId.Value)
            dsTotal = objGetData.GetResultTotal(Session("E1CaseId"), hidEqId.Value, hidLayerId.Value)
            If dsData.Tables(0).Rows.Count > 0 Then
                count = dsData.Tables(0).Rows.Count + 1
            Else
                count = Convert.ToInt32(hidRowNum.Value)
            End If

            trHeader = New TableRow
            For i = 1 To 6
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "200px", "Job Results Material", "1")
                        HeaderTdSetting(tdHeader2, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        HeaderTdSetting(tdHeader, "200px", "Good Production", "1")
                        HeaderTdSetting(tdHeader2, "0", "(" + unit + ")", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        HeaderTdSetting(tdHeader, "200px", "Gross Production", "1")
                        HeaderTdSetting(tdHeader2, "0", "(" + unit + ")", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        HeaderTdSetting(tdHeader, "200px", "Process Waste", "1")
                        HeaderTdSetting(tdHeader2, "0", "(" + unit + ")", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 5
                        HeaderTdSetting(tdHeader, "200px", "Registration Waste", "1")
                        HeaderTdSetting(tdHeader2, "0", "(" + unit + ")", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 6
                        HeaderTdSetting(tdHeader, "200px", "Total Waste", "1")
                        HeaderTdSetting(tdHeader2, "0", "(" + unit + ")", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                End Select
            Next
            tblMatRes.Controls.Add(trHeader)
            'tblJSeqR.Controls.Add(trHeader1)
            tblMatRes.Controls.Add(trHeader2)
            If dsResult.Tables(0).Rows.Count > 0 Then
                For i = 0 To dsResult.Tables(0).Rows.Count
                    trInner = New TableRow
                    Dim netVal As New Double
                    Dim grossVal As New Double
                    Dim colVal As New Double
                    Dim cylVal As New Double
                    Dim serVal As New Double
                    Dim regVal As New Double
                    Dim parVal As New Double
                    Dim mainVal As New Double
                    Dim PressTotal As New Double
                    For j = 1 To 6
                        tdInner = New TableCell
                        Select Case j
                            Case 1
                                InnerTdSetting(tdInner, "20px", "Center")
                                lbl = New Label
                                If i = dsData.Tables(0).Rows.Count Then
                                    tdInner.Text = "Total"
                                Else
                                    lbl.Text = dsResult.Tables(0).Rows(i).Item("DESGDESC").ToString()
                                    tdInner.Controls.Add(lbl)
                                End If
                            Case 2
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label

                                If i = dsData.Tables(0).Rows.Count Then
                                    tdInner.Text = FormatNumber(dsTotal.Tables(0).Rows(0).Item("GOODPRODTOTAL").ToString(), 1)
                                Else
                                    netVal = (dsData.Tables(0).Rows(i).Item("RSIZEVAL") / TotalRunP) * TotalRunP
                                    TotMNetVal = TotMNetVal + netVal
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(i).Item("GOODPROD").ToString(), 1)
                                    tdInner.Controls.Add(lbl)
                                End If
                            Case 3
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label

                                If i = dsData.Tables(0).Rows.Count Then
                                    tdInner.Text = FormatNumber(dsTotal.Tables(0).Rows(0).Item("GROSSPRODTOTAL").ToString(), 1)
                                Else
                                    If URwPref <> 0.0 Then
                                        grossVal = netVal / (1 - (URwPref / 100))
                                        TotMGrossVal = TotMGrossVal + grossVal
                                        lbl.Text = FormatNumber(dsResult.Tables(0).Rows(i).Item("GROSSPROD").ToString(), 1)
                                        tdInner.Controls.Add(lbl)
                                    Else
                                        grossVal = netVal / (1 - (TotalRunW / 100))
                                        TotMGrossVal = TotMGrossVal + grossVal
                                        lbl.Text = FormatNumber(dsResult.Tables(0).Rows(i).Item("GROSSPROD").ToString(), 1)
                                        tdInner.Controls.Add(lbl)
                                    End If

                                End If
                            Case 4
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label

                                If i = dsData.Tables(0).Rows.Count Then
                                    tdInner.Text = FormatNumber(dsTotal.Tables(0).Rows(0).Item("PROCWASTETOTAL").ToString(), 1)
                                Else
                                    colVal = grossVal - netVal
                                    TotMColVal = TotMColVal + colVal
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(i).Item("PROCWASTE").ToString(), 1)
                                    tdInner.Controls.Add(lbl)
                                End If
                            Case 5
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label
                                If i = dsData.Tables(0).Rows.Count Then
                                    tdInner.Text = FormatNumber(dsTotal.Tables(0).Rows(0).Item("REGWASTETOTAL").ToString(), 1)
                                Else
                                    'regVal = dsData.Tables(0).Rows(i).Item("COLVAL") * TotalRegT * URegVal
                                    TotMRegVal = TotMRegVal + regVal
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(i).Item("REGWASTE").ToString(), 1)
                                    tdInner.Controls.Add(lbl)
                                End If
                            Case 6
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label
                                If i = dsData.Tables(0).Rows.Count Then
                                    tdInner.Text = FormatNumber(dsTotal.Tables(0).Rows(0).Item("TOTALMAT").ToString(), 1)
                                Else
                                    serVal = colVal + regVal
                                    TotMSerVal = TotMSerVal + serVal
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(i).Item("TOTALWASTE").ToString(), 1)
                                    tdInner.Controls.Add(lbl)
                                End If
                        End Select
                        trInner.Controls.Add(tdInner)
                    Next
                    If (i Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    tblMatRes.Controls.Add(trInner)
                Next
            End If


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetMatJobSeqResDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetTimeSumDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim tdHeader2 As New TableCell
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim dsPref As New DataSet
        Dim dsMat As New DataSet
        Dim rdb As New RadioButton
        Dim val As New Double
        Dim dvData As New DataView
        Dim dtData As New DataTable
        Dim dsCyl As New DataSet
        Dim dsDataT As New DataSet
        Dim txt As New TextBox
        Dim lnkbtn As New LinkButton
        Dim count As New Integer
        Dim HrVal As New Double
        Dim dsResult As New DataSet
        Try
            tblTSum.Rows.Clear()

            dsResult = objGetData.GetTimeSummary(Session("E1CaseId"), hidEqId.Value, hidLayerId.Value)
            dsData = objGetData.GetHoursDetails(Session("E1CaseId"), hidLayerId.Value)
            HrVal = dsData.Tables(0).Rows(0).Item("HRVAL")
            If dsData.Tables(0).Rows.Count > 0 Then
                count = dsData.Tables(0).Rows.Count + 1
            Else
                count = Convert.ToInt32(hidRowNum.Value)
            End If

            trHeader = New TableRow
            For i = 1 To 7
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "200px", "Time Summary", "1")
                        HeaderTdSetting(tdHeader2, "0", "Target Operating Time", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        HeaderTdSetting(tdHeader, "200px", "One Job", "3")
                        HeaderTdSetting(tdHeader2, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        'HeaderTdSetting(tdHeader, "200px", "Gross Production", "1")
                        HeaderTdSetting(tdHeader2, "0", "(min)", "1")
                        'trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        'HeaderTdSetting(tdHeader, "200px", "Gross Production", "1")
                        HeaderTdSetting(tdHeader2, "0", "(%)", "1")
                        'trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 5
                        HeaderTdSetting(tdHeader, "200px", "All Year", "3")
                        HeaderTdSetting(tdHeader2, "0", HrVal.ToString(), "1")
                        tdHeader2.ToolTip = "Entered on Operating Parameter Screen"
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 6
                        'HeaderTdSetting(tdHeader, "200px", "Gross Production", "1")
                        HeaderTdSetting(tdHeader2, "0", "(hours)", "1")
                        'trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 7
                        'HeaderTdSetting(tdHeader, "200px", "Gross Production", "1")
                        HeaderTdSetting(tdHeader2, "0", "(%)", "1")
                        'trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                End Select
            Next
            tblTSum.Controls.Add(trHeader)
            'tblJSeqR.Controls.Add(trHeader1)
            tblTSum.Controls.Add(trHeader2)

            If TotalPref = "0" Then
                If hidJRes.Value = "Parallel" Then
                    TotSumVal = TotParVal + TotRegVal + TotMainVal + TotGrossVal
                Else
                    TotSumVal = TotSerVal + TotRegVal + TotMainVal + TotGrossVal
                End If
            Else
                TotSumVal = TotalPressPref + TotRegVal + TotMainVal + TotGrossVal
            End If

            Dim diffValue As Double = (URegVal / TotalRunP) * 100
            Dim wasteValue As Double = TotRegVal * diffValue / 100
            Dim regDValue As Double = TotRegVal - wasteValue


            Dim diffPrefValue As Double
            Dim regDPrefValue As Double
            Dim wastePrefValue As Double

            ' If hidJRes.Value = "Parallel" Then
            '     TotSumVal = TotParVal + TotRegVal + TotMainVal + TotGrossVal
            ' Else
            '     TotSumVal = TotSerVal + TotRegVal + TotMainVal + TotGrossVal
            ' End If
            'TotSumVal = TotParVal + TotRegVal + TotMainVal + TotGrossVal
            TotHrVal = 0.0
            TotHr2Val = 0.0
            TotPerc = 0.0
            If dsResult.Tables(0).Rows.Count > 0 Then
                For i = 0 To 5
                    trInner = New TableRow
                    Dim Perc As New Double
                    Dim HrValue As New Double
                    For j = 1 To 7
                        tdInner = New TableCell
                        Select Case j
                            Case 1
                                InnerTdSetting(tdInner, "20px", "Center")
                                lbl = New Label
                                If i = 0 Then
                                    lbl.Text = "Setup Downtime"
                                ElseIf i = 1 Then
                                    lbl.Text = "Registration Downtime"
                                ElseIf i = 2 Then
                                    lbl.Text = "Maintenance Downtime"
                                ElseIf i = 3 Then
                                    lbl.Text = "Gross Product Run Time"
                                ElseIf i = 4 Then
                                    lbl.Text = "Registration Waste Run Time"
                                ElseIf i = 5 Then
                                    lbl.Text = "Total Press Time"
                                End If
                                tdInner.Controls.Add(lbl)
                            Case 2
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label
                                If i = 0 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("SETUPDTIME"), 1).ToString()
                                ElseIf i = 1 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("REGDTIME"), 1).ToString()
                                ElseIf i = 2 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("MAINTDTIME"), 1).ToString()
                                ElseIf i = 3 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("GROSSPRTIME"), 1).ToString()
                                ElseIf i = 4 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("REGWASTERTIME"), 1).ToString()
                                ElseIf i = 5 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTALTIME1"), 1).ToString()
                                End If
                                tdInner.Controls.Add(lbl)

                            Case 3
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label
                                lbl.Text = "(min)"
                                tdInner.Controls.Add(lbl)

                            Case 4
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label
                                'If i = 0 Then
                                '    If TotalPref = "0" Then
                                '        If hidJRes.Value = "Parallel" Then
                                '            Perc = TotParVal / TotSumVal * 100
                                '            lbl.Text = FormatNumber(Perc, 1).ToString() + " %"
                                '            TotPerc = TotPerc + Perc
                                '        Else
                                '            Perc = TotSerVal / TotSumVal * 100
                                '            lbl.Text = FormatNumber(Perc, 1).ToString() + " %"
                                '            TotPerc = TotPerc + Perc
                                '        End If
                                '    Else
                                '        Perc = TotalPressPref / TotSumVal * 100
                                '        lbl.Text = FormatNumber(Perc, 1).ToString() + " %"
                                '        TotPerc = TotPerc + Perc
                                '    End If
                                '    'Perc = TotParVal / TotSumVal * 100
                                '    'lbl.Text = FormatNumber(Perc, 1).ToString() + " %"
                                '    ' TotPerc = TotPerc + Perc
                                'ElseIf i = 1 Then
                                '    Perc = regDValue / TotSumVal * 100
                                '    lbl.Text = FormatNumber(Perc, 1).ToString() + " %"
                                '    TotPerc = TotPerc + Perc
                                'ElseIf i = 2 Then
                                '    Perc = TotMainVal / TotSumVal * 100
                                '    lbl.Text = FormatNumber(Perc, 1).ToString() + " %"
                                '    TotPerc = TotPerc + Perc
                                'ElseIf i = 3 Then
                                '    Perc = TotGrossVal / TotSumVal * 100
                                '    lbl.Text = FormatNumber(Perc, 1).ToString() + " %"
                                '    TotPerc = TotPerc + Perc
                                'ElseIf i = 4 Then
                                '    Perc = wasteValue / TotSumVal * 100
                                '    lbl.Text = FormatNumber(Perc, 1).ToString() + " %"
                                '    TotPerc = TotPerc + Perc
                                'ElseIf i = 5 Then
                                '    lbl.Text = TotPerc.ToString() + " %"
                                'End If

                                If i = 0 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("SETUPDTIMEP"), 1).ToString() + " %"
                                ElseIf i = 1 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("REGDTIMEP"), 1).ToString() + " %"
                                ElseIf i = 2 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("MAINTDTIMEP"), 1).ToString() + " %"
                                ElseIf i = 3 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("GROSSPRTIMEP"), 1).ToString() + " %"
                                ElseIf i = 4 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("REGWASTERTIMEP"), 1).ToString() + " %"
                                ElseIf i = 5 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTALTIMEP1"), 1).ToString() + " %"
                                End If
                                tdInner.Controls.Add(lbl)

                            Case 5
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label

                                'If i = 5 Then
                                '    tdInner.Text = FormatNumber(TotHrVal, 0).ToString()
                                'Else
                                '    HrValue = HrVal * (Perc / 100)
                                '    If i = 3 Then
                                '        PWaste1 = HrValue
                                '    ElseIf i = 1 Then
                                '        RegT = HrValue
                                '        diffPrefValue = (HrValue / regValue)
                                '        regDPrefValue = HrValue - diffPrefValue
                                '        'HrValue = regDPrefValue
                                '        regTimeValue = FormatNumber(HrValue, 0)

                                '    ElseIf i = 0 Then
                                '        downTimeValue = FormatNumber(HrValue, 0)
                                '    ElseIf i = 2 Then
                                '        maintenanceValue = FormatNumber(HrValue, 0)
                                '    ElseIf i = 4 Then
                                '        'wastePrefValue = RegT - regDPrefValue
                                '        'HrValue = wastePrefValue
                                '        RegT = RegT + HrValue
                                '        FormatNumber(HrValue, 0)

                                '    End If

                                '    TotHrVal = TotHrVal + HrValue
                                '    lbl.Text = FormatNumber(HrValue, 0).ToString()

                                If i = 0 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("SETUPDTIMEY"), 1).ToString() + " "
                                    downTimeValue = dsResult.Tables(0).Rows(0).Item("SETUPDTIMEY")
                                ElseIf i = 1 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("REGDTIMEY"), 1).ToString() + " "
                                    regTimeValue = dsResult.Tables(0).Rows(0).Item("REGDTIMEY")
                                ElseIf i = 2 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("MAINTDTIMEY"), 1).ToString() + " "
                                    maintenanceValue = dsResult.Tables(0).Rows(0).Item("MAINTDTIMEY")
                                ElseIf i = 3 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("GROSSPRTIMEY"), 1).ToString() + " "
                                ElseIf i = 4 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("REGWASTERTIMEY"), 1).ToString() + " "
                                ElseIf i = 5 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTALTIMEY1"), 1).ToString() + " "
                                    TotHrVal = dsResult.Tables(0).Rows(0).Item("TOTALTIMEY1")
                                End If
                                tdInner.Controls.Add(lbl)
                            'End If

                            Case 6
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label
                                lbl.Text = "(hours)"
                                tdInner.Controls.Add(lbl)

                            Case 7
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label
                                'If i = 5 Then
                                '    tdInner.Text = TotPerc.ToString() + " %"
                                'Else
                                '    lbl.Text = FormatNumber(Perc, 1).ToString() + " %"
                                '    tdInner.Controls.Add(lbl)
                                'End If

                                If i = 0 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("SETUPDTIMEP"), 1).ToString() + " %"
                                ElseIf i = 1 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("REGDTIMEP"), 1).ToString() + " %"
                                ElseIf i = 2 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("MAINTDTIMEP"), 1).ToString() + " %"
                                ElseIf i = 3 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("GROSSPRTIMEP"), 1).ToString() + " %"
                                ElseIf i = 4 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("REGWASTERTIMEP"), 1).ToString() + " %"
                                ElseIf i = 5 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTALTIMEP1"), 1).ToString() + " %"
                                End If
                                tdInner.Controls.Add(lbl)

                        End Select
                        trInner.Controls.Add(tdInner)
                    Next
                    If (i Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    tblTSum.Controls.Add(trInner)
                Next


                Dim TotTSum2Val As New Double
                Dim GoodVal As New Double
                Dim SetVal As New Double
                Dim WTimeVal As New Double
                Dim TotPerc2 As New Double
                GoodVal = TotNetVal
                If TotalPref = "0" Then
                    If hidJRes.Value = "Parallel" Then
                        SetVal = TotParVal + regDValue + TotMainVal
                    Else
                        SetVal = TotSerVal + regDValue + TotMainVal
                    End If
                Else
                    SetVal = TotalPressPref + regDValue + TotMainVal
                End If

                '  SetVal = TotParVal + TotRegVal + TotMainVal
                WTimeVal = TotGrossVal - TotNetVal
                WTimeVal = WTimeVal + wasteValue
                TotTSum2Val = GoodVal + SetVal + WTimeVal
                tblTSum2.Rows.Clear()
                For i = 0 To 3
                    trInner = New TableRow
                    Dim Perc As New Double
                    Dim HrValue As New Double
                    For j = 1 To 7
                        tdInner = New TableCell
                        Select Case j
                            Case 1
                                InnerTdSetting(tdInner, "200px", "Center")
                                lbl = New Label
                                If i = 0 Then
                                    lbl.Text = "Good Product Run Time"
                                ElseIf i = 1 Then
                                    lbl.Text = "Setup, Regist, Maint Downtime"
                                ElseIf i = 2 Then
                                    lbl.Text = "Process and Registration Waste Downtime"
                                ElseIf i = 3 Then
                                    lbl.Text = "Total Press Time"
                                End If
                                tdInner.Controls.Add(lbl)
                            Case 2
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label
                                If i = 0 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTREGDTIME"), 1).ToString()
                                ElseIf i = 1 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTMAINTDTIME"), 1).ToString()
                                ElseIf i = 2 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTWASTERTIME"), 1).ToString()
                                ElseIf i = 3 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTALTIME2"), 1).ToString()
                                End If
                                tdInner.Controls.Add(lbl)

                            Case 3
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label
                                lbl.Text = "(min)"
                                tdInner.Controls.Add(lbl)

                            Case 4
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label
                                'If i = 0 Then
                                '    Perc = GoodVal / TotTSum2Val * 100
                                '    lbl.Text = FormatNumber(Perc, 1).ToString() + " %"
                                '    TotPerc2 = TotPerc2 + Perc
                                'ElseIf i = 1 Then
                                '    Perc = SetVal / TotTSum2Val * 100
                                '    lbl.Text = FormatNumber(Perc, 1).ToString() + " %"
                                '    TotPerc2 = TotPerc2 + Perc
                                'ElseIf i = 2 Then
                                '    Perc = WTimeVal / TotTSum2Val * 100
                                '    lbl.Text = FormatNumber(Perc, 1).ToString() + " %"
                                '    TotPerc2 = TotPerc2 + Perc
                                'ElseIf i = 3 Then
                                '    'lbl.Text = TotPerc2.ToString() + " %"
                                '    lbl.Text = FormatNumber(TotPerc2.ToString(), 1).ToString() + " %"
                                'End If

                                If i = 0 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTREGDTIMEP"), 1).ToString() + " %"
                                ElseIf i = 1 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTMAINTDTIMEP"), 1).ToString() + " %"
                                ElseIf i = 2 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTWASTERTIMEP"), 1).ToString() + " %"
                                ElseIf i = 3 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTALTIMEP2"), 1).ToString() + " %"
                                End If
                                tdInner.Controls.Add(lbl)

                            Case 5
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label
                                'If i = 3 Then
                                '    tdInner.Text = FormatNumber(TotHr2Val, 0).ToString()
                                'Else
                                '    HrValue = HrVal * (Perc / 100)
                                '    If i = 0 Then
                                '        PWaste2 = HrValue
                                '    End If
                                '    TotHr2Val = TotHr2Val + HrValue
                                '    lbl.Text = FormatNumber(HrValue, 1).ToString()
                                '    tdInner.Controls.Add(lbl)
                                'End If

                                If i = 0 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTREGDTIMEY"), 1).ToString()
                                ElseIf i = 1 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTMAINTDTIMEY"), 1).ToString()
                                ElseIf i = 2 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTWASTERTIMEY"), 1).ToString()
                                ElseIf i = 3 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTALTIMEY2"), 1).ToString()
                                End If
                                tdInner.Controls.Add(lbl)

                            Case 6
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label
                                lbl.Text = "(hours)"
                                tdInner.Controls.Add(lbl)

                            Case 7
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label
                                'If i = 3 Then
                                '    tdInner.Text = TotPerc.ToString() + " %"
                                'Else
                                '    lbl.Text = FormatNumber(Perc, 1).ToString() + " %"
                                '    tdInner.Controls.Add(lbl)
                                'End If

                                If i = 0 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTREGDTIMEP"), 1).ToString() + " %"
                                ElseIf i = 1 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTMAINTDTIMEP"), 1).ToString() + " %"
                                ElseIf i = 2 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTWASTERTIMEP"), 1).ToString() + " %"
                                ElseIf i = 3 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTALTIMEP2"), 1).ToString() + " %"
                                End If
                                tdInner.Controls.Add(lbl)

                        End Select
                        trInner.Controls.Add(tdInner)
                    Next
                    If (i Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    tblTSum2.Controls.Add(trInner)
                Next
            End If


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetJobSeqResDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetMatSumDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim tdHeader2 As New TableCell
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim dsPref As New DataSet
        Dim dsMat As New DataSet
        Dim rdb As New RadioButton
        Dim val As New Double
        Dim dvData As New DataView
        Dim dtData As New DataTable
        Dim dsCyl As New DataSet
        Dim dsDataT As New DataSet
        Dim txt As New TextBox
        Dim lnkbtn As New LinkButton
        Dim count As New Integer
        Dim HrVal As New Double
        Dim dsResult As New DataSet
        Try
            tblMSum.Rows.Clear()

            dsResult = objGetData.GetMatSummary(Session("E1CaseId"), hidEqId.Value, hidLayerId.Value)
            dsData = objGetData.GetHoursDetails(Session("E1CaseId"), Request.QueryString("RowNum").ToString())
            HrVal = dsData.Tables(0).Rows(0).Item("HRVAL")
            If dsData.Tables(0).Rows.Count > 0 Then
                count = dsData.Tables(0).Rows.Count + 1
            Else
                count = Convert.ToInt32(hidRowNum.Value)
            End If

            trHeader = New TableRow
            For i = 1 To 7
                tdHeader = New TableCell
                tdHeader1 = New TableCell
                tdHeader2 = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "200px", "Material Summary", "1")
                        HeaderTdSetting(tdHeader2, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 2
                        HeaderTdSetting(tdHeader, "200px", "One Job", "3")
                        HeaderTdSetting(tdHeader2, "0", "", "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 3
                        'HeaderTdSetting(tdHeader, "200px", "Gross Production", "1")
                        HeaderTdSetting(tdHeader2, "0", "(" + unit + ")", "1")
                        'trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 4
                        'HeaderTdSetting(tdHeader, "200px", "Gross Production", "1")
                        HeaderTdSetting(tdHeader2, "0", "(%)", "1")
                        'trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 5
                        HeaderTdSetting(tdHeader, "200px", "All Year", "3")
                        HeaderTdSetting(tdHeader2, "0", HrVal.ToString(), "1")
                        trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 6
                        'HeaderTdSetting(tdHeader, "200px", "Gross Production", "1")
                        HeaderTdSetting(tdHeader2, "0", "(" + unit + ")", "1")
                        'trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                    Case 7
                        'HeaderTdSetting(tdHeader, "200px", "Gross Production", "1")
                        HeaderTdSetting(tdHeader2, "0", "(%)", "1")
                        'trHeader.Controls.Add(tdHeader)
                        trHeader2.Controls.Add(tdHeader2)
                End Select
            Next
            tblMSum.Controls.Add(trHeader)
            'tblJSeqR.Controls.Add(trHeader1)
            'tblMSum.Controls.Add(trHeader2)

            Dim TotSumVal As New Double

            Dim TotPerc As New Double
            Dim TotPerc2 As New Double
            Dim Pwaste As Double = (PWaste1 - PWaste2) * TotalRunP
            Dim RegWaste As Double = RegT * URegVal
            Dim GProd As Double = PWaste2 * TotalRunP
            TotSumVal = TotMColVal + TotMRegVal + TotMNetVal

            If dsResult.Tables(0).Rows.Count > 0 Then
                TotSum2Val = CDbl(dsResult.Tables(0).Rows(0).Item("PROCWASTEY")) + CDbl(dsResult.Tables(0).Rows(0).Item("REGWASTEY")) + CDbl(dsResult.Tables(0).Rows(0).Item("GOODPRODY"))
                wastePerc = CDbl(dsResult.Tables(0).Rows(0).Item("PROCWASTEY")) + CDbl(dsResult.Tables(0).Rows(0).Item("REGWASTEY"))

                For i = 0 To 3
                    trInner = New TableRow
                    Dim Perc As New Double
                    Dim Perc2 As New Double
                    Dim HrValue As New Double
                    For j = 1 To 7
                        tdInner = New TableCell
                        Select Case j
                            Case 1
                                InnerTdSetting(tdInner, "20px", "Center")
                                lbl = New Label
                                If i = 0 Then
                                    lbl.Text = "Process Waste"
                                ElseIf i = 1 Then
                                    lbl.Text = "Registration Waste"
                                ElseIf i = 2 Then
                                    lbl.Text = "Good Product"
                                ElseIf i = 3 Then
                                    lbl.Text = "Total Footage"
                                End If
                                tdInner.Controls.Add(lbl)
                            Case 2
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label
                                If i = 0 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("PROCWASTE"), 1).ToString()
                                ElseIf i = 1 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("REGWASTE"), 1).ToString()
                                ElseIf i = 2 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("GOODPROD"), 1).ToString()
                                ElseIf i = 3 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTALFOOTAGE"), 1).ToString()
                                End If
                                tdInner.Controls.Add(lbl)

                            Case 3
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label
                                lbl.Text = "(" + unit + ")"
                                tdInner.Controls.Add(lbl)

                            Case 4
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label
                                'If i = 0 Then
                                '    Perc = TotMColVal / TotSumVal * 100
                                '    lbl.Text = FormatNumber(Perc, 1).ToString() + " %"
                                '    TotPerc = TotPerc + Perc
                                'ElseIf i = 1 Then
                                '    Perc = TotMRegVal / TotSumVal * 100
                                '    lbl.Text = FormatNumber(Perc, 1).ToString() + " %"
                                '    TotPerc = TotPerc + Perc
                                'ElseIf i = 2 Then
                                '    Perc = TotMNetVal / TotSumVal * 100
                                '    lbl.Text = FormatNumber(Perc, 1).ToString() + " %"
                                '    TotPerc = TotPerc + Perc
                                'ElseIf i = 3 Then
                                '    lbl.Text = TotPerc.ToString() + " %"
                                'End If

                                If i = 0 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("PROCWASTEP"), 1).ToString() + " %"
                                ElseIf i = 1 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("REGWASTEP"), 1).ToString() + " %"
                                ElseIf i = 2 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("GOODPRODP"), 1).ToString() + " %"
                                ElseIf i = 3 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTALFOOTAGEP"), 1).ToString() + " %"
                                End If
                                tdInner.Controls.Add(lbl)

                            Case 5
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label
                                'If i = 3 Then
                                '    tdInner.Text = FormatNumber(TotSum2Val, 0).ToString()
                                'Else
                                '    If i = 0 Then
                                '        lbl.Text = FormatNumber(Pwaste, 1).ToString()
                                '    ElseIf i = 1 Then
                                '        lbl.Text = FormatNumber(RegWaste, 1).ToString()
                                '    Else
                                '        lbl.Text = FormatNumber(GProd, 1).ToString()
                                '    End If
                                'End If

                                If i = 0 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("PROCWASTEY"), 1).ToString()
                                ElseIf i = 1 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("REGWASTEY"), 1).ToString()
                                ElseIf i = 2 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("GOODPRODY"), 1).ToString()
                                ElseIf i = 3 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTALFOOTAGEY"), 1).ToString()
                                End If
                                tdInner.Controls.Add(lbl)

                            Case 6
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label
                                lbl.Text = "(" + unit + ")"
                                tdInner.Controls.Add(lbl)

                            Case 7
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label
                                'If i = 3 Then
                                '    tdInner.Text = TotPerc2.ToString() + " %"
                                'Else
                                '    If i = 0 Then
                                '        Perc2 = (Pwaste / TotSum2Val) * 100
                                '    ElseIf i = 1 Then
                                '        Perc2 = (RegWaste / TotSum2Val) * 100
                                '    ElseIf i = 2 Then
                                '        Perc2 = (GProd / TotSum2Val) * 100
                                '    End If
                                '    lbl.Text = FormatNumber(Perc2, 1).ToString() + " %"
                                '    TotPerc2 = TotPerc2 + Perc2
                                '    tdInner.Controls.Add(lbl)
                                'End If
                                If i = 0 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("PROCWASTEP"), 1).ToString() + " %"
                                ElseIf i = 1 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("REGWASTEP"), 1).ToString() + " %"
                                ElseIf i = 2 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("GOODPRODP"), 1).ToString() + " %"
                                ElseIf i = 3 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("TOTALFOOTAGEP"), 1).ToString() + " %"
                                End If
                                tdInner.Controls.Add(lbl)
                        End Select
                        trInner.Controls.Add(tdInner)
                    Next
                    If (i Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    tblMSum.Controls.Add(trInner)
                Next
            End If


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetMatSumDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Public Sub GetSetupSumDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Dim tdHeader As New TableCell
        Dim trHeader As New TableRow
        Dim tdHeader1 As New TableCell
        Dim tdHeader2 As New TableCell
        Dim trHeader1 As New TableRow
        Dim trHeader2 As New TableRow
        Dim dsData As New DataSet
        Dim trInner As New TableRow
        Dim tdInner As New TableCell
        Dim lbl As New Label
        Dim dsPref As New DataSet
        Dim dsMat As New DataSet
        Dim rdb As New RadioButton
        Dim val As New Double
        Dim dvData As New DataView
        Dim dtData As New DataTable
        Dim dsCyl As New DataSet
        Dim dsDataT As New DataSet
        Dim txt As New TextBox
        Dim lnkbtn As New LinkButton
        Dim count As New Integer
        Dim HrVal As New Double
        Dim dsEquip As DataSet
        Dim dsResult As New DataSet
        Try
            tblSSum.Rows.Clear()
            dsEquip = Session("dsEquip")
            '  dsEquip = objGetData.GetEquipmentDetails()
            dsResult = objGetData.GetSetupSummary(Session("E1CaseId"), hidEqId.Value, hidLayerId.Value)
            dsData = objGetData.GetHoursDetails(Session("E1CaseId"), Request.QueryString("RowNum").ToString())
            HrVal = dsData.Tables(0).Rows(0).Item("HRVAL")
            If dsData.Tables(0).Rows.Count > 0 Then
                count = dsData.Tables(0).Rows.Count + 1
            Else
                count = Convert.ToInt32(hidRowNum.Value)
            End If

            trHeader = New TableRow
            For i = 1 To 3
                tdHeader = New TableCell
                Dim Title As String = String.Empty
                'Header
                Select Case i
                    Case 1
                        HeaderTdSetting(tdHeader, "200px", "Setup Summary", "1")
                        trHeader.Controls.Add(tdHeader)
                    Case 2
                        HeaderTdSetting(tdHeader, "200px", "One Job", "2")
                        trHeader.Controls.Add(tdHeader)
                    Case 3
                        HeaderTdSetting(tdHeader, "200px", "All Year", "3")
                        trHeader.Controls.Add(tdHeader)
                End Select
            Next
            tblSSum.Controls.Add(trHeader)
            Dim Perc As New Double
            Dim HrValue As New Double
            If dsResult.Tables(0).Rows.Count > 0 Then
                For i = 0 To 2
                    trInner = New TableRow

                    For j = 1 To 5
                        tdInner = New TableCell
                        Select Case j
                            Case 1
                                InnerTdSetting(tdInner, "20px", "Center")
                                lbl = New Label
                                If i = 0 Then
                                    lbl.Text = "Job"
                                ElseIf i = 1 Then
                                    lbl.Text = "Color Change"
                                ElseIf i = 2 Then
                                    'For k = 0 To dsEquip.Tables(0).Rows.Count - 1
                                    '    If dsEquip.Tables(0).Rows(k).Item("EQUIPDE1").ToString() = "Printing press flexo" Then
                                    '        lbl.Text = "Plate Change"
                                    '    Else
                                    '        lbl.Text = "Cylinder Change"
                                    '    End If
                                    'Next
                                    If hidEqId.Value = "3" Or hidEqId.Value = "26" Or hidEqId.Value = "83" Then
                                        lbl.Text = "Graphic Change"
                                    Else
                                        lbl.Text = "Graphic Change"
                                    End If

                                End If
                                tdInner.Controls.Add(lbl)
                            Case 2
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label
                                If i = 0 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("JOB"), 1).ToString()
                                ElseIf i = 1 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("COLVAL"), 0).ToString()
                                ElseIf i = 2 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("CYLVAL"), 0).ToString()
                                End If
                                tdInner.Controls.Add(lbl)

                            Case 3
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label
                                lbl.Text = "(#)"
                                tdInner.Controls.Add(lbl)

                            Case 4
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label
                                If i = 0 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("JOBY"), 1).ToString()
                                ElseIf i = 1 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("COLVALY"), 0).ToString()
                                ElseIf i = 2 Then
                                    lbl.Text = FormatNumber(dsResult.Tables(0).Rows(0).Item("CYLVALY"), 0).ToString()
                                End If
                                tdInner.Controls.Add(lbl)

                            Case 5
                                InnerTdSetting(tdInner, "60px", "Right")
                                lbl = New Label
                                lbl.Text = "(#)"
                                tdInner.Controls.Add(lbl)
                        End Select
                        trInner.Controls.Add(tdInner)
                    Next
                    If (i Mod 2 = 0) Then
                        trInner.CssClass = "AlterNateColor1"
                    Else
                        trInner.CssClass = "AlterNateColor2"
                    End If
                    tblSSum.Controls.Add(trInner)
                Next
            End If


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetJobSeqResDetails:" + ex.Message.ToString()
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

    Protected Sub rdb_CheckedChanged(sender As Object, e As System.EventArgs)
        Dim rdb As New RadioButton
        Dim objUpIns As New E1UpInsData.UpdateInsert()
        rdb = DirectCast(sender, RadioButton)
        Dim CompId As String = ""
        Dim Value As String = ""
        Dim str As Integer = Integer.Parse(Regex.Replace(rdb.ID, "[^\d]", ""))

  Dim dsJobSqldata As New DataSet
        dsJobSqldata = CType(Session("dsJobSeqData"), DataSet)
        If dsJobSqldata.Tables(0).Rows.Count > 0 Then
        If rdb.Checked = True Then
            If rdb.ID.Contains("Head") Then
                Dim col As Integer = Integer.Parse(Regex.Replace(hidHdCol.Value, "[^\d]", ""))
                Dim cyl As Integer = Integer.Parse(Regex.Replace(hidHdCyl.Value, "[^\d]", ""))
                UHeadVal = arrPWid(str - 1)
                UColVal = arrCol(col - 1, str - 1)
                UCylVal = arrCyl(cyl - 1, str - 1)
                'hidHead.Value = rdb.ID.ToString()
                CompId = "1"
                Value = arrPWidId(str - 1)
                dcRadioSel(1) = arrPWidId(str - 1)
                dcRadioSel(2) = arrCylId(cyl - 1, str - 1)
                dcRadioSel(3) = arrColId(col - 1, str - 1)
                'GetPageDetails()
            ElseIf rdb.ID.Contains("rdbCyl") Then
                Dim head As Integer = Integer.Parse(Regex.Replace(hidHead.Value, "[^\d]", ""))
                UCylVal = arrCyl(str - 1, head - 1)
                'hidHdCyl.Value = rdb.ID.ToString()
                CompId = "2"
                Value = arrCylId(str - 1, head - 1)
                dcRadioSel(2) = arrCylId(str - 1, head - 1)
                'GetPageDetails()
            ElseIf rdb.ID.Contains("rdbCol") Then
                Dim head As Integer = Integer.Parse(Regex.Replace(hidHead.Value, "[^\d]", ""))
                UColVal = arrCol(str - 1, head - 1)
                'hidHdCol.Value = rdb.ID.ToString()
                CompId = "3"
                Value = arrColId(str - 1, head - 1)
                dcRadioSel.Remove(3)
                dcRadioSel(3) = arrColId(str - 1, head - 1)
                'GetPageDetails()
            ElseIf rdb.ID.Contains("RunPPROC") Then
                URPProc = arrRPProc(str - 1)
                'hidRPProc.Value = rdb.ID.ToString()
                CompId = "4"
                Value = arrRPProcId(str - 1)
                dcRadioSel(4) = arrRPProcId(str - 1)
                If unit <> "pieces" Then
                    TotalRunP = (URPProc + URPReg) / 2
                Else
                    cyclePerMin = URPProc
                End If

                'Dim RwSugg As String = TotalRunW.ToString()
                'Dim RtSugg As String = TotalRegT.ToString()
                'dcRadioSel.Add(4, arrRPProcId(str - 1))
                'GetRunPDetails()
            ElseIf rdb.ID.Contains("RunPREG") Then
                URPReg = arrRPReg(str - 1)
                ' hidRPReg.Value = rdb.ID.ToString()
                CompId = "5"
                Value = arrRPRegId(str - 1)
                dcRadioSel(5) = arrRPRegId(str - 1)
                TotalRunP = (URPProc + URPReg) / 2
                'GetRunPDetails()
            ElseIf rdb.ID.Contains("RunWPROC") Then
                URWProc = arrRWProc(str - 1)
                'hidRWProc.Value = rdb.ID.ToString()
                CompId = "6"
                Value = arrRWProcId(str - 1)
                dcRadioSel(6) = arrRWProcId(str - 1)
                TotalRunW = URWProc + URWReg
                'GetRunWDetails()
            ElseIf rdb.ID.Contains("RunWREG") Then
                URWReg = arrRWReg(str - 1)
                'hidRWReg.Value = rdb.ID.ToString()
                CompId = "7"
                Value = arrRWRegId(str - 1)
                dcRadioSel(7) = arrRWRegId(str - 1)
                TotalRunW = URWProc + URWReg
                'GetRunWDetails()
            ElseIf rdb.ID.Contains("RegTPROC") Then
                URTProc = arrRTProc(str - 1)
                'hidRTProc.Value = rdb.ID.ToString()
                CompId = "8"
                Value = arrRTProcId(str - 1)
                dcRadioSel(8) = arrRTProcId(str - 1)
                TotalRegT = URTProc + URTReg
                'GetRegTDetails()
            ElseIf rdb.ID.Contains("RegTREG") Then
                URTReg = arrRTReg(str - 1)
                'hidRTReg.Value = rdb.ID.ToString()
                CompId = "9"
                Value = arrRTRegId(str - 1)
                dcRadioSel(9) = arrRTRegId(str - 1)
                TotalRegT = URTProc + URTReg
                'GetRegTDetails()
            ElseIf rdb.ID.Contains("rdbRes") Then
                If str = 6 Then
                    hidJRes.Value = "Serial"
                Else
                    hidJRes.Value = "Parallel"
                End If
                CompId = "10"
                Value = str

            End If
        End If

        For Each selection As KeyValuePair(Of Integer, Integer) In dcRadioSel
            If hidEqId.Value = "3" Or hidEqId.Value = "26" Or hidEqId.Value = "83" Then
                objUpIns.UpInsEqCaseDetails(Session("E1CaseId"), hidEqId.Value, ddlDate.SelectedValue, "2", selection.Key, selection.Value, hidLayerId.Value)
            Else
                objUpIns.UpInsEqCaseDetails(Session("E1CaseId"), hidEqId.Value, ddlDate.SelectedValue, "1", selection.Key, selection.Value, hidLayerId.Value)
            End If
        Next

        UpdatePreffered()
        GetPercentageValue()
End If
        'objUpIns.UpInsSteelCaseDetails(Session("E1CaseId"), hidEqId.Value, ddlDate.SelectedValue, "1", CompId, Value)
        'GetHeadDetails()
        'GetPageDetails()
    End Sub

    Private Sub LinkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim count As Integer = Convert.ToInt32(hidRowNum.Value.ToString())
            For i = 0 To count
                DesgDesc(i) = Request.Form("txtDes" + (i + 1).ToString() + "")
                RSize(i) = Request.Form("txtRSize" + (i + 1).ToString() + "")
                For j = 0 To columnCount
                    columnVal(i, j) = Request.Form("txt_" + (i + 1).ToString() + "_" + (j + 1).ToString())
                Next

                'CylVal(i) = Request.Form("txtCyl" + (i + 1).ToString() + "")
            Next
            Session("Linkbuttonclicked") = "Addmore"
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hidRowNum.Value.ToString())
            numberDiv += 1
            hidRowNum.Value = numberDiv.ToString()
            GetJobSeqDetails()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub LinkButton_Click1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim count As Integer = Convert.ToInt32(hidRowNum.Value.ToString())
            For i = 0 To count
                DesgDesc(i) = Request.Form("txtDes" + (i + 1).ToString() + "")
                RSize(i) = Request.Form("txtRSize" + (i + 1).ToString() + "")
                For j = 0 To columnCount - 1
                    columnVal(i, j) = Request.Form("txt_" + (i + 1).ToString() + "_" + (j + 1).ToString())
                    'CylVal(i) = Request.Form("txtCyl" + (i + 1).ToString() + "")
                Next

            Next
            Session("Linkbuttonclicked") = "Duplicate"
            Dim numberDiv As Integer
            numberDiv = Convert.ToInt16(hidRowNum.Value.ToString())
            numberDiv += 1
            hidRowNum.Value = numberDiv.ToString()
            GetJobSeqDetails()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlDate_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlDate.SelectedIndexChanged
        Try
            'GetDefaultValue()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgUpdate.Click
        Try
            Dim objUpIns As New UpdateInsert()
            Dim UDesgDesc As String = ""
            Dim URSize As String = ""
            Dim UColValue(5) As String
            Dim UCylValue As String = ""
            Dim SeqPref(70) As String

            For i = 0 To 69
                UDesgDesc = Request.Form("txtDes" + (i + 1).ToString() + "")
                URSize = Request.Form("txtRSize" + (i + 1).ToString() + "")

                If URSize <> "" Then
                    If URSize = "" Then
                        URSize = "0"
                    End If

                    DesgDesc(i) = ""
                    RSize(i) = ""
                    For j = 0 To columnCount - 1
                        UColValue(j) = Request.Form("txt_" + (i + 1).ToString() + "_" + (j + 1).ToString() + "")
                        columnVal(i, j) = ""
                    Next

                    If UDesgDesc <> "" Then
                        objUpIns.UpInsJobSeqDetails(Session("E1CaseId"), hidEqId.Value, UDesgDesc, URSize, UColValue, colName, hidLayerId.Value, (i + 1).ToString(), columnCount)
                    End If
                Else
                    If URSize = "" And UDesgDesc = "" Then
                        objUpIns.DeleteJobSeqDetails(Session("E1CaseId"), hidEqId.Value, hidLayerId.Value, (i + 1).ToString())
                    End If
                End If
            Next


            Dim count As Integer = Convert.ToInt32(Session("SeqCount"))
            For i = 1 To count
                SeqPref(i - 1) = Request.Form("txtPref" + i.ToString() + "")
            Next
            objUpIns.UpInsJobSeqResDetails(Session("E1CaseId"), hidEqId.Value, SeqPref, count)

            UpdatePreffered()

            hidRowNum.Value = "1"

            Response.Redirect(HttpContext.Current.Request.Url.ToString(), True)
        Catch ex As Exception
            _lErrorLble.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Private Sub UpdatePreffered()
        Try
            Dim objUpIns As New UpdateInsert()
            Dim PiecePMin As String = Request.Form("txtPiece")
            Dim RrSugg As String
            Dim RpSugg As String
            If unit <> "pieces" Then
                RpSugg = TotalRunP.ToString()
            Else
                RpSugg = cyclePerMin * CDbl(PiecePMin)
            End If

            Dim RwSugg As String = TotalRunW.ToString()
            Dim RtSugg As String = TotalRegT.ToString()
            Dim MtSugg As String = UMainVal.ToString()
            Dim RpPref As String = Request.Form("txtRpPref")
            If RpPref <> "" Then
                RrSugg = (CDbl(RpPref) * regValue) / 100
            Else
                'RrSugg = (CDbl(TotalRunP) * regValue) / 100
                RrSugg = (CDbl(RpSugg) * regValue) / 100
            End If
            Dim RrPref As String = Request.Form("txtRrPref")
            Dim RwPref As String = Request.Form("txtRwPref")
            Dim RtPref As String = Request.Form("txtRtPref")
            Dim MtPref As String = Request.Form("txtMtPref")

            objUpIns.UpInsEqPrefDetails(Session("E1CaseId"), hidEqId.Value, ddlDate.SelectedValue, RpSugg, RrSugg, RwSugg, RtSugg, MtSugg, RpPref, RrPref, RwPref, RtPref, MtPref, UColVal, UCylVal, hidJRes.Value, cyclePerMin, PiecePMin, hidLayerId.Value)
            Calculate()
            GetDefaultValue()
        Catch ex As Exception
            _lErrorLble.Text = "Error:UpdatePreffered:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Protected Sub Calculate()
        Try
            Dim Econ1Conn As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim obj As New OperatingParamCalculation.OperatingParamCalculation(Econ1Conn)
            obj.OperatingParamCalculate(Session("E1CaseId"), hidEqId.Value, hidLayerId.Value)
        Catch ex As Exception
            _lErrorLble.Text = "Error:Calculate:" + ex.Message.ToString() + ""
        End Try
    End Sub

End Class
