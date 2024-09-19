Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System.Collections
Imports System
Imports Med1GetData
Imports System.Math
Imports System.Web.UI.HtmlTextWriter

Public Class SMed1BarrierGetData
    Public Class Calculations
        Dim Med1Connection As String = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
        Dim SMed1Connection As String = System.Configuration.ConfigurationManager.AppSettings("MedSustain1ConnectionString")
        Public OTRB(9) As String
        Public TotalOTRB As String = "0"
        Public TotalWVTRB As String = "0"
        Public WVTRB(9) As String
        Dim OTRMsgB, WVTRmsgB As String
        Public MsgB As String
        Dim path As String = "C:\Sudhanshu\Raxa_Code\AlliedProject\Log\TimeLogS1.txt"
        Dim objLog As New LogFiles.CreateLogFiles

        Public Sub BarrierPropCalculateNew(ByVal CaseID As Integer, ByVal OtrTemp As String, ByVal WvtrTemp As String, ByVal OtrRH As String, ByVal WvtrRH As String, _
                                    ByVal Grades() As String, ByVal EDate As Date, ByVal MatDes() As String, ByVal OtrPref() As String, ByVal WVTRPref() As String _
                                    , ByVal Thick() As String, ByVal ISADJTHICK() As String)
            Try
                Dim StrSql As String = String.Empty
                Dim StrSql1 As String = String.Empty
                Dim Dts As New DataSet()
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

                'Getting All Gradedata
                StrSql = "SELECT GRADEID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE FROM MATBARRIER WHERE GRADEID=" + Grades(0) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  "
                StrSql = StrSql + "FROM MATBARRIER WHERE GRADEID= " + Grades(0) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT GRADEID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE FROM MATBARRIER WHERE GRADEID=" + Grades(1) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIER WHERE GRADEID= " + Grades(1) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT GRADEID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE FROM MATBARRIER WHERE GRADEID=" + Grades(2) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIER WHERE GRADEID= " + Grades(2) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT GRADEID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE FROM MATBARRIER WHERE GRADEID=" + Grades(3) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIER WHERE GRADEID= " + Grades(3) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT GRADEID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE FROM MATBARRIER WHERE GRADEID=" + Grades(4) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIER WHERE GRADEID= " + Grades(4) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT GRADEID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE FROM MATBARRIER WHERE GRADEID=" + Grades(5) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIER WHERE GRADEID= " + Grades(5) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT GRADEID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE FROM MATBARRIER WHERE GRADEID=" + Grades(6) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIER WHERE GRADEID= " + Grades(6) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT GRADEID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE FROM MATBARRIER WHERE GRADEID=" + Grades(7) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIER WHERE GRADEID= " + Grades(7) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT GRADEID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE FROM MATBARRIER WHERE GRADEID=" + Grades(8) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIER WHERE GRADEID= " + Grades(8) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT GRADEID,TEMPID,RHID,OTRVALUE,WVTRVALUE,EFFDATE FROM MATBARRIER WHERE GRADEID=" + Grades(9) + " AND EFFDATE=TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') "
                StrSql = StrSql + "FROM MATBARRIER WHERE GRADEID= " + Grades(9) + " AND EFFDATE<=TO_DATE('" + EDate + "','MM//DD/YYYY')),'MM/DD/YYYY') "
                Dts = odbUtil.FillDataSet(StrSql, Med1Connection)

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
                                    dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhId
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
                                        OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
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
                                                dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdL
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
                                                dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdH
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
                                                    OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
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
                                                dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhId
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
                                                dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhId
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
                                                    OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
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
                                                        dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdL
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
                                                        dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdH
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
                                                        dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdH
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
                                                        dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdL
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
                                                            OTRB(i) = CDbl(OTRB(i)) / CDbl(Thick(i))
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
                                    dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhId
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
                                        WVTRB(i) = CDbl(WVTRB(i)) / CDbl(Thick(i))
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
                                                dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdL
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
                                                dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempId + " AND RHID=" + rhIdH
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
                                                    WVTRB(i) = CDbl(WVTRB(i)) / CDbl(Thick(i))
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
                                                dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhId
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
                                                dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhId
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
                                                    WVTRB(i) = CDbl(WVTRB(i)) / CDbl(Thick(i))
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
                                                        dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdL
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
                                                        dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdH
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
                                                        dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempIdL + " AND RHID=" + rhIdH
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
                                                        dvBar.RowFilter = "GRADEID=" + Grades(i) + " AND TEMPID=" + tempIdH + " AND RHID=" + rhIdL
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
                                                            WVTRB(i) = CDbl(WVTRB(i)) / CDbl(Thick(i))
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
                    If OTRMsgB <> "" Or WVTRmsgB <> "" Then
                        MsgB = "Data is not present for following materials for Date " + CDate(EDate).Date + "\n"
                        If OTRMsgB <> "" Then
                            MsgB = MsgB + OTRMsgB + ""
                        End If
                        If WVTRmsgB <> "" Then
                            MsgB = MsgB + WVTRmsgB + ""
                        End If
                    End If
                    'TotalOTR = "0"
                    For i = 0 To 9
                        If Grades(i) <> "0" Then
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
                        If Grades(i) <> 0 Then
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
            Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
            Dim StrSql1 As String = String.Empty
            OTRFlag = "0"
            Try
                StrSql1 = "select * from BARRIERTEMP "
                StrSql1 = StrSql1 + "where(TEMPVAL = " + Temp + ") "
                Dts1 = odbUtil.FillDataSet(StrSql1, Med1Connection)
                StrSql1 = String.Empty
                StrSql1 = "select * from BARRIERH "
                StrSql1 = StrSql1 + "where(RHVALUE = " + RH + ") "
                Dts2 = odbUtil.FillDataSet(StrSql1, Med1Connection)
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
            Dim Med1Connection As String = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
            Dim StrSql1 As String = String.Empty
            WVTRFlag = "0"
            Try
                StrSql1 = "select * from BARRIERTEMP "
                StrSql1 = StrSql1 + "where(TEMPVAL = " + Temp + ") "
                Dts1 = odbUtil.FillDataSet(StrSql1, Med1Connection)
                StrSql1 = String.Empty
                StrSql1 = "select * from BARRIERH "
                StrSql1 = StrSql1 + "where(RHVALUE = " + RH + ") "
                Dts2 = odbUtil.FillDataSet(StrSql1, Med1Connection)
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
                Dts = odbUtil.FillDataSet(strsql, Med1Connection)
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
                Dts = odbUtil.FillDataSet(strsql, Med1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("GetBarrierHighLowTemp:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
    End Class
End Class
