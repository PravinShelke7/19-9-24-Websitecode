Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System

Public Class ToolCCS

#Region "PLM"
    Public Sub UpdateCompany(ByVal CaseId As String, ByVal COMPANYID As String)
        Dim odbUtil As New DBUtil()
        Dim ObjGetData As New E1GetData.Selectdata()
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
        Dim StrSql As String = String.Empty
        Try
            StrSql = "UPDATE PERMISSIONSCASES SET "
            StrSql = StrSql + "PACKSPECCMPNYID=" + COMPANYID.ToString() + " "
            StrSql = StrSql + "WHERE CASEID=" + CaseId.ToString() + ""
            odbUtil.UpIns(StrSql, Connection)

        Catch ex As Exception
            Throw New Exception("ToolCCS:UpdateCompany:" + ex.Message.ToString())
        End Try

    End Sub
    Public Sub newCompany(ByVal USERID As String, ByVal CompanyName As String)
        Dim odbUtil As New DBUtil()
        Dim ObjGetData As New E1GetData.Selectdata()
        Dim ds As New DataSet
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings("ShoppingConnectionString")
        Dim StrSql As String = String.Empty
        Try
            ds = ObjGetData.GetCompanyName(CompanyName)
            If ds.Tables(0).Rows.Count = 0 Then
                StrSql = "INSERT INTO SHOPPING.COMPANY  "
                StrSql = StrSql + "(COMPANYID,COMPANYNAME,PARENTID) "
                StrSql = StrSql + "VALUES "
                StrSql = StrSql + "(SEQCOMPANYID.NEXTVAL,'" + CompanyName.ToString() + "',0) "
                odbUtil.UpIns(StrSql, Connection)
            End If

            StrSql = "INSERT INTO SHOPPING.COMPANYLICENSE  "
            StrSql = StrSql + "(COMPANYID,LICENSEID,SERVERDATE) "
            StrSql = StrSql + "SELECT COMPANYID,LICENSEID,SYSDATE  "
            StrSql = StrSql + "FROM SHOPPING.COMPANY ,ECON.LICENSEMASTER "
            StrSql = StrSql + "WHERE COMPANYID=(SELECT COMPANYID FROM SHOPPING.COMPANY WHERE UPPER(COMPANYNAME)='" + CompanyName.ToString().ToUpper() + "') "
            StrSql = StrSql + " And LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + USERID.ToString() + ") "
            odbUtil.UpIns(StrSql, Connection)

        Catch ex As Exception
            Throw New Exception("ToolCCS:newGroup:" + ex.Message.ToString())
        End Try

    End Sub

    Public Sub UpdateGroup(ByVal CaseId As String, ByVal GroupId As String)
        Dim odbUtil As New DBUtil()
        Dim ObjGetData As New E1GetData.Selectdata()
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
        Dim StrSql As String = String.Empty
        Try
            StrSql = "UPDATE PERMISSIONSCASES SET "
            StrSql = StrSql + " PACKSPECGRPID =" + GroupId.ToString() + " "
            StrSql = StrSql + "WHERE CASEID=" + CaseId.ToString() + " "
            odbUtil.UpIns(StrSql, Connection)

        Catch ex As Exception
            Throw New Exception("ToolCCS:UpdateGroup:" + ex.Message.ToString())
        End Try

    End Sub
    Public Sub newGroup(ByVal GroupName As String, ByVal UserId As String)
        Dim odbUtil As New DBUtil()
        Dim ObjGetData As New E1GetData.Selectdata()
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
        Dim StrSql As String = String.Empty
        Dim ds As DataSet
        Try
            ds = ObjGetData.GetGroupName(GroupName)
            If ds.Tables(0).Rows.Count = 0 Then
                StrSql = "INSERT INTO PACKSPECGROUP  "
                StrSql = StrSql + "(PACKSPECGRPID,GRPDETAIL) "
                StrSql = StrSql + "VALUES "
                StrSql = StrSql + "(SEQPACKSPECGRPID.NEXTVAL,'" + GroupName.ToString() + "') "
                odbUtil.UpIns(StrSql, Connection)
            End If


            StrSql = "INSERT INTO PACKSPECGRPLICENSE "
            StrSql = StrSql + "(PACKSPECGRPID,LICENSEID,SERVERDATE) "
            StrSql = StrSql + "SELECT PACKSPECGRPID,LICENSEID,SYSDATE  "
            StrSql = StrSql + "FROM PACKSPECGROUP ,ECON.LICENSEMASTER "
            StrSql = StrSql + "WHERE PACKSPECGRPID=(SELECT PACKSPECGRPID FROM PACKSPECGROUP WHERE UPPER(GRPDETAIL)='" + GroupName.ToString().ToUpper() + "') "
            StrSql = StrSql + " And LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserId.ToString() + ") "
            odbUtil.UpIns(StrSql, Connection)

        Catch ex As Exception
            Throw New Exception("ToolCCS:newGroup:" + ex.Message.ToString())
        End Try

    End Sub

