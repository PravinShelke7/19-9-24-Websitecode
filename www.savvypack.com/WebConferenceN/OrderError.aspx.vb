Imports System.Data
Imports System.Data.OleDb
Imports System
Imports ShopGetData
Imports ShopUpInsData

Partial Class ShoppingCart_OrderError
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblText.Text = Session("PaidMessageWEB")
            Session("PaidMessageWEB") = ""
        Catch ex As Exception

        End Try
    End Sub
End Class
