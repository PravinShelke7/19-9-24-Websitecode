Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System


Public Class SpecGetData
    Public Class Selectdata

        Public Function GetUsernamePassword(ByVal Id As String) As DataTable
            Dim Dts As New DataTable()
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
                Dim StrSql As String = String.Empty
                StrSql = "Select Uname,Upwd,Users.Company  "
                StrSql = StrSql + "From Ulogin "
                StrSql = StrSql + "inner join Users "
                StrSql = StrSql + "On Users.UserName = Ulogin.Uname "
                StrSql = StrSql + "Where ID = " + Id.ToString() + " "

                Dts = odbUtil.FillDataTable(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw New Exception("Selectdata:GetUsernamePassword:-" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSpecDetails(ByVal SpecId As Integer) As DataSet
            Dim Dts As New DataSet
            Try
                Dim StrSql As String = ""
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")
                StrSql = "SELECT PACKAGINGSPECIFICATIONID,  "
                StrSql = StrSql + "COMPNAME, "
                StrSql = StrSql + "(PACKAGINGSPECIFICATIONID||':'||NAME)DES, "
                StrSql = StrSql + "NAME, "
                StrSql = StrSql + "E1S1CASEID, "
                StrSql = StrSql + "E1S2CASEID "
                StrSql = StrSql + "FROM PACKAGINGSPECIFICATIONS "
                If SpecId <> 0 Then
                    StrSql = StrSql + "WHERE PACKAGINGSPECIFICATIONID = " + SpecId.ToString() + " "
                End If
                StrSql = StrSql + "ORDER BY DES"
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
            Catch ex As Exception
                Throw New Exception("Selectdata:GetSpecDetails:-" + ex.Message.ToString())
            End Try
            Return Dts
        End Function

        Public Function GetSpecCompanies(ByVal UserName As String) As DataSet
            Dim Dts As New DataSet
            Try
                Dim StrSql As String = ""
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
                StrSql = "SELECT DISTINCT COMPANY FROM USERS  "
                If UserName <> "ADMINISTRATOR" Then
                    StrSql = StrSql + "WHERE UPPER(USERNAME)= '" + UserName.ToUpper() + "' "
                End If
                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
            Catch ex As Exception
                Throw New Exception("Selectdata:GetSpecCompanies:-" + ex.Message.ToString())
            End Try
            Return Dts
        End Function

        Public Function GetE1S1Cases(ByVal Company As String, ByVal UserName As String) As DataTable
            Dim Dts As New DataTable
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("Sustain1ConnectionString")
                Dim StrSql As String = String.Empty
                StrSql = "SELECT DISTINCT  "
                StrSql = StrSql + "CASEID, "
                StrSql = StrSql + "CASEDE1,(CASEID||' - '|| CASEDE1||' ' || CASEDE2) AS CASEDE "
                StrSql = StrSql + "FROM PERMISSIONSCASES "
                StrSql = StrSql + "INNER JOIN ECON.USERS "
                StrSql = StrSql + "ON USERS.USERNAME = PERMISSIONSCASES.USERNAME "
                If UserName <> "ADMINISTRATOR" Then
                    StrSql = StrSql + "WHERE UPPER(USERS.COMPANY)  = '" + Company.ToUpper() + "' "
                End If
                StrSql = StrSql + "ORDER BY CASEDE1"


                Dts = odbUtil.FillDataTable(StrSql, MyConnectionString)

            Catch ex As Exception
                Throw New Exception("Selectdata:GetE1S1Cases:-" + ex.Message.ToString())

            End Try
            Return Dts
        End Function

        Public Function GetColoumnType(ByVal TableName As String) As DataTable
            Dim Dts As New DataTable()
            Try
                Dim Odbutil As New DBUtil()
                Dim StrSql As String = ""
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")
                StrSql = "SELECT CLIENTSPECID, NAME, COMPNAME, MAT, THICK, RECYCLE, EXTRAPROC, MAPPEDMATID, MAPPEDDEPTID, E1S1CASEID FROM " + TableName + "  "
                Dts = Odbutil.FillDataTable(StrSql, MyConnectionString)
            Catch ex As Exception
                Throw New Exception("SelectQuery.GetColoumnType:" + ex.Message.ToString())
            End Try
            Return Dts
        End Function


#Region "Assumption Supporting SQL"
        Public Function GetSpecMaterials(ByVal SpecId As String) As DataSet
            Dim Dts As New DataSet
            Try
                Dim StrSql As String = ""
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")
                StrSql = "SELECT PKGMFGMATERIALID,  "
                StrSql = StrSql + "(PKGMFGMATERIALID||':'||MATERIALDES)DES "
                StrSql = StrSql + "FROM PKGMFGMATERIALS "
                StrSql = StrSql + "INNER JOIN PACKAGINGSPECIFICATIONS "
                StrSql = StrSql + "ON PACKAGINGSPECIFICATIONS.COMPNAME = PKGMFGMATERIALS.COMPNAME "
                StrSql = StrSql + "WHERE PACKAGINGSPECIFICATIONS.PACKAGINGSPECIFICATIONID = " + SpecId + " "
                StrSql = StrSql + "ORDER BY MATERIALDES "

                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
            Catch ex As Exception
                Throw New Exception("Selectdata:GetSpecMaterials:-" + ex.Message.ToString())
            End Try
            Return Dts
        End Function

        Public Function GetESMaterials() As DataSet
            Dim Dts As New DataSet
            Try
                Dim StrSql As String = ""
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
                StrSql = "select Matid, (Matid||':'||matde1||' '||matde2) MaterialDes ,price,sg from Materials ORDER BY matde1"

                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
            Catch ex As Exception
                Throw New Exception("Selectdata:GetESMaterials:-" + ex.Message.ToString())
            End Try
            Return Dts
        End Function

        Public Function GetESDept() As DataSet
            Dim Dts As New DataSet
            Try
                Dim StrSql As String = ""
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
                StrSql = "SELECT PROCID,(PROCDE1||' '||PROCDE2)PROCDE FROM PROCESS ORDER BY PROCDE"

                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
            Catch ex As Exception
                Throw New Exception("Selectdata:GetESDept:-" + ex.Message.ToString())
            End Try
            Return Dts
        End Function
#End Region

#Region "Assumption Pages SQL"
        Public Function ExtrusionInput(ByVal SpecId As String) As DataSet
            Dim Dts As New DataSet
            Try
                Dim StrSql As String = ""
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")
                StrSql = "SELECT  "
                StrSql = StrSql + "PACKAGINGSPECIFICATIONID, "
                StrSql = StrSql + "M1, "
                StrSql = StrSql + "M2, "
                StrSql = StrSql + "M3, "
                StrSql = StrSql + "M4, "
                StrSql = StrSql + "M5, "
                StrSql = StrSql + "M6, "
                StrSql = StrSql + "M7, "
                StrSql = StrSql + "M8, "
                StrSql = StrSql + "M9, "
                StrSql = StrSql + "M10, "
                StrSql = StrSql + "T1, "
                StrSql = StrSql + "T2, "
                StrSql = StrSql + "T3, "
                StrSql = StrSql + "T4, "
                StrSql = StrSql + "T5, "
                StrSql = StrSql + "T6, "
                StrSql = StrSql + "T7, "
                StrSql = StrSql + "T8, "
                StrSql = StrSql + "T9, "
                StrSql = StrSql + "T10, "
                StrSql = StrSql + "R1, "
                StrSql = StrSql + "R2, "
                StrSql = StrSql + "R3, "
                StrSql = StrSql + "R4, "
                StrSql = StrSql + "R5, "
                StrSql = StrSql + "R6, "
                StrSql = StrSql + "R7, "
                StrSql = StrSql + "R8, "
                StrSql = StrSql + "R9, "
                StrSql = StrSql + "R10, "
                StrSql = StrSql + "D1, "
                StrSql = StrSql + "D2, "
                StrSql = StrSql + "D3, "
                StrSql = StrSql + "D4, "
                StrSql = StrSql + "D5, "
                StrSql = StrSql + "D6, "
                StrSql = StrSql + "D7, "
                StrSql = StrSql + "D8, "
                StrSql = StrSql + "D9, "
                StrSql = StrSql + "D10, "
                StrSql = StrSql + "E1, "
                StrSql = StrSql + "E2, "
                StrSql = StrSql + "E3, "
                StrSql = StrSql + "E4, "
                StrSql = StrSql + "E5, "
                StrSql = StrSql + "E6, "
                StrSql = StrSql + "E7, "
                StrSql = StrSql + "E8, "
                StrSql = StrSql + "E9, "
                StrSql = StrSql + "E10, "
                StrSql = StrSql + "EM1, "
                StrSql = StrSql + "EM2, "
                StrSql = StrSql + "EM3, "
                StrSql = StrSql + "EM4, "
                StrSql = StrSql + "EM5, "
                StrSql = StrSql + "EM6, "
                StrSql = StrSql + "EM7, "
                StrSql = StrSql + "EM8, "
                StrSql = StrSql + "EM9, "
                StrSql = StrSql + "EM10 "
                StrSql = StrSql + "FROM PKGMFGMATERIALLAYERS "
                StrSql = StrSql + "WHERE PKGMFGMATERIALLAYERS.PACKAGINGSPECIFICATIONID = " + SpecId + " "

                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
            Catch ex As Exception
                Throw New Exception("Selectdata:ExtrusionInput:-" + ex.Message.ToString())
            End Try
            Return Dts
        End Function
#End Region

#Region "Results SQL"
        Public Function GetS1ErgyTotal(ByVal SpecId As Integer) As DataSet
            Dim Dts As New DataSet
            Try
                Dim StrSql As String = ""
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")
                StrSql = "SELECT RMERGY AS E1 ,  "
                StrSql = StrSql + "RMPACKERGY AS E2, "
                StrSql = StrSql + "RMANDPACKTRNSPTERGY AS E3, "
                StrSql = StrSql + "PROCERGY AS E4, "
                StrSql = StrSql + "DPPACKERGY AS E5, "
                StrSql = StrSql + "DPTRNSPTERGY AS E6, "
                StrSql = StrSql + "TRSPTTOCUS AS E7, "
                StrSql = StrSql + "(RMERGY+RMPACKERGY+RMANDPACKTRNSPTERGY+PROCERGY+DPPACKERGY+DPTRNSPTERGY+TRSPTTOCUS) AS E8, "
                StrSql = StrSql + "(RMERGY+RMPACKERGY+DPPACKERGY) AS E9, "
                StrSql = StrSql + "PROCERGY AS E10, "
                StrSql = StrSql + "(RMANDPACKTRNSPTERGY+DPTRNSPTERGY+TRSPTTOCUS) AS E11, "
                StrSql = StrSql + "(RMERGY+RMPACKERGY+RMANDPACKTRNSPTERGY+PROCERGY+DPPACKERGY+DPTRNSPTERGY+TRSPTTOCUS) AS E12, "
                StrSql = StrSql + "'Raw Materials' As ET1, "
                StrSql = StrSql + "'Raw Materials Packaging' AS ET2, "
                StrSql = StrSql + "'RM & Pack Transport' AS ET3, "
                StrSql = StrSql + "'Process' AS ET4, "
                StrSql = StrSql + "'Distribution Packaging' AS ET5, "
                StrSql = StrSql + "'DP Transport' AS ET6, "
                StrSql = StrSql + "'Transport to Customer' AS ET7, "
                StrSql = StrSql + "'Total Energy' AS ET8, "
                StrSql = StrSql + "'Purchased Materials' AS ET9, "
                StrSql = StrSql + "'Process' AS ET10, "
                StrSql = StrSql + "'Transportation' AS ET11, "
                StrSql = StrSql + "'Total Energy' AS ET12, "
                StrSql = StrSql + "VOLUME, "
                StrSql = StrSql + "FINVOLMUNITS, "
                StrSql = StrSql + "FINVOLMSI "
                StrSql = StrSql + "FROM S1TOTAL "
                StrSql = StrSql + "WHERE PACKAGINGSPECIFICATIONID = " + SpecId.ToString() + " "


                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
            Catch ex As Exception
                Throw New Exception("Selectdata:GetS1ErgyTotal:-" + ex.Message.ToString())
            End Try
            Return Dts
        End Function

        Public Function GetS1AllSpecErgyTotal(ByVal CompName As String, ByVal IsSalesVolOrMsi As Boolean, ByVal IsAll As Boolean) As DataSet
            Dim Dts As New DataSet
            Try
                Dim StrSql As String = ""
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")
                StrSql = "SELECT SUM(RMERGY) AS E1 ,  "
                StrSql = StrSql + "SUM(RMPACKERGY) AS E2, "
                StrSql = StrSql + "SUM(RMANDPACKTRNSPTERGY) AS E3, "
                StrSql = StrSql + "SUM(PROCERGY) AS E4, "
                StrSql = StrSql + "SUM(DPPACKERGY) AS E5, "
                StrSql = StrSql + "SUM(DPTRNSPTERGY) AS E6, "
                StrSql = StrSql + "SUM(TRSPTTOCUS) AS E7, "
                StrSql = StrSql + "(SUM(RMERGY)+SUM(RMPACKERGY)+SUM(RMANDPACKTRNSPTERGY)+SUM(PROCERGY)+SUM(DPPACKERGY)+SUM(DPTRNSPTERGY)+SUM(TRSPTTOCUS)) AS E8, "
                StrSql = StrSql + "(SUM(RMERGY)+SUM(RMPACKERGY)+SUM(DPPACKERGY)) AS E9, "
                StrSql = StrSql + "SUM(PROCERGY) AS E10, "
                StrSql = StrSql + "(SUM(RMANDPACKTRNSPTERGY)+SUM(DPTRNSPTERGY)+SUM(TRSPTTOCUS)) AS E11, "
                StrSql = StrSql + "(SUM(RMERGY)+SUM(RMPACKERGY)+SUM(RMANDPACKTRNSPTERGY)+SUM(PROCERGY)+SUM(DPPACKERGY)+SUM(DPTRNSPTERGY)+SUM(TRSPTTOCUS)) AS E12, "
                StrSql = StrSql + "'Raw Materials' As ET1, "
                StrSql = StrSql + "'Raw Materials Packaging' AS ET2, "
                StrSql = StrSql + "'RM & Pack Transport' AS ET3, "
                StrSql = StrSql + "'Process' AS ET4, "
                StrSql = StrSql + "'Distribution Packaging' AS ET5, "
                StrSql = StrSql + "'DP Transport' AS ET6, "
                StrSql = StrSql + "'Transport to Customer' AS ET7, "
                StrSql = StrSql + "'Total Energy' AS ET8, "
                StrSql = StrSql + "'Purchased Materials' AS ET9, "
                StrSql = StrSql + "'Process' AS ET10, "
                StrSql = StrSql + "'Transportation' AS ET11, "
                StrSql = StrSql + "'Total Energy' AS ET12, "
                StrSql = StrSql + "SUM(VOLUME)AS VOLUME, "
                StrSql = StrSql + "SUM(FINVOLMUNITS) AS FINVOLMUNITS , "
                StrSql = StrSql + "SUM(FINVOLMSI )AS FINVOLMSI "
                StrSql = StrSql + "FROM S1TOTAL "
                StrSql = StrSql + "INNER JOIN PACKAGINGSPECIFICATIONS "
                StrSql = StrSql + "ON PACKAGINGSPECIFICATIONS.PACKAGINGSPECIFICATIONID = S1TOTAL.PACKAGINGSPECIFICATIONID "
                StrSql = StrSql + "WHERE PACKAGINGSPECIFICATIONS.COMPNAME = '" + CompName + "' "
                If Not IsAll Then
                    If (IsSalesVolOrMsi) Then
                        StrSql = StrSql + "AND FINVOLMUNITS <> 0 "
                    Else
                        StrSql = StrSql + "AND FINVOLMSI <> 0 "
                    End If
                End If




                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
            Catch ex As Exception
                Throw New Exception("Selectdata:GetS1ErgyTotal:-" + ex.Message.ToString())
            End Try
            Return Dts
        End Function



        Public Function GetS1GhgTotal(ByVal SpecId As Integer) As DataSet
            Dim Dts As New DataSet
            Try
                Dim StrSql As String = ""
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")
                StrSql = "SELECT RMGRNHUSGAS AS G1 ,  "
                StrSql = StrSql + "RMPACKGRNHUSGAS AS G2, "
                StrSql = StrSql + "RMANDPACKTRNSPTGRNHUSGAS AS G3, "
                StrSql = StrSql + "PROCGRNHUSGAS AS G4, "
                StrSql = StrSql + "DPPACKGRNHUSGAS AS G5, "
                StrSql = StrSql + "DPTRNSPTGRNHUSGAS AS G6, "
                StrSql = StrSql + "TRSPTTOCUSGRNHUSGAS AS G7, "
                StrSql = StrSql + "(RMGRNHUSGAS+RMPACKGRNHUSGAS+RMANDPACKTRNSPTGRNHUSGAS+PROCGRNHUSGAS+DPPACKGRNHUSGAS+DPTRNSPTGRNHUSGAS+TRSPTTOCUSGRNHUSGAS) AS G8, "
                StrSql = StrSql + "(RMGRNHUSGAS+RMPACKGRNHUSGAS+DPPACKGRNHUSGAS) AS G9, "
                StrSql = StrSql + "PROCGRNHUSGAS AS G10, "
                StrSql = StrSql + "(RMANDPACKTRNSPTGRNHUSGAS+DPTRNSPTGRNHUSGAS+TRSPTTOCUSGRNHUSGAS) AS G11, "
                StrSql = StrSql + "(RMGRNHUSGAS+RMPACKGRNHUSGAS+RMANDPACKTRNSPTGRNHUSGAS+PROCGRNHUSGAS+DPPACKGRNHUSGAS+DPTRNSPTGRNHUSGAS+TRSPTTOCUSGRNHUSGAS) AS G12, "
                StrSql = StrSql + "'Raw Materials' As GT1, "
                StrSql = StrSql + "'Raw Materials Packaging' AS GT2, "
                StrSql = StrSql + "'RM & Pack Transport' AS GT3, "
                StrSql = StrSql + "'Process' AS GT4, "
                StrSql = StrSql + "'Distribution Packaging' AS GT5, "
                StrSql = StrSql + "'DP Transport' AS GT6, "
                StrSql = StrSql + "'Transport to Customer' AS GT7, "
                StrSql = StrSql + "'Total Greenhouse Gas' AS GT8, "
                StrSql = StrSql + "'Purchased Materials' AS GT9, "
                StrSql = StrSql + "'Process' AS GT10, "
                StrSql = StrSql + "'Transportation' AS GT11, "
                StrSql = StrSql + "'Total Greenhouse Gas' AS GT12, "
                StrSql = StrSql + "VOLUME, "
                StrSql = StrSql + "FINVOLMUNITS, "
                StrSql = StrSql + "FINVOLMSI "
                StrSql = StrSql + "FROM S1TOTAL "
                StrSql = StrSql + "WHERE PACKAGINGSPECIFICATIONID = " + SpecId.ToString() + " "


                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
            Catch ex As Exception
                Throw New Exception("Selectdata:GetS1GhgTotal:-" + ex.Message.ToString())
            End Try
            Return Dts
        End Function

        Public Function GetS1AllSpecGhgTotal(ByVal CompName As String, ByVal IsSalesVolOrMsi As Boolean, ByVal IsAll As Boolean) As DataSet
            Dim Dts As New DataSet
            Try
                Dim StrSql As String = ""
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString1 As String = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")
                StrSql = "SELECT SUM(RMGRNHUSGAS) AS G1 ,  "
                StrSql = StrSql + "SUM(RMPACKGRNHUSGAS) AS G2, "
                StrSql = StrSql + "SUM(RMANDPACKTRNSPTGRNHUSGAS) AS G3, "
                StrSql = StrSql + "SUM(PROCGRNHUSGAS) AS G4, "
                StrSql = StrSql + "SUM(DPPACKGRNHUSGAS) AS G5, "
                StrSql = StrSql + "SUM(DPTRNSPTGRNHUSGAS) AS G6, "
                StrSql = StrSql + "SUM(TRSPTTOCUSGRNHUSGAS) AS G7, "
                StrSql = StrSql + "(SUM(RMGRNHUSGAS)+SUM(RMPACKGRNHUSGAS)+SUM(RMANDPACKTRNSPTGRNHUSGAS)+SUM(PROCGRNHUSGAS)+SUM(DPPACKGRNHUSGAS)+SUM(DPTRNSPTGRNHUSGAS)+SUM(TRSPTTOCUSGRNHUSGAS)) AS G8, "
                StrSql = StrSql + "(SUM(RMGRNHUSGAS)+SUM(RMPACKGRNHUSGAS)+SUM(DPPACKGRNHUSGAS)) AS G9, "
                StrSql = StrSql + "SUM(PROCGRNHUSGAS) AS G10, "
                StrSql = StrSql + "(SUM(RMANDPACKTRNSPTGRNHUSGAS)+SUM(DPTRNSPTGRNHUSGAS)+SUM(TRSPTTOCUSGRNHUSGAS)) AS G11, "
                StrSql = StrSql + "(SUM(RMGRNHUSGAS)+SUM(RMPACKGRNHUSGAS)+SUM(RMANDPACKTRNSPTGRNHUSGAS)+SUM(PROCGRNHUSGAS)+SUM(DPPACKGRNHUSGAS)+SUM(DPTRNSPTGRNHUSGAS)+SUM(TRSPTTOCUSGRNHUSGAS)) AS G12, "
                StrSql = StrSql + "'Raw Materials' As GT1, "
                StrSql = StrSql + "'Raw Materials Packaging' AS GT2, "
                StrSql = StrSql + "'RM & Pack Transport' AS GT3, "
                StrSql = StrSql + "'Process' AS GT4, "
                StrSql = StrSql + "'Distribution Packaging' AS GT5, "
                StrSql = StrSql + "'DP Transport' AS GT6, "
                StrSql = StrSql + "'Transport to Customer' AS GT7, "
                StrSql = StrSql + "'Total Greenhouse Gas' AS GT8, "
                StrSql = StrSql + "'Purchased Materials' AS GT9, "
                StrSql = StrSql + "'Process' AS GT10, "
                StrSql = StrSql + "'Transportation' AS GT11, "
                StrSql = StrSql + "'Total Greenhouse Gas' AS GT12, "
                StrSql = StrSql + "SUM(VOLUME)AS VOLUME, "
                StrSql = StrSql + "SUM(FINVOLMUNITS) AS FINVOLMUNITS , "
                StrSql = StrSql + "SUM(FINVOLMSI )AS FINVOLMSI "
                StrSql = StrSql + "FROM S1TOTAL "
                StrSql = StrSql + "INNER JOIN PACKAGINGSPECIFICATIONS "
                StrSql = StrSql + "ON PACKAGINGSPECIFICATIONS.PACKAGINGSPECIFICATIONID = S1TOTAL.PACKAGINGSPECIFICATIONID "
                StrSql = StrSql + "WHERE PACKAGINGSPECIFICATIONS.COMPNAME = '" + CompName + "' "
                If Not IsAll Then
                    If (IsSalesVolOrMsi) Then
                        StrSql = StrSql + "AND FINVOLMUNITS <> 0 "
                    Else
                        StrSql = StrSql + "AND FINVOLMSI <> 0 "
                    End If
                End If

                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString1)
            Catch ex As Exception
                Throw New Exception("Selectdata:GetS1ErgyTotal:-" + ex.Message.ToString())
            End Try
            Return Dts
        End Function
#End Region

#Region "RPF"
        Public Function GetRFPCases(ByVal UserName As String, ByVal DES1 As String, ByVal DES2 As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim CaseIds As String = ""
            Dim SpecConnection As String = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")
            Try
                StrSql = "SELECT  RFPID,DES1, DES2,DES3   "
                StrSql = StrSql + "FROM RFPDETAILS "
                StrSql = StrSql + "WHERE NVL(UPPER(DES1),'#') LIKE '%" + DES1.ToUpper() + "%' "
                StrSql = StrSql + "AND NVL(UPPER(DES2),'#') LIKE '%" + DES2.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY  UPPER(DES1),RFPID "
                Dts = odbUtil.FillDataSet(StrSql, SpecConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SpecGetData:GetRFPCases:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetRFPDetails(ByVal RPFID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim SpecConnection As String = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")
            Try
                StrSql = "SELECT RFPID,DES1,(DES1||' ' ||DES2)DES,DES3,DES2  "
                StrSql = StrSql + "FROM RFPDETAILS "
                StrSql = StrSql + "WHERE RFPID =" + RPFID.ToString() + " "
                StrSql = StrSql + "ORDER BY RFPID "

                Dts = odbUtil.FillDataSet(StrSql, SpecConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("SpecGetData:GetRFPDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

    End Class
End Class
