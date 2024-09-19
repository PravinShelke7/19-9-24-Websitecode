Imports System.Data
Imports System.Data.OleDb
Imports System
Imports Med2GetData
Imports Med2UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_MedEcon2_PopUp_GetPalletPopUp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hypPalDes.Value = Request.QueryString("Des").ToString()
            hidPalid.Value = Request.QueryString("ID").ToString()
            hidPalid.Value.Replace("/""", "#")
            If Not IsPostBack Then
                GetPalletDetails()
            End If
        Catch ex As Exception
            _lErrorLble.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            GetPalletDetails()
        Catch ex As Exception
            _lErrorLble.Text = "Error:btnSearch_Click:" + ex.Message.ToString()
        End Try
    End Sub


    Protected Sub GetPalletDetails()
        Dim Ds As New DataSet
        Dim ObjGetdata As New Med2GetData.Selectdata()
        Try

            Ds = ObjGetdata.GetPallets(-1, txtPalDe1.Text, txtPalDe2.Text)
            grdPallets.DataSource = Ds
            grdPallets.DataBind()

        Catch ex As Exception
            _lErrorLble.Text = "Error:Update_Click:" + ex.Message.ToString() + ""
        End Try
    End Sub


End Class
