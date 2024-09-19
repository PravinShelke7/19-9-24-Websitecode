Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports M1GetData

Public Class M1UpInsData
    Public Class UpdateInsert
        Dim Market1Connection As String = System.Configuration.ConfigurationManager.AppSettings("Market1ConnectionString")
        Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")


#Region "Reports"

        
        Public Function AddReportDetails(ByVal ReportName As String, ByVal UserId As String, ByVal ReportType As String, ByVal ReportFact As String, ByVal filterCount As String, ByVal rowCount As String, ByVal colCount As String, ByVal ReportTypeDes As String, ByVal RegionSetId As String, ByVal RegionId As String) As Integer
            'Creating Database Connection
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty
            Dim i As New Integer
            Dim ReportId As String = String.Empty
            Dim ReportFilterId As New Integer
            Try

                ReportId = ObjGetData.GetReportId().ToString()
                ReportFilterId = ObjGetData.GetReportFilterId().ToString()
                'Report Details
                StrSql = String.Empty
                StrSql = "INSERT INTO USERREPORTS  "
                StrSql = StrSql + "(USERREPORTID, REPORTNAME, USERID, CREATEDDATE,RPTTYPE,RPTFACT,RPTTYPEDES,REGIONSETID,REGIONID) "
                StrSql = StrSql + "VALUES "
                StrSql = StrSql + "(" + ReportId + ",'" + ReportName + "'," + UserId + ",SYSDATE,'" + ReportType + "','" + ReportFact + "','" + ReportTypeDes + "'," + RegionSetId + "," + RegionId + ") "
                odbUtil.UpIns(StrSql, Market1Connection)

                'Filter Type Details
                StrSql = String.Empty
                ' Dim valFilterType As String
                ' valFilterType = FilterType
                For i = 1 To filterCount
                    StrSql = String.Empty
                    StrSql = "INSERT INTO USERREPORTFILTERS  "
                    StrSql = StrSql + "(USERREPORTFILTERID,USERREPORTID, FILTERSEQUENCE) "
                    StrSql = StrSql + "VALUES "
                    StrSql = StrSql + "(SEQUSERREPORTFILTERID.NEXTVAL," + ReportId + "," + i.ToString() + ") "
                    odbUtil.UpIns(StrSql, Market1Connection)
                Next


                'Report Col. Details
                For i = 1 To colCount
                    StrSql = String.Empty
                    StrSql = "INSERT INTO USERREPORTCOLUMNS  "
                    StrSql = StrSql + "(USERREPORTCOLUMNID, USERREPORTID, COLUMNSEQUENCE) "
                    StrSql = StrSql + "VALUES "
                    StrSql = StrSql + "(SEQUSERREPORTCOLUMNID.NEXTVAL," + ReportId + "," + i.ToString() + ") "
                    odbUtil.UpIns(StrSql, Market1Connection)
                Next

                'Report Row. Details
                For i = 1 To rowCount
                    StrSql = String.Empty
                    StrSql = "INSERT INTO USERREPORTROWS  "
                    StrSql = StrSql + "(USERREPORTROWID, USERREPORTID, ROWSEQUENCE) "
                    StrSql = StrSql + "VALUES "
                    StrSql = StrSql + "(SEQUSERREPORTROWID.NEXTVAL," + ReportId + "," + i.ToString() + ") "
                    odbUtil.UpIns(StrSql, Market1Connection)
                Next



                Return ReportId
            Catch ex As Exception
                Throw New Exception("M1UpInsData:AddReport:" + ex.Message.ToString())
            End Try

        End Function
        Public Sub AddReportRowsDetails(ByVal ReportId As String, ByVal rowCount As String, ByVal ReportType As String, ByVal ReportTypeDes As String, ByVal RegionSetId As String, ByVal RegionId As String)
            'Creating Database Connection
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty
            Dim i As New Integer
            Try

                'Deleting Old Report Row
                StrSql = String.Empty
                StrSql = "DELETE FROM USERREPORTROWS  "
                StrSql = StrSql + "WHERE USERREPORTID= " + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)

                'Updating Report
                StrSql = String.Empty
                StrSql = "UPDATE USERREPORTS  "
                StrSql = StrSql + "SET RPTTYPE= '" + ReportType + "', "
                StrSql = StrSql + " RPTTYPEDES= '" + ReportTypeDes + "', "
                StrSql = StrSql + " REGIONSETID= " + RegionSetId + ", "
                StrSql = StrSql + " REGIONID= " + RegionId + " "
                StrSql = StrSql + "WHERE USERREPORTID= " + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)

                'Adding Old Report Row
                For i = 1 To rowCount
                    StrSql = String.Empty
                    StrSql = "INSERT INTO USERREPORTROWS  "
                    StrSql = StrSql + "(USERREPORTROWID, USERREPORTID, ROWSEQUENCE) "
                    StrSql = StrSql + "VALUES "
                    StrSql = StrSql + "(SEQUSERREPORTROWID.NEXTVAL," + ReportId + "," + i.ToString() + ") "
                    odbUtil.UpIns(StrSql, Market1Connection)
                Next

            Catch ex As Exception
                Throw New Exception("M1UpInsData:AddReportRowsDetails:" + ex.Message.ToString())
            End Try

        End Sub
        Public Function AddRowDetails(ByVal ReportId As String, ByVal Seq As String, ByVal RowDesc As String, ByVal RowValuType As String, ByVal RowVal As String, ByVal Curr As String, ByVal Rowval1 As String, ByVal Rowval2 As String, ByVal rowValueTypeId As String, ByVal rowValueId As String) As Integer
            Dim ReportRowId As New Integer
            Try



                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New Selectdata()
                Dim StrSql As String = String.Empty
                Dim i As New Integer

                ReportRowId = ObjGetData.GetReportRowId().ToString()

                'Report Row Details
                StrSql = "INSERT INTO USERREPORTROWS  "
                StrSql = StrSql + "(USERREPORTROWID, USERREPORTID, ROWSEQUENCE, ROWDECRIPTION,ROWVALUETYPE, ROWVALUE,CURR,ROWVAL1,ROWVAL2,ROWTYPEID,ROWVALUEID) "
                StrSql = StrSql + "VALUES "
                StrSql = StrSql + "(" + ReportRowId.ToString() + "," + ReportId.ToString() + "," + Seq.ToString() + ",'" + RowDesc.ToString() + "','" + RowValuType + "', '" + RowVal.ToString() + "'," + Curr + ",'" + Rowval1 + "','" + Rowval2 + "'," + rowValueTypeId.ToString() + "," + rowValueId.ToString() + ") "
                odbUtil.UpIns(StrSql, Market1Connection)
                'ReportRowId = 238
                Return ReportRowId
            Catch ex As Exception
                Throw New Exception("M1UpInsData:AddRowDetails:" + ex.Message.ToString())
                Return ReportRowId
            End Try
        End Function
        Public Function AddColDetails(ByVal ReportId As String, ByVal Seq As String, ByVal ColType As String, ByVal ColVal As String, ByVal UDFV1 As String, ByVal UDFV2 As String, ByVal colTypeId As String, ByVal colValueId As String) As Integer
            Dim ReportColId As New Integer
            Try
                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New Selectdata()
                Dim StrSql As String = String.Empty
                Dim i As New Integer
                ReportColId = ObjGetData.GetReportColumnId().ToString()
                'Report Col Details
                StrSql = "INSERT INTO USERREPORTCOLUMNS  "
                StrSql = StrSql + "(USERREPORTCOLUMNID, USERREPORTID, COLUMNSEQUENCE, COLUMNVALUETYPE, COLUMNVALUE, INPUTVALUE1, INPUTVALUE2,COLUMNTYPEID,COLUMNVALUEID) "
                StrSql = StrSql + "VALUES "
                StrSql = StrSql + "(" + ReportColId.ToString() + "," + ReportId + "," + Seq + ",'" + ColType + "','" + ColVal + "','" + UDFV1 + "','" + UDFV2 + "'," + colTypeId + "," + colValueId + ") "
                odbUtil.UpIns(StrSql, Market1Connection)
                Return ReportColId
            Catch ex As Exception
                Throw New Exception("M1UpInsData:AddColDetails:" + ex.Message.ToString())
                Return ReportColId
            End Try

        End Function
        Public Function AddFilterDetails(ByVal ReportId As String, ByVal Seq As String, ByVal filterType As String, ByVal filterValue As String, ByVal filterTypeId As String, ByVal filterValueId As String) As Integer
            Dim ReportFilterId As New Integer
            Try
                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New Selectdata()
                Dim StrSql As String = String.Empty
                Dim i As New Integer
                ReportFilterId = ObjGetData.GetReportColumnId().ToString()
                'Report Col Details
                StrSql = "INSERT INTO USERREPORTFILTERS  "
                StrSql = StrSql + "(USERREPORTFILTERID, USERREPORTID, FILTERTYPE, FILTERVALUE,FILTERSEQUENCE, FILTERTYPEID,FILTERVALUEID) "
                StrSql = StrSql + "VALUES "
                StrSql = StrSql + "(" + ReportFilterId.ToString() + "," + ReportId.ToString() + ",'" + filterType.ToString() + "','" + filterValue.ToString() + "'," + Seq.ToString() + "," + filterTypeId.ToString() + "," + filterValueId + ")"
                odbUtil.UpIns(StrSql, Market1Connection)
                Return ReportFilterId
            Catch ex As Exception
                Throw New Exception("M1UpInsData:AddFilterDetails:" + ex.Message.ToString())
                Return ReportFilterId
            End Try

        End Function
        
        Public Sub UpdateColDetails(ByVal Seq As String, ByVal ColType As String, ByVal ColVal As String, ByVal UDFV1 As String, ByVal UDFV2 As String, ByVal ReportColId As String, ByVal colTypeId As String, ByVal colValueID As String, ByVal colCAGRYearId1 As String, ByVal colCAGRYearId2 As String, ByVal reportId As String)
            Try
                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New Selectdata()
                Dim StrSql As String = String.Empty
                Dim i As New Integer


                'Report Col Details
                StrSql = "Update USERREPORTCOLUMNS  "
                StrSql = StrSql + "Set COLUMNVALUETYPE='" + ColType + "', COLUMNVALUE='" + ColVal + "', INPUTVALUE1='" + UDFV1 + "', INPUTVALUE2='" + UDFV2 + "', COLUMNTYPEID=" + colTypeId + ", COLUMNVALUEID=" + colValueID + ", "
                StrSql = StrSql + " INPUTVALUETYPE1='" + colCAGRYearId1 + "' , INPUTVALUETYPE2='" + colCAGRYearId2 + "' "
                StrSql = StrSql + " Where USERREPORTCOLUMNID=" + ReportColId
                odbUtil.UpIns(StrSql, Market1Connection)

                If ColType = "Year" Then
                    'UPDATE THE DATA FOR CAGR
                    StrSql = ""
                    StrSql = "Update USERREPORTCOLUMNS  "
                    StrSql = StrSql + "SET INPUTVALUE1=" + ColVal
                    StrSql = StrSql + " WHERE USERREPORTID=" + reportId
                    StrSql = StrSql + " AND INPUTVALUETYPE1=" + ReportColId
                    odbUtil.UpIns(StrSql, Market1Connection)

                    StrSql = ""
                    StrSql = "Update USERREPORTCOLUMNS  "
                    StrSql = StrSql + "SET INPUTVALUE2=" + ColVal
                    StrSql = StrSql + " WHERE USERREPORTID=" + reportId
                    StrSql = StrSql + " AND INPUTVALUETYPE2=" + ReportColId
                    odbUtil.UpIns(StrSql, Market1Connection)
                End If
            Catch ex As Exception
                Throw New Exception("M1UpInsData:UpdateColDetails:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub UpdateRowDetails(ByVal RowDesc As String, ByVal RowVal As String, ByVal ReportRowId As String, ByVal RowValuType As String, ByVal Curr As String, ByVal Rowval1 As String, ByVal Rowval2 As String, ByVal rowTypeId As String, ByVal rowValueID As String, ByVal unitId As String)

            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New Selectdata()
                Dim StrSql As String = String.Empty
                Dim i As New Integer


                StrSql = "UPDATE USERREPORTROWS  "
                StrSql = StrSql + "SET ROWDECRIPTION='" + RowDesc.ToString() + "', ROWVALUE='" + RowVal.ToString() + "',ROWVALUETYPE='" + RowValuType + "', CURR=" + Curr + ", ROWVAL1='" + Rowval1 + "', ROWVAL2='" + Rowval2 + "', ROWTYPEID=" + rowTypeId + ", ROWVALUEID=" + rowValueID + ", "
                StrSql = StrSql + " UNITID=" + unitId.ToString()
                StrSql = StrSql + " WHERE USERREPORTROWID=" + ReportRowId
                odbUtil.UpIns(StrSql, Market1Connection)



            Catch ex As Exception
                Throw New Exception("M1UpInsData:UpdateRowDetails:" + ex.Message.ToString())

            End Try

        End Sub
        Public Sub UpdateFilterDetails(ByVal filterType As String, ByVal filterValue As String, ByVal filterTypeId As String, ByVal filterValueId As String, ByVal ReportFilterID As String)

            Try
                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New Selectdata()
                Dim StrSql As String = String.Empty
                Dim i As New Integer

                'Report Col Details
                StrSql = "UPDATE USERREPORTFILTERS  "

                StrSql = StrSql + " SET FILTERTYPE='" + filterType.ToString() + "', FILTERVALUE='" + filterValue.ToString() + "',FILTERTYPEID=" + filterTypeId.ToString() + ",FILTERVALUEID=" + filterValueId + ""
                StrSql = StrSql + " WHERE USERREPORTFILTERID=" + ReportFilterID
                odbUtil.UpIns(StrSql, Market1Connection)

            Catch ex As Exception
                Throw New Exception("M1UpInsData:UpdateFilterDetails:" + ex.Message.ToString())

            End Try

        End Sub
        Public Sub DeleteReport(ByVal ReportId As String)

            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New Selectdata()
                Dim StrSql As String = String.Empty
                Dim i As New Integer

                'Report Details
                StrSql = String.Empty
                StrSql = "DELETE FROM USERREPORTS  "
                StrSql = StrSql + "WHERE USERREPORTID =" + ReportId.ToString() + ""
                odbUtil.UpIns(StrSql, Market1Connection)

                'Report Rows Details
                StrSql = String.Empty
                StrSql = "DELETE FROM USERREPORTROWS  "
                StrSql = StrSql + "WHERE USERREPORTID =" + ReportId.ToString() + ""
                odbUtil.UpIns(StrSql, Market1Connection)

                'Report Cols. Details
                StrSql = String.Empty
                StrSql = "DELETE FROM USERREPORTCOLUMNS  "
                StrSql = StrSql + "WHERE USERREPORTID =" + ReportId.ToString() + ""
                odbUtil.UpIns(StrSql, Market1Connection)

                'Report Filter Details
                StrSql = String.Empty
                StrSql = "DELETE FROM USERREPORTFILTERS  "
                StrSql = StrSql + "WHERE USERREPORTID =" + ReportId.ToString() + ""
                odbUtil.UpIns(StrSql, Market1Connection)


            Catch ex As Exception
                Throw New Exception("M1UpInsData:DeleteReports:" + ex.Message.ToString())

            End Try

        End Sub

        
        'Public Function CreatACopyReport(ByVal ReportId As String, ByVal CopyFrom As String, ByVal userID As String) As String

        '    'Creating Database Connection
        '    Dim odbUtil As New DBUtil()
        '    Dim ObjGetData As New Selectdata()
        '    Dim StrSql As String = String.Empty
        '    Dim i As New Integer
        '    Dim NewReportId As String = String.Empty

        '    Try
        '        NewReportId = ObjGetData.GetReportId()


        '        If CopyFrom = "Base" Then
        '            'Report Details
        '            StrSql = String.Empty
        '            StrSql = "INSERT INTO USERREPORTS  "
        '            StrSql = StrSql + "(	   USERREPORTID, "
        '            StrSql = StrSql + "REPORTNAME, "
        '            StrSql = StrSql + "USERID, "
        '            StrSql = StrSql + "RPTTYPE, "
        '            StrSql = StrSql + "RPTFACT, "
        '            StrSql = StrSql + "RPTTYPEDES, "
        '            StrSql = StrSql + "REGIONSETID, "
        '            StrSql = StrSql + "REGIONID, "
        '            StrSql = StrSql + "CREATEDDATE "
        '            StrSql = StrSql + ") "
        '            StrSql = StrSql + "SELECT " + NewReportId + ", "
        '            StrSql = StrSql + "REPORTNAME, "
        '            StrSql = StrSql + "" + userID + ", "
        '            StrSql = StrSql + "RPTTYPE, "
        '            StrSql = StrSql + "RPTFACT, "
        '            StrSql = StrSql + "RPTTYPEDES, "
        '            StrSql = StrSql + "REGIONSETID, "
        '            StrSql = StrSql + "REGIONID, "
        '            StrSql = StrSql + "SYSDATE "
        '            StrSql = StrSql + "FROM BASEREPORTS "
        '            StrSql = StrSql + "WHERE BASEREPORTID = " + ReportId + " "
        '            odbUtil.UpIns(StrSql, Market1Connection)
        '        ElseIf CopyFrom = "Prop" Then
        '            'Report Details
        '            StrSql = String.Empty
        '            StrSql = "INSERT INTO USERREPORTS  "
        '            StrSql = StrSql + "(	   USERREPORTID, "
        '            StrSql = StrSql + "REPORTNAME, "
        '            StrSql = StrSql + "USERID, "
        '            StrSql = StrSql + "RPTTYPE, "
        '            StrSql = StrSql + "RPTFACT, "
        '            StrSql = StrSql + "RPTTYPEDES, "
        '            StrSql = StrSql + "REGIONSETID, "
        '            StrSql = StrSql + "REGIONID, "
        '            StrSql = StrSql + "CREATEDDATE "
        '            StrSql = StrSql + ") "
        '            StrSql = StrSql + "SELECT " + NewReportId + ", "
        '            StrSql = StrSql + "REPORTNAME, "
        '            StrSql = StrSql + "USERID, "
        '            StrSql = StrSql + "RPTTYPE, "
        '            StrSql = StrSql + "RPTFACT, "
        '            StrSql = StrSql + "RPTTYPEDES, "
        '            StrSql = StrSql + "REGIONSETID, "
        '            StrSql = StrSql + "REGIONID, "
        '            StrSql = StrSql + "SYSDATE "
        '            StrSql = StrSql + "FROM USERREPORTS "
        '            StrSql = StrSql + "WHERE USERREPORTID = " + ReportId + " "
        '            odbUtil.UpIns(StrSql, Market1Connection)
        '        End If


        '        'Report Rows Details
        '        StrSql = String.Empty
        '        StrSql = "INSERT INTO USERREPORTROWS  "
        '        StrSql = StrSql + "(	   USERREPORTROWID, "
        '        StrSql = StrSql + "USERREPORTID, "
        '        StrSql = StrSql + "ROWSEQUENCE, "
        '        StrSql = StrSql + "ROWDECRIPTION, "
        '        StrSql = StrSql + "ROWVALUETYPE, "
        '        StrSql = StrSql + "ROWVALUE, "
        '        StrSql = StrSql + "ROWTYPEID, "
        '        StrSql = StrSql + "ROWVALUEID, "
        '        StrSql = StrSql + "UNITID, "
        '        StrSql = StrSql + "CURR "
        '        StrSql = StrSql + ") "
        '        StrSql = StrSql + "SELECT SEQUSERREPORTROWID.NEXTVAL, "
        '        StrSql = StrSql + " " + NewReportId + ", "
        '        StrSql = StrSql + "ROWSEQUENCE, "
        '        StrSql = StrSql + "ROWDECRIPTION, "
        '        StrSql = StrSql + "ROWVALUETYPE, "
        '        StrSql = StrSql + "ROWVALUE, "
        '        StrSql = StrSql + "ROWTYPEID, "
        '        StrSql = StrSql + "ROWVALUEID, "
        '        StrSql = StrSql + "UNITID, "
        '        StrSql = StrSql + "CURR "
        '        StrSql = StrSql + "FROM USERREPORTROWS "
        '        StrSql = StrSql + "WHERE USERREPORTID = " + ReportId + " "
        '        odbUtil.UpIns(StrSql, Market1Connection)

        '        'Report Cols. Details
        '        StrSql = String.Empty
        '        StrSql = "INSERT INTO USERREPORTCOLUMNS  "
        '        StrSql = StrSql + "( "
        '        StrSql = StrSql + "USERREPORTCOLUMNID, "
        '        StrSql = StrSql + "USERREPORTID, "
        '        StrSql = StrSql + "COLUMNSEQUENCE, "
        '        StrSql = StrSql + "COLUMNDECRIPTION, "
        '        StrSql = StrSql + "COLUMNVALUETYPE, "
        '        StrSql = StrSql + "COLUMNVALUE, "
        '        StrSql = StrSql + "INPUTVALUETYPE1, "
        '        StrSql = StrSql + "INPUTVALUE1, "
        '        StrSql = StrSql + "INPUTVALUETYPE2, "
        '        StrSql = StrSql + "INPUTVALUE2, "
        '        StrSql = StrSql + "INPUTVALUETYPE3, "
        '        StrSql = StrSql + "INPUTVALUE3, "
        '        StrSql = StrSql + "INPUTVALUETYPE4, "
        '        StrSql = StrSql + "INPUTVALUE4, "
        '        StrSql = StrSql + "COLUMNTYPEID, "
        '        StrSql = StrSql + "COLUMNVALUEID "
        '        StrSql = StrSql + ") "
        '        StrSql = StrSql + "SELECT SEQUSERREPORTCOLUMNID.NEXTVAL, "
        '        StrSql = StrSql + " " + NewReportId + ", "
        '        StrSql = StrSql + "COLUMNSEQUENCE, "
        '        StrSql = StrSql + "COLUMNDECRIPTION, "
        '        StrSql = StrSql + "COLUMNVALUETYPE, "
        '        StrSql = StrSql + "COLUMNVALUE, "
        '        StrSql = StrSql + "INPUTVALUETYPE1, "
        '        StrSql = StrSql + "INPUTVALUE1, "
        '        StrSql = StrSql + "INPUTVALUETYPE2, "
        '        StrSql = StrSql + "INPUTVALUE2, "
        '        StrSql = StrSql + "INPUTVALUETYPE3, "
        '        StrSql = StrSql + "INPUTVALUE3, "
        '        StrSql = StrSql + "INPUTVALUETYPE4, "
        '        StrSql = StrSql + "INPUTVALUE4, "
        '        StrSql = StrSql + "COLUMNTYPEID, "
        '        StrSql = StrSql + "COLUMNVALUEID "
        '        StrSql = StrSql + "FROM USERREPORTCOLUMNS "
        '        StrSql = StrSql + "WHERE USERREPORTID = " + ReportId.ToString() + " "
        '        odbUtil.UpIns(StrSql, Market1Connection)

        '        'Report Filter Details
        '        StrSql = String.Empty
        '        StrSql = "INSERT INTO USERREPORTFILTERS  "
        '        StrSql = StrSql + "( "
        '        StrSql = StrSql + "USERREPORTFILTERID, "
        '        StrSql = StrSql + "FILTERSEQUENCE, "
        '        StrSql = StrSql + "USERREPORTID, "
        '        StrSql = StrSql + "FILTERTYPE, "
        '        StrSql = StrSql + "FILTERVALUE, "
        '        StrSql = StrSql + "FILTERTYPEID, "
        '        StrSql = StrSql + "FILTERVALUEID "
        '        StrSql = StrSql + ") "
        '        StrSql = StrSql + "SELECT SEQUSERREPORTFILTERID.NEXTVAL, "
        '        StrSql = StrSql + "FILTERSEQUENCE, "
        '        StrSql = StrSql + " " + NewReportId + ", "
        '        StrSql = StrSql + "FILTERTYPE, "
        '        StrSql = StrSql + "FILTERVALUE ,"
        '        StrSql = StrSql + "FILTERTYPEID, "
        '        StrSql = StrSql + "FILTERVALUEID "
        '        StrSql = StrSql + "FROM USERREPORTFILTERS "
        '        StrSql = StrSql + "WHERE USERREPORTID = " + ReportId.ToString() + " "
        '        odbUtil.UpIns(StrSql, Market1Connection)


        '    Catch ex As Exception
        '        Throw New Exception("M1UpInsData:CreatACopyReport:" + ex.Message.ToString())
        '    End Try
        '    Return NewReportId
        'End Function

        Public Function CreatACopyReport(ByVal ReportId As String, ByVal CopyFrom As String, ByVal userID As String) As String

            'Creating Database Connection
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty
            Dim i As New Integer
            Dim NewReportId As String = String.Empty

            Try
                NewReportId = ObjGetData.GetReportId()


                If CopyFrom = "Base" Then
                    'Report Details
                    StrSql = String.Empty
                    StrSql = "INSERT INTO USERREPORTS  "
                    StrSql = StrSql + "(	   USERREPORTID, "
                    StrSql = StrSql + "REPORTNAME, "
                    StrSql = StrSql + "USERID, "
                    StrSql = StrSql + "RPTTYPE, "
                    StrSql = StrSql + "RPTFACT, "
                    StrSql = StrSql + "RPTTYPEDES, "
                    StrSql = StrSql + "REGIONSETID, "
                    StrSql = StrSql + "REGIONID, "
                    StrSql = StrSql + "CREATEDDATE "
                    StrSql = StrSql + ") "
                    StrSql = StrSql + "SELECT " + NewReportId + ", "
                    StrSql = StrSql + "REPORTNAME, "
                    StrSql = StrSql + "" + userID + ", "
                    StrSql = StrSql + "RPTTYPE, "
                    StrSql = StrSql + "RPTFACT, "
                    StrSql = StrSql + "RPTTYPEDES, "
                    StrSql = StrSql + "REGIONSETID, "
                    StrSql = StrSql + "REGIONID, "
                    StrSql = StrSql + "SYSDATE "
                    StrSql = StrSql + "FROM BASEREPORTS "
                    StrSql = StrSql + "WHERE BASEREPORTID = " + ReportId + " "
                    odbUtil.UpIns(StrSql, Market1Connection)
                ElseIf CopyFrom = "Prop" Then
                    'Report Details
                    StrSql = String.Empty
                    StrSql = "INSERT INTO USERREPORTS  "
                    StrSql = StrSql + "(	   USERREPORTID, "
                    StrSql = StrSql + "REPORTNAME, "
                    StrSql = StrSql + "USERID, "
                    StrSql = StrSql + "RPTTYPE, "
                    StrSql = StrSql + "RPTFACT, "
                    StrSql = StrSql + "RPTTYPEDES, "
                    StrSql = StrSql + "REGIONSETID, "
                    StrSql = StrSql + "REGIONID, "
                    StrSql = StrSql + "CREATEDDATE "
                    StrSql = StrSql + ") "
                    StrSql = StrSql + "SELECT " + NewReportId + ", "
                    StrSql = StrSql + "REPORTNAME, "
                    StrSql = StrSql + "USERID, "
                    StrSql = StrSql + "RPTTYPE, "
                    StrSql = StrSql + "RPTFACT, "
                    StrSql = StrSql + "RPTTYPEDES, "
                    StrSql = StrSql + "REGIONSETID, "
                    StrSql = StrSql + "REGIONID, "
                    StrSql = StrSql + "SYSDATE "
                    StrSql = StrSql + "FROM USERREPORTS "
                    StrSql = StrSql + "WHERE USERREPORTID = " + ReportId + " "
                    odbUtil.UpIns(StrSql, Market1Connection)
                End If


                'Report Rows Details
                StrSql = String.Empty
                StrSql = "INSERT INTO USERREPORTROWS  "
                StrSql = StrSql + "(	   USERREPORTROWID, "
                StrSql = StrSql + "USERREPORTID, "
                StrSql = StrSql + "ROWSEQUENCE, "
                StrSql = StrSql + "ROWDECRIPTION, "
                StrSql = StrSql + "ROWVALUETYPE, "
                StrSql = StrSql + "ROWVALUE, "
                StrSql = StrSql + "ROWTYPEID, "
                StrSql = StrSql + "ROWVALUEID, "
                StrSql = StrSql + "UNITID, "
                StrSql = StrSql + "CURR "
                StrSql = StrSql + ") "
                StrSql = StrSql + "SELECT SEQUSERREPORTROWID.NEXTVAL, "
                StrSql = StrSql + " " + NewReportId + ", "
                StrSql = StrSql + "ROWSEQUENCE, "
                StrSql = StrSql + "ROWDECRIPTION, "
                StrSql = StrSql + "ROWVALUETYPE, "
                StrSql = StrSql + "ROWVALUE, "
                StrSql = StrSql + "ROWTYPEID, "
                StrSql = StrSql + "ROWVALUEID, "
                StrSql = StrSql + "UNITID, "
                StrSql = StrSql + "CURR "
                StrSql = StrSql + "FROM USERREPORTROWS "
                StrSql = StrSql + "WHERE USERREPORTID = " + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)

                'Report Cols. Details
                StrSql = String.Empty
                StrSql = "INSERT INTO USERREPORTCOLUMNS  "
                StrSql = StrSql + "( "
                StrSql = StrSql + "USERREPORTCOLUMNID, "
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
                StrSql = StrSql + "INPUTVALUE4, "
                StrSql = StrSql + "COLUMNTYPEID, "
                StrSql = StrSql + "COLUMNVALUEID "
                StrSql = StrSql + ") "
                StrSql = StrSql + "SELECT SEQUSERREPORTCOLUMNID.NEXTVAL, "
                StrSql = StrSql + " " + NewReportId + ", "
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
                StrSql = StrSql + "INPUTVALUE4, "
                StrSql = StrSql + "COLUMNTYPEID, "
                StrSql = StrSql + "COLUMNVALUEID "
                StrSql = StrSql + "FROM USERREPORTCOLUMNS "
                StrSql = StrSql + "WHERE USERREPORTID = " + ReportId.ToString() + " "
                odbUtil.UpIns(StrSql, Market1Connection)

                'Report Filter Details
                StrSql = String.Empty
                StrSql = "INSERT INTO USERREPORTFILTERS  "
                StrSql = StrSql + "( "
                StrSql = StrSql + "USERREPORTFILTERID, "
                StrSql = StrSql + "FILTERSEQUENCE, "
                StrSql = StrSql + "USERREPORTID, "
                StrSql = StrSql + "FILTERTYPE, "
                StrSql = StrSql + "FILTERVALUE, "
                StrSql = StrSql + "FILTERTYPEID, "
                StrSql = StrSql + "FILTERVALUEID "
                StrSql = StrSql + ") "
                StrSql = StrSql + "SELECT SEQUSERREPORTFILTERID.NEXTVAL, "
                StrSql = StrSql + "FILTERSEQUENCE, "
                StrSql = StrSql + " " + NewReportId + ", "
                StrSql = StrSql + "FILTERTYPE, "
                StrSql = StrSql + "FILTERVALUE ,"
                StrSql = StrSql + "FILTERTYPEID, "
                StrSql = StrSql + "FILTERVALUEID "
                StrSql = StrSql + "FROM USERREPORTFILTERS "
                StrSql = StrSql + "WHERE USERREPORTID = " + ReportId.ToString() + " "
                odbUtil.UpIns(StrSql, Market1Connection)


                Dim ds As New DataSet
                ds = ObjGetData.GetUsersReportCAGRColumns(ReportId.ToString())
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    StrSql = "UPDATE USERREPORTCOLUMNS SET INPUTVALUETYPE1=  "
                    StrSql = StrSql + "(SELECT USERREPORTCOLUMNID FROM USERREPORTCOLUMNS WHERE COLUMNSEQUENCE=( "
                    StrSql = StrSql + "SELECT COLUMNSEQUENCE FROM USERREPORTCOLUMNS WHERE USERREPORTCOLUMNID=" + ds.Tables(0).Rows(i)("INPUTVALUETYPE1").ToString() + " ) "
                    StrSql = StrSql + "AND USERREPORTID=" + NewReportId.ToString() + ") "
                    StrSql = StrSql + "WHERE USERREPORTID=" + NewReportId.ToString() + " "
                    StrSql = StrSql + "AND COLUMNSEQUENCE=" + ds.Tables(0).Rows(i)("COLUMNSEQUENCE").ToString() + " "
                    odbUtil.UpIns(StrSql, Market1Connection)

                    StrSql = "UPDATE USERREPORTCOLUMNS SET INPUTVALUETYPE2=  "
                    StrSql = StrSql + "(SELECT USERREPORTCOLUMNID FROM USERREPORTCOLUMNS WHERE COLUMNSEQUENCE=( "
                    StrSql = StrSql + "SELECT COLUMNSEQUENCE FROM USERREPORTCOLUMNS WHERE USERREPORTCOLUMNID=" + ds.Tables(0).Rows(i)("INPUTVALUETYPE2").ToString() + " ) "
                    StrSql = StrSql + "AND USERREPORTID=" + NewReportId.ToString() + ") "
                    StrSql = StrSql + "WHERE USERREPORTID=" + NewReportId.ToString() + " "
                    StrSql = StrSql + "AND COLUMNSEQUENCE=" + ds.Tables(0).Rows(i)("COLUMNSEQUENCE").ToString() + " "
                    odbUtil.UpIns(StrSql, Market1Connection)
                Next

            Catch ex As Exception
                Throw New Exception("M1UpInsData:CreatACopyReport:" + ex.Message.ToString())
            End Try
            Return NewReportId
        End Function
        Public Sub EditReport(ByVal ReportType As String, ByVal ReportId As String, ByVal ReportName As String, ByVal FilterType As String, ByVal FilterValue As String, ByVal ReportFacts As String, ByVal RowValue As String, ByVal Curr As String)

            'Creating Database Connection
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty
            Dim i As New Integer

            Try

                If ReportType = "2D" Then
                    'Report Details
                    StrSql = String.Empty
                    StrSql = "UPDATE USERREPORTS  "
                    StrSql = StrSql + "SET USERREPORTS.REPORTNAME = '" + ReportName + "', "
                    StrSql = StrSql + "SET USERREPORTS.REPORTFACTS = '" + ReportFacts + "' "
                    StrSql = StrSql + "WHERE USERREPORTS.USERREPORTID=" + ReportId + " "
                    odbUtil.UpIns(StrSql, Market1Connection)

                    'Report Rows
                    StrSql = String.Empty
                    StrSql = "UPDATE USERREPORTROWS  "
                    StrSql = StrSql + "SET USERREPORTROWS.ROWVALUE='" + RowValue + "', "
                    StrSql = StrSql + "USERREPORTROWS.CURR=" + Curr + " "
                    StrSql = StrSql + "WHERE USERREPORTID=" + ReportId + " "
                    odbUtil.UpIns(StrSql, Market1Connection)
                Else
                    'Report Details
                    StrSql = String.Empty
                    StrSql = "UPDATE USERREPORTS  "
                    StrSql = StrSql + "SET USERREPORTS.REPORTNAME = '" + ReportName + "' "
                    StrSql = StrSql + "WHERE USERREPORTS.USERREPORTID=" + ReportId + " "
                    odbUtil.UpIns(StrSql, Market1Connection)

                End If





            Catch ex As Exception
                Throw New Exception("M1UpInsData:CreatACopyReport:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub RenameReport(ByVal ReportId As String, ByVal ReportName As String)

            'Creating Database Connection
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty
            Dim i As New Integer
            Try


                'Report Details
                StrSql = String.Empty
                StrSql = "UPDATE USERREPORTS  "
                StrSql = StrSql + "SET USERREPORTS.REPORTNAME = '" + ReportName + "' "
                StrSql = StrSql + "WHERE USERREPORTS.USERREPORTID=" + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)

            Catch ex As Exception
                Throw New Exception("M1UpInsData:CreatACopyReport:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub EditReportTypeDes(ByVal ReportId As String, ByVal ReportTypeDes As String)
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty

            Try

                If ReportId > 1000 Then
                    'Report Details
                    StrSql = String.Empty
                    StrSql = "UPDATE USERREPORTS  "
                    StrSql = StrSql + "SET USERREPORTS.RPTTYPEDES = '" + ReportTypeDes + "' "
                    StrSql = StrSql + "WHERE USERREPORTS.USERREPORTID=" + ReportId + " "
                    odbUtil.UpIns(StrSql, Market1Connection)
                Else
                    StrSql = String.Empty
                    StrSql = "UPDATE BASEREPORTS  "
                    StrSql = StrSql + "SET BASEREPORTS.RPTTYPEDES = '" + ReportTypeDes + "' "
                    StrSql = StrSql + "WHERE BASEREPORTS.BASEREPORTID=" + ReportId + " "
                    odbUtil.UpIns(StrSql, Market1Connection)
                End If


            Catch ex As Exception
                Throw New Exception("M1UpInsData:CreatACopyReport:" + ex.Message.ToString())
            End Try

        End Sub

#End Region

#Region "Country Region"
        Public Sub InsertUpdateCountryRegions(ByVal RegionId As String, ByVal RegionName As String, ByVal CountryIds As String, ByVal UserId As String, ByVal IsInsert As Boolean)
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty
            Dim i As New Integer
            Dim NewRegionId As New Integer
            Try



                'Region
                If IsInsert Then
                    NewRegionId = ObjGetData.GetRegionId()
                    StrSql = "INSERT INTO USERREGIONS  "
                    StrSql = StrSql + "(REGIONID,REGRIONNAME,USERID) "
                    StrSql = StrSql + "SELECT " + NewRegionId.ToString() + ", "
                    StrSql = StrSql + "'" + RegionName + "', "
                    StrSql = StrSql + "" + UserId + " "
                    StrSql = StrSql + "FROM DUAL "
                    RegionId = NewRegionId.ToString()
                Else
                    StrSql = "UPDATE USERREGIONS  "
                    StrSql = StrSql + "SET REGRIONNAME = '" + RegionName + "' "
                    StrSql = StrSql + "WHERE USERID=" + UserId + " "
                    StrSql = StrSql + "AND REGIONID=" + RegionId + " "
                End If

                odbUtil.UpIns(StrSql, Market1Connection)


                'Delete Region Countries
                StrSql = String.Empty
                StrSql = "DELETE FROM USERCOUNTRYREGIONS  "
                StrSql = StrSql + "WHERE USERID = " + UserId + ""
                StrSql = StrSql + "AND REGIONID = " + RegionId + ""
                odbUtil.UpIns(StrSql, Market1Connection)

                'Insert Region Countries
                StrSql = String.Empty
                StrSql = "INSERT INTO USERCOUNTRYREGIONS  "
                StrSql = StrSql + "(COUNTRYREGIONID, REGIONID, COUNTRYID, USERID) "
                StrSql = StrSql + "SELECT SEQCOUNTRYREGIONID.NEXTVAL, "
                StrSql = StrSql + "" + RegionId + ", "
                StrSql = StrSql + "DIMCOUNTRIES.COUNTRYID, "
                StrSql = StrSql + "" + UserId + " "
                StrSql = StrSql + "FROM DIMCOUNTRIES "
                StrSql = StrSql + "WHERE DIMCOUNTRIES.COUNTRYID IN (" + CountryIds + ") "
                odbUtil.UpIns(StrSql, Market1Connection)


            Catch ex As Exception
                Throw New Exception("M1UpInsData:DeleteReports:" + ex.Message.ToString())
            End Try

        End Sub
#End Region


    End Class
End Class
