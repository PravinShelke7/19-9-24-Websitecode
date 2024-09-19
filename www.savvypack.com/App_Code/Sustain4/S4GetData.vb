Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Public Class S4GetData
    Public Class Selectdata
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
        
        Public Function GetCouser(ByVal UserName As String) As DataTable
            Dim Dts As New DataTable
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")


                Dim StrSql As String = " Select Users.Username , Users.userid From Users  where exists"
                StrSql = StrSql + " (select 1 from UserPermissions "
                StrSql = StrSql + " Inner Join Services on Services.SERVICEID = UserPermissions.SERVICEID "
                StrSql = StrSql + " where UserPermissions.UserID =Users.UserID and  Services.SERVICEDE='SUSTAIN4'  "
                'StrSql = StrSql + " and Users.company IN(select Users.company from users where username='" + UserName + "')"
                StrSql = StrSql + "AND USERS.LICENSEID=(SELECT LICENSEID FROM USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper.ToString() + "') "
                StrSql = StrSql + " )order by  Users.Username"

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
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")

                Dim StrSql As String = "SELECT caseID,caseDE1,('Case:'||caseID||' - '|| caseDE1||' ' || caseDE2) as CaseDe FROM permissionscases WHERE USERID=" + UserId.ToString() + " "
                StrSql = StrSql + "UNION SELECT caseID,caseDE1,('Case:'||caseID||' - '|| caseDE1||' ' || caseDE2) as CaseDe  FROM basecases ORDER BY caseDE1"

                Dts = odbUtil.FillDataTable(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
        Public Function Cases(ByVal ID As String) As String
            Dim CaseIDs As String = ""
            Try

                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ4ConnectionString")

                Dim StrSqlCases As String = ""
                StrSqlCases = "select to_char( nvl(Assumptions.Case1,0) )  ||  ',' || to_char( nvl(Assumptions.Case2,0))    ||   ',' ||  to_char( nvl(Assumptions.Case3,0) )  ||   ',' || to_char( nvl(Assumptions.Case4,0) )  ||  ',' ||  to_char( nvl(Assumptions.Case5,0) )  ||  ',' ||  to_char ( nvl(Assumptions.Case6,0) ) ||   ',' || to_char( nvl(Assumptions.Case7,0)  )  ||   ','  ||  to_char( nvl(Assumptions.Case8,0)  )  ||  ',' || to_char( nvl(Assumptions.Case9,0) )   ||   ','  || to_Char( nvl(Assumptions.Case10,0) )  as Cases   from  Assumptions WHERE Assumptions.AssumptionId =" + ID + ""
                Dim Cs As New DataTable()
                Cs = odbUtil.FillDataTable(StrSqlCases, MyConnectionString)
                CaseIDs = Cs.Rows(0).Item("Cases").ToString()
                Return CaseIDs
            Catch ex As Exception
                Return CaseIDs
            End Try
        End Function
        Public Function GetDescription(ByVal AssumptionID As String) As String
            Dim Des As String = ""
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ4ConnectionString")

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
        Public Function GetSavedCaseAsperUser(ByVal UserId As String) As DataSet
            Dim Dts As New DataSet
            Try
                Dim StrSqlSaved As String = ""
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("Econ4ConnectionString")

                StrSqlSaved = "SELECT distinct Assumptions.AssumptionId,"
                StrSqlSaved = StrSqlSaved + "( Assumptions.AssumptionId ||' - ' ||"
                StrSqlSaved = StrSqlSaved + "Assumptions.DESCRIPTION || ' : ' ||"
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
                Dts = odbUtil.FillDataSet(StrSqlSaved, MyConnectionString1)
            Catch ex As Exception
                Throw
            End Try
            Return Dts
        End Function
        Public Function GetSavedCaseAsperUser(ByVal UserId As String, ByVal AssumptionId As String) As DataSet
            Dim Dts As New DataSet
            Try
                Dim StrSqlSaved As String = ""
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("Econ4ConnectionString")

                StrSqlSaved = "SELECT distinct Assumptions.AssumptionId,"
                StrSqlSaved = StrSqlSaved + "Assumptions.DESCRIPTION,"
                StrSqlSaved = StrSqlSaved + "( Assumptions.AssumptionId ||' - ' ||"
                StrSqlSaved = StrSqlSaved + "Assumptions.DESCRIPTION || ' : ' ||"
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
     
        Public Function GetDelCases(ByVal CaseID As String, ByVal UserId As String) As String
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")
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
                Throw New Exception("GetDelCases:" + ex.Message.ToString())
            End Try
            Return ""
        End Function
       
        Public Function GetEditCases(ByVal UserId As String, ByVal ID As String, ByVal flag As String) As DataTable
            Dim Dts As New DataTable()
            Dim i As New Integer
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")

                Dim StrSql As String = ""
                StrSql = "SELECT CASEID,CASEDE1,CASEDE FROM  "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT CASEID, "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE USERID=" + UserId.ToString() + " "
                StrSql = StrSql + " UNION "
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
                StrSql = StrSql + "SELECT CASE1 AS CASE FROM  Econ4.ASSUMPTIONS WHERE Econ4.ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE2 AS CASE FROM  Econ4.ASSUMPTIONS WHERE Econ4.ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE3 AS CASE FROM Econ4.ASSUMPTIONS WHERE Econ4.ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE4 AS CASE FROM  Econ4.ASSUMPTIONS WHERE Econ4.ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE5 AS CASE FROM  Econ4.ASSUMPTIONS WHERE Econ4.ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE6 AS CASE FROM  Econ4.ASSUMPTIONS WHERE Econ4.ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE7 AS CASE FROM  Econ4.ASSUMPTIONS WHERE Econ4.ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE8 AS CASE FROM  Econ4.ASSUMPTIONS WHERE Econ4.ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE9 AS CASE FROM  Econ4.ASSUMPTIONS WHERE Econ4.ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE10 AS CASE FROM  Econ4.ASSUMPTIONS WHERE Econ4.ASSUMPTIONS.ASSUMPTIONID = " + ID + " "
                StrSql = StrSql + ")A "
                StrSql = StrSql + "WHERE A.CASE  <>  0 "
                StrSql = StrSql + ") ORDER BY CASEID ASC "
                Dts = odbUtil.FillDataTable(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Return Dts
            End Try
        End Function

#Region "Global Manager"
        Public Function GetAllPermissionCases(ByVal AssumId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim strCaseId As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")
            Try
                StrSql = "SELECT DISTINCT(P.CASEID) FROM PERMISSIONSCASES P  "
                StrSql = StrSql + "WHERE P.CASEID IN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=CASE1)>0 THEN 0 ELSE NVL(CASE1,0) END ) "
                StrSql = StrSql + "FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=CASE2)>0 THEN 0 ELSE NVL(CASE2,0) END ) "
                StrSql = StrSql + "FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=CASE3)>0 THEN 0 ELSE NVL(CASE3,0) END ) "
                StrSql = StrSql + "FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=CASE4)>0 THEN 0 ELSE NVL(CASE4,0) END ) "
                StrSql = StrSql + "FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=CASE5)>0 THEN 0 ELSE NVL(CASE5,0) END ) "
                StrSql = StrSql + "FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT   (CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=CASE6)>0 THEN 0 ELSE NVL(CASE6,0) END ) "
                StrSql = StrSql + "FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT   (CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=CASE7)>0 THEN 0 ELSE NVL(CASE7,0) END ) "
                StrSql = StrSql + "FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT   (CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=CASE8)>0 THEN 0 ELSE NVL(CASE8,0) END ) "
                StrSql = StrSql + "FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT  (CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=CASE9)>0 THEN 0 ELSE NVL(CASE9,0) END ) "
                StrSql = StrSql + "FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT  (CASE WHEN(SELECT CASEID FROM BASECASES WHERE CASEID=CASE10)>0 THEN 0 ELSE NVL(CASE10,0) END )  AS C "
                StrSql = StrSql + "FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                StrSql = StrSql + ")"
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("S4GetAllPermissionCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function CheckUser(ByVal CaseId As String, ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim strCaseId As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "USERNAME "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + " WHERE CASEID= " + CaseId
                StrSql = StrSql + " AND USERID=" + UserId.ToString() + ""
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("S4CheckUser:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetAssumptionCaseId(ByVal AssumptionId As String) As String
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim strCaseid As String = String.Empty
            Dim Dts As New DataSet()
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ4ConnectionString")
            Try
                StrSql = "SELECT (CASE1 || ','|| CASE2 || ','|| CASE3 || ','|| CASE4  "
                StrSql = StrSql + "|| ','|| CASE5 || ','|| CASE6 || ','|| CASE7 || ','|| CASE8 "
                StrSql = StrSql + "|| ','|| CASE9 || ','|| CASE10) CASEID "
                StrSql = StrSql + "FROM ECON4.ASSUMPTIONS "
                StrSql = StrSql + "WHERE ASSUMPTIONID= " + AssumptionId.ToString()
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                strCaseid = Dts.Tables(0).Rows(0).Item("CASEID").ToString()
            Catch ex As Exception
                Throw New Exception("S4GetAssumptionCaseId:" + ex.Message.ToString())
            End Try
            Return strCaseid
        End Function
        Public Function GetCasesByCaseID(ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")

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
#Region "AssumptionsPages"
        Public Function Cases1(ByVal ID As String) As String()
            Dim CaseIDs() As String
            Dim i As New Integer
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ4ConnectionString")
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
        Public Function GetDepartment() As DataSet
            Dim Dts As New DataSet
            Try
                Dim StrSql As String = ""
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
                StrSql = "SELECT PROCID,(PROCDE1||' '||PROCDE2)PROCDE FROM PROCESS ORDER BY PROCDE"
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
            Catch ex As Exception
                Throw
            End Try
            Return Dts
        End Function
#End Region
#Region "Graphical Results"

        Public Function GetChartErgyRes(ByVal CaseIds() As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim strCaseId As String = String.Empty
            Dim i As Integer
            Try
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")

                For i = 0 To CaseIds.Length - 1
                    If i = 0 Then
                        strCaseId = CaseIds(i)
                    Else
                        strCaseId = strCaseId + "," + CaseIds(i)
                    End If

                Next
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
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = TOTAL.CASEID "
                StrSql = StrSql + "WHERE TOTAL.CASEID IN (" + strCaseId + ") "
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
                Return Dts
            Catch ex As Exception
                Throw New Exception("S1GetData:GetChartErgyRes:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetChartGhgRes(ByVal CaseIds() As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim strCaseId As String = String.Empty
            Dim i As Integer
            Try
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")
                For i = 0 To CaseIds.Length - 1
                    If i = 0 Then
                        strCaseId = CaseIds(i)
                    Else
                        strCaseId = strCaseId + "," + CaseIds(i)
                    End If

                Next
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
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = TOTAL.CASEID "
                StrSql = StrSql + "WHERE TOTAL.CASEID IN (" + strCaseId + ") "
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
                Return Dts
            Catch ex As Exception
                Throw New Exception("S1GetData:GetChartGhgRes:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetChartWaterRes(ByVal CaseIds() As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim strCaseId As String = String.Empty
            Dim i As Integer
            Try
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")
                For i = 0 To CaseIds.Length - 1
                    If i = 0 Then
                        strCaseId = CaseIds(i)
                    Else
                        strCaseId = strCaseId + "," + CaseIds(i)
                    End If

                Next
                StrSql = "SELECT  DISTINCT  "
                StrSql = StrSql + "TOTAL.CASEID, "
                StrSql = StrSql + "( CASE WHEN TOTAL.CASEID <= 1000 THEN "
                StrSql = StrSql + "( SELECT (BASECASES.CASEID||':'||BASECASES.CASEDE1||' '||BASECASES.CASEDE2 ) "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + "WHERE  BASECASES.CASEID = TOTAL.CASEID "
                StrSql = StrSql + ") ELSE "
                StrSql = StrSql + "( SELECT DISTINCT (PERMISSIONSCASES.CASEID||':'||NVL(PERMISSIONSCASES.CASEDE1,'') || ' ' || NVL(PERMISSIONSCASES.CASEDE2,'') ) "
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
                StrSql = StrSql + "WHERE TOTAL.CASEID IN (" + strCaseId + ") "
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
                Return Dts
            Catch ex As Exception
                Throw New Exception("S1GetData:GetChartWaterRes:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function



        Public Function ErgyCharts(ByVal CaseIds As String) As DataSet
            Dim Dts As New DataSet
            Try

                Dim StrSql As String = ""
                Dim odbUtil As New DBUtil()

                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")
                StrSql = "SELECT  DISTINCT  "
                StrSql = StrSql + "TOTAL.CASEID, "
                StrSql = StrSql + "( CASE WHEN TOTAL.CASEID <= 1000 THEN "
                StrSql = StrSql + "( SELECT (BASECASES.CASEID||':'||BASECASES.CASEDE1||' '||BASECASES.CASEDE2 ) "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + "WHERE  BASECASES.CASEID = TOTAL.CASEID "
                StrSql = StrSql + ") ELSE "
                StrSql = StrSql + "( SELECT DISTINCT (PERMISSIONSCASES.CASEID||':'||NVL(PERMISSIONSCASES.CASEDE1,'') || ' ' || NVL(PERMISSIONSCASES.CASEDE2,'') ) "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE  PERMISSIONSCASES.CASEID = TOTAL.CASEID "
                StrSql = StrSql + ") "
                StrSql = StrSql + "END  ) AS CASEDE, "
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
                StrSql = StrSql + "(RMERGY+RMPACKERGY+RMANDPACKTRNSPTERGY+PROCERGY+DPPACKERGY+DPTRNSPTERGY+TRSPTTOCUS)TOTALENERGY "
                StrSql = StrSql + "FROM TOTAL "
                StrSql = StrSql + "INNER JOIN RESULTSPL "
                StrSql = StrSql + "ON RESULTSPL.CASEID = TOTAL.CASEID "
                StrSql = StrSql + "WHERE TOTAL.CASEID IN (" + CaseIds + ") "

                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
            Catch ex As Exception
                Throw
            End Try
            Return Dts
        End Function

        Public Function GhgCharts(ByVal CaseIds As String) As DataSet
            Dim Dts As New DataSet
            Try

                Dim StrSql As String = ""
                Dim odbUtil As New DBUtil()

                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")
                StrSql = "SELECT  DISTINCT  "
                StrSql = StrSql + "TOTAL.CASEID, "
                StrSql = StrSql + "( CASE WHEN TOTAL.CASEID <= 1000 THEN "
                StrSql = StrSql + "( SELECT (BASECASES.CASEID||':'||BASECASES.CASEDE1||' '||BASECASES.CASEDE2 ) "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + "WHERE  BASECASES.CASEID = TOTAL.CASEID "
                StrSql = StrSql + ") ELSE "
                StrSql = StrSql + "( SELECT DISTINCT (PERMISSIONSCASES.CASEID||':'||NVL(PERMISSIONSCASES.CASEDE1,'') || ' ' || NVL(PERMISSIONSCASES.CASEDE2,'') ) "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE  PERMISSIONSCASES.CASEID = TOTAL.CASEID "
                StrSql = StrSql + ") "
                StrSql = StrSql + "END  ) AS CASEDE, "
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
                StrSql = StrSql + "(RMGRNHUSGAS+RMPACKGRNHUSGAS+RMANDPACKTRNSPTGRNHUSGAS+PROCGRNHUSGAS+DPPACKGRNHUSGAS+DPTRNSPTGRNHUSGAS+TRSPTTOCUSGRNHUSGAS)TOTALENGRNHUSGAS "
                StrSql = StrSql + "FROM TOTAL "
                StrSql = StrSql + "INNER JOIN RESULTSPL "
                StrSql = StrSql + "ON RESULTSPL.CASEID = TOTAL.CASEID "
                StrSql = StrSql + "WHERE TOTAL.CASEID IN (" + CaseIds + ") "

                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
            Catch ex As Exception
                Throw
            End Try
            Return Dts
        End Function

        Public Function WaterCharts(ByVal CaseIds As String) As DataSet
            Dim Dts As New DataSet
            Try

                Dim StrSql As String = ""
                Dim odbUtil As New DBUtil()

                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")
                StrSql = "SELECT  DISTINCT  "
                StrSql = StrSql + "TOTAL.CASEID, "
                StrSql = StrSql + "( CASE WHEN TOTAL.CASEID <= 1000 THEN "
                StrSql = StrSql + "( SELECT (BASECASES.CASEID||':'||BASECASES.CASEDE1||' '||BASECASES.CASEDE2 ) "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + "WHERE  BASECASES.CASEID = TOTAL.CASEID "
                StrSql = StrSql + ") ELSE "
                StrSql = StrSql + "( SELECT DISTINCT (PERMISSIONSCASES.CASEID||':'||NVL(PERMISSIONSCASES.CASEDE1,'') || ' ' || NVL(PERMISSIONSCASES.CASEDE2,'') ) "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE  PERMISSIONSCASES.CASEID = TOTAL.CASEID "
                StrSql = StrSql + ") "
                StrSql = StrSql + "END  ) AS CASEDE, "
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
                StrSql = StrSql + "(RMWATER+RMPACKWATER+RMANDPACKTRNSPTWATER+PROCWATER+DPPACKWATER+DPTRNSPTWATER+TRSPTTOCUSWATER)TOTALENWATER "
                StrSql = StrSql + "FROM TOTAL "
                StrSql = StrSql + "INNER JOIN RESULTSPL "
                StrSql = StrSql + "ON RESULTSPL.CASEID = TOTAL.CASEID "
                StrSql = StrSql + "WHERE TOTAL.CASEID IN (" + CaseIds + ") "

                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
            Catch ex As Exception
                Throw
            End Try
            Return Dts
        End Function

        Public Function ChartPref(ByVal UserName As String) As DataSet
            Dim Dts As New DataSet
            Try

                Dim StrSql As String = ""
                Dim odbUtil As New DBUtil()

                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

                StrSql = "SELECT  USERNAME,  "
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
                StrSql = StrSql + "CONVTHICK3 "
                StrSql = StrSql + "FROM CHARTPREFERENCES "
                StrSql = StrSql + "WHERE USERNAME='" + UserName + "' "


                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
            Catch ex As Exception
                Throw
            End Try
            Return Dts
        End Function

        Public Function ChartSetting() As DataSet
            Dim Dts As New DataSet
            Try

                Dim StrSql As String = ""
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
                StrSql = "SELECT EXTSERVERADD, INTCOMPORTADD FROM CHARTSETTING "
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
            Catch ex As Exception
                Throw
            End Try
            Return Dts
        End Function


#End Region
#Region "User Category"
        Public Function GetCategorySet(ByVal CatSetName As String, ByVal UserId As String, ByVal Pagename As String) As DataTable
            Dim Dts As New DataTable()
            Dim strsql As String = String.Empty
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Sustain4ConnectionString")
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
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Sustain4ConnectionString")
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
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Sustain4ConnectionString")
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
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Sustain4ConnectionString")
                If PageName = "ERGY" Then
                    strsql = "SELECT  'Raw Materials' DES,'RMERGY' CODE  FROM DUAL  "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Raw Materials Packaging' DES,'RMPACKERGY' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'RM & Pack Transport' DES,'RMANDPACKTRNSPTERGY' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Process' DES,'PROCERGY' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Distribution Packaging' DES,'DPPACKERGY' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'DP Transport' DES,'DPTRNSPTERGY' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Transport to Customer' DES,'TRSPTTOCUS' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Process Energy(S2)' DES,'PROCERGYS2' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Packaged Product Packaging(S2)' DES,'PACKGEDPRODPACK' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'PPP Transport(S2)' DES,'PPPTRNSPT' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Packaged Product Transport(S2)' DES,'PACKGEDPRODTRNSPT' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "

                    strsql = strsql + "SELECT 'Purchased Materials' DES,'PURMATERIALERGY' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Total Process' DES,'TPROCERGY' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Transportation' DES,'TRNSPTERGY' CODE  FROM DUAL "

                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Total Energy' DES,'TOTALENERGY' CODE  FROM DUAL "
                ElseIf PageName = "GHG" Then
                    strsql = "SELECT  'Raw Materials' DES,'RMGRNHUSGAS' CODE  FROM DUAL  "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Raw Materials Packaging' DES,'RMPACKGRNHUSGAS' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'RM & Pack Transport' DES,'RMANDPACKTRNSPTGRNHUSGAS' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Process' DES,'PROCGRNHUSGAS' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Distribution Packaging' DES,'DPPACKGRNHUSGAS' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'DP Transport' DES,'DPTRNSPTGRNHUSGAS' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Transport to Customer' DES,'TRSPTTOCUSGRNHUSGAS' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Process Energy(S2)' DES,'PROCGRNHUSGASS2' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Packaged Product Packaging(S2))' DES,'PACKGEDPRODPACKGRNHUSGAS' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'PPP Transport(S2)' DES,'PPPTRNSPTGRNHUSGAS' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Packaged Product Transport(S2)' DES,'PACKGEDPRODTRNSPTGRNHUSGAS' CODE  FROM DUAL "

                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Purchased Materials' DES,'PURMATERIALGRNHUSGAS' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Total Process' DES,'PROCESSGRNHUSGAS' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Transportation' DES,'TRNSPTGRNHUSGAS' CODE  FROM DUAL "

                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Total GHG' DES,'TOTALENGRNHUSGAS' CODE  FROM DUAL "
                ElseIf PageName = "WATER" Then
                    strsql = "SELECT  'Raw Materials' DES,'RMWATER' CODE  FROM DUAL  "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Raw Materials Packaging' DES,'RMPACKWATER' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'RM & Pack Transport' DES,'RMANDPACKTRNSPTWATER' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Process' DES,'PROCWATER' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Distribution Packaging' DES,'DPPACKWATER' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'DP Transport' DES,'DPTRNSPTWATER' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Transport to Customer' DES,'TRSPTTOCUSWATER' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Process Energy(S2)' DES,'PROCWATERS2' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Packaged Product Packaging(S2)' DES,'PACKGEDPRODPACKWATER' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'PPP Transport(S2)' DES,'PPPTRNSPTWATER' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Packaged Product Transport(S2)' DES,'PACKGEDPRODTRNSPTWATER' CODE  FROM DUAL "


                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Purchased Materials' DES,'PURMATERIALWATER' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Total Process' DES,'PROCESSWATER' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Transportation' DES,'TRNSPTWATER' CODE  FROM DUAL "
                    strsql = strsql + "UNION ALL "
                    strsql = strsql + "SELECT 'Total Water' DES,'TOTALENWATER' CODE  FROM DUAL "
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
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Sustain4ConnectionString")
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
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Sustain4ConnectionString")
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
#Region "Approval Model"
        Public Function GetCasesS4(ByVal UserId As String, ByVal LiceAdmin As String) As DataTable
            Dim Dts As New DataTable
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")
                Dim StrSql As String = ""
                If LiceAdmin = "Y" Then
                    StrSql = "SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2|| '     STATUS:'|| MS.STATUS) AS CASEDE,MS.STATUSID  FROM PERMISSIONSCASES INNER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID WHERE USERID=" + UserId.ToString() + " "
                Else
                    StrSql = "SELECT CASEID,CASEDE1, CASEDE,STATUSID FROM ("
                    StrSql = StrSql + "(SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2|| (CASE WHEN MS.STATUS IS NULL THEN '' ELSE   '     STATUS:'|| MS.STATUS END)) AS CASEDE,0 STATUSID FROM PERMISSIONSCASES LEFT OUTER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID WHERE USERID=" + UserId.ToString() + " ) "
                    StrSql = StrSql + "UNION (SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2|| '     STATUS:'|| MS.STATUS) AS CASEDE ,MS.STATUSID  FROM PERMISSIONSCASES  INNER JOIN ECON.USERS ON USERS.USERID=PERMISSIONSCASES.USERID "
                    StrSql = StrSql + "LEFT OUTER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                    StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS WHERE USERID=" + UserId.ToString() + ") "
                    StrSql = StrSql + "AND PERMISSIONSCASES.STATUSID IN(3,5)) "
                    StrSql = StrSql + ") ORDER BY CASEDE1"
                End If


                Dts = odbUtil.FillDataTable(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
        Public Function GetDelCasesS4(ByVal CaseID As String, ByVal UserId As String, ByVal LiceAdmin As String) As String
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")
            Try
                If LiceAdmin = "Y" Then
                    StrSql = "SELECT CASEID FROM PERMISSIONSCASES WHERE CASEID= " + CaseID + " AND USERID=" + UserId.ToString() + ""
                Else
                    StrSql = "SELECT CASEID FROM ("
                    StrSql = StrSql + "(SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE FROM PERMISSIONSCASES WHERE USERID=" + UserId.ToString() + " ) "
                    StrSql = StrSql + "UNION (SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE  FROM PERMISSIONSCASES  INNER JOIN ECON.USERS ON USERS.USERID=PERMISSIONSCASES.USERID "
                    StrSql = StrSql + "INNER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                    StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS WHERE USERID=" + UserId.ToString() + ") "
                    StrSql = StrSql + "AND PERMISSIONSCASES.STATUSID=3) "
                    StrSql = StrSql + ")  WHERE CASEID= " + CaseID + " "
                End If
                Dim Cs As New DataTable()
                Cs = odbUtil.FillDataTable(StrSql, MyConnectionString)

                If Cs.Rows.Count = 0 Then
                    Return CaseID
                End If
            Catch ex As Exception
                Throw New Exception("GetDelCasesS4:" + ex.Message.ToString())
            End Try
            Return ""
        End Function
        Public Function GetSisterCases(ByVal CaseID As String) As Boolean
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")
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
        Public Function GetAllPermissionCasesS4(ByVal AssumId As String, ByVal licAdmin As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim strCaseId As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")
            Try
                If licAdmin = "Y" Then
                    StrSql = "SELECT DISTINCT(P.CASEID) FROM PERMISSIONSCASES P  "
                    StrSql = StrSql + "WHERE P.CASEID IN "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + "SELECT NVL(CASE1,0) FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT NVL(CASE2,0) FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT NVL(CASE3,0) FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT NVL(CASE4,0) FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT NVL(CASE5,0) FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT NVL(CASE6,0) FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT NVL(CASE7,0) FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT NVL(CASE8,0) FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT NVL(CASE9,0) FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT NVL(CASE10,0) FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + ")"
                Else
                    StrSql = "SELECT DISTINCT(P.CASEID) FROM PERMISSIONSCASES P  "
                    StrSql = StrSql + "WHERE P.CASEID IN "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + "SELECT "
                    StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM PERMISSIONSCASES INNER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID  WHERE PERMISSIONSCASES.STATUSID=3 AND CASEID=CASE1)>0 THEN 0 ELSE NVL(CASE1,0) END ) "
                    StrSql = StrSql + "FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT "
                    StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM PERMISSIONSCASES INNER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID  WHERE PERMISSIONSCASES.STATUSID=3 AND  CASEID=CASE2)>0 THEN 0 ELSE NVL(CASE2,0) END ) "
                    StrSql = StrSql + "FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT "
                    StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM PERMISSIONSCASES INNER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID  WHERE PERMISSIONSCASES.STATUSID=3 AND  CASEID=CASE3)>0 THEN 0 ELSE NVL(CASE3,0) END ) "
                    StrSql = StrSql + "FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT "
                    StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM PERMISSIONSCASES INNER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID  WHERE PERMISSIONSCASES.STATUSID=3 AND  CASEID=CASE4)>0 THEN 0 ELSE NVL(CASE4,0) END ) "
                    StrSql = StrSql + "FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT "
                    StrSql = StrSql + "(CASE WHEN(SELECT CASEID FROM PERMISSIONSCASES INNER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID  WHERE PERMISSIONSCASES.STATUSID=3 AND  CASEID=CASE5)>0 THEN 0 ELSE NVL(CASE5,0) END ) "
                    StrSql = StrSql + "FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT   (CASE WHEN(SELECT CASEID FROM PERMISSIONSCASES INNER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID  WHERE PERMISSIONSCASES.STATUSID=3 AND  CASEID=CASE6)>0 THEN 0 ELSE NVL(CASE6,0) END ) "
                    StrSql = StrSql + "FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT   (CASE WHEN(SELECT CASEID FROM PERMISSIONSCASES INNER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID  WHERE PERMISSIONSCASES.STATUSID=3 AND  CASEID=CASE7)>0 THEN 0 ELSE NVL(CASE7,0) END ) "
                    StrSql = StrSql + "FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID=" + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT   (CASE WHEN(SELECT CASEID FROM PERMISSIONSCASES INNER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID  WHERE PERMISSIONSCASES.STATUSID=3 AND  CASEID=CASE8)>0 THEN 0 ELSE NVL(CASE8,0) END ) "
                    StrSql = StrSql + "FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT  (CASE WHEN(SELECT CASEID FROM PERMISSIONSCASES INNER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID  WHERE PERMISSIONSCASES.STATUSID=3 AND  CASEID=CASE9)>0 THEN 0 ELSE NVL(CASE9,0) END ) "
                    StrSql = StrSql + "FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT  (CASE WHEN(SELECT CASEID FROM PERMISSIONSCASES INNER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID  WHERE PERMISSIONSCASES.STATUSID=3 AND  CASEID=CASE10)>0 THEN 0 ELSE NVL(CASE10,0) END )  AS C "
                    StrSql = StrSql + "FROM ECON4.ASSUMPTIONS WHERE ASSUMPTIONID= " + AssumId
                    StrSql = StrSql + ")"
                End If

                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("GetAllPermissionCasesS4:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        
        Public Function GetEditCasesS4(ByVal UserId As String, ByVal ID As String, ByVal flag As String, ByVal LiceAdmin As String) As DataTable
            Dim Dts As New DataTable()
            Dim i As New Integer
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")

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
                    StrSql = StrSql + "UNION (SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2|| '     STATUS:'|| MS.STATUS) AS CASEDE  FROM PERMISSIONSCASES  INNER JOIN ECON.USERS ON USERS.USERID=PERMISSIONSCASES.USERID "
                    StrSql = StrSql + "LEFT OUTER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                    StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS WHERE USERID=" + UserId.ToString() + ") "
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
                StrSql = StrSql + "SELECT CASE1 AS CASE FROM  ECON4.ASSUMPTIONS WHERE ECON4.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE2 AS CASE FROM  ECON4.ASSUMPTIONS WHERE ECON4.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE3 AS CASE FROM  ECON4.ASSUMPTIONS WHERE ECON4.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE4 AS CASE FROM  ECON4.ASSUMPTIONS WHERE ECON4.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE5 AS CASE FROM  ECON4.ASSUMPTIONS WHERE ECON4.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE6 AS CASE FROM  ECON4.ASSUMPTIONS WHERE ECON4.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE7 AS CASE FROM  ECON4.ASSUMPTIONS WHERE ECON4.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE8 AS CASE FROM  ECON4.ASSUMPTIONS WHERE ECON4.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE9 AS CASE FROM  ECON4.ASSUMPTIONS WHERE ECON4.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CASE10 AS CASE FROM  ECON4.ASSUMPTIONS WHERE ECON4.ASSUMPTIONS.ASSUMPTIONID = " + ID + ""
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
