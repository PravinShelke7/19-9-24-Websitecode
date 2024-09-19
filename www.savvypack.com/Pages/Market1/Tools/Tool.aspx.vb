Imports System.Data
Imports System.Data.OleDb
Imports System
Imports M1GetData
Imports M1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Market1_Tools_Tool
    Inherits System.Web.UI.Page


#Region "Browser Refresh Check"
    Dim objRefresh As zCon.Net.Refresh

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        objRefresh = New zCon.Net.Refresh("_PAGES_MARKET1_TOOLS_TOOL")
    End Sub

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        objRefresh.Render(Page)
    End Sub

#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Back") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("../Errors/Error.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "")
            End If

            If Not IsPostBack Then
                hidBaseRpt.Value = "0"
                hidPropRpt.Value = "0"
                'GetPCases()
                ViewState("Edit") = "N"
            End If

        Catch ex As Exception
            lblError.Text = "Error:Page_Load" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPCases()
        Dim ds As New DataSet
        Dim objeGetData As New M1GetData.Selectdata()
        Try
            ds = objeGetData.GetUserCustomReports(Session("UserId").ToString())
            Session("UserReport") = ds
            'With ddlPCases
            '    .DataSource = ds
            '    .DataTextField = "REPORTDES"
            '    .DataValueField = "USERREPORTID"
            '    .DataBind()
            'End With
        Catch ex As Exception
            lblError.Text = "Error:GetReportDetails:" + ex.Message.ToString()
        End Try
    End Sub



    Protected Sub btnSubmitt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmitt.Click
        Dim ColCnt As String = String.Empty
        Dim RowCnt As String = String.Empty
        Dim ReportName As String = String.Empty
        Dim FilterType As String = String.Empty
        Dim FilterValue As String = String.Empty
        Dim objUpIns As New UpdateInsert()
        Dim objGetData As New Selectdata()
        Dim ReportId As String = String.Empty
        Dim ReportFact As String = String.Empty
        Dim filterCnt As String = String.Empty
        Try
            ColCnt = txtCol.Text.Trim()
            RowCnt = txtRw.Text.Trim()
            filterCnt = txtFilter.Text.Trim()
            ReportName = txtRptName.Text.Trim()

            hidReportType.Value = "MIXED"
            If Not objRefresh.IsRefresh Then
                ReportId = objUpIns.AddReportDetails(ReportName, Session("UserId").ToString(), "MIXED", ReportFact, filterCnt, RowCnt, ColCnt, "0", "Null", "Null")
                SetReportFrameWork(RowCnt, ColCnt, txtFilter.Text, ReportId)
                btnSubmitt.Enabled = False
                btnAddNew.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnSubmitt_Click " + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub SetReportFrameWork(ByVal RowCnt As String, ByVal ColCnt As String, ByVal filterCnt As String, ByVal RptID As String)
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
        Try
            'Checking for Edit
            If ViewState("Edit") = "Y" Then
                Flag = True
            Else
                Flag = False
            End If

            dsRows = objGetData.GetUsersReportRows(RptID)
            dsCol = objGetData.GetUsersReportColumns(RptID)
            dsFilter = objGetData.GetUsersReportFilters(RptID)

            dvCol = dsCol.Tables(0).DefaultView
            HeaderTr = New TableRow
            For j = 0 To ColCnt
                If j = 0 Then
                    tblFilter = New Table
                    dv = dsFilter.Tables(0).DefaultView
                    For k = 1 To filterCnt
                        trRow = New TableRow
                        trCol = New TableCell
                        HeaderTd = New TableCell()
                        hyd = New HiddenField
                        Link = New HyperLink
                        Link.ID = "Filter_" + k.ToString()
                        hyd.ID = "Filter_ID_" + k.ToString()

                        Link.CssClass = "LinkM"

                        dv.RowFilter = "FILTERSEQUENCE = " + k.ToString()
                        dt = dv.ToTable()
                        If (dt.Rows.Count > 0) Then
                            hyd.Value = dt.Rows(0).Item("USERREPORTFILTERID").ToString()
                            ' If Flag Then
                            Link.Text = dt.Rows(0).Item("FILTERVALUE").ToString()
                            'End If
                        Else
                            hyd.Value = 0
                        End If

                        GetFilterLink(k, Link, RptID, hyd.ID, hyd.Value)
                        If Link.Text = "" Then
                            Link.Text = "Filter_" + k.ToString()
                        End If
                        trCol.Controls.Add(Link)
                        trCol.Controls.Add(hyd)
                        trRow.Controls.Add(trCol)

                        tblFilter.Controls.Add(trRow)
                        HeaderTd.Controls.Add(tblFilter)
                        HeaderTdWLinkSetting(HeaderTd, "150px", "", "1")
                    Next

                Else
                    HeaderTd = New TableCell()
                    hyd = New HiddenField
                    Link = New HyperLink



                    dvCol.RowFilter = "COLUMNSEQUENCE = " + j.ToString()
                    dt = dvCol.ToTable()
                    If (dt.Rows.Count > 0) Then
                        hyd.Value = dt.Rows(0).Item("USERREPORTCOLUMNID").ToString()
                        'If Flag Then
                        If dt.Rows(0).Item("COLUMNVALUE").ToString() = "CAGR" Then
                            Des = dt.Rows(0).Item("COLUMNVALUE").ToString() + "(" + dt.Rows(0).Item("INPUTVALUE1").ToString() + "/" + dt.Rows(0).Item("INPUTVALUE2").ToString() + ")"
                        Else
                            Des = dt.Rows(0).Item("COLUMNVALUE").ToString()
                        End If
                        Link.Text = Des
                        'End If
                    Else
                        hyd.Value = 0
                    End If

                    Link.ID = "Column_" + j.ToString()
                    hyd.ID = "Column_ID_" + j.ToString()

                    Link.CssClass = "LinkM"
                    GetColLink(j, Link, RptID, hyd.ID, hyd.Value)
                    If Link.Text = "" Then
                        Link.Text = "Column_" + j.ToString()
                    End If
                    HeaderTd.Controls.Add(Link)
                    HeaderTd.Controls.Add(hyd)
                    HeaderTdWLinkSetting(HeaderTd, "150px", "", "1")
                End If
                HeaderTd.Font.Size = 8.5
                HeaderTd.Font.Name = "Verdana"
                HeaderTd.Font.Bold = False
                HeaderTr.Controls.Add(HeaderTd)

            Next
            HeaderTr.Height = 30
            tblCAGR.Controls.Add(HeaderTr)

            dv = dsRows.Tables(0).DefaultView
            For i = 1 To RowCnt
                Tr = New TableRow
                For j = 0 To ColCnt

                    If j = 0 Then
                        Td = New TableCell()
                        Link = New HyperLink

                        Link.Text = "Row_" + i.ToString()

                        Link.ID = "Row_" + i.ToString()
                        Link.CssClass = "LinkM"
                        hyd = New HiddenField
                        hyd.ID = "Row_ID_" + i.ToString()


                        dv.RowFilter = "ROWSEQUENCE = " + i.ToString()
                        dt = dv.ToTable()
                        If (dt.Rows.Count > 0) Then
                            hyd.Value = dt.Rows(0).Item("USERREPORTROWID").ToString()
                            'If Flag Then
                            If dt.Rows(0).Item("ROWVALUE").ToString() <> "" Then
                                Link.Text = dt.Rows(0).Item("ROWVALUE").ToString() + " (" + dt.Rows(0).Item("UNITSHRT").ToString() + ")"
                            End If

                            'End If
                        Else
                            hyd.Value = 0
                        End If

                        GetRowLink(i, Link, RptID, hyd.ID, hyd.Value)


                        Td.Controls.Add(Link)
                        Td.Controls.Add(hyd)
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
                tblCAGR.Controls.Add(Tr)
            Next


        Catch ex As Exception
            lblError.Text = "Error:SetReportFrameWork " + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub SetSpecialReportFrameWork(ByVal RowCnt As String, ByVal ColCnt As String, ByVal filterCnt As String, ByVal RptID As String)
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
        Dim lbl As Label
        Try
            'Checking for Edit
            If ViewState("Edit") = "Y" Then
                Flag = True
            Else
                Flag = False
            End If

            dsRows = objGetData.GetUsersReportRows(RptID)
            dsCol = objGetData.GetUsersReportColumns(RptID)
            dsFilter = objGetData.GetUsersReportFilters(RptID)

            dvCol = dsCol.Tables(0).DefaultView
            HeaderTr = New TableRow
            For j = 0 To ColCnt
                If j = 0 Then
                    tblFilter = New Table
                    dv = dsFilter.Tables(0).DefaultView
                    For k = 1 To filterCnt
                        trRow = New TableRow
                        trCol = New TableCell
                        HeaderTd = New TableCell()
                        hyd = New HiddenField
                        Link = New HyperLink
                        Link.ID = "Filter_" + k.ToString()
                        hyd.ID = "Filter_ID_" + k.ToString()

                        Link.CssClass = "LinkM"

                        dv.RowFilter = "FILTERSEQUENCE = " + k.ToString()
                        dt = dv.ToTable()
                        If (dt.Rows.Count > 0) Then
                            hyd.Value = dt.Rows(0).Item("USERREPORTFILTERID").ToString()
                            ' If Flag Then
                            Link.Text = dt.Rows(0).Item("FILTERVALUE").ToString()
                            'End If
                        Else
                            hyd.Value = 0
                        End If

                        GetFilterLink(k, Link, RptID, hyd.ID, hyd.Value)
                        If Link.Text = "" Then
                            Link.Text = "Filter_" + k.ToString()
                        End If
                        trCol.Controls.Add(Link)
                        trCol.Controls.Add(hyd)
                        trRow.Controls.Add(trCol)

                        tblFilter.Controls.Add(trRow)
                        HeaderTd.Controls.Add(tblFilter)
                        HeaderTdWLinkSetting(HeaderTd, "150px", "", "1")
                    Next

                Else
                    HeaderTd = New TableCell()
                    hyd = New HiddenField
                    Link = New HyperLink



                    dvCol.RowFilter = "COLUMNSEQUENCE = " + j.ToString()
                    dt = dvCol.ToTable()
                    If (dt.Rows.Count > 0) Then
                        hyd.Value = dt.Rows(0).Item("USERREPORTCOLUMNID").ToString()
                        'If Flag Then
                        If dt.Rows(0).Item("COLUMNVALUE").ToString() = "CAGR" Then
                            Des = dt.Rows(0).Item("COLUMNVALUE").ToString() + "(" + dt.Rows(0).Item("INPUTVALUE1").ToString() + "/" + dt.Rows(0).Item("INPUTVALUE2").ToString() + ")"
                        Else
                            Des = dt.Rows(0).Item("COLUMNVALUE").ToString()
                        End If
                        Link.Text = Des
                        'End If
                    Else
                        hyd.Value = 0
                    End If

                    Link.ID = "Column_" + j.ToString()
                    hyd.ID = "Column_ID_" + j.ToString()

                    Link.CssClass = "LinkM"
                    GetColLink(j, Link, RptID, hyd.ID, hyd.Value)
                    If Link.Text = "" Then
                        Link.Text = "Column_" + j.ToString()
                    End If
                    HeaderTd.Controls.Add(Link)
                    HeaderTd.Controls.Add(hyd)
                    HeaderTdWLinkSetting(HeaderTd, "150px", "", "1")
                End If
                HeaderTd.Font.Size = 8.5
                HeaderTd.Font.Name = "Verdana"
                HeaderTd.Font.Bold = False
                HeaderTr.Controls.Add(HeaderTd)

            Next
            HeaderTr.Height = 30
            tblCAGR.Controls.Add(HeaderTr)

            dv = dsRows.Tables(0).DefaultView
            'SET THE UNIT & SUMMERY PART
            For i = 0 To 1
                Tr = New TableRow()
                For j = 0 To ColCnt
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
                        HeaderTdSetting(Td, "100px", "", "1")
                        If i = 0 Then
                            lbl.ID = "lbl_" + j.ToString()
                            If dsCol.Tables(0).Rows(j - 1).Item("COLUMNVALUETYPE").ToString() = "Formula" Then
                                lbl.Text = "(%)"
                            Else
                                lbl.Text = " (" + dsRows.Tables(0).Rows(0).Item("UNITSHRT").ToString() + ")"
                                hidUnitShort.value = lbl.Text
                            End If
                            Td.Style.Add("text-align", "Center")
                        Else
                            Td.Style.Add("text-align", "Right")
                            Td.Style.Add("padding-right", "5px")
                            Td.CssClass = "AlterNateColor2"
                        End If

                    End If

                    Td.Font.Size = 8.5
                    Td.Font.Name = "Verdana"
                    Td.Font.Bold = False
                    Td.Controls.Add(lbl)
                    Tr.Controls.Add(Td)
                Next
                tblCAGR.Controls.Add(Tr)
            Next


            For i = 1 To dsRows.Tables(0).Rows.Count
                Tr = New TableRow
                For j = 0 To ColCnt

                    If j = 0 Then
                        Td = New TableCell()
                        lbl = New Label
                        dv.RowFilter = "ROWSEQUENCE = " + i.ToString()
                        dt = dv.ToTable()
                        If (dt.Rows.Count > 0) Then
                            If dt.Rows(0).Item("ROWVALUE").ToString() <> "" Then
                                lbl.Text = dt.Rows(0).Item("ROWVALUE").ToString() '+ " (" + dt.Rows(0).Item("UNITSHRT").ToString() + ")"
                            End If
                        End If

                        'Td.Font.Size = 8
                        'Td.Font.Bold = False
                        lbl.CssClass = "Label"
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
                tblCAGR.Controls.Add(Tr)
            Next


        Catch ex As Exception
            lblError.Text = "Error:SetReportFrameWork " + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetFilterLink(ByVal Seq As String, ByVal Link As HyperLink, ByVal RptId As String, ByVal hidId As String, ByVal hidValue As String)
        Dim Path As String
        Try
            Path = "../PopUp/FilterSelector.aspx?RptId=" + RptId + "&Seq=" + Seq.ToString() + "&Id=" + Link.ClientID + "&hidId=" + hidId + ""
            Link.NavigateUrl = "javascript:ShowPopWindow('" + Path + "','" + hidId + "')"
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GetColLink(ByVal Seq As String, ByVal Link As HyperLink, ByVal RptId As String, ByVal hidId As String, ByVal hidValue As String)
        Dim Path As String
        Try
            Path = "../PopUp/ColSelector.aspx?RptId=" + RptId + "&Seq=" + Seq.ToString() + "&Id=" + Link.ClientID + "&hidId=" + hidId + ""
            Link.NavigateUrl = "javascript:ShowPopWindow('" + Path + "','" + hidId + "')"
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetRowLink(ByVal Seq As String, ByVal Link As HyperLink, ByVal RptId As String, ByVal hidId As String, ByVal hidValue As String)
        Dim Path As String

        Dim RowVal2D As String = String.Empty
        Dim RegionSetId As String = String.Empty
        Dim Curr As String = String.Empty


        Try

            Path = "../PopUp/RowSelector.aspx?RptId=" + RptId + "&Seq=" + Seq.ToString() + "&Id=" + Link.ClientID + "&hidId=" + hidId
            Link.NavigateUrl = "javascript:ShowPopWindow('" + Path + "','" + hidId + "')"
        Catch ex As Exception

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

    Protected Sub InnerTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal Align As String)
        Try

            If Width <> "" Then
                Td.Style.Add("width", Width)
            End If
            Td.Style.Add("text-align", Align)
            If Align = "Left" Then
                Td.Style.Add("padding-left", "5px")
            End If
            If Align = "Right" Then
                Td.Style.Add("padding-right", "5px")
            End If
        Catch ex As Exception
            lblError.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSubmitt2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmitt2.Click
        Dim ReportName As String = String.Empty
        Dim FilterType As String = String.Empty
        Dim FilterValue As String = String.Empty
        Dim ReportId As String = String.Empty
        Dim objUpIns As New UpdateInsert()
        Dim RowValue As String = String.Empty
        Dim Curr As String = String.Empty
        Dim ReportType As String = String.Empty
        Dim ReportTFact As String = String.Empty
        Dim dsReport As DataSet
        Dim objGetData As New M1GetData.Selectdata()
        Try
            If ViewState("Edit") = "Y" Then
                ReportId = hidPropRpt.Value 'ddlPCases.SelectedValue
                ReportName = txtRptName.Text.Trim()
                dsReport = objGetData.GetUserCustomReportsByRptId(ReportId)

                objUpIns.EditReport(dsReport.Tables(0).Rows(0)("RPTTYPE").ToString(), ReportId, ReportName, FilterType, FilterValue, ReportTFact, RowValue, Curr)
            End If
            hidReportType.Value = "MIXED"
            btnSubmitt2.Enabled = False

            SetReportFrameWork(txtRw.Text.Trim().ToString(), txtCol.Text.ToString().Trim(), txtFilter.Text.ToString().Trim(), ReportId)
            'EnableDisable("")
            btnAddNew.Visible = True
        Catch ex As Exception
            lblError.Text = "Error:btnSubmitt2_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try

            If hidPropRpt.Value = "0" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select Proprietary Report');", True)
            Else
                GetReportDetails(hidPropRpt.Value)
                btnAddNew.Visible = False
                'divCreate.Visible = True
                EnableDisable("ED")
            End If
            lnkReports.Text = hidPropRptDes.Value

        Catch ex As Exception
            lblError.Text = "Error:btnEdit_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetReportDetails(ByVal RptID As String)
        ' Dim RptID As String = String.Empty
        Dim dsReport As New DataSet
        Dim dsRpt As New DataSet
        Dim dsRows As New DataSet
        Dim dsCol As New DataSet
        Dim dsFilter As New DataSet
        Dim objGetData As New M1GetData.Selectdata()
        ' Dim arr() As String
        Dim dr As DataRow()

        Try
            ' RptID = ddl.SelectedValue.ToString()
            dsReport = objGetData.GetUserCustomReportsByRptId(RptID)
            dsRows = objGetData.GetUsersReportRows(RptID)
            dsCol = objGetData.GetUsersReportColumns(RptID)
            dsFilter = objGetData.GetUsersReportFilters(RptID)
            dsRpt = Session("UserReport")

            If dsReport.Tables(0).Rows(0)("RPTTYPE").ToString() = "UNIFORM" Then
                txtSpecialReportName.Text = dsReport.Tables(0).Rows(0)("REPORTNAME").ToString()
                txtSpecialCols.Text = dsCol.Tables(0).Rows.Count.ToString()
                txtSpecialFilters.Text = dsFilter.Tables(0).Rows.Count.ToString()
                divCreateSpecial.Visible = True
                BindRegionSet()
                BindUnits()
                ddlRegionSet.SelectedValue = dsReport.Tables(0).Rows(0)("REGIONSETID").ToString()
                ddlUnits.SelectedValue = dsRows.Tables(0).Rows(0)("UNITID").ToString()
                If dsReport.Tables(0).Rows(0)("RPTTYPEDES").ToString() = "REGION" Then
                    ddlSpecialType.SelectedValue = "REGION"
                ElseIf dsReport.Tables(0).Rows(0)("RPTTYPEDES").ToString() = "CNTRY" Then
                    ddlSpecialType.SelectedValue = "CNTRY"
                    rowRegions.Visible = True
                    BindRegions()
                    ddlRegions.SelectedValue = dsReport.Tables(0).Rows(0)("REGIONID").ToString()
                    ddlRegions.Enabled = True
                End If
                txtSpecialCols.Enabled = False
                txtSpecialFilters.Enabled = False
                ddlSpecialType.Enabled = True
                ddlRegionSet.Enabled = True
                ddlUnits.Enabled = True
                btnSpecialSubmit2.Visible = True
                btnSpecialSubmit.Visible = False
                btnSpecialSubmit2.Enabled = True
            ElseIf dsReport.Tables(0).Rows(0)("RPTTYPE").ToString() = "MIXED" Then
                txtRptName.Text = dsReport.Tables(0).Rows(0)("REPORTNAME").ToString()
                txtCol.Text = dsCol.Tables(0).Rows.Count.ToString()
                txtRw.Text = dsRows.Tables(0).Rows.Count.ToString()
                txtFilter.Text = dsFilter.Tables(0).Rows.Count.ToString()
                btnSubmitt.Enabled = False
                txtCol.Enabled = False
                txtRw.Enabled = False
                txtFilter.Enabled = False
                divCreate.Visible = True
                btnSubmitt2.Visible = True
                btnSubmitt.Visible = False
                btnSubmitt2.Enabled = True
            End If
            ViewState("Edit") = "Y"

        Catch ex As Exception
            lblError.Text = "Error:GetReportDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Try
            divCreate.Visible = True
            txtCol.Text = String.Empty
            txtRw.Text = String.Empty
            txtFilter.Text = String.Empty
            txtRptName.Text = String.Empty
            btnSubmitt.Visible = True
            btnSubmitt.Enabled = True
            btnSubmitt2.Visible = False
            txtCol.Enabled = True
            txtRw.Enabled = True
            txtFilter.Enabled = True
            EnableDisable("CRM")
        Catch ex As Exception
            lblError.Text = "Error:btnCreate_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim ReportId As String = String.Empty
        Dim objUpIns As New UpdateInsert()
        Try
            If Not objRefresh.IsRefresh Then
                If hidPropRpt.Value = "0" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select Proprietary Report');", True)
                Else
                    ReportId = hidPropRpt.Value 'ddlPCases.SelectedValue.ToString()
                    objUpIns.DeleteReport(ReportId)
                    GetPCases()
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinDelete", "alert('Report#" + ReportId.ToString() + " deleted successfully');", True)
                End If
            End If
            lnkReports.Text = "Select Proprietary Report"
            hidPropRpt.Value = "0"
        Catch ex As Exception
            lblError.Text = "Error:btnDelete_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Dim ReportId As String = String.Empty
        Dim NewReportId As String = String.Empty
        Dim objUpIns As New UpdateInsert()
        Try
            ReportId = hidPropRpt.Value 'ddlPCases.SelectedValue.ToString()
            If Not objRefresh.IsRefresh Then
                If hidPropRpt.Value = "0" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select Proprietary Report');", True)
                Else
                    NewReportId = objUpIns.CreatACopyReport(ReportId, "Prop", Session("UserId").ToString())
                    GetPCases()
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Report#" + NewReportId.ToString() + " created successfully.\nReport#" + ReportId.ToString() + " variables transferred to Report#" + NewReportId.ToString() + " successfully.');", True)
                End If
            End If
            lnkReports.Text = "Select Proprietary Report"
            hidPropRpt.Value = "0"
        Catch ex As Exception
            lblError.Text = "Error:btnCopy_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnCancle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancle.Click
        Try
            divCreate.Visible = False
            EnableDisable("")
            btnAddNew.Visible = False
        Catch ex As Exception
            lblError.Text = "Error:btnCopy_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub EnableDisable(ByVal Flag As String)
        Try

            Select Case Flag
                Case "CC"
                Case "ED"
                    btnCopy.Enabled = False
                    btnCreate.Enabled = False
                    btnCreateSpecialReport.Enabled = False
                    btnDelete.Enabled = False
                    btnEdit.Enabled = True
                Case "DE"
                Case "CR"
                    btnCopy.Enabled = False
                    btnEdit.Enabled = False
                    btnDelete.Enabled = False
                    btnCreate.Enabled = True
                    btnCreateSpecialReport.Enabled = True
                Case "CRU"
                    btnCopy.Enabled = False
                    btnEdit.Enabled = False
                    btnDelete.Enabled = False
                    btnCreate.Enabled = False
                    btnCreateSpecialReport.Enabled = True
                Case "CRM"
                    btnCopy.Enabled = False
                    btnEdit.Enabled = False
                    btnDelete.Enabled = False
                    btnCreate.Enabled = True
                    btnCreateSpecialReport.Enabled = False
                Case Else
                    btnCopy.Enabled = True
                    btnEdit.Enabled = True
                    btnDelete.Enabled = True
                    btnCreate.Enabled = True
                    btnCreateSpecialReport.Enabled = True
            End Select


        Catch ex As Exception
            lblError.Text = "Error:EnableDisable:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Try
            divCreate.Visible = False
            divCreateSpecial.Visible = False
            EnableDisable("")
            GetPCases()
            btnAddNew.Visible = False
            lnkReports.Text = "Select Proprietary Report"
            hidPropRpt.Value = "0"
        Catch ex As Exception
            lblError.Text = "Error:btnCopy_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnCreateSpecialReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateSpecialReport.Click
        Try
            divCreateSpecial.Visible = True
            txtSpecialCols.Text = String.Empty
            txtSpecialFilters.Text = String.Empty
            txtSpecialReportName.Text = String.Empty
            btnSpecialSubmit.Visible = True
            btnSpecialSubmit.Enabled = True
            btnSpecialSubmit2.Visible = False
            txtSpecialCols.Enabled = True
            txtSpecialFilters.Enabled = True
            EnableDisable("CRU")
            ddlSpecialType.Enabled = True
            ddlRegionSet.Enabled = True
            ddlUnits.Enabled = True
            BindRegionSet()
            BindUnits()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BindRegionSet()
        Try
            Dim objGetData As New Selectdata()
            Dim DS As New DataSet()
            DS = objGetData.GetUserRegionSets("-1")
            With ddlRegionSet
                .DataSource = DS
                .DataTextField = "REGIONSETNAME"
                .DataValueField = "REGIONSETID"
                .DataBind()
            End With
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BindUnits()
        Try
            Dim objGetData As New Selectdata()
            Dim DS As New DataSet()
            DS = objGetData.GetUnits("-1")
            With ddlUnits
                .DataSource = DS
                .DataTextField = "UNITDES"
                .DataValueField = "UNITID"
                .DataBind()
            End With
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlSpecialType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSpecialType.SelectedIndexChanged
        Try
            If ddlSpecialType.SelectedValue = "REGION" Then
                BindRegionSet()
                rowRegions.Visible = False
            ElseIf ddlSpecialType.SelectedValue = "CNTRY" Then
                rowRegions.Visible = True
                BindRegions()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSpecialSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSpecialSubmit.Click

        'Dim FilterType As String = String.Empty
        'Dim FilterValue As String = String.Empty
        Dim objUpIns As New UpdateInsert()
        Dim objGetData As New Selectdata()
        Dim ReportId As String = String.Empty
        Dim RowCnt As String = String.Empty
        Dim ReportFact As String = String.Empty
        Dim dsRegions As New DataSet
        Dim RowDes As String = String.Empty
        Dim dsRowSelector As New DataSet
        Dim dsRows As New DataSet
        Dim RowVal1 As String = String.Empty
        Dim RowVal2 As String = String.Empty
        Dim i As Integer
        Dim RegionId As String = String.Empty
        Try


            If ddlSpecialType.SelectedValue = "REGION" Then
                RowCnt = objGetData.GetReportRegionCount(ddlRegionSet.SelectedValue.ToString())
                dsRegions = objGetData.GetReportRegionsByRegionSet(ddlRegionSet.SelectedValue.ToString())
                dsRowSelector = objGetData.GetRowsSelectorByCode("REGION")
                RegionId = "null"
            ElseIf ddlSpecialType.SelectedValue = "CNTRY" Then

                If ddlRegions.SelectedValue.ToString() = "0" Then
                    'RowCnt = objGetData.GetReportCountryCount(ddlRegions.SelectedValue.ToString())
                    dsRegions = objGetData.GetReportCountriesByRegion(ddlRegionSet.SelectedValue.ToString(), "-1")
                    RowCnt = dsRegions.Tables(0).Rows.Count
                Else
                    dsRegions = objGetData.GetReportCountriesByRegion(ddlRegionSet.SelectedValue.ToString(), ddlRegions.SelectedValue.ToString())
                    RowCnt = dsRegions.Tables(0).Rows.Count
                End If
                RegionId = ddlRegions.SelectedValue.ToString()
                dsRowSelector = objGetData.GetRowsSelectorByCode("CNTRY")
            End If
            hidReportType.Value = "UNIFORM"

            ReportId = 0
            If Not objRefresh.IsRefresh Then
                ReportId = objUpIns.AddReportDetails(txtSpecialReportName.Text.Trim(), Session("UserId").ToString(), "UNIFORM", ReportFact, txtSpecialFilters.Text.Trim(), RowCnt, txtSpecialCols.Text.Trim(), ddlSpecialType.SelectedValue, ddlRegionSet.SelectedValue, RegionId)
                dsRows = objGetData.GetUsersReportRows(ReportId)
                For i = 0 To RowCnt - 1
                    objUpIns.UpdateRowDetails(dsRowSelector.Tables(0).Rows(0)("ROWDES").ToString(), dsRegions.Tables(0).Rows(i)("NAME").ToString().Replace("'", "''"), dsRows.Tables(0).Rows(i)("USERREPORTROWID").ToString, dsRowSelector.Tables(0).Rows(0)("ROWDES").ToString(), "0", RowVal1, RowVal2, dsRowSelector.Tables(0).Rows(0)("ROWTYPEID").ToString(), dsRegions.Tables(0).Rows(i)("ID").ToString(), ddlUnits.SelectedValue.ToString())
                Next
            End If

            SetSpecialReportFrameWork(RowCnt, txtSpecialCols.Text.Trim(), txtSpecialFilters.Text, ReportId)
            btnSpecialSubmit.Enabled = False
            btnAddNew.Visible = True
        Catch ex As Exception
            lblError.Text = "Error:btnSubmitt_Click " + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSpecialCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSpecialCancel.Click
        Try
            divCreateSpecial.Visible = False
            EnableDisable("")
            btnAddNew.Visible = False
            lnkReports.Text = "Select Proprietary Report"
            hidPropRpt.Value = "0"
        Catch ex As Exception
            lblError.Text = "Error:btnCopy_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlRegionSet_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegionSet.SelectedIndexChanged
        Try
            BindRegions()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BindRegions()
        Try
            Dim objGetData As New Selectdata()
            Dim DS As New DataSet()
            DS = objGetData.GetUserRegions(ddlRegionSet.SelectedValue.ToString())
            ddlRegions.Items.Clear()
            Dim lst As New ListItem
            lst.Text = "All"
            lst.Value = 0
            ddlRegions.Items.Add(lst)
            ddlRegions.AppendDataBoundItems = True
            With ddlRegions
                .DataSource = DS
                .DataTextField = "REGIONNAME"
                .DataValueField = "REGIONID"
                .DataBind()
            End With
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSpecialSubmit2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSpecialSubmit2.Click
        Dim ReportName As String = String.Empty
        Dim FilterType As String = String.Empty
        Dim FilterValue As String = String.Empty
        Dim ReportId As String = String.Empty
        Dim objUpIns As New UpdateInsert()
        Dim RowValue As String = String.Empty
        Dim Curr As String = String.Empty
        Dim ReportType As String = String.Empty
        Dim ReportTFact As String = String.Empty

        Dim RowCnt As String = String.Empty
        Dim ReportFact As String = String.Empty
        Dim dsRegions As New DataSet
        Dim objGetData As New Selectdata()
        Dim dsRowSelector As New DataSet
        Dim dsRows As New DataSet
        Dim RegionId As String = String.Empty
        Dim RowVal1 As String = String.Empty
        Dim RowVal2 As String = String.Empty
        Dim dsReport As DataSet
        ' Dim objGetData As New M1GetData.Selectdata()
        Try
            If ViewState("Edit") = "Y" Then
                ReportId = hidPropRpt.Value 'ddlPCases.SelectedValue
                ReportName = txtSpecialReportName.Text.Trim()
                dsReport = objGetData.GetUserCustomReportsByRptId(ReportId)

                objUpIns.EditReport(dsReport.Tables(0).Rows(0)("RPTTYPE").ToString(), ReportId, ReportName, FilterType, FilterValue, ReportTFact, ddlUnits.SelectedValue, Curr)

                If ReportType = "MIXED" Then

                Else
                    'ReportId = hidPropRpt.Value
                    'ReportName = txtSpecialReportName.Text.Trim()

                    'objUpIns.EditReport(ReportType, ReportId, ReportName, FilterType, FilterValue, ReportTFact, ddlUnits.SelectedValue, Curr)
                    If ddlSpecialType.SelectedValue = "REGION" Then
                        RowCnt = objGetData.GetReportRegionCount(ddlRegionSet.SelectedValue.ToString())
                        dsRegions = objGetData.GetReportRegionsByRegionSet(ddlRegionSet.SelectedValue.ToString())
                        dsRowSelector = objGetData.GetRowsSelectorByCode("REGION")
                        RegionId = "null"
                    ElseIf ddlSpecialType.SelectedValue = "CNTRY" Then
                        If ddlRegions.SelectedValue.ToString() = "0" Then
                            dsRegions = objGetData.GetReportCountriesByRegion(ddlRegionSet.SelectedValue.ToString(), "-1")
                            RowCnt = dsRegions.Tables(0).Rows.Count
                        Else
                            dsRegions = objGetData.GetReportCountriesByRegion(ddlRegionSet.SelectedValue.ToString(), ddlRegions.SelectedValue.ToString())
                            RowCnt = dsRegions.Tables(0).Rows.Count
                        End If
                        RegionId = ddlRegions.SelectedValue.ToString()
                        dsRowSelector = objGetData.GetRowsSelectorByCode("CNTRY")
                    End If
                    hidReportType.Value = "UNIFORM"
                    If Not objRefresh.IsRefresh Then
                        objUpIns.AddReportRowsDetails(ReportId, RowCnt, "UNIFORM", ddlSpecialType.SelectedValue, ddlRegionSet.SelectedValue, RegionId)
                        dsRows = objGetData.GetUsersReportRows(ReportId)
                        For i = 0 To RowCnt - 1
                            objUpIns.UpdateRowDetails(dsRowSelector.Tables(0).Rows(0)("ROWDES").ToString(), dsRegions.Tables(0).Rows(i)("NAME").ToString().Replace("'", "''"), dsRows.Tables(0).Rows(i)("USERREPORTROWID").ToString, dsRowSelector.Tables(0).Rows(0)("ROWDES").ToString(), "0", RowVal1, RowVal2, dsRowSelector.Tables(0).Rows(0)("ROWTYPEID").ToString(), dsRegions.Tables(0).Rows(i)("ID").ToString(), ddlUnits.SelectedValue.ToString())
                        Next
                    End If

                End If

            End If

            btnSpecialSubmit2.Enabled = False
            SetSpecialReportFrameWork("", txtSpecialCols.Text.ToString().Trim(), txtSpecialFilters.Text.ToString().Trim(), ReportId)
            btnAddNew.Visible = True
            'EnableDisable("")
        Catch ex As Exception
            lblError.Text = "Error:btnSubmitt2_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnCopyBcase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopyBcase.Click
        Dim ReportId As String = String.Empty
        Dim NewReportId As String = String.Empty
        Dim objUpIns As New UpdateInsert()
        Try
            ReportId = hidBaseRpt.Value
            If Not objRefresh.IsRefresh Then
                If hidBaseRpt.Value = "0" Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinP", "alert('Please select Base Report');", True)
                Else
                    NewReportId = objUpIns.CreatACopyReport(ReportId, "Base", Session("UserId").ToString())
                    GetPCases()
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Report#" + NewReportId.ToString() + " created successfully.\nReport#" + ReportId.ToString() + " variables transferred to Report#" + NewReportId.ToString() + " successfully.');", True)
                End If
            End If
            lnkBReports.Text = "Select Base Report"
            hidBaseRpt.Value = "0"
        Catch ex As Exception
            lblError.Text = "Error:btnCopy_Click:" + ex.Message.ToString()
        End Try
    End Sub

    
    
End Class
