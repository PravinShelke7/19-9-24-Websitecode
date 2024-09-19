Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System

Public Class EmoGetData
    Public Class Selectdata
        Dim EmoConnection As String = System.Configuration.ConfigurationManager.AppSettings("EmonitorConnectionString")
        Public Function GetDataManager() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                StrSql = "SELECT "
                StrSql = StrSql + "SPECSDATAID,  "
                StrSql = StrSql + "SPECNUM, "
                StrSql = StrSql + "PACKTYPE, "
                StrSql = StrSql + "LABELTYPE, "
                StrSql = StrSql + "BUSINESSUNIT, "
                StrSql = StrSql + "BRAND, "
                StrSql = StrSql + "VOLUME, "
                StrSql = StrSql + "LNGTH, "
                StrSql = StrSql + "WIDTH, "
                StrSql = StrSql + "(LNGTH*WIDTH)AREA, "
                StrSql = StrSql + "WEIGHT, "
                StrSql = StrSql + "GHGRELEASE, "
                StrSql = StrSql + "(WEIGHT*GHGRELEASE) GHGTOT, "
                StrSql = StrSql + "EFFDATE, "
                StrSql = StrSql + "SKU "
                StrSql = StrSql + "FROM SPECSDATA "

                'StrSql = "SELECT  "
                'StrSql = StrSql + "SPECSDATAID, "
                'StrSql = StrSql + "SPECNUM, "
                'StrSql = StrSql + "PACKTYPE, "
                'StrSql = StrSql + "LABELTYPE, "
                'StrSql = StrSql + "BUSINESSUNIT, "
                'StrSql = StrSql + "BRAND, "
                'StrSql = StrSql + "VOLUME, "
                'StrSql = StrSql + "TO_CHAR(LNGTH, 'FM9999999999.90') LNGTH, "
                'StrSql = StrSql + "TO_CHAR(WIDTH, 'FM9999999999.90') WIDTH, "
                'StrSql = StrSql + "TO_CHAR(LNGTH*WIDTH, 'FM9999999999.90') AREA, "
                'StrSql = StrSql + "WEIGHT, "
                'StrSql = StrSql + "TO_CHAR(GHGRELEASE, 'FM9999999999.90') GHGRELEASE, "
                'StrSql = StrSql + "(WEIGHT*GHGRELEASE) GHGTOT, "
                'StrSql = StrSql + "EFFDATE, "
                'StrSql = StrSql + "SKU "
                'StrSql = StrSql + "FROM SPECSDATA "

                Dts = odbUtil.FillDataSet(StrSql, EmoConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

        Public Function GetGHGTotal() As DataSet
            Dim Dts As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Try
                'strsql = "SELECT COUNT(1) CNT,  " 
                ' StrSql = StrSql + "SUM(VOLUME) VOLTOT, "
                ' StrSql = StrSql + "SUM(WEIGHT) WGHTTOT, "
                ' StrSql = StrSql + "SUM(WEIGHT*GHGRELEASE) GHGTOT, "
                ' StrSql = StrSql + "(SUM(WEIGHT*GHGRELEASE)/COUNT(1)) AVGGHGREL "
                ' StrSql = StrSql + "FROM SPECSDATA "

                'StrSql = "SELECT DISTINCT TO_CHAR(B.EFFDATE,'yyyy') EFFDATE, CNT,VOLTOT,WGHTTOT, "
                'StrSql = StrSql + "GHGTOTAL,TO_CHAR(AVGGHGREL, 'FM9999999999.90') AVGGHGREL FROM "
                'StrSql = StrSql + "( "
                'StrSql = StrSql + "SELECT COUNT(1) CNT, "
                'StrSql = StrSql + "SUM(VOLUME) VOLTOT, "
                'StrSql = StrSql + "SUM(WEIGHT) WGHTTOT, "
                'StrSql = StrSql + "SUM(WEIGHT*GHGRELEASE) GHGTOTAL, "
                'StrSql = StrSql + "(SUM(WEIGHT*GHGRELEASE)/COUNT(1)) AVGGHGREL "
                'StrSql = StrSql + "FROM SPECSDATA "
                'StrSql = StrSql + ") A,SPECSDATA B "

                StrSql = "SELECT DISTINCT TO_CHAR(B.EFFDATE,'yyyy') EFFDATE, CNT,VOLTOT,WGHTTOT,"
                StrSql = StrSql + "GHGTOTAL,AVGGHGREL FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT COUNT(1) CNT, "
                StrSql = StrSql + "SUM(VOLUME) VOLTOT, "
                StrSql = StrSql + "SUM(WEIGHT) WGHTTOT, "
                StrSql = StrSql + "SUM(WEIGHT*GHGRELEASE) GHGTOTAL, "
                StrSql = StrSql + "(SUM(WEIGHT*GHGRELEASE)/SUM(WEIGHT)) AVGGHGREL "
                StrSql = StrSql + "FROM SPECSDATA "
                StrSql = StrSql + ") A,SPECSDATA B "

                Dts = odbUtil.FillDataSet(StrSql, EmoConnection)
                Return Dts
            Catch ex As Exception
                Throw New Exception("E1GetData:GetExtrusionDetails:" + ex.Message.ToString())
                Return Dts
            End Try
        End Function

    End Class
End Class
