Imports System.Data
Imports System.Data.OleDb
Imports System
Imports M1SubGetData
Imports M1SubUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Market1_PopUp_FilterSelector
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            hidFilterDes.Value = Request.QueryString("Id").ToString()
            hidFilterId.Value = Request.QueryString("hidId").ToString()
            Session("GrpRptId") = Request.QueryString("RptID").ToString()
            Session("isTemp") = Request.QueryString("isTemp").ToString()
            getFilterSelector()
        End If
    End Sub
    Protected Sub btnSumitt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSumitt.Click
        Dim RptID As String = Request.QueryString("RptID").ToString()
        Dim objUpIns As New UpdateInsert()
        Dim filterValue As String = String.Empty
        Dim filterValueID As String = String.Empty
        Dim hidValue As Integer = CInt(Request.QueryString("hidValue"))
        Dim hidId As String = Request.QueryString("hidId")

        Dim colValueId As String = String.Empty
        Try
            If hidCatID.Value <> "0" Then
                filterValue = hidCatDes1.Value
                filterValueID = hidCatID.Value
            Else
                filterValue = ddlFilterValue.SelectedItem.ToString()
                filterValueID = ddlFilterValue.SelectedValue.ToString()
            End If

            Dim dsRptCols As New DataSet
            Dim objGetData As New Selectdata
            Dim pSubTypeid As String = "null"
            Dim pSubTypeValue As String = String.Empty
            Dim pSelId As String = "null"
            Dim pSelValue As String = ""
            Dim seq As String = "null"
            Dim dr() As DataRow
            Dim dsrpt As DataSet
            Dim rptType As String
            Dim rptName As String
            Dim rptDes As String = ""
            dsRptCols = objGetData.GetUsersReportFilters(RptID)
            If Request.QueryString("isTemp").ToString() = "Y" Then
                dsrpt = GetReportNameTemp(RptID)
            Else
                dsrpt = GetReportName(RptID)
            End If
            If dsrpt.Tables(0).Rows.Count > 0 Then
                rptName = dsrpt.Tables(0).Rows(0)("REPORTNAME").ToString()
                rptType = dsrpt.Tables(0).Rows(0)("RPTTYPE").ToString()
                rptDes = dsrpt.Tables(0).Rows(0)("RPTTYPEDES").ToString()
            End If
            'If dsRptCols.Tables(0).Rows.Count > 0 Then
            '    dr = dsRptCols.Tables(0).Select("USERREPORTFILTERID=" + hidValue.ToString())
            '    pSubTypeid = IIf(dr(0).Item("FILTERTYPEID").ToString() = "", "null", dr(0).Item("FILTERTYPEID").ToString())
            '    pSubTypeValue = IIf(dr(0).Item("FILTERTYPE").ToString() = "", "", dr(0).Item("FILTERTYPE").ToString())
            '    pSelId = IIf(dr(0).Item("FILTERVALUEID").ToString() = "", "null", dr(0).Item("FILTERVALUEID").ToString())
            '    pSelValue = IIf(dr(0).Item("FILTERVALUE").ToString() = "", "", dr(0).Item("FILTERVALUE").ToString())
            '    seq = IIf(dr(0).Item("FILTERSEQUENCE").ToString() = "", "null", dr(0).Item("FILTERSEQUENCE").ToString())
            'End If

            If hidValue <> 0 Then
                If Request.QueryString("isTemp").ToString() = "Y" Then
                    objUpIns.UpdateFilterDetailsTemp(ddlFilterType.SelectedItem.Text, filterValue, ddlFilterType.SelectedValue.ToString(), filterValueID, hidValue.ToString())
                    If rptType = "MIXED" Then

                        If ddlFilterType.SelectedItem.Text = "Region" Then
                            objUpIns.EditReportTypeDesTemp(RptID, "REGION")
                        ElseIf ddlFilterType.SelectedItem.Text = "Country" Then
                            objUpIns.EditReportTypeDesTemp(RptID, "CNTRY")
                        Else
                            If rptDes = "" Or rptDes = "0" Then
                                objUpIns.EditReportTypeDesTemp(RptID, "REGION")
                            End If
                        End If
                    End If
                ElseIf Request.QueryString("isTemp").ToString() = "N" Then
                    objUpIns.UpdateFilterDetails(ddlFilterType.SelectedItem.Text, filterValue, ddlFilterType.SelectedValue.ToString(), filterValueID, hidValue.ToString())
                    If rptType = "MIXED" Then
                        If ddlFilterType.SelectedItem.Text = "Region" Then
                            objUpIns.EditReportTypeDes(RptID, "REGION")
                        ElseIf ddlFilterType.SelectedItem.Text = "Country" Then
                            objUpIns.EditReportTypeDes(RptID, "CNTRY")
                        End If
                    End If
                End If

                Dim dsRp As New DataSet
                dsRp = objGetData.GetUserCustomReportsByRptIdTemp(RptID)
                If dsRp.Tables(0).Rows.Count > 0 Then
                    Dim rptDesc As String = ""
                    If Session("RPTTYPE") = "MIXED" Then
                        rptDesc = ""
                    Else
                        rptDesc = dsRp.Tables(0).Rows(0).Item("RPTTYPEDES").ToString()
                    End If
                    If ddlFilterType.SelectedItem.Text = "Region" Then
                        objUpIns.InsertLog(Session("UserId"), Session("MLogInLog"), "5", RptID.ToString(), Session("RPTTYPE"), rptDesc, "8", filterValueID, Session.SessionID)
                    ElseIf ddlFilterType.SelectedItem.Text = "Country" Then
                        objUpIns.InsertLog(Session("UserId"), Session("MLogInLog"), "5", RptID.ToString(), Session("RPTTYPE"), rptDesc, "9", filterValueID, Session.SessionID)
                    ElseIf ddlFilterType.SelectedItem.Text = "Product" Then
                        objUpIns.InsertLog(Session("UserId"), Session("MLogInLog"), "5", RptID.ToString(), Session("RPTTYPE"), rptDesc, "6", filterValueID, Session.SessionID)
                    ElseIf ddlFilterType.SelectedItem.Text = "Package" Then
                        objUpIns.InsertLog(Session("UserId"), Session("MLogInLog"), "5", RptID.ToString(), Session("RPTTYPE"), rptDesc, "7", filterValueID, Session.SessionID)
                    ElseIf ddlFilterType.SelectedItem.Text = "Group" Then
                        objUpIns.InsertLog(Session("UserId"), Session("MLogInLog"), "5", RptID.ToString(), Session("RPTTYPE"), rptDesc, "11", filterValueID, Session.SessionID)
                    ElseIf ddlFilterType.SelectedItem.Text = "Material" Then
                        objUpIns.InsertLog(Session("UserId"), Session("MLogInLog"), "5", RptID.ToString(), Session("RPTTYPE"), rptDesc, "10", filterValueID, Session.SessionID)
                    ElseIf ddlFilterType.SelectedItem.Text = "Component" Then
                        objUpIns.InsertLog(Session("UserId"), Session("MLogInLog"), "5", RptID.ToString(), Session("RPTTYPE"), rptDesc, "12", filterValueID, Session.SessionID)
                    End If
                End If

            End If

            ClientScript.RegisterStartupScript(Me.GetType(), "FilterSel", "FilterSelection('" + filterValue.ToString() + "','" + hidValue.ToString() + "','" + hidId + "');", True)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetAndFillYears(ByVal ddl As DropDownList)
        Dim objGetDate As New Selectdata()
        Dim ds As New DataSet()
        Try
            ds = objGetDate.GetYears()
            With ddl
                .DataSource = ds
                .DataTextField = "YEAR"
                .DataValueField = "YEARID"
                .DataBind()
            End With


        Catch ex As Exception
            _lErrorLble.Text = "Error:GetAndFillYears" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub getFilterSelector()
        Dim objGetDate As New Selectdata()
        Dim ds As New DataSet()
        Dim dsRpt As New DataSet()
        Dim dsRow As New DataSet
        Dim lst As New ListItem
        Dim RptType As String = String.Empty
        Dim GrpName As String = ""
        Dim dv As New DataView
        Dim dt As New DataTable
        Try

            dsRpt = GetReportNameTemp(Request.QueryString("RptID").ToString())
            dsRow = objGetDate.GetUsersReportRowsTemp(Request.QueryString("RptID").ToString())
            If Session("RPTTYPE") = "MIXED" Then
                ds = objGetDate.GetMixedFilterSelector(-1)
            Else
                ds = objGetDate.GetFilterSelector(-1)
            End If

            If dsRpt.Tables(0).Rows(0).Item("RPTTYPE").ToString().ToUpper() = "UNIFORM" Then
                dv = ds.Tables(0).DefaultView
                If dsRpt.Tables(0).Rows(0).Item("RPTTYPEDES").ToString().ToUpper() = "CNTRY" Then
                    dv.RowFilter = "FILTERDES NOT IN ('Country')"
                ElseIf dsRpt.Tables(0).Rows(0).Item("RPTTYPEDES").ToString().ToUpper() = "GROUP" Then
                    dv.RowFilter = "FILTERDES NOT IN ('Group','Material'))"
                ElseIf dsRpt.Tables(0).Rows(0).Item("RPTTYPEDES").ToString().ToUpper() = "MAT" Then
                    dv.RowFilter = "FILTERDES NOT IN ('Material','Group')"
                ElseIf dsRpt.Tables(0).Rows(0).Item("RPTTYPEDES").ToString().ToUpper() = "MATGRP" Then
                    dv.RowFilter = "FILTERDES NOT IN ('MaterialGrp')"
                ElseIf dsRpt.Tables(0).Rows(0).Item("RPTTYPEDES").ToString().ToUpper() = "PACK" Then
                    dv.RowFilter = "FILTERDES NOT IN ('Package')"
                ElseIf dsRpt.Tables(0).Rows(0).Item("RPTTYPEDES").ToString().ToUpper() = "PROD" Then
                    dv.RowFilter = "FILTERDES NOT IN ('Product')"
                ElseIf dsRpt.Tables(0).Rows(0).Item("RPTTYPEDES").ToString().ToUpper() = "REGION" Then
                    dv.RowFilter = "FILTERDES NOT IN ('Region')"
                ElseIf dsRpt.Tables(0).Rows(0).Item("RPTTYPEDES").ToString().ToUpper() = "COMPONENT" Then
                    dv.RowFilter = "FILTERDES NOT IN ('Component')"
                End If
                dt = dv.ToTable()
                ds.Tables.Clear()
                ds.Tables.Add(dt)
            Else
                If dsRow.Tables(0).Rows(0).Item("ROWVALUETYPE").ToString() <> "" Then
                    For i = 0 To dsRow.Tables(0).Rows.Count - 1
                        GrpName = GrpName + " " + "'" + dsRow.Tables(0).Rows(i).Item("ROWVALUETYPE").ToString() + "',"
                    Next
                    GrpName = GrpName.Remove(GrpName.Length - 1)
                    dv = ds.Tables(0).DefaultView
                    dv.RowFilter = "FILTERDES NOT IN (" + GrpName + ")"
                    dt = dv.ToTable()
                    ds.Tables.Clear()
                    ds.Tables.Add(dt)
                End If
            End If
            'ds = objGetDate.GetFilterSelector(-1)
            lst.Text = "--Select--"
            lst.Value = "0"
            lst.Attributes.Add("FILTERTYPEID", "-1")
            lst.Attributes.Add("FILTERDES", "SELECT")

            With ddlFilterType
                .Items.Add(lst)
                .SelectedItem.Value = "0"
                .AppendDataBoundItems = True
                .DataSource = ds
                .DataTextField = "FILTERDES"
                .DataValueField = "FILTERTYPEID"
                .DataBind()
            End With
        Catch ex As Exception
            _lErrorLble.Text = "Error:getFilterSelector" + ex.Message.ToString()
        End Try
    End Sub

    Private Sub GetComponentValue(ByVal RptID As Integer)
        Dim objGetDate As New Selectdata()
        Dim ds As New DataSet()
        Dim objGetData As New Selectdata()
        Dim Dts As DataSet
        Dim dsMat As DataSet
        Dim dsFilter As New DataSet
        Dim FactId As String = String.Empty
        Dim PackId As String = String.Empty
        Dim MatId As String = String.Empty
        Dim MatGrpId As String = String.Empty
        Dim GroupId As String = String.Empty
        Dim StrSql As String = String.Empty
        Dim j As Integer = 0
        Dim dsProd As DataSet
        Dim dsFact As DataSet
        Dim dsProdMat As DataSet
        Dim PackValue As String = String.Empty
        Dim FiltValue As String = String.Empty
        Dim arrFact() As String
        Dim ProdId As String = String.Empty
        Try
            ddlFilterValue.Visible = True
            lnkProductTree.Visible = False
            rwRegionSet.Visible = False

            If Request.QueryString("isTemp").ToString() = "Y" Then
                ds = objGetData.GetUserReportProdFilter(RptID)
            Else
                ds = objGetData.GetUserReportProdFilterR(RptID)
            End If

            If ds.Tables(0).Rows.Count > 0 Then

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If ds.Tables(0).Rows(i).Item("FILTERTYPE").ToString() = "Product" Then
                        If ProdId = String.Empty Then
                            ProdId = ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        Else
                            If ProdId <> "0" Then
                                ProdId = ProdId + "," + ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                            End If
                        End If

                    ElseIf ds.Tables(0).Rows(i).Item("FILTERTYPE").ToString() = "Package" Then
                        If PackId = String.Empty Then
                            PackId = ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        Else
                            If PackId <> "0" Then
                                PackId = PackId + "," + ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                            End If
                        End If
                    End If
                Next

                If ProdId = "0" Then
                    arrFact = Regex.Split(Session("M1SubGroupId"), ",")
                    For i = 0 To arrFact.Length - 1
                        dsFact = objGetData.GetSubGroupDetails(arrFact(i))

                        If dsFact.Tables(0).Rows(0).Item("CATID").ToString() <> "" Then
                            If i = 0 Then
                                FactId = dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            Else
                                FactId = FactId + dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            End If

                        End If
                    Next
                    FactId = FactId.Remove(FactId.Length - 1)

                    dsFact = objGetData.GetSubFactGroupDetails(FactId)
                    FactId = String.Empty
                    For i = 0 To dsFact.Tables(0).Rows.Count - 1
                        If dsFact.Tables(0).Rows(0).Item("ID").ToString() <> "" Then
                            If i = 0 Then
                                FactId = dsFact.Tables(0).Rows(i).Item("ID").ToString()
                            Else
                                FactId = FactId + "," + dsFact.Tables(0).Rows(i).Item("ID").ToString()
                            End If
                        End If
                    Next
                End If

                If PackId = "0" Then
                    If FactId = "" Then
                        arrFact = Regex.Split(Session("M1SubGroupId"), ",")
                        For i = 0 To arrFact.Length - 1
                            dsFact = objGetData.GetSubGroupDetails(arrFact(i))

                            If dsFact.Tables(0).Rows(0).Item("CATID").ToString() <> "" Then
                                If i = 0 Then
                                    FactId = dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                                Else
                                    FactId = FactId + dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                                End If

                            End If
                        Next
                        FactId = FactId.Remove(FactId.Length - 1)

                        dsFact = objGetData.GetSubFactGroupDetails(FactId)
                        FactId = String.Empty
                        For i = 0 To dsFact.Tables(0).Rows.Count - 1
                            If dsFact.Tables(0).Rows(0).Item("ID").ToString() <> "" Then
                                If i = 0 Then
                                    FactId = dsFact.Tables(0).Rows(i).Item("ID").ToString()
                                Else
                                    FactId = FactId + "," + dsFact.Tables(0).Rows(i).Item("ID").ToString()
                                End If
                            End If
                        Next
                    End If
                    dsProd = objGetData.GetPackageTypeByFact(FactId)

                    For i = 0 To dsProd.Tables(0).Rows.Count - 1
                        If dsProd.Tables(0).Rows(i).Item("ID").ToString() <> "" Then
                            If i = 0 Then
                                PackId = dsProd.Tables(0).Rows(i).Item("ID").ToString()
                            Else
                                PackId = PackId + "," + dsProd.Tables(0).Rows(i).Item("ID").ToString()
                            End If
                        End If
                    Next
                End If


                If ProdId <> "" And PackId <> "" Then

                    If ProdId = "0" Then
                        ProdId = FactId
                    End If

                    dsProd = objGetData.GetProdPackComponent(ProdId, PackId)

                    For i = 0 To dsProd.Tables(0).Rows.Count - 1
                        If dsProd.Tables(0).Rows(i).Item("ID").ToString() <> "" Then
                            If i = 0 Then
                                MatId = dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            Else
                                MatId = MatId + dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            End If
                        End If
                    Next
                    MatId = MatId.Remove(MatId.Length - 1)
                    dsMat = objGetData.GetComponent(MatId)

                ElseIf ProdId <> "" Then

                    If ProdId = "0" Then
                        ProdId = FactId
                    End If

                    dsProd = objGetData.GetPivotProdComponent(ProdId, "")

                    For i = 0 To dsProd.Tables(0).Rows.Count - 1
                        If dsProd.Tables(0).Rows(i).Item("ID").ToString() <> "" Then
                            If i = 0 Then
                                MatId = dsProd.Tables(0).Rows(i).Item("COMPONENTID").ToString() + ","
                            Else
                                MatId = MatId + dsProd.Tables(0).Rows(i).Item("COMPONENTID").ToString() + ","
                            End If
                        End If
                    Next
                    MatId = MatId.Remove(MatId.Length - 1)
                    dsMat = objGetData.GetComponent(MatId)

                ElseIf PackId <> "" Then
                    dsProd = objGetData.GetPackComponents(PackId)
                    FactId = String.Empty
                    For i = 0 To dsProd.Tables(0).Rows.Count - 1
                        If dsProd.Tables(0).Rows(i).Item("ID").ToString() <> "" Then
                            If i = 0 Then
                                FactId = dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            Else
                                FactId = FactId + dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            End If
                        End If
                    Next
                    FactId = FactId.Remove(FactId.Length - 1)
                    dsMat = objGetData.GetComponent(FactId)

                Else
                    FactId = String.Empty
                    arrFact = Regex.Split(Session("M1SubGroupId"), ",")
                    For i = 0 To arrFact.Length - 1
                        Dts = objGetData.GetSubGroupDetails(arrFact(i))

                        If Dts.Tables(0).Rows(0).Item("CATID").ToString() <> "" Then
                            If i = 0 Then
                                FactId = Dts.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            Else
                                FactId = FactId + Dts.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            End If

                        End If
                    Next
                    FactId = FactId.Remove(FactId.Length - 1)
                    Dts = objGetData.GetSubFactGroupDetails(FactId)
                    If Dts.Tables(0).Rows.Count > 0 Then
                        For i = 0 To Dts.Tables(0).Rows.Count - 1
                            If Dts.Tables(0).Rows(i).Item("ID").ToString() <> "" Then
                                If i = 0 Then
                                    StrSql = Dts.Tables(0).Rows(i).Item("ID").ToString() + ","
                                Else
                                    StrSql = StrSql + Dts.Tables(0).Rows(i).Item("ID").ToString() + ","
                                End If

                            End If
                        Next
                        StrSql = StrSql.Remove(StrSql.Length - 1)
                        dsProdMat = objGetData.GetPivotProdComponent(StrSql, "")

                        If dsProdMat.Tables(0).Rows.Count > 0 Then
                            For i = 0 To dsProdMat.Tables(0).Rows.Count - 1
                                If dsProdMat.Tables(0).Rows(i).Item("ID").ToString() <> "" Then
                                    If i = 0 Then
                                        ProdId = dsProdMat.Tables(0).Rows(i).Item("COMPONENTID").ToString() + ","
                                    Else
                                        ProdId = ProdId + dsProdMat.Tables(0).Rows(i).Item("COMPONENTID").ToString() + ","
                                    End If

                                End If
                            Next
                            ProdId = ProdId.Remove(ProdId.Length - 1)
                            dsMat = objGetData.GetComponent(ProdId)
                        End If
                    End If
                End If
            End If

            ddlFilterValue.Items.Clear()
            Dim lst As New ListItem
            Dim lst1 As New ListItem
            lst.Text = "Nothing Selected"
            lst.Value = -1
            lst1.Text = "All Components"
            lst1.Value = 0
            ddlFilterValue.Items.Add(lst)
            ddlFilterValue.Items.Add(lst1)
            ddlFilterValue.AppendDataBoundItems = True
            With ddlFilterValue
                .DataSource = dsMat
                .DataTextField = "VALUE"
                .DataValueField = "ID"
                .DataBind()
            End With

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetFilterValue" + ex.Message.ToString()
        End Try
    End Sub


    Private Sub GetMaterialValue(ByVal RptID As Integer)
        Dim objGetDate As New Selectdata()
        Dim ds As New DataSet()
        Dim objGetData As New Selectdata()
        Dim Dts As DataSet
        Dim dsMat As DataSet
        Dim dsFilter As New DataSet
        Dim FactId As String = String.Empty
        Dim PackId As String = String.Empty
        Dim MatId As String = String.Empty
        Dim MatGrpId As String = String.Empty
        Dim GroupId As String = String.Empty
        Dim StrSql As String = String.Empty
        Dim j As Integer = 0
        Dim dsProd As DataSet
        Dim dsFact As DataSet
        Dim dsProdMat As DataSet
        Dim PackValue As String = String.Empty
        Dim FiltValue As String = String.Empty
        Dim arrFact() As String
        Dim ProdId As String = String.Empty
        Try
            ddlFilterValue.Visible = True
            lnkProductTree.Visible = False
            rwRegionSet.Visible = False

            If Request.QueryString("isTemp").ToString() = "Y" Then
                ds = objGetData.GetUserReportProdFilter(RptID)
            Else
                ds = objGetData.GetUserReportProdFilterR(RptID)
            End If

            If ds.Tables(0).Rows.Count > 0 Then

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If ds.Tables(0).Rows(i).Item("FILTERTYPE").ToString() = "Product" Then
                        If ProdId = String.Empty Then
                            ProdId = ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        Else
                            If ProdId <> "0" Then
                                ProdId = ProdId + "," + ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                            End If
                        End If

                    ElseIf ds.Tables(0).Rows(i).Item("FILTERTYPE").ToString() = "Package" Then
                        If PackId = String.Empty Then
                            PackId = ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        Else
                            If PackId <> "0" Then
                                PackId = PackId + "," + ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                            End If
                        End If
                    End If
                Next

                If ProdId = "0" Then
                    arrFact = Regex.Split(Session("M1SubGroupId"), ",")
                    For i = 0 To arrFact.Length - 1
                        dsFact = objGetData.GetSubGroupDetails(arrFact(i))

                        If dsFact.Tables(0).Rows(0).Item("CATID").ToString() <> "" Then
                            If i = 0 Then
                                FactId = dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            Else
                                FactId = FactId + dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            End If

                        End If
                    Next
                    FactId = FactId.Remove(FactId.Length - 1)

                    dsFact = objGetData.GetSubFactGroupDetails(FactId)
                    FactId = String.Empty
                    For i = 0 To dsFact.Tables(0).Rows.Count - 1
                        If dsFact.Tables(0).Rows(0).Item("ID").ToString() <> "" Then
                            If i = 0 Then
                                FactId = dsFact.Tables(0).Rows(i).Item("ID").ToString()
                            Else
                                FactId = FactId + "," + dsFact.Tables(0).Rows(i).Item("ID").ToString()
                            End If
                        End If
                    Next
                End If

                If PackId = "0" Then
                    If FactId = "" Then
                        arrFact = Regex.Split(Session("M1SubGroupId"), ",")
                        For i = 0 To arrFact.Length - 1
                            dsFact = objGetData.GetSubGroupDetails(arrFact(i))

                            If dsFact.Tables(0).Rows(0).Item("CATID").ToString() <> "" Then
                                If i = 0 Then
                                    FactId = dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                                Else
                                    FactId = FactId + dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                                End If

                            End If
                        Next
                        FactId = FactId.Remove(FactId.Length - 1)

                        dsFact = objGetData.GetSubFactGroupDetails(FactId)
                        FactId = String.Empty
                        For i = 0 To dsFact.Tables(0).Rows.Count - 1
                            If dsFact.Tables(0).Rows(0).Item("ID").ToString() <> "" Then
                                If i = 0 Then
                                    FactId = dsFact.Tables(0).Rows(i).Item("ID").ToString()
                                Else
                                    FactId = FactId + "," + dsFact.Tables(0).Rows(i).Item("ID").ToString()
                                End If
                            End If
                        Next
                    End If
                    dsProd = objGetData.GetPackageTypeByFact(FactId)

                    For i = 0 To dsProd.Tables(0).Rows.Count - 1
                        If dsProd.Tables(0).Rows(i).Item("ID").ToString() <> "" Then
                            If i = 0 Then
                                PackId = dsProd.Tables(0).Rows(i).Item("ID").ToString()
                            Else
                                PackId = PackId + "," + dsProd.Tables(0).Rows(i).Item("ID").ToString()
                            End If
                        End If
                    Next
                End If


                If ProdId <> "" And PackId <> "" Then

                    If ProdId = "0" Then
                        ProdId = FactId
                    End If

                    dsProd = objGetData.GetSubscPackProdMaterials(ProdId, PackId)

                    For i = 0 To dsProd.Tables(0).Rows.Count - 1
                        If dsProd.Tables(0).Rows(i).Item("ID").ToString() <> "" Then
                            If i = 0 Then
                                MatId = dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            Else
                                MatId = MatId + dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            End If
                        End If
                    Next
                    MatId = MatId.Remove(MatId.Length - 1)
                    dsMat = objGetData.GetSubscProdMaterials(MatId)

                ElseIf ProdId <> "" Then

                    If ProdId = "0" Then
                        ProdId = FactId
                    End If

                    dsProd = objGetData.GetSubscMaterials(ProdId)

                    For i = 0 To dsProd.Tables(0).Rows.Count - 1
                        If dsProd.Tables(0).Rows(i).Item("ID").ToString() <> "" Then
                            If i = 0 Then
                                MatId = dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            Else
                                MatId = MatId + dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            End If
                        End If
                    Next
                    MatId = MatId.Remove(MatId.Length - 1)
                    dsMat = objGetData.GetSubscProdMaterials(MatId)

                ElseIf PackId <> "" Then
                    dsProd = objGetData.GetSubscPackMaterials(PackId)
                    FactId = String.Empty
                    For i = 0 To dsProd.Tables(0).Rows.Count - 1
                        If dsProd.Tables(0).Rows(i).Item("ID").ToString() <> "" Then
                            If i = 0 Then
                                FactId = dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            Else
                                FactId = FactId + dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            End If
                        End If
                    Next
                    FactId = FactId.Remove(FactId.Length - 1)
                    dsMat = objGetData.GetSubscProdMaterials(FactId)

                Else
                    FactId = String.Empty
                    arrFact = Regex.Split(Session("M1SubGroupId"), ",")
                    For i = 0 To arrFact.Length - 1
                        Dts = objGetData.GetSubGroupDetails(arrFact(i))

                        If Dts.Tables(0).Rows(0).Item("CATID").ToString() <> "" Then
                            If i = 0 Then
                                FactId = Dts.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            Else
                                FactId = FactId + Dts.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            End If

                        End If
                    Next
                    FactId = FactId.Remove(FactId.Length - 1)
                    Dts = objGetData.GetSubFactGroupDetails(FactId)
                    If Dts.Tables(0).Rows.Count > 0 Then
                        For i = 0 To Dts.Tables(0).Rows.Count - 1
                            If Dts.Tables(0).Rows(i).Item("ID").ToString() <> "" Then
                                If i = 0 Then
                                    StrSql = Dts.Tables(0).Rows(i).Item("ID").ToString() + ","
                                Else
                                    StrSql = StrSql + Dts.Tables(0).Rows(i).Item("ID").ToString() + ","
                                End If

                            End If
                        Next
                        StrSql = StrSql.Remove(StrSql.Length - 1)
                        dsProdMat = objGetData.GetSubscMaterials(StrSql)

                        If dsProdMat.Tables(0).Rows.Count > 0 Then
                            For i = 0 To dsProdMat.Tables(0).Rows.Count - 1
                                If dsProdMat.Tables(0).Rows(i).Item("ID").ToString() <> "" Then
                                    If i = 0 Then
                                        ProdId = dsProdMat.Tables(0).Rows(i).Item("ID").ToString() + ","
                                    Else
                                        ProdId = ProdId + dsProdMat.Tables(0).Rows(i).Item("ID").ToString() + ","
                                    End If

                                End If
                            Next
                            ProdId = ProdId.Remove(ProdId.Length - 1)
                            dsMat = objGetData.GetSubscProdMaterials(ProdId)
                        End If
                    End If
                End If
            End If

            ddlFilterValue.Items.Clear()
            Dim lst As New ListItem
            lst.Text = "Nothing Selected"
            lst.Value = -1
            ddlFilterValue.Items.Add(lst)
            ddlFilterValue.AppendDataBoundItems = True
            With ddlFilterValue
                .DataSource = dsMat
                .DataTextField = "VALUE"
                .DataValueField = "ID"
                .DataBind()
            End With

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetFilterValue" + ex.Message.ToString()
        End Try
    End Sub

    Private Sub GetPackageValue(ByVal RptID As Integer)
        Dim ds As New DataSet()
        Dim objGetData As New Selectdata()
        Dim Dts As New DataSet
        Dim dsMat As New DataSet
        Dim dsFilter As New DataSet
        Dim FactId As String = String.Empty
        Dim PackId As String = String.Empty
        Dim MatId As String = String.Empty
        Dim MatGrpId As String = String.Empty
        Dim GroupId As String = String.Empty
        Dim StrSql As String = String.Empty
        Dim i As Integer = 0
        Dim dsProd As DataSet
        Dim dsFact As DataSet
        Dim PackValue As String = String.Empty
        Dim FiltValue As String = String.Empty
        Dim arrFact() As String
        Dim ProdId As String = String.Empty
        Dim CompId As String = String.Empty
        Try
            ddlFilterValue.Visible = True
            lnkProductTree.Visible = False
            rwRegionSet.Visible = False

            If Request.QueryString("isTemp").ToString() = "Y" Then
                ds = objGetData.GetUserReportProdFilter(RptID)
            Else
                ds = objGetData.GetUserReportProdFilterR(RptID)
            End If

            If ds.Tables(0).Rows.Count > 0 Then

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If ds.Tables(0).Rows(i).Item("FILTERTYPE").ToString() = "Product" Then
                        If ProdId = String.Empty Then
                            ProdId = ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        Else
                            If ProdId <> "0" Then
                                ProdId = ProdId + "," + ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                            End If
                        End If

                    ElseIf ds.Tables(0).Rows(i).Item("FILTERTYPE").ToString() = "Material" Then
                        If MatId = String.Empty Then
                            MatId = ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        Else
                            MatId = MatId + "," + ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        End If

                    ElseIf ds.Tables(0).Rows(i).Item("FILTERTYPE").ToString() = "Component" Then
                        If CompId = String.Empty Then
                            CompId = ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        Else
                            CompId = CompId + "," + ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        End If

                    End If
                Next
                MatId = ""
                If ProdId = "0" Then
                    arrFact = Regex.Split(Session("M1SubGroupId"), ",")
                    For i = 0 To arrFact.Length - 1
                        dsFact = objGetData.GetSubGroupDetails(arrFact(i))

                        If dsFact.Tables(0).Rows(0).Item("CATID").ToString() <> "" Then
                            If i = 0 Then
                                FactId = dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            Else
                                FactId = FactId + dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            End If

                        End If
                    Next
                    FactId = FactId.Remove(FactId.Length - 1)

                    dsFact = objGetData.GetSubFactGroupDetails(FactId)
                    FactId = String.Empty
                    For i = 0 To dsFact.Tables(0).Rows.Count - 1
                        If dsFact.Tables(0).Rows(0).Item("ID").ToString() <> "" Then
                            If i = 0 Then
                                FactId = dsFact.Tables(0).Rows(i).Item("ID").ToString()
                            Else
                                FactId = FactId + "," + dsFact.Tables(0).Rows(i).Item("ID").ToString()
                            End If
                        End If
                    Next
                End If

                If CompId = "0" Then
                    If FactId = "" Then
                        arrFact = Regex.Split(Session("M1SubGroupId"), ",")
                        For i = 0 To arrFact.Length - 1
                            dsFact = objGetData.GetSubGroupDetails(arrFact(i))

                            If dsFact.Tables(0).Rows(0).Item("CATID").ToString() <> "" Then
                                If i = 0 Then
                                    FactId = dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                                Else
                                    FactId = FactId + dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                                End If

                            End If
                        Next
                        FactId = FactId.Remove(FactId.Length - 1)

                        dsFact = objGetData.GetSubFactGroupDetails(FactId)
                        FactId = String.Empty
                        For i = 0 To dsFact.Tables(0).Rows.Count - 1
                            If dsFact.Tables(0).Rows(0).Item("ID").ToString() <> "" Then
                                If i = 0 Then
                                    FactId = dsFact.Tables(0).Rows(i).Item("ID").ToString()
                                Else
                                    FactId = FactId + "," + dsFact.Tables(0).Rows(i).Item("ID").ToString()
                                End If
                            End If
                        Next
                    End If
                    dsProd = objGetData.GetPivotProdComponent(FactId, "")

                    For i = 0 To dsProd.Tables(0).Rows.Count - 1
                        If dsProd.Tables(0).Rows(i).Item("COMPONENTID").ToString() <> "" Then
                            If i = 0 Then
                                CompId = dsProd.Tables(0).Rows(i).Item("COMPONENTID").ToString()
                            Else
                                CompId = CompId + "," + dsProd.Tables(0).Rows(i).Item("COMPONENTID").ToString()
                            End If
                        End If
                    Next
                End If

                If ProdId <> "" And MatId <> "" Then

                    If ProdId = "0" Then
                        ProdId = FactId
                    End If

                    dsProd = objGetData.GetSubscProdMatPackages(ProdId, MatId)

                    For i = 0 To dsProd.Tables(0).Rows.Count - 1
                        If dsProd.Tables(0).Rows(i).Item("ID").ToString() <> "" Then
                            If i = 0 Then
                                MatId = dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            Else
                                MatId = MatId + dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            End If

                        End If
                    Next
                    MatId = MatId.Remove(MatId.Length - 1)
                    dsMat = objGetData.GetSubscPackages(MatId)

                ElseIf ProdId <> "" And CompId <> "" Then

                    If ProdId = "0" Then
                        ProdId = FactId
                    End If

                    dsProd = objGetData.GetSubscProdCompPackages(ProdId, CompId)

                    For i = 0 To dsProd.Tables(0).Rows.Count - 1
                        If dsProd.Tables(0).Rows(i).Item("ID").ToString() <> "" Then
                            If i = 0 Then
                                MatId = dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            Else
                                MatId = MatId + dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            End If

                        End If
                    Next
                    MatId = MatId.Remove(MatId.Length - 1)
                    dsMat = objGetData.GetSubscPackages(MatId)
                ElseIf ProdId <> "" Then

                    If ProdId = "0" Then
                        ProdId = FactId
                    End If

                    dsProd = objGetData.GetPackageTypeByFact(ProdId)

                    For i = 0 To dsProd.Tables(0).Rows.Count - 1
                        If dsProd.Tables(0).Rows(i).Item("ID").ToString() <> "" Then
                            If i = 0 Then
                                MatId = dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            Else
                                MatId = MatId + dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            End If
                        End If
                    Next
                    MatId = MatId.Remove(MatId.Length - 1)
                    dsMat = objGetData.GetSubscPackages(MatId)
                ElseIf MatId <> "" Then
                    dsProd = objGetData.GetSubscMatPackages(MatId)
                    FactId = String.Empty
                    For i = 0 To dsProd.Tables(0).Rows.Count - 1
                        If dsProd.Tables(0).Rows(i).Item("ID").ToString() <> "" Then
                            If i = 0 Then
                                FactId = dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            Else
                                FactId = FactId + dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            End If
                        End If
                    Next
                    FactId = FactId.Remove(FactId.Length - 1)
                    dsMat = objGetData.GetSubscPackages(FactId)
                ElseIf CompId <> "" Then
                    dsProd = objGetData.GetSubscProdCompPackages("", CompId)
                    FactId = String.Empty
                    For i = 0 To dsProd.Tables(0).Rows.Count - 1
                        If dsProd.Tables(0).Rows(i).Item("ID").ToString() <> "" Then
                            If i = 0 Then
                                FactId = dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            Else
                                FactId = FactId + dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            End If
                        End If
                    Next
                    FactId = FactId.Remove(FactId.Length - 1)
                    dsMat = objGetData.GetSubscPackages(FactId)
                Else
                    FactId = String.Empty
                    arrFact = Regex.Split(Session("M1SubGroupId"), ",")
                    For i = 0 To arrFact.Length - 1
                        Dts = objGetData.GetSubGroupDetails(arrFact(i))

                        If Dts.Tables(0).Rows(0).Item("CATID").ToString() <> "" Then
                            If i = 0 Then
                                FactId = Dts.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            Else
                                FactId = FactId + Dts.Tables(0).Rows(0).Item("CATID").ToString() + ","
                            End If

                        End If
                    Next
                    FactId = FactId.Remove(FactId.Length - 1)

                    Dts = objGetData.GetSubFactGroupDetails(FactId)
                    If Dts.Tables(0).Rows.Count > 0 Then
                        For i = 0 To Dts.Tables(0).Rows.Count - 1
                            If i = 0 Then
                                StrSql = Dts.Tables(0).Rows(i).Item("ID").ToString() + ","
                            Else
                                StrSql = StrSql + Dts.Tables(0).Rows(i).Item("ID").ToString() + ","
                            End If
                        Next
                        StrSql = StrSql.Remove(StrSql.Length - 1)
                        dsMat = objGetData.GetPackageTypeByFact(StrSql)
                    End If
                End If
            End If

            ddlFilterValue.Items.Clear()
            Dim lst As New ListItem
            Dim lst1 As New ListItem
            lst.Text = "Nothing Selected"
            lst1.Text = "All Packages"
            lst.Value = -1
            lst1.Value = 0
            ddlFilterValue.Items.Add(lst)
            If Session("RPTTYPE") <> "MIXED" Then
                ddlFilterValue.Items.Add(lst1)
            End If
            ddlFilterValue.AppendDataBoundItems = True
            With ddlFilterValue
                .DataSource = dsMat
                .DataTextField = "VALUE"
                .DataValueField = "ID"
                .DataBind()
            End With

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetFilterValue" + ex.Message.ToString()
        End Try
    End Sub

    Private Sub GetProductValue(ByVal RptID As Integer)
        Dim ds As New DataSet()
        Dim objGetData As New Selectdata()
        Dim dsMat As New DataSet
        Dim FactId As String = String.Empty
        Dim PackId As String = String.Empty
        Dim MatId As String = String.Empty
        Dim CompId As String = String.Empty
        Dim i As Integer = 0
        Dim dsProd As New DataSet

        Try
            ddlFilterValue.Visible = True
            lnkProductTree.Visible = False
            rwRegionSet.Visible = False

            If Request.QueryString("isTemp").ToString() = "Y" Then
                ds = objGetData.GetUserReportProdFilter(RptID)
            Else
                ds = objGetData.GetUserReportProdFilterR(RptID)
            End If

            If ds.Tables(0).Rows.Count > 0 Then

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If ds.Tables(0).Rows(i).Item("FILTERTYPE").ToString() = "Package" Then
                        If PackId = String.Empty Then
                            PackId = ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        Else
                            If PackId <> "0" Then
                                PackId = PackId + "," + ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                            End If
                        End If

                    ElseIf ds.Tables(0).Rows(i).Item("FILTERTYPE").ToString() = "Material" Then
                        If MatId = String.Empty Then
                            MatId = ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        Else
                            MatId = MatId + "," + ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        End If

                    ElseIf ds.Tables(0).Rows(i).Item("FILTERTYPE").ToString() = "Component" Then
                        If CompId = String.Empty Then
                            CompId = ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        Else
                            CompId = CompId + "," + ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        End If

                    End If
                Next

                PackId = ""
                MatId = ""

                If PackId <> "" And MatId <> "" Then
                    dsProd = objGetData.GetSubscPackMatProducts(PackId, MatId)

                    For i = 0 To dsProd.Tables(0).Rows.Count - 1
                        If dsProd.Tables(0).Rows(i).Item("ID").ToString() <> "" Then
                            If i = 0 Then
                                FactId = dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            Else
                                FactId = FactId + dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            End If

                        End If
                    Next
                    FactId = FactId.Remove(FactId.Length - 1)
                    dsMat = objGetData.GetSubscProductsName(FactId)
                ElseIf PackId <> "" And CompId <> "" Then
                    dsProd = objGetData.GetSubscPackCompProducts(PackId, CompId)

                    For i = 0 To dsProd.Tables(0).Rows.Count - 1
                        If dsProd.Tables(0).Rows(i).Item("ID").ToString() <> "" Then
                            If i = 0 Then
                                FactId = dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            Else
                                FactId = FactId + dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            End If

                        End If
                    Next
                    FactId = FactId.Remove(FactId.Length - 1)
                    dsMat = objGetData.GetSubscProductsName(FactId)
                ElseIf PackId <> "" Then
                    dsProd = objGetData.GetSubscPackMatProducts(PackId, "")

                    For i = 0 To dsProd.Tables(0).Rows.Count - 1
                        If dsProd.Tables(0).Rows(i).Item("ID").ToString() <> "" Then
                            If i = 0 Then
                                FactId = dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            Else
                                FactId = FactId + dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            End If
                        End If
                    Next
                    FactId = FactId.Remove(FactId.Length - 1)
                    dsMat = objGetData.GetSubscProductsName(FactId)
                ElseIf MatId <> "" Then
                    dsProd = objGetData.GetSubscPackMatProducts("", MatId)

                    For i = 0 To dsProd.Tables(0).Rows.Count - 1
                        If dsProd.Tables(0).Rows(i).Item("ID").ToString() <> "" Then
                            If i = 0 Then
                                FactId = dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            Else
                                FactId = FactId + dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            End If
                        End If
                    Next
                    FactId = FactId.Remove(FactId.Length - 1)
                    dsMat = objGetData.GetSubscProductsName(FactId)
                ElseIf CompId <> "" Then
                    dsProd = objGetData.GetSubscPackCompProducts("", CompId)

                    For i = 0 To dsProd.Tables(0).Rows.Count - 1
                        If dsProd.Tables(0).Rows(i).Item("ID").ToString() <> "" Then
                            If i = 0 Then
                                FactId = dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            Else
                                FactId = FactId + dsProd.Tables(0).Rows(i).Item("ID").ToString() + ","
                            End If
                        End If
                    Next
                    FactId = FactId.Remove(FactId.Length - 1)
                    dsMat = objGetData.GetSubscProductsName(FactId)
                Else
                    dsMat = objGetData.GetFilterSQL(1, Session("M1SubGroupId").ToString())
                End If
            End If

            ddlFilterValue.Items.Clear()
            Dim lst As New ListItem
            Dim lst1 As New ListItem
            lst.Text = "Nothing Selected"
            lst1.Text = "All Products"
            lst.Value = -1
            lst1.Value = 0
            ddlFilterValue.Items.Add(lst)
            ddlFilterValue.Items.Add(lst1)
            ddlFilterValue.AppendDataBoundItems = True
            With ddlFilterValue
                .DataSource = dsMat
                .DataTextField = "VALUE"
                .DataValueField = "ID"
                .DataBind()
            End With

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetFilterValue" + ex.Message.ToString()
        End Try
    End Sub

    Private Sub GetFilterValue(ByVal filterTypeId As String)
        Dim objGetDate As New Selectdata()
        Dim ds As New DataSet()
        Dim objGetData As New Selectdata()
        Dim Dts As DataSet
        Dim dsMat As DataSet
        Dim FactId As String = String.Empty
        Dim PackId As String = String.Empty
        Dim MatId As String = String.Empty
        Dim MatGrpId As String = String.Empty
        Dim GroupId As String = String.Empty
        Dim StrSql As String = String.Empty
        Dim arrFact() As String
        Dim j As Integer = 0
        Try
            If filterTypeId = 6 Then 'For Region
                dsMat = objGetDate.GetFilterCountriesByRegion(Session("M1SubRegionId").ToString())

            ElseIf filterTypeId = 3 Then 'For Package

                arrFact = Regex.Split(Session("M1SubGroupId"), ",")
                For i = 0 To arrFact.Length - 1
                    Dts = objGetData.GetSubGroupDetails(arrFact(i))

                    If Dts.Tables(0).Rows(0).Item("CATID").ToString() <> "" Then
                        If i = 0 Then
                            FactId = Dts.Tables(0).Rows(0).Item("CATID").ToString() + ","
                        Else
                            FactId = FactId + Dts.Tables(0).Rows(0).Item("CATID").ToString() + ","
                        End If

                    End If
                Next
                FactId = FactId.Remove(FactId.Length - 1)

                Dts = objGetData.GetSubFactGroupDetails(FactId)
                If Dts.Tables(0).Rows.Count > 0 Then
                    For i = 0 To Dts.Tables(0).Rows.Count - 1
                        If i = 0 Then
                            StrSql = Dts.Tables(0).Rows(i).Item("ID").ToString() + ","
                        Else
                            StrSql = StrSql + Dts.Tables(0).Rows(i).Item("ID").ToString() + ","
                        End If
                    Next
                    StrSql = StrSql.Remove(StrSql.Length - 1)
                    dsMat = objGetData.GetPackageTypeByFact(StrSql)
                End If

            Else
                dsMat = objGetDate.GetFilterSQL(filterTypeId, Session("M1SubGroupId").ToString())
            End If

            ddlFilterValue.Items.Clear()
            If dsMat.Tables(0).Rows.Count > 0 Then
                With ddlFilterValue
                    .DataSource = dsMat
                    .DataTextField = "VALUE"
                    .DataValueField = "ID"
                    .DataBind()
                End With
            Else
                Dim lst As New ListItem
                lst.Text = "Nothing Selected"
                lst.Value = -1
                ddlFilterValue.Items.Add(lst)
                ddlFilterValue.AppendDataBoundItems = True
            End If



        Catch ex As Exception
            _lErrorLble.Text = "Error:GetFilterValue" + ex.Message.ToString()
        End Try
    End Sub

    Private Sub GetFilterValueWithClause(ByVal filterTypeId As String)
        Dim objGetDate As New Selectdata()
        Dim ds As New DataSet()
        Dim Clause As String = String.Empty
        Try

            Clause = " AND CLAUSE1=" + ddlRegionSet.SelectedValue '+ " AND ID IN (" + Session("M1SubRegionId") + ")"
            ds = objGetDate.GetFilterSQLWithClause(filterTypeId, Clause)

            ddlFilterValue.Items.Clear()
            Dim lst As New ListItem
            lst.Text = "Nothing Selected"
            lst.Value = -1
            ddlFilterValue.Items.Add(lst)
            ddlFilterValue.AppendDataBoundItems = True
            With ddlFilterValue
                .DataSource = ds
                .DataTextField = "VALUE"
                .DataValueField = "ID"
                .DataBind()
            End With

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetFilterValueWithClause" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetRegionSet()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet()
        Dim dsSub As DataSet
        Dim RegionSetId As String = String.Empty
        Try


            'dsSub = objGetData.GetSubRegionDetails(Session("M1SubRegionId").ToString())
            'If dsSub.Tables(0).Rows.Count > 0 Then
            '    For i = 0 To dsSub.Tables(0).Rows.Count - 1
            '        If i = 0 Then
            '            RegionId = dsSub.Tables(0).Rows(i).Item("REGIONSETID").ToString() + ","
            '        Else
            '            RegionId = RegionId + dsSub.Tables(0).Rows(i).Item("REGIONSETID").ToString() + ","
            '        End If
            '    Next
            'End If
            'RegionId = RegionId.Remove(RegionId.Length - 1)
            ds = objGetData.GetUserRegionSets(Session("M1SubGroupId"))
            With ddlRegionSet
                .DataSource = ds
                .DataTextField = "REGIONSETNAME"
                .DataValueField = "REGIONSETID"
                .DataBind()
            End With

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetRegionSet" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlFilterType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFilterType.SelectedIndexChanged
        Dim objGetData As New M1SubGetData.Selectdata()
        Dim ds As DataSet
        Dim dsPackage As DataSet
        Dim RptID As String = Request.QueryString("RptID").ToString()
        Dim PackValue As String = String.Empty
        Dim FiltValue As String = String.Empty
        Dim MatId As String = String.Empty
        Dim FactId As String = String.Empty
        Dim StrSql As String = String.Empty
        Dim ProdId As String = String.Empty
        Dim j As Integer
        Try
            hidCatID.Value = "0"

            If ddlFilterType.SelectedItem.Text = "Region" Then  'For Region
                GetRegionSet()
                lblRegionSet.Text = "<b> Select Regionset <b>"
                rwRegionSet.Visible = True
                ddlFilterValue.Visible = True
                lnkProductTree.Visible = False
                GetFilterValueWithClause(ddlFilterType.SelectedValue)

            ElseIf ddlFilterType.SelectedItem.Text = "Category" Or ddlFilterType.SelectedItem.Text = "Group" Then 'for Category and Group
                rwRegionSet.Visible = False
                ddlFilterValue.Visible = False
                lnkProductTree.Visible = True
                lnkProductTree.Text = " Select " + ddlFilterType.SelectedItem.Text

            ElseIf ddlFilterType.SelectedItem.Text = "Package" Then  'For Package
                GetPackageValue(RptID)
                'ddlFilterValue.Visible = True
                'lnkProductTree.Visible = False
                'rwRegionSet.Visible = False
                'ds = objGetData.GetUserReportFilter(RptID)

                'If ds.Tables(0).Rows.Count > 0 Then

                '    If ds.Tables(0).Rows(0).Item("FILTERTYPE").ToString() = "Product" Then
                '        PackValue = ds.Tables(0).Rows(0).Item("FILTERVALUEID")
                '        dsPackage = objGetData.GetPackageTypeByFact(PackValue)

                '        ddlFilterValue.Items.Clear()

                '        If dsPackage.Tables(0).Rows.Count > 0 Then
                '            ddlFilterValue.AppendDataBoundItems = True
                '            With ddlFilterValue
                '                .DataSource = dsPackage
                '                .DataTextField = "VALUE"
                '                .DataValueField = "ID"
                '                .DataBind()
                '            End With
                '        Else
                '            Dim lst As New ListItem
                '            lst.Text = "Nothing Selected"
                '            lst.Value = -1
                '            ddlFilterValue.Items.Add(lst)
                '        End If

                '    Else
                '        lnkProductTree.Visible = False
                '        rwRegionSet.Visible = False
                '        GetFilterValue(ddlFilterType.SelectedValue)
                '    End If

                'Else
                '    lnkProductTree.Visible = False
                '    rwRegionSet.Visible = False
                '    GetFilterValue(ddlFilterType.SelectedValue)
                'End If

            ElseIf ddlFilterType.SelectedItem.Text = "Product" Then ' For Product
                GetProductValue(RptID)

            ElseIf ddlFilterType.SelectedItem.Text = "Material" Then ' For Material
                GetMaterialValue(RptID)
                'ElseIf ddlFilterType.SelectedItem.Text = "MaterialGrp" Then ' For Material Group
                '    GetMaterialGroupValue(RptID)
            ElseIf ddlFilterType.SelectedItem.Text = "Component" Then
                GetComponentValue(RptID)
            Else
                ddlFilterValue.Visible = True
                lnkProductTree.Visible = False
                rwRegionSet.Visible = False
                GetFilterValue(ddlFilterType.SelectedValue)
            End If

            lblFilterValue.Text = "<b>" + ddlFilterType.SelectedItem.Text + " :<b>"
            rwFilterValue.Visible = True
        Catch ex As Exception
            _lErrorLble.Text = "Error:ddlFilterType_SelectedIndexChanged:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlRegionSet_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegionSet.SelectedIndexChanged
        Try

            GetFilterValueWithClause(ddlFilterType.SelectedValue)

        Catch ex As Exception
            _lErrorLble.Text = "Error:ddlRegionSet_SelectedIndexChanged" + ex.Message.ToString()
        End Try

    End Sub

    Public Function GetReportName(ByVal rptId As String) As DataSet
        Dim Dts As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim name As String = String.Empty
        Dim Market1Connection As String = System.Configuration.ConfigurationManager.AppSettings("Market1ConnectionString")
        Try
            StrSql = "SELECT  REPORTNAME,RPTTYPE FROM USERREPORTS WHERE USERREPORTID=" + rptId
            StrSql = StrSql + " UNION SELECT REPORTNAME,RPTTYPE FROM BASEREPORTS WHERE BASEREPORTID=" + rptId

            Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
            If Dts.Tables(0).Rows.Count > 0 Then
                name = Dts.Tables(0).Rows(0)("REPORTNAME").ToString()
            End If
            Return Dts
        Catch ex As Exception
            Throw New Exception("M1SubGetData:GetReportName:" + ex.Message.ToString())
            Return Dts
        End Try
    End Function

    Public Function GetReportNameTemp(ByVal rptId As String) As DataSet
        Dim Dts As New DataSet()
        Dim odbUtil As New DBUtil()
        Dim StrSql As String = String.Empty
        Dim name As String = String.Empty
        Dim Market1Connection As String = System.Configuration.ConfigurationManager.AppSettings("Market1ConnectionString")
        Try
            StrSql = "SELECT  REPORTNAME,RPTTYPE,RPTTYPEDES FROM USERREPORTSTEMP WHERE USERREPORTID=" + rptId

            Dts = odbUtil.FillDataSet(StrSql, Market1Connection)
            If Dts.Tables(0).Rows.Count > 0 Then
                name = Dts.Tables(0).Rows(0)("REPORTNAME").ToString()
            End If
            Return Dts
        Catch ex As Exception
            Throw New Exception("M1SubGetData:GetReportName:" + ex.Message.ToString())
            Return Dts
        End Try
    End Function

End Class
