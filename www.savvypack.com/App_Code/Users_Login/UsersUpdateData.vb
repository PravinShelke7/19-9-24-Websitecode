Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Public Class UsersUpdateData
    Public Class UpdateInsert
        Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
        Dim ContractCon As String = System.Configuration.ConfigurationManager.AppSettings("ContractConnectionString")
        Dim SBAConnection As String = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
        Dim ShoppingConnection As String = System.Configuration.ConfigurationManager.AppSettings("ShoppingConnectionString")

        Public Sub LoginLogOutLogInsert(ByVal UserName As String, ByVal SessionId As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String

            Try
                StrSql = "Insert into LOGINLOGOUTLOG (USERNAME,LOGIN,SESSIONID)  "
                StrSql = StrSql + "VALUES('" + UserName + "',SYSDATE,'" + SessionId + "')"
                odbUtil.UpIns(StrSql, EconConnection)

            Catch ex As Exception
                Throw New Exception("UsersUpdateData:LoginLogOutLogInsert:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub InsertUserDetails(ByVal UserName As String, ByVal password As String, ByVal CompanyID As String, ByVal Prefix As String, _
                                     ByVal FirstName As String, ByVal LastName As String, ByVal Position As String, ByVal PhoneNumber As String, _
                                     ByVal FaxNumber As String, ByVal StreetAddress1 As String, ByVal StreetAddress2 As String, _
                                     ByVal City As String, ByVal State As String, ByVal ZipCode As String, ByVal Country As String, _
                                     ByVal VerfCode As String, ByVal CompanyName As String, ByVal DStatusID As String, ByVal IsApproved As String, _
                                     ByVal MobileNum As String, ByVal DomainID As String, ByVal CompanyStatus As Boolean)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String

            Try
                'Bug#389
                'Inserting Into User Table
                StrSql = "INSERT INTO USERS  "
                StrSql = StrSql + "(USERID,USERNAME,PASSWORD,COMPANYID,VERFCODE,ISVALIDEMAIL,VERIFYDATE,VCREATIONDATE,VUPDATEDATE,LOCKDATE,STATUSID,ISAPPROVED) "
                StrSql = StrSql + "SELECT SEQUSERID.NEXTVAL, "
                StrSql = StrSql + "'" + UserName + "', "
                StrSql = StrSql + "'" + password + "', "
                StrSql = StrSql + "'" + CompanyID + "', "
                
                'If DStatusID = 1 Then
                '    StrSql = StrSql + "'#',"
                '    StrSql = StrSql + "'Y',"
                '    StrSql = StrSql + "NULL,"
                'Else
                '    StrSql = StrSql + "'" + VerfCode + "',"
                '    StrSql = StrSql + "NULL,"
                '    StrSql = StrSql + "SYSDATE,"
                'End If

                'Bug#PK20_1
                StrSql = StrSql + "'" + VerfCode + "',"
                StrSql = StrSql + "NULL,"
                StrSql = StrSql + "SYSDATE,"
                'EndBug#PK20_1

                StrSql = StrSql + "SYSDATE,SYSDATE,CURRENT_Timestamp-interval '17' minute,1 ,'" + IsApproved + "' "
                'Bug#389
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 FROM "
                StrSql = StrSql + "USERS "
                StrSql = StrSql + "WHERE Upper(USERS.USERNAME) = '" + UserName.ToUpper() + "' "
                StrSql = StrSql + ") "
                odbUtil.UpIns(StrSql, EconConnection)

                'Inserting Into UserContacts Table
                StrSql = "INSERT INTO USERCONTACTS  "
                StrSql = StrSql + "(USERCONTACTID, "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "PREFIX, "
                StrSql = StrSql + "FIRSTNAME, "
                StrSql = StrSql + "LASTNAME, "
                StrSql = StrSql + "JOBTITLE, "
                StrSql = StrSql + "EMAILADDRESS, "
                StrSql = StrSql + "PHONENUMBER,MOBILENUMBER, "
                StrSql = StrSql + "FAXNUMBER, "
                ' StrSql = StrSql + "COMPANYNAME, "
                StrSql = StrSql + "STREETADDRESS1, "
                StrSql = StrSql + "STREETADDRESS2, "
                StrSql = StrSql + "CITY, "
                StrSql = StrSql + "STATE, "
                StrSql = StrSql + "ZIPCODE, "
                StrSql = StrSql + "COUNTRY) "
                StrSql = StrSql + "SELECT SEQUSERCONTACTID.NEXTVAL, "
                StrSql = StrSql + "(SELECT USERS.USERID FROM USERS WHERE Upper(USERS.USERNAME)='" + UserName.ToUpper() + "'), "
                StrSql = StrSql + "'" + Prefix + "', "
                StrSql = StrSql + "'" + FirstName + "', "
                StrSql = StrSql + "'" + LastName + "', "
                StrSql = StrSql + "'" + Position + "', "
                StrSql = StrSql + "'" + UserName + "', "
                StrSql = StrSql + "'" + PhoneNumber + "','" + MobileNum + "', "
                StrSql = StrSql + "'" + FaxNumber + "', "
                'StrSql = StrSql + "'" + CompanyName + "', "
                StrSql = StrSql + "'" + StreetAddress1 + "', "
                StrSql = StrSql + "'" + StreetAddress2 + "', "
                StrSql = StrSql + "'" + City + "', "
                StrSql = StrSql + "'" + State + "', "
                StrSql = StrSql + "'" + ZipCode + "', "
                StrSql = StrSql + "'" + Country + "' "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 FROM "
                StrSql = StrSql + "USERCONTACTS "
                StrSql = StrSql + "WHERE USERCONTACTS.USERID = (SELECT USERS.USERID FROM USERS WHERE Upper(USERS.USERNAME)='" + UserName.ToUpper() + "') "
                StrSql = StrSql + ") "
                odbUtil.UpIns(StrSql, EconConnection)

                'Bug#389
                StrSql = "INSERT INTO  PASSWORDLOG(USERID,PASSWORD,SEQUENCE) "
                StrSql = StrSql + " VALUES((select userid from users where upper(USERNAME)='" + UserName.ToUpper() + "' AND PASSWORD='" + password + "'),'" + password + "',1)"
                odbUtil.UpIns(StrSql, EconConnection)
                'Bug#389

                'Bug#379
                'StrSql = "INSERT INTO INKPREFERENCES  "
               ' StrSql = StrSql + "(USERNAME) "
               ' StrSql = StrSql + "VALUES('" + UserName.Trim() + "')"
               ' odbUtil.UpIns(StrSql, EconConnection)

 	    StrSql = "INSERT INTO INKPREFERENCES  "
            StrSql = StrSql + "(USERID) "
            StrSql = StrSql + "VALUES((SELECT USERID FROM USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "' AND PASSWORD='" + password + "'))"
            odbUtil.UpIns(StrSql, EconConnection)
                'Bug#379

                'Inserting into UserEmailBlast
                StrSql = "INSERT INTO USEREMAILBLAST "
                StrSql = StrSql + "(USERID) "
                StrSql = StrSql + "SELECT (SELECT USERS.USERID FROM USERS WHERE Upper(USERS.USERNAME)='" + UserName.ToUpper() + "') FROM DUAL "
                odbUtil.UpIns(StrSql, EconConnection)

                'Inserting into Company
                StrSql = "INSERT INTO COMPANY "
                StrSql = StrSql + "(COMPANYID,COMPANYNAME,PARENTID,ISNEW) "
                StrSql = StrSql + "SELECT '" + CompanyID + "','" + CompanyName + "',0,'Y' FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 FROM COMPANY "
                StrSql = StrSql + "WHERE COMPANYID='" + CompanyID + "' AND UPPER(COMPANYNAME)='" + CompanyName.ToUpper() + "'"
                StrSql = StrSql + ")"
                odbUtil.UpIns(StrSql, ShoppingConnection)

                'Inserting CompanyDomain Combination in CompanyDomain Table
                If CompanyStatus = True Then
                    StrSql = String.Empty
                    StrSql = "INSERT INTO COMPANYDOMAIN "
                    StrSql = StrSql + "(COMPANYDOMAINID,COMPANYID,DOMAINID) "
                    StrSql = StrSql + "SELECT SEQCOMPDOMID.NEXTVAL,'" + CompanyID + "','" + DomainID + "' FROM DUAL "
                    StrSql = StrSql + "WHERE NOT EXISTS "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + "SELECT 1 FROM COMPANYDOMAIN "
                    StrSql = StrSql + "WHERE COMPANYID='" + CompanyID + "' AND DOMAINID='" + DomainID + "' "
                    StrSql = StrSql + ")"
                    odbUtil.UpIns(StrSql, ShoppingConnection)
                End If

            Catch ex As Exception
                Throw New Exception("UsersUpdateData:InsertUserDetails:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub UpdateUserDetails(ByVal UserId As String, ByVal UserName As String, ByVal CompanyID As String, ByVal Prefix As String, _
                                     ByVal FirstName As String, ByVal LastName As String, ByVal Position As String, ByVal PhoneNumber As String, _
                                     ByVal FaxNumber As String, ByVal StreetAddress1 As String, ByVal StreetAddress2 As String, ByVal City As String, _
                                     ByVal State As String, ByVal ZipCode As String, ByVal Country As String, ByVal PromoMail As String, _
                                     ByVal MobileNumber As String, ByVal CompanyName As String, ByVal DomainID As String, _
          ByVal CompanyStatus As Boolean, ByVal DomainStatus As Boolean)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Try
                'Update USERCONTACTS
                StrSql = "UPDATE USERCONTACTS  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "USERCONTACTS.PREFIX='" + Prefix + "', "
                StrSql = StrSql + "USERCONTACTS.FIRSTNAME='" + FirstName + "', "
                StrSql = StrSql + "USERCONTACTS.LASTNAME='" + LastName + "', "
                StrSql = StrSql + "USERCONTACTS.JOBTITLE='" + Position + "', "
                StrSql = StrSql + "USERCONTACTS.PHONENUMBER='" + PhoneNumber + "', "
                StrSql = StrSql + "USERCONTACTS.MOBILENUMBER='" + MobileNumber + "', "
                StrSql = StrSql + "USERCONTACTS.FAXNUMBER='" + FaxNumber + "', "
                ' StrSql = StrSql + "USERCONTACTS.COMPANYNAME='" + CompanyName + "', "
                StrSql = StrSql + "USERCONTACTS.STREETADDRESS1='" + StreetAddress1 + "', "
                StrSql = StrSql + "USERCONTACTS.STREETADDRESS2='" + StreetAddress2 + "', "
                StrSql = StrSql + "USERCONTACTS.CITY='" + City + "', "
                StrSql = StrSql + "USERCONTACTS.STATE='" + State + "', "
                StrSql = StrSql + "USERCONTACTS.ZIPCODE = '" + ZipCode + "', "
                StrSql = StrSql + "USERCONTACTS.COUNTRY = '" + Country + "' "
                StrSql = StrSql + "WHERE USERCONTACTS.USERID=" + UserId + " "
                odbUtil.UpIns(StrSql, EconConnection)

                'Update Company
                StrSql = String.Empty
                StrSql = "UPDATE USERS "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "USERS.COMPANYID='" + CompanyID + "',USERS.STATUSID=1 "
                StrSql = StrSql + "WHERE USERS.USERID=" + UserId + ""
                odbUtil.UpIns(StrSql, EconConnection)

                'Inserting Into UserContacts Table
                StrSql = String.Empty
                StrSql = "INSERT INTO USERCONTACTS  "
                StrSql = StrSql + "(USERCONTACTID, "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "PREFIX, "
                StrSql = StrSql + "FIRSTNAME, "
                StrSql = StrSql + "LASTNAME, "
                StrSql = StrSql + "JOBTITLE, "
                StrSql = StrSql + "EMAILADDRESS, "
                StrSql = StrSql + "PHONENUMBER,MOBILENUMBER, "
                StrSql = StrSql + "FAXNUMBER, "
                'StrSql = StrSql + "COMPANYNAME, "
                StrSql = StrSql + "STREETADDRESS1, "
                StrSql = StrSql + "STREETADDRESS2, "
                StrSql = StrSql + "CITY, "
                StrSql = StrSql + "STATE, "
                StrSql = StrSql + "ZIPCODE, "
                StrSql = StrSql + "COUNTRY) "
                StrSql = StrSql + "SELECT SEQUSERCONTACTID.NEXTVAL, "
                StrSql = StrSql + UserId + ", "
                StrSql = StrSql + "'" + Prefix + "', "
                StrSql = StrSql + "'" + FirstName + "', "
                StrSql = StrSql + "'" + LastName + "', "
                StrSql = StrSql + "'" + Position + "', "
                StrSql = StrSql + "'" + UserName + "', "
                StrSql = StrSql + "'" + PhoneNumber + "', "
                StrSql = StrSql + "'" + MobileNumber + "', "
                StrSql = StrSql + "'" + FaxNumber + "', "
                'StrSql = StrSql + "'" + CompanyName + "', "
                StrSql = StrSql + "'" + StreetAddress1 + "', "
                StrSql = StrSql + "'" + StreetAddress2 + "', "
                StrSql = StrSql + "'" + City + "', "
                StrSql = StrSql + "'" + State + "', "
                StrSql = StrSql + "'" + ZipCode + "', "
                StrSql = StrSql + "'" + Country + "' "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 FROM "
                StrSql = StrSql + "USERCONTACTS "
                StrSql = StrSql + "WHERE USERCONTACTS.USERID =" + UserId + ""
                StrSql = StrSql + ") "
                odbUtil.UpIns(StrSql, EconConnection)

                'Updating User Table
                StrSql = String.Empty
                StrSql = "UPDATE USERS SET ISPROMOMAIL = '" + PromoMail + "' WHERE  USERID=" + UserId + ""
                odbUtil.UpIns(StrSql, EconConnection)

                'UPDATE UserAddress Table
                StrSql = String.Empty
                StrSql = "UPDATE USERADDRESS UA SET "
                StrSql = StrSql + "UA.PREFIX='" + Prefix + "', "
                StrSql = StrSql + "UA.FIRSTNAME='" + FirstName + "', "
                StrSql = StrSql + "UA.LASTNAME='" + LastName + "', "
                StrSql = StrSql + "UA.JOBTITLE='" + Position + "', "
                StrSql = StrSql + "UA.EMAILADDRESS='" + UserName + "', "
                StrSql = StrSql + "UA.PHONENUMBER='" + PhoneNumber + "', "
                StrSql = StrSql + "UA.FAXNUMBER='" + FaxNumber + "', "
                StrSql = StrSql + "UA.COMPANYNAME='" + CompanyName + "', "
                StrSql = StrSql + "UA.STREETADDRESS1='" + StreetAddress1 + "', "
                StrSql = StrSql + "UA.STREETADDRESS2='" + StreetAddress2 + "', "
                StrSql = StrSql + "UA.CITY='" + City + "', "
                StrSql = StrSql + "UA.STATE='" + State + "', "
                StrSql = StrSql + "UA.ZIPCODE = '" + ZipCode + "', "
                StrSql = StrSql + "UA.COUNTRY = '" + Country + "' "
                StrSql = StrSql + "WHERE UA.LOGINUSERID=" + UserId + " "
                StrSql = StrSql + "AND UA.ADDHEADER='Login User' "
                StrSql = StrSql + "AND UA.USERNAME=(SELECT USERNAME FROM USERS WHERE USERID=" + UserId + ")"
                odbUtil.UpIns(StrSql, EconConnection)



                'Inserting into Company
                StrSql = "INSERT INTO COMPANY "
                StrSql = StrSql + "(COMPANYID,COMPANYNAME,PARENTID,ISNEW) "
                StrSql = StrSql + "SELECT '" + CompanyID + "','" + CompanyName + "',0,'Y' FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 FROM COMPANY "
                StrSql = StrSql + "WHERE COMPANYID='" + CompanyID + "' AND UPPER(COMPANYNAME)='" + CompanyName.ToUpper() + "'"
                StrSql = StrSql + ")"
                odbUtil.UpIns(StrSql, ShoppingConnection)

                'Inserting CompanyDomain Combination in CompanyDomain Table
                If CompanyStatus = True Or DomainStatus = True Then
                    StrSql = String.Empty
                    StrSql = "INSERT INTO COMPANYDOMAIN "
                    StrSql = StrSql + "(COMPANYDOMAINID,COMPANYID,DOMAINID) "
                    StrSql = StrSql + "SELECT SEQCOMPDOMID.NEXTVAL,'" + CompanyID + "','" + DomainID + "' FROM DUAL "
                    StrSql = StrSql + "WHERE NOT EXISTS "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + "SELECT 1 FROM COMPANYDOMAIN "
                    StrSql = StrSql + "WHERE COMPANYID='" + CompanyID + "' AND DOMAINID='" + DomainID + "' "
                    StrSql = StrSql + ")"
                    odbUtil.UpIns(StrSql, ShoppingConnection)
                End If

            Catch ex As Exception
                Throw New Exception("UsersUpdateData:UpdateUserDetails:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub UpdateUserDetails_OLD(ByVal UserId As String, ByVal UserName As String, ByVal CompanyID As String, ByVal Prefix As String, _
                                     ByVal FirstName As String, ByVal LastName As String, ByVal Position As String, ByVal PhoneNumber As String, _
                                     ByVal FaxNumber As String, ByVal StreetAddress1 As String, ByVal StreetAddress2 As String, ByVal City As String, _
                                     ByVal State As String, ByVal ZipCode As String, ByVal Country As String, ByVal PromoMail As String, _
                                     ByVal MobileNumber As String, ByVal CompanyName As String, ByVal DomainID As String, _ 
									 ByVal CompanyStatus As Boolean,ByVal DomainStatus As Boolean)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Try
                'Update USERCONTACTS
                StrSql = "UPDATE USERCONTACTS  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "USERCONTACTS.PREFIX='" + Prefix + "', "
                StrSql = StrSql + "USERCONTACTS.FIRSTNAME='" + FirstName + "', "
                StrSql = StrSql + "USERCONTACTS.LASTNAME='" + LastName + "', "
                StrSql = StrSql + "USERCONTACTS.JOBTITLE='" + Position + "', "
                StrSql = StrSql + "USERCONTACTS.PHONENUMBER='" + PhoneNumber + "', "
                StrSql = StrSql + "USERCONTACTS.MOBILENUMBER='" + MobileNumber + "', "
                StrSql = StrSql + "USERCONTACTS.FAXNUMBER='" + FaxNumber + "', "
                ' StrSql = StrSql + "USERCONTACTS.COMPANYNAME='" + CompanyName + "', "
                StrSql = StrSql + "USERCONTACTS.STREETADDRESS1='" + StreetAddress1 + "', "
                StrSql = StrSql + "USERCONTACTS.STREETADDRESS2='" + StreetAddress2 + "', "
                StrSql = StrSql + "USERCONTACTS.CITY='" + City + "', "
                StrSql = StrSql + "USERCONTACTS.STATE='" + State + "', "
                StrSql = StrSql + "USERCONTACTS.ZIPCODE = '" + ZipCode + "', "
                StrSql = StrSql + "USERCONTACTS.COUNTRY = '" + Country + "' "
                StrSql = StrSql + "WHERE USERCONTACTS.USERID=" + UserId + " "
                odbUtil.UpIns(StrSql, EconConnection)

                'Update Company
                StrSql = String.Empty
                StrSql = "UPDATE USERS "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "USERS.COMPANYID='" + CompanyID + "' "
                StrSql = StrSql + "WHERE USERS.USERID=" + UserId + ""
                odbUtil.UpIns(StrSql, EconConnection)

                'Inserting Into UserContacts Table
                StrSql = String.Empty
                StrSql = "INSERT INTO USERCONTACTS  "
                StrSql = StrSql + "(USERCONTACTID, "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "PREFIX, "
                StrSql = StrSql + "FIRSTNAME, "
                StrSql = StrSql + "LASTNAME, "
                StrSql = StrSql + "JOBTITLE, "
                StrSql = StrSql + "EMAILADDRESS, "
                StrSql = StrSql + "PHONENUMBER,MOBILENUMBER, "
                StrSql = StrSql + "FAXNUMBER, "
                'StrSql = StrSql + "COMPANYNAME, "
                StrSql = StrSql + "STREETADDRESS1, "
                StrSql = StrSql + "STREETADDRESS2, "
                StrSql = StrSql + "CITY, "
                StrSql = StrSql + "STATE, "
                StrSql = StrSql + "ZIPCODE, "
                StrSql = StrSql + "COUNTRY) "
                StrSql = StrSql + "SELECT SEQUSERCONTACTID.NEXTVAL, "
                StrSql = StrSql + UserId + ", "
                StrSql = StrSql + "'" + Prefix + "', "
                StrSql = StrSql + "'" + FirstName + "', "
                StrSql = StrSql + "'" + LastName + "', "
                StrSql = StrSql + "'" + Position + "', "
                StrSql = StrSql + "'" + UserName + "', "
                StrSql = StrSql + "'" + PhoneNumber + "', "
                StrSql = StrSql + "'" + MobileNumber + "', "
                StrSql = StrSql + "'" + FaxNumber + "', "
                'StrSql = StrSql + "'" + CompanyName + "', "
                StrSql = StrSql + "'" + StreetAddress1 + "', "
                StrSql = StrSql + "'" + StreetAddress2 + "', "
                StrSql = StrSql + "'" + City + "', "
                StrSql = StrSql + "'" + State + "', "
                StrSql = StrSql + "'" + ZipCode + "', "
                StrSql = StrSql + "'" + Country + "' "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 FROM "
                StrSql = StrSql + "USERCONTACTS "
                StrSql = StrSql + "WHERE USERCONTACTS.USERID =" + UserId + ""
                StrSql = StrSql + ") "
                odbUtil.UpIns(StrSql, EconConnection)

                'Updating User Table
                StrSql = String.Empty
                StrSql = "UPDATE USERS SET ISPROMOMAIL = '" + PromoMail + "' WHERE  USERID=" + UserId + ""
                odbUtil.UpIns(StrSql, EconConnection)

                'Inserting into Company
                StrSql = "INSERT INTO COMPANY "
                StrSql = StrSql + "(COMPANYID,COMPANYNAME,PARENTID,ISNEW) "
                StrSql = StrSql + "SELECT '" + CompanyID + "','" + CompanyName + "',0,'Y' FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 FROM COMPANY "
                StrSql = StrSql + "WHERE COMPANYID='" + CompanyID + "' AND UPPER(COMPANYNAME)='" + CompanyName.ToUpper() + "'"
                StrSql = StrSql + ")"
                odbUtil.UpIns(StrSql, ShoppingConnection)

                'Inserting CompanyDomain Combination in CompanyDomain Table
                If CompanyStatus = True Or DomainStatus = True Then
                    StrSql = String.Empty
                    StrSql = "INSERT INTO COMPANYDOMAIN "
                    StrSql = StrSql + "(COMPANYDOMAINID,COMPANYID,DOMAINID) "
                    StrSql = StrSql + "SELECT SEQCOMPDOMID.NEXTVAL,'" + CompanyID + "','" + DomainID + "' FROM DUAL "
                    StrSql = StrSql + "WHERE NOT EXISTS "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + "SELECT 1 FROM COMPANYDOMAIN "
                    StrSql = StrSql + "WHERE COMPANYID='" + CompanyID + "' AND DOMAINID='" + DomainID + "' "
                    StrSql = StrSql + ")"
                    odbUtil.UpIns(StrSql, ShoppingConnection)
                End If

            Catch ex As Exception
                Throw New Exception("UsersUpdateData:UpdateUserDetails:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub UpdateEmailValidationFlag(ByVal UserId As String, ByVal UserName As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Try
                'Updating User detail
                StrSql = String.Empty
                StrSql = "Update Users Set Users.IsValidEmail= 'Y' Where Upper(UserName)= '" + UserName.ToUpper() + "' and  USERID=" + UserId + ""
                odbUtil.UpIns(StrSql, EconConnection)

            Catch ex As Exception
                Throw New Exception("UsersUpdateData:UpdateUserDetails:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub ChangePWD(ByVal UserID As String, ByVal pwd As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String

            Try

                StrSql = "UPDATE USERS "
                'Bug#389
                StrSql = StrSql + " SET PASSWORD='" + pwd.ToString() + "',VUPDATEDATE=SYSDATE  "
                'Bug#389
                StrSql = StrSql + " WHERE UserID = " + UserID
                odbUtil.UpIns(StrSql, EconConnection)

            Catch ex As Exception
                Throw New Exception("UsersUpdateData:ChangePWD:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub InsShoppingCartAddDetails(ByVal UserId As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim objGetData As New UsersGetData.Selectdata()
            Dim ds As New DataSet()

            Dim CompanyName As String = ""
            Dim AddHeader As String = ""
            Dim Prefix As String = ""
            Dim FirstName As String = ""
            Dim LastName As String = ""
            Dim Position As String = ""
            Dim PhoneNumber As String = ""
            Dim FaxNumber As String = ""
            Dim StreetAddress1 As String = ""
            Dim StreetAddress2 As String = ""
            Dim City As String = ""
            Dim State As String = ""
            Dim ZipCode As String = ""
            Dim Country As String = ""
            Dim UserName As String = ""

            Try
                'Getting Logged In Users Details by UserId
                ds = objGetData.GetUserAddById(UserId)
                If ds.Tables(0).Rows.Count > 0 Then
                    CompanyName = ds.Tables(0).Rows(0).Item("COMPANYNAME").ToString()
                    AddHeader = "Login User"
                    Prefix = ds.Tables(0).Rows(0).Item("PREFIX").ToString()
                    FirstName = ds.Tables(0).Rows(0).Item("FIRSTNAME").ToString()
                    LastName = ds.Tables(0).Rows(0).Item("LASTNAME").ToString()
                    Position = ds.Tables(0).Rows(0).Item("JOBTITLE").ToString()
                    PhoneNumber = ds.Tables(0).Rows(0).Item("PHONENUMBER").ToString()
                    FaxNumber = ds.Tables(0).Rows(0).Item("FAXNUMBER").ToString()
                    StreetAddress1 = ds.Tables(0).Rows(0).Item("STREETADDRESS1").ToString()
                    StreetAddress2 = ds.Tables(0).Rows(0).Item("STREETADDRESS2").ToString()
                    City = ds.Tables(0).Rows(0).Item("CITY").ToString()
                    State = ds.Tables(0).Rows(0).Item("STATE").ToString()
                    ZipCode = ds.Tables(0).Rows(0).Item("ZIPCODE").ToString()
                    Country = ds.Tables(0).Rows(0).Item("COUNTRY").ToString()
                    UserName = ds.Tables(0).Rows(0).Item("LOGINNAME").ToString()
                End If

                'Inserting Into USERADDRESS Table
                StrSql = "INSERT INTO USERADDRESS  "
                StrSql = StrSql + "(USERADDRESSID, "
                StrSql = StrSql + "COMPANYNAME, "
                StrSql = StrSql + "LOGINUSERID, "
                StrSql = StrSql + "USERNAME, "
                StrSql = StrSql + "ADDHEADER, "
                StrSql = StrSql + "PREFIX, "
                StrSql = StrSql + "FIRSTNAME, "
                StrSql = StrSql + "LASTNAME, "
                StrSql = StrSql + "JOBTITLE, "
                StrSql = StrSql + "EMAILADDRESS, "
                StrSql = StrSql + "PHONENUMBER, "
                StrSql = StrSql + "FAXNUMBER, "

                StrSql = StrSql + "STREETADDRESS1, "
                StrSql = StrSql + "STREETADDRESS2, "
                StrSql = StrSql + "CITY, "
                StrSql = StrSql + "STATE, "
                StrSql = StrSql + "ZIPCODE, "
                StrSql = StrSql + "COUNTRY) "
                StrSql = StrSql + "SELECT SEQUSERADDID.NEXTVAL, "
                StrSql = StrSql + "'" + CompanyName.Replace("'", "''").ToString() + "', "
                StrSql = StrSql + UserId + ","
                StrSql = StrSql + "'" + UserName + "',"
                StrSql = StrSql + "'" + AddHeader.Replace("'", "''").ToString() + "',"
                StrSql = StrSql + "'" + Prefix.Replace("'", "''").ToString() + "', "
                StrSql = StrSql + "'" + FirstName.Replace("'", "''").ToString() + "', "
                StrSql = StrSql + "'" + LastName.Replace("'", "''").ToString() + "', "
                StrSql = StrSql + "'" + Position.Replace("'", "''").ToString() + "', "
                StrSql = StrSql + "'" + UserName + "', "
                StrSql = StrSql + "'" + PhoneNumber.Replace("'", "''").ToString() + "', "
                StrSql = StrSql + "'" + FaxNumber.Replace("'", "''").ToString() + "', "
                StrSql = StrSql + "'" + StreetAddress1.Replace("'", "''").ToString() + "', "
                StrSql = StrSql + "'" + StreetAddress2.Replace("'", "''").ToString() + "', "
                StrSql = StrSql + "'" + City.Replace("'", "''").ToString() + "', "
                StrSql = StrSql + "'" + State.Replace("'", "''").ToString() + "', "
                StrSql = StrSql + "'" + ZipCode.Replace("'", "''").ToString() + "', "
                StrSql = StrSql + Country + " "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 FROM "
                StrSql = StrSql + "USERADDRESS "
                StrSql = StrSql + "WHERE LOGINUSERID=" + UserId + ") "
                odbUtil.UpIns(StrSql, EconConnection)


            Catch ex As Exception
                Throw New Exception("UsersUpdateData:InsertUserDetails:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub InsertUserAddressDetails(ByVal UserName As String, ByVal AddHeader As String, ByVal CompanyName As String, ByVal Prefix As String, ByVal FirstName As String, ByVal LastName As String, ByVal Position As String, ByVal PhoneNumber As String, ByVal FaxNumber As String, ByVal StreetAddress1 As String, ByVal StreetAddress2 As String, ByVal City As String, ByVal State As String, ByVal ZipCode As String, ByVal Country As String, ByVal LoginUserID As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Try

                'Inserting Into USERADDRESS Table
                StrSql = "INSERT INTO USERADDRESS  "
                StrSql = StrSql + "(USERADDRESSID, "
                StrSql = StrSql + "COMPANYNAME, "
                StrSql = StrSql + "LOGINUSERID, "
                StrSql = StrSql + "USERNAME, "
                StrSql = StrSql + "ADDHEADER, "
                StrSql = StrSql + "PREFIX, "
                StrSql = StrSql + "FIRSTNAME, "
                StrSql = StrSql + "LASTNAME, "
                StrSql = StrSql + "JOBTITLE, "
                StrSql = StrSql + "EMAILADDRESS, "
                StrSql = StrSql + "PHONENUMBER, "
                StrSql = StrSql + "FAXNUMBER, "

                StrSql = StrSql + "STREETADDRESS1, "
                StrSql = StrSql + "STREETADDRESS2, "
                StrSql = StrSql + "CITY, "
                StrSql = StrSql + "STATE, "
                StrSql = StrSql + "ZIPCODE, "
                StrSql = StrSql + "COUNTRY) "
                StrSql = StrSql + "SELECT SEQUSERADDID.NEXTVAL, "
                StrSql = StrSql + "'" + CompanyName + "', "
                StrSql = StrSql + LoginUserID + ","
                StrSql = StrSql + "'" + UserName + "',"
                StrSql = StrSql + "'" + AddHeader + "',"
                StrSql = StrSql + "'" + Prefix + "', "
                StrSql = StrSql + "'" + FirstName + "', "
                StrSql = StrSql + "'" + LastName + "', "
                StrSql = StrSql + "'" + Position + "', "
                StrSql = StrSql + "'" + UserName + "', "
                StrSql = StrSql + "'" + PhoneNumber + "', "
                StrSql = StrSql + "'" + FaxNumber + "', "
                StrSql = StrSql + "'" + StreetAddress1 + "', "
                StrSql = StrSql + "'" + StreetAddress2 + "', "
                StrSql = StrSql + "'" + City + "', "
                StrSql = StrSql + "'" + State + "', "
                StrSql = StrSql + "'" + ZipCode + "', "
                StrSql = StrSql + "'" + Country + "' "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 FROM "
                StrSql = StrSql + "USERADDRESS "
                StrSql = StrSql + "WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "' AND ADDHEADER='" + AddHeader + "' AND LOGINUSERID=" + LoginUserID + ") "
                odbUtil.UpIns(StrSql, EconConnection)

            Catch ex As Exception
                Throw New Exception("UsersUpdateData:InsertUserDetails:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub UpdateUserAddressDetails(ByVal UserIAddId As String, ByVal CompanyName As String, ByVal Prefix As String, ByVal FirstName As String, ByVal LastName As String, ByVal Position As String, ByVal PhoneNumber As String, ByVal FaxNumber As String, ByVal StreetAddress1 As String, ByVal StreetAddress2 As String, ByVal City As String, ByVal State As String, ByVal ZipCode As String, ByVal Country As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim objGetData As New UsersGetData.Selectdata()
            Dim DsUserData As New DataSet()
			Dim dsUserContactsData As New DataSet()
            Try
                'Update USERADDRESS
                StrSql = "UPDATE USERADDRESS  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "PREFIX='" + Prefix + "', "
                StrSql = StrSql + "FIRSTNAME='" + FirstName + "', "
                StrSql = StrSql + "LASTNAME='" + LastName + "', "
                StrSql = StrSql + "JOBTITLE='" + Position + "', "
                StrSql = StrSql + "PHONENUMBER='" + PhoneNumber + "', "
                StrSql = StrSql + "FAXNUMBER='" + FaxNumber + "', "
                StrSql = StrSql + "COMPANYNAME='" + CompanyName + "', "
                StrSql = StrSql + "STREETADDRESS1='" + StreetAddress1 + "', "
                StrSql = StrSql + "STREETADDRESS2='" + StreetAddress2 + "', "
                StrSql = StrSql + "CITY='" + City + "', "
                StrSql = StrSql + "STATE='" + State + "', "
                StrSql = StrSql + "ZIPCODE = '" + ZipCode + "', "
                StrSql = StrSql + "COUNTRY = '" + Country + "' "
                StrSql = StrSql + "WHERE USERADDRESSID=" + UserIAddId + " "
                odbUtil.UpIns(StrSql, EconConnection)

                'UPDATING LOGIN USER INFO IN USERS,USERCONTACTS TABLE
                DsUserData = objGetData.GetBillToShipToUserDetailsByAddID(UserIAddId)
                If DsUserData.Tables(0).Rows.Count > 0 Then
                    dsUserContactsData = objGetData.GetUserDetails(DsUserData.Tables(0).Rows(0).Item("LOGINUSERID").ToString())
                    If dsUserContactsData.Tables(0).Rows(0).Item("USERNAME").ToString() = DsUserData.Tables(0).Rows(0).Item("USERNAME").ToString() And DsUserData.Tables(0).Rows(0).Item("ADDHEADER").ToString() = "Login User" Then

                        'Update USERCONTACTS
                        StrSql = "UPDATE USERCONTACTS  "
                        StrSql = StrSql + "SET "
                        StrSql = StrSql + "USERCONTACTS.PREFIX='" + Prefix + "', "
                        StrSql = StrSql + "USERCONTACTS.FIRSTNAME='" + FirstName + "', "
                        StrSql = StrSql + "USERCONTACTS.LASTNAME='" + LastName + "', "
                        StrSql = StrSql + "USERCONTACTS.JOBTITLE='" + Position + "', "
                        StrSql = StrSql + "USERCONTACTS.PHONENUMBER='" + PhoneNumber + "', "
                        StrSql = StrSql + "USERCONTACTS.FAXNUMBER='" + FaxNumber + "', "
                        StrSql = StrSql + "USERCONTACTS.STREETADDRESS1='" + StreetAddress1 + "', "
                        StrSql = StrSql + "USERCONTACTS.STREETADDRESS2='" + StreetAddress2 + "', "
                        StrSql = StrSql + "USERCONTACTS.CITY='" + City + "', "
                        StrSql = StrSql + "USERCONTACTS.STATE='" + State + "', "
                        StrSql = StrSql + "USERCONTACTS.ZIPCODE = '" + ZipCode + "', "
                        StrSql = StrSql + "USERCONTACTS.COUNTRY = '" + Country + "' "
                        StrSql = StrSql + "WHERE USERCONTACTS.USERID=" + DsUserData.Tables(0).Rows(0).Item("LOGINUSERID").ToString() + " "
                        odbUtil.UpIns(StrSql, EconConnection)

                    End If
                End If
				
            Catch ex As Exception
                Throw New Exception("UsersUpdateData:UpdateUserAddressDetails:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub AddLicenseToUser(ByVal LicenseName As String, ByVal UserId As String)
            Dim ds As New DataSet()
            Dim odbUtil As New DBUtil()
            Try
                'Get LicenseId
                Dim StrSql As String = String.Empty
                StrSql = "SELECT SEQLICENSEID.NEXTVAL LICENSEID FROM DUAL"
                ds = odbUtil.FillDataSet(StrSql, EconConnection)
                LicenseName = LicenseName + ds.Tables(0).Rows(0).Item("LICENSEID").ToString()

                'Create License
                StrSql = String.Empty
                StrSql = "INSERT INTO LICENSEMASTER  "
                StrSql = StrSql + "(LICENSEID,LICENSENAME,MAXCOUNT,CONTRMAXCOUNT,REPMAXCOUNT) "
                StrSql = StrSql + "SELECT " + ds.Tables(0).Rows(0).Item("LICENSEID").ToString() + ",'" + LicenseName.ToUpper().ToString().Replace("'", "''") + "',10,10,10 "
                StrSql = StrSql + " FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "(SELECT 1 "
                StrSql = StrSql + "FROM LICENSEMASTER "
                StrSql = StrSql + "WHERE Upper(LICENSENAME)='" + LicenseName.ToString().ToUpper().Replace("'", "''") + "' "
                StrSql = StrSql + ") "
                odbUtil.UpIns(StrSql, EconConnection)

                'Assign License To User
                StrSql = String.Empty
                StrSql = "UPDATE USERS "
                StrSql = StrSql + " SET LICENSEID=" + ds.Tables(0).Rows(0).Item("LICENSEID").ToString() + ", "
                StrSql = StrSql + "LOCKDATE=SYSDATE,VUPDATEDATE=SYSDATE WHERE USERID=" + UserId + ""
                odbUtil.UpIns(StrSql, EconConnection)

                'Assign StructAssist to User
                StrSql = String.Empty
                StrSql = "INSERT INTO USERPERMISSIONS  "
                StrSql = StrSql + "(USERID,SERVICEID,USERROLE,MAXCASECOUNT) "
                StrSql = StrSql + "SELECT " + UserId + ",902,'ReadWrite',500 "
                StrSql = StrSql + " FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "(SELECT 1 "
                StrSql = StrSql + "FROM USERPERMISSIONS "
                StrSql = StrSql + "WHERE SERVICEID=902"
                StrSql = StrSql + "AND USERID=" + UserId + " "
                StrSql = StrSql + ") "
                odbUtil.UpIns(StrSql, EconConnection)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub EditLicenseToUser(ByVal UserId As String)
            Dim ds As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = ""
            Try

                'Assign StructAssist to User
                StrSql = String.Empty
                StrSql = "INSERT INTO USERPERMISSIONS  "
                StrSql = StrSql + "(USERID,SERVICEID,USERROLE,MAXCASECOUNT) "
                StrSql = StrSql + "SELECT " + UserId + ",902,'ReadWrite',500 "
                StrSql = StrSql + " FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "(SELECT 1 "
                StrSql = StrSql + "FROM USERPERMISSIONS "
                StrSql = StrSql + "WHERE SERVICEID=902"
                StrSql = StrSql + "AND USERID=" + UserId + " "
                StrSql = StrSql + ") "
                odbUtil.UpIns(StrSql, EconConnection)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub InsertSAUserDetails(ByVal EmailAddress As String, ByVal FirstName As String, ByVal LastName As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String

            Try
                Dim obj As New CryptoHelper
                StrSql = "INSERT INTO SAUSERS  "
                StrSql = StrSql + "(EMAILADDRESS,FIRSTNAME,LASTNAME,CREATIONDATE) "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "'" + EmailAddress.ToString().Replace("'", "''") + "', "
                StrSql = StrSql + "'" + FirstName.ToString().Replace("'", "''") + "', "
                StrSql = StrSql + "'" + LastName.ToString().Replace("'", "''") + "', "
                StrSql = StrSql + "SYSDATE "
                StrSql = StrSql + "FROM DUAL "
                odbUtil.UpIns(StrSql, EconConnection)

            Catch ex As Exception
                Throw New Exception("UsersUpdateData:InsertSAUserDetails:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub AddLicenseToNewUser(ByVal LicenseName As String, ByVal UserId As String)
            Dim ds As New DataSet()
            Dim odbUtil As New DBUtil()
            Try
                'Get LicenseId
                Dim StrSql As String = String.Empty
                StrSql = "SELECT SEQLICENSEID.NEXTVAL LICENSEID FROM DUAL"
                ds = odbUtil.FillDataSet(StrSql, EconConnection)
                LicenseName = LicenseName + ds.Tables(0).Rows(0).Item("LICENSEID").ToString()

                'Create License
                StrSql = String.Empty
                StrSql = "INSERT INTO LICENSEMASTER  "
                StrSql = StrSql + "(LICENSEID,LICENSENAME,MAXCOUNT,CONTRMAXCOUNT,REPMAXCOUNT) "
                StrSql = StrSql + "SELECT " + ds.Tables(0).Rows(0).Item("LICENSEID").ToString() + ",'" + LicenseName.ToUpper().ToString().Replace("'", "''") + "',10,10,10 "
                StrSql = StrSql + " FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "(SELECT 1 "
                StrSql = StrSql + "FROM LICENSEMASTER "
                StrSql = StrSql + "WHERE Upper(LICENSENAME)='" + LicenseName.ToString().ToUpper().Replace("'", "''") + "' "
                StrSql = StrSql + ") "
                odbUtil.UpIns(StrSql, EconConnection)

                'Assign License To User
                StrSql = String.Empty
                StrSql = "UPDATE USERS "
                StrSql = StrSql + " SET LICENSEID=" + ds.Tables(0).Rows(0).Item("LICENSEID").ToString() + ", "
                StrSql = StrSql + "LOCKDATE=SYSDATE,VUPDATEDATE=SYSDATE WHERE USERID=" + UserId + ""
                odbUtil.UpIns(StrSql, EconConnection)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        'Update AccontComplition Check Date
        Public Sub UpdateAccCheckDate(ByVal UserID As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "UPDATE USERS "
                StrSql = StrSql + "SET ACCCHECKDATE=SYSDATE "
                StrSql = StrSql + "WHERE USERID=" + UserID.ToString()

                odbUtil.UpIns(StrSql, EconConnection)
            Catch ex As Exception
                Throw New Exception("UsersUpdateData:UpdateAccCheckDate:" + ex.Message.ToString())
            End Try
        End Sub
        'End

#Region "SA Ordering"
        Public Sub InsertServicesDetails(ByVal RefNum As String, ByVal UserId As String, ByVal Count As Integer, ByVal ServiceType As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim ds As DataSet
            Dim seq As Integer = 0
            Try
                StrSql = "SELECT LICENSEID FROM USERS WHERE USERID=" + UserId
                ds = odbUtil.FillDataSet(StrSql, EconConnection)

                StrSql = String.Empty
                StrSql = "INSERT INTO LICENSESERVICES  "
                StrSql = StrSql + "(REFNUM,LICENSEID,ITEMNUMBER,TOTALCOUNT,ORDERDATE) "
                StrSql = StrSql + "SELECT " + RefNum + "," + ds.Tables(0).Rows(0).Item("LICENSEID").ToString() + ",'" + ServiceType + "'," + Count.ToString() + ",SYSDATE "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql & "WHERE NOT EXISTS "
                StrSql = StrSql & "( "
                StrSql = StrSql & "SELECT 1  "
                StrSql = StrSql & "FROM LICENSESERVICES "
                StrSql = StrSql & "WHERE LICENSESERVICES.LICENSEID=" + ds.Tables(0).Rows(0).Item("LICENSEID").ToString() + " "
                StrSql = StrSql & "AND LICENSESERVICES.ITEMNUMBER='" + ServiceType + "' "
                StrSql = StrSql & ") "
                odbUtil.UpIns(StrSql, EconConnection)

				'Updating User as License Admin
                StrSql = String.Empty
                StrSql = "UPDATE USERS "
                StrSql = StrSql + " SET ISIADMINLICUSR='Y' WHERE USERID=" + UserId + ""
                odbUtil.UpIns(StrSql, EconConnection)
				
				'Inserting record in TRANSSERV
                For i = 0 To Count - 1
                    If seq = 0 Then
                        StrSql = String.Empty
                        StrSql = "INSERT INTO TRANSSERV "
                        StrSql = StrSql + "(LICENSEID,COUNT,USERID1,SEQ,TYPE) "
                        StrSql = StrSql + "VALUES(" + ds.Tables(0).Rows(0).Item("LICENSEID").ToString() + ",'0'," + UserId + "," + (seq + 1).ToString() + ",'" + ServiceType + "')"
                        odbUtil.UpIns(StrSql, SBAConnection)
                    Else
                        StrSql = String.Empty
                        StrSql = "INSERT INTO TRANSSERV "
                        StrSql = StrSql + "(LICENSEID,COUNT,SEQ,TYPE) "
                        StrSql = StrSql + "VALUES(" + ds.Tables(0).Rows(0).Item("LICENSEID").ToString() + ",'0'," + (seq + 1).ToString() + ",'" + ServiceType + "')"
                        odbUtil.UpIns(StrSql, SBAConnection)
                    End If
                    seq = seq + 1
                Next

            Catch ex As Exception
                Throw New Exception("UsersUpdateData:InsertSAUserDetails:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub InsertUserAdmin(ByVal UserId As String, ByVal ServiceType As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim AdminConnection As String = System.Configuration.ConfigurationManager.AppSettings("AdminConnectionString")
            Dim type As String = ""
            Dim ds As New DataSet
            Try
                Dim obj As New CryptoHelper
                If ServiceType = "SA" Then
                    type = "STRUCTUSER"
                ElseIf ServiceType = "KBCOPK" Then
                    type = "CONTRACTUSER"
                End If
                StrSql = "SELECT SERVICEID FROM SERVICES WHERE SERVICECODE='" + type + "' "
                ds = odbUtil.FillDataSet(StrSql, AdminConnection)

                If ds.Tables(0).Rows.Count > 0 Then
                    StrSql = "INSERT INTO USERSERVICES  "
                    StrSql = StrSql + "(USERSERVICEID,SERVICEID,USERID) "
                    StrSql = StrSql + "SELECT USERSERVICEID.NEXTVAL," + ds.Tables(0).Rows(0).Item("SERVICEID").ToString() + "," + UserId + " "
                    StrSql = StrSql + "FROM DUAL "
                    StrSql = StrSql + "WHERE NOT EXISTS (SELECT 1 FROM USERSERVICES WHERE USERID=" + UserId + " AND SERVICEID=" + ds.Tables(0).Rows(0).Item("SERVICEID").ToString() + ")"
                    odbUtil.UpIns(StrSql, AdminConnection)
                End If


            Catch ex As Exception
                Throw New Exception("UsersUpdateData:InsertUserAdmin:" + ex.Message.ToString())
            End Try

        End Sub
#End Region

#Region "Contract Packaging"
        Public Sub AddLicenseUserServ(ByVal LicenseName As String, ByVal UserId As String, ByVal ServiceType As String)
            Dim ds, dsS As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim serviceD As String = ""
            Dim serviceID As String = ""
            Try
                'Get ServiceId
                If ServiceType = "SA" Then
                    serviceD = "STANDASSIST"
                ElseIf ServiceType = "KBCOPK" Then
                    serviceD = "CONTRACT PACKAGING KNOWLEDGEBASE"
                End If
                StrSql = "SELECT SERVICEID FROM SERVICES WHERE UPPER(SERVICEDE)='" + serviceD + "' "
                dsS = odbUtil.FillDataSet(StrSql, EconConnection)
                serviceID = dsS.Tables(0).Rows(0).Item("SERVICEID").ToString()

                'Get LicenseId
                StrSql = ""
                StrSql = "SELECT SEQLICENSEID.NEXTVAL LICENSEID FROM DUAL"
                ds = odbUtil.FillDataSet(StrSql, EconConnection)
                LicenseName = LicenseName + ds.Tables(0).Rows(0).Item("LICENSEID").ToString()

                'Create License
                StrSql = String.Empty
                StrSql = "INSERT INTO LICENSEMASTER  "
                StrSql = StrSql + "(LICENSEID,LICENSENAME,MAXCOUNT,CONTRMAXCOUNT,REPMAXCOUNT) "
                StrSql = StrSql + "SELECT " + ds.Tables(0).Rows(0).Item("LICENSEID").ToString() + ",'" + LicenseName.ToUpper().ToString().Replace("'", "''") + "',10,10,10 "
                StrSql = StrSql + " FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "(SELECT 1 "
                StrSql = StrSql + "FROM LICENSEMASTER "
                StrSql = StrSql + "WHERE Upper(LICENSENAME)='" + LicenseName.ToString().ToUpper().Replace("'", "''") + "' "
                StrSql = StrSql + ") "
                odbUtil.UpIns(StrSql, EconConnection)

                'Assign License To User
                StrSql = String.Empty
                StrSql = "UPDATE USERS "
                StrSql = StrSql + " SET LICENSEID=" + ds.Tables(0).Rows(0).Item("LICENSEID").ToString() + ", "
                StrSql = StrSql + "LOCKDATE=SYSDATE,VUPDATEDATE=SYSDATE WHERE USERID=" + UserId + ""
                odbUtil.UpIns(StrSql, EconConnection)

                'Assign StructAssist to User
                StrSql = String.Empty
                StrSql = "INSERT INTO USERPERMISSIONS  "
                StrSql = StrSql + "(USERID,SERVICEID,USERROLE,MAXCASECOUNT) "
                StrSql = StrSql + "SELECT " + UserId + "," + serviceID + ",'ReadWrite',500 "
                StrSql = StrSql + " FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "(SELECT 1 "
                StrSql = StrSql + "FROM USERPERMISSIONS "
                StrSql = StrSql + "WHERE SERVICEID=" + serviceID + " "
                StrSql = StrSql + "AND USERID=" + UserId + " "
                StrSql = StrSql + ") "
                odbUtil.UpIns(StrSql, EconConnection)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Public Sub InsertServDetails(ByVal RefNum As String, ByVal UserId As String, ByVal Count As Integer, ByVal ServiceType As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim ds As DataSet
            Dim seq As Integer = 0
            Try
                StrSql = "SELECT LICENSEID FROM USERS WHERE USERID=" + UserId
                ds = odbUtil.FillDataSet(StrSql, EconConnection)

                StrSql = String.Empty
                StrSql = "INSERT INTO LICENSESERVICES  "
                StrSql = StrSql + "(REFNUM,LICENSEID,ITEMNUMBER,TOTALCOUNT,ORDERDATE) "
                StrSql = StrSql + "SELECT " + RefNum + "," + ds.Tables(0).Rows(0).Item("LICENSEID").ToString() + ",'" + ServiceType + "'," + Count.ToString() + ",SYSDATE "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql & "WHERE NOT EXISTS "
                StrSql = StrSql & "( "
                StrSql = StrSql & "SELECT 1  "
                StrSql = StrSql & "FROM LICENSESERVICES "
                StrSql = StrSql & "WHERE LICENSESERVICES.LICENSEID=" + ds.Tables(0).Rows(0).Item("LICENSEID").ToString() + " "
                StrSql = StrSql & "AND LICENSESERVICES.ITEMNUMBER='" + ServiceType + "' "
                StrSql = StrSql & ") "
                odbUtil.UpIns(StrSql, EconConnection)

                'Inserting record in TRANSSERV
                For i = 0 To Count - 1
                    If seq = 0 Then
                        StrSql = String.Empty
                        StrSql = "INSERT INTO TRANSSERV "
                        StrSql = StrSql + "(LICENSEID,COUNT,USERID1,SEQ,TYPE) "
                        StrSql = StrSql + "VALUES(" + ds.Tables(0).Rows(0).Item("LICENSEID").ToString() + ",'0'," + UserId + "," + (seq + 1).ToString() + ",'" + ServiceType + "')"
                        odbUtil.UpIns(StrSql, ContractCon)
                    Else
                        StrSql = String.Empty
                        StrSql = "INSERT INTO TRANSSERV "
                        StrSql = StrSql + "(LICENSEID,COUNT,SEQ,TYPE) "
                        StrSql = StrSql + "VALUES(" + ds.Tables(0).Rows(0).Item("LICENSEID").ToString() + ",'0'," + (seq + 1).ToString() + ",'" + ServiceType + "')"
                        odbUtil.UpIns(StrSql, ContractCon)
                    End If
                    seq = seq + 1
                Next

            Catch ex As Exception
                Throw New Exception("UsersUpdateData:InsertServDetails:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub EditLicenseUserServ(ByVal UserId As String, ByVal serviceType As String)
            Dim ds As New DataSet()
            Dim dsSer As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = ""
            Dim serviceD As String = ""
            Try

                If serviceType = "SA" Then
                    serviceD = "STANDASSIST"
                ElseIf serviceType = "KBCOPK" Then
                    serviceD = "CONTRACT PACKAGING KNOWLEDGEBASE"
                End If

                StrSql = "SELECT SERVICEID FROM SERVICES WHERE UPPER(SERVICEDE)='" + serviceD + "' "
                dsSer = odbUtil.FillDataSet(StrSql, EconConnection)

                'Assign StructAssist to User
                StrSql = String.Empty
                StrSql = "INSERT INTO USERPERMISSIONS  "
                StrSql = StrSql + "(USERID,SERVICEID,USERROLE,MAXCASECOUNT) "
                StrSql = StrSql + "SELECT " + UserId + "," + dsSer.Tables(0).Rows(0).Item("SERVICEID").ToString() + ",'ReadWrite',500 "
                StrSql = StrSql + " FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "(SELECT 1 "
                StrSql = StrSql + "FROM USERPERMISSIONS "
                StrSql = StrSql + "WHERE SERVICEID=" + dsSer.Tables(0).Rows(0).Item("SERVICEID").ToString()
                StrSql = StrSql + " AND USERID=" + UserId + " "
                StrSql = StrSql + ") "
                odbUtil.UpIns(StrSql, EconConnection)

            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Public Sub AddEditUserCountries(ByVal UserId As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                'Deleting  User Countries
                StrSql = "DELETE FROM USERCOUNTRIES  "
                StrSql = StrSql + "WHERE USERID =" + UserId + " "
                odbUtil.UpIns(StrSql, ContractCon)

                'Add  User Countries
                StrSql = "INSERT INTO USERCOUNTRIES  "
                StrSql = StrSql + "(USERCOUNTRYID,COUNTRYID,USERID) "
                StrSql = StrSql + "SELECT USERCOUNTRYIDSEQ.NEXTVAL, "
                StrSql = StrSql + "DIMCOUNTRIES.COUNTRYID, "
                StrSql = StrSql + "" + UserId + "  "
                StrSql = StrSql + "FROM ADMINSITE.DIMCOUNTRIES "

                StrSql = StrSql + "INNER JOIN ADMINSITE.COUNTRYAVAIL "
                StrSql = StrSql + "ON DIMCOUNTRIES.COUNTRYID =COUNTRYAVAIL.COUNTRYID "
                StrSql = StrSql + "WHERE COUNTRYAVAIL.MODULEID=1 "

                'StrSql = StrSql + "WHERE AVAIL = 'Y' "
                StrSql = StrSql + "AND NOT EXISTS (    SELECT 1 "
                StrSql = StrSql + "FROM USERCOUNTRIES "
                StrSql = StrSql + "WHERE USERID =" + UserId + " "
                StrSql = StrSql + ") "
                odbUtil.UpIns(StrSql, ContractCon)

            Catch ex As Exception
                Throw New Exception("UsersUpdateData:AddEditUserCountries:" + ex.Message.ToString())
            End Try
        End Sub
#End Region
#Region "NEW USER CREATION"
        Public Sub UpdateAUserDetails(ByVal UserName As String, ByVal password As String, ByVal CompanyName As String, ByVal Prefix As String, ByVal FirstName As String, ByVal LastName As String, ByVal Position As String, ByVal PhoneNumber As String, ByVal FaxNumber As String, ByVal StreetAddress1 As String, ByVal StreetAddress2 As String, ByVal City As String, ByVal State As String, ByVal ZipCode As String, ByVal Country As String, ByVal VerfCode As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String

            Try
                'Bug#389
                'Inserting Into User Table
                StrSql = "UPDATE USERS  "
                StrSql = StrSql + "SET USERNAME='" + UserName + "', "
                StrSql = StrSql + "PASSWORD='" + password + "', "
                'StrSql = StrSql + "COMPANY='" + CompanyName + "', "
                StrSql = StrSql + "VERFCODE='" + VerfCode + "', "
                'StrSql = StrSql + "UTYPE=1, "
                StrSql = StrSql + "VCREATIONDATE=SYSDATE,VUPDATEDATE=SYSDATE,LOCKDATE=CURRENT_Timestamp-interval '17' minute "
                StrSql = StrSql + "WHERE Upper(USERS.USERNAME) = '" + UserName.ToUpper() + "' "
                'Bug#389
                odbUtil.UpIns(StrSql, EconConnection)

                'Inserting Into UserContacts Table
                StrSql = "UPDATE USERCONTACTS  "
                StrSql = StrSql + "SET PREFIX='" + Prefix + "', "
                StrSql = StrSql + "FIRSTNAME='" + FirstName + "', "
                StrSql = StrSql + "LASTNAME='" + LastName + "', "
                StrSql = StrSql + "JOBTITLE='" + Position + "', "
                StrSql = StrSql + "EMAILADDRESS='" + UserName + "', "
                StrSql = StrSql + "PHONENUMBER='" + PhoneNumber + "', "
                StrSql = StrSql + "FAXNUMBER='" + FaxNumber + "', "
                'StrSql = StrSql + "COMPANYNAME='" + CompanyName + "', "
                StrSql = StrSql + "STREETADDRESS1='" + StreetAddress1 + "', "
                StrSql = StrSql + "STREETADDRESS2='" + StreetAddress2 + "', "
                StrSql = StrSql + "CITY='" + City + "', "
                StrSql = StrSql + "STATE='" + State + "', "
                StrSql = StrSql + "ZIPCODE='" + ZipCode + "', "
                StrSql = StrSql + "COUNTRY='" + Country + "' "
                StrSql = StrSql + "WHERE USERCONTACTS.USERID = (SELECT USERS.USERID FROM USERS WHERE Upper(USERS.USERNAME)='" + UserName.ToUpper() + "') "

                odbUtil.UpIns(StrSql, EconConnection)

                'Bug#389
                StrSql = "INSERT INTO  PASSWORDLOG(USERID,PASSWORD,SEQUENCE) "
                StrSql = StrSql + " VALUES((select userid from users where upper(USERNAME)='" + UserName.ToUpper() + "' AND PASSWORD='" + password + "'),'" + password + "',1)"
                odbUtil.UpIns(StrSql, EconConnection)
                'Bug#389

                'Bug#379
                StrSql = "INSERT INTO INKPREFERENCES  "
                StrSql = StrSql + "(USERNAME) "
                StrSql = StrSql + "VALUES('" + UserName.Trim() + "')"
                odbUtil.UpIns(StrSql, EconConnection)
                'Bug#379

            Catch ex As Exception
                Throw New Exception("UsersUpdateData:UpdateUserDetails:" + ex.Message.ToString())
            End Try

        End Sub
#End Region
#Region "New User Creation August3-2017"
        Public Sub UpdateINCOMPUserDetails(ByVal UserName As String, ByVal password As String, ByVal CompanyID As String, ByVal Prefix As String, _
                                           ByVal FirstName As String, ByVal LastName As String, ByVal Position As String, ByVal PhoneNumber As String, _
                                           ByVal FaxNumber As String, ByVal StreetAddress1 As String, ByVal StreetAddress2 As String, ByVal City As String, _
                                           ByVal State As String, ByVal ZipCode As String, ByVal Country As String, ByVal VerfCode As String, _
                                           ByVal CompanyName As String, ByVal MobileNum As String, ByVal DomainStatusId As String, ByVal privUSR As Boolean, _
                                           ByVal DomainID As String, ByVal CompanyStatus As Boolean,ByVal Domstatus As Boolean)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim IsApproved As String = String.Empty
            Try
                'Inserting Into User Table
                StrSql = "UPDATE USERS  "
                StrSql = StrSql + "SET USERNAME='" + UserName + "', "
                StrSql = StrSql + "PASSWORD='" + password + "', "
                StrSql = StrSql + "COMPANYID='" + CompanyID + "', "
                'StrSql = StrSql + "VERFCODE='" + VerfCode + "', "
                StrSql = StrSql + "STATUSID=1, "
                If privUSR = True Then
                Else
                    If DomainStatusId = 1 Or DomainStatusId = 4 Then
                        StrSql = StrSql + "ISAPPROVED='Y', "
                    Else
                        StrSql = StrSql + "ISAPPROVED='', "
                    End If


                   'Bug#PK20_1
                    StrSql = StrSql + "VERFCODE='" + VerfCode + "',"
                    StrSql = StrSql + "VERIFYDATE=SYSDATE,"
                    'EndBug#PK20_1

                    'If DomainStatusId = 1 Then
                    '    StrSql = StrSql + "ISVALIDEMAIL='Y',"
                    '    StrSql = StrSql + "VERFCODE='#', "                   
                    'Else
                    '    StrSql = StrSql + "VERFCODE='" + VerfCode + "',"
                    '    StrSql = StrSql + "VERIFYDATE=SYSDATE,"
                    'End If

                End If

                StrSql = StrSql + "VUPDATEDATE=SYSDATE,LOCKDATE=CURRENT_Timestamp-interval '17' minute "
                StrSql = StrSql + "WHERE Upper(USERS.USERNAME) = '" + UserName.ToUpper() + "' "
                'Bug#389
                odbUtil.UpIns(StrSql, EconConnection)

                'Inserting Into UserContacts Table
                StrSql = "UPDATE USERCONTACTS  "
                StrSql = StrSql + "SET PREFIX='" + Prefix + "', "
                StrSql = StrSql + "FIRSTNAME='" + FirstName + "', "
                StrSql = StrSql + "LASTNAME='" + LastName + "', "
                StrSql = StrSql + "JOBTITLE='" + Position + "', "
                StrSql = StrSql + "EMAILADDRESS='" + UserName + "', "
                StrSql = StrSql + "PHONENUMBER='" + PhoneNumber + "', "
                StrSql = StrSql + "FAXNUMBER='" + FaxNumber + "', "
                StrSql = StrSql + "MOBILENUMBER='" + MobileNum + "', "
                'StrSql = StrSql + "COMPANYNAME='" + CompanyName + "', "
                StrSql = StrSql + "STREETADDRESS1='" + StreetAddress1 + "', "
                StrSql = StrSql + "STREETADDRESS2='" + StreetAddress2 + "', "
                StrSql = StrSql + "CITY='" + City + "', "
                StrSql = StrSql + "STATE='" + State + "', "
                StrSql = StrSql + "ZIPCODE='" + ZipCode + "', "
                StrSql = StrSql + "COUNTRY='" + Country + "' "
                StrSql = StrSql + "WHERE USERCONTACTS.USERID = (SELECT USERS.USERID FROM USERS WHERE Upper(USERS.USERNAME)='" + UserName.ToUpper() + "') "

                odbUtil.UpIns(StrSql, EconConnection)

                'Bug#389
                StrSql = "INSERT INTO  PASSWORDLOG(USERID,PASSWORD,SEQUENCE) "
                StrSql = StrSql + " VALUES((select userid from users where upper(USERNAME)='" + UserName.ToUpper() + "' AND PASSWORD='" + password + "'),'" + password + "',1)"
                odbUtil.UpIns(StrSql, EconConnection)
                'Bug#389

                'Bug#379
                StrSql = "INSERT INTO INKPREFERENCES  "
                StrSql = StrSql + "(USERNAME) "
                StrSql = StrSql + "VALUES('" + UserName.Trim() + "')"
                odbUtil.UpIns(StrSql, EconConnection)
                'Bug#379

                'Inserting into Company
                StrSql = "INSERT INTO COMPANY "
                StrSql = StrSql + "(COMPANYID,COMPANYNAME,PARENTID,ISNEW) "
                StrSql = StrSql + "SELECT '" + CompanyID + "','" + CompanyName + "',0,'Y' FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 FROM COMPANY "
                StrSql = StrSql + "WHERE UPPER(COMPANYNAME)='" + CompanyName.ToUpper() + "'"
                StrSql = StrSql + ")"
                odbUtil.UpIns(StrSql, ShoppingConnection)

                'Inserting CompanyDomain Combination in CompanyDomain Table
                If CompanyStatus = True Or Domstatus = True Then
                    StrSql = String.Empty
                    StrSql = "INSERT INTO COMPANYDOMAIN "
                    StrSql = StrSql + "(COMPANYDOMAINID,COMPANYID,DOMAINID) "
                    StrSql = StrSql + "SELECT SEQCOMPDOMID.NEXTVAL,'" + CompanyID + "','" + DomainID + "' FROM DUAL "
                    StrSql = StrSql + "WHERE NOT EXISTS "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + "SELECT 1 FROM COMPANYDOMAIN "
                    StrSql = StrSql + "WHERE COMPANYID='" + CompanyID + "' AND DOMAINID='" + DomainID + "' "
                    StrSql = StrSql + ")"
                    odbUtil.UpIns(StrSql, ShoppingConnection)
                End If

            Catch ex As Exception
                Throw New Exception("UsersUpdateData:UpdateUserDetails:" + ex.Message.ToString())
            End Try

        End Sub

        Public Function UpdateURdomain(ByVal domainNM As String) As String
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim Dts As New DataSet()
            Dim DId As String = String.Empty
            Try
                'Get Next Domain value
                StrSql = "SELECT SEQDOMAINID.NEXTVAL AS ID  "
                StrSql = StrSql + "FROM DUAL "
                Dts = odbUtil.FillDataSet(StrSql, ShoppingConnection)
                DId = Dts.Tables(0).Rows(0).Item("ID")

                StrSql = String.Empty
                StrSql = "INSERT INTO DOMAIN "
                StrSql = StrSql + "(DOMAINID,DOMAINNAME,ACCEPTANCYFLAG,ISNEW) "
                StrSql = StrSql + "SELECT '" + DId + "','" + domainNM.Replace("'", "''").ToString() + "',4 ,'Y'FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 FROM DOMAIN WHERE UPPER(DOMAINNAME)='" + domainNM.Replace("'", "''").ToUpper() + "' "
                StrSql = StrSql + ")"
                odbUtil.UpIns(StrSql, ShoppingConnection)

                Return DId
            Catch ex As Exception
                Throw New Exception("UsersUpdateData:UpdateURdomain:" + ex.Message.ToString())
                Return DId
            End Try

        End Function

        'Public Sub UpdateURdomain(ByVal domainNM As String)
        '    Dim odbUtil As New DBUtil()
        '    Dim StrSql As String = String.Empty
        '    Try
        '        StrSql = "INSERT INTO DOMAIN "
        '        StrSql = StrSql + "(DOMAINID,DOMAINNAME,ACCEPTANCYFLAG) "
        '        StrSql = StrSql + "SELECT SEQDOMAINID.NEXTVAL,'" + domainNM.Replace("'", "''").ToString() + "',4 FROM DUAL "
        '        StrSql = StrSql + "WHERE NOT EXISTS "
        '        StrSql = StrSql + "( "
        '        StrSql = StrSql + "SELECT 1 FROM DOMAIN WHERE UPPER(DOMAINNAME)='" + domainNM.Replace("'", "''").ToUpper() + "' "
        '        StrSql = StrSql + ")"
        '        odbUtil.UpIns(StrSql, ShoppingConnection)

        '    Catch ex As Exception
        '        Throw New Exception("UsersUpdateData:UpdateURdomain:" + ex.Message.ToString())
        '    End Try

        'End Sub

        Public Sub InsertVerificationDate(ByVal UserId As String, ByVal VerfCode As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "UPDATE USERS SET "
                StrSql = StrSql + "VERFCODE='" + VerfCode + "', "
                StrSql = StrSql + "VERIFYDATE=SYSDATE "
                StrSql = StrSql + "WHERE USERID='" + UserId + "'"
                odbUtil.UpIns(StrSql, EconConnection)
            Catch ex As Exception
                Throw New Exception("UsersUpdateData:InsertVerificationDate:" + ex.Message.ToString())
            End Try
        End Sub
#End Region

#Region "Savvypack Website"
        Public Sub InsertASUserDetails(ByVal EmailAddress As String, ByVal FirstName As String, ByVal LastName As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String

            Try
                Dim obj As New CryptoHelper
                StrSql = "INSERT INTO SAUSERS  "
                StrSql = StrSql + "(EMAILADDRESS,FIRSTNAME,LASTNAME,CREATIONDATE,MODULE) "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "'" + EmailAddress.ToString().Replace("'", "''") + "', "
                StrSql = StrSql + "'" + FirstName.ToString().Replace("'", "''") + "', "
                StrSql = StrSql + "'" + LastName.ToString().Replace("'", "''") + "', "
                StrSql = StrSql + "SYSDATE,'SavvyPackAnalyticalService' "
                StrSql = StrSql + "FROM DUAL "
                odbUtil.UpIns(StrSql, EconConnection)

            Catch ex As Exception
                Throw New Exception("UsersUpdateData:InsertSAUserDetails:" + ex.Message.ToString())
            End Try

        End Sub
#End Region

#Region "Retrieve Password"

        Public Sub InsertPwdVerfCode(ByVal UserID As String, ByVal VrfyCode As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "INSERT INTO PWDVERIFICATION  "
                StrSql = StrSql + "(USERID,PWDVERIFYCODE,PWDVERIFYDATE) "
                StrSql = StrSql + "SELECT " + UserID.ToString() + ",'" + VrfyCode.ToString() + "',SYSDATE "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "(SELECT 1 "
                StrSql = StrSql + "FROM PWDVERIFICATION "
                StrSql = StrSql + "WHERE USERID=" + UserID.ToString()
                StrSql = StrSql + ") "

                odbUtil.UpIns(StrSql, EconConnection)
            Catch ex As Exception
                Throw New Exception("UsersUpdateData:UpdateAccCheckDate:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub UpdatePwdVerfCode(ByVal UserID As String, ByVal VrfyCode As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "UPDATE PWDVERIFICATION SET "
                StrSql = StrSql + "PWDVERIFYCODE='" + VrfyCode.ToString() + "',PWDVERIFYDATE=SYSDATE "
                StrSql = StrSql + "WHERE USERID=" + UserID.ToString()

                odbUtil.UpIns(StrSql, EconConnection)
            Catch ex As Exception
                Throw New Exception("UsersUpdateData:UpdateAccCheckDate:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub UpdatePWDLogWithOSec(ByVal UserID As String, ByVal pwd As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Try
                StrSql = "UPDATE PASSWORDLOG SET PASSWORD='" + pwd + "'WHERE USERID=" + UserID.ToString() + ""
                odbUtil.UpIns(StrSql, EconConnection)
            Catch ex As Exception
                Throw New Exception("UsersUpdateData:UpdatePWDLogWithOSec" + ex.Message.ToString())
            End Try
        End Sub

#End Region

#Region "Account Complition Verification"

        Public Sub UpdateVerfCode(ByVal UserID As String, ByVal VrfyCode As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "UPDATE USERS SET "
                StrSql = StrSql + "VERFCODE='" + VrfyCode + "', "
                StrSql = StrSql + "VERIFYDATE=SYSDATE "
                StrSql = StrSql + "WHERE USERID='" + UserID + "'"
                odbUtil.UpIns(StrSql, EconConnection)
            Catch ex As Exception
                Throw New Exception("UsersUpdateData:UpdateAccCheckDate:" + ex.Message.ToString())
            End Try
        End Sub

#End Region
    End Class
End Class
