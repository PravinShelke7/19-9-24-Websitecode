Imports System.Data
Imports System.Data.OleDb
Imports System
Imports M1GetData
Imports M1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Market1_CAGR_CagrReport
    Inherits System.Web.UI.Page
#Region "Get Set Variables"
    Dim _lErrorLble As Label
    Dim _iUserId As Integer
    Dim _strUserRole As String
    Dim _btnLogOff As ImageButton
    Dim _btnUpdate As ImageButton
    Dim _divMainHeading As HtmlGenericControl
    Dim _ctlContentPlaceHolder As ContentPlaceHolder


    Public Property ErrorLable() As Label
        Get

            Return _lErrorLble
        End Get
        Set(ByVal Value As Label)
            _lErrorLble = Value
        End Set
    End Property

    Public Property UserId() As Integer
        Get
            Return _iUserId
        End Get
        Set(ByVal Value As Integer)
            _iUserId = Value
        End Set
    End Property

    Public Property UserRole() As String
        Get
            Return _strUserRole
        End Get
        Set(ByVal Value As String)
            _strUserRole = Value
        End Set
    End Property

    Public Property LogOffbtn() As ImageButton
        Get
            Return _btnLogOff
        End Get
        Set(ByVal value As ImageButton)
            _btnLogOff = value
        End Set
    End Property

    Public Property Updatebtn() As ImageButton
        Get
            Return _btnUpdate
        End Get
        Set(ByVal value As ImageButton)
            _btnUpdate = value
        End Set
    End Property

    Public Property MainHeading() As HtmlGenericControl
        Get
            Return _divMainHeading
        End Get
        Set(ByVal value As HtmlGenericControl)
            _divMainHeading = value
        End Set
    End Property

    Public Property ctlContentPlaceHolder() As ContentPlaceHolder
        Get
            Return _ctlContentPlaceHolder
        End Get
        Set(ByVal value As ContentPlaceHolder)
            _ctlContentPlaceHolder = value
        End Set
    End Property



#End Region

#Region "MastePage Content Variables"

    Protected Sub GetMasterPageControls()
        GetErrorLable()
        GetLogOffbtn()
        GetUpdatebtn()
        GetMainHeadingdiv()
        'GetContentPlaceHolder()
    End Sub

    Protected Sub GetErrorLable()
        ErrorLable = Page.Master.FindControl("lblError")
        ErrorLable.Text = String.Empty
    End Sub

    Protected Sub GetLogOffbtn()
        LogOffbtn = Page.Master.FindControl("imgLogoff")
        LogOffbtn.Visible = False
    End Sub

    Protected Sub GetUpdatebtn()
        Updatebtn = Page.Master.FindControl("imgUpdate")
        Updatebtn.Visible = True
        AddHandler Updatebtn.Click, AddressOf Update_Click
    End Sub

    Protected Sub GetMainHeadingdiv()
        MainHeading = Page.Master.FindControl("divMainHeading")
        MainHeading.Attributes.Add("onmouseover", "Tip('Market1 Report')")
        MainHeading.Attributes.Add("onmouseout", "UnTip()")
        '  MainHeading.InnerHtml = "CAGR Reports"
    End Sub

    Protected Sub GetContentPlaceHolder()
        ctlContentPlaceHolder = Page.Master.FindControl("Market1ContentPlaceHolder")
    End Sub

