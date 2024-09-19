Imports System.Data
Imports System.Data.OleDb
Imports System
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_SavvyPackPro_Charts_PopUp_PriceColumnSelection
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidPriceColDes.Value = Request.QueryString("Des1").ToString()
            hidPriceColId.Value = Request.QueryString("Id").ToString()
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
            If hidlnkdes.Value = "lnkPriceColumnLine" Then
                'ds = objGetdata.GetPriceDesForLineChart()
                ds = objGetdata.GetPriceColumn(Session("USERID"), Session("RFPPRICEID"))

            Else
                ds = objGetdata.GetPriceColumn(Session("USERID"), Session("RFPPRICEID"))
            End If
            grdCountry.DataSource = ds
            grdCountry.DataBind()
        Catch ex As Exception
            ' _lErrorLble.Text = "Error:GetCaseDetails:" + ex.Message.ToString()
        End Try
    End Sub


End Class
