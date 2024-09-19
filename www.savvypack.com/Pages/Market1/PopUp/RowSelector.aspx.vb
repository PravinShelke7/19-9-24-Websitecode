Imports System.Data
Imports System.Data.OleDb
Imports System
Imports M1GetData
Imports M1UpInsData
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
        Try

            ds = objGetDate.GetRowsSelector(-1)
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

            ds = objGetDate.GetUnits(unitID)
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

    Protected Sub GetProducts(ByVal rowtype As String)
        Dim objGetDate As New Selectdata()
        Dim ds As New DataSet()
        Try

            ds = objGetDate.GetRowSQL(rowtype)

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

            Clause = " AND CLAUSE1=" + ddlRegionSet.SelectedValue
            ds = objGetDate.GetRowSQLWithClause(rowtype, Clause)
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

    Protected Sub btnSumitt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSumitt.Click
        Try
            Dim RptID As String = Request.QueryString("RptID").ToString()
            Dim RowDes As String = String.Empty
            Dim ShowRowDes As String = String.Empty
            Dim Curr As String = String.Empty
            Dim RowVal As String = String.Empty
            Dim RowVal1 As String = String.Empty
            Dim RowVal2 As String = String.Empty
            Dim RowValuType As String = String.Empty
            Dim ds As New DataSet()
            Dim rowValueId As String = String.Empty

            Dim objUpIns As New UpdateInsert()
            Dim objGetDate As New Selectdata()
            Dim hidValue As Integer = CInt(Request.QueryString("hidValue"))
            Dim hidId As String = Request.QueryString("hidId")
            hidRowDes.Value = Request.QueryString("Id").ToString()
            hidRowID.Value = Request.QueryString("hidId").ToString()

            RowDes = ddlRowType.SelectedItem.Text.ToString()
            RowVal = ddlRowType.SelectedItem.Value.ToString()



            ds = objGetDate.GetUnits(ddlUnits.SelectedValue.ToString())
            If ds.Tables(0).Rows.Count > 0 Then
                ShowRowDes = ddlProducts.SelectedItem.Text + " (" + ds.Tables(0).Rows(0).Item("UNITSHRT").ToString() + ")"
            Else
                ShowRowDes = ddlProducts.SelectedItem.Text
            End If
            RowValuType = ddlRowType.SelectedItem.Text.ToString()
            RowVal = ddlProducts.SelectedItem.Text
            rowValueId = ddlProducts.SelectedValue.ToString()
            If hidCatID.Value <> "0" Then
                ShowRowDes = hidCatDes1.Value + " (" + ds.Tables(0).Rows(0).Item("UNITSHRT").ToString() + ")"
                RowVal = hidCatDes1.Value
                rowValueId = hidCatID.Value
            End If
            Curr = "0"


            If hidValue = 0 Then
                '  hidValue = objUpIns.AddRowDetails(RptID, Seq, RowDes, RowValuType, RowVal, Curr, RowVal1, RowVal2, ddlRowType.SelectedValue.ToString(), ddlProducts.SelectedValue.ToString())
            Else
                objUpIns.UpdateRowDetails(RowDes, RowVal, hidValue, RowValuType, Curr, RowVal1, RowVal2, ddlRowType.SelectedValue.ToString(), rowValueId, ddlUnits.SelectedValue.ToString())
                If ddlRowType.SelectedItem.Text = "Region" Then
                    objUpIns.EditReportTypeDes(RptID, "REGION")
                ElseIf ddlRowType.SelectedItem.Text = "Country" Then
                    objUpIns.EditReportTypeDes(RptID, "CNTRY")
                End If
            End If


            ClientScript.RegisterStartupScript(Me.GetType(), "RowSel", "RowSelection('" + ShowRowDes.ToString() + "','" + hidValue.ToString() + "','" + hidId + "');", True)
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSumitt_Click" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub ddlRowType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRowType.SelectedIndexChanged
        Try

            'RowCode = hidRow.Value
            hidCatID.Value = "0"
            If ddlRowType.SelectedItem.Text = "Region" Then
                lblRegionSet.Text = "<b> Select Regionset <b>"
                rwRegionSet.Visible = True
                ddlProducts.Visible = True
                lnkProductTree.Visible = False
                GetRegionSet()
                GetProductsWithClause(ddlRowType.SelectedValue)
            ElseIf ddlRowType.SelectedItem.Text = "Category" Or ddlRowType.SelectedItem.Text = "Group" Then
                rwRegionSet.Visible = False
                GetProducts(ddlRowType.SelectedValue)
                ddlProducts.Visible = False
                lnkProductTree.Visible = True
                lnkProductTree.Text = " Select " + ddlRowType.SelectedItem.Text
            Else
                rwRegionSet.Visible = False
                ddlProducts.Visible = True
                lnkProductTree.Visible = False
                GetProducts(ddlRowType.SelectedValue)
            End If
            lblProducts.Text = "<b> Select " + ddlRowType.SelectedItem.Text + ":<b>"
            GetUnits("-1")
            rwProducts.Visible = True
            lblProducts.Visible = True
            rwUnits.Visible = True
            colUnits.Visible = True

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
End Class