#End Region

   Public Sub UpdatePermissionCasesSis(ByVal Schema As String, ByVal CaseId As String, ByVal SisCaseId As String)
        Dim DtsTbl As New DataSet()
        Dim DtsTblDet As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim Column As String = String.Empty
        Dim Value As String = String.Empty
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Dim i As New Integer
        Dim j As New Integer

        Try

            'UPDATE PermissionsCases
            StrSql = "UPDATE PERMISSIONSCASES  "
            StrSql = StrSql + "SET SISTERCASEID=" + SisCaseId.ToString() + " "
            StrSql = StrSql + "WHERE CASEID=" + CaseId.ToString() + " "
            odbUtil.UpIns(StrSql, Connection)



        Catch ex As Exception
            Throw New Exception("ToolCCS:UpdatePermissionCasesSis:" + ex.Message.ToString())
        End Try
    End Sub
    Public Function CreateCase(ByVal UserId As String, ByVal Schema As String, ByVal PSchema As String, ByVal CaseId As Integer) As Integer
        Dim DtsTbl As New DataSet()
        Dim DtsTblDet As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim Column As String = String.Empty
        Dim Value As String = String.Empty
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Dim i As New Integer
        Dim j As New Integer

        Try
            'Create Case Table Details
            DtsTbl = GetCreateCaseTable(Schema)
            'Case Id 
            If CaseId = 0 Then
                CaseId = GetCaseId(PSchema)
            End If


            'Insert Into PermissionsCases
            StrSql = "INSERT INTO PERMISSIONSCASES  "
            StrSql = StrSql + "(SERVERDATE,CREATIONDATE, USERID, CASEID) "
            StrSql = StrSql + "VALUES "
            StrSql = StrSql + "(SYSDATE,SYSDATE," + UserId.ToString() + "," + CaseId.ToString() + ") "
            odbUtil.UpIns(StrSql, Connection)

            'Insert In other Tables
            For i = 0 To DtsTbl.Tables(0).Rows.Count - 1
                DtsTblDet = GetTableDetails(DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString(), Schema, True, False, False)
                For j = 0 To DtsTblDet.Tables(0).Rows.Count - 1
                    Column = Column + DtsTblDet.Tables(0).Rows(j).Item("COLNAME") + ","
                    If DtsTblDet.Tables(0).Rows(j).Item("COLNAME") = "CASEID" Then
                        Value = Value + "" + CaseId.ToString() + "" + ","
                    Else
                        Value = Value + DtsTblDet.Tables(0).Rows(j).Item("DEFAULTVAL") + ","
                    End If
                Next
                Column = Column.Remove(Column.Length - 1, 1)
                Value = Value.Remove(Value.Length - 1, 1)
                StrSql = "INSERT INTO " + DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString() + " "
                StrSql = StrSql + "(" + Column + ") "
                StrSql = StrSql + "VALUES "
                StrSql = StrSql + "(" + Value + ") "
                odbUtil.UpIns(StrSql, Connection)
                Column = ""
                Value = ""
            Next

            Return CaseId
        Catch ex As Exception
            Throw New Exception("ToolCCS:CreateCase:" + ex.Message.ToString())
            Return CaseId
        End Try
    End Function

    Public Sub CopyCase(ByVal SCaseId As String, ByVal TCaseId As String, ByVal Schema As String)
        Dim DtsTbl As New DataSet()
        Dim DtsTblDet As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Dim Column As String = String.Empty
        Dim i As New Integer
        Dim j As New Integer
        Dim Stbl As String = String.Empty
        Dim Ttbl As String = String.Empty
        Try
            'Copy Case Table Details
            DtsTbl = GetCopyCaseTable(Schema)
            For i = 0 To DtsTbl.Tables(0).Rows.Count - 1
                '------------------ Added by SagarJ for debugging purpose------------
                If DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString() = "EQUIP2ENERGYPREF" Then
                    Dim s As String
                    s = "Sagar"
                End If
                '-----------------------------------------------------------------------------------
                DtsTblDet = GetTableDetails(DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString(), Schema, False, True, False)
                For j = 0 To DtsTblDet.Tables(0).Rows.Count - 1
                    Column = Column + DtsTblDet.Tables(0).Rows(j).Item("COLNAME") + ","
                Next
                Column = Column.Remove(Column.Length - 1, 1)
                If DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString() = "BASECASES" Or DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString() = "PERMISSIONSCASES" Then
                    If SCaseId < 1000 Then
                        Stbl = "BASECASES"
                        Ttbl = "PERMISSIONSCASES"
                    Else
                        Stbl = "PERMISSIONSCASES"
                        Ttbl = "PERMISSIONSCASES"
                    End If
                    StrSql = "UPDATE " + Ttbl + "   "
                    StrSql = StrSql + "SET "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + Column + ","
                    StrSql = StrSql + "SERVERDATE "
                    StrSql = StrSql + ") "
                    StrSql = StrSql + "= "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + "SELECT  " + Column + ", "
                    StrSql = StrSql + "(SELECT SYSDATE FROM DUAL) "
                    StrSql = StrSql + "FROM " + Stbl + "   "
                    StrSql = StrSql + "WHERE CASEID = " + SCaseId + " "
                    StrSql = StrSql + " AND ROWNUM=1 ) "
                    StrSql = StrSql + "WHERE CASEID = " + TCaseId + " "
                    odbUtil.UpIns(StrSql, Connection)
                    Column = ""
                Else
                    Stbl = DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString()
                    Ttbl = DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString()

                    If DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString() = "USERSMATERIAL" Then

                        StrSql = "DELETE FROM " + Ttbl + " "
                        StrSql = StrSql + "WHERE CASEID = " + TCaseId + " "
                        odbUtil.UpIns(StrSql, Connection)

                        StrSql = "INSERT INTO " + Ttbl + "   "
                        StrSql = StrSql + "(CASEID,MATID,MATDES) "
                        StrSql = StrSql + "SELECT  " + TCaseId + " , MATID,MATDES "
                        StrSql = StrSql + "FROM " + Stbl + "   "
                        StrSql = StrSql + "WHERE CASEID = " + SCaseId + " "
                        odbUtil.UpIns(StrSql, Connection)
                    ElseIf DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString() = "USERSEQUIPMENT" Then

                        StrSql = "DELETE FROM " + Ttbl + " "
                        StrSql = StrSql + "WHERE CASEID = " + TCaseId + " "
                        odbUtil.UpIns(StrSql, Connection)

                        StrSql = "INSERT INTO " + Ttbl + "   "
                        StrSql = StrSql + "(CASEID,EQUIPID,EQUIPDES) "
                        StrSql = StrSql + "SELECT  " + TCaseId + " , EQUIPID,EQUIPDES "
                        StrSql = StrSql + "FROM " + Stbl + "   "
                        StrSql = StrSql + "WHERE CASEID = " + SCaseId + " "
                        odbUtil.UpIns(StrSql, Connection)
                    Else
                        StrSql = "UPDATE " + Ttbl + "   "
                        StrSql = StrSql + "SET "
                        StrSql = StrSql + "( "
                        StrSql = StrSql + Column
                        StrSql = StrSql + ") "
                        StrSql = StrSql + "= "
                        StrSql = StrSql + "( "
                        StrSql = StrSql + "SELECT  " + Column + " "
                        StrSql = StrSql + "FROM " + Stbl + "   "
                        StrSql = StrSql + "WHERE CASEID = " + SCaseId + " "
                        StrSql = StrSql + " AND ROWNUM=1 ) "
                        StrSql = StrSql + "WHERE CASEID = " + TCaseId + " "
                        odbUtil.UpIns(StrSql, Connection)
                    End If
                    Column = ""
                End If


                

            Next


        Catch ex As Exception
            Throw New Exception("ToolCCS:CopyCase:" + ex.Message.ToString())
        End Try
    End Sub

    Public Sub CaseSynch(ByVal SCaseId As String, ByVal TCaseId As String, ByVal SSchemaName As String, ByVal TSchemaName As String, ByVal SSchema As String, ByVal MSchema As String)
        Dim DtsTbl As New DataSet()
        Dim DtsTblDet As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(MSchema)
        Dim Column As String = String.Empty
        Dim i As New Integer
        Dim j As New Integer
        Dim Stbl As String = String.Empty
        Dim Ttbl As String = String.Empty
        Try
            'Copy Case Table Details
            DtsTbl = GetSyncCaseTable(SSchema)
            For i = 0 To DtsTbl.Tables(0).Rows.Count - 1
                DtsTblDet = GetTableDetails(DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString(), SSchema, False, False, True)
                For j = 0 To DtsTblDet.Tables(0).Rows.Count - 1
                    Column = Column + DtsTblDet.Tables(0).Rows(j).Item("COLNAME") + ","
                Next
                Column = Column.Remove(Column.Length - 1, 1)
                If DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString() = "BASECASES" Or DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString() = "PERMISSIONSCASES" Then
                    If SCaseId < 1000 Then
                        Stbl = "BASECASES"
                        Ttbl = "PERMISSIONSCASES"
                    Else
                        Stbl = "PERMISSIONSCASES"
                        Ttbl = "PERMISSIONSCASES"
                    End If

                    StrSql = "UPDATE " + TSchemaName + "." + Ttbl + "   "
                    StrSql = StrSql + "SET "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + Column + ","
                    StrSql = StrSql + "SERVERDATE "
                    StrSql = StrSql + ") "
                    StrSql = StrSql + "= "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + "SELECT  " + Column + ", "
                    StrSql = StrSql + "(SELECT SYSDATE FROM DUAL) "
                    StrSql = StrSql + "FROM " + SSchemaName + "." + Stbl + "   "
                    StrSql = StrSql + "WHERE CASEID = " + SCaseId + " "
                    StrSql = StrSql + " AND ROWNUM=1 ) "
                    StrSql = StrSql + "WHERE CASEID = " + TCaseId + " "
                    odbUtil.UpIns(StrSql, Connection)
                    Column = ""
                Else
                    Stbl = DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString()
                    Ttbl = DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString()

                    If DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString() = "USERSMATERIAL" Then
                        StrSql = "DELETE FROM " + TSchemaName + "." + Ttbl + " "
                        StrSql = StrSql + "WHERE CASEID = " + TCaseId + " "
                        odbUtil.UpIns(StrSql, Connection)

                        StrSql = "INSERT INTO " + TSchemaName + "." + Ttbl + " "
                        StrSql = StrSql + "(CASEID,MATID,MATDES) "
                        StrSql = StrSql + "SELECT  " + TCaseId + " , MATID,MATDES "
                        StrSql = StrSql + "FROM " + SSchemaName + "." + Stbl + " "
                        StrSql = StrSql + "WHERE CASEID = " + SCaseId + " "
                        odbUtil.UpIns(StrSql, Connection)
                    ElseIf DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString() = "USERSEQUIPMENT" Then

                        StrSql = "DELETE FROM " + Ttbl + " "
                        StrSql = StrSql + "WHERE CASEID = " + TCaseId + " "
                        odbUtil.UpIns(StrSql, Connection)

                        StrSql = "INSERT INTO " + Ttbl + "   "
                        StrSql = StrSql + "(CASEID,EQUIPID,EQUIPDES) "
                        StrSql = StrSql + "SELECT  " + TCaseId + " , EQUIPID,EQUIPDES "
                        StrSql = StrSql + "FROM " + Stbl + "   "
                        StrSql = StrSql + "WHERE CASEID = " + SCaseId + " "
                        odbUtil.UpIns(StrSql, Connection)
                    Else
                        StrSql = "UPDATE " + TSchemaName + "." + Ttbl + "   "
                        StrSql = StrSql + "SET "
                        StrSql = StrSql + "( "
                        StrSql = StrSql + Column
                        StrSql = StrSql + ") "
                        StrSql = StrSql + "= "
                        StrSql = StrSql + "( "
                        StrSql = StrSql + "SELECT  " + Column + " "
                        StrSql = StrSql + "FROM " + SSchemaName + "." + Stbl + "   "
                        StrSql = StrSql + "WHERE CASEID = " + SCaseId + " "
                        StrSql = StrSql + " AND ROWNUM=1 ) "
                        StrSql = StrSql + "WHERE CASEID = " + TCaseId + " "
                        odbUtil.UpIns(StrSql, Connection)
                    End If
                    Column = ""
                End If


               
            Next


        Catch ex As Exception
            Throw New Exception("ToolCCS:CopyCase:" + ex.Message.ToString())
        End Try
    End Sub

    Public Sub CaseRename(ByVal CaseId As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String, ByVal CaseDe3 As String, ByVal Schema As String)
        Dim odbUtil As New DBUtil()
        Dim ObjGetData As New E1GetData.Selectdata()
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Dim StrSql As String = String.Empty
        Try

            StrSql = "UPDATE PERMISSIONSCASES SET CASEDE1 ='" + CaseDe1 + "', CASEDE2 ='" + CaseDe2 + "', CASEDE3 ='" + CaseDe3 + "'  WHERE CASEID=" + CaseId.ToString() + " "
            odbUtil.UpIns(StrSql, Connection)

        Catch ex As Exception
            Throw New Exception("E1GetData:ExtrusionUpdate:" + ex.Message.ToString())
        End Try

    End Sub

    Public Sub CaseAccess(ByVal UserId As String, ByVal CaseId As String, ByVal Schema As String)
        Dim StrSql As String = String.Empty
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Dim odbUtil As New DBUtil()
        Try
            'Insert Into PermissionsCases
            StrSql = "INSERT INTO PERMISSIONSCASES  "
            StrSql = StrSql + "(SERVERDATE,CREATIONDATE, USERID, CASEID, CASEDE1 ,CASEDE2, CASEDE3) "
            StrSql = StrSql + "SELECT DISTINCT SYSDATE,CREATIONDATE," + UserId.ToString() + ",CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000)) FROM PERMISSIONSCASES WHERE CASEID=" + CaseId.ToString() + ""
            StrSql = StrSql + "AND NOT EXISTS (SELECT 1 "
            StrSql = StrSql + "FROM PERMISSIONSCASES "
            StrSql = StrSql + "WHERE CASEID = " + CaseId.ToString() + " "
            StrSql = StrSql + "AND USERID=" + UserId.ToString() + ") "

            odbUtil.UpIns(StrSql, Connection)
        Catch ex As Exception

        End Try
    End Sub

