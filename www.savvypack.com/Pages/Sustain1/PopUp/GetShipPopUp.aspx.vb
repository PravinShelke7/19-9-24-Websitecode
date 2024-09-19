Imports System.Data
Imports System.Data.OleDb
Imports System
Imports S1GetData

Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_Sustain1_PopUp_GetShipPopUp
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidShipdes.Value = Request.QueryString("Des").ToString()
            hidShipid.Value = Request.QueryString("ID").ToString()
            If Not IsPostBack Then
                GetShipDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetShipDetails()
        Dim ds As New DataSet
        Dim objGetData As New S1GetData.Selectdata()
        Try
            ds = objGetData.GetShipingSelector(-1, txtShipDe1.Text.Trim.ToString())
            grdShip.DataSource = ds
            grdShip.DataBind()
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetShipDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetShipDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub
End Class
