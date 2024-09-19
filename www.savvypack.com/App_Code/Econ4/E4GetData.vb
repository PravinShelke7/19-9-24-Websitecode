Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System

Public Class E4GetData
    Public Class Selectdata
        Public Function GetUsernamePassword(ByVal Id As String) As DataTable
            Dim Dts As New DataTable()
            Try
                Dim odbUtil As New DbUtil()
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


                Dim StrSql As String = "Select * from inuse where USERID=" + UserId.ToString() + " "
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
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
                Dim StrSql As String = "SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE FROM PERMISSIONSCASES WHERE USERID=" + UserId.ToString() + ""
                StrSql = StrSql + "UNION SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE  FROM BASECASES ORDER BY caseDE1"

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
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")

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
                StrSql = StrSql + "WHERE USERID = '" + UserId.ToString() + "' "
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
                StrSql = StrSql + " where UserPermissions.UserID =Users.UserID and  Services.SERVICEDE='ECON4'  "
                StrSql = StrSql + "AND USERS.LICENSEID=(SELECT LICENSEID FROM USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper.ToString() + "') "
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

        Public Function Cases(ByVal ID As String) As String()
            Dim CaseIDs() As String
            Dim i As New Integer
            Try

                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ4ConnectionString")

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
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("Econ4ConnectionString")

                StrSqlSaved = "SELECT distinct Assumptions.AssumptionId, "
                StrSqlSaved = StrSqlSaved + "Assumptions.DESCRIPTION,"
                StrSqlSaved = StrSqlSaved + "(nvl(Assumptions.Case1,0) || ', '||"
                StrSqlSaved = StrSqlSaved + "nvl(Assumptions.Case2,0) || ', '||"
                StrSqlSaved = StrSqlSaved + "nvl(Assumptions.Case3,0) || ', '||"
                StrSqlSaved = StrSqlSaved + "nvl(Assumptions.Case4,0) || ', '||"
                StrSqlSaved = StrSqlSaved + "nvl(Assumptions.Case5,0) || ', '||"
                StrSqlSaved = StrSqlSaved + "nvl(Assumptions.Case6,0) || ', '||"
                StrSqlSaved = StrSqlSaved + "nvl(Assumptions.Case7,0) || ', '||"
                StrSqlSaved = StrSqlSaved + "nvl(Assumptions.Case8,0) || ', '||"
                StrSqlSaved = StrSqlSaved + "nvl(Assumptions.Case9,0) || ', '||"
                StrSqlSaved = StrSqlSaved + "nvl(Assumptions.Case10,0))CaseIds, "
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
        Public Function GetEditCases(ByVal UserId As String, ByVal ID As String, ByVal flag As String) As DataTable
            Dim Dts As New DataTable()
            Dim i As New Integer
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")

                Dim StrSql As String = ""
                StrSql = "SELECT CASEID,CASEDE1,CASEDE FROM  "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT CASEID, "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE USERID=" + UserId.ToString() + " "
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
                StrSql = StrSql + ") ORDER BY CASEID ASC "
                Dts = odbUtil.FillDataTable(StrSql, MyConnectionString)
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
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
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
                StrSql = StrSql + "( SELECT (BASECASES.CASEID||':'||REPLACE(REPLACE((REPLACE(NVL(BASECASES.CASEDE1,''),')','}')),'(','{')||' '||REPLACE((REPLACE(NVL(BASECASES.CASEDE2,''),')','}')),'(','{'),';',':') )  "
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

                StrSql = StrSql + "RESULTSPL.VMATERIAL, "
                StrSql = StrSql + "RESULTSPL.VLABOR, "
                StrSql = StrSql + "RESULTSPL.VENERGY, "
                StrSql = StrSql + "RESULTSPL.VPACK, "
                StrSql = StrSql + "RESULTSPL.VSHIP, "
                StrSql = StrSql + "RESULTSPL.FIXEDCOST, "
                StrSql = StrSql + "RESULTSPL.FIXEDCOSTDEP, "

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
                StrSql = StrSql + "RESULTSPL.LAB, "
                StrSql = StrSql + "RESULTSPL.INKSUP, "
                StrSql = StrSql + "RESULTSPL.PLATESUP, "
                StrSql = StrSql + "RESULTSPL.metsup, "

                StrSql = StrSql + "DEP.DEPRECIATION AS DEPRECIATION "

                StrSql = StrSql + "FROM RESULTSPL "
                StrSql = StrSql + "INNER JOIN DEPRECIATION DEP "
                StrSql = StrSql + "ON DEP.CASEID=RESULTSPL.CASEID "
                StrSql = StrSql + "WHERE RESULTSPL.CASEID IN (" + strCaseId + ") "
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E4GetData:GetChartProfitAndLossRes:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetChartCostRes(ByVal CaseIds() As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim strCaseId As String = String.Empty
            Dim i As Integer
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
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
                StrSql = StrSql + "( SELECT (BASECASES.CASEID||':'||REPLACE(REPLACE((REPLACE(NVL(BASECASES.CASEDE1,''),')','}')),'(','{')||' '||REPLACE((REPLACE(NVL(BASECASES.CASEDE2,''),')','}')),'(','{'),';',':') )  "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + "WHERE  BASECASES.CASEID = RESULTSPL.CASEID "
                StrSql = StrSql + ") ELSE "
                StrSql = StrSql + "( SELECT DISTINCT (PERMISSIONSCASES.CASEID||':'||REPLACE(REPLACE((REPLACE(NVL(PERMISSIONSCASES.CASEDE1,''),')','}')),'(','{') || ' ' || REPLACE((REPLACE(NVL(PERMISSIONSCASES.CASEDE2,''),')','}')),'(','{'),';',':') )  "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE  PERMISSIONSCASES.CASEID = RESULTSPL.CASEID "
                StrSql = StrSql + ") "
                StrSql = StrSql + "END  ) AS CASEDE, "

                StrSql = StrSql + "RESULTSPL.FINVOLMUNITS, "
                StrSql = StrSql + "RESULTSPL.VOLUME, "
                StrSql = StrSql + "RESULTSPL.SVOLUME, "
                StrSql = StrSql + "RESULTSPL.PVOLUSE, "
                StrSql = StrSql + "RESULTSPL.FIXEDCOSTDEP, "
                StrSql = StrSql + "RESULTSPL.TOTALCOSTDEP, "
                StrSql = StrSql + "RESULTSPL.FIXEDCOST, "
                StrSql = StrSql + "RESULTSPL.TOTALCOST, "

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
                Throw New Exception("E4GetData:GetChartCostRes:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetROISQl(ByVal CaseIds As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
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
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
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
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
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

#Region "Global Manager"
        Public Function GetAllPermissionCases(ByVal AssumId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim strCaseId As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
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
                Throw New Exception("E3GetAllPermissionCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function CheckUser(ByVal CaseId As String, ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim strCaseId As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "USERID "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + " WHERE CASEID= " + CaseId
                StrSql = StrSql + " AND USERID=" + UserId.ToString() + ""
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E3CheckUser:" + ex.Message.ToString())
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
                Throw New Exception("GetAssumptionCaseId:" + ex.Message.ToString())
            End Try
            Return strCaseid
        End Function
        Public Function GetDelCases(ByVal CaseID As String, ByVal UserId As String) As String
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
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
        Public Function GetCasesByCaseID(ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")

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
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ4ConnectionString")
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
        Public Function GetCategorySetByID(ByVal CatSetID As String) As DataTable
            Dim Dts As New DataTable()
            Dim strsql As String = String.Empty
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ4ConnectionString")
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
        Public Function GetCategoryBySet(ByVal CatSetId As String, ByVal CatName As String) As DataTable
            Dim Dts As New DataTable()
            Dim strsql As String = String.Empty
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ4ConnectionString")
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
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ4ConnectionString")
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
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ4ConnectionString")
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
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ4ConnectionString")
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
#End Region
#Region "Approve Model"
        Public Function GetCasesE4(ByVal UserId As String, ByVal LiceAdmin As String) As DataTable
            Dim Dts As New DataTable
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
                Dim StrSql As String = ""
                If LiceAdmin = "Y" Then
                    StrSql = "SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2|| '     STATUS:'|| MS.STATUS) AS CASEDE,MS.STATUSID FROM PERMISSIONSCASES INNER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID WHERE USERID=" + UserId.ToString() + " "
                Else
                    StrSql = "SELECT CASEID,CASEDE1, CASEDE,STATUSID  FROM ("
                    StrSql = StrSql + "(SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2|| (CASE WHEN MS.STATUS IS NULL THEN '' ELSE   '     STATUS:'|| MS.STATUS END)) AS CASEDE,0 STATUSID FROM PERMISSIONSCASES LEFT OUTER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID WHERE USERID=" + UserId.ToString() + " ) "
                    StrSql = StrSql + "UNION (SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2|| '     STATUS:'|| MS.STATUS) AS CASEDE,MS.STATUSID  FROM PERMISSIONSCASES  INNER JOIN ECON.USERS ON USERS.USERID=PERMISSIONSCASES.USERID "
                    StrSql = StrSql + "INNER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
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
        Public Function GetDelCasesE4(ByVal CaseID As String, ByVal UserId As String, ByVal LiceAdmin As String) As String
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
            Try
                If LiceAdmin = "Y" Then
                    StrSql = "SELECT CASEID FROM PERMISSIONSCASES WHERE CASEID= " + CaseID + " AND USERID=" + UserId.ToString() + " "
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
                Throw New Exception("GetDelCasesE4:" + ex.Message.ToString())
            End Try
            Return ""
        End Function
        Public Function GetSisterCases(ByVal CaseID As String) As Boolean
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
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
        Public Function GetAllPermissionCasesE4(ByVal AssumId As String, ByVal licAdmin As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim strCaseId As String = String.Empty
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
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
                Throw New Exception("GetAllPermissionCasesE4:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        
        Public Function GetEditCasesE4(ByVal UserId As String, ByVal ID As String, ByVal flag As String, ByVal LiceAdmin As String) As DataTable
            Dim Dts As New DataTable()
            Dim i As New Integer
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")

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
                    StrSql = StrSql + "UNION (SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2|| '     STATUS:'|| MS.STATUS) AS CASEDE  FROM PERMISSIONSCASES INNER JOIN ECON.USERS ON USERS.USERID=PERMISSIONSCASES.USERID "
                    StrSql = StrSql + "LEFT OUTER JOIN ECON.MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                    StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS WHERE USERID=" + UserId.ToString() + ") "
                    StrSql = StrSql + "AND PERMISSIONSCASES.STATUSID IN (3,5)) "
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
