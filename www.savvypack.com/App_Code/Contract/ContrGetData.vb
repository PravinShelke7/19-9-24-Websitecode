Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System

Public Class ContrGetData
    Public Class Selectdata

        Dim ContractConnection As String = System.Configuration.ConfigurationManager.AppSettings("ContractConnectionString")
		Dim odbUtil As New DBUtil()
		Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
		
#Region "Error"
        Public Function GetErrors(ByVal ErrorCode As String) As DataSet
            Dim Dts As New DataSet()
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("ContractConnectionString")
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
        Public Function GetCompanyData(ByVal des As String, ByVal UserId As String, ByVal CountryId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT ID,NAME FROM  "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 0 AS ID, "
                StrSql = StrSql + "'none' AS NAME "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT COMPANYID ID, "
                StrSql = StrSql + "NAME AS NAME "
                StrSql = StrSql + "FROM COMPANIES "
                StrSql = StrSql + "INNER JOIN USERCOUNTRIES "
                StrSql = StrSql + "ON USERCOUNTRIES.COUNTRYID = COMPANIES.COUNTRYID "
                StrSql = StrSql + "WHERE USERCOUNTRIES.USERID =" + UserId + " AND COMPANYID <> 0 "
                If CountryId <> "-1" Then
                    StrSql = StrSql + "AND USERCOUNTRIES.COUNTRYID =" + CountryId + " "
                End If
                StrSql = StrSql + ") "
                StrSql = StrSql + "WHERE NVL(UPPER(NAME),'#') LIKE '" + des.ToUpper().Replace("'", "''") + "%' "
                StrSql = StrSql + "ORDER BY NAME "


                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetCompanyData:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCountryDetails(ByVal des As String, ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT COUNTRYID,NAME,SHORTNAME FROM  "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT -1 AS COUNTRYID, "
                StrSql = StrSql + "'All' AS NAME, "
                StrSql = StrSql + "'' AS SHORTNAME "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 0 AS COUNTRYID, "
                StrSql = StrSql + "'none' AS NAME, "
                StrSql = StrSql + "'' AS SHORTNAME "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT DIMCOUNTRIES.COUNTRYID, "
                StrSql = StrSql + "DIMCOUNTRIES.COUNTRYDES NAME, "
                StrSql = StrSql + "DIMCOUNTRIES.ABBREVIATIONS SHORTNAME "
                StrSql = StrSql + "FROM ADMINSITE.DIMCOUNTRIES "
                StrSql = StrSql + "INNER JOIN USERCOUNTRIES "
                StrSql = StrSql + "ON USERCOUNTRIES.COUNTRYID = DIMCOUNTRIES.COUNTRYID "
                StrSql = StrSql + "WHERE USERCOUNTRIES.USERID = " + UserId + " "
                StrSql = StrSql + ") "
                StrSql = StrSql + "WHERE NVL(UPPER(NAME),'#') LIKE '" + des.ToUpper().Replace("'", "''") + "%' "
                StrSql = StrSql + "ORDER BY NAME "

                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetCountryDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetStateDetails(ByVal des As String, ByVal CountryId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT STATEID,ID,NAME FROM  "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 0 AS STATEID, "
                StrSql = StrSql + "0 AS ID, "
                StrSql = StrSql + "0 AS SEQ, "
                StrSql = StrSql + "'none' AS NAME "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT STATEID, "
                StrSql = StrSql + "COUNTRYID AS ID, "
                StrSql = StrSql + "1 AS SEQ, "
                StrSql = StrSql + "NAME "
                StrSql = StrSql + "FROM ADMINSITE.STATE "
                StrSql = StrSql + "WHERE COUNTRYID=" + CountryId + " "
                StrSql = StrSql + ") "
                StrSql = StrSql + "WHERE NVL(UPPER(NAME),'#') LIKE '" + des.ToUpper().Replace("'", "''") + "%' "
                StrSql = StrSql + "ORDER BY SEQ,NAME "

                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetStateDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCategoryByDetails(ByVal catDetails As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
           
                StrSql = "SELECT 0 AS CATEGORYID,  "
                StrSql = StrSql + "0 AS ID, "
                StrSql = StrSql + "'none' AS DATA, "
                StrSql = StrSql + "' ' AS DETAILS "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CAT.CATEGORYID, "
                StrSql = StrSql + "CATD.CATEGORYDETAILID AS ID, "
                StrSql = StrSql + "CATD.DETAILS AS DATA, "
                StrSql = StrSql + "CATD.DETAILS "
                StrSql = StrSql + "FROM CATEGORIES  CAT "
                StrSql = StrSql + "INNER JOIN CATEGORYDETAILS CATD "
                StrSql = StrSql + "ON CAT.CATEGORYID=CATD.CATEGORYID "
                StrSql = StrSql + "AND CAT.CODE='" + catDetails + "' ORDER BY DETAILS "


                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetCategoryByID:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCategoryDetailsByText(ByVal catDetails As String, ByVal des As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT ID,DATA FROM  "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 0 AS CATEGORYID,'0:none' AS DETAILS,0 AS ID,'None' AS DATA  FROM DUAL "
                StrSql = StrSql + "UNION SELECT CAT.CATEGORYID,CATD.CATEGORYDETAILID || ':' ||CATD.DETAILS AS DETAILS, "
                StrSql = StrSql + "CATD.CATEGORYDETAILID AS ID,CATD.DETAILS AS DATA FROM CATEGORIES  CAT "
                StrSql = StrSql + "INNER JOIN CATEGORYDETAILS CATD ON CAT.CATEGORYID=CATD.CATEGORYID "
                StrSql = StrSql + "AND CAT.CODE='" + catDetails + "' "
                StrSql = StrSql + ") "
                StrSql = StrSql + "WHERE NVL(UPPER(DATA),'#') LIKE '" + des.ToUpper().Replace("'", "''") + "%' "
                StrSql = StrSql + "ORDER BY DATA "

                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetCategoryDetailsByText:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetSelectionDataByUser(ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT COMPANY,PRODUCT,COUNTRY,STATE,SERVICE,DESIGN,PROCESS,MACHINE,CUSTOMER FROM SELECTION WHERE USERID =" + UserId.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetSelectionDataByUser:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompanyDetailsByCountry_ORG(ByVal CountryId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT COMPANIES.COMPANYID AS ID,REPLACE(COMPANIES.NAME,'''','##') NAME   FROM COMPANIES    "
                StrSql = StrSql + "WHERE COMPANIES.COUNTRYID='" + CountryId + "' ORDER BY COMPANIES.NAME "
                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetCompanyDetailsByState:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompanyDetailsByCountry(ByVal CountryId As String, ByVal userId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT COMPANIES.COMPANYID AS ID,REPLACE(COMPANIES.NAME,'''','##') NAME   FROM COMPANIES    "
                If CountryId = "-1" Then
                    StrSql = StrSql + "WHERE COMPANIES.COUNTRYID IN (SELECT COUNTRYID FROM USERCOUNTRIES WHERE USERID=" + userId.ToString() + ") ORDER BY COMPANIES.NAME "
                Else
                    StrSql = StrSql + "WHERE COMPANIES.COUNTRYID='" + CountryId + "' ORDER BY COMPANIES.NAME "
                End If
                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetCompanyDetailsByCountry:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompanyDetailsByState(ByVal stateId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT COMPANIES.COMPANYID AS ID,REPLACE(COMPANIES.NAME,'''','##') NAME   FROM COMPANIES    "
                StrSql = StrSql + "WHERE COMPANIES.STATEID='" + stateId + "' ORDER BY COMPANIES.NAME "
                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetCompanyDetailsByState:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompanyDetailsByCategory_ORG(ByVal catId As String, ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT C1.COMPANYID,REPLACE(COMPANIES.NAME,'''','##') NAME ,C1.CATEGORYID FROM COMPANYCATEGORIES C1  "
                StrSql = StrSql + "INNER JOIN CATEGORYDETAILS C2 ON C1.CATEGORYDETAILID=C2.CATEGORYDETAILID "
                StrSql = StrSql + "INNER JOIN COMPANIES ON COMPANIES.COMPANYID=C1.COMPANYID "
                StrSql = StrSql + "INNER JOIN USERCOUNTRIES ON USERCOUNTRIES.COUNTRYID = COMPANIES.COUNTRYID "
                StrSql = StrSql + "WHERE C1.CATEGORYDETAILID='" + catId + "' "
                StrSql = StrSql + "AND USERCOUNTRIES.USERID='" + UserId + "' "
                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetCompanyDetailsByCategory:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
 Public Function GetCompanyDetailsByCategory(ByVal countryId As String, ByVal catId As String, ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT C1.COMPANYID,REPLACE(COMPANIES.NAME,'''','##') NAME ,C1.CATEGORYID FROM COMPANYCATEGORIES C1  "
                StrSql = StrSql + "INNER JOIN CATEGORYDETAILS C2 ON C1.CATEGORYDETAILID=C2.CATEGORYDETAILID "
                StrSql = StrSql + "INNER JOIN COMPANIES ON COMPANIES.COMPANYID=C1.COMPANYID "
                StrSql = StrSql + "INNER JOIN USERCOUNTRIES ON USERCOUNTRIES.COUNTRYID = COMPANIES.COUNTRYID "
                StrSql = StrSql + "WHERE C1.CATEGORYDETAILID='" + catId + "' "
                StrSql = StrSql + "AND USERCOUNTRIES.USERID='" + UserId + "' "
                If countryId <> "-1" Then
                    StrSql = StrSql + "AND COMPANIES.COUNTRYID='" + countryId + "' "
                End If
                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetCompanyDetailsByCategory:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetSelectionDetails(ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT NVL(COMP.NAME,'None') AS COMPNAME,  "
                StrSql = StrSql + "NVL(PROD.DETAILS,'None') AS PRODAREA, "
                StrSql = StrSql + "NVL(PRODSER.DETAILS,'None') AS PROSER , "
                StrSql = StrSql + "NVL(PRODEVCAP.DETAILS,'None') AS PRODEVCAP , "
                StrSql = StrSql + "NVL(PROCAP.DETAILS,'None') AS PROCAP , "
                StrSql = StrSql + "NVL(MACH.DETAILS,'None') AS MACH, "
                StrSql = StrSql + "NVL(REPCUS.DETAILS,'None') AS REPCUS, "
                StrSql = StrSql + "NVL(STAT.NAME,'None') AS STAT "
                StrSql = StrSql + "FROM SELECTION SEL "
                StrSql = StrSql + "LEFT JOIN COMPANIES COMP "
                StrSql = StrSql + "ON COMP.COMPANYID= SEL.COMPANY "
                StrSql = StrSql + "LEFT JOIN ADMINSITE.STATE STAT "
                StrSql = StrSql + "ON STAT.STATEID= SEL.STATE "
                StrSql = StrSql + "LEFT JOIN CATEGORYDETAILS PROD "
                StrSql = StrSql + "ON PROD.CATEGORYDETAILID=SEL.PRODUCT "
                StrSql = StrSql + "LEFT JOIN CATEGORYDETAILS PRODSER "
                StrSql = StrSql + "ON PRODSER.CATEGORYDETAILID=SEL.SERVICE "
                StrSql = StrSql + "LEFT JOIN CATEGORYDETAILS PRODEVCAP "
                StrSql = StrSql + "ON PRODEVCAP.CATEGORYDETAILID=SEL.DESIGN "
                StrSql = StrSql + "LEFT JOIN CATEGORYDETAILS PROCAP "
                StrSql = StrSql + "ON PROCAP.CATEGORYDETAILID=SEL.PROCESS "
                StrSql = StrSql + "LEFT JOIN CATEGORYDETAILS MACH "
                StrSql = StrSql + "ON MACH.CATEGORYDETAILID=SEL.MACHINE "
                StrSql = StrSql + "LEFT JOIN CATEGORYDETAILS REPCUS "
                StrSql = StrSql + "ON REPCUS.CATEGORYDETAILID=SEL.CUSTOMER "
                StrSql = StrSql + "WHERE USERID =" + UserId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetSelectionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSearchResults(ByVal UserId As String, ByVal criteria As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT COMP.COMPANYID,  "
                StrSql = StrSql + "COMP.NAME "
                StrSql = StrSql + "FROM COMPANIES COMP "
                StrSql = StrSql + "INNER JOIN RESULTS RES "
                StrSql = StrSql + "ON COMP.COMPANYID  = RES.COMPANY "
                StrSql = StrSql + "WHERE RES.USERID =" + UserId.ToString() + "  "
                StrSql = StrSql + "AND UPPER(RES.CRITERIA)= '" + criteria.ToUpper() + "' "
                StrSql = StrSql + "ORDER BY COMP.NAME "


                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetSearchResults:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetLogicORSearchResults(ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT 'Company' CATEGORY,  "
                StrSql = StrSql + " COMPANIES.NAME  DETAILS, "
                StrSql = StrSql + "'COMPANY' AS CODE, "
                StrSql = StrSql + "0 SEQ "
                StrSql = StrSql + "FROM COMPANIES "
                StrSql = StrSql + "INNER JOIN SELECTION "
                StrSql = StrSql + "ON COMPANIES.COMPANYID = SELECTION.COMPANY "
                StrSql = StrSql + "AND SELECTION.USERID=" + UserId.ToString() + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Country' CATEGORY , "
                StrSql = StrSql + "DIMCOUNTRIES.COUNTRYDES  DETAILS, "
                StrSql = StrSql + "'COUNTRY' AS CODE, "
                StrSql = StrSql + "1 SEQ "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "ADMINSITE.DIMCOUNTRIES "
                StrSql = StrSql + "INNER JOIN SELECTION "
                StrSql = StrSql + "ON SELECTION.COUNTRY = DIMCOUNTRIES.COUNTRYID "
                StrSql = StrSql + "AND SELECTION.USERID=" + UserId.ToString() + " "
                'StrSql = StrSql + "AND UPPER(USERNAME)='" + userName.ToUpper() + "' "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT CATEGORY, "
                StrSql = StrSql + "DETAILS, "
                StrSql = StrSql + "CODE, "
                StrSql = StrSql + "SEQ "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 'Product Type' CATEGORY, "
                StrSql = StrSql + "PRODUCT VALUE, "
                StrSql = StrSql + "'PRODUCT' AS CODE, "
                StrSql = StrSql + "3 SEQ "
                StrSql = StrSql + "FROM  SELECTION WHERE USERID=" + UserId.ToString() + " "
                'StrSql = StrSql + "FROM  SELECTION WHERE UPPER(USERNAME)='" + userName.ToUpper() + "' "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Product Services' DATA, "
                StrSql = StrSql + "SERVICE VALUE, "
                StrSql = StrSql + "'SERVICE' AS CODE, "
                StrSql = StrSql + "4 SEQ "
                StrSql = StrSql + "FROM  SELECTION WHERE USERID=" + UserId.ToString() + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Product Development Capabilities' DATA, "
                StrSql = StrSql + "DESIGN VALUE, "
                StrSql = StrSql + "'DESIGN' AS CODE, "
                StrSql = StrSql + "5 SEQ "
                StrSql = StrSql + "FROM  SELECTION WHERE USERID=" + UserId.ToString() + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Processing Capabilities' DATA, "
                StrSql = StrSql + "PROCESS VALUE, "
                StrSql = StrSql + "'PROCESS' AS CODE, "
                StrSql = StrSql + "6 SEQ "
                StrSql = StrSql + "FROM  SELECTION WHERE USERID=" + UserId.ToString() + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Machinery Systems' DATA, "
                StrSql = StrSql + "MACHINE VALUE, "
                StrSql = StrSql + "'MACHINE' AS CODE, "
                StrSql = StrSql + "7 SEQ "
                StrSql = StrSql + "FROM  SELECTION WHERE USERID=" + UserId.ToString() + " "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Representative Customers' DATA, "
                StrSql = StrSql + "CUSTOMER VALUE, "
                StrSql = StrSql + "'CUSTOMER' AS CODE, "
                StrSql = StrSql + "8 SEQ "
                StrSql = StrSql + "FROM  SELECTION WHERE USERID=" + UserId.ToString() + " "
                StrSql = StrSql + ") A "
                StrSql = StrSql + "LEFT OUTER JOIN CATEGORYDETAILS "
                StrSql = StrSql + "ON CATEGORYDETAILS.CATEGORYDETAILID = A.VALUE "
                StrSql = StrSql + "ORDER BY SEQ "


                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetSearchResults:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetLogicAndSearchResults(ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim Ds As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim i As New Integer
            Try
                StrSql = "SELECT  COMPANY,  "
                StrSql = StrSql + "PRODUCT, "
                StrSql = StrSql + "COUNTRY, "
                StrSql = StrSql + "STATE, "
                StrSql = StrSql + "SERVICE, "
                StrSql = StrSql + "DESIGN, "
                StrSql = StrSql + "MACHINE, "
                StrSql = StrSql + "PROCESS, "
                StrSql = StrSql + "CUSTOMER "
                StrSql = StrSql + "FROM SELECTION "
                StrSql = StrSql + "WHERE USERID=" + UserId.ToString() + " "
                Ds = odbUtil.FillDataSet(StrSql, ContractConnection)

                If Ds.Tables(0).Rows.Count > 0 Then
                    i = 0
                    If CInt(Ds.Tables(0).Rows(0).Item("COMPANY")) <> CInt(0) Then
                        i = i + 1
                    End If
                    If CInt(Ds.Tables(0).Rows(0).Item("PRODUCT")) <> CInt(0) Then
                        i = i + 1
                    End If
                    If CInt(Ds.Tables(0).Rows(0).Item("COUNTRY")) <> CInt(0) Then
                        i = i + 1
                    End If
                    If CInt(Ds.Tables(0).Rows(0).Item("STATE")) <> CInt(0) Then
                        If CDbl(Ds.Tables(0).Rows(0).Item("STATE")) <> CDbl(-1) Then
                            i = i + 1
                        End If
                    End If
                    If CInt(Ds.Tables(0).Rows(0).Item("SERVICE")) <> CInt(0) Then
                        i = i + 1
                    End If
                    If CInt(Ds.Tables(0).Rows(0).Item("DESIGN")) <> CInt(0) Then
                        i = i + 1
                    End If
                    If CInt(Ds.Tables(0).Rows(0).Item("MACHINE")) <> CInt(0) Then
                        i = i + 1
                    End If
                    If CInt(Ds.Tables(0).Rows(0).Item("PROCESS")) <> CInt(0) Then
                        i = i + 1
                    End If
                    If CInt(Ds.Tables(0).Rows(0).Item("CUSTOMER")) <> CInt(0) Then
                        i = i + 1
                    End If

                    StrSql = String.Empty
                    StrSql = "SELECT RESULTS.COMPANY AS COMPANYID ,  "
                    StrSql = StrSql + "COMPANIES.NAME, "
                    StrSql = StrSql + "COUNT(DISTINCT '1' || CRITERIA ) "
                    StrSql = StrSql + "FROM RESULTS, "
                    StrSql = StrSql + "COMPANIES "
                    StrSql = StrSql + "WHERE USERID =" + UserId.ToString() + " "
                    StrSql = StrSql + "AND COMPANIES.COMPANYID = RESULTS.COMPANY "
                    StrSql = StrSql + "GROUP BY RESULTS.COMPANY , "
                    StrSql = StrSql + "COMPANIES.NAME HAVING COUNT(DISTINCT '1' ||  CRITERIA) = " + i.ToString() + " "
                    StrSql = StrSql + "ORDER BY COMPANIES.NAME "

                    Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                End If



                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetSearchResults:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompanyDetails(ByVal CompanyId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  COMPANIES.COMPANYID,  "
                StrSql = StrSql + "COMPANIES.FAXNUMBER, "
                StrSql = StrSql + "COMPANIES.LUDATE, "
                StrSql = StrSql + "COMPANIES.NAME, "
                StrSql = StrSql + "COMPANIES.PHONENUMBER, "
                StrSql = StrSql + "STATE.STATEID, "
                StrSql = StrSql + "STATE.NAME AS STATE, "
                StrSql = StrSql + "COMPANIES.CITY, "
                StrSql = StrSql + "DIMCOUNTRIES.COUNTRYDES AS COUNTRY, "
                StrSql = StrSql + "DIMCOUNTRIES.CURRENCYNAME AS CURRENCYNAME, "
                StrSql = StrSql + "(COMPANIES.STREETADDRESS1||'  '||COMPANIES.STREETADDRESS2) AS ADDRESS, "
                StrSql = StrSql + "COMPANIES.WEBADDRESS, "
                StrSql = StrSql + "COMPANIES.ZIPCODE "
                StrSql = StrSql + "FROM COMPANIES "
                StrSql = StrSql + "INNER JOIN ADMINSITE.STATE "
                StrSql = StrSql + "ON STATE.STATEID = COMPANIES.STATEID "
                StrSql = StrSql + "INNER JOIN ADMINSITE.DIMCOUNTRIES "
                StrSql = StrSql + "ON DIMCOUNTRIES.COUNTRYID = COMPANIES.COUNTRYID "
                StrSql = StrSql + "WHERE COMPANIES.COMPANYID = " + CompanyId + " "



                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetCompanyDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetProfileCategories() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CATEGORIES.NAME,  "
                StrSql = StrSql + "CATEGORIES.CODE, "
                StrSql = StrSql + "CATEGORIES.ISCOMMENTS, "
                StrSql = StrSql + "CATEGORIES.ISDETAILS, "
                StrSql = StrSql + "CATEGORIES.CATEGORYID "
                StrSql = StrSql + "FROM CATEGORIES "


                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetProfileCategories:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetProfileCategoryDetComm(ByVal CompanyId As String, ByVal CategoryId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT   NAME, "
                StrSql = StrSql + "CATEGORYID, "
                StrSql = StrSql + "SUBSTR(SYS_CONNECT_BY_PATH(DETAILS, ','),2) DETAILS "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT    CA.NAME, "
                StrSql = StrSql + "CD.DETAILS, "
                StrSql = StrSql + "CA.SEQ AS SQ, "
                StrSql = StrSql + "CA.CATEGORYID, "
                StrSql = StrSql + "CC.COMPANYID, "
                StrSql = StrSql + "COUNT(*) OVER ( PARTITION BY CA.NAME ) CNT, "
                StrSql = StrSql + "ROW_NUMBER () OVER ( PARTITION BY CA.NAME ORDER BY CD.DETAILS) SEQ "
                StrSql = StrSql + "FROM COMPANYCATEGORIES CC "
                StrSql = StrSql + "INNER JOIN CATEGORYDETAILS CD "
                StrSql = StrSql + "ON CC.CATEGORYDETAILID = CD.CATEGORYDETAILID "
                StrSql = StrSql + "INNER JOIN CATEGORIES CA "
                StrSql = StrSql + "ON CA.CATEGORYID = CC.CATEGORYID "
                StrSql = StrSql + "WHERE CC.COMPANYID = " + CompanyId + " "
                StrSql = StrSql + "AND CA.CATEGORYID = " + CategoryId + " "
                StrSql = StrSql + ") "
                StrSql = StrSql + "WHERE    SEQ=CNT "
                StrSql = StrSql + "START WITH    SEQ=1 "
                StrSql = StrSql + "CONNECT BY PRIOR    SEQ+1=SEQ "
                StrSql = StrSql + "AND PRIOR   NAME=NAME "
           
                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetProfileCategoryDetComm:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetProfileCategoryDetails(ByVal CompanyId As String, ByVal CategoryId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DETAILS  "
                StrSql = StrSql + "FROM COMPANYCATEGORIES "
                StrSql = StrSql + "WHERE COMPANYID = " + CompanyId + " "
                StrSql = StrSql + "AND CATEGORYID = " + CategoryId + " "
                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetProfileCategoryDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetProfileCategoryDetails2(ByVal CompanyId As String, ByVal CategoryId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CD.CATEGORYDETAILID ID,  "
                StrSql = StrSql + "CD.DETAILS, "
                StrSql = StrSql + "'' AS DATA, "
                StrSql = StrSql + "NVL(UCC.COMPANYCATEGORYID,0)COMPANYCATEGORYID, "
                StrSql = StrSql + "(CASE WHEN NVL(UCC.CATEGORYDETAILID,0) = CD.CATEGORYDETAILID THEN "
                StrSql = StrSql + "'Y' "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "'N' "
                StrSql = StrSql + "END)ISADDED "
                StrSql = StrSql + "FROM CATEGORYDETAILS CD "
                StrSql = StrSql + "LEFT OUTER JOIN COMPANYCATEGORIES UCC "
                StrSql = StrSql + "ON UCC.CATEGORYDETAILID = CD.CATEGORYDETAILID "
                StrSql = StrSql + "AND UCC.COMPANYID = " + CompanyId + " "
                StrSql = StrSql + "WHERE CD.CATEGORYID =" + CategoryId + "  "
                StrSql = StrSql + "ORDER BY  CD.DETAILS "

                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetProfileCategoryDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetProfileCategoryComments(ByVal CompanyId As String, ByVal CategoryId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT COMMENTS  "
                StrSql = StrSql + "FROM COMPANYCATEGORYCOMM "
                StrSql = StrSql + "WHERE COMPANYID = " + CompanyId + " "
                StrSql = StrSql + "AND CATEGORYID = " + CategoryId + " "
                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetProfileCategoryDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

#Region "User Preferences"
        Public Function GetUserCategories() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CATEGORYID,  "
                StrSql = StrSql + "NAME, "
                StrSql = StrSql + "UPDATEDDATETIME, "
                StrSql = StrSql + "CODE, "
                StrSql = StrSql + "SEQ, "
                StrSql = StrSql + "ISCOMMENTS, "
                StrSql = StrSql + "ISDETAILS "
                StrSql = StrSql + "FROM CATEGORIES "
                StrSql = StrSql + "ORDER BY SEQ "

                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetUserCategories:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUserCategoryDetails(ByVal CategoryId As String, ByVal CompanyId As String, ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT CD.CATEGORYDETAILID,  "
                StrSql = StrSql + "CD.DETAILS, "
                StrSql = StrSql + "NVL(UCC.COMPANYCATEGORYID,0)COMPANYCATEGORYID, "
                StrSql = StrSql + "CD.CATEGORYDETAILID||':'||NVL(UCC.COMPANYCATEGORYID,0) ID, "
                StrSql = StrSql + "(CASE WHEN NVL(UCC.CATEGORYDETAILID,0) = CD.CATEGORYDETAILID THEN "
                StrSql = StrSql + "'Y' "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "'N' "
                StrSql = StrSql + "END)ISADDED "
                StrSql = StrSql + "FROM CATEGORYDETAILS CD "
                StrSql = StrSql + "LEFT OUTER JOIN USERCOMPANYCATEGORIES UCC "
                StrSql = StrSql + "ON UCC.CATEGORYDETAILID = CD.CATEGORYDETAILID "
                StrSql = StrSql + "AND UCC.COMPANYID = " + CompanyId + " "
                StrSql = StrSql + "AND UCC.USERID = " + UserId + " "
                StrSql = StrSql + "WHERE CD.CATEGORYID = " + CategoryId + " "
                StrSql = StrSql + "ORDER BY  CD.DETAILS "

                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetUserCategoryDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUserCategoryDetailsByDetialId(ByVal CategorydetailId As String, ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT C1.COMPANYID,  "
                StrSql = StrSql + "REPLACE(COMPANIES.NAME,'''','##') NAME , "
                StrSql = StrSql + "C1.CATEGORYID "
                StrSql = StrSql + "FROM USERCOMPANYCATEGORIES C1 "
                StrSql = StrSql + "INNER JOIN CATEGORYDETAILS C2 "
                StrSql = StrSql + "ON C1.CATEGORYDETAILID=C2.CATEGORYDETAILID "
                StrSql = StrSql + "INNER JOIN COMPANIES "
                StrSql = StrSql + "ON COMPANIES.COMPANYID=C1.COMPANYID "
                StrSql = StrSql + "INNER JOIN USERCOUNTRIES ON USERCOUNTRIES.COUNTRYID = COMPANIES.COUNTRYID "
                StrSql = StrSql + "WHERE C1.CATEGORYDETAILID=" + CategorydetailId + " "
                StrSql = StrSql + "AND C1.USERID = " + UserId + " "
                StrSql = StrSql + "AND USERCOUNTRIES.USERID = " + UserId + " "


                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetUserCategoryDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUserCategoryDetailsProfile(ByVal CategoryId As String, ByVal CompanyId As String, ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT   NAME,  "
                StrSql = StrSql + "CATEGORYID, "
                StrSql = StrSql + "SUBSTR(SYS_CONNECT_BY_PATH(DETAILS, ','),2) DETAILS "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT    CA.NAME, "
                StrSql = StrSql + "CD.DETAILS, "
                StrSql = StrSql + "CA.SEQ AS SQ, "
                StrSql = StrSql + "CA.CATEGORYID, "
                StrSql = StrSql + "CC.COMPANYID, "
                StrSql = StrSql + "COUNT(*) OVER ( PARTITION BY CA.NAME ) CNT, "
                StrSql = StrSql + "ROW_NUMBER () OVER ( PARTITION BY CA.NAME ORDER BY CD.DETAILS) SEQ "
                StrSql = StrSql + "FROM USERCOMPANYCATEGORIES CC "
                StrSql = StrSql + "INNER JOIN CATEGORYDETAILS CD "
                StrSql = StrSql + "ON CC.CATEGORYDETAILID = CD.CATEGORYDETAILID "
                StrSql = StrSql + "INNER JOIN CATEGORIES CA "
                StrSql = StrSql + "ON CA.CATEGORYID = CC.CATEGORYID "
                StrSql = StrSql + "WHERE CC.COMPANYID =" + CompanyId + ""
                StrSql = StrSql + "AND CA.CATEGORYID = " + CategoryId + ""
                StrSql = StrSql + "AND CC.USERID = " + UserId + ""
                StrSql = StrSql + ") "
                StrSql = StrSql + "WHERE    SEQ=CNT "
                StrSql = StrSql + "START WITH    SEQ=1 "
                StrSql = StrSql + "CONNECT BY PRIOR    SEQ+1=SEQ "
                StrSql = StrSql + "AND PRIOR   NAME=NAME "


                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetUserCategoryDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUserCategoryComments(ByVal CategoryId As String, ByVal CompanyId As String, ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT UCCOM.CATEGORYID,  "
                StrSql = StrSql + "UCCOM.COMPANYCATEGORYCOMMID, "
                StrSql = StrSql + "UCCOM.COMMENTS "
                StrSql = StrSql + "FROM USERCOMPANYCATEGORYCOMM UCCOM "
                StrSql = StrSql + "WHERE UCCOM.COMPANYID = " + CompanyId + " "
                StrSql = StrSql + "AND UCCOM.USERID = " + UserId + " "
                StrSql = StrSql + "AND UCCOM.CATEGORYID = " + CategoryId + " "


                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetUserCategoryComments:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUserCategoryDetl(ByVal CategoryId As String, ByVal CompanyId As String, ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT UCC.COMPANYCATEGORYID,  "
                StrSql = StrSql + "UCC.DETAILS "
                StrSql = StrSql + "FROM USERCOMPANYCATEGORIES UCC "
                StrSql = StrSql + "WHERE UCC.COMPANYID = " + CompanyId + " "
                StrSql = StrSql + "AND UCC.USERID = " + UserId + " "
                StrSql = StrSql + "AND UCC.CATEGORYID = " + CategoryId + " "

                Dts = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContractGetData:GetUserCategoryDetl:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Manage Contract Users Admin Tool"

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
                StrSql = StrSql + "FROM ULOGIN "
                StrSql = StrSql + "INNER JOIN USERS "
                StrSql = StrSql + "ON UPPER(USERS.USERNAME) = UPPER(ULOGIN.UNAME) "
                StrSql = StrSql + "AND UPPER(USERS.PASSWORD) = UPPER(ULOGIN.UPWD) "
                StrSql = StrSql + "INNER JOIN USERPERMISSIONS "
                StrSql = StrSql + "ON USERPERMISSIONS.USERID = USERS.USERID "
                StrSql = StrSql + "INNER JOIN SERVICES "
                StrSql = StrSql + "ON SERVICES.SERVICEID = USERPERMISSIONS.SERVICEID "
                StrSql = StrSql + "WHERE ULOGIN.ID = " + Id.ToString() + " "
                StrSql = StrSql + "AND UPPER(SERVICES.SERVICEDE)='CONTRACT PACKAGING KNOWLEDGEBASE' "


                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("ContrGetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetStructUserInforMation(ByVal UserName As String, ByVal SrcUser As String, ByVal UserId As String) As DataSet
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty

                StrSql = "SELECT USERS.USERID,  "
                StrSql = StrSql + "USERS.USERNAME,UCS.FIRSTNAME,UCS.LASTNAME, "
                StrSql = StrSql + "USERS.PASSWORD, "
                'StrSql = StrSql + "USERS.COMPANY, "
                StrSql = StrSql + "(CASE WHEN COMPANY.COMPANYNAME IS NULL THEN '' ELSE COMPANY.COMPANYNAME END )COMPANY,"
                StrSql = StrSql + "NVL(USERS.LICENSEID,0) LICENSEID, "
                StrSql = StrSql + "LICENSEMASTER.LICENSENAME LICENSENO, "
                StrSql = StrSql + "USERS.ISPROMOMAIL, "
                StrSql = StrSql + "USERS.ISIADMINLICUSR ISADMIN, "
                StrSql = StrSql + "NVL(USERS.INDUSAGE,'N')INDUSAGE, "
                StrSql = StrSql + "NVL(USERS.LICUSAGE,'N')LICUSAGE "
                StrSql = StrSql + "FROM USERS LEFT JOIN LICENSEMASTER ON LICENSEMASTER.LICENSEID=USERS.LICENSEID "
                StrSql = StrSql + "INNER JOIN USERCONTACTS UCS ON USERS.USERID=UCS.USERID "
                StrSql = StrSql + "LEFT OUTER JOIN SHOPPING.COMPANY "
                StrSql = StrSql + "ON COMPANY.COMPANYID=USERS.COMPANYID "
                StrSql = StrSql + "WHERE UPPER(NVL(USERS.USERNAME,'#')) LIKE '%@" + UserName.ToString().ToUpper() + "' "
                StrSql = StrSql + "AND UPPER(NVL(USERS.USERNAME,'#')) LIKE '%" + SrcUser.ToString().ToUpper() + "%' AND USERS.ISVALIDEMAIL='Y' "
                StrSql = StrSql + "ORDER BY UPPER(USERS.USERNAME) "
                Ds = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetTUserInforMation(ByVal UserId As String) As DataSet
            Dim Ds, dsSer As New DataSet()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select T1.LICENSEID,  "
                StrSql = StrSql + "'License'||T1.SEQ LICENSE , "
                StrSql = StrSql + "T2.USERNAME USERNAME1, "
                StrSql = StrSql + "T3.USERNAME USERNAME2 "
                StrSql = StrSql + "from TRANSSERV T1 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.USERS T2 "
                StrSql = StrSql + "ON T2.USERID=T1.USERID1 "
                StrSql = StrSql + "LEFT OUTER JOIN ECON.USERS T3 "
                StrSql = StrSql + "ON T3.USERID=T1.USERID2 "
                StrSql = StrSql + "WHERE T1.LICENSEID IN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT LICENSEID FROM ECON.USERS WHERE USERID=" + UserId + " "
                StrSql = StrSql + ") AND T1.TYPE='KBCOPK' ORDER BY T1.SEQ "

                Ds = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetConAdminServiceInforMation(ByVal UserId As String) As DataSet
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty
                StrSql = "SELECT REFNUM,LICENSEID,ITEMNUMBER,TOTALCOUNT "
                StrSql = StrSql + "FROM LICENSESERVICES "
                StrSql = StrSql + "WHERE LICENSEID=(SELECT LICENSEID FROM USERS WHERE USERID=" + UserId + ") AND ITEMNUMBER='KBCOPK' "
                Ds = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function LicUserCount(ByVal licenseId As String) As DataSet
            Dim Ds As New DataSet()
            Dim StrSql As String = String.Empty
            Try
                'Inserting  License Count
                StrSql = "SELECT count(*) CNT FROM TRANSSERV "
                StrSql = StrSql + "WHERE LICENSEID = " + licenseId + " AND COUNT=1 AND TYPE='KBCOPK' "
                Ds = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Ds

            Catch ex As Exception
                Throw New Exception("Getdata:LicUserCount:" + ex.Message.ToString())
            End Try
        End Function

        Public Function LicUserCountn(ByVal licenseId As String) As DataSet
            Dim Ds As New DataSet()
            Dim StrSql As String = String.Empty
            Try
                'Inserting  License Count
                StrSql = "SELECT count(*) CNT FROM TRANSSERV "
                StrSql = StrSql + "WHERE LICENSEID = " + licenseId + " AND NVL(USERID1,-1)<>-1 AND TYPE='KBCOPK' "
                Ds = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Ds

            Catch ex As Exception
                Throw New Exception("Getdata:LicUserCount:" + ex.Message.ToString())
            End Try
        End Function

        Public Function GetConServiceUserInforMation(ByVal UserName As String, ByVal UserId As String, ByVal serviceDesc As String) As DataSet
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty

                StrSql = "SELECT DISTINCT USRP.USERID,USERROLE  "
                StrSql = "SELECT DISTINCT USRP.USERID,USERROLE  "
                StrSql = StrSql + "FROM USERPERMISSIONS USRP "
                StrSql = StrSql + "LEFT JOIN USERS ON USERS.USERID=USRP.USERID  "
                StrSql = StrSql + "INNER JOIN SERVICES ON SERVICES.SERVICEID=USRP.SERVICEID  "
                StrSql = StrSql + "WHERE  UPPER(SERVICES.SERVICEDE)='" + serviceDesc.ToUpper() + "' "
                StrSql = StrSql + "AND UPPER(USERS.USERNAME) LIKE '%@" + UserName.ToString().ToUpper() + "' "
                Ds = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetServiceUser(ByVal UserId As String, ByVal serviceDesc As String) As DataSet
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty

                StrSql = "SELECT USERID,USERROLE  "
                StrSql = StrSql + "FROM USERPERMISSIONS USRP "
                StrSql = StrSql + "INNER JOIN SERVICES ON SERVICES.SERVICEID=USRP.SERVICEID  "
                StrSql = StrSql + "WHERE  UPPER(SERVICES.SERVICEDE)='" + serviceDesc.ToUpper() + "' "
                StrSql = StrSql + "AND USERID= " + UserId + " "
                Ds = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Ds
            Catch ex As Exception
                Throw New Exception("UtilityGetdata:GetServiceUser:" + ex.Message.ToString())
            End Try
        End Function

        Public Function LicenseFTransferUser(ByVal userid As String) As DataSet
            Dim Ds As New DataSet()
            Dim StrSql As String = String.Empty
            Try
                'Inserting  License Count
                StrSql = "SELECT T1.USERID1,T2.USERNAME FROM TRANSSERV T1 "
                StrSql = StrSql + "INNER JOIN ECON.USERS T2 ON T1.USERID1=T2.USERID "
                StrSql = StrSql + "WHERE T1.LICENSEID = (SELECT LICENSEID FROM ECON.USERS WHERE USERID='" + userid + "' ) AND COUNT=0 AND TYPE='KBCOPK' "

                Ds = odbUtil.FillDataSet(StrSql, ContractConnection)
                Return Ds

            Catch ex As Exception
                Throw New Exception("Getdata:LicenseFTransferUser:" + ex.Message.ToString())
            End Try
        End Function

      

        Public Function GetUserInforMation(ByVal UserId As String) As DataSet
            Dim Ds As New DataSet()
            Try
               Dim StrSql As String = String.Empty
                StrSql = "SELECT USERID,LICENSEID,ISIADMINLICUSR FROM USERS WHERE USERID=" + UserId + " "
                Ds = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Ds
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetServiceUserData(ByVal UserName As String, ByVal serviceDesc As String) As DataSet
            Dim Ds As New DataSet()
            Try
                Dim StrSql As String = String.Empty
                StrSql = "SELECT USERID,USERROLE  "
                StrSql = StrSql + "FROM USERPERMISSIONS USRP "
                StrSql = StrSql + "INNER JOIN SERVICES ON SERVICES.SERVICEID=USRP.SERVICEID  "
                StrSql = StrSql + "WHERE  UPPER(SERVICES.SERVICEDE)='" + serviceDesc.ToUpper() + "' "
                StrSql = StrSql + "AND USERID= (SELECT USERID FROM USERS WHERE UPPER(USERNAME)='" + UserName.ToUpper() + "') "
                Ds = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Ds
            Catch ex As Exception
                Throw New Exception("Getdata:GetServiceUserData:" + ex.Message.ToString())
            End Try
        End Function
#End Region
        

       
    End Class
End Class
