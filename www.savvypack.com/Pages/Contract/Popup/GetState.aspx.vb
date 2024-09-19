Imports System.Data
Imports System.Data.OleDb
Imports System
Imports ContrGetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Contract_Popup_GetState
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidCatid.Value = Request.QueryString("ID").ToString()
            If Not IsPostBack Then
                GetStateDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetStateDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetStateDetails()
        Dim ds As New DataSet
        Dim objGetData As New ContrGetData.Selectdata()
        Dim CountryId As String = Request.QueryString("CountryId")
        Try
            ds = objGetData.GetStateDetails(txtState.Text, CountryId)
            grdState.DataSource = ds
            grdState.DataBind()
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetStateDetails:" + ex.Message.ToString()
        End Try
    End Sub
End Class
