Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System

Public Class UsersGetData
    Public Class Selectdata
        Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
        Dim ConfigurationConnection As String = System.Configuration.ConfigurationManager.AppSettings("ConfigurationConnectionString")
        Dim ShoppingConnection As String = System.Configuration.ConfigurationManager.AppSettings("ShoppingConnectionString")
#Region "Login Details"
        Public Function ValidateUser(ByVal UserName As String, ByVal Password As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select USERID,  "
                StrSql = StrSql + "USERNAME, "
                StrSql = StrSql + "PASSWORD, "
                StrSql = StrSql + "LICENSENO,USERS.ISINTERNALUSR, "
                'StrSql = StrSql + "COMPANY, "
                StrSql = StrSql + "(CASE WHEN COMPANY.COMPANYNAME='' THEN '' ELSE COMPANY.COMPANYNAME END)COMPANY, "
                StrSql = StrSql + "ISVALIDEMAIL,"
                StrSql = StrSql + "VERFCODE,"
                StrSql = StrSql + "TO_CHAR(VCREATIONDATE,'MON DD, YYYY')CDATE, "
                StrSql = StrSql + "ISAPPROVED,UTYPE,"
                StrSql = StrSql + "VERIFYDATE,"
                'Bug#389
                StrSql = StrSql + "VUPDATEDATE, "
                'Bug389
                StrSql = StrSql + "STATUSID,ISINTERNALUSR,"
                StrSql = StrSql + "TO_CHAR(ACCCHECKDATE,'MON DD, YYYY')ACCCHECKDATE "
                StrSql = StrSql + "from users "
                StrSql = StrSql + "LEFT OUTER JOIN SHOPPING.COMPANY "
                StrSql = StrSql + "ON COMPANY.COMPANYID=USERS.COMPANYID "
                StrSql = StrSql + "WHERE Upper(username)='" + UserName.ToUpper() + "'"
                StrSql = StrSql + " AND password='" + Password + "' "
                'StrSql = StrSql + " AND NVL(USERS.ISVALIDEMAIL,'N')<>'N'"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:ValidateUser:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function ValidateUserWithCode(ByVal UserName As String, ByVal Password As String, ByVal VerfCode As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select USERS.USERID,  "
                StrSql = StrSql + "USERNAME, "
                StrSql = StrSql + "PASSWORD, "
                StrSql = StrSql + "LICENSEID, "
                'StrSql = StrSql + "COMPANY, "
                StrSql = StrSql + "COMPANY.COMPANYNAME COMPANY, "
                StrSql = StrSql + "ISVALIDEMAIL,"
                StrSql = StrSql + "VERFCODE,"
                StrSql = StrSql + "TO_CHAR(VCREATIONDATE,'MON DD, YYYY')CDATE, "
                StrSql = StrSql + "EMAILADDRESS, "
                StrSql = StrSql + "USERCONTACTID, "
                StrSql = StrSql + "(PREFIX||' '||FIRSTNAME||' '||LASTNAME)NAME "

                StrSql = StrSql + "from USERS "
                StrSql = StrSql + "INNER JOIN USERCONTACTS "
                StrSql = StrSql + "ON USERS.USERID=USERCONTACTS.USERID "
                StrSql = StrSql + "LEFT OUTER JOIN SHOPPING.COMPANY "
                StrSql = StrSql + "ON COMPANY.COMPANYID=USERS.COMPANYID "
                StrSql = StrSql + "WHERE Upper(username)='" + UserName.ToUpper() + "'"
                StrSql = StrSql + " AND password='" + Password + "' "
                StrSql = StrSql + " AND VERFCODE='" + VerfCode + "' "

                Dts = odbUtil.FillDataSet(StrSql, EconConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:ValidateUser:" + ex.Message.ToString())
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
                StrSql = StrSql + "USERS.USERNAME, "
				StrSql = StrSql + "USERS.VUPDATEDATE, "
                StrSql = StrSql + "USERCONTACTS.MOBILENUMBER,"
                StrSql = StrSql + "COMPANY.COMPANYNAME,COMPANY.COMPANYID,COMPANY.ISNEW, "
                StrSql = StrSql + "LICENSEID, "
                StrSql = StrSql + "STREETADDRESS1, "
                StrSql = StrSql + "STREETADDRESS2, "
                StrSql = StrSql + "CITY, "
                StrSql = StrSql + "STATE, "
                StrSql = StrSql + "ZIPCODE, "
                StrSql = StrSql + "COUNTRY COUNTRYID,"
                StrSql = StrSql + "(CASE WHEN COUNTRY=0 THEN  '' ELSE DIMCOUNTRIES.COUNTRYDES END) COUNTRY "
                StrSql = StrSql + "FROM USERCONTACTS "
                StrSql = StrSql + "INNER JOIN USERS "
                StrSql = StrSql + "ON USERS.USERID=USERCONTACTS.USERID "
                StrSql = StrSql + "INNER JOIN ADMINSITE.DIMCOUNTRIES "
                StrSql = StrSql + "ON DIMCOUNTRIES.COUNTRYID=USERCONTACTS.COUNTRY "
                StrSql = StrSql + "LEFT OUTER JOIN SHOPPING.COMPANY "
                StrSql = StrSql + "ON COMPANY.COMPANYID=USERS.COMPANYID "
                StrSql = StrSql + "WHERE USERCONTACTS.USERID  = '" + userId + "'"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:GetUserDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUserCount(ByVal userName As String) As Integer
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim count As Integer
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT Count(1) As Cnt FROM USERS WHERE upper(USERS.USERNAME)= '" + userName.ToUpper() + "'"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                count = CInt(Dts.Tables(0).Rows(0).Item("Cnt"))
                Return count
            Catch ex As Exception
                Throw New Exception("UsersGetData:GetUserCount:" + ex.Message.ToString())
                Return count
            End Try
        End Function
        Public Function GetCountry() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT COUNTRYID,  "
                StrSql = StrSql + "COUNTRYDES "
                StrSql = StrSql + "FROM ADMINSITE.DIMCOUNTRIES "
                StrSql = StrSql + " ORDER BY UPPER(COUNTRYDES)"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetState() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT STATEID, "
                StrSql = StrSql + "COUNTRYID, "
                StrSql = StrSql + "NAME "
                StrSql = StrSql + "FROM ADMINSITE.STATE "
                StrSql = StrSql + " ORDER BY UPPER(NAME)"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetStateByDes(ByVal stateDes As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT STATEID, "
                StrSql = StrSql + "COUNTRYID, "
                StrSql = StrSql + "NAME "
                StrSql = StrSql + "FROM ADMINSITE.STATE "
                StrSql = StrSql + "WHERE UPPER(NAME)='" + stateDes.ToUpper() + "' "
                StrSql = StrSql + " ORDER BY UPPER(NAME)"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        '10th_Jan_2018
        Public Function GetStateByCountry(ByVal CountryID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT STATEID, "
                StrSql = StrSql + "COUNTRYID, "
                StrSql = StrSql + "NAME "
                StrSql = StrSql + "FROM ADMINSITE.STATE "
                StrSql = StrSql + "WHERE COUNTRYID=" + CountryID + " "
                StrSql = StrSql + " ORDER BY UPPER(NAME)"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        'End

        Public Function GetUserAddresDetailByID(ByVal UserID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty

            Try
                StrSql = "SELECT USERADDRESSID,  "
                StrSql = StrSql + "USERNAME, "
                StrSql = StrSql + "ADDHEADER,"
                StrSql = StrSql + "PREFIX, "
                StrSql = StrSql + "FIRSTNAME, "
                StrSql = StrSql + "LASTNAME, "
                StrSql = StrSql + "PREFIX ||' '||FIRSTNAME||' '||LASTNAME AS FULLNAME, "
                StrSql = StrSql + "STREETADDRESS1, "
                StrSql = StrSql + "STREETADDRESS2, "
                StrSql = StrSql + "STREETADDRESS1 ||' '|| STREETADDRESS2 ADDRESS, "
                StrSql = StrSql + "PHONENUMBER, "
                StrSql = StrSql + "CITY, "
                StrSql = StrSql + "STATE, "
                StrSql = StrSql + "ZIPCODE, "
                StrSql = StrSql + "CO.COUNTRYDES, "
                StrSql = StrSql + "EMAILADDRESS, "
                StrSql = StrSql + "FAXNUMBER, "
                StrSql = StrSql + "COMPANYNAME, "
                StrSql = StrSql + "JOBTITLE "
                StrSql = StrSql + "FROM USERADDRESS UA "
                StrSql = StrSql + "LEFT JOIN ADMINSITE.DIMCOUNTRIES CO ON UA.COUNTRY=CO.COUNTRYID "
                StrSql = StrSql + "WHERE USERADDRESSID=" + UserID
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)

                Return Dts

            Catch ex As Exception
                Throw New Exception("UsersGetData:GetUserAddresDetailByID:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetEmailConfigDetails(ByVal IsPrimary As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT SMTP,  "
                StrSql = StrSql + "URL, "
                StrSql = StrSql + "SMTPUNAME, "
                StrSql = StrSql + "SMTPPWD, "
                StrSql = StrSql + "ISAUNTH, "
                StrSql = StrSql + "HTTPSURL, "
                StrSql = StrSql + "SITE, "
                StrSql = StrSql + "SMTPPORT, "
                StrSql = StrSql + "SMTPISSSL "
                StrSql = StrSql + "FROM SAVVYEMAILCONFIG "
                StrSql = StrSql + "WHERE ISPRIMARY='" + IsPrimary + "' "
                Dts = odbUtil.FillDataSet(StrSql, ConfigurationConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:GetEmailConfigDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetEmailConfigDetailsByOrder() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT SMTP,  "
                StrSql = StrSql + "URL, "
                StrSql = StrSql + "SMTPUNAME, "
                StrSql = StrSql + "SMTPPWD, "
                StrSql = StrSql + "ISAUNTH, "
                StrSql = StrSql + "HTTPSURL, "
                StrSql = StrSql + "SITE, "
                StrSql = StrSql + "SMTPPORT, "
                StrSql = StrSql + "SMTPISSSL, "
                StrSql = StrSql + "SEQ "
                StrSql = StrSql + "FROM SAVVYEMAILCONFIG "
                StrSql = StrSql + "ORDER BY SEQ ASC "
                Dts = odbUtil.FillDataSet(StrSql, ConfigurationConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:GetEmailConfigDetailsByOrder:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUserId(ByVal userName As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                   StrSql = "SELECT USERS.USERID,  "
                StrSql = StrSql + "USERS.USERNAME, "
                StrSql = StrSql + "USERS.PASSWORD, "
                StrSql = StrSql + "USERCONTACTS.EMAILADDRESS EMAILADD, "
                StrSql = StrSql + "USERCONTACTS.FIRSTNAME FNAME, "
                StrSql = StrSql + "(PREFIX||' '||FIRSTNAME||' '||LASTNAME)NAME, "
                StrSql = StrSql + "USERCONTACTID "
                StrSql = StrSql + "FROM USERS "
                StrSql = StrSql + "INNER JOIN USERCONTACTS "
                StrSql = StrSql + "ON USERCONTACTS.USERID=USERS.USERID "
                StrSql = StrSql + "WHERE Upper(USERS.USERNAME)=  '" + userName.ToUpper() + "'"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:GetUserId:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetAlliedMemberMail(ByVal code As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select * from SAVVYPADMINMAIL  "
                StrSql = StrSql + "WHERE CODE='" + code.Trim() + "' "
                Dts = odbUtil.FillDataSet(StrSql, ConfigurationConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:GetUserId:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function ValidateUserPWD(ByVal UserID As String, ByVal Password As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERS.USERID,  "
                StrSql = StrSql + "USERS.USERNAME, "
                StrSql = StrSql + "USERS.PASSWORD, "
                StrSql = StrSql + "USERCONTACTS.EMAILADDRESS EMAILADD, "
                StrSql = StrSql + "USERCONTACTS.FIRSTNAME FNAME "
                StrSql = StrSql + " FROM USERS "
                StrSql = StrSql + "INNER JOIN USERCONTACTS "
                StrSql = StrSql + "ON USERCONTACTS.USERID=USERS.USERID "
                StrSql = StrSql + "WHERE Upper(USERS.USERID)=" + UserID.ToString() + " "
                StrSql = StrSql + " AND USERS.password='" + Password + "' "

                Dts = odbUtil.FillDataSet(StrSql, EconConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:ValidateUserPWD:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetBillToShipToUserDetailsByAddID(ByVal UID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERADDRESSID,  "
                StrSql = StrSql + "USERNAME, "
                StrSql = StrSql + "ADDHEADER,"
                StrSql = StrSql + "PREFIX, "
                StrSql = StrSql + "FIRSTNAME, "
                StrSql = StrSql + "LASTNAME, "
                StrSql = StrSql + "PREFIX ||' '||FIRSTNAME||' '||LASTNAME AS FULLNAME, "
                StrSql = StrSql + "STREETADDRESS1, "
                StrSql = StrSql + "STREETADDRESS2, "
                StrSql = StrSql + "JOBTITLE, "
                StrSql = StrSql + "COMPANYNAME, "
                StrSql = StrSql + "FAXNUMBER, "
                StrSql = StrSql + "CITY, "
                StrSql = StrSql + "STATE, "
                StrSql = StrSql + "PHONENUMBER, "
                StrSql = StrSql + "COUNTRY, "
                StrSql = StrSql + "ZIPCODE, "
                StrSql = StrSql + "CO.COUNTRYID, "
                StrSql = StrSql + "CO.COUNTRYDES, "
                StrSql = StrSql + "EMAILADDRESS, "
                StrSql = StrSql + "LOGINUSERID "
                StrSql = StrSql + "FROM USERADDRESS UA "
                StrSql = StrSql + "LEFT JOIN ADMINSITE.DIMCOUNTRIES CO ON UA.COUNTRY=CO.COUNTRYID "
                StrSql = StrSql + "WHERE USERADDRESSID=" + UID
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:GetBillToShipToUserDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUserAddById(ByVal userAddId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try


                StrSql = "SELECT  "
                StrSql = StrSql + "U.USERID, "
                StrSql = StrSql + "PREFIX, "
                StrSql = StrSql + "PREFIX||' '||FIRSTNAME ||' '||LASTNAME USERNAME, "
                StrSql = StrSql + "FIRSTNAME, "
                StrSql = StrSql + "LASTNAME, "
                StrSql = StrSql + "JOBTITLE, "
                StrSql = StrSql + "EMAILADDRESS, "
                StrSql = StrSql + "PHONENUMBER, "
                StrSql = StrSql + "FAXNUMBER, "
                'StrSql = StrSql + "COMPANYNAME, "
                StrSql = StrSql + "(CASE WHEN COMPANY.COMPANYNAME='' THEN '' ELSE COMPANY.COMPANYNAME END)COMPANYNAME, "
                StrSql = StrSql + "STREETADDRESS1, "
                StrSql = StrSql + "STREETADDRESS2, "
                StrSql = StrSql + "CITY, "
                StrSql = StrSql + "STATE, "
                StrSql = StrSql + "ZIPCODE, "
                StrSql = StrSql + "COUNTRY, "
                StrSql = StrSql + "COUNTRYDES, "
                StrSql = StrSql + "U.USERNAME LOGINNAME "
                StrSql = StrSql + "FROM USERCONTACTS UC "
                StrSql = StrSql + "INNER JOIN USERS U ON U.USERID=UC.USERID "
                StrSql = StrSql + "LEFT OUTER JOIN SHOPPING.COMPANY "
                StrSql = StrSql + "ON COMPANY.COMPANYID=U.COMPANYID "
                StrSql = StrSql + "INNER JOIN ADMINSITE.DIMCOUNTRIES CO "
                StrSql = StrSql + "ON UC.COUNTRY=CO.COUNTRYID "
                StrSql = StrSql + "WHERE UC.USERID= " + userAddId


                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:GetUserAddById:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUserAddressByUserID(ByVal LogInUserID As String, ByVal isWebC As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERADDRESSID,  "
                StrSql = StrSql + "USERNAME, "
                StrSql = StrSql + "ADDHEADER,"
                StrSql = StrSql + "PREFIX, "
                StrSql = StrSql + "FIRSTNAME, "
                StrSql = StrSql + "LASTNAME, "
                StrSql = StrSql + "PREFIX ||' '||FIRSTNAME||' '||LASTNAME AS FULLNAME, "
                StrSql = StrSql + "STREETADDRESS1, "
                StrSql = StrSql + "STREETADDRESS2, "
                StrSql = StrSql + "STREETADDRESS1 ||' '|| STREETADDRESS2 ADDRESS, "
                StrSql = StrSql + "PHONENUMBER, "
                StrSql = StrSql + "CITY, "
                StrSql = StrSql + "STATE, "
                StrSql = StrSql + "ZIPCODE, "
                StrSql = StrSql + "CO.COUNTRYDES, "
                StrSql = StrSql + "EMAILADDRESS, "
                StrSql = StrSql + "FAXNUMBER, "
                StrSql = StrSql + "COMPANYNAME, "
                StrSql = StrSql + "JOBTITLE "
                StrSql = StrSql + "FROM USERADDRESS UA "
                StrSql = StrSql + "LEFT JOIN ADMINSITE.DIMCOUNTRIES CO ON UA.COUNTRY=CO.COUNTRYID "
                StrSql = StrSql + "WHERE LOGINUSERID=" + LogInUserID
                If isWebC = "Y" Then
                    StrSql = StrSql + "UNION  "
                    StrSql = StrSql + "SELECT 0 AS USERADDRESSID, "
                    StrSql = StrSql + "'No One Selected' as USERNAME, "
                    StrSql = StrSql + "'NA' ADDHEADER, "
                    StrSql = StrSql + "'NA' PREFIX, "
                    StrSql = StrSql + "'NA' FIRSTNAME, "
                    StrSql = StrSql + "'NA' LASTNAME, "
                    StrSql = StrSql + "'NA' AS FULLNAME, "
                    StrSql = StrSql + "'NA' STREETADDRESS1, "
                    StrSql = StrSql + "'NA' STREETADDRESS2, "
                    StrSql = StrSql + "'NA' ADDRESS, "
                    StrSql = StrSql + "'NA' PHONENUMBER, "
                    StrSql = StrSql + "'NA' CITY, "
                    StrSql = StrSql + "'NA' STATE, "
                    StrSql = StrSql + "'NA' ZIPCODE, "
                    StrSql = StrSql + "'NA' COUNTRYDES, "
                    StrSql = StrSql + "'NA' EMAILADDRESS, "
                    StrSql = StrSql + "'NA' FAXNUMBER, "
                    StrSql = StrSql + "'NA' COMPANYNAME, "
                    StrSql = StrSql + "'NA' JOBTITLE "
                    StrSql = StrSql + "FROM DUAL "
                End If
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:GetBillToShipToUserDetailsByAddID:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUserAddressByNameID(ByVal userName As String, ByVal LogInUserID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERADDRESSID,  "
                StrSql = StrSql + "USERNAME, "
                StrSql = StrSql + "ADDHEADER,"
                StrSql = StrSql + "FIRSTNAME, "
                StrSql = StrSql + "LASTNAME, "
                StrSql = StrSql + "STREETADDRESS1, "
                StrSql = StrSql + "CITY, "
                StrSql = StrSql + "STATE, "
                StrSql = StrSql + "ZIPCODE, "
                StrSql = StrSql + "CO.COUNTRYDES, "
                StrSql = StrSql + "EMAILADDRESS "
                StrSql = StrSql + "FROM USERADDRESS UA "
                StrSql = StrSql + "LEFT JOIN ADMINSITE.DIMCOUNTRIES CO ON UA.COUNTRY=CO.COUNTRYID "
                StrSql = StrSql + "WHERE LOGINUSERID=" + LogInUserID + " "
                StrSql = StrSql + "AND  UPPER(USERNAME)='" + userName.ToUpper() + "' "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:GetUserAddressByNameID:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUserAddressByNameHeadID(ByVal userName As String, ByVal headerText As String, ByVal logInUserID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT * FROM USERADDRESS  "
                StrSql = StrSql + "WHERE Upper(USERNAME)=  '" + userName.ToUpper() + "' AND UPPER(ADDHEADER)='" + headerText.ToUpper() + "' AND LOGINUSERID=" + logInUserID
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:GetUserAddressByNameHeadID:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUserAddressIDByHeadID(ByVal headerText As String, ByVal logInUserID As String) As Integer
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim id As Integer
            Try
                StrSql = "SELECT USERADDRESSID FROM USERADDRESS  "
                StrSql = StrSql + "WHERE  UPPER(ADDHEADER)='" + headerText.ToUpper() + "' AND LOGINUSERID=" + logInUserID
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                If Dts.Tables(0).Rows.Count > 0 Then
                    id = Dts.Tables(0).Rows(0)("USERADDRESSID").ToString()
                End If
                Return id
            Catch ex As Exception
                Throw New Exception("UsersGetData:GetUserAddressByName:" + ex.Message.ToString())
                Return 0
            End Try
        End Function
        Public Function GetAddresNameByID(ByVal UserID As String) As String
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim strName As String = ""
            Try
                StrSql = "SELECT USERADDRESSID,  "
                StrSql = StrSql + "USERNAME, "
                StrSql = StrSql + "ADDHEADER,"
                StrSql = StrSql + "PREFIX, "
                StrSql = StrSql + "FIRSTNAME, "
                StrSql = StrSql + "LASTNAME, "
                StrSql = StrSql + "PREFIX ||' '||FIRSTNAME||' '||LASTNAME AS FULLNAME, "
                StrSql = StrSql + "STREETADDRESS1, "
                StrSql = StrSql + "STREETADDRESS2, "
                StrSql = StrSql + "STREETADDRESS1 ||' '|| STREETADDRESS2 ADDRESS, "
                StrSql = StrSql + "PHONENUMBER, "
                StrSql = StrSql + "CITY, "
                StrSql = StrSql + "STATE, "
                StrSql = StrSql + "ZIPCODE, "
                StrSql = StrSql + "CO.COUNTRYDES, "
                StrSql = StrSql + "EMAILADDRESS, "
                StrSql = StrSql + "FAXNUMBER, "
                StrSql = StrSql + "COMPANYNAME, "
                StrSql = StrSql + "JOBTITLE "
                StrSql = StrSql + "FROM USERADDRESS UA "
                StrSql = StrSql + "LEFT JOIN ADMINSITE.DIMCOUNTRIES CO ON UA.COUNTRY=CO.COUNTRYID "
                StrSql = StrSql + "WHERE USERADDRESSID=" + UserID
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                If Dts.Tables(0).Rows.Count > 0 Then
                    strName = Dts.Tables(0).Rows(0).Item("UserName").ToString()
                    Return strName
                End If
            Catch ex As Exception
                Throw New Exception("UsersGetData:GetUserAddressByNameAndUserID:" + ex.Message.ToString())
                Return strName
            End Try
        End Function
#End Region

#Region "Email Config"
        Public Function GetEmailConfig() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select SMTP,  "
                StrSql = StrSql + "URL, "
                StrSql = StrSql + "SMTPUNAME, "
                StrSql = StrSql + "ISAUNTH, "
                StrSql = StrSql + "HTTPSURL "
                StrSql = StrSql + "FROM SAVVYEMAILCONFIG "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:GetEmailConfig:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region
#Region "New User Creation"
        Public Function GetAUserCount(ByVal userName As String, ByVal Type As String) As Integer
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim count As Integer
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT Count(1) As Cnt FROM USERS WHERE upper(USERS.USERNAME)= '" + userName.ToUpper() + "' "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                count = CInt(Dts.Tables(0).Rows(0).Item("Cnt"))
                Return count
            Catch ex As Exception
                Throw New Exception("UsersGetData:GetAUserCount:" + ex.Message.ToString())
                Return count
            End Try
        End Function
#End Region
#Region "New User Creation August3-2017"
        Public Function CheckUserExist(ByVal userName As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim count As Integer
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERS.STATUSID,STATUS.DETAILS,USERS.ISVALIDEMAIL,USERS.ISAPPROVED FROM USERS INNER JOIN STATUS ON STATUS.STATUSID=USERS.STATUSID "
                StrSql = StrSql + "WHERE UPPER(USERS.USERNAME)= '" + userName.ToUpper() + "' "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:CheckUserExist:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUserIdData(ByVal userName As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERS.USERID,  "
                StrSql = StrSql + "USERS.USERNAME, "
                StrSql = StrSql + "USERS.PASSWORD, "
                StrSql = StrSql + "USERCONTACTS.EMAILADDRESS EMAILADD, "
                StrSql = StrSql + "USERCONTACTS.FIRSTNAME FNAME,USERS.STATUSID, "
                StrSql = StrSql + "STATUS.DETAILS "
                StrSql = StrSql + "FROM USERS "
                StrSql = StrSql + "INNER JOIN USERCONTACTS "
                StrSql = StrSql + "ON USERCONTACTS.USERID=USERS.USERID "
                StrSql = StrSql + "INNER JOIN STATUS "
                StrSql = StrSql + "ON STATUS.STATUSID=USERS.STATUSID "
                StrSql = StrSql + "WHERE Upper(USERS.USERNAME)=  '" + userName.ToUpper() + "'"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:GetUserId:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUDetails(ByVal UName As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "USERS.ISPROMOMAIL, "
                StrSql = StrSql + "USERS.USERNAME, "
                StrSql = StrSql + "USERCONTACTID, "
                StrSql = StrSql + "USERCONTACTS.USERID, "
                StrSql = StrSql + "PREFIX, "
                StrSql = StrSql + "FIRSTNAME, "
                StrSql = StrSql + "LASTNAME,(PREFIX||' '||FIRSTNAME||' '||LASTNAME)NAME,nvl(USERS.IsValidEmail,'N')IsValidEmail, "
                StrSql = StrSql + "JOBTITLE, "
                StrSql = StrSql + "EMAILADDRESS, "
                StrSql = StrSql + "PHONENUMBER, "
                StrSql = StrSql + "MOBILENUMBER, "
                StrSql = StrSql + "FAXNUMBER, "
                StrSql = StrSql + "(CASE WHEN COMPANY.COMPANYNAME='' THEN '' ELSE COMPANY.COMPANYNAME END)COMPANYNAME,COMPANY.COMPANYID,COMPANY.ISNEW, "
                StrSql = StrSql + "LICENSEID, "
                StrSql = StrSql + "STREETADDRESS1, "
                StrSql = StrSql + "STREETADDRESS2, "
                StrSql = StrSql + "CITY, "
                StrSql = StrSql + "STATE, "
                StrSql = StrSql + "ZIPCODE, "
                StrSql = StrSql + "COUNTRY COUNTRYID,"
                StrSql = StrSql + "(CASE WHEN COUNTRY=0 THEN  '' ELSE DIMCOUNTRIES.COUNTRYDES END) COUNTRY "
                StrSql = StrSql + "FROM USERCONTACTS "
                StrSql = StrSql + "INNER JOIN USERS "
                StrSql = StrSql + "ON USERS.USERID=USERCONTACTS.USERID "
                StrSql = StrSql + "LEFT OUTER JOIN SHOPPING.COMPANY "
                StrSql = StrSql + "ON COMPANY.COMPANYID=USERS.COMPANYID "
                StrSql = StrSql + "INNER JOIN ADMINSITE.DIMCOUNTRIES "
                StrSql = StrSql + "ON DIMCOUNTRIES.COUNTRYID=USERCONTACTS.COUNTRY "
                StrSql = StrSql + "WHERE UPPER(USERS.USERNAME)  = '" + UName.ToUpper() + "'"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:ValidateUser:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Verify Domain & Company"


        Public Function ValidateUserIDCode(ByVal UserID As String, ByVal VerfCode As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select USERS.USERID,  "
                StrSql = StrSql + "USERNAME, "
                StrSql = StrSql + "PASSWORD, "
                StrSql = StrSql + "LICENSEID, "
                'StrSql = StrSql + "COMPANY, "
                StrSql = StrSql + "(CASE WHEN COMPANY.COMPANYNAME='' THEN '' ELSE COMPANY.COMPANYNAME END)COMPANY,COMPANY.ISNEW,COMPANY.COMPANYID,  "
                StrSql = StrSql + "ISVALIDEMAIL,"
                StrSql = StrSql + "VERFCODE,"
                StrSql = StrSql + "ISAPPROVED,UTYPE,"
                StrSql = StrSql + "VERIFYDATE,"
                StrSql = StrSql + "TO_CHAR(VCREATIONDATE,'MON DD, YYYY')CDATE, "
                StrSql = StrSql + "EMAILADDRESS, "
                StrSql = StrSql + "USERCONTACTID, "
                StrSql = StrSql + "(PREFIX||' '||FIRSTNAME||' '||LASTNAME)NAME "

                StrSql = StrSql + "from USERS "
                StrSql = StrSql + "INNER JOIN USERCONTACTS "
                StrSql = StrSql + "ON USERS.USERID=USERCONTACTS.USERID "
                StrSql = StrSql + "LEFT OUTER JOIN SHOPPING.COMPANY "
                StrSql = StrSql + "ON COMPANY.COMPANYID=USERS.COMPANYID "
                StrSql = StrSql + "WHERE USERS.USERID='" + UserID + "' "
                If VerfCode <> "" Then
                    StrSql = StrSql + "AND VERFCODE='" + VerfCode + "' "
                End If


                Dts = odbUtil.FillDataSet(StrSql, EconConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:ValidateUser:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function


        Public Function GetDomainFlag(ByVal DomainName As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT * FROM DOMAIN WHERE UPPER(DOMAINNAME)='" + DomainName.Replace("'", "''").ToUpper() + "' "
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("GetData:GetDomainFlag:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCompanyList(ByVal DomainID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                'StrSql = "SELECT "
                'StrSql = StrSql + "COMPANYID,COMPANYNAME,PARENTID FROM COMPANY "
                'StrSql = StrSql + "WHERE COMPANYID IN "
                'StrSql = StrSql + "(SELECT COMPANYID FROM COMPANYDOMAIN WHERE DOMAINID=" + DomainID + ")"
                'Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)

                'If Dts.Tables(0).Rows(0).Item("PARENTID") <> 0 Then
                '    StrSql = String.Empty
                '    StrSql = "SELECT COMPANYID,COMPANYNAME,PARENTID FROM COMPANY "
                '    StrSql = StrSql + "WHERE PARENTID IN(SELECT PARENTID FROM COMPANY WHERE COMPANYID IN "
                '    StrSql = StrSql + "(SELECT COMPANYID FROM COMPANYDOMAIN WHERE DOMAINID=" + DomainID + ")) "
                '    StrSql = StrSql + "UNION ALL "
                '    StrSql = StrSql + "SELECT COMPANYID,COMPANYNAME,PARENTID FROM COMPANY WHERE COMPANYID IN( "
                '    StrSql = StrSql + "SELECT PARENTID FROM COMPANY WHERE COMPANYID IN "
                '    StrSql = StrSql + "(SELECT COMPANYID FROM COMPANYDOMAIN WHERE DOMAINID=" + DomainID + ")) "
                '    Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                'Else
                '    StrSql = String.Empty
                '    StrSql = StrSql + "SELECT "
                '    StrSql = StrSql + "COMPANYID,COMPANYNAME,PARENTID FROM COMPANY "
                '    StrSql = StrSql + "WHERE PARENTID='" + Dts.Tables(0).Rows(0).Item("COMPANYID").ToString() + "' "
                '    StrSql = StrSql + "OR COMPANYID='" + Dts.Tables(0).Rows(0).Item("COMPANYID").ToString() + "' "
                '    Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                'End If

                StrSql = "SELECT * FROM COMPANY  "
                StrSql = StrSql + "WHERE COMPANYID IN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT COMPANYID FROM COMPANYDOMAIN WHERE DOMAINID=" + DomainID + " "
                StrSql = StrSql + ") "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT * FROM COMPANY "
                StrSql = StrSql + "WHERE PARENTID IN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT COMPANYID FROM COMPANYDOMAIN WHERE DOMAINID=" + DomainID + " "
                StrSql = StrSql + ") "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT * FROM COMPANY "
                StrSql = StrSql + "WHERE COMPANYID IN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT PARENTID FROM COMPANY "
                StrSql = StrSql + "WHERE COMPANYID IN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT COMPANYID FROM COMPANYDOMAIN WHERE DOMAINID=" + DomainID + " "
                StrSql = StrSql + ") "
                StrSql = StrSql + ") "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT * FROM COMPANY "
                StrSql = StrSql + "WHERE PARENTID IN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT COMPANYID FROM COMPANY "
                StrSql = StrSql + "WHERE COMPANYID IN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT PARENTID FROM COMPANY "
                StrSql = StrSql + "WHERE COMPANYID IN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT COMPANYID FROM COMPANYDOMAIN WHERE DOMAINID=" + DomainID + " "
                StrSql = StrSql + ") "
                StrSql = StrSql + ") "
                StrSql = StrSql + ") "
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("GetData:GetCompanyList:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function CheckCompanyExist(ByVal CompanyName As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT * FROM COMPANY WHERE UPPER(COMPANYNAME)='" + CompanyName.Replace("'", "''").ToUpper() + "' "
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("GetData:CheckCompanyExist:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetComapnyID() As String
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim CId As String = String.Empty
            Try
                StrSql = "SELECT SEQCOMPANYID.NEXTVAL AS ID  "
                StrSql = StrSql + "FROM DUAL "

                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                CId = Dts.Tables(0).Rows(0).Item("ID")
                Return CId
            Catch ex As Exception
                Throw New Exception("GetData:GetComapnyID:" + ex.Message.ToString())
                Return CId
            End Try
        End Function
		
		 Public Function CheckNewCompany(ByVal CompanyID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT * FROM COMPANY WHERE COMPANYID='" + CompanyID + "' "
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("GetData:CheckNewCompany:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        '10th_jan_2018
        Public Function CheckCountryInState(ByVal CountryID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT * FROM ADMINSITE.STATE WHERE COUNTRYID='" + CountryID + "' "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("GetData:CheckCountryInState:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

#End Region

#Region "AddEditAccount"

        Public Function GetUsrInfo(ByVal userName As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERS.USERID,  "
                StrSql = StrSql + "USERS.USERNAME, "
                StrSql = StrSql + "USERS.PASSWORD, "
                StrSql = StrSql + "USERS.UTYPE,"
                StrSql = StrSql + "STATUS.DETAILS "
                StrSql = StrSql + "FROM USERS "
                StrSql = StrSql + "INNER JOIN USERCONTACTS "
                StrSql = StrSql + "ON USERCONTACTS.USERID=USERS.USERID "
                StrSql = StrSql + "INNER JOIN STATUS "
                StrSql = StrSql + "ON STATUS.STATUSID=USERS.STATUSID "
                StrSql = StrSql + "WHERE Upper(USERS.USERNAME)=  '" + userName.Replace("'", "''").ToUpper() + "'"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:GetUsrInfo:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

#End Region


#Region "Retrieve Password"

        Public Function GetUserIDByEmailID(ByVal UserNm As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim UserID As String = String.Empty
            Try
                StrSql = "SELECT "
                StrSql = StrSql + "U.USERID,U.USERNAME,UC.FIRSTNAME,UC.LASTNAME,UC.EMAILADDRESS,U.VERIFYDATE "
                StrSql = StrSql + "FROM USERS U "
                StrSql = StrSql + "INNER JOIN USERCONTACTS UC ON UC.USERID=U.USERID "
                StrSql = StrSql + "WHERE UPPER(U.USERNAME)='" + UserNm.ToUpper().ToString() + "'"

                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:GetEmailConfig:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPwdVerfCode(ByVal UserID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT "
                StrSql = StrSql + "USERID,PWDVERIFYCODE,PWDVERIFYDATE "
                StrSql = StrSql + "FROM PWDVERIFICATION "
                StrSql = StrSql + "WHERE USERID=" + UserID.ToString()

                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:GetEmailConfig:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function CheckVerfCodeByUserID(ByVal UserID As String, ByVal VrfyCode As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT "
                StrSql = StrSql + "USERID,PWDVERIFYCODE,PWDVERIFYDATE "
                StrSql = StrSql + "FROM PWDVERIFICATION "
                StrSql = StrSql + "WHERE USERID=" + UserID.ToString() + ""
                StrSql = StrSql + "AND PWDVERIFYCODE='" + VrfyCode.Replace("'", "''").ToString() + "'"

                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:GetEmailConfig:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUserInfoByUID(ByVal UserID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT "
                StrSql = StrSql + "U.USERID,U.USERNAME,UC.FIRSTNAME,UC.LASTNAME,UC.EMAILADDRESS,U.PASSWORD "
                StrSql = StrSql + "FROM USERS U "
                StrSql = StrSql + "INNER JOIN USERCONTACTS UC ON UC.USERID=U.USERID "
                StrSql = StrSql + "WHERE U.USERID=" + UserID.ToString()

                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:GetUserInfoByUID:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

#End Region

    End Class
End Class
