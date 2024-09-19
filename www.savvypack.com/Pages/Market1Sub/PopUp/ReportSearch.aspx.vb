Imports System.Data
Imports System.Data.OleDb
Imports System
Imports M1SubGetData
Imports M1SubUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Market1_PopUp_ReportSearch
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidReportId.Value = Request.QueryString("ID").ToString()
            If Not IsPostBack Then
                GetReportDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetReportDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetReportDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Try
            If hidReportId.Value = "ddlPCase" Then
                ds = objGetData.GetUserCustomReportsForSearch(Session("UserId"), txtReportName.Text.ToString().Replace("'", "''").ToUpper(), Session("M1ServiceID"))
            Else

            End If

            grdReportSearch.DataSource = ds
            grdReportSearch.DataBind()

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetReportDetails:" + ex.Message.ToString()
        End Try
    End Sub

End Class
