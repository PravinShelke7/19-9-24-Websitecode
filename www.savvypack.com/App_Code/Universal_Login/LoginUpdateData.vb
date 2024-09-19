Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System

Public Class LoginUpdateData
    Public Class Selectdata
        Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
        Dim Econ2Connection As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
        Dim Econ3Connection As String = System.Configuration.ConfigurationManager.AppSettings("Econ3ConnectionString")
        Dim Econ4Connection As String = System.Configuration.ConfigurationManager.AppSettings("Econ4ConnectionString")
        Dim Sustain1Connection As String = System.Configuration.ConfigurationManager.AppSettings("Sustain1ConnectionString")
        Dim Sustain2Connection As String = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")
        Dim Sustain3Connection As String = System.Configuration.ConfigurationManager.AppSettings("Sustain3ConnectionString")
        Dim Sustain4Connection As String = System.Configuration.ConfigurationManager.AppSettings("Sustain4ConnectionString")
        Dim SpecConnection As String = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")
        Dim Schem1Connection As String = System.Configuration.ConfigurationManager.AppSettings("Schem1ConnectionString")
        Dim Echem1Connection As String = System.Configuration.ConfigurationManager.AppSettings("Echem1ConnectionString")
        Dim Market1Connection As String = System.Configuration.ConfigurationManager.AppSettings("Market1ConnectionString")
        Dim ContractConnection As String = System.Configuration.ConfigurationManager.AppSettings("ContractConnectionString")
        Dim ReportConnection As String = System.Configuration.ConfigurationManager.AppSettings("ReportConnectionString")
        Dim DistributionConnection As String = System.Configuration.ConfigurationManager.AppSettings("DistributionConnectionString")
        Dim PackProdConnection As String = System.Configuration.ConfigurationManager.AppSettings("PackProdConnectionString")
        Dim RetailConnection As String = System.Configuration.ConfigurationManager.AppSettings("RetailConnectionString")
        Dim SDistributionConnection As String = System.Configuration.ConfigurationManager.AppSettings("SDistributionConnectionString")
        Dim VChainConnection As String = System.Configuration.ConfigurationManager.AppSettings("VChainConnectionString")
        Dim SBAConnection As String = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
        Dim MedEcon1Connection As String = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
        Dim MedEcon2Connection As String = System.Configuration.ConfigurationManager.AppSettings("MedEcon2ConnectionString")
        Dim MedSustain1Connection As String = System.Configuration.ConfigurationManager.AppSettings("MedSustain1ConnectionString")
        Dim MedSustain2Connection As String = System.Configuration.ConfigurationManager.AppSettings("MedSustain2ConnectionString")
        Dim MoldE1Connection As String = System.Configuration.ConfigurationManager.AppSettings("MoldE1ConnectionString")
        Dim MoldE2Connection As String = System.Configuration.ConfigurationManager.AppSettings("MoldE2ConnectionString")
        Dim MoldS1Connection As String = System.Configuration.ConfigurationManager.AppSettings("MoldS1ConnectionString")
        Dim MoldS2Connection As String = System.Configuration.ConfigurationManager.AppSettings("MoldS2ConnectionString")
        Dim SavvyProConnection As String = System.Configuration.ConfigurationManager.AppSettings("SavvyPackProConnectionString")

