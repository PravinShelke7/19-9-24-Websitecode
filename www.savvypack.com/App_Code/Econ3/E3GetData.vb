Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Public Class E3GetData
    Public Class Selectdata

#Region "E3 new result "

        Public Function GetCasesByPLMGrp(ByVal GrpID As String, ByVal UserId As String) As String()
            Dim CaseIDs() As String
            Dim i As New Integer
            Try

                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
                Dim StrSqlCases As String = ""

                StrSqlCases = StrSqlCases + "SELECT PC.CASEID "
                StrSqlCases = StrSqlCases + "FROM PERMISSIONSCASES PC "
                StrSqlCases = StrSqlCases + "INNER JOIN PREFERENCES P ON P.CASEID=PC.CASEID "
                StrSqlCases = StrSqlCases + "WHERE PACKSPECGRPID=" + GrpID.ToString() + " AND USERID IN (SELECT USERID FROM USERS WHERE LICENSEID IN (SELECT SECONDARYLICENSEID FROM USERS WHERE USERID= " + UserId.ToString() + " )) "
                StrSqlCases = StrSqlCases + "ORDER BY EFFDATE ASC "
                Dim Cs As New DataTable()
                Cs = odbUtil.FillDataTable(StrSqlCases, MyConnectionString)
                ReDim CaseIDs(Cs.Rows.Count - 1)
                For i = 0 To Cs.Rows.Count - 1
                    CaseIDs(i) = Cs.Rows(i).Item("CASEID").ToString()
                Next

                Return CaseIDs
            Catch ex As Exception
                Return CaseIDs
            End Try



        End Function

