Imports System.Data
Imports System.Data.OleDb
Imports System
Imports MoldE1GetData
Imports MoldE1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MoldEcon1_PopUp_GetProdcuts
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hypPalDes.Value = Convert.ToString(Request.QueryString("Des"))
            hidPalid.Value = Convert.ToString(Request.QueryString("ID"))
            hidButton.Value = Convert.ToString(Request.QueryString("Btn"))
            If Not IsPostBack Then
                GetProductDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetProductDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub


    Protected Sub GetProductDetails()
        Dim Ds As New DataSet
        Dim ObjGetdata As New MoldE1GetData.Selectdata()
        Try

            Ds = ObjGetdata.GetProductFormt(-1, txtPalDe1.Text, txtPalDe2.Text)
            grdProducts.DataSource = Ds
            grdProducts.DataBind()

        Catch ex As Exception
            _lErrorLble.Text = "Error:GetProductDetails:" + ex.Message.ToString() + ""
        End Try
    End Sub


End Class
