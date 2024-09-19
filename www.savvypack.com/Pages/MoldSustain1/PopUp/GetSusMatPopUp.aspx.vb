Imports System.Data
Imports System.Data.OleDb
Imports System
Imports MoldS1GetData

Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MoldSustain1_PopUp_GetSusMatPopUp
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidMatdes.Value = Request.QueryString("Des").ToString()
            hidMatid.Value = Request.QueryString("ID").ToString()
            If Not IsPostBack Then
                GetSusMaterial()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub GetSusMaterial()
        Dim ds As New DataSet
        Dim objGetData As New MoldS1GetData.Selectdata()
        Try
            ds = objGetData.GetSustainableMaterial(-1, txtSusMatDe1.Text.Trim.ToString())
            grdMat.DataSource = ds
            grdMat.DataBind()
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetMaterialDetails:" + ex.Message.ToString()
        End Try
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetSusMaterial()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub
End Class
