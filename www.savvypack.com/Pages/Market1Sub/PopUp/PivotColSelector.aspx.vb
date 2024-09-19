Imports System.Data
Imports System.Data.OleDb
Imports System
Imports M1SubGetData
Imports M1SubUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Market1_PopUp_PivotColSelector
    Inherits System.Web.UI.Page

    Protected Sub btnSumitt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSumitt.Click
        Dim RptID As String = Request.QueryString("RptID").ToString()
        Dim Seq As String = Request.QueryString("Seq").ToString()
        Dim ColType As String = String.Empty
        Dim ColVal As String = String.Empty
        Dim UDFV1 As String = String.Empty
        Dim UDFV2 As String = String.Empty
        Dim CAGRYearId1 As String = String.Empty
        Dim CAGRYearId2 As String = String.Empty
        Dim objUpIns As New UpdateInsert()
        Dim Value As String = String.Empty
        Dim hidValue As Integer = CInt(Request.QueryString("hidValue"))
        Dim hidId As String = Request.QueryString("hidId")
        hidColDes.Value = Request.QueryString("Id").ToString()
        hidColId.Value = Request.QueryString("hidId").ToString()
        Dim colValueId As String = String.Empty
        Try

            If ddlColType.SelectedItem.Text = "Year" Then
                ColType = "Year"
                ColVal = ddlYear.SelectedItem.Text.ToString()
                UDFV1 = ""
                UDFV2 = ""
                CAGRYearId1 = ""
                CAGRYearId2 = ""
                Value = ColVal
                colValueId = ddlYear.SelectedValue
            ElseIf ddlColType.SelectedItem.Text = "CAGR" Then
                ColType = "Formula"
                ColVal = "CAGR"
                UDFV1 = ddlBYear.SelectedItem.ToString()
                UDFV2 = ddlEYear.SelectedItem.ToString()
                Value = ColVal + "(" + UDFV2 + "/" + UDFV1 + ")"
                colValueId = "''"
                CAGRYearId1 = ddlBYear.SelectedValue.ToString()
                CAGRYearId2 = ddlEYear.SelectedValue.ToString()
            End If

            Dim dsRptCols As New DataSet
            Dim objGetData As New Selectdata
            Dim pSubTypeid As String = "null"
            Dim pSubTypeValue As String = String.Empty
            Dim pSelId As String = "null"
            Dim pSelValue As String = ""
            Dim dr() As DataRow
            Dim rptName As String
            dsRptCols = objGetData.GetUsersDynamicReportCols(RptID)
            rptName = objGetData.GetReportName(RptID)

            'If dsRptCols.Tables(0).Rows.Count > 0 Then
            '    dr = dsRptCols.Tables(0).Select("COLUMNSEQUENCE=" + Seq)
            '    If dr.Length <> 0 Then
            '        pSubTypeid = IIf(dr(0).Item("COLUMNTYPEID").ToString() = "", "null", dr(0).Item("COLUMNTYPEID").ToString())
            '        pSubTypeValue = IIf(dr(0).Item("COLUMNVALUETYPE").ToString() = "", "", dr(0).Item("COLUMNVALUETYPE").ToString())
            '        pSelId = IIf(dr(0).Item("COLUMNVALUEID").ToString() = "", "null", dr(0).Item("COLUMNVALUEID").ToString())
            '        pSelValue = IIf(dr(0).Item("COLUMNVALUE").ToString() = "", "", dr(0).Item("COLUMNVALUE").ToString())
            '    End If
            'End If

            If hidValue <> 0 Then
                If Request.QueryString("isTemp").ToString() = "Y" Then
                    objUpIns.UpdateColDetailsTemp(Seq, ColType, ColVal, UDFV1, UDFV2, hidValue, ddlColType.SelectedValue, colValueId, CAGRYearId1, CAGRYearId2, RptID)
                ElseIf Request.QueryString("isTemp").ToString() = "N" Then
                    objUpIns.UpdateColDetails(Seq, ColType, ColVal, UDFV1, UDFV2, hidValue, ddlColType.SelectedValue, colValueId, CAGRYearId1, CAGRYearId2, RptID)
                End If
            End If

            Dim dsRpt As New DataSet
            dsRpt = objGetData.GetReportDetails(RptID)
            If ddlColType.SelectedItem.Text = "CAGR" Then
                objUpIns.InsertLog(Session("UserId"), Session("MLogInLog"), "11", RptID.ToString(), "Pivot", dsRpt.Tables(0).Rows(0)("RPTTYPEDES").ToString(), "15", "", Session.SessionID)
            Else
                objUpIns.InsertLog(Session("UserId"), Session("MLogInLog"), "11", RptID.ToString(), "Pivot", dsRpt.Tables(0).Rows(0)("RPTTYPEDES").ToString(), "16", colValueId, Session.SessionID)
            End If
            ClientScript.RegisterStartupScript(Me.GetType(), "ColSel", "ColSelection('" + Value.ToString() + "','" + hidValue.ToString() + "','" + hidId + "'," + Seq + ");", True)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetAndFillYears(ByVal ddl As DropDownList)
        Dim objGetDate As New Selectdata()
        Dim ds As New DataSet()
        Dim dsYear As New DataSet()
        Try
            If ddlColType.SelectedValue = 1 Then
                dsYear = objGetDate.GetGroupYears(Session("M1SubGroupId"))
                If dsYear.Tables(0).Rows.Count > 0 Then
                    ds = objGetDate.GetColSQL_Group(ddlColType.SelectedValue.ToString(), dsYear.Tables(0).Rows(0).Item("YEARIDMIN").ToString(), dsYear.Tables(0).Rows(0).Item("YEARIDMAX").ToString())
                Else
                    ds = objGetDate.GetColSQL(ddlColType.SelectedValue.ToString())

                End If
            Else
                ds = objGetDate.GetColSQL(ddlColType.SelectedValue.ToString())
            End If



            With ddl
                .DataSource = ds
                .DataTextField = "VALUE"
                .DataValueField = "ID"
                .DataBind()
            End With


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetAndFillYears" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlColType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlColType.SelectedIndexChanged
        Try
            ReportType()
        Catch ex As Exception
            _lErrorLble.Text = "Error:ddlColType_SelectedIndexChanged" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' ReportType()
            getColSelector()
        End If
    End Sub
    Protected Sub getColSelector()
        Dim objGetDate As New Selectdata()
        Dim ds As New DataSet()
        Dim lst As New ListItem
        Dim RptType As String = String.Empty
        Dim dsCols As New DataSet
        Try
            dsCols = objGetDate.GetPivotReportsCols(Request.QueryString("RptId").ToString(), Request.QueryString("Seq").ToString())
            If dsCols.Tables(0).Rows(0).Item("COLUMNVALUETYPE").ToString() = "Year" Then
                ds = objGetDate.GetColsSelector("1")
            Else
                ds = objGetDate.GetColsSelector("2")
            End If

            With ddlColType
                .DataSource = ds
                .DataTextField = "COLDES"
                .DataValueField = "COLTYPEID"
                .DataBind()
            End With

            If dsCols.Tables(0).Rows(0).Item("COLUMNVALUETYPE").ToString() = "Year" Then
                divYear.Visible = True
                divCAGR.Visible = False
                GetAndFillYears(ddlYear)
            ElseIf dsCols.Tables(0).Rows(0).Item("COLUMNVALUETYPE").ToString() = "Formula" Then
                divYear.Visible = False
                divCAGR.Visible = True
                'GetAndFillYears(ddlBYear)
                'GetAndFillYears(ddlEYear)
                GetAndFillByColumns(ddlBYear)
                GetAndFillByColumns(ddlEYear)
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCOlSelector" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub ReportType()
        If ddlColType.SelectedItem.Text = "Year" Then
            divYear.Visible = True
            divCAGR.Visible = False
            GetAndFillYears(ddlYear)
        ElseIf ddlColType.SelectedItem.Text = "CAGR" Then
            divYear.Visible = False
            divCAGR.Visible = True
            'GetAndFillYears(ddlBYear)
            'GetAndFillYears(ddlEYear)
            GetAndFillByColumns(ddlBYear)
            GetAndFillByColumns(ddlEYear)
        End If
    End Sub

    Protected Sub GetAndFillByColumns(ByVal ddl As DropDownList)
        Dim objGetDate As New Selectdata()
        Dim ds As New DataSet()
        Dim strCluase As String = String.Empty
        Dim isTemp As String = String.Empty
        Try
            isTemp = Request.QueryString("isTemp")
            strCluase = " AND CLUASE1=" + Request.QueryString("RptID").ToString() + " AND CLUASE2<" + Request.QueryString("Seq").ToString()
            If isTemp = "Y" Then
                ds = objGetDate.GetColSQLWithClause(ddlColType.SelectedValue.ToString(), strCluase)
            Else
                ds = objGetDate.GetColSQLWithEdiClause(ddlColType.SelectedValue.ToString(), strCluase)
            End If
            'ds = objGetDate.GetYearsColumnOnly(Request.QueryString("RptID").ToString(), Request.QueryString("Seq").ToString())


            With ddl
                .DataSource = ds
                .DataTextField = "VALUE"
                .DataValueField = "ID"
                .DataBind()
            End With


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetAndFillYears" + ex.Message.ToString()
        End Try
    End Sub
End Class