Public Sub CaseAccessSBA(ByVal UserId As String, ByVal CaseId As String, ByVal Schema As String)
        Dim StrSql As String = String.Empty
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Dim odbUtil As New DBUtil()
        Try
            'Insert Into PermissionsCases
            StrSql = "INSERT INTO PERMISSIONSCASES  "
            StrSql = StrSql + "(SERVERDATE,CREATIONDATE, USERID, CASEID, CASEDE1 ,CASEDE2, CASEDE3,APPLICATION) "
            StrSql = StrSql + "SELECT DISTINCT SYSDATE,CREATIONDATE," + UserId.ToString() + ",CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000)),APPLICATION FROM PERMISSIONSCASES WHERE CASEID=" + CaseId.ToString() + ""
            StrSql = StrSql + "AND NOT EXISTS (SELECT 1 "
            StrSql = StrSql + "FROM PERMISSIONSCASES "
            StrSql = StrSql + "WHERE CASEID = " + CaseId.ToString() + " "
            StrSql = StrSql + "AND USERID=" + UserId.ToString() + ") "

            odbUtil.UpIns(StrSql, Connection)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub CaseDelete(ByVal UserId As String, ByVal CaseId As String, ByVal Schema As String)
        Dim StrSql As String = String.Empty
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Dim odbUtil As New DBUtil()
        Try
            'Delete From PermissionsCases
            'StrSql = "DELETE FROM PERMISSIONSCASES WHERE CASEID=" + CaseId.ToString() + " AND UPPER(USERNAME)='" + UserName.ToUpper() + "' "
            StrSql = "DELETE FROM PERMISSIONSCASES WHERE CASEID=" + CaseId.ToString() + " AND USERID=" + UserId.ToString() + " "
            odbUtil.UpIns(StrSql, Connection)
			
			'Delete from UsersMaterial
            If Schema = "EconConnectionString" Or Schema = "Sustain1ConnectionString" Then
                StrSql = "DELETE FROM USERSMATERIAL WHERE CASEID=" + CaseId.ToString() + " "
                odbUtil.UpIns(StrSql, Connection)
            End If

            'Delete from USERSEQUIPMENT
            If Schema = "EconConnectionString" Then 'Or Schema = "Sustain1ConnectionString" Then
                StrSql = "DELETE FROM USERSEQUIPMENT WHERE CASEID=" + CaseId.ToString() + " "
                odbUtil.UpIns(StrSql, Connection)
            End If
            'Insert Into Deleted         
            'StrSql = "INSERT INTO DELETED  "
            'StrSql = StrSql + "SELECT '" + UserName.ToString() + "'," + CaseId.ToString() + ",SYSDATE FROM DUAL "
            StrSql = "INSERT INTO DELETED(USERID,CASEID,""DATE"")  "
            StrSql = StrSql + "SELECT " + UserId.ToString() + "," + CaseId.ToString() + ",SYSDATE FROM DUAL "
            odbUtil.UpIns(StrSql, Connection)


        Catch ex As Exception

        End Try
    End Sub

    Protected Function GetCreateCaseTable(ByVal Schema As String) As DataSet
        Dim Dts As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Try
            StrSql = "SELECT TABLENAME  "
            StrSql = StrSql + "FROM PAGEWISETABLEDETAILS "
            StrSql = StrSql + "WHERE ISCREATE = 'Y' "
            StrSql = StrSql + "GROUP BY TABLENAME "
            Dts = odbUtil.FillDataSet(StrSql, Connection)
            Return Dts
        Catch ex As Exception
            Throw New Exception("E1GetData:GetCreateCaseTable:" + ex.Message.ToString())
            Return Dts
        End Try
    End Function

    Protected Function GetCopyCaseTable(ByVal Schema As String) As DataSet
        Dim Dts As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Try
            StrSql = "SELECT TABLENAME  "
            StrSql = StrSql + "FROM PAGEWISETABLEDETAILS "
            StrSql = StrSql + "WHERE ISCOPY = 'Y' "
            StrSql = StrSql + "GROUP BY TABLENAME "
            Dts = odbUtil.FillDataSet(StrSql, Connection)
            Return Dts
        Catch ex As Exception
            Throw New Exception("E1GetData:GetCreateCaseTable:" + ex.Message.ToString())
            Return Dts
        End Try
    End Function

    Protected Function GetSyncCaseTable(ByVal Schema As String) As DataSet
        Dim Dts As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Try
            StrSql = "SELECT TABLENAME  "
            StrSql = StrSql + "FROM PAGEWISETABLEDETAILS "
            StrSql = StrSql + "WHERE ISSYNCH = 'Y' "
            StrSql = StrSql + "GROUP BY TABLENAME "
            Dts = odbUtil.FillDataSet(StrSql, Connection)
            Return Dts
        Catch ex As Exception
            Throw New Exception("E1GetData:GetCreateCaseTable:" + ex.Message.ToString())
            Return Dts
        End Try
    End Function

    Protected Function GetTableDetails(ByVal Table As String, ByVal Schema As String, ByVal IsCreate As Boolean, ByVal IsCopy As Boolean, ByVal IsSynch As Boolean) As DataSet
        Dim Dts As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Try
            StrSql = "SELECT COLNAME,DEFAULTVAL FROM PAGEWISETABLEDETAILS "
            If (IsCreate) Then
                StrSql = StrSql + "WHERE ISCREATE = 'Y' "
            ElseIf (IsCopy) Then
                StrSql = StrSql + "WHERE ISCOPY = 'Y' "
            ElseIf (IsSynch) Then
                StrSql = StrSql + "WHERE ISSYNCH = 'Y' "
            End If
            StrSql = StrSql + "AND TABLENAME = '" + Table + "' "
            StrSql = StrSql + "ORDER BY PAGEWISETABLEDETAILID "
            Dts = odbUtil.FillDataSet(StrSql, Connection)
            Return Dts
        Catch ex As Exception
            Throw New Exception("E1GetData:GetCreateCaseTable:" + ex.Message.ToString())
            Return Dts
        End Try
    End Function

    Protected Function GetCaseId(ByVal Schema As String) As Integer
        Dim CaseId As New Integer
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim Connection As String = String.Empty
        If Schema = "E1" Then
            Connection = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
        ElseIf Schema = "E2" Then
            Connection = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
        ElseIf Schema = "SC1" Then
            Connection = System.Configuration.ConfigurationManager.AppSettings("Schem1ConnectionString")
        ElseIf Schema = "EDist" Then
            Connection = System.Configuration.ConfigurationManager.AppSettings("DistributionConnectionString")
        ElseIf Schema = "RETL" Then
            Connection = System.Configuration.ConfigurationManager.AppSettings("RetailConnectionString")
        ElseIf Schema = "Echem1" Then
            Connection = System.Configuration.ConfigurationManager.AppSettings("Echem1ConnectionString")
        ElseIf Schema = "SBA" Then
            Connection = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
        ElseIf Schema = "MEDE1" Then
            Connection = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
        ElseIf Schema = "MEDE2" Then
            Connection = System.Configuration.ConfigurationManager.AppSettings("MedEcon2ConnectionString")
		ElseIf Schema = "MoldE1" Then
            Connection = System.Configuration.ConfigurationManager.AppSettings("MoldE1ConnectionString")
        ElseIf Schema = "MoldE2" Then
            Connection = System.Configuration.ConfigurationManager.AppSettings("MoldE2ConnectionString")
        End If


        Dim Dts As New DataSet()
        Try
            StrSql = "SELECT SEQCASEID.NEXTVAL AS CASEID FROM DUAL"
            Dts = odbUtil.FillDataSet(StrSql, Connection)
            CaseId = CInt(Dts.Tables(0).Rows(0).Item("CaseId").ToString())
            Return CaseId
        Catch ex As Exception
            Throw New Exception("E1GetData:GetExtrusionDetails:" + ex.Message.ToString())
            Return CaseId
        End Try
    End Function
    Public Sub CopyBaseCase(ByVal SCaseId As String, ByVal TCaseId As String, ByVal Schema As String)
        Dim DtsTbl As New DataSet()
        Dim DtsTblDet As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Dim Column As String = String.Empty
        Dim i As New Integer
        Dim j As New Integer
        Dim Stbl As String = String.Empty
        Dim Ttbl As String = String.Empty
        Try
            'Copy Case Table Details
            DtsTbl = GetCopyCaseTable(Schema)
            For i = 0 To DtsTbl.Tables(0).Rows.Count - 1
                DtsTblDet = GetTableDetails(DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString(), Schema, False, True, False)
                For j = 0 To DtsTblDet.Tables(0).Rows.Count - 1
                    Column = Column + DtsTblDet.Tables(0).Rows(j).Item("COLNAME") + ","
                Next
                Column = Column.Remove(Column.Length - 1, 1)
                If DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString() = "BASECASES" Or DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString() = "PERMISSIONSCASES" Then
                    Stbl = "BASECASES"
                    Ttbl = "BASECASES"

                    StrSql = "UPDATE " + Ttbl + "   "
                    StrSql = StrSql + "SET "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + Column + ","
                    StrSql = StrSql + "SERVERDATE "
                    StrSql = StrSql + ") "
                    StrSql = StrSql + "= "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + "SELECT  " + Column + ", "
                    StrSql = StrSql + "(SELECT SYSDATE FROM DUAL) "
                    StrSql = StrSql + "FROM " + Stbl + "   "
                    StrSql = StrSql + "WHERE CASEID = " + SCaseId + " "
                    StrSql = StrSql + " AND ROWNUM=1 ) "
                    StrSql = StrSql + "WHERE CASEID = " + TCaseId + " "
                    odbUtil.UpIns(StrSql, Connection)
                    Column = ""
                Else
                    Stbl = DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString()
                    Ttbl = DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString()

                    StrSql = "UPDATE " + Ttbl + "   "
                    StrSql = StrSql + "SET "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + Column
                    StrSql = StrSql + ") "
                    StrSql = StrSql + "= "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + "SELECT  " + Column + " "
                    StrSql = StrSql + "FROM " + Stbl + "   "
                    StrSql = StrSql + "WHERE CASEID = " + SCaseId + " "
                    StrSql = StrSql + " AND ROWNUM=1 ) "
                    StrSql = StrSql + "WHERE CASEID = " + TCaseId + " "
                    odbUtil.UpIns(StrSql, Connection)
                    Column = ""
                End If


               

            Next


        Catch ex As Exception
            Throw New Exception("ToolCCS:CopyCase:" + ex.Message.ToString())
        End Try
    End Sub
    Public Sub BaseCaseSynch(ByVal SCaseId As String, ByVal TCaseId As String, ByVal SSchemaName As String, ByVal TSchemaName As String, ByVal SSchema As String, ByVal MSchema As String)
        Dim DtsTbl As New DataSet()
        Dim DtsTblDet As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(MSchema)
        Dim Column As String = String.Empty
        Dim i As New Integer
        Dim j As New Integer
        Dim Stbl As String = String.Empty
        Dim Ttbl As String = String.Empty
        Try
            'Copy Case Table Details
            DtsTbl = GetSyncCaseTable(SSchema)
            For i = 0 To DtsTbl.Tables(0).Rows.Count - 1
                DtsTblDet = GetTableDetails(DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString(), SSchema, False, False, True)
                For j = 0 To DtsTblDet.Tables(0).Rows.Count - 1
                    Column = Column + DtsTblDet.Tables(0).Rows(j).Item("COLNAME") + ","
                Next
                Column = Column.Remove(Column.Length - 1, 1)
                If DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString() = "BASECASES" Or DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString() = "PERMISSIONSCASES" Then
                    If SCaseId < 1000 Then
                        Stbl = "BASECASES"
                        Ttbl = "BASECASES"
                    Else
                        Stbl = "PERMISSIONSCASES"
                        Ttbl = "PERMISSIONSCASES"
                    End If

                    StrSql = "UPDATE " + TSchemaName + "." + Ttbl + "   "
                    StrSql = StrSql + "SET "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + Column + ","
                    StrSql = StrSql + "SERVERDATE "
                    StrSql = StrSql + ") "
                    StrSql = StrSql + "= "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + "SELECT  " + Column + ", "
                    StrSql = StrSql + "(SELECT SYSDATE FROM DUAL) "
                    StrSql = StrSql + "FROM " + SSchemaName + "." + Stbl + "   "
                    StrSql = StrSql + "WHERE CASEID = " + SCaseId + " "
                    StrSql = StrSql + " AND ROWNUM=1 ) "
                    StrSql = StrSql + "WHERE CASEID = " + TCaseId + " "
                    odbUtil.UpIns(StrSql, Connection)
                    Column = ""
                Else
                    Stbl = DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString()
                    Ttbl = DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString()

                    StrSql = "UPDATE " + TSchemaName + "." + Ttbl + "   "
                    StrSql = StrSql + "SET "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + Column
                    StrSql = StrSql + ") "
                    StrSql = StrSql + "= "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + "SELECT  " + Column + " "
                    StrSql = StrSql + "FROM " + SSchemaName + "." + Stbl + "   "
                    StrSql = StrSql + "WHERE CASEID = " + SCaseId + " "
                    StrSql = StrSql + " AND ROWNUM=1 ) "
                    StrSql = StrSql + "WHERE CASEID = " + TCaseId + " "
                    odbUtil.UpIns(StrSql, Connection)
                    Column = ""
                End If


                
            Next


        Catch ex As Exception
            Throw New Exception("ToolCCS:CopyCase:" + ex.Message.ToString())
        End Try
    End Sub
    Public Sub CaseRenameBase(ByVal CaseId As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String, ByVal CaseDe3 As String, ByVal Schema As String)
        Dim odbUtil As New DBUtil()
        Dim ObjGetData As New E1GetData.Selectdata()
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Dim StrSql As String = String.Empty
        Try

            StrSql = "UPDATE BASECASES SET CASEDE1 ='" + CaseDe1 + "', CASEDE2 ='" + CaseDe2 + "', CASEDE3 ='" + CaseDe3 + "'  WHERE CASEID=" + CaseId.ToString() + " "
            odbUtil.UpIns(StrSql, Connection)

        Catch ex As Exception
            Throw New Exception("E1GetData:ExtrusionUpdate:" + ex.Message.ToString())
        End Try

    End Sub
    Public Sub CaseBaseDelete(ByVal UserId As String, ByVal CaseId As String, ByVal Schema As String)
        Dim StrSql As String = String.Empty
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Dim odbUtil As New DBUtil()
        Try
            'Delete From PermissionsCases
            StrSql = "DELETE FROM BASECASES WHERE CASEID=" + CaseId.ToString() + " "
            odbUtil.UpIns(StrSql, Connection)

            'Insert Into Deleted
            'StrSql = "INSERT INTO DELETED  "
            'StrSql = StrSql + "SELECT '" + UserName.ToString() + "'," + CaseId.ToString() + ",SYSDATE FROM DUAL "
            StrSql = "INSERT INTO DELETED(USERID,CASEID,""DATE"")  "
            StrSql = StrSql + "SELECT " + UserId.ToString() + "," + CaseId.ToString() + ",SYSDATE FROM DUAL "
            odbUtil.UpIns(StrSql, Connection)


        Catch ex As Exception

        End Try
    End Sub
    Public Function CreateBaseCase(ByVal Schema As String, ByVal PSchema As String, ByVal CaseId As Integer) As Integer
        Dim DtsTbl As New DataSet()
        Dim DtsTblDet As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim Column As String = String.Empty
        Dim Value As String = String.Empty
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Dim i As New Integer
        Dim j As New Integer

        Try
            'Create Case Table Details
            DtsTbl = GetCreateCaseTable(Schema)
            'Case Id 
            If CaseId = 0 Then
                CaseId = GetBaseCaseId(PSchema)
            End If


            'Insert Into PermissionsCases
            StrSql = "INSERT INTO BASECASES  "
            StrSql = StrSql + "(SERVERDATE,CREATIONDATE, CASEID) "
            StrSql = StrSql + "VALUES "
            StrSql = StrSql + "(SYSDATE,SYSDATE," + CaseId.ToString() + ") "
            odbUtil.UpIns(StrSql, Connection)

            'Insert In other Tables
            For i = 0 To DtsTbl.Tables(0).Rows.Count - 1
                DtsTblDet = GetTableDetails(DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString(), Schema, True, False, False)
                For j = 0 To DtsTblDet.Tables(0).Rows.Count - 1
                    Column = Column + DtsTblDet.Tables(0).Rows(j).Item("COLNAME") + ","
                    If DtsTblDet.Tables(0).Rows(j).Item("COLNAME") = "CASEID" Then
                        Value = Value + "" + CaseId.ToString() + "" + ","
                    Else
                        Value = Value + DtsTblDet.Tables(0).Rows(j).Item("DEFAULTVAL") + ","
                    End If
                Next
                Column = Column.Remove(Column.Length - 1, 1)
                Value = Value.Remove(Value.Length - 1, 1)
                StrSql = "INSERT INTO " + DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString() + " "
                StrSql = StrSql + "(" + Column + ") "
                StrSql = StrSql + "VALUES "
                StrSql = StrSql + "(" + Value + ") "
                odbUtil.UpIns(StrSql, Connection)
                Column = ""
                Value = ""
            Next

            Return CaseId
        Catch ex As Exception
            Throw New Exception("ToolCCS:CreateCase:" + ex.Message.ToString())
            Return CaseId
        End Try
    End Function
    Protected Function GetBaseCaseId(ByVal Schema As String) As Integer
        Dim CaseId As New Integer
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim Connection As String = String.Empty
        If Schema = "E1" Then
            Connection = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
        ElseIf Schema = "E2" Then
            Connection = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")
        ElseIf Schema = "MEDE1" Then
            Connection = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
        ElseIf Schema = "MEDE2" Then
            Connection = System.Configuration.ConfigurationManager.AppSettings("MedEcon2ConnectionString")
        ElseIf Schema = "SC1" Then
            Connection = System.Configuration.ConfigurationManager.AppSettings("Schem1ConnectionString")
        ElseIf Schema = "EDIST" Then
            Connection = System.Configuration.ConfigurationManager.AppSettings("DistributionConnectionString")
        ElseIf Schema = "RETL" Then
            Connection = System.Configuration.ConfigurationManager.AppSettings("RetailConnectionString")
        ElseIf Schema = "EDist" Then
            Connection = System.Configuration.ConfigurationManager.AppSettings("DistributionConnectionString")
        ElseIf Schema = "Echem1" Then
            Connection = System.Configuration.ConfigurationManager.AppSettings("Echem1ConnectionString")
        ElseIf Schema = "SBA" Then
            Connection = System.Configuration.ConfigurationManager.AppSettings("SBAConnectionString")
		ElseIf Schema = "MoldE1" Then
            Connection = System.Configuration.ConfigurationManager.AppSettings("MoldE1ConnectionString")
        ElseIf Schema = "MoldE2" Then
            Connection = System.Configuration.ConfigurationManager.AppSettings("MoldE2ConnectionString")
        End If


        Dim Dts As New DataSet()
        Try
            StrSql = "SELECT SEQBCASEID.NEXTVAL AS CASEID FROM DUAL"
            Dts = odbUtil.FillDataSet(StrSql, Connection)
            CaseId = CInt(Dts.Tables(0).Rows(0).Item("CaseId").ToString())
            Return CaseId
        Catch ex As Exception
            Throw New Exception("ToolCCS:GetBaseCaseId:" + ex.Message.ToString())
            Return CaseId
        End Try
    End Function

