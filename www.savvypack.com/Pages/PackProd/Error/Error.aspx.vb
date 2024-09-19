Imports System.Data
Imports System.Data.OleDb
Imports System
Imports PackProdGetData
Imports PackProdUpInsData
Imports System.Collections
Imports System.IO.StringWriter
Imports System.Math
Imports System.Web.UI.HtmlTextWriter
Partial Class Pages_PackProd_Error
    Inherits System.Web.UI.Page
    Dim _strErrorCode As String

    Public Property ErrorCode() As String
        Get
            Return _strErrorCode
        End Get
        Set(ByVal value As String)
            Dim obj As New CryptoHelper
            _strErrorCode = obj.Decrypt(value)
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            ErrorCode = Request.QueryString("ErrorCode").ToString().Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&")
            GetPageDetails()
        Catch ex As Exception
            lblError.Text = "Error:Page_Load:" + ex.Message.ToString()
        End Try
    End Sub

    Protected Sub GetPageDetails()
        Dim objGetData As New PackProdGetData.Selectdata()
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
