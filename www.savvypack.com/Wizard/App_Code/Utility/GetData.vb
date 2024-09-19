Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Data
Imports Allied.DataBaseUtility
Namespace Allied.GetData


    Public Class GetData
        Dim connectionStringS As String = System.Configuration.ConfigurationManager.ConnectionStrings("SustainConn").ConnectionString.ToString()
        Dim connectionStringS2 As String = System.Configuration.ConfigurationManager.ConnectionStrings("Sustain2Conn").ConnectionString.ToString()
        Dim connectionStringE1 As String = System.Configuration.ConfigurationManager.ConnectionStrings("EconConn").ConnectionString.ToString()
        Dim connectionStringE2 As String = System.Configuration.ConfigurationManager.ConnectionStrings("Econ2Conn").ConnectionString.ToString()

        Dim connectionStringEChem1 As String = System.Configuration.ConfigurationManager.ConnectionStrings("Echem1Conn").ConnectionString.ToString()
        Dim connectionStringSchem1 As String = System.Configuration.ConfigurationManager.ConnectionStrings("Schem1Conn").ConnectionString.ToString()
        Dim connectionStringEDist As String = System.Configuration.ConfigurationManager.ConnectionStrings("EDistConn").ConnectionString.ToString()
        Dim connectionStringSDist As String = System.Configuration.ConfigurationManager.ConnectionStrings("SDistConn").ConnectionString.ToString()

        Dim connectionStringSMed1 As String = System.Configuration.ConfigurationManager.ConnectionStrings("SMed1Conn").ConnectionString.ToString()
        Dim connectionStringSMed2 As String = System.Configuration.ConfigurationManager.ConnectionStrings("SMed2Conn").ConnectionString.ToString()
        Dim connectionStringMed1 As String = System.Configuration.ConfigurationManager.ConnectionStrings("Med1Conn").ConnectionString.ToString()
        Dim connectionStringMed2 As String = System.Configuration.ConfigurationManager.ConnectionStrings("Med2Conn").ConnectionString.ToString()
		
		Dim connectionStringMold1 As String = System.Configuration.ConfigurationManager.ConnectionStrings("MoldE1Conn").ConnectionString.ToString()
        Dim connectionStringMold2 As String = System.Configuration.ConfigurationManager.ConnectionStrings("MoldE2Conn").ConnectionString.ToString()
        Dim connectionStringSMold1 As String = System.Configuration.ConfigurationManager.ConnectionStrings("MoldS1Conn").ConnectionString.ToString()
        Dim connectionStringSMold2 As String = System.Configuration.ConfigurationManager.ConnectionStrings("MoldS2Conn").ConnectionString.ToString()

        Dim odbutil As New DbUtil.DbUtil()
