Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System

Public Class MoldS2GetData
    Public Class Selectdata
        Dim MoldS1Connection As String = System.Configuration.ConfigurationManager.AppSettings("MoldS1ConnectionString")
        Dim MoldS2Connection As String = System.Configuration.ConfigurationManager.AppSettings("MoldS2ConnectionString")
        Dim MoldE2Connection As String = System.Configuration.ConfigurationManager.AppSettings("MoldE2ConnectionString")
        Dim MoldE1Connection As String = System.Configuration.ConfigurationManager.AppSettings("MoldE1ConnectionString")
        Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

#Region "Supporting Assumptions Pages"

        Public Function GetBCaseDetails() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES FROM BASECASES "
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID "

                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetBCaseDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPCaseDetails(ByVal USERID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE USERID ='" + USERID.ToString() + "' "
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID "

                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetPCaseDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCaseDetails(ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID,CASEDE1,(CASEDE1||' ' ||CASEDE2)CASEDES,CASEDE3,CASEDE2,CASETYPE  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT  CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,'Base Case' CASETYPE FROM BASECASES "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT DISTINCT CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,'Proprietary Case' CASETYPE FROM PERMISSIONSCASES "
                StrSql = StrSql + ") "
                StrSql = StrSql + "WHERE CASEID =" + CaseId.ToString() + " "
                StrSql = StrSql + "ORDER BY CASEID "

                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetCaseDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCases(ByVal USERID As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE USERID ='" + USERID.ToUpper().ToString() + "' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSelectedUserDetails(ByVal usreName As String, ByVal Schema As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERPERMISSIONS.MaxCaseCount FROM USERPERMISSIONS INNER JOIN USERS ON USERS.USERID= USERPERMISSIONS.USERID  "
                StrSql = StrSql + "INNER JOIN SERVICES ON SERVICES.SERVICEID=USERPERMISSIONS.SERVICEID "
                StrSql = StrSql + "WHERE UPPER(USERS.USERNAME)='" + usreName.ToUpper() + "' AND SERVICES.SERVICEDE='" + Schema + "' "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetSelectedUserDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetBCases(ByVal CaseDe1 As String, ByVal CaseDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetBCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCaseViewer(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PROC1.PROCDE1 AS DEPT1,  "
                StrSql = StrSql + "PROC2.PROCDE1 AS DEPT2, "
                StrSql = StrSql + "PROC3.PROCDE1 AS DEPT3, "
                StrSql = StrSql + "PROC4.PROCDE1 AS DEPT4, "
                StrSql = StrSql + "PROC5.PROCDE1 AS DEPT5, "
                StrSql = StrSql + "PROC6.PROCDE1 AS DEPT6, "
                StrSql = StrSql + "PROC7.PROCDE1 AS DEPT7, "
                StrSql = StrSql + "PROC8.PROCDE1 AS DEPT8, "
                StrSql = StrSql + "PROC9.PROCDE1 AS DEPT9, "
                StrSql = StrSql + "PROC10.PROCDE1 AS DEPT10, "
                StrSql = StrSql + "PLANTCONFIG.M1, "
                StrSql = StrSql + "PLANTCONFIG.M2, "
                StrSql = StrSql + "PLANTCONFIG.M3, "
                StrSql = StrSql + "PLANTCONFIG.M4, "
                StrSql = StrSql + "PLANTCONFIG.M5, "
                StrSql = StrSql + "PLANTCONFIG.M6, "
                StrSql = StrSql + "PLANTCONFIG.M7, "
                StrSql = StrSql + "PLANTCONFIG.M8, "
                StrSql = StrSql + "PLANTCONFIG.M9, "
                StrSql = StrSql + "PLANTCONFIG.M10, "
                StrSql = StrSql + "(RMERGY+RMPACKERGY+RMANDPACKTRNSPTERGY+PROCERGY+DPPACKERGY+DPTRNSPTERGY+TRSPTTOCUS+PROCERGYS2+PACKGEDPRODPACK+PPPTRNSPT+PACKGEDPRODTRNSPT)TOTERGY, "
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
                StrSql = StrSql + "PREF.TITLE12 "
                StrSql = StrSql + "FROM PLANTCONFIG "
                StrSql = StrSql + "INNER JOIN TOTAL "
                StrSql = StrSql + "ON TOTAL.CASEID = PLANTCONFIG.CASEID "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = PLANTCONFIG.CASEID "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS PROC1 "
                StrSql = StrSql + "ON PROC1.PROCID = PLANTCONFIG.M1 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS PROC2 "
                StrSql = StrSql + "ON PROC2.PROCID = PLANTCONFIG.M2 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS PROC3 "
                StrSql = StrSql + "ON PROC3.PROCID = PLANTCONFIG.M3 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS PROC4 "
                StrSql = StrSql + "ON PROC4.PROCID = PLANTCONFIG.M4 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS PROC5 "
                StrSql = StrSql + "ON PROC5.PROCID = PLANTCONFIG.M5 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS PROC6 "
                StrSql = StrSql + "ON PROC6.PROCID = PLANTCONFIG.M6 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS PROC7 "
                StrSql = StrSql + "ON PROC7.PROCID = PLANTCONFIG.M7 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS PROC8 "
                StrSql = StrSql + "ON PROC8.PROCID = PLANTCONFIG.M8 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS PROC9 "
                StrSql = StrSql + "ON PROC9.PROCID = PLANTCONFIG.M9 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS PROC10 "
                StrSql = StrSql + "ON PROC10.PROCID = PLANTCONFIG.M10 "
                StrSql = StrSql + "WHERE PLANTCONFIG.CASEID  = " + CaseId.ToString() + ""


                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetCaseViewer:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetMaterials(ByVal MatId As Integer, ByVal MatDe1 As String, ByVal MatDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MATID, (MATDE1||'  '||MATDE2)MATDES,MATDE1,MATDE2  "
                StrSql = StrSql + "FROM MATERIALS "
                StrSql = StrSql + "WHERE MATID = CASE WHEN " + MatId.ToString() + " = -1 THEN "
                StrSql = StrSql + "MATID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + MatId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(MATDE1),'#') LIKE '" + MatDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(MATDE2),'#') LIKE '" + MatDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  MATDE1"
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetMaterials:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSustain1Cases(ByVal CaseIdS1 As Integer, ByVal CaseIdS2 As Integer, ByVal CaseDe1 As String, ByVal CaseDe2 As String, ByVal USERID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                If CaseIdS2 >= 1000 Then
                    If CaseIdS1 = -1 And CaseDe1 <> "Nothing" And CaseDe1 <> "Selected" Then
                        StrSql = "SELECT CaseId, (Casede1||'  '||Casede2)CASDES,Casede1,Casede2  "
                        StrSql = StrSql + "FROM PermissionsCases "
                        StrSql = StrSql + "WHERE USERID='" + USERID.ToString() + "' AND NVL(UPPER(Casede1),'#') LIKE '" + CaseDe1.ToUpper() + "%' "
                        StrSql = StrSql + "AND NVL(UPPER(Casede2),'#') LIKE '" + CaseDe2.ToUpper() + "%' "
                        StrSql = StrSql + "Union  "
                        StrSql = StrSql + "SELECT 0 ,'Nothing Selected' AS CASDES,'Nothing' Casede1,'Selected' Casede2 FROM DUAL  "
                    ElseIf CaseIdS1 = -1 And CaseDe1 = "Nothing" Or CaseDe1 = "Selected" Then
                        StrSql = StrSql + "SELECT 0 ,'Nothing Selected' AS CASDES,'Nothing' Casede1,'Selected' Casede2 FROM DUAL  "
                    ElseIf CaseIdS1 = 0 Then
                        StrSql = StrSql + "SELECT 0 ,'Nothing Selected' AS CASDES,'Nothing' Casede1,'Selected' Casede2 FROM DUAL  "
                    Else
                        StrSql = "SELECT CaseId, (Casede1||'  '||Casede2)CASDES,Casede1,Casede2  "
                        StrSql = StrSql + "FROM PermissionsCases "
                        StrSql = StrSql + "WHERE USERID='" + USERID.ToString() + "' AND CaseId = CASE WHEN " + CaseIdS1.ToString() + " = -1 THEN "
                        StrSql = StrSql + "CaseId "
                        StrSql = StrSql + "ELSE "
                        StrSql = StrSql + "" + CaseIdS1.ToString() + " "
                        StrSql = StrSql + "END "
                        StrSql = StrSql + "AND NVL(UPPER(Casede1),'#') LIKE '" + CaseDe1.ToUpper() + "%' "
                        StrSql = StrSql + "AND NVL(UPPER(Casede2),'#') LIKE '" + CaseDe2.ToUpper() + "%' "
                    End If
                    StrSql = StrSql + "Order By  CASDES"
                    Dts = odbUtil.FillDataSet(StrSql, MoldS1Connection)
                    Return Dts
                Else
                    If CaseIdS1 = -1 Then
                        StrSql = "SELECT CaseId, (Casede1||'  '||Casede2)CASDES,Casede1,Casede2  "
                        StrSql = StrSql + "FROM BaseCases "
                        StrSql = StrSql + "WHERE NVL(UPPER(Casede1),'#') LIKE '" + CaseDe1.ToUpper() + "%' "
                        StrSql = StrSql + "AND NVL(UPPER(Casede2),'#') LIKE '" + CaseDe2.ToUpper() + "%' "
                        StrSql = StrSql + "Union  "
                        StrSql = StrSql + "SELECT 0 ,'Nothing Selected' AS CASDES,'Nothing' Casede1,'Selected' Casede2 FROM DUAL "
                    ElseIf CaseIdS1 = 0 Then
                        StrSql = StrSql + "SELECT 0 ,'Nothing Selected' AS CASDES,'Nothing' Casede1,'Selected' Casede2 FROM DUAL "
                    Else
                        StrSql = "SELECT CaseId, (Casede1||'  '||Casede2)CASDES,Casede1,Casede2  "
                        StrSql = StrSql + "FROM BaseCases "
                        StrSql = StrSql + "WHERE CaseId = CASE WHEN " + CaseIdS1.ToString() + " = -1 THEN "
                        StrSql = StrSql + "CaseId "
                        StrSql = StrSql + "ELSE "
                        StrSql = StrSql + "" + CaseIdS1.ToString() + " "
                        StrSql = StrSql + "END "
                        StrSql = StrSql + "AND NVL(UPPER(Casede1),'#') LIKE '" + CaseDe1.ToUpper() + "%' "
                        StrSql = StrSql + "AND NVL(UPPER(Casede2),'#') LIKE '" + CaseDe2.ToUpper() + "%' "
                    End If

                    StrSql = StrSql + "Order By  CASDES"
                    Dts = odbUtil.FillDataSet(StrSql, MoldS1Connection)
                    Return Dts
                End If

            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetSustain1Cases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetDept(ByVal ProcId As Integer, ByVal ProcDe1 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim CaseId As String = HttpContext.Current.Session("MoldS2CaseId").ToString()
            Try
                StrSql = "SELECT PROCID,(PROCDE1||'  '||PROCDE2) AS PROCDE,PROCDE1  "
                StrSql = StrSql + "FROM ECON2MOLD.PROCESS "
                StrSql = StrSql + "WHERE PROCID = CASE WHEN " + ProcId.ToString() + " = -1 THEN "
                StrSql = StrSql + "PROCID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + ProcId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(PROCDE1),'#') LIKE '" + ProcDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND PROCID IN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT M1 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M2 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M3 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M4 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M5 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M6 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M7 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M8 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M9 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M10 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 1 FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 0 FROM DUAL "
                StrSql = StrSql + ") "
                StrSql = StrSql + "ORDER BY  PROCDE1"

                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetDept:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetDept(ByVal ProcId As Integer, ByVal ProcDe1 As String, ByVal ProcDe2 As String, ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PROCID,(PROCDE1||'  '||PROCDE2) AS PROCDE,PROCDE1,PROCDE2  "
                StrSql = StrSql + "FROM ECON2MOLD.PROCESS "
                StrSql = StrSql + "WHERE PROCID = CASE WHEN " + ProcId.ToString() + " = -1 THEN "
                StrSql = StrSql + "PROCID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + ProcId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(PROCDE1),'#') LIKE '" + ProcDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(PROCDE1),'#') LIKE '" + ProcDe2.ToUpper() + "%' "
                StrSql = StrSql + "AND PROCID IN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT M1 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M2 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M3 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M4 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M5 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M6 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M7 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M8 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M9 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M10 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 1 FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 0 FROM DUAL "
                StrSql = StrSql + ") "

                StrSql = StrSql + "ORDER BY  PROCDE1"

                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetDept:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetPref(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PREFERENCES.CASEID,  "
                StrSql = StrSql + "UNITS, "
                StrSql = StrSql + "CURRENCY, "
                StrSql = StrSql + "CURR, "
                StrSql = StrSql + "CONVWT, "
                StrSql = StrSql + "CONVTHICK, "
                StrSql = StrSql + "OCOUNTRY, "
                StrSql = StrSql + "DCOUNTRY, "
                StrSql = StrSql + "TITLE1, "
                StrSql = StrSql + "TITLE3, "
                StrSql = StrSql + "TITLE2, "
                StrSql = StrSql + "CONVAREA, "
                StrSql = StrSql + "TITLE4, "
                StrSql = StrSql + "CONVAREA2, "
                StrSql = StrSql + "TITLE5, "
                StrSql = StrSql + "CONVTHICK2, "
                StrSql = StrSql + "CONVTHICK3, "
                StrSql = StrSql + "CONVGALLON, "
                StrSql = StrSql + "TITLE6, "
                StrSql = StrSql + "TITLE7, "
                StrSql = StrSql + "TITLE8, "
                StrSql = StrSql + "TITLE9, "
                StrSql = StrSql + "EFFDATE, "
                StrSql = StrSql + "TO_CHAR(EFFDATE,'MON DD,YYYY')AS EDATE, "
                StrSql = StrSql + "INVENTORYTYPE, "
                StrSql = StrSql + "RESULTSPL.PVOLUSE, "
                StrSql = StrSql + "TITLE10, "
                StrSql = StrSql + "TITLE11, "
                StrSql = StrSql + "TITLE12, "
                StrSql = StrSql + "ERGYCALC,ERGYTYPE "
                StrSql = StrSql + "FROM PREFERENCES "
                StrSql = StrSql + "INNER JOIN RESULTSPL "
                StrSql = StrSql + "ON RESULTSPL.CASEID = PREFERENCES.CASEID "
                StrSql = StrSql + "WHERE PREFERENCES.CASEID = " + CaseId.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetProductFormt(ByVal FormtId As Integer, ByVal FormtDe1 As String, ByVal FormtDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  FormatID,  "
                StrSql = StrSql + "(FormatDe1 || ' ' ||  FormatDe2) FormatDes, "
                StrSql = StrSql + "FormatDe1, "
                StrSql = StrSql + "FormatDe2 "
                StrSql = StrSql + "FROM PRODUCTFORMAT "
                StrSql = StrSql + "WHERE FormatID = CASE WHEN " + FormtId.ToString() + " = -1 "
                StrSql = StrSql + "THEN FormatID ELSE " + FormtId.ToString() + "  END "
                StrSql = StrSql + "AND NVL(UPPER(FormatDe1),'#') LIKE '" + FormtDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(FormatDe1),'#') LIKE '" + FormtDe2.ToUpper() + "%' "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT  FormatID, "
                StrSql = StrSql + "(FormatDe1 || ' ' ||  FormatDe2) FormatDes, "
                StrSql = StrSql + "FormatDe1, "
                StrSql = StrSql + "FormatDe2 "
                StrSql = StrSql + "FROM PRODUCTFORMAT2 "
                StrSql = StrSql + "WHERE FormatID = CASE WHEN " + FormtId.ToString() + " = -1 "
                StrSql = StrSql + "THEN FormatID ELSE " + FormtId.ToString() + "  END "
                StrSql = StrSql + "AND NVL(UPPER(FormatDe1),'#') LIKE '" + FormtDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(FormatDe1),'#') LIKE '" + FormtDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  FormatDe1 "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetProductFormat:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPallets(ByVal PalletId As Integer, ByVal PalDe1 As String, ByVal PalDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  DISTINCT PALLETID,PALLETDE1,PALLETDE2, (PALLETDE1 || ' ' ||  PALLETDE2) PALLETDES, replace((PALLETDE1 || ' ' ||  PALLETDE2),'" + Chr(34) + "','##') PALLETDES1 "
                StrSql = StrSql + "FROM SUSTAIN.PALLETARCH "
                StrSql = StrSql + "WHERE PALLETID = CASE WHEN " + PalletId.ToString() + " = -1 THEN "
                StrSql = StrSql + "PALLETID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + PalletId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(PALLETDE1),'#') LIKE '" + PalDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(PALLETDE2),'#') LIKE '" + PalDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  PALLETDE1"
                Dts = odbUtil.FillDataSet(StrSql, MoldS1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetPallets:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSustainableMaterial(ByVal TYPEID As Integer, ByVal TYPE As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select TYPEID, TYPE  "
                StrSql = StrSql + "FROM SUSTAIN.HEALTHANDSAFETY "
                StrSql = StrSql + "WHERE TYPEID = CASE WHEN " + TYPEID.ToString() + " = -1 THEN "
                StrSql = StrSql + "TYPEID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + TYPEID.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(TYPE),'#') LIKE '" + TYPE.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  TYPEID"
                Dts = odbUtil.FillDataSet(StrSql, MoldS1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetSustainableMaterial:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetDeptPlantConfig(ByVal ProcId As Integer, ByVal ProcDe1 As String, ByVal ProcDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PROCID,(PROCDE1||'  '||PROCDE2) AS PROCDE,PROCDE1,PROCDE2  "
                StrSql = StrSql + "FROM PROCESS "
                StrSql = StrSql + "WHERE PROCID = CASE WHEN " + ProcId.ToString() + " = -1 THEN "
                StrSql = StrSql + "PROCID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + ProcId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(PROCDE1),'#') LIKE '" + ProcDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(PROCDE1),'#') LIKE '" + ProcDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  PROCDE1"

                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetDeptPlantConfig:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetEquipment(ByVal EqId As Integer, ByVal EqDe1 As String, ByVal EqlDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select equipID,equipDE1,equipDE2,(equipDE1 || ' ' || equipDE2) equipDES,replace((equipDE1 || ' ' ||  equipDE2),Chr(34) ,'##') equipDES1 "
                StrSql = StrSql + "from equipment  "
                StrSql = StrSql + "WHERE EQUIPID = CASE WHEN " + EqId.ToString() + " = -1 THEN "
                StrSql = StrSql + "EQUIPID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + EqId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(equipDE1),'#') LIKE '" + EqDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(equipDE2),'#') LIKE '" + EqlDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY equipDE1"
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetEquipment:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSupportEquipment(ByVal EqId As Integer, ByVal EqDe1 As String, ByVal EqlDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select equipID,equipDE1,equipDE2,(equipDE1 || ' ' || equipDE2) equipDES,replace((equipDE1 || ' ' ||  equipDE2),Chr(34) ,'##') equipDES1 "
                StrSql = StrSql + "from equipment2   "
                StrSql = StrSql + "WHERE EQUIPID = CASE WHEN " + EqId.ToString() + " = -1 THEN "
                StrSql = StrSql + "EQUIPID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + EqId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(equipDE1),'#') LIKE '" + EqDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(equipDE2),'#') LIKE '" + EqlDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY equipDE1"
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetSupportEquipment:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetEFFCOUNTRY(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select (CASE WHEN PREF.OCOUNTRY=0 THEN  "
                StrSql = StrSql + "'personnel' "
                StrSql = StrSql + "WHEN PREF.OCOUNTRY=1 THEN "
                StrSql = StrSql + "'personnelChina' "
                StrSql = StrSql + "WHEN PREF.OCOUNTRY=2 THEN "
                StrSql = StrSql + "'personnel' "
                StrSql = StrSql + "END) AS COUNTRY "
                StrSql = StrSql + "FROM PREFERENCES PREF "
                StrSql = StrSql + "WHERE CASEID = " + CaseId.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPersonnelInfo(ByVal PersId As Integer, ByVal PersDe1 As String, ByVal PersDe2 As String, ByVal Country As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select persid, persde1,persde2,(persde1 || ' ' ||  persde2) as persDES, persde2,replace((persde1 || ' ' ||  persde2),Chr(34) ,'##') persDES1"
                StrSql = StrSql + " from " + Country
                StrSql = StrSql + " WHERE persid = CASE WHEN " + PersId.ToString() + " = -1 THEN "
                StrSql = StrSql + "persid "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + PersId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(persde1),'#') LIKE '" + PersDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(persde2),'#') LIKE '" + PersDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY persde1"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetPersonnelInfo:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUserCompanyUsers(ByVal UserName As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  USERS.USERID,  "
                StrSql = StrSql + "USERS.USERNAME "
                StrSql = StrSql + "FROM USERS "
                StrSql = StrSql + "INNER JOIN USERPERMISSIONS "
                StrSql = StrSql + "ON USERPERMISSIONS.USERID= USERS.USERID "
                StrSql = StrSql + "INNER JOIN SERVICES "
                StrSql = StrSql + "ON SERVICES.SERVICEID = USERPERMISSIONS.SERVICEID "
                StrSql = StrSql + "WHERE USERS.LICENSEID=(SELECT LICENSEID FROM USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper.ToString() + "') "
                StrSql = StrSql + "AND SERVICES.SERVICEDE IN ('MOLDE1','MOLDE2','MOLDS1','MOLDS2') "
                StrSql = StrSql + "AND UPPER(USERPERMISSIONS.USERROLE)='READWRITE' "
                StrSql = StrSql + "GROUP BY USERS.USERID,USERS.USERNAME "
                StrSql = StrSql + "ORDER BY USERS.USERNAME "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetUserCompanyUsers:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetEffDate(ByVal Inventorytype As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT TO_CHAR(EFFDATE,'MON DD,YYYY')AS EDATE FROM SUSTAIN.MATERIALSARCH WHERE INVENTORYTYPE=" + Inventorytype + ""
                Dts = odbUtil.FillDataSet(StrSql, MoldS1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetEffDate:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetLCI() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT INVENTORYID, INVENTORY FROM INVENTORYTYPE  ORDER BY INVENTORY DESC"
                Dts = odbUtil.FillDataSet(StrSql, MoldS1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetLCI:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetLCI(ByVal Inventorytype As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT INVENTORYID, INVENTORY FROM INVENTORYTYPE WHERE INVENTORYID=" + Inventorytype + ""
                Dts = odbUtil.FillDataSet(StrSql, MoldS1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetLCI:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetConversionFactor() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT "
                StrSql = StrSql + "MICPMIL, KGPLB, M2PMSI, M2PSQFT, MPFT, KMPMILE, JPMJ, LITPGAL, IN2PSQFT "
                StrSql = StrSql + "FROM CONVFACTORS "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetConversionFactor:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCustomerPageLists() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select PageId,PageName from CustomerPages"
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetCustomerPageLists:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function


#End Region

#Region "UserDetails"
        Public Function GetUserDetails(ByVal Id As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERS.USERID,  "
                StrSql = StrSql + "UPPER(USERNAME)USERNAME, "
                StrSql = StrSql + "USERNAME AS TOOLUSERNAME, "

                'Checking for License Administrator
                StrSql = StrSql + "NVL(USERS.ISIADMINLICUSR,'N')ISIADMINLICUSR,"
                StrSql = StrSql + "USERS.LICENSEID,"

                StrSql = StrSql + "SERVICES.SERVICEDE, "
                StrSql = StrSql + "USERPERMISSIONS.USERROLE AS SERVIECROLE, "
                StrSql = StrSql + "USERPERMISSIONS.MAXCASECOUNT, "
                StrSql = StrSql + "(CASE WHEN NVL(USERS.ISINTERNALUSR,'N') ='Y' THEN "
                StrSql = StrSql + "'AADMIN' "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "'USER' "
                StrSql = StrSql + "END)USERROLE "
                StrSql = StrSql + "FROM ECON.ULOGIN "
                StrSql = StrSql + "INNER JOIN USERS "
                StrSql = StrSql + "ON UPPER(USERS.USERNAME) = UPPER(ULOGIN.UNAME) "
                StrSql = StrSql + "AND UPPER(USERS.PASSWORD) = UPPER(ULOGIN.UPWD) "
                StrSql = StrSql + "INNER JOIN USERPERMISSIONS "
                StrSql = StrSql + "ON USERPERMISSIONS.USERID = USERS.USERID "
                StrSql = StrSql + "INNER JOIN SERVICES "
                StrSql = StrSql + "ON SERVICES.SERVICEID = USERPERMISSIONS.SERVICEID "
                StrSql = StrSql + "WHERE ULOGIN.ID = " + Id.ToString() + " "
                StrSql = StrSql + "AND SERVICES.SERVICEDE='MOLDS2' "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetUserDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetTotalCaseCount(ByVal USERID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT COUNT(*) AS TOTALCOUNT FROM PERMISSIONSCASES  "
                StrSql = StrSql + "WHERE USERID='" + USERID.ToString() + "' "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetTotalCaseCount:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Notes"
        Public Function GetAssumptionPageDetails(ByVal AssumptionType As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT ASSUMPTIONTYPEID, ASSUMPTIONTYPEDE1, ASSUMPTIONTYPEDE2, ASSUMPTIONTYPECODE  "
                StrSql = StrSql + "FROM ASSUMPTIONTYPES "
                StrSql = StrSql + "WHERE ASSUMPTIONTYPECODE ='" + AssumptionType + "'"
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetAssumptionPageDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPageNoteDetails(ByVal AssumptionType As String, ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID, USERID, ASSUMPTIONTYPE, NOTE FROM NOTES  "
                StrSql = StrSql + "WHERE CASEID = " + CaseId + " "
                StrSql = StrSql + "AND ASSUMPTIONTYPE ='" + AssumptionType + "' "

                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetPageNoteDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCaseNoteDetails(ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "Select (Assumptiontypes.Assumptiontypede1||' '||Assumptiontypes.Assumptiontypede2)assumptiontypedes,  "
                StrSql = StrSql + "Notes.Note "
                StrSql = StrSql + "From Notes "
                StrSql = StrSql + "Inner Join Assumptiontypes "
                StrSql = StrSql + "On Assumptiontypes.Assumptiontypecode = Notes.Assumptiontype "
                StrSql = StrSql + "Where Caseid = " + CaseId + "  Order By Assumptiontypes.Assumptiontypeid"


                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetCaseNoteDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Assumptions Pages"
        Public Function GetExtrusionDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MI.CASEID,  "
                StrSql = StrSql + "MI.M1, "
                StrSql = StrSql + "MI.M2, "
                StrSql = StrSql + "MI.M3, "
                StrSql = StrSql + "MI.M4, "
                StrSql = StrSql + "MI.M5, "
                StrSql = StrSql + "MI.M6, "
                StrSql = StrSql + "MI.M7, "
                StrSql = StrSql + "MI.M8, "
                StrSql = StrSql + "MI.M9, "
                StrSql = StrSql + "MI.M10, "
                StrSql = StrSql + "MI.PLATE, "
                StrSql = StrSql + "MI.IP1, "
                StrSql = StrSql + "MI.IP2, "
                StrSql = StrSql + "MI.IP3, "
                StrSql = StrSql + "MI.IP4, "
                StrSql = StrSql + "MI.IP5, "
                StrSql = StrSql + "MI.IP6, "
                StrSql = StrSql + "MI.IP7, "
                StrSql = StrSql + "MI.IP8, "
                StrSql = StrSql + "MI.IP9, "
                StrSql = StrSql + "MI.IP10, "
                StrSql = StrSql + "MI.Q1, "
                StrSql = StrSql + "MI.Q2, "
                StrSql = StrSql + "MI.Q3, "
                StrSql = StrSql + "MI.Q4, "
                StrSql = StrSql + "MI.Q5, "
                StrSql = StrSql + "MI.Q6, "
                StrSql = StrSql + "MI.Q7, "
                StrSql = StrSql + "MI.Q8, "
                StrSql = StrSql + "MI.Q9, "
                StrSql = StrSql + "MI.Q10, "
                StrSql = StrSql + "MI.C1, "
                StrSql = StrSql + "MI.C2, "
                StrSql = StrSql + "MI.C3, "
                StrSql = StrSql + "MI.C4, "
                StrSql = StrSql + "MI.C5, "
                StrSql = StrSql + "MI.C6, "
                StrSql = StrSql + "MI.C7, "
                StrSql = StrSql + "MI.C8, "
                StrSql = StrSql + "MI.C9, "
                StrSql = StrSql + "MI.C10, "
                StrSql = StrSql + "NVL((SELECT PRODWT*PREF.CONVWT FROM SUSTAIN1MOLD.TOTAL WHERE CASEID=MI.C1),0) WEIGHTS1,  "
                StrSql = StrSql + "NVL((SELECT PRODWT*PREF.CONVWT FROM SUSTAIN1MOLD.TOTAL WHERE CASEID=MI.C2),0) WEIGHTS2, "
                StrSql = StrSql + "NVL((SELECT PRODWT*PREF.CONVWT FROM SUSTAIN1MOLD.TOTAL WHERE CASEID=MI.C3),0) WEIGHTS3, "
                StrSql = StrSql + "NVL((SELECT PRODWT*PREF.CONVWT FROM SUSTAIN1MOLD.TOTAL WHERE CASEID=MI.C4),0) WEIGHTS4, "
                StrSql = StrSql + "NVL((SELECT PRODWT*PREF.CONVWT FROM SUSTAIN1MOLD.TOTAL WHERE CASEID=MI.C5),0) WEIGHTS5, "
                StrSql = StrSql + "NVL((SELECT PRODWT*PREF.CONVWT FROM SUSTAIN1MOLD.TOTAL WHERE CASEID=MI.C6),0) WEIGHTS6, "
                StrSql = StrSql + "NVL((SELECT PRODWT*PREF.CONVWT FROM SUSTAIN1MOLD.TOTAL WHERE CASEID=MI.C7),0) WEIGHTS7, "
                StrSql = StrSql + "NVL((SELECT PRODWT*PREF.CONVWT FROM SUSTAIN1MOLD.TOTAL WHERE CASEID=MI.C8),0) WEIGHTS8, "
                StrSql = StrSql + "NVL((SELECT PRODWT*PREF.CONVWT FROM SUSTAIN1MOLD.TOTAL WHERE CASEID=MI.C9),0) WEIGHTS9, "
                StrSql = StrSql + "NVL((SELECT PRODWT*PREF.CONVWT FROM SUSTAIN1MOLD.TOTAL WHERE CASEID=MI.C10),0) WEIGHTS10, "

                StrSql = StrSql + "(MI.T1*PREF.CONVWT) AS WEIGHTP1, "
                StrSql = StrSql + "(MI.T2*PREF.CONVWT) AS WEIGHTP2, "
                StrSql = StrSql + "(MI.T3*PREF.CONVWT) AS WEIGHTP3, "
                StrSql = StrSql + "(MI.T4*PREF.CONVWT) AS WEIGHTP4, "
                StrSql = StrSql + "(MI.T5*PREF.CONVWT) AS WEIGHTP5, "
                StrSql = StrSql + "(MI.T6*PREF.CONVWT) AS WEIGHTP6, "
                StrSql = StrSql + "(MI.T7*PREF.CONVWT) AS WEIGHTP7, "
                StrSql = StrSql + "(MI.T8*PREF.CONVWT) AS WEIGHTP8, "
                StrSql = StrSql + "(MI.T9*PREF.CONVWT) AS WEIGHTP9, "
                StrSql = StrSql + "(MI.T10*PREF.CONVWT) AS WEIGHTP10, "
                StrSql = StrSql + "MI.R1, "
                StrSql = StrSql + "MI.R2, "
                StrSql = StrSql + "MI.R3, "
                StrSql = StrSql + "MI.R4, "
                StrSql = StrSql + "MI.R5, "
                StrSql = StrSql + "MI.R6, "
                StrSql = StrSql + "MI.R7, "
                StrSql = StrSql + "MI.R8, "
                StrSql = StrSql + "MI.R9, "
                StrSql = StrSql + "MI.R10, "
                StrSql = StrSql + "MI.E1, "
                StrSql = StrSql + "MI.E2, "
                StrSql = StrSql + "MI.E3, "
                StrSql = StrSql + "MI.E4, "
                StrSql = StrSql + "MI.E5, "
                StrSql = StrSql + "MI.E6, "
                StrSql = StrSql + "MI.E7, "
                StrSql = StrSql + "MI.E8, "
                StrSql = StrSql + "MI.E9, "
                StrSql = StrSql + "MI.E10, "
                StrSql = StrSql + "MO.P1, "
                StrSql = StrSql + "MO.P2, "
                StrSql = StrSql + "MO.P3, "
                StrSql = StrSql + "MO.P4, "
                StrSql = StrSql + "MO.P5, "
                StrSql = StrSql + "MO.P6, "
                StrSql = StrSql + "MO.P7, "
                StrSql = StrSql + "MO.P8, "
                StrSql = StrSql + "MO.P9, "
                StrSql = StrSql + "MO.P10, "
                StrSql = StrSql + "MI.D1, "
                StrSql = StrSql + "MI.D2, "
                StrSql = StrSql + "MI.D3, "
                StrSql = StrSql + "MI.D4, "
                StrSql = StrSql + "MI.D5, "
                StrSql = StrSql + "MI.D6, "
                StrSql = StrSql + "MI.D7, "
                StrSql = StrSql + "MI.D8, "
                StrSql = StrSql + "MI.D9, "
                StrSql = StrSql + "MI.D10, "
                StrSql = StrSql + "TOTAL.THICK*PREF.CONVWT AS WEIGHTTOT, "
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
                StrSql = StrSql + "FROM MATERIALINPUT MI "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID= MI.CASEID "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT1 "
                StrSql = StrSql + "ON MAT1.MATID=MI.M1 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT2 "
                StrSql = StrSql + "ON MAT2.MATID=MI.M2 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT3 "
                StrSql = StrSql + "ON MAT3.MATID=MI.M3 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT4 "
                StrSql = StrSql + "ON MAT4.MATID=MI.M4 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT5 "
                StrSql = StrSql + "ON MAT5.MATID=MI.M5 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT6 "
                StrSql = StrSql + "ON MAT6.MATID=MI.M6 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT7 "
                StrSql = StrSql + "ON MAT7.MATID=MI.M7 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT8 "
                StrSql = StrSql + "ON MAT8.MATID=MI.M8 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT9 "
                StrSql = StrSql + "ON MAT9.MATID=MI.M9 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT10 "
                StrSql = StrSql + "ON MAT10.MATID=MI.M10 "
                StrSql = StrSql + "LEFT JOIN MATERIALOUTPUT MO "
                StrSql = StrSql + "ON MO.CASEID=MI.CASEID "
                StrSql = StrSql + "INNER JOIN TOTAL "
                StrSql = StrSql + "ON TOTAL.CASEID=MI.CASEID "
                StrSql = StrSql + "WHERE MI.CASEID=" + CaseId.ToString() + "  ORDER BY MI.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetProductFromatIn(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PF.M1,  "
                StrSql = StrSql + "(PF.M2*PREF.CONVTHICK) AS M2, "
                StrSql = StrSql + "(PF.M3*PREF.CONVTHICK* "
                StrSql = StrSql + "(CASE PREF.UNITS "
                StrSql = StrSql + "WHEN 1 THEN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "CASE PF.M1  WHEN 1 THEN 0.01204  ELSE 1 END "
                StrSql = StrSql + ") "
                StrSql = StrSql + "ELSE 1 "
                StrSql = StrSql + "END "
                StrSql = StrSql + ") "
                StrSql = StrSql + ") AS M3, "
                StrSql = StrSql + "(PF.M4*PREF.CONVTHICK) AS M4, "
                StrSql = StrSql + "PF.M5, "
                StrSql = StrSql + "PF.M6, "
                StrSql = StrSql + "(TOTAL.PRODWT*PREF.CONVWT) AS PRODUCTWEIGHT, "
                StrSql = StrSql + "NVL(PRODUCTFORMAT.M1,PRODUCTFORMAT2.M1 ) AS FORMAT_M1, "
                StrSql = StrSql + "NVL(PRODUCTFORMAT.M2,PRODUCTFORMAT2.M2 ) AS FORMAT_M2, "
                StrSql = StrSql + "NVL(PRODUCTFORMAT.M3,PRODUCTFORMAT2.M3 ) AS FORMAT_M3, "
                StrSql = StrSql + "NVL(PRODUCTFORMAT.M4,PRODUCTFORMAT2.M4 ) AS FORMAT_M4, "
                StrSql = StrSql + "NVL(PRODUCTFORMAT.M5,PRODUCTFORMAT2.M5 ) AS FORMAT_M5, "
                StrSql = StrSql + "(CASE "
                StrSql = StrSql + "WHEN TOTAL.ROLLDIA > 0 THEN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN 'Roll Diameter (in)'  ELSE 'Roll Diameter (mm)'  END "
                StrSql = StrSql + ") "
                StrSql = StrSql + "END "
                StrSql = StrSql + ") "
                StrSql = StrSql + "AS RUNIT, "
                StrSql = StrSql + "(CASE "
                StrSql = StrSql + "WHEN TOTAL.ROLLDIA > 0 THEN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "TOTAL.ROLLDIA*PREF.CONVTHICK "
                StrSql = StrSql + ") "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END "
                StrSql = StrSql + ") "
                StrSql = StrSql + "AS ROLLDIA, "
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
                StrSql = StrSql + "FROM PRODUCTFORMATIN PF "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=PF.CASEID "
                StrSql = StrSql + "LEFT OUTER JOIN ECON2MOLD.PRODUCTFORMAT "
                StrSql = StrSql + "ON PRODUCTFORMAT.FORMATID = PF.M1 "
                StrSql = StrSql + "AND PREF.UNITS = 0 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON2MOLD.PRODUCTFORMAT2 "
                StrSql = StrSql + "ON PRODUCTFORMAT2.FORMATID = PF.M1 "
                StrSql = StrSql + "AND PREF.UNITS = 1 "
                StrSql = StrSql + "INNER JOIN TOTAL "
                StrSql = StrSql + "ON TOTAL.CASEID=PF.CASEID "
                StrSql = StrSql + "WHERE PF.CASEID=" + CaseId.ToString() + " ORDER BY PF.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetProductFromatIn:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPalletAndTruck(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT TPI.CASEID,  "
                StrSql = StrSql + "TPI.M1*PREF.CONVTHICK AS M1, "
                StrSql = StrSql + "TPI.M2*PREF.CONVTHICK AS M2, "
                StrSql = StrSql + "TPI.M3*PREF.CONVTHICK AS M3, "
                StrSql = StrSql + "TPI.M4, "
                StrSql = StrSql + "TPI.M5, "
                StrSql = StrSql + "TPI.T1*PREF.CONVTHICK AS T1, "
                StrSql = StrSql + "TPI.T2*PREF.CONVTHICK AS T2, "
                StrSql = StrSql + "TPI.T3*PREF.CONVTHICK AS T3, "
                StrSql = StrSql + "TPI.T4*PREF.CONVWT AS T4, "
                StrSql = StrSql + "TPI.T5, "
                StrSql = StrSql + "(TOTAL.TOTWTPERT*PREF.CONVWT) AS CALWEIGHT, "
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
                StrSql = StrSql + "FROM TRUCKPALLETIN TPI "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=TPI.CASEID "
                StrSql = StrSql + "INNER JOIN TOTAL ON "
                StrSql = StrSql + "TOTAL.CASEID=TPI.CASEID "
                StrSql = StrSql + "WHERE TPI.CASEID =" + CaseId.ToString() + ""
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetPalletAndTruck:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPalletInDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PI.CASEID,  "
                StrSql = StrSql + "PI.M1, "
                StrSql = StrSql + "PI.M2, "
                StrSql = StrSql + "PI.M3, "
                StrSql = StrSql + "PI.M4, "
                StrSql = StrSql + "PI.M5, "
                StrSql = StrSql + "PI.M6, "
                StrSql = StrSql + "PI.M7, "
                StrSql = StrSql + "PI.M8, "
                StrSql = StrSql + "PI.M9, "
                StrSql = StrSql + "PI.M10, "
                StrSql = StrSql + "PI.INVENTORYTYPE, "
                StrSql = StrSql + "TO_CHAR(NVL(PI.EFFDATE,TO_DATE('JAN 01,1900','MON DD,YYYY')),'MON DD,YYYY')AS EFFDATE, "
                StrSql = StrSql + "PI.T1 NUM1, "
                StrSql = StrSql + "PI.T2 NUM2, "
                StrSql = StrSql + "PI.T3 NUM3, "
                StrSql = StrSql + "PI.T4 NUM4, "
                StrSql = StrSql + "PI.T5 NUM5, "
                StrSql = StrSql + "PI.T6 NUM6, "
                StrSql = StrSql + "PI.T7 NUM7, "
                StrSql = StrSql + "PI.T8 NUM8, "
                StrSql = StrSql + "PI.T9 NUM9, "
                StrSql = StrSql + "PI.T10 NUM10, "
                StrSql = StrSql + "PI.R1 AS NU1, "
                StrSql = StrSql + "PI.R2 AS NU2, "
                StrSql = StrSql + "PI.R3 AS NU3, "
                StrSql = StrSql + "PI.R4 AS NU4, "
                StrSql = StrSql + "PI.R5 AS NU5, "
                StrSql = StrSql + "PI.R6 AS NU6, "
                StrSql = StrSql + "PI.R7 AS NU7, "
                StrSql = StrSql + "PI.R8 AS NU8, "
                StrSql = StrSql + "PI.R9 AS NU9, "
                StrSql = StrSql + "PI.R10 AS NU10, "
                StrSql = StrSql + "(NVL(PA1.WEIGHT,0)*PREF.CONVWT) AS WS1, "
                StrSql = StrSql + "(NVL(PA2.WEIGHT,0)*PREF.CONVWT) AS WS2, "
                StrSql = StrSql + "(NVL(PA3.WEIGHT,0)*PREF.CONVWT) AS WS3, "
                StrSql = StrSql + "(NVL(PA4.WEIGHT,0)*PREF.CONVWT) AS WS4, "
                StrSql = StrSql + "(NVL(PA5.WEIGHT,0)*PREF.CONVWT) AS WS5, "
                StrSql = StrSql + "(NVL(PA6.WEIGHT,0)*PREF.CONVWT) AS WS6, "
                StrSql = StrSql + "(NVL(PA7.WEIGHT,0)*PREF.CONVWT) AS WS7, "
                StrSql = StrSql + "(NVL(PA8.WEIGHT,0)*PREF.CONVWT) AS WS8, "
                StrSql = StrSql + "(NVL(PA9.WEIGHT,0)*PREF.CONVWT) AS WS9, "
                StrSql = StrSql + "(NVL(PA10.WEIGHT,0)*PREF.CONVWT) AS WS10, "
                StrSql = StrSql + "PI.W1*PREF.convwt AS WP1 , "
                StrSql = StrSql + "PI.W2*PREF.convwt AS WP2, "
                StrSql = StrSql + "PI.W3*PREF.convwt AS WP3, "
                StrSql = StrSql + "PI.W4*PREF.convwt AS WP4, "
                StrSql = StrSql + "PI.W5*PREF.convwt AS WP5, "
                StrSql = StrSql + "PI.W6*PREF.convwt AS WP6, "
                StrSql = StrSql + "PI.W7*PREF.convwt AS WP7, "
                StrSql = StrSql + "PI.W8*PREF.convwt AS WP8, "
                StrSql = StrSql + "PI.W9*PREF.convwt AS WP9, "
                StrSql = StrSql + "PI.W10*PREF.convwt AS WP10, "
                StrSql = StrSql + "NVL(PA1.JOULE,0)/PREF.convwt AS ES1, "
                StrSql = StrSql + "NVL(PA2.JOULE,0)/PREF.convwt AS ES2, "
                StrSql = StrSql + "NVL(PA3.JOULE,0)/PREF.convwt AS ES3, "
                StrSql = StrSql + "NVL(PA4.JOULE,0)/PREF.convwt AS ES4, "
                StrSql = StrSql + "NVL(PA5.JOULE,0)/PREF.convwt AS ES5, "
                StrSql = StrSql + "NVL(PA6.JOULE,0)/PREF.convwt AS ES6, "
                StrSql = StrSql + "NVL(PA7.JOULE,0)/PREF.convwt AS ES7, "
                StrSql = StrSql + "NVL(PA8.JOULE,0)/PREF.convwt AS ES8, "
                StrSql = StrSql + "NVL(PA9.JOULE,0)/PREF.convwt AS ES9, "
                StrSql = StrSql + "NVL(PA10.JOULE,0)/PREF.convwt AS ES10, "
                StrSql = StrSql + "PALLETENERGYPREF.M1/PREF.convwt AS EP1, "
                StrSql = StrSql + "PALLETENERGYPREF.M2/PREF.convwt AS EP2, "
                StrSql = StrSql + "PALLETENERGYPREF.M3/PREF.convwt AS EP3, "
                StrSql = StrSql + "PALLETENERGYPREF.M4/PREF.convwt AS EP4, "
                StrSql = StrSql + "PALLETENERGYPREF.M5/PREF.convwt AS EP5, "
                StrSql = StrSql + "PALLETENERGYPREF.M6/PREF.convwt AS EP6, "
                StrSql = StrSql + "PALLETENERGYPREF.M7/PREF.convwt AS EP7, "
                StrSql = StrSql + "PALLETENERGYPREF.M8/PREF.convwt AS EP8, "
                StrSql = StrSql + "PALLETENERGYPREF.M9/PREF.convwt AS EP9, "
                StrSql = StrSql + "PALLETENERGYPREF.M10/PREF.convwt AS EP10, "
                StrSql = StrSql + "NVL(PA1.PRICE,0) AS CO2S1, "
                StrSql = StrSql + "NVL(PA2.PRICE,0) AS CO2S2, "
                StrSql = StrSql + "NVL(PA3.PRICE,0) AS CO2S3, "
                StrSql = StrSql + "NVL(PA4.PRICE,0) AS CO2S4, "
                StrSql = StrSql + "NVL(PA5.PRICE,0) AS CO2S5, "
                StrSql = StrSql + "NVL(PA6.PRICE,0) AS CO2S6, "
                StrSql = StrSql + "NVL(PA7.PRICE,0) AS CO2S7, "
                StrSql = StrSql + "NVL(PA8.PRICE,0) AS CO2S8, "
                StrSql = StrSql + "NVL(PA9.PRICE,0) AS CO2S9, "
                StrSql = StrSql + "NVL(PA10.PRICE,0) AS CO2S10, "
                StrSql = StrSql + "PI.P1 AS CO2P1, "
                StrSql = StrSql + "PI.P2 AS CO2P2, "
                StrSql = StrSql + "PI.P3 AS CO2P3, "
                StrSql = StrSql + "PI.P4 AS CO2P4, "
                StrSql = StrSql + "PI.P5 AS CO2P5, "
                StrSql = StrSql + "PI.P6 AS CO2P6, "
                StrSql = StrSql + "PI.P7 AS CO2P7, "
                StrSql = StrSql + "PI.P8 AS CO2P8, "
                StrSql = StrSql + "PI.P9 AS CO2P9, "
                StrSql = StrSql + "PI.P10 AS CO2P10, "
                'Water Started
                StrSql = StrSql + "(NVL(PA1.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS1, "
                StrSql = StrSql + "(NVL(PA2.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS2, "
                StrSql = StrSql + "(NVL(PA3.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS3, "
                StrSql = StrSql + "(NVL(PA4.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS4, "
                StrSql = StrSql + "(NVL(PA5.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS5, "
                StrSql = StrSql + "(NVL(PA6.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS6, "
                StrSql = StrSql + "(NVL(PA7.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS7, "
                StrSql = StrSql + "(NVL(PA8.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS8, "
                StrSql = StrSql + "(NVL(PA9.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS9, "
                StrSql = StrSql + "(NVL(PA10.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS10, "
                StrSql = StrSql + "(PATWATER.M1*PREF.CONVGALLON/PREF.CONVWT) AS WATERP1, "
                StrSql = StrSql + "(PATWATER.M2*PREF.CONVGALLON/PREF.CONVWT) AS WATERP2, "
                StrSql = StrSql + "(PATWATER.M3*PREF.CONVGALLON/PREF.CONVWT) AS WATERP3, "
                StrSql = StrSql + "(PATWATER.M4*PREF.CONVGALLON/PREF.CONVWT) AS WATERP4, "
                StrSql = StrSql + "(PATWATER.M5*PREF.CONVGALLON/PREF.CONVWT) AS WATERP5, "
                StrSql = StrSql + "(PATWATER.M6*PREF.CONVGALLON/PREF.CONVWT) AS WATERP6, "
                StrSql = StrSql + "(PATWATER.M7*PREF.CONVGALLON/PREF.CONVWT) AS WATERP7, "
                StrSql = StrSql + "(PATWATER.M8*PREF.CONVGALLON/PREF.CONVWT) AS WATERP8, "
                StrSql = StrSql + "(PATWATER.M9*PREF.CONVGALLON/PREF.CONVWT) AS WATERP9, "
                StrSql = StrSql + "(PATWATER.M10*PREF.CONVGALLON/PREF.CONVWT) AS WATERP10, "
                'Water End

                StrSql = StrSql + "NVL(PA1.RECOVERYSUG,0) AS RECS1, "
                StrSql = StrSql + "NVL(PA2.RECOVERYSUG,0) AS RECS2, "
                StrSql = StrSql + "NVL(PA3.RECOVERYSUG,0) AS RECS3, "
                StrSql = StrSql + "NVL(PA4.RECOVERYSUG,0) AS RECS4, "
                StrSql = StrSql + "NVL(PA5.RECOVERYSUG,0) AS RECS5, "
                StrSql = StrSql + "NVL(PA6.RECOVERYSUG,0) AS RECS6, "
                StrSql = StrSql + "NVL(PA7.RECOVERYSUG,0) AS RECS7, "
                StrSql = StrSql + "NVL(PA8.RECOVERYSUG,0) AS RECS8, "
                StrSql = StrSql + "NVL(PA9.RECOVERYSUG,0) AS RECS9, "
                StrSql = StrSql + "NVL(PA10.RECOVERYSUG,0) AS RECS10, "
                StrSql = StrSql + "PI.REC1 AS RECP1, "
                StrSql = StrSql + "PI.REC2 AS RECP2, "
                StrSql = StrSql + "PI.REC3 AS RECP3, "
                StrSql = StrSql + "PI.REC4 AS RECP4, "
                StrSql = StrSql + "PI.REC5 AS RECP5, "
                StrSql = StrSql + "PI.REC6 AS RECP6, "
                StrSql = StrSql + "PI.REC7 AS RECP7, "
                StrSql = StrSql + "PI.REC8 AS RECP8, "
                StrSql = StrSql + "PI.REC9 AS RECP9, "
                StrSql = StrSql + "PI.REC10 AS RECP10, "
                StrSql = StrSql + "NVL(PA1.OSHAFACTOR,'Nothing') AS SMS1, "
                StrSql = StrSql + "NVL(PA2.OSHAFACTOR,'Nothing') AS SMS2, "
                StrSql = StrSql + "NVL(PA3.OSHAFACTOR,'Nothing') AS SMS3, "
                StrSql = StrSql + "NVL(PA4.OSHAFACTOR,'Nothing') AS SMS4, "
                StrSql = StrSql + "NVL(PA5.OSHAFACTOR,'Nothing') AS SMS5, "
                StrSql = StrSql + "NVL(PA6.OSHAFACTOR,'Nothing') AS SMS6, "
                StrSql = StrSql + "NVL(PA7.OSHAFACTOR,'Nothing') AS SMS7, "
                StrSql = StrSql + "NVL(PA8.OSHAFACTOR,'Nothing') AS SMS8, "
                StrSql = StrSql + "NVL(PA9.OSHAFACTOR,'Nothing') AS SMS9, "
                StrSql = StrSql + "NVL(PA10.OSHAFACTOR,'Nothing') AS SMS10, "
                StrSql = StrSql + "PI.OSH1 AS SMP1, "
                StrSql = StrSql + "PI.OSH2 AS SMP2, "
                StrSql = StrSql + "PI.OSH3 AS SMP3, "
                StrSql = StrSql + "PI.OSH4 AS SMP4, "
                StrSql = StrSql + "PI.OSH5 AS SMP5, "
                StrSql = StrSql + "PI.OSH6 AS SMP6, "
                StrSql = StrSql + "PI.OSH7 AS SMP7, "
                StrSql = StrSql + "PI.OSH8 AS SMP8, "
                StrSql = StrSql + "PI.OSH9 AS SMP9, "
                StrSql = StrSql + "PI.OSH10 AS SMP10, "
                StrSql = StrSql + "NVL(PA1.POSTCONSUMER,0) AS PCRECS1, "
                StrSql = StrSql + "NVL(PA2.POSTCONSUMER,0) AS PCRECS2, "
                StrSql = StrSql + "NVL(PA3.POSTCONSUMER,0) AS PCRECS3, "
                StrSql = StrSql + "NVL(PA4.POSTCONSUMER,0) AS PCRECS4, "
                StrSql = StrSql + "NVL(PA5.POSTCONSUMER,0) AS PCRECS5, "
                StrSql = StrSql + "NVL(PA6.POSTCONSUMER,0) AS PCRECS6, "
                StrSql = StrSql + "NVL(PA7.POSTCONSUMER,0) AS PCRECS7, "
                StrSql = StrSql + "NVL(PA8.POSTCONSUMER,0) AS PCRECS8, "
                StrSql = StrSql + "NVL(PA9.POSTCONSUMER,0) AS PCRECS9, "
                StrSql = StrSql + "NVL(PA10.POSTCONSUMER,0) AS PCRECS10, "
                StrSql = StrSql + "PI.POC1 AS PCRECP1, "
                StrSql = StrSql + "PI.POC2 AS PCRECP2, "
                StrSql = StrSql + "PI.POC3 AS PCRECP3, "
                StrSql = StrSql + "PI.POC4 AS PCRECP4, "
                StrSql = StrSql + "PI.POC5 AS PCRECP5, "
                StrSql = StrSql + "PI.POC6 AS PCRECP6, "
                StrSql = StrSql + "PI.POC7 AS PCRECP7, "
                StrSql = StrSql + "PI.POC8 AS PCRECP8, "
                StrSql = StrSql + "PI.POC9 AS PCRECP9, "
                StrSql = StrSql + "PI.POC10 AS PCRECP10, "
                StrSql = StrSql + "NVL(PA1.SHIP,0)*PREF.CONVTHICK3 AS SS1, "
                StrSql = StrSql + "NVL(PA2.SHIP,0)*PREF.CONVTHICK3 AS SS2, "
                StrSql = StrSql + "NVL(PA3.SHIP,0)*PREF.CONVTHICK3 AS SS3, "
                StrSql = StrSql + "NVL(PA4.SHIP,0)*PREF.CONVTHICK3 AS SS4, "
                StrSql = StrSql + "NVL(PA5.SHIP,0)*PREF.CONVTHICK3 AS SS5, "
                StrSql = StrSql + "NVL(PA6.SHIP,0)*PREF.CONVTHICK3 AS SS6, "
                StrSql = StrSql + "NVL(PA7.SHIP,0)*PREF.CONVTHICK3 AS SS7, "
                StrSql = StrSql + "NVL(PA8.SHIP,0)*PREF.CONVTHICK3 AS SS8, "
                StrSql = StrSql + "NVL(PA9.SHIP,0)*PREF.CONVTHICK3 AS SS9, "
                StrSql = StrSql + "NVL(PA10.SHIP,0)*PREF.CONVTHICK3 AS SS10, "
                StrSql = StrSql + "PI.SD1*PREF.CONVTHICK3 AS SP1, "
                StrSql = StrSql + "PI.SD2*PREF.CONVTHICK3 AS SP2, "
                StrSql = StrSql + "PI.SD3*PREF.CONVTHICK3 AS SP3, "
                StrSql = StrSql + "PI.SD4*PREF.CONVTHICK3 AS SP4, "
                StrSql = StrSql + "PI.SD5*PREF.CONVTHICK3 AS SP5, "
                StrSql = StrSql + "PI.SD6*PREF.CONVTHICK3 AS SP6, "
                StrSql = StrSql + "PI.SD7*PREF.CONVTHICK3 AS SP7, "
                StrSql = StrSql + "PI.SD8*PREF.CONVTHICK3 AS SP8, "
                StrSql = StrSql + "PI.SD9*PREF.CONVTHICK3 AS SP9, "
                StrSql = StrSql + "PI.SD10*PREF.CONVTHICK3 AS SP10, "
                StrSql = StrSql + "PI.D1, "
                StrSql = StrSql + "PI.D2, "
                StrSql = StrSql + "PI.D3, "
                StrSql = StrSql + "PI.D4, "
                StrSql = StrSql + "PI.D5, "
                StrSql = StrSql + "PI.D6, "
                StrSql = StrSql + "PI.D7, "
                StrSql = StrSql + "PI.D8, "
                StrSql = StrSql + "PI.D9, "
                StrSql = StrSql + "PI.D10, "
                StrSql = StrSql + "TOTAL.TOTOLDPALLETWT*PREF.CONVWT AS WEIGHTTOT, "
                StrSql = StrSql + "TOTAL.TOTALOLDPALLETERGY/PREF.CONVWT AS ENERGYTOT, "
                StrSql = StrSql + "TOTAL.TOTPALLETOLDCO2 CO2TOT, "
                'WATER Started
                StrSql = StrSql + "(TOTAL.TOTPALLETOLDWATER*PREF.CONVGALLON/PREF.CONVWT) WATERTOT, "
                'WATer End
                StrSql = StrSql + "TOTAL.TOTALOLDPALLETRECOVERY AS RECOVTOT, "
                StrSql = StrSql + "TOTAL.TOTALOLDPALLETOSHA, "
                StrSql = StrSql + "TOTAL.TOTALOLDPALLETPOC PCRECTOT, "
                StrSql = StrSql + "TOTAL.TOTALOLDPALLETAVGSHIPPING*PREF.CONVTHICK3 SHIPTOT, "
                StrSql = StrSql + "TOTAL.TOTALOLDPALLETSHIPPING SHIPOLDTOT, "
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
                StrSql = StrSql + "FROM PALLETIN PI "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=PI.CASEID "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PA1 "
                StrSql = StrSql + "ON PA1.INVENTORYTYPE=PI.INVENTORYTYPE "
                StrSql = StrSql + "AND PA1.EFFDATE = PI.EFFDATE "
                StrSql = StrSql + "AND PA1.PALLETID=PI.M1 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PA2 "
                StrSql = StrSql + "ON PA2.INVENTORYTYPE=PI.INVENTORYTYPE "
                StrSql = StrSql + "AND PA2.EFFDATE = PI.EFFDATE "
                StrSql = StrSql + "AND PA2.PALLETID=PI.M2 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PA3 "
                StrSql = StrSql + "ON PA3.INVENTORYTYPE=PI.INVENTORYTYPE "
                StrSql = StrSql + "AND PA3.EFFDATE = PI.EFFDATE "
                StrSql = StrSql + "AND PA3.PALLETID=PI.M3 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PA4 "
                StrSql = StrSql + "ON PA4.INVENTORYTYPE=PI.INVENTORYTYPE "
                StrSql = StrSql + "AND PA4.EFFDATE = PI.EFFDATE "
                StrSql = StrSql + "AND PA4.PALLETID=PI.M4 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PA5 "
                StrSql = StrSql + "ON PA5.INVENTORYTYPE=PI.INVENTORYTYPE "
                StrSql = StrSql + "AND PA5.EFFDATE = PI.EFFDATE "
                StrSql = StrSql + "AND PA5.PALLETID=PI.M5 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PA6 "
                StrSql = StrSql + "ON PA6.INVENTORYTYPE=PI.INVENTORYTYPE "
                StrSql = StrSql + "AND PA6.EFFDATE = PI.EFFDATE "
                StrSql = StrSql + "AND PA6.PALLETID=PI.M6 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PA7 "
                StrSql = StrSql + "ON PA7.INVENTORYTYPE=PI.INVENTORYTYPE "
                StrSql = StrSql + "AND PA7.EFFDATE = PI.EFFDATE "
                StrSql = StrSql + "AND PA7.PALLETID=PI.M7 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PA8 "
                StrSql = StrSql + "ON PA8.INVENTORYTYPE=PI.INVENTORYTYPE "
                StrSql = StrSql + "AND PA8.EFFDATE = PI.EFFDATE "
                StrSql = StrSql + "AND PA8.PALLETID=PI.M8 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PA9 "
                StrSql = StrSql + "ON PA9.INVENTORYTYPE=PI.INVENTORYTYPE "
                StrSql = StrSql + "AND PA9.EFFDATE = PI.EFFDATE "
                StrSql = StrSql + "AND PA9.PALLETID=PI.M9 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PA10 "
                StrSql = StrSql + "ON PA10.INVENTORYTYPE=PI.INVENTORYTYPE "
                StrSql = StrSql + "AND PA10.EFFDATE = PI.EFFDATE "
                StrSql = StrSql + "AND PA10.PALLETID=PI.M10 "
                StrSql = StrSql + "INNER JOIN PALLETENERGYPREF "
                StrSql = StrSql + "ON PALLETENERGYPREF.CASEID=PI.CASEID "
                StrSql = StrSql + "LEFT JOIN SUSTAIN.HEALTHANDSAFETY HS1 "
                StrSql = StrSql + "ON HS1.TYPE=PI.OSH1 "
                StrSql = StrSql + "LEFT JOIN SUSTAIN.HEALTHANDSAFETY HS2 "
                StrSql = StrSql + "ON HS2.TYPE=PI.OSH2 "
                StrSql = StrSql + "LEFT JOIN SUSTAIN.HEALTHANDSAFETY HS3 "
                StrSql = StrSql + "ON HS3.TYPE=PI.OSH3 "
                StrSql = StrSql + "LEFT JOIN SUSTAIN.HEALTHANDSAFETY HS4 "
                StrSql = StrSql + "ON HS4.TYPE=PI.OSH4 "
                StrSql = StrSql + "LEFT JOIN SUSTAIN.HEALTHANDSAFETY HS5 "
                StrSql = StrSql + "ON HS5.TYPE=PI.OSH5 "
                StrSql = StrSql + "LEFT JOIN SUSTAIN.HEALTHANDSAFETY HS6 "
                StrSql = StrSql + "ON HS6.TYPE=PI.OSH6 "
                StrSql = StrSql + "LEFT JOIN SUSTAIN.HEALTHANDSAFETY HS7 "
                StrSql = StrSql + "ON HS7.TYPE=PI.OSH7 "
                StrSql = StrSql + "LEFT JOIN SUSTAIN.HEALTHANDSAFETY HS8 "
                StrSql = StrSql + "ON HS8.TYPE=PI.OSH8 "
                StrSql = StrSql + "LEFT JOIN SUSTAIN.HEALTHANDSAFETY HS9 "
                StrSql = StrSql + "ON HS9.TYPE=PI.OSH9 "
                StrSql = StrSql + "LEFT JOIN SUSTAIN.HEALTHANDSAFETY HS10 "
                StrSql = StrSql + "ON HS10.TYPE=PI.OSH10 "
                StrSql = StrSql + "INNER JOIN TOTAL ON "
                StrSql = StrSql + "TOTAL.CASEID=PI.CASEID "
                'Water Started
                StrSql = StrSql + "INNER JOIN PALLETWATERPREF PATWATER "
                StrSql = StrSql + "ON PATWATER.CASEID=PI.CASEID "
                'Water End
                StrSql = StrSql + "WHERE PI.CASEID = " + CaseId.ToString() + " ORDER BY PI.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetPalletInDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPlantConfigDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PC.CASEID,  "
                StrSql = StrSql + "PC.M1 AS DA1, "
                StrSql = StrSql + "PC.M2 AS DA2, "
                StrSql = StrSql + "PC.M3 AS DA3, "
                StrSql = StrSql + "PC.M4 AS DA4, "
                StrSql = StrSql + "PC.M5 AS DA5, "
                StrSql = StrSql + "PC.M6 AS DA6, "
                StrSql = StrSql + "PC.M7 AS DA7, "
                StrSql = StrSql + "PC.M8 AS DA8, "
                StrSql = StrSql + "PC.M9 AS DA9, "
                StrSql = StrSql + "PC.M10 AS DA10, "
                StrSql = StrSql + "PC.T1 AS DB1, "
                StrSql = StrSql + "PC.T2 AS DB2, "
                StrSql = StrSql + "PC.T3 AS DB3, "
                StrSql = StrSql + "PC.T4 AS DB4, "
                StrSql = StrSql + "PC.T5 AS DB5, "
                StrSql = StrSql + "PC.T6 AS DB6, "
                StrSql = StrSql + "PC.T7 AS DB7, "
                StrSql = StrSql + "PC.T8 AS DB8, "
                StrSql = StrSql + "PC.T9 AS DB9, "
                StrSql = StrSql + "PC.T10 AS DB10, "
                StrSql = StrSql + "PC.S1 AS DC1, "
                StrSql = StrSql + "PC.S2 AS DC2, "
                StrSql = StrSql + "PC.S3 AS DC3, "
                StrSql = StrSql + "PC.S4 AS DC4, "
                StrSql = StrSql + "PC.S5 AS DC5, "
                StrSql = StrSql + "PC.S6 AS DC6, "
                StrSql = StrSql + "PC.S7 AS DC7, "
                StrSql = StrSql + "PC.S8 AS DC8, "
                StrSql = StrSql + "PC.S9 AS DC9, "
                StrSql = StrSql + "PC.S10 AS DC10, "
                StrSql = StrSql + "PC.Y1 AS DD1, "
                StrSql = StrSql + "PC.Y2 AS DD2, "
                StrSql = StrSql + "PC.Y3 AS DD3, "
                StrSql = StrSql + "PC.Y4 AS DD4, "
                StrSql = StrSql + "PC.Y5 AS DD5, "
                StrSql = StrSql + "PC.Y6 AS DD6, "
                StrSql = StrSql + "PC.Y7 AS DD7, "
                StrSql = StrSql + "PC.Y8 AS DD8, "
                StrSql = StrSql + "PC.Y9 AS DD9, "
                StrSql = StrSql + "PC.Y10 AS DD10, "
                StrSql = StrSql + "PC.D1 AS DE1, "
                StrSql = StrSql + "PC.D2 AS DE2, "
                StrSql = StrSql + "PC.D3 AS DE3, "
                StrSql = StrSql + "PC.D4 AS DE4, "
                StrSql = StrSql + "PC.D5 AS DE5, "
                StrSql = StrSql + "PC.D6 AS DE6, "
                StrSql = StrSql + "PC.D7 AS DE7, "
                StrSql = StrSql + "PC.D8 AS DE8, "
                StrSql = StrSql + "PC.D9 AS DE9, "
                StrSql = StrSql + "PC.D10 AS DE10, "
                StrSql = StrSql + "PC.Z1 AS DF1, "
                StrSql = StrSql + "PC.Z2 AS DF2, "
                StrSql = StrSql + "PC.Z3 AS DF3, "
                StrSql = StrSql + "PC.Z4 AS DF4, "
                StrSql = StrSql + "PC.Z5 AS DF5, "
                StrSql = StrSql + "PC.Z6 AS DF6, "
                StrSql = StrSql + "PC.Z7 AS DF7, "
                StrSql = StrSql + "PC.Z8 AS DF8, "
                StrSql = StrSql + "PC.Z9 AS DF9, "
                StrSql = StrSql + "PC.Z10 AS DF10, "
                StrSql = StrSql + "PC.B1 AS DG1, "
                StrSql = StrSql + "PC.B2 AS DG2, "
                StrSql = StrSql + "PC.B3 AS DG3, "
                StrSql = StrSql + "PC.B4 AS DG4, "
                StrSql = StrSql + "PC.B5 AS DG5, "
                StrSql = StrSql + "PC.B6 AS DG6, "
                StrSql = StrSql + "PC.B7 AS DG7, "
                StrSql = StrSql + "PC.B8 AS DG8, "
                StrSql = StrSql + "PC.B9 AS DG9, "
                StrSql = StrSql + "PC.B10 AS DG10, "
                StrSql = StrSql + "PC.R1 AS DH1, "
                StrSql = StrSql + "PC.R2 AS DH2, "
                StrSql = StrSql + "PC.R3 AS DH3, "
                StrSql = StrSql + "PC.R4 AS DH4, "
                StrSql = StrSql + "PC.R5 AS DH5, "
                StrSql = StrSql + "PC.R6 AS DH6, "
                StrSql = StrSql + "PC.R7 AS DH7, "
                StrSql = StrSql + "PC.R8 AS DH8, "
                StrSql = StrSql + "PC.R9 AS DH9, "
                StrSql = StrSql + "PC.R10 AS DH10, "
                StrSql = StrSql + "PC.K1 AS DI1, "
                StrSql = StrSql + "PC.K2 AS DI2, "
                StrSql = StrSql + "PC.K3 AS DI3, "
                StrSql = StrSql + "PC.K4 AS DI4, "
                StrSql = StrSql + "PC.K5 AS DI5, "
                StrSql = StrSql + "PC.K6 AS DI6, "
                StrSql = StrSql + "PC.K7 AS DI7, "
                StrSql = StrSql + "PC.K8 AS DI8, "
                StrSql = StrSql + "PC.K9 AS DI9, "
                StrSql = StrSql + "PC.K10 AS DI10, "
                StrSql = StrSql + "PC.P1 AS DJ1, "
                StrSql = StrSql + "PC.P2 AS DJ2, "
                StrSql = StrSql + "PC.P3 AS DJ3, "
                StrSql = StrSql + "PC.P4 AS DJ4, "
                StrSql = StrSql + "PC.P5 AS DJ5, "
                StrSql = StrSql + "PC.P6 AS DJ6, "
                StrSql = StrSql + "PC.P7 AS DJ7, "
                StrSql = StrSql + "PC.P8 AS DJ8, "
                StrSql = StrSql + "PC.P9 AS DJ9, "
                StrSql = StrSql + "PC.P10 AS DJ10 "
                StrSql = StrSql + "FROM PLANTCONFIG PC "
                StrSql = StrSql + "WHERE PC.CASEID = " + CaseId.ToString() + " ORDER BY PC.CASEID"
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetPlantConfigDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetEffiencyDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PC.CASEID,  "
                StrSql = StrSql + "P1.PROCDE1 || ' '||P1.PROCDE2 AS DEP1, "
                StrSql = StrSql + "P2.PROCDE1 || ' '||P2.PROCDE2 AS DEP2, "
                StrSql = StrSql + "P3.PROCDE1 || ' '||P3.PROCDE2 AS DEP3, "
                StrSql = StrSql + "P4.PROCDE1 || ' '||P4.PROCDE2 AS DEP4, "
                StrSql = StrSql + "P5.PROCDE1 || ' '||P5.PROCDE2 AS DEP5, "
                StrSql = StrSql + "P6.PROCDE1 || ' '||P6.PROCDE2 AS DEP6, "
                StrSql = StrSql + "P7.PROCDE1 || ' '||P7.PROCDE2 AS DEP7, "
                StrSql = StrSql + "P8.PROCDE1 || ' '||P8.PROCDE2 AS DEP8, "
                StrSql = StrSql + "P9.PROCDE1 || ' '||P9.PROCDE2 AS DEP9, "
                StrSql = StrSql + "P10.PROCDE1 || ' '||P10.PROCDE2 AS DEP10, "
                StrSql = StrSql + "MAT1.MATDE1 || ' '||MAT1.MATDE2 AS MAT1, "
                StrSql = StrSql + "MAT2.MATDE1 || ' '||MAT2.MATDE2 AS MAT2, "
                StrSql = StrSql + "MAT3.MATDE1 || ' '||MAT3.MATDE2 AS MAT3, "
                StrSql = StrSql + "MAT4.MATDE1 || ' '||MAT4.MATDE2 AS MAT4, "
                StrSql = StrSql + "MAT5.MATDE1 || ' '||MAT5.MATDE2 AS MAT5, "
                StrSql = StrSql + "MAT6.MATDE1 || ' '||MAT6.MATDE2 AS MAT6, "
                StrSql = StrSql + "MAT7.MATDE1 || ' '||MAT7.MATDE2 AS MAT7, "
                StrSql = StrSql + "MAT8.MATDE1 || ' '||MAT8.MATDE2 AS MAT8, "
                StrSql = StrSql + "MAT9.MATDE1 || ' '||MAT9.MATDE2 AS MAT9, "
                StrSql = StrSql + "MAT10.MATDE1 || ' '||MAT10.MATDE2 AS MAT10, "
                StrSql = StrSql + "MEFF.T1 AS A1, "
                StrSql = StrSql + "MEFF.T2 AS A2, "
                StrSql = StrSql + "MEFF.T3 AS A3, "
                StrSql = StrSql + "MEFF.T4 AS A4, "
                StrSql = StrSql + "MEFF.T5 AS A5, "
                StrSql = StrSql + "MEFF.T6 AS A6, "
                StrSql = StrSql + "MEFF.T7 AS A7, "
                StrSql = StrSql + "MEFF.T8 AS A8, "
                StrSql = StrSql + "MEFF.T9 AS A9, "
                StrSql = StrSql + "MEFF.T10 AS A10, "
                StrSql = StrSql + "MEFF.S1 AS B1, "
                StrSql = StrSql + "MEFF.S2 AS B2, "
                StrSql = StrSql + "MEFF.S3 AS B3, "
                StrSql = StrSql + "MEFF.S4 AS B4, "
                StrSql = StrSql + "MEFF.S5 AS B5, "
                StrSql = StrSql + "MEFF.S6 AS B6, "
                StrSql = StrSql + "MEFF.S7 AS B7, "
                StrSql = StrSql + "MEFF.S8 AS B8, "
                StrSql = StrSql + "MEFF.S9 AS B9, "
                StrSql = StrSql + "MEFF.S10 AS B10, "
                StrSql = StrSql + "MEFF.Y1 AS C1, "
                StrSql = StrSql + "MEFF.Y2 AS C2, "
                StrSql = StrSql + "MEFF.Y3 AS C3, "
                StrSql = StrSql + "MEFF.Y4 AS C4, "
                StrSql = StrSql + "MEFF.Y5 AS C5, "
                StrSql = StrSql + "MEFF.Y6 AS C6, "
                StrSql = StrSql + "MEFF.Y7 AS C7, "
                StrSql = StrSql + "MEFF.Y8 AS C8, "
                StrSql = StrSql + "MEFF.Y9 AS C9, "
                StrSql = StrSql + "MEFF.Y10 AS C10, "
                StrSql = StrSql + "MEFF.D1 AS D1, "
                StrSql = StrSql + "MEFF.D2 AS D2, "
                StrSql = StrSql + "MEFF.D3 AS D3, "
                StrSql = StrSql + "MEFF.D4 AS D4, "
                StrSql = StrSql + "MEFF.D5 AS D5, "
                StrSql = StrSql + "MEFF.D6 AS D6, "
                StrSql = StrSql + "MEFF.D7 AS D7, "
                StrSql = StrSql + "MEFF.D8 AS D8, "
                StrSql = StrSql + "MEFF.D9 AS D9, "
                StrSql = StrSql + "MEFF.D10 AS D10, "
                StrSql = StrSql + "MEFF.E1 AS E1, "
                StrSql = StrSql + "MEFF.E2 AS E2, "
                StrSql = StrSql + "MEFF.E3 AS E3, "
                StrSql = StrSql + "MEFF.E4 AS E4, "
                StrSql = StrSql + "MEFF.E5 AS E5, "
                StrSql = StrSql + "MEFF.E6 AS E6, "
                StrSql = StrSql + "MEFF.E7 AS E7, "
                StrSql = StrSql + "MEFF.E8 AS E8, "
                StrSql = StrSql + "MEFF.E9 AS E9, "
                StrSql = StrSql + "MEFF.E10 AS E10, "
                StrSql = StrSql + "MEFF.Z1 AS F1, "
                StrSql = StrSql + "MEFF.Z2 AS F2, "
                StrSql = StrSql + "MEFF.Z3 AS F3, "
                StrSql = StrSql + "MEFF.Z4 AS F4, "
                StrSql = StrSql + "MEFF.Z5 AS F5, "
                StrSql = StrSql + "MEFF.Z6 AS F6, "
                StrSql = StrSql + "MEFF.Z7 AS F7, "
                StrSql = StrSql + "MEFF.Z8 AS F8, "
                StrSql = StrSql + "MEFF.Z9 AS F9, "
                StrSql = StrSql + "MEFF.Z10 AS F10, "
                StrSql = StrSql + "MEFF.B1 AS G1, "
                StrSql = StrSql + "MEFF.B2 AS G2, "
                StrSql = StrSql + "MEFF.B3 AS G3, "
                StrSql = StrSql + "MEFF.B4 AS G4, "
                StrSql = StrSql + "MEFF.B5 AS G5, "
                StrSql = StrSql + "MEFF.B6 AS G6, "
                StrSql = StrSql + "MEFF.B7 AS G7, "
                StrSql = StrSql + "MEFF.B8 AS G8, "
                StrSql = StrSql + "MEFF.B9 AS G9, "
                StrSql = StrSql + "MEFF.B10 AS G10, "
                StrSql = StrSql + "MEFF.R1 AS H1, "
                StrSql = StrSql + "MEFF.R2 AS H2, "
                StrSql = StrSql + "MEFF.R3 AS H3, "
                StrSql = StrSql + "MEFF.R4 AS H4, "
                StrSql = StrSql + "MEFF.R5 AS H5, "
                StrSql = StrSql + "MEFF.R6 AS H6, "
                StrSql = StrSql + "MEFF.R7 AS H7, "
                StrSql = StrSql + "MEFF.R8 AS H8, "
                StrSql = StrSql + "MEFF.R9 AS H9, "
                StrSql = StrSql + "MEFF.R10 AS H10, "
                StrSql = StrSql + "MEFF.K1 AS I1, "
                StrSql = StrSql + "MEFF.K2 AS I2, "
                StrSql = StrSql + "MEFF.K3 AS I3, "
                StrSql = StrSql + "MEFF.K4 AS I4, "
                StrSql = StrSql + "MEFF.K5 AS I5, "
                StrSql = StrSql + "MEFF.K6 AS I6, "
                StrSql = StrSql + "MEFF.K7 AS I7, "
                StrSql = StrSql + "MEFF.K8 AS I8, "
                StrSql = StrSql + "MEFF.K9 AS I9, "
                StrSql = StrSql + "MEFF.K10 AS I10, "
                StrSql = StrSql + "MEFF.P1 AS J1, "
                StrSql = StrSql + "MEFF.P2 AS J2, "
                StrSql = StrSql + "MEFF.P3 AS J3, "
                StrSql = StrSql + "MEFF.P4 AS J4, "
                StrSql = StrSql + "MEFF.P5 AS J5, "
                StrSql = StrSql + "MEFF.P6 AS J6, "
                StrSql = StrSql + "MEFF.P7 AS J7, "
                StrSql = StrSql + "MEFF.P8 AS J8, "
                StrSql = StrSql + "MEFF.P9 AS J9, "
                StrSql = StrSql + "MEFF.P10 AS J10 "
                StrSql = StrSql + "FROM PLANTCONFIG PC "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS P1 "
                StrSql = StrSql + "ON P1.PROCID=PC.M1 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS P2 "
                StrSql = StrSql + "ON P2.PROCID=PC.M2 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS P3 "
                StrSql = StrSql + "ON P3.PROCID=PC.M3 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS P4 "
                StrSql = StrSql + "ON P4.PROCID=PC.M4 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS P5 "
                StrSql = StrSql + "ON P5.PROCID=PC.M5 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS P6 "
                StrSql = StrSql + "ON P6.PROCID=PC.M6 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS P7 "
                StrSql = StrSql + "ON P7.PROCID=PC.M7 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS P8 "
                StrSql = StrSql + "ON P8.PROCID=PC.M8 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS P9 "
                StrSql = StrSql + "ON P9.PROCID=PC.M9 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS P10 "
                StrSql = StrSql + "ON P10.PROCID=PC.M10 "
                StrSql = StrSql + "INNER JOIN MATERIALINPUT MI "
                StrSql = StrSql + "ON MI.CASEID=PC.CASEID "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT1 "
                StrSql = StrSql + "ON MAT1.MATID=MI.M1 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT2 "
                StrSql = StrSql + "ON MAT2.MATID=MI.M2 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT3 "
                StrSql = StrSql + "ON MAT3.MATID=MI.M3 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT4 "
                StrSql = StrSql + "ON MAT4.MATID=MI.M4 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT5 "
                StrSql = StrSql + "ON MAT5.MATID=MI.M5 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT6 "
                StrSql = StrSql + "ON MAT6.MATID=MI.M6 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT7 "
                StrSql = StrSql + "ON MAT7.MATID=MI.M7 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT8 "
                StrSql = StrSql + "ON MAT8.MATID=MI.M8 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT9 "
                StrSql = StrSql + "ON MAT9.MATID=MI.M9 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT10 "
                StrSql = StrSql + "ON MAT10.MATID=MI.M10 "
                StrSql = StrSql + "INNER JOIN MATERIALEFF MEFF "
                StrSql = StrSql + "ON MEFF.CASEID=PC.CASEID "
                StrSql = StrSql + "WHERE PC.CASEID=" + CaseId.ToString() + " ORDER BY PC.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetEffiencyDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetEquipmentInDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT EQT.CASEID,  "
                StrSql = StrSql + "EQT.M1, "
                StrSql = StrSql + "EQT.M2, "
                StrSql = StrSql + "EQT.M3, "
                StrSql = StrSql + "EQT.M4, "
                StrSql = StrSql + "EQT.M5, "
                StrSql = StrSql + "EQT.M6, "
                StrSql = StrSql + "EQT.M7, "
                StrSql = StrSql + "EQT.M8, "
                StrSql = StrSql + "EQT.M9, "
                StrSql = StrSql + "EQT.M10, "
                StrSql = StrSql + "EQT.M11, "
                StrSql = StrSql + "EQT.M12, "
                StrSql = StrSql + "EQT.M13, "
                StrSql = StrSql + "EQT.M14, "
                StrSql = StrSql + "EQT.M15, "
                StrSql = StrSql + "EQT.M16, "
                StrSql = StrSql + "EQT.M17, "
                StrSql = StrSql + "EQT.M18, "
                StrSql = StrSql + "EQT.M19, "
                StrSql = StrSql + "EQT.M20, "
                StrSql = StrSql + "EQT.M21, "
                StrSql = StrSql + "EQT.M22, "
                StrSql = StrSql + "EQT.M23, "
                StrSql = StrSql + "EQT.M24, "
                StrSql = StrSql + "EQT.M25, "
                StrSql = StrSql + "EQT.M26, "
                StrSql = StrSql + "EQT.M27, "
                StrSql = StrSql + "EQT.M28, "
                StrSql = StrSql + "EQT.M29, "
                StrSql = StrSql + "EQT.M30, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ1.AREATYPE )AT1, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ2.AREATYPE )AT2, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ3.AREATYPE )AT3, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ4.AREATYPE )AT4, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ5.AREATYPE )AT5, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ6.AREATYPE )AT6, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ7.AREATYPE )AT7, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ8.AREATYPE )AT8, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ9.AREATYPE )AT9, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ10.AREATYPE )AT10, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ11.AREATYPE )AT11, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ12.AREATYPE )AT12, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ13.AREATYPE )AT13, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ14.AREATYPE )AT14, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ15.AREATYPE )AT15, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ16.AREATYPE )AT16, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ17.AREATYPE )AT17, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ18.AREATYPE )AT18, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ19.AREATYPE )AT19, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ20.AREATYPE )AT20, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ21.AREATYPE )AT21, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ22.AREATYPE )AT22, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ23.AREATYPE )AT23, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ24.AREATYPE )AT24, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ25.AREATYPE )AT25, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ26.AREATYPE )AT26, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ27.AREATYPE )AT27, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ28.AREATYPE )AT28, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ29.AREATYPE )AT29, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQ30.AREATYPE )AT30, "
                StrSql = StrSql + "(EQ1.AREA*PREF.CONVAREA2) AS PAS1, "
                StrSql = StrSql + "(EQ2.AREA*PREF.CONVAREA2) AS PAS2, "
                StrSql = StrSql + "(EQ3.AREA*PREF.CONVAREA2) AS PAS3, "
                StrSql = StrSql + "(EQ4.AREA*PREF.CONVAREA2) AS PAS4, "
                StrSql = StrSql + "(EQ5.AREA*PREF.CONVAREA2) AS PAS5, "
                StrSql = StrSql + "(EQ6.AREA*PREF.CONVAREA2) AS PAS6, "
                StrSql = StrSql + "(EQ7.AREA*PREF.CONVAREA2) AS PAS7, "
                StrSql = StrSql + "(EQ8.AREA*PREF.CONVAREA2) AS PAS8, "
                StrSql = StrSql + "(EQ9.AREA*PREF.CONVAREA2) AS PAS9, "
                StrSql = StrSql + "(EQ10.AREA*PREF.CONVAREA2) AS PAS10, "
                StrSql = StrSql + "(EQ11.AREA*PREF.CONVAREA2) AS PAS11, "
                StrSql = StrSql + "(EQ12.AREA*PREF.CONVAREA2) AS PAS12, "
                StrSql = StrSql + "(EQ13.AREA*PREF.CONVAREA2) AS PAS13, "
                StrSql = StrSql + "(EQ14.AREA*PREF.CONVAREA2) AS PAS14, "
                StrSql = StrSql + "(EQ15.AREA*PREF.CONVAREA2) AS PAS15, "
                StrSql = StrSql + "(EQ16.AREA*PREF.CONVAREA2) AS PAS16, "
                StrSql = StrSql + "(EQ17.AREA*PREF.CONVAREA2) AS PAS17, "
                StrSql = StrSql + "(EQ18.AREA*PREF.CONVAREA2) AS PAS18, "
                StrSql = StrSql + "(EQ19.AREA*PREF.CONVAREA2) AS PAS19, "
                StrSql = StrSql + "(EQ20.AREA*PREF.CONVAREA2) AS PAS20, "
                StrSql = StrSql + "(EQ21.AREA*PREF.CONVAREA2) AS PAS21, "
                StrSql = StrSql + "(EQ22.AREA*PREF.CONVAREA2) AS PAS22, "
                StrSql = StrSql + "(EQ23.AREA*PREF.CONVAREA2) AS PAS23, "
                StrSql = StrSql + "(EQ24.AREA*PREF.CONVAREA2) AS PAS24, "
                StrSql = StrSql + "(EQ25.AREA*PREF.CONVAREA2) AS PAS25, "
                StrSql = StrSql + "(EQ26.AREA*PREF.CONVAREA2) AS PAS26, "
                StrSql = StrSql + "(EQ27.AREA*PREF.CONVAREA2) AS PAS27, "
                StrSql = StrSql + "(EQ28.AREA*PREF.CONVAREA2) AS PAS28, "
                StrSql = StrSql + "(EQ29.AREA*PREF.CONVAREA2) AS PAS29, "
                StrSql = StrSql + "(EQ30.AREA*PREF.CONVAREA2) AS PAS30, "
                StrSql = StrSql + "(EA.M1*PREF.CONVAREA2) AS PAP1, "
                StrSql = StrSql + "(EA.M2*PREF.CONVAREA2) AS PAP2, "
                StrSql = StrSql + "(EA.M3*PREF.CONVAREA2) AS PAP3, "
                StrSql = StrSql + "(EA.M4*PREF.CONVAREA2) AS PAP4, "
                StrSql = StrSql + "(EA.M5*PREF.CONVAREA2) AS PAP5, "
                StrSql = StrSql + "(EA.M6*PREF.CONVAREA2) AS PAP6, "
                StrSql = StrSql + "(EA.M7*PREF.CONVAREA2) AS PAP7, "
                StrSql = StrSql + "(EA.M8*PREF.CONVAREA2) AS PAP8, "
                StrSql = StrSql + "(EA.M9*PREF.CONVAREA2) AS PAP9, "
                StrSql = StrSql + "(EA.M10*PREF.CONVAREA2) AS PAP10, "
                StrSql = StrSql + "(EA.M11*PREF.CONVAREA2) AS PAP11, "
                StrSql = StrSql + "(EA.M12*PREF.CONVAREA2) AS PAP12, "
                StrSql = StrSql + "(EA.M13*PREF.CONVAREA2) AS PAP13, "
                StrSql = StrSql + "(EA.M14*PREF.CONVAREA2) AS PAP14, "
                StrSql = StrSql + "(EA.M15*PREF.CONVAREA2) AS PAP15, "
                StrSql = StrSql + "(EA.M16*PREF.CONVAREA2) AS PAP16, "
                StrSql = StrSql + "(EA.M17*PREF.CONVAREA2) AS PAP17, "
                StrSql = StrSql + "(EA.M18*PREF.CONVAREA2) AS PAP18, "
                StrSql = StrSql + "(EA.M19*PREF.CONVAREA2) AS PAP19, "
                StrSql = StrSql + "(EA.M20*PREF.CONVAREA2) AS PAP20, "
                StrSql = StrSql + "(EA.M21*PREF.CONVAREA2) AS PAP21, "
                StrSql = StrSql + "(EA.M22*PREF.CONVAREA2) AS PAP22, "
                StrSql = StrSql + "(EA.M23*PREF.CONVAREA2) AS PAP23, "
                StrSql = StrSql + "(EA.M24*PREF.CONVAREA2) AS PAP24, "
                StrSql = StrSql + "(EA.M25*PREF.CONVAREA2) AS PAP25, "
                StrSql = StrSql + "(EA.M26*PREF.CONVAREA2) AS PAP26, "
                StrSql = StrSql + "(EA.M27*PREF.CONVAREA2) AS PAP27, "
                StrSql = StrSql + "(EA.M28*PREF.CONVAREA2) AS PAP28, "
                StrSql = StrSql + "(EA.M29*PREF.CONVAREA2) AS PAP29, "
                StrSql = StrSql + "(EA.M30*PREF.CONVAREA2) AS PAP30, "
                StrSql = StrSql + "EQ1.INSTKW AS ECS1, "
                StrSql = StrSql + "EQ2.INSTKW AS ECS2, "
                StrSql = StrSql + "EQ3.INSTKW AS ECS3, "
                StrSql = StrSql + "EQ4.INSTKW AS ECS4, "
                StrSql = StrSql + "EQ5.INSTKW AS ECS5, "
                StrSql = StrSql + "EQ6.INSTKW AS ECS6, "
                StrSql = StrSql + "EQ7.INSTKW AS ECS7, "
                StrSql = StrSql + "EQ8.INSTKW AS ECS8, "
                StrSql = StrSql + "EQ9.INSTKW AS ECS9, "
                StrSql = StrSql + "EQ10.INSTKW AS ECS10, "
                StrSql = StrSql + "EQ11.INSTKW AS ECS11, "
                StrSql = StrSql + "EQ12.INSTKW AS ECS12, "
                StrSql = StrSql + "EQ13.INSTKW AS ECS13, "
                StrSql = StrSql + "EQ14.INSTKW AS ECS14, "
                StrSql = StrSql + "EQ15.INSTKW AS ECS15, "
                StrSql = StrSql + "EQ16.INSTKW AS ECS16, "
                StrSql = StrSql + "EQ17.INSTKW AS ECS17, "
                StrSql = StrSql + "EQ18.INSTKW AS ECS18, "
                StrSql = StrSql + "EQ19.INSTKW AS ECS19, "
                StrSql = StrSql + "EQ20.INSTKW AS ECS20, "
                StrSql = StrSql + "EQ21.INSTKW AS ECS21, "
                StrSql = StrSql + "EQ22.INSTKW AS ECS22, "
                StrSql = StrSql + "EQ23.INSTKW AS ECS23, "
                StrSql = StrSql + "EQ24.INSTKW AS ECS24, "
                StrSql = StrSql + "EQ25.INSTKW AS ECS25, "
                StrSql = StrSql + "EQ26.INSTKW AS ECS26, "
                StrSql = StrSql + "EQ27.INSTKW AS ECS27, "
                StrSql = StrSql + "EQ28.INSTKW AS ECS28, "
                StrSql = StrSql + "EQ29.INSTKW AS ECS29, "
                StrSql = StrSql + "EQ30.INSTKW AS ECS30, "
                StrSql = StrSql + "EQPREF.M1 ECP1, "
                StrSql = StrSql + "EQPREF.M2 ECP2, "
                StrSql = StrSql + "EQPREF.M3 ECP3, "
                StrSql = StrSql + "EQPREF.M4 ECP4, "
                StrSql = StrSql + "EQPREF.M5 ECP5, "
                StrSql = StrSql + "EQPREF.M6 ECP6, "
                StrSql = StrSql + "EQPREF.M7 ECP7, "
                StrSql = StrSql + "EQPREF.M8 ECP8, "
                StrSql = StrSql + "EQPREF.M9 ECP9, "
                StrSql = StrSql + "EQPREF.M10 ECP10, "
                StrSql = StrSql + "EQPREF.M11 ECP11, "
                StrSql = StrSql + "EQPREF.M12 ECP12, "
                StrSql = StrSql + "EQPREF.M13 ECP13, "
                StrSql = StrSql + "EQPREF.M14 ECP14, "
                StrSql = StrSql + "EQPREF.M15 ECP15, "
                StrSql = StrSql + "EQPREF.M16 ECP16, "
                StrSql = StrSql + "EQPREF.M17 ECP17, "
                StrSql = StrSql + "EQPREF.M18 ECP18, "
                StrSql = StrSql + "EQPREF.M19 ECP19, "
                StrSql = StrSql + "EQPREF.M20 ECP20, "
                StrSql = StrSql + "EQPREF.M21 ECP21, "
                StrSql = StrSql + "EQPREF.M22 ECP22, "
                StrSql = StrSql + "EQPREF.M23 ECP23, "
                StrSql = StrSql + "EQPREF.M24 ECP24, "
                StrSql = StrSql + "EQPREF.M25 ECP25, "
                StrSql = StrSql + "EQPREF.M26 ECP26, "
                StrSql = StrSql + "EQPREF.M27 ECP27, "
                StrSql = StrSql + "EQPREF.M28 ECP28, "
                StrSql = StrSql + "EQPREF.M29 ECP29, "
                StrSql = StrSql + "EQPREF.M30 ECP30, "
                StrSql = StrSql + "EQ1.NTGKW AS NGS1, "
                StrSql = StrSql + "EQ2.NTGKW AS NGS2, "
                StrSql = StrSql + "EQ3.NTGKW AS NGS3, "
                StrSql = StrSql + "EQ4.NTGKW AS NGS4, "
                StrSql = StrSql + "EQ5.NTGKW AS NGS5, "
                StrSql = StrSql + "EQ6.NTGKW AS NGS6, "
                StrSql = StrSql + "EQ7.NTGKW AS NGS7, "
                StrSql = StrSql + "EQ8.NTGKW AS NGS8, "
                StrSql = StrSql + "EQ9.NTGKW AS NGS9, "
                StrSql = StrSql + "EQ10.NTGKW AS NGS10, "
                StrSql = StrSql + "EQ11.NTGKW AS NGS11, "
                StrSql = StrSql + "EQ12.NTGKW AS NGS12, "
                StrSql = StrSql + "EQ13.NTGKW AS NGS13, "
                StrSql = StrSql + "EQ14.NTGKW AS NGS14, "
                StrSql = StrSql + "EQ15.NTGKW AS NGS15, "
                StrSql = StrSql + "EQ16.NTGKW AS NGS16, "
                StrSql = StrSql + "EQ17.NTGKW AS NGS17, "
                StrSql = StrSql + "EQ18.NTGKW AS NGS18, "
                StrSql = StrSql + "EQ19.NTGKW AS NGS19, "
                StrSql = StrSql + "EQ20.NTGKW AS NGS20, "
                StrSql = StrSql + "EQ21.NTGKW AS NGS21, "
                StrSql = StrSql + "EQ22.NTGKW AS NGS22, "
                StrSql = StrSql + "EQ23.NTGKW AS NGS23, "
                StrSql = StrSql + "EQ24.NTGKW AS NGS24, "
                StrSql = StrSql + "EQ25.NTGKW AS NGS25, "
                StrSql = StrSql + "EQ26.NTGKW AS NGS26, "
                StrSql = StrSql + "EQ27.NTGKW AS NGS27, "
                StrSql = StrSql + "EQ28.NTGKW AS NGS28, "
                StrSql = StrSql + "EQ29.NTGKW AS NGS29, "
                StrSql = StrSql + "EQ30.NTGKW AS NGS30, "
                StrSql = StrSql + "EQNPREF.M1 NGP1, "
                StrSql = StrSql + "EQNPREF.M2 NGP2, "
                StrSql = StrSql + "EQNPREF.M3 NGP3, "
                StrSql = StrSql + "EQNPREF.M4 NGP4, "
                StrSql = StrSql + "EQNPREF.M5 NGP5, "
                StrSql = StrSql + "EQNPREF.M6 NGP6, "
                StrSql = StrSql + "EQNPREF.M7 NGP7, "
                StrSql = StrSql + "EQNPREF.M8 NGP8, "
                StrSql = StrSql + "EQNPREF.M9 NGP9, "
                StrSql = StrSql + "EQNPREF.M10 NGP10, "
                StrSql = StrSql + "EQNPREF.M11 NGP11, "
                StrSql = StrSql + "EQNPREF.M12 NGP12, "
                StrSql = StrSql + "EQNPREF.M13 NGP13, "
                StrSql = StrSql + "EQNPREF.M14 NGP14, "
                StrSql = StrSql + "EQNPREF.M15 NGP15, "
                StrSql = StrSql + "EQNPREF.M16 NGP16, "
                StrSql = StrSql + "EQNPREF.M17 NGP17, "
                StrSql = StrSql + "EQNPREF.M18 NGP18, "
                StrSql = StrSql + "EQNPREF.M19 NGP19, "
                StrSql = StrSql + "EQNPREF.M20 NGP20, "
                StrSql = StrSql + "EQNPREF.M21 NGP21, "
                StrSql = StrSql + "EQNPREF.M22 NGP22, "
                StrSql = StrSql + "EQNPREF.M23 NGP23, "
                StrSql = StrSql + "EQNPREF.M24 NGP24, "
                StrSql = StrSql + "EQNPREF.M25 NGP25, "
                StrSql = StrSql + "EQNPREF.M26 NGP26, "
                StrSql = StrSql + "EQNPREF.M27 NGP27, "
                StrSql = StrSql + "EQNPREF.M28 NGP28, "
                StrSql = StrSql + "EQNPREF.M29 NGP29, "
                StrSql = StrSql + "EQNPREF.M30 NGP30, "
                'Water Started
                StrSql = StrSql + "(EQ1.WATERKW*PREF.CONVGALLON ) AS WATERS1, "
                StrSql = StrSql + "(EQ2.WATERKW*PREF.CONVGALLON ) AS WATERS2, "
                StrSql = StrSql + "(EQ3.WATERKW*PREF.CONVGALLON ) AS WATERS3, "
                StrSql = StrSql + "(EQ4.WATERKW*PREF.CONVGALLON ) AS WATERS4, "
                StrSql = StrSql + "(EQ5.WATERKW*PREF.CONVGALLON ) AS WATERS5, "
                StrSql = StrSql + "(EQ6.WATERKW*PREF.CONVGALLON ) AS WATERS6, "
                StrSql = StrSql + "(EQ7.WATERKW*PREF.CONVGALLON ) AS WATERS7, "
                StrSql = StrSql + "(EQ8.WATERKW*PREF.CONVGALLON ) AS WATERS8, "
                StrSql = StrSql + "(EQ9.WATERKW*PREF.CONVGALLON ) AS WATERS9, "
                StrSql = StrSql + "(EQ10.WATERKW*PREF.CONVGALLON ) AS WATERS10, "
                StrSql = StrSql + "(EQ11.WATERKW*PREF.CONVGALLON ) AS WATERS11, "
                StrSql = StrSql + "(EQ12.WATERKW*PREF.CONVGALLON ) AS WATERS12, "
                StrSql = StrSql + "(EQ13.WATERKW*PREF.CONVGALLON ) AS WATERS13, "
                StrSql = StrSql + "(EQ14.WATERKW*PREF.CONVGALLON ) AS WATERS14, "
                StrSql = StrSql + "(EQ15.WATERKW*PREF.CONVGALLON ) AS WATERS15, "
                StrSql = StrSql + "(EQ16.WATERKW*PREF.CONVGALLON ) AS WATERS16, "
                StrSql = StrSql + "(EQ17.WATERKW*PREF.CONVGALLON ) AS WATERS17, "
                StrSql = StrSql + "(EQ18.WATERKW*PREF.CONVGALLON ) AS WATERS18, "
                StrSql = StrSql + "(EQ19.WATERKW*PREF.CONVGALLON ) AS WATERS19, "
                StrSql = StrSql + "(EQ20.WATERKW*PREF.CONVGALLON ) AS WATERS20, "
                StrSql = StrSql + "(EQ21.WATERKW*PREF.CONVGALLON ) AS WATERS21, "
                StrSql = StrSql + "(EQ22.WATERKW*PREF.CONVGALLON ) AS WATERS22, "
                StrSql = StrSql + "(EQ23.WATERKW*PREF.CONVGALLON ) AS WATERS23, "
                StrSql = StrSql + "(EQ24.WATERKW*PREF.CONVGALLON ) AS WATERS24, "
                StrSql = StrSql + "(EQ25.WATERKW*PREF.CONVGALLON ) AS WATERS25, "
                StrSql = StrSql + "(EQ26.WATERKW*PREF.CONVGALLON ) AS WATERS26, "
                StrSql = StrSql + "(EQ27.WATERKW*PREF.CONVGALLON ) AS WATERS27, "
                StrSql = StrSql + "(EQ28.WATERKW*PREF.CONVGALLON ) AS WATERS28, "
                StrSql = StrSql + "(EQ29.WATERKW*PREF.CONVGALLON ) AS WATERS29, "
                StrSql = StrSql + "(EQ30.WATERKW*PREF.CONVGALLON ) AS WATERS30, "
                StrSql = StrSql + "(EQUIPWATERPREF.M1*PREF.CONVGALLON ) AS WATERP1, "
                StrSql = StrSql + "(EQUIPWATERPREF.M2*PREF.CONVGALLON ) AS WATERP2, "
                StrSql = StrSql + "(EQUIPWATERPREF.M3*PREF.CONVGALLON ) AS WATERP3, "
                StrSql = StrSql + "(EQUIPWATERPREF.M4*PREF.CONVGALLON ) AS WATERP4, "
                StrSql = StrSql + "(EQUIPWATERPREF.M5*PREF.CONVGALLON ) AS WATERP5, "
                StrSql = StrSql + "(EQUIPWATERPREF.M6*PREF.CONVGALLON ) AS WATERP6, "
                StrSql = StrSql + "(EQUIPWATERPREF.M7*PREF.CONVGALLON ) AS WATERP7, "
                StrSql = StrSql + "(EQUIPWATERPREF.M8*PREF.CONVGALLON ) AS WATERP8, "
                StrSql = StrSql + "(EQUIPWATERPREF.M9*PREF.CONVGALLON ) AS WATERP9, "
                StrSql = StrSql + "(EQUIPWATERPREF.M10*PREF.CONVGALLON ) AS WATERP10, "
                StrSql = StrSql + "(EQUIPWATERPREF.M11*PREF.CONVGALLON ) AS WATERP11, "
                StrSql = StrSql + "(EQUIPWATERPREF.M12 *PREF.CONVGALLON) AS WATERP12, "
                StrSql = StrSql + "(EQUIPWATERPREF.M13 *PREF.CONVGALLON) AS WATERP13, "
                StrSql = StrSql + "(EQUIPWATERPREF.M14 *PREF.CONVGALLON) AS WATERP14, "
                StrSql = StrSql + "(EQUIPWATERPREF.M15 *PREF.CONVGALLON) AS WATERP15, "
                StrSql = StrSql + "(EQUIPWATERPREF.M16 *PREF.CONVGALLON) AS WATERP16, "
                StrSql = StrSql + "(EQUIPWATERPREF.M17 *PREF.CONVGALLON) AS WATERP17, "
                StrSql = StrSql + "(EQUIPWATERPREF.M18 *PREF.CONVGALLON) AS WATERP18, "
                StrSql = StrSql + "(EQUIPWATERPREF.M19 *PREF.CONVGALLON) AS WATERP19, "
                StrSql = StrSql + "(EQUIPWATERPREF.M20 *PREF.CONVGALLON) AS WATERP20, "
                StrSql = StrSql + "(EQUIPWATERPREF.M21 *PREF.CONVGALLON) AS WATERP21, "
                StrSql = StrSql + "(EQUIPWATERPREF.M22 *PREF.CONVGALLON) AS WATERP22, "
                StrSql = StrSql + "(EQUIPWATERPREF.M23 *PREF.CONVGALLON) AS WATERP23, "
                StrSql = StrSql + "(EQUIPWATERPREF.M24 *PREF.CONVGALLON) AS WATERP24, "
                StrSql = StrSql + "(EQUIPWATERPREF.M25 *PREF.CONVGALLON) AS WATERP25, "
                StrSql = StrSql + "(EQUIPWATERPREF.M26 *PREF.CONVGALLON) AS WATERP26, "
                StrSql = StrSql + "(EQUIPWATERPREF.M27 *PREF.CONVGALLON) AS WATERP27, "
                StrSql = StrSql + "(EQUIPWATERPREF.M28 *PREF.CONVGALLON) AS WATERP28, "
                StrSql = StrSql + "(EQUIPWATERPREF.M29 *PREF.CONVGALLON) AS WATERP29, "
                StrSql = StrSql + "(EQUIPWATERPREF.M30 *PREF.CONVGALLON) AS WATERP30, "
                'Water End
                StrSql = StrSql + "EQD.M1 DEP1, "
                StrSql = StrSql + "EQD.M2 DEP2, "
                StrSql = StrSql + "EQD.M3 DEP3, "
                StrSql = StrSql + "EQD.M4 DEP4, "
                StrSql = StrSql + "EQD.M5 DEP5, "
                StrSql = StrSql + "EQD.M6 DEP6, "
                StrSql = StrSql + "EQD.M7 DEP7, "
                StrSql = StrSql + "EQD.M8 DEP8, "
                StrSql = StrSql + "EQD.M9 DEP9, "
                StrSql = StrSql + "EQD.M10 DEP10, "
                StrSql = StrSql + "EQD.M11 DEP11, "
                StrSql = StrSql + "EQD.M12 DEP12, "
                StrSql = StrSql + "EQD.M13 DEP13, "
                StrSql = StrSql + "EQD.M14 DEP14, "
                StrSql = StrSql + "EQD.M15 DEP15, "
                StrSql = StrSql + "EQD.M16 DEP16, "
                StrSql = StrSql + "EQD.M17 DEP17, "
                StrSql = StrSql + "EQD.M18 DEP18, "
                StrSql = StrSql + "EQD.M19 DEP19, "
                StrSql = StrSql + "EQD.M20 DEP20, "
                StrSql = StrSql + "EQD.M21 DEP21, "
                StrSql = StrSql + "EQD.M22 DEP22, "
                StrSql = StrSql + "EQD.M23 DEP23, "
                StrSql = StrSql + "EQD.M24 DEP24, "
                StrSql = StrSql + "EQD.M25 DEP25, "
                StrSql = StrSql + "EQD.M26 DEP26, "
                StrSql = StrSql + "EQD.M27 DEP27, "
                StrSql = StrSql + "EQD.M28 DEP28, "
                StrSql = StrSql + "EQD.M29 DEP29, "
                StrSql = StrSql + "EQD.M30 DEP30, "
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

                'ADDED FOR BUG#344
                StrSql = StrSql + "EQNUM.M1 AS NUM1, "
                StrSql = StrSql + "EQNUM.M2 AS NUM2, "
                StrSql = StrSql + "EQNUM.M3 AS NUM3, "
                StrSql = StrSql + "EQNUM.M4 AS NUM4, "
                StrSql = StrSql + "EQNUM.M5 AS NUM5, "
                StrSql = StrSql + "EQNUM.M6 AS NUM6, "
                StrSql = StrSql + "EQNUM.M7 AS NUM7, "
                StrSql = StrSql + "EQNUM.M8 AS NUM8, "
                StrSql = StrSql + "EQNUM.M9 AS NUM9, "
                StrSql = StrSql + "EQNUM.M10 AS NUM10, "
                StrSql = StrSql + "EQNUM.M11 AS NUM11, "
                StrSql = StrSql + "EQNUM.M12 AS NUM12, "
                StrSql = StrSql + "EQNUM.M13 AS NUM13, "
                StrSql = StrSql + "EQNUM.M14 AS NUM14, "
                StrSql = StrSql + "EQNUM.M15 AS NUM15, "
                StrSql = StrSql + "EQNUM.M16 AS NUM16, "
                StrSql = StrSql + "EQNUM.M17 AS NUM17, "
                StrSql = StrSql + "EQNUM.M18 AS NUM18, "
                StrSql = StrSql + "EQNUM.M19 AS NUM19, "
                StrSql = StrSql + "EQNUM.M20 AS NUM20, "
                StrSql = StrSql + "EQNUM.M21 AS NUM21, "
                StrSql = StrSql + "EQNUM.M22 AS NUM22, "
                StrSql = StrSql + "EQNUM.M23 AS NUM23, "
                StrSql = StrSql + "EQNUM.M24 AS NUM24, "
                StrSql = StrSql + "EQNUM.M25 AS NUM25, "
                StrSql = StrSql + "EQNUM.M26 AS NUM26, "
                StrSql = StrSql + "EQNUM.M27 AS NUM27, "
                StrSql = StrSql + "EQNUM.M28 AS NUM28, "
                StrSql = StrSql + "EQNUM.M29 AS NUM29, "
                StrSql = StrSql + "EQNUM.M30 AS NUM30 "
                'END BUG#344

                StrSql = StrSql + "FROM EQUIPMENTTYPE EQT "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ1 "
                StrSql = StrSql + "ON EQ1.EQUIPID=EQT.M1 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ2 "
                StrSql = StrSql + "ON EQ2.EQUIPID=EQT.M2 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ3 "
                StrSql = StrSql + "ON EQ3.EQUIPID=EQT.M3 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ4 "
                StrSql = StrSql + "ON EQ4.EQUIPID=EQT.M4 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ5 "
                StrSql = StrSql + "ON EQ5.EQUIPID=EQT.M5 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ6 "
                StrSql = StrSql + "ON EQ6.EQUIPID=EQT.M6 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ7 "
                StrSql = StrSql + "ON EQ7.EQUIPID=EQT.M7 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ8 "
                StrSql = StrSql + "ON EQ8.EQUIPID=EQT.M8 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ9 "
                StrSql = StrSql + "ON EQ9.EQUIPID=EQT.M9 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ10 "
                StrSql = StrSql + "ON EQ10.EQUIPID=EQT.M10 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ11 "
                StrSql = StrSql + "ON EQ11.EQUIPID=EQT.M11 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ12 "
                StrSql = StrSql + "ON EQ12.EQUIPID=EQT.M12 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ13 "
                StrSql = StrSql + "ON EQ13.EQUIPID=EQT.M13 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ14 "
                StrSql = StrSql + "ON EQ14.EQUIPID=EQT.M14 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ15 "
                StrSql = StrSql + "ON EQ15.EQUIPID=EQT.M15 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ16 "
                StrSql = StrSql + "ON EQ16.EQUIPID=EQT.M16 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ17 "
                StrSql = StrSql + "ON EQ17.EQUIPID=EQT.M17 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ18 "
                StrSql = StrSql + "ON EQ18.EQUIPID=EQT.M18 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ19 "
                StrSql = StrSql + "ON EQ19.EQUIPID=EQT.M19 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ20 "
                StrSql = StrSql + "ON EQ20.EQUIPID=EQT.M20 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ21 "
                StrSql = StrSql + "ON EQ21.EQUIPID=EQT.M21 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ22 "
                StrSql = StrSql + "ON EQ22.EQUIPID=EQT.M22 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ23 "
                StrSql = StrSql + "ON EQ23.EQUIPID=EQT.M23 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ24 "
                StrSql = StrSql + "ON EQ24.EQUIPID=EQT.M24 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ25 "
                StrSql = StrSql + "ON EQ25.EQUIPID=EQT.M25 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ26 "
                StrSql = StrSql + "ON EQ26.EQUIPID=EQT.M26 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ27 "
                StrSql = StrSql + "ON EQ27.EQUIPID=EQT.M27 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ28 "
                StrSql = StrSql + "ON EQ28.EQUIPID=EQT.M28 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ29 "
                StrSql = StrSql + "ON EQ29.EQUIPID=EQT.M29 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ30 "
                StrSql = StrSql + "ON EQ30.EQUIPID=EQT.M30 "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=EQT.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIPMENTAREA EA "
                StrSql = StrSql + "ON EA.CASEID=EQT.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIPENERGYPREF EQPREF "
                StrSql = StrSql + "ON EQPREF.CASEID=EQT.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIPNATURALGASPREF EQNPREF "
                StrSql = StrSql + "ON EQNPREF.CASEID=EQT.CASEID "
                StrSql = StrSql + "INNER JOIN EquipmentDEP EQD "
                StrSql = StrSql + "ON EQD.CASEID=EQT.CASEID "
                'Water Start
                StrSql = StrSql + "INNER JOIN EQUIPWATERPREF "
                StrSql = StrSql + "ON EQUIPWATERPREF.CASEID=EQT.CASEID "
                'Water End
                StrSql = StrSql + "INNER JOIN EQUIPMENTNUMBER EQNUM "
                StrSql = StrSql + "ON EQNUM.CASEID=EQT.CASEID "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "EQT.CASEID=" + CaseId.ToString() + " ORDER BY EQT.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetEquipmentDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSupportEquipmentInDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT EQT.CASEID,  "
                StrSql = StrSql + "EQT.M1, "
                StrSql = StrSql + "EQT.M2, "
                StrSql = StrSql + "EQT.M3, "
                StrSql = StrSql + "EQT.M4, "
                StrSql = StrSql + "EQT.M5, "
                StrSql = StrSql + "EQT.M6, "
                StrSql = StrSql + "EQT.M7, "
                StrSql = StrSql + "EQT.M8, "
                StrSql = StrSql + "EQT.M9, "
                StrSql = StrSql + "EQT.M10, "
                StrSql = StrSql + "EQT.M11, "
                StrSql = StrSql + "EQT.M12, "
                StrSql = StrSql + "EQT.M13, "
                StrSql = StrSql + "EQT.M14, "
                StrSql = StrSql + "EQT.M15, "
                StrSql = StrSql + "EQT.M16, "
                StrSql = StrSql + "EQT.M17, "
                StrSql = StrSql + "EQT.M18, "
                StrSql = StrSql + "EQT.M19, "
                StrSql = StrSql + "EQT.M20, "
                StrSql = StrSql + "EQT.M21, "
                StrSql = StrSql + "EQT.M22, "
                StrSql = StrSql + "EQT.M23, "
                StrSql = StrSql + "EQT.M24, "
                StrSql = StrSql + "EQT.M25, "
                StrSql = StrSql + "EQT.M26, "
                StrSql = StrSql + "EQT.M27, "
                StrSql = StrSql + "EQT.M28, "
                StrSql = StrSql + "EQT.M29, "
                StrSql = StrSql + "EQT.M30, "
                StrSql = StrSql + "EQ1.INSTKW AS ECS1, "
                StrSql = StrSql + "EQ2.INSTKW AS ECS2, "
                StrSql = StrSql + "EQ3.INSTKW AS ECS3, "
                StrSql = StrSql + "EQ4.INSTKW AS ECS4, "
                StrSql = StrSql + "EQ5.INSTKW AS ECS5, "
                StrSql = StrSql + "EQ6.INSTKW AS ECS6, "
                StrSql = StrSql + "EQ7.INSTKW AS ECS7, "
                StrSql = StrSql + "EQ8.INSTKW AS ECS8, "
                StrSql = StrSql + "EQ9.INSTKW AS ECS9, "
                StrSql = StrSql + "EQ10.INSTKW AS ECS10, "
                StrSql = StrSql + "EQ11.INSTKW AS ECS11, "
                StrSql = StrSql + "EQ12.INSTKW AS ECS12, "
                StrSql = StrSql + "EQ13.INSTKW AS ECS13, "
                StrSql = StrSql + "EQ14.INSTKW AS ECS14, "
                StrSql = StrSql + "EQ15.INSTKW AS ECS15, "
                StrSql = StrSql + "EQ16.INSTKW AS ECS16, "
                StrSql = StrSql + "EQ17.INSTKW AS ECS17, "
                StrSql = StrSql + "EQ18.INSTKW AS ECS18, "
                StrSql = StrSql + "EQ19.INSTKW AS ECS19, "
                StrSql = StrSql + "EQ20.INSTKW AS ECS20, "
                StrSql = StrSql + "EQ21.INSTKW AS ECS21, "
                StrSql = StrSql + "EQ22.INSTKW AS ECS22, "
                StrSql = StrSql + "EQ23.INSTKW AS ECS23, "
                StrSql = StrSql + "EQ24.INSTKW AS ECS24, "
                StrSql = StrSql + "EQ25.INSTKW AS ECS25, "
                StrSql = StrSql + "EQ26.INSTKW AS ECS26, "
                StrSql = StrSql + "EQ27.INSTKW AS ECS27, "
                StrSql = StrSql + "EQ28.INSTKW AS ECS28, "
                StrSql = StrSql + "EQ29.INSTKW AS ECS29, "
                StrSql = StrSql + "EQ30.INSTKW AS ECS30, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M1 ECP1, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M2 ECP2, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M3 ECP3, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M4 ECP4, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M5 ECP5, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M6 ECP6, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M7 ECP7, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M8 ECP8, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M9 ECP9, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M10 ECP10, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M11 ECP11, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M12 ECP12, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M13 ECP13, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M14 ECP14, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M15 ECP15, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M16 ECP16, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M17 ECP17, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M18 ECP18, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M19 ECP19, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M20 ECP20, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M21 ECP21, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M22 ECP22, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M23 ECP23, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M24 ECP24, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M25 ECP25, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M26 ECP26, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M27 ECP27, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M28 ECP28, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M29 ECP29, "
                StrSql = StrSql + "EQUIP2ENERGYPREF.M30 ECP30, "
                StrSql = StrSql + "EQ1.NTGKW AS NGS1, "
                StrSql = StrSql + "EQ2.NTGKW AS NGS2, "
                StrSql = StrSql + "EQ3.NTGKW AS NGS3, "
                StrSql = StrSql + "EQ4.NTGKW AS NGS4, "
                StrSql = StrSql + "EQ5.NTGKW AS NGS5, "
                StrSql = StrSql + "EQ6.NTGKW AS NGS6, "
                StrSql = StrSql + "EQ7.NTGKW AS NGS7, "
                StrSql = StrSql + "EQ8.NTGKW AS NGS8, "
                StrSql = StrSql + "EQ9.NTGKW AS NGS9, "
                StrSql = StrSql + "EQ10.NTGKW AS NGS10, "
                StrSql = StrSql + "EQ11.NTGKW AS NGS11, "
                StrSql = StrSql + "EQ12.NTGKW AS NGS12, "
                StrSql = StrSql + "EQ13.NTGKW AS NGS13, "
                StrSql = StrSql + "EQ14.NTGKW AS NGS14, "
                StrSql = StrSql + "EQ15.NTGKW AS NGS15, "
                StrSql = StrSql + "EQ16.NTGKW AS NGS16, "
                StrSql = StrSql + "EQ17.NTGKW AS NGS17, "
                StrSql = StrSql + "EQ18.NTGKW AS NGS18, "
                StrSql = StrSql + "EQ19.NTGKW AS NGS19, "
                StrSql = StrSql + "EQ20.NTGKW AS NGS20, "
                StrSql = StrSql + "EQ21.NTGKW AS NGS21, "
                StrSql = StrSql + "EQ22.NTGKW AS NGS22, "
                StrSql = StrSql + "EQ23.NTGKW AS NGS23, "
                StrSql = StrSql + "EQ24.NTGKW AS NGS24, "
                StrSql = StrSql + "EQ25.NTGKW AS NGS25, "
                StrSql = StrSql + "EQ26.NTGKW AS NGS26, "
                StrSql = StrSql + "EQ27.NTGKW AS NGS27, "
                StrSql = StrSql + "EQ28.NTGKW AS NGS28, "
                StrSql = StrSql + "EQ29.NTGKW AS NGS29, "
                StrSql = StrSql + "EQ30.NTGKW AS NGS30, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M1 NGP1, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M2 NGP2, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M3 NGP3, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M4 NGP4, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M5 NGP5, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M6 NGP6, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M7 NGP7, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M8 NGP8, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M9 NGP9, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M10 NGP10, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M11 NGP11, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M12 NGP12, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M13 NGP13, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M14 NGP14, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M15 NGP15, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M16 NGP16, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M17 NGP17, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M18 NGP18, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M19 NGP19, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M20 NGP20, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M21 NGP21, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M22 NGP22, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M23 NGP23, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M24 NGP24, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M25 NGP25, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M26 NGP26, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M27 NGP27, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M28 NGP28, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M29 NGP29, "
                StrSql = StrSql + "EQUIP2NATURALGASPREF.M30 NGP30, "
                'Water Started
                StrSql = StrSql + "(EQ1.WATERKW*PREF.CONVGALLON ) AS WATERS1, "
                StrSql = StrSql + "(EQ2.WATERKW*PREF.CONVGALLON ) AS WATERS2, "
                StrSql = StrSql + "(EQ3.WATERKW*PREF.CONVGALLON ) AS WATERS3, "
                StrSql = StrSql + "(EQ4.WATERKW*PREF.CONVGALLON ) AS WATERS4, "
                StrSql = StrSql + "(EQ5.WATERKW*PREF.CONVGALLON ) AS WATERS5, "
                StrSql = StrSql + "(EQ6.WATERKW*PREF.CONVGALLON ) AS WATERS6, "
                StrSql = StrSql + "(EQ7.WATERKW*PREF.CONVGALLON ) AS WATERS7, "
                StrSql = StrSql + "(EQ8.WATERKW*PREF.CONVGALLON ) AS WATERS8, "
                StrSql = StrSql + "(EQ9.WATERKW*PREF.CONVGALLON ) AS WATERS9, "
                StrSql = StrSql + "(EQ10.WATERKW*PREF.CONVGALLON ) AS WATERS10, "
                StrSql = StrSql + "(EQ11.WATERKW*PREF.CONVGALLON ) AS WATERS11, "
                StrSql = StrSql + "(EQ12.WATERKW*PREF.CONVGALLON ) AS WATERS12, "
                StrSql = StrSql + "(EQ13.WATERKW*PREF.CONVGALLON ) AS WATERS13, "
                StrSql = StrSql + "(EQ14.WATERKW*PREF.CONVGALLON ) AS WATERS14, "
                StrSql = StrSql + "(EQ15.WATERKW*PREF.CONVGALLON ) AS WATERS15, "
                StrSql = StrSql + "(EQ16.WATERKW*PREF.CONVGALLON ) AS WATERS16, "
                StrSql = StrSql + "(EQ17.WATERKW*PREF.CONVGALLON ) AS WATERS17, "
                StrSql = StrSql + "(EQ18.WATERKW*PREF.CONVGALLON ) AS WATERS18, "
                StrSql = StrSql + "(EQ19.WATERKW*PREF.CONVGALLON ) AS WATERS19, "
                StrSql = StrSql + "(EQ20.WATERKW*PREF.CONVGALLON ) AS WATERS20, "
                StrSql = StrSql + "(EQ21.WATERKW*PREF.CONVGALLON ) AS WATERS21, "
                StrSql = StrSql + "(EQ22.WATERKW*PREF.CONVGALLON ) AS WATERS22, "
                StrSql = StrSql + "(EQ23.WATERKW*PREF.CONVGALLON ) AS WATERS23, "
                StrSql = StrSql + "(EQ24.WATERKW*PREF.CONVGALLON ) AS WATERS24, "
                StrSql = StrSql + "(EQ25.WATERKW*PREF.CONVGALLON ) AS WATERS25, "
                StrSql = StrSql + "(EQ26.WATERKW*PREF.CONVGALLON ) AS WATERS26, "
                StrSql = StrSql + "(EQ27.WATERKW*PREF.CONVGALLON ) AS WATERS27, "
                StrSql = StrSql + "(EQ28.WATERKW*PREF.CONVGALLON ) AS WATERS28, "
                StrSql = StrSql + "(EQ29.WATERKW*PREF.CONVGALLON ) AS WATERS29, "
                StrSql = StrSql + "(EQ30.WATERKW*PREF.CONVGALLON ) AS WATERS30, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M1 *PREF.CONVGALLON) AS WATERP1, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M2 *PREF.CONVGALLON) AS WATERP2, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M3 *PREF.CONVGALLON) AS WATERP3, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M4 *PREF.CONVGALLON) AS WATERP4, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M5 *PREF.CONVGALLON) AS WATERP5, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M6 *PREF.CONVGALLON) AS WATERP6, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M7 *PREF.CONVGALLON) AS WATERP7, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M8 *PREF.CONVGALLON) AS WATERP8, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M9 *PREF.CONVGALLON) AS WATERP9, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M10 *PREF.CONVGALLON) AS WATERP10, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M11 *PREF.CONVGALLON) AS WATERP11, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M12 *PREF.CONVGALLON) AS WATERP12, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M13 *PREF.CONVGALLON) AS WATERP13, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M14 *PREF.CONVGALLON) AS WATERP14, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M15 *PREF.CONVGALLON) AS WATERP15, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M16 *PREF.CONVGALLON) AS WATERP16, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M17 *PREF.CONVGALLON) AS WATERP17, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M18 *PREF.CONVGALLON) AS WATERP18, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M19 *PREF.CONVGALLON) AS WATERP19, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M20 *PREF.CONVGALLON) AS WATERP20, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M21 *PREF.CONVGALLON) AS WATERP21, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M22 *PREF.CONVGALLON) AS WATERP22, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M23 *PREF.CONVGALLON) AS WATERP23, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M24 *PREF.CONVGALLON) AS WATERP24, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M25 *PREF.CONVGALLON) AS WATERP25, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M26 *PREF.CONVGALLON) AS WATERP26, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M27 *PREF.CONVGALLON) AS WATERP27, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M28 *PREF.CONVGALLON) AS WATERP28, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M29 *PREF.CONVGALLON) AS WATERP29, "
                StrSql = StrSql + "(EQUIP2WATERPREF.M30 *PREF.CONVGALLON) AS WATERP30, "
                'Water End
                StrSql = StrSql + "EQHRS.M1 AS HRS1, "
                StrSql = StrSql + "EQHRS.M2 AS HRS2, "
                StrSql = StrSql + "EQHRS.M3 AS HRS3, "
                StrSql = StrSql + "EQHRS.M4 AS HRS4, "
                StrSql = StrSql + "EQHRS.M5 AS HRS5, "
                StrSql = StrSql + "EQHRS.M6 AS HRS6, "
                StrSql = StrSql + "EQHRS.M7 AS HRS7, "
                StrSql = StrSql + "EQHRS.M8 AS HRS8, "
                StrSql = StrSql + "EQHRS.M9 AS HRS9, "
                StrSql = StrSql + "EQHRS.M10 AS HRS10, "
                StrSql = StrSql + "EQHRS.M11 AS HRS11, "
                StrSql = StrSql + "EQHRS.M12 AS HRS12, "
                StrSql = StrSql + "EQHRS.M13 AS HRS13, "
                StrSql = StrSql + "EQHRS.M14 AS HRS14, "
                StrSql = StrSql + "EQHRS.M15 AS HRS15, "
                StrSql = StrSql + "EQHRS.M16 AS HRS16, "
                StrSql = StrSql + "EQHRS.M17 AS HRS17, "
                StrSql = StrSql + "EQHRS.M18 AS HRS18, "
                StrSql = StrSql + "EQHRS.M19 AS HRS19, "
                StrSql = StrSql + "EQHRS.M20 AS HRS20, "
                StrSql = StrSql + "EQHRS.M21 AS HRS21, "
                StrSql = StrSql + "EQHRS.M22 AS HRS22, "
                StrSql = StrSql + "EQHRS.M23 AS HRS23, "
                StrSql = StrSql + "EQHRS.M24 AS HRS24, "
                StrSql = StrSql + "EQHRS.M25 AS HRS25, "
                StrSql = StrSql + "EQHRS.M26 AS HRS26, "
                StrSql = StrSql + "EQHRS.M27 AS HRS27, "
                StrSql = StrSql + "EQHRS.M28 AS HRS28, "
                StrSql = StrSql + "EQHRS.M29 AS HRS29, "
                StrSql = StrSql + "EQHRS.M30 AS HRS30, "
                StrSql = StrSql + "Equipment2DEP.M1 DEP1, "
                StrSql = StrSql + "Equipment2DEP.M2 DEP2, "
                StrSql = StrSql + "Equipment2DEP.M3 DEP3, "
                StrSql = StrSql + "Equipment2DEP.M4 DEP4, "
                StrSql = StrSql + "Equipment2DEP.M5 DEP5, "
                StrSql = StrSql + "Equipment2DEP.M6 DEP6, "
                StrSql = StrSql + "Equipment2DEP.M7 DEP7, "
                StrSql = StrSql + "Equipment2DEP.M8 DEP8, "
                StrSql = StrSql + "Equipment2DEP.M9 DEP9, "
                StrSql = StrSql + "Equipment2DEP.M10 DEP10, "
                StrSql = StrSql + "Equipment2DEP.M11 DEP11, "
                StrSql = StrSql + "Equipment2DEP.M12 DEP12, "
                StrSql = StrSql + "Equipment2DEP.M13 DEP13, "
                StrSql = StrSql + "Equipment2DEP.M14 DEP14, "
                StrSql = StrSql + "Equipment2DEP.M15 DEP15, "
                StrSql = StrSql + "Equipment2DEP.M16 DEP16, "
                StrSql = StrSql + "Equipment2DEP.M17 DEP17, "
                StrSql = StrSql + "Equipment2DEP.M18 DEP18, "
                StrSql = StrSql + "Equipment2DEP.M19 DEP19, "
                StrSql = StrSql + "Equipment2DEP.M20 DEP20, "
                StrSql = StrSql + "Equipment2DEP.M21 DEP21, "
                StrSql = StrSql + "Equipment2DEP.M22 DEP22, "
                StrSql = StrSql + "Equipment2DEP.M23 DEP23, "
                StrSql = StrSql + "Equipment2DEP.M24 DEP24, "
                StrSql = StrSql + "Equipment2DEP.M25 DEP25, "
                StrSql = StrSql + "Equipment2DEP.M26 DEP26, "
                StrSql = StrSql + "Equipment2DEP.M27 DEP27, "
                StrSql = StrSql + "Equipment2DEP.M28 DEP28, "
                StrSql = StrSql + "Equipment2DEP.M29 DEP29, "
                StrSql = StrSql + "Equipment2DEP.M30 DEP30, "
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

                StrSql = StrSql + "EQNUM.M1 AS NUM1, "
                StrSql = StrSql + "EQNUM.M2 AS NUM2, "
                StrSql = StrSql + "EQNUM.M3 AS NUM3, "
                StrSql = StrSql + "EQNUM.M4 AS NUM4, "
                StrSql = StrSql + "EQNUM.M5 AS NUM5, "
                StrSql = StrSql + "EQNUM.M6 AS NUM6, "
                StrSql = StrSql + "EQNUM.M7 AS NUM7, "
                StrSql = StrSql + "EQNUM.M8 AS NUM8, "
                StrSql = StrSql + "EQNUM.M9 AS NUM9, "
                StrSql = StrSql + "EQNUM.M10 AS NUM10, "
                StrSql = StrSql + "EQNUM.M11 AS NUM11, "
                StrSql = StrSql + "EQNUM.M12 AS NUM12, "
                StrSql = StrSql + "EQNUM.M13 AS NUM13, "
                StrSql = StrSql + "EQNUM.M14 AS NUM14, "
                StrSql = StrSql + "EQNUM.M15 AS NUM15, "
                StrSql = StrSql + "EQNUM.M16 AS NUM16, "
                StrSql = StrSql + "EQNUM.M17 AS NUM17, "
                StrSql = StrSql + "EQNUM.M18 AS NUM18, "
                StrSql = StrSql + "EQNUM.M19 AS NUM19, "
                StrSql = StrSql + "EQNUM.M20 AS NUM20, "
                StrSql = StrSql + "EQNUM.M21 AS NUM21, "
                StrSql = StrSql + "EQNUM.M22 AS NUM22, "
                StrSql = StrSql + "EQNUM.M23 AS NUM23, "
                StrSql = StrSql + "EQNUM.M24 AS NUM24, "
                StrSql = StrSql + "EQNUM.M25 AS NUM25, "
                StrSql = StrSql + "EQNUM.M26 AS NUM26, "
                StrSql = StrSql + "EQNUM.M27 AS NUM27, "
                StrSql = StrSql + "EQNUM.M28 AS NUM28, "
                StrSql = StrSql + "EQNUM.M29 AS NUM29, "
                StrSql = StrSql + "EQNUM.M30 AS NUM30 "

                StrSql = StrSql + "FROM EQUIPMENT2TYPE EQT "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ1 "
                StrSql = StrSql + "ON EQ1.EQUIPID=EQT.M1 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ2 "
                StrSql = StrSql + "ON EQ2.EQUIPID=EQT.M2 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ3 "
                StrSql = StrSql + "ON EQ3.EQUIPID=EQT.M3 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ4 "
                StrSql = StrSql + "ON EQ4.EQUIPID=EQT.M4 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ5 "
                StrSql = StrSql + "ON EQ5.EQUIPID=EQT.M5 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ6 "
                StrSql = StrSql + "ON EQ6.EQUIPID=EQT.M6 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ7 "
                StrSql = StrSql + "ON EQ7.EQUIPID=EQT.M7 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ8 "
                StrSql = StrSql + "ON EQ8.EQUIPID=EQT.M8 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ9 "
                StrSql = StrSql + "ON EQ9.EQUIPID=EQT.M9 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ10 "
                StrSql = StrSql + "ON EQ10.EQUIPID=EQT.M10 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ11 "
                StrSql = StrSql + "ON EQ11.EQUIPID=EQT.M11 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ12 "
                StrSql = StrSql + "ON EQ12.EQUIPID=EQT.M12 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ13 "
                StrSql = StrSql + "ON EQ13.EQUIPID=EQT.M13 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ14 "
                StrSql = StrSql + "ON EQ14.EQUIPID=EQT.M14 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ15 "
                StrSql = StrSql + "ON EQ15.EQUIPID=EQT.M15 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ16 "
                StrSql = StrSql + "ON EQ16.EQUIPID=EQT.M16 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ17 "
                StrSql = StrSql + "ON EQ17.EQUIPID=EQT.M17 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ18 "
                StrSql = StrSql + "ON EQ18.EQUIPID=EQT.M18 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ19 "
                StrSql = StrSql + "ON EQ19.EQUIPID=EQT.M19 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ20 "
                StrSql = StrSql + "ON EQ20.EQUIPID=EQT.M20 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ21 "
                StrSql = StrSql + "ON EQ21.EQUIPID=EQT.M21 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ22 "
                StrSql = StrSql + "ON EQ22.EQUIPID=EQT.M22 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ23 "
                StrSql = StrSql + "ON EQ23.EQUIPID=EQT.M23 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ24 "
                StrSql = StrSql + "ON EQ24.EQUIPID=EQT.M24 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ25 "
                StrSql = StrSql + "ON EQ25.EQUIPID=EQT.M25 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ26 "
                StrSql = StrSql + "ON EQ26.EQUIPID=EQT.M26 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ27 "
                StrSql = StrSql + "ON EQ27.EQUIPID=EQT.M27 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ28 "
                StrSql = StrSql + "ON EQ28.EQUIPID=EQT.M28 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ29 "
                StrSql = StrSql + "ON EQ29.EQUIPID=EQT.M29 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT2 EQ30 "
                StrSql = StrSql + "ON EQ30.EQUIPID=EQT.M30 "
                StrSql = StrSql + "INNER JOIN EQUIP2ENERGYPREF "
                StrSql = StrSql + "ON EQUIP2ENERGYPREF.CASEID=EQT.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIP2NATURALGASPREF "
                StrSql = StrSql + "ON EQUIP2NATURALGASPREF.CASEID=EQT.CASEID "
                StrSql = StrSql + "INNER JOIN Equipment2DEP "
                StrSql = StrSql + "ON Equipment2DEP.CASEID=EQT.CASEID "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=EQT.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2MAHRS EQHRS "
                StrSql = StrSql + "ON EQHRS.CASEID=EQT.CASEID "
                'Water Started
                StrSql = StrSql + "INNER JOIN EQUIP2WATERPREF "
                StrSql = StrSql + "ON EQUIP2WATERPREF.CASEID=EQT.CASEID "
                'Water End

                StrSql = StrSql + "INNER JOIN EQUIPMENT2NUMBER EQNUM "
                StrSql = StrSql + "ON EQNUM.CASEID=EQT.CASEID "

                StrSql = StrSql + "WHERE EQT.CASEID=" + CaseId.ToString() + " ORDER BY EQT.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetSupportDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetOperationInDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT EQT.CASEID,  "
                StrSql = StrSql + "(EQ1.EQUIPDE1 || ' '|| EQ1.EQUIPDE2) AS EQDES1, "
                StrSql = StrSql + "(EQ2.EQUIPDE1 || ' '|| EQ2.EQUIPDE2) AS EQDES2, "
                StrSql = StrSql + "(EQ3.EQUIPDE1 || ' '|| EQ3.EQUIPDE2) AS EQDES3, "
                StrSql = StrSql + "(EQ4.EQUIPDE1 || ' '|| EQ4.EQUIPDE2) AS EQDES4, "
                StrSql = StrSql + "(EQ5.EQUIPDE1 || ' '|| EQ5.EQUIPDE2) AS EQDES5, "
                StrSql = StrSql + "(EQ6.EQUIPDE1 || ' '|| EQ6.EQUIPDE2) AS EQDES6, "
                StrSql = StrSql + "(EQ7.EQUIPDE1 || ' '|| EQ7.EQUIPDE2) AS EQDES7, "
                StrSql = StrSql + "(EQ8.EQUIPDE1 || ' '|| EQ8.EQUIPDE2) AS EQDES8, "
                StrSql = StrSql + "(EQ9.EQUIPDE1 || ' '|| EQ9.EQUIPDE2) AS EQDES9, "
                StrSql = StrSql + "(EQ10.EQUIPDE1 || ' '|| EQ10.EQUIPDE2) AS EQDES10, "
                StrSql = StrSql + "(EQ11.EQUIPDE1 || ' '|| EQ11.EQUIPDE2) AS EQDES11, "
                StrSql = StrSql + "(EQ12.EQUIPDE1 || ' '|| EQ12.EQUIPDE2) AS EQDES12, "
                StrSql = StrSql + "(EQ13.EQUIPDE1 || ' '|| EQ13.EQUIPDE2) AS EQDES13, "
                StrSql = StrSql + "(EQ14.EQUIPDE1 || ' '|| EQ14.EQUIPDE2) AS EQDES14, "
                StrSql = StrSql + "(EQ15.EQUIPDE1 || ' '|| EQ15.EQUIPDE2) AS EQDES15, "
                StrSql = StrSql + "(EQ16.EQUIPDE1 || ' '|| EQ16.EQUIPDE2) AS EQDES16, "
                StrSql = StrSql + "(EQ17.EQUIPDE1 || ' '|| EQ17.EQUIPDE2) AS EQDES17, "
                StrSql = StrSql + "(EQ18.EQUIPDE1 || ' '|| EQ18.EQUIPDE2) AS EQDES18, "
                StrSql = StrSql + "(EQ19.EQUIPDE1 || ' '|| EQ19.EQUIPDE2) AS EQDES19, "
                StrSql = StrSql + "(EQ20.EQUIPDE1 || ' '|| EQ20.EQUIPDE2) AS EQDES20, "
                StrSql = StrSql + "(EQ21.EQUIPDE1 || ' '|| EQ21.EQUIPDE2) AS EQDES21, "
                StrSql = StrSql + "(EQ22.EQUIPDE1 || ' '|| EQ22.EQUIPDE2) AS EQDES22, "
                StrSql = StrSql + "(EQ23.EQUIPDE1 || ' '|| EQ23.EQUIPDE2) AS EQDES23, "
                StrSql = StrSql + "(EQ24.EQUIPDE1 || ' '|| EQ24.EQUIPDE2) AS EQDES24, "
                StrSql = StrSql + "(EQ25.EQUIPDE1 || ' '|| EQ25.EQUIPDE2) AS EQDES25, "
                StrSql = StrSql + "(EQ26.EQUIPDE1 || ' '|| EQ26.EQUIPDE2) AS EQDES26, "
                StrSql = StrSql + "(EQ27.EQUIPDE1 || ' '|| EQ27.EQUIPDE2) AS EQDES27, "
                StrSql = StrSql + "(EQ28.EQUIPDE1 || ' '|| EQ28.EQUIPDE2) AS EQDES28, "
                StrSql = StrSql + "(EQ29.EQUIPDE1 || ' '|| EQ29.EQUIPDE2) AS EQDES29, "
                StrSql = StrSql + "(EQ30.EQUIPDE1 || ' '|| EQ30.EQUIPDE2) AS EQDES30, "
                StrSql = StrSql + "EQT.M1, "
                StrSql = StrSql + "EQT.M2, "
                StrSql = StrSql + "EQT.M3, "
                StrSql = StrSql + "EQT.M4, "
                StrSql = StrSql + "EQT.M5, "
                StrSql = StrSql + "EQT.M6, "
                StrSql = StrSql + "EQT.M7, "
                StrSql = StrSql + "EQT.M8, "
                StrSql = StrSql + "EQT.M9, "
                StrSql = StrSql + "EQT.M10, "
                StrSql = StrSql + "EQT.M11, "
                StrSql = StrSql + "EQT.M12, "
                StrSql = StrSql + "EQT.M13, "
                StrSql = StrSql + "EQT.M14, "
                StrSql = StrSql + "EQT.M15, "
                StrSql = StrSql + "EQT.M16, "
                StrSql = StrSql + "EQT.M17, "
                StrSql = StrSql + "EQT.M18, "
                StrSql = StrSql + "EQT.M19, "
                StrSql = StrSql + "EQT.M20, "
                StrSql = StrSql + "EQT.M21, "
                StrSql = StrSql + "EQT.M22, "
                StrSql = StrSql + "EQT.M23, "
                StrSql = StrSql + "EQT.M24, "
                StrSql = StrSql + "EQT.M25, "
                StrSql = StrSql + "EQT.M26, "
                StrSql = StrSql + "EQT.M27, "
                StrSql = StrSql + "EQT.M28, "
                StrSql = StrSql + "EQT.M29, "
                StrSql = StrSql + "EQT.M30, "
                StrSql = StrSql + "OPMAXRUNHRS.M1 MAXHOUR1, "
                StrSql = StrSql + "OPMAXRUNHRS.M2 MAXHOUR2, "
                StrSql = StrSql + "OPMAXRUNHRS.M3 MAXHOUR3, "
                StrSql = StrSql + "OPMAXRUNHRS.M4 MAXHOUR4, "
                StrSql = StrSql + "OPMAXRUNHRS.M5 MAXHOUR5, "
                StrSql = StrSql + "OPMAXRUNHRS.M6 MAXHOUR6, "
                StrSql = StrSql + "OPMAXRUNHRS.M7 MAXHOUR7, "
                StrSql = StrSql + "OPMAXRUNHRS.M8 MAXHOUR8, "
                StrSql = StrSql + "OPMAXRUNHRS.M9 MAXHOUR9, "
                StrSql = StrSql + "OPMAXRUNHRS.M10 MAXHOUR10, "
                StrSql = StrSql + "OPMAXRUNHRS.M11 MAXHOUR11, "
                StrSql = StrSql + "OPMAXRUNHRS.M12 MAXHOUR12, "
                StrSql = StrSql + "OPMAXRUNHRS.M13 MAXHOUR13, "
                StrSql = StrSql + "OPMAXRUNHRS.M14 MAXHOUR14, "
                StrSql = StrSql + "OPMAXRUNHRS.M15 MAXHOUR15, "
                StrSql = StrSql + "OPMAXRUNHRS.M16 MAXHOUR16, "
                StrSql = StrSql + "OPMAXRUNHRS.M17 MAXHOUR17, "
                StrSql = StrSql + "OPMAXRUNHRS.M18 MAXHOUR18, "
                StrSql = StrSql + "OPMAXRUNHRS.M19 MAXHOUR19, "
                StrSql = StrSql + "OPMAXRUNHRS.M20 MAXHOUR20, "
                StrSql = StrSql + "OPMAXRUNHRS.M21 MAXHOUR21, "
                StrSql = StrSql + "OPMAXRUNHRS.M22 MAXHOUR22, "
                StrSql = StrSql + "OPMAXRUNHRS.M23 MAXHOUR23, "
                StrSql = StrSql + "OPMAXRUNHRS.M24 MAXHOUR24, "
                StrSql = StrSql + "OPMAXRUNHRS.M25 MAXHOUR25, "
                StrSql = StrSql + "OPMAXRUNHRS.M26 MAXHOUR26, "
                StrSql = StrSql + "OPMAXRUNHRS.M27 MAXHOUR27, "
                StrSql = StrSql + "OPMAXRUNHRS.M28 MAXHOUR28, "
                StrSql = StrSql + "OPMAXRUNHRS.M29 MAXHOUR29, "
                StrSql = StrSql + "OPMAXRUNHRS.M30 MAXHOUR30, "
                StrSql = StrSql + "OPDT.M1 AS DT1, "
                StrSql = StrSql + "OPDT.M2 AS DT2, "
                StrSql = StrSql + "OPDT.M3 AS DT3, "
                StrSql = StrSql + "OPDT.M4 AS DT4, "
                StrSql = StrSql + "OPDT.M5 AS DT5, "
                StrSql = StrSql + "OPDT.M6 AS DT6, "
                StrSql = StrSql + "OPDT.M7 AS DT7, "
                StrSql = StrSql + "OPDT.M8 AS DT8, "
                StrSql = StrSql + "OPDT.M9 AS DT9, "
                StrSql = StrSql + "OPDT.M10 AS DT10, "
                StrSql = StrSql + "OPDT.M11 AS DT11, "
                StrSql = StrSql + "OPDT.M12 AS DT12, "
                StrSql = StrSql + "OPDT.M13 AS DT13, "
                StrSql = StrSql + "OPDT.M14 AS DT14, "
                StrSql = StrSql + "OPDT.M15 AS DT15, "
                StrSql = StrSql + "OPDT.M16 AS DT16, "
                StrSql = StrSql + "OPDT.M17 AS DT17, "
                StrSql = StrSql + "OPDT.M18 AS DT18, "
                StrSql = StrSql + "OPDT.M19 AS DT19, "
                StrSql = StrSql + "OPDT.M20 AS DT20, "
                StrSql = StrSql + "OPDT.M21 AS DT21, "
                StrSql = StrSql + "OPDT.M22 AS DT22, "
                StrSql = StrSql + "OPDT.M23 AS DT23, "
                StrSql = StrSql + "OPDT.M24 AS DT24, "
                StrSql = StrSql + "OPDT.M25 AS DT25, "
                StrSql = StrSql + "OPDT.M26 AS DT26, "
                StrSql = StrSql + "OPDT.M27 AS DT27, "
                StrSql = StrSql + "OPDT.M28 AS DT28, "
                StrSql = StrSql + "OPDT.M29 AS DT29, "
                StrSql = StrSql + "OPDT.M30 AS DT30, "
                StrSql = StrSql + "OPWASTE.M1 AS OPWASTE1, "
                StrSql = StrSql + "OPWASTE.M2 AS OPWASTE2, "
                StrSql = StrSql + "OPWASTE.M3 AS OPWASTE3, "
                StrSql = StrSql + "OPWASTE.M4 AS OPWASTE4, "
                StrSql = StrSql + "OPWASTE.M5 AS OPWASTE5, "
                StrSql = StrSql + "OPWASTE.M6 AS OPWASTE6, "
                StrSql = StrSql + "OPWASTE.M7 AS OPWASTE7, "
                StrSql = StrSql + "OPWASTE.M8 AS OPWASTE8, "
                StrSql = StrSql + "OPWASTE.M9 AS OPWASTE9, "
                StrSql = StrSql + "OPWASTE.M10 AS OPWASTE10, "
                StrSql = StrSql + "OPWASTE.M11 AS OPWASTE11, "
                StrSql = StrSql + "OPWASTE.M12 AS OPWASTE12, "
                StrSql = StrSql + "OPWASTE.M13 AS OPWASTE13, "
                StrSql = StrSql + "OPWASTE.M14 AS OPWASTE14, "
                StrSql = StrSql + "OPWASTE.M15 AS OPWASTE15, "
                StrSql = StrSql + "OPWASTE.M16 AS OPWASTE16, "
                StrSql = StrSql + "OPWASTE.M17 AS OPWASTE17, "
                StrSql = StrSql + "OPWASTE.M18 AS OPWASTE18, "
                StrSql = StrSql + "OPWASTE.M19 AS OPWASTE19, "
                StrSql = StrSql + "OPWASTE.M20 AS OPWASTE20, "
                StrSql = StrSql + "OPWASTE.M21 AS OPWASTE21, "
                StrSql = StrSql + "OPWASTE.M22 AS OPWASTE22, "
                StrSql = StrSql + "OPWASTE.M23 AS OPWASTE23, "
                StrSql = StrSql + "OPWASTE.M24 AS OPWASTE24, "
                StrSql = StrSql + "OPWASTE.M25 AS OPWASTE25, "
                StrSql = StrSql + "OPWASTE.M26 AS OPWASTE26, "
                StrSql = StrSql + "OPWASTE.M27 AS OPWASTE27, "
                StrSql = StrSql + "OPWASTE.M28 AS OPWASTE28, "
                StrSql = StrSql + "OPWASTE.M29 AS OPWASTE29, "
                StrSql = StrSql + "OPWASTE.M30 AS OPWASTE30, "
                StrSql = StrSql + "EQ1.UNITS AS UNITS1, "
                StrSql = StrSql + "EQ2.UNITS AS UNITS2, "
                StrSql = StrSql + "EQ3.UNITS AS UNITS3, "
                StrSql = StrSql + "EQ4.UNITS AS UNITS4, "
                StrSql = StrSql + "EQ5.UNITS AS UNITS5, "
                StrSql = StrSql + "EQ6.UNITS AS UNITS6, "
                StrSql = StrSql + "EQ7.UNITS AS UNITS7, "
                StrSql = StrSql + "EQ8.UNITS AS UNITS8, "
                StrSql = StrSql + "EQ9.UNITS AS UNITS9, "
                StrSql = StrSql + "EQ10.UNITS AS UNITS10, "
                StrSql = StrSql + "EQ11.UNITS AS UNITS11, "
                StrSql = StrSql + "EQ12.UNITS AS UNITS12, "
                StrSql = StrSql + "EQ13.UNITS AS UNITS13, "
                StrSql = StrSql + "EQ14.UNITS AS UNITS14, "
                StrSql = StrSql + "EQ15.UNITS AS UNITS15, "
                StrSql = StrSql + "EQ16.UNITS AS UNITS16, "
                StrSql = StrSql + "EQ17.UNITS AS UNITS17, "
                StrSql = StrSql + "EQ18.UNITS AS UNITS18, "
                StrSql = StrSql + "EQ19.UNITS AS UNITS19, "
                StrSql = StrSql + "EQ20.UNITS AS UNITS20, "
                StrSql = StrSql + "EQ21.UNITS AS UNITS21, "
                StrSql = StrSql + "EQ22.UNITS AS UNITS22, "
                StrSql = StrSql + "EQ23.UNITS AS UNITS23, "
                StrSql = StrSql + "EQ24.UNITS AS UNITS24, "
                StrSql = StrSql + "EQ25.UNITS AS UNITS25, "
                StrSql = StrSql + "EQ26.UNITS AS UNITS26, "
                StrSql = StrSql + "EQ27.UNITS AS UNITS27, "
                StrSql = StrSql + "EQ28.UNITS AS UNITS28, "
                StrSql = StrSql + "EQ29.UNITS AS UNITS29, "
                StrSql = StrSql + "EQ30.UNITS AS UNITS30, "
                StrSql = StrSql + "OPINSTGRSRATE.M1  AS OPINSTGRSRATE1, "
                StrSql = StrSql + "OPINSTGRSRATE.M2  AS OPINSTGRSRATE2, "
                StrSql = StrSql + "OPINSTGRSRATE.M3  AS OPINSTGRSRATE3, "
                StrSql = StrSql + "OPINSTGRSRATE.M4  AS OPINSTGRSRATE4, "
                StrSql = StrSql + "OPINSTGRSRATE.M5  AS OPINSTGRSRATE5, "
                StrSql = StrSql + "OPINSTGRSRATE.M6  AS OPINSTGRSRATE6, "
                StrSql = StrSql + "OPINSTGRSRATE.M7  AS OPINSTGRSRATE7, "
                StrSql = StrSql + "OPINSTGRSRATE.M8  AS OPINSTGRSRATE8, "
                StrSql = StrSql + "OPINSTGRSRATE.M9  AS OPINSTGRSRATE9, "
                StrSql = StrSql + "OPINSTGRSRATE.M10  AS OPINSTGRSRATE10, "
                StrSql = StrSql + "OPINSTGRSRATE.M11  AS OPINSTGRSRATE11, "
                StrSql = StrSql + "OPINSTGRSRATE.M12  AS OPINSTGRSRATE12, "
                StrSql = StrSql + "OPINSTGRSRATE.M13  AS OPINSTGRSRATE13, "
                StrSql = StrSql + "OPINSTGRSRATE.M14  AS OPINSTGRSRATE14, "
                StrSql = StrSql + "OPINSTGRSRATE.M15  AS OPINSTGRSRATE15, "
                StrSql = StrSql + "OPINSTGRSRATE.M16  AS OPINSTGRSRATE16, "
                StrSql = StrSql + "OPINSTGRSRATE.M17  AS OPINSTGRSRATE17, "
                StrSql = StrSql + "OPINSTGRSRATE.M18  AS OPINSTGRSRATE18, "
                StrSql = StrSql + "OPINSTGRSRATE.M19  AS OPINSTGRSRATE19, "
                StrSql = StrSql + "OPINSTGRSRATE.M20  AS OPINSTGRSRATE20, "
                StrSql = StrSql + "OPINSTGRSRATE.M21  AS OPINSTGRSRATE21, "
                StrSql = StrSql + "OPINSTGRSRATE.M22  AS OPINSTGRSRATE22, "
                StrSql = StrSql + "OPINSTGRSRATE.M23  AS OPINSTGRSRATE23, "
                StrSql = StrSql + "OPINSTGRSRATE.M24  AS OPINSTGRSRATE24, "
                StrSql = StrSql + "OPINSTGRSRATE.M25  AS OPINSTGRSRATE25, "
                StrSql = StrSql + "OPINSTGRSRATE.M26  AS OPINSTGRSRATE26, "
                StrSql = StrSql + "OPINSTGRSRATE.M27  AS OPINSTGRSRATE27, "
                StrSql = StrSql + "OPINSTGRSRATE.M28  AS OPINSTGRSRATE28, "
                StrSql = StrSql + "OPINSTGRSRATE.M29  AS OPINSTGRSRATE29, "
                StrSql = StrSql + "OPINSTGRSRATE.M30  AS OPINSTGRSRATE30, "
                StrSql = StrSql + "TOTAL.PRODWT, "
                StrSql = StrSql + "TOTAL.WTPERAREA, "
                StrSql = StrSql + "EQ1.EXITS AS EXITS1, "
                StrSql = StrSql + "EQ2.EXITS AS EXITS2, "
                StrSql = StrSql + "EQ3.EXITS AS EXITS3, "
                StrSql = StrSql + "EQ4.EXITS AS EXITS4, "
                StrSql = StrSql + "EQ5.EXITS AS EXITS5, "
                StrSql = StrSql + "EQ6.EXITS AS EXITS6, "
                StrSql = StrSql + "EQ7.EXITS AS EXITS7, "
                StrSql = StrSql + "EQ8.EXITS AS EXITS8, "
                StrSql = StrSql + "EQ9.EXITS AS EXITS9, "
                StrSql = StrSql + "EQ10.EXITS AS EXITS10, "
                StrSql = StrSql + "EQ11.EXITS AS EXITS11, "
                StrSql = StrSql + "EQ12.EXITS AS EXITS12, "
                StrSql = StrSql + "EQ13.EXITS AS EXITS13, "
                StrSql = StrSql + "EQ14.EXITS AS EXITS14, "
                StrSql = StrSql + "EQ15.EXITS AS EXITS15, "
                StrSql = StrSql + "EQ16.EXITS AS EXITS16, "
                StrSql = StrSql + "EQ17.EXITS AS EXITS17, "
                StrSql = StrSql + "EQ18.EXITS AS EXITS18, "
                StrSql = StrSql + "EQ19.EXITS AS EXITS19, "
                StrSql = StrSql + "EQ20.EXITS AS EXITS20, "
                StrSql = StrSql + "EQ21.EXITS AS EXITS21, "
                StrSql = StrSql + "EQ22.EXITS AS EXITS22, "
                StrSql = StrSql + "EQ23.EXITS AS EXITS23, "
                StrSql = StrSql + "EQ24.EXITS AS EXITS24, "
                StrSql = StrSql + "EQ25.EXITS AS EXITS25, "
                StrSql = StrSql + "EQ26.EXITS AS EXITS26, "
                StrSql = StrSql + "EQ27.EXITS AS EXITS27, "
                StrSql = StrSql + "EQ28.EXITS AS EXITS28, "
                StrSql = StrSql + "EQ29.EXITS AS EXITS29, "
                StrSql = StrSql + "EQ30.EXITS AS EXITS30, "
                StrSql = StrSql + "EQ1.UNITS2 AS UNITST1, "
                StrSql = StrSql + "EQ2.UNITS2 AS UNITST2, "
                StrSql = StrSql + "EQ3.UNITS2 AS UNITST3, "
                StrSql = StrSql + "EQ4.UNITS2 AS UNITST4, "
                StrSql = StrSql + "EQ5.UNITS2 AS UNITST5, "
                StrSql = StrSql + "EQ6.UNITS2 AS UNITST6, "
                StrSql = StrSql + "EQ7.UNITS2 AS UNITST7, "
                StrSql = StrSql + "EQ8.UNITS2 AS UNITST8, "
                StrSql = StrSql + "EQ9.UNITS2 AS UNITST9, "
                StrSql = StrSql + "EQ10.UNITS2 AS UNITST10, "
                StrSql = StrSql + "EQ11.UNITS2 AS UNITST11, "
                StrSql = StrSql + "EQ12.UNITS2 AS UNITST12, "
                StrSql = StrSql + "EQ13.UNITS2 AS UNITST13, "
                StrSql = StrSql + "EQ14.UNITS2 AS UNITST14, "
                StrSql = StrSql + "EQ15.UNITS2 AS UNITST15, "
                StrSql = StrSql + "EQ16.UNITS2 AS UNITST16, "
                StrSql = StrSql + "EQ17.UNITS2 AS UNITST17, "
                StrSql = StrSql + "EQ18.UNITS2 AS UNITST18, "
                StrSql = StrSql + "EQ19.UNITS2 AS UNITST19, "
                StrSql = StrSql + "EQ20.UNITS2 AS UNITST20, "
                StrSql = StrSql + "EQ21.UNITS2 AS UNITST21, "
                StrSql = StrSql + "EQ22.UNITS2 AS UNITST22, "
                StrSql = StrSql + "EQ23.UNITS2 AS UNITST23, "
                StrSql = StrSql + "EQ24.UNITS2 AS UNITST24, "
                StrSql = StrSql + "EQ25.UNITS2 AS UNITST25, "
                StrSql = StrSql + "EQ26.UNITS2 AS UNITST26, "
                StrSql = StrSql + "EQ27.UNITS2 AS UNITST27, "
                StrSql = StrSql + "EQ28.UNITS2 AS UNITST28, "
                StrSql = StrSql + "EQ29.UNITS2 AS UNITST29, "
                StrSql = StrSql + "EQ30.UNITS2 AS UNITST30, "
                StrSql = StrSql + "EQ1.WIDTH AS WIDTH1, "
                StrSql = StrSql + "EQ2.WIDTH AS WIDTH2, "
                StrSql = StrSql + "EQ3.WIDTH AS WIDTH3, "
                StrSql = StrSql + "EQ4.WIDTH AS WIDTH4, "
                StrSql = StrSql + "EQ5.WIDTH AS WIDTH5, "
                StrSql = StrSql + "EQ6.WIDTH AS WIDTH6, "
                StrSql = StrSql + "EQ7.WIDTH AS WIDTH7, "
                StrSql = StrSql + "EQ8.WIDTH AS WIDTH8, "
                StrSql = StrSql + "EQ9.WIDTH AS WIDTH9, "
                StrSql = StrSql + "EQ10.WIDTH AS WIDTH10, "
                StrSql = StrSql + "EQ11.WIDTH AS WIDTH11, "
                StrSql = StrSql + "EQ12.WIDTH AS WIDTH12, "
                StrSql = StrSql + "EQ13.WIDTH AS WIDTH13, "
                StrSql = StrSql + "EQ14.WIDTH AS WIDTH14, "
                StrSql = StrSql + "EQ15.WIDTH AS WIDTH15, "
                StrSql = StrSql + "EQ16.WIDTH AS WIDTH16, "
                StrSql = StrSql + "EQ17.WIDTH AS WIDTH17, "
                StrSql = StrSql + "EQ18.WIDTH AS WIDTH18, "
                StrSql = StrSql + "EQ19.WIDTH AS WIDTH19, "
                StrSql = StrSql + "EQ20.WIDTH AS WIDTH20, "
                StrSql = StrSql + "EQ21.WIDTH AS WIDTH21, "
                StrSql = StrSql + "EQ22.WIDTH AS WIDTH22, "
                StrSql = StrSql + "EQ23.WIDTH AS WIDTH23, "
                StrSql = StrSql + "EQ24.WIDTH AS WIDTH24, "
                StrSql = StrSql + "EQ25.WIDTH AS WIDTH25, "
                StrSql = StrSql + "EQ26.WIDTH AS WIDTH26, "
                StrSql = StrSql + "EQ27.WIDTH AS WIDTH27, "
                StrSql = StrSql + "EQ28.WIDTH AS WIDTH28, "
                StrSql = StrSql + "EQ29.WIDTH AS WIDTH29, "
                StrSql = StrSql + "EQ30.WIDTH AS WIDTH30, "
                StrSql = StrSql + "equipmentDEP.M1 EQDEP1, "
                StrSql = StrSql + "equipmentDEP.M2 EQDEP2, "
                StrSql = StrSql + "equipmentDEP.M3 EQDEP3, "
                StrSql = StrSql + "equipmentDEP.M4 EQDEP4, "
                StrSql = StrSql + "equipmentDEP.M5 EQDEP5, "
                StrSql = StrSql + "equipmentDEP.M6 EQDEP6, "
                StrSql = StrSql + "equipmentDEP.M7 EQDEP7, "
                StrSql = StrSql + "equipmentDEP.M8 EQDEP8, "
                StrSql = StrSql + "equipmentDEP.M9 EQDEP9, "
                StrSql = StrSql + "equipmentDEP.M10 EQDEP10, "
                StrSql = StrSql + "equipmentDEP.M11 EQDEP11, "
                StrSql = StrSql + "equipmentDEP.M12 EQDEP12, "
                StrSql = StrSql + "equipmentDEP.M13 EQDEP13, "
                StrSql = StrSql + "equipmentDEP.M14 EQDEP14, "
                StrSql = StrSql + "equipmentDEP.M15 EQDEP15, "
                StrSql = StrSql + "equipmentDEP.M16 EQDEP16, "
                StrSql = StrSql + "equipmentDEP.M17 EQDEP17, "
                StrSql = StrSql + "equipmentDEP.M18 EQDEP18, "
                StrSql = StrSql + "equipmentDEP.M19 EQDEP19, "
                StrSql = StrSql + "equipmentDEP.M20 EQDEP20, "
                StrSql = StrSql + "equipmentDEP.M21 EQDEP21, "
                StrSql = StrSql + "equipmentDEP.M22 EQDEP22, "
                StrSql = StrSql + "equipmentDEP.M23 EQDEP23, "
                StrSql = StrSql + "equipmentDEP.M24 EQDEP24, "
                StrSql = StrSql + "equipmentDEP.M25 EQDEP25, "
                StrSql = StrSql + "equipmentDEP.M26 EQDEP26, "
                StrSql = StrSql + "equipmentDEP.M27 EQDEP27, "
                StrSql = StrSql + "equipmentDEP.M28 EQDEP28, "
                StrSql = StrSql + "equipmentDEP.M29 EQDEP29, "
                StrSql = StrSql + "equipmentDEP.M30 EQDEP30, "
                StrSql = StrSql + "plantCONFIG.M1 pCONFIG1, "
                StrSql = StrSql + "plantCONFIG.M2 pCONFIG2, "
                StrSql = StrSql + "plantCONFIG.M3 pCONFIG3, "
                StrSql = StrSql + "plantCONFIG.M4 pCONFIG4, "
                StrSql = StrSql + "plantCONFIG.M5 pCONFIG5, "
                StrSql = StrSql + "plantCONFIG.M6 pCONFIG6, "
                StrSql = StrSql + "plantCONFIG.M7 pCONFIG7, "
                StrSql = StrSql + "plantCONFIG.M8 pCONFIG8, "
                StrSql = StrSql + "plantCONFIG.M9 pCONFIG9, "
                StrSql = StrSql + "plantCONFIG.M10 pCONFIG10, "
                StrSql = StrSql + "OPdepVOL.PCT1 PCT1, "
                StrSql = StrSql + "OPdepVOL.PCT2 PCT2, "
                StrSql = StrSql + "OPdepVOL.PCT3 PCT3, "
                StrSql = StrSql + "OPdepVOL.PCT4 PCT4, "
                StrSql = StrSql + "OPdepVOL.PCT5 PCT5, "
                StrSql = StrSql + "OPdepVOL.PCT6 PCT6, "
                StrSql = StrSql + "OPdepVOL.PCT7 PCT7, "
                StrSql = StrSql + "OPdepVOL.PCT8 PCT8, "
                StrSql = StrSql + "OPdepVOL.PCT9 PCT9, "
                StrSql = StrSql + "OPdepVOL.PCT10 PCT10, "
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
                StrSql = StrSql + "PREF.CONVWT, "
                StrSql = StrSql + "PREF.CONVTHICK2, "
                'ADDED FOR BUG#344
                StrSql = StrSql + "EQNUM.M1 AS NUM1, "
                StrSql = StrSql + "EQNUM.M2 AS NUM2, "
                StrSql = StrSql + "EQNUM.M3 AS NUM3, "
                StrSql = StrSql + "EQNUM.M4 AS NUM4, "
                StrSql = StrSql + "EQNUM.M5 AS NUM5, "
                StrSql = StrSql + "EQNUM.M6 AS NUM6, "
                StrSql = StrSql + "EQNUM.M7 AS NUM7, "
                StrSql = StrSql + "EQNUM.M8 AS NUM8, "
                StrSql = StrSql + "EQNUM.M9 AS NUM9, "
                StrSql = StrSql + "EQNUM.M10 AS NUM10, "
                StrSql = StrSql + "EQNUM.M11 AS NUM11, "
                StrSql = StrSql + "EQNUM.M12 AS NUM12, "
                StrSql = StrSql + "EQNUM.M13 AS NUM13, "
                StrSql = StrSql + "EQNUM.M14 AS NUM14, "
                StrSql = StrSql + "EQNUM.M15 AS NUM15, "
                StrSql = StrSql + "EQNUM.M16 AS NUM16, "
                StrSql = StrSql + "EQNUM.M17 AS NUM17, "
                StrSql = StrSql + "EQNUM.M18 AS NUM18, "
                StrSql = StrSql + "EQNUM.M19 AS NUM19, "
                StrSql = StrSql + "EQNUM.M20 AS NUM20, "
                StrSql = StrSql + "EQNUM.M21 AS NUM21, "
                StrSql = StrSql + "EQNUM.M22 AS NUM22, "
                StrSql = StrSql + "EQNUM.M23 AS NUM23, "
                StrSql = StrSql + "EQNUM.M24 AS NUM24, "
                StrSql = StrSql + "EQNUM.M25 AS NUM25, "
                StrSql = StrSql + "EQNUM.M26 AS NUM26, "
                StrSql = StrSql + "EQNUM.M27 AS NUM27, "
                StrSql = StrSql + "EQNUM.M28 AS NUM28, "
                StrSql = StrSql + "EQNUM.M29 AS NUM29, "
                StrSql = StrSql + "EQNUM.M30 AS NUM30 "
                'END BUG#344
                StrSql = StrSql + "FROM EQUIPMENTTYPE EQT "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ1 "
                StrSql = StrSql + "ON EQ1.EQUIPID=EQT.M1 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ2 "
                StrSql = StrSql + "ON EQ2.EQUIPID=EQT.M2 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ3 "
                StrSql = StrSql + "ON EQ3.EQUIPID=EQT.M3 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ4 "
                StrSql = StrSql + "ON EQ4.EQUIPID=EQT.M4 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ5 "
                StrSql = StrSql + "ON EQ5.EQUIPID=EQT.M5 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ6 "
                StrSql = StrSql + "ON EQ6.EQUIPID=EQT.M6 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ7 "
                StrSql = StrSql + "ON EQ7.EQUIPID=EQT.M7 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ8 "
                StrSql = StrSql + "ON EQ8.EQUIPID=EQT.M8 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ9 "
                StrSql = StrSql + "ON EQ9.EQUIPID=EQT.M9 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ10 "
                StrSql = StrSql + "ON EQ10.EQUIPID=EQT.M10 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ11 "
                StrSql = StrSql + "ON EQ11.EQUIPID=EQT.M11 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ12 "
                StrSql = StrSql + "ON EQ12.EQUIPID=EQT.M12 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ13 "
                StrSql = StrSql + "ON EQ13.EQUIPID=EQT.M13 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ14 "
                StrSql = StrSql + "ON EQ14.EQUIPID=EQT.M14 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ15 "
                StrSql = StrSql + "ON EQ15.EQUIPID=EQT.M15 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ16 "
                StrSql = StrSql + "ON EQ16.EQUIPID=EQT.M16 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ17 "
                StrSql = StrSql + "ON EQ17.EQUIPID=EQT.M17 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ18 "
                StrSql = StrSql + "ON EQ18.EQUIPID=EQT.M18 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ19 "
                StrSql = StrSql + "ON EQ19.EQUIPID=EQT.M19 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ20 "
                StrSql = StrSql + "ON EQ20.EQUIPID=EQT.M20 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ21 "
                StrSql = StrSql + "ON EQ21.EQUIPID=EQT.M21 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ22 "
                StrSql = StrSql + "ON EQ22.EQUIPID=EQT.M22 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ23 "
                StrSql = StrSql + "ON EQ23.EQUIPID=EQT.M23 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ24 "
                StrSql = StrSql + "ON EQ24.EQUIPID=EQT.M24 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ25 "
                StrSql = StrSql + "ON EQ25.EQUIPID=EQT.M25 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ26 "
                StrSql = StrSql + "ON EQ26.EQUIPID=EQT.M26 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ27 "
                StrSql = StrSql + "ON EQ27.EQUIPID=EQT.M27 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ28 "
                StrSql = StrSql + "ON EQ28.EQUIPID=EQT.M28 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ29 "
                StrSql = StrSql + "ON EQ29.EQUIPID=EQT.M29 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.EQUIPMENT EQ30 "
                StrSql = StrSql + "ON EQ30.EQUIPID=EQT.M30 "
                StrSql = StrSql + "INNER JOIN OPMAXRUNHRS "
                StrSql = StrSql + "ON OPMAXRUNHRS.CASEID=EQT.CASEID "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=EQT.CASEID "
                StrSql = StrSql + "INNER JOIN OPINSTGRSRATE "
                StrSql = StrSql + "ON OPINSTGRSRATE.CASEID=EQT.CASEID "
                StrSql = StrSql + "INNER JOIN TOTAL "
                StrSql = StrSql + "ON TOTAL.CASEID=EQT.CASEID "
                StrSql = StrSql + "INNER JOIN OPDOWNTIME OPDT "
                StrSql = StrSql + "ON OPDT.CASEID=EQT.CASEID "
                StrSql = StrSql + "INNER JOIN OPWASTE "
                StrSql = StrSql + "ON OPWASTE.CASEID=EQT.CASEID "
                StrSql = StrSql + "INNER JOIN equipmentDEP "
                StrSql = StrSql + "ON equipmentDEP.CASEID=EQT.CASEID "
                StrSql = StrSql + "INNER JOIN plantCONFIG "
                StrSql = StrSql + "ON plantCONFIG.CASEID=EQT.CASEID "
                StrSql = StrSql + "INNER JOIN OPdepVOL "
                StrSql = StrSql + "ON OPdepVOL.CASEID=EQT.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIPMENTNUMBER EQNUM "
                StrSql = StrSql + "ON EQNUM.CASEID=EQT.CASEID "
                StrSql = StrSql + "WHERE EQT.CASEID=" + CaseId.ToString() + ""
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetOperationInDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPersonnelInDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT  "
                StrSql = StrSql + "PERSONNELPOS.CASEID, "
                StrSql = StrSql + "PERSONNELPOS.M1  AS ID1, "
                StrSql = StrSql + "PERSONNELPOS.M2  AS ID2, "
                StrSql = StrSql + "PERSONNELPOS.M3  AS ID3, "
                StrSql = StrSql + "PERSONNELPOS.M4  AS ID4, "
                StrSql = StrSql + "PERSONNELPOS.M5  AS ID5, "
                StrSql = StrSql + "PERSONNELPOS.M6  AS ID6, "
                StrSql = StrSql + "PERSONNELPOS.M7  AS ID7, "
                StrSql = StrSql + "PERSONNELPOS.M8  AS ID8, "
                StrSql = StrSql + "PERSONNELPOS.M9  AS ID9, "
                StrSql = StrSql + "PERSONNELPOS.M10  AS ID10, "
                StrSql = StrSql + "PERSONNELPOS.M11  AS ID11, "
                StrSql = StrSql + "PERSONNELPOS.M12  AS ID12, "
                StrSql = StrSql + "PERSONNELPOS.M13  AS ID13, "
                StrSql = StrSql + "PERSONNELPOS.M14  AS ID14, "
                StrSql = StrSql + "PERSONNELPOS.M15  AS ID15, "
                StrSql = StrSql + "PERSONNELPOS.M16  AS ID16, "
                StrSql = StrSql + "PERSONNELPOS.M17  AS ID17, "
                StrSql = StrSql + "PERSONNELPOS.M18  AS ID18, "
                StrSql = StrSql + "PERSONNELPOS.M19  AS ID19, "
                StrSql = StrSql + "PERSONNELPOS.M20  AS ID20, "
                StrSql = StrSql + "PERSONNELPOS.M21  AS ID21, "
                StrSql = StrSql + "PERSONNELPOS.M22  AS ID22, "
                StrSql = StrSql + "PERSONNELPOS.M23  AS ID23, "
                StrSql = StrSql + "PERSONNELPOS.M24  AS ID24, "
                StrSql = StrSql + "PERSONNELPOS.M25  AS ID25, "
                StrSql = StrSql + "PERSONNELPOS.M26  AS ID26, "
                StrSql = StrSql + "PERSONNELPOS.M27  AS ID27, "
                StrSql = StrSql + "PERSONNELPOS.M28  AS ID28, "
                StrSql = StrSql + "PERSONNELPOS.M29  AS ID29, "
                StrSql = StrSql + "PERSONNELPOS.M30  AS ID30, "
                StrSql = StrSql + "PERSONNELNUM.M1  AS NUMBER1, "
                StrSql = StrSql + "PERSONNELNUM.M2  AS NUMBER2, "
                StrSql = StrSql + "PERSONNELNUM.M3  AS NUMBER3, "
                StrSql = StrSql + "PERSONNELNUM.M4  AS NUMBER4, "
                StrSql = StrSql + "PERSONNELNUM.M5  AS NUMBER5, "
                StrSql = StrSql + "PERSONNELNUM.M6  AS NUMBER6, "
                StrSql = StrSql + "PERSONNELNUM.M7  AS NUMBER7, "
                StrSql = StrSql + "PERSONNELNUM.M8  AS NUMBER8, "
                StrSql = StrSql + "PERSONNELNUM.M9  AS NUMBER9, "
                StrSql = StrSql + "PERSONNELNUM.M10  AS NUMBER10, "
                StrSql = StrSql + "PERSONNELNUM.M11  AS NUMBER11, "
                StrSql = StrSql + "PERSONNELNUM.M12  AS NUMBER12, "
                StrSql = StrSql + "PERSONNELNUM.M13  AS NUMBER13, "
                StrSql = StrSql + "PERSONNELNUM.M14  AS NUMBER14, "
                StrSql = StrSql + "PERSONNELNUM.M15  AS NUMBER15, "
                StrSql = StrSql + "PERSONNELNUM.M16  AS NUMBER16, "
                StrSql = StrSql + "PERSONNELNUM.M17  AS NUMBER17, "
                StrSql = StrSql + "PERSONNELNUM.M18  AS NUMBER18, "
                StrSql = StrSql + "PERSONNELNUM.M19  AS NUMBER19, "
                StrSql = StrSql + "PERSONNELNUM.M20  AS NUMBER20, "
                StrSql = StrSql + "PERSONNELNUM.M21  AS NUMBER21, "
                StrSql = StrSql + "PERSONNELNUM.M22  AS NUMBER22, "
                StrSql = StrSql + "PERSONNELNUM.M23  AS NUMBER23, "
                StrSql = StrSql + "PERSONNELNUM.M24  AS NUMBER24, "
                StrSql = StrSql + "PERSONNELNUM.M25  AS NUMBER25, "
                StrSql = StrSql + "PERSONNELNUM.M26  AS NUMBER26, "
                StrSql = StrSql + "PERSONNELNUM.M27  AS NUMBER27, "
                StrSql = StrSql + "PERSONNELNUM.M28  AS NUMBER28, "
                StrSql = StrSql + "PERSONNELNUM.M29  AS NUMBER29, "
                StrSql = StrSql + "PERSONNELNUM.M30  AS NUMBER30, "
                StrSql = StrSql + "PERSONNELDEP.M1  AS DEP1, "
                StrSql = StrSql + "PERSONNELDEP.M2  AS DEP2, "
                StrSql = StrSql + "PERSONNELDEP.M3  AS DEP3, "
                StrSql = StrSql + "PERSONNELDEP.M4  AS DEP4, "
                StrSql = StrSql + "PERSONNELDEP.M5  AS DEP5, "
                StrSql = StrSql + "PERSONNELDEP.M6  AS DEP6, "
                StrSql = StrSql + "PERSONNELDEP.M7  AS DEP7, "
                StrSql = StrSql + "PERSONNELDEP.M8  AS DEP8, "
                StrSql = StrSql + "PERSONNELDEP.M9  AS DEP9, "
                StrSql = StrSql + "PERSONNELDEP.M10  AS DEP10, "
                StrSql = StrSql + "PERSONNELDEP.M11  AS DEP11, "
                StrSql = StrSql + "PERSONNELDEP.M12  AS DEP12, "
                StrSql = StrSql + "PERSONNELDEP.M13  AS DEP13, "
                StrSql = StrSql + "PERSONNELDEP.M14  AS DEP14, "
                StrSql = StrSql + "PERSONNELDEP.M15  AS DEP15, "
                StrSql = StrSql + "PERSONNELDEP.M16  AS DEP16, "
                StrSql = StrSql + "PERSONNELDEP.M17  AS DEP17, "
                StrSql = StrSql + "PERSONNELDEP.M18  AS DEP18, "
                StrSql = StrSql + "PERSONNELDEP.M19  AS DEP19, "
                StrSql = StrSql + "PERSONNELDEP.M20  AS DEP20, "
                StrSql = StrSql + "PERSONNELDEP.M21  AS DEP21, "
                StrSql = StrSql + "PERSONNELDEP.M22  AS DEP22, "
                StrSql = StrSql + "PERSONNELDEP.M23  AS DEP23, "
                StrSql = StrSql + "PERSONNELDEP.M24  AS DEP24, "
                StrSql = StrSql + "PERSONNELDEP.M25  AS DEP25, "
                StrSql = StrSql + "PERSONNELDEP.M26  AS DEP26, "
                StrSql = StrSql + "PERSONNELDEP.M27  AS DEP27, "
                StrSql = StrSql + "PERSONNELDEP.M28  AS DEP28, "
                StrSql = StrSql + "PERSONNELDEP.M29  AS DEP29, "
                StrSql = StrSql + "PERSONNELDEP.M30  AS DEP30, "
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
                StrSql = StrSql + "DECODE (PREF.OCOUNTRY ,0 , 'PERSONNEL', "
                StrSql = StrSql + "1 , 'PERSONNELCHINA', "
                StrSql = StrSql + "2 , 'PERSONNEL' "
                StrSql = StrSql + ")COUNTRY "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "PERSONNELPOS "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID =PERSONNELPOS.CASEID "
                StrSql = StrSql + "INNER JOIN PERSONNELNUM "
                StrSql = StrSql + "ON 	PERSONNELNUM.CASEID = PERSONNELPOS.CASEID "
                StrSql = StrSql + "INNER JOIN PERSONNELDEP "
                StrSql = StrSql + "ON PERSONNELDEP.CASEID = PERSONNELPOS.CASEID "
                StrSql = StrSql + "WHERE PERSONNELPOS.CASEID =" + CaseId.ToString() + " ORDER BY PERSONNELPOS.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetPersonnelInDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPlantConfig2Details(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT  "
                StrSql = StrSql + "PLANTSPACE.CASEID, "
                StrSql = StrSql + "'Production' AS SPACETYPE1, "
                StrSql = StrSql + "'Warehouse' AS SPACETYPE2, "
                StrSql = StrSql + "'Office' AS SPACETYPE3, "
                StrSql = StrSql + "'Support' AS SPACETYPE4, "
                StrSql = StrSql + "(PLANTSPACE.M1*PREF.CONVAREA2) AS AREA1, "
                StrSql = StrSql + "(PLANTSPACE.M2*PREF.CONVAREA2) AS AREA2, "
                StrSql = StrSql + "(PLANTSPACE.M3*PREF.CONVAREA2) AS AREA3, "
                StrSql = StrSql + "(PLANTSPACE.M4*PREF.CONVAREA2) AS AREA4, "
                StrSql = StrSql + "(PLANTSPACE.M5*PREF.CONVAREA2) AS TOTALAREA, "
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
                StrSql = StrSql + "PLANTSPACE "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "PREFERENCES PREF "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "PREF.CASEID = PLANTSPACE. CASEID "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "PLANTSPACE.CASEID=" + CaseId.ToString() + " ORDER BY CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetPlantConfig2Details:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetEnergyDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT  "
                StrSql = StrSql + "PLANTENERGY.CASEID, "
                StrSql = StrSql + "'Electricity' AS ENERGYTYPE1, "
                StrSql = StrSql + "'Natural Gas' AS  ENERGYTYPE2, "
                StrSql = StrSql + "(SELECT PRICE FROM SUSTAIN.ENERGY WHERE ENERGYID =1) AS  ERS1, "
                StrSql = StrSql + "(SELECT PRICE FROM SUSTAIN.ENERGY WHERE ENERGYID =2) AS  ERS2, "
                StrSql = StrSql + "PLANTENERGY.ELECPRICE AS ERP1, "
                StrSql = StrSql + "PLANTENERGY.NGASPRICE AS ERP2, "
                StrSql = StrSql + "'Mj Per Kwh' AS CONVERSIONFACT1, "
                StrSql = StrSql + "'Mj Per Cubicft' AS CONVERSIONFACT2, "
                StrSql = StrSql + "'Co2' || PREF.TITLE8 || ' Per Kwh' AS CONVERSIONFACT3, "
                StrSql = StrSql + "'Co2' || PREF.TITLE8 || ' Cubicft' AS CONVERSIONFACT4, "
                'Water Started
                StrSql = StrSql + " PREF.TITLE10 || ' Per Kwh' AS CONVERSIONFACT5, "
                StrSql = StrSql + " PREF.TITLE10 || ' Per Cubicft' AS CONVERSIONFACT6, "
                'Water End

                StrSql = StrSql + "(SELECT MJPKWH FROM ECON.CONVFACTORS2) AS CFACTORSUG1, "
                StrSql = StrSql + "(SELECT MJPCUBICFT FROM ECON.CONVFACTORS2) AS CFACTORSUG2, "
                StrSql = StrSql + "(SELECT CO2LBPKWH FROM ECON.CONVFACTORS2)*PREF.CONVWT AS CFACTORSUG3, "
                StrSql = StrSql + "(SELECT CO2LBPCUBICFT FROM ECON.CONVFACTORS2)*PREF.CONVWT AS CFACTORSUG4, "
                'Water Started
                StrSql = StrSql + "(SELECT WATERGALPKWH FROM ECON.CONVFACTORS2)*PREF.CONVGALLON AS CFACTORSUG5, "
                StrSql = StrSql + "(SELECT WATERGALPCUBICFT FROM ECON.CONVFACTORS2)*PREF.CONVGALLON AS CFACTORSUG6, "
                'Water End
                StrSql = StrSql + "PLANTCO2.P1*PREF.CONVWT CFACTORPREF3, "
                StrSql = StrSql + "PLANTWATER.P1*PREF.CONVGALLON CFACTORPREF5, "

                StrSql = StrSql + "'Production' AS SpaceType1, "
                StrSql = StrSql + "'Warehouse'  AS SpaceType2, "
                StrSql = StrSql + "'Office'  AS SpaceType3, "
                StrSql = StrSql + "'Support' AS SpaceType4, "
                StrSql = StrSql + "PLANTENERGY.M1 AS ELECA1, "
                StrSql = StrSql + "PLANTENERGY.M2 AS ELECA2, "
                StrSql = StrSql + "PLANTENERGY.M3 AS ELECA3, "
                StrSql = StrSql + "PLANTENERGY.M4 AS ELECA4, "
                StrSql = StrSql + "PLANTENERGY.M5 AS ELECA5, "
                StrSql = StrSql + "PLANTENERGY.M6 AS NGASA1, "
                StrSql = StrSql + "PLANTENERGY.M7  AS NGASA2, "
                StrSql = StrSql + "PLANTENERGY.M8  AS NGASA3, "
                StrSql = StrSql + "PLANTENERGY.M9  AS NGASA4, "
                StrSql = StrSql + "PLANTENERGY.M10 AS NGASA5, "
                StrSql = StrSql + "PLANTENERGY.D1 AS ELECB1, "
                StrSql = StrSql + "PLANTENERGY.D2 AS ELECB2, "
                StrSql = StrSql + "PLANTENERGY.D3 AS ELECB3, "
                StrSql = StrSql + "PLANTENERGY.D4 AS ELECB4, "
                StrSql = StrSql + "PLANTENERGY.D5 AS ELECB5, "
                StrSql = StrSql + "PLANTENERGY.D6 AS NGASB1, "
                StrSql = StrSql + "PLANTENERGY.D7 AS NGASB2, "
                StrSql = StrSql + "PLANTENERGY.D8 AS NGASB3, "
                StrSql = StrSql + "PLANTENERGY.D9 AS NGASB4, "
                StrSql = StrSql + "PLANTENERGY.D10 AS NGASB5, "
                StrSql = StrSql + "PLANTENERGY.D11 AS TOTALA1, "
                StrSql = StrSql + "PLANTENERGY.D12 AS TOTALA2, "
                StrSql = StrSql + "PLANTENERGY.D13 AS TOTALA3, "
                StrSql = StrSql + "PLANTENERGY.D14 AS TOTALA4, "
                StrSql = StrSql + "PLANTENERGY.D15 AS TOTALA5, "

                StrSql = StrSql + "PLANTENERGY.E1 AS NRELEC1, "
                StrSql = StrSql + "PLANTENERGY.E2 AS NRELEC2, "
                StrSql = StrSql + "PLANTENERGY.E3 AS NRELEC3, "
                StrSql = StrSql + "PLANTENERGY.E4 AS NRELEC4, "
                StrSql = StrSql + "PLANTENERGY.E5 AS NRELEC5, "
                StrSql = StrSql + "PLANTENERGY.E6 AS RELEC1, "
                StrSql = StrSql + "PLANTENERGY.E7 AS RELEC2, "
                StrSql = StrSql + "PLANTENERGY.E8 AS RELEC3, "
                StrSql = StrSql + "PLANTENERGY.E9 AS RELEC4, "
                StrSql = StrSql + "PLANTENERGY.E10 AS RELEC5, "
                StrSql = StrSql + "PLANTENERGY.E11 AS TOTALNR1, "
                StrSql = StrSql + "PLANTENERGY.E12 AS TOTALNR2, "
                StrSql = StrSql + "PLANTENERGY.E13 AS TOTALNR3, "
                StrSql = StrSql + "PLANTENERGY.E14 AS TOTALNR4, "
                StrSql = StrSql + "PLANTENERGY.E15 AS TOTALNR5, "


                StrSql = StrSql + "(PLANTCO2.D1*PREF.CONVWT) AS CO2ELEC1 , "
                StrSql = StrSql + "(PLANTCO2.D2*PREF.CONVWT) AS CO2ELEC2, "
                StrSql = StrSql + "(PLANTCO2.D3*PREF.CONVWT) AS CO2ELEC3, "
                StrSql = StrSql + "(PLANTCO2.D4*PREF.CONVWT) AS CO2ELEC4, "
                StrSql = StrSql + "(PLANTCO2.D5*PREF.CONVWT) AS CO2ELEC5, "
                StrSql = StrSql + "(PLANTCO2.D6*PREF.CONVWT) AS CO2NG1, "
                StrSql = StrSql + "(PLANTCO2.D7*PREF.CONVWT) AS CO2NG2, "
                StrSql = StrSql + "(PLANTCO2.D8*PREF.CONVWT) AS CO2NG3, "
                StrSql = StrSql + "(PLANTCO2.D9*PREF.CONVWT) AS CO2NG4, "
                StrSql = StrSql + "(PLANTCO2.D10*PREF.CONVWT) AS CO2NG5, "
                StrSql = StrSql + "(PLANTCO2.D11*PREF.CONVWT) AS TOTALB1, "
                StrSql = StrSql + "(PLANTCO2.D12*PREF.CONVWT) AS TOTALB2, "
                StrSql = StrSql + "(PLANTCO2.D13*PREF.CONVWT) AS TOTALB3, "
                StrSql = StrSql + "(PLANTCO2.D14*PREF.CONVWT) AS TOTALB4, "
                StrSql = StrSql + "(PLANTCO2.D15*PREF.CONVWT) AS TOTALB5, "

                'Change for Water
                StrSql = StrSql + "PLANTWATER.W1*PREF.CONVGALLON AS ENWATERA1, "
                StrSql = StrSql + "PLANTWATER.W2*PREF.CONVGALLON AS ENWATERA2, "
                StrSql = StrSql + "PLANTWATER.W3*PREF.CONVGALLON AS ENWATERA3, "
                StrSql = StrSql + "PLANTWATER.W4*PREF.CONVGALLON AS ENWATERA4, "
                StrSql = StrSql + "PLANTWATER.W5*PREF.CONVGALLON AS ENWATERA5, "

                StrSql = StrSql + "(PLANTWATER.W1*PREF.CONVGALLON) AS ENWATERB1, "
                StrSql = StrSql + "(PLANTWATER.W2*PREF.CONVGALLON) AS ENWATERB2, "
                StrSql = StrSql + "(PLANTWATER.W3*PREF.CONVGALLON) AS ENWATERB3, "
                StrSql = StrSql + "(PLANTWATER.W4*PREF.CONVGALLON) AS ENWATERB4, "
                StrSql = StrSql + "(PLANTWATER.W5*PREF.CONVGALLON) AS ENWATERB5, "

                StrSql = StrSql + "(PLANTWATER.D1*PREF.CONVGALLON) AS WATERELEC1 , "
                StrSql = StrSql + "(PLANTWATER.D2*PREF.CONVGALLON) AS WATERELEC2, "
                StrSql = StrSql + "(PLANTWATER.D3*PREF.CONVGALLON) AS WATERELEC3, "
                StrSql = StrSql + "(PLANTWATER.D4*PREF.CONVGALLON) AS WATERELEC4, "
                StrSql = StrSql + "(PLANTWATER.D5*PREF.CONVGALLON) AS WATERELEC5, "
                StrSql = StrSql + "(PLANTWATER.D6*PREF.CONVGALLON) AS WATERNG1, "
                StrSql = StrSql + "(PLANTWATER.D7*PREF.CONVGALLON) AS WATERNG2, "
                StrSql = StrSql + "(PLANTWATER.D8*PREF.CONVGALLON) AS WATERNG3, "
                StrSql = StrSql + "(PLANTWATER.D9*PREF.CONVGALLON) AS WATERNG4, "
                StrSql = StrSql + "(PLANTWATER.D10*PREF.CONVGALLON) AS WATERNG5, "
                StrSql = StrSql + "(PLANTWATER.D11*PREF.CONVGALLON) AS TOTALC1, "
                StrSql = StrSql + "(PLANTWATER.D12*PREF.CONVGALLON) AS TOTALC2, "
                StrSql = StrSql + "(PLANTWATER.D13*PREF.CONVGALLON) AS TOTALC3, "
                StrSql = StrSql + "(PLANTWATER.D14*PREF.CONVGALLON) AS TOTALC4, "
                StrSql = StrSql + "(PLANTWATER.D15*PREF.CONVGALLON) AS TOTALC5, "
                'Change for Water
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
                StrSql = StrSql + "FROM PLANTENERGY "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = PLANTENERGY.CASEID "
                StrSql = StrSql + "INNER JOIN PLANTCO2 "
                StrSql = StrSql + "ON PLANTCO2.CASEID = PLANTENERGY.CASEID "
                'Water Started
                StrSql = StrSql + "INNER JOIN PLANTWATER "
                StrSql = StrSql + "ON PLANTWATER.CASEID = PLANTENERGY.CASEID "
                'Water End
                StrSql = StrSql + "WHERE PLANTENERGY.CASEID =" + CaseId.ToString() + " ORDER BY PLANTENERGY.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetEnergyDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetAdditionalEnergyInfo(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PREF.CASEID,  "
                StrSql = StrSql + "'Production' AS SPACETYPE1, "
                StrSql = StrSql + "'Warehouse' AS SPACETYPE2, "
                StrSql = StrSql + "'Office' AS SPACETYPE3, "
                StrSql = StrSql + "'Support' AS SPACETYPE4, "
                StrSql = StrSql + "AT1.ELECPERAREA/PREF.CONVAREA2 AS ENERGYCONSUMAS1, "
                StrSql = StrSql + "AT2.ELECPERAREA/PREF.CONVAREA2 AS ENERGYCONSUMAS2, "
                StrSql = StrSql + "AT3.ELECPERAREA/PREF.CONVAREA2 AS ENERGYCONSUMAS3, "
                StrSql = StrSql + "SPACEENERGYPREFPERSQFT.P4/PREF.CONVAREA2 AS ENERGYCONSUMAP1, "
                StrSql = StrSql + "SPACEENERGYPREFPERSQFT.P5/PREF.CONVAREA2 AS ENERGYCONSUMAP2, "
                StrSql = StrSql + "SPACEENERGYPREFPERSQFT.P6/PREF.CONVAREA2 AS ENERGYCONSUMAP3, "
                StrSql = StrSql + "AT4.GASPERAREA/PREF.CONVAREA2 AS  ENERGYCONSUMBS1, "
                StrSql = StrSql + "AT1.GASPERAREA/PREF.CONVAREA2 AS  ENERGYCONSUMBS2, "
                StrSql = StrSql + "AT2.GASPERAREA/PREF.CONVAREA2 AS  ENERGYCONSUMBS3, "
                StrSql = StrSql + "AT3.GASPERAREA/PREF.CONVAREA2 AS  ENERGYCONSUMBS4, "
                StrSql = StrSql + "SPACEENERGYPREFPERSQFT.P7/PREF.CONVAREA2 AS ENERGYCONSUMBP1, "
                StrSql = StrSql + "SPACEENERGYPREFPERSQFT.P10/PREF.CONVAREA2 AS ENERGYCONSUMBP2, "
                StrSql = StrSql + "SPACEENERGYPREFPERSQFT.P11/PREF.CONVAREA2 AS ENERGYCONSUMBP3, "
                StrSql = StrSql + "SPACEENERGYPREFPERSQFT.P12/PREF.CONVAREA2 AS ENERGYCONSUMBP4, "
                'Water Started
                StrSql = StrSql + "SPACEENERGYPREFPERSQFT.P13*PREF.CONVGALLON/PREF.CONVAREA2 AS ENERGYCONSUMCP1, "
                StrSql = StrSql + "SPACEENERGYPREFPERSQFT.P14*PREF.CONVGALLON/PREF.CONVAREA2 AS ENERGYCONSUMCP2, "
                StrSql = StrSql + "SPACEENERGYPREFPERSQFT.P15*PREF.CONVGALLON/PREF.CONVAREA2 AS ENERGYCONSUMCP3, "
                StrSql = StrSql + "SPACEENERGYPREFPERSQFT.P16*PREF.CONVGALLON/PREF.CONVAREA2 AS ENERGYCONSUMCP4, "

                StrSql = StrSql + "AT4.WATERPERAREA*PREF.CONVGALLON/PREF.CONVAREA2 AS  ENERGYCONSUMCS1, "
                StrSql = StrSql + "AT1.WATERPERAREA*PREF.CONVGALLON/PREF.CONVAREA2 AS  ENERGYCONSUMCS2, "
                StrSql = StrSql + "AT2.WATERPERAREA*PREF.CONVGALLON/PREF.CONVAREA2 AS  ENERGYCONSUMCS3, "
                StrSql = StrSql + "AT3.WATERPERAREA*PREF.CONVGALLON/PREF.CONVAREA2 AS  ENERGYCONSUMCS4, "
                'Water End

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
                StrSql = StrSql + "FROM PREFERENCES PREF "
                StrSql = StrSql + "INNER JOIN ECON.AREATYPE AT1 "
                StrSql = StrSql + "ON AT1.AREAID=4 "
                StrSql = StrSql + "INNER  JOIN ECON.AREATYPE AT2 "
                StrSql = StrSql + "ON AT2.AREAID=5 "
                StrSql = StrSql + "INNER  JOIN ECON.AREATYPE AT3 "
                StrSql = StrSql + "ON AT3.AREAID=6 "
                StrSql = StrSql + "INNER  JOIN ECON.AREATYPE AT4 "
                StrSql = StrSql + "ON AT4.AREAID=1 "
                StrSql = StrSql + "INNER JOIN SPACEENERGYPREFPERSQFT "
                StrSql = StrSql + "ON SPACEENERGYPREFPERSQFT.CASEID=PREF.CASEID "
                StrSql = StrSql + "WHERE PREF.CASEID=" + CaseId.ToString()
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetAdditionalEnergyInfo:" + ex.Message.ToString())
                Return Dts

            End Try
        End Function

        Public Function GetTransportDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT  "
                StrSql = StrSql + "CUSTOMERIN.CASEID, "
                StrSql = StrSql + "(CUSTOMERIN.M2*PREF.CONVTHICK3)SDTOCUST, "
                StrSql = StrSql + "((CNVF.TFUELEFF*PREF.CONVTHICK3)/PREF.CONVGALLON)TFUELEFFS, "
                StrSql = StrSql + "((CUSTOMERIN.M3*PREF.CONVTHICK3)/PREF.CONVGALLON)TFUELEFFP, "
                StrSql = StrSql + "((CNVF.RFUELEFF*PREF.CONVTHICK3)/PREF.CONVGALLON)RFUELEFFS, "
                StrSql = StrSql + "((CUSTOMERIN.M4*PREF.CONVTHICK3)/PREF.CONVGALLON)RFUELEFFP, "
                StrSql = StrSql + "(CNVF.ERGY/PREF.CONVGALLON) AS GasolineEnergy, "
                StrSql = StrSql + "((CNVF.CO2*PREF.CONVWT)/PREF.CONVGALLON) AS GasolineCO2, "
                'Water Started
                StrSql = StrSql + "(CNVF.WATER) AS GasolineWATER, "
                'Water Ended
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
                StrSql = StrSql + "FROM CUSTOMERIN "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = CUSTOMERIN.CASEID "
                StrSql = StrSql + "INNER JOIN ECON.CONVFACTORS2 CNVF "
                StrSql = StrSql + "ON 1 = 1 "
                StrSql = StrSql + "WHERE CUSTOMERIN.CASEID =" + CaseId.ToString() + " ORDER BY CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetTransportDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetExtraProcessDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "FIXEDCOSTSUG.CASEID, "
                StrSql = StrSql + "'Plant Waste:' AS CATEGORY1, "
                StrSql = StrSql + "'Office Waste:' AS CATEGORY2, "
                StrSql = StrSql + "(FIXEDCOSTPCT.M1*PREF.CONVWT)EP1, "
                StrSql = StrSql + "(FIXEDCOSTPCT.M2*PREF.CONVWT)EP2, "

                StrSql = StrSql + "'weight per employee' AS WASTERULE1, "
                StrSql = StrSql + "'weight per employee' AS WASTERULE2, "

                StrSql = StrSql + "(FIXEDCOSTSUG.M1*PREF.CONVWT)SUG1, "
                StrSql = StrSql + "(FIXEDCOSTSUG.M2*PREF.CONVWT)SUG2, "
                StrSql = StrSql + "(FIXEDCOSTPRE.M1*PREF.CONVWT)PREF1, "
                StrSql = StrSql + "(FIXEDCOSTPRE.M2*PREF.CONVWT)PREF2, "
                StrSql = StrSql + "FIXEDCOSTDEP.M1 DEP1, "
                StrSql = StrSql + "FIXEDCOSTDEP.M2 DEP2, "

                'Water Started
                StrSql = StrSql + "'Water:' AS CATEGORY3, "
                StrSql = StrSql + "(FIXEDCOSTPCT.M3*PREF.CONVGALLON)EP3, "
                StrSql = StrSql + "'volume per employee' AS WASTERULE3, "
                StrSql = StrSql + "(FIXEDCOSTSUG.M3*PREF.CONVGALLON)SUG3, "
                StrSql = StrSql + "(FIXEDCOSTPRE.M3*PREF.CONVGALLON)PREF3, "
                StrSql = StrSql + "FIXEDCOSTDEP.M3 DEP3, "
                'Water End
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
                StrSql = StrSql + "FROM FIXEDCOSTSUG "
                StrSql = StrSql + "INNER JOIN FIXEDCOSTPCT "
                StrSql = StrSql + "ON FIXEDCOSTPCT.CASEID  = FIXEDCOSTSUG.CASEID "
                StrSql = StrSql + "INNER JOIN FIXEDCOSTPRE "
                StrSql = StrSql + "ON FIXEDCOSTPRE.CASEID  = FIXEDCOSTSUG.CASEID "
                StrSql = StrSql + "INNER JOIN FIXEDCOSTDEP "
                StrSql = StrSql + "ON FIXEDCOSTDEP.CASEID  = FIXEDCOSTSUG.CASEID "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = FIXEDCOSTSUG.CASEID "
                StrSql = StrSql + "WHERE FIXEDCOSTSUG.CASEID =" + CaseId.ToString() + " ORDER BY FIXEDCOSTSUG.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetExtraProcessDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

#End Region

#Region "Intermediate result Pages"
        Public Function GetExtrusionOutDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "MAT.CASEID, "
                StrSql = StrSql + "(MAT1.MATDE1||' '||MAT1.MATDE2) AS MATDE1, "
                StrSql = StrSql + "(MAT2.MATDE1||' '||MAT2.MATDE2) AS MATDE2, "
                StrSql = StrSql + "(MAT3.MATDE1||' '||MAT3.MATDE2) AS MATDE3, "
                StrSql = StrSql + "(MAT4.MATDE1||' '||MAT4.MATDE2) AS MATDE4, "
                StrSql = StrSql + "(MAT5.MATDE1||' '||MAT5.MATDE2) AS MATDE5, "
                StrSql = StrSql + "(MAT6.MATDE1||' '||MAT6.MATDE2) AS MATDE6, "
                StrSql = StrSql + "(MAT7.MATDE1||' '||MAT7.MATDE2) AS MATDE7, "
                StrSql = StrSql + "(MAT8.MATDE1||' '||MAT8.MATDE2) AS MATDE8, "
                StrSql = StrSql + "(MAT9.MATDE1||' '||MAT9.MATDE2) AS MATDE9, "
                StrSql = StrSql + "(MAT10.MATDE1||' '||MAT10.MATDE2) AS MATDE10, "
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
                StrSql = StrSql + "MAT1.SG AS SG1, "
                StrSql = StrSql + "MAT2.SG AS SG2, "
                StrSql = StrSql + "MAT3.SG AS SG3, "
                StrSql = StrSql + "MAT4.SG AS SG4, "
                StrSql = StrSql + "MAT5.SG AS SG5, "
                StrSql = StrSql + "MAT6.SG AS SG6, "
                StrSql = StrSql + "MAT7.SG AS SG7, "
                StrSql = StrSql + "MAT8.SG AS SG8, "
                StrSql = StrSql + "MAT9.SG AS SG9, "
                StrSql = StrSql + "MAT10.SG AS SG10, "
                StrSql = StrSql + "MATOUT.P1 AS W1, "
                StrSql = StrSql + "MATOUT.P2 AS W2, "
                StrSql = StrSql + "MATOUT.P3 AS W3, "
                StrSql = StrSql + "MATOUT.P4 AS W4, "
                StrSql = StrSql + "MATOUT.P5 AS W5, "
                StrSql = StrSql + "MATOUT.P6 AS W6, "
                StrSql = StrSql + "MATOUT.P7 AS W7, "
                StrSql = StrSql + "MATOUT.P8 AS W8, "
                StrSql = StrSql + "MATOUT.P9 AS W9, "
                StrSql = StrSql + "MATOUT.P10 AS W10, "
                StrSql = StrSql + "(MATOUT.PUR1*PREF.CONVWT)AS PUR1, "
                StrSql = StrSql + "(MATOUT.PUR2*PREF.CONVWT)AS PUR2, "
                StrSql = StrSql + "(MATOUT.PUR3*PREF.CONVWT)AS PUR3, "
                StrSql = StrSql + "(MATOUT.PUR4*PREF.CONVWT)AS PUR4, "
                StrSql = StrSql + "(MATOUT.PUR5*PREF.CONVWT)AS PUR5, "
                StrSql = StrSql + "(MATOUT.PUR6*PREF.CONVWT)AS PUR6, "
                StrSql = StrSql + "(MATOUT.PUR7*PREF.CONVWT)AS PUR7, "
                StrSql = StrSql + "(MATOUT.PUR8*PREF.CONVWT)AS PUR8, "
                StrSql = StrSql + "(MATOUT.PUR9*PREF.CONVWT)AS PUR9, "
                StrSql = StrSql + "(MATOUT.PUR10*PREF.CONVWT)AS PUR10, "
                StrSql = StrSql + "(MATOUT.PURZ1)AS ERGY1, "
                StrSql = StrSql + "(MATOUT.PURZ2)AS ERGY2, "
                StrSql = StrSql + "(MATOUT.PURZ3)AS ERGY3, "
                StrSql = StrSql + "(MATOUT.PURZ4)AS ERGY4, "
                StrSql = StrSql + "(MATOUT.PURZ5)AS ERGY5, "
                StrSql = StrSql + "(MATOUT.PURZ6)AS ERGY6, "
                StrSql = StrSql + "(MATOUT.PURZ7)AS ERGY7, "
                StrSql = StrSql + "(MATOUT.PURZ8)AS ERGY8, "
                StrSql = StrSql + "(MATOUT.PURZ9)AS ERGY9, "
                StrSql = StrSql + "(MATOUT.PURZ10)AS ERGY10, "
                StrSql = StrSql + "(MATOUT.G1*PREF.CONVWT) AS G1, "
                StrSql = StrSql + "(MATOUT.G2*PREF.CONVWT) AS G2, "
                StrSql = StrSql + "(MATOUT.G3*PREF.CONVWT) AS G3, "
                StrSql = StrSql + "(MATOUT.G4*PREF.CONVWT) AS G4, "
                StrSql = StrSql + "(MATOUT.G5*PREF.CONVWT) AS G5, "
                StrSql = StrSql + "(MATOUT.G6*PREF.CONVWT) AS G6, "
                StrSql = StrSql + "(MATOUT.G7*PREF.CONVWT) AS G7, "
                StrSql = StrSql + "(MATOUT.G8*PREF.CONVWT) AS G8, "
                StrSql = StrSql + "(MATOUT.G9*PREF.CONVWT) AS G9, "
                StrSql = StrSql + "(MATOUT.G10*PREF.CONVWT) AS G10, "
                'Water Started
                StrSql = StrSql + "(MATOUT.PURW1*PREF.CONVGALLON)AS WATER1, "
                StrSql = StrSql + "(MATOUT.PURW2*PREF.CONVGALLON)AS WATER2, "
                StrSql = StrSql + "(MATOUT.PURW3*PREF.CONVGALLON)AS WATER3, "
                StrSql = StrSql + "(MATOUT.PURW4*PREF.CONVGALLON)AS WATER4, "
                StrSql = StrSql + "(MATOUT.PURW5*PREF.CONVGALLON)AS WATER5, "
                StrSql = StrSql + "(MATOUT.PURW6*PREF.CONVGALLON)AS WATER6, "
                StrSql = StrSql + "(MATOUT.PURW7*PREF.CONVGALLON)AS WATER7, "
                StrSql = StrSql + "(MATOUT.PURW8*PREF.CONVGALLON)AS WATER8, "
                StrSql = StrSql + "(MATOUT.PURW9*PREF.CONVGALLON)AS WATER9, "
                StrSql = StrSql + "(MATOUT.PURW10*PREF.CONVGALLON)AS WATER10, "
                'Water Ended

                StrSql = StrSql + "TOTAL.SG AS TOTSG, "
                StrSql = StrSql + "(MATOUT.P1+ MATOUT.P2 + MATOUT.P3 + MATOUT.P4 + MATOUT.P5 + MATOUT.P6 + MATOUT.P7 + MATOUT.P8 + MATOUT.P9 + MATOUT.P10) AS TOTWEIGHT, "
                StrSql = StrSql + "(MATOUT.PUR1+ MATOUT.PUR2 + MATOUT.PUR3 + MATOUT.PUR4 + MATOUT.PUR5 + MATOUT.PUR6 + MATOUT.PUR7 + MATOUT.PUR8 + MATOUT.PUR9 + MATOUT.PUR10)*PREF.CONVWT AS TOTPUR, "
                StrSql = StrSql + "(MATOUT.PURZ1+ MATOUT.PURZ2 + MATOUT.PURZ3 + MATOUT.PURZ4 + MATOUT.PURZ5 + MATOUT.PURZ6 + MATOUT.PURZ7 + MATOUT.PURZ8 + MATOUT.PURZ9 + MATOUT.PURZ10) AS TOTERGY, "
                StrSql = StrSql + "(MATOUT.G1+ MATOUT.G2 + MATOUT.G3 + MATOUT.G4 + MATOUT.G5 + MATOUT.G6 + MATOUT.G7 + MATOUT.G8 + MATOUT.G9 + MATOUT.G10)*PREF.CONVWT AS TOTCO2, "
                'Water Started
                StrSql = StrSql + "(MATOUT.PURW1+ MATOUT.PURW2 + MATOUT.PURW3 + MATOUT.PURW4 + MATOUT.PURW5 + MATOUT.PURW6 + MATOUT.PURW7 + MATOUT.PURW8 + MATOUT.PURW9 + MATOUT.PURW10)*PREF.CONVGALLON AS TOTWATER, "
                'Water End
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
                StrSql = StrSql + "FROM MATERIALINPUT MAT "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT1 "
                StrSql = StrSql + "ON MAT1.MATID = MAT.M1 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT2 "
                StrSql = StrSql + "ON MAT2.MATID = MAT.M2 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT3 "
                StrSql = StrSql + "ON MAT3.MATID = MAT.M3 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT4 "
                StrSql = StrSql + "ON MAT4.MATID = MAT.M4 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT5 "
                StrSql = StrSql + "ON MAT5.MATID = MAT.M5 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT6 "
                StrSql = StrSql + "ON MAT6.MATID = MAT.M6 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT7 "
                StrSql = StrSql + "ON MAT7.MATID = MAT.M7 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT8 "
                StrSql = StrSql + "ON MAT8.MATID = MAT.M8 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT9 "
                StrSql = StrSql + "ON MAT9.MATID = MAT.M9 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.MATERIALS MAT10 "
                StrSql = StrSql + "ON MAT10.MATID = MAT.M10 "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=MAT.CASEID "
                StrSql = StrSql + "INNER JOIN MATERIALOUTPUT MATOUT "
                StrSql = StrSql + "ON MATOUT.CASEID = MAT.CASEID "
                StrSql = StrSql + "INNER JOIN TOTAL "
                StrSql = StrSql + "ON TOTAL.CASEID = MAT.CASEID "
                StrSql = StrSql + "WHERE MAT.CASEID =" + CaseId.ToString() + " ORDER BY MAT.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetExtrusionOutDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetResultsEOLIN(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "MATB.CASEID, "
                StrSql = StrSql + "(CASE WHEN RESULTSPL.PVOLUSE=0 THEN "
                StrSql = StrSql + "RESULTSPL.VOLUME*PREF.CONVWT*PREF.CONVWT "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "RESULTSPL.SVOLUME*PREF.CONVWT*PREF.CONVWT "
                StrSql = StrSql + "END) AS SALESUNITVAL1, "

                StrSql = StrSql + "NVL((CASE WHEN RESULTSPL.FINVOLMUNITS >0 THEN "
                StrSql = StrSql + "RESULTSPL.FINVOLMUNITS "
                StrSql = StrSql + "END  ),0) SALESUNITVAL2, "

                StrSql = StrSql + "'Sales Volume ('||PREF.TITLE8 || ')' AS SALESUNIT1, "

                StrSql = StrSql + "(CASE WHEN RESULTSPL.FINVOLMUNITS >0 THEN "
                StrSql = StrSql + "'Sales Volume (units)' "
                StrSql = StrSql + "END) SALESUNIT2, "

                StrSql = StrSql + "'Product' AS PLANTSPACE1, "
                StrSql = StrSql + "'Raw Materials' AS PLANTSPACE2, "
                StrSql = StrSql + "'Production Waste' AS PLANTSPACE3, "
                StrSql = StrSql + "'Finished Product' AS PLANTSPACE4, "
                StrSql = StrSql + "'Raw Material Packaging' AS PLANTSPACE5, "
                StrSql = StrSql + "'Total Flow' AS PLANTSPACE6, "
                StrSql = StrSql + "'Waste' AS PLANTSPACE7, "
                StrSql = StrSql + "'Product Packaging' AS PLANTSPACE8, "
                StrSql = StrSql + "'Total Flow' AS PLANTSPACE9, "
                StrSql = StrSql + "'Waste' AS PLANTSPACE10, "
                StrSql = StrSql + "'Other Waste' AS PLANTSPACE11, "
                StrSql = StrSql + "'Plant' AS PLANTSPACE12, "
                StrSql = StrSql + "'Office' AS PLANTSPACE13, "
                StrSql = StrSql + "MATB.M1*PREF.CONVWT AS MATB1, "
                StrSql = StrSql + "MATB.M2*PREF.CONVWT AS MATB2, "
                StrSql = StrSql + "MATB.M3*PREF.CONVWT AS MATB3, "
                StrSql = StrSql + "MATB.M4*PREF.CONVWT AS MATB4, "
                StrSql = StrSql + "MATB.M5*PREF.CONVWT AS MATB5, "
                StrSql = StrSql + "MATB.M6*PREF.CONVWT AS MATB6, "
                StrSql = StrSql + "MATB.M7*PREF.CONVWT AS MATB7, "
                StrSql = StrSql + "MATB.M8*PREF.CONVWT AS MATB8, "
                StrSql = StrSql + "MATB.M9*PREF.CONVWT AS MATB9, "
                StrSql = StrSql + "MALIN.MR1 AS MALRE1, "
                StrSql = StrSql + "MALIN.MR2 AS MALRE2, "
                StrSql = StrSql + "MALIN.MR3 AS MALRE3, "
                StrSql = StrSql + "MALIN.MR4 AS MALRE4, "
                StrSql = StrSql + "MALIN.MR5 AS MALRE5, "
                StrSql = StrSql + "MALIN.MR6 AS MALRE6, "
                StrSql = StrSql + "MALIN.MR7 AS MALRE7, "
                StrSql = StrSql + "MALIN.MR8 AS MALRE8, "
                StrSql = StrSql + "MALIN.MR9 AS MALRE9, "
                StrSql = StrSql + "MALIN.MI1 AS MALIN1, "
                StrSql = StrSql + "MALIN.MI2 AS MALIN2, "
                StrSql = StrSql + "MALIN.MI3 AS MALIN3, "
                StrSql = StrSql + "MALIN.MI4 AS MALIN4, "
                StrSql = StrSql + "MALIN.MI5 AS MALIN5, "
                StrSql = StrSql + "MALIN.MI6 AS MALIN6, "
                StrSql = StrSql + "MALIN.MI7 AS MALIN7, "
                StrSql = StrSql + "MALIN.MI8 AS MALIN8, "
                StrSql = StrSql + "MALIN.MI9 AS MALIN9, "
                StrSql = StrSql + "MALIN.MC1 AS MALCM1, "
                StrSql = StrSql + "MALIN.MC2 AS MALCM2, "
                StrSql = StrSql + "MALIN.MC3 AS MALCM3, "
                StrSql = StrSql + "MALIN.MC4 AS MALCM4, "
                StrSql = StrSql + "MALIN.MC5 AS MALCM5, "
                StrSql = StrSql + "MALIN.MC6 AS MALCM6, "
                StrSql = StrSql + "MALIN.MC7 AS MALCM7, "
                StrSql = StrSql + "MALIN.MC8 AS MALCM8, "
                StrSql = StrSql + "MALIN.MC9 AS MALCM9, "
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
                StrSql = StrSql + "FROM MATERIALBALANCE MATB "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = MATB.CASEID "
                StrSql = StrSql + "INNER JOIN MATENDOFLIFEIN MALIN "
                StrSql = StrSql + "ON MALIN.CASEID = MATB.CASEID "
                StrSql = StrSql + "INNER JOIN RESULTSPL ON "
                StrSql = StrSql + "RESULTSPL.CASEID=MATB.CASEID "
                StrSql = StrSql + "WHERE MATB.CASEID =" + CaseId.ToString() + " ORDER BY MATB.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetResultsEOLIN:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetPalletInout(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "POUT.CASEID, "
                StrSql = StrSql + "'Product Weight Per Pallet' AS ITEM1, "
                StrSql = StrSql + "'Packaging Weight Per Pallet' AS ITEM2, "
                StrSql = StrSql + "'Total Weight  Per Pallet' AS ITEM3, "
                StrSql = StrSql + "'Number Of Pallets Per Truck' AS ITEM4, "
                StrSql = StrSql + "'Product Weight Per Truck' AS ITEM5, "
                StrSql = StrSql + "'Packaging Weight Per Truck'AS ITEM6, "
                StrSql = StrSql + "'Total Weight  Per Truck' AS ITEM7, "
                StrSql = StrSql + "'Total Number Of Trucks' AS ITEM8, "
                StrSql = StrSql + "(POUT.M1*PREF.CONVWT) AS VALUE1, "
                StrSql = StrSql + "(POUT.M2*PREF.CONVWT) AS VALUE2, "
                StrSql = StrSql + "(POUT.M3*PREF.CONVWT)AS VALUE3, "
                StrSql = StrSql + "POUT.M4 AS VALUE4, "
                StrSql = StrSql + "(POUT.M5*PREF.CONVWT)AS VALUE5, "
                StrSql = StrSql + "(POUT.M6*PREF.CONVWT) AS VALUE6, "
                StrSql = StrSql + "(POUT.M7*PREF.CONVWT) AS VALUE7, "
                StrSql = StrSql + "POUT.M8 AS VALUE8, "
                StrSql = StrSql + "'('|| PREF.TITLE8 || ')' AS UNIT1, "
                StrSql = StrSql + "'('|| PREF.TITLE8 || ')' AS UNIT2, "
                StrSql = StrSql + "'('|| PREF.TITLE8 || ')' AS UNIT3, "
                StrSql = StrSql + "'(unitless)' AS UNIT4, "
                StrSql = StrSql + "'('|| PREF.TITLE8 || ')' AS UNIT5, "
                StrSql = StrSql + "'('|| PREF.TITLE8 || ')' AS UNIT6, "
                StrSql = StrSql + "'('|| PREF.TITLE8 || ')' AS UNIT7, "
                StrSql = StrSql + "'(unitless)' AS UNIT8, "
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
                StrSql = StrSql + "FROM PALLETINOUT POUT "
                StrSql = StrSql + "INNER JOIN TOTAL "
                StrSql = StrSql + "ON TOTAL.CASEID = POUT.CASEID "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=POUT.CASEID "
                StrSql = StrSql + "WHERE POUT.CASEID=" + CaseId.ToString() + "  ORDER BY POUT.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetPalletInout:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetScoreCard(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT SCREIN.CASEID,  "
                StrSql = StrSql + "'Green House Gases' AS TYPE1, "
                StrSql = StrSql + "'Shelf unit packages' AS TYPE2, "
                StrSql = StrSql + "'Transport packaging for packaged product' AS TYPE3, "
                StrSql = StrSql + "'Sustainable Material' AS TYPE4, "
                StrSql = StrSql + "'Shelf unit packages' AS TYPE5, "
                StrSql = StrSql + "'Transport packaging for packaged product' AS TYPE6, "
                StrSql = StrSql + "'Transportation Distance' AS TYPE7, "
                StrSql = StrSql + "'Shelf unit transport rating'AS TYPE8, "
                StrSql = StrSql + "'Transport packaging for packaged product' AS TYPE9, "
                StrSql = StrSql + "'Package to Product ratio' AS TYPE10, "
                StrSql = StrSql + "'Shelf unit packages' AS TYPE11, "
                StrSql = StrSql + "'Transport packaging for packaged product' AS TYPE12, "
                StrSql = StrSql + "'Cube Utilization' AS TYPE13, "
                StrSql = StrSql + "'Product Selling Unit Volume ratio' AS TYPE14, "
                StrSql = StrSql + "'Pallet Transport Volume use ratio' AS TYPE15, "
                StrSql = StrSql + "'PC Recycled Content' AS TYPE16, "
                StrSql = StrSql + "'Shelf unit packages' AS TYPE17, "
                StrSql = StrSql + "'Transport packaging for packaged product' AS TYPE18, "
                StrSql = StrSql + "'Recovery' AS TYPE19, "
                StrSql = StrSql + "'Shelf unit packages' AS TYPE20, "
                StrSql = StrSql + "'Transport packaging for packaged product' AS TYPE21, "
                StrSql = StrSql + "SCREIN.M1, "
                StrSql = StrSql + "SCREIN.M2, "
                StrSql = StrSql + "SCREIN.M3, "
                StrSql = StrSql + "SCREIN.M4, "
                StrSql = StrSql + "SCREIN.M5, "
                StrSql = StrSql + "SCREIN.M6, "
                StrSql = StrSql + "SCREIN.M7, "
                StrSql = StrSql + "SCREIN.M8, "
                StrSql = StrSql + "SCREIN.M9, "
                StrSql = StrSql + "SCREIN.M10, "
                StrSql = StrSql + "SCREIN.M11, "
                StrSql = StrSql + "SCREIN.M12, "
                StrSql = StrSql + "SCREIN.M13, "
                StrSql = StrSql + "SCREIN.M14, "
                StrSql = StrSql + "SCREIN.M15, "
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
                StrSql = StrSql + "FROM SCORECARDINTM SCREIN "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=SCREIN.CASEID "
                StrSql = StrSql + "WHERE SCREIN.CASEID=" + CaseId.ToString() + "  ORDER BY SCREIN.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetScoreCard:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetOperationsOut(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "OPOUT.CASEID, "
                StrSql = StrSql + "(OPOUT1.PROCDE1) AS PROCDE1, "
                StrSql = StrSql + "(OPOUT2.PROCDE1) AS PROCDE2, "
                StrSql = StrSql + "(OPOUT3.PROCDE1) AS PROCDE3, "
                StrSql = StrSql + "(OPOUT4.PROCDE1) AS PROCDE4, "
                StrSql = StrSql + "(OPOUT5.PROCDE1) AS PROCDE5, "
                StrSql = StrSql + "(OPOUT6.PROCDE1) AS PROCDE6, "
                StrSql = StrSql + "(OPOUT7.PROCDE1) AS PROCDE7, "
                StrSql = StrSql + "(OPOUT8.PROCDE1) AS PROCDE8, "
                StrSql = StrSql + "(OPOUT9.PROCDE1) AS PROCDE9, "
                StrSql = StrSql + "(OPOUT10.PROCDE1) AS PROCDE10, "
                StrSql = StrSql + "(OPDEPVOL.M1*PREF.CONVWT) AS PV1, "
                StrSql = StrSql + "(OPDEPVOL.M2*PREF.CONVWT) AS PV2, "
                StrSql = StrSql + "(OPDEPVOL.M3*PREF.CONVWT) AS PV3, "
                StrSql = StrSql + "(OPDEPVOL.M4*PREF.CONVWT) AS PV4, "
                StrSql = StrSql + "(OPDEPVOL.M5*PREF.CONVWT) AS PV5, "
                StrSql = StrSql + "(OPDEPVOL.M6*PREF.CONVWT) AS PV6, "
                StrSql = StrSql + "(OPDEPVOL.M7*PREF.CONVWT) AS PV7, "
                StrSql = StrSql + "(OPDEPVOL.M8*PREF.CONVWT) AS PV8, "
                StrSql = StrSql + "(OPDEPVOL.M9*PREF.CONVWT) AS PV9, "
                StrSql = StrSql + "(OPDEPVOL.M10*PREF.CONVWT) AS PV10, "
                StrSql = StrSql + "(OPDEPVOL.T1*PREF.CONVWT) AS FEV1, "
                StrSql = StrSql + "(OPDEPVOL.T2*PREF.CONVWT) AS FEV2, "
                StrSql = StrSql + "(OPDEPVOL.T3*PREF.CONVWT) AS FEV3, "
                StrSql = StrSql + "(OPDEPVOL.T4*PREF.CONVWT) AS FEV4, "
                StrSql = StrSql + "(OPDEPVOL.T5*PREF.CONVWT) AS FEV5, "
                StrSql = StrSql + "(OPDEPVOL.T6*PREF.CONVWT) AS FEV6, "
                StrSql = StrSql + "(OPDEPVOL.T7*PREF.CONVWT) AS FEV7, "
                StrSql = StrSql + "(OPDEPVOL.T8*PREF.CONVWT) AS FEV8, "
                StrSql = StrSql + "(OPDEPVOL.T9*PREF.CONVWT) AS FEV9, "
                StrSql = StrSql + "(OPDEPVOL.T10*PREF.CONVWT) AS FEV10, "
                StrSql = StrSql + "(OPDEPVOL.G1*PREF.CONVWT) AS FPV1, "
                StrSql = StrSql + "(OPDEPVOL.G2*PREF.CONVWT) AS FPV2, "
                StrSql = StrSql + "(OPDEPVOL.G3*PREF.CONVWT) AS FPV3, "
                StrSql = StrSql + "(OPDEPVOL.G4*PREF.CONVWT) AS FPV4, "
                StrSql = StrSql + "(OPDEPVOL.G5*PREF.CONVWT) AS FPV5, "
                StrSql = StrSql + "(OPDEPVOL.G6*PREF.CONVWT) AS FPV6, "
                StrSql = StrSql + "(OPDEPVOL.G7*PREF.CONVWT) AS FPV7, "
                StrSql = StrSql + "(OPDEPVOL.G8*PREF.CONVWT) AS FPV8, "
                StrSql = StrSql + "(OPDEPVOL.G9*PREF.CONVWT) AS FPV9, "
                StrSql = StrSql + "(OPDEPVOL.G10*PREF.CONVWT) AS FPV10, "
                StrSql = StrSql + "(OPDEPVOL.D1) AS DW1, "
                StrSql = StrSql + "(OPDEPVOL.D2) AS DW2, "
                StrSql = StrSql + "(OPDEPVOL.D3) AS DW3, "
                StrSql = StrSql + "(OPDEPVOL.D4) AS DW4, "
                StrSql = StrSql + "(OPDEPVOL.D5) AS DW5, "
                StrSql = StrSql + "(OPDEPVOL.D6) AS DW6, "
                StrSql = StrSql + "(OPDEPVOL.D7) AS DW7, "
                StrSql = StrSql + "(OPDEPVOL.D8) AS DW8, "
                StrSql = StrSql + "(OPDEPVOL.D9) AS DW9, "
                StrSql = StrSql + "(OPDEPVOL.D10) AS DW10, "
                StrSql = StrSql + "(OPDEPVOL.W1) AS AW1, "
                StrSql = StrSql + "(OPDEPVOL.W2) AS AW2, "
                StrSql = StrSql + "(OPDEPVOL.W3) AS AW3, "
                StrSql = StrSql + "(OPDEPVOL.W4) AS AW4, "
                StrSql = StrSql + "(OPDEPVOL.W5) AS AW5, "
                StrSql = StrSql + "(OPDEPVOL.W6) AS AW6, "
                StrSql = StrSql + "(OPDEPVOL.W7) AS AW7, "
                StrSql = StrSql + "(OPDEPVOL.W8) AS AW8, "
                StrSql = StrSql + "(OPDEPVOL.W9) AS AW9, "
                StrSql = StrSql + "(OPDEPVOL.W10) AS AW10, "
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
                StrSql = StrSql + "FROM PLANTCONFIG OPOUT "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS OPOUT1 "
                StrSql = StrSql + "ON OPOUT1.PROCID = OPOUT.M1 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS OPOUT2 "
                StrSql = StrSql + "ON OPOUT2.PROCID = OPOUT.M2 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS OPOUT3 "
                StrSql = StrSql + "ON OPOUT3.PROCID = OPOUT.M3 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS OPOUT4 "
                StrSql = StrSql + "ON OPOUT4.PROCID = OPOUT.M4 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS OPOUT5 "
                StrSql = StrSql + "ON OPOUT5.PROCID = OPOUT.M5 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS OPOUT6 "
                StrSql = StrSql + "ON OPOUT6.PROCID = OPOUT.M6 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS OPOUT7 "
                StrSql = StrSql + "ON OPOUT7.PROCID = OPOUT.M7 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS OPOUT8 "
                StrSql = StrSql + "ON OPOUT8.PROCID = OPOUT.M8 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS OPOUT9 "
                StrSql = StrSql + "ON OPOUT9.PROCID = OPOUT.M9 "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PROCESS OPOUT10 "
                StrSql = StrSql + "ON OPOUT10.PROCID = OPOUT.M10 "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=OPOUT.CASEID "
                StrSql = StrSql + "INNER JOIN OPDEPVOL "
                StrSql = StrSql + "ON OPDEPVOL.CASEID = OPOUT.CASEID "
                StrSql = StrSql + "WHERE OPOUT.CASEID =" + CaseId.ToString() + "   ORDER BY OPOUT.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetOperationsOut:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Wizard"
        Public Function GetSessionWizardId() As DataSet
            Dim Dts As New DataSet()
            Try
                Dim odbUtil As New DBUtil()
                Dim StrSql As String = ""
                StrSql = "SELECT (SEQSESSIONID.NEXTVAL) AS SESSIONID FROM DUAL "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
#End Region

#Region "Results Pages"
        Public Function GetEnergyResults(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "TOT.CASEID, "
                StrSql = StrSql + "RSPL.CUSSALESVOLUME, "
                StrSql = StrSql + "RSPL.CUSSALESUNIT, "
                StrSql = StrSql + "'Raw Materials' AS A1, "
                StrSql = StrSql + "'Raw Materials Packaging' AS A2, "
                StrSql = StrSql + "'RM & Pack Transport' AS A3, "
                StrSql = StrSql + "'Process' AS A4, "
                StrSql = StrSql + "'Distribution Packaging' AS A5, "
                StrSql = StrSql + "'DP Transport' AS A6, "
                StrSql = StrSql + "'Transport to Customer' AS A7, "
                StrSql = StrSql + "'Process Energy(S2)' AS A8, "
                StrSql = StrSql + "'Packaged Product Packaging(S2)' AS A9, "
                StrSql = StrSql + "'PPP Transport(S2)' AS A10, "
                StrSql = StrSql + "'Packaged Product Transport(S2)' AS A11, "
                StrSql = StrSql + "'Total Energy' AS A12, "
                StrSql = StrSql + "'Purchased Materials' AS A13, "
                StrSql = StrSql + "'Process' AS A14, "
                StrSql = StrSql + "'Transportation' AS A15, "
                StrSql = StrSql + "'Total Energy' AS A16, "
                StrSql = StrSql + "TOT.RMERGY AS T1, "
                StrSql = StrSql + "TOT.RMPACKERGY AS T2, "
                StrSql = StrSql + "TOT.RMANDPACKTRNSPTERGY AS T3, "
                StrSql = StrSql + "TOT.PROCERGY AS T4, "
                StrSql = StrSql + "TOT.DPPACKERGY AS T5, "
                StrSql = StrSql + "TOT.DPTRNSPTERGY AS T6, "
                StrSql = StrSql + "TOT.TRSPTTOCUS AS T7, "
                StrSql = StrSql + "TOT.PROCERGYS2 AS T8, "
                StrSql = StrSql + "TOT.PACKGEDPRODPACK AS T9, "
                StrSql = StrSql + "TOT.PPPTRNSPT AS T10, "
                StrSql = StrSql + "TOT.PACKGEDPRODTRNSPT AS T11, "
                StrSql = StrSql + "(TOT.RMERGY+TOT.RMPACKERGY+TOT.RMANDPACKTRNSPTERGY+TOT.PROCERGY+TOT.DPPACKERGY+TOT.DPTRNSPTERGY+TOT.TRSPTTOCUS+TOT.PROCERGYS2+TOT.PACKGEDPRODPACK+TOT.PPPTRNSPT+TOT.PACKGEDPRODTRNSPT) T12, "
                StrSql = StrSql + "(TOT.RMERGY+TOT.RMPACKERGY+TOT.DPPACKERGY+TOT.PACKGEDPRODPACK) AS T13, "
                StrSql = StrSql + "(TOT.PROCERGY+TOT.PROCERGYS2)AS T14, "
                StrSql = StrSql + "(TOT.RMANDPACKTRNSPTERGY+TOT.DPTRNSPTERGY+TOT.TRSPTTOCUS+TOT.PPPTRNSPT+TOT.PACKGEDPRODTRNSPT) AS  T15, "
                StrSql = StrSql + "(TOT.RMERGY+TOT.RMPACKERGY+TOT.RMANDPACKTRNSPTERGY+TOT.PROCERGY+TOT.DPPACKERGY+TOT.DPTRNSPTERGY+TOT.TRSPTTOCUS+TOT.PROCERGYS2+TOT.PACKGEDPRODPACK+TOT.PPPTRNSPT+TOT.PACKGEDPRODTRNSPT) T16, "
                StrSql = StrSql + "(CASE WHEN RSPL.PVOLUSE=0 THEN RSPL.VOLUME*PREF.CONVWT "
                StrSql = StrSql + "ELSE RSPL.SVOLUME*PREF.CONVWT END) AS SALESVOLUMELB, "

                StrSql = StrSql + "NVL((CASE WHEN RSPL.FINVOLMUNITS>0 THEN RSPL.FINVOLMUNITS END),0) SALESVOLUMEUNIT, "
                StrSql = StrSql + "'Sales Volume ('|| PREF.TITLE8||')' AS VOLUNI1, "

                StrSql = StrSql + "(CASE WHEN RSPL.FINVOLMUNITS > 0 THEN 'Sales Volume (units)' END) VOLUNI2, "
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
                StrSql = StrSql + "PREF.CONVWT, "
                StrSql = StrSql + "PREF.UNITS , "
                StrSql = StrSql + "(CASE WHEN PLANTENERGY.ELECPRICE <> 0 THEN "
                StrSql = StrSql + "PLANTENERGY.ELECPRICE "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "ELEC.PRICE "
                StrSql = StrSql + "END)ELECP, "
                StrSql = StrSql + "(SELECT MJPKWH FROM Econ.CONVFACTORS2) AS MJPKWH "
                StrSql = StrSql + "FROM TOTAL TOT "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = TOT.CASEID "
                StrSql = StrSql + "INNER JOIN RESULTSPL RSPL "
                StrSql = StrSql + "ON RSPL.CASEID = TOT.CASEID "
                StrSql = StrSql + "Inner Join PLANTENERGY "
                StrSql = StrSql + "ON PLANTENERGY.Caseid= TOT.CASEID "
                StrSql = StrSql + "INNER JOIN SUSTAIN.ENERGY ELEC "
                StrSql = StrSql + "ON ELEC.ENERGYID = 1 "
                StrSql = StrSql + "WHERE TOT.CASEID =" + CaseId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetEnergyResults:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetEOLResults(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "MATB.CASEID, "
                StrSql = StrSql + "RSPL.CUSSALESVOLUME, "
                StrSql = StrSql + "RSPL.CUSSALESUNIT, "
                StrSql = StrSql + "'Product' AS PLANTSPACE1, "
                StrSql = StrSql + "'Raw Materials' AS PLANTSPACE2, "
                StrSql = StrSql + "'Production Waste' AS PLANTSPACE3, "
                StrSql = StrSql + "'Finished Product' AS PLANTSPACE4, "
                StrSql = StrSql + "'Raw Material Packaging' AS PLANTSPACE5, "
                StrSql = StrSql + "'Total Flow' AS PLANTSPACE6, "
                StrSql = StrSql + "'Waste' AS PLANTSPACE7, "
                StrSql = StrSql + "'Product Packaging' AS PLANTSPACE8, "
                StrSql = StrSql + "'Total Flow' AS PLANTSPACE9, "
                StrSql = StrSql + "'Waste' AS PLANTSPACE10, "
                StrSql = StrSql + "'Other Waste' AS PLANTSPACE11, "
                StrSql = StrSql + "'Plant' AS PLANTSPACE12, "
                StrSql = StrSql + "'Office' AS PLANTSPACE13, "
                StrSql = StrSql + "'End Of life Energy (MJ)' AS PLANTSPACE14, "
                StrSql = StrSql + "'Finished Product (MJ)' AS PLANTSPACE15, "
                StrSql = StrSql + "'Production Waste (MJ)' AS PLANTSPACE16, "
                StrSql = StrSql + "'Raw Material Packaging Waste (MJ)' AS PLANTSPACE17, "
                StrSql = StrSql + "'Product Packaging Waste (MJ)'  AS PLANTSPACE18, "
                StrSql = StrSql + "'Plant Waste (MJ)' AS PLANTSPACE19, "
                StrSql = StrSql + "'Office Waste (MJ)' AS PLANTSPACE20, "
                StrSql = StrSql + "'Total End Of life Energy (MJ)' AS PLANTSPACE21, "
                StrSql = StrSql + "'End Of Life GHG ('|| PREF.TITLE8||')' AS PLANTSPACE22, "
                StrSql = StrSql + "'Finished Product  ('|| PREF.TITLE8||')' AS PLANTSPACE23, "
                StrSql = StrSql + "'Production Waste ('|| PREF.TITLE8||')' AS PLANTSPACE24, "
                StrSql = StrSql + "'Raw Material Packaging Waste ('|| PREF.TITLE8||')' AS PLANTSPACE25, "
                StrSql = StrSql + "'Product Packaging Waste ('|| PREF.TITLE8||')' AS PLANTSPACE26, "
                StrSql = StrSql + "'Plant Waste ('|| PREF.TITLE8||')' AS PLANTSPACE27, "
                StrSql = StrSql + "'Office Waste ('|| PREF.TITLE8||')' AS PLANTSPACE28, "
                StrSql = StrSql + "'Total End Of Life GHG ('|| PREF.TITLE8||')' AS PLANTSPACE29, "
                StrSql = StrSql + "MATB.M1*PREF.CONVWT AS MATB1, "
                StrSql = StrSql + "MATB.M2*PREF.CONVWT AS MATB2, "
                StrSql = StrSql + "MATB.M3*PREF.CONVWT AS MATB3, "
                StrSql = StrSql + "MATB.M4*PREF.CONVWT AS MATB4, "
                StrSql = StrSql + "MATB.M5*PREF.CONVWT AS MATB5, "
                StrSql = StrSql + "MATB.M6*PREF.CONVWT AS MATB6, "
                StrSql = StrSql + "MATB.M7*PREF.CONVWT AS MATB7, "
                StrSql = StrSql + "MATB.M8*PREF.CONVWT AS MATB8, "
                StrSql = StrSql + "MATB.M9*PREF.CONVWT AS MATB9, "
                StrSql = StrSql + "MATBW.M1 AS MATB10, "
                StrSql = StrSql + "MATBW.M2 AS MATB11, "
                StrSql = StrSql + "MATBW.M3 AS MATB12, "
                StrSql = StrSql + "MATBW.M4 AS MATB13, "
                StrSql = StrSql + "MATBW.M5 AS MATB14, "
                StrSql = StrSql + "MATBW.M6 AS MATB15, "
                StrSql = StrSql + "(MATBW.M1+MATBW.M2+MATBW.M3+MATBW.M4+MATBW.M5+MATBW.M6) AS MATB16, "
                StrSql = StrSql + "(MATBW.M7+MATBW.M13)*PREF.CONVWT AS MATB17, "
                StrSql = StrSql + "(MATBW.M8+MATBW.M14)*PREF.CONVWT AS MATB18, "
                StrSql = StrSql + "(MATBW.M9+MATBW.M15)*PREF.CONVWT AS MATB19, "
                StrSql = StrSql + "(MATBW.M10+MATBW.M16)*PREF.CONVWT AS MATB20, "
                StrSql = StrSql + "(MATBW.M11+MATBW.M17)*PREF.CONVWT AS MATB21, "
                StrSql = StrSql + "(MATBW.M12+MATBW.M18)*PREF.CONVWT AS MATB22, "
                StrSql = StrSql + "(MATBW.M7+MATBW.M8+MATBW.M9+MATBW.M10+MATBW.M11+MATBW.M12+MATBW.M13+MATBW.M14+MATBW.M15+MATBW.M16+MATBW.M17+MATBW.M18)*PREF.CONVWT AS MATB23, "
                StrSql = StrSql + "MALOUT.MR1*PREF.CONVWT AS MATRE1, "
                StrSql = StrSql + "MALOUT.MR2*PREF.CONVWT AS MATRE2, "
                StrSql = StrSql + "MALOUT.MR3*PREF.CONVWT AS MATRE3, "
                StrSql = StrSql + "MALOUT.MR4*PREF.CONVWT AS MATRE4, "
                StrSql = StrSql + "MALOUT.MR5*PREF.CONVWT AS MATRE5, "
                StrSql = StrSql + "MALOUT.MR6*PREF.CONVWT AS MATRE6, "
                StrSql = StrSql + "MALOUT.MR7*PREF.CONVWT AS MATRE7, "
                StrSql = StrSql + "MALOUT.MR8*PREF.CONVWT AS MATRE8, "
                StrSql = StrSql + "MALOUT.MR9*PREF.CONVWT AS MATRE9, "
                StrSql = StrSql + "MALOUT.MI1*PREF.CONVWT AS MATIN1, "
                StrSql = StrSql + "MALOUT.MI2*PREF.CONVWT AS MATIN2, "
                StrSql = StrSql + "MALOUT.MI3*PREF.CONVWT AS MATIN3, "
                StrSql = StrSql + "MALOUT.MI4*PREF.CONVWT AS MATIN4, "
                StrSql = StrSql + "MALOUT.MI5*PREF.CONVWT AS MATIN5, "
                StrSql = StrSql + "MALOUT.MI6*PREF.CONVWT AS MATIN6, "
                StrSql = StrSql + "MALOUT.MI7*PREF.CONVWT AS MATIN7, "
                StrSql = StrSql + "MALOUT.MI8*PREF.CONVWT AS MATIN8, "
                StrSql = StrSql + "MALOUT.MI9*PREF.CONVWT AS MATIN9, "
                StrSql = StrSql + "MATBW.M1 AS MATIN10, "
                StrSql = StrSql + "MATBW.M2 AS MATIN11, "
                StrSql = StrSql + "MATBW.M3 AS MATIN12, "
                StrSql = StrSql + "MATBW.M4 AS MATIN13, "
                StrSql = StrSql + "MATBW.M5 AS MATIN14, "
                StrSql = StrSql + "MATBW.M6 AS MATIN15, "
                StrSql = StrSql + "(MATBW.M1+MATBW.M2+MATBW.M3+MATBW.M4+MATBW.M5+MATBW.M6) AS MATIN16, "
                StrSql = StrSql + "MATBW.M7*PREF.CONVWT AS MATIN17, "
                StrSql = StrSql + "MATBW.M8*PREF.CONVWT AS MATIN18, "
                StrSql = StrSql + "MATBW.M9*PREF.CONVWT AS MATIN19, "
                StrSql = StrSql + "MATBW.M10*PREF.CONVWT AS MATIN20, "
                StrSql = StrSql + "MATBW.M11*PREF.CONVWT AS MATIN21, "
                StrSql = StrSql + "MATBW.M12*PREF.CONVWT AS MATIN22, "
                StrSql = StrSql + "(MATBW.M7+MATBW.M8+MATBW.M9+MATBW.M10+MATBW.M11+MATBW.M12)*PREF.CONVWT AS MATIN23, "
                StrSql = StrSql + "MALOUT.MC1*PREF.CONVWT AS MATCM1, "
                StrSql = StrSql + "MALOUT.MC2*PREF.CONVWT AS MATCM2, "
                StrSql = StrSql + "MALOUT.MC3*PREF.CONVWT AS MATCM3, "
                StrSql = StrSql + "MALOUT.MC4*PREF.CONVWT AS MATCM4, "
                StrSql = StrSql + "MALOUT.MC5*PREF.CONVWT AS MATCM5, "
                StrSql = StrSql + "MALOUT.MC6*PREF.CONVWT AS MATCM6, "
                StrSql = StrSql + "MALOUT.MC7*PREF.CONVWT AS MATCM7, "
                StrSql = StrSql + "MALOUT.MC8*PREF.CONVWT AS MATCM8, "
                StrSql = StrSql + "MALOUT.MC9*PREF.CONVWT AS MATCM9, "
                StrSql = StrSql + "MATBW.M13*PREF.CONVWT AS MATCM17, "
                StrSql = StrSql + "MATBW.M14*PREF.CONVWT AS MATCM18, "
                StrSql = StrSql + "MATBW.M15*PREF.CONVWT AS MATCM19, "
                StrSql = StrSql + "MATBW.M16*PREF.CONVWT AS MATCM20, "
                StrSql = StrSql + "MATBW.M17*PREF.CONVWT AS MATCM21, "
                StrSql = StrSql + "MATBW.M18*PREF.CONVWT AS MATCM22, "
                StrSql = StrSql + "(MATBW.M13+MATBW.M14+MATBW.M15+MATBW.M16+MATBW.M17+MATBW.M18)*PREF.CONVWT AS MATCM23, "
                StrSql = StrSql + "MALOUT.ML1*PREF.CONVWT AS MATLF1, "
                StrSql = StrSql + "MALOUT.ML2*PREF.CONVWT AS MATLF2, "
                StrSql = StrSql + "MALOUT.ML3*PREF.CONVWT AS MATLF3, "
                StrSql = StrSql + "MALOUT.ML4*PREF.CONVWT AS MATLF4, "
                StrSql = StrSql + "MALOUT.ML5*PREF.CONVWT AS MATLF5, "
                StrSql = StrSql + "MALOUT.ML6*PREF.CONVWT AS MATLF6, "
                StrSql = StrSql + "MALOUT.ML7*PREF.CONVWT AS MATLF7, "
                StrSql = StrSql + "MALOUT.ML8*PREF.CONVWT AS MATLF8, "
                StrSql = StrSql + "MALOUT.ML9*PREF.CONVWT AS MATLF9, "
                StrSql = StrSql + "(CASE WHEN RSPL.PVOLUSE=0 THEN RSPL.VOLUME*PREF.CONVWT "
                StrSql = StrSql + "ELSE RSPL.SVOLUME*PREF.CONVWT END) AS SALESVOLUMELB, "

                StrSql = StrSql + "NVL((CASE WHEN RSPL.FINVOLMUNITS>0 THEN RSPL.FINVOLMUNITS END),0) SALESVOLUMEUNIT, "
                StrSql = StrSql + "'Sales Volume ('|| PREF.TITLE8||')' AS VOLUNI1, "

                StrSql = StrSql + "(CASE WHEN RSPL.FINVOLMUNITS > 0 THEN 'Sales Volume (units)' END) VOLUNI2, "
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
                StrSql = StrSql + "PREF.CONVWT ,"
                StrSql = StrSql + "PREF.UNITS "
                StrSql = StrSql + "FROM MATERIALBALANCE MATB "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = MATB.CASEID "
                StrSql = StrSql + "INNER JOIN MATERIALBALANCEWASTE MATBW "
                StrSql = StrSql + "ON MATBW.CASEID = MATB.CASEID "
                StrSql = StrSql + "INNER JOIN MATENDOFLIFEOUT MALOUT "
                StrSql = StrSql + "ON MALOUT.CASEID = MATB.CASEID "
                StrSql = StrSql + "INNER JOIN RESULTSPL RSPL "
                StrSql = StrSql + "ON RSPL.CASEID = MATB.CASEID "
                StrSql = StrSql + "INNER JOIN Total TOT "
                StrSql = StrSql + "ON TOT.CASEID = MATB.CASEID "

                StrSql = StrSql + "WHERE MATB.CASEID =" + CaseId.ToString() + " "



                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetEOLResults:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetScoreCardResults(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT RSCRE.CASEID,  "
                StrSql = StrSql + "'Purchased Material GHG' AS TYPE1, "
                StrSql = StrSql + "'Sustainable Material' AS TYPE2, "
                StrSql = StrSql + "'Transportation Distance' AS TYPE3, "
                StrSql = StrSql + "'Package to Product ratio' AS TYPE4, "
                StrSql = StrSql + "'Cube Utilization' AS TYPE5, "
                StrSql = StrSql + "'Recycled Content' AS TYPE6, "
                StrSql = StrSql + "'Recovery' AS TYPE7, "
                StrSql = StrSql + "'Renewable Energy' AS TYPE8, "
                StrSql = StrSql + "'Energy Innovation' AS TYPE9, "
                StrSql = StrSql + "RSCRE.M1 AS RW1, "
                StrSql = StrSql + "RSCRE.M2 AS RW2, "
                StrSql = StrSql + "RSCRE.M3 AS RW3, "
                StrSql = StrSql + "RSCRE.M4 AS RW4, "
                StrSql = StrSql + "RSCRE.M5 AS RW5, "
                StrSql = StrSql + "RSCRE.M6 AS RW6, "
                StrSql = StrSql + "RSCRE.M7 AS RW7, "
                StrSql = StrSql + "RSCRE.M8 AS RW8, "
                StrSql = StrSql + "RSCRE.M9 AS RW9, "
                StrSql = StrSql + "(SELECT LS1 FROM RANGES WHERE ROWNUM=1) AS LOWRANGES1, "
                StrSql = StrSql + "(SELECT LS2 FROM RANGES WHERE ROWNUM=1) AS LOWRANGES2, "
                StrSql = StrSql + "(SELECT LS3 FROM RANGES WHERE ROWNUM=1) AS LOWRANGES3, "
                StrSql = StrSql + "(SELECT LS4 FROM RANGES WHERE ROWNUM=1) AS LOWRANGES4, "
                StrSql = StrSql + "(SELECT LS5 FROM RANGES WHERE ROWNUM=1) AS LOWRANGES5, "
                StrSql = StrSql + "(SELECT LS6 FROM RANGES WHERE ROWNUM=1) AS LOWRANGES6, "
                StrSql = StrSql + "(SELECT LS7 FROM RANGES WHERE ROWNUM=1) AS LOWRANGES7, "
                StrSql = StrSql + "(SELECT LS8 FROM RANGES WHERE ROWNUM=1) AS LOWRANGES8, "
                StrSql = StrSql + "(SELECT LS9 FROM RANGES WHERE ROWNUM=1) AS LOWRANGES9, "
                StrSql = StrSql + "RANGESPREF.LP1 AS LOWRANGEP1, "
                StrSql = StrSql + "RANGESPREF.LP2 AS LOWRANGEP2, "
                StrSql = StrSql + "RANGESPREF.LP3 AS LOWRANGEP3, "
                StrSql = StrSql + "RANGESPREF.LP4 AS LOWRANGEP4, "
                StrSql = StrSql + "RANGESPREF.LP5 AS LOWRANGEP5, "
                StrSql = StrSql + "RANGESPREF.LP6 AS LOWRANGEP6, "
                StrSql = StrSql + "RANGESPREF.LP7 AS LOWRANGEP7, "
                StrSql = StrSql + "RANGESPREF.LP8 AS LOWRANGEP8, "
                StrSql = StrSql + "RANGESPREF.LP9 AS LOWRANGEP9, "
                StrSql = StrSql + "(SELECT HS1 FROM RANGES WHERE ROWNUM=1) AS HIGHRANGES1, "
                StrSql = StrSql + "(SELECT HS2 FROM RANGES WHERE ROWNUM=1) AS HIGHRANGES2, "
                StrSql = StrSql + "(SELECT HS3 FROM RANGES WHERE ROWNUM=1) AS HIGHRANGES3, "
                StrSql = StrSql + "(SELECT HS4 FROM RANGES WHERE ROWNUM=1) AS HIGHRANGES4, "
                StrSql = StrSql + "(SELECT HS5 FROM RANGES WHERE ROWNUM=1) AS HIGHRANGES5, "
                StrSql = StrSql + "(SELECT HS6 FROM RANGES WHERE ROWNUM=1) AS HIGHRANGES6, "
                StrSql = StrSql + "(SELECT HS7 FROM RANGES WHERE ROWNUM=1) AS HIGHRANGES7, "
                StrSql = StrSql + "(SELECT HS8 FROM RANGES WHERE ROWNUM=1) AS HIGHRANGES8, "
                StrSql = StrSql + "(SELECT HS9 FROM RANGES WHERE ROWNUM=1) AS HIGHRANGES9, "
                StrSql = StrSql + "RANGESPREF.HP1 AS HIGHRANGEP1, "
                StrSql = StrSql + "RANGESPREF.HP2 AS HIGHRANGEP2, "
                StrSql = StrSql + "RANGESPREF.HP3 AS HIGHRANGEP3, "
                StrSql = StrSql + "RANGESPREF.HP4 AS HIGHRANGEP4, "
                StrSql = StrSql + "RANGESPREF.HP5 AS HIGHRANGEP5, "
                StrSql = StrSql + "RANGESPREF.HP6 AS HIGHRANGEP6, "
                StrSql = StrSql + "RANGESPREF.HP7 AS HIGHRANGEP7, "
                StrSql = StrSql + "RANGESPREF.HP8 AS HIGHRANGEP8, "
                StrSql = StrSql + "RANGESPREF.HP9 AS HIGHRANGEP9, "
                StrSql = StrSql + "(SELECT W1 FROM WEIGHTINGS) AS WEIGHTS1, "
                StrSql = StrSql + "(SELECT W2 FROM WEIGHTINGS) AS WEIGHTS2, "
                StrSql = StrSql + "(SELECT W3 FROM WEIGHTINGS) AS WEIGHTS3, "
                StrSql = StrSql + "(SELECT W4 FROM WEIGHTINGS) AS WEIGHTS4, "
                StrSql = StrSql + "(SELECT W5 FROM WEIGHTINGS) AS WEIGHTS5, "
                StrSql = StrSql + "(SELECT W6 FROM WEIGHTINGS) AS WEIGHTS6, "
                StrSql = StrSql + "(SELECT W7 FROM WEIGHTINGS) AS WEIGHTS7, "
                StrSql = StrSql + "(SELECT W8 FROM WEIGHTINGS) AS WEIGHTS8, "
                StrSql = StrSql + "(SELECT W9 FROM WEIGHTINGS) AS WEIGHTS9, "
                StrSql = StrSql + "WEIGHTINGSPREF.WP1 AS WEIGHTP1, "
                StrSql = StrSql + "WEIGHTINGSPREF.WP2 AS WEIGHTP2, "
                StrSql = StrSql + "WEIGHTINGSPREF.WP3 AS WEIGHTP3, "
                StrSql = StrSql + "WEIGHTINGSPREF.WP4 AS WEIGHTP4, "
                StrSql = StrSql + "WEIGHTINGSPREF.WP5 AS WEIGHTP5, "
                StrSql = StrSql + "WEIGHTINGSPREF.WP6 AS WEIGHTP6, "
                StrSql = StrSql + "WEIGHTINGSPREF.WP7 AS WEIGHTP7, "
                StrSql = StrSql + "WEIGHTINGSPREF.WP8 AS WEIGHTP8, "
                StrSql = StrSql + "WEIGHTINGSPREF.WP9 AS WEIGHTP9, "
                StrSql = StrSql + "WEIGHTINGSPREF.WT AS WEIGHTINGSPREFTOT, "
                'StrSql = StrSql + "(M1+M2+M3+M4+M5+M6+M7+M8+M9)RWT, "
                StrSql = StrSql + "(RSCRE.M1+RSCRE.M2+RSCRE.M3+RSCRE.M4+RSCRE.M5+RSCRE.M6+RSCRE.M7+RSCRE.M8+RSCRE.M9)RWT, "
                StrSql = StrSql + "MSCRE.M1 AS MX1, "
                StrSql = StrSql + "MSCRE.M2 AS MX2, "
                StrSql = StrSql + "MSCRE.M3 AS MX3, "
                StrSql = StrSql + "MSCRE.M4 AS MX4, "
                StrSql = StrSql + "MSCRE.M5 AS MX5, "
                StrSql = StrSql + "MSCRE.M6 AS MX6, "
                StrSql = StrSql + "MSCRE.M7 AS MX7, "
                StrSql = StrSql + "MSCRE.M8 AS MX8, "
                StrSql = StrSql + "MSCRE.M9 AS MX9, "
                StrSql = StrSql + "(MSCRE.M1+MSCRE.M2+MSCRE.M3+MSCRE.M4+MSCRE.M5+MSCRE.M6+MSCRE.M7+MSCRE.M8+MSCRE.M9)MXT, "
                StrSql = StrSql + "TSCRE.M1 AS TOT1, "
                StrSql = StrSql + "TSCRE.M2 AS TOT2, "
                StrSql = StrSql + "TSCRE.M3 AS TOT3, "
                StrSql = StrSql + "TSCRE.M4 AS TOT4, "
                StrSql = StrSql + "TSCRE.M5 AS TOT5, "
                StrSql = StrSql + "TSCRE.M6 AS TOT6, "
                StrSql = StrSql + "TSCRE.M7 AS TOT7, "
                StrSql = StrSql + "TSCRE.M8 AS TOT8, "
                StrSql = StrSql + "TSCRE.M9 AS TOT9, "
                StrSql = StrSql + "(TSCRE.M1+TSCRE.M2+TSCRE.M3+TSCRE.M4+TSCRE.M5+TSCRE.M6+TSCRE.M7+TSCRE.M8+TSCRE.M9)TOTT, "
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
                StrSql = StrSql + "FROM RAWSCORE RSCRE "
                StrSql = StrSql + "INNER JOIN MAXSCORE MSCRE "
                StrSql = StrSql + "ON MSCRE.CASEID = RSCRE.CASEID "
                StrSql = StrSql + "INNER JOIN TOTALSCORE TSCRE "
                StrSql = StrSql + "ON TSCRE.CASEID = RSCRE.CASEID "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=RSCRE.CASEID "
                StrSql = StrSql + "INNER JOIN RANGESPREF ON "
                StrSql = StrSql + "RANGESPREF.CASEID=RSCRE.CASEID "
                StrSql = StrSql + "INNER JOIN WEIGHTINGSPREF ON "
                StrSql = StrSql + "WEIGHTINGSPREF.CASEID=RSCRE.CASEID "
                StrSql = StrSql + "WHERE RSCRE.CASEID =" + CaseId.ToString() + " "


                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetScoreCardResults:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetGhgResults(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "TOT.CASEID, "
                StrSql = StrSql + "RSPL.CUSSALESVOLUME, "
                StrSql = StrSql + "RSPL.CUSSALESUNIT, "
                StrSql = StrSql + "'Raw Materials' AS A1, "
                StrSql = StrSql + "'Raw Materials Packaging' AS A2, "
                StrSql = StrSql + "'RM & Pack Transport' AS A3, "
                StrSql = StrSql + "'Process' AS A4, "
                StrSql = StrSql + "'Distribution Packaging' AS A5, "
                StrSql = StrSql + "'DP Transport' AS A6, "
                StrSql = StrSql + "'Transport to Customer' AS A7, "
                StrSql = StrSql + "'Process Energy(S2)' AS A8, "
                StrSql = StrSql + "'Packaged Product Packaging(S2)' AS A9, "
                StrSql = StrSql + "'PPP Transport(S2)' AS A10, "
                StrSql = StrSql + "'Packaged Product Transport(S2)' AS A11, "
                StrSql = StrSql + "'Total Greenhouse Gas' AS A12, "
                StrSql = StrSql + "'Purchased Materials' AS A13, "
                StrSql = StrSql + "'Process' AS A14, "
                StrSql = StrSql + "'Transportation' AS A15, "
                StrSql = StrSql + "'Total Greenhouse Gas' AS A16, "
                StrSql = StrSql + "(TOT.RMGRNHUSGAS*PREF.CONVWT) AS T1, "
                StrSql = StrSql + "(TOT.RMPACKGRNHUSGAS*PREF.CONVWT) AS T2, "
                StrSql = StrSql + "(TOT.RMANDPACKTRNSPTGRNHUSGAS*PREF.CONVWT) AS T3, "
                StrSql = StrSql + "(TOT.PROCGRNHUSGAS*PREF.CONVWT) AS T4, "
                StrSql = StrSql + "(TOT.DPPACKGRNHUSGAS*PREF.CONVWT) AS T5, "
                StrSql = StrSql + "(TOT.DPTRNSPTGRNHUSGAS*PREF.CONVWT) AS T6, "
                StrSql = StrSql + "(TOT.TRSPTTOCUSGRNHUSGAS*PREF.CONVWT) AS T7, "
                StrSql = StrSql + "(TOT.PROCGRNHUSGASS2*PREF.CONVWT) AS T8, "
                StrSql = StrSql + "(TOT.PACKGEDPRODPACKGRNHUSGAS*PREF.CONVWT) AS T9, "
                StrSql = StrSql + "(TOT.PPPTRNSPTGRNHUSGAS*PREF.CONVWT) AS T10, "
                StrSql = StrSql + "(TOT.PACKGEDPRODTRNSPTGRNHUSGAS*PREF.CONVWT) AS T11, "
                StrSql = StrSql + "((TOT.RMGRNHUSGAS+TOT.RMPACKGRNHUSGAS+TOT.RMANDPACKTRNSPTGRNHUSGAS+TOT.PROCGRNHUSGAS+TOT.DPPACKGRNHUSGAS+TOT.DPTRNSPTGRNHUSGAS+TOT.TRSPTTOCUSGRNHUSGAS+TOT.PROCGRNHUSGASS2+TOT.PACKGEDPRODPACKGRNHUSGAS+TOT.PPPTRNSPTGRNHUSGAS+TOT.PACKGEDPRODTRNSPTGRNHUSGAS)*PREF.CONVWT) T12, "
                StrSql = StrSql + "((TOT.RMGRNHUSGAS+TOT.RMPACKGRNHUSGAS+TOT.DPPACKGRNHUSGAS+TOT.PACKGEDPRODPACKGRNHUSGAS)*PREF.CONVWT) AS T13, "
                StrSql = StrSql + "(TOT.PROCGRNHUSGAS+TOT.PROCGRNHUSGASS2)*PREF.CONVWT AS T14, "
                StrSql = StrSql + "((TOT.RMANDPACKTRNSPTGRNHUSGAS+TOT.DPTRNSPTGRNHUSGAS+TOT.TRSPTTOCUSGRNHUSGAS+TOT.PPPTRNSPTGRNHUSGAS+TOT.PACKGEDPRODTRNSPTGRNHUSGAS)*PREF.CONVWT) AS  T15, "
                StrSql = StrSql + "((TOT.RMGRNHUSGAS+TOT.RMPACKGRNHUSGAS+TOT.RMANDPACKTRNSPTGRNHUSGAS+TOT.PROCGRNHUSGAS+TOT.DPPACKGRNHUSGAS+TOT.DPTRNSPTGRNHUSGAS+TOT.TRSPTTOCUSGRNHUSGAS+TOT.PROCGRNHUSGASS2+TOT.PACKGEDPRODPACKGRNHUSGAS+TOT.PPPTRNSPTGRNHUSGAS+TOT.PACKGEDPRODTRNSPTGRNHUSGAS)*PREF.CONVWT) T16, "
                StrSql = StrSql + "(CASE WHEN RSPL.PVOLUSE=0 THEN RSPL.VOLUME*PREF.CONVWT "
                StrSql = StrSql + "ELSE RSPL.SVOLUME*PREF.CONVWT END) AS SALESVOLUMELB, "

                StrSql = StrSql + "NVL((CASE WHEN RSPL.FINVOLMUNITS>0 THEN RSPL.FINVOLMUNITS END),0) SALESVOLUMEUNIT, "
                StrSql = StrSql + "'Sales Volume ('|| PREF.TITLE8||')' AS VOLUNI1, "

                StrSql = StrSql + "(CASE WHEN RSPL.FINVOLMUNITS > 0 THEN 'Sales Volume (units)' END)VOLUNI2, "
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
                StrSql = StrSql + "PREF.Convwt, "
                StrSql = StrSql + "PREF.UNITS "
                StrSql = StrSql + "FROM TOTAL TOT "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = TOT.CASEID "
                StrSql = StrSql + "INNER JOIN RESULTSPL RSPL "
                StrSql = StrSql + "ON RSPL.CASEID = TOT.CASEID "
                StrSql = StrSql + "WHERE TOT.CASEID =" + CaseId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetGhgResults:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        'Water Started
        Public Function GetWaterResults(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "TOT.CASEID, "
                StrSql = StrSql + "RSPL.CUSSALESVOLUME, "
                StrSql = StrSql + "RSPL.CUSSALESUNIT, "
                StrSql = StrSql + "'Raw Materials' AS A1, "
                StrSql = StrSql + "'Raw Materials Packaging' AS A2, "
                StrSql = StrSql + "'RM & Pack Transport' AS A3, "
                StrSql = StrSql + "'Process' AS A4, "
                StrSql = StrSql + "'Distribution Packaging' AS A5, "
                StrSql = StrSql + "'DP Transport' AS A6, "
                StrSql = StrSql + "'Transport to Customer' AS A7, "
                StrSql = StrSql + "'Process Energy(S2)' AS A8, "
                StrSql = StrSql + "'Packaged Product Packaging(S2)' AS A9, "
                StrSql = StrSql + "'PPP Transport(S2)' AS A10, "
                StrSql = StrSql + "'Packaged Product Transport(S2)' AS A11, "
                StrSql = StrSql + "'Total Water' AS A12, "
                StrSql = StrSql + "'Purchased Materials' AS A13, "
                StrSql = StrSql + "'Process' AS A14, "
                StrSql = StrSql + "'Transportation' AS A15, "
                StrSql = StrSql + "'Total Water' AS A16, "
                StrSql = StrSql + "(TOT.RMWATER*PREF.CONVGALLON) AS T1, "
                StrSql = StrSql + "(TOT.RMPACKWATER*PREF.CONVGALLON) AS T2, "
                StrSql = StrSql + "(TOT.RMANDPACKTRNSPTWATER*PREF.CONVGALLON) AS T3, "
                StrSql = StrSql + "(TOT.PROCWATER*PREF.CONVGALLON) AS T4, "
                StrSql = StrSql + "(TOT.DPPACKWATER*PREF.CONVGALLON) AS T5, "
                StrSql = StrSql + "(TOT.DPTRNSPTWATER*PREF.CONVGALLON) AS T6, "
                StrSql = StrSql + "(TOT.TRSPTTOCUSWATER*PREF.CONVGALLON) AS T7, "
                StrSql = StrSql + "(TOT.PROCWATERS2*PREF.CONVGALLON) AS T8, "
                StrSql = StrSql + "(TOT.PACKGEDPRODPACKWATER*PREF.CONVGALLON) AS T9, "
                StrSql = StrSql + "(TOT.PPPTRNSPTWATER*PREF.CONVGALLON) AS T10, "
                StrSql = StrSql + "(TOT.PACKGEDPRODTRNSPTWATER*PREF.CONVGALLON) AS T11, "
                StrSql = StrSql + "((TOT.RMWATER+TOT.RMPACKWATER+TOT.RMANDPACKTRNSPTWATER+TOT.PROCWATER+TOT.DPPACKWATER+TOT.DPTRNSPTWATER+TOT.TRSPTTOCUSWATER+TOT.PROCWATERS2+TOT.PACKGEDPRODPACKWATER+TOT.PPPTRNSPTWATER+TOT.PACKGEDPRODTRNSPTWATER)*PREF.CONVGALLON) T12, "
                StrSql = StrSql + "((TOT.RMWATER+TOT.RMPACKWATER+TOT.DPPACKWATER+TOT.PACKGEDPRODPACKWATER)*PREF.CONVGALLON) AS T13, "
                StrSql = StrSql + "(TOT.PROCWATER+TOT.PROCWATERS2)*PREF.CONVGALLON AS T14, "
                StrSql = StrSql + "((TOT.RMANDPACKTRNSPTWATER+TOT.DPTRNSPTWATER+TOT.TRSPTTOCUSWATER+TOT.PPPTRNSPTWATER+TOT.PACKGEDPRODTRNSPTWATER)*PREF.CONVGALLON) AS  T15, "
                StrSql = StrSql + "((TOT.RMWATER+TOT.RMPACKWATER+TOT.RMANDPACKTRNSPTWATER+TOT.PROCWATER+TOT.DPPACKWATER+TOT.DPTRNSPTWATER+TOT.TRSPTTOCUSWATER+TOT.PROCWATERS2+TOT.PACKGEDPRODPACKWATER+TOT.PPPTRNSPTWATER+TOT.PACKGEDPRODTRNSPTWATER)*PREF.CONVGALLON) T16, "
                StrSql = StrSql + "(CASE WHEN RSPL.PVOLUSE=0 THEN RSPL.VOLUME*PREF.CONVWT "
                StrSql = StrSql + "ELSE RSPL.SVOLUME*PREF.CONVWT END) AS SALESVOLUMELB, "

                StrSql = StrSql + "NVL((CASE WHEN RSPL.FINVOLMUNITS>0 THEN RSPL.FINVOLMUNITS END),0) SALESVOLUMEUNIT, "
                StrSql = StrSql + "'Sales Volume ('|| PREF.TITLE8||')' AS VOLUNI1, "

                StrSql = StrSql + "(CASE WHEN RSPL.FINVOLMUNITS > 0 THEN 'Sales Volume (units)' END) VOLUNI2, "
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
                StrSql = StrSql + "PREF.Convwt, "
                StrSql = StrSql + "PREF.UNITS "
                StrSql = StrSql + "FROM TOTAL TOT "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = TOT.CASEID "
                StrSql = StrSql + "INNER JOIN RESULTSPL RSPL "
                StrSql = StrSql + "ON RSPL.CASEID = TOT.CASEID "
                StrSql = StrSql + "WHERE TOT.CASEID =" + CaseId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetWaterResults:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        'Water End
#End Region

#Region "Error"
        Public Function GetErrors(ByVal ErrorCode As String) As DataSet
            Dim Dts As New DataSet()
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MoldS2ConnectionString")
                Dim StrSql As String = ""
                StrSql = "SELECT ERRORID,  "
                StrSql = StrSql + "ERRORCODE, "
                StrSql = StrSql + "ERRORDE1, "
                StrSql = StrSql + "ERRORDE2, "
                StrSql = StrSql + "ERRORTYPE, "
                StrSql = StrSql + "SHORTERROR "
                StrSql = StrSql + "FROM ERROR "
                StrSql = StrSql + "WHERE ERRORCODE='" + ErrorCode.ToString() + "' "
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
#End Region

#Region "Charts"
        Public Function GetChartErgyRes(ByVal CaseId1 As String, ByVal CaseId2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT  DISTINCT  "
                StrSql = StrSql + "TOTAL.CASEID, "
                StrSql = StrSql + "( CASE WHEN TOTAL.CASEID <= 1000 THEN "
                StrSql = StrSql + "( SELECT (BASECASES.CASEID||':'||REPLACE(REPLACE((REPLACE(NVL(BASECASES.CASEDE1,''),')','}')),'(','{')||' '||REPLACE((REPLACE(NVL(BASECASES.CASEDE2,''),')','}')),'(','{'),';',':') ) "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + "WHERE  BASECASES.CASEID = TOTAL.CASEID "
                StrSql = StrSql + ") ELSE "
                StrSql = StrSql + "( SELECT DISTINCT (PERMISSIONSCASES.CASEID||':'||REPLACE(REPLACE((REPLACE(NVL(PERMISSIONSCASES.CASEDE1,''),')','}')),'(','{') || ' ' || REPLACE((REPLACE(NVL(PERMISSIONSCASES.CASEDE2,''),')','}')),'(','{'),';',':') )  "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE  PERMISSIONSCASES.CASEID = TOTAL.CASEID "
                StrSql = StrSql + ") "
                StrSql = StrSql + "END  ) AS CASEDE, "
                StrSql = StrSql + "RESULTSPL.FINVOLMSI,"
                StrSql = StrSql + "RESULTSPL.FINVOLMUNITS,"
                StrSql = StrSql + "RESULTSPL.VOLUME, "
                StrSql = StrSql + "RESULTSPL.SVOLUME, "
                StrSql = StrSql + "RESULTSPL.PVOLUSE, "
                StrSql = StrSql + "RMERGY, "
                StrSql = StrSql + "RMPACKERGY, "
                StrSql = StrSql + "RMANDPACKTRNSPTERGY, "
                StrSql = StrSql + "PROCERGY, "
                StrSql = StrSql + "PROCERGYS2, "
                StrSql = StrSql + "DPPACKERGY, "
                StrSql = StrSql + "DPTRNSPTERGY, "
                StrSql = StrSql + "TRSPTTOCUS, "
                StrSql = StrSql + "PACKGEDPRODPACK, "
                StrSql = StrSql + "PPPTRNSPT, "
                StrSql = StrSql + "PACKGEDPRODTRNSPT, "
                StrSql = StrSql + "(RMERGY+RMPACKERGY+DPPACKERGY+PACKGEDPRODPACK)PURMATERIALERGY, "
                StrSql = StrSql + "(PROCERGY+PROCERGYS2)TPROCERGY, "
                StrSql = StrSql + "(RMANDPACKTRNSPTERGY+DPTRNSPTERGY+TRSPTTOCUS+PPPTRNSPT+PACKGEDPRODTRNSPT)TRNSPTERGY, "
                StrSql = StrSql + "(RMERGY+RMPACKERGY+RMANDPACKTRNSPTERGY+PROCERGY+DPPACKERGY+DPTRNSPTERGY+TRSPTTOCUS+PROCERGYS2+PACKGEDPRODPACK+PPPTRNSPT+PACKGEDPRODTRNSPT)TOTALENERGY "
                StrSql = StrSql + "FROM TOTAL "
                StrSql = StrSql + "INNER JOIN RESULTSPL "
                StrSql = StrSql + "ON RESULTSPL.CASEID = TOTAL.CASEID "
                StrSql = StrSql + "WHERE TOTAL.CASEID IN (" + CaseId1 + "," + CaseId2 + ") "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetChartErgyRes:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetChartGhgRes(ByVal CaseId1 As String, ByVal CaseId2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT  DISTINCT  "
                StrSql = StrSql + "TOTAL.CASEID, "
                StrSql = StrSql + "( CASE WHEN TOTAL.CASEID <= 1000 THEN "
                StrSql = StrSql + "( SELECT (BASECASES.CASEID||':'||REPLACE(REPLACE((REPLACE(NVL(BASECASES.CASEDE1,''),')','}')),'(','{')||' '||REPLACE((REPLACE(NVL(BASECASES.CASEDE2,''),')','}')),'(','{'),';',':') ) "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + "WHERE  BASECASES.CASEID = TOTAL.CASEID "
                StrSql = StrSql + ") ELSE "
                StrSql = StrSql + "( SELECT DISTINCT (PERMISSIONSCASES.CASEID||':'||REPLACE(REPLACE((REPLACE(NVL(PERMISSIONSCASES.CASEDE1,''),')','}')),'(','{') || ' ' || REPLACE((REPLACE(NVL(PERMISSIONSCASES.CASEDE2,''),')','}')),'(','{'),';',':') )  "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE  PERMISSIONSCASES.CASEID = TOTAL.CASEID "
                StrSql = StrSql + ") "
                StrSql = StrSql + "END  ) AS CASEDE, "
                StrSql = StrSql + "RESULTSPL.FINVOLMSI,"
                StrSql = StrSql + "RESULTSPL.FINVOLMUNITS,"
                StrSql = StrSql + "RESULTSPL.VOLUME, "
                StrSql = StrSql + "RESULTSPL.SVOLUME, "
                StrSql = StrSql + "RESULTSPL.PVOLUSE, "
                StrSql = StrSql + "RMGRNHUSGAS, "
                StrSql = StrSql + "RMPACKGRNHUSGAS, "
                StrSql = StrSql + "RMANDPACKTRNSPTGRNHUSGAS, "
                StrSql = StrSql + "PROCGRNHUSGAS, "
                StrSql = StrSql + "DPPACKGRNHUSGAS, "
                StrSql = StrSql + "DPTRNSPTGRNHUSGAS, "
                StrSql = StrSql + "TRSPTTOCUSGRNHUSGAS, "
                StrSql = StrSql + "PACKGEDPRODPACKGRNHUSGAS, "
                StrSql = StrSql + "PPPTRNSPTGRNHUSGAS, "
                StrSql = StrSql + "PACKGEDPRODTRNSPTGRNHUSGAS, "
                StrSql = StrSql + "PROCGRNHUSGASS2, "
                StrSql = StrSql + "(RMGRNHUSGAS+RMPACKGRNHUSGAS+DPPACKGRNHUSGAS+PACKGEDPRODPACKGRNHUSGAS)PURMATERIALGRNHUSGAS, "
                StrSql = StrSql + "(PROCGRNHUSGAS+PROCGRNHUSGASS2)PROCESSGRNHUSGAS, "
                StrSql = StrSql + "(RMANDPACKTRNSPTGRNHUSGAS+DPTRNSPTGRNHUSGAS+TRSPTTOCUSGRNHUSGAS+PPPTRNSPTGRNHUSGAS+PACKGEDPRODTRNSPTGRNHUSGAS)TRNSPTGRNHUSGAS, "
                StrSql = StrSql + "(RMGRNHUSGAS+RMPACKGRNHUSGAS+RMANDPACKTRNSPTGRNHUSGAS+PROCGRNHUSGAS+DPPACKGRNHUSGAS+DPTRNSPTGRNHUSGAS+TRSPTTOCUSGRNHUSGAS+PROCGRNHUSGASS2+PACKGEDPRODPACKGRNHUSGAS+PPPTRNSPTGRNHUSGAS+PACKGEDPRODTRNSPTGRNHUSGAS)TOTALENGRNHUSGAS "
                StrSql = StrSql + "FROM TOTAL "
                StrSql = StrSql + "INNER JOIN RESULTSPL "
                StrSql = StrSql + "ON RESULTSPL.CASEID = TOTAL.CASEID "
                StrSql = StrSql + "WHERE TOTAL.CASEID IN (" + CaseId1 + "," + CaseId2 + ") "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetChartGhgRes:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetChartWaterRes(ByVal CaseId1 As String, ByVal CaseId2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT  DISTINCT  "
                StrSql = StrSql + "TOTAL.CASEID, "
                'StrSql = StrSql + "( CASE WHEN TOTAL.CASEID <= 1000 THEN "
                'StrSql = StrSql + "( SELECT (BASECASES.CASEID||':'||BASECASES.CASEDE1||' '||BASECASES.CASEDE2 ) "
                'StrSql = StrSql + "FROM BASECASES "
                'StrSql = StrSql + "WHERE  BASECASES.CASEID = TOTAL.CASEID "
                'StrSql = StrSql + ") ELSE "
                'StrSql = StrSql + "( SELECT DISTINCT (PERMISSIONSCASES.CASEID||':'||NVL(PERMISSIONSCASES.CASEDE1,'') || ' ' || NVL(PERMISSIONSCASES.CASEDE2,'') ) "
                'StrSql = StrSql + "FROM PERMISSIONSCASES "
                'StrSql = StrSql + "WHERE  PERMISSIONSCASES.CASEID = TOTAL.CASEID "
                'StrSql = StrSql + ") "
                'StrSql = StrSql + "END  ) AS CASEDE, "
                StrSql = StrSql + "( CASE WHEN TOTAL.CASEID <= 1000 THEN "
                StrSql = StrSql + "( SELECT (BASECASES.CASEID||':'||REPLACE(REPLACE((REPLACE(NVL(BASECASES.CASEDE1,''),')','}')),'(','{')||' '||REPLACE((REPLACE(NVL(BASECASES.CASEDE2,''),')','}')),'(','{'),';',':') )  "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + "WHERE  BASECASES.CASEID = TOTAL.CASEID "
                StrSql = StrSql + ") ELSE "
                StrSql = StrSql + "( SELECT DISTINCT (PERMISSIONSCASES.CASEID||':'||REPLACE(REPLACE((REPLACE(NVL(PERMISSIONSCASES.CASEDE1,''),')','}')),'(','{') || ' ' || REPLACE((REPLACE(NVL(PERMISSIONSCASES.CASEDE2,''),')','}')),'(','{'),';',':') )  "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE  PERMISSIONSCASES.CASEID = TOTAL.CASEID "
                StrSql = StrSql + ") "
                StrSql = StrSql + "END  ) AS CASEDE, "
                StrSql = StrSql + "RESULTSPL.FINVOLMSI,"
                StrSql = StrSql + "RESULTSPL.FINVOLMUNITS,"
                StrSql = StrSql + "RESULTSPL.VOLUME, "
                StrSql = StrSql + "RESULTSPL.SVOLUME, "
                StrSql = StrSql + "RESULTSPL.PVOLUSE, "
                StrSql = StrSql + "RMWATER, "
                StrSql = StrSql + "RMPACKWATER, "
                StrSql = StrSql + "RMANDPACKTRNSPTWATER, "
                StrSql = StrSql + "PROCWATER, "
                StrSql = StrSql + "DPPACKWATER, "
                StrSql = StrSql + "DPTRNSPTWATER, "
                StrSql = StrSql + "TRSPTTOCUSWATER, "
                StrSql = StrSql + "PACKGEDPRODPACKWATER, "
                StrSql = StrSql + "PPPTRNSPTWATER, "
                StrSql = StrSql + "PACKGEDPRODTRNSPTWATER, "
                StrSql = StrSql + "PROCWATERS2, "
                StrSql = StrSql + "(RMWATER+RMPACKWATER+DPPACKWATER+PACKGEDPRODPACKWATER)PURMATERIALWATER, "
                StrSql = StrSql + "(PROCWATER+PROCWATERS2)PROCESSWATER, "
                StrSql = StrSql + "(RMANDPACKTRNSPTWATER+DPTRNSPTWATER+TRSPTTOCUSWATER+PPPTRNSPTWATER+PACKGEDPRODTRNSPTWATER)TRNSPTWATER, "
                StrSql = StrSql + "(RMWATER+RMPACKWATER+RMANDPACKTRNSPTWATER+PROCWATER+DPPACKWATER+DPTRNSPTWATER+TRSPTTOCUSWATER+PROCWATERS2+PACKGEDPRODPACKWATER+PPPTRNSPTWATER+PACKGEDPRODTRNSPTWATER)TOTALENWATER "
                StrSql = StrSql + "FROM TOTAL "
                StrSql = StrSql + "INNER JOIN RESULTSPL "
                StrSql = StrSql + "ON RESULTSPL.CASEID = TOTAL.CASEID "
                StrSql = StrSql + "WHERE TOTAL.CASEID IN (" + CaseId1 + "," + CaseId2 + ") "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("S1GetData:GetChartWaterRes:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetChartErgy() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT 'Raw Materials' AS TEXT,  "
                StrSql = StrSql + "'RMERGY' AS VALUE, "
                StrSql = StrSql + "1 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Raw Materials Packaging' AS TEXT, "
                StrSql = StrSql + "'RMPACKERGY' AS VALUE, "
                StrSql = StrSql + "2 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'RM & Pack Transport' AS TEXT, "
                StrSql = StrSql + "'RMANDPACKTRNSPTERGY' AS VALUE, "
                StrSql = StrSql + "3 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Process' AS TEXT, "
                StrSql = StrSql + "'PROCERGY' AS VALUE, "
                StrSql = StrSql + "4 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Distribution Packaging' AS TEXT, "
                StrSql = StrSql + "'DPPACKERGY' AS VALUE, "
                StrSql = StrSql + "5 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'DP Transport' AS TEXT, "
                StrSql = StrSql + "'DPTRNSPTERGY' AS VALUE, "
                StrSql = StrSql + "6 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Transport to Customer' AS TEXT, "
                StrSql = StrSql + "'TRSPTTOCUS' AS VALUE, "
                StrSql = StrSql + "7 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Process Energy(S2)' AS TEXT, "
                StrSql = StrSql + "'PROCERGYS2' AS VALUE, "
                StrSql = StrSql + "8 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Packaged Product Packaging(S2)' AS TEXT, "
                StrSql = StrSql + "'PACKGEDPRODPACK' AS VALUE, "
                StrSql = StrSql + "9 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'PPP Transport(S2)' AS TEXT, "
                StrSql = StrSql + "'PPPTRNSPT' AS VALUE, "
                StrSql = StrSql + "10 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Packaged Product Transport(S2)' AS TEXT, "
                StrSql = StrSql + "'PACKGEDPRODTRNSPT' AS VALUE, "
                StrSql = StrSql + "11 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Purchased Materials' AS TEXT, "
                StrSql = StrSql + "'PURMATERIALERGY' AS VALUE, "
                StrSql = StrSql + "12 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Total Process' AS TEXT, "
                StrSql = StrSql + "'TPROCERGY' AS VALUE, "
                StrSql = StrSql + "13 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Transportation' AS TEXT, "
                StrSql = StrSql + "'TRNSPTERGY' AS VALUE, "
                StrSql = StrSql + "14 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Total Energy' AS TEXT, "
                StrSql = StrSql + "'TOTALENERGY' AS VALUE, "
                StrSql = StrSql + "15 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "ORDER BY SEQ "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetChartErgy:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetChartGhg() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT 'Raw Materials' AS TEXT,  "
                StrSql = StrSql + "'RMGRNHUSGAS' AS VALUE, "
                StrSql = StrSql + "1 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Raw Materials Packaging' AS TEXT, "
                StrSql = StrSql + "'RMPACKGRNHUSGAS' AS VALUE, "
                StrSql = StrSql + "2 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'RM & Pack Transport' AS TEXT, "
                StrSql = StrSql + "'RMANDPACKTRNSPTGRNHUSGAS' AS VALUE, "
                StrSql = StrSql + "3 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Process' AS TEXT, "
                StrSql = StrSql + "'PROCGRNHUSGAS' AS VALUE, "
                StrSql = StrSql + "4 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Distribution Packaging' AS TEXT, "
                StrSql = StrSql + "'DPPACKGRNHUSGAS' AS VALUE, "
                StrSql = StrSql + "5 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'DP Transport' AS TEXT, "
                StrSql = StrSql + "'DPTRNSPTGRNHUSGAS' AS VALUE, "
                StrSql = StrSql + "6 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Transport to Customer' AS TEXT, "
                StrSql = StrSql + "'TRSPTTOCUSGRNHUSGAS' AS VALUE, "
                StrSql = StrSql + "7 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Process (S2)' AS TEXT, "
                StrSql = StrSql + "'PROCGRNHUSGASS2' AS VALUE, "
                StrSql = StrSql + "8 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Packaged Product Packaging(S2)' AS TEXT, "
                StrSql = StrSql + "'PACKGEDPRODPACKGRNHUSGAS' AS VALUE, "
                StrSql = StrSql + "9 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'PPP Transport(S2)' AS TEXT, "
                StrSql = StrSql + "'PPPTRNSPTGRNHUSGAS' AS VALUE, "
                StrSql = StrSql + "10 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Packaged Product Transport(S2)' AS TEXT, "
                StrSql = StrSql + "'PACKGEDPRODTRNSPTGRNHUSGAS' AS VALUE, "
                StrSql = StrSql + "11 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Purchased Materials' AS TEXT, "
                StrSql = StrSql + "'PURMATERIALGRNHUSGAS' AS VALUE, "
                StrSql = StrSql + "12 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Total Process' AS TEXT, "
                StrSql = StrSql + "'PROCESSGRNHUSGAS' AS VALUE, "
                StrSql = StrSql + "13 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Transportation' AS TEXT, "
                StrSql = StrSql + "'TRNSPTGRNHUSGAS' AS VALUE, "
                StrSql = StrSql + "14 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Total Greenhouse Gas' AS TEXT, "
                StrSql = StrSql + "'TOTALENGRNHUSGAS' AS VALUE, "
                StrSql = StrSql + "15 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "ORDER BY SEQ "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetChartGhg:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetChartWater() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT 'Raw Materials' AS TEXT,  "
                StrSql = StrSql + "'RMWATER' AS VALUE, "
                StrSql = StrSql + "1 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Raw Materials Packaging' AS TEXT, "
                StrSql = StrSql + "'RMPACKWATER' AS VALUE, "
                StrSql = StrSql + "2 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'RM & Pack Transport' AS TEXT, "
                StrSql = StrSql + "'RMANDPACKTRNSPTWATER' AS VALUE, "
                StrSql = StrSql + "3 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Process' AS TEXT, "
                StrSql = StrSql + "'PROCWATER' AS VALUE, "
                StrSql = StrSql + "4 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Distribution Packaging' AS TEXT, "
                StrSql = StrSql + "'DPPACKWATER' AS VALUE, "
                StrSql = StrSql + "5 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'DP Transport' AS TEXT, "
                StrSql = StrSql + "'DPTRNSPTWATER' AS VALUE, "
                StrSql = StrSql + "6 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Transport to Customer' AS TEXT, "
                StrSql = StrSql + "'TRSPTTOCUSWATER' AS VALUE, "
                StrSql = StrSql + "7 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Process (S2)' AS TEXT, "
                StrSql = StrSql + "'PROCWATERS2' AS VALUE, "
                StrSql = StrSql + "8 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Packaged Product Packaging(S2)' AS TEXT, "
                StrSql = StrSql + "'PACKGEDPRODPACKWATER' AS VALUE, "
                StrSql = StrSql + "9 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'PPP Transport(S2)' AS TEXT, "
                StrSql = StrSql + "'PPPTRNSPTWATER' AS VALUE, "
                StrSql = StrSql + "10 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Packaged Product Transport(S2)' AS TEXT, "
                StrSql = StrSql + "'PACKGEDPRODTRNSPTWATER' AS VALUE, "
                StrSql = StrSql + "11 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Purchased Materials' AS TEXT, "
                StrSql = StrSql + "'PURMATERIALWATER' AS VALUE, "
                StrSql = StrSql + "12 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Total Process' AS TEXT, "
                StrSql = StrSql + "'PROCESSWATER' AS VALUE, "
                StrSql = StrSql + "13 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Transportation' AS TEXT, "
                StrSql = StrSql + "'TRNSPTWATER' AS VALUE, "
                StrSql = StrSql + "14 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Total Water' AS TEXT, "
                StrSql = StrSql + "'TOTALENWATER' AS VALUE, "
                StrSql = StrSql + "15 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "ORDER BY SEQ "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("S1GetData:GetChartWater:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetChartPref(ByVal USERID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT  USERID,  "
                StrSql = StrSql + "UNITS, "
                StrSql = StrSql + "CURRENCY, "
                StrSql = StrSql + "CURR, "
                StrSql = StrSql + "CONVERSIONFACTOR, "
                StrSql = StrSql + "TITLE1, "
                StrSql = StrSql + "TITLE2, "
                StrSql = StrSql + "TITLE3, "
                StrSql = StrSql + "TITLE4, "
                StrSql = StrSql + "TITLE5, "
                StrSql = StrSql + "TITLE6, "
                StrSql = StrSql + "CONVAREA, "
                StrSql = StrSql + "TITLE7, "
                StrSql = StrSql + "TITLE8, "
                StrSql = StrSql + "TITLE9, "
                StrSql = StrSql + "TITLE10, "
                StrSql = StrSql + "TITLE11, "
                StrSql = StrSql + "TITLE12, "
                StrSql = StrSql + "CONVWT, "
                StrSql = StrSql + "CONVAREA2, "
                StrSql = StrSql + "CONVTHICK, "
                StrSql = StrSql + "CONVTHICK2, "
                StrSql = StrSql + "CONVTHICK3, "
                StrSql = StrSql + "CONVGALLON "
                StrSql = StrSql + "FROM CHARTPREFERENCES "
                StrSql = StrSql + "WHERE USERID='" + USERID.ToString() + "' "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetChartPref:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "LicenseAdministrator"
        Public Function GetPCaseDetailsByLicense(ByVal USERID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT DISTINCT CASEID,  "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "CASEDE2, "
                StrSql = StrSql + "CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "INNER JOIN ECON.USERS USERS "
                StrSql = StrSql + "ON USERS.USERID=PERMISSIONSCASES.USERID "
                StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS USERS WHERE USERID='" + USERID.ToString() + "') "
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID "


                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetPCaseDetailsByLicense:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCasesByLicense(ByVal USERID As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT CASEID,  "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "CASEDE2, "
                StrSql = StrSql + "CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "INNER JOIN ECON.USERS USERS "
                StrSql = StrSql + "ON USERS.USERID=PERMISSIONSCASES.USERID "
                StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS USERS WHERE USERID='" + USERID.ToString() + "') "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetCasesByLicense:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

#End Region

#Region "Case Grouping:"


        Public Function GetPCaseGrpDetails(ByVal UserName As String, ByVal UserID As String, ByVal keyWord As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT GROUPNAME,GROUPID,CASEID,CASEDE1,CASEDE2,CASEDE3,CASEDES,CREATIONDATE,SERVERDATE "
                StrSql = StrSql + " FROM "
                StrSql = StrSql + " ( "
                StrSql = StrSql + "SELECT CASE WHEN NVL(GROUPS.GROUPID,'0')='0' THEN 'None' ELSE GROUPS.DES1 END AS GROUPNAME,  "
                StrSql = StrSql + "NVL(GROUPS.GROUPID,'0') AS GROUPID, "
                StrSql = StrSql + "PC.CASEID, "
                StrSql = StrSql + "PC.CASEDE1, "
                StrSql = StrSql + "PC.CASEDE2, "
                StrSql = StrSql + "PC.CASEDE3, "
                StrSql = StrSql + "(PC.CASEID||'. PACKAGE FORMAT= '||PC.CASEDE1||' UNIQUE FEATURES= '||PC.CASEDE2)CASEDES, "

                StrSql = StrSql + " PC.CREATIONDATE, "
                StrSql = StrSql + "CASE WHEN PC.CREATIONDATE-PC.SERVERDATE =0 THEN 'NA' ELSE to_char(PC.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE "


                StrSql = StrSql + "FROM ECON2MOLD.GROUPS "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.GROUPCASES "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
                StrSql = StrSql + "AND GROUPS.USERID=" + UserID + " "
                StrSql = StrSql + "RIGHT OUTER JOIN PERMISSIONSCASES PC "
                StrSql = StrSql + "ON PC.CASEID=GROUPCASES.CASEID "
                StrSql = StrSql + "WHERE PC.UserID ='" + UserID.ToString() + "' "
                StrSql = StrSql + "ORDER BY CASEID DESC "
                StrSql = StrSql + " ) "

                StrSql = StrSql + "WHERE UPPER(CASEDE1) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE2) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEID) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE3) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(GROUPNAME) LIKE '%" + keyWord.ToUpper().Trim() + "%' "

                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetPCaseGrpDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetGroupIDByUSer(ByVal UserID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = ""
            Try

                StrSql = "SELECT GROUPID,  "
                StrSql = StrSql + "DES1, "
                StrSql = StrSql + "DES2, "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "CREATIONDATE, "
                StrSql = StrSql + "UPDATEDATE "
                StrSql = StrSql + "FROM GROUPS WHERE USERID= " + UserID + " "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetGroupIDByUSer:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetGroupDetails(ByVal UserID As String, ByVal flag As Char) As DataSet

            Dim Dts As New DataSet()
            Dim objGetData As New MoldE2GetData.Selectdata()
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim strSQL As String = String.Empty
            Dim CaseIDs As String = String.Empty
            Dim DtRes As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim dtOutPut As New DataSet()
            Dim strSqlOutPut As String = String.Empty
            ' MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MoldE1ConnectionString")
            Try


                'Getting Groups
                strSQL = "SELECT GROUPID,  "
                strSQL = strSQL + "DES1 GROUPNAME, "
                strSQL = strSQL + "DES2 GROUPDES, "
                strSQL = strSQL + "GROUPID || ':'|| DES1 CDES1,"
                strSQL = strSQL + "to_char(CREATIONDATE,'mm/dd/yyyy hh:mi:ss AM'  ) CREATIONDATE, "
                strSQL = strSQL + "CASE WHEN CREATIONDATE-UPDATEDATE =0 THEN 'NA' ELSE to_char(UPDATEDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  UPDATEDATE "
                strSQL = strSQL + "FROM "
                strSQL = strSQL + "GROUPS "
                strSQL = strSQL + "WHERE USERID= " + UserID + " "
                DtRes = odbUtil.FillDataSet(strSQL, MoldE2Connection)
                If DtRes.Tables(0).Rows.Count > 0 Then
                    For j = 0 To DtRes.Tables(0).Rows.Count - 1
                        'Getting CaseID
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "CASEID "
                        strSQL = strSQL + "FROM ECON2MOLD.GROUPCASES "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString()
                        strSQL = strSQL + " ORDER BY  SEQ"
                        Dts = odbUtil.FillDataSet(strSQL, MoldE2Connection)
                        If Dts.Tables(0).Rows.Count > 0 Then
                            For i = 0 To Dts.Tables(0).Rows.Count - 1
                                If i = 0 Then
                                    CaseIDs = Dts.Tables(0).Rows(i).Item("CASEID").ToString()
                                Else
                                    CaseIDs = CaseIDs + ", " + Dts.Tables(0).Rows(i).Item("CASEID").ToString()
                                End If
                            Next
                        Else
                            CaseIDs = ""
                        End If
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "DES1 GROUPNAME, "
                        strSQL = strSQL + "'" + CaseIDs.ToString() + "' CaseID, "
                        strSQL = strSQL + "DES2 GROUPDES, "
                        strSQL = strSQL + "GROUPID || ':'|| DES1 CDES1,"
                        strSQL = strSQL + "to_char(CREATIONDATE,'mm/dd/yyyy hh:mi:ss AM'  ) CREATIONDATE, "
                        strSQL = strSQL + "CASE WHEN CREATIONDATE-UPDATEDATE =0 THEN 'NA' ELSE to_char(UPDATEDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  UPDATEDATE "
                        strSQL = strSQL + "FROM "
                        strSQL = strSQL + "GROUPS "
                        strSQL = strSQL + "WHERE USERID= " + UserID + " AND GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString() + " "
                        If j = 0 Then
                            strSqlOutPut = strSQL
                        Else
                            strSqlOutPut = strSqlOutPut + " UNION ALL " + strSQL
                        End If
                    Next
                    If flag = "Y" Then ' Y FOR GROUPDEATILS PAGE ,N FOR EDITGROUPS PAGE
                        strSQL = "SELECT 0 GROUPID,  "
                        strSQL = strSQL + "'None'  GROUPNAME, "
                        strSQL = strSQL + "'NA' CaseID, "
                        strSQL = strSQL + "'NA'  GROUPDES, "
                        strSQL = strSQL + "'0:None'  CDES1, "
                        strSQL = strSQL + "'NA'  CREATIONDATE, "
                        strSQL = strSQL + "'NA'  UPDATEDATE "
                        strSQL = strSQL + "FROM "
                        strSQL = strSQL + "DUAL "
                        strSqlOutPut = strSqlOutPut + " UNION ALL " + strSQL
                    End If
                    strSqlOutPut = "SELECT * FROM ( " + strSqlOutPut + " ) DUAL ORDER BY UPPER(GROUPNAME),UPPER(GROUPDES) "

                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, MoldE2Connection)

                Else
                    strSqlOutPut = " SELECT * FROM GROUPCASES WHERE GROUPID=0 "
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, MoldE2Connection)
                End If
                Return dtOutPut
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function
        Public Function GetPCaseDetailsByGroup(ByVal USERID As String, ByVal grpId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PERMISSIONSCASES.CASEID,CASEDE1,CASEDE2,CASEDE3,(PERMISSIONSCASES.CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES,SEQ FROM PERMISSIONSCASES "
                StrSql = StrSql + " INNER JOIN ECON2MOLD.GROUPCASES ON GROUPCASES.CASEID=PERMISSIONSCASES.CASEID "
                StrSql = StrSql + "WHERE USERID ='" + USERID.ToString() + "'  AND GROUPCASES.GROUPID=" + grpId + " "
                StrSql = StrSql + " AND PERMISSIONSCASES.CASEID IN(SELECT CASEID FROM ECON2MOLD.GROUPCASES WHERE GROUPID =" + grpId + ")"
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID "

                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetPCaseDetailsByGroup:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetGroupCaseDetails(ByVal UserID As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New MoldE2GetData.Selectdata()
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim strSQL As String = String.Empty
            Dim ds As New DataSet()
            Dim CaseIDs As String = String.Empty
            Dim DtRes As New DataSet()
            Dim cnt As Integer = 0
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                'MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MoldE1ConnectionString")
                Dts = objGetData.GetGroupIDByUSer(UserID)
                If Dts.Tables(0).Rows.Count > 0 Then
                    For i = 0 To Dts.Tables(0).Rows.Count - 1
                        ds = objGetData.GetGroupCasesByUSer(Dts.Tables(0).Rows(i).Item("GROUPID").ToString())
                        If ds.Tables(0).Rows.Count > 0 Then
                            For j = 0 To ds.Tables(0).Rows.Count - 1
                                If j = 0 Then
                                    CaseIDs = ds.Tables(0).Rows(j).Item("CASEID").ToString()
                                Else
                                    CaseIDs = CaseIDs + " " + ds.Tables(0).Rows(j).Item("CASEID").ToString()
                                End If
                            Next
                        Else
                            CaseIDs = "0"
                        End If
                        If cnt = 0 Then
                            cnt += 1
                            'strSQL = "SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' || ' ,Cases:" + CaseIDs + "' AS GROUPDES,'" + CaseIDs + "' AS CASEIDS FROM DUAL "
                            strSQL = "SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '   ' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '   '|| '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "'  AS GROUPDES,'" + CaseIDs + "' AS CASEIDS,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS DES1,'" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS DES2,'" + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + "' || ':' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS CDES1 FROM DUAL "
                        Else
                            cnt += 1
                            strSQL = strSQL + "UNION ALL SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' ||  '   ' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '   '||  '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "'  AS GROUPDES,'" + CaseIDs + "' AS CASEIDS,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS DES1,'" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS DES2,'" + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + "' || ':' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS CDES1 FROM DUAL "
                        End If
                    Next
                    strSQL = " SELECT * FROM ( " + strSQL + " ) DUAL ORDER BY UPPER(DES1),UPPER(DES2)"
                End If
                If strSQL <> "" Then
                    DtRes = odbUtil.FillDataSet(strSQL, MoldE2Connection)
                Else
                    strSQL = "select * FROM GROUPS WHERE GROUPID=0"
                    DtRes = odbUtil.FillDataSet(strSQL, MoldE2Connection)
                End If
                Return DtRes
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function
        Public Function GetGroupCases(ByVal USERID As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String, ByVal groupID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE USERID ='" + USERID.ToString() + "' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "AND CASEID IN(SELECT CASEID FROM ECON2MOLD.GROUPCASES WHERE GROUPID=" + groupID + " ) "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetAllGroupDetails(ByVal UserID As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New E1GetData.Selectdata()
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim strSQL As String = String.Empty
            Dim ds As New DataSet()
            Dim CaseIDs As String = String.Empty
            Dim DtRes As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim MyConnectionString As String = ""
            Dim dtOutPut As New DataSet()
            Dim strSqlOutPut As String = String.Empty
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MoldE2ConnectionString")
            Try


                'Getting Groups
                strSQL = "SELECT GROUPID,  "
                strSQL = strSQL + "DES1 GROUPNAME, "
                strSQL = strSQL + "DES2 GROUPDES, "
                strSQL = strSQL + "GROUPID || ':'|| DES1 CDES1,"
                strSQL = strSQL + "to_char(CREATIONDATE,'mm/dd/yyyy hh:mi:ss AM'  ) CREATIONDATE, "
                strSQL = strSQL + "CASE WHEN CREATIONDATE-UPDATEDATE =0 THEN 'NA' ELSE to_char(UPDATEDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  UPDATEDATE "
                strSQL = strSQL + "FROM "
                strSQL = strSQL + "GROUPS "
                strSQL = strSQL + "WHERE USERID= " + UserID + " "
                DtRes = odbUtil.FillDataSet(strSQL, MoldE2Connection)
                If DtRes.Tables(0).Rows.Count > 0 Then
                    For j = 0 To DtRes.Tables(0).Rows.Count - 1
                        'Getting CaseID
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "CASEID "
                        strSQL = strSQL + "FROM ECON2MOLD.GROUPCASES "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString()
                        strSQL = strSQL + " ORDER BY  SEQ"
                        Dts = odbUtil.FillDataSet(strSQL, MoldE2Connection)
                        If Dts.Tables(0).Rows.Count > 0 Then
                            For i = 0 To Dts.Tables(0).Rows.Count - 1
                                If i = 0 Then
                                    CaseIDs = Dts.Tables(0).Rows(i).Item("CASEID").ToString()
                                Else
                                    CaseIDs = CaseIDs + ", " + Dts.Tables(0).Rows(i).Item("CASEID").ToString()
                                End If
                            Next
                        Else
                            CaseIDs = ""
                        End If
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "DES1 GROUPNAME, "
                        strSQL = strSQL + "'" + CaseIDs.ToString() + "' CASEID, "
                        strSQL = strSQL + "DES2 GROUPDES, "
                        strSQL = strSQL + "USR.UserName, "
                        strSQL = strSQL + "GROUPID || ':'|| DES1 CDES1,"
                        strSQL = strSQL + "to_char(GPS.CREATIONDATE,'mm/dd/yyyy hh:mi:ss AM'  ) CREATIONDATE, "
                        strSQL = strSQL + "CASE WHEN GPS.CREATIONDATE-GPS.UPDATEDATE =0 THEN 'NA' ELSE to_char(GPS.UPDATEDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  UPDATEDATE "
                        strSQL = strSQL + "FROM "
                        strSQL = strSQL + "GROUPS GPS "
                        strSQL = strSQL + "INNER JOIN ECON.USERS USR "
                        strSQL = strSQL + "ON  USR.USERID=GPS.USERID "
                        strSQL = strSQL + "WHERE GPS.USERID= " + UserID + " AND GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString() + " "
                        If j = 0 Then
                            strSqlOutPut = strSQL
                        Else
                            strSqlOutPut = strSqlOutPut + " UNION ALL " + strSQL
                        End If
                    Next

                    strSqlOutPut = "SELECT * FROM ( " + strSqlOutPut + " ) DUAL ORDER BY UPPER(GROUPNAME),UPPER(GROUPDES)"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, MoldE2Connection)
                Else
                    strSqlOutPut = " SELECT * FROM GROUPCASES WHERE GROUPID=0 "
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, MoldE2Connection)
                End If

                Return dtOutPut
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function
#End Region

#Region "Bemis"
        Public Function GetStatusForSister(ByVal Schema As String, ByVal CaseId As String, ByVal userName As String) As DataSet
            Dim Dts As New DataSet()
            Dim StrSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Dim CaseIds As String = String.Empty
            Dim con As String = ""
            Try
                If Schema = "S2" Then
                    con = MoldS2Connection
                End If
                StrSql = "SELECT CASEID FROM STATUSUPDATE WHERE "
                StrSql = StrSql + "CASEID = " + CaseId + " AND UPPER(USERNAME)='" + userName.ToUpper() + "' AND UPPER(STATUS)='SISTER CASE' ORDER BY DATED ASC"

                Dts = odbUtil.FillDataSet(StrSql, con)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetStatusDetailsByID:" + ex.Message.ToString())
            End Try
        End Function
        Public Function GetPCaseGrpDetailsBem(ByVal UserName As String, ByVal UserID As String, ByVal keyWord As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim CaseIds As String = ""
            Try
                CaseIds = GetPropCaseStatus(UserID)

                StrSql = "SELECT GROUPNAME,GROUPID,CASEID,CASEDE1,CASEDE2,CASEDE3,CASEDES,CREATIONDATE,SERVERDATE "
                StrSql = StrSql + " FROM "
                StrSql = StrSql + " ( "
                StrSql = StrSql + "SELECT CASE WHEN NVL(GROUPS.GROUPID,'0')='0' THEN 'None' ELSE GROUPS.DES1 END AS GROUPNAME,  "
                StrSql = StrSql + "NVL(GROUPS.GROUPID,'0') AS GROUPID, "
                StrSql = StrSql + "PC.CASEID, "
                StrSql = StrSql + "PC.CASEDE1, "
                StrSql = StrSql + "PC.CASEDE2, "
                StrSql = StrSql + "CAST(PC.CASEDE3 AS VARCHAR(4000))CASEDE3, "
                StrSql = StrSql + "(PC.CASEID||'. PACKAGE FORMAT= '||PC.CASEDE1||' UNIQUE FEATURES= '||PC.CASEDE2)CASEDES, "
                StrSql = StrSql + " PC.CREATIONDATE, "
                StrSql = StrSql + "CASE WHEN PC.CREATIONDATE-PC.SERVERDATE =0 THEN 'NA' ELSE to_char(PC.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE "
                StrSql = StrSql + "FROM ECON2MOLD.GROUPS "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.GROUPCASES "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
                StrSql = StrSql + "AND GROUPS.USERID=" + UserID + " "
                StrSql = StrSql + "RIGHT OUTER JOIN PERMISSIONSCASES PC "
                StrSql = StrSql + "ON PC.CASEID=GROUPCASES.CASEID "
                StrSql = StrSql + "WHERE UserID ='" + UserID.ToString() + "' "
                StrSql = StrSql + "AND NVL(PC.STATUSID,0) NOT IN(3,4) "

                StrSql = StrSql + "UNION ALL "

                StrSql = StrSql + "SELECT CASE WHEN NVL(GROUPS.GROUPID,'0')='0' THEN 'None' ELSE GROUPS.DES1 END AS GROUPNAME,  "
                StrSql = StrSql + "NVL(GROUPS.GROUPID,'0') AS GROUPID, "
                StrSql = StrSql + "PC.CASEID, "
                StrSql = StrSql + "PC.CASEDE1, "
                StrSql = StrSql + "PC.CASEDE2, "
                StrSql = StrSql + "CAST(PC.CASEDE3 AS VARCHAR(4000))CASEDE3, "
                StrSql = StrSql + "(PC.CASEID||'. PACKAGE FORMAT= '||PC.CASEDE1||' UNIQUE FEATURES= '||PC.CASEDE2)CASEDES, "
                StrSql = StrSql + " PC.CREATIONDATE, "
                StrSql = StrSql + "CASE WHEN PC.CREATIONDATE-PC.SERVERDATE =0 THEN 'NA' ELSE to_char(PC.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE "
                StrSql = StrSql + "FROM ECON2MOLD.GROUPS "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.GROUPCASES "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
                StrSql = StrSql + "AND GROUPS.USERID=" + UserID + " "
                StrSql = StrSql + "RIGHT OUTER JOIN PERMISSIONSCASES PC "
                StrSql = StrSql + "ON PC.CASEID=GROUPCASES.CASEID "
                StrSql = StrSql + "WHERE UserID ='" + UserID.ToString() + "' "
                StrSql = StrSql + "AND PC.CASEID in(" + CaseIds + ") "

                StrSql = StrSql + " ) "

                StrSql = StrSql + "WHERE UPPER(CASEDE1) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE2) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEID) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE3) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(GROUPNAME) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "ORDER BY CASEID DESC "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetPCaseGrpDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetPropCasesById(ByVal USERID As String, ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = StrSql + "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,MS.STATUS,PERMISSIONSCASES.STATUSID,USERID CaseOwner "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                StrSql = StrSql + "WHERE USERID ='" + USERID.ToString() + "' "
                StrSql = StrSql + "AND CASEID= " + CaseId + " "
                StrSql = StrSql + "AND NVL(PERMISSIONSCASES.STATUSID,0) NOT IN(3,4) "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetPropCasesById:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetGroupCaseDet(ByVal UserID As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New MoldE2GetData.Selectdata()
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim strSQL As String = String.Empty
            Dim strSQL2 As String = String.Empty
            Dim ds As New DataSet()
            Dim CaseIDs As String = String.Empty
            Dim DtRes As New DataSet()
            Dim cnt As Integer = 0
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                'MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MoldE1ConnectionString")
                Dts = objGetData.GetGroupIDByUSer(UserID)
                If Dts.Tables(0).Rows.Count > 0 Then
                    For i = 0 To Dts.Tables(0).Rows.Count - 1
                        ds = objGetData.GetGroupCasesByUSer(Dts.Tables(0).Rows(i).Item("GROUPID").ToString())
                        If ds.Tables(0).Rows.Count > 0 Then
                            For j = 0 To ds.Tables(0).Rows.Count - 1
                                If j = 0 Then
                                    CaseIDs = ds.Tables(0).Rows(j).Item("CASEID").ToString()
                                Else
                                    CaseIDs = CaseIDs + " " + ds.Tables(0).Rows(j).Item("CASEID").ToString()
                                End If
                            Next
                        Else
                            CaseIDs = "0"
                        End If
                        If cnt = 0 Then
                            cnt += 1
                            'strSQL = "SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' || ' ,Cases:" + CaseIDs + "' AS GROUPDES,'" + CaseIDs + "' AS CASEIDS FROM DUAL "
                            strSQL = "SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '   ' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '   '|| '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "'  AS GROUPDES,'" + CaseIDs + "' AS CASEIDS,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS DES1,'" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS DES2,'" + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + "' || ':' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS CDES1 FROM DUAL "
                        Else
                            cnt += 1
                            strSQL = strSQL + "UNION ALL SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' ||  '   ' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '   '||  '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "'  AS GROUPDES,'" + CaseIDs + "' AS CASEIDS,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS DES1,'" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS DES2,'" + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + "' || ':' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS CDES1 FROM DUAL "
                        End If
                    Next
                    strSQL = "SELECT * FROM (" + strSQL + ") DUAL "
                End If
                strSQL2 = "SELECT 0 GROUPID,'' AS GROUPNAME,'All Groups and All Cases'  AS GROUPDES,'' AS CASEIDS,'' AS DES1,'' AS DES2,'' AS CDES1 FROM DUAL "
                If strSQL <> "" Then
                    strSQL2 = strSQL2 + " UNION ALL " + strSQL
                    strSQL2 = "SELECT GROUPID,GROUPNAME,GROUPDES,CASEIDS,DES1,DES2,CDES1 FROM (" + strSQL2 + " )ORDER BY UPPER(DES1),UPPER(DES2) "
                    DtRes = odbUtil.FillDataSet(strSQL2, MoldE2Connection)
                Else
                    strSQL2 = strSQL2 + ""
                    DtRes = odbUtil.FillDataSet(strSQL2, MoldE2Connection)
                End If
                Return DtRes
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function
        Public Function GetApprovedCases(ByVal USERID As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  CASEID, CASEDES,CASEDE1, CASEDE2,CASEDE, STATUS,STATUSID,CaseOwner  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "(SELECT 0 CASEID, 'Select Case' CASEDES, 'Select Case' CASEDE1, ''CASEDE2, 'Select Case' CASEDE, '' STATUS,0 STATUSID,'None'  CaseOwner "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,MS.STATUS,PERMISSIONSCASES.STATUSID,PERMISSIONSCASES.USERID CaseOwner "
                StrSql = StrSql + "FROM PERMISSIONSCASES "

                StrSql = StrSql + "INNER JOIN ECON.USERS "
                StrSql = StrSql + "ON USERS.USERID=PERMISSIONSCASES.USERID "


                StrSql = StrSql + "INNER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "

                StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS WHERE USERID='" + USERID.ToUpper() + "') "
                ' StrSql = StrSql + "WHERE UPPER(USERNAME) ='" + UserName.ToUpper().ToString() + "' "

                StrSql = StrSql + "AND PERMISSIONSCASES.STATUSID=3) "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetAppCasesByLicense(ByVal USERID As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  CASEID, CASEDE1, CASEDE2, CASEDE3,CASEDE,CASEDES, STATUS,STATUSID,CaseOwner  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "(SELECT 0 CASEID,  'Select Case' CASEDE1, ''CASEDE2, ''CASEDE3,'Select Case' CASEDE,'Select Case' CASEDES, '' STATUS,0 STATUSID,'None'  CaseOwner "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION ALL "

                StrSql = StrSql + "SELECT DISTINCT CASEID,  "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "CASEDE2, "
                StrSql = StrSql + "CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,(CASEDE1||'  '||CASEDE2)CASEDES, "
                StrSql = StrSql + "MS.STATUS,PERMISSIONSCASES.STATUSID ,USERS.USERID CaseOwner "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "INNER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                StrSql = StrSql + "INNER JOIN ECON.USERS "
                StrSql = StrSql + "ON USERS.USERID=PERMISSIONSCASES.USERID "
                StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS WHERE USERID='" + USERID.ToString() + "') "

                StrSql = StrSql + "AND PERMISSIONSCASES.STATUSID=3) "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetCasesByLicense:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetPropCases(ByVal USERID As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim CaseIds As String = ""
            Try
                CaseIds = GetPropCaseStatus(USERID)

                StrSql = "SELECT  CASEID, CASEDES,CASEDE1, CASEDE2,CASEDE, STATUS,STATUSID,CaseOwner  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "(SELECT 0 CASEID, 'Select Case' CASEDES, 'Select Case' CASEDE1, ''CASEDE2, 'Select Case' CASEDE, '' STATUS,0 STATUSID,'None'  CaseOwner "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION ALL "


                StrSql = StrSql + "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,MS.STATUS,PERMISSIONSCASES.STATUSID,USERID CaseOwner "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                StrSql = StrSql + "WHERE USERID ='" + USERID.ToString() + "' "
                'StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                'StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(PERMISSIONSCASES.STATUSID,0) NOT IN(3,4) "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,MS.STATUS,PERMISSIONSCASES.STATUSID,USERID CaseOwner "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                StrSql = StrSql + "WHERE CaseId in(" + CaseIds + ") "
                StrSql = StrSql + "AND USERID ='" + USERID.ToString() + "' "
                StrSql = StrSql + ") "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetPropCaseStatus(ByVal USERID As String) As String
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim CaseIds As String = ""
            Dim ds As New DataSet()
            Dim i As Integer = 0
            Dim cnt As Integer = 0
            Try
                StrSql = StrSql + "SELECT CASEID "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE STATUSID=4 AND USERID='" + USERID.ToString() + "' "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                If Dts.Tables(0).Rows.Count > 0 Then
                    For i = 0 To Dts.Tables(0).Rows.Count - 1
                        ds = GetPropDisAppStatus(USERID, Dts.Tables(0).Rows(i).Item("CaseId").ToString())
                        If ds.Tables(0).Rows.Count = 0 Then
                            If cnt = 0 Then
                                CaseIds = Dts.Tables(0).Rows(0).Item("CaseId").ToString()
                            Else
                                CaseIds = CaseIds + "," + Dts.Tables(0).Rows(i).Item("CaseId").ToString()
                            End If
                            cnt += 1
                        End If
                    Next

                Else
                    CaseIds = "0"
                End If
                Return CaseIds
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetPropCaseStatus:" + ex.Message.ToString())
                Return CaseIds
            End Try
        End Function
        Public Function GetPropDisAppStatus(ByVal USERID As String, ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim CaseIds As String = ""
            Try
                StrSql = StrSql + "SELECT * "
                StrSql = StrSql + "FROM STATUSUPDATE "
                StrSql = StrSql + "WHERE USERID='" + USERID.ToString() + "' AND CASEID=" + CaseId.ToString() + " AND UPPER(STATUS)='APPROVED' ORDER BY DATED ASC "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                If Dts.Tables(0).Rows.Count > 0 Then

                Else
                    CaseIds = "0"
                End If
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetPropDisAppStatus:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetPropCasesByLicense(ByVal USERID As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim StrSqlN As String = String.Empty
            Dim CaseIds As String = ""

            Dim ds As New DataSet()
            Dim dsUsers As New DataSet()
            Dim i As Integer = 0
            Dim cnt As Integer = 0
            Dim j As Integer = 0
            Try
                StrSql = "SELECT  CASEID, CASEDE1, CASEDE2, CASEDE3,CASEDE,CASEDES, STATUS,STATUSID,CaseOwner  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "(SELECT 0 CASEID,  'Select Case' CASEDE1, ''CASEDE2, ''CASEDE3,'Select Case' CASEDE,'Select Case' CASEDES, '' STATUS,0 STATUSID,'None'  CaseOwner "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT DISTINCT CASEID,  "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "CASEDE2, "
                StrSql = StrSql + "CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,(CASEDE1||'  '||CASEDE2)CASEDES, "
                StrSql = StrSql + "MS.STATUS,PERMISSIONSCASES.STATUSID,USERS.USERID CaseOwner  "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                StrSql = StrSql + "INNER JOIN ECON.USERS "
                StrSql = StrSql + "ON USERS.USERID=PERMISSIONSCASES.USERID "
                StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS WHERE USERID='" + USERID.ToUpper() + "') "
                'StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                'StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(PERMISSIONSCASES.STATUSID,0) NOT IN(3) "
                'dsUsers = GetUserCompanyUsersBem(UserName)
                'If dsUsers.Tables(0).Rows.Count > 0 Then
                '    For j = 0 To dsUsers.Tables(0).Rows.Count - 1
                '        StrSqlN = "SELECT CASEID "
                '        StrSqlN = StrSqlN + "FROM PERMISSIONSCASES "
                '        StrSqlN = StrSqlN + "WHERE STATUSID=4 AND UPPER(USERNAME)='" + dsUsers.Tables(0).Rows(j).Item("USERNAME").ToString().ToUpper() + "' "
                '        Dts = odbUtil.FillDataSet(StrSqlN, MoldS2Connection)
                '        If Dts.Tables(0).Rows.Count > 0 Then
                '            For i = 0 To Dts.Tables(0).Rows.Count - 1
                '                ds = GetPropDisAppStatus(dsUsers.Tables(0).Rows(j).Item("USERNAME").ToString(), Dts.Tables(0).Rows(i).Item("CaseId").ToString())
                '                If ds.Tables(0).Rows.Count = 0 Then
                '                    StrSql = StrSql + "UNION ALL "
                '                    StrSql = StrSql + "SELECT CASEID,  "
                '                    StrSql = StrSql + "CASEDE1, "
                '                    StrSql = StrSql + "CASEDE2, "
                '                    StrSql = StrSql + "CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,(CASEDE1||'  '||CASEDE2)CASEDES, "
                '                    StrSql = StrSql + "MS.STATUS,PERMISSIONSCASES.STATUSID,USERNAME CaseOwner  "
                '                    StrSql = StrSql + "FROM PERMISSIONSCASES "
                '                    StrSql = StrSql + "LEFT OUTER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                '                    StrSql = StrSql + "WHERE CaseId =" + Dts.Tables(0).Rows(i).Item("CaseId").ToString() + " AND UPPER(USERNAME)='" + dsUsers.Tables(0).Rows(j).Item("USERNAME").ToString().ToUpper() + "' "
                '                End If
                '            Next
                '        End If
                '    Next
                'End If


                StrSql = StrSql + ") "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetPropCasesByLicense:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetStatusDetailsByID(ByVal CaseId As String, ByVal UserName As String) As DataSet
            Dim Dts As New DataSet()
            Dim StrSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Dim CaseIds As String = String.Empty
            Try
                StrSql = "SELECT CASEID,STATUS,DATED,ACTIONBY,COMMENTS FROM STATUSUPDATE WHERE "
                StrSql = StrSql + "CASEID = " + CaseId + " AND UPPER(USERNAME)='" + UserName.ToUpper() + "' ORDER BY DATED ASC "

                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetStatusDetailsByID:" + ex.Message.ToString())
            End Try
        End Function
        Public Function GetGrpStatusDetailsByID(ByVal CaseId As String, ByVal UserName As String) As DataSet
            Dim Dts As New DataSet()
            Dim StrSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Dim CaseIds As String = String.Empty
            Try
                StrSql = "SELECT CASEID,STATUS,DATED,ACTIONBY,COMMENTS FROM STATUSUPDATE WHERE "
                StrSql = StrSql + "CASEID = " + CaseId + " AND UPPER(USERNAME)='" + UserName.ToUpper() + "' ORDER BY DATED ASC "

                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetStatusDetailsByID:" + ex.Message.ToString())
            End Try
        End Function
        Public Function GetPermissionStatus(ByVal CaseId As Integer, ByVal UserName As String) As DataSet
            Try
                Dim Dts As New DataSet()
                Dim odbUtil As New DBUtil()
                Dim StrSql As String = ""
                StrSql = ""
                StrSql = "SELECT CASEID,STATUS,DATED,ACTIONBY,COMMENTS FROM STATUSUPDATE "
                StrSql = StrSql + "WHERE CASEID=" + CaseId.ToString() + " AND UPPER(ACTIONBY)='" + UserName.ToUpper() + "' ORDER BY DATED ASC  "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("S2Data:GetPermissionStatus:" + ex.Message.ToString())
            End Try
        End Function
        Public Function GetGroupPCases(ByVal USERID As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String, ByVal groupID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  CASEID, CASEDES,CASEDE1, CASEDE2,CASEDE, STATUS,STATUSID,CaseOwner  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "(SELECT 0 CASEID, 'Select Case' CASEDES, 'Select Case' CASEDE1, ''CASEDE2, 'Select Case' CASEDE, '' STATUS,0 STATUSID,'None'  CaseOwner "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,MS.STATUS,PERMISSIONSCASES.STATUSID,USERID CaseOwner "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                StrSql = StrSql + "WHERE USERID ='" + USERID.ToString() + "' "
                StrSql = StrSql + "AND CASEID IN(SELECT CASEID FROM ECON2MOLD.GROUPCASES WHERE GROUPID=" + groupID + " )) "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldS2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUserCompanyUsersBem(ByVal UserName As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERID,USERNAME FROM (  "

                StrSql = StrSql + "SELECT  0 USERID,'Select User' USERNAME FROM DUAL  "
                StrSql = StrSql + "UNION ALL  "
                StrSql = StrSql + "SELECT  USERS.USERID,  "
                StrSql = StrSql + "USERS.USERNAME "
                StrSql = StrSql + "FROM USERS "
                StrSql = StrSql + "INNER JOIN USERPERMISSIONS "
                StrSql = StrSql + "ON USERPERMISSIONS.USERID= USERS.USERID "
                StrSql = StrSql + "INNER JOIN SERVICES "
                StrSql = StrSql + "ON SERVICES.SERVICEID = USERPERMISSIONS.SERVICEID "
                StrSql = StrSql + "WHERE USERS.LICENSEID=(SELECT LICENSEID FROM USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper.ToString() + "') "
                StrSql = StrSql + "AND SERVICES.SERVICEDE IN ('MOLDE1','MOLDE2','MOLDS1','MOLDS2') "
                StrSql = StrSql + "AND UPPER(USERPERMISSIONS.USERROLE)='READWRITE' "
                StrSql = StrSql + "GROUP BY USERS.USERID,USERS.USERNAME) "
                StrSql = StrSql + "ORDER BY USERNAME "

                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldS2GetData:GetUserCompanyUsersBem:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

    End Class
End Class
