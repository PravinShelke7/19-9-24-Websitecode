Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Med1GetData
Imports Med1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MedEcon1_PopUp_GetCostTypePopup
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hypCostDes.Value = Request.QueryString("Des").ToString()
            hidCostid.Value = Request.QueryString("ID").ToString()
            hidCostid.Value.Replace("/""", "#")
            If Not IsPostBack Then
                GetCostTypeDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetCostTypeDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub


    Protected Sub GetCostTypeDetails()
        Dim Ds As New DataSet
        Dim ObjGetdata As New Med1GetData.Selectdata()
        Try

            Ds = ObjGetdata.GetCostTypeInfo(-1, txtCostDe1.Text)
            grdCostType.DataSource = Ds
            grdCostType.DataBind()
            'Ds = ObjGetdata.GetMaterials(-1, "", "")
            'grdMaterials.DataSource = Ds
            'grdMaterials.DataBind()

        Catch ex As Exception
            _lErrorLble.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub
End Class
