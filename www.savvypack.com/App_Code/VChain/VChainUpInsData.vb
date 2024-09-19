Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports VChainGetData
Imports System.Math
Imports System.Web.UI.HtmlTextWriter

Public Class VChainUpInsData
    Public Class UpdateInsert
        Dim VChainConnection As String = System.Configuration.ConfigurationManager.AppSettings("VChainConnectionString")


#Region "Value Chain Update"
        Public Sub AddValueChain(ByVal VChainName As String, ByVal UserId As String, ByVal modName As String, ByVal resCase As String)
            Try
                Dim odButil As New DBUtil()
                Dim strsql As String = ""
                strsql = "INSERT INTO USRVALUECHAIN"
                strsql = strsql + "( "
                strsql = strsql + "VALUECHAINID, VALUECHAINNAME,USERID,MODNAME,RESULTCASES"
                strsql = strsql + ") "
                strsql = strsql + "SELECT SEQVALUECHAINID.NEXTVAL, "
                strsql = strsql + "'" + VChainName + "', "
                strsql = strsql + UserId + ", "
                strsql = strsql + "'" + modName + "', "
                strsql = strsql + resCase + " "
                strsql = strsql + "FROM DUAL "
                odButil.UpIns(strsql, VChainConnection)
            Catch ex As Exception

            End Try
        End Sub
        Public Sub EditValueChain(ByVal VChainID As String, ByVal UserId As String, ByVal modName As String, ByVal resCaseId As String)
            Try
                Dim odButil As New DBUtil()
                Dim strsql As String = ""
                strsql = "UPDATE USRVALUECHAIN "
                strsql = strsql + "SET MODNAME='" + modName + "', "
                strsql = strsql + "RESULTCASES=" + resCaseId + " "
                strsql = strsql + "WHERE VALUECHAINID=" + VChainID + " "
                strsql = strsql + "AND USERID= " + UserId + " "
                strsql = strsql + "AND MODNAME IS NULL "
                strsql = strsql + "AND RESULTCASES IS NULL "
                odButil.UpIns(strsql, VChainConnection)
            Catch ex As Exception

            End Try
        End Sub

        Public Sub DeleteVChain(ByVal UserName As String, ByVal VChainId As String)
            Try
                Dim StrSql As String = String.Empty                
                Dim odbUtil As New DBUtil()
                'DELETE THE VALUECHAIN FROM USRVALUECHAIN
                StrSql = "DELETE FROM USRVALUECHAIN  "
                StrSql = StrSql + "WHERE VALUECHAINID=" + VChainId.ToString()
                odbUtil.UpIns(StrSql, VChainConnection)

                'INSERT INTO THE DELETED TABLE
                StrSql = ""
                StrSql = "INSERT INTO DELETED  "
                StrSql = StrSql + "(USERNAME,VALUECHAINID,SERVERDATE) "
                StrSql = StrSql + "SELECT '" + UserName.ToString() + "'," + VChainId.ToString() + ", SYSDATE "
                StrSql = StrSql + "FROM DUAL "
                odbUtil.UpIns(StrSql, VChainConnection)

            Catch ex As Exception
                Throw New Exception("VChainUpInsData:DeleteVChain:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub CaseUpdate_OLD(ByVal CaseID As String, ByVal ModType() As String, ByVal Cases() As String, ByVal Schema As String)
            Dim con As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
            Try
                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim Matid As New Integer
                Dim Mat As New Integer
                Dim StrSqlUpadate As String = ""
                Dim StrSqlIUpadate As String = ""
                StrSqlUpadate = "UPDATE MATERIALINPUT SET"
                For Mat = 0 To 9
                    Matid = Mat + 1
                    'Module Type
                    If ModType(Mat) <> "" Then
                        StrSqlIUpadate = StrSqlIUpadate + " MT" + Matid.ToString() + " = " + ModType(Mat).ToString() + ","
                    End If
                    'Cases
                    StrSqlIUpadate = StrSqlIUpadate + " C" + Matid.ToString() + " = " + Cases(Mat).ToString() + ","
                Next
                StrSqlIUpadate = StrSqlIUpadate.Remove(StrSqlIUpadate.Length - 1)
                StrSqlUpadate = StrSqlUpadate + StrSqlIUpadate
                StrSqlUpadate = StrSqlUpadate + " WHERE CASEID = " + CaseID + ""
                odbUtil.UpIns(StrSqlUpadate, con)
            Catch ex As Exception
                Throw New Exception("VChainGetData:CaseUpdate:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub CaseUpdate(ByVal VChainId As String, ByVal CaseID As String, ByVal ModType() As String, ByVal Cases() As String, ByVal Schema As String, ByVal modName As String, ByVal PRICE() As String, ByVal ToolPage As Char)
            Dim con As String = System.Configuration.ConfigurationManager.AppSettings(Schema)
            Try
                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim Matid As New Integer
                Dim Mat As New Integer
                Dim tblName As String = String.Empty
                Dim StrSql As String = String.Empty



                'Preferences
                Dim dts As New DataSet()
                Dim Totalprice As String

                If modName.ToUpper() = "ECON2" Then
                    tblName = "MATCASE_ECON2"
                    Dim ObjGetData As New E2GetData.Selectdata()
                    dts = ObjGetData.GetPref(CaseID)
                ElseIf modName.ToUpper() = "ECON1" Then
                    tblName = "MATCASE_ECON1"
                    Dim ObjGetData As New E1GetData.Selectdata()
                    dts = ObjGetData.GetPref(CaseID)
                ElseIf modName.ToUpper() = "ECHEM1" Then
                    tblName = "MATCASE_ECHEM1"
                End If

                Dim Convwt As String = dts.Tables(0).Rows(0).Item("convwt")
                Dim Curr As String = dts.Tables(0).Rows(0).Item("curr")


                For Mat = 0 To 9
                    'If Cases(Mat).ToString() <> "" Then

                    If (PRICE(Mat) <> "") Then
                        Totalprice = CDbl(PRICE(Mat) * Convwt / Curr)
                    Else
                        Totalprice = "''"
                    End If

                    StrSql = "INSERT INTO " + tblName
                    StrSql = StrSql + "(VALUECHAINID,CASEID,MODTYPE,MODCASEID,LAYER,PRICE) "
                    StrSql = StrSql + "SELECT " + VChainId.ToString() + "," + CaseID + "," + ModType(Mat).ToString() + "," + Cases(Mat).ToString() + "," + (Mat + 1).ToString() + "," + Totalprice + " FROM DUAL "
                    StrSql = StrSql + "WHERE NOT EXISTS "
                    StrSql = StrSql + "(SELECT 1 "
                    StrSql = StrSql + " FROM " + tblName
                    StrSql = StrSql + " WHERE  VALUECHAINID=" + VChainId.ToString() + " AND CASEID=" + CaseID + " AND LAYER=" + (Mat + 1).ToString() + ") "
                    odbUtil.UpIns(StrSql, VChainConnection)


                    StrSql = "UPDATE " + tblName + " SET  "
                    StrSql = StrSql + "MODTYPE=" + ModType(Mat).ToString() + ", "
                    StrSql = StrSql + "MODCASEID=" + Cases(Mat).ToString()
                    If ToolPage.ToString() = "N" Then 'UPDATE PRICE ONLY IF THE PAGE IS MATERIAL & STRUCTURE
                        StrSql = StrSql + ", "
                        StrSql = StrSql + "PRICE= " + Totalprice.ToString() + ""
                    End If
                    StrSql = StrSql + " WHERE VALUECHAINID=" + VChainId.ToString() + " AND CASEID=" + CaseID + " AND LAYER=" + (Mat + 1).ToString() + ""
                    odbUtil.UpIns(StrSql, VChainConnection)


                    If ModType(Mat).ToString() = "2" Then
                        InsertResultSpl(VChainId.ToString(), "ECON1", Cases(Mat).ToString())
                    ElseIf ModType(Mat).ToString() = "3" Then
                        InsertResultSpl(VChainId.ToString(), "ECON2", Cases(Mat).ToString())
                    ElseIf ModType(Mat).ToString() = "1" Then
                        InsertResultSpl(VChainId.ToString(), "ECHEM1", Cases(Mat).ToString())
                    End If
                Next

            Catch ex As Exception
                Throw New Exception("VChainGetData:CaseUpdate:" + ex.Message.ToString())
            End Try

        End Sub
#End Region

#Region "ASSUMPTIONS"
        
        Public Sub InsertResultSpl(ByVal VChainId As String, ByVal ModType As String, ByVal CaseID As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim tblName As String = String.Empty
            Dim tblTotal As String = String.Empty
            Try
                If ModType.ToUpper() = "ECON2" Then
                    tblName = "RESULTSPL_ECON2"
                ElseIf ModType.ToUpper() = "ECON1" Then
                    tblName = "RESULTSPL_ECON1"
                    'tblTotal = "TOTAL_ECON1"
                ElseIf ModType.ToUpper() = "ECHEM1" Then
                    tblName = "RESULTSPL_ECHEM1"
                End If
                'INSERT INTO RESULTS TABLE
                StrSql = "INSERT INTO  "
                StrSql = StrSql + tblName
                StrSql = StrSql + "(VALUECHAINID,CASEID) "
                StrSql = StrSql + "SELECT " + VChainId.ToString() + "," + CaseID.ToString() + " FROM DUAL "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "(SELECT 1 "
                StrSql = StrSql + "FROM " + tblName
                StrSql = StrSql + " WHERE VALUECHAINID=" + VChainId.ToString() + " AND CASEID=" + CaseID.ToString() + ") "
                odbUtil.UpIns(StrSql, VChainConnection)

                'INSERT INTO TOTAL TABLE
                If ModType.ToUpper() = "ECON1" Then
                    StrSql = ""
                    StrSql = "INSERT INTO TOTAL_ECON1  "
                    StrSql = StrSql + "(VALUECHAINID,CASEID) "
                    StrSql = StrSql + "SELECT " + VChainId.ToString() + "," + CaseID.ToString() + " "
                    StrSql = StrSql + "FROM DUAL "
                    StrSql = StrSql + " WHERE NOT EXISTS  "
                    StrSql = StrSql + "(SELECT 1 "
                    StrSql = StrSql + "FROM TOTAL_ECON1 "
                    StrSql = StrSql + "WHERE VALUECHAINID=" + VChainId.ToString() + " AND CASEID=" + CaseID.ToString()
                    StrSql = StrSql + ") "

                    odbUtil.UpIns(StrSql, VChainConnection)
                End If
            Catch ex As Exception
                Throw New Exception("VChainGetData:InsertResultSpl:" + ex.Message.ToString())
            End Try
        End Sub
#End Region
    End Class
End Class
