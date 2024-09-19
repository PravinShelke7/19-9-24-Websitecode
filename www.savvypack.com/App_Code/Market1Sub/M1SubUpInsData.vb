Imports Microsoft.VisualBasic
Imports System.Web.SessionState
Imports System.Data
Imports System.Data.OleDb
Imports System
Imports M1SubGetData

Public Class M1SubUpInsData
    Public Class UpdateInsert
        Dim Market1Connection As String = System.Configuration.ConfigurationManager.AppSettings("Market1ConnectionString")
        Dim EconConnection As String = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")


#Region "Reports"

        Public Function AddReportDetails_old(ByVal ReportName As String, ByVal ColCnt As String, ByVal RowCnt As String, ByVal UserId As String, ByVal FilterValue As String, ByVal FilterType As String, ByVal ReportType As String, ByVal ReportFact As String) As Integer
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
                StrSql = StrSql + "(USERREPORTID, REPORTNAME, USERID, CREATEDDATE,RPTTYPE,RPTFACT) "
                StrSql = StrSql + "VALUES "
                StrSql = StrSql + "(" + ReportId + ",'" + ReportName + "'," + UserId + ",SYSDATE,'" + ReportType + "','" + ReportFact + "') "
                odbUtil.UpIns(StrSql, Market1Connection)

                'Filter Type Details
                StrSql = String.Empty
                Dim valFilterType As String
                valFilterType = FilterType
                StrSql = String.Empty
                StrSql = "INSERT INTO USERREPORTFILTERS  "
                StrSql = StrSql + "(USERREPORTFILTERID,USERREPORTID, FILTERTYPE,FILTERVALUE) "
                StrSql = StrSql + "VALUES "
                StrSql = StrSql + "(" + ReportFilterId.ToString() + "," + ReportId + ",'" + valFilterType.ToString() + "','" + FilterValue.ToString() + "') "
                odbUtil.UpIns(StrSql, Market1Connection)



                'Report Col. Details
                For i = 1 To ColCnt
                    StrSql = String.Empty
                    StrSql = "INSERT INTO USERREPORTCOLUMNS  "
                    StrSql = StrSql + "(USERREPORTCOLUMNID, USERREPORTID, COLUMNSEQUENCE) "
                    StrSql = StrSql + "VALUES "
                    StrSql = StrSql + "(SEQUSERREPORTCOLUMNID.NEXTVAL," + ReportId + "," + i.ToString() + ") "
                    'odbUtil.UpIns(StrSql, Market1Connection)
                Next

                'Report Row. Details
                For i = 1 To RowCnt
                    StrSql = String.Empty
                    StrSql = "INSERT INTO USERREPORTROWS  "
                    StrSql = StrSql + "(USERREPORTROWID, USERREPORTID, ROWSEQUENCE) "
                    StrSql = StrSql + "VALUES "
                    StrSql = StrSql + "(SEQUSERREPORTROWID.NEXTVAL," + ReportId + "," + i.ToString() + ") "
                    'odbUtil.UpIns(StrSql, Market1Connection)
                Next
                Return ReportId
            Catch ex As Exception
                Throw New Exception("M1UpInsData:AddReport:" + ex.Message.ToString())
            End Try

        End Function
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

        Public Function AddReportRowsDetails(ByVal ReportId As String, ByVal rowCount As String, ByVal ReportType As String, ByVal ReportTypeDes As String, ByVal RegionSetId As String, ByVal RegionId As String)
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

        End Function

        Public Function AddColDetails_old(ByVal ReportId As String, ByVal Seq As String, ByVal ColType As String, ByVal ColVal As String, ByVal UDFV1 As String, ByVal UDFV2 As String) As Integer
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
                StrSql = StrSql + "(USERREPORTCOLUMNID, USERREPORTID, COLUMNSEQUENCE, COLUMNVALUETYPE, COLUMNVALUE, INPUTVALUE1, INPUTVALUE2) "
                StrSql = StrSql + "VALUES "
                StrSql = StrSql + "(" + ReportColId.ToString() + "," + ReportId + "," + Seq + ",'" + ColType + "','" + ColVal + "','" + UDFV1 + "','" + UDFV2 + "') "
                odbUtil.UpIns(StrSql, Market1Connection)
                Return ReportColId



            Catch ex As Exception
                Throw New Exception("M1UpInsData:AddColDetails:" + ex.Message.ToString())
                Return ReportColId
            End Try

        End Function

        Public Sub UpdateColDetails_old(ByVal Seq As String, ByVal ColType As String, ByVal ColVal As String, ByVal UDFV1 As String, ByVal UDFV2 As String, ByVal ReportColId As String)
            Try
                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New Selectdata()
                Dim StrSql As String = String.Empty
                Dim i As New Integer
                Dim ColId As New Integer
                ColId = ReportColId

                'Report Col Details
                StrSql = "Update USERREPORTCOLUMNS  "
                StrSql = StrSql + "Set COLUMNVALUETYPE='" + ColType + "', COLUMNVALUE='" + ColVal + "', INPUTVALUE1='" + UDFV1 + "', INPUTVALUE2='" + UDFV2 + "'"
                StrSql = StrSql + " Where USERREPORTCOLUMNID=" + ReportColId
                odbUtil.UpIns(StrSql, Market1Connection)
            Catch ex As Exception
                Throw New Exception("M1UpInsData:AddColDetails:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub UpdateRowDetails_old(ByVal RowDesc As String, ByVal RowVal As String, ByVal ReportRowId As String, ByVal RowValuType As String, ByVal Curr As String, ByVal Rowval1 As String, ByVal Rowval2 As String)

            Try

                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New Selectdata()
                Dim StrSql As String = String.Empty
                Dim i As New Integer


                StrSql = "UPDATE USERREPORTROWS  "
                StrSql = StrSql + "SET ROWDECRIPTION='" + RowDesc.ToString() + "', ROWVALUE='" + RowVal.ToString() + "',ROWVALUETYPE='" + RowValuType + "', CURR=" + Curr + ", ROWVAL1='" + Rowval1 + "', ROWVAL2='" + Rowval2 + "' "
                StrSql = StrSql + " WHERE USERREPORTROWID=" + ReportRowId
                odbUtil.UpIns(StrSql, Market1Connection)



            Catch ex As Exception
                Throw New Exception("M1UpInsData:UpdateRowDetails:" + ex.Message.ToString())

            End Try

        End Sub
        Public Function AddRowDetails_old(ByVal ReportId As String, ByVal Seq As String, ByVal RowDesc As String, ByVal RowValuType As String, ByVal RowVal As String, ByVal Curr As String, ByVal Rowval1 As String, ByVal Rowval2 As String) As Integer
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
                StrSql = StrSql + "(USERREPORTROWID, USERREPORTID, ROWSEQUENCE, ROWDECRIPTION,ROWVALUETYPE, ROWVALUE,CURR,ROWVAL1,ROWVAL2) "
                StrSql = StrSql + "VALUES "
                StrSql = StrSql + "(" + ReportRowId.ToString() + "," + ReportId.ToString() + "," + Seq.ToString() + ",'" + RowDesc.ToString() + "','" + RowValuType + "', '" + RowVal.ToString() + "'," + Curr + ",'" + Rowval1 + "','" + Rowval2 + "') "
                odbUtil.UpIns(StrSql, Market1Connection)
                Return ReportRowId
            Catch ex As Exception
                Throw New Exception("M1UpInsData:AddRowDetails:" + ex.Message.ToString())
                Return ReportRowId
            End Try

        End Function


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
        Public Sub UpdateRowDetails(ByVal RowDesc As String, ByVal RowVal As String, ByVal ReportRowId As String, ByVal RowValuType As String, ByVal Curr As String, ByVal Rowval1 As String, ByVal Rowval2 As String, ByVal rowTypeId As String, ByVal rowValueID As String, ByVal unitId As String, ByVal reportId As String)
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

                If RowValuType <> "Formula" Then
                    'UPDATE THE DATA FOR CAGR
                    StrSql = ""
                    StrSql = "Update USERREPORTROWS  "
                    StrSql = StrSql + "SET ROWVAL1='" + RowVal + "', UNITID1=" + unitId + " "
                    StrSql = StrSql + " WHERE USERREPORTID=" + reportId
                    StrSql = StrSql + " AND ROWVALID1=" + ReportRowId
                    odbUtil.UpIns(StrSql, Market1Connection)

                    StrSql = ""
                    StrSql = "Update USERREPORTROWS  "
                    StrSql = StrSql + "SET ROWVAL2='" + RowVal + "', UNITID2=" + unitId + " "
                    StrSql = StrSql + " WHERE USERREPORTID=" + reportId
                    StrSql = StrSql + " AND ROWVALID2=" + ReportRowId
                    odbUtil.UpIns(StrSql, Market1Connection)
                End If

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

               'Report Group Detail
                StrSql = String.Empty
                StrSql = "DELETE FROM GROUPREPORTS "
                StrSql = StrSql + "WHERE REPORTID =" + ReportId.ToString() + ""
                odbUtil.UpIns(StrSql, Market1Connection)

            Catch ex As Exception
                Throw New Exception("M1UpInsData:DeleteReports:" + ex.Message.ToString())

            End Try

        End Sub

        Public Function CreatACopyReport(ByVal ReportId As String, ByVal CopyFrom As String, ByVal userID As String, ByVal ServiceId As String) As String

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
                    StrSql = StrSql + "SERVICEID, "
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
                    StrSql = StrSql + "" + ServiceId + ", "
                    StrSql = StrSql + "SYSDATE "
                    StrSql = StrSql + "FROM USERREPORTS "
                    StrSql = StrSql + "WHERE USERREPORTID = " + ReportId + " "
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
                    StrSql = StrSql + "SERVICEID, "
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
                    StrSql = StrSql + "" + ServiceId + ", "
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
                StrSql = StrSql + "CURR, "
                StrSql = StrSql + "ROWVAL1, "
                StrSql = StrSql + "ROWVAL2, "
                StrSql = StrSql + "ROWVALID1, "
                StrSql = StrSql + "ROWVALID2, "
                StrSql = StrSql + "UNITID1, "
                StrSql = StrSql + "UNITID2 "
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
                StrSql = StrSql + "CURR, "
                StrSql = StrSql + "ROWVAL1, "
                StrSql = StrSql + "ROWVAL2, "
                StrSql = StrSql + "ROWVALID1, "
                StrSql = StrSql + "ROWVALID2, "
                StrSql = StrSql + "UNITID1, "
                StrSql = StrSql + "UNITID2 "
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

                ''StrSql = "UPDATE USERREPORTCOLUMNS SET  INPUTVALUETYPE2= (SELECT USERREPORTCOLUMNID FROM USERREPORTCOLUMNS  "
                ''StrSql = StrSql + "WHERE COLUMNSEQUENCE=( "
                ''StrSql = StrSql + "SELECT COLUMNSEQUENCE FROM USERREPORTCOLUMNS "
                ''StrSql = StrSql + "WHERE USERREPORTCOLUMNID=( "
                ''StrSql = StrSql + "SELECT INPUTVALUETYPE2 FROM USERREPORTCOLUMNS "
                ''StrSql = StrSql + "WHERE COLUMNTYPEID=2 AND USERREPORTID=" + NewReportId.ToString() + " )) "
                ''StrSql = StrSql + "AND USERREPORTID=" + NewReportId.ToString() + " ) "
                ''StrSql = StrSql + "WHERE USERREPORTID=" + NewReportId.ToString() + " AND COLUMNTYPEID=2 "
                ''odbUtil.UpIns(StrSql, Market1Connection)

                ''StrSql = "UPDATE USERREPORTCOLUMNS SET  INPUTVALUETYPE1= (SELECT USERREPORTCOLUMNID FROM USERREPORTCOLUMNS  "
                ''StrSql = StrSql + "WHERE COLUMNSEQUENCE=( "
                ''StrSql = StrSql + "SELECT COLUMNSEQUENCE FROM USERREPORTCOLUMNS "
                ''StrSql = StrSql + "WHERE USERREPORTCOLUMNID=( "
                ''StrSql = StrSql + "SELECT INPUTVALUETYPE1 FROM USERREPORTCOLUMNS "
                ''StrSql = StrSql + "WHERE COLUMNTYPEID=2 AND USERREPORTID=" + NewReportId.ToString() + " )) "
                ''StrSql = StrSql + "AND USERREPORTID=" + NewReportId.ToString() + " ) "
                ''StrSql = StrSql + "WHERE USERREPORTID=" + NewReportId.ToString() + " AND COLUMNTYPEID=2 "
                ''odbUtil.UpIns(StrSql, Market1Connection)

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

                Dim dsCap As New DataSet
                dsCap = ObjGetData.GetUsersReportCapitaRows(ReportId.ToString())
                For i = 0 To dsCap.Tables(0).Rows.Count - 1
                    StrSql = "UPDATE USERREPORTROWS SET ROWVALID1=  "
                    StrSql = StrSql + "(SELECT USERREPORTROWID FROM USERREPORTROWS WHERE ROWSEQUENCE=( "
                    StrSql = StrSql + "SELECT ROWSEQUENCE FROM USERREPORTROWS WHERE USERREPORTROWID=" + dsCap.Tables(0).Rows(i)("ROWVALID1").ToString() + " ) "
                    StrSql = StrSql + "AND USERREPORTID=" + NewReportId.ToString() + ") "
                    StrSql = StrSql + "WHERE USERREPORTID=" + NewReportId.ToString() + " "
                    StrSql = StrSql + "AND ROWSEQUENCE=" + dsCap.Tables(0).Rows(i)("ROWSEQUENCE").ToString() + " "
                    odbUtil.UpIns(StrSql, Market1Connection)

                    StrSql = "UPDATE USERREPORTROWS SET ROWVALID2=  "
                    StrSql = StrSql + "(SELECT USERREPORTROWID FROM USERREPORTROWS WHERE ROWSEQUENCE=( "
                    StrSql = StrSql + "SELECT ROWSEQUENCE FROM USERREPORTROWS WHERE USERREPORTROWID=" + dsCap.Tables(0).Rows(i)("ROWVALID2").ToString() + " ) "
                    StrSql = StrSql + "AND USERREPORTID=" + NewReportId.ToString() + ") "
                    StrSql = StrSql + "WHERE USERREPORTID=" + NewReportId.ToString() + " "
                    StrSql = StrSql + "AND ROWSEQUENCE=" + dsCap.Tables(0).Rows(i)("ROWSEQUENCE").ToString() + " "
                    odbUtil.UpIns(StrSql, Market1Connection)
                Next


            Catch ex As Exception
                Throw New Exception("M1UpInsData:CreatACopyReport:" + ex.Message.ToString())
            End Try
            Return NewReportId
        End Function

        Public Sub EditReport(ByVal ReportType As String, ByVal ReportId As String, ByVal ReportName As String, ByVal FilterType As String, ByVal FilterValue As String, ByVal ReportFacts As String, ByVal UnitId As String, ByVal Curr As String)

            'Creating Database Connection
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty
            Dim i As New Integer

            Try

                ' ''If ReportType = "UNIFORM" Then
                'Report Details
                StrSql = String.Empty
                StrSql = "UPDATE USERREPORTS  "
                StrSql = StrSql + "SET USERREPORTS.REPORTNAME = '" + ReportName + "' "
                'StrSql = StrSql + "SET USERREPORTS.REPORTFACTS = '" + ReportFacts + "' "
                StrSql = StrSql + "WHERE USERREPORTS.USERREPORTID=" + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)

                'Report(Rows)
                If ReportType <> "MIXED" Then
                    StrSql = String.Empty
                    StrSql = "UPDATE USERREPORTROWS  "
                    StrSql = StrSql + "SET USERREPORTROWS.UNITID=" + UnitId + " "
                    'StrSql = StrSql + "USERREPORTROWS.CURR=" + Curr + " "
                    StrSql = StrSql + "WHERE USERREPORTID=" + ReportId + " "
                    odbUtil.UpIns(StrSql, Market1Connection)
                End If



                '' ''Report Filter Details
                '' ''StrSql = String.Empty
                '' ''StrSql = "UPDATE USERREPORTFILTERS  "
                '' ''StrSql = StrSql + "SET FILTERTYPE='" + FilterType + "', "
                '' ''StrSql = StrSql + "FILTERVALUE='" + FilterValue + "' "
                '' ''StrSql = StrSql + "WHERE USERREPORTFILTERS.USERREPORTID=" + ReportId + " "
                '' ''odbUtil.UpIns(StrSql, Market1Connection)



                ' ''End If





            Catch ex As Exception
                Throw New Exception("M1UpInsData:CreatACopyReport:" + ex.Message.ToString())
            End Try

        End Sub

        Public Sub RenameReport(ByVal ReportId As String, ByVal ReportName As String)

            'Creating Database Connection
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty

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

        Public Function EditUSERReportDetTemp(ByVal RepID As String, ByVal ReportName As String, ByVal filterCount As String, ByVal rowCount As String, ByVal colCount As String, ByVal ReportType As String, ByVal ReportTypeDes As String, ByVal UserID As String, ByVal ReportFact As String, ByVal RegionSetId As String, ByVal RegionId As String, ByVal RowFlag As String, ByVal ColFlag As String, ByVal FilterFlag As String) As Integer
            'Creating Database Connection
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New M1GetData.Selectdata()
            Dim StrSql As String = String.Empty
            Dim i As New Integer
            Dim ReportId As String = String.Empty
            Try
                ReportId = RepID
                If RepID <> "0" Then
                    'Deleting Old Report Row
                    If RowFlag = "Y" Then
                        StrSql = String.Empty
                        StrSql = "DELETE FROM USERREPORTROWSTEMP  "
                        StrSql = StrSql + "WHERE USERREPORTID= " + RepID + " "
                        odbUtil.UpIns(StrSql, Market1Connection)
                    End If

                    'Deleting Old Report Column
                    If ColFlag = "Y" Then
                        StrSql = String.Empty
                        StrSql = "DELETE FROM USERREPORTCOLUMNSTEMP  "
                        StrSql = StrSql + "WHERE USERREPORTID= " + RepID + " "
                        odbUtil.UpIns(StrSql, Market1Connection)
                    End If


                    'Deleting Old Report Filter
                    If FilterFlag = "Y" Then
                        StrSql = String.Empty
                        StrSql = "DELETE FROM USERREPORTFILTERSTEMP  "
                        StrSql = StrSql + "WHERE USERREPORTID= " + RepID + " "
                        odbUtil.UpIns(StrSql, Market1Connection)
                    End If


                    StrSql = String.Empty
                    StrSql = "UPDATE USERREPORTSTEMP  "
                    StrSql = StrSql + "SET REPORTNAME = '" + ReportName + "' "
                    StrSql = StrSql + "WHERE USERREPORTID=" + RepID + " "
                    odbUtil.UpIns(StrSql, Market1Connection)
                End If


                'Filter Type Details
                If FilterFlag = "Y" Then
                    For i = 1 To filterCount
                        StrSql = String.Empty
                        StrSql = "INSERT INTO USERREPORTFILTERSTEMP  "
                        StrSql = StrSql + "(USERREPORTFILTERID,USERREPORTID, FILTERSEQUENCE) "
                        StrSql = StrSql + "VALUES "
                        StrSql = StrSql + "(SEQUSERREPORTFILTERID.NEXTVAL," + ReportId + "," + i.ToString() + ") "
                        odbUtil.UpIns(StrSql, Market1Connection)
                    Next
                End If



                'Report Col. Details
                If ColFlag = "Y" Then
                    For i = 1 To colCount
                        StrSql = String.Empty
                        StrSql = "INSERT INTO USERREPORTCOLUMNSTEMP  "
                        StrSql = StrSql + "(USERREPORTCOLUMNID, USERREPORTID, COLUMNSEQUENCE) "
                        StrSql = StrSql + "VALUES "
                        StrSql = StrSql + "(SEQUSERREPORTCOLUMNID.NEXTVAL," + ReportId + "," + i.ToString() + ") "
                        odbUtil.UpIns(StrSql, Market1Connection)
                    Next
                End If


                'Report Row. Details
                If RowFlag = "Y" Then
                    For i = 1 To rowCount
                        StrSql = String.Empty
                        StrSql = "INSERT INTO USERREPORTROWSTEMP  "
                        StrSql = StrSql + "(USERREPORTROWID, USERREPORTID, ROWSEQUENCE) "
                        StrSql = StrSql + "VALUES "
                        StrSql = StrSql + "(SEQUSERREPORTROWID.NEXTVAL," + ReportId + "," + i.ToString() + ") "
                        odbUtil.UpIns(StrSql, Market1Connection)
                    Next
                End If




                Return ReportId
            Catch ex As Exception
                Throw New Exception("M1UpInsData:EditUSERReportDetTemp:" + ex.Message.ToString())
            End Try

        End Function
        Public Sub UpdateTempReports(ByVal RepID As String, ByVal RPTTYPEDES As String, ByVal REGIONSETID As String, ByVal REGIONID As String)
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty
            Try
                StrSql = String.Empty
                StrSql = "UPDATE USERREPORTSTEMP  "
                StrSql = StrSql + "SET RPTTYPEDES = '" + RPTTYPEDES + "', "
                StrSql = StrSql + "REGIONSETID = '" + REGIONSETID + "', "
                StrSql = StrSql + "REGIONID = " + REGIONID + " "
                StrSql = StrSql + "WHERE USERREPORTID=" + RepID + " "
                odbUtil.UpIns(StrSql, Market1Connection)

            Catch ex As Exception

            End Try
        End Sub
        Public Function AddReportDetailsTemp(ByVal ReportName As String, ByVal UserId As String, ByVal ReportType As String, ByVal ReportFact As String, ByVal filterCount As String, ByVal rowCount As String, ByVal colCount As String, ByVal ReportTypeDes As String, ByVal RegionSetId As String, ByVal RegionId As String, ByVal ServiceId As String) As Integer
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
                StrSql = "INSERT INTO USERREPORTSTEMP  "
                StrSql = StrSql + "(USERREPORTID, REPORTNAME, USERID, CREATEDDATE,RPTTYPE,RPTFACT,RPTTYPEDES,REGIONSETID,REGIONID,SERVICEID) "
                StrSql = StrSql + "VALUES "
                StrSql = StrSql + "(" + ReportId + ",'" + ReportName + "'," + UserId + ",SYSDATE,'" + ReportType + "','" + ReportFact + "','" + ReportTypeDes + "'," + RegionSetId + "," + RegionId + "," + ServiceId + ") "
                odbUtil.UpIns(StrSql, Market1Connection)

                'Filter Type Details
                StrSql = String.Empty
                ' Dim valFilterType As String
                ' valFilterType = FilterType
                For i = 1 To filterCount
                    StrSql = String.Empty
                    StrSql = "INSERT INTO USERREPORTFILTERSTEMP  "
                    StrSql = StrSql + "(USERREPORTFILTERID,USERREPORTID, FILTERSEQUENCE) "
                    StrSql = StrSql + "VALUES "
                    StrSql = StrSql + "(SEQUSERREPORTFILTERID.NEXTVAL," + ReportId + "," + i.ToString() + ") "
                    odbUtil.UpIns(StrSql, Market1Connection)
                Next


                'Report Col. Details
                For i = 1 To colCount
                    StrSql = String.Empty
                    StrSql = "INSERT INTO USERREPORTCOLUMNSTEMP  "
                    StrSql = StrSql + "(USERREPORTCOLUMNID, USERREPORTID, COLUMNSEQUENCE) "
                    StrSql = StrSql + "VALUES "
                    StrSql = StrSql + "(SEQUSERREPORTCOLUMNID.NEXTVAL," + ReportId + "," + i.ToString() + ") "
                    odbUtil.UpIns(StrSql, Market1Connection)
                Next

                'Report Row. Details
                For i = 1 To rowCount
                    StrSql = String.Empty
                    StrSql = "INSERT INTO USERREPORTROWSTEMP  "
                    StrSql = StrSql + "(USERREPORTROWID, USERREPORTID, ROWSEQUENCE) "
                    StrSql = StrSql + "VALUES "
                    StrSql = StrSql + "(SEQUSERREPORTROWID.NEXTVAL," + ReportId + "," + i.ToString() + ") "
                    odbUtil.UpIns(StrSql, Market1Connection)
                Next

                Return ReportId
            Catch ex As Exception
                Throw New Exception("M1UpInsData:AddReportDetailsTemp:" + ex.Message.ToString())
            End Try

        End Function
        Public Sub UpdateRowDetailsTemp(ByVal RowDesc As String, ByVal RowVal As String, ByVal ReportRowId As String, ByVal RowValuType As String, ByVal Curr As String, ByVal Rowval1 As String, ByVal Rowval2 As String, ByVal rowTypeId As String, ByVal rowValueID As String, ByVal unitId As String, ByVal reportId As String)
            Try
                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New Selectdata()
                Dim StrSql As String = String.Empty
                Dim i As New Integer

                StrSql = "UPDATE USERREPORTROWSTEMP  "
                StrSql = StrSql + "SET ROWDECRIPTION='" + RowDesc.ToString() + "', ROWVALUE='" + RowVal.ToString() + "',ROWVALUETYPE='" + RowValuType + "', CURR=" + Curr + ", ROWVAL1='" + Rowval1 + "', ROWVAL2='" + Rowval2 + "', ROWTYPEID=" + rowTypeId + ", ROWVALUEID=" + rowValueID + ", "
                StrSql = StrSql + " UNITID=" + unitId.ToString()
                StrSql = StrSql + " WHERE USERREPORTROWID=" + ReportRowId
                odbUtil.UpIns(StrSql, Market1Connection)

                If RowValuType <> "Formula" Then
                    'UPDATE THE DATA FOR CAPITA
                    StrSql = ""
                    StrSql = "Update USERREPORTROWSTEMP  "
                    StrSql = StrSql + "SET ROWVAL1=" + RowVal + "', UNITID1=" + unitId + " "
                    StrSql = StrSql + " WHERE USERREPORTID=" + reportId
                    StrSql = StrSql + " AND ROWVALID1=" + ReportRowId
                    odbUtil.UpIns(StrSql, Market1Connection)

                    StrSql = ""
                    StrSql = "Update USERREPORTROWS  "
                    StrSql = StrSql + "SET ROWVAL2='" + RowVal + "', UNITID2=" + unitId + " "
                    StrSql = StrSql + " WHERE USERREPORTID=" + reportId
                    StrSql = StrSql + " AND ROWVALID2=" + ReportRowId
                    odbUtil.UpIns(StrSql, Market1Connection)
                End If

            Catch ex As Exception
                Throw New Exception("M1UpInsData:UpdateRowDetailsTemp:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub UpdateCapitaRowDetailsTemp(ByVal RowDesc As String, ByVal RowVal As String, ByVal ReportRowId As String, ByVal RowValuType As String, ByVal Curr As String, ByVal Rowval1 As String, ByVal Rowval2 As String, ByVal rowTypeId As String, ByVal rowValueID As String, ByVal unitId As String, ByVal RowValId1 As String, ByVal RowValId2 As String, ByVal unitId1 As String, ByVal unitId2 As String, ByVal ReportId As String)
            Try
                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New Selectdata()
                Dim StrSql As String = String.Empty
                Dim i As New Integer

                StrSql = "UPDATE USERREPORTROWSTEMP "
                StrSql = StrSql + "SET ROWDECRIPTION='" + RowDesc.ToString() + "', ROWVALUE='" + RowVal.ToString() + "',ROWVALUETYPE='" + RowValuType + "', CURR=" + Curr + ", "
                StrSql = StrSql + "ROWVAL1='" + Rowval1 + "', ROWVAL2='" + Rowval2 + "', ROWTYPEID=" + rowTypeId + ", ROWVALUEID='" + rowValueID + "',"
                StrSql = StrSql + " UNITID='" + unitId + "',UNITID1=" + unitId1 + ",UNITID2=" + unitId2 + ","
                StrSql = StrSql + "ROWVALID1=(SELECT USERREPORTROWID FROM USERREPORTROWSTEMP WHERE ROWVALUE='" + Rowval1 + "' AND UNITID=" + unitId1 + " AND ROWVALUEID=" + RowValId1 + " AND USERREPORTID=" + ReportId + "),"
                StrSql = StrSql + "ROWVALID2=(SELECT USERREPORTROWID FROM USERREPORTROWSTEMP WHERE ROWVALUE='" + Rowval2 + "' AND UNITID=" + unitId2 + " AND ROWVALUEID=" + RowValId2 + " AND USERREPORTID=" + ReportId + ")"
                StrSql = StrSql + " WHERE USERREPORTROWID=" + ReportRowId
                odbUtil.UpIns(StrSql, Market1Connection)

            Catch ex As Exception
                Throw New Exception("M1UpInsData:UpdateCapitaRowDetailsTemp:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub UpdateCapitaRowDetails(ByVal RowDesc As String, ByVal RowVal As String, ByVal ReportRowId As String, ByVal RowValuType As String, ByVal Curr As String, ByVal Rowval1 As String, ByVal Rowval2 As String, ByVal rowTypeId As String, ByVal rowValueID As String, ByVal unitId As String, ByVal RowValId1 As String, ByVal RowValId2 As String, ByVal unitId1 As String, ByVal unitId2 As String, ByVal ReportId As String)
            Try
                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New Selectdata()
                Dim StrSql As String = String.Empty
                Dim i As New Integer

                StrSql = "UPDATE USERREPORTROWS  "
                StrSql = StrSql + "SET ROWDECRIPTION='" + RowDesc.ToString() + "', ROWVALUE='" + RowVal.ToString() + "',ROWVALUETYPE='" + RowValuType + "', CURR=" + Curr + ", "
                StrSql = StrSql + "ROWVAL1='" + Rowval1 + "', ROWVAL2='" + Rowval2 + "', ROWTYPEID=" + rowTypeId + ", ROWVALUEID='" + rowValueID + "',"
                StrSql = StrSql + " UNITID='" + unitId + "',UNITID1=" + unitId1 + ",UNITID2=" + unitId2 + ", "
                StrSql = StrSql + "ROWVALID1=(SELECT USERREPORTROWID FROM USERREPORTROWS WHERE ROWVALUE='" + Rowval1 + "' AND UNITID=" + unitId1 + " AND ROWVALUEID=" + RowValId1 + " AND USERREPORTID=" + ReportId + "),"
                StrSql = StrSql + "ROWVALID2=(SELECT USERREPORTROWID FROM USERREPORTROWS WHERE ROWVALUE='" + Rowval2 + "' AND UNITID=" + unitId2 + " AND ROWVALUEID=" + RowValId2 + " AND USERREPORTID=" + ReportId + ")"
                StrSql = StrSql + " WHERE USERREPORTROWID=" + ReportRowId
                odbUtil.UpIns(StrSql, Market1Connection)

            Catch ex As Exception
                Throw New Exception("M1UpInsData:UpdateCapitaRowDetails:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub SaveUserTempReports(ByVal ReportId As String)
            'Creating Database Connection
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty
            Dim i As New Integer
            Try
                'Deleting Previous User Report Data
                StrSql = "DELETE FROM USERREPORTSTEMP  "
                StrSql = StrSql + "Where USERREPORTID=" + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)
                StrSql = "DELETE FROM USERREPORTROWSTEMP  "
                StrSql = StrSql + "Where USERREPORTID=" + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)
                StrSql = "DELETE FROM USERREPORTCOLUMNSTEMP  "
                StrSql = StrSql + "Where USERREPORTID=" + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)
                StrSql = "DELETE FROM USERREPORTFILTERSTEMP "
                StrSql = StrSql + "Where USERREPORTID=" + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)

                'Updating User Report
                StrSql = "INSERT INTO USERREPORTSTEMP  "
                StrSql = StrSql + "(USERREPORTID,REPORTNAME,USERID,CREATEDDATE,ISSYSTEM,RPTTYPE,RPTFACT,RPTTYPEDES,REGIONSETID,REGIONID,SERVICEID) "
                StrSql = StrSql + "Select USERREPORTID,REPORTNAME,USERID,CREATEDDATE,ISSYSTEM,RPTTYPE,RPTFACT,RPTTYPEDES,REGIONSETID,REGIONID,SERVICEID FROM USERREPORTS "
                StrSql = StrSql + "Where USERREPORTID=" + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)

                'Updating User Report Row
                StrSql = "INSERT INTO USERREPORTROWSTEMP  "
                StrSql = StrSql + "(USERREPORTROWID,USERREPORTID,ROWSEQUENCE,ROWDECRIPTION,ROWVALUETYPE,ROWVALUE,CURR,ROWVAL1,ROWVAL2,ROWTYPEID,ROWVALUEID,UNITID,ROWVALID1,ROWVALID2,UNITID1,UNITID2) "
                StrSql = StrSql + "Select USERREPORTROWID,USERREPORTID,ROWSEQUENCE,ROWDECRIPTION,ROWVALUETYPE,ROWVALUE,CURR,ROWVAL1,ROWVAL2,ROWTYPEID,ROWVALUEID,UNITID,ROWVALID1,ROWVALID2,UNITID1,UNITID2 FROM USERREPORTROWS "
                StrSql = StrSql + "Where USERREPORTID=" + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)

                'Updating User Report Column
                StrSql = "INSERT INTO USERREPORTCOLUMNSTEMP  "
                StrSql = StrSql + "(USERREPORTCOLUMNID,USERREPORTID,COLUMNSEQUENCE,COLUMNDECRIPTION,COLUMNVALUETYPE,COLUMNVALUE,INPUTVALUETYPE1,INPUTVALUE1,INPUTVALUETYPE2,INPUTVALUE2,INPUTVALUETYPE3,INPUTVALUE3,INPUTVALUETYPE4,INPUTVALUE4,COLUMNTYPEID,COLUMNVALUEID) "
                StrSql = StrSql + "Select USERREPORTCOLUMNID,USERREPORTID,COLUMNSEQUENCE,COLUMNDECRIPTION,COLUMNVALUETYPE,COLUMNVALUE,INPUTVALUETYPE1,INPUTVALUE1,INPUTVALUETYPE2,INPUTVALUE2,INPUTVALUETYPE3,INPUTVALUE3,INPUTVALUETYPE4,INPUTVALUE4,COLUMNTYPEID,COLUMNVALUEID FROM USERREPORTCOLUMNS "
                StrSql = StrSql + "Where USERREPORTID=" + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)


                'Updating User Report Filters
                StrSql = "INSERT INTO USERREPORTFILTERSTEMP  "
                StrSql = StrSql + "(USERREPORTFILTERID,USERREPORTID,FILTERTYPE,FILTERVALUE,FILTERSEQUENCE,FILTERTYPEID,FILTERVALUEID) "
                StrSql = StrSql + "Select USERREPORTFILTERID,USERREPORTID,FILTERTYPE,FILTERVALUE,FILTERSEQUENCE,FILTERTYPEID,FILTERVALUEID FROM USERREPORTFILTERS "
                StrSql = StrSql + "Where USERREPORTID=" + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)

            Catch ex As Exception
                Throw New Exception("M1UpInsData:SaveUserTempReports:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub UpdateColDetailsTemp(ByVal Seq As String, ByVal ColType As String, ByVal ColVal As String, ByVal UDFV1 As String, ByVal UDFV2 As String, ByVal ReportColId As String, ByVal colTypeId As String, ByVal colValueID As String, ByVal colCAGRYearId1 As String, ByVal colCAGRYearId2 As String, ByVal reportId As String)
            Try
                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New Selectdata()
                Dim StrSql As String = String.Empty
                Dim i As New Integer


                'Report Col Details
                StrSql = "Update USERREPORTCOLUMNSTEMP  "
                StrSql = StrSql + "Set COLUMNVALUETYPE='" + ColType + "', COLUMNVALUE='" + ColVal + "', INPUTVALUE1='" + UDFV1 + "', INPUTVALUE2='" + UDFV2 + "', COLUMNTYPEID=" + colTypeId + ", COLUMNVALUEID=" + colValueID + ", "
                StrSql = StrSql + " INPUTVALUETYPE1='" + colCAGRYearId1 + "' , INPUTVALUETYPE2='" + colCAGRYearId2 + "' "
                StrSql = StrSql + " Where USERREPORTCOLUMNID=" + ReportColId
                odbUtil.UpIns(StrSql, Market1Connection)

                If ColType = "Year" Then
                    'UPDATE THE DATA FOR CAGR
                    StrSql = ""
                    StrSql = "Update USERREPORTCOLUMNSTEMP  "
                    StrSql = StrSql + "SET INPUTVALUE1=" + ColVal
                    StrSql = StrSql + " WHERE USERREPORTID=" + reportId
                    StrSql = StrSql + " AND INPUTVALUETYPE1=" + ReportColId
                    odbUtil.UpIns(StrSql, Market1Connection)

                    StrSql = ""
                    StrSql = "Update USERREPORTCOLUMNSTEMP  "
                    StrSql = StrSql + "SET INPUTVALUE2=" + ColVal
                    StrSql = StrSql + " WHERE USERREPORTID=" + reportId
                    StrSql = StrSql + " AND INPUTVALUETYPE2=" + ReportColId
                    odbUtil.UpIns(StrSql, Market1Connection)
                End If
            Catch ex As Exception
                Throw New Exception("M1UpInsData:UpdateColDetailsTemp:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub UpdateFilterDetailsTemp(ByVal filterType As String, ByVal filterValue As String, ByVal filterTypeId As String, ByVal filterValueId As String, ByVal ReportFilterID As String)

            Try
                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New Selectdata()
                Dim StrSql As String = String.Empty
                Dim i As New Integer

                'Report Col Details
                StrSql = "UPDATE USERREPORTFILTERSTEMP  "

                StrSql = StrSql + " SET FILTERTYPE='" + filterType.ToString() + "', FILTERVALUE='" + filterValue.ToString() + "',FILTERTYPEID=" + filterTypeId.ToString() + ",FILTERVALUEID=" + filterValueId + ""
                StrSql = StrSql + " WHERE USERREPORTFILTERID=" + ReportFilterID
                odbUtil.UpIns(StrSql, Market1Connection)

            Catch ex As Exception
                Throw New Exception("M1UpInsData:UpdateFilterDetailsTemp:" + ex.Message.ToString())

            End Try

        End Sub
        Public Sub EditReportTypeDesTemp(ByVal ReportId As String, ByVal ReportTypeDes As String)
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty

            Try

                If ReportId > 1000 Then
                    'Report Details
                    StrSql = String.Empty
                    StrSql = "UPDATE USERREPORTSTEMP  "
                    StrSql = StrSql + "SET USERREPORTSTEMP.RPTTYPEDES = '" + ReportTypeDes + "' "
                    StrSql = StrSql + "WHERE USERREPORTSTEMP.USERREPORTID=" + ReportId + " "
                    odbUtil.UpIns(StrSql, Market1Connection)
                Else
                    StrSql = String.Empty
                    StrSql = "UPDATE BASEREPORTSTEMP  "
                    StrSql = StrSql + "SET BASEREPORTSTEMP.RPTTYPEDES = '" + ReportTypeDes + "' "
                    StrSql = StrSql + "WHERE BASEREPORTSTEMP.BASEREPORTID=" + ReportId + " "
                    odbUtil.UpIns(StrSql, Market1Connection)
                End If


            Catch ex As Exception
                Throw New Exception("M1UpInsData:EditReportTypeDesTemp:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub UpdateUserReportNameTemp(ByVal ReportName As String, ByVal ReportId As String)
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty
            Try
                StrSql = String.Empty
                StrSql = "UPDATE USERREPORTSTEMP  "
                StrSql = StrSql + "SET REPORTNAME = '" + ReportName + "' "
                StrSql = StrSql + "WHERE USERREPORTID=" + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)
            Catch ex As Exception
                Throw New Exception("M1UpInsData:UpdateUserReportNameTemp:" + ex.Message.ToString())
            End Try
        End Sub
        Public Sub SaveUserReports(ByVal ReportId As String)
            'Creating Database Connection
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty
            Dim i As New Integer
            Try

                'Deleting Previous User Report Data
                StrSql = "DELETE FROM USERREPORTS  "
                StrSql = StrSql + "Where USERREPORTID=" + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)
                StrSql = "DELETE FROM USERREPORTROWS  "
                StrSql = StrSql + "Where USERREPORTID=" + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)
                StrSql = "DELETE FROM USERREPORTCOLUMNS  "
                StrSql = StrSql + "Where USERREPORTID=" + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)
                StrSql = "DELETE FROM USERREPORTFILTERS  "
                StrSql = StrSql + "Where USERREPORTID=" + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)

                'Updating User Report
                StrSql = "INSERT INTO USERREPORTS  "
                StrSql = StrSql + "(USERREPORTID,REPORTNAME,USERID,CREATEDDATE,ISSYSTEM,RPTTYPE,RPTFACT,RPTTYPEDES,REGIONSETID,REGIONID,SERVICEID) "
                StrSql = StrSql + "Select USERREPORTID,REPORTNAME,USERID,CREATEDDATE,ISSYSTEM,RPTTYPE,RPTFACT,RPTTYPEDES,REGIONSETID,REGIONID,SERVICEID FROM USERREPORTSTEMP "
                StrSql = StrSql + "Where USERREPORTID=" + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)

                'Updating User Report Row
                StrSql = "INSERT INTO USERREPORTROWS  "
                StrSql = StrSql + "(USERREPORTROWID,USERREPORTID,ROWSEQUENCE,ROWDECRIPTION,ROWVALUETYPE,ROWVALUE,CURR,ROWVAL1,ROWVAL2,ROWTYPEID,ROWVALUEID,UNITID,ROWVALID1,ROWVALID2,UNITID1,UNITID2) "
                StrSql = StrSql + "Select USERREPORTROWID,USERREPORTID,ROWSEQUENCE,ROWDECRIPTION,ROWVALUETYPE,ROWVALUE,CURR,ROWVAL1,ROWVAL2,ROWTYPEID,ROWVALUEID,UNITID,ROWVALID1,ROWVALID2,UNITID1,UNITID2 FROM USERREPORTROWSTEMP "
                StrSql = StrSql + "Where USERREPORTID=" + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)

                'Updating User Report Column
                StrSql = "INSERT INTO USERREPORTCOLUMNS  "
                StrSql = StrSql + "(USERREPORTCOLUMNID,USERREPORTID,COLUMNSEQUENCE,COLUMNDECRIPTION,COLUMNVALUETYPE,COLUMNVALUE,INPUTVALUETYPE1,INPUTVALUE1,INPUTVALUETYPE2,INPUTVALUE2,INPUTVALUETYPE3,INPUTVALUE3,INPUTVALUETYPE4,INPUTVALUE4,COLUMNTYPEID,COLUMNVALUEID) "
                StrSql = StrSql + "Select USERREPORTCOLUMNID,USERREPORTID,COLUMNSEQUENCE,COLUMNDECRIPTION,COLUMNVALUETYPE,COLUMNVALUE,INPUTVALUETYPE1,INPUTVALUE1,INPUTVALUETYPE2,INPUTVALUE2,INPUTVALUETYPE3,INPUTVALUE3,INPUTVALUETYPE4,INPUTVALUE4,COLUMNTYPEID,COLUMNVALUEID FROM USERREPORTCOLUMNSTEMP "
                StrSql = StrSql + "Where USERREPORTID=" + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)


                'Updating User Report Filters
                StrSql = "INSERT INTO USERREPORTFILTERS  "
                StrSql = StrSql + "(USERREPORTFILTERID,USERREPORTID,FILTERTYPE,FILTERVALUE,FILTERSEQUENCE,FILTERTYPEID,FILTERVALUEID) "
                StrSql = StrSql + "Select USERREPORTFILTERID,USERREPORTID,FILTERTYPE,FILTERVALUE,FILTERSEQUENCE,FILTERTYPEID,FILTERVALUEID FROM USERREPORTFILTERSTEMP "
                StrSql = StrSql + "Where USERREPORTID=" + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)

                DeleteUserReportsTemp(ReportId)

            Catch ex As Exception
                Throw New Exception("M1UpInsData:SaveUserReports:" + ex.Message.ToString())
            End Try

        End Sub
        Public Sub DeleteUserReportsTemp(ByVal ReportId As String)
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New Selectdata()
            Dim StrSql As String = String.Empty
            Try
                'Deleting Temp User Report Data
                StrSql = "DELETE FROM USERREPORTSTEMP  "
                StrSql = StrSql + "Where USERREPORTID=" + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)
                StrSql = "DELETE FROM USERREPORTROWSTEMP  "
                StrSql = StrSql + "Where USERREPORTID=" + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)
                StrSql = "DELETE FROM USERREPORTCOLUMNSTEMP  "
                StrSql = StrSql + "Where USERREPORTID=" + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)
                StrSql = "DELETE FROM USERREPORTFILTERSTEMP  "
                StrSql = StrSql + "Where USERREPORTID=" + ReportId + " "
                odbUtil.UpIns(StrSql, Market1Connection)
            Catch ex As Exception
                Throw New Exception("M1UpInsData:DeleteUserReportsTemp:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub UpdateRowDetailsRep(ByVal RowDesc As String, ByVal RowVal As String, ByVal ReportRowId As String, ByVal RowValuType As String, ByVal Curr As String, ByVal Rowval1 As String, ByVal Rowval2 As String, ByVal rowTypeId As String, ByVal rowValueID As String, ByVal unitId As String)
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
                Throw New Exception("M1UpInsData:UpdateRowDetailsRep:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub UpdateRowDetailsRepGRP(ByVal RepID As String, ByVal RowDesc As String, ByVal RowVal As String, ByVal RowValuType As String, ByVal Curr As String, ByVal Rowval1 As String, ByVal Rowval2 As String, ByVal rowTypeId As String, ByVal rowValueID As String, ByVal unitId As String)
            Try
                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New Selectdata()
                Dim StrSql As String = String.Empty
                Dim i As New Integer

                StrSql = "INSERT INTO USERREPORTROWS(USERREPORTID,USERREPORTROWID,ROWDECRIPTION,ROWVALUE,ROWVALUETYPE,CURR,ROWVAL1,ROWVAL2,ROWTYPEID,ROWVALUEID,UNITID)  "
                StrSql = StrSql + "SELECT " + RepID + ", SEQUSERREPORTROWID.NEXTVAL,'" + RowDesc.ToString() + "','" + RowVal.ToString() + "','" + RowValuType + "'," + Curr + ",'" + Rowval1 + "','" + Rowval2 + "'," + rowTypeId + "," + rowValueID + ", "
                StrSql = StrSql + " " + unitId.ToString() + " FROM DUAL "

                odbUtil.UpIns(StrSql, Market1Connection)
            Catch ex As Exception
                Throw New Exception("M1UpInsData:UpdateRowDetailsRep:" + ex.Message.ToString())
            End Try
        End Sub

        Public Function EditUSERReportRowDetail(ByVal RepID As String, ByVal rowCount As String) As Integer
            'Creating Database Connection
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New M1GetData.Selectdata()
            Dim StrSql As String = String.Empty
            Dim i As New Integer
            Dim ReportId As String = String.Empty
            Dim UnitId As String = String.Empty
            Dim Dts As New DataSet
            Try
                ReportId = RepID

                'Getting UnitID
                StrSql = "SELECT  "
                StrSql = StrSql + "DISTINCT UNITID "
                StrSql = StrSql + "FROM USERREPORTROWS  "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "USERREPORTID = " + ReportId + " "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                If Dts.Tables(0).Rows.Count > 0 Then
                    UnitId = Dts.Tables(0).Rows(0).Item("UNITID").ToString()
                End If

                If RepID <> "0" Then
                    'Deleting Old Report Row
                    StrSql = String.Empty
                    StrSql = "DELETE FROM USERREPORTROWS  "
                    StrSql = StrSql + "WHERE USERREPORTID= " + RepID + " "
                    odbUtil.UpIns(StrSql, Market1Connection)
                End If

                'Report Row. Details
                For i = 1 To rowCount
                    StrSql = String.Empty
                    StrSql = "INSERT INTO USERREPORTROWS  "
                    StrSql = StrSql + "(USERREPORTROWID, USERREPORTID, ROWSEQUENCE) "
                    StrSql = StrSql + "VALUES "
                    StrSql = StrSql + "(SEQUSERREPORTROWID.NEXTVAL," + ReportId + "," + i.ToString() + ") "
                    odbUtil.UpIns(StrSql, Market1Connection)
                Next
                Return UnitId
            Catch ex As Exception
                Throw New Exception("M1UpInsData:EditUSERReportDetail:" + ex.Message.ToString())
            End Try
        End Function

        Public Function EditUSERReportRowDetailGRP(ByVal RepID As String, ByVal rowCount As String) As Integer
            'Creating Database Connection
            Dim odbUtil As New DBUtil()
            Dim ObjGetData As New M1GetData.Selectdata()
            Dim StrSql As String = String.Empty
            Dim i As New Integer
            Dim ReportId As String = String.Empty
            Dim UnitId As String = String.Empty
            Dim Dts As New DataSet
            Try
                ReportId = RepID

                'Getting UnitID
                StrSql = "SELECT  "
                StrSql = StrSql + "DISTINCT UNITID "
                StrSql = StrSql + "FROM USERREPORTROWS  "
                StrSql = StrSql + "WHERE "
                StrSql = StrSql + "USERREPORTID = " + ReportId + " "
                Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
                If Dts.Tables(0).Rows.Count > 0 Then
                    UnitId = Dts.Tables(0).Rows(0).Item("UNITID").ToString()
                End If

                If RepID <> "0" Then
                    'Deleting Old Report Row
                    StrSql = String.Empty
                    StrSql = "DELETE FROM USERREPORTROWS  "
                    StrSql = StrSql + "WHERE USERREPORTID= " + RepID + " "
                    odbUtil.UpIns(StrSql, Market1Connection)
                End If

                'Report Row. Details
                'For i = 1 To rowCount
                '    StrSql = String.Empty
                '    StrSql = "INSERT INTO USERREPORTROWS  "
                '    StrSql = StrSql + "(USERREPORTROWID, USERREPORTID, ROWSEQUENCE) "
                '    StrSql = StrSql + "VALUES "
                '    StrSql = StrSql + "(SEQUSERREPORTROWID.NEXTVAL," + ReportId + "," + i.ToString() + ") "
                '    odbUtil.UpIns(StrSql, Market1Connection)
                'Next
                Return UnitId
            Catch ex As Exception
                Throw New Exception("M1UpInsData:EditUSERReportDetail:" + ex.Message.ToString())
            End Try
        End Function
#Region "Preferences"
        Public Sub PrefrencesUpdate(ByVal reportId As String, ByVal Units As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim Title1 As String = ""
            Dim Title2 As String = ""
            Dim Title3 As String = ""
            Dim Title7 As String = ""
            Dim Title8 As String = ""
            Dim Title9 As String = ""
            Dim ConvWt As String = ""
            Dim ConvArea As String = ""
            Dim ConvGallon As String = ""
            Try
                If Units = 0 Then
                    ConvWt = "1"
                    Title1 = "lb"
                    Title2 = "gal"
                    Title3 = "msi"
                    Title7 = "mil"
                    Title8 = "oz"
                    Title9 = "sq.in"
                    ConvArea = "1"
                    ConvGallon = "1"
                Else
                    Title1 = "kg"
                    Title2 = "ltr"
                    Title3 = "sq.m"
                    Title7 = "micron"
                    Title8 = "ml"
                    Title9 = "sq.cm"
                    ConvArea = "0.64516"
                    ConvWt = "0.453592"
                    ConvGallon = "3.78541"
                End If
                StrSql = "UPDATE WEBPREFERRENCES  "
                StrSql = StrSql + "SET "
                StrSql = StrSql + "UNITS =" + Units + ", "
                StrSql = StrSql + "TITLE1 ='" + Title1 + "', "
                StrSql = StrSql + "TITLE2 ='" + Title2 + "', "
                StrSql = StrSql + "TITLE3 ='" + Title3 + "', "
                StrSql = StrSql + "TITLE7 ='" + Title7 + "', "
                StrSql = StrSql + "TITLE8 ='" + Title8 + "', "
                StrSql = StrSql + "TITLE9 ='" + Title9 + "', "
                StrSql = StrSql + "CONVTWT =" + ConvWt + ", "
                StrSql = StrSql + "CONVTAREA =" + ConvArea + ", "
                StrSql = StrSql + "CONVTVOL =" + ConvGallon + " "
                StrSql = StrSql + "WHERE REPORTID =  " + reportId + ""
                odbUtil.UpIns(StrSql, Market1Connection)
            Catch ex As Exception

            End Try
        End Sub

        Public Sub PrefrencesInsert(ByVal reportId As String)
            Dim odbUtil As New DBUtil()
            Dim StrSql As String
            Dim Units As String = ""
            Dim Title1 As String = ""
            Dim Title2 As String = ""
            Dim Title3 As String = ""
            Dim Title7 As String = ""
            Dim Title8 As String = ""
            Dim Title9 As String = ""
            Dim ConvWt As String = ""
            Dim ConvArea As String = ""
            Dim ConvGallon As String = ""
            Try
                Units = 0
                ConvWt = "1"
                Title1 = "lb"
                Title2 = "gal"
                Title3 = "msi"
                Title7 = "mil"
                Title8 = "oz"
                Title9 = "sq.in"
                ConvArea = "1"
                ConvGallon = "1"

                StrSql = "INSERT INTO WEBPREFERRENCES  "
                StrSql = StrSql + "(REPORTID, UNITS, TITLE1, TITLE2, TITLE3, TITLE7, TITLE8, TITLE9, CONVTWT, CONVTAREA, CONVTVOL)"
                StrSql = StrSql + "SELECT '" + reportId + "', '" + Units + "', '" + Title1 + "', '" + Title2 + "', '" + Title3 + "', '" + Title7 + "', '" + Title8 + "', '" + Title9 + "', '" + ConvWt + "', '" + ConvArea + "', '" + ConvGallon + "' FROM DUAL"
                StrSql = StrSql + " WHERE NOT EXISTS (SELECT * FROM WEBPREFERRENCES WHERE REPORTID = '" + reportId + "')"
                odbUtil.UpIns(StrSql, Market1Connection)
            Catch ex As Exception

            End Try
        End Sub
#End Region

#Region "Market1 Group"
        Public Function AddGroupName(ByVal Des1 As String, ByVal Des2 As String, ByVal USERID As String, ByVal Type As String, ByVal ServiceID As String) As String
            Dim odButil As New DBUtil()
            Dim strsql As String = String.Empty
            Dim Dts As New DataSet
            Dim GROUPID As String = ""
            Dim i As Integer = 0
            Try
                'Getting GROUPID from Sequence
                strsql = String.Empty
                strsql = "SELECT SEQGROUPID.NEXTVAL AS GROUPID  "
                strsql = strsql + "FROM DUAL "
                Dts = odButil.FillDataSet(strsql, Market1Connection)
                If Dts.Tables(0).Rows.Count > 0 Then
                    GROUPID = Dts.Tables(0).Rows(0).Item("GROUPID").ToString()

                    'Inserting Into Groups Table
                    strsql = String.Empty
                    strsql = "INSERT INTO GROUPS  "
                    strsql = strsql + "( "
                    strsql = strsql + "GROUPID, "
                    strsql = strsql + "DES1, "
                    strsql = strsql + "DES2, "
                    strsql = strsql + "USERID, "
                    strsql = strsql + "SERVICEID, "
                    strsql = strsql + "CREATIONDATE, "
                    strsql = strsql + "UPDATEDATE "
                    strsql = strsql + ") "
                    strsql = strsql + "SELECT " + GROUPID + ", "
                    strsql = strsql + "'" + Des1.ToString().Replace("'", "''") + "', "
                    strsql = strsql + "'" + Des2.ToString().Replace("'", "''") + "', "
                    strsql = strsql + USERID + ", "
                    strsql = strsql + ServiceID + ", "
                    strsql = strsql + "sysdate, "
                    strsql = strsql + "sysdate "
                    strsql = strsql + "FROM DUAL "
                    strsql = strsql + "WHERE NOT EXISTS "
                    strsql = strsql + "( "
                    strsql = strsql + "SELECT 1 "
                    strsql = strsql + "FROM "
                    strsql = strsql + "GROUPS "
                    strsql = strsql + "WHERE "
                    strsql = strsql + "DES1='" + Des1.ToString().Replace("'", "''") + "' "
                    strsql = strsql + "AND "
                    strsql = strsql + "DES2='" + Des2.ToString().Replace("'", "''") + "' "
                    strsql = strsql + "AND SERVICEID= " + ServiceID + " "
                    strsql = strsql + ") "
                    odButil.UpIns(strsql, Market1Connection)

                End If
                Return GROUPID
            Catch ex As Exception
                Throw New Exception("M1UpInsData:AddGroupName:" + ex.Message.ToString())
            End Try
        End Function

        Public Sub EditGroups(ByVal grpID As String, ByVal REPORTId As String, ByVal ServiceID As String)
            Try
                Dim DtsCount As New DataSet()
                Dim odButil As New DBUtil()
                Dim objGetData As New M1SubGetData.Selectdata()
                Dim seqCount As Integer = 0
                Dim StrSqlUpadate As String = ""

                'Updating New Group Server Datae
                StrSqlUpadate = "UPDATE GROUPS  SET UPDATEDATE=sysdate WHERE GROUPID= " + grpID + " "
                odButil.UpIns(StrSqlUpadate, Market1Connection)

                StrSqlUpadate = String.Empty


                If (REPORTId <> Nothing) Then
                    'Count for number of structures in group
                    DtsCount = objGetData.GetMaxSEQReports(grpID)
                    If DtsCount.Tables(0).Rows.Count > 0 Then
                        seqCount = DtsCount.Tables(0).Rows(0).Item("MAXCOUNT").ToString()
                    End If
                    StrSqlUpadate = String.Empty
                    If grpID <> 0 Then
                        'Inserting new group Id
                        StrSqlUpadate = "INSERT INTO GROUPREPORTS  "
                        StrSqlUpadate = StrSqlUpadate + "( "
                        StrSqlUpadate = StrSqlUpadate + "GROUPREPORTID, "
                        StrSqlUpadate = StrSqlUpadate + "GROUPID, "
                        StrSqlUpadate = StrSqlUpadate + "REPORTID, "
                        StrSqlUpadate = StrSqlUpadate + "SERVICEID, "
                        StrSqlUpadate = StrSqlUpadate + "SEQ "
                        StrSqlUpadate = StrSqlUpadate + ") "
                        StrSqlUpadate = StrSqlUpadate + "SELECT SEQGROUPREPORTID.NEXTVAL, "
                        StrSqlUpadate = StrSqlUpadate + grpID + ", "
                        StrSqlUpadate = StrSqlUpadate + REPORTId + ", "
                        StrSqlUpadate = StrSqlUpadate + ServiceID + ", "
                        StrSqlUpadate = StrSqlUpadate + (seqCount + 1).ToString() + " "
                        StrSqlUpadate = StrSqlUpadate + "FROM DUAL "
                        StrSqlUpadate = StrSqlUpadate + "WHERE NOT EXISTS "
                        StrSqlUpadate = StrSqlUpadate + "( "
                        StrSqlUpadate = StrSqlUpadate + "SELECT 1 "
                        StrSqlUpadate = StrSqlUpadate + "FROM "
                        StrSqlUpadate = StrSqlUpadate + "GROUPREPORTS "
                        StrSqlUpadate = StrSqlUpadate + "WHERE "
                        StrSqlUpadate = StrSqlUpadate + "GROUPID=" + grpID + " AND "
                        StrSqlUpadate = StrSqlUpadate + "REPORTID=" + REPORTId + " AND "
                        StrSqlUpadate = StrSqlUpadate + "SERVICEID=" + ServiceID + " "
                        StrSqlUpadate = StrSqlUpadate + ") "
                        odButil.UpIns(StrSqlUpadate, Market1Connection)
                    End If
                End If

            Catch ex As Exception
                Throw New Exception("M1UpInsData:EditGroups:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub DeleteReportsFrmGrp(ByVal grpID As String, ByVal ReportId As String, ByVal ServiceID As String)
            Try
                Dim DtsCount As New DataSet()
                Dim odButil As New DBUtil()
                Dim objGetData As New StandGetData.Selectdata()
                Dim StrSqlUpadate As String = ""

                If (ReportId <> Nothing) Then
                    StrSqlUpadate = String.Empty
                    If grpID <> 0 Then
                        'Deleting Previous Group Entry
                        StrSqlUpadate = "DELETE FROM GROUPREPORTS WHERE GROUPID= " + grpID + " AND REPORTID=" + ReportId + "AND SERVICEID=" + ServiceID + " "
                        odButil.UpIns(StrSqlUpadate, Market1Connection)
                        StrSqlUpadate = String.Empty
                        'Updating New Group Server Datae
                        StrSqlUpadate = "UPDATE GROUPS  SET UPDATEDATE=sysdate WHERE GROUPID= " + grpID + " "
                        odButil.UpIns(StrSqlUpadate, Market1Connection)
                    End If
                End If

            Catch ex As Exception
                Throw New Exception("M1UpInsData:DeleteReportsFrmGrp:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub UpdateGroupByReportID(ByVal OldgrpID As String, ByVal grpID As String, ByVal ReportID As String, ByVal ServiceId As String)
            Dim DtsCount As New DataSet()
            Dim objGetData As New M1SubGetData.Selectdata()
            Dim seqCount As Integer = 0
            Dim odButil As New DBUtil()
            Dim strsql As String = String.Empty
            Dim StrSqlUpadate As String = ""
            Try
                'Deleting Previous Group Entry
                StrSqlUpadate = "DELETE FROM GROUPREPORTS WHERE GROUPID= " + OldgrpID + " AND REPORTID=" + ReportID + " "
                odButil.UpIns(StrSqlUpadate, Market1Connection)

                'Updating Old Group Server Datae
                StrSqlUpadate = "UPDATE GROUPS  SET UPDATEDATE=sysdate WHERE GROUPID= " + OldgrpID + " "
                odButil.UpIns(StrSqlUpadate, Market1Connection)

                'Updating New Group Server Datae
                StrSqlUpadate = "UPDATE GROUPS  SET UPDATEDATE=sysdate WHERE GROUPID= " + grpID + " "
                odButil.UpIns(StrSqlUpadate, Market1Connection)

                DtsCount = objGetData.GetMaxSEQGREPORT(grpID, ServiceId)
                If DtsCount.Tables(0).Rows.Count > 0 Then
                    seqCount = DtsCount.Tables(0).Rows(0).Item("MAXCOUNT").ToString()
                End If
                If grpID <> 0 Then
                    'Inserting new group Id
                    strsql = "INSERT INTO GROUPREPORTS  "
                    strsql = strsql + "( "
                    strsql = strsql + "GROUPREPORTID, "
                    strsql = strsql + "GROUPID, "
                    strsql = strsql + "REPORTID, "
                    strsql = strsql + "SEQ, "
                    strsql = strsql + "SERVICEID "
                    strsql = strsql + ") "
                    strsql = strsql + "SELECT SEQGROUPREPORTID.NEXTVAL, "
                    strsql = strsql + grpID + ", "
                    strsql = strsql + ReportID + ", "
                    strsql = strsql + (seqCount + 1).ToString() + ", "
                    strsql = strsql + ServiceId + " "
                    strsql = strsql + "FROM DUAL "
                    strsql = strsql + "WHERE NOT EXISTS "
                    strsql = strsql + "( "
                    strsql = strsql + "SELECT 1 "
                    strsql = strsql + "FROM "
                    strsql = strsql + "GROUPREPORTS "
                    strsql = strsql + "WHERE "
                    strsql = strsql + "GROUPID=" + grpID + " AND "
                    strsql = strsql + "REPORTID=" + ReportID + " "
                    strsql = strsql + ") "

                    odButil.UpIns(strsql, Market1Connection)
                End If
            Catch ex As Exception
                Throw New Exception("M1SubGetData:UpdateGroupByReportID:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub UpdateGroupName(ByVal groupName() As String, ByVal groupDes() As String, ByVal GroupID() As String, ByVal count As Integer)
            ' Dim DtsCount As New DataSet()
            'Dim objGetData As New E1GetData.Selectdata()
            ' Dim seqCount As Integer = 0
            Dim odButil As New DBUtil()
            'Dim strsql As String = String.Empty
            Dim StrSqlUpadate As String = ""
            Dim i As Integer = 0
            Try
                For i = 0 To count - 1
                    If groupName(i) <> "" Then
                        StrSqlUpadate = "UPDATE GROUPS SET "
                        StrSqlUpadate = StrSqlUpadate + " DES1='" + groupName(i).ToString() + "', "
                        StrSqlUpadate = StrSqlUpadate + " DES2='" + groupDes(i).ToString() + "' "
                        StrSqlUpadate = StrSqlUpadate + " WHERE GROUPID= " + GroupID(i).ToString()
                        odButil.UpIns(StrSqlUpadate, Market1Connection)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception("M1SubGetData:UpdateGroupName:" + ex.Message.ToString())
            End Try
        End Sub

        Public Sub DeleteGroups(ByVal grpId As String)
            Dim odButil As New DBUtil()
            Dim StrSqlUpadate As String = ""

            Try
                StrSqlUpadate = " DELETE FROM GROUPS WHERE GROUPID=" + grpId
                odButil.UpIns(StrSqlUpadate, Market1Connection)
                StrSqlUpadate = " DELETE FROM GROUPREPORTS WHERE GROUPID=" + grpId
                odButil.UpIns(StrSqlUpadate, Market1Connection)
            Catch ex As Exception
                Throw New Exception("M1SubUpdate:DeleteGroup:" + ex.Message.ToString())
            End Try
        End Sub
#End Region

#Region "Log"
        Public Sub InsertLog(ByVal UserId As String, ByVal LogInLog As String, ByVal PageId As String, ByVal ReportId As String, ByVal TypeId As String,
                             ByVal DesId As String, ByVal ActivityId As String, ByVal Value As String, ByVal SessionId As String)
            Try
                'Creating Database Connection
                Dim odbUtil As New DBUtil()
                Dim ObjGetData As New Selectdata()
                Dim StrSql As String = String.Empty
                Dim i As New Integer
                Dim Sequence As Integer
                Dim logCount As Integer

                If LogInLog = "" Then
                    StrSql = "SELECT NVL(MAX(LOGINLOGC),1) LOGINLOGC FROM ACTIVITYLOG"
                    logCount = odbUtil.FillData(StrSql, Market1Connection) + 1

                    LogInLog = logCount.ToString()
                    HttpContext.Current.Session("MLogInLog") = LogInLog
                End If

                StrSql = "SELECT NVL(MAX(USERSEQUENCE),0) USERSEQUENCE FROM ACTIVITYLOG WHERE LOGINLOGC=" + LogInLog
                Sequence = odbUtil.FillData(StrSql, Market1Connection) + 1

                StrSql = "INSERT INTO ACTIVITYLOG(ACTIVITYLOGID,USERID,LOGINLOGC,USERSEQUENCE,PAGEID,REPORTID,REPORTTYPE,REPORTDES,ACTIVITYID,VALUE,SESSIONID,ACTIVITYTIME)  "
                StrSql = StrSql + "SELECT SEQACTIVITYLOGID.NEXTVAL,'" + UserId.ToString() + "','" + LogInLog.ToString() + "','" + Sequence.ToString() + "'," + PageId + ","
                StrSql = StrSql + "'" + ReportId + "','" + TypeId + "','" + DesId + "'," + ActivityId + ",'" + Value + "','" + SessionId + "',SYSDATE FROM DUAL "

                odbUtil.UpIns(StrSql, Market1Connection)
            Catch ex As Exception
                Throw New Exception("M1UpInsData:InsertLog:" + ex.Message.ToString())
            End Try
        End Sub
#End Region
    End Class
End Class
