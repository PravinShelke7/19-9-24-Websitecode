Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Public Class StudyGetData
    Public Class Selectdata
        Dim ShoppingConnection As String = System.Configuration.ConfigurationManager.AppSettings("ShoppingConnectionString")
        Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
        Public Function GetStudyDetails(ByVal ReportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select ReportId,  "
                StrSql = StrSql + "ReportHeader, "
                StrSql = StrSql + "MetaData, "
                StrSql = StrSql + "ReportDetails, "
                StrSql = StrSql + "LinkText,ShortDes as ShortDescription, "
                StrSql = StrSql + "SHORTTITLE, "
                StrSql = StrSql + "IsNew,BROCHUREPAGES,TOCPAGES,ReportLength from Report Where "
                StrSql = StrSql + "NVL(ReportId,'#') LIKE '" + ReportId + "%' "
                StrSql = StrSql + " Order By ReportSeq DESC"
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StudyGetData:GetStudyDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetStudyDetailsByTitle(ByVal title As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select * from Report Where replace(RTRIM(LTRIM(ShortTitle)),'.','')='" + title.Replace("_", " ").Trim().Replace("#", ".").Trim() + "'"
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StudyGetData:GetStudyDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetInventoryDetails(ByVal ReportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT ID,  "
                StrSql = StrSql + "PARTID, "
                StrSql = StrSql + "PARTDES, "
                StrSql = StrSql + "PRICE, DELFORMAT, TO_CHAR(PUBDATE,'MON DD, YYYY')PUBDATE, "
                StrSql = StrSql + "COPYRIGHT, LINK, FORMATID, "
                StrSql = StrSql + "SEQUENCE FROM "
                StrSql = StrSql + "INVENTORY WHERE "
                StrSql = StrSql + "PARTID ='" + ReportId + "' "
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StudyGetData:GetStudyDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetInventoryLineDetails(ByVal ReportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  'Publication date' AS TITLE, TO_CHAR(PUBDATE,'MON DD, YYYY') AS TITLEDES, 1 AS SEQ FROM INVENTORY WHERE PARTID  = '" + ReportId + "' AND ROWNUM = 1  "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT  'Study number' AS TITLE, PARTID AS TITLEDES, 2 AS SEQ FROM INVENTORY  WHERE PARTID  = '" + ReportId + "' AND ROWNUM = 1 "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT  'Copyright' AS TITLE, COPYRIGHT AS TITLEDES, 3 AS SEQ FROM INVENTORY WHERE PARTID  = '" + ReportId + "' AND ROWNUM = 1 "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT INVENTORY.DELFORMAT AS TITLE, ('US$ '||INVENTORY.PRICE) AS TITLEDES , SEQUENCE+3 AS SEQ FROM INVENTORY WHERE PARTID  = '" + ReportId + "' "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Report Length' AS TITLE,REPORT.REPORTLENGTH AS TITLEDES, 99999 AS SEQ FROM REPORT WHERE REPORT.REPORTID='" + ReportId + "' AND ROWNUM = 1 "
                StrSql = StrSql + "ORDER BY SEQ "

                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StudyGetData:GetInventoryLineDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetSamplePageDetails(ByVal ReportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PartID,PARTDES,SAMPLEPAGES FROM INVENTORY WHERE "
                StrSql = StrSql + "PARTID ='" + ReportId + "'  AND ROWNUM=1"
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("StudyGetData:GetSamplePageDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUserDetails(ByVal userId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "USERS.ISPROMOMAIL, "
                StrSql = StrSql + "USERCONTACTID, "
                StrSql = StrSql + "USERCONTACTS.USERID, "
                StrSql = StrSql + "PREFIX, "
                StrSql = StrSql + "FIRSTNAME, "
                StrSql = StrSql + "LASTNAME,(PREFIX||' '||FIRSTNAME||' '||LASTNAME)NAME,nvl(USERS.IsValidEmail,'N')IsValidEmail, "
                StrSql = StrSql + "JOBTITLE, "
                StrSql = StrSql + "EMAILADDRESS, "
                StrSql = StrSql + "PHONENUMBER, "
                StrSql = StrSql + "FAXNUMBER, "
                'StrSql = StrSql + "COMPANYNAME, "
                StrSql = StrSql + "COMPANY.COMPANYNAME COMPANYNAME, "
                StrSql = StrSql + "STREETADDRESS1, "
                StrSql = StrSql + "STREETADDRESS2, "
                StrSql = StrSql + "CITY, "
                StrSql = StrSql + "STATE, "
                StrSql = StrSql + "ZIPCODE, "
                StrSql = StrSql + "COUNTRY "
                StrSql = StrSql + "FROM USERCONTACTS "
                StrSql = StrSql + "INNER JOIN USERS "
                StrSql = StrSql + "ON USERS.USERID=USERCONTACTS.USERID "
                StrSql = StrSql + "LEFT OUTER JOIN SHOPPING.COMPANY "
                StrSql = StrSql + "ON COMPANY.COMPANYID=USERS.COMPANYID "
                StrSql = StrSql + "WHERE USERCONTACTS.USERID  = '" + userId + "'"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:ValidateUser:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function


        Public Function GetUser(ByVal userId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "USERS.USERID, "
                StrSql = StrSql + "USERS.USERNAME, "
                'StrSql = StrSql + "USERS.COMPANY "
                StrSql = StrSql + "(CASE WHEN COMPANY.COMPANYNAME='' THEN '' ELSE COMPANY.COMPANYNAME END)COMPANY "
                StrSql = StrSql + "FROM USERS "
                StrSql = StrSql + "LEFT OUTER JOIN SHOPPING.COMPANY "
                StrSql = StrSql + "ON COMPANY.COMPANYID=USERS.COMPANYID "
                StrSql = StrSql + "WHERE USERS.USERID  = '" + userId + "'"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:ValidateUser:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function


    End Class

End Class
