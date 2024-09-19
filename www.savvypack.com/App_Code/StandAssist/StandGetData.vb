Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System

Public Class StandGetData
    Public Class Selectdata
        Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
        Dim SBAConnection As String = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
		Dim odbutil As New DBUtil()
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
                StrSql = StrSql + "USERS.ISCOMPLIBRARY,"
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
                StrSql = StrSql + "AND SERVICES.SERVICEDE='StandAssist' "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetUserDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetTotalCaseCount(ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT COUNT(*) AS TOTALCOUNT FROM PERMISSIONSCASES  "
                StrSql = StrSql + "WHERE USERID=" + UserId.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetTotalCaseCount:" + ex.Message.ToString())
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
                Throw New Exception("StandGetData:GetSelectedUserDetails:" + ex.Message.ToString())
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
                ' StrSql = "SELECT CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES FROM BASECASES "
                ' StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID "
                StrSql = "SELECT CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(CASEID||'. Primary Descriptor= '||CASEDE1||' Secondary Descriptor= '||CASEDE2)CASEDES FROM BASECASES "
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetBCaseDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPCaseDetails(ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE USERID =" + UserId.ToString() + " "
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetPCaseDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUserCompanyUsers(ByVal UserName As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERS.USERID,  "
                StrSql = StrSql + "USERS.USERNAME "
                StrSql = StrSql + "FROM USERS "
                StrSql = StrSql + "INNER JOIN USERPERMISSIONS "
                StrSql = StrSql + "ON USERPERMISSIONS.USERID= USERS.USERID "
                StrSql = StrSql + "INNER JOIN SERVICES "
                StrSql = StrSql + "ON SERVICES.SERVICEID = USERPERMISSIONS.SERVICEID "
                StrSql = StrSql + "WHERE USERS.LICENSEID=(SELECT LICENSEID FROM USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper.ToString() + "') "
                StrSql = StrSql + "AND SERVICES.SERVICEDE IN ('StandAssist') "
                StrSql = StrSql + "GROUP BY USERS.USERID,USERS.USERNAME "
                StrSql = StrSql + "ORDER BY USERS.USERNAME "

                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetUserCompanyUsers:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCaseDetails(ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID,CASEDE1,(CASEDE1||' ' ||CASEDE2)CASEDES,CASEDE3,CASEDE2,CASETYPE,SERVERDATE,APPLICATION  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT  CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,'Public' CASETYPE,SERVERDATE,APPLICATION FROM BASECASES "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT  CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,'Public' CASETYPE,SERVERDATE,APPLICATION FROM COMPANYCASES "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT DISTINCT CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,'Proprietary' CASETYPE,SERVERDATE,APPLICATION FROM PERMISSIONSCASES "
                StrSql = StrSql + ") "
                StrSql = StrSql + "WHERE CASEID =" + CaseId.ToString() + " "
                StrSql = StrSql + "ORDER BY CASEID "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetCaseDetails:" + ex.Message.ToString())
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
                StrSql = StrSql + "WHERE USERID =" + UserId.ToString() + " "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetCases:" + ex.Message.ToString())
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
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetCases:" + ex.Message.ToString())
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
                StrSql = StrSql + "ISDSCTNEW, "
                StrSql = StrSql + "DFLAG,CONVPA,TITLE21 "
                StrSql = StrSql + "FROM PREFERENCES "
                StrSql = StrSql + "WHERE CASEID = " + CaseId.ToString() + " "


                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetPref:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        'Public Function GetMaterials(ByVal MatId As Integer, ByVal MatDe1 As String, ByVal MatDe2 As String) As DataSet
        '    Dim Dts As New DataSet()
        '   Dim odbUtil As New DBUtil()
        '    Dim StrSql As String = String.Empty
        '    Try
        '        StrSql = "SELECT MATERIALS.MATID, (MATDE1||'  '||MATDE2)MATDES,MATDE1,MATDE2,GRADE.GRADENAME,GRADE.GRADEID,GRADE.WEIGHT,MATERIALS.SG  "
        '        StrSql = StrSql + "FROM MATERIALS "
        '        StrSql = StrSql + "INNER JOIN MATGRADE MG ON MG.MATID=MATERIALS.MATID  "
        '        StrSql = StrSql + "INNER JOIN GRADE ON GRADE.GRADEID=MG.GRADEID AND GRADE.ISDEFAULT='Y' "
        '        StrSql = StrSql + "WHERE MATERIALS.MATID = CASE WHEN " + MatId.ToString() + " = -1 THEN "
        '        StrSql = StrSql + "MATERIALS.MATID "
        '        StrSql = StrSql + "ELSE "
        '        StrSql = StrSql + "" + MatId.ToString() + " "
        '        StrSql = StrSql + "END "
        '        StrSql = StrSql + "AND NVL(UPPER(MATDE1),'#') LIKE '" + MatDe1.ToUpper() + "%' "
        '        StrSql = StrSql + "AND NVL(UPPER(MATDE2),'#') LIKE '" + MatDe2.ToUpper() + "%' "
        '        StrSql = StrSql + "ORDER BY  MATDE1"
        '        Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
        '        Return Dts
        '    Catch ex As Exception
        '        Throw New Exception("StandGetData:GetExtrusionDetails:" + ex.Message.ToString())
        '        Return Dts
        '    End Try
        ' End Function

 Public Function GetCategory(ByVal MatId As Integer, ByVal MatDe1 As String, ByVal MatDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = " SELECT MATID,CATEGORY,MATERIAL FROM "
                StrSql = StrSql + "(SELECT MATID,CATEGORY,''MATERIAL FROM ADHESIVEPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,''MATERIAL FROM ALUMINUMPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,''MATERIAL FROM BASEFIBERPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,''MATERIAL FROM COATINGSPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,''MATERIAL FROM CONCENTRATEPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,''MATERIAL FROM FILMPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,''MATERIAL FROM GLASSPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,''MATERIAL FROM INKPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,''MATERIAL FROM NONWOVENSPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,''MATERIAL FROM PAPERBOARDPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,''MATERIAL FROM PAPERPROP "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT MATID,CATEGORY,MATERIAL FROM RESINPROP  "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,''MATERIAL FROM SHEETPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,''MATERIAL FROM STEELPROP )  "
                StrSql = StrSql + "WHERE MATID=" + MatId.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function


        Public Function GetConversionFactor() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT "
                StrSql = StrSql + "MICPMIL, KGPLB, M2PMSI, M2PSQFT, MPFT, KMPMILE, JPMJ, LITPGAL, IN2PSQFT,CCMPCFT,GMPLB,MM2PIN2,TPKN,GMPOZ,MPAPLBIN2 "
                StrSql = StrSql + "FROM CONVFACTORS "
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetConversionFactor:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPCaseDetailsByLicense(ByVal UserId As String) As DataSet
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
                StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS USERS WHERE USERID=" + UserId.ToString() + ") "
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetPCaseDetailsByLicense:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCasesByLicense(ByVal UserId As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String) As DataSet
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
                StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS USERS WHERE USERID=" + UserId.ToString() + ") "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetCasesByLicense:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

#End Region

#Region "Assumptions Pages"

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
                StrSql = StrSql + "LEFT OUTER JOIN PRODUCTFORMAT "
                StrSql = StrSql + "ON PRODUCTFORMAT.FORMATID = PRODUCTFORMATIN.M1 "
                StrSql = StrSql + "AND PREFERENCES.UNITS = 0 "
                StrSql = StrSql + "LEFT OUTER JOIN PRODUCTFORMAT2 "
                StrSql = StrSql + "ON PRODUCTFORMAT2.FORMATID = PRODUCTFORMATIN.M1 "
                StrSql = StrSql + "AND PREFERENCES.UNITS = 1 "
                StrSql = StrSql + "WHERE PRODUCTFORMATIN.CASEID  = " + CaseId.ToString() + " ORDER BY PRODUCTFORMATIN.CASEID "
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetProductFromatIn:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetExistingMaterial(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT M1,M2,M3,M4,M5,M6,M7,M8,M9,M10,SG1,SG2,SG3,SG4,SG5,SG6,SG7,SG8,SG9,SG10, "
                StrSql = StrSql + "T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,OTR1,OTR2,OTR3,OTR4,OTR5,OTR6,OTR7,OTR8,OTR9,OTR10, "
                StrSql = StrSql + "WVTR1,WVTR2,WVTR3,WVTR4,WVTR5,WVTR6,WVTR7,WVTR8,WVTR9,WVTR10 "
                StrSql = StrSql + "FROM MATERIALINPUT "
                StrSql = StrSql + "INNER JOIN BARRIERINPUT ON BARRIERINPUT.CASEID=MATERIALINPUT.CASEID "
                StrSql = StrSql + "WHERE MATERIALINPUT.CASEID = " + CaseId.ToString() + ""
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetProductFromatIn:" + ex.Message.ToString())
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
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
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

#Region "Notes"
        Public Function GetAssumptionPageDetails(ByVal AssumptionType As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT ASSUMPTIONTYPEID, ASSUMPTIONTYPEDE1, ASSUMPTIONTYPEDE2, ASSUMPTIONTYPECODE  "
                StrSql = StrSql + "FROM ASSUMPTIONTYPES "
                StrSql = StrSql + "WHERE ASSUMPTIONTYPECODE ='" + AssumptionType + "'"
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetAssumptionPageDetails:" + ex.Message.ToString())
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

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetPageNoteDetails:" + ex.Message.ToString())
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


                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetCaseNoteDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

#End Region

#Region "Case Grouping:"

        

        Public Function GetGroupDetails(ByVal UserID As String, ByVal flag As Char, ByVal Type As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New StandGetData.Selectdata()
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
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
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
                strSQL = strSQL + "AND GROUPTYPE= '" + Type + "' "
                DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                If DtRes.Tables(0).Rows.Count > 0 Then
                    For j = 0 To DtRes.Tables(0).Rows.Count - 1
                        'Getting CaseID
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "CASEID "
                        strSQL = strSQL + "FROM GROUPCASES "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString()
                        strSQL = strSQL + " ORDER BY  SEQ"
                        Dts = odbUtil.FillDataSet(strSQL, SBAConnection)
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
                        strSQL = strSQL + "DES2 GROUPDES,DES3,APPLICATION, "
                        strSQL = strSQL + "GROUPID || ':'|| DES1 CDES1,'' FILENAME,'' NAME, "
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
                        strSQL = strSQL + "'NA'  DES3, "
                        strSQL = strSQL + "'NA'  APPLICATION, "
                        strSQL = strSQL + "'NA'  FILENAME, "
                        strSQL = strSQL + "'NA'  NAME, "
                        strSQL = strSQL + "'0:None'  CDES1, "
                        strSQL = strSQL + "'NA'  CREATIONDATE, "
                        strSQL = strSQL + "'NA'  UPDATEDATE "
                        strSQL = strSQL + "FROM "
                        strSQL = strSQL + "DUAL "

                        strSqlOutPut = strSqlOutPut + " UNION ALL " + strSQL
                    End If
                    strSqlOutPut = " SELECT * FROM ( " + strSqlOutPut + " ) DUAL ORDER BY GROUPNAME,GROUPDES"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, SBAConnection)
                Else
                    strSqlOutPut = " SELECT * FROM GROUPCASES WHERE GROUPID=0 "
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, SBAConnection)
                End If

                Return dtOutPut
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function

        Public Function GetGroupCaseDetails(ByVal UserID As String, ByVal Type As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New StandGetData.Selectdata()
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
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
                Dts = objGetData.GetGroupIDByUSer(UserID, Type)
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

        Public Function GetGroupIDByUSer(ByVal UserID As String, ByVal Type As String) As DataSet
            Dim Dts As New DataSet()
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
                Dim StrSql As String = ""
                StrSql = "SELECT GROUPID,  "
                StrSql = StrSql + "DES1, "
                StrSql = StrSql + "DES2, "
                StrSql = StrSql + "DES3, "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "CREATIONDATE, "
                StrSql = StrSql + "UPDATEDATE, "
                StrSql = StrSql + "APPLICATION, "
                StrSql = StrSql + "SUP.NAME "
                StrSql = StrSql + "FROM GROUPS LEFT OUTER JOIN SUPPLIER SUP ON SUP.SUPPLIERID=GROUPS.SUPPLIERID WHERE USERID= " + UserID + " "
				  StrSql = StrSql + "AND GROUPTYPE= '" + Type + "' "
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetGroupIDByUSer:" + ex.Message.ToString())
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
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetGroupCasesByUSer:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetAllGroupDetails(ByVal UserID As String, ByVal Type As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New StandGetData.Selectdata()
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
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
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
				 strSQL = strSQL + "AND GROUPTYPE= '" + Type + "' "
				 
                DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                If DtRes.Tables(0).Rows.Count > 0 Then
                    For j = 0 To DtRes.Tables(0).Rows.Count - 1
                        'Getting CaseID
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "CASEID "
                        strSQL = strSQL + "FROM GROUPCASES "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString()
                        strSQL = strSQL + " ORDER BY  SEQ"
                        Dts = odbUtil.FillDataSet(strSQL, SBAConnection)
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
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, SBAConnection)
                Else
                    strSqlOutPut = " SELECT * FROM GROUPCASES WHERE GROUPID=0"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, SBAConnection)
                End If

                Return dtOutPut
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function

        Public Function GetPCaseDetailsByGroup(ByVal UserId As String, ByVal CaseDe1 As String, ByVal grpId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  DISTINCT CASEID,(CASEID||':'||CASEDE1) CASEIDD,CASEDE1,REPLACE(CASEDE1,'''','&#')CASEDEAPOS1,CASEDE2,REPLACE(CASEDE2,'''','&#')CASEDEAPOS2,CASEDE3,REPLACE(CASEDE3,'''','&#')CASEDEAPOS3,CASEDES,CREATIONDATE,SERVERDATE,NAME,REPLACE(NAME,'''','&#')NAMEAPOS,SUPPLIERID,APPLICATION,REPLACE(APPLICATION,'''','&#')APPLICATIONAPOS,'' FILENAME  "
                StrSql = StrSql + " FROM "
                StrSql = StrSql + " ( "
                StrSql = StrSql + "SELECT ''USERNAME,0 CASEID,' Nothing'CASEDE1,'Selected'CASEDE2,''CASEDE3,''APPLICATION,''CASEDES,NULL CREATIONDATE,''SERVERDATE,''NAME,NULL SUPPLIERID FROM DUAL  "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT USERS.USERNAME, "
                StrSql = StrSql + "PC.CASEID, "
                StrSql = StrSql + "PC.CASEDE1, "
                StrSql = StrSql + "PC.CASEDE2, "
                StrSql = StrSql + "PC.CASEDE3, "
                StrSql = StrSql + "PC.APPLICATION, "
                StrSql = StrSql + "('PACKAGE FORMAT= '||PC.CASEDE1||' UNIQUE FEATURES= '||PC.CASEDE2)CASEDES, "

                StrSql = StrSql + " PC.CREATIONDATE, "
                StrSql = StrSql + "CASE WHEN PC.CREATIONDATE-PC.SERVERDATE =0 THEN 'NA' ELSE to_char(PC.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE, "
                StrSql = StrSql + " '' NAME, 0 SUPPLIERID "

                StrSql = StrSql + "FROM GROUPS "
                StrSql = StrSql + "INNER JOIN GROUPCASES "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "

                StrSql = StrSql + "RIGHT OUTER JOIN PERMISSIONSCASES PC "
                StrSql = StrSql + "ON PC.CASEID=GROUPCASES.CASEID "
                StrSql = StrSql + "INNER JOIN ECON.USERS ON USERS.USERID=PC.USERID "
                StrSql = StrSql + "WHERE PC.USERID =" + UserId.ToString() + " "
                StrSql = StrSql + "AND PC.CASEID IN(SELECT CASEID FROM GROUPCASES WHERE GROUPID=" + grpId + " ) "
                StrSql = StrSql + " ) "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR CASEID LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(CASEDE3),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(APPLICATION),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY CASEID,UPPER(CASEDE1),UPPER(CASEDE2) "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetPCaseDetailsByGroup:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

		 Public Function GetBCasesByID(ByVal CaseID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID, CASEDE1,CASEDE2,CASEDE3,APPLICATION,NVL(SUPPLIER.SUPPLIERID,0)SUPPLIERID,SUPPLIER.NAME,FILENAME "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + "LEFT OUTER JOIN SUPPLIER ON BASECASES.SUPPLIERID=SUPPLIER.SUPPLIERID "
                StrSql = StrSql + "WHERE CASEID= " + CaseID + " "
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetBCasesByID:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
		Public Function GetBGroupCaseDet2(ByVal UserID As String, ByVal Type As String, ByVal grpID As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New StandGetData.Selectdata()
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim strSQL As String = String.Empty
            Dim strSQL2 As String = String.Empty
            Dim strSQL3 As String = String.Empty
            Dim ds As New DataSet()
            Dim CaseIDs As String = String.Empty
            Dim DtRes As New DataSet()
            Dim cnt As Integer = 0
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
                Dts = objGetData.GetBGroupIDByUSer(UserID, Type)
                If Dts.Tables(0).Rows.Count > 0 Then
                    For i = 0 To Dts.Tables(0).Rows.Count - 1
                        ds = objGetData.GetGroupCasesByUSer(Dts.Tables(0).Rows(i).Item("GROUPID").ToString())
                        If ds.Tables(0).Rows.Count > 0 Then
                            For j = 0 To ds.Tables(0).Rows.Count - 1
                                If j = 0 Then
                                    CaseIDs = ds.Tables(0).Rows(j).Item("CASEID").ToString()
                                Else
                                    CaseIDs = CaseIDs + ", " + ds.Tables(0).Rows(j).Item("CASEID").ToString()
                                End If
                            Next
                        Else
                            CaseIDs = "0"
                        End If
                        If cnt = 0 Then
                            cnt += 1
                            'strSQL = "SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' || ' ,Cases:" + CaseIDs + "' AS GROUPDES,'" + CaseIDs + "' AS CASEIDS FROM DUAL "
                            strSQL = "SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '   ' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '   '|| '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "'  AS GROUPDES,'" + CaseIDs + "' AS CASEIDS,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS DES1,'" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS DES2,'" + Dts.Tables(0).Rows(i).Item("Des3").ToString() + "' AS DES3,'" + Dts.Tables(0).Rows(i).Item("SUPPLIERID").ToString() + "' AS SUPPLIERID,'" + Dts.Tables(0).Rows(i).Item("FILENAME").ToString() + "' AS FILENAME,'" + Dts.Tables(0).Rows(i).Item("APPLICATION").ToString() + "' AS APPLICATION,'" + Dts.Tables(0).Rows(i).Item("NAME").ToString() + "' AS NAME,'" + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + "' || ':' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS CDES1 FROM DUAL "
                        Else
                            cnt += 1
                            strSQL = strSQL + "UNION ALL SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '   ' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '   '|| '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "'  AS GROUPDES,'" + CaseIDs + "' AS CASEIDS,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS DES1,'" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS DES2,'" + Dts.Tables(0).Rows(i).Item("Des3").ToString() + "' AS DES3,'" + Dts.Tables(0).Rows(i).Item("SUPPLIERID").ToString() + "' AS SUPPLIERID,'" + Dts.Tables(0).Rows(i).Item("FILENAME").ToString() + "' AS FILENAME,'" + Dts.Tables(0).Rows(i).Item("APPLICATION").ToString() + "' AS APPLICATION,'" + Dts.Tables(0).Rows(i).Item("NAME").ToString() + "' AS NAME,'" + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + "' || ':' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS CDES1 FROM DUAL "
                        End If
                    Next
                    strSQL = " SELECT * FROM ( " + strSQL + " ) DUAL"
                End If

                strSQL2 = "SELECT 0 GROUPID,'All Groups and All Cases' AS GROUPNAME,''  AS GROUPDES,'' AS CASEIDS,'All Groups' AS DES1,'All Cases' AS DES2,'' AS DES3,'' AS APPLICATION,'' AS NAME,'' AS CDES1 FROM DUAL "

                strSQL2 = strSQL
                strSQL3 = "SELECT GROUPID,GROUPNAME,GROUPDES,CASEIDS,DES1,DES2,DES3,NVL(SUPPLIERID,0) SUPPLIERID,CDES1,APPLICATION,NAME,FILENAME FROM (" + strSQL2 + " ) "

                If grpID = "-1" Then
                Else
                    strSQL3 = strSQL3 + " WHERE  "
                    strSQL3 = strSQL3 + "GROUPID =" + grpID.ToString() + ""
                End If

                strSQL3 = strSQL3 + "ORDER BY UPPER(DES1),UPPER(DES2) "
                DtRes = odbUtil.FillDataSet(strSQL3, MyConnectionString)

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
                StrSql = StrSql + "WHERE USERID =" + UserId.ToString() + " "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "AND CASEID IN(SELECT CASEID FROM GROUPCASES WHERE GROUPID=" + groupID + " ) "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetCases:" + ex.Message.ToString())
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

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetMaxSEQGCASE:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

      
#End Region

#Region "New"
        Public Function GetExtrusionDetailsPref(ByVal CaseId As Integer) As DataSet
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
                StrSql = StrSql + "MAT.EFFDATE EFFDATEB, "
                StrSql = StrSql + "(MAT.T1/PREF.CONVTHICK) AS THICK1, "
                StrSql = StrSql + "(MAT.T2/PREF.CONVTHICK) AS THICK2, "
                StrSql = StrSql + "(MAT.T3/PREF.CONVTHICK) AS THICK3, "
                StrSql = StrSql + "(MAT.T4/PREF.CONVTHICK) AS THICK4, "
                StrSql = StrSql + "(MAT.T5/PREF.CONVTHICK) AS THICK5, "
                StrSql = StrSql + "(MAT.T6/PREF.CONVTHICK) AS THICK6, "
                StrSql = StrSql + "(MAT.T7/PREF.CONVTHICK) AS THICK7, "
                StrSql = StrSql + "(MAT.T8/PREF.CONVTHICK) AS THICK8, "
                StrSql = StrSql + "(MAT.T9/PREF.CONVTHICK) AS THICK9, "
                StrSql = StrSql + "(MAT.T10/PREF.CONVTHICK) AS THICK10, "
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
                StrSql = StrSql + "PREF.TITLE15, "
                StrSql = StrSql + "PREF.UNITS, "
                StrSql = StrSql + "PREF.CONVAREA, "
                StrSql = StrSql + "PREF.CONVTHICK, "
                StrSql = StrSql + "PREF.TITLE19, "
                StrSql = StrSql + "PREF.TITLE20, "
                StrSql = StrSql + "PREF.CONVWT, "
                StrSql = StrSql + "PREF.CURR, "
                StrSql = StrSql + "MAT.OTRTEMP, "
                StrSql = StrSql + "MAT.WVTRTEMP, "
                StrSql = StrSql + "MAT.OTRRH, "
                StrSql = StrSql + "MAT.WVTRRH, "
                StrSql = StrSql + "MAT.ORH, "
                StrSql = StrSql + "MAT.IRH, "
                StrSql = StrSql + "NVL(TYPEM1,0) TYPEM1, "
                StrSql = StrSql + "NVL(TYPEM2,0) TYPEM2, "
                StrSql = StrSql + "NVL(TYPEM3,0) TYPEM3, "
                StrSql = StrSql + "NVL(TYPEM4,0) TYPEM4, "
                StrSql = StrSql + "NVL(TYPEM5,0) TYPEM5, "
                StrSql = StrSql + "NVL(TYPEM6,0) TYPEM6, "
                StrSql = StrSql + "NVL(TYPEM7,0) TYPEM7, "
                StrSql = StrSql + "NVL(TYPEM8,0) TYPEM8, "
                StrSql = StrSql + "NVL(TYPEM9,0) TYPEM9, "
                StrSql = StrSql + "NVL(TYPEM10,0) TYPEM10, "
                StrSql = StrSql + "MAT.TYPEMSG1, "
                StrSql = StrSql + "MAT.TYPEMSG2, "
                StrSql = StrSql + "MAT.TYPEMSG3, "
                StrSql = StrSql + "MAT.TYPEMSG4, "
                StrSql = StrSql + "MAT.TYPEMSG5, "
                StrSql = StrSql + "MAT.TYPEMSG6, "
                StrSql = StrSql + "MAT.TYPEMSG7, "
                StrSql = StrSql + "MAT.TYPEMSG8, "
                StrSql = StrSql + "MAT.TYPEMSG9, "
                StrSql = StrSql + "MAT.TYPEMSG10, "
				'changes started1.1
				 StrSql = StrSql + "TS1VAL1, "
                StrSql = StrSql + "TS1VAL2, "
                StrSql = StrSql + "TS1VAL3, "
                StrSql = StrSql + "TS1VAL4, "
                StrSql = StrSql + "TS1VAL5, "
                StrSql = StrSql + "TS1VAL6, "
                StrSql = StrSql + "TS1VAL7, "
                StrSql = StrSql + "TS1VAL8, "
                StrSql = StrSql + "TS1VAL9, "
                StrSql = StrSql + "TS1VAL10, "
                StrSql = StrSql + "TS2VAL1, "
                StrSql = StrSql + "TS2VAL2, "
                StrSql = StrSql + "TS2VAL3, "
                StrSql = StrSql + "TS2VAL4, "
                StrSql = StrSql + "TS2VAL5, "
                StrSql = StrSql + "TS2VAL6, "
                StrSql = StrSql + "TS2VAL7, "
                StrSql = StrSql + "TS2VAL8, "
                StrSql = StrSql + "TS2VAL9, "
                StrSql = StrSql + "TS2VAL10, "
				StrSql = StrSql + "PREF.CONVPA, "
                StrSql = StrSql + "PREF.TITLE21 "
				'Changes ended1.1

                StrSql = StrSql + "FROM MATERIALINPUT MAT "
                StrSql = StrSql + "INNER JOIN BARRIERINPUT BI "
                StrSql = StrSql + "ON MAT.CASEID=BI.CASEID "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=MAT.CASEID "
                StrSql = StrSql + "WHERE MAT.CASEID =" + CaseId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetExtrusionDetailsBarrP:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetExtrusionDetailsSugg(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  MAT.CASEID,  "
                StrSql = StrSql + "TO_CHAR(MAT.EFFDATE,'MON DD,YYYY')AS EDATE, "
                'StrSql = StrSql + "(TOT.THICK*PREF.CONVTHICK)THICK, "
                StrSql = StrSql + "(MAT1.MATDE1||' '||MAT1.MATDE2)MATS1,  "
                StrSql = StrSql + "(MAT2.MATDE1||' '||MAT2.MATDE2)MATS2, "
                StrSql = StrSql + "(MAT3.MATDE1||' '||MAT3.MATDE2)MATS3, "
                StrSql = StrSql + "(MAT4.MATDE1||' '||MAT4.MATDE2)MATS4, "
                StrSql = StrSql + "(MAT5.MATDE1||' '||MAT5.MATDE2)MATS5, "
                StrSql = StrSql + "(MAT6.MATDE1||' '||MAT6.MATDE2)MATS6, "
                StrSql = StrSql + "(MAT7.MATDE1||' '||MAT7.MATDE2)MATS7, "
                StrSql = StrSql + "(MAT8.MATDE1||' '||MAT8.MATDE2)MATS8, "
                StrSql = StrSql + "(MAT9.MATDE1||' '||MAT9.MATDE2)MATS9, "
                StrSql = StrSql + "(MAT10.MATDE1||' '||MAT10.MATDE2)MATS10, "
                StrSql = StrSql + "(MAT1.ISADJTHICK) ISADJTHICK1,  "
                StrSql = StrSql + "(MAT2.ISADJTHICK) ISADJTHICK2,  "
                StrSql = StrSql + "(MAT3.ISADJTHICK) ISADJTHICK3,  "
                StrSql = StrSql + "(MAT4.ISADJTHICK) ISADJTHICK4,  "
                StrSql = StrSql + "(MAT5.ISADJTHICK) ISADJTHICK5,  "
                StrSql = StrSql + "(MAT6.ISADJTHICK) ISADJTHICK6,  "
                StrSql = StrSql + "(MAT7.ISADJTHICK) ISADJTHICK7,  "
                StrSql = StrSql + "(MAT8.ISADJTHICK) ISADJTHICK8,  "
                StrSql = StrSql + "(MAT9.ISADJTHICK) ISADJTHICK9,  "
                StrSql = StrSql + "(MAT10.ISADJTHICK) ISADJTHICK10,  "
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
				'Changes started1.1
				 StrSql = StrSql + "(MAT1.DGRADATION)AS DG1, "
                StrSql = StrSql + "(MAT2.DGRADATION)AS DG2, "
                StrSql = StrSql + "(MAT3.DGRADATION)AS DG3, "
                StrSql = StrSql + "(MAT4.DGRADATION)AS DG4, "
                StrSql = StrSql + "(MAT5.DGRADATION)AS DG5, "
                StrSql = StrSql + "(MAT6.DGRADATION)AS DG6, "
                StrSql = StrSql + "(MAT7.DGRADATION)AS DG7, "
                StrSql = StrSql + "(MAT8.DGRADATION)AS DG8, "
                StrSql = StrSql + "(MAT9.DGRADATION)AS DG9, "
                StrSql = StrSql + "(MAT10.DGRADATION)AS DG10 "
				'Changes ended1.1
                'StrSql = StrSql + "TOT.DISCRETEWT * PREF.CONVWT AS DISCTOTAL, "
                'StrSql = StrSql + "TOT.DISCRETECOST "
                StrSql = StrSql + "FROM MATERIALINPUT MAT "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=MAT.CASEID "
                'StrSql = StrSql + "INNER JOIN TOTAL TOT "
                'StrSql = StrSql + "ON TOT.CASEID = MAT.CASEID "
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
                StrSql = StrSql + "WHERE MAT.CASEID =" + CaseId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetExtrusionDetailsBarrS:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetGradesVal(ByVal GRADEID As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty

            Try
                StrSql = "SELECT GRADE.GRADEID,GRADE.GRADENAME FROM GRADE WHERE GRADEID =" + GRADEID.ToString()
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetGradesVal:" + ex.Message.ToString())
                Return Dts
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


                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetPrefDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetMinMaxBarrierTemp() As DataSet
            Dim StrSql1 As String
            Dim Ds As DataSet
            Dim odbUtil As New DBUtil()
            Try
                StrSql1 = "select max(TEMPVAL) MAXVAL,min(TEMPVAL) MINVAL from BARRIERTEMP"
                Ds = odbUtil.FillDataSet(StrSql1, SBAConnection)
                Return Ds
            Catch ex As Exception
                Throw New Exception("E1GetData:GetMinMaxBarrierTemp:" + ex.Message.ToString())
                Return Ds
            End Try
        End Function

        Public Function GetMinMaxBarrierHumidity() As DataSet
            Dim StrSql1 As String
            Dim Ds As DataSet
            Dim odbUtil As New DBUtil() '
            Try
                StrSql1 = "select max(RHVALUE) MAXVAL,min(RHVALUE) MINVAL from BARRIERH"
                Ds = odbUtil.FillDataSet(StrSql1, SBAConnection)
                Return Ds
            Catch ex As Exception
                Throw New Exception("E1GetData:GetMinMaxBarrierHumidity:" + ex.Message.ToString())
                Return Ds
            End Try
        End Function

#End Region

        Function GetSponsGrades(ByVal matId As String, ByVal SupId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = " SELECT DISTINCT GRADENAME,GRADEID  "
                StrSql = StrSql + "FROM (SELECT SGD.SGDETAILSID,SGD.GRADEID, "
                StrSql = StrSql + "GRADES.GRADENAME,   SGD.VALUE, SGD.VALUE_U,SGD.THICKNESS,  "
                StrSql = StrSql + "SGD.THICKNESS_U,  SGD.AREA, SGD.AREA_U, SGD.TIME, SGD.TIME_U, SGD.PRESSURE,   "
                StrSql = StrSql + " SGD.PRESSURE_U, SGD.RH, SGD.RH_U, SGD.TEMP, SGD.TEMP_U,   "
                StrSql = StrSql + " SGD.VOLUME, SGD.VOLUME_U, SGD.ANGLE, SGD.ANGLE_U,  SGD.WEIGHT,  "
                StrSql = StrSql + " SGD.WEIGHT_U, PROPERTY.PROPERTYID,  PROPERTY.PROPNAME, PROPERTY.TESTBODY,  "
                StrSql = StrSql + " PROPERTY.TESTNUMBER, SGD.SEQUENCE , PSSEP.SEP1 SEQUENCE1, PSSEP.SEP2 SEQUENCE2,   "
                StrSql = StrSql + " PSSEP.SEP3 SEQUENCE3, PSSEP.SEP4 SEQUENCE4, PSSEP.SEP5 SEQUENCE5,  PSSEP.SEP6 SEQUENCE6,  "
                StrSql = StrSql + " PSSEP.SEP7 SEQUENCE7, PSSEP.SEP8 SEQUENCE8,  PSSEP.SEP9 SEQUENCE9, PSSEP.SEP10 SEQUENCE10   "
                StrSql = StrSql + " FROM SUPPLIERGRADEDETAILS  SGD INNER JOIN PROPERTY  ON SGD.PROPERTYID=PROPERTY.PROPERTYID   "
                StrSql = StrSql + " LEFT OUTER JOIN  PROPERTYSEQ PSSEP  ON PSSEP.SGDETAILSID=SGD.SGDETAILSID  INNER JOIN GRADES   "
                StrSql = StrSql + " ON SGD.GRADEID=GRADES.GRADEID WHERE MATERIALID= " + matId.ToString() + " AND SGD.SUPPLIERID=" + SupId.ToString() + "   "
                StrSql = StrSql + " ORDER BY SGD.SEQUENCE,SGD.SGDETAILSID ASC )  "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SAssist:GetSupplierMaterialSEQ:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
	Public Function GetMetricConvFactor() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT "
                StrSql = StrSql + "METRICUNIT, "
                StrSql = StrSql + "ENGLISHUNIT, "
                StrSql = StrSql + "CONVFACTOR "
                StrSql = StrSql + "FROM METENGFACTORS "
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SAssist:GetSupplierMaterialSEQ:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
		 Public Function GetMetricConvFactorProp() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT PROPERTYID,"
                StrSql = StrSql + "METRICUNIT, "
                StrSql = StrSql + "ENGLISHUNIT, "
                StrSql = StrSql + "CONVFACTOR "
                StrSql = StrSql + "FROM METENGFACTORSP "
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SAssist:GetSupplierMaterialSEQ:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetSponsorSupplier_ORG(ByVal MatId As Integer, ByVal flag As Boolean) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty

            Try
                If MatId <> "0" Then

                    StrSql = "  SELECT    "
                    StrSql = StrSql + "  DISTINCT GS.GRADEID, SP.SUPPLIERID, "
                    StrSql = StrSql + "SP.NAME , "
                    StrSql = StrSql + "GR.GRADENAME, "
                    StrSql = StrSql + "MS.ISSPONSOR "
                    StrSql = StrSql + "FROM   "
                    StrSql = StrSql + "SUPPLIER SP  "
                    StrSql = StrSql + "LEFT OUTER JOIN MATSUPPLIERGRADE GS   "
                    StrSql = StrSql + "ON SP.SUPPLIERID=GS.SUPPLIERID   "
                    StrSql = StrSql + " INNER JOIN MATERIALSUPPLIER MS "
                    StrSql = StrSql + "ON MS.MATID=GS.MATID "
                    StrSql = StrSql + " LEFT OUTER JOIN GRADES GR  "
                    StrSql = StrSql + "ON GR.GRADEID=GS.GRADEID  "
                    StrSql = StrSql + "WHERE GS.MATID = " + MatId.ToString()
                    If flag = True Then
                        StrSql = StrSql + " AND MS.ISSPONSOR='Y' "
                    Else
                        StrSql = StrSql + " AND MS.ISSPONSOR='N' "
                    End If
                    StrSql = StrSql + "ORDER BY NAME,GRADENAME ASC "
                End If

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetSponsorSupplier:" + ex.Message.ToString())
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
                StrSql = StrSql + "FROM CURRENCYARCH "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.EFFDATE = CURRENCYARCH.EFFDATE "
                StrSql = StrSql + "WHERE PREF.CASEID =" + CaseId + ""
                StrSql = StrSql + "AND CURRENCYARCH.CURID = " + CurID + ""
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetCurrancyArch:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCurrancy(ByVal currId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CURID,CURDE1 FROM CURRENCY  "
                StrSql = StrSql + "WHERE CURID = CASE WHEN " + currId.ToString() + " = -1 THEN "
                StrSql = StrSql + "CURID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + currId.ToString() + " "
                StrSql = StrSql + "END  ORDER BY CURDE1"
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetCurrancy:" + ex.Message.ToString())
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
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetCountry:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetEffDate() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT TO_CHAR(EFFDATE,'MON DD,YYYY')AS EDATE FROM EFFDATE ORDER BY EFFDATE DESC"
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetEffDate:" + ex.Message.ToString())
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

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetGrades:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
		
		   Public Function GetSupplier(ByVal MatId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty

            Try
                If MatId <> "0" Then
                    StrSql = "SELECT  "
                    StrSql = StrSql + "DISTINCT "
                    StrSql = StrSql + "SGD.SUPPLIERID, "
                    StrSql = StrSql + "SR.NAME, "
                    StrSql = StrSql + "SR.PHONENUMBER, "
                    StrSql = StrSql + "SR.EMAILADDRESS "
                    StrSql = StrSql + "FROM "
                    StrSql = StrSql + "SUPPLIERGRADEDETAILS SGD "
                    StrSql = StrSql + "INNER JOIN SUPPLIER SR "
                    StrSql = StrSql + "ON SR.SUPPLIERID=SGD.SUPPLIERID "
                    StrSql = StrSql + "WHERE SGD.MATERIALID =" + MatId.ToString() + " "
                End If

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetGrades:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
		
         
        Public Function GetSupplierMaterial(ByVal matId As String, ByVal SupId As String, ByVal GradeId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT SGD.SGDETAILSID,  "
                StrSql = StrSql + "SGD.VALUE, "
                StrSql = StrSql + "SGD.VALUE_U, "
                StrSql = StrSql + "SGD.THICKNESS, "
                StrSql = StrSql + "SGD.THICKNESS_U, "

                StrSql = StrSql + "SGD.AREA, "
                StrSql = StrSql + "SGD.AREA_U, "

                StrSql = StrSql + "SGD.TIME, "
                StrSql = StrSql + "SGD.TIME_U, "

                StrSql = StrSql + "SGD.PRESSURE, "
                StrSql = StrSql + "SGD.PRESSURE_U, "

                StrSql = StrSql + "SGD.RH, "
                StrSql = StrSql + "SGD.RH_U, "

                StrSql = StrSql + "SGD.TEMP, "
                StrSql = StrSql + "SGD.TEMP_U, "

                StrSql = StrSql + "SGD.VOLUME, "
                StrSql = StrSql + "SGD.VOLUME_U, "

                StrSql = StrSql + "SGD.ANGLE, "
                StrSql = StrSql + "SGD.ANGLE_U, "

                StrSql = StrSql + "SGD.WEIGHT, "
                StrSql = StrSql + "SGD.WEIGHT_U, "

                StrSql = StrSql + "SGD.FORCE, "
                StrSql = StrSql + "SGD.FORCE_U, "

                StrSql = StrSql + "SGD.PERCENTAGE, "
                StrSql = StrSql + "SGD.PERCENTAGE_U, "

                StrSql = StrSql + "SGD.LENGTH, "
                StrSql = StrSql + "SGD.LENGTH_U, "

                StrSql = StrSql + "SGD.ENERGY , "
                StrSql = StrSql + "SGD.ENERGY_U, "

                StrSql = StrSql + "SGD.TTHICKNESS, "
                StrSql = StrSql + "SGD.TTHICKNESS_U, "

                StrSql = StrSql + "SGD.TAREA, "
                StrSql = StrSql + "SGD.TAREA_U, "

                StrSql = StrSql + "SGD.TTIME, "
                StrSql = StrSql + "SGD.TTIME_U, "

                StrSql = StrSql + "SGD.TPRESSURE, "
                StrSql = StrSql + "SGD.TPRESSURE_U, "

                StrSql = StrSql + "SGD.TRH, "
                StrSql = StrSql + "SGD.TRH_U, "

                StrSql = StrSql + "SGD.TTEMP, "
                StrSql = StrSql + "SGD.TTEMP_U, "

                StrSql = StrSql + "SGD.TVOLUME, "
                StrSql = StrSql + "SGD.TVOLUME_U, "
                StrSql = StrSql + "SGD.TANGLE, "
                StrSql = StrSql + "SGD.TANGLE_U, "

                StrSql = StrSql + "SGD.TWEIGHT, "
                StrSql = StrSql + "SGD.TWEIGHT_U, "

                StrSql = StrSql + "SGD.TFORCE, "
                StrSql = StrSql + "SGD.TFORCE_U, "

                StrSql = StrSql + "SGD.TPERCENTAGE, "
                StrSql = StrSql + "SGD.TPERCENTAGE_U, "

                StrSql = StrSql + "SGD.TLENGTH, "
                StrSql = StrSql + "SGD.TLENGTH_U, "

                StrSql = StrSql + "SGD.TENERGY , "
                StrSql = StrSql + "SGD.TENERGY_U, "
                StrSql = StrSql + "SGD.COMMENTS, "
                StrSql = StrSql + "PROPERTY.PROPERTYID, "
                StrSql = StrSql + "PROPERTY.PROPNAME, "
                StrSql = StrSql + "PROPERTY.TESTBODY, "
                StrSql = StrSql + "PROPERTY.TESTNUMBER, "
                StrSql = StrSql + "SGD.SEQUENCE , "
                StrSql = StrSql + "PSSEP.SEP1 SEQUENCE1, "
                StrSql = StrSql + "PSSEP.SEP2 SEQUENCE2, "
                StrSql = StrSql + "PSSEP.SEP3 SEQUENCE3, "
                StrSql = StrSql + "PSSEP.SEP4 SEQUENCE4, "
                StrSql = StrSql + "PSSEP.SEP5 SEQUENCE5, "
                StrSql = StrSql + "PSSEP.SEP6 SEQUENCE6, "
                StrSql = StrSql + "PSSEP.SEP7 SEQUENCE7, "
                StrSql = StrSql + "PSSEP.SEP8 SEQUENCE8, "
                StrSql = StrSql + "PSSEP.SEP9 SEQUENCE9, "
                StrSql = StrSql + "PSSEP.SEP10 SEQUENCE10, "
                StrSql = StrSql + "PSSEP.TSEP1 TSEQUENCE1, "
                StrSql = StrSql + "PSSEP.TSEP2 TSEQUENCE2, "
                StrSql = StrSql + "PSSEP.TSEP3 TSEQUENCE3, "
                StrSql = StrSql + "PSSEP.TSEP4 TSEQUENCE4, "
                StrSql = StrSql + "PSSEP.TSEP5 TSEQUENCE5, "
                StrSql = StrSql + "PSSEP.TSEP6 TSEQUENCE6, "
                StrSql = StrSql + "PSSEP.TSEP7 TSEQUENCE7, "
                StrSql = StrSql + "PSSEP.TSEP8 TSEQUENCE8, "
                StrSql = StrSql + "PSSEP.TSEP9 TSEQUENCE9, "
                StrSql = StrSql + "PSSEP.TSEP10 TSEQUENCE10 "

                StrSql = StrSql + "FROM SUPPLIERGRADEDETAILS SGD "
                StrSql = StrSql + "INNER JOIN PROPERTY "
                StrSql = StrSql + "ON SGD.PROPERTYID=PROPERTY.PROPERTYID "
                StrSql = StrSql + "LEFT OUTER JOIN PROPERTYSEQ PSSEP "
                StrSql = StrSql + "ON PSSEP.SGDETAILSID=SGD.SGDETAILSID "
                StrSql = StrSql + "WHERE MATERIALID= " + matId.ToString() + " "
                StrSql = StrSql + "AND SGD.SUPPLIERID='" + SupId.ToString() + "'"
                StrSql = StrSql + "AND GRADEID=" + GradeId.ToString() + " "
                StrSql = StrSql + "ORDER BY SGD.SEQUENCE,SGD.SGDETAILSID ASC "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SAssist:GetSupplierMaterial:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSupplierMaterialSEQ(ByVal matId As String, ByVal SupId As String, ByVal GradeId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                 StrSql = "SELECT DISTINCT  "
                StrSql = StrSql + "SGD.PROPERTYID, "
                StrSql = StrSql + "SEQ1 SEQUENCE1, "
                StrSql = StrSql + "SEQ2 SEQUENCE2, "
                StrSql = StrSql + "SEQ3 SEQUENCE3, "
                StrSql = StrSql + "SEQ4 SEQUENCE4, "
                StrSql = StrSql + "SEQ5 SEQUENCE5, "
                StrSql = StrSql + "SEQ6 SEQUENCE6, "
                StrSql = StrSql + "SEQ7 SEQUENCE7, "
                StrSql = StrSql + "SEQ8 SEQUENCE8, "
                StrSql = StrSql + "SEQ9 SEQUENCE9, "
                StrSql = StrSql + "SEQ10 SEQUENCE10, "

                StrSql = StrSql + "TSEQ1 TSEQUENCE1, "
                StrSql = StrSql + "TSEQ2 TSEQUENCE2, "
                StrSql = StrSql + "TSEQ3 TSEQUENCE3, "
                StrSql = StrSql + "TSEQ4 TSEQUENCE4, "
                StrSql = StrSql + "TSEQ5 TSEQUENCE5, "
                StrSql = StrSql + "TSEQ6 TSEQUENCE6, "
                StrSql = StrSql + "TSEQ7 TSEQUENCE7, "
                StrSql = StrSql + "TSEQ8 TSEQUENCE8, "
                StrSql = StrSql + "TSEQ9 TSEQUENCE9, "
                StrSql = StrSql + "TSEQ10 TSEQUENCE10, "
                StrSql = StrSql + "SGD.SEQUENCE "
                StrSql = StrSql + "FROM PROPERTYSEQ "
                StrSql = StrSql + "INNER JOIN SUPPLIERGRADEDETAILS SGD "
                StrSql = StrSql + "ON PROPERTYSEQ.SGDETAILSID=SGD.SGDETAILSID "
                StrSql = StrSql + "WHERE SGD.MATERIALID= " + matId.ToString() + " "
                StrSql = StrSql + "AND SGD.SUPPLIERID='" + SupId.ToString() + "' "
                StrSql = StrSql + "AND SGD.GRADEID=" + GradeId.ToString() + " "
                StrSql = StrSql + "ORDER BY SGD.SEQUENCE ASC "
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SAssist:GetSupplierMaterialSEQ:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetMatSupplierGrade_ORG(ByVal matId As String, ByVal SupId As String, ByVal GradeId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = " SELECT SP.NAME,GRADES.GRADENAME,GRADES.GRADEID,SP.SUPPLIERID FROM SUPPLIER SP "
                StrSql = StrSql + "INNER JOIN MATSUPPLIERGRADE MS  "
                StrSql = StrSql + "ON SP.SUPPLIERID=MS.SUPPLIERID "
                StrSql = StrSql + "INNER JOIN GRADES  "
                StrSql = StrSql + "ON GRADES.GRADEID = MS.GRADEID "
                StrSql = StrSql + "WHERE MS.MATID= " + matId.ToString() + " "
                StrSql = StrSql + "AND GRADES.GRADEID=" + GradeId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SAssist:GetSupplierMaterial:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
	Public Function GetMatSupGradeType(ByVal matId As String, ByVal SupId As String, ByVal GradeId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = " SELECT MATID,MATERIAL FROM "
                StrSql = StrSql + "(SELECT MATID,MATERIAL FROm ADHESIVEPROP "
                StrSql = StrSql + "UNION SELECT MATID,MATERIAL FROm ALUMINUMPROP "
                StrSql = StrSql + "UNION SELECT MATID,DESCRIPTION MATERIAL FROm BASEFIBERPROP "
                StrSql = StrSql + "UNION SELECT MATID,MATERIAL FROm COATINGSPROP "
                StrSql = StrSql + "UNION SELECT MATID,DESCRIPTION MATERIAL  FROm CONCENTRATEPROP "
                StrSql = StrSql + "UNION SELECT MATID,MATERIAL FROm FILMPROP "
                StrSql = StrSql + "UNION SELECT MATID,MATERIAL FROm GLASSPROP "
                StrSql = StrSql + "UNION SELECT MATID,MATERIAL FROm INKPROP "
                StrSql = StrSql + "UNION SELECT MATID,MATERIAL FROm NONWOVENSPROP "
                StrSql = StrSql + "UNION SELECT MATID,DESCRIPTION MATERIAL FROm PAPERBOARDPROP "
                StrSql = StrSql + "UNION SELECT MATID,DESCRIPTION MATERIAL FROm PAPERPROP "
                StrSql = StrSql + "UNION SELECT MATID,MATERIAL FROm RESINPROP "
                StrSql = StrSql + "UNION SELECT MATID,DESCRIPTION MATERIAL FROm SHEETPROP "
                StrSql = StrSql + "UNION SELECT MATID,DESCRIPTION MATERIAL FROm STEELPROP "
                StrSql = StrSql + ") "
                StrSql = StrSql + "WHERE MATID=" + matId.ToString() + ""

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SAssist:GetSupplierMaterial:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetAllSupplierMaterial(ByVal matId As String, ByVal SupId As String, ByVal GradeId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT SGD.SGDETAILSID,  "
                StrSql = StrSql + "SGD.VALUE, "
                StrSql = StrSql + "SGD.VALUE_U, "
                StrSql = StrSql + "SGD.THICKNESS, "
                StrSql = StrSql + "SGD.THICKNESS_U, "
                StrSql = StrSql + "SGD.AREA, "
                StrSql = StrSql + "SGD.AREA_U, "
                StrSql = StrSql + "SGD.TIME, "
                StrSql = StrSql + "SGD.TIME_U, "
                StrSql = StrSql + "SGD.PRESSURE, "
                StrSql = StrSql + "SGD.PRESSURE_U, "
                StrSql = StrSql + "SGD.RH, "
                StrSql = StrSql + "SGD.RH_U, "
                StrSql = StrSql + "SGD.TEMP, "
                StrSql = StrSql + "SGD.TEMP_U, "
                StrSql = StrSql + "SGD.VOLUME, "
                StrSql = StrSql + "SGD.VOLUME_U, "
                StrSql = StrSql + "SGD.ANGLE, "
                StrSql = StrSql + "SGD.ANGLE_U, "
                StrSql = StrSql + "SGD.WEIGHT, "
                StrSql = StrSql + "SGD.WEIGHT_U, "
                StrSql = StrSql + "PROPERTY.PROPERTYID, "
                StrSql = StrSql + "PROPERTY.PROPNAME, "
                StrSql = StrSql + "PROPERTY.TESTBODY, "
                StrSql = StrSql + "PROPERTY.TESTNUMBER, "
                StrSql = StrSql + "SGD.SEQUENCE , "
                StrSql = StrSql + "PSSEP.SEP1 SEQUENCE1, "
                StrSql = StrSql + "PSSEP.SEP2 SEQUENCE2, "
                StrSql = StrSql + "PSSEP.SEP3 SEQUENCE3, "
                StrSql = StrSql + "PSSEP.SEP4 SEQUENCE4, "
                StrSql = StrSql + "PSSEP.SEP5 SEQUENCE5, "
                StrSql = StrSql + "PSSEP.SEP6 SEQUENCE6, "
                StrSql = StrSql + "PSSEP.SEP7 SEQUENCE7, "
                StrSql = StrSql + "PSSEP.SEP8 SEQUENCE8, "
                StrSql = StrSql + "PSSEP.SEP9 SEQUENCE9, "
                StrSql = StrSql + "PSSEP.SEP10 SEQUENCE10 "
                StrSql = StrSql + "FROM SUPPLIERGRADEDETAILS SGD "
                StrSql = StrSql + "INNER JOIN PROPERTY "
                StrSql = StrSql + "ON SGD.PROPERTYID=PROPERTY.PROPERTYID "
                StrSql = StrSql + "LEFT OUTER JOIN PROPERTYSEQ PSSEP "
                StrSql = StrSql + "ON PSSEP.SGDETAILSID=SGD.SGDETAILSID "
                StrSql = StrSql + "WHERE MATERIALID= " + matId.ToString() + " "
                StrSql = StrSql + "AND SGD.SUPPLIERID='" + SupId.ToString() + "' "
                StrSql = StrSql + "ORDER BY SGD.SEQUENCE,SGD.SGDETAILSID ASC "
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SAssist:GetSupplierMaterial:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetAllSupplierMaterialSEQ(ByVal matId As String, ByVal SupId As String, ByVal GradeId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT DISTINCT  "
                StrSql = StrSql + "SGD.PROPERTYID, "
                StrSql = StrSql + "SEQ1 SEQUENCE1, "
                StrSql = StrSql + "SEQ2 SEQUENCE2, "
                StrSql = StrSql + "SEQ3 SEQUENCE3, "
                StrSql = StrSql + "SEQ4 SEQUENCE4, "
                StrSql = StrSql + "SEQ5 SEQUENCE5, "
                StrSql = StrSql + "SEQ6 SEQUENCE6, "
                StrSql = StrSql + "SEQ7 SEQUENCE7, "
                StrSql = StrSql + "SEQ8 SEQUENCE8, "
                StrSql = StrSql + "SEQ9 SEQUENCE9, "
                StrSql = StrSql + "SEQ10 SEQUENCE10 "
                StrSql = StrSql + "FROM PROPERTYSEQ "
                StrSql = StrSql + "INNER JOIN SUPPLIERGRADEDETAILS SGD "
                StrSql = StrSql + "ON PROPERTYSEQ.SGDETAILSID=SGD.SGDETAILSID "
                StrSql = StrSql + "WHERE SGD.MATERIALID= " + matId.ToString() + " "
                StrSql = StrSql + "AND SGD.SUPPLIERID='" + SupId.ToString() + "' "
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SAssist:GetSupplierMaterialSEQ:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

		#Region "New Sassist"
		  Public Function GetBGroupIDByUSer(ByVal UserID As String, ByVal Type As String) As DataSet
            Dim Dts As New DataSet()
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
                Dim StrSql As String = ""
                StrSql = "SELECT GROUPID,  "
                StrSql = StrSql + "DES1, "
                StrSql = StrSql + "DES2, "
                StrSql = StrSql + "DES3, "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "CREATIONDATE, "
                StrSql = StrSql + "UPDATEDATE, "
                StrSql = StrSql + "APPLICATION, "
                StrSql = StrSql + "SUP.NAME, "
                StrSql = StrSql + "SUP.SUPPLIERID,FILENAME "
                StrSql = StrSql + "FROM GROUPS LEFT OUTER JOIN SUPPLIER SUP ON SUP.SUPPLIERID=GROUPS.SUPPLIERID WHERE GROUPTYPE= '" + Type + "' "

                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetGroupIDByUSer:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPGroupCaseDet1(ByVal UserID As String, ByVal Type As String, ByVal Key As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New StandGetData.Selectdata()
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim strSQL As String = String.Empty
            Dim strSQL2 As String = String.Empty
            Dim strSQL3 As String = String.Empty
            Dim ds As New DataSet()
            Dim CaseIDs As String = String.Empty
            Dim DtRes As New DataSet()
            Dim cnt As Integer = 0
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
                Dts = objGetData.GetGroupIDByUSer(UserID, Type)
                If Dts.Tables(0).Rows.Count > 0 Then
                    For i = 0 To Dts.Tables(0).Rows.Count - 1
                        ds = objGetData.GetGroupCasesByUSer(Dts.Tables(0).Rows(i).Item("GROUPID").ToString())
                        If ds.Tables(0).Rows.Count > 0 Then
                            For j = 0 To ds.Tables(0).Rows.Count - 1
                                If j = 0 Then
                                    CaseIDs = ds.Tables(0).Rows(j).Item("CASEID").ToString()
                                Else
                                    CaseIDs = CaseIDs + ", " + ds.Tables(0).Rows(j).Item("CASEID").ToString()
                                End If
                            Next
                        Else
                            CaseIDs = "0"
                        End If
                        If cnt = 0 Then
                            cnt += 1
                            'strSQL = "SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' || ' ,Cases:" + CaseIDs + "' AS GROUPDES,'" + CaseIDs + "' AS CASEIDS FROM DUAL "
                            strSQL = "SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' || '   ' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' || '   '|| '" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "'  AS GROUPDES,'" + CaseIDs + "' AS CASEIDS,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' AS DES1,REPLACE('" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "','''','&#')DESAPOS1,'" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "' AS DES2,REPLACE('" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "','''','&#')DESAPOS2,'" + Dts.Tables(0).Rows(i).Item("Des3").ToString().Replace("'", "''") + "' AS DES3,REPLACE('" + Dts.Tables(0).Rows(i).Item("Des3").ToString().Replace("'", "''") + "','''','&#')DESAPOS3,'" + Dts.Tables(0).Rows(i).Item("APPLICATION").ToString().Replace("'", "''") + "' AS APPLICATION,REPLACE('" + Dts.Tables(0).Rows(i).Item("APPLICATION").ToString().Replace("'", "''") + "','''','&#')APPLICATIONAPOS,'" + Dts.Tables(0).Rows(i).Item("NAME").ToString().Replace("'", "''") + "' AS NAME,REPLACE('" + Dts.Tables(0).Rows(i).Item("NAME").ToString().Replace("'", "''") + "','''','&#')NAMEAPOS,'" + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + "' || ':' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' AS CDES1,'' FILENAME FROM DUAL "
                        Else
                            cnt += 1
                            strSQL = strSQL + "UNION ALL SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' || '   ' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' || '   '|| '" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "'  AS GROUPDES,'" + CaseIDs + "' AS CASEIDS,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' AS DES1,REPLACE('" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "','''','&#')DESAPOS1,'" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "' AS DES2,REPLACE('" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "','''','&#')DESAPOS2,'" + Dts.Tables(0).Rows(i).Item("Des3").ToString().Replace("'", "''") + "' AS DES3,REPLACE('" + Dts.Tables(0).Rows(i).Item("Des3").ToString().Replace("'", "''") + "','''','&#')DESAPOS3,'" + Dts.Tables(0).Rows(i).Item("APPLICATION").ToString().Replace("'", "''") + "' AS APPLICATION,REPLACE('" + Dts.Tables(0).Rows(i).Item("APPLICATION").ToString().Replace("'", "''") + "','''','&#')APPLICATIONAPOS,'" + Dts.Tables(0).Rows(i).Item("NAME").ToString().Replace("'", "''") + "' AS NAME,REPLACE('" + Dts.Tables(0).Rows(i).Item("NAME").ToString().Replace("'", "''") + "','''','&#')NAMEAPOS,'" + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + "' || ':' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' AS CDES1,'' FILENAME FROM DUAL "
                        End If
                    Next
                    strSQL = " SELECT * FROM ( " + strSQL + " ) DUAL"
                End If

                strSQL2 = "SELECT 0 GROUPID,'All Structures' AS GROUPNAME,''  AS GROUPDES,'' AS CASEIDS,'All Structures' AS DES1,'All Structures' AS DESAPOS1,'' AS DES2,'' AS DESAPOS2,'' AS DES3,'' AS DESAPOS3,'' AS APPLICATION,'' AS APPLICATIONAPOS,'' AS NAME,'' AS NAMEAPOS,'' AS CDES1,'' FILENAME FROM DUAL "
                If strSQL <> "" Then
                    strSQL2 = strSQL2 + " UNION ALL " + strSQL
                    strSQL3 = "SELECT GROUPID,GROUPNAME,GROUPDES,CASEIDS,(GROUPID||':'||DES1) GROUPIDD,DES1,REPLACE(DES1,'''','&#')DESAPOS1,DES2,REPLACE(DES2,'''','&#')DESAPOS2,DES3,REPLACE(DES3,'''','&#')DESAPOS3,CDES1,APPLICATION,REPLACE(APPLICATION,'''','&#')APPLICATIONAPOS,NAME,REPLACE(NAME,'''','&#')NAMEAPOS,'' FILENAME FROM (" + strSQL2 + " ) "
                    strSQL3 = strSQL3 + " WHERE NVL(UPPER(DES1),'#') LIKE '%" + Key.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSQL3 = strSQL3 + "OR NVL(UPPER(DES2),'#') LIKE '%" + Key.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSQL3 = strSQL3 + "OR NVL(UPPER(DES3),'#') LIKE '%" + Key.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSQL3 = strSQL3 + "OR NVL(UPPER(APPLICATION),'#') LIKE '%" + Key.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSQL3 = strSQL3 + "OR NVL(UPPER(CASEIDS),'#') LIKE '%" + Key.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSQL3 = strSQL3 + "OR NVL(UPPER(NAME),'#') LIKE '%" + Key.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSQL3 = strSQL3 + "OR GROUPID LIKE '%" + Key.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSQL3 = strSQL3 + "ORDER BY GROUPID,UPPER(DES1),UPPER(DES2) "
                    DtRes = odbUtil.FillDataSet(strSQL3, MyConnectionString)
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
        Public Function GetBGroupCaseDet1(ByVal UserID As String, ByVal Type As String, ByVal key As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New StandGetData.Selectdata()
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim strSQL As String = String.Empty
            Dim strSQL2 As String = String.Empty
            Dim strSQL3 As String = String.Empty
            Dim ds As New DataSet()
            Dim CaseIDs As String = String.Empty
            Dim DtRes As New DataSet()
            Dim cnt As Integer = 0
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
                Dts = objGetData.GetBGroupIDByUSer(UserID, Type)
                If Dts.Tables(0).Rows.Count > 0 Then
                    For i = 0 To Dts.Tables(0).Rows.Count - 1
                        ds = objGetData.GetGroupCasesByUSer(Dts.Tables(0).Rows(i).Item("GROUPID").ToString())
                        If ds.Tables(0).Rows.Count > 0 Then
                            For j = 0 To ds.Tables(0).Rows.Count - 1
                                If j = 0 Then
                                    CaseIDs = ds.Tables(0).Rows(j).Item("CASEID").ToString()
                                Else
                                    CaseIDs = CaseIDs + ", " + ds.Tables(0).Rows(j).Item("CASEID").ToString()
                                End If
                            Next
                        Else
                            CaseIDs = "0"
                        End If
                        If cnt = 0 Then
                            cnt += 1
                            'strSQL = "SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' || ' ,Cases:" + CaseIDs + "' AS GROUPDES,'" + CaseIDs + "' AS CASEIDS FROM DUAL "
                            strSQL = "SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' || '   ' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' || '   '|| '" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "'  AS GROUPDES,'" + CaseIDs + "' AS CASEIDS,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' AS DES1,REPLACE('" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "','''','&#')DESAPOS1,'" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "' AS DES2,REPLACE('" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "','''','&#')DESAPOS2,'" + Dts.Tables(0).Rows(i).Item("Des3").ToString().Replace("'", "''") + "' AS DES3,REPLACE('" + Dts.Tables(0).Rows(i).Item("Des3").ToString().Replace("'", "''") + "','''','&#')DESAPOS3,'" + Dts.Tables(0).Rows(i).Item("APPLICATION").ToString().Replace("'", "''") + "' AS APPLICATION,REPLACE('" + Dts.Tables(0).Rows(i).Item("APPLICATION").ToString().Replace("'", "''") + "','''','&#')APPLICATIONAPOS,'" + Dts.Tables(0).Rows(i).Item("NAME").ToString().Replace("'", "''") + "' AS NAME,REPLACE('" + Dts.Tables(0).Rows(i).Item("NAME").ToString().Replace("'", "''") + "','''','&#')NAMEAPOS,'" + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + "' || ':' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' AS CDES1, '" + Dts.Tables(0).Rows(i).Item("FILENAME").ToString() + "' AS FILENAME FROM DUAL "
                        Else
                            cnt += 1
                            strSQL = strSQL + "UNION ALL SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' || '   ' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' || '   '|| '" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "'  AS GROUPDES,'" + CaseIDs + "' AS CASEIDS,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' AS DES1,REPLACE('" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "','''','&#')DESAPOS1,'" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "' AS DES2,REPLACE('" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "','''','&#')DESAPOS2,'" + Dts.Tables(0).Rows(i).Item("Des3").ToString().Replace("'", "''") + "' AS DES3,REPLACE('" + Dts.Tables(0).Rows(i).Item("Des3").ToString().Replace("'", "''") + "','''','&#')DESAPOS3,'" + Dts.Tables(0).Rows(i).Item("APPLICATION").ToString().Replace("'", "''") + "' AS APPLICATION,REPLACE('" + Dts.Tables(0).Rows(i).Item("APPLICATION").ToString().Replace("'", "''") + "','''','&#')APPLICATIONAPOS,'" + Dts.Tables(0).Rows(i).Item("NAME").ToString().Replace("'", "''") + "' AS NAME,REPLACE('" + Dts.Tables(0).Rows(i).Item("NAME").ToString().Replace("'", "''") + "','''','&#')NAMEAPOS,'" + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + "' || ':' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' AS CDES1, '" + Dts.Tables(0).Rows(i).Item("FILENAME").ToString() + "' AS FILENAME FROM DUAL "
                        End If
                    Next
                    strSQL = " SELECT * FROM ( " + strSQL + " ) DUAL"
                End If

                strSQL2 = "SELECT 0 GROUPID,'All Structures' AS GROUPNAME,''  AS GROUPDES,'' AS CASEIDS,'All Structures' AS DES1,'All Structures' AS DESAPOS1,'' AS DES2,'' AS DESAPOS2,'' AS DES3,'' AS DESAPOS3,'' AS APPLICATION,'' AS APPLICATIONAPOS,'' AS NAME,'' AS NAMEAPOS,'' AS CDES1,'' AS FILENAME FROM DUAL "
                If strSQL <> "" Then
                    strSQL2 = strSQL2 + " UNION ALL " + strSQL
                    strSQL3 = "SELECT GROUPID,GROUPNAME,GROUPDES,(GROUPID||':'||DES1) GROUPIDD,CASEIDS,DES1,REPLACE(DES1,'''','&#')DESAPOS1,DES2,REPLACE(DES2,'''','&#')DESAPOS2,DES3,REPLACE(DES3,'''','&#')DESAPOS3,CDES1,APPLICATION,REPLACE(APPLICATION,'''','&#')APPLICATIONAPOS,NAME,REPLACE(NAME,'''','&#')NAMEAPOS,FILENAME FROM (" + strSQL2 + " ) "
                    strSQL3 = strSQL3 + " WHERE NVL(UPPER(DES1),'#') LIKE '%" + key.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSQL3 = strSQL3 + "OR NVL(UPPER(DES2),'#') LIKE '%" + key.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSQL3 = strSQL3 + "OR NVL(UPPER(DES3),'#') LIKE '%" + key.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSQL3 = strSQL3 + "OR NVL(UPPER(APPLICATION),'#') LIKE '%" + key.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSQL3 = strSQL3 + "OR NVL(UPPER(CASEIDS),'#') LIKE '%" + key.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSQL3 = strSQL3 + "OR NVL(UPPER(NAME),'#') LIKE '%" + key.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSQL3 = strSQL3 + "OR GROUPID LIKE '%" + key.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSQL3 = strSQL3 + "ORDER BY GROUPID,UPPER(DES1),UPPER(DES2) "
                    DtRes = odbUtil.FillDataSet(strSQL3, MyConnectionString)
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

        Public Function GetGroupIDCheck(ByVal Des1 As String, ByVal Des2 As String, ByVal UserID As String, ByVal Type As Object) As DataSet
            Dim odButil As New DBUtil()
            Dim strsql As String = String.Empty
            Dim Dts As New DataSet


            Try
                'Getting GROUPID 
                strsql = String.Empty
                strsql = "SELECT 1 "
                strsql = strsql + "FROM "
                strsql = strsql + "GROUPS "
                strsql = strsql + "WHERE "
                strsql = strsql + "GROUPTYPE='" + Type + "' "
                strsql = strsql + "AND UPPER(DES1)='" + (Des1.ToString().Replace("'", "''")).ToUpper() + "' "
                strsql = strsql + "AND "
                If Des2 = "" Then
                    strsql = strsql + "DES2 IS NULL "
                Else
                    strsql = strsql + "UPPER(DES2)='" + (Des2.ToString().Replace("'", "''")).ToUpper() + "' "
                End If

                Dts = odButil.FillDataSet(strsql, SBAConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetGetGroupIDCheck:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetAllBGroupDetails(ByVal UserID As String, ByVal Type As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New StandGetData.Selectdata()
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
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
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
                strSQL = strSQL + "WHERE GROUPTYPE= '" + Type + "'"

                DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                If DtRes.Tables(0).Rows.Count > 0 Then
                    For j = 0 To DtRes.Tables(0).Rows.Count - 1
                        'Getting CaseID
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "CASEID "
                        strSQL = strSQL + "FROM GROUPCASES "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString()
                        strSQL = strSQL + " ORDER BY  SEQ"
                        Dts = odbUtil.FillDataSet(strSQL, SBAConnection)
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
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString() + " "
                        If j = 0 Then
                            strSqlOutPut = strSQL
                        Else
                            strSqlOutPut = strSqlOutPut + " UNION ALL " + strSQL
                        End If
                    Next

                    strSqlOutPut = "SELECT * FROM ( " + strSqlOutPut + " ) DUAL ORDER BY UPPER(GROUPNAME),UPPER(GROUPDES)"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, SBAConnection)
                Else
                    strSqlOutPut = " SELECT * FROM GROUPCASES WHERE GROUPID=0"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, SBAConnection)
                End If

                Return dtOutPut
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function
        Public Function GetBCaseGrpDetails_ORG(ByVal UserName As String, ByVal UserID As String, ByVal keyWord As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT GROUPNAME,GROUPID,CASEID,CASEDE1,CASEDE2,CASEDE3,CASEDES,CREATIONDATE,SERVERDATE "
                StrSql = StrSql + " FROM "
                StrSql = StrSql + " ( "
                StrSql = StrSql + "SELECT CASE WHEN NVL(GROUPS.GROUPID,'0')='0' THEN 'None' ELSE GROUPS.DES1 END AS GROUPNAME,  "
                StrSql = StrSql + "NVL(GROUPS.GROUPID,'0') AS GROUPID, "
                StrSql = StrSql + "BC.CASEID, "
                StrSql = StrSql + "BC.CASEDE1, "
                StrSql = StrSql + "BC.CASEDE2, "
                StrSql = StrSql + "BC.CASEDE3, "
                StrSql = StrSql + "(BC.CASEID||'. PACKAGE FORMAT= '||BC.CASEDE1||' UNIQUE FEATURES= '||BC.CASEDE2)CASEDES, "

                StrSql = StrSql + " BC.CREATIONDATE, "
                StrSql = StrSql + "CASE WHEN BC.CREATIONDATE-BC.SERVERDATE =0 THEN 'NA' ELSE to_char(BC.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE "


                StrSql = StrSql + "FROM GROUPS "
                StrSql = StrSql + "INNER JOIN GROUPCASES "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
                StrSql = StrSql + "AND GROUPS.GROUPTYPE='" + UserID + "' "
                StrSql = StrSql + "RIGHT OUTER JOIN BASECASES BC "
                StrSql = StrSql + "ON BC.CASEID=GROUPCASES.CASEID "

                StrSql = StrSql + "ORDER BY CASEID DESC "
                StrSql = StrSql + " ) "

                StrSql = StrSql + "WHERE UPPER(CASEDE1) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE2) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEID) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE3) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(GROUPNAME) LIKE '%" + keyWord.ToUpper().Trim() + "%' "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetPCaseGrpDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Function GetBCaseSearchByGroup(ByVal CaseDe1 As String, ByVal CaseDe2 As String, ByVal grpId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + "WHERE CASEID IN(SELECT CASEID FROM GROUPCASES WHERE GROUPID=" + grpId + " ) "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetBCaseSearchByGroup:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetBGroupDetails(ByVal UserID As String, ByVal flag As Char, ByVal Type As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New StandGetData.Selectdata()
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
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
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
                strSQL = strSQL + "WHERE GROUPTYPE= '" + Type + "' "
                DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                If DtRes.Tables(0).Rows.Count > 0 Then
                    For j = 0 To DtRes.Tables(0).Rows.Count - 1
                        'Getting CaseID
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "CASEID "
                        strSQL = strSQL + "FROM GROUPCASES "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString()
                        strSQL = strSQL + " ORDER BY  SEQ"
                        Dts = odbUtil.FillDataSet(strSQL, SBAConnection)
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
                        strSQL = strSQL + "DES2 GROUPDES,DES3,APPLICATION,FILENAME,NVL (SUPPLIER.NAME,'NA') NAME,SUPPLIER.SUPPLIERID SPONSID,  "
                        strSQL = strSQL + "GROUPID || ':'|| DES1 CDES1,"
                        strSQL = strSQL + "to_char(CREATIONDATE,'mm/dd/yyyy hh:mi:ss AM'  ) CREATIONDATE, "
                        strSQL = strSQL + "CASE WHEN CREATIONDATE-UPDATEDATE =0 THEN 'NA' ELSE to_char(UPDATEDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  UPDATEDATE "
                        strSQL = strSQL + "FROM "
                        strSQL = strSQL + "GROUPS "
                        strSQL = strSQL + "LEFT OUTER JOIN SUPPLIER ON GROUPS.SUPPLIERID=SUPPLIER.SUPPLIERID "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString() + " "
                        If j = 0 Then
                            strSqlOutPut = strSQL
                        Else
                            strSqlOutPut = strSqlOutPut + " UNION ALL " + strSQL
                        End If
                    Next
                    If flag = "Y" Then ' Y FOR GROUPDEATILS PAGE ,N FOR EDITGROUPS PAGE
                        strSQL = "SELECT 0 GROUPID,  "
                        strSQL = strSQL + "'None'  GROUPNAME, "
                        strSQL = strSQL + "'' CaseID, "
                        strSQL = strSQL + "''  GROUPDES,"
                        strSQL = strSQL + " '' DES3,'' APPLICATION, "
                        strSQL = strSQL + "''  FILENAME, "
                        strSQL = strSQL + "''  NAME, "
                        strSQL = strSQL + "0  SPONSID, "
                        strSQL = strSQL + "'0:None'  CDES1, "
                        strSQL = strSQL + "'NA'  CREATIONDATE, "
                        strSQL = strSQL + "'NA'  UPDATEDATE "
                        strSQL = strSQL + "FROM "
                        strSQL = strSQL + "DUAL "

                        strSqlOutPut = strSqlOutPut + " UNION ALL " + strSQL
                    End If
                    strSqlOutPut = " SELECT * FROM ( " + strSqlOutPut + " ) DUAL ORDER BY GROUPNAME,GROUPDES"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, SBAConnection)
                Else
                    strSqlOutPut = " SELECT * FROM GROUPCASES WHERE GROUPID=0 "
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, SBAConnection)
                End If

                Return dtOutPut
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function
      
        
        Public Function GetBaseCaseGrpDet(ByVal Text As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT CASEID,(CASEID||':'||CASEDE1) CASEIDD,CASEDE1,REPLACE(CASEDE1,'''','&#')CASEDEAPOS1,CASEDE2,REPLACE(CASEDE2,'''','&#')CASEDEAPOS2,CASEDE3,REPLACE(CASEDE3,'''','&#')CASEDEAPOS3,CASEDES,CREATIONDATE,SERVERDATE,NAME,REPLACE(NAME,'''','&#')NAMEAPOS,SUPPLIERID,FILENAME,APPLICATION,REPLACE(APPLICATION,'''','&#')APPLICATIONAPOS "
                StrSql = StrSql + " FROM "
                StrSql = StrSql + " ( "
                StrSql = StrSql + "SELECT 0 CASEID,' Nothing'CASEDE1,'Selected'CASEDE2,''CASEDE3,''CASEDES,NULL CREATIONDATE,''SERVERDATE,''NAME,NULL SUPPLIERID,''FILENAME,''APPLICATION  FROM DUAL  "
                StrSql = StrSql + "UNION "

                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "BC.CASEID, "
                StrSql = StrSql + "BC.CASEDE1, "
                StrSql = StrSql + "BC.CASEDE2, "
                StrSql = StrSql + "BC.CASEDE3, "
                StrSql = StrSql + "('PACKAGE FORMAT= '||BC.CASEDE1||' UNIQUE FEATURES= '||BC.CASEDE2)CASEDES, "

                StrSql = StrSql + " BC.CREATIONDATE, "
                StrSql = StrSql + "CASE WHEN BC.CREATIONDATE-BC.SERVERDATE =0 THEN 'NA' ELSE to_char(BC.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE, "
                StrSql = StrSql + "SP.NAME,SP.SUPPLIERID,BC.FILENAME,BC.APPLICATION "

                StrSql = StrSql + "FROM GROUPCASES "
                StrSql = StrSql + "INNER JOIN GROUPS "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
                StrSql = StrSql + "RIGHT OUTER JOIN BASECASES BC "
                StrSql = StrSql + "ON BC.CASEID=GROUPCASES.CASEID "
                StrSql = StrSql + "LEFT OUTER JOIN SUPPLIER SP  "
                StrSql = StrSql + "ON BC.SUPPLIERID=SP.SUPPLIERID  "

                StrSql = StrSql + " ) "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + Text.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(CASEDE2),'#') LIKE '%" + Text.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(CASEDE3),'#') LIKE '%" + Text.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(APPLICATION),'#') LIKE '%" + Text.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(NAME),'#') LIKE '%" + Text.ToUpper() + "%' "
                StrSql = StrSql + "OR CASEID LIKE '%" + Text.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY CASEID,UPPER(CASEDE1),UPPER(CASEDE2) "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetPCaseGrpDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetBCaseDetailsByGroup(ByVal CaseDe1 As String, ByVal grpId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = StrSql + "SELECT DISTINCT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,(CASEID||':'||CASEDE1) CASEIDD,CASEDE1,REPLACE(CASEDE1,'''','&#')CASEDEAPOS1,CASEDE2,REPLACE(CASEDE2,'''','&#')CASEDEAPOS2,CASEDE3,REPLACE(CASEDE3,'''','&#')CASEDEAPOS3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,NAME,REPLACE(NAME,'''','&#')NAMEAPOS,APPLICATION,REPLACE(APPLICATION,'''','&#')APPLICATIONAPOS,SUPPLIERID,FILENAME "
                StrSql = StrSql + "FROM ("
                StrSql = StrSql + "SELECT 0 CASEID,''CASEDES,' Nothing'CASEDE1,'Selected'CASEDE2,''CASEDE3,''CASEDE,''NAME,''APPLICATION,NULL SUPPLIERID,''FILENAME  FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,NAME,APPLICATION,BC.SUPPLIERID,BC.FILENAME "
                StrSql = StrSql + "FROM BASECASES BC "
                StrSql = StrSql + "LEFT OUTER JOIN SUPPLIER SP "
                StrSql = StrSql + "ON BC.SUPPLIERID = SP.SUPPLIERID "
                StrSql = StrSql + "WHERE CASEID IN(SELECT CASEID FROM GROUPCASES WHERE GROUPID=" + grpId + " )) "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(APPLICATION),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR CASEID LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(NAME),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  CASEID,UPPER(CASEDE1),UPPER(CASEDE2) "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetBCaseDetailsByGroup:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPCaseGrpDetailsByType(ByVal UserID As String, ByVal CaseDe1 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  DISTINCT CASEID,(CASEID||':'||CASEDE1) CASEIDD, CASEDE1,REPLACE(CASEDE1,'''','&#')CASEDEAPOS1,CASEDE2,REPLACE(CASEDE2,'''','&#')CASEDEAPOS2,CASEDE3,REPLACE(CASEDE3,'''','&#')CASEDEAPOS3,CASEDES,CREATIONDATE,SERVERDATE,NAME,REPLACE(NAME,'''','&#')NAMEAPOS,SUPPLIERID,APPLICATION,REPLACE(APPLICATION,'''','&#')APPLICATIONAPOS,'' FILENAME  "
                StrSql = StrSql + " FROM "
                StrSql = StrSql + " ( "
                StrSql = StrSql + "SELECT ''USERNAME,0 CASEID,' Nothing'CASEDE1,'Selected'CASEDE2,''CASEDE3,''APPLICATION,''CASEDES,NULL CREATIONDATE,''SERVERDATE,''NAME,NULL SUPPLIERID FROM DUAL  "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT USERS.USERNAME, "
                StrSql = StrSql + "PC.CASEID, "
                StrSql = StrSql + "PC.CASEDE1, "
                StrSql = StrSql + "PC.CASEDE2, "
                StrSql = StrSql + "PC.CASEDE3, "
                StrSql = StrSql + "PC.APPLICATION, "
                StrSql = StrSql + "('PACKAGE FORMAT= '||PC.CASEDE1||' UNIQUE FEATURES= '||PC.CASEDE2)CASEDES, "

                StrSql = StrSql + " PC.CREATIONDATE, "
                StrSql = StrSql + "CASE WHEN PC.CREATIONDATE-PC.SERVERDATE =0 THEN 'NA' ELSE to_char(PC.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE, "
                StrSql = StrSql + " '' NAME, 0 SUPPLIERID "

                StrSql = StrSql + "FROM GROUPS "
                StrSql = StrSql + "INNER JOIN GROUPCASES "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "

                StrSql = StrSql + "RIGHT OUTER JOIN PERMISSIONSCASES PC "
                StrSql = StrSql + "ON PC.CASEID=GROUPCASES.CASEID "
                StrSql = StrSql + "INNER JOIN ECON.USERS ON USERS.USERID=PC.USERID "
                StrSql = StrSql + "WHERE PC.USERID =" + UserID.ToString() + " "

                StrSql = StrSql + " ) "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR CASEID LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(CASEDE3),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(APPLICATION),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY CASEID,UPPER(CASEDE1),UPPER(CASEDE2) "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetPCaseGrpDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetBaseCaseGrpDet(ByVal Text As String, ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID,(CASEID||':'||CASEDE1) CASEIDD,CASEDE1,CASEDE2,CASEDE3,CASEDES,CREATIONDATE,SERVERDATE,NAME,SUPPLIERID,FILENAME,APPLICATION "
                StrSql = StrSql + " FROM "
                StrSql = StrSql + " ( "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "BC.CASEID, "
                StrSql = StrSql + "BC.CASEDE1, "
                StrSql = StrSql + "BC.CASEDE2, "
                StrSql = StrSql + "BC.CASEDE3, "
                StrSql = StrSql + "('PACKAGE FORMAT= '||BC.CASEDE1||' UNIQUE FEATURES= '||BC.CASEDE2)CASEDES, "

                StrSql = StrSql + " BC.CREATIONDATE, "
                StrSql = StrSql + "CASE WHEN BC.CREATIONDATE-BC.SERVERDATE =0 THEN 'NA' ELSE to_char(BC.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE, "
                StrSql = StrSql + "SP.NAME,SP.SUPPLIERID,BC.FILENAME,BC.APPLICATION "

                StrSql = StrSql + "FROM GROUPCASES "
                StrSql = StrSql + "INNER JOIN GROUPS "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
                StrSql = StrSql + "RIGHT OUTER JOIN BASECASES BC "
                StrSql = StrSql + "ON BC.CASEID=GROUPCASES.CASEID "
                StrSql = StrSql + "LEFT OUTER JOIN SUPPLIER SP  "
                StrSql = StrSql + "ON BC.SUPPLIERID=SP.SUPPLIERID  "
                StrSql = StrSql + "WHERE BC.CASEID=" + CaseId + "  "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 0 CASEID,' Nothing'CASEDE1,'Selected'CASEDE2,''CASEDE3,''CASEDES,NULL CREATIONDATE,''SERVERDATE,''NAME,NULL SUPPLIERID,''FILENAME,''APPLICATION  FROM DUAL  "
                StrSql = StrSql + "UNION ALL "

                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "BC.CASEID, "
                StrSql = StrSql + "BC.CASEDE1, "
                StrSql = StrSql + "BC.CASEDE2, "
                StrSql = StrSql + "BC.CASEDE3, "
                StrSql = StrSql + "('PACKAGE FORMAT= '||BC.CASEDE1||' UNIQUE FEATURES= '||BC.CASEDE2)CASEDES, "

                StrSql = StrSql + " BC.CREATIONDATE, "
                StrSql = StrSql + "CASE WHEN BC.CREATIONDATE-BC.SERVERDATE =0 THEN 'NA' ELSE to_char(BC.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE, "
                StrSql = StrSql + "SP.NAME,SP.SUPPLIERID,BC.FILENAME,BC.APPLICATION "

                StrSql = StrSql + "FROM GROUPCASES "
                StrSql = StrSql + "INNER JOIN GROUPS "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
                StrSql = StrSql + "RIGHT OUTER JOIN BASECASES BC "
                StrSql = StrSql + "ON BC.CASEID=GROUPCASES.CASEID "
                StrSql = StrSql + "LEFT OUTER JOIN SUPPLIER SP  "
                StrSql = StrSql + "ON BC.SUPPLIERID=SP.SUPPLIERID  "
                StrSql = StrSql + "WHERE BC.CASEID <>" + CaseId + "  "
                StrSql = StrSql + " ) "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + Text.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(CASEDE2),'#') LIKE '%" + Text.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(CASEDE3),'#') LIKE '%" + Text.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(APPLICATION),'#') LIKE '%" + Text.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(NAME),'#') LIKE '%" + Text.ToUpper() + "%' "
                StrSql = StrSql + "OR CASEID LIKE '%" + Text.ToUpper() + "%' "


                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetBaseCaseGrpDet:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

#End Region

#Region "Material Selector"
        

        Public Function GetMaterialbyGroups(ByVal MatId As String, ByVal MatDe1 As String, ByVal MatGrp As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT MATERIALS.MATID,MATDE1 MATDES,MATDE2,   "
                StrSql = StrSql + "MATDE2 MATERIAL,GRADE.GRADENAME GRADE ,GRADE.GRADEID,GRADE.WEIGHT,MATERIALS.SG ,'' TECHNICALDES, "
                StrSql = StrSql + "0 INTVISC,0 RELVISC,0 VISC,0 MELTFRATE,0 MELTINDEX,'' PROCESSING,'' APPLICATION "
                StrSql = StrSql + "FROM MATERIALS "
                StrSql = StrSql + "INNER JOIN MATGRADE MG ON MG.MATID=MATERIALS.MATID  "
                StrSql = StrSql + "INNER JOIN GRADE "
                StrSql = StrSql + "ON GRADE.GRADEID=MG.GRADEID AND GRADE.ISDEFAULT='Y' "
                StrSql = StrSql + "WHERE MATERIALS.MATID = CASE WHEN " + MatId.ToString() + " = -1 THEN "
                StrSql = StrSql + "MATERIALS.MATID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + MatId.ToString() + " "
                StrSql = StrSql + "END "

                If MatGrp.Trim() = "ADHESIVE" Then
                    StrSql = StrSql + "AND (NVL(UPPER(MATDE2),'#') LIKE '%" + MatDe1.ToUpper() + "%' "
                    If IsNumeric(MatDe1) Then
                        StrSql = StrSql + "OR UPPER(SG) =" + MatDe1.ToUpper().Trim() + " "
                    End If
                    StrSql = StrSql + "OR UPPER(GRADENAME) LIKE '%" + MatDe1.ToUpper().Trim() + "%') "

                ElseIf MatGrp.Trim() = "ALUMINUM" Then
                    StrSql = StrSql + "AND (NVL(UPPER(MATDE1),'#') LIKE '%" + MatDe1.ToUpper() + "%' "
                    StrSql = StrSql + "OR NVL(UPPER(MATDE2),'#') LIKE '%" + MatDe1.ToUpper() + "%' "
                    If IsNumeric(MatDe1) Then
                        StrSql = StrSql + "OR UPPER(WEIGHT) =" + MatDe1.ToUpper().Trim() + ") "
                    Else
                        StrSql = StrSql + ") "
                    End If

                ElseIf MatGrp.Trim() = "STEEL" Then
                    StrSql = StrSql + "AND (NVL(UPPER(GRADENAME),'#') LIKE '%" + MatDe1.ToUpper() + "%' "
                    If IsNumeric(MatDe1) Then
                        StrSql = StrSql + "OR UPPER(SG) =" + MatDe1.ToUpper().Trim() + ") "
                    Else
                        StrSql = StrSql + ") "
                    End If

                Else
                    StrSql = StrSql + "AND (NVL(UPPER(MATDE1),'#') LIKE '" + MatDe1.ToUpper() + "%' "
                    StrSql = StrSql + "OR NVL(UPPER(MATDE2),'#') LIKE '" + MatDe1.ToUpper() + "%' "
                    If IsNumeric(MatDe1) Then
                        StrSql = StrSql + "OR UPPER(SG) =" + MatDe1.ToUpper().Trim() + " "
                    End If


                    StrSql = StrSql + "OR UPPER(GRADENAME) LIKE '%" + MatDe1.ToUpper().Trim() + "%') "
                End If

                StrSql = StrSql + "AND  UPPER(MATDE1) ='" + MatGrp.ToUpper().Trim() + "' "
                'StrSql = StrSql + "ORDER BY  MATDES"

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetMaterialbyGroups:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetMaterialGroups() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = " SELECT DISTINCT UPPER(TRIM(CATEGORY)) MATNAME FROM "
                StrSql = StrSql + "(SELECT UPPER(TRIM(CATEGORY)) CATEGORY FROM ADHESIVEPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT UPPER(TRIM(CATEGORY)) CATEGORY FROM ALUMINUMPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT UPPER(TRIM(CATEGORY)) CATEGORY FROM BASEFIBERPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT UPPER(TRIM(CATEGORY)) CATEGORY FROM COATINGSPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT UPPER(TRIM(CATEGORY)) CATEGORY FROM CONCENTRATEPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT UPPER(TRIM(CATEGORY)) CATEGORY FROM FILMPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT UPPER(TRIM(CATEGORY)) CATEGORY FROM GLASSPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT UPPER(TRIM(CATEGORY)) CATEGORY FROM INKPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT UPPER(TRIM(CATEGORY)) CATEGORY FROM NONWOVENSPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT UPPER(TRIM(CATEGORY)) CATEGORY FROM PAPERBOARDPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT UPPER(TRIM(CATEGORY)) CATEGORY FROM PAPERPROP "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT UPPER(TRIM(CATEGORY)) CATEGORY FROM RESINPROP  "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT UPPER(TRIM(CATEGORY)) CATEGORY FROM SHEETPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT UPPER(TRIM(CATEGORY)) CATEGORY FROM STEELPROP )  "
                StrSql = StrSql + "WHERE CATEGORY NOT IN (' ') "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetMaterialGroups:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetResinMaterial(ByVal MatId As String, ByVal MatDe1 As String, ByVal MatGrp As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT MATERIALS.MATID,CATEGORY MATDES,MATERIAL MATDE2,(MATERIAL||','||TECHNICALDES) MATERIAL,GRADE,GRADE.GRADEID,TECHNICALDES, "
                StrSql = StrSql + "INTVISC,RELVISC,VISC,MELTFRATE,MELTINDEX,PROCESSING,APPLICATION "
                StrSql = StrSql + "FROM MATERIALS "
                StrSql = StrSql + "INNER JOIN RESINPROP RP ON RP.MATID=MATERIALS.MATID "
                StrSql = StrSql + "INNER JOIN MATGRADE MG ON MG.MATID=MATERIALS.MATID "
                StrSql = StrSql + "INNER JOIN GRADE "
                StrSql = StrSql + "ON GRADE.GRADEID=MG.GRADEID AND GRADE.ISDEFAULT='Y' "
                StrSql = StrSql + "WHERE MATERIALS.MATID = CASE WHEN " + MatId.ToString() + " = -1 THEN "
                StrSql = StrSql + "MATERIALS.MATID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + MatId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND (NVL(UPPER(MATDE1),'#') LIKE '" + MatDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDE2),'#') LIKE '" + MatDe1.ToUpper() + "%') "
                StrSql = StrSql + "ORDER BY  MATDES"

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetResinMaterial:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region
#Region "MaterialGroup Popup"
        Public Function GetResinMaterialSubLayer(ByVal MatId As String, ByVal MatDe1 As String, ByVal MatGrp As String, ByVal MBlend As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MATID, MATDES,MATDE2,MATERIAL,DENSITY,MELTINDEX,CATEGORY, "
                StrSql = StrSql + "MELTFRATE,PROCESS,POLYMERSTRUCT,POLYMERDESC,VISC,GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM ( "

                StrSql = StrSql + "SELECT 0 MATID,'Nothing' MATDES,'Selected' MATDE2,NULL MATERIAL,NULL DENSITY,NULL MELTINDEX,NULL CATEGORY, "
                StrSql = StrSql + "NULL MELTFRATE,NULL PROCESS,NULL POLYMERSTRUCT,NULL POLYMERDESC,NULL VISC,'' GRADECOL,'' MOUSEOVERCOL "
                StrSql = StrSql + "FROM RESINPROP "
                StrSql = StrSql + "UNION "

                StrSql = StrSql + "SELECT MATID,CATEGORY MATDES,MATERIAL MATDE2,MATERIAL,DENSITY,MELTINDEX,CATEGORY, "
                StrSql = StrSql + "MELTFRATE,PROCESS,POLYMERSTRUCT,POLYMERDESC,VISC,GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM RESINPROP "
                StrSql = StrSql + "WHERE  TRIM(UPPER(CATEGORY)) ='" + MatGrp.ToUpper() + "' "
                If MBlend = "BLEND" Then
                    StrSql = StrSql + " AND MATID NOT BETWEEN 501 AND 505 "
                End If
                StrSql = StrSql + ") "

                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "NVL(UPPER(MATID),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDES),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDE2),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(DENSITY),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MELTINDEX),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MELTFRATE),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(PROCESS),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(POLYMERSTRUCT),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(POLYMERDESC),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(VISC),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "ORDER BY  MATID,MATDES,MATDE2"


                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetResinMaterialbyGroups(ByVal MatId As String, ByVal MatDe1 As String, ByVal MatGrp As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MATID, MATDES,MATDE2,MATERIAL,DENSITY,MELTINDEX,CATEGORY, "
                StrSql = StrSql + "MELTFRATE,PROCESS,POLYMERSTRUCT,POLYMERDESC,VISC,GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM ( "

                StrSql = StrSql + "SELECT 0 MATID,'Nothing' MATDES,'Selected' MATDE2,NULL MATERIAL,NULL DENSITY,NULL MELTINDEX,NULL CATEGORY, "
                StrSql = StrSql + "NULL MELTFRATE,NULL PROCESS,NULL POLYMERSTRUCT,NULL POLYMERDESC,NULL VISC,'' GRADECOL,'' MOUSEOVERCOL "
                StrSql = StrSql + "FROM RESINPROP "
                StrSql = StrSql + "UNION "

                StrSql = StrSql + "SELECT MATID,CATEGORY MATDES,MATERIAL MATDE2,MATERIAL,DENSITY,MELTINDEX,CATEGORY, "
                StrSql = StrSql + "MELTFRATE,PROCESS,POLYMERSTRUCT,POLYMERDESC,VISC,GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM RESINPROP "
                StrSql = StrSql + "WHERE  TRIM(UPPER(CATEGORY)) ='" + MatGrp.ToUpper() + "' "
                StrSql = StrSql + ") "

                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "NVL(UPPER(MATID),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDES),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDE2),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(DENSITY),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MELTINDEX),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MELTFRATE),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(PROCESS),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(POLYMERSTRUCT),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(POLYMERDESC),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(VISC),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "ORDER BY  MATID,MATDES,MATDE2"


                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetFilmMaterialbyGroups(ByVal MatId As String, ByVal MatDe1 As String, ByVal MatGrp As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim SBAConnection As String = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
            Try
                StrSql = "SELECT MATID,MATDES,MATDE2,MATERIAL, "
                StrSql = StrSql + "SUBLAYERS,PRESIDE,PRETYPE,MODIFIERS,OUTCOATING,PRODOUTCOATING, "
                StrSql = StrSql + "OXYGENBARR,MOISTUREBARR,DENSITY,APPLICATION,GRADECOL,MOUSEOVERCOL  "
                StrSql = StrSql + "FROM ( "

                StrSql = StrSql + "SELECT 0 MATID,'Nothing' MATDES,'Selected' MATDE2,NULL MATERIAL, "
                StrSql = StrSql + "NULL SUBLAYERS,NULL PRESIDE,NULL PRETYPE,NULL MODIFIERS,NULL OUTCOATING,NULL PRODOUTCOATING, "
                StrSql = StrSql + "NULL OXYGENBARR,NULL MOISTUREBARR,NULL DENSITY,NULL APPLICATION,NULL GRADECOL,NULL MOUSEOVERCOL  "
                StrSql = StrSql + "FROM DUAL UNION "


                StrSql = StrSql + "SELECT MATID,CATEGORY MATDES,MATERIAL MATDE2,MATERIAL, "
                StrSql = StrSql + "SUBLAYERS,PRESIDE,PRETYPE,MODIFIERS,OUTCOATING,PRODOUTCOATING, "
                StrSql = StrSql + "OXYGENBARR,MOISTUREBARR,DENSITY,APPLICATION,GRADECOL,MOUSEOVERCOL  "
                StrSql = StrSql + "FROM FILMPROP "
                StrSql = StrSql + "WHERE  TRIM(UPPER(CATEGORY)) ='" + MatGrp.ToUpper() + "') "

                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "NVL(UPPER(MATID),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDES),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDE2),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(SUBLAYERS),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(PRESIDE),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(PRETYPE),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MODIFIERS),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(OUTCOATING),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(PRODOUTCOATING),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(OXYGENBARR),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MOISTUREBARR),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(DENSITY),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(APPLICATION),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "

                StrSql = StrSql + "ORDER BY  MATID,MATDES,MATDE2 "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetFilmMaterialbyGroups:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetAdhesiveMaterialbyGroups(ByVal MatId As String, ByVal MatDe1 As String, ByVal MatGrp As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MATID,MATDES,MATDE2,MATERIAL,DILUENT,CURING,  "
                StrSql = StrSql + "RESINFAMILY,REACTIVE,GRADECOL,MOUSEOVERCOL  "
                StrSql = StrSql + "FROM ( "

                StrSql = StrSql + "SELECT 0 MATID,'Nothing' MATDES,'Selected' MATDE2,NULL MATERIAL,NULL DILUENT,NULL CURING,  "
                StrSql = StrSql + "NULL RESINFAMILY,NULL REACTIVE, NULL GRADECOL,NULL MOUSEOVERCOL  "
                StrSql = StrSql + "FROM DUAL UNION "

                StrSql = StrSql + "SELECT MATID,CATEGORY MATDES,MATERIAL MATDE2,MATERIAL,DILUENT,CURING,  "
                StrSql = StrSql + "RESINFAMILY,REACTIVE,GRADECOL,MOUSEOVERCOL  "
                StrSql = StrSql + "FROM ADHESIVEPROP "
                StrSql = StrSql + "WHERE  TRIM(UPPER(CATEGORY)) ='" + MatGrp.ToUpper() + "' ) "

                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "NVL(UPPER(MATID),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDES),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDE2),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(DILUENT),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(CURING),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(RESINFAMILY),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(REACTIVE),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "


                StrSql = StrSql + "ORDER BY  MATID,MATDES,MATDE2 "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetAdhesiveMaterialNew(ByVal MatId As String, ByVal MatDe1 As String, ByVal MatGrp As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MATID,MATDES,MATDE2,MATERIAL,DILUENT,CURING,  "
                StrSql = StrSql + "RESINFAMILY,GRADECOL,MOUSEOVERCOL  "
                StrSql = StrSql + "FROM ( "

                StrSql = StrSql + "SELECT 0 MATID,'Nothing' MATDES,'Selected' MATDE2,NULL MATERIAL,NULL DILUENT,NULL CURING,  "
                StrSql = StrSql + "NULL RESINFAMILY,NULL GRADECOL,NULL MOUSEOVERCOL  "
                StrSql = StrSql + "FROM DUAL UNION "

                StrSql = StrSql + "SELECT MATID,CATEGORY MATDES,MATERIAL MATDE2,MATERIAL,DILUENT,CURING,  "
                StrSql = StrSql + "RESINFAMILY,GRADECOL,MOUSEOVERCOL  "
                StrSql = StrSql + "FROM ADHESIVEPROP "
                StrSql = StrSql + "WHERE  TRIM(UPPER(CATEGORY)) ='" + MatGrp.ToUpper() + "' ) "

                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "NVL(UPPER(MATID),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDES),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDE2),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(DILUENT),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(CURING),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(RESINFAMILY),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "ORDER BY  MATID,MATDES,MATDE2 "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetAdhesiveMaterialNew:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetAluminMaterialbyGroups(ByVal MatId As String, ByVal MatDe1 As String, ByVal MatGrp As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT MATID, MATDES,MATDE2,MATERIAL,TECHDESC,ALLOY,TEMPER, "
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM ( "

                StrSql = StrSql + "SELECT 0 MATID,'Nothing' MATDES,'Selected' MATDE2,NULL MATERIAL,NULL TECHDESC,NULL ALLOY,NULL TEMPER, "
                StrSql = StrSql + "NULL GRADECOL,NULL MOUSEOVERCOL "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "

                StrSql = StrSql + "SELECT MATID,CATEGORY MATDES,MATERIAL MATDE2,MATERIAL,TECHDESC,ALLOY,TEMPER, "
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM ALUMINUMPROP "
                StrSql = StrSql + "WHERE  TRIM(UPPER(CATEGORY)) ='" + MatGrp.ToUpper() + "' ) "

                StrSql = StrSql + "WHERE  "
                StrSql = StrSql + "NVL(UPPER(MATID),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDES),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDE2),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(TECHDESC),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(ALLOY),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(TEMPER),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "

                StrSql = StrSql + "ORDER BY  MATID,MATDES,MATDE2"

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetAluminMaterialNew(ByVal MatId As String, ByVal MatDe1 As String, ByVal MatGrp As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT MATID, MATDES,MATDE2,MATERIAL,TECHDESC,"
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM ( "

                StrSql = StrSql + "SELECT 0 MATID,'Nothing' MATDES,'Selected' MATDE2,NULL MATERIAL,NULL TECHDESC, "
                StrSql = StrSql + "NULL GRADECOL,NULL MOUSEOVERCOL "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "

                StrSql = StrSql + "SELECT MATID,CATEGORY MATDES,MATERIAL MATDE2,MATERIAL,TECHDESC, "
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM ALUMINUMPROP "
                StrSql = StrSql + "WHERE  TRIM(UPPER(CATEGORY)) ='" + MatGrp.ToUpper() + "' ) "

                StrSql = StrSql + "WHERE  "
                StrSql = StrSql + "NVL(UPPER(MATID),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDES),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDE2),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(TECHDESC),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "

                StrSql = StrSql + "ORDER BY  MATID,MATDES,MATDE2"

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetAluminMaterialNew:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function


        Public Function GetBaseFMaterialbyGroups(ByVal MatId As String, ByVal MatDe1 As String, ByVal MatGrp As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MATID,MATDES,MATDE2,DESCRIPTION,DENSITY, "
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM ( "

                StrSql = StrSql + "SELECT 0 MATID,'Nothing' MATDES,'Selected' MATDE2,NULL DESCRIPTION,NULL DENSITY, "
                StrSql = StrSql + "NULL GRADECOL,NULL MOUSEOVERCOL "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "

                StrSql = StrSql + "SELECT MATID,CATEGORY MATDES,DESCRIPTION MATDE2,DESCRIPTION,DENSITY, "
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM BASEFIBERPROP "
                StrSql = StrSql + "WHERE  TRIM(UPPER(CATEGORY)) ='" + MatGrp.ToUpper() + "' ) "

                StrSql = StrSql + "WHERE NVL(UPPER(MATID),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "

                StrSql = StrSql + "OR NVL(UPPER(MATDES),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDE2),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "

                StrSql = StrSql + "OR NVL(UPPER(DENSITY),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "

                StrSql = StrSql + "ORDER BY MATID,MATDES,MATDE2 "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCoatingMaterialbyGroups(ByVal MatId As String, ByVal MatDe1 As String, ByVal MatGrp As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MATID,MATDES,MATDE2,MATERIAL,DENSITY,DILUENT,RESINFAMILY,TYPE,SUBSTRATE, "
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM (  "

                StrSql = StrSql + "SELECT 0 MATID,'Nothing' MATDES,'Selected' MATDE2,NULL MATERIAL,NULL DENSITY,NULL DILUENT,NULL RESINFAMILY,NULL TYPE,NULL SUBSTRATE, "
                StrSql = StrSql + "NULL GRADECOL,NULL MOUSEOVERCOL "
                StrSql = StrSql + "FROM DUAL  "
                StrSql = StrSql + "UNION  "

                StrSql = StrSql + "SELECT MATID,CATEGORY MATDES,MATERIAL MATDE2,MATERIAL,DENSITY,DILUENT,RESINFAMILY,TYPE,SUBSTRATE, "
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM COATINGSPROP  "
                StrSql = StrSql + "WHERE  TRIM(UPPER(CATEGORY)) ='" + MatGrp.ToUpper() + "' )"

                StrSql = StrSql + "WHERE NVL(UPPER(MATID),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDES),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDE2),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(DENSITY),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(DILUENT),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(RESINFAMILY),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(TYPE),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(SUBSTRATE),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "

                StrSql = StrSql + "ORDER BY  MATID,MATDES,MATDE2"

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetConcentrateMaterialbyGroups(ByVal MatId As String, ByVal MatDe1 As String, ByVal MatGrp As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MATID,MATDES,MATDE2,DESCRIPTION,FUNCTION, "
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM ( "

                StrSql = StrSql + "SELECT 0 MATID,'Nothing' MATDES,'Selected' MATDE2,NULL DESCRIPTION,NULL FUNCTION, "
                StrSql = StrSql + "NULL GRADECOL,NULL MOUSEOVERCOL "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "


                StrSql = StrSql + "SELECT MATID,CATEGORY MATDES,DESCRIPTION MATDE2,DESCRIPTION,FUNCTION, "
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM CONCENTRATEPROP "
                StrSql = StrSql + "WHERE  TRIM(UPPER(CATEGORY)) ='" + MatGrp.ToUpper() + "' ) "

                StrSql = StrSql + "WHERE NVL(UPPER(MATID),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDES),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDE2),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(DESCRIPTION),'#') LIKE '" + MatDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(FUNCTION),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "

                StrSql = StrSql + "ORDER BY  MATID,MATDES,MATDE2 "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetGlassMaterialbyGroups(ByVal MatId As String, ByVal MatDe1 As String, ByVal MatGrp As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = StrSql + "SELECT MATID,MATDES,MATDE2,MATERIAL,DENSITY,PARTICLESIZE, "
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM (  "

                StrSql = StrSql + "SELECT 0 MATID,'Nothing' MATDES,'Selected' MATDE2,NULL MATERIAL,NULL DENSITY,NULL PARTICLESIZE, "
                StrSql = StrSql + "NULL GRADECOL,NULL MOUSEOVERCOL "
                StrSql = StrSql + "FROM DUAL UNION  "


                StrSql = StrSql + "SELECT MATID,CATEGORY MATDES,MATERIAL MATDE2,MATERIAL,DENSITY,PARTICLESIZE, "
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM GLASSPROP  "
                StrSql = StrSql + "WHERE TRIM(UPPER(CATEGORY)) ='" + MatGrp.ToUpper() + "') "

                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "NVL(UPPER(MATID),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDES),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDE2),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(DENSITY),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(PARTICLESIZE),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "

                StrSql = StrSql + "ORDER BY  MATID,MATDES,MATDE2 "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetInkMaterialbyGroups(ByVal MatId As String, ByVal MatDe1 As String, ByVal MatGrp As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MATID,MATDES,MATDE2,MATERIAL,SUBSTRATES,CURING,DILUENT,RESINFAMILY, "
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM ( "

                StrSql = StrSql + "SELECT 0 MATID,'Nothing' MATDES,'Selected' MATDE2,NULL MATERIAL,NULL SUBSTRATES,NULL CURING,NULL DILUENT,NULL RESINFAMILY, "
                StrSql = StrSql + "NULL GRADECOL,NULL MOUSEOVERCOL "
                StrSql = StrSql + "FROM DUAL UNION "

                StrSql = StrSql + "SELECT MATID,CATEGORY MATDES,MATERIAL MATDE2,MATERIAL,SUBSTRATES,CURING,DILUENT,RESINFAMILY, "
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM INKPROP "
                StrSql = StrSql + "WHERE  TRIM(UPPER(CATEGORY)) ='" + MatGrp.ToUpper() + "' ) "

                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "NVL(UPPER(MATID),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDES),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDE2),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(SUBSTRATES),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(CURING),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(DILUENT),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(RESINFAMILY),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "ORDER BY  MATID,MATDES,MATDE2"

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetInkMaterialbyGroups:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetNonWMaterialbyGroups(ByVal MatId As String, ByVal MatDe1 As String, ByVal MatGrp As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MATID,MATDES,MATDE2,MATERIAL,DENSITY,THICKNESS, "
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM ( "

                StrSql = StrSql + "SELECT 0 MATID,'Nothing' MATDES,'Selected' MATDE2,NULL MATERIAL,NULL DENSITY,NULL THICKNESS, "
                StrSql = StrSql + "NULL GRADECOL,NULL MOUSEOVERCOL "
                StrSql = StrSql + "FROM DUAL UNION "

                StrSql = StrSql + "SELECT MATID,CATEGORY MATDES,MATERIAL MATDE2,MATERIAL,DENSITY,THICKNESS, "
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM NONWOVENSPROP "
                StrSql = StrSql + "WHERE  TRIM(UPPER(CATEGORY)) ='" + MatGrp.ToUpper() + "') "

                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "NVL(UPPER(MATID),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDES),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDE2),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(DENSITY),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(THICKNESS),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "

                StrSql = StrSql + "ORDER BY  MATID,MATDES,MATDE2 "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPaperMaterialbyGroups(ByVal MatId As String, ByVal MatDe1 As String, ByVal MatGrp As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MATID, MATDES, MATDE2,DESCRIPTION,TECHDESCRIPTION,RECYCLE, "
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM (  "

                StrSql = StrSql + "SELECT 0 MATID,'Nothing' MATDES,'Selected' MATDE2,NULL DESCRIPTION,NULL TECHDESCRIPTION,NULL RECYCLE, "
                StrSql = StrSql + "NULL GRADECOL,NULL MOUSEOVERCOL "
                StrSql = StrSql + "FROM DUAL UNION  "


                StrSql = StrSql + "SELECT MATID,CATEGORY MATDES,DESCRIPTION MATDE2,DESCRIPTION,TECHDESCRIPTION,RECYCLE, "
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM PAPERPROP  "
                StrSql = StrSql + "WHERE  TRIM(UPPER(CATEGORY)) ='" + MatGrp.ToUpper() + "' ) "

                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "NVL(UPPER(MATID),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDES),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDE2),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(DESCRIPTION),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(TECHDESCRIPTION),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(RECYCLE),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "

                StrSql = StrSql + "ORDER BY  MATID,MATDES,MATDE2 "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPaperBMaterialbyGroups(ByVal MatId As String, ByVal MatDe1 As String, ByVal MatGrp As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MATID,MATDES,MATDE2,DESCRIPTION,TECHDESCRIPTION,RECYCLE, "
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM ( "

                StrSql = StrSql + "SELECT 0 MATID,'Nothing' MATDES,'Selected' MATDE2,NULL DESCRIPTION,NULL TECHDESCRIPTION,NULL RECYCLE, "
                StrSql = StrSql + "NULL GRADECOL,NULL MOUSEOVERCOL "
                StrSql = StrSql + "FROM DUAL UNION "

                StrSql = StrSql + "SELECT MATID,CATEGORY MATDES,DESCRIPTION MATDE2,DESCRIPTION,TECHDESCRIPTION,RECYCLE, "
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM PAPERBOARDPROP "
                StrSql = StrSql + "WHERE  TRIM(UPPER(CATEGORY)) ='" + MatGrp.ToUpper() + "') "

                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "NVL(UPPER(MATID),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDES),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDE2),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "

                StrSql = StrSql + "OR NVL(UPPER(DESCRIPTION),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(TECHDESCRIPTION),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(RECYCLE),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "

                StrSql = StrSql + "ORDER BY  MATID,MATDES,MATDE2 "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetSheetMaterialbyGroups(ByVal MatId As String, ByVal MatDe1 As String, ByVal MatGrp As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MATID,MATDES,MATDE2,DESCRIPTION,DENSITY, "
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM ( "

                StrSql = StrSql + "SELECT 0 MATID,'Nothing' MATDES,'Selected' MATDE2,NULL DESCRIPTION,NULL DENSITY, "
                StrSql = StrSql + "NULL GRADECOL,NULL MOUSEOVERCOL "
                StrSql = StrSql + "FROM DUAL UNION  "

                StrSql = StrSql + "SELECT MATID,CATEGORY MATDES,DESCRIPTION MATDE2,DESCRIPTION,DENSITY, "
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM SHEETPROP "
                StrSql = StrSql + "WHERE  TRIM(UPPER(CATEGORY)) ='" + MatGrp.ToUpper() + "' ) "

                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "NVL(UPPER(MATID),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDES),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDE2),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(DESCRIPTION),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(DENSITY),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "

                StrSql = StrSql + "ORDER BY  MATID,MATDES,MATDE2 "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetSteelMaterialbyGroups(ByVal MatId As String, ByVal MatDe1 As String, ByVal MatGrp As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MATID,MATDES,MATDE2,DESCRIPTION,DENSITY,"
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM ( "

                StrSql = StrSql + "SELECT 0 MATID,'Nothing' MATDES,'Selected' MATDE2,NULL DESCRIPTION,NULL DENSITY,"
                StrSql = StrSql + "NULL GRADECOL,NULL MOUSEOVERCOL "
                StrSql = StrSql + "FROM DUAL UNION "

                StrSql = StrSql + "SELECT MATID,CATEGORY MATDES,DESCRIPTION MATDE2,DESCRIPTION,DENSITY,"
                StrSql = StrSql + "GRADECOL,MOUSEOVERCOL "
                StrSql = StrSql + "FROM STEELPROP "
                StrSql = StrSql + "WHERE  TRIM(UPPER(CATEGORY)) ='" + MatGrp.ToUpper() + "' )"

                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "NVL(UPPER(MATID),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDES),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(MATDE2),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(DENSITY),'#') LIKE '%" + MatDe1.ToUpper().Trim() + "%' "

                StrSql = StrSql + "ORDER BY  MATID,MATDES,MATDE2 "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region


        Public Function GetFlagSponsors() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT "
                StrSql = StrSql + "SUPPLIER.SUPPLIERID,NAME,  "
                StrSql = StrSql + "EMAILADDRESS,IMAGE,IMAGEURL  "
                StrSql = StrSql + "FROM FLAGSUPPLIERS "
                StrSql = StrSql + "INNER JOIN SUPPLIER ON SUPPLIER.SUPPLIERID=FLAGSUPPLIERS.SUPPLIERID "
                StrSql = StrSql + "ORDER BY dbms_random.value "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SAssist:GetSupplierMaterialSEQ:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetFlagTempQuery(ByVal SessionId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT "
                StrSql = StrSql + "SESSIONID,SUPPLIERIDS,SPONSORFLAGS,SPONSORURLS  "
                StrSql = StrSql + "FROM FLAGQUERYTEMP "
                StrSql = StrSql + "WHERE SESSIONID='" + SessionId + "'"

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SAssist:GetSupplierMaterialSEQ:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetMatSupplier(ByVal MatId As Integer, ByVal flag As Boolean) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty

            Try
                If MatId <> "0" Then

                     StrSql = " SELECT     "
                    StrSql = StrSql + "DISTINCT  "
                    StrSql = StrSql + "SP.SUPPLIERID,  "
                    StrSql = StrSql + "SP.NAME,   "
                    StrSql = StrSql + "SP.EMAILADDRESS  "
                    StrSql = StrSql + " FROM    "
                    StrSql = StrSql + "SUPPLIER SP    "
                    StrSql = StrSql + "LEFT OUTER JOIN MATSUPPLIERGRADE GS   "
                    StrSql = StrSql + "ON SP.SUPPLIERID=GS.SUPPLIERID   "
                    StrSql = StrSql + " INNER JOIN MATERIALSUPPLIER MS "
                    StrSql = StrSql + "ON MS.SUPPLIERID=GS.SUPPLIERID "
                    StrSql = StrSql + " LEFT OUTER JOIN GRADES GR  "
                    StrSql = StrSql + "ON GR.GRADEID=GS.GRADEID  "
                    StrSql = StrSql + "WHERE GS.MATID = " + MatId.ToString() + " "
                    If flag = True Then
                        StrSql = StrSql + " AND MS.ISSPONSOR='Y' "
                    Else
                        StrSql = StrSql + " AND MS.ISSPONSOR='N' "
                    End If
                    StrSql = StrSql + "ORDER BY NAME ASC "
                End If

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetSponsorSupplier:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSponsorUsers(ByVal text As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT SP.SUPPLIERID, "
                StrSql = StrSql + "SP.NAME "
                StrSql = StrSql + "FROM SUPPLIER SP  "
                'StrSql = StrSql + "INNER JOIN SUPPLIERUSER SU "
                'StrSql = StrSql + "ON SP.SUPPLIERID=SU.SUPPLIERID "
                StrSql = StrSql + "WHERE UPPER(NAME) LIKE '%" + text.ToUpper.ToString() + "%'  ORDER BY UPPER(NAME) "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetSponsorUsers:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetGradeDesc(ByVal matId As String, ByVal SupId As String, ByVal GradeId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DESCRIPTION FROM MATSUPPLIERGRADE  "
                StrSql = StrSql + "WHERE MATID= " + matId.ToString() + " "
                StrSql = StrSql + "AND SUPPLIERID='" + SupId.ToString() + "'"
                StrSql = StrSql + "AND GRADEID=" + GradeId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SAssist:GetSupplierMaterial:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetLogUserDetails(ByVal UserID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  UC.FIRSTNAME,UC.LASTNAME, "
                StrSql = StrSql + "(CASE WHEN COMPANY.COMPANYNAME='' THEN '' ELSE COMPANY.COMPANYNAME END)COMPANY "
                StrSql = StrSql + "FROM ECON.USERS USR "
                StrSql = StrSql + "INNER JOIN ECON.USERCONTACTS UC "
                StrSql = StrSql + "ON USR.USERID=UC.USERID "
                StrSql = StrSql + "LEFT OUTER JOIN SHOPPING.COMPANY "
                StrSql = StrSql + "ON COMPANY.COMPANYID=USR.COMPANYID "
                StrSql = StrSql + "WHERE USR.USERID='" + UserID + "' "
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:GetEmailConfigDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

#Region "Comparison"

        Public Function GetStructures(ByVal UserId As String, ByVal CompFlag As String) As DataTable
            Dim Dts As New DataTable
            Try
                Dim odbUtil As New DBUtil()

                Dim StrSql As String = "SELECT CASEID,CASEDE1,('STRUCTURE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE FROM PERMISSIONSCASES WHERE USERID=" + UserId.ToString() + ""
                StrSql = StrSql + "UNION SELECT CASEID,CASEDE1,('STRUCTURE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE  FROM BASECASES  "
                If CompFlag = "Y" Then
                    StrSql = StrSql + "UNION SELECT CASEID,CASEDE1,('STRUCTURE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE  FROM COMPANYCASES WHERE LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserId.ToString() + ") ORDER BY caseDE1"

                End If

                Dts = odbUtil.FillDataTable(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function

        Public Function GetSavedCaseAsperUser(ByVal UserId As String, ByVal AssumptionId As String) As DataSet
            Dim Dts As New DataSet
            Try
                Dim StrSqlSaved As String = ""
                Dim odbUtil As New DBUtil()


                StrSqlSaved = "SELECT ASSUMPTIONID,DESCRIPTION,REPLACE(CASEIDS,', 0','')CASEIDS,DES,REPLACE(DESCRIPTION,'''','&#')DESCRIPTIONAPOS1 FROM ( "
                StrSqlSaved = StrSqlSaved + "SELECT distinct AssumptionId,DESCRIPTION,"
                StrSqlSaved = StrSqlSaved + "(nvl(STRUCT1,0) || ', '||"
                StrSqlSaved = StrSqlSaved + "nvl(STRUCT2,0) || ', '||"
                StrSqlSaved = StrSqlSaved + "nvl(STRUCT3,0) || ', '||"
                StrSqlSaved = StrSqlSaved + "nvl(STRUCT4,0) || ', '||"
                StrSqlSaved = StrSqlSaved + "nvl(STRUCT5,0) || ', '||"
                StrSqlSaved = StrSqlSaved + "nvl(STRUCT6,0) || ', '||"
                StrSqlSaved = StrSqlSaved + "nvl(STRUCT7,0) || ', '||"
                StrSqlSaved = StrSqlSaved + "nvl(STRUCT8,0) || ', '||"
                StrSqlSaved = StrSqlSaved + "nvl(STRUCT9,0) || ', '||"
                StrSqlSaved = StrSqlSaved + "nvl(STRUCT10,0))CaseIds, "
                StrSqlSaved = StrSqlSaved + "(AssumptionId ||' - ' ||"
                StrSqlSaved = StrSqlSaved + "DESCRIPTION || ', Cases: ' ||"
                StrSqlSaved = StrSqlSaved + "STRUCT1 || ' '||"
                StrSqlSaved = StrSqlSaved + "STRUCT2 || ' '||"
                StrSqlSaved = StrSqlSaved + "STRUCT3 || ' '||"
                StrSqlSaved = StrSqlSaved + "STRUCT4 || ' '||"
                StrSqlSaved = StrSqlSaved + "STRUCT5 || ' '||"
                StrSqlSaved = StrSqlSaved + "STRUCT6 || ' '||"
                StrSqlSaved = StrSqlSaved + "STRUCT7 || ' '||"
                StrSqlSaved = StrSqlSaved + "STRUCT8 || ' '||"
                StrSqlSaved = StrSqlSaved + "STRUCT9 || ' '||"
                StrSqlSaved = StrSqlSaved + "STRUCT10 "
                StrSqlSaved = StrSqlSaved + ")As Des  FROM Assumptions "
                StrSqlSaved = StrSqlSaved + " WHERE USERID=" + UserId.ToString() + " "
                StrSqlSaved = StrSqlSaved + "AND AssumptionId = CASE WHEN " + AssumptionId.ToString() + " = -1 THEN "
                StrSqlSaved = StrSqlSaved + "AssumptionId "
                StrSqlSaved = StrSqlSaved + "ELSE "
                StrSqlSaved = StrSqlSaved + "" + AssumptionId.ToString() + " "
                StrSqlSaved = StrSqlSaved + "END "
                StrSqlSaved = StrSqlSaved + " ) "
                Dts = odbUtil.FillDataSet(StrSqlSaved, SBAConnection)
            Catch ex As Exception
                Throw
            End Try
            Return Dts
        End Function

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
                StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=STRUCT1)>0 THEN 0 ELSE NVL(STRUCT1,0) END ) "
                StrSql = StrSql + "FROM ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=STRUCT2)>0 THEN 0 ELSE NVL(STRUCT2,0) END ) "
                StrSql = StrSql + "FROM ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=STRUCT3)>0 THEN 0 ELSE NVL(STRUCT3,0) END ) "
                StrSql = StrSql + "FROM  ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=STRUCT4)>0 THEN 0 ELSE NVL(STRUCT4,0) END ) "
                StrSql = StrSql + "FROM  ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=STRUCT5)>0 THEN 0 ELSE NVL(STRUCT5,0) END ) "
                StrSql = StrSql + "FROM  ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT   (CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=STRUCT6)>0 THEN 0 ELSE NVL(STRUCT6,0) END ) "
                StrSql = StrSql + "FROM  ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT   (CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=STRUCT7)>0 THEN 0 ELSE NVL(STRUCT7,0) END ) "
                StrSql = StrSql + "FROM  ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT   (CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=STRUCT8)>0 THEN 0 ELSE NVL(STRUCT8,0) END ) "
                StrSql = StrSql + "FROM  ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT  (CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=STRUCT9)>0 THEN 0 ELSE NVL(STRUCT9,0) END ) "
                StrSql = StrSql + "FROM  ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT  (CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=STRUCT10)>0 THEN 0 ELSE NVL(STRUCT10,0) END )  AS C "
                StrSql = StrSql + "FROM ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                StrSql = StrSql + ")"
                'Company Lib Changes:
                StrSql = StrSql + " UNION "
                StrSql = StrSql + "SELECT DISTINCT(P.CASEID) FROM COMPANYCASES P  "
                StrSql = StrSql + "WHERE P.CASEID IN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=STRUCT1)>0 THEN 0 ELSE NVL(STRUCT1,0) END ) "
                StrSql = StrSql + "FROM ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=STRUCT2)>0 THEN 0 ELSE NVL(STRUCT2,0) END ) "
                StrSql = StrSql + "FROM ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=STRUCT3)>0 THEN 0 ELSE NVL(STRUCT3,0) END ) "
                StrSql = StrSql + "FROM  ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=STRUCT4)>0 THEN 0 ELSE NVL(STRUCT4,0) END ) "
                StrSql = StrSql + "FROM  ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=STRUCT5)>0 THEN 0 ELSE NVL(STRUCT5,0) END ) "
                StrSql = StrSql + "FROM  ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT   (CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=STRUCT6)>0 THEN 0 ELSE NVL(STRUCT6,0) END ) "
                StrSql = StrSql + "FROM  ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT   (CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=STRUCT7)>0 THEN 0 ELSE NVL(STRUCT7,0) END ) "
                StrSql = StrSql + "FROM  ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT   (CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=STRUCT8)>0 THEN 0 ELSE NVL(STRUCT8,0) END ) "
                StrSql = StrSql + "FROM  ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT  (CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=STRUCT9)>0 THEN 0 ELSE NVL(STRUCT9,0) END ) "
                StrSql = StrSql + "FROM  ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT  (CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=STRUCT10)>0 THEN 0 ELSE NVL(STRUCT10,0) END )  AS C "
                StrSql = StrSql + "FROM ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                StrSql = StrSql + ")"
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
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
            Dim ds As New DataSet
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "ISCOMPLIBRARY "
                StrSql = StrSql + "FROM ECON.USERS "
                StrSql = StrSql + " WHERE USERID=" + UserId.ToString() + ""
                ds = odbUtil.FillDataSet(StrSql, SBAConnection)
                StrSql = String.Empty

                StrSql = "SELECT  "
                StrSql = StrSql + "USERS.USERNAME "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "INNER JOIN ECON.USERS ON USERS.USERID=PERMISSIONSCASES.USERID "
                StrSql = StrSql + " WHERE CASEID= " + CaseId
                StrSql = StrSql + " AND PERMISSIONSCASES.USERID=" + UserId.ToString() + ""
                If ds.Tables(0).Rows(0).Item("ISCOMPLIBRARY").ToString() = "Y" Then
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT USERS.USERNAME FROM COMPANYCASES INNER JOIN ECON.USERS ON USERS.USERID=COMPANYCASES.USERID WHERE CASEID= " + CaseId + " AND USERS.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserId.ToString() + ") "
                End If
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("CheckUser:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetAssumptionCaseId(ByVal AssumptionId As String) As DataSet
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim strCaseid As String = String.Empty
            Dim Dts As New DataSet()
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
            Try
                StrSql = "SELECT (STRUCT1 || ','|| STRUCT2 || ','|| STRUCT3 || ','|| STRUCT4  "
                StrSql = StrSql + "|| ','|| STRUCT5 || ','|| STRUCT6 || ','|| STRUCT7 || ','|| STRUCT8 "
                StrSql = StrSql + "|| ','|| STRUCT9 || ','|| STRUCT10) STRUCTID,DESCRIPTION "
                StrSql = StrSql + "FROM ASSUMPTIONS "
                StrSql = StrSql + "WHERE ASSUMPTIONID= " + AssumptionId.ToString()
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
                'strCaseid = Dts.Tables(0).Rows(0).Item("STRUCTID").ToString()
            Catch ex As Exception
                Throw New Exception("GetAssumptionCaseId:" + ex.Message.ToString())
            End Try
            'Return strCaseid
        End Function
        Public Function GetCasesByCaseID(ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

                Dim StrSql As String = "SELECT CASEID FROM PERMISSIONSCASES WHERE CASEID=" + CaseId + " "
                StrSql = StrSql + "UNION SELECT CASEID FROM BASECASES WHERE CASEID=" + CaseId
                StrSql = StrSql + "UNION SELECT CASEID FROM COMPANYCASES WHERE CASEID=" + CaseId
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function

        Public Function Cases(ByVal ID As String) As String()
            Dim CaseIDs() As String
            Dim i As New Integer
            Try

                Dim odbUtil As New DBUtil()

                Dim StrSqlCases As String = ""
                'StrSqlCases = "select to_char( nvl(Assumptions.Case1,0) )  ||  ',' || to_char( nvl(Assumptions.Case2,0))    ||   ',' ||  to_char( nvl(Assumptions.Case3,0) )  ||   ',' || to_char( nvl(Assumptions.Case4,0) )  ||  ',' ||  to_char( nvl(Assumptions.Case5,0) )  ||  ',' ||  to_char ( nvl(Assumptions.Case6,0) ) ||   ',' || to_char( nvl(Assumptions.Case7,0)  )  ||   ','  ||  to_char( nvl(Assumptions.Case8,0)  )  ||  ',' || to_char( nvl(Assumptions.Case9,0) )   ||   ','  || to_Char( nvl(Assumptions.Case10,0) )  as Cases   from  Assumptions WHERE Assumptions.AssumptionId =" + ID + ""

                StrSqlCases = "SELECT *  "
                StrSqlCases = StrSqlCases + "FROM "
                StrSqlCases = StrSqlCases + "( "
                StrSqlCases = StrSqlCases + "SELECT STRUCT1 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT STRUCT2 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT STRUCT3 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT STRUCT4 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT STRUCT5 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT STRUCT6 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT STRUCT7 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT STRUCT8 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT STRUCT9 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + "UNION "
                StrSqlCases = StrSqlCases + "SELECT STRUCT10 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSqlCases = StrSqlCases + ")A "
                StrSqlCases = StrSqlCases + "WHERE A.CASE  <>  0 "

                Dim Cs As New DataTable()
                Cs = odbUtil.FillDataTable(StrSqlCases, SBAConnection)
                ReDim CaseIDs(Cs.Rows.Count - 1)
                For i = 0 To Cs.Rows.Count - 1
                    CaseIDs(i) = Cs.Rows(i).Item("CASE").ToString()
                Next

                Return CaseIDs
            Catch ex As Exception
                Return CaseIDs
            End Try
        End Function


        Public Function GetDelCases(ByVal CaseID As String, ByVal UserId As String, ByVal CompFlag As String) As String
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Try
                StrSql = "SELECT CASEID FROM PERMISSIONSCASES WHERE CASEID= " + CaseID + " AND USERID=" + UserId.ToString() + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASEID FROM BASECASES WHERE CASEID= " + CaseID
                If CompFlag = "Y" Then
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT CASEID FROM COMPANYCASES WHERE CASEID= " + CaseID + " AND LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserId.ToString() + ") "

                End If
                Dim Cs As New DataTable()
                Cs = odbUtil.FillDataTable(StrSql, SBAConnection)

                If Cs.Rows.Count = 0 Then
                    Return CaseID
                End If
            Catch ex As Exception
                Throw New Exception("GetDelCases:" + ex.Message.ToString())
            End Try
            Return ""
        End Function



        Public Function GetEditCases(ByVal UserId As String, ByVal ID As String, ByVal flag As String, ByVal compFlag As String) As DataTable
            Dim Dts As New DataTable()
            Dim i As New Integer
            Try
                Dim odbUtil As New DBUtil()

                Dim StrSql As String = ""
                StrSql = "SELECT CASEID,CASEDE1,CASEDE FROM  "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT CASEID, "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "('STRUCTURE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE USERID=" + UserId.ToString() + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "CASEID, "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "('STRUCTURE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE "
                StrSql = StrSql + "FROM BASECASES "
                If compFlag = "Y" Then
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT "
                    StrSql = StrSql + "CASEID, "
                    StrSql = StrSql + "CASEDE1, "
                    StrSql = StrSql + "('STRUCTURE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE "
                    StrSql = StrSql + "FROM COMPANYCASES WHERE LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserId.ToString() + ") "
                End If
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
                StrSql = StrSql + "SELECT STRUCT1 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT STRUCT2 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT STRUCT3 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT STRUCT4 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT STRUCT5 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT STRUCT6 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT STRUCT7 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT STRUCT8 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT STRUCT9 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT STRUCT10 AS CASE FROM  ASSUMPTIONS WHERE ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + ")A "
                StrSql = StrSql + "WHERE A.CASE  <>  0 "
                StrSql = StrSql + ") "
                Dts = odbUtil.FillDataTable(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Return Dts
            End Try
        End Function

        Public Function GetExtrusionDetails(ByVal CaseId As Integer) As DataSet
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
                StrSql = StrSql + "(MAT.T1/PREF.CONVTHICK) AS THICK1, "
                StrSql = StrSql + "(MAT.T2/PREF.CONVTHICK) AS THICK2, "
                StrSql = StrSql + "(MAT.T3/PREF.CONVTHICK) AS THICK3, "
                StrSql = StrSql + "(MAT.T4/PREF.CONVTHICK) AS THICK4, "
                StrSql = StrSql + "(MAT.T5/PREF.CONVTHICK) AS THICK5, "
                StrSql = StrSql + "(MAT.T6/PREF.CONVTHICK) AS THICK6, "
                StrSql = StrSql + "(MAT.T7/PREF.CONVTHICK) AS THICK7, "
                StrSql = StrSql + "(MAT.T8/PREF.CONVTHICK) AS THICK8, "
                StrSql = StrSql + "(MAT.T9/PREF.CONVTHICK) AS THICK9, "
                StrSql = StrSql + "(MAT.T10/PREF.CONVTHICK) AS THICK10, "
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
                StrSql = StrSql + "OTR1 AS OTRP1, "
                StrSql = StrSql + "OTR2 AS OTRP2, "
                StrSql = StrSql + "OTR3 AS OTRP3, "
                StrSql = StrSql + "OTR4 AS OTRP4 , "
                StrSql = StrSql + "OTR5 AS OTRP5, "
                StrSql = StrSql + "OTR6 AS OTRP6 , "
                StrSql = StrSql + "OTR7 AS OTRP7, "
                StrSql = StrSql + "OTR8 AS OTRP8, "
                StrSql = StrSql + "OTR9 AS OTRP9, "
                StrSql = StrSql + "OTR10 AS OTRP10, "
                StrSql = StrSql + "WVTR1 AS WVTRP1, "
                StrSql = StrSql + "WVTR2 AS WVTRP2, "
                StrSql = StrSql + "WVTR3 AS WVTRP3, "
                StrSql = StrSql + "WVTR4 AS WVTRP4, "
                StrSql = StrSql + "WVTR5 AS WVTRP5, "
                StrSql = StrSql + "WVTR6 AS WVTRP6, "
                StrSql = StrSql + "WVTR7 AS WVTRP7, "
                StrSql = StrSql + "WVTR8 AS WVTRP8, "
                StrSql = StrSql + "WVTR9 AS WVTRP9, "
                StrSql = StrSql + "WVTR10 AS WVTRP10, "
				 StrSql = StrSql + "TS1VAL1, "
                StrSql = StrSql + "TS1VAL2, "
                StrSql = StrSql + "TS1VAL3, "
                StrSql = StrSql + "TS1VAL4, "
                StrSql = StrSql + "TS1VAL5, "
                StrSql = StrSql + "TS1VAL6, "
                StrSql = StrSql + "TS1VAL7, "
                StrSql = StrSql + "TS1VAL8, "
                StrSql = StrSql + "TS1VAL9, "
                StrSql = StrSql + "TS1VAL10, "
                StrSql = StrSql + "TS2VAL1, "
                StrSql = StrSql + "TS2VAL2, "
                StrSql = StrSql + "TS2VAL3, "
                StrSql = StrSql + "TS2VAL4, "
                StrSql = StrSql + "TS2VAL5, "
                StrSql = StrSql + "TS2VAL6, "
                StrSql = StrSql + "TS2VAL7, "
                StrSql = StrSql + "TS2VAL8, "
                StrSql = StrSql + "TS2VAL9, "
                StrSql = StrSql + "TS2VAL10, "
                StrSql = StrSql + "TO_CHAR(MAT.EFFDATE,'MON DD, YYYY')EFFDATE, "
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
                StrSql = StrSql + "PREF.TITLE15, "
                StrSql = StrSql + "PREF.UNITS, "
                StrSql = StrSql + "PREF.CONVAREA, "
                StrSql = StrSql + "PREF.CONVTHICK, "
                StrSql = StrSql + "PREF.TITLE19, "
                StrSql = StrSql + "PREF.TITLE20, "
                StrSql = StrSql + "PREF.CONVWT, "
                StrSql = StrSql + "PREF.CURR, "
				 StrSql = StrSql + "PREF.CONVPA, "
                StrSql = StrSql + "PREF.TITLE21, "
                StrSql = StrSql + "MAT.OTRTEMP, "
                StrSql = StrSql + "MAT.WVTRTEMP, "
                StrSql = StrSql + "MAT.OTRRH, "
                StrSql = StrSql + "MAT.WVTRRH, "
                StrSql = StrSql + "NVL(TYPEM1,0) TYPEM1, "
                StrSql = StrSql + "NVL(TYPEM2,0) TYPEM2, "
                StrSql = StrSql + "NVL(TYPEM3,0) TYPEM3, "
                StrSql = StrSql + "NVL(TYPEM4,0) TYPEM4, "
                StrSql = StrSql + "NVL(TYPEM5,0) TYPEM5, "
                StrSql = StrSql + "NVL(TYPEM6,0) TYPEM6, "
                StrSql = StrSql + "NVL(TYPEM7,0) TYPEM7, "
                StrSql = StrSql + "NVL(TYPEM8,0) TYPEM8, "
                StrSql = StrSql + "NVL(TYPEM9,0) TYPEM9, "
                StrSql = StrSql + "NVL(TYPEM10,0) TYPEM10, "
                StrSql = StrSql + "MAT.TYPEMSG1, "
                StrSql = StrSql + "MAT.TYPEMSG2, "
                StrSql = StrSql + "MAT.TYPEMSG3, "
                StrSql = StrSql + "MAT.TYPEMSG4, "
                StrSql = StrSql + "MAT.TYPEMSG5, "
                StrSql = StrSql + "MAT.TYPEMSG6, "
                StrSql = StrSql + "MAT.TYPEMSG7, "
                StrSql = StrSql + "MAT.TYPEMSG8, "
                StrSql = StrSql + "MAT.TYPEMSG9, "
                StrSql = StrSql + "MAT.TYPEMSG10 "

                StrSql = StrSql + "FROM MATERIALINPUT MAT "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=MAT.CASEID "
                StrSql = StrSql + "INNER JOIN BARRIERINPUT BI  "
                StrSql = StrSql + "ON BI.CASEID=MAT.CASEID "
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
                StrSql = StrSql + "WHERE MAT.CASEID =" + CaseId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetMaterials(ByVal MatId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = " SELECT MATID,CATEGORY,GRADEDES FROM "
                StrSql = StrSql + "(SELECT MATID,CATEGORY,MATERIAL AS GRADEDES FROM ADHESIVEPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,MATERIAL AS GRADEDES  FROM ALUMINUMPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,DESCRIPTION AS GRADEDES FROM BASEFIBERPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,MATERIAL AS GRADEDES FROM COATINGSPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,DESCRIPTION AS GRADEDES FROM CONCENTRATEPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,MATERIAL AS GRADEDES FROM FILMPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,MATERIAL AS GRADEDES FROM GLASSPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,MATERIAL AS GRADEDES FROM INKPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,MATERIAL AS GRADEDES FROM NONWOVENSPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,TECHDESCRIPTION AS GRADEDES FROM PAPERBOARDPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,DESCRIPTION AS GRADEDES FROM PAPERPROP "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT MATID,CATEGORY,MATERIAL AS GRADEDES FROM RESINPROP  "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,DESCRIPTION AS GRADEDES FROM SHEETPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,CATEGORY,DESCRIPTION AS GRADEDES FROM STEELPROP )  "
                StrSql = StrSql + "WHERE MATID=" + MatId.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetMaterialGrades(ByVal MatId As Integer, ByVal MatDe1 As String, ByVal MatDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = " SELECT MATID,GRADEDES FROM "
                StrSql = StrSql + "(SELECT MATID,MATERIAL AS GRADEDES FROM ADHESIVEPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,MATERIAL AS GRADEDES  FROM ALUMINUMPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,DESCRIPTION AS GRADEDES FROM BASEFIBERPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,MATERIAL AS GRADEDES FROM COATINGSPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,DESCRIPTION AS GRADEDES FROM CONCENTRATEPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,MATERIAL AS GRADEDES FROM FILMPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,MATERIAL AS GRADEDES FROM GLASSPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,MATERIAL AS GRADEDES FROM INKPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,MATERIAL AS GRADEDES FROM NONWOVENSPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,TECHDESCRIPTION AS GRADEDES FROM PAPERBOARDPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,DESCRIPTION AS GRADEDES FROM PAPERPROP "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT MATID,MATERIAL AS GRADEDES FROM RESINPROP  "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,DESCRIPTION AS GRADEDES FROM SHEETPROP "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT MATID,DESCRIPTION AS GRADEDES FROM STEELPROP )  "
                StrSql = StrSql + "WHERE MATID=" + MatId.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetDelStructure(ByVal CaseID As String, ByVal UserId As String) As DataSet
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim Cs As New DataSet()
            Try
                StrSql = "SELECT CASEID FROM PERMISSIONSCASES WHERE CASEID= " + CaseID + " AND USERID=" + UserId.ToString() + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASEID FROM BASECASES WHERE CASEID= " + CaseID
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASEID FROM COMPANYCASES WHERE CASEID= " + CaseID + " AND LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserId.ToString() + ") "
                Cs = odbUtil.FillDataSet(StrSql, SBAConnection)
            Catch ex As Exception
                Throw New Exception("GetDelStructure:" + ex.Message.ToString())
            End Try
            Return Cs
        End Function
        Public Function GetDelGroup(ByVal groupId As String) As DataSet
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            Dim Cs As New DataSet()
            Try
                StrSql = "SELECT GROUPID FROM GROUPS WHERE GROUPID= " + groupId
                Cs = odbUtil.FillDataSet(StrSql, SBAConnection)
            Catch ex As Exception
                Throw New Exception("GetDelGroup:" + ex.Message.ToString())
            End Try
            Return Cs
        End Function
#End Region

#Region "NEW GROUP"
        Public Function GetBCaseDetailsByUserGrp(ByVal keyWord As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  DISTINCT CASEID,CASEDES,CASEDE2,CASEDE3 AS NOTES,APPLICATION,CREATIONDATE,SERVERDATE  "
                StrSql = StrSql + " FROM "
                StrSql = StrSql + " ( "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "PC.CASEID, "
                StrSql = StrSql + "(PC.CASEID||' '||PC.CASEDE1)CASEDES, "
                StrSql = StrSql + "PC.CASEDE2, "
                StrSql = StrSql + "PC.CASEDE3, "
                StrSql = StrSql + "PC.APPLICATION, "
                StrSql = StrSql + " PC.CREATIONDATE, "
                StrSql = StrSql + "CASE WHEN PC.CREATIONDATE-PC.SERVERDATE =0 THEN 'NA' ELSE to_char(PC.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE "
                StrSql = StrSql + "FROM GROUPS "
                StrSql = StrSql + "INNER JOIN GROUPCASES "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
                StrSql = StrSql + "RIGHT OUTER JOIN BASECASES PC "
                StrSql = StrSql + "ON PC.CASEID=GROUPCASES.CASEID "
                StrSql = StrSql + " ) "
                StrSql = StrSql + "WHERE UPPER(CASEDES) LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE2) LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEID) LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE3) LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(APPLICATION) LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper().Trim() + "%' "
                StrSql = StrSql + "ORDER BY UPPER(CASEID),UPPER(CASEDES) "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetBCaseDetailsByUserGrp:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetPCaseDetailsByUserGrp(ByVal UserId As String, ByVal keyWord As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  DISTINCT CASEID,CASEDES,CASEDE2,CASEDE3 AS NOTES,APPLICATION,CREATIONDATE,SERVERDATE  "
                StrSql = StrSql + " FROM "
                StrSql = StrSql + " ( "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "PC.CASEID, "
                StrSql = StrSql + "(PC.CASEID||' '||PC.CASEDE1)CASEDES, "
                StrSql = StrSql + "PC.CASEDE2, "
                StrSql = StrSql + "PC.CASEDE3, "
                StrSql = StrSql + "PC.APPLICATION, "
                StrSql = StrSql + " PC.CREATIONDATE, "
                StrSql = StrSql + "CASE WHEN PC.CREATIONDATE-PC.SERVERDATE =0 THEN 'NA' ELSE to_char(PC.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE "
                StrSql = StrSql + "FROM GROUPS "
                StrSql = StrSql + "INNER JOIN GROUPCASES "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
                StrSql = StrSql + "RIGHT OUTER JOIN PERMISSIONSCASES PC "
                StrSql = StrSql + "ON PC.CASEID=GROUPCASES.CASEID "
                StrSql = StrSql + "WHERE PC.USERID ='" + UserId + "' "
                'StrSql = StrSql + "WHERE UPPER(PC.USERNAME) ='" + UserName.ToUpper().ToString() + "' "
                StrSql = StrSql + " ) "
                StrSql = StrSql + "WHERE UPPER(CASEDES) LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE2) LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEID) LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE3) LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(APPLICATION) LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper().Trim() + "%' "
                StrSql = StrSql + "ORDER BY UPPER(CASEID),UPPER(CASEDES) "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetPCaseDetailsByUserGrp:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetPCaseByGroups(ByVal UserId As String, ByVal CaseDe1 As String, ByVal grpId As String, ByVal Type As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  DISTINCT CASEID,CASEDES   "
                StrSql = StrSql + " FROM "
                StrSql = StrSql + " ( "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "PC.CASEID, "
                StrSql = StrSql + "(PC.CASEID||' '||PC.CASEDE1||PC.CASEDE2)CASEDES "
                StrSql = StrSql + "FROM GROUPS "
                StrSql = StrSql + "INNER JOIN GROUPCASES "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
                If Type = "PROP" Then
                    StrSql = StrSql + "RIGHT OUTER JOIN PERMISSIONSCASES PC "
                ElseIf Type = "BASE" Then
                    StrSql = StrSql + "RIGHT OUTER JOIN BASECASES PC "
                ElseIf Type = "CPROP" Then
                    StrSql = StrSql + "INNER JOIN COMPANYCASES PC "
                End If
                StrSql = StrSql + "ON PC.CASEID=GROUPCASES.CASEID "
                If Type = "PROP" Then
                    StrSql = StrSql + "WHERE PC.USERID =" + UserId.ToString() + " "
                    StrSql = StrSql + "AND PC.CASEID IN(SELECT CASEID FROM GROUPCASES WHERE GROUPID=" + grpId + " ) "
                ElseIf Type = "BASE" Then
                    StrSql = StrSql + "WHERE PC.CASEID IN(SELECT CASEID FROM GROUPCASES WHERE GROUPID=" + grpId + " ) "
                ElseIf Type = "CPROP" Then
                    StrSql = StrSql + "AND PC.CASEID IN(SELECT CASEID FROM GROUPCASES WHERE GROUPID=" + grpId + " ) "
                End If
                StrSql = StrSql + " ) "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDES),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR CASEID LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY UPPER(CASEID),UPPER(CASEDES) "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetPCaseByGroups:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Function GetPManageCaseGrpDetails(ByVal UserID As String, ByVal keyWord As String, ByVal Type As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New StandGetData.Selectdata()
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
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
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
                strSQL = strSQL + "AND GROUPTYPE= '" + Type + "' "
                DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                If DtRes.Tables(0).Rows.Count > 0 Then
                    For j = 0 To DtRes.Tables(0).Rows.Count - 1
                        'Getting CaseID
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "CASEID "
                        strSQL = strSQL + "FROM GROUPCASES "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString()
                        strSQL = strSQL + " ORDER BY  SEQ"
                        Dts = odbUtil.FillDataSet(strSQL, SBAConnection)
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
                        strSQL = strSQL + "DES2, "
                        strSQL = strSQL + "DES3 AS NOTES, "
                        strSQL = strSQL + "APPLICATION,"
                        strSQL = strSQL + "to_char(GPS.CREATIONDATE,'mm/dd/yyyy hh:mi:ss AM'  ) CREATIONDATE, "
                        strSQL = strSQL + "CASE WHEN GPS.CREATIONDATE-GPS.UPDATEDATE =0 THEN 'NA' ELSE to_char(GPS.UPDATEDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  UPDATEDATE "
                        strSQL = strSQL + "FROM "
                        strSQL = strSQL + "GROUPS GPS "
                        strSQL = strSQL + "WHERE GPS.USERID= " + UserID + " AND GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString() + " "
                        If j = 0 Then
                            strSqlOutPut = strSQL
                        Else
                            strSqlOutPut = strSqlOutPut + " UNION ALL " + strSQL
                        End If
                    Next

                    strSqlOutPut = "SELECT * FROM ( " + strSqlOutPut + " ) DUAL "
                    strSqlOutPut = strSqlOutPut + "WHERE NVL(UPPER(DES2),'#') LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSqlOutPut = strSqlOutPut + "OR NVL(UPPER(NOTES),'#') LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSqlOutPut = strSqlOutPut + "OR NVL(UPPER(APPLICATION),'#') LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSqlOutPut = strSqlOutPut + "OR NVL(UPPER(CASEID),'#') LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSqlOutPut = strSqlOutPut + "OR NVL(UPPER(GROUPNAME),'#') LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSqlOutPut = strSqlOutPut + "OR GROUPID LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper() + "%' " + " ORDER BY UPPER(GROUPNAME),UPPER(DES2)"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, SBAConnection)
                Else
                    strSqlOutPut = " SELECT * FROM GROUPCASES WHERE GROUPID=0"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, SBAConnection)
                End If

                Return dtOutPut
            Catch ex As Exception
                Throw New Exception("StandGetData:GetPManageCaseGrpDetails:" + ex.Message.ToString())
                Return DtRes
            End Try
        End Function
        Public Function GetBCaseGrpDetails(ByVal Type As String, ByVal keyWord As String) As DataSet

            Dim Dts As New DataSet()
            Dim objGetData As New StandGetData.Selectdata()
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
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
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
                strSQL = strSQL + "WHERE GROUPTYPE= '" + Type + "' "
                DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                If DtRes.Tables(0).Rows.Count > 0 Then
                    For j = 0 To DtRes.Tables(0).Rows.Count - 1
                        'Getting CaseID
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "CASEID "
                        strSQL = strSQL + "FROM GROUPCASES "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString()
                        strSQL = strSQL + " ORDER BY  SEQ"
                        Dts = odbUtil.FillDataSet(strSQL, SBAConnection)
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
                        strSQL = strSQL + "DES2, "
                        strSQL = strSQL + "DES3 AS NOTES, "
                        strSQL = strSQL + "APPLICATION,"
                        strSQL = strSQL + "to_char(GPS.CREATIONDATE,'mm/dd/yyyy hh:mi:ss AM'  ) CREATIONDATE, "
                        strSQL = strSQL + "CASE WHEN GPS.CREATIONDATE-GPS.UPDATEDATE =0 THEN 'NA' ELSE to_char(GPS.UPDATEDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  UPDATEDATE "
                        strSQL = strSQL + "FROM "
                        strSQL = strSQL + "GROUPS GPS "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString() + " "
                        If j = 0 Then
                            strSqlOutPut = strSQL
                        Else
                            strSqlOutPut = strSqlOutPut + " UNION ALL " + strSQL
                        End If
                    Next

                    strSqlOutPut = "SELECT * FROM ( " + strSqlOutPut + " ) DUAL "
                    strSqlOutPut = strSqlOutPut + "WHERE NVL(UPPER(DES2),'#') LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSqlOutPut = strSqlOutPut + "OR NVL(UPPER(NOTES),'#') LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSqlOutPut = strSqlOutPut + "OR NVL(UPPER(APPLICATION),'#') LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSqlOutPut = strSqlOutPut + "OR NVL(UPPER(CASEID),'#') LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSqlOutPut = strSqlOutPut + "OR NVL(UPPER(GROUPNAME),'#') LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSqlOutPut = strSqlOutPut + "OR GROUPID LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper() + "%' " + " ORDER BY UPPER(GROUPNAME),UPPER(DES2)"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, SBAConnection)
                Else
                    strSqlOutPut = " SELECT * FROM GROUPCASES WHERE GROUPID=0"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, SBAConnection)
                End If

                Return dtOutPut
            Catch ex As Exception
                Throw
                Return DtRes
            End Try

        End Function
#End Region

#Region "New changes Notes And Sponsor Popup"
        Public Function GetGradeDetails(ByVal GradeId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = " SELECT GRADES.GRADENAME,GRADES.GRADEID FROM GRADES "
                StrSql = StrSql + "WHERE GRADES.GRADEID=" + GradeId.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SAssist:GetGradeDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetNotes(ByVal CaseID As String, ByVal grpID As String, ByVal Type As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                If Type = "GNOTES" Or Type = "BGNOTES" Or Type = "CGNOTES" Then
                    StrSql = "SELECT  DES3 AS NOTES "
                    StrSql = StrSql + "FROM GROUPS "
                    StrSql = StrSql + "WHERE GROUPID='" + grpID + "' "
                ElseIf Type = "PNOTES" Then
                    StrSql = "SELECT  CASEDE3 AS NOTES "
                    StrSql = StrSql + "FROM PERMISSIONSCASES "
                    StrSql = StrSql + "WHERE CASEID='" + CaseID + "' "
                ElseIf Type = "BNOTES" Then
                    StrSql = "SELECT  CASEDE3 AS NOTES "
                    StrSql = StrSql + "FROM BASECASES "
                    StrSql = StrSql + "WHERE CASEID='" + CaseID + "' "
                ElseIf Type = "CNOTES" Then
                    StrSql = "SELECT  CASEDE3 AS NOTES "
                    StrSql = StrSql + "FROM COMPANYCASES "
                    StrSql = StrSql + "WHERE CASEID='" + CaseID + "' "
                End If


                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetNotes:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Blend"
        Public Function GetBlendMatDetails(ByVal CaseId As String, ByVal MatId As String, ByVal Type As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty

            Try
                StrSql = "SELECT BCM1,BCM2,BCM3,BCM4,BCM5,BCM6,BCM7,BCM8,BCM9,BCM10,BCSG1,BCSG2,BCSG3,BCSG4,BCSG5,BCSG6,BCSG7,BCSG8,BCSG9,BCSG10,"
                StrSql = StrSql + "BCT1,BCT2,BCT3,BCT4,BCT5,BCT6,BCT7,BCT8,BCT9,BCT10,BCOTR1,BCOTR2,BCOTR3,BCOTR4,BCOTR5,BCOTR6,BCOTR7,BCOTR8,BCOTR9,BCOTR10,"
                StrSql = StrSql + "BCWVTR1,BCWVTR2,BCWVTR3,BCWVTR4,BCWVTR5,BCWVTR6,BCWVTR7,BCWVTR8,BCWVTR9,BCWVTR10, "
                StrSql = StrSql + "BCGRADE1,BCGRADE2,BCGRADE3,BCGRADE4,BCGRADE5,BCGRADE6,BCGRADE7,BCGRADE8,BCGRADE9,BCGRADE10 FROM BLENDMATINPUT "
                StrSql = StrSql + "WHERE CASEID=" + CaseId + " AND TYPEM=" + MatId + " AND TYPEID=" + Type + ""
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetDataGetData:GetBlendMatDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetBlendDetailsPref(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = " SELECT BMAT.CASEID,TYPEM,TYPEID,"
                StrSql = StrSql + "NVL(BCM1,0) AS BCM1,"
                StrSql = StrSql + "NVL(BCM2,0) AS BCM2,"
                StrSql = StrSql + "(BCT1/PREF.CONVTHICK) AS BCT1, "
                StrSql = StrSql + "(BCT2/PREF.CONVTHICK) AS BCT2, "
                StrSql = StrSql + "NVL(BCGRADE1,0) AS BCGRADE1,"
                StrSql = StrSql + "NVL(BCGRADE2,0) AS BCGRADE2,"
                StrSql = StrSql + "BCOTR1,BCOTR2,BCWVTR1,BCWVTR2,BCSG1,BCSG2,BCTS1VAL1,BCTS1VAL2,BCTS2VAL1,BCTS2VAL2 FROM BLENDMATINPUT BMAT  "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF ON PREF.CASEID=BMAT.CASEID "
                StrSql = StrSql + "WHERE BMAT.CASEID=" + CaseId.ToString()
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)

                If Dts.Tables(0).Rows.Count > 0 Then

                Else
                    StrSql = " SELECT '" + CaseId.ToString() + "' AS CASEID, '0' AS TYPEM,"
                    StrSql = StrSql + "'0' AS TYPEID,"
                    StrSql = StrSql + "'0' AS BCM1,"
                    StrSql = StrSql + "'0' AS BCM2,"
                    StrSql = StrSql + "'0' AS BCT1,"
                    StrSql = StrSql + "'0' AS BCT2,"
                    StrSql = StrSql + "'0' AS BCGRADE1,"
                    StrSql = StrSql + "'0' AS BCGRADE2,"
                    StrSql = StrSql + "'' AS BCOTR1,'' AS BCOTR2,'' AS BCWVTR1,'' AS BCWVTR2,'' AS BCSG1,'' AS BCSG2,  "
					  StrSql = StrSql + "'' AS BCTS1VAL1,'' AS BCTS1VAL2,'' AS BCTS2VAL1,'' AS BCTS2VAL2 FROM DUAL"
                    Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                End If

                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetBlendDetailsPref:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetBlendDetailsSugg(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = " SELECT TYPEM, "
                StrSql = StrSql + "(MAT1.MATDE1||' '||MAT1.MATDE2)MATS1,   "
                StrSql = StrSql + "(MAT2.MATDE1||' '||MAT2.MATDE2)MATS2,  "
                StrSql = StrSql + "(MAT1.SG)AS SGS1,  "
                StrSql = StrSql + "(MAT2.SG)AS SGS2, "
				 StrSql = StrSql + "(MAT1.DGRADATION)AS DG1,  "
                StrSql = StrSql + "(MAT2.DGRADATION)AS DG2, "
                StrSql = StrSql + "(MAT1.ISADJTHICK) ISADJTHICK1,   "
                StrSql = StrSql + "(MAT2.ISADJTHICK) ISADJTHICK2    "
                StrSql = StrSql + "FROM BLENDMATINPUT BMI "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT1  "
                StrSql = StrSql + "ON MAT1.MATID = BMI.BCM1  "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT2  "
                StrSql = StrSql + "ON MAT2.MATID = BMI.BCM2  "
                StrSql = StrSql + "WHERE BMI.CASEID=" + CaseId.ToString()
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)

                If Dts.Tables(0).Rows.Count > 0 Then

                Else
                    StrSql = " SELECT '" + CaseId.ToString() + "' AS CASEID,'0' AS TYPEM,"
                    StrSql = StrSql + "'' AS MATS1,"
                    StrSql = StrSql + "'' AS MATS2,"
                    StrSql = StrSql + "'0' AS SGS1,"
                    StrSql = StrSql + "'0' AS SGS2,"
					 StrSql = StrSql + "'0' AS DG1,"
                    StrSql = StrSql + "'0' AS DG2,"
                    StrSql = StrSql + "'' AS ISADJTHICK1,"
                    StrSql = StrSql + "'' AS ISADJTHICK2"
                    StrSql = StrSql + " FROM DUAL  "
                    Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                End If

                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetBlendDetailsSugg:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetExistingBlendMaterials(ByVal CaseId As Integer, ByVal TypeId As Integer, ByVal TypeM As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = " SELECT * FROM BLENDMATINPUT BMI "
                StrSql = StrSql + "WHERE BMI.CASEID=" + CaseId.ToString() + " "
                StrSql = StrSql + "AND BMI.TYPEID=" + TypeId.ToString() + " "
                StrSql = StrSql + "AND BMI.TYPEM=" + TypeM.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetExistingBlendMaterials:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

#End Region

#Region "Company Library"
        Public Function GetCompGroupCaseDet1(ByVal UserID As String, ByVal Type As String, ByVal Key As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New StandGetData.Selectdata()
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim strSQL As String = String.Empty
            Dim strSQL2 As String = String.Empty
            Dim strSQL3 As String = String.Empty
            Dim ds As New DataSet()
            Dim CaseIDs As String = String.Empty
            Dim DtRes As New DataSet()
            Dim cnt As Integer = 0
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
                Dts = objGetData.GetCompGroupIDByUSer(UserID, Type)
                If Dts.Tables(0).Rows.Count > 0 Then
                    For i = 0 To Dts.Tables(0).Rows.Count - 1
                        ds = objGetData.GetGroupCasesByUSer(Dts.Tables(0).Rows(i).Item("GROUPID").ToString())
                        If ds.Tables(0).Rows.Count > 0 Then
                            For j = 0 To ds.Tables(0).Rows.Count - 1
                                If j = 0 Then
                                    CaseIDs = ds.Tables(0).Rows(j).Item("CASEID").ToString()
                                Else
                                    CaseIDs = CaseIDs + ", " + ds.Tables(0).Rows(j).Item("CASEID").ToString()
                                End If
                            Next
                        Else
                            CaseIDs = "0"
                        End If
                        If cnt = 0 Then
                            cnt += 1
                            'strSQL = "SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' || ' ,Cases:" + CaseIDs + "' AS GROUPDES,'" + CaseIDs + "' AS CASEIDS FROM DUAL "
                            strSQL = "SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' || '   ' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' || '   '|| '" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "'  AS GROUPDES,'" + CaseIDs + "' AS CASEIDS,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' AS DES1,REPLACE('" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "','''','&#')DESAPOS1,'" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "' AS DES2,REPLACE('" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "','''','&#')DESAPOS2,'" + Dts.Tables(0).Rows(i).Item("Des3").ToString().Replace("'", "''") + "' AS DES3,REPLACE('" + Dts.Tables(0).Rows(i).Item("Des3").ToString().Replace("'", "''") + "','''','&#')DESAPOS3,'" + Dts.Tables(0).Rows(i).Item("APPLICATION").ToString().Replace("'", "''") + "' AS APPLICATION,REPLACE('" + Dts.Tables(0).Rows(i).Item("APPLICATION").ToString().Replace("'", "''") + "','''','&#')APPLICATIONAPOS,'" + Dts.Tables(0).Rows(i).Item("NAME").ToString().Replace("'", "''") + "' AS NAME,REPLACE('" + Dts.Tables(0).Rows(i).Item("NAME").ToString().Replace("'", "''") + "','''','&#')NAMEAPOS,'" + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + "' || ':' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' AS CDES1,'' FILENAME FROM DUAL "
                        Else
                            cnt += 1
                            strSQL = strSQL + "UNION ALL SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' || '   ' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' || '   '|| '" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "'  AS GROUPDES,'" + CaseIDs + "' AS CASEIDS,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' AS DES1,REPLACE('" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "','''','&#')DESAPOS1,'" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "' AS DES2,REPLACE('" + Dts.Tables(0).Rows(i).Item("Des2").ToString().Replace("'", "''") + "','''','&#')DESAPOS2,'" + Dts.Tables(0).Rows(i).Item("Des3").ToString().Replace("'", "''") + "' AS DES3,REPLACE('" + Dts.Tables(0).Rows(i).Item("Des3").ToString().Replace("'", "''") + "','''','&#')DESAPOS3,'" + Dts.Tables(0).Rows(i).Item("APPLICATION").ToString().Replace("'", "''") + "' AS APPLICATION,REPLACE('" + Dts.Tables(0).Rows(i).Item("APPLICATION").ToString().Replace("'", "''") + "','''','&#')APPLICATIONAPOS,'" + Dts.Tables(0).Rows(i).Item("NAME").ToString().Replace("'", "''") + "' AS NAME,REPLACE('" + Dts.Tables(0).Rows(i).Item("NAME").ToString().Replace("'", "''") + "','''','&#')NAMEAPOS,'" + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + "' || ':' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString().Replace("'", "''") + "' AS CDES1,'' FILENAME FROM DUAL "
                        End If
                    Next
                    strSQL = " SELECT * FROM ( " + strSQL + " ) DUAL"
                End If

                strSQL2 = "SELECT 0 GROUPID,'All Structures' AS GROUPNAME,''  AS GROUPDES,'' AS CASEIDS,'All Structures' AS DES1,'All Structures' AS DESAPOS1,'' AS DES2,'' AS DESAPOS2,'' AS DES3,'' AS DESAPOS3,'' AS APPLICATION,'' AS APPLICATIONAPOS,'' AS NAME,'' AS NAMEAPOS,'' AS CDES1,'' FILENAME FROM DUAL "
                If strSQL <> "" Then
                    strSQL2 = strSQL2 + " UNION ALL " + strSQL
                    strSQL3 = "SELECT GROUPID,GROUPNAME,GROUPDES,CASEIDS,(GROUPID||':'||DES1) GROUPIDD,DES1,REPLACE(DES1,'''','&#')DESAPOS1,DES2,REPLACE(DES2,'''','&#')DESAPOS2,DES3,REPLACE(DES3,'''','&#')DESAPOS3,CDES1,APPLICATION,REPLACE(APPLICATION,'''','&#')APPLICATIONAPOS,NAME,REPLACE(NAME,'''','&#')NAMEAPOS,'' FILENAME FROM (" + strSQL2 + " ) "
                    strSQL3 = strSQL3 + " WHERE NVL(UPPER(DES1),'#') LIKE '%" + Key.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSQL3 = strSQL3 + "OR NVL(UPPER(DES2),'#') LIKE '%" + Key.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSQL3 = strSQL3 + "OR NVL(UPPER(DES3),'#') LIKE '%" + Key.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSQL3 = strSQL3 + "OR NVL(UPPER(APPLICATION),'#') LIKE '%" + Key.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSQL3 = strSQL3 + "OR NVL(UPPER(CASEIDS),'#') LIKE '%" + Key.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSQL3 = strSQL3 + "OR NVL(UPPER(NAME),'#') LIKE '%" + Key.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSQL3 = strSQL3 + "OR GROUPID LIKE '%" + Key.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSQL3 = strSQL3 + "ORDER BY GROUPID,UPPER(DES1),UPPER(DES2) "
                    DtRes = odbUtil.FillDataSet(strSQL3, MyConnectionString)
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
        Public Function GetCompGroupIDByUSer(ByVal UserID As String, ByVal Type As String) As DataSet
            Dim Dts As New DataSet()
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
                Dim StrSql As String = ""
                StrSql = "SELECT DISTINCT GROUPID,  "
                StrSql = StrSql + "DES1, "
                StrSql = StrSql + "DES2, "
                StrSql = StrSql + "DES3, "
                StrSql = StrSql + "CREATIONDATE, "
                StrSql = StrSql + "UPDATEDATE, "
                StrSql = StrSql + "APPLICATION,'' NAME, ''FILENAME  "
                StrSql = StrSql + "FROM GROUPS "
                StrSql = StrSql + "INNER JOIN ECON.USERS USR ON USR.LICENSEID=GROUPS.LICENSEID WHERE GROUPTYPE= '" + Type + "'   "
                StrSql = StrSql + "AND GROUPS.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ")"

                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetGroupIDByUSer:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompCaseByLic(ByVal UserId As String, ByVal CaseDe1 As String, ByVal LicId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERNAME,CASEID,CASEDE1,REPLACE(CASEDE1,'''','&#')CASEDEAPOS1,CASEDE2,REPLACE(CASEDE2,'''','&#')CASEDEAPOS2,CASEDE3,REPLACE(CASEDE3,'''','&#')CASEDEAPOS3,APPLICATION,REPLACE(APPLICATION,'''','&#')APPLICATIONAPOS,CASEDES,CREATIONDATE,SERVERDATE,NAME,REPLACE(NAME,'''','&#')NAMEAPOS,SUPPLIERID,FILENAME,STRUCTTYPE  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "(SELECT '' USERNAME, 0 CASEID,' Nothing'CASEDE1,'Selected'CASEDE2,''CASEDE3,''CASEDES,NULL CREATIONDATE,NULL SERVERDATE,''NAME,NULL SUPPLIERID,NULL FILENAME,'' APPLICATION,'' STRUCTTYPE  FROM DUAL  "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT USERS.USERNAME, "
                StrSql = StrSql + "CASEID, "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "CASEDE2, "
                StrSql = StrSql + "CASEDE3, "
                StrSql = StrSql + "('PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES, "

                StrSql = StrSql + " CREATIONDATE, "
                StrSql = StrSql + "CASE WHEN CREATIONDATE-SERVERDATE =0 THEN 'NA' ELSE to_char(SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE, "
                StrSql = StrSql + " '' NAME, 0 SUPPLIERID,''FILENAME,"
                StrSql = StrSql + "APPLICATION,'COMPANY' STRUCTTYPE  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "COMPANYCASES "
                StrSql = StrSql + "INNER JOIN ECON.USERS ON USERS.USERID=COMPANYCASES.USERID "
                StrSql = StrSql + "WHERE USERS.LICENSEID=" + LicId + ") "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR CASEID LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(CASEDE3),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(APPLICATION),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY CASEID,UPPER(CASEDE1),UPPER(CASEDE2) "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetCompCaseByLic:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompCaseDetailsByGroupLic(ByVal CaseDe1 As String, ByVal grpId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = " SELECT  CASEID,USERNAME,(CASEID||':'||CASEDE1) CASEIDD,CASEDE1,REPLACE(CASEDE1,'''','&#')CASEDEAPOS1,CASEDE2,REPLACE(CASEDE2,'''','&#')CASEDEAPOS2,CASEDE3,REPLACE(CASEDE3,'''','&#')CASEDEAPOS3,CASEDES,CREATIONDATE,SERVERDATE,NAME,REPLACE(NAME,'''','&#')NAMEAPOS,SUPPLIERID,APPLICATION,REPLACE(APPLICATION,'''','&#')APPLICATIONAPOS,'COMPANY' STRUCTTYPE,''FILENAME   "
                StrSql = StrSql + " FROM  "
                StrSql = StrSql + " (  "
                StrSql = StrSql + "SELECT DISTINCT 0 CASEID,''USERNAME,' Nothing'CASEDE1,'Selected'CASEDE2,''CASEDE3,''APPLICATION,''CASEDES,NULL CREATIONDATE,''SERVERDATE,''NAME,NULL SUPPLIERID,'' STRUCTTYPE,''FILENAME FROM DUAL   "
                StrSql = StrSql + "UNION ALL  "
                StrSql = StrSql + "SELECT DISTINCT PC.CASEID,  "
                StrSql = StrSql + " USERS.USERNAME,  "
                StrSql = StrSql + "PC.CASEDE1,  "
                StrSql = StrSql + "PC.CASEDE2,  "
                StrSql = StrSql + "PC.CASEDE3,  "
                StrSql = StrSql + "PC.APPLICATION,  "
                StrSql = StrSql + "('PACKAGE FORMAT= '||PC.CASEDE1||' UNIQUE FEATURES= '||PC.CASEDE2)CASEDES,  "
                StrSql = StrSql + " PC.CREATIONDATE,  "
                StrSql = StrSql + "CASE WHEN PC.CREATIONDATE-PC.SERVERDATE =0 THEN 'NA' ELSE to_char(PC.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE,  "
                StrSql = StrSql + " '' NAME, 0 SUPPLIERID,'COMPANY' STRUCTTYPE,''FILENAME  "
                StrSql = StrSql + "FROM GROUPS  "
                StrSql = StrSql + "INNER JOIN GROUPCASES  "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.GROUPID  "
                StrSql = StrSql + "RIGHT OUTER JOIN COMPANYCASES PC  "
                StrSql = StrSql + "ON PC.CASEID=GROUPCASES.CASEID  "
                StrSql = StrSql + "INNER JOIN ECON.USERS USERS   "
                StrSql = StrSql + "ON USERS.USERID=PC.USERID "
                'StrSql = StrSql + "ON UPPER(USERS.USERNAME)=UPPER(PC.USERNAME) "
                StrSql = StrSql + "WHERE PC.CASEID IN(SELECT CASEID FROM GROUPCASES WHERE GROUPID=" + grpId + " ) "
                StrSql = StrSql + " ) "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR CASEID LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(CASEDE3),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(APPLICATION),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY CASEID,UPPER(CASEDE1),UPPER(CASEDE2) "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetCompCaseDetailsByGroupLic:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompAllUserPropCaseByLic(ByVal CaseDe1 As String, ByVal LicId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERNAME,CASEID,CASEDE1,REPLACE(CASEDE1,'''','&#')CASEDEAPOS1,CASEDE2,REPLACE(CASEDE2,'''','&#')CASEDEAPOS2,CASEDE3,REPLACE(CASEDE3,'''','&#')CASEDEAPOS3,APPLICATION,REPLACE(APPLICATION,'''','&#')APPLICATIONAPOS,LICENSEID,CASEDES,CREATIONDATE,SERVERDATE,NAME,REPLACE(NAME,'''','&#')NAMEAPOS,SUPPLIERID,FILENAME,STRUCTTYPE  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "(SELECT ''USERNAME, 0 CASEID,' Nothing'CASEDE1,'Selected'CASEDE2,''CASEDE3,''CASEDES,NULL CREATIONDATE,''SERVERDATE,NULL NAME,NULL SUPPLIERID,''FILENAME,NULL APPLICATION,''STRUCTTYPE,NULL LICENSEID  FROM DUAL  "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT USR.USERNAME, "
                StrSql = StrSql + "CCS.CASEID,CCS.CASEDE1,CCS.CASEDE2, "
                StrSql = StrSql + "CCS.CASEDE3, "
                StrSql = StrSql + "('PACKAGE FORMAT= '||CCS.CASEDE1||' UNIQUE FEATURES= '||CCS.CASEDE2)CASEDES, "

                StrSql = StrSql + "CCS.CREATIONDATE, "
                StrSql = StrSql + "CASE WHEN CCS.CREATIONDATE-CCS.SERVERDATE =0 THEN 'NA' ELSE to_char(CCS.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE, "
                StrSql = StrSql + " '' NAME, 0 SUPPLIERID,''FILENAME,CCS.APPLICATION,'Company library' STRUCTTYPE,USR.LICENSEID "
                StrSql = StrSql + "FROM COMPANYCASES CCS "
                StrSql = StrSql + "INNER JOIN ECON.USERS USR "
                StrSql = StrSql + "ON CCS.LICENSEID=USR.LICENSEID "
                StrSql = StrSql + "WHERE USR.LICENSEID='" + LicId.ToString() + "' "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT USR.USERNAME, "
                StrSql = StrSql + "PC.CASEID,PC.CASEDE1,PC.CASEDE2, "
                StrSql = StrSql + "PC.CASEDE3,('PACKAGE FORMAT= '||PC.CASEDE1||' UNIQUE FEATURES= '||PC.CASEDE2)CASEDES,  "
                StrSql = StrSql + "PC.CREATIONDATE,CASE WHEN PC.CREATIONDATE-PC.SERVERDATE =0 THEN 'NA' ELSE to_char(PC.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE,  "
                StrSql = StrSql + "'' NAME, 0 SUPPLIERID,''FILENAME,PC.APPLICATION,'Individual user library' STRUCTTYPE,USR.LICENSEID  "
                StrSql = StrSql + "FROM  "
                StrSql = StrSql + "PERMISSIONSCASES PC  "
                StrSql = StrSql + "INNER JOIN ECON.USERS USR "
                StrSql = StrSql + "ON PC.USERID=USR.USERID  "
                StrSql = StrSql + "WHERE USR.LICENSEID='" + LicId.ToString() + "' "
                StrSql = StrSql + ") "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR CASEID LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(CASEDE3),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(APPLICATION),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(USERNAME),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(STRUCTTYPE),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY CASEID,UPPER(CASEDE1),UPPER(CASEDE2) "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetCompAllUserPropCaseByLic:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompCaseDetails(ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT CASEID, CASEDE1,CASEDE2,CASEDE3,APPLICATION,0 SUPPLIERID,''NAME,''FILENAME "
                StrSql = StrSql + "FROM COMPANYCASES "
                StrSql = StrSql + "WHERE LICENSEID= (SELECT USERS.LICENSEID FROM ECON.USERS USERS WHERE USERID=" + UserId.ToString() + ")  "
                StrSql = StrSql + "ORDER BY CASEID "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetCompanyCaseDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompCaseData(ByVal CaseId As String) As DataSet
            Dim StrSql As String = String.Empty
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Try
                StrSql = "SELECT CASEID, CASEDE1,CASEDE2,CASEDE3,APPLICATION,0 SUPPLIERID,''NAME,''FILENAME "
                StrSql = StrSql + "FROM COMPANYCASES "
                StrSql = StrSql + "WHERE CASEID= " + CaseId + " "
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts

            Catch ex As Exception
                Throw New Exception("StandGetData:GetCompCaseData:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Function GetCompanyManageCaseGrpDetails(ByVal UserID As String, ByVal keyWord As String, ByVal Type As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New StandGetData.Selectdata()
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
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
            Try


                'Getting Groups
                strSQL = "SELECT DISTINCT GROUPID,  "
                strSQL = strSQL + "DES1 GROUPNAME, "
                strSQL = strSQL + "DES2 GROUPDES, "
                strSQL = strSQL + "GROUPID || ':'|| DES1 CDES1,"
                strSQL = strSQL + "to_char(CREATIONDATE,'mm/dd/yyyy hh:mi:ss AM'  ) CREATIONDATE, "
                strSQL = strSQL + "CASE WHEN CREATIONDATE-UPDATEDATE =0 THEN 'NA' ELSE to_char(UPDATEDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  UPDATEDATE "
                strSQL = strSQL + "FROM "
                strSQL = strSQL + "GROUPS "
                strSQL = strSQL + "INNER JOIN ECON.USERS USERS "
                strSQL = strSQL + "ON USERS.LICENSEID=GROUPS.LICENSEID "
                strSQL = strSQL + "WHERE GROUPTYPE= '" + Type + "' "
                strSQL = strSQL + "AND GROUPS.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                If DtRes.Tables(0).Rows.Count > 0 Then
                    For j = 0 To DtRes.Tables(0).Rows.Count - 1
                        'Getting CaseID
                        strSQL = "SELECT GROUPCASES.GROUPID,  "
                        strSQL = strSQL + "CASEID "
                        strSQL = strSQL + "FROM GROUPCASES "
                        strSQL = strSQL + "INNER JOIN GROUPS "
                        strSQL = strSQL + "ON GROUPS.GROUPID=GROUPCASES.GROUPID "
                        strSQL = strSQL + "INNER JOIN ECON.USERS USERS "
                        strSQL = strSQL + "ON USERS.USERID=GROUPS.USERID "
                        strSQL = strSQL + "WHERE GROUPCASES.GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString()
                        strSQL = strSQL + " ORDER BY  SEQ"
                        Dts = odbUtil.FillDataSet(strSQL, SBAConnection)
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
                        strSQL = strSQL + "DES2, "
                        strSQL = strSQL + "DES3 AS NOTES, "
                        strSQL = strSQL + "APPLICATION,"
                        strSQL = strSQL + "to_char(GPS.CREATIONDATE,'mm/dd/yyyy hh:mi:ss AM'  ) CREATIONDATE, "
                        strSQL = strSQL + "CASE WHEN GPS.CREATIONDATE-GPS.UPDATEDATE =0 THEN 'NA' ELSE to_char(GPS.UPDATEDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  UPDATEDATE "
                        strSQL = strSQL + "FROM "
                        strSQL = strSQL + "GROUPS GPS "
                        strSQL = strSQL + "INNER JOIN ECON.USERS USERS "
                        strSQL = strSQL + "ON USERS.USERID=GPS.USERID "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString() + " "
                        If j = 0 Then
                            strSqlOutPut = strSQL
                        Else
                            strSqlOutPut = strSqlOutPut + " UNION ALL " + strSQL
                        End If
                    Next

                    strSqlOutPut = "SELECT * FROM ( " + strSqlOutPut + " ) DUAL "
                    strSqlOutPut = strSqlOutPut + "WHERE NVL(UPPER(DES2),'#') LIKE '%" + keyWord.ToUpper() + "%' "
                    strSqlOutPut = strSqlOutPut + "OR NVL(UPPER(NOTES),'#') LIKE '%" + keyWord.ToUpper() + "%' "
                    strSqlOutPut = strSqlOutPut + "OR NVL(UPPER(APPLICATION),'#') LIKE '%" + keyWord.ToUpper() + "%' "
                    strSqlOutPut = strSqlOutPut + "OR NVL(UPPER(CASEID),'#') LIKE '%" + keyWord.ToUpper() + "%' "
                    strSqlOutPut = strSqlOutPut + "OR NVL(UPPER(GROUPNAME),'#') LIKE '%" + keyWord.ToUpper() + "%' "
                    strSqlOutPut = strSqlOutPut + "OR GROUPID LIKE '%" + keyWord.ToUpper() + "%' " + " ORDER BY UPPER(GROUPNAME),UPPER(DES2)"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, SBAConnection)
                Else
                    strSqlOutPut = " SELECT * FROM GROUPCASES WHERE GROUPID=0"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, SBAConnection)
                End If

                Return dtOutPut
            Catch ex As Exception
                Throw New Exception("StandGetData:GetCompanyManageCaseGrpDetails:" + ex.Message.ToString())
                Return DtRes
            End Try
        End Function
        Public Function GetCompGroupDetails(ByVal UserID As String, ByVal flag As Char, ByVal Type As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New StandGetData.Selectdata()
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
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
            Try
                'Getting Groups
                strSQL = "SELECT DISTINCT GROUPID,  "
                strSQL = strSQL + "DES1 GROUPNAME, "
                strSQL = strSQL + "DES2 GROUPDES, "
                strSQL = strSQL + "GROUPID || ':'|| DES1 CDES1,"
                strSQL = strSQL + "to_char(CREATIONDATE,'mm/dd/yyyy hh:mi:ss AM'  ) CREATIONDATE, "
                strSQL = strSQL + "CASE WHEN CREATIONDATE-UPDATEDATE =0 THEN 'NA' ELSE to_char(UPDATEDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  UPDATEDATE "
                strSQL = strSQL + "FROM "
                strSQL = strSQL + "GROUPS "
                strSQL = strSQL + "INNER JOIN ECON.USERS USERS "
                strSQL = strSQL + "ON USERS.LICENSEID=GROUPS.LICENSEID "
                strSQL = strSQL + "WHERE GROUPTYPE= '" + Type + "' "
                strSQL = strSQL + "AND GROUPS.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                If DtRes.Tables(0).Rows.Count > 0 Then
                    For j = 0 To DtRes.Tables(0).Rows.Count - 1
                        'Getting CaseID
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "CASEID "
                        strSQL = strSQL + "FROM GROUPCASES "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString()
                        strSQL = strSQL + " ORDER BY  SEQ"
                        Dts = odbUtil.FillDataSet(strSQL, SBAConnection)
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
                        strSQL = strSQL + "DES2 GROUPDES,DES3,APPLICATION, "
                        strSQL = strSQL + "GROUPID || ':'|| DES1 CDES1,'' FILENAME,'' NAME, "
                        strSQL = strSQL + "to_char(CREATIONDATE,'mm/dd/yyyy hh:mi:ss AM'  ) CREATIONDATE, "
                        strSQL = strSQL + "CASE WHEN CREATIONDATE-UPDATEDATE =0 THEN 'NA' ELSE to_char(UPDATEDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  UPDATEDATE "
                        strSQL = strSQL + "FROM "
                        strSQL = strSQL + "GROUPS "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString() + " "
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
                        strSQL = strSQL + "'NA'  DES3, "
                        strSQL = strSQL + "'NA'  APPLICATION, "
                        strSQL = strSQL + "'NA'  FILENAME, "
                        strSQL = strSQL + "'NA'  NAME, "
                        strSQL = strSQL + "'0:None'  CDES1, "
                        strSQL = strSQL + "'NA'  CREATIONDATE, "
                        strSQL = strSQL + "'NA'  UPDATEDATE "
                        strSQL = strSQL + "FROM "
                        strSQL = strSQL + "DUAL "

                        strSqlOutPut = strSqlOutPut + " UNION ALL " + strSQL
                    End If
                    strSqlOutPut = " SELECT * FROM ( " + strSqlOutPut + " ) DUAL ORDER BY GROUPNAME,GROUPDES"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, SBAConnection)
                Else
                    strSqlOutPut = " SELECT * FROM GROUPCASES WHERE GROUPID=0 "
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, SBAConnection)
                End If

                Return dtOutPut
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function
        Public Function GetCompCaseDetailsByUserGrp(ByVal keyWord As String, ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  DISTINCT CASEID,CASEDES,CASEDE2,CASEDE3 AS NOTES,APPLICATION,CREATIONDATE,SERVERDATE  "
                StrSql = StrSql + " FROM "
                StrSql = StrSql + " ( "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "PC.CASEID, "
                StrSql = StrSql + "(PC.CASEID||' '||PC.CASEDE1)CASEDES, "
                StrSql = StrSql + "PC.CASEDE2, "
                StrSql = StrSql + "PC.CASEDE3, "
                StrSql = StrSql + "PC.APPLICATION, "
                StrSql = StrSql + " PC.CREATIONDATE, "
                StrSql = StrSql + "CASE WHEN PC.CREATIONDATE-PC.SERVERDATE =0 THEN 'NA' ELSE to_char(PC.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE "
                StrSql = StrSql + "FROM GROUPS "
                StrSql = StrSql + "INNER JOIN GROUPCASES "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
                StrSql = StrSql + "RIGHT OUTER JOIN COMPANYCASES PC "
                StrSql = StrSql + "ON PC.CASEID=GROUPCASES.CASEID "
                StrSql = StrSql + "INNER JOIN ECON.USERS USR "
                StrSql = StrSql + "ON PC.USERID=USR.USERID "
                StrSql = StrSql + "WHERE USR.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS USERS WHERE USERID=" + UserId.ToString() + ")  "
                StrSql = StrSql + " ) "
                StrSql = StrSql + "WHERE UPPER(CASEDES) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE2) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEID) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE3) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(APPLICATION) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "ORDER BY UPPER(CASEID),UPPER(CASEDES) "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetCompCaseDetailsByUserGrp:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetAllCompGroupDetails(ByVal UserID As String, ByVal Type As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New StandGetData.Selectdata()
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
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
            Try


                'Getting Groups
                strSQL = "SELECT DISTINCT GROUPID,  "
                strSQL = strSQL + "DES1 GROUPNAME, "
                strSQL = strSQL + "DES2 GROUPDES, "
                strSQL = strSQL + "GROUPID || ':'|| DES1 CDES1,"
                strSQL = strSQL + "to_char(CREATIONDATE,'mm/dd/yyyy hh:mi:ss AM'  ) CREATIONDATE, "
                strSQL = strSQL + "CASE WHEN CREATIONDATE-UPDATEDATE =0 THEN 'NA' ELSE to_char(UPDATEDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  UPDATEDATE "
                strSQL = strSQL + "FROM "
                strSQL = strSQL + "GROUPS "
                strSQL = strSQL + "INNER JOIN ECON.USERS USERS "
                strSQL = strSQL + "ON USERS.LICENSEID=GROUPS.LICENSEID "
                strSQL = strSQL + "WHERE GROUPTYPE= '" + Type + "' "
                strSQL = strSQL + "AND GROUPS.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "

                DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                If DtRes.Tables(0).Rows.Count > 0 Then
                    For j = 0 To DtRes.Tables(0).Rows.Count - 1
                        'Getting CaseID
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "CASEID "
                        strSQL = strSQL + "FROM GROUPCASES "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString()
                        strSQL = strSQL + " ORDER BY  SEQ"
                        Dts = odbUtil.FillDataSet(strSQL, SBAConnection)
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
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString() + " "
                        If j = 0 Then
                            strSqlOutPut = strSQL
                        Else
                            strSqlOutPut = strSqlOutPut + " UNION ALL " + strSQL
                        End If
                    Next

                    strSqlOutPut = "SELECT * FROM ( " + strSqlOutPut + " ) DUAL ORDER BY UPPER(GROUPNAME),UPPER(GROUPDES)"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, SBAConnection)
                Else
                    strSqlOutPut = " SELECT * FROM GROUPCASES WHERE GROUPID=0"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, SBAConnection)
                End If

                Return dtOutPut
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function
#End Region

#Region "1Sep2016"
        Public Function GetSupplierMatGradeCheck(ByVal matId As String, ByVal supId As String, ByVal gradeId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT *  "
                StrSql = StrSql + "FROM SUPPLIERGRADEDETAILS SGD "
                StrSql = StrSql + "WHERE SGD.MATERIALID= " + matId + " "
                StrSql = StrSql + "AND SGD.SUPPLIERID= " + supId + " "
                StrSql = StrSql + "AND SGD.GRADEID= " + gradeId + " "
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SBAssist:GetSupplierMatGradeCheck:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetSponsorSupplier(ByVal MatId As Integer, ByVal flag As Boolean) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty

            Try
                If MatId <> "0" Then

                    StrSql = "  SELECT    "
                    StrSql = StrSql + " DISTINCT GS.GRADEID, SP.SUPPLIERID,GS.DESCRIPTION, "
                    StrSql = StrSql + "SP.NAME , "
                    StrSql = StrSql + "GR.GRADENAME, "
                    StrSql = StrSql + "MS.ISSPONSOR "
                    StrSql = StrSql + "FROM   "
                    StrSql = StrSql + "SUPPLIER SP  "
                    StrSql = StrSql + "LEFT OUTER JOIN MATSUPPLIERGRADE GS   "
                    StrSql = StrSql + "ON SP.SUPPLIERID=GS.SUPPLIERID   "
                    StrSql = StrSql + " INNER JOIN MATERIALSUPPLIER MS "
                    StrSql = StrSql + "ON MS.MATID=GS.MATID "
                    StrSql = StrSql + " LEFT OUTER JOIN GRADES GR  "
                    StrSql = StrSql + "ON GR.GRADEID=GS.GRADEID  "
                    StrSql = StrSql + "WHERE GS.MATID = " + MatId.ToString()
                    If flag = True Then
                        StrSql = StrSql + " AND MS.ISSPONSOR='Y' "
                    Else
                        StrSql = StrSql + " AND MS.ISSPONSOR='N' "
                    End If
                    StrSql = StrSql + "ORDER BY NAME,GRADENAME ASC "
                End If

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetSponsorSupplier:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetMatSupplierGrade(ByVal matId As String, ByVal SupId As String, ByVal GradeId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = " SELECT SP.NAME,GRADES.GRADENAME,GRADES.GRADEID,SP.SUPPLIERID,MS.DESCRIPTION FROM SUPPLIER SP "
                StrSql = StrSql + "INNER JOIN MATSUPPLIERGRADE MS  "
                StrSql = StrSql + "ON SP.SUPPLIERID=MS.SUPPLIERID "
                StrSql = StrSql + "INNER JOIN GRADES  "
                StrSql = StrSql + "ON GRADES.GRADEID = MS.GRADEID "
                StrSql = StrSql + "WHERE MS.MATID= " + matId.ToString() + " "
                StrSql = StrSql + "AND GRADES.GRADEID=" + GradeId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SAssist:GetSupplierMaterial:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

Public Function GetMetricTestConvFactorProp() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT PROPERTYID,METRICUNIT,ENGLISHUNIT,CONVFACTOR,MATERIALID,SUPPLIERID,GRADEID "
                StrSql = StrSql + "FROM METENGFACTORSPT "
                Dts = odbUtil.FillDataSet(StrSql, SBAConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SAssist:GetSupplierMaterialSEQ:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
		 Public Function GetCaseViewDet(ByVal CaseId As String) As DataSet
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ds As New DataSet()
            Try
                StrSql = "SELECT CASEID,ISOTR,ISWVTR,ISTS1,ISTS2 FROM CASEVIEWDET WHERE CASEID= " + CaseId
                ds = odbUtil.FillDataSet(StrSql, SBAConnection)
            Catch ex As Exception
                Throw New Exception("GetCaseViewDet:" + ex.Message.ToString())
            End Try
            Return ds
        End Function
#End Region

#Region "Manage SA Users Admin Tool"

        Public Function GetStructUserInforMation(ByVal UserName As String, ByVal SrcUser As String, ByVal UserId As String) As DataSet
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty

                StrSql = "SELECT USERS.USERID,  "
                StrSql = StrSql + "USERS.USERNAME,UCS.FIRSTNAME,UCS.LASTNAME, "
                StrSql = StrSql + "USERS.PASSWORD, "
                'StrSql = StrSql + "USERS.COMPANY, "
                StrSql = StrSql + "(CASE WHEN COMPANY.COMPANYNAME IS NULL THEN '' ELSE COMPANY.COMPANYNAME END )COMPANY,"
                StrSql = StrSql + "NVL(USERS.LICENSEID,0) LICENSEID, "
                StrSql = StrSql + "LICENSEMASTER.LICENSENAME LICENSENO, "
                StrSql = StrSql + "USERS.ISPROMOMAIL, "
                StrSql = StrSql + "USERS.ISIADMINLICUSR ISADMIN, "
                StrSql = StrSql + "NVL(USERS.INDUSAGE,'N')INDUSAGE, "
                StrSql = StrSql + "NVL(USERS.LICUSAGE,'N')LICUSAGE "
                StrSql = StrSql + "FROM USERS LEFT JOIN LICENSEMASTER ON LICENSEMASTER.LICENSEID=USERS.LICENSEID "
                StrSql = StrSql + "INNER JOIN USERCONTACTS UCS ON USERS.USERID=UCS.USERID "
                StrSql = StrSql + "LEFT OUTER JOIN SHOPPING.COMPANY "
                StrSql = StrSql + "ON COMPANY.COMPANYID=USERS.COMPANYID "
                StrSql = StrSql + "WHERE UPPER(NVL(USERS.USERNAME,'#')) LIKE '%@" + UserName.ToString().ToUpper() + "' "
                StrSql = StrSql + "AND UPPER(NVL(USERS.USERNAME,'#')) LIKE '%" + SrcUser.ToString().ToUpper() + "%' AND USERS.ISVALIDEMAIL='Y' "
                StrSql = StrSql + "ORDER BY UPPER(USERS.USERNAME) "
                Ds = odbutil.FillDataSet(StrSql, EconConnection)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetStructTUserInforMation(ByVal UserId As String) As DataSet
            Dim Ds, dsSer As New DataSet()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select T1.LICENSEID,  "
                StrSql = StrSql + "'License'||T1.SEQ LICENSE , "
                StrSql = StrSql + "T2.USERNAME USERNAME1, "
                StrSql = StrSql + "T3.USERNAME USERNAME2 "
                StrSql = StrSql + "from TRANSSERV T1 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.USERS T2 "
                StrSql = StrSql + "ON T2.USERID=T1.USERID1 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.USERS T3 "
                StrSql = StrSql + "ON T3.USERID=T1.USERID2 "
                StrSql = StrSql + "WHERE T1.LICENSEID IN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserId + " "
                StrSql = StrSql + ") "
                StrSql = StrSql + "ORDER BY SEQ ASC "

                Ds = odbutil.FillDataSet(StrSql, SBAConnection)

                Return Ds
            Catch ex As Exception
                Throw New Exception("Getdata:GetStructTUserInforMation:" + ex.Message.ToString())
            End Try
        End Function

        Public Function GetStructAdminServiceInforMation(ByVal UserId As String) As DataSet
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty

                StrSql = "SELECT REFNUM,LICENSEID,ITEMNUMBER,TOTALCOUNT "
                StrSql = StrSql + "FROM LICENSESERVICES "
                StrSql = StrSql + "WHERE LICENSEID=(SELECT LICENSEID FROM USERS WHERE USERID=" + UserId + ") AND ITEMNUMBER='SA'  "
                Ds = odbutil.FillDataSet(StrSql, EconConnection)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function StructLicUserCount(ByVal licenseId As String) As DataSet
            Dim Ds As New DataSet()
            Dim StrSql As String = String.Empty
            Try
                'Inserting  License Count
                StrSql = "SELECT count(*) CNT FROM TRANSSERV "
                StrSql = StrSql + "WHERE LICENSEID = " + licenseId + " AND COUNT=1 AND TYPE='SA'"
                Ds = odbutil.FillDataSet(StrSql, SBAConnection)
                Return Ds

            Catch ex As Exception
                Throw New Exception("Getdata:StructLicUserCount:" + ex.Message.ToString())
            End Try
        End Function
        Public Function StructLicUserCountn(ByVal licenseId As String) As DataSet
            Dim Ds As New DataSet()
            Dim StrSql As String = String.Empty
            Try
                 'Inserting  License Count
                StrSql = "SELECT count(*) CNT FROM TRANSSERV "
                StrSql = StrSql + "WHERE LICENSEID = " + licenseId + " AND NVL(USERID1,-1)<>-1 AND TYPE='SA' "
                Ds = odbutil.FillDataSet(StrSql, SBAConnection)

                Return Ds

            Catch ex As Exception
                Throw New Exception("Getdata:LicUserCount:" + ex.Message.ToString())
            End Try
        End Function

        Public Function GetTUserInforMation(ByVal UserId As String) As DataSet
            Dim Ds, dsSer As New DataSet()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select T1.LICENSEID,  "
                StrSql = StrSql + "'License'||T1.SEQ LICENSE , "
                StrSql = StrSql + "T2.USERNAME USERNAME1, "
                StrSql = StrSql + "T3.USERNAME USERNAME2 "
                StrSql = StrSql + "from TRANSSERV T1 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.USERS T2 "
                StrSql = StrSql + "ON T2.USERID=T1.USERID1 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.USERS T3 "
                StrSql = StrSql + "ON T3.USERID=T1.USERID2 "
                StrSql = StrSql + "WHERE T1.LICENSEID IN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserId + " "
                StrSql = StrSql + ") AND T1.TYPE='SA' "

                Ds = odbutil.FillDataSet(StrSql, SBAConnection)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetStructServiceUserInforMation(ByVal UserName As String, ByVal UserId As String, ByVal serviceDesc As String) As DataSet
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty

                StrSql = "SELECT DISTINCT USRP.USERID,USERROLE  "
                StrSql = "SELECT DISTINCT USRP.USERID,USERROLE  "
                StrSql = StrSql + "FROM USERPERMISSIONS USRP "
                StrSql = StrSql + "LEFT JOIN USERS ON USERS.USERID=USRP.USERID  "
                StrSql = StrSql + "INNER JOIN SERVICES ON SERVICES.SERVICEID=USRP.SERVICEID  "
                StrSql = StrSql + "WHERE  UPPER(SERVICES.SERVICEDE)='" + serviceDesc.ToUpper() + "' "
                StrSql = StrSql + "AND UPPER(USERS.USERNAME) LIKE '%@" + UserName.ToString().ToUpper() + "' "

                Ds = odbutil.FillDataSet(StrSql, EconConnection)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetStructServiceUser(ByVal UserId As String, ByVal serviceDesc As String) As DataSet
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty

                StrSql = "SELECT USERID,USERROLE  "
                StrSql = StrSql + "FROM USERPERMISSIONS USRP "
                StrSql = StrSql + "INNER JOIN SERVICES ON SERVICES.SERVICEID=USRP.SERVICEID  "
                StrSql = StrSql + "WHERE  UPPER(SERVICES.SERVICEDE)='" + serviceDesc.ToUpper() + "' "
                StrSql = StrSql + "AND USERID= " + UserId + " "
                Ds = odbutil.FillDataSet(StrSql, EconConnection)
                Return Ds
            Catch ex As Exception
                Throw New Exception("UtilityGetdata:GetStructServiceUser:" + ex.Message.ToString())
            End Try
        End Function

        Public Function LicenseFSTransferUser(ByVal userid As String) As DataSet
            Dim Ds As New DataSet()
            Dim StrSql As String = String.Empty
            Try
                'Inserting  License Count
                StrSql = "SELECT T1.USERID1,T2.USERNAME FROM TRANSSERV T1 "
                StrSql = StrSql + "INNER JOIN ECON.USERS T2 ON T1.USERID1=T2.USERID "
                StrSql = StrSql + "WHERE T1.LICENSEID = (SELECT LICENSEID FROM ECON.USERS WHERE USERID='" + userid + "' ) AND COUNT=0 AND TYPE='SA' "
                Ds = odbutil.FillDataSet(StrSql, SBAConnection)
                Return Ds

            Catch ex As Exception
                Throw New Exception("Getdata:LicenseFSTransferUser:" + ex.Message.ToString())
            End Try
        End Function

        Public Function GetUserInforMation(ByVal UserId As String) As DataSet
            Dim Ds As New DataSet()
            Try
              Dim StrSql As String = String.Empty
                StrSql = "SELECT USERID,LICENSEID,ISIADMINLICUSR FROM USERS WHERE USERID=" + UserId + " "
                Ds = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetServiceUserData(ByVal UserName As String, ByVal serviceDesc As String) As DataSet
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty

                StrSql = "SELECT USERID,USERROLE  "
                StrSql = StrSql + "FROM USERPERMISSIONS USRP "
                StrSql = StrSql + "INNER JOIN SERVICES ON SERVICES.SERVICEID=USRP.SERVICEID  "
                StrSql = StrSql + "WHERE  UPPER(SERVICES.SERVICEDE)='" + serviceDesc.ToUpper() + "' "
                StrSql = StrSql + "AND USERID= (SELECT USERID FROM USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "') "
                Ds = odbutil.FillDataSet(StrSql, EconConnection)
                Return Ds
            Catch ex As Exception
                Throw New Exception("Getdata:GetServiceUserData:" + ex.Message.ToString())
            End Try
        End Function

        Public Sub AddOrderUsersD(ByVal user As String, ByVal serviceDesc As String)
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty
                Dim Conn As String = String.Empty

                StrSql = "INSERT INTO USERPERMISSIONS (USERID,SERVICEID,USERROLE,MAXCASECOUNT)  "
                StrSql = StrSql + "SELECT (SELECT USERID FROM USERS WHERE UPPER(USERNAME)='" + user.ToUpper() + "'),(SELECT SERVICEID FROM SERVICES WHERE UPPER(SERVICEDE)='" + serviceDesc.ToUpper() + "'),'ReadWrite',500 FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + " (SELECT 1 FROM USERPERMISSIONS "
                StrSql = StrSql + "WHERE USERID=(SELECT USERID FROM USERS WHERE UPPER(USERNAME)='" + user.ToUpper() + "') AND SERVICEID IN (SELECT SERVICEID FROM SERVICES WHERE UPPER(SERVICEDE)='" + serviceDesc.ToUpper() + "'))"

                odbutil.UpIns(StrSql, EconConnection)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub DeleteOrderUsers(ByVal UserId As String, ByVal serviceDesc As String)
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty
                Dim Conn As String = String.Empty

                StrSql = "DELETE FROM USERPERMISSIONS "
                StrSql = StrSql + "WHERE USERID=" + UserId + " AND SERVICEID IN (SELECT SERVICEID FROM SERVICES WHERE UPPER(SERVICEDE)='" + serviceDesc.ToUpper() + "') "

                odbutil.UpIns(StrSql, EconConnection)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub UpdateSLicenseData(ByVal licenseId As String, ByVal fUserId As String, ByVal tUser As String)
            Dim StrSql As String = String.Empty
            Try
                'Update  License Count
                StrSql = "UPDATE TRANSSERV SET USERID2 =(SELECT USERID FROM ECON.USERS WHERE UPPER(USERNAME)='" + tUser.ToUpper() + "'),COUNT=1 "
                StrSql = StrSql + "WHERE LICENSEID = " + licenseId + " AND USERID1 =" + fUserId + " AND TYPE='SA'  "
                odbutil.UpIns(StrSql, SBAConnection)

            Catch ex As Exception
                Throw New Exception("UpInsData:UpdateLicenseData:" + ex.Message.ToString())
            End Try
        End Sub

        Public Function GetStructUserDetails(ByVal Id As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERID,  "
                StrSql = StrSql + "UPPER(USERNAME)USERNAME, "
                StrSql = StrSql + "USERNAME AS TOOLUSERNAME, "

                'Checking for License Administrator
                StrSql = StrSql + "NVL(USERS.ISIADMINLICUSR,'N')ISIADMINLICUSR,"
                StrSql = StrSql + "USERS.LICENSEID,"
                StrSql = StrSql + "USERS.ISCOMPLIBRARY,"
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
                StrSql = StrSql + "WHERE USERS.USERID = " + Id.ToString() + " "
                StrSql = StrSql + "AND SERVICES.SERVICEDE='StandAssist' "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StandGetData:GetUserDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

#End Region
    End Class
   



End Class
