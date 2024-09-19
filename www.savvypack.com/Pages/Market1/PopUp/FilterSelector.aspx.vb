Imports System.Data
Imports System.Data.OleDb
Imports System
Imports M1GetData
Imports M1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Market1_PopUp_FilterSelector
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
        Try
            If hidCatID.Value <> "0" Then
                filterValue = hidCatDes1.Value
                filterValueID = hidCatID.Value
            Else
                filterValue = ddlFilterValue.SelectedItem.ToString()
                filterValueID = ddlFilterValue.SelectedValue.ToString()
            End If
            If hidValue = 0 Then
                'hidValue = objUpIns.AddFilterDetails(RptID, Seq, ddlFilterType.SelectedItem.Text, ddlFilterValue.SelectedItem.Text, ddlFilterType.SelectedValue, ddlFilterValue.SelectedValue)
            Else
                objUpIns.UpdateFilterDetails(ddlFilterType.SelectedItem.Text, filterValue, ddlFilterType.SelectedValue.ToString(), filterValueID, hidValue.ToString())
                If ddlFilterType.SelectedItem.Text = "Region" Then
                    objUpIns.EditReportTypeDes(RptID, "REGION")
                ElseIf ddlFilterType.SelectedItem.Text = "Country" Then
                    objUpIns.EditReportTypeDes(RptID, "CNTRY")
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
        Dim lst As New ListItem
        Dim RptType As String = String.Empty
        Try

            ds = objGetDate.GetFilterSelector(-1)
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


    Private Sub GetFilterValue(ByVal filterTypeId As String)
        Dim objGetDate As New Selectdata()
        Dim ds As New DataSet()
        Try

            ds = objGetDate.GetFilterSQL(filterTypeId)
            With ddlFilterValue
                .DataSource = ds
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

            Clause = " AND CLAUSE1=" + ddlRegionSet.SelectedValue
            ds = objGetDate.GetFilterSQLWithClause(filterTypeId, Clause)
            With ddlFilterValue
                .DataSource = ds
                .DataTextField = "VALUE"
                .DataValueField = "ID"
                .DataBind()
            End With

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetFilterValue" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetRegionSet()
        Dim objGetDate As New Selectdata()
        Dim ds As New DataSet()
        Try

            ds = objGetDate.GetUserRegionSets("-1")
            With ddlRegionSet
                .DataSource = ds
                .DataTextField = "REGIONSETNAME"
                .DataValueField = "REGIONSETID"
                .DataBind()
            End With

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetUnits" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub ddlFilterType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFilterType.SelectedIndexChanged
        Try
            hidCatID.Value = "0"
            If ddlFilterType.SelectedItem.Text = "Region" Then
                GetRegionSet()
                lblRegionSet.Text = "<b> Select Regionset <b>"
                rwRegionSet.Visible = True
                ddlFilterValue.Visible = True
                lnkProductTree.Visible = False
                GetFilterValueWithClause(ddlFilterType.SelectedValue)
            ElseIf ddlFilterType.SelectedItem.Text = "Category" Or ddlFilterType.SelectedItem.Text = "Group" Then
                rwRegionSet.Visible = False
                ddlFilterValue.Visible = False
                lnkProductTree.Visible = True
                lnkProductTree.Text = " Select " + ddlFilterType.SelectedItem.Text
            Else
                ddlFilterValue.Visible = True
                lnkProductTree.Visible = False
                rwRegionSet.Visible = False
                GetFilterValue(ddlFilterType.SelectedValue)
            End If
            lblFilterValue.Text = "<b>" + ddlFilterType.SelectedItem.Text + " :<b>"
            rwFilterValue.Visible = True
        Catch ex As Exception
            _lErrorLble.Text = "Error:ddlFilterValue:" + ex.Message.ToString()
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
