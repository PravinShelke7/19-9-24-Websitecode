Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System

Public Class PackProdGetData
    Public Class Selectdata

        Dim PackProdConnection As String = System.Configuration.ConfigurationManager.AppSettings("PackProdConnectionString")

#Region "Error"
        Public Function GetErrors(ByVal ErrorCode As String) As DataSet
            Dim Dts As New DataSet()
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("PackProdConnectionString")
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
        Public Function GetCompanyData(ByVal des As String, ByVal UserId As String) As DataSet
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
                StrSql = StrSql + ") "
                StrSql = StrSql + "WHERE NVL(UPPER(NAME),'#') LIKE '" + des.ToUpper().Replace("'", "''") + "%' "
                StrSql = StrSql + "ORDER BY NAME "


                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetCompanyData:" + ex.Message.ToString())
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

                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetCountryDetails:" + ex.Message.ToString())
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

                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetStateDetails:" + ex.Message.ToString())
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


                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetCategoryByID:" + ex.Message.ToString())
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

                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetCategoryDetailsByText:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetSelectionDataByUser(ByVal userName As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT COMPANY,PRODUCT,COUNTRY,STATE,SERVICE,DESIGN,PROCESS,MACHINE,CUSTOMER FROM SELECTION WHERE UPPER(USERNAME) ='" + userName.ToUpper() + "' "
                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetSelectionDataByUser:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompanyDetailsByCountry(ByVal CountryId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT COMPANIES.COMPANYID AS ID,REPLACE(COMPANIES.NAME,'''','##') NAME   FROM COMPANIES    "
                StrSql = StrSql + "WHERE COMPANIES.COUNTRYID='" + CountryId + "' ORDER BY COMPANIES.NAME "
                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetCompanyDetailsByState:" + ex.Message.ToString())
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
                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetCompanyDetailsByState:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetCompanyDetailsByCategory(ByVal catId As String, ByVal UserId As String) As DataSet
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
                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetCompanyDetailsByCategory:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetSelectionDetails(ByVal userName As String) As DataSet
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
                StrSql = StrSql + "WHERE UPPER(USERNAME) ='" + userName.ToUpper() + "' "


                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetSelectionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSearchResults(ByVal userName As String, ByVal criteria As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT COMP.COMPANYID,  "
                StrSql = StrSql + "COMP.NAME "
                StrSql = StrSql + "FROM COMPANIES COMP "
                StrSql = StrSql + "INNER JOIN RESULTS RES "
                StrSql = StrSql + "ON COMP.COMPANYID  = RES.COMPANY "
                StrSql = StrSql + "WHERE UPPER(RES.USERNAME) ='" + userName.ToUpper() + "'  "
                StrSql = StrSql + "AND UPPER(RES.CRITERIA)= '" + criteria.ToUpper() + "' "
                StrSql = StrSql + "ORDER BY COMP.NAME "


                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetSearchResults:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetLogicORSearchResults(ByVal userName As String) As DataSet
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
                StrSql = StrSql + "AND UPPER(USERNAME)='" + userName.ToUpper() + "' "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Country' CATEGORY , "
                StrSql = StrSql + "DIMCOUNTRIES.COUNTRYDES  DETAILS, "
                StrSql = StrSql + "'COUNTRY' AS CODE, "
                StrSql = StrSql + "1 SEQ "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "ADMINSITE.DIMCOUNTRIES "
                StrSql = StrSql + "INNER JOIN SELECTION "
                StrSql = StrSql + "ON SELECTION.COUNTRY = DIMCOUNTRIES.COUNTRYID "
                StrSql = StrSql + "AND UPPER(USERNAME)='" + userName.ToUpper() + "' "
                StrSql = StrSql + "UNION "
                'StrSql = StrSql + "SELECT 'State' CATEGORY , "
                'StrSql = StrSql + "STATE.NAME  DETAILS, "
                'StrSql = StrSql + "'STATE' AS CODE, "
                'StrSql = StrSql + "2 SEQ "
                'StrSql = StrSql + "FROM "
                'StrSql = StrSql + "STATE "
                'StrSql = StrSql + "INNER JOIN SELECTION "
                'StrSql = StrSql + "ON SELECTION.STATE = STATE.STATEID "
                'StrSql = StrSql + "AND UPPER(USERNAME)='" + userName.ToUpper() + "' "
                'StrSql = StrSql + "UNION "
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
                StrSql = StrSql + "FROM  SELECTION WHERE UPPER(USERNAME)='" + userName.ToUpper() + "' "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Product Services' DATA, "
                StrSql = StrSql + "SERVICE VALUE, "
                StrSql = StrSql + "'SERVICE' AS CODE, "
                StrSql = StrSql + "4 SEQ "
                StrSql = StrSql + "FROM  SELECTION WHERE UPPER(USERNAME)='" + userName.ToUpper() + "' "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Product Development Capabilities' DATA, "
                StrSql = StrSql + "DESIGN VALUE, "
                StrSql = StrSql + "'DESIGN' AS CODE, "
                StrSql = StrSql + "5 SEQ "
                StrSql = StrSql + "FROM  SELECTION WHERE UPPER(USERNAME)='" + userName.ToUpper() + "' "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Processing Capabilities' DATA, "
                StrSql = StrSql + "PROCESS VALUE, "
                StrSql = StrSql + "'PROCESS' AS CODE, "
                StrSql = StrSql + "6 SEQ "
                StrSql = StrSql + "FROM  SELECTION WHERE UPPER(USERNAME)='" + userName.ToUpper() + "' "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Machinery Systems' DATA, "
                StrSql = StrSql + "MACHINE VALUE, "
                StrSql = StrSql + "'MACHINE' AS CODE, "
                StrSql = StrSql + "7 SEQ "
                StrSql = StrSql + "FROM  SELECTION WHERE UPPER(USERNAME)='" + userName.ToUpper() + "' "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Representative Customers' DATA, "
                StrSql = StrSql + "CUSTOMER VALUE, "
                StrSql = StrSql + "'CUSTOMER' AS CODE, "
                StrSql = StrSql + "8 SEQ "
                StrSql = StrSql + "FROM  SELECTION WHERE UPPER(USERNAME)='" + userName.ToUpper() + "' "
                StrSql = StrSql + ") A "
                StrSql = StrSql + "LEFT OUTER JOIN CATEGORYDETAILS "
                StrSql = StrSql + "ON CATEGORYDETAILS.CATEGORYDETAILID = A.VALUE "
                StrSql = StrSql + "ORDER BY SEQ "


                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetSearchResults:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetLogicAndSearchResults(ByVal userName As String) As DataSet
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
                StrSql = StrSql + "WHERE UPPER(USERNAME)='" + userName.ToUpper() + "' "
                Ds = odbUtil.FillDataSet(StrSql, PackProdConnection)

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
                    StrSql = StrSql + "WHERE UPPER(USERNAME) ='" + userName.ToUpper() + "' "
                    StrSql = StrSql + "AND COMPANIES.COMPANYID = RESULTS.COMPANY "
                    StrSql = StrSql + "GROUP BY RESULTS.COMPANY , "
                    StrSql = StrSql + "COMPANIES.NAME HAVING COUNT(DISTINCT '1' ||  CRITERIA) = " + i.ToString() + " "
                    StrSql = StrSql + "ORDER BY COMPANIES.NAME "

                    Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                End If



                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetSearchResults:" + ex.Message.ToString())
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



                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetCompanyDetails:" + ex.Message.ToString())
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


                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetProfileCategories:" + ex.Message.ToString())
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

                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetProfileCategoryDetComm:" + ex.Message.ToString())
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
                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetProfileCategoryDetails:" + ex.Message.ToString())
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

                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetProfileCategoryDetails:" + ex.Message.ToString())
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
                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetProfileCategoryDetails:" + ex.Message.ToString())
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

                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetUserCategories:" + ex.Message.ToString())
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

                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetUserCategoryDetails:" + ex.Message.ToString())
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


                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetUserCategoryDetails:" + ex.Message.ToString())
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


                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetUserCategoryDetails:" + ex.Message.ToString())
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


                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetUserCategoryComments:" + ex.Message.ToString())
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

                Dts = odbUtil.FillDataSet(StrSql, PackProdConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("PackProdGetData:GetUserCategoryDetl:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region






    End Class
End Class
