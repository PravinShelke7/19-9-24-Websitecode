Imports System.Data
Imports System.Data.OleDb
Imports System
Imports E1GetData
Imports E1UpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_SavvyPackPro_Errors_Error
    Inherits System.Web.UI.Page
    Dim ErrorCode As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim obj As New CryptoHelper
        Try
            ErrorCode = obj.Decrypt(Request.QueryString("ErrorCode").ToString().Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&"))
            GetPageDetails()
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim objGetData As New E1GetData.Selectdata()
        Dim ds As New DataSet()

        Try
            ds = objGetData.GetErrors(ErrorCode)
            lblErrorCode.Text = ds.Tables(0).Rows(0).Item("ERRORCODE").ToString()
            lblErrorMessage.Text = ds.Tables(0).Rows(0).Item("ERRORDE1").ToString()
            If ds.Tables(0).Rows(0).Item("ERRORTYPE").ToString() = "UPDATE" Then
                divUpdate.Visible = True
                hypPage.NavigateUrl = "../" + DirectCast(Request.UrlReferrer, System.Uri).Segments(Request.UrlReferrer.Segments.Length - 2) + DirectCast(Request.UrlReferrer, System.Uri).Segments(Request.UrlReferrer.Segments.Length - 1) + ""
            Else
                divUpdate.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = "Error:GetPageDetails:" + ex.Message.ToString()
        End Try
    End Sub

End Class
