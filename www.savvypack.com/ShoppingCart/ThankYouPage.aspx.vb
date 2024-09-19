Imports System.Data
Imports System.Data.OleDb
Imports System
Imports ShopGetData
Imports ShopUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Imports System.Net.Mail
Imports System.Net
Imports System.Configuration
Imports System.Collections.Generic
Imports System.IO

Partial Class ShoppingCart_ThankYouPage
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objGetUserData As New UsersGetData.Selectdata()
        Try
            If Session("RefNumber") = "" Then
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "New_WinCreate", "window.close();", True)
                Response.Redirect("~/Index.aspx", False)
            End If
            Response.Cache.SetCacheability(HttpCacheability.NoCache)

            If Session("Back") = Nothing And Session("SBack") = Nothing Then
                Dim obj As New CryptoHelper
                Response.Redirect("~/Universal_loginN/Pages/ErrorU.aspx?ErrorCode=" + obj.Encrypt("ALDE112") + "", False)
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class
