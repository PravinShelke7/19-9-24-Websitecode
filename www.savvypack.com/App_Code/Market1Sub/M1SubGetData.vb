Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports System.Data.OracleClient


Public Class M1SubGetData

    Public Class Selectdata
        Dim Market1Connection As String = System.Configuration.ConfigurationManager.AppSettings("Market1ConnectionString")
        Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
        Dim Market1ConnectionForPkg As String = System.Configuration.ConfigurationManager.AppSettings("Market1ConnectionStringForPkg")

#Region "UserDetails"
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
                StrSql = StrSql + "(CASE WHEN USERS.PASSWORD='9krh65sve3' THEN "
                StrSql = StrSql + "'AADMIN' "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "'USER' "
                StrSql = StrSql + "END)USERROLE "
                StrSql = StrSql + "FROM ECON.ULOGIN "
                StrSql = StrSql + "INNER JOIN USERS "
                StrSql = StrSql + "ON UPPER(USERS.USERNAME) = UPPER(ULOGIN.UNAME) "
                StrSql = StrSql + "AND UPPER(USERS.PASSWORD) = UPPER(ULOGIN.UPWD) "
                StrSql = StrSql + "INNER JOIN USERPERMISSIONS "
                StrSql = StrSql + "ON USERPERMISSIONS.USERID = USERS.USERID "
                StrSql = StrSql + "INNER JOIN SERVICES "
                StrSql = StrSql + "ON SERVICES.SERVICEID = USERPERMISSIONS.SERVICEID "
                StrSql = StrSql + "WHERE ULOGIN.ID = " + Id.ToString() + " "
                StrSql = StrSql + "AND SERVICES.SERVICEDE='MARKET1' "
                Dts = odbUtil.FillDataSet(StrSql, EconConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetExtrusionDetails:" + ex.Message.ToString())
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
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("Market1ConnectionString")
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

#Region "Reports"

        Public Function GetMinMaxYear() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MIN(YEAR)MINYR,  "
                StrSql = StrSql + "MAX(YEAR)MAXYR "
                StrSql = StrSql + "FROM VW_POPUALATION "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetMinMaxYear:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetContinentsWise() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT POP.CONTINENTID,  "
                StrSql = StrSql + "POP.CONTINENTDES, "
                StrSql = StrSql + "SUM(POP.FACT) FACTPOP, "
                StrSql = StrSql + "SUM(GDP.FACT) FACTGDP, "
                StrSql = StrSql + "SUM(GDP.FACT)/SUM(POP.FACT) AS GDPPC "
                StrSql = StrSql + "FROM VW_POPUALATION POP "
                StrSql = StrSql + "INNER JOIN VW_GDP GDP "
                StrSql = StrSql + "ON POP.COUNTRYID = GDP.COUNTRYID "
                StrSql = StrSql + "AND POP.YEARID = GDP.YEARID "
                StrSql = StrSql + "GROUP BY POP.CONTINENTID,POP.CONTINENTDES "
                StrSql = StrSql + "ORDER BY POP.CONTINENTDES "


                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetContinentsWise:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCountryWise(ByVal Cid As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT POP.COUNTRYID,  "
                StrSql = StrSql + "POP.COUNTRYDES, "
                StrSql = StrSql + "SUM(POP.FACT) FACTPOP, "
                StrSql = StrSql + "SUM(GDP.FACT) FACTGDP, "
                StrSql = StrSql + "SUM(GDP.FACT)/SUM(POP.FACT) AS GDPPC "
                StrSql = StrSql + "FROM VW_POPUALATION POP "
                StrSql = StrSql + "INNER JOIN VW_GDP GDP "
                StrSql = StrSql + "ON POP.COUNTRYID = GDP.COUNTRYID "
                StrSql = StrSql + "AND POP.YEARID = GDP.YEARID "
                StrSql = StrSql + "WHERE POP.CONTINENTID = " + Cid.ToString() + " "
                StrSql = StrSql + "GROUP BY POP.COUNTRYID,POP.COUNTRYDES "
                StrSql = StrSql + "ORDER BY POP.COUNTRYDES "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetCountryWise:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCountrYearWise(ByVal Cid As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT POP.CONTINENTDES,  "
                StrSql = StrSql + "POP.COUNTRYDES, "
                StrSql = StrSql + "POP.YEAR, "
                StrSql = StrSql + "POP.FACT POPULATION, "
                StrSql = StrSql + "POP.ACT_EST POPULATIONA_E, "
                StrSql = StrSql + "GDP.FACT GDP, "
                StrSql = StrSql + "GDP.ACT_EST GDPA_E, "
                StrSql = StrSql + "GDP.CURRENCYDE, "
                StrSql = StrSql + "CASE WHEN POP.FACT <> 0  THEN "
                StrSql = StrSql + "GDP.FACT/POP.FACT "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END "
                StrSql = StrSql + "AS GDPPC "
                StrSql = StrSql + "FROM VW_POPUALATION POP "
                StrSql = StrSql + "INNER JOIN VW_GDP GDP "
                StrSql = StrSql + "ON POP.COUNTRYID = GDP.COUNTRYID "
                StrSql = StrSql + "AND POP.YEARID = GDP.YEARID "
                StrSql = StrSql + "WHERE POP.COUNTRYID = " + Cid.ToString() + " "
                StrSql = StrSql + "ORDER BY POP.CONTINENTDES,POP.COUNTRYDES "


                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetCountrYearWise:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

#End Region

#Region "Reports"

        Public Function GetSystemReports() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERREPORTID,  "
                StrSql = StrSql + "REPORTNAME, "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "CREATEDDATE "
                StrSql = StrSql + "FROM USERREPORTS "
                StrSql = StrSql + "WHERE ISSYSTEM = 'Y'"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetSystemReports:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUserCustomReports(ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERREPORTID,  "
                StrSql = StrSql + "REPORTNAME, "
                StrSql = StrSql + "USERREPORTID||':'||REPORTNAME REPORTDES, "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "RPTTYPE, "
                StrSql = StrSql + "RPTFACT, "
                StrSql = StrSql + "CREATEDDATE "
                StrSql = StrSql + "FROM USERREPORTS "
                StrSql = StrSql + "WHERE USERID = " + UserId.ToString() + " "
                StrSql = StrSql + " ORDER BY REPORTNAME"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUserCustomReports:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUserCustomReportsByRptId(ByVal RptId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERREPORTID REPORTID,  "
                StrSql = StrSql + "REPORTNAME, "
                StrSql = StrSql + "USERREPORTID||':'||REPORTNAME REPORTDES, "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "RPTTYPE, "
                StrSql = StrSql + "RPTFACT, "
                StrSql = StrSql + "RPTTYPEDES, "
                StrSql = StrSql + "REGIONSETID, "
                StrSql = StrSql + "REGIONID, "
                StrSql = StrSql + "CREATEDDATE "
                StrSql = StrSql + "FROM USERREPORTS "
                StrSql = StrSql + "WHERE USERREPORTID = " + RptId.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUserCustomReports:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUserCustomReportsForSearch(ByVal UserId As String, ByVal ReportName As String, ByVal ServiceId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERREPORTID REPORTID,  "
                StrSql = StrSql + "REPORTNAME, "
                StrSql = StrSql + "USERREPORTID||':'||REPORTNAME REPORTDES, "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "RPTTYPE, "
                StrSql = StrSql + "'Prop' Type, "
                StrSql = StrSql + "RPTFACT, "
                StrSql = StrSql + "RPTTYPEDES, "
                StrSql = StrSql + "CREATEDDATE "
                StrSql = StrSql + "FROM USERREPORTS "
                StrSql = StrSql + "WHERE USERID = " + UserId.ToString() + " "
                StrSql = StrSql + "AND UPPER(REPORTNAME) LIKE '%" + ReportName + "%' "
                StrSql = StrSql + "AND SERVICEID=" + ServiceId + " "
                StrSql = StrSql + " ORDER BY REPORTNAME "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUserCustomReports:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUserReportsFilter(ByVal UserReportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                'StrSql = "SELECT USERREPORTFILTERS.USERREPORTFILTERID,  "
                'StrSql = StrSql + "USERREPORTFILTERS.USERREPORTID, "
                'StrSql = StrSql + "USERREPORTFILTERS.FILTERTYPE, "
                'StrSql = StrSql + "USERREPORTFILTERS.FILTERVALUE, "
                'StrSql = StrSql + "REPORTFILTERS.FILTERNAME "
                'StrSql = StrSql + "FROM USERREPORTFILTERS "
                'StrSql = StrSql + "INNER JOIN REPORTFILTERS "
                'StrSql = StrSql + "ON REPORTFILTERS.FILTERCODE = USERREPORTFILTERS.FILTERTYPE "
                'StrSql = StrSql + "WHERE USERREPORTFILTERS.USERREPORTID  = " + UserReportId.ToString() + " "
                StrSql = "SELECT USERREPORTFILTERS.USERREPORTFILTERID,  "
                StrSql = StrSql + "USERREPORTFILTERS.USERREPORTID, "
                StrSql = StrSql + "USERREPORTFILTERS.FILTERTYPE, "
                StrSql = StrSql + "USERREPORTFILTERS.FILTERVALUE VALUE, "
                StrSql = StrSql + "(CASE WHEN USERREPORTFILTERS.FILTERTYPE = 'CNTRY' THEN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT  DIMCOUNTRIES.COUNTRYDES AS VALUE FROM DIMCOUNTRIES "
                StrSql = StrSql + "WHERE DIMCOUNTRIES.COUNTRYID = USERREPORTFILTERS.FILTERVALUE "
                StrSql = StrSql + ") "
                StrSql = StrSql + "WHEN USERREPORTFILTERS.FILTERTYPE = 'USRREGION' THEN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT  USERREGIONS.REGIONNAME AS VALUE FROM USERREGIONS "
                StrSql = StrSql + "WHERE USERREGIONS.REGIONID = USERREPORTFILTERS.FILTERVALUE "
                StrSql = StrSql + ") "
                StrSql = StrSql + "WHEN USERREPORTFILTERS.FILTERTYPE = 'USRREGIONSET' THEN "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT RPTFACT FROM USERREPORTS WHERE USERREPORTID  =  " + UserReportId.ToString() + " "
                StrSql = StrSql + ") "
                StrSql = StrSql + "END)FILTERVALUE, "
                StrSql = StrSql + "REPORTFILTERS.FILTERNAME "
                StrSql = StrSql + "FROM USERREPORTFILTERS "
                StrSql = StrSql + "INNER JOIN REPORTFILTERS "
                StrSql = StrSql + "ON REPORTFILTERS.FILTERCODE = USERREPORTFILTERS.FILTERTYPE "
                StrSql = StrSql + "WHERE USERREPORTFILTERS.USERREPORTID  =  " + UserReportId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUserReportsFilter:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCountryIds(ByVal FileterId As String, ByVal FilterCode As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                If FilterCode = "CNTRY" Then
                    StrSql = "SELECT DIMCOUNTRIES.COUNTRYID "
                    StrSql = StrSql + "FROM DIMCOUNTRIES "
                    StrSql = StrSql + "WHERE DIMCOUNTRIES.COUNTRYID = " + FileterId + " "
                ElseIf FilterCode = "USRREGION" Then

                    StrSql = "SELECT REGIONID,  "
                    StrSql = StrSql + "SUBSTR(SYS_CONNECT_BY_PATH(COUNTRYID, ','),2) COUNTRYID "
                    StrSql = StrSql + "FROM "
                    StrSql = StrSql + "( "
                    StrSql = StrSql + "SELECT USERREGIONCOUNTRIES.REGIONCOUNTRYID, "
                    StrSql = StrSql + "USERREGIONCOUNTRIES.REGIONID, "
                    StrSql = StrSql + "USERREGIONCOUNTRIES.COUNTRYID, "
                    StrSql = StrSql + "COUNT(*) OVER (PARTITION BY  USERREGIONCOUNTRIES.REGIONID) CNT, "
                    StrSql = StrSql + "ROW_NUMBER () OVER ( PARTITION BY USERREGIONCOUNTRIES.REGIONID ORDER BY USERREGIONCOUNTRIES.COUNTRYID) SEQ "
                    StrSql = StrSql + "FROM USERREGIONCOUNTRIES "
                    StrSql = StrSql + "WHERE USERREGIONCOUNTRIES.REGIONID = " + FileterId + " "
                    StrSql = StrSql + ") "
                    StrSql = StrSql + "WHERE    SEQ=CNT "
                    StrSql = StrSql + "START WITH    SEQ=1 "
                    StrSql = StrSql + "CONNECT BY PRIOR    SEQ+1=SEQ "
                    StrSql = StrSql + "AND PRIOR   REGIONID=REGIONID "

                End If

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUserReportsRows:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUserReportsRows(ByVal UserReportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERREPORTROWID,  "
                StrSql = StrSql + "USERREPORTID, "
                StrSql = StrSql + "ROWSEQUENCE, "
                StrSql = StrSql + "ROWDECRIPTION, "
                StrSql = StrSql + "ROWVALUETYPE, "
                StrSql = StrSql + "CURR, "
                StrSql = StrSql + "NVL(CURRENCY.CURRENCYSYM,'NA') AS TITLE, "
                StrSql = StrSql + "ROWVALUE, "
                StrSql = StrSql + "ROWVAL1, "
                StrSql = StrSql + "ROWVAL2 "
                StrSql = StrSql + "FROM USERREPORTROWS "
                StrSql = StrSql + "LEFT OUTER JOIN CURRENCY "
                StrSql = StrSql + "ON CURRENCY.CURRENCYID = USERREPORTROWS.CURR "
                StrSql = StrSql + "WHERE USERREPORTID  = " + UserReportId.ToString() + " "
                StrSql = StrSql + "ORDER BY ROWSEQUENCE "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUserReportsRows:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUserReportsCols(ByVal UserReportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERREPORTCOLUMNID,  "
                StrSql = StrSql + "USERREPORTID, "
                StrSql = StrSql + "COLUMNSEQUENCE, "
                StrSql = StrSql + "COLUMNDECRIPTION, "
                StrSql = StrSql + "COLUMNVALUETYPE, "
                StrSql = StrSql + "COLUMNVALUE, "
                StrSql = StrSql + "INPUTVALUETYPE1, "
                StrSql = StrSql + "INPUTVALUE1, "
                StrSql = StrSql + "INPUTVALUETYPE2, "
                StrSql = StrSql + "INPUTVALUE2, "
                StrSql = StrSql + "INPUTVALUETYPE3, "
                StrSql = StrSql + "INPUTVALUE3, "
                StrSql = StrSql + "INPUTVALUETYPE4, "
                StrSql = StrSql + "INPUTVALUE4 "
                StrSql = StrSql + "FROM USERREPORTCOLUMNS "
                StrSql = StrSql + "WHERE USERREPORTID  = " + UserReportId.ToString() + " "
                StrSql = StrSql + "ORDER BY COLUMNSEQUENCE "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUserReportsCols:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCAGRReports(ByVal tbl As String, ByVal Cntry As String, ByVal Year As String, ByVal Curr As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                'StrSql = "SELECT FACT,  "
                'StrSql = StrSql + "YEAR, "
                'StrSql = StrSql + "NVL(CURRENCYCONVERSION.CONVERSIONVALUE,1)CURR, "
                'StrSql = StrSql + "CURRENCY.CURRENCYSYM AS TITLE "
                'StrSql = StrSql + "FROM  " + tbl + " FACTTBL "
                'StrSql = StrSql + "LEFT OUTER JOIN CURRENCYCONVERSION "
                'StrSql = StrSql + "ON CURRENCYCONVERSION.YEARID = FACTTBL.YEARID "
                'StrSql = StrSql + "AND CURRENCYCONVERSION.CONVERSIONCURRENCYID =" + Curr + " "
                'StrSql = StrSql + "LEFT OUTER JOIN CURRENCY "
                'StrSql = StrSql + "ON CURRENCY.CURRENCYID = CURRENCYCONVERSION.CONVERSIONCURRENCYID "
                'StrSql = StrSql + "WHERE COUNTRYDES='" + Cntry + "' "
                'StrSql = StrSql + "AND YEAR IN (" + Year + ") "

                StrSql = "SELECT A.FACT,  "
                StrSql = StrSql + "A.YEAR, "
                StrSql = StrSql + "NVL(CURRENCYCONVERSION.CONVERSIONVALUE,1)CURR, "
                StrSql = StrSql + "CURRENCY.CURRENCYSYM AS TITLE "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT SUM(FACT)FACT, "
                StrSql = StrSql + "YEAR, "
                StrSql = StrSql + "YEARID "
                StrSql = StrSql + "FROM  " + tbl + "  FACTTBL "
                StrSql = StrSql + "WHERE FACTTBL.COUNTRYID in (" + Cntry + ") "
                StrSql = StrSql + "AND YEAR IN (" + Year + ") "
                StrSql = StrSql + "GROUP BY YEAR,YEARID "
                StrSql = StrSql + ") A "
                StrSql = StrSql + "LEFT OUTER JOIN CURRENCY "
                StrSql = StrSql + "ON CURRENCY.CURRENCYID = " + Curr + " "
                StrSql = StrSql + "LEFT OUTER JOIN CURRENCYCONVERSION "
                StrSql = StrSql + "ON CURRENCYCONVERSION.YEARID = A.YEARID "
                StrSql = StrSql + "AND CURRENCYCONVERSION.CONVERSIONCURRENCYID  = CURRENCY.COUNTRYID "




                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUserReportsCols:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCAGRRegReports(ByVal tbl As String, ByVal regionId As String, ByVal Year As String, ByVal Curr As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  FACT,  "
                StrSql = StrSql + "YEAR, "
                StrSql = StrSql + "NVL(CURRENCYCONVERSION.CONVERSIONVALUE,1)CURR, "
                StrSql = StrSql + "CURRENCY.CURRENCYSYM AS TITLE "
                StrSql = StrSql + "FROM   " + tbl + " FACTTBL "
                StrSql = StrSql + "LEFT OUTER JOIN CURRENCYCONVERSION "
                StrSql = StrSql + "ON CURRENCYCONVERSION.YEARID = FACTTBL.YEARID "
                StrSql = StrSql + "AND CURRENCYCONVERSION.CONVERSIONCURRENCYID = (SELECT CURRENCY.COUNTRYID FROM CURRENCY WHERE CURRENCY.CURRENCYID = " + Curr + ") "
                StrSql = StrSql + "LEFT OUTER JOIN CURRENCY "
                StrSql = StrSql + "ON CURRENCY.CURRENCYID = CURRENCYCONVERSION.CONVERSIONCURRENCYID "
                StrSql = StrSql + "WHERE REGIONID IN (" + regionId + ") "
                StrSql = StrSql + "AND YEAR IN (" + Year + ") "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUserReportsCols:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetReportId() As Integer
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                StrSql = "SELECT SEQUSERREPORTID.NEXTVAL AS REPORTID  "
                StrSql = StrSql + "FROM DUAL "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                ReportId = Convert.ToInt32(Dts.Tables(0).Rows(0).Item("ReportId").ToString())
                Return ReportId
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetReportId:" + ex.Message.ToString())
                Return ReportId
            End Try
        End Function


        Public Function GetReportColumnId() As Integer
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportColId As New Integer
            Try
                StrSql = "SELECT SEQUSERREPORTCOLUMNID.NEXTVAL AS REPORTFILTERID  "
                StrSql = StrSql + "FROM DUAL "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                ReportColId = Convert.ToInt32(Dts.Tables(0).Rows(0).Item("REPORTFILTERID").ToString())
                Return ReportColId
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetReportId:" + ex.Message.ToString())
                Return ReportColId
            End Try
        End Function

        Public Function GetReportRowId() As Integer
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportRowId As New Integer
            Try
                StrSql = "SELECT SEQUSERREPORTROWID.NEXTVAL AS REPORTROWID  "
                StrSql = StrSql + "FROM DUAL "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                ReportRowId = Convert.ToInt32(Dts.Tables(0).Rows(0).Item("REPORTROWID").ToString())
                Return ReportRowId
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetReportId:" + ex.Message.ToString())
                Return ReportRowId
            End Try
        End Function
        Public Function GetReportFilterId() As Integer
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportColId As New Integer
            Try
                StrSql = "SELECT SEQUSERREPORTFILTERID.NEXTVAL AS REPORTFILTERID  "
                StrSql = StrSql + "FROM DUAL "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                ReportColId = Convert.ToInt32(Dts.Tables(0).Rows(0).Item("REPORTFILTERID").ToString())
                Return ReportColId
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetReportFilterId:" + ex.Message.ToString())
                Return ReportColId
            End Try
        End Function
        Public Function GetYears() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                StrSql = "SELECT YEARID, YEAR FROM DIMYEARS ORDER BY YEAR"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetReportId:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetReportFilterType(ByVal ReportType As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                StrSql = "SELECT REPORTFILTERS.REPORTFILTERID,  "
                StrSql = StrSql + "REPORTFILTERS.FILTERNAME, "
                StrSql = StrSql + "REPORTFILTERS.FILTERCODE, "
                StrSql = StrSql + "REPORTFILTERS.SQL "
                StrSql = StrSql + "FROM REPORTFILTERS "
                StrSql = StrSql + "WHERE RPTTYPE='" + ReportType + "' "
                StrSql = StrSql + "ORDER BY REPORTFILTERS.FILTERNAME "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetReportFilter:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetReportFilter(ByVal UserId As String, ByVal Sql As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                StrSql = Sql.Replace("@USERID", 0)
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetReportFilter:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetRowType2D(ByVal UserId As String, ByVal RegionSetID As String, ByVal Sql As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                StrSql = Sql.Replace("@USERID", UserId).Replace("@REGIONSETID", RegionSetID)
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetReportFilter:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCurrency() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                StrSql = "SELECT CURRENCYID, CURRENCYDE, CURRENCYSYM, COUNTRYID FROM CURRENCY ORDER BY CURRENCYDE"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetCurrency:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetReportFactType() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty

            Try

                StrSql = "SELECT NAME,  "
                StrSql = StrSql + "VALUE "
                StrSql = StrSql + "FROM REPORTFACTS "
                StrSql = StrSql + "ORDER BY NAME "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function GetRowsSelectorold(ByVal RptType As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                'StrSql = "SELECT 'GDP' AS TEXT,  "
                'StrSql = StrSql + "'VW_GDP' AS VAL "
                'StrSql = StrSql + "FROM DUAL "
                'StrSql = StrSql + "UNION "
                'StrSql = StrSql + "SELECT 'Population' AS TEXT, "
                'StrSql = StrSql + "'VW_POPUALATION' AS VAL "
                'StrSql = StrSql + "FROM DUAL "
                'StrSql = StrSql + "UNION "
                'StrSql = StrSql + "SELECT 'GDP/Capita' AS TEXT, "
                'StrSql = StrSql + "'VW_GDPPCAPITA' AS VAL "
                'StrSql = StrSql + "FROM DUAL "
                'StrSql = StrSql + "UNION "
                'StrSql = StrSql + "SELECT 'Product' AS TEXT, "
                'StrSql = StrSql + "'VW_PRODUCTS' AS VAL "
                'StrSql = StrSql + "FROM DUAL "
                'StrSql = StrSql + "UNION "
                'StrSql = StrSql + "SELECT 'Product/Capita' AS TEXT, "
                'StrSql = StrSql + "'VW_PRODUCTSPCAPITA' AS VAL "
                'StrSql = StrSql + "FROM DUAL "
                'StrSql = StrSql + "UNION "
                'StrSql = StrSql + "SELECT 'Package' AS TEXT, "
                'StrSql = StrSql + "'VW_PACKAGES_DATA' AS VAL "
                'StrSql = StrSql + "FROM DUAL "
                'StrSql = StrSql + "UNION "
                'StrSql = StrSql + "SELECT 'Package/Capita' AS TEXT, "
                'StrSql = StrSql + "'VW_PACKAGESPCAPITA_DATA' AS VAL "
                'StrSql = StrSql + "FROM DUAL "
                StrSql = "SELECT ROWTYPEID,  "
                StrSql = StrSql + "ROWDES, "
                StrSql = StrSql + "ROWVALUE, "
                StrSql = StrSql + "ROWCODE, "
                StrSql = StrSql + "ROWSQL "
                StrSql = StrSql + "FROM ROWTYPE "
                StrSql = StrSql + "WHERE ROWCODE='" + RptType + "'  "
                StrSql = StrSql + "ORDER BY ROWDES "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetRowsSelector:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetRowsSelector(ByVal rowTypeId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                StrSql = "SELECT ROWTYPEID,  "
                StrSql = StrSql + "ROWDES, "
                StrSql = StrSql + "ROWVALUE, "
                StrSql = StrSql + "ROWCODE, "
                StrSql = StrSql + "ROWSQL "
                StrSql = StrSql + "FROM ROWTYPE "
                StrSql = StrSql + "WHERE ROWTYPEID=CASE WHEN " + rowTypeId + "=-1 THEN ROWTYPEID ELSE " + rowTypeId + " END "
                StrSql = StrSql + "ORDER BY ROWDES "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetRowsSelector:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetRowsSelectorByCode(ByVal rowCode As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                StrSql = "SELECT ROWTYPEID,  "
                StrSql = StrSql + "ROWDES, "
                StrSql = StrSql + "ROWVALUE, "
                StrSql = StrSql + "ROWCODE, "
                StrSql = StrSql + "ROWSQL "
                StrSql = StrSql + "FROM ROWTYPE "
                StrSql = StrSql + "WHERE ROWCODE='" + rowCode + "' "
                StrSql = StrSql + "ORDER BY ROWDES DESC"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetRowsSelector:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetColsSelector(ByVal colTypeId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                StrSql = "SELECT COLTYPEID,  "
                StrSql = StrSql + "COLDES, "
                StrSql = StrSql + "COLVALUE, "
                StrSql = StrSql + "COLCODE, "
                StrSql = StrSql + "COLSQL "
                StrSql = StrSql + "FROM COLTYPE "
                StrSql = StrSql + "WHERE COLTYPEID=CASE WHEN  " + colTypeId + " =-1 THEN COLTYPEID ELSE " + colTypeId + " END "
                StrSql = StrSql + "ORDER BY COLDES "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetColsSelector:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetFilterSelector(ByVal filterTypeId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                StrSql = "SELECT FILTERTYPEID,   "
                StrSql = StrSql + "FILTERDES,  "
                StrSql = StrSql + "FILTERVALUE,  "
                StrSql = StrSql + "FILTERCODE,  "
                StrSql = StrSql + "FILTERSQL  "
                StrSql = StrSql + "FROM FILTERTYPE  "
                StrSql = StrSql + "WHERE FILTERTYPEID NOT IN "
                StrSql = StrSql + "(SELECT FILTERTYPEID "
                StrSql = StrSql + "FROM FILTERTYPE  "
                StrSql = StrSql + "WHERE FILTERTYPEID=CASE WHEN 9  =-1 THEN FILTERTYPEID ELSE 9  END ) "
                StrSql = StrSql + "AND FILTERTYPEID=CASE WHEN  " + filterTypeId + " =-1 THEN FILTERTYPEID ELSE " + filterTypeId + " END "
                StrSql = StrSql + "ORDER BY FILTERDES  "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetFilterSelector:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetFilterType() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                StrSql = "SELECT * from DIMCOUNTRIES ORDER BY COUNTRYDES"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetFilterType:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetColSQL(ByVal rSql As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                Dts = (GetColsSelector(rSql))
                If Dts.Tables(0).Rows(0).Item("COLSQL").ToString() <> "" Then
                    rSql = Dts.Tables(0).Rows(0).Item("COLSQL").ToString()
                    StrSql = "SELECT *  "
                    StrSql = StrSql + "FROM "
                    StrSql = StrSql + "(" + rSql + ")"
                    StrSql = StrSql + "DUAL"
                End If


                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetRowSQL:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetColSQLWithEdiClause(ByVal rSql As String, ByVal clause As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT *  "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "(SELECT  U.USERREPORTCOLUMNID AS ID,U.USERREPORTID AS CLUASE1,"
                StrSql = StrSql + "U.COLUMNSEQUENCE AS CLUASE2,  U.COLUMNVALUE AS VALUE,COLUMNVALUEID AS VALUEID "
                StrSql = StrSql + "FROM USERREPORTCOLUMNS U WHERE U.COLUMNVALUETYPE='Year' "
                StrSql = StrSql + "ORDER BY U.COLUMNSEQUENCE)"
                StrSql = StrSql + "DUAL"
                StrSql = StrSql + " WHERE 1=1 "
                StrSql = StrSql + clause


                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetColSQLWithClause:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetColSQLWithClause(ByVal rSql As String, ByVal clause As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                Dts = (GetColsSelector(rSql))
                If Dts.Tables(0).Rows(0).Item("COLSQL").ToString() <> "" Then
                    rSql = Dts.Tables(0).Rows(0).Item("COLSQL").ToString()
                    StrSql = "SELECT *  "
                    StrSql = StrSql + "FROM "
                    StrSql = StrSql + "(" + rSql + ")"
                    StrSql = StrSql + "DUAL"
                    StrSql = StrSql + " WHERE 1=1 "
                    StrSql = StrSql + clause
                End If


                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetRowSQL:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetRowSQL(ByVal rSql As String, ByVal SubGroupId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim arrFact() As String
            Dim ProdId As String
            Try
                If rSql = "1" Then

                    arrFact = Regex.Split(SubGroupId, ",")
                    For i = 0 To arrFact.Length - 1
                        Dts = GetSubGroupDetails(arrFact(i))

                        If Dts.Tables(0).Rows(0).Item("CATID").ToString() <> "" Then
                            If i = 0 Then
                                ProdId = Dts.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            Else
                                ProdId = ProdId + Dts.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            End If

                        End If

                    Next
                    ProdId = ProdId.Remove(ProdId.Length - 1)

                    If ProdId <> "" Then


                        StrSql = "SELECT F.TYPE AS VALUE,F.ID as ID,F.ProductType as ProductType  "
                        StrSql = StrSql + "FROM FACT F "
                        StrSql = StrSql + "WHERE F.ID IN (SELECT FACTID FROM PRODUCTCATEGORIES "
                        StrSql = StrSql + "WHERE ID IN(" + ProdId + "))"

                    End If
                Else
                    Dts = (GetRowsSelector(rSql))
                    If Dts.Tables(0).Rows(0).Item("ROWSQL").ToString() <> "" Then
                        rSql = Dts.Tables(0).Rows(0).Item("ROWSQL").ToString()
                        StrSql = "SELECT *  "
                        StrSql = StrSql + "FROM "
                        StrSql = StrSql + "(" + rSql + ")"
                        StrSql = StrSql + "DUAL"
                    End If
                End If




                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetRowSQL:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetRowSQLWithClause(ByVal rSql As String, ByVal clause As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                Dts = (GetRowsSelector(rSql))
                If Dts.Tables(0).Rows(0).Item("ROWSQL").ToString() <> "" Then
                    rSql = Dts.Tables(0).Rows(0).Item("ROWSQL").ToString()
                    StrSql = "SELECT *  "
                    StrSql = StrSql + "FROM "
                    StrSql = StrSql + "(" + rSql + ")"
                    StrSql = StrSql + "DUAL"
                    StrSql = StrSql + " WHERE 1=1 "
                    StrSql = StrSql + clause
                End If


                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetRowSQL:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetFilterSQL(ByVal rSql As String, ByVal SubGroupId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim arrFact() As String
            Dim ProdId As String
            Try
                If rSql = "1" Then
                    arrFact = Regex.Split(SubGroupId, ",")
                    For i = 0 To arrFact.Length - 1
                        Dts = GetSubGroupDetails(arrFact(i))

                        If Dts.Tables(0).Rows(0).Item("CATID").ToString() <> "" Then
                            If i = 0 Then
                                ProdId = Dts.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            Else
                                ProdId = ProdId + Dts.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            End If

                        End If

                    Next
                    ProdId = ProdId.Remove(ProdId.Length - 1)
                    StrSql = "SELECT F.TYPE AS VALUE,F.ID as ID,F.ProductType as ProductType  "
                    StrSql = StrSql + "FROM FACT F "
                    StrSql = StrSql + "WHERE F.ID IN (SELECT FACTID FROM PRODUCTCATEGORIES "
                    StrSql = StrSql + "WHERE ID IN(" + ProdId + "))"
                    StrSql = StrSql + "ORDER BY TYPE"
                Else
                    Dts = (GetFilterSelector(rSql))
                    If Dts.Tables(0).Rows(0).Item("FILTERSQL").ToString() <> "" Then
                        rSql = Dts.Tables(0).Rows(0).Item("FILTERSQL").ToString()
                        StrSql = "SELECT *  "
                        StrSql = StrSql + "FROM "
                        StrSql = StrSql + "(" + rSql + ")"
                        StrSql = StrSql + "DUAL"

                    End If
                End If



                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetFilterSQL:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetFilterSQLWithClause(ByVal rSql As String, ByVal Clause As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                Dts = (GetFilterSelector(rSql))
                If Dts.Tables(0).Rows(0).Item("FILTERSQL").ToString() <> "" Then
                    rSql = Dts.Tables(0).Rows(0).Item("FILTERSQL").ToString()
                    StrSql = "SELECT *  "
                    StrSql = StrSql + "FROM "
                    StrSql = StrSql + "(" + rSql + ")"
                    StrSql = StrSql + "DUAL"
                    StrSql = StrSql + " WHERE 1=1 "
                    StrSql = StrSql + Clause
                End If

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetFilterSQL:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetReportRegionCount_ORG(ByVal RegionId As String) As Integer
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT Count(*) REGIONCOUNT FROM USERREGIONS  "
                StrSql = StrSql + "WHERE REGIONID IN (" + RegionId + ")"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                Return Dts.Tables(0).Rows(0)("REGIONCOUNT").ToString()
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetFilterSQL:" + ex.Message.ToString())
                Return 0
            End Try
        End Function
        Public Function GetReportRegionCount(ByVal RegionSetId As String) As Integer
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT Count(*) REGIONCOUNT FROM USERREGIONS  "
                StrSql = StrSql + "WHERE REGIONSETID=" + RegionSetId
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                Return Dts.Tables(0).Rows(0)("REGIONCOUNT").ToString()
            Catch ex As Exception
                Throw New Exception("M1GetData:GetReportRegionCount:" + ex.Message.ToString())
                Return 0
            End Try
        End Function
        Public Function GetReportPackCount(ByVal GroupId As String) As Integer
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT Count(*) REGIONCOUNT FROM PACKAGETYPE  "
                StrSql = StrSql + "WHERE GROUPID=" + GroupId
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                Return Dts.Tables(0).Rows(0)("REGIONCOUNT").ToString()
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetFilterSQL:" + ex.Message.ToString())
                Return 0
            End Try
        End Function

        Public Function GetReportProdGrpCount(ByVal GroupId As String) As Integer
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT Count(*) REGIONCOUNT FROM PRODUCTGROUP  "
                StrSql = StrSql + "WHERE PARENTID=" + GroupId
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                Return Dts.Tables(0).Rows(0)("REGIONCOUNT").ToString()
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetFilterSQL:" + ex.Message.ToString())
                Return 0
            End Try
        End Function
      
        Public Function GetReportProductCount(ByVal GroupId As String) As Integer
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT Count(*) REGIONCOUNT FROM FACT  "
                StrSql = StrSql + "WHERE GROUPID=" + GroupId
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                Return Dts.Tables(0).Rows(0)("REGIONCOUNT").ToString()
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetFilterSQL:" + ex.Message.ToString())
                Return 0
            End Try
        End Function
        Public Function GetReportCountryCount(ByVal RegionId As String) As Integer
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT Count(*) REGIONCOUNT FROM USERREGIONCOUNTRIES  "
                StrSql = StrSql + "WHERE REGIONID=" + RegionId
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                Return Dts.Tables(0).Rows(0)("REGIONCOUNT").ToString
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetFilterSQL:" + ex.Message.ToString())
                Return 0
            End Try
        End Function
        Public Function GetReportRegionsByRegionSet_ORG(ByVal RegionId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT REGIONID ID,  "
                StrSql = StrSql + "REGIONNAME NAME, "
                StrSql = StrSql + "REGIONSETID "
                StrSql = StrSql + "FROM USERREGIONS "
                StrSql = StrSql + "WHERE REGIONID IN (" + RegionId + ")"
                StrSql = StrSql + " ORDER BY REGIONNAME"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts


            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetReportRegionsByRegionSet:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetReportRegionsByRegionSet(ByVal RegionSetId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT REGIONID ID,  "
                StrSql = StrSql + "REGIONNAME NAME, "
                StrSql = StrSql + "REGIONSETID "
                StrSql = StrSql + "FROM USERREGIONS "
                StrSql = StrSql + "WHERE REGIONSETID=" + RegionSetId
                StrSql = StrSql + " ORDER BY REGIONNAME"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts


            Catch ex As Exception
                Throw New Exception("M1GetData:GetReportRegionsByRegionSet:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetReportPackagesByGroup(ByVal GroupId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PACKAGETYPEID ID,  "
                StrSql = StrSql + "(PACKAGETYPEDE1||''||PACKAGETYPEDE2) NAME, "
                StrSql = StrSql + "GROUPID "
                StrSql = StrSql + "FROM PACKAGETYPE "
                StrSql = StrSql + "WHERE GROUPID=" + GroupId
                StrSql = StrSql + " ORDER BY NAME"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetFilterSQL:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetReportGroupTypes(ByVal GroupId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PRODGROUPID ID,  "
                StrSql = StrSql + "NAME, "
                StrSql = StrSql + "PARENTID "
                StrSql = StrSql + "FROM PRODUCTGROUP "
                StrSql = StrSql + "WHERE PARENTID=" + GroupId
                StrSql = StrSql + " ORDER BY NAME"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetFilterSQL:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetReportProductByPackMat(ByVal type As ArrayList, ByVal Id As ArrayList, ByVal GroupId As String) As DataSet
            Dim Dts As New DataSet()
            Dim ds As New DataSet
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim FactId As String = String.Empty
            Dim ProdId As String = String.Empty
            Dim arrFact() As String
                Try
                    'chages for Subscription Product
                    arrFact = Regex.Split(GroupId, ",")
                    For i = 0 To arrFact.Length - 1
                        Dts = GetSubGroupDetails(arrFact(i))

                        If Dts.Tables(0).Rows(0).Item("CATID").ToString() <> "" Then
                            If i = 0 Then
                                FactId = Dts.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            Else
                                FactId = FactId + Dts.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            End If

                        End If
                    Next
                    FactId = FactId.Remove(FactId.Length - 1)

                    Dts = GetSubFactGroupDetails(FactId)
                    For i = 0 To Dts.Tables(0).Rows.Count - 1
                        If i = 0 Then
                            ProdId = Dts.Tables(0).Rows(i).Item("ID").ToString()
                        Else
                            ProdId = ProdId + "," + Dts.Tables(0).Rows(i).Item("ID").ToString()
                        End If
                    Next
                    ' End Changes

                    Dim strMatId = GetMaterialId(type, Id)
                    Dim strPackId = GetPackId(type, Id)
                StrSql = "SELECT DISTINCT ID, "
                    StrSql = StrSql + "TYPE NAME "
                    StrSql = StrSql + "FROM FACT "

                    If strMatId <> String.Empty And strPackId <> String.Empty Then
                        StrSql = StrSql + "INNER JOIN PACKTYPEMATERIAL P "
                        StrSql = StrSql + "ON FACT.ID = P.PRODID "
                        StrSql = StrSql + "WHERE P.PACKTYPEID IN(" + strPackId + ") AND P.MATERIALID IN(" + strMatId + ")"
                        StrSql = StrSql + "AND P.PRODID IN(" + ProdId + ")"
                    Else
                        If strPackId <> String.Empty Then
                            StrSql = StrSql + "INNER JOIN FACTPACKTYPEPACKSIZE P "
                            StrSql = StrSql + "ON FACT.ID = P.FACTID "
                            StrSql = StrSql + "WHERE P.PACKAGETYPEID IN(" + strPackId + ") "
                            StrSql = StrSql + "AND FACT.ID IN(" + ProdId + ")"
                        ElseIf strMatId <> String.Empty Then
                            StrSql = StrSql + "INNER JOIN PACKTYPEMATERIAL P "
                            StrSql = StrSql + "ON FACT.ID = P.PRODID "
                            StrSql = StrSql + "WHERE P.MATERIALID IN(" + strMatId + ") "
                            StrSql = StrSql + "AND P.PRODID IN(" + ProdId + ")"
                        Else
                            StrSql = StrSql + "WHERE ID IN(" + ProdId + ")"
                        End If
                    End If

                    StrSql = StrSql + " ORDER BY NAME"
                    Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                    Return Dts
                Catch ex As Exception
                    Throw New Exception("M1SubGetData:GetFilterSQL:" + ex.Message.ToString())
                    Return Dts
                End Try
        End Function
        Public Function GetReportPackageByProdMat(ByVal type As ArrayList, ByVal Id As ArrayList, ByVal FactId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                Dim strMatId = GetMaterialId(type, Id)
                Dim strProdId = GetProdId(type, Id, FactId)
				
				If strProdId = "" Then
                    Dim arrFact() As String
                    Dim dsFact As New DataSet
                    Dim catId As String = String.Empty
                    arrFact = Regex.Split(HttpContext.Current.Session("M1SubGroupId").ToString(), ",")
                    For j = 0 To arrFact.Length - 1
                        dsFact = GetSubGroupDetails(arrFact(j))
                        If dsFact.Tables(0).Rows(0).Item("CATID").ToString() <> "" Then
                            If j = 0 Then
                                catId = dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            Else
                                catId = catId + dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            End If

                        End If
                    Next
                    catId = catId.Remove(catId.Length - 1)

                    'getting productID
                    dsFact = GetSubFactGroupDetails(catId)
                    FactId = String.Empty
                    For k = 0 To dsFact.Tables(0).Rows.Count - 1
                        If dsFact.Tables(0).Rows(0).Item("ID").ToString() <> "" Then
                            If k = 0 Then
                                FactId = dsFact.Tables(0).Rows(k).Item("ID").ToString()
                            Else
                                FactId = FactId + "," + dsFact.Tables(0).Rows(k).Item("ID").ToString()
                            End If
                        End If
                    Next
                    strProdId = FactId
                End If
				
                StrSql = "SELECT DISTINCT PACKAGETYPE.PACKAGETYPEID AS ID, "
                StrSql = StrSql + "(PACKAGETYPEDE1 || ' ' || PACKAGETYPEDE2) NAME "
                StrSql = StrSql + "FROM PACKAGETYPE "

                If strMatId <> String.Empty And strProdId <> String.Empty Then
                    StrSql = StrSql + "INNER JOIN PACKTYPEMATERIAL P "
                    StrSql = StrSql + "ON PACKAGETYPE.PACKAGETYPEID = P.PACKTYPEID "
                    StrSql = StrSql + "WHERE P.PRODID IN(" + strProdId + ") AND P.MATERIALID IN(" + strMatId + ")"
                Else
                    If strProdId <> String.Empty Then
                        StrSql = StrSql + "INNER JOIN FACTPACKTYPEPACKSIZE P "
                        StrSql = StrSql + "ON PACKAGETYPE.PACKAGETYPEID = P.PACKAGETYPEID "
                        StrSql = StrSql + "WHERE P.FACTID IN(" + strProdId + ") "
                    ElseIf strMatId <> String.Empty Then
                        StrSql = StrSql + "INNER JOIN PACKTYPEMATERIAL P "
                        StrSql = StrSql + "ON PACKAGETYPE.PACKAGETYPEID = P.PACKTYPEID "
                        StrSql = StrSql + "WHERE P.MATERIALID IN(" + strMatId + ") "
                    End If

                      If strProdId = String.Empty Then
                        StrSql = "SELECT DISTINCT PACKAGETYPE.PACKAGETYPEID AS ID, "
                        StrSql = StrSql + "(PACKAGETYPEDE1 || ' ' || PACKAGETYPEDE2) NAME "
                        StrSql = StrSql + "FROM PACKAGETYPE WHERE PACKAGETYPEID=-1 "
                     End If
 
                End If

                StrSql = StrSql + " ORDER BY NAME"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetFilterSQL:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetReportGroupByProdCntry(ByVal type As ArrayList, ByVal Id As ArrayList, ByVal ParentId As String, ByVal FactId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim arrFactId() As String
            Dim ds As New DataSet()
            Dim StrSql As String = String.Empty
            Try
                Dim strRegCnrtyId = GetRegCntryId(type, Id)
                Dim strCnrtyId = GetCountryId(type, Id)
                Dim strProdId = GetProdId(type, Id, FactId)
				 Dim strPackId = GetPackId(type, Id)
				 
                

                If strProdId <> String.Empty Then
				arrFactId = Regex.Split(strProdId, ",")
                    For i = 0 To arrFactId.Length - 1
                        ds = GetFactType(arrFactId(i))
                        If i > 0 Then
                            StrSql = StrSql + "UNION "
                            StrSql = StrSql + "SELECT  DISTINCT PRODGROUPID AS ID,NAME FROM PRODUCTGROUP "
                            StrSql = StrSql + "INNER JOIN " + ds.Tables(0).Rows(0).Item("TABLENAME").ToString() + " ON "
                            StrSql = StrSql + ds.Tables(0).Rows(0).Item("TABLENAME").ToString() + ".SUBGROUPID = PRODGROUPID "
                            If strCnrtyId <> String.Empty Then
                                StrSql = StrSql + "WHERE PARENTID IN(" + ParentId + ") AND COUNTRYID IN (" + strCnrtyId + ") "
                            Else
                                If strRegCnrtyId <> String.Empty Then
                                    StrSql = StrSql + "WHERE PARENTID IN(" + ParentId + ") " + strRegCnrtyId + " "
                                Else
                                    StrSql = StrSql + "WHERE PARENTID IN(" + ParentId + ") "
                                End If
                            End If
							 If strPackId <> String.Empty Then
                                StrSql = StrSql + " AND " + ds.Tables(0).Rows(0).Item("TABLENAME").ToString() + ".PACKAGETYPEID IN (" + strPackId + ")"
                            End If
                            If strRegCnrtyId <> String.Empty And strCnrtyId <> String.Empty Then
                                StrSql = StrSql + "UNION "
                                StrSql = StrSql + "SELECT  DISTINCT PRODGROUPID AS ID,NAME FROM PRODUCTGROUP "
                                StrSql = StrSql + "INNER JOIN " + ds.Tables(0).Rows(0).Item("TABLENAME").ToString() + " ON "
                                StrSql = StrSql + ds.Tables(0).Rows(0).Item("TABLENAME").ToString() + ".SUBGROUPID = PRODGROUPID "
                                StrSql = StrSql + "WHERE PARENTID IN(" + ParentId + ") " + strRegCnrtyId + " "
                            End If
                        Else
                            StrSql = "SELECT  DISTINCT PRODGROUPID AS ID,NAME FROM PRODUCTGROUP "
                            StrSql = StrSql + "INNER JOIN " + ds.Tables(0).Rows(0).Item("TABLENAME").ToString() + " ON "
                            StrSql = StrSql + ds.Tables(0).Rows(0).Item("TABLENAME").ToString() + ".SUBGROUPID = PRODGROUPID "
                            If strCnrtyId <> String.Empty Then
                                StrSql = StrSql + "WHERE PARENTID IN(" + ParentId + ") AND COUNTRYID IN (" + strCnrtyId + ") "
                            Else
                                If strRegCnrtyId <> String.Empty Then
                                    StrSql = StrSql + "WHERE PARENTID IN(" + ParentId + ") " + strRegCnrtyId + " "
                                Else
                                    StrSql = StrSql + "WHERE PARENTID IN(" + ParentId + ") "
                                End If
                            End If
							 If strPackId <> String.Empty Then
                                StrSql = StrSql + " AND " + ds.Tables(0).Rows(0).Item("TABLENAME").ToString() + ".PACKAGETYPEID IN (" + strPackId + ")"
                            End If
                            If strRegCnrtyId <> String.Empty And strCnrtyId <> String.Empty Then
                                StrSql = StrSql + "UNION "
                                StrSql = StrSql + "SELECT  DISTINCT PRODGROUPID AS ID,NAME FROM PRODUCTGROUP "
                                StrSql = StrSql + "INNER JOIN " + ds.Tables(0).Rows(0).Item("TABLENAME").ToString() + " ON "
                                StrSql = StrSql + ds.Tables(0).Rows(0).Item("TABLENAME").ToString() + ".SUBGROUPID = PRODGROUPID "
                                StrSql = StrSql + "WHERE PARENTID IN(" + ParentId + ") " + strRegCnrtyId + " "
                            End If
                        End If
                    Next
					Else
                    arrFactId = Regex.Split(FactId, ",")
                    For i = 0 To arrFactId.Length - 1
                        ds = GetFactType(arrFactId(i))
                        If i > 0 Then
                            StrSql = StrSql + "UNION "
                            StrSql = StrSql + "SELECT  DISTINCT PRODGROUPID AS ID,NAME FROM PRODUCTGROUP "
                            StrSql = StrSql + "INNER JOIN " + ds.Tables(0).Rows(0).Item("TABLENAME").ToString() + " ON "
                            StrSql = StrSql + ds.Tables(0).Rows(0).Item("TABLENAME").ToString() + ".SUBGROUPID = PRODGROUPID "
                            If strCnrtyId <> String.Empty Then
                                StrSql = StrSql + "WHERE PARENTID IN(" + ParentId + ") AND COUNTRYID IN (" + strCnrtyId + ") "
                            Else
                                If strRegCnrtyId <> String.Empty Then
                                    StrSql = StrSql + "WHERE PARENTID IN(" + ParentId + ") " + strRegCnrtyId + " "
                                Else
                                    StrSql = StrSql + "WHERE PARENTID IN(" + ParentId + ") "
                                End If
                            End If
                            If strPackId <> String.Empty Then
                                StrSql = StrSql + " AND " + ds.Tables(0).Rows(0).Item("TABLENAME").ToString() + ".PACKAGETYPEID IN (" + strPackId + ")"
                            End If
                            If strRegCnrtyId <> String.Empty And strCnrtyId <> String.Empty Then
                                StrSql = StrSql + "UNION "
                                StrSql = StrSql + "SELECT  DISTINCT PRODGROUPID AS ID,NAME FROM PRODUCTGROUP "
                                StrSql = StrSql + "INNER JOIN " + ds.Tables(0).Rows(0).Item("TABLENAME").ToString() + " ON "
                                StrSql = StrSql + ds.Tables(0).Rows(0).Item("TABLENAME").ToString() + ".SUBGROUPID = PRODGROUPID "
                                StrSql = StrSql + "WHERE PARENTID IN(" + ParentId + ") " + strRegCnrtyId + " "
                            End If
                        Else
                            StrSql = "SELECT  DISTINCT PRODGROUPID AS ID,NAME FROM PRODUCTGROUP "
                            StrSql = StrSql + "INNER JOIN " + ds.Tables(0).Rows(0).Item("TABLENAME").ToString() + " ON "
                            StrSql = StrSql + ds.Tables(0).Rows(0).Item("TABLENAME").ToString() + ".SUBGROUPID = PRODGROUPID "
                            If strCnrtyId <> String.Empty Then
                                StrSql = StrSql + "WHERE PARENTID IN(" + ParentId + ") AND COUNTRYID IN (" + strCnrtyId + ") "
                            Else
                                If strRegCnrtyId <> String.Empty Then
                                    StrSql = StrSql + "WHERE PARENTID IN(" + ParentId + ") " + strRegCnrtyId + " "
                                Else
                                    StrSql = StrSql + "WHERE PARENTID IN(" + ParentId + ") "
                                End If
                            End If
                            If strPackId <> String.Empty Then
                                StrSql = StrSql + " AND " + ds.Tables(0).Rows(0).Item("TABLENAME").ToString() + ".PACKAGETYPEID IN (" + strPackId + ")"
                            End If
                            If strRegCnrtyId <> String.Empty And strCnrtyId <> String.Empty Then
                                StrSql = StrSql + "UNION "
                                StrSql = StrSql + "SELECT  DISTINCT PRODGROUPID AS ID,NAME FROM PRODUCTGROUP "
                                StrSql = StrSql + "INNER JOIN " + ds.Tables(0).Rows(0).Item("TABLENAME").ToString() + " ON "
                                StrSql = StrSql + ds.Tables(0).Rows(0).Item("TABLENAME").ToString() + ".SUBGROUPID = PRODGROUPID "
                                StrSql = StrSql + "WHERE PARENTID IN(" + ParentId + ") " + strRegCnrtyId + " "
                            End If
                        End If
                    Next
                End If

                StrSql = StrSql + " ORDER BY NAME"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetFilterSQL:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetFactType(ByVal ID As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT ID,  "
                StrSql = StrSql + "TYPE, "
                StrSql = StrSql + "UNIT, "
                StrSql = StrSql + "TABLENAME, "
                StrSql = StrSql + "SEQUENCES, "
                StrSql = StrSql + "CODE "
                StrSql = StrSql + "FROM FACT "
                StrSql = StrSql + "WHERE  ID = " + ID.ToString() + " "
                StrSql = StrSql + "ORDER BY TYPE "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetReportMaterialGroupByProdPack(ByVal type As ArrayList, ByVal Id As ArrayList) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                Dim strPackId = GetPackId(type, Id)
                Dim strProdId = GetProdId(type, Id, "")
                StrSql = "SELECT DISTINCT MATERIALGROUP.ID AS ID, "
                StrSql = StrSql + "(GROUPNAME) NAME "
                StrSql = StrSql + "FROM MATERIALGROUP "

                If strPackId <> String.Empty And strProdId <> String.Empty Then
                    StrSql = StrSql + "INNER JOIN PACKTYPEMATERIAL P "
                    StrSql = StrSql + "ON MATERIALGROUP.ID = P.GROUPID "
                    StrSql = StrSql + "WHERE P.PRODID IN(" + strProdId + ") AND P.PACKTYPEID IN(" + strPackId + ")"
                Else
                    If strProdId <> String.Empty Then
                        StrSql = StrSql + "INNER JOIN  PACKTYPEMATERIAL P "
                        StrSql = StrSql + "ON P.GROUPID = MATERIALGROUP.ID "
                        StrSql = StrSql + "WHERE P.PRODID IN(" + strProdId + ") "
                    ElseIf strPackId <> String.Empty Then
                        StrSql = StrSql + "INNER JOIN PACKTYPEMATERIAL P "
                        StrSql = StrSql + "ON P.GROUPID = MATERIALGROUP.ID "
                        StrSql = StrSql + "WHERE P.PACKTYPEID IN(" + strPackId + ") "
                    End If
                End If

                StrSql = StrSql + " ORDER BY NAME"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1GetData:GetFilterSQL:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetReportDummy(ByVal ReprtType As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                If ReprtType = "PROD" Then
                    StrSql = "SELECT '0' AS ID,  "
                    StrSql = StrSql + "'PRODUCT' AS NAME "
                    StrSql = StrSql + "FROM DUAL "
                ElseIf ReprtType = "PACK" Then
                    StrSql = "SELECT '0' AS ID,  "
                    StrSql = StrSql + "'PACKAGE' AS NAME "
                    StrSql = StrSql + "FROM DUAL "
                ElseIf ReprtType = "MAT" Then
                    StrSql = "SELECT '0' AS ID,  "
                    StrSql = StrSql + "'MATERIAL' AS NAME "
                    StrSql = StrSql + "FROM DUAL "
                ElseIf ReprtType = "MATGRP" Then
                    StrSql = "SELECT '0' AS ID,  "
                    StrSql = StrSql + "'MATERIAL GROUP' AS NAME "
                    StrSql = StrSql + "FROM DUAL "
                ElseIf ReprtType = "GROUP" Then
                    StrSql = "SELECT '0' AS ID,  "
                    StrSql = StrSql + "'GROUP' AS NAME "
                    StrSql = StrSql + "FROM DUAL "
                End If
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1GetData:GetFilterSQL:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetReportFiltersByReportId(ByVal reportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT * "
                StrSql = StrSql + "FROM USERREPORTFILTERSTEMP WHERE "
                StrSql = StrSql + "USERREPORTID = " + reportId
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetFilterSQL:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetReportCountriesByRegion(ByVal RegionSetId As String, ByVal RegionId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  UC.REGIONCOUNTRYID ,  "
                StrSql = StrSql + "DC.COUNTRYID ID, "
                StrSql = StrSql + "DC.COUNTRYDES NAME "
                StrSql = StrSql + "FROM USERREGIONCOUNTRIES UC "
                StrSql = StrSql + "INNER JOIN USERREGIONS UR "
                StrSql = StrSql + "ON UR.REGIONID=UC.REGIONID "
                StrSql = StrSql + "INNER JOIN DIMCOUNTRIES DC "
                StrSql = StrSql + "ON UC.COUNTRYID=DC.COUNTRYID "
                StrSql = StrSql + "WHERE  UC.REGIONID=CASE WHEN " + RegionId + "=-1 THEN UC.REGIONID ELSE " + RegionId + " END "
                StrSql = StrSql + "AND UR.REGIONSETID= " + RegionSetId
                StrSql = StrSql + "ORDER BY COUNTRYDES "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts


            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetFilterSQL:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUsersReportRows(ByVal reportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                '''''''''PREVIOUS ONE
                'StrSql = "SELECT  "
                'StrSql = StrSql + "USERREPORTROWID, "
                'StrSql = StrSql + "USERREPORTID, "
                'StrSql = StrSql + "ROWSEQUENCE, "
                'StrSql = StrSql + "ROWDECRIPTION, "
                'StrSql = StrSql + "ROWVALUETYPE, "
                'StrSql = StrSql + "UNITID, "
                'StrSql = StrSql + "ROWVALUE "
                'StrSql = StrSql + "FROM USERREPORTROWS "
                'StrSql = StrSql + "WHERE "
                'StrSql = StrSql + "USERREPORTID=CASE WHEN " + reportId + "=-1 THEN USERREPORTID  ELSE " + reportId + " END "


                '''''DATE  03-JAN-2012
                StrSql = "SELECT  "
                StrSql = StrSql + "USERREPORTROWID, "
                StrSql = StrSql + "USERREPORTID, "
                StrSql = StrSql + "ROWSEQUENCE, "
                StrSql = StrSql + "ROWDECRIPTION, "
                StrSql = StrSql + "ROWVALUETYPE, "
                StrSql = StrSql + "UNITS.UNITID, "
                StrSql = StrSql + "UNITS.UNITSHRT, "
                StrSql = StrSql + "ROWVALUE "
                StrSql = StrSql + "FROM USERREPORTROWS UR "
                StrSql = StrSql + "LEFT JOIN UNITS ON UR.UNITID=UNITS.UNITID "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "USERREPORTID=CASE WHEN " + reportId + "=-1 THEN USERREPORTID  ELSE " + reportId + " END "
                StrSql = StrSql + " ORDER BY USERREPORTROWID"

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUsersReportRows:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUsersReportFilters(ByVal reportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT  "
                StrSql = StrSql + "USERREPORTFILTERID, "
                StrSql = StrSql + "USERREPORTID, "
                StrSql = StrSql + "FILTERSEQUENCE, "
				  StrSql = StrSql + "FILTERTYPE, "
                StrSql = StrSql + "FILTERVALUE,FILTERVALUEID "
                StrSql = StrSql + "FROM USERREPORTFILTERS "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "USERREPORTID=CASE WHEN " + reportId + "=-1 THEN USERREPORTID  ELSE " + reportId + " END "
                StrSql = StrSql + "ORDER BY FILTERSEQUENCE "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUsersReportFilters:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUsersReportCAGRColumns(ByVal reportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT  "
                StrSql = StrSql + "USERREPORTCOLUMNID, "
                StrSql = StrSql + "USERREPORTID, "
                StrSql = StrSql + "COLUMNVALUETYPE, "
                StrSql = StrSql + "COLUMNTYPEID, "
                StrSql = StrSql + "COLUMNVALUEID, "
                StrSql = StrSql + "COLUMNSEQUENCE, "
                StrSql = StrSql + "COLUMNVALUE, "
                StrSql = StrSql + "INPUTVALUE1, "
                StrSql = StrSql + "INPUTVALUE2,"
                StrSql = StrSql + "INPUTVALUETYPE1,"
                StrSql = StrSql + "INPUTVALUETYPE2 "
                StrSql = StrSql + " FROM USERREPORTCOLUMNS "
                StrSql = StrSql + " WHERE "
                StrSql = StrSql + " USERREPORTID=CASE WHEN " + reportId + "=-1 THEN USERREPORTID  ELSE " + reportId + " END "
                StrSql = StrSql + " AND COLUMNTYPEID=2"

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUsersReportColumns:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUsersReportCapitaRows(ByVal reportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT  "
                StrSql = StrSql + "USERREPORTROWID, "
                StrSql = StrSql + "USERREPORTID, "
                StrSql = StrSql + "ROWVALUETYPE, "
                StrSql = StrSql + "ROWTYPEID, "
                StrSql = StrSql + "ROWVALUEID, "
                StrSql = StrSql + "ROWSEQUENCE, "
                StrSql = StrSql + "ROWVALUE, "
                StrSql = StrSql + "ROWVAL1, "
                StrSql = StrSql + "ROWVAL2,"
                StrSql = StrSql + "ROWVALID1,"
                StrSql = StrSql + "ROWVALID2 "
                StrSql = StrSql + " FROM USERREPORTROWS "
                StrSql = StrSql + " WHERE "
                StrSql = StrSql + " USERREPORTID=CASE WHEN " + reportId + "=-1 THEN USERREPORTID  ELSE " + reportId + " END "
                StrSql = StrSql + " AND ROWTYPEID=9"

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUsersReportColumns:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUsersReportColumns(ByVal reportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT  "
                StrSql = StrSql + "USERREPORTCOLUMNID, "
                StrSql = StrSql + "USERREPORTID, "
                StrSql = StrSql + "COLUMNVALUETYPE, "
                StrSql = StrSql + "COLUMNVALUEID, "
                StrSql = StrSql + "COLUMNSEQUENCE, "
                StrSql = StrSql + "COLUMNVALUE, "
                StrSql = StrSql + "INPUTVALUE1, "
                StrSql = StrSql + "INPUTVALUE2 "
                StrSql = StrSql + "FROM USERREPORTCOLUMNS "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "USERREPORTID=CASE WHEN " + reportId + "=-1 THEN USERREPORTID  ELSE " + reportId + " END "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUsersReportColumns:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUsersDynamicReportData(ByVal ReportId As Integer, ByVal reportDes As String, ByVal GroupId As String) As DataSet
            Dim ds As New DataSet
            Dim odbUtil As New DBUtil()
            Dim cmd As New OracleCommand()
            Dim con As New OracleConnection()
            Dim da As New OracleDataAdapter()
            Try
                con.ConnectionString = Market1ConnectionForPkg
                cmd.Connection = con
                If reportDes = "CNTRY" Then
                    cmd.CommandText = "PKG_GETMIXEDREPORTS58_BB.Proc_GetReportDataByCountry"
                ElseIf reportDes = "REGION" Then
                    cmd.CommandText = "PKG_GETMIXEDREPORTS58_BB.Proc_GetReportDataByRegion"
                End If
				'If reportDes = "CNTRY" Then
                    'cmd.CommandText = "PKG_GETMIXEDREPORTSB11.Proc_GetReportDataByCountry"
                'ElseIf reportDes = "REGION" Then
                    'cmd.CommandText = "PKG_GETMIXEDREPORTSB11.Proc_GetReportDataByRegion"
                'End If
				

                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New OracleParameter("io_cursor", OracleType.Cursor)).Direction = ParameterDirection.Output
                cmd.Parameters.Add("In_Report_ID", OracleType.Number, 4).Value = ReportId
				 'cmd.Parameters.Add("In_Group_Id", OracleType.NVarChar).Value = GroupId
                da = New OracleDataAdapter(cmd)
                'da.Fill(ds, 0)
                da.Fill(ds)
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetReportData:" + ex.Message.ToString())
                Return ds
            End Try
            Return ds
        End Function
        Public Function GetUsersDynamicUniformReportData(ByVal ReportId As Integer, ByVal reportDes As String, ByVal CountryFilter As Boolean, ByVal groupId As String) As DataSet
            Dim ds As New DataSet
            Dim odbUtil As New DBUtil()
            Dim cmd As New OracleCommand()
            Dim con As New OracleConnection()
            Dim da As New OracleDataAdapter()
            Try
                con.ConnectionString = Market1ConnectionForPkg
                cmd.Connection = con
                'cmd.CommandText = "PKG_GETREPORTS.Proc_GetReportData"

                'If reportDes = "CNTRY" Then
                '    cmd.CommandText = "PKG_GETUNIFORMREPORTSTEST1.Proc_GetReportDataByCountry"
                'ElseIf reportDes = "REGION" Then
                '    cmd.CommandText = "PKG_GETUNIFORMREPORTSTEST1.Proc_GetReportDataByRegion"
                'End If
                If CountryFilter = True Then
                    cmd.CommandText = "PKG_GETUNIFORMREPORTS62.Proc_GetReportDataByCountry"
                Else

                    If reportDes = "CNTRY" Then
                        cmd.CommandText = "PKG_GETUNIFORMREPORTS62.Proc_GetReportDataByCountry"
                    Else
                        cmd.CommandText = "PKG_GETUNIFORMREPORTS62.Proc_GetReportDataByRegion"
                    End If
                End If

                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New OracleParameter("io_cursor", OracleType.Cursor)).Direction = ParameterDirection.Output
                cmd.Parameters.Add("In_Report_ID", OracleType.Number, 4).Value = ReportId
                cmd.Parameters.Add("In_Group_Id", OracleType.NVarChar).Value = groupId
                da = New OracleDataAdapter(cmd)
                da.Fill(ds, 0)
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetReportData:" + ex.Message.ToString())
                Return ds
            End Try
            Return ds
        End Function
        Public Function GetUsersDynamicReportRows(ByVal reportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT U.USERREPORTROWID,  "
                StrSql = StrSql + "U.USERREPORTID, "
                StrSql = StrSql + "U.ROWSEQUENCE, "
                StrSql = StrSql + "U.ROWDECRIPTION, "
                StrSql = StrSql + "U.ROWVALUETYPE, "
                StrSql = StrSql + "U.ROWVALUEID, U.ROWVALID1, U.ROWVALID2, "
                StrSql = StrSql + "U.ROWVALUE,U.ROWVAL1,U.ROWVAL2, "
                StrSql = StrSql + "UNITS.UNITSHRT,U1.UNITSHRT UNITSHRT1,U2.UNITSHRT UNITSHRT2, "
                StrSql = StrSql + "UNITS.METRICUNIT,U1.METRICUNIT METRICUNIT1,U2.METRICUNIT METRICUNIT2, "
                StrSql = StrSql + "U.CURR, "
                StrSql = StrSql + "U.UNITID,U.UNITID1,U.UNITID2, "
                StrSql = StrSql + "R.ROWCODE, "
                StrSql = StrSql + "R.ROWVALUE AS ROWCOLUMNID "
                StrSql = StrSql + "FROM USERREPORTROWS U "
                StrSql = StrSql + "INNER JOIN ROWTYPE R "
                StrSql = StrSql + "ON R.ROWTYPEID = U.ROWTYPEID "
                StrSql = StrSql + "LEFT JOIN UNITS ON U.UNITID=UNITS.UNITID "
                StrSql = StrSql + "LEFT JOIN UNITS U1 ON U.UNITID1=U1.UNITID "
                StrSql = StrSql + "LEFT JOIN UNITS U2 ON U.UNITID2=U2.UNITID "
                StrSql = StrSql + "WHERE U.USERREPORTID = " + reportId + ""
                StrSql = StrSql + "ORDER BY U.ROWSEQUENCE "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUsersReportRows:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUsersDynamicReportCols(ByVal reportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT U.USERREPORTID,  "
                StrSql = StrSql + "U.USERREPORTCOLUMNID, "
                StrSql = StrSql + "U.COLUMNSEQUENCE, "
                StrSql = StrSql + "U.COLUMNDECRIPTION, "
                StrSql = StrSql + "U.COLUMNVALUETYPE, "
                StrSql = StrSql + "U.COLUMNVALUEID, "
                StrSql = StrSql + "U.COLUMNVALUE, "
                StrSql = StrSql + "U.INPUTVALUE1, "
                StrSql = StrSql + "U.INPUTVALUE2, "
                StrSql = StrSql + "U.INPUTVALUETYPE1, "
                StrSql = StrSql + "U.INPUTVALUETYPE2, "
                StrSql = StrSql + "C.COLVALUE AS COLCOLUMNID "
                StrSql = StrSql + "FROM USERREPORTCOLUMNS U "
                StrSql = StrSql + "INNER JOIN COLTYPE C "
                StrSql = StrSql + "ON C.COLTYPEID = U.COLUMNTYPEID "
                StrSql = StrSql + "WHERE U.USERREPORTID = " + reportId + ""
                StrSql = StrSql + "ORDER BY U.COLUMNSEQUENCE ASC "




                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUsersReportRows:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        'Public Function GetYearsColumnOnly(ByVal reportId As String, ByVal seqId As String) As DataSet
        '    Dim Dts As New DataSet()
        '    Dim odbUtil As New DBUtil()
        '    Dim StrSql As String = String.Empty
        '    Try

        '        StrSql = "SELECT   "
        '        StrSql = StrSql + "U.USERREPORTCOLUMNID, "
        '        StrSql = StrSql + "U.COLUMNSEQUENCE, "
        '        ' StrSql = StrSql + "U.COLUMNDECRIPTION, "
        '        StrSql = StrSql + "U.COLUMNVALUETYPE, "
        '        StrSql = StrSql + "U.COLUMNVALUEID, "
        '        StrSql = StrSql + "U.COLUMNVALUE  "
        '        'StrSql = StrSql + "U.INPUTVALUE1, "
        '        ' StrSql = StrSql + "U.INPUTVALUE2, "
        '        'StrSql = StrSql + "U.INPUTVALUETYPE1, "
        '        'StrSql = StrSql + "U.INPUTVALUETYPE2, "
        '        'StrSql = StrSql + "C.COLVALUE AS COLCOLUMNID "
        '        StrSql = StrSql + "FROM USERREPORTCOLUMNS U "
        '        StrSql = StrSql + "WHERE U.USERREPORTID = " + reportId + ""
        '        StrSql = StrSql + " AND  U.COLUMNVALUETYPE='Year'"
        '        StrSql = StrSql + " AND  U.COLUMNSEQUENCE<" + seqId + " "
        '        StrSql = StrSql + "ORDER BY U.COLUMNSEQUENCE "




        '        Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
        '        Return Dts
        '    Catch ex As Exception
        '        Throw New Exception("M1SubGetData:GetUsersReportRows:" + ex.Message.ToString())
        '        Return Dts
        '    End Try
        'End Function
#End Region
        Public Function GetUserRegionSets(ByVal grpId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                'StrSql = "SELECT REGIONSETID,REGIONSETNAME FROM USERREGIONSETS "
                'StrSql = StrSql + " WHERE USERID=0 "
                'StrSql = StrSql + "  ORDER BY REGIONSETNAME "
                StrSql = "SELECT USERREGIONSETS.REGIONSETID,USERREGIONSETS.REGIONSETNAME "
                StrSql = StrSql + "FROM USERREGIONSETS "
                StrSql = StrSql + "INNER JOIN GROUPREGIONSET "
                StrSql = StrSql + "ON GROUPREGIONSET.REGIONSETID=USERREGIONSETS.REGIONSETID "
                StrSql = StrSql + "WHERE GROUPREGIONSET.GROUPID IN (" + grpId + ")"
                StrSql = StrSql + "  ORDER BY UPPER(REGIONSETNAME) "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUserRegionSets:" + ex.Message.ToString())
                Return Dts

            End Try
        End Function
        Public Function GetUserRegions(ByVal RegionSetId As String, ByVal ServiceId As String, ByVal RegionId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
		 StrSql = StrSql + "SELECT 0 AS REGIONID,'All' AS REGIONNAME FROM USERREGIONS UNION "
                StrSql = StrSql + "SELECT USERREGIONS.REGIONID,  "
                'StrSql = "SELECT USERREGIONS.REGIONID,  "
                StrSql = StrSql + "REGIONNAME "
                StrSql = StrSql + "FROM USERREGIONS "
                'StrSql = StrSql + "INNER JOIN USERMARKET1SERVICE UMS "
                'StrSql = StrSql + "ON USERREGIONS.REGIONID IN (" + RegionId + ") "
                StrSql = StrSql + "WHERE REGIONSETID=" + RegionSetId
                'StrSql = StrSql + " AND USERID=0 "
                'StrSql = StrSql + " AND UMS.SERVICEID=" + ServiceId + ""
                StrSql = StrSql + " ORDER BY REGIONNAME"


                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUserRegions:" + ex.Message.ToString())
                Return Dts

            End Try
        End Function
#Region "Product-Package"

        Public Function GetProducts() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                StrSql = "SELECT PRODUCTID,  "
                StrSql = StrSql + "CATEGORYID, "
                StrSql = StrSql + "PRODUCTNAME, "
                StrSql = StrSql + "AVGPRODWT "
                StrSql = StrSql + "FROM PRODUCTS "
                StrSql = StrSql + "ORDER BY PRODUCTNAME "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPackage() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                StrSql = "SELECT PACKAGEID,  "
                StrSql = StrSql + "PACKAGEDE "
                StrSql = StrSql + "FROM VW_PACKAGES "

                StrSql = StrSql + "ORDER BY PACKAGEDE "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetProductsById(ByVal Id As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PRODUCTID,  "
                StrSql = StrSql + "CATEGORYID, "
                StrSql = StrSql + "PRODUCTNAME, "
                StrSql = StrSql + "AVGPRODWT "
                StrSql = StrSql + "FROM PRODUCTS "
                StrSql = StrSql + "WHERE PRODUCTID=" + Id + "  "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPackageById(ByVal Id As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                StrSql = "SELECT PACKAGEID,  "
                StrSql = StrSql + "PACKAGEDE "
                StrSql = StrSql + "FROM VW_PACKAGES "
                StrSql = StrSql + "ORDER BY PACKAGEDE "
                StrSql = StrSql + "WHERE PACKAGEID=" + Id + "  "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCAGRProductReports(ByVal tbl As String, ByVal Cntry As String, ByVal Year As String, ByVal Curr As String, ByVal ProdId As String, ByVal PkgId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT FACT,  "
                StrSql = StrSql + "YEAR, "
                StrSql = StrSql + "NVL(CURRENCYCONVERSION.CONVERSIONVALUE,1)CURR, "
                StrSql = StrSql + "CURRENCY.CURRENCYSYM AS TITLE "
                StrSql = StrSql + "FROM  " + tbl + " FACTTBL "
                StrSql = StrSql + "LEFT OUTER JOIN CURRENCYCONVERSION "
                StrSql = StrSql + "ON CURRENCYCONVERSION.YEARID = FACTTBL.YEARID "
                StrSql = StrSql + "AND CURRENCYCONVERSION.CONVERSIONCURRENCYID =" + Curr + " "
                StrSql = StrSql + "LEFT OUTER JOIN CURRENCY "
                StrSql = StrSql + "ON CURRENCY.CURRENCYID = CURRENCYCONVERSION.CONVERSIONCURRENCYID "
                StrSql = StrSql + "WHERE COUNTRYID IN = (" + Cntry + ") "
                StrSql = StrSql + "AND YEAR IN (" + Year + ") "
                StrSql = StrSql + "AND PRODUCTID  IN (" + ProdId + ") "
                StrSql = StrSql + "AND PACKAGEID IN (" + PkgId + ") "


                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUserReportsCols:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function


        Public Function GetCAGRegions(ByVal tbl As String, ByVal Cntry As String, ByVal Year As String, ByVal Curr As String, ByVal ProdId As String, ByVal PkgId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT FACT,  "
                StrSql = StrSql + "YEAR, "
                StrSql = StrSql + "NVL(CURRENCYCONVERSION.CONVERSIONVALUE,1)CURR, "
                StrSql = StrSql + "CURRENCY.CURRENCYSYM AS TITLE "
                StrSql = StrSql + "FROM  " + tbl + " FACTTBL "
                StrSql = StrSql + "LEFT OUTER JOIN CURRENCYCONVERSION "
                StrSql = StrSql + "ON CURRENCYCONVERSION.YEARID = FACTTBL.YEARID "
                StrSql = StrSql + "AND CURRENCYCONVERSION.CONVERSIONCURRENCYID =" + Curr + " "
                StrSql = StrSql + "LEFT OUTER JOIN CURRENCY "
                StrSql = StrSql + "ON CURRENCY.CURRENCYID = CURRENCYCONVERSION.CONVERSIONCURRENCYID "
                StrSql = StrSql + "WHERE COUNTRYID IN = (" + Cntry + ") "
                StrSql = StrSql + "AND YEAR IN (" + Year + ") "
                StrSql = StrSql + "AND PRODUCTID  IN (" + ProdId + ") "
                StrSql = StrSql + "AND PACKAGEID IN (" + PkgId + ") "


                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUserReportsCols:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUnits(ByVal unitId As String, ByVal ProductId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                'StrSql = "SELECT DISTINCT UNITID,  "
                'StrSql = StrSql + "UNITDES, "
                'StrSql = StrSql + "UNITSHRT "
                'StrSql = StrSql + "FROM UNITS "
                'StrSql = StrSql + " WHERE UNITID=CASE WHEN " + unitId + "=-1 THEN UNITID ELSE " + unitId + " END "
                'StrSql = StrSql + " ORDER BY UNITDES "

                StrSql = "SELECT DISTINCT UNITS.UNITID,  "
                StrSql = StrSql + "UNITDES, "
                StrSql = StrSql + "UNITSHRT "
                StrSql = StrSql + "FROM UNITS "
                StrSql = StrSql + "INNER JOIN GROUPUNITS  "
                StrSql = StrSql + "ON GROUPUNITS.UNITID=UNITS.UNITID "
                StrSql = StrSql + "WHERE GROUPUNITS.GROUPID IN (" + ProductId + ") "
                StrSql = StrSql + " AND UNITS.UNITID=CASE WHEN " + unitId + "=-1 THEN UNITS.UNITID ELSE " + unitId + " END "
                StrSql = StrSql + " ORDER BY UNITDES "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUnits:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Country Region"
        Public Function GetRegionId() As Integer
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                StrSql = "SELECT SEQUSERREPORTID.NEXTVAL AS REPORTID  "
                StrSql = StrSql + "FROM DUAL "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                ReportId = Convert.ToInt32(Dts.Tables(0).Rows(0).Item("ReportId").ToString())
                Return ReportId
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetReportId:" + ex.Message.ToString())
                Return ReportId
            End Try
        End Function

        Public Function GetRegionDetails(ByVal UserId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                StrSql = "SELECT REGIONID,  "
                StrSql = StrSql + "REGRIONNAME, "
                StrSql = StrSql + "SUBSTR(SYS_CONNECT_BY_PATH(COUNTRYDES, ', '),3) COUNTRYDES "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT USERREGIONS.REGRIONNAME, "
                StrSql = StrSql + "USERREGIONS.REGIONID, "
                StrSql = StrSql + "CNTRY.COUNTRYDES, "
                StrSql = StrSql + "COUNT(*) OVER ( PARTITION BY USERREGIONS.REGIONID ) CNT, "
                StrSql = StrSql + "ROW_NUMBER () OVER ( PARTITION BY USERREGIONS.REGIONID ORDER BY CNTRY.COUNTRYDES) SEQ "
                StrSql = StrSql + "FROM USERCOUNTRYREGIONS UREGCNTRY "
                StrSql = StrSql + "INNER JOIN DIMCOUNTRIES CNTRY "
                StrSql = StrSql + "ON CNTRY.COUNTRYID = UREGCNTRY.COUNTRYID "
                StrSql = StrSql + "INNER JOIN USERREGIONS "
                StrSql = StrSql + "ON USERREGIONS.REGIONID = UREGCNTRY.REGIONID "
                StrSql = StrSql + "WHERE USERREGIONS.USERID = " + UserId + " "
                StrSql = StrSql + ") "
                StrSql = StrSql + "WHERE    SEQ=CNT "
                StrSql = StrSql + "START WITH    SEQ=1 "
                StrSql = StrSql + "CONNECT BY PRIOR    SEQ+1=SEQ "
                StrSql = StrSql + "AND PRIOR   REGIONID=REGIONID "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetRegionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetRegionCntryDetails(ByVal RegionId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                StrSql = "SELECT USERREGIONS.REGRIONNAME,  "
                StrSql = StrSql + "CNTRY.COUNTRYID, "
                StrSql = StrSql + "CNTRY.COUNTRYDES, "
                StrSql = StrSql + "CNTRY.ABBREVIATIONS, "
                StrSql = StrSql + "CASE WHEN NVL(CNTRY.COUNTRYID,0) = NVL(UREGCNTRY.COUNTRYID,0) "
                StrSql = StrSql + "THEN "
                StrSql = StrSql + "'Y' "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "'N' "
                StrSql = StrSql + "END ISINREGION "
                StrSql = StrSql + "FROM USERCOUNTRYREGIONS UREGCNTRY "
                StrSql = StrSql + "RIGHT OUTER JOIN DIMCOUNTRIES CNTRY "
                StrSql = StrSql + "ON UREGCNTRY.COUNTRYID = CNTRY.COUNTRYID "
                StrSql = StrSql + "LEFT OUTER JOIN USERREGIONS "
                StrSql = StrSql + "ON NVL(USERREGIONS.REGIONID," + RegionId + ") = NVL(UREGCNTRY.REGIONID," + RegionId + ") "
                StrSql = StrSql + "WHERE NVL(UREGCNTRY.REGIONID," + RegionId + ") = " + RegionId + " "
                StrSql = StrSql + "ORDER BY CNTRY.COUNTRYDES "


                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetRegionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "RegionSet"
        Public Function GetRegionsSetDetails() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim Id As String = String.Empty
            Try
                StrSql = "SELECT USRREGS.REGIONSETNAME,  "
                StrSql = StrSql + "USRREGS.REGIONSETID, "
                StrSql = StrSql + "COUNT(USRREG.REGIONSETID) REGIONCOUNT "
                StrSql = StrSql + "FROM USERREGIONSETS USRREGS "
                StrSql = StrSql + "LEFT OUTER  JOIN USERREGIONS USRREG "
                StrSql = StrSql + "ON USRREGS.REGIONSETID=USRREG.REGIONSETID "
                StrSql = StrSql + "GROUP BY "
                StrSql = StrSql + "USRREGS.REGIONSETNAME, "
                StrSql = StrSql + "USRREGS.REGIONSETID "
                StrSql = StrSql + "ORDER BY UPPER(USRREGS.REGIONSETNAME) "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetRegionsSetDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetRegionSet_Regions(ByVal RegionSetId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim Id As String = String.Empty

            Try
                StrSql = "SELECT USERREGIONSETS.REGIONSETID,  "
                StrSql = StrSql + "USERREGIONSETS.REGIONSETNAME, "
                StrSql = StrSql + "USERREGIONS.REGIONID, "
                StrSql = StrSql + "USERREGIONS.REGIONNAME, "
                StrSql = StrSql + "DIMCOUNTRIES.COUNTRYID, "
                StrSql = StrSql + "DIMCOUNTRIES.COUNTRYDES "
                StrSql = StrSql + "FROM USERREGIONSETS "
                StrSql = StrSql + "INNER JOIN USERREGIONS "
                StrSql = StrSql + "ON USERREGIONS.REGIONSETID = USERREGIONSETS.REGIONSETID "
                StrSql = StrSql + "LEFT OUTER JOIN USERREGIONCOUNTRIES "
                StrSql = StrSql + "ON USERREGIONCOUNTRIES.REGIONID = USERREGIONS.REGIONID "
                StrSql = StrSql + "LEFT OUTER JOIN DIMCOUNTRIES "
                StrSql = StrSql + "ON DIMCOUNTRIES.COUNTRYID = USERREGIONCOUNTRIES.COUNTRYID "
                StrSql = StrSql + "WHERE USERREGIONSETS.REGIONSETID = " + RegionSetId + " "
                StrSql = StrSql + "ORDER BY USERREGIONSETS.REGIONSETNAME,USERREGIONS.REGIONNAME,DIMCOUNTRIES.COUNTRYDES "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUserRegionSet_Regions:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Base Reports"
        Public Function GetUserCustomReportsForSearch(ByVal UserId As String, ByVal ReportName As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERREPORTID REPORTID,  "
                StrSql = StrSql + "REPORTNAME, "
                StrSql = StrSql + "USERREPORTID||':'||REPORTNAME REPORTDES, "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "RPTTYPE, "
                StrSql = StrSql + "'Prop' Type, "
                StrSql = StrSql + "RPTFACT, "
                StrSql = StrSql + "RPTTYPEDES, "
                StrSql = StrSql + "CREATEDDATE "
                StrSql = StrSql + "FROM USERREPORTS "
                StrSql = StrSql + "WHERE USERID = " + UserId.ToString() + " "
                StrSql = StrSql + "AND UPPER(REPORTNAME) LIKE '%" + ReportName + "%' "
                StrSql = StrSql + " ORDER BY REPORTNAME "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1GetData:GetUserCustomReports:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetBaseReports(ByVal grpId As String, ByVal repDes As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                'StrSql = "SELECT USERREPORTID REPORTID,  "
                'StrSql = StrSql + "REPORTNAME, "
                'StrSql = StrSql + "USERREPORTID||':'||REPORTNAME REPORTDES, "
                'StrSql = StrSql + "RPTTYPE, "
                'StrSql = StrSql + "RPTFACT, "
                'StrSql = StrSql + "CREATEDDATE "
                'StrSql = StrSql + "FROM USERREPORTS "
                'StrSql = StrSql + "WHERE USERREPORTS.GROUPID IN(" + grpId + ") "
                'StrSql = StrSql + " ORDER BY REPORTNAME"
                StrSql = "SELECT USERREPORTID REPORTID,  "
                StrSql = StrSql + "REPORTNAME, "
                StrSql = StrSql + "USERREPORTID||':'||REPORTNAME REPORTDES, "
                StrSql = StrSql + "RPTTYPE, "
                StrSql = StrSql + "RPTFACT, "
                StrSql = StrSql + "CREATEDDATE "
                StrSql = StrSql + "FROM USERREPORTS "
                StrSql = StrSql + "WHERE USERREPORTS.GROUPID IN(" + grpId + ") "
                StrSql = StrSql + "AND UPPER(USERREPORTID||REPORTNAME || RPTTYPE) LIKE '%" + repDes.ToString().Replace("'", "''").ToUpper() + "%' "
                StrSql = StrSql + "AND USERREPORTID<1000 "
                StrSql = StrSql + " ORDER BY REPORTNAME"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetBaseReports:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetBaseReportsByRptId(ByVal RptId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERREPORTID REPORTID,  "
                StrSql = StrSql + "REPORTNAME, "
                StrSql = StrSql + "USERREPORTID||':'||REPORTNAME REPORTDES, "
                StrSql = StrSql + "RPTTYPE, "
                StrSql = StrSql + "RPTFACT, "
                StrSql = StrSql + "RPTTYPEDES, "
                StrSql = StrSql + "CREATEDDATE "
                StrSql = StrSql + "FROM USERREPORTS "
                StrSql = StrSql + "WHERE USERREPORTID = " + RptId.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetBaseReportsByRptId:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

#End Region
#Region "Product Tree"
        Public Function GetProducts(ByVal ParentId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT ID,PARENTID,CATEGORYNAME,FACTID  "
                StrSql = StrSql + " FROM PRODUCTCATEGORIES "
                StrSql = StrSql + " WHERE PARENTID=" + ParentId
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetProductsUptoProduct(ByVal ParentId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT ID,PARENTID,CATEGORYNAME,FACTID   "
                StrSql = StrSql + " FROM PRODUCTCATEGORIES "
                StrSql = StrSql + " WHERE PARENTID=" + ParentId
                StrSql = StrSql + " AND FACTID IS NOT NULL"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetBaseReports:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region
#Region "Group Tree"
        Public Function GetGroups(ByVal ParentId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PRODGROUPID,NAME "
                StrSql = StrSql + " FROM PRODUCTGROUP "
                StrSql = StrSql + " WHERE PARENTID=" + ParentId + " "
                StrSql = StrSql + " ORDER BY NAME ASC"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetGroups:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        'Public Function GetProductsUptoProduct(ByVal ParentId As String) As DataSet
        '    Dim Dts As New DataSet()
        '    Dim odbUtil As New DBUtil()
        '    Dim StrSql As String = String.Empty
        '    Try
        '        StrSql = "SELECT ID,PARENTID,CATEGORYNAME,FACTID   "
        '        StrSql = StrSql + " FROM PRODUCTCATEGORIES "
        '        StrSql = StrSql + " WHERE PARENTID=" + ParentId
        '        StrSql = StrSql + " AND FACTID IS NOT NULL"
        '        Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
        '        Return Dts
        '    Catch ex As Exception
        '        Throw New Exception("M1SubGetData:GetBaseReports:" + ex.Message.ToString())
        '        Return Dts
        '    End Try
        'End Function
#End Region

#Region "Free Market1 Changes"
        Public Function GetReportName(ByVal rptId As String) As String
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim name As String = String.Empty
            Try
                StrSql = "SELECT  REPORTNAME FROM USERREPORTS WHERE USERREPORTID=" + rptId
                StrSql = StrSql + " UNION SELECT REPORTNAME FROM BASEREPORTS WHERE BASEREPORTID=" + rptId

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                If Dts.Tables(0).Rows.Count > 0 Then
                    name = Dts.Tables(0).Rows(0)("REPORTNAME").ToString()
                End If
                Return name
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetReportName:" + ex.Message.ToString())
                Return name
            End Try
        End Function
        Public Function GetUserCustomReportsByRptIdTemp(ByVal RptId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERREPORTID REPORTID,  "
                StrSql = StrSql + "REPORTNAME, "
                StrSql = StrSql + "USERREPORTID||':'||REPORTNAME REPORTDES, "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "RPTTYPE, "
                StrSql = StrSql + "RPTFACT, "
                StrSql = StrSql + "RPTTYPEDES, "
                StrSql = StrSql + "REGIONSETID, "
                StrSql = StrSql + "REGIONID, "
                StrSql = StrSql + "SERVICEID, "
                StrSql = StrSql + "CREATEDDATE "
                StrSql = StrSql + "FROM USERREPORTSTEMP "
                StrSql = StrSql + "WHERE USERREPORTID = " + RptId.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUserCustomReportsByRptIdTemp:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUsersReportRowsTemp(ByVal reportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                '''''DATE  03-JAN-2012
                StrSql = "SELECT  "
                StrSql = StrSql + "USERREPORTROWID, "
                StrSql = StrSql + "USERREPORTID, "
                StrSql = StrSql + "ROWSEQUENCE, "
                StrSql = StrSql + "ROWDECRIPTION, "
                StrSql = StrSql + "ROWVALUETYPE,ROWVALID1,ROWVALID2, "
                StrSql = StrSql + "UNITS.UNITID,UNITID1,UNITID2, "
                StrSql = StrSql + "UNITS.UNITSHRT,U1.UNITSHRT UNITSHRT1,U2.UNITSHRT UNITSHRT2, "
                StrSql = StrSql + "ROWVALUE,ROWVAL1,ROWVAL2 "
                StrSql = StrSql + "FROM USERREPORTROWSTEMP UR "
                StrSql = StrSql + "LEFT JOIN UNITS ON UR.UNITID=UNITS.UNITID "
                StrSql = StrSql + "LEFT JOIN UNITS U1 ON UR.UNITID1=U1.UNITID "
                StrSql = StrSql + "LEFT JOIN UNITS U2 ON UR.UNITID2=U2.UNITID "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "USERREPORTID=CASE WHEN " + reportId + "=-1 THEN USERREPORTID  ELSE " + reportId + " END "
                StrSql = StrSql + " ORDER BY USERREPORTROWID "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUsersReportRowsTemp:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUsersReportColumnsTemp(ByVal reportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                StrSql = "SELECT  "
                StrSql = StrSql + "USERREPORTCOLUMNID, "
                StrSql = StrSql + "USERREPORTID, "
                StrSql = StrSql + "COLUMNVALUETYPE, "
                StrSql = StrSql + "COLUMNVALUEID, "
                StrSql = StrSql + "COLUMNSEQUENCE, "
                StrSql = StrSql + "COLUMNVALUE, "
                StrSql = StrSql + "INPUTVALUE1, "
                StrSql = StrSql + "INPUTVALUE2 "
                StrSql = StrSql + "FROM USERREPORTCOLUMNSTEMP "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "USERREPORTID=CASE WHEN " + reportId + "=-1 THEN USERREPORTID  ELSE " + reportId + " END "
                StrSql = StrSql + "ORDER BY COLUMNSEQUENCE ASC "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUsersReportColumnsTemp:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUsersReportFiltersTemp(ByVal reportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "USERREPORTFILTERID, "
                StrSql = StrSql + "USERREPORTID, "
                StrSql = StrSql + "FILTERSEQUENCE,FILTERTYPE,FILTERVALUE,FILTERTYPEID,FILTERVALUEID, "
                StrSql = StrSql + "FILTERVALUE "
                StrSql = StrSql + "FROM USERREPORTFILTERSTEMP "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "USERREPORTID=CASE WHEN " + reportId + "=-1 THEN USERREPORTID  ELSE " + reportId + " END "
                StrSql = StrSql + "ORDER BY FILTERSEQUENCE ASC "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUsersReportFiltersTemp:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Preferences"
        Public Function GetPref(ByVal reportId As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT * "
                StrSql = StrSql + "FROM WEBPREFERRENCES "
                StrSql = StrSql + "WHERE REPORTID = " + reportId.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPref:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Private Function GetMaterialId(ByVal type As ArrayList, ByVal Id As ArrayList) As String
            Dim strMatId As String = String.Empty
            For i = 0 To type.Count - 1
                If type.Item(i).ToString() = "Material" Then
                    If strMatId = String.Empty Then
                        strMatId = Id(i).ToString()
                    Else
                        strMatId = "," + Id(i).ToString()
                    End If
                End If
            Next
            Return strMatId
        End Function

        Private Function GetRegCntryId(ByVal type As ArrayList, ByVal Id As ArrayList) As String
            Dim strCntryId As String = String.Empty
            For i = 0 To type.Count - 1
                If type.Item(i).ToString() = "Region" Then
                    If Id.Contains("0") = False Then
                        If strCntryId = String.Empty Then
                            strCntryId = "AND COUNTRYID IN (SELECT COUNTRYID FROM USERREGIONCOUNTRIES WHERE REGIONID IN (" + Id(i).ToString()
                        Else
                            strCntryId = strCntryId + "," + Id(i).ToString()
                        End If
                    End If
                End If
            Next
            If strCntryId <> String.Empty Then
                strCntryId = strCntryId + "))"
            End If
            Return strCntryId
        End Function

        Private Function GetCountryId(ByVal type As ArrayList, ByVal Id As ArrayList) As String
            Dim strCntryId As String = String.Empty
            For i = 0 To type.Count - 1
                If type.Item(i).ToString() = "Country" Then
                    If strCntryId = String.Empty Then
                        strCntryId = Id(i).ToString()
                    Else
                        strCntryId = strCntryId + "," + Id(i).ToString()
                    End If
                End If
            Next
            Return strCntryId
        End Function

        Private Function GetPackId(ByVal type As ArrayList, ByVal Id As ArrayList) As String
            Dim strPackId As String = String.Empty
            For i = 0 To type.Count - 1
                If type.Item(i).ToString() = "Package" Then
                    If strPackId = String.Empty Then
                        strPackId = Id(i).ToString()
                    Else
                        strPackId = "," + Id(i).ToString()
                    End If
                End If
            Next
            Return strPackId
        End Function

        Private Function GetProdId(ByVal type As ArrayList, ByVal Id As ArrayList, ByVal FactId As String) As String
            Dim strProdId As String = String.Empty
            For i = 0 To type.Count - 1
                If type.Item(i).ToString() = "Product" Then
                    If strProdId = String.Empty Then
                        If Id(i).ToString() = "0" Then
                            strProdId = FactId
                        Else
                            strProdId = Id(i).ToString()
                        End If
                    Else
                        If Id(i).ToString() = "0" Then
                            strProdId = "," + FactId
                        Else
                            strProdId = "," + Id(i).ToString()
                        End If
                    End If
                End If
            Next
            Return strProdId
        End Function

        Public Function GetPackageGroups(ByVal GroupId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT * "
                StrSql = StrSql + "FROM PACKGROUP WHERE ID IN(" + GroupId + ") "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPref:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

#End Region

#Region "Subscription"
        Public Function GetSubscriptionDetails(ByVal ServiceId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT SERVICEID,  "
                StrSql = StrSql + "LICENSEID, GROUPID, REGIONID, SERVICENAME "
                StrSql = StrSql + "FROM USERMARKET1SERVICE "
                StrSql = StrSql + "WHERE SERVICEID = " + ServiceId.ToString() + " "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetSubscriptionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSubRegionDetails(ByVal RegionId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim Regionid1 As String = String.Empty
            Dim Regionid2 As String = String.Empty
            Try
                
                StrSql = "SELECT DISTINCT REGIONSETID  "
                StrSql = StrSql + "FROM USERREGIONS "
                StrSql = StrSql + "WHERE REGIONID IN (" + RegionId.ToString() + " )"
                

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetSubscriptionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSubGroupDetails(ByVal GroupId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT GetCatProducts(" + GroupId + ")  "
                StrSql = StrSql + "CATID  FROM DUAL "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetSubscriptionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSubFactGroupDetails(ByVal FactId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT F.TYPE AS VALUE,F.ID as ID,F.ProductType as ProductType  "
                StrSql = StrSql + "FROM FACT F "
                StrSql = StrSql + "WHERE F.ID IN (SELECT FACTID FROM PRODUCTCATEGORIES "
                StrSql = StrSql + "WHERE ID IN(" + FactId + "))"

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetSubscriptionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUserReportFilter(ByVal ReportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT * FROM USERREPORTFILTERSTEMP "
                StrSql = StrSql + "WHERE USERREPORTID= " + ReportId + ""
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetSubscriptionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUserReportProdFilter(ByVal ReportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT * FROM USERREPORTFILTERSTEMP "
                StrSql = StrSql + "WHERE USERREPORTID= " + ReportId + ""
                'StrSql = StrSql + "AND FILTERSEQUENCE= 1"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetSubscriptionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUserReportProdFilterR(ByVal ReportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT * FROM USERREPORTFILTERS "
                StrSql = StrSql + "WHERE USERREPORTID= " + ReportId + ""
                'StrSql = StrSql + "AND FILTERSEQUENCE= 1"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetSubscriptionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUserReportFilter2R(ByVal ReportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT * FROM USERREPORTFILTERS "
                StrSql = StrSql + "WHERE USERREPORTID= " + ReportId + " "
                'StrSql = StrSql + "AND FILTERSEQUENCE= 2"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetSubscriptionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPackageTypeByFact(ByVal FactId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PACKAGETYPE.PACKAGETYPEID AS ID, "
                StrSql = StrSql + "PACKAGETYPEDE1 DES1, "
                StrSql = StrSql + "PACKAGETYPEDE2 DES2, "

                StrSql = StrSql + "(PACKAGETYPE.PACKAGETYPEDE1||' '||PACKAGETYPE.PACKAGETYPEDE2)VALUE "
                StrSql = StrSql + "FROM FACTPACKTYPEPACKSIZE "
                StrSql = StrSql + "INNER JOIN PACKAGETYPE "
                StrSql = StrSql + "ON PACKAGETYPE.PACKAGETYPEID=FACTPACKTYPEPACKSIZE.PACKAGETYPEID "
                StrSql = StrSql + "WHERE FACTID IN (" + FactId.ToString() + ")"

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPackageGroupTypeByFact(ByVal FactId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  DISTINCT GROUPID "
                StrSql = StrSql + "FROM FACTPACKTYPEPACKSIZE "
                StrSql = StrSql + "INNER JOIN PACKAGETYPE "
                StrSql = StrSql + "ON PACKAGETYPE.PACKAGETYPEID=FACTPACKTYPEPACKSIZE.PACKAGETYPEID "
                StrSql = StrSql + "WHERE FACTID IN (" + FactId.ToString() + ")"

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetUserSubscRegions(ByVal ServiceId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT REGIONID "
                StrSql = StrSql + "FROM USERMARKET1SERVICE "
                StrSql = StrSql + "WHERE SERVICEID=" + ServiceId

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUsersReportRows:" + ex.Message.ToString())
                Return Dts

            End Try
        End Function

        Public Function GetFilterCountriesByRegion(ByVal RegionId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                'StrSql = "SELECT  "
                'StrSql = StrSql + "DC.COUNTRYID ID, "
                'StrSql = StrSql + "DC.COUNTRYDES VALUE "
                'StrSql = StrSql + "FROM USERREGIONCOUNTRIES UC "
                'StrSql = StrSql + "INNER JOIN USERREGIONS UR "
                'StrSql = StrSql + "ON UR.REGIONID=UC.REGIONID "
                'StrSql = StrSql + "INNER JOIN DIMCOUNTRIES DC "
                'StrSql = StrSql + "ON UC.COUNTRYID=DC.COUNTRYID "
                'StrSql = StrSql + "WHERE  UC.REGIONID IN (" + RegionId + ")"
                'StrSql = StrSql + "ORDER BY COUNTRYDES "

                StrSql = "SELECT  "
                StrSql = StrSql + "DC.COUNTRYID ID, "
                StrSql = StrSql + "DC.COUNTRYDES VALUE "
                StrSql = StrSql + "FROM DIMCOUNTRIES DC "
                StrSql = StrSql + "ORDER BY COUNTRYDES "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts


            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetFilterCountriesByRegion:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSubscProducts(ByVal Id As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT ID,PARENTID,CATEGORYNAME,FACTID  "
                StrSql = StrSql + " FROM PRODUCTCATEGORIES "
                StrSql = StrSql + " WHERE ID IN (" + Id + ")"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSubscMaterials(ByVal Id As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MATERIALID ID, GROUPID "
                StrSql = StrSql + " FROM PACKTYPEMATERIAL "
                StrSql = StrSql + " WHERE PRODID IN (" + Id + ")"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSubscPackMaterials(ByVal Id As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MATERIALID ID, GROUPID "
                StrSql = StrSql + " FROM PACKTYPEMATERIAL "
                StrSql = StrSql + " WHERE PACKTYPEID IN (" + Id + ")"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSubscPackProdMaterials(ByVal ProdId As String, ByVal PackId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MATERIALID ID, GROUPID "
                StrSql = StrSql + " FROM PACKTYPEMATERIAL "
                StrSql = StrSql + " WHERE PRODID IN (" + ProdId + ")"
                StrSql = StrSql + " AND PACKTYPEID IN (" + PackId + ")"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSubscProdMaterials(ByVal MatId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  MATID ID,  "
                StrSql = StrSql + " (MATERIALS.MATDE1||' '||MATERIALS.MATDE2)VALUE "
                StrSql = StrSql + " FROM MATERIALS "
                StrSql = StrSql + " WHERE MATID IN (" + MatId + ")"
                StrSql = StrSql + " ORDER BY MATDE1 "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSubscProdGroups(ByVal GrpId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT ID,   (GROUPNAME)VALUE  "
                StrSql = StrSql + "FROM MATERIALGROUP "
                StrSql = StrSql + "INNER JOIN MATERIALMATGRP MG ON "
                StrSql = StrSql + "MG.GROUPID=MATERIALGROUP.ID "
                StrSql = StrSql + "WHERE MG.GROUPID IN (" + GrpId + ")"

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUserReportFilter2(ByVal ReportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT * FROM USERREPORTFILTERSTEMP "
                StrSql = StrSql + "WHERE USERREPORTID= " + ReportId + " "
                StrSql = StrSql + "AND FILTERSEQUENCE= 2"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetSubscriptionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCountriesByRegion(ByVal RegionId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  "
                StrSql = StrSql + "DC.COUNTRYID ID, "
                StrSql = StrSql + "DC.COUNTRYDES NAME "
                StrSql = StrSql + "FROM USERREGIONCOUNTRIES UC "
                StrSql = StrSql + "INNER JOIN USERREGIONS UR "
                StrSql = StrSql + "ON UR.REGIONID=UC.REGIONID "
                StrSql = StrSql + "INNER JOIN DIMCOUNTRIES DC "
                StrSql = StrSql + "ON UC.COUNTRYID=DC.COUNTRYID "
                StrSql = StrSql + "WHERE  UC.REGIONID IN (" + RegionId + ")"

                StrSql = StrSql + "ORDER BY COUNTRYDES "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts


            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetFilterSQL:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSubscProdMatPackages(ByVal ProdId As String, ByVal MatId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PACKTYPEID ID "
                StrSql = StrSql + " FROM PACKTYPEMATERIAL "
                StrSql = StrSql + " WHERE PRODID IN (" + ProdId + ")"
                StrSql = StrSql + " AND MATERIALID IN (" + MatId + ")"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSubscPackages(ByVal PackId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PACKAGETYPEID ID,  "
                StrSql = StrSql + " (PACKAGETYPEDE1||' '||PACKAGETYPEDE2)VALUE "
                StrSql = StrSql + " FROM PACKAGETYPE "
                StrSql = StrSql + " WHERE PACKAGETYPEID IN (" + PackId + ")"
                StrSql = StrSql + " ORDER BY VALUE "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetSubscPackages:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSubscMatPackages_OLD(ByVal Id As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PACKTYPEID ID, GROUPID "
                StrSql = StrSql + " FROM PACKTYPEMATERIAL "
                StrSql = StrSql + " WHERE MATERIALID IN (" + Id + ")"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSubscMatPackages(ByVal Id As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PACKTYPEID PACKAGETYPEID, PACKTYPEMATERIAL.GROUPID, MATERIALID, "
                StrSql = StrSql + "(PACKAGETYPE.PACKAGETYPEDE1||' '||PACKAGETYPE.PACKAGETYPEDE2)VALUE "
                StrSql = StrSql + " FROM PACKTYPEMATERIAL "
                StrSql = StrSql + "INNER JOIN PACKAGETYPE "
                StrSql = StrSql + "ON PACKAGETYPE.PACKAGETYPEID=PACKTYPEMATERIAL.PACKTYPEID "
                StrSql = StrSql + " WHERE MATERIALID IN (" + Id + ")"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSubscPackMatProducts(ByVal PackId As String, ByVal MatId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PRODID ID "
                StrSql = StrSql + " FROM PACKTYPEMATERIAL "
                If PackId <> "" And MatId <> "" Then
                    StrSql = StrSql + " WHERE PACKTYPEID IN (" + PackId + ")"
                    StrSql = StrSql + " AND MATERIALID IN (" + MatId + ")"
                ElseIf PackId <> "" Then
                    StrSql = StrSql + " WHERE PACKTYPEID IN (" + PackId + ")"
                ElseIf MatId <> "" Then
                    StrSql = StrSql + " WHERE MATERIALID IN (" + MatId + ")"
                End If

                
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetProductTypeByPack(ByVal PackId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  DISTINCT  PACKAGETYPE.PACKAGETYPEID AS ID, "
                StrSql = StrSql + "PACKAGETYPEDE1 DES1, "
                StrSql = StrSql + "PACKAGETYPEDE2 DES2, "
                StrSql = StrSql + "FACTID, "
                StrSql = StrSql + "(PACKAGETYPE.PACKAGETYPEDE1||' '||PACKAGETYPE.PACKAGETYPEDE2)VALUE "
                StrSql = StrSql + "FROM FACTPACKTYPEPACKSIZE "
                StrSql = StrSql + "INNER JOIN PACKAGETYPE "
                StrSql = StrSql + "ON PACKAGETYPE.PACKAGETYPEID=FACTPACKTYPEPACKSIZE.PACKAGETYPEID "
                StrSql = StrSql + "WHERE FACTPACKTYPEPACKSIZE.PACKAGETYPEID IN (" + PackId.ToString() + ")"

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetSubscProductsName(ByVal ProdId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT FACTID ID,  "
                StrSql = StrSql + " CATEGORYNAME VALUE "
                StrSql = StrSql + " FROM PRODUCTCATEGORIES "
                StrSql = StrSql + " WHERE FACTID IN (" + ProdId + ") "
                StrSql = StrSql + " ORDER BY CATEGORYNAME"

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

#End Region

        Public Function GetMaterials(ByVal id As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT * "
                StrSql = StrSql + "FROM MATERIALMATGRP WHERE GROUPID = " + id.ToString()
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1GetData:GetPref:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetReportMaterialByProdPack(ByVal type As ArrayList, ByVal Id As ArrayList, ByVal FactId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                Dim strPackId = GetPackId(type, Id)
                Dim strProdId = GetProdId(type, Id, FactId)
				
				 If strProdId = "" Then
                    Dim arrFact() As String
                    Dim dsFact As New DataSet
                    Dim catId As String = String.Empty
                    arrFact = Regex.Split(HttpContext.Current.Session("M1SubGroupId").ToString(), ",")
                    For j = 0 To arrFact.Length - 1
                        dsFact = GetSubGroupDetails(arrFact(j))
                        If dsFact.Tables(0).Rows(0).Item("CATID").ToString() <> "" Then
                            If j = 0 Then
                                catId = dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            Else
                                catId = catId + dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            End If

                        End If
                    Next
                    catId = catId.Remove(catId.Length - 1)

                    'getting productID
                    dsFact = GetSubFactGroupDetails(catId)
                    FactId = String.Empty
                    For k = 0 To dsFact.Tables(0).Rows.Count - 1
                        If dsFact.Tables(0).Rows(0).Item("ID").ToString() <> "" Then
                            If k = 0 Then
                                FactId = dsFact.Tables(0).Rows(k).Item("ID").ToString()
                            Else
                                FactId = FactId + "," + dsFact.Tables(0).Rows(k).Item("ID").ToString()
                            End If
                        End If
                    Next
                    strProdId = FactId
                End If

		If strPackId = "0" Then
                    Dim dsPack As New DataSet
                    Dim PackId As String = String.Empty
                    dsPack = GetPivotProdPackage(strProdId, "")
                    For b = 0 To dsPack.Tables(0).Rows.Count - 1
                        PackId = PackId + "" + dsPack.Tables(0).Rows(b).Item("PACKAGETYPEID").ToString() + ","
                    Next
                    PackId = PackId.Remove(PackId.Length - 1)
                    strPackId = PackId
                End If
				
                StrSql = "SELECT DISTINCT MATERIALS.MATID AS ID, "
                StrSql = StrSql + "(MATDE1 || ' ' || MATDE2) NAME "
                StrSql = StrSql + "FROM MATERIALS "

                If strPackId <> String.Empty And strProdId <> String.Empty Then
                    StrSql = StrSql + "INNER JOIN PACKTYPEMATERIAL P "
                    StrSql = StrSql + "ON MATERIALS.MATID = P.MATERIALID "
                    StrSql = StrSql + "WHERE P.PRODID IN(" + strProdId + ") AND P.PACKTYPEID IN(" + strPackId + ")"
                Else
                    If strProdId <> String.Empty Then
                        StrSql = StrSql + "INNER JOIN  PACKTYPEMATERIAL P "
                        StrSql = StrSql + "ON P.MATERIALID = MATERIALS.MATID "
                        StrSql = StrSql + "WHERE P.PRODID IN(" + strProdId + ") "
                    ElseIf strPackId <> String.Empty Then
                        StrSql = StrSql + "INNER JOIN PACKTYPEMATERIAL P "
                        StrSql = StrSql + "ON P.MATERIALID = MATERIALS.MATID "
                        StrSql = StrSql + "WHERE P.PACKTYPEID IN(" + strPackId + ") "
                    End If

                     If strProdId = String.Empty Then
                        StrSql = "SELECT DISTINCT PACKAGETYPE.PACKAGETYPEID AS ID, "
                        StrSql = StrSql + "(PACKAGETYPEDE1 || ' ' || PACKAGETYPEDE2) NAME "
                        StrSql = StrSql + "FROM PACKAGETYPE WHERE PACKAGETYPEID=-1 "
                    End If
                End If

                StrSql = StrSql + " ORDER BY NAME"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1GetData:GetFilterSQL:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCAGRReportColumn(ByVal ColumnId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT COLUMNVALUEID FROM USERREPORTCOLUMNS "
                StrSql = StrSql + "WHERE USERREPORTCOLUMNID= " + ColumnId + " "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetSubscriptionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetReportDetails(ByVal rptId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim name As String = String.Empty
            Try
                StrSql = "SELECT  REPORTNAME,RPTTYPEDES,RPTTYPE,REGIONSETID FROM USERREPORTS WHERE USERREPORTID=" + rptId

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetReportDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetProductGroups(ByVal GroupId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT A.PRODUCTGROUPID,B.NAME FROM PRODUCTCATGROUP A  "
                StrSql = StrSql + "INNER JOIN PRODUCTGROUP B "
                StrSql = StrSql + "ON A.PRODUCTGROUPID=B.PRODGROUPID "
                StrSql = StrSql + "WHERE CATEGORYID IN(" + GroupId + ") "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPref:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetProductGroupsNew(ByVal GroupId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PRODGROUPID,NAME FROM PRODUCTGROUP WHERE PRODGROUPID IN"
                StrSql = StrSql + "(SELECT PRODUCTGROUPID FROM PRODUCTCATGROUP WHERE CATEGORYID IN"
                StrSql = StrSql + "(SELECT ID FROM PRODUCTCATEGORIES WHERE PARENTID IN  "
                StrSql = StrSql + "(SELECT ID FROM PRODUCTCATEGORIES WHERE PARENTID IN  "
                StrSql = StrSql + "(SELECT ID FROM PRODUCTCATEGORIES WHERE PARENTID IN (" + GroupId + ")))) ) "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPref:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetProductCatGroupsNew(ByVal FactId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PRODGROUPID,NAME FROM PRODUCTGROUP WHERE PRODGROUPID IN"
                StrSql = StrSql + "(SELECT PRODUCTGROUPID FROM PRODUCTCATGROUP WHERE CATEGORYID IN"
                StrSql = StrSql + "(SELECT ID FROM PRODUCTCATEGORIES WHERE FACTID IN (" + FactId + ")) ) "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPref:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetProductGroupTypeByFact(ByVal FactId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                'StrSql = "SELECT  DISTINCT PRODUCTGROUPID "
                'StrSql = StrSql + "FROM PRODUCTCATGROUP "
                'StrSql = StrSql + "WHERE CATEGORYID IN (" + FactId.ToString() + ")"
                StrSql = "SELECT DISTINCT A.PRODUCTGROUPID,B.NAME FROM PRODUCTCATGROUP A  "
                StrSql = StrSql + "INNER JOIN PRODUCTGROUP B "
                StrSql = StrSql + "ON A.PRODUCTGROUPID=B.PRODGROUPID "
                StrSql = StrSql + "WHERE CATEGORYID IN(" + FactId.ToString() + ") "


                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetReportFiltersByRepId(ByVal reportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT * "
                StrSql = StrSql + "FROM USERREPORTFILTERS WHERE "
                StrSql = StrSql + "USERREPORTID = " + reportId
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetReportFiltersByRepId:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUsersReportRowsRep(ByVal reportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

                '''''DATE  03-JAN-2012
                StrSql = "SELECT  "
                StrSql = StrSql + "USERREPORTROWID, "
                StrSql = StrSql + "USERREPORTID, "
                StrSql = StrSql + "ROWSEQUENCE, "
                StrSql = StrSql + "ROWDECRIPTION, "
                StrSql = StrSql + "ROWVALUETYPE, "
                StrSql = StrSql + "UNITS.UNITID, "
                StrSql = StrSql + "UNITS.UNITSHRT, "
                StrSql = StrSql + "ROWVALUE "
                StrSql = StrSql + "FROM USERREPORTROWS UR "
                StrSql = StrSql + "LEFT JOIN UNITS ON UR.UNITID=UNITS.UNITID "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "USERREPORTID=CASE WHEN " + reportId + "=-1 THEN USERREPORTID  ELSE " + reportId + " END "
                StrSql = StrSql + " ORDER BY USERREPORTROWID"

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUsersReportRowsRep:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
		

#Region "Changes 16Jan2017"
        Public Function GetParentProducts(ByVal ParentId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT ID,PARENTID,CATEGORYNAME,FACTID  "
                StrSql = StrSql + " FROM PRODUCTCATEGORIES "
                StrSql = StrSql + " WHERE ID IN (" + ParentId + ")"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1GetData:GetParentProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetRegDetails(ByVal GroupId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim Id As String = String.Empty
            Try
                StrSql = "SELECT USRREGS.REGIONSETNAME,  "
                StrSql = StrSql + "USRREGS.REGIONSETID, "
                StrSql = StrSql + "COUNT(USRREG.REGIONSETID) REGIONCOUNT "
                StrSql = StrSql + "FROM USERREGIONSETS USRREGS "
                StrSql = StrSql + "LEFT OUTER  JOIN USERREGIONS USRREG "
                StrSql = StrSql + "ON USRREGS.REGIONSETID=USRREG.REGIONSETID "
                StrSql = StrSql + "LEFT OUTER  JOIN GROUPREGIONSET "
                StrSql = StrSql + "ON GROUPREGIONSET.REGIONSETID=USRREG.REGIONSETID "
                StrSql = StrSql + "WHERE GROUPREGIONSET.GROUPID IN (" + GroupId + ") "
                StrSql = StrSql + "GROUP BY "
                StrSql = StrSql + "USRREGS.REGIONSETNAME, "
                StrSql = StrSql + "USRREGS.REGIONSETID "
                StrSql = StrSql + "ORDER BY UPPER(USRREGS.REGIONSETNAME) "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1GetData:GetRegDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

#End Region
#Region "Limited Years"
        Public Function GetGroupYears(ByVal GroupId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT YEARIDMIN,YEARIDMAX "
                StrSql = StrSql + "FROM GROUPYEARS WHERE GROUPID IN (" + GroupId + ")"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetGroupYears:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetColSQL_Group(ByVal rSql As String, ByVal minyr As String, ByVal maxyr As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                Dts = (GetColsSelector(rSql))

                If Dts.Tables(0).Rows(0).Item("COLSQL").ToString() <> "" Then
                    rSql = Dts.Tables(0).Rows(0).Item("COLSQL").ToString()
                    StrSql = "SELECT *  "
                    StrSql = StrSql + "FROM "
                    StrSql = StrSql + "(" + rSql + ")"
                    StrSql = StrSql + "DUAL WHERE ID BETWEEN " + minyr + " AND " + maxyr + ""
                End If

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetRowSQL:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region


#Region "Group feature"
        Public Function GetGroupIDCheck(ByVal Des1 As String, ByVal Des2 As String, ByVal UserID As String, ByVal Type As Object, ByVal ServiceID As String) As DataSet
            Dim odButil As New DBUtil()
            Dim strsql As String = String.Empty
            Dim Dts As New DataSet
            Try
                'Getting GROUPID 
                strsql = String.Empty
                strsql = "SELECT 1 "
                strsql = strsql + "FROM "
                strsql = strsql + "GROUPS "
                strsql = strsql + "WHERE "
                strsql = strsql + "UPPER(DES1)='" + (Des1.ToString().Replace("'", "''")).ToUpper() + "' "
                strsql = strsql + "AND "
                If Des2 = "" Then
                    strsql = strsql + "DES2 IS NULL "
                Else
                    strsql = strsql + "UPPER(DES2)='" + (Des2.ToString().Replace("'", "''")).ToUpper() + "' "
                End If
                strsql = strsql + "AND SERVICEID= " + ServiceID + " "

                Dts = odButil.FillDataSet(strsql, Market1Connection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetGroupIDCheck:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetGroupIDByService(ByVal UserID As String, ByVal ServiceID As String, ByVal Des1 As String) As DataSet
            Dim Dts As New DataSet()
            Try
                Dim odbUtil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("Market1ConnectionString")
                Dim StrSql As String = ""
                StrSql = "SELECT GROUPID,(DES1 ||' '|| DES2) GROUPNAME,(GROUPID ||' '|| DES1 ||' '|| DES2) GROUPDES,DES1 DES1,DES2 DES2 "
                StrSql = StrSql + "FROM "

                StrSql = StrSql + "(SELECT 0 GROUPID, 'All Reports' GROUPNAME,(0 ||' '|| '' ||' '|| '') GROUPDES,'All Reports' DES1,'' DES2 FROM DUAL "
                StrSql = StrSql + "UNION ALL "
                StrSql = StrSql + "SELECT GROUPID,(DES1 ||''|| DES2) GROUPNAME,(GROUPID ||' '|| DES1 ||' '|| DES2) GROUPDES,des1 DES1,DES2 DES2  FROM GROUPS WHERE USERID=" + UserID + " AND SERVICEID= " + ServiceID + ") "
                StrSql = StrSql + "WHERE NVL(UPPER(DES1),'#') LIKE '%" + Des1.ToUpper() + "%' "
                StrSql = StrSql + "OR NVL(UPPER(GROUPDES),'#') LIKE '%" + Des1.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY UPPER(GROUPNAME) "

                Dts = odbUtil.FillDataSet(StrSql, MyConnectionString)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function

        Function GetPManageReportGrpDetails(ByVal UserName As String, ByVal UserID As String, ByVal keyWord As String, ByVal Type As String, ByVal ServiceId As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New M1SubGetData.Selectdata()
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim strSQL As String = String.Empty
            Dim ds As New DataSet()
            Dim ReportIDs As String = String.Empty
            Dim DtRes As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim MyConnectionString As String = ""
            Dim dtOutPut As New DataSet()
            Dim strSqlOutPut As String = String.Empty
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("Market1ConnectionString")
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
                strSQL = strSQL + "AND SERVICEID= " + ServiceId + " "
                DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                If DtRes.Tables(0).Rows.Count > 0 Then
                    For j = 0 To DtRes.Tables(0).Rows.Count - 1
                        'Getting ReportID
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "REPORTID "
                        strSQL = strSQL + "FROM GROUPREPORTS "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString()
                        strSQL = strSQL + " ORDER BY  SEQ"
                        Dts = odbUtil.FillDataSet(strSQL, MyConnectionString)
                        If Dts.Tables(0).Rows.Count > 0 Then
                            For i = 0 To Dts.Tables(0).Rows.Count - 1
                                If i = 0 Then
                                    ReportIDs = Dts.Tables(0).Rows(i).Item("REPORTID").ToString()
                                Else
                                    ReportIDs = ReportIDs + ", " + Dts.Tables(0).Rows(i).Item("REPORTID").ToString()
                                End If
                            Next
                        Else
                            ReportIDs = ""
                        End If
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "DES1 GROUPNAME, "
                        strSQL = strSQL + "'" + ReportIDs.ToString() + "' REPORTID, "
                        strSQL = strSQL + "DES2, "
                        strSQL = strSQL + "to_char(GPS.CREATIONDATE,'mm/dd/yyyy hh:mi:ss AM'  ) CREATIONDATE, "
                        strSQL = strSQL + "CASE WHEN GPS.CREATIONDATE-GPS.UPDATEDATE =0 THEN 'NA' ELSE to_char(GPS.UPDATEDATE,'mm/dd/yyyy hh:mi:ss AM'  ) END  UPDATEDATE "
                        strSQL = strSQL + "FROM "
                        strSQL = strSQL + "GROUPS GPS "
                        strSQL = strSQL + "WHERE GPS.USERID= " + UserID + " AND GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString() + " "
                        If j = 0 Then
                            strSqlOutPut = strSQL
                        Else
                            strSqlOutPut = strSqlOutPut + " UNION ALL " + strSQL
                        End If
                    Next

                    strSqlOutPut = "SELECT * FROM ( " + strSqlOutPut + " ) DUAL "
                    strSqlOutPut = strSqlOutPut + "WHERE NVL(UPPER(DES2),'#') LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSqlOutPut = strSqlOutPut + "OR NVL(UPPER(REPORTID),'#') LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSqlOutPut = strSqlOutPut + "OR NVL(UPPER(GROUPNAME),'#') LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper() + "%' "
                    strSqlOutPut = strSqlOutPut + "OR GROUPID LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper() + "%' " + " ORDER BY UPPER(GROUPNAME),UPPER(DES2)"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, MyConnectionString)
                Else
                    strSqlOutPut = " SELECT * FROM GROUPREPORTS WHERE GROUPID=0"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, MyConnectionString)
                End If

                Return dtOutPut
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPManageReportGrpDetails:" + ex.Message.ToString())
                Return DtRes
            End Try
        End Function

        Public Function GetGroupIDCheckD(ByVal Des1 As String, ByVal Des2 As String, ByVal UserID As String, ByVal Type As Object, ByVal ServiceID As String) As DataSet
            Dim odButil As New DBUtil()
            Dim strsql As String = String.Empty
            Dim Dts As New DataSet
            Try
                'Getting GROUPID 
                strsql = String.Empty
                strsql = "SELECT 1 "
                strsql = strsql + "FROM "
                strsql = strsql + "GROUPS "
                strsql = strsql + "WHERE "
                strsql = strsql + "UPPER(DES1)='" + (Des1.ToString().Replace("'", "''")).ToUpper() + "' "
                strsql = strsql + "AND "
                If Des2 = "" Then
                    strsql = strsql + "DES2 IS NULL "
                Else
                    strsql = strsql + "UPPER(DES2)='" + (Des2.ToString().Replace("'", "''")).ToUpper() + "' "
                End If
                strsql = strsql + "AND SERVICEID= " + ServiceID + " "

                Dts = odButil.FillDataSet(strsql, Market1Connection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetGroupIDCheckD:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetMaxSEQReports(ByVal grpId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT NVL(MAX(SEQ),0) MAXCOUNT  "
                StrSql = StrSql + "FROM GROUPREPORTS "
                StrSql = StrSql + "WHERE GROUPID= " + grpId

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetMaxSEQReports:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetAllGroupDetails(ByVal UserID As String, ByVal ServiceID As String) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New M1SubGetData.Selectdata()
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim strSQL As String = String.Empty
            Dim ds As New DataSet()
            Dim ReportIDs As String = String.Empty
            Dim DtRes As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim MyConnectionString As String = ""
            Dim dtOutPut As New DataSet()
            Dim strSqlOutPut As String = String.Empty
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("Market1ConnectionString")
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
                strSQL = strSQL + "AND SERVICEID= " + ServiceID + " "
                DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                If DtRes.Tables(0).Rows.Count > 0 Then
                    For j = 0 To DtRes.Tables(0).Rows.Count - 1
                        'Getting ReportID
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "REPORTID "
                        strSQL = strSQL + "FROM GROUPREPORTS "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString()
                        strSQL = strSQL + " ORDER BY  SEQ"
                        Dts = odbUtil.FillDataSet(strSQL, Market1Connection)
                        If Dts.Tables(0).Rows.Count > 0 Then
                            For i = 0 To Dts.Tables(0).Rows.Count - 1
                                If i = 0 Then
                                    ReportIDs = Dts.Tables(0).Rows(i).Item("REPORTID").ToString()
                                Else
                                    ReportIDs = ReportIDs + ", " + Dts.Tables(0).Rows(i).Item("REPORTID").ToString()
                                End If
                            Next
                        Else
                            ReportIDs = ""
                        End If
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "DES1 GROUPNAME, "
                        strSQL = strSQL + "'" + ReportIDs.ToString() + "' REPORTID, "
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
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, Market1Connection)
                Else
                    strSqlOutPut = " SELECT * FROM GROUPREPORTS WHERE GROUPID=0"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, Market1Connection)
                End If

                Return dtOutPut
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function

        Public Function GetPReportDetailsByUserGrp(ByVal UserName As String, ByVal keyWord As String, ByVal ServiceID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  DISTINCT USERREPORTID,CASEDES,CASEDES1,CREATEDDATE  "
                StrSql = StrSql + " FROM "
                StrSql = StrSql + " ( "
                StrSql = StrSql + "SELECT "
                StrSql = StrSql + "PC.USERREPORTID, "
                StrSql = StrSql + "(PC.USERREPORTID||' '||PC.REPORTNAME)CASEDES, "
                StrSql = StrSql + " PC.REPORTNAME CASEDES1, "
                StrSql = StrSql + " PC.CREATEDDATE "
                StrSql = StrSql + "FROM GROUPS "
                StrSql = StrSql + "INNER JOIN GROUPREPORTS "
                StrSql = StrSql + "ON GROUPREPORTS.GROUPID=GROUPS.Groupid "
                StrSql = StrSql + "RIGHT OUTER JOIN USERREPORTS PC "
                StrSql = StrSql + "ON PC.USERREPORTID=GROUPREPORTS.REPORTID "
                StrSql = StrSql + "WHERE UPPER(PC.USERID) ='" + UserName.ToUpper().ToString() + "' "
                StrSql = StrSql + "AND PC.SERVICEID= " + ServiceID + " "
                StrSql = StrSql + " ) "
                StrSql = StrSql + "WHERE UPPER(CASEDES) LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper().Trim() + "%' "
                StrSql = StrSql + "OR UPPER(USERREPORTID) LIKE '%" + keyWord.ToString().Replace("'", "''").ToUpper().Trim() + "%' "

                StrSql = StrSql + "ORDER BY UPPER(CASEDES1) "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPReportDetailsByUserGrp:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPReportByGroups(ByVal UserName As String, ByVal ReportDe1 As String, ByVal grpId As String, ByVal Type As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  DISTINCT USERREPORTID,CASEDES "
                StrSql = StrSql + " FROM "
                StrSql = StrSql + " ( "
                If Type = "PROP" Then
                    StrSql = StrSql + "SELECT "
                    StrSql = StrSql + "PC.USERREPORTID, "
                    StrSql = StrSql + "(PC.USERREPORTID||' '||PC.REPORTNAME)CASEDES "
                    StrSql = StrSql + "FROM GROUPS "
                    StrSql = StrSql + "INNER JOIN GROUPREPORTS "
                    StrSql = StrSql + "ON GROUPREPORTS.GROUPID=GROUPS.GROUPID "
                    StrSql = StrSql + "RIGHT OUTER JOIN USERREPORTS PC "
                    StrSql = StrSql + "ON PC.USERREPORTID=GROUPREPORTS.REPORTID "
                    StrSql = StrSql + "WHERE UPPER(PC.USERID) ='" + UserName.ToUpper().ToString() + "' "
                    StrSql = StrSql + "AND PC.USERREPORTID IN(SELECT REPORTID FROM GROUPREPORTS WHERE GROUPID=" + grpId + " ) "
                End If

                StrSql = StrSql + " ) "
                StrSql = StrSql + "WHERE NVL(UPPER(CASEDES),'#') LIKE '%" + ReportDe1.ToUpper() + "%' "
                StrSql = StrSql + "OR USERREPORTID LIKE '%" + ReportDe1.ToUpper() + "%' "
                StrSql = StrSql + "ORDER BY UPPER(USERREPORTID),UPPER(CASEDES) "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPReportByGroups:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUserCustomReportsForSub(ByVal UserId As String, ByVal ServiceID As String, ByVal Text As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty

            Try
                StrSql = "SELECT USERREPORTID REPORTID,  "
                StrSql = StrSql + "REPORTNAME, "
                StrSql = StrSql + "USERREPORTID||':'||REPORTNAME REPORTDES, "
                StrSql = StrSql + "USERID, "
                StrSql = StrSql + "RPTTYPE, "
                StrSql = StrSql + "'Prop' Type, "
                StrSql = StrSql + "RPTFACT, "
                StrSql = StrSql + "RPTTYPEDES, "
                StrSql = StrSql + "CREATEDDATE "
                StrSql = StrSql + "FROM USERREPORTS "
                StrSql = StrSql + "WHERE USERID = " + UserId.ToString() + " "
                StrSql = StrSql + "AND SERVICEID=" + ServiceID + " "
                StrSql = StrSql + "AND UPPER(USERREPORTID||REPORTNAME || RPTTYPE) LIKE '%" + Text.ToString().Replace("'", "''").ToUpper() + "%' "
             
                StrSql = StrSql + " ORDER BY UPPER(REPORTNAME) "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw
                Return Dts
            End Try
        End Function

        Public Function GetUserCustomReportsbyGroup(ByVal UserId As String, ByVal ServiceId As String, ByVal GrpID As String, ByVal Text As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty

            Try
                StrSql = "SELECT DISTINCT  GROUPREPORTS.REPORTID,USERREPORTS.REPORTNAME,USERREPORTS.RPTTYPE FROM GROUPREPORTS "
                StrSql = StrSql + " INNER JOIN USERREPORTS ON GROUPREPORTS.REPORTID=USERREPORTS.USERREPORTID "
                StrSql = StrSql + "WHERE USERREPORTS.USERID ='" + UserId + "' AND GROUPREPORTS.GROUPID=" + GrpID + " "
                StrSql = StrSql + "AND UPPER(REPORTNAME) LIKE '%" + Text.ToString().Replace("'", "''").ToUpper() + "%' "
                StrSql = StrSql + " AND USERREPORTS.USERREPORTID IN(SELECT REPORTID FROM GROUPREPORTS WHERE GROUPID =" + GrpID + " AND SERVICEID=" + ServiceId + " ) ORDER BY UPPER(REPORTNAME) "


                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUserCustomReportsForSearch:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetMaxSEQGREPORT(ByVal grpId As String, ByVal ServiceID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT NVL(MAX(SEQ),0) MAXCOUNT  "
                StrSql = StrSql + "FROM GROUPREPORTS "
                StrSql = StrSql + "WHERE GROUPID= " + grpId + ""
                StrSql = StrSql + "AND SERVICEID= " + ServiceID + ""
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetMaxSEQGREPORT:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetGroupDetails(ByVal UserID As String, ByVal ServiceID As String, ByVal flag As Char) As DataSet
            Dim Dts As New DataSet()
            Dim objGetData As New M1SubGetData.Selectdata()
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim strSQL As String = String.Empty
            Dim ds As New DataSet()
            Dim ReportIDs As String = String.Empty
            Dim DtRes As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim MyConnectionString As String = ""
            Dim dtOutPut As New DataSet()
            Dim strSqlOutPut As String = String.Empty
            MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("Market1ConnectionString")
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
                strSQL = strSQL + "AND SERVICEID= " + ServiceID + " "
                DtRes = odbUtil.FillDataSet(strSQL, MyConnectionString)
                If DtRes.Tables(0).Rows.Count > 0 Then
                    For j = 0 To DtRes.Tables(0).Rows.Count - 1
                        'Getting ReportID
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "REPORTID "
                        strSQL = strSQL + "FROM GROUPREPORTS "
                        strSQL = strSQL + "WHERE GROUPID=" + DtRes.Tables(0).Rows(j).Item("GROUPID").ToString()
                        strSQL = strSQL + " ORDER BY  SEQ"
                        Dts = odbUtil.FillDataSet(strSQL, Market1Connection)
                        If Dts.Tables(0).Rows.Count > 0 Then
                            For i = 0 To Dts.Tables(0).Rows.Count - 1
                                If i = 0 Then
                                    ReportIDs = Dts.Tables(0).Rows(i).Item("REPORTID").ToString()
                                Else
                                    ReportIDs = ReportIDs + ", " + Dts.Tables(0).Rows(i).Item("REPORTID").ToString()
                                End If
                            Next
                        Else
                            ReportIDs = ""
                        End If
                        strSQL = "SELECT GROUPID,  "
                        strSQL = strSQL + "DES1 GROUPNAME, "
                        strSQL = strSQL + "'" + ReportIDs.ToString() + "' ReportID, "
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
                        strSQL = strSQL + "'NA' ReportID, "
                        strSQL = strSQL + "'NA'  GROUPDES, "
                        strSQL = strSQL + "'0:None'  CDES1, "
                        strSQL = strSQL + "'NA'  CREATIONDATE, "
                        strSQL = strSQL + "'NA'  UPDATEDATE "
                        strSQL = strSQL + "FROM "
                        strSQL = strSQL + "DUAL "
                        strSqlOutPut = strSqlOutPut + " UNION ALL " + strSQL
                    End If
                    strSqlOutPut = "SELECT * FROM ( " + strSqlOutPut + " ) DUAL ORDER BY UPPER(GROUPNAME),UPPER(GROUPDES)"
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, Market1Connection)
                Else
                    strSqlOutPut = " SELECT * FROM GROUPREPORTS WHERE GROUPID=0 "
                    dtOutPut = odbUtil.FillDataSet(strSqlOutPut, Market1Connection)
                End If

                Return dtOutPut
            Catch ex As Exception
                Throw
                Return DtRes
            End Try
        End Function
#End Region


#Region "Pivot Report"
        Public Function GetPivotReportsCols(ByVal UserReportId As String, ByVal Seq As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERREPORTCOLUMNID,  "
                StrSql = StrSql + "USERREPORTID, "
                StrSql = StrSql + "COLUMNSEQUENCE, "
                StrSql = StrSql + "COLUMNDECRIPTION, "
                StrSql = StrSql + "COLUMNVALUETYPE, "
                StrSql = StrSql + "COLUMNVALUE, "
                StrSql = StrSql + "INPUTVALUETYPE1, "
                StrSql = StrSql + "INPUTVALUE1, "
                StrSql = StrSql + "INPUTVALUETYPE2, "
                StrSql = StrSql + "INPUTVALUE2, "
                StrSql = StrSql + "INPUTVALUETYPE3, "
                StrSql = StrSql + "INPUTVALUE3, "
                StrSql = StrSql + "INPUTVALUETYPE4, "
                StrSql = StrSql + "INPUTVALUE4 "
                StrSql = StrSql + "FROM USERREPORTCOLUMNS "
                StrSql = StrSql + "WHERE USERREPORTID  = " + UserReportId.ToString() + " AND COLUMNSEQUENCE=" + Seq

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotReportsCols:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotReportDataEMEA(ByVal TblName() As String, ByVal RptType As String, ByVal YearId As String, ByVal PackId As String, ByVal UnitId As String, ByVal Count As Integer, ByVal RegID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim colStr As String = ""
            Dim grpStr As String = ""
            Try
                If RptType = "MAT" Then
                    colStr = "YEARID,UNITID,CATEGORYID,PACKAGETYPEID,MATERIALID "
                    grpStr = "YEARID,UNITID,CATEGORYID,PACKAGETYPEID,MATERIALID "
                Else
                    colStr = "YEARID,UNITID,CATEGORYID,PACKAGETYPEID,COUNTRYID "
                    grpStr = "YEARID,UNITID,CATEGORYID,PACKAGETYPEID,COUNTRYID "
                End If
                For a = 0 To Count - 1
                    If a <> 0 Then
                        StrSql = StrSql + " UNION "
                    End If
                    StrSql = StrSql + "SELECT SUM(FACT) FCT," + colStr + " "
                    StrSql = StrSql + "FROM " + TblName(a) + " FCT  INNER JOIN USERREGIONCOUNTRIES ON USERREGIONCOUNTRIES.COUNTRYID=FCT.COUNTRYID INNER JOIN USERREGIONS ON USERREGIONS.REGIONID=USERREGIONCOUNTRIES.REGIONID  "
                    'If RptType = "MAT" Then
                    '    StrSql = StrSql + "INNER JOIN MATERIALS MAT" + (a + 1).ToString() + " ON MAT" + (a + 1).ToString() + ".MATID=FCT" + (a + 1).ToString() + ".MATERIALID "
                    'End If
                    StrSql = StrSql + "WHERE UNITID = " + UnitId + " AND YEARID IN (" + YearId + ") AND PACKAGETYPEID IN (" + PackId + ")  AND USERREGIONS.REGIONID IN (" + RegID + ") "
                    If RptType = "MAT" Then
                        StrSql = StrSql + "AND MATERIALID IS NOT NULL "
                    End If
                    StrSql = StrSql + "GROUP BY " + grpStr + ""

                Next
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotReportData:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotReportFiltersByRepId(ByVal reportId As String, ByVal Seq As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT USERREPORTID,FILTERVALUE,FILTERSEQUENCE,FILTERTYPEID,FILTERVALUE,FILTERTYPE "
                StrSql = StrSql + "FROM USERREPORTFILTERS WHERE "
                StrSql = StrSql + "USERREPORTID = " + reportId + " AND FILTERSEQUENCE = " + Seq
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetReportFiltersByRepId:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotReportDataEMEA_BW(ByVal TblName() As String, ByVal RptType As String, ByVal YearId As String, ByVal PackId As String, ByVal UnitId As String, ByVal Count As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim colStr As String = ""
            Dim grpStr As String = ""
            Try
                If RptType = "MAT" Then
                    colStr = "YEARID,UNITID,CATEGORYID,PACKAGETYPEID,MATERIALID "
                    grpStr = "YEARID,UNITID,CATEGORYID,PACKAGETYPEID,MATERIALID "
                Else
                    colStr = "YEARID,UNITID,CATEGORYID,PACKAGETYPEID,COUNTRYID "
                    grpStr = "YEARID,UNITID,CATEGORYID,PACKAGETYPEID,COUNTRYID "
                End If
                For a = 0 To Count - 1
                    If TblName(a) <> "" Then
                        If a <> 0 Then
                            StrSql = StrSql + " UNION "
                        End If
                        StrSql = StrSql + "SELECT SUM(FACT) FCT," + colStr + " "
                        StrSql = StrSql + "FROM " + TblName(a) + " FCT "
                        'If RptType = "MAT" Then
                        '    StrSql = StrSql + "INNER JOIN MATERIALS MAT" + (a + 1).ToString() + " ON MAT" + (a + 1).ToString() + ".MATID=FCT" + (a + 1).ToString() + ".MATERIALID "
                        'End If
                        StrSql = StrSql + "WHERE UNITID = " + UnitId + " AND YEARID IN (" + YearId + ") AND PACKAGETYPEID IN (" + PackId + ") AND COUNTRYID IS NOT NULL "
                        If RptType = "MAT" Then
                            StrSql = StrSql + "AND MATERIALID IS NOT NULL "
                        End If
                        StrSql = StrSql + "GROUP BY " + grpStr + ""
                    End If
                Next
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotReportData:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotRegionset(ByVal ReportId As String) As Integer
            Dim Dts As New DataSet()
            Dim Id As New Integer
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT REGIONSETID FROM USERREPORTS WHERE USERREPORTID=" + ReportId

                Id = odbUtil.FillData(StrSql, Market1Connection)
                Return Id
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPivotRegions(ByVal SetId As String) As DataSet
            Dim Dts As New DataSet()
            Dim unitId As New Integer
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT REGIONID,REGIONNAME FROM USERREGIONS WHERE REGIONSETID=" + SetId

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPivotReportRegData(ByVal TblName() As String, ByVal YearId As String, ByVal PackId As String, ByVal UnitId As String, ByVal Count As Integer, ByVal SetId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim colStr As String = ""
            Dim grpStr As String = ""
            Try
                For a = 0 To Count - 1
                    If TblName(a) <> "" Then
                        If TblName(a) <> "" Then
                            If a <> 0 Then
                                StrSql = StrSql + " UNION "
                            End If

                            StrSql = StrSql + "SELECT SUM(FACT) FCT,UR.REGIONID,CATEGORYID,PACKAGETYPEID,YEARID,UNITID FROM " + TblName(a) + " FCT "
                            StrSql = StrSql + "INNER JOIN USERREGIONCOUNTRIES UC ON UC.COUNTRYID=FCT.COUNTRYID INNER JOIN USERREGIONS UR ON UR.REGIONID=UC.REGIONID "
                            StrSql = StrSql + "WHERE UR.REGIONSETID=" + SetId + " AND YEARID IN (" + YearId + ") AND PACKAGETYPEID IN (" + PackId + ") AND UNITID=" + UnitId + " "
                            If UnitId = "4" Then
                                StrSql = StrSql + "AND MATERIALID=-1 "
                            Else
                                StrSql = StrSql + "AND MATERIALID IS NULL "
                            End If

                            StrSql = StrSql + "Group BY UR.REGIONID,CATEGORYID,PACKAGETYPEID,YEARID,UNITID "
                        End If
                    End If
                Next
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotReportData:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetProductDetails(ByVal GroupId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ds As New DataSet
            Try
                StrSql = "SELECT Fun_GetFactSQLDATA(" + GroupId + ") "
                StrSql = StrSql + "CATID  FROM DUAL "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                StrSql = Dts.Tables(0).Rows(0).Item("CATID").ToString()
                ds = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return ds
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetSubscriptionDetails:" + ex.Message.ToString())
                Return ds
            End Try
        End Function

        Public Function GetPivotReportData(ByVal TblName() As String, ByVal RptType As String, ByVal YearId As String, ByVal PackId As String, ByVal UnitId As String, ByVal Count As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim colStr As String = ""
            Dim grpStr As String = ""
            Try
                If RptType = "MAT" Then
                    colStr = "YEARID,UNITID,CATEGORYID,PACKAGETYPEID,MATERIALID "
                    grpStr = "YEARID,UNITID,CATEGORYID,PACKAGETYPEID,MATERIALID "
                Else
                    colStr = "YEARID,UNITID,CATEGORYID,PACKAGETYPEID,COUNTRYID "
                    grpStr = "YEARID,UNITID,CATEGORYID,PACKAGETYPEID,COUNTRYID "
                End If
                For a = 0 To Count - 1
                    If TblName(a) <> "" Then
                        If a <> 0 Then
                            StrSql = StrSql + " UNION "
                        End If
                        StrSql = StrSql + "SELECT SUM(FACT) FCT," + colStr + " "
                        StrSql = StrSql + "FROM " + TblName(a) + " FCT "
                        'If RptType = "MAT" Then
                        '    StrSql = StrSql + "INNER JOIN MATERIALS MAT" + (a + 1).ToString() + " ON MAT" + (a + 1).ToString() + ".MATID=FCT" + (a + 1).ToString() + ".MATERIALID "
                        'End If
                        StrSql = StrSql + "WHERE UNITID = " + UnitId + " AND YEARID IN (" + YearId + ") AND PACKAGETYPEID IN (" + PackId + ") AND COUNTRYID IS NOT NULL "
                        If RptType = "MAT" Then
                            StrSql = StrSql + "AND MATERIALID IS NOT NULL "
                        End If
                        StrSql = StrSql + "GROUP BY " + grpStr + ""
                    End If


                Next
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotReportData:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotReportDataCNTRY(ByVal TblName() As String, ByVal RptType As String, ByVal YearId As String, ByVal PackId As String, ByVal UnitId As String, ByVal Count As Integer, ByVal CNTRY As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim colStr As String = ""
            Dim grpStr As String = ""
            Try
                If RptType = "MAT" Then
                    colStr = "YEARID,UNITID,CATEGORYID,PACKAGETYPEID,MATERIALID "
                    grpStr = "YEARID,UNITID,CATEGORYID,PACKAGETYPEID,MATERIALID "
                Else
                    colStr = "YEARID,UNITID,CATEGORYID,PACKAGETYPEID,COUNTRYID "
                End If
                For a = 0 To Count - 1
                    If TblName(a) <> "" Then
                        If a <> 0 Then
                            StrSql = StrSql + " UNION "
                        End If
                        StrSql = StrSql + "SELECT SUM(FACT) FCT," + colStr + " "
                        StrSql = StrSql + "FROM " + TblName(a) + " FCT "
                        'If RptType = "MAT" Then
                        '    StrSql = StrSql + "INNER JOIN MATERIALS MAT" + (a + 1).ToString() + " ON MAT" + (a + 1).ToString() + ".MATID=FCT" + (a + 1).ToString() + ".MATERIALID "
                        'End If
                        StrSql = StrSql + "WHERE UNITID = " + UnitId + " AND YEARID IN (" + YearId + ") AND PACKAGETYPEID IN (" + PackId + ") AND COUNTRYID IN (" + CNTRY.ToString() + ") "
                        If RptType = "MAT" Then
                            StrSql = StrSql + "AND MATERIALID IS NOT NULL "
                        End If
                        StrSql = StrSql + "GROUP BY " + grpStr + ""
                    End If
                Next
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotReportData:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotReportDataREGION(ByVal TblName() As String, ByVal RptType As String, ByVal YearId As String, ByVal PackId As String, ByVal UnitId As String, ByVal Count As Integer, ByVal RegionID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim colStr As String = ""
            Dim grpStr As String = ""
            Try
                If RptType = "MAT" Then
                    colStr = "YEARID,UNITID,CATEGORYID,PACKAGETYPEID,MATERIALID "
                    grpStr = "YEARID,UNITID,CATEGORYID,PACKAGETYPEID,MATERIALID "
                Else
                    colStr = "YEARID,UNITID,CATEGORYID,PACKAGETYPEID,COUNTRYID "
                    grpStr = "YEARID,UNITID,CATEGORYID,PACKAGETYPEID,COUNTRYID "
                End If
                For a = 0 To Count - 1
                    If TblName(a) <> "" Then
                        If a <> 0 Then
                            StrSql = StrSql + " UNION "
                        End If
                        StrSql = StrSql + "SELECT SUM(FACT) FCT," + colStr + " "
                        StrSql = StrSql + "FROM " + TblName(a) + " FCT "
                        'If RptType = "MAT" Then
                        '    StrSql = StrSql + "INNER JOIN MATERIALS MAT" + (a + 1).ToString() + " ON MAT" + (a + 1).ToString() + ".MATID=FCT" + (a + 1).ToString() + ".MATERIALID "
                        'End If
                        StrSql = StrSql + "WHERE UNITID = " + UnitId + " AND YEARID IN (" + YearId + ") AND PACKAGETYPEID IN (" + PackId + ") AND COUNTRYID IN (SELECT COUNTRYID FROM USERREGIONCOUNTRIES WHERE REGIONID IN(" + RegionID.ToString() + ")) "
                        If RptType = "MAT" Then
                            StrSql = StrSql + "AND MATERIALID IS NOT NULL "
                        End If
                        StrSql = StrSql + "GROUP BY " + grpStr + ""
                    End If
                Next
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotReportData:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotBuyersReportData(ByVal TblName() As String, ByVal RptType As String, ByVal YearId As String, ByVal RegID As String, ByVal UnitId As String, ByVal Count As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim colStr As String = ""
            Dim grpStr As String = ""
            Try

                colStr = "YEARID,UNITID,CATEGORYID,SUBGROUPID,USERREGIONS.REGIONID "
                grpStr = "YEARID,UNITID,CATEGORYID,SUBGROUPID,USERREGIONS.REGIONID "

                For a = 0 To Count - 1
                    If TblName(a) <> "" Then
                        If a <> 0 Then
                            StrSql = StrSql + " UNION "
                        End If
                        StrSql = StrSql + "SELECT SUM(FACT) FCT," + colStr + " "
                        StrSql = StrSql + "FROM " + TblName(a) + " FCT INNER JOIN USERREGIONCOUNTRIES ON USERREGIONCOUNTRIES.COUNTRYID=FCT.COUNTRYID INNER JOIN USERREGIONS ON USERREGIONS.REGIONID=USERREGIONCOUNTRIES.REGIONID "

                        StrSql = StrSql + "WHERE UNITID = " + UnitId + " AND YEARID IN (" + YearId + ") AND NVL(PACKAGETYPEID,-1)=-1 AND USERREGIONS.REGIONID IN (" + RegID + ") AND NVL(SUBGROUPID,-1)<>-1 "

                        StrSql = StrSql + "GROUP BY " + grpStr + ""
                    End If


                Next
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotReportData:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetProductDescription(ByVal GroupId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ds As New DataSet
            Try
                StrSql = "SELECT ID,CATEGORYNAME,FACTID FROM PRODUCTCATEGORIES WHERE FACTID IN (" + GroupId + ") "

                ds = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return ds
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetSubscriptionDetails:" + ex.Message.ToString())
                Return ds
            End Try
        End Function

        Public Function GetPivotMaterialss(ByVal ProdId As String, ByVal PackId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT MATERIALID ID,GROUPID,PRODID,PACKTYPEID,(MATDE1 ||' '|| MATDE2) DES "
                StrSql = StrSql + " FROM PACKTYPEMATERIAL PTM "
                StrSql = StrSql + " INNER JOIN MATERIALS MAT ON MAT.MATID= PTM.MATERIALID"
                StrSql = StrSql + " WHERE PRODID IN (" + ProdId + ")"
                StrSql = StrSql + " AND PACKTYPEID IN (" + PackId + ")"
                StrSql = StrSql + " ORDER BY PRODID,DES ASC"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotMaterials:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotBuyers(ByVal ProdId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PRODGROUPID SUBGROUPID, NAME GROUPNAME FROM PRODUCTCATGROUP  "
                StrSql = StrSql + "INNER JOIN PRODUCTGROUP ON PRODUCTGROUP.PARENTID=PRODUCTCATGROUP.PRODUCTGROUPID "
                StrSql = StrSql + "AND CATEGORYID IN ( SELECT ID FROM PRODUCTCATEGORIES WHERE FACTID IN(" + ProdId + ")) "
                StrSql = StrSql + "ORDER BY NAME ASC "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotBuyers:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotRegionn(ByVal regID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "select * from USERREGIONS WHERE REGIONID =" + regID + ""

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPivotCountry() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT COUNTRYID,COUNTRYDES FROM DIMCOUNTRIES ORDER BY COUNTRYID "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPivotUnit(ByVal ReportId As String) As Integer
            Dim Dts As New DataSet()
            Dim unitId As New Integer
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT UNITID FROM USERREPORTROWS WHERE USERREPORTID=" + ReportId

                unitId = odbUtil.FillData(StrSql, Market1Connection)
                Return unitId
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPivotPackageTYP(ByVal FactId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PACKAGETYPE.PACKAGETYPEID AS ID, "
                StrSql = StrSql + "PACKAGETYPEDE1 DES1, "
                StrSql = StrSql + "PACKAGETYPEDE2 DES2, "

                StrSql = StrSql + "(PACKAGETYPE.PACKAGETYPEDE1||' '||PACKAGETYPE.PACKAGETYPEDE2)VALUE "
                StrSql = StrSql + "FROM FACTPACKTYPEPACKSIZE "
                StrSql = StrSql + "INNER JOIN PACKAGETYPE "
                StrSql = StrSql + "ON PACKAGETYPE.PACKAGETYPEID=FACTPACKTYPEPACKSIZE.PACKAGETYPEID "
                StrSql = StrSql + "WHERE FACTID IN (" + FactId.ToString() + ") ORDER BY DES1 "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPivotPackage(ByVal FactId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PACKAGETYPE.PACKAGETYPEID AS PACKAGETYPEID,FACTID, "
                StrSql = StrSql + "PACKAGETYPEDE1 DES1, "
                StrSql = StrSql + "PACKAGETYPEDE2 DES2, "

                StrSql = StrSql + "(PACKAGETYPE.PACKAGETYPEDE1||' '||PACKAGETYPE.PACKAGETYPEDE2)VALUE "
                StrSql = StrSql + "FROM FACTPACKTYPEPACKSIZE "
                StrSql = StrSql + "INNER JOIN PACKAGETYPE "
                StrSql = StrSql + "ON PACKAGETYPE.PACKAGETYPEID=FACTPACKTYPEPACKSIZE.PACKAGETYPEID "
                StrSql = StrSql + "WHERE FACTID IN (" + FactId.ToString() + ")"

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetMedicalDeviceMaterials(ByVal ProdId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT  ID, DES FROM("
                StrSql = StrSql + "SELECT MATERIALID ID,GROUPID,PRODID,PACKTYPEID,(MATDE1 ||' '|| MATDE2) DES "
                StrSql = StrSql + " FROM PACKTYPEMATERIAL PTM "
                StrSql = StrSql + " INNER JOIN MATERIALS MAT ON MAT.MATID= PTM.MATERIALID"
                StrSql = StrSql + " WHERE PRODID IN (" + ProdId + ")"
                StrSql = StrSql + " ORDER BY PRODID,DES ASC) ORDER BY DES "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetMedicalDeviceMaterials:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "PIVOT REP NEW"
        Public Function GetPivotRegion(ByVal RegionId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT REGIONID,  "
                StrSql = StrSql + "REGIONNAME VALUE, "
                StrSql = StrSql + "REGIONSETID "
                StrSql = StrSql + "FROM USERREGIONS "
                StrSql = StrSql + "WHERE REGIONID IN (" + RegionId + ")"
                StrSql = StrSql + " ORDER BY REGIONNAME"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetReportRegionsByRegionSet:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotCountry(ByVal RegionId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT COUNTRYID,COUNTRYDES FROM DIMCOUNTRIES "
                If RegionId <> "0" Then
                    StrSql = StrSql + "WHERE COUNTRYID IN (SELECT COUNTRYID FROM USERREGIONCOUNTRIES WHERE REGIONID IN(" + RegionId + ")) ORDER BY COUNTRYDES ASC "
                Else
                    StrSql = StrSql + "WHERE COUNTRYID IS NOT NULL ORDER BY COUNTRYDES ASC "
                End If

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPivotRepMaterials(ByVal MatId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  MATID ID,  "
                StrSql = StrSql + " (MATERIALS.MATDE1||' '||MATERIALS.MATDE2)VALUE, PTM.PRODID FACTID, PTM.PACKTYPEID "
                StrSql = StrSql + " FROM MATERIALS "
                StrSql = StrSql + "INNER JOIN PACKTYPEMATERIAL PTM "
                StrSql = StrSql + "ON PTM.MATERIALID=MATERIALS.MATID "
                StrSql = StrSql + " WHERE MATID IN (" + MatId + ")"
                StrSql = StrSql + " ORDER BY MATDE1 "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotProductDescription(ByVal GroupId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ds As New DataSet
            Try
                StrSql = "SELECT ID CATEGORYID,CATEGORYNAME VALUE, FACTID ID FROM PRODUCTCATEGORIES WHERE FACTID IN (" + GroupId + ") "

                ds = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return ds
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetSubscriptionDetails:" + ex.Message.ToString())
                Return ds
            End Try
        End Function

        Public Function GetPivotCountriesByRegion(ByVal CountryId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DC.COUNTRYID, DC.COUNTRYDES VALUE "
                StrSql = StrSql + "FROM DIMCOUNTRIES DC WHERE COUNTRYID = " + CountryId

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts


            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetFilterCountriesByRegion:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        

          Public Function GetPivotReportData_REGION(ByVal TblName() As String, ByVal MatId As String, ByVal YearId As String, ByVal ProdId As String, ByVal PackId As String,
                                                  ByVal GrpId As String, ByVal CompId As String, ByVal UnitId As String, ByVal Count As Integer, ByVal RegionID As String, ByVal dsReg As DataSet) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim colStr As String = "YEARID,UNITID,"
            Dim grpStr As String = ""
            Try
                If ProdId <> "" Then
                    colStr = colStr + "CATEGORYID,"
                End If
                If PackId <> "" Then
                    colStr = colStr + "PACKAGETYPEID,"
                End If
                If MatId <> "" Then
                    colStr = colStr + "MATERIALID,"
                End If
                If GrpId <> "" Then
                    colStr = colStr + "SUBGROUPID,"
                End If
                If CompId <> "" Then
                    colStr = colStr + "COMPONENTID,"
                End If

                colStr = colStr.Remove(colStr.Length - 1)
                For a = 0 To Count - 1
                    If TblName(a) <> "" Then
                        If a <> 0 Then
                            StrSql = StrSql + " UNION ALL "
                        End If
                        For i = 0 To dsReg.Tables(0).Rows.Count - 1
                            If i <> 0 Then
                                StrSql = StrSql + " UNION ALL "
                            End If
                            StrSql = StrSql + "SELECT SUM(FACT) FCT," + colStr + "," + dsReg.Tables(0).Rows(i).Item("ID").ToString() + " REGIONID "
                            StrSql = StrSql + "FROM " + TblName(a) + " FCT "
                            StrSql = StrSql + "WHERE UNITID = " + UnitId + " AND YEARID IN (" + YearId + ") AND COUNTRYID IN (SELECT COUNTRYID FROM USERREGIONCOUNTRIES WHERE REGIONID IN(" + dsReg.Tables(0).Rows(i).Item("ID").ToString() + ")) "
                            If PackId <> "" Then
                                StrSql = StrSql + "AND PACKAGETYPEID IN (" + PackId + ") "
                            End If
                            If MatId <> "" Then
                                StrSql = StrSql + "AND MATERIALID IN (" + MatId + ") "
                            Else
                                If UnitId <> "4" Then
                                    StrSql = StrSql + "AND MATERIALID IS NULL "
                                Else
                                    StrSql = StrSql + "AND NVL(MATERIALID,-1) = -1 "
                                End If
                            End If
                            If GrpId <> "" Then
                                StrSql = StrSql + "AND SUBGROUPID IN (" + GrpId + ") "
                            Else
                                StrSql = StrSql + "AND NVL(SUBGROUPID,-1)=-1 "
                            End If
                            If CompId <> "" Then
                                StrSql = StrSql + "AND COMPONENTID IN (" + CompId + ") "
                            Else
                                StrSql = StrSql + "AND NVL(COMPONENTID,-1)=-1 "
                            End If
                            StrSql = StrSql + "GROUP BY " + colStr + ",REGIONID "
                        Next
                    End If
                Next
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotReportData:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function


        Public Function GetPivotReportData_COUNTRY(ByVal TblName() As String, ByVal YearId As String, ByVal ProdId As String, ByVal PackId As String, ByVal MatId As String,
                                                   ByVal GrpId As String, ByVal CompId As String, ByVal RegionId As String, ByVal UnitId As String, ByVal Count As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim colStr As String = ""
            Dim grpStr As String = ""
            Try
                If ProdId <> "" Then
                    colStr = colStr + "CATEGORYID,"
                End If
                If PackId <> "" Then
                    colStr = colStr + "PACKAGETYPEID,"
                End If
                If MatId <> "" Then
                    colStr = colStr + "MATERIALID,"
                End If
                If GrpId <> "" Then
                    colStr = colStr + "SUBGROUPID,"
                End If
                If CompId <> "" Then
                    colStr = colStr + "COMPONENTID,"
                End If
                colStr = colStr.Remove(colStr.Length - 1)
                For a = 0 To Count - 1
                    If TblName(a) <> "" Then
                        If a <> 0 Then
                            StrSql = StrSql + " UNION "
                        End If
                        StrSql = StrSql + "SELECT SUM(FACT) FCT," + colStr + ",COUNTRYID,YEARID "
                        StrSql = StrSql + "FROM " + TblName(a) + " FCT "
                        StrSql = StrSql + "WHERE UNITID = " + UnitId + " AND YEARID IN (" + YearId + ") "
                        If RegionId <> "0" Then
                            StrSql = StrSql + "AND COUNTRYID IN (SELECT COUNTRYID FROM USERREGIONCOUNTRIES WHERE REGIONID IN(" + RegionId + "))"
                        Else
                            StrSql = StrSql + "AND COUNTRYID IS NOT NULL "
                        End If
                        If PackId <> "" Then
                            StrSql = StrSql + "AND PACKAGETYPEID IN (" + PackId + ") "
                        End If
                        If GrpId <> "" Then
                            StrSql = StrSql + "AND SUBGROUPID IN (" + GrpId + ") "
                        Else
                            StrSql = StrSql + "AND NVL(SUBGROUPID,-1)=-1 "
                        End If
                        If CompId <> "" Then
                            StrSql = StrSql + "AND COMPONENTID IN (" + CompId + ") "
                        Else
                            StrSql = StrSql + "AND NVL(COMPONENTID,-1)=-1 "
                        End If
                        If MatId <> "" Then
                            StrSql = StrSql + "AND MATERIALID IN (" + MatId + ") "
                        Else
                            If UnitId = "4" Then
                                StrSql = StrSql + "AND NVL(MATERIALID,-1)=-1 "
                            Else
                                StrSql = StrSql + "AND NVL(MATERIALID,-1)=-1 "
                            End If
                        End If
                        StrSql = StrSql + "GROUP BY " + colStr + ",COUNTRYID,YEARID "
                    End If
                Next
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotReportData:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function


        Public Function GetPivotReportData_MAT(ByVal TblName() As String, ByVal YearId As String, ByVal ProdId As String, ByVal PackId As String, ByVal GrpId As String, ByVal CountryId As String, ByVal RegionId As String, ByVal UnitId As String, ByVal Count As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim colStr As String = ""
            Dim grpStr As String = ""
            Try
                If ProdId <> "" Then
                    colStr = colStr + "CATEGORYID,"
                End If
                If PackId <> "" Then
                    colStr = colStr + "PACKAGETYPEID,"
                End If
                If GrpId <> "" Then
                    colStr = colStr + "SUBGROUPID,"
                End If
                If CountryId <> "" Then
                    colStr = colStr + "COUNTRYID,"
                End If
                If colStr <> "" Then
                    colStr = colStr.Remove(colStr.Length - 1)
                End If

                For a = 0 To Count - 1
                    If TblName(a) <> "" Then
                        If a <> 0 Then
                            StrSql = StrSql + " UNION "
                        End If
                        StrSql = StrSql + "SELECT SUM(FACT) FCT,MATERIALID,YEARID "
                        If colStr <> "" Then
                            StrSql = StrSql + "," + colStr + " "
                        End If
                        StrSql = StrSql + "FROM " + TblName(a) + " FCT "
                        StrSql = StrSql + "WHERE UNITID = " + UnitId + " AND YEARID IN (" + YearId + ") "
                        If UnitId <> "1" Then
                            StrSql = StrSql + "AND NVL(MATERIALID,-1)<>-1"
                        Else
                            StrSql = StrSql + "AND NVL(MATERIALID,-1)<>-1 "
                        End If
                        If PackId <> "" Then
                            StrSql = StrSql + "AND PACKAGETYPEID IN (" + PackId + ") "
                        End If
                        If GrpId <> "" Then
                            StrSql = StrSql + "AND SUBGROUPID IN (" + GrpId + ") "
			 Else
                            StrSql = StrSql + "AND NVL(SUBGROUPID,-1)=-1 "
                        End If
                        If CountryId <> "" Then
                            StrSql = StrSql + "AND COUNTRYID IN (" + CountryId + ") "
                        End If
                        If RegionId <> "" Then
                            StrSql = StrSql + "AND COUNTRYID IN (SELECT COUNTRYID FROM USERREGIONCOUNTRIES WHERE REGIONID IN(" + RegionId + ")) "
                        Else
                            StrSql = StrSql + "AND COUNTRYID IS NOT NULL "
                        End If
                        StrSql = StrSql + "GROUP BY MATERIALID,YEARID "
                        If colStr <> "" Then
                            StrSql = StrSql + "," + colStr + " "
                        End If
                    End If
                Next
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotReportData:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotReportData_PACK(ByVal TblName() As String, ByVal YearId As String, ByVal ProdId As String, ByVal MatId As String, ByVal GrpId As String,
                                                ByVal CompId As String, ByVal CountryId As String, ByVal RegionId As String, ByVal UnitId As String, ByVal Count As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim colStr As String = ""
            Dim grpStr As String = ""
            Try
                If ProdId <> "" Then
                    colStr = colStr + "CATEGORYID,"
                End If
                If GrpId <> "" Then
                    colStr = colStr + "SUBGROUPID,"
                End If
                If MatId <> "" Then
                    colStr = colStr + "MATERIALID,"
                End If
                If CountryId <> "" Then
                    colStr = colStr + "COUNTRYID,"
                End If
                If CompId <> "" Then
                    colStr = colStr + "COMPONENTID,"
                End If
                If colStr <> "" Then
                    colStr = colStr.Remove(colStr.Length - 1)
                End If

                For a = 0 To Count - 1
                    If TblName(a) <> "" Then
                        If a <> 0 Then
                            StrSql = StrSql + " UNION "
                        End If
                        StrSql = StrSql + "SELECT SUM(FACT) FCT,PACKAGETYPEID,YEARID "
                        If colStr <> "" Then
                            StrSql = StrSql + "," + colStr + " "
                        End If
                        StrSql = StrSql + "FROM " + TblName(a) + " FCT "
                        StrSql = StrSql + "WHERE UNITID = " + UnitId + " AND YEARID IN (" + YearId + ") "
                        If MatId <> "" Then
                            StrSql = StrSql + "AND MATERIALID IN (" + MatId + ") "
                        Else
                            If UnitId = "4" Then
                                StrSql = StrSql + "AND NVL(MATERIALID,-1)=-1"
                            Else
                                StrSql = StrSql + "AND NVL(MATERIALID,-1)=-1 "
                            End If
                        End If
                        If GrpId <> "" Then
                            StrSql = StrSql + "AND SUBGROUPID IN (" + GrpId + ") AND PACKAGETYPEID <> -1 "
			Else
                            StrSql = StrSql + " AND PACKAGETYPEID IS NOT NULL AND NVL(SUBGROUPID,-1)=-1 "
                        End If
                        If CompId <> "" Then
                            StrSql = StrSql + "AND COMPONENTID IN (" + CompId + ") AND PACKAGETYPEID <> -1 "
                        Else
                            StrSql = StrSql + " AND PACKAGETYPEID IS NOT NULL AND NVL(COMPONENTID,-1)=-1 "
                        End If
                        If RegionId <> "" Then
                            StrSql = StrSql + "AND COUNTRYID IN (SELECT COUNTRYID FROM USERREGIONCOUNTRIES WHERE REGIONID IN(" + RegionId + ")) "
                        Else
                            If CountryId <> "" Then
                                StrSql = StrSql + "AND COUNTRYID IN (" + CountryId + ") "
                            Else
                                StrSql = StrSql + "AND COUNTRYID IS NOT NULL "
                            End If
                        End If
                        StrSql = StrSql + "GROUP BY PACKAGETYPEID,YEARID "
                        If colStr <> "" Then
                            StrSql = StrSql + "," + colStr + " "
                        End If
                    End If
                Next
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotReportData:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function


        Public Function GetPivotReportData_PROD(ByVal TblName() As String, ByVal YearId As String, ByVal PackId As String, ByVal MatId As String, ByVal GrpId As String,
                                                ByVal CompId As String, ByVal CountryId As String, ByVal RegionId As String, ByVal UnitId As String, ByVal Count As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim colStr As String = ""
            Dim grpStr As String = ""
            Try
                If PackId <> "" Then
                    colStr = colStr + "PACKAGETYPEID,"
                End If
                If GrpId <> "" Then
                    colStr = colStr + "SUBGROUPID,"
                End If
                If MatId <> "" Then
                    colStr = colStr + "MATERIALID,"
                End If
                If CompId <> "" Then
                    colStr = colStr + "COMPONENTID,"
                End If
                If CountryId <> "" Then
                    colStr = colStr + "COUNTRYID,"
                End If
                If colStr <> "" Then
                    colStr = colStr.Remove(colStr.Length - 1)
                End If

                For a = 0 To Count - 1
                    If TblName(a) <> "" Then
                        If a <> 0 Then
                            StrSql = StrSql + " UNION "
                        End If
                        StrSql = StrSql + "SELECT SUM(FACT) FCT,CATEGORYID,YEARID "
                        If colStr <> "" Then
                            StrSql = StrSql + "," + colStr + " "
                        End If
                        StrSql = StrSql + "FROM " + TblName(a) + " FCT "
                        StrSql = StrSql + "WHERE UNITID = " + UnitId + " AND YEARID IN (" + YearId + ") "
                        If PackId <> "" Then
                            StrSql = StrSql + "AND PACKAGETYPEID IN (" + PackId + ") "
                        End If
                        If GrpId <> "" Then
                            StrSql = StrSql + "AND SUBGROUPID IN (" + GrpId + ") "
			Else
                            StrSql = StrSql + "AND NVL(SUBGROUPID,-1)=-1 "
                        End If
                        If MatId <> "" Then
                            StrSql = StrSql + "AND MATERIALID IN (" + MatId + ") "
                        Else
                            If UnitId <> "4" Then
                                StrSql = StrSql + "AND NVL(MATERIALID,-1)=-1 "
                            Else
                                StrSql = StrSql + "AND NVL(MATERIALID,-1)=-1 "
                            End If
                        End If
                        If CompId <> "" Then
                            StrSql = StrSql + "AND COMPONENTID IN (" + CompId + ") "
                        ELSE
			    StrSql = StrSql + "AND NVL(COMPONENTID,-1)=-1"
                        End If
                        If CountryId <> "" Then
                            StrSql = StrSql + "AND COUNTRYID IN (" + CountryId + ") "
                        End If
                        If RegionId <> "" Then
                            StrSql = StrSql + "AND COUNTRYID IN (SELECT COUNTRYID FROM USERREGIONCOUNTRIES WHERE REGIONID IN(" + RegionId + ")) "
                        Else
                            StrSql = StrSql + "AND COUNTRYID IS NOT NULL "
                        End If
                        StrSql = StrSql + "GROUP BY CATEGORYID,YEARID "
                        If colStr <> "" Then
                            StrSql = StrSql + "," + colStr + " "
                        End If
                    End If
                Next
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotReportData:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function


        Public Function GetPivotReportData_GRP(ByVal TblName() As String, ByVal YearId As String, ByVal PackId As String, ByVal MatId As String, ByVal CountryId As String, ByVal RegionId As String, ByVal UnitId As String, ByVal Count As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim colStr As String = ""
            Dim grpStr As String = ""
            Try
                If PackId <> "" Then
                    colStr = colStr + "PACKAGETYPEID,"
                End If
                If MatId <> "" Then
                    colStr = colStr + "MATERIALID,"
                End If
                If CountryId <> "" Then
                    colStr = colStr + "COUNTRYID,"
                End If
                If colStr <> "" Then
                    colStr = colStr.Remove(colStr.Length - 1)
                End If

                For a = 0 To Count - 1
                    If TblName(a) <> "" Then
                        If a <> 0 Then
                            StrSql = StrSql + " UNION "
                        End If
                        StrSql = StrSql + "SELECT SUM(FACT) FCT,SUBGROUPID,CATEGORYID,YEARID "
                        If colStr <> "" Then
                            StrSql = StrSql + "," + colStr + " "
                        End If
                        StrSql = StrSql + "FROM " + TblName(a) + " FCT "
                        StrSql = StrSql + "WHERE UNITID = " + UnitId + " AND YEARID IN (" + YearId + ") AND SUBGROUPID IS NOT NULL "
                        If PackId <> "" Then
                            StrSql = StrSql + "AND PACKAGETYPEID IN (" + PackId + ") "
                        End If
                        If MatId <> "" Then
                            StrSql = StrSql + "AND MATERIALID IN (" + MatId + ") "
                        Else
                            StrSql = StrSql + "AND NVL(MATERIALID,-1)=-1 "
                        End If
                        If CountryId <> "" Then
                            StrSql = StrSql + "AND COUNTRYID IN (" + CountryId + ") "
                        End If
                        If RegionId <> "" Then
                            StrSql = StrSql + "AND COUNTRYID IN (SELECT COUNTRYID FROM USERREGIONCOUNTRIES WHERE REGIONID IN(" + RegionId + ")) "
                        Else
                            StrSql = StrSql + "AND COUNTRYID IS NOT NULL "
                        End If
                        StrSql = StrSql + "GROUP BY SUBGROUPID,CATEGORYID,YEARID "
                        If colStr <> "" Then
                            StrSql = StrSql + "," + colStr + " "
                        End If
                    End If
                Next
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotReportData:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function


        Public Function GetPivotProdMatPackages(ByVal ProdId As String, ByVal MatId As String, ByVal PackId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PACKTYPEID PACKAGETYPEID, MATERIALID, PACKTYPEMATERIAL.PRODID ID, PC.ID CATEGORYID, "
                StrSql = StrSql + " (PACKAGETYPE.PACKAGETYPEDE1||' '||PACKAGETYPE.PACKAGETYPEDE2)VALUE "
                StrSql = StrSql + " FROM PACKTYPEMATERIAL "
                StrSql = StrSql + "INNER JOIN PACKAGETYPE ON PACKAGETYPE.PACKAGETYPEID=PACKTYPEMATERIAL.PACKTYPEID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATEGORIES PC ON PC.FACTID=PACKTYPEMATERIAL.PRODID "
                StrSql = StrSql + " WHERE PRODID IN (" + ProdId + ") "
                StrSql = StrSql + " AND MATERIALID IN (" + MatId + ") "
                If PackId <> "" Then
                    StrSql = StrSql + " AND PACKTYPEID IN (" + PackId + ")"
                End If
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotMaterials(ByVal MatId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT  MATID MATERIALID,  "
                StrSql = StrSql + " (MATERIALS.MATDE1||' '||MATERIALS.MATDE2)VALUE "
                StrSql = StrSql + " FROM MATERIALS "
                StrSql = StrSql + " WHERE MATID IN (" + MatId + ")"
                StrSql = StrSql + " ORDER BY MATDE1 "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotProdPackage(ByVal FactId As String, ByVal PackId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PACKAGETYPE.PACKAGETYPEID AS PACKAGETYPEID,FACTPACKTYPEPACKSIZE.FACTID ID,PC.ID CATEGORYID, "
                StrSql = StrSql + "PACKAGETYPEDE1 DES1, "
                StrSql = StrSql + "PACKAGETYPEDE2 DES2, "
                StrSql = StrSql + "(PACKAGETYPE.PACKAGETYPEDE1||' '||PACKAGETYPE.PACKAGETYPEDE2)VALUE "
                StrSql = StrSql + "FROM FACTPACKTYPEPACKSIZE "
                StrSql = StrSql + "INNER JOIN PACKAGETYPE "
                StrSql = StrSql + "ON PACKAGETYPE.PACKAGETYPEID=FACTPACKTYPEPACKSIZE.PACKAGETYPEID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATEGORIES PC ON PC.FACTID=FACTPACKTYPEPACKSIZE.FACTID "
                StrSql = StrSql + "WHERE FACTPACKTYPEPACKSIZE.FACTID IN (" + FactId.ToString() + ") "
                If PackId <> "" Then
                    StrSql = StrSql + "AND PACKAGETYPE.PACKAGETYPEID IN (" + PackId + ")"
                End If
		StrSql = StrSql + "ORDER BY VALUE ASC"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPivotMatPackages(ByVal Id As String, ByVal PackId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PACKTYPEID PACKAGETYPEID, PACKTYPEMATERIAL.GROUPID, MATERIALID, "
                StrSql = StrSql + "(PACKAGETYPE.PACKAGETYPEDE1||' '||PACKAGETYPE.PACKAGETYPEDE2)VALUE "
                StrSql = StrSql + " FROM PACKTYPEMATERIAL "
                StrSql = StrSql + "INNER JOIN PACKAGETYPE "
                StrSql = StrSql + "ON PACKAGETYPE.PACKAGETYPEID=PACKTYPEMATERIAL.PACKTYPEID "
                StrSql = StrSql + " WHERE MATERIALID IN (" + Id + ") AND PACKTYPEID IN (" + PackId + ")"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotPackProdMaterials(ByVal ProdId As String, ByVal PackId As String, ByVal MatId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT MATERIALID, GROUPID, PC.ID CATEGORYID, PACKTYPEID PACKAGETYPEID, "
                StrSql = StrSql + " (MATERIALS.MATDE1||' '||MATERIALS.MATDE2)VALUE "
                StrSql = StrSql + " FROM PACKTYPEMATERIAL "
                StrSql = StrSql + "INNER JOIN MATERIALS ON MATERIALS.MATID=PACKTYPEMATERIAL.MATERIALID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATEGORIES PC ON PC.FACTID=PACKTYPEMATERIAL.PRODID "
                StrSql = StrSql + " WHERE PRODID IN (" + ProdId + ")"
                StrSql = StrSql + " AND PACKTYPEID IN (" + PackId + ")"
                If MatId <> "" Then
                    StrSql = StrSql + " AND MATERIALID IN (" + MatId + ")"
                End If

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotProdMaterials(ByVal Id As String, ByVal MatId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT MATERIALID, GROUPID, PC.ID CATEGORYID, "
                StrSql = StrSql + " (MATERIALS.MATDE1||' '||MATERIALS.MATDE2)VALUE "
                StrSql = StrSql + " FROM PACKTYPEMATERIAL "
                StrSql = StrSql + "INNER JOIN MATERIALS ON MATERIALS.MATID=PACKTYPEMATERIAL.MATERIALID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATEGORIES PC ON PC.FACTID=PACKTYPEMATERIAL.PRODID "
                StrSql = StrSql + " WHERE PRODID IN (" + Id + ")"
                If MatId <> "" Then
                    StrSql = StrSql + " AND MATERIALID IN (" + MatId + ")"
                End If

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotPackMaterials_OLD(ByVal Id As String, ByVal MatId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT MATERIALID , GROUPID, PACKTYPEID PACKAGETYPEID, "
                StrSql = StrSql + " (MATERIALS.MATDE1||' '||MATERIALS.MATDE2)VALUE "
                StrSql = StrSql + " FROM PACKTYPEMATERIAL "
                StrSql = StrSql + "INNER JOIN MATERIALS ON MATERIALS.MATID=PACKTYPEMATERIAL.MATERIALID "
                StrSql = StrSql + " WHERE PACKTYPEID IN (" + Id + ") "
                If MatId <> "" Then
                    StrSql = StrSql + " AND MATERIALID IN (" + MatId + ") "
                End If

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotPackMatProducts(ByVal ProdId As String, ByVal PackId As String, ByVal MatId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PRODID ID, PC.ID CATEGORYID, GROUPID, MATERIALID , PACKTYPEID PACKAGETYPEID, "
                StrSql = StrSql + "PC.CATEGORYNAME VALUE "
                StrSql = StrSql + "FROM PACKTYPEMATERIAL "
                StrSql = StrSql + "INNER JOIN MATERIALS ON MATERIALS.MATID=PACKTYPEMATERIAL.MATERIALID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATEGORIES PC ON PC.FACTID = PACKTYPEMATERIAL.PRODID "
                StrSql = StrSql + " WHERE MATERIALID IN (" + MatId + ")"
                StrSql = StrSql + " AND PACKTYPEID IN (" + PackId + ")"
                If ProdId <> "" Then
                    StrSql = StrSql + " AND PRODID IN (" + ProdId + ")"
                End If
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotMatProducts(ByVal MatId As String, ByVal ProdId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PC.ID CATEGORYID,PRODID ID, MATERIALID, GROUPID,"
                StrSql = StrSql + " TYPE VALUE "
                StrSql = StrSql + " FROM PACKTYPEMATERIAL "
                StrSql = StrSql + "INNER JOIN FACT ON FACT.ID=PACKTYPEMATERIAL.PRODID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATEGORIES PC ON PC.FACTID = FACT.ID "
                StrSql = StrSql + " WHERE MATERIALID IN (" + MatId + ") "
                If ProdId <> "" Then
                    StrSql = StrSql + " AND PRODID IN (" + ProdId + ")"
                End If

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotPackProducts(ByVal FactId As String, ByVal PackId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT FACTPACKTYPEPACKSIZE.FACTID ID,PC.ID CATEGORYID, FACTPACKTYPEPACKSIZE.PACKAGETYPEID AS PACKAGETYPEID, "
                StrSql = StrSql + "TYPE VALUE "
                StrSql = StrSql + "FROM FACTPACKTYPEPACKSIZE "
                StrSql = StrSql + "INNER JOIN FACT "
                StrSql = StrSql + "ON FACT.ID=FACTPACKTYPEPACKSIZE.FACTID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATEGORIES PC "
                StrSql = StrSql + "ON PC.FACTID=FACT.ID "
                StrSql = StrSql + "WHERE FACTPACKTYPEPACKSIZE.PACKAGETYPEID IN (" + PackId + ") "
                If FactId <> "" Then
                    StrSql = StrSql + "AND FACTPACKTYPEPACKSIZE.FACTID IN (" + FactId.ToString() + ") "
                End If
                StrSql = StrSql + "ORDER BY FACTPACKTYPEPACKSIZE.FACTID "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPivotPackages(ByVal PackId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PACKAGETYPE.PACKAGETYPEID, "
                StrSql = StrSql + "PACKAGETYPEDE1 DES1, "
                StrSql = StrSql + "PACKAGETYPEDE2 DES2, "
                StrSql = StrSql + "(PACKAGETYPE.PACKAGETYPEDE1||' '||PACKAGETYPE.PACKAGETYPEDE2)VALUE "
                StrSql = StrSql + "FROM FACTPACKTYPEPACKSIZE "
                StrSql = StrSql + "INNER JOIN PACKAGETYPE "
                StrSql = StrSql + "ON PACKAGETYPE.PACKAGETYPEID=FACTPACKTYPEPACKSIZE.PACKAGETYPEID "
                StrSql = StrSql + "WHERE FACTPACKTYPEPACKSIZE.PACKAGETYPEID IN (" + PackId.ToString() + ")"

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function



        Public Function GetPivotPackages(ByVal ProdId As String, ByVal MatId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PACKTYPEID PACKAGETYPEID, "
                StrSql = StrSql + " (PACKAGETYPE.PACKAGETYPEDE1||' '||PACKAGETYPE.PACKAGETYPEDE2)VALUE "
                StrSql = StrSql + " FROM PACKTYPEMATERIAL "
                StrSql = StrSql + "INNER JOIN PACKAGETYPE ON PACKAGETYPE.PACKAGETYPEID=PACKTYPEMATERIAL.PACKTYPEID "
                If ProdId <> "" And MatId <> "" Then
                    StrSql = StrSql + " WHERE PRODID IN (" + ProdId + ") "
                    StrSql = StrSql + " AND MATERIALID IN (" + MatId + ") "
                ElseIf ProdId <> "" Then
                    StrSql = StrSql + " WHERE PRODID IN (" + ProdId + ") "
                ElseIf MatId <> "" Then
                    StrSql = StrSql + " WHERE MATERIALID IN (" + MatId + ") "
                End If

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotPackages:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotPackMaterials(ByVal ProdId As String, ByVal PackId As String, ByVal MatId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT MATERIALID, PACKTYPEID PACKAGETYPEID, "
                StrSql = StrSql + " (MATERIALS.MATDE1||' '||MATERIALS.MATDE2)VALUE "
                StrSql = StrSql + " FROM PACKTYPEMATERIAL "
                StrSql = StrSql + "INNER JOIN MATERIALS ON MATERIALS.MATID=PACKTYPEMATERIAL.MATERIALID "
                StrSql = StrSql + " WHERE MATERIALID IN (" + MatId + ") "
                StrSql = StrSql + " AND PACKTYPEID IN (" + PackId + ") "
                If ProdId <> "" Then
                    StrSql = StrSql + " AND PRODID IN (" + ProdId + ")"
                End If
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function


        Public Function GetPivotProducts(ByVal ProdId As String, ByVal PackId As String, ByVal MatId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PC.ID CATEGORYID,TYPE VALUE "
                StrSql = StrSql + " FROM PACKTYPEMATERIAL "
                StrSql = StrSql + "INNER JOIN FACT ON FACT.ID=PACKTYPEMATERIAL.PRODID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATEGORIES PC ON PC.FACTID=FACT.ID "
                If PackId <> "" And MatId <> "" Then
                    StrSql = StrSql + "INNER JOIN MATERIALS ON MATERIALS.MATID=PACKTYPEMATERIAL.MATERIALID "
                    StrSql = StrSql + "WHERE MATERIALID IN (" + MatId + ") "
                    StrSql = StrSql + "AND PACKTYPEID IN (" + PackId + ") "
                    StrSql = StrSql + "AND PRODID IN (" + ProdId + ") "
                ElseIf PackId <> "" Then
                    StrSql = StrSql + "WHERE PACKTYPEID IN (" + PackId + ") "
                    StrSql = StrSql + "AND PRODID IN (" + ProdId + ") "
                ElseIf MatId <> "" Then
                    StrSql = StrSql + "INNER JOIN MATERIALS ON MATERIALS.MATID=PACKTYPEMATERIAL.MATERIALID "
                    StrSql = StrSql + "WHERE MATERIALID IN (" + MatId + ") "
                    StrSql = StrSql + "AND PRODID IN (" + ProdId + ") "
                Else
                    StrSql = StrSql + "WHERE PRODID IN (" + ProdId + ") "
                End If
                StrSql = StrSql + "ORDER BY CATEGORYID "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotGroups(ByVal GroupId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PRODGROUPID SUBGROUPID,  "
                StrSql = StrSql + "NAME VALUE, "
                StrSql = StrSql + "PARENTID "
                StrSql = StrSql + "FROM PRODUCTGROUP "
                StrSql = StrSql + "WHERE PRODGROUPID IN (" + GroupId + ")"
                StrSql = StrSql + " ORDER BY NAME"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetReportRegionsByRegionSet:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotGroupList(ByVal ParentId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PRODGROUPID SUBGROUPID,  "
                StrSql = StrSql + "NAME VALUE, "
                StrSql = StrSql + "PARENTID "
                StrSql = StrSql + "FROM PRODUCTGROUP "
                StrSql = StrSql + "WHERE PARENTID IN (" + ParentId + ")"
                StrSql = StrSql + " ORDER BY NAME"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetReportRegionsByRegionSet:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotMaterials(ByVal ProdId As String, ByVal PackId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT MATERIALID,(MATDE1 ||' '|| MATDE2) VALUE "
                StrSql = StrSql + " FROM PACKTYPEMATERIAL PTM "
                StrSql = StrSql + " INNER JOIN MATERIALS MAT ON MAT.MATID= PTM.MATERIALID"
                If ProdId <> "" And PackId <> "" Then
                    StrSql = StrSql + " WHERE PRODID IN (" + ProdId + ")"
                    StrSql = StrSql + " AND PACKTYPEID IN (" + PackId + ")"
                ElseIf ProdId <> "" Then
                    StrSql = StrSql + " WHERE PRODID IN (" + ProdId + ")"
                ElseIf PackId <> "" Then
                    StrSql = StrSql + " WHERE PACKTYPEID IN (" + PackId + ")"
                End If

                StrSql = StrSql + " ORDER BY VALUE ASC"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotMaterials:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

	Public Function GetPivotMatAllPackages(ByVal ProdId As String, ByVal MatId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT MATERIALID, PACKTYPEMATERIAL.GROUPID, PACKTYPEID PACKAGETYPEID, "
                StrSql = StrSql + "(PACKAGETYPE.PACKAGETYPEDE1||' '||PACKAGETYPE.PACKAGETYPEDE2)VALUE "
                StrSql = StrSql + " FROM PACKTYPEMATERIAL "
                StrSql = StrSql + "INNER JOIN MATERIALS ON MATERIALS.MATID=PACKTYPEMATERIAL.MATERIALID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATEGORIES PC ON PC.FACTID=PACKTYPEMATERIAL.PRODID "
                StrSql = StrSql + "INNER JOIN PACKAGETYPE ON PACKAGETYPE.PACKAGETYPEID=PACKTYPEMATERIAL.PACKTYPEID "
                StrSql = StrSql + " WHERE PRODID IN (" + ProdId + ")"
                If MatId <> "" Then
                    StrSql = StrSql + " AND MATERIALID IN (" + MatId + ")"
                End If

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

	Public Function GetPackages(ByVal FactId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PACKAGETYPE.PACKAGETYPEID AS PACKAGETYPEID,"
                StrSql = StrSql + "PACKAGETYPEDE1 DES1, "
                StrSql = StrSql + "PACKAGETYPEDE2 DES2, "
                StrSql = StrSql + "(PACKAGETYPE.PACKAGETYPEDE1||' '||PACKAGETYPE.PACKAGETYPEDE2)VALUE "
                StrSql = StrSql + "FROM FACTPACKTYPEPACKSIZE "
                StrSql = StrSql + "INNER JOIN PACKAGETYPE "
                StrSql = StrSql + "ON PACKAGETYPE.PACKAGETYPEID=FACTPACKTYPEPACKSIZE.PACKAGETYPEID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATEGORIES PC ON PC.FACTID=FACTPACKTYPEPACKSIZE.FACTID "
                StrSql = StrSql + "WHERE FACTPACKTYPEPACKSIZE.FACTID IN (" + FactId.ToString() + ") "
                StrSql = StrSql + "ORDER BY PACKAGETYPEID "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

#Region "Mixed Report"
        Public Function GetMixedReportData(ByVal TblName() As String, ByVal YearId As String, ByVal UnitId As String, ByVal Type As String, ByVal Val As String, ByVal Cond As String, ByVal Count As Integer, ByVal ProdId As String, ByVal IsMat As Boolean) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim colStr As String = ""
            Dim grpStr As String = ""
            Try
                If Type = "PRODUCT" Then
                    colStr = "YEARID,UNITID," + Val + " PRODUCTID,PACKAGETYPEID,MATERIALID,REGIONID,COUNTRYID,F.SUBGROUPID "
                    grpStr = "YEARID,UNITID,PRODUCTID,PACKAGETYPEID,MATERIALID,REGIONID,COUNTRYID,SUBGROUPID "
                ElseIf Type = "REGION" Then
                    If Count = 1 Then
                        colStr = "YEARID,UNITID," + ProdId + " PRODUCTID,PACKAGETYPEID,MATERIALID," + Val + " REGIONID,COUNTRYID,F.SUBGROUPID "
                        grpStr = "YEARID,UNITID,PRODUCTID,PACKAGETYPEID,MATERIALID,REGIONID,COUNTRYID,SUBGROUPID "
                    Else
                        colStr = "YEARID,UNITID,0 PRODUCTID,PACKAGETYPEID,MATERIALID," + Val + " REGIONID,COUNTRYID,F.SUBGROUPID "
                        grpStr = "YEARID,UNITID,PRODUCTID,PACKAGETYPEID,MATERIALID,REGIONID,COUNTRYID,SUBGROUPID "
                    End If
                Else
                    If Count = 1 Then
                        colStr = "YEARID,UNITID," + ProdId + " PRODUCTID,PACKAGETYPEID,MATERIALID,REGIONID,COUNTRYID,F.SUBGROUPID "
                        grpStr = "YEARID,UNITID,PRODUCTID,PACKAGETYPEID,MATERIALID,REGIONID,COUNTRYID,SUBGROUPID "
                    Else
                        colStr = "YEARID,UNITID,0 PRODUCTID,PACKAGETYPEID,MATERIALID,REGIONID,COUNTRYID,F.SUBGROUPID "
                        grpStr = "YEARID,UNITID,PRODUCTID,PACKAGETYPEID,MATERIALID,REGIONID,COUNTRYID,SUBGROUPID "
                    End If
                End If

                For a = 0 To Count - 1
                    If TblName(a) <> "" Then
                        If a <> 0 Then
                            StrSql = StrSql + " UNION "
                        End If
                        StrSql = StrSql + "SELECT SUM(FACT) FCT,YEARID,UNITID,PRODUCTID,PACKAGETYPEID,MATERIALID,REGIONID,COUNTRYID,SUBGROUPID  FROM ( "
                        StrSql = StrSql + "SELECT FACT," + colStr + " "
                        StrSql = StrSql + "FROM " + TblName(a) + " F WHERE 1=1 "
                        If Type = "REGION" Then
                            StrSql = StrSql + "AND COUNTRYID IN (SELECT COUNTRYID FROM USERREGIONCOUNTRIES WHERE REGIONID=" + Val + ") "
                        ElseIf Type = "COUNTRY" Then
                            StrSql = StrSql + "AND COUNTRYID IN (" + Val + ") "
                        Else
                            StrSql = StrSql + "AND COUNTRYID IS NOT NULL "
                        End If
                        If Type = "MATERIAL" Then
                            StrSql = StrSql + "AND MATERIALID=" + Val + " "
                        Else
                            If UnitId = "1" Then
                                If Type = "MATERIAL" Or IsMat Then
                                    StrSql = StrSql + "AND MATERIALID IS NOT NULL "
                                Else
                                    StrSql = StrSql + "AND MATERIALID IS NULL "
                                End If
                            Else
                                StrSql = StrSql + "AND NVL(MATERIALID,-1)=-1 "
                            End If
                        End If
                        StrSql = StrSql + ") A WHERE 1=1  AND " + Cond + " AND UNITID IN (" + UnitId + ") AND YEARID IN (" + YearId + ") "
                        StrSql = StrSql + "GROUP BY " + grpStr + ""
                    End If
                Next
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotReportData:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetMixedReportData_Popl(ByVal YearId As String, ByVal UnitId As String, ByVal Type As String, ByVal Val As String, ByVal Cond As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim colStr As String = ""
            Dim grpStr As String = ""
            Try
                If Type = "METRIC" Then
                    colStr = "YEARID,UNITID," + Val + " PRODUCTID,PACKAGETYPEID,MATERIALID, REGIONID, COUNTRYID,F.SUBGROUPID "
                    grpStr = "YEARID,UNITID,PRODUCTID,PACKAGETYPEID,MATERIALID,REGIONID,COUNTRYID,SUBGROUPID "
                End If

                StrSql = StrSql + "SELECT SUM(FACT) FCT,YEARID,UNITID,PRODUCTID,PACKAGETYPEID,MATERIALID, REGIONID, COUNTRYID,SUBGROUPID  FROM ( "
                StrSql = StrSql + "SELECT FACT," + colStr + " "
                StrSql = StrSql + "FROM FCTPOPULATIONS F WHERE 1=1 ) A "
                StrSql = StrSql + "WHERE 1=1 AND UNITID in (" + UnitId + ") AND YEARID in (" + YearId + ") "
                If Cond <> "" Then
                    StrSql = StrSql + "AND " + Cond + " "
                End If
                StrSql = StrSql + "GROUP BY " + grpStr + ""

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotReportData:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetMixedReportData_Gdp(ByVal YearId As String, ByVal UnitId As String, ByVal Type As String, ByVal Val As String, ByVal Cond As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim colStr As String = ""
            Dim grpStr As String = ""
            Try
                If Type = "METRIC" Then
                    colStr = "YEARID,UNITID," + Val + " PRODUCTID,PACKAGETYPEID,MATERIALID,REGIONID,COUNTRYID,F.SUBGROUPID "
                    grpStr = "YEARID,UNITID,PRODUCTID,PACKAGETYPEID,MATERIALID,REGIONID,COUNTRYID,SUBGROUPID "
                End If

                StrSql = StrSql + "SELECT SUM(FACT) FCT,YEARID,UNITID,PRODUCTID,PACKAGETYPEID,MATERIALID,NULL REGIONID,NULL COUNTRYID,SUBGROUPID FROM ( "
                StrSql = StrSql + "SELECT FACT," + colStr + " "
                StrSql = StrSql + "FROM FCTGDP F WHERE 1=1 ) A "
                StrSql = StrSql + "WHERE 1=1 AND UNITID in (" + UnitId + ") AND YEARID in (" + YearId + ") "
                If Cond <> "" Then
                    StrSql = StrSql + "AND " + Cond + " "
                End If
                StrSql = StrSql + "GROUP BY " + grpStr + ""

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotReportData:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUserReportRowFilter(ByVal ReportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT U.ROWVALUE AS VALUE,U.USERREPORTID AS CLUASE1, "
                StrSql = StrSql + "ROWVALUEID AS VALUEID FROM USERREPORTROWS U "
                StrSql = StrSql + "WHERE USERREPORTID= " + ReportId + " AND ROWVALUE NOT IN (' ','Mixed Row') "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUserReportRowFilter:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUserReportRowTFilter(ByVal ReportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT U.ROWVALUE AS VALUE,U.USERREPORTID AS CLUASE1, "
                StrSql = StrSql + "ROWVALUEID AS VALUEID FROM USERREPORTROWSTEMP U "
                StrSql = StrSql + "WHERE USERREPORTID= " + ReportId + " AND ROWVALUE NOT IN (' ','Mixed Row') "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUserReportRowTFilter:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCapitaUnits(ByVal UnitId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                StrSql = "SELECT DISTINCT UNITS.UNITID ID, "
                StrSql = StrSql + "UNITDES VALUE, "
                StrSql = StrSql + "UNITSHRT "
                StrSql = StrSql + "FROM UNITS "
                StrSql = StrSql + "WHERE UNITID IN (" + UnitId + ") "
                StrSql = StrSql + " ORDER BY UNITDES "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetCapitaUnits:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

	Public Function GetMixedFilterSelector(ByVal filterTypeId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                StrSql = "SELECT FILTERTYPEID,   "
                StrSql = StrSql + "FILTERDES,  "
                StrSql = StrSql + "FILTERVALUE,  "
                StrSql = StrSql + "FILTERCODE,  "
                StrSql = StrSql + "FILTERSQL  "
                StrSql = StrSql + "FROM FILTERTYPE  "
                StrSql = StrSql + "WHERE FILTERTYPEID NOT IN "
                StrSql = StrSql + "(7,9) "
                StrSql = StrSql + "AND FILTERTYPEID=CASE WHEN  " + filterTypeId + " =-1 THEN FILTERTYPEID ELSE " + filterTypeId + " END "
                StrSql = StrSql + "ORDER BY FILTERDES  "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetFilterSelector:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetMixedRowsSelector(ByVal rowTypeId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                StrSql = "SELECT ROWTYPEID,  "
                StrSql = StrSql + "ROWDES, "
                StrSql = StrSql + "ROWVALUE, "
                StrSql = StrSql + "ROWCODE, "
                StrSql = StrSql + "ROWSQL "
                StrSql = StrSql + "FROM ROWTYPE "
                StrSql = StrSql + "WHERE ROWTYPEID NOT IN (2,6) "
                StrSql = StrSql + "ORDER BY ROWDES "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetRowsSelector:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUserReportRowFilterUnit(ByVal ReportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT U.USERREPORTROWID AS ID,U.USERREPORTID AS CLUASE1, U.ROWSEQUENCE AS CLUASE2, "
                StrSql = StrSql + "U.ROWVALUE AS VALUE,ROWVALUEID AS VALUEID,UNITID FROM USERREPORTROWS U "
                StrSql = StrSql + "WHERE USERREPORTID= " + ReportId + " ORDER BY U.ROWSEQUENCE"

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUserReportRowFilter:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUserReportRowTFilterUnit(ByVal ReportId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT U.USERREPORTROWID AS ID,U.USERREPORTID AS CLUASE1, U.ROWSEQUENCE AS CLUASE2, "
                StrSql = StrSql + "U.ROWVALUE AS VALUE,ROWVALUEID AS VALUEID,UNITID FROM USERREPORTROWSTEMP U "
                StrSql = StrSql + "WHERE USERREPORTID= " + ReportId + " ORDER BY U.ROWSEQUENCE"

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetUserReportRowTFilter:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

	Public Function GetReportComponentByProdPack(ByVal type As ArrayList, ByVal Id As ArrayList, ByVal FactId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                Dim strPackId = GetPackId(type, Id)
                Dim strProdId = GetProdId(type, Id, FactId)

                If strProdId = "" Then
                    Dim arrFact() As String
                    Dim dsFact As New DataSet
                    Dim catId As String = String.Empty
                    arrFact = Regex.Split(HttpContext.Current.Session("M1SubGroupId").ToString(), ",")
                    For j = 0 To arrFact.Length - 1
                        dsFact = GetSubGroupDetails(arrFact(j))
                        If dsFact.Tables(0).Rows(0).Item("CATID").ToString() <> "" Then
                            If j = 0 Then
                                catId = dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            Else
                                catId = catId + dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            End If

                        End If
                    Next
                    catId = catId.Remove(catId.Length - 1)

                    'getting productID
                    dsFact = GetSubFactGroupDetails(catId)
                    FactId = String.Empty
                    For k = 0 To dsFact.Tables(0).Rows.Count - 1
                        If dsFact.Tables(0).Rows(0).Item("ID").ToString() <> "" Then
                            If k = 0 Then
                                FactId = dsFact.Tables(0).Rows(k).Item("ID").ToString()
                            Else
                                FactId = FactId + "," + dsFact.Tables(0).Rows(k).Item("ID").ToString()
                            End If
                        End If
                    Next
                    strProdId = FactId
                End If

                If strPackId = "0" Then
                    Dim dsPack As New DataSet
                    Dim PackId As String = String.Empty
                    dsPack = GetPivotProdPackage(strProdId, "")
                    For b = 0 To dsPack.Tables(0).Rows.Count - 1
                        PackId = PackId + "" + dsPack.Tables(0).Rows(b).Item("PACKAGETYPEID").ToString() + ","
                    Next
                    PackId = PackId.Remove(PackId.Length - 1)
                    strPackId = PackId
                End If

                StrSql = "SELECT DISTINCT COMP ID, "
                StrSql = StrSql + "VALUE NAME "
                StrSql = StrSql + "FROM COMPONENTS "
                StrSql = StrSql + "INNER JOIN PRODPACKCOMPONENTS P "
                StrSql = StrSql + "ON P.COMPID = COMPONENTS.COMPID "

                If strPackId <> String.Empty And strProdId <> String.Empty Then
                    StrSql = StrSql + "WHERE P.FACTID IN(" + strProdId + ") AND P.PACKID IN(" + strPackId + ")"
                Else
                    If strProdId <> String.Empty Then
                        StrSql = StrSql + "WHERE P.FACTID IN(" + strProdId + ") "
                    ElseIf strPackId <> String.Empty Then
                        StrSql = StrSql + "WHERE P.PACKID IN(" + strPackId + ") "
                    End If

                    If strProdId = String.Empty Then
                        StrSql = "SELECT DISTINCT COMPID AS ID, "
                        StrSql = StrSql + "VALUE NAME "
                        StrSql = StrSql + "FROM COMPONENTS "
                    End If
                End If

                StrSql = StrSql + " ORDER BY NAME"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1GetData:GetFilterSQL:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Component Pivot Report"
        Public Function GetComponents(ByVal FactId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PPC.COMPID COMPONENTID,COMPDES,FACTID FROM PRODPACKCOMPONENTS PPC "
                StrSql = StrSql + "INNER JOIN COMPONENTS COMP ON COMP.COMPID=PPC.COMPID "
                StrSql = StrSql + "WHERE PPC.FACTID IN (" + FactId.ToString() + ") "
                StrSql = StrSql + "ORDER BY COMPDES ASC "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPackComponents(ByVal PackId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT PPC.COMPID ID,PACKID FROM PRODPACKCOMPONENTS PPC "
                StrSql = StrSql + "WHERE PPC.PACKID IN (" + PackId.ToString() + ") "
                StrSql = StrSql + "ORDER BY ID ASC "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPivotProdComponent(ByVal FactId As String, ByVal PackId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PRODPACKCOMPONENTS.COMPID COMPONENTID, PACKAGETYPE.PACKAGETYPEID AS PACKAGETYPEID,PRODPACKCOMPONENTS.FACTID ID,PC.ID CATEGORYID, "
                StrSql = StrSql + "COMPDES VALUE "
                StrSql = StrSql + "FROM PRODPACKCOMPONENTS "
                StrSql = StrSql + "INNER JOIN PACKAGETYPE "
                StrSql = StrSql + "ON PACKAGETYPE.PACKAGETYPEID=PRODPACKCOMPONENTS.PACKID "
                StrSql = StrSql + "INNER JOIN COMPONENTS COMP "
                StrSql = StrSql + "ON COMP.COMPID=PRODPACKCOMPONENTS.COMPID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATEGORIES PC ON PC.FACTID=PRODPACKCOMPONENTS.FACTID "
                StrSql = StrSql + "WHERE PRODPACKCOMPONENTS.FACTID IN (" + FactId.ToString() + ") "
                If PackId <> "" Then
                    StrSql = StrSql + "AND PACKAGETYPE.PACKAGETYPEID IN (" + PackId + ")"
                End If
                StrSql = StrSql + "ORDER BY VALUE ASC"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPivotComponent(ByVal CompId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PRODPACKCOMPONENTS.COMPID COMPONENTID, "
                StrSql = StrSql + "COMPDES VALUE "
                StrSql = StrSql + "FROM PRODPACKCOMPONENTS "
                StrSql = StrSql + "INNER JOIN COMPONENTS COMP "
                StrSql = StrSql + "ON COMP.COMPID=PRODPACKCOMPONENTS.COMPID "
                StrSql = StrSql + "WHERE PRODPACKCOMPONENTS.COMPID IN (" + CompId.ToString() + ")"

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetProdPackComponent(ByVal FactId As String, ByVal PackId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PRODPACKCOMPONENTS.COMPID ID, FACTID, PACKID "
                StrSql = StrSql + "FROM PRODPACKCOMPONENTS "
                StrSql = StrSql + "WHERE PRODPACKCOMPONENTS.FACTID IN (" + FactId.ToString() + ") "
                StrSql = StrSql + "AND PRODPACKCOMPONENTS.PACKID IN (" + PackId + ")"
                StrSql = StrSql + "ORDER BY ID ASC"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetComponent(ByVal CompId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PRODPACKCOMPONENTS.COMPID ID, "
                StrSql = StrSql + "COMPDES VALUE "
                StrSql = StrSql + "FROM PRODPACKCOMPONENTS "
                StrSql = StrSql + "INNER JOIN COMPONENTS ON COMPONENTS.COMPID=PRODPACKCOMPONENTS.COMPID "
                StrSql = StrSql + "WHERE PRODPACKCOMPONENTS.COMPID IN (" + CompId.ToString() + ") "
                StrSql = StrSql + "ORDER BY ID ASC"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetPivotComponents(ByVal ProdId As String, ByVal PackId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT COMP.COMPID COMPONENTID,COMPDES VALUE "
                StrSql = StrSql + " FROM COMPONENTS COMP "
                StrSql = StrSql + " INNER JOIN PRODPACKCOMPONENTS PPC ON PPC.COMPID= COMP.COMPID "
                If ProdId <> "" And PackId <> "" Then
                    StrSql = StrSql + " WHERE FACTID IN (" + ProdId + ")"
                    StrSql = StrSql + " AND PACKID IN (" + PackId + ")"
                ElseIf ProdId <> "" Then
                    StrSql = StrSql + " WHERE FACTID IN (" + ProdId + ")"
                ElseIf PackId <> "" Then
                    StrSql = StrSql + " WHERE PACKID IN (" + PackId + ")"
                End If

                StrSql = StrSql + " ORDER BY VALUE ASC"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotMaterials:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotReportData_COMP(ByVal TblName() As String, ByVal YearId As String, ByVal ProdId As String, ByVal PackId As String, ByVal GrpId As String, ByVal CountryId As String, ByVal RegionId As String, ByVal UnitId As String, ByVal Count As Integer) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim colStr As String = ""
            Dim grpStr As String = ""
            Try
                If ProdId <> "" Then
                    colStr = colStr + "CATEGORYID,"
                End If
                If PackId <> "" Then
                    colStr = colStr + "PACKAGETYPEID,"
                End If
                If GrpId <> "" Then
                    colStr = colStr + "SUBGROUPID,"
                End If
                If CountryId <> "" Then
                    colStr = colStr + "COUNTRYID,"
                End If
                If colStr <> "" Then
                    colStr = colStr.Remove(colStr.Length - 1)
                End If

                For a = 0 To Count - 1
                    If TblName(a) <> "" Then
                        If a <> 0 Then
                            StrSql = StrSql + " UNION "
                        End If
                        StrSql = StrSql + "SELECT SUM(FACT) FCT,COMPONENTID,YEARID "
                        If colStr <> "" Then
                            StrSql = StrSql + "," + colStr + " "
                        End If
                        StrSql = StrSql + "FROM " + TblName(a) + " FCT "
                        StrSql = StrSql + "WHERE UNITID = " + UnitId + " AND YEARID IN (" + YearId + ") "
                        If UnitId <> "1" Then
                            StrSql = StrSql + "AND NVL(COMPONENTID,-1)<>-1"
                        Else
                            StrSql = StrSql + "AND NVL(COMPONENTID,-1)<>-1 "
                        End If
                        If PackId <> "" Then
                            StrSql = StrSql + "AND PACKAGETYPEID IN (" + PackId + ") "
                        End If
                        If GrpId <> "" Then
                            StrSql = StrSql + "AND SUBGROUPID IN (" + GrpId + ") "
			
                        End If
                        If CountryId <> "" Then
                            StrSql = StrSql + "AND COUNTRYID IN (" + CountryId + ") "
                        End If
                        If RegionId <> "" Then
                            StrSql = StrSql + "AND COUNTRYID IN (SELECT COUNTRYID FROM USERREGIONCOUNTRIES WHERE REGIONID IN(" + RegionId + ")) "
                        Else
                            StrSql = StrSql + "AND COUNTRYID IS NOT NULL "
                        End If
                        StrSql = StrSql + "GROUP BY COMPONENTID,YEARID "
                        If colStr <> "" Then
                            StrSql = StrSql + "," + colStr + " "
                        End If
                    End If
                Next
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)

                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotReportData:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotPackProdComponents(ByVal ProdId As String, ByVal PackId As String, ByVal CompId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PPC.COMPID COMPONENTID, COMPDES VALUE, PC.FACTID, PC.ID CATEGORYID, PACKID PACKAGETYPEID "
                StrSql = StrSql + "FROM COMPONENTS COMP "
                StrSql = StrSql + "INNER JOIN PRODPACKCOMPONENTS PPC ON PPC.COMPID=COMP.COMPID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATEGORIES PC ON PC.FACTID=PPC.FACTID "
                StrSql = StrSql + " WHERE PPC.FACTID IN (" + ProdId + ")"
                StrSql = StrSql + " AND PPC.PACKID IN (" + PackId + ")"
                If CompId <> "" Then
                    StrSql = StrSql + " AND PPC.COMPID IN (" + CompId + ")"
                End If

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotPackProdComponents:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotProdComponents(ByVal ProdId As String, ByVal CompId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PPC.COMPID COMPONENTID, COMPDES VALUE, PC.FACTID, PC.ID CATEGORYID "
                StrSql = StrSql + "FROM COMPONENTS COMP "
                StrSql = StrSql + "INNER JOIN PRODPACKCOMPONENTS PPC ON PPC.COMPID=COMP.COMPID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATEGORIES PC ON PC.FACTID=PPC.FACTID "
                StrSql = StrSql + " WHERE PPC.FACTID IN (" + ProdId + ")"
                If CompId <> "" Then
                    StrSql = StrSql + " AND PPC.COMPID IN (" + CompId + ")"
                End If

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotProdComponents:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotPackComponents(ByVal ProdId As String, ByVal PackId As String, ByVal CompId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PPC.COMPID COMPONENTID, COMPDES VALUE, PACKID PACKAGETYPEID "
                StrSql = StrSql + "FROM COMPONENTS COMP "
                StrSql = StrSql + "INNER JOIN PRODPACKCOMPONENTS PPC ON PPC.COMPID=COMP.COMPID "
                StrSql = StrSql + " WHERE PPC.FACTID IN (" + ProdId + ")"
                StrSql = StrSql + " AND PPC.PACKID IN (" + PackId + ")"
                If CompId <> "" Then
                    StrSql = StrSql + " AND PPC.COMPID IN (" + CompId + ")"
                End If

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotPackComponents:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetRepComponent(ByVal CompId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT COMPID COMPONENTID, "
                StrSql = StrSql + "COMPDES VALUE "
                StrSql = StrSql + "FROM COMPONENTS "
                StrSql = StrSql + "WHERE COMPID IN (" + CompId.ToString() + ") "
                'StrSql = StrSql + "ORDER BY ID ASC"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetSubscProdCompPackages(ByVal ProdId As String, ByVal CompId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PACKID ID "
                StrSql = StrSql + " FROM PRODPACKCOMPONENTS "
                StrSql = StrSql + " WHERE COMPID IN (" + CompId + ")"
                If ProdId <> "" Then
                    StrSql = StrSql + " AND FACTID IN (" + ProdId + ")"
                End If

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetSubscPackCompProducts(ByVal PackId As String, ByVal CompId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT FACTID ID "
                StrSql = StrSql + " FROM PRODPACKCOMPONENTS "
                StrSql = StrSql + " WHERE COMPID IN (" + CompId + ")"
                If PackId <> "" Then
                    StrSql = StrSql + " AND PACKID IN (" + PackId + ")"
                End If
                StrSql = StrSql + "ORDER BY ID ASC "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetSubscPackCompProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetProdCompPackages(ByVal ProdId As String, ByVal CompId As String, ByVal PackId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PACKAGETYPEID, PC.FACTID, PC.ID CATEGORYID, COMPID COMPONENTID, "
                StrSql = StrSql + "(PACKAGETYPE.PACKAGETYPEDE1||' '||PACKAGETYPE.PACKAGETYPEDE2)VALUE "
                StrSql = StrSql + " FROM PRODPACKCOMPONENTS PPC "
                StrSql = StrSql + "INNER JOIN PACKAGETYPE "
                StrSql = StrSql + "ON PACKAGETYPE.PACKAGETYPEID=PPC.PACKID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATEGORIES PC ON PC.FACTID=PPC.FACTID "
                StrSql = StrSql + " WHERE PPC.COMPID IN (" + CompId + ") "
                StrSql = StrSql + " AND PPC.FACTID IN (" + ProdId + ") "
                If PackId <> "" Then
                    StrSql = StrSql + " AND PPC.PACKID IN (" + PackId + ")"
                End If

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotCompPackages(ByVal PackId As String, ByVal CompId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PACKAGETYPEID, COMPID COMPONENTID, "
                StrSql = StrSql + "(PACKAGETYPE.PACKAGETYPEDE1||' '||PACKAGETYPE.PACKAGETYPEDE2)VALUE "
                StrSql = StrSql + " FROM PRODPACKCOMPONENTS PPC "
                StrSql = StrSql + "INNER JOIN PACKAGETYPE "
                StrSql = StrSql + "ON PACKAGETYPE.PACKAGETYPEID=PPC.PACKID "
                StrSql = StrSql + " WHERE COMPID IN (" + CompId + ")"
                If PackId <> "" Then
                    StrSql = StrSql + " AND PPC.PACKID IN (" + PackId + ")"
                End If
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotCompAllPackages(ByVal ProdId As String, ByVal CompId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT COMPID COMPONENTID, PACKAGETYPEID, "
                StrSql = StrSql + "(PACKAGETYPE.PACKAGETYPEDE1||' '||PACKAGETYPE.PACKAGETYPEDE2)VALUE "
                StrSql = StrSql + " FROM PACKAGETYPE "
                StrSql = StrSql + "INNER JOIN PRODPACKCOMPONENTS PPC ON PPC.PACKID=PACKAGETYPE.PACKID "
                StrSql = StrSql + " WHERE PPC.FACTID IN (" + ProdId + ")"
                If CompId <> "" Then
                    StrSql = StrSql + " AND PPC.COMPID IN (" + CompId + ")"
                End If

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotCompAllPackages:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotPRODGRPPackages(ByVal ProdId As String, ByVal GrpID As String, ByVal PackID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT "
                StrSql = StrSql + "PRODUCTGROUP.PRODGROUPID SUBGROUPID,PRODUCTGROUP.NAME, "
                StrSql = StrSql + "PACKAGETYPE.PACKAGETYPEID, "
                StrSql = StrSql + "(PACKAGETYPE.PACKAGETYPEDE1||' '||PACKAGETYPE.PACKAGETYPEDE2)VALUE,PC.FACTID ID, PC.ID CATEGORYID "
                StrSql = StrSql + "FROM PACKAGETYPE "
                StrSql = StrSql + "INNER JOIN FACTPACKTYPEPACKSIZE PPC ON PPC.PACKAGETYPEID=PACKAGETYPE.PACKAGETYPEID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATGROUP PCG ON PCG.CATEGORYID=(SELECT CATEGORYID FROM PRODUCTCATEGORIES WHERE FACTID=PPC.FACTID) "
                StrSql = StrSql + "INNER JOIN PRODUCTGROUP ON PRODUCTGROUP.PARENTID=PCG.PRODUCTGROUPID "
                StrSql = StrSql + "INNER JOIN FACT ON FACT.ID=PPC.FACTID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATEGORIES PC ON PC.FACTID = PPC.FACTID "
                StrSql = StrSql + "WHERE PPC.FACTID IN (" + ProdId + ")  "
                If GrpID <> "" Then
                    StrSql = StrSql + " AND PRODUCTGROUP.PRODGROUPID IN (" + GrpID + ")"
                End If
                If PackID <> "" Then
                    StrSql = StrSql + " AND PACKAGETYPE.PACKAGETYPEID IN (" + PackID + ")"
                End If

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotCompAllPackages:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotGRPAllPackages(ByVal ProdId As String, ByVal GrpID As String, ByVal packID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT "
                StrSql = StrSql + "PRODUCTGROUP.PRODGROUPID SUBGROUPID,PRODUCTGROUP.NAME, "
                StrSql = StrSql + "PACKAGETYPE.PACKAGETYPEID, "
                StrSql = StrSql + "(PACKAGETYPE.PACKAGETYPEDE1||' '||PACKAGETYPE.PACKAGETYPEDE2)VALUE "
                StrSql = StrSql + "FROM PACKAGETYPE "
                StrSql = StrSql + "INNER JOIN FACTPACKTYPEPACKSIZE PPC ON PPC.PACKAGETYPEID=PACKAGETYPE.PACKAGETYPEID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATGROUP PCG ON PCG.CATEGORYID=(SELECT CATEGORYID FROM PRODUCTCATEGORIES WHERE FACTID=PPC.FACTID) "
                StrSql = StrSql + "INNER JOIN PRODUCTGROUP ON PRODUCTGROUP.PARENTID=PCG.PRODUCTGROUPID "
                StrSql = StrSql + "INNER JOIN FACT ON FACT.ID=PPC.FACTID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATEGORIES PC ON PC.FACTID = PPC.FACTID "
                StrSql = StrSql + "WHERE PPC.FACTID IN (" + ProdId + ")  "
                If GrpID <> "" Then
                    StrSql = StrSql + " AND PRODUCTGROUP.PRODGROUPID IN (" + GrpID + ")"
                End If
                If packID <> "" Then
                    StrSql = StrSql + " AND PACKAGETYPE.PACKAGETYPEID IN (" + packID + ")"
                End If


                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotCompAllPackages:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotPackCompProducts(ByVal ProdId As String, ByVal PackId As String, ByVal CompId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PC.FACTID ID, PC.ID CATEGORYID, PPC.COMPID COMPONENTID, PPC.PACKID PACKAGETYPEID, "
                StrSql = StrSql + "PC.CATEGORYNAME VALUE "
                StrSql = StrSql + "FROM PRODPACKCOMPONENTS PPC "
                StrSql = StrSql + "INNER JOIN FACT ON FACT.ID=PPC.FACTID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATEGORIES PC ON PC.FACTID = PPC.FACTID "
                StrSql = StrSql + " WHERE COMPID IN (" + CompId + ")"
                StrSql = StrSql + " AND PACKID IN (" + PackId + ")"
                If ProdId <> "" Then
                    StrSql = StrSql + " AND PPC.FACTID IN (" + ProdId + ")"
                End If
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotPackCompProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotPackGRPProducts(ByVal ProdId As String, ByVal PackId As String, ByVal GrpID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT "
                StrSql = StrSql + "PRODUCTGROUP.PRODGROUPID SUBGROUPID,PRODUCTGROUP.NAME, "
                StrSql = StrSql + "PC.FACTID ID, PC.ID CATEGORYID,PC.CATEGORYNAME VALUE,PPC.PACKAGETYPEID "
                StrSql = StrSql + "FROM FACTPACKTYPEPACKSIZE PPC "
                StrSql = StrSql + "INNER JOIN PRODUCTCATGROUP PCG ON PCG.CATEGORYID=(SELECT CATEGORYID FROM PRODUCTCATEGORIES WHERE FACTID=PPC.FACTID) "
                StrSql = StrSql + "INNER JOIN PRODUCTGROUP ON PRODUCTGROUP.PARENTID=PCG.PRODUCTGROUPID "
                StrSql = StrSql + "INNER JOIN FACT ON FACT.ID=PPC.FACTID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATEGORIES PC ON PC.FACTID = PPC.FACTID "
                StrSql = StrSql + "WHERE PPC.FACTID IN (" + ProdId + ")  "
                If GrpID <> "" Then
                    StrSql = StrSql + " AND PRODUCTGROUP.PRODGROUPID IN (" + GrpID + ")"
                End If
                If PackId <> "" Then
                    StrSql = StrSql + " AND PPC.PACKAGETYPEID IN (" + PackId + ")"
                End If
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotPackCompProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotGRPProducts(ByVal ProdId As String, ByVal GrpID As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT "
                StrSql = StrSql + "PRODUCTGROUP.PRODGROUPID SUBGROUPID,PRODUCTGROUP.NAME, "
                StrSql = StrSql + "PC.FACTID ID, PC.ID CATEGORYID,PC.CATEGORYNAME VALUE "
                StrSql = StrSql + "FROM FACTPACKTYPEPACKSIZE PPC "
                StrSql = StrSql + "INNER JOIN PRODUCTCATGROUP PCG ON PCG.CATEGORYID=(SELECT CATEGORYID FROM PRODUCTCATEGORIES WHERE FACTID=PPC.FACTID) "
                StrSql = StrSql + "INNER JOIN PRODUCTGROUP ON PRODUCTGROUP.PARENTID=PCG.PRODUCTGROUPID "
                StrSql = StrSql + "INNER JOIN FACT ON FACT.ID=PPC.FACTID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATEGORIES PC ON PC.FACTID = PPC.FACTID "
                StrSql = StrSql + "WHERE PPC.FACTID IN (" + ProdId + ")  "
                If GrpID <> "" Then
                    StrSql = StrSql + " AND PRODUCTGROUP.PRODGROUPID IN (" + GrpID + ")"
                End If
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotPackCompProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotCompProducts(ByVal CompId As String, ByVal ProdId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PC.FACTID ID, PC.ID CATEGORYID, PPC.COMPID COMPONENTID,  "
                StrSql = StrSql + "PC.CATEGORYNAME VALUE "
                StrSql = StrSql + " FROM PRODPACKCOMPONENTS PPC "
                StrSql = StrSql + "INNER JOIN FACT ON FACT.ID=PPC.FACTID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATEGORIES PC ON PC.FACTID = FACT.ID "
                StrSql = StrSql + " WHERE COMPID IN (" + CompId + ") "
                If ProdId <> "" Then
                    StrSql = StrSql + " AND PPC.FACTID IN (" + ProdId + ")"
                End If
                StrSql = StrSql + " ORDER BY ID ASC "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetProducts:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotComponentsPackages(ByVal ProdId As String, ByVal CompId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PACKID PACKAGETYPEID, "
                StrSql = StrSql + "(PACKAGETYPE.PACKAGETYPEDE1||' '||PACKAGETYPE.PACKAGETYPEDE2)VALUE "
                StrSql = StrSql + " FROM PRODPACKCOMPONENTS PPC "
                StrSql = StrSql + "INNER JOIN PACKAGETYPE "
                StrSql = StrSql + "ON PACKAGETYPE.PACKAGETYPEID=PPC.PACKID "
                StrSql = StrSql + " WHERE PPC.COMPID IN (" + CompId + ") "
                StrSql = StrSql + " AND PPC.FACTID IN (" + ProdId + ") "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotPackages:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotComponentsProducts(ByVal FactId As String, ByVal PackId As String, ByVal CompId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PC.FACTID ID, PC.ID CATEGORYID, "
                StrSql = StrSql + "PC.CATEGORYNAME VALUE "
                StrSql = StrSql + " FROM PRODPACKCOMPONENTS PPC "
                StrSql = StrSql + "INNER JOIN FACT ON FACT.ID=PPC.FACTID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATEGORIES PC ON PC.FACTID = FACT.ID "
                StrSql = StrSql + " WHERE PPC.FACTID IN (" + FactId + ") "
                If Not String.IsNullOrEmpty(CompId) Then
                    StrSql = StrSql + " AND PPC.COMPID IN (" + CompId + ") "
                End If
                If Not String.IsNullOrEmpty(PackId) Then
                    StrSql = StrSql + " AND PPC.PACKID IN (" + PackId + ") "
                End If
                StrSql = StrSql + " ORDER BY ID ASC"

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotPackages:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetPivotAllComponents(ByVal ProdId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT DISTINCT PPC.COMPID COMPONENTID, COMPDES VALUE "
                StrSql = StrSql + "FROM COMPONENTS COMP "
                StrSql = StrSql + "INNER JOIN PRODPACKCOMPONENTS PPC ON PPC.COMPID=COMP.COMPID "
                StrSql = StrSql + "INNER JOIN PRODUCTCATEGORIES PC ON PC.FACTID=PPC.FACTID "
                StrSql = StrSql + " WHERE PPC.FACTID IN (" + ProdId + ")"

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1SubGetData:GetPivotProdComponents:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region
    End Class
End Class
