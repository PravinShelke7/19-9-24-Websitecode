Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System

Public Class MoldE2GetData
    Public Class Selectdata

        Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
        Dim MoldE2Connection As String = System.Configuration.ConfigurationManager.AppSettings("MoldE2ConnectionString")
        Dim MoldE1Connection As String = System.Configuration.ConfigurationManager.AppSettings("MoldE1ConnectionString")

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
                StrSql = StrSql + "FROM ULOGIN "
                StrSql = StrSql + "INNER JOIN USERS "
                StrSql = StrSql + "ON UPPER(USERS.USERNAME) = UPPER(ULOGIN.UNAME) "
                StrSql = StrSql + "AND UPPER(USERS.PASSWORD) = UPPER(ULOGIN.UPWD) "
                StrSql = StrSql + "INNER JOIN USERPERMISSIONS "
                StrSql = StrSql + "ON USERPERMISSIONS.USERID = USERS.USERID "
                StrSql = StrSql + "INNER JOIN SERVICES "
                StrSql = StrSql + "ON SERVICES.SERVICEID = USERPERMISSIONS.SERVICEID "
                StrSql = StrSql + "WHERE ULOGIN.ID = " + Id.ToString() + " "
                StrSql = StrSql + "AND SERVICES.SERVICEDE='MOLDE2' "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetUserDetails:" + ex.Message.ToString())
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
                Throw New Exception("E1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetTotalCaseCount(ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT COUNT(*) AS TOTALCOUNT FROM PERMISSIONSCASES  "
                StrSql = StrSql + "WHERE USERID=" + UserId + " "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetTotalCaseCount:" + ex.Message.ToString())
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
                Throw New Exception("MoldE2GetData:GetSelectedUserDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Supporting Assumptions Pages"
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

                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetCaseDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetBCaseDetails() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES FROM BASECASES "
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetBCaseDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPCaseDetails(ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE USERID =" + UserId + " "
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetPCaseDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetMaterials(ByVal MatId As Integer, ByVal MatDe1 As String, ByVal MatDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MATID, (MATDE1||'  '||MATDE2)MATDES,MATDE1,MATDE2,SG,price  "
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
                Throw New Exception("MoldE2GetData:GetMaterials:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetDept(ByVal ProcId As Integer, ByVal ProcDe1 As String, ByVal ProcDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim CaseId As String = HttpContext.Current.Session("MoldE2CaseId").ToString()
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

                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetDept:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetDept(ByVal ProcId As Integer, ByVal ProcDe1 As String, ByVal ProcDe2 As String, ByVal CaseId As String) As DataSet
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

                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetDept:" + ex.Message.ToString())
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
                StrSql = StrSql + "TITLE6, "
                StrSql = StrSql + "TITLE7, "
                StrSql = StrSql + "TITLE8, "
                StrSql = StrSql + "TITLE9, "
                StrSql = StrSql + "EFFDATE, "
                StrSql = StrSql + "TO_CHAR(EFFDATE,'MON DD,YYYY')AS EDATE, "
                StrSql = StrSql + "TITLE10, "
                StrSql = StrSql + "TITLE11, "
                StrSql = StrSql + "TITLE12, "
                StrSql = StrSql + "ERGYCALC, "
                StrSql = StrSql + "RESULTSPL.PVOLUSE "
                StrSql = StrSql + "FROM PREFERENCES "
                StrSql = StrSql + "INNER JOIN RESULTSPL "
                StrSql = StrSql + "ON RESULTSPL.CASEID = PREFERENCES.CASEID "
                StrSql = StrSql + "WHERE PREFERENCES.CASEID = " + CaseId.ToString() + " "


                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetPref:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCases(ByVal UserId As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE USERID =" + UserId + " "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID"
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetCases:" + ex.Message.ToString())
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
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetBCases:" + ex.Message.ToString())
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
                StrSql = StrSql + "(RESULTSPL.REVENUE*PREF.CURR)REVENUE, "
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
                StrSql = StrSql + "INNER JOIN RESULTSPL "
                StrSql = StrSql + "ON RESULTSPL.CASEID = PLANTCONFIG.CASEID "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = PLANTCONFIG.CASEID "
                StrSql = StrSql + "INNER JOIN PROCESS PROC1 "
                StrSql = StrSql + "ON PROC1.PROCID = PLANTCONFIG.M1 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC2 "
                StrSql = StrSql + "ON PROC2.PROCID = PLANTCONFIG.M2 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC3 "
                StrSql = StrSql + "ON PROC3.PROCID = PLANTCONFIG.M3 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC4 "
                StrSql = StrSql + "ON PROC4.PROCID = PLANTCONFIG.M4 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC5 "
                StrSql = StrSql + "ON PROC5.PROCID = PLANTCONFIG.M5 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC6 "
                StrSql = StrSql + "ON PROC6.PROCID = PLANTCONFIG.M6 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC7 "
                StrSql = StrSql + "ON PROC7.PROCID = PLANTCONFIG.M7 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC8 "
                StrSql = StrSql + "ON PROC8.PROCID = PLANTCONFIG.M8 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC9 "
                StrSql = StrSql + "ON PROC9.PROCID = PLANTCONFIG.M9 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC10 "
                StrSql = StrSql + "ON PROC10.PROCID = PLANTCONFIG.M10 "
                StrSql = StrSql + "WHERE PLANTCONFIG.CASEID  = " + CaseId.ToString() + ""
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetCaseViewer:" + ex.Message.ToString())
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
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetCaseNoteDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCountry() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT COUNTRYID,COUNTRYDE1 FROM COUNTRY ORDER BY COUNTRYDE1"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetCountry:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCountry(ByVal countryId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT COUNTRYID,COUNTRYDE1 FROM COUNTRY "
                StrSql = StrSql + "WHERE COUNTRYID = CASE WHEN " + countryId.ToString() + " = -1 THEN "
                StrSql = StrSql + "COUNTRYID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + countryId.ToString() + " "
                StrSql = StrSql + "END  ORDER BY COUNTRYDE1"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetCountry:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetEffDate() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT TO_CHAR(EFFDATE,'MON DD,YYYY')AS EDATE FROM EFFDATE ORDER BY EFFDATE DESC"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetEffDate:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCurrancy() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CURID,CURDE1 FROM CURRENCY ORDER BY CURDE1 "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetCurrancy:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCurrancy(ByVal currId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CURID,CURDE1 FROM CURRENCY "
                StrSql = StrSql + "WHERE CURID = CASE WHEN " + currId.ToString() + " = -1 THEN "
                StrSql = StrSql + "CURID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + currId.ToString() + " "
                StrSql = StrSql + "END  ORDER BY CURDE1"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetCurrancy:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCurrancyArch(ByVal CaseId As String, ByVal CurID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CURRENCYARCH.CURID,  "
                StrSql = StrSql + "CURRENCYARCH.CURPUSD, "
                StrSql = StrSql + "CURRENCYARCH.EFFDATE "
                StrSql = StrSql + "FROM ECON.CURRENCYARCH "
                StrSql = StrSql + "INNER JOIN ECON2MOLD.PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.EFFDATE = CURRENCYARCH.EFFDATE "
                StrSql = StrSql + "WHERE PREF.CASEID =" + CaseId + ""
                StrSql = StrSql + "AND CURRENCYARCH.CURID = " + CurID + ""
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetCurrancyArch:" + ex.Message.ToString())
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
                Throw New Exception("MoldE2GetData:GetConversionFactor:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetProductFormat(ByVal FormtId As Integer, ByVal FormtDe1 As String, ByVal FormtDe2 As String) As DataSet
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
                Throw New Exception("MoldE2GetData:GetProductFormat:" + ex.Message.ToString())
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
                Throw New Exception("MoldE2GetData:GetDeptPlantConfig:" + ex.Message.ToString())
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
                Throw New Exception("MoldE2GetData:GetEquipment:" + ex.Message.ToString())
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
                Throw New Exception("MoldE2GetData:GetSupportEquipment:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCountryTable(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select (CASE WHEN PREF.OCOUNTRY=0 THEN  "
                StrSql = StrSql + "'personnel' "
                StrSql = StrSql + "WHEN PREF.OCOUNTRY=1 THEN "
                StrSql = StrSql + "'personnelChina' "
                StrSql = StrSql + "WHEN PREF.OCOUNTRY=2 THEN "
                StrSql = StrSql + "'personnelUK' "
                StrSql = StrSql + "WHEN PREF.OCOUNTRY=3 THEN "
                StrSql = StrSql + "'personnelGermany' "
                StrSql = StrSql + "WHEN PREF.OCOUNTRY=4 THEN "
                StrSql = StrSql + "'personnelSKorea' "
                StrSql = StrSql + "END) AS COUNTRY, "
                StrSql = StrSql + "(CASE WHEN PREF.OCOUNTRY=0 THEN "
                StrSql = StrSql + "'persArchUS' "
                StrSql = StrSql + "WHEN PREF.OCOUNTRY=1 THEN "
                StrSql = StrSql + "'persArchChina' "
                StrSql = StrSql + "WHEN PREF.OCOUNTRY=2 THEN "
                StrSql = StrSql + "'persArchUK' "
                StrSql = StrSql + "WHEN PREF.OCOUNTRY=3 THEN "
                StrSql = StrSql + "'persArchGermany' "
                StrSql = StrSql + "WHEN PREF.OCOUNTRY=4 THEN "
                StrSql = StrSql + "'persArchSKorea' "
                StrSql = StrSql + "END) AS EFFCOUNTRY "
                StrSql = StrSql + "FROM PREFERENCES PREF "
                StrSql = StrSql + "WHERE CASEID = " + CaseId.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetEFFCOUNTRY:" + ex.Message.ToString())
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
                Throw New Exception("MoldE2GetData:GetPersonnelInfo:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCostTypeInfo(ByVal CostId As Integer, ByVal Costde1 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select  costID,costde1 ,replace(costde1,Chr(34) ,'##') costdes"
                StrSql = StrSql + " from  costTYPE"
                StrSql = StrSql + " WHERE costID = CASE WHEN " + CostId.ToString() + " = -1 THEN "
                StrSql = StrSql + "costID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + CostId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(costde1),'#') LIKE '" + Costde1.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY costde1"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetCostTypeInfo:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPallets(ByVal PalletId As Integer, ByVal PalDe1 As String, ByVal PalDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  PalletId, (palletde1 || ' ' ||  palletde2) PallteDes, palletde1,palletde2,replace((palletde1 || ' ' ||  palletde2),'" + Chr(34) + "','##') PallteDes1 "
                StrSql = StrSql + "FROM Pallet "
                StrSql = StrSql + "WHERE PalletId = CASE WHEN " + PalletId.ToString() + " = -1 THEN "
                StrSql = StrSql + "PalletId "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + PalletId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(palletde1),'#') LIKE '" + PalDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(palletde2),'#') LIKE '" + PalDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  palletde1"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetPallets:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetEcon1Cases(ByVal CaseIdE1 As Integer, ByVal CaseIdE2 As Integer, ByVal CaseDe1 As String, ByVal CaseDe2 As String, ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                If CaseIdE2 >= 1000 Then
                    If CaseIdE1 = -1 And CaseDe1 <> "Nothing" And CaseDe1 <> "Selected" Then
                        StrSql = "SELECT CaseId, (Casede1||'  '||Casede2)CASDES,Casede1,Casede2,replace((Casede1 || ' ' ||  Casede2),'''' ,'##') CASDES1  "
                        StrSql = StrSql + "FROM PermissionsCases "
                        StrSql = StrSql + "WHERE USERID=" + UserId + " AND NVL(UPPER(Casede1),'#') LIKE '" + CaseDe1.ToUpper() + "%' "
                        StrSql = StrSql + "AND NVL(UPPER(Casede2),'#') LIKE '" + CaseDe2.ToUpper() + "%' "
                        StrSql = StrSql + "Union  "
                        StrSql = StrSql + "SELECT 0 ,'Nothing Selected' AS CASDES,'Nothing' Casede1,'Selected' Casede2,'Nothing Selected' AS CASDES1 FROM DUAL  "
                    ElseIf CaseIdE1 = -1 And CaseDe1 = "Nothing" Or CaseDe1 = "Selected" Then
                        StrSql = StrSql + "SELECT 0 ,'Nothing Selected' AS CASDES,'Nothing' Casede1,'Selected' Casede2,'Nothing Selected' AS CASDES1 FROM DUAL  "
                    ElseIf CaseIdE1 = 0 Then
                        StrSql = StrSql + "SELECT 0 ,'Nothing Selected' AS CASDES,'Nothing' Casede1,'Selected' Casede2,'Nothing Selected' AS CASDES1 FROM DUAL  "
                    Else
                        StrSql = "SELECT CaseId, (Casede1||'  '||Casede2)CASDES,Casede1,Casede2,replace((Casede1 || ' ' ||  Casede2),'''' ,'##') CASDES1  "
                        StrSql = StrSql + "FROM PermissionsCases "
                        StrSql = StrSql + "WHERE USERID=" + UserId + " AND CaseId = CASE WHEN " + CaseIdE1.ToString() + " = -1 THEN "
                        StrSql = StrSql + "CaseId "
                        StrSql = StrSql + "ELSE "
                        StrSql = StrSql + "" + CaseIdE1.ToString() + " "
                        StrSql = StrSql + "END "
                        StrSql = StrSql + "AND NVL(UPPER(Casede1),'#') LIKE '" + CaseDe1.ToUpper() + "%' "
                        StrSql = StrSql + "AND NVL(UPPER(Casede2),'#') LIKE '" + CaseDe2.ToUpper() + "%' "
                    End If
                    StrSql = StrSql + "Order By  CASDES"
                    Dts = odbUtil.FillDataSet(StrSql, MoldE1Connection)
                    Return Dts
                Else
                    If CaseIdE1 = -1 Then
                        StrSql = "SELECT CaseId, (Casede1||'  '||Casede2)CASDES,Casede1,Casede2,replace((Casede1 || ' ' ||  Casede2),'''' ,'##') CASDES1  "
                        StrSql = StrSql + "FROM BaseCases "
                        StrSql = StrSql + "WHERE NVL(UPPER(Casede1),'#') LIKE '" + CaseDe1.ToUpper() + "%' "
                        StrSql = StrSql + "AND NVL(UPPER(Casede2),'#') LIKE '" + CaseDe2.ToUpper() + "%' "
                        StrSql = StrSql + "Union  "
                        StrSql = StrSql + "SELECT 0 ,'Nothing Selected' AS CASDES,'Nothing' Casede1,'Selected' Casede2,'Nothing Selected' AS CASDES1 FROM DUAL "
                    ElseIf CaseIdE1 = 0 Then
                        StrSql = StrSql + "SELECT 0 ,'Nothing Selected' AS CASDES,'Nothing' Casede1,'Selected' Casede2,'Nothing Selected' AS CASDES1 FROM DUAL "
                    Else
                        StrSql = "SELECT CaseId, (Casede1||'  '||Casede2)CASDES,Casede1,Casede2,replace((Casede1 || ' ' ||  Casede2),'''' ,'##') CASDES1  "
                        StrSql = StrSql + "FROM BaseCases "
                        StrSql = StrSql + "WHERE CaseId = CASE WHEN " + CaseIdE1.ToString() + " = -1 THEN "
                        StrSql = StrSql + "CaseId "
                        StrSql = StrSql + "ELSE "
                        StrSql = StrSql + "" + CaseIdE1.ToString() + " "
                        StrSql = StrSql + "END "
                        StrSql = StrSql + "AND NVL(UPPER(Casede1),'#') LIKE '" + CaseDe1.ToUpper() + "%' "
                        StrSql = StrSql + "AND NVL(UPPER(Casede2),'#') LIKE '" + CaseDe2.ToUpper() + "%' "
                    End If

                    StrSql = StrSql + "Order By  CASDES"
                    Dts = odbUtil.FillDataSet(StrSql, MoldE1Connection)
                    Return Dts
                End If

            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetEcon1Cases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function getUnits(ByVal caseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim QuId As New Integer
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "(PREF.TITLE2||'/'||	PREF.TITLE8) UNIT, "
                StrSql = StrSql + "'0' AS VAL "
                StrSql = StrSql + "FROM RESULTSPL "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=RESULTSPL.CASEID "
                StrSql = StrSql + "WHERE RESULTSPL.Caseid= " + caseId.ToString() + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(CASE  WHEN FINVOLMSI > 1 THEN "
                StrSql = StrSql + "PREF.TITLE6||'/'||	PREF.TITLE3 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "PREF.TITLE6||'/unit ' "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END)Unit, "
                StrSql = StrSql + "'1' AS VAL "
                StrSql = StrSql + "FROM RESULTSPL "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=RESULTSPL.CASEID "
                StrSql = StrSql + "WHERE RESULTSPL.Caseid= " + caseId.ToString() + " "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(PREF.TITLE2||'/thousand') UNIT, "
                StrSql = StrSql + "'2' AS VAL "
                StrSql = StrSql + "FROM PREFERENCES PREF "
                StrSql = StrSql + "WHERE PREF.Caseid= " + caseId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:getUnits:" + ex.Message.ToString())
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
                StrSql = StrSql + "(CASE WHEN NVL(Case1.PRODWT,0)=0 THEN  0 "
                StrSql = StrSql + "ELSE (Case1.PRODWT*PREF.CONVWT) End) WEIGHTS1, "
                StrSql = StrSql + "(CASE WHEN NVL(Case2.PRODWT,0)=0 THEN  0 "
                StrSql = StrSql + "ELSE (Case2.PRODWT*PREF.CONVWT) End) WEIGHTS2, "
                StrSql = StrSql + "(CASE WHEN NVL(Case3.PRODWT,0)=0 THEN  0 "
                StrSql = StrSql + "ELSE (Case3.PRODWT*PREF.CONVWT) End) WEIGHTS3, "
                StrSql = StrSql + "(CASE WHEN NVL(Case4.PRODWT,0)=0 THEN  0 "
                StrSql = StrSql + "ELSE (Case4.PRODWT*PREF.CONVWT) End) WEIGHTS4, "
                StrSql = StrSql + "(CASE WHEN NVL(Case5.PRODWT,0)=0 THEN  0 "
                StrSql = StrSql + "ELSE (Case5.PRODWT*PREF.CONVWT) End) WEIGHTS5, "
                StrSql = StrSql + "(CASE WHEN NVL(Case6.PRODWT,0)=0 THEN  0 "
                StrSql = StrSql + "ELSE (Case6.PRODWT*PREF.CONVWT) End) WEIGHTS6, "
                StrSql = StrSql + "(CASE WHEN NVL(Case7.PRODWT,0)=0 THEN  0 "
                StrSql = StrSql + "ELSE (Case7.PRODWT*PREF.CONVWT) End) WEIGHTS7, "
                StrSql = StrSql + "(CASE WHEN NVL(Case8.PRODWT,0)=0 THEN  0 "
                StrSql = StrSql + "ELSE (Case8.PRODWT*PREF.CONVWT) End) WEIGHTS8, "
                StrSql = StrSql + "(CASE WHEN NVL(Case9.PRODWT,0)=0 THEN  0 "
                StrSql = StrSql + "ELSE (Case9.PRODWT*PREF.CONVWT) End) WEIGHTS9, "
                StrSql = StrSql + "(CASE WHEN NVL(Case10.PRODWT,0)=0 THEN  0 "
                StrSql = StrSql + "ELSE (Case10.PRODWT*PREF.CONVWT) End) WEIGHTS10, "
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

                'StrSql = StrSql + "CASE WHEN MI.C1=0 THEN (SELECT PRICE FROM MATERIALS WHERE MATID=MI.M1)  "
                'StrSql = StrSql + "ELSE(CASE WHEN NVL(Price1.VOLUME,0)<=0 THEN 0 "
                'StrSql = StrSql + "ELSE ((Price1.REVENUE/Price1.VOLUME)*PREF.CURR/PREF.CONVWT) END)END PRICES1, "
                'StrSql = StrSql + "CASE WHEN MI.C2=0 THEN (SELECT PRICE FROM MATERIALS WHERE MATID=MI.M2) "
                'StrSql = StrSql + "ELSE(CASE WHEN NVL(Price2.VOLUME,0)<=0 THEN  0 "
                'StrSql = StrSql + "ELSE ((Price2.REVENUE/Price2.VOLUME)*PREF.CURR/PREF.CONVWT) END)END PRICES2, "
                'StrSql = StrSql + "CASE WHEN MI.C3=0 THEN (SELECT PRICE FROM MATERIALS WHERE MATID=MI.M3) "
                'StrSql = StrSql + "ELSE(CASE WHEN NVL(Price3.VOLUME,0)<=0 THEN  0 "
                'StrSql = StrSql + "ELSE ((Price3.REVENUE/Price3.VOLUME)*PREF.CURR/PREF.CONVWT) END)END PRICES3, "
                'StrSql = StrSql + "CASE WHEN MI.C4=0 THEN (SELECT PRICE FROM MATERIALS WHERE MATID=MI.M4) "
                'StrSql = StrSql + "ELSE(CASE WHEN NVL(Price4.VOLUME,0)<=0 THEN 0 "
                'StrSql = StrSql + "ELSE ((Price4.REVENUE/Price4.VOLUME)*PREF.CURR/PREF.CONVWT) END)END PRICES4, "
                'StrSql = StrSql + "CASE WHEN MI.C5=0 THEN (SELECT PRICE FROM MATERIALS WHERE MATID=MI.M5) "
                'StrSql = StrSql + "ELSE(CASE WHEN NVL(Price5.VOLUME,0)<=0 THEN  0 "
                'StrSql = StrSql + "ELSE ((Price5.REVENUE/Price5.VOLUME)*PREF.CURR/PREF.CONVWT) END)END PRICES5, "
                'StrSql = StrSql + "CASE WHEN MI.C6=0 THEN (SELECT PRICE FROM MATERIALS WHERE MATID=MI.M6) "
                'StrSql = StrSql + "ELSE(CASE WHEN NVL(Price6.VOLUME,0)<=0 THEN  0 "
                'StrSql = StrSql + "ELSE ((Price6.REVENUE/Price6.VOLUME)*PREF.CURR/PREF.CONVWT) END)END PRICES6, "
                'StrSql = StrSql + "CASE WHEN MI.C7=0 THEN (SELECT PRICE FROM MATERIALS WHERE MATID=MI.M7) "
                'StrSql = StrSql + "ELSE(CASE WHEN NVL(Price7.VOLUME,0)<=0 THEN  0 "
                'StrSql = StrSql + "ELSE ((Price7.REVENUE/Price7.VOLUME)*PREF.CURR/PREF.CONVWT) END)END PRICES7, "
                'StrSql = StrSql + "CASE WHEN MI.C8=0 THEN (SELECT PRICE FROM MATERIALS WHERE MATID=MI.M8) "
                'StrSql = StrSql + "ELSE(CASE WHEN NVL(Price8.VOLUME,0)<=0 THEN  0 "
                'StrSql = StrSql + "ELSE ((Price8.REVENUE/Price8.VOLUME)*PREF.CURR/PREF.CONVWT) END)END PRICES8, "
                'StrSql = StrSql + "CASE WHEN MI.C9=0 THEN (SELECT PRICE FROM MATERIALS WHERE MATID=MI.M9) "
                'StrSql = StrSql + "ELSE(CASE WHEN NVL(Price9.VOLUME,0)<=0 THEN  0 "
                'StrSql = StrSql + "ELSE ((Price9.REVENUE/Price9.VOLUME)*PREF.CURR/PREF.CONVWT) END)END PRICES9, "
                'StrSql = StrSql + "CASE WHEN MI.C10=0 THEN (SELECT PRICE FROM MATERIALS WHERE MATID=MI.M10) "
                'StrSql = StrSql + "ELSE(CASE WHEN NVL(Price10.VOLUME,0)<=0 THEN  0 "
                'StrSql = StrSql + "ELSE ((Price10.REVENUE/Price10.VOLUME)*PREF.CURR/PREF.CONVWT) END)END PRICES10, "

                StrSql = StrSql + "CASE WHEN MI.C1=0 THEN (SELECT NVL(PRICE,0)/PREF.CONVWT*PREF.CURR FROM MATERIALS WHERE MATID=MI.M1)  "
                StrSql = StrSql + "ELSE(CASE WHEN NVL(Price1.VOLUME,0)<=0 THEN 0 "
                StrSql = StrSql + "ELSE ((Price1.REVENUE/Price1.VOLUME)*PREF.CURR/PREF.CONVWT) END)END PRICES1, "
                StrSql = StrSql + "CASE WHEN MI.C2=0 THEN (SELECT NVL(PRICE,0)/PREF.CONVWT*PREF.CURR FROM MATERIALS WHERE MATID=MI.M2) "
                StrSql = StrSql + "ELSE(CASE WHEN NVL(Price2.VOLUME,0)<=0 THEN  0 "
                StrSql = StrSql + "ELSE ((Price2.REVENUE/Price2.VOLUME)*PREF.CURR/PREF.CONVWT) END)END PRICES2, "
                StrSql = StrSql + "CASE WHEN MI.C3=0 THEN (SELECT NVL(PRICE,0)/PREF.CONVWT*PREF.CURR FROM MATERIALS WHERE MATID=MI.M3) "
                StrSql = StrSql + "ELSE(CASE WHEN NVL(Price3.VOLUME,0)<=0 THEN  0 "
                StrSql = StrSql + "ELSE ((Price3.REVENUE/Price3.VOLUME)*PREF.CURR/PREF.CONVWT) END)END PRICES3, "
                StrSql = StrSql + "CASE WHEN MI.C4=0 THEN (SELECT NVL(PRICE,0)/PREF.CONVWT*PREF.CURR FROM MATERIALS WHERE MATID=MI.M4) "
                StrSql = StrSql + "ELSE(CASE WHEN NVL(Price4.VOLUME,0)<=0 THEN 0 "
                StrSql = StrSql + "ELSE ((Price4.REVENUE/Price4.VOLUME)*PREF.CURR/PREF.CONVWT) END)END PRICES4, "
                StrSql = StrSql + "CASE WHEN MI.C5=0 THEN (SELECT NVL(PRICE,0)/PREF.CONVWT*PREF.CURR FROM MATERIALS WHERE MATID=MI.M5) "
                StrSql = StrSql + "ELSE(CASE WHEN NVL(Price5.VOLUME,0)<=0 THEN  0 "
                StrSql = StrSql + "ELSE ((Price5.REVENUE/Price5.VOLUME)*PREF.CURR/PREF.CONVWT) END)END PRICES5, "
                StrSql = StrSql + "CASE WHEN MI.C6=0 THEN (SELECT NVL(PRICE,0)/PREF.CONVWT*PREF.CURR FROM MATERIALS WHERE MATID=MI.M6) "
                StrSql = StrSql + "ELSE(CASE WHEN NVL(Price6.VOLUME,0)<=0 THEN  0 "
                StrSql = StrSql + "ELSE ((Price6.REVENUE/Price6.VOLUME)*PREF.CURR/PREF.CONVWT) END)END PRICES6, "
                StrSql = StrSql + "CASE WHEN MI.C7=0 THEN (SELECT NVL(PRICE,0)/PREF.CONVWT*PREF.CURR FROM MATERIALS WHERE MATID=MI.M7) "
                StrSql = StrSql + "ELSE(CASE WHEN NVL(Price7.VOLUME,0)<=0 THEN  0 "
                StrSql = StrSql + "ELSE ((Price7.REVENUE/Price7.VOLUME)*PREF.CURR/PREF.CONVWT) END)END PRICES7, "
                StrSql = StrSql + "CASE WHEN MI.C8=0 THEN (SELECT NVL(PRICE,0)/PREF.CONVWT*PREF.CURR FROM MATERIALS WHERE MATID=MI.M8) "
                StrSql = StrSql + "ELSE(CASE WHEN NVL(Price8.VOLUME,0)<=0 THEN  0 "
                StrSql = StrSql + "ELSE ((Price8.REVENUE/Price8.VOLUME)*PREF.CURR/PREF.CONVWT) END)END PRICES8, "
                StrSql = StrSql + "CASE WHEN MI.C9=0 THEN (SELECT NVL(PRICE,0)/PREF.CONVWT*PREF.CURR FROM MATERIALS WHERE MATID=MI.M9) "
                StrSql = StrSql + "ELSE(CASE WHEN NVL(Price9.VOLUME,0)<=0 THEN  0 "
                StrSql = StrSql + "ELSE ((Price9.REVENUE/Price9.VOLUME)*PREF.CURR/PREF.CONVWT) END)END PRICES9, "
                StrSql = StrSql + "CASE WHEN MI.C10=0 THEN (SELECT NVL(PRICE,0)/PREF.CONVWT*PREF.CURR FROM MATERIALS WHERE MATID=MI.M10) "
                StrSql = StrSql + "ELSE(CASE WHEN NVL(Price10.VOLUME,0)<=0 THEN  0 "
                StrSql = StrSql + "ELSE ((Price10.REVENUE/Price10.VOLUME)*PREF.CURR/PREF.CONVWT) END)END PRICES10, "

                StrSql = StrSql + "(MI.S1/PREF.CONVWT*PREF.CURR) AS PRICEP1, "
                StrSql = StrSql + "(MI.S2/PREF.CONVWT*PREF.CURR) AS PRICEP2, "
                StrSql = StrSql + "(MI.S3/PREF.CONVWT*PREF.CURR) AS PRICEP3, "
                StrSql = StrSql + "(MI.S4/PREF.CONVWT*PREF.CURR) AS PRICEP4, "
                StrSql = StrSql + "(MI.S5/PREF.CONVWT*PREF.CURR) AS PRICEP5, "
                StrSql = StrSql + "(MI.S6/PREF.CONVWT*PREF.CURR) AS PRICEP6, "
                StrSql = StrSql + "(MI.S7/PREF.CONVWT*PREF.CURR) AS PRICEP7, "
                StrSql = StrSql + "(MI.S8/PREF.CONVWT*PREF.CURR) AS PRICEP8, "
                StrSql = StrSql + "(MI.S9/PREF.CONVWT*PREF.CURR) AS PRICEP9, "
                StrSql = StrSql + "(MI.S10/PREF.CONVWT*PREF.CURR) AS PRICEP10, "
                StrSql = StrSql + "(MI.T1*PREF.convthick) AS THICK1, "
                StrSql = StrSql + "(MI.T2*PREF.convthick) AS THICK2, "
                StrSql = StrSql + "(MI.T3*PREF.convthick) AS THICK3, "
                StrSql = StrSql + "(MI.T4*PREF.convthick) AS THICK4, "
                StrSql = StrSql + "(MI.T5*PREF.convthick) AS THICK5, "
                StrSql = StrSql + "(MI.T6*PREF.convthick) AS THICK6, "
                StrSql = StrSql + "(MI.T7*PREF.convthick) AS THICK7, "
                StrSql = StrSql + "(MI.T8*PREF.convthick) AS THICK8, "
                StrSql = StrSql + "(MI.T9*PREF.convthick) AS THICK9, "
                StrSql = StrSql + "(MI.T10*PREF.convthick) AS THICK10, "
                StrSql = StrSql + "MI.IP1 AS PCOMP1, "
                StrSql = StrSql + "MI.IP2 AS PCOMP2, "
                StrSql = StrSql + "MI.IP3 AS PCOMP3, "
                StrSql = StrSql + "MI.IP4 AS PCOMP4, "
                StrSql = StrSql + "MI.IP5 AS PCOMP5, "
                StrSql = StrSql + "MI.IP6 AS PCOMP6, "
                StrSql = StrSql + "MI.IP7 AS PCOMP7, "
                StrSql = StrSql + "MI.IP8 AS PCOMP8, "
                StrSql = StrSql + "MI.IP9 AS PCOMP9, "
                StrSql = StrSql + "MI.IP10 AS PCOMP10, "
                StrSql = StrSql + "MI.R1 AS REC1, "
                StrSql = StrSql + "MI.R2 AS REC2, "
                StrSql = StrSql + "MI.R3 AS REC3, "
                StrSql = StrSql + "MI.R4 AS REC4, "
                StrSql = StrSql + "MI.R5 AS REC5, "
                StrSql = StrSql + "MI.R6 AS REC6, "
                StrSql = StrSql + "MI.R7 AS REC7, "
                StrSql = StrSql + "MI.R8 AS REC8, "
                StrSql = StrSql + "MI.R9 AS REC9, "
                StrSql = StrSql + "MI.R10 AS REC10, "
                StrSql = StrSql + "MI.E1 AS EP1, "
                StrSql = StrSql + "MI.E2 AS EP2, "
                StrSql = StrSql + "MI.E3 AS EP3, "
                StrSql = StrSql + "MI.E4 AS EP4, "
                StrSql = StrSql + "MI.E5 AS EP5, "
                StrSql = StrSql + "MI.E6 AS EP6, "
                StrSql = StrSql + "MI.E7 AS EP7, "
                StrSql = StrSql + "MI.E8 AS EP8, "
                StrSql = StrSql + "MI.E9 AS EP9, "
                StrSql = StrSql + "MI.E10 AS EP10, "
                StrSql = StrSql + "MO.P1 AS WP1, "
                StrSql = StrSql + "MO.P2 AS WP2, "
                StrSql = StrSql + "MO.P3 AS WP3, "
                StrSql = StrSql + "MO.P4 AS WP4, "
                StrSql = StrSql + "MO.P5 AS WP5, "
                StrSql = StrSql + "MO.P6 AS WP6, "
                StrSql = StrSql + "MO.P7 AS WP7, "
                StrSql = StrSql + "MO.P8 AS WP8, "
                StrSql = StrSql + "MO.P9 AS WP9, "
                StrSql = StrSql + "MO.P10 AS WP10, "
                StrSql = StrSql + "MI.D1 AS DEP1, "
                StrSql = StrSql + "MI.D2 AS DEP2, "
                StrSql = StrSql + "MI.D3 AS DEP3, "
                StrSql = StrSql + "MI.D4 AS DEP4, "
                StrSql = StrSql + "MI.D5 AS DEP5, "
                StrSql = StrSql + "MI.D6 AS DEP6, "
                StrSql = StrSql + "MI.D7 AS DEP7, "
                StrSql = StrSql + "MI.D8 AS DEP8, "
                StrSql = StrSql + "MI.D9 AS DEP9, "
                StrSql = StrSql + "MI.D10 AS DEP10, "
                StrSql = StrSql + "TOTAL.THICK*PREF.CONVWT AS TOTWGHT, "
                StrSql = StrSql + "MI.PLATE, "
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
                StrSql = StrSql + "ON PREF.CASEID=MI.CASEID "
                StrSql = StrSql + "INNER JOIN MATERIALOUTPUT MO "
                StrSql = StrSql + "ON MO.CASEID=MI.CASEID "
                StrSql = StrSql + "INNER JOIN TOTAL ON "
                StrSql = StrSql + "TOTAL.CASEID=MI.CASEID "
                StrSql = StrSql + "Left Join ECON1MOLD.Total Case1 "
                StrSql = StrSql + "on Case1.CaseId=MI.C1 "
                StrSql = StrSql + "Left Join ECON1MOLD.Total Case2 "
                StrSql = StrSql + "on Case2.CaseId=MI.C2 "
                StrSql = StrSql + "Left Join ECON1MOLD.Total Case3 "
                StrSql = StrSql + "on Case3.CaseId=MI.C3 "
                StrSql = StrSql + "Left Join ECON1MOLD.Total Case4 "
                StrSql = StrSql + "on Case4.CaseId=MI.C4 "
                StrSql = StrSql + "Left Join ECON1MOLD.Total Case5 "
                StrSql = StrSql + "on Case5.CaseId=MI.C5 "
                StrSql = StrSql + "Left Join ECON1MOLD.Total Case6 "
                StrSql = StrSql + "on Case6.CaseId=MI.C6 "
                StrSql = StrSql + "Left Join ECON1MOLD.Total Case7 "
                StrSql = StrSql + "on Case7.CaseId=MI.C7 "
                StrSql = StrSql + "Left Join ECON1MOLD.Total Case8 "
                StrSql = StrSql + "on Case8.CaseId=MI.C8 "
                StrSql = StrSql + "Left Join ECON1MOLD.Total Case9 "
                StrSql = StrSql + "on Case9.CaseId=MI.C9 "
                StrSql = StrSql + "Left Join ECON1MOLD.Total Case10 "
                StrSql = StrSql + "on Case10.CaseId=MI.C10 "
                StrSql = StrSql + "Left Join ECON1MOLD.RESULTSPL Price1 "
                StrSql = StrSql + "on Price1.CaseId=MI.C1 "
                StrSql = StrSql + "Left Join ECON1MOLD.RESULTSPL Price2 "
                StrSql = StrSql + "on Price2.CaseId=MI.C2 "
                StrSql = StrSql + "Left Join ECON1MOLD.RESULTSPL Price3 "
                StrSql = StrSql + "on Price3.CaseId=MI.C3 "
                StrSql = StrSql + "Left Join ECON1MOLD.RESULTSPL Price4 "
                StrSql = StrSql + "on Price4.CaseId=MI.C4 "
                StrSql = StrSql + "Left Join ECON1MOLD.RESULTSPL Price5 "
                StrSql = StrSql + "on Price5.CaseId=MI.C5 "
                StrSql = StrSql + "Left Join ECON1MOLD.RESULTSPL Price6 "
                StrSql = StrSql + "on Price6.CaseId=MI.C6 "
                StrSql = StrSql + "Left Join ECON1MOLD.RESULTSPL Price7 "
                StrSql = StrSql + "on Price7.CaseId=MI.C7 "
                StrSql = StrSql + "Left Join ECON1MOLD.RESULTSPL Price8 "
                StrSql = StrSql + "on Price8.CaseId=MI.C8 "
                StrSql = StrSql + "Left Join ECON1MOLD.RESULTSPL Price9 "
                StrSql = StrSql + "on Price9.CaseId=MI.C9 "
                StrSql = StrSql + "Left Join ECON1MOLD.RESULTSPL Price10 "
                StrSql = StrSql + "on Price10.CaseId=MI.C10 "
                StrSql = StrSql + "WHERE MI.CASEID =" + CaseId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetExtrusionDetails:" + ex.Message.ToString())
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
                StrSql = StrSql + "AS RD, "
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
                StrSql = StrSql + "LEFT OUTER JOIN PRODUCTFORMAT "
                StrSql = StrSql + "ON PRODUCTFORMAT.FORMATID = PF.M1 "
                StrSql = StrSql + "AND PREF.UNITS = 0 "
                StrSql = StrSql + "LEFT OUTER JOIN PRODUCTFORMAT2 "
                StrSql = StrSql + "ON PRODUCTFORMAT2.FORMATID = PF.M1 "
                StrSql = StrSql + "AND PREF.UNITS = 1 "
                StrSql = StrSql + "INNER JOIN TOTAL "
                StrSql = StrSql + "ON TOTAL.CASEID=PF.CASEID "
                StrSql = StrSql + "WHERE PF.CASEID  = " + CaseId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetProductFromatIn:" + ex.Message.ToString())
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
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetPalletAndTruck:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPlantConfigDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "PLANTCONFIG.CASEID, "
                StrSql = StrSql + "PLANTCONFIG.M1 AS D1P1, "
                StrSql = StrSql + "PLANTCONFIG.M2 AS D2P1, "
                StrSql = StrSql + "PLANTCONFIG.M3 AS D3P1, "
                StrSql = StrSql + "PLANTCONFIG.M4 AS D4P1, "
                StrSql = StrSql + "PLANTCONFIG.M5 AS D5P1, "
                StrSql = StrSql + "PLANTCONFIG.M6 AS D6P1, "
                StrSql = StrSql + "PLANTCONFIG.M7 AS D7P1, "
                StrSql = StrSql + "PLANTCONFIG.M8 AS D8P1, "
                StrSql = StrSql + "PLANTCONFIG.M9 AS D9P1, "
                StrSql = StrSql + "PLANTCONFIG.M10 AS D10P1, "
                StrSql = StrSql + "PLANTCONFIG.T1 AS D1P2, "
                StrSql = StrSql + "PLANTCONFIG.T2 AS D2P2, "
                StrSql = StrSql + "PLANTCONFIG.T3 AS D3P2, "
                StrSql = StrSql + "PLANTCONFIG.T4 AS D4P2, "
                StrSql = StrSql + "PLANTCONFIG.T5 AS D5P2, "
                StrSql = StrSql + "PLANTCONFIG.T6 AS D6P2, "
                StrSql = StrSql + "PLANTCONFIG.T7 AS D7P2, "
                StrSql = StrSql + "PLANTCONFIG.T8 AS D8P2, "
                StrSql = StrSql + "PLANTCONFIG.T9 AS D9P2, "
                StrSql = StrSql + "PLANTCONFIG.T10 AS D10P2, "
                StrSql = StrSql + "PLANTCONFIG.S1 AS D1P3, "
                StrSql = StrSql + "PLANTCONFIG.S2 AS D2P3, "
                StrSql = StrSql + "PLANTCONFIG.S3 AS D3P3, "
                StrSql = StrSql + "PLANTCONFIG.S4 AS D4P3, "
                StrSql = StrSql + "PLANTCONFIG.S5 AS D5P3, "
                StrSql = StrSql + "PLANTCONFIG.S6 AS D6P3, "
                StrSql = StrSql + "PLANTCONFIG.S7 AS D7P3, "
                StrSql = StrSql + "PLANTCONFIG.S8 AS D8P3, "
                StrSql = StrSql + "PLANTCONFIG.S9 AS D9P3, "
                StrSql = StrSql + "PLANTCONFIG.S10 AS D10P3, "
                StrSql = StrSql + "PLANTCONFIG.Y1 AS D1P4, "
                StrSql = StrSql + "PLANTCONFIG.Y2 AS D2P4, "
                StrSql = StrSql + "PLANTCONFIG.Y3 AS D3P4, "
                StrSql = StrSql + "PLANTCONFIG.Y4 AS D4P4, "
                StrSql = StrSql + "PLANTCONFIG.Y5 AS D5P4, "
                StrSql = StrSql + "PLANTCONFIG.Y6 AS D6P4, "
                StrSql = StrSql + "PLANTCONFIG.Y7 AS D7P4, "
                StrSql = StrSql + "PLANTCONFIG.Y8 AS D8P4, "
                StrSql = StrSql + "PLANTCONFIG.Y9 AS D9P4, "
                StrSql = StrSql + "PLANTCONFIG.Y10 AS D10P4, "
                StrSql = StrSql + "PLANTCONFIG.D1 AS D1P5, "
                StrSql = StrSql + "PLANTCONFIG.D2 AS D2P5, "
                StrSql = StrSql + "PLANTCONFIG.D3 AS D3P5, "
                StrSql = StrSql + "PLANTCONFIG.D4 AS D4P5, "
                StrSql = StrSql + "PLANTCONFIG.D5 AS D5P5, "
                StrSql = StrSql + "PLANTCONFIG.D6 AS D6P5, "
                StrSql = StrSql + "PLANTCONFIG.D7 AS D7P5, "
                StrSql = StrSql + "PLANTCONFIG.D8 AS D8P5, "
                StrSql = StrSql + "PLANTCONFIG.D9 AS D9P5, "
                StrSql = StrSql + "PLANTCONFIG.D10 AS D10P5, "
                StrSql = StrSql + "PLANTCONFIG.Z1 AS D1P6, "
                StrSql = StrSql + "PLANTCONFIG.Z2 AS D2P6, "
                StrSql = StrSql + "PLANTCONFIG.Z3 AS D3P6, "
                StrSql = StrSql + "PLANTCONFIG.Z4 AS D4P6, "
                StrSql = StrSql + "PLANTCONFIG.Z5 AS D5P6, "
                StrSql = StrSql + "PLANTCONFIG.Z6 AS D6P6, "
                StrSql = StrSql + "PLANTCONFIG.Z7 AS D7P6, "
                StrSql = StrSql + "PLANTCONFIG.Z8 AS D8P6, "
                StrSql = StrSql + "PLANTCONFIG.Z9 AS D9P6, "
                StrSql = StrSql + "PLANTCONFIG.Z10 AS D10P6, "
                StrSql = StrSql + "PLANTCONFIG.B1 AS D1P7, "
                StrSql = StrSql + "PLANTCONFIG.B2 AS D2P7, "
                StrSql = StrSql + "PLANTCONFIG.B3 AS D3P7, "
                StrSql = StrSql + "PLANTCONFIG.B4 AS D4P7, "
                StrSql = StrSql + "PLANTCONFIG.B5 AS D5P7, "
                StrSql = StrSql + "PLANTCONFIG.B6 AS D6P7, "
                StrSql = StrSql + "PLANTCONFIG.B7 AS D7P7, "
                StrSql = StrSql + "PLANTCONFIG.B8 AS D8P7, "
                StrSql = StrSql + "PLANTCONFIG.B9 AS D9P7, "
                StrSql = StrSql + "PLANTCONFIG.B10 AS D10P7, "
                StrSql = StrSql + "PLANTCONFIG.R1 AS D1P8, "
                StrSql = StrSql + "PLANTCONFIG.R2 AS D2P8, "
                StrSql = StrSql + "PLANTCONFIG.R3 AS D3P8, "
                StrSql = StrSql + "PLANTCONFIG.R4 AS D4P8, "
                StrSql = StrSql + "PLANTCONFIG.R5 AS D5P8, "
                StrSql = StrSql + "PLANTCONFIG.R6 AS D6P8, "
                StrSql = StrSql + "PLANTCONFIG.R7 AS D7P8, "
                StrSql = StrSql + "PLANTCONFIG.R8 AS D8P8, "
                StrSql = StrSql + "PLANTCONFIG.R9 AS D9P8, "
                StrSql = StrSql + "PLANTCONFIG.R10 AS D10P8, "
                StrSql = StrSql + "PLANTCONFIG.K1 AS D1P9, "
                StrSql = StrSql + "PLANTCONFIG.K2 AS D2P9, "
                StrSql = StrSql + "PLANTCONFIG.K3 AS D3P9, "
                StrSql = StrSql + "PLANTCONFIG.K4 AS D4P9, "
                StrSql = StrSql + "PLANTCONFIG.K5 AS D5P9, "
                StrSql = StrSql + "PLANTCONFIG.K6 AS D6P9, "
                StrSql = StrSql + "PLANTCONFIG.K7 AS D7P9, "
                StrSql = StrSql + "PLANTCONFIG.K8 AS D8P9, "
                StrSql = StrSql + "PLANTCONFIG.K9 AS D9P9, "
                StrSql = StrSql + "PLANTCONFIG.K10 AS D10P9, "
                StrSql = StrSql + "PLANTCONFIG.P1 AS D1P10, "
                StrSql = StrSql + "PLANTCONFIG.P2 AS D2P10, "
                StrSql = StrSql + "PLANTCONFIG.P3 AS D3P10, "
                StrSql = StrSql + "PLANTCONFIG.P4 AS D4P10, "
                StrSql = StrSql + "PLANTCONFIG.P5 AS D5P10, "
                StrSql = StrSql + "PLANTCONFIG.P6 AS D6P10, "
                StrSql = StrSql + "PLANTCONFIG.P7 AS D7P10, "
                StrSql = StrSql + "PLANTCONFIG.P8 AS D8P10, "
                StrSql = StrSql + "PLANTCONFIG.P9 AS D9P10, "
                StrSql = StrSql + "PLANTCONFIG.P10 AS D10P10 "
                StrSql = StrSql + "FROM PLANTCONFIG "
                StrSql = StrSql + "WHERE PLANTCONFIG.CASEID = " + CaseId.ToString() + ""
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetPlanConfigDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetEffiencyDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MATIN.CASEID,  "
                StrSql = StrSql + "(MAT1.MATDE1 || ' ' || MAT1.MATDE2) AS MAT1, "
                StrSql = StrSql + "(MAT2.MATDE1 || ' ' || MAT2.MATDE2) AS MAT2, "
                StrSql = StrSql + "(MAT3.MATDE1 || ' ' || MAT3.MATDE2) AS MAT3, "
                StrSql = StrSql + "(MAT4.MATDE1 || ' ' || MAT4.MATDE2) AS MAT4, "
                StrSql = StrSql + "(MAT5.MATDE1 || ' ' || MAT5.MATDE2) AS MAT5, "
                StrSql = StrSql + "(MAT6.MATDE1 || ' ' || MAT6.MATDE2) AS MAT6, "
                StrSql = StrSql + "(MAT7.MATDE1 || ' ' || MAT7.MATDE2) AS MAT7, "
                StrSql = StrSql + "(MAT8.MATDE1 || ' ' || MAT8.MATDE2) AS MAT8, "
                StrSql = StrSql + "(MAT9.MATDE1 || ' ' || MAT9.MATDE2) AS MAT9, "
                StrSql = StrSql + "(MAT10.MATDE1 || ' ' || MAT10.MATDE2) AS MAT10, "
                StrSql = StrSql + "(PROC1.PROCDE1 || ' ' ||  PROC1.PROCDE2) AS PROC1, "
                StrSql = StrSql + "(PROC2.PROCDE1 || ' ' ||  PROC2.PROCDE2) AS PROC2, "
                StrSql = StrSql + "(PROC3.PROCDE1 || ' ' ||  PROC3.PROCDE2) AS PROC3, "
                StrSql = StrSql + "(PROC4.PROCDE1 || ' ' ||  PROC4.PROCDE2) AS PROC4, "
                StrSql = StrSql + "(PROC5.PROCDE1 || ' ' ||  PROC5.PROCDE2) AS PROC5, "
                StrSql = StrSql + "(PROC6.PROCDE1 || ' ' ||  PROC6.PROCDE2) AS PROC6, "
                StrSql = StrSql + "(PROC7.PROCDE1 || ' ' ||  PROC7.PROCDE2) AS PROC7, "
                StrSql = StrSql + "(PROC8.PROCDE1 || ' ' ||  PROC8.PROCDE2) AS PROC8, "
                StrSql = StrSql + "(PROC9.PROCDE1 || ' ' ||  PROC9.PROCDE2) AS PROC9, "
                StrSql = StrSql + "(PROC10.PROCDE1 || ' ' ||  PROC10.PROCDE2) AS PROC10, "
                StrSql = StrSql + "MATEF.T1 AS MATA1, "
                StrSql = StrSql + "MATEF.T2 AS MATA2, "
                StrSql = StrSql + "MATEF.T3 AS MATA3, "
                StrSql = StrSql + "MATEF.T4 AS MATA4, "
                StrSql = StrSql + "MATEF.T5 AS MATA5, "
                StrSql = StrSql + "MATEF.T6 AS MATA6, "
                StrSql = StrSql + "MATEF.T7 AS MATA7, "
                StrSql = StrSql + "MATEF.T8 AS MATA8, "
                StrSql = StrSql + "MATEF.T9 AS MATA9, "
                StrSql = StrSql + "MATEF.T10 AS MATA10, "
                StrSql = StrSql + "MATEF.S1 AS MATB1, "
                StrSql = StrSql + "MATEF.S2 AS MATB2, "
                StrSql = StrSql + "MATEF.S3 AS MATB3, "
                StrSql = StrSql + "MATEF.S4 AS MATB4, "
                StrSql = StrSql + "MATEF.S5 AS MATB5, "
                StrSql = StrSql + "MATEF.S6 AS MATB6, "
                StrSql = StrSql + "MATEF.S7 AS MATB7, "
                StrSql = StrSql + "MATEF.S8 AS MATB8, "
                StrSql = StrSql + "MATEF.S9 AS MATB9, "
                StrSql = StrSql + "MATEF.S10 AS MATB10, "
                StrSql = StrSql + "MATEF.Y1 AS MATC1, "
                StrSql = StrSql + "MATEF.Y2 AS MATC2, "
                StrSql = StrSql + "MATEF.Y3 AS MATC3, "
                StrSql = StrSql + "MATEF.Y4 AS MATC4, "
                StrSql = StrSql + "MATEF.Y5 AS MATC5, "
                StrSql = StrSql + "MATEF.Y6 AS MATC6, "
                StrSql = StrSql + "MATEF.Y7 AS MATC7, "
                StrSql = StrSql + "MATEF.Y8 AS MATC8, "
                StrSql = StrSql + "MATEF.Y9 AS MATC9, "
                StrSql = StrSql + "MATEF.Y10 AS MATC10, "
                StrSql = StrSql + "MATEF.D1 AS MATD1, "
                StrSql = StrSql + "MATEF.D2 AS MATD2, "
                StrSql = StrSql + "MATEF.D3 AS MATD3, "
                StrSql = StrSql + "MATEF.D4 AS MATD4, "
                StrSql = StrSql + "MATEF.D5 AS MATD5, "
                StrSql = StrSql + "MATEF.D6 AS MATD6, "
                StrSql = StrSql + "MATEF.D7 AS MATD7, "
                StrSql = StrSql + "MATEF.D8 AS MATD8, "
                StrSql = StrSql + "MATEF.D9 AS MATD9, "
                StrSql = StrSql + "MATEF.D10 AS MATD10, "
                StrSql = StrSql + "MATEF.E1 AS MATE1, "
                StrSql = StrSql + "MATEF.E2 AS MATE2, "
                StrSql = StrSql + "MATEF.E3 AS MATE3, "
                StrSql = StrSql + "MATEF.E4 AS MATE4, "
                StrSql = StrSql + "MATEF.E5 AS MATE5, "
                StrSql = StrSql + "MATEF.E6 AS MATE6, "
                StrSql = StrSql + "MATEF.E7 AS MATE7, "
                StrSql = StrSql + "MATEF.E8 AS MATE8, "
                StrSql = StrSql + "MATEF.E9 AS MATE9, "
                StrSql = StrSql + "MATEF.E10 AS MATE10, "
                StrSql = StrSql + "MATEF.Z1 AS MATF1, "
                StrSql = StrSql + "MATEF.Z2 AS MATF2, "
                StrSql = StrSql + "MATEF.Z3 AS MATF3, "
                StrSql = StrSql + "MATEF.Z4 AS MATF4, "
                StrSql = StrSql + "MATEF.Z5 AS MATF5, "
                StrSql = StrSql + "MATEF.Z6 AS MATF6, "
                StrSql = StrSql + "MATEF.Z7 AS MATF7, "
                StrSql = StrSql + "MATEF.Z8 AS MATF8, "
                StrSql = StrSql + "MATEF.Z9 AS MATF9, "
                StrSql = StrSql + "MATEF.Z10 AS MATF10, "
                StrSql = StrSql + "MATEF.B1 AS MATG1, "
                StrSql = StrSql + "MATEF.B2 AS MATG2, "
                StrSql = StrSql + "MATEF.B3 AS MATG3, "
                StrSql = StrSql + "MATEF.B4 AS MATG4, "
                StrSql = StrSql + "MATEF.B5 AS MATG5, "
                StrSql = StrSql + "MATEF.B6 AS MATG6, "
                StrSql = StrSql + "MATEF.B7 AS MATG7, "
                StrSql = StrSql + "MATEF.B8 AS MATG8, "
                StrSql = StrSql + "MATEF.B9 AS MATG9, "
                StrSql = StrSql + "MATEF.B10 AS MATG10, "
                StrSql = StrSql + "MATEF.R1 AS MATH1, "
                StrSql = StrSql + "MATEF.R2 AS MATH2, "
                StrSql = StrSql + "MATEF.R3 AS MATH3, "
                StrSql = StrSql + "MATEF.R4 AS MATH4, "
                StrSql = StrSql + "MATEF.R5 AS MATH5, "
                StrSql = StrSql + "MATEF.R6 AS MATH6, "
                StrSql = StrSql + "MATEF.R7 AS MATH7, "
                StrSql = StrSql + "MATEF.R8 AS MATH8, "
                StrSql = StrSql + "MATEF.R9 AS MATH9, "
                StrSql = StrSql + "MATEF.R10 AS MATH10, "
                StrSql = StrSql + "MATEF.K1 AS MATI1, "
                StrSql = StrSql + "MATEF.K2 AS MATI2, "
                StrSql = StrSql + "MATEF.K3 AS MATI3, "
                StrSql = StrSql + "MATEF.K4 AS MATI4, "
                StrSql = StrSql + "MATEF.K5 AS MATI5, "
                StrSql = StrSql + "MATEF.K6 AS MATI6, "
                StrSql = StrSql + "MATEF.K7 AS MATI7, "
                StrSql = StrSql + "MATEF.K8 AS MATI8, "
                StrSql = StrSql + "MATEF.K9 AS MATI9, "
                StrSql = StrSql + "MATEF.K10 AS MATI10, "
                StrSql = StrSql + "MATEF.P1 AS MATJ1, "
                StrSql = StrSql + "MATEF.P2 AS MATJ2, "
                StrSql = StrSql + "MATEF.P3 AS MATJ3, "
                StrSql = StrSql + "MATEF.P4 AS MATJ4, "
                StrSql = StrSql + "MATEF.P5 AS MATJ5, "
                StrSql = StrSql + "MATEF.P6 AS MATJ6, "
                StrSql = StrSql + "MATEF.P7 AS MATJ7, "
                StrSql = StrSql + "MATEF.P8 AS MATJ8, "
                StrSql = StrSql + "MATEF.P9 AS MATJ9, "
                StrSql = StrSql + "MATEF.P10 AS MATJ10 "
                StrSql = StrSql + "FROM MATERIALINPUT MATIN "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT1 "
                StrSql = StrSql + "ON MAT1.MATID=MATIN.M1 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT2 "
                StrSql = StrSql + "ON MAT2.MATID=MATIN.M2 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT3 "
                StrSql = StrSql + "ON MAT3.MATID=MATIN.M3 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT4 "
                StrSql = StrSql + "ON MAT4.MATID=MATIN.M4 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT5 "
                StrSql = StrSql + "ON MAT5.MATID=MATIN.M5 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT6 "
                StrSql = StrSql + "ON MAT6.MATID=MATIN.M6 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT7 "
                StrSql = StrSql + "ON MAT7.MATID=MATIN.M7 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT8 "
                StrSql = StrSql + "ON MAT8.MATID=MATIN.M8 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT9 "
                StrSql = StrSql + "ON MAT9.MATID=MATIN.M9 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT10 "
                StrSql = StrSql + "ON MAT10.MATID=MATIN.M10 "
                StrSql = StrSql + "INNER JOIN PLANTCONFIG PLC "
                StrSql = StrSql + "ON PLC.CASEID=MATIN.CASEID "
                StrSql = StrSql + "INNER JOIN PROCESS PROC1 "
                StrSql = StrSql + "ON PROC1.PROCID=PLC.M1 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC2 "
                StrSql = StrSql + "ON PROC2.PROCID=PLC.M2 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC3 "
                StrSql = StrSql + "ON PROC3.PROCID=PLC.M3 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC4 "
                StrSql = StrSql + "ON PROC4.PROCID=PLC.M4 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC5 "
                StrSql = StrSql + "ON PROC5.PROCID=PLC.M5 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC6 "
                StrSql = StrSql + "ON PROC6.PROCID=PLC.M6 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC7 "
                StrSql = StrSql + "ON PROC7.PROCID=PLC.M7 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC8 "
                StrSql = StrSql + "ON PROC8.PROCID=PLC.M8 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC9 "
                StrSql = StrSql + "ON PROC9.PROCID=PLC.M9 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC10 "
                StrSql = StrSql + "ON PROC10.PROCID=PLC.M10 "
                StrSql = StrSql + "INNER JOIN MATERIALEFF MATEF "
                StrSql = StrSql + "ON MATEF.CASEID=MATIN.CASEID "
                StrSql = StrSql + "WHERE  MATIN.CASEID=" + CaseId.ToString() + ""

                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetEffiencyDetails:" + ex.Message.ToString())
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
                StrSql = StrSql + "EQT.M12 , "
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
                StrSql = StrSql + "(EQ1.cost*PREF.CURR) AS AS1, "
                StrSql = StrSql + "(EQ2.cost*PREF.CURR) AS AS2, "
                StrSql = StrSql + "(EQ3.cost*PREF.CURR) AS AS3, "
                StrSql = StrSql + "(EQ4.cost*PREF.CURR) AS AS4, "
                StrSql = StrSql + "(EQ5.cost*PREF.CURR) AS AS5, "
                StrSql = StrSql + "(EQ6.cost*PREF.CURR) AS AS6, "
                StrSql = StrSql + "(EQ7.cost*PREF.CURR) AS AS7, "
                StrSql = StrSql + "(EQ8.cost*PREF.CURR) AS AS8, "
                StrSql = StrSql + "(EQ9.cost*PREF.CURR) AS AS9, "
                StrSql = StrSql + "(EQ10.cost*PREF.CURR) AS AS10, "
                StrSql = StrSql + "(EQ11.cost*PREF.CURR) AS AS11, "
                StrSql = StrSql + "(EQ12.cost*PREF.CURR) AS AS12, "
                StrSql = StrSql + "(EQ13.cost*PREF.CURR) AS AS13, "
                StrSql = StrSql + "(EQ14.cost*PREF.CURR) AS AS14, "
                StrSql = StrSql + "(EQ15.cost*PREF.CURR) AS AS15, "
                StrSql = StrSql + "(EQ16.cost*PREF.CURR) AS AS16, "
                StrSql = StrSql + "(EQ17.cost*PREF.CURR) AS AS17, "
                StrSql = StrSql + "(EQ18.cost*PREF.CURR) AS AS18, "
                StrSql = StrSql + "(EQ19.cost*PREF.CURR) AS AS19, "
                StrSql = StrSql + "(EQ20.cost*PREF.CURR) AS AS20, "
                StrSql = StrSql + "(EQ21.cost*PREF.CURR) AS AS21, "
                StrSql = StrSql + "(EQ22.cost*PREF.CURR) AS AS22, "
                StrSql = StrSql + "(EQ23.cost*PREF.CURR) AS AS23, "
                StrSql = StrSql + "(EQ24.cost*PREF.CURR) AS AS24, "
                StrSql = StrSql + "(EQ25.cost*PREF.CURR) AS AS25, "
                StrSql = StrSql + "(EQ26.cost*PREF.CURR) AS AS26, "
                StrSql = StrSql + "(EQ27.cost*PREF.CURR) AS AS27, "
                StrSql = StrSql + "(EQ28.cost*PREF.CURR) AS AS28, "
                StrSql = StrSql + "(EQ29.cost*PREF.CURR) AS AS29, "
                StrSql = StrSql + "(EQ30.cost*PREF.CURR) AS AS30, "
                StrSql = StrSql + "(EquipmentCOST.M1*PREF.CURR) AS AP1, "
                StrSql = StrSql + "(EquipmentCOST.M2*PREF.CURR) AS AP2, "
                StrSql = StrSql + "(EquipmentCOST.M3*PREF.CURR) AS AP3, "
                StrSql = StrSql + "(EquipmentCOST.M4*PREF.CURR) AS AP4, "
                StrSql = StrSql + "(EquipmentCOST.M5*PREF.CURR) AS AP5, "
                StrSql = StrSql + "(EquipmentCOST.M6*PREF.CURR) AS AP6, "
                StrSql = StrSql + "(EquipmentCOST.M7*PREF.CURR) AS AP7, "
                StrSql = StrSql + "(EquipmentCOST.M8*PREF.CURR) AS AP8, "
                StrSql = StrSql + "(EquipmentCOST.M9*PREF.CURR) AS AP9, "
                StrSql = StrSql + "(EquipmentCOST.M10*PREF.CURR) AS AP10, "
                StrSql = StrSql + "(EquipmentCOST.M11*PREF.CURR) AS AP11, "
                StrSql = StrSql + "(EquipmentCOST.M12*PREF.CURR) AS AP12, "
                StrSql = StrSql + "(EquipmentCOST.M13*PREF.CURR) AS AP13, "
                StrSql = StrSql + "(EquipmentCOST.M14*PREF.CURR) AS AP14, "
                StrSql = StrSql + "(EquipmentCOST.M15*PREF.CURR) AS AP15, "
                StrSql = StrSql + "(EquipmentCOST.M16*PREF.CURR) AS AP16, "
                StrSql = StrSql + "(EquipmentCOST.M17*PREF.CURR) AS AP17, "
                StrSql = StrSql + "(EquipmentCOST.M18*PREF.CURR) AS AP18, "
                StrSql = StrSql + "(EquipmentCOST.M19*PREF.CURR) AS AP19, "
                StrSql = StrSql + "(EquipmentCOST.M20*PREF.CURR) AS AP20, "
                StrSql = StrSql + "(EquipmentCOST.M21*PREF.CURR) AS AP21, "
                StrSql = StrSql + "(EquipmentCOST.M22*PREF.CURR) AS AP22, "
                StrSql = StrSql + "(EquipmentCOST.M23*PREF.CURR) AS AP23, "
                StrSql = StrSql + "(EquipmentCOST.M24*PREF.CURR) AS AP24, "
                StrSql = StrSql + "(EquipmentCOST.M25*PREF.CURR) AS AP25, "
                StrSql = StrSql + "(EquipmentCOST.M26*PREF.CURR) AS AP26, "
                StrSql = StrSql + "(EquipmentCOST.M27*PREF.CURR) AS AP27, "
                StrSql = StrSql + "(EquipmentCOST.M28*PREF.CURR) AS AP28, "
                StrSql = StrSql + "(EquipmentCOST.M29*PREF.CURR) AS AP29, "
                StrSql = StrSql + "(EquipmentCOST.M30*PREF.CURR) AS AP30, "
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

                StrSql = StrSql + "FROM EQUIPMENTTYPE EQT "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ1 "
                StrSql = StrSql + "ON EQ1.EQUIPID=EQT.M1 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ2 "
                StrSql = StrSql + "ON EQ2.EQUIPID=EQT.M2 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ3 "
                StrSql = StrSql + "ON EQ3.EQUIPID=EQT.M3 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ4 "
                StrSql = StrSql + "ON EQ4.EQUIPID=EQT.M4 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ5 "
                StrSql = StrSql + "ON EQ5.EQUIPID=EQT.M5 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ6 "
                StrSql = StrSql + "ON EQ6.EQUIPID=EQT.M6 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ7 "
                StrSql = StrSql + "ON EQ7.EQUIPID=EQT.M7 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ8 "
                StrSql = StrSql + "ON EQ8.EQUIPID=EQT.M8 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ9 "
                StrSql = StrSql + "ON EQ9.EQUIPID=EQT.M9 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ10 "
                StrSql = StrSql + "ON EQ10.EQUIPID=EQT.M10 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ11 "
                StrSql = StrSql + "ON EQ11.EQUIPID=EQT.M11 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ12 "
                StrSql = StrSql + "ON EQ12.EQUIPID=EQT.M12 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ13 "
                StrSql = StrSql + "ON EQ13.EQUIPID=EQT.M13 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ14 "
                StrSql = StrSql + "ON EQ14.EQUIPID=EQT.M14 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ15 "
                StrSql = StrSql + "ON EQ15.EQUIPID=EQT.M15 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ16 "
                StrSql = StrSql + "ON EQ16.EQUIPID=EQT.M16 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ17 "
                StrSql = StrSql + "ON EQ17.EQUIPID=EQT.M17 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ18 "
                StrSql = StrSql + "ON EQ18.EQUIPID=EQT.M18 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ19 "
                StrSql = StrSql + "ON EQ19.EQUIPID=EQT.M19 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ20 "
                StrSql = StrSql + "ON EQ20.EQUIPID=EQT.M20 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ21 "
                StrSql = StrSql + "ON EQ21.EQUIPID=EQT.M21 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ22 "
                StrSql = StrSql + "ON EQ22.EQUIPID=EQT.M22 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ23 "
                StrSql = StrSql + "ON EQ23.EQUIPID=EQT.M23 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ24 "
                StrSql = StrSql + "ON EQ24.EQUIPID=EQT.M24 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ25 "
                StrSql = StrSql + "ON EQ25.EQUIPID=EQT.M25 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ26 "
                StrSql = StrSql + "ON EQ26.EQUIPID=EQT.M26 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ27 "
                StrSql = StrSql + "ON EQ27.EQUIPID=EQT.M27 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ28 "
                StrSql = StrSql + "ON EQ28.EQUIPID=EQT.M28 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ29 "
                StrSql = StrSql + "ON EQ29.EQUIPID=EQT.M29 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ30 "
                StrSql = StrSql + "ON EQ30.EQUIPID=EQT.M30 "
                StrSql = StrSql + "INNER JOIN EquipmentCOST "
                StrSql = StrSql + "ON EquipmentCOST.CASEID=EQT.CASEID "
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

                StrSql = StrSql + "INNER JOIN EQUIPMENTNUMBER EQNUM "
                StrSql = StrSql + "ON EQNUM.CASEID=EQT.CASEID "

                StrSql = StrSql + "WHERE EQT.CASEID=" + CaseId.ToString() + ""
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetEquipmentDetails:" + ex.Message.ToString())
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
                StrSql = StrSql + "(EQ1.cost*PREF.CURR) AS AS1, "
                StrSql = StrSql + "(EQ2.cost*PREF.CURR) AS AS2, "
                StrSql = StrSql + "(EQ3.cost*PREF.CURR) AS AS3, "
                StrSql = StrSql + "(EQ4.cost*PREF.CURR) AS AS4, "
                StrSql = StrSql + "(EQ5.cost*PREF.CURR) AS AS5, "
                StrSql = StrSql + "(EQ6.cost*PREF.CURR) AS AS6, "
                StrSql = StrSql + "(EQ7.cost*PREF.CURR) AS AS7, "
                StrSql = StrSql + "(EQ8.cost*PREF.CURR) AS AS8, "
                StrSql = StrSql + "(EQ9.cost*PREF.CURR) AS AS9, "
                StrSql = StrSql + "(EQ10.cost*PREF.CURR) AS AS10, "
                StrSql = StrSql + "(EQ11.cost*PREF.CURR) AS AS11, "
                StrSql = StrSql + "(EQ12.cost*PREF.CURR) AS AS12, "
                StrSql = StrSql + "(EQ13.cost*PREF.CURR) AS AS13, "
                StrSql = StrSql + "(EQ14.cost*PREF.CURR) AS AS14, "
                StrSql = StrSql + "(EQ15.cost*PREF.CURR) AS AS15, "
                StrSql = StrSql + "(EQ16.cost*PREF.CURR) AS AS16, "
                StrSql = StrSql + "(EQ17.cost*PREF.CURR) AS AS17, "
                StrSql = StrSql + "(EQ18.cost*PREF.CURR) AS AS18, "
                StrSql = StrSql + "(EQ19.cost*PREF.CURR) AS AS19, "
                StrSql = StrSql + "(EQ20.cost*PREF.CURR) AS AS20, "
                StrSql = StrSql + "(EQ21.cost*PREF.CURR) AS AS21, "
                StrSql = StrSql + "(EQ22.cost*PREF.CURR) AS AS22, "
                StrSql = StrSql + "(EQ23.cost*PREF.CURR) AS AS23, "
                StrSql = StrSql + "(EQ24.cost*PREF.CURR) AS AS24, "
                StrSql = StrSql + "(EQ25.cost*PREF.CURR) AS AS25, "
                StrSql = StrSql + "(EQ26.cost*PREF.CURR) AS AS26, "
                StrSql = StrSql + "(EQ27.cost*PREF.CURR) AS AS27, "
                StrSql = StrSql + "(EQ28.cost*PREF.CURR) AS AS28, "
                StrSql = StrSql + "(EQ29.cost*PREF.CURR) AS AS29, "
                StrSql = StrSql + "(EQ30.cost*PREF.CURR) AS AS30, "
                StrSql = StrSql + "(Equipment2COST.M1*PREF.CURR) AS AP1, "
                StrSql = StrSql + "(Equipment2COST.M2*PREF.CURR) AS AP2, "
                StrSql = StrSql + "(Equipment2COST.M3*PREF.CURR) AS AP3, "
                StrSql = StrSql + "(Equipment2COST.M4*PREF.CURR) AS AP4, "
                StrSql = StrSql + "(Equipment2COST.M5*PREF.CURR) AS AP5, "
                StrSql = StrSql + "(Equipment2COST.M6*PREF.CURR) AS AP6, "
                StrSql = StrSql + "(Equipment2COST.M7*PREF.CURR) AS AP7, "
                StrSql = StrSql + "(Equipment2COST.M8*PREF.CURR) AS AP8, "
                StrSql = StrSql + "(Equipment2COST.M9*PREF.CURR) AS AP9, "
                StrSql = StrSql + "(Equipment2COST.M10*PREF.CURR) AS AP10, "
                StrSql = StrSql + "(Equipment2COST.M11*PREF.CURR) AS AP11, "
                StrSql = StrSql + "(Equipment2COST.M12*PREF.CURR) AS AP12, "
                StrSql = StrSql + "(Equipment2COST.M13*PREF.CURR) AS AP13, "
                StrSql = StrSql + "(Equipment2COST.M14*PREF.CURR) AS AP14, "
                StrSql = StrSql + "(Equipment2COST.M15*PREF.CURR) AS AP15, "
                StrSql = StrSql + "(Equipment2COST.M16*PREF.CURR) AS AP16, "
                StrSql = StrSql + "(Equipment2COST.M17*PREF.CURR) AS AP17, "
                StrSql = StrSql + "(Equipment2COST.M18*PREF.CURR) AS AP18, "
                StrSql = StrSql + "(Equipment2COST.M19*PREF.CURR) AS AP19, "
                StrSql = StrSql + "(Equipment2COST.M20*PREF.CURR) AS AP20, "
                StrSql = StrSql + "(Equipment2COST.M21*PREF.CURR) AS AP21, "
                StrSql = StrSql + "(Equipment2COST.M22*PREF.CURR) AS AP22, "
                StrSql = StrSql + "(Equipment2COST.M23*PREF.CURR) AS AP23, "
                StrSql = StrSql + "(Equipment2COST.M24*PREF.CURR) AS AP24, "
                StrSql = StrSql + "(Equipment2COST.M25*PREF.CURR) AS AP25, "
                StrSql = StrSql + "(Equipment2COST.M26*PREF.CURR) AS AP26, "
                StrSql = StrSql + "(Equipment2COST.M27*PREF.CURR) AS AP27, "
                StrSql = StrSql + "(Equipment2COST.M28*PREF.CURR) AS AP28, "
                StrSql = StrSql + "(Equipment2COST.M29*PREF.CURR) AS AP29, "
                StrSql = StrSql + "(Equipment2COST.M30*PREF.CURR) AS AP30, "
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
                StrSql = StrSql + "EQCOSTTYPE.M1 AS COSTTYPE1, "
                StrSql = StrSql + "EQCOSTTYPE.M2 AS COSTTYPE2, "
                StrSql = StrSql + "EQCOSTTYPE.M3 AS COSTTYPE3, "
                StrSql = StrSql + "EQCOSTTYPE.M4 AS COSTTYPE4, "
                StrSql = StrSql + "EQCOSTTYPE.M5 AS COSTTYPE5, "
                StrSql = StrSql + "EQCOSTTYPE.M6 AS COSTTYPE6, "
                StrSql = StrSql + "EQCOSTTYPE.M7 AS COSTTYPE7, "
                StrSql = StrSql + "EQCOSTTYPE.M8 AS COSTTYPE8, "
                StrSql = StrSql + "EQCOSTTYPE.M9 AS COSTTYPE9, "
                StrSql = StrSql + "EQCOSTTYPE.M10 AS COSTTYPE10, "
                StrSql = StrSql + "EQCOSTTYPE.M11 AS COSTTYPE11, "
                StrSql = StrSql + "EQCOSTTYPE.M12 AS COSTTYPE12, "
                StrSql = StrSql + "EQCOSTTYPE.M13 AS COSTTYPE13, "
                StrSql = StrSql + "EQCOSTTYPE.M14 AS COSTTYPE14, "
                StrSql = StrSql + "EQCOSTTYPE.M15 AS COSTTYPE15, "
                StrSql = StrSql + "EQCOSTTYPE.M16 AS COSTTYPE16, "
                StrSql = StrSql + "EQCOSTTYPE.M17 AS COSTTYPE17, "
                StrSql = StrSql + "EQCOSTTYPE.M18 AS COSTTYPE18, "
                StrSql = StrSql + "EQCOSTTYPE.M19 AS COSTTYPE19, "
                StrSql = StrSql + "EQCOSTTYPE.M20 AS COSTTYPE20, "
                StrSql = StrSql + "EQCOSTTYPE.M21 AS COSTTYPE21, "
                StrSql = StrSql + "EQCOSTTYPE.M22 AS COSTTYPE22, "
                StrSql = StrSql + "EQCOSTTYPE.M23 AS COSTTYPE23, "
                StrSql = StrSql + "EQCOSTTYPE.M24 AS COSTTYPE24, "
                StrSql = StrSql + "EQCOSTTYPE.M25 AS COSTTYPE25, "
                StrSql = StrSql + "EQCOSTTYPE.M26 AS COSTTYPE26, "
                StrSql = StrSql + "EQCOSTTYPE.M27 AS COSTTYPE27, "
                StrSql = StrSql + "EQCOSTTYPE.M28 AS COSTTYPE28, "
                StrSql = StrSql + "EQCOSTTYPE.M29 AS COSTTYPE29, "
                StrSql = StrSql + "EQCOSTTYPE.M30 AS COSTTYPE30, "
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
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ1 "
                StrSql = StrSql + "ON EQ1.EQUIPID=EQT.M1 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ2 "
                StrSql = StrSql + "ON EQ2.EQUIPID=EQT.M2 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ3 "
                StrSql = StrSql + "ON EQ3.EQUIPID=EQT.M3 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ4 "
                StrSql = StrSql + "ON EQ4.EQUIPID=EQT.M4 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ5 "
                StrSql = StrSql + "ON EQ5.EQUIPID=EQT.M5 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ6 "
                StrSql = StrSql + "ON EQ6.EQUIPID=EQT.M6 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ7 "
                StrSql = StrSql + "ON EQ7.EQUIPID=EQT.M7 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ8 "
                StrSql = StrSql + "ON EQ8.EQUIPID=EQT.M8 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ9 "
                StrSql = StrSql + "ON EQ9.EQUIPID=EQT.M9 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ10 "
                StrSql = StrSql + "ON EQ10.EQUIPID=EQT.M10 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ11 "
                StrSql = StrSql + "ON EQ11.EQUIPID=EQT.M11 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ12 "
                StrSql = StrSql + "ON EQ12.EQUIPID=EQT.M12 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ13 "
                StrSql = StrSql + "ON EQ13.EQUIPID=EQT.M13 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ14 "
                StrSql = StrSql + "ON EQ14.EQUIPID=EQT.M14 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ15 "
                StrSql = StrSql + "ON EQ15.EQUIPID=EQT.M15 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ16 "
                StrSql = StrSql + "ON EQ16.EQUIPID=EQT.M16 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ17 "
                StrSql = StrSql + "ON EQ17.EQUIPID=EQT.M17 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ18 "
                StrSql = StrSql + "ON EQ18.EQUIPID=EQT.M18 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ19 "
                StrSql = StrSql + "ON EQ19.EQUIPID=EQT.M19 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ20 "
                StrSql = StrSql + "ON EQ20.EQUIPID=EQT.M20 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ21 "
                StrSql = StrSql + "ON EQ21.EQUIPID=EQT.M21 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ22 "
                StrSql = StrSql + "ON EQ22.EQUIPID=EQT.M22 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ23 "
                StrSql = StrSql + "ON EQ23.EQUIPID=EQT.M23 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ24 "
                StrSql = StrSql + "ON EQ24.EQUIPID=EQT.M24 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ25 "
                StrSql = StrSql + "ON EQ25.EQUIPID=EQT.M25 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ26 "
                StrSql = StrSql + "ON EQ26.EQUIPID=EQT.M26 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ27 "
                StrSql = StrSql + "ON EQ27.EQUIPID=EQT.M27 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ28 "
                StrSql = StrSql + "ON EQ28.EQUIPID=EQT.M28 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ29 "
                StrSql = StrSql + "ON EQ29.EQUIPID=EQT.M29 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2 EQ30 "
                StrSql = StrSql + "ON EQ30.EQUIPID=EQT.M30 "
                StrSql = StrSql + "INNER JOIN Equipment2COST "
                StrSql = StrSql + "ON Equipment2COST.CASEID=EQT.CASEID "
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
                StrSql = StrSql + "INNER JOIN EQUIPMENT2COSTTYPE EQCOSTTYPE "
                StrSql = StrSql + "ON EQCOSTTYPE.CASEID=EQT.CASEID "

                StrSql = StrSql + "INNER JOIN EQUIPMENT2NUMBER EQNUM "
                StrSql = StrSql + "ON EQNUM.CASEID=EQT.CASEID "

                StrSql = StrSql + "WHERE EQT.CASEID=" + CaseId.ToString() + ""

                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetSupportEquipmentInDetails:" + ex.Message.ToString())
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
                StrSql = StrSql + "EQT.M1 AS ASSETID1, "
                StrSql = StrSql + "EQT.M2  AS ASSETID2, "
                StrSql = StrSql + "EQT.M3  AS ASSETID3, "
                StrSql = StrSql + "EQT.M4  AS ASSETID4, "
                StrSql = StrSql + "EQT.M5  AS ASSETID5, "
                StrSql = StrSql + "EQT.M6  AS ASSETID6, "
                StrSql = StrSql + "EQT.M7  AS ASSETID7, "
                StrSql = StrSql + "EQT.M8  AS ASSETID8, "
                StrSql = StrSql + "EQT.M9 AS ASSETID9, "
                StrSql = StrSql + "EQT.M10 AS ASSETID10, "
                StrSql = StrSql + "EQT.M11 AS ASSETID11, "
                StrSql = StrSql + "EQT.M12 AS ASSETID12, "
                StrSql = StrSql + "EQT.M13 AS ASSETID13, "
                StrSql = StrSql + "EQT.M14 AS ASSETID14, "
                StrSql = StrSql + "EQT.M15 AS ASSETID15, "
                StrSql = StrSql + "EQT.M16 AS ASSETID16, "
                StrSql = StrSql + "EQT.M17 AS ASSETID17, "
                StrSql = StrSql + "EQT.M18 AS ASSETID18, "
                StrSql = StrSql + "EQT.M19 AS ASSETID19, "
                StrSql = StrSql + "EQT.M20 AS ASSETID20, "
                StrSql = StrSql + "EQT.M21 AS ASSETID21, "
                StrSql = StrSql + "EQT.M22 AS ASSETID22, "
                StrSql = StrSql + "EQT.M23 AS ASSETID23, "
                StrSql = StrSql + "EQT.M24 AS ASSETID24, "
                StrSql = StrSql + "EQT.M25 AS ASSETID25, "
                StrSql = StrSql + "EQT.M26 AS ASSETID26, "
                StrSql = StrSql + "EQT.M27 AS ASSETID27, "
                StrSql = StrSql + "EQT.M28 AS ASSETID28, "
                StrSql = StrSql + "EQT.M29 AS ASSETID29, "
                StrSql = StrSql + "EQT.M30 AS ASSETID30, "
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
                StrSql = StrSql + "PREF.CONVTHICK2 , "
                StrSql = StrSql + "PREF.CONVWT, "

                'Added for Bug#344
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
                'end of Bug3344

                StrSql = StrSql + "FROM EQUIPMENTTYPE EQT "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ1 "
                StrSql = StrSql + "ON EQ1.EQUIPID=EQT.M1 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ2 "
                StrSql = StrSql + "ON EQ2.EQUIPID=EQT.M2 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ3 "
                StrSql = StrSql + "ON EQ3.EQUIPID=EQT.M3 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ4 "
                StrSql = StrSql + "ON EQ4.EQUIPID=EQT.M4 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ5 "
                StrSql = StrSql + "ON EQ5.EQUIPID=EQT.M5 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ6 "
                StrSql = StrSql + "ON EQ6.EQUIPID=EQT.M6 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ7 "
                StrSql = StrSql + "ON EQ7.EQUIPID=EQT.M7 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ8 "
                StrSql = StrSql + "ON EQ8.EQUIPID=EQT.M8 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ9 "
                StrSql = StrSql + "ON EQ9.EQUIPID=EQT.M9 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ10 "
                StrSql = StrSql + "ON EQ10.EQUIPID=EQT.M10 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ11 "
                StrSql = StrSql + "ON EQ11.EQUIPID=EQT.M11 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ12 "
                StrSql = StrSql + "ON EQ12.EQUIPID=EQT.M12 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ13 "
                StrSql = StrSql + "ON EQ13.EQUIPID=EQT.M13 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ14 "
                StrSql = StrSql + "ON EQ14.EQUIPID=EQT.M14 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ15 "
                StrSql = StrSql + "ON EQ15.EQUIPID=EQT.M15 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ16 "
                StrSql = StrSql + "ON EQ16.EQUIPID=EQT.M16 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ17 "
                StrSql = StrSql + "ON EQ17.EQUIPID=EQT.M17 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ18 "
                StrSql = StrSql + "ON EQ18.EQUIPID=EQT.M18 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ19 "
                StrSql = StrSql + "ON EQ19.EQUIPID=EQT.M19 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ20 "
                StrSql = StrSql + "ON EQ20.EQUIPID=EQT.M20 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ21 "
                StrSql = StrSql + "ON EQ21.EQUIPID=EQT.M21 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ22 "
                StrSql = StrSql + "ON EQ22.EQUIPID=EQT.M22 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ23 "
                StrSql = StrSql + "ON EQ23.EQUIPID=EQT.M23 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ24 "
                StrSql = StrSql + "ON EQ24.EQUIPID=EQT.M24 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ25 "
                StrSql = StrSql + "ON EQ25.EQUIPID=EQT.M25 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ26 "
                StrSql = StrSql + "ON EQ26.EQUIPID=EQT.M26 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ27 "
                StrSql = StrSql + "ON EQ27.EQUIPID=EQT.M27 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ28 "
                StrSql = StrSql + "ON EQ28.EQUIPID=EQT.M28 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ29 "
                StrSql = StrSql + "ON EQ29.EQUIPID=EQT.M29 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQ30 "
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
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetOperationInDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPersonnelInDetails(ByVal CaseId As Integer, ByVal EFFCOUNTRY As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PERSPOS.CASEID,  "
                StrSql = StrSql + "TO_CHAR(NVL(PERSPOS.EFFDATE,TO_DATE('Jan 01,1900','MON DD,YYYY')),'MON DD,YYYY')AS EFFDATE , "
                StrSql = StrSql + "PERSPOS.M1 AS PERSPOS1, "
                StrSql = StrSql + "PERSPOS.M2 AS PERSPOS2, "
                StrSql = StrSql + "PERSPOS.M3 AS PERSPOS3, "
                StrSql = StrSql + "PERSPOS.M4 AS PERSPOS4, "
                StrSql = StrSql + "PERSPOS.M5 AS PERSPOS5, "
                StrSql = StrSql + "PERSPOS.M6 AS PERSPOS6, "
                StrSql = StrSql + "PERSPOS.M7 AS PERSPOS7, "
                StrSql = StrSql + "PERSPOS.M8 AS PERSPOS8, "
                StrSql = StrSql + "PERSPOS.M9 AS PERSPOS9, "
                StrSql = StrSql + "PERSPOS.M10 AS PERSPOS10, "
                StrSql = StrSql + "PERSPOS.M11 AS PERSPOS11, "
                StrSql = StrSql + "PERSPOS.M12 AS PERSPOS12, "
                StrSql = StrSql + "PERSPOS.M13 AS PERSPOS13, "
                StrSql = StrSql + "PERSPOS.M14 AS PERSPOS14, "
                StrSql = StrSql + "PERSPOS.M15 AS PERSPOS15, "
                StrSql = StrSql + "PERSPOS.M16 AS PERSPOS16, "
                StrSql = StrSql + "PERSPOS.M17 AS PERSPOS17, "
                StrSql = StrSql + "PERSPOS.M18 AS PERSPOS18, "
                StrSql = StrSql + "PERSPOS.M19 AS PERSPOS19, "
                StrSql = StrSql + "PERSPOS.M20 AS PERSPOS20, "
                StrSql = StrSql + "PERSPOS.M21 AS PERSPOS21, "
                StrSql = StrSql + "PERSPOS.M22 AS PERSPOS22, "
                StrSql = StrSql + "PERSPOS.M23 AS PERSPOS23, "
                StrSql = StrSql + "PERSPOS.M24 AS PERSPOS24, "
                StrSql = StrSql + "PERSPOS.M25 AS PERSPOS25, "
                StrSql = StrSql + "PERSPOS.M26 AS PERSPOS26, "
                StrSql = StrSql + "PERSPOS.M27 AS PERSPOS27, "
                StrSql = StrSql + "PERSPOS.M28 AS PERSPOS28, "
                StrSql = StrSql + "PERSPOS.M29 AS PERSPOS29, "
                StrSql = StrSql + "PERSPOS.M30 AS PERSPOS30, "
                StrSql = StrSql + "PNUM.M1 AS PERNUM1, "
                StrSql = StrSql + "PNUM.M2 AS PERNUM2, "
                StrSql = StrSql + "PNUM.M3 AS PERNUM3, "
                StrSql = StrSql + "PNUM.M4 AS PERNUM4, "
                StrSql = StrSql + "PNUM.M5 AS PERNUM5, "
                StrSql = StrSql + "PNUM.M6 AS PERNUM6, "
                StrSql = StrSql + "PNUM.M7 AS PERNUM7, "
                StrSql = StrSql + "PNUM.M8 AS PERNUM8, "
                StrSql = StrSql + "PNUM.M9 AS PERNUM9, "
                StrSql = StrSql + "PNUM.M10 AS PERNUM10, "
                StrSql = StrSql + "PNUM.M11 AS PERNUM11, "
                StrSql = StrSql + "PNUM.M12 AS PERNUM12, "
                StrSql = StrSql + "PNUM.M13 AS PERNUM13, "
                StrSql = StrSql + "PNUM.M14 AS PERNUM14, "
                StrSql = StrSql + "PNUM.M15 AS PERNUM15, "
                StrSql = StrSql + "PNUM.M16 AS PERNUM16, "
                StrSql = StrSql + "PNUM.M17 AS PERNUM17, "
                StrSql = StrSql + "PNUM.M18 AS PERNUM18, "
                StrSql = StrSql + "PNUM.M19 AS PERNUM19, "
                StrSql = StrSql + "PNUM.M20 AS PERNUM20, "
                StrSql = StrSql + "PNUM.M21 AS PERNUM21, "
                StrSql = StrSql + "PNUM.M22 AS PERNUM22, "
                StrSql = StrSql + "PNUM.M23 AS PERNUM23, "
                StrSql = StrSql + "PNUM.M24 AS PERNUM24, "
                StrSql = StrSql + "PNUM.M25 AS PERNUM25, "
                StrSql = StrSql + "PNUM.M26 AS PERNUM26, "
                StrSql = StrSql + "PNUM.M27 AS PERNUM27, "
                StrSql = StrSql + "PNUM.M28 AS PERNUM28, "
                StrSql = StrSql + "PNUM.M29 AS PERNUM29, "
                StrSql = StrSql + "PNUM.M30 AS PERNUM30, "
                StrSql = StrSql + "(PERSPOS1.SALARY*PREF.CURR) AS SALS1, "
                StrSql = StrSql + "(PERSPOS2.SALARY*PREF.CURR) AS SALS2, "
                StrSql = StrSql + "(PERSPOS3.SALARY*PREF.CURR) AS SALS3, "
                StrSql = StrSql + "(PERSPOS4.SALARY*PREF.CURR) AS SALS4, "
                StrSql = StrSql + "(PERSPOS5.SALARY*PREF.CURR) AS SALS5, "
                StrSql = StrSql + "(PERSPOS6.SALARY*PREF.CURR) AS SALS6, "
                StrSql = StrSql + "(PERSPOS7.SALARY*PREF.CURR) AS SALS7, "
                StrSql = StrSql + "(PERSPOS8.SALARY*PREF.CURR) AS SALS8, "
                StrSql = StrSql + "(PERSPOS9.SALARY*PREF.CURR) AS SALS9, "
                StrSql = StrSql + "(PERSPOS10.SALARY*PREF.CURR) AS SALS10, "
                StrSql = StrSql + "(PERSPOS11.SALARY*PREF.CURR) AS SALS11, "
                StrSql = StrSql + "(PERSPOS12.SALARY*PREF.CURR) AS SALS12, "
                StrSql = StrSql + "(PERSPOS13.SALARY*PREF.CURR) AS SALS13, "
                StrSql = StrSql + "(PERSPOS14.SALARY*PREF.CURR) AS SALS14, "
                StrSql = StrSql + "(PERSPOS15.SALARY*PREF.CURR) AS SALS15, "
                StrSql = StrSql + "(PERSPOS16.SALARY*PREF.CURR) AS SALS16, "
                StrSql = StrSql + "(PERSPOS17.SALARY*PREF.CURR) AS SALS17, "
                StrSql = StrSql + "(PERSPOS18.SALARY*PREF.CURR) AS SALS18, "
                StrSql = StrSql + "(PERSPOS19.SALARY*PREF.CURR) AS SALS19, "
                StrSql = StrSql + "(PERSPOS20.SALARY*PREF.CURR) AS SALS20, "
                StrSql = StrSql + "(PERSPOS21.SALARY*PREF.CURR) AS SALS21, "
                StrSql = StrSql + "(PERSPOS22.SALARY*PREF.CURR) AS SALS22, "
                StrSql = StrSql + "(PERSPOS23.SALARY*PREF.CURR) AS SALS23, "
                StrSql = StrSql + "(PERSPOS24.SALARY*PREF.CURR) AS SALS24, "
                StrSql = StrSql + "(PERSPOS25.SALARY*PREF.CURR) AS SALS25, "
                StrSql = StrSql + "(PERSPOS26.SALARY*PREF.CURR) AS SALS26, "
                StrSql = StrSql + "(PERSPOS27.SALARY*PREF.CURR) AS SALS27, "
                StrSql = StrSql + "(PERSPOS28.SALARY*PREF.CURR) AS SALS28, "
                StrSql = StrSql + "(PERSPOS29.SALARY*PREF.CURR) AS SALS29, "
                StrSql = StrSql + "(PERSPOS30.SALARY*PREF.CURR) AS SALS30, "
                StrSql = StrSql + "(PERSAL.M1*PREF.CURR) AS SALPRE1, "
                StrSql = StrSql + "(PERSAL.M2*PREF.CURR) AS SALPRE2, "
                StrSql = StrSql + "(PERSAL.M3*PREF.CURR) AS SALPRE3, "
                StrSql = StrSql + "(PERSAL.M4*PREF.CURR) AS SALPRE4, "
                StrSql = StrSql + "(PERSAL.M5*PREF.CURR) AS SALPRE5, "
                StrSql = StrSql + "(PERSAL.M6*PREF.CURR) AS SALPRE6, "
                StrSql = StrSql + "(PERSAL.M7*PREF.CURR) AS SALPRE7, "
                StrSql = StrSql + "(PERSAL.M8*PREF.CURR) AS SALPRE8, "
                StrSql = StrSql + "(PERSAL.M9*PREF.CURR) AS SALPRE9, "
                StrSql = StrSql + "(PERSAL.M10*PREF.CURR) AS SALPRE10, "
                StrSql = StrSql + "(PERSAL.M11*PREF.CURR) AS SALPRE11, "
                StrSql = StrSql + "(PERSAL.M12*PREF.CURR) AS SALPRE12, "
                StrSql = StrSql + "(PERSAL.M13*PREF.CURR) AS SALPRE13, "
                StrSql = StrSql + "(PERSAL.M14*PREF.CURR) AS SALPRE14, "
                StrSql = StrSql + "(PERSAL.M15*PREF.CURR) AS SALPRE15, "
                StrSql = StrSql + "(PERSAL.M16*PREF.CURR) AS SALPRE16, "
                StrSql = StrSql + "(PERSAL.M17*PREF.CURR) AS SALPRE17, "
                StrSql = StrSql + "(PERSAL.M18*PREF.CURR) AS SALPRE18, "
                StrSql = StrSql + "(PERSAL.M19*PREF.CURR) AS SALPRE19, "
                StrSql = StrSql + "(PERSAL.M20*PREF.CURR) AS SALPRE20, "
                StrSql = StrSql + "(PERSAL.M21*PREF.CURR) AS SALPRE21, "
                StrSql = StrSql + "(PERSAL.M22*PREF.CURR) AS SALPRE22, "
                StrSql = StrSql + "(PERSAL.M23*PREF.CURR) AS SALPRE23, "
                StrSql = StrSql + "(PERSAL.M24*PREF.CURR) AS SALPRE24, "
                StrSql = StrSql + "(PERSAL.M25*PREF.CURR) AS SALPRE25, "
                StrSql = StrSql + "(PERSAL.M26*PREF.CURR) AS SALPRE26, "
                StrSql = StrSql + "(PERSAL.M27*PREF.CURR) AS SALPRE27, "
                StrSql = StrSql + "(PERSAL.M28*PREF.CURR) AS SALPRE28, "
                StrSql = StrSql + "(PERSAL.M29*PREF.CURR) AS SALPRE29, "
                StrSql = StrSql + "(PERSAL.M30*PREF.CURR) AS SALPRE30, "
                StrSql = StrSql + "PERSVP.M1 AS COSTTYPID1, "
                StrSql = StrSql + "PERSVP.M2 AS COSTTYPID2, "
                StrSql = StrSql + "PERSVP.M3 AS COSTTYPID3, "
                StrSql = StrSql + "PERSVP.M4 AS COSTTYPID4, "
                StrSql = StrSql + "PERSVP.M5 AS COSTTYPID5, "
                StrSql = StrSql + "PERSVP.M6 AS COSTTYPID6, "
                StrSql = StrSql + "PERSVP.M7 AS COSTTYPID7, "
                StrSql = StrSql + "PERSVP.M8 AS COSTTYPID8, "
                StrSql = StrSql + "PERSVP.M9 AS COSTTYPID9, "
                StrSql = StrSql + "PERSVP.M10 AS COSTTYPID10, "
                StrSql = StrSql + "PERSVP.M11 AS COSTTYPID11, "
                StrSql = StrSql + "PERSVP.M12 AS COSTTYPID12, "
                StrSql = StrSql + "PERSVP.M13 AS COSTTYPID13, "
                StrSql = StrSql + "PERSVP.M14 AS COSTTYPID14, "
                StrSql = StrSql + "PERSVP.M15 AS COSTTYPID15, "
                StrSql = StrSql + "PERSVP.M16 AS COSTTYPID16, "
                StrSql = StrSql + "PERSVP.M17 AS COSTTYPID17, "
                StrSql = StrSql + "PERSVP.M18 AS COSTTYPID18, "
                StrSql = StrSql + "PERSVP.M19 AS COSTTYPID19, "
                StrSql = StrSql + "PERSVP.M20 AS COSTTYPID20, "
                StrSql = StrSql + "PERSVP.M21 AS COSTTYPID21, "
                StrSql = StrSql + "PERSVP.M22 AS COSTTYPID22, "
                StrSql = StrSql + "PERSVP.M23 AS COSTTYPID23, "
                StrSql = StrSql + "PERSVP.M24 AS COSTTYPID24, "
                StrSql = StrSql + "PERSVP.M25 AS COSTTYPID25, "
                StrSql = StrSql + "PERSVP.M26 AS COSTTYPID26, "
                StrSql = StrSql + "PERSVP.M27 AS COSTTYPID27, "
                StrSql = StrSql + "PERSVP.M28 AS COSTTYPID28, "
                StrSql = StrSql + "PERSVP.M29 AS COSTTYPID29, "
                StrSql = StrSql + "PERSVP.M30 AS COSTTYPID30, "
                StrSql = StrSql + "PERDEP.M1 AS DEPID1, "
                StrSql = StrSql + "PERDEP.M2 AS DEPID2, "
                StrSql = StrSql + "PERDEP.M3 AS DEPID3, "
                StrSql = StrSql + "PERDEP.M4 AS DEPID4, "
                StrSql = StrSql + "PERDEP.M5 AS DEPID5, "
                StrSql = StrSql + "PERDEP.M6 AS DEPID6, "
                StrSql = StrSql + "PERDEP.M7 AS DEPID7, "
                StrSql = StrSql + "PERDEP.M8 AS DEPID8, "
                StrSql = StrSql + "PERDEP.M9 AS DEPID9, "
                StrSql = StrSql + "PERDEP.M10 AS DEPID10, "
                StrSql = StrSql + "PERDEP.M11 AS DEPID11, "
                StrSql = StrSql + "PERDEP.M12 AS DEPID12, "
                StrSql = StrSql + "PERDEP.M13 AS DEPID13, "
                StrSql = StrSql + "PERDEP.M14 AS DEPID14, "
                StrSql = StrSql + "PERDEP.M15 AS DEPID15, "
                StrSql = StrSql + "PERDEP.M16 AS DEPID16, "
                StrSql = StrSql + "PERDEP.M17 AS DEPID17, "
                StrSql = StrSql + "PERDEP.M18 AS DEPID18, "
                StrSql = StrSql + "PERDEP.M19 AS DEPID19, "
                StrSql = StrSql + "PERDEP.M20 AS DEPID20, "
                StrSql = StrSql + "PERDEP.M21 AS DEPID21, "
                StrSql = StrSql + "PERDEP.M22 AS DEPID22, "
                StrSql = StrSql + "PERDEP.M23 AS DEPID23, "
                StrSql = StrSql + "PERDEP.M24 AS DEPID24, "
                StrSql = StrSql + "PERDEP.M25 AS DEPID25, "
                StrSql = StrSql + "PERDEP.M26 AS DEPID26, "
                StrSql = StrSql + "PERDEP.M27 AS DEPID27, "
                StrSql = StrSql + "PERDEP.M28 AS DEPID28, "
                StrSql = StrSql + "PERDEP.M29 AS DEPID29, "
                StrSql = StrSql + "PERDEP.M30 AS DEPID30, "
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
                StrSql = StrSql + "FROM PERSONNELPOS PERSPOS "
                StrSql = StrSql + "INNER JOIN PERSONNELNUM PNUM "
                StrSql = StrSql + "ON PNUM.CASEID=PERSPOS.CASEID "
                StrSql = StrSql + "INNER JOIN PREFERENCES  PREF ON "
                StrSql = StrSql + "PREF.CASEID=PERSPOS.CASEID "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + " PERSPOS1 "
                StrSql = StrSql + "ON PERSPOS1.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS1.PERSID = PERSPOS.M1 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS2 "
                StrSql = StrSql + "ON PERSPOS2.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS2.PERSID = PERSPOS.M2 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS3 "
                StrSql = StrSql + "ON PERSPOS3.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS3.PERSID = PERSPOS.M3 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS4 "
                StrSql = StrSql + "ON PERSPOS4.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS4.PERSID = PERSPOS.M4 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS5 "
                StrSql = StrSql + "ON PERSPOS5.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS5.PERSID = PERSPOS.M5 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS6 "
                StrSql = StrSql + "ON PERSPOS6.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS6.PERSID = PERSPOS.M6 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS7 "
                StrSql = StrSql + "ON PERSPOS7.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS7.PERSID = PERSPOS.M7 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS8 "
                StrSql = StrSql + "ON PERSPOS8.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS8.PERSID = PERSPOS.M8 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS9 "
                StrSql = StrSql + "ON PERSPOS9.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS9.PERSID = PERSPOS.M9 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS10 "
                StrSql = StrSql + "ON PERSPOS10.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS10.PERSID = PERSPOS.M10 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS11 "
                StrSql = StrSql + "ON PERSPOS11.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS11.PERSID = PERSPOS.M11 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS12 "
                StrSql = StrSql + "ON PERSPOS12.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS12.PERSID = PERSPOS.M12 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS13 "
                StrSql = StrSql + "ON PERSPOS13.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS13.PERSID = PERSPOS.M13 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS14 "
                StrSql = StrSql + "ON PERSPOS14.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS14.PERSID = PERSPOS.M14 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS15 "
                StrSql = StrSql + "ON PERSPOS15.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS15.PERSID = PERSPOS.M15 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS16 "
                StrSql = StrSql + "ON PERSPOS16.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS16.PERSID = PERSPOS.M16 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS17 "
                StrSql = StrSql + "ON PERSPOS17.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS17.PERSID = PERSPOS.M17 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS18 "
                StrSql = StrSql + "ON PERSPOS18.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS18.PERSID = PERSPOS.M18 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS19 "
                StrSql = StrSql + "ON PERSPOS19.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS19.PERSID = PERSPOS.M19 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS20 "
                StrSql = StrSql + "ON PERSPOS20.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS20.PERSID = PERSPOS.M20 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS21 "
                StrSql = StrSql + "ON PERSPOS21.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS21.PERSID = PERSPOS.M21 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS22 "
                StrSql = StrSql + "ON PERSPOS22.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS22.PERSID = PERSPOS.M22 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS23 "
                StrSql = StrSql + "ON PERSPOS23.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS23.PERSID = PERSPOS.M23 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS24 "
                StrSql = StrSql + "ON PERSPOS24.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS24.PERSID = PERSPOS.M24 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS25 "
                StrSql = StrSql + "ON PERSPOS25.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS25.PERSID = PERSPOS.M25 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS26 "
                StrSql = StrSql + "ON PERSPOS26.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS26.PERSID = PERSPOS.M26 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS27 "
                StrSql = StrSql + "ON PERSPOS27.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS27.PERSID = PERSPOS.M27 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS28 "
                StrSql = StrSql + "ON PERSPOS28.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS28.PERSID = PERSPOS.M28 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS29 "
                StrSql = StrSql + "ON PERSPOS29.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS29.PERSID = PERSPOS.M29 "
                StrSql = StrSql + "LEFT OUTER JOIN ECon." + EFFCOUNTRY + "  PERSPOS30 "
                StrSql = StrSql + "ON PERSPOS30.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS30.PERSID = PERSPOS.M30 "
                StrSql = StrSql + "INNER JOIN PERSONNELSAL PERSAL "
                StrSql = StrSql + "ON PERSAL.CASEID=PERSPOS.CASEID "
                StrSql = StrSql + "INNER JOIN PERSONNELVP PERSVP "
                StrSql = StrSql + "ON PERSVP.CASEID=PERSPOS.CASEID "
                StrSql = StrSql + "INNER JOIN PERSONNELDEP PERDEP "
                StrSql = StrSql + "ON PERDEP.CASEID=PERSPOS.CASEID "
                StrSql = StrSql + "WHERE PERSPOS.CASEID=" + CaseId.ToString() + ""
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetPersonnelInDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPlantConfig2Details(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "Select PLS.CASEID,  "
                StrSql = StrSql + "'Production' AS PSPACE1, "
                StrSql = StrSql + "'High Bay' AS PSPACE2, "
                StrSql = StrSql + "'Partial High Bay' AS PSPACE3, "
                StrSql = StrSql + "'Standard' AS PSPACE4, "
                StrSql = StrSql + "'Production Total' AS PSPACE5, " 'added for bug#345
                StrSql = StrSql + "'Warehouse' AS PSPACE6, "
                StrSql = StrSql + "'Office' AS PSPACE7, "
                StrSql = StrSql + "'Support' AS PSPACE8, "
                StrSql = StrSql + "(PLS.M1* PREF.CONVAREA2) AS AR1, "
                StrSql = StrSql + "(PLS.M2* PREF.CONVAREA2) AS AR2, "
                StrSql = StrSql + "(PLS.M3* PREF.CONVAREA2) AS AR3, "
                StrSql = StrSql + "(PLS.M4* PREF.CONVAREA2) AS AR4, "
                StrSql = StrSql + "(PLS.M5* PREF.CONVAREA2) AS ARTOT, "
                StrSql = StrSql + "PREF.TITLE7 AS TITLE7, "
                StrSql = StrSql + "PREF.TITLE4 AS TITLE4, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN (ART1.LEASERATE*PREF.CURR) ELSE (ART1.LEASERATE*PREF.CURR/PREF.CONVAREA2) END AS  SUG1, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN (ART2.LEASERATE*PREF.CURR) ELSE (ART2.LEASERATE*PREF.CURR/PREF.CONVAREA2) END AS  SUG2, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN (ART3.LEASERATE*PREF.CURR) ELSE (ART3.LEASERATE*PREF.CURR/PREF.CONVAREA2) END AS  SUG3, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN (ART4.LEASERATE*PREF.CURR) ELSE (ART4.LEASERATE*PREF.CURR/PREF.CONVAREA2) END AS  SUG4, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN (ART5.LEASERATE*PREF.CURR) ELSE (ART5.LEASERATE*PREF.CURR/PREF.CONVAREA2) END AS  SUG5, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN (ART6.LEASERATE*PREF.CURR) ELSE (ART6.LEASERATE*PREF.CURR/PREF.CONVAREA2) END AS  SUG6, "
                StrSql = StrSql + "CASE PREF.UNITS WHEN 0 THEN "
                StrSql = StrSql + "PLS.PRODUCTIONLEASEHB*PREF.CURR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "PLS.PRODUCTIONLEASEHB*PREF.CURR/PREF.CONVAREA2 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AS PREF1 , "
                StrSql = StrSql + "CASE PREF.UNITS WHEN 0 THEN "
                StrSql = StrSql + "PLS.PRODUCTIONLEASEPHB*PREF.CURR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "PLS.PRODUCTIONLEASEPHB*PREF.CURR/PREF.CONVAREA2 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AS PREF2 , "
                StrSql = StrSql + "CASE PREF.UNITS WHEN 0 THEN "
                StrSql = StrSql + "PLS.PRODUCTIONLEASE*PREF.CURR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "PLS.PRODUCTIONLEASE*PREF.CURR/PREF.CONVAREA2 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AS PREF3, "
                StrSql = StrSql + "CASE PREF.UNITS WHEN 0 THEN "
                StrSql = StrSql + "PLS.WAREHOUSELEASE*PREF.CURR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "PLS.WAREHOUSELEASE*PREF.CURR/PREF.CONVAREA2 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AS PREF4, "
                StrSql = StrSql + "CASE PREF.UNITS WHEN 0 THEN "
                StrSql = StrSql + "PLS.OFFICELEASE*PREF.CURR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "PLS.OFFICELEASE*PREF.CURR/PREF.CONVAREA2 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AS PREF5, "
                StrSql = StrSql + "CASE PREF.UNITS WHEN 0 THEN "
                StrSql = StrSql + "PLS.SUPPORTLEASE*PREF.CURR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "PLS.SUPPORTLEASE*PREF.CURR/PREF.CONVAREA2 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AS PREF6, "

                StrSql = StrSql + "(PLS.PHIGHBAY* PREF.CURR) AS PTOT1, "
                StrSql = StrSql + "(PLS.PPHIGHBAY* PREF.CURR) AS PTOT2, "
                StrSql = StrSql + "(PLS.PSTD* PREF.CURR) AS PTOT3, "
                StrSql = StrSql + "(PLS.M6* PREF.CURR) AS PTOT4, "
                StrSql = StrSql + "(PLS.M7* PREF.CURR) AS PTOT5, "
                StrSql = StrSql + "(PLS.M8* PREF.CURR) AS PTOT6, "
                StrSql = StrSql + "(PLS.M9* PREF.CURR) AS PTOT7, "
                StrSql = StrSql + "(PLS.M10* PREF.CURR) AS LEASETOTAL, "

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

                StrSql = StrSql + "FROM PLANTSPACE PLS "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=PLS.CASEID "
                StrSql = StrSql + "INNER JOIN ECON.AREATYPE ART1 "
                StrSql = StrSql + "ON ART1.AREAID=1 "
                StrSql = StrSql + "INNER JOIN ECON.AREATYPE ART2 "
                StrSql = StrSql + "ON ART2.AREAID=2 "
                StrSql = StrSql + "INNER JOIN ECON.AREATYPE ART3 "
                StrSql = StrSql + "ON ART3.AREAID=3 "
                StrSql = StrSql + "INNER JOIN ECON.AREATYPE ART4 "
                StrSql = StrSql + "ON ART4.AREAID=4 "
                StrSql = StrSql + "INNER JOIN ECON.AREATYPE ART5 "
                StrSql = StrSql + "ON ART5.AREAID=5 "
                StrSql = StrSql + "INNER JOIN ECON.AREATYPE ART6 "
                StrSql = StrSql + "ON ART6.AREAID=6 "
                StrSql = StrSql + "WHERE  PLS.CASEID=" + CaseId.ToString() + ""
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetPlantConfig2Details:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetEnergyDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  'Production' AS PSPACE1,  "
                StrSql = StrSql + "'Warehouse' AS PSPACE2, "
                StrSql = StrSql + "'Office' AS PSPACE3, "
                StrSql = StrSql + "'Support' AS PSPACE4, "
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
                StrSql = StrSql + "PREF.UNITS , "
                StrSql = StrSql + "(B1.PRICE*PREF.CURR) ERGPSUG1, "
                StrSql = StrSql + "(B2.PRICE*PREF.CURR) ERGPSUG2, "
                StrSql = StrSql + "(PENG.ELECPRICE*PREF.CURR) ERGPPREF1, "
                StrSql = StrSql + "(PENG.NGASPRICE*PREF.CURR) ERGPPREF2, "
                StrSql = StrSql + "PENG.M1 AS ENRGE1, "
                StrSql = StrSql + "PENG.M2 AS ENRGE2, "
                StrSql = StrSql + "PENG.M3 AS ENRGE3, "
                StrSql = StrSql + "PENG.M4 AS ENRGE4, "
                StrSql = StrSql + "PENG.M5 AS ENRGE5, "
                StrSql = StrSql + "PENG.M6 AS ENRGN1, "
                StrSql = StrSql + "PENG.M7 AS ENRGN2, "
                StrSql = StrSql + "PENG.M8 AS ENRGN3, "
                StrSql = StrSql + "PENG.M9 AS ENRGN4, "
                StrSql = StrSql + "PENG.M10 AS ENRGN5, "
                StrSql = StrSql + "(PENG.D1*PREF.CURR) AS ENRGCE1, "
                StrSql = StrSql + "(PENG.D2*PREF.CURR) AS ENRGCE2, "
                StrSql = StrSql + "(PENG.D3*PREF.CURR) AS ENRGCE3, "
                StrSql = StrSql + "(PENG.D4*PREF.CURR) AS ENRGCE4, "
                StrSql = StrSql + "(PENG.D5*PREF.CURR) AS ENRGCE5, "
                StrSql = StrSql + "(PENG.D6*PREF.CURR) AS ENRGCG1, "
                StrSql = StrSql + "(PENG.D7*PREF.CURR) AS ENRGCG2, "
                StrSql = StrSql + "(PENG.D8*PREF.CURR) AS ENRGCG3, "
                StrSql = StrSql + "(PENG.D9*PREF.CURR) AS ENRGCG4, "
                StrSql = StrSql + "(PENG.D10*PREF.CURR) AS ENRGCG5, "
                StrSql = StrSql + "(PENG.D11*PREF.CURR) AS TOTAL1, "
                StrSql = StrSql + "(PENG.D12*PREF.CURR) AS TOTAL2, "
                StrSql = StrSql + "(PENG.D13*PREF.CURR) AS TOTAL3, "
                StrSql = StrSql + "(PENG.D14*PREF.CURR) AS TOTAL4, "
                StrSql = StrSql + "(PENG.D15*PREF.CURR) AS TOTAL5, "
                StrSql = StrSql + "(PENG.V1*PREF.CURR) AS VTOTAL1, "
                StrSql = StrSql + "(PENG.V2*PREF.CURR) AS VTOTAL2, "
                StrSql = StrSql + "(PENG.V3*PREF.CURR) AS VTOTAL3, "
                StrSql = StrSql + "(PENG.V4*PREF.CURR) AS VTOTAL4, "
                StrSql = StrSql + "(PENG.V5*PREF.CURR) AS VTOTAL5, "
                StrSql = StrSql + "(PENG.F1*PREF.CURR) AS FTOTAL1, "
                StrSql = StrSql + "(PENG.F2*PREF.CURR) AS FTOTAL2, "
                StrSql = StrSql + "(PENG.F3*PREF.CURR) AS FTOTAL3, "
                StrSql = StrSql + "(PENG.F4*PREF.CURR) AS FTOTAL4, "
                StrSql = StrSql + "(PENG.F5*PREF.CURR) AS FTOTAL5 "
                StrSql = StrSql + "FROM PREFERENCES PREF "
                StrSql = StrSql + "INNER JOIN PLANTENERGY PENG "
                StrSql = StrSql + "ON PENG.CASEID= PREF.CASEID "
                StrSql = StrSql + "INNER JOIN ECON.ENERGYARCH B1 "
                StrSql = StrSql + "ON B1.ENERGYID = 1 "
                StrSql = StrSql + "AND B1.EFFDATE =PENG.EFFDATE "
                StrSql = StrSql + "INNER JOIN ECON.ENERGYARCH B2 "
                StrSql = StrSql + "ON B2.ENERGYID=2 "
                StrSql = StrSql + "AND B2.EFFDATE =PENG.EFFDATE "
                StrSql = StrSql + "WHERE PREF.CASEID =" + CaseId.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetEnergyDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCustomerINDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT CIN.CASEID,  "
                StrSql = StrSql + "PREF.TITLE2 AS PREFT2, "
                StrSql = StrSql + "PREF.TITLE8 AS PREFT8, "
                StrSql = StrSql + "(CIN.M1*PREF.CURR/PREF.CONVWT) AS PRODPUR, "
                StrSql = StrSql + "CIN.M2*PREF.CONVTHICK3 AS SHIPDIST, "
                StrSql = StrSql + "(CIN.M3*PREF.CURR/PREF.CONVTHICK3) AS MILCOST, "
                StrSql = StrSql + "PREF.TITLE4 AS PREFT4, "
                StrSql = StrSql + "PREF.UNITS "
                StrSql = StrSql + "FROM CUSTOMERIN CIN "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=CIN.CASEID "
                StrSql = StrSql + "WHERE CIN.CASEID=" + CaseId.ToString() + ""
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetCustomerINDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetFixedCostDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT FIXCOST.CASEID,  "
                StrSql = StrSql + "'Office supplies:' AS CATEGORY1, "
                StrSql = StrSql + "'Laboratory supplies:' AS CATEGORY2, "
                StrSql = StrSql + "'Insurance:' AS CATEGORY3, "
                StrSql = StrSql + "'Travel:' AS CATEGORY4, "
                StrSql = StrSql + "'Voice and data:' AS CATEGORY5, "
                StrSql = StrSql + "'Water:' AS CATEGORY6, "
                StrSql = StrSql + "'Waste disposal' AS CATEGORY7, "
                StrSql = StrSql + "'Maintenance supplies' AS CATEGORY8, "
                StrSql = StrSql + "'Minor equipment' AS CATEGORY9, "
                StrSql = StrSql + "'Outside services' AS CATEGORY10, "
                StrSql = StrSql + "'Professional services' AS CATEGORY11, "
                StrSql = StrSql + "'Ink supplies' AS CATEGORY12, "
                StrSql = StrSql + "'Plate supplies' AS CATEGORY13, "
                StrSql = StrSql + "'Metallization supplies' AS CATEGORY14, "
                StrSql = StrSql + "'Expense15:' AS CATEGORY15, "
                StrSql = StrSql + "'Expense16:' AS CATEGORY16, "
                StrSql = StrSql + "'Expense17:' AS CATEGORY17, "
                StrSql = StrSql + "'Expense18:' AS CATEGORY18, "
                StrSql = StrSql + "'Expense19:' AS CATEGORY19, "
                StrSql = StrSql + "'Expense20:' AS CATEGORY20, "
                StrSql = StrSql + "'Expense21:' AS CATEGORY21, "
                StrSql = StrSql + "'Expense22:' AS CATEGORY22, "
                StrSql = StrSql + "'Expense23:' AS CATEGORY23, "
                StrSql = StrSql + "'Expense24:' AS CATEGORY24, "
                StrSql = StrSql + "'Expense25:' AS CATEGORY25, "
                StrSql = StrSql + "'Expense26:' AS CATEGORY26, "
                StrSql = StrSql + "'Expense27:' AS CATEGORY27, "
                StrSql = StrSql + "'Expense28:' AS CATEGORY28, "
                StrSql = StrSql + "'Expense29:' AS CATEGORY29, "
                StrSql = StrSql + "'Expense30:' AS CATEGORY30, "
                StrSql = StrSql + "(FIXCOST.M1*PREF.CURR) AS FIXCOST1, "
                StrSql = StrSql + "(FIXCOST.M2*PREF.CURR) AS FIXCOST2, "
                StrSql = StrSql + "FIXCOST.M3 AS FIXCOST3, "
                StrSql = StrSql + "FIXCOST.M4 AS FIXCOST4, "
                StrSql = StrSql + "(FIXCOST.M5*PREF.CURR) AS FIXCOST5, "
                StrSql = StrSql + "(FIXCOST.M6*PREF.CURR) AS FIXCOST6, "
                StrSql = StrSql + "(FIXCOST.M7*PREF.CURR) AS FIXCOST7, "
                StrSql = StrSql + "FIXCOST.M8 AS FIXCOST8, "
                StrSql = StrSql + "FIXCOST.M9 AS FIXCOST9, "
                StrSql = StrSql + "FIXCOST.M10 AS FIXCOST10, "
                StrSql = StrSql + "FIXCOST.M11 AS FIXCOST11, "
                StrSql = StrSql + "FIXCOST.M12 AS FIXCOST12, "
                StrSql = StrSql + "FIXCOST.M13 AS FIXCOST13, "
                StrSql = StrSql + "FIXCOST.M14 AS FIXCOST14, "
                StrSql = StrSql + "FIXCOST.M15 AS FIXCOST15, "
                StrSql = StrSql + "FIXCOST.M16 AS FIXCOST16, "
                StrSql = StrSql + "FIXCOST.M17 AS FIXCOST17, "
                StrSql = StrSql + "FIXCOST.M18 AS FIXCOST18, "
                StrSql = StrSql + "FIXCOST.M19 AS FIXCOST19, "
                StrSql = StrSql + "FIXCOST.M20 AS FIXCOST20, "
                StrSql = StrSql + "FIXCOST.M21 AS FIXCOST21, "
                StrSql = StrSql + "FIXCOST.M22 AS FIXCOST22, "
                StrSql = StrSql + "FIXCOST.M23 AS FIXCOST23, "
                StrSql = StrSql + "FIXCOST.M24 AS FIXCOST24, "
                StrSql = StrSql + "FIXCOST.M25 AS FIXCOST25, "
                StrSql = StrSql + "FIXCOST.M26 AS FIXCOST26, "
                StrSql = StrSql + "FIXCOST.M27 AS FIXCOST27, "
                StrSql = StrSql + "FIXCOST.M28 AS FIXCOST28, "
                StrSql = StrSql + "FIXCOST.M29 AS FIXCOST29, "
                StrSql = StrSql + "FIXCOST.M30 AS FIXCOST30, "
                StrSql = StrSql + "'currency per employee' AS RULE1, "
                StrSql = StrSql + "'currency per employee' AS RULE2, "
                StrSql = StrSql + "'percent of asset base' AS RULE3, "
                StrSql = StrSql + "'percent of payroll' AS RULE4, "
                StrSql = StrSql + "'currency per employee' AS RULE5, "
                StrSql = StrSql + "'currency per employee' AS RULE6, "
                StrSql = StrSql + "'currency per employee' AS RULE7, "
                StrSql = StrSql + "'percent of asset base' AS RULE8, "
                StrSql = StrSql + "'percent of asset base' AS RULE9, "
                StrSql = StrSql + "'percent of asset base' AS RULE10, "
                StrSql = StrSql + "'percent of payroll' AS RULE11, "
                StrSql = StrSql + "'percent of ink cost' AS RULE12, "
                StrSql = StrSql + "'percent of plate cost' AS RULE13, "
                StrSql = StrSql + "'percent of metal cost' AS RULE14, "
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
                StrSql = StrSql + "PREF.UNITS , "
                StrSql = StrSql + "'' AS RULE15, "
                StrSql = StrSql + "'' AS RULE16, "
                StrSql = StrSql + "'' AS RULE17, "
                StrSql = StrSql + "'' AS RULE18, "
                StrSql = StrSql + "'' AS RULE19, "
                StrSql = StrSql + "'' AS RULE20, "
                StrSql = StrSql + "'' AS RULE21, "
                StrSql = StrSql + "'' AS RULE22, "
                StrSql = StrSql + "'' AS RULE23, "
                StrSql = StrSql + "'' AS RULE24, "
                StrSql = StrSql + "'' AS RULE25, "
                StrSql = StrSql + "'' AS RULE26, "
                StrSql = StrSql + "'' AS RULE27, "
                StrSql = StrSql + "'' AS RULE28, "
                StrSql = StrSql + "'' AS RULE29, "
                StrSql = StrSql + "'' AS RULE30, "
                StrSql = StrSql + "(FIXCOSTSUG.M1*PREF.CURR) AS FCSG1, "
                StrSql = StrSql + "(FIXCOSTSUG.M2*PREF.CURR) AS FCSG2, "
                StrSql = StrSql + "(FIXCOSTSUG.M3*PREF.CURR) AS FCSG3, "
                StrSql = StrSql + "(FIXCOSTSUG.M4*PREF.CURR) AS FCSG4, "
                StrSql = StrSql + "(FIXCOSTSUG.M5*PREF.CURR) AS FCSG5, "
                StrSql = StrSql + "(FIXCOSTSUG.M6*PREF.CURR) AS FCSG6, "
                StrSql = StrSql + "(FIXCOSTSUG.M7*PREF.CURR) AS FCSG7, "
                StrSql = StrSql + "(FIXCOSTSUG.M8*PREF.CURR) AS FCSG8, "
                StrSql = StrSql + "(FIXCOSTSUG.M9*PREF.CURR) AS FCSG9, "
                StrSql = StrSql + "(FIXCOSTSUG.M10*PREF.CURR) AS FCSG10, "
                StrSql = StrSql + "(FIXCOSTSUG.M11*PREF.CURR) AS FCSG11, "
                StrSql = StrSql + "(FIXCOSTSUG.M12*PREF.CURR) AS FCSG12, "
                StrSql = StrSql + "(FIXCOSTSUG.M13*PREF.CURR) AS FCSG13, "
                StrSql = StrSql + "(FIXCOSTSUG.M14*PREF.CURR) AS FCSG14, "
                StrSql = StrSql + "(FIXCOSTSUG.M15*PREF.CURR) AS FCSG15, "
                StrSql = StrSql + "(FIXCOSTSUG.M16*PREF.CURR) AS FCSG16, "
                StrSql = StrSql + "(FIXCOSTSUG.M17*PREF.CURR) AS FCSG17, "
                StrSql = StrSql + "(FIXCOSTSUG.M18*PREF.CURR) AS FCSG18, "
                StrSql = StrSql + "(FIXCOSTSUG.M19*PREF.CURR) AS FCSG19, "
                StrSql = StrSql + "(FIXCOSTSUG.M20*PREF.CURR) AS FCSG20, "
                StrSql = StrSql + "(FIXCOSTSUG.M21*PREF.CURR) AS FCSG21, "
                StrSql = StrSql + "(FIXCOSTSUG.M22*PREF.CURR) AS FCSG22, "
                StrSql = StrSql + "(FIXCOSTSUG.M23*PREF.CURR) AS FCSG23, "
                StrSql = StrSql + "(FIXCOSTSUG.M24*PREF.CURR) AS FCSG24, "
                StrSql = StrSql + "(FIXCOSTSUG.M25*PREF.CURR) AS FCSG25, "
                StrSql = StrSql + "(FIXCOSTSUG.M26*PREF.CURR) AS FCSG26, "
                StrSql = StrSql + "(FIXCOSTSUG.M27*PREF.CURR) AS FCSG27, "
                StrSql = StrSql + "(FIXCOSTSUG.M28*PREF.CURR) AS FCSG28, "
                StrSql = StrSql + "(FIXCOSTSUG.M29*PREF.CURR) AS FCSG29, "
                StrSql = StrSql + "(FIXCOSTSUG.M30*PREF.CURR) AS FCSG30, "
                StrSql = StrSql + "(FIXCOSTPRE.M1*PREF.CURR) AS FCPREF1, "
                StrSql = StrSql + "(FIXCOSTPRE.M2*PREF.CURR) AS FCPREF2, "
                StrSql = StrSql + "(FIXCOSTPRE.M3*PREF.CURR) AS FCPREF3, "
                StrSql = StrSql + "(FIXCOSTPRE.M4*PREF.CURR) AS FCPREF4, "
                StrSql = StrSql + "(FIXCOSTPRE.M5*PREF.CURR) AS FCPREF5, "
                StrSql = StrSql + "(FIXCOSTPRE.M6*PREF.CURR) AS FCPREF6, "
                StrSql = StrSql + "(FIXCOSTPRE.M7*PREF.CURR) AS FCPREF7, "
                StrSql = StrSql + "(FIXCOSTPRE.M8*PREF.CURR) AS FCPREF8, "
                StrSql = StrSql + "(FIXCOSTPRE.M9*PREF.CURR) AS FCPREF9, "
                StrSql = StrSql + "(FIXCOSTPRE.M10*PREF.CURR) AS FCPREF10, "
                StrSql = StrSql + "(FIXCOSTPRE.M11*PREF.CURR) AS FCPREF11, "
                StrSql = StrSql + "(FIXCOSTPRE.M12*PREF.CURR) AS FCPREF12, "
                StrSql = StrSql + "(FIXCOSTPRE.M13*PREF.CURR) AS FCPREF13, "
                StrSql = StrSql + "(FIXCOSTPRE.M14*PREF.CURR) AS FCPREF14, "
                StrSql = StrSql + "(FIXCOSTPRE.M15*PREF.CURR) AS FCPREF15, "
                StrSql = StrSql + "(FIXCOSTPRE.M16*PREF.CURR) AS FCPREF16, "
                StrSql = StrSql + "(FIXCOSTPRE.M17*PREF.CURR) AS FCPREF17, "
                StrSql = StrSql + "(FIXCOSTPRE.M18*PREF.CURR) AS FCPREF18, "
                StrSql = StrSql + "(FIXCOSTPRE.M19*PREF.CURR) AS FCPREF19, "
                StrSql = StrSql + "(FIXCOSTPRE.M20*PREF.CURR) AS FCPREF20, "
                StrSql = StrSql + "(FIXCOSTPRE.M21*PREF.CURR) AS FCPREF21, "
                StrSql = StrSql + "(FIXCOSTPRE.M22*PREF.CURR) AS FCPREF22, "
                StrSql = StrSql + "(FIXCOSTPRE.M23*PREF.CURR) AS FCPREF23, "
                StrSql = StrSql + "(FIXCOSTPRE.M24*PREF.CURR) AS FCPREF24, "
                StrSql = StrSql + "(FIXCOSTPRE.M25*PREF.CURR) AS FCPREF25, "
                StrSql = StrSql + "(FIXCOSTPRE.M26*PREF.CURR) AS FCPREF26, "
                StrSql = StrSql + "(FIXCOSTPRE.M27*PREF.CURR) AS FCPREF27, "
                StrSql = StrSql + "(FIXCOSTPRE.M28*PREF.CURR) AS FCPREF28, "
                StrSql = StrSql + "(FIXCOSTPRE.M29*PREF.CURR) AS FCPREF29, "
                StrSql = StrSql + "(FIXCOSTPRE.M30*PREF.CURR) AS FCPREF30, "
                StrSql = StrSql + "FCDEP.M1 as DEPID1, "
                StrSql = StrSql + "FCDEP.M2 as DEPID2, "
                StrSql = StrSql + "FCDEP.M3 as DEPID3, "
                StrSql = StrSql + "FCDEP.M4 as DEPID4, "
                StrSql = StrSql + "FCDEP.M5 as DEPID5, "
                StrSql = StrSql + "FCDEP.M6 as DEPID6, "
                StrSql = StrSql + "FCDEP.M7 as DEPID7, "
                StrSql = StrSql + "FCDEP.M8 as DEPID8, "
                StrSql = StrSql + "FCDEP.M9 as DEPID9, "
                StrSql = StrSql + "FCDEP.M10 as DEPID10, "
                StrSql = StrSql + "FCDEP.M11 as DEPID11, "
                StrSql = StrSql + "FCDEP.M12 as DEPID12, "
                StrSql = StrSql + "FCDEP.M13 as DEPID13, "
                StrSql = StrSql + "FCDEP.M14 as DEPID14, "
                StrSql = StrSql + "FCDEP.M15 as DEPID15, "
                StrSql = StrSql + "FCDEP.M16 as DEPID16, "
                StrSql = StrSql + "FCDEP.M17 as DEPID17, "
                StrSql = StrSql + "FCDEP.M18 as DEPID18, "
                StrSql = StrSql + "FCDEP.M19 as DEPID19, "
                StrSql = StrSql + "FCDEP.M20 as DEPID20, "
                StrSql = StrSql + "FCDEP.M21 as DEPID21, "
                StrSql = StrSql + "FCDEP.M22 as DEPID22, "
                StrSql = StrSql + "FCDEP.M23 as DEPID23, "
                StrSql = StrSql + "FCDEP.M24 as DEPID24, "
                StrSql = StrSql + "FCDEP.M25 as DEPID25, "
                StrSql = StrSql + "FCDEP.M26 as DEPID26, "
                StrSql = StrSql + "FCDEP.M27 as DEPID27, "
                StrSql = StrSql + "FCDEP.M28 as DEPID28, "
                StrSql = StrSql + "FCDEP.M29 as DEPID29, "
                StrSql = StrSql + "FCDEP.M30 as DEPID30, "
                StrSql = StrSql + "(TOTAL.ASSETTOTAL*PREF.CURR) AS ASSETTOTAL, "
                StrSql = StrSql + "DEPC.YEARS AS DEPYEARS, "
                StrSql = StrSql + "(DEPC.DEPRECIATION*PREF.CURR) AS DEPANNUAL "
                StrSql = StrSql + "FROM FIXEDCOSTPCT FIXCOST "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=FIXCOST.CASEID "
                StrSql = StrSql + "INNER JOIN FIXEDCOSTSUG FIXCOSTSUG "
                StrSql = StrSql + "ON FIXCOSTSUG.CASEID=FIXCOST.CASEID "
                StrSql = StrSql + "INNER JOIN FIXEDCOSTPRE FIXCOSTPRE "
                StrSql = StrSql + "ON FIXCOSTPRE.CASEID=FIXCOST.CASEID "
                StrSql = StrSql + "INNER JOIN TOTAL "
                StrSql = StrSql + "ON TOTAL.CASEID=FIXCOST.CASEID "
                StrSql = StrSql + "INNER JOIN DEPRECIATION DEPC "
                StrSql = StrSql + "ON DEPC.CASEID=FIXCOST.CASEID "
                StrSql = StrSql + "INNER JOIN FIXEDCOSTDEP FCDEP "
                StrSql = StrSql + "ON FCDEP.CASEID=FIXCOST.CASEID "
                StrSql = StrSql + "WHERE FIXCOST.CASEID=" + CaseId.ToString() + ""
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetFixedCostDetails:" + ex.Message.ToString())
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
                StrSql = StrSql + "(PAL1.weight*PREF.CONVWT) AS WS1, "
                StrSql = StrSql + "(PAL2.weight*PREF.CONVWT) AS WS2, "
                StrSql = StrSql + "(PAL3.weight*PREF.CONVWT) AS WS3, "
                StrSql = StrSql + "(PAL4.weight*PREF.CONVWT) AS WS4, "
                StrSql = StrSql + "(PAL5.weight*PREF.CONVWT) AS WS5, "
                StrSql = StrSql + "(PAL6.weight*PREF.CONVWT) AS WS6, "
                StrSql = StrSql + "(PAL7.weight*PREF.CONVWT) AS WS7, "
                StrSql = StrSql + "(PAL8.weight*PREF.CONVWT) AS WS8, "
                StrSql = StrSql + "(PAL9.weight*PREF.CONVWT) AS WS9, "
                StrSql = StrSql + "(PAL10.weight*PREF.CONVWT) AS WS10, "
                StrSql = StrSql + "(PI.W1*PREF.CONVWT) AS WP1, "
                StrSql = StrSql + "(PI.W2*PREF.CONVWT) AS WP2, "
                StrSql = StrSql + "(PI.W3*PREF.CONVWT) AS WP3, "
                StrSql = StrSql + "(PI.W4*PREF.CONVWT) AS WP4, "
                StrSql = StrSql + "(PI.W5*PREF.CONVWT) AS WP5, "
                StrSql = StrSql + "(PI.W6*PREF.CONVWT) AS WP6, "
                StrSql = StrSql + "(PI.W7*PREF.CONVWT) AS WP7, "
                StrSql = StrSql + "(PI.W8*PREF.CONVWT) AS WP8, "
                StrSql = StrSql + "(PI.W9*PREF.CONVWT) AS WP9, "
                StrSql = StrSql + "(PI.W10*PREF.CONVWT) AS WP10, "
                StrSql = StrSql + "(PAL1.price*PREF.curr) AS PS1, "
                StrSql = StrSql + "(PAL2.price*PREF.curr) AS PS2, "
                StrSql = StrSql + "(PAL3.price*PREF.curr) AS PS3, "
                StrSql = StrSql + "(PAL4.price*PREF.curr) AS PS4, "
                StrSql = StrSql + "(PAL5.price*PREF.curr) AS PS5, "
                StrSql = StrSql + "(PAL6.price*PREF.curr) AS PS6, "
                StrSql = StrSql + "(PAL7.price*PREF.curr) AS PS7, "
                StrSql = StrSql + "(PAL8.price*PREF.curr) AS PS8, "
                StrSql = StrSql + "(PAL9.price*PREF.curr) AS PS9, "
                StrSql = StrSql + "(PAL10.price*PREF.curr) AS PS10, "
                StrSql = StrSql + "(PI.P1*PREF.curr) AS PP1, "
                StrSql = StrSql + "(PI.P2*PREF.curr) AS PP2, "
                StrSql = StrSql + "(PI.P3*PREF.curr) AS PP3, "
                StrSql = StrSql + "(PI.P4*PREF.curr) AS PP4, "
                StrSql = StrSql + "(PI.P5*PREF.curr) AS PP5, "
                StrSql = StrSql + "(PI.P6*PREF.curr) AS PP6, "
                StrSql = StrSql + "(PI.P7*PREF.curr) AS PP7, "
                StrSql = StrSql + "(PI.P8*PREF.curr) AS PP8, "
                StrSql = StrSql + "(PI.P9*PREF.curr) AS PP9, "
                StrSql = StrSql + "(PI.P10*PREF.curr) AS PP10, "
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
                StrSql = StrSql + "INNER JOIN ECON.Pallet  PAL1 "
                StrSql = StrSql + "ON PAL1.palletid=PI.M1 "
                StrSql = StrSql + "INNER JOIN ECON.Pallet PAL2 "
                StrSql = StrSql + "ON PAL2.palletid=PI.M2 "
                StrSql = StrSql + "INNER JOIN ECON.Pallet PAL3 "
                StrSql = StrSql + "ON PAL3.palletid=PI.M3 "
                StrSql = StrSql + "INNER JOIN ECON.Pallet PAL4 "
                StrSql = StrSql + "ON PAL4.palletid=PI.M4 "
                StrSql = StrSql + "INNER JOIN ECON.Pallet PAL5 "
                StrSql = StrSql + "ON PAL5.palletid=PI.M5 "
                StrSql = StrSql + "INNER JOIN ECON.Pallet PAL6 "
                StrSql = StrSql + "ON PAL6.palletid=PI.M6 "
                StrSql = StrSql + "INNER JOIN ECON.Pallet PAL7 "
                StrSql = StrSql + "ON PAL7.palletid=PI.M7 "
                StrSql = StrSql + "INNER JOIN ECON.Pallet PAL8 "
                StrSql = StrSql + "ON PAL8.palletid=PI.M8 "
                StrSql = StrSql + "INNER JOIN ECON.Pallet PAL9 "
                StrSql = StrSql + "ON PAL9.palletid=PI.M9 "
                StrSql = StrSql + "INNER JOIN ECON.Pallet PAL10 "
                StrSql = StrSql + "ON PAL10.palletid=PI.M10 "
                StrSql = StrSql + "WHERE PI.CASEID = " + CaseId.ToString() + ""
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetPalletInDetails:" + ex.Message.ToString())
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
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetAdditionalEnergyInfo:" + ex.Message.ToString())
                Return Dts

            End Try
        End Function

#End Region

#Region "Intermediate Results"
        Public Function GetExtrusionOutDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "MAT.CASEID, "
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
                StrSql = StrSql + "(MATOUT.PURZ1*PREF.CURR)AS PURZ1, "
                StrSql = StrSql + "(MATOUT.PURZ2*PREF.CURR)AS PURZ2, "
                StrSql = StrSql + "(MATOUT.PURZ3*PREF.CURR)AS PURZ3, "
                StrSql = StrSql + "(MATOUT.PURZ4*PREF.CURR)AS PURZ4, "
                StrSql = StrSql + "(MATOUT.PURZ5*PREF.CURR)AS PURZ5, "
                StrSql = StrSql + "(MATOUT.PURZ6*PREF.CURR)AS PURZ6, "
                StrSql = StrSql + "(MATOUT.PURZ7*PREF.CURR)AS PURZ7, "
                StrSql = StrSql + "(MATOUT.PURZ8*PREF.CURR)AS PURZ8, "
                StrSql = StrSql + "(MATOUT.PURZ9*PREF.CURR)AS PURZ9, "
                StrSql = StrSql + "(MATOUT.PURZ10*PREF.CURR)AS PURZ10, "
                StrSql = StrSql + "TOTAL.SG AS TOTSG, "
                StrSql = StrSql + "(MATOUT.P1+ MATOUT.P2 + MATOUT.P3 + MATOUT.P4 + MATOUT.P5 + MATOUT.P6 + MATOUT.P7 + MATOUT.P8 + MATOUT.P9 + MATOUT.P10) AS TOTWEIGHT, "
                StrSql = StrSql + "(MATOUT.PUR1+ MATOUT.PUR2 + MATOUT.PUR3 + MATOUT.PUR4 + MATOUT.PUR5 + MATOUT.PUR6 + MATOUT.PUR7 + MATOUT.PUR8 + MATOUT.PUR9 + MATOUT.PUR10)*PREF.CONVWT AS TOTPUR, "
                StrSql = StrSql + "(MATOUT.PURZ1+ MATOUT.PURZ2 + MATOUT.PURZ3 + MATOUT.PURZ4 + MATOUT.PURZ5 + MATOUT.PURZ6 + MATOUT.PURZ7 + MATOUT.PURZ8 + MATOUT.PURZ9 + MATOUT.PURZ10)*PREF.CURR AS TOTPURZ, "
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
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=MAT.CASEID "
                StrSql = StrSql + "INNER JOIN MATERIALOUTPUT MATOUT "
                StrSql = StrSql + "ON MATOUT.CASEID = MAT.CASEID "
                StrSql = StrSql + "INNER JOIN TOTAL "
                StrSql = StrSql + "ON TOTAL.CASEID = MAT.CASEID "
                StrSql = StrSql + "WHERE MAT.CASEID = " + CaseId.ToString() + "  ORDER BY MAT.CASEID"
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetExtrusionOutDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetOpreationsOutDetails(ByVal CaseId As Integer) As DataSet
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
                StrSql = StrSql + "INNER JOIN PROCESS OPOUT1 "
                StrSql = StrSql + "ON OPOUT1.PROCID = OPOUT.M1 "
                StrSql = StrSql + "INNER JOIN PROCESS OPOUT2 "
                StrSql = StrSql + "ON OPOUT2.PROCID = OPOUT.M2 "
                StrSql = StrSql + "INNER JOIN PROCESS OPOUT3 "
                StrSql = StrSql + "ON OPOUT3.PROCID= OPOUT.M3 "
                StrSql = StrSql + "INNER JOIN PROCESS OPOUT4 "
                StrSql = StrSql + "ON OPOUT4.PROCID = OPOUT.M4 "
                StrSql = StrSql + "INNER JOIN PROCESS OPOUT5 "
                StrSql = StrSql + "ON OPOUT5.PROCID = OPOUT.M5 "
                StrSql = StrSql + "INNER JOIN PROCESS OPOUT6 "
                StrSql = StrSql + "ON OPOUT6.PROCID = OPOUT.M6 "
                StrSql = StrSql + "INNER JOIN PROCESS OPOUT7 "
                StrSql = StrSql + "ON OPOUT7.PROCID = OPOUT.M7 "
                StrSql = StrSql + "INNER JOIN PROCESS OPOUT8 "
                StrSql = StrSql + "ON OPOUT8.PROCID = OPOUT.M8 "
                StrSql = StrSql + "INNER JOIN PROCESS OPOUT9 "
                StrSql = StrSql + "ON OPOUT9.PROCID = OPOUT.M9 "
                StrSql = StrSql + "INNER JOIN PROCESS OPOUT10 "
                StrSql = StrSql + "ON OPOUT10.PROCID = OPOUT.M10 "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=OPOUT.CASEID "
                StrSql = StrSql + "INNER JOIN OPDEPVOL "
                StrSql = StrSql + "ON OPDEPVOL.CASEID = OPOUT.CASEID "
                StrSql = StrSql + "WHERE OPOUT.CASEID = " + CaseId.ToString() + " ORDER BY OPOUT.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetOpreationsOutDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPersonnelOutDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                'StrSql = "SELECT   (POS1.PERSDE1 ||' '||  POS1.PERSDE2) AS PERSDES1,  "
                'StrSql = StrSql + "(POS2.PERSDE1 ||' '||  POS2.PERSDE2) AS PERSDES2, "
                'StrSql = StrSql + "(POS3.PERSDE1 ||' '||  POS3.PERSDE2) AS PERSDES3, "
                'StrSql = StrSql + "(POS4.PERSDE1 ||' '||  POS4.PERSDE2) AS PERSDES4, "
                'StrSql = StrSql + "(POS5.PERSDE1 ||' '||  POS5.PERSDE2) AS PERSDES5, "
                'StrSql = StrSql + "(POS6.PERSDE1 ||' '||  POS6.PERSDE2) AS PERSDES6, "
                'StrSql = StrSql + "(POS7.PERSDE1 ||' '||  POS7.PERSDE2) AS PERSDES7, "
                'StrSql = StrSql + "(POS8.PERSDE1 ||' '||  POS8.PERSDE2) AS PERSDES8, "
                'StrSql = StrSql + "(POS9.PERSDE1 ||' '||  POS9.PERSDE2) AS PERSDES9, "
                'StrSql = StrSql + "(POS10.PERSDE1 ||' '||  POS10.PERSDE2) AS PERSDES10, "
                'StrSql = StrSql + "(POS11.PERSDE1 ||' '||  POS11.PERSDE2) AS PERSDES11, "
                'StrSql = StrSql + "(POS12.PERSDE1 ||' '||  POS12.PERSDE2) AS PERSDES12, "
                'StrSql = StrSql + "(POS13.PERSDE1 ||' '||  POS13.PERSDE2) AS PERSDES13, "
                'StrSql = StrSql + "(POS14.PERSDE1 ||' '||  POS14.PERSDE2) AS PERSDES14, "
                'StrSql = StrSql + "(POS15.PERSDE1 ||' '||  POS15.PERSDE2) AS PERSDES15, "
                'StrSql = StrSql + "(POS16.PERSDE1 ||' '||  POS16.PERSDE2) AS PERSDES16, "
                'StrSql = StrSql + "(POS17.PERSDE1 ||' '||  POS17.PERSDE2) AS PERSDES17, "
                'StrSql = StrSql + "(POS18.PERSDE1 ||' '||  POS18.PERSDE2) AS PERSDES18, "
                'StrSql = StrSql + "(POS19.PERSDE1 ||' '||  POS19.PERSDE2) AS PERSDES19, "
                'StrSql = StrSql + "(POS20.PERSDE1 ||' '||  POS20.PERSDE2) AS PERSDES20, "
                'StrSql = StrSql + "(POS21.PERSDE1 ||' '||  POS21.PERSDE2) AS PERSDES21, "
                'StrSql = StrSql + "(POS22.PERSDE1 ||' '||  POS22.PERSDE2) AS PERSDES22, "
                'StrSql = StrSql + "(POS23.PERSDE1 ||' '||  POS23.PERSDE2) AS PERSDES23, "
                'StrSql = StrSql + "(POS24.PERSDE1 ||' '||  POS24.PERSDE2) AS PERSDES24, "
                'StrSql = StrSql + "(POS25.PERSDE1 ||' '||  POS25.PERSDE2) AS PERSDES25, "
                'StrSql = StrSql + "(POS26.PERSDE1 ||' '||  POS26.PERSDE2) AS PERSDES26, "
                'StrSql = StrSql + "(POS27.PERSDE1 ||' '||  POS27.PERSDE2) AS PERSDES27, "
                'StrSql = StrSql + "(POS28.PERSDE1 ||' '||  POS28.PERSDE2) AS PERSDES28, "
                'StrSql = StrSql + "(POS29.PERSDE1 ||' '||  POS29.PERSDE2) AS PERSDES29, "
                'StrSql = StrSql + "(POS30.PERSDE1 ||' '||  POS30.PERSDE2) AS PERSDES30, "
                'StrSql = StrSql + "PNUM.M1  AS N1, "
                'StrSql = StrSql + "PNUM.M2  AS N2, "
                'StrSql = StrSql + "PNUM.M3  AS N3, "
                'StrSql = StrSql + "PNUM.M4  AS N4, "
                'StrSql = StrSql + "PNUM.M5  AS N5, "
                'StrSql = StrSql + "PNUM.M6  AS N6, "
                'StrSql = StrSql + "PNUM.M7  AS N7, "
                'StrSql = StrSql + "PNUM.M8  AS N8, "
                'StrSql = StrSql + "PNUM.M9  AS N9, "
                'StrSql = StrSql + "PNUM.M10  AS N10, "
                'StrSql = StrSql + "PNUM.M11  AS N11, "
                'StrSql = StrSql + "PNUM.M12  AS N12, "
                'StrSql = StrSql + "PNUM.M13  AS N13, "
                'StrSql = StrSql + "PNUM.M14  AS N14, "
                'StrSql = StrSql + "PNUM.M15  AS N15, "
                'StrSql = StrSql + "PNUM.M16  AS N16, "
                'StrSql = StrSql + "PNUM.M17  AS N17, "
                'StrSql = StrSql + "PNUM.M18  AS N18, "
                'StrSql = StrSql + "PNUM.M19  AS N19, "
                'StrSql = StrSql + "PNUM.M20  AS N20, "
                'StrSql = StrSql + "PNUM.M21  AS N21, "
                'StrSql = StrSql + "PNUM.M22  AS N22, "
                'StrSql = StrSql + "PNUM.M23  AS N23, "
                'StrSql = StrSql + "PNUM.M24  AS N24, "
                'StrSql = StrSql + "PNUM.M25  AS N25, "
                'StrSql = StrSql + "PNUM.M26  AS N26, "
                'StrSql = StrSql + "PNUM.M27  AS N27, "
                'StrSql = StrSql + "PNUM.M28  AS N28, "
                'StrSql = StrSql + "PNUM.M29  AS N29, "
                'StrSql = StrSql + "PNUM.M30  AS N30, "
                'StrSql = StrSql + "(PNUM.M1+PNUM.M2+PNUM.M3+PNUM.M4+PNUM.M5+PNUM.M6+PNUM.M7+PNUM.M8+PNUM.M9+PNUM.M10+PNUM.M11+PNUM.M12+PNUM.M13+PNUM.M14+PNUM.M15+PNUM.M16+PNUM.M17+PNUM.M18+PNUM.M19+PNUM.M20+PNUM.M21+PNUM.M22+PNUM.M23+PNUM.M24+PNUM.M25+PNUM.M26+PNUM.M27+PNUM.M28+PNUM.M29+PNUM.M30)N31, "
                'StrSql = StrSql + "(PAY.M1*PREF.CURR) AS P1, "
                'StrSql = StrSql + "(PAY.M2*PREF.CURR) AS P2, "
                'StrSql = StrSql + "(PAY.M3*PREF.CURR) AS P3, "
                'StrSql = StrSql + "(PAY.M4*PREF.CURR) AS P4, "
                'StrSql = StrSql + "(PAY.M5*PREF.CURR) AS P5, "
                'StrSql = StrSql + "(PAY.M6*PREF.CURR) AS P6, "
                'StrSql = StrSql + "(PAY.M7*PREF.CURR) AS P7, "
                'StrSql = StrSql + "(PAY.M8*PREF.CURR) AS P8, "
                'StrSql = StrSql + "(PAY.M9*PREF.CURR) AS P9, "
                'StrSql = StrSql + "(PAY.M10*PREF.CURR) AS P10, "
                'StrSql = StrSql + "(PAY.M11*PREF.CURR) AS P11, "
                'StrSql = StrSql + "(PAY.M12*PREF.CURR) AS P12, "
                'StrSql = StrSql + "(PAY.M13*PREF.CURR) AS P13, "
                'StrSql = StrSql + "(PAY.M14*PREF.CURR) AS P14, "
                'StrSql = StrSql + "(PAY.M15*PREF.CURR) AS P15, "
                'StrSql = StrSql + "(PAY.M16*PREF.CURR) AS P16, "
                'StrSql = StrSql + "(PAY.M17*PREF.CURR) AS P17, "
                'StrSql = StrSql + "(PAY.M18*PREF.CURR) AS P18, "
                'StrSql = StrSql + "(PAY.M19*PREF.CURR) AS P19, "
                'StrSql = StrSql + "(PAY.M20*PREF.CURR) AS P20, "
                'StrSql = StrSql + "(PAY.M21*PREF.CURR) AS P21, "
                'StrSql = StrSql + "(PAY.M22*PREF.CURR) AS P22, "
                'StrSql = StrSql + "(PAY.M23*PREF.CURR) AS P23, "
                'StrSql = StrSql + "(PAY.M24*PREF.CURR) AS P24, "
                'StrSql = StrSql + "(PAY.M25*PREF.CURR) AS P25, "
                'StrSql = StrSql + "(PAY.M26*PREF.CURR) AS P26, "
                'StrSql = StrSql + "(PAY.M27*PREF.CURR) AS P27, "
                'StrSql = StrSql + "(PAY.M28*PREF.CURR) AS P28, "
                'StrSql = StrSql + "(PAY.M29*PREF.CURR) AS P29, "
                'StrSql = StrSql + "(PAY.M30*PREF.CURR) AS P30, "
                'StrSql = StrSql + "((PAY.M1+PAY.M2+PAY.M3+PAY.M4+PAY.M5+PAY.M6+PAY.M7+PAY.M8+PAY.M9+PAY.M10+PAY.M11+PAY.M12+PAY.M13+PAY.M14+PAY.M15+PAY.M16+PAY.M17+PAY.M18+PAY.M19+PAY.M20+PAY.M21+PAY.M22+PAY.M23+PAY.M24+PAY.M25+PAY.M26+PAY.M27+PAY.M28+PAY.M29+PAY.M30)*PREF.CURR)P31, "
                'StrSql = StrSql + "PREF.TITLE1, "
                'StrSql = StrSql + "PREF.TITLE3, "
                'StrSql = StrSql + "PREF.TITLE2, "
                'StrSql = StrSql + "PREF.TITLE4, "
                'StrSql = StrSql + "PREF.TITLE5, "
                'StrSql = StrSql + "PREF.TITLE6, "
                'StrSql = StrSql + "PREF.TITLE7, "
                'StrSql = StrSql + "PREF.TITLE8, "
                'StrSql = StrSql + "PREF.TITLE9, "
                'StrSql = StrSql + "PREF.TITLE10, "
                'StrSql = StrSql + "PREF.TITLE11, "
                'StrSql = StrSql + "PREF.TITLE12, "
                'StrSql = StrSql + "PREF.UNITS "
                'StrSql = StrSql + "FROM PERSONNELPOS POS "
                'StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                'StrSql = StrSql + "ON PREF.CASEID = POS.CASEID "
                'StrSql = StrSql + "INNER JOIN PERSONNELNUM PNUM "
                'StrSql = StrSql + "ON PNUM.CASEID = POS.CASEID "
                'StrSql = StrSql + "INNER JOIN PersonnelPAY PAY "
                'StrSql = StrSql + "ON PAY.CASEID = POS.CASEID "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS1 "
                'StrSql = StrSql + "ON POS1.PERSID=POS.M1 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS2 "
                'StrSql = StrSql + "ON POS2.PERSID=POS.M2 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS3 "
                'StrSql = StrSql + "ON POS3.PERSID=POS.M3 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS4 "
                'StrSql = StrSql + "ON POS4.PERSID=POS.M4 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS5 "
                'StrSql = StrSql + "ON POS5.PERSID=POS.M5 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS6 "
                'StrSql = StrSql + "ON POS6.PERSID=POS.M6 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS7 "
                'StrSql = StrSql + "ON POS7.PERSID=POS.M7 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS8 "
                'StrSql = StrSql + "ON POS8.PERSID=POS.M8 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS9 "
                'StrSql = StrSql + "ON POS9.PERSID=POS.M9 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS10 "
                'StrSql = StrSql + "ON POS10.PERSID=POS.M10 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS11 "
                'StrSql = StrSql + "ON POS11.PERSID=POS.M11 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS12 "
                'StrSql = StrSql + "ON POS12.PERSID=POS.M12 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS13 "
                'StrSql = StrSql + "ON POS13.PERSID=POS.M13 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS14 "
                'StrSql = StrSql + "ON POS14.PERSID=POS.M14 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS15 "
                'StrSql = StrSql + "ON POS15.PERSID=POS.M15 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS16 "
                'StrSql = StrSql + "ON POS16.PERSID=POS.M16 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS17 "
                'StrSql = StrSql + "ON POS17.PERSID=POS.M17 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS18 "
                'StrSql = StrSql + "ON POS18.PERSID=POS.M18 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS19 "
                'StrSql = StrSql + "ON POS19.PERSID=POS.M19 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS20 "
                'StrSql = StrSql + "ON POS20.PERSID=POS.M20 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS21 "
                'StrSql = StrSql + "ON POS21.PERSID=POS.M21 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS22 "
                'StrSql = StrSql + "ON POS22.PERSID=POS.M22 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS23 "
                'StrSql = StrSql + "ON POS23.PERSID=POS.M23 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS24 "
                'StrSql = StrSql + "ON POS24.PERSID=POS.M24 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS25 "
                'StrSql = StrSql + "ON POS25.PERSID=POS.M25 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS26 "
                'StrSql = StrSql + "ON POS26.PERSID=POS.M26 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS27 "
                'StrSql = StrSql + "ON POS27.PERSID=POS.M27 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS28 "
                'StrSql = StrSql + "ON POS28.PERSID=POS.M28 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS29 "
                'StrSql = StrSql + "ON POS29.PERSID=POS.M29 "
                'StrSql = StrSql + "INNER JOIN ECon.personnel  POS30 "
                'StrSql = StrSql + "ON POS30.PERSID=POS.M30 "
                'StrSql = StrSql + "WHERE POS.CASEID =" + CaseId.ToString() + ""

                StrSql = "SELECT  "
                StrSql = StrSql + "PERSDES1,PERSDES2,PERSDES3,PERSDES4,PERSDES5,PERSDES6,PERSDES7,PERSDES8,PERSDES9,PERSDES10, "
                StrSql = StrSql + "PERSDES11,PERSDES12,PERSDES13,PERSDES14,PERSDES15,PERSDES16,PERSDES17,PERSDES18,PERSDES19,PERSDES20, "
                StrSql = StrSql + "PERSDES21,PERSDES22,PERSDES23,PERSDES24,PERSDES25,PERSDES26,PERSDES27,PERSDES28,PERSDES29,PERSDES30, "
                StrSql = StrSql + "N1,N2,N3,N4,N5,N6,N7,N8,N9,N10, "
                StrSql = StrSql + "N11,N12,N13,N14,N15,N16,N17,N18,N19,N20, "
                StrSql = StrSql + "N21,N22,N23,N24,N25,N26,N27,N28,N29,N30, "
                StrSql = StrSql + "(N1+N2+N3+N4+N5+N6+N7+N8+N9+N10+N11+N12+N13+N14+N15+N16+N17+N18+N19+N20+N21+N22+N23+N24+N25+N26+N27+N28+N29+N30)N31, "
                StrSql = StrSql + "P1,P2,P3,P4,P5,P6,P7,P8,P9,P10, "
                StrSql = StrSql + "P11,P12,P13,P14,P15,P16,P17,P18,P19,P20, "
                StrSql = StrSql + "P21,P22,P23,P24,P25,P26,P27,P28,P29,P30, "
                StrSql = StrSql + "P31,TITLE1,TITLE3,TITLE2,TITLE4,TITLE5,TITLE6,TITLE7,TITLE8,TITLE9,TITLE10,TITLE11,TITLE12,UNITS "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT   (POS1.PERSDE1 ||' '||  POS1.PERSDE2) AS PERSDES1, "
                StrSql = StrSql + "(POS2.PERSDE1 ||' '||  POS2.PERSDE2) AS PERSDES2, "
                StrSql = StrSql + "(POS3.PERSDE1 ||' '||  POS3.PERSDE2) AS PERSDES3, "
                StrSql = StrSql + "(POS4.PERSDE1 ||' '||  POS4.PERSDE2) AS PERSDES4, "
                StrSql = StrSql + "(POS5.PERSDE1 ||' '||  POS5.PERSDE2) AS PERSDES5, "
                StrSql = StrSql + "(POS6.PERSDE1 ||' '||  POS6.PERSDE2) AS PERSDES6, "
                StrSql = StrSql + "(POS7.PERSDE1 ||' '||  POS7.PERSDE2) AS PERSDES7, "
                StrSql = StrSql + "(POS8.PERSDE1 ||' '||  POS8.PERSDE2) AS PERSDES8, "
                StrSql = StrSql + "(POS9.PERSDE1 ||' '||  POS9.PERSDE2) AS PERSDES9, "
                StrSql = StrSql + "(POS10.PERSDE1 ||' '||  POS10.PERSDE2) AS PERSDES10, "
                StrSql = StrSql + "(POS11.PERSDE1 ||' '||  POS11.PERSDE2) AS PERSDES11, "
                StrSql = StrSql + "(POS12.PERSDE1 ||' '||  POS12.PERSDE2) AS PERSDES12, "
                StrSql = StrSql + "(POS13.PERSDE1 ||' '||  POS13.PERSDE2) AS PERSDES13, "
                StrSql = StrSql + "(POS14.PERSDE1 ||' '||  POS14.PERSDE2) AS PERSDES14, "
                StrSql = StrSql + "(POS15.PERSDE1 ||' '||  POS15.PERSDE2) AS PERSDES15, "
                StrSql = StrSql + "(POS16.PERSDE1 ||' '||  POS16.PERSDE2) AS PERSDES16, "
                StrSql = StrSql + "(POS17.PERSDE1 ||' '||  POS17.PERSDE2) AS PERSDES17, "
                StrSql = StrSql + "(POS18.PERSDE1 ||' '||  POS18.PERSDE2) AS PERSDES18, "
                StrSql = StrSql + "(POS19.PERSDE1 ||' '||  POS19.PERSDE2) AS PERSDES19, "
                StrSql = StrSql + "(POS20.PERSDE1 ||' '||  POS20.PERSDE2) AS PERSDES20, "
                StrSql = StrSql + "(POS21.PERSDE1 ||' '||  POS21.PERSDE2) AS PERSDES21, "
                StrSql = StrSql + "(POS22.PERSDE1 ||' '||  POS22.PERSDE2) AS PERSDES22, "
                StrSql = StrSql + "(POS23.PERSDE1 ||' '||  POS23.PERSDE2) AS PERSDES23, "
                StrSql = StrSql + "(POS24.PERSDE1 ||' '||  POS24.PERSDE2) AS PERSDES24, "
                StrSql = StrSql + "(POS25.PERSDE1 ||' '||  POS25.PERSDE2) AS PERSDES25, "
                StrSql = StrSql + "(POS26.PERSDE1 ||' '||  POS26.PERSDE2) AS PERSDES26, "
                StrSql = StrSql + "(POS27.PERSDE1 ||' '||  POS27.PERSDE2) AS PERSDES27, "
                StrSql = StrSql + "(POS28.PERSDE1 ||' '||  POS28.PERSDE2) AS PERSDES28, "
                StrSql = StrSql + "(POS29.PERSDE1 ||' '||  POS29.PERSDE2) AS PERSDES29, "
                StrSql = StrSql + "(POS30.PERSDE1 ||' '||  POS30.PERSDE2) AS PERSDES30, "
                StrSql = StrSql + "CASE WHEN POS.M1>0 THEN PNUM.M1 ELSE 0 END AS N1, "
                StrSql = StrSql + "CASE WHEN POS.M2>0 THEN PNUM.M2  ELSE 0 END AS N2, "
                StrSql = StrSql + "CASE WHEN POS.M3>0 THEN PNUM.M3  ELSE 0 END AS N3, "
                StrSql = StrSql + "CASE WHEN POS.M4>0 THEN PNUM.M4  ELSE 0 END AS N4, "
                StrSql = StrSql + "CASE WHEN POS.M5>0 THEN PNUM.M5  ELSE 0 END AS N5, "
                StrSql = StrSql + "CASE WHEN POS.M6>0 THEN PNUM.M6  ELSE 0 END AS N6, "
                StrSql = StrSql + "CASE WHEN POS.M7>0 THEN PNUM.M7  ELSE 0 END AS N7, "
                StrSql = StrSql + "CASE WHEN POS.M8>0 THEN PNUM.M8  ELSE 0 END AS N8, "
                StrSql = StrSql + "CASE WHEN POS.M9>0 THEN PNUM.M9  ELSE 0 END AS N9, "
                StrSql = StrSql + "CASE WHEN POS.M10>0 THEN PNUM.M10  ELSE 0 END AS N10, "
                StrSql = StrSql + "CASE WHEN POS.M11>0 THEN PNUM.M11  ELSE 0 END AS N11, "
                StrSql = StrSql + "CASE WHEN POS.M12>0 THEN PNUM.M12  ELSE 0 END AS N12, "
                StrSql = StrSql + "CASE WHEN POS.M13>0 THEN PNUM.M13  ELSE 0 END AS N13, "
                StrSql = StrSql + "CASE WHEN POS.M14>0 THEN PNUM.M14  ELSE 0 END AS N14, "
                StrSql = StrSql + "CASE WHEN POS.M15>0 THEN PNUM.M15  ELSE 0 END AS N15, "
                StrSql = StrSql + "CASE WHEN POS.M16>0 THEN PNUM.M16  ELSE 0 END AS N16, "
                StrSql = StrSql + "CASE WHEN POS.M17>0 THEN PNUM.M17  ELSE 0 END AS N17, "
                StrSql = StrSql + "CASE WHEN POS.M18>0 THEN PNUM.M18  ELSE 0 END AS N18, "
                StrSql = StrSql + "CASE WHEN POS.M19>0 THEN PNUM.M19  ELSE 0 END AS N19, "
                StrSql = StrSql + "CASE WHEN POS.M20>0 THEN PNUM.M20  ELSE 0 END AS N20, "
                StrSql = StrSql + "CASE WHEN POS.M21>0 THEN PNUM.M21  ELSE 0 END AS N21, "
                StrSql = StrSql + "CASE WHEN POS.M22>0 THEN PNUM.M22  ELSE 0 END AS N22, "
                StrSql = StrSql + "CASE WHEN POS.M23>0 THEN PNUM.M23  ELSE 0 END AS N23, "
                StrSql = StrSql + "CASE WHEN POS.M24>0 THEN PNUM.M24  ELSE 0 END AS N24, "
                StrSql = StrSql + "CASE WHEN POS.M25>0 THEN PNUM.M25  ELSE 0 END AS N25, "
                StrSql = StrSql + "CASE WHEN POS.M26>0 THEN PNUM.M26  ELSE 0 END AS N26, "
                StrSql = StrSql + "CASE WHEN POS.M27>0 THEN PNUM.M27 ELSE 0 END AS N27, "
                StrSql = StrSql + "CASE WHEN POS.M28>0 THEN PNUM.M28 ELSE 0 END AS N28, "
                StrSql = StrSql + "CASE WHEN POS.M29>0 THEN PNUM.M29 ELSE 0 END AS N29, "
                StrSql = StrSql + "CASE WHEN POS.M30>0 THEN PNUM.M30 ELSE 0 END AS N30, "
                StrSql = StrSql + "(PAY.M1*PREF.CURR) AS P1, "
                StrSql = StrSql + "(PAY.M2*PREF.CURR) AS P2, "
                StrSql = StrSql + "(PAY.M3*PREF.CURR) AS P3, "
                StrSql = StrSql + "(PAY.M4*PREF.CURR) AS P4, "
                StrSql = StrSql + "(PAY.M5*PREF.CURR) AS P5, "
                StrSql = StrSql + "(PAY.M6*PREF.CURR) AS P6, "
                StrSql = StrSql + "(PAY.M7*PREF.CURR) AS P7, "
                StrSql = StrSql + "(PAY.M8*PREF.CURR) AS P8, "
                StrSql = StrSql + "(PAY.M9*PREF.CURR) AS P9, "
                StrSql = StrSql + "(PAY.M10*PREF.CURR) AS P10, "
                StrSql = StrSql + "(PAY.M11*PREF.CURR) AS P11, "
                StrSql = StrSql + "(PAY.M12*PREF.CURR) AS P12, "
                StrSql = StrSql + "(PAY.M13*PREF.CURR) AS P13, "
                StrSql = StrSql + "(PAY.M14*PREF.CURR) AS P14, "
                StrSql = StrSql + "(PAY.M15*PREF.CURR) AS P15, "
                StrSql = StrSql + "(PAY.M16*PREF.CURR) AS P16, "
                StrSql = StrSql + "(PAY.M17*PREF.CURR) AS P17, "
                StrSql = StrSql + "(PAY.M18*PREF.CURR) AS P18, "
                StrSql = StrSql + "(PAY.M19*PREF.CURR) AS P19, "
                StrSql = StrSql + "(PAY.M20*PREF.CURR) AS P20, "
                StrSql = StrSql + "(PAY.M21*PREF.CURR) AS P21, "
                StrSql = StrSql + "(PAY.M22*PREF.CURR) AS P22, "
                StrSql = StrSql + "(PAY.M23*PREF.CURR) AS P23, "
                StrSql = StrSql + "(PAY.M24*PREF.CURR) AS P24, "
                StrSql = StrSql + "(PAY.M25*PREF.CURR) AS P25, "
                StrSql = StrSql + "(PAY.M26*PREF.CURR) AS P26, "
                StrSql = StrSql + "(PAY.M27*PREF.CURR) AS P27, "
                StrSql = StrSql + "(PAY.M28*PREF.CURR) AS P28, "
                StrSql = StrSql + "(PAY.M29*PREF.CURR) AS P29, "
                StrSql = StrSql + "(PAY.M30*PREF.CURR) AS P30, "
                StrSql = StrSql + "((PAY.M1+PAY.M2+PAY.M3+PAY.M4+PAY.M5+PAY.M6+PAY.M7+PAY.M8+PAY.M9+PAY.M10+PAY.M11+PAY.M12+PAY.M13+PAY.M14+PAY.M15+PAY.M16+PAY.M17+PAY.M18+PAY.M19+PAY.M20+PAY.M21+PAY.M22+PAY.M23+PAY.M24+PAY.M25+PAY.M26+PAY.M27+PAY.M28+PAY.M29+PAY.M30)*PREF.CURR)P31, "
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
                StrSql = StrSql + "FROM PERSONNELPOS POS "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = POS.CASEID "
                StrSql = StrSql + "INNER JOIN PERSONNELNUM PNUM "
                StrSql = StrSql + "ON PNUM.CASEID = POS.CASEID "
                StrSql = StrSql + "INNER JOIN PersonnelPAY PAY "
                StrSql = StrSql + "ON PAY.CASEID = POS.CASEID "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS1 "
                StrSql = StrSql + "ON POS1.PERSID=POS.M1 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS2 "
                StrSql = StrSql + "ON POS2.PERSID=POS.M2 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS3 "
                StrSql = StrSql + "ON POS3.PERSID=POS.M3 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS4 "
                StrSql = StrSql + "ON POS4.PERSID=POS.M4 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS5 "
                StrSql = StrSql + "ON POS5.PERSID=POS.M5 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS6 "
                StrSql = StrSql + "ON POS6.PERSID=POS.M6 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS7 "
                StrSql = StrSql + "ON POS7.PERSID=POS.M7 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS8 "
                StrSql = StrSql + "ON POS8.PERSID=POS.M8 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS9 "
                StrSql = StrSql + "ON POS9.PERSID=POS.M9 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS10 "
                StrSql = StrSql + "ON POS10.PERSID=POS.M10 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS11 "
                StrSql = StrSql + "ON POS11.PERSID=POS.M11 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS12 "
                StrSql = StrSql + "ON POS12.PERSID=POS.M12 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS13 "
                StrSql = StrSql + "ON POS13.PERSID=POS.M13 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS14 "
                StrSql = StrSql + "ON POS14.PERSID=POS.M14 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS15 "
                StrSql = StrSql + "ON POS15.PERSID=POS.M15 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS16 "
                StrSql = StrSql + "ON POS16.PERSID=POS.M16 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS17 "
                StrSql = StrSql + "ON POS17.PERSID=POS.M17 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS18 "
                StrSql = StrSql + "ON POS18.PERSID=POS.M18 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS19 "
                StrSql = StrSql + "ON POS19.PERSID=POS.M19 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS20 "
                StrSql = StrSql + "ON POS20.PERSID=POS.M20 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS21 "
                StrSql = StrSql + "ON POS21.PERSID=POS.M21 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS22 "
                StrSql = StrSql + "ON POS22.PERSID=POS.M22 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS23 "
                StrSql = StrSql + "ON POS23.PERSID=POS.M23 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS24 "
                StrSql = StrSql + "ON POS24.PERSID=POS.M24 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS25 "
                StrSql = StrSql + "ON POS25.PERSID=POS.M25 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS26 "
                StrSql = StrSql + "ON POS26.PERSID=POS.M26 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS27 "
                StrSql = StrSql + "ON POS27.PERSID=POS.M27 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS28 "
                StrSql = StrSql + "ON POS28.PERSID=POS.M28 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS29 "
                StrSql = StrSql + "ON POS29.PERSID=POS.M29 "
                StrSql = StrSql + "INNER JOIN ECon.personnel  POS30 "
                StrSql = StrSql + "ON POS30.PERSID=POS.M30 "
                StrSql = StrSql + "WHERE POS.CASEID = " + CaseId.ToString() + " "
                StrSql = StrSql + ") DUAL "

                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetPersonnelOutDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Results"
        Public Function GetProfitAndLossDetails(ByVal CaseId As String, ByVal Isdep As Boolean) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty

            Try
                StrSql = "SELECT  RSPL.CASEID ,  "
                StrSql = StrSql + "'Revenue' AS DES1, "
                StrSql = StrSql + "'Materials' AS DES2, "
                StrSql = StrSql + "'Labor' AS DES3, "
                StrSql = StrSql + "'Energy' AS DES4, "
                StrSql = StrSql + "'Distribution Packaging' AS DES5, "
                StrSql = StrSql + "'Shipping to Customer' AS DES6, "
                StrSql = StrSql + "'Variable Margin' AS DES7, "
                StrSql = StrSql + "'Office Supplies' AS DES8, "
                StrSql = StrSql + "'Labor' AS DES9, "
                StrSql = StrSql + "'Energy' AS DES10, "
                StrSql = StrSql + "'Lease Cost' AS DES11, "
                StrSql = StrSql + "'Insurance' AS DES12, "
                StrSql = StrSql + "'Utilities' AS DES13, "
                StrSql = StrSql + "'Communications' AS DES14, "
                StrSql = StrSql + "'Travel' AS DES15, "
                StrSql = StrSql + "'Maintenance Supplies' AS DES16, "
                StrSql = StrSql + "'Minor Equipment' AS DES17, "
                StrSql = StrSql + "'Outside Services'  AS DES18, "
                StrSql = StrSql + "'Professional Services' AS DES19, "
                StrSql = StrSql + "'Laboratory Supplies' AS DES20, "
                StrSql = StrSql + "'Ink Supplies' AS DES21, "
                StrSql = StrSql + "'Plate Supplies' AS DES22, "
                StrSql = StrSql + "'Metal Supplies' AS DES23, "
                StrSql = StrSql + "'Plant Margin' AS DES24, "
                StrSql = StrSql + "(CASE WHEN RSPL.PVOLUSE=0 THEN RSPL.VOLUME*PREF.CONVWT "
                StrSql = StrSql + "ELSE RSPL.SVOLUME*PREF.CONVWT END) AS SALESVOLUMELB, "
                StrSql = StrSql + "NVL((CASE WHEN RSPL.FINVOLMUNITS>0 THEN RSPL.FINVOLMUNITS END),0) AS SALESVOLUMEUNIT, "

                StrSql = StrSql + " CASE WHEN CUSSALESUNIT=0 THEN RSPL.CUSSALESVOLUME*PREF.CONVWT  "
                StrSql = StrSql + " ELSE NVL((CASE  WHEN RSPL.FINVOLMUNITS>0  THEN  RSPL.CUSSALESVOLUME END),0) "
                StrSql = StrSql + " END AS CUSSALESVOLUME, "
                StrSql = StrSql + " CUSSALESVOLUME AS  CUSSALESVOLUME1, "
                StrSql = StrSql + " CUSSALESUNIT, "

                StrSql = StrSql + "'Sales Volume ('|| PREF.TITLE8||')' AS VOLUNI1, "

                StrSql = StrSql + "(CASE WHEN RSPL.FINVOLMUNITS > 0 THEN 'Sales Volume (units)' END) VOLUNI2, "
                StrSql = StrSql + "RSPL.REVENUE AS PL1, "
                StrSql = StrSql + "RSPL.VMATERIAL AS PL2 , "
                StrSql = StrSql + "RSPL.VLABOR AS PL3, "
                StrSql = StrSql + "RSPL.VENERGY AS PL4, "
                StrSql = StrSql + "RSPL.VPACK AS PL5, "
                StrSql = StrSql + "RSPL.VSHIP AS PL6, "
                StrSql = StrSql + "RSPL.VM AS PL7, "
                StrSql = StrSql + "RSPL.OFFICESUPPLIES AS PL8, "
                StrSql = StrSql + "RSPL.PLABOR AS PL9, "
                StrSql = StrSql + "RSPL.PENERGY AS PL10, "
                StrSql = StrSql + "RSPL.LEASECOST AS PL11, "
                StrSql = StrSql + "RSPL.INSURANCE AS PL12, "
                StrSql = StrSql + "RSPL.UTILITIES AS PL13, "
                StrSql = StrSql + "RSPL.COMMUN AS PL14, "
                StrSql = StrSql + "RSPL.TRAVEL AS PL15, "
                StrSql = StrSql + "RSPL.MAINT AS PL16, "
                StrSql = StrSql + "RSPL.MINOR AS PL17, "
                StrSql = StrSql + "RSPL.OUT  AS PL18, "
                StrSql = StrSql + "RSPL.PROF AS PL19, "
                StrSql = StrSql + "RSPL.LAB AS PL20, "
                StrSql = StrSql + "RSPL.INKSUP AS PL21, "
                StrSql = StrSql + "RSPL.PLATESUP AS PL22, "
                StrSql = StrSql + "RSPL.metsup AS PL23, "
                StrSql = StrSql + "RSPL.pm AS PL24, "
                StrSql = StrSql + "RSPL.finvolmsi, "
                StrSql = StrSql + "RSPL.finvolmunits, "
                StrSql = StrSql + "RSPL.unitprice, "
                StrSql = StrSql + "RSPL.volume AS PVOLUME, "
                StrSql = StrSql + "CUSTOMERIN.M1*PREF.CURR/PREF.CONVWT AS SPRICE, "
                StrSql = StrSql + "RSPL.UNITTYPE, "
                StrSql = StrSql + "PREF.CURR, "
                StrSql = StrSql + "PREF.CONVAREA, "
                StrSql = StrSql + "PREF.CONVWT, "
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
                StrSql = StrSql + "FROM RESULTSPL RSPL "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=RSPL.CASEID "
                StrSql = StrSql + "INNER JOIN CUSTOMERIN "
                StrSql = StrSql + "ON CUSTOMERIN.CASEID=RSPL.CASEID "
                StrSql = StrSql + "WHERE RSPL.CASEID IN (" + CaseId.ToString() + ") "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetProfitAndLossDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetProfitAndLossDetailsWithDep(ByVal CaseId As String, ByVal Isdep As Boolean) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty

            Try
                StrSql = "SELECT        RSPL.CASEID ,  "
                StrSql = StrSql + "'Revenue' AS DES1, "
                StrSql = StrSql + "'Materials' AS DES2, "
                StrSql = StrSql + "'Labor' AS DES3, "
                StrSql = StrSql + "'Energy' AS DES4, "
                StrSql = StrSql + "'Distribution Packaging' AS DES5, "
                StrSql = StrSql + "'Shipping to Customer' AS DES6, "
                StrSql = StrSql + "'Variable Margin' AS DES7, "
                StrSql = StrSql + "'Office Supplies' AS DES8, "
                StrSql = StrSql + "'Labor' AS DES9, "
                StrSql = StrSql + "'Energy' AS DES10, "
                StrSql = StrSql + "'Lease Cost' AS DES11, "
                StrSql = StrSql + "'Insurance' AS DES12, "
                StrSql = StrSql + "'Utilities' AS DES13, "
                StrSql = StrSql + "'Communications' AS DES14, "
                StrSql = StrSql + "'Travel' AS DES15, "
                StrSql = StrSql + "'Maintenance Supplies' AS DES16, "
                StrSql = StrSql + "'Minor Equipment' AS DES17, "
                StrSql = StrSql + "'Outside Services'  AS DES18, "
                StrSql = StrSql + "'Professional Services' AS DES19, "
                StrSql = StrSql + "'Laboratory Supplies' AS DES20, "
                StrSql = StrSql + "'Ink Supplies' AS DES21, "
                StrSql = StrSql + "'Plate Supplies' AS DES22, "
                StrSql = StrSql + "'Metal Supplies' AS DES23, "
                StrSql = StrSql + "'Depreciation' AS DES24 , "
                StrSql = StrSql + "'Plant Margin' AS DES25, "
                StrSql = StrSql + "(CASE WHEN RSPL.PVOLUSE=0 THEN RSPL.VOLUME*PREF.CONVWT "
                StrSql = StrSql + "ELSE RSPL.SVOLUME*PREF.CONVWT END) AS SALESVOLUMELB, "
                StrSql = StrSql + "NVL((CASE WHEN RSPL.FINVOLMUNITS>0 THEN RSPL.FINVOLMUNITS END),0) SALESVOLUMEUNIT, "
                'ADDED FOR THE BUG#326
                StrSql = StrSql + " CASE WHEN CUSSALESUNIT=0 THEN RSPL.CUSSALESVOLUME*PREF.CONVWT  "
                StrSql = StrSql + " ELSE NVL((CASE  WHEN RSPL.FINVOLMUNITS>0  THEN  RSPL.CUSSALESVOLUME END),0) "
                StrSql = StrSql + " END AS CUSSALESVOLUME, "
                StrSql = StrSql + " CUSSALESVOLUME AS  CUSSALESVOLUME1, "
                StrSql = StrSql + " CUSSALESUNIT, "
                'END FOR THE BUG#326
                StrSql = StrSql + "'Sales Volume ('|| PREF.TITLE8||')' AS VOLUNI1, "
                StrSql = StrSql + "(CASE WHEN RSPL.FINVOLMUNITS > 0 THEN 'Sales Volume (units)' END) VOLUNI2, "

                StrSql = StrSql + "RSPL.REVENUE AS PL1, "
                StrSql = StrSql + "RSPL.VMATERIAL AS PL2 , "
                StrSql = StrSql + "RSPL.VLABOR AS PL3, "
                StrSql = StrSql + "RSPL.VENERGY AS PL4, "
                StrSql = StrSql + "RSPL.VPACK AS PL5, "
                StrSql = StrSql + "RSPL.VSHIP AS PL6, "
                StrSql = StrSql + "RSPL.VM AS PL7, "
                StrSql = StrSql + "RSPL.OFFICESUPPLIES AS PL8, "
                StrSql = StrSql + "RSPL.PLABOR AS PL9, "
                StrSql = StrSql + "RSPL.PENERGY AS PL10, "
                StrSql = StrSql + "RSPL.LEASECOST AS PL11, "
                StrSql = StrSql + "RSPL.INSURANCE AS PL12, "
                StrSql = StrSql + "RSPL.UTILITIES AS PL13, "
                StrSql = StrSql + "RSPL.COMMUN AS PL14, "
                StrSql = StrSql + "RSPL.TRAVEL AS PL15, "
                StrSql = StrSql + "RSPL.MAINT AS PL16, "
                StrSql = StrSql + "RSPL.MINOR AS PL17, "
                StrSql = StrSql + "RSPL.OUT  AS PL18, "
                StrSql = StrSql + "RSPL.PROF AS PL19, "
                StrSql = StrSql + "RSPL.LAB AS PL20, "
                StrSql = StrSql + "RSPL.INKSUP AS PL21, "
                StrSql = StrSql + "RSPL.PLATESUP AS PL22, "
                StrSql = StrSql + "RSPL.METSUP AS PL23, "
                StrSql = StrSql + "DEPRECIATION.DEPRECIATION AS PL24, "
                StrSql = StrSql + "RSPL.PMDEP AS PL25, "
                StrSql = StrSql + "RSPL.FINVOLMSI, "
                StrSql = StrSql + "RSPL.FINVOLMUNITS, "
                StrSql = StrSql + "RSPL.UNITPRICE, "
                StrSql = StrSql + "CUSTOMERIN.M1*PREF.CURR/PREF.CONVWT AS SPRICE, "
                StrSql = StrSql + "RSPL.UNITTYPE, "
                StrSql = StrSql + "PREF.CURR, "
                StrSql = StrSql + "PREF.CONVAREA, "
                StrSql = StrSql + "PREF.CONVWT, "
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
                StrSql = StrSql + "FROM RESULTSPL RSPL "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=RSPL.CASEID "
                StrSql = StrSql + "INNER JOIN DEPRECIATION "
                StrSql = StrSql + "ON DEPRECIATION.CASEID=RSPL.CASEID "
                StrSql = StrSql + "INNER JOIN CUSTOMERIN "
                StrSql = StrSql + "ON CUSTOMERIN.CASEID=RSPL.CASEID "
                StrSql = StrSql + "WHERE RSPL.CASEID IN (" + CaseId.ToString() + ") "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetProfitAndLossDetailsWithDep:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCostDetails(ByVal CaseId As String, ByVal Isdep As Boolean) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty

            Try
                StrSql = "SELECT        RSPL.CASEID ,  "
                StrSql = StrSql + "'Material Cost' AS DES1, "
                StrSql = StrSql + "'Labor Cost' AS DES2, "
                StrSql = StrSql + "'Energy Cost' AS DES3, "
                StrSql = StrSql + "'Distribution Packaging Cost' AS DES4, "
                StrSql = StrSql + "'Shipping Cost' AS DES5, "
                StrSql = StrSql + "'Total Variable Cost' AS DES6, "
                StrSql = StrSql + "'Office Supplies Cost' AS DES7, "
                StrSql = StrSql + "'Labor Cost' AS DES8, "
                StrSql = StrSql + "'Energy Cost' AS DES9, "
                StrSql = StrSql + "'Lease Cost' AS DES10, "
                StrSql = StrSql + "'Insurance Cost' AS DES11, "
                StrSql = StrSql + "'Utilities Cost' AS DES12, "
                StrSql = StrSql + "'Communications Cost' AS DES13, "
                StrSql = StrSql + "'Travel Cost' AS DES14, "
                StrSql = StrSql + "'Maintenance Supplies Cost' AS DES15, "
                StrSql = StrSql + "'Minor Equipment Cost' AS DES16, "
                StrSql = StrSql + "'Outside Services Cost' AS DES17, "
                StrSql = StrSql + "'Professional Services Cost'  AS DES18, "
                StrSql = StrSql + "'Laboratory Supplies Cost' AS DES19, "
                StrSql = StrSql + "'Ink Supplies Cost' AS DES20, "
                StrSql = StrSql + "'Plate Supplies Cost' AS DES21, "
                StrSql = StrSql + "'Metal Supplies Cost' AS DES22, "
                StrSql = StrSql + "'Total Fixed Cost' AS DES23, "
                StrSql = StrSql + "'Total Cost' AS DES24, "
                StrSql = StrSql + "(CASE WHEN RSPL.PVOLUSE=0 THEN RSPL.VOLUME*PREF.CONVWT "
                StrSql = StrSql + "ELSE RSPL.SVOLUME*PREF.CONVWT END) AS SALESVOLUMELB, "

                StrSql = StrSql + "NVL((CASE WHEN RSPL.FINVOLMUNITS>0 THEN RSPL.FINVOLMUNITS END),0) SALESVOLUMEUNIT, "

                StrSql = StrSql + "'SALES VOLUME ('|| PREF.TITLE8||')' AS VOLUNI1, "

                StrSql = StrSql + "(CASE WHEN RSPL.FINVOLMUNITS > 0 THEN 'SALES VOLUME (UNITS)' END) VOLUNI2, "
                StrSql = StrSql + "RSPL.VMATERIAL AS PL1 , "
                StrSql = StrSql + "RSPL.VLABOR AS PL2, "
                StrSql = StrSql + "RSPL.VENERGY AS PL3, "
                StrSql = StrSql + "RSPL.VPACK AS PL4, "
                StrSql = StrSql + "RSPL.VSHIP AS PL5, "
                StrSql = StrSql + "RSPL.variablecost AS PL6, "
                StrSql = StrSql + "RSPL.OFFICESUPPLIES AS PL7, "
                StrSql = StrSql + "RSPL.PLABOR AS PL8, "
                StrSql = StrSql + "RSPL.PENERGY AS PL9, "
                StrSql = StrSql + "RSPL.LEASECOST AS PL10, "
                StrSql = StrSql + "RSPL.INSURANCE AS PL11, "
                StrSql = StrSql + "RSPL.UTILITIES AS PL12, "
                StrSql = StrSql + "RSPL.COMMUN AS PL13, "
                StrSql = StrSql + "RSPL.TRAVEL AS PL14, "
                StrSql = StrSql + "RSPL.MAINT AS PL15, "
                StrSql = StrSql + "RSPL.MINOR AS PL16, "
                StrSql = StrSql + "RSPL.OUT  AS PL17, "
                StrSql = StrSql + "RSPL.PROF AS PL18, "
                StrSql = StrSql + "RSPL.LAB AS PL19, "
                StrSql = StrSql + "RSPL.INKSUP AS PL20, "
                StrSql = StrSql + "RSPL.PLATESUP AS PL21, "
                StrSql = StrSql + "RSPL.metsup AS PL22, "
                StrSql = StrSql + "RSPL.fixedcost AS PL23, "
                StrSql = StrSql + "RSPL.totalcost AS PL24, "
                StrSql = StrSql + "RSPL.finvolmsi, "
                StrSql = StrSql + "RSPL.finvolmunits, "
                StrSql = StrSql + "RSPL.unitprice, "
                StrSql = StrSql + "RSPL.totalcost, "
                StrSql = StrSql + "PREF.CURR, "
                StrSql = StrSql + "PREF.CONVAREA, "
                StrSql = StrSql + "PREF.CONVWT, "
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
                'ADDED FOR THE BUG#326
                StrSql = StrSql + " CASE WHEN CUSSALESUNIT=0 THEN RSPL.CUSSALESVOLUME*PREF.CONVWT  "
                StrSql = StrSql + " ELSE NVL((CASE  WHEN RSPL.FINVOLMUNITS>0  THEN  RSPL.CUSSALESVOLUME END),0) "
                StrSql = StrSql + " END AS CUSSALESVOLUME, "
                StrSql = StrSql + " CUSSALESVOLUME AS  CUSSALESVOLUME1, "
                StrSql = StrSql + " CUSSALESUNIT "
                'END FOR THE BUG#326
                StrSql = StrSql + "FROM RESULTSPL RSPL "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=RSPL.CASEID "
                StrSql = StrSql + "INNER JOIN CUSTOMERIN "
                StrSql = StrSql + "ON CUSTOMERIN.CASEID=RSPL.CASEID "
                StrSql = StrSql + "WHERE RSPL.CASEID IN (" + CaseId.ToString() + ")"
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetCostDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCostDetailsWithDep(ByVal CaseId As String, ByVal Isdep As Boolean) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty

            Try
                StrSql = "SELECT        RSPL.CASEID ,  "
                StrSql = StrSql + "'Material Cost' AS DES1, "
                StrSql = StrSql + "'Labor Cost' AS DES2, "
                StrSql = StrSql + "'Energy Cost' AS DES3, "
                StrSql = StrSql + "'Distribution Packaging Cost' AS DES4, "
                StrSql = StrSql + "'Shipping Cost' AS DES5, "
                StrSql = StrSql + "'Total Variable Cost' AS DES6, "
                StrSql = StrSql + "'Office Supplies Cost' AS DES7, "
                StrSql = StrSql + "'Labor Cost' AS DES8, "
                StrSql = StrSql + "'Energy Cost' AS DES9, "
                StrSql = StrSql + "'Lease Cost' AS DES10, "
                StrSql = StrSql + "'Insurance Cost' AS DES11, "
                StrSql = StrSql + "'Utilities Cost' AS DES12, "
                StrSql = StrSql + "'Communications Cost' AS DES13, "
                StrSql = StrSql + "'Travel Cost' AS DES14, "
                StrSql = StrSql + "'Maintenance Supplies Cost' AS DES15, "
                StrSql = StrSql + "'Minor Equipment Cost' AS DES16, "
                StrSql = StrSql + "'Outside Services Cost' AS DES17, "
                StrSql = StrSql + "'Professional Services Cost'  AS DES18, "
                StrSql = StrSql + "'Laboratory Supplies Cost' AS DES19, "
                StrSql = StrSql + "'Ink Supplies Cost' AS DES20, "
                StrSql = StrSql + "'Plate Supplies Cost' AS DES21, "
                StrSql = StrSql + "'Metal Supplies Cost' AS DES22, "
                StrSql = StrSql + "'Depreciation' AS DES23, "
                StrSql = StrSql + "'Total Fixed Cost' AS DES24, "
                StrSql = StrSql + "'Total Cost' AS DES25, "
                StrSql = StrSql + "(CASE WHEN RSPL.PVOLUSE=0 THEN RSPL.VOLUME*PREF.CONVWT "
                StrSql = StrSql + "ELSE RSPL.SVOLUME*PREF.CONVWT END) AS SALESVOLUMELB, "

                StrSql = StrSql + "NVL((CASE WHEN RSPL.FINVOLMUNITS>0 THEN RSPL.FINVOLMUNITS END),0) SALESVOLUMEUNIT, "
                StrSql = StrSql + "'SALES VOLUME ('|| PREF.TITLE8||')' AS VOLUNI1, "

                StrSql = StrSql + "(CASE WHEN RSPL.FINVOLMUNITS > 0 THEN 'SALES VOLUME (UNITS)' END) VOLUNI2, "
                StrSql = StrSql + "RSPL.VMATERIAL AS PL1 , "
                StrSql = StrSql + "RSPL.VLABOR AS PL2, "
                StrSql = StrSql + "RSPL.VENERGY AS PL3, "
                StrSql = StrSql + "RSPL.VPACK AS PL4, "
                StrSql = StrSql + "RSPL.VSHIP AS PL5, "
                StrSql = StrSql + "RSPL.variablecost AS PL6, "
                StrSql = StrSql + "RSPL.OFFICESUPPLIES AS PL7, "
                StrSql = StrSql + "RSPL.PLABOR AS PL8, "
                StrSql = StrSql + "RSPL.PENERGY AS PL9, "
                StrSql = StrSql + "RSPL.LEASECOST AS PL10, "
                StrSql = StrSql + "RSPL.INSURANCE AS PL11, "
                StrSql = StrSql + "RSPL.UTILITIES AS PL12, "
                StrSql = StrSql + "RSPL.COMMUN AS PL13, "
                StrSql = StrSql + "RSPL.TRAVEL AS PL14, "
                StrSql = StrSql + "RSPL.MAINT AS PL15, "
                StrSql = StrSql + "RSPL.MINOR AS PL16, "
                StrSql = StrSql + "RSPL.OUT  AS PL17, "
                StrSql = StrSql + "RSPL.PROF AS PL18, "
                StrSql = StrSql + "RSPL.LAB AS PL19, "
                StrSql = StrSql + "RSPL.INKSUP AS PL20, "
                StrSql = StrSql + "RSPL.PLATESUP AS PL21, "
                StrSql = StrSql + "RSPL.metsup AS PL22, "
                StrSql = StrSql + "DEPRECIATION.DEPRECIATION AS PL23, "
                StrSql = StrSql + "RSPL.fixedcostdep AS PL24, "
                StrSql = StrSql + "RSPL.totalcostdep AS PL25, "
                StrSql = StrSql + "RSPL.finvolmsi, "
                StrSql = StrSql + "RSPL.finvolmunits, "
                StrSql = StrSql + "RSPL.unitprice, "
                StrSql = StrSql + "RSPL.totalcostdep, "
                StrSql = StrSql + "PREF.CURR, "
                StrSql = StrSql + "PREF.CONVAREA, "
                StrSql = StrSql + "PREF.CONVWT, "
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
                'ADDED FOR THE BUG#326
                StrSql = StrSql + " CASE WHEN CUSSALESUNIT=0 THEN RSPL.CUSSALESVOLUME*PREF.CONVWT  "
                StrSql = StrSql + " ELSE NVL((CASE  WHEN RSPL.FINVOLMUNITS>0  THEN  RSPL.CUSSALESVOLUME END),0) "
                StrSql = StrSql + " END AS CUSSALESVOLUME, "
                StrSql = StrSql + " CUSSALESVOLUME AS  CUSSALESVOLUME1, "
                StrSql = StrSql + " CUSSALESUNIT "
                'END FOR THE BUG#326
                StrSql = StrSql + "FROM RESULTSPL RSPL "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=RSPL.CASEID "
                StrSql = StrSql + "INNER JOIN DEPRECIATION "
                StrSql = StrSql + "ON DEPRECIATION.CASEID=RSPL.CASEID "
                StrSql = StrSql + "WHERE RSPL.CASEID IN (" + CaseId.ToString() + ")"
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetCostDetailsWithDep:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Error"
        Public Function GetErrors(ByVal ErrorCode As String) As DataSet
            Dim Dts As New DataSet()
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MoldE2ConnectionString")
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
                Throw New Exception("MoldE2GetData:GetErrors:" + ex.Message.ToString())
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
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetAssumptionPageDetails:" + ex.Message.ToString())
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

                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetAssumptionPageDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function


#End Region

#Region "Wizard"
        Public Function GetSessionWizardId() As DataSet
            Dim Dts As New DataSet()
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MoldE2ConnectionString")
                Dim StrSql As String = ""
                StrSql = "SELECT (SEQSESSIONID.NEXTVAL) As SessionId FROM DUAL "
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
#End Region

#Region "Charts"
        Public Function GetChartProfitAndLoss(ByVal IsDep As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                If IsDep = "N" Then


                    StrSql = "SELECT 'Revenue' AS TYPEDES,  "
                    StrSql = StrSql + "'REVENUE' AS TYPE, "
                    StrSql = StrSql + "'1' AS SEQ "
                    StrSql = StrSql + "FROM DUAL "
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT 'Variable Margin' AS TYPEDES, "
                    StrSql = StrSql + "'VM' AS TYPE, "
                    StrSql = StrSql + "'2' AS SEQ "
                    StrSql = StrSql + "FROM DUAL "
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT 'Plant Margin' AS TYPEDES, "
                    StrSql = StrSql + "'PM' AS TYPE, "
                    StrSql = StrSql + "'2' AS SEQ "
                    StrSql = StrSql + "FROM DUAL ORDER BY  SEQ"
                Else

                    StrSql = "SELECT 'Revenue With Depreciation' AS TYPEDES,  "
                    StrSql = StrSql + "'REVENUE' AS TYPE, "
                    StrSql = StrSql + "'1' AS SEQ "
                    StrSql = StrSql + "FROM DUAL "
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT 'Variable Margin With Depreciation' AS TYPEDES, "
                    StrSql = StrSql + "'VM' AS TYPE, "
                    StrSql = StrSql + "'2' AS SEQ "
                    StrSql = StrSql + "FROM DUAL "
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT 'Plant Margin With Depreciation' AS TYPEDES, "
                    StrSql = StrSql + "'PMDEP' AS TYPE, "
                    StrSql = StrSql + "'3' AS SEQ "
                    StrSql = StrSql + "FROM DUAL ORDER BY  SEQ"

                End If
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetChartProfitAndLoss:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetChartCost(ByVal IsDep As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                If IsDep = "N" Then

                    StrSql = "SELECT 'Variable Cost' AS TYPEDES,  "
                    StrSql = StrSql + "'VARIABLECOST' AS TYPE, "
                    StrSql = StrSql + "'1' AS SEQ "
                    StrSql = StrSql + "FROM DUAL "
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT 'Fixed Cost' AS TYPEDES, "
                    StrSql = StrSql + "'FIXEDCOST' AS TYPE, "
                    StrSql = StrSql + "'2' AS SEQ "
                    StrSql = StrSql + "FROM DUAL "
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT 'Total Cost' AS TYPEDES, "
                    StrSql = StrSql + "'TOTALCOST' AS TYPE, "
                    StrSql = StrSql + "'2' AS SEQ "
                    StrSql = StrSql + "FROM DUAL ORDER BY  SEQ"
                Else

                    StrSql = "SELECT 'Variable Cost With Depreciation' AS TYPEDES,  "
                    StrSql = StrSql + "'VARIABLECOST' AS TYPE, "
                    StrSql = StrSql + "'1' AS SEQ "
                    StrSql = StrSql + "FROM DUAL "
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT 'Fixed Cost With Depreciation' AS TYPEDES, "
                    StrSql = StrSql + "'FIXEDCOSTDEP' AS TYPE, "
                    StrSql = StrSql + "'2' AS SEQ "
                    StrSql = StrSql + "FROM DUAL "
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT 'Total Cost With Depreciation' AS TYPEDES, "
                    StrSql = StrSql + "'TOTALCOSTDEP' AS TYPE, "
                    StrSql = StrSql + "'3' AS SEQ "
                    StrSql = StrSql + "FROM DUAL ORDER BY  SEQ"

                End If
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetChartCost:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetChartProfitAndLossRes(ByVal CaseId1 As String, ByVal CaseId2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT RESULTSPL.CASEID,  "
                StrSql = StrSql + "( CASE WHEN RESULTSPL.CASEID <= 1000 THEN "
                StrSql = StrSql + "( SELECT (BASECASES.CASEID||':'||REPLACE(REPLACE((REPLACE(NVL(BASECASES.CASEDE1,''),')','}')),'(','{')||' '||REPLACE((REPLACE(NVL(BASECASES.CASEDE2,''),')','}')),'(','{'),';',':') ) "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + "WHERE  BASECASES.CASEID = RESULTSPL.CASEID "
                StrSql = StrSql + ") ELSE "
                StrSql = StrSql + "( SELECT DISTINCT (PERMISSIONSCASES.CASEID||':'||REPLACE(REPLACE((REPLACE(NVL(PERMISSIONSCASES.CASEDE1,''),')','}')),'(','{') || ' ' || REPLACE((REPLACE(NVL(PERMISSIONSCASES.CASEDE2,''),')','}')),'(','{'),';',':') )  "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE  PERMISSIONSCASES.CASEID = RESULTSPL.CASEID "
                StrSql = StrSql + ") "
                StrSql = StrSql + "END  ) AS CASEDE, "
                StrSql = StrSql + "RESULTSPL.FINVOLMSI, "
                StrSql = StrSql + "RESULTSPL.FINVOLMUNITS, "
                StrSql = StrSql + "RESULTSPL.VOLUME, "
                StrSql = StrSql + "RESULTSPL.SVOLUME, "
                StrSql = StrSql + "RESULTSPL.PVOLUSE, "
                StrSql = StrSql + "RESULTSPL.REVENUE, "
                StrSql = StrSql + "RESULTSPL.VM, "
                StrSql = StrSql + "RESULTSPL.PM, "
                StrSql = StrSql + "RESULTSPL.PMDEP, "
                'ADDED FOR THE BUG#326
                StrSql = StrSql + " RESULTSPL.CUSSALESVOLUME, "
                StrSql = StrSql + " RESULTSPL.CUSSALESUNIT "
                'END FOR THE BUG#326
                StrSql = StrSql + "FROM RESULTSPL "
                StrSql = StrSql + "WHERE RESULTSPL.CASEID IN (" + CaseId1.ToString() + "," + CaseId2.ToString() + ") "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetChartProfitAndLossRes:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetChartCostRes(ByVal CaseId1 As String, ByVal CaseId2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT RESULTSPL.CASEID,  "
                StrSql = StrSql + "( CASE WHEN RESULTSPL.CASEID <= 1000 THEN "
                StrSql = StrSql + "( SELECT (BASECASES.CASEID||':'||REPLACE(REPLACE((REPLACE(NVL(BASECASES.CASEDE1,''),')','}')),'(','{')||' '||REPLACE((REPLACE(NVL(BASECASES.CASEDE2,''),')','}')),'(','{'),';',':') ) "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + "WHERE  BASECASES.CASEID = RESULTSPL.CASEID "
                StrSql = StrSql + ") ELSE "
                StrSql = StrSql + "( SELECT DISTINCT (PERMISSIONSCASES.CASEID||':'||REPLACE(REPLACE((REPLACE(NVL(PERMISSIONSCASES.CASEDE1,''),')','}')),'(','{') || ' ' || REPLACE((REPLACE(NVL(PERMISSIONSCASES.CASEDE2,''),')','}')),'(','{'),';',':') )  "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE  PERMISSIONSCASES.CASEID = RESULTSPL.CASEID "
                StrSql = StrSql + ") "
                StrSql = StrSql + "END  ) AS CASEDE, "
                StrSql = StrSql + "RESULTSPL.FINVOLMSI, "
                StrSql = StrSql + "RESULTSPL.FINVOLMUNITS, "
                StrSql = StrSql + "RESULTSPL.VOLUME, "
                StrSql = StrSql + "RESULTSPL.SVOLUME, "
                StrSql = StrSql + "RESULTSPL.PVOLUSE, "
                StrSql = StrSql + "RESULTSPL.FIXEDCOSTDEP, "
                StrSql = StrSql + "RESULTSPL.TOTALCOSTDEP, "
                StrSql = StrSql + "RESULTSPL.VARIABLECOST, "
                StrSql = StrSql + "RESULTSPL.FIXEDCOST, "
                StrSql = StrSql + "RESULTSPL.TOTALCOST ,"
                'ADDED FOR THE BUG#326
                StrSql = StrSql + " RESULTSPL.CUSSALESVOLUME, "
                StrSql = StrSql + " RESULTSPL.CUSSALESUNIT "
                'END FOR THE BUG#326
                StrSql = StrSql + "FROM RESULTSPL "
                StrSql = StrSql + "WHERE RESULTSPL.CASEID IN (" + CaseId1.ToString() + "," + CaseId2.ToString() + ") "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetChartCostRes:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region
#Region "LicenseAdministrator"
        Public Function GetPCaseDetailsByLicense(ByVal UserName As String) As DataSet
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
                StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "') "
                StrSql = StrSql + " ORDER BY UPPER(CASEDE1),CASEID "

                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetPCaseDetailsByLicense:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCasesByLicense(ByVal UserName As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String) As DataSet
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
                StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "') "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetCasesByLicense:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region
#Region "Case Grouping"
        Public Function ValidateGroupName(ByVal Des1 As String, ByVal Des2 As String, ByVal USERID As String) As DataSet
            Dim odButil As New DBUtil()
            Dim strsql As String = String.Empty
            Dim Dts As New DataSet
            Dim GROUPID As String = ""
            Dim i As Integer = 0
            Try
                'Getting GROUPID from Sequence
                strsql = String.Empty
                strsql = strsql + "SELECT 1 "
                strsql = strsql + "FROM "
                strsql = strsql + "GROUPS "
                strsql = strsql + "WHERE "
                strsql = strsql + "USERID=" + USERID + " "
                strsql = strsql + "AND UPPER(DES1)='" + Des1.ToUpper() + "' "
                strsql = strsql + "AND "
                If Des2 = "" Then
                    strsql = strsql + "DES2 IS NULL "
                Else
                    strsql = strsql + "UPPER(DES2)='" + Des2.ToUpper() + "' "
                End If
                Dts = odButil.FillDataSet(strsql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Return Dts
                Throw New Exception("MoldE2GetData:ValidateGroupName:" + ex.Message.ToString())
            End Try
        End Function
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


                StrSql = StrSql + "FROM GROUPS "
                StrSql = StrSql + "INNER JOIN GROUPCASES "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
                StrSql = StrSql + "AND GROUPS.USERID=" + UserID + " "
                StrSql = StrSql + "RIGHT OUTER JOIN PERMISSIONSCASES PC "
                StrSql = StrSql + "ON PC.CASEID=GROUPCASES.CASEID "
                StrSql = StrSql + "WHERE PC.USERID =" + UserID + " "
                StrSql = StrSql + "ORDER BY CASEID DESC "
                StrSql = StrSql + " ) "

                StrSql = StrSql + "WHERE UPPER(CASEDE1) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE2) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEID) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE3) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(GROUPNAME) LIKE '%" + keyWord.ToUpper().Trim() + "%' "

                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetPCaseGrpDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetGroupDetailsByID(ByVal grpID As String) As DataSet
            Dim Dts As New DataSet()
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                Dim StrSql As String = ""
                StrSql = "SELECT GROUPID,  "
                StrSql = StrSql + "DES1, "
                StrSql = StrSql + "DES2, "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "DES1 || '&'||'nbsp;'||'&'||'nbsp;'||'&'||'nbsp;'|| DES2 AS GDES, "
                StrSql = StrSql + "CREATIONDATE, "
                StrSql = StrSql + "UPDATEDATE "
                StrSql = StrSql + "FROM GROUPS WHERE GROUPID= " + grpID + " "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
        Public Function ValiDateGroupcases(ByVal CaseIDs As String, ByVal UserID As String) As DataSet
            Dim odbUtil As New DBUtil()
            Dim MyConnectionString As String = ""
            Dim Dts As New DataSet()
            Dim strCaseIDS() As String
            Dim i As Integer = 0
            Dim StrSql As String = ""
            Dim strQuery As String = String.Empty
            Dim count As Integer = 0
            Try
                'MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MoldE2ConnectionString")
                strCaseIDS = Regex.Split(CaseIDs, ",")

                If strCaseIDS.Length > 0 Then
                    For i = 0 To strCaseIDS.Length - 1
                        If strCaseIDS(i) <> "" Then
                            StrSql = "SELECT GROUPS.GROUPID,  "
                            StrSql = StrSql + "CASEID, "
                            StrSql = StrSql + "DES1 AS GROUPNAME "
                            StrSql = StrSql + "FROM "
                            StrSql = StrSql + "GROUPCASES "
                            StrSql = StrSql + "INNER JOIN GROUPS "
                            StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.GROUPID "
                            StrSql = StrSql + "WHERE CASEID=" + strCaseIDS(i).ToString()
                            StrSql = StrSql + " AND UserID=" + UserID.ToString()
                            Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                            If Dts.Tables(0).Rows.Count > 0 Then
                                If count = 0 Then
                                    strQuery = " SELECT '" + Dts.Tables(0).Rows(0).Item("CASEID").ToString() + "' CASEID,'" + Dts.Tables(0).Rows(0).Item("GROUPID").ToString() + "' GROUPID,'" + Dts.Tables(0).Rows(0).Item("GROUPNAME").ToString() + "' AS GROUPNAME FROM DUAL "
                                Else
                                    strQuery = strQuery + " UNION ALL SELECT '" + Dts.Tables(0).Rows(0).Item("CASEID").ToString() + "' CASEID,'" + Dts.Tables(0).Rows(0).Item("GROUPID").ToString() + "' GROUPID,'" + Dts.Tables(0).Rows(0).Item("GROUPNAME").ToString() + "' AS GROUPNAME FROM DUAL "
                                End If
                                count += 1
                            End If
                        End If
                    Next
                End If
                If strQuery <> "" Then
                    Dts = odbUtil.FillDataSet(strQuery, MoldE2Connection)
                Else
                    StrSql = " SELECT * FROM GROUPCASES WHERE GROUPID=0 "
                    Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                End If

                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function

        Public Function GetPCaseDetailsByGroup(ByVal UserId As String, ByVal grpId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try


                StrSql = "SELECT DISTINCT PERMISSIONSCASES.CASEID,  "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "CASEDE2, "
                StrSql = StrSql + "CASEDE3, "
                StrSql = StrSql + "(PERMISSIONSCASES.CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES, "
                StrSql = StrSql + "SEQ "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "INNER JOIN GROUPCASES ON GROUPCASES.CASEID=PERMISSIONSCASES.CASEID "
                StrSql = StrSql + "WHERE USERID =" + UserId + " AND GROUPCASES.GROUPID=" + grpId + " "
                StrSql = StrSql + "AND PERMISSIONSCASES.CASEID IN(SELECT CASEID FROM GROUPCASES WHERE GROUPID =" + grpId + ") "
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID "


                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetPCaseDetailsByGroup:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetGroupIDByUSer(ByVal UserID As String) As DataSet
            Dim Dts As New DataSet()
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                ' MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MoldE2ConnectionString")
                Dim StrSql As String = ""
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
                Throw
                Return Dts
            End Try
        End Function
        Public Function GetGroupCasesByUSer(ByVal grpID As String) As DataSet
            Dim Dts As New DataSet()
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                Dim StrSql As String = ""
                StrSql = "SELECT GROUPCASEID,  "
                StrSql = StrSql + "GROUPID, "
                StrSql = StrSql + "CASEID, "
                StrSql = StrSql + "SEQ "
                StrSql = StrSql + "FROM GROUPCASES WHERE GROUPID=" + grpID + " "
                StrSql = StrSql + "ORDER BY SEQ ASC "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw
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
                'MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MoldE2ConnectionString")
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
                            strSQL = "SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '   ' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '   '|| '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "'  AS GROUPDES,'" + CaseIDs + "' AS CASEIDS,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS DES1,'" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS DES2,'" + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + "' || ':' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS CDES1 FROM DUAL "
                        Else
                            cnt += 1
                            strSQL = strSQL + "UNION ALL SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' ||  '   ' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '   '||  '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "'  AS GROUPDES,'" + CaseIDs + "' AS CASEIDS,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS DES1,'" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS DES2,'" + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + "' || ':' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS CDES1 FROM DUAL "
                        End If
                    Next
                    strSQL = "SELECT * FROM (" + strSQL + ") DUAL ORDER BY UPPER(DES1),UPPER(DES2)"
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
        Public Function GetGroupCases(ByVal UserId As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String, ByVal groupID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE USERID =" + UserId + " "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "AND CASEID IN(SELECT CASEID FROM ECON2MOLD.GROUPCASES WHERE GROUPID=" + groupID + " ) "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID"
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetMaxSEQGCASE(ByVal grpId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT NVL(MAX(SEQ),0) MAXCOUNT  "
                StrSql = StrSql + "FROM GROUPCASES "
                StrSql = StrSql + "WHERE GROUPID= " + grpId

                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetMaxSEQGCASE:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetGroupDetails(ByVal UserID As String, ByVal flag As Char) As DataSet
            Dim Dts As New DataSet()
            'Dim objGetData As New E1GetData.Selectdata()
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
            'MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MoldE1ConnectionString")
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
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, MoldE1Connection)
                End If

                Return dtOutPut
            Catch ex As Exception
                Throw
                Return DtRes
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
                    strSqlOutPut = " SELECT * FROM GROUPCASES WHERE GROUPID=0"
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
                If Schema = "E2" Then
                    con = MoldE2Connection
                End If
                StrSql = "SELECT CASEID FROM STATUSUPDATE WHERE "
                StrSql = StrSql + "CASEID = " + CaseId + " AND UPPER(USERNAME)='" + userName.ToUpper() + "' AND UPPER(STATUS)='SISTER CASE' ORDER BY DATED ASC"

                Dts = odbUtil.FillDataSet(StrSql, con)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetStatusDetailsByID:" + ex.Message.ToString())
            End Try
        End Function
        Public Function GetPCaseGrpDetailsBem(ByVal UserName As String, ByVal UserID As String, ByVal keyWord As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim CaseIds As String = ""
            Try
                CaseIds = GetPropCaseStatus(UserName)

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


                StrSql = StrSql + "FROM GROUPS "
                StrSql = StrSql + "INNER JOIN GROUPCASES "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
                StrSql = StrSql + "AND GROUPS.USERID=" + UserID + " "
                StrSql = StrSql + "RIGHT OUTER JOIN PERMISSIONSCASES PC "
                StrSql = StrSql + "ON PC.CASEID=GROUPCASES.CASEID "
                StrSql = StrSql + "WHERE USERID =" + UserID + " "
                StrSql = StrSql + "AND NVL(PC.STATUSID,0) NOT IN(3,4) "

                StrSql = StrSql + "UNION ALL "

                StrSql = StrSql + "SELECT CASE WHEN NVL(GROUPS.GROUPID,'0')='0' THEN 'None' ELSE GROUPS.DES1 END AS GROUPNAME,  "
                StrSql = StrSql + "NVL(GROUPS.GROUPID,'0') AS GROUPID, "
                StrSql = StrSql + "PC.CASEID, "
                StrSql = StrSql + "PC.CASEDE1, "
                StrSql = StrSql + "PC.CASEDE2, "
                StrSql = StrSql + "PC.CASEDE3, "
                StrSql = StrSql + "(PC.CASEID||'. PACKAGE FORMAT= '||PC.CASEDE1||' UNIQUE FEATURES= '||PC.CASEDE2)CASEDES, "

                StrSql = StrSql + " PC.CREATIONDATE, "
                StrSql = StrSql + "CASE WHEN PC.CREATIONDATE-PC.SERVERDATE =0 THEN 'NA' ELSE to_char(PC.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE "


                StrSql = StrSql + "FROM GROUPS "
                StrSql = StrSql + "INNER JOIN GROUPCASES "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
                StrSql = StrSql + "AND GROUPS.USERID=" + UserID + " "
                StrSql = StrSql + "RIGHT OUTER JOIN PERMISSIONSCASES PC "
                StrSql = StrSql + "ON PC.CASEID=GROUPCASES.CASEID "
                StrSql = StrSql + "WHERE USERID =" + UserID + " "
                StrSql = StrSql + "AND PC.CASEID in(" + CaseIds + ") "

                StrSql = StrSql + " ) "

                StrSql = StrSql + "WHERE UPPER(CASEDE1) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE2) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEID) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE3) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(GROUPNAME) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "ORDER BY CASEID DESC "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetPCaseGrpDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetPropCasesById(ByVal UserId As String, ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = StrSql + "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,MS.STATUS,PERMISSIONSCASES.STATUSID,USERNAME CaseOwner "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                StrSql = StrSql + "WHERE USERID =" + UserId + " "
                StrSql = StrSql + "AND CASEID= " + CaseId + " "
                StrSql = StrSql + "AND NVL(PERMISSIONSCASES.STATUSID,0) NOT IN(3,4) "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetPropCasesById:" + ex.Message.ToString())
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
        Public Function GetApprovedCases(ByVal UserName As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  CASEID, CASEDES,CASEDE1, CASEDE2,CASEDE, STATUS,STATUSID,CaseOwner  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "(SELECT 0 CASEID, 'Select Case' CASEDES, 'Select Case' CASEDE1, ''CASEDE2, 'Select Case' CASEDE, '' STATUS,0 STATUSID,'None'  CaseOwner "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,MS.STATUS,PERMISSIONSCASES.STATUSID,USERS.UserName CaseOwner "
                StrSql = StrSql + "FROM PERMISSIONSCASES "

                StrSql = StrSql + "INNER JOIN ECON.USERS USERS "
                StrSql = StrSql + "ON USERS.USERID=PERMISSIONSCASES.USERID "
                StrSql = StrSql + "INNER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT LICENSEID FROM ECON.USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "') "

                StrSql = StrSql + "AND PERMISSIONSCASES.STATUSID=3) "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetApprovedCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetAppCasesByLicense(ByVal UserName As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String) As DataSet
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
                StrSql = StrSql + "MS.STATUS,PERMISSIONSCASES.STATUSID ,USERS.USERNAME CaseOwner "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "INNER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                StrSql = StrSql + "INNER JOIN ECON.USERS USERS "
                StrSql = StrSql + "ON USERS.USERID=PERMISSIONSCASES.USERID "
                StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "') "

                StrSql = StrSql + "AND PERMISSIONSCASES.STATUSID=3) "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetCasesByLicense:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetPropCases(ByVal UserName As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim CaseIds As String = ""
            Try
                CaseIds = GetPropCaseStatus(UserName)

                StrSql = "SELECT  CASEID, CASEDES,CASEDE1, CASEDE2,CASEDE, STATUS,STATUSID,CaseOwner  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "(SELECT 0 CASEID, 'Select Case' CASEDES, 'Select Case' CASEDE1, ''CASEDE2, 'Select Case' CASEDE, '' STATUS,0 STATUSID,'None'  CaseOwner "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION ALL "


                StrSql = StrSql + "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,MS.STATUS,PERMISSIONSCASES.STATUSID,USERS.USERNAME CaseOwner "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                StrSql = StrSql + "INNER JOIN ECON.USERS USERS "
                StrSql = StrSql + "ON USERS.USERID=PERMISSIONSCASES.USERID "
                StrSql = StrSql + "WHERE USERID IN (SELECT USERID FROM ECON.USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "') "

                StrSql = StrSql + "AND NVL(PERMISSIONSCASES.STATUSID,0) NOT IN(3,4) "

                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,MS.STATUS,PERMISSIONSCASES.STATUSID,USERS.USERNAME CaseOwner "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                StrSql = StrSql + "INNER JOIN ECON.USERS USERS "
                StrSql = StrSql + "ON USERS.USERID=PERMISSIONSCASES.USERID "
                StrSql = StrSql + "WHERE CaseId in(" + CaseIds + ") "
                StrSql = StrSql + "AND USERID IN (SELECT USERID FROM ECON.USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "') "


                StrSql = StrSql + ") "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetPropCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetPropCaseStatus(ByVal UserName As String) As String
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
                StrSql = StrSql + "WHERE STATUSID=4 AND USERID IN (SELECT USERID FROM ECON.USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "') "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                If Dts.Tables(0).Rows.Count > 0 Then
                    For i = 0 To Dts.Tables(0).Rows.Count - 1
                        ds = GetPropDisAppStatus(UserName, Dts.Tables(0).Rows(i).Item("CaseId").ToString())
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
                Throw New Exception("MoldE2GetData:GetPropCaseStatus:" + ex.Message.ToString())
                Return CaseIds
            End Try
        End Function
        Public Function GetPropDisAppStatus(ByVal UserName As String, ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim CaseIds As String = ""
            Try
                StrSql = StrSql + "SELECT * "
                StrSql = StrSql + "FROM STATUSUPDATE "
                StrSql = StrSql + "WHERE UPPER(USERNAME)='" + UserName.ToUpper().ToString() + "' AND CASEID=" + CaseId.ToString() + " AND UPPER(STATUS)='APPROVED' "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                If Dts.Tables(0).Rows.Count > 0 Then

                Else
                    CaseIds = "0"
                End If
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetPropDisAppStatus:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetPropCasesByLicense(ByVal UserName As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String) As DataSet
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
                StrSql = StrSql + "MS.STATUS,PERMISSIONSCASES.STATUSID,USERS.USERNAME CaseOwner  "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                StrSql = StrSql + "INNER JOIN ECON.USERS "
                StrSql = StrSql + "ON USERS.USERID=PERMISSIONSCASES.USERID "
                StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "') "
                'StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                'StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(PERMISSIONSCASES.STATUSID,0) NOT IN(3) "

                'dsUsers = GetUserCompanyUsersBem(UserName)
                'If dsUsers.Tables(0).Rows.Count > 0 Then
                '    For j = 0 To dsUsers.Tables(0).Rows.Count - 1
                '        StrSqlN = "SELECT CASEID "
                '        StrSqlN = StrSqlN + "FROM PERMISSIONSCASES "
                '        StrSqlN = StrSqlN + "WHERE STATUSID=4 AND UPPER(USERNAME)='" + dsUsers.Tables(0).Rows(j).Item("USERNAME").ToString().ToUpper() + "' "
                '        Dts = odbUtil.FillDataSet(StrSqlN, MoldE2Connection)
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
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetPropCasesByLicense:" + ex.Message.ToString())
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

                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetStatusDetailsByID:" + ex.Message.ToString())
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

                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetGrpStatusDetailsByID:" + ex.Message.ToString())
            End Try
        End Function
        Public Function GetPermissionStatus(ByVal CaseId As Integer, ByVal UserName As String) As DataSet
            Try
                Dim Dts As New DataSet()
                Dim odbUtil As New DBUtil()
                Dim StrSql As String = ""
                StrSql = ""
                StrSql = "SELECT CASEID,STATUS,DATED,ACTIONBY,COMMENTS FROM STATUSUPDATE "
                StrSql = StrSql + "WHERE CASEID=" + CaseId.ToString() + " AND UPPER(ACTIONBY)='" + UserName.ToUpper() + "' ORDER BY DATED ASC "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E2Data:GetPermissionStatus:" + ex.Message.ToString())
            End Try
        End Function
        Public Function GetGroupPCases(ByVal UserName As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String, ByVal groupID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  CASEID, CASEDES,CASEDE1, CASEDE2,CASEDE, STATUS,STATUSID,CaseOwner  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "(SELECT 0 CASEID, 'Select Case' CASEDES, 'Select Case' CASEDE1, ''CASEDE2, 'Select Case' CASEDE, '' STATUS,0 STATUSID,'None'  CaseOwner "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,MS.STATUS,PERMISSIONSCASES.STATUSID,USERS.USERNAME CaseOwner "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                StrSql = StrSql + "INNER JOIN ECON.USERS "
                StrSql = StrSql + "ON USERS.USERID=PERMISSIONSCASES.USERID "
                StrSql = StrSql + "WHERE USERID IN (SELECT USERS.USERID FROM ECON.USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "')  "

                StrSql = StrSql + "AND CASEID IN(SELECT CASEID FROM ECON2MOLD.GROUPCASES WHERE GROUPID=" + groupID + " )) "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetGroupPCases:" + ex.Message.ToString())
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
                Throw New Exception("MoldE2GetData:GetUserCompanyUsersBem:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "DepreciationCost"
        Public Function GetAssetC(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT EQUIP.CASEID,  "
                StrSql = StrSql + "PREF.TITLE2 AS ASSESTCOSTUNIT, "
                StrSql = StrSql + "PREF.UNITS, "
                StrSql = StrSql + "PREF.CURR, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN 'square feet'  ELSE 'square meters' END AS PLANTAREAUNIT, "
                StrSql = StrSql + "NVL((EQCOS.M1*  PREF.CURR),0) AS ASSETP1, "
                StrSql = StrSql + "NVL((EQCOS.M2*  PREF.CURR),0) AS ASSETP2, "
                StrSql = StrSql + "NVL((EQCOS.M3*  PREF.CURR),0) AS ASSETP3, "
                StrSql = StrSql + "NVL((EQCOS.M4*  PREF.CURR),0) AS ASSETP4, "
                StrSql = StrSql + "NVL((EQCOS.M5*  PREF.CURR),0) AS ASSETP5, "
                StrSql = StrSql + "NVL((EQCOS.M6*  PREF.CURR),0) AS ASSETP6, "
                StrSql = StrSql + "NVL((EQCOS.M7*  PREF.CURR),0) AS ASSETP7, "
                StrSql = StrSql + "NVL((EQCOS.M8*  PREF.CURR),0) AS ASSETP8, "
                StrSql = StrSql + "NVL((EQCOS.M9*  PREF.CURR),0) AS ASSETP9, "
                StrSql = StrSql + "NVL((EQCOS.M10*  PREF.CURR),0) AS ASSETP10, "
                StrSql = StrSql + "NVL((EQCOS.M11*  PREF.CURR),0) AS ASSETP11, "
                StrSql = StrSql + "NVL((EQCOS.M12*  PREF.CURR),0) AS ASSETP12, "
                StrSql = StrSql + "NVL((EQCOS.M13*  PREF.CURR),0) AS ASSETP13, "
                StrSql = StrSql + "NVL((EQCOS.M14*  PREF.CURR),0) AS ASSETP14, "
                StrSql = StrSql + "NVL((EQCOS.M15*  PREF.CURR),0) AS ASSETP15, "
                StrSql = StrSql + "NVL((EQCOS.M16*  PREF.CURR),0) AS ASSETP16, "
                StrSql = StrSql + "NVL((EQCOS.M17*  PREF.CURR),0) AS ASSETP17, "
                StrSql = StrSql + "NVL((EQCOS.M18*  PREF.CURR),0) AS ASSETP18, "
                StrSql = StrSql + "NVL((EQCOS.M19*  PREF.CURR),0) AS ASSETP19, "
                StrSql = StrSql + "NVL((EQCOS.M20*  PREF.CURR),0) AS ASSETP20, "
                StrSql = StrSql + "NVL((EQCOS.M21*  PREF.CURR),0) AS ASSETP21, "
                StrSql = StrSql + "NVL((EQCOS.M22*  PREF.CURR),0) AS ASSETP22, "
                StrSql = StrSql + "NVL((EQCOS.M23*  PREF.CURR),0) AS ASSETP23, "
                StrSql = StrSql + "NVL((EQCOS.M24*  PREF.CURR),0) AS ASSETP24, "
                StrSql = StrSql + "NVL((EQCOS.M25*  PREF.CURR),0) AS ASSETP25, "
                StrSql = StrSql + "NVL((EQCOS.M26*  PREF.CURR),0) AS ASSETP26, "
                StrSql = StrSql + "NVL((EQCOS.M27*  PREF.CURR),0) AS ASSETP27, "
                StrSql = StrSql + "NVL((EQCOS.M28*  PREF.CURR),0) AS ASSETP28, "
                StrSql = StrSql + "NVL((EQCOS.M29*  PREF.CURR),0) AS ASSETP29, "
                StrSql = StrSql + "NVL((EQCOS.M30*  PREF.CURR),0) AS ASSETP30, "
                StrSql = StrSql + "(EQUIP1.COST *  PREF.CURR) AS ASSETS1, "
                StrSql = StrSql + "(EQUIP2.COST *  PREF.CURR) AS ASSETS2, "
                StrSql = StrSql + "(EQUIP3.COST *  PREF.CURR) AS ASSETS3, "
                StrSql = StrSql + "(EQUIP4.COST *  PREF.CURR) AS ASSETS4, "
                StrSql = StrSql + "(EQUIP5.COST *  PREF.CURR) AS ASSETS5, "
                StrSql = StrSql + "(EQUIP6.COST *  PREF.CURR) AS ASSETS6, "
                StrSql = StrSql + "(EQUIP7.COST *  PREF.CURR) AS ASSETS7, "
                StrSql = StrSql + "(EQUIP8.COST *  PREF.CURR) AS ASSETS8, "
                StrSql = StrSql + "(EQUIP9.COST *  PREF.CURR) AS ASSETS9, "
                StrSql = StrSql + "(EQUIP10.COST *  PREF.CURR) AS ASSETS10, "
                StrSql = StrSql + "(EQUIP11.COST *  PREF.CURR) AS ASSETS11, "
                StrSql = StrSql + "(EQUIP12.COST *  PREF.CURR) AS ASSETS12, "
                StrSql = StrSql + "(EQUIP13.COST *  PREF.CURR) AS ASSETS13, "
                StrSql = StrSql + "(EQUIP14.COST *  PREF.CURR) AS ASSETS14, "
                StrSql = StrSql + "(EQUIP15.COST *  PREF.CURR) AS ASSETS15, "
                StrSql = StrSql + "(EQUIP16.COST *  PREF.CURR) AS ASSETS16, "
                StrSql = StrSql + "(EQUIP17.COST *  PREF.CURR) AS ASSETS17, "
                StrSql = StrSql + "(EQUIP18.COST *  PREF.CURR) AS ASSETS18, "
                StrSql = StrSql + "(EQUIP19.COST *  PREF.CURR) AS ASSETS19, "
                StrSql = StrSql + "(EQUIP20.COST *  PREF.CURR) AS ASSETS20, "
                StrSql = StrSql + "(EQUIP21.COST *  PREF.CURR) AS ASSETS21, "
                StrSql = StrSql + "(EQUIP22.COST *  PREF.CURR) AS ASSETS22, "
                StrSql = StrSql + "(EQUIP23.COST *  PREF.CURR) AS ASSETS23, "
                StrSql = StrSql + "(EQUIP24.COST *  PREF.CURR) AS ASSETS24, "
                StrSql = StrSql + "(EQUIP25.COST *  PREF.CURR) AS ASSETS25, "
                StrSql = StrSql + "(EQUIP26.COST *  PREF.CURR) AS ASSETS26, "
                StrSql = StrSql + "(EQUIP27.COST *  PREF.CURR) AS ASSETS27, "
                StrSql = StrSql + "(EQUIP28.COST *  PREF.CURR) AS ASSETS28, "
                StrSql = StrSql + "(EQUIP29.COST *  PREF.CURR) AS ASSETS29, "
                StrSql = StrSql + "(EQUIP30.COST *  PREF.CURR) AS ASSETS30, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP1.EQUIPID )EQUIPDES1, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP2.EQUIPID )EQUIPDES2, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP3.EQUIPID )EQUIPDES3, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP4.EQUIPID )EQUIPDES4, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP5.EQUIPID )EQUIPDES5, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP6.EQUIPID )EQUIPDES6, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP7.EQUIPID )EQUIPDES7, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP8.EQUIPID )EQUIPDES8, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP9.EQUIPID )EQUIPDES9, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP10.EQUIPID )EQUIPDES10,"
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP11.EQUIPID )EQUIPDES11, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP12.EQUIPID )EQUIPDES12, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP13.EQUIPID )EQUIPDES13, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP14.EQUIPID )EQUIPDES14,"
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP15.EQUIPID )EQUIPDES15,"
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP16.EQUIPID )EQUIPDES16, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP17.EQUIPID )EQUIPDES17, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP18.EQUIPID )EQUIPDES18, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP19.EQUIPID )EQUIPDES19, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP20.EQUIPID )EQUIPDES20, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP21.EQUIPID )EQUIPDES21, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP22.EQUIPID )EQUIPDES22, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP23.EQUIPID )EQUIPDES23, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP24.EQUIPID )EQUIPDES24, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP25.EQUIPID )EQUIPDES25, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP26.EQUIPID )EQUIPDES26, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP27.EQUIPID )EQUIPDES27, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP28.EQUIPID )EQUIPDES28, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP29.EQUIPID )EQUIPDES29, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM equipment WHERE EQUIPID=EQUIP30.EQUIPID )EQUIPDES30, "
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
                StrSql = StrSql + "EQNUM.M30 AS NUM30, "
                StrSql = StrSql + "EQNUM.D1 AS DEPRE1, "
                StrSql = StrSql + "EQNUM.D2 AS DEPRE2, "
                StrSql = StrSql + "EQNUM.D3 AS DEPRE3, "
                StrSql = StrSql + "EQNUM.D4 AS DEPRE4, "
                StrSql = StrSql + "EQNUM.D5 AS DEPRE5, "
                StrSql = StrSql + "EQNUM.D6 AS DEPRE6, "
                StrSql = StrSql + "EQNUM.D7 AS DEPRE7, "
                StrSql = StrSql + "EQNUM.D8 AS DEPRE8, "
                StrSql = StrSql + "EQNUM.D9 AS DEPRE9, "
                StrSql = StrSql + "EQNUM.D10 AS DEPRE10, "
                StrSql = StrSql + "EQNUM.D11 AS DEPRE11, "
                StrSql = StrSql + "EQNUM.D12 AS DEPRE12, "
                StrSql = StrSql + "EQNUM.D13 AS DEPRE13, "
                StrSql = StrSql + "EQNUM.D14 AS DEPRE14, "
                StrSql = StrSql + "EQNUM.D15 AS DEPRE15, "
                StrSql = StrSql + "EQNUM.D16 AS DEPRE16, "
                StrSql = StrSql + "EQNUM.D17 AS DEPRE17, "
                StrSql = StrSql + "EQNUM.D18 AS DEPRE18, "
                StrSql = StrSql + "EQNUM.D19 AS DEPRE19, "
                StrSql = StrSql + "EQNUM.D20 AS DEPRE20, "
                StrSql = StrSql + "EQNUM.D21 AS DEPRE21, "
                StrSql = StrSql + "EQNUM.D22 AS DEPRE22, "
                StrSql = StrSql + "EQNUM.D23 AS DEPRE23, "
                StrSql = StrSql + "EQNUM.D24 AS DEPRE24, "
                StrSql = StrSql + "EQNUM.D25 AS DEPRE25, "
                StrSql = StrSql + "EQNUM.D26 AS DEPRE26, "
                StrSql = StrSql + "EQNUM.D27 AS DEPRE27, "
                StrSql = StrSql + "EQNUM.D28 AS DEPRE28, "
                StrSql = StrSql + "EQNUM.D29 AS DEPRE29, "
                StrSql = StrSql + "EQNUM.D30 AS DEPRE30, "
                StrSql = StrSql + "(TOTAL.ASSETTOTAL*PREF.CURR) AS ASSETTOTAL, "
                StrSql = StrSql + "(DEPC.DEPRECIATION*PREF.CURR) AS DEPTOTAL, "
                'Changes For Econ3
                StrSql = StrSql + "(RC.ICPE1*  PREF.CURR) AS ICPE1, "
                StrSql = StrSql + "(RC.ICPE2*  PREF.CURR) AS ICPE2, "
                StrSql = StrSql + "(RC.ICPE3*  PREF.CURR) AS ICPE3, "
                StrSql = StrSql + "(RC.ICPE4*  PREF.CURR) AS ICPE4, "
                StrSql = StrSql + "(RC.ICPE5*  PREF.CURR) AS ICPE5, "
                StrSql = StrSql + "(RC.ICPE6*  PREF.CURR) AS ICPE6, "
                StrSql = StrSql + "(RC.ICPE7*  PREF.CURR) AS ICPE7, "
                StrSql = StrSql + "(RC.ICPE8*  PREF.CURR) AS ICPE8, "
                StrSql = StrSql + "(RC.ICPE9*  PREF.CURR) AS ICPE9, "
                StrSql = StrSql + "(RC.ICPE10*  PREF.CURR) AS ICPE10, "
                StrSql = StrSql + "(RC.ICPE11*  PREF.CURR) AS ICPE11, "
                StrSql = StrSql + "(RC.ICPE12*  PREF.CURR) AS ICPE12, "
                StrSql = StrSql + "(RC.ICPE13*  PREF.CURR) AS ICPE13, "
                StrSql = StrSql + "(RC.ICPE14*  PREF.CURR) AS ICPE14, "
                StrSql = StrSql + "(RC.ICPE15*  PREF.CURR) AS ICPE15, "
                StrSql = StrSql + "(RC.ICPE16*  PREF.CURR) AS ICPE16, "
                StrSql = StrSql + "(RC.ICPE17*  PREF.CURR) AS ICPE17, "
                StrSql = StrSql + "(RC.ICPE18*  PREF.CURR) AS ICPE18, "
                StrSql = StrSql + "(RC.ICPE19*  PREF.CURR) AS ICPE19, "
                StrSql = StrSql + "(RC.ICPE20*  PREF.CURR) AS ICPE20, "
                StrSql = StrSql + "(RC.ICPE21*  PREF.CURR) AS ICPE21, "
                StrSql = StrSql + "(RC.ICPE22*  PREF.CURR) AS ICPE22, "
                StrSql = StrSql + "(RC.ICPE23*  PREF.CURR) AS ICPE23, "
                StrSql = StrSql + "(RC.ICPE24*  PREF.CURR) AS ICPE24, "
                StrSql = StrSql + "(RC.ICPE25*  PREF.CURR) AS ICPE25, "
                StrSql = StrSql + "(RC.ICPE26*  PREF.CURR) AS ICPE26, "
                StrSql = StrSql + "(RC.ICPE27*  PREF.CURR) AS ICPE27, "
                StrSql = StrSql + "(RC.ICPE28*  PREF.CURR) AS ICPE28, "
                StrSql = StrSql + "(RC.ICPE29*  PREF.CURR) AS ICPE29, "
                StrSql = StrSql + "(RC.ICPE30*  PREF.CURR) AS ICPE30, "

                StrSql = StrSql + "(RC.DCPE1*  PREF.CURR) AS DCPE1, "
                StrSql = StrSql + "(RC.DCPE2*  PREF.CURR) AS DCPE2, "
                StrSql = StrSql + "(RC.DCPE3*  PREF.CURR) AS DCPE3, "
                StrSql = StrSql + "(RC.DCPE4*  PREF.CURR) AS DCPE4, "
                StrSql = StrSql + "(RC.DCPE5*  PREF.CURR) AS DCPE5, "
                StrSql = StrSql + "(RC.DCPE6*  PREF.CURR) AS DCPE6, "
                StrSql = StrSql + "(RC.DCPE7*  PREF.CURR) AS DCPE7, "
                StrSql = StrSql + "(RC.DCPE8*  PREF.CURR) AS DCPE8, "
                StrSql = StrSql + "(RC.DCPE9*  PREF.CURR) AS DCPE9, "
                StrSql = StrSql + "(RC.DCPE10*  PREF.CURR) AS DCPE10, "
                StrSql = StrSql + "(RC.DCPE11*  PREF.CURR) AS DCPE11, "
                StrSql = StrSql + "(RC.DCPE12*  PREF.CURR) AS DCPE12, "
                StrSql = StrSql + "(RC.DCPE13*  PREF.CURR) AS DCPE13, "
                StrSql = StrSql + "(RC.DCPE14*  PREF.CURR) AS DCPE14, "
                StrSql = StrSql + "(RC.DCPE15*  PREF.CURR) AS DCPE15, "
                StrSql = StrSql + "(RC.DCPE16*  PREF.CURR) AS DCPE16, "
                StrSql = StrSql + "(RC.DCPE17*  PREF.CURR) AS DCPE17, "
                StrSql = StrSql + "(RC.DCPE18*  PREF.CURR) AS DCPE18, "
                StrSql = StrSql + "(RC.DCPE19*  PREF.CURR) AS DCPE19, "
                StrSql = StrSql + "(RC.DCPE20*  PREF.CURR) AS DCPE20, "
                StrSql = StrSql + "(RC.DCPE21*  PREF.CURR) AS DCPE21, "
                StrSql = StrSql + "(RC.DCPE22*  PREF.CURR) AS DCPE22, "
                StrSql = StrSql + "(RC.DCPE23*  PREF.CURR) AS DCPE23, "
                StrSql = StrSql + "(RC.DCPE24*  PREF.CURR) AS DCPE24, "
                StrSql = StrSql + "(RC.DCPE25*  PREF.CURR) AS DCPE25, "
                StrSql = StrSql + "(RC.DCPE26*  PREF.CURR) AS DCPE26, "
                StrSql = StrSql + "(RC.DCPE27*  PREF.CURR) AS DCPE27, "
                StrSql = StrSql + "(RC.DCPE28*  PREF.CURR) AS DCPE28, "
                StrSql = StrSql + "(RC.DCPE29*  PREF.CURR) AS DCPE29, "
                StrSql = StrSql + "(RC.DCPE30*  PREF.CURR) AS DCPE30, "

                StrSql = StrSql + "(RC.INVESTTOTALPE*  PREF.CURR) AS INVESTTOTALPE, "
                StrSql = StrSql + "(RC.DEPRETOTALPE*  PREF.CURR) AS DEPRETOTALPE "
                'End Econ3

                StrSql = StrSql + "FROM EQUIPMENTTYPE EQUIP "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP1 "
                StrSql = StrSql + "ON EQUIP1.EQUIPID=EQUIP.M1 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP2 "
                StrSql = StrSql + "ON EQUIP2.EQUIPID=EQUIP.M2 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP3 "
                StrSql = StrSql + "ON EQUIP3.EQUIPID=EQUIP.M3 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP4 "
                StrSql = StrSql + "ON EQUIP4.EQUIPID=EQUIP.M4 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP5 "
                StrSql = StrSql + "ON EQUIP5.EQUIPID=EQUIP.M5 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP6 "
                StrSql = StrSql + "ON EQUIP6.EQUIPID=EQUIP.M6 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP7 "
                StrSql = StrSql + "ON EQUIP7.EQUIPID=EQUIP.M7 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP8 "
                StrSql = StrSql + "ON EQUIP8.EQUIPID=EQUIP.M8 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP9 "
                StrSql = StrSql + "ON EQUIP9.EQUIPID=EQUIP.M9 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP10 "
                StrSql = StrSql + "ON EQUIP10.EQUIPID=EQUIP.M10 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP11 "
                StrSql = StrSql + "ON EQUIP11.EQUIPID=EQUIP.M11 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP12 "
                StrSql = StrSql + "ON EQUIP12.EQUIPID=EQUIP.M12 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP13 "
                StrSql = StrSql + "ON EQUIP13.EQUIPID=EQUIP.M13 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP14 "
                StrSql = StrSql + "ON EQUIP14.EQUIPID=EQUIP.M14 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP15 "
                StrSql = StrSql + "ON EQUIP15.EQUIPID=EQUIP.M15 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP16 "
                StrSql = StrSql + "ON EQUIP16.EQUIPID=EQUIP.M16 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP17 "
                StrSql = StrSql + "ON EQUIP17.EQUIPID=EQUIP.M17 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP18 "
                StrSql = StrSql + "ON EQUIP18.EQUIPID=EQUIP.M18 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP19 "
                StrSql = StrSql + "ON EQUIP19.EQUIPID=EQUIP.M19 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP20 "
                StrSql = StrSql + "ON EQUIP20.EQUIPID=EQUIP.M20 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP21 "
                StrSql = StrSql + "ON EQUIP21.EQUIPID=EQUIP.M21 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP22 "
                StrSql = StrSql + "ON EQUIP22.EQUIPID=EQUIP.M22 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP23 "
                StrSql = StrSql + "ON EQUIP23.EQUIPID=EQUIP.M23 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP24 "
                StrSql = StrSql + "ON EQUIP24.EQUIPID=EQUIP.M24 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP25 "
                StrSql = StrSql + "ON EQUIP25.EQUIPID=EQUIP.M25 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP26 "
                StrSql = StrSql + "ON EQUIP26.EQUIPID=EQUIP.M26 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP27 "
                StrSql = StrSql + "ON EQUIP27.EQUIPID=EQUIP.M27 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP28 "
                StrSql = StrSql + "ON EQUIP28.EQUIPID=EQUIP.M28 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP29 "
                StrSql = StrSql + "ON EQUIP29.EQUIPID=EQUIP.M29 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP30 "
                StrSql = StrSql + "ON EQUIP30.EQUIPID=EQUIP.M30 "
                StrSql = StrSql + "INNER JOIN PREFERENCES  PREF ON "
                StrSql = StrSql + "PREF.CASEID=EQUIP.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIPMENTCOST EQCOS "
                StrSql = StrSql + "ON EQCOS.CASEID=EQUIP.CASEID "
                StrSql = StrSql + "INNER JOIN DEPRECIATION DEPC "
                StrSql = StrSql + "ON DEPC.CASEID=EQUIP.CASEID "
                StrSql = StrSql + "INNER JOIN TOTAL "
                StrSql = StrSql + "ON TOTAL.CASEID=EQUIP.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIPMENTNUMBER EQNUM "
                StrSql = StrSql + "ON EQNUM.CASEID=EQUIP.CASEID "
                'Changes For Econ3
                StrSql = StrSql + "INNER JOIN RESULTSCOST2 RC "
                StrSql = StrSql + "ON RC.CASEID=EQUIP.CASEID "
                'End
                StrSql = StrSql + "WHERE EQUIP.CASEID=" + CaseId.ToString() + ""

                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetAssetC:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetAssetS(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT EQUIP.CASEID,  "
                StrSql = StrSql + "PREF.TITLE2 AS ASSESTCOSTUNIT, "
                StrSql = StrSql + "PREF.UNITS, "
                StrSql = StrSql + "PREF.CURR, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN 'SQUARE FEET'  ELSE 'SQUARE METERS' END AS PLANTAREAUNIT, "
                StrSql = StrSql + "(EQUIP1.COST *  PREF.CURR) AS ASSETS1, "
                StrSql = StrSql + "(EQUIP2.COST *  PREF.CURR) AS ASSETS2, "
                StrSql = StrSql + "(EQUIP3.COST *  PREF.CURR) AS ASSETS3, "
                StrSql = StrSql + "(EQUIP4.COST *  PREF.CURR) AS ASSETS4, "
                StrSql = StrSql + "(EQUIP5.COST *  PREF.CURR) AS ASSETS5, "
                StrSql = StrSql + "(EQUIP6.COST *  PREF.CURR) AS ASSETS6, "
                StrSql = StrSql + "(EQUIP7.COST *  PREF.CURR) AS ASSETS7, "
                StrSql = StrSql + "(EQUIP8.COST *  PREF.CURR) AS ASSETS8, "
                StrSql = StrSql + "(EQUIP9.COST *  PREF.CURR) AS ASSETS9, "
                StrSql = StrSql + "(EQUIP10.COST *  PREF.CURR) AS ASSETS10, "
                StrSql = StrSql + "(EQUIP11.COST *  PREF.CURR) AS ASSETS11, "
                StrSql = StrSql + "(EQUIP12.COST *  PREF.CURR) AS ASSETS12, "
                StrSql = StrSql + "(EQUIP13.COST *  PREF.CURR) AS ASSETS13, "
                StrSql = StrSql + "(EQUIP14.COST *  PREF.CURR) AS ASSETS14, "
                StrSql = StrSql + "(EQUIP15.COST *  PREF.CURR) AS ASSETS15, "
                StrSql = StrSql + "(EQUIP16.COST *  PREF.CURR) AS ASSETS16, "
                StrSql = StrSql + "(EQUIP17.COST *  PREF.CURR) AS ASSETS17, "
                StrSql = StrSql + "(EQUIP18.COST *  PREF.CURR) AS ASSETS18, "
                StrSql = StrSql + "(EQUIP19.COST *  PREF.CURR) AS ASSETS19, "
                StrSql = StrSql + "(EQUIP20.COST *  PREF.CURR) AS ASSETS20, "
                StrSql = StrSql + "(EQUIP21.COST *  PREF.CURR) AS ASSETS21, "
                StrSql = StrSql + "(EQUIP22.COST *  PREF.CURR) AS ASSETS22, "
                StrSql = StrSql + "(EQUIP23.COST *  PREF.CURR) AS ASSETS23, "
                StrSql = StrSql + "(EQUIP24.COST *  PREF.CURR) AS ASSETS24, "
                StrSql = StrSql + "(EQUIP25.COST *  PREF.CURR) AS ASSETS25, "
                StrSql = StrSql + "(EQUIP26.COST *  PREF.CURR) AS ASSETS26, "
                StrSql = StrSql + "(EQUIP27.COST *  PREF.CURR) AS ASSETS27, "
                StrSql = StrSql + "(EQUIP28.COST *  PREF.CURR) AS ASSETS28, "
                StrSql = StrSql + "(EQUIP29.COST *  PREF.CURR) AS ASSETS29, "
                StrSql = StrSql + "(EQUIP30.COST *  PREF.CURR) AS ASSETS30, "
                StrSql = StrSql + "(EQCOS.M1*  PREF.CURR) AS ASSETP1, "
                StrSql = StrSql + "(EQCOS.M2*  PREF.CURR) AS ASSETP2, "
                StrSql = StrSql + "(EQCOS.M3*  PREF.CURR) AS ASSETP3, "
                StrSql = StrSql + "(EQCOS.M4*  PREF.CURR) AS ASSETP4, "
                StrSql = StrSql + "(EQCOS.M5*  PREF.CURR) AS ASSETP5, "
                StrSql = StrSql + "(EQCOS.M6*  PREF.CURR) AS ASSETP6, "
                StrSql = StrSql + "(EQCOS.M7*  PREF.CURR) AS ASSETP7, "
                StrSql = StrSql + "(EQCOS.M8*  PREF.CURR) AS ASSETP8, "
                StrSql = StrSql + "(EQCOS.M9*  PREF.CURR) AS ASSETP9, "
                StrSql = StrSql + "(EQCOS.M10*  PREF.CURR) AS ASSETP10, "
                StrSql = StrSql + "(EQCOS.M11*  PREF.CURR) AS ASSETP11, "
                StrSql = StrSql + "(EQCOS.M12*  PREF.CURR) AS ASSETP12, "
                StrSql = StrSql + "(EQCOS.M13*  PREF.CURR) AS ASSETP13, "
                StrSql = StrSql + "(EQCOS.M14*  PREF.CURR) AS ASSETP14, "
                StrSql = StrSql + "(EQCOS.M15*  PREF.CURR) AS ASSETP15, "
                StrSql = StrSql + "(EQCOS.M16*  PREF.CURR) AS ASSETP16, "
                StrSql = StrSql + "(EQCOS.M17*  PREF.CURR) AS ASSETP17, "
                StrSql = StrSql + "(EQCOS.M18*  PREF.CURR) AS ASSETP18, "
                StrSql = StrSql + "(EQCOS.M19*  PREF.CURR) AS ASSETP19, "
                StrSql = StrSql + "(EQCOS.M20*  PREF.CURR) AS ASSETP20, "
                StrSql = StrSql + "(EQCOS.M21*  PREF.CURR) AS ASSETP21, "
                StrSql = StrSql + "(EQCOS.M22*  PREF.CURR) AS ASSETP22, "
                StrSql = StrSql + "(EQCOS.M23*  PREF.CURR) AS ASSETP23, "
                StrSql = StrSql + "(EQCOS.M24*  PREF.CURR) AS ASSETP24, "
                StrSql = StrSql + "(EQCOS.M25*  PREF.CURR) AS ASSETP25, "
                StrSql = StrSql + "(EQCOS.M26*  PREF.CURR) AS ASSETP26, "
                StrSql = StrSql + "(EQCOS.M27*  PREF.CURR) AS ASSETP27, "
                StrSql = StrSql + "(EQCOS.M28*  PREF.CURR) AS ASSETP28, "
                StrSql = StrSql + "(EQCOS.M29*  PREF.CURR) AS ASSETP29, "
                StrSql = StrSql + "(EQCOS.M30*  PREF.CURR) AS ASSETP30, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP1.EQUIPID )EQUIPDES1, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP2.EQUIPID )EQUIPDES2, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP3.EQUIPID )EQUIPDES3, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP4.EQUIPID )EQUIPDES4, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP5.EQUIPID )EQUIPDES5, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP6.EQUIPID )EQUIPDES6, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP7.EQUIPID )EQUIPDES7, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP8.EQUIPID )EQUIPDES8, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP9.EQUIPID )EQUIPDES9, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP10.EQUIPID )EQUIPDES10,"
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP11.EQUIPID )EQUIPDES11, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP12.EQUIPID )EQUIPDES12, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP13.EQUIPID )EQUIPDES13, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP14.EQUIPID )EQUIPDES14,"
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP15.EQUIPID )EQUIPDES15,"
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP16.EQUIPID )EQUIPDES16, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP17.EQUIPID )EQUIPDES17, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP18.EQUIPID )EQUIPDES18, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP19.EQUIPID )EQUIPDES19, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP20.EQUIPID )EQUIPDES20, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP21.EQUIPID )EQUIPDES21, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP22.EQUIPID )EQUIPDES22, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP23.EQUIPID )EQUIPDES23, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP24.EQUIPID )EQUIPDES24, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP25.EQUIPID )EQUIPDES25, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP26.EQUIPID )EQUIPDES26, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP27.EQUIPID )EQUIPDES27, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP28.EQUIPID )EQUIPDES28, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP29.EQUIPID )EQUIPDES29, "
                StrSql = StrSql + "(SELECT EQUIPDE1||' '||EQUIPDE2 FROM EQUIPMENT2 WHERE EQUIPID=EQUIP30.EQUIPID )EQUIPDES30, "
                StrSql = StrSql + "EQNUM.D1 AS DEPRES1, "
                StrSql = StrSql + "EQNUM.D2 AS DEPRES2, "
                StrSql = StrSql + "EQNUM.D3 AS DEPRES3, "
                StrSql = StrSql + "EQNUM.D4 AS DEPRES4, "
                StrSql = StrSql + "EQNUM.D5 AS DEPRES5, "
                StrSql = StrSql + "EQNUM.D6 AS DEPRES6, "
                StrSql = StrSql + "EQNUM.D7 AS DEPRES7, "
                StrSql = StrSql + "EQNUM.D8 AS DEPRES8, "
                StrSql = StrSql + "EQNUM.D9 AS DEPRES9, "
                StrSql = StrSql + "EQNUM.D10 AS DEPRES10, "
                StrSql = StrSql + "EQNUM.D11 AS DEPRES11, "
                StrSql = StrSql + "EQNUM.D12 AS DEPRES12, "
                StrSql = StrSql + "EQNUM.D13 AS DEPRES13, "
                StrSql = StrSql + "EQNUM.D14 AS DEPRES14, "
                StrSql = StrSql + "EQNUM.D15 AS DEPRES15, "
                StrSql = StrSql + "EQNUM.D16 AS DEPRES16, "
                StrSql = StrSql + "EQNUM.D17 AS DEPRES17, "
                StrSql = StrSql + "EQNUM.D18 AS DEPRES18, "
                StrSql = StrSql + "EQNUM.D19 AS DEPRES19, "
                StrSql = StrSql + "EQNUM.D20 AS DEPRES20, "
                StrSql = StrSql + "EQNUM.D21 AS DEPRES21, "
                StrSql = StrSql + "EQNUM.D22 AS DEPRES22, "
                StrSql = StrSql + "EQNUM.D23 AS DEPRES23, "
                StrSql = StrSql + "EQNUM.D24 AS DEPRES24, "
                StrSql = StrSql + "EQNUM.D25 AS DEPRES25, "
                StrSql = StrSql + "EQNUM.D26 AS DEPRES26, "
                StrSql = StrSql + "EQNUM.D27 AS DEPRES27, "
                StrSql = StrSql + "EQNUM.D28 AS DEPRES28, "
                StrSql = StrSql + "EQNUM.D29 AS DEPRES29, "
                StrSql = StrSql + "EQNUM.D30 AS DEPRES30, "
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
                StrSql = StrSql + "EQNUM.M30 AS NUM30, "
                'Changes For Econ3
                StrSql = StrSql + "(RC.ICSE1 *  PREF.CURR) AS ICSE1, "
                StrSql = StrSql + "(RC.ICSE2 *  PREF.CURR) AS ICSE2, "
                StrSql = StrSql + "(RC.ICSE3 *  PREF.CURR) AS ICSE3, "
                StrSql = StrSql + "(RC.ICSE4 *  PREF.CURR) AS ICSE4, "
                StrSql = StrSql + "(RC.ICSE5 *  PREF.CURR) AS ICSE5, "
                StrSql = StrSql + "(RC.ICSE6 *  PREF.CURR) AS ICSE6, "
                StrSql = StrSql + "(RC.ICSE7 *  PREF.CURR) AS ICSE7, "
                StrSql = StrSql + "(RC.ICSE8 *  PREF.CURR) AS ICSE8, "
                StrSql = StrSql + "(RC.ICSE9 *  PREF.CURR) AS ICSE9, "
                StrSql = StrSql + "(RC.ICSE10 *  PREF.CURR) AS ICSE10, "
                StrSql = StrSql + "(RC.ICSE11 *  PREF.CURR) AS ICSE11, "
                StrSql = StrSql + "(RC.ICSE12 *  PREF.CURR) AS ICSE12, "
                StrSql = StrSql + "(RC.ICSE13 *  PREF.CURR) AS ICSE13, "
                StrSql = StrSql + "(RC.ICSE14 *  PREF.CURR) AS ICSE14, "
                StrSql = StrSql + "(RC.ICSE15 *  PREF.CURR) AS ICSE15, "
                StrSql = StrSql + "(RC.ICSE16 *  PREF.CURR) AS ICSE16, "
                StrSql = StrSql + "(RC.ICSE17 *  PREF.CURR) AS ICSE17, "
                StrSql = StrSql + "(RC.ICSE18 *  PREF.CURR) AS ICSE18, "
                StrSql = StrSql + "(RC.ICSE19 *  PREF.CURR) AS ICSE19, "
                StrSql = StrSql + "(RC.ICSE20 *  PREF.CURR) AS ICSE20, "
                StrSql = StrSql + "(RC.ICSE21 *  PREF.CURR) AS ICSE21, "
                StrSql = StrSql + "(RC.ICSE22 *  PREF.CURR) AS ICSE22, "
                StrSql = StrSql + "(RC.ICSE23 *  PREF.CURR) AS ICSE23, "
                StrSql = StrSql + "(RC.ICSE24 *  PREF.CURR) AS ICSE24, "
                StrSql = StrSql + "(RC.ICSE25 *  PREF.CURR) AS ICSE25, "
                StrSql = StrSql + "(RC.ICSE26 *  PREF.CURR) AS ICSE26, "
                StrSql = StrSql + "(RC.ICSE27 *  PREF.CURR) AS ICSE27, "
                StrSql = StrSql + "(RC.ICSE28 *  PREF.CURR) AS ICSE28, "
                StrSql = StrSql + "(RC.ICSE29 *  PREF.CURR) AS ICSE29, "
                StrSql = StrSql + "(RC.ICSE30 *  PREF.CURR) AS ICSE30, "

                StrSql = StrSql + "(RC.DCSE1 *  PREF.CURR) AS DCSE1, "
                StrSql = StrSql + "(RC.DCSE2 *  PREF.CURR) AS DCSE2, "
                StrSql = StrSql + "(RC.DCSE3 *  PREF.CURR) AS DCSE3, "
                StrSql = StrSql + "(RC.DCSE4 *  PREF.CURR) AS DCSE4, "
                StrSql = StrSql + "(RC.DCSE5 *  PREF.CURR) AS DCSE5, "
                StrSql = StrSql + "(RC.DCSE6 *  PREF.CURR) AS DCSE6, "
                StrSql = StrSql + "(RC.DCSE7 *  PREF.CURR) AS DCSE7, "
                StrSql = StrSql + "(RC.DCSE8 *  PREF.CURR) AS DCSE8, "
                StrSql = StrSql + "(RC.DCSE9 *  PREF.CURR) AS DCSE9, "
                StrSql = StrSql + "(RC.DCSE10 *  PREF.CURR) AS DCSE10, "
                StrSql = StrSql + "(RC.DCSE11 *  PREF.CURR) AS DCSE11, "
                StrSql = StrSql + "(RC.DCSE12 *  PREF.CURR) AS DCSE12, "
                StrSql = StrSql + "(RC.DCSE13 *  PREF.CURR) AS DCSE13, "
                StrSql = StrSql + "(RC.DCSE14 *  PREF.CURR) AS DCSE14, "
                StrSql = StrSql + "(RC.DCSE15 *  PREF.CURR) AS DCSE15, "
                StrSql = StrSql + "(RC.DCSE16 *  PREF.CURR) AS DCSE16, "
                StrSql = StrSql + "(RC.DCSE17 *  PREF.CURR) AS DCSE17, "
                StrSql = StrSql + "(RC.DCSE18 *  PREF.CURR) AS DCSE18, "
                StrSql = StrSql + "(RC.DCSE19 *  PREF.CURR) AS DCSE19, "
                StrSql = StrSql + "(RC.DCSE20 *  PREF.CURR) AS DCSE20, "
                StrSql = StrSql + "(RC.DCSE21 *  PREF.CURR) AS DCSE21, "
                StrSql = StrSql + "(RC.DCSE22 *  PREF.CURR) AS DCSE22, "
                StrSql = StrSql + "(RC.DCSE23 *  PREF.CURR) AS DCSE23, "
                StrSql = StrSql + "(RC.DCSE24 *  PREF.CURR) AS DCSE24, "
                StrSql = StrSql + "(RC.DCSE25 *  PREF.CURR) AS DCSE25, "
                StrSql = StrSql + "(RC.DCSE26 *  PREF.CURR) AS DCSE26, "
                StrSql = StrSql + "(RC.DCSE27 *  PREF.CURR) AS DCSE27, "
                StrSql = StrSql + "(RC.DCSE28 *  PREF.CURR) AS DCSE28, "
                StrSql = StrSql + "(RC.DCSE29 *  PREF.CURR) AS DCSE29, "
                StrSql = StrSql + "(RC.DCSE30 *  PREF.CURR) AS DCSE30, "

                StrSql = StrSql + "(RC.INVESTTOTALSE *  PREF.CURR) AS INVESTTOTALSE, "
                StrSql = StrSql + "(RC.DEPRETOTALSE *  PREF.CURR) AS DEPRETOTALSE "
                'End Econ3


                StrSql = StrSql + "FROM EQUIPMENT2TYPE EQUIP "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP1 "
                StrSql = StrSql + "ON EQUIP1.EQUIPID=EQUIP.M1 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP2 "
                StrSql = StrSql + "ON EQUIP2.EQUIPID=EQUIP.M2 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP3 "
                StrSql = StrSql + "ON EQUIP3.EQUIPID=EQUIP.M3 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP4 "
                StrSql = StrSql + "ON EQUIP4.EQUIPID=EQUIP.M4 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP5 "
                StrSql = StrSql + "ON EQUIP5.EQUIPID=EQUIP.M5 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP6 "
                StrSql = StrSql + "ON EQUIP6.EQUIPID=EQUIP.M6 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP7 "
                StrSql = StrSql + "ON EQUIP7.EQUIPID=EQUIP.M7 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP8 "
                StrSql = StrSql + "ON EQUIP8.EQUIPID=EQUIP.M8 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP9 "
                StrSql = StrSql + "ON EQUIP9.EQUIPID=EQUIP.M9 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP10 "
                StrSql = StrSql + "ON EQUIP10.EQUIPID=EQUIP.M10 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP11 "
                StrSql = StrSql + "ON EQUIP11.EQUIPID=EQUIP.M11 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP12 "
                StrSql = StrSql + "ON EQUIP12.EQUIPID=EQUIP.M12 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP13 "
                StrSql = StrSql + "ON EQUIP13.EQUIPID=EQUIP.M13 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP14 "
                StrSql = StrSql + "ON EQUIP14.EQUIPID=EQUIP.M14 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP15 "
                StrSql = StrSql + "ON EQUIP15.EQUIPID=EQUIP.M15 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP16 "
                StrSql = StrSql + "ON EQUIP16.EQUIPID=EQUIP.M16 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP17 "
                StrSql = StrSql + "ON EQUIP17.EQUIPID=EQUIP.M17 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP18 "
                StrSql = StrSql + "ON EQUIP18.EQUIPID=EQUIP.M18 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP19 "
                StrSql = StrSql + "ON EQUIP19.EQUIPID=EQUIP.M19 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP20 "
                StrSql = StrSql + "ON EQUIP20.EQUIPID=EQUIP.M20 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP21 "
                StrSql = StrSql + "ON EQUIP21.EQUIPID=EQUIP.M21 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP22 "
                StrSql = StrSql + "ON EQUIP22.EQUIPID=EQUIP.M22 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP23 "
                StrSql = StrSql + "ON EQUIP23.EQUIPID=EQUIP.M23 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP24 "
                StrSql = StrSql + "ON EQUIP24.EQUIPID=EQUIP.M24 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP25 "
                StrSql = StrSql + "ON EQUIP25.EQUIPID=EQUIP.M25 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP26 "
                StrSql = StrSql + "ON EQUIP26.EQUIPID=EQUIP.M26 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP27 "
                StrSql = StrSql + "ON EQUIP27.EQUIPID=EQUIP.M27 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP28 "
                StrSql = StrSql + "ON EQUIP28.EQUIPID=EQUIP.M28 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP29 "
                StrSql = StrSql + "ON EQUIP29.EQUIPID=EQUIP.M29 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP30 "
                StrSql = StrSql + "ON EQUIP30.EQUIPID=EQUIP.M30 "
                StrSql = StrSql + "INNER JOIN PREFERENCES  PREF ON "
                StrSql = StrSql + "PREF.CASEID = EQUIP.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2COST EQCOS "
                StrSql = StrSql + "ON EQCOS.CASEID=EQUIP.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2NUMBER EQNUM "
                StrSql = StrSql + "ON EQNUM.CASEID=EQUIP.CASEID "
                'Changes For Econ3
                StrSql = StrSql + "INNER JOIN RESULTSCOST2 RC "
                StrSql = StrSql + "ON RC.CASEID=EQUIP.CASEID "
                'END econ3
                StrSql = StrSql + "WHERE EQUIP.CASEID=" + CaseId.ToString() + ""

                Dts = odbUtil.FillDataSet(StrSql, MoldE2Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("MoldE2GetData:GetAssetS:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region
    End Class
End Class
