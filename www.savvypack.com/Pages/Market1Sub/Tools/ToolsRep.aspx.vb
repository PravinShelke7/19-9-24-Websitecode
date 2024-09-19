Imports System.Data
Imports System.Data.OleDb
Imports System
Imports M1GetData
Imports M1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Market1_Tools_ToolsRep
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
            If Not IsPostBack Then
                GetFilterTypeDetails()
                GetPCases()
                GetCurr()
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
            With ddlPCases
                .DataSource = ds
                .DataTextField = "REPORTDES"
                .DataValueField = "USERREPORTID"
                .DataBind()
            End With
        Catch ex As Exception
            lblError.Text = "Error:GetReportDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetFilterTypeDetails()
        Dim objGetData As New M1GetData.Selectdata()
        Dim ds As New DataSet()
        Dim ReportType As String
        Try
            ReportType = ddlReportType.SelectedValue
            ds = objGetData.GetReportFilterType(ReportType)
            With ddlFilterType
                .DataSource = ds
                .DataTextField = "FILTERNAME"
                .DataValueField = "FILTERCODE"
                .DataBind()
                .Font.Size = 8
            End With

            ViewState("FilterType") = ds
            GetFilter()
        Catch ex As Exception
            lblError.Text = "Error:GetFilterTypeDetails:" + ex.Message.ToString() + ""
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

        Try
            ColCnt = txtCol.Text.Trim()
            RowCnt = txtRw.Text.Trim()
            ReportName = txtRptName.Text.Trim()
            FilterType = ddlFilterType.SelectedValue


            If ddlReportType.SelectedValue = "2D" Then
                ReportFact = ddlFact.SelectedItem.Text
            Else
                ReportFact = ""

            End If
            FilterValue = ddlFilter.SelectedValue
            If Not objRefresh.IsRefresh Then
                '                ReportId = objUpIns.AddReportDetails(ReportName, ColCnt, RowCnt, Session("UserId").ToString(), FilterValue, FilterType, ddlReportType.SelectedValue, ReportFact)
                SetReportFrameWork(RowCnt, ColCnt, ReportId, ddlReportType.SelectedValue)
                btnSubmitt.Enabled = False
                btnAddNew.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnSubmitt_Click " + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub SetReportFrameWork(ByVal RowCnt As String, ByVal ColCnt As String, ByVal RptID As String, ByVal RptType As String)
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
        Dim Flag As Boolean
        Dim Des As String = String.Empty


        Try
            'Checking for Edit
            If ViewState("Edit") = "Y" Then
                Flag = True
                dsRows = ViewState("dsRows")
                dsCol = ViewState("dsCol")
            Else
                Flag = False
            End If

            HeaderTr = New TableRow
            For j = 0 To ColCnt
                If j = 0 Then
                    HeaderTd = New TableCell()
                    HeaderTdSetting(HeaderTd, "100px", "&nbsp;", "1")
                Else
                    HeaderTd = New TableCell()
                    hyd = New HiddenField
                    Link = New HyperLink
                    If Flag Then
                        Des = String.Empty

                        If dsCol.Tables(0).Rows(j - 1).Item("COLUMNVALUE").ToString() = "CAGR" Then
                            Des = dsCol.Tables(0).Rows(j - 1).Item("COLUMNVALUE").ToString() + "(" + dsCol.Tables(0).Rows(j - 1).Item("INPUTVALUE1").ToString() + "/" + dsCol.Tables(0).Rows(j - 1).Item("INPUTVALUE2").ToString() + ")"
                        Else
                            Des = dsCol.Tables(0).Rows(j - 1).Item("COLUMNVALUE").ToString()
                        End If
                        Link.Text = Des
                    Else
                        Link.Text = "Column_" + j.ToString()
                    End If

                    Link.ID = "Column_" + j.ToString()
                    hyd.ID = "Column_ID_" + j.ToString()
                    If Flag Then
                        hyd.Value = dsCol.Tables(0).Rows(j - 1).Item("USERREPORTCOLUMNID").ToString()
                    End If
                    If hyd.Value = "" Then
                        hyd.Value = "0"
                    End If
                    Link.CssClass = "LinkM"
                    GetColLink(j, Link, RptID, hyd.ID, hyd.Value)
                    HeaderTd.Controls.Add(Link)
                    HeaderTd.Controls.Add(hyd)
                    HeaderTdWLinkSetting(HeaderTd, "150px", "", "1")
                End If
                HeaderTr.Controls.Add(HeaderTd)

            Next
            HeaderTr.Height = 30
            tblCAGR.Controls.Add(HeaderTr)

            For i = 1 To RowCnt
                Tr = New TableRow
                For j = 0 To ColCnt

                    If j = 0 Then
                        Td = New TableCell()
                        Link = New HyperLink
                        If Flag Then
                            Des = String.Empty
                            If dsRows.Tables(0).Rows(i - 1).Item("TITLE").ToString() <> "NA" Then
                                Des = "(" + dsRows.Tables(0).Rows(i - 1).Item("TITLE").ToString() + ")"
                            End If
                            If RptType = "1D" Then
                                Link.Text = dsRows.Tables(0).Rows(i - 1).Item("ROWDECRIPTION").ToString() + Des
                            Else
                                Link.Text = dsRows.Tables(0).Rows(i - 1).Item("ROWDECRIPTION").ToString()
                            End If

                        Else
                            Link.Text = "Row_" + i.ToString()
                        End If

                        Link.ID = "Row_" + i.ToString()
                        Link.CssClass = "LinkM"
                        hyd = New HiddenField
                        hyd.ID = "Row_ID_" + i.ToString()
                        If Flag Then

                            hyd.Value = dsRows.Tables(0).Rows(i - 1).Item("USERREPORTROWID").ToString()
                        End If
                        If hyd.Value = "" Then
                            hyd.Value = "0"
                        End If
                        GetRowLink(i, Link, RptID, hyd.ID, hyd.Value)


                        Td.Controls.Add(Link)
                        Td.Controls.Add(hyd)
                        HeaderTdWLinkSetting(Td, "100px", "", "1")
                    Else
                        Td = New TableCell()
                        Td.Text = "&nbsp;"
                        HeaderTdSetting(Td, "150px", "", "1")
                        Td.CssClass = "AlterNateColor2"
                    End If
                    Tr.Controls.Add(Td)
                Next
                Tr.Height = 30
                tblCAGR.Controls.Add(Tr)
            Next


        Catch ex As Exception
            lblError.Text = "Error:SetReportFrameWork " + ex.Message.ToString()
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
        Dim RptType As String = String.Empty
        Dim RowVal2D As String = String.Empty
        Dim RegionSetId As String = String.Empty
        Dim Curr As String = String.Empty
        RptType = ddlReportType.SelectedValue

        Try
            If RptType = "1D" Then
                RegionSetId = 0
                RowVal2D = ""
            ElseIf RptType = "2D" Then
                RegionSetId = ddlFilter.SelectedValue
                RowVal2D = ddlFact.SelectedValue
                Curr = ddlCurr.SelectedValue
            End If

            Path = "../PopUp/RowSelector.aspx?RptId=" + RptId + "&Seq=" + Seq.ToString() + "&Id=" + Link.ClientID + "&hidId=" + hidId + "&RptType=" + RptType + "&RID=" + RegionSetId + "&ROWVAL2D=" + RowVal2D + "&CURR=" + Curr + ""
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

        Try
            If ViewState("Edit") = "Y" Then
                ReportType = ddlReportType.SelectedValue
                ReportId = ddlPCases.SelectedValue
                ReportName = txtRptName.Text.Trim()
                FilterType = ddlFilterType.SelectedValue
                FilterValue = ddlFilter.SelectedValue

                If ReportType = "2D" Then
                    RowValue = ddlFact.SelectedValue
                    Curr = ddlCurr.SelectedValue
                    ReportTFact = ddlFact.SelectedItem.Text
                End If


                objUpIns.EditReport(ReportType, ReportId, ReportName, FilterType, FilterValue, ReportTFact, RowValue, Curr)
            End If
            txtRptName.Text = ""
            txtRw.Text = ""
            txtCol.Text = ""
            ddlFilterType.SelectedIndex = 0
            btnSubmitt.Enabled = True
            btnAddNew.Visible = False
            GetPCases()
            divCreate.Visible = False
            EnableDisable("")
        Catch ex As Exception
            lblError.Text = "Error:btnAddNew_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try

            divCreate.Visible = False
            GetReportDetails(ddlPCases)
            btnAddNew.Visible = False
            divCreate.Visible = True
            EnableDisable("ED")
            btnSubmitt2.Visible = True
            btnSubmitt.Visible = False



        Catch ex As Exception
            lblError.Text = "Error:btnEdit_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetReportDetails(ByVal ddl As DropDownList)
        Dim RptID As String = String.Empty
        Dim dsRpt As New DataSet
        Dim dsRows As New DataSet
        Dim dsCol As New DataSet
        Dim dsFilter As New DataSet
        Dim objGetData As New M1GetData.Selectdata()
        Dim arr() As String
        Dim dr As DataRow()

        Try
            RptID = ddl.SelectedValue.ToString()
            dsRows = objGetData.GetUserReportsRows(RptID)
            dsCol = objGetData.GetUserReportsCols(RptID)
            dsFilter = objGetData.GetUserReportsFilter(RptID)
            dsRpt = Session("UserReport")
            arr = ddl.SelectedItem.Text.Split(":")

            ViewState("dsRows") = dsRows
            ViewState("dsCol") = dsCol
            ViewState("Edit") = "Y"
            dr = dsRpt.Tables(0).Select("USERREPORTID=" + RptID + "")


            'Report Details

            ddlReportType.SelectedValue = dr(0).Item("RPTTYPE")
            GetFilterTypeDetails()

            txtRptName.Text = arr(1).Trim()
            txtCol.Text = dsCol.Tables(0).Rows.Count.ToString()
            txtRw.Text = dsRows.Tables(0).Rows.Count.ToString()
            ddlFilterType.SelectedValue = dsFilter.Tables(0).Rows(0).Item("FILTERTYPE").ToString()
            ddlFilter.SelectedValue = dsFilter.Tables(0).Rows(0).Item("VALUE").ToString()
            btnSubmitt.Enabled = False
            'ddlFilterType.Enabled = False
            'ddlFilter.Enabled = False
            txtCol.Enabled = False
            txtRw.Enabled = False
            ddlCurr.SelectedValue = dsRows.Tables(0).Rows(0).Item("Curr")




            'GetFilter()

            'ReportFrameWork
            'SetReportFrameWork(txtRw.Text, txtCol.Text, RptID, ddlReportType.SelectedValue)


        Catch ex As Exception
            lblError.Text = "Error:GetReportDetails:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetCurr()
        Dim objGetDate As New Selectdata()
        Dim ds As New DataSet()
        Dim lst As New ListItem
        Try
            lst.Text = "--Select Currency--"
            lst.Value = "0"
            ddlCurr.Items.Add(lst)
            ddlCurr.AppendDataBoundItems = True

            ds = objGetDate.GetCurrency()
            With ddlCurr
                .DataSource = ds
                .DataTextField = "CURRENCYDE"
                .DataValueField = "CURRENCYID"
                .DataBind()
            End With


        Catch ex As Exception
            lblError.Text = "Error:GetRowSelector" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Try
            divCreate.Visible = True
            txtCol.Text = String.Empty
            txtRw.Text = String.Empty
            txtRptName.Text = String.Empty
            ddlFilterType.SelectedIndex = 0
            ddlFilterType.Enabled = True
            btnSubmitt.Visible = True
            btnSubmitt.Enabled = True
            btnSubmitt2.Visible = False
            txtCol.Enabled = True
            txtRw.Enabled = True
            EnableDisable("CR")
        Catch ex As Exception
            lblError.Text = "Error:btnCreate_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim ReportId As String = String.Empty
        Dim objUpIns As New UpdateInsert()
        Try
            If Not objRefresh.IsRefresh Then
                ReportId = ddlPCases.SelectedValue.ToString()
                objUpIns.DeleteReport(ReportId)
                GetPCases()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinDelete", "alert('Report#" + ReportId.ToString() + " deleted successfully');", True)
            End If
        Catch ex As Exception
            lblError.Text = "Error:btnDelete_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Dim ReportId As String = String.Empty
        Dim NewReportId As String = String.Empty
        Dim objUpIns As New UpdateInsert()
        Try
            ReportId = ddlPCases.SelectedValue.ToString()
            If Not objRefresh.IsRefresh Then
                'NewReportId = objUpIns.CreatACopyReport(ReportId)
                GetPCases()
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "alert('Report#" + NewReportId.ToString() + " created successfully.\nReport#" + ReportId.ToString() + " variables transferred to Report#" + NewReportId.ToString() + " successfully.');", True)
            End If

        Catch ex As Exception
            lblError.Text = "Error:btnCopy_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnCancle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancle.Click
        Try
            divCreate.Visible = False
            EnableDisable("")
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
                    btnDelete.Enabled = False
                    btnEdit.Enabled = True
                    ddlReportType.Enabled = False
                    If ddlReportType.SelectedValue = "2D" Then
                        ddlFilter.Enabled = False
                        ddlFilterType.Enabled = False
                    End If
                Case "DE"
                Case "CR"
                    btnCopy.Enabled = False
                    btnEdit.Enabled = False
                    btnDelete.Enabled = False
                    btnCreate.Enabled = True
                    ddlReportType.Enabled = True
                    ddlFilter.Enabled = True
                    ddlFilterType.Enabled = True

                Case Else
                    btnCopy.Enabled = True
                    btnEdit.Enabled = True
                    btnDelete.Enabled = True
                    btnCreate.Enabled = True
            End Select


        Catch ex As Exception
            lblError.Text = "Error:EnableDisable:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlFilterType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFilterType.SelectedIndexChanged
        GetFilter()
        'If ViewState("Edit") = "Y" Then
        '    SetReportFrameWork(txtRw.Text, txtCol.Text, ddlPCases.SelectedValue, ddlReportType.SelectedValue)
        'End If
    End Sub

    Protected Sub GetFilter()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet
        Dim sql As String = String.Empty
        Dim dr As DataRow()
        Dim dsType As New DataSet
        Try
            dsType = ViewState("FilterType")
            dr = dsType.Tables(0).Select("FilterCode='" + ddlFilterType.SelectedValue + "'")

            lblFilterName.Text = ddlFilterType.SelectedItem.Text
            sql = dr(0).Item("SQL")

            ds = objGetData.GetReportFilter(Session("UserId").ToString(), sql)

            With ddlFilter
                .DataSource = ds
                .DataTextField = "VALUE"
                .DataValueField = "ID"
                .DataBind()
            End With

            If ddlReportType.SelectedValue = "2D" Then
                ds = objGetData.GetReportFactType()
                With ddlFact
                    .DataSource = ds
                    .DataTextField = "NAME"
                    .DataValueField = "VALUE"
                    .DataBind()
                End With
                rw_Fact.Visible = True

                If ddlFact.SelectedItem.Text <> "GDP" Then
                    rw_Curr.Visible = False
                Else
                    rw_Curr.Visible = True
                End If
            Else
                rw_Fact.Visible = False
                rw_Curr.Visible = False
            End If



        Catch ex As Exception
            lblError.Text = "Error:ddlFilterType_SelectedIndexChanged:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlReportType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlReportType.SelectedIndexChanged
        Try
            GetFilterTypeDetails()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlCurr_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCurr.SelectedIndexChanged
        Try
            If ddlFact.SelectedValue = "GDP" Then
                rw_Curr.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
