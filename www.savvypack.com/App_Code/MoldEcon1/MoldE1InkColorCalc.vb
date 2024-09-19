Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports MoldE1GetData
Imports System.Math
Imports System.Web.UI.HtmlTextWriter

Public Class MoldE1InkColorCalc
    Public Class Calculation
        Dim MoldE1Connection As String = System.Configuration.ConfigurationManager.AppSettings("MoldE1ConnectionString")
        Public thickness(10) As String
        Public weight(10) As String
        Public dryprice(10) As String
        Public costperarea(10) As String
        Dim colorCode(10) As String
        Dim Coverage(10) As String
        Public total(5) As String

        Public totaVal1 As String
        Public totaVal2 As String

        Dim wetCost As String
        Dim perSolid As String
        Dim drycost As String
        Dim SpecGrav As String
        Public Sub InkColorCalculate(ByVal caseid As String, ByVal UserName As String)
            Dim ds As New DataSet
            Dim dsColor As New DataSet
            Dim dsPref As New DataSet
            Dim i As Integer = 0
            Try
                ds = GetColorDetails(caseid)
                If ds.Tables(0).Rows.Count > 0 Then
                    For i = 0 To 9
                        colorCode(i) = ds.Tables(0).Rows(0).Item("COLOR" + (i + 1).ToString() + "")
                        Coverage(i) = ds.Tables(0).Rows(0).Item("COV" + (i + 1).ToString() + "")
                        'Calculation For Thickness
                        If CDbl(colorCode(i)) > 0.0 Then
                            thickness(i) = FormatNumber((CDbl(Coverage(i) / 100) * CDbl(0.03)).ToString(), 3)
                        Else
                            thickness(i) = 0
                        End If

                        total(0) = total(0) + CDbl(Coverage(i))

                        'Calculation of Specific Gravity
                        If CDbl(colorCode(i)) > 0.0 Then
                            dsColor = GetColors(colorCode(i).ToString(), UserName, caseid)
                            If dsColor.Tables(0).Rows.Count > 0 Then
                                SpecGrav = 0
                                For j = 1 To 11
                                    If CDbl(dsColor.Tables(0).Rows(0).Item("SGP" + j.ToString() + "")) > 0.0 Then
                                        'If dsColor.Tables(0).Rows(0).Item("UNITS").ToString() <> "1" Then
                                        SpecGrav = SpecGrav + (CDbl(dsColor.Tables(0).Rows(0).Item("BCOLOR" + j.ToString() + "").ToString()) * (CDbl(dsColor.Tables(0).Rows(0).Item("SGP" + j.ToString() + "").ToString()) * 0.036) / CDbl(100))
                                        'Else
                                        ''  SpecGrav = SpecGrav + (CDbl(dsColor.Tables(0).Rows(0).Item("BCOLOR" + j.ToString() + "").ToString()) * (CDbl(dsColor.Tables(0).Rows(0).Item("SGP" + j.ToString() + "").ToString())) / CDbl(100))
                                        'End If

                                    Else
                                        ' If dsColor.Tables(0).Rows(0).Item("UNITS").ToString() <> "1" Then
                                        SpecGrav = SpecGrav + (CDbl(dsColor.Tables(0).Rows(0).Item("BCOLOR" + j.ToString() + "").ToString()) * (CDbl(dsColor.Tables(0).Rows(0).Item("SGS" + j.ToString() + "").ToString()) * 0.036) / CDbl(100))
                                        ' Else
                                        '  SpecGrav = SpecGrav + (CDbl(dsColor.Tables(0).Rows(0).Item("BCOLOR" + j.ToString() + "").ToString()) * (CDbl(dsColor.Tables(0).Rows(0).Item("SGS" + j.ToString() + "").ToString())) / CDbl(100))
                                        ' End If

                                    End If


                                Next
                            End If
                            'conversion from english to metric unit
                            weight(i) = FormatNumber((CDbl(thickness(i)) * CDbl(SpecGrav)).ToString(), 5)
                            thickness(i) = thickness(i) * CDbl(ds.Tables(0).Rows(0).Item("CONVTHICK").ToString())
                            total(1) = total(1) + CDbl(thickness(i))
                            'If (CDbl(dsColor.Tables(0).Rows(0).Item("CONVAREA").ToString()) <> 1) Then
                            '    weight(i) = (weight(i) * CDbl(dsColor.Tables(0).Rows(0).Item("CONVWT").ToString() * 1000)) / CDbl(dsColor.Tables(0).Rows(0).Item("CONVAREA").ToString())
                            'End If



                        Else
                            weight(i) = 0
                        End If
                        total(2) = total(2) + CDbl(weight(i))

                        'Calculation of Dry Cost
                        If CDbl(colorCode(i)) > 0.0 Then
                            dsPref = GetColorPrefDetails(UserName, colorCode(i).ToString(), caseid)
                            If dsPref.Tables(0).Rows.Count > 0 Then
                                drycost = 0
                                For j = 1 To 11
                                    If CDbl(dsPref.Tables(0).Rows(0).Item("WETPP" + j.ToString() + "").ToString()) <> 0 Then
                                        wetCost = CDbl(dsPref.Tables(0).Rows(0).Item("WETPP" + j.ToString() + "").ToString())
                                    Else
                                        wetCost = CDbl(dsPref.Tables(0).Rows(0).Item("WETPS" + j.ToString() + "").ToString())
                                    End If

                                    If CDbl(dsPref.Tables(0).Rows(0).Item("PERSOLP" + j.ToString() + "").ToString()) <> 0 Then
                                        perSolid = CDbl(dsPref.Tables(0).Rows(0).Item("PERSOLP" + j.ToString() + "").ToString())
                                    Else
                                        perSolid = CDbl(dsPref.Tables(0).Rows(0).Item("PERSOLS" + j.ToString() + "").ToString())
                                    End If

                                    If perSolid > 0 Then
                                        drycost = CDbl(wetCost / perSolid)
                                    Else
                                        drycost = 0
                                    End If
                                    If CDbl(dsPref.Tables(0).Rows(0).Item("WETPP" + j.ToString() + "").ToString()) <> 0 Then
                                        dryprice(i) = dryprice(i) + (CDbl(drycost) * CDbl(dsPref.Tables(0).Rows(0).Item("BCOLOR" + j.ToString() + "").ToString()) / CDbl(100))
                                    Else
                                        dryprice(i) = dryprice(i) + (CDbl(drycost) * CDbl(dsPref.Tables(0).Rows(0).Item("BCOLOR" + j.ToString() + "").ToString()) / CDbl(100))
                                    End If

                                Next
                                'If (CDbl(dsColor.Tables(0).Rows(0).Item("UNITS").ToString()) = 1) Then
                                '    dryprice(i) = FormatNumber(dryprice(i) * 0.002205, 3)
                                'Else
                                '    dryprice(i) = FormatNumber(dryprice(i), 3)
                                'End If

                                dryprice(i) = dryprice(i) * CDbl(ds.Tables(0).Rows(0).Item("CURR").ToString())

                            End If
                        Else
                            dryprice(i) = 0
                        End If

                        'calculation of cost per area
                        If CDbl(colorCode(i)) > 0.0 Then
                            costperarea(i) = FormatNumber((dryprice(i) * weight(i)).ToString(), 4)
                        Else
                            costperarea(i) = 0
                        End If
                        total(4) = total(4) + CDbl(costperarea(i))
                    Next
                    total(3) = CDbl(total(4)) / CDbl(total(2))

                    total(2) = CDbl(total(2)) * (CDbl(ds.Tables(0).Rows(0).Item("CONVWT2").ToString()) / CDbl(ds.Tables(0).Rows(0).Item("CONVAREA").ToString()))
                    total(3) = total(3) * CDbl(1 / CDbl(ds.Tables(0).Rows(0).Item("CONVWT").ToString()))
                    total(4) = total(4) * CDbl(1 / CDbl(ds.Tables(0).Rows(0).Item("CONVAREA").ToString()))



                End If
            Catch ex As Exception
                Throw New Exception("E1InkColorCalc:InkColorCalculate:" + ex.Message.ToString())
            End Try
        End Sub

