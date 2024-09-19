Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System.Collections
Imports System
Imports System.Math
Imports System.Web.UI.HtmlTextWriter

Public Class StandBarrierGetData
    Public Class Calculations
        Dim SBAConnection As String = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
        Public OTRB(9) As String
        Public TotalOTRB As String = "0"
        Public TotalWVTRB As String = "0"
        Public WVTRB(9) As String
        Dim OTRMsgB, WVTRmsgB, TS1msgB, TS2msgB As String
        Public MsgB As String
        Public TS1B(9) As String
        Public TS2B(9) As String
        Public TotalTS1B As String = "0"
        Public TotalTS2B As String = "0"
        Public DG1B(9) As String
        Public TotalDG1B As String = "0"
        Public DG2B(9) As String
        Public TotalDG2B As String = "0"

        'Relative Humidity
        Public RH(9) As Double
        Public OTRH(9) As String
        Public OTRAIR(9) As String
        Public TotalOTRH As String = "0"
        Public LayerRH(9) As Double
        Public WVTRHUMID(9) As Double

        'Blend
        Public WVTR(9) As String
        Public WVTRL(9, 1) As Double
        Public OTR(9) As String
        Public OTRL(9, 1) As Double
        Public OTRBLENDSUB(9, 1) As Double
        Public WVTRBLENDSUB(9, 1) As Double
        Public TotalOTRBLEND As String = "0"
        Public TotalWVTRBLEND As String = "0"
     
        Dim OTRMsgL, WVTRmsgL, TSmsgL As String
        Public MsgL As String
        Dim OTRMsgBlendSub, WVTRmsgBlendSub, TS1msgBlendSub, TS2msgBlendSub As String
        Public MsgBlendSub As String

        'Blend for Relative humidity
        Public OTRRHB(9) As String
        Public OTRLOW(9, 1) As Double
        Public OTRSUB(9, 1) As Double
        Dim OTRMsgLow As String
        Public MsgLow As String
        Dim OTRMsgSub As String
        Public MsgSub As String

        'Tensile Strength
        Public TS1(9) As String
        Public TS1L(9, 1) As Double
        Public TS1P(9, 1) As Double
        Public DG1(9) As String
        Public DG1L(9, 1) As Double
        Public TS2(9) As String
        Public TS2L(9, 1) As Double
        Public TS2P(9, 1) As Double
        Public DG2(9) As String
        Public DG2L(9, 1) As Double
        Public TS1BLENDSUB(9, 1) As Double
        Public DG1BLENDSUB(9, 1) As Double
        Public TS2BLENDSUB(9, 1) As Double
        Public DG2BLENDSUB(9, 1) As Double
        Public TotalTS1BLEND As String = "0"
        Public TotalDG1BLEND As String = "0"
        Public TotalTS2BLEND As String = "0"
        Public TotalDG2BLEND As String = "0"

        Public Sub BarrierPropCalculate(ByVal CaseID As Integer, ByVal OtrTemp As String, ByVal WvtrTemp As String, ByVal OtrRH As String, ByVal WvtrRH As String, _
                                   ByVal MAT() As String, ByVal EDate As Date, ByVal MatDes() As String, ByVal OtrPref() As String, ByVal WVTRPref() As String _
                                   , ByVal Thick() As String, ByVal ISADJTHICK() As String, ByVal Dts As DataSet)
            Try
                Dim StrSql As String = String.Empty
                Dim StrSql1 As String = String.Empty
                'Dim Dts As New DataSet()
                Dim dvBarr As New DataView
                Dim dtBar As New DataTable
                Dim odbUtil As New DBUtil()
                Dim OTRFlag As String
                Dim WVTRFlag As String
                Dim dvBar As New DataView
                Dim dsBar As New DataSet()
                Dim WVTRFlagVal As String = ""
                TotalOTRB = "0"
                TotalWVTRB = "0"
                OTRMsgB = ""
                WVTRmsgB = ""
                MsgB = ""
                'Tensile Strength
                Dim TSFlag As String
                TS1msgB = ""
                TS2msgB = ""

                'Getting All Gradedata
                StrSql = "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MAT(0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MAT(1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MAT(2) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(2) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MAT(3) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(3) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MAT(4) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(4) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MAT(5) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(5) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MAT(6) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(6) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MAT(7) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(7) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MAT(8) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(8) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MAT(9) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(9) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                'Dts = odbUtil.FillDataSet(StrSql, SBAConnection)

                Dim OTRFlagVal As String = ""
                Dim TempRhVal As String = ""
                If Dts.Tables(0).Rows.Count > 0 Then
                    'Getting OTR Flag and Values
                    OTRFlag = GetOTRFlag(OtrTemp, OtrRH)
                    Dim strSpl() As String
                    strSpl = Regex.Split(OTRFlag, "-")
                    If strSpl.Length > 0 Then
                        OTRFlagVal = strSpl(0)
                        TempRhVal = strSpl(1)
                    End If


                    If OTRFlagVal = "1" Then
                        Dim tempId, rhId As String
                        Dim SplFlag1() As String
                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            tempId = SplFlag1(0)
                            rhId = SplFlag1(1)
                            For i = 0 To 9
                                dvBar = Dts.Tables(0).DefaultView
                                Try
                                    dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhId
                                    dtBar = dvBar.ToTable()
                                    If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                        OTRB(i) = "10000"
                                        If Not OTRMsgB.Contains(MatDes(i)) Then
                                            OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                        End If

                                    Else
                                        OTRB(i) = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                    End If
                                    If ISADJTHICK(i) <> "N" Then
                                        'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                                        OTRB(i) = CDbl((OTRB(i) / CDbl(Thick(i))) * 25.4)
                                    End If
                                Catch ex As Exception
                                    OTRB(i) = "0"
                                End Try
                            Next
                        End If
                    ElseIf OTRFlagVal = "3" Then
                        Dim tempId, rhIdL, rhIdH, rhValL, rhValH As String
                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String


                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            tempId = SplFlag1(0)
                            SplFlag2 = Regex.Split(SplFlag1(1), "#")
                            SplFlag3 = Regex.Split(SplFlag1(2), "#")
                            If SplFlag2.Length > 0 Then
                                rhIdL = SplFlag2(1)
                                rhValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    rhIdH = SplFlag3(1)
                                    rhValH = SplFlag3(0)
                                    For i = 0 To 9
                                        dvBar = Dts.Tables(0).DefaultView
                                        Try
                                            Dim lowRh As String = ""
                                            Dim highRh As String = ""
                                            Dim cngCounter As Integer = 0


                                            Try
                                                'Getting lower RH
                                                dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdL
                                                dtBar = dvBar.ToTable()
                                                If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                    lowRh = "10000"
                                                    If Not OTRMsgB.Contains(MatDes(i)) Then
                                                        OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                    End If
                                                    cngCounter = 1
                                                Else
                                                    lowRh = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                End If



                                                'Getting higher RH
                                                dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdH
                                                dtBar = dvBar.ToTable()
                                                If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                    highRh = "10000"
                                                    If cngCounter <> 1 Then
                                                        If Not OTRMsgB.Contains(MatDes(i)) Then
                                                            OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                        End If
                                                        cngCounter = 1
                                                    End If
                                                Else
                                                    highRh = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                End If

                                                OTRB(i) = CDbl(lowRh) + ((OtrRH - rhValL) / (rhValH - rhValL) * (CDbl(highRh) - CDbl(lowRh)))
                                                If ISADJTHICK(i) <> "N" Then
                                                    'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                                                    OTRB(i) = CDbl((OTRB(i) / CDbl(Thick(i))) * 25.4)
                                                End If
                                            Catch ex As Exception
                                                OTRB(i) = "0"
                                            End Try

                                        Catch ex As Exception

                                        End Try
                                    Next
                                End If

                            End If

                        End If
                    ElseIf OTRFlagVal = "4" Then
                        Dim rhId, tempIdL, tempIdH, tempValL, tempValH As String
                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String

                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            rhId = SplFlag1(0)
                            SplFlag2 = Regex.Split(SplFlag1(1), "#")
                            SplFlag3 = Regex.Split(SplFlag1(2), "#")
                            If SplFlag2.Length > 0 Then
                                tempIdL = SplFlag2(1)
                                tempValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    tempIdH = SplFlag3(1)
                                    tempValH = SplFlag3(0)
                                    For i = 0 To 9
                                        dvBar = Dts.Tables(0).DefaultView
                                        Try
                                            Dim lowTemp As String = ""
                                            Dim highTemp As String = ""
                                            Dim cngCounter As Integer = 0

                                            Try
                                                'Getting lower RH
                                                dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhId
                                                dtBar = dvBar.ToTable()
                                                If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                    lowTemp = "10000"
                                                    If Not OTRMsgB.Contains(MatDes(i)) Then
                                                        OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                    End If
                                                    cngCounter = 1
                                                Else
                                                    lowTemp = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                End If

                                                'Getting higher RH
                                                dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhId
                                                dtBar = dvBar.ToTable()
                                                If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                    highTemp = "10000"
                                                    If cngCounter <> 1 Then
                                                        If Not OTRMsgB.Contains(MatDes(i)) Then
                                                            OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                        End If
                                                        cngCounter = 1
                                                    End If
                                                Else
                                                    highTemp = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                End If
                                                OTRB(i) = CDbl(lowTemp) + ((OtrTemp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))
                                                If ISADJTHICK(i) <> "N" Then
                                                    'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                                                    OTRB(i) = CDbl((OTRB(i) / CDbl(Thick(i))) * 25.4)
                                                End If
                                            Catch ex As Exception
                                                OTRB(i) = "0"
                                            End Try

                                        Catch ex As Exception

                                        End Try
                                    Next
                                End If

                            End If

                        End If
                    ElseIf OTRFlagVal = "2" Then
                        Dim tempIdL, tempIdH, tempValL, tempValH As String
                        Dim rhIdL, rhIdH, rhValL, rhValH As String


                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String

                        Dim SplFlag4() As String
                        Dim SplFlag5() As String

                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then

                            SplFlag2 = Regex.Split(SplFlag1(0), "#")
                            SplFlag3 = Regex.Split(SplFlag1(1), "#")

                            SplFlag4 = Regex.Split(SplFlag1(2), "#")
                            SplFlag5 = Regex.Split(SplFlag1(3), "#")

                            If SplFlag2.Length > 0 Then
                                tempIdL = SplFlag2(1)
                                tempValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    tempIdH = SplFlag3(1)
                                    tempValH = SplFlag3(0)
                                    If SplFlag4.Length > 0 Then
                                        rhIdL = SplFlag4(1)
                                        rhValL = SplFlag4(0)
                                        If SplFlag5.Length > 0 Then
                                            rhIdH = SplFlag5(1)
                                            rhValH = SplFlag5(0)
                                            For i = 0 To 9
                                                dvBar = Dts.Tables(0).DefaultView
                                                Try
                                                    Dim tempLrhL, tempHrhH, tempLrhH, tempHrhL As String
                                                    Dim MidTemp1 As String
                                                    Dim MidTemp2 As String
                                                    Dim cngCounter As Integer = 0

                                                    Try
                                                        'Getting lower temp lower Rh
                                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdL
                                                        dtBar = dvBar.ToTable()
                                                        If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                            tempLrhL = "10000"
                                                            If Not OTRMsgB.Contains(MatDes(i)) Then
                                                                OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                            End If
                                                            cngCounter = 1
                                                        Else
                                                            tempLrhL = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                        End If


                                                        'Getting higher temp higher Rh
                                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdH
                                                        dtBar = dvBar.ToTable()
                                                        If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                            tempHrhH = "10000"
                                                            If cngCounter <> 1 Then
                                                                If Not OTRMsgB.Contains(MatDes(i)) Then
                                                                    OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                                End If
                                                                cngCounter = 1
                                                            End If
                                                        Else
                                                            tempHrhH = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                        End If


                                                        'Getting lower temp Higher Rh
                                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdH
                                                        dtBar = dvBar.ToTable()
                                                        If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                            tempLrhH = "10000"
                                                            If cngCounter <> 1 Then
                                                                If Not OTRMsgB.Contains(MatDes(i)) Then
                                                                    OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                                End If
                                                                cngCounter = 1
                                                            End If
                                                        Else
                                                            tempLrhH = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                        End If


                                                        'Getting higher temp lower Rh
                                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdL
                                                        dtBar = dvBar.ToTable()
                                                        If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                            tempHrhL = "10000"
                                                            If cngCounter <> 1 Then
                                                                If Not OTRMsgB.Contains(MatDes(i)) Then
                                                                    OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                                End If
                                                                cngCounter = 1
                                                            End If
                                                        Else
                                                            tempHrhL = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                        End If

                                                        MidTemp1 = CDbl(tempLrhL) + (CDbl(OtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhL) - CDbl(CDbl(tempLrhL)))))
                                                        MidTemp2 = CDbl(tempLrhH) + (CDbl(OtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhH) - CDbl(CDbl(tempLrhH)))))
                                                        OTRB(i) = MidTemp1 + ((OtrRH - rhValL) / (rhValH - rhValL) * (MidTemp2 - MidTemp1))
                                                        If ISADJTHICK(i) <> "N" Then
                                                            'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                                                            OTRB(i) = CDbl((OTRB(i) / CDbl(Thick(i))) * 25.4)
                                                        End If
                                                    Catch ex As Exception
                                                        OTRB(i) = "0"
                                                    End Try

                                                Catch ex As Exception

                                                End Try
                                            Next
                                        End If
                                    End If
                                End If

                            End If

                        End If
                    End If
                    '##############################################################################################################
                    '##############################################################################################################
                    'Getting WVTR Flag and Values
                    WVTRFlag = GetWVTRFlag(WvtrTemp, WvtrRH)
                    strSpl = Regex.Split(WVTRFlag, "-")
                    If strSpl.Length > 0 Then
                        WVTRFlagVal = strSpl(0)
                        TempRhVal = strSpl(1)
                    End If


                    If WVTRFlagVal = "1" Then
                        Dim tempId, rhId As String
                        Dim SplFlag1() As String
                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            tempId = SplFlag1(0)
                            rhId = SplFlag1(1)
                            For i = 0 To 9
                                dvBar = Dts.Tables(0).DefaultView
                                Try
                                    dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhId
                                    dtBar = dvBar.ToTable()
                                    If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                        WVTRB(i) = "10000"
                                        If Not WVTRmsgB.Contains(MatDes(i)) Then
                                            WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                        End If
                                    Else
                                        WVTRB(i) = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                    End If
                                    If ISADJTHICK(i) <> "N" Then
                                        'WVTRB(i) = CDbl(WVTRB(i)) / CDbl(Thick(i))
                                        WVTRB(i) = CDbl((WVTRB(i) / CDbl(Thick(i))) * 25.4)
                                    End If
                                Catch ex As Exception
                                    WVTRB(i) = "0"

                                End Try
                            Next
                        End If
                    ElseIf WVTRFlagVal = "3" Then
                        Dim tempId, rhIdL, rhIdH, rhValL, rhValH As String
                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String

                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            tempId = SplFlag1(0)
                            SplFlag2 = Regex.Split(SplFlag1(1), "#")
                            SplFlag3 = Regex.Split(SplFlag1(2), "#")
                            If SplFlag2.Length > 0 Then
                                rhIdL = SplFlag2(1)
                                rhValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    rhIdH = SplFlag3(1)
                                    rhValH = SplFlag3(0)
                                    For i = 0 To 9
                                        dvBar = Dts.Tables(0).DefaultView
                                        Try
                                            Dim lowRh As String = ""
                                            Dim highRh As String = ""
                                            Dim cngCounter As Integer = 0
                                            Try
                                                'Getting lower RH
                                                dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdL
                                                dtBar = dvBar.ToTable()
                                                If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                    lowRh = "10000"
                                                    If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                        WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                    End If
                                                    cngCounter = 1
                                                Else
                                                    lowRh = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                End If

                                                'Getting higher RH
                                                dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdH
                                                dtBar = dvBar.ToTable()
                                                If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                    highRh = "10000"
                                                    If cngCounter <> 1 Then
                                                        If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                            WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                        End If
                                                        cngCounter = 1
                                                    End If
                                                Else
                                                    highRh = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                End If
                                                WVTRB(i) = CDbl(lowRh) + ((WvtrRH - rhValL) / (rhValH - rhValL) * (CDbl(highRh) - CDbl(lowRh)))
                                                If ISADJTHICK(i) <> "N" Then
                                                    'WVTRB(i) = CDbl(WVTRB(i)) / CDbl(Thick(i))
                                                    WVTRB(i) = CDbl((WVTRB(i) / CDbl(Thick(i))) * 25.4)
                                                End If
                                            Catch ex As Exception
                                                WVTRB(i) = "0"
                                            End Try
                                        Catch ex As Exception
                                        End Try
                                    Next
                                End If

                            End If

                        End If
                    ElseIf WVTRFlagVal = "4" Then
                        Dim rhId, tempIdL, tempIdH, tempValL, tempValH As String
                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String

                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            rhId = SplFlag1(0)
                            SplFlag2 = Regex.Split(SplFlag1(1), "#")
                            SplFlag3 = Regex.Split(SplFlag1(2), "#")
                            If SplFlag2.Length > 0 Then
                                tempIdL = SplFlag2(1)
                                tempValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    tempIdH = SplFlag3(1)
                                    tempValH = SplFlag3(0)
                                    For i = 0 To 9
                                        dvBar = Dts.Tables(0).DefaultView
                                        Try
                                            Dim lowTemp As String = ""
                                            Dim highTemp As String = ""
                                            Dim cngCounter As Integer = 0
                                            Try
                                                'Getting lower RH
                                                dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhId
                                                dtBar = dvBar.ToTable()
                                                If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                    lowTemp = "10000"
                                                    If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                        WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                    End If
                                                    cngCounter = 1
                                                Else
                                                    lowTemp = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                End If


                                                'Getting higher RH
                                                dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhId
                                                dtBar = dvBar.ToTable()
                                                If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                    highTemp = "10000"
                                                    If cngCounter <> 1 Then
                                                        If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                            WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                        End If
                                                        cngCounter = 1
                                                    End If
                                                Else
                                                    highTemp = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                End If

                                                WVTRB(i) = CDbl(lowTemp) + ((WvtrTemp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))
                                                If ISADJTHICK(i) <> "N" Then
                                                    'WVTRB(i) = CDbl(WVTRB(i)) / CDbl(Thick(i))
                                                    WVTRB(i) = CDbl((WVTRB(i) / CDbl(Thick(i))) * 25.4)
                                                End If
                                            Catch ex As Exception
                                                WVTRB(i) = "0"
                                            End Try
                                        Catch ex As Exception

                                        End Try
                                    Next
                                End If

                            End If

                        End If
                    ElseIf WVTRFlagVal = "2" Then
                        Dim tempIdL, tempIdH, tempValL, tempValH As String
                        Dim rhIdL, rhIdH, rhValL, rhValH As String


                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String

                        Dim SplFlag4() As String
                        Dim SplFlag5() As String

                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then

                            SplFlag2 = Regex.Split(SplFlag1(0), "#")
                            SplFlag3 = Regex.Split(SplFlag1(1), "#")

                            SplFlag4 = Regex.Split(SplFlag1(2), "#")
                            SplFlag5 = Regex.Split(SplFlag1(3), "#")

                            If SplFlag2.Length > 0 Then
                                tempIdL = SplFlag2(1)
                                tempValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    tempIdH = SplFlag3(1)
                                    tempValH = SplFlag3(0)
                                    If SplFlag4.Length > 0 Then
                                        rhIdL = SplFlag4(1)
                                        rhValL = SplFlag4(0)
                                        If SplFlag5.Length > 0 Then
                                            rhIdH = SplFlag5(1)
                                            rhValH = SplFlag5(0)
                                            For i = 0 To 9
                                                dvBar = Dts.Tables(0).DefaultView
                                                Try
                                                    Dim tempLrhL, tempHrhH, tempLrhH, tempHrhL As String
                                                    Dim MidTemp1 As String
                                                    Dim MidTemp2 As String
                                                    Dim cngCounter As Integer = 0
                                                    Try
                                                        'Getting lower temp lower Rh
                                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdL
                                                        dtBar = dvBar.ToTable()
                                                        If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                            tempLrhL = "10000"
                                                            If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                                WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                            End If
                                                            cngCounter = 1
                                                        Else
                                                            tempLrhL = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                        End If


                                                        'Getting higher temp higher Rh
                                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdH
                                                        dtBar = dvBar.ToTable()
                                                        If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                            tempHrhH = "10000"
                                                            If cngCounter <> 1 Then
                                                                If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                                    WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                                End If
                                                                cngCounter = 1
                                                            End If
                                                        Else
                                                            tempHrhH = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                        End If


                                                        'Getting lower temp Higher Rh
                                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdH
                                                        dtBar = dvBar.ToTable()
                                                        If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                            tempLrhH = "10000"
                                                            If cngCounter <> 1 Then
                                                                If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                                    WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                                End If
                                                                cngCounter = 1
                                                            End If
                                                        Else
                                                            tempLrhH = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                        End If


                                                        'Getting higher temp lower Rh
                                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdL
                                                        dtBar = dvBar.ToTable()
                                                        If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                            tempHrhL = "10000"
                                                            If cngCounter <> 1 Then
                                                                If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                                    WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                                End If
                                                                cngCounter = 1
                                                            End If
                                                        Else
                                                            tempHrhL = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                        End If


                                                        MidTemp1 = CDbl(tempLrhL) + (CDbl(WvtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhL) - CDbl(CDbl(tempLrhL)))))
                                                        MidTemp2 = CDbl(tempLrhH) + (CDbl(WvtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhH) - CDbl(CDbl(tempLrhH)))))
                                                        WVTRB(i) = MidTemp1 + ((WvtrRH - rhValL) / (rhValH - rhValL) * (MidTemp2 - MidTemp1))
                                                        If ISADJTHICK(i) <> "N" Then
                                                            'WVTRB(i) = CDbl(WVTRB(i)) / CDbl(Thick(i))
                                                            WVTRB(i) = CDbl((WVTRB(i) / CDbl(Thick(i))) * 25.4)
                                                        End If
                                                    Catch ex As Exception
                                                        WVTRB(i) = "0"
                                                    End Try

                                                Catch ex As Exception

                                                End Try
                                            Next
                                        End If
                                    End If
                                End If

                            End If

                        End If
                    End If
                    '######################################################################################################################
                    '######################################################################################################################
                    'Getting Tensile strength Flag and Values
                    Dim TSFlagVal As String = ""
                    TSFlag = GetTSFlag("25", "100")
                    Dim strTSpl() As String
                    strTSpl = Regex.Split(TSFlag, "-")
                    If strTSpl.Length > 0 Then
                        TSFlagVal = strTSpl(0)
                        TempRhVal = strTSpl(1)
                    End If


                    If TSFlagVal = "1" Then
                        Dim tempId, rhId As String
                        Dim SplFlag1() As String
                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            tempId = SplFlag1(0)
                            rhId = SplFlag1(1)
                            For i = 0 To 9
                                dvBar = Dts.Tables(0).DefaultView
                                Try
                                    dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhId
                                    dtBar = dvBar.ToTable()
                                    Try
                                        If dtBar.Rows(0).Item("MDBREAK").ToString() = "" Then
                                            TS1B(i) = ""
                                            If Not TS1msgB.Contains(MatDes(i)) Then
                                                TS1msgB = TS1msgB + "" + MatDes(i) + " (TS1)\n"
                                            End If
                                        Else
                                            TS1B(i) = dtBar.Rows(0).Item("MDBREAK").ToString()
                                        End If
                                    Catch ex As Exception
                                        TS1B(i) = "0"
                                    End Try

                                    Try
                                        If dtBar.Rows(0).Item("TDBREAK").ToString() = "" Then
                                            TS2B(i) = ""
                                            If Not TS2msgB.Contains(MatDes(i)) Then
                                                TS2msgB = TS2msgB + "" + MatDes(i) + " (TS2)\n"
                                            End If
                                        Else
                                            TS2B(i) = dtBar.Rows(0).Item("TDBREAK").ToString()
                                        End If
                                    Catch ex As Exception
                                        TS2B(i) = "0"
                                    End Try
                                    
                                    If ISADJTHICK(i) <> "N" Then
                                        
                                    End If
                                Catch ex As Exception

                                End Try
                            Next
                        End If
                    ElseIf TSFlagVal = "3" Then
                        Dim tempId, rhIdL, rhIdH, rhValL, rhValH As String
                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String


                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            tempId = SplFlag1(0)
                            SplFlag2 = Regex.Split(SplFlag1(1), "#")
                            SplFlag3 = Regex.Split(SplFlag1(2), "#")
                            If SplFlag2.Length > 0 Then
                                rhIdL = SplFlag2(1)
                                rhValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    rhIdH = SplFlag3(1)
                                    rhValH = SplFlag3(0)
                                    For i = 0 To 9
                                        dvBar = Dts.Tables(0).DefaultView
                                        Try
                                            Dim lowRh1 As String = ""
                                            Dim highRh1 As String = ""
                                            Dim cngCounter1 As Integer = 0

                                            Dim lowRh2 As String = ""
                                            Dim highRh2 As String = ""
                                            Dim cngCounter2 As Integer = 0


                                            Try
                                                'Getting lower RH
                                                dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdL
                                                dtBar = dvBar.ToTable()
                                                If dtBar.Rows(0).Item("MDBREAK").ToString() = "" Then
                                                    lowRh1 = ""
                                                    If Not TS1msgB.Contains(MatDes(i)) Then
                                                        TS1msgB = TS1msgB + "" + MatDes(i) + " (TS1)\n"
                                                    End If
                                                    cngCounter1 = 1
                                                Else
                                                    lowRh1 = dtBar.Rows(0).Item("MDBREAK").ToString()
                                                End If

                                                If dtBar.Rows(0).Item("TDBREAK").ToString() = "" Then
                                                    lowRh2 = ""
                                                    If Not TS2msgB.Contains(MatDes(i)) Then
                                                        TS2msgB = TS2msgB + "" + MatDes(i) + " (TS2)\n"
                                                    End If
                                                    cngCounter2 = 1
                                                Else
                                                    lowRh2 = dtBar.Rows(0).Item("TDBREAK").ToString()
                                                End If

                                                'Getting higher RH
                                                dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdH
                                                dtBar = dvBar.ToTable()
                                                If dtBar.Rows(0).Item("MDBREAK").ToString() = "" Then
                                                    highRh1 = ""
                                                    If cngCounter1 <> 1 Then
                                                        If Not TS1msgB.Contains(MatDes(i)) Then
                                                            TS1msgB = TS1msgB + "" + MatDes(i) + " (TS1)\n"
                                                        End If
                                                        cngCounter1 = 1
                                                    End If
                                                Else
                                                    highRh1 = dtBar.Rows(0).Item("MDBREAK").ToString()
                                                End If

                                                If dtBar.Rows(0).Item("TDBREAK").ToString() = "" Then
                                                    highRh2 = ""
                                                    If cngCounter2 <> 1 Then
                                                        If Not TS2msgB.Contains(MatDes(i)) Then
                                                            TS2msgB = TS2msgB + "" + MatDes(i) + " (TS2)\n"
                                                        End If
                                                        cngCounter2 = 1
                                                    End If
                                                Else
                                                    highRh2 = dtBar.Rows(0).Item("TDBREAK").ToString()
                                                End If

                                                TS1B(i) = CDbl(lowRh1) + ((OtrRH - rhValL) / (rhValH - rhValL) * (CDbl(highRh1) - CDbl(lowRh1)))
                                                TS2B(i) = CDbl(lowRh2) + ((OtrRH - rhValL) / (rhValH - rhValL) * (CDbl(highRh2) - CDbl(lowRh2)))
                                                If ISADJTHICK(i) <> "N" Then
                                                    'TSB(i) = CDbl(TSB(i)) / CDbl(Thick(i))
                                                    'TS1B(i) = CDbl((TS1B(i) / CDbl(Thick(i))) * 25.4)
                                                    'TS2B(i) = CDbl((TS2B(i) / CDbl(Thick(i))) * 25.4)
                                                End If
                                            Catch ex As Exception
                                                TS1B(i) = "0"
                                                TS2B(i) = "0"
                                            End Try

                                        Catch ex As Exception

                                        End Try
                                    Next
                                End If

                            End If

                        End If
                    ElseIf TSFlagVal = "4" Then
                        Dim rhId, tempIdL, tempIdH, tempValL, tempValH As String
                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String

                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            rhId = SplFlag1(0)
                            SplFlag2 = Regex.Split(SplFlag1(1), "#")
                            SplFlag3 = Regex.Split(SplFlag1(2), "#")
                            If SplFlag2.Length > 0 Then
                                tempIdL = SplFlag2(1)
                                tempValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    tempIdH = SplFlag3(1)
                                    tempValH = SplFlag3(0)
                                    For i = 0 To 9
                                        dvBar = Dts.Tables(0).DefaultView
                                        Try
                                            Dim lowTemp1 As String = ""
                                            Dim highTemp1 As String = ""
                                            Dim cngCounter1 As Integer = 0

                                            Dim lowTemp2 As String = ""
                                            Dim highTemp2 As String = ""
                                            Dim cngCounter2 As Integer = 0

                                            Try
                                                'Getting lower RH
                                                dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhId
                                                dtBar = dvBar.ToTable()
                                                If dtBar.Rows(0).Item("MDBREAK").ToString() = "" Then
                                                    lowTemp1 = ""
                                                    If Not TS1msgB.Contains(MatDes(i)) Then
                                                        TS1msgB = TS1msgB + "" + MatDes(i) + " (TS1)\n"
                                                    End If
                                                    cngCounter1 = 1
                                                Else
                                                    lowTemp1 = dtBar.Rows(0).Item("MDBREAK").ToString()
                                                End If

                                                If dtBar.Rows(0).Item("TDBREAK").ToString() = "" Then
                                                    lowTemp2 = ""
                                                    If Not TS2msgB.Contains(MatDes(i)) Then
                                                        TS2msgB = TS2msgB + "" + MatDes(i) + " (TS2)\n"
                                                    End If
                                                    cngCounter2 = 1
                                                Else
                                                    lowTemp2 = dtBar.Rows(0).Item("TDBREAK").ToString()
                                                End If

                                                'Getting higher RH
                                                dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhId
                                                dtBar = dvBar.ToTable()
                                                If dtBar.Rows(0).Item("MDBREAK").ToString() = "" Then
                                                    highTemp1 = ""
                                                    If cngCounter1 <> 1 Then
                                                        If Not TS1msgB.Contains(MatDes(i)) Then
                                                            TS1msgB = TS1msgB + "" + MatDes(i) + " (TS1)\n"
                                                        End If
                                                        cngCounter1 = 1
                                                    End If
                                                Else
                                                    highTemp1 = dtBar.Rows(0).Item("MDBREAK").ToString()
                                                End If

                                                If dtBar.Rows(0).Item("TDBREAK").ToString() = "" Then
                                                    highTemp2 = ""
                                                    If cngCounter2 <> 1 Then
                                                        If Not TS2msgB.Contains(MatDes(i)) Then
                                                            TS2msgB = TS2msgB + "" + MatDes(i) + " (TS2)\n"
                                                        End If
                                                        cngCounter2 = 1
                                                    End If
                                                Else
                                                    highTemp2 = dtBar.Rows(0).Item("TDBREAK").ToString()
                                                End If

                                                TS1B(i) = CDbl(lowTemp1) + ((OtrTemp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp1) - CDbl(lowTemp1)))
                                                TS2B(i) = CDbl(lowTemp2) + ((OtrTemp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp2) - CDbl(lowTemp2)))

                                                If ISADJTHICK(i) <> "N" Then
                                                    'TSB(i) = CDbl(TSB(i)) / CDbl(Thick(i))
                                                    'TS1B(i) = CDbl((TS1B(i) / CDbl(Thick(i))) * 25.4)
                                                    'TS2B(i) = CDbl((TS2B(i) / CDbl(Thick(i))) * 25.4)
                                                End If
                                            Catch ex As Exception
                                                TS1B(i) = "0"
                                                TS2B(i) = "0"
                                            End Try

                                        Catch ex As Exception

                                        End Try
                                    Next
                                End If

                            End If

                        End If
                    ElseIf TSFlagVal = "2" Then
                        Dim tempIdL, tempIdH, tempValL, tempValH As String
                        Dim rhIdL, rhIdH, rhValL, rhValH As String


                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String

                        Dim SplFlag4() As String
                        Dim SplFlag5() As String

                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then

                            SplFlag2 = Regex.Split(SplFlag1(0), "#")
                            SplFlag3 = Regex.Split(SplFlag1(1), "#")

                            SplFlag4 = Regex.Split(SplFlag1(2), "#")
                            SplFlag5 = Regex.Split(SplFlag1(3), "#")

                            If SplFlag2.Length > 0 Then
                                tempIdL = SplFlag2(1)
                                tempValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    tempIdH = SplFlag3(1)
                                    tempValH = SplFlag3(0)
                                    If SplFlag4.Length > 0 Then
                                        rhIdL = SplFlag4(1)
                                        rhValL = SplFlag4(0)
                                        If SplFlag5.Length > 0 Then
                                            rhIdH = SplFlag5(1)
                                            rhValH = SplFlag5(0)
                                            For i = 0 To 9
                                                dvBar = Dts.Tables(0).DefaultView
                                                Try
                                                    Dim tempLrhL1, tempHrhH1, tempLrhH1, tempHrhL1 As String
                                                    Dim tempLrhL2, tempHrhH2, tempLrhH2, tempHrhL2 As String
                                                    Dim Mid1Temp1, Mid1Temp2 As String
                                                    Dim Mid2Temp1, Mid2Temp2 As String
                                                    Dim cngCounter1 As Integer = 0
                                                    Dim cngCounter2 As Integer = 0

                                                    Try
                                                        'Getting lower temp lower Rh
                                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdL
                                                        dtBar = dvBar.ToTable()
                                                        If dtBar.Rows(0).Item("MDBREAK").ToString() = "" Then
                                                            tempLrhL1 = ""
                                                            If Not TS1msgB.Contains(MatDes(i)) Then
                                                                TS1msgB = TS1msgB + "" + MatDes(i) + " (TS1)\n"
                                                            End If
                                                            cngCounter1 = 1
                                                        Else
                                                            tempLrhL1 = dtBar.Rows(0).Item("MDBREAK").ToString()
                                                        End If

                                                        If dtBar.Rows(0).Item("TDBREAK").ToString() = "" Then
                                                            tempLrhL2 = ""
                                                            If Not TS2msgB.Contains(MatDes(i)) Then
                                                                TS2msgB = TS2msgB + "" + MatDes(i) + " (TS2)\n"
                                                            End If
                                                            cngCounter2 = 1
                                                        Else
                                                            tempLrhL2 = dtBar.Rows(0).Item("TDBREAK").ToString()
                                                        End If


                                                        'Getting higher temp higher Rh
                                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdH
                                                        dtBar = dvBar.ToTable()
                                                        If dtBar.Rows(0).Item("MDBREAK").ToString() = "" Then
                                                            tempHrhH1 = ""
                                                            If cngCounter1 <> 1 Then
                                                                If Not TS1msgB.Contains(MatDes(i)) Then
                                                                    TS1msgB = TS1msgB + "" + MatDes(i) + " (TS1)\n"
                                                                End If
                                                                cngCounter1 = 1
                                                            End If
                                                        Else
                                                            tempHrhH1 = dtBar.Rows(0).Item("MDBREAK").ToString()
                                                        End If

                                                        If dtBar.Rows(0).Item("TDYIELD").ToString() = "" Then
                                                            tempHrhH2 = ""
                                                            If cngCounter2 <> 1 Then
                                                                If Not TS2msgB.Contains(MatDes(i)) Then
                                                                    TS2msgB = TS2msgB + "" + MatDes(i) + " (TS2)\n"
                                                                End If
                                                                cngCounter2 = 1
                                                            End If
                                                        Else
                                                            tempHrhH2 = dtBar.Rows(0).Item("TDBREAK").ToString()
                                                        End If


                                                        'Getting lower temp Higher Rh
                                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdH
                                                        dtBar = dvBar.ToTable()
                                                        If dtBar.Rows(0).Item("MDBREAK").ToString() = "" Then
                                                            tempLrhH1 = ""
                                                            If cngCounter1 <> 1 Then
                                                                If Not TS1msgB.Contains(MatDes(i)) Then
                                                                    TS1msgB = TS1msgB + "" + MatDes(i) + " (TS1)\n"
                                                                End If
                                                                cngCounter1 = 1
                                                            End If
                                                        Else
                                                            tempLrhH1 = dtBar.Rows(0).Item("MDBREAK").ToString()
                                                        End If

                                                        If dtBar.Rows(0).Item("TDBREAK").ToString() = "" Then
                                                            tempLrhH2 = ""
                                                            If cngCounter2 <> 1 Then
                                                                If Not TS2msgB.Contains(MatDes(i)) Then
                                                                    TS2msgB = TS2msgB + "" + MatDes(i) + " (TS2)\n"
                                                                End If
                                                                cngCounter2 = 1
                                                            End If
                                                        Else
                                                            tempLrhH2 = dtBar.Rows(0).Item("TDBREAK").ToString()
                                                        End If


                                                        'Getting higher temp lower Rh
                                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdL
                                                        dtBar = dvBar.ToTable()
                                                        If dtBar.Rows(0).Item("MDBREAK").ToString() = "" Then
                                                            tempHrhL1 = ""
                                                            If cngCounter1 <> 1 Then
                                                                If Not TS1msgB.Contains(MatDes(i)) Then
                                                                    TS1msgB = TS1msgB + "" + MatDes(i) + " (TS1)\n"
                                                                End If
                                                                cngCounter1 = 1
                                                            End If
                                                        Else
                                                            tempHrhL1 = dtBar.Rows(0).Item("MDBREAK").ToString()
                                                        End If

                                                        If dtBar.Rows(0).Item("TDBREAK").ToString() = "" Then
                                                            tempHrhL2 = ""
                                                            If cngCounter2 <> 1 Then
                                                                If Not TS2msgB.Contains(MatDes(i)) Then
                                                                    TS2msgB = TS2msgB + "" + MatDes(i) + " (TS2)\n"
                                                                End If
                                                                cngCounter2 = 1
                                                            End If
                                                        Else
                                                            tempHrhL2 = dtBar.Rows(0).Item("TDBREAK").ToString()
                                                        End If

                                                        Mid1Temp1 = CDbl(tempLrhL1) + (CDbl(OtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhL1) - CDbl(CDbl(tempLrhL1)))))
                                                        Mid1Temp2 = CDbl(tempLrhH1) + (CDbl(OtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhH1) - CDbl(CDbl(tempLrhH1)))))

                                                        Mid2Temp1 = CDbl(tempLrhL2) + (CDbl(OtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhL2) - CDbl(CDbl(tempLrhL2)))))
                                                        Mid2Temp2 = CDbl(tempLrhH2) + (CDbl(OtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhH2) - CDbl(CDbl(tempLrhH2)))))

                                                        TS1B(i) = Mid1Temp1 + ((OtrRH - rhValL) / (rhValH - rhValL) * (Mid1Temp2 - Mid1Temp1))
                                                        TS2B(i) = Mid2Temp1 + ((OtrRH - rhValL) / (rhValH - rhValL) * (Mid2Temp2 - Mid2Temp1))

                                                        If ISADJTHICK(i) <> "N" Then
                                                            'TSB(i) = CDbl(TSB(i)) / CDbl(Thick(i))
                                                            'TS1B(i) = CDbl((TS1B(i) / CDbl(Thick(i))) * 25.4)
                                                            'TS2B(i) = CDbl((TS2B(i) / CDbl(Thick(i))) * 25.4)
                                                        End If
                                                    Catch ex As Exception
                                                        TS1B(i) = "0"
                                                        TS2B(i) = "0"
                                                    End Try

                                                Catch ex As Exception

                                                End Try
                                            Next
                                        End If
                                    End If
                                End If

                            End If

                        End If
                    End If
                    '##############################################################################################################
                    If OTRMsgB <> "" Or WVTRmsgB <> "" Or TS1msgB <> "" Or TS2msgB <> "" Then
                        MsgB = "Data is not present for following materials for Date " + CDate(EDate).Date + "\n"
                        If OTRMsgB <> "" Then
                            MsgB = MsgB + OTRMsgB + ""
                        End If
                        If WVTRmsgB <> "" Then
                            MsgB = MsgB + WVTRmsgB + ""
                        End If
                        If TS1msgB <> "" Then
                            MsgB = MsgB + TS1msgB + ""
                        End If
                        If TS2msgB <> "" Then
                            MsgB = MsgB + TS2msgB + ""
                        End If
                    End If
                    ''TotalOTR = "0"
                    'For i = 0 To 9
                    '    If MAT(i) <> "0" Then
                    '        If OTRB(i) <> 0 Then
                    '            If Not IsNumeric(OtrPref(i)) Then
                    '                TotalOTRB = CDbl(TotalOTRB) + (1 / CDbl(OTRB(i)))
                    '            Else
                    '                'If (Ds.Tables(0).Rows(0).Item("OTR" + (i + 1).ToString())) <> 0 Then
                    '                TotalOTRB = CDbl(TotalOTRB) + (1 / OtrPref(i))
                    '                'End If


                    '            End If
                    '        Else
                    '            If IsNumeric(OtrPref(i)) Then
                    '                TotalOTRB = CDbl(TotalOTRB) + (1 / OtrPref(i))
                    '            Else
                    '                TotalOTRB = CDbl(TotalOTRB) + (1 / CDbl(OTRB(i)))
                    '            End If

                    '        End If
                    '    End If


                    'Next
                    'If TotalOTRB <> "0" Then
                    '    TotalOTRB = 1 / CDbl(TotalOTRB)
                    'End If


                    ''TotalWVTR 
                    'For i = 0 To 9
                    '    If MAT(i) <> 0 Then
                    '        If WVTRB(i) <> 0 Then
                    '            If Not IsNumeric(WVTRPref(i)) Then
                    '                TotalWVTRB = CDbl(TotalWVTRB) + (1 / CDbl(WVTRB(i)))
                    '            Else
                    '                'If (Ds.Tables(0).Rows(0).Item("WVTR" + (i + 1).ToString())) <> 0 Then
                    '                TotalWVTRB = CDbl(TotalWVTRB) + (1 / CDbl(WVTRPref(i)))
                    '                'End If

                    '            End If
                    '        Else
                    '            If IsNumeric(WVTRPref(i)) Then
                    '                'If (Ds.Tables(0).Rows(0).Item("WVTR" + (i + 1).ToString())) <> 0 Then
                    '                TotalWVTRB = CDbl(TotalWVTRB) + (1 / CDbl(WVTRPref(i)))
                    '            Else
                    '                TotalWVTRB = CDbl(TotalWVTRB) + (1 / CDbl(WVTRB(i)))
                    '                'End If
                    '            End If
                    '        End If
                    '    End If


                    'Next
                    'If TotalWVTRB <> "0" Then
                    '    TotalWVTRB = 1 / CDbl(TotalWVTRB)
                    'End If
                End If
            Catch ex As Exception

            End Try
        End Sub

        Public Sub BarrierPropCalculateNew(ByVal CaseID As Integer, ByVal OtrTemp As String, ByVal WvtrTemp As String, ByVal OtrRH As String, ByVal WvtrRH As String, _
                                          ByVal MAT() As String, ByVal EDate As Date, ByVal MatDes() As String, ByVal OtrPref() As String, ByVal WVTRPref() As String, _
                                          ByVal TS1Pref() As String, ByVal TS2Pref() As String, ByVal DGVal() As String, ByVal Thick(,) As String, ByVal ISADJTHICK() As String, _
                                          ByVal BMAT(,) As String, ByVal BISADJTHICK(,) As String, ByVal ThickPer(,) As String, ByVal OTRSub(,) As String, ByVal WVTRSub(,) As String, _
                                          ByVal TS1Sub(,) As String, ByVal TS2Sub(,) As String, ByVal BDGVal(,) As String, ByVal BThick(,) As String, ByVal k As String, _
                                          ByVal Dts As DataSet, ByVal BMatDes(,) As String, ByVal MatPer() As String, ByVal BMatPer(,) As String)
            Try
                Dim StrSql As String = String.Empty
                Dim StrSql1 As String = String.Empty
                Dim dvBarr As New DataView
                Dim dtBar As New DataTable
                Dim odbUtil As New DBUtil()
                Dim OTRFlag As String
                Dim WVTRFlag As String
                Dim dvBar As New DataView
                Dim dsBar As New DataSet()
                Dim WVTRFlagVal As String = ""
                TotalOTRB = "0"
                TotalWVTRB = "0"
                TotalTS1B = "0"
                TotalTS2B = "0"
                OTRMsgB = ""
                WVTRmsgB = ""
                MsgB = ""

                Dim OTRFlagVal As String = ""
                Dim TempRhVal As String = ""
                If Dts.Tables(0).Rows.Count > 0 Then

                    BarrierPropCalBlendSubLayers(CaseID, OtrTemp, WvtrTemp, OtrRH, WvtrRH, BMAT, EDate, BMatDes, OTRSub, WVTRSub, TS1Sub, TS2Sub, BDGVal, BThick, BISADJTHICK, ThickPer, OtrPref, WVTRPref, TS1Pref, TS2Pref, DGVal, MatDes, Dts, MatPer, BMatPer)

                    'Getting OTR Flag and Values
                    OTRFlag = GetOTRFlag(OtrTemp, OtrRH)
                    Dim strSpl() As String
                    strSpl = Regex.Split(OTRFlag, "-")
                    If strSpl.Length > 0 Then
                        OTRFlagVal = strSpl(0)
                        TempRhVal = strSpl(1)
                    End If
                    For i = 0 To 9
                        If MAT(i) > 500 And MAT(i) < 506 Then
                            OTRB(i) = OTR(i)
                        Else
                            If OTRFlagVal = "1" Then
                                Dim tempId, rhId As String
                                Dim SplFlag1() As String
                                SplFlag1 = Regex.Split(TempRhVal, "##")
                                If SplFlag1.Length > 0 Then
                                    tempId = SplFlag1(0)
                                    rhId = SplFlag1(1)

                                    dvBar = Dts.Tables(0).DefaultView
                                    Try
                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhId
                                        dtBar = dvBar.ToTable()
                                        If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                            OTRB(i) = "10000"
                                            If Not OTRMsgB.Contains(MatDes(i)) Then
                                                OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                            End If

                                        Else
                                            OTRB(i) = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                        End If
                                        If ISADJTHICK(i) <> "N" Then
                                            'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                                            OTRB(i) = CDbl((OTRB(i) / CDbl(Thick(k, i))) * 25.4)
                                        End If
                                    Catch ex As Exception
                                        OTRB(i) = "0"
                                    End Try

                                End If
                            ElseIf OTRFlagVal = "3" Then
                                Dim tempId, rhIdL, rhIdH, rhValL, rhValH As String
                                Dim SplFlag1() As String
                                Dim SplFlag2() As String
                                Dim SplFlag3() As String


                                SplFlag1 = Regex.Split(TempRhVal, "##")
                                If SplFlag1.Length > 0 Then
                                    tempId = SplFlag1(0)
                                    SplFlag2 = Regex.Split(SplFlag1(1), "#")
                                    SplFlag3 = Regex.Split(SplFlag1(2), "#")
                                    If SplFlag2.Length > 0 Then
                                        rhIdL = SplFlag2(1)
                                        rhValL = SplFlag2(0)
                                        If SplFlag3.Length > 0 Then
                                            rhIdH = SplFlag3(1)
                                            rhValH = SplFlag3(0)

                                            dvBar = Dts.Tables(0).DefaultView
                                            Try
                                                Dim lowRh As String = ""
                                                Dim highRh As String = ""
                                                Dim cngCounter As Integer = 0


                                                Try
                                                    'Getting lower RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdL
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                        lowRh = "10000"
                                                        If Not OTRMsgB.Contains(MatDes(i)) Then
                                                            OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                        End If
                                                        cngCounter = 1
                                                    Else
                                                        lowRh = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                    End If



                                                    'Getting higher RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdH
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                        highRh = "10000"
                                                        If cngCounter <> 1 Then
                                                            If Not OTRMsgB.Contains(MatDes(i)) Then
                                                                OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                            End If
                                                            cngCounter = 1
                                                        End If
                                                    Else
                                                        highRh = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                    End If

                                                    OTRB(i) = CDbl(lowRh) + ((OtrRH - rhValL) / (rhValH - rhValL) * (CDbl(highRh) - CDbl(lowRh)))
                                                    If ISADJTHICK(i) <> "N" Then
                                                        'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                                                        OTRB(i) = CDbl((OTRB(i) / CDbl(Thick(k, i))) * 25.4)
                                                    End If
                                                Catch ex As Exception
                                                    OTRB(i) = "0"
                                                End Try

                                            Catch ex As Exception

                                            End Try

                                        End If

                                    End If

                                End If
                            ElseIf OTRFlagVal = "4" Then
                                Dim rhId, tempIdL, tempIdH, tempValL, tempValH As String
                                Dim SplFlag1() As String
                                Dim SplFlag2() As String
                                Dim SplFlag3() As String

                                SplFlag1 = Regex.Split(TempRhVal, "##")
                                If SplFlag1.Length > 0 Then
                                    rhId = SplFlag1(0)
                                    SplFlag2 = Regex.Split(SplFlag1(1), "#")
                                    SplFlag3 = Regex.Split(SplFlag1(2), "#")
                                    If SplFlag2.Length > 0 Then
                                        tempIdL = SplFlag2(1)
                                        tempValL = SplFlag2(0)
                                        If SplFlag3.Length > 0 Then
                                            tempIdH = SplFlag3(1)
                                            tempValH = SplFlag3(0)

                                            dvBar = Dts.Tables(0).DefaultView
                                            Try
                                                Dim lowTemp As String = ""
                                                Dim highTemp As String = ""
                                                Dim cngCounter As Integer = 0

                                                Try
                                                    'Getting lower RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhId
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                        lowTemp = "10000"
                                                        If Not OTRMsgB.Contains(MatDes(i)) Then
                                                            OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                        End If
                                                        cngCounter = 1
                                                    Else
                                                        lowTemp = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                    End If

                                                    'Getting higher RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhId
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                        highTemp = "10000"
                                                        If cngCounter <> 1 Then
                                                            If Not OTRMsgB.Contains(MatDes(i)) Then
                                                                OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                            End If
                                                            cngCounter = 1
                                                        End If
                                                    Else
                                                        highTemp = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                    End If
                                                    OTRB(i) = CDbl(lowTemp) + ((OtrTemp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))
                                                    If ISADJTHICK(i) <> "N" Then
                                                        'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                                                        OTRB(i) = CDbl((OTRB(i) / CDbl(Thick(k, i))) * 25.4)
                                                    End If
                                                Catch ex As Exception
                                                    OTRB(i) = "0"
                                                End Try

                                            Catch ex As Exception

                                            End Try

                                        End If

                                    End If

                                End If
                            ElseIf OTRFlagVal = "2" Then
                                Dim tempIdL, tempIdH, tempValL, tempValH As String
                                Dim rhIdL, rhIdH, rhValL, rhValH As String


                                Dim SplFlag1() As String
                                Dim SplFlag2() As String
                                Dim SplFlag3() As String

                                Dim SplFlag4() As String
                                Dim SplFlag5() As String

                                SplFlag1 = Regex.Split(TempRhVal, "##")
                                If SplFlag1.Length > 0 Then

                                    SplFlag2 = Regex.Split(SplFlag1(0), "#")
                                    SplFlag3 = Regex.Split(SplFlag1(1), "#")

                                    SplFlag4 = Regex.Split(SplFlag1(2), "#")
                                    SplFlag5 = Regex.Split(SplFlag1(3), "#")

                                    If SplFlag2.Length > 0 Then
                                        tempIdL = SplFlag2(1)
                                        tempValL = SplFlag2(0)
                                        If SplFlag3.Length > 0 Then
                                            tempIdH = SplFlag3(1)
                                            tempValH = SplFlag3(0)
                                            If SplFlag4.Length > 0 Then
                                                rhIdL = SplFlag4(1)
                                                rhValL = SplFlag4(0)
                                                If SplFlag5.Length > 0 Then
                                                    rhIdH = SplFlag5(1)
                                                    rhValH = SplFlag5(0)

                                                    dvBar = Dts.Tables(0).DefaultView
                                                    Try
                                                        Dim tempLrhL, tempHrhH, tempLrhH, tempHrhL As String
                                                        Dim MidTemp1 As String
                                                        Dim MidTemp2 As String
                                                        Dim cngCounter As Integer = 0

                                                        Try
                                                            'Getting lower temp lower Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdL
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                                tempLrhL = "10000"
                                                                If Not OTRMsgB.Contains(MatDes(i)) Then
                                                                    OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                                End If
                                                                cngCounter = 1
                                                            Else
                                                                tempLrhL = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                            End If


                                                            'Getting higher temp higher Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdH
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                                tempHrhH = "10000"
                                                                If cngCounter <> 1 Then
                                                                    If Not OTRMsgB.Contains(MatDes(i)) Then
                                                                        OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                                    End If
                                                                    cngCounter = 1
                                                                End If
                                                            Else
                                                                tempHrhH = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                            End If


                                                            'Getting lower temp Higher Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdH
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                                tempLrhH = "10000"
                                                                If cngCounter <> 1 Then
                                                                    If Not OTRMsgB.Contains(MatDes(i)) Then
                                                                        OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                                    End If
                                                                    cngCounter = 1
                                                                End If
                                                            Else
                                                                tempLrhH = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                            End If


                                                            'Getting higher temp lower Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdL
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                                tempHrhL = "10000"
                                                                If cngCounter <> 1 Then
                                                                    If Not OTRMsgB.Contains(MatDes(i)) Then
                                                                        OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                                    End If
                                                                    cngCounter = 1
                                                                End If
                                                            Else
                                                                tempHrhL = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                            End If

                                                            MidTemp1 = CDbl(tempLrhL) + (CDbl(OtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhL) - CDbl(CDbl(tempLrhL)))))
                                                            MidTemp2 = CDbl(tempLrhH) + (CDbl(OtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhH) - CDbl(CDbl(tempLrhH)))))
                                                            OTRB(i) = MidTemp1 + ((OtrRH - rhValL) / (rhValH - rhValL) * (MidTemp2 - MidTemp1))
                                                            If ISADJTHICK(i) <> "N" Then
                                                                'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                                                                OTRB(i) = CDbl((OTRB(i) / CDbl(Thick(k, i))) * 25.4)
                                                            End If
                                                        Catch ex As Exception
                                                            OTRB(i) = "0"
                                                        End Try

                                                    Catch ex As Exception

                                                    End Try

                                                End If
                                            End If
                                        End If

                                    End If

                                End If
                            End If
                        End If
                    Next
                    '##############################################################################################################
                    '##############################################################################################################
                    'Getting WVTR Flag and Values
                    WVTRFlag = GetWVTRFlag(WvtrTemp, WvtrRH)
                    strSpl = Regex.Split(WVTRFlag, "-")
                    If strSpl.Length > 0 Then
                        WVTRFlagVal = strSpl(0)
                        TempRhVal = strSpl(1)
                    End If
                    For i = 0 To 9
                        If MAT(i) > 500 And MAT(i) < 506 Then
                            WVTRB(i) = WVTR(i)
                        Else
                            If WVTRFlagVal = "1" Then
                                Dim tempId, rhId As String
                                Dim SplFlag1() As String
                                SplFlag1 = Regex.Split(TempRhVal, "##")
                                If SplFlag1.Length > 0 Then
                                    tempId = SplFlag1(0)
                                    rhId = SplFlag1(1)

                                    dvBar = Dts.Tables(0).DefaultView
                                    Try
                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhId
                                        dtBar = dvBar.ToTable()
                                        If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                            WVTRB(i) = "10000"
                                            If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                            End If
                                        Else
                                            WVTRB(i) = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                        End If
                                        If ISADJTHICK(i) <> "N" Then
                                            'WVTRB(i) = CDbl(WVTRB(i)) / CDbl(Thick(i))
                                            WVTRB(i) = CDbl((WVTRB(i) / CDbl(Thick(k, i))) * 25.4)
                                        End If
                                    Catch ex As Exception
                                        WVTRB(i) = "0"

                                    End Try

                                End If
                            ElseIf WVTRFlagVal = "3" Then
                                Dim tempId, rhIdL, rhIdH, rhValL, rhValH As String
                                Dim SplFlag1() As String
                                Dim SplFlag2() As String
                                Dim SplFlag3() As String

                                SplFlag1 = Regex.Split(TempRhVal, "##")
                                If SplFlag1.Length > 0 Then
                                    tempId = SplFlag1(0)
                                    SplFlag2 = Regex.Split(SplFlag1(1), "#")
                                    SplFlag3 = Regex.Split(SplFlag1(2), "#")
                                    If SplFlag2.Length > 0 Then
                                        rhIdL = SplFlag2(1)
                                        rhValL = SplFlag2(0)
                                        If SplFlag3.Length > 0 Then
                                            rhIdH = SplFlag3(1)
                                            rhValH = SplFlag3(0)

                                            dvBar = Dts.Tables(0).DefaultView
                                            Try
                                                Dim lowRh As String = ""
                                                Dim highRh As String = ""
                                                Dim cngCounter As Integer = 0
                                                Try
                                                    'Getting lower RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdL
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                        lowRh = "10000"
                                                        If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                            WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                        End If
                                                        cngCounter = 1
                                                    Else
                                                        lowRh = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                    End If

                                                    'Getting higher RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdH
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                        highRh = "10000"
                                                        If cngCounter <> 1 Then
                                                            If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                                WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                            End If
                                                            cngCounter = 1
                                                        End If
                                                    Else
                                                        highRh = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                    End If
                                                    WVTRB(i) = CDbl(lowRh) + ((WvtrRH - rhValL) / (rhValH - rhValL) * (CDbl(highRh) - CDbl(lowRh)))
                                                    If ISADJTHICK(i) <> "N" Then
                                                        'WVTRB(i) = CDbl(WVTRB(i)) / CDbl(Thick(i))
                                                        WVTRB(i) = CDbl((WVTRB(i) / CDbl(Thick(k, i))) * 25.4)
                                                    End If
                                                Catch ex As Exception
                                                    WVTRB(i) = "0"
                                                End Try
                                            Catch ex As Exception
                                            End Try

                                        End If

                                    End If

                                End If
                            ElseIf WVTRFlagVal = "4" Then
                                Dim rhId, tempIdL, tempIdH, tempValL, tempValH As String
                                Dim SplFlag1() As String
                                Dim SplFlag2() As String
                                Dim SplFlag3() As String

                                SplFlag1 = Regex.Split(TempRhVal, "##")
                                If SplFlag1.Length > 0 Then
                                    rhId = SplFlag1(0)
                                    SplFlag2 = Regex.Split(SplFlag1(1), "#")
                                    SplFlag3 = Regex.Split(SplFlag1(2), "#")
                                    If SplFlag2.Length > 0 Then
                                        tempIdL = SplFlag2(1)
                                        tempValL = SplFlag2(0)
                                        If SplFlag3.Length > 0 Then
                                            tempIdH = SplFlag3(1)
                                            tempValH = SplFlag3(0)

                                            dvBar = Dts.Tables(0).DefaultView
                                            Try
                                                Dim lowTemp As String = ""
                                                Dim highTemp As String = ""
                                                Dim cngCounter As Integer = 0
                                                Try
                                                    'Getting lower RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhId
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                        lowTemp = "10000"
                                                        If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                            WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                        End If
                                                        cngCounter = 1
                                                    Else
                                                        lowTemp = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                    End If


                                                    'Getting higher RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhId
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                        highTemp = "10000"
                                                        If cngCounter <> 1 Then
                                                            If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                                WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                            End If
                                                            cngCounter = 1
                                                        End If
                                                    Else
                                                        highTemp = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                    End If

                                                    WVTRB(i) = CDbl(lowTemp) + ((WvtrTemp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))
                                                    If ISADJTHICK(i) <> "N" Then
                                                        'WVTRB(i) = CDbl(WVTRB(i)) / CDbl(Thick(i))
                                                        WVTRB(i) = CDbl((WVTRB(i) / CDbl(Thick(k, i))) * 25.4)
                                                    End If
                                                Catch ex As Exception
                                                    WVTRB(i) = "0"
                                                End Try
                                            Catch ex As Exception

                                            End Try

                                        End If

                                    End If

                                End If
                            ElseIf WVTRFlagVal = "2" Then
                                Dim tempIdL, tempIdH, tempValL, tempValH As String
                                Dim rhIdL, rhIdH, rhValL, rhValH As String


                                Dim SplFlag1() As String
                                Dim SplFlag2() As String
                                Dim SplFlag3() As String

                                Dim SplFlag4() As String
                                Dim SplFlag5() As String

                                SplFlag1 = Regex.Split(TempRhVal, "##")
                                If SplFlag1.Length > 0 Then

                                    SplFlag2 = Regex.Split(SplFlag1(0), "#")
                                    SplFlag3 = Regex.Split(SplFlag1(1), "#")

                                    SplFlag4 = Regex.Split(SplFlag1(2), "#")
                                    SplFlag5 = Regex.Split(SplFlag1(3), "#")

                                    If SplFlag2.Length > 0 Then
                                        tempIdL = SplFlag2(1)
                                        tempValL = SplFlag2(0)
                                        If SplFlag3.Length > 0 Then
                                            tempIdH = SplFlag3(1)
                                            tempValH = SplFlag3(0)
                                            If SplFlag4.Length > 0 Then
                                                rhIdL = SplFlag4(1)
                                                rhValL = SplFlag4(0)
                                                If SplFlag5.Length > 0 Then
                                                    rhIdH = SplFlag5(1)
                                                    rhValH = SplFlag5(0)

                                                    dvBar = Dts.Tables(0).DefaultView
                                                    Try
                                                        Dim tempLrhL, tempHrhH, tempLrhH, tempHrhL As String
                                                        Dim MidTemp1 As String
                                                        Dim MidTemp2 As String
                                                        Dim cngCounter As Integer = 0
                                                        Try
                                                            'Getting lower temp lower Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdL
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                                tempLrhL = "10000"
                                                                If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                                    WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                                End If
                                                                cngCounter = 1
                                                            Else
                                                                tempLrhL = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                            End If


                                                            'Getting higher temp higher Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdH
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                                tempHrhH = "10000"
                                                                If cngCounter <> 1 Then
                                                                    If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                                        WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                                    End If
                                                                    cngCounter = 1
                                                                End If
                                                            Else
                                                                tempHrhH = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                            End If


                                                            'Getting lower temp Higher Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdH
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                                tempLrhH = "10000"
                                                                If cngCounter <> 1 Then
                                                                    If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                                        WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                                    End If
                                                                    cngCounter = 1
                                                                End If
                                                            Else
                                                                tempLrhH = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                            End If


                                                            'Getting higher temp lower Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdL
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                                tempHrhL = "10000"
                                                                If cngCounter <> 1 Then
                                                                    If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                                        WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                                    End If
                                                                    cngCounter = 1
                                                                End If
                                                            Else
                                                                tempHrhL = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                            End If


                                                            MidTemp1 = CDbl(tempLrhL) + (CDbl(WvtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhL) - CDbl(CDbl(tempLrhL)))))
                                                            MidTemp2 = CDbl(tempLrhH) + (CDbl(WvtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhH) - CDbl(CDbl(tempLrhH)))))
                                                            WVTRB(i) = MidTemp1 + ((WvtrRH - rhValL) / (rhValH - rhValL) * (MidTemp2 - MidTemp1))
                                                            If ISADJTHICK(i) <> "N" Then
                                                                'WVTRB(i) = CDbl(WVTRB(i)) / CDbl(Thick(i))
                                                                WVTRB(i) = CDbl((WVTRB(i) / CDbl(Thick(k, i))) * 25.4)
                                                            End If
                                                        Catch ex As Exception
                                                            WVTRB(i) = "0"
                                                        End Try

                                                    Catch ex As Exception

                                                    End Try

                                                End If
                                            End If
                                        End If

                                    End If

                                End If
                            End If
                        End If
                    Next

                    '######################################################################################################################
                    'Getting Tensile strength Flag and Values
                    Dim TSFlag As String
                    Dim TSFlagVal As String = ""
                    TSFlag = GetTSFlag("25", "100")
                    Dim strTSpl() As String
                    strTSpl = Regex.Split(TSFlag, "-")
                    If strTSpl.Length > 0 Then
                        TSFlagVal = strTSpl(0)
                        TempRhVal = strTSpl(1)
                    End If

                    For i = 0 To 9
                        If MAT(i) > 500 And MAT(i) < 506 Then
                            TS1B(i) = TS1(i)
                            TS2B(i) = TS2(i)
                        Else
                            If TSFlagVal = "1" Then
                                Dim tempId, rhId As String
                                Dim SplFlag1() As String
                                SplFlag1 = Regex.Split(TempRhVal, "##")
                                If SplFlag1.Length > 0 Then
                                    tempId = SplFlag1(0)
                                    rhId = SplFlag1(1)

                                    dvBar = Dts.Tables(0).DefaultView
                                    Try
                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhId
                                        dtBar = dvBar.ToTable()
                                        Try
                                            If dtBar.Rows(0).Item("MDBREAK").ToString() = "" Then
                                                TS1B(i) = ""
                                                If Not TS1msgB.Contains(MatDes(i)) Then
                                                    TS1msgB = TS1msgB + "" + MatDes(i) + " (TS1)\n"
                                                End If
                                            Else
                                                TS1B(i) = dtBar.Rows(0).Item("MDBREAK").ToString()
                                            End If
                                        Catch ex As Exception
                                            TS1B(i) = "0"
                                        End Try

                                        Try
                                            If dtBar.Rows(0).Item("TDBREAK").ToString() = "" Then
                                                TS2B(i) = ""
                                                If Not TS2msgB.Contains(MatDes(i)) Then
                                                    TS2msgB = TS2msgB + "" + MatDes(i) + " (TS2)\n"
                                                End If
                                            Else
                                                TS2B(i) = dtBar.Rows(0).Item("TDBREAK").ToString()
                                            End If
                                        Catch ex As Exception
                                            TS2B(i) = "0"
                                        End Try

                                    Catch ex As Exception
                                    End Try

                                End If
                            End If
                        End If
                    Next
                    'TotalOTR = "0"
                    For i = 0 To 9
                        If MAT(i) <> "0" Then
                            If OTRB(i) <> 0 Then
                                If Not IsNumeric(OtrPref(i)) Then
                                    TotalOTRB = CDbl(TotalOTRB) + (1 / CDbl(OTRB(i)))
                                Else
                                    'If (Ds.Tables(0).Rows(0).Item("OTR" + (i + 1).ToString())) <> 0 Then
                                    TotalOTRB = CDbl(TotalOTRB) + (1 / OtrPref(i))
                                    'End If
                                End If
                            Else
                                If IsNumeric(OtrPref(i)) Then
                                    TotalOTRB = CDbl(TotalOTRB) + (1 / OtrPref(i))
                                Else
                                    TotalOTRB = CDbl(TotalOTRB) + (1 / CDbl(OTRB(i)))
                                End If

                            End If
                        End If
                    Next
                    If TotalOTRB <> "0" Then
                        TotalOTRB = 1 / CDbl(TotalOTRB)
                    End If


                    'TotalWVTR 
                    For i = 0 To 9
                        If MAT(i) <> 0 Then
                            If WVTRB(i) <> 0 Then
                                If Not IsNumeric(WVTRPref(i)) Then
                                    TotalWVTRB = CDbl(TotalWVTRB) + (1 / CDbl(WVTRB(i)))
                                Else
                                    'If (Ds.Tables(0).Rows(0).Item("WVTR" + (i + 1).ToString())) <> 0 Then
                                    TotalWVTRB = CDbl(TotalWVTRB) + (1 / CDbl(WVTRPref(i)))
                                    'End If

                                End If
                            Else
                                If IsNumeric(WVTRPref(i)) Then
                                    'If (Ds.Tables(0).Rows(0).Item("WVTR" + (i + 1).ToString())) <> 0 Then
                                    TotalWVTRB = CDbl(TotalWVTRB) + (1 / CDbl(WVTRPref(i)))
                                Else
                                    TotalWVTRB = CDbl(TotalWVTRB) + (1 / CDbl(WVTRB(i)))
                                    'End If
                                End If
                            End If
                        End If
                    Next
                    If TotalWVTRB <> "0" Then
                        TotalWVTRB = 1 / CDbl(TotalWVTRB)
                    End If

                    'Total Tensile Strength 1
                    For i = 0 To 9
                        If MAT(i) <> "0" Then
                            If TS1B(i) <> 0 Then
                                If Not IsNumeric(TS1Pref(i)) Then
                                    TotalTS1B = CDbl(TotalTS1B) + (CDbl(TS1B(i)) * CDbl(MatPer(i)))
                                Else
                                    TotalTS1B = CDbl(TotalTS1B) + (TS1Pref(i) * CDbl(MatPer(i)))
                                End If
                            Else
                                If IsNumeric(TS1Pref(i)) Then
                                    TotalTS1B = CDbl(TotalTS1B) + (TS1Pref(i) * CDbl(MatPer(i)))
                                Else
                                    If TS1(i) <> "" Then
                                        TotalTS1B = CDbl(TotalTS1B) + (CDbl(TS1(i)) * CDbl(MatPer(i)))
                                    End If
                                End If
                            End If
                        End If
                    Next

                    'Total Tensile Strength 2
                    For i = 0 To 9
                        If MAT(i) <> "0" Then
                            If TS2B(i) <> "" Then
                                If Not IsNumeric(TS2Pref(i)) Then
                                    TotalTS2B = CDbl(TotalTS2B) + (CDbl(TS2B(i)) * CDbl(MatPer(i)))

                                Else
                                    TotalTS2B = CDbl(TotalTS2B) + (TS2Pref(i) * CDbl(MatPer(i)))
                                End If
                            Else
                                If IsNumeric(TS2Pref(i)) Then
                                    TotalTS2B = CDbl(TotalTS2B) + (TS2Pref(i) * CDbl(MatPer(i)))
                                Else
                                    If TS2(i) <> "" Then
                                        TotalTS2B = CDbl(TotalTS2B) + (CDbl(TS2(i)) * CDbl(MatPer(i)))
                                    End If
                                End If
                            End If
                        End If
                    Next

                    If OTRMsgB <> "" Or WVTRmsgB <> "" Or TS1msgB <> "" Or TS2msgB <> "" Then
                        MsgB = "Data is not present for following materials for Date " + CDate(EDate).Date + "\n"
                        If OTRMsgB <> "" Then
                            MsgB = MsgB + OTRMsgB + ""
                        End If
                        If WVTRmsgB <> "" Then
                            MsgB = MsgB + WVTRmsgB + ""
                        End If
                        If TS1msgB <> "" Then
                            MsgB = MsgB + TS1msgB + ""
                        End If
                        If TS2msgB <> "" Then
                            MsgB = MsgB + TS2msgB + ""
                        End If
                    End If

                End If
            Catch ex As Exception

            End Try
        End Sub

        Public Sub GetWVTRValue1(ByVal Grades() As String, ByVal WVTRTemp As String, ByVal WVTRRh As String, ByVal Dts As DataSet)
            Try
                Dim StrSql As String = String.Empty
                Dim StrSql1 As String = String.Empty
                Dim dvBarr As New DataView
                Dim dtBar As New DataTable
                Dim odbUtil As New DBUtil()
                Dim WVTRFlag As String
                Dim dvBar As New DataView
                Dim dsBar As New DataSet()
                Dim WVTRFlagVal As String = ""
                Dim TempRhVal As String = ""

                'Getting OTR Flag and Values
                WVTRFlag = GetWVTRFlag(WVTRTemp, WVTRRh)
                Dim strSpl() As String
                strSpl = Regex.Split(WVTRFlag, "-")
                If strSpl.Length > 0 Then
                    WVTRFlagVal = strSpl(0)
                    TempRhVal = strSpl(1)
                End If


                If WVTRFlagVal = "1" Then
                    Dim tempId, rhId As String
                    Dim SplFlag1() As String
                    SplFlag1 = Regex.Split(TempRhVal, "##")
                    If SplFlag1.Length > 0 Then
                        tempId = SplFlag1(0)
                        rhId = SplFlag1(1)
                        For i = 0 To 9
                            dvBar = Dts.Tables(0).DefaultView
                            Try
                                dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhId
                                dtBar = dvBar.ToTable()
                                WVTRB(i) = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                            Catch ex As Exception
                                WVTRB(i) = "0"

                            End Try
                        Next
                    End If
                ElseIf WVTRFlagVal = "3" Then
                    Dim tempId, rhIdL, rhIdH, rhValL, rhValH As String
                    Dim SplFlag1() As String
                    Dim SplFlag2() As String
                    Dim SplFlag3() As String

                    SplFlag1 = Regex.Split(TempRhVal, "##")
                    If SplFlag1.Length > 0 Then
                        tempId = SplFlag1(0)
                        SplFlag2 = Regex.Split(SplFlag1(1), "#")
                        SplFlag3 = Regex.Split(SplFlag1(2), "#")
                        If SplFlag2.Length > 0 Then
                            rhIdL = SplFlag2(1)
                            rhValL = SplFlag2(0)
                            If SplFlag3.Length > 0 Then
                                rhIdH = SplFlag3(1)
                                rhValH = SplFlag3(0)
                                For i = 0 To 9
                                    dvBar = Dts.Tables(0).DefaultView
                                    Try
                                        Dim lowRh As String = ""
                                        Dim highRh As String = ""



                                        Try
                                            'Getting lower RH
                                            dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdL
                                            dtBar = dvBar.ToTable()
                                            lowRh = dtBar.Rows(0).Item("WVTRVALUE").ToString()


                                            'Getting higher RH
                                            dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdH
                                            dtBar = dvBar.ToTable()
                                            highRh = dtBar.Rows(0).Item("WVTRVALUE").ToString()


                                            WVTRB(i) = CDbl(lowRh) + ((WVTRRh - rhValL) / (rhValH - rhValL) * (CDbl(highRh) - CDbl(lowRh)))

                                        Catch ex As Exception
                                            WVTRB(i) = "0"
                                        End Try
                                    Catch ex As Exception
                                    End Try
                                Next
                            End If

                        End If

                    End If
                ElseIf WVTRFlagVal = "4" Then
                    Dim rhId, tempIdL, tempIdH, tempValL, tempValH As String
                    Dim SplFlag1() As String
                    Dim SplFlag2() As String
                    Dim SplFlag3() As String

                    SplFlag1 = Regex.Split(TempRhVal, "##")
                    If SplFlag1.Length > 0 Then
                        rhId = SplFlag1(0)
                        SplFlag2 = Regex.Split(SplFlag1(1), "#")
                        SplFlag3 = Regex.Split(SplFlag1(2), "#")
                        If SplFlag2.Length > 0 Then
                            tempIdL = SplFlag2(1)
                            tempValL = SplFlag2(0)
                            If SplFlag3.Length > 0 Then
                                tempIdH = SplFlag3(1)
                                tempValH = SplFlag3(0)
                                For i = 0 To 9
                                    dvBar = Dts.Tables(0).DefaultView
                                    Try
                                        Dim lowTemp As String = ""
                                        Dim highTemp As String = ""

                                        Try
                                            'Getting lower RH
                                            dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhId
                                            dtBar = dvBar.ToTable()
                                            lowTemp = dtBar.Rows(0).Item("WVTRVALUE").ToString()

                                            'Getting higher RH
                                            dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhId
                                            dtBar = dvBar.ToTable()
                                            highTemp = dtBar.Rows(0).Item("WVTRVALUE").ToString()


                                            WVTRB(i) = CDbl(lowTemp) + ((WVTRTemp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))
                                        Catch ex As Exception
                                            WVTRB(i) = "0"
                                        End Try
                                    Catch ex As Exception

                                    End Try
                                Next
                            End If

                        End If

                    End If
                ElseIf WVTRFlagVal = "2" Then
                    Dim tempIdL, tempIdH, tempValL, tempValH As String
                    Dim rhIdL, rhIdH, rhValL, rhValH As String


                    Dim SplFlag1() As String
                    Dim SplFlag2() As String
                    Dim SplFlag3() As String

                    Dim SplFlag4() As String
                    Dim SplFlag5() As String

                    SplFlag1 = Regex.Split(TempRhVal, "##")
                    If SplFlag1.Length > 0 Then

                        SplFlag2 = Regex.Split(SplFlag1(0), "#")
                        SplFlag3 = Regex.Split(SplFlag1(1), "#")

                        SplFlag4 = Regex.Split(SplFlag1(2), "#")
                        SplFlag5 = Regex.Split(SplFlag1(3), "#")

                        If SplFlag2.Length > 0 Then
                            tempIdL = SplFlag2(1)
                            tempValL = SplFlag2(0)
                            If SplFlag3.Length > 0 Then
                                tempIdH = SplFlag3(1)
                                tempValH = SplFlag3(0)
                                If SplFlag4.Length > 0 Then
                                    rhIdL = SplFlag4(1)
                                    rhValL = SplFlag4(0)
                                    If SplFlag5.Length > 0 Then
                                        rhIdH = SplFlag5(1)
                                        rhValH = SplFlag5(0)
                                        For i = 0 To 9
                                            dvBar = Dts.Tables(0).DefaultView
                                            Try
                                                Dim tempLrhL, tempHrhH, tempLrhH, tempHrhL As String
                                                Dim MidTemp1 As String
                                                Dim MidTemp2 As String

                                                Try
                                                    'Getting lower temp lower Rh
                                                    dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdL
                                                    dtBar = dvBar.ToTable()
                                                    tempLrhL = dtBar.Rows(0).Item("WVTRVALUE").ToString()

                                                    'Getting higher temp higher Rh
                                                    dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdH
                                                    dtBar = dvBar.ToTable()
                                                    tempHrhH = dtBar.Rows(0).Item("WVTRVALUE").ToString()

                                                    'Getting lower temp Higher Rh
                                                    dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdH
                                                    dtBar = dvBar.ToTable()
                                                    tempLrhH = dtBar.Rows(0).Item("WVTRVALUE").ToString()

                                                    'Getting higher temp lower Rh
                                                    dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdL
                                                    dtBar = dvBar.ToTable()
                                                    tempHrhL = dtBar.Rows(0).Item("WVTRVALUE").ToString()

                                                    MidTemp1 = CDbl(tempLrhL) + (CDbl(WVTRTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhL) - CDbl(CDbl(tempLrhL)))))
                                                    MidTemp2 = CDbl(tempLrhH) + (CDbl(WVTRTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhH) - CDbl(CDbl(tempLrhH)))))
                                                    WVTRB(i) = MidTemp1 + ((WVTRRh - rhValL) / (rhValH - rhValL) * (MidTemp2 - MidTemp1))

                                                Catch ex As Exception
                                                    WVTRB(i) = "0"
                                                End Try

                                            Catch ex As Exception

                                            End Try
                                        Next
                                    End If
                                End If
                            End If

                        End If

                    End If

                End If
            Catch ex As Exception

            End Try
        End Sub
        Public Function GetOTRFlag(ByVal Temp As String, ByVal RH As String) As String
            Dim OTRFlag As String
            Dim Dts1 As New DataSet()
            Dim Dts2 As New DataSet()
            Dim dsTemp As New DataSet()
            Dim dsRH As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim SBAConnection As String = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
            Dim StrSql1 As String = String.Empty
            OTRFlag = "0"
            Try
                StrSql1 = "select * from BARRIERTEMP "
                StrSql1 = StrSql1 + "where(TEMPVAL = " + Temp + ") "
                Dts1 = odbUtil.FillDataSet(StrSql1, SBAConnection)
                StrSql1 = String.Empty
                StrSql1 = "select * from BARRIERH "
                StrSql1 = StrSql1 + "where(RHVALUE = " + RH + ") "
                Dts2 = odbUtil.FillDataSet(StrSql1, SBAConnection)
                If Dts1.Tables(0).Rows.Count > 0 And Dts2.Tables(0).Rows.Count > 0 Then
                    OTRFlag = "1" + "-" + Dts1.Tables(0).Rows(0).Item("TEMPID").ToString() + "##" + Dts2.Tables(0).Rows(0).Item("RHID").ToString()
                ElseIf Dts1.Tables(0).Rows.Count = 0 And Dts2.Tables(0).Rows.Count = 0 Then
                    OTRFlag = "2"
                    dsTemp = GetBarrierHighLowTemp(Temp)
                    dsRH = GetBarrierHighLowHumidity(RH)
                    OTRFlag = "2" + "-" + dsTemp.Tables(0).Rows(0).Item("TEMPVALUE").ToString() + "##" + dsTemp.Tables(0).Rows(1).Item("TEMPVALUE").ToString() _
                    + "##" + dsRH.Tables(0).Rows(0).Item("RHVALUE").ToString() + "##" + dsRH.Tables(0).Rows(1).Item("RHVALUE").ToString()

                ElseIf Dts1.Tables(0).Rows.Count > 0 And Dts2.Tables(0).Rows.Count = 0 Then
                    dsRH = GetBarrierHighLowHumidity(RH)
                    OTRFlag = "3" + "-" + Dts1.Tables(0).Rows(0).Item("TEMPID").ToString() + "##" + dsRH.Tables(0).Rows(0).Item("RHVALUE").ToString() + "##" + dsRH.Tables(0).Rows(1).Item("RHVALUE").ToString()
                    'flag-tempid##MIN(RHUVALUE#RHID)##MAX(RHUVALUE#RHID)
                ElseIf Dts1.Tables(0).Rows.Count = 0 And Dts2.Tables(0).Rows.Count > 0 Then
                    dsRH = GetBarrierHighLowTemp(Temp)
                    OTRFlag = "4" + "-" + Dts2.Tables(0).Rows(0).Item("RHID").ToString() + "##" + dsRH.Tables(0).Rows(0).Item("TEMPVALUE").ToString() + "##" + dsRH.Tables(0).Rows(1).Item("TEMPVALUE").ToString()
                    'flag-tempid##MIN(RHUVALUE#RHID)##MAX(RHUVALUE#RHID)
                End If
                Return OTRFlag
            Catch ex As Exception
                Return OTRFlag
            End Try
        End Function
        Public Function GetWVTRFlag(ByVal Temp As String, ByVal RH As String) As String
            Dim WVTRFlag As String
            Dim Dts1 As New DataSet()
            Dim Dts2 As New DataSet()
            Dim dsTemp As New DataSet()
            Dim dsRH As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim SBAConnection As String = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
            Dim StrSql1 As String = String.Empty
            WVTRFlag = "0"
            Try
                StrSql1 = "select * from BARRIERTEMP "
                StrSql1 = StrSql1 + "where(TEMPVAL = " + Temp + ") "
                Dts1 = odbUtil.FillDataSet(StrSql1, SBAConnection)
                StrSql1 = String.Empty
                StrSql1 = "select * from BARRIERH "
                StrSql1 = StrSql1 + "where(RHVALUE = " + RH + ") "
                Dts2 = odbUtil.FillDataSet(StrSql1, SBAConnection)
                If Dts1.Tables(0).Rows.Count > 0 And Dts2.Tables(0).Rows.Count > 0 Then
                    WVTRFlag = "1" + "-" + Dts1.Tables(0).Rows(0).Item("TEMPID").ToString() + "##" + Dts2.Tables(0).Rows(0).Item("RHID").ToString()
                ElseIf Dts1.Tables(0).Rows.Count = 0 And Dts2.Tables(0).Rows.Count = 0 Then

                    dsTemp = GetBarrierHighLowTemp(Temp)
                    dsRH = GetBarrierHighLowHumidity(RH)
                    WVTRFlag = "2" + "-" + dsTemp.Tables(0).Rows(0).Item("TEMPVALUE").ToString() + "##" + dsTemp.Tables(0).Rows(1).Item("TEMPVALUE").ToString() _
                    + "##" + dsRH.Tables(0).Rows(0).Item("RHVALUE").ToString() + "##" + dsRH.Tables(0).Rows(1).Item("RHVALUE").ToString()

                ElseIf Dts1.Tables(0).Rows.Count > 0 And Dts2.Tables(0).Rows.Count = 0 Then
                    dsRH = GetBarrierHighLowHumidity(RH)
                    WVTRFlag = "3" + "-" + Dts1.Tables(0).Rows(0).Item("TEMPID").ToString() + "##" + dsRH.Tables(0).Rows(0).Item("RHVALUE").ToString() + "##" + dsRH.Tables(0).Rows(1).Item("RHVALUE").ToString()
                    'flag-tempid##MIN(RHUVALUE#RHID)##MAX(RHUVALUE#RHID)
                ElseIf Dts1.Tables(0).Rows.Count = 0 And Dts2.Tables(0).Rows.Count > 0 Then
                    dsRH = GetBarrierHighLowTemp(Temp)
                    WVTRFlag = "4" + "-" + Dts2.Tables(0).Rows(0).Item("RHID").ToString() + "##" + dsRH.Tables(0).Rows(0).Item("TEMPVALUE").ToString() + "##" + dsRH.Tables(0).Rows(1).Item("TEMPVALUE").ToString()
                    'flag-tempid##MIN(RHUVALUE#RHID)##MAX(RHUVALUE#RHID)
                End If
                Return WVTRFlag
            Catch ex As Exception
                Return WVTRFlag
            End Try
        End Function

        Public Function GetTSFlag(ByVal Temp As String, ByVal RH As String) As String
            Dim TSFlag As String
            Dim Dts1 As New DataSet()
            Dim Dts2 As New DataSet()
            Dim dsTemp As New DataSet()
            Dim dsRH As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim SBAConnection As String = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
            Dim StrSql1 As String = String.Empty
            TSFlag = "0"
            Try
                StrSql1 = "select * from BARRIERTEMP "
                StrSql1 = StrSql1 + "where(TEMPVAL = " + Temp + ") "
                Dts1 = odbUtil.FillDataSet(StrSql1, SBAConnection)
                StrSql1 = String.Empty
                StrSql1 = "select * from BARRIERH "
                StrSql1 = StrSql1 + "where(RHVALUE = " + RH + ") "
                Dts2 = odbUtil.FillDataSet(StrSql1, SBAConnection)
                If Dts1.Tables(0).Rows.Count > 0 And Dts2.Tables(0).Rows.Count > 0 Then
                    TSFlag = "1" + "-" + Dts1.Tables(0).Rows(0).Item("TEMPID").ToString() + "##" + Dts2.Tables(0).Rows(0).Item("RHID").ToString()
                ElseIf Dts1.Tables(0).Rows.Count = 0 And Dts2.Tables(0).Rows.Count = 0 Then
                    TSFlag = "2"
                    dsTemp = GetBarrierHighLowTemp(Temp)
                    dsRH = GetBarrierHighLowHumidity(RH)
                    TSFlag = "2" + "-" + dsTemp.Tables(0).Rows(0).Item("TEMPVALUE").ToString() + "##" + dsTemp.Tables(0).Rows(1).Item("TEMPVALUE").ToString() _
                    + "##" + dsRH.Tables(0).Rows(0).Item("RHVALUE").ToString() + "##" + dsRH.Tables(0).Rows(1).Item("RHVALUE").ToString()

                ElseIf Dts1.Tables(0).Rows.Count > 0 And Dts2.Tables(0).Rows.Count = 0 Then
                    dsRH = GetBarrierHighLowHumidity(RH)
                    TSFlag = "3" + "-" + Dts1.Tables(0).Rows(0).Item("TEMPID").ToString() + "##" + dsRH.Tables(0).Rows(0).Item("RHVALUE").ToString() + "##" + dsRH.Tables(0).Rows(1).Item("RHVALUE").ToString()
                    'flag-tempid##MIN(RHUVALUE#RHID)##MAX(RHUVALUE#RHID)
                ElseIf Dts1.Tables(0).Rows.Count = 0 And Dts2.Tables(0).Rows.Count > 0 Then
                    dsRH = GetBarrierHighLowTemp(Temp)
                    TSFlag = "4" + "-" + Dts2.Tables(0).Rows(0).Item("RHID").ToString() + "##" + dsRH.Tables(0).Rows(0).Item("TEMPVALUE").ToString() + "##" + dsRH.Tables(0).Rows(1).Item("TEMPVALUE").ToString()
                    'flag-tempid##MIN(RHUVALUE#RHID)##MAX(RHUVALUE#RHID)
                End If
                Return TSFlag
            Catch ex As Exception
                Return TSFlag
            End Try
        End Function

        Public Function GetBarrierHighLowHumidity(ByVal RH As String) As DataSet
            Dim strsql As String
            Dim Dts As DataSet
            Dim odbUtil As New DBUtil()
            Try
                strsql = "SELECT (A.RHVALUE | | '#' || B.RHID) RHVALUE FROM  "
                strsql = strsql + "( "
                strsql = strsql + "SELECT RHVALUE FROM BARRIERH WHERE RHVALUE IN( "
                strsql = strsql + "SELECT MAX(RHVALUE)  FROM BARRIERH "
                strsql = strsql + "WHERE(RHVALUE < " + RH + ") "
                strsql = strsql + "UNION ALL "
                strsql = strsql + "SELECT MIN(RHVALUE) FROM BARRIERH "
                strsql = strsql + "WHERE RHVALUE >" + RH + ") "
                strsql = strsql + ") A "
                strsql = strsql + "INNER JOIN BARRIERH B "
                strsql = strsql + "ON B.RHVALUE=A.RHVALUE "
                Dts = odbUtil.FillDataSet(strsql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("GetBarrierHighLowHumidity:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetBarrierHighLowTemp(ByVal Temp As String) As DataSet
            Dim strsql As String
            Dim Dts As DataSet
            Dim odbUtil As New DBUtil()
            Try
                strsql = "SELECT (A.TEMPVAL | | '#' || B.TEMPID) TEMPVALUE FROM  "
                strsql = strsql + "( "
                strsql = strsql + "SELECT TEMPVAL FROM BARRIERTEMP WHERE TEMPVAL IN( "
                strsql = strsql + "SELECT MAX(TEMPVAL)  FROM BARRIERTEMP "
                strsql = strsql + "WHERE(TEMPVAL < " + Temp + ") "
                strsql = strsql + "UNION ALL "
                strsql = strsql + "SELECT MIN(TEMPVAL) FROM BARRIERTEMP "
                strsql = strsql + "WHERE TEMPVAL >" + Temp + ") "
                strsql = strsql + ")A "
                strsql = strsql + "INNER JOIN BARRIERTEMP B "
                strsql = strsql + "ON B.TEMPVAL=A.TEMPVAL "
                Dts = odbUtil.FillDataSet(strsql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("GetBarrierHighLowTemp:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function BarrierData(ByVal CaseID As Integer, ByVal MAT() As String, ByVal EDate As Date, ByVal MATSUB(,) As String) As DataSet
            Dim StrSql As String = String.Empty
            Dim StrSql1 As String = String.Empty
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Try
                'Getting All Gradedata
                StrSql = "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MAT(0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MAT(1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MAT(2) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(2) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MAT(3) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(3) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MAT(4) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(4) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MAT(5) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(5) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MAT(6) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(6) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MAT(7) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(7) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MAT(8) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(8) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MAT(9) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(9) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                'Getting Data for Sub Layers
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(0, 0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(0, 0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(0, 1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(0, 1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(1, 0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(1, 0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(1, 1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(1, 1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(2, 0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(2, 0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(2, 1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(2, 1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(3, 0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(3, 0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(3, 1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(3, 1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(4, 0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(4, 0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(4, 1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(4, 1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(5, 0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(5, 0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(5, 1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(5, 1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(6, 0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(6, 0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(6, 1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(6, 1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(7, 0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(7, 0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(7, 1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(7, 1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(8, 0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(8, 0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(8, 1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(8, 1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(9, 0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(9, 0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,MDBREAK,TDBREAK,EFFDATE FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(9, 1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(9, 1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "


                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("BarrierData:" + ex.Message.ToString())
                Return Dts
            End Try

        End Function

              Public Function BarrierDataComp(ByVal MAT() As String, ByVal EDate As Date, ByVal MATSUB(,) As String) As DataSet
            Dim StrSql As String = String.Empty
            Dim StrSql1 As String = String.Empty
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Try
                'Getting All Gradedata
                StrSql = "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MAT(0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MAT(1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MAT(2) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(2) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MAT(3) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(3) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MAT(4) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(4) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MAT(5) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(5) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MAT(6) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(6) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MAT(7) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(7) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MAT(8) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(8) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MAT(9) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MAT(9) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                'Getting Data for Sub Layers
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(0, 0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(0, 0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(0, 1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(0, 1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(1, 0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(1, 0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(1, 1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(1, 1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(2, 0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(2, 0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(2, 1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(2, 1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(3, 0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(3, 0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(3, 1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(3, 1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(4, 0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(4, 0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(4, 1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(4, 1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(5, 0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(5, 0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(5, 1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(5, 1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(6, 0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(6, 0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(6, 1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(6, 1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(7, 0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(7, 0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(7, 1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(7, 1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(8, 0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(8, 0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(8, 1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(8, 1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(9, 0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(9, 0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT MATID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE,MDBREAK,TDBREAK FROM MATBARRIERMETRIC WHERE MATID=" + MATSUB(9, 1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIERMETRIC WHERE MATID= " + MATSUB(9, 1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "


                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("BarrierData:" + ex.Message.ToString())
                Return Dts
            End Try

        End Function

        Public Sub BarrierPropCalculateRH(ByVal CaseID As Integer, ByVal WvtrTemp As String, ByVal WvtrRH As String, _
                                ByVal MAT() As String, ByVal EDate As Date, ByVal MatDes() As String _
                                , ByVal Thick() As String, ByVal ISADJTHICK() As String, ByVal Dts As DataSet, ByVal MTR() As String)
            Try
                Dim StrSql As String = String.Empty
                Dim StrSql1 As String = String.Empty
                'Dim Dts As New DataSet()
                Dim dvBarr As New DataView
                Dim dtBar As New DataTable
                Dim odbUtil As New DBUtil()
                Dim WVTRFlag As String
                Dim dvBar As New DataView
                Dim dsBar As New DataSet()
                Dim WVTRFlagVal As String = ""
                TotalWVTRB = "0"
                WVTRmsgB = ""
                MsgB = ""


                Dim OTRFlagVal As String = ""
                Dim TempRhVal As String = ""
                If Dts.Tables(0).Rows.Count > 0 Then
                    Dim strSpl() As String
                    'Getting WVTR Flag and Values
                    WVTRFlag = GetWVTRFlag(WvtrTemp, WvtrRH)
                    strSpl = Regex.Split(WVTRFlag, "-")
                    If strSpl.Length > 0 Then
                        WVTRFlagVal = strSpl(0)
                        TempRhVal = strSpl(1)
                    End If


                    If WVTRFlagVal = "1" Then
                        Dim tempId, rhId As String
                        Dim SplFlag1() As String
                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            tempId = SplFlag1(0)
                            rhId = SplFlag1(1)
                            For i = 0 To 9
                                dvBar = Dts.Tables(0).DefaultView
                                Try
                                    dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhId
                                    dtBar = dvBar.ToTable()
                                    If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                        WVTRB(i) = "10000"
                                        If Not WVTRmsgB.Contains(MatDes(i)) Then
                                            WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                        End If
                                    Else
                                        WVTRB(i) = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                    End If

                                Catch ex As Exception
                                    WVTRB(i) = "0"

                                End Try
                            Next
                        End If
                    ElseIf WVTRFlagVal = "3" Then
                        Dim tempId, rhIdL, rhIdH, rhValL, rhValH As String
                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String

                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            tempId = SplFlag1(0)
                            SplFlag2 = Regex.Split(SplFlag1(1), "#")
                            SplFlag3 = Regex.Split(SplFlag1(2), "#")
                            If SplFlag2.Length > 0 Then
                                rhIdL = SplFlag2(1)
                                rhValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    rhIdH = SplFlag3(1)
                                    rhValH = SplFlag3(0)
                                    For i = 0 To 9
                                        dvBar = Dts.Tables(0).DefaultView
                                        Try
                                            Dim lowRh As String = ""
                                            Dim highRh As String = ""
                                            Dim cngCounter As Integer = 0
                                            Try
                                                'Getting lower RH
                                                dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdL
                                                dtBar = dvBar.ToTable()
                                                If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                    lowRh = "10000"
                                                    If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                        WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                    End If
                                                    cngCounter = 1
                                                Else
                                                    lowRh = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                End If

                                                'Getting higher RH
                                                dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdH
                                                dtBar = dvBar.ToTable()
                                                If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                    highRh = "10000"
                                                    If cngCounter <> 1 Then
                                                        If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                            WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                        End If
                                                        cngCounter = 1
                                                    End If
                                                Else
                                                    highRh = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                End If
                                                WVTRB(i) = CDbl(lowRh) + ((WvtrRH - rhValL) / (rhValH - rhValL) * (CDbl(highRh) - CDbl(lowRh)))

                                            Catch ex As Exception
                                                WVTRB(i) = "0"
                                            End Try
                                        Catch ex As Exception
                                        End Try
                                    Next
                                End If

                            End If

                        End If
                    ElseIf WVTRFlagVal = "4" Then
                        Dim rhId, tempIdL, tempIdH, tempValL, tempValH As String
                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String

                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            rhId = SplFlag1(0)
                            SplFlag2 = Regex.Split(SplFlag1(1), "#")
                            SplFlag3 = Regex.Split(SplFlag1(2), "#")
                            If SplFlag2.Length > 0 Then
                                tempIdL = SplFlag2(1)
                                tempValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    tempIdH = SplFlag3(1)
                                    tempValH = SplFlag3(0)
                                    For i = 0 To 9
                                        dvBar = Dts.Tables(0).DefaultView
                                        Try
                                            Dim lowTemp As String = ""
                                            Dim highTemp As String = ""
                                            Dim cngCounter As Integer = 0
                                            Try
                                                'Getting lower RH
                                                dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhId
                                                dtBar = dvBar.ToTable()
                                                If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                    lowTemp = "10000"
                                                    If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                        WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                    End If
                                                    cngCounter = 1
                                                Else
                                                    lowTemp = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                End If


                                                'Getting higher RH
                                                dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhId
                                                dtBar = dvBar.ToTable()
                                                If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                    highTemp = "10000"
                                                    If cngCounter <> 1 Then
                                                        If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                            WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                        End If
                                                        cngCounter = 1
                                                    End If
                                                Else
                                                    highTemp = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                End If

                                                WVTRB(i) = CDbl(lowTemp) + ((WvtrTemp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))

                                            Catch ex As Exception
                                                WVTRB(i) = "0"
                                            End Try
                                        Catch ex As Exception

                                        End Try
                                    Next
                                End If

                            End If

                        End If
                    ElseIf WVTRFlagVal = "2" Then
                        Dim tempIdL, tempIdH, tempValL, tempValH As String
                        Dim rhIdL, rhIdH, rhValL, rhValH As String


                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String

                        Dim SplFlag4() As String
                        Dim SplFlag5() As String

                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then

                            SplFlag2 = Regex.Split(SplFlag1(0), "#")
                            SplFlag3 = Regex.Split(SplFlag1(1), "#")

                            SplFlag4 = Regex.Split(SplFlag1(2), "#")
                            SplFlag5 = Regex.Split(SplFlag1(3), "#")

                            If SplFlag2.Length > 0 Then
                                tempIdL = SplFlag2(1)
                                tempValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    tempIdH = SplFlag3(1)
                                    tempValH = SplFlag3(0)
                                    If SplFlag4.Length > 0 Then
                                        rhIdL = SplFlag4(1)
                                        rhValL = SplFlag4(0)
                                        If SplFlag5.Length > 0 Then
                                            rhIdH = SplFlag5(1)
                                            rhValH = SplFlag5(0)
                                            For i = 0 To 9
                                                dvBar = Dts.Tables(0).DefaultView
                                                Try
                                                    Dim tempLrhL, tempHrhH, tempLrhH, tempHrhL As String
                                                    Dim MidTemp1 As String
                                                    Dim MidTemp2 As String
                                                    Dim cngCounter As Integer = 0
                                                    Try
                                                        'Getting lower temp lower Rh
                                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdL
                                                        dtBar = dvBar.ToTable()
                                                        If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                            tempLrhL = "10000"
                                                            If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                                WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                            End If
                                                            cngCounter = 1
                                                        Else
                                                            tempLrhL = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                        End If


                                                        'Getting higher temp higher Rh
                                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdH
                                                        dtBar = dvBar.ToTable()
                                                        If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                            tempHrhH = "10000"
                                                            If cngCounter <> 1 Then
                                                                If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                                    WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                                End If
                                                                cngCounter = 1
                                                            End If
                                                        Else
                                                            tempHrhH = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                        End If


                                                        'Getting lower temp Higher Rh
                                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdH
                                                        dtBar = dvBar.ToTable()
                                                        If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                            tempLrhH = "10000"
                                                            If cngCounter <> 1 Then
                                                                If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                                    WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                                End If
                                                                cngCounter = 1
                                                            End If
                                                        Else
                                                            tempLrhH = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                        End If


                                                        'Getting higher temp lower Rh
                                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdL
                                                        dtBar = dvBar.ToTable()
                                                        If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                            tempHrhL = "10000"
                                                            If cngCounter <> 1 Then
                                                                If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                                    WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                                End If
                                                                cngCounter = 1
                                                            End If
                                                        Else
                                                            tempHrhL = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                        End If


                                                        MidTemp1 = CDbl(tempLrhL) + (CDbl(WvtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhL) - CDbl(CDbl(tempLrhL)))))
                                                        MidTemp2 = CDbl(tempLrhH) + (CDbl(WvtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhH) - CDbl(CDbl(tempLrhH)))))
                                                        WVTRB(i) = MidTemp1 + ((WvtrRH - rhValL) / (rhValH - rhValL) * (MidTemp2 - MidTemp1))

                                                    Catch ex As Exception
                                                        WVTRB(i) = "0"
                                                    End Try

                                                Catch ex As Exception

                                                End Try
                                            Next
                                        End If
                                    End If
                                End If

                            End If

                        End If
                    End If
                    If WVTRmsgB <> "" Then
                        MsgB = "Data is not present for following materials for Date " + CDate(EDate).Date + "\n"
                        If WVTRmsgB <> "" Then
                            MsgB = MsgB + WVTRmsgB + ""
                        End If
                    End If



                    'TotalWVTR 
                    For i = 0 To 9
                        If MAT(i) <> 0 Then
                            If MTR(i) <> 0 Then
                                TotalWVTRB = CDbl(TotalWVTRB) + (1 / CDbl(MTR(i)))
                            Else
                                TotalWVTRB = CDbl(TotalWVTRB) + (1 / CDbl(MTR(i)))
                            End If
                        End If
                    Next
                    If TotalWVTRB <> "0" Then
                        TotalWVTRB = 1 / CDbl(TotalWVTRB)
                    End If
                End If
            Catch ex As Exception

            End Try
        End Sub
        Public Sub RHCalculation(ByVal temp As String, ByVal outRH As String, ByVal inRH As String, ByVal MAT() As String, ByVal Thick() As String, ByVal ISADJTHICK() As String, ByVal MTR() As String, ByVal caseId As String, _
                                    ByVal matSub(,) As String, ByVal eDate As String, ByVal matDes(,) As String, ByVal otrPref(,) As String, ByVal thickSub(,) As String, ByVal isAdjThickSub(,) As String, ByVal thickPer(,) As String, ByVal Dts As DataSet)

            Dim Fact2 As Double = 0.0
            Dim Fact1(9) As Double
            Dim Fact(9) As Double
            Dim BlendRH(9) As String
            Try
                'Calculations for Fact1
                For i = 0 To 9
                    
                    WVTRHUMID(i) = MTR(i)

                    If WVTRHUMID(i) = "0" Then
                        WVTRHUMID(i) = "0.00000000001"
                    End If
                Next

                For i = 0 To 9
                    If MAT(i) <> 0 And Thick(i) <> 0 Then
                        Fact(i) = (1 / (WVTRHUMID(i) / Thick(i)))
                        Select Case i
                            Case 0
                                Fact1(i) = (0 / (WVTRHUMID(0) / Thick(0))) + (1 / (2 * (WVTRHUMID(0) / Thick(0))))
                            Case 1
                                Fact1(i) = (1 / (WVTRHUMID(i - 1) / Thick(i - 1))) + (1 / (2 * (WVTRHUMID(i) / Thick(i))))
                            Case 2
                                Fact1(i) = (1 / (WVTRHUMID(i - 2) / Thick(i - 2))) + (1 / (WVTRHUMID(i - 1) / Thick(i - 1))) + (1 / (2 * (WVTRHUMID(i) / Thick(i))))
                            Case 3
                                Fact1(i) = (1 / (WVTRHUMID(i - 3) / Thick(i - 3))) + (1 / (WVTRHUMID(i - 2) / Thick(i - 2))) + (1 / (WVTRHUMID(i - 1) / Thick(i - 1))) + (1 / (2 * (WVTRHUMID(i) / Thick(i))))
                            Case 4
                                Fact1(i) = (1 / (WVTRHUMID(i - 4) / Thick(i - 4))) + (1 / (WVTRHUMID(i - 3) / Thick(i - 3))) + (1 / (WVTRHUMID(i - 2) / Thick(i - 2))) + (1 / (WVTRHUMID(i - 1) / Thick(i - 1))) + (1 / (2 * (WVTRHUMID(i) / Thick(i))))
                            Case 5
                                Fact1(i) = (1 / (WVTRHUMID(i - 5) / Thick(i - 5))) + (1 / (WVTRHUMID(i - 4) / Thick(i - 4))) + (1 / (WVTRHUMID(i - 3) / Thick(i - 3))) + (1 / (WVTRHUMID(i - 2) / Thick(i - 2))) + (1 / (WVTRHUMID(i - 1) / Thick(i - 1))) + (1 / (2 * (WVTRHUMID(i) / Thick(i))))
                            Case 6
                                Fact1(i) = (1 / (WVTRHUMID(i - 6) / Thick(i - 6))) + (1 / (WVTRHUMID(i - 5) / Thick(i - 5))) + (1 / (WVTRHUMID(i - 4) / Thick(i - 4))) + (1 / (WVTRHUMID(i - 3) / Thick(i - 3))) + (1 / (WVTRHUMID(i - 2) / Thick(i - 2))) + (1 / (WVTRHUMID(i - 1) / Thick(i - 1))) + (1 / (2 * (WVTRHUMID(i) / Thick(i))))
                            Case 7
                                Fact1(i) = (1 / (WVTRHUMID(i - 7) / Thick(i - 7))) + (1 / (WVTRHUMID(i - 6) / Thick(i - 6))) + (1 / (WVTRHUMID(i - 5) / Thick(i - 5))) + (1 / (WVTRHUMID(i - 4) / Thick(i - 4))) + (1 / (WVTRHUMID(i - 3) / Thick(i - 3))) + (1 / (WVTRHUMID(i - 2) / Thick(i - 2))) + (1 / (WVTRHUMID(i - 1) / Thick(i - 1))) + (1 / (2 * (WVTRHUMID(i) / Thick(i))))
                            Case 8
                                Fact1(i) = (1 / (WVTRHUMID(i - 8) / Thick(i - 8))) + (1 / (WVTRHUMID(i - 7) / Thick(i - 7))) + (1 / (WVTRHUMID(i - 6) / Thick(i - 6))) + (1 / (WVTRHUMID(i - 5) / Thick(i - 5))) + (1 / (WVTRHUMID(i - 4) / Thick(i - 4))) + (1 / (WVTRHUMID(i - 3) / Thick(i - 3))) + (1 / (WVTRHUMID(i - 2) / Thick(i - 2))) + (1 / (WVTRHUMID(i - 1) / Thick(i - 1))) + (1 / (2 * (WVTRHUMID(i) / Thick(i))))
                            Case 9
                                Fact1(i) = (1 / (WVTRHUMID(i - 9) / Thick(i - 9))) + (1 / (WVTRHUMID(i - 8) / Thick(i - 8))) + (1 / (WVTRHUMID(i - 7) / Thick(i - 7))) + (1 / (WVTRHUMID(i - 6) / Thick(i - 6))) + (1 / (WVTRHUMID(i - 5) / Thick(i - 5))) + (1 / (WVTRHUMID(i - 4) / Thick(i - 4))) + (1 / (WVTRHUMID(i - 3) / Thick(i - 3))) + (1 / (WVTRHUMID(i - 2) / Thick(i - 2))) + (1 / (WVTRHUMID(i - 1) / Thick(i - 1))) + (1 / (2 * (WVTRHUMID(i) / Thick(i))))
                        End Select
                    Else
                        Fact(i) = 0
                    End If
                Next

                'Calculations for Fact2
                If Fact(0) = 0 And Fact(1) = 0 And Fact(2) = 0 And Fact(3) = 0 And Fact(4) = 0 And Fact(5) = 0 And Fact(6) = 0 And Fact(7) = 0 And Fact(8) = 0 And Fact(9) = 0 Then
                    Fact2 = 0
                Else
                    Fact2 = (outRH - inRH) / (Fact(0) + Fact(1) + Fact(2) + Fact(3) + Fact(4) + Fact(5) + Fact(6) + Fact(7) + Fact(8) + Fact(9))
                End If

                'Calculations for RH
                For i = 0 To 9
                    If MAT(i) <> 0 And Thick(i) <> 0 And WVTRHUMID(i) <> 0 Then
                        If i = 0 Then
                            RH(i) = (2 * (outRH - (Fact1(i) * Fact2))) - outRH

                        Else
                            RH(i) = (2 * (outRH - (Fact1(i) * Fact2))) - RH(i - 1)

                        End If
                    Else
                        RH(i) = 0

                    End If
                Next

                'Calculations for LayerRH
                For i = 0 To 9
                    If MAT(i) <> 0 And Thick(i) <> 0 And WVTRHUMID(i) <> 0 Then
                        If i = 0 Then
                            LayerRH(i) = (outRH + RH(i)) / 2
                        ElseIf i = 9 Then
                            LayerRH(i) = (RH(i - 1) + inRH) / 2
                        Else
                            LayerRH(i) = (RH(i - 1) + RH(i)) / 2
                        End If
                    Else
                        LayerRH(i) = 0
                    End If
                    If MAT(i) > 500 And MAT(i) < 506 Then
                        BlendRH(i) = LayerRH(i).ToString()
                    Else
                        BlendRH(i) = "0"
                    End If
                Next

                BarrierPropCalBlendSubLayersOTR(caseId, temp, BlendRH, matSub, eDate, matDes, otrPref, thickSub, isAdjThickSub, thickPer, Dts)

            Catch
            End Try

        End Sub
        Public Sub BarrierPropCalculateOTR(ByVal Temp As String, ByVal RH() As Double, ByVal MAT() As String, ByVal EDate As Date, ByVal MatDes() As String, ByVal Thick() As String, ByVal ISADJTHICK() As String, ByVal Dts As DataSet)
            Try
                Dim StrSql As String = String.Empty
                Dim StrSql1 As String = String.Empty
                'Dim Dts As New DataSet()
                Dim dvBarr As New DataView
                Dim dtBar As New DataTable
                Dim odbUtil As New DBUtil()
                Dim OTRFlag As String
                Dim dvBar As New DataView
                Dim dsBar As New DataSet()
                TotalOTRB = "0"
                OTRMsgB = ""
                MsgB = ""

                Dim OTRFlagVal As String = ""
                Dim TempRhVal As String = ""

                For i = 0 To 9
                    If MAT(i) > 500 And MAT(i) < 506 Then
                        OTRH(i) = OTRRHB(i)
                    Else
                        If Dts.Tables(0).Rows.Count > 0 Then
                            'Getting OTR Flag and Values
                            OTRFlag = GetOTRFlag(Temp, RH(i))
                            Dim strSpl() As String
                            strSpl = Regex.Split(OTRFlag, "-")
                            If strSpl.Length > 0 Then
                                OTRFlagVal = strSpl(0)
                                TempRhVal = strSpl(1)
                            End If


                            If OTRFlagVal = "1" Then
                                Dim tempId, rhId As String
                                Dim SplFlag1() As String
                                SplFlag1 = Regex.Split(TempRhVal, "##")
                                If SplFlag1.Length > 0 Then
                                    tempId = SplFlag1(0)
                                    rhId = SplFlag1(1)

                                    dvBar = Dts.Tables(0).DefaultView
                                    Try
                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhId
                                        dtBar = dvBar.ToTable()
                                        If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                            OTRH(i) = "10000"
                                            If Not OTRMsgB.Contains(MatDes(i)) Then
                                                OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                            End If

                                        Else
                                            OTRH(i) = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                        End If
                                        If ISADJTHICK(i) <> "N" Then
                                            'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                                            OTRH(i) = CDbl((OTRH(i) / CDbl(Thick(i))) * 25.4)
                                        End If
                                    Catch ex As Exception
                                        OTRH(i) = "0"
                                    End Try

                                End If
                            ElseIf OTRFlagVal = "3" Then
                                Dim tempId, rhIdL, rhIdH, rhValL, rhValH As String
                                Dim SplFlag1() As String
                                Dim SplFlag2() As String
                                Dim SplFlag3() As String


                                SplFlag1 = Regex.Split(TempRhVal, "##")
                                If SplFlag1.Length > 0 Then
                                    tempId = SplFlag1(0)
                                    SplFlag2 = Regex.Split(SplFlag1(1), "#")
                                    SplFlag3 = Regex.Split(SplFlag1(2), "#")
                                    If SplFlag2.Length > 0 Then
                                        rhIdL = SplFlag2(1)
                                        rhValL = SplFlag2(0)
                                        If SplFlag3.Length > 0 Then
                                            rhIdH = SplFlag3(1)
                                            rhValH = SplFlag3(0)

                                            dvBar = Dts.Tables(0).DefaultView
                                            Try
                                                Dim lowRh As String = ""
                                                Dim highRh As String = ""
                                                Dim cngCounter As Integer = 0


                                                Try
                                                    'Getting lower RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdL
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                        lowRh = "10000"
                                                        If Not OTRMsgB.Contains(MatDes(i)) Then
                                                            OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                        End If
                                                        cngCounter = 1
                                                    Else
                                                        lowRh = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                    End If



                                                    'Getting higher RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdH
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                        highRh = "10000"
                                                        If cngCounter <> 1 Then
                                                            If Not OTRMsgB.Contains(MatDes(i)) Then
                                                                OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                            End If
                                                            cngCounter = 1
                                                        End If
                                                    Else
                                                        highRh = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                    End If

                                                    OTRH(i) = CDbl(lowRh) + ((RH(i) - rhValL) / (rhValH - rhValL) * (CDbl(highRh) - CDbl(lowRh)))
                                                    If ISADJTHICK(i) <> "N" Then
                                                        'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                                                        OTRH(i) = CDbl((OTRH(i) / CDbl(Thick(i))) * 25.4)
                                                    End If
                                                Catch ex As Exception
                                                    OTRH(i) = "0"
                                                End Try

                                            Catch ex As Exception

                                            End Try

                                        End If

                                    End If

                                End If
                            ElseIf OTRFlagVal = "4" Then
                                Dim rhId, tempIdL, tempIdH, tempValL, tempValH As String
                                Dim SplFlag1() As String
                                Dim SplFlag2() As String
                                Dim SplFlag3() As String

                                SplFlag1 = Regex.Split(TempRhVal, "##")
                                If SplFlag1.Length > 0 Then
                                    rhId = SplFlag1(0)
                                    SplFlag2 = Regex.Split(SplFlag1(1), "#")
                                    SplFlag3 = Regex.Split(SplFlag1(2), "#")
                                    If SplFlag2.Length > 0 Then
                                        tempIdL = SplFlag2(1)
                                        tempValL = SplFlag2(0)
                                        If SplFlag3.Length > 0 Then
                                            tempIdH = SplFlag3(1)
                                            tempValH = SplFlag3(0)

                                            dvBar = Dts.Tables(0).DefaultView
                                            Try
                                                Dim lowTemp As String = ""
                                                Dim highTemp As String = ""
                                                Dim cngCounter As Integer = 0

                                                Try
                                                    'Getting lower RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhId
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                        lowTemp = "10000"
                                                        If Not OTRMsgB.Contains(MatDes(i)) Then
                                                            OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                        End If
                                                        cngCounter = 1
                                                    Else
                                                        lowTemp = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                    End If

                                                    'Getting higher RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhId
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                        highTemp = "10000"
                                                        If cngCounter <> 1 Then
                                                            If Not OTRMsgB.Contains(MatDes(i)) Then
                                                                OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                            End If
                                                            cngCounter = 1
                                                        End If
                                                    Else
                                                        highTemp = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                    End If
                                                    OTRH(i) = CDbl(lowTemp) + ((Temp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))
                                                    If ISADJTHICK(i) <> "N" Then
                                                        'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                                                        OTRH(i) = CDbl((OTRH(i) / CDbl(Thick(i))) * 25.4)
                                                    End If
                                                Catch ex As Exception
                                                    OTRH(i) = "0"
                                                End Try

                                            Catch ex As Exception

                                            End Try

                                        End If

                                    End If

                                End If
                            ElseIf OTRFlagVal = "2" Then
                                Dim tempIdL, tempIdH, tempValL, tempValH As String
                                Dim rhIdL, rhIdH, rhValL, rhValH As String


                                Dim SplFlag1() As String
                                Dim SplFlag2() As String
                                Dim SplFlag3() As String

                                Dim SplFlag4() As String
                                Dim SplFlag5() As String

                                SplFlag1 = Regex.Split(TempRhVal, "##")
                                If SplFlag1.Length > 0 Then

                                    SplFlag2 = Regex.Split(SplFlag1(0), "#")
                                    SplFlag3 = Regex.Split(SplFlag1(1), "#")

                                    SplFlag4 = Regex.Split(SplFlag1(2), "#")
                                    SplFlag5 = Regex.Split(SplFlag1(3), "#")

                                    If SplFlag2.Length > 0 Then
                                        tempIdL = SplFlag2(1)
                                        tempValL = SplFlag2(0)
                                        If SplFlag3.Length > 0 Then
                                            tempIdH = SplFlag3(1)
                                            tempValH = SplFlag3(0)
                                            If SplFlag4.Length > 0 Then
                                                rhIdL = SplFlag4(1)
                                                rhValL = SplFlag4(0)
                                                If SplFlag5.Length > 0 Then
                                                    rhIdH = SplFlag5(1)
                                                    rhValH = SplFlag5(0)

                                                    dvBar = Dts.Tables(0).DefaultView
                                                    Try
                                                        Dim tempLrhL, tempHrhH, tempLrhH, tempHrhL As String
                                                        Dim MidTemp1 As String
                                                        Dim MidTemp2 As String
                                                        Dim cngCounter As Integer = 0

                                                        Try
                                                            'Getting lower temp lower Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdL
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                                tempLrhL = "10000"
                                                                If Not OTRMsgB.Contains(MatDes(i)) Then
                                                                    OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                                End If
                                                                cngCounter = 1
                                                            Else
                                                                tempLrhL = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                            End If


                                                            'Getting higher temp higher Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdH
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                                tempHrhH = "10000"
                                                                If cngCounter <> 1 Then
                                                                    If Not OTRMsgB.Contains(MatDes(i)) Then
                                                                        OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                                    End If
                                                                    cngCounter = 1
                                                                End If
                                                            Else
                                                                tempHrhH = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                            End If


                                                            'Getting lower temp Higher Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdH
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                                tempLrhH = "10000"
                                                                If cngCounter <> 1 Then
                                                                    If Not OTRMsgB.Contains(MatDes(i)) Then
                                                                        OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                                    End If
                                                                    cngCounter = 1
                                                                End If
                                                            Else
                                                                tempLrhH = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                            End If


                                                            'Getting higher temp lower Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdL
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                                tempHrhL = "10000"
                                                                If cngCounter <> 1 Then
                                                                    If Not OTRMsgB.Contains(MatDes(i)) Then
                                                                        OTRMsgB = OTRMsgB + "" + MatDes(i) + " (OTR)\n"
                                                                    End If
                                                                    cngCounter = 1
                                                                End If
                                                            Else
                                                                tempHrhL = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                            End If

                                                            MidTemp1 = CDbl(tempLrhL) + (CDbl(Temp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhL) - CDbl(CDbl(tempLrhL)))))
                                                            MidTemp2 = CDbl(tempLrhH) + (CDbl(Temp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhH) - CDbl(CDbl(tempLrhH)))))
                                                            OTRH(i) = MidTemp1 + ((RH(i) - rhValL) / (rhValH - rhValL) * (MidTemp2 - MidTemp1))
                                                            If ISADJTHICK(i) <> "N" Then
                                                                'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                                                                OTRH(i) = CDbl((OTRH(i) / CDbl(Thick(i))) * 25.4)
                                                            End If
                                                        Catch ex As Exception
                                                            OTRH(i) = "0"
                                                        End Try

                                                    Catch ex As Exception

                                                    End Try

                                                End If
                                            End If
                                        End If

                                    End If

                                End If
                            End If
                           
                        End If
                    End If
                    If OTRH(i) <> 0 Then
                        OTRAIR(i) = (OTRH(i) * 20.95) / 100
                    Else
                        OTRAIR(i) = 0
                    End If
                Next

                'Total OTR
                For i = 0 To 9
                    If MAT(i) <> 0 Then
                        If OTRH(i) <> 0 Then
                            TotalOTRH = CDbl(TotalOTRH) + (1 / CDbl(OTRH(i)))
                        Else
                            TotalOTRH = CDbl(TotalOTRH) + (1 / CDbl(OTRH(i)))
                        End If
                    End If


                Next
                If TotalOTRH <> "0" Then
                    TotalOTRH = 1 / CDbl(TotalOTRH)
                End If




            Catch ex As Exception

            End Try
        End Sub

#Region "Blend"
        Public Sub BarrierPropCalBlendSubLayers(ByVal CaseID As Integer, ByVal OtrTemp As String, ByVal WvtrTemp As String, ByVal OtrRH As String, ByVal WvtrRH As String, _
                                    ByVal MAT(,) As String, ByVal EDate As Date, ByVal MatDes(,) As String, ByVal OtrPref(,) As String, ByVal WVTRPref(,) As String, _
                                    ByVal TS1Pref(,) As String, ByVal TS2Pref(,) As String, ByVal BDG(,) As String, ByVal Thick(,) As String, ByVal ISADJTHICK(,) As String, _
                                    ByVal ThickPer(,) As String, ByVal OTRDb() As String, ByVal WVTRDb() As String, ByVal TS1Db() As String, ByVal TS2Db() As String, _
                                    ByVal DG() As String, ByVal MATERIAL() As String, ByVal Dts As DataSet, ByVal MatPer() As String, ByVal BMatPer(,) As String)
            Try
                Dim StrSql As String = String.Empty
                Dim StrSql1 As String = String.Empty
                Dim dvBarr As New DataView
                Dim dtBar As New DataTable
                Dim odbUtil As New DBUtil()
                Dim OTRFlag As String
                Dim WVTRFlag As String
                Dim dvBar As New DataView
                Dim dsBar As New DataSet()
                Dim WVTRFlagVal As String = ""
                OTRMsgBlendSub = ""
                WVTRmsgBlendSub = ""
                MsgBlendSub = ""
                'Tensile Strength
                Dim TSFlag As String
                TS1msgBlendSub = ""
                TS2msgBlendSub = ""
                Dim TSFlagVal As String = ""
                Dim OTRFlagVal As String = ""
                Dim TempRhVal As String = ""
                If Dts.Tables(0).Rows.Count > 0 Then
                    'Getting OTR Flag and Values
                    OTRFlag = GetOTRFlag(OtrTemp, OtrRH)
                    Dim strSpl() As String
                    strSpl = Regex.Split(OTRFlag, "-")
                    If strSpl.Length > 0 Then
                        OTRFlagVal = strSpl(0)
                        TempRhVal = strSpl(1)
                    End If


                    If OTRFlagVal = "1" Then
                        Dim tempId, rhId As String
                        Dim SplFlag1() As String
                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            tempId = SplFlag1(0)
                            rhId = SplFlag1(1)
                            For i = 0 To 9
                                For j = 0 To 1
                                    dvBar = Dts.Tables(0).DefaultView
                                    Try
                                        dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempId + " AND RHID=" + rhId
                                        dtBar = dvBar.ToTable()
                                        If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                            OTRBLENDSUB(i, j) = "10000"
                                            OTRL(i, j) = "10000"
                                            If Not OTRMsgBlendSub.Contains(MatDes(i, j)) Then
                                                OTRMsgBlendSub = OTRMsgBlendSub + "" + MatDes(i, j) + " (OTR)\n"
                                            End If

                                        Else
                                            OTRBLENDSUB(i, j) = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                            OTRL(i, j) = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                        End If
                                        If ISADJTHICK(i, j) <> "N" Then
                                            'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                                            OTRBLENDSUB(i, j) = CDbl((OTRBLENDSUB(i, j) / CDbl(Thick(i, j))) * 25.4)
                                        End If
                                    Catch ex As Exception
                                        OTRBLENDSUB(i, j) = "0"
                                        OTRL(i, j) = "0"
                                    End Try
                                Next
                            Next
                        End If
                    ElseIf OTRFlagVal = "3" Then
                        Dim tempId, rhIdL, rhIdH, rhValL, rhValH As String
                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String


                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            tempId = SplFlag1(0)
                            SplFlag2 = Regex.Split(SplFlag1(1), "#")
                            SplFlag3 = Regex.Split(SplFlag1(2), "#")
                            If SplFlag2.Length > 0 Then
                                rhIdL = SplFlag2(1)
                                rhValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    rhIdH = SplFlag3(1)
                                    rhValH = SplFlag3(0)
                                    For i = 0 To 9
                                        For j = 0 To 1
                                            dvBar = Dts.Tables(0).DefaultView
                                            Try
                                                Dim lowRh As String = ""
                                                Dim highRh As String = ""
                                                Dim cngCounter As Integer = 0


                                                Try
                                                    'Getting lower RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdL
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                        lowRh = "10000"
                                                        If Not OTRMsgBlendSub.Contains(MatDes(i, j)) Then
                                                            OTRMsgBlendSub = OTRMsgBlendSub + "" + MatDes(i, j) + " (OTR)\n"
                                                        End If
                                                        cngCounter = 1
                                                    Else
                                                        lowRh = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                    End If



                                                    'Getting higher RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdH
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                        highRh = "10000"
                                                        If cngCounter <> 1 Then
                                                            If Not OTRMsgBlendSub.Contains(MatDes(i, j)) Then
                                                                OTRMsgBlendSub = OTRMsgBlendSub + "" + MatDes(i, j) + " (OTR)\n"
                                                            End If
                                                            cngCounter = 1
                                                        End If
                                                    Else
                                                        highRh = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                    End If

                                                    OTRBLENDSUB(i, j) = CDbl(lowRh) + ((OtrRH - rhValL) / (rhValH - rhValL) * (CDbl(highRh) - CDbl(lowRh)))
                                                    OTRL(i, j) = CDbl(lowRh) + ((OtrRH - rhValL) / (rhValH - rhValL) * (CDbl(highRh) - CDbl(lowRh)))
                                                    If ISADJTHICK(i, j) <> "N" Then
                                                        'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                                                        OTRBLENDSUB(i, j) = CDbl((OTRBLENDSUB(i, j) / CDbl(Thick(i, j))) * 25.4)
                                                    End If
                                                Catch ex As Exception
                                                    OTRBLENDSUB(i, j) = "0"
                                                    OTRL(i, j) = "0"
                                                End Try

                                            Catch ex As Exception

                                            End Try
                                        Next
                                    Next
                                End If

                            End If

                        End If
                    ElseIf OTRFlagVal = "4" Then
                        Dim rhId, tempIdL, tempIdH, tempValL, tempValH As String
                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String

                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            rhId = SplFlag1(0)
                            SplFlag2 = Regex.Split(SplFlag1(1), "#")
                            SplFlag3 = Regex.Split(SplFlag1(2), "#")
                            If SplFlag2.Length > 0 Then
                                tempIdL = SplFlag2(1)
                                tempValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    tempIdH = SplFlag3(1)
                                    tempValH = SplFlag3(0)
                                    For i = 0 To 9
                                        For j = 0 To 1
                                            dvBar = Dts.Tables(0).DefaultView
                                            Try
                                                Dim lowTemp As String = ""
                                                Dim highTemp As String = ""
                                                Dim cngCounter As Integer = 0

                                                Try
                                                    'Getting lower RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhId
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                        lowTemp = "10000"
                                                        If Not OTRMsgBlendSub.Contains(MatDes(i, j)) Then
                                                            OTRMsgBlendSub = OTRMsgBlendSub + "" + MatDes(i, j) + " (OTR)\n"
                                                        End If
                                                        cngCounter = 1
                                                    Else
                                                        lowTemp = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                    End If

                                                    'Getting higher RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhId
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                        highTemp = "10000"
                                                        If cngCounter <> 1 Then
                                                            If Not OTRMsgBlendSub.Contains(MatDes(i, j)) Then
                                                                OTRMsgBlendSub = OTRMsgBlendSub + "" + MatDes(i, j) + " (OTR)\n"
                                                            End If
                                                            cngCounter = 1
                                                        End If
                                                    Else
                                                        highTemp = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                    End If
                                                    OTRBLENDSUB(i, j) = CDbl(lowTemp) + ((OtrTemp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))
                                                    OTRL(i, j) = CDbl(lowTemp) + ((OtrTemp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))
                                                    If ISADJTHICK(i, j) <> "N" Then
                                                        'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                                                        OTRBLENDSUB(i, j) = CDbl((OTRBLENDSUB(i, j) / CDbl(Thick(i, j))) * 25.4)
                                                    End If
                                                Catch ex As Exception
                                                    OTRBLENDSUB(i, j) = "0"
                                                    OTRL(i, j) = "0"
                                                End Try

                                            Catch ex As Exception

                                            End Try
                                        Next
                                    Next
                                End If

                            End If

                        End If
                    ElseIf OTRFlagVal = "2" Then
                        Dim tempIdL, tempIdH, tempValL, tempValH As String
                        Dim rhIdL, rhIdH, rhValL, rhValH As String


                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String

                        Dim SplFlag4() As String
                        Dim SplFlag5() As String

                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then

                            SplFlag2 = Regex.Split(SplFlag1(0), "#")
                            SplFlag3 = Regex.Split(SplFlag1(1), "#")

                            SplFlag4 = Regex.Split(SplFlag1(2), "#")
                            SplFlag5 = Regex.Split(SplFlag1(3), "#")

                            If SplFlag2.Length > 0 Then
                                tempIdL = SplFlag2(1)
                                tempValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    tempIdH = SplFlag3(1)
                                    tempValH = SplFlag3(0)
                                    If SplFlag4.Length > 0 Then
                                        rhIdL = SplFlag4(1)
                                        rhValL = SplFlag4(0)
                                        If SplFlag5.Length > 0 Then
                                            rhIdH = SplFlag5(1)
                                            rhValH = SplFlag5(0)
                                            For i = 0 To 9
                                                For j = 0 To 1
                                                    dvBar = Dts.Tables(0).DefaultView
                                                    Try
                                                        Dim tempLrhL, tempHrhH, tempLrhH, tempHrhL As String
                                                        Dim MidTemp1 As String
                                                        Dim MidTemp2 As String
                                                        Dim cngCounter As Integer = 0

                                                        Try
                                                            'Getting lower temp lower Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdL
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                                tempLrhL = "10000"
                                                                If Not OTRMsgBlendSub.Contains(MatDes(i, j)) Then
                                                                    OTRMsgBlendSub = OTRMsgBlendSub + "" + MatDes(i, j) + " (OTR)\n"
                                                                End If
                                                                cngCounter = 1
                                                            Else
                                                                tempLrhL = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                            End If


                                                            'Getting higher temp higher Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdH
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                                tempHrhH = "10000"
                                                                If cngCounter <> 1 Then
                                                                    If Not OTRMsgBlendSub.Contains(MatDes(i, j)) Then
                                                                        OTRMsgBlendSub = OTRMsgBlendSub + "" + MatDes(i, j) + " (OTR)\n"
                                                                    End If
                                                                    cngCounter = 1
                                                                End If
                                                            Else
                                                                tempHrhH = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                            End If


                                                            'Getting lower temp Higher Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdH
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                                tempLrhH = "10000"
                                                                If cngCounter <> 1 Then
                                                                    If Not OTRMsgBlendSub.Contains(MatDes(i, j)) Then
                                                                        OTRMsgBlendSub = OTRMsgBlendSub + "" + MatDes(i, j) + " (OTR)\n"
                                                                    End If
                                                                    cngCounter = 1
                                                                End If
                                                            Else
                                                                tempLrhH = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                            End If


                                                            'Getting higher temp lower Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdL
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                                tempHrhL = "10000"
                                                                If cngCounter <> 1 Then
                                                                    If Not OTRMsgBlendSub.Contains(MatDes(i, j)) Then
                                                                        OTRMsgBlendSub = OTRMsgBlendSub + "" + MatDes(i, j) + " (OTR)\n"
                                                                    End If
                                                                    cngCounter = 1
                                                                End If
                                                            Else
                                                                tempHrhL = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                            End If

                                                            MidTemp1 = CDbl(tempLrhL) + (CDbl(OtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhL) - CDbl(CDbl(tempLrhL)))))
                                                            MidTemp2 = CDbl(tempLrhH) + (CDbl(OtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhH) - CDbl(CDbl(tempLrhH)))))
                                                            OTRBLENDSUB(i, j) = MidTemp1 + ((OtrRH - rhValL) / (rhValH - rhValL) * (MidTemp2 - MidTemp1))
                                                            OTRL(i, j) = MidTemp1 + ((OtrRH - rhValL) / (rhValH - rhValL) * (MidTemp2 - MidTemp1))
                                                            If ISADJTHICK(i, j) <> "N" Then
                                                                'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                                                                OTRBLENDSUB(i, j) = CDbl((OTRBLENDSUB(i, j) / CDbl(Thick(i, j))) * 25.4)
                                                            End If
                                                        Catch ex As Exception
                                                            OTRBLENDSUB(i, j) = "0"
                                                            OTRL(i, j) = "0"
                                                        End Try

                                                    Catch ex As Exception

                                                    End Try
                                                Next
                                            Next
                                        End If
                                    End If
                                End If

                            End If

                        End If
                    End If

                    'Getting WVTR Flag and Values
                    WVTRFlag = GetWVTRFlag(WvtrTemp, WvtrRH)
                    strSpl = Regex.Split(WVTRFlag, "-")
                    If strSpl.Length > 0 Then
                        WVTRFlagVal = strSpl(0)
                        TempRhVal = strSpl(1)
                    End If


                    If WVTRFlagVal = "1" Then
                        Dim tempId, rhId As String
                        Dim SplFlag1() As String
                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            tempId = SplFlag1(0)
                            rhId = SplFlag1(1)
                            For i = 0 To 9
                                For j = 0 To 1
                                    dvBar = Dts.Tables(0).DefaultView
                                    Try
                                        dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempId + " AND RHID=" + rhId
                                        dtBar = dvBar.ToTable()
                                        If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                            WVTRBLENDSUB(i, j) = "10000"
                                            WVTRL(i, j) = "10000"
                                            If Not WVTRmsgBlendSub.Contains(MatDes(i, j)) Then
                                                WVTRmsgBlendSub = WVTRmsgBlendSub + "" + MatDes(i, j) + " (WVTR)\n"
                                            End If
                                        Else
                                            WVTRBLENDSUB(i, j) = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                            WVTRL(i, j) = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                        End If
                                        If ISADJTHICK(i, j) <> "N" Then
                                            'WVTRB(i) = CDbl(WVTRB(i)) / CDbl(Thick(i))
                                            WVTRBLENDSUB(i, j) = CDbl((WVTRBLENDSUB(i, j) / CDbl(Thick(i, j))) * 25.4)
                                        End If
                                    Catch ex As Exception
                                        WVTRBLENDSUB(i, j) = "0"
                                        WVTRL(i, j) = "0"
                                    End Try
                                Next
                            Next
                        End If
                    ElseIf WVTRFlagVal = "3" Then
                        Dim tempId, rhIdL, rhIdH, rhValL, rhValH As String
                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String

                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            tempId = SplFlag1(0)
                            SplFlag2 = Regex.Split(SplFlag1(1), "#")
                            SplFlag3 = Regex.Split(SplFlag1(2), "#")
                            If SplFlag2.Length > 0 Then
                                rhIdL = SplFlag2(1)
                                rhValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    rhIdH = SplFlag3(1)
                                    rhValH = SplFlag3(0)
                                    For i = 0 To 9
                                        For j = 0 To 1
                                            dvBar = Dts.Tables(0).DefaultView
                                            Try
                                                Dim lowRh As String = ""
                                                Dim highRh As String = ""
                                                Dim cngCounter As Integer = 0
                                                Try
                                                    'Getting lower RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdL
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                        lowRh = "10000"
                                                        If Not WVTRmsgBlendSub.Contains(MatDes(i, j)) Then
                                                            WVTRmsgBlendSub = WVTRmsgBlendSub + "" + MatDes(i, j) + " (WVTR)\n"
                                                        End If
                                                        cngCounter = 1
                                                    Else
                                                        lowRh = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                    End If

                                                    'Getting higher RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdH
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                        highRh = "10000"
                                                        If cngCounter <> 1 Then
                                                            If Not WVTRmsgBlendSub.Contains(MatDes(i, j)) Then
                                                                WVTRmsgBlendSub = WVTRmsgBlendSub + "" + MatDes(i, j) + " (WVTR)\n"
                                                            End If
                                                            cngCounter = 1
                                                        End If
                                                    Else
                                                        highRh = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                    End If
                                                    WVTRBLENDSUB(i, j) = CDbl(lowRh) + ((WvtrRH - rhValL) / (rhValH - rhValL) * (CDbl(highRh) - CDbl(lowRh)))
                                                    WVTRL(i, j) = CDbl(lowRh) + ((WvtrRH - rhValL) / (rhValH - rhValL) * (CDbl(highRh) - CDbl(lowRh)))
                                                    If ISADJTHICK(i, j) <> "N" Then
                                                        'WVTRB(i) = CDbl(WVTRB(i)) / CDbl(Thick(i))
                                                        WVTRBLENDSUB(i, j) = CDbl((WVTRBLENDSUB(i, j) / CDbl(Thick(i, j))) * 25.4)
                                                    End If
                                                Catch ex As Exception
                                                    WVTRBLENDSUB(i, j) = "0"
                                                    WVTRL(i, j) = "0"
                                                End Try
                                            Catch ex As Exception
                                            End Try
                                        Next
                                    Next
                                End If

                            End If

                        End If
                    ElseIf WVTRFlagVal = "4" Then
                        Dim rhId, tempIdL, tempIdH, tempValL, tempValH As String
                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String

                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            rhId = SplFlag1(0)
                            SplFlag2 = Regex.Split(SplFlag1(1), "#")
                            SplFlag3 = Regex.Split(SplFlag1(2), "#")
                            If SplFlag2.Length > 0 Then
                                tempIdL = SplFlag2(1)
                                tempValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    tempIdH = SplFlag3(1)
                                    tempValH = SplFlag3(0)
                                    For i = 0 To 9
                                        For j = 0 To 1
                                            dvBar = Dts.Tables(0).DefaultView
                                            Try
                                                Dim lowTemp As String = ""
                                                Dim highTemp As String = ""
                                                Dim cngCounter As Integer = 0
                                                Try
                                                    'Getting lower RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhId
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                        lowTemp = "10000"
                                                        If Not WVTRmsgBlendSub.Contains(MatDes(i, j)) Then
                                                            WVTRmsgBlendSub = WVTRmsgBlendSub + "" + MatDes(i, j) + " (WVTR)\n"
                                                        End If
                                                        cngCounter = 1
                                                    Else
                                                        lowTemp = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                    End If


                                                    'Getting higher RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhId
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                        highTemp = "10000"
                                                        If cngCounter <> 1 Then
                                                            If Not WVTRmsgBlendSub.Contains(MatDes(i, j)) Then
                                                                WVTRmsgBlendSub = WVTRmsgBlendSub + "" + MatDes(i, j) + " (WVTR)\n"
                                                            End If
                                                            cngCounter = 1
                                                        End If
                                                    Else
                                                        highTemp = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                    End If

                                                    WVTRBLENDSUB(i, j) = CDbl(lowTemp) + ((WvtrTemp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))
                                                    WVTRL(i, j) = CDbl(lowTemp) + ((WvtrTemp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))
                                                    If ISADJTHICK(i, j) <> "N" Then
                                                        'WVTRB(i) = CDbl(WVTRB(i)) / CDbl(Thick(i))
                                                        WVTRBLENDSUB(i, j) = CDbl((WVTRBLENDSUB(i, j) / CDbl(Thick(i, j))) * 25.4)
                                                    End If
                                                Catch ex As Exception
                                                    WVTRBLENDSUB(i, j) = "0"
                                                    WVTRL(i, j) = "0"
                                                End Try
                                            Catch ex As Exception

                                            End Try
                                        Next
                                    Next
                                End If

                            End If

                        End If
                    ElseIf WVTRFlagVal = "2" Then
                        Dim tempIdL, tempIdH, tempValL, tempValH As String
                        Dim rhIdL, rhIdH, rhValL, rhValH As String


                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String

                        Dim SplFlag4() As String
                        Dim SplFlag5() As String

                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then

                            SplFlag2 = Regex.Split(SplFlag1(0), "#")
                            SplFlag3 = Regex.Split(SplFlag1(1), "#")

                            SplFlag4 = Regex.Split(SplFlag1(2), "#")
                            SplFlag5 = Regex.Split(SplFlag1(3), "#")

                            If SplFlag2.Length > 0 Then
                                tempIdL = SplFlag2(1)
                                tempValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    tempIdH = SplFlag3(1)
                                    tempValH = SplFlag3(0)
                                    If SplFlag4.Length > 0 Then
                                        rhIdL = SplFlag4(1)
                                        rhValL = SplFlag4(0)
                                        If SplFlag5.Length > 0 Then
                                            rhIdH = SplFlag5(1)
                                            rhValH = SplFlag5(0)
                                            For i = 0 To 9
                                                For j = 0 To 1
                                                    dvBar = Dts.Tables(0).DefaultView
                                                    Try
                                                        Dim tempLrhL, tempHrhH, tempLrhH, tempHrhL As String
                                                        Dim MidTemp1 As String
                                                        Dim MidTemp2 As String
                                                        Dim cngCounter As Integer = 0
                                                        Try
                                                            'Getting lower temp lower Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdL
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                                tempLrhL = "10000"
                                                                If Not WVTRmsgBlendSub.Contains(MatDes(i, j)) Then
                                                                    WVTRmsgBlendSub = WVTRmsgBlendSub + "" + MatDes(i, j) + " (WVTR)\n"
                                                                End If
                                                                cngCounter = 1
                                                            Else
                                                                tempLrhL = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                            End If


                                                            'Getting higher temp higher Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdH
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                                tempHrhH = "10000"
                                                                If cngCounter <> 1 Then
                                                                    If Not WVTRmsgBlendSub.Contains(MatDes(i, j)) Then
                                                                        WVTRmsgBlendSub = WVTRmsgBlendSub + "" + MatDes(i, j) + " (WVTR)\n"
                                                                    End If
                                                                    cngCounter = 1
                                                                End If
                                                            Else
                                                                tempHrhH = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                            End If


                                                            'Getting lower temp Higher Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdH
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                                tempLrhH = "10000"
                                                                If cngCounter <> 1 Then
                                                                    If Not WVTRmsgBlendSub.Contains(MatDes(i, j)) Then
                                                                        WVTRmsgBlendSub = WVTRmsgBlendSub + "" + MatDes(i, j) + " (WVTR)\n"
                                                                    End If
                                                                    cngCounter = 1
                                                                End If
                                                            Else
                                                                tempLrhH = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                            End If


                                                            'Getting higher temp lower Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdL
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                                tempHrhL = "10000"
                                                                If cngCounter <> 1 Then
                                                                    If Not WVTRmsgBlendSub.Contains(MatDes(i, j)) Then
                                                                        WVTRmsgBlendSub = WVTRmsgBlendSub + "" + MatDes(i, j) + " (WVTR)\n"
                                                                    End If
                                                                    cngCounter = 1
                                                                End If
                                                            Else
                                                                tempHrhL = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                            End If


                                                            MidTemp1 = CDbl(tempLrhL) + (CDbl(WvtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhL) - CDbl(CDbl(tempLrhL)))))
                                                            MidTemp2 = CDbl(tempLrhH) + (CDbl(WvtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhH) - CDbl(CDbl(tempLrhH)))))
                                                            WVTRBLENDSUB(i, j) = MidTemp1 + ((WvtrRH - rhValL) / (rhValH - rhValL) * (MidTemp2 - MidTemp1))
                                                            WVTRL(i, j) = MidTemp1 + ((WvtrRH - rhValL) / (rhValH - rhValL) * (MidTemp2 - MidTemp1))
                                                            If ISADJTHICK(i, j) <> "N" Then
                                                                'WVTRB(i) = CDbl(WVTRB(i)) / CDbl(Thick(i))
                                                                WVTRBLENDSUB(i, j) = CDbl((WVTRBLENDSUB(i, j) / CDbl(Thick(i, j))) * 25.4)
                                                            End If
                                                        Catch ex As Exception
                                                            WVTRBLENDSUB(i, j) = "0"
                                                            WVTRL(i, j) = "0"
                                                        End Try

                                                    Catch ex As Exception

                                                    End Try
                                                Next
                                            Next
                                        End If
                                    End If
                                End If

                            End If

                        End If
                    End If
                    'Tensile Strength value
                    TSFlag = GetTSFlag("25", "100")
                    Dim strTSpl() As String
                    strTSpl = Regex.Split(TSFlag, "-")
                    If strSpl.Length > 0 Then
                        TSFlagVal = strTSpl(0)
                        TempRhVal = strTSpl(1)
                    End If


                    If TSFlagVal = "1" Then
                        Dim tempId, rhId As String
                        Dim SplFlag1() As String
                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            tempId = SplFlag1(0)
                            rhId = SplFlag1(1)
                            For i = 0 To 9
                                For j = 0 To 1
                                    dvBar = Dts.Tables(0).DefaultView
                                    Try
                                        dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempId + " AND RHID=" + rhId
                                        dtBar = dvBar.ToTable()
                                        Try
                                            If dtBar.Rows(0).Item("MDBREAK").ToString() = "" Then
                                                TS1BLENDSUB(i, j) = ""
                                                TS1L(i, j) = ""

                                                'calculate Dgrade Value
                                                DG1BLENDSUB(i, j) = "" ' (10000 * BDG(i, j)).ToString()
                                                DG1L(i, j) = "" ' (10000 * BDG(i, j)).ToString()
                                                If Not TS1msgBlendSub.Contains(MatDes(i, j)) Then
                                                    TS1msgBlendSub = TS1msgBlendSub + "" + MatDes(i, j) + " (TS1)\n"
                                                End If

                                            Else
                                                TS1BLENDSUB(i, j) = dtBar.Rows(0).Item("MDBREAK").ToString()
                                                TS1L(i, j) = dtBar.Rows(0).Item("MDBREAK").ToString()

                                                'calculate Dgrade Value
                                                If BDG(i, j) <> "" Then
                                                    DG1BLENDSUB(i, j) = (dtBar.Rows(0).Item("MDBREAK") * BDG(i, j)).ToString()
                                                    DG1L(i, j) = (dtBar.Rows(0).Item("MDBREAK") * BDG(i, j)).ToString()
                                                Else
                                                    DG1BLENDSUB(i, j) = "0"
                                                    DG1L(i, j) = "0"
                                                End If
                                            End If
                                        Catch ex As Exception
                                            TS1BLENDSUB(i, j) = "0"
                                            TS1L(i, j) = "0"
                                            DG1BLENDSUB(i, j) = "0"
                                            DG1L(i, j) = "0"
                                        End Try
                                        
                                        Try
                                            If dtBar.Rows(0).Item("TDBREAK").ToString() = "" Then
                                                TS2BLENDSUB(i, j) = ""
                                                TS2L(i, j) = ""

                                                'calculate Dgrade Value
                                                DG2BLENDSUB(i, j) = "" ' (10000 * BDG(i, j)).ToString()
                                                DG2L(i, j) = "" ' (10000 * BDG(i, j)).ToString()
                                                If Not TS2msgBlendSub.Contains(MatDes(i, j)) Then
                                                    TS2msgBlendSub = TS2msgBlendSub + "" + MatDes(i, j) + " (TS2)\n"
                                                End If

                                            Else
                                                TS2BLENDSUB(i, j) = dtBar.Rows(0).Item("TDBREAK").ToString()
                                                TS2L(i, j) = dtBar.Rows(0).Item("TDBREAK").ToString()

                                                'calculate Dgrade Value
                                                If BDG(i, j) <> "" Then
                                                    DG2BLENDSUB(i, j) = (dtBar.Rows(0).Item("TDBREAK") * BDG(i, j)).ToString()
                                                    DG2L(i, j) = (dtBar.Rows(0).Item("TDBREAK") * BDG(i, j)).ToString()
                                                Else
                                                    DG2BLENDSUB(i, j) = "0" '(dtBar.Rows(0).Item("TDBREAK") * BDG(i, j)).ToString()
                                                    DG2L(i, j) = "0" '(dtBar.Rows(0).Item("TDBREAK") * BDG(i, j)).ToString()
                                                End If
                                            End If
                                        Catch ex As Exception
                                            TS2BLENDSUB(i, j) = "0"
                                            TS2L(i, j) = "0"
                                            DG2BLENDSUB(i, j) = "0"
                                            DG2L(i, j) = "0"
                                        End Try
                                       

                                        If ISADJTHICK(i, j) <> "N" Then
                                            'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                                            'TS1BLENDSUB(i, j) = CDbl((TS1BLENDSUB(i, j) / CDbl(Thick(i, j))) * 25.4)
                                            'TS2BLENDSUB(i, j) = CDbl((TS2BLENDSUB(i, j) / CDbl(Thick(i, j))) * 25.4)

                                            'calculate Dgrade Value
                                            'DG1BLENDSUB(i, j) = CDbl((DG1BLENDSUB(i, j) / CDbl(Thick(i, j))) * 25.4)
                                            'DG2BLENDSUB(i, j) = CDbl((DG2BLENDSUB(i, j) / CDbl(Thick(i, j))) * 25.4)
                                        End If
                                    Catch ex As Exception
                                        
                                        

                                        'calculate Dgrade Value
                                        
                                        
                                    End Try
                                Next
                            Next
                        End If
                        'ElseIf TSFlagVal = "3" Then
                        '    Dim tempId, rhIdL, rhIdH, rhValL, rhValH As String
                        '    Dim SplFlag1() As String
                        '    Dim SplFlag2() As String
                        '    Dim SplFlag3() As String


                        '    SplFlag1 = Regex.Split(TempRhVal, "##")
                        '    If SplFlag1.Length > 0 Then
                        '        tempId = SplFlag1(0)
                        '        SplFlag2 = Regex.Split(SplFlag1(1), "#")
                        '        SplFlag3 = Regex.Split(SplFlag1(2), "#")
                        '        If SplFlag2.Length > 0 Then
                        '            rhIdL = SplFlag2(1)
                        '            rhValL = SplFlag2(0)
                        '            If SplFlag3.Length > 0 Then
                        '                rhIdH = SplFlag3(1)
                        '                rhValH = SplFlag3(0)
                        '                For i = 0 To 9
                        '                    For j = 0 To 1
                        '                        dvBar = Dts.Tables(0).DefaultView
                        '                        Try
                        '                            Dim lowRh As String = ""
                        '                            Dim highRh As String = ""
                        '                            Dim cngCounter As Integer = 0


                        '                            Try
                        '                                'Getting lower RH
                        '                                dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdL
                        '                                dtBar = dvBar.ToTable()
                        '                                If dtBar.Rows(0).Item("TSVALUE").ToString() = "" Then
                        '                                    lowRh = "10000"
                        '                                    If Not OTRMsgBlendSub.Contains(MatDes(i, j)) Then
                        '                                        TSmsgBlendSub = TSmsgBlendSub + "" + MatDes(i, j) + " (TS)\n"
                        '                                    End If
                        '                                    cngCounter = 1
                        '                                Else
                        '                                    lowRh = dtBar.Rows(0).Item("TSVALUE").ToString()
                        '                                End If



                        '                                'Getting higher RH
                        '                                dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdH
                        '                                dtBar = dvBar.ToTable()
                        '                                If dtBar.Rows(0).Item("TSVALUE").ToString() = "" Then
                        '                                    highRh = "10000"
                        '                                    If cngCounter <> 1 Then
                        '                                        If Not TSmsgBlendSub.Contains(MatDes(i, j)) Then
                        '                                            TSmsgBlendSub = TSmsgBlendSub + "" + MatDes(i, j) + " (TS)\n"
                        '                                        End If
                        '                                        cngCounter = 1
                        '                                    End If
                        '                                Else
                        '                                    highRh = dtBar.Rows(0).Item("TSVALUE").ToString()
                        '                                End If

                        '                                TSBLENDSUB(i, j) = CDbl(lowRh) + ((OtrRH - rhValL) / (rhValH - rhValL) * (CDbl(highRh) - CDbl(lowRh)))
                        '                                TSL(i, j) = CDbl(lowRh) + ((OtrRH - rhValL) / (rhValH - rhValL) * (CDbl(highRh) - CDbl(lowRh)))
                        '                                If ISADJTHICK(i, j) <> "N" Then
                        '                                    'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                        '                                    TSBLENDSUB(i, j) = CDbl((TSBLENDSUB(i, j) / CDbl(Thick(i, j))) * 25.4)
                        '                                End If
                        '                            Catch ex As Exception
                        '                                TSBLENDSUB(i, j) = "0"
                        '                                TSL(i, j) = "0"
                        '                            End Try

                        '                        Catch ex As Exception

                        '                        End Try
                        '                    Next
                        '                Next
                        '            End If

                        '        End If

                        '    End If
                        'ElseIf TSFlagVal = "4" Then
                        '    Dim rhId, tempIdL, tempIdH, tempValL, tempValH As String
                        '    Dim SplFlag1() As String
                        '    Dim SplFlag2() As String
                        '    Dim SplFlag3() As String

                        '    SplFlag1 = Regex.Split(TempRhVal, "##")
                        '    If SplFlag1.Length > 0 Then
                        '        rhId = SplFlag1(0)
                        '        SplFlag2 = Regex.Split(SplFlag1(1), "#")
                        '        SplFlag3 = Regex.Split(SplFlag1(2), "#")
                        '        If SplFlag2.Length > 0 Then
                        '            tempIdL = SplFlag2(1)
                        '            tempValL = SplFlag2(0)
                        '            If SplFlag3.Length > 0 Then
                        '                tempIdH = SplFlag3(1)
                        '                tempValH = SplFlag3(0)
                        '                For i = 0 To 9
                        '                    For j = 0 To 1
                        '                        dvBar = Dts.Tables(0).DefaultView
                        '                        Try
                        '                            Dim lowTemp As String = ""
                        '                            Dim highTemp As String = ""
                        '                            Dim cngCounter As Integer = 0

                        '                            Try
                        '                                'Getting lower RH
                        '                                dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhId
                        '                                dtBar = dvBar.ToTable()
                        '                                If dtBar.Rows(0).Item("TSVALUE").ToString() = "" Then
                        '                                    lowTemp = "10000"
                        '                                    If Not TSmsgBlendSub.Contains(MatDes(i, j)) Then
                        '                                        TSmsgBlendSub = TSmsgBlendSub + "" + MatDes(i, j) + " (TS)\n"
                        '                                    End If
                        '                                    cngCounter = 1
                        '                                Else
                        '                                    lowTemp = dtBar.Rows(0).Item("TSVALUE").ToString()
                        '                                End If

                        '                                'Getting higher RH
                        '                                dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhId
                        '                                dtBar = dvBar.ToTable()
                        '                                If dtBar.Rows(0).Item("TSVALUE").ToString() = "" Then
                        '                                    highTemp = "10000"
                        '                                    If cngCounter <> 1 Then
                        '                                        If Not TSmsgBlendSub.Contains(MatDes(i, j)) Then
                        '                                            TSmsgBlendSub = TSmsgBlendSub + "" + MatDes(i, j) + " (TS)\n"
                        '                                        End If
                        '                                        cngCounter = 1
                        '                                    End If
                        '                                Else
                        '                                    highTemp = dtBar.Rows(0).Item("TSVALUE").ToString()
                        '                                End If
                        '                                TSBLENDSUB(i, j) = CDbl(lowTemp) + ((OtrTemp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))
                        '                                TSL(i, j) = CDbl(lowTemp) + ((OtrTemp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))
                        '                                If ISADJTHICK(i, j) <> "N" Then
                        '                                    'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                        '                                    TSBLENDSUB(i, j) = CDbl((TSBLENDSUB(i, j) / CDbl(Thick(i, j))) * 25.4)
                        '                                End If
                        '                            Catch ex As Exception
                        '                                TSBLENDSUB(i, j) = "0"
                        '                                TSL(i, j) = "0"
                        '                            End Try

                        '                        Catch ex As Exception

                        '                        End Try
                        '                    Next
                        '                Next
                        '            End If

                        '        End If

                        '    End If
                        'ElseIf TSFlagVal = "2" Then
                        '    Dim tempIdL, tempIdH, tempValL, tempValH As String
                        '    Dim rhIdL, rhIdH, rhValL, rhValH As String


                        '    Dim SplFlag1() As String
                        '    Dim SplFlag2() As String
                        '    Dim SplFlag3() As String

                        '    Dim SplFlag4() As String
                        '    Dim SplFlag5() As String

                        '    SplFlag1 = Regex.Split(TempRhVal, "##")
                        '    If SplFlag1.Length > 0 Then

                        '        SplFlag2 = Regex.Split(SplFlag1(0), "#")
                        '        SplFlag3 = Regex.Split(SplFlag1(1), "#")

                        '        SplFlag4 = Regex.Split(SplFlag1(2), "#")
                        '        SplFlag5 = Regex.Split(SplFlag1(3), "#")

                        '        If SplFlag2.Length > 0 Then
                        '            tempIdL = SplFlag2(1)
                        '            tempValL = SplFlag2(0)
                        '            If SplFlag3.Length > 0 Then
                        '                tempIdH = SplFlag3(1)
                        '                tempValH = SplFlag3(0)
                        '                If SplFlag4.Length > 0 Then
                        '                    rhIdL = SplFlag4(1)
                        '                    rhValL = SplFlag4(0)
                        '                    If SplFlag5.Length > 0 Then
                        '                        rhIdH = SplFlag5(1)
                        '                        rhValH = SplFlag5(0)
                        '                        For i = 0 To 9
                        '                            For j = 0 To 1
                        '                                dvBar = Dts.Tables(0).DefaultView
                        '                                Try
                        '                                    Dim tempLrhL, tempHrhH, tempLrhH, tempHrhL As String
                        '                                    Dim MidTemp1 As String
                        '                                    Dim MidTemp2 As String
                        '                                    Dim cngCounter As Integer = 0

                        '                                    Try
                        '                                        'Getting lower temp lower Rh
                        '                                        dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdL
                        '                                        dtBar = dvBar.ToTable()
                        '                                        If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                        '                                            tempLrhL = "10000"
                        '                                            If Not TSmsgBlendSub.Contains(MatDes(i, j)) Then
                        '                                                TSmsgBlendSub = TSmsgBlendSub + "" + MatDes(i, j) + " (TS)\n"
                        '                                            End If
                        '                                            cngCounter = 1
                        '                                        Else
                        '                                            tempLrhL = dtBar.Rows(0).Item("TSVALUE").ToString()
                        '                                        End If


                        '                                        'Getting higher temp higher Rh
                        '                                        dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdH
                        '                                        dtBar = dvBar.ToTable()
                        '                                        If dtBar.Rows(0).Item("TSVALUE").ToString() = "" Then
                        '                                            tempHrhH = "10000"
                        '                                            If cngCounter <> 1 Then
                        '                                                If Not TSmsgBlendSub.Contains(MatDes(i, j)) Then
                        '                                                    TSmsgBlendSub = TSmsgBlendSub + "" + MatDes(i, j) + " (TS)\n"
                        '                                                End If
                        '                                                cngCounter = 1
                        '                                            End If
                        '                                        Else
                        '                                            tempHrhH = dtBar.Rows(0).Item("TSVALUE").ToString()
                        '                                        End If


                        '                                        'Getting lower temp Higher Rh
                        '                                        dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdH
                        '                                        dtBar = dvBar.ToTable()
                        '                                        If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                        '                                            tempLrhH = "10000"
                        '                                            If cngCounter <> 1 Then
                        '                                                If Not TSmsgBlendSub.Contains(MatDes(i, j)) Then
                        '                                                    TSmsgBlendSub = TSmsgBlendSub + "" + MatDes(i, j) + " (TS)\n"
                        '                                                End If
                        '                                                cngCounter = 1
                        '                                            End If
                        '                                        Else
                        '                                            tempLrhH = dtBar.Rows(0).Item("TSVALUE").ToString()
                        '                                        End If


                        '                                        'Getting higher temp lower Rh
                        '                                        dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdL
                        '                                        dtBar = dvBar.ToTable()
                        '                                        If dtBar.Rows(0).Item("TSVALUE").ToString() = "" Then
                        '                                            tempHrhL = "10000"
                        '                                            If cngCounter <> 1 Then
                        '                                                If Not TSmsgBlendSub.Contains(MatDes(i, j)) Then
                        '                                                    TSmsgBlendSub = TSmsgBlendSub + "" + MatDes(i, j) + " (TS)\n"
                        '                                                End If
                        '                                                cngCounter = 1
                        '                                            End If
                        '                                        Else
                        '                                            tempHrhL = dtBar.Rows(0).Item("TSVALUE").ToString()
                        '                                        End If

                        '                                        MidTemp1 = CDbl(tempLrhL) + (CDbl(OtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhL) - CDbl(CDbl(tempLrhL)))))
                        '                                        MidTemp2 = CDbl(tempLrhH) + (CDbl(OtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhH) - CDbl(CDbl(tempLrhH)))))
                        '                                        OTRBLENDSUB(i, j) = MidTemp1 + ((OtrRH - rhValL) / (rhValH - rhValL) * (MidTemp2 - MidTemp1))
                        '                                        OTRL(i, j) = MidTemp1 + ((OtrRH - rhValL) / (rhValH - rhValL) * (MidTemp2 - MidTemp1))
                        '                                        If ISADJTHICK(i, j) <> "N" Then
                        '                                            'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                        '                                            TSBLENDSUB(i, j) = CDbl((TSBLENDSUB(i, j) / CDbl(Thick(i, j))) * 25.4)
                        '                                        End If
                        '                                    Catch ex As Exception
                        '                                        TSBLENDSUB(i, j) = "0"
                        '                                        TSL(i, j) = "0"
                        '                                    End Try

                        '                                Catch ex As Exception

                        '                                End Try
                        '                            Next
                        '                        Next
                        '                    End If
                        '                End If
                        '            End If

                        '        End If

                        '    End If
                    End If
                    If OTRMsgBlendSub <> "" Or WVTRmsgBlendSub <> "" Or TS1msgBlendSub <> "" Or TS2msgBlendSub <> "" Then
                        MsgBlendSub = "Data is not present for following materials for Date " + CDate(EDate).Date + "\n"
                        If OTRMsgBlendSub <> "" Then
                            MsgBlendSub = MsgBlendSub + OTRMsgBlendSub + ""
                        End If
                        If WVTRmsgBlendSub <> "" Then
                            MsgBlendSub = MsgBlendSub + WVTRmsgBlendSub + ""
                        End If
                        If TS1msgBlendSub <> "" Then
                            MsgBlendSub = MsgBlendSub + TS1msgBlendSub + ""
                        End If
                        If TS2msgBlendSub <> "" Then
                            MsgBlendSub = MsgBlendSub + TS2msgBlendSub + ""
                        End If
                    End If

                    If Dts.Tables(0).Rows.Count > 0 Then
                        BarrierPropCalBlend(OtrPref, WVTRPref, TS1Pref, TS2Pref, BDG, ThickPer, MAT, EDate, OTRDb, WVTRDb, TS1Db, TS2Db, DG, MATERIAL, Thick, MatPer, BMatPer)
                    End If
                End If
            Catch ex As Exception

            End Try
        End Sub

        Public Sub BarrierPropCalBlend(ByVal OtrPref(,) As String, ByVal WVTRPref(,) As String, ByVal TS1Pref(,) As String, ByVal TS2Pref(,) As String, ByVal BdgDb(,) As String, _
                                       ByVal ThickPer(,) As String, ByVal MAT(,) As String, ByVal EDate As String, ByVal otrDb() As String, ByVal wvtrDb() As String, _
                                       ByVal ts1Db() As String, ByVal ts2Db() As String, ByVal dgDb() As String, ByVal MATERIAL() As String, ByVal Thick(,) As String, ByVal MatPer() As String, ByVal BMatPer(,) As String)
            Try
                Dim StrSql As String = String.Empty
                Dim StrSql1 As String = String.Empty
                Dim DtsWVTR As New DataSet()
                Dim DtsOTR As New DataSet()
                Dim PERCENTOTR(9) As String
                Dim LAMINATEOTR(9) As String
                Dim PERCENTWVTR(9) As String
                Dim LAMINATEWVTR(9) As String
                Dim dvBarr As New DataView
                Dim dtBarr As New DataTable
                Dim odbUtil As New DBUtil()
                Dim dvBar As New DataView
                Dim dtBar As New DataTable
                TotalOTRBLEND = "0"
                TotalWVTRBLEND = "0"
                Dim OTR1 As Double
                Dim OTR2 As Double
                Dim WVTR1 As Double
                Dim WVTR2 As Double
                'Tensile Strength
                TotalTS1BLEND = "0"
                TotalTS2BLEND = "0"
                Dim TS1Val1 As Double
                Dim TS1Val2 As Double
                Dim TS2Val1 As Double
                Dim TS2Val2 As Double
                Dim DG1Val As Double
                Dim DG2Val As Double
                For i = 0 To 9
                    For j = 0 To 1 - 1
                        If OtrPref(i, j) <> "" And OtrPref(i, j + 1) <> "" Then
                            If OtrPref(i, j) <> "0" And OtrPref(i, j + 1) <> "0" Then
                                LAMINATEOTR(i) = (1 / ((1 / OtrPref(i, j + 1)) + (1 / OtrPref(i, j))))
                                OTR1 = CDbl(OtrPref(i, j) * Thick(i, j) / 25.4)
                                OTR2 = CDbl(OtrPref(i, j + 1) * Thick(i, j + 1) / 25.4)
                                If OTR1 < OTR2 Then
                                    PERCENTOTR(i) = ThickPer(i, j)
                                ElseIf OTR2 < OTR1 Then
                                    PERCENTOTR(i) = ThickPer(i, j + 1)
                                Else
                                    PERCENTOTR(i) = ThickPer(i, j)
                                End If
                            Else
                                LAMINATEOTR(i) = "0"
                                OTR(i) = "0"
                                PERCENTOTR(i) = "0"
                            End If
                        ElseIf OtrPref(i, j) <> "" Or OtrPref(i, j + 1) <> "" Then
                            If OtrPref(i, j) = "" Then
                                If OTRBLENDSUB(i, j) <> "0" And OtrPref(i, j + 1) <> "0" Then
                                    LAMINATEOTR(i) = (1 / ((1 / OtrPref(i, j + 1)) + (1 / OTRBLENDSUB(i, j))))
                                    OTR2 = CDbl(OtrPref(i, j + 1) * Thick(i, j + 1) / 25.4)
                                    If OTRL(i, j) < OTR2 Then
                                        PERCENTOTR(i) = ThickPer(i, j)
                                    ElseIf OTR2 < OTRL(i, j) Then
                                        PERCENTOTR(i) = ThickPer(i, j + 1)
                                    Else
                                        PERCENTOTR(i) = ThickPer(i, j)
                                    End If
                                Else
                                    LAMINATEOTR(i) = "0"
                                    OTR(i) = "0"
                                    PERCENTOTR(i) = "0"
                                End If
                            ElseIf OtrPref(i, j + 1) = "" Then
                                If OtrPref(i, j) <> "0" And OTRBLENDSUB(i, j + 1) <> "0" Then
                                    LAMINATEOTR(i) = (1 / ((1 / OTRBLENDSUB(i, j + 1)) + (1 / OtrPref(i, j))))
                                    OTR1 = CDbl(OtrPref(i, j) * Thick(i, j) / 25.4)
                                    If OTR1 < OTRL(i, j + 1) Then
                                        PERCENTOTR(i) = ThickPer(i, j)
                                    ElseIf OTRL(i, j + 1) < OTR1 Then
                                        PERCENTOTR(i) = ThickPer(i, j + 1)
                                    Else
                                        PERCENTOTR(i) = ThickPer(i, j)
                                    End If
                                Else
                                    LAMINATEOTR(i) = "0"
                                    OTR(i) = "0"
                                    PERCENTOTR(i) = "0"
                                End If
                            End If
                        ElseIf OTRL(i, j) <> "0" And OTRL(i, j + 1) <> "0" Then
                            LAMINATEOTR(i) = (1 / ((1 / OTRBLENDSUB(i, j + 1)) + (1 / OTRBLENDSUB(i, j))))
                            If OTRL(i, j) < OTRL(i, j + 1) Then
                                PERCENTOTR(i) = ThickPer(i, j)
                            ElseIf OTRL(i, j + 1) < OTRL(i, j) Then
                                PERCENTOTR(i) = ThickPer(i, j + 1)
                            Else
                                PERCENTOTR(i) = ThickPer(i, j)
                            End If
                        Else
                            LAMINATEOTR(i) = "0"
                            OTR(i) = "0"
                            PERCENTOTR(i) = "0"
                        End If

                        'WVTR Calculation
                        If WVTRPref(i, j) <> "" And WVTRPref(i, j + 1) <> "" Then
                            If WVTRPref(i, j) <> "0" And WVTRPref(i, j + 1) <> "0" Then
                                LAMINATEWVTR(i) = (1 / ((1 / WVTRPref(i, j + 1)) + (1 / WVTRPref(i, j))))
                                WVTR1 = CDbl(WVTRPref(i, j) * Thick(i, j) / 25.4)
                                WVTR2 = CDbl(WVTRPref(i, j + 1) * Thick(i, j + 1) / 25.4)
                                If WVTR1 < WVTR2 Then
                                    PERCENTWVTR(i) = ThickPer(i, j)
                                ElseIf WVTR2 < WVTR1 Then
                                    PERCENTWVTR(i) = ThickPer(i, j + 1)
                                Else
                                    PERCENTWVTR(i) = ThickPer(i, j)
                                End If
                            Else
                                LAMINATEWVTR(i) = "0"
                                WVTR(i) = "0"
                                PERCENTWVTR(i) = "0"
                            End If
                        ElseIf WVTRPref(i, j) <> "" Or WVTRPref(i, j + 1) <> "" Then
                            If WVTRPref(i, j) = "" Then
                                If WVTRBLENDSUB(i, j) <> "0" And WVTRPref(i, j + 1) <> "0" Then
                                    LAMINATEWVTR(i) = (1 / ((1 / WVTRPref(i, j + 1)) + (1 / WVTRBLENDSUB(i, j))))
                                    WVTR2 = CDbl(WVTRPref(i, j + 1) * Thick(i, j + 1) / 25.4)
                                    If WVTRL(i, j) < WVTR2 Then
                                        PERCENTWVTR(i) = ThickPer(i, j)
                                    ElseIf WVTR2 < WVTRL(i, j) Then
                                        PERCENTWVTR(i) = ThickPer(i, j + 1)
                                    Else
                                        PERCENTWVTR(i) = ThickPer(i, j)
                                    End If
                                Else
                                    LAMINATEWVTR(i) = "0"
                                    WVTR(i) = "0"
                                    PERCENTWVTR(i) = "0"
                                End If
                            ElseIf WVTRPref(i, j + 1) = "" Then
                                If WVTRPref(i, j) <> "0" And WVTRBLENDSUB(i, j + 1) <> "0" Then
                                    LAMINATEWVTR(i) = (1 / ((1 / WVTRBLENDSUB(i, j + 1)) + (1 / WVTRPref(i, j))))
                                    WVTR1 = CDbl(WVTRPref(i, j) * Thick(i, j) / 25.4)
                                    If WVTR1 < WVTRL(i, j + 1) Then
                                        PERCENTWVTR(i) = ThickPer(i, j)
                                    ElseIf WVTRL(i, j + 1) < WVTR1 Then
                                        PERCENTWVTR(i) = ThickPer(i, j + 1)
                                    Else
                                        PERCENTWVTR(i) = ThickPer(i, j)
                                    End If
                                Else
                                    LAMINATEWVTR(i) = "0"
                                    WVTR(i) = "0"
                                    PERCENTWVTR(i) = "0"
                                End If
                            End If
                        ElseIf WVTRL(i, j) <> "0" And WVTRL(i, j + 1) <> "0" Then
                            LAMINATEWVTR(i) = (1 / ((1 / WVTRBLENDSUB(i, j + 1)) + (1 / WVTRBLENDSUB(i, j))))
                            If WVTRL(i, j) < WVTRL(i, j + 1) Then
                                PERCENTWVTR(i) = ThickPer(i, j)
                            ElseIf WVTRL(i, j + 1) < WVTRL(i, j) Then
                                PERCENTWVTR(i) = ThickPer(i, j + 1)
                            Else
                                PERCENTWVTR(i) = ThickPer(i, j)
                            End If
                        Else
                            LAMINATEWVTR(i) = "0"
                            WVTR(i) = "0"
                            PERCENTWVTR(i) = "0"
                        End If

                        '##########################################################################################################################
                        'Tensile Strength 1 Calculation
                        If TS1Pref(i, j) <> "" And TS1Pref(i, j + 1) <> "" Then
                            If TS1Pref(i, j) <> "0" And TS1Pref(i, j + 1) <> "0" Then
                                TS1(i) = (CDbl(TS1Pref(i, j)) * CDbl(BMatPer(i, j))) + (CDbl(TS1Pref(i, j + 1)) * CDbl(BMatPer(i, j + 1)))
                                'calculate percentage
                                TS1P(i, j) = CDbl(TS1Pref(i, j)) / (TS1(i) * 2)
                                TS1P(i, j + 1) = CDbl(TS1Pref(i, j + 1)) / (TS1(i) * 2)
                                'calculate DGrade
                                'If BdgDb(i, j + 1) <> "" And BdgDb(i, j) <> "" Then
                                '    DG1(i) = ((CDbl(TS1Pref(i, j + 1)) * BdgDb(i, j + 1)) + (CDbl(TS1Pref(i, j)) * BdgDb(i, j))) / 2
                                'Else
                                '    If BdgDb(i, j + 1) = "" Then
                                '        DG1(i) = ((CDbl(TS1Pref(i, j + 1)) * 0) + (CDbl(TS1Pref(i, j)) * BdgDb(i, j))) / 2
                                '    ElseIf BdgDb(i, j) = "" Then
                                '        DG1(i) = ((CDbl(TS1Pref(i, j + 1)) * BdgDb(i, j + 1)) + (CDbl(TS1Pref(i, j)) * 0)) / 2
                                '    End If
                                'End If
                                DG1(i) = "0"
                            Else
                                TS1(i) = "0"
                                DG1(i) = "0"
                            End If
                        ElseIf TS1Pref(i, j) <> "" Or TS1Pref(i, j + 1) <> "" Then
                            If TS1Pref(i, j) = "" Then
                                If TS1BLENDSUB(i, j) <> "0" And TS1Pref(i, j + 1) <> "0" Then
                                    TS1(i) = (TS1BLENDSUB(i, j) * CDbl(BMatPer(i, j))) + (TS1Pref(i, j + 1) * CDbl(BMatPer(i, j + 1)))
                                    'calculate percentage
                                    TS1P(i, j) = CDbl(TS1BLENDSUB(i, j)) / (TS1(i) * 2)
                                    TS1P(i, j + 1) = CDbl(TS1Pref(i, j + 1)) / (TS1(i) * 2)
                                    'calculate DGrade
                                    'If BdgDb(i, j + 1) <> "" Then
                                    '    DG1(i) = ((TS1Pref(i, j + 1) * BdgDb(i, j + 1)) + DG1BLENDSUB(i, j)) / 2
                                    'Else
                                    '    DG1(i) = ((TS1Pref(i, j + 1) * 0) + DG1BLENDSUB(i, j)) / 2
                                    'End If
                                    DG1(i) = "0"
                                Else
                                    If TS1BLENDSUB(i, j) <> "0" Then
                                        TS1(i) = TS1BLENDSUB(i, j) * BMatPer(i, j)
                                    ElseIf TS1Pref(i, j + 1) <> "0" Then
                                        TS1(i) = TS1Pref(i, j + 1) * BMatPer(i, j + 1)
                                    Else
                                        TS1(i) = "0"
                                    End If
                                    DG1(i) = "0"
                                End If
                            ElseIf TS1Pref(i, j + 1) = "" Then
                                If TS1Pref(i, j) <> "0" And TS1BLENDSUB(i, j + 1) <> "0" Then
                                    TS1(i) = (TS1BLENDSUB(i, j + 1) * CDbl(BMatPer(i, j + 1))) + (TS1Pref(i, j) * CDbl(BMatPer(i, j)))
                                    'calculate percentage
                                    TS1P(i, j) = CDbl(TS1Pref(i, j)) / (TS1(i) * 2)
                                    TS1P(i, j + 1) = CDbl(TS1BLENDSUB(i, j + 1)) / (TS1(i) * 2)
                                    'calculate DGrade   
                                    'If BdgDb(i, j) <> "" Then
                                    '    DG1(i) = (DG1BLENDSUB(i, j + 1)) + ((TS1Pref(i, j) * BdgDb(i, j))) / 2
                                    'Else
                                    '    DG1(i) = (DG1BLENDSUB(i, j + 1)) + ((TS1Pref(i, j) * 0)) / 2
                                    'End If
                                    DG1(i) = "0"
                                Else
                                    If TS1BLENDSUB(i, j + 1) <> "0" Then
                                        TS1(i) = TS1BLENDSUB(i, j + 1) * BMatPer(i, j + 1)
                                    ElseIf TS1Pref(i, j) <> "0" Then
                                        TS1(i) = TS1Pref(i, j) * BMatPer(i, j)
                                    Else
                                        TS1(i) = "0"
                                    End If
                                    DG1(i) = "0"
                                End If
                            End If
                        ElseIf TS1L(i, j) <> "0" And TS1L(i, j + 1) <> "0" Then
                            TS1(i) = (TS1BLENDSUB(i, j + 1) * CDbl(BMatPer(i, j + 1))) + (TS1BLENDSUB(i, j) * CDbl(BMatPer(i, j)))
                            'calculate percentage
                            TS1P(i, j) = CDbl(TS1BLENDSUB(i, j)) / (TS1(i) * 2)
                            TS1P(i, j + 1) = CDbl(TS1BLENDSUB(i, j + 1)) / (TS1(i) * 2)
                            DG1(i) = (DG1BLENDSUB(i, j + 1) + DG1BLENDSUB(i, j)) / 2
                        Else
                            TS1(i) = "0"
                            DG1(i) = "0"
                        End If

                        'Tensile Strength 2 Calculation
                        If TS2Pref(i, j) <> "" And TS2Pref(i, j + 1) <> "" Then
                            If TS2Pref(i, j) <> "0" And TS2Pref(i, j + 1) <> "0" Then
                                TS2(i) = (CDbl(TS2Pref(i, j)) * CDbl(BMatPer(i, j))) + (CDbl(TS2Pref(i, j + 1)) * CDbl(BMatPer(i, j + 1)))
                                'calculate percentage
                                TS2P(i, j) = CDbl(TS2Pref(i, j)) / (TS2(i) * 2)
                                TS2P(i, j + 1) = CDbl(TS2Pref(i, j + 1)) / (TS2(i) * 2)
                                'calculate DGrade
                                'If BdgDb(i, j + 1) <> "" And BdgDb(i, j) <> "" Then
                                '    DG2(i) = ((CDbl(TS2Pref(i, j + 1)) * BdgDb(i, j + 1)) + (CDbl(TS2Pref(i, j)) * BdgDb(i, j))) / 2
                                'Else
                                '    If BdgDb(i, j + 1) = "" Then
                                '        DG2(i) = ((CDbl(TS2Pref(i, j + 1)) * 0) + (CDbl(TS2Pref(i, j)) * BdgDb(i, j))) / 2
                                '    ElseIf BdgDb(i, j) = "" Then
                                '        DG2(i) = ((CDbl(TS2Pref(i, j + 1)) * BdgDb(i, j + 1)) + (CDbl(TS2Pref(i, j)) * 0)) / 2
                                '    End If
                                'End If
                                DG2(i) = "0"
                            Else
                                TS2(i) = "0"
                                DG2(i) = "0"
                            End If
                        ElseIf TS2Pref(i, j) <> "" Or TS2Pref(i, j + 1) <> "" Then
                            If TS2Pref(i, j) = "" Then
                                If TS2BLENDSUB(i, j) <> "0" And TS2Pref(i, j + 1) <> "0" Then
                                    TS2(i) = (TS2BLENDSUB(i, j) * CDbl(BMatPer(i, j))) + (TS2Pref(i, j + 1) * CDbl(BMatPer(i, j + 1)))
                                    'calculate percentage
                                    TS2P(i, j) = CDbl(TS2BLENDSUB(i, j)) / (TS2(i) * 2)
                                    TS2P(i, j + 1) = CDbl(TS2Pref(i, j + 1)) / (TS2(i) * 2)
                                    'calculate DGrade
                                    'If BdgDb(i, j + 1) <> "" Then
                                    '    DG2(i) = ((TS2Pref(i, j + 1) * BdgDb(i, j + 1)) + DG2BLENDSUB(i, j)) / 2
                                    'Else
                                    '    DG2(i) = ((TS2Pref(i, j + 1) * 0) + DG2BLENDSUB(i, j)) / 2
                                    'End If
                                    DG2(i) = "0"
                                Else
                                    If TS2BLENDSUB(i, j) <> "0" Then
                                        TS2(i) = TS2BLENDSUB(i, j) * BMatPer(i, j)
                                    ElseIf TS2Pref(i, j + 1) <> "0" Then
                                        TS2(i) = TS2Pref(i, j + 1) * BMatPer(i, j + 1)
                                    Else
                                        TS2(i) = "0"
                                    End If
                                    DG2(i) = "0"
                                End If
                            ElseIf TS2Pref(i, j + 1) = "" Then
                                If TS2Pref(i, j) <> "0" And TS2BLENDSUB(i, j + 1) <> "0" Then
                                    TS2(i) = (TS2BLENDSUB(i, j + 1) * CDbl(BMatPer(i, j + 1))) + (TS2Pref(i, j) * CDbl(BMatPer(i, j)))
                                    'calculate percentage
                                    TS2P(i, j) = CDbl(TS2Pref(i, j)) / (TS2(i) * 2)
                                    TS2P(i, j + 1) = CDbl(TS2BLENDSUB(i, j + 1)) / (TS2(i) * 2)
                                    'calculate DGrade  
                                    'If BdgDb(i, j) <> "" Then
                                    '    DG2(i) = (DG2BLENDSUB(i, j + 1)) + ((TS2Pref(i, j) * BdgDb(i, j))) / 2
                                    'Else
                                    '    DG2(i) = (DG2BLENDSUB(i, j + 1)) + ((TS2Pref(i, j) * 0)) / 2
                                    'End If
                                    DG2(i) = "0"
                                Else
                                    If TS2BLENDSUB(i, j + 1) <> "0" Then
                                        TS2(i) = TS2BLENDSUB(i, j + 1) * BMatPer(i, j + 1)
                                    ElseIf TS2Pref(i, j) <> "0" Then
                                        TS2(i) = TS2Pref(i, j) * BMatPer(i, j)
                                    Else
                                        TS2(i) = "0"
                                    End If
                                    DG2(i) = "0"
                                End If
                            End If
                        ElseIf TS2L(i, j) <> "0" And TS2L(i, j + 1) <> "0" Then
                            TS2(i) = (TS2BLENDSUB(i, j + 1) * CDbl(BMatPer(i, j + 1))) + (TS2BLENDSUB(i, j) * CDbl(BMatPer(i, j)))
                            'calculate percentage
                            TS2P(i, j) = CDbl(TS2BLENDSUB(i, j)) / (TS2(i) * 2)
                            TS2P(i, j + 1) = CDbl(TS2BLENDSUB(i, j + 1)) / (TS2(i) * 2)
                            DG2(i) = (DG2BLENDSUB(i, j + 1) + DG2BLENDSUB(i, j)) / 2
                        Else
                            TS2(i) = "0"
                            DG2(i) = "0"
                        End If
                    Next
                Next

                StrSql = "SELECT LOWPERM, HIGHPERM, FACTOR FROM PENALTYFACTOR WHERE LOWPERM IN (" + FormatNumber(PERCENTOTR(0), 0) + "," + FormatNumber(PERCENTOTR(1), 0) + "," + FormatNumber(PERCENTOTR(2), 0) + ","
                StrSql = StrSql + FormatNumber(PERCENTOTR(3), 0) + "," + FormatNumber(PERCENTOTR(4), 0) + "," + FormatNumber(PERCENTOTR(5), 0) + "," + FormatNumber(PERCENTOTR(6), 0) + "," + FormatNumber(PERCENTOTR(7), 0) + "," + FormatNumber(PERCENTOTR(8), 0) + ","
                StrSql = StrSql + FormatNumber(PERCENTOTR(9), 0) + ")"
                DtsOTR = odbUtil.FillDataSet(StrSql, SBAConnection)

                StrSql1 = "SELECT LOWPERM, HIGHPERM, FACTOR FROM PENALTYFACTOR WHERE LOWPERM IN (" + FormatNumber(PERCENTWVTR(0), 0) + "," + FormatNumber(PERCENTWVTR(1), 0) + "," + FormatNumber(PERCENTWVTR(2), 0) + ","
                StrSql1 = StrSql1 + FormatNumber(PERCENTWVTR(3), 0) + "," + FormatNumber(PERCENTWVTR(4), 0) + "," + FormatNumber(PERCENTWVTR(5), 0) + "," + FormatNumber(PERCENTWVTR(6), 0) + "," + FormatNumber(PERCENTWVTR(7), 0) + "," + FormatNumber(PERCENTWVTR(8), 0) + ","
                StrSql1 = StrSql1 + FormatNumber(PERCENTWVTR(9), 0) + ")"
                DtsWVTR = odbUtil.FillDataSet(StrSql1, SBAConnection)

                If DtsOTR.Tables(0).Rows.Count > 0 Then
                    dvBar = DtsOTR.Tables(0).DefaultView()
                    dvBarr = DtsWVTR.Tables(0).DefaultView()
                    For i = 0 To 9
                        If OTR(i) <> "0" Then
                            dvBar.RowFilter = "LOWPERM = " + FormatNumber(PERCENTOTR(i), 0)
                            dtBar = dvBar.ToTable()
                            If dtBar.Rows.Count > 0 Then
                                OTR(i) = (LAMINATEOTR(i) + (LAMINATEOTR(i) * dtBar.Rows(0).Item("FACTOR").ToString()))
                            Else
                                OTR(i) = " 0"
                            End If
                        End If
                        If DtsWVTR.Tables(0).Rows.Count > 0 Then
                            If WVTR(i) <> "0" Then
                                dvBarr.RowFilter = "LOWPERM = " + FormatNumber(PERCENTWVTR(i), 0)
                                dtBarr = dvBarr.ToTable()
                                If dtBarr.Rows.Count > 0 Then
                                    WVTR(i) = (LAMINATEWVTR(i) + (LAMINATEWVTR(i) * dtBarr.Rows(0).Item("FACTOR").ToString()))
                                Else
                                    WVTR(i) = " 0"
                                End If
                            End If
                        End If
                    Next
                End If

                'TotalOTR = "0"
                For i = 0 To 9
                    If MATERIAL(i) <> "0" Then
                        If MATERIAL(i) > 500 And MATERIAL(i) < 506 Then
                            If OTR(i) <> 0 Then
                                If Not IsNumeric(otrDb(i)) Then
                                    TotalOTRBLEND = CDbl(TotalOTRBLEND) + (1 / CDbl(OTR(i)))
                                Else
                                    'If (Ds.Tables(0).Rows(0).Item("OTR" + (i + 1).ToString())) <> 0 Then
                                    TotalOTRBLEND = CDbl(TotalOTRBLEND) + (1 / otrDb(i))
                                    'End If
                                End If
                            Else
                                If IsNumeric(otrDb(i)) Then
                                    TotalOTRBLEND = CDbl(TotalOTRBLEND) + (1 / otrDb(i))
                                Else
                                    TotalOTRBLEND = CDbl(TotalOTRBLEND) + (1 / CDbl(OTR(i)))
                                End If

                            End If
                        Else
                            If OTRB(i) <> 0 Then
                                If Not IsNumeric(otrDb(i)) Then
                                    TotalOTRBLEND = CDbl(TotalOTRBLEND) + (1 / CDbl(OTRB(i)))
                                Else
                                    'If (Ds.Tables(0).Rows(0).Item("OTR" + (i + 1).ToString())) <> 0 Then
                                    TotalOTRBLEND = CDbl(TotalOTRBLEND) + (1 / otrDb(i))
                                    'End If
                                End If
                            Else
                                If IsNumeric(otrDb(i)) Then
                                    TotalOTRBLEND = CDbl(TotalOTRBLEND) + (1 / otrDb(i))
                                Else
                                    TotalOTRBLEND = CDbl(TotalOTRBLEND) + (1 / CDbl(OTRB(i)))
                                End If

                            End If
                        End If
                    End If
                Next
                If TotalOTRBLEND <> "0" Then
                    TotalOTRBLEND = 1 / CDbl(TotalOTRBLEND)
                End If


                'TotalWVTR 
                For i = 0 To 9
                    If MATERIAL(i) <> 0 Then
                        If MATERIAL(i) > 500 And MATERIAL(i) < 506 Then
                            If WVTR(i) <> 0 Then
                                If Not IsNumeric(wvtrDb(i)) Then
                                    TotalWVTRBLEND = CDbl(TotalWVTRBLEND) + (1 / CDbl(WVTR(i)))
                                Else
                                    'If (Ds.Tables(0).Rows(0).Item("WVTR" + (i + 1).ToString())) <> 0 Then
                                    TotalWVTRBLEND = CDbl(TotalWVTRBLEND) + (1 / CDbl(wvtrDb(i)))
                                    'End If

                                End If
                            Else
                                If IsNumeric(wvtrDb(i)) Then
                                    'If (Ds.Tables(0).Rows(0).Item("WVTR" + (i + 1).ToString())) <> 0 Then
                                    TotalWVTRBLEND = CDbl(TotalWVTRBLEND) + (1 / CDbl(wvtrDb(i)))
                                Else
                                    TotalWVTRBLEND = CDbl(TotalWVTRBLEND) + (1 / CDbl(WVTR(i)))
                                    'End If
                                End If
                            End If
                        Else
                            If WVTRB(i) <> 0 Then
                                If Not IsNumeric(wvtrDb(i)) Then
                                    TotalWVTRBLEND = CDbl(TotalWVTRBLEND) + (1 / CDbl(WVTRB(i)))
                                Else
                                    'If (Ds.Tables(0).Rows(0).Item("WVTR" + (i + 1).ToString())) <> 0 Then
                                    TotalWVTRBLEND = CDbl(TotalWVTRBLEND) + (1 / CDbl(wvtrDb(i)))
                                    'End If

                                End If
                            Else
                                If IsNumeric(wvtrDb(i)) Then
                                    'If (Ds.Tables(0).Rows(0).Item("WVTR" + (i + 1).ToString())) <> 0 Then
                                    TotalWVTRBLEND = CDbl(TotalWVTRBLEND) + (1 / CDbl(wvtrDb(i)))
                                Else
                                    TotalWVTRBLEND = CDbl(TotalWVTRBLEND) + (1 / CDbl(WVTRB(i)))
                                    'End If
                                End If
                            End If
                        End If
                    End If
                Next
                If TotalWVTRBLEND <> "0" Then
                    TotalWVTRBLEND = 1 / CDbl(TotalWVTRBLEND)
                End If

                'Total Tensile Strength 1
                'count = 0
                For i = 0 To 9
                    If MATERIAL(i) <> "0" Then
                        'count = count + 1
                        If MATERIAL(i) > 500 And MATERIAL(i) < 506 Then
                            If TS1(i) <> "" Then
                                If Not IsNumeric(ts1Db(i)) Then
                                    TotalTS1BLEND = CDbl(TotalTS1BLEND) + (CDbl(TS1(i)) * CDbl(MatPer(i)))
                                    'calculate DGrade
                                    TotalDG1BLEND = CDbl(TotalDG1BLEND) + CDbl(DG1(i))
                                Else
                                    TotalTS1BLEND = CDbl(TotalTS1BLEND) + (ts1Db(i) * CDbl(MatPer(i)))
                                    'calculate DGrade
                                    If BdgDb(i, 0) <> "" Then
                                        If BdgDb(i, 1) <> "" Then
                                            TotalDG1BLEND = CDbl(TotalDG1BLEND) + ((ts1Db(i) * TS1P(i, 0) * BdgDb(i, 0)) + (ts1Db(i) * TS1P(i, 1) * BdgDb(i, 1))) / 2
                                        Else
                                            TotalDG1BLEND = CDbl(TotalDG1BLEND) + ((ts1Db(i) * TS1P(i, 0) * BdgDb(i, 0)) + (ts1Db(i) * TS1P(i, 1) * 0)) / 2
                                        End If
                                    Else
                                        If BdgDb(i, 1) <> "" Then
                                            TotalDG1BLEND = CDbl(TotalDG1BLEND) + ((ts1Db(i) * TS1P(i, 0) * 0) + (ts1Db(i) * TS1P(i, 1) * BdgDb(i, 1))) / 2
                                        Else
                                            TotalDG1BLEND = CDbl(TotalDG1BLEND) + ((ts1Db(i) * TS1P(i, 0) * 0) + (ts1Db(i) * TS1P(i, 1) * 0)) / 2
                                        End If
                                    End If
                                End If
                            Else
                                If IsNumeric(ts1Db(i)) Then
                                    TotalTS1BLEND = CDbl(TotalTS1BLEND) + (ts1Db(i) * CDbl(MatPer(i)))
                                    'calculate DGrade
                                    If BdgDb(i, 0) <> "" Then
                                        If BdgDb(i, 1) <> "" Then
                                            TotalDG1BLEND = CDbl(TotalDG1BLEND) + ((ts1Db(i) * TS1P(i, 0) * BdgDb(i, 0)) + (ts1Db(i) * TS1P(i, 1) * BdgDb(i, 1))) / 2
                                        Else
                                            TotalDG1BLEND = CDbl(TotalDG1BLEND) + ((ts1Db(i) * TS1P(i, 0) * BdgDb(i, 0)) + (ts1Db(i) * TS1P(i, 1) * 0)) / 2
                                        End If
                                    Else
                                        If BdgDb(i, 1) <> "" Then
                                            TotalDG1BLEND = CDbl(TotalDG1BLEND) + ((ts1Db(i) * TS1P(i, 0) * 0) + (ts1Db(i) * TS1P(i, 1) * BdgDb(i, 1))) / 2
                                        Else
                                            TotalDG1BLEND = CDbl(TotalDG1BLEND) + ((ts1Db(i) * TS1P(i, 0) * 0) + (ts1Db(i) * TS1P(i, 1) * 0)) / 2
                                        End If
                                    End If
                                    'TotalDG1BLEND = CDbl(TotalDG1BLEND) + ((ts1Db(i) * TS1P(i, 0) * BdgDb(i, 0)) + (ts1Db(i) * TS1P(i, 1) * BdgDb(i, 1))) / 2
                                Else
                                    If TS1(i) <> "" Then
                                        TotalTS1BLEND = CDbl(TotalTS1BLEND) + (CDbl(TS1(i)) * CDbl(MatPer(i)))
                                        'calculate DGrade
                                        TotalDG1BLEND = CDbl(TotalDG1BLEND) + CDbl(DG1(i))
                                    End If
                                End If
                            End If
                        Else
                            If TS1B(i) <> "" Then
                                If Not IsNumeric(ts1Db(i)) Then
                                    TotalTS1BLEND = CDbl(TotalTS1BLEND) + (CDbl(TS1B(i)) * CDbl(MatPer(i)))
                                    'calculate DGrade
                                    If dgDb(i) <> "" Then
                                        TotalDG1BLEND = CDbl(TotalDG1BLEND) + (CDbl(TS1B(i)) * CDbl(dgDb(i)))
                                    Else
                                        TotalDG1BLEND = CDbl(TotalDG1BLEND) + (CDbl(TS1B(i)) * 0)
                                    End If
                                Else
                                    TotalTS1BLEND = CDbl(TotalTS1BLEND) + (ts1Db(i) * CDbl(MatPer(i)))
                                    'calculate DGrade
                                    If dgDb(i) <> "" Then
                                        TotalDG1BLEND = CDbl(TotalDG1BLEND) + (CDbl(ts1Db(i)) * CDbl(dgDb(i)))
                                    Else
                                        TotalDG1BLEND = CDbl(TotalDG1BLEND) + (CDbl(ts1Db(i)) * 0)
                                    End If
                                End If
                            Else
                                If IsNumeric(ts1Db(i)) Then
                                    TotalTS1BLEND = CDbl(TotalTS1BLEND) + (ts1Db(i) * CDbl(MatPer(i)))
                                    'calculate DGrade
                                    If dgDb(i) <> "" Then
                                        TotalDG1BLEND = CDbl(TotalDG1BLEND) + (CDbl(ts1Db(i)) * CDbl(dgDb(i)))
                                    Else
                                        TotalDG1BLEND = CDbl(TotalDG1BLEND) + (CDbl(ts1Db(i)) * 0)
                                    End If
                                Else
                                    If TS1B(i) <> "" Then
                                        TotalTS1BLEND = CDbl(TotalTS1BLEND) + (CDbl(TS1B(i)) * CDbl(MatPer(i)))
                                        'calculate DGrade
                                        If dgDb(i) <> "" Then
                                            TotalDG1BLEND = CDbl(TotalDG1BLEND) + (CDbl(TS1B(i)) * CDbl(dgDb(i)))
                                        Else
                                            TotalDG1BLEND = CDbl(TotalDG1BLEND) + (CDbl(TS1B(i)) * 0)
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
                'If TotalTS1BLEND <> "0" Then
                '    TotalTS1BLEND = CDbl(TotalTS1BLEND) / count
                'End If

                'If TotalDG1BLEND <> "0" Then
                '    TotalDG1BLEND = CDbl(TotalDG1BLEND) / count
                'End If

                'Total Tensile Strength 2
                'count = 0
                For i = 0 To 9
                    If MATERIAL(i) <> "0" Then
                        'count = count + 1
                        If MATERIAL(i) > 500 And MATERIAL(i) < 506 Then
                            If TS2(i) <> "" Then
                                If Not IsNumeric(ts2Db(i)) Then
                                    TotalTS2BLEND = CDbl(TotalTS2BLEND) + (CDbl(TS2(i)) * CDbl(MatPer(i)))
                                    'calculate DGrade
                                    TotalDG2BLEND = CDbl(TotalDG2BLEND) + CDbl(DG2(i))
                                Else
                                    TotalTS2BLEND = CDbl(TotalTS2BLEND) + (ts2Db(i) * CDbl(MatPer(i)))
                                    'calculate DGrade
                                    If BdgDb(i, 0) <> "" Then
                                        If BdgDb(i, 1) <> "" Then
                                            TotalDG2BLEND = CDbl(TotalDG2BLEND) + ((ts2Db(i) * TS2P(i, 0) * BdgDb(i, 0)) + (ts2Db(i) * TS2P(i, 1) * BdgDb(i, 1))) / 2
                                        Else
                                            TotalDG2BLEND = CDbl(TotalDG2BLEND) + ((ts2Db(i) * TS2P(i, 0) * BdgDb(i, 0)) + (ts2Db(i) * TS2P(i, 1) * 0)) / 2
                                        End If
                                    Else
                                        If BdgDb(i, 1) <> "" Then
                                            TotalDG2BLEND = CDbl(TotalDG2BLEND) + ((ts2Db(i) * TS2P(i, 0) * 0) + (ts2Db(i) * TS2P(i, 1) * BdgDb(i, 1))) / 2
                                        Else
                                            TotalDG2BLEND = CDbl(TotalDG2BLEND) + ((ts2Db(i) * TS2P(i, 0) * 0) + (ts2Db(i) * TS2P(i, 1) * 0)) / 2
                                        End If
                                    End If
                                End If
                            Else
                                If IsNumeric(ts2Db(i)) Then
                                    TotalTS2BLEND = CDbl(TotalTS2BLEND) + (ts2Db(i) * CDbl(MatPer(i)))
                                    'calculate DGrade
                                    If BdgDb(i, 0) <> "" Then
                                        If BdgDb(i, 1) <> "" Then
                                            TotalDG2BLEND = CDbl(TotalDG2BLEND) + ((ts2Db(i) * TS2P(i, 0) * BdgDb(i, 0)) + (ts2Db(i) * TS2P(i, 1) * BdgDb(i, 1))) / 2
                                        Else
                                            TotalDG2BLEND = CDbl(TotalDG2BLEND) + ((ts2Db(i) * TS2P(i, 0) * BdgDb(i, 0)) + (ts2Db(i) * TS2P(i, 1) * 0)) / 2
                                        End If
                                    Else
                                        If BdgDb(i, 1) <> "" Then
                                            TotalDG2BLEND = CDbl(TotalDG2BLEND) + ((ts2Db(i) * TS2P(i, 0) * 0) + (ts2Db(i) * TS2P(i, 1) * BdgDb(i, 1))) / 2
                                        Else
                                            TotalDG2BLEND = CDbl(TotalDG2BLEND) + ((ts2Db(i) * TS2P(i, 0) * 0) + (ts2Db(i) * TS2P(i, 1) * 0)) / 2
                                        End If
                                    End If

                                Else
                                    If TS2(i) <> "" Then
                                        TotalTS2BLEND = CDbl(TotalTS2BLEND) + (CDbl(TS2(i)) * CDbl(MatPer(i)))
                                        'calculate DGrade
                                        TotalDG2BLEND = CDbl(TotalDG2BLEND) + CDbl(DG2(i))
                                    End If
                                End If
                            End If
                        Else
                            If TS2B(i) <> "" Then
                                If Not IsNumeric(ts2Db(i)) Then
                                    TotalTS2BLEND = CDbl(TotalTS2BLEND) + (CDbl(TS2B(i)) * CDbl(MatPer(i)))
                                    'calculate DGrade
                                    If dgDb(i) <> "" Then
                                        TotalDG2BLEND = CDbl(TotalDG2BLEND) + (CDbl(TS2B(i)) * CDbl(dgDb(i)))
                                    Else
                                        TotalDG2BLEND = CDbl(TotalDG2BLEND) + (CDbl(TS2B(i)) * 0)
                                    End If
                                Else
                                    TotalTS2BLEND = CDbl(TotalTS2BLEND) + (ts2Db(i) * CDbl(MatPer(i)))
                                    'calculate DGrade
                                    If dgDb(i) <> "" Then
                                        TotalDG2BLEND = CDbl(TotalDG2BLEND) + (CDbl(ts2Db(i)) * CDbl(dgDb(i)))
                                    Else
                                        TotalDG2BLEND = CDbl(TotalDG2BLEND) + (CDbl(ts2Db(i)) * 0)
                                    End If

                                End If
                            Else
                                If IsNumeric(ts2Db(i)) Then
                                    TotalTS2BLEND = CDbl(TotalTS2BLEND) + (ts2Db(i) * CDbl(MatPer(i)))
                                    'calculate DGrade
                                    If dgDb(i) <> "" Then
                                        TotalDG2BLEND = CDbl(TotalDG2BLEND) + (CDbl(ts2Db(i)) * CDbl(dgDb(i)))
                                    Else
                                        TotalDG2BLEND = CDbl(TotalDG2BLEND) + (CDbl(ts2Db(i)) * 0)
                                    End If

                                Else
                                    If TS2B(i) <> "" Then
                                        TotalTS2BLEND = CDbl(TotalTS2BLEND) + (CDbl(TS2B(i)) * CDbl(MatPer(i)))
                                        'calculate DGrade
                                        If dgDb(i) <> "" Then
                                            TotalDG2BLEND = CDbl(TotalDG2BLEND) + (CDbl(TS2B(i)) * CDbl(dgDb(i)))
                                        Else
                                            TotalDG2BLEND = CDbl(TotalDG2BLEND) + (CDbl(TS2B(i)) * 0)
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
                'If TotalTS2BLEND <> "0" Then
                '    TotalTS2BLEND = CDbl(TotalTS2BLEND) / count
                'End If

                'If TotalDG2BLEND <> "0" Then
                '    TotalDG2BLEND = CDbl(TotalDG2BLEND) / count
                'End If

            Catch ex As Exception

            End Try
        End Sub

        Public Sub BarrierPropCalBlendSubLayersOTR(ByVal CaseID As Integer, ByVal Temp As String, ByVal RH() As String, ByVal MAT(,) As String, ByVal EDate As Date, ByVal MatDes(,) As String, ByVal OtrPref(,) As String, _
                                    ByVal Thick(,) As String, ByVal ISADJTHICK(,) As String, ByVal ThickPer(,) As String, ByVal Dts As DataSet)
            Try
                Dim StrSql As String = String.Empty
                Dim StrSql1 As String = String.Empty
                'Dim Dts As New DataSet()
                Dim dvBarr As New DataView
                Dim dtBar As New DataTable
                Dim odbUtil As New DBUtil()
                Dim OTRFlag As String
                Dim dvBar As New DataView
                Dim dsBar As New DataSet()
                OTRMsgSub = ""
                MsgSub = ""

                Dim OTRFlagVal As String = ""
                Dim TempRhVal As String = ""
                If Dts.Tables(0).Rows.Count > 0 Then
                    For i = 0 To 9
                        'Getting OTR Flag and Values
                        OTRFlag = GetOTRFlag(Temp, RH(i))
                        Dim strSpl() As String
                        strSpl = Regex.Split(OTRFlag, "-")
                        If strSpl.Length > 0 Then
                            OTRFlagVal = strSpl(0)
                            TempRhVal = strSpl(1)
                        End If

                        If OTRFlagVal = "1" Then
                            Dim tempId, rhId As String
                            Dim SplFlag1() As String
                            SplFlag1 = Regex.Split(TempRhVal, "##")
                            If SplFlag1.Length > 0 Then
                                tempId = SplFlag1(0)
                                rhId = SplFlag1(1)
                                For j = 0 To 1
                                    dvBar = Dts.Tables(0).DefaultView
                                    Try
                                        dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempId + " AND RHID=" + rhId
                                        dtBar = dvBar.ToTable()
                                        If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                            OTRSUB(i, j) = "10000"
                                            OTRLOW(i, j) = "10000"
                                            If Not OTRMsgSub.Contains(MatDes(i, j)) Then
                                                OTRMsgSub = OTRMsgSub + "" + MatDes(i, j) + " (OTR)\n"
                                            End If
                                        Else
                                            OTRSUB(i, j) = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                            OTRLOW(i, j) = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                        End If
                                        If ISADJTHICK(i, j) <> "N" Then
                                            'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                                            OTRSUB(i, j) = CDbl((OTRSUB(i, j) / CDbl(Thick(i, j))) * 25.4)
                                        End If
                                    Catch ex As Exception
                                        OTRSUB(i, j) = "0"
                                        OTRLOW(i, j) = "0"
                                    End Try
                                Next
                            End If
                        ElseIf OTRFlagVal = "3" Then
                            Dim tempId, rhIdL, rhIdH, rhValL, rhValH As String
                            Dim SplFlag1() As String
                            Dim SplFlag2() As String
                            Dim SplFlag3() As String
                            SplFlag1 = Regex.Split(TempRhVal, "##")
                            If SplFlag1.Length > 0 Then
                                tempId = SplFlag1(0)
                                SplFlag2 = Regex.Split(SplFlag1(1), "#")
                                SplFlag3 = Regex.Split(SplFlag1(2), "#")
                                If SplFlag2.Length > 0 Then
                                    rhIdL = SplFlag2(1)
                                    rhValL = SplFlag2(0)
                                    If SplFlag3.Length > 0 Then
                                        rhIdH = SplFlag3(1)
                                        rhValH = SplFlag3(0)
                                        For j = 0 To 1
                                            dvBar = Dts.Tables(0).DefaultView
                                            Try
                                                Dim lowRh As String = ""
                                                Dim highRh As String = ""
                                                Dim cngCounter As Integer = 0
                                                Try
                                                    'Getting lower RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdL
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                        lowRh = "10000"
                                                        If Not OTRMsgSub.Contains(MatDes(i, j)) Then
                                                            OTRMsgSub = OTRMsgSub + "" + MatDes(i, j) + " (OTR)\n"
                                                        End If
                                                        cngCounter = 1
                                                    Else
                                                        lowRh = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                    End If
                                                    'Getting higher RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdH
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                        highRh = "10000"
                                                        If cngCounter <> 1 Then
                                                            If Not OTRMsgSub.Contains(MatDes(i, j)) Then
                                                                OTRMsgSub = OTRMsgSub + "" + MatDes(i, j) + " (OTR)\n"
                                                            End If
                                                            cngCounter = 1
                                                        End If
                                                    Else
                                                        highRh = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                    End If
                                                    OTRSUB(i, j) = CDbl(lowRh) + ((RH(i) - rhValL) / (rhValH - rhValL) * (CDbl(highRh) - CDbl(lowRh)))
                                                    OTRLOW(i, j) = CDbl(lowRh) + ((RH(i) - rhValL) / (rhValH - rhValL) * (CDbl(highRh) - CDbl(lowRh)))
                                                    If ISADJTHICK(i, j) <> "N" Then
                                                        'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                                                        OTRSUB(i, j) = CDbl((OTRSUB(i, j) / CDbl(Thick(i, j))) * 25.4)
                                                    End If
                                                Catch ex As Exception
                                                    OTRSUB(i, j) = "0"
                                                    OTRLOW(i, j) = "0"
                                                End Try
                                            Catch ex As Exception

                                            End Try
                                        Next
                                    End If
                                End If
                            End If
                        ElseIf OTRFlagVal = "4" Then
                            Dim rhId, tempIdL, tempIdH, tempValL, tempValH As String
                            Dim SplFlag1() As String
                            Dim SplFlag2() As String
                            Dim SplFlag3() As String

                            SplFlag1 = Regex.Split(TempRhVal, "##")
                            If SplFlag1.Length > 0 Then
                                rhId = SplFlag1(0)
                                SplFlag2 = Regex.Split(SplFlag1(1), "#")
                                SplFlag3 = Regex.Split(SplFlag1(2), "#")
                                If SplFlag2.Length > 0 Then
                                    tempIdL = SplFlag2(1)
                                    tempValL = SplFlag2(0)
                                    If SplFlag3.Length > 0 Then
                                        tempIdH = SplFlag3(1)
                                        tempValH = SplFlag3(0)
                                        For j = 0 To 1
                                            dvBar = Dts.Tables(0).DefaultView
                                            Try
                                                Dim lowTemp As String = ""
                                                Dim highTemp As String = ""
                                                Dim cngCounter As Integer = 0

                                                Try
                                                    'Getting lower RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhId
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                        lowTemp = "10000"
                                                        If Not OTRMsgSub.Contains(MatDes(i, j)) Then
                                                            OTRMsgSub = OTRMsgSub + "" + MatDes(i, j) + " (OTR)\n"
                                                        End If
                                                        cngCounter = 1
                                                    Else
                                                        lowTemp = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                    End If

                                                    'Getting higher RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhId
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                        highTemp = "10000"
                                                        If cngCounter <> 1 Then
                                                            If Not OTRMsgSub.Contains(MatDes(i, j)) Then
                                                                OTRMsgSub = OTRMsgSub + "" + MatDes(i, j) + " (OTR)\n"
                                                            End If
                                                            cngCounter = 1
                                                        End If
                                                    Else
                                                        highTemp = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                    End If
                                                    OTRSUB(i, j) = CDbl(lowTemp) + ((Temp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))
                                                    OTRLOW(i, j) = CDbl(lowTemp) + ((Temp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))
                                                    If ISADJTHICK(i, j) <> "N" Then
                                                        'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                                                        OTRSUB(i, j) = CDbl((OTRSUB(i, j) / CDbl(Thick(i, j))) * 25.4)
                                                    End If
                                                Catch ex As Exception
                                                    OTRSUB(i, j) = "0"
                                                    OTRLOW(i, j) = "0"
                                                End Try

                                            Catch ex As Exception

                                            End Try
                                        Next
                                    End If

                                End If

                            End If
                        ElseIf OTRFlagVal = "2" Then
                            Dim tempIdL, tempIdH, tempValL, tempValH As String
                            Dim rhIdL, rhIdH, rhValL, rhValH As String


                            Dim SplFlag1() As String
                            Dim SplFlag2() As String
                            Dim SplFlag3() As String

                            Dim SplFlag4() As String
                            Dim SplFlag5() As String

                            SplFlag1 = Regex.Split(TempRhVal, "##")
                            If SplFlag1.Length > 0 Then

                                SplFlag2 = Regex.Split(SplFlag1(0), "#")
                                SplFlag3 = Regex.Split(SplFlag1(1), "#")

                                SplFlag4 = Regex.Split(SplFlag1(2), "#")
                                SplFlag5 = Regex.Split(SplFlag1(3), "#")

                                If SplFlag2.Length > 0 Then
                                    tempIdL = SplFlag2(1)
                                    tempValL = SplFlag2(0)
                                    If SplFlag3.Length > 0 Then
                                        tempIdH = SplFlag3(1)
                                        tempValH = SplFlag3(0)
                                        If SplFlag4.Length > 0 Then
                                            rhIdL = SplFlag4(1)
                                            rhValL = SplFlag4(0)
                                            If SplFlag5.Length > 0 Then
                                                rhIdH = SplFlag5(1)
                                                rhValH = SplFlag5(0)
                                                For j = 0 To 1
                                                    dvBar = Dts.Tables(0).DefaultView
                                                    Try
                                                        Dim tempLrhL, tempHrhH, tempLrhH, tempHrhL As String
                                                        Dim MidTemp1 As String
                                                        Dim MidTemp2 As String
                                                        Dim cngCounter As Integer = 0

                                                        Try
                                                            'Getting lower temp lower Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdL
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                                tempLrhL = "10000"
                                                                If Not OTRMsgSub.Contains(MatDes(i, j)) Then
                                                                    OTRMsgSub = OTRMsgSub + "" + MatDes(i, j) + " (OTR)\n"
                                                                End If
                                                                cngCounter = 1
                                                            Else
                                                                tempLrhL = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                            End If


                                                            'Getting higher temp higher Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdH
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                                tempHrhH = "10000"
                                                                If cngCounter <> 1 Then
                                                                    If Not OTRMsgSub.Contains(MatDes(i, j)) Then
                                                                        OTRMsgSub = OTRMsgSub + "" + MatDes(i, j) + " (OTR)\n"
                                                                    End If
                                                                    cngCounter = 1
                                                                End If
                                                            Else
                                                                tempHrhH = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                            End If


                                                            'Getting lower temp Higher Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdH
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                                tempLrhH = "10000"
                                                                If cngCounter <> 1 Then
                                                                    If Not OTRMsgSub.Contains(MatDes(i, j)) Then
                                                                        OTRMsgSub = OTRMsgSub + "" + MatDes(i, j) + " (OTR)\n"
                                                                    End If
                                                                    cngCounter = 1
                                                                End If
                                                            Else
                                                                tempLrhH = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                            End If


                                                            'Getting higher temp lower Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdL
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("OTRVALUE").ToString() = "" Then
                                                                tempHrhL = "10000"
                                                                If cngCounter <> 1 Then
                                                                    If Not OTRMsgSub.Contains(MatDes(i, j)) Then
                                                                        OTRMsgSub = OTRMsgSub + "" + MatDes(i, j) + " (OTR)\n"
                                                                    End If
                                                                    cngCounter = 1
                                                                End If
                                                            Else
                                                                tempHrhL = dtBar.Rows(0).Item("OTRVALUE").ToString()
                                                            End If

                                                            MidTemp1 = CDbl(tempLrhL) + (CDbl(Temp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhL) - CDbl(CDbl(tempLrhL)))))
                                                            MidTemp2 = CDbl(tempLrhH) + (CDbl(Temp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhH) - CDbl(CDbl(tempLrhH)))))
                                                            OTRSUB(i, j) = MidTemp1 + ((RH(i) - rhValL) / (rhValH - rhValL) * (MidTemp2 - MidTemp1))
                                                            OTRLOW(i, j) = MidTemp1 + ((RH(i) - rhValL) / (rhValH - rhValL) * (MidTemp2 - MidTemp1))
                                                            If ISADJTHICK(i, j) <> "N" Then
                                                                'OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
                                                                OTRSUB(i, j) = CDbl((OTRSUB(i, j) / CDbl(Thick(i, j))) * 25.4)
                                                            End If
                                                        Catch ex As Exception
                                                            OTRSUB(i, j) = "0"
                                                            OTRLOW(i, j) = "0"
                                                        End Try

                                                    Catch ex As Exception

                                                    End Try
                                                Next
                                            End If
                                        End If
                                    End If

                                End If

                            End If
                        End If
                    Next
                    If OTRMsgSub <> "" Then
                        MsgSub = "Data is not present for following materials for Date " + CDate(EDate).Date + "\n"
                        If OTRMsgSub <> "" Then
                            MsgSub = MsgSub + OTRMsgSub + ""
                        End If
                    End If

                    If Dts.Tables(0).Rows.Count > 0 Then
                        BarrierPropCalBlendedOTR(OtrPref, ThickPer, MAT, Thick)
                    End If

                End If
            Catch ex As Exception

            End Try
        End Sub

        Public Sub BarrierPropCalBlendedOTR(ByVal OtrPref(,) As String, ByVal ThickPer(,) As String, ByVal MAT(,) As String, ByVal Thick(,) As String)
            Try
                Dim StrSql As String = String.Empty
                Dim StrSql1 As String = String.Empty
                Dim DtsOTR As New DataSet()
                Dim PERCENTOTR(9) As String
                Dim LAMINATEOTR(9) As String
                Dim odbUtil As New DBUtil()
                Dim dvBar As New DataView
                Dim dtBar As New DataTable
                Dim OTR1 As Double
                Dim OTR2 As Double

                For i = 0 To 9
                    For j = 0 To 1 - 1
                        If OtrPref(i, j) <> "" And OtrPref(i, j + 1) <> "" Then
                            If OtrPref(i, j) <> "0" And OtrPref(i, j + 1) <> "0" Then
                                LAMINATEOTR(i) = (1 / ((1 / OtrPref(i, j + 1)) + (1 / OtrPref(i, j))))
                                OTR1 = CDbl(OtrPref(i, j) * Thick(i, j) / 25.4)
                                OTR2 = CDbl(OtrPref(i, j + 1) * Thick(i, j + 1) / 25.4)
                                If OTR1 < OTR2 Then
                                    PERCENTOTR(i) = ThickPer(i, j)
                                ElseIf OTR2 < OTR1 Then
                                    PERCENTOTR(i) = ThickPer(i, j + 1)
                                Else
                                    PERCENTOTR(i) = ThickPer(i, j)
                                End If
                            Else
                                LAMINATEOTR(i) = "0"
                                OTRRHB(i) = "0"
                                PERCENTOTR(i) = "0"
                            End If
                        ElseIf OtrPref(i, j) <> "" Or OtrPref(i, j + 1) <> "" Then
                            If OtrPref(i, j) = "" Then
                                If OTRSUB(i, j) <> "0" And OtrPref(i, j + 1) <> "0" Then
                                    LAMINATEOTR(i) = (1 / ((1 / OtrPref(i, j + 1)) + (1 / OTRSUB(i, j))))
                                    OTR2 = CDbl(OtrPref(i, j + 1) * Thick(i, j + 1) / 25.4)
                                    If OTRLOW(i, j) < OTR2 Then
                                        PERCENTOTR(i) = ThickPer(i, j)
                                    ElseIf OTR2 < OTRLOW(i, j) Then
                                        PERCENTOTR(i) = ThickPer(i, j + 1)
                                    Else
                                        PERCENTOTR(i) = ThickPer(i, j)
                                    End If
                                Else
                                    LAMINATEOTR(i) = "0"
                                    OTRRHB(i) = "0"
                                    PERCENTOTR(i) = "0"
                                End If
                            ElseIf OtrPref(i, j + 1) = "" Then
                                If OtrPref(i, j) <> "0" And OTRSUB(i, j + 1) <> "0" Then
                                    LAMINATEOTR(i) = (1 / ((1 / OTRSUB(i, j + 1)) + (1 / OtrPref(i, j))))
                                    OTR1 = CDbl(OtrPref(i, j) * Thick(i, j) / 25.4)
                                    If OTR1 < OTRLOW(i, j + 1) Then
                                        PERCENTOTR(i) = ThickPer(i, j)
                                    ElseIf OTRLOW(i, j + 1) < OTR1 Then
                                        PERCENTOTR(i) = ThickPer(i, j + 1)
                                    Else
                                        PERCENTOTR(i) = ThickPer(i, j)
                                    End If
                                Else
                                    LAMINATEOTR(i) = "0"
                                    OTRRHB(i) = "0"
                                    PERCENTOTR(i) = "0"
                                End If
                            End If
                        ElseIf OTRLOW(i, j) <> "0" And OTRLOW(i, j + 1) <> "0" Then
                            LAMINATEOTR(i) = (1 / ((1 / OTRSUB(i, j + 1)) + (1 / OTRSUB(i, j))))
                            If OTRLOW(i, j) < OTRLOW(i, j + 1) Then
                                PERCENTOTR(i) = ThickPer(i, j)
                            ElseIf OTRLOW(i, j + 1) < OTRLOW(i, j) Then
                                PERCENTOTR(i) = ThickPer(i, j + 1)
                            Else
                                PERCENTOTR(i) = ThickPer(i, j)
                            End If
                        Else
                            LAMINATEOTR(i) = "0"
                            OTRRHB(i) = "0"
                            PERCENTOTR(i) = "0"
                        End If
                    Next
                Next

                StrSql = "SELECT LOWPERM, HIGHPERM, FACTOR FROM PENALTYFACTOR WHERE LOWPERM IN (" + FormatNumber(PERCENTOTR(0), 0) + "," + FormatNumber(PERCENTOTR(1), 0) + "," + FormatNumber(PERCENTOTR(2), 0) + ","
                StrSql = StrSql + FormatNumber(PERCENTOTR(3), 0) + "," + FormatNumber(PERCENTOTR(4), 0) + "," + FormatNumber(PERCENTOTR(5), 0) + "," + FormatNumber(PERCENTOTR(6), 0) + "," + FormatNumber(PERCENTOTR(7), 0) + "," + FormatNumber(PERCENTOTR(8), 0) + ","
                StrSql = StrSql + FormatNumber(PERCENTOTR(9), 0) + ")"
                DtsOTR = odbUtil.FillDataSet(StrSql, SBAConnection)

                If DtsOTR.Tables(0).Rows.Count > 0 Then
                    dvBar = DtsOTR.Tables(0).DefaultView()
                    For i = 0 To 9
                        If OTRRHB(i) <> "0" Then
                            dvBar.RowFilter = "LOWPERM = " + FormatNumber(PERCENTOTR(i), 0)
                            dtBar = dvBar.ToTable()
                            If dtBar.Rows.Count > 0 Then
                                OTRRHB(i) = (LAMINATEOTR(i) + (LAMINATEOTR(i) * dtBar.Rows(0).Item("FACTOR").ToString()))
                            Else
                                OTRRHB(i) = " 0"
                            End If
                        End If
                    Next
                End If

            Catch ex As Exception

            End Try
        End Sub
#End Region

#Region "MVTR New Changes"
        Public Sub BarrierPropCalculateWVTR100(ByVal CaseID As Integer, ByVal WvtrTemp As String, ByVal WvtrRH As String, _
                                         ByVal MAT() As String, ByVal EDate As Date, ByVal MatDes() As String, ByVal Dts As DataSet)
            Try
                'Dim Dts As New DataSet()
                Dim dtBar As New DataTable
                Dim odbUtil As New DBUtil()
                Dim WVTRFlag As String
                Dim dvBar As New DataView
                Dim WVTRFlagVal As String = ""
                TotalWVTRB = "0"
                WVTRmsgB = ""
                MsgB = ""


                Dim TempRhVal As String = ""
                If Dts.Tables(0).Rows.Count > 0 Then
                    'Getting WVTR Flag and Values
                    Dim strSpl() As String
                    WVTRFlag = GetWVTRFlag(WvtrTemp, WvtrRH)
                    strSpl = Regex.Split(WVTRFlag, "-")
                    If strSpl.Length > 0 Then
                        WVTRFlagVal = strSpl(0)
                        TempRhVal = strSpl(1)
                    End If


                    If WVTRFlagVal = "1" Then
                        Dim tempId, rhId As String
                        Dim SplFlag1() As String
                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            tempId = SplFlag1(0)
                            rhId = SplFlag1(1)
                            For i = 0 To 9
                                dvBar = Dts.Tables(0).DefaultView
                                Try
                                    dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhId
                                    dtBar = dvBar.ToTable()
                                    If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                        WVTRB(i) = "10000"
                                        If Not WVTRmsgB.Contains(MatDes(i)) Then
                                            WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                        End If
                                    Else
                                        WVTRB(i) = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                    End If
                                Catch ex As Exception
                                    WVTRB(i) = "0"
                                End Try
                            Next
                        End If
                    ElseIf WVTRFlagVal = "3" Then
                        Dim tempId, rhIdL, rhIdH, rhValL, rhValH As String
                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String

                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            tempId = SplFlag1(0)
                            SplFlag2 = Regex.Split(SplFlag1(1), "#")
                            SplFlag3 = Regex.Split(SplFlag1(2), "#")
                            If SplFlag2.Length > 0 Then
                                rhIdL = SplFlag2(1)
                                rhValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    rhIdH = SplFlag3(1)
                                    rhValH = SplFlag3(0)
                                    For i = 0 To 9
                                        dvBar = Dts.Tables(0).DefaultView
                                        Try
                                            Dim lowRh As String = ""
                                            Dim highRh As String = ""
                                            Dim cngCounter As Integer = 0
                                            Try
                                                'Getting lower RH
                                                dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdL
                                                dtBar = dvBar.ToTable()
                                                If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                    lowRh = "10000"
                                                    If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                        WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                    End If
                                                    cngCounter = 1
                                                Else
                                                    lowRh = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                End If

                                                'Getting higher RH
                                                dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdH
                                                dtBar = dvBar.ToTable()
                                                If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                    highRh = "10000"
                                                    If cngCounter <> 1 Then
                                                        If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                            WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                        End If
                                                        cngCounter = 1
                                                    End If
                                                Else
                                                    highRh = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                End If
                                                WVTRB(i) = CDbl(lowRh) + ((WvtrRH - rhValL) / (rhValH - rhValL) * (CDbl(highRh) - CDbl(lowRh)))
                                            Catch ex As Exception
                                                WVTRB(i) = "0"
                                            End Try
                                        Catch ex As Exception
                                        End Try
                                    Next
                                End If

                            End If

                        End If
                    ElseIf WVTRFlagVal = "4" Then
                        Dim rhId, tempIdL, tempIdH, tempValL, tempValH As String
                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String

                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            rhId = SplFlag1(0)
                            SplFlag2 = Regex.Split(SplFlag1(1), "#")
                            SplFlag3 = Regex.Split(SplFlag1(2), "#")
                            If SplFlag2.Length > 0 Then
                                tempIdL = SplFlag2(1)
                                tempValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    tempIdH = SplFlag3(1)
                                    tempValH = SplFlag3(0)
                                    For i = 0 To 9
                                        dvBar = Dts.Tables(0).DefaultView
                                        Try
                                            Dim lowTemp As String = ""
                                            Dim highTemp As String = ""
                                            Dim cngCounter As Integer = 0
                                            Try
                                                'Getting lower RH
                                                dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhId
                                                dtBar = dvBar.ToTable()
                                                If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                    lowTemp = "10000"
                                                    If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                        WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                    End If
                                                    cngCounter = 1
                                                Else
                                                    lowTemp = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                End If


                                                'Getting higher RH
                                                dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhId
                                                dtBar = dvBar.ToTable()
                                                If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                    highTemp = "10000"
                                                    If cngCounter <> 1 Then
                                                        If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                            WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                        End If
                                                        cngCounter = 1
                                                    End If
                                                Else
                                                    highTemp = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                End If
                                                WVTRB(i) = CDbl(lowTemp) + ((WvtrTemp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))
                                            Catch ex As Exception
                                                WVTRB(i) = "0"
                                            End Try
                                        Catch ex As Exception

                                        End Try
                                    Next
                                End If

                            End If

                        End If
                    ElseIf WVTRFlagVal = "2" Then
                        Dim tempIdL, tempIdH, tempValL, tempValH As String
                        Dim rhIdL, rhIdH, rhValL, rhValH As String


                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String

                        Dim SplFlag4() As String
                        Dim SplFlag5() As String

                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then

                            SplFlag2 = Regex.Split(SplFlag1(0), "#")
                            SplFlag3 = Regex.Split(SplFlag1(1), "#")

                            SplFlag4 = Regex.Split(SplFlag1(2), "#")
                            SplFlag5 = Regex.Split(SplFlag1(3), "#")

                            If SplFlag2.Length > 0 Then
                                tempIdL = SplFlag2(1)
                                tempValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    tempIdH = SplFlag3(1)
                                    tempValH = SplFlag3(0)
                                    If SplFlag4.Length > 0 Then
                                        rhIdL = SplFlag4(1)
                                        rhValL = SplFlag4(0)
                                        If SplFlag5.Length > 0 Then
                                            rhIdH = SplFlag5(1)
                                            rhValH = SplFlag5(0)
                                            For i = 0 To 9
                                                dvBar = Dts.Tables(0).DefaultView
                                                Try
                                                    Dim tempLrhL, tempHrhH, tempLrhH, tempHrhL As String
                                                    Dim MidTemp1 As String
                                                    Dim MidTemp2 As String
                                                    Dim cngCounter As Integer = 0
                                                    Try
                                                        'Getting lower temp lower Rh
                                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdL
                                                        dtBar = dvBar.ToTable()
                                                        If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                            tempLrhL = "10000"
                                                            If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                                WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                            End If
                                                            cngCounter = 1
                                                        Else
                                                            tempLrhL = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                        End If


                                                        'Getting higher temp higher Rh
                                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdH
                                                        dtBar = dvBar.ToTable()
                                                        If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                            tempHrhH = "10000"
                                                            If cngCounter <> 1 Then
                                                                If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                                    WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                                End If
                                                                cngCounter = 1
                                                            End If
                                                        Else
                                                            tempHrhH = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                        End If


                                                        'Getting lower temp Higher Rh
                                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdH
                                                        dtBar = dvBar.ToTable()
                                                        If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                            tempLrhH = "10000"
                                                            If cngCounter <> 1 Then
                                                                If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                                    WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                                End If
                                                                cngCounter = 1
                                                            End If
                                                        Else
                                                            tempLrhH = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                        End If


                                                        'Getting higher temp lower Rh
                                                        dvBar.RowFilter = "MATID=" + MAT(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdL
                                                        dtBar = dvBar.ToTable()
                                                        If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                            tempHrhL = "10000"
                                                            If cngCounter <> 1 Then
                                                                If Not WVTRmsgB.Contains(MatDes(i)) Then
                                                                    WVTRmsgB = WVTRmsgB + "" + MatDes(i) + " (WVTR)\n"
                                                                End If
                                                                cngCounter = 1
                                                            End If
                                                        Else
                                                            tempHrhL = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                        End If


                                                        MidTemp1 = CDbl(tempLrhL) + (CDbl(WvtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhL) - CDbl(CDbl(tempLrhL)))))
                                                        MidTemp2 = CDbl(tempLrhH) + (CDbl(WvtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhH) - CDbl(CDbl(tempLrhH)))))
                                                        WVTRB(i) = MidTemp1 + ((WvtrRH - rhValL) / (rhValH - rhValL) * (MidTemp2 - MidTemp1))
                                                    Catch ex As Exception
                                                        WVTRB(i) = "0"
                                                    End Try

                                                Catch ex As Exception

                                                End Try
                                            Next
                                        End If
                                    End If
                                End If

                            End If

                        End If
                    End If
                    
                End If
            Catch ex As Exception

            End Try
        End Sub
        Public Sub BarrierPropCalBlendSubLayersWVTR100(ByVal CaseID As Integer, ByVal WvtrTemp As String, ByVal WvtrRH As String, ByVal ISADJTHICK(,) As String, _
                                                   ByVal MAT(,) As String, ByVal EDate As Date, ByVal MatDes(,) As String, ByVal WVTRPref(,) As String, _
                                                   ByVal Thick(,) As String, ByVal ThickPer(,) As String, ByVal Dts As DataSet)
            Try
                Dim odbUtil As New DBUtil()
                Dim WVTRFlag As String
                Dim dvBar As New DataView
                Dim dtBar As New DataTable
                Dim WVTRFlagVal As String = ""
                WVTRmsgBlendSub = ""
                MsgBlendSub = ""

                Dim TempRhVal As String = ""
                Dim strSpl() As String
                If Dts.Tables(0).Rows.Count > 0 Then
                    'Getting WVTR Flag and Values
                    WVTRFlag = GetWVTRFlag(WvtrTemp, WvtrRH)
                    strSpl = Regex.Split(WVTRFlag, "-")
                    If strSpl.Length > 0 Then
                        WVTRFlagVal = strSpl(0)
                        TempRhVal = strSpl(1)
                    End If


                    If WVTRFlagVal = "1" Then
                        Dim tempId, rhId As String
                        Dim SplFlag1() As String
                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            tempId = SplFlag1(0)
                            rhId = SplFlag1(1)
                            For i = 0 To 9
                                For j = 0 To 1
                                    dvBar = Dts.Tables(0).DefaultView
                                    Try
                                        dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempId + " AND RHID=" + rhId
                                        dtBar = dvBar.ToTable()
                                        If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                            WVTRBLENDSUB(i, j) = "10000"
                                            WVTRL(i, j) = "10000"
                                            If Not WVTRmsgBlendSub.Contains(MatDes(i, j)) Then
                                                WVTRmsgBlendSub = WVTRmsgBlendSub + "" + MatDes(i, j) + " (WVTR)\n"
                                            End If
                                        Else
                                            WVTRBLENDSUB(i, j) = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                            WVTRL(i, j) = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                        End If
                                        If ISADJTHICK(i, j) <> "N" Then
                                            'WVTRB(i) = CDbl(WVTRB(i)) / CDbl(Thick(i))
                                            WVTRBLENDSUB(i, j) = CDbl((WVTRBLENDSUB(i, j) / CDbl(Thick(i, j))) * 25.4)
                                        End If
                                    Catch ex As Exception
                                        WVTRBLENDSUB(i, j) = "0"
                                        WVTRL(i, j) = "0"
                                    End Try
                                Next
                            Next
                        End If
                    ElseIf WVTRFlagVal = "3" Then
                        Dim tempId, rhIdL, rhIdH, rhValL, rhValH As String
                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String

                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            tempId = SplFlag1(0)
                            SplFlag2 = Regex.Split(SplFlag1(1), "#")
                            SplFlag3 = Regex.Split(SplFlag1(2), "#")
                            If SplFlag2.Length > 0 Then
                                rhIdL = SplFlag2(1)
                                rhValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    rhIdH = SplFlag3(1)
                                    rhValH = SplFlag3(0)
                                    For i = 0 To 9
                                        For j = 0 To 1
                                            dvBar = Dts.Tables(0).DefaultView
                                            Try
                                                Dim lowRh As String = ""
                                                Dim highRh As String = ""
                                                Dim cngCounter As Integer = 0
                                                Try
                                                    'Getting lower RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdL
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                        lowRh = "10000"
                                                        If Not WVTRmsgBlendSub.Contains(MatDes(i, j)) Then
                                                            WVTRmsgBlendSub = WVTRmsgBlendSub + "" + MatDes(i, j) + " (WVTR)\n"
                                                        End If
                                                        cngCounter = 1
                                                    Else
                                                        lowRh = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                    End If

                                                    'Getting higher RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdH
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                        highRh = "10000"
                                                        If cngCounter <> 1 Then
                                                            If Not WVTRmsgBlendSub.Contains(MatDes(i, j)) Then
                                                                WVTRmsgBlendSub = WVTRmsgBlendSub + "" + MatDes(i, j) + " (WVTR)\n"
                                                            End If
                                                            cngCounter = 1
                                                        End If
                                                    Else
                                                        highRh = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                    End If
                                                    WVTRBLENDSUB(i, j) = CDbl(lowRh) + ((WvtrRH - rhValL) / (rhValH - rhValL) * (CDbl(highRh) - CDbl(lowRh)))
                                                    WVTRL(i, j) = CDbl(lowRh) + ((WvtrRH - rhValL) / (rhValH - rhValL) * (CDbl(highRh) - CDbl(lowRh)))
                                                    If ISADJTHICK(i, j) <> "N" Then
                                                        'WVTRB(i) = CDbl(WVTRB(i)) / CDbl(Thick(i))
                                                        WVTRBLENDSUB(i, j) = CDbl((WVTRBLENDSUB(i, j) / CDbl(Thick(i, j))) * 25.4)
                                                    End If
                                                Catch ex As Exception
                                                    WVTRBLENDSUB(i, j) = "0"
                                                    WVTRL(i, j) = "0"
                                                End Try
                                            Catch ex As Exception
                                            End Try
                                        Next
                                    Next
                                End If

                            End If

                        End If
                    ElseIf WVTRFlagVal = "4" Then
                        Dim rhId, tempIdL, tempIdH, tempValL, tempValH As String
                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String

                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then
                            rhId = SplFlag1(0)
                            SplFlag2 = Regex.Split(SplFlag1(1), "#")
                            SplFlag3 = Regex.Split(SplFlag1(2), "#")
                            If SplFlag2.Length > 0 Then
                                tempIdL = SplFlag2(1)
                                tempValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    tempIdH = SplFlag3(1)
                                    tempValH = SplFlag3(0)
                                    For i = 0 To 9
                                        For j = 0 To 1
                                            dvBar = Dts.Tables(0).DefaultView
                                            Try
                                                Dim lowTemp As String = ""
                                                Dim highTemp As String = ""
                                                Dim cngCounter As Integer = 0
                                                Try
                                                    'Getting lower RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhId
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                        lowTemp = "10000"
                                                        If Not WVTRmsgBlendSub.Contains(MatDes(i, j)) Then
                                                            WVTRmsgBlendSub = WVTRmsgBlendSub + "" + MatDes(i, j) + " (WVTR)\n"
                                                        End If
                                                        cngCounter = 1
                                                    Else
                                                        lowTemp = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                    End If


                                                    'Getting higher RH
                                                    dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhId
                                                    dtBar = dvBar.ToTable()
                                                    If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                        highTemp = "10000"
                                                        If cngCounter <> 1 Then
                                                            If Not WVTRmsgBlendSub.Contains(MatDes(i, j)) Then
                                                                WVTRmsgBlendSub = WVTRmsgBlendSub + "" + MatDes(i, j) + " (WVTR)\n"
                                                            End If
                                                            cngCounter = 1
                                                        End If
                                                    Else
                                                        highTemp = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                    End If
                                                    WVTRBLENDSUB(i, j) = CDbl(lowTemp) + ((WvtrTemp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))
                                                    WVTRL(i, j) = CDbl(lowTemp) + ((WvtrTemp - tempValL) / (tempValH - tempValL) * (CDbl(highTemp) - CDbl(lowTemp)))
                                                    If ISADJTHICK(i, j) <> "N" Then
                                                        'WVTRB(i) = CDbl(WVTRB(i)) / CDbl(Thick(i))
                                                        WVTRBLENDSUB(i, j) = CDbl((WVTRBLENDSUB(i, j) / CDbl(Thick(i, j))) * 25.4)
                                                    End If
                                                Catch ex As Exception
                                                    WVTRBLENDSUB(i, j) = "0"
                                                    WVTRL(i, j) = "0"
                                                End Try
                                            Catch ex As Exception

                                            End Try
                                        Next
                                    Next
                                End If

                            End If

                        End If
                    ElseIf WVTRFlagVal = "2" Then
                        Dim tempIdL, tempIdH, tempValL, tempValH As String
                        Dim rhIdL, rhIdH, rhValL, rhValH As String


                        Dim SplFlag1() As String
                        Dim SplFlag2() As String
                        Dim SplFlag3() As String

                        Dim SplFlag4() As String
                        Dim SplFlag5() As String

                        SplFlag1 = Regex.Split(TempRhVal, "##")
                        If SplFlag1.Length > 0 Then

                            SplFlag2 = Regex.Split(SplFlag1(0), "#")
                            SplFlag3 = Regex.Split(SplFlag1(1), "#")

                            SplFlag4 = Regex.Split(SplFlag1(2), "#")
                            SplFlag5 = Regex.Split(SplFlag1(3), "#")

                            If SplFlag2.Length > 0 Then
                                tempIdL = SplFlag2(1)
                                tempValL = SplFlag2(0)
                                If SplFlag3.Length > 0 Then
                                    tempIdH = SplFlag3(1)
                                    tempValH = SplFlag3(0)
                                    If SplFlag4.Length > 0 Then
                                        rhIdL = SplFlag4(1)
                                        rhValL = SplFlag4(0)
                                        If SplFlag5.Length > 0 Then
                                            rhIdH = SplFlag5(1)
                                            rhValH = SplFlag5(0)
                                            For i = 0 To 9
                                                For j = 0 To 1
                                                    dvBar = Dts.Tables(0).DefaultView
                                                    Try
                                                        Dim tempLrhL, tempHrhH, tempLrhH, tempHrhL As String
                                                        Dim MidTemp1 As String
                                                        Dim MidTemp2 As String
                                                        Dim cngCounter As Integer = 0
                                                        Try
                                                            'Getting lower temp lower Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdL
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                                tempLrhL = "10000"
                                                                If Not WVTRmsgBlendSub.Contains(MatDes(i, j)) Then
                                                                    WVTRmsgBlendSub = WVTRmsgBlendSub + "" + MatDes(i, j) + " (WVTR)\n"
                                                                End If
                                                                cngCounter = 1
                                                            Else
                                                                tempLrhL = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                            End If


                                                            'Getting higher temp higher Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdH
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                                tempHrhH = "10000"
                                                                If cngCounter <> 1 Then
                                                                    If Not WVTRmsgBlendSub.Contains(MatDes(i, j)) Then
                                                                        WVTRmsgBlendSub = WVTRmsgBlendSub + "" + MatDes(i, j) + " (WVTR)\n"
                                                                    End If
                                                                    cngCounter = 1
                                                                End If
                                                            Else
                                                                tempHrhH = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                            End If


                                                            'Getting lower temp Higher Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdH
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                                tempLrhH = "10000"
                                                                If cngCounter <> 1 Then
                                                                    If Not WVTRmsgBlendSub.Contains(MatDes(i, j)) Then
                                                                        WVTRmsgBlendSub = WVTRmsgBlendSub + "" + MatDes(i, j) + " (WVTR)\n"
                                                                    End If
                                                                    cngCounter = 1
                                                                End If
                                                            Else
                                                                tempLrhH = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                            End If


                                                            'Getting higher temp lower Rh
                                                            dvBar.RowFilter = "MATID=" + MAT(i, j) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdL
                                                            dtBar = dvBar.ToTable()
                                                            If dtBar.Rows(0).Item("WVTRVALUE").ToString() = "" Then
                                                                tempHrhL = "10000"
                                                                If cngCounter <> 1 Then
                                                                    If Not WVTRmsgBlendSub.Contains(MatDes(i, j)) Then
                                                                        WVTRmsgBlendSub = WVTRmsgBlendSub + "" + MatDes(i, j) + " (WVTR)\n"
                                                                    End If
                                                                    cngCounter = 1
                                                                End If
                                                            Else
                                                                tempHrhL = dtBar.Rows(0).Item("WVTRVALUE").ToString()
                                                            End If


                                                            MidTemp1 = CDbl(tempLrhL) + (CDbl(WvtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhL) - CDbl(CDbl(tempLrhL)))))
                                                            MidTemp2 = CDbl(tempLrhH) + (CDbl(WvtrTemp - tempValL) / CDbl(tempValH - tempValL) * (CDbl(CDbl(tempHrhH) - CDbl(CDbl(tempLrhH)))))
                                                            WVTRBLENDSUB(i, j) = MidTemp1 + ((WvtrRH - rhValL) / (rhValH - rhValL) * (MidTemp2 - MidTemp1))
                                                            WVTRL(i, j) = MidTemp1 + ((WvtrRH - rhValL) / (rhValH - rhValL) * (MidTemp2 - MidTemp1))
                                                            If ISADJTHICK(i, j) <> "N" Then
                                                                'WVTRB(i) = CDbl(WVTRB(i)) / CDbl(Thick(i))
                                                                WVTRBLENDSUB(i, j) = CDbl((WVTRBLENDSUB(i, j) / CDbl(Thick(i, j))) * 25.4)
                                                            End If
                                                        Catch ex As Exception
                                                            WVTRBLENDSUB(i, j) = "0"
                                                            WVTRL(i, j) = "0"
                                                        End Try

                                                    Catch ex As Exception

                                                    End Try
                                                Next
                                            Next
                                        End If
                                    End If
                                End If

                            End If

                        End If
                    End If
                    If OTRMsgBlendSub <> "" Or WVTRmsgBlendSub <> "" Then
                        MsgBlendSub = "Data is not present for following materials for Date " + CDate(EDate).Date + "\n"
                        If OTRMsgBlendSub <> "" Then
                            MsgBlendSub = MsgBlendSub + OTRMsgBlendSub + ""
                        End If
                        If WVTRmsgBlendSub <> "" Then
                            MsgBlendSub = MsgBlendSub + WVTRmsgBlendSub + ""
                        End If
                    End If

                    If Dts.Tables(0).Rows.Count > 0 Then
                        BarrierPropCalBlendWVTR100(WVTRPref, ThickPer, MAT, EDate, Thick)
                    End If
                End If
            Catch ex As Exception

            End Try
        End Sub
        Public Sub BarrierPropCalBlendWVTR100(ByVal WVTRPref(,) As String, ByVal ThickPer(,) As String, ByVal MAT(,) As String, ByVal EDate As String, ByVal Thick(,) As String)
            Try
                Dim StrSql As String = String.Empty
                Dim StrSql1 As String = String.Empty
                Dim DtsWVTR As New DataSet()
                Dim PERCENTWVTR(9) As String
                Dim LAMINATEWVTR(9) As String
                Dim dvBarr As New DataView
                Dim dtBarr As New DataTable
                Dim odbUtil As New DBUtil()
                TotalWVTRBLEND = "0"
                Dim WVTR1 As Double
                Dim WVTR2 As Double

                For i = 0 To 9
                    For j = 0 To 1 - 1
                        'WVTR Calculation
                        If WVTRPref(i, j) <> "" And WVTRPref(i, j + 1) <> "" Then
                            If WVTRPref(i, j) <> "0" And WVTRPref(i, j + 1) <> "0" Then
                                LAMINATEWVTR(i) = (1 / ((1 / WVTRPref(i, j + 1)) + (1 / WVTRPref(i, j))))
                                WVTR1 = CDbl(WVTRPref(i, j) * Thick(i, j) / 25.4)
                                WVTR2 = CDbl(WVTRPref(i, j + 1) * Thick(i, j + 1) / 25.4)
                                If WVTR1 < WVTR2 Then
                                    PERCENTWVTR(i) = ThickPer(i, j)
                                ElseIf WVTR2 < WVTR1 Then
                                    PERCENTWVTR(i) = ThickPer(i, j + 1)
                                Else
                                    PERCENTWVTR(i) = ThickPer(i, j)
                                End If
                            Else
                                LAMINATEWVTR(i) = "0"
                                WVTR(i) = "0"
                                PERCENTWVTR(i) = "0"
                            End If
                        ElseIf WVTRPref(i, j) <> "" Or WVTRPref(i, j + 1) <> "" Then
                            If WVTRPref(i, j) = "" Then
                                If WVTRBLENDSUB(i, j) <> "0" And WVTRPref(i, j + 1) <> "0" Then
                                    LAMINATEWVTR(i) = (1 / ((1 / WVTRPref(i, j + 1)) + (1 / WVTRBLENDSUB(i, j))))
                                    WVTR2 = CDbl(WVTRPref(i, j + 1) * Thick(i, j + 1) / 25.4)
                                    If WVTRL(i, j) < WVTR2 Then
                                        PERCENTWVTR(i) = ThickPer(i, j)
                                    ElseIf WVTR2 < WVTRL(i, j) Then
                                        PERCENTWVTR(i) = ThickPer(i, j + 1)
                                    Else
                                        PERCENTWVTR(i) = ThickPer(i, j)
                                    End If
                                Else
                                    LAMINATEWVTR(i) = "0"
                                    WVTR(i) = "0"
                                    PERCENTWVTR(i) = "0"
                                End If
                            ElseIf WVTRPref(i, j + 1) = "" Then
                                If WVTRPref(i, j) <> "0" And WVTRBLENDSUB(i, j + 1) <> "0" Then
                                    LAMINATEWVTR(i) = (1 / ((1 / WVTRBLENDSUB(i, j + 1)) + (1 / WVTRPref(i, j))))
                                    WVTR1 = CDbl(WVTRPref(i, j) * Thick(i, j) / 25.4)
                                    If WVTR1 < WVTRL(i, j + 1) Then
                                        PERCENTWVTR(i) = ThickPer(i, j)
                                    ElseIf WVTRL(i, j + 1) < WVTR1 Then
                                        PERCENTWVTR(i) = ThickPer(i, j + 1)
                                    Else
                                        PERCENTWVTR(i) = ThickPer(i, j)
                                    End If
                                Else
                                    LAMINATEWVTR(i) = "0"
                                    WVTR(i) = "0"
                                    PERCENTWVTR(i) = "0"
                                End If
                            End If
                        ElseIf WVTRL(i, j) <> "0" And WVTRL(i, j + 1) <> "0" Then
                            LAMINATEWVTR(i) = (1 / ((1 / WVTRBLENDSUB(i, j + 1)) + (1 / WVTRBLENDSUB(i, j))))
                            If WVTRL(i, j) < WVTRL(i, j + 1) Then
                                PERCENTWVTR(i) = ThickPer(i, j)
                            ElseIf WVTRL(i, j + 1) < WVTRL(i, j) Then
                                PERCENTWVTR(i) = ThickPer(i, j + 1)
                            Else
                                PERCENTWVTR(i) = ThickPer(i, j)
                            End If
                        Else
                            LAMINATEWVTR(i) = "0"
                            WVTR(i) = "0"
                            PERCENTWVTR(i) = "0"
                        End If
                    Next
                Next

                StrSql1 = "SELECT LOWPERM, HIGHPERM, FACTOR FROM PENALTYFACTOR WHERE LOWPERM IN (" + FormatNumber(PERCENTWVTR(0), 0) + "," + FormatNumber(PERCENTWVTR(1), 0) + "," + FormatNumber(PERCENTWVTR(2), 0) + ","
                StrSql1 = StrSql1 + FormatNumber(PERCENTWVTR(3), 0) + "," + FormatNumber(PERCENTWVTR(4), 0) + "," + FormatNumber(PERCENTWVTR(5), 0) + "," + FormatNumber(PERCENTWVTR(6), 0) + "," + FormatNumber(PERCENTWVTR(7), 0) + "," + FormatNumber(PERCENTWVTR(8), 0) + ","
                StrSql1 = StrSql1 + FormatNumber(PERCENTWVTR(9), 0) + ")"
                DtsWVTR = odbUtil.FillDataSet(StrSql1, SBAConnection)

                If DtsWVTR.Tables(0).Rows.Count > 0 Then
                    dvBarr = DtsWVTR.Tables(0).DefaultView()
                    For i = 0 To 9
                        If WVTR(i) <> "0" Then
                            dvBarr.RowFilter = "LOWPERM = " + FormatNumber(PERCENTWVTR(i), 0)
                            dtBarr = dvBarr.ToTable()
                            If dtBarr.Rows.Count > 0 Then
                                WVTR(i) = (LAMINATEWVTR(i) + (LAMINATEWVTR(i) * dtBarr.Rows(0).Item("FACTOR").ToString()))
                            Else
                                WVTR(i) = " 0"
                            End If
                        End If
                    Next
                End If

            Catch ex As Exception

            End Try
        End Sub
#End Region
    End Class
End Class
