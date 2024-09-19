Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System

Public Class VChainGetData
    Public Class Selectdata
        Dim VChainConnection As String = System.Configuration.ConfigurationManager.AppSettings("VChainConnectionString")
        Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
        Dim Econ2Connection As String = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
#Region "Error"
        Public Function GetErrors(ByVal ErrorCode As String) As DataSet
            Dim Dts As New DataSet()
            Try
                Dim odbUtil As New DBUtil()
                Dim StrSql As String = ""
                StrSql = "SELECT ERRORID,  "
                StrSql = StrSql + "ERRORCODE, "
                StrSql = StrSql + "ERRORDE1, "
                StrSql = StrSql + "ERRORDE2, "
                StrSql = StrSql + "ERRORTYPE, "
                StrSql = StrSql + "SHORTERROR "
                StrSql = StrSql + "FROM ERROR "
                StrSql = StrSql + "WHERE ERRORCODE='" + ErrorCode.ToString() + "' "
                Dts = odbUtil.FillDataSet(StrSql, VChainConnection)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
#End Region
#Region "Supporting Pages"
        Public Function GetUserDetails(ByVal Id As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERS.USERID,  "
                StrSql = StrSql + "UPPER(USERNAME)USERNAME, "
                StrSql = StrSql + "USERNAME AS TOOLUSERNAME, "
                StrSql = StrSql + "SERVICES.SERVICEDE, "
                StrSql = StrSql + "USERPERMISSIONS.USERROLE AS SERVIECROLE, "
                StrSql = StrSql + "USERPERMISSIONS.MAXCASECOUNT, "

                StrSql = StrSql + "NVL(USERS.ISIADMINLICUSR,'N')ISIADMINLICUSR,"
                StrSql = StrSql + "USERS.LICENSEID,"

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
                StrSql = StrSql + "AND SERVICES.SERVICEDE='ECON2' "

                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E2GetData:GetUserDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetValueChain(ByVal VChaintName As String, ByVal UserId As String) As DataTable
            Dim Dts As New DataTable()
            Dim strsql As String = String.Empty
            Try
                Dim odbUtil As New DBUtil()
                strsql = "SELECT VALUECHAINID, VALUECHAINNAME, MODNAME, RESULTCASES, VALUECHAINID ||'.'||VALUECHAINNAME AS VALUECHAINNAME1  FROM  "
                strsql = strsql + "USRVALUECHAIN "
                If VChaintName <> "" Then
                    strsql = strsql + "WHERE UPPER(VALUECHAINNAME)='" + VChaintName.ToUpper() + "' "
                    strsql = strsql + " AND USERID=" + UserId.ToString() + " "
                Else
                    strsql = strsql + "WHERE USERID=" + UserId.ToString() + " "
                End If
                strsql = strsql + "ORDER BY UPPER(VALUECHAINNAME)"
                Dts = odbUtil.FillDataTable(strsql, VChainConnection)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
        Public Function GetPCaseDetails(ByVal UserName As String, ByVal Schema As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As New StringBuilder
            Dim con As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
            Try
                StrSql.Append("SELECT DISTINCT CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES FROM PERMISSIONSCASES ")
                StrSql.Append("WHERE UPPER(USERNAME) ='" + UserName.ToUpper().ToString() + "' ")
                StrSql.Append("ORDER BY CASEDE1 ")
                Dts = odbUtil.FillDataSet(StrSql.ToString(), con)
                Return Dts
            Catch ex As Exception
                Throw New Exception("VChain:GetPCaseDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetVChainCasesTest(ByVal ChildMod As String, ByVal ParMod As String, ByVal Schema As String, ByVal vChainId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As New StringBuilder
            Dim con As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
            Dim modName As String = String.Empty
            Try
                If ChildMod.ToUpper() = "ECON1" Then
                    modName = "ECON"
                ElseIf ChildMod.ToUpper() = "ECON2" Then
                    modName = "ECON2"
                End If
                StrSql.Append("SELECT DISTINCT CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES FROM " + modName + ".PERMISSIONSCASES ")
                StrSql.Append("WHERE CASEID IN ")
                StrSql.Append("(")
                StrSql.Append("SELECT RESULTCASES CASEID ")
                StrSql.Append("FROM ")
                StrSql.Append("USRVALUECHAIN ")
                StrSql.Append("WHERE ")
                StrSql.Append("VALUECHAINID= " + vChainId + " ")
                StrSql.Append("AND UPPER(MODNAME)='" + ChildMod.ToUpper() + "' ")
                StrSql.Append("UNION ")
                StrSql.Append("SELECT PARENTCASEID CASES FROM  ECON.PARCHILDCASES ")
                StrSql.Append("WHERE UPPER(PARENTMOD)='" + ChildMod.ToUpper() + "' ")
                StrSql.Append("AND UPPER(CHILDMOD)='" + ParMod.ToUpper() + "' ")
                StrSql.Append("AND CHILDCASEID IN ")
                StrSql.Append("( ")
                StrSql.Append("SELECT RESULTCASES CASES ")
                StrSql.Append("FROM ")
                StrSql.Append("USRVALUECHAIN ")
                StrSql.Append("WHERE ")
                StrSql.Append("VALUECHAINID=" + vChainId + " ")
                StrSql.Append("AND UPPER(MODNAME)='" + ParMod.ToUpper() + "' ")
                StrSql.Append(") ")
                StrSql.Append(")")
                StrSql.Append("ORDER BY CASEDE1 ")
                Dts = odbUtil.FillDataSet(StrSql.ToString(), VChainConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("VChain:GetVChainCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetVChainMCaseID(ByVal ChildMod As String, ByVal ParMod As String, ByVal vChainId As String, ByVal resCaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As New StringBuilder
            Try
                If ChildMod.ToUpper() = "ECHEM1" And ParMod.ToUpper() = "ECON2" Then
                    StrSql.Append("SELECT RESULTCASES CASEID ")
                    StrSql.Append("FROM ")
                    StrSql.Append("USRVALUECHAIN ")
                    StrSql.Append("WHERE ")
                    StrSql.Append("VALUECHAINID=" + vChainId.ToString() + " ")
                    StrSql.Append("AND UPPER(MODNAME)='" + ChildMod.ToUpper() + "' ")
                    StrSql.Append("UNION ")
                    StrSql.Append("SELECT PARENTCASEID CASES FROM  ECON.PARCHILDCASES ")
                    StrSql.Append("WHERE UPPER(PARENTMOD)='ECON1' ")
                    StrSql.Append("AND UPPER(CHILDMOD)='" + ParMod.ToUpper() + "' ")
                    StrSql.Append("AND CHILDCASEID = " + resCaseId.ToString() + " ")
                Else
                    StrSql.Append("SELECT RESULTCASES CASEID ")
                    StrSql.Append("FROM ")
                    StrSql.Append("USRVALUECHAIN ")
                    StrSql.Append("WHERE ")
                    StrSql.Append("VALUECHAINID=" + vChainId.ToString() + " ")
                    StrSql.Append("AND UPPER(MODNAME)='" + ChildMod.ToUpper() + "' ")
                    StrSql.Append("UNION ")
                    StrSql.Append("SELECT PARENTCASEID CASES FROM  ECON.PARCHILDCASES ")
                    StrSql.Append("WHERE UPPER(PARENTMOD)='" + ChildMod.ToUpper() + "' ")
                    StrSql.Append("AND UPPER(CHILDMOD)='" + ParMod.ToUpper() + "' ")
                    StrSql.Append("AND CHILDCASEID = " + resCaseId.ToString() + " ")
                End If



                Dts = odbUtil.FillDataSet(StrSql.ToString(), VChainConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("VChain:GetVChainMCaseID:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetVChainEchemCaseID(ByVal ChildMod As String, ByVal ParMod As String, ByVal CaseIds As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As New StringBuilder
            Try

                StrSql.Append("SELECT PARENTCASEID CASES FROM  ECON.PARCHILDCASES ")
                StrSql.Append("WHERE UPPER(PARENTMOD)='" + ChildMod.ToUpper() + "' ")
                StrSql.Append("AND UPPER(CHILDMOD)='ECON1' ")
                StrSql.Append("AND CHILDCASEID IN ( " + CaseIds.ToString() + ") ")
                Dts = odbUtil.FillDataSet(StrSql.ToString(), VChainConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("VChain:GetVChainEchemCaseID:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetVChainDCaseID(ByVal modName As String, ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As New StringBuilder
            Try
                StrSql.Append("SELECT PARENTCASEID CASES  FROM ECON.PARCHILDCASES ")
                StrSql.Append("WHERE UPPER(PARENTMOD)='" + modName.ToUpper() + "' ")
                StrSql.Append("AND UPPER(CHILDMOD)='" + modName.ToUpper() + "' ")
                StrSql.Append("AND CHILDCASEID=" + CaseId.ToString())
                Dts = odbUtil.FillDataSet(StrSql.ToString(), VChainConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("VChain:GetVChainDCaseID:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetVChainModCaseID(ByVal ParModName As String, ByVal modName As String, ByVal CaseId As String, ByVal VChainID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As New StringBuilder
            Dim tblName As String = String.Empty
            Dim modType As Integer
            Try
                If ParModName.ToUpper() = "ECON2" Then
                    tblName = "MATCASE_ECON2"
                ElseIf ParModName.ToUpper() = "ECON1" Then
                    tblName = "MATCASE_ECON1"
                ElseIf ParModName.ToUpper() = "ECHEM1" Then
                    tblName = "MATCASE_ECHEM1"
                End If
                If modName.ToUpper() = "ECON2" Then
                    modType = 3
                ElseIf modName.ToUpper() = "ECON1" Then
                    modType = 2
                ElseIf modName.ToUpper() = "ECHEM1" Then
                    modType = 1
                End If
                StrSql.Append("SELECT CASEID, MODTYPE, MODCASEID, LAYER FROM " + tblName + " ")
                StrSql.Append("WHERE MODTYPE=" + modType.ToString() + " ")
                StrSql.Append("AND CaseId=" + CaseId.ToString() + " AND VALUECHAINID=" + VChainID + " ")
                Dts = odbUtil.FillDataSet(StrSql.ToString(), VChainConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("VChain:GetVChainModCaseID:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetVChainCases(ByVal CaseIds As String, ByVal Schema As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As New StringBuilder
            Dim con As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
            Dim dsM As New DataSet
            Dim modName As String = String.Empty
            Dim i As Integer = 0
            Try
                StrSql.Append("SELECT DISTINCT CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES FROM PERMISSIONSCASES ")
                StrSql.Append("WHERE CASEID IN ")
                StrSql.Append("(")
                StrSql.Append(CaseIds)
                StrSql.Append(")")
                StrSql.Append("ORDER BY CASEDE1 ")
                Dts = odbUtil.FillDataSet(StrSql.ToString(), con)
                Return Dts
            Catch ex As Exception
                Throw New Exception("VChain:GetVChainCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function CheckCases(ByVal CaseId As String, ByVal GCaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As New StringBuilder
            Try
                StrSql.Append("select count(*) count ")
                StrSql.Append("from dual ")
                StrSql.Append("where " + CaseId + " in(" + GCaseId + ") ")

                Dts = odbUtil.FillDataSet(StrSql.ToString(), VChainConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("VChain:GetVChainCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetMaterialDetails1(ByVal CaseId As Integer, ByVal Schema As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim con As String = System.Configuration.ConfigurationManager.AppSettings(Schema)

            Try

                StrSql = "SELECT  MAT.CASEID,  "
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
                If Schema = "EconConnectionString" Then
                    StrSql = StrSql + "MAT.MT1, "
                    StrSql = StrSql + "MAT.MT2, "
                    StrSql = StrSql + "MAT.MT3, "
                    StrSql = StrSql + "MAT.MT4, "
                    StrSql = StrSql + "MAT.MT5, "
                    StrSql = StrSql + "MAT.MT6, "
                    StrSql = StrSql + "MAT.MT7, "
                    StrSql = StrSql + "MAT.MT8, "
                    StrSql = StrSql + "MAT.MT9, "
                    StrSql = StrSql + "MAT.MT10, "
                End If
                StrSql = StrSql + "MAT.C1, "
                StrSql = StrSql + "MAT.C2, "
                StrSql = StrSql + "MAT.C3, "
                StrSql = StrSql + "MAT.C4, "
                StrSql = StrSql + "MAT.C5, "
                StrSql = StrSql + "MAT.C6, "
                StrSql = StrSql + "MAT.C7, "
                StrSql = StrSql + "MAT.C8, "
                StrSql = StrSql + "MAT.C9, "
                StrSql = StrSql + "MAT.C10 "

                StrSql = StrSql + "FROM MATERIALINPUT MAT "
                StrSql = StrSql + "WHERE MAT.CASEID =" + CaseId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, con)
                Return Dts
            Catch ex As Exception
                Throw New Exception("VChainGetData:GetMaterialDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetMaterialDetails(ByVal VChainId As String, ByVal CaseId As Integer, ByVal Schema As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim con As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
            Dim tbl As String = String.Empty
            Try
                If Schema.ToUpper() = "ECON2" Then
                    tbl = "MATCASE_ECON2"
                    Schema = "ECON2"
                ElseIf Schema.ToUpper() = "ECON1" Then
                    tbl = "MATCASE_ECON1"
                    Schema = "ECON"
                End If


                StrSql = "SELECT A2.LAYER,  "
                StrSql = StrSql + "A2.MATID, "
                StrSql = StrSql + "A2.Caseid, "
                StrSql = StrSql + "NVL(A1.Modtype,0) Modtype, "
                StrSql = StrSql + "NVL(A1.Modcaseid,0)Modcaseid, "
                StrSql = StrSql + " NVL(PRICE,0)PRICE "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + tbl + " A1 "
                StrSql = StrSql + "RIGHT OUTER JOIN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 LAYER, M1 MATID,CASEID FROM " + Schema + ".materialinput "
                StrSql = StrSql + "WHERE caseid=" + CaseId.ToString() + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 2 LAYER, M2 MATID,CASEID FROM " + Schema + ".materialinput "
                StrSql = StrSql + "WHERE caseid=" + CaseId.ToString() + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 3 LAYER, M3 MATID,CASEID FROM " + Schema + ".materialinput "
                StrSql = StrSql + "WHERE caseid=" + CaseId.ToString() + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 4 LAYER, M4 MATID,CASEID FROM " + Schema + ".materialinput "
                StrSql = StrSql + "WHERE caseid=" + CaseId.ToString() + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 5 LAYER, M5 MATID,CASEID FROM " + Schema + ".materialinput "
                StrSql = StrSql + "WHERE caseid=" + CaseId.ToString() + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 6 LAYER, M6 MATID,CASEID FROM " + Schema + ".materialinput "
                StrSql = StrSql + "WHERE caseid=" + CaseId.ToString() + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 7 LAYER, M7 MATID,CASEID FROM " + Schema + ".materialinput "
                StrSql = StrSql + "WHERE caseid=" + CaseId.ToString() + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 8 LAYER, M8 MATID,CASEID FROM " + Schema + ".materialinput "
                StrSql = StrSql + "WHERE caseid=" + CaseId.ToString() + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 9 LAYER, M9 MATID,CASEID FROM " + Schema + ".materialinput "
                StrSql = StrSql + "WHERE caseid=" + CaseId.ToString() + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 10 LAYER, M10 MATID,CASEID FROM " + Schema + ".materialinput "
                StrSql = StrSql + "WHERE caseid=" + CaseId.ToString() + " "
                StrSql = StrSql + ") A2 "
                StrSql = StrSql + "ON A1.LAYER=A2.LAYER AND "
                StrSql = StrSql + "A1.CASEID=A2.CASEID AND A1.CASEID=" + CaseId.ToString() + " AND VALUECHAINID=" + VChainId.ToString()
                StrSql = StrSql + "ORDER BY LAYER ASC "

                Dts = odbUtil.FillDataSet(StrSql, VChainConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("VChainGetData:GetMaterialDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetModuleType(ByVal typeId As Integer, ByVal typeDes As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As New StringBuilder
            Try
                StrSql.Append("SELECT TYPEID,DES FROM ")
                StrSql.Append("( ")
                StrSql.Append("SELECT 0 TYPEID,'Nothing' DES FROM DUAL ")
                StrSql.Append("UNION ")
                'StrSql.Append("SELECT 1 TYPEID,'Echem1' DES FROM DUAL ")
                'StrSql.Append("UNION ")
                StrSql.Append("SELECT 2 TYPEID,'Econ1' DES FROM DUAL ")
                StrSql.Append(") ")
                StrSql.Append("WHERE TYPEID = CASE WHEN  " + typeId.ToString() + "  = -1 THEN ")
                StrSql.Append("TYPEID ")
                StrSql.Append("ELSE ")
                StrSql.Append(typeId.ToString())
                StrSql.Append("END ")
                StrSql.Append("AND NVL(UPPER(DES),'#') LIKE '" + typeDes.ToUpper() + "%' ")
                StrSql.Append("ORDER BY  TYPEID ")
                Dts = odbUtil.FillDataSet(StrSql.ToString(), EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("VChainGetData:GetModuleType:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetMaterials(ByVal MatId As Integer, ByVal MatDe1 As String, ByVal MatDe2 As String, ByVal Schema As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim con As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
            Try
                StrSql = "SELECT MATID, (MATDE1||'  '||MATDE2)MATDES,MATDE1,MATDE2  "
                StrSql = StrSql + "FROM MATERIALS "
                StrSql = StrSql + "WHERE MATID = CASE WHEN " + MatId.ToString() + " = -1 THEN "
                StrSql = StrSql + "MATID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + MatId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(MATDE1),'#') LIKE '" + MatDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(MATDE2),'#') LIKE '" + MatDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  MATDE1"
                Dts = odbUtil.FillDataSet(StrSql, con)
                Return Dts
            Catch ex As Exception
                Throw New Exception("VChaingetData:GetMaterials:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCases(ByVal CaseIdE1 As Integer, ByVal CaseIdE2 As Integer, ByVal CaseDe1 As String, ByVal CaseDe2 As String, ByVal userName As String, ByVal Schema As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim con As String = String.Empty
            ' Dim con As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
            If Schema.ToUpper = "ECON1" Then
                con = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            ElseIf Schema.ToUpper = "ECON2" Then
                con = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
            End If
            Try
                If CaseIdE2 >= 1000 Then
                    If CaseIdE1 = -1 And CaseDe1 <> "Nothing" And CaseDe1 <> "Selected" Then
                        StrSql = "SELECT CaseId, (Casede1||'  '||Casede2)CASDES,Casede1,Casede2,replace((Casede1 || ' ' ||  Casede2),'''' ,'##') CASDES1  "
                        StrSql = StrSql + "FROM PermissionsCases "
                        StrSql = StrSql + "WHERE Upper(Username)='" + userName.ToUpper() + "' AND NVL(UPPER(Casede1),'#') LIKE '" + CaseDe1.ToUpper() + "%' "
                        StrSql = StrSql + "AND NVL(UPPER(Casede2),'#') LIKE '" + CaseDe2.ToUpper() + "%' "
                        StrSql = StrSql + "AND CASEID<>" + CaseIdE2.ToString() + " "
                        StrSql = StrSql + "Union  "
                        StrSql = StrSql + "SELECT 0 ,'Nothing Selected' AS CASDES,'Nothing' Casede1,'Selected' Casede2,'Nothing Selected' AS CASDES1 FROM DUAL  "
                    ElseIf CaseIdE1 = -1 And CaseDe1 = "Nothing" Or CaseDe1 = "Selected" Then
                        StrSql = StrSql + "SELECT 0 ,'Nothing Selected' AS CASDES,'Nothing' Casede1,'Selected' Casede2,'Nothing Selected' AS CASDES1 FROM DUAL  "
                    ElseIf CaseIdE1 = 0 Then
                        StrSql = StrSql + "SELECT 0 ,'Nothing Selected' AS CASDES,'Nothing' Casede1,'Selected' Casede2,'Nothing Selected' AS CASDES1 FROM DUAL  "
                    Else
                        StrSql = "SELECT CaseId, (Casede1||'  '||Casede2)CASDES,Casede1,Casede2,replace((Casede1 || ' ' ||  Casede2),'''' ,'##') CASDES1  "
                        StrSql = StrSql + "FROM PermissionsCases "
                        StrSql = StrSql + "WHERE Upper(Username)='" + userName.ToUpper() + "' AND CaseId = CASE WHEN " + CaseIdE1.ToString() + " = -1 THEN "
                        StrSql = StrSql + "CaseId "
                        StrSql = StrSql + "ELSE "
                        StrSql = StrSql + "" + CaseIdE1.ToString() + " "
                        StrSql = StrSql + "END "
                        StrSql = StrSql + "AND NVL(UPPER(Casede1),'#') LIKE '" + CaseDe1.ToUpper() + "%' "
                        StrSql = StrSql + "AND NVL(UPPER(Casede2),'#') LIKE '" + CaseDe2.ToUpper() + "%' "
                        StrSql = StrSql + "AND CASEID<>" + CaseIdE2.ToString() + " "
                    End If
                    StrSql = StrSql + "Order By  CASDES"
                    Dts = odbUtil.FillDataSet(StrSql, con)
                    Return Dts
                Else
                    If CaseIdE1 = -1 Then
                        StrSql = "SELECT CaseId, (Casede1||'  '||Casede2)CASDES,Casede1,Casede2,replace((Casede1 || ' ' ||  Casede2),'''' ,'##') CASDES1  "
                        StrSql = StrSql + "FROM BaseCases "
                        StrSql = StrSql + "WHERE NVL(UPPER(Casede1),'#') LIKE '" + CaseDe1.ToUpper() + "%' "
                        StrSql = StrSql + "AND NVL(UPPER(Casede2),'#') LIKE '" + CaseDe2.ToUpper() + "%' "
                        StrSql = StrSql + "AND CASEID<>" + CaseIdE2.ToString() + " "
                        StrSql = StrSql + "Union  "
                        StrSql = StrSql + "SELECT 0 ,'Nothing Selected' AS CASDES,'Nothing' Casede1,'Selected' Casede2,'Nothing Selected' AS CASDES1 FROM DUAL "
                    ElseIf CaseIdE1 = 0 Then
                        StrSql = StrSql + "SELECT 0 ,'Nothing Selected' AS CASDES,'Nothing' Casede1,'Selected' Casede2,'Nothing Selected' AS CASDES1 FROM DUAL "
                    Else
                        StrSql = "SELECT CaseId, (Casede1||'  '||Casede2)CASDES,Casede1,Casede2,replace((Casede1 || ' ' ||  Casede2),'''' ,'##') CASDES1  "
                        StrSql = StrSql + "FROM BaseCases "
                        StrSql = StrSql + "WHERE CaseId = CASE WHEN " + CaseIdE1.ToString() + " = -1 THEN "
                        StrSql = StrSql + "CaseId "
                        StrSql = StrSql + "ELSE "
                        StrSql = StrSql + "" + CaseIdE1.ToString() + " "
                        StrSql = StrSql + "END "
                        StrSql = StrSql + "AND NVL(UPPER(Casede1),'#') LIKE '" + CaseDe1.ToUpper() + "%' "
                        StrSql = StrSql + "AND NVL(UPPER(Casede2),'#') LIKE '" + CaseDe2.ToUpper() + "%' "
                        StrSql = StrSql + "AND CASEID<>" + CaseIdE2.ToString() + " "
                    End If

                    StrSql = StrSql + "Order By  CASDES"
                    Dts = odbUtil.FillDataSet(StrSql, con)
                    Return Dts
                End If

            Catch ex As Exception
                Throw New Exception("VChainGetData:GetCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetPages(ByVal PageName As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As New StringBuilder
            Try
                If PageName = "Econ2" Then
                    StrSql.Append("SELECT PAGE,VALUE FROM ")
                    StrSql.Append("( ")
                    StrSql.Append("SELECT 0 ID,'Econ2' PAGE,'Econ2ConnectionString' VALUE FROM DUAL ")
                    StrSql.Append("UNION ")
                    StrSql.Append("SELECT 1 ID,'Econ1' PAGE,'EconConnectionString' VALUE FROM DUAL ")
                    StrSql.Append("UNION ")
                    StrSql.Append("SELECT 2 ID,'Echem1' PAGE,'Echem1ConnectionString' VALUE FROM DUAL ")
                    StrSql.Append(") ")
                    StrSql.Append("ORDER BY ID ")
                ElseIf PageName = "Econ1" Then
                    StrSql.Append("SELECT PAGE,VALUE FROM ")
                    StrSql.Append("( ")
                    StrSql.Append("SELECT 1 ID,'Econ1' PAGE,'EconConnectionString' VALUE FROM DUAL ")
                    StrSql.Append("UNION ")
                    StrSql.Append("SELECT 2 ID,'Echem1' PAGE,'Echem1ConnectionString' VALUE FROM DUAL ")
                    StrSql.Append(") ")
                    StrSql.Append("ORDER BY ID ")
                ElseIf PageName = "Echem1" Then
                    StrSql.Append("SELECT PAGE,VALUE FROM ")
                    StrSql.Append("( ")
                    StrSql.Append("SELECT 2 ID,'Echem1' PAGE,'Echem1ConnectionString' VALUE FROM DUAL ")
                    StrSql.Append(") ")
                    StrSql.Append("ORDER BY ID ")
                Else
                    StrSql.Append("SELECT PAGE,VALUE FROM ")
                    StrSql.Append("( ")
                    StrSql.Append("SELECT 0 ID,'Nothing Selected' PAGE,'' VALUE FROM DUAL ")
                    StrSql.Append(") ")
                    StrSql.Append("ORDER BY ID ")
                End If
                Dts = odbUtil.FillDataSet(StrSql.ToString(), VChainConnection)
                Return Dts

            Catch ex As Exception
                Throw New Exception("VChainGetData:GetPages:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetModuleCases(ByVal CaseIdE1 As Integer, ByVal CaseIdE2 As Integer, ByVal CaseDe1 As String, ByVal CaseDe2 As String, ByVal userName As String, ByVal strModId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim strCon As String = String.Empty
            Dim Echem1Connection As String = System.Configuration.ConfigurationManager.AppSettings("Echem1ConnectionString")
            Try
                If strModId = "0" Then
                    StrSql = StrSql + "SELECT 0 CaseId,'Nothing Selected' AS CASDES,'Nothing' Casede1,'Selected' Casede2,'Nothing Selected' AS CASDES1 FROM DUAL  "
                    Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                    Return Dts
                Else

                    If strModId = "1" Then
                        strCon = Echem1Connection
                    ElseIf strModId = "2" Then
                        strCon = EconConnection
                    End If
                    If CaseIdE2 >= 1000 Then
                        If CaseIdE1 = -1 And CaseDe1 <> "Nothing" And CaseDe1 <> "Selected" Then
                            StrSql = "SELECT CaseId, (Casede1||'  '||Casede2)CASDES,Casede1,Casede2,replace((Casede1 || ' ' ||  Casede2),'''' ,'##') CASDES1  "
                            StrSql = StrSql + "FROM PermissionsCases "
                            StrSql = StrSql + "WHERE Upper(Username)='" + userName.ToUpper() + "' AND NVL(UPPER(Casede1),'#') LIKE '" + CaseDe1.ToUpper() + "%' "
                            StrSql = StrSql + "AND NVL(UPPER(Casede2),'#') LIKE '" + CaseDe2.ToUpper() + "%' "
                            StrSql = StrSql + "AND CASEID<>" + CaseIdE1.ToString() + " "
                            StrSql = StrSql + "Union  "
                            StrSql = StrSql + "SELECT 0 ,'Nothing Selected' AS CASDES,'Nothing' Casede1,'Selected' Casede2,'Nothing Selected' AS CASDES1 FROM DUAL  "
                        ElseIf CaseIdE1 = -1 And CaseDe1 = "Nothing" Or CaseDe1 = "Selected" Then
                            StrSql = StrSql + "SELECT 0 ,'Nothing Selected' AS CASDES,'Nothing' Casede1,'Selected' Casede2,'Nothing Selected' AS CASDES1 FROM DUAL  "
                        ElseIf CaseIdE1 = 0 Then
                            StrSql = StrSql + "SELECT 0 ,'Nothing Selected' AS CASDES,'Nothing' Casede1,'Selected' Casede2,'Nothing Selected' AS CASDES1 FROM DUAL  "
                        Else
                            StrSql = "SELECT CaseId, (Casede1||'  '||Casede2)CASDES,Casede1,Casede2,replace((Casede1 || ' ' ||  Casede2),'''' ,'##') CASDES1  "
                            StrSql = StrSql + "FROM PermissionsCases "
                            StrSql = StrSql + "WHERE Upper(Username)='" + userName.ToUpper() + "' AND CaseId = CASE WHEN " + CaseIdE1.ToString() + " = -1 THEN "
                            StrSql = StrSql + "CaseId "
                            StrSql = StrSql + "ELSE "
                            StrSql = StrSql + "" + CaseIdE1.ToString() + " "
                            StrSql = StrSql + "END "
                            StrSql = StrSql + "AND NVL(UPPER(Casede1),'#') LIKE '" + CaseDe1.ToUpper() + "%' "
                            StrSql = StrSql + "AND NVL(UPPER(Casede2),'#') LIKE '" + CaseDe2.ToUpper() + "%' "
                            StrSql = StrSql + "AND CASEID<>" + CaseIdE2.ToString() + " "
                        End If
                        StrSql = StrSql + "Order By  CASDES"
                        Dts = odbUtil.FillDataSet(StrSql, strCon)
                        Return Dts
                    Else
                        If CaseIdE1 = -1 Then
                            StrSql = "SELECT CaseId, (Casede1||'  '||Casede2)CASDES,Casede1,Casede2,replace((Casede1 || ' ' ||  Casede2),'''' ,'##') CASDES1  "
                            StrSql = StrSql + "FROM BaseCases "
                            StrSql = StrSql + "WHERE NVL(UPPER(Casede1),'#') LIKE '" + CaseDe1.ToUpper() + "%' "
                            StrSql = StrSql + "AND NVL(UPPER(Casede2),'#') LIKE '" + CaseDe2.ToUpper() + "%' "
                            StrSql = StrSql + "AND CASEID<>" + CaseIdE2.ToString() + " "
                            StrSql = StrSql + "Union  "
                            StrSql = StrSql + "SELECT 0 ,'Nothing Selected' AS CASDES,'Nothing' Casede1,'Selected' Casede2,'Nothing Selected' AS CASDES1 FROM DUAL "
                        ElseIf CaseIdE1 = 0 Then
                            StrSql = StrSql + "SELECT 0 ,'Nothing Selected' AS CASDES,'Nothing' Casede1,'Selected' Casede2,'Nothing Selected' AS CASDES1 FROM DUAL "
                        Else
                            StrSql = "SELECT CaseId, (Casede1||'  '||Casede2)CASDES,Casede1,Casede2,replace((Casede1 || ' ' ||  Casede2),'''' ,'##') CASDES1  "
                            StrSql = StrSql + "FROM BaseCases "
                            StrSql = StrSql + "WHERE CaseId = CASE WHEN " + CaseIdE1.ToString() + " = -1 THEN "
                            StrSql = StrSql + "CaseId "
                            StrSql = StrSql + "ELSE "
                            StrSql = StrSql + "" + CaseIdE1.ToString() + " "
                            StrSql = StrSql + "END "
                            StrSql = StrSql + "AND NVL(UPPER(Casede1),'#') LIKE '" + CaseDe1.ToUpper() + "%' "
                            StrSql = StrSql + "AND NVL(UPPER(Casede2),'#') LIKE '" + CaseDe2.ToUpper() + "%' "
                            StrSql = StrSql + "AND CASEID<>" + CaseIdE2.ToString() + " "
                        End If

                        StrSql = StrSql + "Order By  CASDES"
                        Dts = odbUtil.FillDataSet(StrSql, strCon)
                        Return Dts
                    End If

                End If

            Catch ex As Exception
                Throw New Exception("VChainGetData:GetModuleCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetDescription(ByVal ValueChainId As String) As DataTable
            Dim Dts As New DataTable()
            Try
                Dim odbUtil As New DBUtil()
                Dim StrSql As String = String.Empty

                StrSql = "SELECT VALUECHAINID,VALUECHAINNAME,MODNAME,RESULTCASES,RESULTCASES  "
                StrSql = StrSql + "FROM USRVALUECHAIN "
                StrSql = StrSql + " WHERE VALUECHAINID= " + ValueChainId

                Dts = odbUtil.FillDataTable(StrSql, VChainConnection)
                'Des = Dts.Rows(0).Item("VALUECHAINNAME")
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
#End Region
#Region "ASSUMPTIONS"
        Public Function GetAllVChainCases(ByVal VChainId As String, ByVal ResultMod As String, ByVal ResultCaseID As String) As DataSet
            Dim Dts As New DataSet()
            Try
                Dim odbUtil As New DBUtil()
                Dim StrSql As String = ""
                Dim tblName As String = String.Empty
                Dim i As Integer
                If ResultMod.ToUpper() = "ECON2" Then
                    tblName = "MATCASE_ECON2"
                ElseIf ResultMod.ToUpper() = "ECON1" Then
                    tblName = "MATCASE_ECON1"
                ElseIf ResultMod.ToUpper() = "ECHEM1" Then
                    tblName = "MATCASE_ECHEM1"
                End If
                'StrSql = "SELECT CASE WHEN MODTYPE=2 THEN 'ECON1' WHEN MODTYPE=1 THEN 'ECHM1' ELSE 'ECON2' END MODNAME,MODTYPE,MODCASEID,LAYER FROM " + tblName
                StrSql = "SELECT  "
                StrSql = StrSql + "CASE WHEN MODTYPE=2 THEN 'Econ1' WHEN MODTYPE=1 THEN 'Echem' ELSE 'Econ2' END MODNAME, "
                StrSql = StrSql + "MODTYPE, "
                StrSql = StrSql + "MODCASEID, "
                StrSql = StrSql + "LAYER, "
                StrSql = StrSql + "VALUECHAINID "
                StrSql = StrSql + "FROM " + tblName
                StrSql = StrSql + " WHERE  VALUECHAINID=" + VChainId.ToString() + " AND  CASEID=" + ResultCaseID.ToString() + " AND MODTYPE > 0 "
                Dts = odbUtil.FillDataSet(StrSql, VChainConnection)
                For i = 0 To Dts.Tables(0).Rows.Count - 1
                    If Dts.Tables(0).Rows(i)("MODTYPE").ToString() = "2" Then
                        tblName = "MATCASE_ECON1"
                    ElseIf Dts.Tables(0).Rows(i)("MODTYPE").ToString() = "1" Then
                        tblName = "MATCASE_ECHEM1"
                    ElseIf Dts.Tables(0).Rows(i)("MODTYPE").ToString() = "3" Then
                        tblName = "MATCASE_ECON2"
                    End If

                    StrSql = StrSql + " UNION  "
                    'StrSql = StrSql + "SELECT MODTYPE,MODCASEID,LAYER FROM " + tblName
                    StrSql = StrSql + " SELECT  "
                    StrSql = StrSql + "CASE WHEN MODTYPE=2 THEN 'Econ1' WHEN MODTYPE=1 THEN 'Echem1' ELSE 'Econ2' END MODNAME, "
                    StrSql = StrSql + "MODTYPE, "
                    StrSql = StrSql + "MODCASEID, "
                    StrSql = StrSql + "LAYER, "
                    StrSql = StrSql + "VALUECHAINID "
                    StrSql = StrSql + "FROM " + tblName
                    StrSql = StrSql + " WHERE VALUECHAINID=" + VChainId.ToString() + " AND CASEID= " + Dts.Tables(0).Rows(i)("MODCASEID").ToString() + " AND MODTYPE > 0 "
                Next

                Dts = odbUtil.FillDataSet(StrSql, VChainConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("VChainGetData:GetAllVChainCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetMaterialsWithPrice(ByVal ModName As String, ByVal CaseID As String) As DataSet
            Dim Dts As New DataSet()
            Try
                Dim odbUtil As New DBUtil()
                Dim StrSql As String = ""
                Dim con As String = String.Empty               
                If ModName.ToUpper = "ECON1" Then
                    con = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
                ElseIf ModName.ToUpper = "ECON2" Then
                    con = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
                End If

                StrSql = "SELECT MAT.CASEID,  "
                StrSql = StrSql + "MAT1.MATDE1||' '||MAT1.MATDE2 AS MATDES1 , "
                StrSql = StrSql + "MAT2.MATDE1||' '||MAT2.MATDE2 AS MATDES2, "
                StrSql = StrSql + "MAT3.MATDE1||' '||MAT3.MATDE2 AS MATDES3, "
                StrSql = StrSql + "MAT4.MATDE1||' '||MAT4.MATDE2 AS MATDES4, "
                StrSql = StrSql + "MAT5.MATDE1||' '||MAT5.MATDE2 AS MATDES5, "
                StrSql = StrSql + "MAT6.MATDE1||' '||MAT6.MATDE2 AS MATDES6, "
                StrSql = StrSql + "MAT7.MATDE1||' '||MAT7.MATDE2 AS MATDES7, "
                StrSql = StrSql + "MAT8.MATDE1||' '||MAT8.MATDE2 AS MATDES8, "
                StrSql = StrSql + "MAT9.MATDE1||' '||MAT9.MATDE2 AS MATDES9, "
                StrSql = StrSql + "MAT10.MATDE1||' '||MAT10.MATDE2 AS MATDES10, "
                StrSql = StrSql + "(NVL(MATA1.PRICE,0)/PREF.CONVWT*PREF.CURR)AS PRS1, "
                StrSql = StrSql + "(NVL(MATA2.PRICE,0)/PREF.CONVWT*PREF.CURR)AS PRS2, "
                StrSql = StrSql + "(NVL(MATA3.PRICE,0)/PREF.CONVWT*PREF.CURR)AS PRS3, "
                StrSql = StrSql + "(NVL(MATA4.PRICE,0)/PREF.CONVWT*PREF.CURR)AS PRS4, "
                StrSql = StrSql + "(NVL(MATA5.PRICE,0)/PREF.CONVWT*PREF.CURR)AS PRS5, "
                StrSql = StrSql + "(NVL(MATA6.PRICE,0)/PREF.CONVWT*PREF.CURR)AS PRS6, "
                StrSql = StrSql + "(NVL(MATA7.PRICE,0)/PREF.CONVWT*PREF.CURR)AS PRS7, "
                StrSql = StrSql + "(NVL(MATA8.PRICE,0)/PREF.CONVWT*PREF.CURR)AS PRS8, "
                StrSql = StrSql + "(NVL(MATA9.PRICE,0)/PREF.CONVWT*PREF.CURR)AS PRS9, "
                StrSql = StrSql + "(NVL(MATA10.PRICE,0)/PREF.CONVWT*PREF.CURR)AS PRS10, "
                StrSql = StrSql + "(MAT.S1/PREF.CONVWT*PREF.CURR) AS PRP1, "
                StrSql = StrSql + "(MAT.S2/PREF.CONVWT*PREF.CURR) AS PRP2, "
                StrSql = StrSql + "(MAT.S3/PREF.CONVWT*PREF.CURR) AS PRP3, "
                StrSql = StrSql + "(MAT.S4/PREF.CONVWT*PREF.CURR) AS PRP4, "
                StrSql = StrSql + "(MAT.S5/PREF.CONVWT*PREF.CURR) AS PRP5, "
                StrSql = StrSql + "(MAT.S6/PREF.CONVWT*PREF.CURR) AS PRP6, "
                StrSql = StrSql + "(MAT.S7/PREF.CONVWT*PREF.CURR) AS PRP7, "
                StrSql = StrSql + "(MAT.S8/PREF.CONVWT*PREF.CURR) AS PRP8, "
                StrSql = StrSql + "(MAT.S9/PREF.CONVWT*PREF.CURR) AS PRP9, "
                StrSql = StrSql + "(MAT.S10/PREF.CONVWT*PREF.CURR) AS PRP10, "
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
                StrSql = StrSql + "MATERIALINPUT  MAT "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=MAT.CASEID "
                StrSql = StrSql + "LEFT OUTER JOIN MATERIALSARCH MATA1 "
                StrSql = StrSql + "ON MATA1.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA1.MATID = MAT.M1 "
                StrSql = StrSql + "LEFT OUTER JOIN MATERIALSARCH MATA2 "
                StrSql = StrSql + "ON MATA2.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA2.MATID = MAT.M2 "
                StrSql = StrSql + "LEFT OUTER JOIN MATERIALSARCH MATA3 "
                StrSql = StrSql + "ON MATA3.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA3.MATID = MAT.M3 "
                StrSql = StrSql + "LEFT OUTER JOIN MATERIALSARCH MATA4 "
                StrSql = StrSql + "ON MATA4.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA4.MATID = MAT.M4 "
                StrSql = StrSql + "LEFT OUTER JOIN MATERIALSARCH MATA5 "
                StrSql = StrSql + "ON MATA5.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA5.MATID = MAT.M5 "
                StrSql = StrSql + "LEFT OUTER JOIN MATERIALSARCH MATA6 "
                StrSql = StrSql + "ON MATA6.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA6.MATID = MAT.M6 "
                StrSql = StrSql + "LEFT OUTER JOIN MATERIALSARCH MATA7 "
                StrSql = StrSql + "ON MATA7.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA7.MATID = MAT.M7 "
                StrSql = StrSql + "LEFT OUTER JOIN MATERIALSARCH MATA8 "
                StrSql = StrSql + "ON MATA8.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA8.MATID = MAT.M8 "
                StrSql = StrSql + "LEFT OUTER JOIN MATERIALSARCH MATA9 "
                StrSql = StrSql + "ON MATA9.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA9.MATID = MAT.M9 "
                StrSql = StrSql + "LEFT OUTER JOIN MATERIALSARCH MATA10 "
                StrSql = StrSql + "ON MATA10.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA10.MATID = MAT.M10 "
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
                StrSql = StrSql + "WHERE MAT.CASEID=" + CaseID.ToString()
                Dts = odbUtil.FillDataSet(StrSql, con)
                Return Dts
            Catch ex As Exception
                Throw New Exception("VChainGetData:GetAllVChainCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCaseWithVPrice(ByVal VChainId As String, ByVal ResultMod As String, ByVal ResultCaseID As String) As DataSet
            Dim Dts As New DataSet()
            Dim ds As New DataSet
            Try
                Dim odbUtil As New DBUtil()
                Dim StrSql As String = ""
                Dim tblName As String = String.Empty
                If ResultMod.ToUpper() = "ECON2" Then
                    tblName = "MATCASE_ECON2"
                    Dim ObjGetData As New E2GetData.Selectdata()
                    Dts = ObjGetData.GetPref(ResultCaseID)
                ElseIf ResultMod.ToUpper() = "ECON1" Then
                    tblName = "MATCASE_ECON1"
                    Dim ObjGetData As New E1GetData.Selectdata()
                    Dts = ObjGetData.GetPref(ResultCaseID)
                ElseIf ResultMod.ToUpper() = "ECHEM1" Then
                    tblName = "MATCASE_ECHEM1"
                    'Dim ObjGetData As New Echem1GetData.Selectdata()
                    'Dts = ObjGetData.GetPref(ResultCaseID)
                End If

                Dim Convwt As String = Dts.Tables(0).Rows(0).Item("convwt")
                Dim Curr As String = Dts.Tables(0).Rows(0).Item("curr")

                'StrSql = "SELECT CASE WHEN MODTYPE=2 THEN 'ECON1' WHEN MODTYPE=1 THEN 'ECHM1' ELSE 'ECON2' END MODNAME,MODTYPE,MODCASEID,LAYER FROM " + tblName
                StrSql = "SELECT  "
                ' StrSql = StrSql + "CASE WHEN MODTYPE=2 THEN 'Econ1' WHEN MODTYPE=1 THEN 'Echem' ELSE 'Econ2' END MODNAME, "
                StrSql = StrSql + "MODTYPE, "
                StrSql = StrSql + "MODCASEID, "
                StrSql = StrSql + "LAYER, "
                StrSql = StrSql + "PRICE" + "/" + Convwt.ToString() + " * " + Curr.ToString() + " AS PRICE"
                StrSql = StrSql + " FROM " + tblName
                StrSql = StrSql + " WHERE CASEID=" + ResultCaseID.ToString() + " "
                StrSql = StrSql + " AND VALUECHAINID=" + VChainId.ToString()
                StrSql = StrSql + " ORDER BY LAYER"
                ds = odbUtil.FillDataSet(StrSql, VChainConnection)
                Return ds
            Catch ex As Exception
                Throw New Exception("VChainGetData:GetCaseWithVPrice:" + ex.Message.ToString())
                Return ds
            End Try
        End Function
#End Region
#Region "Results"
        Public Function GetProfitAndLossDetails(ByVal VChianId As String, ByVal CaseId As String, ByVal ModType As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim Pm As String = String.Empty

            Try

                If ModType.ToUpper() = "ECON2" Then
                    Dts = GetProfitAndLossDetailsEcon2(VChianId, CaseId)
                ElseIf ModType.ToUpper() = "ECON1" Then
                    Dts = GetProfitAndLossDetailsEcon1(VChianId, CaseId)
                ElseIf ModType.ToUpper() = "ECHEM1" Then
                    Dts = GetProfitAndLossDetailsEchem1(VChianId, CaseId)
                End If

               
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetPalletInDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetProfitAndLossDetailsEcon1(ByVal VChianId As String, ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT  "
                StrSql = StrSql + "'Revenue' AS PDES1, "
                StrSql = StrSql + "'Materials' AS PDES2, "
                StrSql = StrSql + "'Labor' AS PDES3, "
                StrSql = StrSql + "'Energy' AS PDES4, "
                StrSql = StrSql + "'Distribution Packaging' AS PDES5, "
                StrSql = StrSql + "'Shipping to Customer' AS PDES6, "
                StrSql = StrSql + "'Variable Margin' AS PDES7, "
                StrSql = StrSql + "'Office Supplies' AS PDES8, "
                StrSql = StrSql + "'Labor' AS PDES9, "
                StrSql = StrSql + "'Energy' AS PDES10, "
                StrSql = StrSql + "'Lease Cost' AS PDES11, "
                StrSql = StrSql + "'Insurance' AS PDES12, "
                StrSql = StrSql + "'Utilities' AS PDES13, "
                StrSql = StrSql + "'Communications' AS PDES14, "
                StrSql = StrSql + "'Travel' AS PDES15, "
                StrSql = StrSql + "'Maintenance Supplies' AS PDES16, "
                StrSql = StrSql + "'Minor Equipment' AS PDES17, "
                StrSql = StrSql + "'Outside Services'  AS PDES18, "
                StrSql = StrSql + "'Professional Services' AS PDES19, "
                StrSql = StrSql + "'Laboratory Supplies' AS PDES20, "
                StrSql = StrSql + "'Ink Supplies' AS PDES21, "
                StrSql = StrSql + "'Plate Supplies' AS PDES22, "
                StrSql = StrSql + "'Metal Supplies' AS PDES23, "
                StrSql = StrSql + "'Depreciation' AS PDES24, "
                StrSql = StrSql + "'Plant Margin' AS PDES25, "
                StrSql = StrSql + "REVENUE AS PL1, "
                StrSql = StrSql + "VMATERIAL AS PL2, "
                StrSql = StrSql + "VLABOR AS PL3, "
                StrSql = StrSql + "VENERGY AS PL4, "
                StrSql = StrSql + "VPACK AS PL5, "
                StrSql = StrSql + "VSHIP AS PL6, "
                StrSql = StrSql + "VM AS PL7, "
                StrSql = StrSql + "OFFICESUPPLIES AS PL8, "
                StrSql = StrSql + "PLABOR AS PL9, "
                StrSql = StrSql + "PENERGY AS PL10, "
                StrSql = StrSql + "LEASECOST AS PL11, "
                StrSql = StrSql + "INSURANCE AS PL12, "
                StrSql = StrSql + "UTILITIES AS PL13, "
                StrSql = StrSql + "COMMUN AS PL14, "
                StrSql = StrSql + "TRAVEL AS PL15, "
                StrSql = StrSql + "MAINT AS PL16, "
                StrSql = StrSql + "MINOR AS PL17, "
                StrSql = StrSql + "OUT  AS PL18, "
                StrSql = StrSql + "PROF AS PL19, "
                StrSql = StrSql + "LAB AS PL20, "
                StrSql = StrSql + "INKSUP AS PL21, "
                StrSql = StrSql + "PLATESUP AS PL22, "
                StrSql = StrSql + "METSUP AS PL23, "
                StrSql = StrSql + "PM AS PL25, "
                StrSql = StrSql + "(CASE  WHEN FINVOLMSI > 1 THEN "
                StrSql = StrSql + "PREF.TITLE6||' per '||	PREF.TITLE3 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "PREF.TITLE6||' per unit ' "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END)PUN, "
                StrSql = StrSql + "(CASE  WHEN FINVOLMSI > 1 THEN "
                StrSql = StrSql + "PREF.TITLE3 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "'units' "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END)SUNITLBL, "
                StrSql = StrSql + "NVL( "
                StrSql = StrSql + "(CASE  WHEN FINVOLMSI > 1 THEN "
                StrSql = StrSql + "(100*PREF.CURR/PREF.CONVAREA)/FINVOLMSI "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "(100*PREF.CURR)/FINVOLMUNITS "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END),0)SUNIT, "
                StrSql = StrSql + "NVL((CASE  WHEN FINVOLMSI > 1 THEN "
                StrSql = StrSql + "(FINVOLMSI*PREF.CONVAREA)"
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "(FINVOLMUNITS) "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END),0)SUNITVAL, "
                StrSql = StrSql + "(VOLUME*PREF.CONVWT) SVOLUME, "
                'StrSql = StrSql + "UNITTYPE, "
                StrSql = StrSql + "PREF.CURR, "
                StrSql = StrSql + "PREF.CONVAREA, "
                StrSql = StrSql + "PREF.CONVWT, "
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
                StrSql = StrSql + "RESULTSPL_ECON1.CASEID "
                StrSql = StrSql + "FROM RESULTSPL_ECON1 "
                StrSql = StrSql + "INNER JOIN ECON.PREFERENCES PREF "


                StrSql = StrSql + "ON PREF.CASEID=RESULTSPL_ECON1.CASEID "
                StrSql = StrSql + "WHERE VALUECHAINID=" + VChianId.ToString() + " AND RESULTSPL_ECON1.CASEID IN (" + CaseId.ToString() + ") "


                Dts = odbUtil.FillDataSet(StrSql, VChainConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetPalletInDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetProfitAndLossDetailsEcon2(ByVal VChianId As String, ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT  RSPL.CASEID ,  "
                StrSql = StrSql + "'Revenue' AS PDES1, "
                StrSql = StrSql + "'Materials' AS PDES2, "
                StrSql = StrSql + "'Labor' AS PDES3, "
                StrSql = StrSql + "'Energy' AS PDES4, "
                StrSql = StrSql + "'Distribution Packaging' AS PDES5, "
                StrSql = StrSql + "'Shipping to Customer' AS PDES6, "
                StrSql = StrSql + "'Variable Margin' AS PDES7, "
                StrSql = StrSql + "'Office Supplies' AS PDES8, "
                StrSql = StrSql + "'Labor' AS PDES9, "
                StrSql = StrSql + "'Energy' AS PDES10, "
                StrSql = StrSql + "'Lease Cost' AS PDES11, "
                StrSql = StrSql + "'Insurance' AS PDES12, "
                StrSql = StrSql + "'Utilities' AS PDES13, "
                StrSql = StrSql + "'Communications' AS PDES14, "
                StrSql = StrSql + "'Travel' AS PDES15, "
                StrSql = StrSql + "'Maintenance Supplies' AS PDES16, "
                StrSql = StrSql + "'Minor Equipment' AS PDES17, "
                StrSql = StrSql + "'Outside Services'  AS PDES18, "
                StrSql = StrSql + "'Professional Services' AS PDES19, "
                StrSql = StrSql + "'Laboratory Supplies' AS PDES20, "
                StrSql = StrSql + "'Ink Supplies' AS PDES21, "
                StrSql = StrSql + "'Plate Supplies' AS PDES22, "
                StrSql = StrSql + "'Metal Supplies' AS PDES23, "
                StrSql = StrSql + "'Plant Margin' AS PDES25, "
                StrSql = StrSql + "(CASE WHEN RSPL1.PVOLUSE=0 THEN RSPL.VOLUME*PREF.CONVWT "
                StrSql = StrSql + "ELSE RSPL.SVOLUME*PREF.CONVWT END) AS SVOLUME, "
                StrSql = StrSql + "NVL((CASE WHEN RSPL.FINVOLMUNITS>0 THEN RSPL.FINVOLMUNITS END),0) AS SUNITVAL, "
                StrSql = StrSql + "'Sales Volume ('|| PREF.TITLE8||')' AS VOLUNI1, "

                StrSql = StrSql + "NVL ((CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "(100*PREF.CURR)/FINVOLMUNITS "
                StrSql = StrSql + "END),0) AS  "
                StrSql = StrSql + " SUNIT, "

                StrSql = StrSql + "'units' SUNITLBL, "
                StrSql = StrSql + "RSPL.REVENUE AS PL1, "
                StrSql = StrSql + "RSPL.VMATERIAL AS PL2 , "
                StrSql = StrSql + "RSPL.VLABOR AS PL3, "
                StrSql = StrSql + "RSPL.VENERGY AS PL4, "
                StrSql = StrSql + "RSPL.VPACK AS PL5, "
                StrSql = StrSql + "RSPL.VSHIP AS PL6, "
                StrSql = StrSql + "RSPL.VM AS PL7, "
                StrSql = StrSql + "RSPL.OFFICESUPPLIES AS PL8, "
                StrSql = StrSql + "RSPL.PLABOR AS PL9, "
                StrSql = StrSql + "RSPL.PENERGY AS PL10, "
                StrSql = StrSql + "RSPL.LEASECOST AS PL11, "
                StrSql = StrSql + "RSPL.INSURANCE AS PL12, "
                StrSql = StrSql + "RSPL.UTILITIES AS PL13, "
                StrSql = StrSql + "RSPL.COMMUN AS PL14, "
                StrSql = StrSql + "RSPL.TRAVEL AS PL15, "
                StrSql = StrSql + "RSPL.MAINT AS PL16, "
                StrSql = StrSql + "RSPL.MINOR AS PL17, "
                StrSql = StrSql + "RSPL.OUT  AS PL18, "
                StrSql = StrSql + "RSPL.PROF AS PL19, "
                StrSql = StrSql + "RSPL.LAB AS PL20, "
                StrSql = StrSql + "RSPL.INKSUP AS PL21, "
                StrSql = StrSql + "RSPL.PLATESUP AS PL22, "
                StrSql = StrSql + "RSPL.metsup AS PL23, "
                StrSql = StrSql + "RSPL.pm AS PL25, "
                StrSql = StrSql + "RSPL.finvolmsi, "
                StrSql = StrSql + "RSPL.finvolmunits, "
                StrSql = StrSql + "RSPL.volume AS PVOLUME, "
                StrSql = StrSql + "PREF.CURR, "
                StrSql = StrSql + "PREF.CONVAREA, "
                StrSql = StrSql + "PREF.CONVWT, "
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
                StrSql = StrSql + "(CASE WHEN RSPL.FINVOLMUNITS>0 THEN PREF.TITLE6  || ' Per unit' END) AS PUN "

                StrSql = StrSql + "FROM RESULTSPL_ECON2 RSPL "
                StrSql = StrSql + "INNER JOIN ECON2.PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=RSPL.CASEID "
                StrSql = StrSql + " INNER JOIN ECON2.RESULTSPL RSPL1  "
                StrSql = StrSql + "ON  RSPL.CASEID=RSPL1.CASEID "


                StrSql = StrSql + "WHERE VALUECHAINID=" + VChianId.ToString() + " AND RSPL.CASEID IN (" + CaseId.ToString() + ") "


                Dts = odbUtil.FillDataSet(StrSql, VChainConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetPalletInDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetProfitAndLossDetailsEchem1(ByVal VChianId As String, ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT  "
                StrSql = StrSql + "'Revenue' AS PDES1, "
                StrSql = StrSql + "'Materials' AS PDES2, "
                StrSql = StrSql + "'Labor' AS PDES3, "
                StrSql = StrSql + "'Energy' AS PDES4, "
                StrSql = StrSql + "'Distribution Packaging' AS PDES5, "
                StrSql = StrSql + "'Shipping to Customer' AS PDES6, "
                StrSql = StrSql + "'Variable Margin' AS PDES7, "
                StrSql = StrSql + "'Office Supplies' AS PDES8, "
                StrSql = StrSql + "'Labor' AS PDES9, "
                StrSql = StrSql + "'Energy' AS PDES10, "
                StrSql = StrSql + "'Lease Cost' AS PDES11, "
                StrSql = StrSql + "'Insurance' AS PDES12, "
                StrSql = StrSql + "'Utilities' AS PDES13, "
                StrSql = StrSql + "'Communications' AS PDES14, "
                StrSql = StrSql + "'Travel' AS PDES15, "
                StrSql = StrSql + "'Maintenance Supplies' AS PDES16, "
                StrSql = StrSql + "'Minor Equipment' AS PDES17, "
                StrSql = StrSql + "'Outside Services'  AS PDES18, "
                StrSql = StrSql + "'Professional Services' AS PDES19, "
                StrSql = StrSql + "'Laboratory Supplies' AS PDES20, "
                StrSql = StrSql + "'Ink Supplies' AS PDES21, "
                StrSql = StrSql + "'Plate Supplies' AS PDES22, "
                StrSql = StrSql + "'Metal Supplies' AS PDES23, "
                StrSql = StrSql + "'Depreciation' AS PDES24, "
                StrSql = StrSql + "'Plant Margin' AS PDES25, "
                StrSql = StrSql + "REVENUE AS PL1, "
                StrSql = StrSql + "VMATERIAL AS PL2, "
                StrSql = StrSql + "VLABOR AS PL3, "
                StrSql = StrSql + "VENERGY AS PL4, "
                StrSql = StrSql + "VPACK AS PL5, "
                StrSql = StrSql + "VSHIP AS PL6, "
                StrSql = StrSql + "VM AS PL7, "
                StrSql = StrSql + "OFFICESUPPLIES AS PL8, "
                StrSql = StrSql + "PLABOR AS PL9, "
                StrSql = StrSql + "PENERGY AS PL10, "
                StrSql = StrSql + "LEASECOST AS PL11, "
                StrSql = StrSql + "INSURANCE AS PL12, "
                StrSql = StrSql + "UTILITIES AS PL13, "
                StrSql = StrSql + "COMMUN AS PL14, "
                StrSql = StrSql + "TRAVEL AS PL15, "
                StrSql = StrSql + "MAINT AS PL16, "
                StrSql = StrSql + "MINOR AS PL17, "
                StrSql = StrSql + "OUT  AS PL18, "
                StrSql = StrSql + "PROF AS PL19, "
                StrSql = StrSql + "LAB AS PL20, "
                StrSql = StrSql + "INKSUP AS PL21, "
                StrSql = StrSql + "PLATESUP AS PL22, "
                StrSql = StrSql + "METSUP AS PL23, "
                StrSql = StrSql + "PM AS PL25, "
                StrSql = StrSql + "(CASE  WHEN FINVOLMSI > 1 THEN "
                StrSql = StrSql + "PREF.TITLE6||' per '||	PREF.TITLE3 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "PREF.TITLE6||' per unit ' "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END)PUN, "
                StrSql = StrSql + "(CASE  WHEN FINVOLMSI > 1 THEN "
                StrSql = StrSql + "PREF.TITLE3 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "'units' "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END)SUNITLBL, "
                StrSql = StrSql + "NVL( "
                StrSql = StrSql + "(CASE  WHEN FINVOLMSI > 1 THEN "
                StrSql = StrSql + "(100*PREF.CURR/PREF.CONVAREA)/FINVOLMSI "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "(100*PREF.CURR)/FINVOLMUNITS "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END),0)SUNIT, "
                StrSql = StrSql + "NVL((CASE  WHEN FINVOLMSI > 1 THEN "
                StrSql = StrSql + "(FINVOLMSI*PREF.CONVAREA)"
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "(FINVOLMUNITS) "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END),0)SUNITVAL, "
                StrSql = StrSql + "(VOLUME*PREF.CONVWT) SVOLUME, "
                'StrSql = StrSql + "UNITTYPE, "
                StrSql = StrSql + "PREF.CURR, "
                StrSql = StrSql + "PREF.CONVAREA, "
                StrSql = StrSql + "PREF.CONVWT, "
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
                StrSql = StrSql + "RESULTSPL.CASEID "
                StrSql = StrSql + "FROM RESULTSPL_ECHEM1 "
                StrSql = StrSql + "INNER JOIN ECHEM1.PREFERENCES PREF "


                StrSql = StrSql + "ON PREF.CASEID=RESULTSPL_ECHEM1.CASEID "
                StrSql = StrSql + "WHERE VALUECHAINID=" + VChianId.ToString() + " AND RESULTSPL_ECHEM1.CASEID IN (" + CaseId.ToString() + ") "


                Dts = odbUtil.FillDataSet(StrSql, VChainConnection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetPalletInDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region
    End Class
End Class