#Region "Wizard"

        Public Function GetWizrdDetails(ByVal RegionId As String, ByVal Effdate As String) As DataSet
            Dim Ds As New DataSet()
            Try

                Dim StrSql As String = String.Empty
                StrSql = "SELECT  "
                StrSql = StrSql + "GHGID, "
                StrSql = StrSql + "GHGDE1, "
                StrSql = StrSql + "REGIONEID, "
                StrSql = StrSql + "EFFDATE, "
                StrSql = StrSql + "PRODUCTION, "
                StrSql = StrSql + "CO2FRMELEGEN, "
                StrSql = StrSql + "ERGYUSERATIO, "
                StrSql = StrSql + "WATERUSERATIO, "
                StrSql = StrSql + "TRMSIONLOSS, "
                StrSql = StrSql + "LOSATMFGFACI "
                StrSql = StrSql + "FROM WIZARDGHGARCH "
                StrSql = StrSql + "WHERE REGIONEID = " + RegionId.ToString() + " "
                StrSql = StrSql + "AND EFFDATE  =  TO_DATE('" + Effdate.ToString() + "','MON DD,YYYY') ORDER BY GHGID "

                Ds = odbutil.FillDataSet(StrSql, connectionStringS)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetWizrdPDetails() As DataSet
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty
                StrSql = "SELECT  "
                StrSql = StrSql + "0 AS PROD1, "
                StrSql = StrSql + "0 AS PROD2, "
                StrSql = StrSql + "0 AS PROD3, "
                StrSql = StrSql + "0 AS PROD4, "
                StrSql = StrSql + "0 AS PROD5, "
                StrSql = StrSql + "0 AS PROD6, "
                StrSql = StrSql + "0 AS PROD7, "
                StrSql = StrSql + "0 AS PROD8, "
                StrSql = StrSql + "0 AS PROD9, "
                StrSql = StrSql + "0 AS PROD10, "
                StrSql = StrSql + "0 AS GHG1, "
                StrSql = StrSql + "0 AS GHG2, "
                StrSql = StrSql + "0 AS GHG3, "
                StrSql = StrSql + "0 AS GHG4, "
                StrSql = StrSql + "0 AS GHG5, "
                StrSql = StrSql + "0 AS GHG6, "
                StrSql = StrSql + "0 AS GHG7, "
                StrSql = StrSql + "0 AS GHG8, "
                StrSql = StrSql + "0 AS GHG9, "
                StrSql = StrSql + "0 AS GHG10, "
                StrSql = StrSql + "0 AS LMF1, "
                StrSql = StrSql + "0 AS LMF2, "
                StrSql = StrSql + "0 AS LMF3, "
                StrSql = StrSql + "0 AS LMF4, "
                StrSql = StrSql + "0 AS LMF5, "
                StrSql = StrSql + "0 AS LMF6, "
                StrSql = StrSql + "0 AS LMF7, "
                StrSql = StrSql + "0 AS LMF8, "
                StrSql = StrSql + "0 AS LMF9, "
                StrSql = StrSql + "0 AS LMF10, "
                StrSql = StrSql + "0 AS TML1, "
                StrSql = StrSql + "0 AS TML2, "
                StrSql = StrSql + "0 AS TML3, "
                StrSql = StrSql + "0 AS TML4, "
                StrSql = StrSql + "0 AS TML5, "
                StrSql = StrSql + "0 AS TML6, "
                StrSql = StrSql + "0 AS TML7, "
                StrSql = StrSql + "0 AS TML8, "
                StrSql = StrSql + "0 AS TML9, "
                StrSql = StrSql + "0 AS TML10, "
                StrSql = StrSql + "0 AS TGHG1, "
                StrSql = StrSql + "0 AS TGHG2, "
                StrSql = StrSql + "0 AS TGHG3, "
                StrSql = StrSql + "0 AS TGHG4, "
                StrSql = StrSql + "0 AS TGHG5, "
                StrSql = StrSql + "0 AS TGHG6, "
                StrSql = StrSql + "0 AS TGHG7, "
                StrSql = StrSql + "0 AS TGHG8, "
                StrSql = StrSql + "0 AS TGHG9, "
                StrSql = StrSql + "0 AS TGHG10 "
                StrSql = StrSql + "FROM DUAL "
                Ds = odbutil.FillDataSet(StrSql, connectionStringS)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetWizrdPErgyDetails() As DataSet
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty
                StrSql = "SELECT  "
                StrSql = StrSql + "0 AS PROD1, "
                StrSql = StrSql + "0 AS PROD2, "
                StrSql = StrSql + "0 AS PROD3, "
                StrSql = StrSql + "0 AS PROD4, "
                StrSql = StrSql + "0 AS PROD5, "
                StrSql = StrSql + "0 AS PROD6, "
                StrSql = StrSql + "0 AS PROD7, "
                StrSql = StrSql + "0 AS PROD8, "
                StrSql = StrSql + "0 AS PROD9, "
                StrSql = StrSql + "0 AS PROD10, "
                StrSql = StrSql + "0 AS ERGY1, "
                StrSql = StrSql + "0 AS ERGY2, "
                StrSql = StrSql + "0 AS ERGY3, "
                StrSql = StrSql + "0 AS ERGY4, "
                StrSql = StrSql + "0 AS ERGY5, "
                StrSql = StrSql + "0 AS ERGY6, "
                StrSql = StrSql + "0 AS ERGY7, "
                StrSql = StrSql + "0 AS ERGY8, "
                StrSql = StrSql + "0 AS ERGY9, "
                StrSql = StrSql + "0 AS ERGY10, "
                StrSql = StrSql + "0 AS LMF1, "
                StrSql = StrSql + "0 AS LMF2, "
                StrSql = StrSql + "0 AS LMF3, "
                StrSql = StrSql + "0 AS LMF4, "
                StrSql = StrSql + "0 AS LMF5, "
                StrSql = StrSql + "0 AS LMF6, "
                StrSql = StrSql + "0 AS LMF7, "
                StrSql = StrSql + "0 AS LMF8, "
                StrSql = StrSql + "0 AS LMF9, "
                StrSql = StrSql + "0 AS LMF10, "
                StrSql = StrSql + "0 AS TML1, "
                StrSql = StrSql + "0 AS TML2, "
                StrSql = StrSql + "0 AS TML3, "
                StrSql = StrSql + "0 AS TML4, "
                StrSql = StrSql + "0 AS TML5, "
                StrSql = StrSql + "0 AS TML6, "
                StrSql = StrSql + "0 AS TML7, "
                StrSql = StrSql + "0 AS TML8, "
                StrSql = StrSql + "0 AS TML9, "
                StrSql = StrSql + "0 AS TML10, "
                StrSql = StrSql + "0 AS TERGY1, "
                StrSql = StrSql + "0 AS TERGY2, "
                StrSql = StrSql + "0 AS TERGY3, "
                StrSql = StrSql + "0 AS TERGY4, "
                StrSql = StrSql + "0 AS TERGY5, "
                StrSql = StrSql + "0 AS TERGY6, "
                StrSql = StrSql + "0 AS TERGY7, "
                StrSql = StrSql + "0 AS TERGY8, "
                StrSql = StrSql + "0 AS TERGY9, "
                StrSql = StrSql + "0 AS TERGY10 "
                StrSql = StrSql + "FROM DUAL "
                Ds = odbutil.FillDataSet(StrSql, connectionStringS)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetWizrdWaterDetails() As DataSet
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty
                StrSql = "SELECT  "
                StrSql = StrSql + "0 AS PROD1, "
                StrSql = StrSql + "0 AS PROD2, "
                StrSql = StrSql + "0 AS PROD3, "
                StrSql = StrSql + "0 AS PROD4, "
                StrSql = StrSql + "0 AS PROD5, "
                StrSql = StrSql + "0 AS PROD6, "
                StrSql = StrSql + "0 AS PROD7, "
                StrSql = StrSql + "0 AS PROD8, "
                StrSql = StrSql + "0 AS PROD9, "
                StrSql = StrSql + "0 AS PROD10, "

                StrSql = StrSql + "0 AS WATER1, "
                StrSql = StrSql + "0 AS WATER2, "
                StrSql = StrSql + "0 AS WATER3, "
                StrSql = StrSql + "0 AS WATER4, "
                StrSql = StrSql + "0 AS WATER5, "
                StrSql = StrSql + "0 AS WATER6, "
                StrSql = StrSql + "0 AS WATER7, "
                StrSql = StrSql + "0 AS WATER8, "
                StrSql = StrSql + "0 AS WATER9, "
                StrSql = StrSql + "0 AS WATER10, "

                StrSql = StrSql + "0 AS LMF1, "
                StrSql = StrSql + "0 AS LMF2, "
                StrSql = StrSql + "0 AS LMF3, "
                StrSql = StrSql + "0 AS LMF4, "
                StrSql = StrSql + "0 AS LMF5, "
                StrSql = StrSql + "0 AS LMF6, "
                StrSql = StrSql + "0 AS LMF7, "
                StrSql = StrSql + "0 AS LMF8, "
                StrSql = StrSql + "0 AS LMF9, "
                StrSql = StrSql + "0 AS LMF10, "
                StrSql = StrSql + "0 AS TML1, "
                StrSql = StrSql + "0 AS TML2, "
                StrSql = StrSql + "0 AS TML3, "
                StrSql = StrSql + "0 AS TML4, "
                StrSql = StrSql + "0 AS TML5, "
                StrSql = StrSql + "0 AS TML6, "
                StrSql = StrSql + "0 AS TML7, "
                StrSql = StrSql + "0 AS TML8, "
                StrSql = StrSql + "0 AS TML9, "
                StrSql = StrSql + "0 AS TML10, "

                StrSql = StrSql + "0 AS TWATER1, "
                StrSql = StrSql + "0 AS TWATER2, "
                StrSql = StrSql + "0 AS TWATER3, "
                StrSql = StrSql + "0 AS TWATER4, "
                StrSql = StrSql + "0 AS TWATER5, "
                StrSql = StrSql + "0 AS TWATER6, "
                StrSql = StrSql + "0 AS TWATER7, "
                StrSql = StrSql + "0 AS TWATER8, "
                StrSql = StrSql + "0 AS TWATER9, "
                StrSql = StrSql + "0 AS TWATER10 "
                StrSql = StrSql + "FROM DUAL "
                Ds = odbutil.FillDataSet(StrSql, connectionStringS)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetRegione() As DataSet
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty
                StrSql = "SELECT  "
                StrSql = StrSql + "REGIONEID, "
                StrSql = StrSql + "(REGIONEDE1||' '||REGIONEDE2) REGIONEDES "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "WIZARDGEOGRAPHICREGION ORDER BY REGIONEDE1"

                Ds = odbutil.FillDataSet(StrSql, connectionStringS)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetEffdate(ByVal RegionId As String) As DataSet
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty
                StrSql = "SELECT DISTINCT TO_CHAR(EFFDATE,'MON DD,YYYY') EFFDATE "
                StrSql = StrSql + "FROM WIZARDGHGARCH "
                StrSql = StrSql + "WHERE REGIONEID = " + RegionId.ToString() + " "
                Ds = odbutil.FillDataSet(StrSql, connectionStringS)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetMaxEffdate(ByVal RegionId As String) As DataSet
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty
                StrSql = "SELECT TO_CHAR(MAX(EFFDATE),'MON DD,YYYY') EFFDATE "
                StrSql = StrSql + "FROM WIZARDGHGARCH "
                StrSql = StrSql + "WHERE REGIONEID = " + RegionId.ToString() + " "
                Ds = odbutil.FillDataSet(StrSql, connectionStringS)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPreferences(ByVal SessionId As String, ByVal Modules As String) As DataSet
            Dim Ds As New DataSet()
            Dim Conn As String = String.Empty
            Try

                Dim StrSql As String = String.Empty
                StrSql = "SELECT PREFERENCES.CASEID,  "
                StrSql = StrSql + "PREFERENCES.UNITS, "
                StrSql = StrSql + "PREFERENCES.CURRENCY, "
                StrSql = StrSql + "PREFERENCES.CURR, "
                StrSql = StrSql + "PREFERENCES.CONVWT, "
                StrSql = StrSql + "PREFERENCES.CONVTHICK, "
                StrSql = StrSql + "PREFERENCES.OCOUNTRY, "
                StrSql = StrSql + "PREFERENCES.DCOUNTRY, "
                StrSql = StrSql + "PREFERENCES.TITLE1, "
                StrSql = StrSql + "PREFERENCES.TITLE3, "
                StrSql = StrSql + "PREFERENCES.TITLE2, "
                StrSql = StrSql + "PREFERENCES.CONVAREA, "
                StrSql = StrSql + "PREFERENCES.TITLE4, "
                StrSql = StrSql + "PREFERENCES.CONVAREA2, "
                StrSql = StrSql + "PREFERENCES.TITLE5, "
                StrSql = StrSql + "PREFERENCES.CONVTHICK2, "
                StrSql = StrSql + "PREFERENCES.CONVTHICK3, "
                StrSql = StrSql + "PREFERENCES.TITLE6, "
                StrSql = StrSql + "PREFERENCES.TITLE7, "
                StrSql = StrSql + "PREFERENCES.TITLE8, "
                StrSql = StrSql + "PREFERENCES.TITLE9, "
                StrSql = StrSql + "PREFERENCES.TITLE10, "
                StrSql = StrSql + "PREFERENCES.TITLE11, "
                StrSql = StrSql + "PREFERENCES.TITLE12, "
                StrSql = StrSql + "PREFERENCES.CONVGALLON "
                StrSql = StrSql + "FROM PREFERENCES "
                StrSql = StrSql + "INNER JOIN SESSIONVALUES "
                StrSql = StrSql + "ON SESSIONVALUES.CASEID  = PREFERENCES.CASEID "
                StrSql = StrSql + "WHERE SESSIONVALUES.SESSIONID = " + SessionId + " "

                If Modules = "S1" Then
                    Conn = connectionStringS
                ElseIf Modules = "S2" Then
                    Conn = connectionStringS2
                ElseIf Modules.ToUpper() = "SCHEM1" Then
                    Conn = connectionStringSchem1
                ElseIf Modules.ToUpper() = "SDIST" Then
                    Conn = connectionStringSDist
                ElseIf Modules.ToUpper() = "SMED1" Then
                    Conn = connectionStringSMed1
                ElseIf Modules.ToUpper() = "SMED2" Then
                    Conn = connectionStringSMed2
				ElseIf Modules.ToUpper() = "MOLDS1" Then
                    Conn = connectionStringSMold1 
                ElseIf Modules.ToUpper() = "MOLDS2" Then
                    Conn = connectionStringSMold2 
                End If

                Ds = odbutil.FillDataSet(StrSql, Conn)
                Return Ds
            Catch ex As Exception
                Throw New Exception("GetPreferences:" + ex.Message.ToString())
            End Try
        End Function

        Public Function GetPreferencesEcon(ByVal SessionId As String, ByVal Modules As String) As DataSet
            Dim Ds As New DataSet()
            Dim Conn As String = String.Empty
            Try

                Dim StrSql As String = String.Empty
                StrSql = "SELECT PREFERENCES.CASEID,  "
                StrSql = StrSql + "PREFERENCES.UNITS, "
                StrSql = StrSql + "PREFERENCES.CURRENCY, "
                StrSql = StrSql + "PREFERENCES.CURR, "
                StrSql = StrSql + "PREFERENCES.CONVWT, "
                StrSql = StrSql + "PREFERENCES.CONVTHICK, "
                StrSql = StrSql + "PREFERENCES.OCOUNTRY, "
                StrSql = StrSql + "PREFERENCES.DCOUNTRY, "
                StrSql = StrSql + "PREFERENCES.TITLE1, "
                StrSql = StrSql + "PREFERENCES.TITLE3, "
                StrSql = StrSql + "PREFERENCES.TITLE2, "
                StrSql = StrSql + "PREFERENCES.CONVAREA, "
                StrSql = StrSql + "PREFERENCES.TITLE4, "
                StrSql = StrSql + "PREFERENCES.CONVAREA2, "
                StrSql = StrSql + "PREFERENCES.TITLE5, "
                StrSql = StrSql + "PREFERENCES.CONVTHICK2, "
                StrSql = StrSql + "PREFERENCES.CONVTHICK3, "
                StrSql = StrSql + "PREFERENCES.TITLE6, "
                StrSql = StrSql + "PREFERENCES.TITLE7, "
                StrSql = StrSql + "PREFERENCES.TITLE8, "
                StrSql = StrSql + "PREFERENCES.TITLE9, "
                StrSql = StrSql + "PREFERENCES.TITLE10, "
                StrSql = StrSql + "PREFERENCES.TITLE11, "
                StrSql = StrSql + "PREFERENCES.TITLE12 "
                StrSql = StrSql + "FROM PREFERENCES "
                StrSql = StrSql + "INNER JOIN SESSIONVALUES "
                StrSql = StrSql + "ON SESSIONVALUES.CASEID  = PREFERENCES.CASEID "
                StrSql = StrSql + "WHERE SESSIONVALUES.SESSIONID = " + SessionId + " "

                If Modules = "E1" Then
                    Conn = connectionStringE1
                ElseIf Modules = "MED1" Then
                    Conn = connectionStringMed1
                End If

                Ds = odbutil.FillDataSet(StrSql, Conn)
                Return Ds
            Catch ex As Exception
                Throw New Exception("GetPreferencesEcon:" + ex.Message.ToString())
            End Try
        End Function


