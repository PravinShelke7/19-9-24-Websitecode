Imports System.Data
Imports System.Data.OleDb
Imports System
Imports SDistGetData
Imports SDistUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_SDistribution_PopUp_CaseSearch
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidCaseid.Value = Request.QueryString("ID").ToString()
            If Not IsPostBack Then
                GetCaseDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetCaseDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetCaseDetails()
        Dim ds As New DataSet
        Dim objGetData As New SDistGetData.Selectdata()
        Try
            If hidCaseid.Value = "ddlPCase" Then
                ds = objGetData.GetCases(Session("USERID"), txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), txtCaseDe2.Text.Trim.ToString().Replace("'", "''"))
            Else
                ds = objGetData.GetBCases(txtCaseDe1.Text.Trim.ToString().Replace("'", "''"), txtCaseDe2.Text.Trim.ToString().Replace("'", "''"))
            End If

            grdCaseSearch.DataSource = ds
            grdCaseSearch.DataBind()

            If ds.Tables(0).Rows.Count > 0 Then
                btnCaseViewer.Visible = True
            Else
                btnCaseViewer.Visible = False
            End If

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub

End Class
