Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Med1GetData
Imports Med1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MedEcon1_PopUp_GetMatDisPopUp
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hidMatdes.Value = Request.QueryString("Des").ToString()
            hidMatid.Value = Request.QueryString("ID").ToString()
            If Not IsPostBack Then
                GetDiscMaterialDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetDiscMaterialDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetDiscMaterialDetails()
        Dim ds As New DataSet
        Dim objGetData As New Med1GetData.Selectdata()
        Try
            ds = objGetData.GetDiscMaterials(-1, txtMatDe1.Text.Trim.ToString())
            grdMaterials.DataSource = ds
            grdMaterials.DataBind()
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetDiscreteMaterialDetails:" + ex.Message.ToString()
        End Try
    End Sub
End Class