#End Region

#Region "BuildABox"
        Public Function GetPackageDetails() As DataSet
            Dim Ds As New DataSet()
            Dim Conn As String = String.Empty
            Try
                Dim StrSql As String = String.Empty
                StrSql = "SELECT 'Bottle' AS FORMATDE,  "
                StrSql = StrSql + "1 AS FORMATID, "
                StrSql = StrSql + "'Diameter (in)' AS M1, "
                StrSql = StrSql + "'Height  (in)' AS M2, "
                StrSql = StrSql + "'Not Required' AS M3 "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Can' AS FORMATDE, "
                StrSql = StrSql + "2 AS FORMATID, "
                StrSql = StrSql + "'Diameter (in)' AS M1, "
                StrSql = StrSql + "'Height  (in)' AS M2, "
                StrSql = StrSql + "'Not Required' AS M3 "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Folding Carton' AS FORMATDE, "
                StrSql = StrSql + "3 AS FORMATID, "
                StrSql = StrSql + "'Length (in)' AS M1, "
                StrSql = StrSql + "'Width (in)' AS M2, "
                StrSql = StrSql + "'Depth  (in)' AS M3 "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "ORDER BY 1 "

                Ds = odbutil.FillDataSet(StrSql, connectionStringE1)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetCartonFluteType() As DataSet
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty
                StrSql = "SELECT FLUTEID,  "
                StrSql = StrSql + "FLUTEDE, "
                StrSql = StrSql + "FLUTECOST, "
                StrSql = StrSql + "FLUTEWEIGHT "
                StrSql = StrSql + "FROM FLUTEDETAILS "


                Ds = odbutil.FillDataSet(StrSql, connectionStringE1)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetConversionFactor() As DataSet
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty
                StrSql = "SELECT  MICPMIL,  "
                StrSql = StrSql + "KGPLB, "
                StrSql = StrSql + "M2PMSI, "
                StrSql = StrSql + "M2PSQFT, "
                StrSql = StrSql + "MPFT, "
                StrSql = StrSql + "KMPMILE, "
                StrSql = StrSql + "JPMJ, "
                StrSql = StrSql + "LITPGAL, "
                StrSql = StrSql + "IN2PSQFT "
                StrSql = StrSql + "FROM CONVFACTORS "


                Ds = odbutil.FillDataSet(StrSql, connectionStringE1)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetBbPreferences(ByVal SessionId As String, ByVal Modules As String) As DataSet
            Dim Ds As New DataSet()
            Dim Conn As String = String.Empty
            Try

                Dim StrSql As String = String.Empty
                StrSql = "SELECT PREFERENCES.CASEID,  "
                StrSql = StrSql + "PREFERENCES.UNITS, "
                StrSql = StrSql + "PREFERENCES.CONVTHICK, "
                StrSql = StrSql + "PREFERENCES.CONVAREA, "
                StrSql = StrSql + "PREFERENCES.CONVWT, "
                StrSql = StrSql + "PREFERENCES.CONVAREA2, "
                StrSql = StrSql + "PREFERENCES.CONVTHICK2, "
                StrSql = StrSql + "PREFERENCES.CONVTHICK3 "
                StrSql = StrSql + "FROM PREFERENCES "
                StrSql = StrSql + "INNER JOIN SESSIONVALUES "
                StrSql = StrSql + "ON SESSIONVALUES.CASEID  = PREFERENCES.CASEID "
                StrSql = StrSql + "WHERE SESSIONVALUES.SESSIONID = " + SessionId + " "

                If Modules = "S1" Then
                    Conn = connectionStringS
                ElseIf Modules = "S2" Then
                    Conn = connectionStringS2
                ElseIf Modules = "E1" Then
                    Conn = connectionStringE1
                ElseIf Modules = "E2" Then
                    Conn = connectionStringE2
                ElseIf Modules.ToUpper() = "SCHEM1" Then
                    Conn = connectionStringSchem1
                ElseIf Modules.ToUpper() = "SDIST" Then
                    Conn = connectionStringSDist
                ElseIf Modules.ToUpper() = "ECHEM1" Then
                    Conn = connectionStringEChem1
                ElseIf Modules.ToUpper() = "EDIST" Then
                    Conn = connectionStringEDist
                ElseIf Modules.ToUpper() = "MED1" Then
                    Conn = connectionStringMed1
                ElseIf Modules.ToUpper() = "MED2" Then
                    Conn = connectionStringMed2
                ElseIf Modules.ToUpper() = "SMED1" Then
                    Conn = connectionStringSMed1
                ElseIf Modules.ToUpper() = "SMED2" Then
                    Conn = connectionStringSMed2
				ElseIf Modules.ToUpper() = "MOLDS1" Then
                    Conn = connectionStringSMold1 
                ElseIf Modules.ToUpper() = "MOLDS2" Then
                    Conn = connectionStringSMold2 
                End If

                Ds = odbutil.FillDataSet(StrSql, Conn)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetEPreferences(ByVal SessionId As String, ByVal Modules As String) As DataSet
            Dim Ds As New DataSet()
            Dim Conn As String = String.Empty
            Try

                Dim StrSql As String = String.Empty
                StrSql = "SELECT PREFERENCES.CURR,  "
                StrSql = StrSql + "PREFERENCES.TITLE2 "
                StrSql = StrSql + "FROM PREFERENCES "
                StrSql = StrSql + "INNER JOIN SESSIONVALUES "
                StrSql = StrSql + "ON SESSIONVALUES.CASEID  = PREFERENCES.CASEID "
                StrSql = StrSql + "WHERE SESSIONVALUES.SESSIONID = " + SessionId + " "


                If Modules = "E1" Then
                    Conn = connectionStringE1
                ElseIf Modules = "E2" Then
                    Conn = connectionStringE2
                ElseIf Modules.ToUpper() = "ECHEM1" Then
                    Conn = connectionStringEChem1
                ElseIf Modules.ToUpper() = "EDIST" Then
                    Conn = connectionStringEDist
                ElseIf Modules.ToUpper() = "MED1" Then
                    Conn = connectionStringMed1
                ElseIf Modules.ToUpper() = "MED2" Then
                    Conn = connectionStringMed2
                End If


                Ds = odbutil.FillDataSet(StrSql, Conn)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetLCIData(ByVal SessionId As String, ByVal Modules As String) As DataSet
            Dim Ds As New DataSet()
            Dim DsPref As New DataSet()
            Dim Conn As String = String.Empty
            Try

                Dim StrSql As String = String.Empty
                Dim StrSqlPref As String = String.Empty

                StrSqlPref = "SELECT PREFERENCES.INVENTORYTYPE,  "
                StrSqlPref = StrSqlPref + "TO_CHAR(PREFERENCES.EFFDATE,'MM/DD/YYYY')EFFDATE "
                StrSqlPref = StrSqlPref + "FROM PREFERENCES "
                StrSqlPref = StrSqlPref + "INNER JOIN SESSIONVALUES "
                StrSqlPref = StrSqlPref + "ON SESSIONVALUES.CASEID  = PREFERENCES.CASEID "
                StrSqlPref = StrSqlPref + "WHERE SESSIONVALUES.SESSIONID = " + SessionId + " "

                If Modules = "S1" Then
                    Conn = connectionStringS
                ElseIf Modules = "S2" Then
                    Conn = connectionStringS2
                ElseIf Modules.ToUpper() = "SCHEM1" Then
                    Conn = connectionStringSchem1
                ElseIf Modules.ToUpper() = "SDIST" Then
                    Conn = connectionStringSDist
                ElseIf Modules.ToUpper() = "SMED1" Then
                    Conn = connectionStringSMed1
                ElseIf Modules.ToUpper() = "SMED2" Then
                    Conn = connectionStringSMed2
				ElseIf Modules.ToUpper() = "MOLDS1" Then
                    Conn = connectionStringSMold1 
                ElseIf Modules.ToUpper() = "MOLDS2" Then
                    Conn = connectionStringSMold2 
                End If

                DsPref = odbutil.FillDataSet(StrSqlPref, Conn)

                StrSql = "SELECT PALLETARCH.PALLETDE1,  "
                StrSql = StrSql + "PALLETARCH.PRICE AS GHG, "
                StrSql = StrSql + "PALLETARCH.JOULE AS ERGY "
                StrSql = StrSql + "FROM PALLETARCH "
                StrSql = StrSql + "WHERE PALLETARCH.INVENTORYTYPE = " + DsPref.Tables(0).Rows(0).Item("INVENTORYTYPE").ToString() + " "
                StrSql = StrSql + "AND PALLETARCH.EFFDATE = TO_DATE('" + DsPref.Tables(0).Rows(0).Item("EFFDATE").ToString() + "','MM/DD/YYYY') "
                StrSql = StrSql + "AND PALLETARCH.PALLETID = 25 "


                Ds = odbutil.FillDataSet(StrSql, connectionStringS)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Messages"
        Public Function GetMessages(ByVal UserId As String) As DataSet
            Dim Ds As New DataSet()
            Try

                Dim StrSql As String = String.Empty
                StrSql = "SELECT MESSAGES.MESSAGEID,  "
                StrSql = StrSql + "MESSAGES.MESSAGEHEAD, "
                StrSql = StrSql + "('mailto:sam@allied-dev.com?Subject=Feedback On '||MESSAGEHEAD||'&'||'CC=emm@allied-dev.com') AS FEEDBACK, "
                StrSql = StrSql + "MESSAGES.MESSAGETEXT, "
                StrSql = StrSql + "TO_CHAR(MESSAGES.SERVERDATE,'MON DD,YYYY')SERVERDATE, "
                StrSql = StrSql + "SERVERDATE AS SERVERDATE2 "
                StrSql = StrSql + "FROM MESSAGES "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 "
                StrSql = StrSql + "FROM USEERDELETDMSG "
                StrSql = StrSql + "WHERE USEERDELETDMSG.MESSAGEID = MESSAGES.MESSAGEID "
                StrSql = StrSql + "AND USEERDELETDMSG.USERID = " + UserId.ToString() + ""
                StrSql = StrSql + ") "
                StrSql = StrSql + "AND MESSAGES.EXPIRYDATE >= SYSDATE "
                StrSql = StrSql + "ORDER BY SERVERDATE2 DESC "
                Ds = odbutil.FillDataSet(StrSql, connectionStringE1)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "EnergyWizard"
        Public Function GetCaseId(ByVal SessionId As String, ByVal Modules As String) As DataSet
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty
                Dim Conn As String = ""
                StrSql = "SELECT CASEID FROM SESSIONVALUES WHERE SESSIONID= " + SessionId

                If Modules = "S1" Then
                    Conn = connectionStringS
                ElseIf Modules = "S2" Then
                    Conn = connectionStringS2
				ElseIf Modules.ToUpper() = "MOLDS1" Then
                    Conn = connectionStringSMold1 
                ElseIf Modules.ToUpper() = "MOLDS2" Then
                    Conn = connectionStringSMold2 
                End If

                Ds = odbutil.FillDataSet(StrSql, Conn)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Function GetWizrdPrefferedDetails_OLD(ByVal SessionId As String, ByVal Modules As String) As DataSet
            Dim Ds As New DataSet()
            Dim Conn As String = String.Empty
            Try
                Dim StrSql As String = String.Empty
                StrSql = "SELECT REGIONID, TO_CHAR(EFFDATE,'MON DD,YYYY') EFFDATE, PRODUCTION1, PRODUCTION2, PRODUCTION3, PRODUCTION4, PRODUCTION5, "
                StrSql = StrSql + "PRODUCTION6, PRODUCTION7, PRODUCTION8, PRODUCTION9, PRODUCTION10, "
                StrSql = StrSql + "CO2USERATIO1, CO2USERATIO2, CO2USERATIO3, CO2USERATIO4, CO2USERATIO5, CO2USERATIO6, CO2USERATIO7, "
                StrSql = StrSql + "CO2USERATIO8, CO2USERATIO9, CO2USERATIO10, ERGYUSERATIO1, ERGYUSERATIO2, ERGYUSERATIO3, ERGYUSERATIO4, "
                StrSql = StrSql + "ERGYUSERATIO5, ERGYUSERATIO6, ERGYUSERATIO7, ERGYUSERATIO8, ERGYUSERATIO9, ERGYUSERATIO10, "
                StrSql = StrSql + "WATERUSERATIO1, WATERUSERATIO2, WATERUSERATIO3, WATERUSERATIO4, WATERUSERATIO5, WATERUSERATIO6, WATERUSERATIO7, "
                StrSql = StrSql + "WATERUSERATIO8, WATERUSERATIO9, WATERUSERATIO10, TRANSLOSS1, TRANSLOSS2, TRANSLOSS3, TRANSLOSS4, TRANSLOSS5, "
                StrSql = StrSql + "TRANSLOSS6, TRANSLOSS7, TRANSLOSS8, TRANSLOSS9, TRANSLOSS10, LOSSMFGFACI1, LOSSMFGFACI2, LOSSMFGFACI3, "
                StrSql = StrSql + "LOSSMFGFACI4, LOSSMFGFACI5, LOSSMFGFACI6, LOSSMFGFACI7, LOSSMFGFACI8, LOSSMFGFACI9, LOSSMFGFACI10, "
                StrSql = StrSql + "TOTCO21, TOTCO22, TOTCO23, TOTCO24, TOTCO25, TOTCO26, TOTCO27, TOTCO28, TOTCO29, TOTCO210, "
                StrSql = StrSql + "TOTERGY1, TOTERGY2, TOTERGY3, TOTERGY4, TOTERGY5, TOTERGY6, TOTERGY7, TOTERGY8, TOTERGY9, TOTERGY10,"
                StrSql = StrSql + "TOTWATER1, TOTWATER2, TOTWATER3, TOTWATER4, TOTWATER5, TOTWATER6, TOTWATER7, TOTWATER8, TOTWATER9, TOTWATER10 "
                StrSql = StrSql + "FROM WIZARDDETAILS "
                StrSql = StrSql + "WHERE CASEID=" + SessionId

                If Modules = "S1" Then
                    Conn = connectionStringS
                ElseIf Modules = "S2" Then
                    Conn = connectionStringS2
                ElseIf Modules.ToUpper() = "MOLDS1" Then
                    Conn = connectionStringSMold1
                ElseIf Modules.ToUpper() = "MOLDS2" Then
                    Conn = connectionStringSMold2
                End If
                Ds = odbutil.FillDataSet(StrSql, Conn)
                Return Ds
            Catch ex As Exception
                Throw New Exception("GetWizrdPrefferedDetails:" + ex.Message.ToString())
            End Try
        End Function

        Public Function GetWizrdPrefferedDetails(ByVal CaseID As String, ByVal Modules As String, ByVal effDate As String, ByVal regID As String) As DataSet
            Dim Ds As New DataSet()
            Dim Conn As String = String.Empty
            Dim StrSql As String = String.Empty
            Dim StrSql1 As String = String.Empty
            Try
                StrSql1 = "SELECT * FROM WIZARDDETAILS "
                StrSql1 = StrSql1 + "WHERE CASEID=" + CaseID + " "

                If Modules = "S1" Then
                    Conn = connectionStringS
                ElseIf Modules = "S2" Then
                    Conn = connectionStringS2
                ElseIf Modules.ToUpper() = "MOLDS1" Then
                    Conn = connectionStringSMold1
                ElseIf Modules.ToUpper() = "MOLDS2" Then
                    Conn = connectionStringSMold2
                End If
                Ds = odbutil.FillDataSet(StrSql1, Conn)

                If Ds.Tables(0).Rows.Count = 0 Then
                    StrSql = "INSERT INTO WIZARDDETAILS(CASEID,REGIONID,EFFDATE) SELECT " + CaseID + "," + regID + ", TO_DATE('" + effDate + "','MM?DD/YYYY') FROM DUAL WHERE NOT EXISTS( SELECT 1 FROM  WIZARDDETAILS WHERE CASEID=" + CaseID + " AND REGIONID=" + regID + " AND EFFDATE  =  TO_DATE('" + effDate.ToString() + "','MON DD,YYYY'))"
                    If Modules = "S1" Then
                        Conn = connectionStringS
                    ElseIf Modules = "S2" Then
                        Conn = connectionStringS2
                    End If
                    odbutil.UpIns(StrSql, Conn)
                End If
                StrSql = "SELECT REGIONID, TO_CHAR(EFFDATE,'MON DD,YYYY') EFFDATE, PRODUCTION1, PRODUCTION2, PRODUCTION3, PRODUCTION4, PRODUCTION5, "
                StrSql = StrSql + "PRODUCTION6, PRODUCTION7, PRODUCTION8, PRODUCTION9, PRODUCTION10, "
                StrSql = StrSql + "CO2USERATIO1, CO2USERATIO2, CO2USERATIO3, CO2USERATIO4, CO2USERATIO5, CO2USERATIO6, CO2USERATIO7, "
                StrSql = StrSql + "CO2USERATIO8, CO2USERATIO9, CO2USERATIO10, ERGYUSERATIO1, ERGYUSERATIO2, ERGYUSERATIO3, ERGYUSERATIO4, "
                StrSql = StrSql + "ERGYUSERATIO5, ERGYUSERATIO6, ERGYUSERATIO7, ERGYUSERATIO8, ERGYUSERATIO9, ERGYUSERATIO10, "
                StrSql = StrSql + "WATERUSERATIO1, WATERUSERATIO2, WATERUSERATIO3, WATERUSERATIO4, WATERUSERATIO5, WATERUSERATIO6, WATERUSERATIO7, "
                StrSql = StrSql + "WATERUSERATIO8, WATERUSERATIO9, WATERUSERATIO10, TRANSLOSS1, TRANSLOSS2, TRANSLOSS3, TRANSLOSS4, TRANSLOSS5, "
                StrSql = StrSql + "TRANSLOSS6, TRANSLOSS7, TRANSLOSS8, TRANSLOSS9, TRANSLOSS10, LOSSMFGFACI1, LOSSMFGFACI2, LOSSMFGFACI3, "
                StrSql = StrSql + "LOSSMFGFACI4, LOSSMFGFACI5, LOSSMFGFACI6, LOSSMFGFACI7, LOSSMFGFACI8, LOSSMFGFACI9, LOSSMFGFACI10, "
                StrSql = StrSql + "TOTCO21, TOTCO22, TOTCO23, TOTCO24, TOTCO25, TOTCO26, TOTCO27, TOTCO28, TOTCO29, TOTCO210, "
                StrSql = StrSql + "TOTERGY1, TOTERGY2, TOTERGY3, TOTERGY4, TOTERGY5, TOTERGY6, TOTERGY7, TOTERGY8, TOTERGY9, TOTERGY10,"
                StrSql = StrSql + "TOTWATER1, TOTWATER2, TOTWATER3, TOTWATER4, TOTWATER5, TOTWATER6, TOTWATER7, TOTWATER8, TOTWATER9, TOTWATER10 "
                StrSql = StrSql + "FROM WIZARDDETAILS "
                StrSql = StrSql + "WHERE CASEID=" + CaseID + " "

                If Modules = "S1" Then
                    Conn = connectionStringS
                ElseIf Modules = "S2" Then
                    Conn = connectionStringS2
                ElseIf Modules.ToUpper() = "MOLDS1" Then
                    Conn = connectionStringSMold1
                ElseIf Modules.ToUpper() = "MOLDS2" Then
                    Conn = connectionStringSMold2
                End If
                Ds = odbutil.FillDataSet(StrSql, Conn)
                Return Ds
            Catch ex As Exception
                Throw New Exception("GetWizrdPrefferedDetails:" + ex.Message.ToString())
            End Try
        End Function
#End Region







    End Class
End Namespace
