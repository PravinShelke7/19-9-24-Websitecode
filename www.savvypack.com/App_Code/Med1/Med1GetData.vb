Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System

Public Class Med1GetData
    Public Class Selectdata
        Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
        Dim MedEcon1Connection As String = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")

#Region "UserDetails"
        Public Function GetUserDetails(ByVal Id As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERS.USERID,  "
                StrSql = StrSql + "UPPER(USERNAME)USERNAME, "
                StrSql = StrSql + "USERNAME AS TOOLUSERNAME, "

                'Checking for License Administrator
                StrSql = StrSql + "NVL(USERS.ISIADMINLICUSR,'N')ISIADMINLICUSR,"
                StrSql = StrSql + "USERS.LICENSEID,"
                StrSql = StrSql + "SERVICES.SERVICEID,"
                StrSql = StrSql + "SERVICES.SERVICEDE, "
                StrSql = StrSql + "USERPERMISSIONS.USERROLE AS SERVIECROLE, "
                StrSql = StrSql + "USERPERMISSIONS.MAXCASECOUNT, "
                StrSql = StrSql + "(CASE WHEN NVL(USERS.ISINTERNALUSR,'N') ='Y' THEN "
                StrSql = StrSql + "'AADMIN' "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "'USER' "
                StrSql = StrSql + "END)USERROLE "
                StrSql = StrSql + "FROM ECON.ULOGIN "
                StrSql = StrSql + "INNER JOIN ECON.USERS "
                StrSql = StrSql + "ON UPPER(USERS.USERNAME) = UPPER(ECON.ULOGIN.UNAME) "
                StrSql = StrSql + "AND UPPER(USERS.PASSWORD) = UPPER(ECON.ULOGIN.UPWD) "
                StrSql = StrSql + "INNER JOIN ECON.USERPERMISSIONS "
                StrSql = StrSql + "ON USERPERMISSIONS.USERID = USERS.USERID "
                StrSql = StrSql + "INNER JOIN ECON.SERVICES "
                StrSql = StrSql + "ON SERVICES.SERVICEID = USERPERMISSIONS.SERVICEID "
                StrSql = StrSql + "WHERE ECON.ULOGIN.ID = " + Id.ToString() + " "
                StrSql = StrSql + "AND SERVICES.SERVICEDE='MedE1' "


                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUserCompanyUsers(ByVal UserName As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  USERS.USERID,  "
                StrSql = StrSql + "USERS.USERNAME "
                StrSql = StrSql + "FROM ECON.USERS "
                StrSql = StrSql + "INNER JOIN ECON.USERPERMISSIONS "
                StrSql = StrSql + "ON USERPERMISSIONS.USERID= USERS.USERID "
                StrSql = StrSql + "INNER JOIN ECON.SERVICES "
                StrSql = StrSql + "ON SERVICES.SERVICEID = USERPERMISSIONS.SERVICEID "
                StrSql = StrSql + "WHERE USERS.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper.ToString() + "') "
                StrSql = StrSql + "AND SERVICES.SERVICEDE IN ('MedE1','MedE2','MedS1','MedS2') "
                StrSql = StrSql + "AND UPPER(USERPERMISSIONS.USERROLE)='READWRITE' "
                StrSql = StrSql + "GROUP BY USERS.USERID,USERS.USERNAME "
                StrSql = StrSql + "ORDER BY USERS.USERNAME "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetTotalCaseCount(ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT COUNT(*) AS TOTALCOUNT FROM PERMISSIONSCASES  "
                StrSql = StrSql + "WHERE USERID=" + UserId.ToString() + " "
                StrSql = StrSql + "AND SERVICEID IS NULL "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetTotalCaseCount:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSelectedUserDetails(ByVal usreName As String, ByVal Schema As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERPERMISSIONS.MaxCaseCount FROM ECON.USERPERMISSIONS INNER JOIN ECON.USERS ON USERS.USERID= USERPERMISSIONS.USERID  "
                StrSql = StrSql + "INNER JOIN ECON.SERVICES ON SERVICES.SERVICEID=USERPERMISSIONS.SERVICEID "
                StrSql = StrSql + "WHERE UPPER(USERS.USERNAME)='" + usreName.ToUpper() + "' AND SERVICES.SERVICEDE='" + Schema + "' "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetSelectedUserDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Supporting Assumptions Pages"
        Public Function GetBCaseDetails() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES FROM BASECASES "
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID "

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPCaseDetails(ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE USERID =" + UserId.ToString() + " "
                StrSql = StrSql + "AND SERVICEID IS NULL "
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID "

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCaseDetails(ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID,CASEDE1,(CASEDE1||' ' ||CASEDE2)CASEDES,CASEDE3,CASEDE2,CASETYPE  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT  CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,'Base Case' CASETYPE FROM BASECASES "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT DISTINCT CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,'Proprietary Case' CASETYPE FROM PERMISSIONSCASES "
                StrSql = StrSql + ") "
                StrSql = StrSql + "WHERE CASEID =" + CaseId.ToString() + " "
                StrSql = StrSql + "ORDER BY CASEID "

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetCaseDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCases(ByVal UserId As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE USERID =" + UserId.ToString() + " "
                StrSql = StrSql + "AND SERVICEID IS NULL "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID"
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetBCases(ByVal CaseDe1 As String, ByVal CaseDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE "
                StrSql = StrSql + "FROM BASECASES "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetMaterials(ByVal MatId As Integer, ByVal MatDe1 As String, ByVal MatDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                'StrSql = "SELECT MATID, (MATDE1||'  '||MATDE2)MATDES,MATDE1,MATDE2  "
                'StrSql = StrSql + "FROM MATERIALS "
                'StrSql = StrSql + "WHERE MATID = CASE WHEN " + MatId.ToString() + " = -1 THEN "
                'StrSql = StrSql + "MATID "
                'StrSql = StrSql + "ELSE "
                'StrSql = StrSql + "" + MatId.ToString() + " "
                'StrSql = StrSql + "END "
                'StrSql = StrSql + "AND NVL(UPPER(MATDE1),'#') LIKE '" + MatDe1.ToUpper() + "%' "
                'StrSql = StrSql + "AND NVL(UPPER(MATDE2),'#') LIKE '" + MatDe2.ToUpper() + "%' "
                'StrSql = StrSql + "ORDER BY  MATDE1"
                StrSql = "SELECT MATERIALS.MATID, (MATDE1||'  '||MATDE2)MATDES,MATDE1,MATDE2,GRADE.GRADENAME,GRADE.GRADEID,GRADE.WEIGHT,MATERIALS.SG  "
                StrSql = StrSql + "FROM MATERIALS "
                StrSql = StrSql + "INNER JOIN MATGRADE MG ON MG.MATID=MATERIALS.MATID  "
                StrSql = StrSql + "INNER JOIN GRADE ON GRADE.GRADEID=MG.GRADEID AND GRADE.ISDEFAULT='Y' "
                StrSql = StrSql + "WHERE MATERIALS.MATID = CASE WHEN " + MatId.ToString() + " = -1 THEN "
                StrSql = StrSql + "MATERIALS.MATID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + MatId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(MATDE1),'#') LIKE '" + MatDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(MATDE2),'#') LIKE '" + MatDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  MATDE1"
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetDiscMaterials(ByVal MatDisId As Integer, ByVal MatDe1 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select matDISid, matDISde1  "
                StrSql = StrSql + "FROM MaterialDIS "
                StrSql = StrSql + "WHERE matDISid = CASE WHEN " + MatDisId.ToString() + " = -1 THEN "
                StrSql = StrSql + "matDISid "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + MatDisId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(matDISde1),'#') LIKE '" + MatDe1.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  matDISde1"
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetProductFormt(ByVal FormtId As Integer, ByVal FormtDe1 As String, ByVal FormtDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  FormatID,  "
                StrSql = StrSql + "(FormatDe1 || ' ' ||  FormatDe2) FormatDes, "
                StrSql = StrSql + "FormatDe1, "
                StrSql = StrSql + "FormatDe2 "
                StrSql = StrSql + "FROM PRODUCTFORMAT "
                StrSql = StrSql + "WHERE FormatID = CASE WHEN " + FormtId.ToString() + " = -1 "
                StrSql = StrSql + "THEN FormatID ELSE " + FormtId.ToString() + "  END "
                StrSql = StrSql + "AND NVL(UPPER(FormatDe1),'#') LIKE '" + FormtDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(FormatDe1),'#') LIKE '" + FormtDe2.ToUpper() + "%' "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT  FormatID, "
                StrSql = StrSql + "(FormatDe1 || ' ' ||  FormatDe2) FormatDes, "
                StrSql = StrSql + "FormatDe1, "
                StrSql = StrSql + "FormatDe2 "
                StrSql = StrSql + "FROM PRODUCTFORMAT2 "
                StrSql = StrSql + "WHERE FormatID = CASE WHEN " + FormtId.ToString() + " = -1 "
                StrSql = StrSql + "THEN FormatID ELSE " + FormtId.ToString() + "  END "
                StrSql = StrSql + "AND NVL(UPPER(FormatDe1),'#') LIKE '" + FormtDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(FormatDe1),'#') LIKE '" + FormtDe2.ToUpper() + "%' "
                StrSql = StrSql + "AND FORMATID <> 17"
                StrSql = StrSql + "ORDER BY  FormatDe1 "

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetProductFormtByID(ByVal CaseID1 As Integer, ByVal CaseID2 As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  CASEID,M1, "
                StrSql = StrSql + "CASE WHEN M1=1 or M1=17 THEN 'msi' ELSE 'units' END UNITS "
                StrSql = StrSql + "FROM PRODUCTFORMATIN "
                StrSql = StrSql + "WHERE CASEID IN( " + CaseID1.ToString() + "," + CaseID2.ToString() + ")"
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetProductFormtByID:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPallets(ByVal PalletId As Integer, ByVal PalDe1 As String, ByVal PalDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  PalletId, (palletde1 || ' ' ||  palletde2) PallteDes, palletde1,palletde2,replace((palletde1 || ' ' ||  palletde2),'" + Chr(34) + "','##') PallteDes1 "
                StrSql = StrSql + "FROM ECON.Pallet "
                StrSql = StrSql + "WHERE PalletId = CASE WHEN " + PalletId.ToString() + " = -1 THEN "
                StrSql = StrSql + "PalletId "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + PalletId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(palletde1),'#') LIKE '" + PalDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(palletde2),'#') LIKE '" + PalDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  palletde1"
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetEquipment(ByVal EqId As Integer, ByVal EqDe1 As String, ByVal EqlDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select equipID,equipDE1,equipDE2,(equipDE1 || ' ' || equipDE2) equipDES,replace((equipDE1 || ' ' ||  equipDE2),Chr(34) ,'##') equipDES1 "
                StrSql = StrSql + "from equipment  "
                StrSql = StrSql + "WHERE EQUIPID = CASE WHEN " + EqId.ToString() + " = -1 THEN "
                StrSql = StrSql + "EQUIPID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + EqId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(equipDE1),'#') LIKE '" + EqDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(equipDE2),'#') LIKE '" + EqlDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY equipDE1"
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCostTypeInfo(ByVal CostId As Integer, ByVal Costde1 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select  costID,costde1 ,replace(costde1,Chr(34) ,'##') costdes"
                StrSql = StrSql + " from  ECON.costTYPE"
                StrSql = StrSql + " WHERE costID = CASE WHEN " + CostId.ToString() + " = -1 THEN "
                StrSql = StrSql + "costID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + CostId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(costde1),'#') LIKE '" + Costde1.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY costde1"
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPersonnelInfo(ByVal PersId As Integer, ByVal PersDe1 As String, ByVal PersDe2 As String, ByVal Country As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select persid, persde1,persde2,(persde1 || ' ' ||  persde2) as persDES, persde2,replace((persde1 || ' ' ||  persde2),Chr(34) ,'##') persDES1"
                StrSql = StrSql + " from ECON." + Country
                StrSql = StrSql + " WHERE persid = CASE WHEN " + PersId.ToString() + " = -1 THEN "
                StrSql = StrSql + "persid "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + PersId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(persde1),'#') LIKE '" + PersDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(persde2),'#') LIKE '" + PersDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY persde1"
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSupportEquipment(ByVal EqId As Integer, ByVal EqDe1 As String, ByVal EqlDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select equipID,equipDE1,equipDE2,(equipDE1 || ' ' || equipDE2) equipDES,replace((equipDE1 || ' ' ||  equipDE2),Chr(34) ,'##') equipDES1 "
                StrSql = StrSql + "from equipment2   "
                StrSql = StrSql + "WHERE EQUIPID = CASE WHEN " + EqId.ToString() + " = -1 THEN "
                StrSql = StrSql + "EQUIPID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + EqId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(equipDE1),'#') LIKE '" + EqDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(equipDE2),'#') LIKE '" + EqlDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY equipDE1"
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetDept(ByVal ProcId As Integer, ByVal ProcDe1 As String, ByVal ProcDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim CaseId As String = HttpContext.Current.Session("Med1CaseId").ToString()
            Try
                StrSql = "SELECT PROCID,(PROCDE1||'  '||PROCDE2) AS PROCDE,PROCDE1,PROCDE2  "
                StrSql = StrSql + "FROM PROCESS "
                StrSql = StrSql + "WHERE PROCID = CASE WHEN " + ProcId.ToString() + " = -1 THEN "
                StrSql = StrSql + "PROCID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + ProcId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(PROCDE1),'#') LIKE '" + ProcDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(PROCDE1),'#') LIKE '" + ProcDe2.ToUpper() + "%' "
                StrSql = StrSql + "AND PROCID IN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT M1 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M2 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M3 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M4 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M5 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M6 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M7 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M8 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M9 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M10 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 1 FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 0 FROM DUAL "
                StrSql = StrSql + ") "

                StrSql = StrSql + "ORDER BY  PROCDE1"

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetDept(ByVal ProcId As Integer, ByVal ProcDe1 As String, ByVal ProcDe2 As String, ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PROCID,(PROCDE1||'  '||PROCDE2) AS PROCDE,PROCDE1,PROCDE2  "
                StrSql = StrSql + "FROM PROCESS "
                StrSql = StrSql + "WHERE PROCID = CASE WHEN " + ProcId.ToString() + " = -1 THEN "
                StrSql = StrSql + "PROCID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + ProcId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(PROCDE1),'#') LIKE '" + ProcDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(PROCDE1),'#') LIKE '" + ProcDe2.ToUpper() + "%' "
                StrSql = StrSql + "AND PROCID IN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT M1 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M2 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M3 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M4 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M5 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M6 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M7 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M8 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M9 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT M10 FROM PLANTCONFIG WHERE CASEID =" + CaseId + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 1 FROM DUAL "
                StrSql = StrSql + ") "

                StrSql = StrSql + "ORDER BY  PROCDE1"

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetDeptPlantConfig(ByVal ProcId As Integer, ByVal ProcDe1 As String, ByVal ProcDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PROCID,(PROCDE1||'  '||PROCDE2) AS PROCDE,PROCDE1,PROCDE2  "
                StrSql = StrSql + "FROM PROCESS "
                StrSql = StrSql + "WHERE PROCID = CASE WHEN " + ProcId.ToString() + " = -1 THEN "
                StrSql = StrSql + "PROCID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + ProcId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AND NVL(UPPER(PROCDE1),'#') LIKE '" + ProcDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(PROCDE1),'#') LIKE '" + ProcDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  PROCDE1"

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPref(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID,  "
                StrSql = StrSql + "UNITS, "
                StrSql = StrSql + "CURRENCY, "
                StrSql = StrSql + "CURR, "
                StrSql = StrSql + "CONVWT, "
                StrSql = StrSql + "CONVTHICK, "
                StrSql = StrSql + "OCOUNTRY, "
                StrSql = StrSql + "DCOUNTRY, "
                StrSql = StrSql + "TITLE1, "
                StrSql = StrSql + "TITLE3, "
                StrSql = StrSql + "TITLE2, "
                StrSql = StrSql + "CONVAREA, "
                StrSql = StrSql + "TITLE4, "
                StrSql = StrSql + "CONVAREA2, "
                StrSql = StrSql + "TITLE5, "
                StrSql = StrSql + "CONVTHICK2, "
                StrSql = StrSql + "CONVTHICK3, "
                StrSql = StrSql + "TITLE6, "
                StrSql = StrSql + "TITLE7, "
                StrSql = StrSql + "TITLE8, "
                StrSql = StrSql + "TITLE9, "
                StrSql = StrSql + "EFFDATE, "
                StrSql = StrSql + "TO_CHAR(EFFDATE,'MON DD,YYYY')AS EDATE, "
                StrSql = StrSql + "TITLE10, "
                StrSql = StrSql + "TITLE11, "
                StrSql = StrSql + "TITLE12, "
                'Bug#385
                StrSql = StrSql + "TITLE13, "
                StrSql = StrSql + "TITLE14, "
                StrSql = StrSql + "TITLE15, "
                StrSql = StrSql + "CONVVOL, "
                StrSql = StrSql + "CONVAREA3, "
                'Bug#385
                StrSql = StrSql + "ERGYCALC, "
                StrSql = StrSql + "ISDSCTNEW, "
                StrSql = StrSql + "DFLAG "
                StrSql = StrSql + "FROM PREFERENCES "
                StrSql = StrSql + "WHERE CASEID = " + CaseId.ToString() + " "


                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetPrefDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID,  "
                StrSql = StrSql + "UNITS, "
                StrSql = StrSql + "CURRENCY, "
                StrSql = StrSql + "OCOUNTRY, "
                StrSql = StrSql + "DCOUNTRY, "
                StrSql = StrSql + "EFFDATE, "
                StrSql = StrSql + "TO_CHAR(EFFDATE,'MON DD,YYYY')AS EDATE, "
                StrSql = StrSql + "ERGYCALC, "
                StrSql = StrSql + "ISDSCTNEW "
                StrSql = StrSql + "FROM PREFERENCES "
                StrSql = StrSql + "WHERE CASEID = " + CaseId.ToString() + " "


                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetEFFCOUNTRY(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select (CASE WHEN PREF.OCOUNTRY=0 THEN  "
                StrSql = StrSql + "'personnel' "
                StrSql = StrSql + "WHEN PREF.OCOUNTRY=1 THEN "
                StrSql = StrSql + "'personnelChina' "
                StrSql = StrSql + "WHEN PREF.OCOUNTRY=2 THEN "
                StrSql = StrSql + "'personnelUK' "
                StrSql = StrSql + "WHEN PREF.OCOUNTRY=3 THEN "
                StrSql = StrSql + "'personnelGermany' "
                StrSql = StrSql + "WHEN PREF.OCOUNTRY=4 THEN "
                StrSql = StrSql + "'personnelSKorea' "
                StrSql = StrSql + "END) AS COUNTRY, "
                StrSql = StrSql + "(CASE WHEN PREF.OCOUNTRY=0 THEN "
                StrSql = StrSql + "'persArchUS' "
                StrSql = StrSql + "WHEN PREF.OCOUNTRY=1 THEN "
                StrSql = StrSql + "'persArchChina' "
                StrSql = StrSql + "WHEN PREF.OCOUNTRY=2 THEN "
                StrSql = StrSql + "'persArchUK' "
                StrSql = StrSql + "WHEN PREF.OCOUNTRY=3 THEN "
                StrSql = StrSql + "'persArchGermany' "
                StrSql = StrSql + "WHEN PREF.OCOUNTRY=4 THEN "
                StrSql = StrSql + "'persArchSKorea' "
                StrSql = StrSql + "END) AS EFFCOUNTRY "
                StrSql = StrSql + "FROM PREFERENCES PREF "
                StrSql = StrSql + "WHERE CASEID = " + CaseId.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetDiscretedMaterialTotal(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select caseID,thick,sg,wtPERarea, discreteWT from Total WHERE caseID= " + CaseId.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCountry(ByVal countryId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT COUNTRYID,COUNTRYDE1 FROM ECON.COUNTRY "
                StrSql = StrSql + "WHERE COUNTRYID = CASE WHEN " + countryId.ToString() + " = -1 THEN "
                StrSql = StrSql + "COUNTRYID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + countryId.ToString() + " "
                StrSql = StrSql + "END  ORDER BY COUNTRYDE1"
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetCountry:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        'Public Function GetCountry() As DataSet
        '    Dim Dts As New DataSet()
        '    Dim odbUtil As New DBUtil()
        '    Dim StrSql As String = String.Empty
        '    Try
        '        StrSql = "SELECT COUNTRYID,COUNTRYDE1 FROM COUNTRY ORDER BY COUNTRYDE1"
        '        Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
        '        Return Dts
        '    Catch ex As Exception
        '        Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
        '        Return Dts
        '    End Try
        'End Function

        Public Function GetEffDate() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT TO_CHAR(EFFDATE,'MON DD,YYYY')AS EDATE FROM ECON.EFFDATE ORDER BY EFFDATE DESC"
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCurrancy(ByVal currId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CURID,CURDE1 FROM ECON.CURRENCY  "
                StrSql = StrSql + "WHERE CURID = CASE WHEN " + currId.ToString() + " = -1 THEN "
                StrSql = StrSql + "CURID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + currId.ToString() + " "
                StrSql = StrSql + "END  ORDER BY CURDE1"
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetCurrancy:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        'Public Function GetCurrancy() As DataSet
        '    Dim Dts As New DataSet()
        '    Dim odbUtil As New DBUtil()
        '    Dim StrSql As String = String.Empty
        '    Try
        '        StrSql = "SELECT CURID,CURDE1 FROM CURRENCY ORDER BY CURDE1 "
        '        Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
        '        Return Dts
        '    Catch ex As Exception
        '        Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
        '        Return Dts
        '    End Try
        'End Function

        Public Function GetCurrancyArch(ByVal CaseId As String, ByVal CurID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CURRENCYARCH.CURID,  "
                StrSql = StrSql + "CURRENCYARCH.CURPUSD, "
                StrSql = StrSql + "CURRENCYARCH.EFFDATE "
                StrSql = StrSql + "FROM ECON.CURRENCYARCH "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.EFFDATE = CURRENCYARCH.EFFDATE "
                StrSql = StrSql + "WHERE PREF.CASEID =" + CaseId + ""
                StrSql = StrSql + "AND CURRENCYARCH.CURID = " + CurID + ""
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetConversionFactor() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT "
                StrSql = StrSql + "MICPMIL, KGPLB, M2PMSI, M2PSQFT, MPFT, KMPMILE, JPMJ, LITPGAL, IN2PSQFT, "
                'Bug#385
                StrSql = StrSql + "CCMPCFT,GMPLB,MM2PIN2,TPKN,GMPOZ  "
                'Bug#385
                StrSql = StrSql + "FROM ECON.CONVFACTORS "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetQuestionID() As Integer
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim QuId As New Integer
            Try
                StrSql = "SELECT SEQQUESTIONID.NEXTVAL AS QID  "
                StrSql = StrSql + "FROM DUAL "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                QuId = Convert.ToInt32(Dts.Tables(0).Rows(0).Item("QID").ToString())
                Return QuId
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetQuestionID:" + ex.Message.ToString())
                Return QuId
            End Try
        End Function

        Public Function getUnits(ByVal caseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim QuId As New Integer
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "(PREF.TITLE2||'/'||	PREF.TITLE8) UNIT, "
                StrSql = StrSql + "'0' AS VAL "
                StrSql = StrSql + "FROM RESULTSPL "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=RESULTSPL.CASEID "
                StrSql = StrSql + "WHERE RESULTSPL.Caseid= " + caseId.ToString() + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(CASE  WHEN FINVOLMSI > 1 THEN "
                StrSql = StrSql + "PREF.TITLE6||'/'||	PREF.TITLE3 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "PREF.TITLE6||'/unit ' "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END)Unit, "
                StrSql = StrSql + "'1' AS VAL "
                StrSql = StrSql + "FROM RESULTSPL "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=RESULTSPL.CASEID "
                StrSql = StrSql + "WHERE RESULTSPL.Caseid= " + caseId.ToString() + " "
                StrSql = StrSql + "UNION  "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(PREF.TITLE2||'/thousand') UNIT, "
                StrSql = StrSql + "'2' AS VAL "
                StrSql = StrSql + "FROM PREFERENCES PREF "
                StrSql = StrSql + "WHERE PREF.Caseid= " + caseId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:getUnits:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
       
#End Region

#Region "Assumptions Pages"
        Public Function GetExtrusionDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  MAT.CASEID,  "
                  StrSql = StrSql + "TO_CHAR(MAT.EFFDATE,'MON DD,YYYY')AS EDATE, "
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
                StrSql = StrSql + "(MAT.T1*PREF.CONVTHICK) AS THICK1, "
                StrSql = StrSql + "(MAT.T2*PREF.CONVTHICK) AS THICK2, "
                StrSql = StrSql + "(MAT.T3*PREF.CONVTHICK) AS THICK3, "
                StrSql = StrSql + "(MAT.T4*PREF.CONVTHICK) AS THICK4, "
                StrSql = StrSql + "(MAT.T5*PREF.CONVTHICK) AS THICK5, "
                StrSql = StrSql + "(MAT.T6*PREF.CONVTHICK) AS THICK6, "
                StrSql = StrSql + "(MAT.T7*PREF.CONVTHICK) AS THICK7, "
                StrSql = StrSql + "(MAT.T8*PREF.CONVTHICK) AS THICK8, "
                StrSql = StrSql + "(MAT.T9*PREF.CONVTHICK) AS THICK9, "
                StrSql = StrSql + "(MAT.T10*PREF.CONVTHICK) AS THICK10, "
                StrSql = StrSql + "(TOT.THICK*PREF.CONVTHICK)THICK, "
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
                StrSql = StrSql + "MAT.R1, "
                StrSql = StrSql + "MAT.R2, "
                StrSql = StrSql + "MAT.R3, "
                StrSql = StrSql + "MAT.R4, "
                StrSql = StrSql + "MAT.R5, "
                StrSql = StrSql + "MAT.R6, "
                StrSql = StrSql + "MAT.R7, "
                StrSql = StrSql + "MAT.R8, "
                StrSql = StrSql + "MAT.R9, "
                StrSql = StrSql + "MAT.R10, "
                StrSql = StrSql + "MAT.E1, "
                StrSql = StrSql + "MAT.E2, "
                StrSql = StrSql + "MAT.E3, "
                StrSql = StrSql + "MAT.E4, "
                StrSql = StrSql + "MAT.E5, "
                StrSql = StrSql + "MAT.E6, "
                StrSql = StrSql + "MAT.E7, "
                StrSql = StrSql + "MAT.E8, "
                StrSql = StrSql + "MAT.E9, "
                StrSql = StrSql + "MAT.E10, "
                StrSql = StrSql + "(MAT1.SG)AS SGS1, "
                StrSql = StrSql + "(MAT2.SG)AS SGS2, "
                StrSql = StrSql + "(MAT3.SG)AS SGS3, "
                StrSql = StrSql + "(MAT4.SG)AS SGS4, "
                StrSql = StrSql + "(MAT5.SG)AS SGS5, "
                StrSql = StrSql + "(MAT6.SG)AS SGS6, "
                StrSql = StrSql + "(MAT7.SG)AS SGS7, "
                StrSql = StrSql + "(MAT8.SG)AS SGS8, "
                StrSql = StrSql + "(MAT9.SG)AS SGS9, "
                StrSql = StrSql + "(MAT10.SG)AS SGS10, "
                StrSql = StrSql + "MAT.SG1 AS SGP1, "
                StrSql = StrSql + "MAT.SG2 AS SGP2, "
                StrSql = StrSql + "MAT.SG3 AS SGP3, "
                StrSql = StrSql + "MAT.SG4 AS SGP4, "
                StrSql = StrSql + "MAT.SG5 AS SGP5, "
                StrSql = StrSql + "MAT.SG6 AS SGP6, "
                StrSql = StrSql + "MAT.SG7 AS SGP7, "
                StrSql = StrSql + "MAT.SG8 AS SGP8, "
                StrSql = StrSql + "MAT.SG9 AS SGP9, "
                StrSql = StrSql + "MAT.SG10 AS SGP10, "
                StrSql = StrSql + "(MATOUT.M1*PREF.CONVWT/PREF.CONVAREA) AS WTPARA1, "
                StrSql = StrSql + "(MATOUT.M2*PREF.CONVWT/PREF.CONVAREA) AS WTPARA2, "
                StrSql = StrSql + "(MATOUT.M3*PREF.CONVWT/PREF.CONVAREA) AS WTPARA3, "
                StrSql = StrSql + "(MATOUT.M4*PREF.CONVWT/PREF.CONVAREA) AS WTPARA4, "
                StrSql = StrSql + "(MATOUT.M5*PREF.CONVWT/PREF.CONVAREA) AS WTPARA5, "
                StrSql = StrSql + "(MATOUT.M6*PREF.CONVWT/PREF.CONVAREA) AS WTPARA6, "
                StrSql = StrSql + "(MATOUT.M7*PREF.CONVWT/PREF.CONVAREA) AS WTPARA7, "
                StrSql = StrSql + "(MATOUT.M8*PREF.CONVWT/PREF.CONVAREA) AS WTPARA8, "
                StrSql = StrSql + "(MATOUT.M9*PREF.CONVWT/PREF.CONVAREA) AS WTPARA9, "
                StrSql = StrSql + "(MATOUT.M10*PREF.CONVWT/PREF.CONVAREA) AS WTPARA10, "
                StrSql = StrSql + "(TOT.WTPERAREA*PREF.CONVWT/PREF.CONVAREA)WTPERAREA, "
                StrSql = StrSql + "MAT.D1, "
                StrSql = StrSql + "MAT.D2, "
                StrSql = StrSql + "MAT.D3, "
                StrSql = StrSql + "MAT.D4, "
                StrSql = StrSql + "MAT.D5, "
                StrSql = StrSql + "MAT.D6, "
                StrSql = StrSql + "MAT.D7, "
                StrSql = StrSql + "MAT.D8, "
                StrSql = StrSql + "MAT.D9, "
                StrSql = StrSql + "MAT.D10, "
                StrSql = StrSql + "TO_CHAR(MAT.EFFDATE,'MON DD, YYYY')EFFDATE, "
                StrSql = StrSql + "MAT.PLATE, "
                StrSql = StrSql + "MAT.DISCMATYN, "
                StrSql = StrSql + "TOT.DISCRETEWT * PREF.CONVWT AS DISCTOTAL, "
                StrSql = StrSql + "TOT.DISCRETECOST, "
                StrSql = StrSql + "MATDESC.DISID1, "
                StrSql = StrSql + "MATDESC.DISID2, "
                StrSql = StrSql + "MATDESC.DISID3, "
                StrSql = StrSql + "MATDESC.DISW1* PREF.CONVWT AS DISW1, "
                StrSql = StrSql + "MATDESC.DISW2* PREF.CONVWT AS DISW2, "
                StrSql = StrSql + "MATDESC.DISW3* PREF.CONVWT AS DISW3, "
                StrSql = StrSql + "MATDESC.DISP1* PREF.CURR AS DISP1, "
                StrSql = StrSql + "MATDESC.DISP2* PREF.CURR AS DISP2, "
                StrSql = StrSql + "MATDESC.DISP3* PREF.CURR AS DISP3, "
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

                'Barrier
                StrSql = StrSql + "(NVL(TOTTB.PRICE,0)/PREF.CONVWT*PREF.CURR)AS PRICE, "
                StrSql = StrSql + "(MATOUT.AM1*PREF.CURR/PREF.CONVAREA) AS COSTPARA1, "
                StrSql = StrSql + "(MATOUT.AM2*PREF.CURR/PREF.CONVAREA) AS COSTPARA2, "
                StrSql = StrSql + "(MATOUT.AM3*PREF.CURR/PREF.CONVAREA) AS COSTPARA3, "
                StrSql = StrSql + "(MATOUT.AM4*PREF.CURR/PREF.CONVAREA) AS COSTPARA4, "
                StrSql = StrSql + "(MATOUT.AM5*PREF.CURR/PREF.CONVAREA) AS COSTPARA5, "
                StrSql = StrSql + "(MATOUT.AM6*PREF.CURR/PREF.CONVAREA) AS COSTPARA6, "
                StrSql = StrSql + "(MATOUT.AM7*PREF.CURR/PREF.CONVAREA) AS COSTPARA7, "
                StrSql = StrSql + "(MATOUT.AM8*PREF.CURR/PREF.CONVAREA) AS COSTPARA8, "
                StrSql = StrSql + "(MATOUT.AM9*PREF.CURR/PREF.CONVAREA) AS COSTPARA9, "
                StrSql = StrSql + "(MATOUT.AM10*PREF.CURR/PREF.CONVAREA) AS COSTPARA10, "
                StrSql = StrSql + "(TOTTB.PRICEAREA*PREF.CURR/PREF.CONVAREA)COSTPARAREA , "
                StrSql = StrSql + "PREF.CONVAREA, "
                StrSql = StrSql + "PREF.TITLE19, "
                StrSql = StrSql + "PREF.TITLE20, "
                StrSql = StrSql + "OTR1, "
                StrSql = StrSql + "OTR2, "
                StrSql = StrSql + "OTR3, "
                StrSql = StrSql + "OTR4 , "
                StrSql = StrSql + "OTR5, "
                StrSql = StrSql + "OTR6 , "
                StrSql = StrSql + "OTR7, "
                StrSql = StrSql + "OTR8, "
                StrSql = StrSql + "OTR9, "
                StrSql = StrSql + "OTR10, "
                StrSql = StrSql + "WVTR1, "
                StrSql = StrSql + "WVTR2, "
                StrSql = StrSql + "WVTR3, "
                StrSql = StrSql + "WVTR4, "
                StrSql = StrSql + "WVTR5, "
                StrSql = StrSql + "WVTR6, "
                StrSql = StrSql + "WVTR7, "
                StrSql = StrSql + "WVTR8, "
                StrSql = StrSql + "WVTR9, "
                StrSql = StrSql + "WVTR10, "

                StrSql = StrSql + "NVL(GRADE1,0) GRADE1, "
                StrSql = StrSql + "NVL(GRADE2,0) GRADE2, "
                StrSql = StrSql + "NVL(GRADE3,0) GRADE3, "
                StrSql = StrSql + "NVL(GRADE4,0) GRADE4, "
                StrSql = StrSql + "NVL(GRADE5,0) GRADE5, "
                StrSql = StrSql + "NVL(GRADE6,0) GRADE6, "
                StrSql = StrSql + "NVL(GRADE7,0) GRADE7, "
                StrSql = StrSql + "NVL(GRADE8,0) GRADE8, "
                StrSql = StrSql + "NVL(GRADE9,0) GRADE9, "
                StrSql = StrSql + "NVL(GRADE10,0) GRADE10, "
                StrSql = StrSql + "MAT.OTRTEMP, "
                StrSql = StrSql + "MAT.WVTRTEMP, "
                StrSql = StrSql + "MAT.OTRRH, "
                StrSql = StrSql + "MAT.WVTRRH "

                StrSql = StrSql + "FROM MATERIALINPUT MAT "

                'Barrier
                StrSql = StrSql + "INNER JOIN BARRIERINPUT BI "
                StrSql = StrSql + "ON MAT.CASEID=BI.CASEID "

                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=MAT.CASEID "
                StrSql = StrSql + "INNER JOIN TOTAL TOT "
                StrSql = StrSql + "ON TOT.CASEID = MAT.CASEID "

                'Bug#441
                StrSql = StrSql + "INNER JOIN TOTALTB TOTTB "
                StrSql = StrSql + "ON TOTTB.CASEID = MAT.CASEID "

                StrSql = StrSql + "INNER JOIN MATERIALDISIN MATDESC "
                StrSql = StrSql + "ON MATDESC.CASEID = MAT.CASEID "
                StrSql = StrSql + "INNER JOIN MATERIALOUTPUT MATOUT "
                StrSql = StrSql + "ON MATOUT.CASEID=MAT.CASEID "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MATERIALSARCH MATA1 "
                StrSql = StrSql + "ON MATA1.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA1.MATID = MAT.M1 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MATERIALSARCH MATA2 "
                StrSql = StrSql + "ON MATA2.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA2.MATID = MAT.M2 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MATERIALSARCH MATA3 "
                StrSql = StrSql + "ON MATA3.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA3.MATID = MAT.M3 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MATERIALSARCH MATA4 "
                StrSql = StrSql + "ON MATA4.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA4.MATID = MAT.M4 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MATERIALSARCH MATA5 "
                StrSql = StrSql + "ON MATA5.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA5.MATID = MAT.M5 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MATERIALSARCH MATA6 "
                StrSql = StrSql + "ON MATA6.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA6.MATID = MAT.M6 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MATERIALSARCH MATA7 "
                StrSql = StrSql + "ON MATA7.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA7.MATID = MAT.M7 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MATERIALSARCH MATA8 "
                StrSql = StrSql + "ON MATA8.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA8.MATID = MAT.M8 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MATERIALSARCH MATA9 "
                StrSql = StrSql + "ON MATA9.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA9.MATID = MAT.M9 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MATERIALSARCH MATA10 "
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
                StrSql = StrSql + "WHERE MAT.CASEID =" + CaseId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetProductFromatIn(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  PRODUCTFORMATIN.M1,  "
                StrSql = StrSql + "(PRODUCTFORMATIN.M2*PREF.CONVTHICK) AS M2, "
                StrSql = StrSql + "(CASE WHEN (PREF.UNITS = 1 AND PRODUCTFORMATIN.M1 =1) THEN "
                StrSql = StrSql + "(PRODUCTFORMATIN.M3*PREF.CONVTHICK*0.01204) "
                StrSql = StrSql + "ELSE (PRODUCTFORMATIN.M3*PREF.CONVTHICK) "
                StrSql = StrSql + "END) AS M3, "
                StrSql = StrSql + "(PRODUCTFORMATIN.M4*PREF.CONVTHICK) AS M4, "
                StrSql = StrSql + "PRODUCTFORMATIN.M5, "
                StrSql = StrSql + "PRODUCTFORMATIN.M6, "
                StrSql = StrSql + "NVL(PRODUCTFORMAT.M1,PRODUCTFORMAT2.M1 ) AS FORMAT_M1, "
                StrSql = StrSql + "NVL(PRODUCTFORMAT.M2,PRODUCTFORMAT2.M2 ) AS FORMAT_M2, "
                StrSql = StrSql + "NVL(PRODUCTFORMAT.M3,PRODUCTFORMAT2.M3 ) AS FORMAT_M3, "
                StrSql = StrSql + "NVL(PRODUCTFORMAT.M4,PRODUCTFORMAT2.M4 ) AS FORMAT_M4, "
                StrSql = StrSql + "NVL(PRODUCTFORMAT.M5,PRODUCTFORMAT2.M5 ) AS FORMAT_M5, "
                StrSql = StrSql + "(TOTAL.PRODWT*PREF.CONVWT) AS PRODWT, "
                StrSql = StrSql + "(PRODUCTFORMATIN.PWT*PREF.CONVWT) AS PRODWTPREF, "
                StrSql = StrSql + "(TOTAL.ROLLDIA*PREF.CONVTHICK)ROLLDIA, "
                StrSql = StrSql + "PREF.UNITS, "
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
                StrSql = StrSql + "PREF.TITLE12 "
                StrSql = StrSql + "FROM PRODUCTFORMATIN "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = PRODUCTFORMATIN.CASEID "
                StrSql = StrSql + "INNER JOIN TOTAL "
                StrSql = StrSql + "ON TOTAL.CASEID = PRODUCTFORMATIN.CASEID "
                StrSql = StrSql + "LEFT OUTER JOIN PRODUCTFORMAT "
                StrSql = StrSql + "ON PRODUCTFORMAT.FORMATID = PRODUCTFORMATIN.M1 "
                StrSql = StrSql + "AND PREF.UNITS = 0 "
                StrSql = StrSql + "LEFT OUTER JOIN PRODUCTFORMAT2 "
                StrSql = StrSql + "ON PRODUCTFORMAT2.FORMATID = PRODUCTFORMATIN.M1 "
                StrSql = StrSql + "AND PREF.UNITS = 1 "
                StrSql = StrSql + "WHERE PRODUCTFORMATIN.CASEID  = " + CaseId.ToString() + " "


                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPalletAndTruck(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT (TKLP.M1*PREF.CONVTHICK) AS P1,  "
                StrSql = StrSql + "(TKLP.M2*PREF.CONVTHICK)	AS P2, "
                StrSql = StrSql + "(TKLP.M3*PREF.CONVTHICK) AS P3, "
                StrSql = StrSql + "TKLP.M4 AS P4, "
                StrSql = StrSql + "TKLP.M5 AS P5, "
                StrSql = StrSql + "(TKLP.T1*PREF.CONVTHICK) AS T1, "
                StrSql = StrSql + "(TKLP.T2*PREF.CONVTHICK) AS T2, "
                StrSql = StrSql + "(TKLP.T3*PREF.CONVTHICK) AS T3, "
                StrSql = StrSql + "(TKLP.T4*PREF.CONVWT) AS T4, "
                StrSql = StrSql + "TKLP.T5 AS T5, "
                StrSql = StrSql + "(TOTAL.TOTWTPERT*PREF.CONVWT) AS CALCULATEDWEIGHT, "
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
                StrSql = StrSql + "FROM TRUCKPALLETIN TKLP "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = TKLP.CASEID "
                StrSql = StrSql + "INNER JOIN TOTAL "
                StrSql = StrSql + "ON TOTAL.CASEID = TKLP.CASEID "
                StrSql = StrSql + "WHERE TKLP.CASEID =" + CaseId.ToString() + ""



                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPalletInDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  DISTINCT  "
                StrSql = StrSql + "PALLETIN.CASEID, "
                StrSql = StrSql + "ITEM1.PALLETID AS ITEM1, "
                StrSql = StrSql + "ITEM2.PALLETID AS ITEM2, "
                StrSql = StrSql + "ITEM3.PALLETID AS ITEM3, "
                StrSql = StrSql + "ITEM4.PALLETID AS ITEM4, "
                StrSql = StrSql + "ITEM5.PALLETID AS ITEM5, "
                StrSql = StrSql + "ITEM6.PALLETID AS ITEM6, "
                StrSql = StrSql + "ITEM7.PALLETID AS ITEM7, "
                StrSql = StrSql + "ITEM8.PALLETID AS ITEM8, "
                StrSql = StrSql + "ITEM9.PALLETID AS ITEM9, "
                StrSql = StrSql + "ITEM10.PALLETID AS ITEM10, "
                StrSql = StrSql + "PALLETIN.T1 AS NUMBER1, "
                StrSql = StrSql + "PALLETIN.T2 AS NUMBER2, "
                StrSql = StrSql + "PALLETIN.T3 AS NUMBER3, "
                StrSql = StrSql + "PALLETIN.T4 AS NUMBER4, "
                StrSql = StrSql + "PALLETIN.T5 AS NUMBER5, "
                StrSql = StrSql + "PALLETIN.T6 AS NUMBER6, "
                StrSql = StrSql + "PALLETIN.T7 AS NUMBER7, "
                StrSql = StrSql + "PALLETIN.T8 AS NUMBER8, "
                StrSql = StrSql + "PALLETIN.T9 AS NUMBER9, "
                StrSql = StrSql + "PALLETIN.T10 AS NUMBER10, "
                StrSql = StrSql + "PALLETIN.R1 AS NOOFUSE1, "
                StrSql = StrSql + "PALLETIN.R2 AS NOOFUSE2, "
                StrSql = StrSql + "PALLETIN.R3 AS NOOFUSE3, "
                StrSql = StrSql + "PALLETIN.R4 AS NOOFUSE4, "
                StrSql = StrSql + "PALLETIN.R5 AS NOOFUSE5, "
                StrSql = StrSql + "PALLETIN.R6 AS NOOFUSE6, "
                StrSql = StrSql + "PALLETIN.R7 AS NOOFUSE7, "
                StrSql = StrSql + "PALLETIN.R8 AS NOOFUSE8, "
                StrSql = StrSql + "PALLETIN.R9 AS NOOFUSE9, "
                StrSql = StrSql + "PALLETIN.R10 AS NOOFUSE10, "
                StrSql = StrSql + "(ITEM1.WEIGHT * PREF.CONVWT) AS WeightS1, "
                StrSql = StrSql + "(ITEM2.WEIGHT * PREF.CONVWT) AS WeightS2, "
                StrSql = StrSql + "(ITEM3.WEIGHT * PREF.CONVWT) AS WeightS3, "
                StrSql = StrSql + "(ITEM4.WEIGHT * PREF.CONVWT) AS WeightS4, "
                StrSql = StrSql + "(ITEM5.WEIGHT * PREF.CONVWT) AS WeightS5, "
                StrSql = StrSql + "(ITEM6.WEIGHT * PREF.CONVWT) AS WeightS6, "
                StrSql = StrSql + "(ITEM7.WEIGHT * PREF.CONVWT) AS WeightS7, "
                StrSql = StrSql + "(ITEM8.WEIGHT * PREF.CONVWT) AS WeightS8, "
                StrSql = StrSql + "(ITEM9.WEIGHT * PREF.CONVWT) AS WeightS9, "
                StrSql = StrSql + "(ITEM10.WEIGHT * PREF.CONVWT) AS WeightS10, "
                StrSql = StrSql + "(PALLETIN.W1* PREF.CONVWT) AS WeightP1, "
                StrSql = StrSql + "(PALLETIN.W2* PREF.CONVWT) AS WeightP2, "
                StrSql = StrSql + "(PALLETIN.W3* PREF.CONVWT) AS WeightP3, "
                StrSql = StrSql + "(PALLETIN.W4* PREF.CONVWT) AS WeightP4, "
                StrSql = StrSql + "(PALLETIN.W5* PREF.CONVWT) AS WeightP5, "
                StrSql = StrSql + "(PALLETIN.W6* PREF.CONVWT) AS WeightP6, "
                StrSql = StrSql + "(PALLETIN.W7* PREF.CONVWT) AS WeightP7, "
                StrSql = StrSql + "(PALLETIN.W8* PREF.CONVWT) AS WeightP8, "
                StrSql = StrSql + "(PALLETIN.W9* PREF.CONVWT) AS WeightP9, "
                StrSql = StrSql + "(PALLETIN.W10* PREF.CONVWT) AS WeightP10, "
                StrSql = StrSql + "(ITEM1.PRICE * PREF.CURR) AS PRICES1, "
                StrSql = StrSql + "(ITEM2.PRICE * PREF.CURR) AS PRICES2, "
                StrSql = StrSql + "(ITEM3.PRICE * PREF.CURR) AS PRICES3, "
                StrSql = StrSql + "(ITEM4.PRICE * PREF.CURR) AS PRICES4, "
                StrSql = StrSql + "(ITEM5.PRICE * PREF.CURR) AS PRICES5, "
                StrSql = StrSql + "(ITEM6.PRICE * PREF.CURR) AS PRICES6, "
                StrSql = StrSql + "(ITEM7.PRICE * PREF.CURR) AS PRICES7, "
                StrSql = StrSql + "(ITEM8.PRICE * PREF.CURR) AS PRICES8, "
                StrSql = StrSql + "(ITEM9.PRICE * PREF.CURR) AS PRICES9, "
                StrSql = StrSql + "(ITEM10.PRICE * PREF.CURR) AS PRICES10, "
                StrSql = StrSql + "(PALLETIN.P1 * PREF.CURR) AS PRICEP1, "
                StrSql = StrSql + "(PALLETIN.P2 * PREF.CURR) AS PRICEP2, "
                StrSql = StrSql + "(PALLETIN.P3 * PREF.CURR) AS PRICEP3, "
                StrSql = StrSql + "(PALLETIN.P4 * PREF.CURR) AS PRICEP4, "
                StrSql = StrSql + "(PALLETIN.P5 * PREF.CURR) AS PRICEP5, "
                StrSql = StrSql + "(PALLETIN.P6 * PREF.CURR) AS PRICEP6, "
                StrSql = StrSql + "(PALLETIN.P7 * PREF.CURR) AS PRICEP7, "
                StrSql = StrSql + "(PALLETIN.P8 * PREF.CURR) AS PRICEP8, "
                StrSql = StrSql + "(PALLETIN.P9 * PREF.CURR) AS PRICEP9, "
                StrSql = StrSql + "(PALLETIN.P10 * PREF.CURR) AS PRICEP10, "
                StrSql = StrSql + "PALLETIN.D1 AS DEPT1, "
                StrSql = StrSql + "PALLETIN.D2  AS DEPT2, "
                StrSql = StrSql + "PALLETIN.D3  AS DEPT3, "
                StrSql = StrSql + "PALLETIN.D4  AS DEPT4, "
                StrSql = StrSql + "PALLETIN.D5  AS DEPT5, "
                StrSql = StrSql + "PALLETIN.D6  AS DEPT6, "
                StrSql = StrSql + "PALLETIN.D7  AS DEPT7, "
                StrSql = StrSql + "PALLETIN.D8  AS DEPT8, "
                StrSql = StrSql + "PALLETIN.D9  AS DEPT9, "
                StrSql = StrSql + "PALLETIN.D10  AS DEPT10, "
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
                StrSql = StrSql + "PALLETIN "
                StrSql = StrSql + "INNER JOIN "
                StrSql = StrSql + "PREFERENCES PREF "
                StrSql = StrSql + "ON "
                StrSql = StrSql + "PREF.CASEID=PALLETIN.CASEID "
                StrSql = StrSql + "INNER JOIN ECON.PALLET  ITEM1 "
                StrSql = StrSql + "ON ITEM1.PALLETID = 	PALLETIN.M1 "
                StrSql = StrSql + "INNER JOIN ECON.PALLET  ITEM2 "
                StrSql = StrSql + "ON ITEM2.PALLETID = 	PALLETIN.M2 "
                StrSql = StrSql + "INNER JOIN ECON.PALLET  ITEM3 "
                StrSql = StrSql + "ON ITEM3.PALLETID = 	PALLETIN.M3 "
                StrSql = StrSql + "INNER JOIN ECON.PALLET  ITEM4 "
                StrSql = StrSql + "ON ITEM4.PALLETID = 	PALLETIN.M4 "
                StrSql = StrSql + "INNER JOIN ECON.PALLET  ITEM5 "
                StrSql = StrSql + "ON ITEM5.PALLETID = 	PALLETIN.M5 "
                StrSql = StrSql + "INNER JOIN ECON.PALLET  ITEM6 "
                StrSql = StrSql + "ON ITEM6.PALLETID = 	PALLETIN.M6 "
                StrSql = StrSql + "INNER JOIN ECON.PALLET  ITEM7 "
                StrSql = StrSql + "ON ITEM7.PALLETID = 	PALLETIN.M7 "
                StrSql = StrSql + "INNER JOIN ECON.PALLET  ITEM8 "
                StrSql = StrSql + "ON ITEM8.PALLETID = 	PALLETIN.M8 "
                StrSql = StrSql + "INNER JOIN ECON.PALLET  ITEM9 "
                StrSql = StrSql + "ON ITEM9.PALLETID = 	PALLETIN.M9 "
                StrSql = StrSql + "INNER JOIN ECON.PALLET  ITEM10 "
                StrSql = StrSql + "ON ITEM10.PALLETID = PALLETIN.M10 "
                StrSql = StrSql + "WHERE PALLETIN.CASEID = " + CaseId.ToString() + ""


                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetPalletInDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPlantConfigDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  DISTINCT  "
                StrSql = StrSql + "PlantCONFIG.CASEID, "
                StrSql = StrSql + "PlantCONFIG.m1 AS DEPTA1, "
                StrSql = StrSql + "PlantCONFIG.m2 AS DEPTA2, "
                StrSql = StrSql + "PlantCONFIG.m3 AS DEPTA3, "
                StrSql = StrSql + "PlantCONFIG.m4 AS DEPTA4, "
                StrSql = StrSql + "PlantCONFIG.m5 AS DEPTA5, "
                StrSql = StrSql + "PlantCONFIG.m6 AS DEPTA6, "
                StrSql = StrSql + "PlantCONFIG.m7 AS DEPTA7, "
                StrSql = StrSql + "PlantCONFIG.m8 AS DEPTA8, "
                StrSql = StrSql + "PlantCONFIG.m9 AS DEPTA9, "
                StrSql = StrSql + "PlantCONFIG.m10 AS DEPTA10, "

                StrSql = StrSql + "PlantCONFIG.t1 AS DEPTB1, "
                StrSql = StrSql + "PlantCONFIG.t2 AS DEPTB2, "
                StrSql = StrSql + "PlantCONFIG.t3 AS DEPTB3, "
                StrSql = StrSql + "PlantCONFIG.t4 AS DEPTB4, "
                StrSql = StrSql + "PlantCONFIG.t5 AS DEPTB5, "
                StrSql = StrSql + "PlantCONFIG.t6 AS DEPTB6, "
                StrSql = StrSql + "PlantCONFIG.t7 AS DEPTB7, "
                StrSql = StrSql + "PlantCONFIG.t8 AS DEPTB8, "
                StrSql = StrSql + "PlantCONFIG.t9 AS DEPTB9, "
                StrSql = StrSql + "PlantCONFIG.t10 AS DEPTB10, "

                StrSql = StrSql + "PlantCONFIG.s1 AS DEPTC1, "
                StrSql = StrSql + "PlantCONFIG.s2 AS DEPTC2, "
                StrSql = StrSql + "PlantCONFIG.s3 AS DEPTC3, "
                StrSql = StrSql + "PlantCONFIG.s4 AS DEPTC4, "
                StrSql = StrSql + "PlantCONFIG.s5 AS DEPTC5, "
                StrSql = StrSql + "PlantCONFIG.s6 AS DEPTC6, "
                StrSql = StrSql + "PlantCONFIG.s7 AS DEPTC7, "
                StrSql = StrSql + "PlantCONFIG.s8 AS DEPTC8, "
                StrSql = StrSql + "PlantCONFIG.s9 AS DEPTC9, "
                StrSql = StrSql + "PlantCONFIG.s10 AS DEPTC10, "

                StrSql = StrSql + "PlantCONFIG.Y1 AS DEPTD1, "
                StrSql = StrSql + "PlantCONFIG.Y2 AS DEPTD2, "
                StrSql = StrSql + "PlantCONFIG.Y3 AS DEPTD3, "
                StrSql = StrSql + "PlantCONFIG.Y4 AS DEPTD4, "
                StrSql = StrSql + "PlantCONFIG.Y5 AS DEPTD5, "
                StrSql = StrSql + "PlantCONFIG.Y6 AS DEPTD6, "
                StrSql = StrSql + "PlantCONFIG.Y7 AS DEPTD7, "
                StrSql = StrSql + "PlantCONFIG.Y8 AS DEPTD8, "
                StrSql = StrSql + "PlantCONFIG.Y9 AS DEPTD9, "
                StrSql = StrSql + "PlantCONFIG.Y10 AS DEPTD10, "

                StrSql = StrSql + "PlantCONFIG.D1 AS DEPTE1, "
                StrSql = StrSql + "PlantCONFIG.D2 AS DEPTE2, "
                StrSql = StrSql + "PlantCONFIG.D3 AS DEPTE3, "
                StrSql = StrSql + "PlantCONFIG.D4 AS DEPTE4, "
                StrSql = StrSql + "PlantCONFIG.D5 AS DEPTE5, "
                StrSql = StrSql + "PlantCONFIG.D6 AS DEPTE6, "
                StrSql = StrSql + "PlantCONFIG.D7 AS DEPTE7, "
                StrSql = StrSql + "PlantCONFIG.D8 AS DEPTE8, "
                StrSql = StrSql + "PlantCONFIG.D9 AS DEPTE9, "
                StrSql = StrSql + "PlantCONFIG.D10 AS DEPTE10, "

                StrSql = StrSql + "PlantCONFIG.Z1 AS DEPTF1, "
                StrSql = StrSql + "PlantCONFIG.Z2 AS DEPTF2, "
                StrSql = StrSql + "PlantCONFIG.Z3 AS DEPTF3, "
                StrSql = StrSql + "PlantCONFIG.Z4 AS DEPTF4, "
                StrSql = StrSql + "PlantCONFIG.Z5 AS DEPTF5, "
                StrSql = StrSql + "PlantCONFIG.Z6 AS DEPTF6, "
                StrSql = StrSql + "PlantCONFIG.Z7 AS DEPTF7, "
                StrSql = StrSql + "PlantCONFIG.Z8 AS DEPTF8, "
                StrSql = StrSql + "PlantCONFIG.Z9 AS DEPTF9, "
                StrSql = StrSql + "PlantCONFIG.Z10 AS DEPTF10, "

                StrSql = StrSql + "PlantCONFIG.B1 AS DEPTG1, "
                StrSql = StrSql + "PlantCONFIG.B2 AS DEPTG2, "
                StrSql = StrSql + "PlantCONFIG.B3 AS DEPTG3, "
                StrSql = StrSql + "PlantCONFIG.B4 AS DEPTG4, "
                StrSql = StrSql + "PlantCONFIG.B5 AS DEPTG5, "
                StrSql = StrSql + "PlantCONFIG.B6 AS DEPTG6, "
                StrSql = StrSql + "PlantCONFIG.B7 AS DEPTG7, "
                StrSql = StrSql + "PlantCONFIG.B8 AS DEPTG8, "
                StrSql = StrSql + "PlantCONFIG.B9 AS DEPTG9, "
                StrSql = StrSql + "PlantCONFIG.B10 AS DEPTG10, "

                StrSql = StrSql + "PlantCONFIG.R1 AS DEPTH1, "
                StrSql = StrSql + "PlantCONFIG.R2 AS DEPTH2, "
                StrSql = StrSql + "PlantCONFIG.R3 AS DEPTH3, "
                StrSql = StrSql + "PlantCONFIG.R4 AS DEPTH4, "
                StrSql = StrSql + "PlantCONFIG.R5 AS DEPTH5, "
                StrSql = StrSql + "PlantCONFIG.R6 AS DEPTH6, "
                StrSql = StrSql + "PlantCONFIG.R7 AS DEPTH7, "
                StrSql = StrSql + "PlantCONFIG.R8 AS DEPTH8, "
                StrSql = StrSql + "PlantCONFIG.R9 AS DEPTH9, "
                StrSql = StrSql + "PlantCONFIG.R10 AS DEPTH10, "

                StrSql = StrSql + "PlantCONFIG.K1 AS DEPTI1, "
                StrSql = StrSql + "PlantCONFIG.K2 AS DEPTI2, "
                StrSql = StrSql + "PlantCONFIG.K3 AS DEPTI3, "
                StrSql = StrSql + "PlantCONFIG.K4 AS DEPTI4, "
                StrSql = StrSql + "PlantCONFIG.K5 AS DEPTI5, "
                StrSql = StrSql + "PlantCONFIG.K6 AS DEPTI6, "
                StrSql = StrSql + "PlantCONFIG.K7 AS DEPTI7, "
                StrSql = StrSql + "PlantCONFIG.K8 AS DEPTI8, "
                StrSql = StrSql + "PlantCONFIG.K9 AS DEPTI9, "
                StrSql = StrSql + "PlantCONFIG.K10 AS DEPTI10, "

                StrSql = StrSql + "PlantCONFIG.P1 AS DEPTJ1, "
                StrSql = StrSql + "PlantCONFIG.P2 AS DEPTJ2, "
                StrSql = StrSql + "PlantCONFIG.P3 AS DEPTJ3, "
                StrSql = StrSql + "PlantCONFIG.P4 AS DEPTJ4, "
                StrSql = StrSql + "PlantCONFIG.P5 AS DEPTJ5, "
                StrSql = StrSql + "PlantCONFIG.P6 AS DEPTJ6, "
                StrSql = StrSql + "PlantCONFIG.P7 AS DEPTJ7, "
                StrSql = StrSql + "PlantCONFIG.P8 AS DEPTJ8, "
                StrSql = StrSql + "PlantCONFIG.P9 AS DEPTJ9, "
                StrSql = StrSql + "PlantCONFIG.P10 AS DEPTJ10 "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "PlantCONFIG "
                StrSql = StrSql + "WHERE PlantCONFIG.CASEID = " + CaseId.ToString() + ""
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetPlanConfigDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetEquipmentInDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT 	EQUIP.CASEID,  "
                StrSql = StrSql + "PREF.TITLE2 AS ASSESTCOSTUNIT, "
                StrSql = StrSql + "PREF.UNITS, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN 'square feet'  ELSE 'square meters' END AS PLANTAREAUNIT, "
                StrSql = StrSql + "EQUIP.M1 AS ASSETID1, "
                StrSql = StrSql + "EQUIP.M2  AS ASSETID2, "
                StrSql = StrSql + "EQUIP.M3  AS ASSETID3, "
                StrSql = StrSql + "EQUIP.M4  AS ASSETID4, "
                StrSql = StrSql + "EQUIP.M5  AS ASSETID5, "
                StrSql = StrSql + "EQUIP.M6  AS ASSETID6, "
                StrSql = StrSql + "EQUIP.M7  AS ASSETID7, "
                StrSql = StrSql + "EQUIP.M8  AS ASSETID8, "
                StrSql = StrSql + "EQUIP.M9 AS ASSETID9, "
                StrSql = StrSql + "EQUIP.M10 AS ASSETID10, "
                StrSql = StrSql + "EQUIP.M11 AS ASSETID11, "
                StrSql = StrSql + "EQUIP.M12 AS ASSETID12, "
                StrSql = StrSql + "EQUIP.M13 AS ASSETID13, "
                StrSql = StrSql + "EQUIP.M14 AS ASSETID14, "
                StrSql = StrSql + "EQUIP.M15 AS ASSETID15, "
                StrSql = StrSql + "EQUIP.M16 AS ASSETID16, "
                StrSql = StrSql + "EQUIP.M17 AS ASSETID17, "
                StrSql = StrSql + "EQUIP.M18 AS ASSETID18, "
                StrSql = StrSql + "EQUIP.M19 AS ASSETID19, "
                StrSql = StrSql + "EQUIP.M20 AS ASSETID20, "
                StrSql = StrSql + "EQUIP.M21 AS ASSETID21, "
                StrSql = StrSql + "EQUIP.M22 AS ASSETID22, "
                StrSql = StrSql + "EQUIP.M23 AS ASSETID23, "
                StrSql = StrSql + "EQUIP.M24 AS ASSETID24, "
                StrSql = StrSql + "EQUIP.M25 AS ASSETID25, "
                StrSql = StrSql + "EQUIP.M26 AS ASSETID26, "
                StrSql = StrSql + "EQUIP.M27 AS ASSETID27, "
                StrSql = StrSql + "EQUIP.M28 AS ASSETID28, "
                StrSql = StrSql + "EQUIP.M29 AS ASSETID29, "
                StrSql = StrSql + "EQUIP.M30 AS ASSETID30, "
                StrSql = StrSql + "(EQUIP1.COST *  PREF.CURR) AS ASSETS1, "
                StrSql = StrSql + "(EQUIP2.COST *  PREF.CURR) AS ASSETS2, "
                StrSql = StrSql + "(EQUIP3.COST *  PREF.CURR) AS ASSETS3, "
                StrSql = StrSql + "(EQUIP4.COST *  PREF.CURR) AS ASSETS4, "
                StrSql = StrSql + "(EQUIP5.COST *  PREF.CURR) AS ASSETS5, "
                StrSql = StrSql + "(EQUIP6.COST *  PREF.CURR) AS ASSETS6, "
                StrSql = StrSql + "(EQUIP7.COST *  PREF.CURR) AS ASSETS7, "
                StrSql = StrSql + "(EQUIP8.COST *  PREF.CURR) AS ASSETS8, "
                StrSql = StrSql + "(EQUIP9.COST *  PREF.CURR) AS ASSETS9, "
                StrSql = StrSql + "(EQUIP10.COST *  PREF.CURR) AS ASSETS10, "
                StrSql = StrSql + "(EQUIP11.COST *  PREF.CURR) AS ASSETS11, "
                StrSql = StrSql + "(EQUIP12.COST *  PREF.CURR) AS ASSETS12, "
                StrSql = StrSql + "(EQUIP13.COST *  PREF.CURR) AS ASSETS13, "
                StrSql = StrSql + "(EQUIP14.COST *  PREF.CURR) AS ASSETS14, "
                StrSql = StrSql + "(EQUIP15.COST *  PREF.CURR) AS ASSETS15, "
                StrSql = StrSql + "(EQUIP16.COST *  PREF.CURR) AS ASSETS16, "
                StrSql = StrSql + "(EQUIP17.COST *  PREF.CURR) AS ASSETS17, "
                StrSql = StrSql + "(EQUIP18.COST *  PREF.CURR) AS ASSETS18, "
                StrSql = StrSql + "(EQUIP19.COST *  PREF.CURR) AS ASSETS19, "
                StrSql = StrSql + "(EQUIP20.COST *  PREF.CURR) AS ASSETS20, "
                StrSql = StrSql + "(EQUIP21.COST *  PREF.CURR) AS ASSETS21, "
                StrSql = StrSql + "(EQUIP22.COST *  PREF.CURR) AS ASSETS22, "
                StrSql = StrSql + "(EQUIP23.COST *  PREF.CURR) AS ASSETS23, "
                StrSql = StrSql + "(EQUIP24.COST *  PREF.CURR) AS ASSETS24, "
                StrSql = StrSql + "(EQUIP25.COST *  PREF.CURR) AS ASSETS25, "
                StrSql = StrSql + "(EQUIP26.COST *  PREF.CURR) AS ASSETS26, "
                StrSql = StrSql + "(EQUIP27.COST *  PREF.CURR) AS ASSETS27, "
                StrSql = StrSql + "(EQUIP28.COST *  PREF.CURR) AS ASSETS28, "
                StrSql = StrSql + "(EQUIP29.COST *  PREF.CURR) AS ASSETS29, "
                StrSql = StrSql + "(EQUIP30.COST *  PREF.CURR) AS ASSETS30, "
                StrSql = StrSql + "(EQCOS.M1*  PREF.CURR) AS ASSETP1, "
                StrSql = StrSql + "(EQCOS.M2*  PREF.CURR) AS ASSETP2, "
                StrSql = StrSql + "(EQCOS.M3*  PREF.CURR) AS ASSETP3, "
                StrSql = StrSql + "(EQCOS.M4*  PREF.CURR) AS ASSETP4, "
                StrSql = StrSql + "(EQCOS.M5*  PREF.CURR) AS ASSETP5, "
                StrSql = StrSql + "(EQCOS.M6*  PREF.CURR) AS ASSETP6, "
                StrSql = StrSql + "(EQCOS.M7*  PREF.CURR) AS ASSETP7, "
                StrSql = StrSql + "(EQCOS.M8*  PREF.CURR) AS ASSETP8, "
                StrSql = StrSql + "(EQCOS.M9*  PREF.CURR) AS ASSETP9, "
                StrSql = StrSql + "(EQCOS.M10*  PREF.CURR) AS ASSETP10, "
                StrSql = StrSql + "(EQCOS.M11*  PREF.CURR) AS ASSETP11, "
                StrSql = StrSql + "(EQCOS.M12*  PREF.CURR) AS ASSETP12, "
                StrSql = StrSql + "(EQCOS.M13*  PREF.CURR) AS ASSETP13, "
                StrSql = StrSql + "(EQCOS.M14*  PREF.CURR) AS ASSETP14, "
                StrSql = StrSql + "(EQCOS.M15*  PREF.CURR) AS ASSETP15, "
                StrSql = StrSql + "(EQCOS.M16*  PREF.CURR) AS ASSETP16, "
                StrSql = StrSql + "(EQCOS.M17*  PREF.CURR) AS ASSETP17, "
                StrSql = StrSql + "(EQCOS.M18*  PREF.CURR) AS ASSETP18, "
                StrSql = StrSql + "(EQCOS.M19*  PREF.CURR) AS ASSETP19, "
                StrSql = StrSql + "(EQCOS.M20*  PREF.CURR) AS ASSETP20, "
                StrSql = StrSql + "(EQCOS.M21*  PREF.CURR) AS ASSETP21, "
                StrSql = StrSql + "(EQCOS.M22*  PREF.CURR) AS ASSETP22, "
                StrSql = StrSql + "(EQCOS.M23*  PREF.CURR) AS ASSETP23, "
                StrSql = StrSql + "(EQCOS.M24*  PREF.CURR) AS ASSETP24, "
                StrSql = StrSql + "(EQCOS.M25*  PREF.CURR) AS ASSETP25, "
                StrSql = StrSql + "(EQCOS.M26*  PREF.CURR) AS ASSETP26, "
                StrSql = StrSql + "(EQCOS.M27*  PREF.CURR) AS ASSETP27, "
                StrSql = StrSql + "(EQCOS.M28*  PREF.CURR) AS ASSETP28, "
                StrSql = StrSql + "(EQCOS.M29*  PREF.CURR) AS ASSETP29, "
                StrSql = StrSql + "(EQCOS.M30*  PREF.CURR) AS ASSETP30, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP1.AREATYPE )AREADE1, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP2.AREATYPE )AREADE2, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP3.AREATYPE )AREADE3, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP4.AREATYPE )AREADE4, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP5.AREATYPE )AREADE5, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP6.AREATYPE )AREADE6, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP7.AREATYPE )AREADE7, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP8.AREATYPE )AREADE8, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP9.AREATYPE )AREADE9, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP10.AREATYPE )AREADE10, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP11.AREATYPE )AREADE11, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP12.AREATYPE )AREADE12, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP13.AREATYPE )AREADE13, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP14.AREATYPE )AREADE14, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP15.AREATYPE )AREADE15, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP16.AREATYPE )AREADE16, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP17.AREATYPE )AREADE17, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP18.AREATYPE )AREADE18, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP19.AREATYPE )AREADE19, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP20.AREATYPE )AREADE20, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP21.AREATYPE )AREADE21, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP22.AREATYPE )AREADE22, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP23.AREATYPE )AREADE23, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP24.AREATYPE )AREADE24, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP25.AREATYPE )AREADE25, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP26.AREATYPE )AREADE26, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP27.AREATYPE )AREADE27, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP28.AREATYPE )AREADE28, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP29.AREATYPE )AREADE29, "
                StrSql = StrSql + "(SELECT AREADE1||' '||AREADE2 FROM ECON.AREATYPE WHERE AREAID=EQUIP30.AREATYPE )AREADE30, "
                StrSql = StrSql + "(EQUIP1.AREA *  PREF.CONVAREA2) AS PARS1, "
                StrSql = StrSql + "(EQUIP2.AREA *  PREF.CONVAREA2) AS PARS2, "
                StrSql = StrSql + "(EQUIP3.AREA *  PREF.CONVAREA2) AS PARS3, "
                StrSql = StrSql + "(EQUIP4.AREA *  PREF.CONVAREA2) AS PARS4, "
                StrSql = StrSql + "(EQUIP5.AREA *  PREF.CONVAREA2) AS PARS5, "
                StrSql = StrSql + "(EQUIP6.AREA *  PREF.CONVAREA2) AS PARS6, "
                StrSql = StrSql + "(EQUIP7.AREA *  PREF.CONVAREA2) AS PARS7, "
                StrSql = StrSql + "(EQUIP8.AREA *  PREF.CONVAREA2) AS PARS8, "
                StrSql = StrSql + "(EQUIP9.AREA *  PREF.CONVAREA2) AS PARS9, "
                StrSql = StrSql + "(EQUIP10.AREA *  PREF.CONVAREA2) AS PARS10, "
                StrSql = StrSql + "(EQUIP11.AREA *  PREF.CONVAREA2) AS PARS11, "
                StrSql = StrSql + "(EQUIP12.AREA *  PREF.CONVAREA2) AS PARS12, "
                StrSql = StrSql + "(EQUIP13.AREA *  PREF.CONVAREA2) AS PARS13, "
                StrSql = StrSql + "(EQUIP14.AREA *  PREF.CONVAREA2) AS PARS14, "
                StrSql = StrSql + "(EQUIP15.AREA *  PREF.CONVAREA2) AS PARS15, "
                StrSql = StrSql + "(EQUIP16.AREA *  PREF.CONVAREA2) AS PARS16, "
                StrSql = StrSql + "(EQUIP17.AREA *  PREF.CONVAREA2) AS PARS17, "
                StrSql = StrSql + "(EQUIP18.AREA *  PREF.CONVAREA2) AS PARS18, "
                StrSql = StrSql + "(EQUIP19.AREA *  PREF.CONVAREA2) AS PARS19, "
                StrSql = StrSql + "(EQUIP20.AREA *  PREF.CONVAREA2) AS PARS20, "
                StrSql = StrSql + "(EQUIP21.AREA *  PREF.CONVAREA2) AS PARS21, "
                StrSql = StrSql + "(EQUIP22.AREA *  PREF.CONVAREA2) AS PARS22, "
                StrSql = StrSql + "(EQUIP23.AREA *  PREF.CONVAREA2) AS PARS23, "
                StrSql = StrSql + "(EQUIP24.AREA *  PREF.CONVAREA2) AS PARS24, "
                StrSql = StrSql + "(EQUIP25.AREA *  PREF.CONVAREA2) AS PARS25, "
                StrSql = StrSql + "(EQUIP26.AREA *  PREF.CONVAREA2) AS PARS26, "
                StrSql = StrSql + "(EQUIP27.AREA *  PREF.CONVAREA2) AS PARS27, "
                StrSql = StrSql + "(EQUIP28.AREA *  PREF.CONVAREA2) AS PARS28, "
                StrSql = StrSql + "(EQUIP29.AREA *  PREF.CONVAREA2) AS PARS29, "
                StrSql = StrSql + "(EQUIP30.AREA *  PREF.CONVAREA2) AS PARS30, "
                StrSql = StrSql + "(EQAR.M1*  PREF.CONVAREA2) AS PARP1, "
                StrSql = StrSql + "(EQAR.M2*  PREF.CONVAREA2) AS PARP2, "
                StrSql = StrSql + "(EQAR.M3*  PREF.CONVAREA2) AS PARP3, "
                StrSql = StrSql + "(EQAR.M4*  PREF.CONVAREA2) AS PARP4, "
                StrSql = StrSql + "(EQAR.M5*  PREF.CONVAREA2) AS PARP5, "
                StrSql = StrSql + "(EQAR.M6*  PREF.CONVAREA2) AS PARP6, "
                StrSql = StrSql + "(EQAR.M7*  PREF.CONVAREA2) AS PARP7, "
                StrSql = StrSql + "(EQAR.M8*  PREF.CONVAREA2) AS PARP8, "
                StrSql = StrSql + "(EQAR.M9*  PREF.CONVAREA2) AS PARP9, "
                StrSql = StrSql + "(EQAR.M10*  PREF.CONVAREA2) AS PARP10, "
                StrSql = StrSql + "(EQAR.M11*  PREF.CONVAREA2) AS PARP11, "
                StrSql = StrSql + "(EQAR.M12*  PREF.CONVAREA2) AS PARP12, "
                StrSql = StrSql + "(EQAR.M13*  PREF.CONVAREA2) AS PARP13, "
                StrSql = StrSql + "(EQAR.M14*  PREF.CONVAREA2) AS PARP14, "
                StrSql = StrSql + "(EQAR.M15*  PREF.CONVAREA2) AS PARP15, "
                StrSql = StrSql + "(EQAR.M16*  PREF.CONVAREA2) AS PARP16, "
                StrSql = StrSql + "(EQAR.M17*  PREF.CONVAREA2) AS PARP17, "
                StrSql = StrSql + "(EQAR.M18*  PREF.CONVAREA2) AS PARP18, "
                StrSql = StrSql + "(EQAR.M19*  PREF.CONVAREA2) AS PARP19, "
                StrSql = StrSql + "(EQAR.M20*  PREF.CONVAREA2) AS PARP20, "
                StrSql = StrSql + "(EQAR.M21*  PREF.CONVAREA2) AS PARP21, "
                StrSql = StrSql + "(EQAR.M22*  PREF.CONVAREA2) AS PARP22, "
                StrSql = StrSql + "(EQAR.M23*  PREF.CONVAREA2) AS PARP23, "
                StrSql = StrSql + "(EQAR.M24*  PREF.CONVAREA2) AS PARP24, "
                StrSql = StrSql + "(EQAR.M25*  PREF.CONVAREA2) AS PARP25, "
                StrSql = StrSql + "(EQAR.M26*  PREF.CONVAREA2) AS PARP26, "
                StrSql = StrSql + "(EQAR.M27*  PREF.CONVAREA2) AS PARP27, "
                StrSql = StrSql + "(EQAR.M28*  PREF.CONVAREA2) AS PARP28, "
                StrSql = StrSql + "(EQAR.M29*  PREF.CONVAREA2) AS PARP29, "
                StrSql = StrSql + "(EQAR.M30*  PREF.CONVAREA2) AS PARP30, "
                StrSql = StrSql + "EQUIP1.INSTKW AS ECS1, "
                StrSql = StrSql + "EQUIP2.INSTKW AS ECS2, "
                StrSql = StrSql + "EQUIP3.INSTKW AS ECS3, "
                StrSql = StrSql + "EQUIP4.INSTKW AS ECS4, "
                StrSql = StrSql + "EQUIP5.INSTKW AS ECS5, "
                StrSql = StrSql + "EQUIP6.INSTKW AS ECS6, "
                StrSql = StrSql + "EQUIP7.INSTKW AS ECS7, "
                StrSql = StrSql + "EQUIP8.INSTKW AS ECS8, "
                StrSql = StrSql + "EQUIP9.INSTKW AS ECS9, "
                StrSql = StrSql + "EQUIP10.INSTKW AS ECS10, "
                StrSql = StrSql + "EQUIP11.INSTKW AS ECS11, "
                StrSql = StrSql + "EQUIP12.INSTKW AS ECS12, "
                StrSql = StrSql + "EQUIP13.INSTKW AS ECS13, "
                StrSql = StrSql + "EQUIP14.INSTKW AS ECS14, "
                StrSql = StrSql + "EQUIP15.INSTKW AS ECS15, "
                StrSql = StrSql + "EQUIP16.INSTKW AS ECS16, "
                StrSql = StrSql + "EQUIP17.INSTKW AS ECS17, "
                StrSql = StrSql + "EQUIP18.INSTKW AS ECS18, "
                StrSql = StrSql + "EQUIP19.INSTKW AS ECS19, "
                StrSql = StrSql + "EQUIP20.INSTKW AS ECS20, "
                StrSql = StrSql + "EQUIP21.INSTKW AS ECS21, "
                StrSql = StrSql + "EQUIP22.INSTKW AS ECS22, "
                StrSql = StrSql + "EQUIP23.INSTKW AS ECS23, "
                StrSql = StrSql + "EQUIP24.INSTKW AS ECS24, "
                StrSql = StrSql + "EQUIP25.INSTKW AS ECS25, "
                StrSql = StrSql + "EQUIP26.INSTKW AS ECS26, "
                StrSql = StrSql + "EQUIP27.INSTKW AS ECS27, "
                StrSql = StrSql + "EQUIP28.INSTKW AS ECS28, "
                StrSql = StrSql + "EQUIP29.INSTKW AS ECS29, "
                StrSql = StrSql + "EQUIP30.INSTKW AS ECS30, "
                StrSql = StrSql + "EQEGPREF.M1 AS ECP1, "
                StrSql = StrSql + "EQEGPREF.M2 AS ECP2, "
                StrSql = StrSql + "EQEGPREF.M3 AS ECP3, "
                StrSql = StrSql + "EQEGPREF.M4 AS ECP4, "
                StrSql = StrSql + "EQEGPREF.M5 AS ECP5, "
                StrSql = StrSql + "EQEGPREF.M6 AS ECP6, "
                StrSql = StrSql + "EQEGPREF.M7 AS ECP7, "
                StrSql = StrSql + "EQEGPREF.M8 AS ECP8, "
                StrSql = StrSql + "EQEGPREF.M9 AS ECP9, "
                StrSql = StrSql + "EQEGPREF.M10 AS ECP10, "
                StrSql = StrSql + "EQEGPREF.M11 AS ECP11, "
                StrSql = StrSql + "EQEGPREF.M12 AS ECP12, "
                StrSql = StrSql + "EQEGPREF.M13 AS ECP13, "
                StrSql = StrSql + "EQEGPREF.M14 AS ECP14, "
                StrSql = StrSql + "EQEGPREF.M15 AS ECP15, "
                StrSql = StrSql + "EQEGPREF.M16 AS ECP16, "
                StrSql = StrSql + "EQEGPREF.M17 AS ECP17, "
                StrSql = StrSql + "EQEGPREF.M18 AS ECP18, "
                StrSql = StrSql + "EQEGPREF.M19 AS ECP19, "
                StrSql = StrSql + "EQEGPREF.M20 AS ECP20, "
                StrSql = StrSql + "EQEGPREF.M21 AS ECP21, "
                StrSql = StrSql + "EQEGPREF.M22 AS ECP22, "
                StrSql = StrSql + "EQEGPREF.M23 AS ECP23, "
                StrSql = StrSql + "EQEGPREF.M24 AS ECP24, "
                StrSql = StrSql + "EQEGPREF.M25 AS ECP25, "
                StrSql = StrSql + "EQEGPREF.M26 AS ECP26, "
                StrSql = StrSql + "EQEGPREF.M27 AS ECP27, "
                StrSql = StrSql + "EQEGPREF.M28 AS ECP28, "
                StrSql = StrSql + "EQEGPREF.M29 AS ECP29, "
                StrSql = StrSql + "EQEGPREF.M30 AS ECP30, "
                StrSql = StrSql + "EQUIP1.NTGKW AS NGCS1, "
                StrSql = StrSql + "EQUIP2.NTGKW AS NGCS2, "
                StrSql = StrSql + "EQUIP3.NTGKW AS NGCS3, "
                StrSql = StrSql + "EQUIP4.NTGKW AS NGCS4, "
                StrSql = StrSql + "EQUIP5.NTGKW AS NGCS5, "
                StrSql = StrSql + "EQUIP6.NTGKW AS NGCS6, "
                StrSql = StrSql + "EQUIP7.NTGKW AS NGCS7, "
                StrSql = StrSql + "EQUIP8.NTGKW AS NGCS8, "
                StrSql = StrSql + "EQUIP9.NTGKW AS NGCS9, "
                StrSql = StrSql + "EQUIP10.NTGKW AS NGCS10, "
                StrSql = StrSql + "EQUIP11.NTGKW AS NGCS11, "
                StrSql = StrSql + "EQUIP12.NTGKW AS NGCS12, "
                StrSql = StrSql + "EQUIP13.NTGKW AS NGCS13, "
                StrSql = StrSql + "EQUIP14.NTGKW AS NGCS14, "
                StrSql = StrSql + "EQUIP15.NTGKW AS NGCS15, "
                StrSql = StrSql + "EQUIP16.NTGKW AS NGCS16, "
                StrSql = StrSql + "EQUIP17.NTGKW AS NGCS17, "
                StrSql = StrSql + "EQUIP18.NTGKW AS NGCS18, "
                StrSql = StrSql + "EQUIP19.NTGKW AS NGCS19, "
                StrSql = StrSql + "EQUIP20.NTGKW AS NGCS20, "
                StrSql = StrSql + "EQUIP21.NTGKW AS NGCS21, "
                StrSql = StrSql + "EQUIP22.NTGKW AS NGCS22, "
                StrSql = StrSql + "EQUIP23.NTGKW AS NGCS23, "
                StrSql = StrSql + "EQUIP24.NTGKW AS NGCS24, "
                StrSql = StrSql + "EQUIP25.NTGKW AS NGCS25, "
                StrSql = StrSql + "EQUIP26.NTGKW AS NGCS26, "
                StrSql = StrSql + "EQUIP27.NTGKW AS NGCS27, "
                StrSql = StrSql + "EQUIP28.NTGKW AS NGCS28, "
                StrSql = StrSql + "EQUIP29.NTGKW AS NGCS29, "
                StrSql = StrSql + "EQUIP30.NTGKW AS NGCS30, "
                StrSql = StrSql + "EQNGPREF.M1 AS NGCP1, "
                StrSql = StrSql + "EQNGPREF.M2 AS NGCP2, "
                StrSql = StrSql + "EQNGPREF.M3 AS NGCP3, "
                StrSql = StrSql + "EQNGPREF.M4 AS NGCP4, "
                StrSql = StrSql + "EQNGPREF.M5 AS NGCP5, "
                StrSql = StrSql + "EQNGPREF.M6 AS NGCP6, "
                StrSql = StrSql + "EQNGPREF.M7 AS NGCP7, "
                StrSql = StrSql + "EQNGPREF.M8 AS NGCP8, "
                StrSql = StrSql + "EQNGPREF.M9 AS NGCP9, "
                StrSql = StrSql + "EQNGPREF.M10 AS NGCP10, "
                StrSql = StrSql + "EQNGPREF.M11 AS NGCP11, "
                StrSql = StrSql + "EQNGPREF.M12 AS NGCP12, "
                StrSql = StrSql + "EQNGPREF.M13 AS NGCP13, "
                StrSql = StrSql + "EQNGPREF.M14 AS NGCP14, "
                StrSql = StrSql + "EQNGPREF.M15 AS NGCP15, "
                StrSql = StrSql + "EQNGPREF.M16 AS NGCP16, "
                StrSql = StrSql + "EQNGPREF.M17 AS NGCP17, "
                StrSql = StrSql + "EQNGPREF.M18 AS NGCP18, "
                StrSql = StrSql + "EQNGPREF.M19 AS NGCP19, "
                StrSql = StrSql + "EQNGPREF.M20 AS NGCP20, "
                StrSql = StrSql + "EQNGPREF.M21 AS NGCP21, "
                StrSql = StrSql + "EQNGPREF.M22 AS NGCP22, "
                StrSql = StrSql + "EQNGPREF.M23 AS NGCP23, "
                StrSql = StrSql + "EQNGPREF.M24 AS NGCP24, "
                StrSql = StrSql + "EQNGPREF.M25 AS NGCP25, "
                StrSql = StrSql + "EQNGPREF.M26 AS NGCP26, "
                StrSql = StrSql + "EQNGPREF.M27 AS NGCP27, "
                StrSql = StrSql + "EQNGPREF.M28 AS NGCP28, "
                StrSql = StrSql + "EQNGPREF.M29 AS NGCP29, "
                StrSql = StrSql + "EQNGPREF.M30 AS NGCP30, "
                StrSql = StrSql + "EQDEP.M1 AS DEP1, "
                StrSql = StrSql + "EQDEP.M2 AS DEP2, "
                StrSql = StrSql + "EQDEP.M3 AS DEP3, "
                StrSql = StrSql + "EQDEP.M4 AS DEP4, "
                StrSql = StrSql + "EQDEP.M5 AS DEP5, "
                StrSql = StrSql + "EQDEP.M6 AS DEP6, "
                StrSql = StrSql + "EQDEP.M7 AS DEP7, "
                StrSql = StrSql + "EQDEP.M8 AS DEP8, "
                StrSql = StrSql + "EQDEP.M9 AS DEP9, "
                StrSql = StrSql + "EQDEP.M10 AS DEP10, "
                StrSql = StrSql + "EQDEP.M11 AS DEP11, "
                StrSql = StrSql + "EQDEP.M12 AS DEP12, "
                StrSql = StrSql + "EQDEP.M13 AS DEP13, "
                StrSql = StrSql + "EQDEP.M14 AS DEP14, "
                StrSql = StrSql + "EQDEP.M15 AS DEP15, "
                StrSql = StrSql + "EQDEP.M16 AS DEP16, "
                StrSql = StrSql + "EQDEP.M17 AS DEP17, "
                StrSql = StrSql + "EQDEP.M18 AS DEP18, "
                StrSql = StrSql + "EQDEP.M19 AS DEP19, "
                StrSql = StrSql + "EQDEP.M20 AS DEP20, "
                StrSql = StrSql + "EQDEP.M21 AS DEP21, "
                StrSql = StrSql + "EQDEP.M22 AS DEP22, "
                StrSql = StrSql + "EQDEP.M23 AS DEP23, "
                StrSql = StrSql + "EQDEP.M24 AS DEP24, "
                StrSql = StrSql + "EQDEP.M25 AS DEP25, "
                StrSql = StrSql + "EQDEP.M26 AS DEP26, "
                StrSql = StrSql + "EQDEP.M27 AS DEP27, "
                StrSql = StrSql + "EQDEP.M28 AS DEP28, "
                StrSql = StrSql + "EQDEP.M29 AS DEP29, "
                StrSql = StrSql + "EQDEP.M30 AS DEP30, "
                'BUG#344
                StrSql = StrSql + "EQNUM.M1 AS NUM1, "
                StrSql = StrSql + "EQNUM.M2 AS NUM2, "
                StrSql = StrSql + "EQNUM.M3 AS NUM3, "
                StrSql = StrSql + "EQNUM.M4 AS NUM4, "
                StrSql = StrSql + "EQNUM.M5 AS NUM5, "
                StrSql = StrSql + "EQNUM.M6 AS NUM6, "
                StrSql = StrSql + "EQNUM.M7 AS NUM7, "
                StrSql = StrSql + "EQNUM.M8 AS NUM8, "
                StrSql = StrSql + "EQNUM.M9 AS NUM9, "
                StrSql = StrSql + "EQNUM.M10 AS NUM10, "
                StrSql = StrSql + "EQNUM.M11 AS NUM11, "
                StrSql = StrSql + "EQNUM.M12 AS NUM12, "
                StrSql = StrSql + "EQNUM.M13 AS NUM13, "
                StrSql = StrSql + "EQNUM.M14 AS NUM14, "
                StrSql = StrSql + "EQNUM.M15 AS NUM15, "
                StrSql = StrSql + "EQNUM.M16 AS NUM16, "
                StrSql = StrSql + "EQNUM.M17 AS NUM17, "
                StrSql = StrSql + "EQNUM.M18 AS NUM18, "
                StrSql = StrSql + "EQNUM.M19 AS NUM19, "
                StrSql = StrSql + "EQNUM.M20 AS NUM20, "
                StrSql = StrSql + "EQNUM.M21 AS NUM21, "
                StrSql = StrSql + "EQNUM.M22 AS NUM22, "
                StrSql = StrSql + "EQNUM.M23 AS NUM23, "
                StrSql = StrSql + "EQNUM.M24 AS NUM24, "
                StrSql = StrSql + "EQNUM.M25 AS NUM25, "
                StrSql = StrSql + "EQNUM.M26 AS NUM26, "
                StrSql = StrSql + "EQNUM.M27 AS NUM27, "
                StrSql = StrSql + "EQNUM.M28 AS NUM28, "
                StrSql = StrSql + "EQNUM.M29 AS NUM29, "
                StrSql = StrSql + "EQNUM.M30 AS NUM30 "
                'END BUG#344
                StrSql = StrSql + "FROM EQUIPMENTTYPE EQUIP "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP1 "
                StrSql = StrSql + "ON EQUIP1.EQUIPID=EQUIP.M1 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP2 "
                StrSql = StrSql + "ON EQUIP2.EQUIPID=EQUIP.M2 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP3 "
                StrSql = StrSql + "ON EQUIP3.EQUIPID=EQUIP.M3 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP4 "
                StrSql = StrSql + "ON EQUIP4.EQUIPID=EQUIP.M4 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP5 "
                StrSql = StrSql + "ON EQUIP5.EQUIPID=EQUIP.M5 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP6 "
                StrSql = StrSql + "ON EQUIP6.EQUIPID=EQUIP.M6 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP7 "
                StrSql = StrSql + "ON EQUIP7.EQUIPID=EQUIP.M7 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP8 "
                StrSql = StrSql + "ON EQUIP8.EQUIPID=EQUIP.M8 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP9 "
                StrSql = StrSql + "ON EQUIP9.EQUIPID=EQUIP.M9 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP10 "
                StrSql = StrSql + "ON EQUIP10.EQUIPID=EQUIP.M10 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP11 "
                StrSql = StrSql + "ON EQUIP11.EQUIPID=EQUIP.M11 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP12 "
                StrSql = StrSql + "ON EQUIP12.EQUIPID=EQUIP.M12 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP13 "
                StrSql = StrSql + "ON EQUIP13.EQUIPID=EQUIP.M13 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP14 "
                StrSql = StrSql + "ON EQUIP14.EQUIPID=EQUIP.M14 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP15 "
                StrSql = StrSql + "ON EQUIP15.EQUIPID=EQUIP.M15 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP16 "
                StrSql = StrSql + "ON EQUIP16.EQUIPID=EQUIP.M16 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP17 "
                StrSql = StrSql + "ON EQUIP17.EQUIPID=EQUIP.M17 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP18 "
                StrSql = StrSql + "ON EQUIP18.EQUIPID=EQUIP.M18 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP19 "
                StrSql = StrSql + "ON EQUIP19.EQUIPID=EQUIP.M19 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP20 "
                StrSql = StrSql + "ON EQUIP20.EQUIPID=EQUIP.M20 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP21 "
                StrSql = StrSql + "ON EQUIP21.EQUIPID=EQUIP.M21 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP22 "
                StrSql = StrSql + "ON EQUIP22.EQUIPID=EQUIP.M22 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP23 "
                StrSql = StrSql + "ON EQUIP23.EQUIPID=EQUIP.M23 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP24 "
                StrSql = StrSql + "ON EQUIP24.EQUIPID=EQUIP.M24 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP25 "
                StrSql = StrSql + "ON EQUIP25.EQUIPID=EQUIP.M25 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP26 "
                StrSql = StrSql + "ON EQUIP26.EQUIPID=EQUIP.M26 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP27 "
                StrSql = StrSql + "ON EQUIP27.EQUIPID=EQUIP.M27 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP28 "
                StrSql = StrSql + "ON EQUIP28.EQUIPID=EQUIP.M28 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP29 "
                StrSql = StrSql + "ON EQUIP29.EQUIPID=EQUIP.M29 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT  EQUIP30 "
                StrSql = StrSql + "ON EQUIP30.EQUIPID=EQUIP.M30 "
                StrSql = StrSql + "INNER JOIN PREFERENCES  PREF ON "
                StrSql = StrSql + "PREF.CASEID=EQUIP.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIPMENTCOST EQCOS "
                StrSql = StrSql + "ON EQCOS.CASEID=EQUIP.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIPMENTAREA EQAR "
                StrSql = StrSql + "ON EQAR.CASEID=EQUIP.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIPENERGYPREF EQEGPREF "
                StrSql = StrSql + "ON EQEGPREF.CASEID=EQUIP.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIPNATURALGASPREF EQNGPREF "
                StrSql = StrSql + "ON EQNGPREF.CASEID=EQUIP.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIPMENTDEP EQDEP "
                StrSql = StrSql + "ON EQDEP.CASEID=EQUIP.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIPMENTNUMBER EQNUM "
                StrSql = StrSql + "ON EQNUM.CASEID=EQUIP.CASEID "
                StrSql = StrSql + "WHERE EQUIP.CASEID=" + CaseId.ToString() + ""

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetEquipmentDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSupportEquipmentInDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT 	EQUIP.CASEID,  "
                StrSql = StrSql + "PREF.TITLE2 AS ASSESTCOSTUNIT, "
                StrSql = StrSql + "PREF.UNITS, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN 'SQUARE FEET'  ELSE 'SQUARE METERS' END AS PLANTAREAUNIT, "
                StrSql = StrSql + "EQUIP.M1 AS ASSETID1, "
                StrSql = StrSql + "EQUIP.M2  AS ASSETID2, "
                StrSql = StrSql + "EQUIP.M3  AS ASSETID3, "
                StrSql = StrSql + "EQUIP.M4  AS ASSETID4, "
                StrSql = StrSql + "EQUIP.M5  AS ASSETID5, "
                StrSql = StrSql + "EQUIP.M6  AS ASSETID6, "
                StrSql = StrSql + "EQUIP.M7  AS ASSETID7, "
                StrSql = StrSql + "EQUIP.M8  AS ASSETID8, "
                StrSql = StrSql + "EQUIP.M9 AS ASSETID9, "
                StrSql = StrSql + "EQUIP.M10 AS ASSETID10, "
                StrSql = StrSql + "EQUIP.M11 AS ASSETID11, "
                StrSql = StrSql + "EQUIP.M12 AS ASSETID12, "
                StrSql = StrSql + "EQUIP.M13 AS ASSETID13, "
                StrSql = StrSql + "EQUIP.M14 AS ASSETID14, "
                StrSql = StrSql + "EQUIP.M15 AS ASSETID15, "
                StrSql = StrSql + "EQUIP.M16 AS ASSETID16, "
                StrSql = StrSql + "EQUIP.M17 AS ASSETID17, "
                StrSql = StrSql + "EQUIP.M18 AS ASSETID18, "
                StrSql = StrSql + "EQUIP.M19 AS ASSETID19, "
                StrSql = StrSql + "EQUIP.M20 AS ASSETID20, "
                StrSql = StrSql + "EQUIP.M21 AS ASSETID21, "
                StrSql = StrSql + "EQUIP.M22 AS ASSETID22, "
                StrSql = StrSql + "EQUIP.M23 AS ASSETID23, "
                StrSql = StrSql + "EQUIP.M24 AS ASSETID24, "
                StrSql = StrSql + "EQUIP.M25 AS ASSETID25, "
                StrSql = StrSql + "EQUIP.M26 AS ASSETID26, "
                StrSql = StrSql + "EQUIP.M27 AS ASSETID27, "
                StrSql = StrSql + "EQUIP.M28 AS ASSETID28, "
                StrSql = StrSql + "EQUIP.M29 AS ASSETID29, "
                StrSql = StrSql + "EQUIP.M30 AS ASSETID30, "
                StrSql = StrSql + "(EQUIP1.COST *  PREF.CURR) AS ASSETS1, "
                StrSql = StrSql + "(EQUIP2.COST *  PREF.CURR) AS ASSETS2, "
                StrSql = StrSql + "(EQUIP3.COST *  PREF.CURR) AS ASSETS3, "
                StrSql = StrSql + "(EQUIP4.COST *  PREF.CURR) AS ASSETS4, "
                StrSql = StrSql + "(EQUIP5.COST *  PREF.CURR) AS ASSETS5, "
                StrSql = StrSql + "(EQUIP6.COST *  PREF.CURR) AS ASSETS6, "
                StrSql = StrSql + "(EQUIP7.COST *  PREF.CURR) AS ASSETS7, "
                StrSql = StrSql + "(EQUIP8.COST *  PREF.CURR) AS ASSETS8, "
                StrSql = StrSql + "(EQUIP9.COST *  PREF.CURR) AS ASSETS9, "
                StrSql = StrSql + "(EQUIP10.COST *  PREF.CURR) AS ASSETS10, "
                StrSql = StrSql + "(EQUIP11.COST *  PREF.CURR) AS ASSETS11, "
                StrSql = StrSql + "(EQUIP12.COST *  PREF.CURR) AS ASSETS12, "
                StrSql = StrSql + "(EQUIP13.COST *  PREF.CURR) AS ASSETS13, "
                StrSql = StrSql + "(EQUIP14.COST *  PREF.CURR) AS ASSETS14, "
                StrSql = StrSql + "(EQUIP15.COST *  PREF.CURR) AS ASSETS15, "
                StrSql = StrSql + "(EQUIP16.COST *  PREF.CURR) AS ASSETS16, "
                StrSql = StrSql + "(EQUIP17.COST *  PREF.CURR) AS ASSETS17, "
                StrSql = StrSql + "(EQUIP18.COST *  PREF.CURR) AS ASSETS18, "
                StrSql = StrSql + "(EQUIP19.COST *  PREF.CURR) AS ASSETS19, "
                StrSql = StrSql + "(EQUIP20.COST *  PREF.CURR) AS ASSETS20, "
                StrSql = StrSql + "(EQUIP21.COST *  PREF.CURR) AS ASSETS21, "
                StrSql = StrSql + "(EQUIP22.COST *  PREF.CURR) AS ASSETS22, "
                StrSql = StrSql + "(EQUIP23.COST *  PREF.CURR) AS ASSETS23, "
                StrSql = StrSql + "(EQUIP24.COST *  PREF.CURR) AS ASSETS24, "
                StrSql = StrSql + "(EQUIP25.COST *  PREF.CURR) AS ASSETS25, "
                StrSql = StrSql + "(EQUIP26.COST *  PREF.CURR) AS ASSETS26, "
                StrSql = StrSql + "(EQUIP27.COST *  PREF.CURR) AS ASSETS27, "
                StrSql = StrSql + "(EQUIP28.COST *  PREF.CURR) AS ASSETS28, "
                StrSql = StrSql + "(EQUIP29.COST *  PREF.CURR) AS ASSETS29, "
                StrSql = StrSql + "(EQUIP30.COST *  PREF.CURR) AS ASSETS30, "
                StrSql = StrSql + "(EQCOS.M1*  PREF.CURR) AS ASSETP1, "
                StrSql = StrSql + "(EQCOS.M2*  PREF.CURR) AS ASSETP2, "
                StrSql = StrSql + "(EQCOS.M3*  PREF.CURR) AS ASSETP3, "
                StrSql = StrSql + "(EQCOS.M4*  PREF.CURR) AS ASSETP4, "
                StrSql = StrSql + "(EQCOS.M5*  PREF.CURR) AS ASSETP5, "
                StrSql = StrSql + "(EQCOS.M6*  PREF.CURR) AS ASSETP6, "
                StrSql = StrSql + "(EQCOS.M7*  PREF.CURR) AS ASSETP7, "
                StrSql = StrSql + "(EQCOS.M8*  PREF.CURR) AS ASSETP8, "
                StrSql = StrSql + "(EQCOS.M9*  PREF.CURR) AS ASSETP9, "
                StrSql = StrSql + "(EQCOS.M10*  PREF.CURR) AS ASSETP10, "
                StrSql = StrSql + "(EQCOS.M11*  PREF.CURR) AS ASSETP11, "
                StrSql = StrSql + "(EQCOS.M12*  PREF.CURR) AS ASSETP12, "
                StrSql = StrSql + "(EQCOS.M13*  PREF.CURR) AS ASSETP13, "
                StrSql = StrSql + "(EQCOS.M14*  PREF.CURR) AS ASSETP14, "
                StrSql = StrSql + "(EQCOS.M15*  PREF.CURR) AS ASSETP15, "
                StrSql = StrSql + "(EQCOS.M16*  PREF.CURR) AS ASSETP16, "
                StrSql = StrSql + "(EQCOS.M17*  PREF.CURR) AS ASSETP17, "
                StrSql = StrSql + "(EQCOS.M18*  PREF.CURR) AS ASSETP18, "
                StrSql = StrSql + "(EQCOS.M19*  PREF.CURR) AS ASSETP19, "
                StrSql = StrSql + "(EQCOS.M20*  PREF.CURR) AS ASSETP20, "
                StrSql = StrSql + "(EQCOS.M21*  PREF.CURR) AS ASSETP21, "
                StrSql = StrSql + "(EQCOS.M22*  PREF.CURR) AS ASSETP22, "
                StrSql = StrSql + "(EQCOS.M23*  PREF.CURR) AS ASSETP23, "
                StrSql = StrSql + "(EQCOS.M24*  PREF.CURR) AS ASSETP24, "
                StrSql = StrSql + "(EQCOS.M25*  PREF.CURR) AS ASSETP25, "
                StrSql = StrSql + "(EQCOS.M26*  PREF.CURR) AS ASSETP26, "
                StrSql = StrSql + "(EQCOS.M27*  PREF.CURR) AS ASSETP27, "
                StrSql = StrSql + "(EQCOS.M28*  PREF.CURR) AS ASSETP28, "
                StrSql = StrSql + "(EQCOS.M29*  PREF.CURR) AS ASSETP29, "
                StrSql = StrSql + "(EQCOS.M30*  PREF.CURR) AS ASSETP30, "
                StrSql = StrSql + "EQUIP1.INSTKW AS ECS1, "
                StrSql = StrSql + "EQUIP2.INSTKW AS ECS2, "
                StrSql = StrSql + "EQUIP3.INSTKW AS ECS3, "
                StrSql = StrSql + "EQUIP4.INSTKW AS ECS4, "
                StrSql = StrSql + "EQUIP5.INSTKW AS ECS5, "
                StrSql = StrSql + "EQUIP6.INSTKW AS ECS6, "
                StrSql = StrSql + "EQUIP7.INSTKW AS ECS7, "
                StrSql = StrSql + "EQUIP8.INSTKW AS ECS8, "
                StrSql = StrSql + "EQUIP9.INSTKW AS ECS9, "
                StrSql = StrSql + "EQUIP10.INSTKW AS ECS10, "
                StrSql = StrSql + "EQUIP11.INSTKW AS ECS11, "
                StrSql = StrSql + "EQUIP12.INSTKW AS ECS12, "
                StrSql = StrSql + "EQUIP13.INSTKW AS ECS13, "
                StrSql = StrSql + "EQUIP14.INSTKW AS ECS14, "
                StrSql = StrSql + "EQUIP15.INSTKW AS ECS15, "
                StrSql = StrSql + "EQUIP16.INSTKW AS ECS16, "
                StrSql = StrSql + "EQUIP17.INSTKW AS ECS17, "
                StrSql = StrSql + "EQUIP18.INSTKW AS ECS18, "
                StrSql = StrSql + "EQUIP19.INSTKW AS ECS19, "
                StrSql = StrSql + "EQUIP20.INSTKW AS ECS20, "
                StrSql = StrSql + "EQUIP21.INSTKW AS ECS21, "
                StrSql = StrSql + "EQUIP22.INSTKW AS ECS22, "
                StrSql = StrSql + "EQUIP23.INSTKW AS ECS23, "
                StrSql = StrSql + "EQUIP24.INSTKW AS ECS24, "
                StrSql = StrSql + "EQUIP25.INSTKW AS ECS25, "
                StrSql = StrSql + "EQUIP26.INSTKW AS ECS26, "
                StrSql = StrSql + "EQUIP27.INSTKW AS ECS27, "
                StrSql = StrSql + "EQUIP28.INSTKW AS ECS28, "
                StrSql = StrSql + "EQUIP29.INSTKW AS ECS29, "
                StrSql = StrSql + "EQUIP30.INSTKW AS ECS30, "
                StrSql = StrSql + "EQEGPREF.M1 AS ECP1, "
                StrSql = StrSql + "EQEGPREF.M2 AS ECP2, "
                StrSql = StrSql + "EQEGPREF.M3 AS ECP3, "
                StrSql = StrSql + "EQEGPREF.M4 AS ECP4, "
                StrSql = StrSql + "EQEGPREF.M5 AS ECP5, "
                StrSql = StrSql + "EQEGPREF.M6 AS ECP6, "
                StrSql = StrSql + "EQEGPREF.M7 AS ECP7, "
                StrSql = StrSql + "EQEGPREF.M8 AS ECP8, "
                StrSql = StrSql + "EQEGPREF.M9 AS ECP9, "
                StrSql = StrSql + "EQEGPREF.M10 AS ECP10, "
                StrSql = StrSql + "EQEGPREF.M11 AS ECP11, "
                StrSql = StrSql + "EQEGPREF.M12 AS ECP12, "
                StrSql = StrSql + "EQEGPREF.M13 AS ECP13, "
                StrSql = StrSql + "EQEGPREF.M14 AS ECP14, "
                StrSql = StrSql + "EQEGPREF.M15 AS ECP15, "
                StrSql = StrSql + "EQEGPREF.M16 AS ECP16, "
                StrSql = StrSql + "EQEGPREF.M17 AS ECP17, "
                StrSql = StrSql + "EQEGPREF.M18 AS ECP18, "
                StrSql = StrSql + "EQEGPREF.M19 AS ECP19, "
                StrSql = StrSql + "EQEGPREF.M20 AS ECP20, "
                StrSql = StrSql + "EQEGPREF.M21 AS ECP21, "
                StrSql = StrSql + "EQEGPREF.M22 AS ECP22, "
                StrSql = StrSql + "EQEGPREF.M23 AS ECP23, "
                StrSql = StrSql + "EQEGPREF.M24 AS ECP24, "
                StrSql = StrSql + "EQEGPREF.M25 AS ECP25, "
                StrSql = StrSql + "EQEGPREF.M26 AS ECP26, "
                StrSql = StrSql + "EQEGPREF.M27 AS ECP27, "
                StrSql = StrSql + "EQEGPREF.M28 AS ECP28, "
                StrSql = StrSql + "EQEGPREF.M29 AS ECP29, "
                StrSql = StrSql + "EQEGPREF.M30 AS ECP30, "
                StrSql = StrSql + "EQUIP1.NTGKW AS NGCS1, "
                StrSql = StrSql + "EQUIP2.NTGKW AS NGCS2, "
                StrSql = StrSql + "EQUIP3.NTGKW AS NGCS3, "
                StrSql = StrSql + "EQUIP4.NTGKW AS NGCS4, "
                StrSql = StrSql + "EQUIP5.NTGKW AS NGCS5, "
                StrSql = StrSql + "EQUIP6.NTGKW AS NGCS6, "
                StrSql = StrSql + "EQUIP7.NTGKW AS NGCS7, "
                StrSql = StrSql + "EQUIP8.NTGKW AS NGCS8, "
                StrSql = StrSql + "EQUIP9.NTGKW AS NGCS9, "
                StrSql = StrSql + "EQUIP10.NTGKW AS NGCS10, "
                StrSql = StrSql + "EQUIP11.NTGKW AS NGCS11, "
                StrSql = StrSql + "EQUIP12.NTGKW AS NGCS12, "
                StrSql = StrSql + "EQUIP13.NTGKW AS NGCS13, "
                StrSql = StrSql + "EQUIP14.NTGKW AS NGCS14, "
                StrSql = StrSql + "EQUIP15.NTGKW AS NGCS15, "
                StrSql = StrSql + "EQUIP16.NTGKW AS NGCS16, "
                StrSql = StrSql + "EQUIP17.NTGKW AS NGCS17, "
                StrSql = StrSql + "EQUIP18.NTGKW AS NGCS18, "
                StrSql = StrSql + "EQUIP19.NTGKW AS NGCS19, "
                StrSql = StrSql + "EQUIP20.NTGKW AS NGCS20, "
                StrSql = StrSql + "EQUIP21.NTGKW AS NGCS21, "
                StrSql = StrSql + "EQUIP22.NTGKW AS NGCS22, "
                StrSql = StrSql + "EQUIP23.NTGKW AS NGCS23, "
                StrSql = StrSql + "EQUIP24.NTGKW AS NGCS24, "
                StrSql = StrSql + "EQUIP25.NTGKW AS NGCS25, "
                StrSql = StrSql + "EQUIP26.NTGKW AS NGCS26, "
                StrSql = StrSql + "EQUIP27.NTGKW AS NGCS27, "
                StrSql = StrSql + "EQUIP28.NTGKW AS NGCS28, "
                StrSql = StrSql + "EQUIP29.NTGKW AS NGCS29, "
                StrSql = StrSql + "EQUIP30.NTGKW AS NGCS30, "
                StrSql = StrSql + "EQNGPREF.M1 AS NGCP1, "
                StrSql = StrSql + "EQNGPREF.M2 AS NGCP2, "
                StrSql = StrSql + "EQNGPREF.M3 AS NGCP3, "
                StrSql = StrSql + "EQNGPREF.M4 AS NGCP4, "
                StrSql = StrSql + "EQNGPREF.M5 AS NGCP5, "
                StrSql = StrSql + "EQNGPREF.M6 AS NGCP6, "
                StrSql = StrSql + "EQNGPREF.M7 AS NGCP7, "
                StrSql = StrSql + "EQNGPREF.M8 AS NGCP8, "
                StrSql = StrSql + "EQNGPREF.M9 AS NGCP9, "
                StrSql = StrSql + "EQNGPREF.M10 AS NGCP10, "
                StrSql = StrSql + "EQNGPREF.M11 AS NGCP11, "
                StrSql = StrSql + "EQNGPREF.M12 AS NGCP12, "
                StrSql = StrSql + "EQNGPREF.M13 AS NGCP13, "
                StrSql = StrSql + "EQNGPREF.M14 AS NGCP14, "
                StrSql = StrSql + "EQNGPREF.M15 AS NGCP15, "
                StrSql = StrSql + "EQNGPREF.M16 AS NGCP16, "
                StrSql = StrSql + "EQNGPREF.M17 AS NGCP17, "
                StrSql = StrSql + "EQNGPREF.M18 AS NGCP18, "
                StrSql = StrSql + "EQNGPREF.M19 AS NGCP19, "
                StrSql = StrSql + "EQNGPREF.M20 AS NGCP20, "
                StrSql = StrSql + "EQNGPREF.M21 AS NGCP21, "
                StrSql = StrSql + "EQNGPREF.M22 AS NGCP22, "
                StrSql = StrSql + "EQNGPREF.M23 AS NGCP23, "
                StrSql = StrSql + "EQNGPREF.M24 AS NGCP24, "
                StrSql = StrSql + "EQNGPREF.M25 AS NGCP25, "
                StrSql = StrSql + "EQNGPREF.M26 AS NGCP26, "
                StrSql = StrSql + "EQNGPREF.M27 AS NGCP27, "
                StrSql = StrSql + "EQNGPREF.M28 AS NGCP28, "
                StrSql = StrSql + "EQNGPREF.M29 AS NGCP29, "
                StrSql = StrSql + "EQNGPREF.M30 AS NGCP30, "
                StrSql = StrSql + "EQHRS.M1 AS HRS1, "
                StrSql = StrSql + "EQHRS.M2 AS HRS2, "
                StrSql = StrSql + "EQHRS.M3 AS HRS3, "
                StrSql = StrSql + "EQHRS.M4 AS HRS4, "
                StrSql = StrSql + "EQHRS.M5 AS HRS5, "
                StrSql = StrSql + "EQHRS.M6 AS HRS6, "
                StrSql = StrSql + "EQHRS.M7 AS HRS7, "
                StrSql = StrSql + "EQHRS.M8 AS HRS8, "
                StrSql = StrSql + "EQHRS.M9 AS HRS9, "
                StrSql = StrSql + "EQHRS.M10 AS HRS10, "
                StrSql = StrSql + "EQHRS.M11 AS HRS11, "
                StrSql = StrSql + "EQHRS.M12 AS HRS12, "
                StrSql = StrSql + "EQHRS.M13 AS HRS13, "
                StrSql = StrSql + "EQHRS.M14 AS HRS14, "
                StrSql = StrSql + "EQHRS.M15 AS HRS15, "
                StrSql = StrSql + "EQHRS.M16 AS HRS16, "
                StrSql = StrSql + "EQHRS.M17 AS HRS17, "
                StrSql = StrSql + "EQHRS.M18 AS HRS18, "
                StrSql = StrSql + "EQHRS.M19 AS HRS19, "
                StrSql = StrSql + "EQHRS.M20 AS HRS20, "
                StrSql = StrSql + "EQHRS.M21 AS HRS21, "
                StrSql = StrSql + "EQHRS.M22 AS HRS22, "
                StrSql = StrSql + "EQHRS.M23 AS HRS23, "
                StrSql = StrSql + "EQHRS.M24 AS HRS24, "
                StrSql = StrSql + "EQHRS.M25 AS HRS25, "
                StrSql = StrSql + "EQHRS.M26 AS HRS26, "
                StrSql = StrSql + "EQHRS.M27 AS HRS27, "
                StrSql = StrSql + "EQHRS.M28 AS HRS28, "
                StrSql = StrSql + "EQHRS.M29 AS HRS29, "
                StrSql = StrSql + "EQHRS.M30 AS HRS30, "
                StrSql = StrSql + "EQCOSTTYPE.M1 AS COSTTYPE1, "
                StrSql = StrSql + "EQCOSTTYPE.M2 AS COSTTYPE2, "
                StrSql = StrSql + "EQCOSTTYPE.M3 AS COSTTYPE3, "
                StrSql = StrSql + "EQCOSTTYPE.M4 AS COSTTYPE4, "
                StrSql = StrSql + "EQCOSTTYPE.M5 AS COSTTYPE5, "
                StrSql = StrSql + "EQCOSTTYPE.M6 AS COSTTYPE6, "
                StrSql = StrSql + "EQCOSTTYPE.M7 AS COSTTYPE7, "
                StrSql = StrSql + "EQCOSTTYPE.M8 AS COSTTYPE8, "
                StrSql = StrSql + "EQCOSTTYPE.M9 AS COSTTYPE9, "
                StrSql = StrSql + "EQCOSTTYPE.M10 AS COSTTYPE10, "
                StrSql = StrSql + "EQCOSTTYPE.M11 AS COSTTYPE11, "
                StrSql = StrSql + "EQCOSTTYPE.M12 AS COSTTYPE12, "
                StrSql = StrSql + "EQCOSTTYPE.M13 AS COSTTYPE13, "
                StrSql = StrSql + "EQCOSTTYPE.M14 AS COSTTYPE14, "
                StrSql = StrSql + "EQCOSTTYPE.M15 AS COSTTYPE15, "
                StrSql = StrSql + "EQCOSTTYPE.M16 AS COSTTYPE16, "
                StrSql = StrSql + "EQCOSTTYPE.M17 AS COSTTYPE17, "
                StrSql = StrSql + "EQCOSTTYPE.M18 AS COSTTYPE18, "
                StrSql = StrSql + "EQCOSTTYPE.M19 AS COSTTYPE19, "
                StrSql = StrSql + "EQCOSTTYPE.M20 AS COSTTYPE20, "
                StrSql = StrSql + "EQCOSTTYPE.M21 AS COSTTYPE21, "
                StrSql = StrSql + "EQCOSTTYPE.M22 AS COSTTYPE22, "
                StrSql = StrSql + "EQCOSTTYPE.M23 AS COSTTYPE23, "
                StrSql = StrSql + "EQCOSTTYPE.M24 AS COSTTYPE24, "
                StrSql = StrSql + "EQCOSTTYPE.M25 AS COSTTYPE25, "
                StrSql = StrSql + "EQCOSTTYPE.M26 AS COSTTYPE26, "
                StrSql = StrSql + "EQCOSTTYPE.M27 AS COSTTYPE27, "
                StrSql = StrSql + "EQCOSTTYPE.M28 AS COSTTYPE28, "
                StrSql = StrSql + "EQCOSTTYPE.M29 AS COSTTYPE29, "
                StrSql = StrSql + "EQCOSTTYPE.M30 AS COSTTYPE30, "
                StrSql = StrSql + "EQDEP.M1 AS DEP1, "
                StrSql = StrSql + "EQDEP.M2 AS DEP2, "
                StrSql = StrSql + "EQDEP.M3 AS DEP3, "
                StrSql = StrSql + "EQDEP.M4 AS DEP4, "
                StrSql = StrSql + "EQDEP.M5 AS DEP5, "
                StrSql = StrSql + "EQDEP.M6 AS DEP6, "
                StrSql = StrSql + "EQDEP.M7 AS DEP7, "
                StrSql = StrSql + "EQDEP.M8 AS DEP8, "
                StrSql = StrSql + "EQDEP.M9 AS DEP9, "
                StrSql = StrSql + "EQDEP.M10 AS DEP10, "
                StrSql = StrSql + "EQDEP.M11 AS DEP11, "
                StrSql = StrSql + "EQDEP.M12 AS DEP12, "
                StrSql = StrSql + "EQDEP.M13 AS DEP13, "
                StrSql = StrSql + "EQDEP.M14 AS DEP14, "
                StrSql = StrSql + "EQDEP.M15 AS DEP15, "
                StrSql = StrSql + "EQDEP.M16 AS DEP16, "
                StrSql = StrSql + "EQDEP.M17 AS DEP17, "
                StrSql = StrSql + "EQDEP.M18 AS DEP18, "
                StrSql = StrSql + "EQDEP.M19 AS DEP19, "
                StrSql = StrSql + "EQDEP.M20 AS DEP20, "
                StrSql = StrSql + "EQDEP.M21 AS DEP21, "
                StrSql = StrSql + "EQDEP.M22 AS DEP22, "
                StrSql = StrSql + "EQDEP.M23 AS DEP23, "
                StrSql = StrSql + "EQDEP.M24 AS DEP24, "
                StrSql = StrSql + "EQDEP.M25 AS DEP25, "
                StrSql = StrSql + "EQDEP.M26 AS DEP26, "
                StrSql = StrSql + "EQDEP.M27 AS DEP27, "
                StrSql = StrSql + "EQDEP.M28 AS DEP28, "
                StrSql = StrSql + "EQDEP.M29 AS DEP29, "
                StrSql = StrSql + "EQDEP.M30 AS DEP30, "

                StrSql = StrSql + "EQNUM.M1 AS NUM1, "
                StrSql = StrSql + "EQNUM.M2 AS NUM2, "
                StrSql = StrSql + "EQNUM.M3 AS NUM3, "
                StrSql = StrSql + "EQNUM.M4 AS NUM4, "
                StrSql = StrSql + "EQNUM.M5 AS NUM5, "
                StrSql = StrSql + "EQNUM.M6 AS NUM6, "
                StrSql = StrSql + "EQNUM.M7 AS NUM7, "
                StrSql = StrSql + "EQNUM.M8 AS NUM8, "
                StrSql = StrSql + "EQNUM.M9 AS NUM9, "
                StrSql = StrSql + "EQNUM.M10 AS NUM10, "
                StrSql = StrSql + "EQNUM.M11 AS NUM11, "
                StrSql = StrSql + "EQNUM.M12 AS NUM12, "
                StrSql = StrSql + "EQNUM.M13 AS NUM13, "
                StrSql = StrSql + "EQNUM.M14 AS NUM14, "
                StrSql = StrSql + "EQNUM.M15 AS NUM15, "
                StrSql = StrSql + "EQNUM.M16 AS NUM16, "
                StrSql = StrSql + "EQNUM.M17 AS NUM17, "
                StrSql = StrSql + "EQNUM.M18 AS NUM18, "
                StrSql = StrSql + "EQNUM.M19 AS NUM19, "
                StrSql = StrSql + "EQNUM.M20 AS NUM20, "
                StrSql = StrSql + "EQNUM.M21 AS NUM21, "
                StrSql = StrSql + "EQNUM.M22 AS NUM22, "
                StrSql = StrSql + "EQNUM.M23 AS NUM23, "
                StrSql = StrSql + "EQNUM.M24 AS NUM24, "
                StrSql = StrSql + "EQNUM.M25 AS NUM25, "
                StrSql = StrSql + "EQNUM.M26 AS NUM26, "
                StrSql = StrSql + "EQNUM.M27 AS NUM27, "
                StrSql = StrSql + "EQNUM.M28 AS NUM28, "
                StrSql = StrSql + "EQNUM.M29 AS NUM29, "
                StrSql = StrSql + "EQNUM.M30 AS NUM30 "

                StrSql = StrSql + "FROM EQUIPMENT2TYPE EQUIP "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP1 "
                StrSql = StrSql + "ON EQUIP1.EQUIPID=EQUIP.M1 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP2 "
                StrSql = StrSql + "ON EQUIP2.EQUIPID=EQUIP.M2 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP3 "
                StrSql = StrSql + "ON EQUIP3.EQUIPID=EQUIP.M3 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP4 "
                StrSql = StrSql + "ON EQUIP4.EQUIPID=EQUIP.M4 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP5 "
                StrSql = StrSql + "ON EQUIP5.EQUIPID=EQUIP.M5 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP6 "
                StrSql = StrSql + "ON EQUIP6.EQUIPID=EQUIP.M6 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP7 "
                StrSql = StrSql + "ON EQUIP7.EQUIPID=EQUIP.M7 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP8 "
                StrSql = StrSql + "ON EQUIP8.EQUIPID=EQUIP.M8 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP9 "
                StrSql = StrSql + "ON EQUIP9.EQUIPID=EQUIP.M9 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP10 "
                StrSql = StrSql + "ON EQUIP10.EQUIPID=EQUIP.M10 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP11 "
                StrSql = StrSql + "ON EQUIP11.EQUIPID=EQUIP.M11 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP12 "
                StrSql = StrSql + "ON EQUIP12.EQUIPID=EQUIP.M12 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP13 "
                StrSql = StrSql + "ON EQUIP13.EQUIPID=EQUIP.M13 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP14 "
                StrSql = StrSql + "ON EQUIP14.EQUIPID=EQUIP.M14 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP15 "
                StrSql = StrSql + "ON EQUIP15.EQUIPID=EQUIP.M15 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP16 "
                StrSql = StrSql + "ON EQUIP16.EQUIPID=EQUIP.M16 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP17 "
                StrSql = StrSql + "ON EQUIP17.EQUIPID=EQUIP.M17 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP18 "
                StrSql = StrSql + "ON EQUIP18.EQUIPID=EQUIP.M18 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP19 "
                StrSql = StrSql + "ON EQUIP19.EQUIPID=EQUIP.M19 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP20 "
                StrSql = StrSql + "ON EQUIP20.EQUIPID=EQUIP.M20 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP21 "
                StrSql = StrSql + "ON EQUIP21.EQUIPID=EQUIP.M21 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP22 "
                StrSql = StrSql + "ON EQUIP22.EQUIPID=EQUIP.M22 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP23 "
                StrSql = StrSql + "ON EQUIP23.EQUIPID=EQUIP.M23 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP24 "
                StrSql = StrSql + "ON EQUIP24.EQUIPID=EQUIP.M24 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP25 "
                StrSql = StrSql + "ON EQUIP25.EQUIPID=EQUIP.M25 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP26 "
                StrSql = StrSql + "ON EQUIP26.EQUIPID=EQUIP.M26 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP27 "
                StrSql = StrSql + "ON EQUIP27.EQUIPID=EQUIP.M27 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP28 "
                StrSql = StrSql + "ON EQUIP28.EQUIPID=EQUIP.M28 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP29 "
                StrSql = StrSql + "ON EQUIP29.EQUIPID=EQUIP.M29 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2  EQUIP30 "
                StrSql = StrSql + "ON EQUIP30.EQUIPID=EQUIP.M30 "
                StrSql = StrSql + "INNER JOIN PREFERENCES  PREF ON "
                StrSql = StrSql + "PREF.CASEID=EQUIP.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2COST EQCOS "
                StrSql = StrSql + "ON EQCOS.CASEID=EQUIP.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIP2ENERGYPREF EQEGPREF "
                StrSql = StrSql + "ON EQEGPREF.CASEID=EQUIP.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIP2NATURALGASPREF EQNGPREF "
                StrSql = StrSql + "ON EQNGPREF.CASEID=EQUIP.CASEID "
                StrSql = StrSql + "INNER JOIN Equipment2DEP EQDEP "
                StrSql = StrSql + "ON EQDEP.CASEID=EQUIP.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2MAHRS EQHRS "
                StrSql = StrSql + "ON EQHRS.CASEID=EQUIP.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIPMENT2COSTTYPE EQCOSTTYPE "
                StrSql = StrSql + "ON EQCOSTTYPE.CASEID=EQUIP.CASEID "

                StrSql = StrSql + "INNER JOIN EQUIPMENT2NUMBER EQNUM "
                StrSql = StrSql + "ON EQNUM.CASEID=EQUIP.CASEID "

                StrSql = StrSql + "WHERE EQUIP.CASEID=" + CaseId.ToString() + ""

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetSupportDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetOperationInDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT  EQUIPT.CASEID,  "
                StrSql = StrSql + "PREF.TITLE9 AS WEBWIDTHUNIT, "
                StrSql = StrSql + "PREF.TITLE8 AS INSTRUNIT, "
                StrSql = StrSql + "PREF.UNITS, "
                StrSql = StrSql + "PREF.TITLE2, "
                StrSql = StrSql + "(EQUIP1.EQUIPDE1 ||' ' || EQUIP1.EQUIPDE2) AS 	EQUIPDES1, "
                StrSql = StrSql + "(EQUIP2.EQUIPDE1 ||' ' || EQUIP2.EQUIPDE2) AS 	EQUIPDES2, "
                StrSql = StrSql + "(EQUIP3.EQUIPDE1 ||' ' || EQUIP3.EQUIPDE2) AS 	EQUIPDES3, "
                StrSql = StrSql + "(EQUIP4.EQUIPDE1 ||' ' || EQUIP4.EQUIPDE2) AS 	EQUIPDES4, "
                StrSql = StrSql + "(EQUIP5.EQUIPDE1 ||' ' || EQUIP5.EQUIPDE2) AS 	EQUIPDES5, "
                StrSql = StrSql + "(EQUIP6.EQUIPDE1 ||' ' || EQUIP6.EQUIPDE2) AS 	EQUIPDES6, "
                StrSql = StrSql + "(EQUIP7.EQUIPDE1 ||' ' || EQUIP7.EQUIPDE2) AS 	EQUIPDES7, "
                StrSql = StrSql + "(EQUIP8.EQUIPDE1 ||' ' || EQUIP8.EQUIPDE2) AS 	EQUIPDES8, "
                StrSql = StrSql + "(EQUIP9.EQUIPDE1 ||' ' || EQUIP9.EQUIPDE2) AS 	EQUIPDES9, "
                StrSql = StrSql + "(EQUIP10.EQUIPDE1 ||' ' || EQUIP10.EQUIPDE2) AS EQUIPDES10, "
                StrSql = StrSql + "(EQUIP11.EQUIPDE1 ||' ' || EQUIP11.EQUIPDE2) AS 	EQUIPDES11, "
                StrSql = StrSql + "(EQUIP12.EQUIPDE1 ||' ' || EQUIP12.EQUIPDE2) AS 	EQUIPDES12, "
                StrSql = StrSql + "(EQUIP13.EQUIPDE1 ||' ' || EQUIP13.EQUIPDE2) AS 	EQUIPDES13, "
                StrSql = StrSql + "(EQUIP14.EQUIPDE1 ||' ' || EQUIP14.EQUIPDE2) AS 	EQUIPDES14, "
                StrSql = StrSql + "(EQUIP15.EQUIPDE1 ||' ' || EQUIP15.EQUIPDE2) AS 	EQUIPDES15, "
                StrSql = StrSql + "(EQUIP16.EQUIPDE1 ||' ' || EQUIP16.EQUIPDE2) AS 	EQUIPDES16, "
                StrSql = StrSql + "(EQUIP17.EQUIPDE1 ||' ' || EQUIP17.EQUIPDE2) AS 	EQUIPDES17, "
                StrSql = StrSql + "(EQUIP18.EQUIPDE1 ||' ' || EQUIP18.EQUIPDE2) AS 	EQUIPDES18, "
                StrSql = StrSql + "(EQUIP19.EQUIPDE1 ||' ' || EQUIP19.EQUIPDE2) AS 	EQUIPDES19, "
                StrSql = StrSql + "(EQUIP20.EQUIPDE1 ||' ' || EQUIP20.EQUIPDE2) AS  EQUIPDES20, "
                StrSql = StrSql + "(EQUIP21.EQUIPDE1 ||' ' || EQUIP21.EQUIPDE2) AS 	EQUIPDES21, "
                StrSql = StrSql + "(EQUIP22.EQUIPDE1 ||' ' || EQUIP22.EQUIPDE2) AS 	EQUIPDES22, "
                StrSql = StrSql + "(EQUIP23.EQUIPDE1 ||' ' || EQUIP23.EQUIPDE2) AS 	EQUIPDES23, "
                StrSql = StrSql + "(EQUIP24.EQUIPDE1 ||' ' || EQUIP24.EQUIPDE2) AS 	EQUIPDES24, "
                StrSql = StrSql + "(EQUIP25.EQUIPDE1 ||' ' || EQUIP25.EQUIPDE2) AS 	EQUIPDES25, "
                StrSql = StrSql + "(EQUIP26.EQUIPDE1 ||' ' || EQUIP26.EQUIPDE2) AS 	EQUIPDES26, "
                StrSql = StrSql + "(EQUIP27.EQUIPDE1 ||' ' || EQUIP27.EQUIPDE2) AS 	EQUIPDES27, "
                StrSql = StrSql + "(EQUIP28.EQUIPDE1 ||' ' || EQUIP28.EQUIPDE2) AS 	EQUIPDES28, "
                StrSql = StrSql + "(EQUIP29.EQUIPDE1 ||' ' || EQUIP29.EQUIPDE2) AS 	EQUIPDES29, "
                StrSql = StrSql + "(EQUIP30.EQUIPDE1 ||' ' || EQUIP30.EQUIPDE2) AS EQUIPDES30, "
                StrSql = StrSql + "EQUIPT.M1 AS ASSETID1, "
                StrSql = StrSql + "EQUIPT.M2  AS ASSETID2, "
                StrSql = StrSql + "EQUIPT.M3  AS ASSETID3, "
                StrSql = StrSql + "EQUIPT.M4  AS ASSETID4, "
                StrSql = StrSql + "EQUIPT.M5  AS ASSETID5, "
                StrSql = StrSql + "EQUIPT.M6  AS ASSETID6, "
                StrSql = StrSql + "EQUIPT.M7  AS ASSETID7, "
                StrSql = StrSql + "EQUIPT.M8  AS ASSETID8, "
                StrSql = StrSql + "EQUIPT.M9 AS ASSETID9, "
                StrSql = StrSql + "EQUIPT.M10 AS ASSETID10, "
                StrSql = StrSql + "EQUIPT.M11 AS ASSETID11, "
                StrSql = StrSql + "EQUIPT.M12 AS ASSETID12, "
                StrSql = StrSql + "EQUIPT.M13 AS ASSETID13, "
                StrSql = StrSql + "EQUIPT.M14 AS ASSETID14, "
                StrSql = StrSql + "EQUIPT.M15 AS ASSETID15, "
                StrSql = StrSql + "EQUIPT.M16 AS ASSETID16, "
                StrSql = StrSql + "EQUIPT.M17 AS ASSETID17, "
                StrSql = StrSql + "EQUIPT.M18 AS ASSETID18, "
                StrSql = StrSql + "EQUIPT.M19 AS ASSETID19, "
                StrSql = StrSql + "EQUIPT.M20 AS ASSETID20, "
                StrSql = StrSql + "EQUIPT.M21 AS ASSETID21, "
                StrSql = StrSql + "EQUIPT.M22 AS ASSETID22, "
                StrSql = StrSql + "EQUIPT.M23 AS ASSETID23, "
                StrSql = StrSql + "EQUIPT.M24 AS ASSETID24, "
                StrSql = StrSql + "EQUIPT.M25 AS ASSETID25, "
                StrSql = StrSql + "EQUIPT.M26 AS ASSETID26, "
                StrSql = StrSql + "EQUIPT.M27 AS ASSETID27, "
                StrSql = StrSql + "EQUIPT.M28 AS ASSETID28, "
                StrSql = StrSql + "EQUIPT.M29 AS ASSETID29, "
                StrSql = StrSql + "EQUIPT.M30 AS ASSETID30, "
                StrSql = StrSql + "CASE EQUIP1.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M1*PREF.CONVTHICK) END AS OPWebWidth1, "
                StrSql = StrSql + "CASE EQUIP2.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M2*PREF.CONVTHICK) END AS OPWebWidth2, "
                StrSql = StrSql + "CASE EQUIP3.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M3*PREF.CONVTHICK) END AS OPWebWidth3, "
                StrSql = StrSql + "CASE EQUIP4.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M4*PREF.CONVTHICK) END AS OPWebWidth4, "
                StrSql = StrSql + "CASE EQUIP5.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M5*PREF.CONVTHICK) END AS OPWebWidth5, "
                StrSql = StrSql + "CASE EQUIP6.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M6*PREF.CONVTHICK) END AS OPWebWidth6, "
                StrSql = StrSql + "CASE EQUIP7.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M7*PREF.CONVTHICK) END AS OPWebWidth7, "
                StrSql = StrSql + "CASE EQUIP8.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M8*PREF.CONVTHICK) END AS OPWebWidth8, "
                StrSql = StrSql + "CASE EQUIP9.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M9*PREF.CONVTHICK) END AS OPWebWidth9, "
                StrSql = StrSql + "CASE EQUIP10.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M10*PREF.CONVTHICK) END AS OPWebWidth10, "
                StrSql = StrSql + "CASE EQUIP11.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M11*PREF.CONVTHICK) END AS OPWebWidth11, "
                StrSql = StrSql + "CASE EQUIP12.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M12*PREF.CONVTHICK) END AS OPWebWidth12, "
                StrSql = StrSql + "CASE EQUIP13.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M13*PREF.CONVTHICK) END AS OPWebWidth13, "
                StrSql = StrSql + "CASE EQUIP14.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M14*PREF.CONVTHICK) END AS OPWebWidth14, "
                StrSql = StrSql + "CASE EQUIP15.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M15*PREF.CONVTHICK) END AS OPWebWidth15, "
                StrSql = StrSql + "CASE EQUIP16.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M16*PREF.CONVTHICK) END AS OPWebWidth16, "
                StrSql = StrSql + "CASE EQUIP17.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M17*PREF.CONVTHICK) END AS OPWebWidth17, "
                StrSql = StrSql + "CASE EQUIP18.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M18*PREF.CONVTHICK) END AS OPWebWidth18, "
                StrSql = StrSql + "CASE EQUIP19.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M19*PREF.CONVTHICK) END AS OPWebWidth19, "
                StrSql = StrSql + "CASE EQUIP20.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M20*PREF.CONVTHICK) END AS OPWebWidth20, "
                StrSql = StrSql + "CASE EQUIP21.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M21*PREF.CONVTHICK) END AS OPWebWidth21, "
                StrSql = StrSql + "CASE EQUIP22.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M22*PREF.CONVTHICK) END AS OPWebWidth22, "
                StrSql = StrSql + "CASE EQUIP23.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M23*PREF.CONVTHICK) END AS OPWebWidth23, "
                StrSql = StrSql + "CASE EQUIP24.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M24*PREF.CONVTHICK) END AS OPWebWidth24, "
                StrSql = StrSql + "CASE EQUIP25.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M25*PREF.CONVTHICK) END AS OPWebWidth25, "
                StrSql = StrSql + "CASE EQUIP26.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M26*PREF.CONVTHICK) END AS OPWebWidth26, "
                StrSql = StrSql + "CASE EQUIP27.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M27*PREF.CONVTHICK) END AS OPWebWidth27, "
                StrSql = StrSql + "CASE EQUIP28.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M28*PREF.CONVTHICK) END AS OPWebWidth28, "
                StrSql = StrSql + "CASE EQUIP29.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M29*PREF.CONVTHICK) END AS OPWebWidth29, "
                StrSql = StrSql + "CASE EQUIP30.WIDTH  WHEN 0 THEN -1  ELSE (OPW.M30*PREF.CONVTHICK) END AS OPWebWidth30, "

                StrSql = StrSql + "(OPW.M1*PREF.CONVTHICK) AS OPWEBWIDTHPREF1, "
                StrSql = StrSql + "(OPW.M2*PREF.CONVTHICK) AS OPWEBWIDTHPREF2, "
                StrSql = StrSql + "(OPW.M3*PREF.CONVTHICK) AS OPWEBWIDTHPREF3, "
                StrSql = StrSql + "(OPW.M4*PREF.CONVTHICK) AS OPWEBWIDTHPREF4, "
                StrSql = StrSql + "(OPW.M5*PREF.CONVTHICK) AS OPWEBWIDTHPREF5, "
                StrSql = StrSql + "(OPW.M6*PREF.CONVTHICK) AS OPWEBWIDTHPREF6, "
                StrSql = StrSql + "(OPW.M7*PREF.CONVTHICK) AS OPWEBWIDTHPREF7, "
                StrSql = StrSql + "(OPW.M8*PREF.CONVTHICK) AS OPWEBWIDTHPREF8, "
                StrSql = StrSql + "(OPW.M9*PREF.CONVTHICK) AS OPWEBWIDTHPREF9, "
                StrSql = StrSql + "(OPW.M10*PREF.CONVTHICK) AS OPWEBWIDTHPREF10, "
                StrSql = StrSql + "(OPW.M11*PREF.CONVTHICK) AS OPWEBWIDTHPREF11, "
                StrSql = StrSql + "(OPW.M12*PREF.CONVTHICK) AS OPWEBWIDTHPREF12, "
                StrSql = StrSql + "(OPW.M13*PREF.CONVTHICK) AS OPWEBWIDTHPREF13, "
                StrSql = StrSql + "(OPW.M14*PREF.CONVTHICK) AS OPWEBWIDTHPREF14, "
                StrSql = StrSql + "(OPW.M15*PREF.CONVTHICK) AS OPWEBWIDTHPREF15, "
                StrSql = StrSql + "(OPW.M16*PREF.CONVTHICK) AS OPWEBWIDTHPREF16, "
                StrSql = StrSql + "(OPW.M17*PREF.CONVTHICK) AS OPWEBWIDTHPREF17, "
                StrSql = StrSql + "(OPW.M18*PREF.CONVTHICK) AS OPWEBWIDTHPREF18, "
                StrSql = StrSql + "(OPW.M19*PREF.CONVTHICK) AS OPWEBWIDTHPREF19, "
                StrSql = StrSql + "(OPW.M20*PREF.CONVTHICK) AS OPWEBWIDTHPREF20, "
                StrSql = StrSql + "(OPW.M21*PREF.CONVTHICK) AS OPWEBWIDTHPREF21, "
                StrSql = StrSql + "(OPW.M22*PREF.CONVTHICK) AS OPWEBWIDTHPREF22, "
                StrSql = StrSql + "(OPW.M23*PREF.CONVTHICK) AS OPWEBWIDTHPREF23, "
                StrSql = StrSql + "(OPW.M24*PREF.CONVTHICK) AS OPWEBWIDTHPREF24, "
                StrSql = StrSql + "(OPW.M25*PREF.CONVTHICK) AS OPWEBWIDTHPREF25, "
                StrSql = StrSql + "(OPW.M26*PREF.CONVTHICK) AS OPWEBWIDTHPREF26, "
                StrSql = StrSql + "(OPW.M27*PREF.CONVTHICK) AS OPWEBWIDTHPREF27, "
                StrSql = StrSql + "(OPW.M28*PREF.CONVTHICK) AS OPWEBWIDTHPREF28, "
                StrSql = StrSql + "(OPW.M29*PREF.CONVTHICK) AS OPWEBWIDTHPREF29, "
                StrSql = StrSql + "(OPW.M30*PREF.CONVTHICK) AS OPWEBWIDTHPREF30, "
                StrSql = StrSql + "(EQUIP1.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG1, "
                StrSql = StrSql + "(EQUIP2.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG2, "
                StrSql = StrSql + "(EQUIP3.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG3, "
                StrSql = StrSql + "(EQUIP4.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG4, "
                StrSql = StrSql + "(EQUIP5.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG5, "
                StrSql = StrSql + "(EQUIP6.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG6, "
                StrSql = StrSql + "(EQUIP7.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG7, "
                StrSql = StrSql + "(EQUIP8.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG8, "
                StrSql = StrSql + "(EQUIP9.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG9, "
                StrSql = StrSql + "(EQUIP10.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG10, "
                StrSql = StrSql + "(EQUIP11.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG11, "
                StrSql = StrSql + "(EQUIP12.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG12, "
                StrSql = StrSql + "(EQUIP13.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG13, "
                StrSql = StrSql + "(EQUIP14.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG14, "
                StrSql = StrSql + "(EQUIP15.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG15, "
                StrSql = StrSql + "(EQUIP16.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG16, "
                StrSql = StrSql + "(EQUIP17.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG17, "
                StrSql = StrSql + "(EQUIP18.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG18, "
                StrSql = StrSql + "(EQUIP19.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG19, "
                StrSql = StrSql + "(EQUIP20.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG20, "
                StrSql = StrSql + "(EQUIP21.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG21, "
                StrSql = StrSql + "(EQUIP22.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG22, "
                StrSql = StrSql + "(EQUIP23.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG23, "
                StrSql = StrSql + "(EQUIP24.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG24, "
                StrSql = StrSql + "(EQUIP25.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG25, "
                StrSql = StrSql + "(EQUIP26.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG26, "
                StrSql = StrSql + "(EQUIP27.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG27, "
                StrSql = StrSql + "(EQUIP28.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG28, "
                StrSql = StrSql + "(EQUIP29.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG29, "
                StrSql = StrSql + "(EQUIP30.WIDTH *  PREF.CONVTHICK) AS OPWEBWIDTHSUGG30, "
                StrSql = StrSql + "(OPE.M1) AS OPEXITSPREF1, "
                StrSql = StrSql + "(OPE.M2) AS OPEXITSPREF2, "
                StrSql = StrSql + "(OPE.M3) AS OPEXITSPREF3, "
                StrSql = StrSql + "(OPE.M4) AS OPEXITSPREF4, "
                StrSql = StrSql + "(OPE.M5) AS OPEXITSPREF5, "
                StrSql = StrSql + "(OPE.M6) AS OPEXITSPREF6, "
                StrSql = StrSql + "(OPE.M7) AS OPEXITSPREF7, "
                StrSql = StrSql + "(OPE.M8) AS OPEXITSPREF8, "
                StrSql = StrSql + "(OPE.M9) AS OPEXITSPREF9, "
                StrSql = StrSql + "(OPE.M10) AS OPEXITSPREF10, "
                StrSql = StrSql + "(OPE.M11) AS OPEXITSPREF11, "
                StrSql = StrSql + "(OPE.M12) AS OPEXITSPREF12, "
                StrSql = StrSql + "(OPE.M13) AS OPEXITSPREF13, "
                StrSql = StrSql + "(OPE.M14) AS OPEXITSPREF14, "
                StrSql = StrSql + "(OPE.M15) AS OPEXITSPREF15, "
                StrSql = StrSql + "(OPE.M16) AS OPEXITSPREF16, "
                StrSql = StrSql + "(OPE.M17) AS OPEXITSPREF17, "
                StrSql = StrSql + "(OPE.M18) AS OPEXITSPREF18, "
                StrSql = StrSql + "(OPE.M19) AS OPEXITSPREF19, "
                StrSql = StrSql + "(OPE.M20) AS OPEXITSPREF20, "
                StrSql = StrSql + "(OPE.M21) AS OPEXITSPREF21, "
                StrSql = StrSql + "(OPE.M22) AS OPEXITSPREF22, "
                StrSql = StrSql + "(OPE.M23) AS OPEXITSPREF23, "
                StrSql = StrSql + "(OPE.M24) AS OPEXITSPREF24, "
                StrSql = StrSql + "(OPE.M25) AS OPEXITSPREF25, "
                StrSql = StrSql + "(OPE.M26) AS OPEXITSPREF26, "
                StrSql = StrSql + "(OPE.M27) AS OPEXITSPREF27, "
                StrSql = StrSql + "(OPE.M28) AS OPEXITSPREF28, "
                StrSql = StrSql + "(OPE.M29) AS OPEXITSPREF29, "
                StrSql = StrSql + "(OPE.M30) AS OPEXITSPREF30, "
                StrSql = StrSql + "(EQUIP1.EXITS ) as OPEXITSSUGG1, "
                StrSql = StrSql + "(EQUIP2.EXITS ) as OPEXITSSUGG2, "
                StrSql = StrSql + "(EQUIP3.EXITS ) as OPEXITSSUGG3, "
                StrSql = StrSql + "(EQUIP4.EXITS ) as OPEXITSSUGG4, "
                StrSql = StrSql + "(EQUIP5.EXITS ) as OPEXITSSUGG5, "
                StrSql = StrSql + "(EQUIP6.EXITS ) as OPEXITSSUGG6, "
                StrSql = StrSql + "(EQUIP7.EXITS ) as OPEXITSSUGG7, "
                StrSql = StrSql + "(EQUIP8.EXITS ) as OPEXITSSUGG8, "
                StrSql = StrSql + "(EQUIP9.EXITS ) as OPEXITSSUGG9, "
                StrSql = StrSql + "(EQUIP10.EXITS ) as OPEXITSSUGG10, "
                StrSql = StrSql + "(EQUIP11.EXITS ) as OPEXITSSUGG11, "
                StrSql = StrSql + "(EQUIP12.EXITS ) as OPEXITSSUGG12, "
                StrSql = StrSql + "(EQUIP13.EXITS ) as OPEXITSSUGG13, "
                StrSql = StrSql + "(EQUIP14.EXITS ) as OPEXITSSUGG14, "
                StrSql = StrSql + "(EQUIP15.EXITS ) as OPEXITSSUGG15, "
                StrSql = StrSql + "(EQUIP16.EXITS ) as OPEXITSSUGG16, "
                StrSql = StrSql + "(EQUIP17.EXITS ) as OPEXITSSUGG17, "
                StrSql = StrSql + "(EQUIP18.EXITS ) as OPEXITSSUGG18, "
                StrSql = StrSql + "(EQUIP19.EXITS ) as OPEXITSSUGG19, "
                StrSql = StrSql + "(EQUIP20.EXITS ) as OPEXITSSUGG20, "
                StrSql = StrSql + "(EQUIP21.EXITS ) as OPEXITSSUGG21, "
                StrSql = StrSql + "(EQUIP22.EXITS ) as OPEXITSSUGG22, "
                StrSql = StrSql + "(EQUIP23.EXITS ) as OPEXITSSUGG23, "
                StrSql = StrSql + "(EQUIP24.EXITS ) as OPEXITSSUGG24, "
                StrSql = StrSql + "(EQUIP25.EXITS ) as OPEXITSSUGG25, "
                StrSql = StrSql + "(EQUIP26.EXITS ) as OPEXITSSUGG26, "
                StrSql = StrSql + "(EQUIP27.EXITS ) as OPEXITSSUGG27, "
                StrSql = StrSql + "(EQUIP28.EXITS ) as OPEXITSSUGG28, "
                StrSql = StrSql + "(EQUIP29.EXITS ) as OPEXITSSUGG29, "
                StrSql = StrSql + "(EQUIP30.EXITS ) as OPEXITSSUGG30, "

                StrSql = StrSql + "OMAXR.M1 AS OMAXRH1, "
                StrSql = StrSql + "OMAXR.M2  AS OMAXRH2, "
                StrSql = StrSql + "OMAXR.M3  AS OMAXRH3, "
                StrSql = StrSql + "OMAXR.M4  AS OMAXRH4, "
                StrSql = StrSql + "OMAXR.M5  AS OMAXRH5, "
                StrSql = StrSql + "OMAXR.M6  AS OMAXRH6, "
                StrSql = StrSql + "OMAXR.M7  AS OMAXRH7, "
                StrSql = StrSql + "OMAXR.M8  AS OMAXRH8, "
                StrSql = StrSql + "OMAXR.M9 AS OMAXRH9, "
                StrSql = StrSql + "OMAXR.M10 AS OMAXRH10, "
                StrSql = StrSql + "OMAXR.M11 AS OMAXRH11, "
                StrSql = StrSql + "OMAXR.M12 AS OMAXRH12, "
                StrSql = StrSql + "OMAXR.M13 AS OMAXRH13, "
                StrSql = StrSql + "OMAXR.M14 AS OMAXRH14, "
                StrSql = StrSql + "OMAXR.M15 AS OMAXRH15, "
                StrSql = StrSql + "OMAXR.M16 AS OMAXRH16, "
                StrSql = StrSql + "OMAXR.M17 AS OMAXRH17, "
                StrSql = StrSql + "OMAXR.M18 AS OMAXRH18, "
                StrSql = StrSql + "OMAXR.M19 AS OMAXRH19, "
                StrSql = StrSql + "OMAXR.M20 AS OMAXRH20, "
                StrSql = StrSql + "OMAXR.M21 AS OMAXRH21, "
                StrSql = StrSql + "OMAXR.M22 AS OMAXRH22, "
                StrSql = StrSql + "OMAXR.M23 AS OMAXRH23, "
                StrSql = StrSql + "OMAXR.M24 AS OMAXRH24, "
                StrSql = StrSql + "OMAXR.M25 AS OMAXRH25, "
                StrSql = StrSql + "OMAXR.M26 AS OMAXRH26, "
                StrSql = StrSql + "OMAXR.M27 AS OMAXRH27, "
                StrSql = StrSql + "OMAXR.M28 AS OMAXRH28, "
                StrSql = StrSql + "OMAXR.M29 AS OMAXRH29, "
                StrSql = StrSql + "OMAXR.M30 AS OMAXRH30, "
                StrSql = StrSql + "(CASE WHEN EQUIP1.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M1*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M1 "
                StrSql = StrSql + "END) AS OPINSTR1, "
                StrSql = StrSql + "(CASE WHEN EQUIP2.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M2*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M2 "
                StrSql = StrSql + "END) AS OPINSTR2, "
                StrSql = StrSql + "(CASE WHEN EQUIP3.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M3*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M3 "
                StrSql = StrSql + "END) AS OPINSTR3, "
                StrSql = StrSql + "(CASE WHEN EQUIP4.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M4*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M4 "
                StrSql = StrSql + "END) AS OPINSTR4, "
                StrSql = StrSql + "(CASE WHEN EQUIP5.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M5*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M5 "
                StrSql = StrSql + "END) AS OPINSTR5, "
                StrSql = StrSql + "(CASE WHEN EQUIP6.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M6*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M6 "
                StrSql = StrSql + "END) AS OPINSTR6, "
                StrSql = StrSql + "(CASE WHEN EQUIP7.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M7*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M7 "
                StrSql = StrSql + "END) AS OPINSTR7, "
                StrSql = StrSql + "(CASE WHEN EQUIP8.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M8*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M8 "
                StrSql = StrSql + "END) AS OPINSTR8, "
                StrSql = StrSql + "(CASE WHEN EQUIP9.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M9*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M9 "
                StrSql = StrSql + "END) AS OPINSTR9, "
                StrSql = StrSql + "(CASE WHEN EQUIP10.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M10*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M10 "
                StrSql = StrSql + "END) AS OPINSTR10, "
                StrSql = StrSql + "(CASE WHEN EQUIP11.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M11*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M11 "
                StrSql = StrSql + "END) AS OPINSTR11, "
                StrSql = StrSql + "(CASE WHEN EQUIP12.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M12*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M12 "
                StrSql = StrSql + "END) AS OPINSTR12, "
                StrSql = StrSql + "(CASE WHEN EQUIP13.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M13*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M13 "
                StrSql = StrSql + "END) AS OPINSTR13, "
                StrSql = StrSql + "(CASE WHEN EQUIP14.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M14*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M14 "
                StrSql = StrSql + "END) AS OPINSTR14, "
                StrSql = StrSql + "(CASE WHEN EQUIP15.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M15*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M15 "
                StrSql = StrSql + "END) AS OPINSTR15, "
                StrSql = StrSql + "(CASE WHEN EQUIP16.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M16*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M16 "
                StrSql = StrSql + "END) AS OPINSTR16, "
                StrSql = StrSql + "(CASE WHEN EQUIP17.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M17*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M17 "
                StrSql = StrSql + "END) AS OPINSTR17, "
                StrSql = StrSql + "(CASE WHEN EQUIP18.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M18*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M18 "
                StrSql = StrSql + "END) AS OPINSTR18, "
                StrSql = StrSql + "(CASE WHEN EQUIP19.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M19*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M19 "
                StrSql = StrSql + "END) AS OPINSTR19, "
                StrSql = StrSql + "(CASE WHEN EQUIP20.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M20*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M20 "
                StrSql = StrSql + "END) AS OPINSTR20, "
                StrSql = StrSql + "(CASE WHEN EQUIP21.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M21*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M21 "
                StrSql = StrSql + "END) AS OPINSTR21, "
                StrSql = StrSql + "(CASE WHEN EQUIP22.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M22*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M22 "
                StrSql = StrSql + "END) AS OPINSTR22, "
                StrSql = StrSql + "(CASE WHEN EQUIP23.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M23*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M23 "
                StrSql = StrSql + "END) AS OPINSTR23, "
                StrSql = StrSql + "(CASE WHEN EQUIP24.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M24*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M24 "
                StrSql = StrSql + "END) AS OPINSTR24, "
                StrSql = StrSql + "(CASE WHEN EQUIP5.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M25*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M25 "
                StrSql = StrSql + "END) AS OPINSTR25, "
                StrSql = StrSql + "(CASE WHEN EQUIP26.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M26*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M26 "
                StrSql = StrSql + "END) AS OPINSTR26, "
                StrSql = StrSql + "(CASE WHEN EQUIP27.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M27*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M27 "
                StrSql = StrSql + "END) AS OPINSTR27, "
                StrSql = StrSql + "(CASE WHEN EQUIP28.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M28*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M28 "
                StrSql = StrSql + "END) AS OPINSTR28, "
                StrSql = StrSql + "(CASE WHEN EQUIP29.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M29*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M29 "
                StrSql = StrSql + "END) AS OPINSTR29, "
                StrSql = StrSql + "(CASE WHEN EQUIP30.UNITS='fpm' THEN "
                StrSql = StrSql + "(OPGSR.M30*PREF.convthick2) "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "OPGSR.M30 "
                StrSql = StrSql + "END) AS OPINSTR30, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP1.UNITS  ELSE EQUIP1.UNITS2 END AS Unit1, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP2.UNITS  ELSE EQUIP2.UNITS2 END AS Unit2, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP3.UNITS  ELSE EQUIP3.UNITS2 END AS Unit3, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP4.UNITS  ELSE EQUIP4.UNITS2 END AS Unit4, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP5.UNITS  ELSE EQUIP5.UNITS2 END AS Unit5, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP6.UNITS  ELSE EQUIP6.UNITS2 END AS Unit6, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP7.UNITS  ELSE EQUIP7.UNITS2 END AS Unit7, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP8.UNITS  ELSE EQUIP8.UNITS2 END AS Unit8, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP9.UNITS  ELSE EQUIP9.UNITS2 END AS Unit9, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP10.UNITS  ELSE EQUIP10.UNITS2 END AS Unit10, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP11.UNITS  ELSE EQUIP11.UNITS2 END AS Unit11, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP12.UNITS  ELSE EQUIP12.UNITS2 END AS Unit12, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP13.UNITS  ELSE EQUIP13.UNITS2 END AS Unit13, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP14.UNITS  ELSE EQUIP14.UNITS2 END AS Unit14, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP15.UNITS  ELSE EQUIP15.UNITS2 END AS Unit15, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP16.UNITS  ELSE EQUIP16.UNITS2 END AS Unit16, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP17.UNITS  ELSE EQUIP17.UNITS2 END AS Unit17, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP18.UNITS  ELSE EQUIP18.UNITS2 END AS Unit18, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP19.UNITS  ELSE EQUIP19.UNITS2 END AS Unit19, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP20.UNITS  ELSE EQUIP20.UNITS2 END AS Unit20, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP21.UNITS  ELSE EQUIP21.UNITS2 END AS Unit21, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP22.UNITS  ELSE EQUIP22.UNITS2 END AS Unit22, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP23.UNITS  ELSE EQUIP23.UNITS2 END AS Unit23, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP24.UNITS  ELSE EQUIP24.UNITS2 END AS Unit24, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP25.UNITS  ELSE EQUIP25.UNITS2 END AS Unit25, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP26.UNITS  ELSE EQUIP26.UNITS2 END AS Unit26, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP27.UNITS  ELSE EQUIP27.UNITS2 END AS Unit27, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP28.UNITS  ELSE EQUIP28.UNITS2 END AS Unit28, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP29.UNITS  ELSE EQUIP29.UNITS2 END AS Unit29, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN EQUIP30.UNITS  ELSE EQUIP30.UNITS2 END AS Unit30, "
                StrSql = StrSql + "(OPLBSPERHOUR.M1 * PREF.CONVWT) AS INSTR1, "
                StrSql = StrSql + "(OPLBSPERHOUR.M2 * PREF.CONVWT) AS INSTR2, "
                StrSql = StrSql + "(OPLBSPERHOUR.M3 * PREF.CONVWT) AS INSTR3, "
                StrSql = StrSql + "(OPLBSPERHOUR.M4 * PREF.CONVWT) AS INSTR4, "
                StrSql = StrSql + "(OPLBSPERHOUR.M5 * PREF.CONVWT) AS INSTR5, "
                StrSql = StrSql + "(OPLBSPERHOUR.M6 * PREF.CONVWT) AS INSTR6, "
                StrSql = StrSql + "(OPLBSPERHOUR.M7 * PREF.CONVWT) AS INSTR7, "
                StrSql = StrSql + "(OPLBSPERHOUR.M8 * PREF.CONVWT) AS INSTR8, "
                StrSql = StrSql + "(OPLBSPERHOUR.M9 * PREF.CONVWT) AS INSTR9, "
                StrSql = StrSql + "(OPLBSPERHOUR.M10 * PREF.CONVWT) AS INSTR10, "
                StrSql = StrSql + "(OPLBSPERHOUR.M11 * PREF.CONVWT) AS INSTR11, "
                StrSql = StrSql + "(OPLBSPERHOUR.M12 * PREF.CONVWT) AS INSTR12, "
                StrSql = StrSql + "(OPLBSPERHOUR.M13 * PREF.CONVWT) AS INSTR13, "
                StrSql = StrSql + "(OPLBSPERHOUR.M14 * PREF.CONVWT) AS INSTR14, "
                StrSql = StrSql + "(OPLBSPERHOUR.M15 * PREF.CONVWT) AS INSTR15, "
                StrSql = StrSql + "(OPLBSPERHOUR.M16 * PREF.CONVWT) AS INSTR16, "
                StrSql = StrSql + "(OPLBSPERHOUR.M17 * PREF.CONVWT) AS INSTR17, "
                StrSql = StrSql + "(OPLBSPERHOUR.M18 * PREF.CONVWT) AS INSTR18, "
                StrSql = StrSql + "(OPLBSPERHOUR.M19 * PREF.CONVWT) AS INSTR19, "
                StrSql = StrSql + "(OPLBSPERHOUR.M20 * PREF.CONVWT) AS INSTR20, "
                StrSql = StrSql + "(OPLBSPERHOUR.M21 * PREF.CONVWT) AS INSTR21, "
                StrSql = StrSql + "(OPLBSPERHOUR.M22 * PREF.CONVWT) AS INSTR22, "
                StrSql = StrSql + "(OPLBSPERHOUR.M23 * PREF.CONVWT) AS INSTR23, "
                StrSql = StrSql + "(OPLBSPERHOUR.M24 * PREF.CONVWT) AS INSTR24, "
                StrSql = StrSql + "(OPLBSPERHOUR.M25 * PREF.CONVWT) AS INSTR25, "
                StrSql = StrSql + "(OPLBSPERHOUR.M26 * PREF.CONVWT) AS INSTR26, "
                StrSql = StrSql + "(OPLBSPERHOUR.M27 * PREF.CONVWT) AS INSTR27, "
                StrSql = StrSql + "(OPLBSPERHOUR.M28 * PREF.CONVWT) AS INSTR28, "
                StrSql = StrSql + "(OPLBSPERHOUR.M29 * PREF.CONVWT) AS INSTR29, "
                StrSql = StrSql + "(OPLBSPERHOUR.M30 * PREF.CONVWT) AS INSTR30, "
                StrSql = StrSql + "OPDT.M1 as DT1, "
                StrSql = StrSql + "OPDT.M2 as DT2, "
                StrSql = StrSql + "OPDT.M3 as DT3, "
                StrSql = StrSql + "OPDT.M4 as DT4, "
                StrSql = StrSql + "OPDT.M5 as DT5, "
                StrSql = StrSql + "OPDT.M6 as DT6, "
                StrSql = StrSql + "OPDT.M7 as DT7, "
                StrSql = StrSql + "OPDT.M8 as DT8, "
                StrSql = StrSql + "OPDT.M9 as DT9, "
                StrSql = StrSql + "OPDT.M10 as DT10, "
                StrSql = StrSql + "OPDT.M11 as DT11, "
                StrSql = StrSql + "OPDT.M12 as DT12, "
                StrSql = StrSql + "OPDT.M13 as DT13, "
                StrSql = StrSql + "OPDT.M14 as DT14, "
                StrSql = StrSql + "OPDT.M15 as DT15, "
                StrSql = StrSql + "OPDT.M16 as DT16, "
                StrSql = StrSql + "OPDT.M17 as DT17, "
                StrSql = StrSql + "OPDT.M18 as DT18, "
                StrSql = StrSql + "OPDT.M19 as DT19, "
                StrSql = StrSql + "OPDT.M20 as DT20, "
                StrSql = StrSql + "OPDT.M21 as DT21, "
                StrSql = StrSql + "OPDT.M22 as DT22, "
                StrSql = StrSql + "OPDT.M23 as DT23, "
                StrSql = StrSql + "OPDT.M24 as DT24, "
                StrSql = StrSql + "OPDT.M25 as DT25, "
                StrSql = StrSql + "OPDT.M26 as DT26, "
                StrSql = StrSql + "OPDT.M27 as DT27, "
                StrSql = StrSql + "OPDT.M28 as DT28, "
                StrSql = StrSql + "OPDT.M29 as DT29, "
                StrSql = StrSql + "OPDT.M30 as DT30, "
                StrSql = StrSql + "OPWASTE.M1 as OPWASTE1, "
                StrSql = StrSql + "OPWASTE.M2 as OPWASTE2, "
                StrSql = StrSql + "OPWASTE.M3 as OPWASTE3, "
                StrSql = StrSql + "OPWASTE.M4 as OPWASTE4, "
                StrSql = StrSql + "OPWASTE.M5 as OPWASTE5, "
                StrSql = StrSql + "OPWASTE.M6 as OPWASTE6, "
                StrSql = StrSql + "OPWASTE.M7 as OPWASTE7, "
                StrSql = StrSql + "OPWASTE.M8 as OPWASTE8, "
                StrSql = StrSql + "OPWASTE.M9 as OPWASTE9, "
                StrSql = StrSql + "OPWASTE.M10 as OPWASTE10, "
                StrSql = StrSql + "OPWASTE.M11 as OPWASTE11, "
                StrSql = StrSql + "OPWASTE.M12 as OPWASTE12, "
                StrSql = StrSql + "OPWASTE.M13 as OPWASTE13, "
                StrSql = StrSql + "OPWASTE.M14 as OPWASTE14, "
                StrSql = StrSql + "OPWASTE.M15 as OPWASTE15, "
                StrSql = StrSql + "OPWASTE.M16 as OPWASTE16, "
                StrSql = StrSql + "OPWASTE.M17 as OPWASTE17, "
                StrSql = StrSql + "OPWASTE.M18 as OPWASTE18, "
                StrSql = StrSql + "OPWASTE.M19 as OPWASTE19, "
                StrSql = StrSql + "OPWASTE.M20 as OPWASTE20, "
                StrSql = StrSql + "OPWASTE.M21 as OPWASTE21, "
                StrSql = StrSql + "OPWASTE.M22 as OPWASTE22, "
                StrSql = StrSql + "OPWASTE.M23 as OPWASTE23, "
                StrSql = StrSql + "OPWASTE.M24 as OPWASTE24, "
                StrSql = StrSql + "OPWASTE.M25 as OPWASTE25, "
                StrSql = StrSql + "OPWASTE.M26 as OPWASTE26, "
                StrSql = StrSql + "OPWASTE.M27 as OPWASTE27, "
                StrSql = StrSql + "OPWASTE.M28 as OPWASTE28, "
                StrSql = StrSql + "OPWASTE.M29 as OPWASTE29, "
                StrSql = StrSql + "OPWASTE.M30 as OPWASTE30, "
                StrSql = StrSql + "OPWASTE.W1 AS DESIGNWAST1, "
                StrSql = StrSql + "OPWASTE.W2 AS DESIGNWAST2, "
                StrSql = StrSql + "OPWASTE.W3 AS DESIGNWAST3, "
                StrSql = StrSql + "OPWASTE.W4 AS DESIGNWAST4, "
                StrSql = StrSql + "OPWASTE.W5 AS DESIGNWAST5, "
                StrSql = StrSql + "OPWASTE.W6 AS DESIGNWAST6, "
                StrSql = StrSql + "OPWASTE.W7 AS DESIGNWAST7, "
                StrSql = StrSql + "OPWASTE.W8 AS DESIGNWAST8, "
                StrSql = StrSql + "OPWASTE.W9 AS DESIGNWAST9, "
                StrSql = StrSql + "OPWASTE.W10 AS DESIGNWAST10, "
                StrSql = StrSql + "OPWASTE.W11 AS DESIGNWAST11, "
                StrSql = StrSql + "OPWASTE.W12 AS DESIGNWAST12, "
                StrSql = StrSql + "OPWASTE.W13 AS DESIGNWAST13, "
                StrSql = StrSql + "OPWASTE.W14 AS DESIGNWAST14, "
                StrSql = StrSql + "OPWASTE.W15 AS DESIGNWAST15, "
                StrSql = StrSql + "OPWASTE.W16 AS DESIGNWAST16, "
                StrSql = StrSql + "OPWASTE.W17 AS DESIGNWAST17, "
                StrSql = StrSql + "OPWASTE.W18 AS DESIGNWAST18, "
                StrSql = StrSql + "OPWASTE.W19 AS DESIGNWAST19, "
                StrSql = StrSql + "OPWASTE.W20 AS DESIGNWAST20, "
                StrSql = StrSql + "OPWASTE.W21 AS DESIGNWAST21, "
                StrSql = StrSql + "OPWASTE.W22 AS DESIGNWAST22, "
                StrSql = StrSql + "OPWASTE.W23 AS DESIGNWAST23, "
                StrSql = StrSql + "OPWASTE.W24 AS DESIGNWAST24, "
                StrSql = StrSql + "OPWASTE.W25 AS DESIGNWAST25, "
                StrSql = StrSql + "OPWASTE.W26 AS DESIGNWAST26, "
                StrSql = StrSql + "OPWASTE.W27 AS DESIGNWAST27, "
                StrSql = StrSql + "OPWASTE.W28 AS DESIGNWAST28, "
                StrSql = StrSql + "OPWASTE.W29 AS DESIGNWAST29, "
                StrSql = StrSql + "OPWASTE.W30 AS DESIGNWAST30,  "
                StrSql = StrSql + "EQUIP1.UNITS EqUnit1, "
                StrSql = StrSql + "EQUIP2.UNITS EqUnit2, "
                StrSql = StrSql + "EQUIP3.UNITS EqUnit3, "
                StrSql = StrSql + "EQUIP4.UNITS EqUnit4, "
                StrSql = StrSql + "EQUIP5.UNITS EqUnit5, "
                StrSql = StrSql + "EQUIP6.UNITS EqUnit6, "
                StrSql = StrSql + "EQUIP7.UNITS EqUnit7, "
                StrSql = StrSql + "EQUIP8.UNITS EqUnit8, "
                StrSql = StrSql + "EQUIP9.UNITS EqUnit9, "
                StrSql = StrSql + "EQUIP10.UNITS EqUnit10, "
                StrSql = StrSql + "EQUIP11.UNITS EqUnit11, "
                StrSql = StrSql + "EQUIP12.UNITS EqUnit12, "
                StrSql = StrSql + "EQUIP13.UNITS EqUnit13, "
                StrSql = StrSql + "EQUIP14.UNITS EqUnit14, "
                StrSql = StrSql + "EQUIP15.UNITS EqUnit15, "
                StrSql = StrSql + "EQUIP16.UNITS EqUnit16, "
                StrSql = StrSql + "EQUIP17.UNITS EqUnit17, "
                StrSql = StrSql + "EQUIP18.UNITS EqUnit18, "
                StrSql = StrSql + "EQUIP19.UNITS EqUnit19, "
                StrSql = StrSql + "EQUIP20.UNITS EqUnit20, "
                StrSql = StrSql + "EQUIP21.UNITS EqUnit21, "
                StrSql = StrSql + "EQUIP22.UNITS EqUnit22, "
                StrSql = StrSql + "EQUIP23.UNITS EqUnit23, "
                StrSql = StrSql + "EQUIP24.UNITS EqUnit24, "
                StrSql = StrSql + "EQUIP25.UNITS EqUnit25, "
                StrSql = StrSql + "EQUIP26.UNITS EqUnit26, "
                StrSql = StrSql + "EQUIP27.UNITS EqUnit27, "
                StrSql = StrSql + "EQUIP28.UNITS EqUnit28, "
                StrSql = StrSql + "EQUIP29.UNITS EqUnit29, "
                StrSql = StrSql + "EQUIP30.UNITS EqUnit30, "
                'Added for Bug#344
                StrSql = StrSql + "EQNUM.M1 AS NUM1, "
                StrSql = StrSql + "EQNUM.M2 AS NUM2, "
                StrSql = StrSql + "EQNUM.M3 AS NUM3, "
                StrSql = StrSql + "EQNUM.M4 AS NUM4, "
                StrSql = StrSql + "EQNUM.M5 AS NUM5, "
                StrSql = StrSql + "EQNUM.M6 AS NUM6, "
                StrSql = StrSql + "EQNUM.M7 AS NUM7, "
                StrSql = StrSql + "EQNUM.M8 AS NUM8, "
                StrSql = StrSql + "EQNUM.M9 AS NUM9, "
                StrSql = StrSql + "EQNUM.M10 AS NUM10, "
                StrSql = StrSql + "EQNUM.M11 AS NUM11, "
                StrSql = StrSql + "EQNUM.M12 AS NUM12, "
                StrSql = StrSql + "EQNUM.M13 AS NUM13, "
                StrSql = StrSql + "EQNUM.M14 AS NUM14, "
                StrSql = StrSql + "EQNUM.M15 AS NUM15, "
                StrSql = StrSql + "EQNUM.M16 AS NUM16, "
                StrSql = StrSql + "EQNUM.M17 AS NUM17, "
                StrSql = StrSql + "EQNUM.M18 AS NUM18, "
                StrSql = StrSql + "EQNUM.M19 AS NUM19, "
                StrSql = StrSql + "EQNUM.M20 AS NUM20, "
                StrSql = StrSql + "EQNUM.M21 AS NUM21, "
                StrSql = StrSql + "EQNUM.M22 AS NUM22, "
                StrSql = StrSql + "EQNUM.M23 AS NUM23, "
                StrSql = StrSql + "EQNUM.M24 AS NUM24, "
                StrSql = StrSql + "EQNUM.M25 AS NUM25, "
                StrSql = StrSql + "EQNUM.M26 AS NUM26, "
                StrSql = StrSql + "EQNUM.M27 AS NUM27, "
                StrSql = StrSql + "EQNUM.M28 AS NUM28, "
                StrSql = StrSql + "EQNUM.M29 AS NUM29, "
                StrSql = StrSql + "EQNUM.M30 AS NUM30 "
                'end of Bug3344
                StrSql = StrSql + "FROM EQUIPMENTTYPE EQUIPT "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP1 "
                StrSql = StrSql + "ON EQUIP1.EQUIPID=EQUIPT.M1 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP2 "
                StrSql = StrSql + "ON EQUIP2.EQUIPID=EQUIPT.M2 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP3 "
                StrSql = StrSql + "ON EQUIP3.EQUIPID=EQUIPT.M3 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP4 "
                StrSql = StrSql + "ON EQUIP4.EQUIPID=EQUIPT.M4 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP5 "
                StrSql = StrSql + "ON EQUIP5.EQUIPID=EQUIPT.M5 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP6 "
                StrSql = StrSql + "ON EQUIP6.EQUIPID=EQUIPT.M6 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP7 "
                StrSql = StrSql + "ON EQUIP7.EQUIPID=EQUIPT.M7 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP8 "
                StrSql = StrSql + "ON EQUIP8.EQUIPID=EQUIPT.M8 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP9 "
                StrSql = StrSql + "ON EQUIP9.EQUIPID=EQUIPT.M9 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP10 "
                StrSql = StrSql + "ON EQUIP10.EQUIPID=EQUIPT.M10 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP11 "
                StrSql = StrSql + "ON EQUIP11.EQUIPID=EQUIPT.M11 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP12 "
                StrSql = StrSql + "ON EQUIP12.EQUIPID=EQUIPT.M12 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP13 "
                StrSql = StrSql + "ON EQUIP13.EQUIPID=EQUIPT.M13 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP14 "
                StrSql = StrSql + "ON EQUIP14.EQUIPID=EQUIPT.M14 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP15 "
                StrSql = StrSql + "ON EQUIP15.EQUIPID=EQUIPT.M15 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP16 "
                StrSql = StrSql + "ON EQUIP16.EQUIPID=EQUIPT.M16 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP17 "
                StrSql = StrSql + "ON EQUIP17.EQUIPID=EQUIPT.M17 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP18 "
                StrSql = StrSql + "ON EQUIP18.EQUIPID=EQUIPT.M18 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP19 "
                StrSql = StrSql + "ON EQUIP19.EQUIPID=EQUIPT.M19 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP20 "
                StrSql = StrSql + "ON EQUIP20.EQUIPID=EQUIPT.M20 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP21 "
                StrSql = StrSql + "ON EQUIP21.EQUIPID=EQUIPT.M21 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP22 "
                StrSql = StrSql + "ON EQUIP22.EQUIPID=EQUIPT.M22 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP23 "
                StrSql = StrSql + "ON EQUIP23.EQUIPID=EQUIPT.M23 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP24 "
                StrSql = StrSql + "ON EQUIP24.EQUIPID=EQUIPT.M24 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP25 "
                StrSql = StrSql + "ON EQUIP25.EQUIPID=EQUIPT.M25 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP26 "
                StrSql = StrSql + "ON EQUIP26.EQUIPID=EQUIPT.M26 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP27 "
                StrSql = StrSql + "ON EQUIP27.EQUIPID=EQUIPT.M27 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP28 "
                StrSql = StrSql + "ON EQUIP28.EQUIPID=EQUIPT.M28 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP29 "
                StrSql = StrSql + "ON EQUIP29.EQUIPID=EQUIPT.M29 "
                StrSql = StrSql + "INNER JOIN EQUIPMENT EQUIP30 "
                StrSql = StrSql + "ON EQUIP30.EQUIPID=EQUIPT.M30 "
                StrSql = StrSql + "INNER JOIN OPWEBWIDTH OPW "
                StrSql = StrSql + "ON OPW.CASEID=EQUIPT.CASEID "
                StrSql = StrSql + "INNER JOIN PREFERENCES  PREF ON "
                StrSql = StrSql + "PREF.CASEID=EQUIPT.CASEID "
                StrSql = StrSql + "INNER JOIN OPMAXRUNHRS OMAXR "
                StrSql = StrSql + "ON OMAXR.CASEID=EQUIPT.CASEID "
                StrSql = StrSql + "INNER JOIN OPINSTGRSRATE OPGSR "
                StrSql = StrSql + "ON OPGSR.CASEID=EQUIPT.CASEID "
                StrSql = StrSql + "INNER JOIN OPLBSPERHOUR "
                StrSql = StrSql + "ON OPLBSPERHOUR.CASEID=EQUIPT.CASEID "
                StrSql = StrSql + "INNER JOIN OPDOWNTIME OPDT "
                StrSql = StrSql + "ON OPDT.CASEID=EQUIPT.CASEID "
                StrSql = StrSql + "INNER JOIN OPWASTE "
                StrSql = StrSql + "ON OPWASTE.CASEID=EQUIPT.CASEID "
                StrSql = StrSql + "INNER JOIN OPEXITS OPE "
                StrSql = StrSql + "ON OPE.CASEID=EQUIPT.CASEID "
                StrSql = StrSql + "INNER JOIN EQUIPMENTNUMBER EQNUM "
                StrSql = StrSql + "ON EQNUM.CASEID=EQUIPT.CASEID "
                StrSql = StrSql + "WHERE EQUIPT.CASEID=" + CaseId.ToString() + ""

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetSupportDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPersonnelInDetails(ByVal CaseId As Integer, ByVal EFFCOUNTRY As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT PERSPOS.CASEID,  "
                StrSql = StrSql + "TO_CHAR(NVL(PERSPOS.EFFDATE,TO_DATE('Jan 01,1900','MON DD,YYYY')),'MON DD,YYYY')AS EFFDATE , "
                StrSql = StrSql + "PREF.title2 AS PREFTITLE2, "
                StrSql = StrSql + "PREF.UNITS, "
                StrSql = StrSql + "PERSPOS.M1 AS PERSPOS1, "
                StrSql = StrSql + "PERSPOS.M2 AS PERSPOS2, "
                StrSql = StrSql + "PERSPOS.M3 AS PERSPOS3, "
                StrSql = StrSql + "PERSPOS.M4 AS PERSPOS4, "
                StrSql = StrSql + "PERSPOS.M5 AS PERSPOS5, "
                StrSql = StrSql + "PERSPOS.M6 AS PERSPOS6, "
                StrSql = StrSql + "PERSPOS.M7 AS PERSPOS7, "
                StrSql = StrSql + "PERSPOS.M8 AS PERSPOS8, "
                StrSql = StrSql + "PERSPOS.M9 AS PERSPOS9, "
                StrSql = StrSql + "PERSPOS.M10 AS PERSPOS10, "
                StrSql = StrSql + "PERSPOS.M11 AS PERSPOS11, "
                StrSql = StrSql + "PERSPOS.M12 AS PERSPOS12, "
                StrSql = StrSql + "PERSPOS.M13 AS PERSPOS13, "
                StrSql = StrSql + "PERSPOS.M14 AS PERSPOS14, "
                StrSql = StrSql + "PERSPOS.M15 AS PERSPOS15, "
                StrSql = StrSql + "PERSPOS.M16 AS PERSPOS16, "
                StrSql = StrSql + "PERSPOS.M17 AS PERSPOS17, "
                StrSql = StrSql + "PERSPOS.M18 AS PERSPOS18, "
                StrSql = StrSql + "PERSPOS.M19 AS PERSPOS19, "
                StrSql = StrSql + "PERSPOS.M20 AS PERSPOS20, "
                StrSql = StrSql + "PERSPOS.M21 AS PERSPOS21, "
                StrSql = StrSql + "PERSPOS.M22 AS PERSPOS22, "
                StrSql = StrSql + "PERSPOS.M23 AS PERSPOS23, "
                StrSql = StrSql + "PERSPOS.M24 AS PERSPOS24, "
                StrSql = StrSql + "PERSPOS.M25 AS PERSPOS25, "
                StrSql = StrSql + "PERSPOS.M26 AS PERSPOS26, "
                StrSql = StrSql + "PERSPOS.M27 AS PERSPOS27, "
                StrSql = StrSql + "PERSPOS.M28 AS PERSPOS28, "
                StrSql = StrSql + "PERSPOS.M29 AS PERSPOS29, "
                StrSql = StrSql + "PERSPOS.M30 AS PERSPOS30, "
                StrSql = StrSql + "PNUM.M1 AS PERNUM1, "
                StrSql = StrSql + "PNUM.M2 AS PERNUM2, "
                StrSql = StrSql + "PNUM.M3 AS PERNUM3, "
                StrSql = StrSql + "PNUM.M4 AS PERNUM4, "
                StrSql = StrSql + "PNUM.M5 AS PERNUM5, "
                StrSql = StrSql + "PNUM.M6 AS PERNUM6, "
                StrSql = StrSql + "PNUM.M7 AS PERNUM7, "
                StrSql = StrSql + "PNUM.M8 AS PERNUM8, "
                StrSql = StrSql + "PNUM.M9 AS PERNUM9, "
                StrSql = StrSql + "PNUM.M10 AS PERNUM10, "
                StrSql = StrSql + "PNUM.M11 AS PERNUM11, "
                StrSql = StrSql + "PNUM.M12 AS PERNUM12, "
                StrSql = StrSql + "PNUM.M13 AS PERNUM13, "
                StrSql = StrSql + "PNUM.M14 AS PERNUM14, "
                StrSql = StrSql + "PNUM.M15 AS PERNUM15, "
                StrSql = StrSql + "PNUM.M16 AS PERNUM16, "
                StrSql = StrSql + "PNUM.M17 AS PERNUM17, "
                StrSql = StrSql + "PNUM.M18 AS PERNUM18, "
                StrSql = StrSql + "PNUM.M19 AS PERNUM19, "
                StrSql = StrSql + "PNUM.M20 AS PERNUM20, "
                StrSql = StrSql + "PNUM.M21 AS PERNUM21, "
                StrSql = StrSql + "PNUM.M22 AS PERNUM22, "
                StrSql = StrSql + "PNUM.M23 AS PERNUM23, "
                StrSql = StrSql + "PNUM.M24 AS PERNUM24, "
                StrSql = StrSql + "PNUM.M25 AS PERNUM25, "
                StrSql = StrSql + "PNUM.M26 AS PERNUM26, "
                StrSql = StrSql + "PNUM.M27 AS PERNUM27, "
                StrSql = StrSql + "PNUM.M28 AS PERNUM28, "
                StrSql = StrSql + "PNUM.M29 AS PERNUM29, "
                StrSql = StrSql + "PNUM.M30 AS PERNUM30, "
                StrSql = StrSql + "(PERSPOS1.SALARY*PREF.CURR) AS SALS1, "
                StrSql = StrSql + "(PERSPOS2.SALARY*PREF.CURR) AS SALS2, "
                StrSql = StrSql + "(PERSPOS3.SALARY*PREF.CURR) AS SALS3, "
                StrSql = StrSql + "(PERSPOS4.SALARY*PREF.CURR) AS SALS4, "
                StrSql = StrSql + "(PERSPOS5.SALARY*PREF.CURR) AS SALS5, "
                StrSql = StrSql + "(PERSPOS6.SALARY*PREF.CURR) AS SALS6, "
                StrSql = StrSql + "(PERSPOS7.SALARY*PREF.CURR) AS SALS7, "
                StrSql = StrSql + "(PERSPOS8.SALARY*PREF.CURR) AS SALS8, "
                StrSql = StrSql + "(PERSPOS9.SALARY*PREF.CURR) AS SALS9, "
                StrSql = StrSql + "(PERSPOS10.SALARY*PREF.CURR) AS SALS10, "
                StrSql = StrSql + "(PERSPOS11.SALARY*PREF.CURR) AS SALS11, "
                StrSql = StrSql + "(PERSPOS12.SALARY*PREF.CURR) AS SALS12, "
                StrSql = StrSql + "(PERSPOS13.SALARY*PREF.CURR) AS SALS13, "
                StrSql = StrSql + "(PERSPOS14.SALARY*PREF.CURR) AS SALS14, "
                StrSql = StrSql + "(PERSPOS15.SALARY*PREF.CURR) AS SALS15, "
                StrSql = StrSql + "(PERSPOS16.SALARY*PREF.CURR) AS SALS16, "
                StrSql = StrSql + "(PERSPOS17.SALARY*PREF.CURR) AS SALS17, "
                StrSql = StrSql + "(PERSPOS18.SALARY*PREF.CURR) AS SALS18, "
                StrSql = StrSql + "(PERSPOS19.SALARY*PREF.CURR) AS SALS19, "
                StrSql = StrSql + "(PERSPOS20.SALARY*PREF.CURR) AS SALS20, "
                StrSql = StrSql + "(PERSPOS21.SALARY*PREF.CURR) AS SALS21, "
                StrSql = StrSql + "(PERSPOS22.SALARY*PREF.CURR) AS SALS22, "
                StrSql = StrSql + "(PERSPOS23.SALARY*PREF.CURR) AS SALS23, "
                StrSql = StrSql + "(PERSPOS24.SALARY*PREF.CURR) AS SALS24, "
                StrSql = StrSql + "(PERSPOS25.SALARY*PREF.CURR) AS SALS25, "
                StrSql = StrSql + "(PERSPOS26.SALARY*PREF.CURR) AS SALS26, "
                StrSql = StrSql + "(PERSPOS27.SALARY*PREF.CURR) AS SALS27, "
                StrSql = StrSql + "(PERSPOS28.SALARY*PREF.CURR) AS SALS28, "
                StrSql = StrSql + "(PERSPOS29.SALARY*PREF.CURR) AS SALS29, "
                StrSql = StrSql + "(PERSPOS30.SALARY*PREF.CURR) AS SALS30, "
                StrSql = StrSql + "(PERSAL.M1*PREF.CURR) AS SALPRE1, "
                StrSql = StrSql + "(PERSAL.M2*PREF.CURR) AS SALPRE2, "
                StrSql = StrSql + "(PERSAL.M3*PREF.CURR) AS SALPRE3, "
                StrSql = StrSql + "(PERSAL.M4*PREF.CURR) AS SALPRE4, "
                StrSql = StrSql + "(PERSAL.M5*PREF.CURR) AS SALPRE5, "
                StrSql = StrSql + "(PERSAL.M6*PREF.CURR) AS SALPRE6, "
                StrSql = StrSql + "(PERSAL.M7*PREF.CURR) AS SALPRE7, "
                StrSql = StrSql + "(PERSAL.M8*PREF.CURR) AS SALPRE8, "
                StrSql = StrSql + "(PERSAL.M9*PREF.CURR) AS SALPRE9, "
                StrSql = StrSql + "(PERSAL.M10*PREF.CURR) AS SALPRE10, "
                StrSql = StrSql + "(PERSAL.M11*PREF.CURR) AS SALPRE11, "
                StrSql = StrSql + "(PERSAL.M12*PREF.CURR) AS SALPRE12, "
                StrSql = StrSql + "(PERSAL.M13*PREF.CURR) AS SALPRE13, "
                StrSql = StrSql + "(PERSAL.M14*PREF.CURR) AS SALPRE14, "
                StrSql = StrSql + "(PERSAL.M15*PREF.CURR) AS SALPRE15, "
                StrSql = StrSql + "(PERSAL.M16*PREF.CURR) AS SALPRE16, "
                StrSql = StrSql + "(PERSAL.M17*PREF.CURR) AS SALPRE17, "
                StrSql = StrSql + "(PERSAL.M18*PREF.CURR) AS SALPRE18, "
                StrSql = StrSql + "(PERSAL.M19*PREF.CURR) AS SALPRE19, "
                StrSql = StrSql + "(PERSAL.M20*PREF.CURR) AS SALPRE20, "
                StrSql = StrSql + "(PERSAL.M21*PREF.CURR) AS SALPRE21, "
                StrSql = StrSql + "(PERSAL.M22*PREF.CURR) AS SALPRE22, "
                StrSql = StrSql + "(PERSAL.M23*PREF.CURR) AS SALPRE23, "
                StrSql = StrSql + "(PERSAL.M24*PREF.CURR) AS SALPRE24, "
                StrSql = StrSql + "(PERSAL.M25*PREF.CURR) AS SALPRE25, "
                StrSql = StrSql + "(PERSAL.M26*PREF.CURR) AS SALPRE26, "
                StrSql = StrSql + "(PERSAL.M27*PREF.CURR) AS SALPRE27, "
                StrSql = StrSql + "(PERSAL.M28*PREF.CURR) AS SALPRE28, "
                StrSql = StrSql + "(PERSAL.M29*PREF.CURR) AS SALPRE29, "
                StrSql = StrSql + "(PERSAL.M30*PREF.CURR) AS SALPRE30, "
                StrSql = StrSql + "PERSVP.M1 AS COSTTYPID1, "
                StrSql = StrSql + "PERSVP.M2 AS COSTTYPID2, "
                StrSql = StrSql + "PERSVP.M3 AS COSTTYPID3, "
                StrSql = StrSql + "PERSVP.M4 AS COSTTYPID4, "
                StrSql = StrSql + "PERSVP.M5 AS COSTTYPID5, "
                StrSql = StrSql + "PERSVP.M6 AS COSTTYPID6, "
                StrSql = StrSql + "PERSVP.M7 AS COSTTYPID7, "
                StrSql = StrSql + "PERSVP.M8 AS COSTTYPID8, "
                StrSql = StrSql + "PERSVP.M9 AS COSTTYPID9, "
                StrSql = StrSql + "PERSVP.M10 AS COSTTYPID10, "
                StrSql = StrSql + "PERSVP.M11 AS COSTTYPID11, "
                StrSql = StrSql + "PERSVP.M12 AS COSTTYPID12, "
                StrSql = StrSql + "PERSVP.M13 AS COSTTYPID13, "
                StrSql = StrSql + "PERSVP.M14 AS COSTTYPID14, "
                StrSql = StrSql + "PERSVP.M15 AS COSTTYPID15, "
                StrSql = StrSql + "PERSVP.M16 AS COSTTYPID16, "
                StrSql = StrSql + "PERSVP.M17 AS COSTTYPID17, "
                StrSql = StrSql + "PERSVP.M18 AS COSTTYPID18, "
                StrSql = StrSql + "PERSVP.M19 AS COSTTYPID19, "
                StrSql = StrSql + "PERSVP.M20 AS COSTTYPID20, "
                StrSql = StrSql + "PERSVP.M21 AS COSTTYPID21, "
                StrSql = StrSql + "PERSVP.M22 AS COSTTYPID22, "
                StrSql = StrSql + "PERSVP.M23 AS COSTTYPID23, "
                StrSql = StrSql + "PERSVP.M24 AS COSTTYPID24, "
                StrSql = StrSql + "PERSVP.M25 AS COSTTYPID25, "
                StrSql = StrSql + "PERSVP.M26 AS COSTTYPID26, "
                StrSql = StrSql + "PERSVP.M27 AS COSTTYPID27, "
                StrSql = StrSql + "PERSVP.M28 AS COSTTYPID28, "
                StrSql = StrSql + "PERSVP.M29 AS COSTTYPID29, "
                StrSql = StrSql + "PERSVP.M30 AS COSTTYPID30, "

                StrSql = StrSql + "PERDEP.M1 AS DEPID1,  "
                StrSql = StrSql + "PERDEP.M2 AS DEPID2, "
                StrSql = StrSql + "PERDEP.M3 AS DEPID3, "
                StrSql = StrSql + "PERDEP.M4 AS DEPID4, "
                StrSql = StrSql + "PERDEP.M5 AS DEPID5, "
                StrSql = StrSql + "PERDEP.M6 AS DEPID6, "
                StrSql = StrSql + "PERDEP.M7 AS DEPID7, "
                StrSql = StrSql + "PERDEP.M8 AS DEPID8, "
                StrSql = StrSql + "PERDEP.M9 AS DEPID9, "
                StrSql = StrSql + "PERDEP.M10 AS DEPID10, "
                StrSql = StrSql + "PERDEP.M11 AS DEPID11, "
                StrSql = StrSql + "PERDEP.M12 AS DEPID12, "
                StrSql = StrSql + "PERDEP.M13 AS DEPID13, "
                StrSql = StrSql + "PERDEP.M14 AS DEPID14, "
                StrSql = StrSql + "PERDEP.M15 AS DEPID15, "
                StrSql = StrSql + "PERDEP.M16 AS DEPID16, "
                StrSql = StrSql + "PERDEP.M17 AS DEPID17, "
                StrSql = StrSql + "PERDEP.M18 AS DEPID18, "
                StrSql = StrSql + "PERDEP.M19 AS DEPID19, "
                StrSql = StrSql + "PERDEP.M20 AS DEPID20, "
                StrSql = StrSql + "PERDEP.M21 AS DEPID21, "
                StrSql = StrSql + "PERDEP.M22 AS DEPID22, "
                StrSql = StrSql + "PERDEP.M23 AS DEPID23, "
                StrSql = StrSql + "PERDEP.M24 AS DEPID24, "
                StrSql = StrSql + "PERDEP.M25 AS DEPID25, "
                StrSql = StrSql + "PERDEP.M26 AS DEPID26, "
                StrSql = StrSql + "PERDEP.M27 AS DEPID27, "
                StrSql = StrSql + "PERDEP.M28 AS DEPID28, "
                StrSql = StrSql + "PERDEP.M29 AS DEPID29, "
                StrSql = StrSql + "PERDEP.M30 AS DEPID30, "
                StrSql = StrSql + "TO_CHAR(PERSPOS.EFFDATE,'MON DD, YYYY')EFFDATE "
                StrSql = StrSql + "FROM PERSONNELPOS PERSPOS "
                StrSql = StrSql + "INNER JOIN PERSONNELNUM PNUM "
                StrSql = StrSql + "ON PNUM.CASEID=PERSPOS.CASEID "
                StrSql = StrSql + "INNER JOIN PREFERENCES  PREF ON "
                StrSql = StrSql + "PREF.CASEID=PERSPOS.CASEID "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + " PERSPOS1 "
                StrSql = StrSql + "ON PERSPOS1.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS1.PERSID = PERSPOS.M1 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS2 "
                StrSql = StrSql + "ON PERSPOS2.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS2.PERSID = PERSPOS.M2 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS3 "
                StrSql = StrSql + "ON PERSPOS3.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS3.PERSID = PERSPOS.M3 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS4 "
                StrSql = StrSql + "ON PERSPOS4.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS4.PERSID = PERSPOS.M4 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS5 "
                StrSql = StrSql + "ON PERSPOS5.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS5.PERSID = PERSPOS.M5 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS6 "
                StrSql = StrSql + "ON PERSPOS6.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS6.PERSID = PERSPOS.M6 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS7 "
                StrSql = StrSql + "ON PERSPOS7.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS7.PERSID = PERSPOS.M7 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS8 "
                StrSql = StrSql + "ON PERSPOS8.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS8.PERSID = PERSPOS.M8 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS9 "
                StrSql = StrSql + "ON PERSPOS9.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS9.PERSID = PERSPOS.M9 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS10 "
                StrSql = StrSql + "ON PERSPOS10.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS10.PERSID = PERSPOS.M10 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS11 "
                StrSql = StrSql + "ON PERSPOS11.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS11.PERSID = PERSPOS.M11 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS12 "
                StrSql = StrSql + "ON PERSPOS12.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS12.PERSID = PERSPOS.M12 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS13 "
                StrSql = StrSql + "ON PERSPOS13.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS13.PERSID = PERSPOS.M13 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS14 "
                StrSql = StrSql + "ON PERSPOS14.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS14.PERSID = PERSPOS.M14 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS15 "
                StrSql = StrSql + "ON PERSPOS15.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS15.PERSID = PERSPOS.M15 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS16 "
                StrSql = StrSql + "ON PERSPOS16.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS16.PERSID = PERSPOS.M16 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS17 "
                StrSql = StrSql + "ON PERSPOS17.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS17.PERSID = PERSPOS.M17 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS18 "
                StrSql = StrSql + "ON PERSPOS18.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS18.PERSID = PERSPOS.M18 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS19 "
                StrSql = StrSql + "ON PERSPOS19.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS19.PERSID = PERSPOS.M19 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS20 "
                StrSql = StrSql + "ON PERSPOS20.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS20.PERSID = PERSPOS.M20 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS21 "
                StrSql = StrSql + "ON PERSPOS21.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS21.PERSID = PERSPOS.M21 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS22 "
                StrSql = StrSql + "ON PERSPOS22.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS22.PERSID = PERSPOS.M22 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS23 "
                StrSql = StrSql + "ON PERSPOS23.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS23.PERSID = PERSPOS.M23 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS24 "
                StrSql = StrSql + "ON PERSPOS24.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS24.PERSID = PERSPOS.M24 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS25 "
                StrSql = StrSql + "ON PERSPOS25.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS25.PERSID = PERSPOS.M25 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS26 "
                StrSql = StrSql + "ON PERSPOS26.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS26.PERSID = PERSPOS.M26 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS27 "
                StrSql = StrSql + "ON PERSPOS27.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS27.PERSID = PERSPOS.M27 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS28 "
                StrSql = StrSql + "ON PERSPOS28.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS28.PERSID = PERSPOS.M28 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS29 "
                StrSql = StrSql + "ON PERSPOS29.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS29.PERSID = PERSPOS.M29 "
                StrSql = StrSql + "INNER JOIN " + " ECON." + EFFCOUNTRY + "  PERSPOS30 "
                StrSql = StrSql + "ON PERSPOS30.EFFDATE = PERSPOS.EFFDATE "
                StrSql = StrSql + "AND PERSPOS30.PERSID = PERSPOS.M30 "
                StrSql = StrSql + "INNER JOIN PERSONNELSAL PERSAL "
                StrSql = StrSql + "ON PERSAL.CASEID=PERSPOS.CASEID "
                StrSql = StrSql + "INNER JOIN PERSONNELVP PERSVP "
                StrSql = StrSql + "ON PERSVP.CASEID=PERSPOS.CASEID "
                StrSql = StrSql + "INNER JOIN PERSONNELDEP PERDEP  "
                StrSql = StrSql + "ON PERDEP.CASEID=PERSPOS.CASEID "
                StrSql = StrSql + "WHERE PERSPOS.CASEID=" + CaseId.ToString() + ""

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetSupportDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
       

        Public Function GetFixedCostDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT FIXCOST.CASEID,  "
                StrSql = StrSql + "'Office supplies:' AS CATEGORY1, "
                StrSql = StrSql + "'Laboratory supplies:' AS CATEGORY2, "
                StrSql = StrSql + "'Insurance:' AS CATEGORY3, "
                StrSql = StrSql + "'Travel:' AS CATEGORY4, "
                StrSql = StrSql + "'Voice and data:' AS CATEGORY5, "
                StrSql = StrSql + "'Water:' AS CATEGORY6, "
                StrSql = StrSql + "'Waste disposal' AS CATEGORY7, "
                StrSql = StrSql + "'Maintenance supplies' AS CATEGORY8, "
                StrSql = StrSql + "'Minor equipment' AS CATEGORY9, "
                StrSql = StrSql + "'Outside services' AS CATEGORY10, "
                StrSql = StrSql + "'Professional services' AS CATEGORY11, "
                StrSql = StrSql + "'Ink supplies' AS CATEGORY12, "
                StrSql = StrSql + "'Plate supplies' AS CATEGORY13, "
                StrSql = StrSql + "'Metallization supplies' AS CATEGORY14, "
                StrSql = StrSql + "'Expense15:' AS CATEGORY15, "
                StrSql = StrSql + "'Expense16:' AS CATEGORY16, "
                StrSql = StrSql + "'Expense17:' AS CATEGORY17, "
                StrSql = StrSql + "'Expense18:' AS CATEGORY18, "
                StrSql = StrSql + "'Expense19:' AS CATEGORY19, "
                StrSql = StrSql + "'Expense20:' AS CATEGORY20, "
                StrSql = StrSql + "'Expense21:' AS CATEGORY21, "
                StrSql = StrSql + "'Expense22:' AS CATEGORY22, "
                StrSql = StrSql + "'Expense23:' AS CATEGORY23, "
                StrSql = StrSql + "'Expense24:' AS CATEGORY24, "
                StrSql = StrSql + "'Expense25:' AS CATEGORY25, "
                StrSql = StrSql + "'Expense26:' AS CATEGORY26, "
                StrSql = StrSql + "'Expense27:' AS CATEGORY27, "
                StrSql = StrSql + "'Expense28:' AS CATEGORY28, "
                StrSql = StrSql + "'Expense29:' AS CATEGORY29, "
                StrSql = StrSql + "'Expense30:' AS CATEGORY30, "
                StrSql = StrSql + "(FIXCOST.M1*PREF.CURR) AS FIXCOST1, "
                StrSql = StrSql + "(FIXCOST.M2*PREF.CURR) AS FIXCOST2, "
                StrSql = StrSql + "FIXCOST.M3 AS FIXCOST3, "
                StrSql = StrSql + "FIXCOST.M4 AS FIXCOST4, "
                StrSql = StrSql + "(FIXCOST.M5*PREF.CURR) AS FIXCOST5, "
                StrSql = StrSql + "(FIXCOST.M6*PREF.CURR) AS FIXCOST6, "
                StrSql = StrSql + "(FIXCOST.M7*PREF.CURR) AS FIXCOST7, "
                StrSql = StrSql + "FIXCOST.M8 AS FIXCOST8, "
                StrSql = StrSql + "FIXCOST.M9 AS FIXCOST9, "
                StrSql = StrSql + "FIXCOST.M10 AS FIXCOST10, "
                StrSql = StrSql + "FIXCOST.M11 AS FIXCOST11, "
                StrSql = StrSql + "FIXCOST.M12 AS FIXCOST12, "
                StrSql = StrSql + "FIXCOST.M13 AS FIXCOST13, "
                StrSql = StrSql + "FIXCOST.M14 AS FIXCOST14, "
                StrSql = StrSql + "FIXCOST.M15 AS FIXCOST15, "
                StrSql = StrSql + "FIXCOST.M16 AS FIXCOST16, "
                StrSql = StrSql + "FIXCOST.M17 AS FIXCOST17, "
                StrSql = StrSql + "FIXCOST.M18 AS FIXCOST18, "
                StrSql = StrSql + "FIXCOST.M19 AS FIXCOST19, "
                StrSql = StrSql + "FIXCOST.M20 AS FIXCOST20, "
                StrSql = StrSql + "FIXCOST.M21 AS FIXCOST21, "
                StrSql = StrSql + "FIXCOST.M22 AS FIXCOST22, "
                StrSql = StrSql + "FIXCOST.M23 AS FIXCOST23, "
                StrSql = StrSql + "FIXCOST.M24 AS FIXCOST24, "
                StrSql = StrSql + "FIXCOST.M25 AS FIXCOST25, "
                StrSql = StrSql + "FIXCOST.M26 AS FIXCOST26, "
                StrSql = StrSql + "FIXCOST.M27 AS FIXCOST27, "
                StrSql = StrSql + "FIXCOST.M28 AS FIXCOST28, "
                StrSql = StrSql + "FIXCOST.M29 AS FIXCOST29, "
                StrSql = StrSql + "FIXCOST.M30 AS FIXCOST30, "
                StrSql = StrSql + "'currency per employee' AS RULE1, "
                StrSql = StrSql + "'currency per employee' AS RULE2, "
                StrSql = StrSql + "'percent of asset base' AS RULE3, "
                StrSql = StrSql + "'percent of payroll' AS RULE4, "
                StrSql = StrSql + "'currency per employee' AS RULE5, "
                StrSql = StrSql + "'currency per employee' AS RULE6, "
                StrSql = StrSql + "'currency per employee' AS RULE7, "
                StrSql = StrSql + "'percent of asset base' AS RULE8, "
                StrSql = StrSql + "'percent of asset base' AS RULE9, "
                StrSql = StrSql + "'percent of asset base' AS RULE10, "
                StrSql = StrSql + "'percent of payroll' AS RULE11, "
                StrSql = StrSql + "'percent of ink cost' AS RULE12, "
                StrSql = StrSql + "'percent of plate cost' AS RULE13, "
                StrSql = StrSql + "'percent of metal cost' AS RULE14, "
                StrSql = StrSql + "'' AS RULE15, "
                StrSql = StrSql + "'' AS RULE16, "
                StrSql = StrSql + "'' AS RULE17, "
                StrSql = StrSql + "'' AS RULE18, "
                StrSql = StrSql + "'' AS RULE19, "
                StrSql = StrSql + "'' AS RULE20, "
                StrSql = StrSql + "'' AS RULE21, "
                StrSql = StrSql + "'' AS RULE22, "
                StrSql = StrSql + "'' AS RULE23, "
                StrSql = StrSql + "'' AS RULE24, "
                StrSql = StrSql + "'' AS RULE25, "
                StrSql = StrSql + "'' AS RULE26, "
                StrSql = StrSql + "'' AS RULE27, "
                StrSql = StrSql + "'' AS RULE28, "
                StrSql = StrSql + "'' AS RULE29, "
                StrSql = StrSql + "'' AS RULE30, "
                StrSql = StrSql + "(FIXCOSTSUG.M1*PREF.CURR) AS FCSG1, "
                StrSql = StrSql + "(FIXCOSTSUG.M2*PREF.CURR) AS FCSG2, "
                StrSql = StrSql + "(FIXCOSTSUG.M3*PREF.CURR) AS FCSG3, "
                StrSql = StrSql + "(FIXCOSTSUG.M4*PREF.CURR) AS FCSG4, "
                StrSql = StrSql + "(FIXCOSTSUG.M5*PREF.CURR) AS FCSG5, "
                StrSql = StrSql + "(FIXCOSTSUG.M6*PREF.CURR) AS FCSG6, "
                StrSql = StrSql + "(FIXCOSTSUG.M7*PREF.CURR) AS FCSG7, "
                StrSql = StrSql + "(FIXCOSTSUG.M8*PREF.CURR) AS FCSG8, "
                StrSql = StrSql + "(FIXCOSTSUG.M9*PREF.CURR) AS FCSG9, "
                StrSql = StrSql + "(FIXCOSTSUG.M10*PREF.CURR) AS FCSG10, "
                StrSql = StrSql + "(FIXCOSTSUG.M11*PREF.CURR) AS FCSG11, "
                StrSql = StrSql + "(FIXCOSTSUG.M12*PREF.CURR) AS FCSG12, "
                StrSql = StrSql + "(FIXCOSTSUG.M13*PREF.CURR) AS FCSG13, "
                StrSql = StrSql + "(FIXCOSTSUG.M14*PREF.CURR) AS FCSG14, "
                StrSql = StrSql + "(FIXCOSTSUG.M15*PREF.CURR) AS FCSG15, "
                StrSql = StrSql + "(FIXCOSTSUG.M16*PREF.CURR) AS FCSG16, "
                StrSql = StrSql + "(FIXCOSTSUG.M17*PREF.CURR) AS FCSG17, "
                StrSql = StrSql + "(FIXCOSTSUG.M18*PREF.CURR) AS FCSG18, "
                StrSql = StrSql + "(FIXCOSTSUG.M19*PREF.CURR) AS FCSG19, "
                StrSql = StrSql + "(FIXCOSTSUG.M20*PREF.CURR) AS FCSG20, "
                StrSql = StrSql + "(FIXCOSTSUG.M21*PREF.CURR) AS FCSG21, "
                StrSql = StrSql + "(FIXCOSTSUG.M22*PREF.CURR) AS FCSG22, "
                StrSql = StrSql + "(FIXCOSTSUG.M23*PREF.CURR) AS FCSG23, "
                StrSql = StrSql + "(FIXCOSTSUG.M24*PREF.CURR) AS FCSG24, "
                StrSql = StrSql + "(FIXCOSTSUG.M25*PREF.CURR) AS FCSG25, "
                StrSql = StrSql + "(FIXCOSTSUG.M26*PREF.CURR) AS FCSG26, "
                StrSql = StrSql + "(FIXCOSTSUG.M27*PREF.CURR) AS FCSG27, "
                StrSql = StrSql + "(FIXCOSTSUG.M28*PREF.CURR) AS FCSG28, "
                StrSql = StrSql + "(FIXCOSTSUG.M29*PREF.CURR) AS FCSG29, "
                StrSql = StrSql + "(FIXCOSTSUG.M30*PREF.CURR) AS FCSG30, "
                StrSql = StrSql + "(FIXCOSTPRE.M1*PREF.CURR) AS FCPREF1, "
                StrSql = StrSql + "(FIXCOSTPRE.M2*PREF.CURR) AS FCPREF2, "
                StrSql = StrSql + "(FIXCOSTPRE.M3*PREF.CURR) AS FCPREF3, "
                StrSql = StrSql + "(FIXCOSTPRE.M4*PREF.CURR) AS FCPREF4, "
                StrSql = StrSql + "(FIXCOSTPRE.M5*PREF.CURR) AS FCPREF5, "
                StrSql = StrSql + "(FIXCOSTPRE.M6*PREF.CURR) AS FCPREF6, "
                StrSql = StrSql + "(FIXCOSTPRE.M7*PREF.CURR) AS FCPREF7, "
                StrSql = StrSql + "(FIXCOSTPRE.M8*PREF.CURR) AS FCPREF8, "
                StrSql = StrSql + "(FIXCOSTPRE.M9*PREF.CURR) AS FCPREF9, "
                StrSql = StrSql + "(FIXCOSTPRE.M10*PREF.CURR) AS FCPREF10, "
                StrSql = StrSql + "(FIXCOSTPRE.M11*PREF.CURR) AS FCPREF11, "
                StrSql = StrSql + "(FIXCOSTPRE.M12*PREF.CURR) AS FCPREF12, "
                StrSql = StrSql + "(FIXCOSTPRE.M13*PREF.CURR) AS FCPREF13, "
                StrSql = StrSql + "(FIXCOSTPRE.M14*PREF.CURR) AS FCPREF14, "
                StrSql = StrSql + "(FIXCOSTPRE.M15*PREF.CURR) AS FCPREF15, "
                StrSql = StrSql + "(FIXCOSTPRE.M16*PREF.CURR) AS FCPREF16, "
                StrSql = StrSql + "(FIXCOSTPRE.M17*PREF.CURR) AS FCPREF17, "
                StrSql = StrSql + "(FIXCOSTPRE.M18*PREF.CURR) AS FCPREF18, "
                StrSql = StrSql + "(FIXCOSTPRE.M19*PREF.CURR) AS FCPREF19, "
                StrSql = StrSql + "(FIXCOSTPRE.M20*PREF.CURR) AS FCPREF20, "
                StrSql = StrSql + "(FIXCOSTPRE.M21*PREF.CURR) AS FCPREF21, "
                StrSql = StrSql + "(FIXCOSTPRE.M22*PREF.CURR) AS FCPREF22, "
                StrSql = StrSql + "(FIXCOSTPRE.M23*PREF.CURR) AS FCPREF23, "
                StrSql = StrSql + "(FIXCOSTPRE.M24*PREF.CURR) AS FCPREF24, "
                StrSql = StrSql + "(FIXCOSTPRE.M25*PREF.CURR) AS FCPREF25, "
                StrSql = StrSql + "(FIXCOSTPRE.M26*PREF.CURR) AS FCPREF26, "
                StrSql = StrSql + "(FIXCOSTPRE.M27*PREF.CURR) AS FCPREF27, "
                StrSql = StrSql + "(FIXCOSTPRE.M28*PREF.CURR) AS FCPREF28, "
                StrSql = StrSql + "(FIXCOSTPRE.M29*PREF.CURR) AS FCPREF29, "
                StrSql = StrSql + "(FIXCOSTPRE.M30*PREF.CURR) AS FCPREF30, "
                StrSql = StrSql + "FCDEP.M1 as DEPID1, "
                StrSql = StrSql + "FCDEP.M2 as DEPID2, "
                StrSql = StrSql + "FCDEP.M3 as DEPID3, "
                StrSql = StrSql + "FCDEP.M4 as DEPID4, "
                StrSql = StrSql + "FCDEP.M5 as DEPID5, "
                StrSql = StrSql + "FCDEP.M6 as DEPID6, "
                StrSql = StrSql + "FCDEP.M7 as DEPID7, "
                StrSql = StrSql + "FCDEP.M8 as DEPID8, "
                StrSql = StrSql + "FCDEP.M9 as DEPID9, "
                StrSql = StrSql + "FCDEP.M10 as DEPID10, "
                StrSql = StrSql + "FCDEP.M11 as DEPID11, "
                StrSql = StrSql + "FCDEP.M12 as DEPID12, "
                StrSql = StrSql + "FCDEP.M13 as DEPID13, "
                StrSql = StrSql + "FCDEP.M14 as DEPID14, "
                StrSql = StrSql + "FCDEP.M15 as DEPID15, "
                StrSql = StrSql + "FCDEP.M16 as DEPID16, "
                StrSql = StrSql + "FCDEP.M17 as DEPID17, "
                StrSql = StrSql + "FCDEP.M18 as DEPID18, "
                StrSql = StrSql + "FCDEP.M19 as DEPID19, "
                StrSql = StrSql + "FCDEP.M20 as DEPID20, "
                StrSql = StrSql + "FCDEP.M21 as DEPID21, "
                StrSql = StrSql + "FCDEP.M22 as DEPID22, "
                StrSql = StrSql + "FCDEP.M23 as DEPID23, "
                StrSql = StrSql + "FCDEP.M24 as DEPID24, "
                StrSql = StrSql + "FCDEP.M25 as DEPID25, "
                StrSql = StrSql + "FCDEP.M26 as DEPID26, "
                StrSql = StrSql + "FCDEP.M27 as DEPID27, "
                StrSql = StrSql + "FCDEP.M28 as DEPID28, "
                StrSql = StrSql + "FCDEP.M29 as DEPID29, "
                StrSql = StrSql + "FCDEP.M30 as DEPID30, "
                StrSql = StrSql + "PREF.UNITS, "
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
                StrSql = StrSql + "(TOTAL.ASSETTOTAL*PREF.CURR) AS ASSETTOTAL, "
                StrSql = StrSql + "DEPC.YEARS AS DEPYEARS, "
                StrSql = StrSql + "(DEPC.DEPRECIATION*PREF.CURR) AS DEPANNUAL "
                StrSql = StrSql + "FROM FIXEDCOSTPCT FIXCOST "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=FIXCOST.CASEID "
                StrSql = StrSql + "INNER JOIN FIXEDCOSTSUG FIXCOSTSUG "
                StrSql = StrSql + "ON FIXCOSTSUG.CASEID=FIXCOST.CASEID "
                StrSql = StrSql + "INNER JOIN FIXEDCOSTPRE FIXCOSTPRE "
                StrSql = StrSql + "ON FIXCOSTPRE.CASEID=FIXCOST.CASEID "
                StrSql = StrSql + "INNER JOIN TOTAL "
                StrSql = StrSql + "ON TOTAL.CASEID=FIXCOST.CASEID "
                StrSql = StrSql + "INNER JOIN DEPRECIATION DEPC "
                StrSql = StrSql + "ON DEPC.CASEID=FIXCOST.CASEID "
                StrSql = StrSql + "INNER JOIN FIXEDCOSTDEP FCDEP "
                StrSql = StrSql + "ON FCDEP.CASEID=FIXCOST.CASEID "
                StrSql = StrSql + "WHERE FIXCOST.CASEID=" + CaseId.ToString() + ""

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetSupportDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCustomerINDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT CIN.CASEID,  "
                StrSql = StrSql + "PREF.TITLE2 AS PREFT2, "
                StrSql = StrSql + "PREF.TITLE8 AS PREFT8, "
                StrSql = StrSql + "(CIN.M1*PREF.CURR/PREF.CONVWT) AS PRODPUR, "
                StrSql = StrSql + "CIN.M2*PREF.CONVTHICK3 AS SHIPDIST, "
                StrSql = StrSql + "(CIN.M3*PREF.CURR/PREF.CONVTHICK3) AS MILCOST, "
                StrSql = StrSql + "PREF.TITLE4 AS PREFT4, "
                StrSql = StrSql + "PREF.UNITS "
                StrSql = StrSql + "FROM CUSTOMERIN CIN "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=CIN.CASEID "
                StrSql = StrSql + "WHERE CIN.CASEID=" + CaseId.ToString() + ""

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetSupportDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetEffiencyDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT MATIN.CASEID,  "
                StrSql = StrSql + "(MAT1.MATDE1 || ' ' || MAT1.MATDE2) AS MAT1, "
                StrSql = StrSql + "(MAT2.MATDE1 || ' ' || MAT2.MATDE2) AS MAT2, "
                StrSql = StrSql + "(MAT3.MATDE1 || ' ' || MAT3.MATDE2) AS MAT3, "
                StrSql = StrSql + "(MAT4.MATDE1 || ' ' || MAT4.MATDE2) AS MAT4, "
                StrSql = StrSql + "(MAT5.MATDE1 || ' ' || MAT5.MATDE2) AS MAT5, "
                StrSql = StrSql + "(MAT6.MATDE1 || ' ' || MAT6.MATDE2) AS MAT6, "
                StrSql = StrSql + "(MAT7.MATDE1 || ' ' || MAT7.MATDE2) AS MAT7, "
                StrSql = StrSql + "(MAT8.MATDE1 || ' ' || MAT8.MATDE2) AS MAT8, "
                StrSql = StrSql + "(MAT9.MATDE1 || ' ' || MAT9.MATDE2) AS MAT9, "
                StrSql = StrSql + "(MAT10.MATDE1 || ' ' || MAT10.MATDE2) AS MAT10, "
                StrSql = StrSql + "(PROC1.PROCDE1 || ' ' ||  PROC1.PROCDE2) AS PROC1, "
                StrSql = StrSql + "(PROC2.PROCDE1 || ' ' ||  PROC2.PROCDE2) AS PROC2, "
                StrSql = StrSql + "(PROC3.PROCDE1 || ' ' ||  PROC3.PROCDE2) AS PROC3, "
                StrSql = StrSql + "(PROC4.PROCDE1 || ' ' ||  PROC4.PROCDE2) AS PROC4, "
                StrSql = StrSql + "(PROC5.PROCDE1 || ' ' ||  PROC5.PROCDE2) AS PROC5, "
                StrSql = StrSql + "(PROC6.PROCDE1 || ' ' ||  PROC6.PROCDE2) AS PROC6, "
                StrSql = StrSql + "(PROC7.PROCDE1 || ' ' ||  PROC7.PROCDE2) AS PROC7, "
                StrSql = StrSql + "(PROC8.PROCDE1 || ' ' ||  PROC8.PROCDE2) AS PROC8, "
                StrSql = StrSql + "(PROC9.PROCDE1 || ' ' ||  PROC9.PROCDE2) AS PROC9, "
                StrSql = StrSql + "(PROC10.PROCDE1 || ' ' ||  PROC10.PROCDE2) AS PROC10, "
                StrSql = StrSql + "MATEF.T1 AS MATA1, "
                StrSql = StrSql + "MATEF.T2 AS MATA2, "
                StrSql = StrSql + "MATEF.T3 AS MATA3, "
                StrSql = StrSql + "MATEF.T4 AS MATA4, "
                StrSql = StrSql + "MATEF.T5 AS MATA5, "
                StrSql = StrSql + "MATEF.T6 AS MATA6, "
                StrSql = StrSql + "MATEF.T7 AS MATA7, "
                StrSql = StrSql + "MATEF.T8 AS MATA8, "
                StrSql = StrSql + "MATEF.T9 AS MATA9, "
                StrSql = StrSql + "MATEF.T10 AS MATA10, "
                StrSql = StrSql + "MATEF.S1 AS MATB1, "
                StrSql = StrSql + "MATEF.S2 AS MATB2, "
                StrSql = StrSql + "MATEF.S3 AS MATB3, "
                StrSql = StrSql + "MATEF.S4 AS MATB4, "
                StrSql = StrSql + "MATEF.S5 AS MATB5, "
                StrSql = StrSql + "MATEF.S6 AS MATB6, "
                StrSql = StrSql + "MATEF.S7 AS MATB7, "
                StrSql = StrSql + "MATEF.S8 AS MATB8, "
                StrSql = StrSql + "MATEF.S9 AS MATB9, "
                StrSql = StrSql + "MATEF.S10 AS MATB10, "
                StrSql = StrSql + "MATEF.Y1 AS MATC1, "
                StrSql = StrSql + "MATEF.Y2 AS MATC2, "
                StrSql = StrSql + "MATEF.Y3 AS MATC3, "
                StrSql = StrSql + "MATEF.Y4 AS MATC4, "
                StrSql = StrSql + "MATEF.Y5 AS MATC5, "
                StrSql = StrSql + "MATEF.Y6 AS MATC6, "
                StrSql = StrSql + "MATEF.Y7 AS MATC7, "
                StrSql = StrSql + "MATEF.Y8 AS MATC8, "
                StrSql = StrSql + "MATEF.Y9 AS MATC9, "
                StrSql = StrSql + "MATEF.Y10 AS MATC10, "
                StrSql = StrSql + "MATEF.D1 AS MATD1, "
                StrSql = StrSql + "MATEF.D2 AS MATD2, "
                StrSql = StrSql + "MATEF.D3 AS MATD3, "
                StrSql = StrSql + "MATEF.D4 AS MATD4, "
                StrSql = StrSql + "MATEF.D5 AS MATD5, "
                StrSql = StrSql + "MATEF.D6 AS MATD6, "
                StrSql = StrSql + "MATEF.D7 AS MATD7, "
                StrSql = StrSql + "MATEF.D8 AS MATD8, "
                StrSql = StrSql + "MATEF.D9 AS MATD9, "
                StrSql = StrSql + "MATEF.D10 AS MATD10, "
                StrSql = StrSql + "MATEF.E1 AS MATE1, "
                StrSql = StrSql + "MATEF.E2 AS MATE2, "
                StrSql = StrSql + "MATEF.E3 AS MATE3, "
                StrSql = StrSql + "MATEF.E4 AS MATE4, "
                StrSql = StrSql + "MATEF.E5 AS MATE5, "
                StrSql = StrSql + "MATEF.E6 AS MATE6, "
                StrSql = StrSql + "MATEF.E7 AS MATE7, "
                StrSql = StrSql + "MATEF.E8 AS MATE8, "
                StrSql = StrSql + "MATEF.E9 AS MATE9, "
                StrSql = StrSql + "MATEF.E10 AS MATE10, "
                StrSql = StrSql + "MATEF.Z1 AS MATF1, "
                StrSql = StrSql + "MATEF.Z2 AS MATF2, "
                StrSql = StrSql + "MATEF.Z3 AS MATF3, "
                StrSql = StrSql + "MATEF.Z4 AS MATF4, "
                StrSql = StrSql + "MATEF.Z5 AS MATF5, "
                StrSql = StrSql + "MATEF.Z6 AS MATF6, "
                StrSql = StrSql + "MATEF.Z7 AS MATF7, "
                StrSql = StrSql + "MATEF.Z8 AS MATF8, "
                StrSql = StrSql + "MATEF.Z9 AS MATF9, "
                StrSql = StrSql + "MATEF.Z10 AS MATF10, "
                StrSql = StrSql + "MATEF.B1 AS MATG1, "
                StrSql = StrSql + "MATEF.B2 AS MATG2, "
                StrSql = StrSql + "MATEF.B3 AS MATG3, "
                StrSql = StrSql + "MATEF.B4 AS MATG4, "
                StrSql = StrSql + "MATEF.B5 AS MATG5, "
                StrSql = StrSql + "MATEF.B6 AS MATG6, "
                StrSql = StrSql + "MATEF.B7 AS MATG7, "
                StrSql = StrSql + "MATEF.B8 AS MATG8, "
                StrSql = StrSql + "MATEF.B9 AS MATG9, "
                StrSql = StrSql + "MATEF.B10 AS MATG10, "
                StrSql = StrSql + "MATEF.R1 AS MATH1, "
                StrSql = StrSql + "MATEF.R2 AS MATH2, "
                StrSql = StrSql + "MATEF.R3 AS MATH3, "
                StrSql = StrSql + "MATEF.R4 AS MATH4, "
                StrSql = StrSql + "MATEF.R5 AS MATH5, "
                StrSql = StrSql + "MATEF.R6 AS MATH6, "
                StrSql = StrSql + "MATEF.R7 AS MATH7, "
                StrSql = StrSql + "MATEF.R8 AS MATH8, "
                StrSql = StrSql + "MATEF.R9 AS MATH9, "
                StrSql = StrSql + "MATEF.R10 AS MATH10, "
                StrSql = StrSql + "MATEF.K1 AS MATI1, "
                StrSql = StrSql + "MATEF.K2 AS MATI2, "
                StrSql = StrSql + "MATEF.K3 AS MATI3, "
                StrSql = StrSql + "MATEF.K4 AS MATI4, "
                StrSql = StrSql + "MATEF.K5 AS MATI5, "
                StrSql = StrSql + "MATEF.K6 AS MATI6, "
                StrSql = StrSql + "MATEF.K7 AS MATI7, "
                StrSql = StrSql + "MATEF.K8 AS MATI8, "
                StrSql = StrSql + "MATEF.K9 AS MATI9, "
                StrSql = StrSql + "MATEF.K10 AS MATI10, "
                StrSql = StrSql + "MATEF.P1 AS MATJ1, "
                StrSql = StrSql + "MATEF.P2 AS MATJ2, "
                StrSql = StrSql + "MATEF.P3 AS MATJ3, "
                StrSql = StrSql + "MATEF.P4 AS MATJ4, "
                StrSql = StrSql + "MATEF.P5 AS MATJ5, "
                StrSql = StrSql + "MATEF.P6 AS MATJ6, "
                StrSql = StrSql + "MATEF.P7 AS MATJ7, "
                StrSql = StrSql + "MATEF.P8 AS MATJ8, "
                StrSql = StrSql + "MATEF.P9 AS MATJ9, "
                StrSql = StrSql + "MATEF.P10 AS MATJ10 "
                StrSql = StrSql + "FROM MATERIALINPUT MATIN "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT1 "
                StrSql = StrSql + "ON MAT1.MATID=MATIN.M1 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT2 "
                StrSql = StrSql + "ON MAT2.MATID=MATIN.M2 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT3 "
                StrSql = StrSql + "ON MAT3.MATID=MATIN.M3 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT4 "
                StrSql = StrSql + "ON MAT4.MATID=MATIN.M4 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT5 "
                StrSql = StrSql + "ON MAT5.MATID=MATIN.M5 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT6 "
                StrSql = StrSql + "ON MAT6.MATID=MATIN.M6 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT7 "
                StrSql = StrSql + "ON MAT7.MATID=MATIN.M7 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT8 "
                StrSql = StrSql + "ON MAT8.MATID=MATIN.M8 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT9 "
                StrSql = StrSql + "ON MAT9.MATID=MATIN.M9 "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT10 "
                StrSql = StrSql + "ON MAT10.MATID=MATIN.M10 "
                StrSql = StrSql + "INNER JOIN PLANTCONFIG PLC "
                StrSql = StrSql + "ON PLC.CASEID=MATIN.CASEID "
                StrSql = StrSql + "INNER JOIN PROCESS PROC1 "
                StrSql = StrSql + "ON PROC1.PROCID=PLC.M1 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC2 "
                StrSql = StrSql + "ON PROC2.PROCID=PLC.M2 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC3 "
                StrSql = StrSql + "ON PROC3.PROCID=PLC.M3 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC4 "
                StrSql = StrSql + "ON PROC4.PROCID=PLC.M4 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC5 "
                StrSql = StrSql + "ON PROC5.PROCID=PLC.M5 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC6 "
                StrSql = StrSql + "ON PROC6.PROCID=PLC.M6 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC7 "
                StrSql = StrSql + "ON PROC7.PROCID=PLC.M7 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC8 "
                StrSql = StrSql + "ON PROC8.PROCID=PLC.M8 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC9 "
                StrSql = StrSql + "ON PROC9.PROCID=PLC.M9 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC10 "
                StrSql = StrSql + "ON PROC10.PROCID=PLC.M10 "
                StrSql = StrSql + "INNER JOIN MATERIALEFF MATEF "
                StrSql = StrSql + "ON MATEF.CASEID=MATIN.CASEID "
                StrSql = StrSql + "WHERE  MATIN.CASEID=" + CaseId.ToString() + ""

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetSupportDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPlantConfig2Details(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "Select PLS.CASEID,  "
                StrSql = StrSql + "'Production' AS PSPACE1, "
                StrSql = StrSql + "'High Bay' AS PSPACE2, "
                StrSql = StrSql + "'Partial High Bay' AS PSPACE3, "
                StrSql = StrSql + "'Standard' AS PSPACE4, "
                StrSql = StrSql + "'Production Total' AS PSPACE5, "
                StrSql = StrSql + "'Warehouse' AS PSPACE6, "
                StrSql = StrSql + "'Office' AS PSPACE7, "
                StrSql = StrSql + "'Support' AS PSPACE8, "

                StrSql = StrSql + "(PLS.M1* PREF.CONVAREA2) AS AR1, "
                StrSql = StrSql + "(PLS.M2* PREF.CONVAREA2) AS AR2, "
                StrSql = StrSql + "(PLS.M3* PREF.CONVAREA2) AS AR3, "
                StrSql = StrSql + "(PLS.M4* PREF.CONVAREA2) AS AR4, "
                StrSql = StrSql + "(PLS.M5* PREF.CONVAREA2) AS ARTOT, "
                StrSql = StrSql + "PREF.TITLE7 AS TITLE7, "
                StrSql = StrSql + "PREF.TITLE4 AS TITLE4, "
                StrSql = StrSql + "PREF.TITLE2, "
                StrSql = StrSql + "PREF.UNITS, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN (ART1.LEASERATE*PREF.CURR) ELSE (ART1.LEASERATE*PREF.CURR/PREF.CONVAREA2) END AS  SUG1, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN (ART2.LEASERATE*PREF.CURR) ELSE (ART2.LEASERATE*PREF.CURR/PREF.CONVAREA2) END AS  SUG2, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN (ART3.LEASERATE*PREF.CURR) ELSE (ART3.LEASERATE*PREF.CURR/PREF.CONVAREA2) END AS  SUG3, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN (ART4.LEASERATE*PREF.CURR) ELSE (ART4.LEASERATE*PREF.CURR/PREF.CONVAREA2) END AS  SUG4, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN (ART5.LEASERATE*PREF.CURR) ELSE (ART5.LEASERATE*PREF.CURR/PREF.CONVAREA2) END AS  SUG5, "
                StrSql = StrSql + "CASE PREF.UNITS  WHEN 0 THEN (ART6.LEASERATE*PREF.CURR) ELSE (ART6.LEASERATE*PREF.CURR/PREF.CONVAREA2) END AS  SUG6, "
                StrSql = StrSql + "CASE PREF.UNITS WHEN 0 THEN "
                StrSql = StrSql + "PLS.PRODUCTIONLEASEHB*PREF.CURR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "PLS.PRODUCTIONLEASEHB*PREF.CURR/PREF.CONVAREA2 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AS PREF1 , "
                StrSql = StrSql + "CASE PREF.UNITS WHEN 0 THEN "
                StrSql = StrSql + "PLS.PRODUCTIONLEASEPHB*PREF.CURR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "PLS.PRODUCTIONLEASEPHB*PREF.CURR/PREF.CONVAREA2 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AS PREF2 , "
                StrSql = StrSql + "CASE PREF.UNITS WHEN 0 THEN "
                StrSql = StrSql + "PLS.PRODUCTIONLEASE*PREF.CURR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "PLS.PRODUCTIONLEASE*PREF.CURR/PREF.CONVAREA2 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AS PREF3, "
                StrSql = StrSql + "CASE PREF.UNITS WHEN 0 THEN "
                StrSql = StrSql + "PLS.WAREHOUSELEASE*PREF.CURR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "PLS.WAREHOUSELEASE*PREF.CURR/PREF.CONVAREA2 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AS PREF4, "
                StrSql = StrSql + "CASE PREF.UNITS WHEN 0 THEN "
                StrSql = StrSql + "PLS.OFFICELEASE*PREF.CURR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "PLS.OFFICELEASE*PREF.CURR/PREF.CONVAREA2 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AS PREF5, "
                StrSql = StrSql + "CASE PREF.UNITS WHEN 0 THEN "
                StrSql = StrSql + "PLS.SUPPORTLEASE*PREF.CURR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "PLS.SUPPORTLEASE*PREF.CURR/PREF.CONVAREA2 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AS PREF6, "
                StrSql = StrSql + "(PLS.PHIGHBAY* PREF.CURR) AS PTOT1, "
                StrSql = StrSql + "(PLS.PPHIGHBAY* PREF.CURR) AS PTOT2, "
                StrSql = StrSql + "(PLS.PSTD* PREF.CURR) AS PTOT3, "
                StrSql = StrSql + "(PLS.M6* PREF.CURR) AS PTOT4, "
                StrSql = StrSql + "(PLS.M7* PREF.CURR) AS PTOT5, "
                StrSql = StrSql + "(PLS.M8* PREF.CURR) AS PTOT6, "
                StrSql = StrSql + "(PLS.M9* PREF.CURR) AS PTOT7, "
                StrSql = StrSql + "(PLS.M10* PREF.CURR) AS LEASETOTAL "
                StrSql = StrSql + "FROM PLANTSPACE PLS "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=PLS.CASEID "
                StrSql = StrSql + "INNER JOIN ECON.AREATYPE ART1 "
                StrSql = StrSql + "ON ART1.AREAID=1 "
                StrSql = StrSql + "INNER JOIN ECON.AREATYPE ART2 "
                StrSql = StrSql + "ON ART2.AREAID=2 "
                StrSql = StrSql + "INNER JOIN ECON.AREATYPE ART3 "
                StrSql = StrSql + "ON ART3.AREAID=3 "
                StrSql = StrSql + "INNER JOIN ECON.AREATYPE ART4 "
                StrSql = StrSql + "ON ART4.AREAID=4 "
                StrSql = StrSql + "INNER JOIN ECON.AREATYPE ART5 "
                StrSql = StrSql + "ON ART5.AREAID=5 "
                StrSql = StrSql + "INNER JOIN ECON.AREATYPE ART6 "
                StrSql = StrSql + "ON ART6.AREAID=6 "
                StrSql = StrSql + "WHERE  PLS.CASEID=" + CaseId.ToString() + ""

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetSupportDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetEnergyDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PREF.TITLE1,  "
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
                StrSql = StrSql + "PREF.UNITS , "
                StrSql = StrSql + "(B1.PRICE*PREF.CURR) ERGPSUG1, "
                StrSql = StrSql + "(B2.PRICE*PREF.CURR) ERGPSUG2, "
                StrSql = StrSql + "(PENG.ELECPRICE*PREF.CURR) ERGPPREF1, "
                StrSql = StrSql + "(PENG.NGASPRICE*PREF.CURR) ERGPPREF2, "
                StrSql = StrSql + "PENG.M1 AS ENRGE1, "
                StrSql = StrSql + "PENG.M2 AS ENRGE2, "
                StrSql = StrSql + "PENG.M3 AS ENRGE3, "
                StrSql = StrSql + "PENG.M4 AS ENRGE4, "
                StrSql = StrSql + "PENG.M5 AS ENRGE5, "
                StrSql = StrSql + "PENG.M6 AS ENRGN1, "
                StrSql = StrSql + "PENG.M7 AS ENRGN2, "
                StrSql = StrSql + "PENG.M8 AS ENRGN3, "
                StrSql = StrSql + "PENG.M9 AS ENRGN4, "
                StrSql = StrSql + "PENG.M10 AS ENRGN5, "
                StrSql = StrSql + "(PENG.D1*PREF.CURR) AS ENRGCE1, "
                StrSql = StrSql + "(PENG.D2*PREF.CURR) AS ENRGCE2, "
                StrSql = StrSql + "(PENG.D3*PREF.CURR) AS ENRGCE3, "
                StrSql = StrSql + "(PENG.D4*PREF.CURR) AS ENRGCE4, "
                StrSql = StrSql + "(PENG.D5*PREF.CURR) AS ENRGCE5, "
                StrSql = StrSql + "(PENG.D6*PREF.CURR) AS ENRGCG1, "
                StrSql = StrSql + "(PENG.D7*PREF.CURR) AS ENRGCG2, "
                StrSql = StrSql + "(PENG.D8*PREF.CURR) AS ENRGCG3, "
                StrSql = StrSql + "(PENG.D9*PREF.CURR) AS ENRGCG4, "
                StrSql = StrSql + "(PENG.D10*PREF.CURR) AS ENRGCG5, "
                StrSql = StrSql + "(PENG.D11*PREF.CURR) AS TOTAL1, "
                StrSql = StrSql + "(PENG.D12*PREF.CURR) AS TOTAL2, "
                StrSql = StrSql + "(PENG.D13*PREF.CURR) AS TOTAL3, "
                StrSql = StrSql + "(PENG.D14*PREF.CURR) AS TOTAL4, "
                StrSql = StrSql + "(PENG.D15*PREF.CURR) AS TOTAL5, "
                StrSql = StrSql + "(PENG.V1*PREF.CURR) AS VTOTAL1, "
                StrSql = StrSql + "(PENG.V2*PREF.CURR) AS VTOTAL2, "
                StrSql = StrSql + "(PENG.V3*PREF.CURR) AS VTOTAL3, "
                StrSql = StrSql + "(PENG.V4*PREF.CURR) AS VTOTAL4, "
                StrSql = StrSql + "(PENG.V5*PREF.CURR) AS VTOTAL5, "
                StrSql = StrSql + "(PENG.F1*PREF.CURR) AS FTOTAL1, "
                StrSql = StrSql + "(PENG.F2*PREF.CURR) AS FTOTAL2, "
                StrSql = StrSql + "(PENG.F3*PREF.CURR) AS FTOTAL3, "
                StrSql = StrSql + "(PENG.F4*PREF.CURR) AS FTOTAL4, "
                StrSql = StrSql + "(PENG.F5*PREF.CURR) AS FTOTAL5 "
                StrSql = StrSql + "FROM PREFERENCES PREF "
                StrSql = StrSql + "INNER JOIN PLANTENERGY PENG "
                StrSql = StrSql + "ON PENG.CASEID= PREF.CASEID "
                StrSql = StrSql + "INNER JOIN ECON.ENERGYARCH B1 "
                StrSql = StrSql + "ON B1.ENERGYID = 1 "
                StrSql = StrSql + "AND B1.EFFDATE =PENG.EFFDATE "
                StrSql = StrSql + "INNER JOIN ECON.ENERGYARCH B2 "
                StrSql = StrSql + "ON B2.ENERGYID=2 "
                StrSql = StrSql + "AND B2.EFFDATE =PENG.EFFDATE "
                StrSql = StrSql + "WHERE PREF.CASEID =" + CaseId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetAdditionalEnergyInfo(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PREF.CASEID,  "
                StrSql = StrSql + "'Production' AS SPACETYPE1, "
                StrSql = StrSql + "'Warehouse' AS SPACETYPE2, "
                StrSql = StrSql + "'Office' AS SPACETYPE3, "
                StrSql = StrSql + "'Support' AS SPACETYPE4, "
                StrSql = StrSql + "AT1.ELECPERAREA/PREF.CONVAREA2 AS ENERGYCONSUMAS1, "
                StrSql = StrSql + "AT2.ELECPERAREA/PREF.CONVAREA2 AS ENERGYCONSUMAS2, "
                StrSql = StrSql + "AT3.ELECPERAREA/PREF.CONVAREA2 AS ENERGYCONSUMAS3, "
                StrSql = StrSql + "SPACEENERGYPREFPERSQFT.P4/PREF.CONVAREA2 AS ENERGYCONSUMAP1, "
                StrSql = StrSql + "SPACEENERGYPREFPERSQFT.P5/PREF.CONVAREA2 AS ENERGYCONSUMAP2, "
                StrSql = StrSql + "SPACEENERGYPREFPERSQFT.P6/PREF.CONVAREA2 AS ENERGYCONSUMAP3, "
                StrSql = StrSql + "AT4.GASPERAREA/PREF.CONVAREA2 AS  ENERGYCONSUMBS1, "
                StrSql = StrSql + "AT1.GASPERAREA/PREF.CONVAREA2 AS  ENERGYCONSUMBS2, "
                StrSql = StrSql + "AT2.GASPERAREA/PREF.CONVAREA2 AS  ENERGYCONSUMBS3, "
                StrSql = StrSql + "AT3.GASPERAREA/PREF.CONVAREA2 AS  ENERGYCONSUMBS4, "
                StrSql = StrSql + "SPACEENERGYPREFPERSQFT.P7/PREF.CONVAREA2 AS ENERGYCONSUMBP1, "
                StrSql = StrSql + "SPACEENERGYPREFPERSQFT.P10/PREF.CONVAREA2 AS ENERGYCONSUMBP2, "
                StrSql = StrSql + "SPACEENERGYPREFPERSQFT.P11/PREF.CONVAREA2 AS ENERGYCONSUMBP3, "
                StrSql = StrSql + "SPACEENERGYPREFPERSQFT.P12/PREF.CONVAREA2 AS ENERGYCONSUMBP4, "
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
                StrSql = StrSql + "FROM PREFERENCES PREF "
                StrSql = StrSql + "INNER JOIN ECON.AREATYPE AT1 "
                StrSql = StrSql + "ON AT1.AREAID=4 "
                StrSql = StrSql + "INNER  JOIN ECON.AREATYPE AT2 "
                StrSql = StrSql + "ON AT2.AREAID=5 "
                StrSql = StrSql + "INNER  JOIN ECON.AREATYPE AT3 "
                StrSql = StrSql + "ON AT3.AREAID=6 "
                StrSql = StrSql + "INNER  JOIN ECON.AREATYPE AT4 "
                StrSql = StrSql + "ON AT4.AREAID=1 "
                StrSql = StrSql + "INNER JOIN SPACEENERGYPREFPERSQFT "
                StrSql = StrSql + "ON SPACEENERGYPREFPERSQFT.CASEID=PREF.CASEID "
                StrSql = StrSql + "WHERE PREF.CASEID="+ CaseId.ToString()
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetAdditionalEnergyInfo:" + ex.Message.ToString())
                Return Dts

            End Try
        End Function

        Public Function GetCaseViewer(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PROC1.PROCDE1 AS DEPT1,  "
                StrSql = StrSql + "PROC2.PROCDE1 AS DEPT2, "
                StrSql = StrSql + "PROC3.PROCDE1 AS DEPT3, "
                StrSql = StrSql + "PROC4.PROCDE1 AS DEPT4, "
                StrSql = StrSql + "PROC5.PROCDE1 AS DEPT5, "
                StrSql = StrSql + "PROC6.PROCDE1 AS DEPT6, "
                StrSql = StrSql + "PROC7.PROCDE1 AS DEPT7, "
                StrSql = StrSql + "PROC8.PROCDE1 AS DEPT8, "
                StrSql = StrSql + "PROC9.PROCDE1 AS DEPT9, "
                StrSql = StrSql + "PROC10.PROCDE1 AS DEPT10, "
                StrSql = StrSql + "PLANTCONFIG.M1, "
                StrSql = StrSql + "PLANTCONFIG.M2, "
                StrSql = StrSql + "PLANTCONFIG.M3, "
                StrSql = StrSql + "PLANTCONFIG.M4, "
                StrSql = StrSql + "PLANTCONFIG.M5, "
                StrSql = StrSql + "PLANTCONFIG.M6, "
                StrSql = StrSql + "PLANTCONFIG.M7, "
                StrSql = StrSql + "PLANTCONFIG.M8, "
                StrSql = StrSql + "PLANTCONFIG.M9, "
                StrSql = StrSql + "PLANTCONFIG.M10, "
                StrSql = StrSql + "(RESULTSPL.REVENUE*PREF.CURR)REVENUE, "
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
                StrSql = StrSql + "PREF.TITLE12 "
                StrSql = StrSql + "FROM PLANTCONFIG "
                StrSql = StrSql + "INNER JOIN RESULTSPL "
                StrSql = StrSql + "ON RESULTSPL.CASEID = PLANTCONFIG.CASEID "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = PLANTCONFIG.CASEID "
                StrSql = StrSql + "INNER JOIN PROCESS PROC1 "
                StrSql = StrSql + "ON PROC1.PROCID = PLANTCONFIG.M1 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC2 "
                StrSql = StrSql + "ON PROC2.PROCID = PLANTCONFIG.M2 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC3 "
                StrSql = StrSql + "ON PROC3.PROCID = PLANTCONFIG.M3 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC4 "
                StrSql = StrSql + "ON PROC4.PROCID = PLANTCONFIG.M4 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC5 "
                StrSql = StrSql + "ON PROC5.PROCID = PLANTCONFIG.M5 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC6 "
                StrSql = StrSql + "ON PROC6.PROCID = PLANTCONFIG.M6 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC7 "
                StrSql = StrSql + "ON PROC7.PROCID = PLANTCONFIG.M7 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC8 "
                StrSql = StrSql + "ON PROC8.PROCID = PLANTCONFIG.M8 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC9 "
                StrSql = StrSql + "ON PROC9.PROCID = PLANTCONFIG.M9 "
                StrSql = StrSql + "INNER JOIN PROCESS PROC10 "
                StrSql = StrSql + "ON PROC10.PROCID = PLANTCONFIG.M10 "
                StrSql = StrSql + "WHERE PLANTCONFIG.CASEID  = " + CaseId.ToString() + ""


                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

#End Region

#Region "Notes"
        Public Function GetAssumptionPageDetails(ByVal AssumptionType As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT ASSUMPTIONTYPEID, ASSUMPTIONTYPEDE1, ASSUMPTIONTYPEDE2, ASSUMPTIONTYPECODE  "
                StrSql = StrSql + "FROM ASSUMPTIONTYPES "
                StrSql = StrSql + "WHERE ASSUMPTIONTYPECODE ='" + AssumptionType + "'"
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetAssumptionPageDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPageNoteDetails(ByVal AssumptionType As String, ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID, USERID, ASSUMPTIONTYPE, NOTE FROM NOTES  "
                StrSql = StrSql + "WHERE CASEID = " + CaseId + " "
                StrSql = StrSql + "AND ASSUMPTIONTYPE ='" + AssumptionType + "' "

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetAssumptionPageDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCaseNoteDetails(ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "Select (Assumptiontypes.Assumptiontypede1||' '||Assumptiontypes.Assumptiontypede2)assumptiontypedes,  "
                StrSql = StrSql + "Notes.Note "
                StrSql = StrSql + "From Notes "
                StrSql = StrSql + "Inner Join Assumptiontypes "
                StrSql = StrSql + "On Assumptiontypes.Assumptiontypecode = Notes.Assumptiontype "
                StrSql = StrSql + "Where Caseid = " + CaseId + "  Order By Assumptiontypes.Assumptiontypeid"


                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetAssumptionPageDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Intermediate Results"
        Public Function GetExtrusionOutDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  (MAT1.MATDE1||' '||MAT1.MATDE2)MATS1,  "
                StrSql = StrSql + "(MAT2.MATDE1||' '||MAT2.MATDE2)MATS2, "
                StrSql = StrSql + "(MAT3.MATDE1||' '||MAT3.MATDE2)MATS3, "
                StrSql = StrSql + "(MAT4.MATDE1||' '||MAT4.MATDE2)MATS4, "
                StrSql = StrSql + "(MAT5.MATDE1||' '||MAT5.MATDE2)MATS5, "
                StrSql = StrSql + "(MAT6.MATDE1||' '||MAT6.MATDE2)MATS6, "
                StrSql = StrSql + "(MAT7.MATDE1||' '||MAT7.MATDE2)MATS7, "
                StrSql = StrSql + "(MAT8.MATDE1||' '||MAT8.MATDE2)MATS8, "
                StrSql = StrSql + "(MAT9.MATDE1||' '||MAT9.MATDE2)MATS9, "
                StrSql = StrSql + "(MAT10.MATDE1||' '||MAT10.MATDE2)MATS10, "
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
                StrSql = StrSql + "(CASE WHEN  MAT.SG1 <> 0 THEN "
                StrSql = StrSql + "MAT.SG1 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "MAT1.SG "
                StrSql = StrSql + "END) AS SG1, "
                StrSql = StrSql + "(CASE WHEN  MAT.SG2 <> 0 THEN "
                StrSql = StrSql + "MAT.SG2 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "MAT2.SG "
                StrSql = StrSql + "END) AS SG2, "
                StrSql = StrSql + "(CASE WHEN  MAT.SG3 <> 0 THEN "
                StrSql = StrSql + "MAT.SG3 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "MAT3.SG "
                StrSql = StrSql + "END) AS SG3, "
                StrSql = StrSql + "(CASE WHEN  MAT.SG4 <> 0 THEN "
                StrSql = StrSql + "MAT.SG4 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "MAT4.SG "
                StrSql = StrSql + "END) AS SG4, "
                StrSql = StrSql + "(CASE WHEN  MAT.SG5 <> 0 THEN "
                StrSql = StrSql + "MAT.SG5 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "MAT5.SG "
                StrSql = StrSql + "END) AS SG5, "
                StrSql = StrSql + "(CASE WHEN  MAT.SG6 <> 0 THEN "
                StrSql = StrSql + "MAT.SG6 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "MAT6.SG "
                StrSql = StrSql + "END) AS SG6, "
                StrSql = StrSql + "(CASE WHEN  MAT.SG7 <> 0 THEN "
                StrSql = StrSql + "MAT.SG7 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "MAT7.SG "
                StrSql = StrSql + "END) AS SG7, "
                StrSql = StrSql + "(CASE WHEN  MAT.SG8 <> 0 THEN "
                StrSql = StrSql + "MAT.SG8 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "MAT8.SG "
                StrSql = StrSql + "END) AS SG8, "
                StrSql = StrSql + "(CASE WHEN  MAT.SG9 <> 0 THEN "
                StrSql = StrSql + "MAT.SG9 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "MAT9.SG "
                StrSql = StrSql + "END) AS SG9, "
                StrSql = StrSql + "(CASE WHEN  MAT.SG10 <> 0 THEN "
                StrSql = StrSql + "MAT.SG10 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "MAT10.SG "
                StrSql = StrSql + "END) AS SG10, "
                StrSql = StrSql + "TOT.SG AS SG11, "
                StrSql = StrSql + "(MATOUT.M1*PREF.CONVWT/PREF.CONVAREA)W1, "
                StrSql = StrSql + "(MATOUT.M2*PREF.CONVWT/PREF.CONVAREA)W2, "
                StrSql = StrSql + "(MATOUT.M3*PREF.CONVWT/PREF.CONVAREA)W3, "
                StrSql = StrSql + "(MATOUT.M4*PREF.CONVWT/PREF.CONVAREA)W4, "
                StrSql = StrSql + "(MATOUT.M5*PREF.CONVWT/PREF.CONVAREA)W5, "
                StrSql = StrSql + "(MATOUT.M6*PREF.CONVWT/PREF.CONVAREA)W6, "
                StrSql = StrSql + "(MATOUT.M7*PREF.CONVWT/PREF.CONVAREA)W7, "
                StrSql = StrSql + "(MATOUT.M8*PREF.CONVWT/PREF.CONVAREA)W8, "
                StrSql = StrSql + "(MATOUT.M9*PREF.CONVWT/PREF.CONVAREA)W9, "
                StrSql = StrSql + "(MATOUT.M10*PREF.CONVWT/PREF.CONVAREA)W10, "
                StrSql = StrSql + "(TOT.WTPERAREA*PREF.CONVWT/PREF.CONVAREA)W11, "
                StrSql = StrSql + "MATOUT.P1, "
                StrSql = StrSql + "MATOUT.P2, "
                StrSql = StrSql + "MATOUT.P3, "
                StrSql = StrSql + "MATOUT.P4, "
                StrSql = StrSql + "MATOUT.P5, "
                StrSql = StrSql + "MATOUT.P6, "
                StrSql = StrSql + "MATOUT.P7, "
                StrSql = StrSql + "MATOUT.P8, "
                StrSql = StrSql + "MATOUT.P9, "
                StrSql = StrSql + "MATOUT.P10, "
                StrSql = StrSql + "(MATOUT.P1+MATOUT.P2+MATOUT.P3+MATOUT.P4+MATOUT.P5+MATOUT.P6+MATOUT.P7+MATOUT.P8+MATOUT.P9+MATOUT.P10)P11, "
                StrSql = StrSql + "(MATOUT.PUR1*PREF.CONVWT) PUR1, "
                StrSql = StrSql + "(MATOUT.PUR2*PREF.CONVWT) PUR2, "
                StrSql = StrSql + "(MATOUT.PUR3*PREF.CONVWT) PUR3, "
                StrSql = StrSql + "(MATOUT.PUR4*PREF.CONVWT) PUR4, "
                StrSql = StrSql + "(MATOUT.PUR5*PREF.CONVWT) PUR5, "
                StrSql = StrSql + "(MATOUT.PUR6*PREF.CONVWT) PUR6, "
                StrSql = StrSql + "(MATOUT.PUR7*PREF.CONVWT) PUR7, "
                StrSql = StrSql + "(MATOUT.PUR8*PREF.CONVWT) PUR8, "
                StrSql = StrSql + "(MATOUT.PUR9*PREF.CONVWT) PUR9, "
                StrSql = StrSql + "(MATOUT.PUR10*PREF.CONVWT) PUR10, "
                StrSql = StrSql + "((MATOUT.PUR1+MATOUT.PUR2+MATOUT.PUR3+MATOUT.PUR4+MATOUT.PUR5+MATOUT.PUR6+MATOUT.PUR7+MATOUT.PUR8+MATOUT.PUR9+MATOUT.PUR10)*PREF.CONVWT)PUR11, "
                StrSql = StrSql + "(MATOUT.PURZ1*PREF.CURR) PURZ1, "
                StrSql = StrSql + "(MATOUT.PURZ2*PREF.CURR) PURZ2, "
                StrSql = StrSql + "(MATOUT.PURZ3*PREF.CURR) PURZ3, "
                StrSql = StrSql + "(MATOUT.PURZ4*PREF.CURR) PURZ4, "
                StrSql = StrSql + "(MATOUT.PURZ5*PREF.CURR) PURZ5, "
                StrSql = StrSql + "(MATOUT.PURZ6*PREF.CURR) PURZ6, "
                StrSql = StrSql + "(MATOUT.PURZ7*PREF.CURR) PURZ7, "
                StrSql = StrSql + "(MATOUT.PURZ8*PREF.CURR) PURZ8, "
                StrSql = StrSql + "(MATOUT.PURZ9*PREF.CURR) PURZ9, "
                StrSql = StrSql + "(MATOUT.PURZ10*PREF.CURR) PURZ10, "
                StrSql = StrSql + "((MATOUT.PURZ1+MATOUT.PURZ2+MATOUT.PURZ3+MATOUT.PURZ4+MATOUT.PURZ5+MATOUT.PURZ6+MATOUT.PURZ7+MATOUT.PURZ8+MATOUT.PURZ9+MATOUT.PURZ10)*PREF.CURR)PURZ11, "
                StrSql = StrSql + "((CASE  WHEN RSPL.FINVOLMSI > 1 THEN "
                StrSql = StrSql + "MATOUT.PURZ1/RSPL.FINVOLMSI*100*PREF.CURR/PREF.CONVAREA "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN RSPL.FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "MATOUT.PURZ1/RSPL.FINVOLMUNITS*100*PREF.CURR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END))PUN1, "
                StrSql = StrSql + "((CASE  WHEN RSPL.FINVOLMSI > 1 THEN "
                StrSql = StrSql + "MATOUT.PURZ2/RSPL.FINVOLMSI*100*PREF.CURR/PREF.CONVAREA "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN RSPL.FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "MATOUT.PURZ2/RSPL.FINVOLMUNITS*100*PREF.CURR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END))PUN2, "
                StrSql = StrSql + "((CASE  WHEN RSPL.FINVOLMSI > 1 THEN "
                StrSql = StrSql + "MATOUT.PURZ3/RSPL.FINVOLMSI*100*PREF.CURR/PREF.CONVAREA "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN RSPL.FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "MATOUT.PURZ3/RSPL.FINVOLMUNITS*100*PREF.CURR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END))PUN3, "
                StrSql = StrSql + "((CASE  WHEN RSPL.FINVOLMSI > 1 THEN "
                StrSql = StrSql + "MATOUT.PURZ4/RSPL.FINVOLMSI*100*PREF.CURR/PREF.CONVAREA "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN RSPL.FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "MATOUT.PURZ4/RSPL.FINVOLMUNITS*100*PREF.CURR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END))PUN4, "
                StrSql = StrSql + "((CASE  WHEN RSPL.FINVOLMSI > 1 THEN "
                StrSql = StrSql + "MATOUT.PURZ5/RSPL.FINVOLMSI*100*PREF.CURR/PREF.CONVAREA "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN RSPL.FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "MATOUT.PURZ5/RSPL.FINVOLMUNITS*100*PREF.CURR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END))PUN5, "
                StrSql = StrSql + "((CASE  WHEN RSPL.FINVOLMSI > 1 THEN "
                StrSql = StrSql + "MATOUT.PURZ6/RSPL.FINVOLMSI*100*PREF.CURR/PREF.CONVAREA "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN RSPL.FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "MATOUT.PURZ6/RSPL.FINVOLMUNITS*100*PREF.CURR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END))PUN6, "
                StrSql = StrSql + "((CASE  WHEN RSPL.FINVOLMSI > 1 THEN "
                StrSql = StrSql + "MATOUT.PURZ7/RSPL.FINVOLMSI*100*PREF.CURR/PREF.CONVAREA "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN RSPL.FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "MATOUT.PURZ7/RSPL.FINVOLMUNITS*100*PREF.CURR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END))PUN7, "
                StrSql = StrSql + "((CASE  WHEN RSPL.FINVOLMSI > 1 THEN "
                StrSql = StrSql + "MATOUT.PURZ8/RSPL.FINVOLMSI*100*PREF.CURR/PREF.CONVAREA "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN RSPL.FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "MATOUT.PURZ8/RSPL.FINVOLMUNITS*100*PREF.CURR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END))PUN8, "
                StrSql = StrSql + "((CASE  WHEN RSPL.FINVOLMSI > 1 THEN "
                StrSql = StrSql + "MATOUT.PURZ9/RSPL.FINVOLMSI*100*PREF.CURR/PREF.CONVAREA "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN RSPL.FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "MATOUT.PURZ9/RSPL.FINVOLMUNITS*100*PREF.CURR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END))PUN9, "
                StrSql = StrSql + "((CASE  WHEN RSPL.FINVOLMSI > 1 THEN "
                StrSql = StrSql + "MATOUT.PURZ10/RSPL.FINVOLMSI*100*PREF.CURR/PREF.CONVAREA "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN RSPL.FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "MATOUT.PURZ10/RSPL.FINVOLMUNITS*100*PREF.CURR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END))PUN10, "
                StrSql = StrSql + "((CASE  WHEN RSPL.FINVOLMSI > 1 THEN "
                StrSql = StrSql + "(MATOUT.PURZ1+MATOUT.PURZ2+MATOUT.PURZ3+MATOUT.PURZ4+MATOUT.PURZ5+MATOUT.PURZ6+MATOUT.PURZ7+MATOUT.PURZ8+MATOUT.PURZ9+MATOUT.PURZ10)/RSPL.FINVOLMSI*100*PREF.CURR/PREF.CONVAREA "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN RSPL.FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "(MATOUT.PURZ1+MATOUT.PURZ2+MATOUT.PURZ3+MATOUT.PURZ4+MATOUT.PURZ5+MATOUT.PURZ6+MATOUT.PURZ7+MATOUT.PURZ8+MATOUT.PURZ9+MATOUT.PURZ10)/RSPL.FINVOLMUNITS*100*PREF.CURR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END))PUN11, "
                StrSql = StrSql + "(CASE  WHEN RSPL.FINVOLMSI > 1 THEN "
                StrSql = StrSql + "PREF.TITLE6||' per '||	PREF.TITLE3 "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN RSPL.FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "PREF.TITLE6||' per unit' "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END)PUN, "
                StrSql = StrSql + "TO_CHAR(MAT.EFFDATE,'MON DD,YYYY') AS EFFDATE, "
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
                StrSql = StrSql + "FROM MATERIALINPUT MAT "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=MAT.CASEID "
                StrSql = StrSql + "INNER JOIN MATERIALOUTPUT MATOUT "
                StrSql = StrSql + "ON MATOUT.CASEID=MAT.CASEID "
                StrSql = StrSql + "INNER JOIN RESULTSPL RSPL "
                StrSql = StrSql + "ON RSPL.CASEID = MAT.CASEID "
                StrSql = StrSql + "INNER JOIN TOTAL TOT "
                StrSql = StrSql + "ON TOT.CASEID=MAT.CASEID "
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
                StrSql = StrSql + "WHERE MAT.CASEID = " + CaseId.ToString() + ""



                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetPalletInDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetOpreationsOutDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  (DEPT1.PROCDE1||' '||DEPT1.PROCDE2)DEPTS1,  "
                StrSql = StrSql + "(DEPT2.PROCDE1||' '||DEPT2.PROCDE2)DEPTS2, "
                StrSql = StrSql + "(DEPT3.PROCDE1||' '||DEPT3.PROCDE2)DEPTS3, "
                StrSql = StrSql + "(DEPT4.PROCDE1||' '||DEPT4.PROCDE2)DEPTS4, "
                StrSql = StrSql + "(DEPT5.PROCDE1||' '||DEPT5.PROCDE2)DEPTS5, "
                StrSql = StrSql + "(DEPT6.PROCDE1||' '||DEPT6.PROCDE2)DEPTS6, "
                StrSql = StrSql + "(DEPT7.PROCDE1||' '||DEPT7.PROCDE2)DEPTS7, "
                StrSql = StrSql + "(DEPT8.PROCDE1||' '||DEPT8.PROCDE2)DEPTS8, "
                StrSql = StrSql + "(DEPT9.PROCDE1||' '||DEPT9.PROCDE2)DEPTS9, "
                StrSql = StrSql + "(DEPT10.PROCDE1||' '||DEPT10.PROCDE2)DEPTS10, "
                StrSql = StrSql + "(OPDEP.M1*PREF.CONVWT) AS PV1, "
                StrSql = StrSql + "(OPDEP.M2*PREF.CONVWT) AS PV2, "
                StrSql = StrSql + "(OPDEP.M3*PREF.CONVWT) AS PV3, "
                StrSql = StrSql + "(OPDEP.M4*PREF.CONVWT) AS PV4, "
                StrSql = StrSql + "(OPDEP.M5*PREF.CONVWT) AS PV5, "
                StrSql = StrSql + "(OPDEP.M6*PREF.CONVWT) AS PV6, "
                StrSql = StrSql + "(OPDEP.M7*PREF.CONVWT) AS PV7, "
                StrSql = StrSql + "(OPDEP.M8*PREF.CONVWT) AS PV8, "
                StrSql = StrSql + "(OPDEP.M9*PREF.CONVWT) AS PV9, "
                StrSql = StrSql + "(OPDEP.M10*PREF.CONVWT) AS PV10, "
                StrSql = StrSql + "(OPDEP.T1*PREF.CONVWT) AS FEQV1, "
                StrSql = StrSql + "(OPDEP.T2*PREF.CONVWT) AS FEQV2, "
                StrSql = StrSql + "(OPDEP.T3*PREF.CONVWT) AS FEQV3, "
                StrSql = StrSql + "(OPDEP.T4*PREF.CONVWT) AS FEQV4, "
                StrSql = StrSql + "(OPDEP.T5*PREF.CONVWT) AS FEQV5, "
                StrSql = StrSql + "(OPDEP.T6*PREF.CONVWT) AS FEQV6, "
                StrSql = StrSql + "(OPDEP.T7*PREF.CONVWT) AS FEQV7, "
                StrSql = StrSql + "(OPDEP.T8*PREF.CONVWT) AS FEQV8, "
                StrSql = StrSql + "(OPDEP.T9*PREF.CONVWT) AS FEQV9, "
                StrSql = StrSql + "(OPDEP.T10*PREF.CONVWT) AS FEQV10, "
                StrSql = StrSql + "(OPDEP.G1*PREF.CONVWT) AS FV1, "
                StrSql = StrSql + "(OPDEP.G2*PREF.CONVWT) AS FV2, "
                StrSql = StrSql + "(OPDEP.G3*PREF.CONVWT) AS FV3, "
                StrSql = StrSql + "(OPDEP.G4*PREF.CONVWT) AS FV4, "
                StrSql = StrSql + "(OPDEP.G5*PREF.CONVWT) AS FV5, "
                StrSql = StrSql + "(OPDEP.G6*PREF.CONVWT) AS FV6, "
                StrSql = StrSql + "(OPDEP.G7*PREF.CONVWT) AS FV7, "
                StrSql = StrSql + "(OPDEP.G8*PREF.CONVWT) AS FV8, "
                StrSql = StrSql + "(OPDEP.G9*PREF.CONVWT) AS FV9, "
                StrSql = StrSql + "(OPDEP.G10*PREF.CONVWT) AS FV10, "
                StrSql = StrSql + "(OPDEP.D1) AS AD1, "
                StrSql = StrSql + "(OPDEP.D2) AS AD2, "
                StrSql = StrSql + "(OPDEP.D3) AS AD3, "
                StrSql = StrSql + "(OPDEP.D4) AS AD4, "
                StrSql = StrSql + "(OPDEP.D5) AS AD5, "
                StrSql = StrSql + "(OPDEP.D6) AS AD6, "
                StrSql = StrSql + "(OPDEP.D7) AS AD7, "
                StrSql = StrSql + "(OPDEP.D8) AS AD8, "
                StrSql = StrSql + "(OPDEP.D9) AS AD9, "
                StrSql = StrSql + "(OPDEP.D10) AS AD10, "
                StrSql = StrSql + "(OPDEP.W1) AS AW1, "
                StrSql = StrSql + "(OPDEP.W2) AS AW2, "
                StrSql = StrSql + "(OPDEP.W3) AS AW3, "
                StrSql = StrSql + "(OPDEP.W4) AS AW4, "
                StrSql = StrSql + "(OPDEP.W5) AS AW5, "
                StrSql = StrSql + "(OPDEP.W6) AS AW6, "
                StrSql = StrSql + "(OPDEP.W7) AS AW7, "
                StrSql = StrSql + "(OPDEP.W8) AS AW8, "
                StrSql = StrSql + "(OPDEP.W9) AS AW9, "
                StrSql = StrSql + "(OPDEP.W10) AS AW10, "
                StrSql = StrSql + "(RESULTSPL.VOLUMEDIS*PREF.CONVWT) VOLUMEDIS, "
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
                StrSql = StrSql + "PREF.TITLE12 ,"
                StrSql = StrSql + "PREF.UNITS "
                StrSql = StrSql + "FROM PLANTCONFIG DEPT "
                StrSql = StrSql + "INNER JOIN RESULTSPL "
                StrSql = StrSql + "ON RESULTSPL.CASEID = DEPT.CASEID "
                StrSql = StrSql + "INNER JOIN OPDEPVOL OPDEP "
                StrSql = StrSql + "ON OPDEP.CASEID = DEPT.CASEID "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=DEPT.CASEID "
                StrSql = StrSql + "INNER JOIN PROCESS DEPT1 "
                StrSql = StrSql + "ON DEPT1.PROCID = DEPT.M1 "
                StrSql = StrSql + "INNER JOIN PROCESS DEPT2 "
                StrSql = StrSql + "ON DEPT2.PROCID = DEPT.M2 "
                StrSql = StrSql + "INNER JOIN PROCESS DEPT3 "
                StrSql = StrSql + "ON DEPT3.PROCID = DEPT.M3 "
                StrSql = StrSql + "INNER JOIN PROCESS DEPT4 "
                StrSql = StrSql + "ON DEPT4.PROCID = DEPT.M4 "
                StrSql = StrSql + "INNER JOIN PROCESS DEPT5 "
                StrSql = StrSql + "ON DEPT5.PROCID = DEPT.M5 "
                StrSql = StrSql + "INNER JOIN PROCESS DEPT6 "
                StrSql = StrSql + "ON DEPT6.PROCID = DEPT.M6 "
                StrSql = StrSql + "INNER JOIN PROCESS DEPT7 "
                StrSql = StrSql + "ON DEPT7.PROCID = DEPT.M7 "
                StrSql = StrSql + "INNER JOIN PROCESS DEPT8 "
                StrSql = StrSql + "ON DEPT8.PROCID = DEPT.M8 "
                StrSql = StrSql + "INNER JOIN PROCESS DEPT9 "
                StrSql = StrSql + "ON DEPT9.PROCID = DEPT.M9 "
                StrSql = StrSql + "INNER JOIN PROCESS DEPT10 "
                StrSql = StrSql + "ON DEPT10.PROCID = DEPT.M10 "
                StrSql = StrSql + "WHERE DEPT.CASEID = " + CaseId.ToString() + ""


                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetPalletInDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPersonnelOutDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                ''StrSql = "SELECT   (POS1.PERSDE1 ||' '||  POS1.PERSDE2) AS PERSDES1,  "
                ''StrSql = StrSql + "(POS2.PERSDE1 ||' '||  POS2.PERSDE2) AS PERSDES2, "
                ''StrSql = StrSql + "(POS3.PERSDE1 ||' '||  POS3.PERSDE2) AS PERSDES3, "
                ''StrSql = StrSql + "(POS4.PERSDE1 ||' '||  POS4.PERSDE2) AS PERSDES4, "
                ''StrSql = StrSql + "(POS5.PERSDE1 ||' '||  POS5.PERSDE2) AS PERSDES5, "
                ''StrSql = StrSql + "(POS6.PERSDE1 ||' '||  POS6.PERSDE2) AS PERSDES6, "
                ''StrSql = StrSql + "(POS7.PERSDE1 ||' '||  POS7.PERSDE2) AS PERSDES7, "
                ''StrSql = StrSql + "(POS8.PERSDE1 ||' '||  POS8.PERSDE2) AS PERSDES8, "
                ''StrSql = StrSql + "(POS9.PERSDE1 ||' '||  POS9.PERSDE2) AS PERSDES9, "
                ''StrSql = StrSql + "(POS10.PERSDE1 ||' '||  POS10.PERSDE2) AS PERSDES10, "
                ''StrSql = StrSql + "(POS11.PERSDE1 ||' '||  POS11.PERSDE2) AS PERSDES11, "
                ''StrSql = StrSql + "(POS12.PERSDE1 ||' '||  POS12.PERSDE2) AS PERSDES12, "
                ''StrSql = StrSql + "(POS13.PERSDE1 ||' '||  POS13.PERSDE2) AS PERSDES13, "
                ''StrSql = StrSql + "(POS14.PERSDE1 ||' '||  POS14.PERSDE2) AS PERSDES14, "
                ''StrSql = StrSql + "(POS15.PERSDE1 ||' '||  POS15.PERSDE2) AS PERSDES15, "
                ''StrSql = StrSql + "(POS16.PERSDE1 ||' '||  POS16.PERSDE2) AS PERSDES16, "
                ''StrSql = StrSql + "(POS17.PERSDE1 ||' '||  POS17.PERSDE2) AS PERSDES17, "
                ''StrSql = StrSql + "(POS18.PERSDE1 ||' '||  POS18.PERSDE2) AS PERSDES18, "
                ''StrSql = StrSql + "(POS19.PERSDE1 ||' '||  POS19.PERSDE2) AS PERSDES19, "
                ''StrSql = StrSql + "(POS20.PERSDE1 ||' '||  POS20.PERSDE2) AS PERSDES20, "
                ''StrSql = StrSql + "(POS21.PERSDE1 ||' '||  POS21.PERSDE2) AS PERSDES21, "
                ''StrSql = StrSql + "(POS22.PERSDE1 ||' '||  POS22.PERSDE2) AS PERSDES22, "
                ''StrSql = StrSql + "(POS23.PERSDE1 ||' '||  POS23.PERSDE2) AS PERSDES23, "
                ''StrSql = StrSql + "(POS24.PERSDE1 ||' '||  POS24.PERSDE2) AS PERSDES24, "
                ''StrSql = StrSql + "(POS25.PERSDE1 ||' '||  POS25.PERSDE2) AS PERSDES25, "
                ''StrSql = StrSql + "(POS26.PERSDE1 ||' '||  POS26.PERSDE2) AS PERSDES26, "
                ''StrSql = StrSql + "(POS27.PERSDE1 ||' '||  POS27.PERSDE2) AS PERSDES27, "
                ''StrSql = StrSql + "(POS28.PERSDE1 ||' '||  POS28.PERSDE2) AS PERSDES28, "
                ''StrSql = StrSql + "(POS29.PERSDE1 ||' '||  POS29.PERSDE2) AS PERSDES29, "
                ''StrSql = StrSql + "(POS30.PERSDE1 ||' '||  POS30.PERSDE2) AS PERSDES30, "
                ''StrSql = StrSql + "PNUM.M1  AS N1, "
                ''StrSql = StrSql + "PNUM.M2  AS N2, "
                ''StrSql = StrSql + "PNUM.M3  AS N3, "
                ''StrSql = StrSql + "PNUM.M4  AS N4, "
                ''StrSql = StrSql + "PNUM.M5  AS N5, "
                ''StrSql = StrSql + "PNUM.M6  AS N6, "
                ''StrSql = StrSql + "PNUM.M7  AS N7, "
                ''StrSql = StrSql + "PNUM.M8  AS N8, "
                ''StrSql = StrSql + "PNUM.M9  AS N9, "
                ''StrSql = StrSql + "PNUM.M10  AS N10, "
                ''StrSql = StrSql + "PNUM.M11  AS N11, "
                ''StrSql = StrSql + "PNUM.M12  AS N12, "
                ''StrSql = StrSql + "PNUM.M13  AS N13, "
                ''StrSql = StrSql + "PNUM.M14  AS N14, "
                ''StrSql = StrSql + "PNUM.M15  AS N15, "
                ''StrSql = StrSql + "PNUM.M16  AS N16, "
                ''StrSql = StrSql + "PNUM.M17  AS N17, "
                ''StrSql = StrSql + "PNUM.M18  AS N18, "
                ''StrSql = StrSql + "PNUM.M19  AS N19, "
                ''StrSql = StrSql + "PNUM.M20  AS N20, "
                ''StrSql = StrSql + "PNUM.M21  AS N21, "
                ''StrSql = StrSql + "PNUM.M22  AS N22, "
                ''StrSql = StrSql + "PNUM.M23  AS N23, "
                ''StrSql = StrSql + "PNUM.M24  AS N24, "
                ''StrSql = StrSql + "PNUM.M25  AS N25, "
                ''StrSql = StrSql + "PNUM.M26  AS N26, "
                ''StrSql = StrSql + "PNUM.M27  AS N27, "
                ''StrSql = StrSql + "PNUM.M28  AS N28, "
                ''StrSql = StrSql + "PNUM.M29  AS N29, "
                ''StrSql = StrSql + "PNUM.M30  AS N30, "
                ''StrSql = StrSql + "(PNUM.M1+PNUM.M2+PNUM.M3+PNUM.M4+PNUM.M5+PNUM.M6+PNUM.M7+PNUM.M8+PNUM.M9+PNUM.M10+PNUM.M11+PNUM.M12+PNUM.M13+PNUM.M14+PNUM.M15+PNUM.M16+PNUM.M17+PNUM.M18+PNUM.M19+PNUM.M20+PNUM.M21+PNUM.M22+PNUM.M23+PNUM.M24+PNUM.M25+PNUM.M26+PNUM.M27+PNUM.M28+PNUM.M29+PNUM.M30)N31, "
                ''StrSql = StrSql + "(PAY.M1*PREF.CURR) AS P1, "
                ''StrSql = StrSql + "(PAY.M2*PREF.CURR) AS P2, "
                ''StrSql = StrSql + "(PAY.M3*PREF.CURR) AS P3, "
                ''StrSql = StrSql + "(PAY.M4*PREF.CURR) AS P4, "
                ''StrSql = StrSql + "(PAY.M5*PREF.CURR) AS P5, "
                ''StrSql = StrSql + "(PAY.M6*PREF.CURR) AS P6, "
                ''StrSql = StrSql + "(PAY.M7*PREF.CURR) AS P7, "
                ''StrSql = StrSql + "(PAY.M8*PREF.CURR) AS P8, "
                ''StrSql = StrSql + "(PAY.M9*PREF.CURR) AS P9, "
                ''StrSql = StrSql + "(PAY.M10*PREF.CURR) AS P10, "
                ''StrSql = StrSql + "(PAY.M11*PREF.CURR) AS P11, "
                ''StrSql = StrSql + "(PAY.M12*PREF.CURR) AS P12, "
                ''StrSql = StrSql + "(PAY.M13*PREF.CURR) AS P13, "
                ''StrSql = StrSql + "(PAY.M14*PREF.CURR) AS P14, "
                ''StrSql = StrSql + "(PAY.M15*PREF.CURR) AS P15, "
                ''StrSql = StrSql + "(PAY.M16*PREF.CURR) AS P16, "
                ''StrSql = StrSql + "(PAY.M17*PREF.CURR) AS P17, "
                ''StrSql = StrSql + "(PAY.M18*PREF.CURR) AS P18, "
                ''StrSql = StrSql + "(PAY.M19*PREF.CURR) AS P19, "
                ''StrSql = StrSql + "(PAY.M20*PREF.CURR) AS P20, "
                ''StrSql = StrSql + "(PAY.M21*PREF.CURR) AS P21, "
                ''StrSql = StrSql + "(PAY.M22*PREF.CURR) AS P22, "
                ''StrSql = StrSql + "(PAY.M23*PREF.CURR) AS P23, "
                ''StrSql = StrSql + "(PAY.M24*PREF.CURR) AS P24, "
                ''StrSql = StrSql + "(PAY.M25*PREF.CURR) AS P25, "
                ''StrSql = StrSql + "(PAY.M26*PREF.CURR) AS P26, "
                ''StrSql = StrSql + "(PAY.M27*PREF.CURR) AS P27, "
                ''StrSql = StrSql + "(PAY.M28*PREF.CURR) AS P28, "
                ''StrSql = StrSql + "(PAY.M29*PREF.CURR) AS P29, "
                ''StrSql = StrSql + "(PAY.M30*PREF.CURR) AS P30, "
                ''StrSql = StrSql + "((PAY.M1+PAY.M2+PAY.M3+PAY.M4+PAY.M5+PAY.M6+PAY.M7+PAY.M8+PAY.M9+PAY.M10+PAY.M11+PAY.M12+PAY.M13+PAY.M14+PAY.M15+PAY.M16+PAY.M17+PAY.M18+PAY.M19+PAY.M20+PAY.M21+PAY.M22+PAY.M23+PAY.M24+PAY.M25+PAY.M26+PAY.M27+PAY.M28+PAY.M29+PAY.M30)*PREF.CURR)P31, "
                ''StrSql = StrSql + "PREF.TITLE1, "
                ''StrSql = StrSql + "PREF.TITLE3, "
                ''StrSql = StrSql + "PREF.TITLE2, "
                ''StrSql = StrSql + "PREF.TITLE4, "
                ''StrSql = StrSql + "PREF.TITLE5, "
                ''StrSql = StrSql + "PREF.TITLE6, "
                ''StrSql = StrSql + "PREF.TITLE7, "
                ''StrSql = StrSql + "PREF.TITLE8, "
                ''StrSql = StrSql + "PREF.TITLE9, "
                ''StrSql = StrSql + "PREF.TITLE10, "
                ''StrSql = StrSql + "PREF.TITLE11, "
                ''StrSql = StrSql + "PREF.TITLE12, "
                ''StrSql = StrSql + "PREF.UNITS "
                ''StrSql = StrSql + "FROM PERSONNELPOS POS "
                ''StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                ''StrSql = StrSql + "ON PREF.CASEID = POS.CASEID "
                ''StrSql = StrSql + "INNER JOIN PERSONNELNUM PNUM "
                ''StrSql = StrSql + "ON PNUM.CASEID = POS.CASEID "
                ''StrSql = StrSql + "INNER JOIN PersonnelPAY PAY "
                ''StrSql = StrSql + "ON PAY.CASEID = POS.CASEID "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS1 "
                ''StrSql = StrSql + "ON POS1.PERSID=POS.M1 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS2 "
                ''StrSql = StrSql + "ON POS2.PERSID=POS.M2 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS3 "
                ''StrSql = StrSql + "ON POS3.PERSID=POS.M3 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS4 "
                ''StrSql = StrSql + "ON POS4.PERSID=POS.M4 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS5 "
                ''StrSql = StrSql + "ON POS5.PERSID=POS.M5 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS6 "
                ''StrSql = StrSql + "ON POS6.PERSID=POS.M6 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS7 "
                ''StrSql = StrSql + "ON POS7.PERSID=POS.M7 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS8 "
                ''StrSql = StrSql + "ON POS8.PERSID=POS.M8 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS9 "
                ''StrSql = StrSql + "ON POS9.PERSID=POS.M9 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS10 "
                ''StrSql = StrSql + "ON POS10.PERSID=POS.M10 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS11 "
                ''StrSql = StrSql + "ON POS11.PERSID=POS.M11 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS12 "
                ''StrSql = StrSql + "ON POS12.PERSID=POS.M12 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS13 "
                ''StrSql = StrSql + "ON POS13.PERSID=POS.M13 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS14 "
                ''StrSql = StrSql + "ON POS14.PERSID=POS.M14 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS15 "
                ''StrSql = StrSql + "ON POS15.PERSID=POS.M15 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS16 "
                ''StrSql = StrSql + "ON POS16.PERSID=POS.M16 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS17 "
                ''StrSql = StrSql + "ON POS17.PERSID=POS.M17 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS18 "
                ''StrSql = StrSql + "ON POS18.PERSID=POS.M18 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS19 "
                ''StrSql = StrSql + "ON POS19.PERSID=POS.M19 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS20 "
                ''StrSql = StrSql + "ON POS20.PERSID=POS.M20 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS21 "
                ''StrSql = StrSql + "ON POS21.PERSID=POS.M21 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS22 "
                ''StrSql = StrSql + "ON POS22.PERSID=POS.M22 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS23 "
                ''StrSql = StrSql + "ON POS23.PERSID=POS.M23 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS24 "
                ''StrSql = StrSql + "ON POS24.PERSID=POS.M24 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS25 "
                ''StrSql = StrSql + "ON POS25.PERSID=POS.M25 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS26 "
                ''StrSql = StrSql + "ON POS26.PERSID=POS.M26 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS27 "
                ''StrSql = StrSql + "ON POS27.PERSID=POS.M27 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS28 "
                ''StrSql = StrSql + "ON POS28.PERSID=POS.M28 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS29 "
                ''StrSql = StrSql + "ON POS29.PERSID=POS.M29 "
                ''StrSql = StrSql + "INNER JOIN PERSONNEL  POS30 "
                ''StrSql = StrSql + "ON POS30.PERSID=POS.M30 "
                ''StrSql = StrSql + "WHERE POS.CASEID =" + CaseId.ToString() + ""
                ''Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)

                StrSql = "SELECT  "
                StrSql = StrSql + "PERSDES1,PERSDES2,PERSDES3,PERSDES4,PERSDES5,PERSDES6,PERSDES7,PERSDES8,PERSDES9,PERSDES10, "
                StrSql = StrSql + "PERSDES11,PERSDES12,PERSDES13,PERSDES14,PERSDES15,PERSDES16,PERSDES17,PERSDES18,PERSDES19,PERSDES20, "
                StrSql = StrSql + "PERSDES21,PERSDES22,PERSDES23,PERSDES24,PERSDES25,PERSDES26,PERSDES27,PERSDES28,PERSDES29,PERSDES30, "
                StrSql = StrSql + "N1,N2,N3,N4,N5,N6,N7,N8,N9,N10, "
                StrSql = StrSql + "N11,N12,N13,N14,N15,N16,N17,N18,N19,N20, "
                StrSql = StrSql + "N21,N22,N23,N24,N25,N26,N27,N28,N29,N30, "
                StrSql = StrSql + "(N1+N2+N3+N4+N5+N6+N7+N8+N9+N10+N11+N12+N13+N14+N15+N16+N17+N18+N19+N20+N21+N22+N23+N24+N25+N26+N27+N28+N29+N30)N31, "
                StrSql = StrSql + "P1,P2,P3,P4,P5,P6,P7,P8,P9,P10, "
                StrSql = StrSql + "P11,P12,P13,P14,P15,P16,P17,P18,P19,P20, "
                StrSql = StrSql + "P21,P22,P23,P24,P25,P26,P27,P28,P29,P30,P31, "
                StrSql = StrSql + "TITLE1,TITLE3,TITLE2,TITLE4,TITLE5, "
                StrSql = StrSql + "TITLE6,TITLE7,TITLE8,TITLE9,TITLE10, "
                StrSql = StrSql + "TITLE11,TITLE12,UNITS "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "(POS1.PERSDE1 ||' '||  POS1.PERSDE2) AS PERSDES1, "
                StrSql = StrSql + "(POS2.PERSDE1 ||' '||  POS2.PERSDE2) AS PERSDES2, "
                StrSql = StrSql + "(POS3.PERSDE1 ||' '||  POS3.PERSDE2) AS PERSDES3, "
                StrSql = StrSql + "(POS4.PERSDE1 ||' '||  POS4.PERSDE2) AS PERSDES4, "
                StrSql = StrSql + "(POS5.PERSDE1 ||' '||  POS5.PERSDE2) AS PERSDES5, "
                StrSql = StrSql + "(POS6.PERSDE1 ||' '||  POS6.PERSDE2) AS PERSDES6, "
                StrSql = StrSql + "(POS7.PERSDE1 ||' '||  POS7.PERSDE2) AS PERSDES7, "
                StrSql = StrSql + "(POS8.PERSDE1 ||' '||  POS8.PERSDE2) AS PERSDES8, "
                StrSql = StrSql + "(POS9.PERSDE1 ||' '||  POS9.PERSDE2) AS PERSDES9, "
                StrSql = StrSql + "(POS10.PERSDE1 ||' '||  POS10.PERSDE2) AS PERSDES10, "
                StrSql = StrSql + "(POS11.PERSDE1 ||' '||  POS11.PERSDE2) AS PERSDES11, "
                StrSql = StrSql + "(POS12.PERSDE1 ||' '||  POS12.PERSDE2) AS PERSDES12, "
                StrSql = StrSql + "(POS13.PERSDE1 ||' '||  POS13.PERSDE2) AS PERSDES13, "
                StrSql = StrSql + "(POS14.PERSDE1 ||' '||  POS14.PERSDE2) AS PERSDES14, "
                StrSql = StrSql + "(POS15.PERSDE1 ||' '||  POS15.PERSDE2) AS PERSDES15, "
                StrSql = StrSql + "(POS16.PERSDE1 ||' '||  POS16.PERSDE2) AS PERSDES16, "
                StrSql = StrSql + "(POS17.PERSDE1 ||' '||  POS17.PERSDE2) AS PERSDES17, "
                StrSql = StrSql + "(POS18.PERSDE1 ||' '||  POS18.PERSDE2) AS PERSDES18, "
                StrSql = StrSql + "(POS19.PERSDE1 ||' '||  POS19.PERSDE2) AS PERSDES19, "
                StrSql = StrSql + "(POS20.PERSDE1 ||' '||  POS20.PERSDE2) AS PERSDES20, "
                StrSql = StrSql + "(POS21.PERSDE1 ||' '||  POS21.PERSDE2) AS PERSDES21, "
                StrSql = StrSql + "(POS22.PERSDE1 ||' '||  POS22.PERSDE2) AS PERSDES22, "
                StrSql = StrSql + "(POS23.PERSDE1 ||' '||  POS23.PERSDE2) AS PERSDES23, "
                StrSql = StrSql + "(POS24.PERSDE1 ||' '||  POS24.PERSDE2) AS PERSDES24, "
                StrSql = StrSql + "(POS25.PERSDE1 ||' '||  POS25.PERSDE2) AS PERSDES25, "
                StrSql = StrSql + "(POS26.PERSDE1 ||' '||  POS26.PERSDE2) AS PERSDES26, "
                StrSql = StrSql + "(POS27.PERSDE1 ||' '||  POS27.PERSDE2) AS PERSDES27, "
                StrSql = StrSql + "(POS28.PERSDE1 ||' '||  POS28.PERSDE2) AS PERSDES28, "
                StrSql = StrSql + "(POS29.PERSDE1 ||' '||  POS29.PERSDE2) AS PERSDES29, "
                StrSql = StrSql + "(POS30.PERSDE1 ||' '||  POS30.PERSDE2) AS PERSDES30, "
                StrSql = StrSql + "CASE WHEN POS.M1>0 THEN PNUM.M1 ELSE 0 END AS N1, "
                StrSql = StrSql + "CASE WHEN POS.M2>0 THEN PNUM.M2 ELSE 0 END AS N2, "
                StrSql = StrSql + "CASE WHEN POS.M3>0 THEN PNUM.M3 ELSE 0 END AS N3, "
                StrSql = StrSql + "CASE WHEN POS.M4>0 THEN PNUM.M4 ELSE 0 END AS N4, "
                StrSql = StrSql + "CASE WHEN POS.M5>0 THEN PNUM.M5 ELSE 0 END AS N5, "
                StrSql = StrSql + "CASE WHEN POS.M6>0 THEN PNUM.M6 ELSE 0 END AS N6, "
                StrSql = StrSql + "CASE WHEN POS.M7>0 THEN PNUM.M7 ELSE 0 END AS N7, "
                StrSql = StrSql + "CASE WHEN POS.M8>0 THEN PNUM.M8 ELSE 0 END AS N8, "
                StrSql = StrSql + "CASE WHEN POS.M9>0 THEN PNUM.M9 ELSE 0 END AS N9, "
                StrSql = StrSql + "CASE WHEN POS.M10>0 THEN PNUM.M10 ELSE 0 END AS N10, "
                StrSql = StrSql + "CASE WHEN POS.M11>0 THEN PNUM.M11 ELSE 0 END AS N11, "
                StrSql = StrSql + "CASE WHEN POS.M12>0 THEN PNUM.M12 ELSE 0 END AS N12, "
                StrSql = StrSql + "CASE WHEN POS.M13>0 THEN PNUM.M13 ELSE 0 END AS N13, "
                StrSql = StrSql + "CASE WHEN POS.M14>0 THEN PNUM.M14 ELSE 0 END AS N14, "
                StrSql = StrSql + "CASE WHEN POS.M15>0 THEN PNUM.M15 ELSE 0 END AS N15, "
                StrSql = StrSql + "CASE WHEN POS.M16>0 THEN PNUM.M16 ELSE 0 END AS N16, "
                StrSql = StrSql + "CASE WHEN POS.M17>0 THEN PNUM.M17 ELSE 0 END AS N17, "
                StrSql = StrSql + "CASE WHEN POS.M18>0 THEN PNUM.M18 ELSE 0 END AS N18, "
                StrSql = StrSql + "CASE WHEN POS.M19>0 THEN PNUM.M19 ELSE 0 END AS N19, "
                StrSql = StrSql + "CASE WHEN POS.M20>0 THEN PNUM.M20 ELSE 0 END AS N20, "
                StrSql = StrSql + "CASE WHEN POS.M21>0 THEN PNUM.M21 ELSE 0 END AS N21, "
                StrSql = StrSql + "CASE WHEN POS.M22>0 THEN PNUM.M22 ELSE 0 END AS N22, "
                StrSql = StrSql + "CASE WHEN POS.M23>0 THEN PNUM.M23 ELSE 0 END AS N23, "
                StrSql = StrSql + "CASE WHEN POS.M24>0 THEN PNUM.M24 ELSE 0 END AS N24, "
                StrSql = StrSql + "CASE WHEN POS.M25>0 THEN PNUM.M25 ELSE 0 END AS N25, "
                StrSql = StrSql + "CASE WHEN POS.M26>0 THEN PNUM.M26 ELSE 0 END AS N26, "
                StrSql = StrSql + "CASE WHEN POS.M27>0 THEN PNUM.M27 ELSE 0 END AS N27, "
                StrSql = StrSql + "CASE WHEN POS.M28>0 THEN PNUM.M28 ELSE 0 END AS N28, "
                StrSql = StrSql + "CASE WHEN POS.M29>0 THEN PNUM.M29 ELSE 0 END AS N29, "
                StrSql = StrSql + "CASE WHEN POS.M30>0 THEN PNUM.M30 ELSE 0 END AS N30, "
                StrSql = StrSql + "(PAY.M1*PREF.CURR) AS P1, "
                StrSql = StrSql + "(PAY.M2*PREF.CURR) AS P2, "
                StrSql = StrSql + "(PAY.M3*PREF.CURR) AS P3, "
                StrSql = StrSql + "(PAY.M4*PREF.CURR) AS P4, "
                StrSql = StrSql + "(PAY.M5*PREF.CURR) AS P5, "
                StrSql = StrSql + "(PAY.M6*PREF.CURR) AS P6, "
                StrSql = StrSql + "(PAY.M7*PREF.CURR) AS P7, "
                StrSql = StrSql + "(PAY.M8*PREF.CURR) AS P8, "
                StrSql = StrSql + "(PAY.M9*PREF.CURR) AS P9, "
                StrSql = StrSql + "(PAY.M10*PREF.CURR) AS P10, "
                StrSql = StrSql + "(PAY.M11*PREF.CURR) AS P11, "
                StrSql = StrSql + "(PAY.M12*PREF.CURR) AS P12, "
                StrSql = StrSql + "(PAY.M13*PREF.CURR) AS P13, "
                StrSql = StrSql + "(PAY.M14*PREF.CURR) AS P14, "
                StrSql = StrSql + "(PAY.M15*PREF.CURR) AS P15, "
                StrSql = StrSql + "(PAY.M16*PREF.CURR) AS P16, "
                StrSql = StrSql + "(PAY.M17*PREF.CURR) AS P17, "
                StrSql = StrSql + "(PAY.M18*PREF.CURR) AS P18, "
                StrSql = StrSql + "(PAY.M19*PREF.CURR) AS P19, "
                StrSql = StrSql + "(PAY.M20*PREF.CURR) AS P20, "
                StrSql = StrSql + "(PAY.M21*PREF.CURR) AS P21, "
                StrSql = StrSql + "(PAY.M22*PREF.CURR) AS P22, "
                StrSql = StrSql + "(PAY.M23*PREF.CURR) AS P23, "
                StrSql = StrSql + "(PAY.M24*PREF.CURR) AS P24, "
                StrSql = StrSql + "(PAY.M25*PREF.CURR) AS P25, "
                StrSql = StrSql + "(PAY.M26*PREF.CURR) AS P26, "
                StrSql = StrSql + "(PAY.M27*PREF.CURR) AS P27, "
                StrSql = StrSql + "(PAY.M28*PREF.CURR) AS P28, "
                StrSql = StrSql + "(PAY.M29*PREF.CURR) AS P29, "
                StrSql = StrSql + "(PAY.M30*PREF.CURR) AS P30, "
                StrSql = StrSql + "((PAY.M1+PAY.M2+PAY.M3+PAY.M4+PAY.M5+PAY.M6+PAY.M7+PAY.M8+PAY.M9+PAY.M10+PAY.M11+PAY.M12+PAY.M13+PAY.M14+PAY.M15+PAY.M16+PAY.M17+PAY.M18+PAY.M19+PAY.M20+PAY.M21+PAY.M22+PAY.M23+PAY.M24+PAY.M25+PAY.M26+PAY.M27+PAY.M28+PAY.M29+PAY.M30)*PREF.CURR)P31, "
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
                StrSql = StrSql + "FROM PERSONNELPOS POS "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = POS.CASEID "
                StrSql = StrSql + "INNER JOIN PERSONNELNUM PNUM "
                StrSql = StrSql + "ON PNUM.CASEID = POS.CASEID "
                StrSql = StrSql + "INNER JOIN PersonnelPAY PAY "
                StrSql = StrSql + "ON PAY.CASEID = POS.CASEID "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS1 "
                StrSql = StrSql + "ON POS1.PERSID=POS.M1 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS2 "
                StrSql = StrSql + "ON POS2.PERSID=POS.M2 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS3 "
                StrSql = StrSql + "ON POS3.PERSID=POS.M3 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS4 "
                StrSql = StrSql + "ON POS4.PERSID=POS.M4 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS5 "
                StrSql = StrSql + "ON POS5.PERSID=POS.M5 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS6 "
                StrSql = StrSql + "ON POS6.PERSID=POS.M6 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS7 "
                StrSql = StrSql + "ON POS7.PERSID=POS.M7 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS8 "
                StrSql = StrSql + "ON POS8.PERSID=POS.M8 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS9 "
                StrSql = StrSql + "ON POS9.PERSID=POS.M9 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS10 "
                StrSql = StrSql + "ON POS10.PERSID=POS.M10 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS11 "
                StrSql = StrSql + "ON POS11.PERSID=POS.M11 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS12 "
                StrSql = StrSql + "ON POS12.PERSID=POS.M12 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS13 "
                StrSql = StrSql + "ON POS13.PERSID=POS.M13 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS14 "
                StrSql = StrSql + "ON POS14.PERSID=POS.M14 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS15 "
                StrSql = StrSql + "ON POS15.PERSID=POS.M15 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS16 "
                StrSql = StrSql + "ON POS16.PERSID=POS.M16 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS17 "
                StrSql = StrSql + "ON POS17.PERSID=POS.M17 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS18 "
                StrSql = StrSql + "ON POS18.PERSID=POS.M18 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS19 "
                StrSql = StrSql + "ON POS19.PERSID=POS.M19 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS20 "
                StrSql = StrSql + "ON POS20.PERSID=POS.M20 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS21 "
                StrSql = StrSql + "ON POS21.PERSID=POS.M21 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS22 "
                StrSql = StrSql + "ON POS22.PERSID=POS.M22 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS23 "
                StrSql = StrSql + "ON POS23.PERSID=POS.M23 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS24 "
                StrSql = StrSql + "ON POS24.PERSID=POS.M24 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS25 "
                StrSql = StrSql + "ON POS25.PERSID=POS.M25 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS26 "
                StrSql = StrSql + "ON POS26.PERSID=POS.M26 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS27 "
                StrSql = StrSql + "ON POS27.PERSID=POS.M27 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS28 "
                StrSql = StrSql + "ON POS28.PERSID=POS.M28 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS29 "
                StrSql = StrSql + "ON POS29.PERSID=POS.M29 "
                StrSql = StrSql + "INNER JOIN ECON.PERSONNEL  POS30 "
                StrSql = StrSql + "ON POS30.PERSID=POS.M30 "
                StrSql = StrSql + "WHERE POS.CASEID =" + CaseId.ToString() + " )  DUAL"
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetPalletInDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Results"
        Public Function GetProfitAndLossDetails(ByVal CaseId As String, ByVal Isdep As Boolean) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim Pm As String = String.Empty
            Try

                If Isdep Then
                    Pm = "PMDEP"
                Else
                    Pm = "PM"
                End If



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
                StrSql = StrSql + "DEP.DEPRECIATION AS PL24, "
                StrSql = StrSql + "" + Pm + " AS PL25, "
                StrSql = StrSql + "FINVOLMSI,"
                StrSql = StrSql + "FINVOLMUNITS,"
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
                StrSql = StrSql + "(CUST.M1*PREF.CURR/PREF.CONVWT) AS UNITPS, "
                StrSql = StrSql + "(UNITPRICE*PREF.CURR/PREF.CONVWT) AS UNITPP, "
                StrSql = StrSql + "UNITTYPE, "
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
                StrSql = StrSql + "RESULTSPL.CASEID, "
                StrSql = StrSql + " CASE WHEN CUSSALESUNIT=0 THEN RESULTSPL.CUSSALESVOLUME*PREF.CONVWT "
                StrSql = StrSql + " ELSE (CASE  WHEN FINVOLMSI > 1 THEN (RESULTSPL.CUSSALESVOLUME*PREF.CONVAREA) ELSE CASE WHEN FINVOLMUNITS > 1 THEN RESULTSPL.CUSSALESVOLUME END END)"
                StrSql = StrSql + " END AS CUSSALESVOLUME, "
                StrSql = StrSql + " CUSSALESUNIT, "
                StrSql = StrSql + " CUSSALESVOLUME CUSSALESVOLUME1 "

                StrSql = StrSql + "FROM RESULTSPL "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=RESULTSPL.CASEID "
                StrSql = StrSql + "INNER JOIN CUSTOMERIN CUST "
                StrSql = StrSql + "ON CUST.CASEID = RESULTSPL.CASEID "
                StrSql = StrSql + "INNER JOIN DEPRECIATION DEP "
                StrSql = StrSql + "ON DEP.CASEID=RESULTSPL.CASEID "
                StrSql = StrSql + "WHERE RESULTSPL.CASEID IN (" + CaseId.ToString() + ") "




                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetPalletInDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCostDetails(ByVal CaseId As String, ByVal Isdep As Boolean) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim fCost As String = String.Empty
            Dim tCost As String = String.Empty
            Try

                If Isdep Then
                    fCost = "FIXEDCOSTDEP"
                    tCost = "TOTALCOSTDEP"
                Else
                    fCost = "FIXEDCOST"
                    tCost = "TOTALCOST"
                End If



                StrSql = "SELECT  "
                StrSql = StrSql + "'Material Cost' AS PDES1, "
                StrSql = StrSql + "'Labor Cost' AS PDES2, "
                StrSql = StrSql + "'Energy Cost' AS PDES3, "
                StrSql = StrSql + "'Distribution Packaging Cost' AS PDES4, "
                StrSql = StrSql + "'Shipping Cost' AS PDES5, "
                StrSql = StrSql + "'Total Variable Cost' AS PDES6, "
                StrSql = StrSql + "'Office Supplies Cost' AS PDES7, "
                StrSql = StrSql + "'Labor Cost' AS PDES8, "
                StrSql = StrSql + "'Energy Cost' AS PDES9, "
                StrSql = StrSql + "'Lease Cost' AS PDES10, "
                StrSql = StrSql + "'Insurance Cost' AS PDES11, "
                StrSql = StrSql + "'Utilities Cost' AS PDES12, "
                StrSql = StrSql + "'Communications Cost' AS PDES13, "
                StrSql = StrSql + "'Travel Cost' AS PDES14, "
                StrSql = StrSql + "'Maintenance Supplies Cost' AS PDES15, "
                StrSql = StrSql + "'Minor Equipment Cost' AS PDES16, "
                StrSql = StrSql + "'Outside Services Cost' AS PDES17, "
                StrSql = StrSql + "'Professional Services Cost'  AS PDES18, "
                StrSql = StrSql + "'Laboratory Supplies Cost' AS PDES19, "
                StrSql = StrSql + "'Ink Supplies Cost' AS PDES20, "
                StrSql = StrSql + "'Plate Supplies Cost' AS PDES21, "
                StrSql = StrSql + "'Metal Supplies Cost' AS PDES22, "
                StrSql = StrSql + "'Depreciation' AS PDES23, "
                StrSql = StrSql + "'Total Fixed Cost' AS PDES24, "
                StrSql = StrSql + "'Total Cost' AS PDES25, "
                StrSql = StrSql + "VMATERIAL AS PL1, "
                StrSql = StrSql + "VLABOR AS PL2, "
                StrSql = StrSql + "VENERGY AS PL3, "
                StrSql = StrSql + "VPACK AS PL4, "
                StrSql = StrSql + "VSHIP AS PL5, "
                StrSql = StrSql + "VARIABLECOST AS PL6, "
                StrSql = StrSql + "OFFICESUPPLIES AS PL7, "
                StrSql = StrSql + "PLABOR AS PL8, "
                StrSql = StrSql + "PENERGY AS PL9, "
                StrSql = StrSql + "LEASECOST AS PL10, "
                StrSql = StrSql + "INSURANCE AS PL11, "
                StrSql = StrSql + "UTILITIES AS PL12, "
                StrSql = StrSql + "COMMUN AS PL13, "
                StrSql = StrSql + "TRAVEL AS PL14, "
                StrSql = StrSql + "MAINT AS PL15, "
                StrSql = StrSql + "MINOR AS PL16, "
                StrSql = StrSql + "OUT  AS PL17, "
                StrSql = StrSql + "PROF AS PL18, "
                StrSql = StrSql + "LAB AS PL19, "
                StrSql = StrSql + "INKSUP AS PL20, "
                StrSql = StrSql + "PLATESUP AS PL21, "
                StrSql = StrSql + "METSUP AS PL22, "
                StrSql = StrSql + "DEP.DEPRECIATION AS PL23, "
                StrSql = StrSql + "" + fCost + " AS PL24, "
                StrSql = StrSql + "" + tCost + " AS PL25, "
                StrSql = StrSql + "FINVOLMSI,"
                StrSql = StrSql + "FINVOLMUNITS,"
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
                StrSql = StrSql + "NVL((CASE  WHEN FINVOLMSI > 1 THEN "
                StrSql = StrSql + "((100*PREF.CURR/PREF.CONVAREA)/FINVOLMSI)"
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "((100*PREF.CURR)/FINVOLMUNITS) "
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
                StrSql = StrSql + "RESULTSPL.CASEID, "
                StrSql = StrSql + " CASE WHEN CUSSALESUNIT=0 THEN RESULTSPL.CUSSALESVOLUME*PREF.CONVWT "
                StrSql = StrSql + " ELSE (CASE  WHEN FINVOLMSI > 1 THEN (RESULTSPL.CUSSALESVOLUME*PREF.CONVAREA) ELSE CASE WHEN FINVOLMUNITS > 1 THEN RESULTSPL.CUSSALESVOLUME END END)"
                StrSql = StrSql + " END AS CUSSALESVOLUME, "
                StrSql = StrSql + " CUSSALESVOLUME CUSSALESVOLUME1, "
                StrSql = StrSql + " CUSSALESUNIT "
                StrSql = StrSql + "FROM RESULTSPL "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=RESULTSPL.CASEID "
                StrSql = StrSql + "INNER JOIN DEPRECIATION DEP "
                StrSql = StrSql + "ON DEP.CASEID=RESULTSPL.CASEID "
                StrSql = StrSql + "WHERE RESULTSPL.CASEID IN (" + CaseId.ToString() + ")"




                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetPalletInDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Charts"
        Public Function GetChartPrefrences(ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
                Dim StrSql As String = ""
                StrSql = "SELECT  USERID,  "
                StrSql = StrSql + "UNITS, "
                StrSql = StrSql + "CURRENCY, "
                StrSql = StrSql + "CURR, "
                StrSql = StrSql + "CONVERSIONFACTOR, "
                StrSql = StrSql + " TO_CHAR(CURREFFDATE,'MON DD,YYYY') AS CEFFDATE, "
                StrSql = StrSql + "TITLE1, "
                StrSql = StrSql + "TITLE2, "
                StrSql = StrSql + "TITLE3, "
                StrSql = StrSql + "TITLE4, "
                StrSql = StrSql + "TITLE5, "
                StrSql = StrSql + "TITLE6, "
                StrSql = StrSql + "CONVAREA, "
                StrSql = StrSql + "TITLE7, "
                StrSql = StrSql + "TITLE8, "
                StrSql = StrSql + "TITLE9, "
                StrSql = StrSql + "TITLE10, "
                StrSql = StrSql + "TITLE11, "
                StrSql = StrSql + "TITLE12, "
                StrSql = StrSql + "CONVWT, "
                StrSql = StrSql + "CONVAREA2, "
                StrSql = StrSql + "CONVTHICK, "
                StrSql = StrSql + "CONVTHICK2, "
                StrSql = StrSql + "CONVTHICK3 "
                StrSql = StrSql + "FROM ECON.CHARTPREFERENCES "
                StrSql = StrSql + "WHERE USERID=" + UserId + " "
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function

        Public Function GetChartProfitAndLoss(ByVal IsDep As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                If IsDep = "N" Then


                    StrSql = "SELECT 'Revenue' AS TYPEDES,  "
                    StrSql = StrSql + "'REVENUE' AS TYPE, "
                    StrSql = StrSql + "'1' AS SEQ "
                    StrSql = StrSql + "FROM DUAL "
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT 'Variable Margin' AS TYPEDES, "
                    StrSql = StrSql + "'VM' AS TYPE, "
                    StrSql = StrSql + "'2' AS SEQ "
                    StrSql = StrSql + "FROM DUAL "
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT 'Plant Margin' AS TYPEDES, "
                    StrSql = StrSql + "'PM' AS TYPE, "
                    StrSql = StrSql + "'2' AS SEQ "
                    StrSql = StrSql + "FROM DUAL ORDER BY  SEQ"
                Else

                    StrSql = "SELECT 'Revenue With Depreciation' AS TYPEDES,  "
                    StrSql = StrSql + "'REVENUE' AS TYPE, "
                    StrSql = StrSql + "'1' AS SEQ "
                    StrSql = StrSql + "FROM DUAL "
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT 'Variable Margin With Depreciation' AS TYPEDES, "
                    StrSql = StrSql + "'VM' AS TYPE, "
                    StrSql = StrSql + "'2' AS SEQ "
                    StrSql = StrSql + "FROM DUAL "
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT 'Plant Margin With Depreciation' AS TYPEDES, "
                    StrSql = StrSql + "'PMDEP' AS TYPE, "
                    StrSql = StrSql + "'3' AS SEQ "
                    StrSql = StrSql + "FROM DUAL ORDER BY  SEQ"

                End If
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("S1GetData:GetChartErgy:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetChartCost(ByVal IsDep As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                If IsDep = "N" Then

                    StrSql = "SELECT 'Variable Cost' AS TYPEDES,  "
                    StrSql = StrSql + "'VARIABLECOST' AS TYPE, "
                    StrSql = StrSql + "'1' AS SEQ "
                    StrSql = StrSql + "FROM DUAL "
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT 'Fixed Cost' AS TYPEDES, "
                    StrSql = StrSql + "'FIXEDCOST' AS TYPE, "
                    StrSql = StrSql + "'2' AS SEQ "
                    StrSql = StrSql + "FROM DUAL "
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT 'Total Cost' AS TYPEDES, "
                    StrSql = StrSql + "'TOTALCOST' AS TYPE, "
                    StrSql = StrSql + "'2' AS SEQ "
                    StrSql = StrSql + "FROM DUAL ORDER BY  SEQ"
                Else
                    StrSql = "SELECT 'Variable Cost With Depreciation' AS TYPEDES,  "
                    StrSql = StrSql + "'VARIABLECOST' AS TYPE, "
                    StrSql = StrSql + "'1' AS SEQ "
                    StrSql = StrSql + "FROM DUAL "
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT 'Fixed Cost With Depreciation' AS TYPEDES, "
                    StrSql = StrSql + "'FIXEDCOSTDEP' AS TYPE, "
                    StrSql = StrSql + "'2' AS SEQ "
                    StrSql = StrSql + "FROM DUAL "
                    StrSql = StrSql + "UNION "
                    StrSql = StrSql + "SELECT 'Total Cost With Depreciation' AS TYPEDES, "
                    StrSql = StrSql + "'TOTALCOSTDEP' AS TYPE, "
                    StrSql = StrSql + "'3' AS SEQ "
                    StrSql = StrSql + "FROM DUAL ORDER BY  SEQ"

                End If
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("S1GetData:GetChartErgy:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetChartProfitAndLossRes(ByVal CaseId1 As String, ByVal CaseId2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT RESULTSPL.CASEID,  "
                StrSql = StrSql + "( CASE WHEN RESULTSPL.CASEID <= 1000 THEN "
                StrSql = StrSql + "( SELECT (BASECASES.CASEID||':'||REPLACE(REPLACE((REPLACE(NVL(BASECASES.CASEDE1,''),')','}')),'(','{')||' '||REPLACE((REPLACE(NVL(BASECASES.CASEDE2,''),')','}')),'(','{'),';',':') ) "
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
                StrSql = StrSql + "RESULTSPL.REVENUE, "
                StrSql = StrSql + "RESULTSPL.VM, "
                StrSql = StrSql + "RESULTSPL.PM, "
                StrSql = StrSql + "RESULTSPL.PMDEP, "
                StrSql = StrSql + "RESULTSPL.CUSSALESVOLUME CUSSALESVOLUME, "
                StrSql = StrSql + "RESULTSPL.CUSSALESUNIT, "
                StrSql = StrSql + "NVL( "
                StrSql = StrSql + "(CASE  WHEN FINVOLMSI > 1 THEN "
                StrSql = StrSql + "(100)/FINVOLMSI "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "(100)/FINVOLMUNITS "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END),0)SUNIT, "
                StrSql = StrSql + "NVL((CASE  WHEN FINVOLMSI > 1 THEN "
                StrSql = StrSql + "(FINVOLMSI)"
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "(FINVOLMUNITS) "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END),0)SUNITVAL "

                StrSql = StrSql + "FROM RESULTSPL "
                StrSql = StrSql + "WHERE RESULTSPL.CASEID IN (" + CaseId1.ToString() + "," + CaseId2.ToString() + ") "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetChartProfitAndLossRes:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetChartCostRes(ByVal CaseId1 As String, ByVal CaseId2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT RESULTSPL.CASEID,  "
                StrSql = StrSql + "( CASE WHEN RESULTSPL.CASEID <= 1000 THEN "
                StrSql = StrSql + "( SELECT (BASECASES.CASEID||':'||REPLACE(REPLACE((REPLACE(NVL(BASECASES.CASEDE1,''),')','}')),'(','{')||' '||REPLACE((REPLACE(NVL(BASECASES.CASEDE2,''),')','}')),'(','{'),';',':') ) "
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
                StrSql = StrSql + "RESULTSPL.FIXEDCOSTDEP, "
                StrSql = StrSql + "RESULTSPL.TOTALCOSTDEP, "
                StrSql = StrSql + "RESULTSPL.VARIABLECOST, "
                StrSql = StrSql + "RESULTSPL.FIXEDCOST, "
                StrSql = StrSql + "RESULTSPL.TOTALCOST, "
                StrSql = StrSql + "RESULTSPL.CUSSALESVOLUME CUSSALESVOLUME, "
                StrSql = StrSql + "RESULTSPL.CUSSALESUNIT, "
                StrSql = StrSql + "NVL( "
                StrSql = StrSql + "(CASE  WHEN FINVOLMSI > 1 THEN "
                StrSql = StrSql + "(100)/FINVOLMSI "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "(100)/FINVOLMUNITS "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END),0)SUNIT, "
                StrSql = StrSql + "NVL((CASE  WHEN FINVOLMSI > 1 THEN "
                StrSql = StrSql + "(FINVOLMSI)"
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "CASE WHEN FINVOLMUNITS > 1 THEN "
                StrSql = StrSql + "(FINVOLMUNITS) "
                StrSql = StrSql + "END "
                StrSql = StrSql + "END),0)SUNITVAL "
                StrSql = StrSql + "FROM RESULTSPL "
                StrSql = StrSql + "WHERE RESULTSPL.CASEID IN (" + CaseId1.ToString() + "," + CaseId2.ToString() + ") "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetChartCostRes:" + ex.Message.ToString())
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
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
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

#Region "Wizard"
        Public Function GetSessionWizardId() As DataSet
            Dim Dts As New DataSet()
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
                Dim StrSql As String = ""
                StrSql = "SELECT (SEQSESSIONID.NEXTVAL) As SessionId FROM DUAL "
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
#End Region

#Region "LicenseAdministrator"
        Public Function GetPCaseDetailsByLicense(ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT DISTINCT CASEID,  "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "CASEDE2, "
                StrSql = StrSql + "CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "INNER JOIN ECON.USERS "
                StrSql = StrSql + "ON USERS.USERID=PERMISSIONSCASES.USERID "
                StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS WHERE USERID=" + UserId + ") "
                StrSql = StrSql + "AND SERVICEID IS NULL "
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID "

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetPCaseDetailsByLicense:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCasesByLicense(ByVal UserId As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT CASEID,  "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "CASEDE2, "
                StrSql = StrSql + "CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "INNER JOIN ECON.USERS "
                StrSql = StrSql + "ON USERS.USERID=PERMISSIONSCASES.USERID "
                StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS WHERE USERID=" + UserId + ") "
                StrSql = StrSql + "AND SERVICEID IS NULL "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID"
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetCasesByLicense:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Case Grouping:"
        Public Function ValidateGroupName(ByVal Des1 As String, ByVal Des2 As String, ByVal USERID As String, ByVal Comp As String) As DataSet
            Dim odButil As New DBUtil()
            Dim strsql As String = String.Empty
            Dim Dts As New DataSet
            Dim GROUPID As String = ""
            Dim i As Integer = 0
            Try
                'Getting GROUPID from Sequence
                strsql = String.Empty
                strsql = strsql + "SELECT 1 "
                strsql = strsql + "FROM "
                strsql = strsql + "GROUPS "
                strsql = strsql + "WHERE "
                strsql = strsql + "USERID=" + USERID + " "
                strsql = strsql + "AND UPPER(DES1)='" + Des1.ToUpper() + "' "
                strsql = strsql + "AND "
                If Des2 = "" Then
                    strsql = strsql + "DES2 IS NULL "
                Else
                    strsql = strsql + "UPPER(DES2)='" + Des2.ToUpper() + "' "
                End If
                If Comp = "COMP" Or Comp = "COMPS1" Then
                    strsql = strsql + "AND SERVICEID IS NOT NULL "
                Else
                    strsql = strsql + "AND SERVICEID IS NULL "
                End If
                Dts = odButil.FillDataSet(strsql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Return Dts
                Throw New Exception("Med1GetData:ValidateGroupName:" + ex.Message.ToString())
            End Try
        End Function
        Public Function GetPCaseGrpDetails(ByVal UserName As String, ByVal UserID As String, ByVal keyWord As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT GROUPNAME,GROUPID,CASEID,CASEDE1,CASEDE2,CASEDE3,CASEDES,CREATIONDATE,SERVERDATE "
                StrSql = StrSql + " FROM "
                StrSql = StrSql + " ( "
                StrSql = StrSql + "SELECT CASE WHEN NVL(GROUPS.GROUPID,'0')='0' THEN 'None' ELSE GROUPS.DES1 END AS GROUPNAME,  "
                StrSql = StrSql + "NVL(GROUPS.GROUPID,'0') AS GROUPID, "
                StrSql = StrSql + "PC.CASEID, "
                StrSql = StrSql + "PC.CASEDE1, "
                StrSql = StrSql + "PC.CASEDE2, "
                StrSql = StrSql + "CAST(PC.CASEDE3 AS VARCHAR(4000))CASEDE3, "

                StrSql = StrSql + "(PC.CASEID||'. PACKAGE FORMAT= '||PC.CASEDE1||' UNIQUE FEATURES= '||PC.CASEDE2)CASEDES, "

                StrSql = StrSql + " PC.CREATIONDATE, "
                StrSql = StrSql + "CASE WHEN PC.CREATIONDATE-PC.SERVERDATE =0 THEN 'NA' ELSE to_char(PC.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE "


                StrSql = StrSql + "FROM GROUPS "
                StrSql = StrSql + "INNER JOIN GROUPCASES "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
                StrSql = StrSql + "AND GROUPS.USERID=" + UserID + " "
                StrSql = StrSql + "RIGHT OUTER JOIN PERMISSIONSCASES PC "
                StrSql = StrSql + "ON PC.CASEID=GROUPCASES.CASEID "
                StrSql = StrSql + "WHERE PC.USERID =" + UserID + " "
                StrSql = StrSql + "AND PC.SERVICEID IS NULL  "
                StrSql = StrSql + "ORDER BY CASEID DESC "
                StrSql = StrSql + " ) "

                StrSql = StrSql + "WHERE UPPER(CASEDE1) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE2) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEID) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE3) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(GROUPNAME) LIKE '%" + keyWord.ToUpper().Trim() + "%' "

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetPCaseGrpDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetGroupDetails(ByVal UserID As String, ByVal flag As Char) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New Med1GetData.Selectdata()
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim strSQL As String = String.Empty
            Dim ds As New DataSet()
            Dim CaseIDs As String = String.Empty
            Dim DtRes As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim MyConnectionString As String = ""
            Dim dtOutPut As New DataSet()
            Dim strSqlOutPut As String = String.Empty
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
            Try


                'Getting Groups
                strSQL = "SELECT GROUPID,  "
                strSQL = strSQL + "DES1 GROUPNAME, "
                strSQL = strSQL + "DES2 GROUPDES, "
                strSQL = strSQL + "GROUPID || ':'|| DES1 CDES1,"
                strSQL = strSQL + "to_char(CREATIONDATE,'mm/dd/yyyy hh:mi:ss AM'  ) CREATIONDATE, "
                strSQL = strSQL + "CASE WHEN CREATIONDATE-UPDATEDATE =0 THEN 'NA' ELSE to_char(UPDATEDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  UPDATEDATE "
                strSQL = strSQL + "FROM "
                strSQL = strSQL + "GROUPS "
                strSQL = strSQL + "WHERE USERID= " + UserID + " "
                strSQL = strSQL + "AND SERVICEID IS NULL "
                DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                If DtRes.Tables(0).Rows.Count > 0 Then
                    For j = 0 To DtRes.Tables(0).Rows.Count - 1
                        'Getting CaseID
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "CASEID "
                        strSQL = strSQL + "FROM MED1.GROUPCASES "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString()
                        strSQL = strSQL + " ORDER BY  SEQ"
                        Dts = odbUtil.FillDataSet(strSQL, MedEcon1Connection)
                        If Dts.Tables(0).Rows.Count > 0 Then
                            For i = 0 To Dts.Tables(0).Rows.Count - 1
                                If i = 0 Then
                                    CaseIDs = Dts.Tables(0).Rows(i).Item("CASEID").ToString()
                                Else
                                    CaseIDs = CaseIDs + ", " + Dts.Tables(0).Rows(i).Item("CASEID").ToString()
                                End If
                            Next
                        Else
                            CaseIDs = ""
                        End If
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "DES1 GROUPNAME, "
                        strSQL = strSQL + "'" + CaseIDs.ToString() + "' CaseID, "
                        strSQL = strSQL + "DES2 GROUPDES, "
                        strSQL = strSQL + "GROUPID || ':'|| DES1 CDES1,"
                        strSQL = strSQL + "to_char(CREATIONDATE,'mm/dd/yyyy hh:mi:ss AM'  ) CREATIONDATE, "
                        strSQL = strSQL + "CASE WHEN CREATIONDATE-UPDATEDATE =0 THEN 'NA' ELSE to_char(UPDATEDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  UPDATEDATE "
                        strSQL = strSQL + "FROM "
                        strSQL = strSQL + "GROUPS "
                        strSQL = strSQL + "WHERE USERID= " + UserID + " AND GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString() + " "
                        If j = 0 Then
                            strSqlOutPut = strSQL
                        Else
                            strSqlOutPut = strSqlOutPut + " UNION ALL " + strSQL
                        End If
                    Next
                    If flag = "Y" Then ' Y FOR GROUPDEATILS PAGE ,N FOR EDITGROUPS PAGE
                        strSQL = "SELECT 0 GROUPID,  "
                        strSQL = strSQL + "'None'  GROUPNAME, "
                        strSQL = strSQL + "'NA' CaseID, "
                        strSQL = strSQL + "'NA'  GROUPDES, "
                        strSQL = strSQL + "'0:None'  CDES1, "
                        strSQL = strSQL + "'NA'  CREATIONDATE, "
                        strSQL = strSQL + "'NA'  UPDATEDATE "
                        strSQL = strSQL + "FROM "
                        strSQL = strSQL + "DUAL "
                        strSqlOutPut = strSqlOutPut + " UNION ALL " + strSQL
                    End If
                    strSqlOutPut = " SELECT * FROM ( " + strSqlOutPut + " ) DUAL ORDER BY UPPER(GROUPNAME),UPPER(GROUPDES)"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, MedEcon1Connection)
                Else
                    strSqlOutPut = " SELECT * FROM GROUPCASES WHERE GROUPID=0 "
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, MedEcon1Connection)
                End If

                Return dtOutPut
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function
        Public Function GetPerBaseCases(ByVal UserName As String) As DataTable
            Dim Dts As New DataTable
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")

                Dim StrSql As String = "SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE FROM PERMISSIONSCASES WHERE UPPER(username)='" + UserName.ToUpper() + "'"
                StrSql = StrSql + "UNION SELECT CASEID,CASEDE1,('CASE:'||CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE  FROM BASECASES ORDER BY caseDE1"

                Dts = odbUtil.FillDataTable(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
        Public Function GetGroupDetailsByID(ByVal grpID As String) As DataSet
            Dim Dts As New DataSet()
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                Dim StrSql As String = ""
                StrSql = "SELECT GROUPID,  "
                StrSql = StrSql + "DES1, "
                StrSql = StrSql + "DES2, "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "DES1 || '&'||'nbsp;'||'&'||'nbsp;'||'&'||'nbsp;'|| DES2 AS GDES, "
                StrSql = StrSql + "CREATIONDATE, "
                StrSql = StrSql + "UPDATEDATE "
                StrSql = StrSql + "FROM GROUPS WHERE GROUPID= " + grpID + " "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
        Public Function GetGroupIDByUSer(ByVal UserID As String) As DataSet
            Dim Dts As New DataSet()
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
                Dim StrSql As String = ""
                StrSql = "SELECT GROUPID,  "
                StrSql = StrSql + "DES1, "
                StrSql = StrSql + "DES2, "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "CREATIONDATE, "
                StrSql = StrSql + "UPDATEDATE "
                StrSql = StrSql + "FROM GROUPS WHERE USERID= " + UserID + " "
                StrSql = StrSql + "AND SERVICEID IS NULL "
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
        Public Function ValiDateGroupcases(ByVal CaseIDs As String, ByVal UserID As String) As DataSet
            Dim odbUtil As New DBUtil()
            Dim MyConnectionString As String = ""
            Dim Dts As New DataSet()
            Dim strCaseIDS() As String
            Dim i As Integer = 0
            Dim StrSql As String = ""
            Dim strQuery As String = String.Empty
            Dim count As Integer = 0
            Try
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
                strCaseIDS = Regex.Split(CaseIDs, ",")

                If strCaseIDS.Length > 0 Then
                    For i = 0 To strCaseIDS.Length - 1
                        If strCaseIDS(i) <> "" Then
                            StrSql = "SELECT GROUPS.GROUPID,  "
                            StrSql = StrSql + "CASEID, "
                            StrSql = StrSql + "DES1 AS GROUPNAME "
                            StrSql = StrSql + "FROM "
                            StrSql = StrSql + "GROUPCASES "
                            StrSql = StrSql + "INNER JOIN GROUPS "
                            StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.GROUPID "
                            StrSql = StrSql + "WHERE CASEID=" + strCaseIDS(i).ToString()
                            StrSql = StrSql + " AND UserID=" + UserID.ToString()
                            Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                            If Dts.Tables(0).Rows.Count > 0 Then
                                If count = 0 Then
                                    strQuery = " SELECT '" + Dts.Tables(0).Rows(0).Item("CASEID").ToString() + "' CASEID,'" + Dts.Tables(0).Rows(0).Item("GROUPID").ToString() + "' GROUPID,'" + Dts.Tables(0).Rows(0).Item("GROUPNAME").ToString() + "' AS GROUPNAME FROM DUAL "
                                Else
                                    strQuery = strQuery + " UNION ALL SELECT '" + Dts.Tables(0).Rows(0).Item("CASEID").ToString() + "' CASEID,'" + Dts.Tables(0).Rows(0).Item("GROUPID").ToString() + "' GROUPID,'" + Dts.Tables(0).Rows(0).Item("GROUPNAME").ToString() + "' AS GROUPNAME FROM DUAL "
                                End If
                                count += 1
                            End If
                        End If
                    Next
                End If
                If strQuery <> "" Then
                    Dts = odbUtil.FillDataSet(strQuery, MyConnectionString)
                Else
                    StrSql = " SELECT * FROM GROUPCASES WHERE GROUPID=0 "
                    Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                End If

                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
        Public Function GetGroupCasesByUSer(ByVal grpID As String) As DataSet
            Dim Dts As New DataSet()
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                Dim StrSql As String = ""
                StrSql = "SELECT GROUPCASEID,  "
                StrSql = StrSql + "GROUPID, "
                StrSql = StrSql + "CASEID, "
                StrSql = StrSql + "SEQ "
                StrSql = StrSql + "FROM GROUPCASES WHERE GROUPID=" + grpID + " "
                StrSql = StrSql + "ORDER BY SEQ ASC "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
        Public Function GetGroupCaseDetails(ByVal UserID As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New Med1GetData.Selectdata()
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim strSQL As String = String.Empty
            Dim ds As New DataSet()
            Dim CaseIDs As String = String.Empty
            Dim DtRes As New DataSet()
            Dim cnt As Integer = 0
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
                Dts = objGetData.GetGroupIDByUSer(UserID)
                If Dts.Tables(0).Rows.Count > 0 Then
                    For i = 0 To Dts.Tables(0).Rows.Count - 1
                        ds = objGetData.GetGroupCasesByUSer(Dts.Tables(0).Rows(i).Item("GROUPID").ToString())
                        If ds.Tables(0).Rows.Count > 0 Then
                            For j = 0 To ds.Tables(0).Rows.Count - 1
                                If j = 0 Then
                                    CaseIDs = ds.Tables(0).Rows(j).Item("CASEID").ToString()
                                Else
                                    CaseIDs = CaseIDs + " " + ds.Tables(0).Rows(j).Item("CASEID").ToString()
                                End If
                            Next
                        Else
                            CaseIDs = "0"
                        End If
                        If cnt = 0 Then
                            cnt += 1
                            'strSQL = "SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' || ' ,Cases:" + CaseIDs + "' AS GROUPDES,'" + CaseIDs + "' AS CASEIDS FROM DUAL "
                            strSQL = "SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '   ' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '   '|| '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "'  AS GROUPDES,'" + CaseIDs + "' AS CASEIDS,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS DES1,'" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS DES2,'" + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + "' || ':' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS CDES1 FROM DUAL "
                        Else
                            cnt += 1
                            strSQL = strSQL + "UNION ALL SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' ||  '   ' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '   '||  '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "'  AS GROUPDES,'" + CaseIDs + "' AS CASEIDS,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS DES1,'" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS DES2,'" + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + "' || ':' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS CDES1 FROM DUAL "
                        End If
                    Next
                    strSQL = " SELECT * FROM ( " + strSQL + " ) DUAL ORDER BY UPPER(DES1),UPPER(DES2)"
                End If
                If strSQL <> "" Then
                    DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                Else
                    strSQL = "select * FROM GROUPS WHERE GROUPID=0"
                    DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                End If
                Return DtRes
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function
        Public Function GetEditCases(ByVal UserName As String, ByVal grpID As String, ByVal flag As String) As DataTable
            Dim Dts As New DataTable()
            Dim i As New Integer
            Try
                Dim odbUtil As New DBUtil()
                Dim StrSql As String = ""
                StrSql = "SELECT CASEID,CASEDE1,CASEDE,SEQ FROM  "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT PERMISSIONSCASES.CASEID, "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "SEQ, "
                StrSql = StrSql + "('CASE:'||PERMISSIONSCASES.CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + " LEFT OUTER JOIN MED1.GROUPCASES ON GROUPCASES.CASEID=PERMISSIONSCASES.CASEID "
                StrSql = StrSql + "WHERE Upper(username)='" + UserName.ToUpper() + "' "
                If flag <> "true" Then
                    StrSql = StrSql + "AND  GROUPID=" + grpID.ToString() + " "
                End If
                StrSql = StrSql + ") "
                StrSql = StrSql + "Where CASEID "
                If flag = "true" Then
                    StrSql = StrSql + "NOT IN "
                    StrSql = StrSql + "(  "
                    StrSql = StrSql + "SELECT CASEID "
                    StrSql = StrSql + "FROM GROUPCASES "
                    StrSql = StrSql + "WHERE GROUPID=" + grpID + ") "
                    StrSql = StrSql + "ORDER BY CASEDE1,CASEID "
                Else
                    StrSql = StrSql + "IN "
                    StrSql = StrSql + "(  "
                    StrSql = StrSql + "SELECT CASEID "
                    StrSql = StrSql + "FROM GROUPCASES "
                    StrSql = StrSql + "WHERE GROUPID=" + grpID + ") "
                    StrSql = StrSql + "ORDER BY SEQ "
                End If


                Dts = odbUtil.FillDataTable(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Return Dts
            End Try
        End Function
        Public Function GetPCaseDetailsByGroup(ByVal UserId As String, ByVal grpId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PERMISSIONSCASES.CASEID,CASEDE1,CASEDE2,CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(PERMISSIONSCASES.CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES,SEQ FROM PERMISSIONSCASES "
                StrSql = StrSql + " INNER JOIN GROUPCASES ON GROUPCASES.CASEID=PERMISSIONSCASES.CASEID "
                StrSql = StrSql + "WHERE USERID =" + UserId + "  AND GROUPCASES.GROUPID=" + grpId + " "
                StrSql = StrSql + " AND PERMISSIONSCASES.CASEID IN(SELECT CASEID FROM GROUPCASES WHERE GROUPID =" + grpId + ")"
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID "


                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetPCaseDetailsByGroup:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetGroupCases(ByVal UserId As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String, ByVal groupID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE USERID =" + UserId + " "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "AND CASEID IN(SELECT CASEID FROM MED1.GROUPCASES WHERE GROUPID=" + groupID + " ) "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID"
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetMaxSEQGCASE(ByVal grpId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT NVL(MAX(SEQ),0) MAXCOUNT  "
                StrSql = StrSql + "FROM GROUPCASES "
                StrSql = StrSql + "WHERE GROUPID= " + grpId

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetMaxSEQGCASE:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetAllGroupDetails(ByVal UserID As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New Med1GetData.Selectdata()
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim strSQL As String = String.Empty
            Dim ds As New DataSet()
            Dim CaseIDs As String = String.Empty
            Dim DtRes As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim MyConnectionString As String = ""
            Dim dtOutPut As New DataSet()
            Dim strSqlOutPut As String = String.Empty
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
            Try


                'Getting Groups
                strSQL = "SELECT GROUPID,  "
                strSQL = strSQL + "DES1 GROUPNAME, "
                strSQL = strSQL + "DES2 GROUPDES, "
                strSQL = strSQL + "GROUPID || ':'|| DES1 CDES1,"
                strSQL = strSQL + "to_char(CREATIONDATE,'mm/dd/yyyy hh:mi:ss AM'  ) CREATIONDATE, "
                strSQL = strSQL + "CASE WHEN CREATIONDATE-UPDATEDATE =0 THEN 'NA' ELSE to_char(UPDATEDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  UPDATEDATE "
                strSQL = strSQL + "FROM "
                strSQL = strSQL + "GROUPS "
                strSQL = strSQL + "WHERE USERID= " + UserID + " "
                strSQL = strSQL + "AND SERVICEID IS NULL "
                DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                If DtRes.Tables(0).Rows.Count > 0 Then
                    For j = 0 To DtRes.Tables(0).Rows.Count - 1
                        'Getting CaseID
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "CASEID "
                        strSQL = strSQL + "FROM MED1.GROUPCASES "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString()
                        strSQL = strSQL + " ORDER BY  SEQ"
                        Dts = odbUtil.FillDataSet(strSQL, MedEcon1Connection)
                        If Dts.Tables(0).Rows.Count > 0 Then
                            For i = 0 To Dts.Tables(0).Rows.Count - 1
                                If i = 0 Then
                                    CaseIDs = Dts.Tables(0).Rows(i).Item("CASEID").ToString()
                                Else
                                    CaseIDs = CaseIDs + ", " + Dts.Tables(0).Rows(i).Item("CASEID").ToString()
                                End If
                            Next
                        Else
                            CaseIDs = ""
                        End If
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "DES1 GROUPNAME, "
                        strSQL = strSQL + "'" + CaseIDs.ToString() + "' CASEID, "
                        strSQL = strSQL + "DES2 GROUPDES, "
                        strSQL = strSQL + "USR.UserName, "
                        strSQL = strSQL + "GROUPID || ':'|| DES1 CDES1,"
                        strSQL = strSQL + "to_char(GPS.CREATIONDATE,'mm/dd/yyyy hh:mi:ss AM'  ) CREATIONDATE, "
                        strSQL = strSQL + "CASE WHEN GPS.CREATIONDATE-GPS.UPDATEDATE =0 THEN 'NA' ELSE to_char(GPS.UPDATEDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  UPDATEDATE "
                        strSQL = strSQL + "FROM "
                        strSQL = strSQL + "GROUPS GPS "
                        strSQL = strSQL + "INNER JOIN ECON.USERS USR "
                        strSQL = strSQL + "ON  USR.USERID=GPS.USERID "
                        strSQL = strSQL + "WHERE GPS.USERID= " + UserID + " AND GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString() + " "
                        If j = 0 Then
                            strSqlOutPut = strSQL
                        Else
                            strSqlOutPut = strSqlOutPut + " UNION ALL " + strSQL
                        End If
                    Next

                    strSqlOutPut = "SELECT * FROM ( " + strSqlOutPut + " ) DUAL ORDER BY UPPER(GROUPNAME),UPPER(GROUPDES)"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, MedEcon1Connection)
                Else
                    strSqlOutPut = " SELECT * FROM GROUPCASES WHERE GROUPID=0"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, MedEcon1Connection)
                End If

                Return dtOutPut
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function
#End Region

#Region "Injection Molding"
        Public Function GetInjectionDetails(ByVal CaseID As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT   INJIN.CASEID,  "

                StrSql = StrSql + "m1.Matde2, "
                StrSql = StrSql + "m2.Matde2, "
                StrSql = StrSql + "m3.Matde2, "
                StrSql = StrSql + "m4.Matde2,"
                StrSql = StrSql + "m5.Matde2, "
                StrSql = StrSql + "m6.Matde2, "
                StrSql = StrSql + "m7.Matde2, "
                StrSql = StrSql + "m8.Matde2, "
                StrSql = StrSql + "m9.Matde2, "
                StrSql = StrSql + "m10.Matde2,"

                StrSql = StrSql + "case when m1.sg = 0 then 0 else PWt1/(m1.SG*62.43) end partvol1, "
                StrSql = StrSql + "case when m2.sg = 0 then 0 else PWt2/(m2.SG*62.43) end partvol2, "
                StrSql = StrSql + "case when m3.sg = 0 then 0 else PWt3/(m3.SG*62.43) end partvol3, "
                StrSql = StrSql + "case when m4.sg = 0 then 0 else PWt4/(m4.SG*62.43) end partvol4, "
                StrSql = StrSql + "case when m5.sg = 0 then 0 else PWt5/(m5.SG*62.43) end partvol5, "
                StrSql = StrSql + "case when m6.sg = 0 then 0 else PWt6/(m6.SG*62.43) end partvol6, "
                StrSql = StrSql + "case when m7.sg = 0 then 0 else PWt7/(m7.SG*62.43) end partvol7, "
                StrSql = StrSql + "case when m8.sg = 0 then 0 else PWt8/(m8.SG*62.43) end partvol8, "
                StrSql = StrSql + "case when m9.sg = 0 then 0 else PWt9/(m9.SG*62.43) end partvol9, "
                StrSql = StrSql + "case when m10.sg = 0 then 0 else PWt10/(m10.SG*62.43) end partvol10, "



                StrSql = StrSql + "CASE RunSys1  WHEN '1' THEN 0  ELSE sa1*CavPL1*0.10   END SArea1, "
                StrSql = StrSql + " CASE RunSys2  WHEN '1' THEN 0  ELSE sa2*CavPL1*0.10   END SArea2, "
                StrSql = StrSql + "CASE RunSys3  WHEN '1' THEN 0  ELSE sa3*CavPL1*0.10   END SArea3, "
                StrSql = StrSql + "CASE RunSys4  WHEN '1' THEN 0  ELSE sa4*CavPL1*0.10   END SArea4, "
                StrSql = StrSql + "CASE RunSys5  WHEN '1' THEN 0  ELSE sa5*CavPL1*0.10   END SArea5, "
                StrSql = StrSql + "CASE RunSys6  WHEN '1' THEN 0  ELSE sa6*CavPL1*0.10   END SArea6, "
                StrSql = StrSql + "CASE RunSys7  WHEN '1' THEN 0  ELSE sa7*CavPL1*0.10   END SArea7, "
                StrSql = StrSql + "CASE RunSys8  WHEN '1' THEN 0  ELSE sa8*CavPL1*0.10   END SArea8, "
                StrSql = StrSql + "CASE RunSys9  WHEN '1' THEN 0  ELSE sa9*CavPL1*0.10   END SArea9, "
                StrSql = StrSql + "CASE RunSys10 WHEN '1' THEN 0  ELSE sa10*CavPL1*0.10   END SArea10, "

                StrSql = StrSql + "Lvls1*cavpl1   totalcav1, "
                StrSql = StrSql + "Lvls2*cavpl2   totalcav2, "
                StrSql = StrSql + "Lvls3*cavpl3   totalcav3, "
                StrSql = StrSql + "Lvls4*cavpl4   totalcav4, "
                StrSql = StrSql + "Lvls5*cavpl5   totalcav5, "
                StrSql = StrSql + "Lvls6*cavpl6   totalcav6, "
                StrSql = StrSql + "Lvls7*cavpl7   totalcav7, "
                StrSql = StrSql + "Lvls8*cavpl8   totalcav8, "
                StrSql = StrSql + "Lvls9*cavpl9   totalcav9, "
                StrSql = StrSql + "Lvls10*cavpl10   totalcav10, "
                StrSql = StrSql + "sa1*CavPL1 totpartSA1, "
                StrSql = StrSql + "sa2*CavPL2 totpartSA2, "
                StrSql = StrSql + "sa3*CavPL3 totpartSA3, "
                StrSql = StrSql + "sa4*CavPL4 totpartSA4, "
                StrSql = StrSql + "sa5*CavPL5 totpartSA5, "
                StrSql = StrSql + "sa6*CavPL6 totpartSA6, "
                StrSql = StrSql + "sa7*CavPL7 totpartSA7, "
                StrSql = StrSql + "sa8*CavPL8 totpartSA8, "
                StrSql = StrSql + "sa9*CavPL9 totpartSA9, "
                StrSql = StrSql + "sa10*CavPL10 totpartSA10, "
                For i = 1 To 10
                    StrSql = StrSql + "CASE runsys" + i.ToString() + " "
                    StrSql = StrSql + "WHEN '1' then "
                    StrSql = StrSql + "CASE m" + i.ToString() + ".SG when 0 then 0"
                    StrSql = StrSql + "ELSE  ROUND((PWt" + i.ToString() + "*16.0/m" + i.ToString() + ".SG* Lvls" + i.ToString() + "*cavpl" + i.ToString() + "),3) "
                    StrSql = StrSql + "END "
                    StrSql = StrSql + "ELSE "
                    StrSql = StrSql + "CASE  m" + i.ToString() + ".SG when 0 then 0 "
                    StrSql = StrSql + "ELSE ROUND(1.2*PWt" + i.ToString() + "*16.0/m" + i.ToString() + ".SG* Lvls" + i.ToString() + "*cavpl" + i.ToString() + ",3) "
                    StrSql = StrSql + "END "
                    StrSql = StrSql + "END ShotWt" + i.ToString() + ", "
                Next
                'StrSql = StrSql + "runsys1, m1.SG, PWt1,mwt1,m1.meltdensity*998.8473 MD1,injrateps1,m1.EFFDIFFUSIVITY EFFDIFFUSIVITY1, "
                'StrSql = StrSql + "runsys2, m2.SG, PWt2,mwt2,m2.meltdensity*998.8473 MD2,injrateps2,m2.EFFDIFFUSIVITY EFFDIFFUSIVITY2, "
                'StrSql = StrSql + "runsys3, m3.SG, PWt3,mwt3,m3.meltdensity*998.8473 MD3,injrateps3,m3.EFFDIFFUSIVITY EFFDIFFUSIVITY3, "
                'StrSql = StrSql + "runsys4, m4.SG, PWt4,mwt4,m4.meltdensity*998.8473 MD4,injrateps4,m4.EFFDIFFUSIVITY EFFDIFFUSIVITY4, "
                'StrSql = StrSql + "runsys5, m5.SG, PWt5,mwt5,m5.meltdensity*998.8473 MD5,injrateps5,m5.EFFDIFFUSIVITY EFFDIFFUSIVITY5, "
                'StrSql = StrSql + "runsys6, m6.SG, PWt6,mwt6,m6.meltdensity*998.8473 MD6,injrateps6,m6.EFFDIFFUSIVITY EFFDIFFUSIVITY6, "
                'StrSql = StrSql + "runsys7, m7.SG, PWt7,mwt7,m7.meltdensity*998.8473 MD7,injrateps7,m7.EFFDIFFUSIVITY EFFDIFFUSIVITY7, "
                'StrSql = StrSql + "runsys8, m8.SG, PWt8,mwt8,m8.meltdensity*998.8473 MD8,injrateps8,m8.EFFDIFFUSIVITY EFFDIFFUSIVITY8, "
                'StrSql = StrSql + "runsys9, m9.SG, PWt9,mwt9,m9.meltdensity*998.8473 MD9,injrateps9,m9.EFFDIFFUSIVITY EFFDIFFUSIVITY9, "
                'StrSql = StrSql + "runsys10,m10.SG, PWt10,mwt10,m10.meltdensity*998.8473 MD10,injrateps10,m10.EFFDIFFUSIVITY EFFDIFFUSIVITY10, "
                StrSql = StrSql + "runsys1, m1.SG, PWt1,mwt1,NVL(INJA1.meltdensity*998.8473,0) MD1,injrateps1,NVL(INJA1.EFFDIFFUSIVITY,0) EFFDIFFUSIVITY1, "
                StrSql = StrSql + "runsys2, m2.SG, PWt2,mwt2,NVL(INJA2.meltdensity*998.8473,0) MD2,injrateps2,NVL(INJA2.EFFDIFFUSIVITY,0) EFFDIFFUSIVITY2, "
                StrSql = StrSql + "runsys3, m3.SG, PWt3,mwt3,NVL(INJA3.meltdensity*998.8473,0) MD3,injrateps3,NVL(INJA3.EFFDIFFUSIVITY,0) EFFDIFFUSIVITY3, "
                StrSql = StrSql + "runsys4, m4.SG, PWt4,mwt4,NVL(INJA4.meltdensity*998.8473,0) MD4,injrateps4,NVL(INJA4.EFFDIFFUSIVITY,0) EFFDIFFUSIVITY4, "
                StrSql = StrSql + "runsys5, m5.SG, PWt5,mwt5,NVL(INJA5.meltdensity*998.8473,0) MD5,injrateps5,NVL(INJA5.EFFDIFFUSIVITY,0) EFFDIFFUSIVITY5, "
                StrSql = StrSql + "runsys6, m6.SG, PWt6,mwt6,NVL(INJA6.meltdensity*998.8473,0) MD6,injrateps6,NVL(INJA6.EFFDIFFUSIVITY,0) EFFDIFFUSIVITY6, "
                StrSql = StrSql + "runsys7, m7.SG, PWt7,mwt7,NVL(INJA7.meltdensity*998.8473,0) MD7,injrateps7,NVL(INJA7.EFFDIFFUSIVITY,0) EFFDIFFUSIVITY7, "
                StrSql = StrSql + "runsys8, m8.SG, PWt8,mwt8,NVL(INJA8.meltdensity*998.8473,0) MD8,injrateps8,NVL(INJA8.EFFDIFFUSIVITY,0) EFFDIFFUSIVITY8, "
                StrSql = StrSql + "runsys9, m9.SG, PWt9,mwt9,NVL(INJA9.meltdensity*998.8473,0) MD9,injrateps9,NVL(INJA9.EFFDIFFUSIVITY,0) EFFDIFFUSIVITY9, "
                StrSql = StrSql + "runsys10,m10.SG, PWt10,mwt10,NVL(INJA10.meltdensity*998.8473,0) MD10,injrateps10,NVL(INJA10.EFFDIFFUSIVITY,0) EFFDIFFUSIVITY10, "


                StrSql = StrSql + " INJIN.MATID1, "
                StrSql = StrSql + " INJIN.MATID2, "
                StrSql = StrSql + " INJIN.MATID3, "
                StrSql = StrSql + " INJIN.MATID4, "
                StrSql = StrSql + " INJIN.MATID5, "
                StrSql = StrSql + " INJIN.MATID6, "
                StrSql = StrSql + " INJIN.MATID7, "
                StrSql = StrSql + " INJIN.MATID8, "
                StrSql = StrSql + " INJIN.MATID9, "
                StrSql = StrSql + " INJIN.MATID10, "

                StrSql = StrSql + " INJIN.AWT1, "
                StrSql = StrSql + " INJIN.AWT2, "
                StrSql = StrSql + " INJIN.AWT3, "
                StrSql = StrSql + " INJIN.AWT4, "
                StrSql = StrSql + " INJIN.AWT5, "
                StrSql = StrSql + " INJIN.AWT6, "
                StrSql = StrSql + " INJIN.AWT7, "
                StrSql = StrSql + " INJIN.AWT8, "
                StrSql = StrSql + " INJIN.AWT9, "
                StrSql = StrSql + " INJIN.AWT10, "
                StrSql = StrSql + " INJIN.SA1, "
                StrSql = StrSql + " INJIN.SA2, "
                StrSql = StrSql + " INJIN.SA3, "
                StrSql = StrSql + " INJIN.SA4, "
                StrSql = StrSql + " INJIN.SA5, "
                StrSql = StrSql + " INJIN.SA6, "
                StrSql = StrSql + " INJIN.SA7, "
                StrSql = StrSql + " INJIN.SA8, "
                StrSql = StrSql + " INJIN.SA9, "
                StrSql = StrSql + " INJIN.SA10, "
                StrSql = StrSql + " INJIN.SMold1, "
                StrSql = StrSql + " INJIN.SMold2, "
                StrSql = StrSql + " INJIN.SMold3, "
                StrSql = StrSql + " INJIN.SMold4, "
                StrSql = StrSql + " INJIN.SMold5, "
                StrSql = StrSql + " INJIN.SMold6, "
                StrSql = StrSql + " INJIN.SMold7, "
                StrSql = StrSql + " INJIN.SMold8, "
                StrSql = StrSql + " INJIN.SMold9, "
                StrSql = StrSql + " INJIN.SMold10, "

                StrSql = StrSql + " INJIN.Lvls1, "
                StrSql = StrSql + " INJIN.Lvls2, "
                StrSql = StrSql + " INJIN.Lvls3, "
                StrSql = StrSql + " INJIN.Lvls4, "
                StrSql = StrSql + " INJIN.Lvls5, "
                StrSql = StrSql + " INJIN.Lvls6, "
                StrSql = StrSql + " INJIN.Lvls7, "
                StrSql = StrSql + " INJIN.Lvls8, "
                StrSql = StrSql + " INJIN.Lvls9, "
                StrSql = StrSql + " INJIN.Lvls10, "
                StrSql = StrSql + " INJIN.CavPL1 , "
                StrSql = StrSql + " INJIN.CavPL2 , "
                StrSql = StrSql + " INJIN.CavPL3 , "
                StrSql = StrSql + " INJIN.CavPL4, "
                StrSql = StrSql + " INJIN.CavPL5, "
                StrSql = StrSql + " INJIN.CavPL6, "
                StrSql = StrSql + " INJIN.CavPL7, "
                StrSql = StrSql + " INJIN.CavPL8, "
                StrSql = StrSql + " INJIN.CavPL9, "
                StrSql = StrSql + " INJIN.CavPL10, "
                StrSql = StrSql + " INJIN.SD1, "
                StrSql = StrSql + " INJIN.SD2, "
                StrSql = StrSql + " INJIN.SD3, "
                StrSql = StrSql + " INJIN.SD4, "
                StrSql = StrSql + " INJIN.SD5, "
                StrSql = StrSql + " INJIN.SD6, "
                StrSql = StrSql + " INJIN.SD7, "
                StrSql = StrSql + " INJIN.SD8, "
                StrSql = StrSql + " INJIN.SD9, "
                StrSql = StrSql + " INJIN.SD10, "

                StrSql = StrSql + " INJIN.DryCT1, "
                StrSql = StrSql + " INJIN.DryCT2, "
                StrSql = StrSql + " INJIN.DryCT3, "
                StrSql = StrSql + " INJIN.DryCT4, "
                StrSql = StrSql + " INJIN.DryCT5, "
                StrSql = StrSql + " INJIN.DryCT6, "
                StrSql = StrSql + " INJIN.DryCT7, "
                StrSql = StrSql + " INJIN.DryCT8, "
                StrSql = StrSql + " INJIN.DryCT9, "
                StrSql = StrSql + " INJIN.DryCT10, "
                StrSql = StrSql + " INJIN.EjectTmp1, "
                StrSql = StrSql + " INJIN.EjectTmp2, "
                StrSql = StrSql + " INJIN.EjectTmp3, "
                StrSql = StrSql + " INJIN.EjectTmp4, "
                StrSql = StrSql + " INJIN.EjectTmp5, "
                StrSql = StrSql + " INJIN.EjectTmp6, "
                StrSql = StrSql + " INJIN.EjectTmp7, "
                StrSql = StrSql + " INJIN.EjectTmp8, "
                StrSql = StrSql + " INJIN.EjectTmp9, "
                StrSql = StrSql + " INJIN.EjectTmp10, "
                StrSql = StrSql + " INJIN.InjectTmp1, "
                StrSql = StrSql + " INJIN.InjectTmp2, "
                StrSql = StrSql + " INJIN.InjectTmp3, "
                StrSql = StrSql + " INJIN.InjectTmp4, "
                StrSql = StrSql + " INJIN.InjectTmp5, "
                StrSql = StrSql + " INJIN.InjectTmp6, "
                StrSql = StrSql + " INJIN.InjectTmp7, "
                StrSql = StrSql + " INJIN.InjectTmp8, "
                StrSql = StrSql + " INJIN.InjectTmp9, "
                StrSql = StrSql + " INJIN.InjectTmp10, "
                StrSql = StrSql + " INJIN.MoldTmp1, "
                StrSql = StrSql + " INJIN.MoldTmp2, "
                StrSql = StrSql + " INJIN.MoldTmp3, "
                StrSql = StrSql + " INJIN.MoldTmp4, "
                StrSql = StrSql + " INJIN.MoldTmp5, "
                StrSql = StrSql + " INJIN.MoldTmp6, "
                StrSql = StrSql + " INJIN.MoldTmp7, "
                StrSql = StrSql + " INJIN.MoldTmp8, "
                StrSql = StrSql + " INJIN.MoldTmp9, "
                StrSql = StrSql + " INJIN.MoldTmp10 ,  "
                'prefered Values of temp
                StrSql = StrSql + " NVL(INJA1.INJTEMPEJECT,0) EjectTmpp1, "
                StrSql = StrSql + " NVL(INJA2.INJTEMPEJECT,0) EjectTmpp2, "
                StrSql = StrSql + " NVL(INJA3.INJTEMPEJECT,0) EjectTmpp3, "
                StrSql = StrSql + " NVL(INJA4.INJTEMPEJECT,0) EjectTmpp4, "
                StrSql = StrSql + " NVL(INJA5.INJTEMPEJECT,0) EjectTmpp5, "
                StrSql = StrSql + " NVL(INJA6.INJTEMPEJECT,0) EjectTmpp6, "
                StrSql = StrSql + " NVL(INJA7.INJTEMPEJECT,0) EjectTmpp7, "
                StrSql = StrSql + " NVL(INJA8.INJTEMPEJECT,0) EjectTmpp8, "
                StrSql = StrSql + " NVL(INJA9.INJTEMPEJECT,0) EjectTmpp9, "
                StrSql = StrSql + " NVL(INJA10.INJTEMPEJECT,0) EjectTmpp10, "
                StrSql = StrSql + " NVL(INJA1.INJTEMPMELT,0) InjectTmpp1, "
                StrSql = StrSql + " NVL(INJA2.INJTEMPMELT,0) InjectTmpp2, "
                StrSql = StrSql + " NVL(INJA3.INJTEMPMELT,0) InjectTmpp3, "
                StrSql = StrSql + " NVL(INJA4.INJTEMPMELT,0) InjectTmpp4, "
                StrSql = StrSql + " NVL(INJA5.INJTEMPMELT,0) InjectTmpp5, "
                StrSql = StrSql + " NVL(INJA6.INJTEMPMELT,0) InjectTmpp6, "
                StrSql = StrSql + " NVL(INJA7.INJTEMPMELT,0) InjectTmpp7, "
                StrSql = StrSql + " NVL(INJA8.INJTEMPMELT,0) InjectTmpp8, "
                StrSql = StrSql + " NVL(INJA9.INJTEMPMELT,0) InjectTmpp9, "
                StrSql = StrSql + " NVL(INJA10.INJTEMPMELT,0) InjectTmpp10, "

                StrSql = StrSql + " NVL(INJA1.INJTEMPTOOL,0) MoldTmpp1, "
                StrSql = StrSql + " NVL(INJA2.INJTEMPTOOL,0) MoldTmpp2, "
                StrSql = StrSql + " NVL(INJA3.INJTEMPTOOL,0) MoldTmpp3, "
                StrSql = StrSql + " NVL(INJA4.INJTEMPTOOL,0) MoldTmpp4, "
                StrSql = StrSql + " NVL(INJA5.INJTEMPTOOL,0) MoldTmpp5, "
                StrSql = StrSql + " NVL(INJA6.INJTEMPTOOL,0) MoldTmpp6, "
                StrSql = StrSql + " NVL(INJA7.INJTEMPTOOL,0) MoldTmpp7, "
                StrSql = StrSql + " NVL(INJA8.INJTEMPTOOL,0) MoldTmpp8, "
                StrSql = StrSql + " NVL(INJA9.INJTEMPTOOL,0) MoldTmpp9, "
                StrSql = StrSql + " NVL(INJA10.INJTEMPTOOL,0)  MoldTmpp10, "
                StrSql = StrSql + " NVL(INJA1.STANDARDWALL,0) STANDARDWALL1, "
                StrSql = StrSql + " NVL(INJA2.STANDARDWALL,0) STANDARDWALL2, "
                StrSql = StrSql + " NVL(INJA3.STANDARDWALL,0) STANDARDWALL3, "
                StrSql = StrSql + " NVL(INJA4.STANDARDWALL,0) STANDARDWALL4, "
                StrSql = StrSql + " NVL(INJA5.STANDARDWALL,0) STANDARDWALL5, "
                StrSql = StrSql + " NVL(INJA6.STANDARDWALL,0) STANDARDWALL6, "
                StrSql = StrSql + " NVL(INJA7.STANDARDWALL,0) STANDARDWALL7, "
                StrSql = StrSql + " NVL(INJA8.STANDARDWALL,0) STANDARDWALL8, "
                StrSql = StrSql + " NVL(INJA9.STANDARDWALL,0) STANDARDWALL9, "
                StrSql = StrSql + " NVL(INJA10.STANDARDWALL,0)  STANDARDWALL10, "

                StrSql = StrSql + " INJIN.Nopress1,"
                StrSql = StrSql + " INJIN.Nopress2,"
                StrSql = StrSql + " INJIN.Nopress3,"
                StrSql = StrSql + " INJIN.Nopress4,"
                StrSql = StrSql + " INJIN.Nopress5,"
                StrSql = StrSql + " INJIN.Nopress6,"
                StrSql = StrSql + " INJIN.Nopress7,"
                StrSql = StrSql + " INJIN.Nopress8,"
                StrSql = StrSql + " INJIN.Nopress9,"
                StrSql = StrSql + " INJIN.Nopress10,"

                StrSql = StrSql + " INJIN.FixEnerLoad1,"
                StrSql = StrSql + " INJIN.FixEnerLoad2,"
                StrSql = StrSql + " INJIN.FixEnerLoad3,"
                StrSql = StrSql + " INJIN.FixEnerLoad4,"
                StrSql = StrSql + " INJIN.FixEnerLoad5,"
                StrSql = StrSql + " INJIN.FixEnerLoad6,"
                StrSql = StrSql + " INJIN.FixEnerLoad7,"
                StrSql = StrSql + " INJIN.FixEnerLoad8,"
                StrSql = StrSql + " INJIN.FixEnerLoad9,"
                StrSql = StrSql + " INJIN.FixEnerLoad10,"

                StrSql = StrSql + " INJIN.ProcessLoad1,"
                StrSql = StrSql + " INJIN.ProcessLoad2,"
                StrSql = StrSql + " INJIN.ProcessLoad3,"
                StrSql = StrSql + " INJIN.ProcessLoad4,"
                StrSql = StrSql + " INJIN.ProcessLoad5,"
                StrSql = StrSql + " INJIN.ProcessLoad6,"
                StrSql = StrSql + " INJIN.ProcessLoad7,"
                StrSql = StrSql + " INJIN.ProcessLoad8,"
                StrSql = StrSql + " INJIN.ProcessLoad9,"
                StrSql = StrSql + " INJIN.ProcessLoad10,"

                'preferences
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
                StrSql = StrSql + "PREF.TITLE13, "
                StrSql = StrSql + "PREF.TITLE14, "
                StrSql = StrSql + "PREF.TITLE15, "
                StrSql = StrSql + "PREF.TITLE16, "
                StrSql = StrSql + "PREF.CONVWT CONVWT, "
                StrSql = StrSql + "PREF.CONVWT2 CONVWT2, "
                StrSql = StrSql + "PREF.CONVWT3 CONVWT3, "
                StrSql = StrSql + "PREF.CONVWT4 CONVWT4, "
                StrSql = StrSql + "PREF.CONVTHICK CONVTHICK, "
                StrSql = StrSql + "PREF.CURR CURR, "
                StrSql = StrSql + "PREF.CONVAREA CONVAREA, "
                StrSql = StrSql + "PREF.CONVAREA3 CONVAREA3, "
                StrSql = StrSql + "PREF.CONVVOL CONVVOL, "
                StrSql = StrSql + "PREF.UNITS "

                StrSql = StrSql + "FROM InjectionInput INJIN  "
                'StrSql = StrSql + "inner JOIN materials MAT1 "
                StrSql = StrSql + "inner JOIN materials M1  ON INJIN.MATID1 = M1.matid  "
                StrSql = StrSql + "inner JOIN materials M2  ON INJIN.MATID2 = M2.matid "
                StrSql = StrSql + "inner JOIN materials M3  ON INJIN.MATID3 = M3.matid "
                StrSql = StrSql + "inner JOIN materials M4  ON INJIN.MATID4 = M4.matid "
                StrSql = StrSql + "inner JOIN materials M5  ON INJIN.MATID5 = M5.matid "
                StrSql = StrSql + "inner JOIN materials M6  ON INJIN.MATID6 = M6.matid "
                StrSql = StrSql + "inner JOIN materials M7  ON INJIN.MATID7 = M7.matid "
                StrSql = StrSql + "inner JOIN materials M8  ON INJIN.MATID8 = M8.matid "
                StrSql = StrSql + "inner JOIN materials M9  ON INJIN.MATID9 = M9.matid "
                StrSql = StrSql + "inner JOIN materials M10  ON INJIN.MATID10 = M10.matid "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID= " + CaseID.ToString()
                '------------Added on 24/01/2014
                StrSql = StrSql + " LEFT OUTER JOIN INJECTIONARCH INJA1 "
                StrSql = StrSql + "ON INJA1.EFFDATE = PREF.EFFDATE "
                StrSql = StrSql + "AND INJA1.MATID = M1.MATID "
                StrSql = StrSql + "LEFT OUTER JOIN INJECTIONARCH INJA2 "
                StrSql = StrSql + "ON INJA2.EFFDATE = PREF.EFFDATE "
                StrSql = StrSql + "AND INJA2.MATID = M2.MATID "
                StrSql = StrSql + "LEFT OUTER JOIN INJECTIONARCH INJA3 "
                StrSql = StrSql + "ON INJA3.EFFDATE = PREF.EFFDATE "
                StrSql = StrSql + "AND INJA3.MATID = M3.MATID "
                StrSql = StrSql + "LEFT OUTER JOIN INJECTIONARCH INJA4 "
                StrSql = StrSql + "ON INJA4.EFFDATE = PREF.EFFDATE "
                StrSql = StrSql + "AND INJA4.MATID = M4.MATID "
                StrSql = StrSql + "LEFT OUTER JOIN INJECTIONARCH INJA5 "
                StrSql = StrSql + "ON INJA5.EFFDATE = PREF.EFFDATE "
                StrSql = StrSql + "AND INJA5.MATID = M5.MATID "
                StrSql = StrSql + "LEFT OUTER JOIN INJECTIONARCH INJA6 "
                StrSql = StrSql + "ON INJA6.EFFDATE = PREF.EFFDATE "
                StrSql = StrSql + "AND INJA6.MATID = M6.MATID "
                StrSql = StrSql + "LEFT OUTER JOIN INJECTIONARCH INJA7 "
                StrSql = StrSql + "ON INJA7.EFFDATE = PREF.EFFDATE "
                StrSql = StrSql + "AND INJA7.MATID = M7.MATID "
                StrSql = StrSql + "LEFT OUTER JOIN INJECTIONARCH INJA8 "
                StrSql = StrSql + "ON INJA8.EFFDATE = PREF.EFFDATE "
                StrSql = StrSql + "AND INJA8.MATID = M8.MATID "
                StrSql = StrSql + "LEFT OUTER JOIN INJECTIONARCH INJA9 "
                StrSql = StrSql + "ON INJA9.EFFDATE = PREF.EFFDATE "
                StrSql = StrSql + "AND INJA9.MATID = M9.MATID "
                StrSql = StrSql + "LEFT OUTER JOIN INJECTIONARCH INJA10 "
                StrSql = StrSql + "ON INJA10.EFFDATE = PREF.EFFDATE "
                StrSql = StrSql + "AND INJA10.MATID = M10.MATID "
                StrSql = StrSql + "WHERE InjIn.CASEID =" + CaseID.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetInjectionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetMaterialByText(ByVal keyWord As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MATID,MATDE1, (MATDE1||' '||MATDE2)MatDES,MATDE2,PRICE,MATID,  "
                StrSql = StrSql + "SG, "
                StrSql = StrSql + "SERVERDATE, "
                StrSql = StrSql + "BW, "
                StrSql = StrSql + "THICK, "
                StrSql = StrSql + "MATDE3, "
                StrSql = StrSql + "INJMOLD, "
                StrSql = StrSql + "MELTDENSITY, "
                StrSql = StrSql + "INJTEMPMELT, "
                StrSql = StrSql + "INJTEMPTOOL, "
                StrSql = StrSql + "INJTEMPEJECT, "
                StrSql = StrSql + "EFFDIFFUSIVITY, "
                StrSql = StrSql + "CFFACTOR "
                StrSql = StrSql + "FROM MATERIALS "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "(UPPER(MATDE2) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(PRICE) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(MATID) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(SG) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(SERVERDATE) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(BW) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(THICK) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(MATDE3) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(INJMOLD) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(MELTDENSITY) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(INJTEMPMELT) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(INJTEMPTOOL) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(INJTEMPEJECT) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(EFFDIFFUSIVITY) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CFFACTOR) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + " ) AND INJMOLD=1 Or MATID=0"
                StrSql = StrSql + "ORDER BY  UPPER(MATDE2) "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetMaterialByText:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetInjectionEquiData(ByVal CaseID As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CLAMPSIZE,  "
                StrSql = StrSql + "INJRATEL, "
                StrSql = StrSql + "INJRATEH, "
                StrSql = StrSql + "INJRATEA, "
                StrSql = StrSql + "SHOTCAPL*PREF.CONVWT4 SHOTCAPL, "
                StrSql = StrSql + "SHOTCAPH*PREF.CONVWT4 SHOTCAPH, "
                StrSql = StrSql + "RATE, "
                StrSql = StrSql + "INVESTCOSTL, "
                StrSql = StrSql + "INVESTCOSTH, "
                StrSql = StrSql + "INVESTCOSTA, "
                StrSql = StrSql + "PREF.UNITS units, "
                StrSql = StrSql + "PREF.CURRENCY CURRENCY, "
                StrSql = StrSql + "PREF.TITLE2 TITLE2, "
                StrSql = StrSql + "PREF.TITLE14 TITLE14, "
                StrSql = StrSql + "PREF.CONVWT3 CONVWT3, "
                StrSql = StrSql + "PREF.CONVWT4 CONVWT4, "
                StrSql = StrSql + "PREF.CONVVOL CONVVOL, "
                StrSql = StrSql + "PREF.TITLE16 TITLE16, "
                StrSql = StrSql + "PREF.TITLE13 TITLE13 "
                StrSql = StrSql + "FROM INJECTIONEQUIPMENT "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID = " + CaseID.ToString()
                StrSql = StrSql + "ORDER BY  CLAMPSIZE ASC"
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetInjectionEquiData:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Ink Printing"
        Public Function GetColorsByText(ByVal keyWord As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT COLORID, (COLORID||'  '||COLORNAME)COLDES,COLORNAME,PMS,COLORNBR,  "
                StrSql = StrSql + "BCOLOR1, "
                StrSql = StrSql + "BCOLOR2, "
                StrSql = StrSql + "BCOLOR3, "
                StrSql = StrSql + "BCOLOR4, "
                StrSql = StrSql + "BCOLOR5, "
                StrSql = StrSql + "BCOLOR6, "
                StrSql = StrSql + "BCOLOR7, "
                StrSql = StrSql + "BCOLOR8, "
                StrSql = StrSql + "BCOLOR9, "
                StrSql = StrSql + "BCOLOR10, "
                StrSql = StrSql + "BCOLOR11 "
                StrSql = StrSql + "FROM COLORDETAILS "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "UPPER(COLORNAME) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(PMS) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(COLORNBR) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(BCOLOR1) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(BCOLOR2) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(BCOLOR3) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(BCOLOR4) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(BCOLOR5) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(BCOLOR6) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(BCOLOR7) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(BCOLOR8) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(BCOLOR9) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(BCOLOR10) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(BCOLOR11) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(COLORNAME) "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetColorsByText:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetColors(ByVal ColId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT COLORID, (COLORID||'  '||COLORNAME)COLDES,COLORNAME,PMS  "
                StrSql = StrSql + "FROM COLORDETAILS "
                StrSql = StrSql + "WHERE COLORNBR = CASE WHEN " + ColId.ToString() + " = -1 THEN "
                StrSql = StrSql + "COLORNBR "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "" + ColId.ToString() + " "
                StrSql = StrSql + "END "
                StrSql = StrSql + "ORDER BY  UPPER(COLORNAME) "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetColors:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetColorDetails(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                strsql = "SELECT  "
                StrSql = StrSql + "PREF.CASEID, "
                StrSql = StrSql + "COLOR1, "
                StrSql = StrSql + "COLOR2, "
                StrSql = StrSql + "COLOR3, "
                StrSql = StrSql + "COLOR4 , "
                StrSql = StrSql + "COLOR5, "
                StrSql = StrSql + "COLOR6 , "
                StrSql = StrSql + "COLOR7, "
                StrSql = StrSql + "COLOR8, "
                StrSql = StrSql + "COLOR9, "
                StrSql = StrSql + "COLOR10, "
                StrSql = StrSql + "COV1, "
                StrSql = StrSql + "COV2, "
                StrSql = StrSql + "COV3, "
                StrSql = StrSql + "COV4, "
                StrSql = StrSql + "COV5, "
                StrSql = StrSql + "COV6, "
                StrSql = StrSql + "COV7, "
                StrSql = StrSql + "COV8, "
                StrSql = StrSql + "COV9, "
                StrSql = StrSql + "COV10, "
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
                StrSql = StrSql + "PREF.TITLE13, "
                StrSql = StrSql + "PREF.CONVWT, "
                StrSql = StrSql + "PREF.CONVWT2, "
                StrSql = StrSql + "PREF.CONVAREA, "
                StrSql = StrSql + "PREF.UNITS "
                StrSql = StrSql + "FROM COLORINPUT "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=COLORINPUT.CASEID "
                StrSql = StrSql + "WHERE PREF.CASEID =" + CaseId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetColorDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetColorPrefDetails(ByVal UserName As String, ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ds As New DataSet()
            Try
                StrSql = "SELECT BASECOLORID,DATECOL  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 1 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 2 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 2 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 3 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 3 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 4 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 4 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 5 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 5 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 6 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 6 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 7 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 7 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 8 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 8 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 9 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 9 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 10 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 10 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT 11 BasecolorId,TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy') DATECOL  FROM BASECOLORARCH WHERE BASECOLORID = 11 AND "
                StrSql = StrSql + "EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY') "
                StrSql = StrSql + ") "
                StrSql = StrSql + "ORDER BY BASECOLORID ASC "
                ds = odbUtil.FillDataSet(StrSql, MedEcon1Connection)

                If ds.Tables(0).Rows.Count > 0 Then
                    StrSql = "SELECT B1.COLORNAME COLOR1,  "
                    StrSql = StrSql + "B2.COLORNAME COLOR2, "
                    StrSql = StrSql + "B3.COLORNAME COLOR3, "
                    StrSql = StrSql + "B4.COLORNAME COLOR4, "
                    StrSql = StrSql + "B5.COLORNAME COLOR5, "
                    StrSql = StrSql + "B6.COLORNAME COLOR6, "
                    StrSql = StrSql + "B7.COLORNAME COLOR7, "
                    StrSql = StrSql + "B8.COLORNAME COLOR8, "
                    StrSql = StrSql + "B9.COLORNAME COLOR9, "
                    StrSql = StrSql + "B10.COLORNAME COLOR10, "
                    StrSql = StrSql + "B11.COLORNAME COLOR11, "
                    'Bug#379# start
                    StrSql = StrSql + "(BA1.WETPRICE/PREF.CONVWT*PREF.CURR) WETPS1, "
                    StrSql = StrSql + "(BA2.WETPRICE/PREF.CONVWT*PREF.CURR) WETPS2, "
                    StrSql = StrSql + "(BA3.WETPRICE/PREF.CONVWT*PREF.CURR) WETPS3, "
                    StrSql = StrSql + "(BA4.WETPRICE/PREF.CONVWT*PREF.CURR) WETPS4, "
                    StrSql = StrSql + "(BA5.WETPRICE/PREF.CONVWT*PREF.CURR) WETPS5, "
                    StrSql = StrSql + "(BA6.WETPRICE/PREF.CONVWT*PREF.CURR) WETPS6, "
                    StrSql = StrSql + "(BA7.WETPRICE/PREF.CONVWT*PREF.CURR) WETPS7, "
                    StrSql = StrSql + "(BA8.WETPRICE/PREF.CONVWT*PREF.CURR) WETPS8, "
                    StrSql = StrSql + "(BA9.WETPRICE/PREF.CONVWT*PREF.CURR) WETPS9, "
                    StrSql = StrSql + "(BA10.WETPRICE/PREF.CONVWT*PREF.CURR) WETPS10, "
                    StrSql = StrSql + "(BA11.WETPRICE/PREF.CONVWT*PREF.CURR) WETPS11, "
                    'Bug#379# End
                    StrSql = StrSql + "(INKP.WETPRICE1/PREF.CONVWT*PREF.CURR) WETPP1, "
                    StrSql = StrSql + "(INKP.WETPRICE2/PREF.CONVWT*PREF.CURR) WETPP2, "
                    StrSql = StrSql + "(INKP.WETPRICE3/PREF.CONVWT*PREF.CURR) WETPP3, "
                    StrSql = StrSql + "(INKP.WETPRICE4/PREF.CONVWT*PREF.CURR) WETPP4, "
                    StrSql = StrSql + "(INKP.WETPRICE5/PREF.CONVWT*PREF.CURR) WETPP5, "
                    StrSql = StrSql + "(INKP.WETPRICE6/PREF.CONVWT*PREF.CURR) WETPP6, "
                    StrSql = StrSql + "(INKP.WETPRICE7/PREF.CONVWT*PREF.CURR) WETPP7, "
                    StrSql = StrSql + "(INKP.WETPRICE8/PREF.CONVWT*PREF.CURR) WETPP8, "
                    StrSql = StrSql + "(INKP.WETPRICE9/PREF.CONVWT*PREF.CURR) WETPP9, "
                    StrSql = StrSql + "(INKP.WETPRICE10/PREF.CONVWT*PREF.CURR) WETPP10, "
                    StrSql = StrSql + "(INKP.WETPRICE11/PREF.CONVWT*PREF.CURR) WETPP11, "
                    'Bug#379# Start
                    StrSql = StrSql + "BA1.PERSOL PERSOLS1, "
                    StrSql = StrSql + "BA2.PERSOL PERSOLS2, "
                    StrSql = StrSql + "BA3.PERSOL PERSOLS3, "
                    StrSql = StrSql + "BA4.PERSOL PERSOLS4, "
                    StrSql = StrSql + "BA5.PERSOL PERSOLS5, "
                    StrSql = StrSql + "BA6.PERSOL PERSOLS6, "
                    StrSql = StrSql + "BA7.PERSOL PERSOLS7, "
                    StrSql = StrSql + "BA8.PERSOL PERSOLS8, "
                    StrSql = StrSql + "BA9.PERSOL PERSOLS9, "
                    StrSql = StrSql + "BA10.PERSOL PERSOLS10, "
                    StrSql = StrSql + "BA11.PERSOL PERSOLS11, "
                    'Bug#379# End
                    StrSql = StrSql + "INKP.PERSOL1 PERSOLP1, "
                    StrSql = StrSql + "INKP.PERSOL2 PERSOLP2, "
                    StrSql = StrSql + "INKP.PERSOL3 PERSOLP3, "
                    StrSql = StrSql + "INKP.PERSOL4 PERSOLP4, "
                    StrSql = StrSql + "INKP.PERSOL5 PERSOLP5, "
                    StrSql = StrSql + "INKP.PERSOL6 PERSOLP6, "
                    StrSql = StrSql + "INKP.PERSOL7 PERSOLP7, "
                    StrSql = StrSql + "INKP.PERSOL8 PERSOLP8, "
                    StrSql = StrSql + "INKP.PERSOL9 PERSOLP9, "
                    StrSql = StrSql + "INKP.PERSOL10 PERSOLP10, "
                    StrSql = StrSql + "INKP.PERSOL11 PERSOLP11, "
                    'Bug#379# Start
                    StrSql = StrSql + "BA1.SGRAVITY SGRAVITYS1, "
                    StrSql = StrSql + "BA2.SGRAVITY SGRAVITYS2, "
                    StrSql = StrSql + "BA3.SGRAVITY SGRAVITYS3, "
                    StrSql = StrSql + "BA4.SGRAVITY SGRAVITYS4, "
                    StrSql = StrSql + "BA5.SGRAVITY SGRAVITYS5, "
                    StrSql = StrSql + "BA6.SGRAVITY SGRAVITYS6, "
                    StrSql = StrSql + "BA7.SGRAVITY SGRAVITYS7, "
                    StrSql = StrSql + "BA8.SGRAVITY SGRAVITYS8, "
                    StrSql = StrSql + "BA9.SGRAVITY SGRAVITYS9, "
                    StrSql = StrSql + "BA10.SGRAVITY SGRAVITYS10, "
                    StrSql = StrSql + "BA11.SGRAVITY SGRAVITYS11, "
                    'Bug#379# End
                    StrSql = StrSql + "INKP.SGRAVITY1 SGRAVITYP1, "
                    StrSql = StrSql + "INKP.SGRAVITY2 SGRAVITYP2, "
                    StrSql = StrSql + "INKP.SGRAVITY3 SGRAVITYP3, "
                    StrSql = StrSql + "INKP.SGRAVITY4 SGRAVITYP4, "
                    StrSql = StrSql + "INKP.SGRAVITY5 SGRAVITYP5, "
                    StrSql = StrSql + "INKP.SGRAVITY6 SGRAVITYP6, "
                    StrSql = StrSql + "INKP.SGRAVITY7 SGRAVITYP7, "
                    StrSql = StrSql + "INKP.SGRAVITY8 SGRAVITYP8, "
                    StrSql = StrSql + "INKP.SGRAVITY9 SGRAVITYP9, "
                    StrSql = StrSql + "INKP.SGRAVITY10 SGRAVITYP10, "
                    StrSql = StrSql + "INKP.SGRAVITY11 SGRAVITYP11, "
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
                    StrSql = StrSql + "FROM INKPREFERENCES INKP "
                    StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                    StrSql = StrSql + "ON PREF.CASEID=" + CaseId + " "
                    StrSql = StrSql + "INNER JOIN BASECOLOR B1 "
                    StrSql = StrSql + "ON B1.BASECOLORID=1 "
                    StrSql = StrSql + "INNER JOIN BASECOLOR B2 "
                    StrSql = StrSql + "ON B2.BASECOLORID=2 "
                    StrSql = StrSql + "INNER JOIN BASECOLOR B3 "
                    StrSql = StrSql + "ON B3.BASECOLORID=3 "
                    StrSql = StrSql + "INNER JOIN BASECOLOR B4 "
                    StrSql = StrSql + "ON B4.BASECOLORID=4 "
                    StrSql = StrSql + "INNER JOIN BASECOLOR B5 "
                    StrSql = StrSql + "ON B5.BASECOLORID=5 "
                    StrSql = StrSql + "INNER JOIN BASECOLOR B6 "
                    StrSql = StrSql + "ON B6.BASECOLORID=6 "
                    StrSql = StrSql + "INNER JOIN BASECOLOR B7 "
                    StrSql = StrSql + "ON B7.BASECOLORID=7 "
                    StrSql = StrSql + "INNER JOIN BASECOLOR B8 "
                    StrSql = StrSql + "ON B8.BASECOLORID=8 "
                    StrSql = StrSql + "INNER JOIN BASECOLOR B9 "
                    StrSql = StrSql + "ON B9.BASECOLORID=9 "
                    StrSql = StrSql + "INNER JOIN BASECOLOR B10 "
                    StrSql = StrSql + "ON B10.BASECOLORID=10 "
                    StrSql = StrSql + "INNER JOIN BASECOLOR B11 "
                    StrSql = StrSql + "ON B11.BASECOLORID=11 "
                    'Bug#379# Start
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA1 "
                    'StrSql = StrSql + "ON BA1.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 1 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA1.BASECOLORID = 1 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA2 "
                    'StrSql = StrSql + "ON BA2.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 2 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA2.BASECOLORID = 2 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA3 "
                    'StrSql = StrSql + "ON BA3.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 3 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA3.BASECOLORID = 3 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA4 "
                    'StrSql = StrSql + "ON BA4.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 4 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA4.BASECOLORID = 4 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA5 "
                    'StrSql = StrSql + "ON BA5.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 5 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA5.BASECOLORID = 5 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA6 "
                    'StrSql = StrSql + "ON BA6.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 6 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA6.BASECOLORID = 6 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA7 "
                    'StrSql = StrSql + "ON BA7.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 7 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA7.BASECOLORID = 7 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA8 "
                    'StrSql = StrSql + "ON BA8.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 8 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA8.BASECOLORID = 8 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA9 "
                    'StrSql = StrSql + "ON BA9.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 9 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA9.BASECOLORID = 9 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA10 "
                    'StrSql = StrSql + "ON BA10.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 10 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA10.BASECOLORID = 10 "
                    'StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA11 "
                    'StrSql = StrSql + "ON BA11.EFFDATE = TO_DATE((SELECT TO_CHAR(MAX(EFFDATE),'mm/dd/yyyy')  FROM BASECOLORARCH WHERE BASECOLORID = 11 AND EFFDATE<=TO_DATE( (SELECT TO_CHAR(EFFDATE,'mm/dd/yyyy')  FROM PREFERENCES WHERE CASEID=" + CaseId + ") ,'MM//DD/YYYY')),'MM/DD/YYYY')  "
                    'StrSql = StrSql + "AND BA11.BASECOLORID = 11"
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA1 "
                    StrSql = StrSql + "ON BA1.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(0).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA1.BASECOLORID = 1 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA2 "
                    StrSql = StrSql + "ON BA2.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(1).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA2.BASECOLORID = 2 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA3 "
                    StrSql = StrSql + "ON BA3.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(2).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA3.BASECOLORID = 3 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA4 "
                    StrSql = StrSql + "ON BA4.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(3).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA4.BASECOLORID = 4 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA5 "
                    StrSql = StrSql + "ON BA5.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(4).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA5.BASECOLORID = 5 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA6 "
                    StrSql = StrSql + "ON BA6.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(5).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA6.BASECOLORID = 6 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA7 "
                    StrSql = StrSql + "ON BA7.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(6).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA7.BASECOLORID = 7 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA8 "
                    StrSql = StrSql + "ON BA8.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(7).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA8.BASECOLORID = 8 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA9 "
                    StrSql = StrSql + "ON BA9.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(8).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA9.BASECOLORID = 9 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA10 "
                    StrSql = StrSql + "ON BA10.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(9).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA10.BASECOLORID = 10 "
                    StrSql = StrSql + "LEFT OUTER JOIN BASECOLORARCH BA11 "
                    StrSql = StrSql + "ON BA11.EFFDATE = TO_DATE('" + ds.Tables(0).Rows(10).Item("DATECOL").ToString() + "','MM/DD/YYYY')  "
                    StrSql = StrSql + "AND BA11.BASECOLORID = 11"
                    'Bug#379# end
                    StrSql = StrSql + "WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "' "

                    Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                End If
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetColorPrefDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Bemis"
        Public Function GetStatusForSister(ByVal Schema As String, ByVal CaseId As String, ByVal userName As String) As DataSet
            Dim Dts As New DataSet()
            Dim StrSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Dim CaseIds As String = String.Empty
            Dim con As String = ""
            Dim MedEcon1Connection As String = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
            Try
                If Schema = "E1" Then
                    con = MedEcon1Connection
                End If
                StrSql = "SELECT CASEID FROM STATUSUPDATE WHERE "
                StrSql = StrSql + "CASEID = " + CaseId + " AND UPPER(USERNAME)='" + userName.ToUpper() + "' AND UPPER(STATUS)='SISTER CASE' ORDER BY DATED ASC"

                Dts = odbUtil.FillDataSet(StrSql, con)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetStatusDetailsByID:" + ex.Message.ToString())
            End Try
        End Function
        Public Function GetModuleType(ByVal UserName As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MODELTID FROM ECON.USERS  "
                StrSql = StrSql + "INNER JOIN LICENSEMASTER "
                StrSql = StrSql + "ON USERS.LICENSEID=LICENSEMASTER.LICENSEID "
                StrSql = StrSql + "WHERE UPPER(USERNAME)='" + UserName.ToUpper().ToString() + "' "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetModuleType:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetPCaseGrpDetailsBem(ByVal UserName As String, ByVal UserID As String, ByVal keyWord As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim CaseIds As String = ""
            Try
                CaseIds = GetPropCaseStatus(UserName)

                StrSql = "SELECT GROUPNAME,GROUPID,CASEID,CASEDE1,CASEDE2,CASEDE3,CASEDES,CREATIONDATE,SERVERDATE "
                StrSql = StrSql + " FROM "
                StrSql = StrSql + " ( "
                StrSql = StrSql + "SELECT CASE WHEN NVL(GROUPS.GROUPID,'0')='0' THEN 'None' ELSE GROUPS.DES1 END AS GROUPNAME,  "
                StrSql = StrSql + "NVL(GROUPS.GROUPID,'0') AS GROUPID, "
                StrSql = StrSql + "PC.CASEID, "
                StrSql = StrSql + "PC.CASEDE1, "
                StrSql = StrSql + "PC.CASEDE2, "
                StrSql = StrSql + "CAST(PC.CASEDE3 AS VARCHAR(4000))CASEDE3, "
                StrSql = StrSql + "(PC.CASEID||'. PACKAGE FORMAT= '||PC.CASEDE1||' UNIQUE FEATURES= '||PC.CASEDE2)CASEDES, "
                StrSql = StrSql + " PC.CREATIONDATE, "
                StrSql = StrSql + "CASE WHEN PC.CREATIONDATE-PC.SERVERDATE =0 THEN 'NA' ELSE to_char(PC.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE "
                StrSql = StrSql + "FROM GROUPS "
                StrSql = StrSql + "INNER JOIN GROUPCASES "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
                StrSql = StrSql + "AND GROUPS.USERID=" + UserID + " "
                StrSql = StrSql + "RIGHT OUTER JOIN PERMISSIONSCASES PC "
                StrSql = StrSql + "ON PC.CASEID=GROUPCASES.CASEID "
                StrSql = StrSql + "WHERE PC.USERID =" + UserID + " "
                StrSql = StrSql + "AND NVL(PC.STATUSID,0) NOT IN(3,4) "

                StrSql = StrSql + "UNION ALL "

                StrSql = StrSql + "SELECT CASE WHEN NVL(GROUPS.GROUPID,'0')='0' THEN 'None' ELSE GROUPS.DES1 END AS GROUPNAME,  "
                StrSql = StrSql + "NVL(GROUPS.GROUPID,'0') AS GROUPID, "
                StrSql = StrSql + "PC.CASEID, "
                StrSql = StrSql + "PC.CASEDE1, "
                StrSql = StrSql + "PC.CASEDE2, "
                StrSql = StrSql + "CAST(PC.CASEDE3 AS VARCHAR(4000))CASEDE3, "
                StrSql = StrSql + "(PC.CASEID||'. PACKAGE FORMAT= '||PC.CASEDE1||' UNIQUE FEATURES= '||PC.CASEDE2)CASEDES, "
                StrSql = StrSql + " PC.CREATIONDATE, "
                StrSql = StrSql + "CASE WHEN PC.CREATIONDATE-PC.SERVERDATE =0 THEN 'NA' ELSE to_char(PC.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE "
                StrSql = StrSql + "FROM GROUPS "
                StrSql = StrSql + "INNER JOIN GROUPCASES "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
                StrSql = StrSql + "AND GROUPS.USERID=" + UserID + " "
                StrSql = StrSql + "RIGHT OUTER JOIN PERMISSIONSCASES PC "
                StrSql = StrSql + "ON PC.CASEID=GROUPCASES.CASEID "
                StrSql = StrSql + "WHERE PC.USERID =" + UserID + " "
                StrSql = StrSql + "AND PC.CASEID in(" + CaseIds + ") "

                StrSql = StrSql + " ) "

                StrSql = StrSql + "WHERE UPPER(CASEDE1) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE2) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEID) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE3) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(GROUPNAME) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "ORDER BY CASEID DESC "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetPCaseGrpDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetPropCasesById(ByVal UserName As String, ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = StrSql + "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,MS.STATUS,PERMISSIONSCASES.STATUSID,USERNAME CaseOwner "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "LEFT OUTER JOIN MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                StrSql = StrSql + "WHERE UPPER(USERNAME) ='" + UserName.ToUpper().ToString() + "' "
                StrSql = StrSql + "AND CASEID= " + CaseId + " "
                StrSql = StrSql + "AND NVL(PERMISSIONSCASES.STATUSID,0) NOT IN(3,4) "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetPropCasesById:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetGroupCaseDet(ByVal UserID As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New Med1GetData.Selectdata()
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim strSQL As String = String.Empty
            Dim strSQL2 As String = String.Empty
            Dim ds As New DataSet()
            Dim CaseIDs As String = String.Empty
            Dim DtRes As New DataSet()
            Dim cnt As Integer = 0
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
                Dts = objGetData.GetGroupIDByUSer(UserID)
                If Dts.Tables(0).Rows.Count > 0 Then
                    For i = 0 To Dts.Tables(0).Rows.Count - 1
                        ds = objGetData.GetGroupCasesByUSer(Dts.Tables(0).Rows(i).Item("GROUPID").ToString())
                        If ds.Tables(0).Rows.Count > 0 Then
                            For j = 0 To ds.Tables(0).Rows.Count - 1
                                If j = 0 Then
                                    CaseIDs = ds.Tables(0).Rows(j).Item("CASEID").ToString()
                                Else
                                    CaseIDs = CaseIDs + " " + ds.Tables(0).Rows(j).Item("CASEID").ToString()
                                End If
                            Next
                        Else
                            CaseIDs = "0"
                        End If
                        If cnt = 0 Then
                            cnt += 1
                            'strSQL = "SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' || ' ,Cases:" + CaseIDs + "' AS GROUPDES,'" + CaseIDs + "' AS CASEIDS FROM DUAL "
                            strSQL = "SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '   ' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '   '|| '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "'  AS GROUPDES,'" + CaseIDs + "' AS CASEIDS,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS DES1,'" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS DES2,'" + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + "' || ':' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS CDES1 FROM DUAL "
                        Else
                            cnt += 1
                            strSQL = strSQL + "UNION ALL SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' ||  '   ' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '   '||  '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "'  AS GROUPDES,'" + CaseIDs + "' AS CASEIDS,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS DES1,'" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS DES2,'" + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + "' || ':' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS CDES1 FROM DUAL "
                        End If
                    Next
                    strSQL = "SELECT * FROM (" + strSQL + ") DUAL "
                End If
                strSQL2 = "SELECT 0 GROUPID,'' AS GROUPNAME,'All Groups and All Cases'  AS GROUPDES,'' AS CASEIDS,'' AS DES1,'' AS DES2,'' AS CDES1 FROM DUAL "
                If strSQL <> "" Then
                    strSQL2 = strSQL2 + " UNION ALL " + strSQL
                    strSQL2 = "SELECT GROUPID,GROUPNAME,GROUPDES,CASEIDS,DES1,DES2,CDES1 FROM (" + strSQL2 + " )ORDER BY UPPER(DES1),UPPER(DES2) "
                    DtRes = odbUtil.FillDataSet(strSQL2, MyConnectionString)
                Else
                    strSQL2 = strSQL2 + ""
                    DtRes = odbUtil.FillDataSet(strSQL2, MyConnectionString)
                End If
                Return DtRes
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function
        Public Function GetApprovedCases(ByVal UserName As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  CASEID, CASEDES,CASEDE1, CASEDE2,CASEDE, STATUS,STATUSID,CaseOwner  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "(SELECT 0 CASEID, 'Select Case' CASEDES, 'Select Case' CASEDE1, ''CASEDE2, 'Select Case' CASEDE, '' STATUS,0 STATUSID,'None'  CaseOwner "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,MS.STATUS,PERMISSIONSCASES.STATUSID,USERS.UserName CaseOwner "
                StrSql = StrSql + "FROM PERMISSIONSCASES "

                StrSql = StrSql + "INNER JOIN ECON.USERS "
                StrSql = StrSql + "ON USERS.USERID=PERMISSIONSCASES.USERID "


                StrSql = StrSql + "INNER JOIN MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "

                StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "') "
                ' StrSql = StrSql + "WHERE UPPER(USERNAME) ='" + UserName.ToUpper().ToString() + "' "

                StrSql = StrSql + "AND PERMISSIONSCASES.STATUSID=3) "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetAppCasesByLicense(ByVal UserName As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  CASEID, CASEDE1, CASEDE2, CASEDE3,CASEDE,CASEDES, STATUS,STATUSID,CaseOwner  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "(SELECT 0 CASEID,  'Select Case' CASEDE1, ''CASEDE2, ''CASEDE3,'Select Case' CASEDE,'Select Case' CASEDES, '' STATUS,0 STATUSID,'None'  CaseOwner "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION ALL "

                StrSql = StrSql + "SELECT DISTINCT CASEID,  "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "CASEDE2, "
                StrSql = StrSql + "CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,(CASEDE1||'  '||CASEDE2)CASEDES, "
                StrSql = StrSql + "MS.STATUS,PERMISSIONSCASES.STATUSID ,USERS.USERNAME CaseOwner "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "INNER JOIN MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                StrSql = StrSql + "INNER JOIN ECON.USERS "
                StrSql = StrSql + "ON USERS.USERID=PERMISSIONSCASES.USERID "
                StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "') "

                StrSql = StrSql + "AND PERMISSIONSCASES.STATUSID=3) "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetCasesByLicense:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetPropCases(ByVal UserName As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim CaseIds As String = ""
            Try
                CaseIds = GetPropCaseStatus(UserName)

                StrSql = "SELECT  CASEID, CASEDES,CASEDE1, CASEDE2,CASEDE, STATUS,STATUSID,CaseOwner  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "(SELECT 0 CASEID, 'Select Case' CASEDES, 'Select Case' CASEDE1, ''CASEDE2, 'Select Case' CASEDE, '' STATUS,0 STATUSID,'None'  CaseOwner "
                StrSql = StrSql + "FROM DUAL "

                StrSql = StrSql + "UNION ALL "


                StrSql = StrSql + "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,MS.STATUS,PERMISSIONSCASES.STATUSID,USERS.USERNAME CaseOwner "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "LEFT OUTER JOIN MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                StrSql = StrSql + "INNER JOIN ECON.USERS "
                StrSql = StrSql + "ON USERS.USERID=PERMISSIONSCASES.USERID "
                StrSql = StrSql + "WHERE USERS.USERID IN (SELECT USERS.LICENSEID FROM ECON.USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "') "
                'StrSql = StrSql + "WHERE UPPER(USERNAME) ='" + UserName.ToUpper().ToString() + "' "
                'StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                'StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(PERMISSIONSCASES.STATUSID,0) NOT IN(3,4) "



                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,MS.STATUS,PERMISSIONSCASES.STATUSID,USERS.USERNAME CaseOwner "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "LEFT OUTER JOIN MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                StrSql = StrSql + "INNER JOIN ECON.USERS "
                StrSql = StrSql + "ON USERS.USERID=PERMISSIONSCASES.USERID "
                StrSql = StrSql + "WHERE CaseId in(" + CaseIds + ") "
                StrSql = StrSql + "AND USERS.USERID IN (SELECT USERS.LICENSEID FROM ECON.USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "')  "


                StrSql = StrSql + ") "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetPropCaseStatus(ByVal UserName As String) As String
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim CaseIds As String = ""
            Dim ds As New DataSet()
            Dim i As Integer = 0
            Dim cnt As Integer = 0
            Try
                StrSql = StrSql + "SELECT CASEID "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE STATUSID=4 AND USERID IN (SELECT USERS.LICENSEID FROM ECON.USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "') "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                If Dts.Tables(0).Rows.Count > 0 Then
                    For i = 0 To Dts.Tables(0).Rows.Count - 1
                        ds = GetPropDisAppStatus(UserName, Dts.Tables(0).Rows(i).Item("CaseId").ToString())
                        If ds.Tables(0).Rows.Count = 0 Then
                            If cnt = 0 Then
                                CaseIds = Dts.Tables(0).Rows(0).Item("CaseId").ToString()
                            Else
                                CaseIds = CaseIds + "," + Dts.Tables(0).Rows(i).Item("CaseId").ToString()
                            End If
                            cnt += 1
                        End If
                    Next

                Else
                    CaseIds = "0"
                End If
                Return CaseIds
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetCases:" + ex.Message.ToString())
                Return CaseIds
            End Try
        End Function
        Public Function GetPropDisAppStatus(ByVal UserName As String, ByVal CaseId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim CaseIds As String = ""
            Try
                StrSql = StrSql + "SELECT * "
                StrSql = StrSql + "FROM STATUSUPDATE "
                StrSql = StrSql + "WHERE USERID IN (SELECT USERS.LICENSEID FROM ECON.USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "') AND CASEID=" + CaseId.ToString() + " AND UPPER(STATUS)='APPROVED' "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                If Dts.Tables(0).Rows.Count > 0 Then

                Else
                    CaseIds = "0"
                End If
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetPropCasesByLicense(ByVal UserName As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim StrSqlN As String = String.Empty
            Dim CaseIds As String = ""

            Dim ds As New DataSet()
            Dim dsUsers As New DataSet()
            Dim i As Integer = 0
            Dim cnt As Integer = 0
            Dim j As Integer = 0

            Try
                'CaseIds = GetPropLicCaseStatus(UserName)

                StrSql = "SELECT  CASEID, CASEDE1, CASEDE2, CASEDE3,CASEDE,CASEDES, STATUS,STATUSID,CaseOwner  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "(SELECT 0 CASEID,  'Select Case' CASEDE1, ''CASEDE2, ''CASEDE3,'Select Case' CASEDE,'Select Case' CASEDES, '' STATUS,0 STATUSID,'None'  CaseOwner "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT CASEID,  "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "CASEDE2, "
                StrSql = StrSql + "CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,(CASEDE1||'  '||CASEDE2)CASEDES, "
                StrSql = StrSql + "MS.STATUS,PERMISSIONSCASES.STATUSID,USERS.USERNAME CaseOwner  "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "LEFT OUTER JOIN MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                StrSql = StrSql + "INNER JOIN ECON.USERS "
                StrSql = StrSql + "ON USERS.USERID=PERMISSIONSCASES.USERID "
                StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "') "

                StrSql = StrSql + "AND NVL(PERMISSIONSCASES.STATUSID,0) NOT IN(3) "



                'dsUsers = GetUserCompanyUsersBem(UserName)
                'If dsUsers.Tables(0).Rows.Count > 0 Then
                '    For j = 0 To dsUsers.Tables(0).Rows.Count - 1
                '        StrSqlN = "SELECT CASEID "
                '        StrSqlN = StrSqlN + "FROM PERMISSIONSCASES "
                '        StrSqlN = StrSqlN + "WHERE STATUSID=4 AND UPPER(USERNAME)='" + dsUsers.Tables(0).Rows(j).Item("USERNAME").ToString().ToUpper() + "' "
                '        Dts = odbUtil.FillDataSet(StrSqlN, MedEcon1Connection)
                '        If Dts.Tables(0).Rows.Count > 0 Then
                '            For i = 0 To Dts.Tables(0).Rows.Count - 1
                '                ds = GetPropDisAppStatus(dsUsers.Tables(0).Rows(j).Item("USERNAME").ToString(), Dts.Tables(0).Rows(i).Item("CaseId").ToString())
                '                If ds.Tables(0).Rows.Count = 0 Then
                '                    StrSql = StrSql + "UNION ALL "
                '                    StrSql = StrSql + "SELECT CASEID,  "
                '                    StrSql = StrSql + "CASEDE1, "
                '                    StrSql = StrSql + "CASEDE2, "
                '                    StrSql = StrSql + "CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,(CASEDE1||'  '||CASEDE2)CASEDES, "
                '                    StrSql = StrSql + "MS.STATUS,PERMISSIONSCASES.STATUSID,USERNAME CaseOwner  "
                '                    StrSql = StrSql + "FROM PERMISSIONSCASES "
                '                    StrSql = StrSql + "LEFT OUTER JOIN MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                '                    StrSql = StrSql + "WHERE CaseId =" + Dts.Tables(0).Rows(i).Item("CaseId").ToString() + " AND UPPER(USERNAME)='" + dsUsers.Tables(0).Rows(j).Item("USERNAME").ToString().ToUpper() + "' "
                '                End If
                '            Next
                '        End If
                '    Next
                'End If


                StrSql = StrSql + ") "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetPropCasesByLicense:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetStatusDetailsByID(ByVal CaseId As String, ByVal UserName As String) As DataSet
            Dim Dts As New DataSet()
            Dim StrSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Dim CaseIds As String = String.Empty
            Try
                StrSql = "SELECT CASEID,STATUS,DATED,ACTIONBY,COMMENTS FROM STATUSUPDATE WHERE "
                StrSql = StrSql + "CASEID = " + CaseId + " AND UPPER(USERNAME)='" + UserName.ToUpper() + "' ORDER BY DATED ASC "

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetStatusDetailsByID:" + ex.Message.ToString())
            End Try
        End Function
        Public Function GetGrpStatusDetailsByID(ByVal CaseId As String, ByVal UserName As String) As DataSet
            Dim Dts As New DataSet()
            Dim StrSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Dim CaseIds As String = String.Empty
            Try
                StrSql = "SELECT CASEID,STATUS,DATED,ACTIONBY,COMMENTS FROM STATUSUPDATE WHERE "
                StrSql = StrSql + "CASEID = " + CaseId + " AND UPPER(USERNAME)='" + UserName.ToUpper() + "' "

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetStatusDetailsByID:" + ex.Message.ToString())
            End Try
        End Function
        Public Function GetPermissionStatus(ByVal CaseId As Integer, ByVal UserName As String) As DataSet
            Try
                Dim Dts As New DataSet()
                Dim odbUtil As New DBUtil()
                Dim StrSql As String = ""
                StrSql = ""
                StrSql = "SELECT CASEID,STATUS,DATED,ACTIONBY,COMMENTS FROM STATUSUPDATE "
                StrSql = StrSql + "WHERE CASEID=" + CaseId.ToString() + " AND UPPER(ACTIONBY)='" + UserName.ToUpper() + "' ORDER BY DATED ASC "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1Data:GetPermissionStatus:" + ex.Message.ToString())
            End Try
        End Function
        Public Function GetGroupPCases(ByVal UserId As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String, ByVal groupID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  CASEID, CASEDES,CASEDE1, CASEDE2,CASEDE, STATUS,STATUSID,CaseOwner  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "(SELECT 0 CASEID, 'Select Case' CASEDES, 'Select Case' CASEDE1, ''CASEDE2, 'Select Case' CASEDE, '' STATUS,0 STATUSID,'None'  CaseOwner "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE,MS.STATUS,PERMISSIONSCASES.STATUSID,USERNAME CaseOwner "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "LEFT OUTER JOIN MODSTATUS MS ON MS.STATUSID=PERMISSIONSCASES.STATUSID "
                StrSql = StrSql + "WHERE USERID =" + UserId + " "

                StrSql = StrSql + "AND CASEID IN(SELECT CASEID FROM MED1.GROUPCASES WHERE GROUPID=" + groupID + " )) "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUserCompanyUsersBem(ByVal UserName As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERID,USERNAME FROM (  "

                StrSql = StrSql + "SELECT  0 USERID,'Select User' USERNAME FROM DUAL  "
                StrSql = StrSql + "UNION ALL  "
                StrSql = StrSql + "SELECT  USERS.USERID,  "
                StrSql = StrSql + "USERS.USERNAME "
                StrSql = StrSql + "FROM ECON.USERS "
                StrSql = StrSql + "INNER JOIN ECON.USERPERMISSIONS "
                StrSql = StrSql + "ON USERPERMISSIONS.USERID= USERS.USERID "
                StrSql = StrSql + "INNER JOIN ECON.SERVICES "
                StrSql = StrSql + "ON SERVICES.SERVICEID = USERPERMISSIONS.SERVICEID "
                StrSql = StrSql + "WHERE USERS.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper.ToString() + "') "
                StrSql = StrSql + "AND SERVICES.SERVICEDE IN ('MedE1','MedE2','MedS1','MedS2') "
                StrSql = StrSql + "AND UPPER(USERPERMISSIONS.USERROLE)='READWRITE' "
                StrSql = StrSql + "GROUP BY USERS.USERID,USERS.USERNAME) "
                StrSql = StrSql + "ORDER BY USERNAME "

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetUserCompanyUsersBem:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Barrier Grade"
        Public Function GetGradesVal(ByVal GRADEID As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty

            Try
                StrSql = "SELECT GRADE.GRADEID,GRADE.GRADENAME FROM GRADE WHERE GRADEID =" + GRADEID.ToString()
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetGradesVal:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetGrades(ByVal MatId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty

            Try
                If MatId = "0" Then
                    StrSql = "SELECT GRADE.GRADEID,GRADE.GRADENAME,0 SG,0 WEIGHT FROM GRADE JOIN MATGRADE ON GRADE.GRADEID=MATGRADE.GRADEID AND MATID =" + MatId.ToString()
                Else
                    StrSql = "SELECT  0 GRADEID, 'Nothing Selected' GRADENAME,0 SG,0 WEIGHT FROM DUAL UNION ALL SELECT GRADE.GRADEID,GRADE.GRADENAME,MAT.SG,GRADE.WEIGHT FROM GRADE "
                    StrSql = StrSql + "INNER JOIN MATGRADE ON GRADE.GRADEID=MATGRADE.GRADEID AND MATID =" + MatId.ToString()
                    StrSql = StrSql + "INNER JOIN MATERIALS MAT  "
                    StrSql = StrSql + "ON MAT.MATID=MATGRADE.MATID "

                End If

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetGrades:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetMatGrades(ByVal MatId As Integer, ByVal GradeId As Integer) As Integer
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty

            Try
                StrSql = "SELECT GRADE.GRADEID,GRADE.GRADENAME,MAT.SG,GRADE.WEIGHT FROM GRADE "
                StrSql = StrSql + "INNER JOIN MATGRADE ON GRADE.GRADEID=MATGRADE.GRADEID AND MATGRADE.MATID =" + MatId.ToString() + " AND MATGRADE.GRADEID=" + GradeId.ToString() + " "
                StrSql = StrSql + "INNER JOIN MATERIALS MAT  "
                StrSql = StrSql + "ON MAT.MATID=MATGRADE.MATID "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                If Dts.Tables(0).Rows.Count > 0 Then
                    Return GradeId
                Else
                    StrSql = "SELECT GRADE.GRADEID FROM MATGRADE  "
                    StrSql = StrSql + "INNER JOIN GRADE "
                    StrSql = StrSql + "ON GRADE.GRADEID=MATGRADE.GRADEID "
                    StrSql = StrSql + "WHERE MATGRADE.MATID= " + MatId.ToString() + " "
                    StrSql = StrSql + "AND GRADE.ISDEFAULT='Y' "
                    Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                    If Dts.Tables(0).Rows.Count > 0 Then
                        Return CInt(Dts.Tables(0).Rows(0).Item("GRADEID"))
                    End If
                End If

            Catch ex As Exception
            End Try
        End Function
        Public Function GetExtrusionDetailsBarrD(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
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
                StrSql = StrSql + "NVL(GRADE1,0) GRADE1, "
                StrSql = StrSql + "NVL(GRADE2,0) GRADE2, "
                StrSql = StrSql + "NVL(GRADE3,0) GRADE3, "
                StrSql = StrSql + "NVL(GRADE4,0) GRADE4, "
                StrSql = StrSql + "NVL(GRADE5,0) GRADE5, "
                StrSql = StrSql + "NVL(GRADE6,0) GRADE6, "
                StrSql = StrSql + "NVL(GRADE7,0) GRADE7, "
                StrSql = StrSql + "NVL(GRADE8,0) GRADE8, "
                StrSql = StrSql + "NVL(GRADE9,0) GRADE9, "
                StrSql = StrSql + "NVL(GRADE10,0) GRADE10 "
                StrSql = StrSql + "FROM MATERIALINPUT MAT "
                StrSql = StrSql + "INNER JOIN BARRIERINPUT BI "
                StrSql = StrSql + "ON MAT.CASEID=BI.CASEID "
                StrSql = StrSql + "WHERE MAT.CASEID =" + CaseId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetailsBarrD:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetMinMaxBarrierTemp() As DataSet
            Dim StrSql1 As String
            Dim Ds As DataSet
            Dim odbUtil As New DBUtil()
            Try
                StrSql1 = "select max(TEMPVAL) MAXVAL,min(TEMPVAL) MINVAL from BARRIERTEMP"
                Ds = odbUtil.FillDataSet(StrSql1, MedEcon1Connection)
                Return Ds
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetMinMaxBarrierTemp:" + ex.Message.ToString())
                Return Ds
            End Try
        End Function
        Public Function GetMinMaxBarrierHumidity() As DataSet
            Dim StrSql1 As String
            Dim Ds As DataSet
            Dim odbUtil As New DBUtil() '
            Try
                StrSql1 = "select max(RHVALUE) MAXVAL,min(RHVALUE) MINVAL from BARRIERH"
                Ds = odbUtil.FillDataSet(StrSql1, MedEcon1Connection)
                Return Ds
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetMinMaxBarrierHumidity:" + ex.Message.ToString())
                Return Ds
            End Try
        End Function
        Public Function GetExtrusionDetailsBarrP(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  MAT.CASEID,  "
                StrSql = StrSql + "TO_CHAR(MAT.EFFDATE,'MON DD,YYYY')AS EDATE, "
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
                StrSql = StrSql + "(MAT.T1*PREF.CONVTHICK) AS THICK1, "
                StrSql = StrSql + "(MAT.T2*PREF.CONVTHICK) AS THICK2, "
                StrSql = StrSql + "(MAT.T3*PREF.CONVTHICK) AS THICK3, "
                StrSql = StrSql + "(MAT.T4*PREF.CONVTHICK) AS THICK4, "
                StrSql = StrSql + "(MAT.T5*PREF.CONVTHICK) AS THICK5, "
                StrSql = StrSql + "(MAT.T6*PREF.CONVTHICK) AS THICK6, "
                StrSql = StrSql + "(MAT.T7*PREF.CONVTHICK) AS THICK7, "
                StrSql = StrSql + "(MAT.T8*PREF.CONVTHICK) AS THICK8, "
                StrSql = StrSql + "(MAT.T9*PREF.CONVTHICK) AS THICK9, "
                StrSql = StrSql + "(MAT.T10*PREF.CONVTHICK) AS THICK10, "
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
                StrSql = StrSql + "MAT.R1, "
                StrSql = StrSql + "MAT.R2, "
                StrSql = StrSql + "MAT.R3, "
                StrSql = StrSql + "MAT.R4, "
                StrSql = StrSql + "MAT.R5, "
                StrSql = StrSql + "MAT.R6, "
                StrSql = StrSql + "MAT.R7, "
                StrSql = StrSql + "MAT.R8, "
                StrSql = StrSql + "MAT.R9, "
                StrSql = StrSql + "MAT.R10, "
                StrSql = StrSql + "MAT.E1, "
                StrSql = StrSql + "MAT.E2, "
                StrSql = StrSql + "MAT.E3, "
                StrSql = StrSql + "MAT.E4, "
                StrSql = StrSql + "MAT.E5, "
                StrSql = StrSql + "MAT.E6, "
                StrSql = StrSql + "MAT.E7, "
                StrSql = StrSql + "MAT.E8, "
                StrSql = StrSql + "MAT.E9, "
                StrSql = StrSql + "MAT.E10, "
                StrSql = StrSql + "MAT.SG1 AS SGP1, "
                StrSql = StrSql + "MAT.SG2 AS SGP2, "
                StrSql = StrSql + "MAT.SG3 AS SGP3, "
                StrSql = StrSql + "MAT.SG4 AS SGP4, "
                StrSql = StrSql + "MAT.SG5 AS SGP5, "
                StrSql = StrSql + "MAT.SG6 AS SGP6, "
                StrSql = StrSql + "MAT.SG7 AS SGP7, "
                StrSql = StrSql + "MAT.SG8 AS SGP8, "
                StrSql = StrSql + "MAT.SG9 AS SGP9, "
                StrSql = StrSql + "MAT.SG10 AS SGP10, "
                StrSql = StrSql + "MAT.D1, "
                StrSql = StrSql + "MAT.D2, "
                StrSql = StrSql + "MAT.D3, "
                StrSql = StrSql + "MAT.D4, "
                StrSql = StrSql + "MAT.D5, "
                StrSql = StrSql + "MAT.D6, "
                StrSql = StrSql + "MAT.D7, "
                StrSql = StrSql + "MAT.D8, "
                StrSql = StrSql + "MAT.D9, "
                StrSql = StrSql + "MAT.D10, "
                StrSql = StrSql + "MAT.EFFDATE EFFDATEB, "
                StrSql = StrSql + "MAT.PLATE, "
                StrSql = StrSql + "MAT.DISCMATYN, "
                StrSql = StrSql + "MATDESC.DISID1, "
                StrSql = StrSql + "MATDESC.DISID2, "
                StrSql = StrSql + "MATDESC.DISID3, "
                StrSql = StrSql + "MATDESC.DISW1* PREF.CONVWT AS DISW1, "
                StrSql = StrSql + "MATDESC.DISW2* PREF.CONVWT AS DISW2, "
                StrSql = StrSql + "MATDESC.DISW3* PREF.CONVWT AS DISW3, "
                StrSql = StrSql + "MATDESC.DISP1* PREF.CURR AS DISP1, "
                StrSql = StrSql + "MATDESC.DISP2* PREF.CURR AS DISP2, "
                StrSql = StrSql + "MATDESC.DISP3* PREF.CURR AS DISP3, "
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
                StrSql = StrSql + "PREF.CONVAREA, "
                StrSql = StrSql + "PREF.CONVTHICK, "
                StrSql = StrSql + "PREF.TITLE19, "
                StrSql = StrSql + "PREF.TITLE20, "
                StrSql = StrSql + "PREF.CONVWT, "
                StrSql = StrSql + "PREF.CURR, "
                StrSql = StrSql + "OTR1, "
                StrSql = StrSql + "OTR2, "
                StrSql = StrSql + "OTR3, "
                StrSql = StrSql + "OTR4 , "
                StrSql = StrSql + "OTR5, "
                StrSql = StrSql + "OTR6 , "
                StrSql = StrSql + "OTR7, "
                StrSql = StrSql + "OTR8, "
                StrSql = StrSql + "OTR9, "
                StrSql = StrSql + "OTR10, "
                StrSql = StrSql + "WVTR1, "
                StrSql = StrSql + "WVTR2, "
                StrSql = StrSql + "WVTR3, "
                StrSql = StrSql + "WVTR4, "
                StrSql = StrSql + "WVTR5, "
                StrSql = StrSql + "WVTR6, "
                StrSql = StrSql + "WVTR7, "
                StrSql = StrSql + "WVTR8, "
                StrSql = StrSql + "WVTR9, "
                StrSql = StrSql + "WVTR10, "
                StrSql = StrSql + "NVL(GRADE1,0) GRADE1, "
                StrSql = StrSql + "NVL(GRADE2,0) GRADE2, "
                StrSql = StrSql + "NVL(GRADE3,0) GRADE3, "
                StrSql = StrSql + "NVL(GRADE4,0) GRADE4, "
                StrSql = StrSql + "NVL(GRADE5,0) GRADE5, "
                StrSql = StrSql + "NVL(GRADE6,0) GRADE6, "
                StrSql = StrSql + "NVL(GRADE7,0) GRADE7, "
                StrSql = StrSql + "NVL(GRADE8,0) GRADE8, "
                StrSql = StrSql + "NVL(GRADE9,0) GRADE9, "
                StrSql = StrSql + "NVL(GRADE10,0) GRADE10, "
                StrSql = StrSql + "MAT.OTRTEMP, "
                StrSql = StrSql + "MAT.WVTRTEMP, "
                StrSql = StrSql + "MAT.OTRRH, "
                StrSql = StrSql + "MAT.WVTRRH "
                StrSql = StrSql + "FROM MATERIALINPUT MAT "
                StrSql = StrSql + "INNER JOIN BARRIERINPUT BI "
                StrSql = StrSql + "ON MAT.CASEID=BI.CASEID "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=MAT.CASEID "
                StrSql = StrSql + "INNER JOIN MATERIALDISIN MATDESC "
                StrSql = StrSql + "ON MATDESC.CASEID = MAT.CASEID "
                StrSql = StrSql + "WHERE MAT.CASEID =" + CaseId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetailsBarrP:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetExtrusionDetailsBarrS(ByVal CaseId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  MAT.CASEID,  "
                StrSql = StrSql + "TO_CHAR(MAT.EFFDATE,'MON DD,YYYY')AS EDATE, "
                StrSql = StrSql + "(TOT.THICK*PREF.CONVTHICK)THICK, "
                StrSql = StrSql + "(MAT1.MATDE1||' '||MAT1.MATDE2)MATS1,  "
                StrSql = StrSql + "(MAT2.MATDE1||' '||MAT2.MATDE2)MATS2, "
                StrSql = StrSql + "(MAT3.MATDE1||' '||MAT3.MATDE2)MATS3, "
                StrSql = StrSql + "(MAT4.MATDE1||' '||MAT4.MATDE2)MATS4, "
                StrSql = StrSql + "(MAT5.MATDE1||' '||MAT5.MATDE2)MATS5, "
                StrSql = StrSql + "(MAT6.MATDE1||' '||MAT6.MATDE2)MATS6, "
                StrSql = StrSql + "(MAT7.MATDE1||' '||MAT7.MATDE2)MATS7, "
                StrSql = StrSql + "(MAT8.MATDE1||' '||MAT8.MATDE2)MATS8, "
                StrSql = StrSql + "(MAT9.MATDE1||' '||MAT9.MATDE2)MATS9, "
                StrSql = StrSql + "(MAT10.MATDE1||' '||MAT10.MATDE2)MATS10, "
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

                StrSql = StrSql + "(MAT1.ISADJTHICK) ISADJTHICK1,  "
                StrSql = StrSql + "(MAT2.ISADJTHICK) ISADJTHICK2,  "
                StrSql = StrSql + "(MAT3.ISADJTHICK) ISADJTHICK3,  "
                StrSql = StrSql + "(MAT4.ISADJTHICK) ISADJTHICK4,  "
                StrSql = StrSql + "(MAT5.ISADJTHICK) ISADJTHICK5,  "
                StrSql = StrSql + "(MAT6.ISADJTHICK) ISADJTHICK6,  "
                StrSql = StrSql + "(MAT7.ISADJTHICK) ISADJTHICK7,  "
                StrSql = StrSql + "(MAT8.ISADJTHICK) ISADJTHICK8,  "
                StrSql = StrSql + "(MAT9.ISADJTHICK) ISADJTHICK9,  "
                StrSql = StrSql + "(MAT10.ISADJTHICK) ISADJTHICK10,  "

                StrSql = StrSql + "(MAT1.SG)AS SGS1, "
                StrSql = StrSql + "(MAT2.SG)AS SGS2, "
                StrSql = StrSql + "(MAT3.SG)AS SGS3, "
                StrSql = StrSql + "(MAT4.SG)AS SGS4, "
                StrSql = StrSql + "(MAT5.SG)AS SGS5, "
                StrSql = StrSql + "(MAT6.SG)AS SGS6, "
                StrSql = StrSql + "(MAT7.SG)AS SGS7, "
                StrSql = StrSql + "(MAT8.SG)AS SGS8, "
                StrSql = StrSql + "(MAT9.SG)AS SGS9, "
                StrSql = StrSql + "(MAT10.SG)AS SGS10, "
                StrSql = StrSql + "(MATOUT.M1*PREF.CONVWT/PREF.CONVAREA) AS WTPARA1, "
                StrSql = StrSql + "(MATOUT.M2*PREF.CONVWT/PREF.CONVAREA) AS WTPARA2, "
                StrSql = StrSql + "(MATOUT.M3*PREF.CONVWT/PREF.CONVAREA) AS WTPARA3, "
                StrSql = StrSql + "(MATOUT.M4*PREF.CONVWT/PREF.CONVAREA) AS WTPARA4, "
                StrSql = StrSql + "(MATOUT.M5*PREF.CONVWT/PREF.CONVAREA) AS WTPARA5, "
                StrSql = StrSql + "(MATOUT.M6*PREF.CONVWT/PREF.CONVAREA) AS WTPARA6, "
                StrSql = StrSql + "(MATOUT.M7*PREF.CONVWT/PREF.CONVAREA) AS WTPARA7, "
                StrSql = StrSql + "(MATOUT.M8*PREF.CONVWT/PREF.CONVAREA) AS WTPARA8, "
                StrSql = StrSql + "(MATOUT.M9*PREF.CONVWT/PREF.CONVAREA) AS WTPARA9, "
                StrSql = StrSql + "(MATOUT.M10*PREF.CONVWT/PREF.CONVAREA) AS WTPARA10, "
                StrSql = StrSql + "(TOT.WTPERAREA*PREF.CONVWT/PREF.CONVAREA)WTPERAREA, "
                StrSql = StrSql + "TOT.DISCRETEWT * PREF.CONVWT AS DISCTOTAL, "
                StrSql = StrSql + "TOT.DISCRETECOST "
                StrSql = StrSql + "FROM MATERIALINPUT MAT "
                StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
                StrSql = StrSql + "ON PREF.CASEID=MAT.CASEID "
                StrSql = StrSql + "INNER JOIN TOTAL TOT "
                StrSql = StrSql + "ON TOT.CASEID = MAT.CASEID "
                StrSql = StrSql + "INNER JOIN MATERIALDISIN MATDESC "
                StrSql = StrSql + "ON MATDESC.CASEID = MAT.CASEID "
                StrSql = StrSql + "INNER JOIN MATERIALOUTPUT MATOUT "
                StrSql = StrSql + "ON MATOUT.CASEID=MAT.CASEID "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MATERIALSARCH MATA1 "
                StrSql = StrSql + "ON MATA1.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA1.MATID = MAT.M1 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MATERIALSARCH MATA2 "
                StrSql = StrSql + "ON MATA2.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA2.MATID = MAT.M2 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MATERIALSARCH MATA3 "
                StrSql = StrSql + "ON MATA3.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA3.MATID = MAT.M3 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MATERIALSARCH MATA4 "
                StrSql = StrSql + "ON MATA4.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA4.MATID = MAT.M4 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MATERIALSARCH MATA5 "
                StrSql = StrSql + "ON MATA5.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA5.MATID = MAT.M5 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MATERIALSARCH MATA6 "
                StrSql = StrSql + "ON MATA6.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA6.MATID = MAT.M6 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MATERIALSARCH MATA7 "
                StrSql = StrSql + "ON MATA7.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA7.MATID = MAT.M7 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MATERIALSARCH MATA8 "
                StrSql = StrSql + "ON MATA8.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA8.MATID = MAT.M8 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MATERIALSARCH MATA9 "
                StrSql = StrSql + "ON MATA9.EFFDATE = MAT.EFFDATE "
                StrSql = StrSql + "AND MATA9.MATID = MAT.M9 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.MATERIALSARCH MATA10 "
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
                StrSql = StrSql + "WHERE MAT.CASEID =" + CaseId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetExtrusionDetailsBarrS:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Comp Module"
        Public Function GetCompUserDetails(ByVal Id As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERS.USERID,  "
                StrSql = StrSql + "UPPER(USERNAME)USERNAME, "
                StrSql = StrSql + "USERNAME AS TOOLUSERNAME, "
                'Checking for License Administrator
                StrSql = StrSql + "NVL(USERS.ISIADMINLICUSR,'N')ISIADMINLICUSR,"
                StrSql = StrSql + "USERS.LICENSEID,"
                StrSql = StrSql + "SERVICES.SERVICEID,"
                StrSql = StrSql + "SERVICES.SERVICEDE, "
                StrSql = StrSql + "USERPERMISSIONS.USERROLE AS SERVIECROLE, "
                StrSql = StrSql + "USERPERMISSIONS.MAXCASECOUNT, "
                StrSql = StrSql + "(CASE WHEN NVL(USERS.ISINTERNALUSR,'N') ='Y' THEN "
                StrSql = StrSql + "'AADMIN' "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "'USER' "
                StrSql = StrSql + "END)USERROLE "
                StrSql = StrSql + "FROM ECON.ULOGIN "
                StrSql = StrSql + "INNER JOIN ECON.USERS "
                StrSql = StrSql + "ON UPPER(USERS.USERNAME) = UPPER(ECON.ULOGIN.UNAME) "
                StrSql = StrSql + "AND UPPER(USERS.PASSWORD) = UPPER(ECON.ULOGIN.UPWD) "
                StrSql = StrSql + "INNER JOIN ECON.USERPERMISSIONS "
                StrSql = StrSql + "ON USERPERMISSIONS.USERID = USERS.USERID "
                StrSql = StrSql + "INNER JOIN ECON.SERVICES "
                StrSql = StrSql + "ON SERVICES.SERVICEID = USERPERMISSIONS.SERVICEID "
                StrSql = StrSql + "WHERE ECON.ULOGIN.ID = " + Id.ToString() + " "
                StrSql = StrSql + "AND SERVICES.SERVICEDE='CompEcon' "

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetCompUserDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetBCaseCompDetails() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  CASEID,CASEDE1, CASEDE2,SERVICEUSER.USERID SERVICEUSERID,"
                StrSql = StrSql + "CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "INNER JOIN ECON.USERS ON UPPER(USERS.USERNAME) = UPPER(PERMISSIONSCASES.USERNAME) "
                StrSql = StrSql + "INNER JOIN SERVICEUSER ON SERVICEUSER.USERID= USERS.USERID "
                StrSql = StrSql + "INNER JOIN ECON.SERVICES ON SERVICES.SERVICEID= SERVICEUSER.SERVICEID "
                StrSql = StrSql + "WHERE SERVICES.SERVICEDE='CompEcon' "
                StrSql = StrSql + "AND PERMISSIONSCASES.SERVICEID IS NULL "
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID"
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetBCaseCompDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetServiceCompUser(ByVal UserId As String) As DataSet
            Dim Dts As DataSet
            Dim StrSql As String = String.Empty
            Dim odbUtil As New DBUtil()
            Try
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
                StrSql = "SELECT SERVICEUSER.SERVICEID ,SERVICES.SERVICEDE "
                StrSql = StrSql + "FROM SERVICEUSER INNER JOIN ECON.SERVICES ON SERVICES.SERVICEID=SERVICEUSER.SERVICEID "
                StrSql = StrSql + "WHERE USERID=" + UserId + " "
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts

            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
        Public Function GetPropCaseDetails(ByVal UserName As String, ByVal ServiceId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT CASEID,CASEDE1,CASEDE2,CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE UPPER(USERNAME) ='" + UserName.ToUpper().ToString() + "' "
                StrSql = StrSql + "AND SERVICEID=" + ServiceId + " "
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID "

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetPropCaseDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompPCaseDetailsByLicense(ByVal UserName As String, ByVal ServiceId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT CASEID,  "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "CASEDE2, "
                StrSql = StrSql + "CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDES "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "INNER JOIN ECON.USERS "
                StrSql = StrSql + "ON UPPER(USERS.USERNAME)=UPPER(PERMISSIONSCASES.USERNAME) "
                StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "') "
                StrSql = StrSql + "AND SERVICEID=" + ServiceId + " "
                StrSql = StrSql + "ORDER BY UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetPCaseDetailsByLicense:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompGroupIDByUSer(ByVal UserID As String, ByVal ServiceId As String) As DataSet
            Dim Dts As New DataSet()
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
                Dim StrSql As String = ""
                StrSql = "SELECT GROUPID,  "
                StrSql = StrSql + "DES1, "
                StrSql = StrSql + "DES2, "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "CREATIONDATE, "
                StrSql = StrSql + "UPDATEDATE, "
                StrSql = StrSql + "SERVICEID "
                StrSql = StrSql + "FROM GROUPS WHERE USERID= " + UserID + " "
                StrSql = StrSql + "AND SERVICEID IN (" + ServiceId + ")"

                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function
        Public Function GetCompGroupCaseDetails(ByVal UserID As String, ByVal ServiceId As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New Med1GetData.Selectdata()
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim strSQL As String = String.Empty
            Dim ds As New DataSet()
            Dim CaseIDs As String = String.Empty
            Dim DtRes As New DataSet()
            Dim cnt As Integer = 0
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
                Dts = objGetData.GetCompGroupIDByUSer(UserID, ServiceId)
                If Dts.Tables(0).Rows.Count > 0 Then
                    For i = 0 To Dts.Tables(0).Rows.Count - 1
                        ds = objGetData.GetGroupCasesByUSer(Dts.Tables(0).Rows(i).Item("GROUPID").ToString())
                        If ds.Tables(0).Rows.Count > 0 Then
                            For j = 0 To ds.Tables(0).Rows.Count - 1
                                If j = 0 Then
                                    CaseIDs = ds.Tables(0).Rows(j).Item("CASEID").ToString()
                                Else
                                    CaseIDs = CaseIDs + " " + ds.Tables(0).Rows(j).Item("CASEID").ToString()
                                End If
                            Next
                        Else
                            CaseIDs = "0"
                        End If
                        If cnt = 0 Then
                            cnt += 1
                            'strSQL = "SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' || ' ,Cases:" + CaseIDs + "' AS GROUPDES,'" + CaseIDs + "' AS CASEIDS FROM DUAL "
                            strSQL = "SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '   ' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '   '|| '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "'  AS GROUPDES,'" + CaseIDs + "' AS CASEIDS,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS DES1,'" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS DES2,'" + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + "' || ':' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS CDES1 FROM DUAL "
                        Else
                            cnt += 1
                            strSQL = strSQL + "UNION ALL SELECT " + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " GROUPID,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' ||  '   ' || '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS GROUPNAME," + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + " || ' - ' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' || '   '||  '" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "'  AS GROUPDES,'" + CaseIDs + "' AS CASEIDS,'" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS DES1,'" + Dts.Tables(0).Rows(i).Item("Des2").ToString() + "' AS DES2,'" + Dts.Tables(0).Rows(i).Item("GROUPID").ToString() + "' || ':' || '" + Dts.Tables(0).Rows(i).Item("Des1").ToString() + "' AS CDES1 FROM DUAL "
                        End If
                    Next
                    strSQL = " SELECT * FROM ( " + strSQL + " ) DUAL ORDER BY UPPER(DES1),UPPER(DES2)"
                End If
                If strSQL <> "" Then
                    DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                Else
                    strSQL = "select * FROM GROUPS WHERE GROUPID=0"
                    DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                End If
                Return DtRes
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function
        Public Function GetCompUserCompanyUsers(ByVal UserName As String, ByVal ServiceId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT  USERS.USERID,  "
                StrSql = StrSql + "USERS.USERNAME "
                StrSql = StrSql + "FROM ECON.USERS "
                StrSql = StrSql + "INNER JOIN ECON.USERPERMISSIONS "
                StrSql = StrSql + "ON USERPERMISSIONS.USERID= USERS.USERID "
                StrSql = StrSql + "INNER JOIN ECON.SERVICES "
                StrSql = StrSql + "ON SERVICES.SERVICEID = USERPERMISSIONS.SERVICEID "
                StrSql = StrSql + "WHERE USERS.LICENSEID=(SELECT LICENSEID FROM ECON.USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper.ToString() + "') "
                StrSql = StrSql + "AND SERVICES.SERVICEID= " + ServiceId.ToString() + " "
                StrSql = StrSql + "AND UPPER(USERPERMISSIONS.USERROLE)='READWRITE' "
                StrSql = StrSql + "GROUP BY USERS.USERID,USERS.USERNAME "
                StrSql = StrSql + "ORDER BY USERS.USERNAME "

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetCompUserCompanyUsers:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompCaseGrpDetails(ByVal UserName As String, ByVal UserID As String, ByVal keyWord As String, ByVal ServiceId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT GROUPNAME,GROUPID,CASEID,CASEDE1,CASEDE2,CASEDE3,CASEDES,CREATIONDATE,SERVERDATE "
                StrSql = StrSql + " FROM "
                StrSql = StrSql + " ( "
                StrSql = StrSql + "SELECT CASE WHEN NVL(GROUPS.GROUPID,'0')='0' THEN 'None' ELSE GROUPS.DES1 END AS GROUPNAME,  "
                StrSql = StrSql + "NVL(GROUPS.GROUPID,'0') AS GROUPID, "
                StrSql = StrSql + "PC.CASEID, "
                StrSql = StrSql + "PC.CASEDE1, "
                StrSql = StrSql + "PC.CASEDE2, "
                StrSql = StrSql + "CAST(PC.CASEDE3 AS VARCHAR(4000))CASEDE3, "

                StrSql = StrSql + "(PC.CASEID||'. PACKAGE FORMAT= '||PC.CASEDE1||' UNIQUE FEATURES= '||PC.CASEDE2)CASEDES, "

                StrSql = StrSql + " PC.CREATIONDATE, "
                StrSql = StrSql + "CASE WHEN PC.CREATIONDATE-PC.SERVERDATE =0 THEN 'NA' ELSE to_char(PC.SERVERDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  SERVERDATE "


                StrSql = StrSql + "FROM GROUPS "
                StrSql = StrSql + "INNER JOIN GROUPCASES "
                StrSql = StrSql + "ON GROUPCASES.GROUPID=GROUPS.Groupid "
                StrSql = StrSql + "AND GROUPS.USERID=" + UserID + " "
                StrSql = StrSql + "RIGHT OUTER JOIN PERMISSIONSCASES PC "
                StrSql = StrSql + "ON PC.CASEID=GROUPCASES.CASEID "
                StrSql = StrSql + "WHERE UPPER(USERNAME) ='" + UserName.ToUpper().Trim() + "' "
                StrSql = StrSql + "AND PC.SERVICEID=" + ServiceId + " "
                StrSql = StrSql + "ORDER BY CASEID DESC "
                StrSql = StrSql + " ) "

                StrSql = StrSql + "WHERE UPPER(CASEDE1) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE2) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEID) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(CASEDE3) LIKE '%" + keyWord.ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(GROUPNAME) LIKE '%" + keyWord.ToUpper().Trim() + "%' "

                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetCompCaseGrpDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompCases(ByVal UserName As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String, ByVal ServiceId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CASEID, (CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "WHERE UPPER(USERNAME) ='" + UserName.ToUpper().ToString() + "' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "AND SERVICEID=" + ServiceId + " "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetCompCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompCasesByLicense(ByVal UserName As String, ByVal CaseDe1 As String, ByVal CaseDe2 As String, ByVal ServiceId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT CASEID,  "
                StrSql = StrSql + "CASEDE1, "
                StrSql = StrSql + "CASEDE2, "
                StrSql = StrSql + "CAST(CASEDE3 AS VARCHAR(4000))CASEDE3,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "INNER JOIN ECON.USERS "
                StrSql = StrSql + "ON UPPER(USERS.USERNAME)=UPPER(PERMISSIONSCASES.USERNAME) "
                StrSql = StrSql + "WHERE USERS.LICENSEID IN (SELECT USERS.LICENSEID FROM ECON.USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "') "
                StrSql = StrSql + "AND SERVICEID =" + ServiceId + " "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID"
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetCompCasesByLicense:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompBCases(ByVal CaseDe1 As String, ByVal CaseDe2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  CASEID,(CASEDE1||'  '||CASEDE2)CASEDES,CASEDE1,CASEDE2,(CASEID||'. PACKAGE FORMAT= '||CASEDE1||' UNIQUE FEATURES= '||CASEDE2)CASEDE "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "INNER JOIN ECON.USERS ON UPPER(USERS.USERNAME) = UPPER(PERMISSIONSCASES.USERNAME) "
                StrSql = StrSql + "INNER JOIN SERVICEUSER ON SERVICEUSER.USERID= USERS.USERID "
                StrSql = StrSql + "INNER JOIN ECON.SERVICES ON SERVICES.SERVICEID= SERVICEUSER.SERVICEID "
                StrSql = StrSql + "WHERE SERVICES.SERVICEDE='CompEcon' "
                StrSql = StrSql + "AND PERMISSIONSCASES.SERVICEID IS NULL "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE1),'#') LIKE '%" + CaseDe1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(CASEDE2),'#') LIKE '%" + CaseDe2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(CASEDE1),CASEID "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetCompBCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetAllCompGroupDetails(ByVal UserID As String, ByVal ServiceId As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New Med1GetData.Selectdata()
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim strSQL As String = String.Empty
            Dim ds As New DataSet()
            Dim CaseIDs As String = String.Empty
            Dim DtRes As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim MyConnectionString As String = ""
            Dim dtOutPut As New DataSet()
            Dim strSqlOutPut As String = String.Empty
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
            Try


                'Getting Groups
                strSQL = "SELECT GROUPID,  "
                strSQL = strSQL + "DES1 GROUPNAME, "
                strSQL = strSQL + "DES2 GROUPDES, "
                strSQL = strSQL + "GROUPID || ':'|| DES1 CDES1,"
                strSQL = strSQL + "to_char(CREATIONDATE,'mm/dd/yyyy hh:mi:ss AM'  ) CREATIONDATE, "
                strSQL = strSQL + "CASE WHEN CREATIONDATE-UPDATEDATE =0 THEN 'NA' ELSE to_char(UPDATEDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  UPDATEDATE "
                strSQL = strSQL + "FROM "
                strSQL = strSQL + "GROUPS "
                strSQL = strSQL + "WHERE USERID= " + UserID + " "
                strSQL = strSQL + "AND SERVICEID IN (" + ServiceId + ")"
                DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                If DtRes.Tables(0).Rows.Count > 0 Then
                    For j = 0 To DtRes.Tables(0).Rows.Count - 1
                        'Getting CaseID
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "CASEID "
                        strSQL = strSQL + "FROM MED1.GROUPCASES "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString()
                        strSQL = strSQL + " ORDER BY  SEQ"
                        Dts = odbUtil.FillDataSet(strSQL, MedEcon1Connection)
                        If Dts.Tables(0).Rows.Count > 0 Then
                            For i = 0 To Dts.Tables(0).Rows.Count - 1
                                If i = 0 Then
                                    CaseIDs = Dts.Tables(0).Rows(i).Item("CASEID").ToString()
                                Else
                                    CaseIDs = CaseIDs + ", " + Dts.Tables(0).Rows(i).Item("CASEID").ToString()
                                End If
                            Next
                        Else
                            CaseIDs = ""
                        End If
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "DES1 GROUPNAME, "
                        strSQL = strSQL + "'" + CaseIDs.ToString() + "' CASEID, "
                        strSQL = strSQL + "DES2 GROUPDES, "
                        strSQL = strSQL + "USR.UserName, "
                        strSQL = strSQL + "GROUPID || ':'|| DES1 CDES1,"
                        strSQL = strSQL + "to_char(GPS.CREATIONDATE,'mm/dd/yyyy hh:mi:ss AM'  ) CREATIONDATE, "
                        strSQL = strSQL + "CASE WHEN GPS.CREATIONDATE-GPS.UPDATEDATE =0 THEN 'NA' ELSE to_char(GPS.UPDATEDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  UPDATEDATE "
                        strSQL = strSQL + "FROM "
                        strSQL = strSQL + "GROUPS GPS "
                        strSQL = strSQL + "INNER JOIN ECON.USERS USR "
                        strSQL = strSQL + "ON  USR.USERID=GPS.USERID "
                        strSQL = strSQL + "WHERE GPS.USERID= " + UserID + " AND GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString() + " "
                        If j = 0 Then
                            strSqlOutPut = strSQL
                        Else
                            strSqlOutPut = strSqlOutPut + " UNION ALL " + strSQL
                        End If
                    Next

                    strSqlOutPut = "SELECT * FROM ( " + strSqlOutPut + " ) DUAL ORDER BY UPPER(GROUPNAME),UPPER(GROUPDES)"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, MedEcon1Connection)
                Else
                    strSqlOutPut = " SELECT * FROM GROUPCASES WHERE GROUPID=0"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, MedEcon1Connection)
                End If

                Return dtOutPut
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function
        Public Function GetCompGroupDetails(ByVal UserID As String, ByVal flag As Char, ByVal ServiceId As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New Med1GetData.Selectdata()
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim strSQL As String = String.Empty
            Dim ds As New DataSet()
            Dim CaseIDs As String = String.Empty
            Dim DtRes As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim MyConnectionString As String = ""
            Dim dtOutPut As New DataSet()
            Dim strSqlOutPut As String = String.Empty
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("MedEcon1ConnectionString")
            Try


                'Getting Groups
                strSQL = "SELECT GROUPID,  "
                strSQL = strSQL + "DES1 GROUPNAME, "
                strSQL = strSQL + "DES2 GROUPDES, "
                strSQL = strSQL + "GROUPID || ':'|| DES1 CDES1,"
                strSQL = strSQL + "to_char(CREATIONDATE,'mm/dd/yyyy hh:mi:ss AM'  ) CREATIONDATE, "
                strSQL = strSQL + "CASE WHEN CREATIONDATE-UPDATEDATE =0 THEN 'NA' ELSE to_char(UPDATEDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  UPDATEDATE "
                strSQL = strSQL + "FROM "
                strSQL = strSQL + "GROUPS "
                strSQL = strSQL + "WHERE USERID= " + UserID + " "
                strSQL = strSQL + "AND SERVICEID IN (" + ServiceId + ") "
                DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                If DtRes.Tables(0).Rows.Count > 0 Then
                    For j = 0 To DtRes.Tables(0).Rows.Count - 1
                        'Getting CaseID
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "CASEID "
                        strSQL = strSQL + "FROM MED1.GROUPCASES "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString()
                        strSQL = strSQL + " ORDER BY  SEQ"
                        Dts = odbUtil.FillDataSet(strSQL, MedEcon1Connection)
                        If Dts.Tables(0).Rows.Count > 0 Then
                            For i = 0 To Dts.Tables(0).Rows.Count - 1
                                If i = 0 Then
                                    CaseIDs = Dts.Tables(0).Rows(i).Item("CASEID").ToString()
                                Else
                                    CaseIDs = CaseIDs + ", " + Dts.Tables(0).Rows(i).Item("CASEID").ToString()
                                End If
                            Next
                        Else
                            CaseIDs = ""
                        End If
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "DES1 GROUPNAME, "
                        strSQL = strSQL + "'" + CaseIDs.ToString() + "' CaseID, "
                        strSQL = strSQL + "DES2 GROUPDES, "
                        strSQL = strSQL + "GROUPID || ':'|| DES1 CDES1,"
                        strSQL = strSQL + "to_char(CREATIONDATE,'mm/dd/yyyy hh:mi:ss AM'  ) CREATIONDATE, "
                        strSQL = strSQL + "CASE WHEN CREATIONDATE-UPDATEDATE =0 THEN 'NA' ELSE to_char(UPDATEDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  UPDATEDATE "
                        strSQL = strSQL + "FROM "
                        strSQL = strSQL + "GROUPS "
                        strSQL = strSQL + "WHERE USERID= " + UserID + " AND GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString() + " "
                        If j = 0 Then
                            strSqlOutPut = strSQL
                        Else
                            strSqlOutPut = strSqlOutPut + " UNION ALL " + strSQL
                        End If
                    Next
                    If flag = "Y" Then ' Y FOR GROUPDEATILS PAGE ,N FOR EDITGROUPS PAGE
                        strSQL = "SELECT 0 GROUPID,  "
                        strSQL = strSQL + "'None'  GROUPNAME, "
                        strSQL = strSQL + "'NA' CaseID, "
                        strSQL = strSQL + "'NA'  GROUPDES, "
                        strSQL = strSQL + "'0:None'  CDES1, "
                        strSQL = strSQL + "'NA'  CREATIONDATE, "
                        strSQL = strSQL + "'NA'  UPDATEDATE "
                        strSQL = strSQL + "FROM "
                        strSQL = strSQL + "DUAL "
                        strSqlOutPut = strSqlOutPut + " UNION ALL " + strSQL
                    End If
                    strSqlOutPut = " SELECT * FROM ( " + strSqlOutPut + " ) DUAL ORDER BY UPPER(GROUPNAME),UPPER(GROUPDES)"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, MedEcon1Connection)
                Else
                    strSqlOutPut = " SELECT * FROM GROUPCASES WHERE GROUPID=0 "
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, MedEcon1Connection)
                End If

                Return dtOutPut
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function
        Public Function GetCompTotalCaseCount(ByVal UNAME As String, ByVal ServiceId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT COUNT(*) AS TOTALCOUNT FROM PERMISSIONSCASES  "
                StrSql = StrSql + "WHERE UPPER(USERNAME)='" + UNAME.ToString().ToUpper() + "' "
                StrSql = StrSql + "AND SERVICEID=" + ServiceId + " "
                Dts = odbUtil.FillDataSet(StrSql, MedEcon1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Med1GetData:GetCompTotalCaseCount:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

    End Class
End Class
