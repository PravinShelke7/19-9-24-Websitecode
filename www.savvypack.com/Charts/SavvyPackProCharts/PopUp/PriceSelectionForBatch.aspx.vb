Imports System.Data
Imports System.Data.OleDb
Imports System
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Charts_SavvyPackProCharts_PopUp_Price
    Inherits System.Web.UI.Page

    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidPriceDes.Value = Request.QueryString("Des1").ToString()
            hidPriceId.Value = Request.QueryString("Id").ToString()
            hidlnkdes.Value = Request.QueryString("Des").ToString()
            If Not IsPostBack Then
                GetCountryDetails()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub GetCountryDetails()
        Dim ds As New DataSet

        Try

            ds = objGetdata.GetPriceDes(Session("RFPID"))
            grdCountry.DataSource = ds
            grdCountry.DataBind()
        Catch ex As Exception
            ' _lErrorLble.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub

End Class