#Region "Group Delete"
    Protected Function GetGroupDetail(ByVal UserID As String, ByVal Schema As String) As DataSet
        Dim Dts As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Try
            StrSql = "SELECT GROUPID  "
            StrSql = StrSql + "FROM "
            StrSql = StrSql + "GROUPS "
            StrSql = StrSql + "WHERE USERID= " + UserID + " "
            Dts = odbUtil.FillDataSet(StrSql, Connection)
            Return Dts
        Catch ex As Exception
            Throw New Exception("ToolCCS:GetGroupDetail:" + ex.Message.ToString())
            Return Dts
        End Try
    End Function
    Public Sub GroupCaseDelete(ByVal UserID As String, ByVal CaseId As String, ByVal Schema As String)
        Dim StrSql As String = String.Empty
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Dim odbUtil As New DBUtil()
        Dim ds As New DataSet()
        Dim i As Integer = 0
        Dim strGroupIDs As String = String.Empty
        Try
            'Getting Group Details for User
            ds = GetGroupDetail(UserID, Schema)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If i = 0 Then
                        strGroupIDs = ds.Tables(0).Rows(i).Item("GROUPID").ToString()
                    Else
                        strGroupIDs = strGroupIDs + "," + ds.Tables(0).Rows(i).Item("GROUPID").ToString()
                    End If

                Next

                'Delete From GroupCASES table
                StrSql = "DELETE FROM GROUPCASES  "
                StrSql = StrSql + "WHERE GROUPID IN (" + strGroupIDs + ") "
                StrSql = StrSql + "AND CASEID=" + CaseId + " "
                odbUtil.UpIns(StrSql, Connection)
            End If
            odbUtil.UpIns(StrSql, Connection)
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Structure Assistant"

 'Changes started for Company Library'
    Public Function CreateCompCase(ByVal UserId As String, ByVal LicId As String, ByVal Schema As String, ByVal PSchema As String, ByVal CaseId As Integer) As Integer
        Dim DtsTbl As New DataSet()
        Dim DtsTblDet As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim Column As String = String.Empty
        Dim Value As String = String.Empty
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Dim i As New Integer
        Dim j As New Integer

        Try
            'Create Case Table Details
            DtsTbl = GetCreateCaseTable(Schema)
            'Case Id 
            If CaseId = 0 Then
                CaseId = GetCaseId(PSchema)
            End If


            'Insert Into PermissionsCases
            StrSql = "INSERT INTO COMPANYCASES  "
            StrSql = StrSql + "(SERVERDATE,CREATIONDATE, USERID, CASEID,LICENSEID) "
            StrSql = StrSql + "VALUES "
            StrSql = StrSql + "(SYSDATE,SYSDATE," + UserId.ToString() + "," + CaseId.ToString() + "," + LicId.ToString() + ") "
            odbUtil.UpIns(StrSql, Connection)

            'Insert In other Tables
            For i = 0 To DtsTbl.Tables(0).Rows.Count - 1
                DtsTblDet = GetTableDetails(DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString(), Schema, True, False, False)
                For j = 0 To DtsTblDet.Tables(0).Rows.Count - 1
                    Column = Column + DtsTblDet.Tables(0).Rows(j).Item("COLNAME") + ","
                    If DtsTblDet.Tables(0).Rows(j).Item("COLNAME") = "CASEID" Then
                        Value = Value + "" + CaseId.ToString() + "" + ","
                    Else
                        Value = Value + DtsTblDet.Tables(0).Rows(j).Item("DEFAULTVAL") + ","
                    End If
                Next
                Column = Column.Remove(Column.Length - 1, 1)
                Value = Value.Remove(Value.Length - 1, 1)
                StrSql = "INSERT INTO " + DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString() + " "
                StrSql = StrSql + "(" + Column + ") "
                StrSql = StrSql + "VALUES "
                StrSql = StrSql + "(" + Value + ") "
                odbUtil.UpIns(StrSql, Connection)
                Column = ""
                Value = ""
            Next

            Return CaseId
        Catch ex As Exception
            Throw New Exception("ToolCCS:CreateCompCase:" + ex.Message.ToString())
            Return CaseId
        End Try
    End Function
    Protected Function GetCompGroupDetail(ByVal Schema As String) As DataSet
        Dim Dts As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Try
            StrSql = "SELECT GROUPID  "
            StrSql = StrSql + "FROM "
            StrSql = StrSql + "GROUPS "
            StrSql = StrSql + "WHERE GROUPTYPE= 'CPROP' "
            Dts = odbUtil.FillDataSet(StrSql, Connection)
            Return Dts
        Catch ex As Exception
            Throw New Exception("ToolCCS:GetCompGroupDetail:" + ex.Message.ToString())
            Return Dts
        End Try
    End Function
    Public Sub CompGroupCaseDelete(ByVal CaseId As String, ByVal Schema As String)
        Dim StrSql As String = String.Empty
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Dim odbUtil As New DBUtil()
        Dim ds As New DataSet()
        Dim i As Integer = 0
        Dim strGroupIDs As String = String.Empty
        Try
            'Getting Group Details for Company
            ds = GetCompGroupDetail(Schema)
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If i = 0 Then
                        strGroupIDs = ds.Tables(0).Rows(i).Item("GROUPID").ToString()
                    Else
                        strGroupIDs = strGroupIDs + "," + ds.Tables(0).Rows(i).Item("GROUPID").ToString()
                    End If
                Next
                'Delete From GroupCASES table
                StrSql = "DELETE FROM GROUPCASES  "
                StrSql = StrSql + "WHERE GROUPID IN (" + strGroupIDs + ") "
                StrSql = StrSql + "AND CASEID=" + CaseId + " "
                odbUtil.UpIns(StrSql, Connection)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Public Sub CaseCompDelete(ByVal UserId As String, ByVal CaseId As String, ByVal Schema As String)
        Dim StrSql As String = String.Empty
        Dim Dts As New DataSet()
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Dim odbUtil As New DBUtil()
        Try
            'Delete From CompanyCases
            StrSql = "DELETE FROM COMPANYCASES WHERE CASEID=" + CaseId.ToString() + " "
            odbUtil.UpIns(StrSql, Connection)

            'Insert Into Deleted
            'StrSql = "INSERT INTO DELETED  "
            'StrSql = StrSql + "SELECT '" + UserName.ToString() + "'," + CaseId.ToString() + ",SYSDATE FROM DUAL "
            StrSql = "INSERT INTO DELETED(USERID,CASEID,""DATE"")  "
            StrSql = StrSql + "SELECT " + UserId.ToString() + "," + CaseId.ToString() + ",SYSDATE FROM DUAL "

            odbUtil.UpIns(StrSql, Connection)
        Catch ex As Exception

        End Try
    End Sub
    Public Sub CaseCompRename(ByVal CaseId As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String, ByVal CaseDe3 As String, ByVal Application As String, ByVal Schema As String)
        Dim odbUtil As New DBUtil()
        Dim ObjGetData As New E1GetData.Selectdata()
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Dim StrSql As String = String.Empty
        Try

            StrSql = "UPDATE COMPANYCASES SET CASEDE1 ='" + CaseDe1 + "', CASEDE2 ='" + CaseDe2 + "', "
            StrSql = StrSql + "CASEDE3 ='" + CaseDe3 + "',APPLICATION='" + Application + "'  WHERE CASEID=" + CaseId.ToString() + ""
            odbUtil.UpIns(StrSql, Connection)

        Catch ex As Exception
            Throw New Exception("SAGetData:CaseCompRename:" + ex.Message.ToString())
        End Try

    End Sub
    Public Sub CopyCompAdminCase(ByVal SCaseId As String, ByVal TCaseId As String, ByVal Schema As String)
        Dim DtsTbl As New DataSet()
        Dim DtsTblDet As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Dim Column As String = String.Empty
        Dim i As New Integer
        Dim j As New Integer
        Dim Stbl As String = String.Empty
        Dim Ttbl As String = String.Empty
        Dim dsType As New DataSet
        Try
            'Getting Casetype
            StrSql = "SELECT CASEID,TYPE  "
            StrSql = StrSql + "FROM "
            StrSql = StrSql + "( "
            StrSql = StrSql + "SELECT CASEID,'COMP' TYPE FROM COMPANYCASES "
            StrSql = StrSql + "UNION ALL "
            StrSql = StrSql + "SELECT CASEID,'PROP' TYPE FROM PERMISSIONSCASES "
            StrSql = StrSql + ") "
            StrSql = StrSql + "WHERE CASEID=" + SCaseId + " "
            dsType = odbUtil.FillDataSet(StrSql, Connection)
            StrSql = String.Empty

            'Copy Case Table Details
            DtsTbl = GetCopyCaseTable(Schema)
            For i = 0 To DtsTbl.Tables(0).Rows.Count - 1
                DtsTblDet = GetTableDetails(DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString(), Schema, False, True, False)
                For j = 0 To DtsTblDet.Tables(0).Rows.Count - 1
                    Column = Column + DtsTblDet.Tables(0).Rows(j).Item("COLNAME") + ","
                Next
                Column = Column.Remove(Column.Length - 1, 1)
                If DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString() = "BASECASES" Or DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString() = "PERMISSIONSCASES" Or DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString() = "COMPANYCASES" Then

                    If Schema = "SBAConnectionString" Then
                        If dsType.Tables(0).Rows(0).Item("TYPE").ToString() = "COMP" Then
                            Stbl = "COMPANYCASES"
                            Ttbl = "COMPANYCASES"
                        ElseIf dsType.Tables(0).Rows(0).Item("TYPE").ToString() = "PROP" Then
                            Stbl = "PERMISSIONSCASES"
                            Ttbl = "COMPANYCASES"
                        End If
                    End If

                    StrSql = "UPDATE " + Ttbl + "   "
                    StrSql = StrSql + "SET "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + Column + ","
                    StrSql = StrSql + "SERVERDATE) "
                    StrSql = StrSql + "= "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + "SELECT  " + Column + ", "
                    StrSql = StrSql + "(SELECT SYSDATE FROM DUAL) "
                    StrSql = StrSql + "FROM " + Stbl + "   "
                    StrSql = StrSql + "WHERE CASEID = " + SCaseId + " "
                    StrSql = StrSql + " AND ROWNUM=1 ) "
                    StrSql = StrSql + "WHERE CASEID = " + TCaseId + " "
                    odbUtil.UpIns(StrSql, Connection)
                    Column = ""
                Else
                    Stbl = DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString()
                    Ttbl = DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString()

                    StrSql = "UPDATE " + Ttbl + "   "
                    StrSql = StrSql + "SET "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + Column
                    StrSql = StrSql + ") "
                    StrSql = StrSql + "= "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + "SELECT  " + Column + " "
                    StrSql = StrSql + "FROM " + Stbl + "   "
                    StrSql = StrSql + "WHERE CASEID = " + SCaseId + " "
                    StrSql = StrSql + " AND ROWNUM=1 ) "
                    StrSql = StrSql + "WHERE CASEID = " + TCaseId + " "
                    odbUtil.UpIns(StrSql, Connection)
                    Column = ""
                End If
            Next
        Catch ex As Exception
            Throw New Exception("ToolCCS:CopyCompAdminCase:" + ex.Message.ToString())
        End Try
    End Sub
    'Changes ended for Company Library'

    Public Sub CaseRenameBaseStruct(ByVal CaseId As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String, ByVal CaseDe3 As String, ByVal Application As String, ByVal SMessage As String, ByVal SponsorId As String, ByVal Schema As String)
        Dim odbUtil As New DBUtil()
        Dim ObjGetData As New E1GetData.Selectdata()
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Dim StrSql As String = String.Empty
        Try
            StrSql = "UPDATE BASECASES SET CASEDE1 ='" + CaseDe1 + "', CASEDE2 ='" + CaseDe2 + "', CASEDE3 =REPLACE(REPLACE('" + CaseDe3 + "',chr(13)),chr(10),' ')" + ",FILENAME='" + SMessage + "', "
            StrSql = StrSql + "APPLICATION='" + Application + "',SUPPLIERID=" + SponsorId + " WHERE CASEID=" + CaseId.ToString() + ""
            odbUtil.UpIns(StrSql, Connection)

        Catch ex As Exception
            Throw New Exception("E1GetData:CaseRenameBase:" + ex.Message.ToString())
        End Try

    End Sub
    Public Sub GroupRenameBaseStruct(ByVal GroupId As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String, ByVal CaseDe3 As String, ByVal Application As String, ByVal SPMessage As String, ByVal SponsorId As String, ByVal Schema As String)
        Dim odbUtil As New DBUtil()
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Dim StrSql As String = String.Empty
        Try
            StrSql = "UPDATE GROUPS SET DES1 ='" + CaseDe1 + "', DES2 ='" + CaseDe2 + "', DES3 ='" + CaseDe3 + "',FILENAME='" + SPMessage + "', "
            StrSql = StrSql + "APPLICATION='" + Application + "',SUPPLIERID=" + SponsorId + " WHERE GROUPID=" + GroupId.ToString() + ""
            odbUtil.UpIns(StrSql, Connection)

        Catch ex As Exception
            Throw New Exception("StructRenameBase:" + ex.Message.ToString())
        End Try

    End Sub

    Public Sub GroupRenamePropStruct(ByVal GroupId As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String, ByVal CaseDe3 As String, ByVal Application As String, ByVal Schema As String)
        Dim odbUtil As New DBUtil()
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Dim StrSql As String = String.Empty
        Try
            StrSql = "UPDATE GROUPS SET DES1 ='" + CaseDe1 + "', DES2 ='" + CaseDe2 + "', DES3 ='" + CaseDe3 + "', "
            StrSql = StrSql + "APPLICATION='" + Application + "' WHERE GROUPID=" + GroupId.ToString() + ""
            odbUtil.UpIns(StrSql, Connection)

        Catch ex As Exception
            Throw New Exception("StructRenameBase:" + ex.Message.ToString())
        End Try

    End Sub
    Public Sub CasePropRename(ByVal CaseId As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String, ByVal CaseDe3 As String, ByVal Application As String, ByVal Schema As String)
        Dim odbUtil As New DBUtil()
        Dim ObjGetData As New E1GetData.Selectdata()
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Dim StrSql As String = String.Empty
        Try
            StrSql = "UPDATE PERMISSIONSCASES SET CASEDE1 ='" + CaseDe1 + "', CASEDE2 ='" + CaseDe2 + "', CASEDE3 =REPLACE(REPLACE('" + CaseDe3 + "',chr(13)),chr(10),' ')" + ",APPLICATION ='" + Application + "'  WHERE CASEID=" + CaseId.ToString() + " "
            odbUtil.UpIns(StrSql, Connection)

        Catch ex As Exception
            Throw New Exception("E1GetData:CaseRename:" + ex.Message.ToString())
        End Try
    End Sub

