Imports M1GetData
Imports System.Data

Partial Class Pages_Market1_PopUp_RepDet
    Inherits System.Web.UI.Page
#Region "GlobalData"
    Dim _iReportId As Integer
    Public Property ReportId() As Integer
        Get
            Return _iReportId
        End Get
        Set(ByVal Value As Integer)
            _iReportId = Value
        End Set
    End Property
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ReportId = Request.QueryString("ReportId").ToString()
            GetHeaderDetails()
            GetPageDetails(ReportId)

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetHeaderDetails()
        Dim ds As New DataSet()
        Dim objGetData As New M1GetData.Selectdata()
        Try
            If Request.QueryString("Type").ToString() = "Base" Then
                ds = objGetData.GetBaseReportsByRptId(ReportId)
                lblheading.text = "Market1 - Base Report Details"
            Else
                ds = objGetData.GetUserCustomReportsByRptId(ReportId)
                lblheading.text = "Market1 - Proprietary Report Details"
            End If

            If ds.Tables(0).Rows.Count > 0 Then
                lblRepID.Text = ds.Tables(0).Rows(0).Item("REPORTID").ToString()
                lblRepType.Text = ds.Tables(0).Rows(0).Item("RPTTYPE").ToString()
                lblRepDes.Text = ds.Tables(0).Rows(0).Item("REPORTNAME").ToString()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetPageDetails(ByVal RptID As String)
        If lblRepType.Text = "UNIFORM" Then
            SetUniformReport(RptID)
        ElseIf lblRepType.Text = "MIXED" Then
            SetMixedReport(RptID)
        End If
    End Sub
    Private Sub SetUniformReport(ByVal RptID As String)

        Dim objGetData As New Selectdata()
        Dim i As New Integer
        Dim j As New Integer
        Dim HeaderTr As New TableRow()
        Dim HeaderTd As New TableCell()
        Dim Tr As New TableRow()
        Dim Td As New TableCell()
        Dim Path As String = String.Empty
        Dim Link As HyperLink
        Dim hyd As HiddenField
        Dim dsRows As New DataSet
        Dim dsCol As New DataSet
        Dim dsFilter As New DataSet
        Dim Flag As Boolean
        Dim Des As String = String.Empty
        Dim k As Integer
        Dim tblFilter As Table
        Dim trRow As TableRow
        Dim trCol As TableCell
        Dim dv As New DataView
        Dim dvCol As New DataView
        Dim dt As New DataTable
        Dim lbl As New Label
        Try
            'Checking for Edit


            dsRows = objGetData.GetUsersReportRows(RptID)
            dsCol = objGetData.GetUsersReportColumns(RptID)
            dsFilter = objGetData.GetUsersReportFilters(RptID)


            dvCol = dsCol.Tables(0).DefaultView
            HeaderTr = New TableRow
            For j = 0 To dsCol.Tables(0).Rows.Count
                If j = 0 Then
                    tblFilter = New Table
                    dv = dsFilter.Tables(0).DefaultView
                    For k = 1 To dsFilter.Tables(0).Rows.Count
                        trRow = New TableRow
                        trCol = New TableCell
                        HeaderTd = New TableCell()
                        lbl = New Label()
                        lbl.CssClass = "Label"

                        dv.RowFilter = "FILTERSEQUENCE = " + k.ToString()
                        dt = dv.ToTable()
                        If (dt.Rows.Count > 0) Then
                            lbl.Text = dt.Rows(0).Item("FILTERVALUE").ToString()
                        Else
                        End If

                        'GetFilterLink(k, Link, RptID, hyd.ID, hyd.Value)
                        If lbl.Text = "" Then
                            lbl.Text = "Filter_" + k.ToString()
                        End If
                        trCol.Controls.Add(lbl)
                        trRow.Controls.Add(trCol)

                        tblFilter.Controls.Add(trRow)
                        HeaderTd.Controls.Add(tblFilter)
                        HeaderTdWLinkSetting(HeaderTd, "150px", "", "1")
                    Next

                Else
                    HeaderTd = New TableCell()
                    lbl = New Label()
                    lbl.CssClass = "Label"
                    dvCol.RowFilter = "COLUMNSEQUENCE = " + j.ToString()
                    dt = dvCol.ToTable()
                    If (dt.Rows.Count > 0) Then
                        'If Flag Then
                        If dt.Rows(0).Item("COLUMNVALUE").ToString() = "CAGR" Then
                            Des = dt.Rows(0).Item("COLUMNVALUE").ToString() + "(" + dt.Rows(0).Item("INPUTVALUE1").ToString() + "/" + dt.Rows(0).Item("INPUTVALUE2").ToString() + ")"
                        Else
                            Des = dt.Rows(0).Item("COLUMNVALUE").ToString()
                        End If
                        lbl.Text = Des
                        'End If
                    Else
                    End If

                    'GetColLink(j, Link, RptID, hyd.ID, hyd.Value)
                    If lbl.Text = "" Then
                        lbl.Text = "Column_" + j.ToString()
                    End If
                    HeaderTd.Controls.Add(lbl)
                    HeaderTdWLinkSetting(HeaderTd, "150px", "", "1")
                End If
                HeaderTd.Font.Size = 8.5
                HeaderTd.Font.Name = "Verdana"
                HeaderTd.Font.Bold = False
                HeaderTr.Controls.Add(HeaderTd)

            Next
            HeaderTr.Height = 30
            tblReportDetails.Controls.Add(HeaderTr)

            'SET THE UNIT & SUMMERY PART
            For i = 0 To 1
                Tr = New TableRow()
                For j = 0 To dsCol.Tables(0).Rows.Count
                    Td = New TableCell()
                    lbl = New Label
                    If j = 0 Then
                        HeaderTdSetting(Td, "150px", "", "1")
                        If i = 0 Then
                            lbl.Text = "UNIT"
                        Else
                            lbl.Text = "GRAND TOTAL"
                        End If
                    Else
                        HeaderTdSetting(Td, "150px", "", "1")
                        If i = 0 Then
                            If dsCol.Tables(0).Rows(j - 1).Item("COLUMNVALUETYPE").ToString() = "Formula" Then
                                lbl.Text = "(%)"
                            Else
                                lbl.Text = "(" + dsRows.Tables(0).Rows(0).Item("UNITSHRT").ToString() + ")"
                            End If
                            Td.Style.Add("text-align", "Center")
                        Else
                            'If dsCol.Tables(0).Rows(j - 1).Item("COLUMNVALUETYPE").ToString() = "Formula" Then
                            '    Dim BeginYearId As String = String.Empty
                            '    Dim EndYearId As String = String.Empty
                            '    BeginYearId = dsCol.Tables(0).Rows(j - 1).Item("INPUTVALUETYPE1").ToString()
                            '    EndYearId = dsCol.Tables(0).Rows(j - 1).Item("INPUTVALUETYPE2").ToString()                          
                            'End If
                            'Td.Style.Add("text-align", "Right")
                            'Td.Style.Add("padding-right", "5px")
                            'Td.CssClass = "AlterNateColor2"
                        End If

                    End If

                    Td.Font.Size = 8
                    Td.Font.Name = "Verdana"
                    Td.Font.Bold = False
                    Td.Controls.Add(lbl)
                    Tr.Controls.Add(Td)
                Next
                tblReportDetails.Controls.Add(Tr)
            Next

            dv = dsRows.Tables(0).DefaultView
            For i = 1 To dsRows.Tables(0).Rows.Count
                Tr = New TableRow
                For j = 0 To dsCol.Tables(0).Rows.Count

                    If j = 0 Then
                        Td = New TableCell()
                        lbl = New Label()

                        lbl.Text = "Row_" + i.ToString()

                        lbl.CssClass = "Label"

                        dv.RowFilter = "ROWSEQUENCE = " + i.ToString()
                        dt = dv.ToTable()
                        If (dt.Rows.Count > 0) Then
                            'If Flag Then
                            If dt.Rows(0).Item("ROWVALUE").ToString() <> "" Then
                                lbl.Text = dt.Rows(0).Item("ROWVALUE").ToString() + " (" + dt.Rows(0).Item("UNITSHRT").ToString() + ")"
                            End If

                            'End If
                        Else

                        End If

                        ' GetRowLink(i, Link, RptID, hyd.ID, hyd.Value)


                        Td.Controls.Add(lbl)
                        Td.Style.Add("text-align", "Left")
                        Td.Style.Add("padding-left", "5px")
                        HeaderTdWLinkSetting(Td, "150px", "", "1")
                    Else
                        Td = New TableCell()
                        Td.Text = "&nbsp;"
                        HeaderTdSetting(Td, "150px", "", "1")
                        Td.CssClass = "AlterNateColor2"
                    End If
                    Td.Font.Size = 8.5
                    Td.Font.Name = "Verdana"
                    Td.Font.Bold = False
                    Tr.Controls.Add(Td)
                Next
                Tr.Height = 30
                tblReportDetails.Controls.Add(Tr)
            Next


        Catch ex As Exception
            lblError.Text = "Error:SetReportFrameWork " + ex.Message.ToString()
        End Try
    End Sub
    Private Sub SetMixedReport(ByVal RptID As String)

        Dim objGetData As New Selectdata()
        Dim i As New Integer
        Dim j As New Integer
        Dim HeaderTr As New TableRow()
        Dim HeaderTd As New TableCell()
        Dim Tr As New TableRow()
        Dim Td As New TableCell()
        Dim Path As String = String.Empty
        Dim Link As HyperLink
        Dim hyd As HiddenField
        Dim dsRows As New DataSet
        Dim dsCol As New DataSet
        Dim dsFilter As New DataSet
        Dim Flag As Boolean
        Dim Des As String = String.Empty
        Dim k As Integer
        Dim tblFilter As Table
        Dim trRow As TableRow
        Dim trCol As TableCell
        Dim dv As New DataView
        Dim dvCol As New DataView
        Dim dt As New DataTable
        Dim lbl As New Label
        Try
            'Checking for Edit


            dsRows = objGetData.GetUsersReportRows(RptID)
            dsCol = objGetData.GetUsersReportColumns(RptID)
            dsFilter = objGetData.GetUsersReportFilters(RptID)


            dvCol = dsCol.Tables(0).DefaultView
            HeaderTr = New TableRow
            For j = 0 To dsCol.Tables(0).Rows.Count
                If j = 0 Then
                    tblFilter = New Table
                    dv = dsFilter.Tables(0).DefaultView
                    For k = 1 To dsFilter.Tables(0).Rows.Count
                        trRow = New TableRow
                        trCol = New TableCell
                        HeaderTd = New TableCell()
                        lbl = New Label()
                        lbl.CssClass = "Label"

                        dv.RowFilter = "FILTERSEQUENCE = " + k.ToString()
                        dt = dv.ToTable()
                        If (dt.Rows.Count > 0) Then
                            lbl.Text = dt.Rows(0).Item("FILTERVALUE").ToString()
                        Else
                        End If

                        'GetFilterLink(k, Link, RptID, hyd.ID, hyd.Value)
                        If lbl.Text = "" Then
                            lbl.Text = "Filter_" + k.ToString()
                        End If
                        trCol.Controls.Add(lbl)
                        trRow.Controls.Add(trCol)

                        tblFilter.Controls.Add(trRow)
                        HeaderTd.Controls.Add(tblFilter)
                        HeaderTdWLinkSetting(HeaderTd, "150px", "", "1")
                    Next

                Else
                    HeaderTd = New TableCell()
                    lbl = New Label()
                    lbl.CssClass = "Label"
                    dvCol.RowFilter = "COLUMNSEQUENCE = " + j.ToString()
                    dt = dvCol.ToTable()
                    If (dt.Rows.Count > 0) Then
                        'If Flag Then
                        If dt.Rows(0).Item("COLUMNVALUE").ToString() = "CAGR" Then
                            Des = dt.Rows(0).Item("COLUMNVALUE").ToString() + "(" + dt.Rows(0).Item("INPUTVALUE1").ToString() + "/" + dt.Rows(0).Item("INPUTVALUE2").ToString() + ")"
                        Else
                            Des = dt.Rows(0).Item("COLUMNVALUE").ToString()
                        End If
                        lbl.Text = Des
                        'End If
                    Else
                    End If

                    'GetColLink(j, Link, RptID, hyd.ID, hyd.Value)
                    If lbl.Text = "" Then
                        lbl.Text = "Column_" + j.ToString()
                    End If
                    HeaderTd.Controls.Add(lbl)
                    HeaderTdWLinkSetting(HeaderTd, "150px", "", "1")
                End If
                HeaderTd.Font.Size = 8.5
                HeaderTd.Font.Name = "Verdana"
                HeaderTd.Font.Bold = False
                HeaderTr.Controls.Add(HeaderTd)

            Next
            HeaderTr.Height = 30
            tblReportDetails.Controls.Add(HeaderTr)

            dv = dsRows.Tables(0).DefaultView
            For i = 1 To dsRows.Tables(0).Rows.Count
                Tr = New TableRow
                For j = 0 To dsCol.Tables(0).Rows.Count

                    If j = 0 Then
                        Td = New TableCell()
                        lbl = New Label()

                        lbl.Text = "Row_" + i.ToString()

                        lbl.CssClass = "Label"

                        dv.RowFilter = "ROWSEQUENCE = " + i.ToString()
                        dt = dv.ToTable()
                        If (dt.Rows.Count > 0) Then
                            'If Flag Then
                            If dt.Rows(0).Item("ROWVALUE").ToString() <> "" Then
                                lbl.Text = dt.Rows(0).Item("ROWVALUE").ToString() + " (" + dt.Rows(0).Item("UNITSHRT").ToString() + ")"
                            End If

                            'End If
                        Else

                        End If

                        ' GetRowLink(i, Link, RptID, hyd.ID, hyd.Value)


                        Td.Controls.Add(lbl)
                        Td.Style.Add("text-align", "Left")
                        Td.Style.Add("padding-left", "5px")
                        HeaderTdWLinkSetting(Td, "150px", "", "1")
                    Else
                        Td = New TableCell()
                        Td.Text = "&nbsp;"
                        HeaderTdSetting(Td, "150px", "", "1")
                        Td.CssClass = "AlterNateColor2"
                    End If
                    Td.Font.Size = 8.5
                    Td.Font.Name = "Verdana"
                    Td.Font.Bold = False
                    Tr.Controls.Add(Td)
                Next
                Tr.Height = 30
                tblReportDetails.Controls.Add(Tr)
            Next


        Catch ex As Exception
            lblError.Text = "Error:SetReportFrameWork " + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub HeaderTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal ColSpan As String)
        Try
            Td.Text = HeaderText
            Td.ColumnSpan = ColSpan
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.CssClass = "TdHeading"
            Td.Height = 20
            Td.Font.Size = 10
            Td.Font.Bold = True
            Td.HorizontalAlign = HorizontalAlign.Center



        Catch ex As Exception
            lblError.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub HeaderTdWLinkSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal ColSpan As String)
        Try
            Td.ColumnSpan = ColSpan
            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.CssClass = "TdHeading"
            Td.Height = 20
            Td.Font.Size = 10
            Td.Font.Bold = True
            Td.HorizontalAlign = HorizontalAlign.Center



        Catch ex As Exception
            lblError.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
        End Try
    End Sub
End Class
