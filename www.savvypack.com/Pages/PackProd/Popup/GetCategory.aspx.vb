Imports System.Data
Imports System.Data.OleDb
Imports System
Imports PackProdGetData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_PackProd_Popup_GetCategory
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim str As String = ""
            If Request.QueryString("ID") <> Nothing Then
                hidCatid.Value = Request.QueryString("ID").ToString()
            End If

            If Request.QueryString("ID").ToString() = "ddlProductType" Then
                hidCatDes.Value = "PRODAREA"
                str = "Product Type"
            ElseIf Request.QueryString("ID").ToString() = "ddlProdService" Then
                hidCatDes.Value = "PRODSER"
                str = "Product Service"
            ElseIf Request.QueryString("ID").ToString() = "ddlProdDevCap" Then
                hidCatDes.Value = "PRODDEVCAP"
                str = "Product Development Capability"
            ElseIf Request.QueryString("ID").ToString() = "ddlProcCap" Then
                hidCatDes.Value = "PROCCAP"
                str = "Processing Capabilities"
            ElseIf Request.QueryString("ID").ToString() = "ddlMacSys" Then
                hidCatDes.Value = "PACKMACHSYS"
                str = "Machinery Systems"
            ElseIf Request.QueryString("ID").ToString() = "ddlRepCus" Then
                hidCatDes.Value = "REPCUST"
                str = "Representative Customers"
            End If
            lblCategory.Text = str
            lblCategoryD.Text = str
            Page.Title = "Get " + str

            If Not IsPostBack Then
                GetCategoryDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetCategoryDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetCategoryDetails()
        Dim ds As New DataSet
        Dim objGetData As New PackProdGetData.Selectdata()
        Try
            ds = objGetData.GetCategoryDetailsByText(hidCatDes.Value, txtCategory.Text)
            grdCategory.DataSource = ds
            grdCategory.DataBind()
        Catch ex As Exception
            _lErrorLble.Text = "Error:GetCategoryDetails:" + ex.Message.ToString()
        End Try
    End Sub
End Class