Public Sub CopyCaseSBA(ByVal SCaseId As String, ByVal TCaseId As String, ByVal Schema As String)
        Dim DtsTbl As New DataSet()
        Dim DtsTblDet As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Dim Column As String = String.Empty
        Dim i As New Integer
        Dim j As New Integer
        Dim Stbl As String = String.Empty
        Dim Ttbl As String = String.Empty
        Try
            'Copy Case Table Details
            DtsTbl = GetCopyCaseTable(Schema)
            For i = 0 To DtsTbl.Tables(0).Rows.Count - 1
                '------------------ Added by SagarJ for debugging purpose------------
                If DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString() = "EQUIP2ENERGYPREF" Then
                    Dim s As String
                    s = "Sagar"
                End If
                '-----------------------------------------------------------------------------------
                DtsTblDet = GetTableDetails(DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString(), Schema, False, True, False)
                For j = 0 To DtsTblDet.Tables(0).Rows.Count - 1
                    Column = Column + DtsTblDet.Tables(0).Rows(j).Item("COLNAME") + ","
                Next
                Column = Column.Remove(Column.Length - 1, 1)
                If DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString() = "BASECASES" Or DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString() = "PERMISSIONSCASES" Then
                    If SCaseId < 10000 Then
                        Stbl = "BASECASES"
                        Ttbl = "PERMISSIONSCASES"
                    Else
                        Stbl = "PERMISSIONSCASES"
                        Ttbl = "PERMISSIONSCASES"
                    End If
                    StrSql = "UPDATE " + Ttbl + "   "
                    StrSql = StrSql + "SET "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + Column + ","
                    StrSql = StrSql + "SERVERDATE "
                    StrSql = StrSql + ") "
                    StrSql = StrSql + "= "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + "SELECT  " + Column + ", "
                    StrSql = StrSql + "(SELECT SYSDATE FROM DUAL) "
                    StrSql = StrSql + "FROM " + Stbl + "   "
                    StrSql = StrSql + "WHERE CASEID = " + SCaseId + " "
                    StrSql = StrSql + " AND ROWNUM=1 ) "
                    StrSql = StrSql + "WHERE CASEID = " + TCaseId + " "
                    odbUtil.UpIns(StrSql, Connection)
                    Column = ""
                Else
                    Stbl = DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString()
                    Ttbl = DtsTbl.Tables(0).Rows(i).Item("TABLENAME").ToString()

                    StrSql = "UPDATE " + Ttbl + "   "
                    StrSql = StrSql + "SET "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + Column
                    StrSql = StrSql + ") "
                    StrSql = StrSql + "= "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + "SELECT  " + Column + " "
                    StrSql = StrSql + "FROM " + Stbl + "   "
                    StrSql = StrSql + "WHERE CASEID = " + SCaseId + " "
                    StrSql = StrSql + " AND ROWNUM=1 ) "
                    StrSql = StrSql + "WHERE CASEID = " + TCaseId + " "
                    odbUtil.UpIns(StrSql, Connection)
                    Column = ""
                End If




            Next


        Catch ex As Exception
            Throw New Exception("ToolCCS:CopyCase:" + ex.Message.ToString())
        End Try
    End Sub