#End Region

        Public Function GetCaseDetails(ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Try
                StrSql = "SELECT A.CASEID,CASEDE1,(CASEDE1||' ' ||CASEDE2)CASEDES,CASEDE3,CASEDE2,CASETYPE  ,COUNTRYDES "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT  CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,'Base Case' CASETYPE FROM BASECASES "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT DISTINCT CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,'Proprietary Case' CASETYPE FROM PERMISSIONSCASES "
                StrSql = StrSql + ") "
                StrSql = StrSql + "A INNER JOIN PREFERENCES P ON P.CASEID=A.CASEID "
                StrSql = StrSql + "INNER JOIN ADMINSITE.DIMCOUNTRIES DC ON DC.COUNTRYID =P.OCOUNTRY "
                StrSql = StrSql + "WHERE A.CASEID  IN (" + CaseId.ToString() + ") "
                StrSql = StrSql + "ORDER BY CASEID "

                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetCaseDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetExtrusionDetails(ByVal CaseId As String) As DataSet
            Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  MAT.CASEID,  "
                StrSql = StrSql + "TO_CHAR(MAT.EFFDATE,'MON DD,YYYY')AS EDATE, "
                StrSql = StrSql + "MAT.M1, "
                StrSql = StrSql + "MAT.M2, "
                StrSql = StrSql + "MAT.M3, "
                StrSql = StrSql + "MAT.M4, "
                StrSql = StrSql + "MAT.M5, "
                StrSql = StrSql + "MAT.M6, "
                StrSql = StrSql + "MAT.M7, "
                StrSql = StrSql + "MAT.M8, "
                StrSql = StrSql + "MAT.M9, "
                StrSql = StrSql + "MAT.M10, "
                StrSql = StrSql + "(MAT.T1*PREF.CONVTHICK) AS THICK1, "
                StrSql = StrSql + "(MAT.T2*PREF.CONVTHICK) AS THICK2, "
                StrSql = StrSql + "(MAT.T3*PREF.CONVTHICK) AS THICK3, "
                StrSql = StrSql + "(MAT.T4*PREF.CONVTHICK) AS THICK4, "
                StrSql = StrSql + "(MAT.T5*PREF.CONVTHICK) AS THICK5, "
                StrSql = StrSql + "(MAT.T6*PREF.CONVTHICK) AS THICK6, "
                StrSql = StrSql + "(MAT.T7*PREF.CONVTHICK) AS THICK7, "
                StrSql = StrSql + "(MAT.T8*PREF.CONVTHICK) AS THICK8, "
                StrSql = StrSql + "(MAT.T9*PREF.CONVTHICK) AS THICK9, "
                StrSql = StrSql + "(MAT.T10*PREF.CONVTHICK) AS THICK10, "
                StrSql = StrSql + "(TOT.THICK*PREF.CONVTHICK)THICK, "
                StrSql = StrSql + "(NVL(MATA1.PRICE,0)/PREF.CONVWT*PREF.CURR)AS PRS1, "
                StrSql = StrSql + "(NVL(MATA2.PRICE,0)/PREF.CONVWT*PREF.CURR)AS PRS2, "
                StrSql = StrSql + "(NVL(MATA3.PRICE,0)/PREF.CONVWT*PREF.CURR)AS PRS3, "
                StrSql = StrSql + "(NVL(MATA4.PRICE,0)/PREF.CONVWT*PREF.CURR)AS PRS4, "
                StrSql = StrSql + "(NVL(MATA5.PRICE,0)/PREF.CONVWT*PREF.CURR)AS PRS5, "
                StrSql = StrSql + "(NVL(MATA6.PRICE,0)/PREF.CONVWT*PREF.CURR)AS PRS6, "
                StrSql = StrSql + "(NVL(MATA7.PRICE,0)/PREF.CONVWT*PREF.CURR)AS PRS7, "
                StrSql = StrSql + "(NVL(MATA8.PRICE,0)/PREF.CONVWT*PREF.CURR)AS PRS8, "
                StrSql = StrSql + "(NVL(MATA9.PRICE,0)/PREF.CONVWT*PREF.CURR)AS PRS9, "
                StrSql = StrSql + "(NVL(MATA10.PRICE,0)/PREF.CONVWT*PREF.CURR)AS PRS10, "
                StrSql = StrSql + "(MAT.S1/PREF.CONVWT*PREF.CURR) AS PRP1, "
                StrSql = StrSql + "(MAT.S2/PREF.CONVWT*PREF.CURR) AS PRP2, "
                StrSql = StrSql + "(MAT.S3/PREF.CONVWT*PREF.CURR) AS PRP3, "
                StrSql = StrSql + "(MAT.S4/PREF.CONVWT*PREF.CURR) AS PRP4, "
                StrSql = StrSql + "(MAT.S5/PREF.CONVWT*PREF.CURR) AS PRP5, "
                StrSql = StrSql + "(MAT.S6/PREF.CONVWT*PREF.CURR) AS PRP6, "
                StrSql = StrSql + "(MAT.S7/PREF.CONVWT*PREF.CURR) AS PRP7, "
                StrSql = StrSql + "(MAT.S8/PREF.CONVWT*PREF.CURR) AS PRP8, "
                StrSql = StrSql + "(MAT.S9/PREF.CONVWT*PREF.CURR) AS PRP9, "
                StrSql = StrSql + "(MAT.S10/PREF.CONVWT*PREF.CURR) AS PRP10, "
                StrSql = StrSql + "MAT.R1, "
                StrSql = StrSql + "MAT.R2, "
                StrSql = StrSql + "MAT.R3, "
                StrSql = StrSql + "MAT.R4, "
                StrSql = StrSql + "MAT.R5, "
                StrSql = StrSql + "MAT.R6, "
                StrSql = StrSql + "MAT.R7, "
                StrSql = StrSql + "MAT.R8, "
                StrSql = StrSql + "MAT.R9, "
                StrSql = StrSql + "MAT.R10, "
                StrSql = StrSql + "MAT.E1, "
                StrSql = StrSql + "MAT.E2, "
                StrSql = StrSql + "MAT.E3, "
                StrSql = StrSql + "MAT.E4, "
                StrSql = StrSql + "MAT.E5, "
                StrSql = StrSql + "MAT.E6, "
                StrSql = StrSql + "MAT.E7, "
                StrSql = StrSql + "MAT.E8, "
                StrSql = StrSql + "MAT.E9, "
                StrSql = StrSql + "MAT.E10, "
                StrSql = StrSql + "(MAT1.SG)AS SGS1, "
                StrSql = StrSql + "(MAT2.SG)AS SGS2, "
                StrSql = StrSql + "(MAT3.SG)AS SGS3, "
                StrSql = StrSql + "(MAT4.SG)AS SGS4, "
                StrSql = StrSql + "(MAT5.SG)AS SGS5, "
                StrSql = StrSql + "(MAT6.SG)AS SGS6, "
                StrSql = StrSql + "(MAT7.SG)AS SGS7, "
                StrSql = StrSql + "(MAT8.SG)AS SGS8, "
                StrSql = StrSql + "(MAT9.SG)AS SGS9, "
                StrSql = StrSql + "(MAT10.SG)AS SGS10, "
                StrSql = StrSql + "MAT.SG1 AS SGP1, "
                StrSql = StrSql + "MAT.SG2 AS SGP2, "
                StrSql = StrSql + "MAT.SG3 AS SGP3, "
                StrSql = StrSql + "MAT.SG4 AS SGP4, "
                StrSql = StrSql + "MAT.SG5 AS SGP5, "
                StrSql = StrSql + "MAT.SG6 AS SGP6, "
                StrSql = StrSql + "MAT.SG7 AS SGP7, "
                StrSql = StrSql + "MAT.SG8 AS SGP8, "
                StrSql = StrSql + "MAT.SG9 AS SGP9, "
                StrSql = StrSql + "MAT.SG10 AS SGP10, "
                StrSql = StrSql + "(MATOUT.M1*PREF.CONVWT/PREF.CONVAREA) AS WTPARA1, "
                StrSql = StrSql + "(MATOUT.M2*PREF.CONVWT/PREF.CONVAREA) AS WTPARA2, "
                StrSql = StrSql + "(MATOUT.M3*PREF.CONVWT/PREF.CONVAREA) AS WTPARA3, "
                StrSql = StrSql + "(MATOUT.M4*PREF.CONVWT/PREF.CONVAREA) AS WTPARA4, "
                StrSql = StrSql + "(MATOUT.M5*PREF.CONVWT/PREF.CONVAREA) AS WTPARA5, "
                StrSql = StrSql + "(MATOUT.M6*PREF.CONVWT/PREF.CONVAREA) AS WTPARA6, "
                StrSql = StrSql + "(MATOUT.M7*PREF.CONVWT/PREF.CONVAREA) AS WTPARA7, "
                StrSql = StrSql + "(MATOUT.M8*PREF.CONVWT/PREF.CONVAREA) AS WTPARA8, "
                StrSql = StrSql + "(MATOUT.M9*PREF.CONVWT/PREF.CONVAREA) AS WTPARA9, "
                StrSql = StrSql + "(MATOUT.M10*PREF.CONVWT/PREF.CONVAREA) AS WTPARA10, "
                StrSql = StrSql + "(TOT.WTPERAREA*PREF.CONVWT/PREF.CONVAREA)WTPERAREA, "
                StrSql = StrSql + "MAT.D1, "
                StrSql = StrSql + "MAT.D2, "
                StrSql = StrSql + "MAT.D3, "
                StrSql = StrSql + "MAT.D4, "
                StrSql = StrSql + "MAT.D5, "
                StrSql = StrSql + "MAT.D6, "
                StrSql = StrSql + "MAT.D7, "
                StrSql = StrSql + "MAT.D8, "
                StrSql = StrSql + "MAT.D9, "
                StrSql = StrSql + "MAT.D10, "
                StrSql = StrSql + "TO_CHAR(MAT.EFFDATE,'MON DD, YYYY')EFFDATE, "
                StrSql = StrSql + "MAT.PLATE, "
                StrSql = StrSql + "MAT.DISCMATYN, "
                StrSql = StrSql + "TOT.DISCRETEWT * PREF.CONVWT AS DISCTOTAL, "
                StrSql = StrSql + "TOT.DISCRETECOST, "
                StrSql = StrSql + "MATDESC.DISID1, "
                StrSql = StrSql + "MATDESC.DISID2, "
                StrSql = StrSql + "MATDESC.DISID3, "
                StrSql = StrSql + "MATDESC.DISW1* PREF.CONVWT AS DISW1, "
                StrSql = StrSql + "MATDESC.DISW2* PREF.CONVWT AS DISW2, "
                StrSql = StrSql + "MATDESC.DISW3* PREF.CONVWT AS DISW3, "
                StrSql = StrSql + "MATDESC.DISP1* PREF.CURR AS DISP1, "
                StrSql = StrSql + "MATDESC.DISP2* PREF.CURR AS DISP2, "
                StrSql = StrSql + "MATDESC.DISP3* PREF.CURR AS DISP3, "
                StrSql = StrSql + "PREF.TITLE1, "
                StrSql = StrSql + "PREF.TITLE3, "
                StrSql = StrSql + "PREF.TITLE2, "
                StrSql = StrSql + "PREF.TITLE4, "
                StrSql = StrSql + "PREF.TITLE5, "
                StrSql = StrSql + "PREF.TITLE6, "
                StrSql = StrSql + "PREF.TITLE7, "
                StrSql = StrSql + "PREF.TITLE8, "
                StrSql = StrSql + "PREF.TITLE9, "
                StrSql = StrSql + "PREF.TITLE10, "
                StrSql = StrSql + "PREF.TITLE11, "
                StrSql = StrSql + "PREF.TITLE12, "
                StrSql = StrSql + "PREF.UNITS, "

                'Barrier
                StrSql = StrSql + "(NVL(TOTTB.PRICE,0)/PREF.CONVWT*PREF.CURR)AS PRICE, "
                StrSql = StrSql + "(MATOUT.AM1*PREF.CURR/PREF.CONVAREA) AS COSTPARA1, "
                StrSql = StrSql + "(MATOUT.AM2*PREF.CURR/PREF.CONVAREA) AS COSTPARA2, "
                StrSql = StrSql + "(MATOUT.AM3*PREF.CURR/PREF.CONVAREA) AS COSTPARA3, "
                StrSql = StrSql + "(MATOUT.AM4*PREF.CURR/PREF.CONVAREA) AS COSTPARA4, "
                StrSql = StrSql + "(MATOUT.AM5*PREF.CURR/PREF.CONVAREA) AS COSTPARA5, "
                StrSql = StrSql + "(MATOUT.AM6*PREF.CURR/PREF.CONVAREA) AS COSTPARA6, "
                StrSql = StrSql + "(MATOUT.AM7*PREF.CURR/PREF.CONVAREA) AS COSTPARA7, "
                StrSql = StrSql + "(MATOUT.AM8*PREF.CURR/PREF.CONVAREA) AS COSTPARA8, "
                StrSql = StrSql + "(MATOUT.AM9*PREF.CURR/PREF.CONVAREA) AS COSTPARA9, "
                StrSql = StrSql + "(MATOUT.AM10*PREF.CURR/PREF.CONVAREA) AS COSTPARA10, "
                StrSql = StrSql + "(TOTTB.PRICEAREA*PREF.CURR/PREF.CONVAREA)COSTPARAREA , "
                StrSql = StrSql + "PREF.CONVAREA, "
                StrSql = StrSql + "PREF.TITLE19, "
                StrSql = StrSql + "PREF.TITLE20, "
                StrSql = StrSql + "OTR1, "
                StrSql = StrSql + "OTR2, "
                StrSql = StrSql + "OTR3, "
                StrSql = StrSql + "OTR4 , "
                StrSql = StrSql + "OTR5, "
                StrSql = StrSql + "OTR6 , "
                StrSql = StrSql + "OTR7, "
                StrSql = StrSql + "OTR8, "
                StrSql = StrSql + "OTR9, "
                StrSql = StrSql + "OTR10, "
                StrSql = StrSql + "WVTR1, "
                StrSql = StrSql + "WVTR2, "
                StrSql = StrSql + "WVTR3, "
                StrSql = StrSql + "WVTR4, "
                StrSql = StrSql + "WVTR5, "
                StrSql = StrSql + "WVTR6, "
                StrSql = StrSql + "WVTR7, "
                StrSql = StrSql + "WVTR8, "
                StrSql = StrSql + "WVTR9, "
                StrSql = StrSql + "WVTR10, "

                StrSql = StrSql + "NVL(GRADE1,0) GRADE1, "
                StrSql = StrSql + "NVL(GRADE2,0) GRADE2, "
                StrSql = StrSql + "NVL(GRADE3,0) GRADE3, "
                StrSql = StrSql + "NVL(GRADE4,0) GRADE4, "
                StrSql = StrSql + "NVL(GRADE5,0) GRADE5, "
                StrSql = StrSql + "NVL(GRADE6,0) GRADE6, "
                StrSql = StrSql + "NVL(GRADE7,0) GRADE7, "
                StrSql = StrSql + "NVL(GRADE8,0) GRADE8, "
                StrSql = StrSql + "NVL(GRADE9,0) GRADE9, "
                StrSql = StrSql + "NVL(GRADE10,0) GRADE10, "
                StrSql = StrSql + "MAT.OTRTEMP, "
                StrSql = StrSql + "MAT.WVTRTEMP, "
                StrSql = StrSql + "MAT.OTRRH, "
                StrSql = StrSql + "MAT.WVTRRH "

                StrSql = StrSql + "FROM MATERIALINPUT MAT "

                'Barrier
                StrSql = StrSql + "INNER JOIN BARRIERINPUT BI "
                StrSql = StrSql + "ON MAT.CASEID=BI.CASEID "

                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=MAT.CASEID "
                StrSql = StrSql + "INNER JOIN TOTAL TOT "
                StrSql = StrSql + "ON TOT.CASEID = MAT.CASEID "

                'Bug#441
                StrSql = StrSql + "INNER JOIN TOTALTB TOTTB "
                StrSql = StrSql + "ON TOTTB.CASEID = MAT.CASEID "

                StrSql = StrSql + "INNER JOIN MATERIALDISIN MATDESC "
                StrSql = StrSql + "ON MATDESC.CASEID = MAT.CASEID "
                StrSql = StrSql + "INNER JOIN MATERIALOUTPUT MATOUT "
                StrSql = StrSql + "ON MATOUT.CASEID=MAT.CASEID "
                StrSql = StrSql + "LEFT OUTER JOIN MATERIALSARCH MATA1 "
                StrSql = StrSql + "ON MATA1.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA1.MATID = MAT.M1 "
                StrSql = StrSql + "LEFT OUTER JOIN MATERIALSARCH MATA2 "
                StrSql = StrSql + "ON MATA2.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA2.MATID = MAT.M2 "
                StrSql = StrSql + "LEFT OUTER JOIN MATERIALSARCH MATA3 "
                StrSql = StrSql + "ON MATA3.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA3.MATID = MAT.M3 "
                StrSql = StrSql + "LEFT OUTER JOIN MATERIALSARCH MATA4 "
                StrSql = StrSql + "ON MATA4.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA4.MATID = MAT.M4 "
                StrSql = StrSql + "LEFT OUTER JOIN MATERIALSARCH MATA5 "
                StrSql = StrSql + "ON MATA5.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA5.MATID = MAT.M5 "
                StrSql = StrSql + "LEFT OUTER JOIN MATERIALSARCH MATA6 "
                StrSql = StrSql + "ON MATA6.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA6.MATID = MAT.M6 "
                StrSql = StrSql + "LEFT OUTER JOIN MATERIALSARCH MATA7 "
                StrSql = StrSql + "ON MATA7.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA7.MATID = MAT.M7 "
                StrSql = StrSql + "LEFT OUTER JOIN MATERIALSARCH MATA8 "
                StrSql = StrSql + "ON MATA8.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA8.MATID = MAT.M8 "
                StrSql = StrSql + "LEFT OUTER JOIN MATERIALSARCH MATA9 "
                StrSql = StrSql + "ON MATA9.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA9.MATID = MAT.M9 "
                StrSql = StrSql + "LEFT OUTER JOIN MATERIALSARCH MATA10 "
                StrSql = StrSql + "ON MATA10.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA10.MATID = MAT.M10 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT1 "
                StrSql = StrSql + "ON MAT1.MATID = MAT.M1 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT2 "
                StrSql = StrSql + "ON MAT2.MATID = MAT.M2 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT3 "
                StrSql = StrSql + "ON MAT3.MATID = MAT.M3 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT4 "
                StrSql = StrSql + "ON MAT4.MATID = MAT.M4 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT5 "
                StrSql = StrSql + "ON MAT5.MATID = MAT.M5 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT6 "
                StrSql = StrSql + "ON MAT6.MATID = MAT.M6 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT7 "
                StrSql = StrSql + "ON MAT7.MATID = MAT.M7 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT8 "
                StrSql = StrSql + "ON MAT8.MATID = MAT.M8 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT9 "
                StrSql = StrSql + "ON MAT9.MATID = MAT.M9 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT10 "
                StrSql = StrSql + "ON MAT10.MATID = MAT.M10 "
                StrSql = StrSql + "WHERE MAT.CASEID IN (" + CaseId.ToString() + ") "

                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function CompCOLWIDTH(ByVal ID As String) As DataSet
            Dim odbUtil As New DBUtil()
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
            Dim StrSqlCases As String = ""
            Dim ds As New DataSet
            Try
                StrSqlCases = "SELECT COLWIDTH FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                ds = odbUtil.FillDataSet(StrSqlCases, MyConnectionString)
                Return ds
            Catch ex As Exception
                Return ds
            End Try
        End Function

        Public Function GetUsernamePassword(ByVal Id As String) As DataTable
            Dim Dts As New DataTable()
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
                Dim StrSql As String = "Select Uname,Upwd From Ulogin Where ID= " & Id
                Dts = odbUtil.FillDataTable(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function 

        Public Function GetInuseCount(ByVal UserId As String) As DataTable
            Dim Dts As New DataTable
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

                Dim StrSql As String = "Select * from inuse where USERID=" + UserId.ToString() + ""
                Dts = odbUtil.FillDataTable(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function

Public Function GetCases(ByVal UserId As String) As DataTable
            Dim Dts As New DataTable
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

                'Dim StrSql As String = "SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE FROM PERMISSIONSCASES WHERE USERID=" + UserId.ToString() + " "
                'StrSql = StrSql + "AND SERVICEID IS NULL "
                'StrSql = StrSql + "UNION SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE  FROM BASECASES ORDER BY caseDE1"
	              Dim StrSql As String = "SELECT A.CASEID,CASEDE1,CASEDE "
                StrSql = StrSql + " FROM ( SELECT PW.CASEID,CASEDE1,('CASE:'||PW.CASEID||' - '|| CASEDE1||' ' || CASEDE2 ||' - ' || DC.COUNTRYDES  ) AS CASEDE FROM PERMISSIONSCASES PW"
                StrSql = StrSql + " INNER JOIN PREFERENCES P ON P.CASEID=PW.CASEID"
                StrSql = StrSql + " INNER JOIN ADMINSITE.DIMCOUNTRIES DC ON DC.COUNTRYID =P.OCOUNTRY"
                StrSql = StrSql + " WHERE USERID=" + UserId.ToString() + " "
                StrSql = StrSql + " AND SERVICEID IS NULL "
                StrSql = StrSql + " UNION SELECT B.CASEID,CASEDE1,('CASE:'||B.CASEID||' - '|| CASEDE1||' ' || CASEDE2 ||' - ' || DC1.COUNTRYDES  ) AS CASEDE  FROM BASECASES B "
                StrSql = StrSql + " INNER JOIN PREFERENCES P ON P.CASEID=B.CASEID"
                StrSql = StrSql + " INNER JOIN ADMINSITE.DIMCOUNTRIES DC1 ON DC1.COUNTRYID =P.OCOUNTRY    )A "
                StrSql = StrSql + " ORDER BY caseDE1"
                Dts = odbUtil.FillDataTable(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
        Public Function GetCasesOLDSep2023(ByVal UserId As String) As DataTable
            Dim Dts As New DataTable
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

                Dim StrSql As String = "SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE FROM PERMISSIONSCASES WHERE USERID=" + UserId.ToString() + " "
                StrSql = StrSql + "AND SERVICEID IS NULL "
                StrSql = StrSql + "UNION SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE  FROM BASECASES ORDER BY caseDE1"

                Dts = odbUtil.FillDataTable(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function

        Public Function GetCasesByGrpIDs(ByVal UserId As String, ByVal GrpID As String) As DataTable
            Dim Dts As New DataTable
            Dim StrSql As String = String.Empty
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

                StrSql = "SELECT PR.CASEID,PR.CASEDE1,('CASE:'||PR.CASEID||' - '|| PR.CASEDE1||' ' || PR.CASEDE2) AS CASEDE  "
                StrSql = StrSql + "FROM PERMISSIONSCASES PR "
                StrSql = StrSql + "INNER JOIN ECON.USERS USR ON USR.USERID=PR.USERID "
                StrSql = StrSql + "LEFT OUTER JOIN ( "
                StrSql = StrSql + "SELECT GROUPS.GROUPID,GROUPS.DES1,GROUPCASES.CASEID ,GROUPS.USERID "
                StrSql = StrSql + "FROM GROUPS "
                StrSql = StrSql + "INNER JOIN GROUPCASES ON GROUPS.GROUPID=GROUPCASES.GROUPID "
                StrSql = StrSql + "INNER JOIN ECON.USERS ON USERS.USERID=GROUPS.USERID "
                StrSql = StrSql + "INNER JOIN PERMISSIONSCASES PRR ON PRR.CASEID=GROUPCASES.CASEID AND PRR.USERID=USERS.USERID "
                StrSql = StrSql + ") GRPS ON GRPS.CASEID=PR.CASEID AND GRPS.USERID=USR.USERID "
                StrSql = StrSql + "WHERE GRPS.GROUPID IN (" + GrpID + ") AND PR.USERID=" + UserId + " "
                StrSql = StrSql + "AND PR.SERVICEID IS NULL"

                Dts = odbUtil.FillDataTable(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function

        Public Function GetCasesToCompare(ByVal UserId As String, ByVal CaseIds As String) As DataTable
            Dim Dts As New DataTable
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

                Dim StrSql As String = String.Empty
                StrSql = "SELECT CASEID,  "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "CASEDE "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT CASEID, "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE USERID = " + UserId.ToString() + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASEID, "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + ")A "
                StrSql = StrSql + "WHERE CASEID NOT IN (" + CaseIds + ") "
                StrSql = StrSql + "ORDER BY CASEDE1 "
                Dts = odbUtil.FillDataTable(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function

        Public Function GetCouser(ByVal UserName As String) As DataTable
            Dim Dts As New DataTable
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")


                Dim StrSql As String = " Select Users.Username , Users.userid From Users  where exists"
                StrSql = StrSql + " (select 1 from UserPermissions "
                StrSql = StrSql + " Inner Join Services on Services.SERVICEID = UserPermissions.SERVICEID "
                StrSql = StrSql + " where UserPermissions.UserID =Users.UserID and  Services.SERVICEDE='ECON3'  "
                StrSql = StrSql + "AND USERS.LICENSEID=(SELECT LICENSEID FROM USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper.ToString() + "') "
                'StrSql = StrSql + " and Users.company IN(select Users.company from users where username='" + UserName + "')"
                StrSql = StrSql + " )order by  Users.Username"

                Dts = odbUtil.FillDataTable(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function

        Public Function GetDescription(ByVal AssumptionID As String) As String
            Dim Des As String = ""
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")

                Dim StrSql As String = "Select DESCRIPTION from ASSUMPTIONS where ASSUMPTIONID = " + AssumptionID.ToString() + " "
                Dim Dts As New DataTable()
                Dts = odbUtil.FillDataTable(StrSql, MyConnectionString)
                Des = Dts.Rows(0).Item("DESCRIPTION")
                Return Des
            Catch ex As Exception
                Throw
                Return Des
            End Try
        End Function

	Public Function BulkCases(ByVal GrpID As String) As String()
            Dim CaseIDs() As String
            Dim i As New Integer
            Try

                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

                Dim StrSql As String = ""
                'StrSqlCases = "select to_char( nvl(Assumptions.Case1,0) )  ||  ',' || to_char( nvl(Assumptions.Case2,0))    ||   ',' ||  to_char( nvl(Assumptions.Case3,0) )  ||   ',' || to_char( nvl(Assumptions.Case4,0) )  ||  ',' ||  to_char( nvl(Assumptions.Case5,0) )  ||  ',' ||  to_char ( nvl(Assumptions.Case6,0) ) ||   ',' || to_char( nvl(Assumptions.Case7,0)  )  ||   ','  ||  to_char( nvl(Assumptions.Case8,0)  )  ||  ',' || to_char( nvl(Assumptions.Case9,0) )   ||   ','  || to_Char( nvl(Assumptions.Case10,0) )  as Cases   from  Assumptions WHERE Assumptions.AssumptionId =" + ID + ""

                StrSql = "SELECT PR.CASEID,PR.CASEDE1,('CASE:'||PR.CASEID||' - '|| PR.CASEDE1||' ' || PR.CASEDE2) AS CASEDE,GRPS.DES1 "
                StrSql = StrSql + "FROM PERMISSIONSCASES PR "
                StrSql = StrSql + "INNER JOIN ECON.USERS USR ON USR.USERID=PR.USERID "
                StrSql = StrSql + "LEFT OUTER JOIN ( "
                StrSql = StrSql + "SELECT GROUPS.GROUPID,GROUPS.DES1,GROUPCASES.CASEID ,GROUPS.USERID "
                StrSql = StrSql + "FROM GROUPS "
                StrSql = StrSql + "INNER JOIN GROUPCASES ON GROUPS.GROUPID=GROUPCASES.GROUPID "
                StrSql = StrSql + "INNER JOIN ECON.USERS ON USERS.USERID=GROUPS.USERID "
                StrSql = StrSql + "INNER JOIN PERMISSIONSCASES PRR ON PRR.CASEID=GROUPCASES.CASEID AND PRR.USERID=USERS.USERID "
                StrSql = StrSql + ") GRPS ON GRPS.CASEID=PR.CASEID AND GRPS.USERID=USR.USERID "
                StrSql = StrSql + "WHERE GRPS.GROUPID IN (" + GrpID + ")  "
                StrSql = StrSql + "AND PR.SERVICEID IS NULL"

                Dim Cs As New DataTable()
                Cs = odbUtil.FillDataTable(StrSql, MyConnectionString)
                ReDim CaseIDs(Cs.Rows.Count - 1)
                For i = 0 To Cs.Rows.Count - 1
                    CaseIDs(i) = Cs.Rows(i).Item("CASEID").ToString()
                Next

                Return CaseIDs
            Catch ex As Exception
                Return CaseIDs
            End Try
        End Function

        Public Function Cases(ByVal ID As String) As String()
            Dim CaseIDs() As String
            Dim i As New Integer
            Try

                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")

                Dim StrSqlCases As String = ""
                'StrSqlCases = "select to_char( nvl(Assumptions.Case1,0) )  ||  ',' || to_char( nvl(Assumptions.Case2,0))    ||   ',' ||  to_char( nvl(Assumptions.Case3,0) )  ||   ',' || to_char( nvl(Assumptions.Case4,0) )  ||  ',' ||  to_char( nvl(Assumptions.Case5,0) )  ||  ',' ||  to_char ( nvl(Assumptions.Case6,0) ) ||   ',' || to_char( nvl(Assumptions.Case7,0)  )  ||   ','  ||  to_char( nvl(Assumptions.Case8,0)  )  ||  ',' || to_char( nvl(Assumptions.Case9,0) )   ||   ','  || to_Char( nvl(Assumptions.Case10,0) )  as Cases   from  Assumptions WHERE Assumptions.AssumptionId =" + ID + ""

                StrSqlCases = "SELECT *  "
                StrSqlCases = StrSqlCases + "FROM "
                StrSqlCases = StrSqlCases + "( "
                StrSqlCases = StrSqlCases + "SELECT CASE1 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT CASE2 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT CASE3 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT CASE4 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT CASE5 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT CASE6 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT CASE7 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT CASE8 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT CASE9 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT CASE10 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + ")A "
                StrSqlCases = StrSqlCases + "WHERE A.CASE  <>  0 "

                Dim Cs As New DataTable()
                Cs = odbUtil.FillDataTable(StrSqlCases, MyConnectionString)
                ReDim CaseIDs(Cs.Rows.Count - 1)
                For i = 0 To Cs.Rows.Count - 1
                    CaseIDs(i) = Cs.Rows(i).Item("CASE").ToString()
                Next

                Return CaseIDs
            Catch ex As Exception
                Return CaseIDs
            End Try



        End Function

        Public Function GetSavedCaseAsperUser(ByVal UserId As String, ByVal AssumptionId As String) As DataSet
            Dim Dts As New DataSet
            Try
                Dim StrSqlSaved As String = ""
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")

                StrSqlSaved = "SELECT distinct Assumptions.AssumptionId, "
                StrSqlSaved = StrSqlSaved + "Assumptions.DESCRIPTION,"
                StrSqlSaved = StrSqlSaved + "( Assumptions.AssumptionId ||' - ' ||"
                StrSqlSaved = StrSqlSaved + "Assumptions.DESCRIPTION || ', Cases: ' ||"
                StrSqlSaved = StrSqlSaved + "Assumptions.Case1 || ' '||"
                StrSqlSaved = StrSqlSaved + "Assumptions.Case2 || ' '||"
                StrSqlSaved = StrSqlSaved + "Assumptions.Case3 || ' '||"
                StrSqlSaved = StrSqlSaved + "Assumptions.Case4 || ' '||"
                StrSqlSaved = StrSqlSaved + "Assumptions.Case5 || ' '||"
                StrSqlSaved = StrSqlSaved + "Assumptions.Case6 || ' '||"
                StrSqlSaved = StrSqlSaved + "Assumptions.Case7 || ' '||"
                StrSqlSaved = StrSqlSaved + "Assumptions.Case8 || ' '||"
                StrSqlSaved = StrSqlSaved + "Assumptions.Case9 || ' '||"
                StrSqlSaved = StrSqlSaved + "Assumptions.Case10 "
                StrSqlSaved = StrSqlSaved + ")As Des  FROM Assumptions  Assumptions "
                StrSqlSaved = StrSqlSaved + " inner join PERMASSUMPTIONS"
                StrSqlSaved = StrSqlSaved + " on Assumptions.AssumptionId  = PERMASSUMPTIONS.AssumptionId "
                StrSqlSaved = StrSqlSaved + " and Assumptions.saved = 1 and Assumptions.MODULE=1"
                StrSqlSaved = StrSqlSaved + " and PERMASSUMPTIONS.USERID=" + UserId.ToString() + " "
                StrSqlSaved = StrSqlSaved + "WHERE Assumptions.AssumptionId = CASE WHEN " + AssumptionId.ToString() + " = -1 THEN "
                StrSqlSaved = StrSqlSaved + "Assumptions.AssumptionId "
                StrSqlSaved = StrSqlSaved + "ELSE "
                StrSqlSaved = StrSqlSaved + "" + AssumptionId.ToString() + " "
                StrSqlSaved = StrSqlSaved + "END "
                Dts = odbUtil.FillDataSet(StrSqlSaved, MyConnectionString1)
            Catch ex As Exception
                Throw
            End Try
            Return Dts
        End Function

        Public Function GetROISQl(ByVal CaseIds As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Try

                StrSql = "SELECT RESULTSPL.CASEID,  "
                StrSql = StrSql + "(RESULTSPL.CASEID||':'|| ( CASE WHEN RESULTSPL.CASEID <= 1000 THEN "
                StrSql = StrSql + "( SELECT (BASECASES.CASEDE1||' '||BASECASES.CASEDE2 ) "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + "WHERE  BASECASES.CASEID = RESULTSPL.CASEID ) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "( SELECT DISTINCT ( NVL(PERMISSIONSCASES.CASEDE1,'') || ' ' || NVL(PERMISSIONSCASES.CASEDE2,'') ) "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE  PERMISSIONSCASES.CASEID = RESULTSPL.CASEID "
                StrSql = StrSql + ") "
                StrSql = StrSql + "END)  ) AS CASEDES, "
                StrSql = StrSql + "RESULTSPL.PM, "
                StrSql = StrSql + "PREF.TITLE1, "
                StrSql = StrSql + "PREF.TITLE3, "
                StrSql = StrSql + "PREF.TITLE2, "
                StrSql = StrSql + "PREF.TITLE4, "
                StrSql = StrSql + "PREF.TITLE5, "
                StrSql = StrSql + "PREF.TITLE6, "
                StrSql = StrSql + "PREF.TITLE7, "
                StrSql = StrSql + "PREF.TITLE8, "
                StrSql = StrSql + "PREF.TITLE9, "
                StrSql = StrSql + "PREF.TITLE10, "
                StrSql = StrSql + "PREF.TITLE11, "
                StrSql = StrSql + "PREF.TITLE12, "
                StrSql = StrSql + "PREF.UNITS "
                StrSql = StrSql + "FROM RESULTSPL "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = RESULTSPL.CASEID "
                StrSql = StrSql + "WHERE RESULTSPL.CASEID IN (" + CaseIds + ") "
                StrSql = StrSql + "ORDER BY RESULTSPL.CASEID "

                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetROI:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetROI1(ByVal SelectedCaseid As String) As DataSet

            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Try


                StrSql = "SELECT  "
                StrSql = StrSql + "1 orderby, "
                StrSql = StrSql + "VARIABLE.CASEID, "
                StrSql = StrSql + "VARIABLE.CASEDES, "
                StrSql = StrSql + "VARIABLE.PALNTMARGIN*PREF.CURR AS PLANTMARGIN, "
                StrSql = StrSql + "0 AS GAINFROMINVESTMENT, "
                StrSql = StrSql + "0 AS COSTOFINVESTMENT, "
                StrSql = StrSql + "0 AS NPV, "
                StrSql = StrSql + "FIXED.BASEASSETTOTAL*PREF.CURR AS TOTALINVESTMENT, "
                StrSql = StrSql + "0 AS ROI, "
                StrSql = StrSql + "PREF.TITLE1, "
                StrSql = StrSql + "PREF.TITLE3, "
                StrSql = StrSql + "PREF.TITLE2, "
                StrSql = StrSql + "PREF.TITLE4, "
                StrSql = StrSql + "PREF.TITLE5, "
                StrSql = StrSql + "PREF.TITLE6, "
                StrSql = StrSql + "PREF.TITLE7, "
                StrSql = StrSql + "PREF.TITLE8, "
                StrSql = StrSql + "PREF.TITLE9, "
                StrSql = StrSql + "PREF.TITLE10, "
                StrSql = StrSql + "PREF.TITLE11, "
                StrSql = StrSql + "PREF.TITLE12, "
                StrSql = StrSql + "PREF.UNITS "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "(SELECT DISTINCT "
                StrSql = StrSql + "RESULTSPL.CASEID AS BASECASE, "
                StrSql = StrSql + "PM AS BASEPLANTMARGIN, "
                StrSql = StrSql + "ASSETTOTAL AS BASEASSETTOTAL "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "RESULTSPL, "
                StrSql = StrSql + "TOTAL "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "RESULTSPL.CASEID= " + SelectedCaseid + " "
                StrSql = StrSql + "AND "
                StrSql = StrSql + "RESULTSPL.CASEID=TOTAL.CASEID) "
                StrSql = StrSql + "FIXED, "
                StrSql = StrSql + "(SELECT DISTINCT "
                StrSql = StrSql + "RESULTSPL.CASEID, "
                StrSql = StrSql + "PM AS PALNTMARGIN, "
                StrSql = StrSql + "ASSETTOTAL, "
                StrSql = StrSql + "( CASE WHEN RESULTSPL.CASEID <= 1000 THEN "
                StrSql = StrSql + "( SELECT (BASECASES.CASEDE1||' '||BASECASES.CASEDE2 ) "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + "WHERE  BASECASES.CASEID = RESULTSPL.CASEID "
                StrSql = StrSql + ") ELSE "
                StrSql = StrSql + "( SELECT DISTINCT ( NVL(PERMISSIONSCASES.CASEDE1,'') || ' ' || NVL(PERMISSIONSCASES.CASEDE2,'') ) "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE  PERMISSIONSCASES.CASEID = RESULTSPL.CASEID "
                StrSql = StrSql + ") "
                StrSql = StrSql + "END  ) AS   CASEDES "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "RESULTSPL, "
                StrSql = StrSql + "TOTAL "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "RESULTSPL.CASEID =" + SelectedCaseid + "  "
                StrSql = StrSql + "AND "
                StrSql = StrSql + "RESULTSPL.CASEID=TOTAL.CASEID) "
                StrSql = StrSql + "VARIABLE, "
                StrSql = StrSql + "PREFERENCES PREF "
                StrSql = StrSql + "WHERE PREF.CASEID = VARIABLE.CASEID "




                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetROI1:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetROI(ByVal SelectedCaseid As String, ByVal Cases As String, ByVal CostofCapital As String, ByVal Year As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Try


                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "2 orderby, "
                StrSql = StrSql + "VARIABLE.CASEID, "
                StrSql = StrSql + "VARIABLE.CASEDES, "
                StrSql = StrSql + "VARIABLE.PALNTMARGIN*PREF.CURR AS PLANTMARGIN, "
                StrSql = StrSql + "(VARIABLE.PALNTMARGIN - FIXED.BASEPLANTMARGIN)*PREF.CURR AS GAINFROMINVESTMENT, "
                StrSql = StrSql + "(VARIABLE.ASSETTOTAL- FIXED.BASEASSETTOTAL)*PREF.CURR AS COSTOFINVESTMENT, "
                StrSql = StrSql + "((VARIABLE.PALNTMARGIN - FIXED.BASEPLANTMARGIN)*(1-POWER(1+" + CostofCapital + ",-" + Year + "))/" + CostofCapital + ")*PREF.CURR AS NPV, "
                StrSql = StrSql + " VARIABLE.ASSETTOTAL*PREF.CURR AS TOTALINVESTMENT, "
                StrSql = StrSql + "(CASE WHEN (VARIABLE.ASSETTOTAL- FIXED.BASEASSETTOTAL)<> 0 THEN ((((VARIABLE.PALNTMARGIN - FIXED.BASEPLANTMARGIN)*(1-POWER(1+" + CostofCapital + ",-" + Year + "))/" + CostofCapital + ")-(VARIABLE.ASSETTOTAL- FIXED.BASEASSETTOTAL))/(VARIABLE.ASSETTOTAL- FIXED.BASEASSETTOTAL))*100*PREF.CURR ELSE 0 END) AS ROI, "
                StrSql = StrSql + "PREF.TITLE1, "
                StrSql = StrSql + "PREF.TITLE3, "
                StrSql = StrSql + "PREF.TITLE2, "
                StrSql = StrSql + "PREF.TITLE4, "
                StrSql = StrSql + "PREF.TITLE5, "
                StrSql = StrSql + "PREF.TITLE6, "
                StrSql = StrSql + "PREF.TITLE7, "
                StrSql = StrSql + "PREF.TITLE8, "
                StrSql = StrSql + "PREF.TITLE9, "
                StrSql = StrSql + "PREF.TITLE10, "
                StrSql = StrSql + "PREF.TITLE11, "
                StrSql = StrSql + "PREF.TITLE12, "
                StrSql = StrSql + "PREF.UNITS "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "(SELECT DISTINCT "
                StrSql = StrSql + "RESULTSPL.CASEID AS BASECASE, "
                StrSql = StrSql + "PM AS BASEPLANTMARGIN, "
                StrSql = StrSql + "ASSETTOTAL AS BASEASSETTOTAL "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "RESULTSPL, "
                StrSql = StrSql + "TOTAL "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "RESULTSPL.CASEID=" + SelectedCaseid + "  "
                StrSql = StrSql + "AND "
                StrSql = StrSql + "RESULTSPL.CASEID=TOTAL.CASEID) "
                StrSql = StrSql + "FIXED, "
                StrSql = StrSql + "(SELECT DISTINCT "
                StrSql = StrSql + "RESULTSPL.CASEID, "
                StrSql = StrSql + "PM AS PALNTMARGIN, "
                StrSql = StrSql + "ASSETTOTAL, "
                StrSql = StrSql + "( CASE WHEN RESULTSPL.CASEID <= 1000 THEN "
                StrSql = StrSql + "( SELECT (BASECASES.CASEDE1||' '||BASECASES.CASEDE2 ) "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + "WHERE  BASECASES.CASEID = RESULTSPL.CASEID "
                StrSql = StrSql + ") ELSE "
                StrSql = StrSql + "( SELECT DISTINCT ( NVL(PERMISSIONSCASES.CASEDE1,'') || ' ' || NVL(PERMISSIONSCASES.CASEDE2,'') ) "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE  PERMISSIONSCASES.CASEID = RESULTSPL.CASEID "
                StrSql = StrSql + ") "
                StrSql = StrSql + "END  ) AS   CASEDES "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "RESULTSPL, "
                StrSql = StrSql + "TOTAL "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "RESULTSPL.CASEID IN(" + Cases + ") "
                StrSql = StrSql + "AND "
                StrSql = StrSql + "RESULTSPL.CASEID<>" + SelectedCaseid + "  "
                StrSql = StrSql + "AND "
                StrSql = StrSql + "RESULTSPL.CASEID=TOTAL.CASEID) "
                StrSql = StrSql + "VARIABLE, "
                StrSql = StrSql + "PREFERENCES PREF "
                StrSql = StrSql + "WHERE PREF.CASEID = VARIABLE.CASEID "

                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetROI:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetEditCases(ByVal UserId As String, ByVal ID As String, ByVal flag As String) As DataTable
            Dim Dts As New DataTable()
            Dim i As New Integer
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

                Dim StrSql As String = ""
                StrSql = "SELECT CASEID,CASEDE1,CASEDE FROM  "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT CASEID, "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE USERID=" + UserId.ToString() + " AND SERVICEID IS NULL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "CASEID, "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Where CASEID "
                If flag = "true" Then
                    StrSql = StrSql + "NOT IN "
                Else
                    StrSql = StrSql + "IN "
                End If
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT CASE as Caseid "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT CASE1 AS CASE FROM  ECON3.ASSUMPTIONS WHERE ECON3.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE2 AS CASE FROM  ECON3.ASSUMPTIONS WHERE ECON3.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE3 AS CASE FROM  ECON3.ASSUMPTIONS WHERE ECON3.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE4 AS CASE FROM  ECON3.ASSUMPTIONS WHERE ECON3.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE5 AS CASE FROM  ECON3.ASSUMPTIONS WHERE ECON3.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE6 AS CASE FROM  ECON3.ASSUMPTIONS WHERE ECON3.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE7 AS CASE FROM  ECON3.ASSUMPTIONS WHERE ECON3.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE8 AS CASE FROM  ECON3.ASSUMPTIONS WHERE ECON3.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE9 AS CASE FROM  ECON3.ASSUMPTIONS WHERE ECON3.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE10 AS CASE FROM  ECON3.ASSUMPTIONS WHERE ECON3.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + ")A "
                StrSql = StrSql + "WHERE A.CASE  <>  0 "
                StrSql = StrSql + ") ORDER BY CASEID ASC "
                Dts = odbUtil.FillDataTable(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Return Dts
            End Try



        End Function
        Public Function GetEditCases2(ByVal ID As String) As DataTable
            Dim Dts As New DataTable()
            Dim i As New Integer
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")

                Dim StrSqlCases As String = ""
                StrSqlCases = "SELECT *  "
                StrSqlCases = StrSqlCases + "FROM "
                StrSqlCases = StrSqlCases + "( "
                StrSqlCases = StrSqlCases + "SELECT CASE1 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT CASE2 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT CASE3 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT CASE4 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT CASE5 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT CASE6 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT CASE7 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT CASE8 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT CASE9 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT CASE10 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + ")A "
                StrSqlCases = StrSqlCases + "WHERE A.CASE  <>  0 "
                Dts = odbUtil.FillDataTable(StrSqlCases, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Return Dts
            End Try



        End Function

        Public Function GetChartProfitAndLossRes(ByVal CaseIds() As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim strCaseId As String = String.Empty
            Dim i As Integer
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Try
                For i = 0 To CaseIds.Length - 1
                    If i = 0 Then
                        strCaseId = CaseIds(i)
                    Else
                        strCaseId = strCaseId + "," + CaseIds(i)
                    End If

                Next
                StrSql = "SELECT RESULTSPL.CASEID,  "
                StrSql = StrSql + "( CASE WHEN RESULTSPL.CASEID <= 1000 THEN "
                StrSql = StrSql + "( SELECT (BASECASES.CASEID||':'||BASECASES.CASEDE1||' '||BASECASES.CASEDE2 ) "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + "WHERE  BASECASES.CASEID = RESULTSPL.CASEID "
                StrSql = StrSql + ") ELSE "
                StrSql = StrSql + "( SELECT DISTINCT (PERMISSIONSCASES.CASEID||':'||NVL(PERMISSIONSCASES.CASEDE1,'') || ' ' || NVL(PERMISSIONSCASES.CASEDE2,'') ) "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE  PERMISSIONSCASES.CASEID = RESULTSPL.CASEID "
                StrSql = StrSql + ") "
                StrSql = StrSql + "END  ) AS CASEDE, "
                StrSql = StrSql + "RESULTSPL.FINVOLMSI, "
                StrSql = StrSql + "RESULTSPL.FINVOLMUNITS, "
                StrSql = StrSql + "RESULTSPL.VOLUME, "
                StrSql = StrSql + "RESULTSPL.PM, "
                StrSql = StrSql + "RESULTSPL.PMDEP, "
                StrSql = StrSql + "RESULTSPL.FIXEDCOST, "
                StrSql = StrSql + "RESULTSPL.FIXEDCOSTDEP,"

                StrSql = StrSql + "RESULTSPL.REVENUE, "
                StrSql = StrSql + "RESULTSPL.VMATERIAL, "
                StrSql = StrSql + "RESULTSPL.VLABOR , "
                StrSql = StrSql + "RESULTSPL.VENERGY, "
                StrSql = StrSql + "RESULTSPL.VPACK, "
                StrSql = StrSql + "RESULTSPL.VSHIP, "
                StrSql = StrSql + "RESULTSPL.VM , "
                StrSql = StrSql + "RESULTSPL.OFFICESUPPLIES, "
                StrSql = StrSql + "RESULTSPL.PLABOR, "
                StrSql = StrSql + "RESULTSPL.PENERGY, "
                StrSql = StrSql + "RESULTSPL.LEASECOST, "
                StrSql = StrSql + "RESULTSPL.INSURANCE, "
                StrSql = StrSql + "RESULTSPL.UTILITIES, "
                StrSql = StrSql + "RESULTSPL.COMMUN, "
                StrSql = StrSql + "RESULTSPL.TRAVEL, "
                StrSql = StrSql + "RESULTSPL.MAINT, "
                StrSql = StrSql + "RESULTSPL.MINOR, "
                StrSql = StrSql + "RESULTSPL.OUT, "
                StrSql = StrSql + "RESULTSPL.PROF, "
                StrSql = StrSql + "RESULTSPL.LAB , "
                StrSql = StrSql + "RESULTSPL.INKSUP, "
                StrSql = StrSql + "RESULTSPL.PLATESUP, "
                StrSql = StrSql + "RESULTSPL.METSUP, "
                StrSql = StrSql + "DEP.DEPRECIATION AS DEPRECIATION "
                StrSql = StrSql + "FROM RESULTSPL "
                StrSql = StrSql + "INNER JOIN DEPRECIATION DEP "
                StrSql = StrSql + "ON DEP.CASEID=RESULTSPL.CASEID "
                StrSql = StrSql + "WHERE RESULTSPL.CASEID IN (" + strCaseId + ") "
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Econ3GetData:GetChartProfitAndLossRes:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetChartCostRes(ByVal CaseIds() As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim strCaseId As String = String.Empty
            Dim i As Integer
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Try
                For i = 0 To CaseIds.Length - 1
                    If i = 0 Then
                        strCaseId = CaseIds(i)
                    Else
                        strCaseId = strCaseId + "," + CaseIds(i)
                    End If

                Next
                StrSql = "SELECT RESULTSPL.CASEID,  "
                StrSql = StrSql + "( CASE WHEN RESULTSPL.CASEID <= 1000 THEN "
                StrSql = StrSql + "( SELECT (BASECASES.CASEID||':'||BASECASES.CASEDE1||' '||BASECASES.CASEDE2 ) "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + "WHERE  BASECASES.CASEID = RESULTSPL.CASEID "
                StrSql = StrSql + ") ELSE "
                StrSql = StrSql + "( SELECT DISTINCT (PERMISSIONSCASES.CASEID||':'||NVL(PERMISSIONSCASES.CASEDE1,'') || ' ' || NVL(PERMISSIONSCASES.CASEDE2,'') ) "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE  PERMISSIONSCASES.CASEID = RESULTSPL.CASEID "
                StrSql = StrSql + ") "
                StrSql = StrSql + "END  ) AS CASEDE, "
                StrSql = StrSql + "RESULTSPL.FINVOLMSI, "
                StrSql = StrSql + "RESULTSPL.FINVOLMUNITS, "
                StrSql = StrSql + "RESULTSPL.VOLUME, "
                StrSql = StrSql + "RESULTSPL.TOTALCOST, "
                StrSql = StrSql + "RESULTSPL.TOTALCOSTDEP, "
                StrSql = StrSql + "RESULTSPL.FIXEDCOST, "
                StrSql = StrSql + "RESULTSPL.FIXEDCOSTDEP, "
                StrSql = StrSql + "RESULTSPL.VMATERIAL, "
                StrSql = StrSql + "RESULTSPL.VLABOR , "
                StrSql = StrSql + "RESULTSPL.VENERGY, "
                StrSql = StrSql + "RESULTSPL.VPACK, "
                StrSql = StrSql + "RESULTSPL.VSHIP, "
                StrSql = StrSql + "RESULTSPL.VARIABLECOST , "
                StrSql = StrSql + "RESULTSPL.OFFICESUPPLIES, "
                StrSql = StrSql + "RESULTSPL.PLABOR, "
                StrSql = StrSql + "RESULTSPL.PENERGY, "
                StrSql = StrSql + "RESULTSPL.LEASECOST, "
                StrSql = StrSql + "RESULTSPL.INSURANCE, "
                StrSql = StrSql + "RESULTSPL.UTILITIES, "
                StrSql = StrSql + "RESULTSPL.COMMUN, "
                StrSql = StrSql + "RESULTSPL.TRAVEL, "
                StrSql = StrSql + "RESULTSPL.MAINT, "
                StrSql = StrSql + "RESULTSPL.MINOR, "
                StrSql = StrSql + "RESULTSPL.OUT, "
                StrSql = StrSql + "RESULTSPL.PROF, "
                StrSql = StrSql + "RESULTSPL.LAB , "
                StrSql = StrSql + "RESULTSPL.INKSUP, "
                StrSql = StrSql + "RESULTSPL.PLATESUP, "
                StrSql = StrSql + "RESULTSPL.METSUP, "
                StrSql = StrSql + "DEP.DEPRECIATION AS DEPRECIATION "
                StrSql = StrSql + "FROM RESULTSPL "
                StrSql = StrSql + "INNER JOIN DEPRECIATION DEP "
                StrSql = StrSql + "ON DEP.CASEID=RESULTSPL.CASEID "
                StrSql = StrSql + "WHERE RESULTSPL.CASEID IN (" + strCaseId + ") "
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Econ3GetData:GetChartCostRes:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

#Region "Supporting Assumption Pages SQL"
        Public Function GetMaterials() As DataSet
            Dim Dts As New DataSet
            Try
                Dim StrSql As String = ""
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
                StrSql = "select Matid, (Matid||':'||matde1||' '||matde2) MaterialDes ,price,sg from Materials ORDER BY matde1"

                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
            Catch ex As Exception
                Throw
            End Try
            Return Dts
        End Function

        Public Function GetDepartment() As DataSet
            Dim Dts As New DataSet
            Try
                Dim StrSql As String = ""
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
                StrSql = "SELECT PROCID,(PROCDE1||' '||PROCDE2)PROCDE FROM PROCESS ORDER BY PROCDE"

                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
            Catch ex As Exception
                Throw
            End Try
            Return Dts
        End Function

        Public Function GetProductFormat(ByVal Unit As String) As DataSet
            Dim Dts As New DataSet()
            Try
                'DataBase Connection
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
                Dim Sql1 As String = " SELECT FORMATID, FORMATDE1, FORMATDE2,(FORMATID||' . '||FORMATDE1||''||FORMATDE2) AS DES FROM PRODUCTFORMAT"
                Dim Sql2 As String = " SELECT FORMATID, FORMATDE1, FORMATDE2,(FORMATID||' . '||FORMATDE1||''||FORMATDE2) AS DES FROM PRODUCTFORMAT2"

                If Unit = 0 Then
                    Dts = odbUtil.FillDataSet(Sql1, MyConnectionString)
                Else
                    Dts = odbUtil.FillDataSet(Sql2, MyConnectionString)
                End If

                Return Dts
            Catch ex As Exception
                Return Dts
            End Try



        End Function

        Public Function GetPalletItems() As DataSet
            Dim Dts As New DataSet
            Try
                Dim StrSql As String = ""
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
                StrSql = "SELECT  PALLETID, (PALLETDE1||' '||PALLETDE2)PALLETDES  "
                StrSql = StrSql + "FROM PALLET "
                StrSql = StrSql + "ORDER BYE PALLETDES"



                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
            Catch ex As Exception
                Throw
            End Try
            Return Dts
        End Function

        Public Function GetEquipments() As DataSet
            Dim Dts As New DataSet
            Try
                Dim StrSql As String = ""
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
                StrSql = "SELECT EQUIPID,(EQUIPDE1||' '||EQUIPDE2) AS EQUIPDES FROM EQUIPMENT ORDER BY  EQUIPDES"
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
            Catch ex As Exception
                Throw
            End Try
            Return Dts
        End Function

        Public Function GetEquipments2() As DataSet
            Dim Dts As New DataSet
            Try
                Dim StrSql As String = ""
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
                StrSql = "SELECT EQUIPID,(EQUIPDE1||' '||EQUIPDE2) AS EQUIPDES FROM EQUIPMENT2 ORDER BY  EQUIPDES"
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
            Catch ex As Exception
                Throw
            End Try
            Return Dts
        End Function

        Public Function GetPositions(ByVal Country As String) As DataSet
            Dim Dts As New DataSet
            Try
                Dim StrSql As String = ""
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
                StrSql = "SELECT  "
                StrSql = StrSql + "PERSID, "
                StrSql = StrSql + "(PERSDE1||''||PERSDE2) AS PERSDE "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + " " + Country + "  "
                StrSql = StrSql + "ORDER BY PERSDE  "
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
            Catch ex As Exception
                Throw
            End Try
            Return Dts
        End Function
#End Region


#Region "Global Manager"
        Public Function GetAllPermissionCases(ByVal AssumId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim strCaseId As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Try
                StrSql = "SELECT DISTINCT(P.CASEID) FROM PERMISSIONSCASES P  "
                StrSql = StrSql + "WHERE P.CASEID IN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=CASE1)>0 THEN 0 ELSE NVL(CASE1,0) END ) "
                StrSql = StrSql + "FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=CASE2)>0 THEN 0 ELSE NVL(CASE2,0) END ) "
                StrSql = StrSql + "FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=CASE3)>0 THEN 0 ELSE NVL(CASE3,0) END ) "
                StrSql = StrSql + "FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=CASE4)>0 THEN 0 ELSE NVL(CASE4,0) END ) "
                StrSql = StrSql + "FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=CASE5)>0 THEN 0 ELSE NVL(CASE5,0) END ) "
                StrSql = StrSql + "FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT   (CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=CASE6)>0 THEN 0 ELSE NVL(CASE6,0) END ) "
                StrSql = StrSql + "FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT   (CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=CASE7)>0 THEN 0 ELSE NVL(CASE7,0) END ) "
                StrSql = StrSql + "FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT   (CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=CASE8)>0 THEN 0 ELSE NVL(CASE8,0) END ) "
                StrSql = StrSql + "FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT  (CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=CASE9)>0 THEN 0 ELSE NVL(CASE9,0) END ) "
                StrSql = StrSql + "FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT  (CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=CASE10)>0 THEN 0 ELSE NVL(CASE10,0) END )  AS C "
                StrSql = StrSql + "FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                StrSql = StrSql + ")"
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E3GetAllPermissionCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function CheckUser(ByVal CaseId As String, ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim strCaseId As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "USERID "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + " WHERE CASEID= " + CaseId
                StrSql = StrSql + " AND USERID=" + UserId.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E3CheckUser:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetDelCases(ByVal CaseID As String, ByVal UserId As String) As String
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Try
                StrSql = "SELECT CASEID FROM PERMISSIONSCASES WHERE CASEID= " + CaseID + " AND USERID=" + UserId.ToString() + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASEID FROM BASECASES WHERE CASEID= " + CaseID

                Dim Cs As New DataTable()
                Cs = odbUtil.FillDataTable(StrSql, MyConnectionString)

                If Cs.Rows.Count = 0 Then
                    Return CaseID
                End If
            Catch ex As Exception
                Throw New Exception("E3CheckUser:" + ex.Message.ToString())
            End Try
            Return ""
        End Function
        Public Function GetAssumptionCaseId(ByVal AssumptionId As String) As String
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim strCaseid As String = String.Empty
            Dim Dts As New DataSet()
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
            Try
                StrSql = "SELECT (CASE1 || ','|| CASE2 || ','|| CASE3 || ','|| CASE4  "
                StrSql = StrSql + "|| ','|| CASE5 || ','|| CASE6 || ','|| CASE7 || ','|| CASE8 "
                StrSql = StrSql + "|| ','|| CASE9 || ','|| CASE10) CASEID "
                StrSql = StrSql + "FROM ECON3.ASSUMPTIONS "
                StrSql = StrSql + "WHERE ASSUMPTIONID= " + AssumptionId.ToString()
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                strCaseid = Dts.Tables(0).Rows(0).Item("CASEID").ToString()
            Catch ex As Exception
                Throw New Exception("GetAssumptionCaseId:" + ex.Message.ToString())
            End Try
            Return strCaseid
        End Function
        Public Function GetCasesByCaseID(ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

                Dim StrSql As String = "SELECT CASEID FROM PERMISSIONSCASES WHERE CASEID=" + CaseId + " "
                StrSql = StrSql + "UNION SELECT CASEID FROM BASECASES WHERE CASEID=" + CaseId
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
#End Region

#Region "User Category"
        Public Function GetCategorySet(ByVal CatSetName As String, ByVal UserId As String, ByVal Pagename As String) As DataTable
            Dim Dts As New DataTable()
            Dim strsql As String = String.Empty
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
                strsql = "SELECT CATEGORYSETID, CATEGORYSETNAME,PAGENAME FROM  "
                strsql = strsql + "CATEGORYSET "
                strsql = strsql + "WHERE USERID= " + UserId + " "
                If CatSetName <> "" Then
                    strsql = strsql + "AND UPPER(CATEGORYSETNAME)='" + CatSetName.ToUpper() + "' "
                End If
                If Pagename <> "" Then
                    strsql = strsql + "AND UPPER(PAGENAME)='" + Pagename.ToUpper() + "' "
                End If
                strsql = strsql + "ORDER BY UPPER(CATEGORYSETNAME)"
                Dts = odbUtil.FillDataTable(strsql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
        Public Function GetCategoryBySet(ByVal CatSetId As String, ByVal CatName As String) As DataTable
            Dim Dts As New DataTable()
            Dim strsql As String = String.Empty
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
                strsql = "SELECT * FROM  "
                strsql = strsql + "CATEGORY "
                strsql = strsql + "WHERE CATEGORYSETID=" + CatSetId + " "
                If CatName <> "" Then
                    strsql = strsql + "AND UPPER(CATEGORYNAME)= '" + CatName.ToUpper() + "' "
                End If
                strsql = strsql + "ORDER BY CATEGORYNAME ASC"
                Dts = odbUtil.FillDataTable(strsql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
        Public Function GetCategoryItemBycategory(ByVal category As String) As DataSet
            Dim Dts As New DataSet()
            Dim strsql As String = String.Empty
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
                strsql = "SELECT ITEMNAME  "
                strsql = strsql + "FROM CATEGORYITEM "
                strsql = strsql + "WHERE CATEGORYID=" + category
                Dts = odbUtil.FillDataSet(strsql, MyConnectionString)
                Return Dts

            Catch ex As Exception
                Throw
                Return Dts
            End Try

        End Function
        Public Function GetCategoryItems(ByVal PageName As String) As DataSet
            Dim Dts As New DataSet()
            Dim strsql As String = String.Empty
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
                If PageName = "PFT" Or PageName = "PFTD" Then
                    strsql = "SELECT  'Revenue' DES,'REVENUE' CODE  FROM DUAL  "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Materials' DES,'VMATERIAL' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Labor' DES,'VLABOR' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Energy' DES,'VENERGY' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Distribution Packaging' DES,'VPACK' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Shipping to Customer' DES,'VSHIP' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Variable Margin' DES,'VM' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Office Supplies' DES,'OFFICESUPPLIES' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Labor' DES,'PLABOR' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Energy' DES,'PENERGY' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Lease Cost' DES,'LEASECOST' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Insurance' DES,'INSURANCE' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Utilities' DES,'UTILITIES' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Communications' DES,'COMMUN' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Travel' DES,'TRAVEL' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Maintenance Supplies' DES,'MAINT' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Minor Equipment' DES,'MINOR' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Outside Services' DES,'OUT' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Professional Services' DES,'PROF' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Laboratory Supplies' DES,'LAB' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Ink Supplies' DES,'INKSUP' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Plate Supplies' DES,'PLATESUP' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Metal Supplies' DES,'METSUP' CODE  FROM DUAL "
                    If (PageName = "PFT") Then
                        strsql = strsql + "UNION ALL "
                        strsql = strsql + "SELECT 'Plant Margin' DES,'PM' CODE  FROM DUAL "
                    ElseIf (PageName = "PFTD") Then
                        strsql = strsql + "UNION ALL "
                        strsql = strsql + "SELECT 'Depreciation' DES,'DEPRECIATION' CODE  FROM DUAL "
                        strsql = strsql + "UNION ALL "
                        strsql = strsql + "SELECT 'Plant Margin' DES,'PMDEP' CODE  FROM DUAL "
                    End If
                ElseIf PageName = "COST" Or PageName = "COSTD" Then
                    strsql = "SELECT  'Material Cost' DES,'VMATERIAL' CODE  FROM DUAL  "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Labor Cost' DES,'VLABOR' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Energy Cost' DES,'VENERGY' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Distribution Packaging Cost' DES,'VPACK' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Shipping Cost' DES,'VSHIP' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Total Variable Cost' DES,'VARIABLECOST' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Office Supplies Cost' DES,'OFFICESUPPLIES' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Labor Cost' DES,'PLABOR' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Energy Cost' DES,'PENERGY' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Lease Cost' DES,'LEASECOST' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Insurance Cost' DES,'INSURANCE' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Utilities Cost' DES,'UTILITIES' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Communications Cost' DES,'COMMUN' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Travel Cost' DES,'TRAVEL' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Maintenance Supplies Cost' DES,'MAINT' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Minor Equipment Cost' DES,'MINOR' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Outside Services Cost' DES,'OUT' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Professional Services Cost' DES,'PROF' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Laboratory Supplies Cost' DES,'LAB' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Ink Supplies Cost' DES,'INKSUP' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Plate Supplies Cost' DES,'PLATESUP' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Metal Supplies Cost' DES,'METSUP' CODE  FROM DUAL "
                    If (PageName = "COST") Then
                        strsql = strsql + "UNION ALL "
                        strsql = strsql + "SELECT 'Total Fixed Cost' DES,'FIXEDCOST' CODE  FROM DUAL "
                        strsql = strsql + "UNION ALL "
                        strsql = strsql + "SELECT 'Total Cost' DES,'TOTALCOST' CODE  FROM DUAL "
                    ElseIf (PageName = "COSTD") Then
                        strsql = strsql + "UNION ALL "
                        strsql = strsql + "SELECT 'Depreciation' DES,'DEPRECIATION' CODE  FROM DUAL "
                        strsql = strsql + "UNION ALL "
                        strsql = strsql + "SELECT 'Total Fixed Cost' DES,'FIXEDCOSTDEP' CODE  FROM DUAL "
                        strsql = strsql + "UNION ALL "
                        strsql = strsql + "SELECT 'Total Cost' DES,'TOTALCOSTDEP' CODE  FROM DUAL "
                    End If
                End If
                Dts = odbUtil.FillDataSet(strsql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
        Public Function GetItemsByCat(ByVal CategoryId As String) As DataSet
            Dim Dts As New DataSet()
            Dim strsql As String = String.Empty
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
                strsql = "SELECT CAT.CATEGORYID,  "
                strsql = strsql + "ITEMNAME "
                strsql = strsql + "FROM CATEGORYITEM "
                strsql = strsql + "INNER JOIN CATEGORY CAT "
                strsql = strsql + "ON CAT.CATEGORYID=CATEGORYITEM.CATEGORYID "
                strsql = strsql + "INNER JOIN CATEGORYSET "
                strsql = strsql + "ON CATEGORYSET.CATEGORYSETID=CAT.CATEGORYSETID "
                strsql = strsql + "WHERE CAT.CATEGORYID=" + CategoryId
                Dts = odbUtil.FillDataSet(strsql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
        Public Function GetCategorySetByID(ByVal CatSetID As String) As DataTable
            Dim Dts As New DataTable()
            Dim strsql As String = String.Empty
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
                strsql = "SELECT CATEGORYSETID, CATEGORYSETNAME,PAGENAME FROM  "
                strsql = strsql + "CATEGORYSET "
                strsql = strsql + "WHERE CATEGORYSETID= " + CatSetID + " "
                Dts = odbUtil.FillDataTable(strsql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
#End Region

#Region "Approval Module"
        Public Function GetAllPermissionCasesE3(ByVal AssumId As String, ByVal licAdmin As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim strCaseId As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Try
                If licAdmin = "Y" Then
                    StrSql = "SELECT DISTINCT(P.CASEID) FROM PERMISSIONSCASES P  "
                    StrSql = StrSql + "WHERE P.CASEID IN "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + "SELECT NVL(CASE1,0) FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT NVL(CASE2,0) FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT NVL(CASE3,0) FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT NVL(CASE4,0) FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT NVL(CASE5,0) FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT NVL(CASE6,0) FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT NVL(CASE7,0) FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT NVL(CASE8,0) FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT NVL(CASE9,0) FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT NVL(CASE10,0) FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + ")"
                Else
                    StrSql = "SELECT DISTINCT(P.CASEID) FROM PERMISSIONSCASES P  "
                    StrSql = StrSql + "WHERE P.CASEID IN "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + "SELECT "
                    StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM PERMISSIONSCASES INNER JOIN MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID  WHERE PERMISSIONSCASES.STATUSID=3 AND CASEID=CASE1)>0 THEN 0 ELSE NVL(CASE1,0) END ) "
                    StrSql = StrSql + "FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT "
                    StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM PERMISSIONSCASES INNER JOIN MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID  WHERE PERMISSIONSCASES.STATUSID=3 AND  CASEID=CASE2)>0 THEN 0 ELSE NVL(CASE2,0) END ) "
                    StrSql = StrSql + "FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT "
                    StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM PERMISSIONSCASES INNER JOIN MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID  WHERE PERMISSIONSCASES.STATUSID=3 AND  CASEID=CASE3)>0 THEN 0 ELSE NVL(CASE3,0) END ) "
                    StrSql = StrSql + "FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT "
                    StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM PERMISSIONSCASES INNER JOIN MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID  WHERE PERMISSIONSCASES.STATUSID=3 AND  CASEID=CASE4)>0 THEN 0 ELSE NVL(CASE4,0) END ) "
                    StrSql = StrSql + "FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT "
                    StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM PERMISSIONSCASES INNER JOIN MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID  WHERE PERMISSIONSCASES.STATUSID=3 AND  CASEID=CASE5)>0 THEN 0 ELSE NVL(CASE5,0) END ) "
                    StrSql = StrSql + "FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT   (CASE WHEN(SELECT CASEID FROM PERMISSIONSCASES INNER JOIN MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID  WHERE PERMISSIONSCASES.STATUSID=3 AND  CASEID=CASE6)>0 THEN 0 ELSE NVL(CASE6,0) END ) "
                    StrSql = StrSql + "FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT   (CASE WHEN(SELECT CASEID FROM PERMISSIONSCASES INNER JOIN MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID  WHERE PERMISSIONSCASES.STATUSID=3 AND  CASEID=CASE7)>0 THEN 0 ELSE NVL(CASE7,0) END ) "
                    StrSql = StrSql + "FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT   (CASE WHEN(SELECT CASEID FROM PERMISSIONSCASES INNER JOIN MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID  WHERE PERMISSIONSCASES.STATUSID=3 AND  CASEID=CASE8)>0 THEN 0 ELSE NVL(CASE8,0) END ) "
                    StrSql = StrSql + "FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT  (CASE WHEN(SELECT CASEID FROM PERMISSIONSCASES INNER JOIN MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID  WHERE PERMISSIONSCASES.STATUSID=3 AND  CASEID=CASE9)>0 THEN 0 ELSE NVL(CASE9,0) END ) "
                    StrSql = StrSql + "FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT  (CASE WHEN(SELECT CASEID FROM PERMISSIONSCASES INNER JOIN MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID  WHERE PERMISSIONSCASES.STATUSID=3 AND  CASEID=CASE10)>0 THEN 0 ELSE NVL(CASE10,0) END )  AS C "
                    StrSql = StrSql + "FROM ECON3.ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                    StrSql = StrSql + ")"
                End If
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E3GetAllPermissionCasesE3:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        
Public Function GetCasesE3(ByVal UserId As String, ByVal LiceAdmin As String) As DataTable
            Dim Dts As New DataTable
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
                Dim StrSql As String = ""
                If LiceAdmin = "Y" Then
                    'StrSql = "SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2|| '     STATUS:'|| MS.STATUS) AS CASEDE,MS.STATUSID FROM PERMISSIONSCASES INNER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID WHERE USERID=" + UserId.ToString() + " "
                    StrSql = "SELECT PW.CASEID,CASEDE1,('CASE:'||pw.CASEID||' - '|| CASEDE1||' ' || CASEDE2|| '     STATUS:'|| MS.STATUS ||' - '|| DC.COUNTRYDES) AS CASEDE,MS.STATUSID FROM PERMISSIONSCASES PW"
                    StrSql = StrSql + " INNER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PW.STATUSID"
                    StrSql = StrSql + "  INNER JOIN PREFERENCES P ON P.CASEID=PW.CASEID INNER JOIN ADMINSITE.DIMCOUNTRIES DC ON DC.COUNTRYID =P.OCOUNTRY"
                    StrSql = StrSql + " WHERE USERID=" + UserId.ToString() + " "                
                Else
                    StrSql = "SELECT CASEID,CASEDE1, CASEDE,STATUSID FROM ("
                    StrSql = StrSql + "(SELECT PW.CASEID,CASEDE1,('CASE:'||PW.CASEID||' - '|| CASEDE1||' ' || CASEDE2|| (CASE WHEN MS.STATUS IS NULL THEN '' ELSE   '     STATUS:'|| MS.STATUS END)|| ' - '|| DC.COUNTRYDES ) AS CASEDE,0 STATUSID FROM PERMISSIONSCASES PW LEFT OUTER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PW.STATUSID "
                    StrSql = StrSql + "  INNER JOIN PREFERENCES P ON P.CASEID=PW.CASEID INNER JOIN ADMINSITE.DIMCOUNTRIES DC ON DC.COUNTRYID =P.OCOUNTRY"
                    StrSql = StrSql + " WHERE USERID=" + UserId.ToString() + " ) "
                    StrSql = StrSql + "UNION (SELECT PW.CASEID,CASEDE1,('CASE:'||PW.CASEID||' - '|| CASEDE1||' ' || CASEDE2|| '     STATUS:'|| MS.STATUS ||'- '||DC1.COUNTRYDES) AS CASEDE,MS.STATUSID FROM PERMISSIONSCASES PW  INNER JOIN USERS ON USERS.USERID=PW.USERID  "
                    StrSql = StrSql + "INNER JOIN MODSTATUS MS ON MS.STATUSID=PW.STATUSID "
                    StrSql = StrSql + " INNER JOIN PREFERENCES P ON P.CASEID=PW.CASEID INNER JOIN ADMINSITE.DIMCOUNTRIES DC1 ON DC1.COUNTRYID =P.OCOUNTRY"
                    StrSql = StrSql + " WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM USERS WHERE USERID=" + UserId.ToString() + ") "
                    StrSql = StrSql + "AND PW.STATUSID IN (3,5)) "
                    StrSql = StrSql + ") ORDER BY CASEDE1"
                End If


                Dts = odbUtil.FillDataTable(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function

        Public Function GetCasesE3_OLDSep2023(ByVal UserId As String, ByVal LiceAdmin As String) As DataTable
            Dim Dts As New DataTable
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
                Dim StrSql As String = ""
                If LiceAdmin = "Y" Then
                    StrSql = "SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2|| '     STATUS:'|| MS.STATUS) AS CASEDE,MS.STATUSID FROM PERMISSIONSCASES INNER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID WHERE USERID=" + UserId.ToString() + " "
                Else
                    StrSql = "SELECT CASEID,CASEDE1, CASEDE,STATUSID FROM ("
                    StrSql = StrSql + "(SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2|| (CASE WHEN MS.STATUS IS NULL THEN '' ELSE   '     STATUS:'|| MS.STATUS END)) AS CASEDE,0 STATUSID FROM PERMISSIONSCASES LEFT OUTER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID WHERE USERID=" + UserId.ToString() + " ) "
                    StrSql = StrSql + "UNION (SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2|| '     STATUS:'|| MS.STATUS) AS CASEDE,MS.STATUSID FROM PERMISSIONSCASES  INNER JOIN USERS ON UPPER(USERS.USERNAME)=UPPER(PERMISSIONSCASES.USERNAME) "
                    StrSql = StrSql + "INNER JOIN MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                    StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM USERS WHERE USERID=" + UserId.ToString() + ") "
                    StrSql = StrSql + "AND PERMISSIONSCASES.STATUSID IN (3,5)) "
                    StrSql = StrSql + ") ORDER BY CASEDE1"
                End If


                Dts = odbUtil.FillDataTable(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
        Public Function GetDelCasesE3(ByVal CaseID As String, ByVal UserId As String, ByVal LiceAdmin As String) As String
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Try
                If LiceAdmin = "Y" Then
                    StrSql = "SELECT CASEID FROM PERMISSIONSCASES WHERE CASEID= " + CaseID + " AND USERID=" + UserId.ToString() + " "
                Else
                    StrSql = "SELECT CASEID FROM ("
                    StrSql = StrSql + "(SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE FROM PERMISSIONSCASES WHERE USERID=" + UserId.ToString() + " ) "
                    StrSql = StrSql + "UNION (SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE  FROM PERMISSIONSCASES  INNER JOIN USERS ON USERS.USERID=PERMISSIONSCASES.USERID "
                    StrSql = StrSql + "INNER JOIN MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                    StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM USERS WHERE USERID=" + UserId.ToString() + ") "
                    StrSql = StrSql + "AND PERMISSIONSCASES.STATUSID=3) "
                    StrSql = StrSql + ")  WHERE CASEID= " + CaseID + " "
                End If

                Dim Cs As New DataTable()
                Cs = odbUtil.FillDataTable(StrSql, MyConnectionString)

                If Cs.Rows.Count = 0 Then
                    Return CaseID
                End If
            Catch ex As Exception
                Throw New Exception("E3CheckUser:" + ex.Message.ToString())
            End Try
            Return ""
        End Function
        Public Function GetSisterCases(ByVal CaseID As String) As Boolean
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Try
                StrSql = "SELECT CASEID FROM PERMISSIONSCASES WHERE CASEID= " + CaseID + " AND STATUSID=5 "
                Dim Cs As New DataTable()
                Cs = odbUtil.FillDataTable(StrSql, MyConnectionString)

                If Cs.Rows.Count = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw New Exception("GetSisterCases:" + ex.Message.ToString())
            End Try
            Return ""
        End Function
        
        Public Function GetEditCasesE3(ByVal UserId As String, ByVal ID As String, ByVal flag As String, ByVal LiceAdmin As String) As DataTable
            Dim Dts As New DataTable()
            Dim i As New Integer
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

                Dim StrSql As String = ""
                If LiceAdmin = "Y" Then
                    StrSql = "SELECT CASEID,CASEDE1,CASEDE FROM  "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + "SELECT CASEID, "
                    StrSql = StrSql + "CASEDE1, "
                    StrSql = StrSql + "('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2|| '     STATUS:'|| MS.STATUS) AS CASEDE "
                    StrSql = StrSql + "FROM PERMISSIONSCASES LEFT OUTER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                    StrSql = StrSql + "WHERE USERID=" + UserId.ToString() + " "
                    StrSql = StrSql + ") "
                Else
                    StrSql = "SELECT CASEID,CASEDE1, CASEDE FROM ("
                    StrSql = StrSql + "(SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2|| (CASE WHEN MS.STATUS IS NULL THEN '' ELSE   '     STATUS:'|| MS.STATUS END)) AS CASEDE FROM PERMISSIONSCASES LEFT OUTER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID WHERE USERID=" + UserId.ToString() + " ) "
                    StrSql = StrSql + "UNION (SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2|| '     STATUS:'|| MS.STATUS) AS CASEDE  FROM PERMISSIONSCASES  INNER JOIN USERS ON USERS.USERID=PERMISSIONSCASES.USERID "
                    StrSql = StrSql + "LEFT OUTER JOIN MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                    StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM USERS WHERE USERID=" + UserId.ToString() + ") "
                    StrSql = StrSql + "AND PERMISSIONSCASES.STATUSID IN(3,5)) "
                    StrSql = StrSql + ") "
                End If

                StrSql = StrSql + "Where CASEID "
                If flag = "true" Then
                    StrSql = StrSql + "NOT IN "
                Else
                    StrSql = StrSql + "IN "
                End If
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT CASE as Caseid "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT CASE1 AS CASE FROM  ECON3.ASSUMPTIONS WHERE ECON3.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE2 AS CASE FROM  ECON3.ASSUMPTIONS WHERE ECON3.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE3 AS CASE FROM  ECON3.ASSUMPTIONS WHERE ECON3.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE4 AS CASE FROM  ECON3.ASSUMPTIONS WHERE ECON3.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE5 AS CASE FROM  ECON3.ASSUMPTIONS WHERE ECON3.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE6 AS CASE FROM  ECON3.ASSUMPTIONS WHERE ECON3.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE7 AS CASE FROM  ECON3.ASSUMPTIONS WHERE ECON3.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE8 AS CASE FROM  ECON3.ASSUMPTIONS WHERE ECON3.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE9 AS CASE FROM  ECON3.ASSUMPTIONS WHERE ECON3.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE10 AS CASE FROM  ECON3.ASSUMPTIONS WHERE ECON3.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + ")A "
                StrSql = StrSql + "WHERE A.CASE  <>  0 "
                StrSql = StrSql + ") "
                Dts = odbUtil.FillDataTable(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Return Dts
            End Try
        End Function
#End Region
    End Class
End Class