#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            GetMasterPageControls()
            If Not IsPostBack Then
                GetReportDetails()
                GetPageDetails()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub GetReportDetails()
        Dim objGetData As New M1GetData.Selectdata()
        Dim ds As New DataSet
        Try
            If Request.QueryString("Type").ToString() = "Base" Then
                ds = objGetData.GetBaseReportsByRptId(Session("M1RptId").ToString())
                lblheading.text = "Market1 - Base Report Details"
            Else
                ds = objGetData.GetUserCustomReportsByRptId(Session("M1RptId").ToString())
                lblheading.text = "Market1 - Proprietary Report Details"
            End If
            If ds.Tables(0).Rows.Count > 0 Then
                hidReportType.Value = ds.Tables(0).Rows(0).Item("RPTTYPE").ToString()
                lblReportID.Text = ds.Tables(0).Rows(0).Item("REPORTID").ToString()
                lblReportType.Text = ds.Tables(0).Rows(0).Item("RPTTYPE").ToString()
                lblReportDe2.Text = ds.Tables(0).Rows(0).Item("REPORTNAME").ToString()
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:GetReportDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetPageDetails()
        Dim objGetData As New Selectdata()
        Dim dsRpt As New DataSet()
        Dim rptType As String = String.Empty
        Try
            rptType = Request.QueryString("Type").ToString()
            If rptType = "Base" Then
                dsRpt = objGetData.GetBaseReportsByRptId(Session("M1RptId"))
            Else
                dsRpt = objGetData.GetUserCustomReportsByRptId(Session("M1RptId"))
            End If

            If dsRpt.Tables(0).Rows(0)("RPTTYPE").ToString() = "UNIFORM" Then
                SetUniformReport(dsRpt.Tables(0).Rows(0)("RPTTYPEDES").ToString(), rptType)
            ElseIf dsRpt.Tables(0).Rows(0)("RPTTYPE").ToString() = "MIXED" Then
                SetMixedReport(dsRpt.Tables(0).Rows(0)("RPTTYPEDES").ToString(), rptType)
            End If
        Catch ex As Exception
            ErrorLable.Text = "Error:GetPageDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub

    Private Sub SetUniformReport(ByVal reportDes As String, ByVal rptType As String)
        Dim objGetData As New Selectdata()
        Dim dsRptRws As New DataSet()
        Dim dsRptCols As New DataSet()
        Dim dsRptFilter As New DataSet()
        Dim dsRptAct As New DataSet()
        Dim dsRptFilterValue As New DataSet()
        Dim ColCnt As New Integer
        Dim RowCnt As New Integer
        Dim FiltCnt As New Integer
        Dim Dr() As DataRow
        Dim i As New Integer
        Dim j As New Integer
        Dim l As New Integer
        Dim HeaderTr As New TableRow()
        Dim HeaderTd As New TableCell()
        Dim Tr As New TableRow()
        Dim Td As New TableCell()
        Dim lbl As New Label
        Dim Fact As New Decimal
        Dim Link As New HyperLink
        Dim hyd As New HiddenField

        dsRptRws = objGetData.GetUsersDynamicReportRows(Session("M1RptId"))
        dsRptCols = objGetData.GetUsersDynamicReportCols(Session("M1RptId"))
        dsRptFilter = objGetData.GetUsersReportFilters(Session("M1RptId"))
        dsRptAct = objGetData.GetUsersDynamicUniformReportData(Session("M1RptId"), reportDes)

        If dsRptRws.Tables.Count > 0 Then
            RowCnt = dsRptRws.Tables(0).Rows.Count - 1
        End If
        If dsRptCols.Tables.Count > 0 Then
            ColCnt = dsRptCols.Tables(0).Rows.Count
        End If
        If dsRptFilter.Tables.Count > 0 Then
            FiltCnt = dsRptFilter.Tables(0).Rows.Count
        End If

        tblCAGR.Rows.Clear()
        'filter row
        HeaderTr = New TableRow
        For j = 0 To ColCnt
            HeaderTd = New TableCell()
            If j = 0 Then
                For l = 0 To FiltCnt - 1
                    Link = New HyperLink
                    hyd = New HiddenField
                    lbl = New Label
                    Link.Text = "Filter " + (l + 1).ToString() + ":" + dsRptFilter.Tables(0).Rows(l).Item("FILTERVALUE") + "<br/> "
                    lbl.Text = "Filter " + (l + 1).ToString() + ":" + dsRptFilter.Tables(0).Rows(l).Item("FILTERVALUE") + "<br/> "
                    hyd.Value = dsRptFilter.Tables(0).Rows(l).Item("USERREPORTFILTERID").ToString()
                    Link.ID = "Column_Fil_" + l.ToString()
                    hyd.ID = "Column_ID_Fil_" + l.ToString()
                    Link.CssClass = "LinkM"

                    GetFilterLink(dsRptFilter.Tables(0).Rows(l).Item("FILTERSEQUENCE").ToString(), Link, dsRptFilter.Tables(0).Rows(l).Item("USERREPORTID").ToString(), hyd.ClientID, hyd.Value)
                    If rptType = "Base" Then
                        HeaderTd.Controls.Add(lbl)
                    Else
                        HeaderTd.Controls.Add(Link)
                    End If
                    HeaderTd.Controls.Add(hyd)
                Next
                HeaderTdSetting(HeaderTd, "150px", "", "1")
            Else
                HeaderTdSetting(HeaderTd, "100px", "", "1")
            End If
            HeaderTd.Font.Size = 8.5
            HeaderTd.Font.Name = "Verdana"
            HeaderTd.Font.Bold = False
            HeaderTr.Controls.Add(HeaderTd)
        Next
        tblCAGR.Controls.Add(HeaderTr)


        'Hedaer Row
        HeaderTr = New TableRow
        For j = 0 To ColCnt
            HeaderTd = New TableCell()
            If j = 0 Then
                lbl = New Label
                If reportDes = "REGION" Then
                    lbl.Text = "GEOGRAPHIC REGIONS"
                ElseIf reportDes = "CNTRY" Then
                    lbl.Text = "COUNTRIES"
                End If
                HeaderTd.Controls.Add(lbl)
                HeaderTdSetting(HeaderTd, "150px", "", "1")
            Else
                hyd = New HiddenField
                Link = New HyperLink
                lbl = New Label
                Dim k As New Integer
                k = j - 1
                If dsRptCols.Tables(0).Rows(k).Item("COLUMNVALUETYPE").ToString() = "Formula" Then
                    Link.Text = dsRptCols.Tables(0).Rows(k).Item("COLUMNVALUE").ToString() + "(" + dsRptCols.Tables(0).Rows(k).Item("INPUTVALUE1").ToString() + "/" + dsRptCols.Tables(0).Rows(k).Item("INPUTVALUE2").ToString() + ")"
                Else
                    Link.Text = dsRptCols.Tables(0).Rows(k).Item("COLUMNVALUE").ToString()
                End If
                lbl.Text = Link.Text
                Link.ID = "Column_" + k.ToString()
                hyd.ID = "Column_ID_" + k.ToString()
                hyd.Value = dsRptCols.Tables(0).Rows(k).Item("USERREPORTCOLUMNID").ToString()
                Link.CssClass = "LinkM"
                GetColLink(dsRptCols.Tables(0).Rows(k).Item("COLUMNSEQUENCE").ToString(), Link, dsRptCols.Tables(0).Rows(k).Item("USERREPORTID").ToString(), hyd.ClientID, hyd.Value)
                If rptType = "Base" Then
                    HeaderTd.Controls.Add(lbl)
                Else
                    HeaderTd.Controls.Add(Link)
                End If
                HeaderTd.Controls.Add(hyd)
                HeaderTdSetting(HeaderTd, "100px", "", "1")

            End If
            HeaderTd.Font.Size = 8.5
            HeaderTd.Font.Name = "Verdana"
            HeaderTd.Font.Bold = False
            HeaderTr.Controls.Add(HeaderTd)
        Next
        tblCAGR.Controls.Add(HeaderTr)


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
                        If dsRptCols.Tables(0).Rows(j - 1).Item("COLUMNVALUETYPE").ToString() = "Formula" Then
                            lbl.Text = "(%)"
                        Else
                            lbl.Text = "(" + dsRptRws.Tables(0).Rows(0).Item("UNITSHRT").ToString() + ")"
                            hidUnitShort.Value = lbl.Text
                        End If
                        Td.Style.Add("text-align", "Center")
                    Else
                        If dsRptCols.Tables(0).Rows(j - 1).Item("COLUMNVALUETYPE").ToString() = "Formula" Then
                            Dim BeginYearId As String = String.Empty
                            Dim EndYearId As String = String.Empty
                            BeginYearId = dsRptCols.Tables(0).Rows(j - 1).Item("INPUTVALUETYPE1").ToString()
                            EndYearId = dsRptCols.Tables(0).Rows(j - 1).Item("INPUTVALUETYPE2").ToString()
                        Else
                            lbl.Text = GetFactValueTotal(dsRptAct, dsRptCols.Tables(0).Rows(j - 1).Item("COLCOLUMNID").ToString(), dsRptCols.Tables(0).Rows(j - 1).Item("COLUMNVALUEID").ToString())
                            lbl.Text = FormatNumber(lbl.Text, 0)
                        End If
                        Td.Style.Add("text-align", "Right")
                        Td.Style.Add("padding-right", "5px")
                        Td.CssClass = "AlterNateColor2"
                    End If

                End If

                Td.Font.Size = 8
                Td.Font.Name = "Verdana"
                Td.Font.Bold = False
                Td.Controls.Add(lbl)
                Tr.Controls.Add(Td)
            Next
            tblCAGR.Controls.Add(Tr)
        Next
        'Inner Row
        For i = 0 To RowCnt
            Tr = New TableRow()
            For j = 0 To ColCnt
                Td = New TableCell()
                If j = 0 Then
                    HeaderTdSetting(Td, "150px", "", "1")
                    lbl = New Label
                    lbl.Text = dsRptRws.Tables(0).Rows(i).Item("ROWVALUE").ToString() '+ "(" + dsRptRws.Tables(0).Rows(i).Item("UNITSHRT").ToString() + ")"

                    Td.Style.Add("text-align", "Left")
                    Td.Font.Size = 10
                    Td.Font.Name = "Verdana"
                    Td.Font.Bold = True
                    Td.Style.Add("padding-left", "5px")
                    Td.Controls.Add(lbl)
                Else
                    Dim k As New Integer
                    k = j - 1
                    Dim ColType As String = dsRptCols.Tables(0).Rows(k).Item("COLUMNVALUETYPE").ToString()
                    Dim RowId As String = String.Empty
                    Dim ColId As String = String.Empty
                    Dim RowValue As String = String.Empty
                    Dim ColValue As String = String.Empty
                    Dim UnitId As String = String.Empty
                    Dim UnitValue As String = String.Empty
                    RowId = dsRptRws.Tables(0).Rows(i).Item("ROWCOLUMNID").ToString()
                    RowValue = dsRptRws.Tables(0).Rows(i).Item("ROWVALUEID").ToString()
                    ColId = dsRptCols.Tables(0).Rows(k).Item("COLCOLUMNID").ToString()
                    ColValue = dsRptCols.Tables(0).Rows(k).Item("COLUMNVALUEID").ToString()
                    UnitId = "UnitId"
                    UnitValue = dsRptRws.Tables(0).Rows(i).Item("UNITID").ToString()

                    If ColType = "Year" Then
                        If GetFactValue(dsRptAct, RowId, ColId, RowValue, ColValue, UnitId, UnitValue) <> String.Empty Then
                            Td.Text = FormatNumber(GetFactValue(dsRptAct, RowId, ColId, RowValue, ColValue, UnitId, UnitValue), 0)
                        End If

                    Else
                        Td.Text = "0.00"
                        Dim BeginYearId As String = String.Empty
                        Dim EndYearId As String = String.Empty
                        Dr = dsRptCols.Tables(0).Select("USERREPORTCOLUMNID=" + dsRptCols.Tables(0).Rows(k).Item("INPUTVALUETYPE1").ToString() + "")

                        BeginYearId = Dr(0).Item("COLUMNVALUEID").ToString() 'dsRptCols.Tables(0).Rows(k).Item("INPUTVALUETYPE1").ToString()
                        Dr = dsRptCols.Tables(0).Select("USERREPORTCOLUMNID=" + dsRptCols.Tables(0).Rows(k).Item("INPUTVALUETYPE2").ToString() + "")
                        EndYearId = Dr(0).Item("COLUMNVALUEID").ToString() 'dsRptCols.Tables(0).Rows(k).Item("INPUTVALUETYPE2").ToString()
                        Td.Text = FormatNumber(GetCAGR(dsRptAct, RowId, ColId, RowValue, UnitId, UnitValue, BeginYearId, EndYearId), 4)
                    End If

                    InnerTdSetting(Td, "", "Right")
                    Td.CssClass = "AlterNateColor2"
                End If
                Td.Font.Size = 8.5
                Td.Font.Name = "Verdana"
                Td.Font.Bold = False
                Tr.Controls.Add(Td)
            Next
            tblCAGR.Controls.Add(Tr)
        Next
        tblCAGR.Visible = True
    End Sub

    Private Sub SetMixedReport(ByVal reportDes As String, ByVal rptType As String)
        Dim objGetData As New Selectdata()
        Dim dsRptRws As New DataSet()
        Dim dsRptCols As New DataSet()
        Dim dsRptFilter As New DataSet()
        Dim dsRptAct As New DataSet()
        Dim ColCnt As New Integer
        Dim RowCnt As New Integer
        Dim FiltCnt As New Integer
        Dim Dr() As DataRow
        Dim i As New Integer
        Dim j As New Integer
        Dim l As New Integer
        Dim HeaderTr As New TableRow()
        Dim HeaderTd As New TableCell()
        Dim Tr As New TableRow()
        Dim Td As New TableCell()
        Dim lbl As New Label
        Dim Link As New HyperLink
        Dim hyd As New HiddenField

        dsRptRws = objGetData.GetUsersDynamicReportRows(Session("M1RptId"))
        dsRptCols = objGetData.GetUsersDynamicReportCols(Session("M1RptId"))
        dsRptFilter = objGetData.GetUsersReportFilters(Session("M1RptId"))
        dsRptAct = objGetData.GetUsersDynamicReportData(Session("M1RptId"), reportDes)


        If dsRptRws.Tables.Count > 0 Then
            RowCnt = dsRptRws.Tables(0).Rows.Count - 1
        End If

        If dsRptCols.Tables.Count > 0 Then
            ColCnt = dsRptCols.Tables(0).Rows.Count
        End If

        If dsRptFilter.Tables.Count > 0 Then
            FiltCnt = dsRptFilter.Tables(0).Rows.Count
        End If

        tblCAGR.Rows.Clear()
        'filter row
        HeaderTr = New TableRow
        For j = 0 To ColCnt
            HeaderTd = New TableCell()
            If j = 0 Then
                For l = 0 To FiltCnt - 1
                    Link = New HyperLink
                    hyd = New HiddenField
                    lbl = New Label
                    Link.Text = "Filter " + (l + 1).ToString() + ":" + dsRptFilter.Tables(0).Rows(l).Item("FILTERVALUE") + "<br/> "
                    lbl.Text = "Filter " + (l + 1).ToString() + ":" + dsRptFilter.Tables(0).Rows(l).Item("FILTERVALUE") + "<br/> "
                    hyd.Value = dsRptFilter.Tables(0).Rows(l).Item("USERREPORTFILTERID").ToString()
                    Link.ID = "Column_Fil_" + l.ToString()
                    hyd.ID = "Column_ID_Fil_" + l.ToString()
                    Link.CssClass = "LinkM"

                    GetFilterLink(dsRptFilter.Tables(0).Rows(l).Item("FILTERSEQUENCE").ToString(), Link, dsRptFilter.Tables(0).Rows(l).Item("USERREPORTID").ToString(), hyd.ClientID, hyd.Value)
                    If rptType = "Base" Then
                        HeaderTd.Controls.Add(lbl)
                    Else
                        HeaderTd.Controls.Add(Link)
                    End If
                    HeaderTd.Controls.Add(hyd)
                Next
                HeaderTdSetting(HeaderTd, "150px", "", "1")
            Else
                HeaderTdSetting(HeaderTd, "100px", "", "1")
            End If
            HeaderTd.Font.Size = 8.5
            HeaderTd.Font.Name = "Verdana"
            HeaderTd.Font.Bold = False
            HeaderTr.Controls.Add(HeaderTd)
        Next
        tblCAGR.Controls.Add(HeaderTr)

        'Hedaer Row
        HeaderTr = New TableRow
        For j = 0 To ColCnt
            HeaderTd = New TableCell()
            If j = 0 Then
                'lbl = New Label

                ' HeaderTd.Controls.Add(lbl)
                HeaderTdSetting(HeaderTd, "150px", "", "1")
            Else
                hyd = New HiddenField
                Link = New HyperLink
                lbl = New Label
                Dim k As New Integer
                k = j - 1
                If dsRptCols.Tables(0).Rows(k).Item("COLUMNVALUETYPE").ToString() = "Formula" Then
                    Link.Text = dsRptCols.Tables(0).Rows(k).Item("COLUMNVALUE").ToString() + "(" + dsRptCols.Tables(0).Rows(k).Item("INPUTVALUE1").ToString() + "/" + dsRptCols.Tables(0).Rows(k).Item("INPUTVALUE2").ToString() + ")"
                Else
                    Link.Text = dsRptCols.Tables(0).Rows(k).Item("COLUMNVALUE").ToString()
                End If
                lbl.Text = Link.Text
                Link.ID = "Column_" + k.ToString()
                hyd.ID = "Column_ID_" + k.ToString()
                hyd.Value = dsRptCols.Tables(0).Rows(k).Item("USERREPORTCOLUMNID").ToString()
                Link.CssClass = "LinkM"
                GetColLink(dsRptCols.Tables(0).Rows(k).Item("COLUMNSEQUENCE").ToString(), Link, dsRptCols.Tables(0).Rows(k).Item("USERREPORTID").ToString(), hyd.ClientID, hyd.Value)
                If rptType = "Base" Then
                    HeaderTd.Controls.Add(lbl)
                Else
                    HeaderTd.Controls.Add(Link)
                End If
                HeaderTd.Controls.Add(hyd)
                HeaderTdSetting(HeaderTd, "100px", "", "1")

            End If
            HeaderTd.Font.Size = 8.5
            HeaderTd.Font.Name = "Verdana"
            HeaderTd.Font.Bold = False
            HeaderTr.Controls.Add(HeaderTd)
        Next
        tblCAGR.Controls.Add(HeaderTr)


        'Inner Row
        For i = 0 To RowCnt
            Tr = New TableRow()
            For j = 0 To ColCnt
                Td = New TableCell()
                If j = 0 Then
                    HeaderTdSetting(Td, "150px", "", "1")
                    hyd = New HiddenField
                    Link = New HyperLink
                    lbl = New Label
                    Link.ID = "Row_" + i.ToString()
                    hyd.ID = "Row_ID_" + i.ToString()
                    hyd.Value = dsRptRws.Tables(0).Rows(i).Item("USERREPORTROWID").ToString()
                    Link.CssClass = "LinkM"
                    Link.Text = dsRptRws.Tables(0).Rows(i).Item("ROWVALUE").ToString() + " (" + dsRptRws.Tables(0).Rows(i).Item("UNITSHRT").ToString() + ")"
                    lbl.Text = Link.Text
                    GetRowLink(dsRptRws.Tables(0).Rows(i).Item("ROWSEQUENCE").ToString(), Link, dsRptRws.Tables(0).Rows(i).Item("USERREPORTID").ToString(), hyd.ClientID, hyd.Value)
                    Td.Style.Add("text-align", "Left")
                    Td.Style.Add("padding-left", "5px")
                    If rptType = "Base" Then
                        Td.Controls.Add(lbl)
                    Else
                        Td.Controls.Add(Link)
                    End If
                    Td.Controls.Add(hyd)

                Else
                    Dim k As New Integer
                    k = j - 1
                    Dim ColType As String = dsRptCols.Tables(0).Rows(k).Item("COLUMNVALUETYPE").ToString()
                    Dim RowType As String = dsRptRws.Tables(0).Rows(i).Item("ROWVALUETYPE").ToString()
                    Dim RowId As String = String.Empty
                    Dim ColId As String = String.Empty
                    Dim RowValue As String = String.Empty
                    Dim ColValue As String = String.Empty
                    Dim UnitId As String = String.Empty
                    Dim UnitValue As String = String.Empty
                    RowId = dsRptRws.Tables(0).Rows(i).Item("ROWCOLUMNID").ToString()
                    RowValue = dsRptRws.Tables(0).Rows(i).Item("ROWVALUEID").ToString()
                    ColId = dsRptCols.Tables(0).Rows(k).Item("COLCOLUMNID").ToString()
                    ColValue = dsRptCols.Tables(0).Rows(k).Item("COLUMNVALUEID").ToString()
                    UnitId = "UnitId"
                    UnitValue = dsRptRws.Tables(0).Rows(i).Item("UNITID").ToString()

                    If ColType = "Year" Then
                        'If RowType = "Category" Then
                        '    RowValue = "9999" + RowValue
                        'End If
                        If GetFactValue(dsRptAct, RowId, ColId, RowValue, ColValue, UnitId, UnitValue) <> String.Empty Then
                            Td.Text = FormatNumber(GetFactValue(dsRptAct, RowId, ColId, RowValue, ColValue, UnitId, UnitValue), 0)
                        Else
                            Td.Text = ""
                        End If

                    Else
                        Td.Text = "0.00"
                        Dim BeginYearId As String = String.Empty
                        Dim EndYearId As String = String.Empty

                        Dr = dsRptCols.Tables(0).Select("USERREPORTCOLUMNID=" + dsRptCols.Tables(0).Rows(k).Item("INPUTVALUETYPE1").ToString() + "")

                        BeginYearId = Dr(0).Item("COLUMNVALUEID").ToString() 'dsRptCols.Tables(0).Rows(k).Item("INPUTVALUETYPE1").ToString()
                        Dr = dsRptCols.Tables(0).Select("USERREPORTCOLUMNID=" + dsRptCols.Tables(0).Rows(k).Item("INPUTVALUETYPE2").ToString() + "")
                        EndYearId = Dr(0).Item("COLUMNVALUEID").ToString() 'dsRptCols.Tables(0).Rows(k).Item("INPUTVALUETYPE2").ToString()
                        'If RowType = "Category" Then
                        '    RowValue = "9999" + RowValue
                        'End If
                        Td.Text = FormatNumber(GetCAGR(dsRptAct, RowId, ColId, RowValue, UnitId, UnitValue, BeginYearId, EndYearId), 4)
                    End If
                    InnerTdSetting(Td, "", "Right")
                    Td.CssClass = "AlterNateColor2"
                End If
                Td.Font.Size = 8.5
                Td.Font.Name = "Verdana"
                Td.Font.Bold = False
                Tr.Controls.Add(Td)
            Next
            tblCAGR.Controls.Add(Tr)
        Next
        tblCAGR.Visible = True
    End Sub

    Protected Function GetFactValueTotal(ByVal ds As DataSet, ByVal colId As String, ByVal colValue As String) As Decimal
        ' Dim Fact As New Decimal
        Dim dr() As DataRow
        Dim i As Integer
        Dim total As Decimal
        Try
            dr = ds.Tables(0).Select("" + colId + "=" + colValue + "")
            total = 0
            For i = 0 To dr.Length - 1 'ds.Tables(0).Rows.Count - 1
                total = Convert.ToDecimal(dr(i).Item("FACTRES")) + total
            Next
        Catch ex As Exception
            'ErrorLable.Text = "Error:GetFactValue:" + ex.Message.ToString() + ""
        End Try
        Return total
    End Function

    Protected Function GetCAGRValueTotal(ByVal ds As DataSet, ByVal colId As String, ByVal BeginYear As String, ByVal EndYear As String) As Decimal
        Dim CAGR As New Decimal
        Dim BeginYearFct As New Decimal
        Dim EndYearFct As New Decimal
        Dim YearDiff As New Decimal
        Try
            BeginYearFct = GetFactValueTotal(ds, colId, BeginYear)
            EndYearFct = GetFactValueTotal(ds, colId, EndYear)
            YearDiff = EndYear - BeginYear
            CAGR = (((EndYearFct / BeginYearFct) ^ (1 / YearDiff)) - 1) * 100
            Return CAGR
        Catch ex As Exception

        End Try
    End Function

    Protected Function GetFactValue(ByVal ds As DataSet, ByVal RowId As String, ByVal ColId As String, ByVal RowValue As String, ByVal ColValue As String, ByVal UnitId As String, ByVal UnitValue As String) As String
        'Dim Fact As New Decimal
        Dim fact As String
        Dim dr() As DataRow
        Try
            If ds.Tables(0).Columns.Item(0).ColumnName = "SUBGROUPID" Then
                RowId = "SUBGROUPID"
            End If
            dr = ds.Tables(0).Select("" + RowId + "=" + RowValue + " And " + ColId + "=" + ColValue + "And " + UnitId + "=" + UnitValue + "")
            If dr.Length > 0 Then
                fact = Convert.ToDecimal(dr(0).Item("FACTRES"))
            Else
                fact = ""
            End If

            '* Convert.ToDecimal(dr(0).Item("CURR"))
        Catch ex As Exception

        End Try
        Return Fact
    End Function

    Protected Function GetCAGR(ByVal ds As DataSet, ByVal RowId As String, ByVal ColId As String, ByVal RowValue As String, ByVal UnitId As String, ByVal UnitValue As String, ByVal BeginYear As String, ByVal EndYear As String) As Decimal
        Dim CAGR As New Decimal
        Dim BeginYearFct As New Decimal
        Dim EndYearFct As New Decimal
        Dim YearDiff As New Decimal
        Try
            If (GetFactValue(ds, RowId, ColId, RowValue, BeginYear, UnitId, UnitValue)) <> String.Empty Then
                BeginYearFct = GetFactValue(ds, RowId, ColId, RowValue, BeginYear, UnitId, UnitValue)
            End If
            If (GetFactValue(ds, RowId, ColId, RowValue, EndYear, UnitId, UnitValue)) <> String.Empty Then
                EndYearFct = GetFactValue(ds, RowId, ColId, RowValue, EndYear, UnitId, UnitValue)
            End If
            YearDiff = EndYear - BeginYear
            CAGR = (((EndYearFct / BeginYearFct) ^ (1 / YearDiff)) - 1) * 100
            Return CAGR
        Catch ex As Exception

        End Try
    End Function

    Protected Sub GetColLink(ByVal Seq As String, ByVal Link As HyperLink, ByVal RptId As String, ByVal hidId As String, ByVal hidValue As String)
        Dim Path As String
        Try
            Path = "../PopUp/ColSelector.aspx?RptId=" + RptId + "&Seq=" + Seq.ToString() + "&Id=ctl00_Market1ContentPlaceHolder_" + Link.ClientID + "&hidId=ctl00_Market1ContentPlaceHolder_" + hidId + ""
            Link.NavigateUrl = "javascript:ShowPopWindow('" + Path + "','ctl00_Market1ContentPlaceHolder_" + hidId + "')"
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetRowLink(ByVal Seq As String, ByVal Link As HyperLink, ByVal RptId As String, ByVal hidId As String, ByVal hidValue As String)
        Dim Path As String

        Dim RowVal2D As String = String.Empty
        Dim RegionSetId As String = String.Empty
        Dim Curr As String = String.Empty
        Try

            Path = "../PopUp/RowSelector.aspx?RptId=" + RptId + "&Seq=" + Seq.ToString() + "&Id=ctl00_Market1ContentPlaceHolder_" + Link.ClientID + "&hidId=ctl00_Market1ContentPlaceHolder_" + hidId
            Link.NavigateUrl = "javascript:ShowPopWindow('" + Path + "','ctl00_Market1ContentPlaceHolder_" + hidId + "')"
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetFilterLink(ByVal Seq As String, ByVal Link As HyperLink, ByVal RptId As String, ByVal hidId As String, ByVal hidValue As String)
        Dim Path As String
        Try
            Path = "../PopUp/FilterSelector.aspx?RptId=" + RptId + "&Seq=" + Seq.ToString() + "&Id=ctl00_Market1ContentPlaceHolder_" + Link.ClientID + "&hidId=ctl00_Market1ContentPlaceHolder_" + hidId + ""
            Link.NavigateUrl = "javascript:ShowPopWindow('" + Path + "','ctl00_Market1ContentPlaceHolder_" + hidId + "')"
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub HeaderTdSetting(ByVal Td As TableCell, ByVal Width As String, ByVal HeaderText As String, ByVal ColSpan As String)
        Try
            'Td.Text = HeaderText
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
            _lErrorLble.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
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
            _lErrorLble.Text = "Error:InnerTdSetting:" + ex.Message.ToString()
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
            _lErrorLble.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub Update_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Try
            GetPageDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:HeaderTdSetting:" + ex.Message.ToString()
        End Try
    End Sub
End Class