Public Sub CopyBlendMat(ByVal SCaseId As String, ByVal TCaseId As String, ByVal Schema As String)
        Dim StrSql As String = String.Empty
        Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
        Dim odbUtil As New DBUtil()
        Try
            'Delete from BlendMatInput
            StrSql = "DELETE FROM BLENDMATINPUT  "
            StrSql = StrSql + "WHERE CASEID= " + TCaseId.ToString()
            odbUtil.UpIns(StrSql, Connection)

            StrSql = String.Empty
            'Insert Into PermissionsCases
            StrSql = "INSERT INTO BLENDMATINPUT  "
            StrSql = StrSql + "(CASEID,TYPEID,BCM1,BCM2,BCM3,BCM4,BCM5,BCM6,BCM7,BCM8,BCM9,BCM10,BCSG1,BCSG2,BCSG3,BCSG4,BCSG5,BCSG6,BCSG7,BCSG8,BCSG9,BCSG10, "
            StrSql = StrSql + "BCT1,BCT2,BCT3,BCT4,BCT5,BCT6,BCT7,BCT8,BCT9,BCT10,BCOTR1,BCOTR2,BCOTR3,BCOTR4,BCOTR5,BCOTR6,BCOTR7,BCOTR8,BCOTR9,BCOTR10, "
            StrSql = StrSql + "BCWVTR1,BCWVTR2,BCWVTR3,BCWVTR4,BCWVTR5,BCWVTR6,BCWVTR7,BCWVTR8,BCWVTR9,BCWVTR10,"
            StrSql = StrSql + "BCGRADE1,BCGRADE2,BCGRADE3,BCGRADE4,BCGRADE5,BCGRADE6,BCGRADE7,BCGRADE8,BCGRADE9,BCGRADE10, "
            StrSql = StrSql + "BCTS1VAL1,BCTS1VAL2,BCTS1VAL3,BCTS1VAL4,BCTS1VAL5,BCTS1VAL6,BCTS1VAL7,BCTS1VAL8,BCTS1VAL9,BCTS1VAL10,"
            StrSql = StrSql + "BCTS2VAL1,BCTS2VAL2,BCTS2VAL3,BCTS2VAL4,BCTS2VAL5,BCTS2VAL6,BCTS2VAL7,BCTS2VAL8,BCTS2VAL9,BCTS2VAL10,TYPEM)"
            StrSql = StrSql + "SELECT " + TCaseId.ToString() + ",TYPEID,BCM1,BCM2,BCM3,BCM4,BCM5,BCM6,BCM7,BCM8,BCM9,BCM10,BCSG1,BCSG2,BCSG3,BCSG4,BCSG5,BCSG6,BCSG7,BCSG8,BCSG9,BCSG10, "
            StrSql = StrSql + "BCT1,BCT2,BCT3,BCT4,BCT5,BCT6,BCT7,BCT8,BCT9,BCT10,BCOTR1,BCOTR2,BCOTR3,BCOTR4,BCOTR5,BCOTR6,BCOTR7,BCOTR8,BCOTR9,BCOTR10, "
            StrSql = StrSql + "BCWVTR1,BCWVTR2,BCWVTR3,BCWVTR4,BCWVTR5,BCWVTR6,BCWVTR7,BCWVTR8,BCWVTR9,BCWVTR10, "
            StrSql = StrSql + "BCGRADE1,BCGRADE2,BCGRADE3,BCGRADE4,BCGRADE5,BCGRADE6,BCGRADE7,BCGRADE8,BCGRADE9,BCGRADE10, "
            StrSql = StrSql + "BCTS1VAL1,BCTS1VAL2,BCTS1VAL3,BCTS1VAL4,BCTS1VAL5,BCTS1VAL6,BCTS1VAL7,BCTS1VAL8,BCTS1VAL9,BCTS1VAL10, "
            StrSql = StrSql + "BCTS2VAL1,BCTS2VAL2,BCTS2VAL3,BCTS2VAL4,BCTS2VAL5,BCTS2VAL6,BCTS2VAL7,BCTS2VAL8,BCTS2VAL9,BCTS2VAL10,TYPEM "
            StrSql = StrSql + "FROM BLENDMATINPUT "
            StrSql = StrSql + "WHERE CASEID = " + SCaseId.ToString() + " "


            odbUtil.UpIns(StrSql, Connection)
        Catch ex As Exception

        End Try
    End Sub
#End Region
End Class