#Region "GetData"
        Protected Function GetColorDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "COLOR1, "
                StrSql = StrSql + "COLOR2, "
                StrSql = StrSql + "COLOR3, "
                StrSql = StrSql + "COLOR4 , "
                StrSql = StrSql + "COLOR5, "
                StrSql = StrSql + "COLOR6 , "
                StrSql = StrSql + "COLOR7, "
                StrSql = StrSql + "COLOR8, "
                StrSql = StrSql + "COLOR9, "
                StrSql = StrSql + "COLOR10, "
                StrSql = StrSql + "COV1, "
                StrSql = StrSql + "COV2, "
                StrSql = StrSql + "COV3, "
                StrSql = StrSql + "COV4, "
                StrSql = StrSql + "COV5, "
                StrSql = StrSql + "COV6, "
                StrSql = StrSql + "COV7, "
                StrSql = StrSql + "COV8, "
                StrSql = StrSql + "COV9, "
                StrSql = StrSql + "COV10, "
                StrSql = StrSql + "PREF.UNITS UNITS, "
                StrSql = StrSql + "PREF.CONVWT CONVWT, "
                StrSql = StrSql + "PREF.CONVWT2 CONVWT2, "
                StrSql = StrSql + "PREF.CONVTHICK CONVTHICK, "
                StrSql = StrSql + "PREF.CURR CURR, "
                StrSql = StrSql + "PREF.CONVAREA CONVAREA "
                StrSql = StrSql + "FROM COLORINPUT "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID= COLORINPUT.CASEID " ' + CaseId.ToString()
                StrSql = StrSql + "WHERE COLORINPUT.CASEID =" + CaseId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, MoldE1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1InkColorCalc:GetColorDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetColors(ByVal ColId As Integer, ByVal UserName As String, ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ds As New DataSet()
            Try
                StrSql = "SELECT BASECOLORID,DATECOL  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 1 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 2 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 2 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 3 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 3 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 4 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 4 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 5 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 5 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 6 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 6 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 7 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 7 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 8 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 8 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 9 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 9 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 10 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 10 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 11 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 11 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + ") "
                StrSql = StrSql + "ORDER BY BASECOLORID ASC "
                ds = odbUtil.FillDataSet(StrSql, MoldE1Connection)

                If ds.Tables(0).Rows.Count > 0 Then
                    StrSql = "SELECT COLORID, (COLORID||'  '||COLORNAME)COLDES,COLORNAME,PMS,  "
                    StrSql = StrSql + "BCOLOR1, "
                    StrSql = StrSql + "BCOLOR2, "
                    StrSql = StrSql + "BCOLOR3, "
                    StrSql = StrSql + "BCOLOR4, "
                    StrSql = StrSql + "BCOLOR5, "
                    StrSql = StrSql + "BCOLOR6, "
                    StrSql = StrSql + "BCOLOR7, "
                    StrSql = StrSql + "BCOLOR8, "
                    StrSql = StrSql + "BCOLOR9, "
                    StrSql = StrSql + "BCOLOR10, "
                    StrSql = StrSql + "BCOLOR11, "

                    StrSql = StrSql + "IP.SGRAVITY1 AS SGP1, "
                    StrSql = StrSql + "IP.SGRAVITY2 AS SGP2, "
                    StrSql = StrSql + "IP.SGRAVITY3 AS SGP3, "
                    StrSql = StrSql + "IP.SGRAVITY4 AS SGP4, "
                    StrSql = StrSql + "IP.SGRAVITY5 AS SGP5, "
                    StrSql = StrSql + "IP.SGRAVITY6 AS SGP6, "
                    StrSql = StrSql + "IP.SGRAVITY7 AS SGP7, "
                    StrSql = StrSql + "IP.SGRAVITY8 AS SGP8, "
                    StrSql = StrSql + "IP.SGRAVITY9 AS SGP9, "
                    StrSql = StrSql + "IP.SGRAVITY10 AS SGP10, "
                    StrSql = StrSql + "IP.SGRAVITY11 AS SGP11,"

                    'Bug#379# start
                    StrSql = StrSql + "BA1.SGRAVITY SGS1, "
                    StrSql = StrSql + "BA2.SGRAVITY SGS2, "
                    StrSql = StrSql + "BA3.SGRAVITY SGS3, "
                    StrSql = StrSql + "BA4.SGRAVITY SGS4, "
                    StrSql = StrSql + "BA5.SGRAVITY SGS5, "
                    StrSql = StrSql + "BA6.SGRAVITY SGS6, "
                    StrSql = StrSql + "BA7.SGRAVITY SGS7, "
                    StrSql = StrSql + "BA8.SGRAVITY SGS8, "
                    StrSql = StrSql + "BA9.SGRAVITY SGS9, "
                    StrSql = StrSql + "BA10.SGRAVITY SGS10, "
                    StrSql = StrSql + "BA11.SGRAVITY SGS11 "
                    'Bug#379# End                

                    StrSql = StrSql + "FROM COLORDETAILS "
                    StrSql = StrSql + "INNER JOIN INKPREFERENCES IP "
                    StrSql = StrSql + "ON UPPER(IP.USERNAME)='" + UserName.ToString().ToUpper() + "' "
                    StrSql = StrSql + "INNER JOIN BASECOLOR BC1 "
                    StrSql = StrSql + "ON BC1.BASECOLORID=1 "
                    StrSql = StrSql + "INNER JOIN BASECOLOR BC2 "
                    StrSql = StrSql + "ON BC2.BASECOLORID=2 "
                    StrSql = StrSql + "INNER JOIN BASECOLOR BC3 "
                    StrSql = StrSql + "ON BC3.BASECOLORID=3 "
                    StrSql = StrSql + "INNER JOIN BASECOLOR BC4 "
                    StrSql = StrSql + "ON BC4.BASECOLORID=4 "
                    StrSql = StrSql + "INNER JOIN BASECOLOR BC5 "
                    StrSql = StrSql + "ON BC5.BASECOLORID=5 "
                    StrSql = StrSql + "INNER JOIN BASECOLOR BC6 "
                    StrSql = StrSql + "ON BC6.BASECOLORID=6 "
                    StrSql = StrSql + "INNER JOIN BASECOLOR BC7 "
                    StrSql = StrSql + "ON BC7.BASECOLORID=7 "
                    StrSql = StrSql + "INNER JOIN BASECOLOR BC8 "
                    StrSql = StrSql + "ON BC8.BASECOLORID=8 "
                    StrSql = StrSql + "INNER JOIN BASECOLOR BC9 "
                    StrSql = StrSql + "ON BC9.BASECOLORID=9 "
                    StrSql = StrSql + "INNER JOIN BASECOLOR BC10 "
                    StrSql = StrSql + "ON BC10.BASECOLORID=10 "
                    StrSql = StrSql + "INNER JOIN BASECOLOR BC11 "
                    StrSql = StrSql + "ON BC11.BASECOLORID=11 "
                    StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                    StrSql = StrSql + "ON PREF.CASEID= " + CaseId.ToString()

                    'Bug#379# Start
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA1 "
                    'StrSql = StrSql + "ON BA1.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 1 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA1.BASECOLORID = 1 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA2 "
                    'StrSql = StrSql + "ON BA2.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 2 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA2.BASECOLORID = 2 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA3 "
                    'StrSql = StrSql + "ON BA3.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 3 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA3.BASECOLORID = 3 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA4 "
                    'StrSql = StrSql + "ON BA4.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 4 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA4.BASECOLORID = 4 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA5 "
                    'StrSql = StrSql + "ON BA5.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 5 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA5.BASECOLORID = 5 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA6 "
                    'StrSql = StrSql + "ON BA6.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 6 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA6.BASECOLORID = 6 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA7 "
                    'StrSql = StrSql + "ON BA7.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 7 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA7.BASECOLORID = 7 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA8 "
                    'StrSql = StrSql + "ON BA8.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 8 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA8.BASECOLORID = 8 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA9 "
                    'StrSql = StrSql + "ON BA9.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 9 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA9.BASECOLORID = 9 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA10 "
                    'StrSql = StrSql + "ON BA10.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 10 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA10.BASECOLORID = 10 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA11 "
                    'StrSql = StrSql + "ON BA11.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 11 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA11.BASECOLORID = 11"
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA1 "
                    StrSql = StrSql + "ON BA1.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(0).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA1.BASECOLORID = 1 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA2 "
                    StrSql = StrSql + "ON BA2.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(1).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA2.BASECOLORID = 2 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA3 "
                    StrSql = StrSql + "ON BA3.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(2).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA3.BASECOLORID = 3 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA4 "
                    StrSql = StrSql + "ON BA4.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(3).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA4.BASECOLORID = 4 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA5 "
                    StrSql = StrSql + "ON BA5.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(4).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA5.BASECOLORID = 5 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA6 "
                    StrSql = StrSql + "ON BA6.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(5).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA6.BASECOLORID = 6 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA7 "
                    StrSql = StrSql + "ON BA7.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(6).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA7.BASECOLORID = 7 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA8 "
                    StrSql = StrSql + "ON BA8.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(7).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA8.BASECOLORID = 8 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA9 "
                    StrSql = StrSql + "ON BA9.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(8).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA9.BASECOLORID = 9 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA10 "
                    StrSql = StrSql + "ON BA10.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(9).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA10.BASECOLORID = 10 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA11 "
                    StrSql = StrSql + "ON BA11.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(10).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA11.BASECOLORID = 11"
                    'Bug#379# End
                    StrSql = StrSql + "WHERE COLORNBR = CASE WHEN " + ColId.ToString() + " = -1 THEN "
                    StrSql = StrSql + "COLORNBR "
                    StrSql = StrSql + "ELSE "
                    StrSql = StrSql + "" + ColId.ToString() + " "
                    StrSql = StrSql + "END "
                    StrSql = StrSql + "ORDER BY  UPPER(COLORNAME) "
                    Dts = odbUtil.FillDataSet(StrSql, MoldE1Connection)
                End If
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1InkColorCalc:GetColors:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetColorPrefDetails(ByVal UserName As String, ByVal ColId As String, ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ds As New DataSet()
            Try
                StrSql = "SELECT BASECOLORID,DATECOL  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 1 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 2 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 2 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 3 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 3 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 4 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 4 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 5 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 5 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 6 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 6 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 7 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 7 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 8 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 8 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 9 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 9 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 10 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 10 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 11 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 11 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + ") "
                StrSql = StrSql + "ORDER BY BASECOLORID ASC "
                ds = odbUtil.FillDataSet(StrSql, MoldE1Connection)

                If ds.Tables(0).Rows.Count > 0 Then
                    StrSql = "SELECT COLORID, (COLORID||'  '||COLORNAME)COLDES,COLORNAME,PMS,  "
                    StrSql = StrSql + "BCOLOR1, "
                    StrSql = StrSql + "BCOLOR2, "
                    StrSql = StrSql + "BCOLOR3, "
                    StrSql = StrSql + "BCOLOR4, "
                    StrSql = StrSql + "BCOLOR5, "
                    StrSql = StrSql + "BCOLOR6, "
                    StrSql = StrSql + "BCOLOR7, "
                    StrSql = StrSql + "BCOLOR8, "
                    StrSql = StrSql + "BCOLOR9, "
                    StrSql = StrSql + "BCOLOR10, "
                    StrSql = StrSql + "BCOLOR11, "
                    'Bug#379# start
                    StrSql = StrSql + "(BA1.WETPRICE) WETPS1, "
                    StrSql = StrSql + "(BA2.WETPRICE) WETPS2, "
                    StrSql = StrSql + "(BA3.WETPRICE) WETPS3, "
                    StrSql = StrSql + "(BA4.WETPRICE) WETPS4, "
                    StrSql = StrSql + "(BA5.WETPRICE) WETPS5, "
                    StrSql = StrSql + "(BA6.WETPRICE) WETPS6, "
                    StrSql = StrSql + "(BA7.WETPRICE) WETPS7, "
                    StrSql = StrSql + "(BA8.WETPRICE) WETPS8, "
                    StrSql = StrSql + "(BA9.WETPRICE) WETPS9, "
                    StrSql = StrSql + "(BA10.WETPRICE) WETPS10, "
                    StrSql = StrSql + "(BA11.WETPRICE) WETPS11, "
                    'Bug#379# End
                    StrSql = StrSql + "INKP.WETPRICE1 WETPP1, "
                    StrSql = StrSql + "INKP.WETPRICE2 WETPP2, "
                    StrSql = StrSql + "INKP.WETPRICE3 WETPP3, "
                    StrSql = StrSql + "INKP.WETPRICE4 WETPP4, "
                    StrSql = StrSql + "INKP.WETPRICE5 WETPP5, "
                    StrSql = StrSql + "INKP.WETPRICE6 WETPP6, "
                    StrSql = StrSql + "INKP.WETPRICE7 WETPP7, "
                    StrSql = StrSql + "INKP.WETPRICE8 WETPP8, "
                    StrSql = StrSql + "INKP.WETPRICE9 WETPP9, "
                    StrSql = StrSql + "INKP.WETPRICE10 WETPP10, "
                    StrSql = StrSql + "INKP.WETPRICE11 WETPP11, "
                    'Bug#379# Start
                    StrSql = StrSql + "BA1.PERSOL PERSOLS1, "
                    StrSql = StrSql + "BA2.PERSOL PERSOLS2, "
                    StrSql = StrSql + "BA3.PERSOL PERSOLS3, "
                    StrSql = StrSql + "BA4.PERSOL PERSOLS4, "
                    StrSql = StrSql + "BA5.PERSOL PERSOLS5, "
                    StrSql = StrSql + "BA6.PERSOL PERSOLS6, "
                    StrSql = StrSql + "BA7.PERSOL PERSOLS7, "
                    StrSql = StrSql + "BA8.PERSOL PERSOLS8, "
                    StrSql = StrSql + "BA9.PERSOL PERSOLS9, "
                    StrSql = StrSql + "BA10.PERSOL PERSOLS10, "
                    StrSql = StrSql + "BA11.PERSOL PERSOLS11, "
                    'Bug#379# End
                    StrSql = StrSql + "INKP.PERSOL1 PERSOLP1, "
                    StrSql = StrSql + "INKP.PERSOL2 PERSOLP2, "
                    StrSql = StrSql + "INKP.PERSOL3 PERSOLP3, "
                    StrSql = StrSql + "INKP.PERSOL4 PERSOLP4, "
                    StrSql = StrSql + "INKP.PERSOL5 PERSOLP5, "
                    StrSql = StrSql + "INKP.PERSOL6 PERSOLP6, "
                    StrSql = StrSql + "INKP.PERSOL7 PERSOLP7, "
                    StrSql = StrSql + "INKP.PERSOL8 PERSOLP8, "
                    StrSql = StrSql + "INKP.PERSOL9 PERSOLP9, "
                    StrSql = StrSql + "INKP.PERSOL10 PERSOLP10, "
                    StrSql = StrSql + "INKP.PERSOL11 PERSOLP11 "


                    StrSql = StrSql + "FROM COLORDETAILS "
                    StrSql = StrSql + "INNER JOIN INKPREFERENCES INKP "
                    StrSql = StrSql + "ON UPPER(INKP.USERNAME)='" + UserName.ToString().ToUpper() + "' "

                    'StrSql = StrSql + "INNER JOIN BASECOLOR B1 "
                    'StrSql = StrSql + "ON B1.BASECOLORID=1 "
                    'StrSql = StrSql + "INNER JOIN BASECOLOR B2 "
                    'StrSql = StrSql + "ON B2.BASECOLORID=2 "
                    'StrSql = StrSql + "INNER JOIN BASECOLOR B3 "
                    'StrSql = StrSql + "ON B3.BASECOLORID=3 "
                    'StrSql = StrSql + "INNER JOIN BASECOLOR B4 "
                    'StrSql = StrSql + "ON B4.BASECOLORID=4 "
                    'StrSql = StrSql + "INNER JOIN BASECOLOR B5 "
                    'StrSql = StrSql + "ON B5.BASECOLORID=5 "
                    'StrSql = StrSql + "INNER JOIN BASECOLOR B6 "
                    'StrSql = StrSql + "ON B6.BASECOLORID=6 "
                    'StrSql = StrSql + "INNER JOIN BASECOLOR B7 "
                    'StrSql = StrSql + "ON B7.BASECOLORID=7 "
                    'StrSql = StrSql + "INNER JOIN BASECOLOR B8 "
                    'StrSql = StrSql + "ON B8.BASECOLORID=8 "
                    'StrSql = StrSql + "INNER JOIN BASECOLOR B9 "
                    'StrSql = StrSql + "ON B9.BASECOLORID=9 "
                    'StrSql = StrSql + "INNER JOIN BASECOLOR B10 "
                    'StrSql = StrSql + "ON B10.BASECOLORID=10 "
                    'StrSql = StrSql + "INNER JOIN BASECOLOR B11 "
                    'StrSql = StrSql + "ON B11.BASECOLORID=11 "

                    'Bug#379# Start
                    StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                    StrSql = StrSql + "ON PREF.CASEID=" + CaseId + " "

                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA1 "
                    'StrSql = StrSql + "ON BA1.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 1 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA1.BASECOLORID = 1 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA2 "
                    'StrSql = StrSql + "ON BA2.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 2 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA2.BASECOLORID = 2 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA3 "
                    'StrSql = StrSql + "ON BA3.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 3 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA3.BASECOLORID = 3 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA4 "
                    'StrSql = StrSql + "ON BA4.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 4 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA4.BASECOLORID = 4 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA5 "
                    'StrSql = StrSql + "ON BA5.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 5 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA5.BASECOLORID = 5 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA6 "
                    'StrSql = StrSql + "ON BA6.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 6 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA6.BASECOLORID = 6 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA7 "
                    'StrSql = StrSql + "ON BA7.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 7 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA7.BASECOLORID = 7 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA8 "
                    'StrSql = StrSql + "ON BA8.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 8 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA8.BASECOLORID = 8 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA9 "
                    'StrSql = StrSql + "ON BA9.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 9 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA9.BASECOLORID = 9 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA10 "
                    'StrSql = StrSql + "ON BA10.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 10 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA10.BASECOLORID = 10 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA11 "
                    'StrSql = StrSql + "ON BA11.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 11 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA11.BASECOLORID = 11"
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA1 "
                    StrSql = StrSql + "ON BA1.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(0).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA1.BASECOLORID = 1 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA2 "
                    StrSql = StrSql + "ON BA2.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(1).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA2.BASECOLORID = 2 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA3 "
                    StrSql = StrSql + "ON BA3.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(2).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA3.BASECOLORID = 3 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA4 "
                    StrSql = StrSql + "ON BA4.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(3).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA4.BASECOLORID = 4 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA5 "
                    StrSql = StrSql + "ON BA5.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(4).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA5.BASECOLORID = 5 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA6 "
                    StrSql = StrSql + "ON BA6.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(5).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA6.BASECOLORID = 6 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA7 "
                    StrSql = StrSql + "ON BA7.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(6).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA7.BASECOLORID = 7 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA8 "
                    StrSql = StrSql + "ON BA8.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(7).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA8.BASECOLORID = 8 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA9 "
                    StrSql = StrSql + "ON BA9.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(8).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA9.BASECOLORID = 9 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA10 "
                    StrSql = StrSql + "ON BA10.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(9).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA10.BASECOLORID = 10 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA11 "
                    StrSql = StrSql + "ON BA11.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(10).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA11.BASECOLORID = 11"
                    'Bug#379# End

                    StrSql = StrSql + "WHERE COLORNBR = CASE WHEN " + ColId.ToString() + " = -1 THEN "
                    StrSql = StrSql + "COLORNBR "
                    StrSql = StrSql + "ELSE "
                    StrSql = StrSql + "" + ColId.ToString() + " "
                    StrSql = StrSql + "END "
                    StrSql = StrSql + "ORDER BY  UPPER(COLORNAME) "


                    Dts = odbUtil.FillDataSet(StrSql, MoldE1Connection)
                End If
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetColorPrefDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region
    End Class

End Class
