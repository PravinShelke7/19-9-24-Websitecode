Imports System.Data
Imports System.Data.OleDb
Imports System
Imports M1SubGetData
Imports M1SubUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Market1Sub_PopUp_FilterSelectorRep
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
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
        hidFilterDes.Value = Request.QueryString("Id").ToString()
        hidFilterId.Value = Request.QueryString("hidId").ToString()
        Dim colValueId As String = String.Empty
        Dim dsRptCols As New DataSet
        Dim objGetData As New Selectdata
        Dim pSubTypeid As String = "null"
        Dim pSubTypeValue As String = String.Empty
        Dim pSelId As String = "null"
        Dim pSelValue As String = ""
        Dim seq As String = "null"
        Dim dr() As DataRow
        Dim dsRep As New DataSet
        Dim rptName As String = ""
        Dim rptRepType As String = ""
        Dim Type As String = ""
        Try
            If hidCatID.Value <> "0" Then
                filterValue = hidCatDes1.Value
                filterValueID = hidCatID.Value
            Else
                filterValue = ddlFilterValue.SelectedItem.ToString()
                filterValueID = ddlFilterValue.SelectedValue.ToString()
            End If

          
            dsRptCols = objGetData.GetUsersReportFilters(RptID)
            dsRep = objGetData.GetReportDetails(RptID)
            If dsRep.Tables(0).Rows.Count > 0 Then
                rptName = dsRep.Tables(0).Rows(0).Item("ReportName").ToString()
                rptRepType = dsRep.Tables(0).Rows(0).Item("RPTTYPEDES").ToString()
                Type = dsRep.Tables(0).Rows(0).Item("RPTTYPE").ToString()
            End If

            If hidValue <> 0 Then
                objUpIns.UpdateFilterDetails(ddlFilterType.SelectedItem.Text, filterValue, ddlFilterType.SelectedValue.ToString(), filterValueID, hidValue.ToString())
                If Type = "MIXED" Then
                    If ddlFilterType.SelectedItem.Text = "Region" Then
                        objUpIns.EditReportTypeDes(RptID, "REGION")
                    ElseIf ddlFilterType.SelectedItem.Text = "Country" Then
                        ' objUpIns.EditReportTypeDes(RptID, "CNTRY")
                        If dsRep.Tables(0).Rows(0).Item("RPTTYPEDES").ToString() = "MAT" Or dsRep.Tables(0).Rows(0).Item("RPTTYPEDES").ToString() = "PROD" Or dsRep.Tables(0).Rows(0).Item("RPTTYPEDES").ToString() = "PACKGRP" Or dsRep.Tables(0).Rows(0).Item("RPTTYPEDES").ToString() = "PACK" Then
                        Else
                            objUpIns.EditReportTypeDes(RptID, "CNTRY")
                        End If
                    End If
                End If

            End If
            If rptRepType = "GROUP" Then
                Dim Regionset = dsRep.Tables(0).Rows(0).Item("REGIONSETID").ToString()
                UpdateReportRows(rptRepType, Regionset)
            Else
                UpdateReportRows(rptRepType, "")
            End If


            If ddlFilterType.SelectedItem.Text = "Region" Then
                objUpIns.InsertLog(Session("UserId"), Session("MLogInLog"), "10", RptID.ToString(), "", "", "8", filterValueID, Session.SessionID)
            ElseIf ddlFilterType.SelectedItem.Text = "Country" Then
                objUpIns.InsertLog(Session("UserId"), Session("MLogInLog"), "10", RptID.ToString(), "", "", "9", filterValueID, Session.SessionID)
            ElseIf ddlFilterType.SelectedItem.Text = "Product" Then
                objUpIns.InsertLog(Session("UserId"), Session("MLogInLog"), "10", RptID.ToString(), "", "", "6", filterValueID, Session.SessionID)
            ElseIf ddlFilterType.SelectedItem.Text = "Package" Then
                objUpIns.InsertLog(Session("UserId"), Session("MLogInLog"), "10", RptID.ToString(), "", "", "7", filterValueID, Session.SessionID)
            ElseIf ddlFilterType.SelectedItem.Text = "Group" Then
                objUpIns.InsertLog(Session("UserId"), Session("MLogInLog"), "10", RptID.ToString(), "", "", "11", filterValueID, Session.SessionID)
            ElseIf ddlFilterType.SelectedItem.Text = "Material" Then
                objUpIns.InsertLog(Session("UserId"), Session("MLogInLog"), "10", RptID.ToString(), "", "", "10", filterValueID, Session.SessionID)
            ElseIf ddlFilterType.SelectedItem.Text = "Component" Then
                objUpIns.InsertLog(Session("UserId"), Session("MLogInLog"), "10", RptID.ToString(), "", "", "12", filterValueID, Session.SessionID)
            End If

            ClientScript.RegisterStartupScript(Me.GetType(), "FilterSel", "FilterSelection('" + filterValue.ToString() + "','" + hidValue.ToString() + "','" + hidId + "');", True)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub UpdateReportRows(ByVal rptRepType As String, ByVal regionset As String)
        Try
            Dim RptID As String = Request.QueryString("RptID").ToString()
            Dim objUpIns As New UpdateInsert()
            Dim objGetData As New Selectdata()
            Dim RowCnt As String = String.Empty
            Dim FactId As String = String.Empty
            Dim dsRegions As New DataSet
            Dim dsPackTypeGroup As New DataSet
            Dim dsMaterials As New DataSet
            Dim dsPackType As New DataSet
            Dim dsProdGroup As New DataSet
            Dim dsProducts As New DataSet
            Dim dsFilters As New DataSet
            Dim dsRowSelector As New DataSet
            Dim dsRows As New DataSet
            Dim dsFact As New DataSet
            Dim arrFact() As String
            Dim listFilterType As New ArrayList
            Dim listFilterValue As New ArrayList
            Dim listFilterType1 As New ArrayList
            Dim listFilterValue1 As New ArrayList
            Dim RowVal1 As String = String.Empty
            Dim RowVal2 As String = String.Empty
            Dim packId As String = String.Empty
            Dim matId As String = String.Empty
            Dim i As Integer
            Dim RegionId As String = String.Empty
            Dim UnitID As String = ""

            'Change started for Edit
            If rptRepType = "PACKGRP" Then
                'RowCnt = objGetData.GetReportPackCount(ddlPackageGrp.SelectedValue.ToString())
                'dsPackTypeGroup = objGetData.GetReportPackagesByGroup(ddlPackageGrp.SelectedValue.ToString())
                'dsRowSelector = objGetData.GetRowsSelectorByCode("PACK")
                'RegionId = "null"
            ElseIf rptRepType = "PROD" Then
                dsFilters = objGetData.GetReportFiltersByRepId(RptID)
                For i = 0 To dsFilters.Tables(0).Rows.Count - 1
                    If dsFilters.Tables(0).Rows(i).Item("FILTERVALUEID").ToString() <> "" Then
                        listFilterType.Add(dsFilters.Tables(0).Rows(i).Item("FILTERTYPE").ToString())
                        listFilterValue.Add(dsFilters.Tables(0).Rows(i).Item("FILTERVALUEID").ToString())
                        If i = dsFilters.Tables(0).Rows.Count - 1 Then
                            dsProducts = objGetData.GetReportProductByPackMat(listFilterType, listFilterValue, Session("M1SubGroupId"))
                        End If
                    End If
                Next
                RegionId = "null"
                If dsProducts.Tables(0).Rows.Count <> 0 Then
                    RowCnt = dsProducts.Tables(0).Rows.Count
                    dsRowSelector = objGetData.GetRowsSelectorByCode("PROD")
                    If RptID <> "0" Then
                        UnitID = objUpIns.EditUSERReportRowDetail(RptID, RowCnt)
                    End If
                    dsRows = objGetData.GetUsersReportRowsRep(RptID)
                    For i = 0 To RowCnt - 1
                        objUpIns.UpdateRowDetailsRep(dsRowSelector.Tables(0).Rows(0)("ROWDES").ToString(), dsProducts.Tables(0).Rows(i)("NAME").ToString().Replace("'", "''"), dsRows.Tables(0).Rows(i)("USERREPORTROWID").ToString, dsRowSelector.Tables(0).Rows(0)("ROWDES").ToString(), "0", RowVal1, RowVal2, dsRowSelector.Tables(0).Rows(0)("ROWTYPEID").ToString(), dsProducts.Tables(0).Rows(i)("ID").ToString(), UnitID)
                    Next
                Else
                    RowCnt = "1"
                    dsRowSelector = objGetData.GetRowsSelectorByCode("PROD")
                    dsProducts = objGetData.GetReportDummy("PROD")
                End If

            ElseIf rptRepType = "PACK" Then
                dsFilters = objGetData.GetReportFiltersByRepId(RptID)
                For i = 0 To dsFilters.Tables(0).Rows.Count - 1
                    If dsFilters.Tables(0).Rows(i).Item("FILTERVALUEID").ToString() <> "" Then
                        If dsFilters.Tables(0).Rows(i).Item("FILTERVALUE").ToString() = "All Products" Then
                            arrFact = Regex.Split(Session("M1SubGroupId"), ",")
                            For j = 0 To arrFact.Length - 1
                                dsFact = objGetData.GetSubGroupDetails(arrFact(j))

                                If dsFact.Tables(0).Rows(0).Item("CATID").ToString() <> "" Then
                                    If j = 0 Then
                                        FactId = dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                                    Else
                                        FactId = FactId + dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                                    End If

                                End If
                            Next
                            FactId = FactId.Remove(FactId.Length - 1)

                            dsFact = objGetData.GetSubFactGroupDetails(FactId)
                            FactId = String.Empty
                            For k = 0 To dsFact.Tables(0).Rows.Count - 1
                                If dsFact.Tables(0).Rows(0).Item("ID").ToString() <> "" Then
                                    If k = 0 Then
                                        FactId = dsFact.Tables(0).Rows(k).Item("ID").ToString()
                                    Else
                                        FactId = FactId + "," + dsFact.Tables(0).Rows(k).Item("ID").ToString()
                                    End If
                                End If
                            Next

                        End If
                        listFilterType1.Add(dsFilters.Tables(0).Rows(i).Item("FILTERTYPE").ToString())
                        listFilterValue1.Add(dsFilters.Tables(0).Rows(i).Item("FILTERVALUEID").ToString())
                        If i = dsFilters.Tables(0).Rows.Count - 1 Then
                            dsPackType = objGetData.GetReportPackageByProdMat(listFilterType1, listFilterValue1, FactId)
                        End If
                    End If
                Next
                RegionId = "null"
                If dsPackType.Tables(0).Rows.Count <> 0 Then
                    RowCnt = dsPackType.Tables(0).Rows.Count
                    dsRowSelector = objGetData.GetRowsSelectorByCode("PACK")
                    If RptID <> "0" Then
                        UnitID = objUpIns.EditUSERReportRowDetail(RptID, RowCnt)
                    End If
                    dsRows = objGetData.GetUsersReportRowsRep(RptID)
                    For i = 0 To RowCnt - 1
                        objUpIns.UpdateRowDetailsRep(dsRowSelector.Tables(0).Rows(0)("ROWDES").ToString(), dsPackType.Tables(0).Rows(i)("NAME").ToString().Replace("'", "''"), dsRows.Tables(0).Rows(i)("USERREPORTROWID").ToString, dsRowSelector.Tables(0).Rows(0)("ROWDES").ToString(), "0", RowVal1, RowVal2, dsRowSelector.Tables(0).Rows(0)("ROWTYPEID").ToString(), dsPackType.Tables(0).Rows(i)("ID").ToString(), UnitID)
                    Next
                Else
                    RowCnt = "1"
                    dsRowSelector = objGetData.GetRowsSelectorByCode("PACK")
                    dsPackType = objGetData.GetReportDummy("PACK")
                End If


            ElseIf rptRepType = "MAT" Then
                dsFilters = objGetData.GetReportFiltersByRepId(RptID)
                For i = 0 To dsFilters.Tables(0).Rows.Count - 1
                    If dsFilters.Tables(0).Rows(i).Item("FILTERVALUEID").ToString() <> "" Then
                        If dsFilters.Tables(0).Rows(i).Item("FILTERVALUE").ToString() = "All Products" Then
                            arrFact = Regex.Split(Session("M1SubGroupId"), ",")
                            For j = 0 To arrFact.Length - 1
                                dsFact = objGetData.GetSubGroupDetails(arrFact(j))

                                If dsFact.Tables(0).Rows(0).Item("CATID").ToString() <> "" Then
                                    If j = 0 Then
                                        FactId = dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                                    Else
                                        FactId = FactId + dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                                    End If

                                End If
                            Next
                            FactId = FactId.Remove(FactId.Length - 1)

                            dsFact = objGetData.GetSubFactGroupDetails(FactId)
                            FactId = String.Empty
                            For k = 0 To dsFact.Tables(0).Rows.Count - 1
                                If dsFact.Tables(0).Rows(0).Item("ID").ToString() <> "" Then
                                    If k = 0 Then
                                        FactId = dsFact.Tables(0).Rows(k).Item("ID").ToString()
                                    Else
                                        FactId = FactId + "," + dsFact.Tables(0).Rows(k).Item("ID").ToString()
                                    End If
                                End If
                            Next
                            FactId = FactId.Remove(FactId.Length - 1)
                        End If
                        listFilterType1.Add(dsFilters.Tables(0).Rows(i).Item("FILTERTYPE").ToString())
                        listFilterValue1.Add(dsFilters.Tables(0).Rows(i).Item("FILTERVALUEID").ToString())
                        If i = dsFilters.Tables(0).Rows.Count - 1 Then
                            dsMaterials = objGetData.GetReportMaterialByProdPack(listFilterType1, listFilterValue1, FactId)
                        End If
                    End If
                Next
                RegionId = "null"
                If dsMaterials.Tables(0).Rows.Count <> 0 Then
                    RowCnt = dsMaterials.Tables(0).Rows.Count
                    dsRowSelector = objGetData.GetRowsSelectorByCode("MAT")
                    If RptID <> "0" Then
                        UnitID = objUpIns.EditUSERReportRowDetail(RptID, RowCnt)
                    End If
                    dsRows = objGetData.GetUsersReportRowsRep(RptID)
                    For i = 0 To RowCnt - 1
                        objUpIns.UpdateRowDetailsRep(dsRowSelector.Tables(0).Rows(0)("ROWDES").ToString(), dsMaterials.Tables(0).Rows(i)("NAME").ToString().Replace("'", "''"), dsRows.Tables(0).Rows(i)("USERREPORTROWID").ToString, dsRowSelector.Tables(0).Rows(0)("ROWDES").ToString(), "0", RowVal1, RowVal2, dsRowSelector.Tables(0).Rows(0)("ROWTYPEID").ToString(), dsMaterials.Tables(0).Rows(i)("ID").ToString(), UnitID)
                    Next
                Else
                    RowCnt = "1"
                    dsRowSelector = objGetData.GetRowsSelectorByCode("MAT")
                    dsMaterials = objGetData.GetReportDummy("MAT")
                End If

            ElseIf rptRepType = "GROUP" Then
                dsFilters = objGetData.GetReportFiltersByRepId(RptID)
                For i = 0 To dsFilters.Tables(0).Rows.Count - 1
                    If dsFilters.Tables(0).Rows(i).Item("FILTERVALUEID").ToString() <> "" Then
                        arrFact = Regex.Split(Session("M1SubGroupId"), ",")
                        For j = 0 To arrFact.Length - 1
                            dsFact = objGetData.GetSubGroupDetails(arrFact(j))

                            If dsFact.Tables(0).Rows(0).Item("CATID").ToString() <> "" Then
                                If j = 0 Then
                                    FactId = dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                                Else
                                    FactId = FactId + dsFact.Tables(0).Rows(0).Item("CATID").ToString() + ","
                                End If

                            End If
                        Next
                        FactId = FactId.Remove(FactId.Length - 1)

                        dsFact = objGetData.GetSubFactGroupDetails(FactId)
                        FactId = String.Empty
                        For k = 0 To dsFact.Tables(0).Rows.Count - 1
                            If dsFact.Tables(0).Rows(0).Item("ID").ToString() <> "" Then
                                If k = 0 Then
                                    FactId = dsFact.Tables(0).Rows(k).Item("ID").ToString()
                                Else
                                    FactId = FactId + "," + dsFact.Tables(0).Rows(k).Item("ID").ToString()
                                End If
                            End If
                        Next
                        listFilterType.Add(dsFilters.Tables(0).Rows(i).Item("FILTERTYPE").ToString())
                        listFilterValue.Add(dsFilters.Tables(0).Rows(i).Item("FILTERVALUEID").ToString())
                        If i = dsFilters.Tables(0).Rows.Count - 1 Then
                            dsProdGroup = objGetData.GetReportGroupByProdCntry(listFilterType, listFilterValue, regionset, FactId)
                        End If
                    End If
                Next
                If dsProdGroup.Tables(0).Rows.Count <> 0 Then
                    RowCnt = dsProdGroup.Tables(0).Rows.Count
                    dsRowSelector = objGetData.GetRowsSelectorByCode("GRP")
                    If RptID <> "0" Then
                        UnitID = objUpIns.EditUSERReportRowDetail(RptID, RowCnt)
                    End If
                    dsRows = objGetData.GetUsersReportRowsRep(RptID)
                    For i = 0 To RowCnt - 1
                        objUpIns.UpdateRowDetailsRep(dsRowSelector.Tables(0).Rows(0)("ROWDES").ToString(), dsProdGroup.Tables(0).Rows(i)("NAME").ToString().Replace("'", "''"), dsRows.Tables(0).Rows(i)("USERREPORTROWID").ToString, dsRowSelector.Tables(0).Rows(0)("ROWDES").ToString(), "0", RowVal1, RowVal2, dsRowSelector.Tables(0).Rows(0)("ROWTYPEID").ToString(), dsProdGroup.Tables(0).Rows(i)("ID").ToString(), UnitID)
                    Next
                Else
                    RowCnt = "1"
                    dsRowSelector = objGetData.GetRowsSelectorByCode("GRP")
                    dsProdGroup = objGetData.GetReportDummy("GROUP")
					If RptID <> "0" Then
                        UnitID = objUpIns.EditUSERReportRowDetail(RptID, RowCnt)
                    End If
                    dsRows = objGetData.GetUsersReportRowsRep(RptID)
                    For i = 0 To RowCnt - 1
                        objUpIns.UpdateRowDetailsRep(dsRowSelector.Tables(0).Rows(0)("ROWDES").ToString(), dsProdGroup.Tables(0).Rows(i)("NAME").ToString().Replace("'", "''"), dsRows.Tables(0).Rows(i)("USERREPORTROWID").ToString, dsRowSelector.Tables(0).Rows(0)("ROWDES").ToString(), "0", RowVal1, RowVal2, dsRowSelector.Tables(0).Rows(0)("ROWTYPEID").ToString(), dsProdGroup.Tables(0).Rows(i)("ID").ToString(), UnitID)
                    Next
                End If
                RegionId = "null"

            End If
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
        Dim lst As New ListItem
        Dim RptType As String = String.Empty
        Dim dsRpt As New DataSet()
        Dim GrpName As String = ""
        Dim dv As New DataView
        Dim dt As New DataTable
        Dim dsRow As New DataSet
        Try
			 dsRpt = objGetDate.GetReportDetails(Request.QueryString("RptID").ToString())
            dsRow = objGetDate.GetUsersReportRows(Request.QueryString("RptID").ToString())
            'ds = objGetDate.GetFilterSelector(-1)
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
                    dv.RowFilter = "FILTERDES NOT IN ('Group')"
                ElseIf dsRpt.Tables(0).Rows(0).Item("RPTTYPEDES").ToString().ToUpper() = "MAT" Then
                    dv.RowFilter = "FILTERDES NOT IN ('Material')"
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
           ' ds = objGetDate.GetFilterSelector(-1)
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
                        If dsProd.Tables(0).Rows(i).Item("COMPONENTID").ToString() <> "" Then
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

    Private Sub GetMaterialGroupValue(ByVal RptID As Integer)
        Dim objGetDate As New Selectdata()
        Dim ds As New DataSet()
        Dim objGetData As New Selectdata()
        Dim Dts As DataSet
        Dim dsMat As DataSet
        Dim dsFilter As DataSet
        Dim FactId As String = String.Empty
        Dim PackId As String = String.Empty
        Dim MatId As String = String.Empty
        Dim MatGrpId As String = String.Empty
        Dim GroupId As String = String.Empty
        Dim StrSql As String = String.Empty
        Dim j As Integer = 0
        Dim dsProd As DataSet
        Dim dsProdMat As DataSet
        Dim PackValue As String = String.Empty
        Dim FiltValue As String = String.Empty
        Dim arrFact() As String
        Dim ProdId As String = String.Empty
        Try
            ddlFilterValue.Visible = True
            lnkProductTree.Visible = False
            rwRegionSet.Visible = False
            ds = objGetData.GetUserReportProdFilter(RptID)

            If ds.Tables(0).Rows.Count > 0 Then

                If ds.Tables(0).Rows(0).Item("FILTERTYPE").ToString() = "Product" Then ' For Productwise
                    dsFilter = objGetData.GetUserReportFilter2(RptID)

                    If dsFilter.Tables(0).Rows.Count > 0 Then
                        If dsFilter.Tables(0).Rows(0).Item("FILTERTYPE").ToString() = "Package" Then ' Material for ProductPackagewise
                            FiltValue = dsFilter.Tables(0).Rows(0).Item("FILTERVALUEID")
                            dsProd = objGetData.GetSubscPackProdMaterials(ds.Tables(0).Rows(0).Item("FILTERVALUEID"), dsFilter.Tables(0).Rows(0).Item("FILTERVALUEID"))

                            For i = 0 To dsProd.Tables(0).Rows.Count - 1
                                If dsProd.Tables(0).Rows(i).Item("GROUPID").ToString() <> "" Then
                                    If i = 0 Then
                                        MatId = dsProd.Tables(0).Rows(i).Item("ID").ToString()
                                    Else
                                        MatId = MatId + "," + dsProd.Tables(0).Rows(i).Item("ID").ToString()
                                    End If
                                End If
                            Next

                            dsMat = objGetData.GetSubscProdGroups(MatId)

                        Else
                            FiltValue = ds.Tables(0).Rows(0).Item("FILTERVALUEID")
                            dsProd = objGetData.GetSubscMaterials(FiltValue)

                            For i = 0 To dsProd.Tables(0).Rows.Count - 1
                                If dsProd.Tables(0).Rows(i).Item("GROUPID").ToString() <> "" Then
                                    If i = 0 Then
                                        MatId = dsProd.Tables(0).Rows(i).Item("ID").ToString()
                                    Else
                                        MatId = MatId + "," + dsProd.Tables(0).Rows(i).Item("ID").ToString()
                                    End If
                                End If
                            Next

                            dsMat = objGetData.GetSubscProdGroups(MatId)
                        End If

                    End If

                ElseIf ds.Tables(0).Rows(0).Item("FILTERTYPE").ToString() = "Package" Then ' for Packagewise
                    FiltValue = ds.Tables(0).Rows(0).Item("FILTERVALUEID")
                    dsProd = objGetData.GetSubscPackMaterials(FiltValue)

                    For i = 0 To dsProd.Tables(0).Rows.Count - 1
                        If dsProd.Tables(0).Rows(i).Item("GROUPID").ToString() <> "" Then
                            If i = 0 Then
                                MatId = dsProd.Tables(0).Rows(i).Item("ID").ToString()
                            Else
                                MatId = MatId + "," + dsProd.Tables(0).Rows(i).Item("ID").ToString()
                            End If
                        End If
                    Next

                    dsMat = objGetData.GetSubscProdGroups(MatId)

                Else                                                        'For Materialwise
                    arrFact = Regex.Split(Session("M1SubGroupId"), ",")
                    For i = 0 To arrFact.Length - 1
                        Dts = objGetData.GetSubGroupDetails(arrFact(i))

                        If Dts.Tables(0).Rows(0).Item("CATID").ToString() <> "" Then
                            If i = 0 Then
                                FactId = Dts.Tables(0).Rows(0).Item("CATID").ToString()
                            Else
                                FactId = FactId + "," + Dts.Tables(0).Rows(0).Item("CATID").ToString()
                            End If

                        End If
                    Next

                    Dts = objGetData.GetSubFactGroupDetails(FactId)
                    If Dts.Tables(0).Rows.Count > 0 Then
                        For i = 0 To Dts.Tables(0).Rows.Count - 1
                            If Dts.Tables(0).Rows(i).Item("ID").ToString() <> "" Then
                                If i = 0 Then
                                    StrSql = Dts.Tables(0).Rows(i).Item("ID").ToString()
                                Else
                                    StrSql = StrSql + "," + Dts.Tables(0).Rows(i).Item("ID").ToString()
                                End If
                            End If
                        Next

                        dsProdMat = objGetData.GetSubscMaterials(StrSql)

                        If dsProdMat.Tables(0).Rows.Count > 0 Then
                            For i = 0 To dsProdMat.Tables(0).Rows.Count - 1
                                If dsProdMat.Tables(0).Rows(i).Item("GROUPID").ToString() <> "" Then
                                    If i = 0 Then
                                        ProdId = dsProdMat.Tables(0).Rows(i).Item("GROUPID").ToString()
                                    Else
                                        ProdId = ProdId + "," + dsProdMat.Tables(0).Rows(i).Item("GROUPID").ToString()
                                    End If
                                End If
                            Next

                            dsMat = objGetData.GetSubscProdGroups(ProdId)
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

    Private Sub GetMaterialValue(ByVal RptID As Integer)
        Dim objGetDate As New Selectdata()
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
        Dim j As Integer = 0
        Dim dsProd As New DataSet
        Dim dsFact As New DataSet
        Dim dsProdMat As New DataSet
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
                            ProdId = ProdId + "," + ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        End If

                    ElseIf ds.Tables(0).Rows(i).Item("FILTERTYPE").ToString() = "Package" Then
                        If PackId = String.Empty Then
                            PackId = ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        Else
                            PackId = PackId + "," + ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
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
                            ProdId = ProdId + "," + ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        End If

                    ElseIf ds.Tables(0).Rows(i).Item("FILTERTYPE").ToString() = "Material" Then
                        If MatId = String.Empty Then
                            MatId = ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        Else
                            MatId = MatId + "," + ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
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

    Private Sub GetProductValue(ByVal RptID As Integer)
        Dim ds As New DataSet()
        Dim objGetData As New Selectdata()
        Dim dsMat As New DataSet
        Dim FactId As String = String.Empty
        Dim PackId As String = String.Empty
        Dim MatId As String = String.Empty
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
                            PackId = PackId + "," + ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        End If

                    ElseIf ds.Tables(0).Rows(i).Item("FILTERTYPE").ToString() = "Material" Then
                        If MatId = String.Empty Then
                            MatId = ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        Else
                            MatId = MatId + "," + ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
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
                            StrSql = Dts.Tables(0).Rows(i).Item("ID").ToString()
                        Else
                            StrSql = StrSql + "," + Dts.Tables(0).Rows(i).Item("ID").ToString()
                        End If
                    Next
                    dsMat = objGetData.GetPackageTypeByFact(StrSql)
                End If

            Else
                dsMat = objGetDate.GetFilterSQL(filterTypeId, Session("M1SubGroupId").ToString())
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
            '            RegionId = dsSub.Tables(0).Rows(i).Item("REGIONSETID").ToString()
            '        Else
            '            RegionId = RegionId + "," + dsSub.Tables(0).Rows(i).Item("REGIONSETID").ToString()
            '        End If
            '    Next
            'End If
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
            ElseIf ddlFilterType.SelectedItem.Text = "Material" Then ' For Material
                GetMaterialValue(RptID)
            ElseIf ddlFilterType.SelectedItem.Text = "Product" Then ' For Product
                GetProductValue(RptID)
            ElseIf ddlFilterType.SelectedItem.Text = "MaterialGrp" Then
                GetMaterialGroupValue(RptID)
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
End Class
