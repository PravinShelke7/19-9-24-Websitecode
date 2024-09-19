Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System

Public Class SMed1GetData1
    Public Class Selectdata
        Dim MedSustain1Connection As String = System.Configuration.ConfigurationManager.AppSettings("MedSustain1ConnectionString")
        Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
        Dim MedEcon1Connection As String = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
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
                StrSql = StrSql + "SERVICES.SERVICEID, "
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
                StrSql = StrSql + "AND SERVICES.SERVICEDE='MedS1' "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
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
                Throw New Exception("SMed1GetData:GetSelectedUserDetails:" + ex.Message.ToString())
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
                StrSql = StrSql + "AND SERVICEID IS NULL "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetTotalCaseCount:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

#End Region

#Region "Supporting Assumptions Pages"

        Public Function GetBCaseDetails() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES FROM BASECASES "
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID "

                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPCaseDetails(ByVal USERID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE USERID ='" + USERID.ToUpper().ToString() + "' "
                StrSql = StrSql + "AND SERVICEID IS NULL "
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID "

                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetExtrusionDetails:" + ex.Message.ToString())
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
                StrSql = StrSql + "AND SERVICES.SERVICEDE IN ('MedE1','MedE2','MedS1','MedS2') "
                StrSql = StrSql + "AND UPPER(USERPERMISSIONS.USERROLE)='READWRITE' "
                StrSql = StrSql + "GROUP BY USERS.USERID,USERS.USERNAME "
                StrSql = StrSql + "ORDER BY USERS.USERNAME "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetUserCompanyUsers:" + ex.Message.ToString())
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

                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetCaseDetails:" + ex.Message.ToString())
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
                StrSql = StrSql + "AND SERVICEID IS NULL "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetCases:" + ex.Message.ToString())
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
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID"
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetDiscMaterials(ByVal MatDisId As Integer, ByVal MatDe1 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select matDISid, matDISde1  "
                StrSql = StrSql + "FROM MaterialDIS "
                StrSql = StrSql + "WHERE matDISid = CASE WHEN " + MatDisId.ToString() + " = -1 THEN "
                StrSql = StrSql + "matDISid "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + MatDisId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(matDISde1),'#') LIKE '" + MatDe1.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  matDISde1"
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetDiscMaterials:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPref(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID,  "
                StrSql = StrSql + "UNITS, "
                StrSql = StrSql + "INVENTORYTYPE, "
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
                StrSql = StrSql + "CONVGALLON, "
                StrSql = StrSql + "EFFDATE, "
                StrSql = StrSql + "TO_CHAR(EFFDATE,'MON DD,YYYY')AS EDATE, "
                StrSql = StrSql + "TITLE10, "
                StrSql = StrSql + "TITLE11, "
                StrSql = StrSql + "TITLE12, "
                StrSql = StrSql + "ERGYCALC, "
                StrSql = StrSql + "ISDSCTNEW, "
                StrSql = StrSql + "DFLAG "
                StrSql = StrSql + "FROM PREFERENCES "
                StrSql = StrSql + "WHERE CASEID = " + CaseId.ToString() + " "


                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetPref:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetShipingSelector(ByVal shipId As Integer, ByVal shipDes As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                'StrSql = "select shipid, shipdes  "
                'select shipid, shipdes from Shippingselector where ShipId<>2 ORDER BY shipid
                StrSql = "select shipid, shipdes  "
                StrSql = StrSql + "FROM SUSTAIN.Shippingselector "
                StrSql = StrSql + "WHERE ShipId<>2 and shipid = CASE WHEN " + shipId.ToString() + " = -1 THEN "
                StrSql = StrSql + "shipid "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + shipId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(shipdes),'#') LIKE '" + shipDes.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  shipid"
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetShipingSelector:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSustainableMaterial(ByVal TYPEID As Integer, ByVal TYPE As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select TYPEID, TYPE  "
                StrSql = StrSql + "FROM HEALTHANDSAFETY "
                StrSql = StrSql + "WHERE TYPEID = CASE WHEN " + TYPEID.ToString() + " = -1 THEN "
                StrSql = StrSql + "TYPEID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + TYPEID.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(TYPE),'#') LIKE '" + TYPE.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  TYPEID"
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetSustainableMaterial:" + ex.Message.ToString())
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
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetPallets:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetDept(ByVal ProcId As Integer, ByVal ProcDe1 As String, ByVal ProcDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim CaseId As String = HttpContext.Current.Session("SMed1CaseId").ToString()
            Try
                StrSql = "SELECT PROCID,(PROCDE1||'  '||PROCDE2) AS PROCDE,PROCDE1,PROCDE2  "
                StrSql = StrSql + "FROM MED1.PROCESS "
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

                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetDept:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetDept(ByVal ProcId As Integer, ByVal ProcDe1 As String, ByVal ProcDe2 As String, ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PROCID,(PROCDE1||'  '||PROCDE2) AS PROCDE,PROCDE1,PROCDE2  "
                StrSql = StrSql + "FROM MED1.PROCESS "
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

                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetDept:" + ex.Message.ToString())
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

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetDeptPlantConfig:" + ex.Message.ToString())
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
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetSupportEquipment:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetEquipment(ByVal EqId As Integer, ByVal EqDe1 As String, ByVal EqlDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select equipID,equipDE1,equipDE2,(equipDE1 || ' ' || equipDE2) equipDES,replace((equipDE1 || ' ' ||  equipDE2),Chr(34) ,'##') equipDES1 "
                StrSql = StrSql + "from MED1.EQUIPMENT  "
                StrSql = StrSql + "WHERE EQUIPID = CASE WHEN " + EqId.ToString() + " = -1 THEN "
                StrSql = StrSql + "EQUIPID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + EqId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(equipDE1),'#') LIKE '" + EqDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(equipDE2),'#') LIKE '" + EqlDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY equipDE1"
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetEquipment:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetMaterials(ByVal MatId As Integer, ByVal MatDe1 As String, ByVal MatDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                'StrSql = "SELECT MATID, (MATDE1||'  '||MATDE2)MATDES,MATDE1,MATDE2  "
                'StrSql = StrSql + "FROM MATERIALS "
                'StrSql = StrSql + "WHERE MATID = CASE WHEN " + MatId.ToString() + " = -1 THEN "
                'StrSql = StrSql + "MATID "
                'StrSql = StrSql + "ELSE "
                'StrSql = StrSql + "" + MatId.ToString() + " "
                'StrSql = StrSql + "END "
                'StrSql = StrSql + "AND NVL(UPPER(MATDE1),'#') LIKE '" + MatDe1.ToUpper() + "%' "
                'StrSql = StrSql + "AND NVL(UPPER(MATDE2),'#') LIKE '" + MatDe2.ToUpper() + "%' "
                'StrSql = StrSql + "ORDER BY  MATDE1"
                StrSql = "SELECT MATERIALS.MATID, (MATDE1||'  '||MATDE2)MATDES,MATDE1,MATDE2,GRADE.GRADENAME,GRADE.GRADEID,GRADE.WEIGHT,MATERIALS.SG  "
                StrSql = StrSql + "FROM MATERIALS "
                StrSql = StrSql + "INNER JOIN MATGRADE MG ON MG.MATID=MATERIALS.MATID  "
                StrSql = StrSql + "INNER JOIN GRADE ON GRADE.GRADEID=MG.GRADEID AND GRADE.ISDEFAULT='Y' "
                StrSql = StrSql + "WHERE MATERIALS.MATID = CASE WHEN " + MatId.ToString() + " = -1 THEN "
                StrSql = StrSql + "MATERIALS.MATID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + MatId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(MATDE1),'#') LIKE '" + MatDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(MATDE2),'#') LIKE '" + MatDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  MATDE1"
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetMaterials:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetProductFormtByID(ByVal CaseID1 As Integer, ByVal CaseID2 As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  CASEID,M1, "
                StrSql = StrSql + "CASE WHEN M1=1 or M1=17 THEN 'msi' ELSE 'units' END UNITS "
                StrSql = StrSql + "FROM PRODUCTFORMATIN "
                StrSql = StrSql + "WHERE CASEID IN( " + CaseID1.ToString() + "," + CaseID2.ToString() + ")"
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetProductFormtByID:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetLCIDataByMatAndDate(ByVal InvId As String, ByVal matId As String, ByVal caseId As String, ByVal effDate As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MATA.JOULE/PREF.CONVWT AS EnergyVal,MATA.PRICE AS Co2Val,  "
                StrSql = StrSql + "(Water*PREF.CONVGALLON/PREF.CONVWT) WaterVal "
                StrSql = StrSql + "FROM SUSTAIN.MATERIALSARCH MATA "
                StrSql = StrSql + "Inner Join PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID= " + caseId + " "
                StrSql = StrSql + "WHERE MATA.MATID= " + matId + " "
                StrSql = StrSql + "AND MATA.INVENTORYTYPE= " + InvId + " "
                StrSql = StrSql + "AND MATA.EFFDATE=TO_DATE('" + effDate + "','mm/dd/yyyy') "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetLCIDataByMatAndDate:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetLatestLCIDate(ByVal InvId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  TO_CHAR(MAX(EFFDATE),'MON DD,YYYY')AS EDATE FROM SUSTAIN.MATERIALSARCH WHERE INVENTORYTYPE= " + InvId
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetInventoryDetail:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetInventoryDetail(ByVal InvId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT * FROM INVENTORYTYPE "
                StrSql = StrSql + "WHERE INVENTORYID = CASE WHEN " + InvId.ToString() + " = -1 THEN "
                StrSql = StrSql + "INVENTORYID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + InvId.ToString() + " "
                StrSql = StrSql + "END "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetInventoryDetail:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompleteMaterials() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MATID FROM MATERIALS "
                StrSql = StrSql + "WHERE MATID <>0 "
                StrSql = StrSql + "ORDER BY MATDE1"
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetCompleteMaterials:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetDiscretedMaterialTotal(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select caseID,thick,sg,wtPERarea, discreteWT from Total WHERE caseID= " + CaseId.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetDiscretedMaterialTotal:" + ex.Message.ToString())
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
                Throw New Exception("SMed1GetData:GetPersonnelInfo:" + ex.Message.ToString())
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
                StrSql = StrSql + "AND FORMATID <> 17"
                StrSql = StrSql + "ORDER BY  FormatDe1 "

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetProductFormat:" + ex.Message.ToString())
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
                StrSql = StrSql + "'personnelUK' "
                StrSql = StrSql + "WHEN PREF.OCOUNTRY=3 THEN "
                StrSql = StrSql + "'personnelGermany' "
                StrSql = StrSql + "WHEN PREF.OCOUNTRY=4 THEN "
                StrSql = StrSql + "'personnelSKorea' "
                StrSql = StrSql + "END) AS COUNTRY "
                StrSql = StrSql + "FROM PREFERENCES PREF "
                StrSql = StrSql + "WHERE CASEID = " + CaseId.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetEffDate(ByVal Inventorytype As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT TO_CHAR(EFFDATE,'MON DD,YYYY')AS EDATE FROM SUSTAIN.MATERIALSARCH WHERE INVENTORYTYPE=" + Inventorytype + ""
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetEffDate:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetLCIEffDates() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                'StrSql = "SELECT DISTINCT TO_CHAR(EFFDATE,'MON DD,YYYY')AS EDATE FROM MATERIALSARCH WHERE INVENTORYTYPE in (Select InventoryID from INVENTORYTYPE )"
                StrSql = "select TO_CHAR(EFFDATE,'MON DD,YYYY')EDATE from SUSTAIN.MATERIALSARCH,InventoryType Group By EffDate"
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetEffDate:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetLCI() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT INVENTORYID, INVENTORY FROM INVENTORYTYPE  ORDER BY INVENTORY DESC"
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetLCI:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetLCI(ByVal Inventorytype As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT INVENTORYID, INVENTORY FROM INVENTORYTYPE WHERE INVENTORYID=" + Inventorytype + ""
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetLCI:" + ex.Message.ToString())
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
                Throw New Exception("SMed1GetData:GetConversionFactor:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function



#End Region

#Region "Assumptions Pages"
        Public Function GetExtrusionDetailsPref(ByVal CaseId As Integer) As DataSet
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

                StrSql = StrSql + "(MATERGY.M1/PREF.CONVWT) AS ERGYP1, "
                StrSql = StrSql + "(MATERGY.M2/PREF.CONVWT) AS ERGYP2, "
                StrSql = StrSql + "(MATERGY.M3/PREF.CONVWT) AS ERGYP3, "
                StrSql = StrSql + "(MATERGY.M4/PREF.CONVWT) AS ERGYP4, "
                StrSql = StrSql + "(MATERGY.M5/PREF.CONVWT) AS ERGYP5, "
                StrSql = StrSql + "(MATERGY.M6/PREF.CONVWT) AS ERGYP6, "
                StrSql = StrSql + "(MATERGY.M7/PREF.CONVWT) AS ERGYP7, "
                StrSql = StrSql + "(MATERGY.M8/PREF.CONVWT) AS ERGYP8, "
                StrSql = StrSql + "(MATERGY.M9/PREF.CONVWT) AS ERGYP9, "
                StrSql = StrSql + "(MATERGY.M10/PREF.CONVWT) AS ERGYP10, "

                StrSql = StrSql + "MAT.S1 AS CO2P1, "
                StrSql = StrSql + "MAT.S2 AS CO2P2, "
                StrSql = StrSql + "MAT.S3 AS CO2P3, "
                StrSql = StrSql + "MAT.S4 AS CO2P4, "
                StrSql = StrSql + "MAT.S5 AS CO2P5, "
                StrSql = StrSql + "MAT.S6 AS CO2P6, "
                StrSql = StrSql + "MAT.S7 AS CO2P7, "
                StrSql = StrSql + "MAT.S8 AS CO2P8, "
                StrSql = StrSql + "MAT.S9 AS CO2P9, "
                StrSql = StrSql + "MAT.S10 AS CO2P10, "

                StrSql = StrSql + "(MATWATERPREF.M1*PREF.CONVGALLON/PREF.CONVWT) AS WATERP1, "
                StrSql = StrSql + "(MATWATERPREF.M2*PREF.CONVGALLON/PREF.CONVWT) AS WATERP2, "
                StrSql = StrSql + "(MATWATERPREF.M3*PREF.CONVGALLON/PREF.CONVWT) AS WATERP3, "
                StrSql = StrSql + "(MATWATERPREF.M4*PREF.CONVGALLON/PREF.CONVWT) AS WATERP4, "
                StrSql = StrSql + "(MATWATERPREF.M5*PREF.CONVGALLON/PREF.CONVWT) AS WATERP5, "
                StrSql = StrSql + "(MATWATERPREF.M6*PREF.CONVGALLON/PREF.CONVWT) AS WATERP6, "
                StrSql = StrSql + "(MATWATERPREF.M7*PREF.CONVGALLON/PREF.CONVWT) AS WATERP7, "
                StrSql = StrSql + "(MATWATERPREF.M8*PREF.CONVGALLON/PREF.CONVWT) AS WATERP8, "
                StrSql = StrSql + "(MATWATERPREF.M9*PREF.CONVGALLON/PREF.CONVWT) AS WATERP9, "
                StrSql = StrSql + "(MATWATERPREF.M10*PREF.CONVGALLON/PREF.CONVWT) AS WATERP10, "

                StrSql = StrSql + "(MATSHIP.M1*PREF.CONVTHICK3) AS SHIPP1, "
                StrSql = StrSql + "(MATSHIP.M2*PREF.CONVTHICK3) AS SHIPP2, "
                StrSql = StrSql + "(MATSHIP.M3*PREF.CONVTHICK3) AS SHIPP3, "
                StrSql = StrSql + "(MATSHIP.M4*PREF.CONVTHICK3) AS SHIPP4, "
                StrSql = StrSql + "(MATSHIP.M5*PREF.CONVTHICK3) AS SHIPP5, "
                StrSql = StrSql + "(MATSHIP.M6*PREF.CONVTHICK3) AS SHIPP6, "
                StrSql = StrSql + "(MATSHIP.M7*PREF.CONVTHICK3) AS SHIPP7, "
                StrSql = StrSql + "(MATSHIP.M8*PREF.CONVTHICK3) AS SHIPP8, "
                StrSql = StrSql + "(MATSHIP.M9*PREF.CONVTHICK3) AS SHIPP9, "
                StrSql = StrSql + "(MATSHIP.M10*PREF.CONVTHICK3) AS SHIPP10, "

                StrSql = StrSql + "MAT.RE1 AS RECOP1, "
                StrSql = StrSql + "MAT.RE2 AS RECOP2, "
                StrSql = StrSql + "MAT.RE3 AS RECOP3, "
                StrSql = StrSql + "MAT.RE4 AS RECOP4, "
                StrSql = StrSql + "MAT.RE5 AS RECOP5, "
                StrSql = StrSql + "MAT.RE6 AS RECOP6, "
                StrSql = StrSql + "MAT.RE7 AS RECOP7, "
                StrSql = StrSql + "MAT.RE8 AS RECOP8, "
                StrSql = StrSql + "MAT.RE9 AS RECOP9, "
                StrSql = StrSql + "MAT.RE10 AS RECOP10, "

                StrSql = StrSql + "MAT.OSH1 AS SUSP1, "
                StrSql = StrSql + "MAT.OSH2 AS SUSP2, "
                StrSql = StrSql + "MAT.OSH3 AS SUSP3, "
                StrSql = StrSql + "MAT.OSH4 AS SUSP4, "
                StrSql = StrSql + "MAT.OSH5 AS SUSP5, "
                StrSql = StrSql + "MAT.OSH6 AS SUSP6, "
                StrSql = StrSql + "MAT.OSH7 AS SUSP7, "
                StrSql = StrSql + "MAT.OSH8 AS SUSP8, "
                StrSql = StrSql + "MAT.OSH9 AS SUSP9, "
                StrSql = StrSql + "MAT.OSH10 AS SUSP10, "

                StrSql = StrSql + "MAT.POC1 AS PCRECP1, "
                StrSql = StrSql + "MAT.POC2 AS PCRECP2, "
                StrSql = StrSql + "MAT.POC3 AS PCRECP3, "
                StrSql = StrSql + "MAT.POC4 AS PCRECP4, "
                StrSql = StrSql + "MAT.POC5 AS PCRECP5, "
                StrSql = StrSql + "MAT.POC6 AS PCRECP6, "
                StrSql = StrSql + "MAT.POC7 AS PCRECP7, "
                StrSql = StrSql + "MAT.POC8 AS PCRECP8, "
                StrSql = StrSql + "MAT.POC9 AS PCRECP9, "
                StrSql = StrSql + "MAT.POC10 AS PCRECP10, "
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

                StrSql = StrSql + "(MAT.PS1*PREF.CONVWT) AS SHIPUNIT1, "
                StrSql = StrSql + "(MAT.PS2*PREF.CONVWT) AS SHIPUNIT2, "
                StrSql = StrSql + "(MAT.PS3*PREF.CONVWT) AS SHIPUNIT3, "
                StrSql = StrSql + "(MAT.PS4*PREF.CONVWT) AS SHIPUNIT4, "
                StrSql = StrSql + "(MAT.PS5*PREF.CONVWT) AS SHIPUNIT5, "
                StrSql = StrSql + "(MAT.PS6*PREF.CONVWT) AS SHIPUNIT6, "
                StrSql = StrSql + "(MAT.PS7*PREF.CONVWT) AS SHIPUNIT7, "
                StrSql = StrSql + "(MAT.PS8*PREF.CONVWT) AS SHIPUNIT8, "
                StrSql = StrSql + "(MAT.PS9*PREF.CONVWT) AS SHIPUNIT9, "
                StrSql = StrSql + "(MAT.PS10*PREF.CONVWT) AS SHIPUNIT10, "
                StrSql = StrSql + "MAT.SS1 AS SS1, "
                StrSql = StrSql + "MAT.SS2 AS SS2, "
                StrSql = StrSql + "MAT.SS3 AS SS3, "
                StrSql = StrSql + "MAT.SS4 AS SS4, "
                StrSql = StrSql + "MAT.SS5 AS SS5, "
                StrSql = StrSql + "MAT.SS6 AS SS6, "
                StrSql = StrSql + "MAT.SS7 AS SS7, "
                StrSql = StrSql + "MAT.SS8 AS SS8, "
                StrSql = StrSql + "MAT.SS9 AS SS9, "
                StrSql = StrSql + "MAT.SS10 AS SS10, "
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
                StrSql = StrSql + "PREF.CONVAREA, "
                StrSql = StrSql + "PREF.CONVTHICK, "
                StrSql = StrSql + "PREF.TITLE19, "
                StrSql = StrSql + "PREF.TITLE20, "

                StrSql = StrSql + "MAT.DISCMATYN, "
                StrSql = StrSql + "MATDIS.DISID1, "
                StrSql = StrSql + "MATDIS.DISID2, "
                StrSql = StrSql + "MATDIS.DISID3, "
                StrSql = StrSql + "(MATDIS.DISW1*PREF.CONVWT) AS DISWEIGHT1, "
                StrSql = StrSql + "(MATDIS.DISW2*PREF.CONVWT) AS DISWEIGHT2, "
                StrSql = StrSql + "(MATDIS.DISW3*PREF.CONVWT) AS DISWEIGHT3, "
                StrSql = StrSql + "(MATDIS.DISE1/PREF.CONVWT) AS DISERGY1, "
                StrSql = StrSql + "(MATDIS.DISE2/PREF.CONVWT) AS DISERGY2, "
                StrSql = StrSql + "(MATDIS.DISE3/PREF.CONVWT) AS DISERGY3, "
                StrSql = StrSql + "(MATDIS.DISC1) AS DISCO1, "
                StrSql = StrSql + "(MATDIS.DISC2) AS DISCO2, "
                StrSql = StrSql + "(MATDIS.DISC3) AS DISCO3, "

                StrSql = StrSql + "(MATDIS.DISWATER1*PREF.CONVGALLON/PREF.CONVWT) AS DISWATER1, "
                StrSql = StrSql + "(MATDIS.DISWATER2*PREF.CONVGALLON/PREF.CONVWT) AS DISWATER2, "
                StrSql = StrSql + "(MATDIS.DISWATER3*PREF.CONVGALLON/PREF.CONVWT) AS DISWATER3, "
                StrSql = StrSql + "MAT.PLATE, "
                StrSql = StrSql + "MAT.EFFDATE EFFDATEB, "
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

                StrSql = StrSql + "INNER JOIN BARRIERINPUT BI "
                StrSql = StrSql + "ON MAT.CASEID=BI.CASEID "

                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=MAT.CASEID "

                StrSql = StrSql + "INNER JOIN MATENERGYPREF MATERGY "
                StrSql = StrSql + "ON MATERGY.CASEID=MAT.CASEID "

                StrSql = StrSql + "INNER JOIN MATWATERPREF "
                StrSql = StrSql + "ON MATWATERPREF.CASEID=MAT.CASEID "

                StrSql = StrSql + "INNER JOIN MATSHIPPREF MATSHIP "
                StrSql = StrSql + "ON MATSHIP.CASEID=MAT.CASEID "

                StrSql = StrSql + "INNER JOIN MATERIALDISIN MATDIS "
                StrSql = StrSql + "ON MATDIS.CASEID=MAT.CASEID "

                StrSql = StrSql + "WHERE MAT.CASEID =" + CaseId.ToString() + "  ORDER BY MAT.CASEID "


                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetExtrusionDetailsPref:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetExtrusionDetailsSugg(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "MAT.CASEID, "

                StrSql = StrSql + "(MATS1.MATDE1||' '||MATS1.MATDE2)MATS1,  "
                StrSql = StrSql + "(MATS2.MATDE1||' '||MATS2.MATDE2)MATS2, "
                StrSql = StrSql + "(MATS3.MATDE1||' '||MATS3.MATDE2)MATS3, "
                StrSql = StrSql + "(MATS4.MATDE1||' '||MATS4.MATDE2)MATS4, "
                StrSql = StrSql + "(MATS5.MATDE1||' '||MATS5.MATDE2)MATS5, "
                StrSql = StrSql + "(MATS6.MATDE1||' '||MATS6.MATDE2)MATS6, "
                StrSql = StrSql + "(MATS7.MATDE1||' '||MATS7.MATDE2)MATS7, "
                StrSql = StrSql + "(MATS8.MATDE1||' '||MATS8.MATDE2)MATS8, "
                StrSql = StrSql + "(MATS9.MATDE1||' '||MATS9.MATDE2)MATS9, "
                StrSql = StrSql + "(MATS10.MATDE1||' '||MATS10.MATDE2)MATS10, "

                StrSql = StrSql + "(MATS1.ISADJTHICK) ISADJTHICK1,  "
                StrSql = StrSql + "(MATS2.ISADJTHICK) ISADJTHICK2,  "
                StrSql = StrSql + "(MATS3.ISADJTHICK) ISADJTHICK3,  "
                StrSql = StrSql + "(MATS4.ISADJTHICK) ISADJTHICK4,  "
                StrSql = StrSql + "(MATS5.ISADJTHICK) ISADJTHICK5,  "
                StrSql = StrSql + "(MATS6.ISADJTHICK) ISADJTHICK6,  "
                StrSql = StrSql + "(MATS7.ISADJTHICK) ISADJTHICK7,  "
                StrSql = StrSql + "(MATS8.ISADJTHICK) ISADJTHICK8,  "
                StrSql = StrSql + "(MATS9.ISADJTHICK) ISADJTHICK9,  "
                StrSql = StrSql + "(MATS10.ISADJTHICK) ISADJTHICK10,  "

                StrSql = StrSql + "(NVL(MAT1.JOULE,0)/PREF.CONVWT) AS ERGYS1, "
                StrSql = StrSql + "(NVL(MAT2.JOULE,0)/PREF.CONVWT) AS ERGYS2, "
                StrSql = StrSql + "(NVL(MAT3.JOULE,0)/PREF.CONVWT) AS ERGYS3, "
                StrSql = StrSql + "(NVL(MAT4.JOULE,0)/PREF.CONVWT) AS ERGYS4, "
                StrSql = StrSql + "(NVL(MAT5.JOULE,0)/PREF.CONVWT) AS ERGYS5, "
                StrSql = StrSql + "(NVL(MAT6.JOULE,0)/PREF.CONVWT) AS ERGYS6, "
                StrSql = StrSql + "(NVL(MAT7.JOULE,0)/PREF.CONVWT) AS ERGYS7, "
                StrSql = StrSql + "(NVL(MAT8.JOULE,0)/PREF.CONVWT) AS ERGYS8, "
                StrSql = StrSql + "(NVL(MAT9.JOULE,0)/PREF.CONVWT) AS ERGYS9, "
                StrSql = StrSql + "(NVL(MAT10.JOULE,0)/PREF.CONVWT) AS ERGYS10, "

                StrSql = StrSql + "NVL(MAT1.PRICE,0) AS CO2S1, "
                StrSql = StrSql + "NVL(MAT2.PRICE,0) AS CO2S2, "
                StrSql = StrSql + "NVL(MAT3.PRICE,0) AS CO2S3, "
                StrSql = StrSql + "NVL(MAT4.PRICE,0) AS CO2S4, "
                StrSql = StrSql + "NVL(MAT5.PRICE,0) AS CO2S5, "
                StrSql = StrSql + "NVL(MAT6.PRICE,0) AS CO2S6, "
                StrSql = StrSql + "NVL(MAT7.PRICE,0) AS CO2S7, "
                StrSql = StrSql + "NVL(MAT8.PRICE,0) AS CO2S8, "
                StrSql = StrSql + "NVL(MAT9.PRICE,0) AS CO2S9, "
                StrSql = StrSql + "NVL(MAT10.PRICE,0) AS CO2S10, "

                StrSql = StrSql + "(NVL(MAT1.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS1, "
                StrSql = StrSql + "(NVL(MAT2.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS2, "
                StrSql = StrSql + "(NVL(MAT3.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS3, "
                StrSql = StrSql + "(NVL(MAT4.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS4, "
                StrSql = StrSql + "(NVL(MAT5.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS5, "
                StrSql = StrSql + "(NVL(MAT6.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS6, "
                StrSql = StrSql + "(NVL(MAT7.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS7, "
                StrSql = StrSql + "(NVL(MAT8.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS8, "
                StrSql = StrSql + "(NVL(MAT9.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS9, "
                StrSql = StrSql + "(NVL(MAT10.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS10, "

                StrSql = StrSql + "(NVL(MAT1.SHIP,0)*PREF.CONVTHICK3) AS SHIPS1, "
                StrSql = StrSql + "(NVL(MAT2.SHIP,0)*PREF.CONVTHICK3) AS SHIPS2, "
                StrSql = StrSql + "(NVL(MAT3.SHIP,0)*PREF.CONVTHICK3) AS SHIPS3, "
                StrSql = StrSql + "(NVL(MAT4.SHIP,0)*PREF.CONVTHICK3) AS SHIPS4, "
                StrSql = StrSql + "(NVL(MAT5.SHIP,0)*PREF.CONVTHICK3) AS SHIPS5, "
                StrSql = StrSql + "(NVL(MAT6.SHIP,0)*PREF.CONVTHICK3) AS SHIPS6, "
                StrSql = StrSql + "(NVL(MAT7.SHIP,0)*PREF.CONVTHICK3) AS SHIPS7, "
                StrSql = StrSql + "(NVL(MAT8.SHIP,0)*PREF.CONVTHICK3) AS SHIPS8, "
                StrSql = StrSql + "(NVL(MAT9.SHIP,0)*PREF.CONVTHICK3) AS SHIPS9, "
                StrSql = StrSql + "(NVL(MAT10.SHIP,0)*PREF.CONVTHICK3) AS SHIPS10, "

                StrSql = StrSql + "NVL(MAT1.RECOVERY,0) AS RECOS1, "
                StrSql = StrSql + "NVL(MAT2.RECOVERY,0) AS RECOS2, "
                StrSql = StrSql + "NVL(MAT3.RECOVERY,0) AS RECOS3, "
                StrSql = StrSql + "NVL(MAT4.RECOVERY,0) AS RECOS4, "
                StrSql = StrSql + "NVL(MAT5.RECOVERY,0) AS RECOS5, "
                StrSql = StrSql + "NVL(MAT6.RECOVERY,0) AS RECOS6, "
                StrSql = StrSql + "NVL(MAT7.RECOVERY,0) AS RECOS7, "
                StrSql = StrSql + "NVL(MAT8.RECOVERY,0) AS RECOS8, "
                StrSql = StrSql + "NVL(MAT9.RECOVERY,0) AS RECOS9, "
                StrSql = StrSql + "NVL(MAT10.RECOVERY,0) AS RECOS10, "

                StrSql = StrSql + "NVL(MAT1.OSHAFACTOR,'Nothing') AS SUSS1, "
                StrSql = StrSql + "NVL(MAT2.OSHAFACTOR,'Nothing') AS SUSS2, "
                StrSql = StrSql + "NVL(MAT3.OSHAFACTOR,'Nothing') AS SUSS3, "
                StrSql = StrSql + "NVL(MAT4.OSHAFACTOR,'Nothing') AS SUSS4, "
                StrSql = StrSql + "NVL(MAT5.OSHAFACTOR,'Nothing') AS SUSS5, "
                StrSql = StrSql + "NVL(MAT6.OSHAFACTOR,'Nothing') AS SUSS6, "
                StrSql = StrSql + "NVL(MAT7.OSHAFACTOR,'Nothing') AS SUSS7, "
                StrSql = StrSql + "NVL(MAT8.OSHAFACTOR,'Nothing') AS SUSS8, "
                StrSql = StrSql + "NVL(MAT9.OSHAFACTOR,'Nothing') AS SUSS9, "
                StrSql = StrSql + "NVL(MAT10.OSHAFACTOR,'Nothing') AS SUSS10, "

                StrSql = StrSql + "NVL(MAT1.POSTCONSUMER,0) AS PCRECS1, "
                StrSql = StrSql + "NVL(MAT2.POSTCONSUMER,0) AS PCRECS2, "
                StrSql = StrSql + "NVL(MAT3.POSTCONSUMER,0) AS PCRECS3, "
                StrSql = StrSql + "NVL(MAT4.POSTCONSUMER,0) AS PCRECS4, "
                StrSql = StrSql + "NVL(MAT5.POSTCONSUMER,0) AS PCRECS5, "
                StrSql = StrSql + "NVL(MAT6.POSTCONSUMER,0) AS PCRECS6, "
                StrSql = StrSql + "NVL(MAT7.POSTCONSUMER,0) AS PCRECS7, "
                StrSql = StrSql + "NVL(MAT8.POSTCONSUMER,0) AS PCRECS8, "
                StrSql = StrSql + "NVL(MAT9.POSTCONSUMER,0) AS PCRECS9, "
                StrSql = StrSql + "NVL(MAT10.POSTCONSUMER,0) AS PCRECS10, "

                StrSql = StrSql + "MATS1.SG AS SGS1, "
                StrSql = StrSql + "MATS2.SG AS SGS2, "
                StrSql = StrSql + "MATS3.SG AS SGS3, "
                StrSql = StrSql + "MATS4.SG AS SGS4, "
                StrSql = StrSql + "MATS5.SG AS SGS5, "
                StrSql = StrSql + "MATS6.SG AS SGS6, "
                StrSql = StrSql + "MATS7.SG AS SGS7, "
                StrSql = StrSql + "MATS8.SG AS SGS8, "
                StrSql = StrSql + "MATS9.SG AS SGS9, "
                StrSql = StrSql + "MATS10.SG AS SGS10, "
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

                StrSql = StrSql + "TOTAL.THICK*PREF.CONVTHICK AS TOTTHICK, "
                StrSql = StrSql + "TOTAL.TOTALENERGY/PREF.CONVWT AS TOTERGY, "
                StrSql = StrSql + "TOTAL.TOTCO2EMISS TOTCO2, "
                StrSql = StrSql + "TOTAL.TOTMATSHIP*PREF.CONVTHICK3 AS TOTSHIP, "
                StrSql = StrSql + "TOTAL.TOTALRECOVERY AS TOTRECOV, "
                StrSql = StrSql + "TOTAL.TOTMATOSHA AS TOTSUSMAT, "
                StrSql = StrSql + "TOTAL.TOTALPOSTCONSUMER AS TOTREC, "
                StrSql = StrSql + "TOTAL.SG AS TOTSG, "
                StrSql = StrSql + "(TOTAL.TOTWATER*PREF.CONVGALLON/PREF.CONVWT) TOTWATER, "
                StrSql = StrSql + "(TOTAL.DISCRETEWATER*PREF.CONVGALLON/PREF.CONVWT) TOTDISWATER, "
                StrSql = StrSql + "TOTAL.WTPERAREA*PREF.CONVWT/PREF.CONVAREA AS TOTWEIGHT, "
                StrSql = StrSql + "TOTAL.DISCRETEWT* PREF.CONVWT AS TOTDISWEIGHT, "
                StrSql = StrSql + "TOTAL.DISCRETERGY/PREF.CONVWT AS TOTDISERGY, "
                StrSql = StrSql + "TOTAL.DISCRETECO2 AS TOTDISCO2 "
                StrSql = StrSql + "FROM MATERIALINPUT MAT "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.MATERIALSARCH MAT1 "
                StrSql = StrSql + "ON MAT1.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MAT1.INVENTORYTYPE =  MAT.INVENTORYTYPE "
                StrSql = StrSql + "AND MAT1.MATID = MAT.M1 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.MATERIALSARCH MAT2 "
                StrSql = StrSql + "ON MAT2.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MAT2.INVENTORYTYPE =  MAT.INVENTORYTYPE "
                StrSql = StrSql + "AND MAT2.MATID = MAT.M2 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.MATERIALSARCH MAT3 "
                StrSql = StrSql + "ON MAT3.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MAT3.INVENTORYTYPE =  MAT.INVENTORYTYPE "
                StrSql = StrSql + "AND MAT3.MATID = MAT.M3 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.MATERIALSARCH MAT4 "
                StrSql = StrSql + "ON MAT4.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MAT4.INVENTORYTYPE =  MAT.INVENTORYTYPE "
                StrSql = StrSql + "AND MAT4.MATID = MAT.M4 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.MATERIALSARCH MAT5 "
                StrSql = StrSql + "ON MAT5.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MAT5.INVENTORYTYPE =  MAT.INVENTORYTYPE "
                StrSql = StrSql + "AND MAT5.MATID = MAT.M5 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.MATERIALSARCH MAT6 "
                StrSql = StrSql + "ON MAT6.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MAT6.INVENTORYTYPE =  MAT.INVENTORYTYPE "
                StrSql = StrSql + "AND MAT6.MATID = MAT.M6 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.MATERIALSARCH MAT7 "
                StrSql = StrSql + "ON MAT7.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MAT7.INVENTORYTYPE =  MAT.INVENTORYTYPE "
                StrSql = StrSql + "AND MAT7.MATID = MAT.M7 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.MATERIALSARCH MAT8 "
                StrSql = StrSql + "ON MAT8.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MAT8.INVENTORYTYPE =  MAT.INVENTORYTYPE "
                StrSql = StrSql + "AND MAT8.MATID = MAT.M8 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.MATERIALSARCH MAT9 "
                StrSql = StrSql + "ON MAT9.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MAT9.INVENTORYTYPE =  MAT.INVENTORYTYPE "
                StrSql = StrSql + "AND MAT9.MATID = MAT.M9 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.MATERIALSARCH MAT10 "
                StrSql = StrSql + "ON MAT10.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MAT10.INVENTORYTYPE =  MAT.INVENTORYTYPE "
                StrSql = StrSql + "AND MAT10.MATID = MAT.M10 "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=MAT.CASEID "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATS1 "
                StrSql = StrSql + "ON MATS1.MATID = MAT.M1 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATS2 "
                StrSql = StrSql + "ON MATS2.MATID = MAT.M2 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATS3 "
                StrSql = StrSql + "ON MATS3.MATID = MAT.M3 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATS4 "
                StrSql = StrSql + "ON MATS4.MATID = MAT.M4 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATS5 "
                StrSql = StrSql + "ON MATS5.MATID = MAT.M5 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATS6 "
                StrSql = StrSql + "ON MATS6.MATID = MAT.M6 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATS7 "
                StrSql = StrSql + "ON MATS7.MATID = MAT.M7 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATS8 "
                StrSql = StrSql + "ON MATS8.MATID = MAT.M8 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATS9 "
                StrSql = StrSql + "ON MATS9.MATID = MAT.M9 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATS10 "
                StrSql = StrSql + "ON MATS10.MATID = MAT.M10 "
                StrSql = StrSql + "INNER JOIN MATERIALOUTPUT MATOUT "
                StrSql = StrSql + "ON MATOUT.CASEID=MAT.CASEID "
                StrSql = StrSql + "INNER JOIN TOTAL ON "
                StrSql = StrSql + "TOTAL.CASEID=MAT.CASEID "
                StrSql = StrSql + "WHERE MAT.CASEID =" + CaseId.ToString() + "  ORDER BY MAT.CASEID "


                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetExtrusionDetailsSugg:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetExtrusionDetails(ByVal CaseId As Integer) As DataSet
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
                StrSql = StrSql + "(NVL(MAT1.JOULE,0)/PREF.CONVWT) AS ERGYS1, "
                StrSql = StrSql + "(NVL(MAT2.JOULE,0)/PREF.CONVWT) AS ERGYS2, "
                StrSql = StrSql + "(NVL(MAT3.JOULE,0)/PREF.CONVWT) AS ERGYS3, "
                StrSql = StrSql + "(NVL(MAT4.JOULE,0)/PREF.CONVWT) AS ERGYS4, "
                StrSql = StrSql + "(NVL(MAT5.JOULE,0)/PREF.CONVWT) AS ERGYS5, "
                StrSql = StrSql + "(NVL(MAT6.JOULE,0)/PREF.CONVWT) AS ERGYS6, "
                StrSql = StrSql + "(NVL(MAT7.JOULE,0)/PREF.CONVWT) AS ERGYS7, "
                StrSql = StrSql + "(NVL(MAT8.JOULE,0)/PREF.CONVWT) AS ERGYS8, "
                StrSql = StrSql + "(NVL(MAT9.JOULE,0)/PREF.CONVWT) AS ERGYS9, "
                StrSql = StrSql + "(NVL(MAT10.JOULE,0)/PREF.CONVWT) AS ERGYS10, "
                StrSql = StrSql + "(MATERGY.M1/PREF.CONVWT) AS ERGYP1, "
                StrSql = StrSql + "(MATERGY.M2/PREF.CONVWT) AS ERGYP2, "
                StrSql = StrSql + "(MATERGY.M3/PREF.CONVWT) AS ERGYP3, "
                StrSql = StrSql + "(MATERGY.M4/PREF.CONVWT) AS ERGYP4, "
                StrSql = StrSql + "(MATERGY.M5/PREF.CONVWT) AS ERGYP5, "
                StrSql = StrSql + "(MATERGY.M6/PREF.CONVWT) AS ERGYP6, "
                StrSql = StrSql + "(MATERGY.M7/PREF.CONVWT) AS ERGYP7, "
                StrSql = StrSql + "(MATERGY.M8/PREF.CONVWT) AS ERGYP8, "
                StrSql = StrSql + "(MATERGY.M9/PREF.CONVWT) AS ERGYP9, "
                StrSql = StrSql + "(MATERGY.M10/PREF.CONVWT) AS ERGYP10, "
                StrSql = StrSql + "NVL(MAT1.PRICE,0) AS CO2S1, "
                StrSql = StrSql + "NVL(MAT2.PRICE,0) AS CO2S2, "
                StrSql = StrSql + "NVL(MAT3.PRICE,0) AS CO2S3, "
                StrSql = StrSql + "NVL(MAT4.PRICE,0) AS CO2S4, "
                StrSql = StrSql + "NVL(MAT5.PRICE,0) AS CO2S5, "
                StrSql = StrSql + "NVL(MAT6.PRICE,0) AS CO2S6, "
                StrSql = StrSql + "NVL(MAT7.PRICE,0) AS CO2S7, "
                StrSql = StrSql + "NVL(MAT8.PRICE,0) AS CO2S8, "
                StrSql = StrSql + "NVL(MAT9.PRICE,0) AS CO2S9, "
                StrSql = StrSql + "NVL(MAT10.PRICE,0) AS CO2S10, "
                StrSql = StrSql + "MAT.S1 AS CO2P1, "
                StrSql = StrSql + "MAT.S2 AS CO2P2, "
                StrSql = StrSql + "MAT.S3 AS CO2P3, "
                StrSql = StrSql + "MAT.S4 AS CO2P4, "
                StrSql = StrSql + "MAT.S5 AS CO2P5, "
                StrSql = StrSql + "MAT.S6 AS CO2P6, "
                StrSql = StrSql + "MAT.S7 AS CO2P7, "
                StrSql = StrSql + "MAT.S8 AS CO2P8, "
                StrSql = StrSql + "MAT.S9 AS CO2P9, "
                StrSql = StrSql + "MAT.S10 AS CO2P10, "
                'Changes for Water Start
                StrSql = StrSql + "(NVL(MAT1.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS1, "
                StrSql = StrSql + "(NVL(MAT2.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS2, "
                StrSql = StrSql + "(NVL(MAT3.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS3, "
                StrSql = StrSql + "(NVL(MAT4.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS4, "
                StrSql = StrSql + "(NVL(MAT5.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS5, "
                StrSql = StrSql + "(NVL(MAT6.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS6, "
                StrSql = StrSql + "(NVL(MAT7.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS7, "
                StrSql = StrSql + "(NVL(MAT8.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS8, "
                StrSql = StrSql + "(NVL(MAT9.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS9, "
                StrSql = StrSql + "(NVL(MAT10.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS10, "
                StrSql = StrSql + "(MATWATERPREF.M1*PREF.CONVGALLON/PREF.CONVWT) AS WATERP1, "
                StrSql = StrSql + "(MATWATERPREF.M2*PREF.CONVGALLON/PREF.CONVWT) AS WATERP2, "
                StrSql = StrSql + "(MATWATERPREF.M3*PREF.CONVGALLON/PREF.CONVWT) AS WATERP3, "
                StrSql = StrSql + "(MATWATERPREF.M4*PREF.CONVGALLON/PREF.CONVWT) AS WATERP4, "
                StrSql = StrSql + "(MATWATERPREF.M5*PREF.CONVGALLON/PREF.CONVWT) AS WATERP5, "
                StrSql = StrSql + "(MATWATERPREF.M6*PREF.CONVGALLON/PREF.CONVWT) AS WATERP6, "
                StrSql = StrSql + "(MATWATERPREF.M7*PREF.CONVGALLON/PREF.CONVWT) AS WATERP7, "
                StrSql = StrSql + "(MATWATERPREF.M8*PREF.CONVGALLON/PREF.CONVWT) AS WATERP8, "
                StrSql = StrSql + "(MATWATERPREF.M9*PREF.CONVGALLON/PREF.CONVWT) AS WATERP9, "
                StrSql = StrSql + "(MATWATERPREF.M10*PREF.CONVGALLON/PREF.CONVWT) AS WATERP10, "
                'Changes for Water end
                StrSql = StrSql + "(NVL(MAT1.SHIP,0)*PREF.CONVTHICK3) AS SHIPS1, "
                StrSql = StrSql + "(NVL(MAT2.SHIP,0)*PREF.CONVTHICK3) AS SHIPS2, "
                StrSql = StrSql + "(NVL(MAT3.SHIP,0)*PREF.CONVTHICK3) AS SHIPS3, "
                StrSql = StrSql + "(NVL(MAT4.SHIP,0)*PREF.CONVTHICK3) AS SHIPS4, "
                StrSql = StrSql + "(NVL(MAT5.SHIP,0)*PREF.CONVTHICK3) AS SHIPS5, "
                StrSql = StrSql + "(NVL(MAT6.SHIP,0)*PREF.CONVTHICK3) AS SHIPS6, "
                StrSql = StrSql + "(NVL(MAT7.SHIP,0)*PREF.CONVTHICK3) AS SHIPS7, "
                StrSql = StrSql + "(NVL(MAT8.SHIP,0)*PREF.CONVTHICK3) AS SHIPS8, "
                StrSql = StrSql + "(NVL(MAT9.SHIP,0)*PREF.CONVTHICK3) AS SHIPS9, "
                StrSql = StrSql + "(NVL(MAT10.SHIP,0)*PREF.CONVTHICK3) AS SHIPS10, "
                StrSql = StrSql + "(MATSHIP.M1*PREF.CONVTHICK3) AS SHIPP1, "
                StrSql = StrSql + "(MATSHIP.M2*PREF.CONVTHICK3) AS SHIPP2, "
                StrSql = StrSql + "(MATSHIP.M3*PREF.CONVTHICK3) AS SHIPP3, "
                StrSql = StrSql + "(MATSHIP.M4*PREF.CONVTHICK3) AS SHIPP4, "
                StrSql = StrSql + "(MATSHIP.M5*PREF.CONVTHICK3) AS SHIPP5, "
                StrSql = StrSql + "(MATSHIP.M6*PREF.CONVTHICK3) AS SHIPP6, "
                StrSql = StrSql + "(MATSHIP.M7*PREF.CONVTHICK3) AS SHIPP7, "
                StrSql = StrSql + "(MATSHIP.M8*PREF.CONVTHICK3) AS SHIPP8, "
                StrSql = StrSql + "(MATSHIP.M9*PREF.CONVTHICK3) AS SHIPP9, "
                StrSql = StrSql + "(MATSHIP.M10*PREF.CONVTHICK3) AS SHIPP10, "
                StrSql = StrSql + "NVL(MAT1.RECOVERY,0) AS RECOS1, "
                StrSql = StrSql + "NVL(MAT2.RECOVERY,0) AS RECOS2, "
                StrSql = StrSql + "NVL(MAT3.RECOVERY,0) AS RECOS3, "
                StrSql = StrSql + "NVL(MAT4.RECOVERY,0) AS RECOS4, "
                StrSql = StrSql + "NVL(MAT5.RECOVERY,0) AS RECOS5, "
                StrSql = StrSql + "NVL(MAT6.RECOVERY,0) AS RECOS6, "
                StrSql = StrSql + "NVL(MAT7.RECOVERY,0) AS RECOS7, "
                StrSql = StrSql + "NVL(MAT8.RECOVERY,0) AS RECOS8, "
                StrSql = StrSql + "NVL(MAT9.RECOVERY,0) AS RECOS9, "
                StrSql = StrSql + "NVL(MAT10.RECOVERY,0) AS RECOS10, "
                StrSql = StrSql + "MAT.RE1 AS RECOP1, "
                StrSql = StrSql + "MAT.RE2 AS RECOP2, "
                StrSql = StrSql + "MAT.RE3 AS RECOP3, "
                StrSql = StrSql + "MAT.RE4 AS RECOP4, "
                StrSql = StrSql + "MAT.RE5 AS RECOP5, "
                StrSql = StrSql + "MAT.RE6 AS RECOP6, "
                StrSql = StrSql + "MAT.RE7 AS RECOP7, "
                StrSql = StrSql + "MAT.RE8 AS RECOP8, "
                StrSql = StrSql + "MAT.RE9 AS RECOP9, "
                StrSql = StrSql + "MAT.RE10 AS RECOP10, "
                StrSql = StrSql + "NVL(MAT1.OSHAFACTOR,'Nothing') AS SUSS1, "
                StrSql = StrSql + "NVL(MAT2.OSHAFACTOR,'Nothing') AS SUSS2, "
                StrSql = StrSql + "NVL(MAT3.OSHAFACTOR,'Nothing') AS SUSS3, "
                StrSql = StrSql + "NVL(MAT4.OSHAFACTOR,'Nothing') AS SUSS4, "
                StrSql = StrSql + "NVL(MAT5.OSHAFACTOR,'Nothing') AS SUSS5, "
                StrSql = StrSql + "NVL(MAT6.OSHAFACTOR,'Nothing') AS SUSS6, "
                StrSql = StrSql + "NVL(MAT7.OSHAFACTOR,'Nothing') AS SUSS7, "
                StrSql = StrSql + "NVL(MAT8.OSHAFACTOR,'Nothing') AS SUSS8, "
                StrSql = StrSql + "NVL(MAT9.OSHAFACTOR,'Nothing') AS SUSS9, "
                StrSql = StrSql + "NVL(MAT10.OSHAFACTOR,'Nothing') AS SUSS10, "
                StrSql = StrSql + "MAT.OSH1 AS SUSP1, "
                StrSql = StrSql + "MAT.OSH2 AS SUSP2, "
                StrSql = StrSql + "MAT.OSH3 AS SUSP3, "
                StrSql = StrSql + "MAT.OSH4 AS SUSP4, "
                StrSql = StrSql + "MAT.OSH5 AS SUSP5, "
                StrSql = StrSql + "MAT.OSH6 AS SUSP6, "
                StrSql = StrSql + "MAT.OSH7 AS SUSP7, "
                StrSql = StrSql + "MAT.OSH8 AS SUSP8, "
                StrSql = StrSql + "MAT.OSH9 AS SUSP9, "
                StrSql = StrSql + "MAT.OSH10 AS SUSP10, "
                StrSql = StrSql + "NVL(MAT1.POSTCONSUMER,0) AS PCRECS1, "
                StrSql = StrSql + "NVL(MAT2.POSTCONSUMER,0) AS PCRECS2, "
                StrSql = StrSql + "NVL(MAT3.POSTCONSUMER,0) AS PCRECS3, "
                StrSql = StrSql + "NVL(MAT4.POSTCONSUMER,0) AS PCRECS4, "
                StrSql = StrSql + "NVL(MAT5.POSTCONSUMER,0) AS PCRECS5, "
                StrSql = StrSql + "NVL(MAT6.POSTCONSUMER,0) AS PCRECS6, "
                StrSql = StrSql + "NVL(MAT7.POSTCONSUMER,0) AS PCRECS7, "
                StrSql = StrSql + "NVL(MAT8.POSTCONSUMER,0) AS PCRECS8, "
                StrSql = StrSql + "NVL(MAT9.POSTCONSUMER,0) AS PCRECS9, "
                StrSql = StrSql + "NVL(MAT10.POSTCONSUMER,0) AS PCRECS10, "
                StrSql = StrSql + "MAT.POC1 AS PCRECP1, "
                StrSql = StrSql + "MAT.POC2 AS PCRECP2, "
                StrSql = StrSql + "MAT.POC3 AS PCRECP3, "
                StrSql = StrSql + "MAT.POC4 AS PCRECP4, "
                StrSql = StrSql + "MAT.POC5 AS PCRECP5, "
                StrSql = StrSql + "MAT.POC6 AS PCRECP6, "
                StrSql = StrSql + "MAT.POC7 AS PCRECP7, "
                StrSql = StrSql + "MAT.POC8 AS PCRECP8, "
                StrSql = StrSql + "MAT.POC9 AS PCRECP9, "
                StrSql = StrSql + "MAT.POC10 AS PCRECP10, "
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
                StrSql = StrSql + "MATS1.SG AS SGS1, "
                StrSql = StrSql + "MATS2.SG AS SGS2, "
                StrSql = StrSql + "MATS3.SG AS SGS3, "
                StrSql = StrSql + "MATS4.SG AS SGS4, "
                StrSql = StrSql + "MATS5.SG AS SGS5, "
                StrSql = StrSql + "MATS6.SG AS SGS6, "
                StrSql = StrSql + "MATS7.SG AS SGS7, "
                StrSql = StrSql + "MATS8.SG AS SGS8, "
                StrSql = StrSql + "MATS9.SG AS SGS9, "
                StrSql = StrSql + "MATS10.SG AS SGS10, "
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
                StrSql = StrSql + "(MAT.PS1*PREF.CONVWT) AS SHIPUNIT1, "
                StrSql = StrSql + "(MAT.PS2*PREF.CONVWT) AS SHIPUNIT2, "
                StrSql = StrSql + "(MAT.PS3*PREF.CONVWT) AS SHIPUNIT3, "
                StrSql = StrSql + "(MAT.PS4*PREF.CONVWT) AS SHIPUNIT4, "
                StrSql = StrSql + "(MAT.PS5*PREF.CONVWT) AS SHIPUNIT5, "
                StrSql = StrSql + "(MAT.PS6*PREF.CONVWT) AS SHIPUNIT6, "
                StrSql = StrSql + "(MAT.PS7*PREF.CONVWT) AS SHIPUNIT7, "
                StrSql = StrSql + "(MAT.PS8*PREF.CONVWT) AS SHIPUNIT8, "
                StrSql = StrSql + "(MAT.PS9*PREF.CONVWT) AS SHIPUNIT9, "
                StrSql = StrSql + "(MAT.PS10*PREF.CONVWT) AS SHIPUNIT10, "
                StrSql = StrSql + "MAT.SS1 AS SS1, "
                StrSql = StrSql + "MAT.SS2 AS SS2, "
                StrSql = StrSql + "MAT.SS3 AS SS3, "
                StrSql = StrSql + "MAT.SS4 AS SS4, "
                StrSql = StrSql + "MAT.SS5 AS SS5, "
                StrSql = StrSql + "MAT.SS6 AS SS6, "
                StrSql = StrSql + "MAT.SS7 AS SS7, "
                StrSql = StrSql + "MAT.SS8 AS SS8, "
                StrSql = StrSql + "MAT.SS9 AS SS9, "
                StrSql = StrSql + "MAT.SS10 AS SS10, "
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
                StrSql = StrSql + "MAT.DISCMATYN, "
                StrSql = StrSql + "MATDIS.DISID1, "
                StrSql = StrSql + "MATDIS.DISID2, "
                StrSql = StrSql + "MATDIS.DISID3, "
                StrSql = StrSql + "(MATDIS.DISW1*PREF.CONVWT) AS DISWEIGHT1, "
                StrSql = StrSql + "(MATDIS.DISW2*PREF.CONVWT) AS DISWEIGHT2, "
                StrSql = StrSql + "(MATDIS.DISW3*PREF.CONVWT) AS DISWEIGHT3, "
                StrSql = StrSql + "(MATDIS.DISE1/PREF.CONVWT) AS DISERGY1, "
                StrSql = StrSql + "(MATDIS.DISE2/PREF.CONVWT) AS DISERGY2, "
                StrSql = StrSql + "(MATDIS.DISE3/PREF.CONVWT) AS DISERGY3, "
                StrSql = StrSql + "(MATDIS.DISC1) AS DISCO1, "
                StrSql = StrSql + "(MATDIS.DISC2) AS DISCO2, "
                StrSql = StrSql + "(MATDIS.DISC3) AS DISCO3, "
                'Changes for Water Start
                StrSql = StrSql + "(MATDIS.DISWATER1*PREF.CONVGALLON/PREF.CONVWT) AS DISWATER1, "
                StrSql = StrSql + "(MATDIS.DISWATER2*PREF.CONVGALLON/PREF.CONVWT) AS DISWATER2, "
                StrSql = StrSql + "(MATDIS.DISWATER3*PREF.CONVGALLON/PREF.CONVWT) AS DISWATER3, "
                'Changes for Water end

                StrSql = StrSql + "MAT.PLATE, "
                StrSql = StrSql + "TOTAL.THICK*PREF.CONVTHICK AS TOTTHICK, "
                StrSql = StrSql + "TOTAL.TOTALENERGY/PREF.CONVWT AS TOTERGY, "
                StrSql = StrSql + "TOTAL.TOTCO2EMISS TOTCO2, "
                StrSql = StrSql + "TOTAL.TOTMATSHIP*PREF.CONVTHICK3 AS TOTSHIP, "
                StrSql = StrSql + "TOTAL.TOTALRECOVERY AS TOTRECOV, "
                StrSql = StrSql + "TOTAL.TOTMATOSHA AS TOTSUSMAT, "
                StrSql = StrSql + "TOTAL.TOTALPOSTCONSUMER AS TOTREC, "
                StrSql = StrSql + "TOTAL.SG AS TOTSG, "
                'Changes for Water Start
                StrSql = StrSql + "(TOTAL.TOTWATER*PREF.CONVGALLON/PREF.CONVWT) TOTWATER, "
                StrSql = StrSql + "(TOTAL.DISCRETEWATER*PREF.CONVGALLON/PREF.CONVWT) TOTDISWATER, "
                'Changes for Water end
                StrSql = StrSql + "TOTAL.WTPERAREA*PREF.CONVWT/PREF.CONVAREA AS TOTWEIGHT, "
                StrSql = StrSql + "TOTAL.DISCRETEWT* PREF.CONVWT AS TOTDISWEIGHT, "
                StrSql = StrSql + "TOTAL.DISCRETERGY/PREF.CONVWT AS TOTDISERGY, "
                StrSql = StrSql + "TOTAL.DISCRETECO2 AS TOTDISCO2 "
                StrSql = StrSql + "FROM MATERIALINPUT MAT "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.MATERIALSARCH MAT1 "
                StrSql = StrSql + "ON MAT1.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MAT1.INVENTORYTYPE =  MAT.INVENTORYTYPE "
                StrSql = StrSql + "AND MAT1.MATID = MAT.M1 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.MATERIALSARCH MAT2 "
                StrSql = StrSql + "ON MAT2.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MAT2.INVENTORYTYPE =  MAT.INVENTORYTYPE "
                StrSql = StrSql + "AND MAT2.MATID = MAT.M2 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.MATERIALSARCH MAT3 "
                StrSql = StrSql + "ON MAT3.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MAT3.INVENTORYTYPE =  MAT.INVENTORYTYPE "
                StrSql = StrSql + "AND MAT3.MATID = MAT.M3 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.MATERIALSARCH MAT4 "
                StrSql = StrSql + "ON MAT4.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MAT4.INVENTORYTYPE =  MAT.INVENTORYTYPE "
                StrSql = StrSql + "AND MAT4.MATID = MAT.M4 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.MATERIALSARCH MAT5 "
                StrSql = StrSql + "ON MAT5.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MAT5.INVENTORYTYPE =  MAT.INVENTORYTYPE "
                StrSql = StrSql + "AND MAT5.MATID = MAT.M5 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.MATERIALSARCH MAT6 "
                StrSql = StrSql + "ON MAT6.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MAT6.INVENTORYTYPE =  MAT.INVENTORYTYPE "
                StrSql = StrSql + "AND MAT6.MATID = MAT.M6 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.MATERIALSARCH MAT7 "
                StrSql = StrSql + "ON MAT7.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MAT7.INVENTORYTYPE =  MAT.INVENTORYTYPE "
                StrSql = StrSql + "AND MAT7.MATID = MAT.M7 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.MATERIALSARCH MAT8 "
                StrSql = StrSql + "ON MAT8.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MAT8.INVENTORYTYPE =  MAT.INVENTORYTYPE "
                StrSql = StrSql + "AND MAT8.MATID = MAT.M8 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.MATERIALSARCH MAT9 "
                StrSql = StrSql + "ON MAT9.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MAT9.INVENTORYTYPE =  MAT.INVENTORYTYPE "
                StrSql = StrSql + "AND MAT9.MATID = MAT.M9 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.MATERIALSARCH MAT10 "
                StrSql = StrSql + "ON MAT10.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MAT10.INVENTORYTYPE =  MAT.INVENTORYTYPE "
                StrSql = StrSql + "AND MAT10.MATID = MAT.M10 "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=MAT.CASEID "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATS1 "
                StrSql = StrSql + "ON MATS1.MATID = MAT.M1 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATS2 "
                StrSql = StrSql + "ON MATS2.MATID = MAT.M2 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATS3 "
                StrSql = StrSql + "ON MATS3.MATID = MAT.M3 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATS4 "
                StrSql = StrSql + "ON MATS4.MATID = MAT.M4 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATS5 "
                StrSql = StrSql + "ON MATS5.MATID = MAT.M5 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATS6 "
                StrSql = StrSql + "ON MATS6.MATID = MAT.M6 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATS7 "
                StrSql = StrSql + "ON MATS7.MATID = MAT.M7 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATS8 "
                StrSql = StrSql + "ON MATS8.MATID = MAT.M8 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATS9 "
                StrSql = StrSql + "ON MATS9.MATID = MAT.M9 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATS10 "
                StrSql = StrSql + "ON MATS10.MATID = MAT.M10 "
                StrSql = StrSql + "INNER JOIN MATENERGYPREF MATERGY "
                StrSql = StrSql + "ON MATERGY.CASEID=MAT.CASEID "
                'Changes for Water Start
                StrSql = StrSql + "INNER JOIN MATWATERPREF "
                StrSql = StrSql + "ON MATWATERPREF.CASEID=MAT.CASEID "
                'Changes for Water end
                StrSql = StrSql + "INNER JOIN MATSHIPPREF MATSHIP "
                StrSql = StrSql + "ON MATSHIP.CASEID=MAT.CASEID "
                StrSql = StrSql + "INNER JOIN MATERIALOUTPUT MATOUT "
                StrSql = StrSql + "ON MATOUT.CASEID=MAT.CASEID "
                StrSql = StrSql + "INNER JOIN MATERIALDISIN MATDIS "
                StrSql = StrSql + "ON MATDIS.CASEID=MAT.CASEID "
                StrSql = StrSql + "INNER JOIN TOTAL ON "
                StrSql = StrSql + "TOTAL.CASEID=MAT.CASEID "
                StrSql = StrSql + "WHERE MAT.CASEID =" + CaseId.ToString() + "  ORDER BY MAT.CASEID "


                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetRWPalletMat(ByVal CaseId As Integer, ByVal Col As String) As DataSet
            Dim StrSql As String = String.Empty
            Dim objDBUtil As New DBUtil()
            Dim ds As New DataSet()
            Try
                StrSql = "SELECT (MATS.MATDE1||' ' ||MATS.MATDE2)MATDES  "
                StrSql = StrSql + "FROM MATERIALINPUT MAT "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATS "
                StrSql = StrSql + "ON MATS.MATID=MAT.M" + Col + " "
                StrSql = StrSql + "WHERE CASEID=" + CaseId.ToString() + ""
                ds = objDBUtil.FillDataSet(StrSql, MedSustain1Connection)
            Catch ex As Exception
                Throw New Exception("S1:GetInputDetails:GetS1RWPalletInSugg:-" + ex.Message.ToString())
            End Try
            Return ds
        End Function

        Public Function GetRWPalletInSugg(ByVal CaseId As Integer, ByVal Col As String) As DataSet
            Dim StrSql As String = String.Empty
            Dim objDBUtil As New DBUtil()
            Dim ds As New DataSet()
            Try

                StrSql = "SELECT (NVL(PARCH1.JOULE,0)/PREF.CONVWT) ERGY1,  "
                StrSql = StrSql + "(NVL(PARCH2.JOULE,0)/PREF.CONVWT) ERGY2, "
                StrSql = StrSql + "(NVL(PARCH3.JOULE,0)/PREF.CONVWT) ERGY3, "
                StrSql = StrSql + "(NVL(PARCH4.JOULE,0)/PREF.CONVWT) ERGY4, "
                StrSql = StrSql + "(NVL(PARCH5.JOULE,0)/PREF.CONVWT) ERGY5, "
                StrSql = StrSql + "(NVL(PARCH6.JOULE,0)/PREF.CONVWT) ERGY6, "
                StrSql = StrSql + "(NVL(PARCH7.JOULE,0)/PREF.CONVWT) ERGY7, "
                StrSql = StrSql + "(NVL(PARCH8.JOULE,0)/PREF.CONVWT) ERGY8, "
                StrSql = StrSql + "(NVL(PARCH9.JOULE,0)/PREF.CONVWT) ERGY9, "
                StrSql = StrSql + "(NVL(PARCH10.JOULE,0)/PREF.CONVWT) ERGY10, "
                StrSql = StrSql + "NVL(PARCH1.PRICE,0) GHG1, "
                StrSql = StrSql + "NVL(PARCH2.PRICE,0) GHG2, "
                StrSql = StrSql + "NVL(PARCH3.PRICE,0) GHG3, "
                StrSql = StrSql + "NVL(PARCH4.PRICE,0) GHG4, "
                StrSql = StrSql + "NVL(PARCH5.PRICE,0) GHG5, "
                StrSql = StrSql + "NVL(PARCH6.PRICE,0) GHG6, "
                StrSql = StrSql + "NVL(PARCH7.PRICE,0) GHG7, "
                StrSql = StrSql + "NVL(PARCH8.PRICE,0) GHG8, "
                StrSql = StrSql + "NVL(PARCH9.PRICE,0) GHG9, "
                StrSql = StrSql + "NVL(PARCH10.PRICE,0) GHG10, "
                'Changes for Water Start
                StrSql = StrSql + "NVL(PARCH1.WATER,0)*PREF.CONVGALLON/PREF.CONVWT WATER1, "
                StrSql = StrSql + "NVL(PARCH2.WATER,0)*PREF.CONVGALLON/PREF.CONVWT WATER2, "
                StrSql = StrSql + "NVL(PARCH3.WATER,0)*PREF.CONVGALLON/PREF.CONVWT WATER3, "
                StrSql = StrSql + "NVL(PARCH4.WATER,0)*PREF.CONVGALLON/PREF.CONVWT WATER4, "
                StrSql = StrSql + "NVL(PARCH5.WATER,0)*PREF.CONVGALLON/PREF.CONVWT WATER5, "
                StrSql = StrSql + "NVL(PARCH6.WATER,0)*PREF.CONVGALLON/PREF.CONVWT WATER6, "
                StrSql = StrSql + "NVL(PARCH7.WATER,0)*PREF.CONVGALLON/PREF.CONVWT WATER7, "
                StrSql = StrSql + "NVL(PARCH8.WATER,0)*PREF.CONVGALLON/PREF.CONVWT WATER8, "
                StrSql = StrSql + "NVL(PARCH9.WATER,0)*PREF.CONVGALLON/PREF.CONVWT WATER9, "
                StrSql = StrSql + "NVL(PARCH10.WATER,0)*PREF.CONVGALLON/PREF.CONVWT WATER10, "
                'Changes for Water end
                StrSql = StrSql + "NVL(PARCH1.RECOVERYSUG,0) RECO1, "
                StrSql = StrSql + "NVL(PARCH2.RECOVERYSUG,0) RECO2, "
                StrSql = StrSql + "NVL(PARCH3.RECOVERYSUG,0) RECO3, "
                StrSql = StrSql + "NVL(PARCH4.RECOVERYSUG,0) RECO4, "
                StrSql = StrSql + "NVL(PARCH5.RECOVERYSUG,0) RECO5, "
                StrSql = StrSql + "NVL(PARCH6.RECOVERYSUG,0) RECO6, "
                StrSql = StrSql + "NVL(PARCH7.RECOVERYSUG,0) RECO7, "
                StrSql = StrSql + "NVL(PARCH8.RECOVERYSUG,0) RECO8, "
                StrSql = StrSql + "NVL(PARCH9.RECOVERYSUG,0) RECO9, "
                StrSql = StrSql + "NVL(PARCH10.RECOVERYSUG,0) RECO10, "
                StrSql = StrSql + "NVL(PARCH1.OSHAFACTOR,'Nothing') OSHA1, "
                StrSql = StrSql + "NVL(PARCH2.OSHAFACTOR,'Nothing') OSHA2, "
                StrSql = StrSql + "NVL(PARCH3.OSHAFACTOR,'Nothing') OSHA3, "
                StrSql = StrSql + "NVL(PARCH4.OSHAFACTOR,'Nothing') OSHA4, "
                StrSql = StrSql + "NVL(PARCH5.OSHAFACTOR,'Nothing') OSHA5, "
                StrSql = StrSql + "NVL(PARCH6.OSHAFACTOR,'Nothing') OSHA6, "
                StrSql = StrSql + "NVL(PARCH7.OSHAFACTOR,'Nothing') OSHA7, "
                StrSql = StrSql + "NVL(PARCH8.OSHAFACTOR,'Nothing') OSHA8, "
                StrSql = StrSql + "NVL(PARCH9.OSHAFACTOR,'Nothing') OSHA9, "
                StrSql = StrSql + "NVL(PARCH10.OSHAFACTOR,'Nothing') OSHA10, "
                StrSql = StrSql + "NVL(PARCH1.POSTCONSUMER,0) PC1, "
                StrSql = StrSql + "NVL(PARCH2.POSTCONSUMER,0) PC2, "
                StrSql = StrSql + "NVL(PARCH3.POSTCONSUMER,0) PC3, "
                StrSql = StrSql + "NVL(PARCH4.POSTCONSUMER,0) PC4, "
                StrSql = StrSql + "NVL(PARCH5.POSTCONSUMER,0) PC5, "
                StrSql = StrSql + "NVL(PARCH6.POSTCONSUMER,0) PC6, "
                StrSql = StrSql + "NVL(PARCH7.POSTCONSUMER,0) PC7, "
                StrSql = StrSql + "NVL(PARCH8.POSTCONSUMER,0) PC8, "
                StrSql = StrSql + "NVL(PARCH9.POSTCONSUMER,0) PC9, "
                StrSql = StrSql + "NVL(PARCH10.POSTCONSUMER,0) PC10, "
                StrSql = StrSql + "(NVL(PARCH1.SHIP,0)*PREF.CONVTHICK3) SHIP1, "
                StrSql = StrSql + "(NVL(PARCH2.SHIP,0)*PREF.CONVTHICK3) SHIP2, "
                StrSql = StrSql + "(NVL(PARCH3.SHIP,0)*PREF.CONVTHICK3) SHIP3, "
                StrSql = StrSql + "(NVL(PARCH4.SHIP,0)*PREF.CONVTHICK3) SHIP4, "
                StrSql = StrSql + "(NVL(PARCH5.SHIP,0)*PREF.CONVTHICK3) SHIP5, "
                StrSql = StrSql + "(NVL(PARCH6.SHIP,0)*PREF.CONVTHICK3) SHIP6, "
                StrSql = StrSql + "(NVL(PARCH7.SHIP,0)*PREF.CONVTHICK3) SHIP7, "
                StrSql = StrSql + "(NVL(PARCH8.SHIP,0)*PREF.CONVTHICK3) SHIP8, "
                StrSql = StrSql + "(NVL(PARCH9.SHIP,0)*PREF.CONVTHICK3) SHIP9, "
                StrSql = StrSql + "(NVL(PARCH10.SHIP,0)*PREF.CONVTHICK3) SHIP10, "
                StrSql = StrSql + "(NVL(PARCH1.WEIGHT,0)*PREF.CONVWT) WEIGHT1, "
                StrSql = StrSql + "(NVL(PARCH2.WEIGHT,0)*PREF.CONVWT) WEIGHT2, "
                StrSql = StrSql + "(NVL(PARCH3.WEIGHT,0)*PREF.CONVWT) WEIGHT3, "
                StrSql = StrSql + "(NVL(PARCH4.WEIGHT,0)*PREF.CONVWT) WEIGHT4, "
                StrSql = StrSql + "(NVL(PARCH5.WEIGHT,0)*PREF.CONVWT) WEIGHT5, "
                StrSql = StrSql + "(NVL(PARCH6.WEIGHT,0)*PREF.CONVWT) WEIGHT6, "
                StrSql = StrSql + "(NVL(PARCH7.WEIGHT,0)*PREF.CONVWT) WEIGHT7, "
                StrSql = StrSql + "(NVL(PARCH8.WEIGHT,0)*PREF.CONVWT) WEIGHT8, "
                StrSql = StrSql + "(NVL(PARCH9.WEIGHT,0)*PREF.CONVWT) WEIGHT9, "
                StrSql = StrSql + "(NVL(PARCH10.WEIGHT,0)*PREF.CONVWT) WEIGHT10, "
                StrSql = StrSql + "NVL(PARCH1.INCINERGY,0) INCINERGY1, "
                StrSql = StrSql + "NVL(PARCH2.INCINERGY,0) INCINERGY2, "
                StrSql = StrSql + "NVL(PARCH3.INCINERGY,0) INCINERGY3, "
                StrSql = StrSql + "NVL(PARCH4.INCINERGY,0) INCINERGY4, "
                StrSql = StrSql + "NVL(PARCH5.INCINERGY,0) INCINERGY5, "
                StrSql = StrSql + "NVL(PARCH6.INCINERGY,0) INCINERGY6, "
                StrSql = StrSql + "NVL(PARCH7.INCINERGY,0) INCINERGY7, "
                StrSql = StrSql + "NVL(PARCH8.INCINERGY,0) INCINERGY8, "
                StrSql = StrSql + "NVL(PARCH9.INCINERGY,0) INCINERGY9, "
                StrSql = StrSql + "NVL(PARCH10.INCINERGY,0) INCINERGY10, "
                StrSql = StrSql + "NVL(PARCH1.INCINGHG,0) INCINGHG1, "
                StrSql = StrSql + "NVL(PARCH2.INCINGHG,0) INCINGHG2, "
                StrSql = StrSql + "NVL(PARCH3.INCINGHG,0) INCINGHG3, "
                StrSql = StrSql + "NVL(PARCH4.INCINGHG,0) INCINGHG4, "
                StrSql = StrSql + "NVL(PARCH5.INCINGHG,0) INCINGHG5, "
                StrSql = StrSql + "NVL(PARCH6.INCINGHG,0) INCINGHG6, "
                StrSql = StrSql + "NVL(PARCH7.INCINGHG,0) INCINGHG7, "
                StrSql = StrSql + "NVL(PARCH8.INCINGHG,0) INCINGHG8, "
                StrSql = StrSql + "NVL(PARCH9.INCINGHG,0) INCINGHG9, "
                StrSql = StrSql + "NVL(PARCH10.INCINGHG,0) INCINGHG10, "
                StrSql = StrSql + "NVL(PARCH1.CMPSTGHG,0) CMPSTGHG1, "
                StrSql = StrSql + "NVL(PARCH2.CMPSTGHG,0) CMPSTGHG2, "
                StrSql = StrSql + "NVL(PARCH3.CMPSTGHG,0) CMPSTGHG3, "
                StrSql = StrSql + "NVL(PARCH4.CMPSTGHG,0) CMPSTGHG4, "
                StrSql = StrSql + "NVL(PARCH5.CMPSTGHG,0) CMPSTGHG5, "
                StrSql = StrSql + "NVL(PARCH6.CMPSTGHG,0) CMPSTGHG6, "
                StrSql = StrSql + "NVL(PARCH7.CMPSTGHG,0) CMPSTGHG7, "
                StrSql = StrSql + "NVL(PARCH8.CMPSTGHG,0) CMPSTGHG8, "
                StrSql = StrSql + "NVL(PARCH9.CMPSTGHG,0) CMPSTGHG9, "
                StrSql = StrSql + "NVL(PARCH10.CMPSTGHG,0) CMPSTGHG10 "
                StrSql = StrSql + "FROM PALLETINNEW PAIN "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PARCH1 "
                StrSql = StrSql + "ON PARCH1.EFFDATE = PAIN.EFFDATE "
                StrSql = StrSql + "AND PARCH1.INVENTORYTYPE = PAIN.INVENTORYTYPE "
                StrSql = StrSql + "AND PARCH1.PALLETID = PAIN." + Col + "1 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PARCH2 "
                StrSql = StrSql + "ON PARCH2.EFFDATE = PAIN.EFFDATE "
                StrSql = StrSql + "AND PARCH2.INVENTORYTYPE = PAIN.INVENTORYTYPE "
                StrSql = StrSql + "AND PARCH2.PALLETID = PAIN." + Col + "2 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PARCH3 "
                StrSql = StrSql + "ON PARCH3.EFFDATE = PAIN.EFFDATE "
                StrSql = StrSql + "AND PARCH3.INVENTORYTYPE = PAIN.INVENTORYTYPE "
                StrSql = StrSql + "AND PARCH3.PALLETID = PAIN." + Col + "3 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PARCH4 "
                StrSql = StrSql + "ON PARCH4.EFFDATE = PAIN.EFFDATE "
                StrSql = StrSql + "AND PARCH4.INVENTORYTYPE = PAIN.INVENTORYTYPE "
                StrSql = StrSql + "AND PARCH4.PALLETID = PAIN." + Col + "4 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PARCH5 "
                StrSql = StrSql + "ON PARCH5.EFFDATE = PAIN.EFFDATE "
                StrSql = StrSql + "AND PARCH5.INVENTORYTYPE = PAIN.INVENTORYTYPE "
                StrSql = StrSql + "AND PARCH5.PALLETID = PAIN." + Col + "5 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PARCH6 "
                StrSql = StrSql + "ON PARCH6.EFFDATE = PAIN.EFFDATE "
                StrSql = StrSql + "AND PARCH6.INVENTORYTYPE = PAIN.INVENTORYTYPE "
                StrSql = StrSql + "AND PARCH6.PALLETID = PAIN." + Col + "6 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PARCH7 "
                StrSql = StrSql + "ON PARCH7.EFFDATE = PAIN.EFFDATE "
                StrSql = StrSql + "AND PARCH7.INVENTORYTYPE = PAIN.INVENTORYTYPE "
                StrSql = StrSql + "AND PARCH7.PALLETID = PAIN." + Col + "7 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PARCH8 "
                StrSql = StrSql + "ON PARCH8.EFFDATE = PAIN.EFFDATE "
                StrSql = StrSql + "AND PARCH8.INVENTORYTYPE = PAIN.INVENTORYTYPE "
                StrSql = StrSql + "AND PARCH8.PALLETID = PAIN." + Col + "8 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PARCH9 "
                StrSql = StrSql + "ON PARCH9.EFFDATE = PAIN.EFFDATE "
                StrSql = StrSql + "AND PARCH9.INVENTORYTYPE = PAIN.INVENTORYTYPE "
                StrSql = StrSql + "AND PARCH9.PALLETID = PAIN." + Col + "9 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PARCH10 "
                StrSql = StrSql + "ON PARCH10.EFFDATE = PAIN.EFFDATE "
                StrSql = StrSql + "AND PARCH10.INVENTORYTYPE = PAIN.INVENTORYTYPE "
                StrSql = StrSql + "AND PARCH10.PALLETID = PAIN." + Col + "10 "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=PAIN.CASEID "
                StrSql = StrSql + "WHERE PAIN.CASEID IN (" + CaseId.ToString() + ") "




                ds = objDBUtil.FillDataSet(StrSql, MedSustain1Connection)
            Catch ex As Exception
                Throw New Exception("S1:GetInputDetails:GetS1RWPalletInSugg:-" + ex.Message.ToString())
            End Try
            Return ds
        End Function

        Public Function GetRWPalletInPref(ByVal CaseId As Integer, ByVal Col As String, ByVal MatLevel As Integer) As DataSet
            Dim StrSql As String = String.Empty
            Dim objDBUtil As New DBUtil()
            Dim ds As New DataSet()
            Try
                StrSql = "SELECT PAINNUM." + Col + "1 NUM1,  "
                StrSql = StrSql + "PAINNUM." + Col + "2 NUM2, "
                StrSql = StrSql + "PAINNUM." + Col + "3 NUM3, "
                StrSql = StrSql + "PAINNUM." + Col + "4 NUM4, "
                StrSql = StrSql + "PAINNUM." + Col + "5 NUM5, "
                StrSql = StrSql + "PAINNUM." + Col + "6 NUM6, "
                StrSql = StrSql + "PAINNUM." + Col + "7 NUM7, "
                StrSql = StrSql + "PAINNUM." + Col + "8 NUM8, "
                StrSql = StrSql + "PAINNUM." + Col + "9 NUM9, "
                StrSql = StrSql + "PAINNUM." + Col + "10 NUM10, "
                StrSql = StrSql + "(PAINERGY." + Col + "1/PREF.CONVWT) ERGY1, "
                StrSql = StrSql + "(PAINERGY." + Col + "2/PREF.CONVWT) ERGY2, "
                StrSql = StrSql + "(PAINERGY." + Col + "3/PREF.CONVWT) ERGY3, "
                StrSql = StrSql + "(PAINERGY." + Col + "4/PREF.CONVWT) ERGY4, "
                StrSql = StrSql + "(PAINERGY." + Col + "5/PREF.CONVWT) ERGY5, "
                StrSql = StrSql + "(PAINERGY." + Col + "6/PREF.CONVWT) ERGY6, "
                StrSql = StrSql + "(PAINERGY." + Col + "7/PREF.CONVWT) ERGY7, "
                StrSql = StrSql + "(PAINERGY." + Col + "8/PREF.CONVWT) ERGY8, "
                StrSql = StrSql + "(PAINERGY." + Col + "9/PREF.CONVWT) ERGY9, "
                StrSql = StrSql + "(PAINERGY." + Col + "10/PREF.CONVWT) ERGY10, "
                StrSql = StrSql + "PAINGHG." + Col + "1 GHG1, "
                StrSql = StrSql + "PAINGHG." + Col + "2 GHG2, "
                StrSql = StrSql + "PAINGHG." + Col + "3 GHG3, "
                StrSql = StrSql + "PAINGHG." + Col + "4 GHG4, "
                StrSql = StrSql + "PAINGHG." + Col + "5 GHG5, "
                StrSql = StrSql + "PAINGHG." + Col + "6 GHG6, "
                StrSql = StrSql + "PAINGHG." + Col + "7 GHG7, "
                StrSql = StrSql + "PAINGHG." + Col + "8 GHG8, "
                StrSql = StrSql + "PAINGHG." + Col + "9 GHG9, "
                StrSql = StrSql + "PAINGHG." + Col + "10 GHG10, "
                'Changes for Water Start
                StrSql = StrSql + "(SELECT " + Col + "1*PREF.CONVGALLON/PREF.CONVWT  FROM PALLETINNEWWATERPREF WHERE CASEID = " + CaseId.ToString() + ")WATER1 , "
                StrSql = StrSql + "(SELECT " + Col + "2*PREF.CONVGALLON/PREF.CONVWT FROM PALLETINNEWWATERPREF WHERE CASEID = " + CaseId.ToString() + ")WATER2 , "
                StrSql = StrSql + "(SELECT " + Col + "3*PREF.CONVGALLON/PREF.CONVWT FROM PALLETINNEWWATERPREF WHERE CASEID = " + CaseId.ToString() + ")WATER3 , "
                StrSql = StrSql + "(SELECT " + Col + "4*PREF.CONVGALLON/PREF.CONVWT FROM PALLETINNEWWATERPREF WHERE CASEID = " + CaseId.ToString() + ")WATER4 , "
                StrSql = StrSql + "(SELECT " + Col + "5*PREF.CONVGALLON/PREF.CONVWT FROM PALLETINNEWWATERPREF WHERE CASEID = " + CaseId.ToString() + ")WATER5 , "
                StrSql = StrSql + "(SELECT " + Col + "6*PREF.CONVGALLON/PREF.CONVWT FROM PALLETINNEWWATERPREF WHERE CASEID = " + CaseId.ToString() + ")WATER6 , "
                StrSql = StrSql + "(SELECT " + Col + "7*PREF.CONVGALLON/PREF.CONVWT FROM PALLETINNEWWATERPREF WHERE CASEID = " + CaseId.ToString() + ")WATER7 , "
                StrSql = StrSql + "(SELECT " + Col + "8*PREF.CONVGALLON/PREF.CONVWT FROM PALLETINNEWWATERPREF WHERE CASEID = " + CaseId.ToString() + ")WATER8 , "
                StrSql = StrSql + "(SELECT " + Col + "9*PREF.CONVGALLON/PREF.CONVWT FROM PALLETINNEWWATERPREF WHERE CASEID = " + CaseId.ToString() + ")WATER9 , "
                StrSql = StrSql + "(SELECT " + Col + "10*PREF.CONVGALLON/PREF.CONVWT FROM PALLETINNEWWATERPREF WHERE CASEID = " + CaseId.ToString() + ")WATER10 , "
                'Changes for Water end

                StrSql = StrSql + "PAIN." + Col + "1 PATID1, "
                StrSql = StrSql + "PAIN." + Col + "2 PATID2, "
                StrSql = StrSql + "PAIN." + Col + "3 PATID3, "
                StrSql = StrSql + "PAIN." + Col + "4 PATID4, "
                StrSql = StrSql + "PAIN." + Col + "5 PATID5, "
                StrSql = StrSql + "PAIN." + Col + "6 PATID6, "
                StrSql = StrSql + "PAIN." + Col + "7 PATID7, "
                StrSql = StrSql + "PAIN." + Col + "8 PATID8, "
                StrSql = StrSql + "PAIN." + Col + "9 PATID9, "
                StrSql = StrSql + "PAIN." + Col + "10 PATID10, "
                StrSql = StrSql + "PAINRECYCLE." + Col + "1 RECYCLE1, "
                StrSql = StrSql + "PAINRECYCLE." + Col + "2 RECYCLE2, "
                StrSql = StrSql + "PAINRECYCLE." + Col + "3 RECYCLE3, "
                StrSql = StrSql + "PAINRECYCLE." + Col + "4 RECYCLE4, "
                StrSql = StrSql + "PAINRECYCLE." + Col + "5 RECYCLE5, "
                StrSql = StrSql + "PAINRECYCLE." + Col + "6 RECYCLE6, "
                StrSql = StrSql + "PAINRECYCLE." + Col + "7 RECYCLE7, "
                StrSql = StrSql + "PAINRECYCLE." + Col + "8 RECYCLE8, "
                StrSql = StrSql + "PAINRECYCLE." + Col + "9 RECYCLE9, "
                StrSql = StrSql + "PAINRECYCLE." + Col + "10 RECYCLE10, "
                StrSql = StrSql + "PAINRECO." + Col + "1 RECO1, "
                StrSql = StrSql + "PAINRECO." + Col + "2 RECO2, "
                StrSql = StrSql + "PAINRECO." + Col + "3 RECO3, "
                StrSql = StrSql + "PAINRECO." + Col + "4 RECO4, "
                StrSql = StrSql + "PAINRECO." + Col + "5 RECO5, "
                StrSql = StrSql + "PAINRECO." + Col + "6 RECO6, "
                StrSql = StrSql + "PAINRECO." + Col + "7 RECO7, "
                StrSql = StrSql + "PAINRECO." + Col + "8 RECO8, "
                StrSql = StrSql + "PAINRECO." + Col + "9 RECO9, "
                StrSql = StrSql + "PAINRECO." + Col + "10 RECO10, "
                StrSql = StrSql + "PAINOSHA." + Col + "1 OSHA1, "
                StrSql = StrSql + "PAINOSHA." + Col + "2 OSHA2, "
                StrSql = StrSql + "PAINOSHA." + Col + "3 OSHA3, "
                StrSql = StrSql + "PAINOSHA." + Col + "4 OSHA4, "
                StrSql = StrSql + "PAINOSHA." + Col + "5 OSHA5, "
                StrSql = StrSql + "PAINOSHA." + Col + "6 OSHA6, "
                StrSql = StrSql + "PAINOSHA." + Col + "7 OSHA7, "
                StrSql = StrSql + "PAINOSHA." + Col + "8 OSHA8, "
                StrSql = StrSql + "PAINOSHA." + Col + "9 OSHA9, "
                StrSql = StrSql + "PAINOSHA." + Col + "10 OSHA10, "
                StrSql = StrSql + "PAINPC." + Col + "1 PC1, "
                StrSql = StrSql + "PAINPC." + Col + "2 PC2, "
                StrSql = StrSql + "PAINPC." + Col + "3 PC3, "
                StrSql = StrSql + "PAINPC." + Col + "4 PC4, "
                StrSql = StrSql + "PAINPC." + Col + "5 PC5, "
                StrSql = StrSql + "PAINPC." + Col + "6 PC6, "
                StrSql = StrSql + "PAINPC." + Col + "7 PC7, "
                StrSql = StrSql + "PAINPC." + Col + "8 PC8, "
                StrSql = StrSql + "PAINPC." + Col + "9 PC9, "
                StrSql = StrSql + "PAINPC." + Col + "10 PC10, "
                StrSql = StrSql + "(PAINSHIP." + Col + "1*PREF.CONVTHICK3) SHIP1, "
                StrSql = StrSql + "(PAINSHIP." + Col + "2*PREF.CONVTHICK3) SHIP2, "
                StrSql = StrSql + "(PAINSHIP." + Col + "3*PREF.CONVTHICK3) SHIP3, "
                StrSql = StrSql + "(PAINSHIP." + Col + "4*PREF.CONVTHICK3) SHIP4, "
                StrSql = StrSql + "(PAINSHIP." + Col + "5*PREF.CONVTHICK3) SHIP5, "
                StrSql = StrSql + "(PAINSHIP." + Col + "6*PREF.CONVTHICK3) SHIP6, "
                StrSql = StrSql + "(PAINSHIP." + Col + "7*PREF.CONVTHICK3) SHIP7, "
                StrSql = StrSql + "(PAINSHIP." + Col + "8*PREF.CONVTHICK3) SHIP8, "
                StrSql = StrSql + "(PAINSHIP." + Col + "9*PREF.CONVTHICK3) SHIP9, "
                StrSql = StrSql + "(PAINSHIP." + Col + "10*PREF.CONVTHICK3) SHIP10, "
                StrSql = StrSql + "PAIN.TRANSUNITCUBE, "
                StrSql = StrSql + "PREF.TITLE4, "
                StrSql = StrSql + "PREF.TITLE8, "
                StrSql = StrSql + "PREF.TITLE10 "
                StrSql = StrSql + "FROM PALLETINNEW PAIN "
                StrSql = StrSql + "INNER JOIN PALLETINNEWNUMBER PAINNUM "
                StrSql = StrSql + "ON PAINNUM.CASEID = PAIN.CASEID "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=PAIN.CASEID "
                StrSql = StrSql + "INNER JOIN PALLETINNEWENERGYPREF PAINERGY "
                StrSql = StrSql + "ON PAINERGY.CASEID = PAIN.CASEID "
                StrSql = StrSql + "INNER JOIN PALLETINNEWPREF PAINGHG "
                StrSql = StrSql + "ON PAINGHG.CASEID = PAIN.CASEID "
                StrSql = StrSql + "INNER JOIN PALLETINNEWRECYCLE PAINRECYCLE "
                StrSql = StrSql + "ON PAINRECYCLE.CASEID = PAIN.CASEID "
                StrSql = StrSql + "INNER JOIN PALLETINNEWRECOVERY PAINRECO "
                StrSql = StrSql + "ON PAINRECO.CASEID = PAIN.CASEID "
                StrSql = StrSql + "INNER JOIN PALLETINNEWOSHAPREF PAINOSHA "
                StrSql = StrSql + "ON PAINOSHA.CASEID = PAIN.CASEID "
                StrSql = StrSql + "INNER JOIN PALLETINNEWPOSTCONSUPREF PAINPC "
                StrSql = StrSql + "ON PAINPC.CASEID = PAIN.CASEID "
                StrSql = StrSql + "INNER JOIN PALLETINNEWSHIPPING PAINSHIP "
                StrSql = StrSql + "ON PAINSHIP.CASEID = PAIN.CASEID "
                StrSql = StrSql + "WHERE PAIN.CASEID IN (" + CaseId.ToString() + ") "



                ds = objDBUtil.FillDataSet(StrSql, MedSustain1Connection)
            Catch ex As Exception
                Throw New Exception("S1:GetInputDetails:GetS1RWPalletInPref:-" + ex.Message.ToString())
            End Try
            Return ds
        End Function

        Public Function GetRWPalletTotal(ByVal CaseId As Integer, ByVal Col As String) As DataSet
            Dim StrSql As String = String.Empty
            Dim objDBUtil As New DBUtil()
            Dim ds As New DataSet()
            Try
                StrSql = "SELECT (W" + Col + "*PREF.CONVWT) AS WEIGHT,  "
                StrSql = StrSql + "(EN" + Col + "/PREF.CONVWT) AS ERGY, "
                'Changes for Water Start
                StrSql = StrSql + "WATER" + Col + "*PREF.CONVGALLON/PREF.CONVWT AS WATER, "
                'Changes for Water end
                StrSql = StrSql + "C" + Col + " AS GHG, "
                StrSql = StrSql + "RECO" + Col + " AS RECO, "
                StrSql = StrSql + "HS" + Col + " AS OSHA, "
                StrSql = StrSql + "PC" + Col + " AS PC, "
                StrSql = StrSql + "(SD" + Col + "*PREF.CONVTHICK3) AS SD "
                StrSql = StrSql + "FROM RWPALLETTOTAL RWTOTAL "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = RWTOTAL.CASEID "
                StrSql = StrSql + "WHERE RWTOTAL.CASEID = " + CaseId.ToString() + ""

                ds = objDBUtil.FillDataSet(StrSql, MedSustain1Connection)
            Catch ex As Exception
                Throw New Exception("Sq:GetInputDetails:GetS1RWPalletInSugg:-" + ex.Message.ToString())
            End Try
            Return ds
        End Function

        Public Function GetMaterialsSource(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "MAT.CASEID, "
                StrSql = StrSql + "MAT1.MATDE1||'  '|| MAT1.MATDE2 AS MAT1, "
                StrSql = StrSql + "MAT2.MATDE1||'  '|| MAT2.MATDE2 AS MAT2, "
                StrSql = StrSql + "MAT3.MATDE1||'  '|| MAT3.MATDE2 AS MAT3, "
                StrSql = StrSql + "MAT4.MATDE1||'  '|| MAT4.MATDE2 AS MAT4, "
                StrSql = StrSql + "MAT5.MATDE1||'  '|| MAT5.MATDE2 AS MAT5, "
                StrSql = StrSql + "MAT6.MATDE1||'  '|| MAT6.MATDE2 AS MAT6, "
                StrSql = StrSql + "MAT7.MATDE1||'  '|| MAT7.MATDE2 AS MAT7, "
                StrSql = StrSql + "MAT8.MATDE1||'  '|| MAT8.MATDE2 AS MAT8, "
                StrSql = StrSql + "MAT9.MATDE1||'  '|| MAT9.MATDE2 AS MAT9, "
                StrSql = StrSql + "MAT10.MATDE1||'  '|| MAT10.MATDE2 AS MAT10, "
                StrSql = StrSql + "MAT1.SOURCE AS MATSOURCE1, "
                StrSql = StrSql + "MAT2.SOURCE AS MATSOURCE2, "
                StrSql = StrSql + "MAT3.SOURCE AS MATSOURCE3, "
                StrSql = StrSql + "MAT4.SOURCE AS MATSOURCE4, "
                StrSql = StrSql + "MAT5.SOURCE AS MATSOURCE5, "
                StrSql = StrSql + "MAT6.SOURCE AS MATSOURCE6, "
                StrSql = StrSql + "MAT7.SOURCE AS MATSOURCE7, "
                StrSql = StrSql + "MAT8.SOURCE AS MATSOURCE8, "
                StrSql = StrSql + "MAT9.SOURCE AS MATSOURCE9, "
                StrSql = StrSql + "MAT10.SOURCE AS MATSOURCE10 "
                StrSql = StrSql + "FROM MATERIALINPUT MAT "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MAT1 "
                StrSql = StrSql + "on MAT1.MATID=MAT.M1 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MAT2 "
                StrSql = StrSql + "on MAT2.MATID=MAT.M2 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MAT3 "
                StrSql = StrSql + "on MAT3.MATID=MAT.M3 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MAT4 "
                StrSql = StrSql + "on MAT4.MATID=MAT.M4 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MAT5 "
                StrSql = StrSql + "on MAT5.MATID=MAT.M5 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MAT6 "
                StrSql = StrSql + "on MAT6.MATID=MAT.M6 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MAT7 "
                StrSql = StrSql + "on MAT7.MATID=MAT.M7 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MAT8 "
                StrSql = StrSql + "on MAT8.MATID=MAT.M8 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MAT9 "
                StrSql = StrSql + "on MAT9.MATID=MAT.M9 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MAT10 "
                StrSql = StrSql + "on MAT10.MATID=MAT.M10 "
                StrSql = StrSql + "WHERE MAT.CASEID =" + CaseId.ToString() + "  ORDER BY MAT.CASEID "

                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetMaterialsSource:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetProductFromatIn(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT  "
                StrSql = StrSql + "PRODUCTFORMATIN.CASEID, "
                StrSql = StrSql + "PRODUCTFORMATIN.M1, "
                StrSql = StrSql + "(PRODUCTFORMATIN.M2*PREFERENCES.CONVTHICK) AS M2, "
                StrSql = StrSql + "(PRODUCTFORMATIN.M3*PREFERENCES.CONVTHICK* "
                StrSql = StrSql + "(CASE PREFERENCES.UNITS "
                StrSql = StrSql + "WHEN 1 THEN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "CASE PRODUCTFORMATIN.M1  WHEN 1 THEN 0.01204  ELSE 1 END "
                StrSql = StrSql + ") "
                StrSql = StrSql + "ELSE 1 "
                StrSql = StrSql + "END "
                StrSql = StrSql + ") "
                StrSql = StrSql + ") AS M3, "
                StrSql = StrSql + "(PRODUCTFORMATIN.M4*PREFERENCES.CONVTHICK) AS M4, "
                StrSql = StrSql + "PRODUCTFORMATIN.M5, "
                StrSql = StrSql + "PRODUCTFORMATIN.M6, "
                StrSql = StrSql + "NVL(PRODUCTFORMAT.M1,PRODUCTFORMAT2.M1 ) AS FORMAT_M1, "
                StrSql = StrSql + "NVL(PRODUCTFORMAT.M2,PRODUCTFORMAT2.M2 ) AS FORMAT_M2, "
                StrSql = StrSql + "NVL(PRODUCTFORMAT.M3,PRODUCTFORMAT2.M3 ) AS FORMAT_M3, "
                StrSql = StrSql + "NVL(PRODUCTFORMAT.M4,PRODUCTFORMAT2.M4 ) AS FORMAT_M4, "
                StrSql = StrSql + "NVL(PRODUCTFORMAT.M5,PRODUCTFORMAT2.M5 ) AS FORMAT_M5, "
                StrSql = StrSql + "(TOTAL.PRODWT*PREFERENCES.CONVWT) AS CONTWT, "
                StrSql = StrSql + "(PRODUCTFORMATIN.PWT*PREFERENCES.CONVWT) AS PRODWTPREF, "
                StrSql = StrSql + "(CASE "
                StrSql = StrSql + "WHEN TOTAL.ROLLDIA > 0 THEN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "CASE PREFERENCES.UNITS  WHEN 0 THEN '(in)'  ELSE '(mm)'  END "
                StrSql = StrSql + ") "
                StrSql = StrSql + "END "
                StrSql = StrSql + ") "
                StrSql = StrSql + "AS RUNIT, "
                StrSql = StrSql + "(CASE "
                StrSql = StrSql + "WHEN TOTAL.ROLLDIA > 0 THEN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "TOTAL.ROLLDIA*PREFERENCES.CONVTHICK "
                StrSql = StrSql + ") "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END "
                StrSql = StrSql + ") "
                StrSql = StrSql + "AS ROLLDIA, "
                StrSql = StrSql + "PREFERENCES.TITLE1, "
                StrSql = StrSql + "PREFERENCES.TITLE3, "
                StrSql = StrSql + "PREFERENCES.TITLE2, "
                StrSql = StrSql + "PREFERENCES.TITLE4, "
                StrSql = StrSql + "PREFERENCES.TITLE5, "
                StrSql = StrSql + "PREFERENCES.TITLE6, "
                StrSql = StrSql + "PREFERENCES.TITLE7, "
                StrSql = StrSql + "PREFERENCES.TITLE8, "
                StrSql = StrSql + "PREFERENCES.TITLE9, "
                StrSql = StrSql + "PREFERENCES.TITLE10, "
                StrSql = StrSql + "PREFERENCES.TITLE11, "
                StrSql = StrSql + "PREFERENCES.TITLE12, "
                StrSql = StrSql + "PREFERENCES.UNITS "
                StrSql = StrSql + "FROM PRODUCTFORMATIN "
                StrSql = StrSql + "INNER JOIN PREFERENCES "
                StrSql = StrSql + "ON PREFERENCES.CASEID = PRODUCTFORMATIN.CASEID "
                StrSql = StrSql + "INNER JOIN TOTAL "
                StrSql = StrSql + "ON TOTAL.CASEID = PRODUCTFORMATIN.CASEID "
                StrSql = StrSql + "LEFT OUTER JOIN MED1.PRODUCTFORMAT "
                StrSql = StrSql + "ON PRODUCTFORMAT.FORMATID = PRODUCTFORMATIN.M1 "
                StrSql = StrSql + "AND PREFERENCES.UNITS = 0 "
                StrSql = StrSql + "LEFT OUTER JOIN MED1.PRODUCTFORMAT2 "
                StrSql = StrSql + "ON PRODUCTFORMAT2.FORMATID = PRODUCTFORMATIN.M1 "
                StrSql = StrSql + "AND PREFERENCES.UNITS = 1 "
                StrSql = StrSql + "WHERE PRODUCTFORMATIN.CASEID  = " + CaseId.ToString() + " ORDER BY PRODUCTFORMATIN.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetProductFromatIn:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPalletAndTruck(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT TKLP.CASEID,  "
                StrSql = StrSql + "(TKLP.M1*PREF.CONVTHICK) AS P1, "
                StrSql = StrSql + "(TKLP.M2*PREF.CONVTHICK)	AS P2, "
                StrSql = StrSql + "(TKLP.M3*PREF.CONVTHICK) AS P3, "
                StrSql = StrSql + "TKLP.M4 AS P4, "
                StrSql = StrSql + "TKLP.M5 AS P5, "
                StrSql = StrSql + "(TKLP.T1*PREF.CONVTHICK) AS T1, "
                StrSql = StrSql + "(TKLP.T2*PREF.CONVTHICK) AS T2, "
                StrSql = StrSql + "(TKLP.T3*PREF.CONVTHICK) AS T3, "
                StrSql = StrSql + "(TKLP.T4*PREF.CONVWT) AS T4, "
                StrSql = StrSql + "TKLP.T5 AS T5, "
                StrSql = StrSql + "(TOTAL.TOTWTPERT*PREF.CONVWT) AS CALCULATEDWEIGHT, "
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
                StrSql = StrSql + "FROM TRUCKPALLETIN TKLP "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = TKLP.CASEID "
                StrSql = StrSql + "INNER JOIN TOTAL "
                StrSql = StrSql + "ON TOTAL.CASEID = TKLP.CASEID "
                StrSql = StrSql + "WHERE TKLP.CASEID =" + CaseId.ToString() + ""
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetPalletAndTruck:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPalletInDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PAT.CASEID,  "
                StrSql = StrSql + "PAT.INVENTORYTYPE, "
                StrSql = StrSql + "TO_CHAR(PAT.EFFDATE,'MM/DD/YYYY')EFFDATE, "
                StrSql = StrSql + "PAT.M1, "
                StrSql = StrSql + "PAT.M2, "
                StrSql = StrSql + "PAT.M3, "
                StrSql = StrSql + "PAT.M4, "
                StrSql = StrSql + "PAT.M5, "
                StrSql = StrSql + "PAT.M6, "
                StrSql = StrSql + "PAT.M7, "
                StrSql = StrSql + "PAT.M8, "
                StrSql = StrSql + "PAT.M9, "
                StrSql = StrSql + "PAT.M10, "
                StrSql = StrSql + "PAT.R1 AS NOUSE1, "
                StrSql = StrSql + "PAT.R2 AS NOUSE2, "
                StrSql = StrSql + "PAT.R3 AS NOUSE3, "
                StrSql = StrSql + "PAT.R4 AS NOUSE4, "
                StrSql = StrSql + "PAT.R5 AS NOUSE5, "
                StrSql = StrSql + "PAT.R6 AS NOUSE6, "
                StrSql = StrSql + "PAT.R7 AS NOUSE7, "
                StrSql = StrSql + "PAT.R8 AS NOUSE8, "
                StrSql = StrSql + "PAT.R9 AS NOUSE9, "
                StrSql = StrSql + "PAT.R10 AS NOUSE10				, "
                StrSql = StrSql + "PAT.T1 AS NUM1, "
                StrSql = StrSql + "PAT.T2 AS NUM2, "
                StrSql = StrSql + "PAT.T3 AS NUM3, "
                StrSql = StrSql + "PAT.T4 AS NUM4, "
                StrSql = StrSql + "PAT.T5 AS NUM5, "
                StrSql = StrSql + "PAT.T6 AS NUM6, "
                StrSql = StrSql + "PAT.T7 AS NUM7, "
                StrSql = StrSql + "PAT.T8 AS NUM8, "
                StrSql = StrSql + "PAT.T9 AS NUM9, "
                StrSql = StrSql + "PAT.T10 AS NUM10, "
                StrSql = StrSql + "(NVL(PAT1.WEIGHT,0)*PREF.CONVWT) AS WS1, "
                StrSql = StrSql + "(NVL(PAT2.WEIGHT,0)*PREF.CONVWT) AS WS2, "
                StrSql = StrSql + "(NVL(PAT3.WEIGHT,0)*PREF.CONVWT) AS WS3, "
                StrSql = StrSql + "(NVL(PAT4.WEIGHT,0)*PREF.CONVWT) AS WS4, "
                StrSql = StrSql + "(NVL(PAT5.WEIGHT,0)*PREF.CONVWT) AS WS5, "
                StrSql = StrSql + "(NVL(PAT6.WEIGHT,0)*PREF.CONVWT) AS WS6, "
                StrSql = StrSql + "(NVL(PAT7.WEIGHT,0)*PREF.CONVWT) AS WS7, "
                StrSql = StrSql + "(NVL(PAT8.WEIGHT,0)*PREF.CONVWT) AS WS8, "
                StrSql = StrSql + "(NVL(PAT9.WEIGHT,0)*PREF.CONVWT) AS WS9, "
                StrSql = StrSql + "(NVL(PAT10.WEIGHT,0)*PREF.CONVWT) AS WS10, "
                StrSql = StrSql + "(PAT.W1*PREF.CONVWT) AS WP1, "
                StrSql = StrSql + "(PAT.W2*PREF.CONVWT) AS WP2, "
                StrSql = StrSql + "(PAT.W3*PREF.CONVWT) AS WP3, "
                StrSql = StrSql + "(PAT.W4*PREF.CONVWT) AS WP4, "
                StrSql = StrSql + "(PAT.W5*PREF.CONVWT) AS WP5, "
                StrSql = StrSql + "(PAT.W6*PREF.CONVWT) AS WP6, "
                StrSql = StrSql + "(PAT.W7*PREF.CONVWT) AS WP7, "
                StrSql = StrSql + "(PAT.W8*PREF.CONVWT) AS WP8, "
                StrSql = StrSql + "(PAT.W9*PREF.CONVWT) AS WP9, "
                StrSql = StrSql + "(PAT.W10*PREF.CONVWT) AS WP10, "
                StrSql = StrSql + "(NVL(PAT1.JOULE,0)/PREF.CONVWT) AS ERGYS1, "
                StrSql = StrSql + "(NVL(PAT2.JOULE,0)/PREF.CONVWT) AS ERGYS2, "
                StrSql = StrSql + "(NVL(PAT3.JOULE,0)/PREF.CONVWT) AS ERGYS3, "
                StrSql = StrSql + "(NVL(PAT4.JOULE,0)/PREF.CONVWT) AS ERGYS4, "
                StrSql = StrSql + "(NVL(PAT5.JOULE,0)/PREF.CONVWT) AS ERGYS5, "
                StrSql = StrSql + "(NVL(PAT6.JOULE,0)/PREF.CONVWT) AS ERGYS6, "
                StrSql = StrSql + "(NVL(PAT7.JOULE,0)/PREF.CONVWT) AS ERGYS7, "
                StrSql = StrSql + "(NVL(PAT8.JOULE,0)/PREF.CONVWT) AS ERGYS8, "
                StrSql = StrSql + "(NVL(PAT9.JOULE,0)/PREF.CONVWT) AS ERGYS9, "
                StrSql = StrSql + "(NVL(PAT10.JOULE,0)/PREF.CONVWT) AS ERGYS10, "
                StrSql = StrSql + "(PATERGY.M1/PREF.CONVWT) AS ERGYP1, "
                StrSql = StrSql + "(PATERGY.M2/PREF.CONVWT) AS ERGYP2, "
                StrSql = StrSql + "(PATERGY.M3/PREF.CONVWT) AS ERGYP3, "
                StrSql = StrSql + "(PATERGY.M4/PREF.CONVWT) AS ERGYP4, "
                StrSql = StrSql + "(PATERGY.M5/PREF.CONVWT) AS ERGYP5, "
                StrSql = StrSql + "(PATERGY.M6/PREF.CONVWT) AS ERGYP6, "
                StrSql = StrSql + "(PATERGY.M7/PREF.CONVWT) AS ERGYP7, "
                StrSql = StrSql + "(PATERGY.M8/PREF.CONVWT) AS ERGYP8, "
                StrSql = StrSql + "(PATERGY.M9/PREF.CONVWT) AS ERGYP9, "
                StrSql = StrSql + "(PATERGY.M10/PREF.CONVWT) AS ERGYP10, "
                StrSql = StrSql + "(NVL(PAT1.PRICE,0)) AS GHGS1, "
                StrSql = StrSql + "(NVL(PAT2.PRICE,0)) AS GHGS2, "
                StrSql = StrSql + "(NVL(PAT3.PRICE,0)) AS GHGS3, "
                StrSql = StrSql + "(NVL(PAT4.PRICE,0)) AS GHGS4, "
                StrSql = StrSql + "(NVL(PAT5.PRICE,0)) AS GHGS5, "
                StrSql = StrSql + "(NVL(PAT6.PRICE,0)) AS GHGS6, "
                StrSql = StrSql + "(NVL(PAT7.PRICE,0)) AS GHGS7, "
                StrSql = StrSql + "(NVL(PAT8.PRICE,0)) AS GHGS8, "
                StrSql = StrSql + "(NVL(PAT9.PRICE,0)) AS GHGS9, "
                StrSql = StrSql + "(NVL(PAT10.PRICE,0)) AS GHGS10, "
                StrSql = StrSql + "(PAT.P1) AS GHGP1, "
                StrSql = StrSql + "(PAT.P2) AS GHGP2, "
                StrSql = StrSql + "(PAT.P3) AS GHGP3, "
                StrSql = StrSql + "(PAT.P4) AS GHGP4, "
                StrSql = StrSql + "(PAT.P5) AS GHGP5, "
                StrSql = StrSql + "(PAT.P6) AS GHGP6, "
                StrSql = StrSql + "(PAT.P7) AS GHGP7, "
                StrSql = StrSql + "(PAT.P8) AS GHGP8, "
                StrSql = StrSql + "(PAT.P9) AS GHGP9, "
                StrSql = StrSql + "(PAT.P10) AS GHGP10, "
                'Changes for Water Start
                StrSql = StrSql + "(NVL(PAT1.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS1, "
                StrSql = StrSql + "(NVL(PAT2.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS2, "
                StrSql = StrSql + "(NVL(PAT3.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS3, "
                StrSql = StrSql + "(NVL(PAT4.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS4, "
                StrSql = StrSql + "(NVL(PAT5.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS5, "
                StrSql = StrSql + "(NVL(PAT6.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS6, "
                StrSql = StrSql + "(NVL(PAT7.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS7, "
                StrSql = StrSql + "(NVL(PAT8.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS8, "
                StrSql = StrSql + "(NVL(PAT9.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS9, "
                StrSql = StrSql + "(NVL(PAT10.WATER,0)*PREF.CONVGALLON/PREF.CONVWT) AS WATERS10, "
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
                'Changes for Water end
                StrSql = StrSql + "(NVL(PAT1.RECOVERYSUG,0)) AS RECS1, "
                StrSql = StrSql + "(NVL(PAT2.RECOVERYSUG,0)) AS RECS2, "
                StrSql = StrSql + "(NVL(PAT3.RECOVERYSUG,0)) AS RECS3, "
                StrSql = StrSql + "(NVL(PAT4.RECOVERYSUG,0)) AS RECS4, "
                StrSql = StrSql + "(NVL(PAT5.RECOVERYSUG,0)) AS RECS5, "
                StrSql = StrSql + "(NVL(PAT6.RECOVERYSUG,0)) AS RECS6, "
                StrSql = StrSql + "(NVL(PAT7.RECOVERYSUG,0)) AS RECS7, "
                StrSql = StrSql + "(NVL(PAT8.RECOVERYSUG,0)) AS RECS8, "
                StrSql = StrSql + "(NVL(PAT9.RECOVERYSUG,0)) AS RECS9, "
                StrSql = StrSql + "(NVL(PAT10.RECOVERYSUG,0)) AS RECS10, "
                StrSql = StrSql + "(PAT.REC1) AS RECP1, "
                StrSql = StrSql + "(PAT.REC2) AS RECP2, "
                StrSql = StrSql + "(PAT.REC3) AS RECP3, "
                StrSql = StrSql + "(PAT.REC4) AS RECP4, "
                StrSql = StrSql + "(PAT.REC5) AS RECP5, "
                StrSql = StrSql + "(PAT.REC6) AS RECP6, "
                StrSql = StrSql + "(PAT.REC7) AS RECP7, "
                StrSql = StrSql + "(PAT.REC8) AS RECP8, "
                StrSql = StrSql + "(PAT.REC9) AS RECP9, "
                StrSql = StrSql + "(PAT.REC10) AS RECP10, "
                StrSql = StrSql + "(NVL(PAT1.OSHAFACTOR,'Nothing')) AS SUSMATS1, "
                StrSql = StrSql + "(NVL(PAT2.OSHAFACTOR,'Nothing')) AS SUSMATS2, "
                StrSql = StrSql + "(NVL(PAT3.OSHAFACTOR,'Nothing')) AS SUSMATS3, "
                StrSql = StrSql + "(NVL(PAT4.OSHAFACTOR,'Nothing')) AS SUSMATS4, "
                StrSql = StrSql + "(NVL(PAT5.OSHAFACTOR,'Nothing')) AS SUSMATS5, "
                StrSql = StrSql + "(NVL(PAT6.OSHAFACTOR,'Nothing')) AS SUSMATS6, "
                StrSql = StrSql + "(NVL(PAT7.OSHAFACTOR,'Nothing')) AS SUSMATS7, "
                StrSql = StrSql + "(NVL(PAT8.OSHAFACTOR,'Nothing')) AS SUSMATS8, "
                StrSql = StrSql + "(NVL(PAT9.OSHAFACTOR,'Nothing')) AS SUSMATS9, "
                StrSql = StrSql + "(NVL(PAT10.OSHAFACTOR,'Nothing')) AS SUSMATS10, "
                StrSql = StrSql + "(PAT.OSH1) AS SUSMATP1, "
                StrSql = StrSql + "(PAT.OSH2) AS SUSMATP2, "
                StrSql = StrSql + "(PAT.OSH3) AS SUSMATP3, "
                StrSql = StrSql + "(PAT.OSH4) AS SUSMATP4, "
                StrSql = StrSql + "(PAT.OSH5) AS SUSMATP5, "
                StrSql = StrSql + "(PAT.OSH6) AS SUSMATP6, "
                StrSql = StrSql + "(PAT.OSH7) AS SUSMATP7, "
                StrSql = StrSql + "(PAT.OSH8) AS SUSMATP8, "
                StrSql = StrSql + "(PAT.OSH9) AS SUSMATP9, "
                StrSql = StrSql + "(PAT.OSH10) AS SUSMATP10, "
                StrSql = StrSql + "(NVL(PAT1.POSTCONSUMER,0)) AS PCRECS1, "
                StrSql = StrSql + "(NVL(PAT2.POSTCONSUMER,0)) AS PCRECS2, "
                StrSql = StrSql + "(NVL(PAT3.POSTCONSUMER,0)) AS PCRECS3, "
                StrSql = StrSql + "(NVL(PAT4.POSTCONSUMER,0)) AS PCRECS4, "
                StrSql = StrSql + "(NVL(PAT5.POSTCONSUMER,0)) AS PCRECS5, "
                StrSql = StrSql + "(NVL(PAT6.POSTCONSUMER,0)) AS PCRECS6, "
                StrSql = StrSql + "(NVL(PAT7.POSTCONSUMER,0)) AS PCRECS7, "
                StrSql = StrSql + "(NVL(PAT8.POSTCONSUMER,0)) AS PCRECS8, "
                StrSql = StrSql + "(NVL(PAT9.POSTCONSUMER,0)) AS PCRECS9, "
                StrSql = StrSql + "(NVL(PAT10.POSTCONSUMER,0)) AS PCRECS10, "
                StrSql = StrSql + "(PAT.POC1) AS PCRECP1, "
                StrSql = StrSql + "(PAT.POC2) AS PCRECP2, "
                StrSql = StrSql + "(PAT.POC3) AS PCRECP3, "
                StrSql = StrSql + "(PAT.POC4) AS PCRECP4, "
                StrSql = StrSql + "(PAT.POC5) AS PCRECP5, "
                StrSql = StrSql + "(PAT.POC6) AS PCRECP6, "
                StrSql = StrSql + "(PAT.POC7) AS PCRECP7, "
                StrSql = StrSql + "(PAT.POC8) AS PCRECP8, "
                StrSql = StrSql + "(PAT.POC9) AS PCRECP9, "
                StrSql = StrSql + "(PAT.POC10) AS PCRECP10, "
                StrSql = StrSql + "(NVL(PAT1.SHIP,0)*PREF.CONVTHICK3) AS SDS1, "
                StrSql = StrSql + "(NVL(PAT2.SHIP,0)*PREF.CONVTHICK3) AS SDS2, "
                StrSql = StrSql + "(NVL(PAT3.SHIP,0)*PREF.CONVTHICK3) AS SDS3, "
                StrSql = StrSql + "(NVL(PAT4.SHIP,0)*PREF.CONVTHICK3) AS SDS4, "
                StrSql = StrSql + "(NVL(PAT5.SHIP,0)*PREF.CONVTHICK3) AS SDS5, "
                StrSql = StrSql + "(NVL(PAT6.SHIP,0)*PREF.CONVTHICK3) AS SDS6, "
                StrSql = StrSql + "(NVL(PAT7.SHIP,0)*PREF.CONVTHICK3) AS SDS7, "
                StrSql = StrSql + "(NVL(PAT8.SHIP,0)*PREF.CONVTHICK3) AS SDS8, "
                StrSql = StrSql + "(NVL(PAT9.SHIP,0)*PREF.CONVTHICK3) AS SDS9, "
                StrSql = StrSql + "(NVL(PAT10.SHIP,0)*PREF.CONVTHICK3) AS SDS10, "
                StrSql = StrSql + "(PAT.SD1*PREF.CONVTHICK3) AS SDP1, "
                StrSql = StrSql + "(PAT.SD2*PREF.CONVTHICK3) AS SDP2, "
                StrSql = StrSql + "(PAT.SD3*PREF.CONVTHICK3) AS SDP3, "
                StrSql = StrSql + "(PAT.SD4*PREF.CONVTHICK3) AS SDP4, "
                StrSql = StrSql + "(PAT.SD5*PREF.CONVTHICK3) AS SDP5, "
                StrSql = StrSql + "(PAT.SD6*PREF.CONVTHICK3) AS SDP6, "
                StrSql = StrSql + "(PAT.SD7*PREF.CONVTHICK3) AS SDP7, "
                StrSql = StrSql + "(PAT.SD8*PREF.CONVTHICK3) AS SDP8, "
                StrSql = StrSql + "(PAT.SD9*PREF.CONVTHICK3) AS SDP9, "
                StrSql = StrSql + "(PAT.SD10*PREF.CONVTHICK3) AS SDP10, "
                StrSql = StrSql + "PAT.D1, "
                StrSql = StrSql + "PAT.D2, "
                StrSql = StrSql + "PAT.D3, "
                StrSql = StrSql + "PAT.D4, "
                StrSql = StrSql + "PAT.D5, "
                StrSql = StrSql + "PAT.D6, "
                StrSql = StrSql + "PAT.D7, "
                StrSql = StrSql + "PAT.D8, "
                StrSql = StrSql + "PAT.D9, "
                StrSql = StrSql + "PAT.D10, "
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
                StrSql = StrSql + "TOTAL.TOTALWTPALLETIN* PREF.CONVWT AS TOTWEIGHT, "
                StrSql = StrSql + "TOTAL.TOTALOLDPALLETENERGY/PREF.CONVWT AS TOTENERGY, "
                StrSql = StrSql + "TOTAL.TOTPALLETOLDCO2 as TOTCO2, "
                StrSql = StrSql + "TOTAL.TOTALOLDPALLETRECOVERY AS TOTRECOV, "
                StrSql = StrSql + "TOTAL.TOTALOLDPALLETOSHA AS TOTSUSMAT, "
                StrSql = StrSql + "TOTAL.TOTALOLDPALLETPOC AS TOTREC, "
                StrSql = StrSql + "TOTAL.TOTALOLDPALLETAVGSHIPPING* PREF.CONVTHICK3 AS TOTSHIPDIS, "
                StrSql = StrSql + "TOTAL.TOTALOLDPALLETSHIPPING, "
                'Changes for Water Start
                StrSql = StrSql + "TOTAL.TOTPALLETOLDWATER*PREF.CONVGALLON/PREF.CONVWT as TOTWATER "
                'Changes for Water end
                StrSql = StrSql + "FROM PALLETIN PAT "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PAT1 "
                StrSql = StrSql + "ON PAT1.EFFDATE = PAT.EFFDATE "
                StrSql = StrSql + "AND PAT1.INVENTORYTYPE =  PAT.INVENTORYTYPE "
                StrSql = StrSql + "AND PAT1.PALLETID = PAT.M1 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PAT2 "
                StrSql = StrSql + "ON PAT2.EFFDATE = PAT.EFFDATE "
                StrSql = StrSql + "AND PAT2.INVENTORYTYPE =  PAT.INVENTORYTYPE "
                StrSql = StrSql + "AND PAT2.PALLETID = PAT.M2 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PAT3 "
                StrSql = StrSql + "ON PAT3.EFFDATE = PAT.EFFDATE "
                StrSql = StrSql + "AND PAT3.INVENTORYTYPE =  PAT.INVENTORYTYPE "
                StrSql = StrSql + "AND PAT3.PALLETID = PAT.M3 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PAT4 "
                StrSql = StrSql + "ON PAT4.EFFDATE = PAT.EFFDATE "
                StrSql = StrSql + "AND PAT4.INVENTORYTYPE =  PAT.INVENTORYTYPE "
                StrSql = StrSql + "AND PAT4.PALLETID = PAT.M4 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PAT5 "
                StrSql = StrSql + "ON PAT5.EFFDATE = PAT.EFFDATE "
                StrSql = StrSql + "AND PAT5.INVENTORYTYPE =  PAT.INVENTORYTYPE "
                StrSql = StrSql + "AND PAT5.PALLETID = PAT.M5 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PAT6 "
                StrSql = StrSql + "ON PAT6.EFFDATE = PAT.EFFDATE "
                StrSql = StrSql + "AND PAT6.INVENTORYTYPE =  PAT.INVENTORYTYPE "
                StrSql = StrSql + "AND PAT6.PALLETID = PAT.M6 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PAT7 "
                StrSql = StrSql + "ON PAT7.EFFDATE = PAT.EFFDATE "
                StrSql = StrSql + "AND PAT7.INVENTORYTYPE =  PAT.INVENTORYTYPE "
                StrSql = StrSql + "AND PAT7.PALLETID = PAT.M7 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PAT8 "
                StrSql = StrSql + "ON PAT8.EFFDATE = PAT.EFFDATE "
                StrSql = StrSql + "AND PAT8.INVENTORYTYPE =  PAT.INVENTORYTYPE "
                StrSql = StrSql + "AND PAT8.PALLETID = PAT.M8 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PAT9 "
                StrSql = StrSql + "ON PAT9.EFFDATE = PAT.EFFDATE "
                StrSql = StrSql + "AND PAT9.INVENTORYTYPE =  PAT.INVENTORYTYPE "
                StrSql = StrSql + "AND PAT9.PALLETID = PAT.M9 "
                StrSql = StrSql + "LEFT OUTER JOIN SUSTAIN.PALLETARCH PAT10 "
                StrSql = StrSql + "ON PAT10.EFFDATE = PAT.EFFDATE "
                StrSql = StrSql + "AND PAT10.INVENTORYTYPE =  PAT.INVENTORYTYPE "
                StrSql = StrSql + "AND PAT10.PALLETID = PAT.M10 "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=PAT.CASEID "
                StrSql = StrSql + "INNER JOIN PALLETENERGYPREF PATERGY "
                StrSql = StrSql + "ON PATERGY.CASEID=PAT.CASEID "
                'Changes for Water Start
                StrSql = StrSql + "INNER JOIN PALLETWATERPREF PATWATER "
                StrSql = StrSql + "ON PATWATER.CASEID=PAT.CASEID "
                'Changes for Water End
                StrSql = StrSql + "INNER JOIN TOTAL "
                StrSql = StrSql + "ON TOTAL.CASEID=PAT.CASEID "
                StrSql = StrSql + "WHERE PAT.CASEID = " + CaseId.ToString() + " ORDER BY PAT.CASEID "


                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetPalletInDetails:" + ex.Message.ToString())
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
                StrSql = StrSql + "WHERE PlantCONFIG.CASEID = " + CaseId.ToString() + " ORDER BY PLANTCONFIG.CASEID"
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetPlantConfigDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetEffiencyDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT  "
                StrSql = StrSql + "MATERIALEFF.CASEID, "
                StrSql = StrSql + "(MATERIALS1.MATDE1 ||' '|| MATERIALS1.MATDE2  )  AS MAT1, "
                StrSql = StrSql + "(MATERIALS2.MATDE1 ||' '|| MATERIALS2.MATDE2  )  AS MAT2, "
                StrSql = StrSql + "(MATERIALS3.MATDE1 ||' '|| MATERIALS3.MATDE2  )  AS MAT3, "
                StrSql = StrSql + "(MATERIALS4.MATDE1 ||' '|| MATERIALS4.MATDE2  )  AS MAT4, "
                StrSql = StrSql + "(MATERIALS5.MATDE1 ||' '|| MATERIALS5.MATDE2  )  AS MAT5, "
                StrSql = StrSql + "(MATERIALS6.MATDE1 ||' '|| MATERIALS6.MATDE2  )  AS MAT6, "
                StrSql = StrSql + "(MATERIALS7.MATDE1 ||' '|| MATERIALS7.MATDE2  )  AS MAT7, "
                StrSql = StrSql + "(MATERIALS8.MATDE1 ||' '|| MATERIALS8.MATDE2  )  AS MAT8, "
                StrSql = StrSql + "(MATERIALS9.MATDE1 ||' '|| MATERIALS9.MATDE2  )  AS MAT9, "
                StrSql = StrSql + "(MATERIALS10.MATDE1 ||' '|| MATERIALS10.MATDE2  )  AS MAT10, "
                StrSql = StrSql + "(PROCESS1.PROCDE1 || ' ' ||  PROCESS1.PROCDE2) AS PROC1, "
                StrSql = StrSql + "(PROCESS2.PROCDE1 || ' ' ||  PROCESS2.PROCDE2) AS PROC2, "
                StrSql = StrSql + "(PROCESS3.PROCDE1 || ' ' ||  PROCESS3.PROCDE2) AS PROC3, "
                StrSql = StrSql + "(PROCESS4.PROCDE1 || ' ' ||  PROCESS4.PROCDE2) AS PROC4, "
                StrSql = StrSql + "(PROCESS5.PROCDE1 || ' ' ||  PROCESS5.PROCDE2) AS PROC5, "
                StrSql = StrSql + "(PROCESS6.PROCDE1 || ' ' ||  PROCESS6.PROCDE2) AS PROC6, "
                StrSql = StrSql + "(PROCESS7.PROCDE1 || ' ' ||  PROCESS7.PROCDE2) AS PROC7, "
                StrSql = StrSql + "(PROCESS8.PROCDE1 || ' ' ||  PROCESS8.PROCDE2) AS PROC8, "
                StrSql = StrSql + "(PROCESS9.PROCDE1 || ' ' ||  PROCESS9.PROCDE2) AS PROC9, "
                StrSql = StrSql + "(PROCESS10.PROCDE1 || ' ' ||  PROCESS10.PROCDE2) AS PROC10, "
                StrSql = StrSql + "MATERIALEFF.T1 AS DEPA1, "
                StrSql = StrSql + "MATERIALEFF.T2 AS DEPA2, "
                StrSql = StrSql + "MATERIALEFF.T3 AS DEPA3, "
                StrSql = StrSql + "MATERIALEFF.T4 AS DEPA4, "
                StrSql = StrSql + "MATERIALEFF.T5 AS DEPA5, "
                StrSql = StrSql + "MATERIALEFF.T6 AS DEPA6, "
                StrSql = StrSql + "MATERIALEFF.T7 AS DEPA7, "
                StrSql = StrSql + "MATERIALEFF.T8 AS DEPA8, "
                StrSql = StrSql + "MATERIALEFF.T9 AS DEPA9, "
                StrSql = StrSql + "MATERIALEFF.T10 AS DEPA10, "
                StrSql = StrSql + "MATERIALEFF.S1 AS DEPB1, "
                StrSql = StrSql + "MATERIALEFF.S2 AS DEPB2, "
                StrSql = StrSql + "MATERIALEFF.S3 AS DEPB3, "
                StrSql = StrSql + "MATERIALEFF.S4 AS DEPB4, "
                StrSql = StrSql + "MATERIALEFF.S5 AS DEPB5, "
                StrSql = StrSql + "MATERIALEFF.S6 AS DEPB6, "
                StrSql = StrSql + "MATERIALEFF.S7 AS DEPB7, "
                StrSql = StrSql + "MATERIALEFF.S8 AS DEPB8, "
                StrSql = StrSql + "MATERIALEFF.S9 AS DEPB9, "
                StrSql = StrSql + "MATERIALEFF.S10 AS DEPB10, "
                StrSql = StrSql + "MATERIALEFF.Y1 AS DEPC1, "
                StrSql = StrSql + "MATERIALEFF.Y2 AS DEPC2, "
                StrSql = StrSql + "MATERIALEFF.Y3 AS DEPC3, "
                StrSql = StrSql + "MATERIALEFF.Y4 AS DEPC4, "
                StrSql = StrSql + "MATERIALEFF.Y5 AS DEPC5, "
                StrSql = StrSql + "MATERIALEFF.Y6 AS DEPC6, "
                StrSql = StrSql + "MATERIALEFF.Y7 AS DEPC7, "
                StrSql = StrSql + "MATERIALEFF.Y8 AS DEPC8, "
                StrSql = StrSql + "MATERIALEFF.Y9 AS DEPC9, "
                StrSql = StrSql + "MATERIALEFF.Y10 AS DEPC10, "
                StrSql = StrSql + "MATERIALEFF.D1 AS DEPD1, "
                StrSql = StrSql + "MATERIALEFF.D2 AS DEPD2, "
                StrSql = StrSql + "MATERIALEFF.D3 AS DEPD3, "
                StrSql = StrSql + "MATERIALEFF.D4 AS DEPD4, "
                StrSql = StrSql + "MATERIALEFF.D5 AS DEPD5, "
                StrSql = StrSql + "MATERIALEFF.D6 AS DEPD6, "
                StrSql = StrSql + "MATERIALEFF.D7 AS DEPD7, "
                StrSql = StrSql + "MATERIALEFF.D8 AS DEPD8, "
                StrSql = StrSql + "MATERIALEFF.D9 AS DEPD9, "
                StrSql = StrSql + "MATERIALEFF.D10 AS DEPD10, "
                StrSql = StrSql + "MATERIALEFF.E1 AS DEPE1, "
                StrSql = StrSql + "MATERIALEFF.E2 AS DEPE2, "
                StrSql = StrSql + "MATERIALEFF.E3 AS DEPE3, "
                StrSql = StrSql + "MATERIALEFF.E4 AS DEPE4, "
                StrSql = StrSql + "MATERIALEFF.E5 AS DEPE5, "
                StrSql = StrSql + "MATERIALEFF.E6 AS DEPE6, "
                StrSql = StrSql + "MATERIALEFF.E7 AS DEPE7, "
                StrSql = StrSql + "MATERIALEFF.E8 AS DEPE8, "
                StrSql = StrSql + "MATERIALEFF.E9 AS DEPE9, "
                StrSql = StrSql + "MATERIALEFF.E10 AS DEPE10, "
                StrSql = StrSql + "MATERIALEFF.Z1 AS DEPF1, "
                StrSql = StrSql + "MATERIALEFF.Z2 AS DEPF2, "
                StrSql = StrSql + "MATERIALEFF.Z3 AS DEPF3, "
                StrSql = StrSql + "MATERIALEFF.Z4 AS DEPF4, "
                StrSql = StrSql + "MATERIALEFF.Z5 AS DEPF5, "
                StrSql = StrSql + "MATERIALEFF.Z6 AS DEPF6, "
                StrSql = StrSql + "MATERIALEFF.Z7 AS DEPF7, "
                StrSql = StrSql + "MATERIALEFF.Z8 AS DEPF8, "
                StrSql = StrSql + "MATERIALEFF.Z9 AS DEPF9, "
                StrSql = StrSql + "MATERIALEFF.Z10 AS DEPF10, "
                StrSql = StrSql + "MATERIALEFF.B1 AS DEPG1, "
                StrSql = StrSql + "MATERIALEFF.B2 AS DEPG2, "
                StrSql = StrSql + "MATERIALEFF.B3 AS DEPG3, "
                StrSql = StrSql + "MATERIALEFF.B4 AS DEPG4, "
                StrSql = StrSql + "MATERIALEFF.B5 AS DEPG5, "
                StrSql = StrSql + "MATERIALEFF.B6 AS DEPG6, "
                StrSql = StrSql + "MATERIALEFF.B7 AS DEPG7, "
                StrSql = StrSql + "MATERIALEFF.B8 AS DEPG8, "
                StrSql = StrSql + "MATERIALEFF.B9 AS DEPG9, "
                StrSql = StrSql + "MATERIALEFF.B10 AS DEPG10, "
                StrSql = StrSql + "MATERIALEFF.R1 AS DEPH1, "
                StrSql = StrSql + "MATERIALEFF.R2 AS DEPH2, "
                StrSql = StrSql + "MATERIALEFF.R3 AS DEPH3, "
                StrSql = StrSql + "MATERIALEFF.R4 AS DEPH4, "
                StrSql = StrSql + "MATERIALEFF.R5 AS DEPH5, "
                StrSql = StrSql + "MATERIALEFF.R6 AS DEPH6, "
                StrSql = StrSql + "MATERIALEFF.R7 AS DEPH7, "
                StrSql = StrSql + "MATERIALEFF.R8 AS DEPH8, "
                StrSql = StrSql + "MATERIALEFF.R9 AS DEPH9, "
                StrSql = StrSql + "MATERIALEFF.R10 AS DEPH10, "
                StrSql = StrSql + "MATERIALEFF.K1 AS DEPI1, "
                StrSql = StrSql + "MATERIALEFF.K2 AS DEPI2, "
                StrSql = StrSql + "MATERIALEFF.K3 AS DEPI3, "
                StrSql = StrSql + "MATERIALEFF.K4 AS DEPI4, "
                StrSql = StrSql + "MATERIALEFF.K5 AS DEPI5, "
                StrSql = StrSql + "MATERIALEFF.K6 AS DEPI6, "
                StrSql = StrSql + "MATERIALEFF.K7 AS DEPI7, "
                StrSql = StrSql + "MATERIALEFF.K8 AS DEPI8, "
                StrSql = StrSql + "MATERIALEFF.K9 AS DEPI9, "
                StrSql = StrSql + "MATERIALEFF.K10 AS DEPI10, "
                StrSql = StrSql + "MATERIALEFF.P1 AS DEPJ1, "
                StrSql = StrSql + "MATERIALEFF.P2 AS DEPJ2, "
                StrSql = StrSql + "MATERIALEFF.P3 AS DEPJ3, "
                StrSql = StrSql + "MATERIALEFF.P4 AS DEPJ4, "
                StrSql = StrSql + "MATERIALEFF.P5 AS DEPJ5, "
                StrSql = StrSql + "MATERIALEFF.P6 AS DEPJ6, "
                StrSql = StrSql + "MATERIALEFF.P7 AS DEPJ7, "
                StrSql = StrSql + "MATERIALEFF.P8 AS DEPJ8, "
                StrSql = StrSql + "MATERIALEFF.P9 AS DEPJ9, "
                StrSql = StrSql + "MATERIALEFF.P10 AS DEPJ10 "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "MATERIALEFF "
                StrSql = StrSql + "INNER JOIN  MATERIALINPUT "
                StrSql = StrSql + "ON  MATERIALINPUT.CASEID = MATERIALEFF.CASEID "
                StrSql = StrSql + "INNER JOIN  PLANTCONFIG "
                StrSql = StrSql + "ON  PLANTCONFIG.CASEID = MATERIALEFF.CASEID "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATERIALS1 "
                StrSql = StrSql + "ON MATERIALS1.MATID = MATERIALINPUT.M1 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATERIALS2 "
                StrSql = StrSql + "ON MATERIALS2.MATID = MATERIALINPUT.M2 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATERIALS3 "
                StrSql = StrSql + "ON MATERIALS3.MATID = MATERIALINPUT.M3 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATERIALS4 "
                StrSql = StrSql + "ON MATERIALS4.MATID = MATERIALINPUT.M4 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATERIALS5 "
                StrSql = StrSql + "ON MATERIALS5.MATID = MATERIALINPUT.M5 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATERIALS6 "
                StrSql = StrSql + "ON MATERIALS6.MATID = MATERIALINPUT.M6 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATERIALS7 "
                StrSql = StrSql + "ON MATERIALS7.MATID = MATERIALINPUT.M7 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATERIALS8 "
                StrSql = StrSql + "ON MATERIALS8.MATID = MATERIALINPUT.M8 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATERIALS9 "
                StrSql = StrSql + "ON MATERIALS9.MATID = MATERIALINPUT.M9 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MATERIALS10 "
                StrSql = StrSql + "ON MATERIALS10.MATID = MATERIALINPUT.M10 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS PROCESS1 "
                StrSql = StrSql + "ON PROCESS1.PROCID = PLANTCONFIG.M1 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS PROCESS2 "
                StrSql = StrSql + "ON PROCESS2.PROCID = PLANTCONFIG.M2 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS PROCESS3 "
                StrSql = StrSql + "ON PROCESS3.PROCID = PLANTCONFIG.M3 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS PROCESS4 "
                StrSql = StrSql + "ON PROCESS4.PROCID = PLANTCONFIG.M4 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS PROCESS5 "
                StrSql = StrSql + "ON PROCESS5.PROCID = PLANTCONFIG.M5 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS PROCESS6 "
                StrSql = StrSql + "ON PROCESS6.PROCID = PLANTCONFIG.M6 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS PROCESS7 "
                StrSql = StrSql + "ON PROCESS7.PROCID = PLANTCONFIG.M7 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS PROCESS8 "
                StrSql = StrSql + "ON PROCESS8.PROCID = PLANTCONFIG.M8 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS PROCESS9 "
                StrSql = StrSql + "ON PROCESS9.PROCID = PLANTCONFIG.M9 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS PROCESS10 "
                StrSql = StrSql + "ON PROCESS10.PROCID = PLANTCONFIG.M10 "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "MATERIALEFF.CASEID=" + CaseId.ToString() + " ORDER BY MATERIALEFF.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetEffiencyDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetEquipmentInDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  	DISTINCT  "
                StrSql = StrSql + "EQUIPMENTTYPE.CASEID, "
                StrSql = StrSql + "EQUIPMENTTYPE.M1, "
                StrSql = StrSql + "EQUIPMENTTYPE.M2, "
                StrSql = StrSql + "EQUIPMENTTYPE.M3, "
                StrSql = StrSql + "EQUIPMENTTYPE.M4, "
                StrSql = StrSql + "EQUIPMENTTYPE.M5, "
                StrSql = StrSql + "EQUIPMENTTYPE.M6, "
                StrSql = StrSql + "EQUIPMENTTYPE.M7, "
                StrSql = StrSql + "EQUIPMENTTYPE.M8, "
                StrSql = StrSql + "EQUIPMENTTYPE.M9, "
                StrSql = StrSql + "EQUIPMENTTYPE.M10, "
                StrSql = StrSql + "EQUIPMENTTYPE.M11, "
                StrSql = StrSql + "EQUIPMENTTYPE.M12, "
                StrSql = StrSql + "EQUIPMENTTYPE.M13, "
                StrSql = StrSql + "EQUIPMENTTYPE.M14, "
                StrSql = StrSql + "EQUIPMENTTYPE.M15, "
                StrSql = StrSql + "EQUIPMENTTYPE.M16, "
                StrSql = StrSql + "EQUIPMENTTYPE.M17, "
                StrSql = StrSql + "EQUIPMENTTYPE.M18, "
                StrSql = StrSql + "EQUIPMENTTYPE.M19, "
                StrSql = StrSql + "EQUIPMENTTYPE.M20, "
                StrSql = StrSql + "EQUIPMENTTYPE.M21, "
                StrSql = StrSql + "EQUIPMENTTYPE.M22, "
                StrSql = StrSql + "EQUIPMENTTYPE.M23, "
                StrSql = StrSql + "EQUIPMENTTYPE.M24, "
                StrSql = StrSql + "EQUIPMENTTYPE.M25, "
                StrSql = StrSql + "EQUIPMENTTYPE.M26, "
                StrSql = StrSql + "EQUIPMENTTYPE.M27, "
                StrSql = StrSql + "EQUIPMENTTYPE.M28, "
                StrSql = StrSql + "EQUIPMENTTYPE.M29, "
                StrSql = StrSql + "EQUIPMENTTYPE.M30, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET1.AREATYPE )AREATYPE1, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET2.AREATYPE )AREATYPE2, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET3.AREATYPE )AREATYPE3, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET4.AREATYPE )AREATYPE4, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET5.AREATYPE )AREATYPE5, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET6.AREATYPE )AREATYPE6, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET7.AREATYPE )AREATYPE7, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET8.AREATYPE )AREATYPE8, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET9.AREATYPE )AREATYPE9, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET10.AREATYPE )AREATYPE10, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET11.AREATYPE )AREATYPE11, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET12.AREATYPE )AREATYPE12, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET13.AREATYPE )AREATYPE13, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET14.AREATYPE )AREATYPE14, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET15.AREATYPE )AREATYPE15, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET16.AREATYPE )AREATYPE16, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET17.AREATYPE )AREATYPE17, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET18.AREATYPE )AREATYPE18, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET19.AREATYPE )AREATYPE19, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET20.AREATYPE )AREATYPE20, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET21.AREATYPE )AREATYPE21, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET22.AREATYPE )AREATYPE22, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET23.AREATYPE )AREATYPE23, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET24.AREATYPE )AREATYPE24, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET25.AREATYPE )AREATYPE25, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET26.AREATYPE )AREATYPE26, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET27.AREATYPE )AREATYPE27, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET28.AREATYPE )AREATYPE28, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET29.AREATYPE )AREATYPE29, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=ASSET30.AREATYPE )AREATYPE30, "
                StrSql = StrSql + "(ASSET1.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS1, "
                StrSql = StrSql + "(ASSET2.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS2, "
                StrSql = StrSql + "(ASSET3.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS3, "
                StrSql = StrSql + "(ASSET4.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS4, "
                StrSql = StrSql + "(ASSET5.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS5, "
                StrSql = StrSql + "(ASSET6.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS6, "
                StrSql = StrSql + "(ASSET7.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS7, "
                StrSql = StrSql + "(ASSET8.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS8, "
                StrSql = StrSql + "(ASSET9.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS9, "
                StrSql = StrSql + "(ASSET10.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS10, "
                StrSql = StrSql + "(ASSET11.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS11, "
                StrSql = StrSql + "(ASSET12.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS12, "
                StrSql = StrSql + "(ASSET13.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS13, "
                StrSql = StrSql + "(ASSET14.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS14, "
                StrSql = StrSql + "(ASSET15.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS15, "
                StrSql = StrSql + "(ASSET16.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS16, "
                StrSql = StrSql + "(ASSET17.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS17, "
                StrSql = StrSql + "(ASSET18.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS18, "
                StrSql = StrSql + "(ASSET19.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS19, "
                StrSql = StrSql + "(ASSET20.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS20, "
                StrSql = StrSql + "(ASSET21.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS21, "
                StrSql = StrSql + "(ASSET22.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS22, "
                StrSql = StrSql + "(ASSET23.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS23, "
                StrSql = StrSql + "(ASSET24.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS24, "
                StrSql = StrSql + "(ASSET25.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS25, "
                StrSql = StrSql + "(ASSET26.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS26, "
                StrSql = StrSql + "(ASSET27.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS27, "
                StrSql = StrSql + "(ASSET28.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS28, "
                StrSql = StrSql + "(ASSET29.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS29, "
                StrSql = StrSql + "(ASSET30.AREA * PREFERENCES.CONVAREA2) AS PLANTAREAS30, "
                StrSql = StrSql + "(EQUIPMENTAREA.M1 * PREFERENCES.CONVAREA2) AS PLANTAREAP1, "
                StrSql = StrSql + "(EQUIPMENTAREA.M2 * PREFERENCES.CONVAREA2) AS PLANTAREAP2, "
                StrSql = StrSql + "(EQUIPMENTAREA.M3 * PREFERENCES.CONVAREA2) AS PLANTAREAP3, "
                StrSql = StrSql + "(EQUIPMENTAREA.M4 * PREFERENCES.CONVAREA2) AS PLANTAREAP4, "
                StrSql = StrSql + "(EQUIPMENTAREA.M5 * PREFERENCES.CONVAREA2) AS PLANTAREAP5, "
                StrSql = StrSql + "(EQUIPMENTAREA.M6 * PREFERENCES.CONVAREA2) AS PLANTAREAP6, "
                StrSql = StrSql + "(EQUIPMENTAREA.M7 * PREFERENCES.CONVAREA2) AS PLANTAREAP7, "
                StrSql = StrSql + "(EQUIPMENTAREA.M8 * PREFERENCES.CONVAREA2) AS PLANTAREAP8, "
                StrSql = StrSql + "(EQUIPMENTAREA.M9 * PREFERENCES.CONVAREA2) AS PLANTAREAP9, "
                StrSql = StrSql + "(EQUIPMENTAREA.M10 * PREFERENCES.CONVAREA2) AS PLANTAREAP10, "
                StrSql = StrSql + "(EQUIPMENTAREA.M11 * PREFERENCES.CONVAREA2) AS PLANTAREAP11, "
                StrSql = StrSql + "(EQUIPMENTAREA.M12 * PREFERENCES.CONVAREA2) AS PLANTAREAP12, "
                StrSql = StrSql + "(EQUIPMENTAREA.M13 * PREFERENCES.CONVAREA2) AS PLANTAREAP13, "
                StrSql = StrSql + "(EQUIPMENTAREA.M14 * PREFERENCES.CONVAREA2) AS PLANTAREAP14, "
                StrSql = StrSql + "(EQUIPMENTAREA.M15 * PREFERENCES.CONVAREA2) AS PLANTAREAP15, "
                StrSql = StrSql + "(EQUIPMENTAREA.M16 * PREFERENCES.CONVAREA2) AS PLANTAREAP16, "
                StrSql = StrSql + "(EQUIPMENTAREA.M17 * PREFERENCES.CONVAREA2) AS PLANTAREAP17, "
                StrSql = StrSql + "(EQUIPMENTAREA.M18 * PREFERENCES.CONVAREA2) AS PLANTAREAP18, "
                StrSql = StrSql + "(EQUIPMENTAREA.M19 * PREFERENCES.CONVAREA2) AS PLANTAREAP19, "
                StrSql = StrSql + "(EQUIPMENTAREA.M20 * PREFERENCES.CONVAREA2) AS PLANTAREAP20, "
                StrSql = StrSql + "(EQUIPMENTAREA.M21 * PREFERENCES.CONVAREA2) AS PLANTAREAP21, "
                StrSql = StrSql + "(EQUIPMENTAREA.M22 * PREFERENCES.CONVAREA2) AS PLANTAREAP22, "
                StrSql = StrSql + "(EQUIPMENTAREA.M23 * PREFERENCES.CONVAREA2) AS PLANTAREAP23, "
                StrSql = StrSql + "(EQUIPMENTAREA.M24 * PREFERENCES.CONVAREA2) AS PLANTAREAP24, "
                StrSql = StrSql + "(EQUIPMENTAREA.M25 * PREFERENCES.CONVAREA2) AS PLANTAREAP25, "
                StrSql = StrSql + "(EQUIPMENTAREA.M26 * PREFERENCES.CONVAREA2) AS PLANTAREAP26, "
                StrSql = StrSql + "(EQUIPMENTAREA.M27 * PREFERENCES.CONVAREA2) AS PLANTAREAP27, "
                StrSql = StrSql + "(EQUIPMENTAREA.M28 * PREFERENCES.CONVAREA2) AS PLANTAREAP28, "
                StrSql = StrSql + "(EQUIPMENTAREA.M29 * PREFERENCES.CONVAREA2) AS PLANTAREAP29, "
                StrSql = StrSql + "(EQUIPMENTAREA.M30 * PREFERENCES.CONVAREA2) AS PLANTAREAP30, "
                StrSql = StrSql + "(ASSET1.INSTKW ) AS ERGYS1, "
                StrSql = StrSql + "(ASSET2.INSTKW ) AS ERGYS2, "
                StrSql = StrSql + "(ASSET3.INSTKW ) AS ERGYS3, "
                StrSql = StrSql + "(ASSET4.INSTKW ) AS ERGYS4, "
                StrSql = StrSql + "(ASSET5.INSTKW ) AS ERGYS5, "
                StrSql = StrSql + "(ASSET6.INSTKW ) AS ERGYS6, "
                StrSql = StrSql + "(ASSET7.INSTKW ) AS ERGYS7, "
                StrSql = StrSql + "(ASSET8.INSTKW ) AS ERGYS8, "
                StrSql = StrSql + "(ASSET9.INSTKW ) AS ERGYS9, "
                StrSql = StrSql + "(ASSET10.INSTKW ) AS ERGYS10, "
                StrSql = StrSql + "(ASSET11.INSTKW ) AS ERGYS11, "
                StrSql = StrSql + "(ASSET12.INSTKW ) AS ERGYS12, "
                StrSql = StrSql + "(ASSET13.INSTKW ) AS ERGYS13, "
                StrSql = StrSql + "(ASSET14.INSTKW ) AS ERGYS14, "
                StrSql = StrSql + "(ASSET15.INSTKW ) AS ERGYS15, "
                StrSql = StrSql + "(ASSET16.INSTKW ) AS ERGYS16, "
                StrSql = StrSql + "(ASSET17.INSTKW ) AS ERGYS17, "
                StrSql = StrSql + "(ASSET18.INSTKW ) AS ERGYS18, "
                StrSql = StrSql + "(ASSET19.INSTKW ) AS ERGYS19, "
                StrSql = StrSql + "(ASSET20.INSTKW ) AS ERGYS20, "
                StrSql = StrSql + "(ASSET21.INSTKW ) AS ERGYS21, "
                StrSql = StrSql + "(ASSET22.INSTKW ) AS ERGYS22, "
                StrSql = StrSql + "(ASSET23.INSTKW ) AS ERGYS23, "
                StrSql = StrSql + "(ASSET24.INSTKW ) AS ERGYS24, "
                StrSql = StrSql + "(ASSET25.INSTKW ) AS ERGYS25, "
                StrSql = StrSql + "(ASSET26.INSTKW ) AS ERGYS26, "
                StrSql = StrSql + "(ASSET27.INSTKW ) AS ERGYS27, "
                StrSql = StrSql + "(ASSET28.INSTKW ) AS ERGYS28, "
                StrSql = StrSql + "(ASSET29.INSTKW ) AS ERGYS29, "
                StrSql = StrSql + "(ASSET30.INSTKW ) AS ERGYS30, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M1 ) AS ERGYP1, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M2 ) AS ERGYP2, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M3 ) AS ERGYP3, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M4 ) AS ERGYP4, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M5 ) AS ERGYP5, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M6 ) AS ERGYP6, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M7 ) AS ERGYP7, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M8 ) AS ERGYP8, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M9 ) AS ERGYP9, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M10 ) AS ERGYP10, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M11 ) AS ERGYP11, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M12 ) AS ERGYP12, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M13 ) AS ERGYP13, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M14 ) AS ERGYP14, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M15 ) AS ERGYP15, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M16 ) AS ERGYP16, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M17 ) AS ERGYP17, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M18 ) AS ERGYP18, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M19 ) AS ERGYP19, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M20 ) AS ERGYP20, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M21 ) AS ERGYP21, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M22 ) AS ERGYP22, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M23 ) AS ERGYP23, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M24 ) AS ERGYP24, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M25 ) AS ERGYP25, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M26 ) AS ERGYP26, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M27 ) AS ERGYP27, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M28 ) AS ERGYP28, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M29 ) AS ERGYP29, "
                StrSql = StrSql + "(EQUIPENERGYPREF.M30 ) AS ERGYP30, "
                StrSql = StrSql + "(ASSET1.NTGKW ) AS NTGS1, "
                StrSql = StrSql + "(ASSET2.NTGKW ) AS NTGS2, "
                StrSql = StrSql + "(ASSET3.NTGKW ) AS NTGS3, "
                StrSql = StrSql + "(ASSET4.NTGKW ) AS NTGS4, "
                StrSql = StrSql + "(ASSET5.NTGKW ) AS NTGS5, "
                StrSql = StrSql + "(ASSET6.NTGKW ) AS NTGS6, "
                StrSql = StrSql + "(ASSET7.NTGKW ) AS NTGS7, "
                StrSql = StrSql + "(ASSET8.NTGKW ) AS NTGS8, "
                StrSql = StrSql + "(ASSET9.NTGKW ) AS NTGS9, "
                StrSql = StrSql + "(ASSET10.NTGKW ) AS NTGS10, "
                StrSql = StrSql + "(ASSET11.NTGKW ) AS NTGS11, "
                StrSql = StrSql + "(ASSET12.NTGKW ) AS NTGS12, "
                StrSql = StrSql + "(ASSET13.NTGKW ) AS NTGS13, "
                StrSql = StrSql + "(ASSET14.NTGKW ) AS NTGS14, "
                StrSql = StrSql + "(ASSET15.NTGKW ) AS NTGS15, "
                StrSql = StrSql + "(ASSET16.NTGKW ) AS NTGS16, "
                StrSql = StrSql + "(ASSET17.NTGKW ) AS NTGS17, "
                StrSql = StrSql + "(ASSET18.NTGKW ) AS NTGS18, "
                StrSql = StrSql + "(ASSET19.NTGKW ) AS NTGS19, "
                StrSql = StrSql + "(ASSET20.NTGKW ) AS NTGS20, "
                StrSql = StrSql + "(ASSET21.NTGKW ) AS NTGS21, "
                StrSql = StrSql + "(ASSET22.NTGKW ) AS NTGS22, "
                StrSql = StrSql + "(ASSET23.NTGKW ) AS NTGS23, "
                StrSql = StrSql + "(ASSET24.NTGKW ) AS NTGS24, "
                StrSql = StrSql + "(ASSET25.NTGKW ) AS NTGS25, "
                StrSql = StrSql + "(ASSET26.NTGKW ) AS NTGS26, "
                StrSql = StrSql + "(ASSET27.NTGKW ) AS NTGS27, "
                StrSql = StrSql + "(ASSET28.NTGKW ) AS NTGS28, "
                StrSql = StrSql + "(ASSET29.NTGKW ) AS NTGS29, "
                StrSql = StrSql + "(ASSET30.NTGKW ) AS NTGS30, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M1 ) AS NTGP1, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M2 ) AS NTGP2, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M3 ) AS NTGP3, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M4 ) AS NTGP4, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M5 ) AS NTGP5, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M6 ) AS NTGP6, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M7 ) AS NTGP7, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M8 ) AS NTGP8, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M9 ) AS NTGP9, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M10 ) AS NTGP10, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M11 ) AS NTGP11, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M12 ) AS NTGP12, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M13 ) AS NTGP13, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M14 ) AS NTGP14, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M15 ) AS NTGP15, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M16 ) AS NTGP16, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M17 ) AS NTGP17, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M18 ) AS NTGP18, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M19 ) AS NTGP19, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M20 ) AS NTGP20, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M21 ) AS NTGP21, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M22 ) AS NTGP22, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M23 ) AS NTGP23, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M24 ) AS NTGP24, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M25 ) AS NTGP25, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M26 ) AS NTGP26, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M27 ) AS NTGP27, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M28 ) AS NTGP28, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M29 ) AS NTGP29, "
                StrSql = StrSql + "(EQUIPNATURALGASPREF.M30 ) AS NTGP30, "
                'Changes for Water Start
                StrSql = StrSql + "(ASSET1.WATERKW*PREF.CONVGALLON ) AS WATERS1, "
                StrSql = StrSql + "(ASSET2.WATERKW*PREF.CONVGALLON ) AS WATERS2, "
                StrSql = StrSql + "(ASSET3.WATERKW*PREF.CONVGALLON ) AS WATERS3, "
                StrSql = StrSql + "(ASSET4.WATERKW*PREF.CONVGALLON ) AS WATERS4, "
                StrSql = StrSql + "(ASSET5.WATERKW*PREF.CONVGALLON ) AS WATERS5, "
                StrSql = StrSql + "(ASSET6.WATERKW*PREF.CONVGALLON ) AS WATERS6, "
                StrSql = StrSql + "(ASSET7.WATERKW*PREF.CONVGALLON ) AS WATERS7, "
                StrSql = StrSql + "(ASSET8.WATERKW*PREF.CONVGALLON ) AS WATERS8, "
                StrSql = StrSql + "(ASSET9.WATERKW*PREF.CONVGALLON ) AS WATERS9, "
                StrSql = StrSql + "(ASSET10.WATERKW*PREF.CONVGALLON ) AS WATERS10, "
                StrSql = StrSql + "(ASSET11.WATERKW*PREF.CONVGALLON ) AS WATERS11, "
                StrSql = StrSql + "(ASSET12.WATERKW*PREF.CONVGALLON ) AS WATERS12, "
                StrSql = StrSql + "(ASSET13.WATERKW*PREF.CONVGALLON ) AS WATERS13, "
                StrSql = StrSql + "(ASSET14.WATERKW*PREF.CONVGALLON ) AS WATERS14, "
                StrSql = StrSql + "(ASSET15.WATERKW*PREF.CONVGALLON ) AS WATERS15, "
                StrSql = StrSql + "(ASSET16.WATERKW*PREF.CONVGALLON ) AS WATERS16, "
                StrSql = StrSql + "(ASSET17.WATERKW*PREF.CONVGALLON ) AS WATERS17, "
                StrSql = StrSql + "(ASSET18.WATERKW*PREF.CONVGALLON ) AS WATERS18, "
                StrSql = StrSql + "(ASSET19.WATERKW*PREF.CONVGALLON ) AS WATERS19, "
                StrSql = StrSql + "(ASSET20.WATERKW*PREF.CONVGALLON ) AS WATERS20, "
                StrSql = StrSql + "(ASSET21.WATERKW*PREF.CONVGALLON ) AS WATERS21, "
                StrSql = StrSql + "(ASSET22.WATERKW*PREF.CONVGALLON ) AS WATERS22, "
                StrSql = StrSql + "(ASSET23.WATERKW*PREF.CONVGALLON ) AS WATERS23, "
                StrSql = StrSql + "(ASSET24.WATERKW*PREF.CONVGALLON ) AS WATERS24, "
                StrSql = StrSql + "(ASSET25.WATERKW*PREF.CONVGALLON ) AS WATERS25, "
                StrSql = StrSql + "(ASSET26.WATERKW*PREF.CONVGALLON ) AS WATERS26, "
                StrSql = StrSql + "(ASSET27.WATERKW*PREF.CONVGALLON ) AS WATERS27, "
                StrSql = StrSql + "(ASSET28.WATERKW*PREF.CONVGALLON ) AS WATERS28, "
                StrSql = StrSql + "(ASSET29.WATERKW*PREF.CONVGALLON ) AS WATERS29, "
                StrSql = StrSql + "(ASSET30.WATERKW*PREF.CONVGALLON ) AS WATERS30, "
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
                'Changes for Water end
                StrSql = StrSql + "(EQUIPMENTDEP.M1 ) AS DEPT1, "
                StrSql = StrSql + "(EQUIPMENTDEP.M2 ) AS DEPT2, "
                StrSql = StrSql + "(EQUIPMENTDEP.M3 ) AS DEPT3, "
                StrSql = StrSql + "(EQUIPMENTDEP.M4 ) AS DEPT4, "
                StrSql = StrSql + "(EQUIPMENTDEP.M5 ) AS DEPT5, "
                StrSql = StrSql + "(EQUIPMENTDEP.M6 ) AS DEPT6, "
                StrSql = StrSql + "(EQUIPMENTDEP.M7 ) AS DEPT7, "
                StrSql = StrSql + "(EQUIPMENTDEP.M8 ) AS DEPT8, "
                StrSql = StrSql + "(EQUIPMENTDEP.M9 ) AS DEPT9, "
                StrSql = StrSql + "(EQUIPMENTDEP.M10 ) AS DEPT10, "
                StrSql = StrSql + "(EQUIPMENTDEP.M11 ) AS DEPT11, "
                StrSql = StrSql + "(EQUIPMENTDEP.M12 ) AS DEPT12, "
                StrSql = StrSql + "(EQUIPMENTDEP.M13 ) AS DEPT13, "
                StrSql = StrSql + "(EQUIPMENTDEP.M14 ) AS DEPT14, "
                StrSql = StrSql + "(EQUIPMENTDEP.M15 ) AS DEPT15, "
                StrSql = StrSql + "(EQUIPMENTDEP.M16 ) AS DEPT16, "
                StrSql = StrSql + "(EQUIPMENTDEP.M17 ) AS DEPT17, "
                StrSql = StrSql + "(EQUIPMENTDEP.M18 ) AS DEPT18, "
                StrSql = StrSql + "(EQUIPMENTDEP.M19 ) AS DEPT19, "
                StrSql = StrSql + "(EQUIPMENTDEP.M20 ) AS DEPT20, "
                StrSql = StrSql + "(EQUIPMENTDEP.M21 ) AS DEPT21, "
                StrSql = StrSql + "(EQUIPMENTDEP.M22 ) AS DEPT22, "
                StrSql = StrSql + "(EQUIPMENTDEP.M23 ) AS DEPT23, "
                StrSql = StrSql + "(EQUIPMENTDEP.M24 ) AS DEPT24, "
                StrSql = StrSql + "(EQUIPMENTDEP.M25 ) AS DEPT25, "
                StrSql = StrSql + "(EQUIPMENTDEP.M26 ) AS DEPT26, "
                StrSql = StrSql + "(EQUIPMENTDEP.M27 ) AS DEPT27, "
                StrSql = StrSql + "(EQUIPMENTDEP.M28 ) AS DEPT28, "
                StrSql = StrSql + "(EQUIPMENTDEP.M29 ) AS DEPT29, "
                StrSql = StrSql + "(EQUIPMENTDEP.M30 ) AS DEPT30, "
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
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "EQUIPMENTTYPE "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "EQUIPMENTCOST "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIPMENTCOST.CASEID=EQUIPMENTTYPE.CASEID "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "PREFERENCES "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "PREFERENCES.CASEID=EQUIPMENTTYPE.CASEID "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "EQUIPMENTAREA "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIPMENTAREA.CASEID=EQUIPMENTTYPE.CASEID "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "EQUIPENERGYPREF "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIPENERGYPREF.CASEID=EQUIPMENTTYPE.CASEID "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "EQUIPMENTDEP "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIPMENTDEP.CASEID=EQUIPMENTTYPE.CASEID "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET1 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET1.EQUIPID = EQUIPMENTTYPE.M1 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET2 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET2.EQUIPID = EQUIPMENTTYPE.M2 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET3 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET3.EQUIPID = EQUIPMENTTYPE.M3 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET4 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET4.EQUIPID = EQUIPMENTTYPE.M4 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET5 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET5.EQUIPID = EQUIPMENTTYPE.M5 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET6 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET6.EQUIPID = EQUIPMENTTYPE.M6 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET7 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET7.EQUIPID = EQUIPMENTTYPE.M7 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET8 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET8.EQUIPID = EQUIPMENTTYPE.M8 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET9 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET9.EQUIPID = EQUIPMENTTYPE.M9 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET10 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET10.EQUIPID = EQUIPMENTTYPE.M10 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET11 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET11.EQUIPID = EQUIPMENTTYPE.M11 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET12 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET12.EQUIPID = EQUIPMENTTYPE.M12 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET13 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET13.EQUIPID = EQUIPMENTTYPE.M13 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET14 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET14.EQUIPID = EQUIPMENTTYPE.M14 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET15 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET15.EQUIPID = EQUIPMENTTYPE.M15 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET16 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET16.EQUIPID = EQUIPMENTTYPE.M16 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET17 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET17.EQUIPID = EQUIPMENTTYPE.M17 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET18 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET18.EQUIPID = EQUIPMENTTYPE.M18 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET19 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET19.EQUIPID = EQUIPMENTTYPE.M19 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET20 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET20.EQUIPID = EQUIPMENTTYPE.M20 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET21 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET21.EQUIPID = EQUIPMENTTYPE.M21 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET22 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET22.EQUIPID = EQUIPMENTTYPE.M22 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET23 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET23.EQUIPID = EQUIPMENTTYPE.M23 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET24 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET24.EQUIPID = EQUIPMENTTYPE.M24 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET25 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET25.EQUIPID = EQUIPMENTTYPE.M25 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET26 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET26.EQUIPID = EQUIPMENTTYPE.M26 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET27 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET27.EQUIPID = EQUIPMENTTYPE.M27 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET28 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET28.EQUIPID = EQUIPMENTTYPE.M28 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET29 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET29.EQUIPID = EQUIPMENTTYPE.M29 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT ASSET30 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "ASSET30.EQUIPID = EQUIPMENTTYPE.M30 "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=EQUIPMENTTYPE.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIPNATURALGASPREF "
                StrSql = StrSql + "ON EQUIPNATURALGASPREF.CASEID=EQUIPMENTTYPE.CASEID "
                'Changes for Water Start
                StrSql = StrSql + "INNER JOIN EQUIPWATERPREF "
                StrSql = StrSql + "ON EQUIPWATERPREF.CASEID=EQUIPMENTTYPE.CASEID "
                'Changes for Water end
                StrSql = StrSql + "INNER JOIN EQUIPMENTNUMBER EQNUM "
                StrSql = StrSql + "ON EQNUM.CASEID=EQUIPMENTTYPE.CASEID "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "EQUIPMENTTYPE.CASEID=" + CaseId.ToString() + " ORDER BY EQUIPMENTTYPE.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetEquipmentDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSupportEquipmentInDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  	 	DISTINCT  "
                StrSql = StrSql + "EQUIPMENT2TYPE.CASEID, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M1, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M2, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M3, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M4, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M5, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M6, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M7, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M8, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M9, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M10, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M11, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M12, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M13, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M14, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M15, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M16, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M17, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M18, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M19, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M20, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M21, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M22, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M23, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M24, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M25, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M26, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M27, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M28, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M29, "
                StrSql = StrSql + "EQUIPMENT2TYPE.M30, "
                StrSql = StrSql + "(ASSET1.INSTKW ) AS ERGYS1, "
                StrSql = StrSql + "(ASSET2.INSTKW ) AS ERGYS2, "
                StrSql = StrSql + "(ASSET3.INSTKW ) AS ERGYS3, "
                StrSql = StrSql + "(ASSET4.INSTKW ) AS ERGYS4, "
                StrSql = StrSql + "(ASSET5.INSTKW ) AS ERGYS5, "
                StrSql = StrSql + "(ASSET6.INSTKW ) AS ERGYS6, "
                StrSql = StrSql + "(ASSET7.INSTKW ) AS ERGYS7, "
                StrSql = StrSql + "(ASSET8.INSTKW ) AS ERGYS8, "
                StrSql = StrSql + "(ASSET9.INSTKW ) AS ERGYS9, "
                StrSql = StrSql + "(ASSET10.INSTKW ) AS ERGYS10, "
                StrSql = StrSql + "(ASSET11.INSTKW ) AS ERGYS11, "
                StrSql = StrSql + "(ASSET12.INSTKW ) AS ERGYS12, "
                StrSql = StrSql + "(ASSET13.INSTKW ) AS ERGYS13, "
                StrSql = StrSql + "(ASSET14.INSTKW ) AS ERGYS14, "
                StrSql = StrSql + "(ASSET15.INSTKW ) AS ERGYS15, "
                StrSql = StrSql + "(ASSET16.INSTKW ) AS ERGYS16, "
                StrSql = StrSql + "(ASSET17.INSTKW ) AS ERGYS17, "
                StrSql = StrSql + "(ASSET18.INSTKW ) AS ERGYS18, "
                StrSql = StrSql + "(ASSET19.INSTKW ) AS ERGYS19, "
                StrSql = StrSql + "(ASSET20.INSTKW ) AS ERGYS20, "
                StrSql = StrSql + "(ASSET21.INSTKW ) AS ERGYS21, "
                StrSql = StrSql + "(ASSET22.INSTKW ) AS ERGYS22, "
                StrSql = StrSql + "(ASSET23.INSTKW ) AS ERGYS23, "
                StrSql = StrSql + "(ASSET24.INSTKW ) AS ERGYS24, "
                StrSql = StrSql + "(ASSET25.INSTKW ) AS ERGYS25, "
                StrSql = StrSql + "(ASSET26.INSTKW ) AS ERGYS26, "
                StrSql = StrSql + "(ASSET27.INSTKW ) AS ERGYS27, "
                StrSql = StrSql + "(ASSET28.INSTKW ) AS ERGYS28, "
                StrSql = StrSql + "(ASSET29.INSTKW ) AS ERGYS29, "
                StrSql = StrSql + "(ASSET30.INSTKW ) AS ERGYS30, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M1 ) AS ERGYP1, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M2 ) AS ERGYP2, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M3 ) AS ERGYP3, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M4 ) AS ERGYP4, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M5 ) AS ERGYP5, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M6 ) AS ERGYP6, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M7 ) AS ERGYP7, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M8 ) AS ERGYP8, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M9 ) AS ERGYP9, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M10 ) AS ERGYP10, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M11 ) AS ERGYP11, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M12 ) AS ERGYP12, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M13 ) AS ERGYP13, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M14 ) AS ERGYP14, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M15 ) AS ERGYP15, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M16 ) AS ERGYP16, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M17 ) AS ERGYP17, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M18 ) AS ERGYP18, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M19 ) AS ERGYP19, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M20 ) AS ERGYP20, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M21 ) AS ERGYP21, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M22 ) AS ERGYP22, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M23 ) AS ERGYP23, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M24 ) AS ERGYP24, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M25 ) AS ERGYP25, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M26 ) AS ERGYP26, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M27 ) AS ERGYP27, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M28 ) AS ERGYP28, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M29 ) AS ERGYP29, "
                StrSql = StrSql + "(EQUIP2ENERGYPREF.M30 ) AS ERGYP30, "
                StrSql = StrSql + "(ASSET1.NTGKW ) AS NTGS1, "
                StrSql = StrSql + "(ASSET2.NTGKW ) AS NTGS2, "
                StrSql = StrSql + "(ASSET3.NTGKW ) AS NTGS3, "
                StrSql = StrSql + "(ASSET4.NTGKW ) AS NTGS4, "
                StrSql = StrSql + "(ASSET5.NTGKW ) AS NTGS5, "
                StrSql = StrSql + "(ASSET6.NTGKW ) AS NTGS6, "
                StrSql = StrSql + "(ASSET7.NTGKW ) AS NTGS7, "
                StrSql = StrSql + "(ASSET8.NTGKW ) AS NTGS8, "
                StrSql = StrSql + "(ASSET9.NTGKW ) AS NTGS9, "
                StrSql = StrSql + "(ASSET10.NTGKW ) AS NTGS10, "
                StrSql = StrSql + "(ASSET11.NTGKW ) AS NTGS11, "
                StrSql = StrSql + "(ASSET12.NTGKW ) AS NTGS12, "
                StrSql = StrSql + "(ASSET13.NTGKW ) AS NTGS13, "
                StrSql = StrSql + "(ASSET14.NTGKW ) AS NTGS14, "
                StrSql = StrSql + "(ASSET15.NTGKW ) AS NTGS15, "
                StrSql = StrSql + "(ASSET16.NTGKW ) AS NTGS16, "
                StrSql = StrSql + "(ASSET17.NTGKW ) AS NTGS17, "
                StrSql = StrSql + "(ASSET18.NTGKW ) AS NTGS18, "
                StrSql = StrSql + "(ASSET19.NTGKW ) AS NTGS19, "
                StrSql = StrSql + "(ASSET20.NTGKW ) AS NTGS20, "
                StrSql = StrSql + "(ASSET21.NTGKW ) AS NTGS21, "
                StrSql = StrSql + "(ASSET22.NTGKW ) AS NTGS22, "
                StrSql = StrSql + "(ASSET23.NTGKW ) AS NTGS23, "
                StrSql = StrSql + "(ASSET24.NTGKW ) AS NTGS24, "
                StrSql = StrSql + "(ASSET25.NTGKW ) AS NTGS25, "
                StrSql = StrSql + "(ASSET26.NTGKW ) AS NTGS26, "
                StrSql = StrSql + "(ASSET27.NTGKW ) AS NTGS27, "
                StrSql = StrSql + "(ASSET28.NTGKW ) AS NTGS28, "
                StrSql = StrSql + "(ASSET29.NTGKW ) AS NTGS29, "
                StrSql = StrSql + "(ASSET30.NTGKW ) AS NTGS30, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M1 ) AS NTGP1, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M2 ) AS NTGP2, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M3 ) AS NTGP3, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M4 ) AS NTGP4, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M5 ) AS NTGP5, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M6 ) AS NTGP6, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M7 ) AS NTGP7, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M8 ) AS NTGP8, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M9 ) AS NTGP9, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M10 ) AS NTGP10, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M11 ) AS NTGP11, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M12 ) AS NTGP12, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M13 ) AS NTGP13, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M14 ) AS NTGP14, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M15 ) AS NTGP15, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M16 ) AS NTGP16, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M17 ) AS NTGP17, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M18 ) AS NTGP18, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M19 ) AS NTGP19, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M20 ) AS NTGP20, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M21 ) AS NTGP21, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M22 ) AS NTGP22, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M23 ) AS NTGP23, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M24 ) AS NTGP24, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M25 ) AS NTGP25, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M26 ) AS NTGP26, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M27 ) AS NTGP27, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M28 ) AS NTGP28, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M29 ) AS NTGP29, "
                StrSql = StrSql + "(EQUIP2NATURALGASPREF.M30 ) AS NTGP30, "
                'Changes for Water Start
                StrSql = StrSql + "(ASSET1.WATERKW*PREF.CONVGALLON ) AS WATERS1, "
                StrSql = StrSql + "(ASSET2.WATERKW*PREF.CONVGALLON ) AS WATERS2, "
                StrSql = StrSql + "(ASSET3.WATERKW*PREF.CONVGALLON ) AS WATERS3, "
                StrSql = StrSql + "(ASSET4.WATERKW*PREF.CONVGALLON ) AS WATERS4, "
                StrSql = StrSql + "(ASSET5.WATERKW*PREF.CONVGALLON ) AS WATERS5, "
                StrSql = StrSql + "(ASSET6.WATERKW*PREF.CONVGALLON ) AS WATERS6, "
                StrSql = StrSql + "(ASSET7.WATERKW*PREF.CONVGALLON ) AS WATERS7, "
                StrSql = StrSql + "(ASSET8.WATERKW*PREF.CONVGALLON ) AS WATERS8, "
                StrSql = StrSql + "(ASSET9.WATERKW*PREF.CONVGALLON ) AS WATERS9, "
                StrSql = StrSql + "(ASSET10.WATERKW*PREF.CONVGALLON ) AS WATERS10, "
                StrSql = StrSql + "(ASSET11.WATERKW*PREF.CONVGALLON ) AS WATERS11, "
                StrSql = StrSql + "(ASSET12.WATERKW*PREF.CONVGALLON ) AS WATERS12, "
                StrSql = StrSql + "(ASSET13.WATERKW*PREF.CONVGALLON ) AS WATERS13, "
                StrSql = StrSql + "(ASSET14.WATERKW*PREF.CONVGALLON ) AS WATERS14, "
                StrSql = StrSql + "(ASSET15.WATERKW*PREF.CONVGALLON ) AS WATERS15, "
                StrSql = StrSql + "(ASSET16.WATERKW*PREF.CONVGALLON ) AS WATERS16, "
                StrSql = StrSql + "(ASSET17.WATERKW*PREF.CONVGALLON ) AS WATERS17, "
                StrSql = StrSql + "(ASSET18.WATERKW*PREF.CONVGALLON ) AS WATERS18, "
                StrSql = StrSql + "(ASSET19.WATERKW*PREF.CONVGALLON ) AS WATERS19, "
                StrSql = StrSql + "(ASSET20.WATERKW*PREF.CONVGALLON ) AS WATERS20, "
                StrSql = StrSql + "(ASSET21.WATERKW*PREF.CONVGALLON ) AS WATERS21, "
                StrSql = StrSql + "(ASSET22.WATERKW*PREF.CONVGALLON ) AS WATERS22, "
                StrSql = StrSql + "(ASSET23.WATERKW*PREF.CONVGALLON ) AS WATERS23, "
                StrSql = StrSql + "(ASSET24.WATERKW*PREF.CONVGALLON ) AS WATERS24, "
                StrSql = StrSql + "(ASSET25.WATERKW*PREF.CONVGALLON ) AS WATERS25, "
                StrSql = StrSql + "(ASSET26.WATERKW*PREF.CONVGALLON ) AS WATERS26, "
                StrSql = StrSql + "(ASSET27.WATERKW*PREF.CONVGALLON ) AS WATERS27, "
                StrSql = StrSql + "(ASSET28.WATERKW*PREF.CONVGALLON ) AS WATERS28, "
                StrSql = StrSql + "(ASSET29.WATERKW*PREF.CONVGALLON ) AS WATERS29, "
                StrSql = StrSql + "(ASSET30.WATERKW*PREF.CONVGALLON ) AS WATERS30, "
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
                'Changes for Water end
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
                StrSql = StrSql + "(EQUIPMENT2DEP.M1 ) AS DEPT1, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M2 ) AS DEPT2, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M3 ) AS DEPT3, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M4 ) AS DEPT4, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M5 ) AS DEPT5, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M6 ) AS DEPT6, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M7 ) AS DEPT7, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M8 ) AS DEPT8, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M9 ) AS DEPT9, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M10 ) AS DEPT10, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M11 ) AS DEPT11, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M12 ) AS DEPT12, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M13 ) AS DEPT13, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M14 ) AS DEPT14, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M15 ) AS DEPT15, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M16 ) AS DEPT16, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M17 ) AS DEPT17, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M18 ) AS DEPT18, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M19 ) AS DEPT19, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M20 ) AS DEPT20, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M21 ) AS DEPT21, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M22 ) AS DEPT22, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M23 ) AS DEPT23, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M24 ) AS DEPT24, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M25 ) AS DEPT25, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M26 ) AS DEPT26, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M27 ) AS DEPT27, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M28 ) AS DEPT28, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M29 ) AS DEPT29, "
                StrSql = StrSql + "(EQUIPMENT2DEP.M30 ) AS DEPT30, "
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
                StrSql = StrSql + "PREF.UNITS ,"

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

                StrSql = StrSql + "FROM EQUIPMENT2TYPE "
                StrSql = StrSql + "INNER JOIN  PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=EQUIPMENT2TYPE.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIP2ENERGYPREF "
                StrSql = StrSql + "ON EQUIP2ENERGYPREF.CASEID=EQUIPMENT2TYPE.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIP2NATURALGASPREF "
                StrSql = StrSql + "ON EQUIP2NATURALGASPREF.CASEID=EQUIPMENT2TYPE.CASEID "
                'Changes for Water Start
                StrSql = StrSql + "INNER JOIN EQUIP2WATERPREF "
                StrSql = StrSql + "ON EQUIP2WATERPREF.CASEID=EQUIPMENT2TYPE.CASEID "
                'Changes for Water end
                StrSql = StrSql + "INNER JOIN EQUIPMENT2DEP "
                StrSql = StrSql + "ON EQUIPMENT2DEP.CASEID=EQUIPMENT2TYPE.CASEID "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET1 "
                StrSql = StrSql + "ON ASSET1.EQUIPID = EQUIPMENT2TYPE.M1 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET2 "
                StrSql = StrSql + "ON ASSET2.EQUIPID = EQUIPMENT2TYPE.M2 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET3 "
                StrSql = StrSql + "ON ASSET3.EQUIPID = EQUIPMENT2TYPE.M3 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET4 "
                StrSql = StrSql + "ON ASSET4.EQUIPID = EQUIPMENT2TYPE.M4 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET5 "
                StrSql = StrSql + "ON ASSET5.EQUIPID = EQUIPMENT2TYPE.M5 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET6 "
                StrSql = StrSql + "ON ASSET6.EQUIPID = EQUIPMENT2TYPE.M6 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET7 "
                StrSql = StrSql + "ON ASSET7.EQUIPID = EQUIPMENT2TYPE.M7 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET8 "
                StrSql = StrSql + "ON ASSET8.EQUIPID = EQUIPMENT2TYPE.M8 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET9 "
                StrSql = StrSql + "ON ASSET9.EQUIPID = EQUIPMENT2TYPE.M9 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET10 "
                StrSql = StrSql + "ON ASSET10.EQUIPID = EQUIPMENT2TYPE.M10 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET11 "
                StrSql = StrSql + "ON ASSET11.EQUIPID = EQUIPMENT2TYPE.M11 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET12 "
                StrSql = StrSql + "ON ASSET12.EQUIPID = EQUIPMENT2TYPE.M12 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET13 "
                StrSql = StrSql + "ON ASSET13.EQUIPID = EQUIPMENT2TYPE.M13 "
                StrSql = StrSql + "INNER JOIN  MED1.EQUIPMENT2 ASSET14 "
                StrSql = StrSql + "ON ASSET14.EQUIPID = EQUIPMENT2TYPE.M14 "
                StrSql = StrSql + "INNER JOIN  MED1.EQUIPMENT2 ASSET15 "
                StrSql = StrSql + "ON ASSET15.EQUIPID = EQUIPMENT2TYPE.M15 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET16 "
                StrSql = StrSql + "ON ASSET16.EQUIPID = EQUIPMENT2TYPE.M16 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET17 "
                StrSql = StrSql + "ON ASSET17.EQUIPID = EQUIPMENT2TYPE.M17 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET18 "
                StrSql = StrSql + "ON ASSET18.EQUIPID = EQUIPMENT2TYPE.M18 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET19 "
                StrSql = StrSql + "ON ASSET19.EQUIPID = EQUIPMENT2TYPE.M19 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET20 "
                StrSql = StrSql + "ON ASSET20.EQUIPID = EQUIPMENT2TYPE.M20 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET21 "
                StrSql = StrSql + "ON ASSET21.EQUIPID = EQUIPMENT2TYPE.M21 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET22 "
                StrSql = StrSql + "ON ASSET22.EQUIPID = EQUIPMENT2TYPE.M22 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET23 "
                StrSql = StrSql + "ON ASSET23.EQUIPID = EQUIPMENT2TYPE.M23 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET24 "
                StrSql = StrSql + "ON ASSET24.EQUIPID = EQUIPMENT2TYPE.M24 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET25 "
                StrSql = StrSql + "ON ASSET25.EQUIPID = EQUIPMENT2TYPE.M25 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET26 "
                StrSql = StrSql + "ON ASSET26.EQUIPID = EQUIPMENT2TYPE.M26 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET27 "
                StrSql = StrSql + "ON ASSET27.EQUIPID = EQUIPMENT2TYPE.M27 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET28 "
                StrSql = StrSql + "ON ASSET28.EQUIPID = EQUIPMENT2TYPE.M28 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET29 "
                StrSql = StrSql + "ON ASSET29.EQUIPID = EQUIPMENT2TYPE.M29 "
                StrSql = StrSql + "INNER JOIN MED1.EQUIPMENT2 ASSET30 "
                StrSql = StrSql + "ON ASSET30.EQUIPID = EQUIPMENT2TYPE.M30 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2MAHRS EQHRS "
                StrSql = StrSql + "ON EQHRS.CASEID=EQUIPMENT2TYPE.CASEID "

                StrSql = StrSql + "INNER JOIN EQUIPMENT2NUMBER EQNUM "
                StrSql = StrSql + "ON EQNUM.CASEID=EQUIPMENT2TYPE.CASEID "

                StrSql = StrSql + "WHERE EQUIPMENT2TYPE.CASEID=" + CaseId.ToString() + " ORDER BY EQUIPMENT2TYPE.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetSupportDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetOperationInDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try


                StrSql = "SELECT DISTINCT  "
                StrSql = StrSql + "EQUIPMENTTYPE.CASEID, "
                StrSql = StrSql + "(EQUIDES1.EQUIPDE1||' '||EQUIDES1.EQUIPDE2) AS EQUIPDES1, "
                StrSql = StrSql + "(EQUIDES2.EQUIPDE1||' '||EQUIDES2.EQUIPDE2) AS EQUIPDES2, "
                StrSql = StrSql + "(EQUIDES3.EQUIPDE1||' '||EQUIDES3.EQUIPDE2) AS EQUIPDES3, "
                StrSql = StrSql + "(EQUIDES4.EQUIPDE1||' '||EQUIDES4.EQUIPDE2) AS EQUIPDES4, "
                StrSql = StrSql + "(EQUIDES5.EQUIPDE1||' '||EQUIDES5.EQUIPDE2) AS EQUIPDES5, "
                StrSql = StrSql + "(EQUIDES6.EQUIPDE1||' '||EQUIDES6.EQUIPDE2) AS EQUIPDES6, "
                StrSql = StrSql + "(EQUIDES7.EQUIPDE1||' '||EQUIDES7.EQUIPDE2) AS EQUIPDES7, "
                StrSql = StrSql + "(EQUIDES8.EQUIPDE1||' '||EQUIDES8.EQUIPDE2) AS EQUIPDES8, "
                StrSql = StrSql + "(EQUIDES9.EQUIPDE1||' '||EQUIDES9.EQUIPDE2) AS EQUIPDES9, "
                StrSql = StrSql + "(EQUIDES10.EQUIPDE1||' '||EQUIDES10.EQUIPDE2) AS EQUIPDES10, "
                StrSql = StrSql + "(EQUIDES11.EQUIPDE1||' '||EQUIDES11.EQUIPDE2) AS EQUIPDES11, "
                StrSql = StrSql + "(EQUIDES12.EQUIPDE1||' '||EQUIDES12.EQUIPDE2) AS EQUIPDES12, "
                StrSql = StrSql + "(EQUIDES13.EQUIPDE1||' '||EQUIDES13.EQUIPDE2) AS EQUIPDES13, "
                StrSql = StrSql + "(EQUIDES14.EQUIPDE1||' '||EQUIDES14.EQUIPDE2) AS EQUIPDES14, "
                StrSql = StrSql + "(EQUIDES15.EQUIPDE1||' '||EQUIDES15.EQUIPDE2) AS EQUIPDES15, "
                StrSql = StrSql + "(EQUIDES16.EQUIPDE1||' '||EQUIDES16.EQUIPDE2) AS EQUIPDES16, "
                StrSql = StrSql + "(EQUIDES17.EQUIPDE1||' '||EQUIDES17.EQUIPDE2) AS EQUIPDES17, "
                StrSql = StrSql + "(EQUIDES18.EQUIPDE1||' '||EQUIDES18.EQUIPDE2) AS EQUIPDES18, "
                StrSql = StrSql + "(EQUIDES19.EQUIPDE1||' '||EQUIDES19.EQUIPDE2) AS EQUIPDES19, "
                StrSql = StrSql + "(EQUIDES20.EQUIPDE1||' '||EQUIDES20.EQUIPDE2) AS EQUIPDES20, "
                StrSql = StrSql + "(EQUIDES21.EQUIPDE1||' '||EQUIDES21.EQUIPDE2) AS EQUIPDES21, "
                StrSql = StrSql + "(EQUIDES22.EQUIPDE1||' '||EQUIDES22.EQUIPDE2) AS EQUIPDES22, "
                StrSql = StrSql + "(EQUIDES23.EQUIPDE1||' '||EQUIDES23.EQUIPDE2) AS EQUIPDES23, "
                StrSql = StrSql + "(EQUIDES24.EQUIPDE1||' '||EQUIDES24.EQUIPDE2) AS EQUIPDES24, "
                StrSql = StrSql + "(EQUIDES25.EQUIPDE1||' '||EQUIDES25.EQUIPDE2) AS EQUIPDES25, "
                StrSql = StrSql + "(EQUIDES26.EQUIPDE1||' '||EQUIDES26.EQUIPDE2) AS EQUIPDES26, "
                StrSql = StrSql + "(EQUIDES27.EQUIPDE1||' '||EQUIDES27.EQUIPDE2) AS EQUIPDES27, "
                StrSql = StrSql + "(EQUIDES28.EQUIPDE1||' '||EQUIDES28.EQUIPDE2) AS EQUIPDES28, "
                StrSql = StrSql + "(EQUIDES29.EQUIPDE1||' '||EQUIDES29.EQUIPDE2) AS EQUIPDES29, "
                StrSql = StrSql + "(EQUIDES30.EQUIPDE1||' '||EQUIDES30.EQUIPDE2) AS EQUIPDES30, "
                StrSql = StrSql + "EQUIPMENTTYPE.M1, "
                StrSql = StrSql + "EQUIPMENTTYPE.M2, "
                StrSql = StrSql + "EQUIPMENTTYPE.M3, "
                StrSql = StrSql + "EQUIPMENTTYPE.M4, "
                StrSql = StrSql + "EQUIPMENTTYPE.M5, "
                StrSql = StrSql + "EQUIPMENTTYPE.M6, "
                StrSql = StrSql + "EQUIPMENTTYPE.M7, "
                StrSql = StrSql + "EQUIPMENTTYPE.M8, "
                StrSql = StrSql + "EQUIPMENTTYPE.M9, "
                StrSql = StrSql + "EQUIPMENTTYPE.M10, "
                StrSql = StrSql + "EQUIPMENTTYPE.M11, "
                StrSql = StrSql + "EQUIPMENTTYPE.M12, "
                StrSql = StrSql + "EQUIPMENTTYPE.M13, "
                StrSql = StrSql + "EQUIPMENTTYPE.M14, "
                StrSql = StrSql + "EQUIPMENTTYPE.M15, "
                StrSql = StrSql + "EQUIPMENTTYPE.M16, "
                StrSql = StrSql + "EQUIPMENTTYPE.M17, "
                StrSql = StrSql + "EQUIPMENTTYPE.M18, "
                StrSql = StrSql + "EQUIPMENTTYPE.M19, "
                StrSql = StrSql + "EQUIPMENTTYPE.M20, "
                StrSql = StrSql + "EQUIPMENTTYPE.M21, "
                StrSql = StrSql + "EQUIPMENTTYPE.M22, "
                StrSql = StrSql + "EQUIPMENTTYPE.M23, "
                StrSql = StrSql + "EQUIPMENTTYPE.M24, "
                StrSql = StrSql + "EQUIPMENTTYPE.M25, "
                StrSql = StrSql + "EQUIPMENTTYPE.M26, "
                StrSql = StrSql + "EQUIPMENTTYPE.M27, "
                StrSql = StrSql + "EQUIPMENTTYPE.M28, "
                StrSql = StrSql + "EQUIPMENTTYPE.M29, "
                StrSql = StrSql + "EQUIPMENTTYPE.M30, "

                StrSql = StrSql + "OPMAXRUNHRS.M1 AS ANNUALRUNHOURS1, "
                StrSql = StrSql + "OPMAXRUNHRS.M2 AS ANNUALRUNHOURS2, "
                StrSql = StrSql + "OPMAXRUNHRS.M3 AS ANNUALRUNHOURS3, "
                StrSql = StrSql + "OPMAXRUNHRS.M4 AS ANNUALRUNHOURS4, "
                StrSql = StrSql + "OPMAXRUNHRS.M5 AS ANNUALRUNHOURS5, "
                StrSql = StrSql + "OPMAXRUNHRS.M6 AS ANNUALRUNHOURS6, "
                StrSql = StrSql + "OPMAXRUNHRS.M7 AS ANNUALRUNHOURS7, "
                StrSql = StrSql + "OPMAXRUNHRS.M8 AS ANNUALRUNHOURS8, "
                StrSql = StrSql + "OPMAXRUNHRS.M9 AS ANNUALRUNHOURS9, "
                StrSql = StrSql + "OPMAXRUNHRS.M10 AS ANNUALRUNHOURS10, "
                StrSql = StrSql + "OPMAXRUNHRS.M11 AS ANNUALRUNHOURS11, "
                StrSql = StrSql + "OPMAXRUNHRS.M12 AS ANNUALRUNHOURS12, "
                StrSql = StrSql + "OPMAXRUNHRS.M13 AS ANNUALRUNHOURS13, "
                StrSql = StrSql + "OPMAXRUNHRS.M14 AS ANNUALRUNHOURS14, "
                StrSql = StrSql + "OPMAXRUNHRS.M15 AS ANNUALRUNHOURS15, "
                StrSql = StrSql + "OPMAXRUNHRS.M16 AS ANNUALRUNHOURS16, "
                StrSql = StrSql + "OPMAXRUNHRS.M17 AS ANNUALRUNHOURS17, "
                StrSql = StrSql + "OPMAXRUNHRS.M18 AS ANNUALRUNHOURS18, "
                StrSql = StrSql + "OPMAXRUNHRS.M19 AS ANNUALRUNHOURS19, "
                StrSql = StrSql + "OPMAXRUNHRS.M20 AS ANNUALRUNHOURS20, "
                StrSql = StrSql + "OPMAXRUNHRS.M21 AS ANNUALRUNHOURS21, "
                StrSql = StrSql + "OPMAXRUNHRS.M22 AS ANNUALRUNHOURS22, "
                StrSql = StrSql + "OPMAXRUNHRS.M23 AS ANNUALRUNHOURS23, "
                StrSql = StrSql + "OPMAXRUNHRS.M24 AS ANNUALRUNHOURS24, "
                StrSql = StrSql + "OPMAXRUNHRS.M25 AS ANNUALRUNHOURS25, "
                StrSql = StrSql + "OPMAXRUNHRS.M26 AS ANNUALRUNHOURS26, "
                StrSql = StrSql + "OPMAXRUNHRS.M27 AS ANNUALRUNHOURS27, "
                StrSql = StrSql + "OPMAXRUNHRS.M28 AS ANNUALRUNHOURS28, "
                StrSql = StrSql + "OPMAXRUNHRS.M29 AS ANNUALRUNHOURS29, "
                StrSql = StrSql + "OPMAXRUNHRS.M30 AS ANNUALRUNHOURS30,  "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES1.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M1 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M1,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE1, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES2.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M2 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M2,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE2, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES3.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M3 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M3,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE3, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES4.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M4 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M4,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE4, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES5.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M5 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M5,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE5, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES6.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M6 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M6,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE6, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES7.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M7 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M7,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE7, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES8.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M8 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M8,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE8, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES9.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M9 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M9,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE9, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES10.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M10 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M10,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE10, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES11.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M11 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M11,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE11, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES12.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M12 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M12,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE12, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES13.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M13 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M13,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE13, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES14.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M14 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M14,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE14, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES15.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M15 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M15,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE15, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES16.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M16 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M16,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE16, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES7.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M17 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M17,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE17, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES18.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M18 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M18,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE18, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES9.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M19 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M19,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE19, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES20.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M20 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M20,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE20, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES21.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M21 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M21,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE21, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES22.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M22 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M22,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE22, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES23.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M23 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M23,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE23, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES24.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M24 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M24,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE24, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES25.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M25 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M25,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE25, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES26.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M26 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M26,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE26, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES27.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M27 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M27,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE27, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES8.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M28 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M28,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE28, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES9.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M29 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M29,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE29, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When EQUIDES30.UNITS ='fpm' then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND((OPINSTGRSRATE.M30 * PREF.CONVTHICK2),2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "ROUND(OPINSTGRSRATE.M30,2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "AS INSTANTANEOUSRATE30, "
                StrSql = StrSql + "EQUIDES1.UNITS as UNIT1, "
                StrSql = StrSql + "EQUIDES2.UNITS as UNIT2, "
                StrSql = StrSql + "EQUIDES3.UNITS as UNIT3, "
                StrSql = StrSql + "EQUIDES4.UNITS as UNIT4, "
                StrSql = StrSql + "EQUIDES5.UNITS as UNIT5, "
                StrSql = StrSql + "EQUIDES6.UNITS as UNIT6, "
                StrSql = StrSql + "EQUIDES7.UNITS as UNIT7, "
                StrSql = StrSql + "EQUIDES8.UNITS as UNIT8, "
                StrSql = StrSql + "EQUIDES9.UNITS as UNIT9, "
                StrSql = StrSql + "EQUIDES10.UNITS as UNIT10, "
                StrSql = StrSql + "EQUIDES11.UNITS as UNIT11, "
                StrSql = StrSql + "EQUIDES12.UNITS as UNIT12, "
                StrSql = StrSql + "EQUIDES13.UNITS as UNIT13, "
                StrSql = StrSql + "EQUIDES14.UNITS as UNIT14, "
                StrSql = StrSql + "EQUIDES15.UNITS as UNIT15, "
                StrSql = StrSql + "EQUIDES16.UNITS as UNIT16, "
                StrSql = StrSql + "EQUIDES17.UNITS as UNIT17, "
                StrSql = StrSql + "EQUIDES18.UNITS as UNIT18, "
                StrSql = StrSql + "EQUIDES19.UNITS as UNIT19, "
                StrSql = StrSql + "EQUIDES20.UNITS as UNIT20, "
                StrSql = StrSql + "EQUIDES21.UNITS as UNIT21, "
                StrSql = StrSql + "EQUIDES22.UNITS as UNIT22, "
                StrSql = StrSql + "EQUIDES23.UNITS as UNIT23, "
                StrSql = StrSql + "EQUIDES24.UNITS as UNIT24, "
                StrSql = StrSql + "EQUIDES25.UNITS as UNIT25, "
                StrSql = StrSql + "EQUIDES26.UNITS as UNIT26, "
                StrSql = StrSql + "EQUIDES27.UNITS as UNIT27, "
                StrSql = StrSql + "EQUIDES28.UNITS as UNIT28, "
                StrSql = StrSql + "EQUIDES29.UNITS as UNIT29, "
                StrSql = StrSql + "EQUIDES30.UNITS as UNIT30, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES1.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES1.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS1, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES2.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES2.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS2, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES3.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES3.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS3, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES4.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES4.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS4, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES5.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES5.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS5, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES6.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES6.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS6, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES7.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES7.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS7, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES8.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES8.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS8, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES9.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES9.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS9, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES10.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES10.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS10, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES11.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES11.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS11, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES12.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES12.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS12, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES13.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES13.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS13, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES14.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES14.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS14, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES15.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES15.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS15, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES16.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES16.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS16, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES17.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES17.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS17, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES18.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES18.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS18, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES19.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES19.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS19, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES20.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES20.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS20, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES21.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES21.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS21, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES22.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES22.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS22, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES23.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES23.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS23, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES24.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES24.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS24, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES25.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES25.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS25, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES26.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES26.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS26, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES27.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES27.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS27, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES28.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES28.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS28, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES29.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES29.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS29, "
                StrSql = StrSql + "Case "
                StrSql = StrSql + "When PREF.UNITS =0 then "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES30.UNITS) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "Else "
                StrSql = StrSql + "( "
                StrSql = StrSql + "(EQUIDES30.UNITS2) "
                StrSql = StrSql + ") "
                StrSql = StrSql + "End "
                StrSql = StrSql + "As EQUIPUNITS30, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M1 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE1,  "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M2 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE2, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M3 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE3, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M4 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE4, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M5 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE5, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M6 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE6, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M7 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE7, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M8 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE8, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M9 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE9, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M10 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE10, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M11 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE11, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M12 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE12, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M13 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE13, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M14 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE14, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M15 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE15, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M16 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE16, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M17 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE17, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M18 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE18, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M19 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE19, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M20 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE20, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M21 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE21, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M22 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE22, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M23 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE23, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M24 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE24, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M25 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE25, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M26 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE26, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M27 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE27, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M28 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE28, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M29 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE29, "
                StrSql = StrSql + "ROUND((OPLBSPERHOUR.M30 * PREF.CONVWT),2) AS INSTANTANEOUS2RATE30, "
                StrSql = StrSql + "OPDOWNTIME.M1 AS DOWNTIME1, "
                StrSql = StrSql + "OPDOWNTIME.M2 AS DOWNTIME2, "
                StrSql = StrSql + "OPDOWNTIME.M3 AS DOWNTIME3, "
                StrSql = StrSql + "OPDOWNTIME.M4 AS DOWNTIME4, "
                StrSql = StrSql + "OPDOWNTIME.M5 AS DOWNTIME5, "
                StrSql = StrSql + "OPDOWNTIME.M6 AS DOWNTIME6, "
                StrSql = StrSql + "OPDOWNTIME.M7 AS DOWNTIME7, "
                StrSql = StrSql + "OPDOWNTIME.M8 AS DOWNTIME8, "
                StrSql = StrSql + "OPDOWNTIME.M9 AS DOWNTIME9, "
                StrSql = StrSql + "OPDOWNTIME.M10 AS DOWNTIME10, "
                StrSql = StrSql + "OPDOWNTIME.M11 AS DOWNTIME11, "
                StrSql = StrSql + "OPDOWNTIME.M12 AS DOWNTIME12, "
                StrSql = StrSql + "OPDOWNTIME.M13 AS DOWNTIME13, "
                StrSql = StrSql + "OPDOWNTIME.M14 AS DOWNTIME14, "
                StrSql = StrSql + "OPDOWNTIME.M15 AS DOWNTIME15, "
                StrSql = StrSql + "OPDOWNTIME.M16 AS DOWNTIME16, "
                StrSql = StrSql + "OPDOWNTIME.M17 AS DOWNTIME17, "
                StrSql = StrSql + "OPDOWNTIME.M18 AS DOWNTIME18, "
                StrSql = StrSql + "OPDOWNTIME.M19 AS DOWNTIME19, "
                StrSql = StrSql + "OPDOWNTIME.M20 AS DOWNTIME20, "
                StrSql = StrSql + "OPDOWNTIME.M21 AS DOWNTIME21, "
                StrSql = StrSql + "OPDOWNTIME.M22 AS DOWNTIME22, "
                StrSql = StrSql + "OPDOWNTIME.M23 AS DOWNTIME23, "
                StrSql = StrSql + "OPDOWNTIME.M24 AS DOWNTIME24, "
                StrSql = StrSql + "OPDOWNTIME.M25 AS DOWNTIME25, "
                StrSql = StrSql + "OPDOWNTIME.M26 AS DOWNTIME26, "
                StrSql = StrSql + "OPDOWNTIME.M27 AS DOWNTIME27, "
                StrSql = StrSql + "OPDOWNTIME.M28 AS DOWNTIME28, "
                StrSql = StrSql + "OPDOWNTIME.M29 AS DOWNTIME29, "
                StrSql = StrSql + "OPDOWNTIME.M30 AS DOWNTIME30, "
                StrSql = StrSql + "OPWASTE.M1 AS PRODUCTIONWAST1, "
                StrSql = StrSql + "OPWASTE.M2 AS PRODUCTIONWAST2, "
                StrSql = StrSql + "OPWASTE.M3 AS PRODUCTIONWAST3, "
                StrSql = StrSql + "OPWASTE.M4 AS PRODUCTIONWAST4, "
                StrSql = StrSql + "OPWASTE.M5 AS PRODUCTIONWAST5, "
                StrSql = StrSql + "OPWASTE.M6 AS PRODUCTIONWAST6, "
                StrSql = StrSql + "OPWASTE.M7 AS PRODUCTIONWAST7, "
                StrSql = StrSql + "OPWASTE.M8 AS PRODUCTIONWAST8, "
                StrSql = StrSql + "OPWASTE.M9 AS PRODUCTIONWAST9, "
                StrSql = StrSql + "OPWASTE.M10 AS PRODUCTIONWAST10, "
                StrSql = StrSql + "OPWASTE.M11 AS PRODUCTIONWAST11, "
                StrSql = StrSql + "OPWASTE.M12 AS PRODUCTIONWAST12, "
                StrSql = StrSql + "OPWASTE.M13 AS PRODUCTIONWAST13, "
                StrSql = StrSql + "OPWASTE.M14 AS PRODUCTIONWAST14, "
                StrSql = StrSql + "OPWASTE.M15 AS PRODUCTIONWAST15, "
                StrSql = StrSql + "OPWASTE.M16 AS PRODUCTIONWAST16, "
                StrSql = StrSql + "OPWASTE.M17 AS PRODUCTIONWAST17, "
                StrSql = StrSql + "OPWASTE.M18 AS PRODUCTIONWAST18, "
                StrSql = StrSql + "OPWASTE.M19 AS PRODUCTIONWAST19, "
                StrSql = StrSql + "OPWASTE.M20 AS PRODUCTIONWAST20, "
                StrSql = StrSql + "OPWASTE.M21 AS PRODUCTIONWAST21, "
                StrSql = StrSql + "OPWASTE.M22 AS PRODUCTIONWAST22, "
                StrSql = StrSql + "OPWASTE.M23 AS PRODUCTIONWAST23, "
                StrSql = StrSql + "OPWASTE.M24 AS PRODUCTIONWAST24, "
                StrSql = StrSql + "OPWASTE.M25 AS PRODUCTIONWAST25, "
                StrSql = StrSql + "OPWASTE.M26 AS PRODUCTIONWAST26, "
                StrSql = StrSql + "OPWASTE.M27 AS PRODUCTIONWAST27, "
                StrSql = StrSql + "OPWASTE.M28 AS PRODUCTIONWAST28, "
                StrSql = StrSql + "OPWASTE.M29 AS PRODUCTIONWAST29, "
                StrSql = StrSql + "OPWASTE.M30 AS PRODUCTIONWAST30, "
                StrSql = StrSql + "OPWASTE.W1 AS DESIGNWAST1, "
                StrSql = StrSql + "OPWASTE.W2 AS DESIGNWAST2, "
                StrSql = StrSql + "OPWASTE.W3 AS DESIGNWAST3, "
                StrSql = StrSql + "OPWASTE.W4 AS DESIGNWAST4, "
                StrSql = StrSql + "OPWASTE.W5 AS DESIGNWAST5, "
                StrSql = StrSql + "OPWASTE.W6 AS DESIGNWAST6, "
                StrSql = StrSql + "OPWASTE.W7 AS DESIGNWAST7, "
                StrSql = StrSql + "OPWASTE.W8 AS DESIGNWAST8, "
                StrSql = StrSql + "OPWASTE.W9 AS DESIGNWAST9, "
                StrSql = StrSql + "OPWASTE.W10 AS DESIGNWAST10, "
                StrSql = StrSql + "OPWASTE.W11 AS DESIGNWAST11, "
                StrSql = StrSql + "OPWASTE.W12 AS DESIGNWAST12, "
                StrSql = StrSql + "OPWASTE.W13 AS DESIGNWAST13, "
                StrSql = StrSql + "OPWASTE.W14 AS DESIGNWAST14, "
                StrSql = StrSql + "OPWASTE.W15 AS DESIGNWAST15, "
                StrSql = StrSql + "OPWASTE.W16 AS DESIGNWAST16, "
                StrSql = StrSql + "OPWASTE.W17 AS DESIGNWAST17, "
                StrSql = StrSql + "OPWASTE.W18 AS DESIGNWAST18, "
                StrSql = StrSql + "OPWASTE.W19 AS DESIGNWAST19, "
                StrSql = StrSql + "OPWASTE.W20 AS DESIGNWAST20, "
                StrSql = StrSql + "OPWASTE.W21 AS DESIGNWAST21, "
                StrSql = StrSql + "OPWASTE.W22 AS DESIGNWAST22, "
                StrSql = StrSql + "OPWASTE.W23 AS DESIGNWAST23, "
                StrSql = StrSql + "OPWASTE.W24 AS DESIGNWAST24, "
                StrSql = StrSql + "OPWASTE.W25 AS DESIGNWAST25, "
                StrSql = StrSql + "OPWASTE.W26 AS DESIGNWAST26, "
                StrSql = StrSql + "OPWASTE.W27 AS DESIGNWAST27, "
                StrSql = StrSql + "OPWASTE.W28 AS DESIGNWAST28, "
                StrSql = StrSql + "OPWASTE.W29 AS DESIGNWAST29, "
                StrSql = StrSql + "OPWASTE.W30 AS DESIGNWAST30,  "

                StrSql = StrSql + "(OPW.M1*PREF.CONVTHICK) AS OPWEBWIDTHPREF1, "
                StrSql = StrSql + "(OPW.M2*PREF.CONVTHICK) AS OPWEBWIDTHPREF2, "
                StrSql = StrSql + "(OPW.M3*PREF.CONVTHICK) AS OPWEBWIDTHPREF3, "
                StrSql = StrSql + "(OPW.M4*PREF.CONVTHICK) AS OPWEBWIDTHPREF4, "
                StrSql = StrSql + "(OPW.M5*PREF.CONVTHICK) AS OPWEBWIDTHPREF5, "
                StrSql = StrSql + "(OPW.M6*PREF.CONVTHICK) AS OPWEBWIDTHPREF6, "
                StrSql = StrSql + "(OPW.M7*PREF.CONVTHICK) AS OPWEBWIDTHPREF7, "
                StrSql = StrSql + "(OPW.M8*PREF.CONVTHICK) AS OPWEBWIDTHPREF8, "
                StrSql = StrSql + "(OPW.M9*PREF.CONVTHICK) AS OPWEBWIDTHPREF9, "
                StrSql = StrSql + "(OPW.M10*PREF.CONVTHICK) AS OPWEBWIDTHPREF10, "
                StrSql = StrSql + "(OPW.M11*PREF.CONVTHICK) AS OPWEBWIDTHPREF11, "
                StrSql = StrSql + "(OPW.M12*PREF.CONVTHICK) AS OPWEBWIDTHPREF12, "
                StrSql = StrSql + "(OPW.M13*PREF.CONVTHICK) AS OPWEBWIDTHPREF13, "
                StrSql = StrSql + "(OPW.M14*PREF.CONVTHICK) AS OPWEBWIDTHPREF14, "
                StrSql = StrSql + "(OPW.M15*PREF.CONVTHICK) AS OPWEBWIDTHPREF15, "
                StrSql = StrSql + "(OPW.M16*PREF.CONVTHICK) AS OPWEBWIDTHPREF16, "
                StrSql = StrSql + "(OPW.M17*PREF.CONVTHICK) AS OPWEBWIDTHPREF17, "
                StrSql = StrSql + "(OPW.M18*PREF.CONVTHICK) AS OPWEBWIDTHPREF18, "
                StrSql = StrSql + "(OPW.M19*PREF.CONVTHICK) AS OPWEBWIDTHPREF19, "
                StrSql = StrSql + "(OPW.M20*PREF.CONVTHICK) AS OPWEBWIDTHPREF20, "
                StrSql = StrSql + "(OPW.M21*PREF.CONVTHICK) AS OPWEBWIDTHPREF21, "
                StrSql = StrSql + "(OPW.M22*PREF.CONVTHICK) AS OPWEBWIDTHPREF22, "
                StrSql = StrSql + "(OPW.M23*PREF.CONVTHICK) AS OPWEBWIDTHPREF23, "
                StrSql = StrSql + "(OPW.M24*PREF.CONVTHICK) AS OPWEBWIDTHPREF24, "
                StrSql = StrSql + "(OPW.M25*PREF.CONVTHICK) AS OPWEBWIDTHPREF25, "
                StrSql = StrSql + "(OPW.M26*PREF.CONVTHICK) AS OPWEBWIDTHPREF26, "
                StrSql = StrSql + "(OPW.M27*PREF.CONVTHICK) AS OPWEBWIDTHPREF27, "
                StrSql = StrSql + "(OPW.M28*PREF.CONVTHICK) AS OPWEBWIDTHPREF28, "
                StrSql = StrSql + "(OPW.M29*PREF.CONVTHICK) AS OPWEBWIDTHPREF29, "
                StrSql = StrSql + "(OPW.M30*PREF.CONVTHICK) AS OPWEBWIDTHPREF30, "
                StrSql = StrSql + "(EQUIDES1.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG1, "
                StrSql = StrSql + "(EQUIDES2.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG2, "
                StrSql = StrSql + "(EQUIDES3.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG3, "
                StrSql = StrSql + "(EQUIDES4.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG4, "
                StrSql = StrSql + "(EQUIDES5.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG5, "
                StrSql = StrSql + "(EQUIDES6.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG6, "
                StrSql = StrSql + "(EQUIDES7.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG7, "
                StrSql = StrSql + "(EQUIDES8.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG8, "
                StrSql = StrSql + "(EQUIDES9.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG9, "
                StrSql = StrSql + "(EQUIDES10.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG10, "
                StrSql = StrSql + "(EQUIDES11.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG11, "
                StrSql = StrSql + "(EQUIDES12.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG12, "
                StrSql = StrSql + "(EQUIDES13.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG13, "
                StrSql = StrSql + "(EQUIDES14.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG14, "
                StrSql = StrSql + "(EQUIDES15.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG15, "
                StrSql = StrSql + "(EQUIDES16.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG16, "
                StrSql = StrSql + "(EQUIDES17.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG17, "
                StrSql = StrSql + "(EQUIDES18.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG18, "
                StrSql = StrSql + "(EQUIDES19.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG19, "
                StrSql = StrSql + "(EQUIDES20.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG20, "
                StrSql = StrSql + "(EQUIDES21.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG21, "
                StrSql = StrSql + "(EQUIDES22.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG22, "
                StrSql = StrSql + "(EQUIDES23.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG23, "
                StrSql = StrSql + "(EQUIDES24.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG24, "
                StrSql = StrSql + "(EQUIDES25.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG25, "
                StrSql = StrSql + "(EQUIDES26.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG26, "
                StrSql = StrSql + "(EQUIDES27.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG27, "
                StrSql = StrSql + "(EQUIDES28.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG28, "
                StrSql = StrSql + "(EQUIDES29.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG29, "
                StrSql = StrSql + "(EQUIDES30.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG30, "
                StrSql = StrSql + "(OPE.M1) AS OPEXITSPREF1, "
                StrSql = StrSql + "(OPE.M2) AS OPEXITSPREF2, "
                StrSql = StrSql + "(OPE.M3) AS OPEXITSPREF3, "
                StrSql = StrSql + "(OPE.M4) AS OPEXITSPREF4, "
                StrSql = StrSql + "(OPE.M5) AS OPEXITSPREF5, "
                StrSql = StrSql + "(OPE.M6) AS OPEXITSPREF6, "
                StrSql = StrSql + "(OPE.M7) AS OPEXITSPREF7, "
                StrSql = StrSql + "(OPE.M8) AS OPEXITSPREF8, "
                StrSql = StrSql + "(OPE.M9) AS OPEXITSPREF9, "
                StrSql = StrSql + "(OPE.M10) AS OPEXITSPREF10, "
                StrSql = StrSql + "(OPE.M11) AS OPEXITSPREF11, "
                StrSql = StrSql + "(OPE.M12) AS OPEXITSPREF12, "
                StrSql = StrSql + "(OPE.M13) AS OPEXITSPREF13, "
                StrSql = StrSql + "(OPE.M14) AS OPEXITSPREF14, "
                StrSql = StrSql + "(OPE.M15) AS OPEXITSPREF15, "
                StrSql = StrSql + "(OPE.M16) AS OPEXITSPREF16, "
                StrSql = StrSql + "(OPE.M17) AS OPEXITSPREF17, "
                StrSql = StrSql + "(OPE.M18) AS OPEXITSPREF18, "
                StrSql = StrSql + "(OPE.M19) AS OPEXITSPREF19, "
                StrSql = StrSql + "(OPE.M20) AS OPEXITSPREF20, "
                StrSql = StrSql + "(OPE.M21) AS OPEXITSPREF21, "
                StrSql = StrSql + "(OPE.M22) AS OPEXITSPREF22, "
                StrSql = StrSql + "(OPE.M23) AS OPEXITSPREF23, "
                StrSql = StrSql + "(OPE.M24) AS OPEXITSPREF24, "
                StrSql = StrSql + "(OPE.M25) AS OPEXITSPREF25, "
                StrSql = StrSql + "(OPE.M26) AS OPEXITSPREF26, "
                StrSql = StrSql + "(OPE.M27) AS OPEXITSPREF27, "
                StrSql = StrSql + "(OPE.M28) AS OPEXITSPREF28, "
                StrSql = StrSql + "(OPE.M29) AS OPEXITSPREF29, "
                StrSql = StrSql + "(OPE.M30) AS OPEXITSPREF30, "
                StrSql = StrSql + "(EQUIDES1.EXITS)  as OPEXITSSUGG1, "
                StrSql = StrSql + "(EQUIDES2.EXITS)  as OPEXITSSUGG2, "
                StrSql = StrSql + "(EQUIDES3.EXITS)  as OPEXITSSUGG3, "
                StrSql = StrSql + "(EQUIDES4.EXITS)  as OPEXITSSUGG4, "
                StrSql = StrSql + "(EQUIDES5.EXITS)  as OPEXITSSUGG5, "
                StrSql = StrSql + "(EQUIDES6.EXITS)  as OPEXITSSUGG6, "
                StrSql = StrSql + "(EQUIDES7.EXITS)  as OPEXITSSUGG7, "
                StrSql = StrSql + "(EQUIDES8.EXITS)  as OPEXITSSUGG8, "
                StrSql = StrSql + "(EQUIDES9.EXITS)  as OPEXITSSUGG9, "
                StrSql = StrSql + "(EQUIDES10.EXITS)  as OPEXITSSUGG10, "
                StrSql = StrSql + "(EQUIDES11.EXITS)  as OPEXITSSUGG11, "
                StrSql = StrSql + "(EQUIDES12.EXITS)  as OPEXITSSUGG12, "
                StrSql = StrSql + "(EQUIDES13.EXITS)  as OPEXITSSUGG13, "
                StrSql = StrSql + "(EQUIDES14.EXITS)  as OPEXITSSUGG14, "
                StrSql = StrSql + "(EQUIDES15.EXITS)  as OPEXITSSUGG15, "
                StrSql = StrSql + "(EQUIDES16.EXITS)  as OPEXITSSUGG16, "
                StrSql = StrSql + "(EQUIDES17.EXITS)  as OPEXITSSUGG17, "
                StrSql = StrSql + "(EQUIDES18.EXITS)  as OPEXITSSUGG18, "
                StrSql = StrSql + "(EQUIDES19.EXITS)  as OPEXITSSUGG19, "
                StrSql = StrSql + "(EQUIDES20.EXITS)  as OPEXITSSUGG20, "
                StrSql = StrSql + "(EQUIDES21.EXITS)  as OPEXITSSUGG21, "
                StrSql = StrSql + "(EQUIDES22.EXITS)  as OPEXITSSUGG22, "
                StrSql = StrSql + "(EQUIDES23.EXITS)  as OPEXITSSUGG23, "
                StrSql = StrSql + "(EQUIDES24.EXITS)  as OPEXITSSUGG24, "
                StrSql = StrSql + "(EQUIDES25.EXITS)  as OPEXITSSUGG25, "
                StrSql = StrSql + "(EQUIDES26.EXITS)  as OPEXITSSUGG26, "
                StrSql = StrSql + "(EQUIDES27.EXITS)  as OPEXITSSUGG27, "
                StrSql = StrSql + "(EQUIDES28.EXITS)  as OPEXITSSUGG28, "
                StrSql = StrSql + "(EQUIDES29.EXITS)  as OPEXITSSUGG29, "
                StrSql = StrSql + "(EQUIDES30.EXITS) as OPEXITSSUGG30, "

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
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "EQUIPMENTTYPE "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES1 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES1.EQUIPID = EQUIPMENTTYPE.M1 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES2 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES2.EQUIPID = EQUIPMENTTYPE.M2 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES3 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES3.EQUIPID = EQUIPMENTTYPE.M3 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES4 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES4.EQUIPID = EQUIPMENTTYPE.M4 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES5 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES5.EQUIPID = EQUIPMENTTYPE.M5 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES6 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES6.EQUIPID = EQUIPMENTTYPE.M6 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES7 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES7.EQUIPID = EQUIPMENTTYPE.M7 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES8 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES8.EQUIPID = EQUIPMENTTYPE.M8 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES9 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES9.EQUIPID = EQUIPMENTTYPE.M9 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES10 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES10.EQUIPID = EQUIPMENTTYPE.M10 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES11 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES11.EQUIPID = EQUIPMENTTYPE.M11 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES12 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES12.EQUIPID = EQUIPMENTTYPE.M12 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES13 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES13.EQUIPID = EQUIPMENTTYPE.M13 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES14 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES14.EQUIPID = EQUIPMENTTYPE.M14 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES15 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES15.EQUIPID = EQUIPMENTTYPE.M15 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES16 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES16.EQUIPID = EQUIPMENTTYPE.M16 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES17 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES17.EQUIPID = EQUIPMENTTYPE.M17 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES18 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES18.EQUIPID = EQUIPMENTTYPE.M18 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES19 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES19.EQUIPID = EQUIPMENTTYPE.M19 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES20 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES20.EQUIPID = EQUIPMENTTYPE.M20 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES21 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES21.EQUIPID = EQUIPMENTTYPE.M21 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES22 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES22.EQUIPID = EQUIPMENTTYPE.M22 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES23 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES23.EQUIPID = EQUIPMENTTYPE.M23 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES24 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES24.EQUIPID = EQUIPMENTTYPE.M24 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES25 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES25.EQUIPID = EQUIPMENTTYPE.M25 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES26 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES26.EQUIPID = EQUIPMENTTYPE.M26 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES27 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES27.EQUIPID = EQUIPMENTTYPE.M27 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES28 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES28.EQUIPID = EQUIPMENTTYPE.M28 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES29 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES29.EQUIPID = EQUIPMENTTYPE.M29 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "MED1.EQUIPMENT EQUIDES30 "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "EQUIDES30.EQUIPID = EQUIPMENTTYPE.M30 "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "OPWEBWIDTH OPW "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "OPW.CASEID=EQUIPMENTTYPE.CASEID "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "PREFERENCES PREF "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "PREF.CASEID=EQUIPMENTTYPE.CASEID "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "OPLBSPERHOUR "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "OPLBSPERHOUR.CASEID=EQUIPMENTTYPE.CASEID "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "OPMAXRUNHRS "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "OPMAXRUNHRS.CASEID=EQUIPMENTTYPE.CASEID "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "OPINSTGRSRATE "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "OPINSTGRSRATE.CASEID=EQUIPMENTTYPE.CASEID "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "OPDOWNTIME "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "OPDOWNTIME.CASEID=EQUIPMENTTYPE.CASEID "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "OPWASTE "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "OPWASTE.CASEID=EQUIPMENTTYPE.CASEID "
                StrSql = StrSql + "INNER JOIN OPEXITS OPE "
                StrSql = StrSql + "ON OPE.CASEID=EQUIPMENTTYPE.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIPMENTNUMBER EQNUM "
                StrSql = StrSql + "ON EQNUM.CASEID=EQUIPMENTTYPE.CASEID "
                StrSql = StrSql + "WHERE EQUIPMENTTYPE.CASEID=" + CaseId.ToString() + ""
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetOperationInDetails:" + ex.Message.ToString())
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
                StrSql = StrSql + "PERSONNELDEP.M1  AS DEPARTMENT1, "
                StrSql = StrSql + "PERSONNELDEP.M2  AS DEPARTMENT2, "
                StrSql = StrSql + "PERSONNELDEP.M3  AS DEPARTMENT3, "
                StrSql = StrSql + "PERSONNELDEP.M4  AS DEPARTMENT4, "
                StrSql = StrSql + "PERSONNELDEP.M5  AS DEPARTMENT5, "
                StrSql = StrSql + "PERSONNELDEP.M6  AS DEPARTMENT6, "
                StrSql = StrSql + "PERSONNELDEP.M7  AS DEPARTMENT7, "
                StrSql = StrSql + "PERSONNELDEP.M8  AS DEPARTMENT8, "
                StrSql = StrSql + "PERSONNELDEP.M9  AS DEPARTMENT9, "
                StrSql = StrSql + "PERSONNELDEP.M10  AS DEPARTMENT10, "
                StrSql = StrSql + "PERSONNELDEP.M11  AS DEPARTMENT11, "
                StrSql = StrSql + "PERSONNELDEP.M12  AS DEPARTMENT12, "
                StrSql = StrSql + "PERSONNELDEP.M13  AS DEPARTMENT13, "
                StrSql = StrSql + "PERSONNELDEP.M14  AS DEPARTMENT14, "
                StrSql = StrSql + "PERSONNELDEP.M15  AS DEPARTMENT15, "
                StrSql = StrSql + "PERSONNELDEP.M16  AS DEPARTMENT16, "
                StrSql = StrSql + "PERSONNELDEP.M17  AS DEPARTMENT17, "
                StrSql = StrSql + "PERSONNELDEP.M18  AS DEPARTMENT18, "
                StrSql = StrSql + "PERSONNELDEP.M19  AS DEPARTMENT19, "
                StrSql = StrSql + "PERSONNELDEP.M20  AS DEPARTMENT20, "
                StrSql = StrSql + "PERSONNELDEP.M21  AS DEPARTMENT21, "
                StrSql = StrSql + "PERSONNELDEP.M22  AS DEPARTMENT22, "
                StrSql = StrSql + "PERSONNELDEP.M23  AS DEPARTMENT23, "
                StrSql = StrSql + "PERSONNELDEP.M24  AS DEPARTMENT24, "
                StrSql = StrSql + "PERSONNELDEP.M25  AS DEPARTMENT25, "
                StrSql = StrSql + "PERSONNELDEP.M26  AS DEPARTMENT26, "
                StrSql = StrSql + "PERSONNELDEP.M27  AS DEPARTMENT27, "
                StrSql = StrSql + "PERSONNELDEP.M28  AS DEPARTMENT28, "
                StrSql = StrSql + "PERSONNELDEP.M29  AS DEPARTMENT29, "
                StrSql = StrSql + "PERSONNELDEP.M30  AS DEPARTMENT30, "
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
                StrSql = StrSql + "2 , 'PERSONNELUK', "
                StrSql = StrSql + "3 , 'PERSONNELGERMANY', "
                StrSql = StrSql + "4,  'PERSONNELSKOREA', "
                StrSql = StrSql + "'NOCOUNTRY' "
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

                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetPersonnelInDetails:" + ex.Message.ToString())
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
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetPlantConfig2Details:" + ex.Message.ToString())
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
                StrSql = StrSql + "'Electricity' AS EnergyType1, "
                StrSql = StrSql + "'Natural Gas' AS  EnergyType2, "
                StrSql = StrSql + "(SELECT PRICE FROM ENERGY WHERE ENERGYID =1) AS  ERGYS1, "
                StrSql = StrSql + "(SELECT PRICE FROM ENERGY WHERE ENERGYID =2) AS  ERGYS2, "
                StrSql = StrSql + "PLANTENERGY.ELECPRICE AS ERGYP1, "
                StrSql = StrSql + "PLANTENERGY.NGASPRICE AS ERGYP2, "
                StrSql = StrSql + "'Mj Per Kwh' AS CONVERSIONFACT1, "
                StrSql = StrSql + "'Mj Per Cubic ft' AS CONVERSIONFACT2, "
                StrSql = StrSql + "'Co2' || PREF.TITLE8 || ' Per Kwh' AS CONVERSIONFACT3, "
                StrSql = StrSql + "'Co2' || PREF.TITLE8 || ' Cubic ft' AS CONVERSIONFACT4, "
                'Changes for Water Start
                StrSql = StrSql + " PREF.TITLE10 || ' Per Kwh' AS CONVERSIONFACT5, "
                StrSql = StrSql + " PREF.TITLE10 || ' Per Cubicft' AS CONVERSIONFACT6, "
                'Changes for Water end
                StrSql = StrSql + "(SELECT MJPKWH FROM ECON.CONVFACTORS2) AS CFACTORSUG1, "
                StrSql = StrSql + "(SELECT MJPCUBICFT FROM ECON.CONVFACTORS2) AS CFACTORSUG2, "
                StrSql = StrSql + "(SELECT CO2LBPKWH FROM ECON.CONVFACTORS2)*PREF.CONVWT AS CFACTORSUG3, "
                StrSql = StrSql + "(SELECT CO2LBPCUBICFT FROM ECON.CONVFACTORS2)*PREF.CONVWT AS CFACTORSUG4, "
                'Changes for Water Start
                StrSql = StrSql + "(SELECT WATERGALPKWH FROM ECON.CONVFACTORS2)*PREF.CONVGALLON AS CFACTORSUG5, "
                StrSql = StrSql + "(SELECT WATERGALPCUBICFT FROM ECON.CONVFACTORS2)*PREF.CONVGALLON AS CFACTORSUG6, "
                'Changes for Water end
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
                'Changes for Water Start
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

                'Changes for Water end
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
                'Changes for Water Start
                StrSql = StrSql + "INNER JOIN PLANTWATER "
                StrSql = StrSql + "ON PLANTWATER.CASEID = PLANTENERGY.CASEID "
                'Changes for Water end
                StrSql = StrSql + "WHERE PLANTENERGY.CASEID =" + CaseId.ToString() + " ORDER BY PLANTENERGY.CASEID "

                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetEnergyDetails:" + ex.Message.ToString())
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
                StrSql = StrSql + "(CNVF.WATER) AS GasolineWATER, "
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
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetTransportDetails:" + ex.Message.ToString())
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
                'Changes for Water Start
                StrSql = StrSql + "'Water:' AS CATEGORY3, "
                'Changes for Water end
                StrSql = StrSql + "(FIXEDCOSTPCT.M1*PREF.CONVWT)EP1, "
                StrSql = StrSql + "(FIXEDCOSTPCT.M2*PREF.CONVWT)EP2, "
                'Changes for Water Start
                StrSql = StrSql + "(FIXEDCOSTPCT.M3*PREF.CONVGALLON)EP3, "
                'Changes for Water end
                StrSql = StrSql + "'weight per employee' AS WASTERULE1, "
                StrSql = StrSql + "'weight per employee' AS WASTERULE2, "
                'Changes for Water Start
                StrSql = StrSql + "'volume per employee' AS WASTERULE3, "
                'Changes for Water end
                StrSql = StrSql + "(FIXEDCOSTSUG.M1*PREF.CONVWT)SUG1, "
                StrSql = StrSql + "(FIXEDCOSTSUG.M2*PREF.CONVWT)SUG2, "
                'Changes for Water Start
                StrSql = StrSql + "(FIXEDCOSTSUG.M3*PREF.CONVGALLON)SUG3, "
                'Changes for Water end
                StrSql = StrSql + "(FIXEDCOSTPRE.M1*PREF.CONVWT)PREF1, "
                StrSql = StrSql + "(FIXEDCOSTPRE.M2*PREF.CONVWT)PREF2, "
                'Changes for Water Start
                StrSql = StrSql + "(FIXEDCOSTPRE.M3*PREF.CONVGALLON)PREF3, "
                'Changes for Water end
                StrSql = StrSql + "FIXEDCOSTDEP.M1 DEP1, "
                StrSql = StrSql + "FIXEDCOSTDEP.M2 DEP2, "
                'Changes for Water Start
                StrSql = StrSql + "FIXEDCOSTDEP.M3 DEP3, "
                'Changes for Water end
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
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetExtraProcessDetails:" + ex.Message.ToString())
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
                'Changes for Water Start
                StrSql = StrSql + "SPACEENERGYPREFPERSQFT.P13*PREF.CONVGALLON/PREF.CONVAREA2 AS ENERGYCONSUMCP1, "
                StrSql = StrSql + "SPACEENERGYPREFPERSQFT.P14*PREF.CONVGALLON/PREF.CONVAREA2 AS ENERGYCONSUMCP2, "
                StrSql = StrSql + "SPACEENERGYPREFPERSQFT.P15*PREF.CONVGALLON/PREF.CONVAREA2 AS ENERGYCONSUMCP3, "
                StrSql = StrSql + "SPACEENERGYPREFPERSQFT.P16*PREF.CONVGALLON/PREF.CONVAREA2 AS ENERGYCONSUMCP4, "

                StrSql = StrSql + "AT4.WATERPERAREA*PREF.CONVGALLON/PREF.CONVAREA2 AS  ENERGYCONSUMCS1, "
                StrSql = StrSql + "AT1.WATERPERAREA*PREF.CONVGALLON/PREF.CONVAREA2 AS  ENERGYCONSUMCS2, "
                StrSql = StrSql + "AT2.WATERPERAREA*PREF.CONVGALLON/PREF.CONVAREA2 AS  ENERGYCONSUMCS3, "
                StrSql = StrSql + "AT3.WATERPERAREA*PREF.CONVGALLON/PREF.CONVAREA2 AS  ENERGYCONSUMCS4, "
                'Changes for Water end

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
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetAdditionalEnergyInfo:" + ex.Message.ToString())
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
                StrSql = StrSql + "(RMERGY+RMPACKERGY+RMANDPACKTRNSPTERGY+PROCERGY+DPPACKERGY+DPTRNSPTERGY+TRSPTTOCUS)TOTERGY, "
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
                StrSql = StrSql + "INNER JOIN MED1.PROCESS PROC1 "
                StrSql = StrSql + "ON PROC1.PROCID = PLANTCONFIG.M1 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS PROC2 "
                StrSql = StrSql + "ON PROC2.PROCID = PLANTCONFIG.M2 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS PROC3 "
                StrSql = StrSql + "ON PROC3.PROCID = PLANTCONFIG.M3 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS PROC4 "
                StrSql = StrSql + "ON PROC4.PROCID = PLANTCONFIG.M4 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS PROC5 "
                StrSql = StrSql + "ON PROC5.PROCID = PLANTCONFIG.M5 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS PROC6 "
                StrSql = StrSql + "ON PROC6.PROCID = PLANTCONFIG.M6 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS PROC7 "
                StrSql = StrSql + "ON PROC7.PROCID = PLANTCONFIG.M7 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS PROC8 "
                StrSql = StrSql + "ON PROC8.PROCID = PLANTCONFIG.M8 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS PROC9 "
                StrSql = StrSql + "ON PROC9.PROCID = PLANTCONFIG.M9 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS PROC10 "
                StrSql = StrSql + "ON PROC10.PROCID = PLANTCONFIG.M10 "
                StrSql = StrSql + "WHERE PLANTCONFIG.CASEID  = " + CaseId.ToString() + ""


                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetCaseViewer:" + ex.Message.ToString())
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
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetAssumptionPageDetails:" + ex.Message.ToString())
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

                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetPageNoteDetails:" + ex.Message.ToString())
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


                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetCaseNoteDetails:" + ex.Message.ToString())
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
                StrSql = StrSql + "(MATOUT.M1*PREF.CONVWT/PREF.CONVAREA) AS WT1, "
                StrSql = StrSql + "(MATOUT.M2*PREF.CONVWT/PREF.CONVAREA) AS WT2, "
                StrSql = StrSql + "(MATOUT.M3*PREF.CONVWT/PREF.CONVAREA) AS WT3, "
                StrSql = StrSql + "(MATOUT.M4*PREF.CONVWT/PREF.CONVAREA) AS WT4, "
                StrSql = StrSql + "(MATOUT.M5*PREF.CONVWT/PREF.CONVAREA) AS WT5, "
                StrSql = StrSql + "(MATOUT.M6*PREF.CONVWT/PREF.CONVAREA) AS WT6, "
                StrSql = StrSql + "(MATOUT.M7*PREF.CONVWT/PREF.CONVAREA) AS WT7, "
                StrSql = StrSql + "(MATOUT.M8*PREF.CONVWT/PREF.CONVAREA) AS WT8, "
                StrSql = StrSql + "(MATOUT.M9*PREF.CONVWT/PREF.CONVAREA) AS WT9, "
                StrSql = StrSql + "(MATOUT.M10*PREF.CONVWT/PREF.CONVAREA) AS WT10, "
                StrSql = StrSql + "MATOUT.P1, "
                StrSql = StrSql + "MATOUT.P2, "
                StrSql = StrSql + "MATOUT.P3, "
                StrSql = StrSql + "MATOUT.P4, "
                StrSql = StrSql + "MATOUT.P5, "
                StrSql = StrSql + "MATOUT.P6, "
                StrSql = StrSql + "MATOUT.P7, "
                StrSql = StrSql + "MATOUT.P8, "
                StrSql = StrSql + "MATOUT.P9, "
                StrSql = StrSql + "MATOUT.P10, "
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
                StrSql = StrSql + "MATOUT.EN1, "
                StrSql = StrSql + "MATOUT.EN2, "
                StrSql = StrSql + "MATOUT.EN3, "
                StrSql = StrSql + "MATOUT.EN4, "
                StrSql = StrSql + "MATOUT.EN5, "
                StrSql = StrSql + "MATOUT.EN6, "
                StrSql = StrSql + "MATOUT.EN7, "
                StrSql = StrSql + "MATOUT.EN8, "
                StrSql = StrSql + "MATOUT.EN9, "
                StrSql = StrSql + "MATOUT.EN10, "
                StrSql = StrSql + "(MATOUT.PURZ1*PREF.CONVWT)AS PURZ1, "
                StrSql = StrSql + "(MATOUT.PURZ2*PREF.CONVWT)AS PURZ2, "
                StrSql = StrSql + "(MATOUT.PURZ3*PREF.CONVWT)AS PURZ3, "
                StrSql = StrSql + "(MATOUT.PURZ4*PREF.CONVWT)AS PURZ4, "
                StrSql = StrSql + "(MATOUT.PURZ5*PREF.CONVWT)AS PURZ5, "
                StrSql = StrSql + "(MATOUT.PURZ6*PREF.CONVWT)AS PURZ6, "
                StrSql = StrSql + "(MATOUT.PURZ7*PREF.CONVWT)AS PURZ7, "
                StrSql = StrSql + "(MATOUT.PURZ8*PREF.CONVWT)AS PURZ8, "
                StrSql = StrSql + "(MATOUT.PURZ9*PREF.CONVWT)AS PURZ9, "
                StrSql = StrSql + "(MATOUT.PURZ10*PREF.CONVWT)AS PURZ10, "
                'Changes for Water Start
                StrSql = StrSql + "(MATOUT.PURW1*PREF.CONVGALLON)AS PURW1, "
                StrSql = StrSql + "(MATOUT.PURW2*PREF.CONVGALLON)AS PURW2, "
                StrSql = StrSql + "(MATOUT.PURW3*PREF.CONVGALLON)AS PURW3, "
                StrSql = StrSql + "(MATOUT.PURW4*PREF.CONVGALLON)AS PURW4, "
                StrSql = StrSql + "(MATOUT.PURW5*PREF.CONVGALLON)AS PURW5, "
                StrSql = StrSql + "(MATOUT.PURW6*PREF.CONVGALLON)AS PURW6, "
                StrSql = StrSql + "(MATOUT.PURW7*PREF.CONVGALLON)AS PURW7, "
                StrSql = StrSql + "(MATOUT.PURW8*PREF.CONVGALLON)AS PURW8, "
                StrSql = StrSql + "(MATOUT.PURW9*PREF.CONVGALLON)AS PURW9, "
                StrSql = StrSql + "(MATOUT.PURW10*PREF.CONVGALLON)AS PURW10, "
                'Changes for Water end

                StrSql = StrSql + "MATOUT.INEN1, "
                StrSql = StrSql + "MATOUT.INEN2, "
                StrSql = StrSql + "MATOUT.INEN3, "
                StrSql = StrSql + "MATOUT.INEN4, "
                StrSql = StrSql + "MATOUT.INEN5, "
                StrSql = StrSql + "MATOUT.INEN6, "
                StrSql = StrSql + "MATOUT.INEN7, "
                StrSql = StrSql + "MATOUT.INEN8, "
                StrSql = StrSql + "MATOUT.INEN9, "
                StrSql = StrSql + "MATOUT.INEN10, "
                StrSql = StrSql + "(MATOUT.INGHG1*PREF.CONVWT)AS INGHG1, "
                StrSql = StrSql + "(MATOUT.INGHG2*PREF.CONVWT)AS INGHG2, "
                StrSql = StrSql + "(MATOUT.INGHG3*PREF.CONVWT)AS INGHG3, "
                StrSql = StrSql + "(MATOUT.INGHG4*PREF.CONVWT)AS INGHG4, "
                StrSql = StrSql + "(MATOUT.INGHG5*PREF.CONVWT)AS INGHG5, "
                StrSql = StrSql + "(MATOUT.INGHG6*PREF.CONVWT)AS INGHG6, "
                StrSql = StrSql + "(MATOUT.INGHG7*PREF.CONVWT)AS INGHG7, "
                StrSql = StrSql + "(MATOUT.INGHG8*PREF.CONVWT)AS INGHG8, "
                StrSql = StrSql + "(MATOUT.INGHG9*PREF.CONVWT)AS INGHG9, "
                StrSql = StrSql + "(MATOUT.INGHG10*PREF.CONVWT)AS INGHG10, "
                StrSql = StrSql + "(MATOUT.CMGHG1*PREF.CONVWT)AS CMGHG1, "
                StrSql = StrSql + "(MATOUT.CMGHG2*PREF.CONVWT)AS CMGHG2, "
                StrSql = StrSql + "(MATOUT.CMGHG3*PREF.CONVWT)AS CMGHG3, "
                StrSql = StrSql + "(MATOUT.CMGHG4*PREF.CONVWT)AS CMGHG4, "
                StrSql = StrSql + "(MATOUT.CMGHG5*PREF.CONVWT)AS CMGHG5, "
                StrSql = StrSql + "(MATOUT.CMGHG6*PREF.CONVWT)AS CMGHG6, "
                StrSql = StrSql + "(MATOUT.CMGHG7*PREF.CONVWT)AS CMGHG7, "
                StrSql = StrSql + "(MATOUT.CMGHG8*PREF.CONVWT)AS CMGHG8, "
                StrSql = StrSql + "(MATOUT.CMGHG9*PREF.CONVWT)AS CMGHG9, "
                StrSql = StrSql + "(MATOUT.CMGHG10*PREF.CONVWT)AS CMGHG10, "
                StrSql = StrSql + "TOTAL.SG AS TOTSG, "
                StrSql = StrSql + "TOTAL.WTPERAREA* PREF.CONVWT/PREF.CONVAREA AS TOTWEIGHT1, "
                StrSql = StrSql + "(MATOUT.P1+ MATOUT.P2 + MATOUT.P3 + MATOUT.P4 + MATOUT.P5 + MATOUT.P6 + MATOUT.P7 + MATOUT.P8 + MATOUT.P9 + MATOUT.P10) AS TOTWEIGHT2, "
                StrSql = StrSql + "(MATOUT.PUR1+ MATOUT.PUR2 + MATOUT.PUR3 + MATOUT.PUR4 + MATOUT.PUR5 + MATOUT.PUR6 + MATOUT.PUR7 + MATOUT.PUR8 + MATOUT.PUR9 + MATOUT.PUR10)*PREF.CONVWT AS TOTSUG, "
                StrSql = StrSql + "(MATOUT.EN1+MATOUT.EN2+MATOUT.EN3+MATOUT.EN4+MATOUT.EN5+MATOUT.EN6+MATOUT.EN7+MATOUT.EN8+MATOUT.EN9+MATOUT.EN10) AS TOTALERGY, "
                StrSql = StrSql + "(MATOUT.PURZ1+ MATOUT.PURZ2 + MATOUT.PURZ3 + MATOUT.PURZ4 + MATOUT.PURZ5 + MATOUT.PURZ6 + MATOUT.PURZ7 + MATOUT.PURZ8 + MATOUT.PURZ9 + MATOUT.PURZ10)*PREF.CONVWT AS TOTCO2, "
                'Changes for Water Start
                StrSql = StrSql + "(MATOUT.PURW1+ MATOUT.PURW2 + MATOUT.PURW3 + MATOUT.PURW4 + MATOUT.PURW5 + MATOUT.PURW6 + MATOUT.PURW7 + MATOUT.PURW8 + MATOUT.PURW9 + MATOUT.PURW10)*PREF.CONVGALLON AS TOTWATER, "
                'Changes for Water end
                StrSql = StrSql + "(MATOUT.INEN1+MATOUT.INEN2+MATOUT.INEN3+MATOUT.INEN4+MATOUT.INEN5+MATOUT.INEN6+MATOUT.INEN7+MATOUT.INEN8+MATOUT.INEN9+MATOUT.INEN10) AS TOTINEN, "
                StrSql = StrSql + "(MATOUT.INGHG1+MATOUT.INGHG2+MATOUT.INGHG3+MATOUT.INGHG4+MATOUT.INGHG5+MATOUT.INGHG6+MATOUT.INGHG7+MATOUT.INGHG8+MATOUT.INGHG9+MATOUT.INGHG10)*PREF.CONVWT AS TOTINGHG, "
                StrSql = StrSql + "(MATOUT.CMGHG1+MATOUT.CMGHG2+MATOUT.CMGHG3+MATOUT.CMGHG4+MATOUT.CMGHG5+MATOUT.CMGHG6+MATOUT.CMGHG7+MATOUT.CMGHG8+MATOUT.CMGHG9+MATOUT.CMGHG10)*PREF.CONVWT AS TOTCMGHG, "
                StrSql = StrSql + "TOTAL.TOTALMATINCINERGY, "
                StrSql = StrSql + "TOTAL.TOTALMATINCINGHG, "
                StrSql = StrSql + "TOTAL.TOTALMATCMPSTGHG, "
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
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MAT1 "
                StrSql = StrSql + "ON MAT1.MATID = MAT.M1 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MAT2 "
                StrSql = StrSql + "ON MAT2.MATID = MAT.M2 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MAT3 "
                StrSql = StrSql + "ON MAT3.MATID = MAT.M3 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MAT4 "
                StrSql = StrSql + "ON MAT4.MATID = MAT.M4 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MAT5 "
                StrSql = StrSql + "ON MAT5.MATID = MAT.M5 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MAT6 "
                StrSql = StrSql + "ON MAT6.MATID = MAT.M6 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MAT7 "
                StrSql = StrSql + "ON MAT7.MATID = MAT.M7 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MAT8 "
                StrSql = StrSql + "ON MAT8.MATID = MAT.M8 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MAT9 "
                StrSql = StrSql + "ON MAT9.MATID = MAT.M9 "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MAT10 "
                StrSql = StrSql + "ON MAT10.MATID = MAT.M10 "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=MAT.CASEID "
                StrSql = StrSql + "INNER JOIN MATERIALOUTPUT MATOUT "
                StrSql = StrSql + "ON MATOUT.CASEID = MAT.CASEID "
                StrSql = StrSql + "INNER JOIN TOTAL "
                StrSql = StrSql + "ON TOTAL.CASEID = MAT.CASEID "
                StrSql = StrSql + "WHERE MAT.CASEID =" + CaseId.ToString() + " ORDER BY MAT.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetExtrusionOutDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPalletInNewout(ByVal CaseId As Integer, ByVal Col As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "PNEWOUT.CASEID, "
                StrSql = StrSql + "(MAT" + Col + ".MATDE1 || ' ' || MAT" + Col + ".MATDE2) AS MATDES, "
                StrSql = StrSql + "'Raw Material  Weight Per Shipping Unit' AS ITEM1, "
                StrSql = StrSql + "'Raw Material Packaging Weight Per Shipping Unit' AS ITEM2, "
                StrSql = StrSql + "'Total Weight  Per Shipping Unit' AS ITEM3, "
                StrSql = StrSql + "'Number Of Shipping Units Per Transport' AS ITEM4, "
                StrSql = StrSql + "'Raw Material Weight Per Transport' AS ITEM5, "
                StrSql = StrSql + "'Raw Material Packaging Weight Per Transport' AS ITEM6, "
                StrSql = StrSql + "'Total Weight  Per Transport' AS ITEM7, "
                StrSql = StrSql + "'Total Number Of Transports' AS ITEM8, "
                StrSql = StrSql + "'Total Raw Material Packaging Weight' AS ITEM9, "
                StrSql = StrSql + "'Raw Material Packaging Energy' AS ITEM10, "
                StrSql = StrSql + "'Raw Material and Packaging Transportation Energy' AS ITEM11, "
                StrSql = StrSql + "'Raw Material Packaging CO2' AS ITEM12, "
                StrSql = StrSql + "'Raw Material and Packaging Transportation Co2' AS ITEM13, "

                StrSql = StrSql + "'Raw Material Packaging Water' AS ITEM14, "
                StrSql = StrSql + "'Raw Material and Packaging Transportation Water' AS ITEM15, "

                StrSql = StrSql + "'Raw Material and Packaging Incineration Energy' AS ITEM16, "
                StrSql = StrSql + "'Raw Material and Packaging Incineration GHG' AS ITEM17, "
                StrSql = StrSql + "'Raw Material and Packaging Compost GHG'  AS ITEM18, "

                StrSql = StrSql + "PNEWOUT.A" + Col + " * PREF.CONVWT AS VAL1, "
                StrSql = StrSql + "PNEWOUT.B" + Col + " * PREF.CONVWT AS VAL2, "
                StrSql = StrSql + "PNEWOUT.C" + Col + " * PREF.CONVWT AS VAL3, "
                StrSql = StrSql + "PNEWOUT.D" + Col + "  AS VAL4, "
                StrSql = StrSql + "PNEWOUT.E" + Col + " * PREF.CONVWT AS VAL5, "
                StrSql = StrSql + "PNEWOUT.F" + Col + " * PREF.CONVWT AS VAL6, "
                StrSql = StrSql + "PNEWOUT.G" + Col + " * PREF.CONVWT AS VAL7, "
                StrSql = StrSql + "PNEWOUT.H" + Col + "  AS VAL8, "
                StrSql = StrSql + "PNEWOUT.I" + Col + " * PREF.CONVWT AS VAL9, "
                StrSql = StrSql + "PNEWOUT.J" + Col + "  AS VAL10, "
                StrSql = StrSql + "PNEWOUT.K" + Col + "  AS VAL11, "
                StrSql = StrSql + "PNEWOUT.L" + Col + " * PREF.CONVWT AS VAL12, "
                StrSql = StrSql + "PNEWOUT.M" + Col + " * PREF.CONVWT AS VAL13, "

                StrSql = StrSql + "PNEWOUT.Q" + Col + " * PREF.CONVGALLON AS VAL14, "
                StrSql = StrSql + "PNEWOUT.R" + Col + " * PREF.CONVGALLON AS VAL15, "

                StrSql = StrSql + "PNEWOUT.N" + Col + "  AS VAL16, "
                StrSql = StrSql + "PNEWOUT.O" + Col + "  AS VAL17, "
                StrSql = StrSql + "PNEWOUT.P" + Col + "  AS VAL18, "

                StrSql = StrSql + "'(' || PREF.TITLE8 || ')' AS UNIT1, "
                StrSql = StrSql + "'(' || PREF.TITLE8 || ')' AS UNIT2, "
                StrSql = StrSql + "'(' || PREF.TITLE8 || ')' AS UNIT3, "
                StrSql = StrSql + "'(unitless)' AS UNIT4, "
                StrSql = StrSql + "'(' || PREF.TITLE8 || ')' AS UNIT5, "
                StrSql = StrSql + "'(' || PREF.TITLE8 || ')' AS UNIT6, "
                StrSql = StrSql + "'(' || PREF.TITLE8 || ')' AS UNIT7, "
                StrSql = StrSql + "'(unitless)' AS UNIT8, "
                StrSql = StrSql + "'(' || PREF.TITLE8 || ')' AS UNIT9, "
                StrSql = StrSql + "'(MJ)' AS UNIT10, "
                StrSql = StrSql + "'(MJ)' AS UNIT11, "
                StrSql = StrSql + "'(' || PREF.TITLE8 || ' gr gas)' AS UNIT12, "
                StrSql = StrSql + "'(' || PREF.TITLE8 || ' gr gas)' AS UNIT13, "

                StrSql = StrSql + "'(' || PREF.TITLE10 || ' water)' AS UNIT14, "
                StrSql = StrSql + "'(' || PREF.TITLE10 || ' water)' AS UNIT15, "

                StrSql = StrSql + "'(MJ)' AS UNIT16, "
                StrSql = StrSql + "'(' || PREF.TITLE8 || ' CO2 / ' || PREF.TITLE8 || ' Mat.)' AS UNIT17, "
                StrSql = StrSql + "'(' || PREF.TITLE8 || ' CO2 / ' || PREF.TITLE8 || ' Mat.)' AS UNIT18, "
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
                StrSql = StrSql + "FROM PALLETINNEWOUT PNEWOUT "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=PNEWOUT.CASEID "
                StrSql = StrSql + "INNER JOIN MATERIALINPUT MATIN "
                StrSql = StrSql + "ON MATIN.CASEID=PNEWOUT.CASEID "
                StrSql = StrSql + "INNER JOIN MED1.MATERIALS MAT" + Col
                StrSql = StrSql + " ON MAT" + Col + ".MATID=MATIN.M" + Col
                StrSql = StrSql + " WHERE PNEWOUT.CASEID =" + CaseId.ToString() + " ORDER BY PNEWOUT.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetPalletInNewout:" + ex.Message.ToString())
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
                StrSql = StrSql + "( CASE WHEN PREF.UNITS =0 THEN "
                StrSql = StrSql + "PREF.TITLE8  ELSE   '' "
                StrSql = StrSql + "END  ) AS SALESUNIT1, "
                StrSql = StrSql + "RESULTSPL.VOLUME*PREF.CONVWT AS SALESUNITVAL1, "
                'StrSql = StrSql + "( CASE WHEN RESULTSPL.finvolmsi >0 THEN "
                'StrSql = StrSql + "( CASE WHEN PREF.units =0 THEN "
                'StrSql = StrSql + "PREF.TITLE8  ELSE   '' "
                'StrSql = StrSql + "END  ) "
                'StrSql = StrSql + "ELSE "
                'StrSql = StrSql + "( CASE WHEN RESULTSPL.finvolmunits >0 THEN "
                'StrSql = StrSql + "PREF.TITLE12 "
                'StrSql = StrSql + "END  ) "
                'StrSql = StrSql + "END  ) AS SALESUNIT2, "
                StrSql = StrSql + "(CASE WHEN RESULTSPL.FINVOLMSI > 0 THEN "
                StrSql = StrSql + "PREF.TITLE3 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "'unit' "
                StrSql = StrSql + "END)SALESUNIT2, "
                StrSql = StrSql + "(CASE WHEN RESULTSPL.FINVOLMSI > 0 THEN "
                StrSql = StrSql + "(RESULTSPL.FINVOLMSI*PREF.CONVAREA) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "RESULTSPL.FINVOLMUNITS "
                StrSql = StrSql + "END)SALESUNITVAL2, "


                'StrSql = StrSql + "( CASE WHEN RESULTSPL.finvolmsi >0 THEN "
                'StrSql = StrSql + "( RESULTSPL.finvolmsi*PREF.CONVAREA) "
                'StrSql = StrSql + "ELSE "
                'StrSql = StrSql + "( CASE WHEN RESULTSPL.finvolmunits >0 THEN "
                'StrSql = StrSql + "RESULTSPL.finvolmunits "
                'StrSql = StrSql + "END  ) "
                'StrSql = StrSql + "END  ) AS SALESUNITVAL2, "
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
                StrSql = StrSql + "Inner Join ResultsPL on "
                StrSql = StrSql + "ResultsPL.CaseId=MATB.CASEID "
                StrSql = StrSql + "WHERE MATB.CASEID =" + CaseId.ToString() + " ORDER BY MATB.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetExtrusionOutDetails:" + ex.Message.ToString())
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
                StrSql = StrSql + "( CASE WHEN POUT.CASEID <= 1000 THEN "
                StrSql = StrSql + "( SELECT (BASECASES.CASEDE1||' '||BASECASES.CASEDE2 ) "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + "WHERE  BASECASES.CASEID = POUT.CASEID "
                StrSql = StrSql + ") ELSE "
                StrSql = StrSql + "( SELECT DISTINCT ( PERMISSIONSCASES.CASEDE1|| ' ' ||PERMISSIONSCASES.CASEDE2) "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE  PERMISSIONSCASES.CASEID = POUT.CASEID "
                StrSql = StrSql + "AND PERMISSIONSCASES.USERID ='1' "
                StrSql = StrSql + ") "
                StrSql = StrSql + "END  ) AS CASEDES, "
                StrSql = StrSql + "'Product Weight Per Pallet' AS ITEM1, "
                StrSql = StrSql + "'Packaging Weight Per Pallet' AS ITEM2, "
                StrSql = StrSql + "'Total Weight  Per Pallet' AS ITEM3, "
                StrSql = StrSql + "'Number Of Pallets Per Truck' AS ITEM4, "
                StrSql = StrSql + "'Product Weight Per Truck' AS ITEM5, "
                StrSql = StrSql + "'Packaging Weight Per Truck'AS ITEM6, "
                StrSql = StrSql + "'Total Weight  Per Truck' AS ITEM7, "
                StrSql = StrSql + "'Total Number Of Trucks' AS ITEM8, "
                StrSql = StrSql + "'Total Energy' AS ITEM9, "
                StrSql = StrSql + "'Total Co2' AS ITEM10, "
                'Changes for Water Start
                StrSql = StrSql + "'Total Water' AS ITEM11, "
                'Changes for Water end
                StrSql = StrSql + "'Incineration Energy' AS ITEM12, "
                StrSql = StrSql + "'Incineration GHG' AS ITEM13, "
                StrSql = StrSql + "'Compost GHG'AS ITEM14, "

                StrSql = StrSql + "(POUT.M1*PREF.CONVWT) AS VALUE1, "
                StrSql = StrSql + "(POUT.M2*PREF.CONVWT) AS VALUE2, "
                StrSql = StrSql + "(POUT.M3*PREF.CONVWT)AS VALUE3, "
                StrSql = StrSql + "POUT.M4 AS VALUE4, "
                StrSql = StrSql + "(POUT.M5*PREF.CONVWT)AS VALUE5, "
                StrSql = StrSql + "(POUT.M6*PREF.CONVWT) AS VALUE6, "
                StrSql = StrSql + "(POUT.M7*PREF.CONVWT) AS VALUE7, "
                StrSql = StrSql + "POUT.M8 AS VALUE8, "
                StrSql = StrSql + "(TOTAL.TOTALOLDPALLETENERGY/PREF.CONVWT) AS VALUE9, "
                StrSql = StrSql + "(TOTAL.TOTPALLETOLDCO2) AS VALUE10, "
                'Changes for Water Start
                StrSql = StrSql + "(TOTAL.TOTPALLETOLDWATER*PREF.CONVGALLON/PREF.CONVWT) AS VALUE11, "
                'Changes for Water end
                StrSql = StrSql + "(TOTAL.TOTALOLDPALLETINCINERGY/PREF.CONVWT) AS VALUE12, "
                StrSql = StrSql + "(TOTAL.TOTALOLDPALLETINCINGHG) AS VALUE13, "
                StrSql = StrSql + "(TOTAL.TOTALOLDPALLETCMPSTGHG) AS VALUE14, "
                StrSql = StrSql + "'('|| PREF.TITLE8 || ')' AS UNIT1, "
                StrSql = StrSql + "'('|| PREF.TITLE8 || ')' AS UNIT2, "
                StrSql = StrSql + "'('|| PREF.TITLE8 || ')' AS UNIT3, "
                StrSql = StrSql + "'(unitless)' AS UNIT4, "
                StrSql = StrSql + "'('|| PREF.TITLE8 || ')' AS UNIT5, "
                StrSql = StrSql + "'('|| PREF.TITLE8 || ')' AS UNIT6, "
                StrSql = StrSql + "'('|| PREF.TITLE8 || ')' AS UNIT7, "
                StrSql = StrSql + "'(unitless)' AS UNIT8, "
                StrSql = StrSql + "'(MJ/' || PREF.TITLE8 || ' Mat.)' AS UNIT9, "
                StrSql = StrSql + "'('|| PREF.Title8 || ' CO2 /' || PREF.TITLE8 || ' Mat.)' AS UNIT10, "
                'Changes for Water Start
                StrSql = StrSql + "'('|| PREF.Title10 || ' Water /' || PREF.TITLE8 || ' Mat.)' AS UNIT11, "
                'Changes for Water end
                StrSql = StrSql + "'(MJ/' || PREF.TITLE8 || ' Mat.)' AS UNIT12, "
                StrSql = StrSql + "'('|| PREF.Title8 || ' CO2 /' || PREF.TITLE8 || ' Mat.)' AS UNIT13, "
                StrSql = StrSql + "'('|| PREF.Title8 || ' CO2 /' || PREF.TITLE8 || ' Mat.)' AS UNIT14, "
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
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetExtrusionOutDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetScoreCard(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT SCREIN.CASEID,  "
                StrSql = StrSql + "( CASE WHEN SCREIN.CASEID <= 1000 THEN "
                StrSql = StrSql + "( SELECT (BASECASES.CASEDE1||' '||BASECASES.CASEDE2 ) "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + "WHERE  BASECASES.CASEID = SCREIN.CASEID "
                StrSql = StrSql + ") ELSE "
                StrSql = StrSql + "( SELECT DISTINCT ( PERMISSIONSCASES.CASEDE1|| ' ' ||PERMISSIONSCASES.CASEDE2) "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE  PERMISSIONSCASES.CASEID = SCREIN.CASEID "
                StrSql = StrSql + "AND PERMISSIONSCASES.USERID ='Administrator' "
                StrSql = StrSql + ") "
                StrSql = StrSql + "END  ) AS CASEDES, "
                StrSql = StrSql + "'Green House Gases' AS TYPE1, "
                StrSql = StrSql + "'Shelf unit packages' AS TYPE2, "
                StrSql = StrSql + "'Sustainable Material' AS TYPE3, "
                StrSql = StrSql + "'Shelf unit packages' AS TYPE4, "
                StrSql = StrSql + "'Transportation Distance' AS TYPE5, "
                StrSql = StrSql + "'Shelf unit transport rating'AS TYPE6, "
                StrSql = StrSql + "'Package to Product ratio' AS TYPE7, "
                StrSql = StrSql + "'Shelf unit packages' AS TYPE8, "
                StrSql = StrSql + "'Cube Utilization' AS TYPE9, "
                StrSql = StrSql + "'Product Selling Unit Volume ratio' AS TYPE10, "
                StrSql = StrSql + "'Pallet Transport Volume use ratio' AS TYPE11, "
                StrSql = StrSql + "'PC Recycled Content' AS TYPE12, "
                StrSql = StrSql + "'Shelf unit packages' AS TYPE13, "
                StrSql = StrSql + "'Recovery' AS TYPE14, "
                StrSql = StrSql + "'Shelf unit packages' AS TYPE15, "
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
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetExtrusionOutDetails:" + ex.Message.ToString())
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
                StrSql = StrSql + "(OPOUT1.PROCDE1||' '||OPOUT1.PROCDE2) AS PROCDE1, "
                StrSql = StrSql + "(OPOUT2.PROCDE1||' '||OPOUT2.PROCDE2) AS PROCDE2, "
                StrSql = StrSql + "(OPOUT3.PROCDE1||' '||OPOUT3.PROCDE2) AS PROCDE3, "
                StrSql = StrSql + "(OPOUT4.PROCDE1||' '||OPOUT4.PROCDE2) AS PROCDE4, "
                StrSql = StrSql + "(OPOUT5.PROCDE1||' '||OPOUT5.PROCDE2) AS PROCDE5, "
                StrSql = StrSql + "(OPOUT6.PROCDE1||' '||OPOUT6.PROCDE2) AS PROCDE6, "
                StrSql = StrSql + "(OPOUT7.PROCDE1||' '||OPOUT7.PROCDE2) AS PROCDE7, "
                StrSql = StrSql + "(OPOUT8.PROCDE1||' '||OPOUT8.PROCDE2) AS PROCDE8, "
                StrSql = StrSql + "(OPOUT9.PROCDE1||' '||OPOUT9.PROCDE2) AS PROCDE9, "
                StrSql = StrSql + "(OPOUT10.PROCDE1||' '||OPOUT10.PROCDE2) AS PROCDE10, "
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
                StrSql = StrSql + "INNER JOIN MED1.PROCESS OPOUT1 "
                StrSql = StrSql + "ON OPOUT1.PROCID = OPOUT.M1 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS OPOUT2 "
                StrSql = StrSql + "ON OPOUT2.PROCID = OPOUT.M2 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS OPOUT3 "
                StrSql = StrSql + "ON OPOUT3.PROCID = OPOUT.M3 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS OPOUT4 "
                StrSql = StrSql + "ON OPOUT4.PROCID = OPOUT.M4 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS OPOUT5 "
                StrSql = StrSql + "ON OPOUT5.PROCID = OPOUT.M5 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS OPOUT6 "
                StrSql = StrSql + "ON OPOUT6.PROCID = OPOUT.M6 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS OPOUT7 "
                StrSql = StrSql + "ON OPOUT7.PROCID = OPOUT.M7 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS OPOUT8 "
                StrSql = StrSql + "ON OPOUT8.PROCID = OPOUT.M8 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS OPOUT9 "
                StrSql = StrSql + "ON OPOUT9.PROCID = OPOUT.M9 "
                StrSql = StrSql + "INNER JOIN MED1.PROCESS OPOUT10 "
                StrSql = StrSql + "ON OPOUT10.PROCID = OPOUT.M10 "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=OPOUT.CASEID "
                StrSql = StrSql + "INNER JOIN OPDEPVOL "
                StrSql = StrSql + "ON OPDEPVOL.CASEID = OPOUT.CASEID "
                StrSql = StrSql + "WHERE OPOUT.CASEID =" + CaseId.ToString() + "   ORDER BY OPOUT.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetOperationsOut:" + ex.Message.ToString())
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
                StrSql = StrSql + "'Raw Materials' As A1, "
                StrSql = StrSql + "'Raw Materials Packaging' AS A2, "
                StrSql = StrSql + "'RM & Pack Transport' AS A3, "
                StrSql = StrSql + "'Process' AS A4, "
                StrSql = StrSql + "'Distribution Packaging' AS A5, "
                StrSql = StrSql + "'DP Transport' AS A6, "
                StrSql = StrSql + "'Transport to Customer' AS A7, "
                StrSql = StrSql + "'Total Energy' AS A8, "
                StrSql = StrSql + "'Purchased Materials' AS A9, "
                StrSql = StrSql + "'Process' AS A10, "
                StrSql = StrSql + "'Transportation' AS A11, "
                StrSql = StrSql + "'Total Energy' AS A12, "
                StrSql = StrSql + "TOT.RMERGY AS T1, "
                StrSql = StrSql + "TOT.RMPACKERGY AS T2, "
                StrSql = StrSql + "TOT.RMANDPACKTRNSPTERGY AS T3, "
                StrSql = StrSql + "TOT.PROCERGY AS T4, "
                StrSql = StrSql + "TOT.DPPACKERGY AS T5, "
                StrSql = StrSql + "TOT.DPTRNSPTERGY AS T6, "
                StrSql = StrSql + "TOT.TRSPTTOCUS AS T7, "
                StrSql = StrSql + "(TOT.RMERGY+TOT.RMPACKERGY+TOT.RMANDPACKTRNSPTERGY+TOT.PROCERGY+TOT.DPPACKERGY+TOT.DPTRNSPTERGY+TOT.TRSPTTOCUS) T8, "
                StrSql = StrSql + "(TOT.RMERGY+TOT.RMPACKERGY+TOT.DPPACKERGY) AS T9, "
                StrSql = StrSql + "TOT.PROCERGY AS T10, "
                StrSql = StrSql + "(TOT.RMANDPACKTRNSPTERGY+TOT.DPTRNSPTERGY+TOT.TRSPTTOCUS) AS  T11, "
                StrSql = StrSql + "(TOT.RMERGY+TOT.RMPACKERGY+TOT.RMANDPACKTRNSPTERGY+TOT.PROCERGY+TOT.DPPACKERGY+TOT.DPTRNSPTERGY+TOT.TRSPTTOCUS) T12, "
                StrSql = StrSql + "(RSPL.VOLUME*PREF.CONVWT)SVOLUME, "
                StrSql = StrSql + "(CASE WHEN RSPL.FINVOLMSI > 0 THEN "
                StrSql = StrSql + "(RSPL.FINVOLMSI*PREF.CONVAREA) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "RSPL.FINVOLMUNITS "
                StrSql = StrSql + "END)SUNITVAL, "
                StrSql = StrSql + "(CASE WHEN RSPL.FINVOLMSI > 0 THEN "
                StrSql = StrSql + "'MJ/'||PREF.TITLE3 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "'MJ/unit' "
                StrSql = StrSql + "END)SUNIT, "
                StrSql = StrSql + "(CASE  WHEN FINVOLMSI > 1 THEN "
                StrSql = StrSql + "PREF.TITLE3 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "'units' "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END)SUNITLBL, "
                StrSql = StrSql + "FINVOLMSI, "
                StrSql = StrSql + "FINVOLMUNITS, "
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
                StrSql = StrSql + "PREF.CONVAREA, "
                StrSql = StrSql + " CUSSALESVOLUME, "
                StrSql = StrSql + " CUSSALESUNIT "

                StrSql = StrSql + "FROM TOTAL TOT "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = TOT.CASEID "
                StrSql = StrSql + "INNER JOIN RESULTSPL RSPL "
                StrSql = StrSql + "ON RSPL.CASEID = TOT.CASEID "
                StrSql = StrSql + "WHERE TOT.CASEID =" + CaseId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetEnergyResults:" + ex.Message.ToString())
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
                StrSql = StrSql + "'Raw Materials' As A1, "
                StrSql = StrSql + "'Raw Materials Packaging' AS A2, "
                StrSql = StrSql + "'RM & Pack Transport' AS A3, "
                StrSql = StrSql + "'Process' AS A4, "
                StrSql = StrSql + "'Distribution Packaging' AS A5, "
                StrSql = StrSql + "'DP Transport' AS A6, "
                StrSql = StrSql + "'Transport to Customer' AS A7, "
                StrSql = StrSql + "'Total Greenhouse Gas' AS A8, "
                StrSql = StrSql + "'Purchased Materials' AS A9, "
                StrSql = StrSql + "'Process' AS A10, "
                StrSql = StrSql + "'Transportation' AS A11, "
                StrSql = StrSql + "'Total Greenhouse Gas' AS A12, "
                StrSql = StrSql + "(TOT.RMGRNHUSGAS*PREF.CONVWT) AS T1, "
                StrSql = StrSql + "(TOT.RMPACKGRNHUSGAS*PREF.CONVWT) AS T2, "
                StrSql = StrSql + "(TOT.RMANDPACKTRNSPTGRNHUSGAS*PREF.CONVWT) AS T3, "
                StrSql = StrSql + "(TOT.PROCGRNHUSGAS*PREF.CONVWT) AS T4, "
                StrSql = StrSql + "(TOT.DPPACKGRNHUSGAS*PREF.CONVWT) AS T5, "
                StrSql = StrSql + "(TOT.DPTRNSPTGRNHUSGAS*PREF.CONVWT) AS T6, "
                StrSql = StrSql + "(TOT.TRSPTTOCUSGRNHUSGAS*PREF.CONVWT) AS T7, "
                StrSql = StrSql + "((TOT.RMGRNHUSGAS+TOT.RMPACKGRNHUSGAS+TOT.RMANDPACKTRNSPTGRNHUSGAS+TOT.PROCGRNHUSGAS+TOT.DPPACKGRNHUSGAS+TOT.DPTRNSPTGRNHUSGAS+TOT.TRSPTTOCUSGRNHUSGAS)*PREF.CONVWT) T8, "
                StrSql = StrSql + "((TOT.RMGRNHUSGAS+TOT.RMPACKGRNHUSGAS+TOT.DPPACKGRNHUSGAS)*PREF.CONVWT) AS T9, "
                StrSql = StrSql + "(TOT.PROCGRNHUSGAS*PREF.CONVWT) AS T10, "
                StrSql = StrSql + "((TOT.RMANDPACKTRNSPTGRNHUSGAS+TOT.DPTRNSPTGRNHUSGAS+TOT.TRSPTTOCUSGRNHUSGAS)*PREF.CONVWT) AS  T11, "
                StrSql = StrSql + "((TOT.RMGRNHUSGAS+TOT.RMPACKGRNHUSGAS+TOT.RMANDPACKTRNSPTGRNHUSGAS+TOT.PROCGRNHUSGAS+TOT.DPPACKGRNHUSGAS+TOT.DPTRNSPTGRNHUSGAS+TOT.TRSPTTOCUSGRNHUSGAS)*PREF.CONVWT) T12, "
                StrSql = StrSql + "(RSPL.VOLUME*PREF.CONVWT)SVOLUME, "
                StrSql = StrSql + "(CASE WHEN RSPL.FINVOLMSI > 0 THEN "
                StrSql = StrSql + "(RSPL.FINVOLMSI*PREF.CONVAREA) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "RSPL.FINVOLMUNITS "
                StrSql = StrSql + "END)SUNITVAL, "
                StrSql = StrSql + "(CASE WHEN RSPL.FINVOLMSI > 0 THEN "
                StrSql = StrSql + "PREF.TITLE8||' gr gas /'||PREF.TITLE3 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "PREF.TITLE8||' gr gas /unit' "
                StrSql = StrSql + "END)SUNIT, "
                StrSql = StrSql + "(CASE  WHEN FINVOLMSI > 1 THEN "
                StrSql = StrSql + "PREF.TITLE3 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "'units' "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END)SUNITLBL, "
                StrSql = StrSql + "FINVOLMSI, "
                StrSql = StrSql + "FINVOLMUNITS, "
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
                StrSql = StrSql + "PREF.CONVAREA, "
                StrSql = StrSql + " CUSSALESVOLUME, "
                StrSql = StrSql + " CUSSALESUNIT "
                StrSql = StrSql + "FROM TOTAL TOT "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = TOT.CASEID "
                StrSql = StrSql + "INNER JOIN RESULTSPL RSPL "
                StrSql = StrSql + "ON RSPL.CASEID = TOT.CASEID "
                StrSql = StrSql + "WHERE TOT.CASEID =" + CaseId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetGhgResults:" + ex.Message.ToString())
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
                StrSql = StrSql + "MATB.M1*PREF.CONVWT AS MATB2, "
                StrSql = StrSql + "MATB.M2*PREF.CONVWT AS MATB3, "
                StrSql = StrSql + "MATB.M3*PREF.CONVWT AS MATB4, "
                StrSql = StrSql + "MATB.M4*PREF.CONVWT AS MATB6, "
                StrSql = StrSql + "MATB.M5*PREF.CONVWT AS MATB7, "
                StrSql = StrSql + "MATB.M6*PREF.CONVWT AS MATB9, "
                StrSql = StrSql + "MATB.M7*PREF.CONVWT AS MATB10, "
                StrSql = StrSql + "MATB.M8*PREF.CONVWT AS MATB12, "
                StrSql = StrSql + "MATB.M9*PREF.CONVWT AS MATB13, "
                StrSql = StrSql + "MALOUT.MR1*PREF.CONVWT AS MATRE2, "
                StrSql = StrSql + "MALOUT.MR2*PREF.CONVWT AS MATRE3, "
                StrSql = StrSql + "MALOUT.MR3*PREF.CONVWT AS MATRE4, "
                StrSql = StrSql + "MALOUT.MR4*PREF.CONVWT AS MATRE6, "
                StrSql = StrSql + "MALOUT.MR5*PREF.CONVWT AS MATRE7, "
                StrSql = StrSql + "MALOUT.MR6*PREF.CONVWT AS MATRE9, "
                StrSql = StrSql + "MALOUT.MR7*PREF.CONVWT AS MATRE10, "
                StrSql = StrSql + "MALOUT.MR8*PREF.CONVWT AS MATRE12, "
                StrSql = StrSql + "MALOUT.MR9*PREF.CONVWT AS MATRE13, "
                StrSql = StrSql + "MALOUT.MI1*PREF.CONVWT AS MATIN2, "
                StrSql = StrSql + "MALOUT.MI2*PREF.CONVWT AS MATIN3, "
                StrSql = StrSql + "MALOUT.MI3*PREF.CONVWT AS MATIN4, "
                StrSql = StrSql + "MALOUT.MI4*PREF.CONVWT AS MATIN6, "
                StrSql = StrSql + "MALOUT.MI5*PREF.CONVWT AS MATIN7, "
                StrSql = StrSql + "MALOUT.MI6*PREF.CONVWT AS MATIN9, "
                StrSql = StrSql + "MALOUT.MI7*PREF.CONVWT AS MATIN10, "
                StrSql = StrSql + "MALOUT.MI8*PREF.CONVWT AS MATIN12, "
                StrSql = StrSql + "MALOUT.MI9*PREF.CONVWT AS MATIN13, "
                StrSql = StrSql + "MALOUT.MC1*PREF.CONVWT AS MATCM2, "
                StrSql = StrSql + "MALOUT.MC2*PREF.CONVWT AS MATCM3, "
                StrSql = StrSql + "MALOUT.MC3*PREF.CONVWT AS MATCM4, "
                StrSql = StrSql + "MALOUT.MC4*PREF.CONVWT AS MATCM6, "
                StrSql = StrSql + "MALOUT.MC5*PREF.CONVWT AS MATCM7, "
                StrSql = StrSql + "MALOUT.MC6*PREF.CONVWT AS MATCM9, "
                StrSql = StrSql + "MALOUT.MC7*PREF.CONVWT AS MATCM10, "
                StrSql = StrSql + "MALOUT.MC8*PREF.CONVWT AS MATCM12, "
                StrSql = StrSql + "MALOUT.MC9*PREF.CONVWT AS MATCM13, "
                StrSql = StrSql + "MALOUT.ML1*PREF.CONVWT AS MATLF2, "
                StrSql = StrSql + "MALOUT.ML2*PREF.CONVWT AS MATLF3, "
                StrSql = StrSql + "MALOUT.ML3*PREF.CONVWT AS MATLF4, "
                StrSql = StrSql + "MALOUT.ML4*PREF.CONVWT AS MATLF6, "
                StrSql = StrSql + "MALOUT.ML5*PREF.CONVWT AS MATLF7, "
                StrSql = StrSql + "MALOUT.ML6*PREF.CONVWT AS MATLF9, "
                StrSql = StrSql + "MALOUT.ML7*PREF.CONVWT AS MATLF10, "
                StrSql = StrSql + "MALOUT.ML8*PREF.CONVWT AS MATLF12, "
                StrSql = StrSql + "MALOUT.ML9*PREF.CONVWT AS MATLF13, "
                StrSql = StrSql + "(RSPL.VOLUME*PREF.CONVWT)SVOLUME, "
                StrSql = StrSql + "(CASE WHEN RSPL.FINVOLMSI > 0 THEN "
                StrSql = StrSql + "(RSPL.FINVOLMSI*PREF.CONVAREA) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "RSPL.FINVOLMUNITS "
                StrSql = StrSql + "END)SUNITVAL, "
                StrSql = StrSql + "(CASE WHEN RSPL.FINVOLMSI > 0 THEN "
                StrSql = StrSql + "PREF.TITLE3 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "'unit' "
                StrSql = StrSql + "END)SUNIT, "
                StrSql = StrSql + "(CASE  WHEN FINVOLMSI > 1 THEN "
                StrSql = StrSql + "PREF.TITLE3 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "'units' "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END)SUNITLBL, "
                StrSql = StrSql + "FINVOLMSI, "
                StrSql = StrSql + "FINVOLMUNITS, "
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
                StrSql = StrSql + "PREF.CONVAREA, "
                StrSql = StrSql + " CUSSALESVOLUME, "
                StrSql = StrSql + " CUSSALESUNIT "
                StrSql = StrSql + "FROM MATERIALBALANCE MATB "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = MATB.CASEID "
                StrSql = StrSql + "INNER JOIN MATENDOFLIFEOUT MALOUT "
                StrSql = StrSql + "ON MALOUT.CASEID = MATB.CASEID "
                StrSql = StrSql + "INNER JOIN RESULTSPL RSPL "
                StrSql = StrSql + "ON RSPL.CASEID = MATB.CASEID "
                StrSql = StrSql + "INNER JOIN TOTAL "
                StrSql = StrSql + "ON TOTAL.CASEID = MATB.CASEID "
                StrSql = StrSql + "WHERE MATB.CASEID =" + CaseId.ToString() + " "



                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetEOLResults:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetWaterResults(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "TOT.CASEID, "
                StrSql = StrSql + "'Raw Materials' As A1, "
                StrSql = StrSql + "'Raw Materials Packaging' AS A2, "
                StrSql = StrSql + "'RM & Pack Transport' AS A3, "
                StrSql = StrSql + "'Process' AS A4, "
                StrSql = StrSql + "'Distribution Packaging' AS A5, "
                StrSql = StrSql + "'DP Transport' AS A6, "
                StrSql = StrSql + "'Transport to Customer' AS A7, "
                StrSql = StrSql + "'Total Water' AS A8, "
                StrSql = StrSql + "'Purchased Materials' AS A9, "
                StrSql = StrSql + "'Process' AS A10, "
                StrSql = StrSql + "'Transportation' AS A11, "
                StrSql = StrSql + "'Total Water' AS A12, "
                StrSql = StrSql + "(TOT.RMWATER*PREF.CONVGALLON) AS T1, "
                StrSql = StrSql + "(TOT.RMPACKWATER*PREF.CONVGALLON) AS T2, "
                StrSql = StrSql + "(TOT.RMANDPACKTRNSPTWATER*PREF.CONVGALLON) AS T3, "
                StrSql = StrSql + "(TOT.PROCWATER*PREF.CONVGALLON) AS T4, "
                StrSql = StrSql + "(TOT.DPPACKWATER*PREF.CONVGALLON) AS T5, "
                StrSql = StrSql + "(TOT.DPTRNSPTWATER*PREF.CONVGALLON) AS T6, "
                StrSql = StrSql + "(TOT.TRSPTTOCUSWATER*PREF.CONVGALLON) AS T7, "
                StrSql = StrSql + "((TOT.RMWATER+TOT.RMPACKWATER+TOT.RMANDPACKTRNSPTWATER+TOT.PROCWATER+TOT.DPPACKWATER+TOT.DPTRNSPTWATER+TOT.TRSPTTOCUSWATER)*PREF.CONVGALLON) T8, "
                StrSql = StrSql + "((TOT.RMWATER+TOT.RMPACKWATER+TOT.DPPACKWATER)*PREF.CONVGALLON) AS T9, "
                StrSql = StrSql + "(TOT.PROCWATER*PREF.CONVGALLON) AS T10, "
                StrSql = StrSql + "((TOT.RMANDPACKTRNSPTWATER+TOT.DPTRNSPTWATER+TOT.TRSPTTOCUSWATER)*PREF.CONVGALLON) AS  T11, "
                StrSql = StrSql + "((TOT.RMWATER+TOT.RMPACKWATER+TOT.RMANDPACKTRNSPTWATER+TOT.PROCWATER+TOT.DPPACKWATER+TOT.DPTRNSPTWATER+TOT.TRSPTTOCUSWATER)*PREF.CONVGALLON) T12, "
                StrSql = StrSql + "(RSPL.VOLUME*PREF.CONVWT)SVOLUME, "
                StrSql = StrSql + "(CASE WHEN RSPL.FINVOLMSI > 0 THEN "
                StrSql = StrSql + "(RSPL.FINVOLMSI*PREF.CONVAREA) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "RSPL.FINVOLMUNITS "
                StrSql = StrSql + "END)SUNITVAL, "
                StrSql = StrSql + "(CASE WHEN RSPL.FINVOLMSI > 0 THEN "
                StrSql = StrSql + "PREF.TITLE10||' water /'||PREF.TITLE3 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "PREF.TITLE10||' water /unit' "
                StrSql = StrSql + "END)SUNIT, "
                StrSql = StrSql + "(CASE  WHEN FINVOLMSI > 1 THEN "
                StrSql = StrSql + "PREF.TITLE3 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "'units' "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END)SUNITLBL, "
                StrSql = StrSql + "FINVOLMSI, "
                StrSql = StrSql + "FINVOLMUNITS, "
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
                StrSql = StrSql + "PREF.CONVAREA, "
                StrSql = StrSql + "PREF.CONVGALLON, "
                StrSql = StrSql + "PREF.UNITS, "
                StrSql = StrSql + "CUSSALESVOLUME, "
                StrSql = StrSql + "CUSSALESUNIT "
                StrSql = StrSql + "FROM TOTAL TOT "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = TOT.CASEID "
                StrSql = StrSql + "INNER JOIN RESULTSPL RSPL "
                StrSql = StrSql + "ON RSPL.CASEID = TOT.CASEID "
                StrSql = StrSql + "WHERE TOT.CASEID =" + CaseId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetWaterResults:" + ex.Message.ToString())
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
                StrSql = StrSql + "(SELECT LS1 FROM RANGES) AS LOWRANGES1, "
                StrSql = StrSql + "(SELECT LS2 FROM RANGES) AS LOWRANGES2, "
                StrSql = StrSql + "(SELECT LS3 FROM RANGES) AS LOWRANGES3, "
                StrSql = StrSql + "(SELECT LS4 FROM RANGES) AS LOWRANGES4, "
                StrSql = StrSql + "(SELECT LS5 FROM RANGES) AS LOWRANGES5, "
                StrSql = StrSql + "(SELECT LS6 FROM RANGES) AS LOWRANGES6, "
                StrSql = StrSql + "(SELECT LS7 FROM RANGES) AS LOWRANGES7, "
                StrSql = StrSql + "(SELECT LS8 FROM RANGES) AS LOWRANGES8, "
                StrSql = StrSql + "(SELECT LS9 FROM RANGES) AS LOWRANGES9, "
                StrSql = StrSql + "RANGESPREF.LP1 AS LOWRANGEP1, "
                StrSql = StrSql + "RANGESPREF.LP2 AS LOWRANGEP2, "
                StrSql = StrSql + "RANGESPREF.LP3 AS LOWRANGEP3, "
                StrSql = StrSql + "RANGESPREF.LP4 AS LOWRANGEP4, "
                StrSql = StrSql + "RANGESPREF.LP5 AS LOWRANGEP5, "
                StrSql = StrSql + "RANGESPREF.LP6 AS LOWRANGEP6, "
                StrSql = StrSql + "RANGESPREF.LP7 AS LOWRANGEP7, "
                StrSql = StrSql + "RANGESPREF.LP8 AS LOWRANGEP8, "
                StrSql = StrSql + "RANGESPREF.LP9 AS LOWRANGEP9, "
                StrSql = StrSql + "(SELECT HS1 FROM RANGES) AS HIGHRANGES1, "
                StrSql = StrSql + "(SELECT HS2 FROM RANGES) AS HIGHRANGES2, "
                StrSql = StrSql + "(SELECT HS3 FROM RANGES) AS HIGHRANGES3, "
                StrSql = StrSql + "(SELECT HS4 FROM RANGES) AS HIGHRANGES4, "
                StrSql = StrSql + "(SELECT HS5 FROM RANGES) AS HIGHRANGES5, "
                StrSql = StrSql + "(SELECT HS6 FROM RANGES) AS HIGHRANGES6, "
                StrSql = StrSql + "(SELECT HS7 FROM RANGES) AS HIGHRANGES7, "
                StrSql = StrSql + "(SELECT HS8 FROM RANGES) AS HIGHRANGES8, "
                StrSql = StrSql + "(SELECT HS9 FROM RANGES) AS HIGHRANGES9, "
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
                StrSql = StrSql + "(M1+M2+M3+M4+M5+M6+M7+M8+M9)RWT, "
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


                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetScoreCardResults:" + ex.Message.ToString())
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
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw
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
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedSustain1ConnectionString")
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
                StrSql = StrSql + "PREF.TITLE3, "
                StrSql = StrSql + "RESULTSPL.FINVOLMSI,"
                StrSql = StrSql + "RESULTSPL.FINVOLMUNITS,"
                StrSql = StrSql + "RESULTSPL.VOLUME, "
                StrSql = StrSql + "RMERGY, "
                StrSql = StrSql + "RMPACKERGY, "
                StrSql = StrSql + "RMANDPACKTRNSPTERGY, "
                StrSql = StrSql + "PROCERGY, "
                StrSql = StrSql + "DPPACKERGY, "
                StrSql = StrSql + "DPTRNSPTERGY, "
                StrSql = StrSql + "TRSPTTOCUS, "
                StrSql = StrSql + "(RMERGY+RMPACKERGY+DPPACKERGY)PURMATERIALERGY, "
                StrSql = StrSql + "(RMANDPACKTRNSPTERGY+DPTRNSPTERGY+TRSPTTOCUS)TRNSPTERGY, "
                StrSql = StrSql + "(RMERGY+RMPACKERGY+RMANDPACKTRNSPTERGY+PROCERGY+DPPACKERGY+DPTRNSPTERGY+TRSPTTOCUS)TOTALENERGY, "
                StrSql = StrSql + "CUSSALESVOLUME, "
                StrSql = StrSql + "CUSSALESUNIT, "
                StrSql = StrSql + "NVL((CASE  WHEN FINVOLMSI > 1 THEN "
                StrSql = StrSql + "(FINVOLMSI)"
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "(FINVOLMUNITS) "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END),0)SUNITVAL "
                StrSql = StrSql + "FROM TOTAL "
                StrSql = StrSql + "INNER JOIN RESULTSPL "
                StrSql = StrSql + "ON RESULTSPL.CASEID = TOTAL.CASEID "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = TOTAL.CASEID "
                StrSql = StrSql + "WHERE TOTAL.CASEID IN (" + CaseId1 + "," + CaseId2 + ") "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetChartErgyRes:" + ex.Message.ToString())
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
                StrSql = StrSql + "PREF.TITLE3, "
                StrSql = StrSql + "RESULTSPL.FINVOLMSI,"
                StrSql = StrSql + "RESULTSPL.FINVOLMUNITS,"
                StrSql = StrSql + "RESULTSPL.VOLUME, "
                StrSql = StrSql + "RMGRNHUSGAS, "
                StrSql = StrSql + "RMPACKGRNHUSGAS, "
                StrSql = StrSql + "RMANDPACKTRNSPTGRNHUSGAS, "
                StrSql = StrSql + "PROCGRNHUSGAS, "
                StrSql = StrSql + "DPPACKGRNHUSGAS, "
                StrSql = StrSql + "DPTRNSPTGRNHUSGAS, "
                StrSql = StrSql + "TRSPTTOCUSGRNHUSGAS, "
                StrSql = StrSql + "(RMGRNHUSGAS+RMPACKGRNHUSGAS+DPPACKGRNHUSGAS)PURMATERIALGRNHUSGAS, "
                StrSql = StrSql + "(RMANDPACKTRNSPTGRNHUSGAS+DPTRNSPTGRNHUSGAS+TRSPTTOCUSGRNHUSGAS)TRNSPTGRNHUSGAS, "
                StrSql = StrSql + "(RMGRNHUSGAS+RMPACKGRNHUSGAS+RMANDPACKTRNSPTGRNHUSGAS+PROCGRNHUSGAS+DPPACKGRNHUSGAS+DPTRNSPTGRNHUSGAS+TRSPTTOCUSGRNHUSGAS)TOTALENGRNHUSGAS ,"
                StrSql = StrSql + "CUSSALESVOLUME, "
                StrSql = StrSql + "CUSSALESUNIT, "
                StrSql = StrSql + "NVL((CASE  WHEN FINVOLMSI > 1 THEN "
                StrSql = StrSql + "(FINVOLMSI)"
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "(FINVOLMUNITS) "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END),0)SUNITVAL "
                StrSql = StrSql + "FROM TOTAL "
                StrSql = StrSql + "INNER JOIN RESULTSPL "
                StrSql = StrSql + "ON RESULTSPL.CASEID = TOTAL.CASEID "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = TOTAL.CASEID "
                StrSql = StrSql + "WHERE TOTAL.CASEID IN (" + CaseId1 + "," + CaseId2 + ") "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetChartGhgRes:" + ex.Message.ToString())
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
                StrSql = StrSql + "PREF.TITLE3, "
                StrSql = StrSql + "RESULTSPL.FINVOLMSI,"
                StrSql = StrSql + "RESULTSPL.FINVOLMUNITS,"
                StrSql = StrSql + "RESULTSPL.VOLUME, "
                StrSql = StrSql + "RMWATER, "
                StrSql = StrSql + "RMPACKWATER, "
                StrSql = StrSql + "RMANDPACKTRNSPTWATER, "
                StrSql = StrSql + "PROCWATER, "
                StrSql = StrSql + "DPPACKWATER, "
                StrSql = StrSql + "DPTRNSPTWATER, "
                StrSql = StrSql + "TRSPTTOCUSWATER, "
                StrSql = StrSql + "(RMWATER+RMPACKWATER+DPPACKWATER)PURMATERIALWATER, "
                StrSql = StrSql + "(RMANDPACKTRNSPTWATER+DPTRNSPTWATER+TRSPTTOCUSWATER)TRNSPTWATER, "
                StrSql = StrSql + "(RMWATER+RMPACKWATER+RMANDPACKTRNSPTWATER+PROCWATER+DPPACKWATER+DPTRNSPTWATER+TRSPTTOCUSWATER)TOTALENWATER, "
                StrSql = StrSql + "CUSSALESVOLUME CUSSALESVOLUME, "
                StrSql = StrSql + "CUSSALESUNIT, "
                StrSql = StrSql + "NVL((CASE  WHEN FINVOLMSI > 1 THEN "
                StrSql = StrSql + "(FINVOLMSI)"
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "(FINVOLMUNITS) "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END),0)SUNITVAL "
                StrSql = StrSql + "FROM TOTAL "
                StrSql = StrSql + "INNER JOIN RESULTSPL "
                StrSql = StrSql + "ON RESULTSPL.CASEID = TOTAL.CASEID "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = TOTAL.CASEID "
                StrSql = StrSql + "WHERE TOTAL.CASEID IN (" + CaseId1 + "," + CaseId2 + ") "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetChartGhgRes:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetChartEOLRes(ByVal CaseId1 As String, ByVal CaseId2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT  DISTINCT  "
                StrSql = StrSql + "MATB.CASEID, "
                StrSql = StrSql + "( CASE WHEN MATB.CASEID <= 1000 THEN "
                StrSql = StrSql + "( SELECT (BASECASES.CASEID||':'||REPLACE(REPLACE((REPLACE(NVL(BASECASES.CASEDE1,''),')','}')),'(','{')||' '||REPLACE((REPLACE(NVL(BASECASES.CASEDE2,''),')','}')),'(','{'),';',':') )  "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + "WHERE  BASECASES.CASEID = MATB.CASEID "
                StrSql = StrSql + ") ELSE "
                StrSql = StrSql + "( SELECT DISTINCT (PERMISSIONSCASES.CASEID||':'||REPLACE(REPLACE((REPLACE(NVL(PERMISSIONSCASES.CASEDE1,''),')','}')),'(','{') || ' ' || REPLACE((REPLACE(NVL(PERMISSIONSCASES.CASEDE2,''),')','}')),'(','{'),';',':') )  "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE  PERMISSIONSCASES.CASEID = MATB.CASEID "
                StrSql = StrSql + ") "
                StrSql = StrSql + "END  ) AS CASEDE, "
                StrSql = StrSql + "round(MATB.M1,28)  AS MATB1, "
                StrSql = StrSql + "round(MATB.M2,28)  AS MATB2, "
                StrSql = StrSql + "round(MATB.M3,28)  AS MATB3, "
                StrSql = StrSql + "round(MATB.M4,28)  AS MATB4, "
                StrSql = StrSql + "round(MATB.M5,28)  AS MATB5, "
                StrSql = StrSql + "round(MATB.M6,28)  AS MATB6, "
                StrSql = StrSql + "round(MATB.M7,28)  AS MATB7, "
                StrSql = StrSql + "round(MATB.M8,28)  AS MATB8, "
                StrSql = StrSql + "round(MATB.M9,28)  AS MATB9, "

                StrSql = StrSql + "round(MALOUT.MR1,28)  AS MATRE1, "
                StrSql = StrSql + "round(MALOUT.MR2,28)  AS MATRE2, "
                StrSql = StrSql + "round(MALOUT.MR3,28)  AS MATRE3, "
                StrSql = StrSql + "round(MALOUT.MR4,28)  AS MATRE4, "
                StrSql = StrSql + "round(MALOUT.MR5,28)  AS MATRE5, "
                StrSql = StrSql + "round(MALOUT.MR6,28)  AS MATRE6, "
                StrSql = StrSql + "round(MALOUT.MR7,28)  AS MATRE7, "
                StrSql = StrSql + "round(MALOUT.MR8,28)  AS MATRE8, "
                StrSql = StrSql + "round(MALOUT.MR9,28)  AS MATRE9, "

                StrSql = StrSql + "round(MALOUT.MI1,28)  AS MATIN1, "
                StrSql = StrSql + "round(MALOUT.MI2,28)  AS MATIN2, "
                StrSql = StrSql + "round(MALOUT.MI3,28)  AS MATIN3, "
                StrSql = StrSql + "round(MALOUT.MI4,28)  AS MATIN4, "
                StrSql = StrSql + "round(MALOUT.MI5,28)  AS MATIN5, "
                StrSql = StrSql + "round(MALOUT.MI6,28)  AS MATIN6, "
                StrSql = StrSql + "round(MALOUT.MI7,28)  AS MATIN7, "
                StrSql = StrSql + "round(MALOUT.MI8,28)  AS MATIN8, "
                StrSql = StrSql + "round(MALOUT.MI9,28)  AS MATIN9, "

                StrSql = StrSql + "round(MALOUT.MC1,28)  AS MATCM1, "
                StrSql = StrSql + "round(MALOUT.MC2,28)  AS MATCM2, "
                StrSql = StrSql + "round(MALOUT.MC3,28)  AS MATCM3, "
                StrSql = StrSql + "round(MALOUT.MC4,28)  AS MATCM4, "
                StrSql = StrSql + "round(MALOUT.MC5,28)  AS MATCM5, "
                StrSql = StrSql + "round(MALOUT.MC6,28)  AS MATCM6, "
                StrSql = StrSql + "round(MALOUT.MC7,28)  AS MATCM7, "
                StrSql = StrSql + "round(MALOUT.MC8,28)  AS MATCM8, "
                StrSql = StrSql + "round(MALOUT.MC9,28)  AS MATCM9, "

                StrSql = StrSql + "round(MALOUT.ML1,28)  AS MATLF1, "
                StrSql = StrSql + "round(MALOUT.ML2,28)  AS MATLF2, "
                StrSql = StrSql + "round(MALOUT.ML3,28)  AS MATLF3, "
                StrSql = StrSql + "round(MALOUT.ML4,28)  AS MATLF4, "
                StrSql = StrSql + "round(MALOUT.ML5,28)  AS MATLF5, "
                StrSql = StrSql + "round(MALOUT.ML6,28)  AS MATLF6, "
                StrSql = StrSql + "round(MALOUT.ML7,28)  AS MATLF7, "
                StrSql = StrSql + "round(MALOUT.ML8,28)  AS MATLF8, "
                StrSql = StrSql + "round(MALOUT.ML9,28)  AS MATLF9, "

                StrSql = StrSql + "RSPL.FINVOLMSI,"
                StrSql = StrSql + "RSPL.FINVOLMUNITS,"
                StrSql = StrSql + "RSPL.VOLUME, "

                StrSql = StrSql + "CUSSALESVOLUME CUSSALESVOLUME, "
                StrSql = StrSql + "CUSSALESUNIT, "
                StrSql = StrSql + "NVL((CASE  WHEN FINVOLMSI > 1 THEN "
                StrSql = StrSql + "(FINVOLMSI)"
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "(FINVOLMUNITS) "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END),0)SUNITVAL "
                StrSql = StrSql + "FROM MATERIALBALANCE MATB "
                StrSql = StrSql + "INNER JOIN RESULTSPL RSPL "
                StrSql = StrSql + "ON RSPL.CASEID = MATB.CASEID "
                StrSql = StrSql + "Inner Join Total "
                StrSql = StrSql + "on Total.caseId=MATB.CASEID "
                StrSql = StrSql + "INNER JOIN MATENDOFLIFEOUT MALOUT "
                StrSql = StrSql + "ON MALOUT.CASEID = MATB.CASEID "
                StrSql = StrSql + "WHERE MATB.CASEID IN (" + CaseId1 + "," + CaseId2 + ") "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetChartEOLRes:" + ex.Message.ToString())
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
                StrSql = StrSql + "SELECT 'Purchased Materials' AS TEXT, "
                StrSql = StrSql + "'PURMATERIALERGY' AS VALUE, "
                StrSql = StrSql + "8 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Transportation' AS TEXT, "
                StrSql = StrSql + "'TRNSPTERGY' AS VALUE, "
                StrSql = StrSql + "9 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Total Energy' AS TEXT, "
                StrSql = StrSql + "'TOTALENERGY' AS VALUE, "
                StrSql = StrSql + "10 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "ORDER BY SEQ "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetChartErgy:" + ex.Message.ToString())
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
                StrSql = StrSql + "SELECT 'Purchased Materials' AS TEXT, "
                StrSql = StrSql + "'PURMATERIALGRNHUSGAS' AS VALUE, "
                StrSql = StrSql + "8 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Transportation' AS TEXT, "
                StrSql = StrSql + "'TRNSPTGRNHUSGAS' AS VALUE, "
                StrSql = StrSql + "9 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Total Greenhouse Gas' AS TEXT, "
                StrSql = StrSql + "'TOTALENGRNHUSGAS' AS VALUE, "
                StrSql = StrSql + "10 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "ORDER BY SEQ "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetChartGhg:" + ex.Message.ToString())
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
                StrSql = StrSql + "SELECT 'Purchased Materials' AS TEXT, "
                StrSql = StrSql + "'PURMATERIALWATER' AS VALUE, "
                StrSql = StrSql + "8 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Transportation' AS TEXT, "
                StrSql = StrSql + "'TRNSPTWATER' AS VALUE, "
                StrSql = StrSql + "9 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Total Water' AS TEXT, "
                StrSql = StrSql + "'TOTALENWATER' AS VALUE, "
                StrSql = StrSql + "10 AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "ORDER BY SEQ "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetChartGhg:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetChartEOL() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT 'Product-Raw Materials' AS TEXT,  "
                StrSql = StrSql + "'1' AS VALUE "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Product-Production Waste' AS TEXT, "
                StrSql = StrSql + "'2' AS VALUE "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Product-Finished Product' AS TEXT, "
                StrSql = StrSql + "'3' AS VALUE "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Raw Material Packaging-Total Flow' AS TEXT, "
                StrSql = StrSql + "'4' AS VALUE "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Raw Material Packaging-Waste' AS TEXT, "
                StrSql = StrSql + "'5' AS VALUE "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Product Packaging-Total Flow' AS TEXT, "
                StrSql = StrSql + "'6' AS VALUE "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Product Packaging-Waste' AS TEXT, "
                StrSql = StrSql + "'7' AS VALUE "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Other Waste-Plant' AS TEXT, "
                StrSql = StrSql + "'8' AS VALUE "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Other Waste-Office' AS TEXT, "
                StrSql = StrSql + "'9' AS VALUE "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "ORDER BY VALUE "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetChartEOL:" + ex.Message.ToString())
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
                Throw New Exception("SMed1GetData:GetChartPref:" + ex.Message.ToString())
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
                StrSql = StrSql + "AND SERVICEID IS NULL "
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetPCaseDetailsByLicense:" + ex.Message.ToString())
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
                StrSql = StrSql + "AND SERVICEID IS NULL "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetCasesByLicense:" + ex.Message.ToString())
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
                StrSql = StrSql + "CAST(PC.CASEDE3 AS VARCHAR(4000))CASEDE3, "
                StrSql = StrSql + "(PC.CASEID||'. PACKAGE FORMAT= '||PC.CASEDE1||' UNIQUE FEATURES= '||PC.CASEDE2)CASEDES, "

                StrSql = StrSql + " PC.CREATIONDATE, "
                StrSql = StrSql + "CASE WHEN PC.CREATIONDATE-PC.SERVERDATE =0 THEN 'NA' ELSE to_char(PC.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE "


                StrSql = StrSql + "FROM MED1.GROUPS "
                StrSql = StrSql + "INNER JOIN MED1.GROUPCASES "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
                StrSql = StrSql + "AND GROUPS.USERID=" + USERID + " "
                StrSql = StrSql + "RIGHT OUTER JOIN PERMISSIONSCASES PC "
                StrSql = StrSql + "ON PC.CASEID=GROUPCASES.CASEID "
                StrSql = StrSql + "WHERE PC.USERID ='" + UserID.ToString() + "' "
                StrSql = StrSql + "AND PC.SERVICEID IS NULL "
                StrSql = StrSql + "ORDER BY CASEID DESC "
                StrSql = StrSql + " ) "

                StrSql = StrSql + "WHERE UPPER(CASEDE1) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE2) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEID) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE3) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(GROUPNAME) LIKE '%" + keyWord.ToUpper().Trim() + "%' "

                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetPCaseGrpDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetGroupDetails(ByVal UserID As String, ByVal flag As Char) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New Med1GetData.Selectdata()
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
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
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
                strSQL = strSQL + "AND SERVICEID IS NULL "

                DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                If DtRes.Tables(0).Rows.Count > 0 Then
                    For j = 0 To DtRes.Tables(0).Rows.Count - 1
                        'Getting CaseID
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "CASEID "
                        strSQL = strSQL + "FROM MED1.GROUPCASES "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString()
                        strSQL = strSQL + " ORDER BY  SEQ"
                        Dts = odbUtil.FillDataSet(strSQL, MedEcon1Connection)
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
                    strSqlOutPut = " SELECT * FROM ( " + strSqlOutPut + " ) DUAL ORDER BY UPPER(GROUPNAME),UPPER(GROUPDES)"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, MedEcon1Connection)
                Else
                    strSqlOutPut = " SELECT * FROM GROUPCASES WHERE GROUPID=0 "
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, MedEcon1Connection)
                End If

                Return dtOutPut
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function
        Public Function GetPerBaseCases1(ByVal USERID As String) As DataTable
            Dim Dts As New DataTable
            Try
                Dim odbUtil As New DBUtil()

                Dim StrSql As String = "SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE FROM PERMISSIONSCASES WHERE USERID='" + USERID.ToString() + "'"
                StrSql = StrSql + "UNION SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE  FROM BASECASES ORDER BY caseDE1"

                Dts = odbUtil.FillDataTable(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetPerBaseCases1:" + ex.Message.ToString())
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
                StrSql = StrSql + "DES1 || '   '|| DES2 AS GDES, "
                StrSql = StrSql + "CREATIONDATE, "
                StrSql = StrSql + "UPDATEDATE "
                StrSql = StrSql + "FROM GROUPS WHERE GROUPID= " + grpID + " "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetGroupDetailsByID:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetGroupIDByUSer(ByVal UserID As String) As DataSet
            Dim Dts As New DataSet()
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
                Dim StrSql As String = ""
                StrSql = "SELECT GROUPID,  "
                StrSql = StrSql + "DES1, "
                StrSql = StrSql + "DES2, "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "CREATIONDATE, "
                StrSql = StrSql + "UPDATEDATE "
                StrSql = StrSql + "FROM GROUPS WHERE USERID= " + UserID + " "
                StrSql = StrSql + "AND SERVICEID IS NULL "
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetGroupIDByUSer:" + ex.Message.ToString())
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
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
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
                            Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
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
                    Dts = odbUtil.FillDataSet(strQuery, MyConnectionString)
                Else
                    StrSql = " SELECT * FROM GROUPCASES WHERE GROUPID=0 "
                    Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                End If

                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:ValiDateGroupcases:" + ex.Message.ToString())
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
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetGroupCasesByUSer:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetGroupCaseDetails(ByVal UserID As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New Med1GetData.Selectdata()
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
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
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
                    DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                Else
                    strSQL = "select * FROM GROUPS WHERE GROUPID=0"
                    DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                End If
                Return DtRes
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function
        Public Function GetEditCases1(ByVal USERID As String, ByVal grpID As String, ByVal flag As String) As DataTable
            Dim Dts As New DataTable()
            Dim i As New Integer
            Try
                Dim odbUtil As New DBUtil()
                Dim StrSql As String = ""
                StrSql = "SELECT CASEID,CASEDE1,CASEDE FROM  "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT CASEID, "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE USERID='" + USERID.ToString() + "' "
                If flag <> "true" Then
                    StrSql = StrSql + "AND  GROUPID=" + grpID.ToString() + " "
                End If
                StrSql = StrSql + ") "
                StrSql = StrSql + "Where CASEID "
                If flag = "true" Then
                    StrSql = StrSql + "NOT IN "
                Else
                    StrSql = StrSql + "IN "
                End If
                StrSql = StrSql + "(  "
                StrSql = StrSql + "SELECT CASEID "
                StrSql = StrSql + "FROM MED1.GROUPCASES "
                StrSql = StrSql + "WHERE GROUPID=" + grpID + ") "
                StrSql = StrSql + "ORDER BY CASEDE1,CASEID "
                Dts = odbUtil.FillDataTable(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetEditCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetEditCases(ByVal USERID As String, ByVal grpID As String, ByVal flag As String) As DataTable
            Dim Dts As New DataTable()
            Dim i As New Integer
            Try
                Dim odbUtil As New DBUtil()
                Dim StrSql As String = ""
                StrSql = "SELECT CASEID,CASEDE1,CASEDE,SEQ FROM  "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT PERMISSIONSCASES.CASEID, "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "SEQ, "
                StrSql = StrSql + "('CASE:'||PERMISSIONSCASES.CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + " LEFT OUTER JOIN MED1.GROUPCASES ON GROUPCASES.CASEID=PERMISSIONSCASES.CASEID "
                StrSql = StrSql + "WHERE USERID='" + USERID.ToString() + "' "
                If flag <> "true" Then
                    StrSql = StrSql + "AND  GROUPID=" + grpID.ToString() + " "
                End If
                StrSql = StrSql + ") "
                StrSql = StrSql + "Where CASEID "
                If flag = "true" Then
                    StrSql = StrSql + "NOT IN "
                    StrSql = StrSql + "(  "
                    StrSql = StrSql + "SELECT CASEID "
                    StrSql = StrSql + "FROM MED1.GROUPCASES "
                    StrSql = StrSql + "WHERE GROUPID=" + grpID + ") "
                    StrSql = StrSql + "ORDER BY CASEDE1,CASEID "
                Else
                    StrSql = StrSql + "IN "
                    StrSql = StrSql + "(  "
                    StrSql = StrSql + "SELECT CASEID "
                    StrSql = StrSql + "FROM MED1.GROUPCASES "
                    StrSql = StrSql + "WHERE GROUPID=" + grpID + ") "
                    StrSql = StrSql + "ORDER BY SEQ "
                End If


                Dts = odbUtil.FillDataTable(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Return Dts
            End Try
        End Function
        Public Function GetPCaseDetailsByGroup(ByVal USERID As String, ByVal grpId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PERMISSIONSCASES.CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(PERMISSIONSCASES.CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES,SEQ FROM PERMISSIONSCASES "
                StrSql = StrSql + " INNER JOIN MED1.GROUPCASES ON GROUPCASES.CASEID=PERMISSIONSCASES.CASEID "
                StrSql = StrSql + "WHERE USERID ='" + USERID.ToString() + "'  AND GROUPCASES.GROUPID=" + grpId + " "
                StrSql = StrSql + " AND PERMISSIONSCASES.CASEID IN(SELECT CASEID FROM MED1.GROUPCASES WHERE GROUPID =" + grpId + ")"
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID "

                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetPCaseDetailsByGroup:" + ex.Message.ToString())
                Return Dts
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
                StrSql = StrSql + "AND CASEID IN(SELECT CASEID FROM MED1.GROUPCASES WHERE GROUPID=" + groupID + " ) "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID"
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetCases:" + ex.Message.ToString())
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

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetMaxSEQGCASE:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetAllGroupDetails(ByVal UserID As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New Med1GetData.Selectdata()
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
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
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
                strSQL = strSQL + "AND SERVICEID IS NULL "
                DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                If DtRes.Tables(0).Rows.Count > 0 Then
                    For j = 0 To DtRes.Tables(0).Rows.Count - 1
                        'Getting CaseID
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "CASEID "
                        strSQL = strSQL + "FROM MED1.GROUPCASES "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString()
                        strSQL = strSQL + " ORDER BY  SEQ"
                        Dts = odbUtil.FillDataSet(strSQL, MedEcon1Connection)
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
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, MedEcon1Connection)
                Else
                    strSqlOutPut = " SELECT * FROM GROUPCASES WHERE GROUPID=0"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, MedEcon1Connection)
                End If

                Return dtOutPut
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function
#End Region

#Region "Bemis"
        Public Function GetStatusForSister(ByVal Schema As String, ByVal CaseId As String, ByVal USERNAME As String) As DataSet
            Dim Dts As New DataSet()
            Dim StrSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Dim CaseIds As String = String.Empty
            Dim con As String = ""
            Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
            Try
                If Schema = "S1" Then
                    con = MedSustain1Connection
                End If
                StrSql = "SELECT CASEID FROM STATUSUPDATE WHERE "
                StrSql = StrSql + "CASEID = " + CaseId + " AND UPPER(USERNAME)='" + USERNAME.ToUpper() + "' AND UPPER(STATUS)='SISTER CASE' ORDER BY DATED ASC"

                Dts = odbUtil.FillDataSet(StrSql, con)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetStatusDetailsByID:" + ex.Message.ToString())
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
                StrSql = StrSql + "FROM MED1.GROUPS "
                StrSql = StrSql + "INNER JOIN MED1.GROUPCASES "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
                StrSql = StrSql + "AND GROUPS.USERID=" + UserID + " "
                StrSql = StrSql + "RIGHT OUTER JOIN PERMISSIONSCASES PC "
                StrSql = StrSql + "ON PC.CASEID=GROUPCASES.CASEID "
                StrSql = StrSql + "WHERE UPPER(USERNAME) ='" + UserName.ToUpper().Trim() + "' "
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
                StrSql = StrSql + "FROM MED1.GROUPS "
                StrSql = StrSql + "INNER JOIN MED1.GROUPCASES "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
                StrSql = StrSql + "AND GROUPS.USERID=" + UserID + " "
                StrSql = StrSql + "RIGHT OUTER JOIN PERMISSIONSCASES PC "
                StrSql = StrSql + "ON PC.CASEID=GROUPCASES.CASEID "
                StrSql = StrSql + "WHERE UPPER(USERNAME) ='" + UserName.ToUpper().Trim() + "' "
                StrSql = StrSql + "AND PC.CASEID in(" + CaseIds + ") "

                StrSql = StrSql + " ) "

                StrSql = StrSql + "WHERE UPPER(CASEDE1) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE2) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEID) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE3) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(GROUPNAME) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "ORDER BY CASEID DESC "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetPCaseGrpDetails:" + ex.Message.ToString())
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
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetPropCasesById:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetGroupCaseDet(ByVal UserID As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New Med1GetData.Selectdata()
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
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
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
                    DtRes = odbUtil.FillDataSet(strSQL2, MyConnectionString)
                Else
                    strSQL2 = strSQL2 + ""
                    DtRes = odbUtil.FillDataSet(strSQL2, MyConnectionString)
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

                StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS WHERE USERID='" + USERID.ToString() + "') "
                ' StrSql = StrSql + "WHERE UPPER(USERNAME) ='" + UserName.ToUpper().ToString() + "' "

                StrSql = StrSql + "AND PERMISSIONSCASES.STATUSID=3) "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetCases:" + ex.Message.ToString())
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
                StrSql = StrSql + "MS.STATUS,PERMISSIONSCASES.STATUSID ,USERS.USERNAME CaseOwner "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "INNER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                StrSql = StrSql + "INNER JOIN ECON.USERS "
                StrSql = StrSql + "ON USERS.USERID=PERMISSIONSCASES.USERID "
                StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS WHERE USERID='" + USERID.ToString() + "') "

                StrSql = StrSql + "AND PERMISSIONSCASES.STATUSID=3) "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetCasesByLicense:" + ex.Message.ToString())
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


                StrSql = StrSql + "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,MS.STATUS,PERMISSIONSCASES.STATUSID,USERNAME CaseOwner "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                StrSql = StrSql + "WHERE USERID ='" + USERID.ToString() + "' "
                'StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                'StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(PERMISSIONSCASES.STATUSID,0) NOT IN(3,4) "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,MS.STATUS,PERMISSIONSCASES.STATUSID,USERNAME CaseOwner "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                StrSql = StrSql + "WHERE CaseId in(" + CaseIds + ") "
                StrSql = StrSql + "AND USERID ='" + USERID.ToString() + "' "
                StrSql = StrSql + ") "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetCases:" + ex.Message.ToString())
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
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
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
                Throw New Exception("SMed1GetData:GetPropCaseStatus:" + ex.Message.ToString())
                Return CaseIds
            End Try
        End Function
        Public Function GetPropDisAppStatus(ByVal USERNAME As String, ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim CaseIds As String = ""
            Try
                StrSql = StrSql + "SELECT * "
                StrSql = StrSql + "FROM STATUSUPDATE "
                StrSql = StrSql + "WHERE UPPER(USERNAME)='" + USERNAME.ToUpper() + "' AND CASEID=" + CaseId.ToString() + " AND UPPER(STATUS)='APPROVED' "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                If Dts.Tables(0).Rows.Count > 0 Then

                Else
                    CaseIds = "0"
                End If
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetPropDisAppStatus:" + ex.Message.ToString())
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
                StrSql = StrSql + "MS.STATUS,PERMISSIONSCASES.STATUSID,USERS.USERNAME CaseOwner  "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                StrSql = StrSql + "INNER JOIN ECON.USERS "
                StrSql = StrSql + "ON USERS.USERID=PERMISSIONSCASES.USERID "
                StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS WHERE USERID='" + USERID.ToString() + "') "
                'StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                'StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(PERMISSIONSCASES.STATUSID,0) NOT IN(3) "
                'dsUsers = GetUserCompanyUsersBem(UserName)
                'If dsUsers.Tables(0).Rows.Count > 0 Then
                '    For j = 0 To dsUsers.Tables(0).Rows.Count - 1
                '        StrSqlN = "SELECT CASEID "
                '        StrSqlN = StrSqlN + "FROM PERMISSIONSCASES "
                '        StrSqlN = StrSqlN + "WHERE STATUSID=4 AND UPPER(USERNAME)='" + dsUsers.Tables(0).Rows(j).Item("USERNAME").ToString().ToUpper() + "' "
                '        Dts = odbUtil.FillDataSet(StrSqlN, MedSustain1Connection)
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
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetPropCasesByLicense:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetStatusDetailsByID(ByVal CaseId As String, ByVal USERNAME As String) As DataSet
            Dim Dts As New DataSet()
            Dim StrSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Dim CaseIds As String = String.Empty
            Try
                StrSql = "SELECT CASEID,STATUS,DATED,ACTIONBY,COMMENTS FROM STATUSUPDATE WHERE "
                StrSql = StrSql + "CASEID = " + CaseId + " AND UPPER(USERNAME)='" + USERNAME.ToUpper() + "' ORDER BY DATED ASC "

                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetStatusDetailsByID:" + ex.Message.ToString())
            End Try
        End Function
        Public Function GetGrpStatusDetailsByID(ByVal CaseId As String, ByVal USERNAME As String) As DataSet
            Dim Dts As New DataSet()
            Dim StrSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Dim CaseIds As String = String.Empty
            Try
                StrSql = "SELECT CASEID,STATUS,DATED,ACTIONBY,COMMENTS FROM STATUSUPDATE WHERE "
                StrSql = StrSql + "CASEID = " + CaseId + " AND UPPER(USERNAME)='" + USERNAME.ToUpper() + "' "

                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetStatusDetailsByID:" + ex.Message.ToString())
            End Try
        End Function
        Public Function GetPermissionStatus(ByVal CaseId As Integer, ByVal USERNAME As String) As DataSet
            Try
                Dim Dts As New DataSet()
                Dim odbUtil As New DBUtil()
                Dim StrSql As String = ""
                StrSql = ""
                StrSql = "SELECT CASEID,STATUS,DATED,ACTIONBY,COMMENTS FROM STATUSUPDATE "
                StrSql = StrSql + "WHERE CASEID=" + CaseId.ToString() + " AND UPPER(ACTIONBY)='" + USERNAME.ToUpper() + "' "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("S1Data:GetPermissionStatus:" + ex.Message.ToString())
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
                StrSql = StrSql + "AND CASEID IN(SELECT CASEID FROM MED1.GROUPCASES WHERE GROUPID=" + groupID + " )) "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetCases:" + ex.Message.ToString())
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
                StrSql = StrSql + "AND SERVICES.SERVICEDE IN ('MedE1','MedE2','MedS1','MedS2') "
                StrSql = StrSql + "AND UPPER(USERPERMISSIONS.USERROLE)='READWRITE' "
                StrSql = StrSql + "GROUP BY USERS.USERID,USERS.USERNAME) "
                StrSql = StrSql + "ORDER BY USERNAME "

                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetUserCompanyUsersBem:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Barrier Grade"
        Public Function GetGradesVal(ByVal GRADEID As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty

            Try
                StrSql = "SELECT GRADE.GRADEID,GRADE.GRADENAME FROM GRADE WHERE GRADEID =" + GRADEID.ToString()
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetGradesVal:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetGrades(ByVal MatId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty

            Try
                If MatId = "0" Then
                    StrSql = "SELECT GRADE.GRADEID,GRADE.GRADENAME,0 SG,0 WEIGHT FROM GRADE JOIN MATGRADE ON GRADE.GRADEID=MATGRADE.GRADEID AND MATID =" + MatId.ToString()
                Else
                    StrSql = "SELECT  0 GRADEID, 'Nothing Selected' GRADENAME,0 SG,0 WEIGHT FROM DUAL UNION ALL SELECT GRADE.GRADEID,GRADE.GRADENAME,MAT.SG,GRADE.WEIGHT FROM GRADE "
                    StrSql = StrSql + "INNER JOIN MATGRADE ON GRADE.GRADEID=MATGRADE.GRADEID AND MATID =" + MatId.ToString()
                    StrSql = StrSql + "INNER JOIN MATERIALS MAT  "
                    StrSql = StrSql + "ON MAT.MATID=MATGRADE.MATID "

                End If

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetGrades:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetMatGrades(ByVal MatId As Integer, ByVal GradeId As Integer) As Integer
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty

            Try
                StrSql = "SELECT GRADE.GRADEID,GRADE.GRADENAME,MAT.SG,GRADE.WEIGHT FROM GRADE "
                StrSql = StrSql + "INNER JOIN MATGRADE ON GRADE.GRADEID=MATGRADE.GRADEID AND MATGRADE.MATID =" + MatId.ToString() + " AND MATGRADE.GRADEID=" + GradeId.ToString() + " "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT  "
                StrSql = StrSql + "ON MAT.MATID=MATGRADE.MATID "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                If Dts.Tables(0).Rows.Count > 0 Then
                    Return GradeId
                Else
                    StrSql = "SELECT GRADE.GRADEID FROM MATGRADE  "
                    StrSql = StrSql + "INNER JOIN GRADE "
                    StrSql = StrSql + "ON GRADE.GRADEID=MATGRADE.GRADEID "
                    StrSql = StrSql + "WHERE MATGRADE.MATID= " + MatId.ToString() + " "
                    StrSql = StrSql + "AND GRADE.ISDEFAULT='Y' "
                    Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                    If Dts.Tables(0).Rows.Count > 0 Then
                        Return CInt(Dts.Tables(0).Rows(0).Item("GRADEID"))
                    End If
                End If

            Catch ex As Exception
            End Try
        End Function
        Public Function GetPrefDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID,  "
                StrSql = StrSql + "UNITS, "
                StrSql = StrSql + "CURRENCY, "
                StrSql = StrSql + "OCOUNTRY, "
                StrSql = StrSql + "DCOUNTRY, "
                StrSql = StrSql + "EFFDATE, "
                StrSql = StrSql + "TO_CHAR(EFFDATE,'MON DD,YYYY')AS EDATE, "
                StrSql = StrSql + "ERGYCALC, "
                StrSql = StrSql + "ISDSCTNEW "
                StrSql = StrSql + "FROM PREFERENCES "
                StrSql = StrSql + "WHERE CASEID = " + CaseId.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetPrefDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetMinMaxBarrierTemp() As DataSet
            Dim StrSql1 As String
            Dim Ds As DataSet
            Dim odbUtil As New DBUtil()
            Try
                StrSql1 = "select max(TEMPVAL) MAXVAL,min(TEMPVAL) MINVAL from BARRIERTEMP"
                Ds = odbUtil.FillDataSet(StrSql1, EconConnection)
                Return Ds
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetMinMaxBarrierTemp:" + ex.Message.ToString())
                Return Ds
            End Try
        End Function
        Public Function GetMinMaxBarrierHumidity() As DataSet
            Dim StrSql1 As String
            Dim Ds As DataSet
            Dim odbUtil As New DBUtil() '
            Try
                StrSql1 = "select max(RHVALUE) MAXVAL,min(RHVALUE) MINVAL from BARRIERH"
                Ds = odbUtil.FillDataSet(StrSql1, EconConnection)
                Return Ds
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetMinMaxBarrierHumidity:" + ex.Message.ToString())
                Return Ds
            End Try
        End Function
#End Region

#Region "Comp Module"
        Public Function GetCompUserDetails(ByVal Id As String) As DataSet
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
                StrSql = StrSql + "SERVICES.SERVICEID, "
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
                StrSql = StrSql + "AND SERVICES.SERVICEDE='CompSustain' "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetCompUserDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetBCaseCompDetails() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  CASEID,CASEDE1, CASEDE2,SERVICEUSER.USERID SERVICEUSERID, "
                StrSql = StrSql + "CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "INNER JOIN ECON.USERS ON USERS.USERID = PERMISSIONSCASES.USERID "
                StrSql = StrSql + "INNER JOIN ECON.SERVICEUSER ON SERVICEUSER.USERID= USERS.USERID "
                StrSql = StrSql + "INNER JOIN ECON.SERVICES ON SERVICES.SERVICEID= SERVICEUSER.SERVICEID "
                StrSql = StrSql + "WHERE SERVICES.SERVICEDE='CompSustain' "
                StrSql = StrSql + "AND PERMISSIONSCASES.SERVICEID IS NULL "
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID"
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetBCaseCompDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetServiceCompUser(ByVal UserId As String) As DataSet
            Dim Dts As DataSet
            Dim StrSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Try
                StrSql = "SELECT SERVICEUSER.SERVICEID ,SERVICES.SERVICEDE "
                StrSql = StrSql + "FROM ECON.SERVICEUSER INNER JOIN ECON.SERVICES ON SERVICES.SERVICEID=SERVICEUSER.SERVICEID "
                StrSql = StrSql + "WHERE USERID=" + UserId + " "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts

            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
        Public Function GetPCaseCompDetails(ByVal USERID As String, ByVal ServiceId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE USERID ='" + USERID.ToUpper().ToString() + "' "
                StrSql = StrSql + "AND SERVICEID =" + ServiceId + " "
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID "

                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetPCaseCompDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompPCaseDetailsByLicense(ByVal USERID As String, ByVal ServiceId As String) As DataSet
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
                StrSql = StrSql + "ON USERS.USERID=PERMISSIONSCASES.USERID) "
                StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS USERS WHERE USERID='" + USERID.ToString() + "') "
                StrSql = StrSql + "AND SERVICEID =" + ServiceId + " "
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetCompPCaseDetailsByLicense:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompGroupCaseDetails(ByVal UserID As String, ByVal ServiceId As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New Med1GetData.Selectdata()
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
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
                Dts = objGetData.GetCompGroupIDByUSer(UserID, ServiceId)
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
                    DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                Else
                    strSQL = "select * FROM GROUPS WHERE GROUPID=0"
                    DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                End If
                Return DtRes
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function
        Public Function GetCompUserCompanyUsers(ByVal UserName As String, ByVal ServiceId As String) As DataSet
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
                StrSql = StrSql + "AND SERVICES.SERVICEID= " + ServiceId.ToString() + " "
                StrSql = StrSql + "AND UPPER(USERPERMISSIONS.USERROLE)='READWRITE' "
                StrSql = StrSql + "GROUP BY USERS.USERID,USERS.USERNAME "
                StrSql = StrSql + "ORDER BY USERS.USERNAME "

                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetCompUserCompanyUsers:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompCaseGrpDetails(ByVal UserName As String, ByVal UserID As String, ByVal keyWord As String, ByVal ServiceId As String) As DataSet
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
                StrSql = StrSql + "CAST(PC.CASEDE3 AS VARCHAR(4000))CASEDE3, "
                StrSql = StrSql + "(PC.CASEID||'. PACKAGE FORMAT= '||PC.CASEDE1||' UNIQUE FEATURES= '||PC.CASEDE2)CASEDES, "

                StrSql = StrSql + " PC.CREATIONDATE, "
                StrSql = StrSql + "CASE WHEN PC.CREATIONDATE-PC.SERVERDATE =0 THEN 'NA' ELSE to_char(PC.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE "


                StrSql = StrSql + "FROM MED1.GROUPS "
                StrSql = StrSql + "INNER JOIN MED1.GROUPCASES "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
                StrSql = StrSql + "AND GROUPS.USERID=" + UserID + " "
                StrSql = StrSql + "RIGHT OUTER JOIN PERMISSIONSCASES PC "
                StrSql = StrSql + "ON PC.CASEID=GROUPCASES.CASEID "
                StrSql = StrSql + "WHERE UserID ='" + UserID.ToString() + "' "
                StrSql = StrSql + "AND PC.SERVICEID = " + ServiceId + " "
                StrSql = StrSql + "ORDER BY CASEID DESC "
                StrSql = StrSql + " ) "

                StrSql = StrSql + "WHERE UPPER(CASEDE1) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE2) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEID) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE3) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(GROUPNAME) LIKE '%" + keyWord.ToUpper().Trim() + "%' "

                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetPCaseGrpDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompCases(ByVal USERID As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String, ByVal ServiceId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE USERID ='" + USERID.ToString() + "' "
                StrSql = StrSql + "AND SERVICEID=" + ServiceId + " "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetCompCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompCasesByLicense(ByVal USERID As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String, ByVal ServiceId As String) As DataSet
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
                StrSql = StrSql + "AND SERVICEID=" + ServiceId + " "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetCompCasesByLicense:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompBCases(ByVal CaseDe1 As String, ByVal CaseDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  CASEID,(CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "INNER JOIN ECON.USERS ON USERS.USERID = PERMISSIONSCASES.USERID "
                StrSql = StrSql + "INNER JOIN ECON.SERVICEUSER ON SERVICEUSER.USERID= USERS.USERID "
                StrSql = StrSql + "INNER JOIN ECON.SERVICES ON SERVICES.SERVICEID= SERVICEUSER.SERVICEID "
                StrSql = StrSql + "WHERE SERVICES.SERVICEDE='CompSustain' "
                StrSql = StrSql + "AND PERMISSIONSCASES.SERVICEID IS NULL "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetCompBCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetAllCompGroupDetails(ByVal UserID As String, ByVal ServiceId As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New Med1GetData.Selectdata()
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
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
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
                strSQL = strSQL + "AND SERVICEID IN (" + ServiceId + ") "

                DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                If DtRes.Tables(0).Rows.Count > 0 Then
                    For j = 0 To DtRes.Tables(0).Rows.Count - 1
                        'Getting CaseID
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "CASEID "
                        strSQL = strSQL + "FROM MED1.GROUPCASES "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString()
                        strSQL = strSQL + " ORDER BY  SEQ"
                        Dts = odbUtil.FillDataSet(strSQL, MedEcon1Connection)
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
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, MedEcon1Connection)
                Else
                    strSqlOutPut = " SELECT * FROM GROUPCASES WHERE GROUPID=0"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, MedEcon1Connection)
                End If

                Return dtOutPut
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function
        Public Function GetCompGroupDetails(ByVal UserID As String, ByVal flag As Char, ByVal ServiceId As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New Med1GetData.Selectdata()
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
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
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
                strSQL = strSQL + "AND SERVICEID IN (" + ServiceId + ") "

                DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                If DtRes.Tables(0).Rows.Count > 0 Then
                    For j = 0 To DtRes.Tables(0).Rows.Count - 1
                        'Getting CaseID
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "CASEID "
                        strSQL = strSQL + "FROM MED1.GROUPCASES "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString()
                        strSQL = strSQL + " ORDER BY  SEQ"
                        Dts = odbUtil.FillDataSet(strSQL, MedEcon1Connection)
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
                    strSqlOutPut = " SELECT * FROM ( " + strSqlOutPut + " ) DUAL ORDER BY UPPER(GROUPNAME),UPPER(GROUPDES)"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, MedEcon1Connection)
                Else
                    strSqlOutPut = " SELECT * FROM GROUPCASES WHERE GROUPID=0 "
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, MedEcon1Connection)
                End If

                Return dtOutPut
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function
        Public Function GetCompTotalCaseCount(ByVal USERID As String, ByVal ServiceId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT COUNT(*) AS TOTALCOUNT FROM PERMISSIONSCASES  "
                StrSql = StrSql + "WHERE USERID='" + USERID.ToString() + "' "
                StrSql = StrSql + "AND SERVICEID=" + ServiceId + " "
                Dts = odbUtil.FillDataSet(StrSql, MedSustain1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SMed1GetData:GetCompTotalCaseCount:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

    End Class

End Class
