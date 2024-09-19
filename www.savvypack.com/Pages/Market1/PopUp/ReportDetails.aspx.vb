Imports System.Data
Imports System.Data.OleDb
Imports System
Imports M1GetData
Imports M1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Market1_PopUp_ReportDetails
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidReportDes.Value = Request.QueryString("Des").ToString()
            hidReportId.Value = Request.QueryString("ID").ToString()
            If Not IsPostBack Then
                GetReportDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub


    Protected Sub GetReportDetails()
        Dim ds As New DataSet
        Dim objGetData As New Selectdata()
        Try
            If Request.QueryString("Type").ToString() = "Base" Then
                ds = objGetData.GetBaseReports()
            Else
                ds = objGetData.GetUserCustomReportsForSearch(Session("UserId"), "")
            End If


            grdReportSearch.DataSource = ds
            grdReportSearch.DataBind()

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetReportDetails:" + ex.Message.ToString()
        End Try
    End Sub
End Class
