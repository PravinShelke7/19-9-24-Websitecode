Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports System.Data.OracleClient

Public Class M1GetData

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
                Throw New Exception("M1GetData:GetMinMaxYear:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetContinentsWise:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetCountryWise:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetCountrYearWise:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetSystemReports:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetUserCustomReports:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetUserCustomReports:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

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
                Throw New Exception("M1GetData:GetUserReportsFilter:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetUserReportsRows:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetUserReportsRows:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetUserReportsCols:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetCAGRReports(ByVal tbl As String, ByVal Cntry As String, ByVal Year As String, ByVal Curr As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try

              

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
                Throw New Exception("M1GetData:GetUserReportsCols:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetUserReportsCols:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetReportId:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetReportId:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetReportId:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetReportFilterId:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetReportId:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetReportFilter:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetReportFilter:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetReportFilter:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetCurrency:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetRowsSelector:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetRowsSelector:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetRowsSelector:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetColsSelector:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetFilterSelector(ByVal filterTypeId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                StrSql = "SELECT FILTERTYPEID,  "
                StrSql = StrSql + "FILTERDES, "
                StrSql = StrSql + "FILTERVALUE, "
                StrSql = StrSql + "FILTERCODE, "
                StrSql = StrSql + "FILTERSQL "
                StrSql = StrSql + "FROM FILTERTYPE "
                StrSql = StrSql + "WHERE FILTERTYPEID=CASE WHEN  " + filterTypeId + " =-1 THEN FILTERTYPEID ELSE " + filterTypeId + " END "
                StrSql = StrSql + "ORDER BY FILTERDES "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1GetData:GetFilterSelector:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetFilterType:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetRowSQL:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetRowSQL:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetRowSQL(ByVal rSql As String) As DataSet
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
                End If


                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1GetData:GetRowSQL:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetRowSQL:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetFilterSQL(ByVal rSql As String) As DataSet
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

                End If

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1GetData:GetFilterSQL:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetFilterSQL:" + ex.Message.ToString())
                Return Dts
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

                Return Dts.Tables(0).Rows(0)("REGIONCOUNT").ToString
            Catch ex As Exception
                Throw New Exception("M1GetData:GetFilterSQL:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetFilterSQL:" + ex.Message.ToString())
                Return 0
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
                Throw New Exception("M1GetData:GetFilterSQL:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetFilterSQL:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetUsersReportRows(ByVal reportId As String) As DataSet
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
                Throw New Exception("M1GetData:GetUsersReportRows:" + ex.Message.ToString())
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
                StrSql = StrSql + "FILTERVALUE "
                StrSql = StrSql + "FROM USERREPORTFILTERS "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "USERREPORTID=CASE WHEN " + reportId + "=-1 THEN USERREPORTID  ELSE " + reportId + " END "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1GetData:GetUsersReportFilters:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetUsersReportColumns:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetUsersReportColumns:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUsersDynamicReportData(ByVal ReportId As Integer, ByVal reportDes As String) As DataSet
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


                If reportDes = "CNTRY" Then
                    cmd.CommandText = "PKG_GETREPORTS.Proc_GetReportDataByCountry"
                ElseIf reportDes = "REGION" Then
                    cmd.CommandText = "PKG_GETREPORTS.Proc_GetReportDataByRegion"
                End If

                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New OracleParameter("io_cursor", OracleType.Cursor)).Direction = ParameterDirection.Output
                cmd.Parameters.Add("In_Report_ID", OracleType.Number, 4).Value = ReportId
                da = New OracleDataAdapter(cmd)
                'da.Fill(ds, 0)
                da.Fill(ds)
            Catch ex As Exception
                Throw New Exception("M1GetData:GetReportData:" + ex.Message.ToString())
                Return ds
            End Try
            Return ds
        End Function
        Public Function GetUsersDynamicUniformReportData(ByVal ReportId As Integer, ByVal reportDes As String) As DataSet
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

                If reportDes = "CNTRY" Then
                    cmd.CommandText = "PKG_GETREPORTS.Proc_GetReportDataByCountry"
                ElseIf reportDes = "REGION" Then
                    cmd.CommandText = "PKG_GETREPORTS.Proc_GetReportDataByRegion"
                End If
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New OracleParameter("io_cursor", OracleType.Cursor)).Direction = ParameterDirection.Output
                cmd.Parameters.Add("In_Report_ID", OracleType.Number, 4).Value = ReportId
                da = New OracleDataAdapter(cmd)
                da.Fill(ds, 0)
            Catch ex As Exception
                Throw New Exception("M1GetData:GetReportData:" + ex.Message.ToString())
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
                StrSql = StrSql + "U.ROWVALUEID, "
                StrSql = StrSql + "U.ROWVALUE, "
                StrSql = StrSql + "UNITS.UNITSHRT, "
                StrSql = StrSql + "U.CURR, "
                StrSql = StrSql + "U.UNITID, "
                StrSql = StrSql + "R.ROWCODE, "
                StrSql = StrSql + "R.ROWVALUE AS ROWCOLUMNID "
                StrSql = StrSql + "FROM USERREPORTROWS U "
                StrSql = StrSql + "INNER JOIN ROWTYPE R "
                StrSql = StrSql + "ON R.ROWTYPEID = U.ROWTYPEID "
                StrSql = StrSql + "LEFT JOIN UNITS ON U.UNITID=UNITS.UNITID "
                StrSql = StrSql + "WHERE U.USERREPORTID = " + reportId + ""
                StrSql = StrSql + "ORDER BY U.ROWSEQUENCE "


                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1GetData:GetUsersReportRows:" + ex.Message.ToString())
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
                StrSql = StrSql + "ORDER BY U.COLUMNSEQUENCE "




                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1GetData:GetUsersReportRows:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        
#End Region
        Public Function GetUserRegionSets(ByVal RegionsetId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT REGIONSETID,REGIONSETNAME FROM USERREGIONSETS "
                StrSql = StrSql + " WHERE USERID=0 "
                StrSql = StrSql + " AND REGIONSETID=CASE WHEN " + RegionsetId + " =-1 THEN REGIONSETID  ELSE  " + RegionsetId + " END "
                StrSql = StrSql + "  ORDER BY REGIONSETNAME "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1GetData:GetUsersReportRows:" + ex.Message.ToString())
                Return Dts

            End Try
        End Function
        Public Function GetUserRegions(ByVal RegionSetId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT REGIONID,  "
                StrSql = StrSql + "REGIONNAME "
                StrSql = StrSql + "FROM USERREGIONS "
                StrSql = StrSql + "WHERE REGIONSETID=" + RegionSetId
                StrSql = StrSql + " AND USERID=0"
                StrSql = StrSql + " ORDER BY REGIONNAME"


                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1GetData:GetUsersReportRows:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetProducts:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetProducts:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetProducts:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetProducts:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetUserReportsCols:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetUserReportsCols:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetUnits(ByVal unitId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim ReportId As New Integer
            Try
                StrSql = "SELECT UNITID,  "
                StrSql = StrSql + "UNITDES, "
                StrSql = StrSql + "UNITSHRT "
                StrSql = StrSql + "FROM UNITS "
                StrSql = StrSql + " WHERE UNITID=CASE WHEN " + unitId + "=-1 THEN UNITID ELSE " + unitId + " END "
                StrSql = StrSql + " ORDER BY UNITDES "

                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1GetData:GetUnits:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetReportId:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetRegionDetails:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetRegionDetails:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetRegionsSetDetails:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetUserRegionSet_Regions:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
#End Region

#Region "Base Reports"
        Public Function GetBaseReports() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT BASEREPORTID REPORTID,  "
                StrSql = StrSql + "REPORTNAME, "
                StrSql = StrSql + "BASEREPORTID||':'||REPORTNAME REPORTDES, "
                StrSql = StrSql + "RPTTYPE, "
                StrSql = StrSql + "'Base' Type, "
                StrSql = StrSql + "RPTFACT, "
                StrSql = StrSql + "CREATEDDATE "
                StrSql = StrSql + "FROM BASEREPORTS "
                StrSql = StrSql + " ORDER BY REPORTNAME"
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1GetData:GetBaseReports:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function
        Public Function GetBaseReportsByRptId(ByVal RptId As String) As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT BASEREPORTID REPORTID,  "
                StrSql = StrSql + "REPORTNAME, "
                StrSql = StrSql + "BASEREPORTID||':'||REPORTNAME REPORTDES, "
                StrSql = StrSql + "RPTTYPE, "
                StrSql = StrSql + "RPTFACT, "
                StrSql = StrSql + "RPTTYPEDES, "
                StrSql = StrSql + "CREATEDDATE "
                StrSql = StrSql + "FROM BASEREPORTS "
                StrSql = StrSql + "WHERE BASEREPORTID = " + RptId.ToString() + " "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1GetData:GetBaseReportsByRptId:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetProducts:" + ex.Message.ToString())
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
                Throw New Exception("M1GetData:GetBaseReports:" + ex.Message.ToString())
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
                StrSql = StrSql + " WHERE PARENTID=" + ParentId
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("M1GetData:GetGroups:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

#End Region
    End Class
End Class
