Imports System.Data
Imports System.Data.OleDb
Imports System
Imports M1SubGetData
Imports M1SubUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Market1_PopUp_RowSelector
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            GetRowSelector()
        End If
    End Sub

    Protected Sub GetRowSelector()
        Dim objGetDate As New Selectdata()
        Dim ds As New DataSet()
        Dim lst As New ListItem
        Dim RptType As String = String.Empty
        Dim dsRpt As New DataSet
        Dim dsFilter As New DataSet
        Dim dv As New DataView
        Dim dt As New DataTable
        Dim GrpName As String = String.Empty
        Try

            'ds = objGetDate.GetRowsSelector(-1)
            Dim RptID As String = Request.QueryString("RptID").ToString()
            If Request.QueryString("isTemp").ToString() = "Y" Then
                dsRpt = objGetDate.GetUserCustomReportsByRptIdTemp(RptID)
                dsFilter = objGetDate.GetUsersReportFiltersTemp(RptID)
            Else
                dsRpt = objGetDate.GetReportDetails(RptID)
                dsFilter = objGetDate.GetUsersReportFilters(RptID)
            End If
            If Session("RPTTYPE") = "MIXED" Then
                ds = objGetDate.GetMixedRowsSelector(-1)
            Else
                ds = objGetDate.GetRowsSelector(-1)
            End If

            If dsRpt.Tables(0).Rows(0).Item("RPTTYPE").ToString().ToUpper() = "UNIFORM" Then
                dv = ds.Tables(0).DefaultView
                If dsRpt.Tables(0).Rows(0).Item("RPTTYPEDES").ToString().ToUpper() = "CNTRY" Then
                    dv.RowFilter = "FILTERDES NOT IN ('Country')"
                ElseIf dsRpt.Tables(0).Rows(0).Item("RPTTYPEDES").ToString().ToUpper() = "GROUP" Then
                    dv.RowFilter = "FILTERDES NOT IN ('Group')"
                ElseIf dsRpt.Tables(0).Rows(0).Item("RPTTYPEDES").ToString().ToUpper() = "MATERIAL" Then
                    dv.RowFilter = "FILTERDES NOT IN ('Material')"
                ElseIf dsRpt.Tables(0).Rows(0).Item("RPTTYPEDES").ToString().ToUpper() = "MATGRP" Then
                    dv.RowFilter = "FILTERDES NOT IN ('MaterialGrp')"
                ElseIf dsRpt.Tables(0).Rows(0).Item("RPTTYPEDES").ToString().ToUpper() = "PACK" Then
                    dv.RowFilter = "FILTERDES NOT IN ('Package')"
                ElseIf dsRpt.Tables(0).Rows(0).Item("RPTTYPEDES").ToString().ToUpper() = "PROD" Then
                    dv.RowFilter = "FILTERDES NOT IN ('Product')"
                ElseIf dsRpt.Tables(0).Rows(0).Item("RPTTYPEDES").ToString().ToUpper() = "REGION" Then
                    dv.RowFilter = "FILTERDES NOT IN ('Region')"
                End If
                dt = dv.ToTable()
                ds.Tables.Clear()
                ds.Tables.Add(dt)
            Else
                If dsFilter.Tables(0).Rows(0).Item("FILTERTYPE").ToString() <> "" Then
                    For i = 0 To dsFilter.Tables(0).Rows.Count - 1
                        GrpName = GrpName + " " + "'" + dsFilter.Tables(0).Rows(i).Item("FILTERTYPE").ToString() + "',"
                    Next
                    GrpName = GrpName.Remove(GrpName.Length - 1)
                    dv = ds.Tables(0).DefaultView
                    dv.RowFilter = "ROWDES NOT IN (" + GrpName + ")"
                    dt = dv.ToTable()
                    ds.Tables.Clear()
                    ds.Tables.Add(dt)
                End If
            End If
            lst.Text = "--Select--"
            lst.Value = "0"
            lst.Attributes.Add("ROWTYPEID", "-1")
            lst.Attributes.Add("ROWDES", "SELECT")

            With ddlRowType
                .Items.Add(lst)
                .SelectedItem.Value = "0"
                .AppendDataBoundItems = True
                .DataSource = ds
                .DataTextField = "ROWDES"
                .DataValueField = "ROWTYPEID"
                .DataBind()
            End With
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetRowSelector" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetUnits(ByVal unitID As String)
        Dim objGetDate As New Selectdata()
        Dim ds As New DataSet()
        Try

            ds = objGetDate.GetUnits(unitID, Session("M1SubGroupId"))
            With ddlUnits
                .DataSource = ds
                .DataTextField = "UNITDES"
                .DataValueField = "UNITID"
                .DataBind()
            End With

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetUnits" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetRegionSet()
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet()
        Dim dsSub As New DataSet()
        Dim RegionSetId As String = String.Empty
        Try

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

    Protected Sub GetProducts(ByVal rowtype As String)
        Dim objGetData As New Selectdata()
        Dim ds As New DataSet()
        Dim dts As New DataSet()
        Dim DsGroup As New DataSet()
        Dim FactId As String = String.Empty
        Dim StrSql As String = String.Empty
        Dim arrFact() As String
        Try
            If rowtype = 1 Then
                ds = objGetData.GetRowSQL(rowtype, Session("M1SubGroupId").ToString())
            ElseIf rowtype = 4 Then
                ds = objGetData.GetFilterCountriesByRegion(Session("M1SubRegionId").ToString())
            ElseIf rowtype = 7 Then
                arrFact = Regex.Split(Session("M1SubGroupId"), ",")
                For i = 0 To arrFact.Length - 1
                    dts = objGetData.GetSubGroupDetails(arrFact(i))

                    If dts.Tables(0).Rows(0).Item("CATID").ToString() <> "" Then
                        If i = 0 Then
                            FactId = dts.Tables(0).Rows(0).Item("CATID").ToString() + ","
                        Else
                            FactId = FactId + dts.Tables(0).Rows(0).Item("CATID").ToString() + ","
                        End If

                    End If
                Next
                FactId = FactId.Remove(FactId.Length - 1)
                dts = objGetData.GetSubFactGroupDetails(FactId)
                For i = 0 To dts.Tables(0).Rows.Count - 1
                    If i = 0 Then
                        StrSql = dts.Tables(0).Rows(i).Item("ID").ToString()
                    Else
                        StrSql = StrSql + "," + dts.Tables(0).Rows(i).Item("ID").ToString()
                    End If
                Next
                ds = objGetData.GetPackageTypeByFact(StrSql)
            Else
                ds = objGetData.GetRowSQL(rowtype, Session("M1SubGroupId").ToString())
            End If

            ddlProducts.Items.Clear()
            Dim lst As New ListItem
            lst.Text = "Nothing Selected"
            lst.Value = -1
            ddlProducts.Items.Add(lst)
            ddlProducts.AppendDataBoundItems = True
            With ddlProducts
                .DataSource = ds
                .DataTextField = "VALUE"
                .DataValueField = "ID"
                .DataBind()
            End With

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetProducts" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetProductsWithClause(ByVal rowtype As String)
        Dim objGetDate As New Selectdata()
        Dim ds As New DataSet()
        Dim Clause As String = String.Empty
        Try

            Clause = " AND CLAUSE1=" + ddlRegionSet.SelectedValue '+ " AND ID IN (" + Session("M1SubRegionId") + ")"
            ds = objGetDate.GetRowSQLWithClause(rowtype, Clause)
            ddlProducts.Items.Clear()
            Dim lst As New ListItem
            lst.Text = "Nothing Selected"
            lst.Value = -1
            ddlProducts.Items.Add(lst)
            ddlProducts.AppendDataBoundItems = True
            With ddlProducts
                .DataSource = ds
                .DataTextField = "VALUE"
                .DataValueField = "ID"
                .DataBind()
            End With

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetProductsWithClause" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSumitt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSumitt.Click
        Try
            Dim RptID As String = Request.QueryString("RptID").ToString()
            '  Dim Seq As String = Request.QueryString("Seq").ToString()
            Dim RowDes As String = String.Empty
            Dim ShowRowDes As String = String.Empty
            Dim Curr As String = String.Empty
            Dim RowVal As String = String.Empty
            Dim RowVal1 As String = String.Empty
            Dim RowVal2 As String = String.Empty
            Dim RowValId1 As String = String.Empty
            Dim RowValId2 As String = String.Empty
            Dim UnitId1 As String = String.Empty
            Dim UnitId2 As String = String.Empty
            Dim RowValuType As String = String.Empty
            'Dim dsCurr As New DataSet
            'Dim drCurr As DataRow()
            Dim ds As New DataSet()
            Dim rowValueId As String = String.Empty

            Dim objUpIns As New UpdateInsert()
            Dim objGetDate As New Selectdata()
            Dim hidValue As Integer = CInt(Request.QueryString("hidValue"))
            Dim hidId As String = Request.QueryString("hidId")

            'dsCurr = ViewState("dsCurr")
            hidRowDes.Value = Request.QueryString("Id").ToString()
            hidRowID.Value = Request.QueryString("hidId").ToString()

            RowDes = ddlRowType.SelectedItem.Text.ToString()
            RowVal = ddlRowType.SelectedItem.Value.ToString()

            Dim ds1 As New DataSet
            Dim ds2 As New DataSet
            If ddlRowType.SelectedItem.Text = "Mixed Row" Then
                ds1 = objGetDate.GetUnits(ddlCapitaUnit1.SelectedValue.ToString(), Session("M1SubGroupId"))
                ds2 = objGetDate.GetUnits(ddlCapitaUnit2.SelectedValue.ToString(), Session("M1SubGroupId"))
                ShowRowDes = ddlCapitaRow1.SelectedItem.Text + " (" + ds1.Tables(0).Rows(0).Item("UNITSHRT").ToString() + ")/" + ddlCapitaRow2.SelectedItem.Text + " (" + ds2.Tables(0).Rows(0).Item("UNITSHRT").ToString() + ")"
            Else
                ds = objGetDate.GetUnits(ddlUnits.SelectedValue.ToString(), Session("M1SubGroupId"))
                If ds.Tables(0).Rows.Count > 0 Then
                    If hidCatID.Value = "0" Then
                        ShowRowDes = ddlProducts.SelectedItem.Text + " (" + ds.Tables(0).Rows(0).Item("UNITSHRT").ToString() + ")"
                    End If
                Else
                    If hidCatID.Value = "0" Then
                        ShowRowDes = ddlProducts.SelectedItem.Text
                    End If
                End If
            End If

            If ddlRowType.SelectedItem.Text = "Mixed Row" Then
                RowValuType = "Formula"
                RowVal = ddlRowType.SelectedItem.Text.ToString()
                rowValueId = "" 'ddlProducts.SelectedValue.ToString()
                RowVal1 = ddlCapitaRow1.SelectedItem.Text.ToString()
                RowVal2 = ddlCapitaRow2.SelectedItem.Text.ToString()
                RowValId1 = ddlCapitaRow1.SelectedValue
                RowValId2 = ddlCapitaRow2.SelectedValue
                UnitId1 = ddlCapitaUnit1.SelectedValue
                UnitId2 = ddlCapitaUnit2.SelectedValue
            Else
                If hidCatID.Value = "0" Then
                    RowValuType = ddlRowType.SelectedItem.Text.ToString()
                    RowVal = ddlProducts.SelectedItem.Text
                    rowValueId = ddlProducts.SelectedValue.ToString()
                End If
                If hidCatID.Value <> "0" Then
                    ShowRowDes = hidCatDes1.Value + " (" + ds.Tables(0).Rows(0).Item("UNITSHRT").ToString() + ")"
                    RowVal = hidCatDes1.Value
                    rowValueId = hidCatID.Value
                End If
            End If

            Curr = "0"
            Dim dsRptCols As New DataSet
            Dim objGetData As New Selectdata
            Dim pSubTypeid As String = "null"
            Dim pSubTypeValue As String = String.Empty
            Dim pSelId As String = "null"
            Dim pSelValue As String = ""
            Dim pUnitId As String = "null"
            Dim pUnitValue As String = ""
            Dim seq As String = "null"
            Dim dr() As DataRow
            Dim rptName As String
            dsRptCols = objGetData.GetUsersDynamicReportRows(RptID)
            rptName = objGetData.GetReportName(RptID)

            If hidValue <> 0 Then
                If Request.QueryString("isTemp").ToString() = "Y" Then
                    If ddlRowType.SelectedItem.Text = "Mixed Row" Then
                        objUpIns.UpdateCapitaRowDetailsTemp(RowDes, RowVal, hidValue, RowValuType, Curr, RowVal1, RowVal2, ddlRowType.SelectedValue.ToString(), rowValueId, "0", RowValId1, RowValId2, UnitId1, UnitId2, RptID)
                    Else
                        objUpIns.UpdateRowDetailsTemp(RowDes, RowVal, hidValue, RowValuType, Curr, RowVal1, RowVal2, ddlRowType.SelectedValue.ToString(), rowValueId, ddlUnits.SelectedValue.ToString(), RptID)
                    End If
                    If ddlRowType.SelectedItem.Text = "Region" Then
                        objUpIns.EditReportTypeDesTemp(RptID, "REGION")
                    ElseIf ddlRowType.SelectedItem.Text = "Country" Then
                        objUpIns.EditReportTypeDesTemp(RptID, "CNTRY")
                    End If
                ElseIf Request.QueryString("isTemp").ToString() = "N" Then
                    If ddlRowType.SelectedItem.Text = "Mixed Row" Then
                        objUpIns.UpdateCapitaRowDetails(RowDes, RowVal, hidValue, RowValuType, Curr, RowVal1, RowVal2, ddlRowType.SelectedValue.ToString(), rowValueId, "0", RowValId1, RowValId2, UnitId1, UnitId2, RptID)
                    Else
                        objUpIns.UpdateRowDetails(RowDes, RowVal, hidValue, RowValuType, Curr, RowVal1, RowVal2, ddlRowType.SelectedValue.ToString(), rowValueId, ddlUnits.SelectedValue.ToString(), RptID)
                    End If
                    If ddlRowType.SelectedItem.Text = "Region" Then
                        objUpIns.EditReportTypeDes(RptID, "REGION")
                    ElseIf ddlRowType.SelectedItem.Text = "Country" Then
                        objUpIns.EditReportTypeDes(RptID, "CNTRY")
                    End If
                End If
            End If

            Dim dsRp As New DataSet
            dsRp = objGetData.GetUserCustomReportsByRptIdTemp(RptID)
            Dim rptDesc As String = ""
            If Session("RPTTYPE") = "MIXED" Then
                rptDesc = ""
            Else
                rptDesc = dsRp.Tables(0).Rows(0).Item("RPTTYPEDES").ToString()
            End If
            If ddlRowType.SelectedItem.Text = "Region" Then
                objUpIns.InsertLog(Session("UserId"), Session("MLogInLog"), "6", RptID.ToString(), Session("RPTTYPE"), rptDesc, "8", rowValueId, Session.SessionID)
            ElseIf ddlRowType.SelectedItem.Text = "Country" Then
                objUpIns.InsertLog(Session("UserId"), Session("MLogInLog"), "6", RptID.ToString(), Session("RPTTYPE"), rptDesc, "9", rowValueId, Session.SessionID)
            ElseIf ddlRowType.SelectedItem.Text = "Product" Then
                objUpIns.InsertLog(Session("UserId"), Session("MLogInLog"), "6", RptID.ToString(), Session("RPTTYPE"), rptDesc, "6", rowValueId, Session.SessionID)
            ElseIf ddlRowType.SelectedItem.Text = "Package" Then
                objUpIns.InsertLog(Session("UserId"), Session("MLogInLog"), "6", RptID.ToString(), Session("RPTTYPE"), rptDesc, "7", rowValueId, Session.SessionID)
            ElseIf ddlRowType.SelectedItem.Text = "Group" Then
                objUpIns.InsertLog(Session("UserId"), Session("MLogInLog"), "6", RptID.ToString(), Session("RPTTYPE"), rptDesc, "11", rowValueId, Session.SessionID)
            ElseIf ddlRowType.SelectedItem.Text = "Material" Then
                objUpIns.InsertLog(Session("UserId"), Session("MLogInLog"), "6", RptID.ToString(), Session("RPTTYPE"), rptDesc, "10", rowValueId, Session.SessionID)
            ElseIf ddlRowType.SelectedItem.Text = "Component" Then
                objUpIns.InsertLog(Session("UserId"), Session("MLogInLog"), "6", RptID.ToString(), Session("RPTTYPE"), rptDesc, "12", rowValueId, Session.SessionID)
            End If

            ClientScript.RegisterStartupScript(Me.GetType(), "RowSel", "RowSelection('" + ShowRowDes.ToString() + "','" + hidValue.ToString() + "','" + hidId + "');", True)
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSumitt_Click" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlRowType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRowType.SelectedIndexChanged
        'Dim RowCode As String = String.Empty
        Dim dsPackage As New DataSet()
        Dim ds As New DataSet()
        Dim PackValue As String
        Dim objGetData As New Selectdata()

        Try
            Dim RptID As String = Request.QueryString("RptID").ToString()
            'RowCode = hidRow.Value
            hidCatID.Value = "0"
            rwCapitaRow1.Visible = False
            rwCapitaRow2.Visible = False
            If ddlRowType.SelectedItem.Text = "Region" Then
                lblRegionSet.Text = "<b> Select Regionset <b>"
                rwRegionSet.Visible = True
                ddlProducts.Visible = True
                lnkProductTree.Visible = False
                GetRegionSet()
                GetProductsWithClause(ddlRowType.SelectedValue)
            ElseIf ddlRowType.SelectedItem.Text = "Category" Or ddlRowType.SelectedItem.Text = "Group" Then
                rwRegionSet.Visible = False
                'GetProducts(ddlRowType.SelectedValue)
                ddlProducts.Visible = False
                lnkProductTree.Visible = True
                lnkProductTree.Text = " Select " + ddlRowType.SelectedItem.Text
            ElseIf ddlRowType.SelectedItem.Text = "Material Group" Then
                GetMaterialGroupValue(RptID)
            ElseIf ddlRowType.SelectedItem.Text = "Material" Then ' For Material
                GetMaterialValue(RptID)
                '    ddlProducts.Visible = True

                '    ds = objGetData.GetUserReportFilter(RptID)

                '    If ds.Tables(0).Rows.Count > 0 Then
                '        PackValue = ds.Tables(0).Rows(0).Item("FILTERVALUEID")
                '        dsPackage = objGetData.GetPackageTypeByFact(PackValue)

                '        With ddlProducts
                '            .DataSource = dsPackage
                '            .DataTextField = "VALUE"
                '            .DataValueField = "ID"
                '            .DataBind()
                '        End With
                '    Else
                '        rwRegionSet.Visible = False
                '        ddlProducts.Visible = True
                '        lnkProductTree.Visible = False
                '        GetProducts(ddlRowType.SelectedValue)
                '    End If
            ElseIf ddlRowType.SelectedItem.Text = "Mixed Row" Then
                GetCapitaValue(RptID)
            Else
                rwRegionSet.Visible = False
                ddlProducts.Visible = True
                lnkProductTree.Visible = False
                GetProducts(ddlRowType.SelectedValue)
            End If
            lblProducts.Text = "<b> Select " + ddlRowType.SelectedItem.Text + ":<b>"
            GetUnits("-1")
            If ddlRowType.SelectedItem.Text <> "Mixed Row" Then
                rwProducts.Visible = True
                lblProducts.Visible = True
                rwUnits.Visible = True
                colUnits.Visible = True
            End If


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub ddlRegionSet_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegionSet.SelectedIndexChanged
        Try
            GetProductsWithClause(ddlRowType.SelectedValue)

        Catch ex As Exception
            _lErrorLble.Text = "Error:ddlRegionSet_SelectedIndexChanged" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlCapitaRow1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCapitaRow1.SelectedIndexChanged
        Try
            GetCapitaUnitValue(Request.QueryString("RptID").ToString(), ddlCapitaRow1.SelectedItem.Text, "1")
        Catch ex As Exception
            _lErrorLble.Text = "Error:ddlCapitaRow1_SelectedIndexChanged" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlCapitaRow2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCapitaRow2.SelectedIndexChanged
        Try
            GetCapitaUnitValue(Request.QueryString("RptID").ToString(), ddlCapitaRow2.SelectedItem.Text, "2")
        Catch ex As Exception
            _lErrorLble.Text = "Error:ddlCapitaRow2_SelectedIndexChanged" + ex.Message.ToString()
        End Try
    End Sub

    Private Sub GetCapitaValue(ByVal RptID As Integer)
        Dim ds As New DataSet()
        Dim objGetData As New Selectdata()
        Dim dv As New DataView
        Dim dt As New DataTable
        Try
            ddlProducts.Visible = False
            rwProducts.Visible = False
            rwUnits.Visible = False
            lnkProductTree.Visible = False
            rwRegionSet.Visible = False
            rwCapitaRow1.Visible = True
            rwCapitaRow2.Visible = True

            If Request.QueryString("isTemp").ToString() = "Y" Then
                ds = objGetData.GetUserReportRowTFilter(RptID)
            Else
                ds = objGetData.GetUserReportRowFilter(RptID)
            End If
            Session("dsCapita") = ds
            'dv = ds.Tables(0).DefaultView
            'dv.RowFilter = "CLUASE2 NOT IN (" + Request.QueryString("Seq").ToString() + ")"
            'dt = dv.ToTable()
            'ds.Tables.Clear()
            'ds.Tables.Add(dt)

            If ds.Tables(0).Rows.Count > 0 Then
                With ddlCapitaRow1
                    .DataSource = ds
                    .DataTextField = "VALUE"
                    .DataValueField = "VALUEID"
                    .DataBind()
                End With

                With ddlCapitaRow2
                    .DataSource = ds
                    .DataTextField = "VALUE"
                    .DataValueField = "VALUEID"
                    .DataBind()
                End With
            End If
            GetCapitaUnitValue(RptID, ddlCapitaRow1.SelectedItem.Text, "1")
            GetCapitaUnitValue(RptID, ddlCapitaRow2.SelectedItem.Text, "2")

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCapitaValue" + ex.Message.ToString()
        End Try
    End Sub

    Private Sub GetCapitaUnitValue(ByVal RptID As Integer, ByVal Value As String, ByVal Row As String)
        Dim ds As New DataSet()
        Dim dv As New DataView
        Dim dt As New DataTable
        Dim objGetData As New Selectdata()
        Dim UnitId As String = ""
        Dim dsUnit As New DataSet()
        Try
            ddlProducts.Visible = False
            rwProducts.Visible = False
            rwUnits.Visible = False
            lnkProductTree.Visible = False
            rwRegionSet.Visible = False
            rwCapitaRow1.Visible = True
            rwCapitaRow2.Visible = True

            If Request.QueryString("isTemp").ToString() = "Y" Then
                ds = objGetData.GetUserReportRowTFilterUnit(RptID)
            Else
                ds = objGetData.GetUserReportRowFilterUnit(RptID)
            End If
            dv = ds.Tables(0).DefaultView

            If ds.Tables(0).Rows.Count > 0 Then
                dv.RowFilter = "VALUE='" + Value + "'"
                dt = dv.ToTable()
                For i = 0 To dt.Rows.Count - 1
                    UnitId = UnitId + dt.Rows(i).Item("UNITID").ToString() + ","
                Next
                UnitId = UnitId.Remove(UnitId.Length - 1)
                dsUnit = objGetData.GetCapitaUnits(UnitId)

                If Row = "1" Then
                    With ddlCapitaUnit1
                        .DataSource = dsUnit
                        .DataTextField = "VALUE"
                        .DataValueField = "ID"
                        .DataBind()
                    End With
                Else
                    With ddlCapitaUnit2
                        .DataSource = dsUnit
                        .DataTextField = "VALUE"
                        .DataValueField = "ID"
                        .DataBind()
                    End With
                End If
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCapitaValue" + ex.Message.ToString()
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
            ddlProducts.Visible = True
            lnkProductTree.Visible = False
            rwRegionSet.Visible = False

            'For Materialwise
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

            ddlProducts.Items.Clear()
            Dim lst As New ListItem
            lst.Text = "Nothing Selected"
            lst.Value = -1
            ddlProducts.Items.Add(lst)
            ddlProducts.AppendDataBoundItems = True
            With ddlProducts
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
        Dim dsFact As New DataSet
        Try
            ddlProducts.Visible = True
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
                            ProdId = "," + ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        End If

                    ElseIf ds.Tables(0).Rows(i).Item("FILTERTYPE").ToString() = "Package" Then
                        If PackId = String.Empty Then
                            PackId = ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
                        Else
                            PackId = "," + ds.Tables(0).Rows(i).Item("FILTERVALUEID").ToString()
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

            ddlProducts.Items.Clear()
            Dim lst As New ListItem
            lst.Text = "Nothing Selected"
            lst.Value = -1
            ddlProducts.Items.Add(lst)
            ddlProducts.AppendDataBoundItems = True
            With ddlProducts
                .DataSource = dsMat
                .DataTextField = "VALUE"
                .DataValueField = "ID"
                .DataBind()
            End With

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetFilterValue" + ex.Message.ToString()
        End Try
    End Sub

End Class
