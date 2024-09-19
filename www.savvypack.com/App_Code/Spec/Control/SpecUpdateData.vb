Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Spec

Public Class SpecUpdateData
    Public Class UpdateInsert


        Public Sub UpdateSpecifications(ByVal SpecId As Integer, ByVal Name As String, ByVal CompName As String, ByVal E1S1CaseId As Integer)
            Dim i As New Integer
            Dim StrSql As String = String.Empty
            Dim odButil As New DBUtil()
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")
            Try
                StrSql = "UPDATE PACKAGINGSPECIFICATIONS  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "COMPNAME = '" + CompName.Replace("'", "''").ToString() + "' , "
                StrSql = StrSql + "NAME ='" + Name + " ', "
                StrSql = StrSql + "E1S1CASEID =" + E1S1CaseId.ToString() + "  "
                StrSql = StrSql + "WHERE PACKAGINGSPECIFICATIONID = " + SpecId.ToString() + " "
                odButil.UpIns(StrSql, MyConnectionString)
            Catch ex As Exception
                Throw New Exception("UpdateInsert:UpdateSpecifications:-" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub CreateSpecifications(ByVal Name As String, ByVal CompName As String, ByVal E1S1CaseId As Integer)
            Dim i As New Integer
            Dim SpecId As New Integer
            Dim StrSql As String = String.Empty
            Dim StrSqlSpec As String = String.Empty
            Dim Ds As New DataSet()
            Dim odButil As New DBUtil()
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")
            Try
                'Getting SpecId
                StrSqlSpec = "SELECT SEQSPECIFICATIONID.NEXTVAL AS SPECID FROM DUAL"
                Ds = odButil.FillDataSet(StrSqlSpec, MyConnectionString)
                SpecId = CInt(Ds.Tables(0).Rows(0).Item("SPECID").ToString())

                'Adding Data to PACKAGINGSPECIFICATIONS
                StrSql = "INSERT INTO PACKAGINGSPECIFICATIONS  "
                StrSql = StrSql + "(PACKAGINGSPECIFICATIONID,COMPNAME,NAME,E1S1CASEID) "
                StrSql = StrSql + "VALUES "
                StrSql = StrSql + "(" + SpecId.ToString() + ",'" + CompName.Replace("'", "''").ToString() + "','" + Name + "'," + E1S1CaseId.ToString() + ") "
                odButil.UpIns(StrSql, MyConnectionString)

                'Adding SpecId to Other Table
                CreateSpecIdForOtherTable(SpecId, "PKGMFGMATERIALLAYERS")
                CreateSpecIdForOtherTable(SpecId, "S1TOTAL")

            Catch ex As Exception
                Throw New Exception("UpdateInsert:CreateSpecifications:-" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub CreateSpecIdForOtherTable(ByVal SpecId As Integer, ByVal TableName As String)
            Dim i As New Integer
            Dim StrSql As String = String.Empty
            Dim odButil As New DBUtil()
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")
            Try
                StrSql = "INSERT INTO " + TableName + " (PACKAGINGSPECIFICATIONID)  VALUES  (" + SpecId.ToString() + ") "
                odButil.UpIns(StrSql, MyConnectionString)
            Catch ex As Exception
                Throw New Exception("UpdateInsert:CreateSpecIdForOtherTable:-" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub UpdateExtrusion(ByVal MatSpec() As String, ByVal MatS() As String, ByVal Thick() As String, ByVal Rec() As String, ByVal Dept() As String, ByVal Expro() As String, ByVal SpecId As Integer)
            Dim i As New Integer
            Dim StrSql As String = String.Empty
            Dim odButil As New DBUtil()
            Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")
            Try
                StrSql = "UPDATE PKGMFGMATERIALLAYERS SET "
                For i = 1 To 10
                    StrSql = StrSql + "M" + i.ToString() + "='" + MatSpec(i).ToString() + "', "
                    StrSql = StrSql + "EM" + i.ToString() + "=" + MatS(i).ToString() + ", "
                    StrSql = StrSql + "T" + i.ToString() + "=" + Thick(i).ToString() + ", "
                    StrSql = StrSql + "R" + i.ToString() + "=" + Rec(i).ToString() + ", "
                    StrSql = StrSql + "D" + i.ToString() + "=" + Dept(i).ToString() + ", "
                    StrSql = StrSql + "E" + i.ToString() + "=" + Expro(i).ToString() + ", "
                Next
                StrSql = StrSql.Remove(StrSql.Length - 2, 1)
                StrSql = StrSql + "WHERE PACKAGINGSPECIFICATIONID =" + SpecId.ToString() + ""
                odButil.UpIns(StrSql, MyConnectionString)
            Catch ex As Exception
                Throw New Exception("UpdateInsert:UpdateExtrusion:-" + ex.Message.ToString())
            End Try
        End Sub

        'Public Sub UpdateEnergyAndGHG(ByVal Total As tblTotal, ByVal Resultspl As tblResultspl, ByVal SpecId As Integer)
        '    Dim i As New Integer
        '    Dim StrSql As String = String.Empty
        '    Dim odButil As New DBUtil()
        '    Dim MyConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")
        '    Try
        '        StrSql = "UPDATE S1TOTAL  "
        '        StrSql = StrSql + "SET "
        '        StrSql = StrSql + "RMERGY = " + Total.RMERGY.ToString() + ", "
        '        StrSql = StrSql + "RMPACKERGY = " + Total.RMPACKERGY.ToString() + ", "
        '        StrSql = StrSql + "RMANDPACKTRNSPTERGY=" + Total.RMANDPACKTRNSPTERGY.ToString() + ", "
        '        StrSql = StrSql + "DPPACKERGY=" + Total.DPPACKERGY.ToString() + ", "
        '        StrSql = StrSql + "TRSPTTOCUS=" + Total.TRSPTTOCUS.ToString() + ", "
        '        StrSql = StrSql + "DPTRNSPTERGY=" + Total.DPTRNSPTERGY.ToString() + ", "
        '        StrSql = StrSql + "PROCERGY=" + Total.PROCERGY.ToString() + ", "
        '        StrSql = StrSql + "RMGRNHUSGAS=" + Total.RMGRNHUSGAS.ToString() + ", "
        '        StrSql = StrSql + "RMPACKGRNHUSGAS=" + Total.RMPACKGRNHUSGAS.ToString() + ", "
        '        StrSql = StrSql + "RMANDPACKTRNSPTGRNHUSGAS=" + Total.RMANDPACKTRNSPTGRNHUSGAS.ToString() + ", "
        '        StrSql = StrSql + "DPPACKGRNHUSGAS=" + Total.DPPACKGRNHUSGAS.ToString() + ", "
        '        StrSql = StrSql + "TRSPTTOCUSGRNHUSGAS=" + Total.TRSPTTOCUSGRNHUSGAS.ToString() + ", "
        '        StrSql = StrSql + "DPTRNSPTGRNHUSGAS=" + Total.DPTRNSPTGRNHUSGAS.ToString() + ", "
        '        StrSql = StrSql + "PROCGRNHUSGAS=" + Total.PROCGRNHUSGAS.ToString() + ", "
        '        StrSql = StrSql + "VOLUME=" + Resultspl.VOLUME.ToString() + ", "
        '        StrSql = StrSql + "FINVOLMUNITS=" + Resultspl.FINVOLMUNITS.ToString() + ", "
        '        StrSql = StrSql + "FINVOLMSI=" + Resultspl.FINVOLMSI.ToString() + " "
        '        StrSql = StrSql + "WHERE PACKAGINGSPECIFICATIONID = " + SpecId.ToString() + " "

        '        odButil.UpIns(StrSql, MyConnectionString)
        '    Catch ex As Exception
        '        Throw New Exception("UpdateInsert:UpdateEnergyAndGHG:-" + ex.Message.ToString())
        '    End Try
        'End Sub

#Region "Upload Csv"
        Public Function FireInsUpdateDelete(ByVal StrSql As String) As Integer
            Dim Status As New Integer
            Try
                Dim ODbutil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")
                ODbutil.UpIns(StrSql, MyConnectionString)
            Catch ex As Exception
                Throw New Exception("UpdateInsert.FireInsUpdateDelete:" + ex.Message.ToString())
            End Try
            Return Status
        End Function

        Public Function UpdateSpecDetailsCsv(ByVal UserName As String) As Integer
            Dim StrSql As String
            Dim Status As New Integer
            Try
                Dim ODbutil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")

                StrSql = "UPDATE PACKAGINGSPECIFICATIONS MAIN  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "(MAIN.NAME,MAIN.COMPNAME,MAIN.CLIENTSPECID,MAIN.E1S1CASEID) "
                StrSql = StrSql + "= "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT DISTINCT TEMP.NAME,TEMP.COMPNAME,TEMP.CLIENTSPECID,TEMP.E1S1CASEID "
                StrSql = StrSql + "FROM TEMPSPECUPLOAD TEMP "
                StrSql = StrSql + "WHERE UPPER(USERNAME) = '" + UserName + "' "
                StrSql = StrSql + "AND MAIN.CLIENTSPECID = TEMP.CLIENTSPECID "
                StrSql = StrSql + ") "
                StrSql = StrSql + "WHERE EXISTS "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 "
                StrSql = StrSql + "FROM TEMPSPECUPLOAD TEMP "
                StrSql = StrSql + "WHERE UPPER(USERNAME) = '" + UserName + "' "
                StrSql = StrSql + "AND MAIN.CLIENTSPECID = TEMP.CLIENTSPECID "
                StrSql = StrSql + ") "

                ODbutil.UpIns(StrSql, MyConnectionString)
            Catch ex As Exception
                Throw New Exception("UpdateInsert.UpdateSpecDetailsCsv:" + ex.Message.ToString())
            End Try
            Return Status
        End Function

        Public Function InsertSpecDetailsCsv(ByVal UserName As String) As Integer
            Dim StrSql As String
            Dim Status As New Integer
            Try
                Dim ODbutil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")

                StrSql = "INSERT INTO PACKAGINGSPECIFICATIONS  "
                StrSql = StrSql + "(PACKAGINGSPECIFICATIONID,NAME,COMPNAME,CLIENTSPECID,E1S1CASEID) "
                StrSql = StrSql + "SELECT SEQSPECIFICATIONID.NEXTVAL, "
                StrSql = StrSql + "TEMP.NAME, "
                StrSql = StrSql + "TEMP.COMPNAME, "
                StrSql = StrSql + "TEMP.CLIENTSPECID, "
                StrSql = StrSql + "TEMP.E1S1CASEID "
                StrSql = StrSql + "FROM( "
                StrSql = StrSql + "SELECT DISTINCT TEMPSPECUPLOAD.NAME, "
                StrSql = StrSql + "TEMPSPECUPLOAD.COMPNAME, "
                StrSql = StrSql + "TEMPSPECUPLOAD.CLIENTSPECID, "
                StrSql = StrSql + "TEMPSPECUPLOAD.E1S1CASEID "
                StrSql = StrSql + "FROM TEMPSPECUPLOAD "
                StrSql = StrSql + "WHERE UPPER(TEMPSPECUPLOAD.USERNAME) = '" + UserName + "' "
                StrSql = StrSql + ")TEMP "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 "
                StrSql = StrSql + "FROM TEMPSPECUPLOAD,PACKAGINGSPECIFICATIONS "
                StrSql = StrSql + "WHERE UPPER(TEMPSPECUPLOAD.USERNAME) = '" + UserName + "' "
                StrSql = StrSql + "AND TEMPSPECUPLOAD.CLIENTSPECID = PACKAGINGSPECIFICATIONS.CLIENTSPECID "
                StrSql = StrSql + ") "


                ODbutil.UpIns(StrSql, MyConnectionString)
                InsertOtherTblesCsv()
            Catch ex As Exception
                Throw New Exception("UpdateInsert.InsertSpecDetailsCsv:" + ex.Message.ToString())
            End Try
            Return Status
        End Function

        Public Sub InsertOtherTblesCsv()
        
            Try
                InsertSpecIdInOtherTbleCsc("S1TOTAL")
                InsertSpecIdInOtherTbleCsc("PKGMFGMATERIALLAYERS")
            Catch ex As Exception
                Throw New Exception("UpdateInsert.InsertSpecDetailsCsv:" + ex.Message.ToString())
            End Try

        End Sub

        Public Function InsertSpecIdInOtherTbleCsc(ByVal TableName As String) As Integer
            Dim StrSql As String
            Dim Status As New Integer
            Try
                Dim ODbutil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")

                StrSql = "INSERT INTO " + TableName + "  "
                StrSql = StrSql + "(PACKAGINGSPECIFICATIONID) "
                StrSql = StrSql + "SELECT DISTINCT PACKAGINGSPECIFICATIONID FROM PACKAGINGSPECIFICATIONS "
                StrSql = StrSql + "WHERE NOT EXISTS "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT 1 FROM " + TableName + " "
                StrSql = StrSql + "WHERE " + TableName + ".PACKAGINGSPECIFICATIONID = PACKAGINGSPECIFICATIONS.PACKAGINGSPECIFICATIONID "
                StrSql = StrSql + ") "



                ODbutil.UpIns(StrSql, MyConnectionString)
            Catch ex As Exception
                Throw New Exception("UpdateInsert.InsertSpecDetailsCsv:" + ex.Message.ToString())
            End Try
            Return Status
        End Function

        Public Function DeleteSpecLayerCsv(ByVal UserName As String) As Integer
            Dim StrSql As String
            Dim Status As New Integer
            Try
                Dim ODbutil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")

                StrSql = "DELETE FROM PKGMFGMATERIALLAYERS  "
                StrSql = StrSql + "WHERE PACKAGINGSPECIFICATIONID IN "
                StrSql = StrSql + "( SELECT DISTINCT PACKAGINGSPECIFICATIONID "
                StrSql = StrSql + "FROM PACKAGINGSPECIFICATIONS "
                StrSql = StrSql + "INNER JOIN TEMPSPECUPLOAD "
                StrSql = StrSql + "ON TEMPSPECUPLOAD.CLIENTSPECID = PACKAGINGSPECIFICATIONS.CLIENTSPECID "
                StrSql = StrSql + "WHERE UPPER(TEMPSPECUPLOAD.USERNAME) ='" + UserName + "' "
                StrSql = StrSql + ") "


                ODbutil.UpIns(StrSql, MyConnectionString)
            Catch ex As Exception
                Throw New Exception("UpdateInsert.DeleteSpecLayerCsv:" + ex.Message.ToString())
            End Try
            Return Status
        End Function

        Public Function InsertSpecLayerCsv(ByVal UserName As String) As Integer
            Dim StrSql As String
            Dim Status As New Integer
            Try
                Dim ODbutil As New DBUtil()
                Dim MyConnectionString As String = ""
                MyConnectionString = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")

                StrSql = "INSERT INTO PKGMFGMATERIALLAYERS  "
                StrSql = StrSql + "SELECT  ROWINDEXBY.PACKAGINGSPECIFICATIONID, "
                '-- M1-M10
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 1 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAT "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "NULL "
                StrSql = StrSql + "END) AS M1, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 2 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAT "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "NULL "
                StrSql = StrSql + "END) AS M2, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 3 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAT "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "NULL "
                StrSql = StrSql + "END) AS M3, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 4 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAT "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "NULL "
                StrSql = StrSql + "END) AS M4, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 5 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAT "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "NULL "
                StrSql = StrSql + "END) AS M5, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 6 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAT "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "NULL "
                StrSql = StrSql + "END) AS M6, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 7 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAT "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "NULL "
                StrSql = StrSql + "END) AS M7, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 8 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAT "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "NULL "
                StrSql = StrSql + "END) AS M8, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 9 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAT "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "NULL "
                StrSql = StrSql + "END) AS M9, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 10 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAT "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "NULL "
                StrSql = StrSql + "END) AS M10, "
                '-- T1-T10 
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 1 THEN "
                StrSql = StrSql + "ROWINDEXBY.THICK "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS T1, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 2 THEN "
                StrSql = StrSql + "ROWINDEXBY.THICK "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS T2, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 3 THEN "
                StrSql = StrSql + "ROWINDEXBY.THICK "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS T3, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 4 THEN "
                StrSql = StrSql + "ROWINDEXBY.THICK "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS T4, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 5 THEN "
                StrSql = StrSql + "ROWINDEXBY.THICK "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS T5, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 6 THEN "
                StrSql = StrSql + "ROWINDEXBY.THICK "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS T6, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 7 THEN "
                StrSql = StrSql + "ROWINDEXBY.THICK "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS T7, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 8 THEN "
                StrSql = StrSql + "ROWINDEXBY.THICK "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS T8, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 9 THEN "
                StrSql = StrSql + "ROWINDEXBY.THICK "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS T9, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 10 THEN "
                StrSql = StrSql + "ROWINDEXBY.THICK "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS T10, "
                '-- R1-R10 
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 1 THEN "
                StrSql = StrSql + "ROWINDEXBY.RECYCLE "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS R1, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 2 THEN "
                StrSql = StrSql + "ROWINDEXBY.RECYCLE "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS R2, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 3 THEN "
                StrSql = StrSql + "ROWINDEXBY.RECYCLE "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS R3, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 4 THEN "
                StrSql = StrSql + "ROWINDEXBY.RECYCLE "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS R4, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 5 THEN "
                StrSql = StrSql + "ROWINDEXBY.RECYCLE "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS R5, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 6 THEN "
                StrSql = StrSql + "ROWINDEXBY.RECYCLE "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS R6, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 7 THEN "
                StrSql = StrSql + "ROWINDEXBY.RECYCLE "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS R7, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 8 THEN "
                StrSql = StrSql + "ROWINDEXBY.RECYCLE "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS R8, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 9 THEN "
                StrSql = StrSql + "ROWINDEXBY.RECYCLE "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS R9, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 10 THEN "
                StrSql = StrSql + "ROWINDEXBY.RECYCLE "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS R10, "
                '-- D1-D10 
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 1 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAPPEDDEPTID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS D1, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 2 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAPPEDDEPTID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS D2, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 3 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAPPEDDEPTID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS D3, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 4 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAPPEDDEPTID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS D4, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 5 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAPPEDDEPTID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS D5, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 6 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAPPEDDEPTID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS D6, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 7 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAPPEDDEPTID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS D7, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 8 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAPPEDDEPTID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS D8, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 9 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAPPEDDEPTID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS D9, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 10 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAPPEDDEPTID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS D10, "
                '-- EM1-EM10 
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 1 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAPPEDMATID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS EM1, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 2 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAPPEDMATID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS EM2, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 3 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAPPEDMATID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS EM3, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 4 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAPPEDMATID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS EM4, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 5 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAPPEDMATID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS EM5, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 6 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAPPEDMATID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS EM6, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 7 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAPPEDMATID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS EM7, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 8 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAPPEDMATID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS EM8, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 9 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAPPEDMATID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS EM9, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 10 THEN "
                StrSql = StrSql + "ROWINDEXBY.MAPPEDMATID "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS EM10, "
                '-- E1-E10 
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 1 THEN "
                StrSql = StrSql + "ROWINDEXBY.EXTRAPROC "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS E1, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 2 THEN "
                StrSql = StrSql + "ROWINDEXBY.EXTRAPROC "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS E2, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 3 THEN "
                StrSql = StrSql + "ROWINDEXBY.EXTRAPROC "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS E3, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 4 THEN "
                StrSql = StrSql + "ROWINDEXBY.EXTRAPROC "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS E4, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 5 THEN "
                StrSql = StrSql + "ROWINDEXBY.EXTRAPROC "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS E5, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 6 THEN "
                StrSql = StrSql + "ROWINDEXBY.EXTRAPROC "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS E6, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 7 THEN "
                StrSql = StrSql + "ROWINDEXBY.EXTRAPROC "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS E7, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 8 THEN "
                StrSql = StrSql + "ROWINDEXBY.EXTRAPROC "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS E8, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 9 THEN "
                StrSql = StrSql + "ROWINDEXBY.EXTRAPROC "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS E9, "
                StrSql = StrSql + "MAX(CASE WHEN ROWINDEXBY.ROWINDEX = 10 THEN "
                StrSql = StrSql + "ROWINDEXBY.EXTRAPROC "
                StrSql = StrSql + "ELSE "
                StrSql = StrSql + "0 "
                StrSql = StrSql + "END) AS E10 "
                StrSql = StrSql + "FROM "
                StrSql = StrSql + "( "
                StrSql = StrSql + "SELECT TEMP.CLIENTSPECID, "
                StrSql = StrSql + "MAIN.PACKAGINGSPECIFICATIONID, "
                StrSql = StrSql + "ROW_NUMBER( ) OVER (PARTITION BY PACKAGINGSPECIFICATIONID ORDER BY LINENUMBER)  AS ROWINDEX, "
                StrSql = StrSql + "TEMP.MAT, "
                StrSql = StrSql + "TEMP.THICK, "
                StrSql = StrSql + "TEMP.RECYCLE, "
                StrSql = StrSql + "TEMP.EXTRAPROC, "
                StrSql = StrSql + "TEMP.MAPPEDMATID, "
                StrSql = StrSql + "TEMP.MAPPEDDEPTID "
                StrSql = StrSql + "FROM PACKAGINGSPECIFICATIONS MAIN "
                StrSql = StrSql + "INNER JOIN TEMPSPECUPLOAD TEMP "
                StrSql = StrSql + "ON TEMP.CLIENTSPECID = MAIN.CLIENTSPECID "
                StrSql = StrSql + "WHERE UPPER(TEMP.USERNAME) ='" + UserName + "' "
                StrSql = StrSql + ") ROWINDEXBY "
                StrSql = StrSql + "GROUP BY "
                StrSql = StrSql + "ROWINDEXBY.PACKAGINGSPECIFICATIONID "


                ODbutil.UpIns(StrSql, MyConnectionString)
            Catch ex As Exception
                Throw New Exception("UpdateInsert.InsertSpecLayerCsv:" + ex.Message.ToString())
            End Try
            Return Status
        End Function
#End Region

#Region "RFP"
        Public Sub RFPRename(ByVal CaseId As String, ByVal Des1 As String, ByVal Des2 As String, ByVal Des3 As String)
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New E1GetData.Selectdata()
            Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")
            Dim StrSql As String = String.Empty
            Try
                StrSql = "UPDATE RFPDETAILS SET DES1 ='" + Des1 + "', DES2 ='" + Des2 + "', DES3 ='" + Des3 + "'  WHERE RFPID=" + CaseId.ToString() + " "
                odbUtil.UpIns(StrSql, Connection)

            Catch ex As Exception
                Throw New Exception("SpecUpDateData:RFPRename:" + ex.Message.ToString())
            End Try
        End Sub

        Public Function CreateRFP(ByVal UserName As String, ByVal Des1 As String, ByVal Des2 As String, ByVal Des3 As String) As Integer
            Dim Dts As New DataSet()
            Dim DtsTblDet As New DataSet()
            Dim odbUtil As New DBUtil()
            Dim StrSql As String = String.Empty
            Dim Column As String = String.Empty
            Dim Value As String = String.Empty
            Dim Connection As String = System.Configuration.ConfigurationManager.AppSettings("SpecConnectionString")
            Dim i As New Integer
            Dim j As New Integer
            Dim CaseId As New Integer

            Try
                'Case Id 

                StrSql = "SELECT SEQRFPID.NEXTVAL AS CASEID FROM DUAL"
                Dts = odbUtil.FillDataSet(StrSql, Connection)
                CaseId = CInt(Dts.Tables(0).Rows(0).Item("CaseId").ToString())


                'Insert Into PermissionsCases
                StrSql = "INSERT INTO RFPDETAILS  "
                StrSql = StrSql + "(RFPID,DES1,DES2, DES3, USERNAME, SERVERDATE,CREATIONDATE) "
                StrSql = StrSql + "VALUES "
                StrSql = StrSql + "(" + CaseId.ToString() + ",'" + Des1.ToString() + "','" + Des2.ToString() + "','" + Des3.ToString() + "','" + UserName.ToString() + "',SYSDATE,SYSDATE) "
                odbUtil.UpIns(StrSql, Connection)

                Return CaseId
            Catch ex As Exception
                Throw New Exception("SpecUpdateData:CreateRFP:" + ex.Message.ToString())
                Return CaseId
            End Try
        End Function
#End Region

    End Class
End Class
