Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System

Public Class WebConf
    Public Class Selectdata
        Dim ShoppingConnection As String = System.Configuration.ConfigurationManager.AppSettings("ShoppingConnectionString")

        Public Function GetWebConfDetails() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT WEBCONFERENCEID,  "
                StrSql = StrSql + "LTRIM(RTRIM(CONFTOPIC)) AS CONFTOPIC, "
                StrSql = StrSql + "LTRIM(RTRIM(CONFDES)) AS CONFDES, "
                StrSql = StrSql + "LENGTH(LTRIM(RTRIM(CONFDES))) AS CONFDESLEN, "
                StrSql = StrSql + "TO_CHAR(CONFDATE,'Month DD, YYYY')CONFDATE, "
                StrSql = StrSql + "CONFDATE As CONFDATE1, "
                StrSql = StrSql + "LTRIM(RTRIM(CONFTIME)) AS CONFTIME, "
                StrSql = StrSql + "(CASE WHEN CONFCOST = 0 THEN 'Free' ELSE 'US$ '|| CONFCOST END)CONFCOST, "
                StrSql = StrSql + "CONFCOST AS CONFCOSTVAL, "
                StrSql = StrSql + "CONFFROMDATE, "
                StrSql = StrSql + "CONFTODATE "
                StrSql = StrSql + "FROM WEBCONFERENCES "
                StrSql = StrSql + "WHERE TRUNC(SYSDATE) BETWEEN CONFFROMDATE AND CONFTODATE "
                StrSql = StrSql + "ORDER BY CONFDATE1 "
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StudyGetData:GetStudyDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetWebConfDetailsById(ByVal WebId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT WEBCONFERENCEID,  "
                StrSql = StrSql + "LTRIM(RTRIM(CONFTOPIC)) AS CONFTOPIC, "
                StrSql = StrSql + "LTRIM(RTRIM(CONFDES)) AS CONFDES, "
                StrSql = StrSql + "LENGTH(LTRIM(RTRIM(CONFDES))) AS CONFDESLEN, "
                StrSql = StrSql + "TO_CHAR(CONFDATE,'Month DD, YYYY')CONFDATE, "
                StrSql = StrSql + "CONFDATE As CONFDATE1, "
                StrSql = StrSql + "LTRIM(RTRIM(CONFTIME)) AS CONFTIME, "
                StrSql = StrSql + "(CASE WHEN CONFCOST = 0 THEN 'Free' ELSE 'US$ '|| CONFCOST END)CONFCOST, "
                StrSql = StrSql + "CONFCOST AS CONFCOSTVAL, "
                StrSql = StrSql + "CONFFROMDATE, "
                StrSql = StrSql + "CONFTODATE "
                StrSql = StrSql + "FROM WEBCONFERENCES "
                StrSql = StrSql + "WHERE WEBCONFERENCEID='" + WebId + "'"
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StudyGetData:GetStudyDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetWebConfMailDetailsById(ByVal WebId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT WEBCONFERENCEID,  "
                StrSql = StrSql + "LTRIM(RTRIM(CONFTOPIC)) AS CONFTOPIC, "
                StrSql = StrSql + "LTRIM(RTRIM(CONFDES)) AS CONFDES, "
                StrSql = StrSql + "LENGTH(LTRIM(RTRIM(CONFDES))) AS CONFDESLEN, "
                StrSql = StrSql + "TO_CHAR(CONFDATE,'Month DD, YYYY')CONFDATE, "
                StrSql = StrSql + "LTRIM(RTRIM(CONFTIME)) AS CONFTIME, "
                StrSql = StrSql + "CONFCOST, "
                StrSql = StrSql + "CONFID, "
                StrSql = StrSql + "CONFKEY, "
                StrSql = StrSql + "CONFFROMDATE, "
                StrSql = StrSql + "CONFTODATE, "
                StrSql = StrSql + "CONFUNAMETEXT, "
                StrSql = StrSql + "CONFPWDTEXT, "
                StrSql = StrSql + "CASE WHEN CONFTYPE='M'THEN "
                StrSql = StrSql + "(CONFJOINLINK||'/join?id='||CONFID||'&'||'role=attend'||'&'||'pw='||CONFKEY) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CONFJOINLINK "
                StrSql = StrSql + "END CONFJOINLINK, "
                StrSql = StrSql + "CONFMANLINK, "
                StrSql = StrSql + "CONFPHONE, "
                StrSql = StrSql + "CONFACCESSCODE, "
                StrSql = StrSql + "CONFTYPE "
                StrSql = StrSql & "FROM WEBCONFERENCES "
                StrSql = StrSql + "WHERE WEBCONFERENCEID='" + WebId + "'"
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StudyGetData:GetStudyDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetWebConfAttDetails(ByVal RefId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                strsql = "select REFNUMBER,  "
                StrSql = StrSql + "WEBCONFID, "
                StrSql = StrSql + "USERCONTACTID, "
                StrSql = StrSql + "ADDRESSID, "
                StrSql = StrSql + "SEQ "
                StrSql = StrSql + "from WEBCONFATTENDI "
                StrSql = StrSql + "WHERE REFNUMBER='" + RefId + "' "
                StrSql = StrSql + "ORDER BY SEQ ASC "
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("GetWebConfAttDetails:GetWebConfAttDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
    End Class
End Class
