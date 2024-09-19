Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports LoginUpdateData

Public Class LoginGetData
    Public Class Selectdata
        Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
        Dim Econ2Connection As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
        Dim Sustain1Connection As String = System.Configuration.ConfigurationManager.AppSettings("Sustain1ConnectionString")
        Dim Sustain2Connection As String = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")
        Dim ContractConnection As String = System.Configuration.ConfigurationManager.AppSettings("ContractConnectionString")
        Dim PackProdConnection As String = System.Configuration.ConfigurationManager.AppSettings("PackProdConnectionString")
        Dim DowConnection As String = System.Configuration.ConfigurationManager.AppSettings("DowChemicalConnectionString")
        Dim ReportConnection As String = System.Configuration.ConfigurationManager.AppSettings("ReportConnectionString")
        Dim MarketConnection As String = System.Configuration.ConfigurationManager.AppSettings("Market1ConnectionString")
#Region "Bug#Security"
        Public Function GetSecuirtyDetails(ByVal SecLVL As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select SECID,  "
                StrSql = StrSql + "SECLEVEL, "
                StrSql = StrSql + "DESCRIPTION "
                StrSql = StrSql + "FROM SECURITYLEVEL "
                StrSql = StrSql + "WHERE SECID= " + SecLVL + " "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:ValidateUser:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetPasswordLog(ByVal UserID As String, ByVal pwd As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERID,  "
                StrSql = StrSql + "PASSWORD, "
                StrSql = StrSql + "SEQUENCE "
                StrSql = StrSql + " FROM PASSWORDLOG "
                StrSql = StrSql + "WHERE Upper(USERID)=" + UserID.ToString() + " AND PASSWORD='" + pwd + "'"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:GetPasswordLog:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function ValidateUserByUserName(ByVal UserName As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select USERID,  "
                StrSql = StrSql + "USERNAME, "
                StrSql = StrSql + "PASSWORD, "
                StrSql = StrSql + "LICENSEID, "
                'StrSql = StrSql + "COMPANY, "
                StrSql = StrSql + "(CASE WHEN COMPANY.COMPANYNAME='' THEN '' ELSE COMPANY.COMPANYNAME END)COMPANY, "
                StrSql = StrSql + "ISVALIDEMAIL,"
                StrSql = StrSql + "VERFCODE,"
                StrSql = StrSql + "TO_CHAR(VCREATIONDATE,'MON DD, YYYY')CDATE,VUPDATEDATE,STATUSID,ISAPPROVED "
                StrSql = StrSql + "FROM USERS "
                StrSql = StrSql + "LEFT OUTER JOIN SHOPPING.COMPANY "
                StrSql = StrSql + "ON COMPANY.COMPANYID=USERS.COMPANYID "
                StrSql = StrSql + "WHERE Upper(USERNAME)='" + UserName.ToUpper() + "'"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("UsersGetData:ValidateUserByUserName:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetSecurityDetails(ByVal SecLVL As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select SECID,  "
                StrSql = StrSql + "SECLEVEL, "
                StrSql = StrSql + "DESCRIPTION "
                StrSql = StrSql + "FROM SECURITYLEVEL "
                StrSql = StrSql + "WHERE SECID= " + SecLVL

                Dts = odbUtil.FillDataSet(StrSql, EconConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetSecurityDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function ValidateUSRByUser(ByVal UserName As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select USERS.USERID,  "
                StrSql = StrSql + "USERS.USERNAME, "
                StrSql = StrSql + "USERS.PASSWORD, "
                StrSql = StrSql + "USERS.LICENSEID, "
                'StrSql = StrSql + "USERS.COMPANY, "
                StrSql = StrSql + "(CASE WHEN COMPANY.COMPANYNAME='' THEN '' ELSE COMPANY.COMPANYNAME END)COMPANY, "
                StrSql = StrSql + "USERS.ISLOCK, "
                StrSql = StrSql + "USERS.LOCKDATE, "
                StrSql = StrSql + "USERS.STATUSID, "
                StrSql = StrSql + "LICENSEMASTER.SECURITYLVL, "
                StrSql = StrSql + "LICENSEMASTER.MAXCOUNT "
                StrSql = StrSql + "FROM USERS "
                StrSql = StrSql + "INNER JOIN LICENSEMASTER ON USERS.LICENSEID= LICENSEMASTER.LICENSEID "
                StrSql = StrSql + "LEFT OUTER JOIN SHOPPING.COMPANY "
                StrSql = StrSql + "ON COMPANY.COMPANYID=USERS.COMPANYID "
                ' StrSql = StrSql + "lEFT OUTER JOIN LICENSEMASTER ON USERS.LICENSEID= LICENSEMASTER.LICENSEID "
                StrSql = StrSql + "WHERE Upper(username)='" + UserName.ToUpper() + "'"

                Dts = odbUtil.FillDataSet(StrSql, EconConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:ValidateUser:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function ValidateUSRWOLICENSE(ByVal UserName As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select USERS.USERID,  "
                StrSql = StrSql + "USERS.USERNAME, "
                StrSql = StrSql + "USERS.PASSWORD, "
                StrSql = StrSql + "USERS.LICENSEID, "
                'StrSql = StrSql + "USERS.COMPANY, "
                StrSql = StrSql + "(CASE WHEN COMPANY.COMPANYNAME='' THEN '' ELSE COMPANY.COMPANYNAME END)COMPANY, "
                StrSql = StrSql + "USERS.ISLOCK, "
                StrSql = StrSql + "USERS.LOCKDATE, "
                'StrSql = StrSql + "LICENSEMASTER.SECURITYLVL, "
                StrSql = StrSql + "NVL(LICENSEMASTER.SECURITYLVL,2) SECURITYLVL, "
                StrSql = StrSql + "LICENSEMASTER.MAXCOUNT "
                StrSql = StrSql + "FROM USERS "
                StrSql = StrSql + "LEFT OUTER JOIN LICENSEMASTER ON USERS.LICENSEID= LICENSEMASTER.LICENSEID "
                StrSql = StrSql + "LEFT OUTER JOIN SHOPPING.COMPANY "
                StrSql = StrSql + "ON COMPANY.COMPANYID=USERS.COMPANYID "
                ' StrSql = StrSql + "lEFT OUTER JOIN LICENSEMASTER ON USERS.LICENSEID= LICENSEMASTER.LICENSEID "
                StrSql = StrSql + "WHERE Upper(username)='" + UserName.ToUpper() + "'"

                Dts = odbUtil.FillDataSet(StrSql, EconConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:ValidateUser:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region
#Region "Login Details"
        Public Function ValidateID(ByVal UserName As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim obj As New CryptoHelper
            Try
                StrSql = "select USERS.USERID,  "
                StrSql = StrSql + "USERS.USERNAME, "
                StrSql = StrSql + "USERS.PASSWORD, "
                StrSql = StrSql + "USERS.LICENSEID, "
                ' StrSql = StrSql + "USERS.COMPANY, "
                StrSql = StrSql + "(CASE WHEN COMPANY.COMPANYNAME='' THEN '' ELSE COMPANY.COMPANYNAME END)COMPANY, "
                StrSql = StrSql + "USERS.ISINTERNALUSR,"
                StrSql = StrSql + "LICENSEMASTER.MAXCOUNT, "
                StrSql = StrSql + "LICENSEMASTER.SECURITYLVL, "
                StrSql = StrSql + "LICENSEMASTER.MAXCOUNT "
                StrSql = StrSql + "FROM USERS "
                StrSql = StrSql + "INNER JOIN LICENSEMASTER ON USERS.LICENSEID= LICENSEMASTER.LICENSEID "
                StrSql = StrSql + "LEFT OUTER JOIN SHOPPING.COMPANY "
                StrSql = StrSql + "ON COMPANY.COMPANYID=USERS.COMPANYID "
                StrSql = StrSql + "WHERE Upper(username)='" + UserName.ToUpper() + "'"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:ValidateID:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function ValidateUser(ByVal UserName As String, ByVal Password As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select USERS.USERID,  "
                StrSql = StrSql + "USERS.USERNAME, "
                StrSql = StrSql + "USERS.PASSWORD, "
                StrSql = StrSql + "USERS.LICENSEID,USERS.ISINTERNALUSR, "
                'StrSql = StrSql + "USERS.COMPANY, "
                StrSql = StrSql + "(CASE WHEN COMPANY.COMPANYNAME='' THEN '' ELSE COMPANY.COMPANYNAME END)COMPANY, "
                StrSql = StrSql + "LICENSEMASTER.MAXCOUNT, "
                'Bug#389
                StrSql = StrSql + "LICENSEMASTER.SECURITYLVL, "
                StrSql = StrSql + "LICENSEMASTER.MAXCOUNT "
                'Bug#389
                StrSql = StrSql + "FROM USERS "
                StrSql = StrSql + "INNER JOIN LICENSEMASTER ON USERS.LICENSEID= LICENSEMASTER.LICENSEID "
                StrSql = StrSql + "LEFT OUTER JOIN SHOPPING.COMPANY "
                StrSql = StrSql + "ON COMPANY.COMPANYID=USERS.COMPANYID "
                StrSql = StrSql + "WHERE Upper(username)='" + UserName.ToUpper() + "'"
                StrSql = StrSql + " AND password='" + Password + "'"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:ValidateUser:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function ValidateUserPermissions(ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select Distinct Services.ServiceType  "
                StrSql = StrSql + "From Services , UserPermissions "
                StrSql = StrSql + "where UserPermissions.ServiceID =Services.ServiceId "
                StrSql = StrSql + "and  UserPermissions.UserID = " + UserId
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:ValidateUser:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function ValidateCorpUser(ByVal CUserName As String, ByVal CPassword As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CORPORATEUSERID,  "
                StrSql = StrSql + "LTRIM(RTRIM(USERNAME))USERNAME, "
                StrSql = StrSql + "LTRIM(RTRIM(PASSWORD))PASSWORD, "
                StrSql = StrSql + "LTRIM(RTRIM(COMPANY))COMPANY, "
                StrSql = StrSql + "LICENSENO "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "CORPORATEUSERS "
                StrSql = StrSql + "WHERE Upper(username)='" + CUserName.ToUpper() + "'"
                StrSql = StrSql + " AND Upper(password)='" + CPassword.ToUpper() + "'"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:ValidateCorpUser:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetModuleDetails(ByVal UserID As String, ByVal Tid As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select Services.ServiceType,  "
                StrSql = StrSql + "Services.ServiceId, "
                StrSql = StrSql + "Services.ServiceDe, "
                StrSql = StrSql + "Services.ServiceName, "
                StrSql = StrSql + "Services.Path, "
                StrSql = StrSql + "Services.Tid "
                StrSql = StrSql + "From Services , UserPermissions "
                StrSql = StrSql + "where UserPermissions.ServiceID =Services.ServiceId "
                StrSql = StrSql + "and   UserPermissions.UserID =" + UserID
                StrSql = StrSql + " and  Services.Tid LIKE '" + Tid + "%' and Services.Servicename <>'Market1 - Market Information' "
                StrSql = StrSql + " and Services.ServiceType = 1 order by ServiceName "


                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetModuleDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetKnowledgeBaseDetails(ByVal UserID As String, ByVal Tid As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select Services.ServiceType,  "
                StrSql = StrSql + "Services.ServiceId, "
                StrSql = StrSql + "Services.ServiceDe, "
                StrSql = StrSql + "Services.ServiceName, "
                StrSql = StrSql + "Services.Path, "
                StrSql = StrSql + "Services.Tid "
                StrSql = StrSql + "From Services , UserPermissions "
                StrSql = StrSql + "where UserPermissions.ServiceID =Services.ServiceId "
                StrSql = StrSql + "and   UserPermissions.UserID =" + UserID
                StrSql = StrSql + " and  Services.Tid LIKE '" + Tid + "%' "
                StrSql = StrSql + " and Services.ServiceType = 2 order by ServiceName "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetKnowledgeBaseDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetReportDetails(ByVal UserID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select Services.ServiceType,  "
                StrSql = StrSql + "Services.ServiceId, "
                StrSql = StrSql + "Services.ServiceDe, "
                StrSql = StrSql + "Services.ServiceName, "
                StrSql = StrSql + "Services.Path, "
                StrSql = StrSql + "Services.Tid "
                StrSql = StrSql + "From Services , UserPermissions "
                StrSql = StrSql + "where UserPermissions.ServiceID =Services.ServiceId "
                StrSql = StrSql + "and   UserPermissions.UserID =" + UserID + " and Services.ServiceType = 3 order by ServiceName "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetReportDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetInuseDetails(ByVal UserID As String, ByVal schema As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ConnectionString As String = System.Configuration.ConfigurationManager.AppSettings(schema)
            Try
                StrSql = "select count(1) as count from inuse  "
                StrSql = StrSql + "inner join Econ.USERS "
                StrSql = StrSql + "on users.USERID=" + UserID
                'StrSql = StrSql + " and Upper(Users.LICENSENO)=Upper(inuse.LICENSEID) " 
                StrSql = StrSql + " and Users.LICENSEID=inuse.LICENSEID "
                Dts = odbUtil.FillDataSet(StrSql, ConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetInuseDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetInuseDetailsByLic(ByVal UserID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("SystemConnectionString")
            Try
                StrSql = "SELECT SUM(COUNT) COUNT FROM  "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM ECON.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM ECON2.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM ECON3.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM ECON4.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM SUSTAIN.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM SUSTAIN2.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM SUSTAIN3.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM SUSTAIN4.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM SCHEM1.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM ECHEM1.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                'StrSql = StrSql + "UNION ALL "
                'StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                'StrSql = StrSql + "FROM MARKET1.INUSE "
                'StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM SPEC.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM DISTRIBUTION.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM SDISTRIBUTION.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM RETAIL.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM VALUECHAIN.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM ECON.INUSECOMP "
                StrSql = StrSql + "WHERE INUSECOMP.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM SUSTAIN.INUSECOMP "
                StrSql = StrSql + "WHERE INUSECOMP.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "

                'Bug SA Inuse missing Start
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM SBASSIST.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                'Bug SA Inuse missing end

                StrSql = StrSql + ") "

                Dts = odbUtil.FillDataSet(StrSql, ConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetInuseDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function getLicenseDetail(ByVal LicenseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT LICENSEID,  "
                StrSql = StrSql + "LICENSENAME, "
                StrSql = StrSql + "MAXCOUNT, "
                StrSql = StrSql + "CONTRMAXCOUNT, "
                StrSql = StrSql + "REPMAXCOUNT, "
                StrSql = StrSql + "SUBSCRPBDATE, "
                StrSql = StrSql + "SUBSCRPEDATE "
                StrSql = StrSql + "FROM LICENSEMASTER "
                StrSql = StrSql + "WHERE LICENSEID= " + LicenseId
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:getLicenseDetail:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetModuleDetailsByLicense(ByVal LicenseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT SERVICES.SERVICETYPE,  "
                StrSql = StrSql + "SERVICES.SERVICEID, "
                StrSql = StrSql + "SERVICES.SERVICEDE "
                StrSql = StrSql + "FROM SERVICES "
                StrSql = StrSql + "INNER JOIN USERPERMISSIONS "
                StrSql = StrSql + "ON USERPERMISSIONS.SERVICEID=SERVICES.SERVICEID "
                StrSql = StrSql + "INNER JOIN USERS "
                StrSql = StrSql + "ON USERS.USERID=USERPERMISSIONS.USERID AND USERS.LICENSEID= " + LicenseId + " "
                StrSql = StrSql + "GROUP BY SERVICES.SERVICEID,SERVICES.SERVICETYPE,SERVICES.SERVICEDE "
                StrSql = StrSql + "HAVING SERVICES.SERVICETYPE = 1 "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetModuleDetailsByLicense:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSelectionData(ByVal UserId As String, ByVal Password As String, ByVal schema As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ConnectionString As String = System.Configuration.ConfigurationManager.AppSettings(schema)
            Try
                StrSql = "select userid from Selection "
                'StrSql = StrSql + "WHERE Upper(username)='" + UserName.ToUpper() + "' " 
                StrSql = StrSql + "WHERE USERID=" + UserId.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, ConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetSelectionData:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function ValidateLicenseUser(ByVal UserId As String, ByVal schema As String, ByVal modName As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ConnectionString As String = String.Empty
            If modName = "CONTR" Or modName = "PACKPROD" Then
                ConnectionString = ContractConnection
                'ElseIf modName = "PACKPROD" Then
                '    ConnectionString = PackProdConnection
            ElseIf modName = "REPORT" Then
                ConnectionString = ReportConnection
            ElseIf modName = "Market1" Then
                ConnectionString = MarketConnection
            Else
                ConnectionString = EconConnection
            End If
            Try
                StrSql = "select userid from license "
                StrSql = StrSql + "WHERE UserId=" + UserId.ToString() + " "
                'StrSql = StrSql + "WHERE Upper(username)='" + UserName.ToUpper() + "'"
                Dts = odbUtil.FillDataSet(StrSql, ConnectionString)

                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:ValidateLicenseUser:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUID() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty

            Try
                StrSql = "SELECT SEQNEWID.nextval NewId FROM dual "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetUID:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetAdministratorUserDetails(ByVal CCompany As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT USERS.USERID,  "
                StrSql = StrSql + "NVL(USERS.USERNAME,'')USERNAME, "
                StrSql = StrSql + "NVL(USERS.PASSWORD,'')PASSWORD, "
                ' StrSql = StrSql + "NVL(USERS.COMPANY,'')COMPANY, "
                StrSql = StrSql + "(CASE WHEN COMPANY.COMPANYNAME='' THEN '' ELSE COMPANY.COMPANYNAME END)COMPANY, "
                StrSql = StrSql + "NVL(USERS.LICENSEID,'')LICENSENO, "
                StrSql = StrSql + "(CASE WHEN NVL(TRIM(USERS.ISCORPORATEUSER),'A')='C' "
                StrSql = StrSql + "THEN 'Y' "
                StrSql = StrSql + "ELSE 'N' "
                StrSql = StrSql + "END)ISCORPORATEUSERF, "
                StrSql = StrSql + "(CASE WHEN NVL(TRIM(USERS.ISCORPORATEUSER),'A')='C' "
                StrSql = StrSql + "THEN 'Yes' "
                StrSql = StrSql + "ELSE 'No' "
                StrSql = StrSql + "END)ISCORPORATEUSERT "
                StrSql = StrSql + "FROM USERS "
                StrSql = StrSql + "LEFT OUTER JOIN SHOPPING.COMPANY "
                StrSql = StrSql + "ON COMPANY.COMPANYID=USERS.COMPANYID "
                StrSql = StrSql + "WHERE Upper(LTRIM(RTRIM(COMPANY)))='" + CCompany.ToUpper() + "' "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetAdministratorUserDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUserDetailsByID(ByVal uId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                'Bug#389
                StrSql = "SELECT USERNAME, PASSWORD, USERID, "
                StrSql = StrSql + "(CASE WHEN COMPANY.COMPANYNAME='' THEN '' ELSE COMPANY.COMPANYNAME END)COMPANY,"
                StrSql = StrSql + " LICENSEID,VUPDATEDATE FROM USERS  "
                StrSql = StrSql + "LEFT OUTER JOIN SHOPPING.COMPANY "
                StrSql = StrSql + "ON COMPANY.COMPANYID=USERS.COMPANYID "
                'Bug#389
                StrSql = StrSql + "WHERE USERID LIKE '" + uId + "' "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetUserDetailsByID:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUserDetailsByName(ByVal UserName As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERNAME, PASSWORD, USERID,"
                StrSql = StrSql + "(CASE WHEN COMPANY.COMPANYNAME='' THEN '' ELSE COMPANY.COMPANYNAME END)COMPANY,"
                StrSql = StrSql + " LICENSEID FROM USERS  "
                StrSql = StrSql + "LEFT OUTER JOIN SHOPPING.COMPANY "
                StrSql = StrSql + "ON COMPANY.COMPANYID=USERS.COMPANYID "
                StrSql = StrSql + " WHERE Upper(UserName) LIKE '" + UserName.ToUpper() + "' "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetUserDetailsByName:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetInuseDetailsByUser(ByVal UserID As String, ByVal schema As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ConnectionString As String = System.Configuration.ConfigurationManager.AppSettings(schema)
            Try
                StrSql = "SELECT COUNT(1) AS COUNT  "
                StrSql = StrSql + "FROM INUSE "
                StrSql = StrSql + "INNER JOIN ECON.USERS "
                StrSql = StrSql + "ON USERS.USERID=" + UserID + " "
                StrSql = StrSql + "AND USERS.LICENSEID=INUSE.LICENSEID "
                StrSql = StrSql + "AND USERS.USERID=INUSE.USERID "
                'StrSql = StrSql + "AND UPPER(USERS.USERNAME)=UPPER(INUSE.USERNAME) "

                Dts = odbUtil.FillDataSet(StrSql, ConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetInuseDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetMaxCountByLic(ByVal UserID As String, ByVal schema As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MAXCOUNT,  "
                StrSql = StrSql + "CONTRMAXCOUNT, "
                StrSql = StrSql + "REPMAXCOUNT,MRKTMAXCOUNT "
                StrSql = StrSql + "FROM LICENSEMASTER "
                StrSql = StrSql + "INNER JOIN USERS "
                StrSql = StrSql + "ON USERS.LICENSEID=LICENSEMASTER.LICENSEID "
                StrSql = StrSql + "WHERE USERS.USERID=" + UserID + " "


                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetMaxCountByLic:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetInuseDetailsByLicense(ByVal LicenseId As String, ByVal schema As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ConnectionString As String = System.Configuration.ConfigurationManager.AppSettings(schema)
            Try
                StrSql = "SELECT USERS.USERNAME USERNAME,  "
                StrSql = StrSql + "SERVERDATE "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT LOGINLOG.USERID, "
                StrSql = StrSql + "MAX(LOGINLOG.SERVERDATE) SERVERDATE "
                StrSql = StrSql + "FROM  LOGINLOG "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "LOGINLOG.USERID "
                StrSql = StrSql + "IN ( "
                StrSql = StrSql + "SELECT INUSE.USERID "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "INUSE "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "LICENSEID= " + LicenseId + " "
                StrSql = StrSql + ") "
                StrSql = StrSql + "GROUP BY LOGINLOG.USERID "
                StrSql = StrSql + ") QRY "
                StrSql = StrSql + "INNER JOIN ECON.USERS "
                StrSql = StrSql + "ON USERS.USERID=QRY.USERID "

                Dts = odbUtil.FillDataSet(StrSql, ConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetInuseDetails:" + ex.Message.ToString())
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
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
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

        Public Function GetInUseTableInfo(ByVal UserId As String, ByVal strCon As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ConnectionString As String = System.Configuration.ConfigurationManager.AppSettings(strCon)
            Try
                StrSql = "select * from inuse  WHERE userid= " + UserId.ToString() + " "
                'StrSql = "select * from inuse  WHERE Upper(username)= '" + UserName.ToUpper() + "'"
                Dts = odbUtil.FillDataSet(StrSql, ConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetInUseTableInfo:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUserCount(ByVal userId As Integer) As Integer
            Dim odbUtil As New DBUtil()
            Dim Dts As New DataSet()
            Dim count As Integer
            Dim StrSql As String = String.Empty
            Try
                StrSql = " SELECT COUNT (*) AS USERCOUNT  "
                StrSql = StrSql & "FROM USERS  "
                StrSql = StrSql & "WHERE LTRIM(RTRIM(COMPANY)) = (SELECT LTRIM(RTRIM(USERS.COMPANY)) FROM USERS WHERE USERS.USERID=" & userId & ") "
                StrSql = StrSql & "AND NVL(TRIM(USERS.ISCORPORATEUSER),'A')='C' "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                count = Dts.Tables(0).Rows(0).Item(0)
                Return count
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetUserCount:" + ex.Message.ToString())
                Return count
            End Try


        End Function
        Public Function GetMaxCarporateUsers(ByVal CUserId As String) As Integer
            Dim odbUtil As New DBUtil()
            Dim Dts As New DataSet()
            Dim userLimit As Integer
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT NVL(USERLIMIT,0)USERLIMIT FROM CORPORATEUSERS WHERE CORPORATEUSERID = " + CUserId + " "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                userLimit = Dts.Tables(0).Rows(0).Item(0)
                Return userLimit
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetMaxCarporateUsers:" + ex.Message.ToString())
                Return userLimit
            End Try


        End Function
        Public Function GetMessagesCnt(ByVal UserId As String) As Integer
            Dim Ds As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim msgcnt As New Integer
            Try


                StrSql = "SELECT COUNT(1)MSGCNT  "
                StrSql = StrSql + "FROM MESSAGES "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 "
                StrSql = StrSql + "FROM USEERDELETDMSG "
                StrSql = StrSql + "WHERE USEERDELETDMSG.MESSAGEID = MESSAGES.MESSAGEID "
                StrSql = StrSql + "AND USEERDELETDMSG.USERID = " + UserId.ToString() + ""
                StrSql = StrSql + ") "
                StrSql = StrSql + "AND MESSAGES.EXPIRYDATE >= SYSDATE "
                Ds = odbUtil.FillDataSet(StrSql, EconConnection)
                If Ds.Tables(0).Rows.Count > 0 Then
                    msgcnt = Convert.ToInt32(Ds.Tables(0).Rows(0).Item("MSGCNT"))
                End If
                Return msgcnt
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#Region "Dow Login"
        Public Function ValidateDowUser(ByVal UserName As String, ByVal Password As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT "
                StrSql = StrSql + "DOWUSER.USERNAME, "
                StrSql = StrSql + "DOWUSER.PASSWORD, "
                StrSql = StrSql + "DOWUSER.FIRSTNAME, "
                StrSql = StrSql + "DOWUSER.LASTNAME "
                StrSql = StrSql + "FROM DOWUSER "
                StrSql = StrSql + "WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "' "
                StrSql = StrSql + "AND PASSWORD='" + Password + "' "
                Dts = odbUtil.FillDataSet(StrSql, DowConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:ValidateDowUser:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Comp Module"
        Public Function GetInuseDetailsByCompUser(ByVal UserID As String, ByVal schema As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ConnectionString As String = System.Configuration.ConfigurationManager.AppSettings(schema)
            Try
                StrSql = "SELECT COUNT(1) AS COUNT  "
                StrSql = StrSql + "FROM INUSECOMP "
                StrSql = StrSql + "INNER JOIN ECON.USERS "
                StrSql = StrSql + "ON USERS.USERID=" + UserID + " "
                StrSql = StrSql + "AND USERS.LICENSEID=INUSECOMP.LICENSEID "
                StrSql = StrSql + "AND USERS.USERID=INUSECOMP.USERID "
                'StrSql = StrSql + "AND UPPER(USERS.USERNAME)=UPPER(INUSECOMP.USERNAME) "

                Dts = odbUtil.FillDataSet(StrSql, ConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetInuseDetailsByCompUser:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCOMPInuseDetailsByLicense(ByVal LicenseId As String, ByVal schema As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ConnectionString As String = System.Configuration.ConfigurationManager.AppSettings(schema)
            Try
                StrSql = "SELECT USERS.USERNAME USERNAME,  "
                StrSql = StrSql + "SERVERDATE "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT LOGINLOGCOMP.USERID, "
                StrSql = StrSql + "MAX(LOGINLOGCOMP.SERVERDATE) SERVERDATE "
                StrSql = StrSql + "FROM  LOGINLOGCOMP "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "LOGINLOGCOMP.USERID "
                'StrSql = StrSql + "UPPER(LOGINLOGCOMP.USERNAME) "
                StrSql = StrSql + "IN ( "
                'StrSql = StrSql + "SELECT UPPER(INUSECOMP.USERNAME) "
                StrSql = StrSql + "SELECT INUSECOMP.USERID "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "INUSECOMP "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "LICENSEID= " + LicenseId + " "
                StrSql = StrSql + ") "
                StrSql = StrSql + "GROUP BY LOGINLOGCOMP.USERID "
                'StrSql = StrSql + "GROUP BY UPPER(LOGINLOGCOMP.USERNAME) "
                StrSql = StrSql + ") QRY "
                StrSql = StrSql + "INNER JOIN ECON.USERS "
                StrSql = StrSql + "ON USERS.USERID=QRY.USERID "
                'StrSql = StrSql + "ON UPPER(USERS.USERNAME)=QRY.USERNAME "

                Dts = odbUtil.FillDataSet(StrSql, ConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetCOMPInuseDetailsByLicense:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetInUseCOMPTableInfo(ByVal UserId As String, ByVal strCon As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ConnectionString As String = System.Configuration.ConfigurationManager.AppSettings(strCon)
            Try
                'StrSql = "select * from INUSECOMP  WHERE Upper(username)= '" + UserName.ToUpper() + "'"
                StrSql = "select * from INUSECOMP  WHERE USERID= " + UserId.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, ConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetInUseCOMPTableInfo:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Market Subscription"
        Public Function GetSubscriptionDetailsBACK(ByVal UserID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                'StrSql = "SELECT SERVICENAME, SERVICEID, US.LICENSEID  "
                'StrSql = StrSql + "FROM USERMARKET1SERVICE "
                'StrSql = StrSql + "INNER JOIN ECON.USERS US "
                'StrSql = StrSql + "ON   USERMARKET1SERVICE.LICENSEID=US.LICENSEID "
                'StrSql = StrSql + "WHERE US.USERID=" + UserID

                StrSql = "SELECT USERMARKET1SERVICE.SERVICENAME, USERMARKET1SERVICE.SERVICEID, US.LICENSEID  "
                StrSql = StrSql + "FROM USERMARKET1SERVICE "
                StrSql = StrSql + "INNER JOIN ECON.USERS US "
                StrSql = StrSql + "ON   USERMARKET1SERVICE.LICENSEID=US.LICENSEID "
                StrSql = StrSql + "INNER JOIN ECON.USERPERMISSIONS "
                StrSql = StrSql + "ON USERPERMISSIONS.USERID = US.USERID "
                StrSql = StrSql + "INNER JOIN ECON.SERVICES "
                StrSql = StrSql + "ON SERVICES.SERVICEID = USERPERMISSIONS.SERVICEID "
                StrSql = StrSql + "WHERE US.USERID=" + UserID
                StrSql = StrSql + "AND SERVICES.SERVICEDE='MARKET1' "

                Dts = odbUtil.FillDataSet(StrSql, MarketConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetSubscriptionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetInuseDetailsByLicMKT(ByVal UserID As String) As DataSet
            Dim Dts, Dts1, Dts2 As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("SystemConnectionString")
            Dim ConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("Market1ConnectionString")
            Try
                StrSql = "SELECT SUM(COUNT) COUNT FROM  "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM ECON.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM ECON2.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM ECON3.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM ECON4.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM SUSTAIN.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM SUSTAIN2.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM SUSTAIN3.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM SUSTAIN4.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM SCHEM1.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM ECHEM1.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM SPEC.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM DISTRIBUTION.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM SDISTRIBUTION.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM RETAIL.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM VALUECHAIN.INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM ECON.INUSECOMP "
                StrSql = StrSql + "WHERE INUSECOMP.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM SUSTAIN.INUSECOMP "
                StrSql = StrSql + "WHERE INUSECOMP.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserID + ") "
                StrSql = StrSql + ") "
                Dts = odbUtil.FillDataSet(StrSql, ConnectionString)

                StrSql = ""
                StrSql = "SELECT COUNT(1) AS COUNT "
                StrSql = StrSql + "FROM INUSE "
                StrSql = StrSql + "WHERE INUSE.LICENSEID=(SELECT LICENSEID FROM USERS@ECON8 WHERE USERID=" + UserID + ") "
                Dts1 = odbUtil.FillDataSet(StrSql, ConnectionString1)

                StrSql = ""
                StrSql = "SELECT (" + Dts.Tables(0).Rows(0).Item("COUNT").ToString() + "+" + Dts1.Tables(0).Rows(0).Item("COUNT").ToString() + " )AS COUNT "
                StrSql = StrSql + "FROM DUAL "
                Dts2 = odbUtil.FillDataSet(StrSql, ConnectionString1)

                Return Dts2
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetInuseDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetInuseDetailsByUserMKT(ByVal UserID As String, ByVal schema As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ConnectionString As String = System.Configuration.ConfigurationManager.AppSettings(schema)
            Try
                StrSql = "SELECT COUNT(1) AS COUNT  "
                StrSql = StrSql + "FROM INUSE "
                StrSql = StrSql + "INNER JOIN USERS@ECON8 "
                StrSql = StrSql + "ON USERS.USERID=" + UserID + " "
                StrSql = StrSql + "AND USERS.LICENSEID=INUSE.LICENSEID "
                StrSql = StrSql + "AND USERS.USERID=INUSE.USERID "

                Dts = odbUtil.FillDataSet(StrSql, ConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetInuseDetailsByUserMKT:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetInuseDetailsByLicenseMKT(ByVal LicenseId As String, ByVal schema As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ConnectionString As String = System.Configuration.ConfigurationManager.AppSettings(schema)
            Try
                StrSql = "SELECT USERS.USERNAME USERNAME,  "
                StrSql = StrSql + "SERVERDATE "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT LOGINLOG.USERID, "
                StrSql = StrSql + "MAX(LOGINLOG.SERVERDATE) SERVERDATE "
                StrSql = StrSql + "FROM  LOGINLOG "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "LOGINLOG.USERID "
                StrSql = StrSql + "IN ( "
                StrSql = StrSql + "SELECT INUSE.USERID "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "INUSE "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "LICENSEID= " + LicenseId + " "
                StrSql = StrSql + ") "
                StrSql = StrSql + "GROUP BY LOGINLOG.USERID "
                StrSql = StrSql + ") QRY "
                StrSql = StrSql + "INNER JOIN USERS@ECON8 "
                StrSql = StrSql + "ON USERS.USERID=QRY.USERID "

                Dts = odbUtil.FillDataSet(StrSql, ConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetInuseDetailsByLicenseMKT:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetSubscriptionDetails(ByVal UserID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                'StrSql = "SELECT SERVICENAME, SERVICEID, US.LICENSEID  "
                'StrSql = StrSql + "FROM USERMARKET1SERVICE "
                'StrSql = StrSql + "INNER JOIN ECON.USERS US "
                'StrSql = StrSql + "ON   USERMARKET1SERVICE.LICENSEID=US.LICENSEID "
                'StrSql = StrSql + "WHERE US.USERID=" + UserID

                StrSql = "SELECT USERMARKET1SERVICE.SERVICENAME, USERMARKET1SERVICE.SERVICEID, US.LICENSEID  "
                StrSql = StrSql + "FROM USERMARKET1SERVICE "
                StrSql = StrSql + "INNER JOIN USERS@ECON8 US "
                StrSql = StrSql + "ON   USERMARKET1SERVICE.LICENSEID=US.LICENSEID "
                StrSql = StrSql + "INNER JOIN USERPERMISSIONS@ECON8 "
                StrSql = StrSql + "ON USERPERMISSIONS.USERID = US.USERID "
                StrSql = StrSql + "INNER JOIN SERVICES@ECON8 "
                StrSql = StrSql + "ON SERVICES.SERVICEID = USERPERMISSIONS.SERVICEID "
                StrSql = StrSql + "WHERE US.USERID=" + UserID
                StrSql = StrSql + "AND SERVICES.SERVICEDE='MARKET1' "

                Dts = odbUtil.FillDataSet(StrSql, MarketConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:GetSubscriptionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region
#Region "Savvypack project"
        Public Function ValidateUserSavvy(ByVal UserName As String, ByVal Password As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select USERS.USERID,  "
                StrSql = StrSql + "USERS.USERNAME, "
                StrSql = StrSql + "USERS.PASSWORD, "
                StrSql = StrSql + "USERS.LICENSEID, "
                StrSql = StrSql + "ISVALIDEMAIL,"
                StrSql = StrSql + "ISAPPROVED,"
                'StrSql = StrSql + "USERS.COMPANY "
                StrSql = StrSql + "(CASE WHEN COMPANY.COMPANYNAME='' THEN '' ELSE COMPANY.COMPANYNAME END)COMPANY "
                'Bug#389
                StrSql = StrSql + "FROM USERS "
                StrSql = StrSql + "LEFT OUTER JOIN SHOPPING.COMPANY "
                StrSql = StrSql + "ON COMPANY.COMPANYID=USERS.COMPANYID "
                StrSql = StrSql + "WHERE Upper(username)='" + UserName.ToUpper() + "'"
                StrSql = StrSql + " AND password='" + Password + "'"
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("LoginGetData:ValidateUserSavvy:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region
    End Class
End Class
