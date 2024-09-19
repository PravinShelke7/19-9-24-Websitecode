Imports System.Data
Imports System.Data.OleDb
Imports System
Imports MoldE2GetData
Imports MoldE2UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MoldEcon2_PopUp_GetMatPopUp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidMatdes.Value = Request.QueryString("Des").ToString()
            hidMatid.Value = Request.QueryString("ID").ToString()
            If Not IsPostBack Then
                GetMaterialDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetMaterialDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetMaterialDetails()
        Dim ds As New DataSet
        Dim objGetData As New MoldE2GetData.Selectdata()
        Try
            ds = objGetData.GetMaterials(-1, txtMatDe1.Text.Trim.ToString(), txtMatDe2.Text.Trim.ToString())
            grdMaterials.DataSource = ds
            grdMaterials.DataBind()
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetMaterialDetails:" + ex.Message.ToString()
        End Try
    End Sub


End Class