#Region "Bug#Security"
        Public Sub UUnlockAccount(ByVal UserName As String, ByVal Status As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Try
                'Update AccountLocked
                StrSql = "UPDATE USERS "
                StrSql = StrSql + " SET ISLOCK='" + Status + "'"
                StrSql = StrSql + " where UPPER(USERNAME)='" + UserName.ToUpper() + "'"
                odbUtil.UpIns(StrSql, EconConnection)

            Catch ex As Exception
                Throw New Exception("LoginUpdateData:UUnlockAccount:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub UAccountLocked(ByVal UserName As String, ByVal Status As String, ByVal LockDate As Date)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Try
                'Update AccountLocked
                StrSql = "UPDATE USERS "
                StrSql = StrSql + " SET ISLOCK='" + Status + "'"
                StrSql = StrSql + ",LOCKDATE=sysdate"
                StrSql = StrSql + " where UPPER(USERNAME)='" + UserName.ToUpper() + "'"
                odbUtil.UpIns(StrSql, EconConnection)

            Catch ex As Exception
                Throw New Exception("LoginUpdateData:UAccountLocked:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub UpdatePWDLog(ByVal UserID As String, ByVal pwd As String, ByVal count As Integer)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim Dts As New DataSet()
            Try
                StrSql = "SELECT * "
                StrSql = StrSql + " FROM PASSWORDLOG "
                StrSql = StrSql + "WHERE Upper(USERID)=" + UserID.ToString() + ""
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                count = Dts.Tables(0).Rows.Count
                If count < 5 Then
                    StrSql = "INSERT INTO PASSWORDLOG(USERID,PASSWORD,SEQUENCE) "
                    count = count + 1
                    StrSql = StrSql + "VALUES(" + UserID.ToString() + ",'" + pwd.ToString() + "'," + count.ToString() + ") "
                    odbUtil.UpIns(StrSql, EconConnection)
                Else
                    StrSql = "DELETE FROM PASSWORDLOG WHERE USERID=" + UserID + " AND SEQUENCE=1"
                    odbUtil.UpIns(StrSql, EconConnection)
                    StrSql = "UPDATE PASSWORDLOG "
                    StrSql = StrSql + " SET SEQUENCE=1 WHERE SEQUENCE=2 AND USERID=" + UserID
                    odbUtil.UpIns(StrSql, EconConnection)

                    StrSql = "UPDATE PASSWORDLOG "
                    StrSql = StrSql + " SET SEQUENCE=2 WHERE SEQUENCE=3 AND USERID=" + UserID
                    odbUtil.UpIns(StrSql, EconConnection)

                    StrSql = "UPDATE PASSWORDLOG "
                    StrSql = StrSql + " SET SEQUENCE=3 WHERE SEQUENCE=4 AND USERID=" + UserID
                    odbUtil.UpIns(StrSql, EconConnection)

                    StrSql = "UPDATE PASSWORDLOG "
                    StrSql = StrSql + " SET SEQUENCE=4 WHERE SEQUENCE=5 AND USERID=" + UserID
                    odbUtil.UpIns(StrSql, EconConnection)

                    StrSql = "INSERT INTO PASSWORDLOG(USERID,PASSWORD,SEQUENCE) "
                    StrSql = StrSql + "VALUES(" + UserID + ",'" + pwd + "',5) "
                    odbUtil.UpIns(StrSql, EconConnection)

                End If

            Catch ex As Exception
                Throw New Exception("UsersUpdateData:ChangePWD:" + ex.Message.ToString())
            End Try

        End Sub
#End Region
        Public Sub InuseLoginInsert(ByVal sessionID As String, ByVal UserId As String, ByVal licenseID As String, ByVal schema As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim ConnectionString As String = System.Configuration.ConfigurationManager.AppSettings(schema)
            Try
                'Insert into Inuse table
                StrSql = "Insert into inuse (sessionID,UserId,licenseID) "
                StrSql = StrSql + "VALUES('" + sessionID + "'," + UserId + ",'" + licenseID + "')"
                odbUtil.UpIns(StrSql, ConnectionString)

                'Insert into loginlog table
                StrSql = "Insert into loginlog (serverdate,UserId,sessionID) "
                StrSql = StrSql + "VALUES(sysdate," + UserId + ",'" + sessionID + "')"
                odbUtil.UpIns(StrSql, ConnectionString)


            Catch ex As Exception
                Throw New Exception("LoginUpdateData:InuseLoginInsert:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub InuseLoginInsertContract(ByVal sessionID As String, ByVal UserId As String, ByVal licenseID As String, ByVal clientIP As String, ByVal clientCO As String, ByVal clientBROWSER As String, ByVal schema As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim ConnectionString As String = System.Configuration.ConfigurationManager.AppSettings(schema)
            Try
                'Insert into Inuse table
                StrSql = "Insert into inuse (sessionID,userid,licenseID) "
                StrSql = StrSql + "VALUES('" + sessionID + "'," + UserId.ToString() + ",'" + licenseID + "')"
                odbUtil.UpIns(StrSql, ConnectionString)

                'Insert into loginlog table
                StrSql = "Insert into loginlog (serverdate,userid,sessionID,clientIP,clientCO,clientBROWSER) "
                StrSql = StrSql + "VALUES(sysdate," + UserId.ToString() + ",'" + sessionID + "','" + clientIP + "','" + clientCO + "','" + clientBROWSER + "')"
                odbUtil.UpIns(StrSql, ConnectionString)

            Catch ex As Exception
                Throw New Exception("LoginUpdateData:InuseLoginInsertContract:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub InuseLoginInsertReport(ByVal sessionID As String, ByVal UserId As String, ByVal licenseID As String, ByVal Password As String, ByVal schema As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim ConnectionString As String = System.Configuration.ConfigurationManager.AppSettings(schema)
            Try
                'Insert into Inuse table
                StrSql = "Insert into inuse (sessionID,userid,licenseID) "
                StrSql = StrSql + "VALUES('" + sessionID + "'," + UserId.ToString() + ",'" + licenseID + "')"
                odbUtil.UpIns(StrSql, ConnectionString)

                'Insert into loginlog table
                StrSql = "Insert into loginlog (serverdate,userid) "
                StrSql = StrSql + "VALUES(sysdate," + UserId.ToString() + ")"
                odbUtil.UpIns(StrSql, ConnectionString)

            Catch ex As Exception
                Throw New Exception("LoginUpdateData:InuseLoginInsertReport:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub InuseLoginInsertPackProd(ByVal sessionID As String, ByVal UserId As String, ByVal licenseID As String, ByVal clientIP As String, ByVal clientCO As String, ByVal clientBROWSER As String, ByVal schema As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim ConnectionString As String = System.Configuration.ConfigurationManager.AppSettings(schema)
            Try
                'Insert into Inuse table
                StrSql = "Insert into inuse (sessionID,userid,licenseID) "
                StrSql = StrSql + "VALUES('" + sessionID + "'," + UserId.ToString() + ",'" + licenseID + "')"
                odbUtil.UpIns(StrSql, ConnectionString)

                'Insert into loginlog table
                StrSql = "Insert into loginlog (serverdate,userid,sessionID,clientIP,clientCO,clientBROWSER) "
                StrSql = StrSql + "VALUES(sysdate," + UserId.ToString() + ",'" + sessionID + "','" + clientIP + "','" + clientCO + "','" + clientBROWSER + "')"
                odbUtil.UpIns(StrSql, ConnectionString)

            Catch ex As Exception
                Throw New Exception("LoginUpdateData:InuseLoginInsertPackProd:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub InsertSelection(ByVal UserId As String, ByVal Password As String, ByVal schema As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim ConnectionString As String = System.Configuration.ConfigurationManager.AppSettings(schema)
            Try
                'Insert into Selection table
                StrSql = "INSERT INTO SELECTION (SERVERDATE,USERID, COMPANY, PRODUCT, COMPANY2, STATE, SERVICE, DESIGN, MACHINE, PROCESS, CUSTOMER,COUNTRY) "
                StrSql = StrSql + "VALUES (sysdate," + UserId.ToString() + ",0,0,0,0,0,0,0,0,0,-1)"
                odbUtil.UpIns(StrSql, ConnectionString)

            Catch ex As Exception
                Throw New Exception("LoginUpdateData:InsertSelection:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub ULoginInsert(ByVal UID As String, ByVal UserName As String, ByVal UserPassword As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String

            Try
                'Delete the existing UID from Ulogin table
                StrSql = "delete ulogin where id= " + UID
                odbUtil.UpIns(StrSql, EconConnection)

                'Insert into Ulogin table
                StrSql = "Insert into Ulogin (ID,uname,upwd,logindate) "
                StrSql = StrSql + "VALUES('" + UID + "','" + UserName + "','" + UserPassword + "',sysdate)"
                odbUtil.UpIns(StrSql, EconConnection)


            Catch ex As Exception
                Throw New Exception("LoginUpdateData:ULoginInsert:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub UpdateInuseLogoutLog(ByVal username As String, ByVal password As String)
            Dim odbutil As New DBUtil()
            Dim objGetData As New LoginGetData.Selectdata()
            Dim ds As New DataSet()
            Dim UserId As String = ""
            Try
                ds = objGetData.ValidateUserByUserName(username)
                If ds.Tables(0).Rows.Count > 0 Then
                    UserId = ds.Tables(0).Rows(0).Item("USERID").ToString()

                    'Getting and deleting Inuse detail for ECon1
                    DeleteInuseDetails(UserId, EconConnection, "EconConnectionString", "")
                    'Getting and deleting Inuse detail for ECon1
                    DeleteInuseDetails(UserId, Econ2Connection, "Econ2ConnectionString", "")
                    'Getting and deleting Inuse detail for ECon3
                    DeleteInuseDetails(UserId, Econ3Connection, "Econ3ConnectionString", "")
                    'Getting and deleting Inuse detail for ECon4
                    DeleteInuseDetails(UserId, Econ4Connection, "Econ4ConnectionString", "")
                    'Getting and deleting Inuse detail for Sustain1
                    DeleteInuseDetails(UserId, Sustain1Connection, "Sustain1ConnectionString", "")
                    'Getting and deleting Inuse detail for Sustain2
                    DeleteInuseDetails(UserId, Sustain2Connection, "Sustain2ConnectionString", "")
                    'Getting and deleting Inuse detail for Sustain3
                    DeleteInuseDetails(UserId, Sustain3Connection, "Sustain3ConnectionString", "")
                    'Getting and deleting Inuse detail for Sustain4
                    DeleteInuseDetails(UserId, Sustain4Connection, "Sustain4ConnectionString", "")
                    'Getting and deleting Inuse detail for Spec
                    DeleteInuseDetails(UserId, SpecConnection, "SpecConnectionString", "")
                    'Getting and deleting Inuse detail for SChem1
                    DeleteInuseDetails(UserId, Schem1Connection, "Schem1ConnectionString", "")
                    'Getting and deleting Inuse detail for EChem1
                    DeleteInuseDetails(UserId, Echem1Connection, "Echem1ConnectionString", "")
                    'Getting and deleting Inuse detail for Market1
                    DeleteInuseDetails(UserId, Market1Connection, "Market1ConnectionString", "")
                    'Getting and deleting Inuse detail for Contract
                    DeleteInuseDetails(UserId, ContractConnection, "ContractConnectionString", "contract")
                    'Getting and deleting Inuse detail for Reports
                    'DeleteInuseDetails(UserId, ReportConnection, "ReportConnectionString", "report")
                    'Getting and deleting Inuse detail for Distribution
                    DeleteInuseDetails(UserId, DistributionConnection, "DistributionConnectionString", "")
                    'Getting and deleting Inuse detail for Retail
                    DeleteInuseDetails(UserId, RetailConnection, "RetailConnectionString", "")
                    'Getting and deleting Inuse detail for Packaging Producer
                    DeleteInuseDetails(UserId, PackProdConnection, "PackProdConnectionString", "PackProd")
                    'Getting and deleting Inuse detail for SDistribution
                    DeleteInuseDetails(UserId, SDistributionConnection, "SDistributionConnectionString", "SDist")
                    'Getting and deleting Inuse detail for Value Chain
                    DeleteInuseDetails(UserId, VChainConnection, "VChainConnectionString", "VChain")
                    'Getting and deleting Inuse detail for ECon1 Comp
                    DeleteCOMPInuseDetails(UserId, EconConnection, "EconConnectionString", "")
                    'Getting and deleting Inuse detail for Sustain1 COMP
                    DeleteCOMPInuseDetails(UserId, Sustain1Connection, "Sustain1ConnectionString", "")
                    'Getting and deleting Inuse detail for SBA
                    DeleteInuseDetails(UserId, SBAConnection, "SBAConnectionString", "Sassist")
                End If

            Catch ex As Exception
                Throw New Exception("LoginUpdateData:UpdateInuseLogoutLog:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub DeleteInuseDetails(ByVal UserId As String, ByVal strCon As String, ByVal schema As String, ByVal flag As String)
            Dim odbutil As New DBUtil()
            Dim StrSql As String
            Dim objGetData As New LoginGetData.Selectdata()
            Dim ds As New DataSet()
            Try

                ds = objGetData.GetInUseTableInfo(UserId, schema)
                If ds.Tables(0).Rows.Count > 0 Then
                    'Delete from inuse table
                    StrSql = "delete from inuse where USERID=" + UserId.ToString() + " "
                    'StrSql = "delete from inuse where Upper(username)='" + username.ToUpper() + "'"
                    odbutil.UpIns(StrSql, strCon)


                    'Insert into Logoutlog table
                    StrSql = "insert into Logoutlog(serverdate,userid,path) "
                    StrSql = StrSql + "values(sysdate," + UserId.ToString() + ",'LR')"
                    odbutil.UpIns(StrSql, strCon)


                End If


            Catch ex As Exception
                Throw New Exception("LoginUpdateData:DeleteInuseDetails:" + ex.Message.ToString())
            End Try


        End Sub
        Public Sub UpdateLogOffDetails(ByVal UserId As String, ByVal Password As String, ByVal type As String, ByVal sessionId As String)

            If type = "E1" Then
                LogOffUpdate(UserId, sessionId, EconConnection)
            ElseIf type = "E2" Then
                LogOffUpdate(UserId, sessionId, Econ2Connection)
            ElseIf type = "E3" Then
                LogOffUpdate(UserId, sessionId, Econ3Connection)
            ElseIf type = "E4" Then
                LogOffUpdate(UserId, sessionId, Econ4Connection)
            ElseIf type = "S1" Then
                LogOffUpdate(UserId, sessionId, Sustain1Connection)
            ElseIf type = "S2" Then
                LogOffUpdate(UserId, sessionId, Sustain2Connection)
            ElseIf type = "S3" Then
                LogOffUpdate(UserId, sessionId, Sustain3Connection)
            ElseIf type = "S4" Then
                LogOffUpdate(UserId, sessionId, Sustain4Connection)
            ElseIf type = "SCH1" Then
                LogOffUpdate(UserId, sessionId, Schem1Connection)
            ElseIf type = "ECH1" Then
                LogOffUpdate(UserId, sessionId, Echem1Connection)
            ElseIf type = "M1" Then
                LogOffUpdate(UserId, sessionId, Market1Connection)
            ElseIf type = "SPEC" Then
                LogOffUpdate(UserId, sessionId, SpecConnection)
            ElseIf type = "Contract" Then
                LogOffContractUpdate(UserId, sessionId, ContractConnection)
            ElseIf type = "DIST" Then
                LogOffUpdate(UserId, sessionId, DistributionConnection)
            ElseIf type = "Retl" Then
                LogOffUpdate(UserId, sessionId, RetailConnection)
            ElseIf type = "PackProd" Then
                LogOffContractUpdate(UserId, sessionId, PackProdConnection)
            ElseIf type = "SDist" Then
                LogOffUpdate(UserId, sessionId, SDistributionConnection)
            ElseIf type = "VChain" Then
                LogOffUpdate(UserId, sessionId, VChainConnection)
            ElseIf type = "COMP" Then
                CompLogOffUpdate(UserId, sessionId, EconConnection)
            ElseIf type = "COMPS1" Then
                CompLogOffUpdate(UserId, sessionId, Sustain1Connection)
            ElseIf type = "StandAssist" Then
                LogOffUpdate(UserId, sessionId, SBAConnection)
            ElseIf type = "MED1" Then
                LogOffUpdate(UserId, sessionId, MedEcon1Connection)
            ElseIf type = "MED2" Then
                LogOffUpdate(UserId, sessionId, MedEcon2Connection)
            ElseIf type = "SMED1" Then
                LogOffUpdate(UserId, sessionId, MedSustain1Connection)
            ElseIf type = "SMED2" Then
                LogOffUpdate(UserId, sessionId, MedSustain2Connection)
            ElseIf type = "MoldE1" Then
                LogOffUpdate(UserId, sessionId, MoldE1Connection)
            ElseIf type = "MoldE2" Then
                LogOffUpdate(UserId, sessionId, MoldE2Connection)
            ElseIf type = "MoldS1" Then
                LogOffUpdate(UserId, sessionId, MoldS1Connection)
            ElseIf type = "MoldS2" Then
                LogOffUpdate(UserId, sessionId, MoldS2Connection)
            ElseIf type = "IContract" Then
                LogOffUpdate(UserId, sessionId, SavvyProConnection)
            End If
        End Sub
        Public Sub LogOffContractUpdate(ByVal UserId As String, ByVal sessionId As String, ByVal schema As String)
            'Deleting from Inuse table
            Dim odbutil As New DBUtil()
            Dim StrSql As String
            Dim ds As New DataSet()
            StrSql = "delete from inuse where sessionid='" + sessionId + "'"
            odbutil.UpIns(StrSql, schema)
            'Inserting in LogoutLog table
            StrSql = ""
            StrSql = "insert into Logoutlog(serverdate,userid,sessionid,path) "
            StrSql = StrSql + "values(sysdate," + UserId.ToString() + ",'" + sessionId + "','L')"
            odbutil.UpIns(StrSql, schema)
        End Sub
        Public Sub LogOffUpdate(ByVal userid As String, ByVal sessionId As String, ByVal schema As String)
            'Deleting from Inuse table
            Dim odbutil As New DBUtil()
            Dim StrSql As String
            Dim ds As New DataSet()
            StrSql = "delete from inuse where sessionid='" + sessionId + "'"
            odbutil.UpIns(StrSql, schema)
            'Inserting in LogoutLog table
            StrSql = ""
            StrSql = "insert into Logoutlog(serverdate,userid,sessionid,path) "
            StrSql = StrSql + "values(sysdate," + userid.ToString() + ",'" + sessionId + "','L')"
            odbutil.UpIns(StrSql, schema)

            If schema = "SBAConnection" Then
                StrSql = ""
                StrSql = "delete from FLAGQUERYTEMP where sessionid='" + sessionId + "'"
                odbutil.UpIns(StrSql, schema)
            End If
        End Sub
        Public Sub UpdatePassword(ByVal userId As String, ByVal password As String)
            Dim odbutil As New DBUtil()
            Dim StrSql As String
            StrSql = "Update USERS SET USERS.PASSWORD ='" + password + "'"
            StrSql = StrSql + " WHERE USERS.USERID=" + userId
            odbutil.UpIns(StrSql, EconConnection)
        End Sub

        Public Sub InsertUserDetails(ByVal userName As String, ByVal password As String, ByVal CorpUserName As String, ByVal CorpPassword As String)
            Dim odbutil As New DBUtil()
            Dim StrSql As String
            StrSql = "INSERT INTO USERS  "
            StrSql = StrSql + "(USERNAME,PASSWORD,USERID,COMPANY,LICENSENO,ISVALIDEMAIL) "
            StrSql = StrSql + "SELECT '" + userName.Trim() + "', '"
            StrSql = StrSql + password.Trim() + "', "
            StrSql = StrSql + "SEQUSERID.NEXTVAL, "
            StrSql = StrSql + "CORPORATEUSERS.COMPANY, "
            StrSql = StrSql + "CORPORATEUSERS.COMPANY||SEQUSERID.NEXTVAL,'Y' FROM CORPORATEUSERS WHERE Upper(CORPORATEUSERS.USERNAME)='" + CorpUserName.ToUpper() + "' "
            StrSql = StrSql + "AND Upper(CORPORATEUSERS.PASSWORD)='" + CorpPassword.ToUpper() + "' "
            odbutil.UpIns(StrSql, EconConnection)

        End Sub

        Public Sub UpdateUser(ByVal userId As String)
            Dim odbutil As New DBUtil
            Dim StrSql As String
            'Dim connStr As String
            'connStr = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            StrSql = " UPDATE USERS "
            StrSql = StrSql + "SET USERS.ISCORPORATEUSER = '' "
            StrSql = StrSql + "WHERE USERID=" + userId + ""
            odbutil.UpIns(StrSql, EconConnection)

            StrSql = " DELETE FROM USERPERMISSIONS "
            StrSql = StrSql & "WHERE USERID=" & userId & ""
            StrSql = StrSql & "AND NVL(ISCORPORATEPERMISSION,'A') = 'C' "
            odbutil.UpIns(StrSql, EconConnection)



        End Sub
        Public Sub InsertUserPermissions(ByVal userId As String, ByVal CUserId As String)
            Dim odbutil As New DBUtil
            Dim StrSql As String
            'Dim connStr As String
            'connStr = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            'Update The User table
            StrSql = " UPDATE USERS "
            StrSql = StrSql & "SET USERS.ISCORPORATEUSER = 'C' "
            StrSql = StrSql & "WHERE USERID=" & userId & ""
            odbutil.UpIns(StrSql, EconConnection)

            'Adding The Permissions
            StrSql = " INSERT INTO USERPERMISSIONS (USERID,SERVICEID,USERROLE,ISCORPORATEPERMISSION)  "
            StrSql = StrSql & "SELECT " & userId & ", "
            StrSql = StrSql & "CORPORATEUSERPERMISSIONS.SERVICEID, "
            StrSql = StrSql & "CORPORATEUSERPERMISSIONS.USERROLE, "
            StrSql = StrSql & "'C' "
            StrSql = StrSql & "FROM CORPORATEUSERPERMISSIONS  "
            StrSql = StrSql & "INNER JOIN CORPORATEUSERS A  "
            StrSql = StrSql & "ON A.CORPORATEUSERID=CORPORATEUSERPERMISSIONS.CORPORATEUSERID  "
            'StrSql = StrSql & "INNER JOIN SERVICES "
            'StrSql = StrSql & "ON  SERVICES.SERVICEID  = CORPORATEUSERPERMISSIONS.SERVICEID "
            'StrSql = StrSql & "AND SERVICES.SERVICEDE IN('ECON1','ECON2','SUSTAIN1','SUSTAIN2')"
            StrSql = StrSql & "AND A.CORPORATEUSERID =" & CUserId & " "
            StrSql = StrSql & "WHERE NOT EXISTS "
            StrSql = StrSql & "( "
            StrSql = StrSql & "SELECT 1  "
            StrSql = StrSql & "FROM USERPERMISSIONS "
            StrSql = StrSql & "WHERE USERPERMISSIONS.USERID=" & userId & " "
            StrSql = StrSql & "AND USERPERMISSIONS.SERVICEID=CORPORATEUSERPERMISSIONS.SERVICEID "
            StrSql = StrSql & ") "

            odbutil.UpIns(StrSql, EconConnection)





        End Sub

        Public Sub UpdateLogOffDetails2(ByVal Tid As String, ByVal sessionId As String, ByVal UserId As String, ByVal logCount As String)

            'If Tid = "1" Or Tid = "4" Then
            LogOffUpdate2(UserId, sessionId, EconConnection)
            'ElseIf Tid = "2" Then
            'LogOffUpdate2(UserId, sessionId, Econ2Connection)
            ''ElseIf Tid = "3" Then
            'LogOffUpdate2(UserId, sessionId, Econ3Connection)
            ''ElseIf Tid = "5" Then
            'LogOffUpdate2(UserId, sessionId, Sustain1Connection)
            ''ElseIf Tid = "6" Then
            'LogOffUpdate2(UserId, sessionId, Sustain2Connection)
            ''ElseIf Tid = "8" Then
            'LogOffUpdate2(UserId, sessionId, Schem1Connection)
            ''ElseIf Tid = "9" Then
            'LogOffUpdate2(UserId, sessionId, Sustain3Connection)
            ''ElseIf Tid = "10" Then
            'LogOffUpdate2(UserId, sessionId, Sustain4Connection)
            ''ElseIf Tid = "11" Then
            'LogOffUpdate2(UserId, sessionId, Econ4Connection)
            ''ElseIf Tid = "7" Then
            'LogOffUpdate2(UserId, sessionId, Echem1Connection)
            ''ElseIf Tid = "12" Then
            'LogOffUpdate2(UserId, sessionId, Market1Connection)
            ''ElseIf Tid = "23" Then
            'LogOffUpdate2(UserId, sessionId, SpecConnection)
            ''ElseIf Tid = "18" Then
            'LogOffUpdate2(UserId, sessionId, DistributionConnection)
            ''ElseIf Tid = "19" Then
            'LogOffUpdate2(UserId, sessionId, RetailConnection)
            ''ElseIf Tid = "20" Then
            'LogOffUpdate2(UserId, sessionId, PackProdConnection)
            ''ElseIf Tid = "21" Then
            'LogOffUpdate2(UserId, sessionId, ContractConnection)
            ''ElseIf Tid = "22" Then
            'LogOffUpdate2(UserId, sessionId, SDistributionConnection)
            ''ElseIf Tid = "24" Then
            'LogOffUpdate2(UserId, sessionId, VChainConnection)
            ''ElseIf Tid = "26" Then
            'LogOffCOMPUpdate2(UserId, sessionId, EconConnection)
            ''ElseIf Tid = "27" Then
            'LogOffCOMPUpdate2(UserId, sessionId, Sustain1Connection)
            ''ElseIf Tid = "28" Then
            'LogOffUpdate3(sessionId, SBAConnection, UserId, logCount)
            ''Medical Device
            'LogOffUpdate2(UserId, sessionId, MedEcon1Connection)
            'LogOffUpdate2(UserId, sessionId, MedEcon2Connection)
            'LogOffUpdate2(UserId, sessionId, MedSustain1Connection)
            'LogOffUpdate2(UserId, sessionId, MedSustain2Connection)

            ''Mold Module
            'LogOffUpdate2(UserId, sessionId, MoldE1Connection)
            'LogOffUpdate2(UserId, sessionId, MoldE2Connection)
            'LogOffUpdate2(UserId, sessionId, MoldS1Connection)
            'LogOffUpdate2(UserId, sessionId, MoldS2Connection)

            'End If
        End Sub
        Public Sub LogOffUpdate2(ByVal userid As String, ByVal sessionId As String, ByVal schema As String)
            'Deleting from Inuse table
            Dim odbutil As New DBUtil()
            Dim StrSql As String
            Dim ds As New DataSet()
            Try
                'Getting inuse detail
                ' StrSql = "select * from inuse  WHERE Upper(username)= '" + username.ToUpper() + "'"
                If userid = "" Then
                    userid = "0"
                End If
                StrSql = "select * from inuse  WHERE USERID= " + userid.ToString() + ""
                ds = odbutil.FillDataSet(StrSql, schema)

                If ds.Tables(0).Rows.Count > 0 Then
                    StrSql = "delete from inuse where sessionid='" + sessionId + "'"
                    odbutil.UpIns(StrSql, schema)
                    'Inserting in LogoutLog table
                    StrSql = ""
                    StrSql = "insert into Logoutlog(serverdate,userid,path) "
                    StrSql = StrSql + "values(sysdate," + userid.ToString() + ",'G')"
                    odbutil.UpIns(StrSql, schema)
                End If

            Catch ex As Exception
                Throw New Exception("LoginUpdateData:LogOffUpdate2:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub LogOffUpdate3(ByVal sessionId As String, ByVal schema As String, ByVal UserId As String, ByVal LogCount As String)
            'Deleting from Inuse table
            Dim odbutil As New DBUtil()
            Dim StrSql As String
            Dim ds As New DataSet()
            Dim objUpIns As New StandUpInsData.UpdateInsert
            Try
                'Getting inuse detail
                StrSql = "select * from inuse  WHERE USERID= " + UserId.ToString() + ""
                ' StrSql = "select * from inuse  WHERE Upper(username)= '" + username.ToUpper() + "'"
                ds = odbutil.FillDataSet(StrSql, schema)

                If ds.Tables(0).Rows.Count > 0 Then
                    StrSql = "delete from inuse where sessionid='" + sessionId + "'"
                    odbutil.UpIns(StrSql, schema)
                    'Inserting in LogoutLog table
                    StrSql = ""
                    StrSql = "insert into Logoutlog(serverdate,userid,path) "
                    StrSql = StrSql + "values(sysdate," + UserId.ToString() + ",'G')"
                    odbutil.UpIns(StrSql, schema)

                    'Activity Log
                    If LogCount <> "" Then
                        objUpIns.InsertLog1(UserId, "1", "Session Timedout from Structure Assistant Module", "", LogCount, sessionId, "", "", "", "", "")

                    End If
                End If


                StrSql = ""
                StrSql = "delete from FLAGQUERYTEMP where sessionid='" + sessionId + "'"
                odbutil.UpIns(StrSql, schema)

            Catch ex As Exception
                Throw New Exception("LoginUpdateData:LogOffUpdate3:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub InsertLicense(ByVal UserId As String, ByVal password As String, ByVal modName As String)
            'Inserting in License table
            Dim odbutil As New DBUtil()
            Dim StrSql As String
            Dim License1 As String
            License1 = "License1"
            If modName <> "CONTR" And modName <> "PACKPROD" And modName <> "REPORT" And modName <> "Market1" Then
                StrSql = "INSERT INTO license (userid, license, serverdate) "
                StrSql = StrSql + "values(" & UserId.ToString() & ",'" & License1 & "',TO_DATE('" & Now & "','mm/dd/yyyy hh:mi:ss pm'))"
                odbutil.UpIns(StrSql, EconConnection)
            ElseIf modName = "CONTR" Then
                StrSql = "INSERT INTO license (userid,license, serverdate) "
                StrSql = StrSql + "values(" & UserId.ToString() & ",'" & License1 & "',TO_DATE('" & Now & "','mm/dd/yyyy hh:mi:ss pm'))"
                odbutil.UpIns(StrSql, ContractConnection)
            ElseIf modName = "PACKPROD" Then
                StrSql = "INSERT INTO license (userid, license, serverdate) "
                StrSql = StrSql + "values(" & UserId.ToString() & ",'" & License1 & "',TO_DATE('" & Now & "','mm/dd/yyyy hh:mi:ss pm'))"
                odbutil.UpIns(StrSql, PackProdConnection)
            ElseIf modName = "REPORT" Then
                StrSql = "INSERT INTO license (userid, license, serverdate) "
                StrSql = StrSql + "values(" & UserId.ToString() & ",'" & License1 & "',TO_DATE('" & Now & "','mm/dd/yyyy hh:mi:ss pm'))"
                odbutil.UpIns(StrSql, ReportConnection)
            ElseIf modName = "Market1" Then
                StrSql = "INSERT INTO license (userid, license, serverdate) "
                StrSql = StrSql + "values(" & UserId.ToString() & ",'" & License1 & "',TO_DATE('" & Now & "','mm/dd/yyyy hh:mi:ss pm'))"
                odbutil.UpIns(StrSql, Market1Connection)
            End If

        End Sub


#Region "Comp Module"
        Public Sub InuseCompLoginInsert(ByVal sessionID As String, ByVal UserId As String, ByVal licenseID As String, ByVal schema As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim ConnectionString As String = System.Configuration.ConfigurationManager.AppSettings(schema)
            Try
                'Insert into Inuse table
                StrSql = "INSERT INTO INUSECOMP (sessionID,userid,licenseID) "
                StrSql = StrSql + "VALUES('" + sessionID + "'," + UserId.ToString() + ",'" + licenseID + "')"
                odbUtil.UpIns(StrSql, ConnectionString)

                'Insert into loginlog table
                StrSql = "INSERT INTO LOGINLOGCOMP (serverdate,userid,sessionID) "
                StrSql = StrSql + "VALUES(sysdate," + UserId.ToString() + ",'" + sessionID + "')"
                odbUtil.UpIns(StrSql, ConnectionString)

            Catch ex As Exception
                Throw New Exception("LoginUpdateData:InuseCompLoginInsert:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub CompLogOffUpdate(ByVal UserId As String, ByVal sessionId As String, ByVal schema As String)
            'Deleting from Inuse table
            Dim odbutil As New DBUtil()
            Dim StrSql As String
            Dim ds As New DataSet()
            StrSql = "DELETE FROM INUSECOMP WHERE SESSIONID='" + sessionId + "'"
            odbutil.UpIns(StrSql, schema)
            'Inserting in LogoutLog table
            StrSql = ""
            StrSql = "INSERT INTO LOGOUTLOGCOMP(serverdate,userid,sessionid,path) "
            StrSql = StrSql + "VALUES(sysdate," + UserId.ToString() + ",'" + sessionId + "','L')"
            odbutil.UpIns(StrSql, schema)
        End Sub
        Public Sub DeleteCOMPInuseDetails(ByVal UserId As String, ByVal strCon As String, ByVal schema As String, ByVal flag As String)
            Dim odbutil As New DBUtil()
            Dim StrSql As String
            Dim objGetData As New LoginGetData.Selectdata()
            Dim ds As New DataSet()
            Try

                ds = objGetData.GetInUseCOMPTableInfo(UserId.ToString(), schema)
                If ds.Tables(0).Rows.Count > 0 Then

                    'StrSql = "delete from inusecomp where Upper(username)='" + username.ToUpper() + "'"
                    StrSql = "delete from inusecomp where userid=" + UserId.ToString() + " "
                    odbutil.UpIns(StrSql, strCon)

                    'Insert into Logoutlog table
                    StrSql = "insert into Logoutlogcomp(serverdate,userid,path) "
                    StrSql = StrSql + "values(sysdate," + UserId.ToString() + ",'LR')"
                    odbutil.UpIns(StrSql, strCon)


                End If


            Catch ex As Exception
                Throw New Exception("LoginUpdateData:DeleteCOMPInuseDetails:" + ex.Message.ToString())
            End Try


        End Sub
        Public Sub LogOffCOMPUpdate2(ByVal UserId As String, ByVal sessionId As String, ByVal schema As String)
            'Deleting from Inuse table
            Dim odbutil As New DBUtil()
            Dim StrSql As String
            Dim ds As New DataSet()
            Try
                'Getting inuse detail
                'StrSql = "select * from inusecomp  WHERE Upper(username)= '" + username.ToUpper() + "'"
                StrSql = "select * from inusecomp  WHERE userid= " + UserId.ToString() + " "
                ds = odbutil.FillDataSet(StrSql, schema)

                If ds.Tables(0).Rows.Count > 0 Then
                    StrSql = "delete from inusecomp where sessionid='" + sessionId + "'"
                    odbutil.UpIns(StrSql, schema)
                    'Inserting in LogoutLog table
                    StrSql = ""
                    StrSql = "insert into Logoutlogcomp(serverdate,userid,path) "
                    StrSql = StrSql + "values(sysdate," + UserId.ToString() + ",'G')"
                    odbutil.UpIns(StrSql, schema)
                End If
            Catch ex As Exception
                Throw New Exception("LoginUpdateData:LogOffCOMPUpdate2:" + ex.Message.ToString())
            End Try

        End Sub
#End Region

#Region "Inuse"
        Public Sub DelInUseEntry(ByVal UserId As String, ByVal type As String)
            Dim odbutil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ConnStr As String = String.Empty
            Try
                If type = "E1" Then
                    ConnStr = EconConnection
                ElseIf type = "E2" Then
                    ConnStr = Econ2Connection
                ElseIf type = "E3" Then
                    ConnStr = Econ3Connection
                ElseIf type = "E4" Then
                    ConnStr = Econ4Connection
                ElseIf type = "S1" Then
                    ConnStr = Sustain1Connection
                ElseIf type = "S2" Then
                    ConnStr = Sustain2Connection
                ElseIf type = "S3" Then
                    ConnStr = Sustain3Connection
                ElseIf type = "S4" Then
                    ConnStr = Sustain4Connection
                ElseIf type = "SC1" Then
                    ConnStr = Schem1Connection
                ElseIf type = "EC1" Then
                    ConnStr = Echem1Connection
                ElseIf type = "M1" Then
                    ConnStr = Market1Connection
                ElseIf type = "SPEC" Then
                    ConnStr = SpecConnection
                ElseIf type = "CONTR" Then
                    ConnStr = ContractConnection
                ElseIf type = "PACKPROD" Then
                    ConnStr = PackProdConnection
                ElseIf type = "DIST" Then
                    ConnStr = DistributionConnection
                ElseIf type = "RETL" Then
                    ConnStr = RetailConnection
                ElseIf type = "SDIST" Then
                    ConnStr = SDistributionConnection
                ElseIf type = "VChain" Then
                    ConnStr = VChainConnection
                ElseIf type = "COMP" Then
                    ConnStr = EconConnection
                ElseIf type = "COMPS1" Then
                    ConnStr = Sustain1Connection
                ElseIf type = "StandAssist" Then
                    ConnStr = SBAConnection
                ElseIf type = "MEDECON1" Then
                    ConnStr = MedEcon1Connection
                ElseIf type = "MEDECON2" Then
                    ConnStr = MedEcon2Connection
                ElseIf type = "MEDSUSTAIN1" Then
                    ConnStr = MedSustain1Connection
                ElseIf type = "MEDSUSTAIN2" Then
                    ConnStr = MedSustain2Connection
                ElseIf type = "MOLDE1" Then
                    ConnStr = MoldE1Connection
                ElseIf type = "MOLDE2" Then
                    ConnStr = MoldE2Connection
                ElseIf type = "MOLDS1" Then
                    ConnStr = MoldS1Connection
                ElseIf type = "MOLDS2" Then
                    ConnStr = MoldS2Connection
                ElseIf type = "EMON" Then
                ElseIf type = "IContract" Then
                    ConnStr = SavvyProConnection
                End If

                If ConnStr <> "" Then


                    If type = "COMP" Or type = "COMPS1" Then
                        Dim Dts As New DataSet
                        StrSql = "select * from INUSECOMP  WHERE userid= " + UserId.ToString() + " "
                        Dts = odbutil.FillDataSet(StrSql, ConnStr)
                        StrSql = ""
                        If Dts.Tables(0).Rows.Count > 0 Then
                            StrSql = "DELETE FROM INUSECOMP "
                            StrSql = StrSql + "WHERE USERID=" + UserId
                            odbutil.UpIns(StrSql, ConnStr)

                            'Insert into Logoutlog table
                            StrSql = "insert into Logoutlog(serverdate,userid,path) "
                            StrSql = StrSql + "values(sysdate," + UserId.ToString() + ",'LR')"
                            odbutil.UpIns(StrSql, ConnStr)
                        End If

                    Else
                        Dim Dts As New DataSet
                        StrSql = "select * from inuse  WHERE userid= " + UserId.ToString() + " "
                        Dts = odbutil.FillDataSet(StrSql, ConnStr)
                        StrSql = ""
                        If Dts.Tables(0).Rows.Count > 0 Then
                            StrSql = "DELETE FROM INUSE "
                            StrSql = StrSql + "WHERE USERID=" + UserId
                            odbutil.UpIns(StrSql, ConnStr)

                            'Insert into Logoutlog table
                            StrSql = "insert into Logoutlog(serverdate,userid,path) "
                            StrSql = StrSql + "values(sysdate," + UserId.ToString() + ",'LR')"
                            odbutil.UpIns(StrSql, ConnStr)
                        End If

                    End If

                End If

            Catch ex As Exception
            End Try
        End Sub

        Public Sub DelInUseEntry_OLD(ByVal UserId As String, ByVal type As String)
            Dim odbutil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ConnStr As String = String.Empty
            Try
                If type = "E1" Then
                    ConnStr = EconConnection
                ElseIf type = "E2" Then
                    ConnStr = Econ2Connection
                ElseIf type = "E3" Then
                    ConnStr = Econ3Connection
                ElseIf type = "E4" Then
                    ConnStr = Econ4Connection
                ElseIf type = "S1" Then
                    ConnStr = Sustain1Connection
                ElseIf type = "S2" Then
                    ConnStr = Sustain2Connection
                ElseIf type = "S3" Then
                    ConnStr = Sustain3Connection
                ElseIf type = "S4" Then
                    ConnStr = Sustain4Connection
                ElseIf type = "SC1" Then
                    ConnStr = Schem1Connection
                ElseIf type = "EC1" Then
                    ConnStr = Echem1Connection
                ElseIf type = "M1" Then
                    ConnStr = Market1Connection
                ElseIf type = "SPEC" Then
                    ConnStr = SpecConnection
                ElseIf type = "CONTR" Then
                    ConnStr = ContractConnection
                ElseIf type = "PACKPROD" Then
                    ConnStr = PackProdConnection
                ElseIf type = "DIST" Then
                    ConnStr = DistributionConnection
                ElseIf type = "RETL" Then
                    ConnStr = RetailConnection
                ElseIf type = "SDIST" Then
                    ConnStr = SDistributionConnection
                ElseIf type = "VChain" Then
                    ConnStr = VChainConnection
                ElseIf type = "COMP" Then
                    ConnStr = EconConnection
                ElseIf type = "COMPS1" Then
                    ConnStr = Sustain1Connection
                ElseIf type = "StandAssist" Then
                    ConnStr = SBAConnection
                ElseIf type = "MEDECON1" Then
                    ConnStr = MedEcon1Connection
                ElseIf type = "MEDECON2" Then
                    ConnStr = MedEcon2Connection
                ElseIf type = "MEDSUSTAIN1" Then
                    ConnStr = MedSustain1Connection
                ElseIf type = "MEDSUSTAIN2" Then
                    ConnStr = MedSustain2Connection
                ElseIf type = "MOLDE1" Then
                    ConnStr = MoldE1Connection
                ElseIf type = "MOLDE2" Then
                    ConnStr = MoldE2Connection
                ElseIf type = "MOLDS1" Then
                    ConnStr = MoldS1Connection
                ElseIf type = "MOLDS2" Then
                    ConnStr = MoldS2Connection
                ElseIf type = "EMON" Then

                End If

                If ConnStr <> "" Then
                    If type = "COMP" Or type = "COMPS1" Then
                        StrSql = "DELETE FROM INUSECOMP "
                        StrSql = StrSql + "WHERE USERID=" + UserId
                        odbutil.UpIns(StrSql, ConnStr)

                        'Insert into Logoutlog table
                        StrSql = "insert into Logoutlog(serverdate,userid,path) "
                        StrSql = StrSql + "values(sysdate," + UserId.ToString() + ",'LR')"
                        odbutil.UpIns(StrSql, ConnStr)
                    Else
                        StrSql = "DELETE FROM INUSE "
                        StrSql = StrSql + "WHERE USERID=" + UserId
                        odbutil.UpIns(StrSql, ConnStr)

                        'Insert into Logoutlog table
                        StrSql = "insert into Logoutlog(serverdate,userid,path) "
                        StrSql = StrSql + "values(sysdate," + UserId.ToString() + ",'LR')"
                        odbutil.UpIns(StrSql, ConnStr)
                    End If

                End If

            Catch ex As Exception
            End Try
        End Sub

#End Region
    End Class

End Class
