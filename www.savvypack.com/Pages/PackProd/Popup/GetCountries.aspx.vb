Imports System.Data
Imports System.Data.OleDb
Imports System
Imports PackProdGetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_PackProd_Popup_GetCountries
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidCatid.Value = Request.QueryString("ID").ToString()
            If Not IsPostBack Then
                GetCountryDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetCountryDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetCountryDetails()
        Dim ds As New DataSet
        Dim objGetData As New PackProdGetData.Selectdata()
        Try
            ds = objGetData.GetCountryDetails(txtCountry.Text, Session("UserId"))
            grdState.DataSource = ds
            grdState.DataBind()
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCountryDetails:" + ex.Message.ToString()
        End Try
    End Sub
End Class
